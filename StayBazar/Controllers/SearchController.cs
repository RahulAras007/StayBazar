using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using System.Configuration;
using StayBazar.Common;
using System.Collections;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;


namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {

        #region Custom Methods
        public bool bDoSearch = false;
        private string RequestedCurrencyCode = "";
        public List<string> LocationList = new List<string>();
        public static List<CLayer.GDSCurrencyConversions> CurrencyConversionList = new List<CLayer.GDSCurrencyConversions>();

        #region Property variables
        private string PropertyCountryName = string.Empty;
        private string PropertyStateName = string.Empty;
        private string PropertyCityName = string.Empty;

        private int PropertyCountryID = 0;
        private int PropertyStateID = 0;
        private int PropertyCityID = 0;

        private string PropertyNoImage = string.Empty;


        #endregion

        public static bool IgnoreCertificateErrorHandler(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        private class SearchResults
        {
            public List<CLayer.SearchResult> Results;
            public int TotalRows;
            public string SessionID { get; set; }
            public int SequenceNumber { get; set; }
            public string SecurityToken { get; set; }
            public string Moreindicator { get; set; }
            public string TraceId { get; set; }

        }
        private async Task<SearchResults> SearchGDS(Models.SimpleSearchModel data, bool IsAmadeus = false)
        {
            LogHandler.AddLog(DateTime.Now.ToString());
            SearchResults searchr = new SearchResults();
            long id = 0;
            string HotelID = "";
            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();

            try
            {
                int adult, children, staytype, bedrooms;
                adult = 0;
                int.TryParse(data.Adults, out adult);
                children = 0;
                int.TryParse(data.Children, out children);
                staytype = 0;
                int.TryParse(data.StayType, out staytype);
                bedrooms = 0;
                int.TryParse(data.Bedrooms, out bedrooms);
                int totalCount = 0;

                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                srch.Adults = adult;
                srch.Children = children;
                srch.StayType = staytype;
                srch.Bedrooms = bedrooms;
                srch.Destination = data.Destination;
                Session["GDSCountry"] = "";
                Session["GDSCountryCode"] = "";
                Session["GDSCurrencyCode"] = "";

                string pKeyword = APIUtility.GDSKeyWord;

                PropertyNoImage = "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);


                if (srch.Destination != null && srch.Destination != "")
                {
                    srch.Destination = Common.Utils.SecureInputString(srch.Destination); //srch.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");

                    srch.Country = APIUtility.GetGDSCountry(srch.Destination);


                    //   srch.Country = APIUtility.GetCountryName(srch.Country);
                    Session["GDSCountry"] = string.IsNullOrEmpty(srch.Country) ? "" : srch.Country;
                    // int GlobalSearch = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalSearch"].ToString());

                    if (srch.Country.ToUpper() == "INDIA")
                    {
                        srch.Country = "";
                        Session["GDSCountry"] = "";
                    }
                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(srch.Country);
                    int GDSCountryCount = (srch.Country != "") ? GDSCountryList.Count : 0;
                    if (GDSCountryCount > 0)
                    {
                        srch.Destination = APIUtility.GetGDSDestination(srch.Destination);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        string sDestination = APIUtility.GetGDSDestination(srch.Destination);
                        srch.Destination = (sDestination == "") ? srch.Destination : sDestination;
                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (srch.Country == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }


                        }

                    }
                }

                srch.CheckIn = data.CheckIn;
                srch.CheckOut = data.CheckOut;
                Session["CheckIn"] = srch.CheckIn;
                Session["CheckOut"] = srch.CheckOut;
                Session["Destination"] = srch.Destination;
                Session["Adults"] = srch.Adults;
                Session["Children"] = srch.Children;
                //srch.StayType = staytype;
                Session["Bedrooms"] = srch.Bedrooms;

                // pass source co-ordinates
                srch.Lattitude = 0;
                srch.Longitude = 0;
                srch.Features = "";
                srch.DistanceInKm = data.distanceInKm;
                srch.RangeBudgetMax = data.rangeBudgetMax;
                srch.RangeBudgetMin = data.rangeBudgetMin;
                srch.StarRatingRange = data.starRatingRange;
                srch.Location = "";


                if (data.Destination != null && data.Destination != "")
                {
                    Common.Utils.Location pos;
                    string loc = BLayer.City.GetLocation(data.Destination);
                    if (loc == "")
                    {
                        srch.Lattitude = 0;
                        srch.Longitude = 0;
                    }
                    else
                    {
                        //pos = await Common.Utils.GetLocation(loc);
                        //srch.Lattitude = pos.Lattitude;
                        //srch.Longitude = pos.Longitude;
                        srch.Lattitude = 0;
                        srch.Longitude = 0;
                    }
                }

                int rip = 0;
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get(Common.Utils.SERARCH_MAX_ROWS), out rip);
                srch.NoOfRows = rip;
                int sr = data.CurrentPage - 1;
                if (sr < 0) sr = 0;
                sr = sr * srch.NoOfRows;
                srch.StaringRow = sr;
                srch.StayTypeGroup = "";


                srch.UserType = CLayer.Role.Roles.Customer;
                if (data.OrderBy < 1 || data.OrderBy > 3)
                    srch.SortOrder = CLayer.SearchCriteria.SortBy.PriceAsc;
                else
                    srch.SortOrder = (CLayer.SearchCriteria.SortBy)data.OrderBy;

                CLayer.Role.Roles val = new CLayer.Role.Roles();
                if (User.Identity.IsAuthenticated)
                {
                    long ud = 0;
                    long.TryParse(User.Identity.GetUserId(), out ud);
                    val = BLayer.User.GetRole(ud);
                    srch.UserType = CLayer.Role.GetRoleForSearch(val);
                    if (val == CLayer.Role.Roles.Corporate)
                        srch.LoggedInUser = ud;
                    else
                    {
                        if (val == CLayer.Role.Roles.CorporateUser)
                        {
                            srch.LoggedInUser = BLayer.B2B.GetCorporateIdOfUser(ud);
                        }
                        else
                            srch.LoggedInUser = 0;
                    }
                }
                // return BLayer.Property.Search(out totalCount, data.Destination, data.CheckIn, data.CheckOut, adult, children, staytype, bedrooms, "");
                // return BLayer.Property.SearchWithFilter(out totalCount, srch);
                TempData["SearchCriteria"] = data;


                //  List<SearchResults> AmadeusResulsts = new List<SearchResults>();

                //save keywords in country table
                CLayer.Country pCountry = new CLayer.Country();
                pCountry.KeyWords = APIUtility.GDSKeyWord;
                pCountry.CountryId = Convert.ToInt64(Session["GDSCountryID"]);

                //save keywords in country table end
                int KeyWords = BLayer.Country.GDSKeywordSave(pCountry);

                LogHandler.AddLog("Amadeus request start:-" + DateTime.Now.ToString());

                DateTime dtMainCheckOut = srch.CheckOut;
                srch.CheckOut = srch.CheckIn.AddDays(1);

                //karthikms added this code from 256 to 260
               string result = TamrindMultiSingleAvailability(srch);
                LogHandler.AddLog("----Ziac---- Time Taken for fetching Tamarind Result" + DateTime.Now.ToString());
               string TBOresult = TBOHotelSearch(srch);
                LogHandler.AddLog("----Ziac---- Time Taken for fetching TBO Result" + DateTime.Now.ToString());
                //string TBOresult = "";
                DataSet lds_auth = new DataSet();
                DataSet lds_tboauth = new DataSet();

                lds_auth.ReadXml(GenerateStreamFromString(result));
                LogHandler.AddLog("tamarind hotel data:-" + DateTime.Now.ToString());
                lds_tboauth.ReadXml(GenerateStreamFromString(TBOresult));
                LogHandler.AddLog("TBO hotel data:-" + DateTime.Now.ToString());

                srch.CheckOut = dtMainCheckOut;

                LogHandler.AddLog("Amadeus request end:-" + DateTime.Now.ToString());

                LogHandler.AddLog("main process start:-" + DateTime.Now.ToString());

                #region Transaction Log 

                //APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailability, 0);

                #endregion Transaction log end

                int StateID = 0;
                string StateName = "";
                string City = srch.Destination;
                int CountryID = 0;
                int CityId = 0;
                long OwnerID = 0;
                int GDSStateID = 0;
                int GDSCityID = 0;
                string GDStateName = "";

                OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    StateID = BLayer.State.GetStateID(srch.Destination).StateId;
                    StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                    CityId = BLayer.City.GetCityID(City).CityId;
                    CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;
                }
                else
                {
                    GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), srch.Destination);
                    GDStateName = BLayer.State.Get(GDSStateID).Name;
                    GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                    CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                }
                //long userId = GetUserId();
                decimal TamrindAPIPercentage = 0.0M;
                TamrindAPIPercentage = BLayer.Tamarind.APIPrice(srch.LoggedInUser);
                if (TamrindAPIPercentage == 0)
                {
                    TamrindAPIPercentage = BLayer.Tamarind.APIPriceAll();
                    LogHandler.AddLog("tamarind API Percentage:-" + DateTime.Now.ToString());
                }



                decimal TBOAPIPercentage = 0.0M;
                TBOAPIPercentage = BLayer.TBO.APIPrice(srch.LoggedInUser);
                if (TBOAPIPercentage == 0)
                {
                    TBOAPIPercentage = BLayer.TBO.APIPriceAll();
                    LogHandler.AddLog("TBO API pecentage:-" + DateTime.Now.ToString());
                }

                decimal price = 0.0M;
                decimal amount = 0.0M;
                decimal Tax = 0.0M;
                decimal EntityTax = 0.0M;
                long TamHotelID = 0;
                long ImgHotelID = 0;
                string image = "";

                Models.PropertyModel objData;
                CLayer.PropertyFiles file;
                long ldt_property;
                string lsz_city;
                int ldt_catid;
                int ldt_sgst_flag = 0;
                int CusStateId = 0;
                decimal Tamarind_GST = 0.0M;
                Models.AccommodationModel acc;
                int EntityFlag = 0;
                decimal lf_SGST = 0;
                decimal lf_CGST = 0;
                decimal lf_IGST = 0;

                EntityFlag = BLayer.SBEntity.SBEntityByStateId(StateID);
                string lsz_hsn_code = "";
                if (EntityFlag > 0)
                {
                    lsz_hsn_code = "996311";
                    ldt_sgst_flag = 1;
                }
                else
                {
                    lsz_hsn_code = "998552";
                    if (srch.LoggedInUser != 0)
                    {
                        CusStateId = BLayer.Address.GetStatidOnUserId(srch.LoggedInUser);
                    }
                    else
                    {
                        CusStateId = BLayer.Address.GetStatidOnUserId(Convert.ToInt64(User.Identity.GetUserId()));
                    }
                    EntityFlag = BLayer.SBEntity.SBEntityByStateId(CusStateId);
                    if (EntityFlag > 0)
                    {
                        ldt_sgst_flag = 1;

                    }
                    else
                    {
                        ldt_sgst_flag = 0;
                    }

                }


                CLayer.SearchResult AmadeusResults;
                if (lds_auth.Tables[0].Rows[0][0].ToString() != "FAIL")
                {
                    for (int i = 0; i < lds_auth.Tables["hotel"].Rows.Count; i++)
                    //for (int i = 0; i < 2; i++)
                    {

                        price = Convert.ToDecimal(lds_auth.Tables["hotel"].Rows[i]["TotalPrice"]);
                        TamHotelID = Convert.ToInt64(lds_auth.Tables["hotel"].Rows[i]["HotelId"]);
                        //for (int j = 0; j < lds_auth.Tables["Images"].Rows.Count; j++)
                        //    for (int j = 0; j < 2; j++)
                        //{
                        //    ImgHotelID = lds_auth.Tables["Images"].Rows[j]["HotelId"].ToString() != "" ? Convert.ToInt64(lds_auth.Tables["Images"].Rows[j]["HotelId"]) : Convert.ToInt64(lds_auth.Tables["Images"].Rows[j]["hotel_Id"]);
                        //    if (TamHotelID == ImgHotelID)
                        //    {
                        //        image = (lds_auth.Tables["Images"].Rows[j]["ImageName"]).ToString();
                        //        break;
                        //    }
                        //}
                        //ImgHotelID = lds_auth.Tables["Images"].Rows[i]["HotelId"].ToString() != "" ? Convert.ToInt64(lds_auth.Tables["Images"].Rows[i]["HotelId"]) : Convert.ToInt64(lds_auth.Tables["Images"].Rows[i]["hotel_Id"]);





                        Tamarind_GST = BLayer.APIPriceMarkup.GSTRate(lsz_hsn_code, Convert.ToInt32(price));

                        if (ldt_sgst_flag == 1)
                        {
                            lf_SGST = Tamarind_GST / 2;
                            lf_CGST = Tamarind_GST / 2;
                        }
                        else
                        {
                            lf_IGST = Tamarind_GST;
                        }

                        if (TamrindAPIPercentage == 0)
                        {
                            //prem sir told set default percentage as 10 on 30-01-2020
                            Tax = (10 * price) / 100;
                            amount = price + Tax;
                        }
                        else
                        {
                            Tax = (TamrindAPIPercentage * price) / 100;
                            amount = price + Tax;
                        }

                        objData = new Models.PropertyModel();
                        file = new CLayer.PropertyFiles();
                        acc = new Models.AccommodationModel();
                        // for property save

                        objData.Title = (lds_auth.Tables["hotel"].Rows[i]["HotelName"]).ToString();
                        objData.Location = "";
                        objData.Description = (lds_auth.Tables["hotel"].Rows[i]["Desc"]).ToString();
                        objData.Image = image;
                        objData.PositionLat = "";
                        objData.PositionLng = "";
                        objData.DistanceFromCity = 0;
                        objData.Rating = 0;
                        objData.City = (lds_auth.Tables["hotel"].Rows[i]["CityID"]).ToString();
                        objData.CityId = CityId;
                        objData.State = StateID;
                        objData.Country = CountryID;
                        objData.ZipCode = "";
                        objData.HotelId = (lds_auth.Tables["hotel"].Rows[i]["HotelId"]).ToString();
                        objData.TamarindHotelId = (lds_auth.Tables["hotel"].Rows[i]["HotelId"]).ToString();
                        objData.InventoryAPIType = 4;
                        objData.TamarindFlag = "Y";
                        objData.RateCardDetailedId = (lds_auth.Tables["hotel"].Rows[i]["RateCardDetailID"]).ToString();
                        objData.OwnerId = GetUserId();
                        objData.TaxPercentage = Tamarind_GST;
                        objData.GSTSlab = ldt_sgst_flag;
                        //ldt_property = await SaveAmadeus_Property(objData);
                        LogHandler.AddLog("----Ziac---- Time Taken for saving Tamarind Property" + DateTime.Now.ToString());

                        //file.PropertyId = ldt_property;
                        file.FileName = objData.Image;

                        //BLayer.PropertyFiles.Save(file);
                        LogHandler.AddLog("----Ziac---- Time Taken for Tamarind Property File Save Result" + DateTime.Now.ToString());
                        LogHandler.AddLog("Property Files Saves:-" + DateTime.Now.ToString());
                        //accommodation type save
                        ldt_catid = BLayer.AccommodationType.AccommodationTypeSave((lds_auth.Tables["hotel"].Rows[i]["RoomCatName"]).ToString(), Convert.ToInt32(lds_auth.Tables["hotel"].Rows[i]["RoomCategoryId"]));
                        LogHandler.AddLog("Accommodation Save:-" + DateTime.Now.ToString());
                        LogHandler.AddLog("----Ziac---- Time Taken for saving  Tamarind Property Accommodation Result" + DateTime.Now.ToString());
                        acc.AccommodationTypeId = ldt_catid;
                        acc.StayCategoryId = 39;
                        //acc.PropertyId = ldt_property;
                        acc.Description = (lds_auth.Tables["hotel"].Rows[i]["Desc"]).ToString();
                        acc.CheckIn = srch.CheckIn;
                        acc.CheckOut = srch.CheckOut;
                        acc.Amount = price;
                        acc.AmountWithTax = amount;
                        //AccommodationSaveByAPI(acc);
                        LogHandler.AddLog("----Ziac---- Time Taken for saving  Tamarind Property Accommodation API" + DateTime.Now.ToString());
                        AmadeusResults = new CLayer.SearchResult();
                        AmadeusResults.PropertyId = Convert.ToInt64(lds_auth.Tables["hotel"].Rows[i]["HotelId"]);
                        //AmadeusResults.PropertyId = ldt_property;
                        AmadeusResults.Amount = amount;

                        AmadeusResults.Title = (lds_auth.Tables["hotel"].Rows[i]["HotelName"]).ToString();
                        AmadeusResults.Location = "";
                        AmadeusResults.Description = (lds_auth.Tables["hotel"].Rows[i]["Desc"]).ToString();
                        AmadeusResults.ImageFile = image;
                        AmadeusResults.Lattitude = "";
                        AmadeusResults.Longitude = "";
                        AmadeusResults.Distance = 0;
                        AmadeusResults.StarRate = 0;
                        AmadeusResults.City = (lds_auth.Tables["hotel"].Rows[i]["CityID"]).ToString();
                        AmadeusResults.IsDistanceFromCity = false;
                        AmadeusResults.IsImageExists = true;
                        AmadeusResults.State = "";
                        AmadeusResults.Country = "";
                        AmadeusResults.Pincode = "";
                        AmadeusResults.PropertyCount = lds_auth.Tables["hotels"].Rows.Count;
                        AmadeusResults.Avail = 0;
                        AmadeusResults.HotelID = (lds_auth.Tables["hotel"].Rows[i]["HotelID"]).ToString();
                        AmadeusResults.InventoryAPIType = 4;
                        AmadeusResults.APIType = "";
                        AmadeusResults.GDSCurrencyCode = Session["GDSCurrencyCode"].ToString();
                        AmadeusResults.GDSRateConversion = 0;
                        AmadeusResults.StarRating = (lds_auth.Tables["hotel"].Rows[i]["HotelRating"]).ToString();
                        AmadeusResults.LocationName = "";
                        AmadeusResults.EntitlementOrder = 1;
                        AmadeusResults.MaximumDailyEntitlement = 34;

                        AmadeusResults.CityId = objData.CityId;
                        AmadeusResults.StateId = objData.State;
                        AmadeusResults.RateCardDetailedId = (lds_auth.Tables["hotel"].Rows[i]["RateCardDetailID"]).ToString();
                        AmadeusResults.TaxPercentage = Tamarind_GST;
                        AmadeusResults.GSTSlab = ldt_sgst_flag;
                        AmadeusResults.AccommodationTpeID = acc.AccommodationTypeId;
                        AmadeusResults.StayCategoryID = acc.StayCategoryId;
                        AmadeusResults.price = price;
                        AmadeusResultList.Add(AmadeusResults);
                        AmadeusResults = null;
                        //AmadeusResults = SetAmadeusResult(HotelItem, RoomStaysResultList, CityName, AmadeusPropertyID, lds_auth.Tables["images"].Rows[i]["images"], objData.Description, objData.Rating, objData.Address);
                    }
                }

                string lsz_Hotelsearchlist = "";
                decimal ld_apigst = 0.0M;

                if (int.Parse(lds_tboauth.Tables["Error"].Rows[0]["ErrorCode"].ToString()) == 0)
                {
                    if (int.Parse(lds_tboauth.Tables["HotelSearchResult"].Rows[0]["ResponseStatus"].ToString()) == 1)
                    {
                        // status is ok
                        lsz_Hotelsearchlist = lds_tboauth.Tables["HotelSearchResult"].Rows[0]["ResponseStatus"].ToString();
                    }
                }

                if (lsz_Hotelsearchlist != "")
                {

                    LogHandler.AddLog("----Ziac---- Time Taken for fetching TBO Room" + DateTime.Now.ToString());

                    lsz_city = BLayer.TBO.GetCityName(Convert.ToInt32(lds_tboauth.Tables[0].Rows[0]["CityId"]));
                    //for (int i = 0; i < lds_tboauth.Tables["HotelResults"].Rows.Count; i++)
                    for (int i = 0; i < lds_tboauth.Tables["HotelResults"].Rows.Count; i++)
                    //for (int i = 0; i < 1; i++)
                    {
                        //ldt_property = BLayer.TBO.UpdateHotelList((lds_tboauth.Tables[3].Rows[i]["HotelCode"]).ToString(), (lds_tboauth.Tables[3].Rows[i]["HotelName"]).ToString());

                        AmadeusResults = new CLayer.SearchResult();
                        objData = new Models.PropertyModel();
                        file = new CLayer.PropertyFiles();

                        acc = new Models.AccommodationModel();
                        price = Convert.ToDecimal(lds_tboauth.Tables[4].Rows[i]["PublishedPrice"]);
                        Tamarind_GST = BLayer.APIPriceMarkup.GSTRate(lsz_hsn_code, Convert.ToInt32(price));

                        if (ldt_sgst_flag == 1)
                        {
                            lf_SGST = Tamarind_GST / 2;
                            lf_CGST = Tamarind_GST / 2;
                        }
                        else
                        {
                            lf_IGST = Tamarind_GST;
                        }



                        if (TBOAPIPercentage == 0)
                        {
                            //prem sir told set default percentage as 10 on 30-01-2020
                            Tax = (10 * price) / 100;
                            EntityTax = (Tamarind_GST * price) / 100;
                            amount = price + Tax + EntityTax;
                        }
                        else
                        {
                            Tax = (TamrindAPIPercentage * price) / 100;
                            EntityTax = (Tamarind_GST * price) / 100;
                            amount = price + Tax + EntityTax;
                        }

                        // for property save
                        objData.Title = (lds_tboauth.Tables[3].Rows[i]["HotelName"]).ToString();
                        objData.Location = (lds_tboauth.Tables[3].Rows[i]["HotelLocation"]).ToString();
                        objData.Description = (lds_tboauth.Tables[3].Rows[i]["HotelDescription"]).ToString();
                        //objData.Image = (lds_tboauth.Tables[3].Rows[i]["HotelPicture"]).ToString();
                        objData.Image = image;
                        objData.PositionLat = (lds_tboauth.Tables[3].Rows[i]["Latitude"]).ToString();
                        objData.PositionLng = (lds_tboauth.Tables[3].Rows[i]["Longitude"]).ToString();
                        objData.DistanceFromCity = 0;
                        objData.Rating = Convert.ToInt16(lds_tboauth.Tables[3].Rows[i]["StarRating"]);
                        objData.City = lsz_city;
                        objData.CityId = CityId;
                        objData.State = StateID;
                        objData.Country = CountryID;
                        objData.ZipCode = "";
                        objData.HotelId = (lds_tboauth.Tables[3].Rows[i]["HotelCode"]).ToString();
                        objData.TBOHotelId = (lds_tboauth.Tables[3].Rows[i]["HotelCode"]).ToString();
                        objData.InventoryAPIType = 5;
                        objData.TBOFlag = "Y";
                        objData.OwnerId = GetUserId();
                        objData.TaxPercentage = Tamarind_GST;
                        objData.GSTSlab = ldt_sgst_flag;
                        //ldt_property = await SaveAmadeus_Property(objData);
                        LogHandler.AddLog("----Ziac---- Time Taken for saving TBO Property" + DateTime.Now.ToString());

                        //file.PropertyId = ldt_property;
                        file.FileName = objData.Image;

                        //BLayer.PropertyFiles.Save(file);

                        srch.TraceId = (lds_tboauth.Tables[0].Rows[0]["TraceId"]).ToString();
                        srch.ResultIndex = Convert.ToInt32(lds_tboauth.Tables[3].Rows[i]["ResultIndex"]);

                        //Was Commented to speed Up 21-03-2020


                        //srch.TraceId = (lds_tboauth.Tables[0].Rows[0]["TraceId"]).ToString();
                        //srch.ResultIndex = Convert.ToInt32(lds_tboauth.Tables[3].Rows[i]["ResultIndex"]);
                        //srch.HotelCode = (lds_tboauth.Tables[3].Rows[i]["HotelCode"]).ToString();
                        //string TBORoomResult = TBOHotelRoomRequest(srch);
                        //DataSet lds_tboroom = new DataSet();
                        //lds_tboroom.ReadXml(GenerateStreamFromString(TBORoomResult));



                        //if (int.Parse(lds_tboroom.Tables["Error"].Rows[0]["ErrorCode"].ToString()) == 0)
                        //{
                        //    //for (int j = 0; j < lds_tboroom.Tables["HotelRoomsDetails"].Rows.Count; j++)
                        //    for (int j = 0; j < 1; j++)
                        //    {
                        //        price = Convert.ToDecimal((lds_tboroom.Tables[4].Rows[j]["OfferedPriceRoundedOff"]).ToString());
                        //        ld_apigst = Convert.ToDecimal((lds_tboroom.Tables[4].Rows[j]["TotalGSTAmount"]).ToString());
                        //        if (TBOAPIPercentage == 0)
                        //        {
                        //            //prem sir told set default percentage as 10 on 30-01-2020
                        //            Tax = (10 * (price + ld_apigst)) / 100;
                        //            amount = price + ld_apigst + Tax;
                        //            //EntityTax = (Tamarind_GST * amount) / 100;
                        //            //amount = amount + EntityTax;
                        //        }
                        //        else
                        //        {
                        //            Tax = (TamrindAPIPercentage * (price + ld_apigst)) / 100;
                        //            amount = price + ld_apigst + Tax;
                        //            //EntityTax = (Tamarind_GST * amount) / 100;
                        //            //amount = amount + EntityTax;
                        //        }

                        //        ldt_catid = BLayer.AccommodationType.TBOAccommodationTypeSave(lds_tboroom.Tables[2].Rows[j]["RoomTypeName"].ToString());
                        //        acc.AccommodationTypeId = ldt_catid;
                        //        acc.StayCategoryId = 39;//default set as hotel 
                        //        acc.PropertyId = ldt_property;
                        //        acc.Description = (lds_tboauth.Tables[3].Rows[i]["HotelDescription"]).ToString();
                        //        acc.CheckIn = srch.CheckIn;
                        //        acc.CheckOut = srch.CheckOut;
                        //        acc.Amount = price;
                        //        acc.AmountWithTax = amount;
                        //        AccommodationSaveByAPI(acc);
                        //        LogHandler.AddLog("----Ziac---- Time Taken for saving TBO Accommodation" + DateTime.Now.ToString());
                        //    }
                        //}
                        ////  Till Here 21-03-2020




                        AmadeusResults.PropertyId = Convert.ToInt64(lds_tboauth.Tables[3].Rows[i]["HotelCode"]);
                        AmadeusResults.Title = (lds_tboauth.Tables[3].Rows[i]["HotelName"]).ToString();
                        AmadeusResults.Location = (lds_tboauth.Tables[3].Rows[i]["HotelLocation"]).ToString();
                        AmadeusResults.Description = (lds_tboauth.Tables[3].Rows[i]["HotelDescription"]).ToString();
                        //AmadeusResults.ImageFile = (lds_tboauth.Tables[3].Rows[i]["HotelPicture"]).ToString();
                        AmadeusResults.ImageFile = image;
                        AmadeusResults.Lattitude = (lds_tboauth.Tables[3].Rows[i]["Latitude"]).ToString();
                        AmadeusResults.Longitude = (lds_tboauth.Tables[3].Rows[i]["Longitude"]).ToString();
                        AmadeusResults.Distance = 0;
                        AmadeusResults.StarRate = Convert.ToInt16(lds_tboauth.Tables[3].Rows[i]["StarRating"]);
                        AmadeusResults.City = lsz_city;
                        AmadeusResults.IsDistanceFromCity = false;
                        AmadeusResults.IsImageExists = true;
                        AmadeusResults.State = "";
                        AmadeusResults.Country = "";
                        AmadeusResults.Pincode = "";
                        AmadeusResults.PropertyCount = lds_tboauth.Tables[3].Rows.Count;
                        AmadeusResults.Avail = 0;
                        AmadeusResults.HotelID = (lds_tboauth.Tables[3].Rows[i]["HotelCode"]).ToString();
                        AmadeusResults.InventoryAPIType = 5;
                        AmadeusResults.APIType = "TBO";
                        AmadeusResults.GDSRateConversion = 0;
                        AmadeusResults.StarRating = "";
                        AmadeusResults.LocationName = "";
                        AmadeusResults.EntitlementOrder = 1;
                        AmadeusResults.MaximumDailyEntitlement = 34;
                        AmadeusResults.Amount = amount;
                        //AmadeusResults.Amount = Convert.ToDecimal((lds_tboauth.Tables[4].Rows[i]["OfferedPriceRoundedOff"]).ToString());
                        AmadeusResults.GDSCurrencyCode = (lds_tboauth.Tables[4].Rows[i]["CurrencyCode"]).ToString();

                        AmadeusResults.CityId = objData.CityId;
                        AmadeusResults.StateId = objData.State;
                        AmadeusResults.TaxPercentage = Tamarind_GST;
                        AmadeusResults.GSTSlab = ldt_sgst_flag;
                        AmadeusResults.price = price;
                        AmadeusResults.TraceID = srch.TraceId;
                        AmadeusResults.ResultIndex = srch.ResultIndex;
                        //AmadeusResults.ErrorCode = int.Parse(lds_tboroom.Tables["Error"].Rows[0]["ErrorCode"].ToString());

                        AmadeusResultList.Add(AmadeusResults);
                        LogHandler.AddLog("----Ziac---- Total Results Count is : " + AmadeusResultList.Count);
                        AmadeusResults = null;
                    }
                }

                searchr.Results = AmadeusResultList;
                searchr.Results = AmadeusResultList.Where(x => x.Amount > 0).ToList();
                if (User.Identity.IsAuthenticated)
                {
                    long ud = 0;
                    long.TryParse(User.Identity.GetUserId(), out ud);
                    //searchr.Results = SetFrequentHotelsOrder(searchr.Results, ud, srch.Destination);
                }

                searchr.TotalRows = searchr.Results.Count;
                if (searchr.TotalRows > 0)
                {
                    searchr.SessionID = Convert.ToString(Session["SessionId"]);
                    //searchr.SessionID = (lds_tboauth.Tables["HotelSearchResult"].Rows[0]["TraceID"]).ToString();
                    searchr.SequenceNumber = Convert.ToInt32(Session["SequenceNumber"]);
                    searchr.SecurityToken = Convert.ToString(Session["SecurityToken"]);
                    searchr.Moreindicator = Convert.ToString(Session["Moreindicator"]);
                    //Session["TraceID"] = (lds_tboauth.Tables["HotelSearchResult"].Rows[0]["TraceID"]).ToString();
                }


                //else if (HotelResultAdvFirst.Body != null)
                //{

                //    //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                //    //{
                //    //    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                //    //    CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                //    //    objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                //    //    Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                //    //    objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                //    //    objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                //    //    objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                //    //    Session["GDSCurrencyConversion"] = objCurrencyConversion;
                //    //    CurrencyConversionList = APIUtility.GetCurrencyConversions(result);
                //    //}
                //    //if (Convert.ToString(Session["GDSCountry"]) == "")
                //    //{
                //    //    Session["GDSCurrencyConversion"] = null;
                //    //}



                //    //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                //    //{
                //    //    //List<string> HotelIDS = new List<string>();
                //    //    //foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                //    //    //{
                //    //    //    HotelIDS.Add(item.BasicPropertyInfo.HotelCode);
                //    //    //}

                //    //    //List<CLayer.GDSProperties> objGDSList = new List<CLayer.GDSProperties>();
                //    //    //DataTable dt = new DataTable();
                //    //    //if (User.Identity.IsAuthenticated)
                //    //    //{
                //    //    //    if ((val == CLayer.Role.Roles.Administrator) || (val == CLayer.Role.Roles.Staff))
                //    //    //    {
                //    //    //        dt = BLayer.Property.SearchForGDSProperties(HotelIDS);
                //    //    //    }
                //    //    //    else
                //    //    //    {
                //    //    //        dt = BLayer.Property.SearchForGDSPropertiesWithUser(HotelIDS);
                //    //    //    }

                //    //    //}
                //    //    //else
                //    //    //{
                //    //    //    dt = BLayer.Property.SearchForGDSPropertiesWithUser(HotelIDS);

                //    //    //}


                //    //    //if (dt.Rows.Count > 0)
                //    //    //{
                //    //    //    foreach (DataRow dr in dt.Rows)
                //    //    //    {
                //    //    //        CLayer.GDSProperties objGDS = new CLayer.GDSProperties();
                //    //    //        objGDS.Propertyid = Convert.ToInt64(dr["propertyid"]);
                //    //    //        objGDS.Hotel_Id = Convert.ToString(dr["Hotel_Id"]);
                //    //    //        objGDS.ImageUrl = Convert.ToString(dr["ImageUrl"]);
                //    //    //        objGDS.Description = Convert.ToString(dr["Description"]);
                //    //    //        objGDS.Rating = Convert.ToInt32(dr["Rating"]);
                //    //    //        objGDSList.Add(objGDS);

                //    //    //    }
                //    //    //}
                //    //    //foreach (var HotelCodes in objGDSList)
                //    //    //{
                //    //    //    foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays.Where(x => x.BasicPropertyInfo.HotelCode == HotelCodes.Hotel_Id))
                //    //    //    {
                //    //    //        HotelID = item.BasicPropertyInfo.HotelCode;
                //    //    //        HotelItemAdvFirst = item;
                //    //    //        Models.PropertyModel objData = new Models.PropertyModel();
                //    //    //        objData.PropertyId = 0;
                //    //    //        objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                //    //    //        //    objData.Description = "";
                //    //    //        objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                //    //    //        objData.OwnerId = OwnerID;
                //    //    //        //  objData.CityName = item.BasicPropertyInfo.Address.CityName;



                //    //    //        objData.Description = GetDescription(objGDSList, HotelID);// objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Description;
                //    //    //        objData.Image = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().ImageUrl;
                //    //    //        objData.PropertyId = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Propertyid;
                //    //    //        objData.Rating = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Rating;


                //    //    //        id = objData.PropertyId;

                //    //    //        objData.CityName = srch.Destination;
                //    //    //        CityName = srch.Destination;
                //    //    //        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                //    //    //        {
                //    //    //            objData.State = Convert.ToInt32(StateID);
                //    //    //            objData.StateName = StateName;
                //    //    //            objData.City = CityName;
                //    //    //            objData.CityId = Convert.ToInt32(CityId);

                //    //    //            PropertyCityID = Convert.ToInt32(CityId);
                //    //    //            PropertyCityName = CityName;
                //    //    //            PropertyStateID = Convert.ToInt32(StateID);
                //    //    //            PropertyStateName = StateName;
                //    //    //        }
                //    //    //        else
                //    //    //        {

                //    //    //            objData.State = GDSStateID;
                //    //    //            //   objData.StateName = "Others";
                //    //    //            objData.StateName = GDStateName;
                //    //    //            objData.City = objData.CityName;
                //    //    //            objData.CityId = GDSCityID;

                //    //    //            PropertyCityID = Convert.ToInt32(GDSCityID);
                //    //    //            PropertyCityName = objData.CityName;
                //    //    //            PropertyStateID = GDSStateID;
                //    //    //            PropertyStateName = GDStateName;
                //    //    //        }

                //    //    //        objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                //    //    //        objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                //    //    //        objData.Country = CountryID;
                //    //    //        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                //    //    //        {
                //    //    //            objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                //    //    //        }
                //    //    //        else
                //    //    //        {
                //    //    //            objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                //    //    //        }
                //    //      //        PropertyCountryID = CountryID;
                //    //    //        PropertyCountryName = objData.CountryName;

                //    //    //        if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                //    //    //        {
                //    //    //            objData.Location = objData.CityName + ", " + objData.StateName;
                //    //    //            if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                //    //    //            objData.Location = objData.Location + ", " + objData.CountryName;

                //    //    //        }

                //    //    //        objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                //    //    //        objData.HotelId = item.BasicPropertyInfo.HotelCode;

                //    //    //        AmadeusPropertyID = objData.PropertyId;

                //    //    //        long AccomodationId = 0;
                //    //    //        string RoomStayRPH = item.RoomStayRPH;
                //    //    //        List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                //    //    //        if (!string.IsNullOrEmpty(RoomStayRPH))
                //    //    //        {

                //    //    //            string[] RoomStayRPHList = RoomStayRPH.Split(' ');

                //    //    //            RoomStaysResultList = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                //    //    //            Session["Moreindicator"] = APIUtility.GetMoreIndicator(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays.MoreIndicator);

                //    //    //        }
                //    //    //        CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();

                //    //    //        AmadeusResults = SetAmadeusResult(HotelItemAdvFirst, RoomStaysResultList, CityName, AmadeusPropertyID, objData.Image, objData.Description, objData.Rating, objData.Address);

                //    //    //        AmadeusResultList.Add(AmadeusResults);
                //    //    //    }
                //    //    //}


                //    //}
                //    //searchr.Results = AmadeusResultList.Where(x => x.Amount > 0).ToList();
                //    //if (User.Identity.IsAuthenticated)
                //    //{
                //    //    long ud = 0;
                //    //    long.TryParse(User.Identity.GetUserId(), out ud);
                //    //    searchr.Results = SetFrequentHotelsOrder(searchr.Results, ud, srch.Destination);
                //    //}
                //    //searchr.TotalRows = searchr.Results.Count;
                //    //if (searchr.TotalRows > 0)
                //    //{
                //    //    searchr.SessionID = Convert.ToString(Session["SessionId"]);
                //    //    searchr.SequenceNumber = Convert.ToInt32(Session["SequenceNumber"]);
                //    //    searchr.SecurityToken = Convert.ToString(Session["SecurityToken"]);
                //    //    searchr.Moreindicator = Convert.ToString(Session["Moreindicator"]);
                //    //}

                //    //if (string.IsNullOrEmpty(searchr.Moreindicator))
                //    //{
                //    //    Session["SessionId"] = "";
                //    //    Session["SequenceNumber"] = "";
                //    //    Session["SecurityToken"] = "";
                //    //    Session["Moreindicator"] = "";
                //    //}
                //}

                //else
                //{
                //    //AmadeusResultList = new List<CLayer.SearchResult>();
                //    //searchr.Results = AmadeusResultList.Where(x => x.Amount > 0).ToList();
                //    //if (User.Identity.IsAuthenticated)
                //    //{
                //    //    long ud = 0;
                //    //    long.TryParse(User.Identity.GetUserId(), out ud);
                //    //    searchr.Results = SetFrequentHotelsOrder(searchr.Results, ud, srch.Destination);
                //    //}


                //    //searchr.TotalRows = 0;
                //}



                //else
                //{
                //    searchr.Results = await FillLocations(BLayer.Property.SearchWithFilter(out totalCount, srch));
                //    searchr.TotalRows = totalCount;
                //}

            }
            catch (Exception ex)
            {
                LogHandler.AddLog(HotelID.ToString() + "-" + ex.Message);
                searchr = null;
            }

            LogHandler.AddLog("main process end:-" + DateTime.Now.ToString());

            //  LogHandler.AddLog(DateTime.Now.ToString());

            TempData["LocationList"] = LocationList;

            return searchr;
        }

        private List<CLayer.SearchResult> SetFrequentHotelsOrder(List<CLayer.SearchResult> pHotelsList, long pUserID, string pDestination)
        {
            long pMaximumDailyEntitlement = GetDailyLodgingEntitlementAmount(pUserID, PropertyCityID); //BLayer.B2BUser.GetMaximumDailyEntitlement(pUserID);
            List<CLayer.Property> DefaultHotels = BLayer.Property.GetDefaultHotels(pUserID, pDestination);
            ArrayList objHotelList = new ArrayList();
            foreach (var item in DefaultHotels)
            {
                objHotelList.Add(item.PropertyId);
                // item.MaximumDailyEntitlement = pMaximumDailyEntitlement;
            }

            var result = pHotelsList.Where(x => objHotelList.Contains(x.PropertyId));
            foreach (var item in result)
            {
                item.EntitlementOrder = 1;
                item.MaximumDailyEntitlement = pMaximumDailyEntitlement;
            }
            var otherResult = pHotelsList.Where(x => !objHotelList.Contains(x.PropertyId));
            foreach (var item in otherResult)
            {
                item.EntitlementOrder = 0;
                item.MaximumDailyEntitlement = pMaximumDailyEntitlement;

            }

            List<CLayer.SearchResult> FinalLResult = new List<CLayer.SearchResult>();
            FinalLResult.AddRange(result);
            FinalLResult.AddRange(otherResult);

            //long MaximumEntitlement = BLayer.User.GetMaximumEntitlement(pUserID);
            //if(MaximumEntitlement>0)
            //{
            //    FinalLResult = FinalLResult.Where(x => x.Amount <= MaximumEntitlement).ToList();
            //}

            return FinalLResult;
        }
        private long GetDailyLodgingEntitlementAmount(long pUserID, long pCityID)
        {
            long result = BLayer.B2BUser.GetDailyLodgingEntitlementAmount(pUserID, pCityID);
            return result;
        }

        private async Task<SearchResults> SearchMoreGDS(Models.SimpleSearchModel data, bool IsAmadeus = false)
        {
            TamarindBasic.BaseDetailsClient client = new TamarindBasic.BaseDetailsClient();

            client.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings.Get("TamarindUserName");
            client.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings.Get("TamarindPassword");

            LogHandler.AddLog(DateTime.Now.ToString());
            SearchResults searchr = new SearchResults();
            long id = 0;
            string HotelID = "";
            string SessionID = data.SessionID;
            int SequenceNumber = data.SequenceNumber;
            string SecurityToken = data.SecurityToken;
            string MoreDataIndicator = data.Moreindicator;

            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();

            try
            {
                int adult, children, staytype, bedrooms;
                adult = 0;
                int.TryParse(data.Adults, out adult);
                children = 0;
                int.TryParse(data.Children, out children);
                staytype = 0;
                int.TryParse(data.StayType, out staytype);
                bedrooms = 0;
                int.TryParse(data.Bedrooms, out bedrooms);
                int totalCount = 0;

                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                srch.Adults = adult;
                srch.Children = children;
                srch.StayType = staytype;
                srch.Bedrooms = bedrooms;
                srch.Destination = data.Destination;
                Session["GDSCountry"] = "";
                Session["GDSCountryCode"] = "";
                Session["GDSCurrencyCode"] = "";

                string pKeyword = APIUtility.GDSKeyWord;

                PropertyNoImage = "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);


                if (srch.Destination != null && srch.Destination != "")
                {
                    srch.Destination = Common.Utils.SecureInputString(srch.Destination); //srch.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");

                    srch.Country = APIUtility.GetGDSCountry(srch.Destination);


                    //   srch.Country = APIUtility.GetCountryName(srch.Country);
                    Session["GDSCountry"] = string.IsNullOrEmpty(srch.Country) ? "" : srch.Country;
                    // int GlobalSearch = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalSearch"].ToString());

                    if (srch.Country.ToUpper() == "INDIA")
                    {
                        srch.Country = "";
                        Session["GDSCountry"] = "";
                    }
                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(srch.Country);
                    int GDSCountryCount = (srch.Country != "") ? GDSCountryList.Count : 0;
                    if (GDSCountryCount > 0)
                    {
                        srch.Destination = APIUtility.GetGDSDestination(srch.Destination);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        string sDestination = APIUtility.GetGDSDestination(srch.Destination);
                        srch.Destination = (sDestination == "") ? srch.Destination : sDestination;
                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (srch.Country == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }


                        }

                    }
                }

                srch.CheckIn = data.CheckIn;
                srch.CheckOut = data.CheckOut;
                Session["CheckIn"] = srch.CheckIn;
                Session["CheckOut"] = srch.CheckOut;
                Session["Destination"] = srch.Destination;
                Session["Adults"] = srch.Adults;
                Session["Children"] = srch.Children;
                //srch.StayType = staytype;
                Session["Bedrooms"] = srch.Bedrooms;

                // pass source co-ordinates
                srch.Lattitude = 0;
                srch.Longitude = 0;
                srch.Features = "";
                srch.DistanceInKm = data.distanceInKm;
                srch.RangeBudgetMax = data.rangeBudgetMax;
                srch.RangeBudgetMin = data.rangeBudgetMin;
                srch.StarRatingRange = data.starRatingRange;
                srch.Location = "";


                if (data.Destination != null && data.Destination != "")
                {
                    Common.Utils.Location pos;
                    string loc = BLayer.City.GetLocation(data.Destination);
                    if (loc == "")
                    {
                        srch.Lattitude = 0;
                        srch.Longitude = 0;
                    }
                    else
                    {
                        //pos = await Common.Utils.GetLocation(loc);
                        //srch.Lattitude = pos.Lattitude;
                        //srch.Longitude = pos.Longitude;
                        srch.Lattitude = 0;
                        srch.Longitude = 0;
                    }
                }

                int rip = 0;
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get(Common.Utils.SERARCH_MAX_ROWS), out rip);
                srch.NoOfRows = rip;
                int sr = data.CurrentPage - 1;
                if (sr < 0) sr = 0;
                sr = sr * srch.NoOfRows;
                srch.StaringRow = sr;
                srch.StayTypeGroup = "";


                srch.UserType = CLayer.Role.Roles.Customer;
                if (data.OrderBy < 1 || data.OrderBy > 3)
                    srch.SortOrder = CLayer.SearchCriteria.SortBy.PriceAsc;
                else
                    srch.SortOrder = (CLayer.SearchCriteria.SortBy)data.OrderBy;

                CLayer.Role.Roles val = new CLayer.Role.Roles();
                if (User.Identity.IsAuthenticated)
                {
                    long ud = 0;
                    long.TryParse(User.Identity.GetUserId(), out ud);
                    val = BLayer.User.GetRole(ud);
                    srch.UserType = CLayer.Role.GetRoleForSearch(val);
                    if (val == CLayer.Role.Roles.Corporate)
                        srch.LoggedInUser = ud;
                    else
                    {
                        if (val == CLayer.Role.Roles.CorporateUser)
                        {
                            srch.LoggedInUser = BLayer.B2B.GetCorporateIdOfUser(ud);
                        }
                        else
                            srch.LoggedInUser = 0;
                    }
                }
                // return BLayer.Property.Search(out totalCount, data.Destination, data.CheckIn, data.CheckOut, adult, children, staytype, bedrooms, "");
                // return BLayer.Property.SearchWithFilter(out totalCount, srch);
                TempData["SearchCriteria"] = data;

                if (IsAmadeus)
                {
                    //  List<SearchResults> AmadeusResulsts = new List<SearchResults>();

                    //save keywords in country table
                    CLayer.Country pCountry = new CLayer.Country();
                    pCountry.KeyWords = APIUtility.GDSKeyWord;
                    pCountry.CountryId = Convert.ToInt64(Session["GDSCountryID"]);

                    //save keywords in country table end
                    int KeyWords = BLayer.Country.GDSKeywordSave(pCountry);

                    srch.SessionID = SessionID;
                    srch.SequenceNumber = SequenceNumber;
                    srch.SecurityToken = SecurityToken;
                    srch.Moreindicator = MoreDataIndicator;

                    DateTime dtMainCheckOut = srch.CheckOut;
                    srch.CheckOut = srch.CheckIn.AddDays(1);
                    //string result = HotelMultiSingleAvailability(srch, true, "");
                    TamarindBasic.ListHotelParam cityparam = new TamarindBasic.ListHotelParam();
                    cityparam.CityID = "BLR";
                    string result = client.GetHotelList(cityparam);

                    srch.CheckOut = dtMainCheckOut;

                    LogHandler.AddLog(DateTime.Now.ToString());

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailability, 0);

                    #endregion Transaction log end

                    Serializer ser = new Serializer();

                    CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                    CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                    CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();

                    try
                    {

                        HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                        Session["SessionId"] = HotelResult.Header.Session.SessionId;
                        Session["SequenceNumber"] = Convert.ToInt32(HotelResult.Header.Session.SequenceNumber);
                        Session["SecurityToken"] = HotelResult.Header.Session.SecurityToken;
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            HotelResultAdv = ser.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                            Session["SessionId"] = HotelResultAdv.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelResultAdv.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelResultAdv.Header.Session.SecurityToken;
                        }
                        catch (Exception ex1)
                        {
                            HotelResultAdvFirst = ser.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                            Session["SessionId"] = HotelResultAdvFirst.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelResultAdvFirst.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelResultAdvFirst.Header.Session.SecurityToken;
                        }

                    }


                    long AmadeusPropertyID = 0;
                    string CityName = string.Empty;
                    var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                    var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();

                    //var HotelItemAdv = new CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStaysHotelStay..OTA_HotelAvailRSHotelStay();
                    //var RoomStayItemListAdv = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSRoomStaysRoomStay>();


                    long StateID = 0;
                    string StateName = "";
                    string City = srch.Destination;
                    int CountryID = 0;
                    long CityId = 0;
                    long OwnerID = 0;
                    int GDSStateID = 0;
                    int GDSCityID = 0;
                    string GDStateName = "";

                    OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                    if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                    {
                        StateID = BLayer.State.GetStateID(srch.Destination).StateId;
                        StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                        CityId = BLayer.City.GetCityID(City).CityId;
                        CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;
                    }
                    else
                    {
                        GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), srch.Destination);
                        GDStateName = BLayer.State.Get(GDSStateID).Name;
                        GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                        CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                    }





                    #region Hotel Result
                    if (HotelResult.Body != null)
                    {

                        if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                            CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                            objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                            Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                            objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                            objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                            objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                            Session["GDSCurrencyConversion"] = objCurrencyConversion;
                            CurrencyConversionList = APIUtility.GetCurrencyConversions(result);
                        }
                        if (Convert.ToString(Session["GDSCountry"]) == "")
                        {
                            Session["GDSCurrencyConversion"] = null;
                        }


                        if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                        {

                            List<string> HotelIDS = new List<string>();
                            foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelIDS.Add(item.BasicPropertyInfo.HotelCode);
                            }

                            List<CLayer.GDSProperties> objGDSList = new List<CLayer.GDSProperties>();
                            DataTable dt = new DataTable();
                            if (User.Identity.IsAuthenticated)
                            {
                                if ((val == CLayer.Role.Roles.Administrator) || (val == CLayer.Role.Roles.Staff))
                                {
                                    dt = BLayer.Property.SearchForGDSProperties(HotelIDS);
                                }
                                else
                                {
                                    dt = BLayer.Property.SearchForGDSPropertiesWithUser(HotelIDS);
                                }

                            }
                            else
                            {
                                dt = BLayer.Property.SearchForGDSPropertiesWithUser(HotelIDS);

                            }
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    CLayer.GDSProperties objGDS = new CLayer.GDSProperties();
                                    objGDS.Propertyid = Convert.ToInt64(dr["propertyid"]);
                                    objGDS.Hotel_Id = Convert.ToString(dr["Hotel_Id"]);
                                    objGDS.ImageUrl = Convert.ToString(dr["ImageUrl"]);
                                    objGDS.Description = Convert.ToString(dr["Description"]);
                                    objGDS.Rating = Convert.ToInt32(dr["Rating"]);
                                    objGDSList.Add(objGDS);

                                }
                            }
                            //dt.Select()


                            foreach (var HotelCodes in objGDSList)
                            {
                                foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays.Where(x => x.BasicPropertyInfo.HotelCode == HotelCodes.Hotel_Id))
                                {
                                    HotelItem = item;
                                    Models.PropertyModel objData = new Models.PropertyModel();
                                    objData.PropertyId = 0;
                                    objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                                    objData.Description = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Description;
                                    objData.Image = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().ImageUrl;
                                    objData.PropertyId = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Propertyid;
                                    objData.Rating = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Rating;


                                    objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                    objData.OwnerId = OwnerID;
                                    //  objData.CityName = item.BasicPropertyInfo.Address.CityName;

                                    objData.CityName = srch.Destination;
                                    CityName = srch.Destination;
                                    if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                    {
                                        objData.State = Convert.ToInt32(StateID);
                                        objData.StateName = StateName;
                                        objData.City = CityName;
                                        objData.CityId = Convert.ToInt32(CityId);

                                        PropertyCityID = Convert.ToInt32(CityId);
                                        PropertyCityName = CityName;
                                        PropertyStateID = Convert.ToInt32(StateID);
                                        PropertyStateName = StateName;

                                    }
                                    else
                                    {

                                        objData.State = GDSStateID;
                                        //   objData.StateName = "Others";
                                        objData.StateName = GDStateName;
                                        objData.City = objData.CityName;
                                        objData.CityId = GDSCityID;

                                        PropertyCityID = Convert.ToInt32(GDSCityID);
                                        PropertyCityName = objData.CityName;
                                        PropertyStateID = GDSStateID;
                                        PropertyStateName = GDStateName;
                                    }

                                    objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                                    objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                                    objData.Country = CountryID;
                                    if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                    {
                                        objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                    }
                                    else
                                    {
                                        objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                                    }

                                    PropertyCountryID = CountryID;
                                    PropertyCountryName = objData.CountryName;


                                    if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                                    {
                                        objData.Location = objData.CityName + ", " + objData.StateName;
                                        if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                        objData.Location = objData.Location + ", " + objData.CountryName;

                                    }



                                    objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                    objData.HotelId = item.BasicPropertyInfo.HotelCode;

                                    AmadeusPropertyID = objData.PropertyId;

                                    long AccomodationId = 0;
                                    string RoomStayRPH = item.RoomStayRPH;
                                    List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                    if (!string.IsNullOrEmpty(RoomStayRPH))
                                    {

                                        string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                        RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                                        Session["Moreindicator"] = APIUtility.GetMoreIndicator(HotelResult.Body.OTA_HotelAvailRS.RoomStays.MoreIndicator);

                                    }
                                    CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();

                                    AmadeusResults = SetAmadeusResult(HotelItem, RoomStaysResultList, CityName, AmadeusPropertyID, objData.Image, objData.Description, objData.Rating, objData.Address);


                                    AmadeusResultList.Add(AmadeusResults);

                                    // Session["GDSCurrencyCode"] = "";

                                }
                            }

                        }

                        #endregion
                        searchr.Results = AmadeusResultList;
                        searchr.Results = AmadeusResultList.Where(x => x.Amount > 0).ToList();
                        if (User.Identity.IsAuthenticated)
                        {
                            long ud = 0;
                            long.TryParse(User.Identity.GetUserId(), out ud);
                            searchr.Results = SetFrequentHotelsOrder(searchr.Results, ud, srch.Destination);
                        }
                        searchr.TotalRows = searchr.Results.Count;
                        if (searchr.TotalRows > 0)
                        {
                            searchr.SessionID = Convert.ToString(Session["SessionId"]);
                            searchr.SequenceNumber = Convert.ToInt32(Session["SequenceNumber"]);
                            searchr.SecurityToken = Convert.ToString(Session["SecurityToken"]);
                            searchr.Moreindicator = Convert.ToString(Session["Moreindicator"]);
                        }
                    }
                    else if (HotelResultAdvFirst.Body != null)
                    {

                        if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                            CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                            objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                            Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                            objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                            objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                            objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                            Session["GDSCurrencyConversion"] = objCurrencyConversion;
                            CurrencyConversionList = APIUtility.GetCurrencyConversions(result);
                        }
                        if (Convert.ToString(Session["GDSCountry"]) == "")
                        {
                            Session["GDSCurrencyConversion"] = null;
                        }



                        if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                        {
                            List<string> HotelIDS = new List<string>();
                            foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelIDS.Add(item.BasicPropertyInfo.HotelCode);
                            }

                            List<CLayer.GDSProperties> objGDSList = new List<CLayer.GDSProperties>();
                            DataTable dt = new DataTable();
                            if (User.Identity.IsAuthenticated)
                            {
                                if ((val == CLayer.Role.Roles.Administrator) || (val == CLayer.Role.Roles.Staff))
                                {
                                    dt = BLayer.Property.SearchForGDSProperties(HotelIDS);
                                }
                                else
                                {
                                    dt = BLayer.Property.SearchForGDSPropertiesWithUser(HotelIDS);
                                }

                            }
                            else
                            {
                                dt = BLayer.Property.SearchForGDSPropertiesWithUser(HotelIDS);

                            }


                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    CLayer.GDSProperties objGDS = new CLayer.GDSProperties();
                                    objGDS.Propertyid = Convert.ToInt64(dr["propertyid"]);
                                    objGDS.Hotel_Id = Convert.ToString(dr["Hotel_Id"]);
                                    objGDS.ImageUrl = Convert.ToString(dr["ImageUrl"]);
                                    objGDS.Description = Convert.ToString(dr["Description"]);
                                    objGDS.Rating = Convert.ToInt32(dr["Rating"]);
                                    objGDSList.Add(objGDS);

                                }
                            }
                            foreach (var HotelCodes in objGDSList)
                            {
                                foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays.Where(x => x.BasicPropertyInfo.HotelCode == HotelCodes.Hotel_Id))
                                {
                                    HotelID = item.BasicPropertyInfo.HotelCode;
                                    HotelItemAdvFirst = item;
                                    Models.PropertyModel objData = new Models.PropertyModel();
                                    objData.PropertyId = 0;
                                    objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                                    //    objData.Description = "";
                                    objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                    objData.OwnerId = OwnerID;
                                    //  objData.CityName = item.BasicPropertyInfo.Address.CityName;



                                    objData.Description = GetDescription(objGDSList, HotelID);// objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Description;
                                    objData.Image = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().ImageUrl;
                                    objData.PropertyId = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Propertyid;
                                    objData.Rating = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Rating;


                                    id = objData.PropertyId;

                                    objData.CityName = srch.Destination;
                                    CityName = srch.Destination;
                                    if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                    {
                                        objData.State = Convert.ToInt32(StateID);
                                        objData.StateName = StateName;
                                        objData.City = CityName;
                                        objData.CityId = Convert.ToInt32(CityId);

                                        PropertyCityID = Convert.ToInt32(CityId);
                                        PropertyCityName = CityName;
                                        PropertyStateID = Convert.ToInt32(StateID);
                                        PropertyStateName = StateName;
                                    }
                                    else
                                    {

                                        objData.State = GDSStateID;
                                        //   objData.StateName = "Others";
                                        objData.StateName = GDStateName;
                                        objData.City = objData.CityName;
                                        objData.CityId = GDSCityID;

                                        PropertyCityID = Convert.ToInt32(GDSCityID);
                                        PropertyCityName = objData.CityName;
                                        PropertyStateID = GDSStateID;
                                        PropertyStateName = GDStateName;
                                    }

                                    objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                                    objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                                    objData.Country = CountryID;
                                    if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                    {
                                        objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                    }
                                    else
                                    {
                                        objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                                    }
                                    PropertyCountryID = CountryID;
                                    PropertyCountryName = objData.CountryName;

                                    if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                                    {
                                        objData.Location = objData.CityName + ", " + objData.StateName;
                                        if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                        objData.Location = objData.Location + ", " + objData.CountryName;


                                    }


                                    objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                    objData.HotelId = item.BasicPropertyInfo.HotelCode;

                                    AmadeusPropertyID = objData.PropertyId;


                                    long AccomodationId = 0;
                                    string RoomStayRPH = item.RoomStayRPH;
                                    List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                    if (!string.IsNullOrEmpty(RoomStayRPH))
                                    {

                                        string[] RoomStayRPHList = RoomStayRPH.Split(' ');

                                        RoomStaysResultList = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                                        Session["Moreindicator"] = APIUtility.GetMoreIndicator(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays.MoreIndicator);


                                    }
                                    CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();

                                    AmadeusResults = SetAmadeusResult(HotelItemAdvFirst, RoomStaysResultList, CityName, AmadeusPropertyID, objData.Image, objData.Description, objData.Rating, objData.Address);

                                    AmadeusResultList.Add(AmadeusResults);
                                }
                            }


                        }
                        searchr.Results = AmadeusResultList.Where(x => x.Amount > 0).ToList();
                        if (User.Identity.IsAuthenticated)
                        {
                            long ud = 0;
                            long.TryParse(User.Identity.GetUserId(), out ud);
                            searchr.Results = SetFrequentHotelsOrder(searchr.Results, ud, srch.Destination);
                        }
                        searchr.TotalRows = searchr.Results.Count;
                        if (searchr.TotalRows > 0)
                        {
                            searchr.SessionID = Convert.ToString(Session["SessionId"]);
                            searchr.SequenceNumber = Convert.ToInt32(Session["SequenceNumber"]);
                            searchr.SecurityToken = Convert.ToString(Session["SecurityToken"]);
                            searchr.Moreindicator = Convert.ToString(Session["Moreindicator"]);
                        }

                        if (string.IsNullOrEmpty(searchr.Moreindicator))
                        {
                            Session["SessionId"] = "";
                            Session["SequenceNumber"] = "";
                            Session["SecurityToken"] = "";
                            Session["Moreindicator"] = "";
                        }
                    }

                    else
                    {
                        AmadeusResultList = new List<CLayer.SearchResult>();
                        searchr.Results = AmadeusResultList.Where(x => x.Amount > 0).ToList();
                        searchr.TotalRows = 0;
                    }


                }
                else
                {
                    searchr.Results = await FillLocations(BLayer.Property.SearchWithFilter(out totalCount, srch));
                    searchr.TotalRows = totalCount;
                }

            }
            catch (Exception ex)
            {
                LogHandler.AddLog(HotelID.ToString() + "-" + ex.Message);
                searchr = null;
            }


            LogHandler.AddLog(DateTime.Now.ToString());

            TempData["LocationList"] = LocationList;

            return searchr;
        }
        private string GetDescription(List<CLayer.GDSProperties> pList, string HotelID)
        {
            string Result = string.Empty;
            try
            {
                Result = pList.Where(x => x.Hotel_Id == HotelID).FirstOrDefault().Description;
            }
            catch (Exception ex)
            {
                Result = "";
            }
            return Result;
        }
        private async Task<SearchResults> Search(Models.SimpleSearchModel data, bool IsAmadeus = false)
        {
            CLayer.Role.Roles val = new CLayer.Role.Roles();
            SearchResults searchr = new SearchResults();
            //List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();
            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();

            try
            {
                int adult, children, staytype, bedrooms;
                adult = 0;
                int.TryParse(data.Adults, out adult);
                children = 0;
                int.TryParse(data.Children, out children);
                staytype = 0;
                int.TryParse(data.StayType, out staytype);
                bedrooms = 0;
                int.TryParse(data.Bedrooms, out bedrooms);
                int totalCount = 0;

                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                srch.Adults = adult;
                srch.Children = children;
                srch.StayType = staytype;
                srch.Bedrooms = bedrooms;
                srch.Destination = data.Destination;

                Session["GDSCountry"] = "";
                Session["GDSCountryCode"] = "";
                Session["GDSCurrencyCode"] = "";

                string pKeyword = APIUtility.GDSKeyWord;


                if (srch.Destination != null && srch.Destination != "")
                {
                    srch.Destination = Common.Utils.SecureInputString(srch.Destination); //srch.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");

                    srch.Country = APIUtility.GetGDSCountry(srch.Destination);


                    //   srch.Country = APIUtility.GetCountryName(srch.Country);
                    Session["GDSCountry"] = string.IsNullOrEmpty(srch.Country) ? "" : srch.Country;
                    // int GlobalSearch = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalSearch"].ToString());

                    if (srch.Country.ToUpper() == "INDIA")
                    {
                        srch.Country = "";
                        Session["GDSCountry"] = "";
                    }
                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(srch.Country);
                    int GDSCountryCount = (srch.Country != "") ? GDSCountryList.Count : 0;
                    if (GDSCountryCount > 0)
                    {
                        srch.Destination = APIUtility.GetGDSDestination(srch.Destination);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        string sDestination = APIUtility.GetGDSDestination(srch.Destination);
                        srch.Destination = (sDestination == "") ? srch.Destination : sDestination;
                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (srch.Country == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }


                        }

                    }
                }

                srch.CheckIn = data.CheckIn;
                srch.CheckOut = data.CheckOut;
                Session["CheckIn"] = srch.CheckIn;
                Session["CheckOut"] = srch.CheckOut;
                Session["Destination"] = srch.Destination;
                Session["Adults"] = srch.Adults;
                Session["Children"] = srch.Children;
                //srch.StayType = staytype;
                Session["Bedrooms"] = srch.Bedrooms;


                // pass source co-ordinates
                srch.Lattitude = 0;
                srch.Longitude = 0;
                srch.Features = "";
                srch.DistanceInKm = data.distanceInKm;
                srch.RangeBudgetMax = data.rangeBudgetMax;
                srch.RangeBudgetMin = data.rangeBudgetMin;
                srch.StarRatingRange = data.starRatingRange;
                srch.Location = "";


                if (data.Destination != null && data.Destination != "")
                {
                    Common.Utils.Location pos;
                    string loc = BLayer.City.GetLocation(data.Destination);
                    if (loc == "")
                    {
                        srch.Lattitude = 0;
                        srch.Longitude = 0;
                    }
                    else
                    {
                        pos = await Common.Utils.GetLocation(loc);
                        srch.Lattitude = pos.Lattitude;
                        srch.Longitude = pos.Longitude;
                    }
                }

                int rip = 0;
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get(Common.Utils.SERARCH_MAX_ROWS), out rip);
                srch.NoOfRows = rip;
                int sr = data.CurrentPage - 1;
                if (sr < 0) sr = 0;
                sr = sr * srch.NoOfRows;
                srch.StaringRow = sr;
                srch.StayTypeGroup = "";


                srch.UserType = CLayer.Role.Roles.Customer;
                if (data.OrderBy < 1 || data.OrderBy > 3)
                    srch.SortOrder = CLayer.SearchCriteria.SortBy.PriceAsc;
                else
                    srch.SortOrder = (CLayer.SearchCriteria.SortBy)data.OrderBy;


                if (User.Identity.IsAuthenticated)
                {
                    long ud = 0;
                    long.TryParse(User.Identity.GetUserId(), out ud);
                    val = BLayer.User.GetRole(ud);
                    srch.UserType = CLayer.Role.GetRoleForSearch(val);
                    if (val == CLayer.Role.Roles.Corporate)
                        srch.LoggedInUser = ud;
                    else
                    {
                        if (val == CLayer.Role.Roles.CorporateUser)
                        {
                            srch.LoggedInUser = BLayer.B2B.GetCorporateIdOfUser(ud);
                        }
                        else
                            srch.LoggedInUser = 0;
                    }
                }
                // return BLayer.Property.Search(out totalCount, data.Destination, data.CheckIn, data.CheckOut, adult, children, staytype, bedrooms, "");
                // return BLayer.Property.SearchWithFilter(out totalCount, srch);
                TempData["SearchCriteria"] = data;

                //if (IsAmadeus)
                //{
                //  List<SearchResults> AmadeusResulsts = new List<SearchResults>();

                //save keywords in country table
                CLayer.Country pCountry = new CLayer.Country();
                pCountry.KeyWords = APIUtility.GDSKeyWord;
                pCountry.CountryId = Convert.ToInt64(Session["GDSCountryID"]);

                //save keywords in country table end
                int KeyWords = BLayer.Country.GDSKeywordSave(pCountry);

                //karthikms added this code from 1775 to 1782
                //string result = TamrindMultiSingleAvailability(srch);
                //string TBOresult = TBOHotelSearch(srch);

                //DataSet lds_auth = new DataSet();
                //DataSet lds_tboauth = new DataSet();

                //lds_auth.ReadXml(GenerateStreamFromString(result));
                //lds_tboauth.ReadXml(GenerateStreamFromString(TBOresult));


                //string result = HotelMultiSingleAvailability(srch, true, "");
                //string result = client.GetHotelList(cityparam);

                #region Transaction Log 

                //APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailability, 0);

                #endregion Transaction log end

                //Serializer ser = new Serializer();

                //CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                //CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                //CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();

                //try
                //{

                //    HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                //}
                //catch (Exception ex)
                //{
                //    try
                //    {
                //        HotelResultAdv = ser.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                //    }
                //    catch (Exception ex1)
                //    {
                //        HotelResultAdvFirst = ser.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                //    }

                //}


                //long AmadeusPropertyID = 0;
                //string CityName = string.Empty;
                //var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                //var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                //var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                //var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();

                //var HotelItemAdv = new CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStaysHotelStay..OTA_HotelAvailRSHotelStay();
                //var RoomStayItemListAdv = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSRoomStaysRoomStay>();


                long StateID = 0;
                string StateName = "";
                string City = srch.Destination;
                int CountryID = 0;
                long CityId = 0;
                long OwnerID = 0;
                int GDSStateID = 0;
                int GDSCityID = 0;
                string GDStateName = "";

                OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    StateID = BLayer.State.GetStateID(srch.Destination).StateId;
                    StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                    CityId = BLayer.City.GetCityID(City).CityId;
                    CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;
                }
                else
                {
                    GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), srch.Destination);
                    GDStateName = BLayer.State.Get(GDSStateID).Name;
                    GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                    CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                }




                //#region Hotel Result
                //if (HotelResult.Body != null)
                //{

                //    //if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                //    //{
                //    //    var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                //    //    CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                //    //    objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                //    //    objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                //    //    objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                //    //    objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                //    //    Session["GDSCurrencyConversion"] = objCurrencyConversion;
                //    //}
                //    //if (Convert.ToString(Session["GDSCountry"]) == "")
                //    //{
                //    //    Session["GDSCurrencyConversion"] = null;
                //    //}



                //    //if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                //    //{

                //        //foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                //        //{
                //        //    //HotelItem = item;
                //        //    //Models.PropertyModel objData = new Models.PropertyModel();
                //        //    //objData.PropertyId = 0;
                //        //    //objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                //        //    ////    objData.Description = "";
                //        //    //objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                //        //    //objData.OwnerId = OwnerID;
                //        //    //  objData.CityName = item.BasicPropertyInfo.Address.CityName;

                //        //    //objData.CityName = srch.Destination;
                //        //    //CityName = srch.Destination;
                //        //    //if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                //        //    //{
                //        //    //    objData.State = Convert.ToInt32(StateID);
                //        //    //    objData.StateName = StateName;
                //        //    //    objData.City = CityName;
                //        //    //    objData.CityId = Convert.ToInt32(CityId);
                //        //    //}
                //        //    //else
                //        //    //{

                //        //    //    objData.State = GDSStateID;
                //        //    //    //   objData.StateName = "Others";
                //        //    //    objData.StateName = GDStateName;
                //        //    //    objData.City = objData.CityName;
                //        //    //    objData.CityId = GDSCityID;
                //        //    //}

                //        //    //objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                //        //    //objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                //        //    //objData.Country = CountryID;
                //        //    //if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                //        //    //{
                //        //    //    objData.CountryName = BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                //        //    //}
                //        //    //else
                //        //    //{
                //        //    //    objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                //        //    //}

                //        //    //if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                //        //    //{
                //        //    //    objData.Location = objData.CityName + ", " + objData.StateName;
                //        //    //    if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                //        //    //    objData.Location = objData.Location + ", " + objData.CountryName;
                //        //    //}



                //        //    //objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                //        //    //objData.HotelId = item.BasicPropertyInfo.HotelCode;

                //        //    //AmadeusPropertyID = await SaveAmadeus_Property(objData);



                //        //    //long AccomodationId = 0;
                //        //    //string RoomStayRPH = item.RoomStayRPH;
                //        //    //List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                //        //    //if (!string.IsNullOrEmpty(RoomStayRPH))
                //        //    //{
                //        //    //    ////BLayer.Accommodation.Accommodation_Amadeus_Delete(AmadeusPropertyID);
                //        //    //    //string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                //        //    //    //List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                //        //    //    //for (int i = 0; i < RoomStayRPHList.Length; i++)
                //        //    //    //{

                //        //    //    //    var RoomStays = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                //        //    //    //    RoomStayItemList = RoomStays;
                //        //    //    //    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();
                //        //    //    //    AccomodationId = 0;
                //        //    //    //    foreach (var Accitem in objAcc)
                //        //    //    //    {
                //        //    //    //        if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                //        //    //    //        {
                //        //    //    //            AccomodationId = Accitem.AccommodationId;
                //        //    //    //            break;
                //        //    //    //        }
                //        //    //    //    }

                //        //    //    //    //if (AccomodationId == 0)
                //        //    //    //    //{

                //        //    //    //    //    foreach (var RoomStayItem in RoomStays)
                //        //    //    //    //    {
                //        //    //    //    //        dataAccommodation.AccommodationId = AccomodationId;
                //        //    //    //    //        dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                //        //    //    //    //        dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                //        //    //    //    //        dataAccommodation.PropertyId = AmadeusPropertyID;
                //        //    //    //    //        dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                //        //    //    //    //        dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                //        //    //    //    //        if (RoomStayItem.RoomTypes != null)
                //        //    //    //    //        {
                //        //    //    //    //            dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                //        //    //    //    //            dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                //        //    //    //    //        }


                //        //    //    //    //        dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                //        //    //    //    //        if (RoomStayItem.RoomRates != null)
                //        //    //    //    //        {
                //        //    //    //    //            dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                //        //    //    //    //            dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                //        //    //    //    //            dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                //        //    //    //    //        }
                //        //    //    //    //        if (RoomStayItem.RatePlans != null)
                //        //    //    //    //        {
                //        //    //    //    //            dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                //        //    //    //    //        }


                //        //    //    //    //        AccommodationSave(dataAccommodation);

                //        //    //    //    //    }
                //        //    //    //    //}

                //        //    //    //}
                //        //    //    //RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList);
                //        //    //}
                //        //    //CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
                //        //    //AmadeusResults = SetAmadeusResult(HotelItem, RoomStaysResultList, CityName, AmadeusPropertyID);
                //        //    //AmadeusResultList.Add(AmadeusResults);
                //        //}

                //    }



                //    #endregion

                //CLayer.SearchResult AmadeusResults;

                //for (int i = 0; i < lds_auth.Tables["hotel"].Rows.Count; i++)
                //{
                //    AmadeusResults = new CLayer.SearchResult();
                //    AmadeusResults.PropertyId = Convert.ToInt64(lds_auth.Tables["hotel"].Rows[i]["HotelId"]);
                //    AmadeusResults.Amount = Convert.ToDecimal(lds_auth.Tables["hotel"].Rows[i]["TotalPrice"]);
                //    AmadeusResults.Title = (lds_auth.Tables["hotel"].Rows[i]["HotelName"]).ToString();
                //    AmadeusResults.Location = "";
                //    AmadeusResults.Description = (lds_auth.Tables["hotel"].Rows[i]["Desc"]).ToString();
                //    AmadeusResults.ImageFile = (lds_auth.Tables["image"].Rows[i]["image_text"]).ToString();
                //    AmadeusResults.Lattitude = "";
                //    AmadeusResults.Longitude = "";
                //    AmadeusResults.Distance = 0;
                //    AmadeusResults.StarRate = 0;
                //    AmadeusResults.City = (lds_auth.Tables["hotel"].Rows[i]["CityID"]).ToString();
                //    AmadeusResults.IsDistanceFromCity = false;
                //    AmadeusResults.IsImageExists = true;
                //    AmadeusResults.State = "";
                //    AmadeusResults.Country = "";
                //    AmadeusResults.Pincode = "";
                //    AmadeusResults.PropertyCount = lds_auth.Tables["hotels"].Rows.Count;
                //    AmadeusResults.Avail = 0;
                //    AmadeusResults.HotelID = (lds_auth.Tables["hotel"].Rows[i]["CityID"]).ToString();
                //    AmadeusResults.InventoryAPIType = 0;
                //    AmadeusResults.APIType = "";
                //    AmadeusResults.GDSCurrencyCode = Session["GDSCurrencyCode"].ToString();
                //    AmadeusResults.GDSRateConversion = 0;
                //    AmadeusResults.StarRating = (lds_auth.Tables["hotel"].Rows[i]["HotelRating"]).ToString();
                //    AmadeusResults.LocationName = "";
                //    AmadeusResults.EntitlementOrder = 1;
                //    AmadeusResults.MaximumDailyEntitlement = 34;
                //    AmadeusResultList.Add(AmadeusResults);
                //    AmadeusResults = null;
                //    //AmadeusResults = SetAmadeusResult(HotelItem, RoomStaysResultList, CityName, AmadeusPropertyID, lds_auth.Tables["images"].Rows[i]["images"], objData.Description, objData.Rating, objData.Address);
                //}
                searchr.Results = AmadeusResultList;
                if (User.Identity.IsAuthenticated)
                {
                    long ud = 0;
                    long.TryParse(User.Identity.GetUserId(), out ud);
                    searchr.Results = SetFrequentHotelsOrder(searchr.Results, ud, srch.Destination);
                }
                searchr.TotalRows = searchr.Results.Count;

            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
                searchr = null;
            }

            return searchr;
        }
        //private CLayer.SearchResult SetAmadeusResult(CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay item, List<CLayer.RoomStaysResult> RoomStaysResultList,string CityName,long PropertyId)
        //{
        //    CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
        //    try
        //    {
        //        int StateId = 0;
        //        CLayer.SearchResult objImgDesc = BLayer.Property.GetGDSImageandDesctiption(PropertyId);


        //        AmadeusResults.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower()); 
        //        AmadeusResults.Description = objImgDesc.Description==null?"": objImgDesc.Description;
        //        AmadeusResults.PropertyId = PropertyId;
        //        AmadeusResults.City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.Address.CityName.ToLower());
        //        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
        //        {
        //            StateId = BLayer.State.GetStateID(CityName).StateId;
        //            AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.State.Get(StateId).Name.ToLower());
        //            AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY).ToLower()); 
        //        }
        //        else
        //        {
        //            StateId = BLayer.State.GetStateID(CityName, Convert.ToInt64(Session["GDSCountryID"])).StateId;
        //            AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.State.Get(StateId).Name.ToLower());
        //            AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Session["GDSCountry"])).ToLower();
        //        }

        //        string AmadeusNonImage= "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);
        //        AmadeusResults.ImageFile = BLayer.Property.GetGDSHotelImage(PropertyId);
        //        if(AmadeusResults.ImageFile=="")
        //        {
        //            AmadeusResults.ImageFile = AmadeusNonImage;
        //            AmadeusResults.IsImageExists = false;
        //        }
        //        else
        //        {
        //            AmadeusResults.IsImageExists = true;
        //        }

        //        AmadeusResults.IsDistanceFromCity = false;

        //        AmadeusResults.Avail = 0;
        //        AmadeusResults.Distance = 0;
        //        AmadeusResults.StarRate = 0;
        //        AmadeusResults.Pincode =Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);



        //        if ((AmadeusResults.Lattitude == "0" && AmadeusResults.Longitude == "0") || (AmadeusResults.Lattitude == null && AmadeusResults.Longitude == null))
        //        {
        //            AmadeusResults.Location = AmadeusResults.City + ", " + AmadeusResults.State;
        //            if (AmadeusResults.Pincode != "") AmadeusResults.Location = AmadeusResults.Location + " " + AmadeusResults.Pincode;
        //            AmadeusResults.Location = AmadeusResults.Location + ", " + AmadeusResults.Country;
        //            AmadeusResults.Location = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(AmadeusResults.Location.ToLower());
        //        }

        //        AmadeusResults.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
        //        AmadeusResults.APIType = GetAPIType(AmadeusResults.InventoryAPIType);
        //        AmadeusResults.HotelID = item.BasicPropertyInfo.HotelCode;


        //        AmadeusResults.RoomStaysResultList = RoomStaysResultList;
        //        Decimal TotalRoomAmount = RoomStaysResultList.Select(c => c.AmountAfterTax).Min();

        //        AmadeusResults.Amount = TotalRoomAmount;// (Convert.ToString(Session["GDSCountry"]) == "")? TotalRoomAmount:APIUtility.GetGDSConvertedAmount(TotalRoomAmount);
        //    }
        //    catch(Exception ex)
        //    {
        //        AmadeusResults = null;
        //    }



        //    return AmadeusResults;

        //}

        //private CLayer.SearchResult SetAmadeusResult(CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay item, List<CLayer.RoomStaysResult> RoomStaysResultList, string CityName, long PropertyId)
        //{
        //    CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
        //    try
        //    {
        //        int StateId = 0;
        //        CLayer.SearchResult objImgDesc = BLayer.Property.GetGDSImageandDesctiption(PropertyId);


        //        AmadeusResults.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
        //        AmadeusResults.Description = objImgDesc.Description == null ? "" : objImgDesc.Description;
        //        AmadeusResults.PropertyId = PropertyId;
        //        AmadeusResults.City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.Address.CityName.ToLower());
        //        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
        //        {
        //            StateId = BLayer.State.GetStateID(CityName).StateId;
        //            AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.State.Get(StateId).Name.ToLower());
        //            AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY).ToLower());
        //        }
        //        else
        //        {
        //            StateId = BLayer.State.GetStateID(CityName, Convert.ToInt64(Session["GDSCountryID"])).StateId;
        //            AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.State.Get(StateId).Name.ToLower());
        //            AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Session["GDSCountry"])).ToLower();
        //        }

        //        string AmadeusNonImage = "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);
        //        AmadeusResults.ImageFile = BLayer.Property.GetGDSHotelImage(PropertyId);
        //        if (AmadeusResults.ImageFile == "")
        //        {
        //            AmadeusResults.ImageFile = AmadeusNonImage;
        //            AmadeusResults.IsImageExists = false;
        //        }
        //        else
        //        {
        //            AmadeusResults.IsImageExists = true;
        //        }

        //        AmadeusResults.IsDistanceFromCity = false;

        //        AmadeusResults.Avail = 0;
        //        AmadeusResults.Distance = 0;
        //        AmadeusResults.StarRate = 0;
        //        AmadeusResults.Pincode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);



        //        if ((AmadeusResults.Lattitude == "0" && AmadeusResults.Longitude == "0") || (AmadeusResults.Lattitude == null && AmadeusResults.Longitude == null))
        //        {
        //            AmadeusResults.Location = AmadeusResults.City + ", " + AmadeusResults.State;
        //            if (AmadeusResults.Pincode != "") AmadeusResults.Location = AmadeusResults.Location + " " + AmadeusResults.Pincode;
        //            AmadeusResults.Location = AmadeusResults.Location + ", " + AmadeusResults.Country;
        //            AmadeusResults.Location = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(AmadeusResults.Location.ToLower());
        //        }

        //        AmadeusResults.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
        //        AmadeusResults.APIType = GetAPIType(AmadeusResults.InventoryAPIType);
        //        AmadeusResults.HotelID = item.BasicPropertyInfo.HotelCode;


        //        AmadeusResults.RoomStaysResultList = RoomStaysResultList;
        //        Decimal TotalRoomAmount = RoomStaysResultList.Select(c => c.AmountAfterTax).Min();

        //        AmadeusResults.Amount = TotalRoomAmount;// (Convert.ToString(Session["GDSCountry"]) == "")? TotalRoomAmount:APIUtility.GetGDSConvertedAmount(TotalRoomAmount);
        //    }
        //    catch (Exception ex)
        //    {
        //        AmadeusResults = null;
        //    }



        //    return AmadeusResults;

        //}


        public CLayer.SearchResult SetAmadeusResult(CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay item, List<CLayer.RoomStaysResult> RoomStaysResultList, string CityName, long PropertyId, string ImageUrl = "", string Description = "", int Rating = 0, string Address = "")
        {
            CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
            try
            {
                int StateId = 0;
                // CLayer.SearchResult objImgDesc = BLayer.Property.GetGDSImageandDesctiption(PropertyId);

                AmadeusResults.GDSCurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                AmadeusResults.GDSRateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                AmadeusResults.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                int idx = Description.IndexOf(CLayer.ObjectStatus.DESCRIPTION_SEPERATOR);
                if (idx > -1)
                {
                    Description = Description.Substring(0, idx);
                }
                Description = (Description.Length > 130) ? Description.Substring(0, 129) : Description;

                AmadeusResults.Description = (Description == null || Description == "") ? "" : APIUtility.StripHTML(Description).Replace("<br/>", " ").Replace(CLayer.ObjectStatus.DESCRIPTION_SEPERATOR, " ").Replace("<", " ").Replace("/>", " ").Replace(">", " ").Replace("<br clear='All'/>", " ");//.Replace("#2", " ").Replace("#"," ").Replace("<br clear='All'/>", " "); ;

                AmadeusResults.PropertyId = PropertyId;
                AmadeusResults.City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.Address.CityName.ToLower());
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    // StateId = BLayer.State.GetStateID(CityName).StateId;
                    StateId = PropertyStateID;
                    AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyStateName.ToLower());
                    AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyCountryName.ToLower());
                }
                else
                {
                    StateId = PropertyStateID; //BLayer.State.GetStateID(CityName, Convert.ToInt64(Session["GDSCountryID"])).StateId;
                    AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyStateName.ToLower());
                    AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Session["GDSCountry"])).ToLower();
                }

                string AmadeusNonImage = PropertyNoImage;// "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);
                AmadeusResults.ImageFile = ImageUrl; //BLayer.Property.GetGDSHotelImage(PropertyId);
                if (AmadeusResults.ImageFile == "")
                {
                    AmadeusResults.ImageFile = AmadeusNonImage;
                    AmadeusResults.IsImageExists = false;
                }
                else
                {
                    AmadeusResults.IsImageExists = true;
                }

                AmadeusResults.IsDistanceFromCity = false;

                AmadeusResults.Avail = 0;
                AmadeusResults.Distance = 0;
                AmadeusResults.StarRate = 0;
                AmadeusResults.Pincode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);



                if ((AmadeusResults.Lattitude == "0" && AmadeusResults.Longitude == "0") || (AmadeusResults.Lattitude == null && AmadeusResults.Longitude == null))
                {
                    AmadeusResults.Location = AmadeusResults.City + ", " + AmadeusResults.State;
                    if (!string.IsNullOrEmpty(AmadeusResults.Pincode) || AmadeusResults.Pincode != "" || AmadeusResults.Pincode != "N/A" || AmadeusResults.Pincode != "undefined") AmadeusResults.Location = AmadeusResults.Location + " " + AmadeusResults.Pincode;
                    AmadeusResults.Location = AmadeusResults.Location + ", " + AmadeusResults.Country;

                    AmadeusResults.Location = APIUtility.ReplaceFirstOccurrence(AmadeusResults.Location, CityName, "");
                    AmadeusResults.Location = APIUtility.ReplaceFirstOccurrence(AmadeusResults.Location, AmadeusResults.Country, "").Replace(",,", ",");

                    AmadeusResults.Location = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(AmadeusResults.Location.ToLower());
                }
                AmadeusResults.LocationName = GetLocationName(Address);
                LocationList.Add(AmadeusResults.LocationName);

                AmadeusResults.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                AmadeusResults.APIType = GetAPIType(AmadeusResults.InventoryAPIType);
                AmadeusResults.HotelID = item.BasicPropertyInfo.HotelCode;


                AmadeusResults.RoomStaysResultList = RoomStaysResultList;
                //  Decimal TotalRoomAmount = RoomStaysResultList[0].MinAmountAfterTax; //RoomStaysResultList.Select(c => c.AmountAfterTax).Min();

                Decimal AmountMin = 0;
                Decimal MinAmountAfterTax = 0;
                for (int i = 0; i < RoomStaysResultList.Count; i++)
                {
                    if (i == 0)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                    if (RoomStaysResultList[i].AmountAfterTax < AmountMin)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                }
                Decimal TotalRoomAmount = MinAmountAfterTax;

                AmadeusResults.Amount = TotalRoomAmount;// (Convert.ToString(Session["GDSCountry"]) == "")? TotalRoomAmount:APIUtility.GetGDSConvertedAmount(TotalRoomAmount);
                AmadeusResults.StarRate = Rating;
                if (Rating > 0)
                {
                    AmadeusResults.StarRating = Common.Utils.GetStarRating(Rating);
                }

            }
            catch (Exception ex)
            {
                AmadeusResults = null;
            }



            return AmadeusResults;

        }

        public CLayer.SearchResult SetAmadeusResult(CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay item, List<CLayer.RoomStaysResult> RoomStaysResultList, string CityName, long PropertyId, string ImageUrl = "", string Description = "", int Rating = 0, string Address = "")
        {
            CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
            try
            {
                int StateId = 0;
                //     CLayer.SearchResult objImgDesc = BLayer.Property.GetGDSImageandDesctiption(PropertyId);
                AmadeusResults.GDSCurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                AmadeusResults.GDSRateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);

                AmadeusResults.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                int idx = Description.IndexOf(CLayer.ObjectStatus.DESCRIPTION_SEPERATOR);
                if (idx > -1)
                {
                    Description = Description.Substring(0, idx);
                }
                Description = (Description.Length > 130) ? Description.Substring(0, 129) : Description;

                AmadeusResults.Description = (Description == null || Description == "") ? "" : APIUtility.StripHTML(Description).Replace("<br/>", " ").Replace(CLayer.ObjectStatus.DESCRIPTION_SEPERATOR, " ").Replace("<", " ").Replace("/>", " ").Replace(">", " ").Replace("<br clear='All'/>", " ");//.Replace("#2", " ").Replace("#"," ").Replace("<br clear='All'/>", " "); ;
                                                                                                                                                                                                                                                                                                        //    AmadeusResults.Description = RemoveInvalidCharacters(AmadeusResults.Description);

                AmadeusResults.PropertyId = PropertyId;
                AmadeusResults.City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.Address.CityName.ToLower());
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    // StateId = BLayer.State.GetStateID(CityName).StateId;
                    StateId = PropertyStateID;
                    AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyStateName.ToLower());
                    AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyCountryName.ToLower());
                }
                else
                {
                    StateId = PropertyStateID; //BLayer.State.GetStateID(CityName, Convert.ToInt64(Session["GDSCountryID"])).StateId;
                    AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyStateName.ToLower());
                    AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Session["GDSCountry"])).ToLower();
                }

                string AmadeusNonImage = PropertyNoImage;// "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);
                AmadeusResults.ImageFile = ImageUrl;//BLayer.Property.GetGDSHotelImage(PropertyId);
                if (AmadeusResults.ImageFile == "")
                {
                    AmadeusResults.ImageFile = AmadeusNonImage;
                    AmadeusResults.IsImageExists = false;
                }
                else
                {
                    AmadeusResults.IsImageExists = true;
                }

                AmadeusResults.IsDistanceFromCity = false;

                AmadeusResults.Avail = 0;
                AmadeusResults.Distance = 0;
                AmadeusResults.StarRate = 0;
                AmadeusResults.Pincode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);



                if ((AmadeusResults.Lattitude == "0" && AmadeusResults.Longitude == "0") || (AmadeusResults.Lattitude == null && AmadeusResults.Longitude == null))
                {
                    AmadeusResults.Location = AmadeusResults.City + ", " + AmadeusResults.State;
                    if (!string.IsNullOrEmpty(AmadeusResults.Pincode) || AmadeusResults.Pincode != "" || AmadeusResults.Pincode != "N/A" || AmadeusResults.Pincode != "undefined") AmadeusResults.Location = AmadeusResults.Location + " " + AmadeusResults.Pincode;
                    AmadeusResults.Location = AmadeusResults.Location + ", " + AmadeusResults.Country;

                    AmadeusResults.Location = APIUtility.ReplaceFirstOccurrence(AmadeusResults.Location, CityName, "");
                    AmadeusResults.Location = APIUtility.ReplaceFirstOccurrence(AmadeusResults.Location, AmadeusResults.Country, "").Replace(",,", ",");

                    AmadeusResults.Location = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(AmadeusResults.Location.ToLower());
                }

                AmadeusResults.LocationName = GetLocationName(Address);
                LocationList.Add(AmadeusResults.LocationName);

                AmadeusResults.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                AmadeusResults.APIType = GetAPIType(AmadeusResults.InventoryAPIType);
                AmadeusResults.HotelID = item.BasicPropertyInfo.HotelCode;


                AmadeusResults.RoomStaysResultList = RoomStaysResultList;
                //   Decimal TotalRoomAmount = RoomStaysResultList[0].MinAmountAfterTax; //RoomStaysResultList.Select(c => c.AmountAfterTax).Min();

                Decimal AmountMin = 0;
                Decimal MinAmountAfterTax = 0;
                for (int i = 0; i < RoomStaysResultList.Count; i++)
                {
                    if (i == 0)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                    if (RoomStaysResultList[i].AmountAfterTax < AmountMin)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                }
                Decimal TotalRoomAmount = MinAmountAfterTax;

                AmadeusResults.Amount = TotalRoomAmount;// (Convert.ToString(Session["GDSCountry"]) == "")? TotalRoomAmount:APIUtility.GetGDSConvertedAmount(TotalRoomAmount);
                AmadeusResults.StarRate = Rating;
                if (Rating > 0)
                {
                    AmadeusResults.StarRating = Common.Utils.GetStarRating(Rating);
                }

            }
            catch (Exception ex)
            {
                AmadeusResults = null;
            }



            return AmadeusResults;

        }
        public bool IsPropertyImageExists(string Path, long PropertyID, string ImgFile)
        {
            bool result = false;
            Path = Server.MapPath("~/Files/Property/" + PropertyID + "/" + ImgFile + "");
            //  Path = Server.MapPath("~/Files/Property/439/43902012015143859OmServicedApartmentsBandra_bandra6_Listing.jpg");

            result = System.IO.File.Exists(Path) ? true : false;


            return result;
        }
        public string GetLocationName(string pValue)
        {
            string result = string.Empty;
            string[] resultList = pValue.Split(',');
            try
            {
                if (resultList.Length > 0)
                {
                    result = ToTitleCase(resultList[resultList.Length - 1]);
                }
                else
                {
                    result = ToTitleCase(pValue);
                }

            }
            catch (Exception ex)
            {

            }
            return result.Trim();
        }
        public string GetAPIType(int pValue)
        {
            string result = string.Empty;
            switch (pValue)
            {
                case 1:
                    result = "maxmojo";
                    break;
                case 2:
                    result = "amadeus";
                    break;
                default:
                    result = "sb";
                    break;
            }
            return result;
        }
        public static List<CLayer.RoomStaysResult> GetRoomStays(CLayer.HotelAvailability.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
            Decimal AmountMin = 0;
            for (int i = 0; i < pRPH.Length; i++)
            {
                var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                if (RoomStayItem[0].RoomRates != null)
                {

                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Rates.Rate.Base.AmountBeforeTax);
                    objRoomStay.AmountBeforeTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax);

                    objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                    objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                    // objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                    objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;
                    //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    //{
                    //    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;

                    if (objRoomStay.CurrencyCode != "INR")
                    {
                        if (RequestedCurrencyCode != objRoomStay.CurrencyCode)
                        {
                            if (CurrencyConversionList != null)
                            {
                                ConversionRate = CurrencyConversionList.Where(x => x.SourceCurrencyCode == objRoomStay.CurrencyCode).FirstOrDefault().RateConversion;
                            }
                        }
                    }



                    if (RequestedCurrencyCode != objRoomStay.CurrencyCode)
                    {
                        objRoomStay.AmountAfterTax = Math.Round(objRoomStay.AmountAfterTax * ConversionRate, 0);

                    }
                    else
                    {
                        objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                    }
                    //}
                    if (i == 0)
                    {
                        AmountMin = objRoomStay.AmountAfterTax;
                        objRoomStay.MinAmountAfterTax = AmountMin;
                    }
                    if (objRoomStay.AmountAfterTax < AmountMin)
                    {
                        AmountMin = objRoomStay.AmountAfterTax;
                        objRoomStay.MinAmountAfterTax = AmountMin;
                    }


                    CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                    ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

                    ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
                    objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;


                    objRoomStay.RoomTypeCode = RoomStayItem[0].RoomRates.RoomRate.RoomTypeCode;
                    objRoomStay.RatePlanCode = RoomStayItem[0].RoomRates.RoomRate.RatePlanCode;


                    //CLayer.RoomRateTotal ObjRoomRateTotal = new CLayer.RoomRateTotal();
                    //List<CLayer.RoomRateTotalTaxes> ObjRoomRateTotalTaxesList = new List<CLayer.RoomRateTotalTaxes>();
                    //CLayer.RoomRateTotalTaxes ObjRoomRateTotalTaxes = new CLayer.RoomRateTotalTaxes();
                    //ObjRoomRateTotalTaxes.Type= RoomStayItem[0].RoomRates.RoomRate[-

                }

                if (RoomStayItem[0].RatePlans != null)
                {
                    objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
                }
                if (RoomStayItem[0].RoomTypes != null)
                {
                    objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
                    // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
                }




                RoomStaysResultList.Add(objRoomStay);

            }
            return RoomStaysResultList;
        }
        //public static List<CLayer.RoomStaysResult> GetRoomStays(CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0)
        //{
        //    List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();

        //    for (int i = 0; i < pRPH.Length; i++)
        //    {
        //        var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
        //        CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
        //        if (RoomStayItem[0].RoomRates != null)
        //        {

        //            objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Total.AmountBeforeTax);
        //            objRoomStay.AmountBeforeTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax);

        //            objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
        //            objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
        //             objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;
        //            //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
        //            //{
        //            //    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
        //            //    CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
        //            //    objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
        //            //    objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
        //            //    objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
        //            //    objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
        //            // // Session["GDSCurrencyConversion"] = objCurrencyConversion;

        //            //}
        //            objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);



        //            CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
        //            ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

        //            ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
        //            objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;


        //            objRoomStay.RoomTypeCode = RoomStayItem[0].RoomRates.RoomRate.RoomTypeCode;
        //            objRoomStay.RatePlanCode = RoomStayItem[0].RoomRates.RoomRate.RatePlanCode;



        //            //CLayer.RoomRateTotal ObjRoomRateTotal = new CLayer.RoomRateTotal();
        //            //List<CLayer.RoomRateTotalTaxes> ObjRoomRateTotalTaxesList = new List<CLayer.RoomRateTotalTaxes>();
        //            //CLayer.RoomRateTotalTaxes ObjRoomRateTotalTaxes = new CLayer.RoomRateTotalTaxes();
        //            //ObjRoomRateTotalTaxes.Type= RoomStayItem[0].RoomRates.RoomRate[-

        //        }

        //        if (RoomStayItem[0].RatePlans != null)
        //        {
        //            objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
        //        }
        //        if (RoomStayItem[0].RoomTypes != null)
        //        {
        //            objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
        //            // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
        //        }




        //        RoomStaysResultList.Add(objRoomStay);

        //    }
        //    return RoomStaysResultList;
        //}

        public static List<CLayer.RoomStaysResult> GetRoomStaysAdv(CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
            Decimal AmountMin = 0;

            for (int i = 0; i < pRPH.Length; i++)
            {
                var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                if (RoomStayItem[0].RoomRates != null)
                {

                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Total.AmountBeforeTax);
                    objRoomStay.AmountBeforeTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax);

                    objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                    objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                    objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;

                    if (objRoomStay.CurrencyCode != "INR")
                    {
                        if (RequestedCurrencyCode != objRoomStay.CurrencyCode)
                        {
                            if (CurrencyConversionList != null)
                            {
                                ConversionRate = CurrencyConversionList.Where(x => x.SourceCurrencyCode == objRoomStay.CurrencyCode).FirstOrDefault().RateConversion;
                            }
                        }
                    }


                    if (RequestedCurrencyCode != objRoomStay.CurrencyCode)
                    {
                        objRoomStay.AmountAfterTax = Math.Round(objRoomStay.AmountAfterTax * ConversionRate, 0);
                    }
                    else
                    {
                        objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                    }

                    if (i == 0)
                    {
                        AmountMin = objRoomStay.AmountAfterTax;
                        objRoomStay.MinAmountAfterTax = AmountMin;
                    }
                    if (objRoomStay.AmountAfterTax < AmountMin)
                    {
                        AmountMin = objRoomStay.AmountAfterTax;
                        objRoomStay.MinAmountAfterTax = AmountMin;
                    }


                    CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                    ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

                    ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
                    objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;


                    objRoomStay.RoomTypeCode = RoomStayItem[0].RoomRates.RoomRate.RoomTypeCode;
                    objRoomStay.RatePlanCode = RoomStayItem[0].RoomRates.RoomRate.RatePlanCode;



                    //CLayer.RoomRateTotal ObjRoomRateTotal = new CLayer.RoomRateTotal();
                    //List<CLayer.RoomRateTotalTaxes> ObjRoomRateTotalTaxesList = new List<CLayer.RoomRateTotalTaxes>();
                    //CLayer.RoomRateTotalTaxes ObjRoomRateTotalTaxes = new CLayer.RoomRateTotalTaxes();
                    //ObjRoomRateTotalTaxes.Type= RoomStayItem[0].RoomRates.RoomRate[-

                }

                if (RoomStayItem[0].RatePlans != null)
                {
                    objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
                }
                if (RoomStayItem[0].RoomTypes != null)
                {
                    objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
                    // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
                }




                RoomStaysResultList.Add(objRoomStay);

            }
            return RoomStaysResultList;
        }
        public void AccommodationSaveByAPI(Models.AccommodationModel data)
        {
            try
            {

                string userid = (User == null) ? "0" : User.Identity.GetUserId();
                long id = 0;
                long.TryParse(userid, out id);
                //if (ModelState.IsValid)
                //{
                CLayer.Accommodation accmdtn = new CLayer.Accommodation()
                {
                    AccommodationId = data.AccommodationId,
                    PropertyId = data.PropertyId,
                    AccommodationTypeId = data.AccommodationTypeId,
                    StayCategoryId = data.StayCategoryId,
                    AccommodationCount = 1,
                    Description = data.Description,
                    //Location = data.Location,
                    MaxNoOfPeople = 0,
                    MaxNoOfChildren = 0,
                    MinNoOfPeople = 0,
                    BedRooms = 0,
                    Area = 0,
                    Status = 1,
                    TotalAccommodations = 1,
                    UpdatedById = id
                };
                if (accmdtn.MaxNoOfPeople < accmdtn.MinNoOfPeople) accmdtn.MaxNoOfPeople = accmdtn.MinNoOfPeople;
                if (accmdtn.MaxNoOfPeople < accmdtn.MaxNoOfChildren) accmdtn.MaxNoOfPeople = accmdtn.MaxNoOfChildren;
                long accId = BLayer.Accommodation.Save_API(accmdtn);

                CLayer.Inventory inv = new CLayer.Inventory()
                {
                    AccommodationId = accId,
                    Quantity = 2,
                    FromDate = data.CheckIn,
                    ToDate = data.CheckOut
                };
                BLayer.Inventory.APIsave(inv);
                DateTime enddate = (data.CheckIn).AddDays(20);
                CLayer.Rates rate = new CLayer.Rates()
                {
                    AccommodationId = accId,
                    RateFor = 7,
                    DailyRate = data.Amount,
                    WeeklyRate = 0,
                    LongTermRate = 0,
                    MonthlyRate = 0,
                    GuestRate = 0,
                    StartDate = data.CheckIn,
                    EndDate = enddate,
                    Status = 1,
                    UpdatedBy = id,
                    SellRateAfterTax = data.AmountWithTax
                };
                BLayer.Rate.APISave(rate);


                if (data.RoomId == null) { data.RoomId = ""; }

                //  BLayer.Accommodation.SetRoomId(data.AccommodationId, data.RoomId.Trim());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
        }
        public void AccommodationSave(Models.AccommodationModel data)
        {
            try
            {

                string userid = (User == null) ? "0" : User.Identity.GetUserId();
                long id = 0;
                long.TryParse(userid, out id);
                //if (ModelState.IsValid)
                //{
                CLayer.Accommodation accmdtn = new CLayer.Accommodation()
                {
                    AccommodationId = data.AccommodationId,
                    PropertyId = data.PropertyId,
                    AccommodationTypeId = data.AccommodationTypeId,
                    StayCategoryId = data.StayCategoryId,
                    AccommodationCount = data.AccommodationCount,
                    Description = data.Description,
                    //Location = data.Location,
                    MaxNoOfPeople = data.MaxNoOfPeople,
                    MaxNoOfChildren = data.MaxNoOfChildren,
                    MinNoOfPeople = data.MinNoOfPeople,
                    BedRooms = data.BedRooms,
                    Area = data.Area,
                    Status = data.Status,
                    TotalAccommodations = data.TotalAccommodations,
                    UpdatedById = id,
                    RoomType = data.RoomType,
                    RoomTypeCode = data.RoomTypeCode,
                    SourceofBusiness = data.SourceofBusiness,
                    BookingCode = data.BookingCode,
                    RatePlanCode = data.RatePlanCode,
                    RoomStayRPH = data.RoomStayRPH

                };
                if (accmdtn.MaxNoOfPeople < accmdtn.MinNoOfPeople) accmdtn.MaxNoOfPeople = accmdtn.MinNoOfPeople;
                if (accmdtn.MaxNoOfPeople < accmdtn.MaxNoOfChildren) accmdtn.MaxNoOfPeople = accmdtn.MaxNoOfChildren;
                long accId = BLayer.Accommodation.Save_Amadeus(accmdtn);
                if (data.RoomId == null) { data.RoomId = ""; }

                //  BLayer.Accommodation.SetRoomId(data.AccommodationId, data.RoomId.Trim());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
        }
        private async Task<List<CLayer.SearchResult>> FillLocations(List<CLayer.SearchResult> data)
        {
            if (data == null || data.Count == 0) return data;
            string location;
            foreach (CLayer.SearchResult sr in data)
            {
                if (sr.Lattitude == "0" && sr.Longitude == "0")
                {
                    location = sr.City + ", " + sr.State;
                    if (sr.Pincode != "") location = location + " " + sr.Pincode;
                    location = location + ", " + sr.Country;
                    Common.Utils.Location pos;

                    pos = await Common.Utils.GetLocation(location);
                    sr.Lattitude = pos.Lattitude.ToString();
                    sr.Longitude = pos.Longitude.ToString();
                }
            }

            return data;
        }
        private async Task<SearchResults> SearchFilter(Models.SimpleSearchModel data)
        {
            if (data.Location == null) { data.Location = ""; }
            SearchResults searchr = new SearchResults();
            try
            {
                CLayer.SearchCriteria cr = new CLayer.SearchCriteria();
                int temp;
                temp = 0;
                //   int.TryParse(data.HidAdults, out temp);
                int.TryParse(data.Adults, out temp);
                cr.Adults = temp;
                temp = 0;
                // int.TryParse(data.HidChildren, out temp);
                int.TryParse(data.Children, out temp);
                cr.Children = temp;
                temp = 0;
                int.TryParse(data.HidStayType, out temp);
                cr.StayType = temp;
                temp = 0;
                //int.TryParse(data.HidBedrooms, out temp);
                int.TryParse(data.Bedrooms, out temp);
                cr.Bedrooms = temp;
                int totalCount = 0;

                cr.Destination = data.Destination;
                //   cr.Destination = data.HiddenDestination;

                if (cr.Destination != null && cr.Destination != "")
                {
                    cr.Destination = Common.Utils.SecureInputString(cr.Destination); // cr.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");
                }
                //cr.CheckOut = data.HiddenCheckOut;
                //cr.CheckIn = data.HiddenCheckIn;

                cr.CheckOut = data.CheckOut;
                cr.CheckIn = data.CheckIn;



                cr.Lattitude = 0;
                cr.Longitude = 0;
                if (data.beds > 0) cr.Bedrooms = data.beds;
                cr.RangeBudgetMax = data.rangeBudgetMax;
                cr.RangeBudgetMin = data.rangeBudgetMin;
                cr.DistanceInKm = data.distanceInKm;
                cr.Features = data.features;
                cr.StarRatingRange = data.starRatingRange;
                string loc = "";
                if (data.Location != null && data.Location != "")
                {
                    loc = Common.Utils.SecureInputString(data.Location);

                    if (cr.Destination != null && cr.Destination != "")
                    {
                        loc = BLayer.City.GetLocation(loc);
                        if (loc == "")
                        {
                            loc = BLayer.City.GetLocation(data.Destination);
                            //if (loc == "") ...
                            //loc = data.Location + ", " + loc;
                        }
                    }
                }
                else
                {
                    //data.HiddenDestination = Common.Utils.SecureInputString(data.HiddenDestination);
                    //loc = BLayer.City.GetLocation(data.HiddenDestination);
                    data.HiddenDestination = Common.Utils.SecureInputString(data.Destination);
                    loc = BLayer.City.GetLocation(data.Destination);
                }
                if (loc == "")
                {
                    cr.Lattitude = 0;
                    cr.Longitude = 0;
                }
                else
                {
                    Common.Utils.Location pos;
                    pos = await Common.Utils.GetLocation(loc);
                    cr.Lattitude = pos.Lattitude;
                    cr.Longitude = pos.Longitude;
                }
                int rip = 0;
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get(Common.Utils.SERARCH_MAX_ROWS), out rip);
                cr.NoOfRows = rip;

                if (data.features != null && data.features != "")
                {
                    cr.Features = Common.Utils.CSVNumericValidation(data.features);
                }
                int sr = data.CurrentPage - 1;
                if (sr < 0) sr = 0;
                sr = sr * cr.NoOfRows;
                cr.StaringRow = sr;
                cr.SortOrder = (CLayer.SearchCriteria.SortBy)data.OrderBy;
                cr.Location = data.Location;
                if (cr.Location != null && cr.Location != "")
                {
                    cr.Location = cr.Location.Replace("'", "").Replace(";", "").Replace("\"", "");
                }
                cr.UserType = CLayer.Role.Roles.Customer;
                cr.LoggedInUser = 0;
                if (User.Identity.IsAuthenticated)
                {
                    long ud = 0;
                    long.TryParse(User.Identity.GetUserId(), out ud);
                    CLayer.Role.Roles val = BLayer.User.GetRole(ud);
                    cr.UserType = CLayer.Role.GetRoleForSearch(val);
                    if (val == CLayer.Role.Roles.Corporate)
                        cr.LoggedInUser = ud;
                    else
                    {
                        if (val == CLayer.Role.Roles.CorporateUser)
                        {
                            cr.LoggedInUser = BLayer.B2B.GetCorporateIdOfUser(ud);
                        }
                        else
                            cr.LoggedInUser = 0;
                    }
                }

                searchr.Results = await FillLocations(BLayer.Property.SearchWithFilter(out totalCount, cr));
                //  searchr.TotalRows = totalCount;
                ViewBag.Criteria = cr;
            }
            catch (Exception ex)
            {
                searchr = null;
                throw ex;
            }

            return searchr;
        }
        //        public static string HotelMultiSingleAvailability(CLayer.SearchCriteria sch, bool Stateless = false, string HotelCode = "", string XMLtext = "")
        //        {
        //            string soapresult = string.Empty;
        //            try
        //            {
        //                string staybazarwbsoffice = BLayer.settings.getvalue(CLayer.settings.staybazarwbsoffice);

        //                var _url = blayer.settings.getvalue(CLayer.settings.staybazarwbsurl); //"https://noded1.test.webservices.amadeus.com/1asiwstyszru";
        //                var _action = blayer.settings.getvalue(CLayer.settings.staybazarwbshotelmultisingleaction); //"http://webservices.amadeus.com/hotel_multisingleavailability_10.0";

        //                string soapheader = string.empty;
        //                string soapbody = string.empty;

        //                if (!string.isnullorempty(sch.moreindicator))
        //                {
        //                    soapheader = apiutility.setstatefulsoapheader(_action, sch.sessionid, sch.sequencenumber, sch.securitytoken, sch.moreindicator);
        //                }
        //                else
        //                {
        //                    soapheader = apiutility.setsoapheader(_url, _action, stateless);
        //                }


        //                if (hotelcode != "")
        //                {
        //                    soapbody = apiutility.sethotelmultisingleavailabilitybody(sch, hotelcode);
        //                }
        //                else
        //                {
        //                    if (!string.isnullorempty(sch.moreindicator))
        //                    {
        //                        soapbody = apiutility.sethotelmultisingleavailabilitybodymore(sch);
        //                    }
        //                    else
        //                    {
        //                        soapbody = apiutility.sethotelmultisingleavailabilitybody(sch);
        //                    }

        //                }


        //                string soapenvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        //                soapenvelop = soapenvelop + "<soapenv:envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/types_v1\" xmlns:iat=\"http://www.iata.org/iata/2007/00/iata2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/appmdw_commontypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/session_v3\" xmlns:ns=\"http://www.opentravel.org/ota/2003/05\">";
        //                soapenvelop = soapenvelop + soapheader + soapbody;
        //                soapenvelop = soapenvelop + "</soapenv:envelope>";

        //                xmldocument soapenvelopexml = new xmldocument();


        //                soapenvelopexml.loadxml(soapenvelop);

        //                system.web.httpcontext.current.session["gdsrequest"] = convert.tostring(soapenvelopexml.innerxml);
        //                convert.tostring(soapenvelopexml.innerxml);

        //                try
        //                {
        //                    httpwebrequest webrequest = createwebrequest(_url, _action);
        //                    webrequest.proxy = null;
        //                    webrequest.automaticdecompression = decompressionmethods.deflate | decompressionmethods.gzip;
        //                    webrequest = insertsoapenvelopeintowebrequest(soapenvelopexml, webrequest);
        //                    webrequest.proxy = null;
        //#if !debug
        //                    webrequest.timeout = 45000;
        //#endif
        //                    begin async call to web request.
        //                    iasyncresult asyncresult = webrequest.begingetresponse(null, null);

        //                    suspend this thread until call is complete.you might want to
        //                     do something usefull here like update your ui.
        //                    asyncresult.asyncwaithandle.waitone();


        //                    get the response from the completed web request.
        //                         webrequest.proxy = null;
        //                    using (webresponse webresponse = webrequest.endgetresponse(asyncresult))
        //                    {
        //                        using (streamreader rd = new streamreader(webresponse.getresponsestream()))
        //                        {
        //                            soapresult = rd.readtoend();
        //                        }
        //                        console.write(soapresult);
        //                    }
        //                }
        //                catch (webexception webex)
        //                {

        //                    webresponse errresp = webex.response;
        //                    using (stream respstream = errresp.getresponsestream())
        //                    {
        //                        streamreader reader = new streamreader(respstream);
        //                        string text = reader.readtoend();
        //                    }

        //                    try
        //                    {
        //                        httpwebrequest webrequest = createwebrequest(_url, _action);
        //                        webrequest.proxy = null;
        //                        webrequest.automaticdecompression = decompressionmethods.deflate | decompressionmethods.gzip;
        //                        webrequest = insertsoapenvelopeintowebrequest(soapenvelopexml, webrequest);
        //                        webrequest.proxy = null;

        //                        webrequest.timeout = 45000;


        //                        begin async call to web request.
        //                        iasyncresult asyncresult = webrequest.begingetresponse(null, null);

        //                        suspend this thread until call is complete.you might want to
        //                         do something usefull here like update your ui.
        //                        asyncresult.asyncwaithandle.waitone();


        //                        get the response from the completed web request.

        //                        using (webresponse webresponse = webrequest.endgetresponse(asyncresult))
        //                        {
        //                            using (streamreader rd = new streamreader(webresponse.getresponsestream()))
        //                            {
        //                                soapresult = rd.readtoend();
        //                            }

        //                            console.write(soapresult);
        //                        }
        //                    }
        //                    catch (exception ex)
        //                    {
        //                        loghandler.handleerror(ex);
        //                    }

        //                }

        //            }
        //            catch (webexception webex)
        //            {

        //                webresponse errresp = webex.response;
        //                using (stream respstream = errresp.getresponsestream())
        //                {
        //                    streamreader reader = new streamreader(respstream);
        //                    string text = reader.readtoend();
        //                    soapresult = text;

        //                }
        //            }

        //            return soapresult;
        //        }
        public static string HotelMultiSingleAvailability(CLayer.SearchCriteria sch, bool Stateless = false, string HotelCode = "", string XMLtext = "")
        {
            string soapResult = string.Empty;
            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/Hotel_MultiSingleAvailability_10.0";

                string SoapHeader = string.Empty;
                string SoapBody = string.Empty;

                SoapHeader = APIUtility.SetSoapHeader(_url, _action);
                SoapBody = APIUtility.SetHotelMultiSingleAvailabilityBody(sch);

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ibas=\"http://www.tamarindtours.in/IBaseDetails\"  xmlns:tam=\"http://schemas.datacontract.org/2004/07/TamarindXML.BusinessClass\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";

                XmlDocument soapEnvelopeXml = new XmlDocument();

                soapEnvelopeXml.LoadXml(SoapEnvelop);



                System.Web.HttpContext.Current.Session["GDSRequest"] = Convert.ToString(soapEnvelopeXml.InnerXml);
                // Convert.ToString(soapEnvelopeXml.InnerXml);

                try
                {
                    HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                    webRequest.Proxy = null;
                    webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                    webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
                    //   webRequest.Proxy = null;
#if !DEBUG
                    webRequest.Timeout = 45000;
#endif
                    // begin async call to web request.
                    IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                    // suspend this thread until call is complete. You might want to
                    // do something usefull here like update your UI.
                    asyncResult.AsyncWaitHandle.WaitOne();


                    // get the response from the completed web request.
                    //     webRequest.Proxy = null;
                    using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                    {
                        using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                        {
                            soapResult = rd.ReadToEnd();
                        }
                        Console.Write(soapResult);
                    }
                }
                catch (WebException webex)
                {

                    WebResponse errResp = webex.Response;
                    using (Stream respStream = errResp.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(respStream);
                        string text = reader.ReadToEnd();
                    }

                    try
                    {
                        HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                        webRequest.Proxy = null;
                        webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
                        webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
                        //   webRequest.Proxy = null;

                        webRequest.Timeout = 45000;


                        // begin async call to web request.
                        IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                        // suspend this thread until call is complete. You might want to
                        // do something usefull here like update your UI.
                        asyncResult.AsyncWaitHandle.WaitOne();


                        // get the response from the completed web request.

                        using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                        {
                            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                            {
                                soapResult = rd.ReadToEnd();
                            }

                            Console.Write(soapResult);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHandler.HandleError(ex);
                    }

                }

            }
            catch (WebException webex)
            {

                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    soapResult = text;

                }
            }

            return soapResult;
        }
        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
#if DEBUG
            //    ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

#endif


            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#if !DEBUG
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 200;
            ServicePointManager.MaxServicePointIdleTime = 2000;
            ServicePointManager.SetTcpKeepAlive(false, 0, 0);
#endif


            webRequest.Headers.Add("SOAPAction", "https://stagingxml.tamarindtours.in/Version1.0/BaseDetails.svc?GetDestinations()");
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string XmlText)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(string.Format(@"{0}", XmlText));
            return soapEnvelopeDocument;
        }

        private static HttpWebRequest InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
                return webRequest;
            }
        }
        //private string GeneratePager(int startPage, int totalRows)
        //{
        //    int no_of_pages_per_pager = 5;
        //    int rowsPerPageCount = 15;

        //    System.Text.StringBuilder html = new System.Text.StringBuilder();
        //    int totalpage = ((int)totalRows / rowsPerPageCount) + ((totalRows % rowsPerPageCount) > 0 ? 1 : 0);
        //    html.Append("<div class=\"page-number\">");
        //    html.Append("Page ");
        //    html.Append(startPage);
        //    html.Append(" of ");
        //    html.Append(totalpage);
        //    html.Append("</div>");
        //    html.Append("<ul class=\"pagination");
        //    if (startPage == 1) html.Append(" disabled");
        //    html.Append("\">");
        //    html.Append("<li class=\"prev disabled\"><a href=\"javascript:void(0);\"><i class=\"fa fa-caret-left\"></i></a></li>");
        //    int i = startPage;
        //    html.Append("<li class=\"page active\"><a href=\"javascript:void(0);\">");
        //    html.Append(i);
        //    html.Append("</a></li>");
        //    for (i = startPage + 1; i <= no_of_pages_per_pager && i <= totalpage; i++)
        //    {
        //        html.Append("<li class=\"page\"><a href=\"javascript:void(0);\">");
        //        html.Append(i);
        //        html.Append("</a></li>");
        //    }
        //    html.Append("<li class=\"next");
        //    if (i == totalpage) html.Append(" disabled");
        //    html.Append("\"><a href=\"javascript:void(0);\">");
        //    html.Append("<i class=\"fa fa-caret-right\"></i></a></li>");
        //    html.Append("</ul>");
        //    return html.ToString();
        //}

        #endregion
        //
        // GET: /Search/
        //public ActionResult Index()
        //{
        //    Models.SearchModel md = new Models.SearchModel();
        //    md.SearchCoordinates = new Models.SimpleSearchModel();
        //    md.SearchCoordinates.CheckIn = DateTime.Today;
        //    md.SearchCoordinates.CheckOut = md.SearchCoordinates.CheckIn.AddDays(1);
        //    md.SearchCoordinates.HiddenCheckIn = md.SearchCoordinates.CheckIn;
        //    md.SearchCoordinates.HiddenCheckOut = md.SearchCoordinates.CheckOut;
        //    // md.Results.PagerHtml = GeneratePager(1, 500);
        //    ViewBag.Criteria = md.SearchCoordinates;
        //    return View(md);
        //}
        //public async Task<ActionResult> Index(string City)
        //{
        //    //sumbit from the home page or from side box
        //    Models.SimpleSearchModel data = new Models.SimpleSearchModel();


        //    Models.SearchModel md = new Models.SearchModel();
        //    try
        //    {
        //        data.Destination = City;
        //        data.CheckIn = DateTime.Today.AddDays(1);
        //        data.CheckOut = data.CheckIn.AddDays(1);
        //        data.Adults = "0";
        //        data.Bedrooms = "0";
        //        data.Children = "0";
        //        data.StayType = "";
        //        data.CurrentPage = 1;
        //        md.SearchCoordinates = new Models.SimpleSearchModel()
        //        {
        //            Destination = data.Destination,
        //            Adults = data.Adults,
        //            Bedrooms = data.Bedrooms,
        //            //  CheckIn = data.CheckIn,
        //            // CheckOut = data.CheckOut,
        //            Children = data.Children,
        //            StayType = data.StayType
        //        };

        //        md.SearchCoordinates.CheckIn = data.CheckIn;
        //        md.SearchCoordinates.CheckOut = data.CheckOut;
        //        md.SearchCoordinates.CurrentPage = data.CurrentPage;
        //        md.Results.Destination = data.Destination;
        //        md.Results.IsSearched = true;
        //        md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
        //        md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
        //        md.SearchCoordinates.HiddenDestination = data.Destination;
        //        md.SearchCoordinates.HidAdults = data.Adults;
        //        md.SearchCoordinates.HidBedrooms = data.Bedrooms;
        //        md.SearchCoordinates.HidChildren = data.Children;
        //        md.SearchCoordinates.HidStayType = data.StayType;

        //        SearchResults result = await Search(data);
        //        md.Results.Results = result.Results;
        //        md.Results.CurrentPageIndex = 1;
        //        md.Results.MaxCount = result.TotalRows;
        //        // md.Results.CurrentPageIndex = data.CurrentPage;
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //    }
        //    return View("Index", md);
        //}
        public async Task<ActionResult> SIndex(Models.SearchParamModel spm)
        {

            //sumbit from the home page or from side box
            Models.SearchModel md = new Models.SearchModel();

            try
            {
                Models.SimpleSearchModel data = spm.GetSimpleSearch();
                if (data.Adults == null) { data.Adults = "1"; }
                if (data.Destination == null) { data.Destination = ""; }
                if (data.Children == null) { data.Children = "0"; }
                if (data.StayType == null) { data.StayType = "0"; }
                if (data.Bedrooms == null) { data.Bedrooms = "0"; }
                //if (data.Location == null) { data.Location = ""; }
                if (!User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("BusinessLogin", "Account");
                }
                md.SearchCoordinates = new Models.SimpleSearchModel()
                {
                    Destination = data.Destination,
                    Adults = data.Adults,
                    Bedrooms = data.Bedrooms,
                    //  CheckIn = data.CheckIn,
                    // CheckOut = data.CheckOut,
                    Children = data.Children,
                    StayType = data.StayType
                };
                if (data.CheckIn == DateTime.MinValue)
                {
                    data.CheckIn = DateTime.Today;
                }
                if (data.CheckOut == DateTime.MinValue)
                { data.CheckOut = data.CheckIn.AddDays(1); }
                if (data.CheckIn > data.CheckOut)
                {
                    DateTime t = data.CheckIn;
                    data.CheckIn = data.CheckOut;
                    data.CheckOut = t;
                }

                //if(data.CheckIn == DateTime.MinValue && data.CheckOut == DateTime.MinValue)
                //{
                //    data.CheckIn = DateTime.Today;
                //    data.CheckOut = DateTime.Today.AddDays(1);
                //}else if(data.CheckIn != DateTime.MinValue && data.CheckOut == DateTime.MinValue)
                //{
                //    data.CheckOut = data.CheckIn.AddDays(1);
                //}else if(data.CheckOut < data.CheckIn)
                //{
                //    DateTime t = data.CheckIn;
                //    data.CheckIn = data.CheckOut;
                //    data.CheckOut =t;
                //}
                md.SearchCoordinates.CheckIn = data.CheckIn;
                md.SearchCoordinates.CheckOut = data.CheckOut;
                md.SearchCoordinates.CurrentPage = data.CurrentPage;
                md.Results.Destination = data.Destination;
                md.Results.IsSearched = true;
                md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                md.SearchCoordinates.HiddenDestination = data.Destination;
                md.SearchCoordinates.HidAdults = data.Adults;
                md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                md.SearchCoordinates.HidChildren = data.Children;
                md.SearchCoordinates.HidStayType = data.StayType;
                //md.SearchCoordinates.Location = data.Location;

                SearchResults result = await Search(data);
                md.Results.Results = result.Results;
                md.Results.CurrentPageIndex = 1;
                md.Results.MaxCount = result.TotalRows;
                ViewBag.Criteria = md.SearchCoordinates;
                // md.Results.CurrentPageIndex = data.CurrentPage;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Index", md);
        }


        public ActionResult Index(Models.SearchParamModel param)
        {

            Models.SearchModel md = new Models.SearchModel();
            try
            {

                Models.SimpleSearchModel data = new Models.SimpleSearchModel();
                //if(Session["Adults"]==null)
                //{
                data = param.GetSimpleSearch();
                if (data.Adults == null) { data.Adults = "1"; }
                if (data.Destination == null) { data.Destination = ""; }
                if (data.Children == null) { data.Children = "0"; }
                if (data.StayType == null) { data.StayType = "0"; }
                if (data.Bedrooms == null) { data.Bedrooms = "0"; }
                //}
                //else
                //{
                //   data = param.GetSimpleSearch();
                //    data.Adults = Convert.ToString(Session["Adults"]); 
                //    data.Destination = Convert.ToString(Session["Destination"]); ;
                //     data.Children = Convert.ToString(Session["Children"]); ; 
                //   //  data.StayType = Convert.ToString(Session["Adults"]); ; 
                //     data.Bedrooms = Convert.ToString(Session["Bedrooms"]); ; 

                //}
                //sumbit from the home page or from side box

                //if (data.Location == null) { data.Location = ""; }

                md.SearchCoordinates = new Models.SimpleSearchModel()
                {
                    Destination = data.Destination,
                    Adults = data.Adults,
                    Bedrooms = data.Bedrooms,
                    //  CheckIn = data.CheckIn,
                    // CheckOut = data.CheckOut,
                    Children = data.Children,
                    StayType = data.StayType
                };
                if (data.CheckIn == DateTime.MinValue)
                {
                    data.CheckIn = DateTime.Today;
                }
                if (data.CheckOut == DateTime.MinValue)
                { data.CheckOut = data.CheckIn.AddDays(1); }
                if (data.CheckIn > data.CheckOut)
                {
                    DateTime t = data.CheckIn;
                    data.CheckIn = data.CheckOut;
                    data.CheckOut = t;
                }

                Models.SimpleSearchModel objSearchModel = new Models.SimpleSearchModel();
                objSearchModel.Adults = data.Adults;
                objSearchModel.Bedrooms = data.Bedrooms;
                objSearchModel.Destination = data.Destination;
                objSearchModel.CheckIn = data.CheckIn;
                objSearchModel.CheckOut = data.CheckOut;
                objSearchModel.Children = data.Children;
                objSearchModel.StayType = data.StayType;

                md.SearchCoordinates = objSearchModel;

                ViewBag.Adults = data.Adults;
                ViewBag.Destination = data.Destination.ToLower();
                ViewBag.Bedrooms = data.Bedrooms;
                ViewBag.CheckIn = data.CheckIn.ToShortDateString();
                ViewBag.CheckOut = data.CheckOut.ToShortDateString();
                ViewBag.Children = data.Children;
                ViewBag.StayType = data.StayType;
                ViewBag.GDSRateConversion = Session["GDSRateConversion"];
                ViewBag.GDSCurrencyCode = Session["GDSCurrencyCode"];

                md.Results.SearchCoordinates = md.SearchCoordinates;
                ViewBag.Criteria = md.SearchCoordinates;

                // md.Results.CurrentPageIndex = data.CurrentPage;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }


            //return Content(json);

            //   return Json(md, JsonRequestBehavior.AllowGet);


            return View("Index", md);
        }

        [AllowAnonymous]


        public async Task<JsonResult> Index1(Models.SearchParamModel param)
        {

            Models.SearchModel md = new Models.SearchModel();
            try
            {

                //sumbit from the home page or from side box
                Models.SimpleSearchModel data = new Models.SimpleSearchModel();
                //if (Session["Adults"] == null)
                //{
                data = param.GetSimpleSearch();
                if (data.Adults == null) { data.Adults = "1"; }
                if (data.Destination == null) { data.Destination = ""; }
                if (data.Children == null) { data.Children = "0"; }

                if (data.StayType == null) { data.StayType = "0"; }
                if (data.Bedrooms == null) { data.Bedrooms = "0"; }
                //}
                //else
                //{
                //    data = param.GetSimpleSearch();
                //    data.Adults = Convert.ToString(Session["Adults"]);
                //    data.Destination = Convert.ToString(Session["Destination"]); ;
                //    data.Children = Convert.ToString(Session["Children"]); ;
                //    //  data.StayType = Convert.ToString(Session["Adults"]); ; 
                //    data.Bedrooms = Convert.ToString(Session["Bedrooms"]); ;

                //}
                //if (data.Location == null) { data.Location = ""; }

                md.SearchCoordinates = new Models.SimpleSearchModel()
                {
                    Destination = data.Destination,
                    Adults = data.Adults,
                    Bedrooms = data.Bedrooms,
                    //  CheckIn = data.CheckIn,
                    // CheckOut = data.CheckOut,
                    Children = data.Children,
                    StayType = data.StayType
                };
                if (data.CheckIn == DateTime.MinValue)
                {
                    data.CheckIn = DateTime.Today;
                }
                if (data.CheckOut == DateTime.MinValue)
                { data.CheckOut = data.CheckIn.AddDays(1); }
                if (data.CheckIn > data.CheckOut)
                {
                    DateTime t = data.CheckIn;
                    data.CheckIn = data.CheckOut;
                    data.CheckOut = t;
                }

                ViewBag.Adults = data.Adults;
                ViewBag.Destination = data.Destination.ToLower();
                ViewBag.Bedrooms = data.Bedrooms;
                ViewBag.CheckIn = data.CheckIn.ToShortDateString();
                ViewBag.CheckOut = data.CheckOut.ToShortDateString();
                ViewBag.Children = data.Children;
                ViewBag.StayType = data.StayType;



                //if(data.CheckIn == DateTime.MinValue && data.CheckOut == DateTime.MinValue)
                //{
                //    data.CheckIn = DateTime.Today;
                //    data.CheckOut = DateTime.Today.AddDays(1);
                //}else if(data.CheckIn != DateTime.MinValue && data.CheckOut == DateTime.MinValue)
                //{
                //    data.CheckOut = data.CheckIn.AddDays(1);
                //}else if(data.CheckOut < data.CheckIn)
                //{
                //    DateTime t = data.CheckIn;
                //    data.CheckIn = data.CheckOut;
                //    data.CheckOut =t;
                //}
                if (Request.HttpMethod == "GET")
                {
                    md.SearchCoordinates.CheckIn = data.CheckIn;
                    md.SearchCoordinates.CheckOut = data.CheckOut;
                    md.SearchCoordinates.CurrentPage = data.CurrentPage;
                    md.Results.Destination = data.Destination;
                    md.Results.IsSearched = true;
                    md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                    md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                    md.SearchCoordinates.HiddenDestination = data.Destination;
                    md.SearchCoordinates.HidAdults = data.Adults;
                    md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                    md.SearchCoordinates.HidChildren = data.Children;
                    md.SearchCoordinates.HidStayType = data.StayType;
                    //md.SearchCoordinates.Location = data.Location;

                }
                md.SearchCoordinates.CheckIn = data.CheckIn;
                md.SearchCoordinates.CheckOut = data.CheckOut;
                md.SearchCoordinates.CurrentPage = data.CurrentPage;
                md.Results.Destination = data.Destination;
                md.Results.IsSearched = true;

                md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                md.SearchCoordinates.HiddenDestination = data.Destination;
                md.SearchCoordinates.HidAdults = data.Adults;
                md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                md.SearchCoordinates.HidChildren = data.Children;
                md.SearchCoordinates.HidStayType = data.StayType;
                //md.SearchCoordinates.Location = data.Location;

                SearchResults result = await Search(data);

                Models.SimpleSearchModel objSearchModel = new Models.SimpleSearchModel();
                objSearchModel.Adults = data.Adults;
                objSearchModel.Bedrooms = data.Bedrooms;
                objSearchModel.Destination = data.Destination;
                objSearchModel.CheckIn = data.CheckIn;
                objSearchModel.CheckOut = data.CheckOut;
                objSearchModel.Children = data.Children;
                objSearchModel.StayType = data.StayType;
                objSearchModel.CurrentPage = data.CurrentPage;
                objSearchModel.HiddenCheckIn = data.CheckIn;
                objSearchModel.HiddenCheckOut = data.CheckOut;
                objSearchModel.HiddenDestination = data.Destination;
                objSearchModel.HidAdults = data.Adults;
                objSearchModel.HidBedrooms = data.Bedrooms;
                objSearchModel.HidChildren = data.Children;
                objSearchModel.HidStayType = data.StayType;

                md.SearchCoordinates = objSearchModel;

                md.Results.Results = result.Results;
                md.Results.CurrentPageIndex = 1;
                md.Results.MaxCount = result.TotalRows;
                md.Results.SearchCoordinates = md.SearchCoordinates;
                md.Results.Moreindicator = result.Moreindicator;
                md.Results.SessionID = result.SessionID;
                md.Results.SequenceNumber = result.SequenceNumber;
                md.Results.SecurityToken = result.SecurityToken;

                ViewBag.Criteria = md.SearchCoordinates;

                // md.Results.CurrentPageIndex = data.CurrentPage;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }


            //return Content(json);

            return Json(md, JsonRequestBehavior.AllowGet);


            // return View("Index",md);
        }
        public class ParamClass
        {
            public string ParamResult { get; set; }
        }
        [AllowAnonymous]
        // string pDestination, string pCheckIn, string pCheckOut, string pAdults, string pChildren,  string pBedRooms,
        // [HttpPost]
        public async Task<ActionResult> GetAmadeusHotels(Models.SearchParamModel param)
        {
            Models.SearchModel md = new Models.SearchModel();
            try
            {
                //sumbit from the home page or from side box
                Models.SimpleSearchModel data = param.GetSimpleSearch();
                if (data.Adults == null) { data.Adults = "1"; }
                if (data.Destination == null) { data.Destination = ""; }
                if (data.Children == null) { data.Children = "0"; }
                if (data.StayType == null) { data.StayType = "0"; }
                if (data.Bedrooms == null) { data.Bedrooms = "0"; }
                //if (data.Location == null) { data.Location = ""; }

                md.SearchCoordinates = new Models.SimpleSearchModel()
                {
                    Destination = data.Destination,
                    Adults = data.Adults,
                    Bedrooms = data.Bedrooms,
                    //  CheckIn = data.CheckIn,
                    // CheckOut = data.CheckOut,
                    Children = data.Children,
                    StayType = data.StayType
                };
                if (data.CheckIn == DateTime.MinValue)
                {
                    data.CheckIn = DateTime.Today;
                }
                if (data.CheckOut == DateTime.MinValue)
                { data.CheckOut = data.CheckIn.AddDays(1); }
                if (data.CheckIn > data.CheckOut)
                {
                    DateTime t = data.CheckIn;
                    data.CheckIn = data.CheckOut;
                    data.CheckOut = t;
                }



                if (Request.HttpMethod == "GET")
                {
                    md.SearchCoordinates.CheckIn = data.CheckIn;
                    md.SearchCoordinates.CheckOut = data.CheckOut;
                    md.SearchCoordinates.CurrentPage = data.CurrentPage;
                    md.Results.Destination = data.Destination;
                    md.Results.IsSearched = true;
                    md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                    md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                    md.SearchCoordinates.HiddenDestination = data.Destination;
                    md.SearchCoordinates.HidAdults = data.Adults;
                    md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                    md.SearchCoordinates.HidChildren = data.Children;
                    md.SearchCoordinates.HidStayType = data.StayType;
                    //md.SearchCoordinates.Location = data.Location;
                }
                md.SearchCoordinates.CheckIn = data.CheckIn;
                md.SearchCoordinates.CheckOut = data.CheckOut;
                md.SearchCoordinates.CurrentPage = data.CurrentPage;
                md.Results.Destination = data.Destination;
                md.Results.IsSearched = true;

                md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                md.SearchCoordinates.HiddenDestination = data.Destination;
                md.SearchCoordinates.HidAdults = data.Adults;
                md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                md.SearchCoordinates.HidChildren = data.Children;
                md.SearchCoordinates.HidStayType = data.StayType;
                //md.SearchCoordinates.Location = data.Location;
                //   SearchResults result = await Search(data,true );

                SearchResults result = await SearchGDS(data, true);


                md.Results.Results = result.Results;
                md.Results.CurrentPageIndex = 1;
                md.Results.MaxCount = result.TotalRows;
                md.Results.Moreindicator = result.Moreindicator;
                md.Results.SessionID = result.SessionID;
                md.Results.SequenceNumber = result.SequenceNumber;
                md.Results.SecurityToken = result.SecurityToken;

                Models.SimpleSearchModel objSearchModel = new Models.SimpleSearchModel();
                objSearchModel.Adults = data.Adults;
                objSearchModel.Bedrooms = data.Bedrooms;
                objSearchModel.Destination = data.Destination;
                objSearchModel.CheckIn = data.CheckIn;
                objSearchModel.CheckOut = data.CheckOut;
                objSearchModel.Children = data.Children;
                objSearchModel.StayType = data.StayType;
                objSearchModel.CurrentPage = data.CurrentPage;
                objSearchModel.HiddenCheckIn = data.CheckIn;
                objSearchModel.HiddenCheckOut = data.CheckOut;
                objSearchModel.HiddenDestination = data.Destination;
                objSearchModel.HidAdults = data.Adults;
                objSearchModel.HidBedrooms = data.Bedrooms;
                objSearchModel.HidChildren = data.Children;
                objSearchModel.HidStayType = data.StayType;

                md.SearchCoordinates = objSearchModel;
                md.Results.SearchCoordinates = md.SearchCoordinates;
                ViewBag.Criteria = md.SearchCoordinates;
                ViewBag.Adults = data.Adults;
                ViewBag.Destination = data.Destination.ToLower();
                ViewBag.Bedrooms = data.Bedrooms;
                ViewBag.CheckIn = data.CheckIn.ToShortDateString();
                ViewBag.CheckOut = data.CheckOut.ToShortDateString();
                ViewBag.Children = data.Children;
                ViewBag.StayType = data.StayType;

                // md.Results.CurrentPageIndex = data.CurrentPage;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            // return View("Index", md);
            return Json(md, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        // string pDestination, string pCheckIn, string pCheckOut, string pAdults, string pChildren,  string pBedRooms,
        // [HttpPost]
        public async Task<ActionResult> GetMoreAmadeusHotels(Models.SearchParamModel param)
        {
            Models.SearchModel md = new Models.SearchModel();
            try
            {
                //sumbit from the home page or from side box
                Models.SimpleSearchModel data = param.GetSimpleSearch();
                if (data.Adults == null) { data.Adults = "1"; }
                if (data.Destination == null) { data.Destination = ""; }
                if (data.Children == null) { data.Children = "0"; }
                if (data.StayType == null) { data.StayType = "0"; }
                if (data.Bedrooms == null) { data.Bedrooms = "0"; }
                //if (data.Location == null) { data.Location = ""; }

                if (string.IsNullOrEmpty(data.Moreindicator))
                {
                    data.Moreindicator = "";
                }

                if (string.IsNullOrEmpty(data.SessionID)) { data.SessionID = ""; }
                if (string.IsNullOrEmpty(data.SecurityToken)) { data.SecurityToken = ""; }
                if (data.SequenceNumber == 0) { data.SequenceNumber = 0; }

                md.SearchCoordinates = new Models.SimpleSearchModel()
                {
                    Destination = data.Destination,
                    Adults = data.Adults,
                    Bedrooms = data.Bedrooms,
                    //  CheckIn = data.CheckIn,
                    // CheckOut = data.CheckOut,
                    Children = data.Children,
                    StayType = data.StayType
                };
                if (data.CheckIn == DateTime.MinValue)
                {
                    data.CheckIn = DateTime.Today;
                }
                if (data.CheckOut == DateTime.MinValue)
                { data.CheckOut = data.CheckIn.AddDays(1); }
                if (data.CheckIn > data.CheckOut)
                {
                    DateTime t = data.CheckIn;
                    data.CheckIn = data.CheckOut;
                    data.CheckOut = t;
                }



                if (Request.HttpMethod == "GET")
                {
                    md.SearchCoordinates.CheckIn = data.CheckIn;
                    md.SearchCoordinates.CheckOut = data.CheckOut;
                    md.SearchCoordinates.CurrentPage = data.CurrentPage;
                    md.Results.Destination = data.Destination;
                    md.Results.IsSearched = true;
                    md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                    md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                    md.SearchCoordinates.HiddenDestination = data.Destination;
                    md.SearchCoordinates.HidAdults = data.Adults;
                    md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                    md.SearchCoordinates.HidChildren = data.Children;
                    md.SearchCoordinates.HidStayType = data.StayType;
                    //md.SearchCoordinates.Location = data.Location;

                }
                md.SearchCoordinates.CheckIn = data.CheckIn;
                md.SearchCoordinates.CheckOut = data.CheckOut;
                md.SearchCoordinates.CurrentPage = data.CurrentPage;
                md.Results.Destination = data.Destination;
                md.Results.IsSearched = true;

                md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                md.SearchCoordinates.HiddenDestination = data.Destination;
                md.SearchCoordinates.HidAdults = data.Adults;
                md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                md.SearchCoordinates.HidChildren = data.Children;
                md.SearchCoordinates.HidStayType = data.StayType;
                //md.SearchCoordinates.Location = data.Location;
                //   SearchResults result = await Search(data,true );

                SearchResults result = await SearchMoreGDS(data, true);


                md.Results.Results = result.Results;
                md.Results.CurrentPageIndex = 1;
                md.Results.MaxCount = result.TotalRows;
                md.Results.Moreindicator = result.Moreindicator;
                md.Results.SessionID = result.SessionID;
                md.Results.SequenceNumber = result.SequenceNumber;
                md.Results.SecurityToken = result.SecurityToken;


                Models.SimpleSearchModel objSearchModel = new Models.SimpleSearchModel();
                objSearchModel.Adults = data.Adults;
                objSearchModel.Bedrooms = data.Bedrooms;
                objSearchModel.Destination = data.Destination;
                objSearchModel.CheckIn = data.CheckIn;
                objSearchModel.CheckOut = data.CheckOut;
                objSearchModel.Children = data.Children;
                objSearchModel.StayType = data.StayType;
                objSearchModel.CurrentPage = data.CurrentPage;
                objSearchModel.HiddenCheckIn = data.CheckIn;
                objSearchModel.HiddenCheckOut = data.CheckOut;
                objSearchModel.HiddenDestination = data.Destination;
                objSearchModel.HidAdults = data.Adults;
                objSearchModel.HidBedrooms = data.Bedrooms;
                objSearchModel.HidChildren = data.Children;
                objSearchModel.HidStayType = data.StayType;

                md.SearchCoordinates = objSearchModel;
                md.Results.SearchCoordinates = md.SearchCoordinates;
                ViewBag.Criteria = md.SearchCoordinates;
                ViewBag.Adults = data.Adults;
                ViewBag.Destination = data.Destination.ToLower();
                ViewBag.Bedrooms = data.Bedrooms;
                ViewBag.CheckIn = data.CheckIn.ToShortDateString();
                ViewBag.CheckOut = data.CheckOut.ToShortDateString();
                ViewBag.Children = data.Children;
                ViewBag.StayType = data.StayType;

                // md.Results.CurrentPageIndex = data.CurrentPage;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            // return View("Index", md);
            return Json(md, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ApplyFilter(Models.SimpleSearchModel data)
        {
            //sumbit from the home page or from side box
            Models.SearchResultModel md = new Models.SearchResultModel();

            SearchResults result = await SearchFilter(data);
            md.Results = result.Results;
            md.CurrentPageIndex = data.CurrentPage;
            md.MaxCount = result.TotalRows;
            md.IsSearched = true;
            //     md.CurrentPageIndex = data.CurrentPage;
            md.Destination = data.Destination;
            ViewBag.Criteria = data;
            // md.MaxCount = 0;
            return Content("");
            //  return View("~/Views/Search/_Result.cshtml",md);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SearchPager(Models.SimpleSearchModel data)
        {
            //sumbit from the home page or from side box
            Models.SearchResultModel md = new Models.SearchResultModel();
            SearchResults result = await SearchFilter(data);
            md.Results = result.Results;
            md.MaxCount = result.TotalRows;
            md.IsSearched = true;
            md.CurrentPageIndex = data.CurrentPage;
            md.Destination = data.Destination;
            md.MaxCount = 0;
            ViewBag.Criteria = data;
            return View("~/Views/Search/_Result.cshtml", md);
        }

        [AllowAnonymous]
        public async Task<ActionResult> sitewide(Models.SearchParamModel param)
        {

            Models.SearchModel md = new Models.SearchModel();
            try
            {
                //sumbit from the home page or from side box
                Models.SimpleSearchModel data = param.GetSimpleSearch();
                if (data.Adults == null) { data.Adults = "1"; }
                if (data.Destination == null) { data.Destination = ""; }
                if (data.Children == null) { data.Children = "0"; }
                if (data.StayType == null) { data.StayType = "0"; }
                if (data.Bedrooms == null) { data.Bedrooms = "0"; }
                //if (data.Location == null) { data.Location = ""; }

                md.SearchCoordinates = new Models.SimpleSearchModel()
                {
                    Destination = data.Destination,
                    Adults = data.Adults,
                    Bedrooms = data.Bedrooms,
                    // CheckIn = data.CheckIn,
                    // CheckOut = data.CheckOut,
                    Children = data.Children,
                    StayType = data.StayType
                };
                if (data.CheckIn == DateTime.MinValue)
                {
                    data.CheckIn = DateTime.Today;
                }
                if (data.CheckOut == DateTime.MinValue)
                { data.CheckOut = data.CheckIn.AddDays(1); }
                if (data.CheckIn > data.CheckOut)
                {
                    DateTime t = data.CheckIn;
                    data.CheckIn = data.CheckOut;
                    data.CheckOut = t;
                }


                //if(data.CheckIn == DateTime.MinValue && data.CheckOut == DateTime.MinValue)
                //{
                //    data.CheckIn = DateTime.Today;
                //    data.CheckOut = DateTime.Today.AddDays(1);
                //}else if(data.CheckIn != DateTime.MinValue && data.CheckOut == DateTime.MinValue)
                //{
                //    data.CheckOut = data.CheckIn.AddDays(1);
                //}else if(data.CheckOut < data.CheckIn)
                //{
                //    DateTime t = data.CheckIn;
                //    data.CheckIn = data.CheckOut;
                //    data.CheckOut =t;
                //}
                if (Request.HttpMethod == "GET")
                {
                    md.SearchCoordinates.CheckIn = data.CheckIn;
                    md.SearchCoordinates.CheckOut = data.CheckOut;
                    md.SearchCoordinates.CurrentPage = data.CurrentPage;
                    md.Results.Destination = data.Destination;
                    md.Results.IsSearched = true;
                    md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                    md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                    md.SearchCoordinates.HiddenDestination = data.Destination;
                    md.SearchCoordinates.HidAdults = data.Adults;
                    md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                    md.SearchCoordinates.HidChildren = data.Children;
                    md.SearchCoordinates.HidStayType = data.StayType;
                    //md.SearchCoordinates.Location = data.Location;

                }
                md.SearchCoordinates.CheckIn = data.CheckIn;
                md.SearchCoordinates.CheckOut = data.CheckOut;
                md.SearchCoordinates.CurrentPage = data.CurrentPage;
                md.Results.Destination = data.Destination;
                md.Results.IsSearched = true;

                md.SearchCoordinates.HiddenCheckIn = data.CheckIn;
                md.SearchCoordinates.HiddenCheckOut = data.CheckOut;
                md.SearchCoordinates.HiddenDestination = data.Destination;
                md.SearchCoordinates.HidAdults = data.Adults;
                md.SearchCoordinates.HidBedrooms = data.Bedrooms;
                md.SearchCoordinates.HidChildren = data.Children;
                md.SearchCoordinates.HidStayType = data.StayType;
                //md.SearchCoordinates.Location = data.Location;
                SearchResults result = await Search(data);
                md.Results.Results = result.Results;
                md.Results.CurrentPageIndex = 1;
                md.Results.MaxCount = result.TotalRows;
                ViewBag.Criteria = md.SearchCoordinates;
                // md.Results.CurrentPageIndex = data.CurrentPage;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("ShortResult", md.Results);
        }

        [HttpGet]
        public ActionResult ResultListing(Models.SearchResultModel data)
        {

            return View("~/Views/Search/_Result.cshtml", data);
        }

        public async Task<long> SaveAmadeus_Property(Models.PropertyModel data)
        {
            try
            {

                CLayer.Property prprty = new CLayer.Property()
                {

                    PropertyId = data.PropertyId,
                    Title = data.Title,
                    Description = data.Description,
                    Location = data.Location,
                    Status = (CLayer.ObjectStatus.StatusType)data.Status,
                    OwnerId = data.OwnerId,
                    Address = data.Address,
                    Country = data.Country,
                    State = data.State,
                    City = data.City,
                    CityId = data.CityId,
                    ZipCode = data.ZipCode,
                    PropertyInventoryType = data.InventoryAPIType,
                    HotelID = data.HotelId,
                    TBOHotelId = data.TBOHotelId,
                    TBOFlag = data.TBOFlag,
                    TamarindHotelId = data.TamarindHotelId,
                    TamarindFlag = data.TamarindFlag,
                    RateCardDetailedId = data.RateCardDetailedId,
                    TaxPercentage = data.TaxPercentage,
                    GSTSlab = data.GSTSlab
                };
                string loc = "";
                try
                {

                    string statename = data.StateName;
                    string cityname;
                    //if (data.Country == 0)
                    //{

                    //}
                    if (data.CityId < 1)
                    {
                        cityname = data.City;
                    }
                    else
                    {
                        cityname = BLayer.City.Get(data.CityId).Name;
                        data.City = cityname;
                        prprty.City = cityname;
                    }
                    string Countryname = data.CountryName;// BLayer.Country.Get(data.Country).Name;
                    loc = cityname + "," + statename + "," + Countryname + "," + data.ZipCode;
                    string qAdddress = data.Title + "," + data.Address + "," + loc;

                    //Common.Utils.Location pos;

                    //pos = await Common.Utils.GetLocation(qAdddress);
                    prprty.PositionLat = null;
                    prprty.PositionLng = null;

                }
                catch (Exception ex)
                {
                    Common.LogHandler.LogError(ex);
                }


                data.PropertyId = BLayer.Property.SaveAmadeus_Property(prprty);
                return data.PropertyId;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return 0;
            }
        }

        private string RemoveInvalidCharacters(string Description)
        {
            string Result = string.Empty;
            //#256#<br clear='All'/><b>Area information</b><br/><div>Close to Shopping (Linking Road) and Office Complex (KBC) <br/></div><br clear='All'/><b>Features</b><br/><div><table><tr><td style="vertical-align:top"><ul><li> Restautant:
            //            TÃªte - Ã  - tÃªte at the Lounge Coffee Shop.
            //24 - hour Room Service multi - cuisine array.
            //Complimentary bed tea and Buffet Breakfast .
            //Only the very best product is used by our chefto create a memorable meal displaying originality and finesse.

            //</ li >< li > Free services:</ li ></ ul ></ td ></ tr ></ table ></ div >< br clear = 'All' />< b > Reception </ b >< br />< div > Manned at weekends<br/></ div >
            if (!string.IsNullOrEmpty(Description))
            {
                Result = Description.Replace("<br clear='All'/><b>", " ");
                Result = Result.Replace("<div>", " ").Replace("</div>", " ");
                Result = Result.Replace("<table><tr><td style=\"vertical - align:top\"><ul><li>", " ");
                Result = Result.Replace("</li>", " ").Replace("<li>", "");
            }



            return Result;
        }
        public string ToTitleCase(string str)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
        public static string TamrindMultiSingleAvailability(CLayer.SearchCriteria sch)
        {
            TamarindService.TamarindDataServiceClient client = new TamarindService.TamarindDataServiceClient();

            client.ClientCredentials.UserName.UserName = "STAYBAZARXMLTEST";
            client.ClientCredentials.UserName.Password = "STAYBAZARXMLTEST";

            string CityId = BLayer.City.GetTamarindCityID(sch.Destination);
            LogHandler.AddLog("tamarind city id:-" + DateTime.Now.ToString());
            //SearchController hotellist = new SearchController();
            //hotellist.TamarindHotelList_Save(CityId);

            var dateAndTime = sch.CheckIn;
            TamarindService.HotelSearchParam HotelParm = new TamarindService.HotelSearchParam();
            HotelParm.AgentMarket = "0";
            HotelParm.AllowOnrequest = Convert.ToBoolean("True");
            HotelParm.CityID = CityId;
            HotelParm.CheckinDate = Convert.ToDateTime(dateAndTime);
            HotelParm.Nights = 1;
            HotelParm.SingleBed = 1;
            HotelParm.DoubleBed = 0;
            HotelParm.TwinBed = 0;
            HotelParm.ExtraBed = 0;
            HotelParm.OriginCountryID = "IN";
            //HotelParm.HotelID = "566";
            HotelParm.HotelID = "";
            HotelParm.Rating = "";
            string hotel = client.GetHotels(HotelParm);
            LogHandler.AddLog("tamarind hotel count:-" + DateTime.Now.ToString());
            return hotel;
        }
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public void TamarindHotelList_Save(string cityid)
        {
            //getting hotellist 22-11-2019

            TamarindBasic.BaseDetailsClient Basic = new TamarindBasic.BaseDetailsClient();

            Basic.ClientCredentials.UserName.UserName = "STAYBAZARXMLTEST";
            Basic.ClientCredentials.UserName.Password = "STAYBAZARXMLTEST";

            TamarindBasic.ListHotelParam pram = new TamarindBasic.ListHotelParam();
            pram.CityID = cityid;
            pram.StarRating = "";
            string hotel = Basic.GetHotelList(pram);

            DataSet lds_hotels = new DataSet();

            lds_hotels.ReadXml(GenerateStreamFromString(hotel));

            for (int i = 0; i < lds_hotels.Tables["Hotel"].Rows.Count; i++)
            {
                BLayer.Tamarind.UpdateHotelList(Convert.ToInt32(lds_hotels.Tables["Hotel"].Rows[i][0]), (lds_hotels.Tables["Hotel"].Rows[i][1]).ToString());
            }
        }
        public static string TBOHotelSearch(CLayer.SearchCriteria sch)
        {
            string lsz_country;
            string tokenid = BLayer.TBO.TokenID();
            if (sch.Country == "")
                lsz_country = BLayer.TBO.CountryCode("India");
            else
                lsz_country = BLayer.TBO.CountryCode(sch.Country);
            int ld_city = BLayer.TBO.GetTBOCityId(sch.Destination);
            LogHandler.AddLog("TBO City id:-" + DateTime.Now.ToString());
            CLayer.TBOHotelSearch param1 = new CLayer.TBOHotelSearch
            {
                EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
                TokenId = tokenid,
                CheckInDate = sch.CheckIn.ToString("dd/MM/yyyy"),
                NoOfNights = 1,
                CountryCode = lsz_country,
                CityId = ld_city,
                PreferredCurrency = "INR",
                GuestNationality = lsz_country,
                NoOfRooms = "1",
                MaxRating = 5,
                MinRating = 0,
                PreferredHotel = "",
                IsNearBySearchAllowed = false
            };
            param1.RoomGuests.Add(new CLayer.RoomGuests()
            {
                NoOfAdults = sch.Adults,
                NoOfChild = 0
            });

            string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(param1);
            string authurl = "http://api.tektravels.com/BookingEngineService_Hotel/hotelservice.svc/rest/GetHotelResult/";

            string responsedata = BLayer.TBO.GetResponse(requestdata, authurl);
            LogHandler.AddLog("TBO response:-" + DateTime.Now.ToString());
            return responsedata;
        }
        public static string TBOHotelRoomRequest(CLayer.SearchCriteria room)
        {
            string tokenid = BLayer.TBO.TokenID();
            CLayer.TBOHotelRoom param1 = new CLayer.TBOHotelRoom
            {
                EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
                TokenId = tokenid,
                TraceId = room.TraceId,
                ResultIndex = room.ResultIndex,
                HotelCode = room.HotelCode
            };

            string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(param1);
            string authurl = "http://api.tektravels.com/BookingEngineService_Hotel/hotelservice.svc/rest/GetHotelRoom";

            string responsedata = BLayer.TBO.GetResponse(requestdata, authurl);
            return responsedata;
        }
        public long GetUserId()
        {
            long userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                long.TryParse(User.Identity.GetUserId(), out userId);
            }
            else
                if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] != null)
            {
                userId = (long)Session[CLayer.ObjectStatus.GUEST_ID_SESSION];
            }
            return userId;
        }
        public ActionResult GetPreferdHotels(Models.SearchParamModel param)
        {
            CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
            ViewBag.Destination = param.Destination;
            ViewBag.CheckIN = param.CheckIn;
            ViewBag.CheckOUT = param.CheckOut;
            return View();
        }
    }

}
