using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission(AllowAllRoles=true)]
    public class GrossMarrginReportController : Controller
    {

        public ActionResult Index()
        {
            Models.GrossMarrginReportModel data = new Models.GrossMarrginReportModel();
            ViewBag.Filter = new Models.GrossMarrginReportModel();
            return View(data);
        }
        [HttpPost]
        public ActionResult Filter(Models.GrossMarrginReportModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.GrossMarrginReport> Reportlist = BLayer.Report.GrossMarrginReport(data.FromDate, data.ToDate, 0,2000);
            ViewBag.Filter = new Models.GrossMarrginReportModel();
            Models.GrossMarrginReportModel forPager = new Models.GrossMarrginReportModel()
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
            return PartialView("_Report", Reportlist);
        }

        [HttpPost]
        public ActionResult Pager(Models.GrossMarrginReportModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.GrossMarrginReport> Reportlist = BLayer.Report.GrossMarrginReport(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.SupplierPaymentReportModel();
            Models.GrossMarrginReportModel forPager = new Models.GrossMarrginReportModel()
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
            return PartialView("_Report", Reportlist);
        }


        public ActionResult ReportPdf(string FromDate, string ToDate, int Limit, int currentPage)
        {
            //FromDate: pfromdate, ToDate: pToDate, Limit: pLimit, currentPage: pcurrentPage
            Models.GrossMarrginReportModel data = new Models.GrossMarrginReportModel();
            List<CLayer.GrossMarrginReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.GrossMarrginReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.GrossMarrginReportModel();
                data.GrossMarrginReportList = Reportlist;
                Models.GrossMarrginReportModel forPager = new Models.GrossMarrginReportModel()
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
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
          
            return new ViewAsPdf("Print", data.GrossMarrginReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }
        public ActionResult ExcelReport(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.GrossMarrginReportModel data = new Models.GrossMarrginReportModel();
            List<CLayer.GrossMarrginReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.GrossMarrginReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.GrossMarrginReportModel();
                data.GrossMarrginReportList = Reportlist;
                Models.GrossMarrginReportModel forPager = new Models.GrossMarrginReportModel()
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
            return View("~/Areas/Admin/Views/GrossMarrginReport/Excel.cshtml", data.GrossMarrginReportList);
        }
        public ActionResult ReportPrint(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.GrossMarrginReportModel data = new Models.GrossMarrginReportModel();
            List<CLayer.GrossMarrginReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.GrossMarrginReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.GrossMarrginReportModel();
                data.GrossMarrginReportList = Reportlist;
                Models.GrossMarrginReportModel forPager = new Models.GrossMarrginReportModel()
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
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Print", data.GrossMarrginReportList);
        }
    }
}