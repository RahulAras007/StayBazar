using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SupplierInvoicePendingReportController : Controller
    {
        public const int LIMIT = 500;
        public const int NOOF_DAYS = -30;
        // GET: Admin/SupplierInvoicePendingReport

        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            try
            {
                model.FromDate = DateTime.Now.AddDays(NOOF_DAYS);
                model.ToDate = DateTime.Now;
                model.OfflineBookingList = BLayer.SupplierInvoice.getSupplierInvoicePendings_Report(0, LIMIT, model.FromDate, model.ToDate);
                model.currentPage = 1;
                model.Limit = LIMIT;
                if (model.OfflineBookingList.Count() > 0)
                {
                    model.TotalRows = model.OfflineBookingList[0].TotalRows;
                }
              
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(model);
        }

        [Common.AdminRolePermission]
        public ActionResult Filter(Models.OfflineBookingModel model)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);

                data.OfflineBookingList = BLayer.SupplierInvoice.getSupplierInvoicePendings_Report(0, LIMIT, FromDate, ToDate);
                data.currentPage = 1;
                data.Limit = LIMIT;
                if (data.OfflineBookingList.Count() > 0)
                {
                    data.TotalRows = data.OfflineBookingList[0].TotalRows;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoicePendingReport/_ReportList.cshtml", data);
        }

        public ActionResult Pager(Models.OfflineBookingModel model)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);
                
                data.FromDate = FromDate;
                data.ToDate = ToDate;
                data.OfflineBookingList = BLayer.SupplierInvoice.getSupplierInvoicePendings_Report((model.currentPage - 1) * model.Limit, model.Limit,FromDate, ToDate);
                data.currentPage = model.currentPage;
                data.Limit = model.Limit;
                if(data.OfflineBookingList.Count()>0)
                {
                    data.TotalRows = data.OfflineBookingList[0].TotalRows;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoicePendingReport/_ReportList.cshtml", data);
        }

        [Common.AdminRolePermission]
        public ActionResult PDFView(Models.OfflineBookingModel model)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);
                data.OfflineBookingList = BLayer.SupplierInvoice.getSupplierInvoicePendings_Report(0, 0, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("PDF", data) { PageOrientation = Rotativa.Options.Orientation.Landscape };
        }

        [Common.AdminRolePermission]
        public ActionResult ExcelView(Models.OfflineBookingModel model)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);
                data.OfflineBookingList = BLayer.SupplierInvoice.getSupplierInvoicePendings_Report(0, 0, FromDate,ToDate);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=SupplierInvoicePendingReport.xls");
            Response.Charset = "";
            return View("~/Areas/Admin/Views/SupplierInvoicePendingReport/Excel.cshtml", data);
        }

        [Common.AdminRolePermission]
        public ActionResult ReportPrint(Models.OfflineBookingModel model)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);
                data.OfflineBookingList = BLayer.SupplierInvoice.getSupplierInvoicePendings_Report(0, 0,FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("PDF", data);
        }

    }
}