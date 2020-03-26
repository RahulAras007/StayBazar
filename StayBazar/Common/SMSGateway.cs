using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Threading.Tasks;

namespace StayBazar.Common
{
    public class SMSGateway
    {
        public const int SMS_BLANKS_LENGTH = 30;
        private const int SMS_BLANKS_LENWITHSPACE = 28;
        private const int SMS_CONTACTNO_LENGTH = 11;

        public static string FixPhoneNumber(string phone)
        {
            if (phone == null || phone == "") return "";
            phone = phone.Trim();
            phone = phone.Replace("-", "");
            phone = phone.Replace("_", "");
            phone = phone.Replace(".", "");
            phone = phone.Replace(",", "");
            phone = phone.Replace(" ", "");
            phone = phone.Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace("–","");
            if (phone.Length > SMS_CONTACTNO_LENGTH)
            {
                phone = phone.Substring(phone.Length - SMS_CONTACTNO_LENGTH);
            }
            return phone;
        }
        

        public async static Task<bool> Send(string message,string phoneNumber)
        {
            string smsurl = BLayer.Settings.GetValue(CLayer.Settings.SMS_URL);
            
            phoneNumber = SMSGateway.FixPhoneNumber(phoneNumber);
           /* string myParameters = "workingkey=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_API) + "&sender=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID) +
                "&to=" + phoneNumber + "&message=" + HttpContext.Current.Server.UrlEncode(message);

            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string HtmlResult = wc.UploadString(url, myParameters);
            }
            */
            try
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

                //string myParameters = "?workingkey=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_API) + "&sender=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID) +
                //    "&to=" + phoneNumber + "&message=" + HttpContext.Current.Server.UrlEncode(message);
                //string result = await client.GetStringAsync(url + myParameters);

                //if (!result.Contains("ID"))
                //{ Common.LogHandler.LogError(new Exception(result)); }

               


                string myParameters = "?api_key=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_API) + "&method=sms&message=" + HttpContext.Current.Server.UrlEncode(message) + "&to=" + phoneNumber + "&sender=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID);




                ////string myParameters = "?username=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_USERNAME) + "&pass=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_PASSWORD) + "&senderid=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID) +
                ////   "&dest_mobileno=" + phoneNumber + "&message=" + HttpContext.Current.Server.UrlEncode(message);
             //   string result = await client.GetStringAsync(smsurl + myParameters);
#if !DEBUG
                string result = await client.GetStringAsync(smsurl + myParameters );
#endif
            }
            catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            
            return true;
        }
        public static bool SendSMSSync(string message, string phoneNumber)
        {
            string smsurl = BLayer.Settings.GetValue(CLayer.Settings.SMS_URL);

            phoneNumber = SMSGateway.FixPhoneNumber(phoneNumber);
            /* string myParameters = "workingkey=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_API) + "&sender=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID) +
                 "&to=" + phoneNumber + "&message=" + HttpContext.Current.Server.UrlEncode(message);

             using (WebClient wc = new WebClient())
             {
                 wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                 string HtmlResult = wc.UploadString(url, myParameters);
             }
             */
            try
            {
                // System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

                //string myParameters = "?workingkey=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_API) + "&sender=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID) +
                //    "&to=" + phoneNumber + "&message=" + HttpContext.Current.Server.UrlEncode(message);
                //string result = client.GetStringAsync(smsurl + myParameters).Result ;

                //if (!result.Contains("ID"))
                //{ Common.LogHandler.LogError(new Exception(result)); }

                Common.LogHandler.AddLog("sms sending started");

                 string myParameters = "?api_key=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_API) + "&method=sms&message=" + HttpUtility.UrlEncode(message) + "&to=" + phoneNumber + "&sender=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID);

                Common.LogHandler.AddLog(myParameters);

                //     var url = "http://alerts.kapsystem.com/api/v4/?api_key=or7c27he3o550lg377t0d1v431123q&method=sms&message=Test&to=" + id + "sender=BRAND";


                ////string myParameters = "?username=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_USERNAME) + "&pass=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_PASSWORD) + "&senderid=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID) +
                ////   "&dest_mobileno=" + phoneNumber + "&message=" + HttpContext.Current.Server.UrlEncode(message);
                //   string result = await client.GetStringAsync(smsurl + myParameters);
#if !DEBUG
                System.Net.WebClient wc = new System.Net.WebClient();

                string result = wc.DownloadString(smsurl + myParameters);
        //        string result = await client.GetStringAsync(smsurl + myParameters);
#endif
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            return true;
        }

        public async static Task<string> SendTest(string message, string phoneNumber)
        {
            string url = "http://123.63.33.43/blank/sms/user/urlsmstemp.php";
            phoneNumber = SMSGateway.FixPhoneNumber(phoneNumber);
            string result = "something happend";
            try
            {
                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();



                string myParameters = "?username=staybazar&pass=Kap@2018u!&senderid=STYBZR&dest_mobileno=" + phoneNumber + "&message=" + HttpContext.Current.Server.UrlEncode(message);
                result = await client.GetStringAsync(url + myParameters);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            return result;
        }

        public static string GetNewBookingMessage(string Name,string orderNo, string checkIn, string checkOut, string propertyTitle, string address,string contactNo)
        {
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Dear ");
            text.Append(Utils.CutString(Name,15));
            text.Append(" Staybazar Itinerary#");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle,18) + ", " + Utils.CutString(address,10);
            text.Append(s);
            text.Append(" Chk-in ");
            text.Append(Utils.CutString(checkIn,11));
            text.Append(" Chk-out ");
            text.Append(Utils.CutString(checkOut,11));
            text.Append(". T & C apply Staybazar Contact No ");
            text.Append(Utils.CutString(contactNo,11));
            return text.ToString();
        }
        //todo added by Keji
         public static string GetNewBookingMessage(string Name,string orderNo, string checkIn, string checkOut, string propertyTitle, string propertycity, string accomodationtype, string contactNo)
        {
           //if(contactNo!= null && contactNo != "") contactNo = contactNo.Trim().Replace(" ", "").Replace("-", "");
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Dear ");
            text.Append(Utils.CutString(Name, 15));
            text.Append(" Staybazar Itinerary#");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle, 16) + ", " + Utils.CutString(propertycity, 10);
            text.Append(s);
            text.Append(" Chk-in ");
            text.Append(Utils.CutString(checkIn, 11));
            text.Append(" Chk-out ");
            text.Append(Utils.CutString(checkOut, 11));
            text.Append(" ");
            text.Append(Utils.CutString(accomodationtype, 10));
            text.Append(". T & C apply Staybazar Contact No ");
            text.Append(Utils.CutString(contactNo, 11));
            return text.ToString();

            //StringBuilder text = new StringBuilder();
            //string s;
            //text.Append("Dear ");
            //text.Append(Utils.CutString(Name,SMS_BLANKS_LENWITHSPACE));
            //text.Append(" Staybazar Itinerary#");
            //text.Append(Utils.CutString(orderNo, SMS_BLANKS_LENWITHSPACE));
            //text.Append(" at ");
            //int i = SMS_BLANKS_LENWITHSPACE;
            //if (propertycity.Length > 12) propertycity = Common.Utils.CutString(propertycity, 12);
            //i = i - propertycity.Length - 2;// 2 for , and space
            //propertyTitle = Common.Utils.CutString(propertyTitle, i);
            //s = propertyTitle + ", " + propertycity;
            //s = Utils.CutString(s, SMS_BLANKS_LENWITHSPACE);
            //text.Append(s);         
            //text.Append(" Chk-in ");
            //text.Append(checkIn);
            //text.Append(" Chk-out ");
            //s =Utils.CutString( checkOut + " " + accomodationtype,SMS_BLANKS_LENWITHSPACE) ;

            //text.Append(checkOut);
            //text.Append(". T & C apply Staybazar Contact No ");
            //text.Append(Utils.CutString(contactNo,SMS_BLANKS_LENGTH-1));
            //return text.ToString();
        }
        public static string GetBookingApprovalMessage(string Name, string orderNo, string checkIn, string checkOut, string propertyTitle, string propertycity, string accomodationtype, string contactNo)
        {
            //if(contactNo!= null && contactNo != "") contactNo = contactNo.Trim().Replace(" ", "").Replace("-", "");
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Dear ");
            text.Append(Utils.CutString(Name, 15));
            text.Append(" Staybazar Booking approval Itinerary#");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle, 16) + ", " + Utils.CutString(propertycity, 10);
            text.Append(s);
            text.Append(" Chk-in ");
            text.Append(Utils.CutString(checkIn, 11));
            text.Append(" Chk-out ");
            text.Append(Utils.CutString(checkOut, 11));
            text.Append(" ");
            text.Append(Utils.CutString(accomodationtype, 10));
            text.Append(". T & C apply Staybazar Contact No ");
            text.Append(Utils.CutString(contactNo, 11));
            return text.ToString();

     
        }
        public static string GetBookingRejectionMessage(string Name, string orderNo, string checkIn, string checkOut, string propertyTitle, string propertycity, string accomodationtype, string contactNo,string RejectionNote)
        {
            //if(contactNo!= null && contactNo != "") contactNo = contactNo.Trim().Replace(" ", "").Replace("-", "");
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Dear ");
            text.Append(Utils.CutString(Name, 15));
            text.Append(" Staybazar Booking rejection Itinerary#");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle, 16) + ", " + Utils.CutString(propertycity, 10);
            text.Append(s);
            text.Append(" Chk-in ");
            text.Append(Utils.CutString(checkIn, 11));
            text.Append(" Chk-out ");
            text.Append(Utils.CutString(checkOut, 11));
            text.Append(" ");
            text.Append(Utils.CutString(accomodationtype, 10));
            text.Append(" ");
            text.Append(" Reason for rejection ");
            text.Append(RejectionNote);
            text.Append(" ");
            text.Append(". T & C apply Staybazar Contact No ");
            text.Append(Utils.CutString(contactNo, 11));
            return text.ToString();


        }
        public static string GetCancellationMsg(string Name, string orderNo, string propertyTitle, string location, string contactNo)
        {
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Dear ");
            text.Append(Utils.CutString(Name, 16));
            text.Append(" Staybazar Itinerary Cancellation# ");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle,16) + ", " + Utils.CutString(location,10);
            text.Append(s);
            text.Append(". T & C apply Staybazar Contact No ");
            text.Append(Utils.CutString(contactNo, 11));
            return text.ToString();
        }

        public static string GetModifyMessage(string Name, string orderNo, string checkIn, string checkOut, string propertyTitle, string address, string contactNo)
        {
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Dear ");
            text.Append(Utils.CutString(Name, 16));
            text.Append(" Staybazar Itinerary Change#");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle, 16) + ", " + Utils.CutString(address, 10);
            text.Append(s);
            text.Append(" New booking details Chk-in ");
            text.Append(Utils.CutString(checkIn, 11));
            text.Append(" Chk-out ");
            text.Append(Utils.CutString(checkOut, 11));
            text.Append(". T & C apply Staybazar Contact No ");
            text.Append(Utils.CutString(contactNo, 11));
            return text.ToString();
        }

        public static string GetSupplierBookingMessage(string Name, string orderNo, string checkIn, string checkOut, string propertyTitle, string propertycity, string accomodationtype, string contactNo)
        {
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Booking Confirmation:  Staybazar Itinerary# ");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle, 18) + ", " + Utils.CutString(propertycity, 10);
            text.Append(s);
            text.Append(" Guest ");
            text.Append(Utils.CutString(Name, 15));
            text.Append(" Chk-in ");
            text.Append(Utils.CutString(checkIn, 11));
            text.Append(" Chk-out ");
            text.Append(Utils.CutString(checkOut, 11));
            text.Append(" ");
            text.Append(Utils.CutString(accomodationtype, 10));
            text.Append(". Staybazar Support Contact ");
            text.Append(Utils.CutString(contactNo, 11));
            return text.ToString();
        }
        public static string GetSupplierCancellationMsg(string Name, string orderNo, string checkIn, string checkOut, string propertyTitle, string propertycity, string accomodationtype, string contactNo)
        {
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Booking Cancellation:  Staybazar Itinerary# ");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle, 18) + ", " + Utils.CutString(propertycity, 10);
            text.Append(s);
            text.Append(" Guest ");
            text.Append(Utils.CutString(Name, 15));
            text.Append(" Chk-in ");
            text.Append(Utils.CutString(checkIn, 11));
            text.Append(" Chk-out ");
            text.Append(Utils.CutString(checkOut, 11));
            text.Append(" ");
            text.Append(Utils.CutString(accomodationtype, 10));
            text.Append(". Staybazar Support Contact ");
            text.Append(Utils.CutString(contactNo, 11));
            return text.ToString();
        }

        public static string GetSupplierModifyMessage(string Name, string orderNo, string checkIn, string checkOut, string propertyTitle, string propertycity, string accomodationtype, string contactNo)
        {
            contactNo = SMSGateway.FixPhoneNumber(contactNo);
            StringBuilder text = new StringBuilder();
            string s;
            text.Append("Booking Modification:  Staybazar Itinerary# ");
            text.Append(Utils.CutString(orderNo, 7));
            text.Append(" at ");
            s = Utils.CutString(propertyTitle, 18) + ", " + Utils.CutString(propertycity, 10);
            text.Append(s);
            text.Append(" Guest ");
            text.Append(Utils.CutString(Name, 15));
            text.Append(" Chk-in ");
            text.Append(Utils.CutString(checkIn, 11));
            text.Append(" Chk-out ");
            text.Append(Utils.CutString(checkOut, 11));
            text.Append(" ");
            text.Append(Utils.CutString(accomodationtype, 10));
            text.Append(". Staybazar Support Contact ");
            text.Append(Utils.CutString(contactNo, 11));
            return text.ToString();
        }

    }
}