using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Linq;

using System.Threading.Tasks;
using System.Reflection;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Xml;
using System.Net;


namespace ExcelReport
{

    class UpdateStarRating
    {



        public bool UpdatePropertyStarRatings()
        {
            bool result = false;
            long id1 = 0;
            string HotelID = string.Empty;
            try
            {
                String hotelcode = string.Empty;

                List<CLayer.Property> objProperties = BLayer.Property.GetAllGDSProperties().ToList();
                CLayer.GDSBookingDetails.Envelope ss = new CLayer.GDSBookingDetails.Envelope();
                CLayer.GDSBookingDetailsAdv.Envelope ssAdv = new CLayer.GDSBookingDetailsAdv.Envelope();// ser.Deserialize<CLayer.GDSBookingDetailsAdv.Envelope>(hotel);
                long id = 0;
                foreach (var pitem in objProperties)
                {

                    try
                    {
                      //  #region Update Property Description
                        StringBuilder Description = new StringBuilder();
                        //Update property description start
                        Serializer ser = new Serializer();
                        id = pitem.PropertyId;
                        id1 = id;
                        string HotelCode = pitem.HotelID;
                        HotelID = HotelCode;
                        string hotel = GetGDS_Hotel_Details(HotelCode);



                        ss = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);

                        DataTable dtHotel = BLayer.Property.GetHotelFormattedDescription(id);
                        string FormattedDescription = string.Empty;
                        if (dtHotel.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtHotel.Rows)
                            {
                                FormattedDescription = Convert.ToString(dr["FormattedDescription"]);

                            }
                        }

                        FormattedDescription = GDSProcess.GDSFormatDescription.GetFormattedDescription(hotel);



                        int StarRatings = GDSProcess.GDSFormatDescription.StarRatings;
                        int LocalStarRating = APIUtility.GetStarRating(hotel);
                        if (LocalStarRating > 0)
                        {
                            BLayer.Property.GDSUpdatePropertyStarRatings(id, LocalStarRating);
                        }
                        if ((LocalStarRating == 0) && (StarRatings == 0))
                        {
                            BLayer.Property.GDSUpdatePropertyStarRatings(id, 0);
                        }



                    }
                    catch (Exception ex)
                    {
                        //  LogHandler.AddLog("Error in-"+hotelcode);
                        WriteToLog(HotelID.ToString() + "-" + ex.Message, true);
                    }
                    finally
                    {

                    }

                }
                result = true;

            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
                string a = id1.ToString();
                result = false;

                throw ex;
            }
            return result;
        }
        public void WriteToLog(string pHotelCode, bool IsError = false)
        {
            //string path = System.Environment.CurrentDirectory+"\\Log.txt";
            string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\log.txt";


            if (IsError)
            {
                File.AppendAllText(path, "ERROR -" + pHotelCode + "-" + DateTime.Now.ToString() + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(path, "SUCCESS -" + pHotelCode + "-" + DateTime.Now.ToString() + Environment.NewLine);
            }

        }
        public string GetGDS_Hotel_Details(string HotelCode)
        {
            string soapResult = string.Empty;
            try
            {
                try
                {
                    string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                    var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                                                                                          //  var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/OTA_HotelDescriptiveInfoRQ_07.1_1A2007A";
                                                                                          // var _url = "https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                    var _action = "http://webservices.amadeus.com/OTA_HotelDescriptiveInfoRQ_07.1_1A2007A";


                    string SoapHeader = APIUtility.hotel_descriptiveinfor_SetSoapHeader(_url, _action);
                    string SoapBody = APIUtility.hotel_descriptiveinfor_detailsbody(HotelCode);

                    string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                    SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                    SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                    SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";
                    XmlDocument soapEnvelopeXml = new XmlDocument();
                    soapEnvelopeXml.LoadXml(SoapEnvelop);



                    HttpWebRequest webRequest = CreateWebRequest(_url, _action);
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
                        Console.WriteLine("Got response for Hotel code: " + HotelCode);
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
                    Console.WriteLine("Got response(second try) for Hotel code: " + HotelCode);
                }
            }
            catch (Exception ex)
            {
                soapResult = "";
                Console.WriteLine("Failed to get response for Hotel code: " + HotelCode);
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
