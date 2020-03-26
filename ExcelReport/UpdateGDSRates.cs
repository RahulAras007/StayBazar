using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Reflection;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Xml;
using System.Net;
using System.Linq;
using System.Globalization;


namespace ExcelReport
{
    public class UpdateGDSRates
    {



        public bool UpdatePropertyRates()
        {
            bool Output = false;
            long id1 = 0;
            string start = "";
            string End = "";
            try
            {
                String hotelcode = string.Empty;

                List<CLayer.Property> objProperties = BLayer.Property.GetAllGDSPropertiesRecommended().ToList();

                CLayer.GDSBookingDetails.Envelope ss = new CLayer.GDSBookingDetails.Envelope();
                CLayer.GDSBookingDetailsAdv.Envelope ssAdv = new CLayer.GDSBookingDetailsAdv.Envelope();// ser.Deserialize<CLayer.GDSBookingDetailsAdv.Envelope>(hotel);
                long id = 0;
                foreach (var pitem in objProperties)
                {
                    try
                    {
                        StringBuilder Description = new StringBuilder();
                        Serializer ser = new Serializer();
                        id = pitem.PropertyId;
                        id1 = id;
                        string HotelCode = pitem.HotelID;
                        string GDSCountry = pitem.Countryname;

                        //    string hotel = GetGDS_Hotel_Details(HotelCode);


                        start = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");
                        End = DateTime.Now.Date.AddDays(2).ToString("yyyy-MM-dd");

                        #region Rate Calculation
                        string result = HotelMultiSingleAvailability(HotelCode, start, End);

                        Serializer ser1 = new Serializer();
                        CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                        CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                        CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();


                        try
                        {

                            HotelResult = ser1.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                HotelResultAdv = ser1.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                            }
                            catch (Exception ex1)
                            {
                                HotelResultAdvFirst = ser1.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                            }

                        }
                        if (HotelResult.Body != null)
                        {
                            decimal GDSRateConversion = 0;
                            string GDSCurrencyCode = "INR";
                            if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                            {
                                var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                                CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                                objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                                GDSRateConversion = objCurrencyConversion.RateConversion;
                                objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                                objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                                objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                                GDSCurrencyCode = objCurrencyConversion.SourceCurrencyCode;
                                //  Session["GDSCurrencyConversion"] = objCurrencyConversion;
                            }
                            //if (GDSCountry == "")
                            //{
                            //     Session["GDSCurrencyConversion"] = null;
                            //}


                            if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                            {
                                foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                                {

                                    //    InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                    string HotelId = item.BasicPropertyInfo.HotelCode;

                                    List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(id);

                                    long AccomodationId = 0;
                                    string RoomStayRPH = item.RoomStayRPH;
                                    List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                    if (!string.IsNullOrEmpty(RoomStayRPH))
                                    {

                                        string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                        RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, GDSCurrencyCode, GDSRateConversion);
                                    }
                                    CultureInfo culture = CultureInfo.CurrentCulture;

                                    CLayer.GDSRates Rdata = new CLayer.GDSRates();
                                    string BookingCode = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                                    Rdata.Rate = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().AmountAfterTax;
                                    Rdata.RateFor = 2;
                                    long userId = 0;
                                    Rdata.UpdatedBy = 1;
                                    Rdata.StartDate = DateTime.Parse(start, CultureInfo.CurrentCulture);
                                    Rdata.EndDate = DateTime.Parse(End, CultureInfo.CurrentCulture);
                                    Rdata.BookingCode = BookingCode;
                                    Rdata.Status = 1;
                                    Rdata.AccommodationId = objAcc.Where(X => X.BookingCode == BookingCode).FirstOrDefault().AccommodationId;
                                    long gdsRateId = BLayer.Rate.GDSRateSave(Rdata);
                                }

                            }
                            else
                            {
                                start = DateTime.Now.Date.AddDays(4).ToString("yyyy-MM-dd");
                                End = DateTime.Now.Date.AddDays(5).ToString("yyyy-MM-dd");
                                UpdateGDSRate(id, HotelCode, start, End);
                            }

                        }

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        LogHandler.AddLog("Error rate updation-Hotel ID" + id.ToString());
                    }
                

                }
                
                Output = true;

            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
                string a = id1.ToString();
                Output = false;
                //#region Transaction Log 
                //APIUtility.GenerateGDSTransactionLog("", "BulkImageandDescriptionFailure", 0, (int)CLayer.ObjectStatus.GDSType.BulkHotelImageDescriptionUpdation, 0);
                //#endregion Transaction Log end
                throw ex;
            }
            return Output;
        }
        public void UpdateGDSRate(long PropertyID, string HotelCode, string Start, string End)
        {
            long id = PropertyID;


            try
            {

                #region Rate Calculation
                string result = HotelMultiSingleAvailability(HotelCode, Start, End);

                Serializer ser1 = new Serializer();
                CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();


                try
                {

                    HotelResult = ser1.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                }
                catch (Exception ex)
                {
                    try
                    {
                        HotelResultAdv = ser1.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                    }
                    catch (Exception ex1)
                    {
                        HotelResultAdvFirst = ser1.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                    }

                }
                if (HotelResult.Body != null)
                {
                    decimal GDSRateConversion = 0;
                    string GDSCurrencyCode = "INR";
                    if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        GDSRateConversion = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        GDSCurrencyCode = objCurrencyConversion.SourceCurrencyCode;
                        //  Session["GDSCurrencyConversion"] = objCurrencyConversion;
                    }
                    //if (GDSCountry == "")
                    //{
                    //     Session["GDSCurrencyConversion"] = null;
                    //}


                    if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                    {
                        foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                        {

                            //    InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            string HotelId = item.BasicPropertyInfo.HotelCode;

                            List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(id);

                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {

                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, GDSCurrencyCode, GDSRateConversion);
                            }
                            CultureInfo culture = CultureInfo.CurrentCulture;

                            CLayer.GDSRates Rdata = new CLayer.GDSRates();
                            string BookingCode = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                            Rdata.Rate = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().AmountAfterTax;
                            Rdata.RateFor = 2;
                            long userId = 0;
                            Rdata.UpdatedBy = 1;
                            Rdata.StartDate = DateTime.Parse(Start, CultureInfo.CurrentCulture);
                            Rdata.EndDate = DateTime.Parse(End, CultureInfo.CurrentCulture);
                            Rdata.BookingCode = BookingCode;
                            Rdata.Status = 1;
                            Rdata.AccommodationId = objAcc.Where(X => X.BookingCode == BookingCode).FirstOrDefault().AccommodationId;
                            long gdsRateId = BLayer.Rate.GDSRateSave(Rdata);
                        }

                    }
                    else
                    {
                        //Start = DateTime.Now.Date.AddDays(9).ToString("yyyy-MM-dd");
                        //End = DateTime.Now.Date.AddDays(10).ToString("yyyy-MM-dd");
                        //UpdateGDSRates(PropertyID, HotelCode, Start, End);

                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                #region Rate Calculation
                string result = HotelMultiSingleAvailability(HotelCode, Start, End);

                Serializer ser1 = new Serializer();
                CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();


                try
                {

                    HotelResult = ser1.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                }
                catch (Exception ex2)
                {
                    try
                    {
                        HotelResultAdv = ser1.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                    }
                    catch (Exception ex1)
                    {
                        HotelResultAdvFirst = ser1.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                    }

                }
                if (HotelResult.Body != null)
                {
                    decimal GDSRateConversion = 0;
                    string GDSCurrencyCode = "INR";
                    if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        GDSRateConversion = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        GDSCurrencyCode = objCurrencyConversion.SourceCurrencyCode;
                        //  Session["GDSCurrencyConversion"] = objCurrencyConversion;
                    }
                    //if (GDSCountry == "")
                    //{
                    //     Session["GDSCurrencyConversion"] = null;
                    //}


                    if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                    {
                        foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                        {

                            //    InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            string HotelId = item.BasicPropertyInfo.HotelCode;

                            List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(id);

                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {

                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, GDSCurrencyCode, GDSRateConversion);
                            }
                            CultureInfo culture = CultureInfo.CurrentCulture;

                            CLayer.GDSRates Rdata = new CLayer.GDSRates();
                            string BookingCode = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                            Rdata.Rate = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().AmountAfterTax;
                            Rdata.RateFor = 2;
                            long userId = 0;
                            Rdata.UpdatedBy = 1;
                            Rdata.StartDate = DateTime.Parse(Start, CultureInfo.CurrentCulture);
                            Rdata.EndDate = DateTime.Parse(End, CultureInfo.CurrentCulture);
                            Rdata.BookingCode = BookingCode;
                            Rdata.Status = 1;
                            Rdata.AccommodationId = objAcc.Where(X => X.BookingCode == BookingCode).FirstOrDefault().AccommodationId;
                            long gdsRateId = BLayer.Rate.GDSRateSave(Rdata);
                        }

                    }
                    else
                    {

                    }

                }

                #endregion
            }
        }
        public static List<CLayer.RoomStaysResult> GetRoomStays(CLayer.HotelAvailability.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0, string GDSCountry = "")
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
            try
            {


                for (int i = 0; i < pRPH.Length; i++)
                {
                    var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                    CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                    if (RoomStayItem[0].RoomRates != null)
                    {

                        objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Rates.Rate.Base.AmountBeforeTax);
                        objRoomStay.AmountBeforeTax = (GDSCountry == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax, ConversionRate);
                        objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                        objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                        // objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                        objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;
                        //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        //{
                        //    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                        if ((RequestedCurrencyCode != objRoomStay.CurrencyCode) && (RequestedCurrencyCode != ""))
                        {
                            objRoomStay.AmountAfterTax = Math.Round(objRoomStay.AmountAfterTax * ConversionRate, 0);

                        }
                        else
                        {
                            objRoomStay.AmountAfterTax = (GDSCountry == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax, ConversionRate);
                        }
                        //}

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
                        //  objRoomStay.RoomTypeDescription = APIUtility.GenerateRoomDescription(RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode);
                        // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;

                    }




                    RoomStaysResultList.Add(objRoomStay);

                }
                // return RoomStaysResultList;
            }
            catch (Exception ex)
            {
                RoomStaysResultList = null;
            }
            return RoomStaysResultList;
        }
        public static List<CLayer.RoomStaysResult> GetRoomStaysAdv(CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0, string GDSCountry = "")
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();

            for (int i = 0; i < pRPH.Length; i++)
            {
                var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                if (RoomStayItem[0].RoomRates != null)
                {

                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Total.AmountBeforeTax);
                    objRoomStay.AmountBeforeTax = (GDSCountry == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax, ConversionRate);

                    objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                    objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                    objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;
                    //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    //{
                    //    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                    if ((RequestedCurrencyCode != objRoomStay.CurrencyCode) && (RequestedCurrencyCode != ""))
                    {
                        objRoomStay.AmountAfterTax = Math.Round(objRoomStay.AmountAfterTax * ConversionRate, 0);
                    }
                    else
                    {
                        objRoomStay.AmountAfterTax = (GDSCountry == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax, ConversionRate);
                    }
                    //}


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
                    //     objRoomStay.RoomTypeDescription = APIUtility.GenerateRoomDescription(RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode);

                    // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
                }




                RoomStaysResultList.Add(objRoomStay);

            }
            return RoomStaysResultList;
        }


        public static string HotelMultiSingleAvailability(string hotelcode, string start, string end)
        {
            string soapResult = string.Empty;
            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/Hotel_MultiSingleAvailability_10.0";


                string SoapHeader = APIUtility.SetSoapHeaderPriceingdetails(_url, _action);
                string SoapBody = APIUtility.SetHotelMultiSingleAvailabilityBodyPriceingdetails(hotelcode, start, end);

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";

                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(SoapEnvelop);

                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                webRequest.Proxy = null;
                webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

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
                    //    Console.Write(soapResult);
                    Console.WriteLine("update gds rates for Hotel code: " + hotelcode);
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
                Console.WriteLine("Failed to get gds rates for Hotel code: " + hotelcode);
            }

            return soapResult;
        }


        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
#if DEBUG
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif 
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
#if !DEBUG
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif 
            webRequest.Headers.Add("SOAPAction", action);
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





    }
}

