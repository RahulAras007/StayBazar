using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
namespace StayBazar.Areas.Admin.Controllers
{
    
    public class ReportForFailedTransactionsController : Controller
    {
        //
        // GET: /Admin/ReportForFailedTransactions/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.ReportForFailedTransactionsModel data = new Models.ReportForFailedTransactionsModel();
            ViewBag.Filter = new Models.ReportForFailedTransactionsModel();
            return View(data);
            //(int)CLayer.ObjectStatus.BookingStatus.CheckOut
            //(int)CLayer.ObjectStatus.BookingStatus.Failed
        }

        [HttpPost]
        [Common.AdminRolePermission]
        public ActionResult Filter(Models.ReportForFailedTransactionsModel data)
        {
            if (data.SearchString == null)
            { data.SearchString = ""; }
            List<CLayer.ReportForFailedTransactions> Reportlist = BLayer.Report.FailedTransation((int)CLayer.ObjectStatus.BookingStatus.CheckOut, (int)CLayer.ObjectStatus.BookingStatus.Failed, data.CurrentDate, 0, 2000);
            ViewBag.Filter = new Models.ReportForFailedTransactionsModel();
            Models.ReportForFailedTransactionsModel forPager = new Models.ReportForFailedTransactionsModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                CurrentDate = data.CurrentDate,
                Status = data.Status,
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
        [Common.AdminRolePermission]
        public ActionResult Pager(Models.ReportForFailedTransactionsModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.ReportForFailedTransactions> Reportlist = BLayer.Report.FailedTransation((int)CLayer.ObjectStatus.BookingStatus.CheckOut, (int)CLayer.ObjectStatus.BookingStatus.Failed, data.CurrentDate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.ReportForFailedTransactionsModel();
            Models.ReportForFailedTransactionsModel forPager = new Models.ReportForFailedTransactionsModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                CurrentDate = data.CurrentDate,
                Status = data.Status,
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

        [Common.AdminRolePermission]
        public ActionResult ReportPdf(string FromDate, int Limit, int currentPage)
        {  
            Models.ReportForFailedTransactionsModel data = new Models.ReportForFailedTransactionsModel();
            List<CLayer.ReportForFailedTransactions> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;            
                DateTime.TryParse(FromDate, out fromD);            
                Reportlist = BLayer.Report.FailedTransation((int)CLayer.ObjectStatus.BookingStatus.CheckOut, (int)CLayer.ObjectStatus.BookingStatus.Failed, fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportForFailedTransactionsModel();
                data.ReportList = Reportlist;
                Models.ReportForFailedTransactionsModel forPager = new Models.ReportForFailedTransactionsModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    Status = data.Status,
                    TotalRows = 0,
                    Limit = 2000,
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
            return new ViewAsPdf("Print", data.ReportList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }
        [Common.AdminRolePermission]
        public ActionResult ExcelReport(string FromDate, int Limit, int currentPage)
        {
            Models.ReportForFailedTransactionsModel data = new Models.ReportForFailedTransactionsModel();
            List<CLayer.ReportForFailedTransactions> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
                DateTime.TryParse(FromDate, out fromD);
                Reportlist = BLayer.Report.FailedTransation((int)CLayer.ObjectStatus.BookingStatus.CheckOut, (int)CLayer.ObjectStatus.BookingStatus.Failed, fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportForFailedTransactionsModel();
                data.ReportList = Reportlist;
                Models.ReportForFailedTransactionsModel forPager = new Models.ReportForFailedTransactionsModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    Status = data.Status,
                    TotalRows = 0,
                    Limit = 2000,
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
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls");
            Response.Charset = "";
            return View("~/Areas/Admin/Views/ReportForFailedTransactions/Excel.cshtml", data.ReportList);
        }
        [Common.AdminRolePermission]
        public ActionResult ReportPrint(string FromDate, int Limit, int currentPage)
        {
            Models.ReportForFailedTransactionsModel data = new Models.ReportForFailedTransactionsModel();
            List<CLayer.ReportForFailedTransactions> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
                DateTime.TryParse(FromDate, out fromD);
                Reportlist = BLayer.Report.FailedTransation((int)CLayer.ObjectStatus.BookingStatus.CheckOut, (int)CLayer.ObjectStatus.BookingStatus.Failed, fromD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportForFailedTransactionsModel();
                data.ReportList = Reportlist;
                Models.ReportForFailedTransactionsModel forPager = new Models.ReportForFailedTransactionsModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    Status = data.Status,
                    TotalRows = 0,
                    Limit = 2000,
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
            return View("Print", data.ReportList);
        }
        [AllowAnonymous]
        public ActionResult Email()
        {
            Models.ReportForFailedTransactionsModel data = new Models.ReportForFailedTransactionsModel();
            List<CLayer.ReportForFailedTransactions> Reportlist;
            try
            {
                DateTime fromD;
                fromD = DateTime.Now;
               
                Reportlist = BLayer.Report.FailedTransation((int)CLayer.ObjectStatus.BookingStatus.CheckOut, (int)CLayer.ObjectStatus.BookingStatus.Failed, fromD, 0, 2000);
                ViewBag.Filter = new Models.ReportForFailedTransactionsModel();
                data.ReportList = Reportlist;
                Models.ReportForFailedTransactionsModel forPager = new Models.ReportForFailedTransactionsModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    CurrentDate = data.CurrentDate,
                    Status = data.Status,
                    TotalRows = 0,
                    Limit = 2000,
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
            return View("_FailedList", data.ReportList);
        }
	}
}