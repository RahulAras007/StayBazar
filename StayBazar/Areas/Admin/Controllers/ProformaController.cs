using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using Rotativa;
using Microsoft.AspNet.Identity;
using System.Data;

namespace StayBazar.Areas.Admin.Controllers
{
    public class ProformaController : Controller
    {
        [Common.AdminRolePermission(AllowAllRoles=true)]
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

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> Edit(long obid)
        {
            CLayer.Invoice data = BLayer.Invoice.GetProformaByOfflineBooking(obid);
            if (data != null)
            {
                if (data.HtmlSection1 == "" || data.HtmlSection2 == "")
                {
                    data = await MessageFromHtml(obid);
                }
            }
            else
            {
                data = new CLayer.Invoice();
                data.OfflineBookingId = obid;
                data.InvoiceDate = DateTime.Today;
                data.DueDate = DateTime.Today.AddDays(10);
                data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Proforma;
                BLayer.Invoice.Save(data);
             //   data = BLayer.Invoice.(obid);
                data = await MessageFromHtml(obid);
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
            inv.InvoiceNumber = data.InvoiceNumber;
            return View("Edit",inv);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> SaveDetails(Models.Invoice data)
        {
            CLayer.Invoice sdata = null;
            if (data.OfflineBookingId > 0)
            {
                sdata = BLayer.Invoice.GetProformaByOfflineBooking(data.OfflineBookingId);
            }
            if (sdata == null) sdata = new CLayer.Invoice();

            sdata.OfflineBookingId = data.OfflineBookingId;
            sdata.InvoiceId = data.InvoiceId;
            DateTime dt = DateTime.Today;
            DateTime.TryParse(data.InvoiceDate, out dt);
            sdata.InvoiceDate = dt;
            sdata.HtmlSection1 = data.HtmlSection1;
            sdata.HtmlSection2 = data.HtmlSection2;
            sdata.HtmlSection3 = data.HtmlSection3;
            //Common.Utils.
            // sdata.InvoiceDate = DateTime.Today;
            sdata.DueDate = DateTime.Today.AddDays(10);
            string prNumber = BLayer.NumberGenerator.GetProformaNumber();
            sdata.InvoiceNumber = prNumber;
            sdata.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Proforma;
            if (data.OPType == 1)//save btn
            {
                BLayer.Invoice.Save(sdata);
                // BLayer.Invoice.Save(sdata);
            }
            else if (data.OPType == 2)//mail btn
            {
                data.OPType = 1;
                BLayer.Invoice.Save(sdata);


                if (sdata != null)
                {
                    if (sdata.InvoiceId > 0)
                    {
                        BLayer.Invoice.SetMailedDate(sdata.InvoiceId, DateTime.Today);
                    }
                }


                // send mail to customer -- after save
                string msg = await GetMailBody(sdata.OfflineBookingId);
                //send mail here using msg as body

                Common.Mailer ml = new Common.Mailer();
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                CLayer.OfflineBooking customer = new CLayer.OfflineBooking();
                if (data.OfflineBookingId > 0)
                {
                    customer = BLayer.OfflineBooking.GetAllCustomerDetails(data.OfflineBookingId);
                }

                if (customer != null)
                {
                    if (customer.CustomerEmail != "")
                    {
                        mail.To.Add(customer.CustomerEmail);
                    }
                }


                //mail.To.Add(BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL));

                string BccEmailsforcus = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                if (BccEmailsforcus != "")
                {
                    string[] emails = BccEmailsforcus.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mail.Bcc.Add(emails[i]);
                    }
                }

                mail.Subject = "Invoice";
                mail.Body = msg;
                mail.IsBodyHtml = true;
                try
                {
                    await ml.SendMailAsyncForBooking(mail, Common.Mailer.MailType.Reservation);
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }

            }
            else if (data.OPType == 3) //reset
            {
                sdata.HtmlSection1 = "";
                sdata.HtmlSection2 = "";
                BLayer.Invoice.Save(sdata);
            }



            return await Edit(data.OfflineBookingId);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        private async Task<string> GetMailBody(long obid)
        {
            string url = ConfigurationManager.AppSettings.Get("ProformaMailLink") + obid.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            return mainHtml;
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        private async Task<CLayer.Invoice>  MessageFromHtml(long offid)
        {
            string url = ConfigurationManager.AppSettings.Get("ProformaLink") + offid.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html1, html2, html3;
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

            CLayer.Invoice data = BLayer.Invoice.GetProformaByOfflineBooking(offid);
            if (data == null)
            {
                return null;
            }

            data.HtmlSection1 = html1;
            data.HtmlSection2 = html2;
            data.HtmlSection3 = html3;

            BLayer.Invoice.Save(data);
            return data;
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> Preview(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetProformaByOfflineBooking(obId);
            if (data != null)
            {
                if (data.InvoiceNumber == null || data.InvoiceNumber == "")
                {
                    string invNumber = BLayer.NumberGenerator.GetProformaNumber();
                    CLayer.Invoice InvoiceNumber = new CLayer.Invoice();
                    data.InvoiceNumber = invNumber;
                    BLayer.Invoice.Save(data);
                }
            }
            if (data == null)
            {
                string invNumber = BLayer.NumberGenerator.GetProformaNumber();
                data = new CLayer.Invoice();
                data.OfflineBookingId = obId;
                data.InvoiceNumber = invNumber;
                data.InvoiceDate = DateTime.Today;
                data.DueDate = DateTime.Today.AddDays(10);
                data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Proforma;
                BLayer.Invoice.Save(data);
                //data = BLayer.Invoice.GetProformaByOfflineBooking(offId);
                //data = await MessageFromHtml(offId);
            }
            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);
            
            string url = ConfigurationManager.AppSettings.Get("ProformaLink") + obId.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html1, html2, html3;
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
            
            return View("Preview", inv);
        }
        [AllowAnonymous]
        public ActionResult MailContent(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetProformaByOfflineBooking(obId);

            if (data == null)
            {
                data = new CLayer.Invoice();
                data.OfflineBookingId = obId;
                data.InvoiceDate = DateTime.Today;
                data.DueDate = DateTime.Today.AddDays(10);
                data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Proforma;
                BLayer.Invoice.Save(data);
                //data = BLayer.Invoice.GetProformaByOfflineBooking(offId);
                //data = await MessageFromHtml(offId);
            }
            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);
            inv.HtmlSection1 = data.HtmlSection1;
            inv.HtmlSection2 = data.HtmlSection2;
            inv.HtmlSection3 = data.HtmlSection3;
            return View("Mail", inv);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> InvoicePDF(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetProformaByOfflineBooking(obId);

            if (data == null)
            {
                data = new CLayer.Invoice();
                data.OfflineBookingId = obId;
                data.InvoiceDate = DateTime.Today;
                data.DueDate = DateTime.Today.AddDays(10);
                data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Proforma;
                BLayer.Invoice.Save(data);
                //data = BLayer.Invoice.GetProformaByOfflineBooking(offId);
                //data = await MessageFromHtml(offId);
            }
            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);



            string url = ConfigurationManager.AppSettings.Get("ProformaLink") + obId.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html1, html2, html3;
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

            //inv.HtmlSection1 = data.HtmlSection1;
            //inv.HtmlSection2 = data.HtmlSection2;
            //inv.HtmlSection3 = data.HtmlSection3;
            return new ViewAsPdf("Mail", inv);
        }

        // for pdf
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> ReportPdf(long obId)
        {
            CLayer.Invoice data = BLayer.Invoice.GetProformaByOfflineBooking(obId);

            if (data == null)
            {
                data = new CLayer.Invoice();
                data.OfflineBookingId = obId;
                data.InvoiceDate = DateTime.Today;
                data.DueDate = DateTime.Today.AddDays(10);
                data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Proforma;
                BLayer.Invoice.Save(data);
                //data = BLayer.Invoice.GetProformaByOfflineBooking(offId);
                //data = await MessageFromHtml(offId);
            }
            Models.Invoice inv = new Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.OfflineBookingId = data.OfflineBookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);


            string url = ConfigurationManager.AppSettings.Get("ProformaLink") + obId.ToString();
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            string mainHtml = await client.GetStringAsync(url);
            string html2;
            int i, len;

           // html2 = mainHtml;
           // html2 = html2.Substring(html2.IndexOf("<!--#SECONDROW_START-->"));
           // len = html2.Length;
            //i = html2.LastIndexOf("<!--#SECONDROW_END-->");
            //html2 = html2.Substring(0, len - (len - i));
            if (data.HtmlSection2 != null && data.HtmlSection2 != "")
            {
                inv.HtmlSection2 = data.HtmlSection2;
            }
            else
            {
               // inv.HtmlSection2 = html2;
            }


            inv.HtmlSection1 = data.HtmlSection1;
            inv.HtmlSection3 = data.HtmlSection3;

           
            return View("Preview", inv);
        }
        //for print
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult ReportPrint(long obId)
        {
            Models.Invoice data = new Models.Invoice();
            DataTable Reportlist = new DataTable();
            //List<CLayer.PropertyDetailsReport> Reportlist;
            try
            {
                Reportlist = BLayer.Report.ReportCityWiseCount();
                ViewBag.Filter = new Models.Invoice();
                //data.ReportList = Reportlist;             
                //data.ForPrint = true;
                //data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Print", Reportlist);
        }


        //for exl
        //public ActionResult ExcelExport(long obId)
        //{
            //    DataTable dt = BLayer.Report.ReportCityWiseCount();
            //    string attachment = "attachment; filename=Report.xls";
            //    Response.ClearContent();
            //    Response.AddHeader("content-disposition", attachment);
            //    Response.ContentType = "application/vnd.ms-excel";
            //    string tab = "";
            //    foreach (DataColumn dc in dt.Columns)
            //    {
            //        Response.Write(tab + dc.ColumnName);
            //        tab = "\t";
            //    }
            //    Response.Write("\n");
            //    int i;
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        tab = "";
            //        for (i = 0; i < dt.Columns.Count; i++)
            //        {
            //            Response.Write(tab + dr[i].ToString());
            //            tab = "\t";
            //        }
            //        Response.Write("\n");
            //    }
            //    Response.End();
            //    Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            //    data.propertyDetails = dt;
            //    return View("Index", data);
            //}

        }
}