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
using StayBazar.Areas.Admin;


namespace StayBazar.Controllers
{
   
    [AllowAnonymous]
    public class GDSInvoiceController : Controller
    {
        public int InventoryAPIType = 0;
        public string InvoiceNumber = "";
        public DateTime InvoiceDate = new DateTime();
        // GET: GDSInvoice
        public ActionResult Index()
        {
            return View();
        }
   
    private async Task<CLayer.Invoice> MakeNewInvoice(long BookingId,long InvID=0)
    {
        CLayer.Invoice data = new CLayer.Invoice();
        data.BookingId = BookingId;
        InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(BookingId));
            //   data.InvoiceDate = DateTime.Today;
            List<CLayer.BookingItem> ob = new List<CLayer.BookingItem>();
          if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
          {
                ob= BLayer.BookingItem.GetAllDetails(BookingId, true);
            }
            else
            {
                ob = BLayer.BookingItem.GetAllDetails(BookingId);
            }
             
        string num = BLayer.Settings.GetValue(CLayer.Settings.INVOICE_DUE_DAYS);
        int i = 0;
        int.TryParse(num, out i);

        data.DueDate = ob[0].CheckOut.AddDays(i);
        data.Status = (int)CLayer.ObjectStatus.InvoiceStatus.Saved;
        data.InvoiceType = (int)CLayer.ObjectStatus.InvoiceType.Invoice;
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data.InvoiceNumber =Convert.ToString(TempData["InvoiceNumber"]);
                data.InvoiceDate = Convert.ToDateTime(TempData["InvoiceDate"]);
                data.InvoiceId = InvID;
                data.InvoiceId = BLayer.Invoice.GDSSave(data);
            }
            else
            {
                data.InvoiceId = BLayer.Invoice.Save(data);
            }
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data = BLayer.Invoice.GetGDSInvoiceByBookingID(BookingId);
            }
            else
            {
                data = BLayer.Invoice.GetInvoiceByOfflineBooking(BookingId);
            }
        data = await MessageFromHtml(BookingId);
        return data;
    }
    private async Task<CLayer.Invoice> MessageFromHtml(long offid)
    {
       // string url = ConfigurationManager.AppSettings.Get("GDSInvoiceLink") + offid.ToString();
            string url = ConfigurationManager.AppSettings.Get("GDSInvoiceLink") + offid.ToString() + "&loggedInUser=" + User.Identity.GetUserId() + "";

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

         InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(offid));
            CLayer.Invoice data = null;
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data = BLayer.Invoice.GetGDSInvoiceByBookingID(offid);
            }
            else
            {
              
                data = BLayer.Invoice.GetInvoiceByOfflineBooking(offid);
            }
        if (data == null)
        {
            return null;
        }

        data.HtmlSection1 = html1;
        data.HtmlSection2 = html2;
        data.HtmlSection3 = html3;


        int BookingType = BLayer.Bookings.GetBookingType(offid);
            BookingType = BookingType > 1 ? (int)CLayer.ObjectStatus.PropertyInventoryType.Offline : (int)CLayer.ObjectStatus.PropertyInventoryType.Online;

        if (BookingType == (int)CLayer.ObjectStatus.PropertyInventoryType.Online)
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

    //[Common.RoleRequired(AllowAllRoles = true)]
    public async Task<ActionResult> Preview(long obId)
    {
            try
            {
                #region Preview
                if (!User.Identity.IsAuthenticated)
                {
                    if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] == null)
                    {

                        return RedirectToAction("Index", "Continue");
                    }
                }

                CLayer.Invoice data = new CLayer.Invoice();
                InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(obId));
                long PropertyID = BLayer.Bookings.GetPropertyId(obId);
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    data = BLayer.Invoice.GetGDSInvoiceByBookingID(obId);
                }
                else
                {
                    data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
                }
                if(data !=null )
                {
                    if (data.InvoiceNumber == null || data.InvoiceNumber == "")
                    {
                        CLayer.InvoiceNumberData inValue;
                        inValue = BLayer.NumberGenerator.GetGDSGSTInvoiceNumber(data.BookingId, PropertyID);
                        if (inValue != null)
                        {
                            data.InvoiceNumber = inValue.InvoiceNumber;
                            data.InvoiceDate = inValue.InvoiceDate;
                            InvoiceNumber = inValue.InvoiceNumber;
                            InvoiceDate = inValue.InvoiceDate;
                            TempData["InvoiceNumber"] = InvoiceNumber;
                            TempData["InvoiceDate"] = InvoiceDate;

                            CLayer.Invoice dataGDSInvoice = new CLayer.Invoice();
                            dataGDSInvoice.InvoiceId = data.InvoiceId;
                            dataGDSInvoice.BookingId = data.BookingId;
                            dataGDSInvoice.InvoiceNumber = inValue.InvoiceNumber;
                            dataGDSInvoice.InvoiceDate = inValue.InvoiceDate;
                            long Result = BLayer.Invoice.UpdateGDSInvoiceByBookingID(data);
                        }
                    }
                }
  

                if (data == null)
                {

                    data = await MakeNewInvoice(obId);
                }

                if (data.InvoiceNumber == null || data.InvoiceNumber == "")
                {
                    CLayer.InvoiceNumberData inValue;
                    inValue = BLayer.NumberGenerator.GetGDSGSTInvoiceNumber(data.BookingId, PropertyID);
                    if (inValue != null)
                    {
                        CLayer.Invoice dataGDSInvoice = new CLayer.Invoice();
                        dataGDSInvoice.InvoiceId = data.InvoiceId;
                        dataGDSInvoice.BookingId = data.BookingId;
                        dataGDSInvoice.InvoiceNumber = inValue.InvoiceNumber;
                        dataGDSInvoice.InvoiceDate = inValue.InvoiceDate;
                        long Result = BLayer.Invoice.UpdateGDSInvoiceByBookingID(data);
                       
                    }
                }


                string url = ConfigurationManager.AppSettings.Get("GDSInvoiceLink") + obId.ToString() + "&loggedInUser=" + User.Identity.GetUserId() + "";
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
                StayBazar.Areas.Admin.Models.Invoice inv = new StayBazar.Areas.Admin.Models.Invoice();
                inv.InvoiceId = data.InvoiceId;
                inv.BookingId = data.BookingId;
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



                int BookingType = BLayer.Bookings.GetBookingType(obId);
                BookingType = BookingType > 1 ? (int)CLayer.ObjectStatus.PropertyInventoryType.Offline : (int)CLayer.ObjectStatus.PropertyInventoryType.Online;

                if (BookingType == (int)CLayer.ObjectStatus.PropertyInventoryType.Online)
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

                inv.LoggedInUser = User.Identity.GetUserId();
                return View("Preview", inv);
                #endregion
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        [AllowAnonymous]
        public ActionResult AutoInvoice(long obid,string loggedInUser)
        {
            
            Session["LoggedInUser"] = loggedInUser;
           List<CLayer.BookingItem> ob = BLayer.BookingItem.GetAllDetails(obid, true);
            
            return View("AutoInvoice", ob[0]);
        }
        public async Task<ActionResult> InvoicePDF(long obId)
        {
        
            CLayer.Invoice data = new CLayer.Invoice();
            InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(obId));

            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data = BLayer.Invoice.GetGDSInvoiceByBookingID(obId);
            }
            else
            {
                data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
            }
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
            StayBazar.Areas.Admin.Models.Invoice inv = new StayBazar.Areas.Admin.Models.Invoice();
            inv.InvoiceId = data.InvoiceId;
            inv.BookingId = data.BookingId;
            inv.IsMailed = (data.MailedDate <= DateTime.Today);


            string url = ConfigurationManager.AppSettings.Get("GDSInvoiceLink") + obId.ToString()+"&loggedInUser="+User.Identity.GetUserId().ToString()+"";
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

            int BookingType = BLayer.Bookings.GetBookingType(obId);
            BookingType = BookingType > 1 ? (int)CLayer.ObjectStatus.PropertyInventoryType.Offline : (int)CLayer.ObjectStatus.PropertyInventoryType.Online;

            if (BookingType == (int)CLayer.ObjectStatus.PropertyInventoryType.Online)
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
   public async Task<ActionResult> showInvoicePreviewonly(long obId)
    {
            CLayer.Invoice data = new CLayer.Invoice();
            InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(obId));

            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data = BLayer.Invoice.GetGDSInvoiceByBookingID(obId);
            }
            else
            {
                data = BLayer.Invoice.GetInvoiceByOfflineBooking(obId);
            }

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

        string url = ConfigurationManager.AppSettings.Get("GDSInvoiceLink") + obId.ToString();
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


         StayBazar.Areas.Admin.Models.Invoice inv = new StayBazar.Areas.Admin.Models.Invoice();
        inv.InvoiceId = data.InvoiceId;
        inv.BookingId = data.BookingId;
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

            int BookingType = BLayer.Bookings.GetBookingType(obId);
            BookingType = BookingType > 1 ? (int)CLayer.ObjectStatus.PropertyInventoryType.Offline : (int)CLayer.ObjectStatus.PropertyInventoryType.Online;

            if (BookingType == (int)CLayer.ObjectStatus.PropertyInventoryType.Online)
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
    }
}