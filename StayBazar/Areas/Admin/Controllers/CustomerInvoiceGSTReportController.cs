using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Controllers
{
    public class CustomerInvoiceGSTReportController : Controller
    {
        public const int LIMIT = 500;
        // GET: Admin/CustomerInvoiceGSTReport
        public ActionResult Index()
        {
            DateTime curdate = DateTime.Now;
            curdate = curdate.AddDays(-30);
            //  DateTime FromDate = DateTime.Now.AddDays(-30);

            DateTime ToDate = DateTime.Now;


            string FromDates = curdate.ToString("MMMM dd, yyyy");

            string FromDatesonlyDate = DateTime.Parse(FromDates).ToShortDateString();


            string ToDates = ToDate.ToString("MMMM dd, yyyy");

            string ToDatesonlyDate = DateTime.Parse(ToDates).ToShortDateString();

            ViewBag.FromDate = FromDatesonlyDate;
            ViewBag.ToDate = ToDatesonlyDate;
            string SearchString = "";
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.CustomerInvoiceGSTReport(SearchString, LIMIT, curdate, ToDate);

            return View(users);
        }



        public ActionResult PDFView(Models.OfflineBookingModel model)
        {
            //Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            //try
            //{
            //    string date1 = Request["FromDate"];
            //    string date2 = Request["ToDate"];

            //    DateTime FromDate = Convert.ToDateTime(date1);
            //    DateTime ToDate = Convert.ToDateTime(date2);

            //    //data.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList_Report(0, 0, FromDate, ToDate);
            //    List<CLayer.OfflineBooking> data = BLayer.OfflineBooking.CustomerInvoiceGSTReport();
            //}
            //catch (Exception ex)
            //{
            //    Common.LogHandler.HandleError(ex);
            //}
            string date1 = Request["FromDate"];
            string date2 = Request["ToDate"];

            string SearchString = Request["SearchString"];


            DateTime FromDate = Convert.ToDateTime(date1);
            DateTime ToDate = Convert.ToDateTime(date2);
            List<CLayer.OfflineBooking> data = BLayer.OfflineBooking.CustomerInvoiceGSTReport(SearchString, LIMIT, FromDate, ToDate);
            return new ViewAsPdf("PDFView", data) { PageOrientation = Rotativa.Options.Orientation.Landscape };
            //return View("~/Areas/Admin/Views/SupplierInvoiceReport/_ReportList.cshtml", data);
        }

        public ActionResult Filter(CLayer.OfflineBooking model)
        {

            string date1 = Request["FromDate"];
            string date2 = Request["ToDate"];
            string SearchString = Request["SearchString"];

            DateTime FromDate = Convert.ToDateTime(date1);
            DateTime ToDate = Convert.ToDateTime(date2);

            string FromDates = FromDate.ToString("MMMM dd, yyyy");

            string FromDatesonlyDate = DateTime.Parse(FromDates).ToShortDateString();


            string ToDates = ToDate.ToString("MMMM dd, yyyy");

            string ToDatesonlyDate = DateTime.Parse(ToDates).ToShortDateString();

            ViewBag.FromDate = FromDatesonlyDate;
            ViewBag.ToDate = ToDatesonlyDate;
            ViewBag.SearchString = SearchString;
            List<CLayer.OfflineBooking> data = BLayer.OfflineBooking.CustomerInvoiceGSTReport(SearchString, LIMIT, FromDate, ToDate);


            return View("Index", data);
        }

        public ActionResult ExcelReport(CLayer.OfflineBooking model)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> Reportlist;
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                string SearchString = Request["SearchString"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);

                string FromDates = FromDate.ToString("MMMM dd, yyyy");

                string FromDatesonlyDate = DateTime.Parse(FromDates).ToShortDateString();


                string ToDates = ToDate.ToString("MMMM dd, yyyy");

                string ToDatesonlyDate = DateTime.Parse(ToDates).ToShortDateString();
                Reportlist = BLayer.OfflineBooking.CustomerInvoiceGSTReport(SearchString, LIMIT, FromDate, ToDate);
                ViewBag.Filter = new Models.GrossMarrginReportModel();
                data.OfflineBookingList = Reportlist;
                Models.OfflineBookingModel forPager = new Models.OfflineBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    TotalRows = 0,
                    Limit = 2000,
                    currentPage = data.currentPage
                };
                if (Reportlist.Count > 0)
                {
                    forPager.TotalRows = Reportlist[0].TotalRows;
                }
                ViewBag.Filter = forPager;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls");
            Response.Charset = "";
            return View("~/Areas/Admin/Views/CustomerInvoiceGSTReport/Excel.cshtml", data.OfflineBookingList);
        }
    }
}