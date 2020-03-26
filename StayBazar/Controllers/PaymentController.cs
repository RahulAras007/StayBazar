using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using StayBazar.Common;
using System.Xml;
using System.Web.Mvc.Html;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class PaymentController : Controller
    {
        public const string PAYMENT_FAILED_LINK = "PaymentError";
        public string HotelSellStatus = "";
        public long CurrentBookingID = 0;
        #region Custom methods

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
       private Models.PaymentModel LoadData()
     
        {
            Models.PaymentModel pay = new Models.PaymentModel();
            long userId = GetUserId();


            long bookingId = BLayer.Bookings.GetCartId(userId);

            // set paymentoption fullpayment
            long PayOption = (int)CLayer.ObjectStatus.PaymentOption.FullPayment;
            BLayer.Bookings.UpdatePaymentOption(PayOption, bookingId);

            if (bookingId > 0)
            {
                List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
                CLayer.Address byAddress = BLayer.Bookings.GetBookedByUser(bookingId);
                CLayer.Booking data = BLayer.Bookings.GetDataForPayment(bookingId);
                pay.account_id = BLayer.Settings.GetValue(CLayer.Settings.PAY_ACCOUNT_ID);
                pay.reference_no = data.OrderNo;
                pay.amount = Math.Round(data.TotalAmount).ToString();

                if (System.Configuration.ConfigurationManager.AppSettings.Get("PayMode") == "1")
                {
                    pay.mode = CLayer.Settings.PAYMENT_MODE_TESTING;
                }
                else { pay.mode = CLayer.Settings.PAYMENT_MODE_LIVE; }


                //    pay.mode = CLayer.Settings.PAYMENT_MODE_LIVE;

                pay.return_url = BLayer.Settings.GetValue(CLayer.Settings.PAY_RETURN_URL);
                pay.description = BLayer.Settings.GetValue(CLayer.Settings.PAY_DESCRIPTION);
                string phone;
                //user details
                pay.name = Common.Utils.CutString((byAddress.Firstname + " " + byAddress.Lastname).Trim(), CLayer.PaymentGateway.EBS.NAME_LENGTH);
                pay.address = Common.Utils.CutString(byAddress.AddressText, CLayer.PaymentGateway.EBS.ADDRESS_LENGTH);
                pay.state = Common.Utils.CutString(byAddress.StateName, CLayer.PaymentGateway.EBS.STATE_LENGTH);
                pay.country = byAddress.CountryCode;
                pay.city = Common.Utils.CutString(byAddress.City, CLayer.PaymentGateway.EBS.CITY_LENGTH);
                if (pay.city == "") pay.city = "city";
                phone = "000000";
                if (byAddress.Mobile != "")
                    phone = byAddress.Mobile;
                else
                {
                    if (byAddress.Phone != "")
                        phone = byAddress.Phone;
                }
                if (phone.Length < 10) phone = phone.PadLeft(10, '0');
                pay.phone = Common.Utils.CutString(phone, CLayer.PaymentGateway.EBS.PHONE_LENGTH);
                pay.email = Common.Utils.CutString(byAddress.Email, CLayer.PaymentGateway.EBS.EMAIL_LENGTH);
                pay.postal_code = Common.Utils.CutString(byAddress.ZipCode, CLayer.PaymentGateway.EBS.ZIP_LENGTH);
                if (pay.postal_code == "") pay.postal_code = "00000";
                //customer details
                pay.ship_name = Common.Utils.CutString((adr[0].Firstname + " " + adr[0].Lastname).Trim(), CLayer.PaymentGateway.EBS.SHIP_NAME);
                pay.ship_address = Common.Utils.CutString(adr[0].AddressText, CLayer.PaymentGateway.EBS.SHIP_ADDR_LENGTH);
                pay.ship_state = Common.Utils.CutString(adr[0].StateName, CLayer.PaymentGateway.EBS.STATE_LENGTH);
                pay.ship_country = adr[0].CountryCode;
                pay.ship_city = Common.Utils.CutString(adr[0].City, CLayer.PaymentGateway.EBS.CITY_LENGTH);
                if (pay.ship_city == "") pay.ship_city = "city";
                phone = "000000";
                if (adr[0].Mobile != "")
                    phone = adr[0].Mobile;
                else
                {
                    if (adr[0].Phone != "")
                        phone = adr[0].Phone;
                }
                if (phone.Length < 10) phone = phone.PadLeft(10, '0');
                pay.ship_phone = Common.Utils.CutString(phone, CLayer.PaymentGateway.EBS.PHONE_LENGTH);
                pay.ship_postal_code = Common.Utils.CutString(adr[0].ZipCode, CLayer.PaymentGateway.EBS.ZIP_LENGTH);
                if (pay.ship_postal_code == "") pay.ship_postal_code = "00000";
                pay.SecurePayment();
              //  pay.secure_hash =Convert.ToString(System.Web.HttpContext.Current.Session["secure_hash"]);

              //  Session["Pay"] = pay;
                //HtmlHelper helper = new HtmlHelper(new ViewContext(ControllerContext, new WebFormView(ControllerContext, "Index"), new ViewDataDictionary(), new TempDataDictionary(), new System.IO.StringWriter()), new ViewPage());
                //helper.RenderAction("DoPayment", "Payment");//call your action

               

            }
            else
            {
                pay.amount = "0";
            }
           
            return pay;
        }
        
    
        public ActionResult DoPayment()
        {
            string Url = "/Payment/MakePayment";
            string FormName = "form1";
            string Method = "post";


            string hashedvalue = Convert.ToString(System.Web.HttpContext.Current.Session["secure_hash"]);
            Models.PaymentModel pay = (Models.PaymentModel)Session["Pay"];


            NameValueCollection FormFields = new NameValueCollection();

            FormFields.Add("account_id", pay.account_id);
            FormFields.Add("channel", "0");
            FormFields.Add("currency", "INR");
            FormFields.Add("reference_no", pay.reference_no);
            FormFields.Add("amount", pay.amount);
            FormFields.Add("description", pay.description);
            FormFields.Add("name", pay.name);
            FormFields.Add("address", pay.address);
            FormFields.Add("city", pay.city);
            FormFields.Add("state", pay.state);
            FormFields.Add("postal_code", pay.postal_code);
            FormFields.Add("country", "IND");
            FormFields.Add("email", pay.email);
            FormFields.Add("phone", pay.phone);
            FormFields.Add("mode", "TEST");
            FormFields.Add("return_url", pay.return_url);
            FormFields.Add("ship_name", pay.name);
            FormFields.Add("ship_address", pay.address);
            FormFields.Add("ship_city", pay.ship_city);
            FormFields.Add("ship_state", pay.ship_state);
            FormFields.Add("ship_country", "IND");
            FormFields.Add("ship_phone", pay.ship_phone);
            FormFields.Add("algo", "SHA512");
            FormFields.Add("ship_postal_code", pay.ship_postal_code);
            FormFields.Add("name_on_card", "null");
            FormFields.Add("card_number", "null");
            FormFields.Add("card_expiry", "null");
            FormFields.Add("card_cvv", "null");



            //FormFields.Add("account_id", "28402");
            //FormFields.Add("channel", "0");
            //FormFields.Add("currency", "INR");
            //FormFields.Add("reference_no", "CB00AD3");
            //FormFields.Add("amount", "2184");
            //FormFields.Add("description", "test");
            //FormFields.Add("name", "Mr. Shivaji Patil");
            //FormFields.Add("address", "121, Surfin Centre, Co - op.Ind.Est., M.V.Road, Near Marol Bhavan,, Marol, Andheri East");
            //FormFields.Add("city", "Mumbai");
            //FormFields.Add("state", "Maharashtra");
            //FormFields.Add("postal_code", "400059");
            //FormFields.Add("country", "IND");
            //FormFields.Add("email", "patil@artekchemicals.com");
            //FormFields.Add("phone", "9819850911");
            //FormFields.Add("mode", "TEST");
            //FormFields.Add("return_url", "http://beta.staybazar.com/response?DR={DR}");
            //FormFields.Add("ship_name", "Mr. Shivaji Patil");
            //FormFields.Add("ship_address", "121, Surfin Centre, Co - op.Ind.Est., M.V.Road, Near Marol Bhavan,, Marol, Andheri East");
            //FormFields.Add("ship_city", "Mumbai");
            //FormFields.Add("ship_state", "Maharashtra");
            //FormFields.Add("ship_country", "IND");
            //FormFields.Add("ship_phone", "9819850911");
            //FormFields.Add("algo", "SHA512");
            //FormFields.Add("ship_postal_code", "400059");
            //FormFields.Add("name_on_card", "null");
            //FormFields.Add("card_number", "null");
            //FormFields.Add("card_expiry", "null");
            //FormFields.Add("card_cvv", "null");
            // }
            Response.Clear();
            Response.Write("<html><head>");
            Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
            for (int i = 0; i < FormFields.Keys.Count; i++)
            {
                Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", FormFields.Keys[i], FormFields[FormFields.Keys[i]]));
            }
            Response.Write("</form>");
            Response.Write("</body></html>");
            Response.End();
            return View();
        }
        static int Compare1(KeyValuePair<string, string> a, KeyValuePair<string, string> b)
        {
            return a.Key.CompareTo(b.Key);
        }
        public ActionResult MakePayment()
        {
            string name = "SHA512" ;
            if (Request.HttpMethod == "POST")
            {

                //Response.Write(name);

                string hashValue = "ebskey"; //Pass your Registered secret key available from EBS secure portal
                string V3URL = "https://sandbox.secure.ebs.in/pg/ma/payment/request";

                string HashData = hashValue;

                List<KeyValuePair<string, string>> postparamslist = new List<KeyValuePair<string, string>>();

                for (int i = 0; i < Request.Form.Keys.Count; i++)
                {
                    KeyValuePair<string, string> postparam = new KeyValuePair<string, string>(Request.Form.Keys[i], Request.Form[i]);

                    if (Request.Form.Keys[i] != "V3URL" && Request.Form.Keys[i] != "submitted")
                        postparamslist.Add(postparam);
                }

                postparamslist.Sort(Compare1);

                foreach (KeyValuePair<string, string> param in postparamslist)
                {
                    HashData += "|" + param.Value;
                }

                string hashedvalue = "";

                if (hashValue.Length > 0)
                {

                    hashedvalue +=APIUtility.computeHash(HashData, name);
                }



                string FormName = "form1";
                string Method = "post";

                Response.Clear();
                Response.Write("<html><head>");
                Response.Write("<META HTTP-EQUIV=\"CACHE-CONTROL\" CONTENT=\"no-store, no-cache, must-revalidate\" />");
                Response.Write("<META HTTP-EQUIV=\"PRAGMA\" CONTENT=\"no-store, no-cache, must-revalidate\" />");
                Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, V3URL));

                foreach (KeyValuePair<string, string> param in postparamslist)
                {
                    Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\" />", param.Key, param.Value));
                }
                // Response.Write("<input type=\"hidden\" name=\"response\" value=" + md5HashData + " />");
                Response.Write("<input type=\"hidden\" name=\"secure_hash\" value=" + hashedvalue + " />");
                Response.Write("</form>");
                Response.Write("</body></html>");
                Response.End();

            }
            return View();
        }


        //public static void RedirectAndPOST(System.Web.UI.Page page)
        //{
        //    NameValueCollection data = new NameValueCollection();
        //    data.Add("cmd", "_xclick");
        //    data.Add("business", "mail@msn.com");

        //    //Prepare the Posting form
        //    string strForm = PreparePOSTForm("https://www.paypal.com/cgi-bin/webscr", data);

        //    //Add a literal control the specified page holding the Post Form, this is to submit the Posting form with the request.
        //    page.Controls.Add(new System.Web.UI.LiteralControl(strForm));
        //}

        //private static String PreparePOSTForm(string url, NameValueCollection data)
        //{
        //    //Set a name for the form
        //    string formID = "PostForm";

        //    //Build the form using the specified data to be posted.
        //    StringBuilder strForm = new StringBuilder();
        //    strForm.Append("<form id=\"" + formID + "\" name=\"" + formID + "\" action=\"" + url + "\" method=\"POST\">");
        //    foreach (string key in data)
        //    {
        //        strForm.Append("<input type=\"hidden\" name=\"" + key + "\" value=\"" + data[key] + "\">");
        //    }
        //    strForm.Append("</form>");

        //    //Build the JavaScript which will do the Posting operation.
        //    StringBuilder strScript = new StringBuilder();
        //    strScript.Append("<script language='javascript'>");
        //    strScript.Append("var v" + formID + " = document." + formID + ";");
        //    strScript.Append("v" + formID + ".submit();");
        //    strScript.Append("</script>");

        //    //Return the form and the script concatenated. (The order is important, Form then JavaScript)
        //    return strForm.ToString() + strScript.ToString();
        //}


        private Models.PaymentModel LoadDataOffline(long? BookId)
        {
            Models.PaymentModel pay = new Models.PaymentModel();
            long bookingId = BookId.Value;

            // set paymentoption fullpayment
            long PayOption = (int)CLayer.ObjectStatus.PaymentOption.FullPayment;
            BLayer.Bookings.UpdatePaymentOption(PayOption, bookingId);

            if (bookingId > 0)
            {
                List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
                CLayer.Address byAddress = BLayer.Bookings.GetBookedByUser(bookingId);
                CLayer.Booking data = BLayer.Bookings.GetDataForPayment(bookingId);
                pay.account_id = BLayer.Settings.GetValue(CLayer.Settings.PAY_ACCOUNT_ID);
                pay.reference_no = data.OrderNo;
                pay.amount = Math.Round(data.TotalAmount).ToString();

                if (System.Configuration.ConfigurationManager.AppSettings.Get("PayMode") == "1")
                {
                    pay.mode = CLayer.Settings.PAYMENT_MODE_TESTING;
                }
                else { pay.mode = CLayer.Settings.PAYMENT_MODE_LIVE; }


                //    pay.mode = CLayer.Settings.PAYMENT_MODE_LIVE;

                pay.return_url = BLayer.Settings.GetValue(CLayer.Settings.PAY_RETURN_URL);
                pay.description = BLayer.Settings.GetValue(CLayer.Settings.PAY_DESCRIPTION);
                string phone;
                //user details
                pay.name = Common.Utils.CutString((byAddress.Firstname + " " + byAddress.Lastname).Trim(), CLayer.PaymentGateway.EBS.NAME_LENGTH);
                pay.address = Common.Utils.CutString(byAddress.AddressText, CLayer.PaymentGateway.EBS.ADDRESS_LENGTH);
                pay.state = Common.Utils.CutString(byAddress.StateName, CLayer.PaymentGateway.EBS.STATE_LENGTH);
                pay.country = byAddress.CountryCode;
                pay.city = Common.Utils.CutString(byAddress.City, CLayer.PaymentGateway.EBS.CITY_LENGTH);
                if (pay.city == "") pay.city = "city";
                phone = "000000";
                if (byAddress.Mobile != "")
                    phone = byAddress.Mobile;
                else
                {
                    if (byAddress.Phone != "")
                        phone = byAddress.Phone;
                }
                if (phone.Length < 10) phone = phone.PadLeft(10, '0');
                pay.phone = Common.Utils.CutString(phone, CLayer.PaymentGateway.EBS.PHONE_LENGTH);
                pay.email = Common.Utils.CutString(byAddress.Email, CLayer.PaymentGateway.EBS.EMAIL_LENGTH);
                pay.postal_code = Common.Utils.CutString(byAddress.ZipCode, CLayer.PaymentGateway.EBS.ZIP_LENGTH);
                if (pay.postal_code == "") pay.postal_code = "00000";
                //customer details
                pay.ship_name = Common.Utils.CutString((adr[0].Firstname + " " + adr[0].Lastname).Trim(), CLayer.PaymentGateway.EBS.SHIP_NAME);
                pay.ship_address = Common.Utils.CutString(adr[0].AddressText, CLayer.PaymentGateway.EBS.SHIP_ADDR_LENGTH);
                pay.ship_state = Common.Utils.CutString(adr[0].StateName, CLayer.PaymentGateway.EBS.STATE_LENGTH);
                pay.ship_country = adr[0].CountryCode;
                pay.ship_city = Common.Utils.CutString(adr[0].City, CLayer.PaymentGateway.EBS.CITY_LENGTH);
                if (pay.ship_city == "") pay.ship_city = "city";
                phone = "000000";
                if (adr[0].Mobile != "")
                    phone = adr[0].Mobile;
                else
                {
                    if (adr[0].Phone != "")
                        phone = adr[0].Phone;
                }
                if (phone.Length < 10) phone = phone.PadLeft(10, '0');
                pay.ship_phone = Common.Utils.CutString(phone, CLayer.PaymentGateway.EBS.PHONE_LENGTH);
                pay.ship_postal_code = Common.Utils.CutString(adr[0].ZipCode, CLayer.PaymentGateway.EBS.ZIP_LENGTH);
                if (pay.ship_postal_code == "") pay.ship_postal_code = "00000";
                pay.SecurePayment();
                //data.Items = BLayer.BookingItem.GetAllUnderCart(userId);
            }
            else
            {
                pay.amount = "0";
            }

            return pay;
        }
        private Models.PaymentModel LoadDataPartialPay(long? BookId)
        {
            Models.PaymentModel pay = new Models.PaymentModel();
            long userId = GetUserId();

            long bookingId = BookId.Value;

            if (bookingId == 0)
            {
                bookingId = BLayer.Bookings.GetCartIdForPartialPay((int)CLayer.ObjectStatus.PartialPaymentStatus.Cart, userId);
            }

            //if (bookingId == 0)
            //{
            //    bookingId = BookId.Value;
            //}
            CLayer.ObjectStatus.PartialPaymentStatus PartialPayStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
            long PayOption = (int)CLayer.ObjectStatus.PaymentOption.FullPayment;
            PayOption = (int)CLayer.ObjectStatus.PaymentOption.PartialPayment;
            BLayer.Bookings.UpdatePaymentOption(PayOption, bookingId);

            if (bookingId > 0)
            {
                List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
                CLayer.Address byAddress = BLayer.Bookings.GetBookedByUser(bookingId);
                CLayer.Booking data = BLayer.Bookings.GetDataForPayment(bookingId);
                pay.account_id = BLayer.Settings.GetValue(CLayer.Settings.PAY_ACCOUNT_ID);
                pay.reference_no = data.OrderNo;


                if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentFailed || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut)
                {
                    pay.amount = Math.Round(data.PaymentFirstinstallment).ToString();
                }
                else if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                {
                    pay.amount = Math.Round(data.PaymentSecondinstallment).ToString();
                }


                if (System.Configuration.ConfigurationManager.AppSettings.Get("PayMode") == "1")
                {
                    pay.mode = CLayer.Settings.PAYMENT_MODE_TESTING;
                }
                else { pay.mode = CLayer.Settings.PAYMENT_MODE_LIVE; }


                //    pay.mode = CLayer.Settings.PAYMENT_MODE_LIVE;

                pay.return_url = BLayer.Settings.GetValue(CLayer.Settings.PAY_RETURN_URL);
                pay.description = BLayer.Settings.GetValue(CLayer.Settings.PAY_DESCRIPTION);
                string phone;
                //user details
                pay.name = Common.Utils.CutString((byAddress.Firstname + " " + byAddress.Lastname).Trim(), CLayer.PaymentGateway.EBS.NAME_LENGTH);
                pay.address = Common.Utils.CutString(byAddress.AddressText, CLayer.PaymentGateway.EBS.ADDRESS_LENGTH);
                pay.state = Common.Utils.CutString(byAddress.StateName, CLayer.PaymentGateway.EBS.STATE_LENGTH);
                pay.country = byAddress.CountryCode;
                pay.city = Common.Utils.CutString(byAddress.City, CLayer.PaymentGateway.EBS.CITY_LENGTH);
                if (pay.city == "") pay.city = "city";
                phone = "000000";
                if (byAddress.Mobile != "")
                    phone = byAddress.Mobile;
                else
                {
                    if (byAddress.Phone != "")
                        phone = byAddress.Phone;
                }
                if (phone.Length < 10) phone = phone.PadLeft(10, '0');
                pay.phone = Common.Utils.CutString(phone, CLayer.PaymentGateway.EBS.PHONE_LENGTH);
                pay.email = Common.Utils.CutString(byAddress.Email, CLayer.PaymentGateway.EBS.EMAIL_LENGTH);
                pay.postal_code = Common.Utils.CutString(byAddress.ZipCode, CLayer.PaymentGateway.EBS.ZIP_LENGTH);
                if (pay.postal_code == "") pay.postal_code = "00000";
                //customer details
                pay.ship_name = Common.Utils.CutString((adr[0].Firstname + " " + adr[0].Lastname).Trim(), CLayer.PaymentGateway.EBS.SHIP_NAME);
                pay.ship_address = Common.Utils.CutString(adr[0].AddressText, CLayer.PaymentGateway.EBS.SHIP_ADDR_LENGTH);
                pay.ship_state = Common.Utils.CutString(adr[0].StateName, CLayer.PaymentGateway.EBS.STATE_LENGTH);
                pay.ship_country = adr[0].CountryCode;
                pay.ship_city = Common.Utils.CutString(adr[0].City, CLayer.PaymentGateway.EBS.CITY_LENGTH);
                if (pay.ship_city == "") pay.ship_city = "city";
                phone = "000000";
                if (adr[0].Mobile != "")
                    phone = adr[0].Mobile;
                else
                {
                    if (adr[0].Phone != "")
                        phone = adr[0].Phone;
                }
                if (phone.Length < 10) phone = phone.PadLeft(10, '0');
                pay.ship_phone = Common.Utils.CutString(phone, CLayer.PaymentGateway.EBS.PHONE_LENGTH);
                pay.ship_postal_code = Common.Utils.CutString(adr[0].ZipCode, CLayer.PaymentGateway.EBS.ZIP_LENGTH);
                if (pay.ship_postal_code == "") pay.ship_postal_code = "00000";
                pay.SecurePayment();
                
                pay.secure_hash = Convert.ToString(System.Web.HttpContext.Current.Session["secure_hash"]);

                Session["Pay"] = pay;
                // data.Items = BLayer.BookingItem.GetAllUnderCart(userId);
            }
            else
            {
                pay.amount = "0";
            }

            return pay;
        }
        #endregion
        //
        // GET: /Payment/
        public async Task<ActionResult> Index(bool IsPartialPay, long? BookId)
        {

            Models.PaymentModel pay = new Models.PaymentModel() { amount = "0" };

            //Request For booking Submit
            long userId = GetUserId();

            CurrentBookingID =Convert.ToInt64( BookId);

            long bookingId = BookId > 0 ? Convert.ToInt64(BookId) : 0;
            if (bookingId <= 0)
            {
                bookingId = BLayer.Bookings.GetCartId(userId);
            }
            long PId = BLayer.Bookings.GetPropertyId(bookingId);

            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PId);

            if (InventoryAPIType != 0)
            {
                //MAXIMOJO
                // check this booking property assigned in maximojo
                string HotelId = BLayer.Property.GetHotelId(BLayer.Bookings.GetPropertyId(bookingId));
                if (HotelId != null && HotelId != "")
                {
                    HotelId = HotelId.Trim();
                    if (bookingId > 0)
                    {
                        bool AllowtoExtReq = true;

                        if (IsPartialPay)
                        {
                            CLayer.ObjectStatus.PartialPaymentStatus PartialPayStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                            if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart)
                            {
                                AllowtoExtReq = true;
                            }
                            else
                            {
                                AllowtoExtReq = false;            // already external request sended in partial payment case                        
                            }
                        }

                        // int RequCount = BLayer.BookingExternalInventory.GetExternalInventoryReqByBookingId(bookingId);

                        if (AllowtoExtReq)
                        {
                            bool returnstring = true;

                            if(InventoryAPIType==1)
                            {
                                returnstring = BookingSubmitRequest();  //MAXIMOJO
                            }
                            else if (InventoryAPIType ==(int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                            {
                                CLayer.B2BApprovers pb2b_Approvers = BLayer.B2BUser.GetApproverUsersExists(userId, bookingId);

                                if ((pb2b_Approvers.b2b_approver_id == 0) && (pb2b_Approvers.approver_order > 0) && (pb2b_Approvers.status == 0))
                                {

                                    bool mailSend =  await SendMailsAndSmsForCorporateBookingForApproval(bookingId);
                                    TempData["ApproverName"] = pb2b_Approvers.username;
                                    TempData["ApproverEmail"] = pb2b_Approvers.approver_email;
                                    return View("~/Views/ErrorPage/BookingApprovalAlert.cshtml");
                                    //   return RedirectToAction("BookingApprovalAlert", "ErrorPage", pb2b_Approvers);
                                }
                                else if ((pb2b_Approvers.b2b_approver_id > 0) && (pb2b_Approvers.approver_order > 0) && (pb2b_Approvers.status < 2))
                                {
                                    bool mailSend =await SendMailsAndSmsForCorporateBookingForApproval(bookingId);
                                    //  return RedirectToAction("BookingApprovalAlert", "ErrorPage", pb2b_Approvers);
                                    TempData["ApproverName"] = pb2b_Approvers.username;
                                    TempData["ApproverEmail"] = pb2b_Approvers.approver_email;

                                    return View("~/Views/ErrorPage/BookingApprovalAlert.cshtml");
                                }
                                else
                                {
                                    returnstring = BookingSubmitRequest(true, InventoryAPIType);  //Amedus
                                }

                            }
                            else if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Tamarind)
                            {
                                CLayer.B2BApprovers pb2b_Approvers = BLayer.B2BUser.GetApproverUsersExists(userId, bookingId);

                                if ((pb2b_Approvers.b2b_approver_id == 0) && (pb2b_Approvers.approver_order > 0) && (pb2b_Approvers.status == 0))
                                {

                                    bool mailSend = await SendMailsAndSmsForCorporateBookingForApproval(bookingId);
                                    TempData["ApproverName"] = pb2b_Approvers.username;
                                    TempData["ApproverEmail"] = pb2b_Approvers.approver_email;
                                    return View("~/Views/ErrorPage/BookingApprovalAlert.cshtml");
                                    //   return RedirectToAction("BookingApprovalAlert", "ErrorPage", pb2b_Approvers);
                                }
                                else if ((pb2b_Approvers.b2b_approver_id > 0) && (pb2b_Approvers.approver_order > 0) && (pb2b_Approvers.status < 2))
                                {
                                    bool mailSend = await SendMailsAndSmsForCorporateBookingForApproval(bookingId);
                                    //  return RedirectToAction("BookingApprovalAlert", "ErrorPage", pb2b_Approvers);
                                    TempData["ApproverName"] = pb2b_Approvers.username;
                                    TempData["ApproverEmail"] = pb2b_Approvers.approver_email;

                                    return View("~/Views/ErrorPage/BookingApprovalAlert.cshtml");
                                }
                                else
                                {
                                    returnstring = BookingSubmitRequest(true, InventoryAPIType);  //Amedus
                                }

                            }
                            else if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.TBO)
                            {
                                CLayer.B2BApprovers pb2b_Approvers = BLayer.B2BUser.GetApproverUsersExists(userId, bookingId);

                                if ((pb2b_Approvers.b2b_approver_id == 0) && (pb2b_Approvers.approver_order > 0) && (pb2b_Approvers.status == 0))
                                {

                                    bool mailSend = await SendMailsAndSmsForCorporateBookingForApproval(bookingId);
                                    TempData["ApproverName"] = pb2b_Approvers.username;
                                    TempData["ApproverEmail"] = pb2b_Approvers.approver_email;
                                    return View("~/Views/ErrorPage/BookingApprovalAlert.cshtml");
                                    //   return RedirectToAction("BookingApprovalAlert", "ErrorPage", pb2b_Approvers);
                                }
                                else if ((pb2b_Approvers.b2b_approver_id > 0) && (pb2b_Approvers.approver_order > 0) && (pb2b_Approvers.status < 2))
                                {
                                    bool mailSend = await SendMailsAndSmsForCorporateBookingForApproval(bookingId);
                                    //  return RedirectToAction("BookingApprovalAlert", "ErrorPage", pb2b_Approvers);
                                    TempData["ApproverName"] = pb2b_Approvers.username;
                                    TempData["ApproverEmail"] = pb2b_Approvers.approver_email;

                                    return View("~/Views/ErrorPage/BookingApprovalAlert.cshtml");
                                }
                                else
                                {
                                    returnstring = BookingSubmitRequest(true, InventoryAPIType);  //Amedus
                                }

                            }


                            //SET ERROR TYPE
                            string Errorcomesfrom = "";
                            if (Convert.ToString(TempData["Errorcomes"]) != null)
                            {
                                Errorcomesfrom = Convert.ToString(TempData["Errorcomes"]);
                            }
                            ViewBag.Errorcomes = Errorcomesfrom;

                            //RETURN VIEW
                            if (returnstring == true)
                            {
                                return View("~/Views/Payment/RequestFailed.cshtml", PId);
                            }
                        }
                    }
                }
            }


            try
            {
                if (IsPartialPay == false)
                {
                    if (BookId != null && BookId > 0)
                    {
                        pay = LoadDataOffline(BookId.Value);
                    }
                    else
                    {
                        pay = LoadData();
                      //  LoadData();
                      //  return DoPayment();
                    }

                }
                else
                {
                    if (BookId == null) { BookId = 0; }
                    pay = LoadDataPartialPay(BookId.Value);

                    //return DoPayment();
                }
                if (pay.amount == "0")
                {
                    return RedirectToAction("Index", "Booking");
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(pay);
          

        }

        [HttpPost]
        public string SetCheckout()//int payType)
        {
            return BookingSetCheckout();
        }

        private string BookingSetCheckout()
        {
            try
            {
                long userId = GetUserId();
                //long.TryParse(User.Identity.GetUserId(), out userId);
                long bookingId = BLayer.Bookings.GetCartId(userId);

                long Payoption = BLayer.Bookings.GetPaymentoption(bookingId);
                if (Payoption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.CheckOut, bookingId);
                    BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.CheckOut, bookingId);
                }
                if (Payoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                {
                    CLayer.ObjectStatus.PartialPaymentStatus PartialPaystatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                    if (PartialPaystatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart)
                    {
                        BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.CheckOut, bookingId);
                        BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.CheckOut, bookingId);
                    }

                }
                //   BLayer.Bookings.SetPayType(payType, bookingId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
        }

        //Paypal Methods
        // [HttpPost]
        public ActionResult ByPaypal(int IsPartial, string refId)
        {

            //Request For booking Submit
            string token = "";
            string errorPage = System.Configuration.ConfigurationManager.AppSettings.Get(PAYMENT_FAILED_LINK);
            CLayer.Booking bk = null;
            long userId = GetUserId();
            bk = BLayer.Bookings.GetDetails(refId);
            if (bk == null)
            {
                return Redirect(errorPage);
            }
            long bookingId = bk.BookingId;
            long PId = BLayer.Bookings.GetPropertyId(bookingId);

            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PId);
            if (InventoryAPIType != 0)
            {
                string HotelId = BLayer.Property.GetHotelId(PId);
                if (HotelId != null && HotelId != "")
                {
                    HotelId = HotelId.Trim();
                    if (bookingId > 0)
                    {
                        bool AllowtoExtReq = true;

                        if (IsPartial == 1)
                        {
                            CLayer.ObjectStatus.PartialPaymentStatus PartialPayStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                            if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart)
                            {
                                AllowtoExtReq = true;
                            }
                            else
                            {
                                AllowtoExtReq = false;            // already external request done                          
                            }
                        }

                        //  int RequCount = BLayer.BookingExternalInventory.GetExternalInventoryReqByBookingId(bookingId);
                        if (AllowtoExtReq)
                        {

                            bool returnstring = BookingSubmitRequest();

                            string Errorcomesfrom = "";
                            if (Convert.ToString(TempData["Errorcomes"]) != null)
                            {
                                Errorcomesfrom = Convert.ToString(TempData["Errorcomes"]);
                            }
                            ViewBag.Errorcomes = Errorcomesfrom;
                            if (returnstring == true)
                            {
                                return View("~/Views/Payment/RequestFailed.cshtml", PId);
                            }
                        }
                    }
                }
            }



            // do the posting here


            try
            {
                //fetch booking details

                decimal amount = bk.TotalAmount;//total amount -in RS - to pay
                if (IsPartial == 1) //if it is a partial payment
                {
                    CLayer.ObjectStatus.PartialPaymentStatus PartialPayStatus = BLayer.Bookings.GetPartialPaymentStatus(bk.BookingId);
                    long PayOption = (int)CLayer.ObjectStatus.PaymentOption.FullPayment;
                    PayOption = (int)CLayer.ObjectStatus.PaymentOption.PartialPayment;
                    BLayer.Bookings.UpdatePaymentOption(PayOption, bk.BookingId);

                    List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bk.BookingId);
                    CLayer.Address byAddress = BLayer.Bookings.GetBookedByUser(bk.BookingId);
                    CLayer.Booking data = BLayer.Bookings.GetDataForPayment(bk.BookingId);


                    if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentFailed || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut)
                    {
                        amount = Math.Round(data.PaymentFirstinstallment);
                    }
                    else if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                    {
                        amount = Math.Round(data.PaymentSecondinstallment);
                    }
                }
                //load paypal url from settings
                string url = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_TOKEN_URL);

                WebRequest rqst = HttpWebRequest.Create(url);

                // only needed, if you use HTTP AUTH
                //CredentialCache creds = new CredentialCache();
                //creds.Add(new Uri(url), "Basic", new NetworkCredential(this.Uname, this.Pwd));
                //rqst.Credentials = creds;
                //rqst.Method = method;
                rqst.Method = "POST";
                string user, pwd, signature, returnurl, cancelurl;
                user = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_USER);
                pwd = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_PWD);
                signature = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_SIGNAUTRE);
                returnurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_RETURNURL);
                cancelurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_CANCELURL);

                CLayer.Currency cur = BLayer.Currency.Get("USD");
                if (cur == null)
                {
                    throw new Exception("Cannot find USD conversion rate for Paypal booking");
                }
                amount = Math.Round(amount * cur.ConversionRate, 2, MidpointRounding.AwayFromZero);
                CLayer.Booking dat = BLayer.Bookings.GetDetailsSMS(bk.BookingId);
                string ProBookDes = dat.PropertyTitle + "(" + bk.CheckIn.ToShortDateString() + "-" + bk.CheckOut.ToShortDateString() + ")";
                string postdata = "METHOD=SetExpressCheckout&VERSION=109.0";
                postdata = postdata + "&USER=" + user + "&PWD=" + pwd + "&SIGNATURE=" + signature;
                postdata = postdata + "&PAYMENTREQUEST_0_AMT=" + amount.ToString("F2") + "&PAYMENTREQUEST_0_CURRENCYCODE=USD" + "&PAYMENTREQUEST_0_DESC=" + ProBookDes;
                postdata = postdata + "&RETURNURL=" + Server.UrlEncode(returnurl);
                postdata = postdata + "&CANCELURL=" + Server.UrlEncode(cancelurl);
                postdata = postdata + "&PAYMENTREQUEST_0_PAYMENTACTION=Sale";

                if (!String.IsNullOrEmpty(postdata))
                {
                    //rqst.ContentType = "application/xml";
                    rqst.ContentType = "application/x-www-form-urlencoded";
                    //st.ContentType = "text/html; charset=UTF-8";
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(postdata);
                    rqst.ContentLength = byteData.Length;
                    using (Stream postStream = rqst.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                        postStream.Close();
                    }
                }
                ((HttpWebRequest)rqst).KeepAlive = false;
                StreamReader rsps = new StreamReader(rqst.GetResponse().GetResponseStream());
                string strRsps = rsps.ReadToEnd();
                Debug.WriteLine(strRsps);
                token = PaymentController.GetToken(strRsps);
                BLayer.Bookings.SetTryingGateway(bk.BookingId, CLayer.ObjectStatus.Gateway.PayPal);
                if (token == "")
                {
                    //failed do restore and got to error page

                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bk.BookingId);
                    //  BLayer.Bookings.SetTryingGateway(bk.BookingId, CLayer.ObjectStatus.Gateway.PayPal);
                    return Redirect(errorPage);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                //revert booking 
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bk.BookingId);
                return Redirect(errorPage);//read it from web.config
            }

            string red = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_REQUEST_URL) + token;
            token = Server.UrlDecode(token);
            //save to database
            BLayer.Bookings.SetPaymentToken(bk.BookingId, token);
            BookingSetCheckout();
            return Redirect(red);
        }

        public static string GetToken(string result)
        {
            string[] items = result.Split('&');
            foreach (string s in items)
            {
                if (s.Contains("TOKEN="))
                {
                    string[] val = s.Split('=');
                    if (val.Length < 2) return "";
                    return val[1].Trim();
                }
            }
            return "";
        }

        public String Success()
        {
            return "Got it";
        }

        public ActionResult Failed(int? status)
        {
            int st = 0;
            if (status.HasValue)
            {
                st = status.Value;
                if (st < 0) st = 0;
            }
            return View("Failed", st);
        }
        public async Task<ActionResult> SuccessResult(int? status)
        {
            int st = 0;
            if (status.HasValue)
            {
                st = status.Value;
                if (st < 0) st = 0;
                BLayer.Bookings.UpdateStatus(Convert.ToInt64(status), (int)CLayer.ObjectStatus.BookingStatus.Success);
                bool mailSend = await SendMailsAndSmsForCorporateBookingResult(st);

            }
            return View("SuccessResult", st);
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

        public bool BookingSubmitRequest()//MAXIMOJO
        {
            long propertyId = 0;
            long bookingId = 0;
            try
            {
                long userId = GetUserId();
                bookingId = BLayer.Bookings.GetCartId(userId);
                string IpAdds = GetIPAddress();
                propertyId = BLayer.BookingExternalInventory.GetPropertyIdByBookingId(bookingId);
                List<CLayer.BookingItem> bookitems = BLayer.BookingItem.GetAllDetails(bookingId);
                string HotelId = BLayer.Property.GetHotelId(BLayer.Bookings.GetPropertyId(bookingId));

                if (HotelId != null && HotelId != "")
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
                                                    amount = Convert.ToDouble(ro.final_price_at_bookingamt),
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
                                            ExternalBook.final_price_at_bookingamt = Convert.ToDecimal(Bookingdetails.reservation.receipt.final_price_at_booking.amount);
                                            ExternalBook.final_price_at_checkoutamt = Convert.ToDecimal(Bookingdetails.reservation.receipt.final_price_at_checkout.amount);
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
                                            Exception Error = new Exception("#Error from  External Booking Submit Request(Payment , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex + "  ,  " + " ResponseString : - " + errorinrequest);
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
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ExternalBookRequestCancel(bookingId);
                Exception Error = new Exception("#Error from External Booking Submit Request(Payment , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
                TempData["Errorcomes"] = "internalerror";
                return true;
            }
            return false;
        }
        private System.Xml.XmlElement GetChildByName(System.Xml.XmlElement parent, string childName, System.Xml.XmlDocument xmlDocument)
        {
            // Try to find it in the parent element.
            System.Xml.XmlElement childElement = parent.SelectSingleNode(childName) as System.Xml.XmlElement;
            if (null == childElement)
            {
                // The child element does not exists, so create it.
                childElement = xmlDocument.CreateElement(childName);
                parent.AppendChild(childElement);
            }
            return childElement;
        }
       
        public bool BookingSubmitRequest(bool IsAmedus,int InventoryAPIType=0)//Amedus
        {
            long propertyId = 0;
            long bookingId = 0;
            string BookingCode = "";
            bool BookingStatus = true;
            try
            {
                long userId = GetUserId();

                bookingId = CurrentBookingID  > 0 ? Convert.ToInt64(CurrentBookingID) : 0;
                if (bookingId <= 0)
                {
                    bookingId = BLayer.Bookings.GetCartId(userId);
                }

               //     bookingId = BLayer.Bookings.GetCartId(userId);
                string IpAdds = GetIPAddress();
                propertyId = BLayer.BookingExternalInventory.GetPropertyIdByBookingId(bookingId);
                List<CLayer.BookingItem> bookitems = new List<CLayer.BookingItem>();

                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    bookitems = BLayer.BookingItem.GetAllDetails(bookingId,true);
                }
                else
                {
                    bookitems = BLayer.BookingItem.GetAllDetails(bookingId);
                }
                 
                string HotelId = BLayer.Property.GetHotelId(BLayer.Bookings.GetPropertyId(bookingId));

              
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                #region HOTEL SELL
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELSELLACTION); 

                    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                    srch.HotelCode = HotelId;
                    List<CLayer.RoomStaysResult> RoomStaysResultList = APIUtility.AmedusHotelRoomStaysResultList;

                    RoomStaysResultList = (List<CLayer.RoomStaysResult>)TempData["RoomStaysResult"];
                    TempData.Keep("RoomStaysResult");
                    string SoapHeaderStateful = string.Empty;
                    foreach (var item in RoomStaysResultList)
                    {
                         BookingCode = item.BookingCode;
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
                            string ControlNumber = Convert.ToString(Session["ControlNumber"]);
                            ViewBag.HotelSellResult = "RequestFailed";
                            TempData["Errorcomes"] = "RequestFailed";

                            List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCode);
                            foreach (var itemerror in objBookItemsError)
                            {
                                BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, result);
                            }

                            #region PNR CANCEL
                            StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                          //  BookingStatus = objBooking.BookingDecline(bookingId, ControlNumber);
                            BookingStatus = objBooking.BookingCancel(bookingId, ControlNumber);

                            //  string resultCancel = APIUtility.ExecutePNRCancel("", (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRGeneration);

                            BookingStatus = true;

                            #endregion

                            //#region GDS Signout
                            //Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSSIGNOUT);
                            //SoapHeaderStateful = APIUtility.SetSecuritySignOutSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                            //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                            //string SignOutSoapBody = APIUtility.SecuritySignOut();
                            //// string SecuritySignOutHeader = APIUtility.SetSecuritySignOutSoapHeader();
                            //string SecuritySignOut = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, SignOutSoapBody);
                            //#endregion
                            return BookingStatus;


                        }



                    }
                    #endregion

                #region PNR MULTIELEMENTS-FINAL
                    Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
                    SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                    string  BookingCodeFinal = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCodeFinal, "test@test.com", 10);

                    TempData["RoomStaysResult"] = RoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = RoomStaysResultList;

                    string resultFinal = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);
           
                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElementsConfirm, bookingId);

                    #endregion Transaction log end

                    Serializer pnrser = new Serializer();

                    CLayer.PNRAddElementsConfirm.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElementsConfirm.Envelope>(resultFinal);
                  //  Session["AmadeusRefNumber"] = PNRAddResult.Body.PNR_Reply.originDestinationDetails.itineraryInfo.hotelReservationInfo.cancelOrConfirmNbr.reservation.controlNumber;
                    Session["ControlNumber"] = PNRAddResult.Body.PNR_Reply.pnrHeader.reservationInfo.reservation.controlNumber;

                    if (APIUtility.CheckHotelBookingErrorExists(resultFinal))
                    {
                       
                        Serializer PNRADDError = new Serializer();
                        CLayer.PNRAddElementsError.Envelope PNRAddErrorResult = PNRADDError.Deserialize<CLayer.PNRAddElementsError.Envelope>(resultFinal);
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
                        string ControlNumber = Convert.ToString(Session["ControlNumber"]);
                        StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                   //     BookingStatus = objBooking.BookingDecline(bookingId, ControlNumber);
                        BookingStatus = objBooking.BookingCancel(bookingId, ControlNumber);
                        BookingStatus = true;

                        //    return false;
                        #endregion

                        //#region GDS Signout
                        //Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSSIGNOUT);
                        //SoapHeaderStateful = APIUtility.SetSecuritySignOutSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        //string SignOutSoapBody = APIUtility.SecuritySignOut();
                        //// string SecuritySignOutHeader = APIUtility.SetSecuritySignOutSoapHeader();
                        //string SecuritySignOut = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, SignOutSoapBody);
                        //#endregion

                        return BookingStatus;

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

                        //#region GDS Signout
                        //Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSSIGNOUT);
                        //SoapHeaderStateful = APIUtility.SetSecuritySignOutSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        //string SignOutSoapBody = APIUtility.SecuritySignOut();
                        //// string SecuritySignOutHeader = APIUtility.SetSecuritySignOutSoapHeader();
                        //string SecuritySignOut = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, SignOutSoapBody);
                        //#endregion


                    }
                    #endregion
                    List<CLayer.BookingItem> objBookItems = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCode);
                    foreach(var item in objBookItems)
                    {
                        BLayer.BookingItem.UpdateHotelConfirmNumber(item.BookingId, item.AccommodationId,Convert.ToString(Session["ControlNumber"]));
                    }
                 


                }

                if (HotelId != null && HotelId != "")
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
                                                    amount = Convert.ToDouble(ro.final_price_at_bookingamt),
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
                                            ExternalBook.final_price_at_bookingamt = Convert.ToDecimal(Bookingdetails.reservation.receipt.final_price_at_booking.amount);
                                            ExternalBook.final_price_at_checkoutamt = Convert.ToDecimal(Bookingdetails.reservation.receipt.final_price_at_checkout.amount);
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
                                            Exception Error = new Exception("#Error from  External Booking Submit Request(Payment , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex + "  ,  " + " ResponseString : - " + errorinrequest);
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
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    BookingController objBooking = new BookingController();

                    objBooking.AmedusHotelBookingCancel("");
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

        public ActionResult CancelBooking(long BookingID)
        {
            try
            {
               long propertyId = BLayer.BookingExternalInventory.GetPropertyIdByBookingId(BookingID);
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(propertyId);
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {

                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, BookingID);
                }
                else
                {
                    ExternalBookRequestCancel(BookingID);
                }
                return Json(new { success = true, responseText = "Success" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from External Booking Request Cancel (Payment , ExternalBookRequestCancel) for bookingId :- " + BookingID + " , " + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
                return Json(new { success = true, responseText = "Error" }, JsonRequestBehavior.AllowGet);

            }
            return View("");
        }

        public ActionResult RequestFailed(long propertyId, string Message)
        {
            ViewBag.Errorcomes = Message;
            return View("~/Views/Payment/RequestFailed.cshtml", propertyId);
        }

        public ActionResult ExternalBookRequestCancel(long bookingId)//MAXIMOJO
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


        public async Task<bool> SendMailsAndSmsForCorporateBookingForApproval(long bookingId)
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
                    //string userid = User.Identity.GetUserId();
                    //long UserID =Convert.ToInt64(userid);
                    //CLayer.GDSUserAddress objUser = BLayer.User.GDUSUserDetails(UserID);
            
                    //send mail to user
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingApprovalRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    //#if DEBUG
                    //                    forUser[0].Email = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
                    //#endif
                   
                    string email = User.Identity.GetUserName();
                   CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);

                    string CorpAdmin = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATE);
                    string CorpUser = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEUSER);
                    string CorpApprover1 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER1);
                    string CorpApprover2 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER2);
                    string  CorporateTestMail = BLayer.Settings.GetValue(CLayer.Settings.CORPORATETESTEMAIL);

                    CLayer.Booking ApproverDetails = BLayer.Bookings.GetNextApproverDetails(bookingId);
                    //booking user email
                    CLayer.Booking BookingUserDetails = BLayer.Bookings.GetBookingUserDetails(bookingId);

                    if(ApproverDetails==null)
                    {
                        if (!string.IsNullOrEmpty(BookingUserDetails.BookingUserEmail))
                        {
                            forUser[0].Email = ApproverDetails.ApproverEmail + "," + BookingUserDetails.BookingUserEmail;
                        }
                    }
                    else
                    {
                        forUser[0].Email = ApproverDetails.ApproverEmail;
                    }

                    customerMsg.To.Add(forUser[0].Email);
//                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
//                    if (BccEmailsforcususer != "")
//                    {
//                        string[] emails = BccEmailsforcususer.Split(',');

//                        for (int i = 0; i < emails.Length; ++i)
//                        {
////#if DEBUG
////                            emails[i] = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
////#endif 
//                            customerMsg.Bcc.Add(emails[i]);
//                        }
//                    }
                    customerMsg.Subject = "Booking Approval Request";
                    customerMsg.Body = message;
                    customerMsg.IsBodyHtml = true;
                    try
                    {

                        //if ((email == CorporateTestMail) || (CorpAdmin == email) || (CorpUser == email) || (CorpApprover1 == email) || (CorpApprover2 == email))
                        //{
                            
                        //}
                            await ml.SendMailAsyncForBooking(customerMsg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }
                    // sending sms
                    try
                    {
#if !DEBUG
                    //    forUser[0].Mobile = System.Configuration.ConfigurationManager.AppSettings.Get("TestSMSNumber");

#endif
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = Common.SMSGateway.GetBookingApprovalMessage(forUser[0].Firstname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                            details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);
                        //  block sms
#if !DEBUG

                  //      var smsresult =  SendMailsAndSms(bookingId);

                          if (phone != "")
                              { b =  Common.SMSGateway.SendSMSSync(smsmsg, phone); }
#endif


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

        public async Task<bool> SendMailsAndSmsForCorporateBookingResult(long bookingId)
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
                    //string userid = User.Identity.GetUserId();
                    //long UserID =Convert.ToInt64(userid);
                    //CLayer.GDSUserAddress objUser = BLayer.User.GDUSUserDetails(UserID);

                    //send mail to user
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingConfirmResult") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    //#if DEBUG
                    //                    forUser[0].Email = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
                    //#endif

                    string email = User.Identity.GetUserName();
                    CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);

                    string CorpAdmin = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATE);
                    string CorpUser = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEUSER);
                    string CorpApprover1 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER1);
                    string CorpApprover2 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER2);
                    string CorporateTestMail = BLayer.Settings.GetValue(CLayer.Settings.CORPORATETESTEMAIL);

                    CLayer.Booking ApproverDetails = BLayer.Bookings.GetNextApproverDetails(bookingId);
                    //booking user email
                    CLayer.Booking BookingUserDetails = BLayer.Bookings.GetBookingUserDetails(bookingId);

                    if (ApproverDetails == null)
                    {
                        if (!string.IsNullOrEmpty(BookingUserDetails.BookingUserEmail))
                        {
                            forUser[0].Email = ApproverDetails.ApproverEmail + "," + BookingUserDetails.BookingUserEmail;
                        }
                    }
                    else
                    {
                        forUser[0].Email = ApproverDetails.ApproverEmail;
                    }

                    customerMsg.To.Add(forUser[0].Email);
                    //                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    //                    if (BccEmailsforcususer != "")
                    //                    {
                    //                        string[] emails = BccEmailsforcususer.Split(',');

                    //                        for (int i = 0; i < emails.Length; ++i)
                    //                        {
                    ////#if DEBUG
                    ////                            emails[i] = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
                    ////#endif 
                    //                            customerMsg.Bcc.Add(emails[i]);
                    //                        }
                    //                    }
                    customerMsg.Subject = "Booking Status";
                    customerMsg.Body = message;
                    customerMsg.IsBodyHtml = true;
                    try
                    {

                        //if ((email == CorporateTestMail) || (CorpAdmin == email) || (CorpUser == email) || (CorpApprover1 == email) || (CorpApprover2 == email))
                        //{

                        //}
                        await ml.SendMailAsyncForBooking(customerMsg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }
                    // sending sms
                    try
                    {
#if !DEBUG
                    //    forUser[0].Mobile = System.Configuration.ConfigurationManager.AppSettings.Get("TestSMSNumber");

#endif
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = Common.SMSGateway.GetBookingApprovalMessage(forUser[0].Firstname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                            details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);
                        //  block sms
#if !DEBUG

                  //      var smsresult =  SendMailsAndSms(bookingId);

                          if (phone != "")
                              { b =  Common.SMSGateway.SendSMSSync(smsmsg, phone); }
#endif


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

    }

}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        