using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission]
    public class CollectionReportController : Controller
    {

        public ActionResult Index()
        {
            Models.CollectionReportModel data = new Models.CollectionReportModel();
            ViewBag.Filter = new Models.CollectionReportModel();
            return View(data);
        }
        [HttpPost]
        public ActionResult Filter(Models.CollectionReportModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.CollectionReport> Reportlist = BLayer.Report.CollectionReport(data.FromDate, data.ToDate, 0, 25);
            ViewBag.Filter = new Models.GrossMarrginReportModel();
            Models.CollectionReportModel forPager = new Models.CollectionReportModel()
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
        public ActionResult Pager(Models.CollectionReportModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.CollectionReport> Reportlist = BLayer.Report.CollectionReport(data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.SupplierPaymentReportModel();
            Models.CollectionReportModel forPager = new Models.CollectionReportModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                TotalRows = 0,
                Limit =25,
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

            Models.CollectionReportModel data = new Models.CollectionReportModel();
            List<CLayer.CollectionReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.CollectionReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.SupplierPaymentReportModel();
                data.CollectionReportList = Reportlist;
                Models.CollectionReportModel forPager = new Models.CollectionReportModel()
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

              // PartialView("_Report", Reportlist);

                    data.ForPrint = true;
                    data.ForPdf = true;               
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("Print", data.CollectionReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }

        
        public ActionResult ExcelReport(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.CollectionReportModel data = new Models.CollectionReportModel();
            List<CLayer.CollectionReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.CollectionReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.SupplierPaymentReportModel();
                data.CollectionReportList = Reportlist;
                Models.CollectionReportModel forPager = new Models.CollectionReportModel()
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
            return View("~/Areas/Admin/Views/CollectionReport/Excel.cshtml", data.CollectionReportList);         
        }
        public ActionResult ReportPrint(string FromDate, string ToDate, int Limit, int currentPage)
          {
            Models.CollectionReportModel data = new Models.CollectionReportModel();
            List<CLayer.CollectionReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.CollectionReport(fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.SupplierPaymentReportModel();
                data.CollectionReportList = Reportlist;
                Models.CollectionReportModel forPager = new Models.CollectionReportModel()
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

                // PartialView("_Report", Reportlist);

                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return  View("Print", data.CollectionReportList);
        }
    }
}