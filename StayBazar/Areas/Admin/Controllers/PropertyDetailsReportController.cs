using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using System.Data;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission]
    public class PropertyDetailsReportController : Controller
    {
        public const int LIMIT = 100;

        
        public ActionResult Index()
        {
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
           //DataTable Reportlist = BLayer.Report.PropertyDetailsReport();
           // //data.ReportList = Reportlist;
           //data.propertyDetails = Reportlist;
            data.propertyDetails = null;
           return View(data);
        }

        
        public ActionResult GetData()
        {
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            data.currentPage = 1;
            data.TotalRows = 0;
            data.Limit = LIMIT;
            //DataTable reportList = BLayer.Report.PropertyDetailsReport("",0);
            DataSet reportListDS = BLayer.Report.PropertyDetailsReport_Pager(data.SearchString, data.SearchValue, LIMIT, 0);
            DataTable reportList = reportListDS.Tables[1];

            if (reportList.Rows.Count > 0 && reportList != null)
            {
                data.TotalRows = Convert.ToInt64(reportListDS.Tables[0].Rows[0]["NumberOfRows"]);
            }
            ViewBag.Filter = data;
            return View("Pdf",reportList);
        }
       
        
        public ActionResult ReportPdf(string Searchstring, int SearchValue)
        {
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            DataTable Reportlist = new DataTable();    
            //List<CLayer.PropertyDetailsReport> Reportlist;
            try
            {
                Reportlist = BLayer.Report.PropertyDetailsReport(Searchstring, SearchValue);
                //data.ReportList = Reportlist;                       
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("Print", Reportlist);
        }
        
        public ActionResult ExcelReport(string Searchstring, int SearchValue)
        {
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            //List<CLayer.PropertyDetailsReport> Reportlist;
            try
            {
                DataTable Reportlist = BLayer.Report.PropertyDetailsReport(Searchstring, SearchValue);
                //data.ReportList = Reportlist;           
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
            ViewBag.SearchString = Searchstring;
            ViewBag.SearchValue = SearchValue;
            return View("~/Areas/Admin/Views/PropertyDetailsReport/Excel.cshtml", data.ReportList);
        }
        
        public ActionResult ReportPrint(string Searchstring, int SearchValue)
        {
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            DataTable Reportlist = new DataTable();
            //List<CLayer.PropertyDetailsReport> Reportlist;
            try
            {
                Reportlist = BLayer.Report.PropertyDetailsReport(Searchstring, SearchValue);
                ViewBag.Filter = new Models.PropertyDetailsReportModel();
                //data.ReportList = Reportlist;             
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Print", Reportlist);
        }
        public ActionResult ExcelExport()
        {
            DataTable dt = BLayer.Report.PropertyDetailsReport(" ",0);
            string attachment = "attachment; filename=Report.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                tab = "";
                for (i = 0; i < dt.Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            data.propertyDetails = dt;
            return View("Index",data);
        }

        [HttpPost]
        public ActionResult Filter(Models.PropertyDetailsReportModel data)
        {
            data.currentPage = 1;
            data.TotalRows = 0;
            data.Limit = LIMIT;

            if (data.SearchString == null)
                data.SearchString = " ";
            if (data.SearchString == " ")
            {
                data.SearchValue = 0;
            }

            DataSet reportListDS = BLayer.Report.PropertyDetailsReport_Pager(data.SearchString, data.SearchValue, LIMIT, 0);
            DataTable reportList = reportListDS.Tables[1];

            if (reportList.Rows.Count > 0 && reportList != null)
            {
                data.TotalRows = Convert.ToInt64(reportListDS.Tables[0].Rows[0]["NumberOfRows"]);
            }
            ViewBag.Filter = data;

            return View("Pdf", reportList);

            //List<CLayer.Booking> users = BLayer.Transaction.GetAllCorporateCreditBookingsForSearch(data.Status, data.SearchString, data.SearchValue, 0, 25);
            //ViewBag.Filter = new Models.CreditBookingModel();
            //return PartialView("_List", users);
        }

        //[HttpPost]
        public ActionResult Pager(Models.PropertyDetailsReportModel data)
        {
            data.SearchString = data.SearchString1;
            data.SearchValue = data.SearchValue1;
            data.currentPage = data.currentPage;
            data.TotalRows = data.TotalRows;
            data.Limit = LIMIT;
            if (data.SearchString == null)
                data.SearchString = " ";
            if (data.SearchString == " ")
            {
                data.SearchValue = 0;
            }

            DataSet reportListDS = BLayer.Report.PropertyDetailsReport_Pager(data.SearchString, data.SearchValue, LIMIT, (data.currentPage - 1) * LIMIT);
            DataTable reportList = reportListDS.Tables[1];

            if (reportList.Rows.Count > 0 && reportList != null)
            {
                data.TotalRows = Convert.ToInt64(reportListDS.Tables[0].Rows[0]["NumberOfRows"]);
            }
            ViewBag.Filter = data;

            return View("Pdf", reportList);
        }

    }
}