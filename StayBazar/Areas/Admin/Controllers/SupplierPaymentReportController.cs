using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission]
    public class SupplierPaymentReportController : Controller
    {
        
        public ActionResult Index()
        {
            Models.SupplierPaymentReportModel data = new Models.SupplierPaymentReportModel();
            ViewBag.Filter = new Models.SupplierPaymentReportModel();
            return View(data);
        }
        [HttpPost]
        public ActionResult Filter(Models.SupplierPaymentReportModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.SupplierPaymentReport> Reportlist = BLayer.Report.SupplierPaymentReport(data.FromDate, data.ToDate, 0, 25);
            ViewBag.Filter = new Models.ReportForDailyBookingModel();
            Models.SupplierPaymentReportModel forPager = new Models.SupplierPaymentReportModel()
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
        public ActionResult Pager(Models.SupplierPaymentReportModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.SupplierPaymentReport> Reportlist = BLayer.Report.SupplierPaymentReport(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.SupplierPaymentReportModel();
            Models.SupplierPaymentReportModel forPager = new Models.SupplierPaymentReportModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate=data.ToDate,
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



        public ActionResult ReportPdf(string FromDate, string ToDate, int Limit, int currentPage)
        {          
            Models.SupplierPaymentReportModel data = new Models.SupplierPaymentReportModel();
            List<CLayer.SupplierPaymentReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.SupplierPaymentReport(fromD, toD, (currentPage - 1) * Limit, Limit);

                ViewBag.Filter = new Models.SupplierPaymentReportModel();
                data.SupplierPaymentReportList = Reportlist;
                Models.SupplierPaymentReportModel forPager = new Models.SupplierPaymentReportModel()
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
            return new ViewAsPdf("Print", data.SupplierPaymentReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }
        public ActionResult ExcelReport(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.SupplierPaymentReportModel data = new Models.SupplierPaymentReportModel();
            List<CLayer.SupplierPaymentReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.SupplierPaymentReport(fromD, toD, (currentPage - 1) * Limit, Limit);

                ViewBag.Filter = new Models.SupplierPaymentReportModel();
                data.SupplierPaymentReportList = Reportlist;
                Models.SupplierPaymentReportModel forPager = new Models.SupplierPaymentReportModel()
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
            return View("~/Areas/Admin/Views/SupplierPaymentReport/Excel.cshtml", data.SupplierPaymentReportList);
        }
        public ActionResult ReportPrint( string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.SupplierPaymentReportModel data = new Models.SupplierPaymentReportModel();
            List<CLayer.SupplierPaymentReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.SupplierPaymentReport(fromD, toD, (currentPage - 1) * Limit, Limit);

                ViewBag.Filter = new Models.SupplierPaymentReportModel();
                data.SupplierPaymentReportList = Reportlist;
                Models.SupplierPaymentReportModel forPager = new Models.SupplierPaymentReportModel()
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
            return View("Print", data.SupplierPaymentReportList);
        }
    }
}