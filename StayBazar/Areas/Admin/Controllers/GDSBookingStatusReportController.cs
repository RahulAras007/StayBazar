using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission]
    public class GDSBookingStatusReportController : Controller
    {
        // GET: Admin/BookingStatusReport
        public ActionResult Index()
        {
            Models.GDSBookingStatusReportModel data = new Models.GDSBookingStatusReportModel();
            ViewBag.Filter = new Models.GDSBookingStatusReportModel();
            if (data.FromDate >= Convert.ToDateTime("01/06/2017 00:00:00") && data.ToDate >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                data.IsGSTModel = true;
                data.GDSBookingStatusReportList = BLayer.Report.GDSBookingStatusReportGST(data.FromDate, data.ToDate, 0, 2000);
            }
            //else
            //{
            //    data.IsGSTModel = false;
            //    data.GDSBookingStatusReportList = BLayer.Report.BookingStatusReport(data.FromDate, data.ToDate, 0, 2000);
            //}

            return View(data);
        }
        [HttpPost]
        public ActionResult Filter(Models.GDSBookingStatusReportModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.GDSBookingStatusReport> Reportlist = new List<CLayer.GDSBookingStatusReport>();

            if (data.FromDate >= Convert.ToDateTime("01/06/2017 00:00:00") && data.ToDate >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                Reportlist = BLayer.Report.GDSBookingStatusReportGST(data.FromDate, data.ToDate, 0, 2000);
            }
            //else
            //{
            //    Reportlist = BLayer.Report.BookingStatusReport(data.FromDate, data.ToDate, 0, 2000);
            //}

            ViewBag.Filter = new Models.GDSBookingStatusReportModel();
            Models.GDSBookingStatusReportModel forPager = new Models.GDSBookingStatusReportModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                TotalRows = 0,
                Limit = 2000,
                currentPage = 1
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.Filter = forPager;

            if (data.FromDate >= Convert.ToDateTime("01/06/2017 00:00:00") && data.ToDate >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                return PartialView("_ReportGST", Reportlist);
            }
            else
            {
                return PartialView("_Report", Reportlist);
            }
        }

        [HttpPost]
        public ActionResult Pager(Models.GDSBookingStatusReportModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.GDSBookingStatusReport> Reportlist = new List<CLayer.GDSBookingStatusReport>();

            if (data.FromDate >= Convert.ToDateTime("01/06/2017 00:00:00") && data.ToDate >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                Reportlist = BLayer.Report.GDSBookingStatusReportGST(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            }
            //else
            //{
            //    Reportlist = BLayer.Report.BookingStatusReport(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            //}

            ViewBag.Filter = new Models.GDSBookingStatusReportModel();
            Models.GDSBookingStatusReportModel forPager = new Models.GDSBookingStatusReportModel()
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
            if (data.FromDate >= Convert.ToDateTime("01/06/2017 00:00:00") && data.ToDate >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                return PartialView(" _ReportGST", Reportlist);
            }
            else
            {
                return PartialView("_Report", Reportlist);
            }

        }


        public ActionResult ReportPdf(string FromDate, string ToDate, int Limit, int currentPage)
        {
            //FromDate: pfromdate, ToDate: pToDate, Limit: pLimit, currentPage: pcurrentPage
            Models.GDSBookingStatusReportModel data = new Models.GDSBookingStatusReportModel();
            DateTime fromD, toD;
            fromD = DateTime.Now;
            toD = DateTime.Now.AddDays(10);
            List<CLayer.GDSBookingStatusReport> Reportlist = null; 
            try
            {


                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);


                DateTime FromDates = Convert.ToDateTime(FromDate);
                DateTime ToDates = Convert.ToDateTime(ToDate);

                //if (data.SearchString == null) data.SearchString = "";
               

                if (fromD >= Convert.ToDateTime("01/06/2017 00:00:00") && toD >= Convert.ToDateTime("01/06/2017 00:00:00"))
                {
                    Reportlist = BLayer.Report.GDSBookingStatusReportGST(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }
                //else
                //{
                //    Reportlist = BLayer.Report.BookingStatusReport(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                //}


                ViewBag.Filter = new Models.PendingCustomerInvoiceReportModel();
                data.GDSBookingStatusReportList = Reportlist;
                Models.GDSBookingStatusReportModel forPager = new Models.GDSBookingStatusReportModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = FromDates,
                    ToDate = ToDates,
                    TotalRows = 0,
                    Limit = 2000,
                    currentPage = data.currentPage
                };
                if (Reportlist.Count > 0)
                {
                    forPager.TotalRows = Reportlist[0].TotalRows; ;
                }
                ViewBag.Filter = forPager;
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            if (fromD >= Convert.ToDateTime("01/06/2017 00:00:00") && toD >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                return new ViewAsPdf("PrintGST", data.GDSBookingStatusReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
            }
            else
            {
                return new ViewAsPdf("Print", data.GDSBookingStatusReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
            }

        }
        public ActionResult ExcelReport(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.GDSBookingStatusReportModel data = new Models.GDSBookingStatusReportModel();
            List<CLayer.GDSBookingStatusReport> Reportlist=null ;
            DateTime fromD, toD;
            fromD = DateTime.Now;
            toD = DateTime.Now.AddDays(10);
            try
            {

                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                DateTime FromDates = Convert.ToDateTime(FromDate);
                DateTime ToDates = Convert.ToDateTime(ToDate);
                //if (data.SearchString == null) data.SearchString = "";

                if (fromD >= Convert.ToDateTime("01/06/2017 00:00:00") && toD >= Convert.ToDateTime("01/06/2017 00:00:00"))
                {
                    Reportlist = BLayer.Report.GDSBookingStatusReportGST(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }
                //else
                //{
                //    Reportlist = BLayer.Report.BookingStatusReport(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                //}


                ViewBag.Filter = new Models.GDSBookingStatusReportModel();
                data.GDSBookingStatusReportList = Reportlist;
                Models.GDSBookingStatusReportModel forPager = new Models.GDSBookingStatusReportModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = FromDates,
                    ToDate = ToDates,
                    TotalRows = 0,
                    Limit = 2000,
                    currentPage = data.currentPage
                };
                if (Reportlist.Count > 0)
                {
                    forPager.TotalRows = Reportlist[0].TotalRows;
                }
                ViewBag.Filter = forPager;
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls");
            Response.Charset = "";

            if (fromD >= Convert.ToDateTime("01/06/2017 00:00:00") && toD >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                return View("~/Areas/Admin/Views/BookingStatusReport/ExcelGST.cshtml", data.GDSBookingStatusReportList);
            }
            else
            {
                return View("~/Areas/Admin/Views/BookingStatusReport/Excel.cshtml", data.GDSBookingStatusReportList);
            }
        }
        public ActionResult ReportPrint(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.GDSBookingStatusReportModel data = new Models.GDSBookingStatusReportModel();
            List<CLayer.GDSBookingStatusReport> Reportlist=null ;
            DateTime fromD, toD;
            fromD = DateTime.Now;
            toD = DateTime.Now.AddDays(10);
            try
            {

                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                DateTime FromDates = Convert.ToDateTime(FromDate);
                DateTime ToDates = Convert.ToDateTime(ToDate);
                //if (data.SearchString == null) data.SearchString = "";

                if (fromD >= Convert.ToDateTime("01/06/2017 00:00:00") && toD >= Convert.ToDateTime("01/06/2017 00:00:00"))
                {
                    Reportlist = BLayer.Report.GDSBookingStatusReportGST(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }
                //else
                //{
                //    Reportlist = BLayer.Report.BookingStatusReport(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                //}


                ViewBag.Filter = new Models.GDSBookingStatusReportModel();
                data.GDSBookingStatusReportList = Reportlist;
                Models.GDSBookingStatusReportModel forPager = new Models.GDSBookingStatusReportModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = FromDates,
                    ToDate = ToDates,
                    TotalRows = 0,
                    Limit = 2000,
                    currentPage = data.currentPage
                };
                if (Reportlist.Count > 0)
                {
                    forPager.TotalRows = Reportlist[0].TotalRows;
                }
                ViewBag.Filter = forPager;
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            if (fromD >= Convert.ToDateTime("01/06/2017 00:00:00") && toD >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                return View("PrintGST", data.GDSBookingStatusReportList);
            }
            else
            {
                return View("Print", data.GDSBookingStatusReportList);
            }
        }
    }
}