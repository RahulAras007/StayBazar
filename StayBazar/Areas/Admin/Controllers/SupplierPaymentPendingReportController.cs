using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission()]
    public class SupplierPaymentPendingReportController : Controller
    {
        // GET: Admin/SupplierPaymentPendingReport
        public ActionResult Index()
        {
            DateTime curdate = DateTime.Now;
            curdate = curdate.AddDays(-60);
            //  curdate = DateTime.Now;
            //  DateTime FromDate = DateTime.Now.AddDays(-30);

            DateTime ToDate = DateTime.Now;


            string FromDatesonlyDate = curdate.ToShortDateString();

            string ToDatesonlyDate = ToDate.ToShortDateString();

            ViewBag.FromDate = FromDatesonlyDate;
            ViewBag.ToDate = ToDatesonlyDate;

           DataTable dt = BLayer.OfflineBooking.SupplierPaymentPendingReport(curdate, ToDate);

            return View(dt);
        }

        public ActionResult Filter(DateTime ToDate, DateTime FromDate)
        {
            DateTime curdate = FromDate;
            //  curdate = DateTime.Now;
            //  DateTime FromDate = DateTime.Now.AddDays(-30);

            DateTime TDate =ToDate;


            ViewBag.FromDate = FromDate.ToShortDateString();
            ViewBag.ToDate = TDate.ToShortDateString();

            DataTable dt = BLayer.OfflineBooking.SupplierPaymentPendingReport(curdate, TDate);

            return View("Index",dt);
        }

        [ActionName("ExcelDownload")]
        public ActionResult ExcelReport(DateTime HiddenToDate, DateTime HiddenFromDate)
        {
            DateTime curdate = HiddenFromDate;

            DateTime ToDate = HiddenToDate;

            DataTable dt = BLayer.OfflineBooking.SupplierPaymentPendingReport(curdate, ToDate);

            return new FileStreamResult(DataTableToExcel.GetExcelStream(dt, "Report", true, null, null, null), DataTableToExcel.CONTENT_TYPE_XLSX)
            {
                FileDownloadName = "SupplierPaymentPendingReport.xlsx"
            };
        }
    }
}