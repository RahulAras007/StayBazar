using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using Rotativa;
using System.IO;
using System.Net.Mime;
using BLayer;
using System.Data;

namespace StayBazar.Controllers
{
    public class BookingHistoryController : Controller
    {
        private const int DATERANGE = -30;
        #region Custom Methods

        #endregion
        // GET: /BookingHistory/
        // [Authorize(Roles = "Customer")]
        [Authorize]
        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            string email = User.Identity.GetUserName();
            CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);


            long id = 0;
            long.TryParse(userid, out id);//70 sumod@gmail.com
            Models.BookingHistoryModel search = new Models.BookingHistoryModel();
            List<CLayer.Booking> users = BLayer.BookingHistory.GetHistoryForUser((int)CLayer.ObjectStatus.BookingStatus.Success, id, 90, 0, 25, 1);
            List<CLayer.Booking> Creditbkgs = BLayer.BookingHistory.GetCreditBookForUser((int)CLayer.ObjectStatus.BookingStatus.Success, id, 0, 25);
            List<CLayer.Booking> OtherBkgs = BLayer.BookingHistory.GetOtherBookForUser((int)CLayer.ObjectStatus.BookingStatus.offlineconfirm, id, 0, 25);

            List<CLayer.OfflineBooking> Others = BLayer.OfflineBooking.GetAllForSearch_customer("", 1, 0, 10, 0, id, DateTime.Today.AddDays(DATERANGE), DateTime.Today);

            List<CLayer.Booking> ReadyForBookingList = new List<CLayer.Booking>();
            List<CLayer.Booking> RejectedBookingList = new List<CLayer.Booking>();

            //if (role == CLayer.Role.Roles.Corporate)
            //{
                ReadyForBookingList = BLayer.BookingHistory.GtbookingApprovedOrRejectedBookings((int)CLayer.ObjectStatus.BookingStatus.Approved, id, 90, 0, 25, 1);
                RejectedBookingList = BLayer.BookingHistory.GtbookingCancelledOrFailedOrRejectedBookings((int)CLayer.ObjectStatus.BookingStatus.Decline , id, 90, 0, 25, 1);

            //}
            //else if (role == CLayer.Role.Roles.CorporateUser)
            //{
            //    ReadyForBookingList = BLayer.BookingHistory.GtbookingApprovedOrRejectedBookings((int)CLayer.ObjectStatus.BookingStatus.Approved, id, 90, 0, 25, 1);
            //    RejectedBookingList = BLayer.BookingHistory.GtbookingApprovedOrRejectedBookings((int)CLayer.ObjectStatus.BookingStatus.Decline, id, 90, 0, 25, 1);

            //}



            search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
            ViewBag.SearchValue = 1;
            search.Day = 90;
            search.Type = 1;
            search.TotalRows = 0;
            ViewBag.TotalRows = 0;
            ViewBag.Limit = 10;
            ViewBag.currentPage = 1;
            search.Bookinglist = users;
            search.CreditBookinglist = Creditbkgs;
            search.OtherBookinglist = OtherBkgs;
            search.OtherofflineBookinglist = Others;
            search.ReadyForBookinglist = ReadyForBookingList;
            search.RejectedBookinglist = RejectedBookingList;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
                ViewBag.TotalRows = users[0].TotalRows;
            }
            if (Others.Count > 0)
            {

                ViewBag.TotalRows = Others[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            search.FromDate = DateTime.Today.AddDays(DATERANGE);
            search.ToDate = DateTime.Today;
            ViewBag.Filter = search;

            return View(search);
        }
        [HttpPost]
        public ActionResult Filter(string searchString, int searchItem, int start, int limit, int Status, long Userid, DateTime From, DateTime To)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            if (searchString == null)
                searchString = "";
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch_customer(searchString, searchItem, 0, 10, Status, id, From, To);
            ViewBag.SearchString = searchString;
            ViewBag.SearchValue = searchItem;
            ViewBag.TotalRows = 0;
            ViewBag.Limit = 10;
            ViewBag.currentPage = 1;

            //  ViewBag.Filter = new Models.OfflineBookingModel();
            //Models.OfflineBookingModel forPager = new Models.OfflineBookingModel()
            //{
            //    SearchString = data.SearchString,
            //    SearchValue = data.SearchValue,
            //    TotalRows = 0,
            //    Limit = 25,
            //    currentPage = 1
            //};
            if (users.Count > 0)
            {
                ViewBag.TotalRows = users[0].TotalRows;
            }
            //ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }
        [HttpPost]
        public ActionResult BookingHistory(int day)
        {
            try
            {
                if (day > 365)
                {
                    string userid = User.Identity.GetUserId();
                    long id = 0;
                    long.TryParse(userid, out id);
                    Models.BookingHistoryModel search = new Models.BookingHistoryModel();
                    List<CLayer.Booking> users = BLayer.BookingHistory.GetHistoryForUser((int)CLayer.ObjectStatus.BookingStatus.Success, id, day, 0, 25, 4);
                    search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                    search.TotalRows = 0;
                    search.Day = day;
                    search.Type = 4;
                    search.Bookinglist = users;
                    if (users.Count > 0)
                    {
                        search.TotalRows = users[0].TotalRows;
                    }
                    search.Limit = 25;
                    search.currentPage = 1;
                    ViewBag.Filter = search;
                    return View("~/Views/BookingHistory/_History.cshtml", users);
                }
                else
                {
                    string userid = User.Identity.GetUserId();
                    long id = 0;
                    long.TryParse(userid, out id);
                    Models.BookingHistoryModel search = new Models.BookingHistoryModel();
                    List<CLayer.Booking> users = BLayer.BookingHistory.GetHistoryForUser((int)CLayer.ObjectStatus.BookingStatus.Success, id, day, 0, 25, 2);
                    search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                    search.TotalRows = 0;
                    search.Day = day;
                    search.Type = 25;
                    search.Bookinglist = users;
                    if (users.Count > 0)
                    {
                        search.TotalRows = users[0].TotalRows;
                    }
                    search.Limit = 25;
                    search.currentPage = 1;
                    ViewBag.Filter = search;
                    return View("~/Views/BookingHistory/_History.cshtml", users);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View();
        }
        [HttpPost]
        public ActionResult Pagerforothers(string searchString, int searchItem, int start, int limit, int Status, long Userid, DateTime From, DateTime To)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch_customer(searchString, searchItem, (start - 1) * 10, 10, Status, id, From, To);
            ViewBag.SearchString = searchString;
            ViewBag.SearchValue = searchItem;
            ViewBag.TotalRows = 0;
            ViewBag.Limit = 10;
            ViewBag.currentPage = start;
            // List<CLayer.Booking> users = BLayer.BookingHistory.GetHistoryForUser((int)CLayer.ObjectStatus.BookingStatus.Success, id, data.Day, (data.currentPage - 1) * data.Limit, data.Limit, data.Type);
            //ViewBag.Filter = new Models.BookingHistoryModel();
            //Models.BookingHistoryModel forPager = new Models.BookingHistoryModel()
            //{
            //    Status = data.Status,
            //    TotalRows = 0,
            //    Limit = 25,
            //    currentPage = data.currentPage
            //};
            if (users.Count > 0)
            {
                ViewBag.TotalRows = users[0].TotalRows;
            }
            //ViewBag.Filter = forPager;
            //if (data.Type == 1)
            //{
            //    return PartialView("_Resent", users);
            //}
            return PartialView("~/Views/BookingHistory/_List.cshtml", users);
        }
        [HttpPost]
        public ActionResult Pager(Models.BookingHistoryModel data)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            List<CLayer.Booking> users = BLayer.BookingHistory.GetHistoryForUser((int)CLayer.ObjectStatus.BookingStatus.Success, id, data.Day, (data.currentPage - 1) * data.Limit, data.Limit, data.Type);
            ViewBag.Filter = new Models.BookingHistoryModel();
            Models.BookingHistoryModel forPager = new Models.BookingHistoryModel()
            {
                Status = data.Status,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            if (data.Type == 1)
            {
                return PartialView("_Resent", users);
            }
            return PartialView("~/Views/BookingHistory/_History.cshtml", users);
        }


        [HttpPost]
        public ActionResult PagerforCreditbook(Models.BookingHistoryModel data)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            List<CLayer.Booking> Creditbkgs = BLayer.BookingHistory.GetCreditBookForUser((int)CLayer.ObjectStatus.BookingStatus.Success, id, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.BookingHistoryModel();
            Models.BookingHistoryModel forPager = new Models.BookingHistoryModel()
            {
                Status = data.Status,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (Creditbkgs.Count > 0)
            {
                forPager.TotalRows = Creditbkgs[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("~/Views/BookingHistory/CorporateCreditBookings.cshtml", Creditbkgs);
        }

        [HttpPost]
        public ActionResult FillBookedByAddressSearch(long BookingId)
        {
            List<CLayer.Address> users = BLayer.BookingHistory.BookedByAddressSearch(BookingId);
            return PartialView("_AddressBookedList", users);
        }
        [HttpPost]
        public ActionResult Booking_GetBookedFor(long BookingId)
        {

            List<CLayer.Address> users = BLayer.BookingHistory.GetBookedForUser(BookingId);
            return PartialView("_AddressForBooking", users);
        }
        public ActionResult ViewBill(long OfflineBookingId)
        {
            Areas.Admin.Models.OfflineBookingModel data = new Areas.Admin.Models.OfflineBookingModel();

            try
            {
                data.OfflineBookingId = OfflineBookingId;
                data.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);
                ViewBag.viewonly = "yes";
                //return View("~/Areas/Admin/Views/OfflineBooking/BookingData.cshtml", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            //return new ViewAsPdf("~/Views/BookingHistory/OfflineBookingCofirmation.cshtml", data)
            //{

            //    PageMargins = new Rotativa.Options.Margins(0, 0, 0, 0),
            //    PageOrientation = Rotativa.Options.Orientation.Portrait
            //};

            var PDFResult = new ViewAsPdf("~/Views/BookingHistory/OfflineBookingCofirmation.cshtml", data)
            {
                FileName = "Report.PDF",
                CustomSwitches =
                       "--footer-center \"  DOS: " +
                       DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                       " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
            };

            return PDFResult;
        }


        public ActionResult InvoicePDF(long obId)
        {
            Areas.Admin.Models.Invoice inv = new Areas.Admin.Models.Invoice();
            try
            {
                CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
                if (data == null)
                {
                    data = new CLayer.Invoice();
                    data.OfflineBookingId = obId;
                    // data.InvoiceDate = DateTime.Today;
                    data.DueDate = DateTime.Today.AddDays(10);
                    data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
                    BLayer.Invoice.Save(data);
                }
                inv.InvoiceId = data.InvoiceId;
                inv.OfflineBookingId = data.OfflineBookingId;
                inv.IsMailed = (data.MailedDate <= DateTime.Today);
                inv.HtmlSection1 = data.HtmlSection1;
                inv.HtmlSection2 = data.HtmlSection2;
                inv.HtmlSection3 = data.HtmlSection3;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("Mail", inv);
        }


        [AllowAnonymous]
        public ActionResult MailContent(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);

            if (data == null)
            {
                data = new CLayer.Invoice();
                data.OfflineBookingId = obId;
                ///  data.InvoiceDate = DateTime.Today;
                data.DueDate = DateTime.Today.AddDays(10);
                data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
                BLayer.Invoice.Save(data);
                //data = BLayer.Invoice.GetInvoiceByOfflineBooking(offId);
                //data = await MessageFromHtml(offId);
            }
            Areas.Admin.Models.Invoice inv = new Areas.Admin.Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);
            inv.HtmlSection1 = data.HtmlSection1;
            inv.HtmlSection2 = data.HtmlSection2;
            inv.HtmlSection3 = data.HtmlSection3;

            CLayer.OfflineBooking dd = BLayer.OfflineBooking.GetAllCustomerDetails(obId);
            if (dd != null)
            {
                inv.CustomerName = dd.CustomerName;
            }

            return View("Mail", inv);
        }
        [AllowAnonymous]
        private async Task<string> GetMailBody(long obid)
        {
            string url = ConfigurationManager.AppSettings.Get("InvoiceMailLinkForCustomer") + obid.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            return mainHtml;
        }

        private async Task<string> GetMailBodyForNotSubmittedandGeneratedList()
        {
            string url = ConfigurationManager.AppSettings.Get("InvoiceMailLinkForNotSubGen");
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            return mainHtml;
        }
        [AllowAnonymous]
        public ActionResult MailContentForInvoiceNotSubted()
        {
            List<CLayer.Invoice> List = BLayer.OfflineBooking.GetDetailsNotSubmittedandGenerated();
            return View("InvoicePendingList", List);
        }

        [AllowAnonymous]
        public async Task<string> InvoicePendingEmail()
        {
            WebClient wc = new WebClient();
            try
            {
                Models.OfflinePaymentModel data = new Models.OfflinePaymentModel();
                List<CLayer.Invoice> dt = BLayer.OfflineBooking.GetDetailsNotSubmittedandGenerated();
                MailMessage mm = new MailMessage();

                string Toemailids = ConfigurationManager.AppSettings.Get("InvoicePendingListSendToMailids");
                if (Toemailids != "")
                {
                    string[] emails = Toemailids.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mm.To.Add(emails[i]);
                    }
                }

                string Ccemailids = ConfigurationManager.AppSettings.Get("InvoicePendingListSendCcMailids");
                if (Ccemailids != "")
                {
                    string[] emails = Ccemailids.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mm.CC.Add(emails[i]);
                    }
                }


                //string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                //if (BccEmailsforcususer != "")
                //{
                //    string[] emails = BccEmailsforcususer.Split(',');
                //    for (int i = 0; i < emails.Length; ++i)
                //    {
                //        mm.Bcc.Add(emails[i]);
                //    }
                //}

                //mm.To.Add(BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL));



                mm.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                Common.Mailer mlr = new Common.Mailer();
                mm.IsBodyHtml = true;
                mm.Subject = "Invoice Pending";
                string result = await GetMailBodyForNotSubmittedandGeneratedList();
                mm.Body = result;

                if (dt != null)
                {
                    if (dt.Count > 0)
                    {
                        await mlr.SendMailAsyncWithoutFields(mm);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
        }

        [AllowAnonymous]
        public async Task<string> CustomerInvoiceEmail()
        {
            WebClient wc = new WebClient();
            try
            {
                Models.OfflinePaymentModel data = new Models.OfflinePaymentModel();
                List<CLayer.OfflineBooking> dt = BLayer.OfflineBooking.GetBookingDetailsAfterSubmitForCustomer();
                long OfflineBookingId = 0;
                MailMessage mm = new MailMessage();

                string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                if (BccEmailsforcususer != "")
                {
                    string[] emails = BccEmailsforcususer.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mm.Bcc.Add(emails[i]);
                    }
                }


                mm.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                Common.Mailer mlr = new Common.Mailer();
                mm.IsBodyHtml = true;
                mm.Subject = "Invoice";
                foreach (CLayer.OfflineBooking offbook in dt)
                {
                    OfflineBookingId = offbook.OfflineBookingId;
                    string result = await GetMailBody(OfflineBookingId);
                    mm.To.Clear();
                    mm.To.Add(offbook.CustomerEmail);
                    mm.Body = result;
                    await mlr.SendMailAsyncWithoutFields(mm);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
        }

        public ActionResult CustomerInvoicePDF(long obId)
        {
            Areas.Admin.Models.Invoice inv = new Areas.Admin.Models.Invoice();
            try
            {
                CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
                if (data == null)
                {
                    data = new CLayer.Invoice();
                    data.OfflineBookingId = obId;
                    // data.InvoiceDate = DateTime.Today;
                    data.DueDate = DateTime.Today.AddDays(10);
                    data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
                    BLayer.Invoice.Save(data);
                }
                inv.InvoiceId = data.InvoiceId;
                inv.OfflineBookingId = data.OfflineBookingId;
                inv.IsMailed = (data.MailedDate <= DateTime.Today);
                inv.HtmlSection1 = data.HtmlSection1;
                inv.HtmlSection2 = data.HtmlSection2;
                inv.HtmlSection3 = data.HtmlSection3;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("CustomerInvoicePDF", inv);
        }

        [AllowAnonymous]
        public async Task<bool> SupplierInvoiceToSupplier()
        {
            List<CLayer.OfflineBooking> BookingList = BLayer.OfflineBooking.GetOfflinebookingsAtCheckInOutNow();
            foreach (CLayer.OfflineBooking dt in BookingList)
            {
                string message = "";
                string Filename = "";
                Common.Mailer ml = new Common.Mailer();
                try
                {
                    if (dt.PropertyEmail != "" || dt.SupplierEmail != "")
                    {
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierInvoiceOfflineBookGST") + dt.OfflineBookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();

                        supplierMsg.Subject = "Supplier Invoice";
                        supplierMsg.Body = message;

                        if (dt.PropertyEmail != null)
                        {
                            if (dt.PropertyEmail != "")
                            {
                                supplierMsg.CC.Add(dt.PropertyEmail);
                            }
                        }

                        if (dt.SupplierEmail != null)
                        {
                            if (dt.SupplierEmail != "")
                            {
                                supplierMsg.To.Add(dt.SupplierEmail);
                            }
                        }

                        string AccountMail = BLayer.Settings.GetValue(CLayer.Settings.SUPPLIER_INVOICE_REQUEST_MAILS);
                        if (AccountMail != "")
                        {
                            string[] Accemails = AccountMail.Split(',');
                            for (int i = 0; i < Accemails.Length; ++i)
                            {
                                supplierMsg.CC.Add(Accemails[i]);
                            }
                        }


                        supplierMsg.IsBodyHtml = true;

                        //add atachment 
                        try
                        {
                            Areas.Admin.Models.OfflineBookingModel data = new Areas.Admin.Models.OfflineBookingModel();
                            data.OfflineBookingId = dt.OfflineBookingId;
                            var PDFResult = new ViewAsPdf("~/Areas/Admin/Views/OfflineBookingGST/SupplierInvoice.cshtml", data)
                            {
                                CustomSwitches =
                                    "--footer-center \"  DOS: " +
                                    DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
                            };
                            string newdirectory = "Files\\Temp\\SupplierInvoice\\" + dt.OfflineBookingId;
                            if (!Directory.Exists(Server.MapPath("~") + "\\" + newdirectory))
                            {
                                Directory.CreateDirectory(Server.MapPath("~") + "\\" + newdirectory);
                            }
                            Filename = Server.MapPath("~") + "\\" + newdirectory + "\\" + "SupplierInvoice_" + dt.ConfirmationNumber + ".pdf";
                            var byteArray = PDFResult.BuildPdf(ControllerContext);
                            var fileStream = new FileStream(Filename, FileMode.Create, FileAccess.Write);
                            fileStream.Write(byteArray, 0, byteArray.Length);
                            fileStream.Close();
                            fileStream.Dispose();
                            Attachment attacht = new Attachment(Filename, MediaTypeNames.Application.Octet);
                            supplierMsg.Attachments.Add(attacht);
                        }
                        catch (Exception ex)
                        {
                            Common.LogHandler.HandleError(ex);
                        }



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
            return true;
        }




        [AllowAnonymous]
        public async Task<string> SupplierPaymentEmail()
        {
            WebClient wc = new WebClient();
            try
            {
                Models.OfflinePaymentModel data = new Models.OfflinePaymentModel();
                List<CLayer.OfflineBooking> dt = BLayer.OfflineBooking.GetAllOfflineDetails();
                long OfflineBookingId = 0;
                MailMessage mm = new MailMessage();

                string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.SUPPLIERPAYMENTSCHEDULEEMAIL);
                if (BccEmailsforcususer != "")
                {
                    mm.To.Clear();
                    string[] emails = BccEmailsforcususer.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mm.To.Add(emails[i]);
                    }
                }


                mm.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                Common.Mailer mlr = new Common.Mailer();
                mm.IsBodyHtml = true;


                foreach (CLayer.OfflineBooking offbook in dt)
                {
                    CLayer.OfflineBooking offedit = BLayer.OfflineBooking.GetAllDetailsById(offbook.OfflineBookingId);

                    if (offedit.IsSupplierPaymentMailSend)
                    {
                        mm.Subject = "SUPPLIER PAYMENT SCHEDULE EMAIL [Revised]";
                    }
                    else
                    {
                        mm.Subject = "SUPPLIER PAYMENT SCHEDULE EMAIL";
                    }


                    if (offbook.SupplierPaymentScheduleList.Count > 0)
                    {
                        int exit = 0;
                        foreach (var schedule in offbook.SupplierPaymentScheduleList)
                        {
                            if (schedule.SupplierPaymentMode == (int)CLayer.OfflineBooking.SupplierPaymentModelist.OnInstalments)
                            {

                                foreach (var date in offbook.SupplierPaymentScheduleList)
                                {
                                    if (date.SupplierPaymentModeDate.ToShortDateString() == System.DateTime.Now.ToShortDateString() && exit == 0)
                                    {
                                        OfflineBookingId = offbook.OfflineBookingId;
                                        string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflinebookingSupplierPaymentSchedule") + OfflineBookingId.ToString();
                                        //for normal user mail body
                                        string result = await Common.Mailer.MessageFromHtml(link);


                                        mm.Body = result;
                                        await mlr.SendMailAsyncWithoutFields(mm);
                                        //Update supplier payment mail send data
                                        BLayer.OfflineBooking.UpdateSupplierPaymentmailsendData(OfflineBookingId);
                                        exit = 1;
                                        break;

                                    }
                                }

                            }
                            if (schedule.SupplierPaymentMode == (int)CLayer.OfflineBooking.SupplierPaymentModelist.Credit)
                            {
                                DateTime dtDate = offbook.CheckOut.AddDays(schedule.SupplierCreditDays);
                                if (dtDate.ToShortDateString() == System.DateTime.Now.ToShortDateString())
                                {
                                    OfflineBookingId = offbook.OfflineBookingId;
                                    string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflinebookingSupplierPaymentSchedule") + OfflineBookingId.ToString();
                                    //for normal user mail body
                                    string result = await Common.Mailer.MessageFromHtml(link);


                                    mm.Body = result;
                                    await mlr.SendMailAsyncWithoutFields(mm);
                                    //Update supplier payment mail send data
                                    BLayer.OfflineBooking.UpdateSupplierPaymentmailsendData(OfflineBookingId);

                                }

                            }
                            if (schedule.SupplierPaymentMode == (int)CLayer.OfflineBooking.SupplierPaymentModelist.FullAmountBeforeCheckin)
                            {
                                if (offbook.CheckIn.ToShortDateString() == System.DateTime.Now.ToShortDateString())
                                {
                                    OfflineBookingId = offbook.OfflineBookingId;
                                    string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflinebookingSupplierPaymentSchedule") + OfflineBookingId.ToString();
                                    //for normal user mail body
                                    string result = await Common.Mailer.MessageFromHtml(link);


                                    mm.Body = result;
                                    await mlr.SendMailAsyncWithoutFields(mm);
                                    //Update supplier payment mail send data
                                    BLayer.OfflineBooking.UpdateSupplierPaymentmailsendData(OfflineBookingId);
                                }

                            }
                            if (schedule.SupplierPaymentMode == (int)CLayer.OfflineBooking.SupplierPaymentModelist.FullAmountBeforeCheckout)
                            {
                                if (offbook.CheckOut.ToShortDateString() == System.DateTime.Now.ToShortDateString())
                                {
                                    OfflineBookingId = offbook.OfflineBookingId;
                                    string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflinebookingSupplierPaymentSchedule") + OfflineBookingId.ToString();
                                    //for normal user mail body
                                    string result = await Common.Mailer.MessageFromHtml(link);


                                    mm.Body = result;
                                    await mlr.SendMailAsyncWithoutFields(mm);
                                    //Update supplier payment mail send data
                                    BLayer.OfflineBooking.UpdateSupplierPaymentmailsendData(OfflineBookingId);
                                }

                            }
                        }
                    }




                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
        }


        [AllowAnonymous]
        public async Task<bool> SmsService()
        {
            bool result = false;
            OfflineBooking obj = new BLayer.OfflineBooking();
            List<CLayer.OfflineBooking> lstOfflineBooking = new List<CLayer.OfflineBooking>();
            DataTable dtdates = obj.GetCheckInDates(0);
            if (dtdates.Rows.Count >= 0)
            {
                foreach (DataRow row in dtdates.Rows)
                {
                    if (row["phonenumber"].ToString() != null && row["phonenumber"].ToString() != "")
                    {
                        var propertyname = System.Web.HttpContext.Current.Server.UrlEncode(row["PropertyName"].ToString());
                        var propertyCityname = System.Web.HttpContext.Current.Server.UrlEncode(row["PropertyCityname"].ToString());
                        var checkindate = System.Web.HttpContext.Current.Server.UrlEncode(row["checkindate"].ToString());
                        var OfflinebookingId = row["BookingId"].ToString();
                        string BookingCNo = "";
                        if (Convert.ToInt32(OfflinebookingId) > 0)
                        {
                            CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(Convert.ToInt32(OfflinebookingId));
                            if (Offlinedata != null)
                            {
                                BookingCNo = Offlinedata.ConfirmationNumber;
                            }
                        }

                        try
                        {
                            string message = "Dear Guest,Hope you are ready for check-in at  " + propertyname + " at " + propertyCityname + " on " + checkindate + " as per BookingId   " + BookingCNo + "  .Wish you a pleasant stay!";
                            result = await StayBazar.Common.SMSGateway.Send(message, row["phonenumber"].ToString());
                        }
                        catch (Exception ex)
                        {
                            Common.LogHandler.HandleError(ex);
                        }
                    }
                    if (row["Guestemail"].ToString() != null && row["Guestemail"].ToString() != "")
                    {
                        await OfflineBookingConfirmGST(Convert.ToInt32(row["BookingId"]), Convert.ToInt32(row["BookingDetailsId"]), row["Guestemail"].ToString());
                    }
                }
            }
            return result;
        }


        public async Task<bool> OfflineBookingConfirmGST(long OfflineBookId, long BookingDetailsId, string GuestEmail)
        {
            try
            {
                if (OfflineBookId < 1) return false;
                if (BookingDetailsId < 1) return false;
                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookId);

                CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookId);

                CLayer.OfflineBooking CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookId);

                if (CustomerDetails == null) return false;
                if (OfflinePropertydata == null) return false;

                CLayer.StayCategory Staycategorydetails = BLayer.StayCategory.Get(Convert.ToInt32(Offlinedata.StayCategoryid));
                CLayer.AccommodationType AccommodationTypedetails = BLayer.AccommodationType.Get(Convert.ToInt32(Offlinedata.Accommodationtypeid));

                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                try
                {
                    #region Repeat portion

                    //List<CLayer.OfflineBooking> BListGST = BLayer.OfflineBooking.GetMultipleBookingDetailsGST(OfflineBookId);
                    //foreach (CLayer.OfflineBooking dt in BListGST)
                    //{

                    System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();
                    //customerMsg.To.Add(CustomerDetails.CustomerEmail);
                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcususer.Trim() != "")
                    {
                        string[] emails = Common.Utils.SplitEmails(BccEmailsforcususer);
                        //BccEmailsforcususer.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            customerMsg.CC.Add(emails[i]);
                        }
                    }


                    customerMsg.Subject = "CHECK-IN REMINDER";
                    customerMsg.IsBodyHtml = true;

                    if (GuestEmail != null)
                    {
                        if (GuestEmail != null && GuestEmail != "")
                        {
                            string[] emails = Common.Utils.SplitEmails(GuestEmail);
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                customerMsg.To.Add(emails[i]);
                            }
                        }
                    }

                    string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingConfirmationBeforeCheckin") + OfflineBookId.ToString() + "&BookingDetailsId=" + BookingDetailsId + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                    message = await Common.Mailer.MessageFromHtml(link);
                    customerMsg.Body = message;



                    //Select Distinct Mail address in To mails

                    System.Net.Mail.MailAddress[] DistinctTo = customerMsg.To.Distinct().ToArray();
                    customerMsg.To.Clear();
                    for (int i = 0; i < DistinctTo.Length; ++i)
                    {
                        customerMsg.To.Add(DistinctTo[i]);
                    }

                    //Select Distinct Mail address in CC mails

                    System.Net.Mail.MailAddress[] DistinctCC = customerMsg.CC.Distinct().Except(DistinctTo).ToArray();
                    customerMsg.CC.Clear();
                    for (int i = 0; i < DistinctCC.Length; ++i)
                    {
                        customerMsg.CC.Add(DistinctCC[i]);
                    }



                    try
                    {
                        await ml.SendMailAsync(customerMsg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                    //}
                    #endregion


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
