using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Controllers
{
    public class BookingStatusGSTReportController : Controller
    {

        // GET: Admin/BookingStatusReport
        public ActionResult Index()
        {
            Models.BookingStatusReportModel data = new Models.BookingStatusReportModel();
            ViewBag.Filter = new Models.BookingStatusReportModel();
            if (data.FromDate >= Convert.ToDateTime("01/06/2017 00:00:00") && data.ToDate >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                data.IsGSTModel = true;
                data.BookingStatusReportList = BLayer.Report.BookingStatusReportGST(data.FromDate, data.ToDate, 0, 2000);
            }
            else
            {
                data.IsGSTModel = false;
                data.BookingStatusReportList = BLayer.Report.BookingStatusReport(data.FromDate, data.ToDate, 0, 2000);
            }

            return View(data);
        }
        [HttpPost]
        public ActionResult Filter(Models.BookingStatusReportModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.BookingStatusReport> Reportlist = new List<CLayer.BookingStatusReport>();

            if (data.FromDate >= Convert.ToDateTime("01/06/2017 00:00:00") && data.ToDate >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                Reportlist = BLayer.Report.BookingStatusReportGST(data.FromDate, data.ToDate, 0, 2000);
            }
            else
            {
                Reportlist = BLayer.Report.BookingStatusReport(data.FromDate, data.ToDate, 0, 2000);
            }

            ViewBag.Filter = new Models.BookingStatusReportModel();
            Models.BookingStatusReportModel forPager = new Models.BookingStatusReportModel()
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
        public ActionResult Pager(Models.BookingStatusReportModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.BookingStatusReport> Reportlist = new List<CLayer.BookingStatusReport>();

            if (data.FromDate >= Convert.ToDateTime("01/06/2017 00:00:00") && data.ToDate >= Convert.ToDateTime("01/06/2017 00:00:00"))
            {
                Reportlist = BLayer.Report.BookingStatusReportGST(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            }
            else
            {
                Reportlist = BLayer.Report.BookingStatusReport(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            }

            ViewBag.Filter = new Models.BookingStatusReportModel();
            Models.BookingStatusReportModel forPager = new Models.BookingStatusReportModel()
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
            Models.BookingStatusReportModel data = new Models.BookingStatusReportModel();
            DateTime fromD, toD;
            fromD = DateTime.Now;
            toD = DateTime.Now.AddDays(10);
            List<CLayer.BookingStatusReport> Reportlist;
            try
            {


                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);


                DateTime FromDates = Convert.ToDateTime(FromDate);
                DateTime ToDates = Convert.ToDateTime(ToDate);

                //if (data.SearchString == null) data.SearchString = "";


                if (fromD >= Convert.ToDateTime("01/06/2017 00:00:00") && toD >= Convert.ToDateTime("01/06/2017 00:00:00"))
                {
                    Reportlist = BLayer.Report.BookingStatusReportGST(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }
                else
                {
                    Reportlist = BLayer.Report.BookingStatusReport(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }


                ViewBag.Filter = new Models.PendingCustomerInvoiceReportModel();
                data.BookingStatusReportList = Reportlist;
                Models.BookingStatusReportModel forPager = new Models.BookingStatusReportModel()
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
                return new ViewAsPdf("PrintGST", data.BookingStatusReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
            }
            else
            {
                return new ViewAsPdf("Print", data.BookingStatusReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
            }

        }
        public ActionResult ExcelReport(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.BookingStatusReportModel data = new Models.BookingStatusReportModel();
            List<CLayer.BookingStatusReport> Reportlist;
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
                    Reportlist = BLayer.Report.BookingStatusReportGST(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }
                else
                {
                    Reportlist = BLayer.Report.BookingStatusReport(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }


                ViewBag.Filter = new Models.BookingStatusReportModel();
                data.BookingStatusReportList = Reportlist;
                Models.BookingStatusReportModel forPager = new Models.BookingStatusReportModel()
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
                return View("~/Areas/Admin/Views/BookingStatusGSTReport/ExcelGST.cshtml", data.BookingStatusReportList);
            }
            else
            {
                return View("~/Areas/Admin/Views/BookingStatusGSTReport/Excel.cshtml", data.BookingStatusReportList);
            }
        }
        public ActionResult ReportPrint(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.BookingStatusReportModel data = new Models.BookingStatusReportModel();
            List<CLayer.BookingStatusReport> Reportlist;
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
                    Reportlist = BLayer.Report.BookingStatusReportGST(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }
                else
                {
                    Reportlist = BLayer.Report.BookingStatusReport(FromDates, ToDates, (currentPage - 1) * Limit, Limit);
                }


                ViewBag.Filter = new Models.BookingStatusReportModel();
                data.BookingStatusReportList = Reportlist;
                Models.BookingStatusReportModel forPager = new Models.BookingStatusReportModel()
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
                return View("PrintGST", data.BookingStatusReportList);
            }
            else
            {
                return View("Print", data.BookingStatusReportList);
            }
        }


    }
}