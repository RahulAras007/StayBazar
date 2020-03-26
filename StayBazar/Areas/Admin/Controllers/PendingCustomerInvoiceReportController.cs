using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class PendingCustomerInvoiceReportController : Controller
    {
        // GET: Admin/Marginracking
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.PendingCustomerInvoiceReportModel data = new Models.PendingCustomerInvoiceReportModel();
            data.PendingCustomerInvoiceReportList = BLayer.Report.PendingCustomerInvoiceReport(data.FromDate, data.ToDate, 0, 2000);
            ViewBag.Filter = new Models.PendingCustomerInvoiceReportModel();
            return View(data);
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult Filter(Models.PendingCustomerInvoiceReportModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.PendingCustomerInvoiceReport> Reportlist = BLayer.Report.PendingCustomerInvoiceReport(data.FromDate, data.ToDate, 0, 2000);
            ViewBag.Filter = new Models.MargintrackingReportModel();
            Models.PendingCustomerInvoiceReportModel forPager = new Models.PendingCustomerInvoiceReportModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                TotalRows = 0,
                Limit = 5000,
                currentPage = 1
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_Report", Reportlist);
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult Pager(Models.PendingCustomerInvoiceReportModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.PendingCustomerInvoiceReport> Reportlist = BLayer.Report.PendingCustomerInvoiceReport(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.PendingCustomerInvoiceReportModel();
            Models.PendingCustomerInvoiceReportModel forPager = new Models.PendingCustomerInvoiceReportModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                TotalRows = 0,
                Limit = 5000,
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
            Models.PendingCustomerInvoiceReportModel data = new Models.PendingCustomerInvoiceReportModel();
            List<CLayer.PendingCustomerInvoiceReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.PendingCustomerInvoiceReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.PendingCustomerInvoiceReportModel();
                data.PendingCustomerInvoiceReportList = Reportlist;
                Models.PendingCustomerInvoiceReportModel forPager = new Models.PendingCustomerInvoiceReportModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    TotalRows = 0,
                    Limit = 5000,
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

            return new ViewAsPdf("Print", data.PendingCustomerInvoiceReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }
        [Common.AdminRolePermission]
        public ActionResult ExcelReport(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.PendingCustomerInvoiceReportModel data = new Models.PendingCustomerInvoiceReportModel();
            List<CLayer.PendingCustomerInvoiceReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.PendingCustomerInvoiceReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.PendingCustomerInvoiceReportModel();
                data.PendingCustomerInvoiceReportList = Reportlist;
                Models.PendingCustomerInvoiceReportModel forPager = new Models.PendingCustomerInvoiceReportModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    TotalRows = 0,
                    Limit = 5000,
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
            return View("~/Areas/Admin/Views/PendingCustomerInvoiceReport/Excel.cshtml", data.PendingCustomerInvoiceReportList);
        }
        [Common.AdminRolePermission]
        public ActionResult ReportPrint(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.PendingCustomerInvoiceReportModel data = new Models.PendingCustomerInvoiceReportModel();
            List<CLayer.PendingCustomerInvoiceReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.PendingCustomerInvoiceReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.PendingCustomerInvoiceReportModel();
                data.PendingCustomerInvoiceReportList = Reportlist;
                Models.PendingCustomerInvoiceReportModel forPager = new Models.PendingCustomerInvoiceReportModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    TotalRows = 0,
                    Limit = 5000,
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
            return View("Print", data.PendingCustomerInvoiceReportList);
        }
    }
}