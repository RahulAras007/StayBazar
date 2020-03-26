using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Controllers
{
    [Common.RoleRequired(Supplier=true)]
    public class BookingChartController : Controller
    {
        private  const int PAGE_SIZE = 50;

        public ActionResult Index()
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);//70 sumod@gmail.com
            Models.BookingHistoryModel search = new Models.BookingHistoryModel();
            List<CLayer.Booking> users = BLayer.BookingHistory.GetBookingHistoryForSupplier((int)CLayer.ObjectStatus.BookingStatus.Success, id, 90, 0, PAGE_SIZE, 1);
            search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
            search.Day = 90;
            search.Type = 1;
            search.TotalRows = 0;
            search.Bookinglist = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = PAGE_SIZE;
            search.currentPage = 1;
            ViewBag.Filter = search;

            return View(search);
        }
        [HttpPost]
        public ActionResult BookingHistory(int day)
        {
            try
            {
                if (day > 365)
                {
                    string userid = User.Identity.GetUserId();
                    long id = 0;
                    long.TryParse(userid, out id);
                    Models.BookingHistoryModel search = new Models.BookingHistoryModel();
                    List<CLayer.Booking> users = BLayer.BookingHistory.GetBookingHistoryForSupplier((int)CLayer.ObjectStatus.BookingStatus.Success, id, day, 0, PAGE_SIZE, 4);
                    search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                    search.TotalRows = 0;
                    search.Day = day;
                    search.Type = 4;
                    search.Bookinglist = users;
                    if (users.Count > 0)
                    {
                        search.TotalRows = users[0].TotalRows;
                    }
                    search.Limit = PAGE_SIZE;
                    search.currentPage = 1;
                    ViewBag.Filter = search;
                    return View("~/Views/BookingChart/_Past.cshtml", users);
                }
                else
                {
                    string userid = User.Identity.GetUserId();
                    long id = 0;
                    long.TryParse(userid, out id);
                    Models.BookingHistoryModel search = new Models.BookingHistoryModel();
                    List<CLayer.Booking> users = BLayer.BookingHistory.GetBookingHistoryForSupplier((int)CLayer.ObjectStatus.BookingStatus.Success, id, day, 0, PAGE_SIZE, 2);
                    search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                    search.TotalRows = 0;
                    search.Day = day;
                    search.Type = PAGE_SIZE;
                    search.Bookinglist = users;
                    if (users.Count > 0)
                    {
                        search.TotalRows = users[0].TotalRows;
                    }
                    search.Limit = PAGE_SIZE;
                    search.currentPage = 1;
                    ViewBag.Filter = search;
                    return View("~/Views/BookingChart/_Past.cshtml", users);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Pager(Models.BookingHistoryModel data)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            List<CLayer.Booking> users = BLayer.BookingHistory.GetBookingHistoryForSupplier((int)CLayer.ObjectStatus.BookingStatus.Success, id, data.Day, (data.currentPage - 1) * data.Limit, data.Limit, data.Type);
            ViewBag.Filter = new Models.BookingHistoryModel();
            Models.BookingHistoryModel forPager = new Models.BookingHistoryModel()
            {
                Status = data.Status,
                TotalRows = 0,
                Limit = PAGE_SIZE,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            if (data.Type == 1)
            {
                return PartialView("_Resent", users);
            }
            return PartialView("~/Views/BookingChart/_Past.cshtml", users);
        }

        [HttpPost]
        public ActionResult FillBookedByAddressSearch(long BookingId)
        {
            List<CLayer.Address> users = BLayer.BookingHistory.BookedByAddressSearch(BookingId);
            return PartialView("_AddressBookedList", users);
        }
        [HttpPost]
        public ActionResult Booking_GetBookedFor(long BookingId)
        {

            List<CLayer.Address> users = BLayer.BookingHistory.GetBookedForUser(BookingId);
            return PartialView("_AddressForBooking", users);
        }

    }
}