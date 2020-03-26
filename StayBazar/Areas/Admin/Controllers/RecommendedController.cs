using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using StayBazar.Common;
using System.Xml;
using System.Net;
using System.IO;

namespace StayBazar.Areas.Admin.Controllers
{

    public class RecommendedController : Controller
    {
        //
        // GET: /Admin/Recommended/

        #region Methods

        private Models.RecommendedModel GetAllRecommended(long? ManageFor)
        {
            Models.RecommendedModel result = new Models.RecommendedModel();
            //   result.Items = BLayer.Recommended.GetAllByManage(ManageFor.Value);
            result.Items = BLayer.Recommended.GetAllByManageWithGDS(ManageFor.Value);
            result.ManageFor = Convert.ToInt32(ManageFor.Value);
            return result;
        }

        #endregion

        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            return View(GetAllRecommended(2));
        }

        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult Browse(string searchText, string srchOptions)
        {
            try
            {
                bool searchOption = (srchOptions.Trim() == "supplier")? true : false;
                List<CLayer.Property> result = BLayer.Property.SearchProperty(searchText, searchOption);
                
                return View("_BrowseResult",result);
            }catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                ContentResult cr = new ContentResult();
                cr.Content = "<div class=\"alert alert-warning\">An error occured</div>";
                return cr;
            }
        }
        //[Common.AdminRolePermission]
        //[HttpPost]
        //public ActionResult Save(Models.RecommendedModel data)
        //{
        //    try
        //    {
        //        if(ModelState.IsValid)
        //        {
        //            DateTime tda = DateTime.Today;
        //            if (data.PropertyId == 0) RedirectToAction("Index");
        //            CLayer.Recommended rec = new CLayer.Recommended();
        //            rec.PropertyId = data.PropertyId;
        //            DateTime.TryParse(data.StartDate, out tda);
        //            rec.StartDate =tda;
        //            tda = DateTime.Today;
        //            DateTime.TryParse(data.EndDate, out tda);
        //            rec.EndDate = tda;
        //            rec.Order = data.Order;
        //            rec.Status = data.Status;
        //            rec.ManageFor = data.ManageFor;
        //            long userid = 0;                 
        //            long.TryParse(User.Identity.GetUserId(), out userid);
        //            rec.UpdatedBy = userid;
        //            BLayer.Recommended.Save(rec);
        //        }

        //    }catch(Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //        RedirectToAction("Index", "ErrorPage");
        //    }
        // ////   RedirectToAction("Index", "Recommended", new { area = "Admin" });
        //  return  RedirectToAction("Index", "Recommended", new { area = "Admin" });
        //}
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult Save(Models.RecommendedModel data)
        {
            string start = "";
            string End = "";
            try
            {
                if (ModelState.IsValid)
                {
                    DateTime tda = DateTime.Today;
                    CLayer.Recommended rec = new CLayer.Recommended();
                    if (data.PropertyId == 0) RedirectToAction("Index");
                    int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(data.PropertyId);
                    if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                    {
                        rec.PropertyId = data.PropertyId;
                        DataTable dtHoelCode = BLayer.Property.GetHotelIDFrompropertyid(rec.PropertyId);
                        var hotelcode = dtHoelCode.Rows[0]["Hotel_Id"].ToString();
                        start = DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd");
                        End = DateTime.Now.Date.AddDays(2).ToString("yyyy-MM-dd");

                        #region Rate Calculation
                        string result = HotelMultiSingleAvailability(hotelcode, start, End);

                        StayBazar.Common.Serializer ser1 = new StayBazar.Common.Serializer();
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
                            }
                            if (Convert.ToString(Session["GDSCountry"]) == "")
                            {
                                Session["GDSCurrencyConversion"] = null;
                            }


                            if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                            {
                                foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                                {

                                    InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                    string HotelId = item.BasicPropertyInfo.HotelCode;

                                    List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(rec.PropertyId);

                                    long AccomodationId = 0;
                                    string RoomStayRPH = item.RoomStayRPH;
                                    List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                    if (!string.IsNullOrEmpty(RoomStayRPH))
                                    {

                                        string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                        RoomStaysResultList = StayBazar.Controllers.PropertyController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                                    }
                                    CultureInfo culture = CultureInfo.CurrentCulture;

                                    CLayer.GDSRates Rdata = new CLayer.GDSRates();
                                    string BookingCode = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                                    Rdata.Rate = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().AmountAfterTax;
                                    Rdata.RateFor = data.ManageFor;
                                    long userId = 0;
                                    long.TryParse(User.Identity.GetUserId(), out userId);
                                    Rdata.UpdatedBy = userId;
                                    Rdata.StartDate = DateTime.Parse(start, CultureInfo.CurrentCulture);
                                    Rdata.EndDate = DateTime.Parse(End, CultureInfo.CurrentCulture);
                                    Rdata.BookingCode = BookingCode;
                                    Rdata.Status = 1;
                                    Rdata.AccommodationId = objAcc.Where(X => X.BookingCode == BookingCode).FirstOrDefault().AccommodationId;
                                    long gdsRateId = BLayer.Rate.GDSRateSave(Rdata);

                                }

                            }

                        }
                        else if (HotelResultAdvFirst.Body != null)
                        {

                            if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                            {
                                var itemAdv = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                                CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                                objCurrencyConversion.RateConversion = itemAdv.CurrencyConversion.RateConversion;
                                Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                                objCurrencyConversion.DecimalPlaces = itemAdv.CurrencyConversion.DecimalPlaces;
                                objCurrencyConversion.RequestedCurrencyCode = itemAdv.CurrencyConversion.RequestedCurrencyCode;
                                objCurrencyConversion.SourceCurrencyCode = itemAdv.CurrencyConversion.SourceCurrencyCode;
                                Session["GDSCurrencyConversion"] = objCurrencyConversion;
                            }
                            if (Convert.ToString(Session["GDSCountry"]) == "")
                            {
                                Session["GDSCurrencyConversion"] = null;
                            }

                            if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                            {
                                foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                                {

                                    InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                    string HotelId = item.BasicPropertyInfo.HotelCode;

                                    List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(rec.PropertyId);

                                    long AccomodationId = 0;
                                    string RoomStayRPH = item.RoomStayRPH;
                                    List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                    if (!string.IsNullOrEmpty(RoomStayRPH))
                                    {

                                        string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                        RoomStaysResultList = StayBazar.Controllers.PropertyController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                                    }
                                    CultureInfo culture = CultureInfo.CurrentCulture;

                                    CLayer.GDSRates Rdata = new CLayer.GDSRates();
                                    string BookingCode = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                                    Rdata.Rate = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().AmountAfterTax;
                                    Rdata.RateFor = data.ManageFor;
                                    long userId = 0;
                                    long.TryParse(User.Identity.GetUserId(), out userId);
                                    Rdata.UpdatedBy = userId;
                                    Rdata.StartDate = DateTime.Parse(start, CultureInfo.CurrentCulture);
                                    Rdata.EndDate = DateTime.Parse(End, CultureInfo.CurrentCulture);
                                    Rdata.BookingCode = BookingCode;
                                    Rdata.Status = 1;
                                    Rdata.AccommodationId = objAcc.Where(X => X.BookingCode == BookingCode).FirstOrDefault().AccommodationId;
                                    long gdsRateId = BLayer.Rate.GDSRateSave(Rdata);

                                }

                            }

                        }
                        #endregion

                        DateTime.TryParse(data.StartDate, out tda);
                        rec.StartDate = tda;
                        tda = DateTime.Today;
                        DateTime.TryParse(data.EndDate, out tda);
                        rec.EndDate = tda;
                        rec.Order = data.Order;
                        rec.Status = data.Status;
                        rec.ManageFor = data.ManageFor;
                        long userid = 0;
                        long.TryParse(User.Identity.GetUserId(), out userid);
                        rec.UpdatedBy = userid;

                    }
                    else
                    {

                        rec.PropertyId = data.PropertyId;
                        DateTime.TryParse(data.StartDate, out tda);
                        rec.StartDate = tda;
                        tda = DateTime.Today;
                        DateTime.TryParse(data.EndDate, out tda);
                        rec.EndDate = tda;
                        rec.Order = data.Order;
                        rec.Status = data.Status;
                        rec.ManageFor = data.ManageFor;
                        long userid = 0;
                        long.TryParse(User.Identity.GetUserId(), out userid);
                        rec.UpdatedBy = userid;

                    }
                    //         BLayer.Recommended.Save(rec);
                    rec.StartDate = DateTime.Parse(start, CultureInfo.CurrentCulture);
                    rec.EndDate = DateTime.Parse(End, CultureInfo.CurrentCulture);

                    BLayer.Recommended.SaveWithGDS(rec);



                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                RedirectToAction("Index", "ErrorPage");
            }
            ////   RedirectToAction("Index", "Recommended", new { area = "Admin" });
            return RedirectToAction("Index", "Recommended", new { area = "Admin" });
        }

        [HttpPost]
        public ActionResult GetDetails(long id,long ManageFor)
        {
            Models.RecommendedModel val = new Models.RecommendedModel();
            try {
                CLayer.Recommended data = BLayer.Recommended.GetData(id,ManageFor);
                if (data == null) RedirectToAction("Index");
                val.PropertyId = id;
                val.StartDate = data.StartDate.ToShortDateString(); ;
                val.EndDate = data.EndDate.ToShortDateString();
                val.Order = data.Order;
                val.Status = data.Status;
                val.Title = data.Title;
                val.ManageFor = data.ManageFor;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                RedirectToAction("Index", "ErrorPage");
            }
            return View("_Details", val);
        }
        [HttpPost]
        public ActionResult Delete(long id,long ManageFor)
        {
            Models.RecommendedModel val = new Models.RecommendedModel();
            try
            {
                BLayer.Recommended.Remove(id,ManageFor);
                val = GetAllRecommended(ManageFor);              
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                RedirectToAction("Index", "ErrorPage");
            }
            return View("_List",val.Items);
        }
        public ActionResult GetMostPrts(long ManageFor)
        {
            Models.RecommendedModel val = new Models.RecommendedModel();
            val.Items = BLayer.Recommended.GetAllByManage(ManageFor);         
            return View("_List", val.Items);
        }
        public static string HotelMultiSingleAvailability(string hotelcode, string start, string end)
        {
            string soapResult = string.Empty;
            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/Hotel_MultiSingleAvailability_10.0";


                string SoapHeader = StayBazar.Common.APIUtility.SetSoapHeaderPriceingdetails(_url, _action);
                string SoapBody = StayBazar.Common.APIUtility.SetHotelMultiSingleAvailabilityBodyPriceingdetails(hotelcode, start, end);

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
                    soapResult = text;

                }
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
            //    webRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#if !DEBUG
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif

            //ServicePointManager.Expect100Continue = false;
            //ServicePointManager.DefaultConnectionLimit = 200;
            //ServicePointManager.MaxServicePointIdleTime = 2000;
            //ServicePointManager.SetTcpKeepAlive(false, 0, 0);

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