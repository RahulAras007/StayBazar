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
    class CancelGDSBookings
    {
       
        

        public bool CancelInCompleteGDSBookings()
        {
            bool result = false;
            try
            {
                String hotelcode = string.Empty;
                List<CLayer.BookingItem> objInCompleteBookings = BLayer.BookingItem.GetIncompleteGDSBookings();
                foreach(var item in objInCompleteBookings)
                {
                    #region Cancel incomplete GDS bookings

                    bool bResult = BookingDecline(item.BookingId, item.HotelConfirmNumber);
                  //  BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, item.BookingId);

                    #endregion
                }
                result = true;
                #region Transaction Log 
                if(objInCompleteBookings.Count>0)
                {
                    APIUtility.GenerateGDSTransactionLog("", "BulkCancelIncompleteBookingsSuccess", 0, (int)CLayer.ObjectStatus.GDSType.BulkCancelIncompleteBookings, 0);
                }

                #endregion Transaction log end

            }
            catch (Exception ex)
            {
                result = false;
                #region Transaction Log 

                APIUtility.GenerateGDSTransactionLog("", "BulkCancelIncompleteBookingsFailure", 0, (int)CLayer.ObjectStatus.GDSType.BulkCancelIncompleteBookings, 0);

                #endregion Transaction log end

                throw ex;
            }
            return result;
        }
        public bool BookingDecline(long BookingId, string ControlNumber = "")
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, BookingId);

                #region GDS Booking Cancel      
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(BookingId));
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    ControlNumber = BLayer.Bookings.GetGDSBookingControlNumber(BookingId);

                    int OptionCode = (ControlNumber == "") ? 0 : (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize;
                    string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, OptionCode,true);
                }
                #endregion 

            }
            catch (Exception ex)
            {
                LogHandler.LogError(ex);
                return false;
            }
            return true;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
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
