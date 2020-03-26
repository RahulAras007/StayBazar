using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Controllers
{
     [Common.AdminRolePermission]
    public class ReportForDailyBookingsController : Controller
    {
        public ActionResult Index()
        {
            Models.ReportForDailyBookingModel data = new Models.ReportForDailyBookingModel();
            ViewBag.Filter = new Models.ReportForDailyBookingModel();
            return View(data);
        }
        [HttpPost]
        public ActionResult FilterDailyBookings(Models.ReportForDailyBookingModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.ReportForDailyBooking> Reportlist = BLayer.Report.DailyBooking( data.CurrentDate, 0, 25);
            ViewBag.Filter = new Models.ReportForDailyBookingModel();
            Models.ReportForDailyBookingModel forPager = new Models.ReportForDailyBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                CurrentDate= data.CurrentDate,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_Report", Reportlist);
        }

        [HttpPost]
        public ActionResult Pager(Models.ReportForDailyBookingModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.ReportForDailyBooking> Reportlist = BLayer.Report.DailyBooking(data.CurrentDate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.ReportForDailyBookingModel();
            Models.ReportForDailyBookingModel forPager = new Models.ReportForDailyBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                CurrentDate = data.CurrentDate,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_Report", Reportlist);
        }

        public ActionResult ReportPdf(string FromDate, int Limit, int currentPage)
        {

            Models.ReportForDailyBookingModel data = new Models.ReportForDailyBookingModel();
            List<CLayer.ReportForDailyBooking> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
                DateTime.TryParse(FromDate, out fromD);
                Reportlist = BLayer.Report.DailyBooking(fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportForDailyBookingModel();
                data.ReportForDailyBookingList = Reportlist;
                Models.ReportForDailyBookingModel forPager = new Models.ReportForDailyBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    TotalRows = 0,
                    Limit = 25,
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
            return new ViewAsPdf("Print", data.ReportForDailyBookingList);
        }
        public ActionResult ExcelReport(string FromDate, int Limit, int currentPage)
        {
            Models.ReportForDailyBookingModel data = new Models.ReportForDailyBookingModel();
            List<CLayer.ReportForDailyBooking> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
                DateTime.TryParse(FromDate, out fromD);
                Reportlist = BLayer.Report.DailyBooking(fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportForDailyBookingModel();
                data.ReportForDailyBookingList = Reportlist;
                Models.ReportForDailyBookingModel forPager = new Models.ReportForDailyBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    TotalRows = 0,
                    Limit = 25,
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
            return View("~/Areas/Admin/Views/ReportForDailyBookings/Excel.cshtml", data.ReportForDailyBookingList);
        }
        public ActionResult ReportPrint(string FromDate, int Limit, int currentPage)
        {
            Models.ReportForDailyBookingModel data = new Models.ReportForDailyBookingModel();
            List<CLayer.ReportForDailyBooking> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
                DateTime.TryParse(FromDate, out fromD);
                Reportlist = BLayer.Report.DailyBooking(fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportForDailyBookingModel();
                data.ReportForDailyBookingList = Reportlist;
                Models.ReportForDailyBookingModel forPager = new Models.ReportForDailyBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    TotalRows = 0,
                    Limit = 25,
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
            return View("Print", data.ReportForDailyBookingList);
        }
	}
}