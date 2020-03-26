using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission(AllowAllRoles=true)]
    public class BookingListController : Controller
    {
        #region Custom Methods
        private Models.BookingListModel InitialBookData()
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            Models.BookingListModel search = new Models.BookingListModel();
            search.Bookinglist = BLayer.Bookings.GetRecentDataForAdmin(60);
            return search;
        }
        #endregion
        // GET: /BookingHistory/        
        public ActionResult Index()
        {
            try
            {                
                return View(InitialBookData());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View();
        }
        public ActionResult ResentBookingData()
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            Models.BookingListModel search = new Models.BookingListModel();
            search.Bookinglist = BLayer.Bookings.GetRecentDataForAdmin(180);
            return PartialView("~/Areas/Admin/Views/BookingList/_Recent.cshtml", search);
        }
        public ActionResult PastBookingData()
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            Models.BookingListModel search = new Models.BookingListModel();
            search.Bookinglist = BLayer.Bookings.GetRecentDataForAdmin(365);
            return PartialView("~/Areas/Admin/Views/BookingList/_Recent.cshtml", search);
        }
    }
}