using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace StayBazar.Areas.Admin.Controllers
{
    [Common.RoleRequired(Administrator = true, Staff = true, SalesPerson = true)]
    public class OfflineBookingListController : Controller
    {
        // GET: /Admin/Transactions/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.OfflineBookingModel search = new Models.OfflineBookingModel();

            long userId = GetUserId();
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch("", 1, 0, 25, 1, userId);
            search.SearchString = "";
            search.SearchValue = 1;
            search.TotalRows = 0;
            search.OfflineBookingList = users;
            search.SaveStatus = 1;
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
        public ActionResult Pager(Models.OfflineBookingModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            long userId = GetUserId();
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch(data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit, data.SaveStatus, userId);
            ViewBag.Filter = new Models.OfflineBookingModel();
            Models.OfflineBookingModel forPager = new Models.OfflineBookingModel()
            {
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
        public ActionResult Filter(Models.OfflineBookingModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            long userId = GetUserId();
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch(data.SearchString, data.SearchValue, 0, 25, data.SaveStatus, userId);
            ViewBag.Filter = new Models.OfflineBookingModel();
            Models.OfflineBookingModel forPager = new Models.OfflineBookingModel()
            {
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



        public async Task<bool> ResendemailC(long OfflineBookingId)
        {
            return true;
            try
            {
                if (OfflineBookingId < 1) return false;
                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookingId);
                CLayer.OfflineBooking CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookingId);

                if (CustomerDetails == null) return false;
                if (OfflinePropertydata == null) return false;

                CLayer.StayCategory Staycategorydetails = BLayer.StayCategory.Get(Convert.ToInt32(Offlinedata.StayCategoryid));
                CLayer.AccommodationType AccommodationTypedetails = BLayer.AccommodationType.Get(Convert.ToInt32(Offlinedata.Accommodationtypeid));

                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingConfirmation") + OfflineBookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //for normal user mail body
                message = await Common.Mailer.MessageFromHtml(link);
                System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();
                //guest mail added
                customerMsg.To.Add(CustomerDetails.CustomerEmail);

                string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                if (BccEmailsforcususer != "")
                {
                    string[] emails = BccEmailsforcususer.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        customerMsg.Bcc.Add(emails[i]);
                    }
                }
                customerMsg.Subject = "Booking Confirmation";
                customerMsg.Body = message;

                customerMsg.IsBodyHtml = true;
                try
                {
                    await ml.SendMailAsync(customerMsg, Common.Mailer.MailType.Reservation);
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
        public async Task<bool> ResendemailS(long OfflineBookingId)
        {
            return true;
            try
            {

                if (OfflineBookingId < 1) return false;
                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookingId);
                CLayer.OfflineBooking CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookingId);

                if (CustomerDetails == null) return false;
                if (OfflinePropertydata == null) return false;

                CLayer.StayCategory Staycategorydetails = BLayer.StayCategory.Get(Convert.ToInt32(Offlinedata.StayCategoryid));
                CLayer.AccommodationType AccommodationTypedetails = BLayer.AccommodationType.Get(Convert.ToInt32(Offlinedata.Accommodationtypeid));

                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                if (OfflineBookingId < 1) return false;
                try
                {

                    if (OfflinePropertydata.PropertyEmail != "")
                    {
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntemationOfflineBook") + OfflineBookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();

                        string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                        if (BccEmailsforsupp != "")
                        {
                            string[] emails = BccEmailsforsupp.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                supplierMsg.Bcc.Add(emails[i]);
                            }
                        }

                        supplierMsg.Subject = "Booking Confirmation";
                        supplierMsg.Body = message;

                        if (OfflinePropertydata.PropertyEmail != "")
                        {
                            supplierMsg.To.Add(OfflinePropertydata.PropertyEmail);
                        }
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


        [Common.RoleRequired(Administrator = true, Staff = true, SalesPerson = true)]
        public ActionResult ReportPdf(long OfflineBookingId)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            data.OfflineBookingId = OfflineBookingId;
            ViewBag.submitenable = "No";
            return new ViewAsPdf("~/Areas/Admin/Views/OfflineBooking/BookingDataPreview.cshtml", data);
        }

        public async Task<bool> DeleteOfflineSavedData(long OfflineBookingId)
        {
            try
            {
                if (User.IsInRole("Administrator"))
                {
                    BLayer.OfflineBooking.DeleteOfflineBooking(OfflineBookingId);
                    return true;
                }
                else
                {
                    if (BLayer.OfflineBooking.CheckOfflineBookingDeleteorNot(OfflineBookingId))
                    {
                        CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(OfflineBookingId);
                        if (data != null)
                        {
                            if (data.Status != (int)CLayer.ObjectStatus.InvoiceStatus.Submitted)
                            {
                                BLayer.OfflineBooking.DeleteOfflineBooking(OfflineBookingId);
                                return true;
                            }
                            else
                            {
                                await BookingDeleteRequestAlertMail(OfflineBookingId);
                                return false;
                            }
                        }
                        else
                        {
                            BLayer.OfflineBooking.DeleteOfflineBooking(OfflineBookingId);
                            return true;
                        }
                    }
                    else
                    {
                        await BookingDeleteRequestAlertMail(OfflineBookingId);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }
        public ActionResult LoadList()
        {
            Models.OfflineBookingModel search = new Models.OfflineBookingModel();
            long userId = GetUserId();
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch("", 1, 0, 25, 1, userId);
            search.SearchString = "";
            search.SearchValue = 1;
            search.TotalRows = 0;
            search.OfflineBookingList = users;
            search.SaveStatus = 1;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return PartialView("~/Areas/Admin/Views/OfflineBookingList/_List.cshtml", users);
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<bool> BookingDeleteRequestAlertMail(long OfflineBookingId)
        {
            try
            {
                string message = "";
                Common.Mailer ml = new Common.Mailer();
                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingDeleteRequest")
                                                              + OfflineBookingId.ToString());

                string AccountMail = BLayer.Settings.GetValue(CLayer.Settings.BOOK_DELETE_ALERT_EMAILS);
                if (AccountMail != "")
                {
                    string[] Accemails = AccountMail.Split(',');
                    for (int i = 0; i < Accemails.Length; ++i)
                    {
                        MailMsg.To.Add(Accemails[i]);
                    }
                }

                MailMsg.Subject = "Booking Delete Request";
                MailMsg.Body = message;
                MailMsg.IsBodyHtml = true;
                try
                {
                    await ml.SendMailAsync(MailMsg, Common.Mailer.MailType.Support);
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


        public async Task<bool> ResendemailCGST(long OfflineBookingId, long BookingDetailsId, string key)
        {
            try
            {
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return true;
                }

                if (OfflineBookingId < 1) return false;
                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookingId);
                CLayer.OfflineBooking CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookingId);
                CLayer.OfflineBooking OfflineBookingDetails = BLayer.OfflineBooking.GetBookingDetailsGST(BookingDetailsId);
                if (CustomerDetails == null) return false;
                if (OfflinePropertydata == null) return false;

                CLayer.StayCategory Staycategorydetails = BLayer.StayCategory.Get(Convert.ToInt32(Offlinedata.StayCategoryid));
                CLayer.AccommodationType AccommodationTypedetails = BLayer.AccommodationType.Get(Convert.ToInt32(Offlinedata.Accommodationtypeid));

                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingConfirmationGST") + OfflineBookingId.ToString() + "&BookingDetailsId=" + BookingDetailsId + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //for normal user mail body
                message = await Common.Mailer.MessageFromHtml(link);
                System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();
                //guest mail added
                customerMsg.To.Add(CustomerDetails.CustomerEmail);

                string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                if (BccEmailsforcususer != "")
                {
                    string[] emails = BccEmailsforcususer.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        customerMsg.Bcc.Add(emails[i]);
                    }
                }
                if (OfflineBookingDetails != null)
                {
                    if (OfflineBookingDetails.GuestEmail != null && OfflineBookingDetails.GuestEmail != "")
                    {
                        customerMsg.To.Add(OfflineBookingDetails.GuestEmail);
                    }
                }
                customerMsg.Subject = "Booking Confirmation";
                customerMsg.Body = message;

                customerMsg.IsBodyHtml = true;
                try
                {
                    await ml.SendMailAsync(customerMsg, Common.Mailer.MailType.Reservation);
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
        public async Task<bool> ResendemailSGST(long OfflineBookingId, long BookingDetailsId, string key)
        {
            try
            {
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return true;
                }



                if (OfflineBookingId < 1) return false;
                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookingId);
                CLayer.OfflineBooking CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookingId);

                if (CustomerDetails == null) return false;
                if (OfflinePropertydata == null) return false;

                CLayer.StayCategory Staycategorydetails = BLayer.StayCategory.Get(Convert.ToInt32(Offlinedata.StayCategoryid));
                CLayer.AccommodationType AccommodationTypedetails = BLayer.AccommodationType.Get(Convert.ToInt32(Offlinedata.Accommodationtypeid));

                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                if (OfflineBookingId < 1) return false;
                try
                {

                    if (OfflinePropertydata.PropertyEmail != "")
                    {
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntemationOfflineBookGST") + OfflineBookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK) + "&BookingDetailsId=" + BookingDetailsId);
                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();

                        string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                        if (BccEmailsforsupp != "")
                        {
                            string[] emails = BccEmailsforsupp.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                supplierMsg.Bcc.Add(emails[i]);
                            }
                        }

                        supplierMsg.Subject = "Booking Confirmation";
                        supplierMsg.Body = message;

                        if (OfflinePropertydata.PropertyEmail != "")
                        {
                            supplierMsg.To.Add(OfflinePropertydata.PropertyEmail);
                        }
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
    }
}