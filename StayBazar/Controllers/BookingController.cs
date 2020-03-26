using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Microsoft.AspNet.Identity;
using Rotativa;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Configuration;
using StayBazar.Common;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.Collections;
using System.Globalization;
using System.Data;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class BookingController : Controller
    {
        //
        // GET: /Booking/

        #region Custom Methods
        public List<CLayer.RoomStaysResult> AmedusRoomStaysResultList = null;
        public CLayer.HotelAvailability.Envelope HotelResult = null;
        public CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
        public CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdvanced = null;
        public CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStays HotelItemAdvanced = new CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStays();
        public static List<CLayer.GDSCurrencyConversions> CurrencyConversionList = new List<CLayer.GDSCurrencyConversions>();

        public Models.BookingModel LoadBilling(long bookingId)
        {
            Models.BookingModel data = new Models.BookingModel();
            if (bookingId == 0) return data;
            //    CLayer.Address wh = BLayer.Bookings.GetBookedByUser(bookingId);
            List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);// for booking user only propertyname and address
            //if the booking items are 
            data.OrderedBy = adr[0];
            int InventoryAPIType = Convert.ToInt32(Session["InventoryAPIType"]);
            InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(bookingId));

            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data.Items = BLayer.BookingItem.GetAllDetails(bookingId, true);
            }
            else
            {
                data.Items = BLayer.BookingItem.GetAllDetails(bookingId);
            }

            long PayOption = BLayer.Bookings.GetPaymentoption(bookingId);
            if (PayOption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment)
            {
                data.partialpayment = false;
            }
            else
            {
                data.partialpayment = true;
            }
            CLayer.Booking bdata = BLayer.Bookings.GetDetails(bookingId);
            data.BookingDetails.OrderNo = bdata.OrderNo;
            data.BookingDetails.BookingDate = bdata.BookingDate;
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data.Supplier = BLayer.Bookings.GetSupplierDetailsAmadeus(bookingId);
            }
            else
            {
                data.Supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
            }
            data.BookingId = bookingId;
            data.InventoryAPIType = InventoryAPIType;
            return data;
        }
        public Models.BookingModel LoadBillingByCredit(long bookingId)
        {
            Models.BookingModel data = new Models.BookingModel();
            if (bookingId == 0) return data;
            //    CLayer.Address wh = BLayer.Bookings.GetBookedByUser(bookingId);
            List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);// for booking user only propertyname and address
            //if the booking items are 
            data.OrderedBy = adr[0];
            int InventoryAPIType = Convert.ToInt32(System.Web.HttpContext.Current.Session["InventoryAPIType"]);
            InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(bookingId));

            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data.Items = BLayer.BookingItem.GetAllDetails(bookingId, true);
            }
            else
            {
                data.Items = BLayer.BookingItem.GetAllDetails(bookingId);
            }

            long PayOption = BLayer.Bookings.GetPaymentoption(bookingId);
            if (PayOption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment)
            {
                data.partialpayment = false;
            }
            else
            {
                data.partialpayment = true;
            }
            CLayer.Booking bdata = BLayer.Bookings.GetDetails(bookingId);
            data.BookingDetails.OrderNo = bdata.OrderNo;
            data.BookingDetails.BookingDate = bdata.BookingDate;
            data.Supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
            data.BookingId = bookingId;
            data.InventoryAPIType = InventoryAPIType;
            return data;
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

        private int BookAccommodation(Models.SimpleBookingModel data)
        {
            try
            {
                long userId = GetUserId();
                long bookingId = BLayer.Bookings.GetCartIdAfterCleaning(userId);
                long offlinebookingId = BLayer.Bookings.GetOfflinebookingCartIdAfterCleaning(userId);
                //decimal PartialAmountPerc = BLayer.Property.GetPropertypartialamount(data.PropertyId);       

                Models.SimpleBookingModel Amedusdata = (Models.SimpleBookingModel)TempData["AmedusData"];
                TempData.Peek("AmedusData");


                if (data.BookingData == "")
                {
                    return 0;
                }

                string[] bookitems = data.BookingData.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                if (bookitems.Length == 0)
                {
                    return 0;
                }
                long id;
                int nums, kids, adults, guests;
                List<CLayer.BookingAccDetails> bookings = new List<CLayer.BookingAccDetails>();
                List<long> accIds = new List<long>();
                CLayer.BookingAccDetails tmp;
                foreach (string s in bookitems)
                {
                    if (s == "") continue;
                    string[] ids = s.Split(new char[] { ',' });
                    if (ids.Length != 5) continue;


                    id = 0;
                    adults = 0;
                    kids = 0;
                    guests = 0;
                    nums = 0;

                    long.TryParse(ids[0], out id);
                    if (id == 0) continue;
                    if (accIds.Contains(id)) continue;
                    int.TryParse(ids[1], out nums);
                    if (nums == 0) continue;
                    int.TryParse(ids[2], out adults);
                    //   if (adults == 0) continue;
                    int.TryParse(ids[3], out kids);
                    int.TryParse(ids[4], out guests);


                    tmp = new CLayer.BookingAccDetails();
                    tmp.AccCount = nums;
                    tmp.AccommodationId = id;
                    tmp.Adults = adults;
                    tmp.Children = kids;
                    tmp.Guest = guests;
                    accIds.Add(id);
                    bookings.Add(tmp);
                }
                if (accIds.Count == 0) return 1;

                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(data.PropertyId);
                Session["InventoryAPIType"] = InventoryAPIType;
                string HotelId = BLayer.Property.GetHotelId(data.PropertyId);
                List<CLayer.RoomStaysResult> objRoomStayResult = new List<CLayer.RoomStaysResult>();
                List<CLayer.Rates> AmadeusRates = new List<CLayer.Rates>();
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {

                    objRoomStayResult = APIUtility.AmedusHotelRoomStaysResultList;// GetRoomStayResult(data.PropertyId, data.BookCheckIn, data.BookCheckOut);
                    #region ENHANCED PRICING
                    //  Enhanced pricing
                    AmadeusRates = GetAmedusRates(data, objRoomStayResult, HotelId);
                    TempData["AmadeusRates"] = AmadeusRates;
                    TempData["InventoryAPIType"] = InventoryAPIType;
                    Session["AmadeusRates"] = AmadeusRates;
                    if (Session["CheckIn"] == null) Session["CheckIn"] = data.BookCheckIn;

                    ViewBag.amt = AmadeusRates;
                    ArrayList BookingCodeList = new ArrayList();
                    foreach (var item in AmadeusRates)
                    {
                        BookingCodeList.Add(item.BookingCode);
                    }

                    objRoomStayResult = objRoomStayResult.Where(a => BookingCodeList.Contains(a.BookingCode)).ToList();
                    //Enhanced pricing end
                    #endregion

                    #region PNR ADD MULTIELEMENTS-0      
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
                    string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;


                    AmedusRoomStaysResultList = objRoomStayResult;
                    APIUtility.AmedusHotelRoomStaysResultList = AmedusRoomStaysResultList;

                    string BookingCode = objRoomStayResult.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCode, "test@test.com");

                    TempData["RoomStaysResult"] = AmedusRoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = AmedusRoomStaysResultList;

                    string result = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElements, bookingId);

                    #endregion Transaction log end

                    Serializer pnrser = new Serializer();

                    CLayer.PNRAddElements.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElements.Envelope>(result);
                    Session["SessionId"] = PNRAddResult.Header.Session.SessionId;
                    Session["SequenceNumber"] = Convert.ToInt32(PNRAddResult.Header.Session.SequenceNumber);
                    Session["SecurityToken"] = PNRAddResult.Header.Session.SecurityToken;
                    #endregion
                }
                else
                {
                    objRoomStayResult = null;
                    AmadeusRates = null;
                }

                try
                {
                    bookingId = (Request["BookingID"]) != null ? Convert.ToInt64(Request["BookingID"]) : 0;
                    Session["BookingID"] = bookingId;
                    bookingId = Session["BookingID"] != null ? Convert.ToInt64(Session["BookingID"]) : 0;
                }
                catch (Exception ex)
                {
                    bookingId = 0;
                }

                int CountryID = Convert.ToInt32(Session["GDSCountryID"]);
                //  List<CLayer.Rates> accrates = BLayer.Rate.GetTotalRates(accIds, data.BookCheckIn, data.BookCheckOut, BLayer.User.GetRole(userId), userId);
                return BLayer.Rate.BookAccommodations(bookings, data.BookCheckIn, data.BookCheckOut, userId, bookingId, offlinebookingId, "", objRoomStayResult, AmadeusRates, CountryID = 0);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return 0;
            }
        }

        private int BookAccommodation(Models.SimpleBookingModel data, long UserID, long BookingID, long OfflineBookingId)
        {
            try
            {
                long userId = UserID;// GetUserId();
                long bookingId = BookingID;// BLayer.Bookings.GetCartIdAfterCleaning(userId);
                //decimal PartialAmountPerc = BLayer.Property.GetPropertypartialamount(data.PropertyId);       
                long offlinebookingid = OfflineBookingId;
                Models.SimpleBookingModel Amedusdata = (Models.SimpleBookingModel)TempData["AmedusData"];
                TempData.Peek("AmedusData");


                if (data.BookingData == "")
                {
                    return 0;
                }

                string[] bookitems = data.BookingData.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
                if (bookitems.Length == 0)
                {
                    return 0;
                }
                long id;
                int nums, kids, adults, guests;
                List<CLayer.BookingAccDetails> bookings = new List<CLayer.BookingAccDetails>();
                List<long> accIds = new List<long>();
                CLayer.BookingAccDetails tmp;
                foreach (string s in bookitems)
                {
                    if (s == "") continue;
                    string[] ids = s.Split(new char[] { ',' });
                    if (ids.Length != 5) continue;


                    id = 0;
                    adults = 0;
                    kids = 0;
                    guests = 0;
                    nums = 0;

                    long.TryParse(ids[0], out id);
                    if (id == 0) continue;
                    if (accIds.Contains(id)) continue;
                    int.TryParse(ids[1], out nums);
                    if (nums == 0) continue;
                    int.TryParse(ids[2], out adults);
                    //   if (adults == 0) continue;
                    int.TryParse(ids[3], out kids);
                    int.TryParse(ids[4], out guests);


                    tmp = new CLayer.BookingAccDetails();
                    tmp.AccCount = nums;
                    tmp.AccommodationId = id;
                    tmp.Adults = adults;
                    tmp.Children = kids;
                    tmp.Guest = guests;
                    accIds.Add(id);
                    bookings.Add(tmp);
                }
                if (accIds.Count == 0) return 1;

                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(data.PropertyId);
                System.Web.HttpContext.Current.Session["InventoryAPIType"] = InventoryAPIType;
                string HotelId = BLayer.Property.GetHotelId(data.PropertyId);
                List<CLayer.RoomStaysResult> objRoomStayResult = new List<CLayer.RoomStaysResult>();
                List<CLayer.Rates> AmadeusRates = new List<CLayer.Rates>();
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {

                    // objRoomStayResult = APIUtility.AmedusHotelRoomStaysResultList;// GetRoomStayResult(data.PropertyId, data.BookCheckIn, data.BookCheckOut);
                    objRoomStayResult = GetRoomStayResultForCreditBooking(InventoryAPIType, data, UserID, BookingID);
                    CLayer.Booking bdataCredit = BLayer.Bookings.GetDataForCreditBooking(BookingID);
                    bdataCredit.ByUserId = UserID;

                    #region ENHANCED PRICING
                    //  Enhanced pricing
                    AmadeusRates = GetAmedusRatesForCredit(data, objRoomStayResult, HotelId, bdataCredit);
                    TempData["AmadeusRates"] = AmadeusRates;
                    TempData["InventoryAPIType"] = InventoryAPIType;
                    System.Web.HttpContext.Current.Session["AmadeusRates"] = AmadeusRates;
                    if (System.Web.HttpContext.Current.Session["CheckIn"] == null) System.Web.HttpContext.Current.Session["CheckIn"] = data.BookCheckIn;

                    ViewBag.amt = AmadeusRates;
                    ArrayList BookingCodeList = new ArrayList();
                    foreach (var item in AmadeusRates)
                    {
                        BookingCodeList.Add(item.BookingCode);
                    }

                    objRoomStayResult = objRoomStayResult.Where(a => BookingCodeList.Contains(a.BookingCode)).ToList();
                    //Enhanced pricing end
                    #endregion

                    #region PNR ADD MULTIELEMENTS-0      
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
                    string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, System.Web.HttpContext.Current.Session["SessionId"].ToString(), Convert.ToInt32(System.Web.HttpContext.Current.Session["SequenceNumber"]), System.Web.HttpContext.Current.Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;


                    AmedusRoomStaysResultList = objRoomStayResult;
                    APIUtility.AmedusHotelRoomStaysResultList = AmedusRoomStaysResultList;

                    string BookingCode = objRoomStayResult.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCode, "test@test.com");

                    TempData["RoomStaysResult"] = AmedusRoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = AmedusRoomStaysResultList;

                    string result = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(UserID), (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElements, bookingId);

                    #endregion Transaction log end

                    Serializer pnrser = new Serializer();

                    CLayer.PNRAddElements.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElements.Envelope>(result);
                    System.Web.HttpContext.Current.Session["SessionId"] = PNRAddResult.Header.Session.SessionId;
                    System.Web.HttpContext.Current.Session["SequenceNumber"] = Convert.ToInt32(PNRAddResult.Header.Session.SequenceNumber);
                    System.Web.HttpContext.Current.Session["SecurityToken"] = PNRAddResult.Header.Session.SecurityToken;
                    #endregion
                }
                else
                {
                    objRoomStayResult = null;
                    AmadeusRates = null;
                }

                try
                {
                    bookingId = BookingID;// (Request["BookingID"]) != null ? Convert.ToInt64(Request["BookingID"]) : 0;
                    System.Web.HttpContext.Current.Session["BookingID"] = bookingId;
                    offlinebookingid = OfflineBookingId;
                    System.Web.HttpContext.Current.Session["BookingID"] = offlinebookingid;
                }
                catch (Exception ex)
                {
                    bookingId = 0;
                    offlinebookingid = 0;
                }

                List<CLayer.Rates> accrates = BLayer.Rate.GetTotalRates(accIds, data.BookCheckIn, data.BookCheckOut, BLayer.User.GetRole(userId), userId, InventoryAPIType);
                return BLayer.Rate.BookAccommodations(bookings, data.BookCheckIn, data.BookCheckOut, userId, bookingId, OfflineBookingId, "", objRoomStayResult, AmadeusRates);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return 0;
            }
        }

        public List<CLayer.RoomStaysResult> GetRoomStayResultForCreditBooking(int InventoryAPIType, Models.SimpleBookingModel data, long UserID, long BookingID)
        {
            List<CLayer.RoomStaysResult> objResult = new List<CLayer.RoomStaysResult>();

            try
            {
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    #region LIVE HOTEL SEARCH
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
                    string HotelId = BLayer.Property.GetHotelId(data.PropertyId);
                    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                    srch.HotelCode = HotelId;
                    srch.CheckIn = data.BookCheckIn;
                    srch.CheckOut = data.BookCheckOut;

                    string result = SearchController.HotelMultiSingleAvailability(srch, false, HotelId);

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(UserID), (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailabilityLiveSearch, 0);

                    #endregion Transaction log end



                    Serializer ser = new Serializer();
                    HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);

                    Serializer seradv = new Serializer();
                    HotelResultAdvanced = seradv.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);

                    if (HotelResult.Body != null)
                    {
                        if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;

                            CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                            objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                            System.Web.HttpContext.Current.Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                            objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                            objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                            objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                            System.Web.HttpContext.Current.Session["GDSCurrencyConversion"] = objCurrencyConversion;

                            CurrencyConversionList = APIUtility.GetCurrencyConversions(result);
                        }
                        string GDSCountry = Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]);
                        System.Web.HttpContext.Current.Session["GDSCountry"] = BLayer.BookingItem.GetGDSCountry(BookingID);
                        if ((Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") || (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]).ToLower() == "india"))
                        {
                            System.Web.HttpContext.Current.Session["GDSCurrencyConversion"] = null;
                            System.Web.HttpContext.Current.Session["GDSCountry"] = "";
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"])))
                        {

                            System.Web.HttpContext.Current.Session["GDSRateConversion"] = "0";
                            System.Web.HttpContext.Current.Session["GDSCurrencyCode"] = "INR";
                        }
                    }
                    else if (HotelResultAdvanced.Body != null)
                    {

                        if (HotelResultAdvanced.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResultAdvanced.Body.OTA_HotelAvailRS.CurrencyConversions;
                            CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                            objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                            System.Web.HttpContext.Current.Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                            objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                            objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                            objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                            System.Web.HttpContext.Current.Session["GDSCurrencyConversion"] = objCurrencyConversion;
                            CurrencyConversionList = APIUtility.GetCurrencyConversions(result);
                        }
                        if (Convert.ToString(Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"])) == "")
                        {
                            System.Web.HttpContext.Current.Session["GDSCurrencyConversion"] = null;
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]))))
                        {

                            System.Web.HttpContext.Current.Session["GDSRateConversion"] = "0";
                            System.Web.HttpContext.Current.Session["GDSCurrencyCode"] = "INR";
                        }

                    }



                    HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();
                    var RoomStayItemListAdvanced = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStays>();




                    System.Web.HttpContext.Current.Session["SessionId"] = HotelResult.Header.Session.SessionId;
                    System.Web.HttpContext.Current.Session["SequenceNumber"] = Convert.ToInt32(HotelResult.Header.Session.SequenceNumber);
                    System.Web.HttpContext.Current.Session["SecurityToken"] = HotelResult.Header.Session.SecurityToken;
                    #endregion

                    //#region PNR ADD MULTIELEMENTS-0               
                    string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, System.Web.HttpContext.Current.Session["SessionId"].ToString(), Convert.ToInt32(System.Web.HttpContext.Current.Session["SequenceNumber"]), System.Web.HttpContext.Current.Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                    List<CLayer.RoomStaysResult> RoomStaysResultList = null;
                    foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                    {
                        HotelItem = item;

                        string RoomStayRPH = item.RoomStayRPH;
                        RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                        if (!string.IsNullOrEmpty(RoomStayRPH))
                        {

                            string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                            RoomStaysResultList = SearchController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(System.Web.HttpContext.Current.Session["GDSCurrencyCode"]), Convert.ToDecimal(System.Web.HttpContext.Current.Session["GDSRateConversion"]));
                        }
                    }
                    AmedusRoomStaysResultList = RoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = AmedusRoomStaysResultList;
                    objResult = APIUtility.AmedusHotelRoomStaysResultList;

                }
            }
            catch (Exception ex)
            {
                objResult = null;
            }
            return objResult;
        }


        public ArrayList GetAccommodationSelected(string bookingData)
        {
            string[] sAccom = bookingData.Trim().Split('#');
            string AccommodationID = string.Empty;
            ArrayList AccList = new ArrayList();
            for (int i = 1; i <= sAccom.Length - 1; i++)
            {
                string[] AccomID = sAccom[i].Split(',');
                AccList.Add(AccomID[0]);
            }
            return AccList;
        }
        public List<CLayer.Rates> GetAmedusRatesForCredit(Models.SimpleBookingModel bookingData, List<CLayer.RoomStaysResult> RoomStaysResultList, string HotelCode, CLayer.Booking bdataCredit)
        {
            List<CLayer.Rates> rtes = new List<CLayer.Rates>();
            CLayer.Rates AmadeusRates = null;
            string result = "";
            long AccomSelect = 0;
            string BookingCode = "";

            string AgeQualifyingCode = "10";
            // string GuestCount = "2";

            int BedRoomsQuantity = Convert.ToInt32(bdataCredit.NoOfAccomodations);
            int GuestCount = Convert.ToInt32(bdataCredit.NoOfAdults) + Convert.ToInt32(bdataCredit.NoOfChildren); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 2 : GuestCount;

            //Customer state id
            int CustomerStateID = BLayer.State.GetCustomerStateID(Convert.ToInt32(bdataCredit.ByUserId));

            //Billing entity state id
            int BillingEntityStateID = BLayer.State.GetBillingEntityStateID(bookingData.PropertyId);
            BillingEntityStateID = (BillingEntityStateID == 0) ? Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARDEFAULTBILLINGENTITY)) : BillingEntityStateID;
            int TaxAddedPercent = 0;
            try
            {
                //Pricing details from enhanced pricing details API
                string SecurityToken = Convert.ToString(System.Web.HttpContext.Current.Session["SecurityToken"]);
                string SequenceNumber = Convert.ToString(System.Web.HttpContext.Current.Session["SequenceNumber"]);
                string SessionId = Convert.ToString(System.Web.HttpContext.Current.Session["SessionId"]);

                CLayer.EnhancedPricing.Envelope PricingResult = null;

                CLayer.EnhancedPricingAdvanced.Envelope PricingResultAdv = null;

                ArrayList AccommodationSelected = GetAccommodationSelected(bookingData.BookingData);

                var accom = BLayer.Accommodation.GetAllAccByPropertyid(bookingData.PropertyId);
                for (int i = 0; i < AccommodationSelected.Count; i++)
                {
                    AccomSelect = Convert.ToInt64(AccommodationSelected[i]);
                    BookingCode = accom.Where(x => x.AccommodationId == Convert.ToInt64(AccommodationSelected[i])).FirstOrDefault().BookingCode;
                    //  RoomStaysResultList = RoomStaysResultList.Where(x=>x.BookingCode==BookingCode).ToList();
                    foreach (var item in RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList())
                    {
                        AmadeusRates = new CLayer.Rates();
                        PropertyController objProperty = new PropertyController();
                        result = objProperty.GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, item.RoomTypeCode, HotelCode, item.RatePlanCode, item.BookingCode, bookingData.BookCheckIn.ToString("yyyy-MM-dd"), bookingData.BookCheckOut.ToString("yyyy-MM-dd"), "", AgeQualifyingCode, GuestCount.ToString(), bookingData.PropertyId);
                        string ErrorCode = APIUtility.GetHotelRateError(result);
                        if (ErrorCode == "696")
                        {
                            TempData["RateErrorCode"] = ErrorCode;
                        }

                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(bdataCredit.ByUserId), (int)CLayer.ObjectStatus.GDSType.HotelEnhancedPricing, 0);

                        #endregion Transaction log end

                        Serializer ser = new Serializer();

                        CLayer.EnhancedPricing.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItem = null;
                        CLayer.EnhancedPricingAdvanced.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItemAdv = null;
                        try
                        {
                            PricingResult = ser.Deserialize<CLayer.EnhancedPricing.Envelope>(result);
                            RatesItem = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                PricingResultAdv = ser.Deserialize<CLayer.EnhancedPricingAdvanced.Envelope>(result);
                                RatesItemAdv = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
                            }
                            catch (Exception innerex)
                            {
                                AmadeusRates = ReadGDSPrice(result, Convert.ToInt64(AccommodationSelected[i]), BookingCode, CustomerStateID, BillingEntityStateID);
                                rtes.Add(AmadeusRates);
                                return rtes;
                            }

                        }



                        if (RatesItem != null)
                        {
                            string RoomStayCurrencyCode = RatesItem.RoomRate.Total.CurrencyCode;
                            decimal RateConversion = Convert.ToDecimal(System.Web.HttpContext.Current.Session["GDSRateConversion"]);
                            string CurrencyCode = Convert.ToString(System.Web.HttpContext.Current.Session["GDSCurrencyCode"]);
                            decimal AmountAfterTax = APIUtility.GetBookingAmount(Convert.ToDecimal(RatesItem.RoomRate.Total.AmountAfterTax), RoomStayCurrencyCode, CurrencyCode, RateConversion);
                            RatesItem.RoomRate.Total.AmountAfterTax = AmountAfterTax;

                            AmadeusRates.NoofAcc = 10;
                            decimal TaxPercent = RatesItem.RoomRate.Total.Taxes.Tax.Percent;
                            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                            decimal TaxAmount = (RatesItem.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
                            AmadeusRates.Amount = RatesItem.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                            AmadeusRates.GDSAmount = AmadeusRates.Amount;
                            //    AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                            System.Web.HttpContext.Current.Session["GDSAmount"] = RatesItem.RoomRate.Total.AmountAfterTax;
                            System.Web.HttpContext.Current.Session["GDSConvertedAmount"] = AmadeusRates.Amount;

                            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                            AmadeusRates.taxpercent = TaxPercent;
                            decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            //AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            AmadeusRates.CGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            AmadeusRates.SGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            AmadeusRates.IGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
                            //  AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : SBTaxPercent;

                            TaxAddedPercent = Convert.ToInt32(AmadeusRates.CGSTTaxPercent + AmadeusRates.SGSTTaxPercent) + 100;
                            Decimal TaxExcludedAmount = AmadeusRates.Amount - TaxAmount;
                            //AmadeusRates.CGSTTaxPercent =  (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent =  (SBTaxPercent / 2);
                            //AmadeusRates.IGSTTaxPercent = SBTaxPercent;

                            AmadeusRates.BookingCode = BookingCode;

                            if (CustomerStateID == BillingEntityStateID)
                            {
                                TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;


                                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                                RatesItem.RoomRate.Total.AmountAfterTax = TaxExcludedAmount;

                                //  AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
                                //   AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));

                                AmadeusRates.SGSTTax = (AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
                                AmadeusRates.CGSTTax = (AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));


                                AmadeusRates.Amount = TaxExcludedAmount;// APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax)- (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                            }
                            else
                            {
                                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                                //   AmadeusRates.IGSTTax  = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                //    AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax);// - AmadeusRates.IGSTTax;
                                AmadeusRates.IGSTTax = ((AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                AmadeusRates.Amount = AmountAfterTax;// - AmadeusRates.IGSTTax;

                                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                            }



                        }
                        else if (RatesItemAdv != null)
                        {
                            string RoomStayCurrencyCode = RatesItemAdv.RoomRate.Total.CurrencyCode;
                            decimal RateConversion = Convert.ToDecimal(System.Web.HttpContext.Current.Session["GDSRateConversion"]);
                            string CurrencyCode = Convert.ToString(System.Web.HttpContext.Current.Session["GDSCurrencyCode"]);
                            decimal AmountAfterTax = APIUtility.GetBookingAmount(Convert.ToDecimal(RatesItemAdv.RoomRate.Total.AmountAfterTax), RoomStayCurrencyCode, CurrencyCode, RateConversion);
                            RatesItemAdv.RoomRate.Total.AmountAfterTax = AmountAfterTax;


                            AmadeusRates.NoofAcc = 10;
                            decimal TaxPercent = RatesItemAdv.RoomRate.Total.Taxes[0].Percent;
                            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                            decimal TaxAmount = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
                            AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                            AmadeusRates.GDSAmount = AmadeusRates.Amount;
                            //        AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                            System.Web.HttpContext.Current.Session["GDSAmount"] = RatesItemAdv.RoomRate.Total.AmountAfterTax;
                            System.Web.HttpContext.Current.Session["GDSConvertedAmount"] = AmadeusRates.Amount;

                            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                            AmadeusRates.taxpercent = TaxPercent;
                            decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            //AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            AmadeusRates.CGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            AmadeusRates.SGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            AmadeusRates.IGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            TaxAddedPercent = Convert.ToInt32(AmadeusRates.CGSTTaxPercent + AmadeusRates.SGSTTaxPercent) + 100;
                            Decimal TaxExcludedAmount = AmadeusRates.Amount - TaxAmount; //AmadeusRates.Amount * 100 / TaxAddedPercent;
                            //AmadeusRates.CGSTTaxPercent = (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent = (SBTaxPercent / 2);
                            //AmadeusRates.IGSTTaxPercent = SBTaxPercent;


                            AmadeusRates.BookingCode = BookingCode;

                            if (CustomerStateID == BillingEntityStateID)
                            {
                                TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;

                                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                                RatesItemAdv.RoomRate.Total.AmountAfterTax = TaxExcludedAmount;
                                //AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
                                //AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));
                                AmadeusRates.SGSTTax = (AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
                                AmadeusRates.CGSTTax = (AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));

                                AmadeusRates.Amount = TaxExcludedAmount;// APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) - (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                            }
                            else
                            {
                                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                                //AmadeusRates.IGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                //AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax);// - AmadeusRates.IGSTTax;
                                AmadeusRates.IGSTTax = ((AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                AmadeusRates.Amount = AmountAfterTax;// - AmadeusRates.IGSTTax;
                                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                            }


                            //}
                        }
                        if (PricingResult != null)
                        {
                            if (PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
                            {
                                AmadeusRates.GuarenteeCode = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();
                            }
                        }

                        if (PricingResultAdv != null)
                        {
                            if (PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
                            {
                                AmadeusRates.GuarenteeCode = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();

                            }
                        }

                        System.Web.HttpContext.Current.Session["GuaranteeCode"] = AmadeusRates.GuarenteeCode;

                        rtes.Add(AmadeusRates);
                    }
                }

                if (PricingResult != null)
                {
                    System.Web.HttpContext.Current.Session["SessionId"] = PricingResult.Header.Session.SessionId;
                    System.Web.HttpContext.Current.Session["SequenceNumber"] = Convert.ToInt32(PricingResult.Header.Session.SequenceNumber);
                    System.Web.HttpContext.Current.Session["SecurityToken"] = PricingResult.Header.Session.SecurityToken;
                }
                else
                {
                    System.Web.HttpContext.Current.Session["SessionId"] = PricingResultAdv.Header.Session.SessionId;
                    System.Web.HttpContext.Current.Session["SequenceNumber"] = Convert.ToInt32(PricingResultAdv.Header.Session.SequenceNumber);
                    System.Web.HttpContext.Current.Session["SecurityToken"] = PricingResultAdv.Header.Session.SecurityToken;
                }

            }
            catch (Exception ex)
            {
                AmadeusRates = ReadGDSPrice(result, AccomSelect, BookingCode, CustomerStateID, BillingEntityStateID);
                if (AmadeusRates != null)
                {
                    rtes.Add(AmadeusRates);
                }
                else
                {
                    rtes = null;
                }


            }
            return rtes;
        }

        public List<CLayer.Rates> GetAmedusRates(Models.SimpleBookingModel bookingData, List<CLayer.RoomStaysResult> RoomStaysResultList, string HotelCode)
        {
            List<CLayer.Rates> rtes = new List<CLayer.Rates>();
            CLayer.Rates AmadeusRates = null;
            string result = "";
            long AccomSelect = 0;
            string BookingCode = "";

            string AgeQualifyingCode = "10";
            // string GuestCount = "2";

            int BedRoomsQuantity = Convert.ToInt32(Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(Session["Adults"]) + Convert.ToInt32(Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 2 : GuestCount;

            //Customer state id
            int CustomerStateID = BLayer.State.GetCustomerStateID(Convert.ToInt32(Session["LoggedInUser"]));

            //Billing entity state id
            int BillingEntityStateID = BLayer.State.GetBillingEntityStateID(bookingData.PropertyId);
            BillingEntityStateID = (BillingEntityStateID == 0) ? Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARDEFAULTBILLINGENTITY)) : BillingEntityStateID;
            int TaxAddedPercent = 0;
            try
            {
                //Pricing details from enhanced pricing details API
                string SecurityToken = Convert.ToString(Session["SecurityToken"]);
                string SequenceNumber = Convert.ToString(Session["SequenceNumber"]);
                string SessionId = Convert.ToString(Session["SessionId"]);

                CLayer.EnhancedPricing.Envelope PricingResult = null;

                CLayer.EnhancedPricingAdvanced.Envelope PricingResultAdv = null;

                ArrayList AccommodationSelected = GetAccommodationSelected(bookingData.BookingData);

                var accom = BLayer.Accommodation.GetAllAccByPropertyid(bookingData.PropertyId);
                for (int i = 0; i < AccommodationSelected.Count; i++)
                {
                    AccomSelect = Convert.ToInt64(AccommodationSelected[i]);
                    BookingCode = accom.Where(x => x.AccommodationId == Convert.ToInt64(AccommodationSelected[i])).FirstOrDefault().BookingCode;
                    //  RoomStaysResultList = RoomStaysResultList.Where(x=>x.BookingCode==BookingCode).ToList();
                    foreach (var item in RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList())
                    {
                        AmadeusRates = new CLayer.Rates();
                        PropertyController objProperty = new PropertyController();
                        result = objProperty.GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, item.RoomTypeCode, HotelCode, item.RatePlanCode, item.BookingCode, bookingData.BookCheckIn.ToString("yyyy-MM-dd"), bookingData.BookCheckOut.ToString("yyyy-MM-dd"), "", AgeQualifyingCode, GuestCount.ToString(), bookingData.PropertyId);
                        string ErrorCode = APIUtility.GetHotelRateError(result);
                        if (ErrorCode == "696")
                        {
                            TempData["RateErrorCode"] = ErrorCode;
                        }

                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelEnhancedPricing, 0);

                        #endregion Transaction log end

                        Serializer ser = new Serializer();

                        CLayer.EnhancedPricing.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItem = null;
                        CLayer.EnhancedPricingAdvanced.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItemAdv = null;
                        try
                        {
                            PricingResult = ser.Deserialize<CLayer.EnhancedPricing.Envelope>(result);
                            RatesItem = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                PricingResultAdv = ser.Deserialize<CLayer.EnhancedPricingAdvanced.Envelope>(result);
                                RatesItemAdv = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
                            }
                            catch (Exception innerex)
                            {
                                AmadeusRates = ReadGDSPrice(result, Convert.ToInt64(AccommodationSelected[i]), BookingCode, CustomerStateID, BillingEntityStateID);
                                rtes.Add(AmadeusRates);
                                return rtes;
                            }

                        }



                        if (RatesItem != null)
                        {
                            string RoomStayCurrencyCode = RatesItem.RoomRate.Total.CurrencyCode;
                            decimal RateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                            string CurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                            decimal AmountAfterTax = APIUtility.GetBookingAmount(Convert.ToDecimal(RatesItem.RoomRate.Total.AmountAfterTax), RoomStayCurrencyCode, CurrencyCode, RateConversion);
                            RatesItem.RoomRate.Total.AmountAfterTax = AmountAfterTax;

                            AmadeusRates.NoofAcc = 10;
                            decimal TaxPercent = RatesItem.RoomRate.Total.Taxes.Tax.Percent;
                            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                            decimal TaxAmount = (RatesItem.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
                            AmadeusRates.Amount = RatesItem.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                            AmadeusRates.GDSAmount = AmadeusRates.Amount;
                            //    AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                            Session["GDSAmount"] = RatesItem.RoomRate.Total.AmountAfterTax;
                            Session["GDSConvertedAmount"] = AmadeusRates.Amount;

                            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                            AmadeusRates.taxpercent = TaxPercent;
                            decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            //AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            AmadeusRates.CGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            AmadeusRates.SGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            AmadeusRates.IGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
                            //  AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : SBTaxPercent;

                            TaxAddedPercent = Convert.ToInt32(AmadeusRates.CGSTTaxPercent + AmadeusRates.SGSTTaxPercent) + 100;
                            Decimal TaxExcludedAmount = AmadeusRates.Amount - TaxAmount;
                            //AmadeusRates.CGSTTaxPercent =  (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent =  (SBTaxPercent / 2);
                            //AmadeusRates.IGSTTaxPercent = SBTaxPercent;

                            AmadeusRates.BookingCode = BookingCode;

                            if (CustomerStateID == BillingEntityStateID)
                            {
                                TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;


                                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                                RatesItem.RoomRate.Total.AmountAfterTax = TaxExcludedAmount;

                                //  AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
                                //   AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));

                                AmadeusRates.SGSTTax = (AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
                                AmadeusRates.CGSTTax = (AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));


                                AmadeusRates.Amount = TaxExcludedAmount;// APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax)- (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                            }
                            else
                            {
                                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                                //   AmadeusRates.IGSTTax  = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                //    AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax);// - AmadeusRates.IGSTTax;
                                AmadeusRates.IGSTTax = ((AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                AmadeusRates.Amount = AmountAfterTax;// - AmadeusRates.IGSTTax;

                                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                            }



                        }
                        else if (RatesItemAdv != null)
                        {
                            string RoomStayCurrencyCode = RatesItemAdv.RoomRate.Total.CurrencyCode;
                            decimal RateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                            string CurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                            decimal AmountAfterTax = APIUtility.GetBookingAmount(Convert.ToDecimal(RatesItemAdv.RoomRate.Total.AmountAfterTax), RoomStayCurrencyCode, CurrencyCode, RateConversion);
                            RatesItemAdv.RoomRate.Total.AmountAfterTax = AmountAfterTax;


                            AmadeusRates.NoofAcc = 10;
                            decimal TaxPercent = RatesItemAdv.RoomRate.Total.Taxes[0].Percent;
                            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                            decimal TaxAmount = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
                            AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                            AmadeusRates.GDSAmount = AmadeusRates.Amount;
                            //        AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                            Session["GDSAmount"] = RatesItemAdv.RoomRate.Total.AmountAfterTax;
                            Session["GDSConvertedAmount"] = AmadeusRates.Amount;

                            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                            AmadeusRates.taxpercent = TaxPercent;
                            decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            //AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            AmadeusRates.CGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            AmadeusRates.SGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            AmadeusRates.IGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            TaxAddedPercent = Convert.ToInt32(AmadeusRates.CGSTTaxPercent + AmadeusRates.SGSTTaxPercent) + 100;
                            Decimal TaxExcludedAmount = AmadeusRates.Amount - TaxAmount; //AmadeusRates.Amount * 100 / TaxAddedPercent;
                            //AmadeusRates.CGSTTaxPercent = (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent = (SBTaxPercent / 2);
                            //AmadeusRates.IGSTTaxPercent = SBTaxPercent;


                            AmadeusRates.BookingCode = BookingCode;

                            if (CustomerStateID == BillingEntityStateID)
                            {
                                TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;

                                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                                RatesItemAdv.RoomRate.Total.AmountAfterTax = TaxExcludedAmount;
                                //AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
                                //AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));
                                AmadeusRates.SGSTTax = (AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
                                AmadeusRates.CGSTTax = (AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));

                                AmadeusRates.Amount = TaxExcludedAmount;// APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) - (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                            }
                            else
                            {
                                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                                //AmadeusRates.IGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                //AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax);// - AmadeusRates.IGSTTax;
                                AmadeusRates.IGSTTax = ((AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                AmadeusRates.Amount = AmountAfterTax;// - AmadeusRates.IGSTTax;
                                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                            }


                            //}
                        }
                        if (PricingResult != null)
                        {
                            if (PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
                            {
                                AmadeusRates.GuarenteeCode = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();
                            }
                        }

                        if (PricingResultAdv != null)
                        {
                            if (PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
                            {
                                AmadeusRates.GuarenteeCode = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();

                            }
                        }

                        Session["GuaranteeCode"] = AmadeusRates.GuarenteeCode;

                        rtes.Add(AmadeusRates);
                    }
                }

                if (PricingResult != null)
                {
                    Session["SessionId"] = PricingResult.Header.Session.SessionId;
                    Session["SequenceNumber"] = Convert.ToInt32(PricingResult.Header.Session.SequenceNumber);
                    Session["SecurityToken"] = PricingResult.Header.Session.SecurityToken;
                }
                else
                {
                    Session["SessionId"] = PricingResultAdv.Header.Session.SessionId;
                    Session["SequenceNumber"] = Convert.ToInt32(PricingResultAdv.Header.Session.SequenceNumber);
                    Session["SecurityToken"] = PricingResultAdv.Header.Session.SecurityToken;
                }

            }
            catch (Exception ex)
            {
                AmadeusRates = ReadGDSPrice(result, AccomSelect, BookingCode, CustomerStateID, BillingEntityStateID);
                if (AmadeusRates != null)
                {
                    rtes.Add(AmadeusRates);
                }
                else
                {
                    rtes = null;
                }


            }
            return rtes;
        }

        public List<CLayer.Rates> GetAmedusRates(Models.SimpleBookingModel bookingData, List<CLayer.RoomStaysResult> RoomStaysResultList, string HotelCode, bool IsMultiple)
        {
            List<CLayer.Rates> rtes = new List<CLayer.Rates>();
            CLayer.Rates AmadeusRates = null;
            string result = "";
            long AccomSelect = 0;
            string BookingCode = "";

            string AgeQualifyingCode = "10";
            // string GuestCount = "2";

            int BedRoomsQuantity = Convert.ToInt32(Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(Session["Adults"]) + Convert.ToInt32(Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 2 : GuestCount;

            //Customer state id
            int CustomerStateID = BLayer.State.GetCustomerStateID(Convert.ToInt32(Session["LoggedInUser"]));

            //Billing entity state id
            int BillingEntityStateID = BLayer.State.GetBillingEntityStateID(bookingData.PropertyId);
            BillingEntityStateID = (BillingEntityStateID == 0) ? Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARDEFAULTBILLINGENTITY)) : BillingEntityStateID;
            int TaxAddedPercent = 0;
            try
            {
                //Pricing details from enhanced pricing details API
                string SecurityToken = Convert.ToString(Session["SecurityToken"]);
                string SequenceNumber = Convert.ToString(Session["SequenceNumber"]);
                string SessionId = Convert.ToString(Session["SessionId"]);

                CLayer.EnhancedPricing.Envelope PricingResult = null;

                CLayer.EnhancedPricingAdvanced.Envelope PricingResultAdv = null;

                ArrayList AccommodationSelected = GetAccommodationSelected(bookingData.BookingData);

                var accom = BLayer.Accommodation.GetAllAccByPropertyid(bookingData.PropertyId);
                for (int i = 0; i < AccommodationSelected.Count; i++)
                {
                    AccomSelect = Convert.ToInt64(AccommodationSelected[i]);
                    BookingCode = accom.Where(x => x.AccommodationId == Convert.ToInt64(AccommodationSelected[i])).FirstOrDefault().BookingCode;
                    //  RoomStaysResultList = RoomStaysResultList.Where(x=>x.BookingCode==BookingCode).ToList();
                    foreach (var item in RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList())
                    {
                        AmadeusRates = new CLayer.Rates();
                        PropertyController objProperty = new PropertyController();
                        result = objProperty.GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, item.RoomTypeCode, HotelCode, item.RatePlanCode, item.BookingCode, bookingData.BookCheckIn.ToString("yyyy-MM-dd"), bookingData.BookCheckOut.ToString("yyyy-MM-dd"), "", AgeQualifyingCode, GuestCount.ToString(), bookingData.PropertyId);

                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelEnhancedPricing, 0);

                        #endregion Transaction log end

                        Serializer ser = new Serializer();

                        CLayer.EnhancedPricing.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItem = null;
                        CLayer.EnhancedPricingAdvanced.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItemAdv = null;
                        try
                        {
                            PricingResult = ser.Deserialize<CLayer.EnhancedPricing.Envelope>(result);
                            RatesItem = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                PricingResultAdv = ser.Deserialize<CLayer.EnhancedPricingAdvanced.Envelope>(result);
                                RatesItemAdv = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
                            }
                            catch (Exception innerex)
                            {
                                AmadeusRates = ReadGDSPrice(result, Convert.ToInt64(AccommodationSelected[i]), BookingCode, CustomerStateID, BillingEntityStateID);
                                rtes.Add(AmadeusRates);
                                return rtes;
                            }

                        }



                        if (RatesItem != null)
                        {
                            string RoomStayCurrencyCode = RatesItem.RoomRate.Total.CurrencyCode;
                            decimal RateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                            string CurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                            decimal AmountAfterTax = APIUtility.GetBookingAmount(Convert.ToDecimal(RatesItem.RoomRate.Total.AmountAfterTax), RoomStayCurrencyCode, CurrencyCode, RateConversion);
                            RatesItem.RoomRate.Total.AmountAfterTax = AmountAfterTax;

                            AmadeusRates.NoofAcc = 10;
                            decimal TaxPercent = RatesItem.RoomRate.Total.Taxes.Tax.Percent;
                            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                            decimal TaxAmount = (RatesItem.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
                            AmadeusRates.Amount = RatesItem.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                            AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                            Session["GDSAmount"] = RatesItem.RoomRate.Total.AmountAfterTax;
                            Session["GDSConvertedAmount"] = AmadeusRates.Amount;

                            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                            AmadeusRates.taxpercent = TaxPercent;
                            decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                            TaxAddedPercent = Convert.ToInt32(SBTaxPercent) + 100;
                            Decimal TaxExcludedAmount = AmadeusRates.Amount * 100 / TaxAddedPercent;
                            AmadeusRates.CGSTTaxPercent = (SBTaxPercent / 2);
                            AmadeusRates.SGSTTaxPercent = (SBTaxPercent / 2);
                            AmadeusRates.IGSTTaxPercent = SBTaxPercent;
                            AmadeusRates.BookingCode = BookingCode;

                            if (CustomerStateID == BillingEntityStateID)
                            {
                                TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;

                                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                                RatesItem.RoomRate.Total.AmountAfterTax = TaxExcludedAmount;
                                AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
                                AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));
                                AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) - (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                            }
                            else
                            {
                                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                                AmadeusRates.IGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) - AmadeusRates.IGSTTax;
                                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                            }



                        }
                        else if (RatesItemAdv != null)
                        {
                            string RoomStayCurrencyCode = RatesItemAdv.RoomRate.Total.CurrencyCode;
                            decimal RateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                            string CurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                            decimal AmountAfterTax = APIUtility.GetBookingAmount(Convert.ToDecimal(RatesItemAdv.RoomRate.Total.AmountAfterTax), RoomStayCurrencyCode, CurrencyCode, RateConversion);
                            RatesItemAdv.RoomRate.Total.AmountAfterTax = AmountAfterTax;


                            AmadeusRates.NoofAcc = 10;
                            decimal TaxPercent = RatesItemAdv.RoomRate.Total.Taxes[0].Percent;
                            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                            decimal TaxAmount = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
                            AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                            AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                            Session["GDSAmount"] = RatesItemAdv.RoomRate.Total.AmountAfterTax;
                            Session["GDSConvertedAmount"] = AmadeusRates.Amount;

                            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                            AmadeusRates.taxpercent = TaxPercent;
                            decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);


                            TaxAddedPercent = Convert.ToInt32(SBTaxPercent) + 100;
                            Decimal TaxExcludedAmount = AmadeusRates.Amount * 100 / TaxAddedPercent;
                            AmadeusRates.CGSTTaxPercent = (SBTaxPercent / 2);
                            AmadeusRates.SGSTTaxPercent = (SBTaxPercent / 2);
                            AmadeusRates.IGSTTaxPercent = SBTaxPercent;
                            AmadeusRates.BookingCode = BookingCode;

                            if (CustomerStateID == BillingEntityStateID)
                            {

                                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                                RatesItemAdv.RoomRate.Total.AmountAfterTax = TaxExcludedAmount;
                                AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
                                AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));
                                AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) - (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                            }
                            else
                            {
                                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                                AmadeusRates.IGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) - AmadeusRates.IGSTTax;
                                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                            }


                            //}
                        }
                        if (PricingResult != null)
                        {
                            if (PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
                            {
                                AmadeusRates.GuarenteeCode = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();
                            }
                        }

                        if (PricingResultAdv != null)
                        {
                            if (PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
                            {
                                AmadeusRates.GuarenteeCode = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();

                            }
                        }

                        Session["GuaranteeCode"] = AmadeusRates.GuarenteeCode;

                        rtes.Add(AmadeusRates);
                    }
                }

                if (PricingResult != null)
                {
                    Session["SessionId"] = PricingResult.Header.Session.SessionId;
                    Session["SequenceNumber"] = Convert.ToInt32(PricingResult.Header.Session.SequenceNumber);
                    Session["SecurityToken"] = PricingResult.Header.Session.SecurityToken;
                }
                else
                {
                    Session["SessionId"] = PricingResultAdv.Header.Session.SessionId;
                    Session["SequenceNumber"] = Convert.ToInt32(PricingResultAdv.Header.Session.SequenceNumber);
                    Session["SecurityToken"] = PricingResultAdv.Header.Session.SecurityToken;
                }

            }
            catch (Exception ex)
            {
                AmadeusRates = ReadGDSPrice(result, AccomSelect, BookingCode, CustomerStateID, BillingEntityStateID);
                if (AmadeusRates != null)
                {
                    rtes.Add(AmadeusRates);
                }
                else
                {
                    rtes = null;
                }


            }
            return rtes;
        }
        private CLayer.Rates ReadGDSPrice(string xml, long AccommodationSelected, string BookingCode, int CustomerStateID, int BillingEntityStateID)
        {

            XmlDocument xmlDoc = new XmlDocument();


            string Message = string.Empty;
            CLayer.Rates AmadeusRates = new CLayer.Rates();
            try
            {


                xmlDoc.LoadXml(xml);

                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XmlNode node = null;
                XmlNode nodeTax = null;
                XmlNode nodeTotal = null;
                string nodeRoot = "/soapenv:Envelope/soapenv:Body/";
                string nodeRate = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RoomRates";
                string nodetotal = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RoomRates/si:RoomRate/si:Total";
                string nodevalue = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RoomRates/si:RoomRate/si:Total/si:Taxes/si:Tax";
                string nodeGuaranteevalue = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RatePlans/si:RatePlan/si:Guarantee";

                string nodeError = "/si:errorGroup/si:errorDescription/si:freeText";
                string nodeWarning = "/si:generalErrorInfo/si:errorWarningDescription";

                xmlnsManager.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                xmlnsManager.AddNamespace("awsse", "http://xml.amadeus.com/2010/06/Session_v3");
                xmlnsManager.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");


                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
                nodeTax = xmlDoc.SelectSingleNode(nodeRoot + nodevalue, xmlnsManager);
                node = xmlDoc.SelectSingleNode(nodeRoot + nodeRate, xmlnsManager);
                if (node == null)
                {
                    AmadeusRates = null;
                    return AmadeusRates;
                }
                nodeTotal = xmlDoc.SelectSingleNode(nodeRoot + nodetotal, xmlnsManager);

                XmlNodeList xNodeList = node.SelectNodes(nodeRoot + nodevalue, xmlnsManager);
                XmlNode nodeguarantee = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RatePlans/si:RatePlan/si:Guarantee/@GuaranteeCode", xmlnsManager);

                //decimal TaxPercent = 0;
                //decimal AmountAfterTax = 0;
                //decimal AmountBeforeTax = 0;
                string GuarenteeCode = "";
                if (nodeguarantee != null)
                {
                    GuarenteeCode = GetXmlChildNodes(nodeguarantee, "GuaranteeCode");
                }



                if (node != null)
                {

                    AmadeusRates.NoofAcc = 10;

                    List<CLayer.GDSRateTaxes> objTaxesList = new List<CLayer.GDSRateTaxes>();
                    foreach (XmlNode xNode in xNodeList)
                    {
                        CLayer.GDSRateTaxes objTaxes = new CLayer.GDSRateTaxes();

                        objTaxes.Percent = GetXmlChildNodes(xNode, "Percent");
                        objTaxes.Type = GetXmlChildNodes(xNode, "Type");
                        objTaxes.Code = GetXmlChildNodes(xNode, "Code");
                        objTaxes.ChargeUnit = GetXmlChildNodes(xNode, "ChargeUnit");
                        objTaxesList.Add(objTaxes);
                    }

                    AmadeusRates.TaxesList = objTaxesList;
                    string Amount = GetXmlChildNodes(nodeTotal, "AmountAfterTax");
                    //      string Amountbeforetax = GetXmlChildNodes(nodeTotal, "AmountBeforeTax");
                    decimal AmountAfterTax = Convert.ToDecimal(Amount);

                    // AmountAfterTax = (Convert.ToString(Session["GDSCountry"]) == "") ? AmountAfterTax : APIUtility.GetGDSConvertedAmount(AmountAfterTax);

                    string RoomStayCurrencyCode = GetXmlChildNodes(nodeTotal, "CurrencyCode");
                    decimal RateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                    string CurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                    AmountAfterTax = APIUtility.GetBookingAmount(AmountAfterTax, RoomStayCurrencyCode, CurrencyCode, RateConversion);




                    AmadeusRates.Amount = (Amount == "") ? 0 : AmountAfterTax;
                    AmadeusRates.GDSAmount = AmadeusRates.Amount;
                    //      AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                    Session["GDSAmount"] = AmountAfterTax;
                    Session["GDSConvertedAmount"] = AmadeusRates.Amount;
                    decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                    decimal CGSTPercent = 0;
                    decimal SGSTPercent = 0;
                    decimal IGSTPercent = 0;
                    if (objTaxesList.Count > 1)
                    {
                        CGSTPercent = (objTaxesList[0].Percent == "") ? 0 : Math.Round(Convert.ToDecimal(objTaxesList[0].Percent), 0);
                        SGSTPercent = (objTaxesList[1].Percent == "") ? 0 : Math.Round(Convert.ToDecimal(objTaxesList[1].Percent), 0);
                        IGSTPercent = (objTaxesList[0].Percent == "") ? 0 : Math.Round(Convert.ToDecimal(objTaxesList[0].Percent), 0);
                    }


                    AmadeusRates.NoofAcc = 10;
                    decimal TaxPercent = Convert.ToDecimal(objTaxesList[0].Percent);// RatesItemAdv.RoomRate.Total.Taxes[0].Percent;
                    TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                    decimal TaxAmount = (AmadeusRates.Amount * (TaxPercent / 100));
                    //AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                    AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                                                                                          //        AmadeusRates.taxpercent = TaxPercent;

                    AmadeusRates.CGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                    AmadeusRates.SGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                    AmadeusRates.IGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                    int TaxAddedPercent = Convert.ToInt32(AmadeusRates.CGSTTaxPercent + AmadeusRates.SGSTTaxPercent) + 100;
                    Decimal TaxExcludedAmount = Convert.ToDecimal(AmountAfterTax) - TaxAmount; //AmadeusRates.Amount * 100 / TaxAddedPercent;


                    //AmadeusRates.CGSTTaxPercent = (SBTaxPercent / 2);
                    //AmadeusRates.SGSTTaxPercent = (SBTaxPercent / 2);
                    //AmadeusRates.IGSTTaxPercent = SBTaxPercent;

                    AmadeusRates.BookingCode = BookingCode;

                    if (CustomerStateID == BillingEntityStateID)
                    {
                        TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;

                        AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                        AmountAfterTax = TaxExcludedAmount;
                        AmadeusRates.SGSTTax = (AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
                        AmadeusRates.CGSTTax = (AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));
                        AmadeusRates.Amount = TaxExcludedAmount;// AmountAfterTax -(AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                        AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                    }
                    else
                    {
                        AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                        AmadeusRates.IGSTTax = (AmountAfterTax * (AmadeusRates.IGSTTaxPercent / 100));
                        AmadeusRates.Amount = AmountAfterTax; // - AmadeusRates.IGSTTax;
                        AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                    }


                    //}
                }
                AmadeusRates.GuarenteeCode = GuarenteeCode;
                Session["GuaranteeCode"] = AmadeusRates.GuarenteeCode;

            }
            catch (Exception ex)
            {
                AmadeusRates = null;
                throw ex;
            }
            return AmadeusRates;
        }
        private string GetXmlChildNodes(XmlNode xNode, String pField)
        {
            string Result = string.Empty;
            try
            {
                if ((xNode.Attributes["" + pField + ""].HasChildNodes))
                {
                    Result = xNode.Attributes["" + pField + ""].Value;
                }

            }
            catch (Exception ex)
            {
                Result = string.Empty;
            }
            return Result;

        }
        public List<CLayer.RoomStaysResult> GetRoomStayResult(long PropertyID, DateTime CheckIn, DateTime CheckOut)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
            // Checking Hotelid,roomid available

            try
            {
                string HotelId = BLayer.Property.GetHotelId(PropertyID);


                //Get accommodations amedus

                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                srch.HotelCode = HotelId;
                srch.CheckIn = CheckIn;
                srch.CheckOut = CheckOut;

                string result = SearchController.HotelMultiSingleAvailability(srch, false, HotelId);

                Serializer ser = new Serializer();
                CLayer.HotelAvailability.Envelope HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);

                string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION);



                long AmadeusPropertyID = 0;
                string CityName = string.Empty;
                var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();
                Session["SessionId"] = HotelResult.Header.Session.SessionId;
                Session["SequenceNumber"] = Convert.ToInt32(HotelResult.Header.Session.SequenceNumber);
                Session["SecurityToken"] = HotelResult.Header.Session.SecurityToken;

                string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                RoomStaysResultList = null;
                foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                {
                    HotelItem = item;

                    string RoomStayRPH = item.RoomStayRPH;
                    RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                    if (!string.IsNullOrEmpty(RoomStayRPH))
                    {

                        string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                        RoomStaysResultList = SearchController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList);
                    }
                }
            }
            catch (Exception ex)
            {
                RoomStaysResultList = null;
            }


            // ViewBag.AmedusRoomsList = RoomStaysResultList.Count;


            return RoomStaysResultList;
        }
        public Models.BookingModel Load()
        {
            Models.BookingModel data = new Models.BookingModel();
            long userId = GetUserId();

            long bookingId = Convert.ToInt64(TempData["BookingID"]);
            TempData.Peek("BookingID");

            if (Session != null && Session["CorpBookingID"] != null && Session["CorpBookingID"].ToString() != "")
            {
                if (Convert.ToInt64(Session["CorpBookingID"]) > 0)
                {
                    bookingId = Convert.ToInt64(Session["CorpBookingID"]);
                    Session["CorpBookingID"] = null;
                }
                //fire code
            }


            if (bookingId <= 0)
            {
                bookingId = BLayer.Bookings.GetCartId(userId);
            }
            //   bookingId = BLayer.Bookings.GetCartId(userId);
            data.BookingId = bookingId;
            data = LoadBilling(bookingId);
            //data.Items = BLayer.BookingItem.GetAllDetails(bookingId);
            //CLayer.Booking bdata = BLayer.Bookings.GetDetails(bookingId);
            //data.BookingDetails.OrderNo = bdata.OrderNo;
            //data.BookingDetails.BookingDate = bdata.BookingDate;
            //data.Supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
            //data.BookingId = bookingId;
            if (bookingId > 0)
            {
                List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
                if (adr.Count > 0)
                {
                    data.OrderedBy = adr[0];
                }
                else
                {
                    data.OrderedBy = new CLayer.Address();

                }
                //  data.Items = BLayer.BookingItem.GetAllUnderCart(userId);
            }
            return data;
        }

        public void CancelOrder()
        {
            long userId = GetUserId();
            if (userId < 1) return;
            BLayer.Bookings.ClearCart(userId);
        }
        public void CancelOrderbyCredit(long userId)
        {
            if (userId < 1) return;
            BLayer.Bookings.ClearCart(userId);
        }

        #endregion
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] != null)
                    {
                        Models.BookingModel data = Load();
                        long userid = GetUserId();
                        string BookingForValue = BLayer.BookingItem.GetBookingFor(data.BookingId);
                        string BillingForValue = BLayer.BookingItem.GetBillingFor(data.BookingId);
                        long Assisted_By = BLayer.BookingItem.GetAssisted_By(data.BookingId);
                        if (BookingForValue != "S" && BillingForValue != "P")
                        {
                            if (BookingForValue == "A" && (BillingForValue == "B" || BillingForValue == "P"))
                            {
                                if (IsBookingApprovalNeeded(Assisted_By, data.BookingId))
                                {
                                    ViewBag.IsApproverNeeded = true;
                                }
                                else
                                {
                                    ViewBag.IsApproverNeeded = false;
                                }
                            }
                            else
                            {
                                if (IsBookingApprovalNeeded(userid, data.BookingId))
                                {
                                    ViewBag.IsApproverNeeded = true;
                                }
                                else
                                {
                                    ViewBag.IsApproverNeeded = false;
                                }
                            }

                        }
                        else
                        {
                            ViewBag.IsApproverNeeded = false;
                        }

                        if (BLayer.B2B.IsCreditBookingNeeded(userid))
                        {
                            ViewBag.IsCreditBookingNeeded = true;
                        }
                        else
                        {
                            ViewBag.IsCreditBookingNeeded = false;
                        }

                        return View(data);
                    }
                }
                else
                {
                    //  BLayer.Bookings.GetCartIdAfterCleaning(GetUserId());
                    // the above line was to clear cart after timeout
                    // to keep the shopping history available it has been commented
                    Models.BookingModel data = Load();
                    string BookingForValue = BLayer.BookingItem.GetBookingFor(data.BookingId);
                    string BillingForValue = BLayer.BookingItem.GetBillingFor(data.BookingId);
                    long Assisted_By = BLayer.BookingItem.GetAssisted_By(data.BookingId);
                    long userid = GetUserId();
                    if (BookingForValue != "S" && BillingForValue != "P")
                    {
                        if (BookingForValue == "A" && (BillingForValue == "B" || BillingForValue == "P"))
                        {
                            if (IsBookingApprovalNeeded(Assisted_By, data.BookingId))
                            {
                                ViewBag.IsApproverNeeded = true;
                            }
                            else
                            {
                                ViewBag.IsApproverNeeded = false;
                            }
                        }
                        else
                        {
                            if (IsBookingApprovalNeeded(userid, data.BookingId))
                            {
                                ViewBag.IsApproverNeeded = true;
                            }
                            else
                            {
                                ViewBag.IsApproverNeeded = false;
                            }
                        }

                    }
                    else
                    {
                        ViewBag.IsApproverNeeded = false;
                    }


                    if (BLayer.B2B.IsCreditBookingNeeded(userid))
                    {
                        ViewBag.IsCreditBookingNeeded = true;
                    }
                    else
                    {
                        ViewBag.IsCreditBookingNeeded = false;
                    }

                    return View(data);
                }
                //  BLayer.Bookings.GetCartIdAfterCleaning(GetUserId());
                string rurl = Url.Action("Index", "Booking");
                return RedirectToAction("Index", "Continue", new { ReturnUrl = rurl });
            }
            catch (Exception ex)
            { Common.LogHandler.HandleError(ex); }
            return View(Load());
        }

        public bool IsBookingApprovalNeeded(long userId, long bookingId)
        {
            bool IsApproverNeeded = false;
            int NoofApprovers = BLayer.B2BUser.GetNoofApprovers(userId);
            if (NoofApprovers > 0)
            {
                CLayer.B2BApprovers pb2b_Approvers = BLayer.B2BUser.CheckBookingApproversExist(userId, bookingId);

                if (pb2b_Approvers.b2b_approver_id > 0)
                {
                    IsApproverNeeded = false;
                }
                else
                {
                    IsApproverNeeded = true;
                }


                //if ((pb2b_Approvers.b2b_approver_id == 0) && (pb2b_Approvers.approver_order > 0) && (pb2b_Approvers.status == 0))
                //{
                //    //   bool mailSend = await SendMailsAndSmsForCorporateBookingForApproval(bookingId);
                //    TempData["ApproverName"] = pb2b_Approvers.username;
                //    TempData["ApproverEmail"] = pb2b_Approvers.approver_email;
                //    IsApproverNeeded = true;
                //    //    return View("~/Views/Error/BookingApprovalAlert.cshtml");
                //    //   return RedirectToAction("BookingApprovalAlert", "ErrorPage", pb2b_Approvers);
                //}
                //else if ((pb2b_Approvers.b2b_approver_id > 0) && (pb2b_Approvers.approver_order > 0) && (pb2b_Approvers.status < 2))
                //{
                //    //  bool mailSend = await SendMailsAndSmsForCorporateBookingForApproval(bookingId);
                //    //  return RedirectToAction("BookingApprovalAlert", "ErrorPage", pb2b_Approvers);
                //    TempData["ApproverName"] = pb2b_Approvers.username;
                //    TempData["ApproverEmail"] = pb2b_Approvers.approver_email;
                //    IsApproverNeeded = true;

                //    // return View("~/Views/ErrorPage/BookingApprovalAlert.cshtml");
                //}
            }
            else
            {
                IsApproverNeeded = false;
            }


            return IsApproverNeeded;
        }


        public async Task<ActionResult> offlineproceed(long BookingId)
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Offline, BookingId);
                CLayer.User userdt = new CLayer.User();
                try
                {
                    long userId = BLayer.Bookings.GetBookedByUserId(BookingId);
                    //long offlinebookinguserId = BLayer.Bookings.GetOfflineBookedByUserId(BookingId);
                    userdt = BLayer.User.Get(userId);
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }

                string message = "";
                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("OfflinebookingQuery") + BookingId.ToString());
                MailMessage mm1 = new MailMessage();

                if (userdt != null)
                {
                    if (userdt.Email != null && userdt.Email != "")
                    {
                        mm1.To.Add(userdt.Email);
                    }
                }


                //string emailidsto1 = ConfigurationManager.AppSettings.Get("OfflineProceedMailTo");
                //if (emailidsto1 != "")
                //{
                //    string[] emailsto1 = emailidsto1.Split(',');
                //    for (int i = 0; i < emailsto1.Length; ++i)
                //    {
                //        mm1.To.Add(emailsto1[i]);
                //    }
                //}

                string emailidsbcc1 = ConfigurationManager.AppSettings.Get("OfflineProceedMailBcc");
                if (emailidsbcc1 != "")
                {
                    string[] emailsbcc1 = emailidsbcc1.Split(',');
                    for (int i = 0; i < emailsbcc1.Length; ++i)
                    {
                        mm1.CC.Add(emailsbcc1[i]);
                    }
                }

                mm1.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                mm1.Subject = "Booking Query";
                mm1.Body = message;
                mm1.IsBodyHtml = true;
                Common.Mailer mlr1 = new Common.Mailer();

                try
                {
                    await mlr1.SendMailAsyncWithoutFields(mm1);
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View();
        }

        public ActionResult ForBookingDisplay(long ForBookingUserId, long BookingItemId)
        {
            long userId = GetUserId();
            Models.BookingForUserModel search = new Models.BookingForUserModel();
            // List<CLayer.BookingItem> forbookingdata = BLayer.Bookings.GetForbookingList(userId);

            List<CLayer.BookingItem> forbookingdata = BLayer.Bookings.GetAllGuestForbookingList(userId);
            search.Items = forbookingdata;
            search.TotalRows = 0;
            if (search.Items.Count > 0)
            {
                search.TotalRows = forbookingdata[0].TotalRows;
            }
            search.Limit = 25;

            //    search.currentPage = 1;
            //    ViewBag.Filter = search;
            //    return PartialView("_ForBookingList", search);
            //}

            search.currentPage = 1;
            search.UserId = userId;
            ViewBag.Filter = search;

            //For get user address
            CLayer.Address adr = BLayer.Address.GetAddressOnUserId(userId);
            search.ForUserFirstName = adr.Firstname;
            search.ForUserLastName = adr.Lastname;
            search.ForUserEmail = adr.Email;
            search.ForUserMobile = adr.Mobile;
            search.ForUserAddress = adr.AddressText;
            search.ForCity = adr.City;
            search.ForState = adr.StateName;
            search.ForCountry = adr.Country;
            search.ZipCode = adr.ZipCode;

            return PartialView("_ForBookingList", search);
        }
        public ActionResult UpdateBookingforUser(long? UserId)
        {
            try
            {
                long userId = GetUserId();

                long bookingId = BLayer.Bookings.GetCartId(userId);
                BLayer.Bookings.UpdateBookingforUser(UserId.Value, bookingId);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");

        }

        public ActionResult UpdateBooking(long? ForBookingUserId)
        {
            try
            {
                long userId = GetUserId();

                long bookingId = BLayer.Bookings.GetCartId(userId);
                BLayer.Bookings.UpdateBooking(ForBookingUserId.Value, bookingId);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");

        }

        public ActionResult DeleteForUser(long? BookingUserId)
        {
            try
            {
                //BLayer.Bookings.DeleteForUser(BookingUserId.Value);

                long userId = GetUserId();

                long bookingId = BLayer.Bookings.GetCartId(userId);
                BLayer.Bookings.DeleteForUser(BookingUserId.Value, userId, bookingId);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [AllowAnonymous]
        public ActionResult Book(Models.SimpleBookingModel data, Models.PropertyModel model)
        {
            try
            {
                if (Convert.ToInt64(Request["BookingID"]) > 0)
                {
                    Session["CorpBookingID"] = Convert.ToInt64(Request["BookingID"]);
                }

                //if (!User.Identity.IsAuthenticated)
                if (!User.Identity.IsAuthenticated)
                {
                    if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] == null)
                    {
                        TempData["Bookingdata"] = data;
                        return RedirectToAction("Index", "Continue");
                    }
                    else if ((data.BookingData != null) && (data.PropertyId > 0))
                    {
                        //string[] BookDat = BookingData.Split('#');
                        Models.SimpleBookingModel datatemp = new Models.SimpleBookingModel();
                        datatemp.BookingData = data.BookingData;
                        datatemp.BookCheckIn = data.BookCheckIn;
                        datatemp.BookCheckOut = data.BookCheckOut;
                        datatemp.PropertyId = data.PropertyId;
                        data = datatemp;
                    }
                    else
                    {
                        Models.SimpleBookingModel datatemp = new Models.SimpleBookingModel();
                        datatemp.BookingData = Convert.ToString(Session["BookingData"]);
                        datatemp.BookCheckIn = Convert.ToDateTime(Session["BookCheckIn"]);
                        datatemp.BookCheckOut = Convert.ToDateTime(Session["BookCheckOut"]);
                        datatemp.PropertyId = Convert.ToInt64(Session["PropertyId"]);
                        data = datatemp;
                    }
                }
                else if ((data.PropertyId <= 0) || (string.IsNullOrEmpty(data.BookingData)))
                {
                    Models.SimpleBookingModel datatemp = new Models.SimpleBookingModel();
                    datatemp.BookingData = Convert.ToString(Session["BookingData"]);
                    datatemp.BookCheckIn = Convert.ToDateTime(Session["BookCheckIn"]);
                    datatemp.BookCheckOut = Convert.ToDateTime(Session["BookCheckOut"]);
                    datatemp.PropertyId = Convert.ToInt64(Session["PropertyId"]);
                    data = datatemp;

                    if ((datatemp.PropertyId <= 0) && (Convert.ToInt64(Request["BookingID"]) > 0))
                    {
                        long BuserId = BLayer.Bookings.GetBookedByUserId(Convert.ToInt64(Request["BookingID"]));
                        CLayer.Booking bdataCredit = BLayer.Bookings.GetDataForCreditBooking(Convert.ToInt64(Request["BookingID"]));
                        string BookingSelected = "," + bdataCredit.NoOfAccomodations.ToString() + "," + bdataCredit.NoOfAdults.ToString() + "," + bdataCredit.NoOfChildren.ToString() + "," + bdataCredit.NoOfGuests.ToString();

                        datatemp = new Models.SimpleBookingModel();
                        datatemp.BookingData = "#" + bdataCredit.AccommodationId.ToString() + BookingSelected;
                        datatemp.BookCheckIn = bdataCredit.CheckIn;
                        datatemp.BookCheckOut = bdataCredit.CheckOut;
                        datatemp.PropertyId = bdataCredit.PropertyId;
                        data = datatemp;
                    }
                }

                Session["LoggedInUser"] = GetUserId();
                //Amedus booking
                TempData["Amedusdata"] = data;
                TempData.Keep("Amedusdata");

                string CancelDays = BLayer.Settings.GetValue(CLayer.Settings.PARTIAL_PAYMENT_SECOND_INSTALLMENT);
                DateTime CancellationDate = data.BookCheckIn - TimeSpan.FromDays(Convert.ToInt32(CancelDays));
                TempData["CancellationDate"] = CancellationDate.ToString("dd-MM-yyyy");


                //check inventory api 
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(data.PropertyId);
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    #region LIVE HOTEL SEARCH
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
                    string HotelId = BLayer.Property.GetHotelId(data.PropertyId);
                    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                    srch.HotelCode = HotelId;
                    srch.CheckIn = data.BookCheckIn;
                    srch.CheckOut = data.BookCheckOut;

                    string result = SearchController.HotelMultiSingleAvailability(srch, false, HotelId);

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailabilityLiveSearch, 0);

                    #endregion Transaction log end

                    Serializer ser = new Serializer();
                    HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);

                    Serializer seradv = new Serializer();
                    HotelResultAdvanced = seradv.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);

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
                        if ((Convert.ToString(Session["GDSCountry"]) == "") || (Convert.ToString(Session["GDSCountry"]).ToLower() == "india"))
                        {
                            Session["GDSCurrencyConversion"] = null;
                            Session["GDSCountry"] = "";
                        }
                        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                        {

                            Session["GDSRateConversion"] = "0";
                            Session["GDSCurrencyCode"] = "INR";
                        }
                    }
                    else if (HotelResultAdvanced.Body != null)
                    {

                        if (HotelResultAdvanced.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResultAdvanced.Body.OTA_HotelAvailRS.CurrencyConversions;
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
                        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                        {

                            Session["GDSRateConversion"] = "0";
                            Session["GDSCurrencyCode"] = "INR";
                        }

                    }



                    HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();
                    var RoomStayItemListAdvanced = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStays>();




                    Session["SessionId"] = HotelResult.Header.Session.SessionId;
                    Session["SequenceNumber"] = Convert.ToInt32(HotelResult.Header.Session.SequenceNumber);
                    Session["SecurityToken"] = HotelResult.Header.Session.SecurityToken;
                    #endregion

                    //#region PNR ADD MULTIELEMENTS-0               
                    string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                    List<CLayer.RoomStaysResult> RoomStaysResultList = null;
                    foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                    {
                        HotelItem = item;

                        string RoomStayRPH = item.RoomStayRPH;
                        RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                        if (!string.IsNullOrEmpty(RoomStayRPH))
                        {

                            string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                            RoomStaysResultList = SearchController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                        }
                    }
                    AmedusRoomStaysResultList = RoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = AmedusRoomStaysResultList;

                    //    string BookingCode = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                    //    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCode);

                    //    result = APIUtility.GetAmedusResult(Action,APIUtility.SoapHeaderStateful, PNRADDSoapBody);

                    //    Serializer pnrser = new Serializer();

                    //     CLayer.PNRAddElements.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElements.Envelope>(result);
                    //#endregion


                }

                //if (InventoryAPIType == 5 || InventoryAPIType == 4)
                if (InventoryAPIType == 4)
                {
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
                    string HotelId = BLayer.Property.GetHotelId(data.PropertyId);
                    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                    srch.HotelCode = HotelId;
                    srch.CheckIn = data.BookCheckIn;
                    srch.CheckOut = data.BookCheckOut;

                    string result = TamrindMultiSingleAvailability(srch);

                    DataSet lds_auth = new DataSet();

                    lds_auth.ReadXml(GenerateStreamFromString(result));
                    long userId = GetUserId();
                    decimal price = 0.0M;
                    decimal amount = 0.0M;

                    //int EntityFlag = 0;
                    //decimal lf_SGST = 0;
                    //decimal lf_CGST = 0;
                    //decimal lf_IGST = 0;

                    //EntityFlag = BLayer.SBEntity.SBEntityByStateId(StateID);
                    //string lsz_hsn_code = "";
                    //if (EntityFlag > 0)
                    //{
                    //    lsz_hsn_code = "996311";
                    //    ldt_sgst_flag = 1;
                    //}
                    //else
                    //{
                    //    lsz_hsn_code = "998552";
                    //    if (srch.LoggedInUser != 0)
                    //    {
                    //        CusStateId = BLayer.Address.GetStatidOnUserId(srch.LoggedInUser);
                    //    }
                    //    else
                    //    {
                    //        CusStateId = BLayer.Address.GetStatidOnUserId(Convert.ToInt64(User.Identity.GetUserId()));
                    //    }
                    //    EntityFlag = BLayer.SBEntity.SBEntityByStateId(CusStateId);
                    //    if (EntityFlag > 0)
                    //    {
                    //        ldt_sgst_flag = 1;

                    //    }
                    //    else
                    //    {
                    //        ldt_sgst_flag = 0;
                    //    }

                    //}

                    if (lds_auth.Tables[0].Rows[0][0].ToString() != "FAIL")
                    {
                        Session["GDSRateConversion"] = "0";
                        Session["GDSCurrencyCode"] = "INR";

                        var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                        var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();
                        var RoomStayItemListAdvanced = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStays>();
                        List<CLayer.RoomStaysResult> RoomStaysResultList = null;
                        RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                        CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();

                        decimal TamrindAPIPercentage = 0.0M;
                        TamrindAPIPercentage = BLayer.Tamarind.APIPrice(userId);
                        if (TamrindAPIPercentage == 0)
                        {
                            TamrindAPIPercentage = BLayer.Tamarind.APIPriceAll();
                        }

                        objRoomStay.AmountBeforeTax = Convert.ToDecimal(lds_auth.Tables["hotel"].Rows[0]["TotalPrice"]);
                        objRoomStay.BookingCode = "CB00A30";
                        objRoomStay.AmountAfterTax = (TamrindAPIPercentage == 0) ? amount = price : amount = (price / TamrindAPIPercentage) * 100;
                        objRoomStay.CurrencyCode = "INR";
                        objRoomStay.MinAmountAfterTax = objRoomStay.AmountAfterTax;

                        CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                        ObjRoomRateDescription.Name = "test";

                        objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;

                        objRoomStay.RoomTypeCode = "";
                        objRoomStay.RatePlanCode = "";

                        RoomStaysResultList.Add(objRoomStay);

                        AmedusRoomStaysResultList = RoomStaysResultList;
                    }
                }


                //Amedus booking end

                if (data.BookingData == null)
                {
                    data = new Models.SimpleBookingModel();
                    if (Request.Cookies.Get("BookingData").Value == "")
                    {
                        return RedirectToAction("Index", "Book");
                    }
                    string s = Request.Cookies.Get("BookingData").Value;
                    //  if(s!= "") s = HttpUtility.HtmlDecode
                    data.BookingData = HttpUtility.UrlDecode(s);
                    DateTime td = DateTime.Now;
                    s = Request.Cookies.Get("BookCheckIn").Value;
                    s = HttpUtility.UrlDecode(s);
                    DateTime.TryParse(s, out td);
                    data.BookCheckIn = td;
                    td = DateTime.Now.AddDays(7);
                    s = Request.Cookies.Get("BookCheckOut").Value;
                    s = HttpUtility.UrlDecode(s);
                    DateTime.TryParse(s, out td);
                    data.BookCheckOut = td;
                }

                if (data == null) return RedirectToAction("Index");
                BLayer.Bookings.ClearCart(GetUserId());
                BLayer.Bookings.ClearOfflinebookingCart(GetUserId());
                BookAccommodation(data);

                Models.BookingModel items = new Models.BookingModel();
                long Id = GetUserId();
                string BookingFor = model.BookingForSelf;
                string BillingFor = model.NewBillingFor;
                long AssistedCorporateUserID = model.AssistedCorporateUserID;
                long bookingId = BLayer.Bookings.GetCartIdAfterCleaning(Id);
                long offlinebbokingid = BLayer.Bookings.GetOfflinebookingCartIdAfterCleaning(Id);
                BLayer.BookingItem.UpdateBillingAndBookingForInBookingTable(bookingId, BookingFor, BillingFor, AssistedCorporateUserID);
                items.Items = BLayer.BookingItem.GetAllDetailsforoffline(bookingId);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            if (Convert.ToString(TempData["RateErrorCode"]) == "696")
            {

                TempData["RateError"] = "696";
                return RedirectToAction("Index", "Booking");
            }
            //else
            //{
            //    ViewBag.RateErrorCode = "";
            //}
            string bookinffor = model.BookingForSelf;
            string billingfor = model.NewBillingFor;
            return RedirectToAction("Index", "Booking");
        }

        //private static string PNRADD_Multielements(string pSoapHeadr,string pSoapBody, string XMLtext = "")
        //{
        //    string soapResult = string.Empty;
        //    try
        //    {
        //        string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

        //        var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
        //        var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); //http://webservices.amadeus.com/PNRADD_15_1_1A;

        //        string SoapHeader = pSoapHeadr;
        //        string SoapBody = pSoapBody;

        //        string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        //        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
        //        SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
        //        SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";

        //        XmlDocument soapEnvelopeXml = new XmlDocument();
        //        soapEnvelopeXml.LoadXml(SoapEnvelop);


        //HttpWebRequest webRequest =APIUtility.CreateWebRequest(_url, _action);
        //        webRequest =APIUtility.InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

        //        // begin async call to web request.
        //        IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

        //        // suspend this thread until call is complete. You might want to
        //        // do something usefull here like update your UI.
        //        asyncResult.AsyncWaitHandle.WaitOne();

        //        // get the response from the completed web request.

        //        using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
        //        {
        //            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
        //            {
        //                soapResult = rd.ReadToEnd();
        //            }
        //            Console.Write(soapResult);
        //        }
        //    }
        //    catch (WebException webex)
        //    {
        //        WebResponse errResp = webex.Response;
        //        using (Stream respStream = errResp.GetResponseStream())
        //        {
        //            StreamReader reader = new StreamReader(respStream);
        //            string text = reader.ReadToEnd();
        //            soapResult = text;

        //        }
        //    }

        //    return soapResult;
        //}

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CancelOrder();
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index", "Booking");
        }

        public ActionResult ViewBill(long id)
        {
            Models.BookingModel data = new Models.BookingModel();
            try
            {
                if (id > 0)
                {
                    data = LoadBilling(id);
                    data.ForPrint = false;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("BillView", data);
        }
        [AllowAnonymous]
        public ActionResult BillPdf(long id)
        {
            Models.BookingModel data = new Models.BookingModel();
            try
            {
                if (id > 0)
                {
                    data = LoadBilling(id);
                    data.ForPdf = true;
                    data.ForPrint = false;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("BillPrint", data)
            {

                PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
                PageOrientation = Rotativa.Options.Orientation.Portrait
            };
        }

        public ActionResult BillPrint(long id)
        {
            Models.BookingModel data = new Models.BookingModel();
            try
            {
                if (id > 0)
                {
                    data = LoadBilling(id);
                    data.ForPrint = true;
                    data.ForPdf = true;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("BillPrint", data);
        }

        public Models.BookingModel LoadBooking(long bookingId, bool IsCredit = false)
        {
            Models.BookingModel data = new Models.BookingModel();
            data.BookingId = bookingId;
            if (IsCredit)
            {
                data = LoadBillingByCredit(bookingId);
            }
            else
            {
                data = LoadBilling(bookingId);
            }

            if (bookingId > 0)
            {
                List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
                if (adr.Count > 0)
                {
                    data.OrderedBy = adr[0];
                }
                else
                {
                    data.OrderedBy = new CLayer.Address();

                }
            }
            return data;
        }
        public ActionResult PartialPaymentDirect()
        {
            long userId = GetUserId();
            long bookingId = BLayer.Bookings.GetBookingIdForPartialPayByUserId(userId);
            if (bookingId != 0)
            {
                CLayer.ObjectStatus.PartialPaymentStatus gg = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                if (gg == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || gg == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                {
                    Models.BookingModel data = LoadBooking(bookingId);
                    return View("PartialPayment", data);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }


        [AllowAnonymous]
        public ActionResult PartialPayment(long bookingId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] == null)
                    {
                        return RedirectToAction("PIndex", "Continue", new { bookingId = bookingId });
                    }
                }
                long userId = GetUserId();
                long LoginUserbookId = BLayer.Bookings.GetBookId(userId, bookingId);
                CLayer.ObjectStatus.PartialPaymentStatus gg = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                if (gg == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || gg == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                {

                    if (LoginUserbookId == 1)
                    {
                        Models.BookingModel data = LoadBooking(bookingId);
                        return View(data);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        public ActionResult OfflinePaymentFromCustomer(long bookingId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] == null)
                    {
                        return RedirectToAction("PIndex", "Continue", new { bookingId = bookingId });
                    }
                }
                long userId = GetUserId();
                long LoginUserbookId = BLayer.Bookings.GetBookId(userId, bookingId);
                CLayer.ObjectStatus.BookingStatus gg = BLayer.Bookings.GetStatus(bookingId);
                if (gg == CLayer.ObjectStatus.BookingStatus.offlineconfirm)
                {
                    if (LoginUserbookId == 1)
                    {
                        Models.BookingModel data = LoadBooking(bookingId);
                        data.BookingId = bookingId;
                        return View("~/Views/Booking/Index.cshtml", data);
                    }
                    else
                    {
                        return RedirectToAction("InvalidBooking", "Booking");
                    }

                }
                else
                {
                    return RedirectToAction("InvalidBooking", "Booking");
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("InvalidBooking", "Booking");
        }

        public ActionResult InvalidBooking()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult offlinePayment(long bookingId)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] == null)
                    {
                        return RedirectToAction("PIndex", "Continue", new { bookingId = bookingId });
                    }
                }
                long userId = GetUserId();
                long LoginUserbookId = BLayer.Bookings.GetBookId(userId, bookingId);
                CLayer.ObjectStatus.BookingStatus gg = BLayer.Bookings.GetStatus(bookingId);
                if (gg == CLayer.ObjectStatus.BookingStatus.offlineconfirm)
                {

                    if (LoginUserbookId == 1)
                    {
                        Models.BookingModel data = LoadBooking(bookingId);
                        data.BookingId = bookingId;
                        return View("~/Views/Booking/Index.cshtml", data);
                    }
                    else
                    {
                        return RedirectToAction("InvalidBooking", "Booking");
                    }

                }
                else
                {
                    return RedirectToAction("InvalidBooking", "Booking");
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("InvalidBooking", "Booking");
        }

        [AllowAnonymous]
        public async Task<ViewResult> CorporateBookingbycredits(long BookingId)
        {

            long BuserId = BLayer.Bookings.GetBookedByUserId(BookingId);
            BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, BookingId);
            BLayer.Bookings.UpdatePaymentOption((int)CLayer.ObjectStatus.PaymentOption.CorporateCreditBooking, BookingId);

            //Request For booking Submit

            if (BookingId > 0)
            {
                int RequCount = BLayer.BookingExternalInventory.GetExternalInventoryReqByBookingId(BookingId);
                if (RequCount == 0)
                {
                    bool returnstring = BookingSubmitRequest(BookingId);

                    string Errorcomesfrom = "";
                    if (Convert.ToString(TempData["Errorcomes"]) != null)
                    {
                        Errorcomesfrom = Convert.ToString(TempData["Errorcomes"]);
                    }
                    ViewBag.Errorcomes = Errorcomesfrom;


                    if (returnstring == true)
                    {
                        long PId = BLayer.Bookings.GetPropertyId(BookingId);
                        return View("~/Views/Payment/CreditRequestFailed.cshtml", PId);
                    }
                }
            }



            // set corporate today booking allow to OFF
            // BLayer.B2B.AllowCBookSamedaybook(0, BuserId);

            // Booking Details
            CLayer.Booking bdata = BLayer.Bookings.GetDetails(BookingId);

            //DateTime dt1 = bdata.CheckOut;
            //DateTime dt2 = bdata.CheckIn;
            //var bookingdays = (dt1).Subtract(dt2).TotalDays;

            //Credit Details

            CLayer.B2B Creditbookingdata = BLayer.B2B.GetbookingcreditforCorte(BuserId);
            Creditbookingdata.CreditAmount = Math.Round(Creditbookingdata.CreditAmount - bdata.TotalAmount);
            //Creditbookingdata.TotalCreditAmount = Creditbookingdata.TotalCreditAmount;


            //Update Corporate Credits
            CLayer.B2B payment = new CLayer.B2B()
            {
                IsCreditBooking = Creditbookingdata.IsCreditBooking,
                CreditDays = Creditbookingdata.CreditDays,
                CreditAmount = Creditbookingdata.CreditAmount,
                UserId = Creditbookingdata.UserId,
                TotalCreditAmount = Creditbookingdata.TotalCreditAmount
            };
            long id = BLayer.B2B.SaveCBookCredit(payment);

            long PropertyId = BLayer.Bookings.GetPropertyId(BookingId);
            bool ExtReqSuccess = false;
            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropertyId);

            if (InventoryAPIType != 0)
            {
                List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(BookingId);

                if (DtBookReq != null)
                {
                    if (DtBookReq.Count > 0)
                    {
                        foreach (CLayer.BookingExternalInventory RoomsReq in DtBookReq)
                        {
                            if (RoomsReq.BookingStatus == (int)CLayer.BookingExternalInventory.BookingStatusenum.Success)
                            {
                                ExtReqSuccess = true;
                            }
                            else
                            {
                                ExtReqSuccess = false;
                                break;
                            }
                        }
                    }
                }
            }


            if (!ExtReqSuccess)
            {
                await SendMailsAndSmsForCorporateBooking(BookingId);
            }
            else
            {
                await BookingConfirmForExternalBookReq(BookingId);
            }

            Models.BookingModel data = LoadBooking(BookingId);
            if (!ExtReqSuccess)
            {
                ViewBag.BookingSuccess = "BookingRequest";
            }
            else
            {
                ViewBag.BookingSuccess = "Success";

            }
            return View("CorporateCreditBooking", data);


        }


        //       [AllowAnonymous]
        public ActionResult CorporateBookingbycreditAmount(long BookingId, long offlinebookingid)
        {
            string content = string.Empty;
            try
            {
                long BuserId = BLayer.Bookings.GetBookedByUserId(BookingId);
                CLayer.Booking bdataCredit = BLayer.Bookings.GetDataForCreditBooking(BookingId);
                string BookingSelected = "," + bdataCredit.NoOfAccomodations.ToString() + "," + bdataCredit.NoOfAdults.ToString() + "," + bdataCredit.NoOfChildren.ToString() + "," + bdataCredit.NoOfGuests.ToString();

                Models.SimpleBookingModel datatemp = new Models.SimpleBookingModel();
                datatemp.BookingData = "#" + bdataCredit.AccommodationId.ToString() + BookingSelected;
                datatemp.BookCheckIn = bdataCredit.CheckIn;
                datatemp.BookCheckOut = bdataCredit.CheckOut;
                datatemp.PropertyId = bdataCredit.PropertyId;


                int result = BookAccommodation(datatemp, BuserId, BookingId, offlinebookingid);

                if (result < 1)
                {
                    content = "Error-Requested accommodation not available at this moment.Please try later.";
                    AmedusHotelBookingCancel("");
                    return Content(content);
                }


                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, BookingId);
                BLayer.Bookings.UpdatePaymentOption((int)CLayer.ObjectStatus.PaymentOption.CorporateCreditBooking, BookingId);






                //Request For booking Submit

                if (BookingId > 0)
                {
                    int RequCount = BLayer.BookingExternalInventory.GetExternalInventoryReqByBookingId(BookingId);
                    if (RequCount == 0)
                    {
                        bool returnstring = BookingSubmitRequest(BookingId, BuserId);

                        string Errorcomesfrom = "";
                        if (Convert.ToString(TempData["Errorcomes"]) != null)
                        {
                            Errorcomesfrom = Convert.ToString(TempData["Errorcomes"]);
                        }
                        ViewBag.Errorcomes = Errorcomesfrom;


                        if (returnstring == true)
                        {
                            long PId = BLayer.Bookings.GetPropertyId(BookingId);
                            content = "Error";
                            return Content(content);
                            //   return View("~/Views/Payment/CreditRequestFailed.cshtml", PId);
                        }
                    }
                }

                BLayer.CreditBooking.SetCreditBookingstatus((int)CLayer.ObjectStatus.Corpcreditbookstatus.All, BookingId);

                // set corporate today booking allow to OFF
                // BLayer.B2B.AllowCBookSamedaybook(0, BuserId);

                // Booking Details
                CLayer.Booking bdata = BLayer.Bookings.GetDetails(BookingId);

                //DateTime dt1 = bdata.CheckOut;
                //DateTime dt2 = bdata.CheckIn;
                //var bookingdays = (dt1).Subtract(dt2).TotalDays;

                //Credit Details

                string userid = BuserId.ToString();
                CLayer.GDSUserAddress objUserEmail = BLayer.User.GDUSUserDetails(Convert.ToInt64(userid));
                string email = objUserEmail.Email;
                CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);

                CLayer.B2B Creditbookingdata = new CLayer.B2B();
                if ((role == CLayer.Role.Roles.Corporate) || (role == CLayer.Role.Roles.CorporateUser))
                {
                    Creditbookingdata = BLayer.B2B.GetbookingcreditforCorporateUser(BuserId);
                }
                else
                {
                    Creditbookingdata = BLayer.B2B.GetbookingcreditforCorte(BuserId);
                }

                Creditbookingdata.CreditAmount = Math.Round(Creditbookingdata.CreditAmount - bdata.TotalAmount);
                //Creditbookingdata.TotalCreditAmount = Creditbookingdata.TotalCreditAmount;


                //Update Corporate Credits
                CLayer.B2B payment = new CLayer.B2B()
                {
                    IsCreditBooking = Creditbookingdata.IsCreditBooking,
                    CreditDays = Creditbookingdata.CreditDays,
                    CreditAmount = Creditbookingdata.CreditAmount,
                    UserId = Creditbookingdata.UserId,
                    TotalCreditAmount = Creditbookingdata.TotalCreditAmount
                };
                long id = BLayer.B2B.SaveCBookCredit(payment);

                long PropertyId = BLayer.Bookings.GetPropertyId(BookingId);
                bool ExtReqSuccess = false;
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropertyId);

                if (InventoryAPIType != 0)
                {
                    List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(BookingId);

                    if (DtBookReq != null)
                    {
                        if (DtBookReq.Count > 0)
                        {
                            foreach (CLayer.BookingExternalInventory RoomsReq in DtBookReq)
                            {
                                if (RoomsReq.BookingStatus == (int)CLayer.BookingExternalInventory.BookingStatusenum.Success)
                                {
                                    ExtReqSuccess = true;
                                }
                                else
                                {
                                    ExtReqSuccess = false;
                                    break;
                                }
                            }
                        }
                    }
                }


                if (!ExtReqSuccess)
                {
                    SendMailsAndSmsForCorporateBooking(BookingId);
                }
                else
                {
                    BookingConfirmForExternalBookReq(BookingId);
                }

                Models.BookingModel data = LoadBooking(BookingId, true);
                if (!ExtReqSuccess)
                {
                    ViewBag.BookingSuccess = "BookingRequest";
                }
                else
                {
                    ViewBag.BookingSuccess = "Success";

                }

                StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBookingTransaction = new Areas.Admin.Controllers.BookingRequestTransactionsController();
                objBookingTransaction.BookingConfirmbyCredit(BookingId);
                content = "success";

            }
            catch (Exception ex)
            {
                content = "failed";
                LogHandler.HandleError(ex);
            }


            //     ViewBag.Status = "Confirmedforbooking";
            //     ViewBag.BookingID = BookingId;
            //     long resultStatus = BLayer.Bookings.GetApprovalStatus(BookingId);

            //     long cid = 0;
            //     long.TryParse(User.Identity.GetUserId(), out cid);
            //     ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(cid);
            //     string ApprovalMessage = "Booking confirmed by " + ViewBag.Approver;

            //     return Json(new { success = true, responseText = ApprovalMessage }, JsonRequestBehavior.AllowGet);

            //  return RedirectToAction("Index", "BookingApproval");

            return Content(content);// View("CorporateCreditBooking", data);


        }



        public async Task<bool> SendMailsAndSmsForCorporateBooking(long bookingId)
        {
            try
            {


                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);

                try
                {

                    //Booking user email send

                    string message = "";

                    Common.Mailer ml = new Common.Mailer();
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();

                    //send mail to user
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CustomerBRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    customerMsg.To.Add(forUser[0].Email);
                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcususer != "")
                    {
                        string[] emails = BccEmailsforcususer.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            customerMsg.Bcc.Add(emails[i]);
                        }
                    }
                    customerMsg.Subject = "Booking Request";
                    customerMsg.Body = message;
                    customerMsg.IsBodyHtml = true;
                    try
                    {
                        await ml.SendMailAsyncForBooking(customerMsg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                    //send mail to support

                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupportBRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    msg.To.Add(System.Configuration.ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                    string BccEmailsforcus = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcus != "")
                    {
                        string[] emails = BccEmailsforcus.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }

                    msg.Subject = "Booking Request";
                    msg.Body = message;
                    msg.IsBodyHtml = true;
                    try
                    {
                        await ml.SendMailAsyncForBooking(msg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }


                    //Supplier email send
                    if (supplier.Email != "" || details.PropertyEmail != "")
                    {
                        if (supplier.Email == "")
                        {
                            supplier.Email = details.PropertyEmail;
                            details.PropertyEmail = "";
                        }
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                        supplierMsg.To.Add(supplier.Email);

                        // add bcc only for Suppliercommunications
                        string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                        if (BccEmailsforsupp != "")
                        {
                            string[] emails = BccEmailsforsupp.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                supplierMsg.Bcc.Add(emails[i]);
                            }
                        }

                        supplierMsg.Subject = "Booking Request";
                        supplierMsg.Body = message;

                        if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                        supplierMsg.IsBodyHtml = true;
                        await ml.SendMailAsyncForBooking(supplierMsg, Common.Mailer.MailType.Reservation);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;

            //          try
            //          {
            //              //BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);

            //              if (bookingId < 1) return false;
            //              CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
            //              List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
            //              if (byUser == null) return false;
            //              if (forUser.Count == 0) return false;
            //              CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
            //              CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
            //              CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
            //              try
            //              {
            //                  string phone = forUser[0].Mobile;
            //                  if (phone == "") phone = forUser[0].Phone;
            //                  string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
            //                      details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
            //                  bool b = false;
            //                  phone = Common.Utils.GetMobileNo(phone);
            ////block sms 
            //                  //if (phone != "")
            //                  //{ b = await Common.SMSGateway.Send(smsmsg, phone); }





            //                  smsmsg = Common.SMSGateway.GetSupplierBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
            //                      details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));

            //                  phone = Common.Utils.GetMobileNo(details.Mobile);
            //                  if (phone != "")
            //                  { b = await Common.SMSGateway.Send(smsmsg, phone); }

            //                  phone = Common.Utils.GetMobileNo(supplier.Mobile);
            //                  if (phone != "")
            //                  {

            //                      b = await Common.SMSGateway.Send(smsmsg, phone);
            //                  }
            //              }
            //              catch (Exception ex)
            //              {
            //                  Common.LogHandler.HandleError(ex);
            //              }
            //              try
            //              {
            //                  string message = "";
            //                  Common.Mailer ml = new Common.Mailer();
            //                  //for normal user mail body
            //                  message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("BConfirmationMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
            //                  System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //                  msg.To.Add(System.Configuration.ConfigurationManager.AppSettings.Get("CustomerCareMail"));

            //                  //guest mail added
            //   //block email 

            //                  //if (forUser[0].Email != byUser.Email)
            //                  //{
            //                  //    msg.CC.Add(byUser.Email);
            //                  //}

            //                  // add bcc for Customercommunications
            //                  string BccEmailsforcus = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
            //                  if (BccEmailsforcus != "")
            //                  {
            //                      string[] emails = BccEmailsforcus.Split(',');
            //                      for (int i = 0; i < emails.Length; ++i)
            //                      {
            //                          msg.Bcc.Add(emails[i]);
            //                      }
            //                  }

            //  //block email 
            //                  //for corporate admins
            //                  //if (rle == CLayer.Role.Roles.CorporateUser)
            //                  //{

            //                  //    long cid = BLayer.B2B.GetCorporateIdOfUser(byUser.UserId);
            //                  //    if (cid > 0)
            //                  //    {
            //                  //        string em = BLayer.User.GetEmail(cid);
            //                  //        if (em != null && em != "")
            //                  //        {
            //                  //            msg.CC.Add(em);
            //                  //        }
            //                  //    }
            //                  //}

            //                  msg.Subject = "Booking Request";
            //                  msg.Body = message;

            //                  msg.IsBodyHtml = true;
            //                  try
            //                  {
            //                      await ml.SendMailAsyncForBooking(msg, Common.Mailer.MailType.Reservation);
            //                  }
            //                  catch (Exception ex)
            //                  { Common.LogHandler.HandleError(ex); }

            //                  if (supplier.Email != "" || details.PropertyEmail != "")
            //                  {
            //                      if (supplier.Email == "")
            //                      {
            //                          supplier.Email = details.PropertyEmail;
            //                          details.PropertyEmail = "";
            //                      }
            //                      message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

            //                      System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
            //                      supplierMsg.To.Add(supplier.Email);
            //                      supplierMsg.Subject = "Booking Request";
            //                      supplierMsg.Body = message;

            //                      // add bcc only for Suppliercommunications
            //                      string BccEmailsforsupp = BLayer.Settings.GetValue("CCSuppliercommunications ");
            //                      if (BccEmailsforsupp != "")
            //                      {
            //                          string[] emails = BccEmailsforsupp.Split(',');
            //                          for (int i = 0; i < emails.Length; ++i)
            //                          {
            //                              supplierMsg.Bcc.Add(emails[i]);
            //                          }
            //                      }

            //                      if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
            //                      supplierMsg.IsBodyHtml = true;
            //                      await ml.SendMailAsyncForBooking(supplierMsg, Common.Mailer.MailType.Reservation);
            //                  }              
            //              }
            //              catch (Exception ex)
            //              {
            //                  Common.LogHandler.HandleError(ex);
            //              }
            //          }
            //          catch (Exception ex)
            //          {
            //              Common.LogHandler.HandleError(ex);
            //          }
            //          return true;
        }

        //Confirm Booking
        public async Task<bool> BookingConfirm(long bookingId)
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);

                //send customer email/message               
                //email
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                try
                {
                    string message = "";
                    Common.Mailer ml = new Common.Mailer();
                    //for normal user mail body
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("BConfirmationMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    //guest mail added
                    msg.To.Add(forUser[0].Email);
                    if (forUser[0].Email != byUser.Email)
                    {
                        msg.CC.Add(byUser.Email);
                    }
                    //for corporate admins
                    if (rle == CLayer.Role.Roles.CorporateUser)
                    {
                        //  message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CorpIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        long cid = BLayer.B2B.GetCorporateIdOfUser(byUser.UserId);
                        if (cid > 0)
                        {
                            string em = BLayer.User.GetEmail(cid);
                            if (em != null && em != "")
                            {
                                msg.CC.Add(em);
                            }
                        }
                    }

                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcususer != "")
                    {
                        string[] emails = BccEmailsforcususer.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }

                    msg.Subject = "Booking Confirmation";
                    msg.Body = message;

                    msg.IsBodyHtml = true;

                    try
                    {
                        await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                    //message
                    if (bookingId < 1) return false;
                    if (byUser == null) return false;
                    if (forUser.Count == 0) return false;
                    try
                    {
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                            details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);

                        if (phone != "")
                        {
                            b = await Common.SMSGateway.Send(smsmsg, phone);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }


                    //send supplier email/message 

                    //message
                    if (bookingId < 1) return false;
                    if (byUser == null) return false;
                    if (forUser.Count == 0) return false;
                    try
                    {
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                            details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);

                        if (phone != "")
                        {
                            b = await Common.SMSGateway.Send(smsmsg, phone);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                    //email
                    if (bookingId < 1) return false;
                    try
                    {
                        if (supplier.Email != "" || details.PropertyEmail != "")
                        {
                            if (supplier.Email == "")
                            {
                                supplier.Email = details.PropertyEmail;
                                details.PropertyEmail = "";
                            }
                            message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                            System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                            supplierMsg.To.Add(supplier.Email);
                            supplierMsg.Subject = "Booking Confirmation";
                            supplierMsg.Body = message;

                            string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                            if (BccEmailsforsupp != "")
                            {
                                string[] emails = BccEmailsforsupp.Split(',');
                                for (int i = 0; i < emails.Length; ++i)
                                {
                                    supplierMsg.Bcc.Add(emails[i]);
                                }
                            }

                            if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                            supplierMsg.IsBodyHtml = true;

                            try
                            {
                                await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
                            }
                            catch (Exception ex)
                            {
                                Common.LogHandler.HandleError(ex);
                            }

                        }

                    }


                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }



                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }

        //Confirm Booking
        public async Task<bool> BookingConfirmForExternalBookReq(long bookingId)
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);

                //send customer email/message               
                //email
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                try
                {
                    string message = "";
                    Common.Mailer ml = new Common.Mailer();
                    //for normal user mail body
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("BConfirmationMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    //guest mail added
                    msg.To.Add(forUser[0].Email);
                    if (forUser[0].Email != byUser.Email)
                    {
                        msg.CC.Add(byUser.Email);
                    }
                    //for corporate admins
                    if (rle == CLayer.Role.Roles.CorporateUser)
                    {
                        //  message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CorpIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        long cid = BLayer.B2B.GetCorporateIdOfUser(byUser.UserId);
                        if (cid > 0)
                        {
                            string em = BLayer.User.GetEmail(cid);
                            if (em != null && em != "")
                            {
                                msg.CC.Add(em);
                            }
                        }
                    }

                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcususer != "")
                    {
                        string[] emails = BccEmailsforcususer.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }

                    msg.Subject = "Booking Confirmation";
                    msg.Body = message;

                    msg.IsBodyHtml = true;

                    try
                    {
                        await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                    //message
                    if (bookingId < 1) return false;
                    if (byUser == null) return false;
                    if (forUser.Count == 0) return false;
                    try
                    {
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                            details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);

                        if (phone != "")
                        {
                            b = await Common.SMSGateway.Send(smsmsg, phone);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }


                    //send supplier email/message 

                    ////message
                    //if (bookingId < 1) return false;
                    //if (byUser == null) return false;
                    //if (forUser.Count == 0) return false;
                    //try
                    //{
                    //    string phone = forUser[0].Mobile;
                    //    if (phone == "") phone = forUser[0].Phone;
                    //    string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                    //        details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    //    bool b = false;
                    //    phone = Common.Utils.GetMobileNo(phone);

                    //    if (phone != "")
                    //    {
                    //        b = await Common.SMSGateway.Send(smsmsg, phone);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Common.LogHandler.HandleError(ex);
                    //}

                    ////email
                    //if (bookingId < 1) return false;
                    //try
                    //{
                    //    if (supplier.Email != "" || details.PropertyEmail != "")
                    //    {
                    //        if (supplier.Email == "")
                    //        {
                    //            supplier.Email = details.PropertyEmail;
                    //            details.PropertyEmail = "";
                    //        }
                    //        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                    //        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                    //        supplierMsg.To.Add(supplier.Email);
                    //        supplierMsg.Subject = "Booking Confirmation";
                    //        supplierMsg.Body = message;

                    //        string BccEmailsforsupp = BLayer.Settings.GetValue("CCSuppliercommunications ");
                    //        if (BccEmailsforsupp != "")
                    //        {
                    //            string[] emails = BccEmailsforsupp.Split(',');
                    //            for (int i = 0; i < emails.Length; ++i)
                    //            {
                    //                supplierMsg.Bcc.Add(emails[i]);
                    //            }
                    //        }

                    //        if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                    //        supplierMsg.IsBodyHtml = true;

                    //        try
                    //        {
                    //            await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            Common.LogHandler.HandleError(ex);
                    //        }

                    //    }

                    //}


                    //catch (Exception ex)
                    //{
                    //    Common.LogHandler.HandleError(ex);
                    //}



                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }

        protected string GetIPAddress()
        {
            var context = System.Web.HttpContext.Current;
            string ip = String.Empty;

            if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                ip = context.Request.UserHostAddress;
            return ip;
        }

        public bool BookingSubmitRequest(long bookingID = 0)
        {
            long propertyId = 0;
            long bookingId = bookingID;
            int InventoryAPIType = 0;
            bool BookingStatus = true;
            string GDSTransID = string.Empty;
            try
            {
                long userId = GetUserId();
                if (bookingId <= 0)
                {
                    bookingId = BLayer.Bookings.GetCartId(userId);
                }

                string IpAdds = GetIPAddress();
                propertyId = BLayer.BookingExternalInventory.GetPropertyIdByBookingId(bookingId);
                InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(bookingId));
                Session["InventoryAPIType"] = InventoryAPIType;
                ViewBag.AmadeusRates = TempData["AmadeusRates"];
                TempData.Keep("AmadeusRates");

                List<CLayer.BookingItem> bookitems = new List<CLayer.BookingItem>();
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    bookitems = BLayer.BookingItem.GetAllDetails(bookingId, true);
                }
                else
                {
                    bookitems = BLayer.BookingItem.GetAllDetails(bookingId);
                }

                string HotelId = BLayer.Property.GetHotelId(BLayer.Bookings.GetPropertyId(bookingId));


                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    #region HOTEL SELL
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELSELLACTION); ;

                    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                    srch.HotelCode = HotelId;



                    List<CLayer.RoomStaysResult> RoomStaysResultList = APIUtility.AmedusHotelRoomStaysResultList;

                    RoomStaysResultList = (List<CLayer.RoomStaysResult>)TempData["RoomStaysResult"];
                    TempData.Keep("RoomStaysResult");
                    string SoapHeaderStateful = string.Empty;
                    foreach (var item in RoomStaysResultList)
                    {
                        string BookingCode = item.BookingCode;
                        string HotelSellSoapBody = APIUtility.Hotel_Sell(HotelId, BookingCode);
                        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        string result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);



                        if (APIUtility.ReadGDSError(result, (int)CLayer.ObjectStatus.GDSType.HotelSell) == "UNABLE TO PROCESS")
                        {
                            result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);
                        }
                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSell, bookingId);

                        #endregion Transaction log end

                        if (!APIUtility.CheckHotelBookingErrorExists(result))
                        {
                            Serializer HotelSell = new Serializer();
                            CLayer.HotelSell.Envelope HotelSellResult = HotelSell.Deserialize<CLayer.HotelSell.Envelope>(result);

                            Session["HotelSellStatus"] = "success";
                            Session["SessionId"] = HotelSellResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelSellResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelSellResult.Header.Session.SecurityToken;
                            BookingStatus = false;
                        }
                        else
                        {
                            Serializer HotelSellError = new Serializer();
                            CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(result);
                            Session["HotelSellStatus"] = "error";
                            Session["SessionId"] = HotelSellErrorResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelSellErrorResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelSellErrorResult.Header.Session.SecurityToken;
                            ViewBag.HotelSellResult = "RequestFailed";
                            TempData["Errorcomes"] = "RequestFailed";

                            List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCode);
                            foreach (var itemerror in objBookItemsError)
                            {
                                BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, result);
                            }


                            #region PNR CANCEL

                            StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                            //   BookingStatus = objBooking.BookingDecline(bookingId);

                            BookingStatus = objBooking.BookingCancel(bookingId);

                            //  string resultCancel = APIUtility.ExecutePNRCancel("", (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRGeneration);

                            BookingStatus = true;
                            return BookingStatus;
                            #endregion

                        }



                    }
                    #endregion

                    #region PNR MULTIELEMENTS-FINAL
                    Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION);
                    SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                    string BookingCodeFinal = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCodeFinal, "test@test.com", 10);

                    TempData["RoomStaysResult"] = RoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = RoomStaysResultList;

                    string resultFinal = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElementsConfirm, bookingId);

                    #endregion Transaction log end

                    Serializer pnrser = new Serializer();

                    CLayer.PNRAddElementsConfirm.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElementsConfirm.Envelope>(resultFinal);
                    Session["ControlNumber"] = PNRAddResult.Body.PNR_Reply.pnrHeader.reservationInfo.reservation.controlNumber;
                    //if(string.IsNullOrEmpty(Convert.ToString(Session["ControlNumber"])))
                    //{
                    //    Session["ControlNumber"]= PNRAddResult.Body.PNR_Reply.originDestinationDetails.originDestination.
                    //}

                    if (APIUtility.CheckHotelBookingErrorExists(resultFinal))
                    {

                        //Serializer HotelSellError = new Serializer();
                        //CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(resultFinal);

                        Serializer PNRADDError = new Serializer();
                        CLayer.PNRAddElementsError.Envelope PNRAddErrorResult = null;
                        CLayer.PNRAddElementsConfirmError.Envelope PNRAddErrorResultConfirm = null;

                        try
                        {
                            PNRAddErrorResult = PNRADDError.Deserialize<CLayer.PNRAddElementsError.Envelope>(resultFinal);
                        }
                        catch (Exception ex)
                        {
                            PNRAddErrorResultConfirm = PNRADDError.Deserialize<CLayer.PNRAddElementsConfirmError.Envelope>(resultFinal);
                        }


                        Session["HotelSellStatus"] = "error";
                        if (PNRAddErrorResult != null)
                        {
                            Session["SessionId"] = PNRAddErrorResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(PNRAddErrorResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = PNRAddErrorResult.Header.Session.SecurityToken;
                            Session["ControlNumber"] = PNRAddErrorResult.Body.PNR_Reply.originDestinationDetails.itineraryInfo.generalOption.Where(x => x.optionDetail.type == "CF").FirstOrDefault();
                        }
                        else
                        {
                            Session["SessionId"] = PNRAddErrorResultConfirm.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(PNRAddErrorResultConfirm.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = PNRAddErrorResultConfirm.Header.Session.SecurityToken;
                            Session["ControlNumber"] = PNRAddErrorResultConfirm.Body.PNR_Reply.originDestinationDetails.itineraryInfo.generalOption.Where(x => x.optionDetail.type == "CF").FirstOrDefault();
                        }

                        List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCodeFinal);
                        foreach (var itemerror in objBookItemsError)
                        {
                            BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, resultFinal);
                        }

                        #region PNR CANCEL
                        ViewBag.HotelSellResult = "RequestFailed";
                        TempData["Errorcomes"] = "RequestFailed";
                        string ControlNumber = Convert.ToString(Session["ControlNumber"]);
                        // string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize);
                        //     return false;
                        StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                        //  BookingStatus = objBooking.BookingDecline(bookingId);
                        BookingStatus = objBooking.BookingCancel(bookingId);
                        BookingStatus = true;
                        return BookingStatus;
                        #endregion

                        //#region GDS Signout
                        //Action = "";
                        //SoapHeaderStateful = APIUtility.SetSecuritySignOutSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        //string SignOutSoapBody = APIUtility.SecuritySignOut();
                        //// string SecuritySignOutHeader = APIUtility.SetSecuritySignOutSoapHeader();
                        //string SecuritySignOut = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, SignOutSoapBody);
                        //#endregion 


                    }
                    else
                    {
                        BookingStatus = false;

                        #region PNR_Retrieve
                        Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELPNRRETRIEVEACTION);
                        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        string PNRRetrieveSoapBody = APIUtility.PNR_Retrieve(Convert.ToString(Session["ControlNumber"]));
                        string resultRetrieve = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRRetrieveSoapBody);
                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRRetrieve, bookingId);

                        #endregion Transaction log end
                        #endregion

                        #region GDS Signout
                        Action = "";
                        SoapHeaderStateful = APIUtility.SetSecuritySignOutSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        string SignOutSoapBody = APIUtility.SecuritySignOut();
                        // string SecuritySignOutHeader = APIUtility.SetSecuritySignOutSoapHeader();
                        string SecuritySignOut = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, SignOutSoapBody);
                        #endregion 

                        //#region Hotel Completereservationinfodetails
                        //Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELCOMPLETERESERVATION);
                        //SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"])+1, Session["SecurityToken"].ToString(), true);
                        //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        //string HotelCompleteReservation = APIUtility.Hotel_CompleteReservationDetails(Convert.ToString(Session["ControlNumber"]));
                        //string resultotelCompleteReservation = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, HotelCompleteReservation);


                        //#endregion 
                    }

                    #endregion
                    List<CLayer.BookingItem> objBookItems = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCodeFinal);
                    foreach (var item in objBookItems)
                    {
                        BLayer.BookingItem.UpdateHotelConfirmNumber(item.BookingId, item.AccommodationId, Convert.ToString(Session["ControlNumber"]));
                        #region Update Converted Amount details
                        if (Session["GDSCurrencyConversion"] != null)
                        {

                            long CountryCode = Convert.ToInt64(Session["GDSCountryID"]);
                            CLayer.GDSCurrencyConversions objCurrency = (CLayer.GDSCurrencyConversions)Session["GDSCurrencyConversion"];
                            CLayer.BookingItem objBookingItem = new CLayer.BookingItem();
                            objBookingItem.BookingId = item.BookingId;
                            objBookingItem.AccommodationId = item.AccommodationId;
                            objBookingItem.GDSAmount = Convert.ToDecimal(Session["GDSAmount"]);
                            objBookingItem.GDSConvertedAmount = Convert.ToDecimal(Session["GDSConvertedAmount"]);
                            objBookingItem.GDSConversionRate = objCurrency.RateConversion;
                            objBookingItem.GDSCountry = CountryCode;

                            BLayer.BookingItem.UpdateGDSCurrencyDetails(objBookingItem);

                        }
                        #endregion
                    }



                }
                else if ((HotelId != null && HotelId != "") && (InventoryAPIType != (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus))
                {
                    HotelId = HotelId.Trim();
                    List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
                    CLayer.Address byAddress = BLayer.Bookings.GetBookedByUser(bookingId);
                    CLayer.Booking data = BLayer.Bookings.GetDetails(bookingId);


                    string query_key = data.OrderNo + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                    string BookingsessionId = "bs" + "_" + bookingId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                    string BookingrequestId = "br" + "_" + bookingId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");


                    string errorinrequest = "";


                    #region

                    foreach (CLayer.BookingItem bi in bookitems)
                    {
                        long accid = bi.AccommodationId;
                        string RoomId = BLayer.Accommodation.GetRoomId(accid);

                        if (RoomId != "" && RoomId != null)
                        {
                            RoomId = RoomId.Trim();
                            int totaladult = bi.NoOfAdults + bi.NoOfGuests;
                            List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> HotelRoomDetailsList = StayBazar.Inventory.MaximojoBook.BookingAvailDeserializeJsonResponse(HotelId, data.CheckIn.ToString("yyyy-MM-dd"), data.CheckOut.ToString("yyyy-MM-dd"), totaladult, bi.NoOfChildren, bi.NoOfAccommodations, query_key, BookingsessionId, BookingrequestId);

                            //Hotel rooms list filter by rooms only plan

                            List<string> newroomtypelist = new List<string>();
                            List<string> Multipleroomtypes = new List<string>();

                            foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails u in HotelRoomDetailsList)
                            {
                                if (!newroomtypelist.Contains(u.roomtype_id))
                                {
                                    newroomtypelist.Add(u.roomtype_id);
                                }
                                else
                                {
                                    if (!Multipleroomtypes.Contains(u.roomtype_id))
                                    {
                                        Multipleroomtypes.Add(u.roomtype_id);
                                    }
                                }
                            }

                            List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> NewHotelRoomDetailsList = new List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails>();

                            foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails RId in HotelRoomDetailsList)
                            {
                                if (!Multipleroomtypes.Contains(RId.roomtype_id))
                                {
                                    NewHotelRoomDetailsList.Add(RId);
                                }
                                else
                                {
                                    //// check name contains Room Only
                                    //string[] SplitRoomonlyterm = RId.room_name.Split('-');
                                    //if (SplitRoomonlyterm[1].Trim() == "Room only")
                                    //{
                                    //    NewHotelRoomDetailsList.Add(RId);
                                    //}

                                    ////if (RId.RatePlanId == System.Configuration.ConfigurationManager.AppSettings.Get("rateplanidformaximojo"))
                                    ////{
                                    ////    NewHotelRoomDetailsList.Add(RId);
                                    ////}

                                    if (RId.room_name.Contains("Room only"))
                                    {
                                        NewHotelRoomDetailsList.Add(RId);
                                    }

                                }
                            }



                            // add rooms without room only type (add min one)

                            foreach (string ff in Multipleroomtypes)
                            {
                                if (!NewHotelRoomDetailsList.Exists(a => a.roomtype_id == ff))
                                {
                                    foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails RId in HotelRoomDetailsList)
                                    {
                                        if (RId.roomtype_id == ff)
                                        {
                                            if (!NewHotelRoomDetailsList.Exists(a => a.roomtype_id == ff))
                                            {
                                                NewHotelRoomDetailsList.Add(RId);
                                            }
                                        }
                                    }
                                }
                            }



                            if (NewHotelRoomDetailsList.Exists(a => a.roomtype_id == RoomId))
                            {
                                #region
                                foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails ro in NewHotelRoomDetailsList)
                                {
                                    if (RoomId == ro.roomtype_id)
                                    {
                                        try
                                        {
                                            #region
                                            //BOOKING REQUEST PASS
                                            //CLayer.BookingExternalInventory roomdet = BLayer.BookingExternalInventory.GetAllDetailsByRoomIdandHotelId(RoomId, HotelId);
                                            List<StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room> RoomsList = new List<StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room>();
                                            StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room Lrooms = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room();

                                            StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party fff = new Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party();
                                            fff.adults = bi.NoOfAdults;

                                            List<object> chdrn = new List<object>();

                                            for (int i = 0; i < bi.NoOfChildren; i++)
                                            {
                                                chdrn.Add(5);
                                            }

                                            fff.children = chdrn;

                                            //multiple accommoadtion 
                                            for (int i = 0; i < bi.NoOfAccommodations; i++)
                                            {
                                                Lrooms.party = fff;
                                                Lrooms.traveler_first_name = byAddress.Firstname;
                                                Lrooms.traveler_last_name = byAddress.Firstname;
                                                RoomsList.Add(Lrooms);
                                            }


                                            var Book_WihoutPayobj = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.RootObject
                                            {
                                                checkin_date = data.CheckIn.ToString("yyyy-MM-dd"),
                                                checkout_date = data.CheckOut.ToString("yyyy-MM-dd"),
                                                hotel_id = HotelId,
                                                reference_id = data.OrderNo + "_" + ro.roomtype_id + "_" + ro.RatePlanId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt"),
                                                ip_address = IpAdds,
                                                customer = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Customer
                                                {
                                                    first_name = byAddress.Firstname,
                                                    last_name = byAddress.Firstname,
                                                    email = byAddress.Email,
                                                    phone_number = byAddress.Mobile,
                                                    country = byAddress.CountryCode
                                                },
                                                final_price_at_booking = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.FinalPriceAtBooking
                                                {
                                                    amount = Convert.ToDouble(ro.final_price_at_bookingamt * bi.NoOfAccommodations),
                                                    currency = ro.final_price_at_bookingamtcurr
                                                },
                                                final_price_at_checkout = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.FinalPriceAtCheckout
                                                {
                                                    amount = Convert.ToDouble(ro.final_price_at_checkoutamt),
                                                    currency = ro.final_price_at_checkoutamtcurr
                                                },
                                                partner_data = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.PartnerData
                                                {
                                                    DomainId = ro.DomainId,
                                                    HotelId = HotelId,
                                                    PromotionId = ro.PromotionId,
                                                    RatePlanId = ro.RatePlanId,
                                                    RoomId = ro.roomtype_id
                                                },
                                                rooms = RoomsList
                                            };




                                            //bookingsubmitWithoutPay
                                            string ResponseBookSub = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Submit_Without_paypaldetails(Book_WihoutPayobj);

                                            errorinrequest = ResponseBookSub;
                                            StayBazar.Inventory.Models.MaxBooking_Submit_Response.RootObject Bookingdetails = JsonConvert.DeserializeObject<StayBazar.Inventory.Models.MaxBooking_Submit_Response.RootObject>(ResponseBookSub);





                                            //Save Booking Response by booking submit 

                                            CLayer.BookingExternalInventory ExternalBook = new CLayer.BookingExternalInventory();
                                            ExternalBook.BookingId = bookingId;
                                            ExternalBook.hotel_id = Bookingdetails.reservation.hotel_id;
                                            ExternalBook.hotel_name = Bookingdetails.reservation.hotel.name;

                                            ExternalBook.Reference_Id = Bookingdetails.reference_id;
                                            ExternalBook.reservation_id = Bookingdetails.reservation.reservation_id;


                                            if (Bookingdetails.status == "Success")
                                            {
                                                ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Success;
                                            }
                                            else if (Bookingdetails.status == "Failure")
                                            {
                                                ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Failure;
                                            }
                                            else if (Bookingdetails.status == "Pending")
                                            {
                                                ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Pending;
                                            }
                                            else
                                            {
                                                ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.None;
                                            }


                                            if (Bookingdetails.reservation.status == "Booked")
                                            {
                                                ExternalBook.ReservationStatus = (int)CLayer.BookingExternalInventory.ReservationStatusenum.Booked;
                                            }
                                            else
                                            {
                                                ExternalBook.ReservationStatus = (int)CLayer.BookingExternalInventory.ReservationStatusenum.none;
                                            }

                                            //if status failure/pending/reservation status error
                                            string VerifyRequestResp = "Booked Successfully";

                                            if (ExternalBook.BookingStatus != 1 || ExternalBook.ReservationStatus != 1)
                                            {
                                                VerifyRequestResp = StayBazar.Inventory.MaximojoBook.Maximojo_Booking_Verify(HotelId, Bookingdetails.reference_id, Bookingdetails.reservation.reservation_id);
                                            }
                                            ExternalBook.StatusDetails = VerifyRequestResp;
                                            ExternalBook.roomtype_id = ro.roomtype_id;
                                            ExternalBook.room_name = ro.room_name;
                                            ExternalBook.room_desc = ro.room_desc;

                                            ExternalBook.CheckInDate = Bookingdetails.reservation.checkin_date;
                                            ExternalBook.CheckOutDate = Bookingdetails.reservation.checkout_date;
                                            ExternalBook.CustomerId = byAddress.UserId;
                                            ExternalBook.IpAddtress = IpAdds;
                                            ExternalBook.Response = ResponseBookSub;
                                            ExternalBook.BookingApiType = (int)CLayer.BookingExternalInventory.BookingApiTypes.Maximojo;
                                            ExternalBook.final_price_at_bookingamt = ro.final_price_at_bookingamt;
                                            ExternalBook.final_price_at_checkoutamt = ro.final_price_at_checkoutamt;
                                            ExternalBook.DomainId = ro.DomainId;
                                            ExternalBook.PromotionId = ro.PromotionId;
                                            ExternalBook.RatePlanId = ro.RatePlanId;
                                            ExternalBook.query_key = query_key;
                                            ExternalBook.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.none;
                                            ExternalBook.BookingsessionId = BookingsessionId;
                                            ExternalBook.BookingrequestId = BookingrequestId;
                                            long BookingExtInvReqId = BLayer.BookingExternalInventory.SaveBookingSubmitResponse(ExternalBook);

                                            //Save rooms details

                                            CLayer.BookingExternalInventory roomdt = new CLayer.BookingExternalInventory();
                                            roomdt.BookingExtInvReqId = BookingExtInvReqId;
                                            roomdt.hotel_id = ro.hotel_id;
                                            roomdt.hotel_name = ro.hotel_name;
                                            roomdt.roomtype_id = ro.roomtype_id;
                                            roomdt.room_name = ro.room_name;
                                            roomdt.room_desc = ro.room_desc;
                                            roomdt.final_price_at_bookingamt = ro.final_price_at_bookingamt;
                                            roomdt.final_price_at_bookingamtcurr = ro.final_price_at_bookingamtcurr;
                                            roomdt.final_price_at_checkoutamt = ro.final_price_at_checkoutamt;
                                            roomdt.final_price_at_checkoutamtcurr = ro.final_price_at_checkoutamtcurr;
                                            roomdt.DomainId = ro.DomainId;
                                            roomdt.PromotionId = ro.PromotionId;
                                            roomdt.RatePlanId = ro.RatePlanId;
                                            BLayer.BookingExternalInventory.Save(roomdt);

                                            //CANCELLATION WHEN BOOKING FAILED 

                                            if (Bookingdetails.status != "Success")
                                            {
                                                // BOOKING CANCEL FROM EXTERNAL INVENTORY REQUEST

                                                ExternalBookRequestCancel(bookingId);

                                                //Redirecting after cancelling  all booked by user                                               
                                                TempData["Errorcomes"] = "RequestFailed";
                                                return true;
                                                //return View("~/Views/Payment/RequestFailed.cshtml", propertyId);

                                            }
                                            #endregion
                                        }
                                        catch (Exception ex)
                                        {
                                            ExternalBookRequestCancel(bookingId);
                                            Exception Error = new Exception("#Error from  External Booking Submit Request(Payment , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex + errorinrequest);
                                            Common.LogHandler.HandleError(Error);
                                            TempData["Errorcomes"] = "internalerror";
                                            return true;
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                // BOOKING CANCEL FROM EXTERNAL INVENTORY REQUEST
                                ExternalBookRequestCancel(bookingId);
                                //Redirecting after cancelling  all booked by user                                               
                                TempData["Errorcomes"] = "RequestFailed";
                                return true;
                            }
                        }
                        //else if
                    }

                    #endregion

                }
            }
            catch (Exception ex)

            {
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    AmedusHotelBookingCancel("");
                }
                else
                {
                    ExternalBookRequestCancel(bookingId);

                }
                Exception Error = new Exception("#Error from External Booking Submit Request(Payment , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
                TempData["Errorcomes"] = "internalerror";
                return true;
            }
            return false;

        }
        public bool BookingSubmitRequest(long bookingID = 0, long UserID = 0)
        {
            long propertyId = 0;
            long bookingId = bookingID;
            int InventoryAPIType = 0;
            bool BookingStatus = true;
            string GDSTransID = string.Empty;
            try
            {
                long userId = UserID;//GetUserId();
                if (bookingId <= 0)
                {
                    bookingId = BLayer.Bookings.GetCartId(userId);
                }

                string IpAdds = GetIPAddress();
                propertyId = BLayer.BookingExternalInventory.GetPropertyIdByBookingId(bookingId);
                InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(bookingId));
                System.Web.HttpContext.Current.Session["InventoryAPIType"] = InventoryAPIType;
                ViewBag.AmadeusRates = TempData["AmadeusRates"];
                TempData.Keep("AmadeusRates");

                List<CLayer.BookingItem> bookitems = new List<CLayer.BookingItem>();
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    bookitems = BLayer.BookingItem.GetAllDetails(bookingId, true);
                }
                else
                {
                    bookitems = BLayer.BookingItem.GetAllDetails(bookingId);
                }

                string HotelId = BLayer.Property.GetHotelId(BLayer.Bookings.GetPropertyId(bookingId));


                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    #region HOTEL SELL
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELSELLACTION); ;

                    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                    srch.HotelCode = HotelId;



                    List<CLayer.RoomStaysResult> RoomStaysResultList = APIUtility.AmedusHotelRoomStaysResultList;

                    //   RoomStaysResultList = (List<CLayer.RoomStaysResult>)TempData["RoomStaysResult"];
                    // TempData.Keep("RoomStaysResult");
                    string SoapHeaderStateful = string.Empty;
                    foreach (var item in RoomStaysResultList)
                    {
                        string BookingCode = item.BookingCode;
                        string HotelSellSoapBody = APIUtility.Hotel_Sell(HotelId, BookingCode);
                        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, System.Web.HttpContext.Current.Session["SessionId"].ToString(), Convert.ToInt32(System.Web.HttpContext.Current.Session["SequenceNumber"]), System.Web.HttpContext.Current.Session["SecurityToken"].ToString());
                        string result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);



                        if (APIUtility.ReadGDSError(result, (int)CLayer.ObjectStatus.GDSType.HotelSell) == "UNABLE TO PROCESS")
                        {
                            result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);
                        }
                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(UserID), (int)CLayer.ObjectStatus.GDSType.HotelSell, bookingId);

                        #endregion Transaction log end

                        if (!APIUtility.CheckHotelBookingErrorExists(result))
                        {
                            Serializer HotelSell = new Serializer();
                            CLayer.HotelSell.Envelope HotelSellResult = HotelSell.Deserialize<CLayer.HotelSell.Envelope>(result);

                            System.Web.HttpContext.Current.Session["HotelSellStatus"] = "success";
                            System.Web.HttpContext.Current.Session["SessionId"] = HotelSellResult.Header.Session.SessionId;
                            System.Web.HttpContext.Current.Session["SequenceNumber"] = Convert.ToInt32(HotelSellResult.Header.Session.SequenceNumber);
                            System.Web.HttpContext.Current.Session["SecurityToken"] = HotelSellResult.Header.Session.SecurityToken;
                            BookingStatus = false;
                        }
                        else
                        {
                            Serializer HotelSellError = new Serializer();
                            CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(result);
                            System.Web.HttpContext.Current.Session["HotelSellStatus"] = "error";
                            System.Web.HttpContext.Current.Session["SessionId"] = HotelSellErrorResult.Header.Session.SessionId;
                            System.Web.HttpContext.Current.Session["SequenceNumber"] = Convert.ToInt32(HotelSellErrorResult.Header.Session.SequenceNumber);
                            System.Web.HttpContext.Current.Session["SecurityToken"] = HotelSellErrorResult.Header.Session.SecurityToken;
                            ViewBag.HotelSellResult = "RequestFailed";
                            TempData["Errorcomes"] = "RequestFailed";

                            List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCode);
                            foreach (var itemerror in objBookItemsError)
                            {
                                BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, result);
                            }


                            #region PNR CANCEL

                            StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                            //   BookingStatus = objBooking.BookingDecline(bookingId);

                            BookingStatus = objBooking.BookingCancel(bookingId);

                            //  string resultCancel = APIUtility.ExecutePNRCancel("", (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRGeneration);

                            BookingStatus = true;
                            return BookingStatus;
                            #endregion

                        }



                    }
                    #endregion

                    #region PNR MULTIELEMENTS-FINAL
                    Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION);
                    SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, System.Web.HttpContext.Current.Session["SessionId"].ToString(), Convert.ToInt32(System.Web.HttpContext.Current.Session["SequenceNumber"]), System.Web.HttpContext.Current.Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                    string BookingCodeFinal = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCodeFinal, "test@test.com", 10);

                    TempData["RoomStaysResult"] = RoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = RoomStaysResultList;

                    string resultFinal = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(UserID), (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElementsConfirm, bookingId);

                    #endregion Transaction log end

                    Serializer pnrser = new Serializer();

                    CLayer.PNRAddElementsConfirm.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElementsConfirm.Envelope>(resultFinal);
                    System.Web.HttpContext.Current.Session["ControlNumber"] = PNRAddResult.Body.PNR_Reply.pnrHeader.reservationInfo.reservation.controlNumber;
                    //if(string.IsNullOrEmpty(Convert.ToString(System.Web.HttpContext.Current.Session["ControlNumber"])))
                    //{
                    //    System.Web.HttpContext.Current.Session["ControlNumber"]= PNRAddResult.Body.PNR_Reply.originDestinationDetails.originDestination.
                    //}

                    if (APIUtility.CheckHotelBookingErrorExists(resultFinal))
                    {

                        //Serializer HotelSellError = new Serializer();
                        //CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(resultFinal);

                        Serializer PNRADDError = new Serializer();
                        CLayer.PNRAddElementsError.Envelope PNRAddErrorResult = null;
                        CLayer.PNRAddElementsConfirmError.Envelope PNRAddErrorResultConfirm = null;

                        try
                        {
                            PNRAddErrorResult = PNRADDError.Deserialize<CLayer.PNRAddElementsError.Envelope>(resultFinal);
                        }
                        catch (Exception ex)
                        {
                            PNRAddErrorResultConfirm = PNRADDError.Deserialize<CLayer.PNRAddElementsConfirmError.Envelope>(resultFinal);
                        }


                        System.Web.HttpContext.Current.Session["HotelSellStatus"] = "error";
                        if (PNRAddErrorResult != null)
                        {
                            System.Web.HttpContext.Current.Session["SessionId"] = PNRAddErrorResult.Header.Session.SessionId;
                            System.Web.HttpContext.Current.Session["SequenceNumber"] = Convert.ToInt32(PNRAddErrorResult.Header.Session.SequenceNumber);
                            System.Web.HttpContext.Current.Session["SecurityToken"] = PNRAddErrorResult.Header.Session.SecurityToken;
                            System.Web.HttpContext.Current.Session["ControlNumber"] = PNRAddErrorResult.Body.PNR_Reply.originDestinationDetails.itineraryInfo.generalOption.Where(x => x.optionDetail.type == "CF").FirstOrDefault();
                        }
                        else
                        {
                            System.Web.HttpContext.Current.Session["SessionId"] = PNRAddErrorResultConfirm.Header.Session.SessionId;
                            System.Web.HttpContext.Current.Session["SequenceNumber"] = Convert.ToInt32(PNRAddErrorResultConfirm.Header.Session.SequenceNumber);
                            System.Web.HttpContext.Current.Session["SecurityToken"] = PNRAddErrorResultConfirm.Header.Session.SecurityToken;
                            System.Web.HttpContext.Current.Session["ControlNumber"] = PNRAddErrorResultConfirm.Body.PNR_Reply.originDestinationDetails.itineraryInfo.generalOption.Where(x => x.optionDetail.type == "CF").FirstOrDefault();
                        }

                        List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCodeFinal);
                        foreach (var itemerror in objBookItemsError)
                        {
                            BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, resultFinal);
                        }

                        #region PNR CANCEL
                        ViewBag.HotelSellResult = "RequestFailed";
                        TempData["Errorcomes"] = "RequestFailed";
                        string ControlNumber = Convert.ToString(System.Web.HttpContext.Current.Session["ControlNumber"]);
                        // string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize);
                        //     return false;
                        StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                        //  BookingStatus = objBooking.BookingDecline(bookingId);
                        BookingStatus = objBooking.BookingCancel(bookingId);
                        BookingStatus = true;
                        return BookingStatus;
                        #endregion

                        //#region GDS Signout
                        //Action = "";
                        //SoapHeaderStateful = APIUtility.SetSecuritySignOutSoapHeader(Action, System.Web.HttpContext.Current.Session["SessionId"].ToString(), Convert.ToInt32(System.Web.HttpContext.Current.Session["SequenceNumber"]), System.Web.HttpContext.Current.Session["SecurityToken"].ToString());
                        //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        //string SignOutSoapBody = APIUtility.SecuritySignOut();
                        //// string SecuritySignOutHeader = APIUtility.SetSecuritySignOutSoapHeader();
                        //string SecuritySignOut = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, SignOutSoapBody);
                        //#endregion 


                    }
                    else
                    {
                        BookingStatus = false;

                        #region PNR_Retrieve
                        Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELPNRRETRIEVEACTION);
                        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, System.Web.HttpContext.Current.Session["SessionId"].ToString(), Convert.ToInt32(System.Web.HttpContext.Current.Session["SequenceNumber"]), System.Web.HttpContext.Current.Session["SecurityToken"].ToString());
                        APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        string PNRRetrieveSoapBody = APIUtility.PNR_Retrieve(Convert.ToString(System.Web.HttpContext.Current.Session["ControlNumber"]));
                        string resultRetrieve = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRRetrieveSoapBody);
                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(System.Web.HttpContext.Current.Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(UserID), (int)CLayer.ObjectStatus.GDSType.PNRRetrieve, bookingId);

                        #endregion Transaction log end
                        #endregion

                        #region GDS Signout
                        Action = "";
                        SoapHeaderStateful = APIUtility.SetSecuritySignOutSoapHeader(Action, System.Web.HttpContext.Current.Session["SessionId"].ToString(), Convert.ToInt32(System.Web.HttpContext.Current.Session["SequenceNumber"]), System.Web.HttpContext.Current.Session["SecurityToken"].ToString());
                        APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        string SignOutSoapBody = APIUtility.SecuritySignOut();
                        // string SecuritySignOutHeader = APIUtility.SetSecuritySignOutSoapHeader();
                        string SecuritySignOut = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, SignOutSoapBody);
                        #endregion 

                        //#region Hotel Completereservationinfodetails
                        //Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELCOMPLETERESERVATION);
                        //SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, System.Web.HttpContext.Current.Session["SessionId"].ToString(), Convert.ToInt32(System.Web.HttpContext.Current.Session["SequenceNumber"])+1, System.Web.HttpContext.Current.Session["SecurityToken"].ToString(), true);
                        //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        //string HotelCompleteReservation = APIUtility.Hotel_CompleteReservationDetails(Convert.ToString(System.Web.HttpContext.Current.Session["ControlNumber"]));
                        //string resultotelCompleteReservation = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, HotelCompleteReservation);


                        //#endregion 
                    }

                    #endregion
                    List<CLayer.BookingItem> objBookItems = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCodeFinal);
                    foreach (var item in objBookItems)
                    {
                        BLayer.BookingItem.UpdateHotelConfirmNumber(item.BookingId, item.AccommodationId, Convert.ToString(System.Web.HttpContext.Current.Session["ControlNumber"]));
                        #region Update Converted Amount details
                        if (System.Web.HttpContext.Current.Session["GDSCurrencyConversion"] != null)
                        {

                            long CountryCode = Convert.ToInt64(System.Web.HttpContext.Current.Session["GDSCountryID"]);
                            CLayer.GDSCurrencyConversions objCurrency = (CLayer.GDSCurrencyConversions)System.Web.HttpContext.Current.Session["GDSCurrencyConversion"];
                            CLayer.BookingItem objBookingItem = new CLayer.BookingItem();
                            objBookingItem.BookingId = item.BookingId;
                            objBookingItem.AccommodationId = item.AccommodationId;
                            objBookingItem.GDSAmount = Convert.ToDecimal(System.Web.HttpContext.Current.Session["GDSAmount"]);
                            objBookingItem.GDSConvertedAmount = Convert.ToDecimal(System.Web.HttpContext.Current.Session["GDSConvertedAmount"]);
                            objBookingItem.GDSConversionRate = objCurrency.RateConversion;
                            objBookingItem.GDSCountry = CountryCode;

                            BLayer.BookingItem.UpdateGDSCurrencyDetails(objBookingItem);

                        }
                        #endregion
                    }



                }
                else if ((HotelId != null && HotelId != "") && (InventoryAPIType != (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus))
                {
                    HotelId = HotelId.Trim();
                    List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
                    CLayer.Address byAddress = BLayer.Bookings.GetBookedByUser(bookingId);
                    CLayer.Booking data = BLayer.Bookings.GetDetails(bookingId);


                    string query_key = data.OrderNo + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                    string BookingsessionId = "bs" + "_" + bookingId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                    string BookingrequestId = "br" + "_" + bookingId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");


                    string errorinrequest = "";


                    #region

                    foreach (CLayer.BookingItem bi in bookitems)
                    {
                        long accid = bi.AccommodationId;
                        string RoomId = BLayer.Accommodation.GetRoomId(accid);

                        if (RoomId != "" && RoomId != null)
                        {
                            RoomId = RoomId.Trim();
                            int totaladult = bi.NoOfAdults + bi.NoOfGuests;
                            List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> HotelRoomDetailsList = StayBazar.Inventory.MaximojoBook.BookingAvailDeserializeJsonResponse(HotelId, data.CheckIn.ToString("yyyy-MM-dd"), data.CheckOut.ToString("yyyy-MM-dd"), totaladult, bi.NoOfChildren, bi.NoOfAccommodations, query_key, BookingsessionId, BookingrequestId);

                            //Hotel rooms list filter by rooms only plan

                            List<string> newroomtypelist = new List<string>();
                            List<string> Multipleroomtypes = new List<string>();

                            foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails u in HotelRoomDetailsList)
                            {
                                if (!newroomtypelist.Contains(u.roomtype_id))
                                {
                                    newroomtypelist.Add(u.roomtype_id);
                                }
                                else
                                {
                                    if (!Multipleroomtypes.Contains(u.roomtype_id))
                                    {
                                        Multipleroomtypes.Add(u.roomtype_id);
                                    }
                                }
                            }

                            List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> NewHotelRoomDetailsList = new List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails>();

                            foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails RId in HotelRoomDetailsList)
                            {
                                if (!Multipleroomtypes.Contains(RId.roomtype_id))
                                {
                                    NewHotelRoomDetailsList.Add(RId);
                                }
                                else
                                {
                                    //// check name contains Room Only
                                    //string[] SplitRoomonlyterm = RId.room_name.Split('-');
                                    //if (SplitRoomonlyterm[1].Trim() == "Room only")
                                    //{
                                    //    NewHotelRoomDetailsList.Add(RId);
                                    //}

                                    ////if (RId.RatePlanId == System.Configuration.ConfigurationManager.AppSettings.Get("rateplanidformaximojo"))
                                    ////{
                                    ////    NewHotelRoomDetailsList.Add(RId);
                                    ////}

                                    if (RId.room_name.Contains("Room only"))
                                    {
                                        NewHotelRoomDetailsList.Add(RId);
                                    }

                                }
                            }



                            // add rooms without room only type (add min one)

                            foreach (string ff in Multipleroomtypes)
                            {
                                if (!NewHotelRoomDetailsList.Exists(a => a.roomtype_id == ff))
                                {
                                    foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails RId in HotelRoomDetailsList)
                                    {
                                        if (RId.roomtype_id == ff)
                                        {
                                            if (!NewHotelRoomDetailsList.Exists(a => a.roomtype_id == ff))
                                            {
                                                NewHotelRoomDetailsList.Add(RId);
                                            }
                                        }
                                    }
                                }
                            }



                            if (NewHotelRoomDetailsList.Exists(a => a.roomtype_id == RoomId))
                            {
                                #region
                                foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails ro in NewHotelRoomDetailsList)
                                {
                                    if (RoomId == ro.roomtype_id)
                                    {
                                        try
                                        {
                                            #region
                                            //BOOKING REQUEST PASS
                                            //CLayer.BookingExternalInventory roomdet = BLayer.BookingExternalInventory.GetAllDetailsByRoomIdandHotelId(RoomId, HotelId);
                                            List<StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room> RoomsList = new List<StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room>();
                                            StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room Lrooms = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room();

                                            StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party fff = new Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party();
                                            fff.adults = bi.NoOfAdults;

                                            List<object> chdrn = new List<object>();

                                            for (int i = 0; i < bi.NoOfChildren; i++)
                                            {
                                                chdrn.Add(5);
                                            }

                                            fff.children = chdrn;

                                            //multiple accommoadtion 
                                            for (int i = 0; i < bi.NoOfAccommodations; i++)
                                            {
                                                Lrooms.party = fff;
                                                Lrooms.traveler_first_name = byAddress.Firstname;
                                                Lrooms.traveler_last_name = byAddress.Firstname;
                                                RoomsList.Add(Lrooms);
                                            }


                                            var Book_WihoutPayobj = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.RootObject
                                            {
                                                checkin_date = data.CheckIn.ToString("yyyy-MM-dd"),
                                                checkout_date = data.CheckOut.ToString("yyyy-MM-dd"),
                                                hotel_id = HotelId,
                                                reference_id = data.OrderNo + "_" + ro.roomtype_id + "_" + ro.RatePlanId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt"),
                                                ip_address = IpAdds,
                                                customer = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Customer
                                                {
                                                    first_name = byAddress.Firstname,
                                                    last_name = byAddress.Firstname,
                                                    email = byAddress.Email,
                                                    phone_number = byAddress.Mobile,
                                                    country = byAddress.CountryCode
                                                },
                                                final_price_at_booking = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.FinalPriceAtBooking
                                                {
                                                    amount = Convert.ToDouble(ro.final_price_at_bookingamt * bi.NoOfAccommodations),
                                                    currency = ro.final_price_at_bookingamtcurr
                                                },
                                                final_price_at_checkout = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.FinalPriceAtCheckout
                                                {
                                                    amount = Convert.ToDouble(ro.final_price_at_checkoutamt),
                                                    currency = ro.final_price_at_checkoutamtcurr
                                                },
                                                partner_data = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.PartnerData
                                                {
                                                    DomainId = ro.DomainId,
                                                    HotelId = HotelId,
                                                    PromotionId = ro.PromotionId,
                                                    RatePlanId = ro.RatePlanId,
                                                    RoomId = ro.roomtype_id
                                                },
                                                rooms = RoomsList
                                            };




                                            //bookingsubmitWithoutPay
                                            string ResponseBookSub = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Submit_Without_paypaldetails(Book_WihoutPayobj);

                                            errorinrequest = ResponseBookSub;
                                            StayBazar.Inventory.Models.MaxBooking_Submit_Response.RootObject Bookingdetails = JsonConvert.DeserializeObject<StayBazar.Inventory.Models.MaxBooking_Submit_Response.RootObject>(ResponseBookSub);





                                            //Save Booking Response by booking submit 

                                            CLayer.BookingExternalInventory ExternalBook = new CLayer.BookingExternalInventory();
                                            ExternalBook.BookingId = bookingId;
                                            ExternalBook.hotel_id = Bookingdetails.reservation.hotel_id;
                                            ExternalBook.hotel_name = Bookingdetails.reservation.hotel.name;

                                            ExternalBook.Reference_Id = Bookingdetails.reference_id;
                                            ExternalBook.reservation_id = Bookingdetails.reservation.reservation_id;


                                            if (Bookingdetails.status == "Success")
                                            {
                                                ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Success;
                                            }
                                            else if (Bookingdetails.status == "Failure")
                                            {
                                                ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Failure;
                                            }
                                            else if (Bookingdetails.status == "Pending")
                                            {
                                                ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Pending;
                                            }
                                            else
                                            {
                                                ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.None;
                                            }


                                            if (Bookingdetails.reservation.status == "Booked")
                                            {
                                                ExternalBook.ReservationStatus = (int)CLayer.BookingExternalInventory.ReservationStatusenum.Booked;
                                            }
                                            else
                                            {
                                                ExternalBook.ReservationStatus = (int)CLayer.BookingExternalInventory.ReservationStatusenum.none;
                                            }

                                            //if status failure/pending/reservation status error
                                            string VerifyRequestResp = "Booked Successfully";

                                            if (ExternalBook.BookingStatus != 1 || ExternalBook.ReservationStatus != 1)
                                            {
                                                VerifyRequestResp = StayBazar.Inventory.MaximojoBook.Maximojo_Booking_Verify(HotelId, Bookingdetails.reference_id, Bookingdetails.reservation.reservation_id);
                                            }
                                            ExternalBook.StatusDetails = VerifyRequestResp;
                                            ExternalBook.roomtype_id = ro.roomtype_id;
                                            ExternalBook.room_name = ro.room_name;
                                            ExternalBook.room_desc = ro.room_desc;

                                            ExternalBook.CheckInDate = Bookingdetails.reservation.checkin_date;
                                            ExternalBook.CheckOutDate = Bookingdetails.reservation.checkout_date;
                                            ExternalBook.CustomerId = byAddress.UserId;
                                            ExternalBook.IpAddtress = IpAdds;
                                            ExternalBook.Response = ResponseBookSub;
                                            ExternalBook.BookingApiType = (int)CLayer.BookingExternalInventory.BookingApiTypes.Maximojo;
                                            ExternalBook.final_price_at_bookingamt = ro.final_price_at_bookingamt;
                                            ExternalBook.final_price_at_checkoutamt = ro.final_price_at_checkoutamt;
                                            ExternalBook.DomainId = ro.DomainId;
                                            ExternalBook.PromotionId = ro.PromotionId;
                                            ExternalBook.RatePlanId = ro.RatePlanId;
                                            ExternalBook.query_key = query_key;
                                            ExternalBook.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.none;
                                            ExternalBook.BookingsessionId = BookingsessionId;
                                            ExternalBook.BookingrequestId = BookingrequestId;
                                            long BookingExtInvReqId = BLayer.BookingExternalInventory.SaveBookingSubmitResponse(ExternalBook);

                                            //Save rooms details

                                            CLayer.BookingExternalInventory roomdt = new CLayer.BookingExternalInventory();
                                            roomdt.BookingExtInvReqId = BookingExtInvReqId;
                                            roomdt.hotel_id = ro.hotel_id;
                                            roomdt.hotel_name = ro.hotel_name;
                                            roomdt.roomtype_id = ro.roomtype_id;
                                            roomdt.room_name = ro.room_name;
                                            roomdt.room_desc = ro.room_desc;
                                            roomdt.final_price_at_bookingamt = ro.final_price_at_bookingamt;
                                            roomdt.final_price_at_bookingamtcurr = ro.final_price_at_bookingamtcurr;
                                            roomdt.final_price_at_checkoutamt = ro.final_price_at_checkoutamt;
                                            roomdt.final_price_at_checkoutamtcurr = ro.final_price_at_checkoutamtcurr;
                                            roomdt.DomainId = ro.DomainId;
                                            roomdt.PromotionId = ro.PromotionId;
                                            roomdt.RatePlanId = ro.RatePlanId;
                                            BLayer.BookingExternalInventory.Save(roomdt);

                                            //CANCELLATION WHEN BOOKING FAILED 

                                            if (Bookingdetails.status != "Success")
                                            {
                                                // BOOKING CANCEL FROM EXTERNAL INVENTORY REQUEST

                                                ExternalBookRequestCancel(bookingId);

                                                //Redirecting after cancelling  all booked by user                                               
                                                TempData["Errorcomes"] = "RequestFailed";
                                                return true;
                                                //return View("~/Views/Payment/RequestFailed.cshtml", propertyId);

                                            }
                                            #endregion
                                        }
                                        catch (Exception ex)
                                        {
                                            ExternalBookRequestCancel(bookingId);
                                            Exception Error = new Exception("#Error from  External Booking Submit Request(Payment , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex + errorinrequest);
                                            Common.LogHandler.HandleError(Error);
                                            TempData["Errorcomes"] = "internalerror";
                                            return true;
                                        }
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                // BOOKING CANCEL FROM EXTERNAL INVENTORY REQUEST
                                ExternalBookRequestCancel(bookingId);
                                //Redirecting after cancelling  all booked by user                                               
                                TempData["Errorcomes"] = "RequestFailed";
                                return true;
                            }
                        }
                        //else if
                    }

                    #endregion

                }
            }
            catch (Exception ex)

            {
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    AmedusHotelBookingCancel("");
                }
                else
                {
                    ExternalBookRequestCancel(bookingId);

                }
                Exception Error = new Exception("#Error from External Booking Submit Request(Payment , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
                TempData["Errorcomes"] = "internalerror";
                return true;
            }
            return false;

        }
        public ActionResult AmedusHotelBookingCancel(string ControlRefNumber, int optioncode = 0)
        {
            try
            {

                string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELPNRCANCELACTION); ;

                string pnrcancelSoapBody = APIUtility.PNR_Cancel(ControlRefNumber, optioncode);

                string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());


                string result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, pnrcancelSoapBody);



                //Serializer pnrser = new Serializer();
                //CLayer.PNRAddElements.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElements.Envelope>(result);


            }
            catch (Exception ex)
            {
                Common.LogHandler.LogError(ex);
            }

            return null;
        }
        public ActionResult ExternalBookRequestCancel(long bookingId)
        {

            try
            {
                List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(bookingId);
                foreach (CLayer.BookingExternalInventory reqbook in DtBookReq)
                {
                    var Book_Cancelobj = new StayBazar.Inventory.Models.MaxBooking_Cancel.RootObject
                    {
                        hotel_id = reqbook.hotel_id,
                        reservation_id = reqbook.reservation_id
                    };

                    string ResponseCancel = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Cancel(Book_Cancelobj);

                    StayBazar.Inventory.Models.MaxBooking_Cancel_Response.RootObject Bookingcanceldetails = JsonConvert.DeserializeObject<StayBazar.Inventory.Models.MaxBooking_Cancel_Response.RootObject>(ResponseCancel);

                    //Saving Cancel Details of external booking request

                    CLayer.BookingExternalInventory bookcandt = new CLayer.BookingExternalInventory();

                    bookcandt.BookingExtInvReqId = reqbook.BookingExtInvReqId;
                    bookcandt.BookingId = reqbook.BookingId;
                    bookcandt.reservation_id = reqbook.reservation_id;

                    if (Bookingcanceldetails.status == "Success")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.Success;
                        //UPDATE BOOKING EXTERNAL INVENTORY REQUEST STATUS CHANGE
                    }
                    else if (Bookingcanceldetails.status == "AlreadyCancelled")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.AlreadyCancelled;
                    }
                    else if (Bookingcanceldetails.status == "CannotBeCancelled")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.CannotBeCancelled;
                    }
                    else if (Bookingcanceldetails.status == "UnknownReference")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.UnknownReference;
                    }
                    else if (Bookingcanceldetails.status == "Error")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.Error;
                    }
                    else
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.none;
                    }
                    //UPDATE BOOKING STATUS
                    int CacelSts = bookcandt.Cancellation_Status;
                    BLayer.BookingExternalInventory.UpdateCancellationStatus(reqbook.BookingExtInvReqId, CacelSts);
                    bookcandt.Cancellation_Number = Bookingcanceldetails.cancellation_number;
                    if (Bookingcanceldetails.cancelled_date == null) { Bookingcanceldetails.cancelled_date = Convert.ToString(DateTime.Now); }
                    bookcandt.Cancelled_Date = Bookingcanceldetails.cancelled_date;
                    bookcandt.Cancellation_Response = ResponseCancel;
                    BLayer.BookingExternalInventory.SaveBookingCancelResponse(bookcandt);
                }
            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from External Booking Request Cancel (Payment , ExternalBookRequestCancel) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
            }
            return null;


        }

        public ActionResult PaymentLinkPayment(Guid PaymentGuid)
        {
            try
            {
                return View(LoadValOffPaymentLinkPayment(PaymentGuid));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.PaymentLinkPaymentModel());

        }
        public Models.PaymentRequestModel LoadValOffPaymentLinkPayment(Guid PaymentGuid)
        {
            Models.PaymentRequestModel data = new Models.PaymentRequestModel();
            Models.PaymentRequestModel details = null;
            //CLayer.OfflineBooking data = BLayer.OfflineBooking.GetOfflineBookingCustomerDetailsByID(CustomerId);
            long LoggedInUser = Convert.ToInt64(System.Web.HttpContext.Current.Session["LoggedInUser"]);
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForPaymentList_DetailsForMail(PaymentGuid);
            details = new Models.PaymentRequestModel()
            {
                //UserId = data.CustomerId,
                OfflineBookingList = users,
                //GrandTotal = users.First().SumTotalSalePrice - users.First().AdvanceReceived,
                GrandTotal = users.Sum(m => m.PayableSalePrice),
                PaymentGuid = PaymentGuid,

            };
            //data.UserId = CustomerId;
            return details;
        }
        public static string TamrindMultiSingleAvailability(CLayer.SearchCriteria sch)
        {
            TamarindService.TamarindDataServiceClient client = new TamarindService.TamarindDataServiceClient();

            client.ClientCredentials.UserName.UserName = "STAYBAZARXMLTEST";
            client.ClientCredentials.UserName.Password = "STAYBAZARXMLTEST";

            string CityId = BLayer.City.GetTamarindCityID(sch.Destination);

            //SearchController hotellist = new SearchController();
            //hotellist.TamarindHotelList_Save(CityId);

            var dateAndTime = sch.CheckIn;
            TamarindService.HotelSearchParam HotelParm = new TamarindService.HotelSearchParam();
            HotelParm.AgentMarket = "0";
            HotelParm.AllowOnrequest = Convert.ToBoolean("True");
            HotelParm.CityID = "BOM";
            HotelParm.CheckinDate = Convert.ToDateTime(dateAndTime);
            HotelParm.Nights = 1;
            HotelParm.SingleBed = 1;
            HotelParm.DoubleBed = 0;
            HotelParm.TwinBed = 0;
            HotelParm.ExtraBed = 0;
            HotelParm.OriginCountryID = "IN";
            HotelParm.HotelID = sch.HotelCode;
            HotelParm.Rating = "";
            string hotel = client.GetHotels(HotelParm);

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

    }
}