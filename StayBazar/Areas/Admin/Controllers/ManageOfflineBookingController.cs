using Rotativa;
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
using System.IO;
using System.Net.Mime;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission(AllowAllRoles = true)]
    public class ManageOfflineBookingController : Controller
    {
        // GET: /Admin/Transactions/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.OfflineBookingModel search = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch_Manage("", 1, 0, 25, 2, 0);
            search.SearchString = "";
            search.SearchValue = 1;
            search.TotalRows = 0;
            search.OfflineBookingList = users;
            search.SaveStatus = 2;
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
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch_Manage(data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit, data.SaveStatus, data.CreatedUser);
            ViewBag.Filter = new Models.OfflineBookingModel();
            Models.OfflineBookingModel forPager = new Models.OfflineBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage,
                SaveStatus = data.SaveStatus
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
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch_Manage(data.SearchString, data.SearchValue, 0, 25, data.SaveStatus, data.CreatedUser);
            ViewBag.Filter = new Models.OfflineBookingModel();
            Models.OfflineBookingModel forPager = new Models.OfflineBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1,
                SaveStatus = data.SaveStatus
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

                if (Offlinedata != null)
                {
                    if (Offlinedata.GuestEmail != null && Offlinedata.GuestEmail != "")
                    {
                        customerMsg.To.Add(Offlinedata.GuestEmail);
                    }
                }


                string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                if (BccEmailsforcususer.Trim() != "")
                {
                    string[] emails = BccEmailsforcususer.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        customerMsg.CC.Add(emails[i]);
                    }
                }


                if (Offlinedata != null)
                {
                    if (Offlinedata.MailContent != "")
                    {
                        string BccBranchEmails = Offlinedata.MailContent.Trim();
                        if (BccBranchEmails != "")
                        {
                            string[] emails = BccBranchEmails.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                customerMsg.CC.Add(emails[i]);
                            }
                        }
                    }
                }


                //send to ops mail
                string SalesPersonOPSMails = "";
                if (OfflineBookingId > 0)
                {
                    CLayer.OfflineBooking OffdataStaff = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                    if (OffdataStaff != null)
                    {
                        if (OffdataStaff.SalesPersonId != null)
                        {
                            if (OffdataStaff.SalesPersonId > 0)
                            {
                                CLayer.User usrstaff = BLayer.User.Get(OffdataStaff.SalesPersonId);
                                if (usrstaff != null)
                                {
                                    if (usrstaff.OPSEmail != null && usrstaff.OPSEmail != "")
                                    {
                                        SalesPersonOPSMails = usrstaff.OPSEmail;
                                    }
                                }
                            }
                        }
                    }
                }
                if (SalesPersonOPSMails != "")
                {
                    string CcOPSEmails = SalesPersonOPSMails.Trim();
                    if (CcOPSEmails != "")
                    {
                        string[] emails = CcOPSEmails.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            customerMsg.CC.Add(emails[i]);
                        }
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
                        if (BccEmailsforsupp.Trim() != "")
                        {
                            string[] emails = BccEmailsforsupp.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                supplierMsg.Bcc.Add(emails[i]);
                            }
                        }

                        if (Offlinedata != null)
                        {
                            if (Offlinedata.MailContent != "")
                            {
                                string BccBranchEmails = Offlinedata.MailContent.Trim();
                                if (BccBranchEmails != "")
                                {
                                    string[] emails = BccBranchEmails.Split(',');
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        supplierMsg.CC.Add(emails[i]);
                                    }
                                }
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


        [Common.AdminRolePermission(AllowAllRoles = true)]
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


        public string GetMessageAlertForBookingDelete(long OfflineBookingId)
        {
            string msgerror = "";
            try
            {
                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetOfflineboikingdetailsforBookDeleteRequest(OfflineBookingId);
                decimal SupplierPayment = BLayer.SupplierPayment.GetSupplierPaymentBybookingid(OfflineBookingId);
                msgerror = "Deletion could not be allowed since invoice no " + Offlinedata.InvoiceNumber + " has already raised / supplier has been paid Rs." + SupplierPayment + " against this booking. Please contact admin";
                return msgerror;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return msgerror;
            }
        }

        public ActionResult LoadList()
        {
            Models.OfflineBookingModel search = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForSearch_Manage("", 1, 0, 25, 2, 0);
            search.SearchString = "";
            search.SearchValue = 1;
            search.TotalRows = 0;
            search.OfflineBookingList = users;
            search.SaveStatus = 2;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return PartialView("~/Areas/Admin/Views/ManageOfflineBooking/_List.cshtml", users);
        }

        public async Task<ActionResult> SetApproved(long OfflineBookingId)
        {
            List<CLayer.OfflineBooking> users = new List<CLayer.OfflineBooking>();
            Models.OfflineBookingModel search = new Models.OfflineBookingModel();
            try
            {
                BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Approved);
                if (OfflineBookingId > 0)
                {
                    long IsGST = BLayer.OfflineBooking.GetOfflinebookingIsGST(OfflineBookingId);
                    if (IsGST == (int)CLayer.OfflineBooking.OfflineBookingsType.GST)
                    {
                        await OfflineBookingConfirmGST(OfflineBookingId);
                    }
                    else
                    {
                        await OfflineBookingConfirm(OfflineBookingId);
                    }

                    BLayer.OfflineBooking objOfflineBooking = new BLayer.OfflineBooking();
                    var time = BLayer.Settings.GetValue(CLayer.Settings.SMS_REMINDER_TIME);
                    var data = objOfflineBooking.GetPhoneNumber(OfflineBookingId, time);
                    if (data.Rows.Count>0)
                    {
                        var propertyname = System.Web.HttpContext.Current.Server.UrlEncode(data.Rows[0]["PropertyName"].ToString());
                        var propertyCityname = System.Web.HttpContext.Current.Server.UrlEncode(data.Rows[0]["PropertyCityname"].ToString());
                        var checkindate = System.Web.HttpContext.Current.Server.UrlEncode(data.Rows[0]["checkindate"].ToString());
                        var OfflinebookingId = data.Rows[0]["BookingId"].ToString();
                        string BookingCNo = "";
                        if (Convert.ToInt32(OfflinebookingId) > 0)
                        {
                            CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(Convert.ToInt32(OfflinebookingId));
                            if (Offlinedata != null)
                            {
                                BookingCNo = Offlinedata.ConfirmationNumber;
                            }
                        }


                        var phone = data.Rows[0]["phonenumber"].ToString();
                        if (phone != null && phone!="")
                        {
                            try
                            {
                                string message = "Dear Guest,Hope you are ready for check-in at  " + propertyname + " at " + propertyCityname + " on " + checkindate + " as per BookingId   " + BookingCNo + "  .Wish you a pleasant stay!";
                                var result = await StayBazar.Common.SMSGateway.Send(message, phone);
                            }
                            catch (Exception ex)
                            {
                                Common.LogHandler.HandleError(ex);
                            }
                        }
                        if (data.Rows[0]["Guestemail"].ToString() != null && data.Rows[0]["Guestemail"].ToString() != "")
                        {
                            await OfflineBookingConfirmCheckinReminder(OfflineBookingId,Convert.ToInt32(data.Rows[0]["BookingDetailsId"]), data.Rows[0]["Guestemail"].ToString());
                        }
                    }
                   
                }
                users = BLayer.OfflineBooking.GetAllForSearch_Manage("", 1, 0, 25, 2, 0);


                await SupplierPaymentSheduleEmail(OfflineBookingId);

                search.SearchString = "";
                search.SearchValue = 2;
                search.TotalRows = 0;
                search.OfflineBookingList = users;
                search.Limit = 25;
                search.currentPage = 1;
                search.SaveStatus = 2;
                ViewBag.Filter = search;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("_List", users);
        }

        public async Task<bool> Sendallmail(long OfflineBookingId)
        {
            try
            {
                if (OfflineBookingId > 0)
                {
                    await OfflineBookingConfirmGST(OfflineBookingId);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }
        //Send email for Offline Booking
        public async Task<bool> OfflineBookingConfirm(long OfflineBookId)
        {
            try
            {
                if (OfflineBookId < 1) return false;

                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookId);

                CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookId);

                //CLayer.Address CustomerDetails = BLayer.Address.GetAddressOnUserId(Offlinedata.CustomerId);

                CLayer.OfflineBooking CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookId);

                //  CLayer.Address SupplierDetails = BLayer.Address.GetAddressOnUserId(Offlinedata.SupplierId);
                //  CLayer.B2B SupplierData = BLayer.B2B.Get(Offlinedata.SupplierId);
                //  CLayer.Property PropertyData = BLayer.Property.Get(Offlinedata.PropertyId);

                if (CustomerDetails == null) return false;
                if (OfflinePropertydata == null) return false;

                CLayer.StayCategory Staycategorydetails = BLayer.StayCategory.Get(Convert.ToInt32(Offlinedata.StayCategoryid));
                CLayer.AccommodationType AccommodationTypedetails = BLayer.AccommodationType.Get(Convert.ToInt32(Offlinedata.Accommodationtypeid));

                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                try
                {

                    if (Offlinedata.sendmailtocustomer == false)
                    {
                        //send email and sms to customer start

                        string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingConfirmation") + OfflineBookId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                        //for normal user mail body
                        message = await Common.Mailer.MessageFromHtml(link);
                        System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();
                        //guest mail added
                        customerMsg.To.Add(CustomerDetails.CustomerEmail);

                        if (Offlinedata != null)
                        {
                            if (Offlinedata.GuestEmail != null && Offlinedata.GuestEmail != "")
                            {
                                customerMsg.To.Add(Offlinedata.GuestEmail);
                            }
                        }


                        string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                        if (BccEmailsforcususer.Trim() != "")
                        {
                            string[] emails = BccEmailsforcususer.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                customerMsg.CC.Add(emails[i]);
                            }
                        }

                        if (Offlinedata != null)
                        {
                            if (Offlinedata.MailContent != "")
                            {
                                string BccBranchEmails = Offlinedata.MailContent.Trim();
                                if (BccBranchEmails != "")
                                {
                                    string[] emails = BccBranchEmails.Split(',');
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        customerMsg.CC.Add(emails[i]);
                                    }
                                }
                            }
                        }


                        //send to ops mail
                        string SalesPersonOPSMails = "";
                        if (OfflineBookId > 0)
                        {
                            CLayer.OfflineBooking OffdataStaff = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookId);
                            if (OffdataStaff != null)
                            {
                                if (OffdataStaff.SalesPersonId != null)
                                {
                                    if (OffdataStaff.SalesPersonId > 0)
                                    {
                                        CLayer.User usrstaff = BLayer.User.Get(OffdataStaff.SalesPersonId);
                                        if (usrstaff != null)
                                        {
                                            if (usrstaff.OPSEmail != null && usrstaff.OPSEmail != "")
                                            {
                                                SalesPersonOPSMails = usrstaff.OPSEmail.Trim();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (SalesPersonOPSMails != "")
                        {
                            string CcOPSEmails = SalesPersonOPSMails.Trim();
                            if (CcOPSEmails != "")
                            {
                                string[] emails = CcOPSEmails.Split(',');
                                for (int i = 0; i < emails.Length; ++i)
                                {
                                    customerMsg.CC.Add(emails[i]);
                                }
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

                    if (Offlinedata.sendmailtosupplier == false)
                    {

                        //send email and sms to supplier start
                        if (OfflineBookId < 1) return false;
                        try
                        {

                            if (OfflinePropertydata.PropertyEmail != "")
                            {
                                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntemationOfflineBook") + OfflineBookId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                                System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();

                                string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                                if (BccEmailsforsupp.Trim() != "")
                                {
                                    string[] emails = BccEmailsforsupp.Split(',');
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        supplierMsg.Bcc.Add(emails[i]);
                                    }
                                }


                                if (Offlinedata != null)
                                {
                                    if (Offlinedata.MailContent != "")
                                    {
                                        string BccBranchEmails = Offlinedata.MailContent.Trim();
                                        if (BccBranchEmails != "")
                                        {
                                            string[] emails = BccBranchEmails.Split(',');
                                            for (int i = 0; i < emails.Length; ++i)
                                            {
                                                supplierMsg.CC.Add(emails[i]);
                                            }
                                        }
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

        public async Task<bool> OfflineBookingConfirmGST(long OfflineBookId)
        {
            try
            {
                if (OfflineBookId < 1) return false;

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
                    if (Offlinedata.sendmailtocustomer == false)
                    {
                        #region Repeat portion

                        List<CLayer.OfflineBooking> BListGST = BLayer.OfflineBooking.GetMultipleBookingDetailsGST(OfflineBookId);
                        foreach (CLayer.OfflineBooking dt in BListGST)
                        {

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
                            if (Offlinedata != null)
                            {
                                if (Offlinedata.MailContent != "")
                                {
                                    string BccBranchEmails = Offlinedata.MailContent.Trim();
                                    if (BccBranchEmails != "")
                                    {
                                        string[] emails = Common.Utils.SplitEmails(BccBranchEmails);

                                        for (int i = 0; i < emails.Length; ++i)
                                        {
                                            customerMsg.CC.Add(emails[i]);
                                        }
                                    }
                                }
                            }
                            string SalesPersonOPSMails = "";
                            if (OfflineBookId > 0)
                            {
                                CLayer.OfflineBooking OffdataStaff = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookId);
                                if (OffdataStaff != null)
                                {
                                    if (OffdataStaff.SalesPersonId != null)
                                    {
                                        if (OffdataStaff.SalesPersonId > 0)
                                        {
                                            CLayer.User usrstaff = BLayer.User.Get(OffdataStaff.SalesPersonId);
                                            if (usrstaff != null)
                                            {
                                                if (usrstaff.OPSEmail != null && usrstaff.OPSEmail != "")
                                                {
                                                    SalesPersonOPSMails = usrstaff.OPSEmail.Trim();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (SalesPersonOPSMails != "")
                            {
                                string CcOPSEmails = SalesPersonOPSMails.Trim();
                                if (CcOPSEmails != "")
                                {
                                    string[] emails = Common.Utils.SplitEmails(CcOPSEmails);
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        customerMsg.CC.Add(emails[i]);
                                    }
                                }
                            }
                            customerMsg.Subject = "Booking Confirmation";
                            customerMsg.IsBodyHtml = true;

                            if (dt != null)
                            {
                                if (dt.GuestEmail != null && dt.GuestEmail != "")
                                {
                                    string[] emails = Common.Utils.SplitEmails(dt.GuestEmail);
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        customerMsg.To.Add(emails[i]);
                                    }
                                }
                            }
                            if (CustomerDetails.CustomerEmail != null)
                            {
                                customerMsg.CC.Add(CustomerDetails.CustomerEmail);
                            }
                            string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingConfirmationGST") + OfflineBookId.ToString() + "&BookingDetailsId=" + dt.BookingDetailsId + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
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

                        }
                        #endregion
                    }

                    if (Offlinedata.sendmailtosupplier == false)
                    {
                        if (OfflineBookId < 1) return false;
                        try
                        {
                            if (OfflinePropertydata.PropertyEmail != "")
                            {
                                #region Static portion
                                System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                                string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                                if (BccEmailsforsupp.Trim() != "")
                                {
                                    string[] emails = Common.Utils.SplitEmails(BccEmailsforsupp);
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        supplierMsg.Bcc.Add(emails[i]);
                                    }
                                }
                                if (Offlinedata != null)
                                {
                                    if (Offlinedata.MailContent != "")
                                    {
                                        string BccBranchEmails = Offlinedata.MailContent.Trim();
                                        if (BccBranchEmails != "")
                                        {
                                            string[] emails = Common.Utils.SplitEmails(BccBranchEmails);
                                            for (int i = 0; i < emails.Length; ++i)
                                            {
                                                supplierMsg.CC.Add(emails[i]);
                                            }
                                        }
                                    }
                                }
                                supplierMsg.Subject = "Booking Confirmation";
                                if (OfflinePropertydata.PropertyEmail != "")
                                {
                                    supplierMsg.To.Add(OfflinePropertydata.PropertyEmail);
                                }
                                supplierMsg.IsBodyHtml = true;
                                #endregion
                                #region Repeat portion also static
                                //List<CLayer.OfflineBooking> BListGST = BLayer.OfflineBooking.GetMultipleBookingDetailsGST(OfflineBookId);
                                //foreach (CLayer.OfflineBooking dt in BListGST)
                                //{
                                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntemationOfflineBookGST") + OfflineBookId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                                //+ "&BookingDetailsId=" + dt.BookingDetailsId)
                                supplierMsg.Body = message;
                                try
                                {
                                    await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
                                }
                                catch (Exception ex)
                                {
                                    Common.LogHandler.HandleError(ex);
                                }
                                //}

                                #endregion
                            }
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

        public ActionResult ViewBillCus(long OfflineBookingId)
        {
            Areas.Admin.Models.OfflineBookingModel data = new Areas.Admin.Models.OfflineBookingModel();
            try
            {
                data.OfflineBookingId = OfflineBookingId;
                data.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);
                ViewBag.viewonly = "yes";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            var PDFResult = new ViewAsPdf("~/Areas/Admin/Views/OfflineBooking/OfflineBookingCofirmation.cshtml", data)
            {
                CustomSwitches =
                    "--footer-center \"  DOS: " +
                    DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
            };

            return PDFResult;
        }

        public ActionResult ViewBillSup(long OfflineBookingId)
        {
            Areas.Admin.Models.OfflineBookingModel data = new Areas.Admin.Models.OfflineBookingModel();
            try
            {
                data.OfflineBookingId = OfflineBookingId;
                data.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);
                ViewBag.viewonly = "yes";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            var PDFResult = new ViewAsPdf("~/Areas/Admin/Views/OfflineBooking/SupplierIntemationOfflineBook.cshtml", data)
            {
                CustomSwitches =
                    "--footer-center \"  DOS: " +
                    DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
            };

            return PDFResult;
        }


        public ActionResult ViewBillCusGST(long OfflineBookingId, long BookingDetailsId)
        {
            Areas.Admin.Models.OfflineBookingModel data = new Areas.Admin.Models.OfflineBookingModel();
            try
            {
                data.OfflineBookingId = OfflineBookingId;
                data.BookingDetailsId = BookingDetailsId;
                data.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);
                ViewBag.viewonly = "yes";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            var PDFResult = new ViewAsPdf("~/Areas/Admin/Views/OfflineBookingGST/OfflineBookingCofirmation.cshtml", data)
            {
                CustomSwitches =
                    "--footer-center \"  DOS: " +
                    DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
            };

            return PDFResult;
        }

        public ActionResult ViewBillSupGST(long OfflineBookingId)
        {
            Areas.Admin.Models.OfflineBookingModel data = new Areas.Admin.Models.OfflineBookingModel();
            try
            {
                data.OfflineBookingId = OfflineBookingId;
                data.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);
                ViewBag.viewonly = "yes";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            var PDFResult = new ViewAsPdf("~/Areas/Admin/Views/OfflineBookingGST/SupplierIntemationOfflineBook.cshtml", data)
            {
                CustomSwitches =
                    "--footer-center \"  DOS: " +
                    DateTime.Now.Date.ToString("MM/dd/yyyy") + "  Page: [page]/[toPage]\"" +
                    " --footer-line --footer-font-size \"9\" --footer-spacing 6 --footer-font-name \"calibri light\""
            };

            return PDFResult;
        }

        public ActionResult ViewBillSupGSTInvoice(long OfflineBookingId)
        {
            Areas.Admin.Models.OfflineBookingModel data = new Areas.Admin.Models.OfflineBookingModel();
            try
            {
                data.OfflineBookingId = OfflineBookingId;
                ViewBag.viewonly = "yes";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            var PDFResult = new ViewAsPdf("~/Areas/Admin/Views/OfflineBookingGST/SupplierInvoice.cshtml", data)
            {
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
            return new ViewAsPdf("~/Areas/Admin/Views/ManageOfflineBooking/Mail.cshtml", inv);
        }



        public async Task<bool> ResendemailCGST(long OfflineBookingId, long BookingDetailsId,string key)
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

                System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();
                string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                if (BccEmailsforcususer.Trim() != "")
                {
                    string[] emails = Common.Utils.SplitEmails(BccEmailsforcususer);
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        customerMsg.CC.Add(emails[i]);
                    }
                }
                customerMsg.To.Add(CustomerDetails.CustomerEmail);
                if (Offlinedata != null)
                {
                    if (Offlinedata.MailContent != "")
                    {
                        string BccBranchEmails = Offlinedata.MailContent.Trim();
                        if (BccBranchEmails != "")
                        {
                            string[] emails = Common.Utils.SplitEmails(BccBranchEmails);
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                customerMsg.CC.Add(emails[i]);
                            }
                        }
                    }
                }
                //send to ops mail
                string SalesPersonOPSMails = "";
                if (OfflineBookingId > 0)
                {
                    CLayer.OfflineBooking OffdataStaff = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                    if (OffdataStaff != null)
                    {
                        if (OffdataStaff.SalesPersonId != null)
                        {
                            if (OffdataStaff.SalesPersonId > 0)
                            {
                                CLayer.User usrstaff = BLayer.User.Get(OffdataStaff.SalesPersonId);
                                if (usrstaff != null)
                                {
                                    if (usrstaff.OPSEmail != null && usrstaff.OPSEmail != "")
                                    {
                                        SalesPersonOPSMails = usrstaff.OPSEmail;
                                    }
                                }
                            }
                        }
                    }
                }
                if (SalesPersonOPSMails != "")
                {
                    string CcOPSEmails = SalesPersonOPSMails.Trim();
                    if (CcOPSEmails != "")
                    {
                        string[] emails = Common.Utils.SplitEmails(CcOPSEmails);
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            customerMsg.CC.Add(emails[i]);
                        }
                    }
                }
                customerMsg.Subject = "Booking Confirmation";




                if (OfflineBookingDetails != null)
                {
                    if (OfflineBookingDetails.GuestEmail != null && OfflineBookingDetails.GuestEmail != "")
                    {
                        customerMsg.To.Add(OfflineBookingDetails.GuestEmail);
                    }
                }
                string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingConfirmationGST") + OfflineBookingId.ToString() + "&BookingDetailsId=" + BookingDetailsId + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                message = await Common.Mailer.MessageFromHtml(link);
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
        public async Task<bool> ResendemailSGST(long OfflineBookingId, string key)
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
                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                if (OfflineBookingId < 1) return false;
                try
                {

                    if (OfflinePropertydata.PropertyEmail != "")
                    {
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntemationOfflineBookGST") + OfflineBookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();

                        string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                        if (BccEmailsforsupp.Trim() != "")
                        {
                            string[] emails = BccEmailsforsupp.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                supplierMsg.Bcc.Add(emails[i]);
                            }
                        }

                        if (Offlinedata != null)
                        {
                            if (Offlinedata.MailContent != "")
                            {
                                string BccBranchEmails = Offlinedata.MailContent.Trim();
                                if (BccBranchEmails != "")
                                {
                                    string[] emails = BccBranchEmails.Split(',');
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        supplierMsg.CC.Add(emails[i]);
                                    }
                                }
                            }
                        }


                        supplierMsg.Subject = "Booking Confirmation";
                        supplierMsg.Body = message;

                        if (OfflinePropertydata.PropertyEmail != "")
                        {
                            supplierMsg.CC.Add(OfflinePropertydata.PropertyEmail);
                        }
                        if (OfflinePropertydata.SupplierEmail != "")
                        {
                            supplierMsg.To.Add(OfflinePropertydata.SupplierEmail);
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

        public async Task<bool> ResendSupplierInvoiceSGST(long OfflineBookingId)
        {
            try
            {
                if (OfflineBookingId < 1) return false;
                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookingId);
                CLayer.OfflineBooking CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookingId);

                if (CustomerDetails == null) return false;
                if (OfflinePropertydata == null) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                if (OfflineBookingId < 1) return false;
                try
                {

                    if (OfflinePropertydata.PropertyEmail != "")
                    {
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierInvoiceOfflineBookGST") + OfflineBookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();

                        //string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                        //if (BccEmailsforsupp.Trim() != "")
                        //{
                        //    string[] emails = BccEmailsforsupp.Split(',');
                        //    for (int i = 0; i < emails.Length; ++i)
                        //    {
                        //        supplierMsg.Bcc.Add(emails[i]);
                        //    }
                        //}

                        //if (Offlinedata != null)
                        //{
                        //    if (Offlinedata.MailContent != "")
                        //    {
                        //        string BccBranchEmails = Offlinedata.MailContent.Trim();
                        //        if (BccBranchEmails != "")
                        //        {
                        //            string[] emails = BccBranchEmails.Split(',');
                        //            for (int i = 0; i < emails.Length; ++i)
                        //            {
                        //                supplierMsg.CC.Add(emails[i]);
                        //            }
                        //        }
                        //    }
                        //}


                        supplierMsg.Subject = "Supplier Invoice";
                        supplierMsg.Body = message;

                        if (OfflinePropertydata.PropertyEmail != "")
                        {
                            supplierMsg.CC.Add(OfflinePropertydata.PropertyEmail);
                        }
                        if (OfflinePropertydata.SupplierEmail != "")
                        {
                            supplierMsg.To.Add(OfflinePropertydata.SupplierEmail);
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


        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<bool> BookingDeleteRequestAlertMail(long OfflineBookingId)
        {
            try
            {
                string message = "";
                Common.Mailer ml = new Common.Mailer();
                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();

                string email = User.Identity.GetUserName();
                long LoginUserid = BLayer.User.GetUserId(email);

                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingDeleteRequest")
                                                              + OfflineBookingId.ToString() + "&LoginUserid=" + LoginUserid);

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
        public bool DraftApproved(long OfflineBookingId)
        {
            try
            {
                BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Submit);
                BLayer.OfflineBooking.UpdateDraftbookingStatus(OfflineBookingId, (int)CLayer.OfflineBooking.DraftbookingStatus.Approved);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }
        public async Task<bool> DraftReject(long OfflineBookingId)
        {
            try
            {
                BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Delete);

                BLayer.OfflineBooking.UpdateDraftbookingStatus(OfflineBookingId, (int)CLayer.OfflineBooking.DraftbookingStatus.Reject);
                //Send mail to user
                await DraftBookingRejectMail(OfflineBookingId);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<bool> DraftBookingRejectMail(long OfflineBookingId)
        {
            try
            {
                string message = "";
                Common.Mailer ml = new Common.Mailer();
                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();
                CLayer.OfflineBooking offedit = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                CLayer.User CreatedBydt = BLayer.User.Get(offedit.CreatedBy);

                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingSavedDraftReject")
                                                              + OfflineBookingId.ToString() + "&CreatedByUserid=" + offedit.CreatedBy);

                string BookingDraftreject = BLayer.Settings.GetValue(CLayer.Settings.Booking_rejected_alert_mails);
                if (BookingDraftreject != "")
                {
                    string[] BookingDraftrejects = BookingDraftreject.Split(',');
                    for (int i = 0; i < BookingDraftrejects.Length; ++i)
                    {
                        MailMsg.CC.Add(BookingDraftrejects[i]);
                    }
                }
                if (CreatedBydt != null)
                {
                    if (CreatedBydt.Email != null)
                    {
                        if (CreatedBydt.Email != "")
                        {
                            MailMsg.To.Add(CreatedBydt.Email);
                        }
                    }
                }
                MailMsg.Subject = "Booking rejected - " + offedit.ConfirmationNumber;
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


        [AllowAnonymous]
        public async Task<string> SupplierPaymentSheduleEmail(long OfflineBookingId)
        {
            WebClient wc = new WebClient();
            try
            {
                Models.OfflinePaymentModel data = new Models.OfflinePaymentModel();
                CLayer.OfflineBooking offbook = BLayer.OfflineBooking.GetAllOfflineDetailsByOfflinebookid(OfflineBookingId);
                MailMessage mm = new MailMessage();

                string Bccsupplierpaymentscheduleemail = BLayer.Settings.GetValue(CLayer.Settings.SUPPLIERPAYMENTSCHEDULEEMAIL);
                if (Bccsupplierpaymentscheduleemail != "")
                {
                    mm.To.Clear();
                    string[] emails = Bccsupplierpaymentscheduleemail.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mm.To.Add(emails[i]);
                    }
                }

                mm.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                Common.Mailer mlr = new Common.Mailer();
                mm.IsBodyHtml = true;
                CLayer.OfflineBooking offedit = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);

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
                    OfflineBookingId = offbook.OfflineBookingId;
                    string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflinebookingSupplierPaymentSchedule") + OfflineBookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                    //for normal user mail body
                    string result = await Common.Mailer.MessageFromHtml(link);

                    mm.Body = result;
                    await mlr.SendMailAsyncWithoutFields(mm);

                    //Update supplier payment mail send data
                    BLayer.OfflineBooking.UpdateSupplierPaymentmailsendData(OfflineBookingId);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
        }


        public async Task<bool> OfflineBookingConfirmCheckinReminder(long OfflineBookId, long BookingDetailsId, string GuestEmail)
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