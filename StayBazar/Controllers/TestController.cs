using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Xml;
using System.Net;

namespace StayBazar.Controllers
{
 //[Common.AdminRolePermission(AllowAllRoles=true)]
    public class TestController : Controller
    {
        //
        // GET: /Test/
        //public async  Task<ActionResult> Index()
        //{
        //   await TestMail();
        //    return View();
        //}

        public string FixCSV(string data)
        {
            if (data == null || data == "") return "";
            data = data.Replace("  ", " ");
            data = data.Replace(", ,", ",");
            string[] items = data.Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries);
            int i, len;
            len = items.Length;
            for (i = 0; i < len;i++)
            {
                if(items[i] != null && items[i] != "") items[i] = items[i].Trim();
            }
            string result = string.Join(",", items);
             i = result.LastIndexOf(",");
            if(i>0)
            {
                if(i == (result.Length-1))
                {
                    result = result.Substring(0,i);
                }
            }
         
            return result; 
        }
       
        public  async Task<string> TestSMS_New(string  id)
        {
            string s="";
            try
            {

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            

                var url = "http://alerts.kapsystem.com/api/v4/?api_key=or7c27he3o550lg377t0d1v431123q&method=sms&message=Test&to=" + id + "&sender=STYBZR";
                 s = await client.GetStringAsync(url);
            }
            catch(Exception ex)
            {
                Common.LogHandler.AddLog(ex.Message);
            }

            

            return s;
        }
        [AllowAnonymous]
        public string TestSMSSync(string id)
        {
            string s = "";
            try
            {
                //    string myParameters = "?api_key=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_API) + "&method=sms&message=" + Server.UrlEncode("testing the message with some params Rs.00") + "&to=" + id + "&sender=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID);
                //    string smsurl = BLayer.Settings.GetValue(CLayer.Settings.SMS_URL);
                //    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();


                ////    var url = "http://alerts.kapsystem.com/api/v4/?api_key=or7c27he3o550lg377t0d1v431123q&method=sms&message=Test&to=" + id + "&sender=STYBZR";
                //    System.Net.WebClient wc = new System.Net.WebClient();

                //   s= wc.DownloadString(smsurl + myParameters);

                return Common.SMSGateway.SendSMSSync("testint message with RS.12.00 Mr. Ashok  Mr.Ravi ", id).ToString();
               
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        private async Task<bool> SendMailsAndSms(long bookingId)
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
                    string phone = forUser[0].Mobile;
                    if (phone == "") phone = forUser[0].Phone;
                    if (phone != "")
                    {
                        string smsmsg =  Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                        details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                       bool b =false;
                       b= await Common.SMSGateway.Send(smsmsg, phone);
                    }
                }catch(Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }

            }
            catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }   
        
         public async Task<string> TesttSMS(string  id)
        {
            //await SendSms(id);
           var returneddata= await TestSMS_New(id);
           return returneddata;
        }
     
        public async Task<string> TestSms()
        {
        string b ="";
        b = await Common.SMSGateway.SendTest("Booking Confirmation: Staybazar Itinerary# RB04E23 at Nagarjuna Suites , Bangalore Guest Prem Kumar Chk-in Sep 08,2015 Chk-out Sep 12,2015 1 BEDROOM APT . Staybazar Support Contact 08025702898", "9995815307");
        return b;
           
            }
       // public async Task<ActionResult> Index()
      public async Task<ActionResult> Index(string id)
        {
         //   var result =await TestSMS_New(id);

            //XmlDocument xdoc = new XmlDocument();
            //xdoc.LoadXml("<?xml version='1.0' encoding='UTF-8'?><output response=\"SUCCESS\" transactionId=\"2690686\" paymentId=\"1399586\" amount=\"200\" dateTime=\"2010-10-06 17:39:38\" mode=\"TEST\" refrenceNo=\"223\" transactionType=\"refunded\" status=\"Processing\"/> ");
            //  xdoc.
            //     long b = 333;
            //    string phone = "1234567890";
            //    FixPhoneNumber(phone);
            //      await SendMailsAndSms(b);
            //     FixCSV("sdf,223");
#if DEBUG
            /*     string responsebody = "<output errorCode=\"10\" error=\"Invalid PaymentID\"/>";
                 XmlDocument xdoc = new XmlDocument();
                 xdoc.LoadXml(responsebody);
                 if (xdoc.DocumentElement.Attributes["transactionType"] != null && xdoc.DocumentElement.Attributes["transactionType"].Value.ToLower() == "refunded")
                 {
                     CLayer.Transaction retran = new CLayer.Transaction();
                     retran.TransactionType = CLayer.ObjectStatus.TransactionType.Refund;
                     retran.TransactionId = xdoc.DocumentElement.Attributes["transactionId"].Value;
                     retran.PaymentId = xdoc.DocumentElement.Attributes["paymentId"].Value;
                     double amount = 0;
                     double.TryParse(xdoc.DocumentElement.Attributes["amount"].Value, out amount);
                     retran.Amount = amount;
                     DateTime dat = DateTime.Today;
                     DateTime.TryParse(xdoc.DocumentElement.Attributes["dateTime"].Value, out dat);
                     retran.DateCreated = dat;
                     retran.Status = CLayer.ObjectStatus.TransactionStatus.Refunded;
                     retran.Notes = "Reference Number: " + xdoc.DocumentElement.Attributes["referenceNo"].Value + " mode=" + xdoc.DocumentElement.Attributes["mode"].Value + " transactionType=" + xdoc.DocumentElement.Attributes["transactionType"].Value
                         + " status=" + xdoc.DocumentElement.Attributes["status"].Value;
                     retran.BookingId = 33;
                     try
                     {
                         BLayer.Transaction.Save(retran);
                         throw new Exception("dsfsdf");
                     }
                     catch
                     {
                         string n = "Could not create transaction record. Received Response from Gateway: " + responsebody + " " ;
                         BLayer.Transaction.SetNote("121212", "sdfdsf", n);
                     }
                 }*/

#endif
            return View(new Models.TestModel());
        }
        [HttpGet]
        public ActionResult Source(string term)
        {
           
            
    var movies = new List<object>();

    movies.Add(new { value = "Ghostbusters", label = "Comedy", desc = "1984", icon = "sizzlejs_32x32.png" });
    movies.Add(new { value = "Gone with Wind", label = "Drama", desc = "1939", icon = "sizzlejs_32x32.png"  });
    movies.Add(new { value = "Star Wars", label = "Science Fiction", desc = "1977", icon = "sizzlejs_32x32.png"  });

    return Json(BLayer.Utility.GetAutoList(term), JsonRequestBehavior.AllowGet);
  /*          
        {
            value: "jquery-ui",
            label: "jQuery UI",
            desc: "the official user interface library for jQuery",
            icon: "jqueryui_32x32.png"
        },
        {
            value: "sizzlejs",
            label: "Sizzle JS",
            desc: "a pure-JavaScript CSS selector engine",
            icon: 
        }
        ]*/

        }
        private  string FixPhoneNumber(string phone)
        {
            if (phone == null || phone == "") return "";
            phone = phone.Trim();
            phone = phone.Replace("-", "");
            phone = phone.Replace("_", "");
            phone = phone.Replace(".", "");
            phone = phone.Replace(",", "");
            phone = phone.Replace(" ", "");
            phone = phone.Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "").Replace("–", "");
            if(phone.Length>11)
            {
                phone = phone.Substring(phone.Length - 11);
            }
            return phone;
        }
       
        public async Task<ActionResult> SMSTest(Models.TestModel data)
        {
            Models.TestModel result = new Models.TestModel();
            try
            {
                if (data != null && data.phone != null && data.phone != "" && data.message!= null && data.message != "")
                {
                    string url = BLayer.Settings.GetValue(CLayer.Settings.SMS_URL);

                    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                   // string myParameters = "?workingkey=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_API) + "&sender=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID) +
                        //"&to=" + data.phone + "&message=" + Server.UrlEncode(data.message);

                    string myParameters = "?username=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_USERNAME) + "&pass=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_PASSWORD) + "&senderid=" + BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID) +
                 "&dest_mobileno=" + data.phone + "&message=" + Server.UrlEncode(data.message);
                    string s = await client.GetStringAsync(url + myParameters);

                    //System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                    //reqparm.Add("workingkey", BLayer.Settings.GetValue(CLayer.Settings.SMS_API));
                    //reqparm.Add("sender", BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID));
                    //reqparm.Add("to", data.phone);
                    //reqparm.Add("message", Server.UrlEncode(data.message));
                    //WebClient client = new WebClient();
                    //byte[] responsebytes = client.UploadValues(url, "GET", reqparm);

                    //string responsebody = Encoding.UTF8.GetString(responsebytes);
                    result.emessage = "Result:" + s ;
                }
                else
                    data = new Models.TestModel();
            }catch(Exception ex)
            {
                result.emessage = ex.Message;
            }
            return View("SMSTest", result);
        }

        public async Task<bool> TestMail()
        {
            //long bookingId = 4;
            //CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
            //List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
            //if (byUser == null) return false;
            //if (forUser.Count == 0) return false;
            //CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.ForBookingUserId);
            //CLayer.Booking details = BLayer.Bookings.GetDetails(bookingId);

            try
            {
                string message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("BConfirmationMail")  + "11&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                Common.Mailer ml = new Common.Mailer();
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                // BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID), "anoop@isletsystems.com", "Booking Confirmation", message);
                msg.To.Add(new System.Net.Mail.MailAddress("anoop@isletsystems.com", "Anoop T P"));
                msg.From = new System.Net.Mail.MailAddress(BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID),"Staybazar Reservation");
                msg.Subject = "Testing Reservation";
                msg.Body = message;
                msg.IsBodyHtml = true;
                await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
            }
            catch (Exception ex) 
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }

        private async Task<bool> SendSms(long bookingId)
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
                    string phone = forUser[0].Mobile;
                    if (phone == "") phone = forUser[0].Phone;
                    string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                        details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    bool b = false;
                    phone = Common.Utils.GetMobileNo(phone);
                    if (phone != "")
                    { b = await Common.SMSGateway.Send(smsmsg, phone); }
                    phone = Common.Utils.GetMobileNo(details.Mobile);
                    if (phone != "")
                    { b = await Common.SMSGateway.Send(smsmsg, phone); }
                    phone = Common.Utils.GetMobileNo(supplier.Mobile);
                    if (phone != "")
                    { b = await Common.SMSGateway.Send(smsmsg, phone); }
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

       
	}
}