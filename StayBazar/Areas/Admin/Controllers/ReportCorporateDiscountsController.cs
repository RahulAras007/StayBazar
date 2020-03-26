using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;


namespace StayBazar.Areas.Admin.Controllers
{
    public class ReportCorporateDiscountsController : Controller
    {
        // GET: /Admin/ReportDailyPropertyBooking/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.CorporateDiscountsModel data = new Models.CorporateDiscountsModel();
            ViewBag.Filter = new Models.CorporateDiscountsModel();
            return View(data);
        }
        [Common.AdminRolePermission]
        public ActionResult Filter(Models.CorporateDiscountsModel data)
        {
            List<CLayer.CorporateDiscounts> Reportlist = BLayer.Report.ReportCorporateDiscounts(data.SupplierId);
            ViewBag.Filter = new Models.ReportDailyPropertyBookingModel();
            data.Suppliername = BLayer.B2B.GetBusinessName(data.SupplierId);
            data.ReportList = Reportlist;
            return PartialView("_Report", data);
        }
        [HttpPost]
        [Common.AdminRolePermission]
        public ActionResult Pager(Models.ReportDailyPropertyBookingModel data)
        {

            string pIds = "0";
            foreach (var propertylist in data.properties)
            {
                pIds += "," + propertylist.ToString();
            }
            if (data.SearchString == null) data.SearchString = "";

            DateTime fda = DateTime.Today.AddDays(-2);
            DateTime.TryParse(data.FromDate, out fda);
            DateTime tda = DateTime.Today.AddDays(-1);
            DateTime.TryParse(data.ToDate, out tda);

            List<CLayer.ReportDailyPropertyBooking> Reportlist = BLayer.Report.DailyPropertyBooking(data.SupplierId, pIds, fda, tda);
            ViewBag.Filter = new Models.ReportDailyPropertyBookingModel();
            Models.ReportDailyPropertyBookingModel forPager = new Models.ReportDailyPropertyBookingModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                SupplierId = data.SupplierId,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.NotFound = (Reportlist.Count == 0);
            ViewBag.Filter = forPager;
            return PartialView("_Report", Reportlist);
        }

        [Common.AdminRolePermission]
        public ActionResult ReportPdf(long supplierId)
        {

            Models.CorporateDiscountsModel data = new Models.CorporateDiscountsModel();
            List<CLayer.CorporateDiscounts> Reportlist;
            try
            {
                Reportlist = BLayer.Report.ReportCorporateDiscounts(supplierId);
                ViewBag.Filter = new Models.ReportDailyPropertyBookingModel();
                data.ReportList = Reportlist;
                data.Suppliername = BLayer.B2B.GetBusinessName(supplierId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("Print", data);
        }
        [Common.AdminRolePermission]
        public ActionResult ExcelReport(string fromDate, string toDate, long supplierId, long propertyId, List<string> list)
        {
            string list1 = list[0];
            Models.CorporateDiscountsModel data = new Models.CorporateDiscountsModel();
            List<CLayer.CorporateDiscounts> Reportlist;
            try
            {
                Reportlist = BLayer.Report.ReportCorporateDiscounts(supplierId);
               
                data.ReportList = Reportlist;
                data.Suppliername = BLayer.B2B.GetBusinessName(supplierId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls");
            Response.Charset = "";
            return View("~/Areas/Admin/Views/ReportCorporateDiscounts/Excel.cshtml", data);
        }
        [Common.AdminRolePermission]
        public ActionResult ReportPrint(long supplierId)
        {
           
            Models.CorporateDiscountsModel data = new Models.CorporateDiscountsModel();
            List<CLayer.CorporateDiscounts> Reportlist;
            try
            {
                Reportlist = BLayer.Report.ReportCorporateDiscounts(supplierId);
                ViewBag.Filter = new Models.ReportDailyPropertyBookingModel();
                data.ReportList = Reportlist;
                data.Suppliername = BLayer.B2B.GetBusinessName(supplierId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Print", data);
        }

        [AllowAnonymous]
        public ActionResult Email(long supplierId, long propertyId)
        {
            Models.ReportDailyPropertyBookingModel data = new Models.ReportDailyPropertyBookingModel();
            List<CLayer.ReportDailyPropertyBooking> Reportlist;
            try
            {
                DateTime fda = DateTime.Today.AddDays(-1);
                //DateTime.TryParse(data.FromDate, out fda);
                DateTime tda = DateTime.Today.AddDays(-1);
                //   DateTime.TryParse(data.ToDate, out tda);

                Reportlist = BLayer.Report.DailyPropertyBookingForEmail(supplierId, propertyId, fda, tda);
                ViewBag.Filter = new Models.ReportDailyPropertyBookingModel();
                data.ReportList = Reportlist;
                Models.ReportDailyPropertyBookingModel forPager = new Models.ReportDailyPropertyBookingModel()
                {
                    SearchString = data.SearchString,
                    SearchValue = data.SearchValue,
                    FromDate = data.FromDate,
                    ToDate = data.ToDate,
                    SupplierId = supplierId,
                    PropertyId = propertyId,
                    TotalRows = 0,
                    Limit = 25,
                    currentPage = data.currentPage
                };
                if (Reportlist.Count > 0)
                {
                    forPager.TotalRows = Reportlist[0].TotalRows;
                }
                ViewBag.NotFound = (Reportlist.Count == 0);
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

        public ActionResult GetProperties(long supplierId, Models.ReportDailyPropertyBookingModel data)
        {

            data.SelectedProperty = BLayer.B2B.GetProperties(supplierId);
            data.PropertyList = BLayer.B2B.GetProperties(supplierId);
            return View("_Filter", data);
        }
    }
}