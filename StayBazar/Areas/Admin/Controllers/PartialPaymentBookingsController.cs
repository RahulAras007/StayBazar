using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class PartialPaymentBookingsController : Controller
    {
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.TransactionsModel search = new Models.TransactionsModel();
            List<CLayer.Booking> users = BLayer.Transaction.GetAllPartialPaymentForSearch((int)CLayer.ObjectStatus.BookingStatus.CheckOut, "", 0, 0, 25);
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
        public ActionResult Pager(Models.TransactionsModel data)
        {
            if (data.SearchString == null) data.SearchString = " ";
            if (data.SearchString == " ")
            {
                data.SearchValue = 0;
            }
            List<CLayer.Booking> users = BLayer.Transaction.GetAllPartialPaymentForSearch((int)CLayer.ObjectStatus.BookingStatus.CheckOut, data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.TransactionsModel();
            Models.TransactionsModel forPager = new Models.TransactionsModel()
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
        public ActionResult Filter(Models.TransactionsModel data)
        {
            if (data.SearchString == null)
                data.SearchString = " ";
            if (data.SearchString == " ")
            {
                data.SearchValue = 0;
            }
            List<CLayer.Booking> users = BLayer.Transaction.GetAllPartialPaymentForSearch((int)CLayer.ObjectStatus.BookingStatus.CheckOut, data.SearchString, data.SearchValue, 0, 25);
            ViewBag.Filter = new Models.TransactionsModel();
            Models.TransactionsModel forPager = new Models.TransactionsModel()
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
        //Edit Status
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



        public ActionResult RestoreInfo(long bookingId)
        {
            Models.TranRestoreModel tm = new Models.TranRestoreModel();
            try
            {
                if (bookingId < 1)
                {
                    tm.Message = "Invalid Booking ID";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }
                bool succ = BLayer.Bookings.CanRestore(bookingId);
                if (!succ)
                {
                    tm.Message = "The booking cannot be restored due to non-availability of rooms.";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }
                tm.BookingId = bookingId;
                tm.Failed = false;
                tm.ShowMsg = false;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                tm.Message = "System could not process the data due to an Error.";
                tm.Failed = true;
            }
            return View("~/Areas/Admin/Views/PartialPaymentBookings/_RestoreBox.cshtml", tm);
        }
        public ActionResult RestoreBooking(Models.TranRestoreModel data)
        {
            Models.TranRestoreModel tm = new Models.TranRestoreModel();
            try
            {
                if (data.BookingId < 1)
                {
                    tm.Message = "Invalid Booking ID";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }
                bool succ = BLayer.Bookings.CanRestore(data.BookingId);
                if (!succ)
                {
                    tm.Message = "The booking cannot be restored due to non-availability of rooms.";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }
                if (data.Amount == 0 || data.TransactionId == "" || data.PaymentId == "" || data.PaymentCode < 1)
                {
                    tm.Message = "Invalid Transaction Details.";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }

                long payoption = BLayer.Bookings.GetPaymentoption(data.BookingId);
                if (payoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                {
                    //BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, data.BookingId);
                    BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentSuccess, data.BookingId);
                }
                BLayer.Transaction.Save(new CLayer.Transaction() { BookingId = data.BookingId, Amount = data.Amount, TransactionId = data.TransactionId, PaymentId = data.PaymentId, ResponseCode = 0, TransactionType = CLayer.ObjectStatus.TransactionType.Payment, PaymentType = data.PaymentCode, Status = CLayer.ObjectStatus.TransactionStatus.Payment });
                tm.Failed = false;
                tm.ShowMsg = true;
                tm.Message = "Booking has been restored.";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                tm.Message = "System could not process the data due to an Error.";
                tm.Failed = true;
            }
            return PartialView("~/Areas/Admin/Views/PartialPaymentBookings/_RestoreBox.cshtml", tm);
        }

    }
}