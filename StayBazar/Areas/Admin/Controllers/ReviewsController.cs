using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{

    public class ReviewsController : Controller
    {
        //
        // GET: /Admin/Reviews/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            long cid = 0;
            long.TryParse(User.Identity.GetUserId(), out cid);
            Models.ReviewsModel search = new Models.ReviewsModel();
            List<CLayer.Review> users = BLayer.Review.GetAll(0, 25);
            search.TotalRows = 0;
            search.ReviewList = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View(search);
        }
        //Pager Waiting list
        [HttpPost]
        public ActionResult PagerWaiting(Models.ReviewsModel data)
        {
            long cid = 0;
            long.TryParse(User.Identity.GetUserId(), out cid);
            Models.ReviewsModel search = new Models.ReviewsModel();
            List<CLayer.Review> users = BLayer.Review.RecommendedByStatus("", (int)CLayer.ObjectStatus.StatusType.NotVerified, false, (data.currentPage - 1) * data.Limit, data.Limit);
            search.TotalRows = 0;
            search.ReviewList = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = data.currentPage;
            ViewBag.Filter = search;
            return View(search);
        }
        //fillter All
        [HttpPost]
        public ActionResult FilterAllName(Models.ReviewsModel data)
        {
            if ((int)data.ViewType == 0)
            {
                int i = (int)Models.ReviewsModel.ReviewSearchValues.All;
                if (data.SearchValue == i)
                {
                    Models.ReviewsModel search = new Models.ReviewsModel();
                    List<CLayer.Review> users = BLayer.Review.GetAll(0, 25);
                    search.TotalRows = 0;
                    search.ReviewList = users;
                    if (users.Count > 0)
                    {
                        search.TotalRows = users[0].TotalRows;
                    }
                    search.Limit = 25;
                    search.currentPage = data.currentPage;
                    ViewBag.Filter = search;
                    return PartialView("_AllReviewsList", search);
                }
                else
                {
                    Models.ReviewsModel search = new Models.ReviewsModel();
                    List<CLayer.Review> users = BLayer.Review.AllNameByFilter(data.SearchString, data.SearchValue, (int)data.ViewType, 0, 25);
                    search.TotalRows = 0;
                    search.ReviewList = users;
                    if (users.Count > 0)
                    {
                        search.TotalRows = users[0].TotalRows;
                    }
                    search.Limit = 25;
                    search.currentPage = data.currentPage;
                    ViewBag.Filter = search;
                    return PartialView("_AllReviewsList", search);
                }
            }
            else
            {
                Models.ReviewsModel search = new Models.ReviewsModel();
                List<CLayer.Review> users = BLayer.Review.AllNameByFilter(data.SearchString, data.SearchValue, (int)data.ViewType, 0, 25);
                search.TotalRows = 0;
                search.ReviewList = users;
                if (users.Count > 0)
                {
                    search.TotalRows = users[0].TotalRows;
                }
                search.Limit = 25;
                search.currentPage = data.currentPage;
                ViewBag.Filter = search;
                return PartialView("_AllReviewsList", search);
            }
        }
        [HttpPost]
        public ActionResult PagerAllReviewsList(Models.ReviewsModel data)
        {
            Models.ReviewsModel search = new Models.ReviewsModel();
            List<CLayer.Review> users = BLayer.Review.AllNameByFilter(data.SearchString, data.SearchValue, (int)data.ViewType, (data.currentPage - 1) * data.Limit, data.Limit);
            search.TotalRows = 0;
            search.ReviewList = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = data.currentPage;
            ViewBag.Filter = search;
            return PartialView("_AllReviewsList", search);
        }
        //Pagination Recommended 
        [HttpPost]
        public ActionResult PagerRecommendedList(Models.ReviewsModel data)
        {
            Models.ReviewsModel search = new Models.ReviewsModel();
            List<CLayer.Review> users = BLayer.Review.RecommendedByStatus("", data.SearchValue, true, (data.currentPage - 1) * data.Limit, data.Limit);
            search.TotalRows = 0;
            search.ReviewList = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = data.currentPage;
            ViewBag.Filter = search;
            return PartialView("_RecommendedList", search);
        }
        [Common.AdminRolePermission]
        public ActionResult Delete(long propertyId, long UserId, long BookingId)
        {
            BLayer.Review.Delete(propertyId, UserId, BookingId);
            return RedirectToAction("Index");
        }

        public ActionResult Details(long propertyId, long UserId, long BookingId)
        {
            Models.ReviewsModel search = new Models.ReviewsModel();
            CLayer.Review users = BLayer.Review.Get(propertyId, UserId, BookingId);
            if (users != null)
            {
                search = new Models.ReviewsModel
               {
                   PropertyId = users.PropertyId,
                   UserId = users.UserId,
                   BookingId = users.BookingId,
                   Rating = users.Rating,
                   Status = users.Status,
                   ReviewDate = users.ReviewDate,
                   FirstName = users.FirstName,
                   LastName = users.LastName,
                   Title = users.Title,
                   Location = users.Location,
                   Address = users.Address,
                   Description = users.Description,
                   UpdatedBy = users.UpdatedBy,
                   UpdatedDate = users.UpdatedDate,
                   Recommended = users.Recommended,
                   AccommodationCity = users.AccommodationCity,
                   Accessibility = users.Accessibility,
                   Easiness = users.Easiness,
                   CleanlinessBed = users.CleanlinessBed,
                   CleanlinessBath = users.CleanlinessBath,
                   SleepQuality = users.SleepQuality,
                   Staffbehave = users.Staffbehave,
                   OverallExp = users.OverallExp,
                   Considerfornext = users.Considerfornext
               };
            }
            return PartialView("_Details", search);
        }

        [HttpPost]
        public string Verify(Models.ReviewsModel data)
        {
            try
            {
                long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);
                CLayer.Review d = new CLayer.Review()
                {
                    PropertyId = data.PropertyId,
                    UserId = data.UserId,
                    BookingId = data.BookingId,
                    Description = data.Description,
                    //Rating=data.Rating,
                    UpdatedBy = cid,
                    Status = data.Status,
                    UpdatedDate = DateTime.Now,
                    Recommended = data.Recommended,
                    AccommodationCity = data.AccommodationCity,
                    Accessibility = data.Accessibility,
                    Easiness = data.Easiness,
                    CleanlinessBed = data.CleanlinessBed,
                    CleanlinessBath = data.CleanlinessBath,
                    SleepQuality = data.SleepQuality,
                    Staffbehave = data.Staffbehave,
                    OverallExp = data.OverallExp,
                    Considerfornext = data.Considerfornext
                };
                BLayer.Review.Verified(d);
                return "Ok";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "Try";
            }
        }


    }
}