using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Rotativa;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net.Mime;
using System.IO;
using Spire.Pdf;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;
using Spire.Pdf.Security;
using System.Net;

namespace StayBazar.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class InvoiceController : Controller
    {
        private const string INVOICE_NUMBER_LOC = "<!--#INVOICENUMBER#-->";
        private const string INVOICE_DATE_LOC = "<!--#INVOICEDATE#-->";
        private const string INVOICE_FOLDER = "~/Files/Invoices/";

        #region Methods
        //without invoice date and number
        private async Task<CLayer.Invoice> MakeNewInvoice(long offlineBookingId)
        {
            CLayer.Invoice data = new CLayer.Invoice();
            data.OfflineBookingId = offlineBookingId;
            //   data.InvoiceDate = DateTime.Today;
            CLayer.OfflineBooking ob = BLayer.OfflineBooking.GetAllDetailsById(offlineBookingId);
            string num = BLayer.Settings.GetValue(CLayer.Settings.INVOICE_DUE_DAYS);
            int i = 0;
            int.TryParse(num, out i);
            
            //karthikms added code and comment
            //CLayer.InvoiceNumberData inv = BLayer.NumberGenerator.GetInvoiceNumber();

            data.DueDate = ob.CheckOut.AddDays(i);
            data.Status = (int)CLayer.ObjectStatus.InvoiceStatus.Saved;
            data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
            //karthikms added code and comment
            //data.InvoiceNumber = inv.InvoiceNumber.ToString();
            data.InvoiceId = BLayer.Invoice.Save(data);
            data = BLayer.Invoice.GetInvoiceByOfflineBooking(offlineBookingId);
            data = await MessageFromHtml(offlineBookingId);
            return data;
        }

        #endregion

        [Common.AdminRolePermission(AllowAllRoles = true)]
        // GET: Admin/Invoice
        public ActionResult Index(long obid = 0, string reset = "no")
        {
            string inLInk = ConfigurationManager.AppSettings.Get("InvoiceLink");
            //  string htmlBody = 
            return View();
        }
        [AllowAnonymous]
        public ActionResult AutoInvoice(long obid)
        {
            CLayer.OfflineBooking ob = BLayer.OfflineBooking.GetAllDetailsById(obid);
            return View("AutoInvoice", ob);
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<ActionResult> Edit(long obid)
        {
            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obid);
            if (data != null)
            {
                int bookType = BLayer.OfflineBooking.GetBookingType(obid);
                if(bookType == (int) CLayer.ObjectStatus.OfflineBookingType.Direct)
                {
                    if (data.HtmlSection1 == "" || data.HtmlSection2 == "" || data.HtmlSection4 =="")
                    {
                        data = await MessageFromHtml(obid);
                    }
                }
                else
                {
                    if (data.HtmlSection1 == "" || data.HtmlSection2 == "")
                    {
                        data = await MessageFromHtml(obid);
                    }
                }
                
            }
            else
            {
                data = await MakeNewInvoice(obid);
            }

            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            DateTime dt = DateTime.Today;
            if (data.InvoiceDate != DateTime.MinValue) inv.InvoiceDate = data.InvoiceDate.ToString("dd/MM/yyyy");

            string u = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(u, out id);
            if (BLayer.User.GetRole(id) != CLayer.Role.Roles.Administrator)
            { inv.ShowText = 2; }


            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);
            inv.HtmlSection1 = data.HtmlSection1;
            inv.HtmlSection2 = data.HtmlSection2;
            inv.HtmlSection3 = data.HtmlSection3;

            if (obid > 0)
            {
                int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(obid);
                if (OfflineBookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
                {
                    inv.HtmlSection4 = data.HtmlSection4;
                }
                else
                {
                    inv.HtmlSection4 = "";
                }
            }

            // inv.HtmlMid = data.HtmlSection2;
            inv.Status = data.Status;
            return View(inv);
        }
        [HttpPost]
        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<ActionResult> SaveDetails(Models.Invoice data)
        {
            CLayer.Invoice sdata = null;
            if (data.OfflineBookingId > 0)
            {
                sdata = BLayer.Invoice.GetInvoiceByOfflineBooking(data.OfflineBookingId);
            }
            if (sdata == null)
            {
                sdata = await MakeNewInvoice(data.OfflineBookingId);
            }


            sdata.OfflineBookingId = data.OfflineBookingId;

            if (data.InvoiceId > 0) sdata.InvoiceId = data.InvoiceId;

            //DateTime dt = DateTime.Today;
            //DateTime.TryParse(data.InvoiceDate, out dt);
            //sdata.InvoiceDate = dt;
            sdata.HtmlSection1 = data.HtmlSection1;
            sdata.HtmlSection2 = data.HtmlSection2;
            sdata.HtmlSection3 = data.HtmlSection3;


            if (data.OfflineBookingId > 0)
            {
                int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(data.OfflineBookingId);
                if (OfflineBookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
                {
                    sdata.HtmlSection4 = data.HtmlSection4;
                }
                else
                {
                    sdata.HtmlSection4 = "";
                }
            }

            //Common.Utils.
            // sdata.InvoiceDate = DateTime.Today;

            sdata.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
            if (data.OPType == 1) // Save button clicked
            {
                BLayer.Invoice.Save(sdata);
                // BLayer.Invoice.Save(sdata);
            }
            else if (data.OPType == 2) // Send mail button
            {

                int bookingType = BLayer.OfflineBooking.GetBookingType(data.OfflineBookingId);
                if (bookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Regular)
                {
                    data.OPType = 1;
                    sdata.MailedDate = DateTime.Today;
                    BLayer.Invoice.Save(sdata);

                    if (sdata != null)
                    {
                        if (sdata.InvoiceId > 0)
                        {
                            BLayer.Invoice.SetMailedDate(sdata.InvoiceId, DateTime.Today);
                            if (data.OfflineBookingId > 0)
                            {
                                // Send mail
                                await SendMailInvoice(data.OfflineBookingId);
                            }
                        }
                    }
                }
            }
            else if (data.OPType == 3) //
            {
                sdata = await MessageFromHtml(data.OfflineBookingId);
                BLayer.Invoice.Save(sdata);
            }
            else if (data.OPType == 4) //Approve Invoce details
            {
                //bool allowed = false;
                //if (BLayer.OfflineBooking.GetBookingType(sdata.OfflineBookingId) != (int)CLayer.ObjectStatus.OfflineBookingType.TAC)
                //{
                //    allowed = true;
                //}
              //  if (allowed)
             //   {
                    int taxtype = BLayer.OfflineBooking.GetTaxType(sdata.OfflineBookingId);
                    //Invoice Number if Exists
                    CLayer.Invoice invdt = new CLayer.Invoice();
                    if (data.InvoiceId > 0)
                    {
                        invdt = BLayer.Invoice.GetInvoice(data.InvoiceId);
                        if (invdt != null)
                        {
                            if (invdt.InvoiceNumber != null && invdt.InvoiceNumber != "")
                            {
                                sdata.InvoiceNumber = invdt.InvoiceNumber;
                            }
                        }
                    }

                    if (sdata.InvoiceNumber == null || sdata.InvoiceNumber == "")
                    {
                        CLayer.InvoiceNumberData inValue;
                        if (taxtype == (int)CLayer.ObjectStatus.OfflineBookingTaxType.GST )
                        {
                            inValue = BLayer.NumberGenerator.GetGSTInvoiceNumber(sdata.OfflineBookingId);
                        }
                        else
                        {
                            inValue = BLayer.NumberGenerator.GetInvoiceNumber();
                        }
                        if (inValue != null)
                        {
                            sdata.InvoiceNumber = inValue.InvoiceNumber;
                            sdata.InvoiceDate = inValue.InvoiceDate;
                        }
                    }

                    sdata.HtmlSection1 = sdata.HtmlSection1.Replace(INVOICE_NUMBER_LOC, sdata.InvoiceNumber.ToString());
                    sdata.HtmlSection1 = sdata.HtmlSection1.Replace(INVOICE_DATE_LOC, sdata.InvoiceDate.ToString("dd/MM/yyyy"));

                    if(sdata.HtmlSection4 != "")
                    {
                        sdata.HtmlSection4 = sdata.HtmlSection4.Replace(INVOICE_NUMBER_LOC, sdata.InvoiceNumber.ToString());
                        sdata.HtmlSection4 = sdata.HtmlSection4.Replace(INVOICE_DATE_LOC, sdata.InvoiceDate.ToString("dd/MM/yyyy"));
                    }

                    sdata.Status = (int)CLayer.ObjectStatus.InvoiceStatus.Submitted;
                    BLayer.Invoice.Save(sdata);

                    if (data.OfflineBookingId > 0)
                    {
                        CLayer.OfflineBooking cus = BLayer.OfflineBooking.GetAllCustomerDetails(data.OfflineBookingId);
                        //if (!BLayer.OfflineBooking.CanSendInvoiceMail(cus.CustomerName, cus.CustomerEmail))
                        //{
                        //    // Send mail
                        //    await SendMailInvoice(data.OfflineBookingId);
                        //}
                    }
              //  }// only if allowed
            }
            else if (data.OPType == 5) //reset Invoice details
            {
                sdata.HtmlSection1 = "";
                sdata.HtmlSection2 = "";
                sdata.HtmlSection4 = "";
                BLayer.Invoice.Save(sdata);
            }

            return RedirectToAction("Edit", new { obid = sdata.OfflineBookingId });
        }


        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<bool> SendMailInvoice(long OfflineBookingId)
        {
            string Filename = "";
            // send mail to customer -- after save
            string msg = await GetMailBody(OfflineBookingId);

            Common.Mailer ml = new Common.Mailer();
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            CLayer.OfflineBooking booking = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
            CLayer.OfflineBooking customer = new CLayer.OfflineBooking();
           
            int bookingType = BLayer.OfflineBooking.GetBookingType(OfflineBookingId);

            if (bookingType == (int)CLayer.ObjectStatus.OfflineBookingType.TAC)
            {
                //Mail To Supplier
                if (OfflineBookingId > 0)
                {
                    CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookingId);
                    if (OfflinePropertydata != null)
                    {
                        if (OfflinePropertydata.SupplierEmail != null && OfflinePropertydata.SupplierEmail != "")
                        {
                            mail.To.Add(OfflinePropertydata.SupplierEmail);
                        }
                    }
                }
                string BccEmailsforSup = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                if (BccEmailsforSup.Trim() != "")
                {
                    string[] emails = BccEmailsforSup.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mail.CC.Add(emails[i]);
                    }
                }
            }
            else
            {
                //Mail To Customer
                if (OfflineBookingId > 0)
                {
                    customer = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookingId);
                }
                if (customer != null)
                {
                    if (customer.CustomerEmail != "")
                    {
                        mail.To.Add(customer.CustomerEmail);
                    }
                }
                #region for corporate
                //for corporate admins
                //long userid = BLayer.User.GetUserId(customer.CustomerEmail);
                //if (userid > 0)
                //{
                //    CLayer.Role.Roles rle = BLayer.User.GetRole(userid);
                //    if (rle == CLayer.Role.Roles.CorporateUser)
                //    {
                //        long cid = BLayer.B2B.GetCorporateIdOfUser(userid);
                //    }
                //}
                //message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CorpIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                //long cid = BLayer.B2B.GetCorporateIdOfUser(byUser.UserId);
                //if (cid > 0)
                //{
                //    string em = BLayer.User.GetEmail(cid);
                //    if (em != null && em != "")
                //    {
                //        msg.CC.Add(em);
                //    }
                //}
                //mail.To.Add(BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL));
                #endregion
                string BccEmailsforcus = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                if (BccEmailsforcus.Trim() != "")
                {
                    string[] emails = BccEmailsforcus.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mail.CC.Add(emails[i]);
                    }
                }
            }




            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(OfflineBookingId);

            if (data != null)
            {
                mail.Subject = "Invoice against Booking ID: " + booking.ConfirmationNumber + ", Invoice No. " + data.InvoiceNumber;
            }

            mail.Body = msg;
            mail.IsBodyHtml = true;





            //add atachment 
            try
            {
                Areas.Admin.Models.Invoice inv = new Areas.Admin.Models.Invoice();

                if (data != null)
                {
                    inv.InvoiceId = data.InvoiceId;
                    inv.OfflineBookingId = data.OfflineBookingId;
                    inv.IsMailed = (data.MailedDate <= DateTime.Today);
                    inv.HtmlSection1 = data.HtmlSection1;
                    inv.HtmlSection2 = data.HtmlSection2;
                    inv.HtmlSection3 = data.HtmlSection3;

                    ViewAsPdf v = new ViewAsPdf("~/Areas/Admin/Views/ManageOfflineBooking/Mail.cshtml", inv)
                    {
                        PageMargins = new Rotativa.Options.Margins(1, 1, 1, 1),
                        PageOrientation = Rotativa.Options.Orientation.Portrait,
                        PageSize = Rotativa.Options.Size.Letter
                    };
                    string newdirectory = "Files\\Temp\\" + inv.InvoiceId;
                    if (!Directory.Exists(Server.MapPath("~") + "\\" + newdirectory))
                    {
                        Directory.CreateDirectory(Server.MapPath("~") + "\\" + newdirectory);
                    }
                    Filename = Server.MapPath("~") + "\\" + newdirectory + "\\" + "Invoice_" + data.InvoiceNumber + ".pdf";
                    var byteArray = v.BuildPdf(ControllerContext);
                    var fileStream = new FileStream(Filename, FileMode.Create, FileAccess.Write);
                    fileStream.Write(byteArray, 0, byteArray.Length);
                    fileStream.Close();
                    fileStream.Dispose();

                    // System.Net.Mail.Attachment attachment;
                    Attachment attacht = new Attachment(Filename, MediaTypeNames.Application.Octet);
                    mail.Attachments.Add(attacht);
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }


            string AccountMail = BLayer.Settings.GetValue(CLayer.Settings.ACCOUNT_EMAILS);
            if (AccountMail != "")
            {
                string[] Accemails = AccountMail.Split(',');
                for (int i = 0; i < Accemails.Length; ++i)
                {
                    mail.CC.Add(Accemails[i]);
                }
            }



            //send to ops mail
            string SalesPersonOPSMails = "";
            if (OfflineBookingId > 0)
            {
                CLayer.OfflineBooking OffdataStaff = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                if (OffdataStaff != null)
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
            if (SalesPersonOPSMails != "")
            {
                string CcOPSEmails = SalesPersonOPSMails.Trim();
                if (CcOPSEmails != "")
                {
                    string[] emails = CcOPSEmails.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mail.CC.Add(emails[i]);
                    }
                }
            }

            try
            {
                await ml.SendMailAsyncForBooking(mail, Common.Mailer.MailType.Reservation);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        private async Task<string> GetMailBody(long obid)
        {
            string url = ConfigurationManager.AppSettings.Get("InvoiceMailLink") + obid.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            return mainHtml;
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        private async Task<CLayer.Invoice> MessageFromHtml(long offid)
        {
            string url = ConfigurationManager.AppSettings.Get("InvoiceLink") + offid.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html1, html2, html3, html4;
            int i, len;

            html1 = mainHtml;
            html1 = html1.Substring(html1.IndexOf("<!--#FIRSTROW_START-->"));
            len = html1.Length;
            i = html1.LastIndexOf("<!--#FIRSTROW_END-->");
            html1 = html1.Substring(0, len - (len - i));

            html2 = mainHtml;
            html2 = html2.Substring(html2.IndexOf("<!--#SECONDROW_START-->"));
            len = html2.Length;
            i = html2.LastIndexOf("<!--#SECONDROW_END-->");
            html2 = html2.Substring(0, len - (len - i));

            html3 = mainHtml;
            html3 = html3.Substring(html3.IndexOf("<!--#THIRDROW_START-->"));
            len = html3.Length;
            i = html3.LastIndexOf("<!--#THIRDROW_END-->");
            html3 = html3.Substring(0, len - (len - i));



            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(offid);
            if (data == null)
            {
                return null;
            }

            data.HtmlSection1 = html1;
            data.HtmlSection2 = html2;
            data.HtmlSection3 = html3;


            int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(offid);
            if (OfflineBookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
            {
                html4 = mainHtml;
                html4 = html4.Substring(html4.IndexOf("<!--#FOURTHROW_START-->"));
                len = html4.Length;
                i = html4.LastIndexOf("<!--#FOURTHROW_END-->");
                html4 = html4.Substring(0, len - (len - i));
                data.HtmlSection4 = html4;
            }
            else
            {
                data.HtmlSection4 = "";
            }

            BLayer.Invoice.Save(data);
            return data;
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<ActionResult> Preview(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);

            //if (data != null)
            //{
            //    //if (data.InvoiceNumber == null || data.InvoiceNumber == "")
            //    //{
            //    //    //string invNumber = BLayer.NumberGenerator.GetInvoiceNumber();
            //    //    CLayer.Invoice InvoiceNumber = new CLayer.Invoice();
            //    //    //data.InvoiceNumber = invNumber;
            //    //    BLayer.Invoice.Save(data);
            //    //}
            //}
            if (data == null)
            {
                ////string invNumber = BLayer.NumberGenerator.GetInvoiceNumber();
                //data = new CLayer.Invoice();
                //data.OfflineBookingId = obId;
                ////data.InvoiceNumber = invNumber;
                //data.InvoiceDate = DateTime.Today;
                //data.DueDate = DateTime.Today.AddDays(10);
                //data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
                //BLayer.Invoice.Save(data);
                //////data = BLayer.Invoice.GetInvoiceByOfflineBooking(offId);
                //////data = await MessageFromHtml(offId);
                data = await MakeNewInvoice(obId);
            }

            string url = ConfigurationManager.AppSettings.Get("InvoiceLink") + obId.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html1, html2, html3, html4;
            int i, len;

            html1 = mainHtml;
            html1 = html1.Substring(html1.IndexOf("<!--#FIRSTROW_START-->"));
            len = html1.Length;
            i = html1.LastIndexOf("<!--#FIRSTROW_END-->");
            html1 = html1.Substring(0, len - (len - i));

            html2 = mainHtml;
            html2 = html2.Substring(html2.IndexOf("<!--#SECONDROW_START-->"));
            len = html2.Length;
            i = html2.LastIndexOf("<!--#SECONDROW_END-->");
            html2 = html2.Substring(0, len - (len - i));

            html3 = mainHtml;
            html3 = html3.Substring(html3.IndexOf("<!--#THIRDROW_START-->"));
            len = html3.Length;
            i = html3.LastIndexOf("<!--#THIRDROW_END-->");
            html3 = html3.Substring(0, len - (len - i));

            html4 = mainHtml;
            int idxloc = html4.IndexOf("<!--#FOURTHROW_START-->");
            if (idxloc > -1)
            {
                html4 = html4.Substring(idxloc);
                len = html4.Length;
                i = html4.LastIndexOf("<!--#FOURTHROW_END-->");
                html4 = html4.Substring(0, len - (len - i));
            }
            else { html4 = ""; }
            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);

            if (data.HtmlSection2 != null && data.HtmlSection2 != "")
            {
                inv.HtmlSection1 = data.HtmlSection1;
                inv.HtmlSection2 = data.HtmlSection2;
                inv.HtmlSection3 = data.HtmlSection3;
            }
            else
            {
                inv.HtmlSection3 = html3;
                inv.HtmlSection2 = html2;
                inv.HtmlSection1 = html1;
            }



            int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(obId);
            if (OfflineBookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
            {
                if (data.HtmlSection4 != null && data.HtmlSection4 != "")
                {
                    inv.HtmlSection4 = data.HtmlSection4;
                }
                else
                {
                    inv.HtmlSection4 = html4;
                }
            }
            else
            {
                inv.HtmlSection4 = "";
            }

            ViewBag.DisableEdit = "";
            return View("Preview", inv);
        }

        public async Task<ActionResult> showInvoicePreviewonly(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);

            //if (data != null)
            //{
            //    if (data.InvoiceNumber == null || data.InvoiceNumber == "")
            //    {
            //        string invNumber = BLayer.NumberGenerator.GetInvoiceNumber();
            //        CLayer.Invoice InvoiceNumber = new CLayer.Invoice();
            //        data.InvoiceNumber = invNumber;
            //        BLayer.Invoice.Save(data);
            //    }
            //}
            if (data == null)
            {
                //string invNumber = BLayer.NumberGenerator.GetInvoiceNumber();
                //data = new CLayer.Invoice();
                //data.OfflineBookingId = obId;
                //data.InvoiceNumber = invNumber;
                //data.InvoiceDate = DateTime.Today;
                //data.DueDate = DateTime.Today.AddDays(10);
                //data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
                //BLayer.Invoice.Save(data);
                data = await MakeNewInvoice(obId);
            }

            string url = ConfigurationManager.AppSettings.Get("InvoiceLink") + obId.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html1, html2, html3, html4;
            int i, len;

            html1 = mainHtml;
            html1 = html1.Substring(html1.IndexOf("<!--#FIRSTROW_START-->"));
            len = html1.Length;
            i = html1.LastIndexOf("<!--#FIRSTROW_END-->");
            html1 = html1.Substring(0, len - (len - i));

            html2 = mainHtml;
            html2 = html2.Substring(html2.IndexOf("<!--#SECONDROW_START-->"));
            len = html2.Length;
            i = html2.LastIndexOf("<!--#SECONDROW_END-->");
            html2 = html2.Substring(0, len - (len - i));

            html3 = mainHtml;
            html3 = html3.Substring(html3.IndexOf("<!--#THIRDROW_START-->"));
            len = html3.Length;
            i = html3.LastIndexOf("<!--#THIRDROW_END-->");
            html3 = html3.Substring(0, len - (len - i));

            html4 = mainHtml;
            int idx = html4.IndexOf("<!--#FOURTHROW_START-->");
            if (idx > -1)
            {
                html4 = html4.Substring(idx);
                len = html4.Length;
                i = html4.LastIndexOf("<!--#FOURTHROW_END-->");
                html4 = html4.Substring(0, len - (len - i));
            }
            else { html4 = ""; }


            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);

            if (data.HtmlSection2 != null && data.HtmlSection2 != "")
            {
                inv.HtmlSection1 = data.HtmlSection1;
                inv.HtmlSection2 = data.HtmlSection2;
                inv.HtmlSection3 = data.HtmlSection3;
            }
            else
            {
                inv.HtmlSection3 = html3;
                inv.HtmlSection2 = html2;
                inv.HtmlSection1 = html1;
            }

            int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(obId);
            if (OfflineBookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
            {
                if (data.HtmlSection4 != null && data.HtmlSection4 != "")
                {
                    inv.HtmlSection4 = data.HtmlSection4;
                }
                else
                {
                    inv.HtmlSection4 = html4;
                }
            }
            else
            {
                inv.HtmlSection4 = "";
            }


            ViewBag.DisableEdit = "True";
            return View("Preview", inv);
        }

        [AllowAnonymous]
        public ActionResult MailContent(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);

            if (data == null)
            {
                data = new CLayer.Invoice();
                data.OfflineBookingId = obId;
                data.InvoiceDate = DateTime.Today;
                data.DueDate = DateTime.Today.AddDays(10);
                data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
                BLayer.Invoice.Save(data);
                //data = BLayer.Invoice.GetInvoiceByOfflineBooking(offId);
                //data = await MessageFromHtml(offId);
            }
            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.InvoiceNumber = data.InvoiceNumber;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);
            inv.HtmlSection1 = data.HtmlSection1;
            inv.HtmlSection2 = data.HtmlSection2;
            inv.HtmlSection3 = data.HtmlSection3;
            return View("Mail", inv);
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<ActionResult> InvoicePDF(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
            if (data == null)
            {
                data = await MakeNewInvoice(obId);
                //data = new CLayer.Invoice();
                //data.OfflineBookingId = obId;
                //data.InvoiceDate = DateTime.Today;
                //data.DueDate = DateTime.Today.AddDays(10);
                //data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
                //BLayer.Invoice.Save(data);
                //data = BLayer.Invoice.GetInvoiceByOfflineBooking(offId);
                //data = await MessageFromHtml(offId);
            }
            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);


            string url = ConfigurationManager.AppSettings.Get("InvoiceLink") + obId.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html1, html2, html3, html4;
            int i, len;

            html1 = mainHtml;
            html1 = html1.Substring(html1.IndexOf("<!--#FIRSTROW_START-->"));
            len = html1.Length;
            i = html1.LastIndexOf("<!--#FIRSTROW_END-->");
            html1 = html1.Substring(0, len - (len - i));

            html2 = mainHtml;
            html2 = html2.Substring(html2.IndexOf("<!--#SECONDROW_START-->"));
            len = html2.Length;
            i = html2.LastIndexOf("<!--#SECONDROW_END-->");
            html2 = html2.Substring(0, len - (len - i));

            html3 = mainHtml;
            html3 = html3.Substring(html3.IndexOf("<!--#THIRDROW_START-->"));
            len = html3.Length;
            i = html3.LastIndexOf("<!--#THIRDROW_END-->");
            html3 = html3.Substring(0, len - (len - i));

            html4 = mainHtml;
            int idx = html4.IndexOf("<!--#FOURTHROW_START-->");
            if (idx > -1)
            {
                html4 = html4.Substring(idx);
                len = html4.Length;
                i = html4.LastIndexOf("<!--#FOURTHROW_END-->");
                html4 = html4.Substring(0, len - (len - i));
            }
            else { html4 = ""; }



            if (data.HtmlSection2 != null && data.HtmlSection2 != "")
            {
                inv.HtmlSection1 = data.HtmlSection1;
                inv.HtmlSection2 = data.HtmlSection2;
                inv.HtmlSection3 = data.HtmlSection3;
            }
            else
            {
                inv.HtmlSection3 = html3;
                inv.HtmlSection2 = html2;
                inv.HtmlSection1 = html1;
            }

            int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(obId);
            if (OfflineBookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
            {
                if (data.HtmlSection4 != null && data.HtmlSection4 != "")
                {
                    inv.HtmlSection4 = data.HtmlSection4;
                }
                else
                {
                    inv.HtmlSection4 = html4;
                }
            }
            else
            {
                inv.HtmlSection4 = "";
            }


            //inv.HtmlSection1 = data.HtmlSection1;
            //inv.HtmlSection2 = data.HtmlSection2;
            //inv.HtmlSection3 = data.HtmlSection3;
            return new ViewAsPdf("PDF", inv);
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        public ActionResult InvoiceDigitalSignPDF(long obId)
        {
            string url = ConfigurationManager.AppSettings.Get("InvoicePDFPreview") + obId.ToString();
            String pfxPath = ConfigurationManager.AppSettings.Get("PFXFileForInvoice");
            string pfxpassword = ConfigurationManager.AppSettings.Get("PFXFileForInvoiceKey");
            // PdfDocument pdf = new PdfDocument();
            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
            string filename = "";
            if (data.InvoiceNumber == "")
            { filename = "Invoice_" + data.InvoiceId.ToString() + ".pdf"; }
            else
            {
                filename = "Invoice_" + data.InvoiceNumber + ".pdf";
            }
          
            string filepath = Server.MapPath(INVOICE_FOLDER) + filename;

            //var thread = new Thread(() => pdf.LoadFromHTML(url, false, false, false));
            //thread.SetApartmentState(ApartmentState.STA);
            //thread.Start();
            //thread.Join();

            WebClient wc = new WebClient();
            wc.DownloadFile(url, filepath);
            
        
           // pdf.SaveToFile(filepath);
            //pdf.PageSettings.Margins.Top = 2.2F;
            //pdf.PageSettings.Margins.Left = 2.2F;
            //pdf.PageSettings.Margins.Right = 2.2F;
           // pdf.Close();

            //pdf = new PdfDocument();
            PdfDocument pdf = new PdfDocument();
            pdf.LoadFromFile(filepath);
            
            PdfCertificate digi = new PdfCertificate(pfxPath, pfxpassword);
            PdfSignature signature = new PdfSignature(pdf,pdf.Pages[0],digi,"Staybazar.com");
            signature.ContactInfo = "Staybazar.com";
            signature.Certificated = true;
            signature.DocumentPermissions = PdfCertificationFlags.ForbidChanges;

            pdf.SaveToFile(filepath);
            pdf.Close();
            
           
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filename,
                Inline = true,
            };

            Response.AppendHeader("Content-Disposition", cd.ToString());

            return File(filedata, contentType);
        }

        //[Common.AdminRolePermission(AllowAllRoles = true)]
        ////parameter changes required a change in web.config appsettings - InvoicePDFPreview
        //public async Task<ActionResult> InvoicePDFHtml(long obId)
        //{
        //    CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
        //    if (data == null)
        //    {
        //        data = await MakeNewInvoice(obId);
        //        //data = new CLayer.Invoice();
        //        //data.OfflineBookingId = obId;
        //        //data.InvoiceDate = DateTime.Today;
        //        //data.DueDate = DateTime.Today.AddDays(10);
        //        //data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
        //        //BLayer.Invoice.Save(data);
        //        //data = BLayer.Invoice.GetInvoiceByOfflineBooking(offId);
        //        //data = await MessageFromHtml(offId);
        //    }
        //    Models.Invoice inv = new Models.Invoice();
        //    inv.InvoiceId = data.InvoiceId;
        //    inv.OfflineBookingId = data.OfflineBookingId;
        //    inv.IsMailed = (data.MailedDate <= DateTime.Today);


        //    string url = ConfigurationManager.AppSettings.Get("InvoiceLink") + obId.ToString();
        //    System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        //    string mainHtml = await client.GetStringAsync(url);
        //    string html1, html2, html3, html4;
        //    int i, len;

        //    html1 = mainHtml;
        //    html1 = html1.Substring(html1.IndexOf("<!--#FIRSTROW_START-->"));
        //    len = html1.Length;
        //    i = html1.LastIndexOf("<!--#FIRSTROW_END-->");
        //    html1 = html1.Substring(0, len - (len - i));

        //    html2 = mainHtml;
        //    html2 = html2.Substring(html2.IndexOf("<!--#SECONDROW_START-->"));
        //    len = html2.Length;
        //    i = html2.LastIndexOf("<!--#SECONDROW_END-->");
        //    html2 = html2.Substring(0, len - (len - i));

        //    html3 = mainHtml;
        //    html3 = html3.Substring(html3.IndexOf("<!--#THIRDROW_START-->"));
        //    len = html3.Length;
        //    i = html3.LastIndexOf("<!--#THIRDROW_END-->");
        //    html3 = html3.Substring(0, len - (len - i));

        //    html4 = mainHtml;
        //    int idx = html4.IndexOf("<!--#FOURTHROW_START-->");
        //    if (idx > -1)
        //    {
        //        html4 = html4.Substring(idx);
        //        len = html4.Length;
        //        i = html4.LastIndexOf("<!--#FOURTHROW_END-->");
        //        html4 = html4.Substring(0, len - (len - i));
        //    }
        //    else { html4 = ""; }



        //    if (data.HtmlSection2 != null && data.HtmlSection2 != "")
        //    {
        //        inv.HtmlSection1 = data.HtmlSection1;
        //        inv.HtmlSection2 = data.HtmlSection2;
        //        inv.HtmlSection3 = data.HtmlSection3;
        //    }
        //    else
        //    {
        //        inv.HtmlSection3 = html3;
        //        inv.HtmlSection2 = html2;
        //        inv.HtmlSection1 = html1;
        //    }

        //    int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(obId);
        //    if (OfflineBookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
        //    {
        //        if (data.HtmlSection4 != null && data.HtmlSection4 != "")
        //        {
        //            inv.HtmlSection4 = data.HtmlSection4;
        //        }
        //        else
        //        {
        //            inv.HtmlSection4 = html4;
        //        }
        //    }
        //    else
        //    {
        //        inv.HtmlSection4 = "";
        //    }



        //    //inv.HtmlSection1 = data.HtmlSection1;
        //    //inv.HtmlSection2 = data.HtmlSection2;
        //    //inv.HtmlSection3 = data.HtmlSection3;
        //    return View("PDF", inv);
        //}

        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<ActionResult> InvoicePDFNoSign(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
            if (data == null)
            {
                data = await MakeNewInvoice(obId);
                //data = new CLayer.Invoice();
                //data.OfflineBookingId = obId;
                //data.InvoiceDate = DateTime.Today;
                //data.DueDate = DateTime.Today.AddDays(10);
                //data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
                //BLayer.Invoice.Save(data);
                //data = BLayer.Invoice.GetInvoiceByOfflineBooking(offId);
                //data = await MessageFromHtml(offId);
            }
            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);


            string url = ConfigurationManager.AppSettings.Get("InvoiceLink") + obId.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html1, html2, html3, html4;
            int i, len;

            html1 = mainHtml;
            html1 = html1.Substring(html1.IndexOf("<!--#FIRSTROW_START-->"));
            len = html1.Length;
            i = html1.LastIndexOf("<!--#FIRSTROW_END-->");
            html1 = html1.Substring(0, len - (len - i));

            html2 = mainHtml;
            html2 = html2.Substring(html2.IndexOf("<!--#SECONDROW_START-->"));
            len = html2.Length;
            i = html2.LastIndexOf("<!--#SECONDROW_END-->");
            html2 = html2.Substring(0, len - (len - i));

            html3 = mainHtml;
            html3 = html3.Substring(html3.IndexOf("<!--#THIRDROW_START-->"));
            len = html3.Length;
            i = html3.LastIndexOf("<!--#THIRDROW_END-->");
            html3 = html3.Substring(0, len - (len - i));

            html4 = mainHtml;
            int idx = html4.IndexOf("<!--#FOURTHROW_START-->");
            if (idx > -1)
            {
                html4 = html4.Substring(idx);
                len = html4.Length;
                i = html4.LastIndexOf("<!--#FOURTHROW_END-->");
                html4 = html4.Substring(0, len - (len - i));
            }
            else { html4 = ""; }


            if (data.HtmlSection2 != null && data.HtmlSection2 != "")
            {
                inv.HtmlSection1 = data.HtmlSection1;
                inv.HtmlSection2 = data.HtmlSection2;
                inv.HtmlSection3 = data.HtmlSection3;
            }
            else
            {
                inv.HtmlSection3 = html3;
                inv.HtmlSection2 = html2;
                inv.HtmlSection1 = html1;
            }

            int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(obId);
            if (OfflineBookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
            {
                if (data.HtmlSection4 != null && data.HtmlSection4 != "")
                {
                    inv.HtmlSection4 = data.HtmlSection4;
                }
                else
                {
                    inv.HtmlSection4 = html4;
                }
            }
            else
            {
                inv.HtmlSection4 = "";
            }



            //inv.HtmlSection1 = data.HtmlSection1;
            //inv.HtmlSection2 = data.HtmlSection2;
            //inv.HtmlSection3 = data.HtmlSection3;
            return new ViewAsPdf("PDFNoSign", inv);
        }

    }



}