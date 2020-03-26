using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SupplierInvoiceReportController : Controller
    {
        public const int LIMIT = 500;
        public const int NOOF_DAYS = -30;
        // GET: Admin/SupplierInvoiceReport
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {
                model.FromDate = DateTime.Now.AddDays(NOOF_DAYS);
                model.ToDate = DateTime.Today;
                model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList_Report(0, LIMIT,model.FromDate,model.ToDate);
                model.currentPage = 1;
                model.Limit = LIMIT;
                if (model.SupplierInvList.Count() > 0)
                {
                    model.TotalRows = model.SupplierInvList[0].TotalRows;
                }
                model.FromDate = DateTime.Now;
                model.ToDate = DateTime.Now.AddDays(30);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(model);
        }

        [Common.AdminRolePermission]
        public ActionResult Filter(Models.SuppierInvoiceModel model)
        {
            Models.SuppierInvoiceModel data = new Models.SuppierInvoiceModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);

                data.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList_Report(0, LIMIT, FromDate, ToDate);
                data.currentPage = 1;
                data.Limit = LIMIT;
                if (data.SupplierInvList.Count() > 0)
                {
                    data.TotalRows = data.SupplierInvList[0].TotalRows;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceReport/_ReportList.cshtml", data);
        }

        [Common.AdminRolePermission]
        public ActionResult Pager(Models.SuppierInvoiceModel model)
        {
            Models.SuppierInvoiceModel data = new Models.SuppierInvoiceModel();
            try
            {
                data.FromDate = model.FromDate1;
                data.ToDate = model.ToDate1;
                data.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList_Report((model.currentPage - 1) * model.Limit, model.Limit, model.FromDate, model.ToDate);
                data.currentPage = model.currentPage;
                data.Limit = model.Limit;
                if (data.SupplierInvList.Count() > 0)
                {
                    data.TotalRows = data.SupplierInvList[0].TotalRows;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceReport/_ReportList.cshtml", data);
        }

        [Common.AdminRolePermission]
        public ActionResult PDFView(Models.SuppierInvoiceModel model)
        {
            Models.SuppierInvoiceModel data = new Models.SuppierInvoiceModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);

                data.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList_Report(0,0, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("PDF", data) { PageOrientation = Rotativa.Options.Orientation.Landscape };
            //return View("~/Areas/Admin/Views/SupplierInvoiceReport/_ReportList.cshtml", data);
        }

        [Common.AdminRolePermission]
        public ActionResult ExcelView(Models.SuppierInvoiceModel model)
        {
            Models.SuppierInvoiceModel data = new Models.SuppierInvoiceModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);
                data.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList_Report(0,0, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("content-disposition", "attachment; filename=SupplierInvoiceReport.xls");
            Response.Charset = "";
            return View("~/Areas/Admin/Views/SupplierInvoiceReport/Excel.cshtml", data);
            //return View("~/Areas/Admin/Views/SupplierInvoiceReport/_ReportList.cshtml", data);
        }

        [Common.AdminRolePermission]
        public ActionResult ReportPrint(Models.SuppierInvoiceModel model)
        {
            Models.SuppierInvoiceModel data = new Models.SuppierInvoiceModel();
            try
            {
                string date1 = Request["FromDate"];
                string date2 = Request["ToDate"];

                DateTime FromDate = Convert.ToDateTime(date1);
                DateTime ToDate = Convert.ToDateTime(date2);

                data.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList_Report(0, 0, FromDate, ToDate);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("PDF", data);
        }

    }
}