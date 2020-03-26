using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Controllers
{
    public class CorporateCreditBookingsReportController : Controller
    {
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.CreditBookingModel data = new Models.CreditBookingModel();
            ViewBag.Filter = new Models.CreditBookingModel();
            return View(data);
        }
        [HttpPost]
        public ActionResult Filter(Models.CreditBookingModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.CreditBookingReport> Reportlist = BLayer.Report.CorporateCreditBookingsReport(data.FromDate, data.ToDate, 0, 25);
            ViewBag.Filter = new Models.CreditBookingModel();
            Models.CreditBookingModel forPager = new Models.CreditBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
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
        public ActionResult Pager(Models.CreditBookingModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.CreditBookingReport> Reportlist = BLayer.Report.CorporateCreditBookingsReport(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.CreditBookingModel();
            Models.CreditBookingModel forPager = new Models.CreditBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
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
        [Common.AdminRolePermission]
        public ActionResult ReportPdf(string FromDate, string ToDate, int Limit, int currentPage)
        {
            //FromDate: pfromdate, ToDate: pToDate, Limit: pLimit, currentPage: pcurrentPage

            Models.CreditBookingModel data = new Models.CreditBookingModel();
            List<CLayer.CreditBookingReport> Reportlist = null;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                 Reportlist = BLayer.Report.CorporateCreditBookingsReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.CreditBookingModel();
                data.CreditBookingList = Reportlist;
                Models.CreditBookingModel forPager = new Models.CreditBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = fromD,
                    ToDate = toD,
                    TotalRows = 0,
                    Limit = 25,
                    currentPage = data.currentPage
                };
                if (Reportlist.Count > 0)
                {
                    forPager.TotalRows = Reportlist[0].TotalRows;
                }
                ViewBag.Filter = forPager;

                // PartialView("_Report", Reportlist);

                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("Print", Reportlist) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }

        [Common.AdminRolePermission]
        public ActionResult ExcelReport(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.CreditBookingModel data = new Models.CreditBookingModel();
            List<CLayer.CreditBookingReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.CorporateCreditBookingsReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.CreditBookingModel();
               
                data.CreditBookingList = Reportlist;
                Models.CreditBookingModel forPager = new Models.CreditBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
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
            return View("~/Areas/Admin/Views/CorporateCreditBookingsReport/Excel.cshtml", data.CreditBookingList);
        }
        [Common.AdminRolePermission]
        public ActionResult ReportPrint(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.CreditBookingModel data = new Models.CreditBookingModel();
            
            List<CLayer.CreditBookingReport> Reportlist = null;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.CorporateCreditBookingsReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.CreditBookingModel();
                data.CreditBookingList = Reportlist;
                Models.CreditBookingModel forPager = new Models.CreditBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = fromD,
                    ToDate = toD,
                    TotalRows = 0,
                    Limit = 25,
                    currentPage = data.currentPage
                };
                if (Reportlist.Count > 0)
                {
                    forPager.TotalRows = Reportlist[0].TotalRows;
                }
                ViewBag.Filter = forPager;

                // PartialView("_Report", Reportlist);

                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Print", Reportlist);
        }
    }
}