using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Models
{
    [Common.AdminRolePermission]
    public class ReportDailyPropertyBookingsController : Controller
    {
        
        // GET: /Admin/ReportDailyPropertyBooking/
        
        public ActionResult Index()
        {
            Models.ReportDailyPropertyBookingModel data = new Models.ReportDailyPropertyBookingModel();
            ViewBag.Filter = new Models.ReportDailyPropertyBookingModel();
            if (data.SelectedProperty == null)
            {
                data.SelectedProperty = new List<CLayer.Property>();
            }
            return View(data);
        }
        
        public ActionResult Filter(Models.ReportDailyPropertyBookingModel data, List<string> properties)
        {
            string pIds = "0";
            foreach (var propertylist in properties)
            {
                pIds +=  "," + propertylist.ToString();
            }
            if (data.SearchString == null)
                data.SearchString = "";
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
                currentPage = 1
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.NotFound = (Reportlist.Count == 0);
            ViewBag.Filter = forPager;
            return PartialView("_Report", Reportlist);
        }
        [HttpPost]
        
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

        
        public ActionResult ReportPdf(string fromDate, string toDate, long supplierId, long propertyId, List<string> list)
        {
            string list1 = list[0];
            Models.ReportDailyPropertyBookingModel data = new Models.ReportDailyPropertyBookingModel();
            List<CLayer.ReportDailyPropertyBooking> Reportlist;
            try
            {
                DateTime fda = DateTime.Today.AddDays(-2);
                DateTime.TryParse(fromDate, out fda);
                DateTime tda = DateTime.Today.AddDays(-1);
                DateTime.TryParse(toDate, out tda);

                Reportlist = BLayer.Report.DailyPropertyBooking(supplierId, list1, fda, tda);
                ViewBag.Filter = new Models.ReportDailyPropertyBookingModel();
                data.ReportList = Reportlist;

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
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("Print", data.ReportList);
        }
        
        public ActionResult ExcelReport(string fromDate, string toDate, long supplierId, long propertyId, List<string> list)
        {
            string list1 = list[0];
            Models.ReportDailyPropertyBookingModel data = new Models.ReportDailyPropertyBookingModel();
            List<CLayer.ReportDailyPropertyBooking> Reportlist;
            try
            {
                DateTime fda = DateTime.Today.AddDays(-2);
                DateTime.TryParse(fromDate, out fda);
                DateTime tda = DateTime.Today.AddDays(-1);
                DateTime.TryParse(toDate, out tda);

                Reportlist = BLayer.Report.DailyPropertyBooking(supplierId, list1, fda, tda);
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
                    ForPrint = true,

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
                //data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=report.xls");
            Response.Charset = "";
            return View("~/Areas/Admin/Views/ReportDailyPropertyBookings/Excel.cshtml", data.ReportList);
        }
        
        public ActionResult ReportPrint(string fromDate, string toDate, long supplierId, long propertyId, List<string> list)
        {
            string list1 = list[0];
            Models.ReportDailyPropertyBookingModel data = new Models.ReportDailyPropertyBookingModel();
            List<CLayer.ReportDailyPropertyBooking> Reportlist;
            try
            {
                DateTime fda = DateTime.Today.AddDays(-2);
                DateTime.TryParse(fromDate, out fda);
                DateTime tda = DateTime.Today.AddDays(-1);
                DateTime.TryParse(toDate, out tda);

                Reportlist = BLayer.Report.DailyPropertyBooking(supplierId, list1, fda, tda);
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
                    currentPage = data.currentPage,

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