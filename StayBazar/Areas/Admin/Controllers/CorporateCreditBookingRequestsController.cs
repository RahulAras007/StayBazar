using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class CorporateCreditBookingRequestsController : Controller
    {
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.CreditBookingModel search = new Models.CreditBookingModel();
            List<CLayer.Booking> users = BLayer.Transaction.GetAllCorporateCreditBookingsRequestForSearch((int)CLayer.ObjectStatus.Corpcreditbookstatus.All, "", 0, 0, 25);
            search.Status = (int)CLayer.ObjectStatus.Corpcreditbookstatus.All;
            search.SearchString = "";
            search.SearchValue = 1;
            search.TotalRows = 0;
            search.Bookinglist = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View(search);
        }
        [HttpPost]
        public ActionResult Pager(Models.CreditBookingModel data)
        {
            if (data.SearchString == null) data.SearchString = " ";
            if (data.SearchString == " ")
            {
                data.SearchValue = 0;
            }
            List<CLayer.Booking> users = BLayer.Transaction.GetAllCorporateCreditBookingsRequestForSearch(data.Status, data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.CreditBookingModel();
            Models.CreditBookingModel forPager = new Models.CreditBookingModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }

        [HttpPost]
        public ActionResult Filter(Models.CreditBookingModel data)
        {
            if (data.SearchString == null)
                data.SearchString = " ";
            if (data.SearchString == " ")
            {
                data.SearchValue = 0;
            }
            List<CLayer.Booking> users = BLayer.Transaction.GetAllCorporateCreditBookingsRequestForSearch(data.Status, data.SearchString, data.SearchValue, 0, 25);
            ViewBag.Filter = new Models.CreditBookingModel();
            Models.CreditBookingModel forPager = new Models.CreditBookingModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        [HttpPost]
        public ActionResult FillBookedByAddressSearch(long BookingId)
        {
            List<CLayer.Address> users = BLayer.Transaction.BookedByAddressSearch(BookingId);
            return PartialView("_AddressBookedList", users);
        }
        [HttpPost]
        public ActionResult Booking_GetBookedFor(long BookingId)
        {
            List<CLayer.Address> users = BLayer.Bookings.GetBookedForUser(BookingId);
            return PartialView("_AddressForBooking", users);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult PaymentBooking(Models.CreditBookingModel data)
        {
            try
            {
                CLayer.CreditBooking corporatebook = new CLayer.CreditBooking();

                corporatebook.bookid = data.bookid;
                corporatebook.Paymentdate = data.Paymentdate;
                //corporatebook.Paid = data.Paid;
                if (data.CreditComment == null) { data.CreditComment = " "; }
                corporatebook.CreditComment = data.CreditComment;
                string userid = User.Identity.GetUserId();
                corporatebook.UpdatedBy = Convert.ToInt32(userid);
                BLayer.CreditBooking.SaveCorBookingPayment(corporatebook);

                //if (data.Paid == true)
                //{
                //    BLayer.CreditBooking.SetCreditBookingstatus((int)CLayer.ObjectStatus.Corpcreditbookstatus.Paid, data.bookid);
                //}
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            //List<CLayer.Booking> users = BLayer.Transaction.GetAllCorporateCreditBookingsForSearch((int)CLayer.ObjectStatus.Corpcreditbookstatus.All, "", 0, 0, 25);
            return null;
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult MarkAsPayment(long BId, Models.CreditBookingModel data)
        {
            try
            {
                bool Paid = true;
                BLayer.CreditBooking.SetCreditBookingPaid(Paid, BId);
                BLayer.CreditBooking.SetCreditBookingstatus((int)CLayer.ObjectStatus.Corpcreditbookstatus.Paid, BId);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            List<CLayer.Booking> users = BLayer.Transaction.GetAllCorporateCreditBookingsRequestForSearch((int)CLayer.ObjectStatus.Corpcreditbookstatus.Paid, "", 0, 0, 25);
            ViewBag.Filter = new Models.CreditBookingModel();
            Models.CreditBookingModel forPager = new Models.CreditBookingModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;

            return PartialView("_List", users);
        }


        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult GetDetails(long BId)
        {
            Models.CreditBookingModel data = new Models.CreditBookingModel();
            try
            {
                CLayer.CreditBooking dt = BLayer.CreditBooking.GetAllCredBookDetailsbyBookid(BId);

                if (dt != null)
                {
                    data.CreditComment = dt.CreditComment;
                    data.Paymentdate = dt.Paymentdate;
                }
                if (data.Paymentdate == null) { data.Paymentdate = DateTime.Now.ToShortDateString(); }
                data.bookid = BId;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("_paypopup", data);
        }
        //Edit Status
        [Common.AdminRolePermission(AllowAllRoles = true)]
        [HttpPost]
        public ActionResult StatusCancel(long BookingId)
        {
            //try
            //{
            if (BookingId > 0)
            {
                BLayer.Transaction.CancelAllTransactions(BookingId);
                //ViewBag.Saved = true;
                //return RedirectToAction("Index");
            }
            else
            {
                // ViewBag.Saved = false; 
            }
            return RedirectToAction("Index");

            //}
            //catch (Exception ex)
            //{
            //    Common.LogHandler.HandleError(ex);
            //    return RedirectToAction("Index");
            //}

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






        //RESEND EMAIL
        public async Task<bool> ResendemailC(long bookingId)
        {
            try
            {
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
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
        public async Task<bool> ResendemailS(long bookingId)
        {
            try
            {
                if (bookingId < 1) return false;
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                try
                {
                    string message = "";
                    Common.Mailer ml = new Common.Mailer();
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
            return true;
        }

        //RESEND SMS
        public async Task<bool> ResendsmsC(long bookingId)
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
                    {
                        b = await Common.SMSGateway.Send(smsmsg, phone);
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
        public async Task<bool> ResendsmsS(long bookingId)
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
                    string phone = "";
                    bool b = false;
                    string smsmsg = "";
                    smsmsg = Common.SMSGateway.GetSupplierBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                        details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    phone = Common.Utils.GetMobileNo(supplier.Mobile);
                    if (phone != "")
                    {

                        b = await Common.SMSGateway.Send(smsmsg, phone);
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
        public bool BookingDecline(long BookingId)
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Decline, BookingId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return false;
            }
            return true;
        }
    }
}