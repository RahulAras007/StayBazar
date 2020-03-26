using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission]
    public class ReportForSuppliersRegistrationController : Controller
    {
        private const int LIMIT = 2000;
        public ActionResult Index()
        {
            Models.ReportForSuppliersRegistrationModel data = new Models.ReportForSuppliersRegistrationModel();
            ViewBag.Filter = new Models.ReportForSuppliersRegistrationModel();
            return View(data);
        }

        [HttpPost]
        public ActionResult Filter(Models.ReportForSuppliersRegistrationModel data)
        {
            if (data.SearchString == null)
            { data.SearchString = ""; }
                
            //if (data.SearchValue == 1)
            //{
            //    data.FromDate = data.CurrentDate + TimeSpan.FromDays(7);
            //    data.ToDate = data.CurrentDate;
            //}
            //else
            //{
            //    data.FromDate = data.CurrentDate + TimeSpan.FromDays(30);
            //    data.ToDate = data.CurrentDate;
            //}
            List<CLayer.ReportForSuppliersRegistration> Reportlist = BLayer.Report.ReportForSuppliersRegistration(data.SearchValue, (int)CLayer.Role.Roles.Supplier, (int)CLayer.ObjectStatus.StatusType.Deleted, data.FromDate, data.ToDate, 0, LIMIT);
            ViewBag.Filter = new Models.ReportForSuppliersRegistrationModel();
            Models.ReportForSuppliersRegistrationModel forPager = new Models.ReportForSuppliersRegistrationModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
            //    CurrentDate = data.CurrentDate,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                Status = data.Status,
                TotalRows = 0,
                Limit = LIMIT,
                currentPage = 1
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_SuppliersRegistrationList", Reportlist);
        }

        [HttpPost]
        public ActionResult Pager(Models.ReportForSuppliersRegistrationModel data)
        {
            //if (data.SearchString == null) data.SearchString = "";
            //if (data.SearchValue == 1)
            //{
            //    data.FromDate = data.CurrentDate - TimeSpan.FromDays(7);
            //    data.ToDate = data.CurrentDate;
            //}
            //else
            //{
            //    data.FromDate = data.CurrentDate - TimeSpan.FromDays(30);
            //    data.ToDate = data.CurrentDate;
            //}
            List<CLayer.ReportForSuppliersRegistration> Reportlist = BLayer.Report.ReportForSuppliersRegistration(data.SearchValue, (int)CLayer.Role.Roles.Supplier, (int)CLayer.ObjectStatus.StatusType.Deleted, data.FromDate, data.ToDate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.ReportForSuppliersRegistrationModel();
            Models.ReportForSuppliersRegistrationModel forPager = new Models.ReportForSuppliersRegistrationModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
             //   CurrentDate = data.CurrentDate,
                FromDate=data.FromDate,
                ToDate=data.ToDate,
                Status=data.Status,
                TotalRows = 0,
                Limit = LIMIT,
                currentPage = data.currentPage
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_SuppliersRegistrationList", Reportlist);
        }

        public ActionResult ReportPdf(int SearchValue,string FromDate, string ToDate, int Limit, int currentPage)
        {
            //FromDate: pfromdate, ToDate: pToDate, Limit: pLimit, currentPage: pcurrentPage

            Models.ReportForSuppliersRegistrationModel data = new Models.ReportForSuppliersRegistrationModel();
            List<CLayer.ReportForSuppliersRegistration> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.ReportForSuppliersRegistration(SearchValue, (int)CLayer.Role.Roles.Supplier, (int)CLayer.ObjectStatus.StatusType.Deleted, fromD, toD, (currentPage - 1) * Limit, Limit);
               
                ViewBag.Filter = new Models.ReportForSuppliersRegistrationModel();
                data.ReportForSuppliersRegistrationList = Reportlist;
                Models.ReportForSuppliersRegistrationModel forPager = new Models.ReportForSuppliersRegistrationModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    TotalRows = 0,
                    Limit = LIMIT,
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
            return new ViewAsPdf("Print", data.ReportForSuppliersRegistrationList) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }
        public ActionResult ExcelReport(int SearchValue, string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.ReportForSuppliersRegistrationModel data = new Models.ReportForSuppliersRegistrationModel();
            List<CLayer.ReportForSuppliersRegistration> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.ReportForSuppliersRegistration(SearchValue, (int)CLayer.Role.Roles.Supplier, (int)CLayer.ObjectStatus.StatusType.Deleted, fromD, toD, (currentPage - 1) * Limit, Limit);

                ViewBag.Filter = new Models.ReportForSuppliersRegistrationModel();
                data.ReportForSuppliersRegistrationList = Reportlist;
                Models.ReportForSuppliersRegistrationModel forPager = new Models.ReportForSuppliersRegistrationModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    TotalRows = 0,
                    Limit = LIMIT,
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
            return View("~/Areas/Admin/Views/ReportForSuppliersRegistration/Excel.cshtml", data.ReportForSuppliersRegistrationList);
        }
        public ActionResult ReportPrint(int SearchValue, string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.ReportForSuppliersRegistrationModel data = new Models.ReportForSuppliersRegistrationModel();
            List<CLayer.ReportForSuppliersRegistration> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                //if (data.SearchString == null) data.SearchString = "";
                Reportlist = BLayer.Report.ReportForSuppliersRegistration(SearchValue, (int)CLayer.Role.Roles.Supplier, (int)CLayer.ObjectStatus.StatusType.Deleted, fromD, toD, (currentPage - 1) * Limit, Limit);
                ViewBag.Filter = new Models.ReportForSuppliersRegistrationModel();
                data.ReportForSuppliersRegistrationList = Reportlist;
                Models.ReportForSuppliersRegistrationModel forPager = new Models.ReportForSuppliersRegistrationModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    TotalRows = 0,
                    Limit = LIMIT,
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
            return View("Print", data.ReportForSuppliersRegistrationList);
        }
	}
}