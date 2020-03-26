using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
namespace StayBazar.Areas.Admin.Controllers
{
    
    public class ReportDailyInventoryAndBookingController : Controller
    {
        //
        // GET: /Admin/ReportDailyInventoryAndBooking/
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Index()
        {
            Models.ReportDailyInventoryAndBookingModel data = new Models.ReportDailyInventoryAndBookingModel();
            ViewBag.Filter = new Models.ReportDailyInventoryAndBookingModel();
            return View(data);
        }
        [HttpPost]
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Filter(Models.ReportDailyInventoryAndBookingModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            DateTime tda = DateTime.Today;
            DateTime.TryParse(data.CurrentDate, out tda);
            List<CLayer.ReportDailyInventoryAndBooking> Reportlist = BLayer.Report.DailyInventoryAndBooking(data.SupplierId,tda, 0, 25);
            ViewBag.Filter = new Models.ReportDailyInventoryAndBookingModel();
            Models.ReportDailyInventoryAndBookingModel forPager = new Models.ReportDailyInventoryAndBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                CurrentDate = data.CurrentDate,
                SupplierId=data.SupplierId,
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
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Pager(Models.ReportDailyInventoryAndBookingModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            DateTime tda = DateTime.Today;
            DateTime.TryParse(data.CurrentDate, out tda);
            List<CLayer.ReportDailyInventoryAndBooking> Reportlist = BLayer.Report.DailyInventoryAndBooking(data.SupplierId,tda, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.ReportDailyInventoryAndBookingModel();
            Models.ReportDailyInventoryAndBookingModel forPager = new Models.ReportDailyInventoryAndBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                CurrentDate = data.CurrentDate,
                SupplierId = data.SupplierId,
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

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult ReportPdf(string FromDate, int Limit, int currentPage,long SupplierId)
        {
            
            Models.ReportDailyInventoryAndBookingModel data = new Models.ReportDailyInventoryAndBookingModel();
            List<CLayer.ReportDailyInventoryAndBooking>  Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
                DateTime.TryParse(FromDate, out fromD);
                Reportlist = BLayer.Report.DailyInventoryAndBooking(SupplierId,fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportDailyInventoryAndBookingModel();
                data.ReportList = Reportlist;
                Models.ReportDailyInventoryAndBookingModel forPager = new Models.ReportDailyInventoryAndBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    SupplierId = data.SupplierId,
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
            return new ViewAsPdf("Print", data.ReportList);
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult ExcelReport(string FromDate, int Limit, int currentPage, long SupplierId)
        {
            Models.ReportDailyInventoryAndBookingModel data = new Models.ReportDailyInventoryAndBookingModel();
            List<CLayer.ReportDailyInventoryAndBooking> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
                DateTime.TryParse(FromDate, out fromD);
                Reportlist = BLayer.Report.DailyInventoryAndBooking(SupplierId, fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportDailyInventoryAndBookingModel();
                data.ReportList = Reportlist;
                Models.ReportDailyInventoryAndBookingModel forPager = new Models.ReportDailyInventoryAndBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    SupplierId = data.SupplierId,
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
            return View("~/Areas/Admin/Views/ReportDailyInventoryAndBooking/Excel.cshtml", data.ReportList);
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult ReportPrint(string FromDate, int Limit, int currentPage,long SupplierId)
        {
            Models.ReportDailyInventoryAndBookingModel data = new Models.ReportDailyInventoryAndBookingModel();
            List<CLayer.ReportDailyInventoryAndBooking> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
                DateTime.TryParse(FromDate, out fromD);
                Reportlist = BLayer.Report.DailyInventoryAndBooking(SupplierId,fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportDailyInventoryAndBookingModel();
                data.ReportList = Reportlist;
                Models.ReportDailyInventoryAndBookingModel forPager = new Models.ReportDailyInventoryAndBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    SupplierId = data.SupplierId,
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
            return View("Print", data.ReportList);
        }

        [AllowAnonymous]
        public ActionResult Email(long SupplierId)
        {
            Models.ReportDailyInventoryAndBookingModel data = new Models.ReportDailyInventoryAndBookingModel();
            List<CLayer.ReportDailyInventoryAndBooking> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Today.AddDays(-1); //yesterday's report
                Reportlist = BLayer.Report.DailyInventoryAndBooking(SupplierId, fromD, 0, 2000);
                ViewBag.Filter = new Models.ReportDailyInventoryAndBookingModel();
                data.ReportList = Reportlist;
                Models.ReportDailyInventoryAndBookingModel forPager = new Models.ReportDailyInventoryAndBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    SupplierId = data.SupplierId,
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
            return View("Print", data.ReportList);
        }
	}
}