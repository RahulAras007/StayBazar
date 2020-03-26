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


namespace ExcelReport
{
    class PropertyDetails
    {
        public const int PictureCount = 8;
        

        public bool UpdatePropertyDescriptionandImages()
        {
            bool result = false;
            long id1 = 0;
            string HotelID = string.Empty;
            try
            {
                String hotelcode = string.Empty;

                List<CLayer.Property> objProperties = BLayer.Property.GetAllGDSPropertiesWithOutData().ToList();
                List<CLayer.Property> objDescriptions = BLayer.Property.GetAllGDSPropertyDescriptionsWithOutData();
              //  List<CLayer.Property> objFormattedDescriptions = BLayer.Property.GetAllGDSPropertyFormattedDescriptionsWithOutData();
                List<CLayer.Property> objTitles = BLayer.Property.GetAllGDSPropertyTitlesWithOutData();
                CLayer.GDSBookingDetails.Envelope ss = new CLayer.GDSBookingDetails.Envelope();
                CLayer.GDSBookingDetailsAdv.Envelope ssAdv = new CLayer.GDSBookingDetailsAdv.Envelope();// ser.Deserialize<CLayer.GDSBookingDetailsAdv.Envelope>(hotel);
                long id = 0;
                foreach (var pitem in objProperties)
                {

                    try
                    {
                        #region Update Property Description
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



                        if (string.IsNullOrEmpty(FormattedDescription))
                        {
                            FormattedDescription = GDSProcess.GDSFormatDescription.GetFormattedDescription(hotel);
                            BLayer.Property.GDSUpdatePropertyDescriptionFormatted(id, FormattedDescription, GDSProcess.GDSFormatDescription.StarRatings, "");
                        }
                        int LocalStarRating = APIUtility.GetStarRating(hotel);
                        if (LocalStarRating > 0)
                        {
                            BLayer.Property.GDSUpdatePropertyStarRatings(id, LocalStarRating);
                        }



                        if (ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents != null)
                        {

                            var TitleItem = objTitles.Where(x => x.HotelID == HotelCode).ToList();
                            if (TitleItem != null)
                            {
                                if (TitleItem.Count > 0)
                                {
                                    string Title = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelName;
                                    BLayer.Property.GDSUpdatePropertyTitle(id, Title);
                                }

                            }

                            #region Update Description Without Formatting

                            if (string.IsNullOrEmpty(FormattedDescription))
                            {
                                var DescriptionItem = objDescriptions.Where(x => x.HotelID == HotelCode).ToList();
                                if (DescriptionItem != null)
                                {
                                    if (DescriptionItem.Count > 0)
                                    {

                                        if (ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo != null)
                                        {
                                            var description = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;
                                            if (description != null)
                                            {
                                                var descriptionList = description.MultimediaDescriptions.MultimediaDescription;


                                                var Descriptions = from order in descriptionList
                                                                   where order.TextItems != null
                                                                   select order;
                                                string HotelDescription = string.Empty;
                                                foreach (var desc in Descriptions)
                                                {
                                                    if (desc.InfoCode != null)
                                                    {
                                                        foreach (var datas in desc.TextItems.TextItem.Description)
                                                        {


                                                            HotelDescription = HotelDescription + datas.__Text + "#256#";

                                                        }
                                                    }
                                                    // HotelDescription = HotelDescription + "<br>";
                                                }
                                                Description.Append(HotelDescription);

                                                BLayer.Property.GDSUpdatePropertyDescription(id, Description.ToString());
                                            }
                                        }
                                    }

                                }

                            }

                            #endregion

                            //update property description end
                            #endregion

                            #region Update Property Images old method
                            //  var ssw = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;
                            ////  var sswguest = ssAdv.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.CategoryCodes.GuestRoomInfo.Where(x => x.Code == 28);

                            //  if (ssw!=null)
                            //  {
                            //      var sswList = ssw;

                            //      List<CLayer.PropertyFiles> picturelist = new List<CLayer.PropertyFiles>();
                            //      //var filteredOrders = from order in sswList
                            //      //                     where order.ImageItems != null
                            //      //                     select order;

                            //      int nos = 0;
                            //    BLayer.Property.DeleteGDSPropertyImages(id);

                            //      foreach (var item in sswList.MultimediaDescriptions.MultimediaDescription)
                            //      {
                            //          if (item.ImageItems != null)
                            //          {
                            //              var t = item.ImageItems.ImageItem;
                            //              if (t != null)
                            //              {
                            //                  foreach (var itemimg in t)
                            //                  {
                            //                      CLayer.PropertyFiles picture = new CLayer.PropertyFiles();
                            //                      picture.FileName = itemimg.ImageFormat.Where(x => x.DimensionCategory == "E").ToList()[0].URL;
                            //                      picture.PropertyId = id;
                            //                      picturelist.Add(picture);
                            //                      BLayer.Property.GDSSaveImageurl(id, picture.FileName);
                            //                      nos++;
                            //                      if (nos == PictureCount) break;
                            //                  }
                            //              }
                            //          }
                            //          if (nos == PictureCount) break;
                            //      }
                            //      if(picturelist.Count<8)
                            //      {
                            //          ssAdv = ser.Deserialize<CLayer.GDSBookingDetailsAdv.Envelope>(hotel);
                            //          var sswguest = ssAdv.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.CategoryCodes.GuestRoomInfo.Where(x => x.Code == 28);
                            //          if (sswguest != null)
                            //          {
                            //              int PictureCount = 8 - picturelist.Count;
                            //              int ImgCounter = 0;
                            //              foreach (var item in sswguest)
                            //              {
                            //                  foreach (var itemAdv in item.MultimediaDescriptions)
                            //                  {
                            //                      if (itemAdv.ImageItems != null)
                            //                      {
                            //                          var t = itemAdv.ImageItems;
                            //                          if (t != null)
                            //                          {
                            //                              foreach (var itemimg in t)
                            //                              {
                            //                                  CLayer.PropertyFiles picture = new CLayer.PropertyFiles();
                            //                                  picture.FileName = itemimg.ImageFormat.Where(x => x.DimensionCategory == "E").ToList()[0].URL;
                            //                                  picture.PropertyId = id;
                            //                                  picturelist.Add(picture);
                            //                                  BLayer.Property.GDSSaveImageurl(id, picture.FileName);
                            //                                  ImgCounter++;
                            //                                  if (ImgCounter == PictureCount) break;
                            //                              }
                            //                          }
                            //                      }
                            //                      if (ImgCounter == PictureCount) break;
                            //                  }
                            //                  if (ImgCounter == PictureCount) break;
                            //              }
                            //          }
                            //      }


                            //  }
                            #endregion




                            #region update property contact numbers
                            //update GDS Property contact numbers start
                            CLayer.GDSBookingDetails.Envelope GDSBookingDetails = ss;
                            string Phone = string.Empty;
                            string Mobile = string.Empty;
                            string Email = string.Empty;
                            if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails != null)
                            {
                                Email = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails.Email.ToString();
                            }
                            if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones != null)
                            {
                                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").Count() > 0)
                                {
                                    Phone = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").FirstOrDefault().PhoneNumber;

                                }
                                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").Count() > 0)
                                {
                                    Mobile = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").FirstOrDefault().PhoneNumber;
                                }
                                if ((!string.IsNullOrEmpty(Phone)) || (!string.IsNullOrEmpty(Mobile)) || (!string.IsNullOrEmpty(Email)))
                                {
                                    BLayer.Property.GDSUpdatePropertyContactNumbers(id, Phone, Mobile, Email);
                                }
                            }
                            //update GDS Property Contact numbers end
                            //#region Transaction Log 

                            //APIUtility.GenerateGDSTransactionLog("", "BulkImageandDescriptionSuccess", 0, (int)CLayer.ObjectStatus.GDSType.BulkHotelImageDescriptionUpdation, 0);

                            //#endregion Transaction log end
                            #endregion
                        }


                        #region UPDATE PROPERTY IMAGES
                        long ImageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                        if (ImageCount < 1)
                        {
                            BLayer.Property.DeleteGDSPropertyImages(id);
                            List<CLayer.PropertyFiles> pictlist = GetGDSImages(hotel, id);
                        }


                        #endregion

                    //    WriteToLog(HotelID);
                    }
                    catch(Exception ex)
                    {
                        //  LogHandler.AddLog("Error in-"+hotelcode);
                        WriteToLog(HotelID,true);
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
        public void WriteToLog(string pHotelCode,bool IsError=false)
        {
            //string path = System.Environment.CurrentDirectory+"\\Log.txt";
            string path= Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+"\\log.txt";


            if (IsError)
            {
                File.AppendAllText(path, "ERROR -" + pHotelCode +"-"+ DateTime.Now.ToString() + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(path, "SUCCESS -" + pHotelCode +"-"+ DateTime.Now.ToString() + Environment.NewLine);
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
            catch(Exception ex)
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

        private List<CLayer.PropertyFiles>  GetGDSImages(string response,long id)
        {
            List<CLayer.PropertyFiles> picturelist = new List<CLayer.PropertyFiles>();
            XmlDocument xmlDoc = new XmlDocument();
            string a = string.Empty;
            //  List<CLayer.PropertyFiles> pictureslist = new List<CLayer.PropertyFiles>();
            try
            {
               // xmlDoc.Load(@"F:\HotelDescriptiveIno.xml");
                xmlDoc.LoadXml(response);
                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XmlNode node = null;
                XmlNode nodeImages = null;
                XmlNode nodeguestImages = null;

                string nodeRoot = "/soapenv:Envelope/soapenv:Body/";
                string nodevalue = "/si:OTA_HotelDescriptiveInfoRS/si:HotelDescriptiveContents/si:HotelDescriptiveContent/si:HotelInfo/si:Descriptions";//"/si:MultimediaDescriptions/si:MultimediaDescription/si:ImageItems/si:ImageItem/si:ImageFormat/si:URL";
                string nodevalueguestroom = "/si:OTA_HotelDescriptiveInfoRS/si:HotelDescriptiveContents/si:HotelDescriptiveContent/si:HotelInfo/si:CategoryCodes/si:CategoryCodes/si:GuestRoomInfo";//si:MultimediaDescriptions/si:MultimediaDescription/si:ImageItems/si:ImageItem/si:ImageFormat/si:URL";

                xmlnsManager.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                xmlnsManager.AddNamespace("awsse", "http://xml.amadeus.com/2010/06/Session_v3");
                xmlnsManager.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");
                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");

                nodeImages = xmlDoc.SelectSingleNode(nodeRoot + nodevalue, xmlnsManager);
                nodeguestImages = xmlDoc.SelectSingleNode(nodeRoot + nodevalueguestroom, xmlnsManager);

#region MAIN IMAGES
                if (nodeImages != null)
                {
                    XmlNodeList list = nodeImages.SelectNodes("//si:MultimediaDescriptions/si:MultimediaDescription", xmlnsManager);
                    int PictureCount = 0;
                    int nodes = 0;
                    if (list != null)
                    {
                        foreach (XmlNode item in list)
                        {
                            var subItem = item.SelectNodes("//si:ImageItems", xmlnsManager);
                            if (subItem != null)
                            {
                                foreach (XmlNode sItem in subItem)
                                {
                                    IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDateTime(r.Attributes["LastModifyDateTime"].Value));
                                    // IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDecimal(r.Attributes["LastModifyDateTime"].Value));
                                    if (subInnerItem != null)
                                    {
                                        foreach (XmlNode sItem1 in subInnerItem)
                                        {
                                            var subUrlItem = sItem1.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager);
                                            if (subUrlItem != null)
                                            {
                                                PictureCount = subInnerItem.Count();
                                                foreach (XmlNode UItem in subUrlItem)
                                                {
                                                    var UrlItem = UItem.ChildNodes[0].InnerText;  //url

                                                    bool bImageExists = true;
                                                    if (!string.IsNullOrEmpty(UrlItem))
                                                    {
                                                        bImageExists =APIUtility.DoesImageExistRemotely(UrlItem);
                                                    }

                                                    if (bImageExists)
                                                    {
                                                        CLayer.PropertyFiles picture = new CLayer.PropertyFiles();
                                                        picture.FileName = UrlItem;// itemimg.ImageFormat.Where(x => x.DimensionCategory == "E").ToList()[0].URL;
                                                        picture.PropertyId = id;
                                                        picture.IsValid = true;
                                                        picturelist.Add(picture);
                                                        BLayer.Property.GDSSaveImageurl(id, picture.FileName);
                                                    }
                                                    nodes++;
                                                    if (nodes == PictureCount) break;
                                                }
                                            }
                                            if (nodes == PictureCount) break;
                                        }

                                    }
                                    if (nodes == PictureCount) break;
                                }
                            }
                            if (nodes == PictureCount) break;
                        }
                    }

                }
#endregion


#region GUESTROOMIMAGES
                if (nodeguestImages != null)
                {
                    XmlNodeList listGuestRoomImages = nodeguestImages.SelectNodes("//si:MultimediaDescriptions/si:MultimediaDescription", xmlnsManager);
                    if (listGuestRoomImages != null)
                    {
                        int PictureCount = 0;
                        int nodes = 0;
                        foreach (XmlNode item in listGuestRoomImages)
                        {
                            var subItem = item.SelectNodes("//si:ImageItems", xmlnsManager);
                            if (subItem != null)
                            {
                                foreach (XmlNode sItem in subItem)
                                {
                                    IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDateTime(r.Attributes["LastModifyDateTime"].Value));
                                    // IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDecimal(r.Attributes["LastModifyDateTime"].Value));
                                    if (subInnerItem != null)
                                    {
                                        foreach (XmlNode sItem1 in subInnerItem)
                                        {
                                            var subUrlItem = sItem1.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager);
                                            if (subUrlItem != null)
                                            {
                                                PictureCount = subInnerItem.Count();
                                                foreach (XmlNode UItem in subUrlItem)
                                                {
                                                    var UrlItem = UItem.ChildNodes[0].InnerText;  //url
                                                    bool bImageExists = true;
                                                    if (!string.IsNullOrEmpty(UrlItem))
                                                    {
                                                        bImageExists = APIUtility.DoesImageExistRemotely(UrlItem);
                                                    }

                                                    if (bImageExists)
                                                    {
                                                        CLayer.PropertyFiles picture = new CLayer.PropertyFiles();
                                                        picture.FileName = UrlItem;// itemimg.ImageFormat.Where(x => x.DimensionCategory == "E").ToList()[0].URL;
                                                        picture.PropertyId = id;
                                                        picturelist.Add(picture);
                                                        BLayer.Property.GDSSaveImageurl(id, picture.FileName);
                                                    }
                                                    nodes++;
                                                    if (nodes == PictureCount) break;
                                                }
                                            }
                                            if (nodes == PictureCount) break;
                                        }

                                    }
                                    if (nodes == PictureCount) break;
                                }
                            }
                            if (nodes == PictureCount) break;
                        }
                    }
                }

#endregion
            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
            }
            return picturelist;

        }



    }
}
