using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
using System.Data;

namespace StayBazar.Areas.Admin.Controllers
{

    public class CityWiseStatisticsController : Controller
    {
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            DataTable Reportlist = BLayer.Report.ReportCityWiseCount();
            //data.ReportList = Reportlist;
            data.propertyDetails = Reportlist;
            return View(data);
        }


        [Common.AdminRolePermission]
        public ActionResult ReportPdf()
        {
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            DataTable Reportlist = new DataTable();
            //List<CLayer.PropertyDetailsReport> Reportlist;
            try
            {
                Reportlist = BLayer.Report.ReportCityWiseCount();
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

        [Common.AdminRolePermission]
        public ActionResult ReportPrint()
        {
            Models.PropertyDetailsReportModel data = new Models.PropertyDetailsReportModel();
            DataTable Reportlist = new DataTable();
            //List<CLayer.PropertyDetailsReport> Reportlist;
            try
            {
                Reportlist = BLayer.Report.ReportCityWiseCount();
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
            DataTable dt = BLayer.Report.ReportCityWiseCount();
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
            return View("Index", data);
        }
    }
}