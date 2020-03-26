
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class InvoiceListController : Controller
    {
        public const int ROWS_PER_PAGE = 25;
        // GET: Admin/InvoiceList
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.InvoiceListModel data = new Models.InvoiceListModel();
            data.Limit = ROWS_PER_PAGE;
            data.Start = 0;
            data.SearchItem = 0;
            data.Status = 1;
            data.TotalRows = 0;
            int totalRows = 0;
            data.SearchString = "";
            data.Bookings= BLayer.Invoice.GetAllBooking(data.Status,data.SearchString,0,data.Start,data.Limit,out totalRows);
            data.TotalRows = totalRows;
            data.SearchStatus = data.Status;
            data.SearchValue = data.SearchString;
            data.ItemSearch = data.SearchItem;
            return View(data);
        }
        [Common.AdminRolePermission]
        public ActionResult Filter(Models.InvoiceListModel data)
        {
            try
            {
                data.Limit = ROWS_PER_PAGE;
                data.Start = 0;
             //   data.SearchItem = 0;
            //    data.Stat1;us = 
                int totalRows = 0;
                data.Bookings = BLayer.Invoice.GetAllBooking(data.Status, data.SearchString, data.SearchItem, data.Start, data.Limit, out totalRows);
                data.TotalRows = totalRows;
                data.SearchStatus = data.Status;
                data.SearchValue = data.SearchString;
                data.ItemSearch = data.SearchItem;

            }catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Index", data);
        }
        [Common.AdminRolePermission]
        public ActionResult Pager(Models.InvoiceListModel data)
        {
            try
            {
                //data.SearchStatus = data.Status;
                //data.SearchValue = data.SearchString;
                //data.ItemSearch = data.SearchItem;
                int totalRows = 0;
                data.Limit = ROWS_PER_PAGE;
                if (data.Start == 1) data.Start = 0;
                int start = data.Start * ROWS_PER_PAGE - ROWS_PER_PAGE;
                if (start < 0) start = 0;
                data.Bookings = BLayer.Invoice.GetAllBooking(data.SearchStatus, data.SearchValue, data.ItemSearch, start, ROWS_PER_PAGE, out totalRows);
                data.TotalRows = totalRows;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("_List", data);
        }
    }
}