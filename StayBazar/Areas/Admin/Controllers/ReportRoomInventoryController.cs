using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;
namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission]
    public class ReportRoomInventoryController : Controller
    {
        public ActionResult Index()
        {
            Models.ReportRoomInventoryModel data = new Models.ReportRoomInventoryModel();
            ViewBag.Filter = new Models.ReportRoomInventoryModel();
            return View(data);
        }
        [HttpPost]
        public ActionResult Filter(Models.ReportRoomInventoryModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.ReportRoomInventory> Reportlist = BLayer.Report.ReportRoomInventoryAvailability( data.SearchString, data.SearchValue,data.FromDate,data.ToDate, 0, 25);
            ViewBag.Filter = new Models.ReportRoomInventoryModel();
            Models.ReportRoomInventoryModel forPager = new Models.ReportRoomInventoryModel()
            {               
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate=data.FromDate,
                ToDate = data.ToDate,
                TotalRows = 0,
                Limit = 25,
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
        public ActionResult Pager(Models.ReportRoomInventoryModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.ReportRoomInventory> Reportlist = BLayer.Report.ReportRoomInventoryAvailability(data.SearchString, data.SearchValue, data.FromDate, data.ToDate,(data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.ReportRoomInventoryModel();
            Models.ReportRoomInventoryModel forPager = new Models.ReportRoomInventoryModel()
            {
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                FromDate = data.FromDate,
                ToDate = data.ToDate,
                TotalRows = 0,
                Limit =25,
                currentPage = data.currentPage
            };
            if (Reportlist.Count > 0)
            {
                forPager.TotalRows = Reportlist[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_Report", Reportlist);
        }

        public ActionResult ReportPdf(string SearchString,int SearchValue, string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.ReportRoomInventoryModel data = new Models.ReportRoomInventoryModel();
            List<CLayer.ReportRoomInventory> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                Reportlist = BLayer.Report.ReportRoomInventoryAvailability(SearchString, SearchValue, fromD, toD, (currentPage - 1) * Limit, Limit);

                ViewBag.Filter = new Models.ReportRoomInventoryModel();
                data.ReportRoomInventoryList = Reportlist;
                Models.ReportRoomInventoryModel forPager = new Models.ReportRoomInventoryModel()
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
            return new ViewAsPdf("Print", data.ReportRoomInventoryList) {PageOrientation=Rotativa.Options.Orientation.Landscape };
        }
        public ActionResult ExcelReport(string SearchString, int SearchValue, string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.ReportRoomInventoryModel data = new Models.ReportRoomInventoryModel();
            List<CLayer.ReportRoomInventory> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                Reportlist = BLayer.Report.ReportRoomInventoryAvailability(SearchString, SearchValue, fromD, toD, (currentPage - 1) * Limit, Limit);

                ViewBag.Filter = new Models.ReportRoomInventoryModel();
                data.ReportRoomInventoryList = Reportlist;
                Models.ReportRoomInventoryModel forPager = new Models.ReportRoomInventoryModel()
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
            return View("~/Areas/Admin/Views/ReportRoomInventory/Excel.cshtml", data.ReportRoomInventoryList);
        }
        public ActionResult ReportPrint(string SearchString,int SearchValue, string FromDate, string ToDate, int Limit, int currentPage)
        {
            Models.ReportRoomInventoryModel data = new Models.ReportRoomInventoryModel();
            List<CLayer.ReportRoomInventory> Reportlist;
            try
            {
                DateTime fromD, toD;
                fromD = DateTime.Now;
                toD = DateTime.Now.AddDays(10);
                DateTime.TryParse(FromDate, out fromD);
                DateTime.TryParse(ToDate, out toD);
                Reportlist = BLayer.Report.ReportRoomInventoryAvailability(SearchString, SearchValue, fromD, toD, (currentPage - 1) * Limit, Limit);

                ViewBag.Filter = new Models.ReportRoomInventoryModel();
                data.ReportRoomInventoryList = Reportlist;
                Models.ReportRoomInventoryModel forPager = new Models.ReportRoomInventoryModel()
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
            return View("Print", data.ReportRoomInventoryList);
        }
	}
}