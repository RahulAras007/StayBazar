using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Controllers
{
    public class ReviewController : Controller
    {
        // GET: /ReviewSave/
        public ActionResult Index()
        {
            long cid = 0;
            long.TryParse(User.Identity.GetUserId(), out cid);
            Models.ReviewModel search = new Models.ReviewModel();
            List<CLayer.Review> users = BLayer.Review.GetGetUserId(0, 25, cid, (int)CLayer.ObjectStatus.StatusType.Verified);
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
        [HttpPost]
        public ActionResult Pager(Models.ReviewModel data)
        {
            long cid = 0;
            long.TryParse(User.Identity.GetUserId(), out cid);
            Models.ReviewModel search = new Models.ReviewModel();
            List<CLayer.Review> users = BLayer.Review.GetGetUserId((data.currentPage - 1) * data.Limit, data.Limit, cid, (int)CLayer.ObjectStatus.StatusType.Verified);
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

        public ActionResult IndexSave(long? idb, long? idp)
        {//long propertyId, long BookingId
            try
            {
                long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);
                Models.ReviewModel data = new Models.ReviewModel();

                if (idb > 0)
                {
                    data.BookingId = idb.Value;
                }
                if (idp > 0)
                {
                    data.PropertyId = idp.Value;
                    if (data.PropertyId > 0)
                    {
                        CLayer.Property pro = BLayer.Property.Get(data.PropertyId);
                        if (pro != null)
                        {
                            if (pro.City != null)
                            {
                                data.AccommodationCity = pro.City;
                            }
                        }
                    }
                }
                return View("~/Views/Review/Save.cshtml", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveData(Models.ReviewModel data)
        {
            try
            {
                ViewBag.LoginError = false;
                ViewBag.GuestError = false;
                long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);
                //if (ModelState.IsValid)
                //{
                CLayer.Review d = new CLayer.Review()
                {
                    PropertyId = data.PropertyId,
                    UserId = cid,
                    BookingId = data.BookingId,
                    Rating = data.Rating,
                    Description = data.Description,
                    Status = (int)CLayer.ObjectStatus.StatusType.NotVerified,
                    // ReviewDate = DateTime.Now
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
                long idsch = BLayer.Review.Chechreviewids(d);


                if (idsch > 0)
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "Review Already saved";
                    data.message1 = "Review Already saved";
                    ViewBag.Saved = true;
                    //return RedirectToAction("IndexSave");
                }
                else
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "Waiting for verification";
                    data.message1 = "Waiting for verification";
                    ViewBag.Saved = true;
                    BLayer.Review.Save(d);
                }
                return RedirectToAction("Index", "BookingHistory");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                //return Redirect("~/Admin/ErrorPage");
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult SaveReview(Models.ReviewModel data)
        {
            try
            {
                ViewBag.LoginError = false;
                ViewBag.GuestError = false;
                long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);


                if (cid == null || cid == 0)
                {
                    if (data.BookingId > 0)
                    {
                        long Userid = BLayer.Bookings.GetBookedByUserId(data.BookingId);
                        cid = Userid;
                    }
                }
                if (data.Description == null) { data.Description = ""; }

                CLayer.Review d = new CLayer.Review()
                {
                    PropertyId = data.PropertyId,
                    UserId = cid,
                    BookingId = data.BookingId,
                    Rating = data.Rating,
                    Description = data.Description,
                    Status = (int)CLayer.ObjectStatus.StatusType.NotVerified,
                    // ReviewDate = DateTime.Now
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
                long idsch = BLayer.Review.Chechreviewids(d);


                if (idsch > 0)
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "Review Already saved";
                    data.message1 = "Review Already saved";
                    ViewBag.Saved = true;
                    //return RedirectToAction("IndexSave");
                }
                else
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "Waiting for verification";
                    data.message1 = "Waiting for verification";
                    ViewBag.Saved = true;
                    BLayer.Review.Save(d);
                }

                return RedirectToAction("ThankYou", "Review");

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                //return Redirect("~/Admin/ErrorPage");
                return RedirectToAction("Index");
            }
        }

        [AllowAnonymous]
        public ActionResult ThankYou()
        {
            return View();
        }

        public Models.BookingModel LoadValreview(long bookingId)
        {
            Models.BookingModel data = new Models.BookingModel();
            //    CLayer.Address wh = BLayer.Bookings.GetBookedByUser(bookingId);
            List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
            if (adr.Count == 0) adr.Add(new CLayer.Address());
            data.OrderedBy = adr[0];
            data.Items = BLayer.BookingItem.GetAllDetails(bookingId);
            CLayer.Booking bdata = BLayer.Bookings.GetDetails(bookingId);
            data.BookingDetails.OrderNo = bdata.OrderNo;
            data.BookingDetails.BookingDate = bdata.BookingDate;
            data.BookingDetails.City = bdata.City;
            data.Supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
            data.BookingId = bookingId;
            return data;
        }

        [AllowAnonymous]
        public ActionResult GetDetailsForReview(long BookId)
        {
            try
            {
                //CLayer.Booking data = BLayer.Bookings.GetPartialBookDetailsbyBookId(BookId);
                return View("SendMail", LoadValreview(BookId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());
        }

        [AllowAnonymous]
        public string GetReviewSendBookingList()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            try
            {
                List<string> Bids = BLayer.Review.GetReviewSendBookingList();
                result.Append(String.Join(",", Bids));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return result.ToString();
        }


        [AllowAnonymous]
        public ActionResult ReviewForm(string referenceno)
        {
            try
            {
                long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);

                Models.ReviewModel data = new Models.ReviewModel();
                if (referenceno != null && referenceno != "")
                {
                    data.BookingId = BLayer.Bookings.GetBookingIdByOrder(referenceno);
                    data.PropertyId = BLayer.Bookings.GetPropertyId(data.BookingId);
                    if (data.PropertyId > 0)
                    {
                        CLayer.Property pro = BLayer.Property.Get(data.PropertyId);
                        if (pro != null)
                        {
                            if (pro.City != null)
                            {
                                data.AccommodationCity = pro.City;
                            }
                        }
                    }
                }
                ViewBag.redirection = "Fromemail";
                return View("~/Views/Review/Save.cshtml", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

    }
}

