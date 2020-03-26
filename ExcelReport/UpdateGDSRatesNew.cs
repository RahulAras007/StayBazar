using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReport
{
    class UpdateGDSRatesNew
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
        public static string HotelMultiSingleAvailability(string hotelcode, string start, string end)
        {
            string soapResult = string.Empty;
            try
            {
               
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
    }
}
