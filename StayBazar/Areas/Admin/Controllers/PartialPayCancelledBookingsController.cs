using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
   // [Common.AdminRolePermission(AllowAllRoles=true)]
    public class PartialPayCancelledBookingsController : Controller
    {
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.TransactionsModel search = new Models.TransactionsModel();
            List<CLayer.Booking> users = BLayer.Transaction.GetAllPartialPaymentCancelledForSearch((int)CLayer.ObjectStatus.BookingStatus.CheckOut, "", 0, 0, 25);
            search.SearchString = "";
            search.SearchValue = 1;
            search.TotalRows = 0;
            search.Bookinglist = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View(search);
        }
        [HttpPost]
        public ActionResult Pager(Models.TransactionsModel data)
        {
            if (data.SearchString == null) data.SearchString = " ";
            if (data.SearchString == " ")
            {
                data.SearchValue = 0;
            }
            List<CLayer.Booking> users = BLayer.Transaction.GetAllPartialPaymentCancelledForSearch((int)CLayer.ObjectStatus.BookingStatus.CheckOut, data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.TransactionsModel();
            Models.TransactionsModel forPager = new Models.TransactionsModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        [HttpPost]
        public ActionResult Filter(Models.TransactionsModel data)
        {
            if (data.SearchString == null)
                data.SearchString = " ";
            if (data.SearchString == " ")
            {
                data.SearchValue = 0;
            }
            List<CLayer.Booking> users = BLayer.Transaction.GetAllPartialPaymentCancelledForSearch((int)CLayer.ObjectStatus.BookingStatus.CheckOut, data.SearchString, data.SearchValue, 0, 25);
            ViewBag.Filter = new Models.TransactionsModel();
            Models.TransactionsModel forPager = new Models.TransactionsModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }

        [HttpPost]
        public ActionResult FillBookedByAddressSearch(long BookingId)
        {
            List<CLayer.Address> users = BLayer.Transaction.BookedByAddressSearch(BookingId);
            return PartialView("_AddressBookedList", users);
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        [HttpPost]
        public ActionResult Booking_GetBookedFor(long BookingId)
        {

            List<CLayer.Address> users = BLayer.Bookings.GetBookedForUser(BookingId);
            return PartialView("_AddressForBooking", users);
        }
    }
}