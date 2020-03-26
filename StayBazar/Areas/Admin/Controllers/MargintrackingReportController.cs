using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class MargintrackingReportController : Controller
    {
        // GET: Admin/Marginracking
        public ActionResult Index()
        {
            string email = User.Identity.GetUserName();
            long LoginUserid = BLayer.User.GetUserId(email);

            Models.MargintrackingReportModel data = new Models.MargintrackingReportModel();
            data.MargintrackingReportList = BLayer.Report.MargintrackingReport(data.GetFromDate(data.FromDate), data.GetToDate(data.ToDate), 0, 2000, LoginUserid);
 
            ViewBag.Filter = new Models.MargintrackingReportModel();
            return View(data);
        }
        [HttpPost]
        public ActionResult Filter(Models.MargintrackingReportModel data)
        {
            string email = User.Identity.GetUserName();
            long LoginUserid = BLayer.User.GetUserId(email);

            if (data.SearchString == null)
                data.SearchString = "";
            
            List<CLayer.MargintrackingReport> Reportlist = BLayer.Report.MargintrackingReport(data.GetFromDate(data.FromDate), data.GetToDate(data.ToDate), 0, 2000, LoginUserid);
            ViewBag.Filter = new Models.MargintrackingReportModel();
            Models.MargintrackingReportModel forPager = new Models.MargintrackingReportModel()
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
        public ActionResult Pager(Models.MargintrackingReportModel data)
        {
            string email = User.Identity.GetUserName();
            long LoginUserid = BLayer.User.GetUserId(email);
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.MargintrackingReport> Reportlist = BLayer.Report.MargintrackingReport(data.GetFromDate(data.FromDate), data.GetToDate(data.ToDate), (data.currentPage - 1) * data.Limit, data.Limit, LoginUserid);
            ViewBag.Filter = new Models.MargintrackingReportModel();
            Models.MargintrackingReportModel forPager = new Models.MargintrackingReportModel()
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
            Models.MargintrackingReportModel data = new Models.MargintrackingReportModel();
            List<CLayer.MargintrackingReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                string email = User.Identity.GetUserName();
                long LoginUserid = BLayer.User.GetUserId(email);
                Reportlist = BLayer.Report.MargintrackingReport(fromD, toD, (currentPage - 1) * Limit, Limit, LoginUserid);
                ViewBag.Filter = new Models.MargintrackingReportModel();
                data.MargintrackingReportList = Reportlist;
                Models.MargintrackingReportModel forPager = new Models.MargintrackingReportModel()
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

            return new ViewAsPdf("Print", data.MargintrackingReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }
        public ActionResult ExcelReport(string FromDate, string ToDate, int Limit, int currentPage)
        {
            string email = User.Identity.GetUserName();
            long LoginUserid = BLayer.User.GetUserId(email);

            Models.MargintrackingReportModel data = new Models.MargintrackingReportModel();
            List<CLayer.MargintrackingReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.MargintrackingReport(fromD, toD, (currentPage - 1) * Limit, Limit, LoginUserid);
                ViewBag.Filter = new Models.MargintrackingReportModel();
                data.MargintrackingReportList = Reportlist;
                Models.MargintrackingReportModel forPager = new Models.MargintrackingReportModel()
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
            return View("~/Areas/Admin/Views/MargintrackingReport/Excel.cshtml", data.MargintrackingReportList);
        }
        public ActionResult ReportPrint(string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.MargintrackingReportModel data = new Models.MargintrackingReportModel();
            List<CLayer.MargintrackingReport> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                string email = User.Identity.GetUserName();
                long LoginUserid = BLayer.User.GetUserId(email);
                Reportlist = BLayer.Report.MargintrackingReport(fromD, toD, (currentPage - 1) * Limit, Limit, LoginUserid);
                ViewBag.Filter = new Models.MargintrackingReportModel();
                data.MargintrackingReportList = Reportlist;
                Models.MargintrackingReportModel forPager = new Models.MargintrackingReportModel()
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
            return View("Print", data.MargintrackingReportList);
        }
    }
}