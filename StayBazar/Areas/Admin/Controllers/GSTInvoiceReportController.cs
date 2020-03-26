using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class GSTInvoiceReportController : Controller
    {
        public const int LIMIT = 500;
        // GET: Admin/GSTInvoiceReport
        public ActionResult Index()
        {
            DateTime curdate = DateTime.Now;
            //curdate = curdate.AddDays(-30);
            //  DateTime FromDate = DateTime.Now.AddDays(-30);

            DateTime ToDate = DateTime.Now;


            string FromDates = curdate.ToString("MMMM dd, yyyy");

            string FromDatesonlyDate = DateTime.Parse(FromDates).ToShortDateString();


            string ToDates = ToDate.ToString("MMMM dd, yyyy");

            string ToDatesonlyDate = DateTime.Parse(ToDates).ToShortDateString();

            ViewBag.FromDate = FromDatesonlyDate;
            ViewBag.ToDate = ToDatesonlyDate;
            string SearchString = "";
            string email = User.Identity.GetUserName();
            long LoginUserid = BLayer.User.GetUserId(email);

            DataTable users = BLayer.OfflineBooking.GSTInvoiceReport(SearchString, LIMIT, curdate, ToDate, LoginUserid);

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
            //    List<CLayer.OfflineBooking> data = BLayer.OfflineBooking.GSTInvoiceReport();
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
            string email = User.Identity.GetUserName();
            long LoginUserid = BLayer.User.GetUserId(email);

            DataTable data = BLayer.OfflineBooking.GSTInvoiceReport(SearchString, LIMIT, FromDate, ToDate, LoginUserid);
            return new ViewAsPdf("PDFView", data) { PageOrientation = Rotativa.Options.Orientation.Landscape };
            //return View("~/Areas/Admin/Views/SupplierInvoiceReport/_ReportList.cshtml", data);
        }



        [HttpPost]
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
            string email = User.Identity.GetUserName();
            long LoginUserid = BLayer.User.GetUserId(email);

            DataTable  data = BLayer.OfflineBooking.GSTInvoiceReport(SearchString, LIMIT, FromDate, ToDate, LoginUserid);


            return View("Index", data);
        }

        public ActionResult ExcelReport(CLayer.OfflineBooking model)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            DataTable Reportlist = new DataTable();
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

                string email = User.Identity.GetUserName();
                long LoginUserid = BLayer.User.GetUserId(email);

                Reportlist = BLayer.OfflineBooking.GSTInvoiceReport(SearchString, LIMIT, FromDate, ToDate, LoginUserid);
                ViewBag.Filter = new Models.GrossMarrginReportModel();
              //  data.OfflineBookingList = Reportlist;
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
                //if (Reportlist.Count > 0)
                //{
                //    forPager.TotalRows = Reportlist[0].TotalRows;
                //}
                ViewBag.Filter = forPager;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls");
            Response.Charset = "";
            return View("~/Areas/Admin/Views/GSTInvoiceReport/Excel.cshtml", Reportlist);
        }
    }
}