using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace StayBazar.Areas.Admin.Controllers
{
    
    public class TransactionListController : Controller
    {
        // GET: /Admin/TransactionList/ Not Verified _List
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.TransactionListModel search = new Models.TransactionListModel();
            List<CLayer.Booking> users = BLayer.Transaction.GetNotVerified((int)CLayer.ObjectStatus.BookingStatus.CheckOut, 90, 0, 25);
            search.Status = (int)CLayer.ObjectStatus.BookingStatus.CheckOut;
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
        // Not Verified _List for pager
        [HttpPost]
        public ActionResult Pager(Models.TransactionListModel data)
        {
            List<CLayer.Booking> users = BLayer.Transaction.GetNotVerified((int)CLayer.ObjectStatus.BookingStatus.CheckOut, 90, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.TransactionListModel();
            Models.TransactionListModel forPager = new Models.TransactionListModel()
            {
                Status = data.Status,
                day = 90,
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
        //Edit Status
        [HttpPost]
        public string StatusEdit(int BookingId, int Status)
        {
            try
            {
                string userid = User.Identity.GetUserId();
                long id = 0;
                long.TryParse(userid, out id);
                if (ModelState.IsValid)
                {
                    CLayer.Booking dataStatus = new CLayer.Booking()
                    {
                        BookingId = BookingId,
                        ByUserId = id,
                        Status = Status
                    };
                    BLayer.Transaction.BookingStatusChange(dataStatus);
                    ViewBag.Saved = true;
                }
                else
                { ViewBag.Saved = false; }
                return BookingId.ToString();
                // return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "0";
            }
        }
        //select _Verified List
        //[HttpPost]
        public ActionResult VerifiedView()
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            Models.TransactionListModel search = new Models.TransactionListModel();
            List<CLayer.Booking> users = BLayer.Transaction.GetVerifiedById((int)CLayer.ObjectStatus.BookingStatus.CheckOut, id, 180, 0, 25);
            search.Status = (int)CLayer.ObjectStatus.BookingStatus.Success;
            search.TotalRows = 0;
            search.Bookinglist = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return PartialView("_Verifiedlist", users);
        }
        //select _Verified List with Start dAte and End date 
        [HttpPost]
        public ActionResult FilterVeriFiedByDate(string StartDate, string EndDate)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            DateTime tda = DateTime.Today;
            Models.TransactionListModel data = new Models.TransactionListModel();
            DateTime.TryParse(StartDate, out tda);
            DateTime StartDate1 = tda;
            DateTime.TryParse(EndDate, out tda);
            DateTime EndDate1 = tda;
            List<CLayer.Booking> users = BLayer.Transaction.VerifiedByDate((int)CLayer.ObjectStatus.BookingStatus.CheckOut, id, 0, 25, StartDate1, EndDate1);
            ViewBag.Filter = new Models.TransactionListModel();
            data.Bookinglist = users;
            Models.TransactionListModel forPager = new Models.TransactionListModel()
            {
                Status = data.Status,
                day = 90,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1
            };
            if (data.Bookinglist.Count > 0)
            {
                forPager.TotalRows = data.Bookinglist[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_Verifiedlist", users);
        }
        //Verified _List for pager
        [HttpPost]
        public ActionResult PagerVerified(Models.TransactionListModel data)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);

            List<CLayer.Booking> users = BLayer.Transaction.GetVerifiedById((int)CLayer.ObjectStatus.BookingStatus.CheckOut, id, 90, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.TransactionListModel();

            data.Bookinglist = users;
            Models.TransactionListModel forPager = new Models.TransactionListModel()
            {
                Status = data.Status,
                day = 90,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_Verifiedlist", users);
        }

        public ActionResult RestoreInfo(long bookingId)
        {
            Models.TranRestoreModel tm = new Models.TranRestoreModel();
            try
            {
                if (bookingId < 1)
                {
                    tm.Message = "Invalid Booking ID";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }
                bool succ = BLayer.Bookings.CanRestore(bookingId);
                if (!succ)
                {
                    tm.Message = "The booking cannot be restored due to non-availability of rooms.";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }
                tm.BookingId = bookingId;
                tm.Failed = false;
                tm.ShowMsg = false;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                tm.Message = "System could not process the data due to an Error.";
                tm.Failed = true;
            }
            return PartialView("~/Areas/Admin/Views/TransactionList/_RestoreBox.cshtml", tm);
        }

        public ActionResult RestoreBooking(Models.TranRestoreModel data)
        {
            Models.TranRestoreModel tm = new Models.TranRestoreModel();
            try
            {
                if (data.BookingId < 1)
                {
                    tm.Message = "Invalid Booking ID";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }
                bool succ = BLayer.Bookings.CanRestore(data.BookingId);
                if (!succ)
                {
                    tm.Message = "The booking cannot be restored due to non-availability of rooms.";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }
                if (data.Amount == 0 || data.TransactionId == "" || data.PaymentId == "" || data.PaymentCode < 1)
                {
                    tm.Message = "Invalid Transaction Details.";
                    tm.Failed = true;
                    return PartialView("_RestoreBox", tm);
                }

                long payoption = BLayer.Bookings.GetPaymentoption(data.BookingId);
                if (payoption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, data.BookingId);
                }
                if (payoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, data.BookingId);
                    BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess, data.BookingId);
                }
                BLayer.Transaction.Save(new CLayer.Transaction() { BookingId = data.BookingId, Amount = data.Amount, TransactionId = data.TransactionId, PaymentId = data.PaymentId, ResponseCode = 0, TransactionType = CLayer.ObjectStatus.TransactionType.Payment, PaymentType = data.PaymentCode, DateCreated = DateTime.Today, Status = CLayer.ObjectStatus.TransactionStatus.Payment });
                tm.Failed = false;
                tm.ShowMsg = true;
                tm.Message = "Booking has been restored.";
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                tm.Message = "System could not process the data due to an Error.";
                tm.Failed = true;
            }
            return PartialView("~/Areas/Admin/Views/TransactionList/_RestoreBox.cshtml", tm);
        }


        public ActionResult CancelExternalRequest(long BookingId)
        {
            List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(BookingId);

            foreach (CLayer.BookingExternalInventory reqbook in DtBookReq)
            {
                var Book_Cancelobj = new StayBazar.Inventory.Models.MaxBooking_Cancel.RootObject
                {
                    hotel_id = reqbook.hotel_id,
                    reservation_id = reqbook.reservation_id
                };

                string ResponseCancel = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Cancel(Book_Cancelobj);

                StayBazar.Inventory.Models.MaxBooking_Cancel_Response.RootObject Bookingcanceldetails = JsonConvert.DeserializeObject<StayBazar.Inventory.Models.MaxBooking_Cancel_Response.RootObject>(ResponseCancel);

                //Saving Cancel Details of external booking request
                CLayer.BookingExternalInventory bookcandt = new CLayer.BookingExternalInventory();

                bookcandt.BookingExtInvReqId = reqbook.BookingExtInvReqId;
                bookcandt.BookingId = reqbook.BookingId;
                bookcandt.reservation_id = reqbook.reservation_id;

                if (Bookingcanceldetails.status == "Success")
                {
                    bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.Success;
                }
                else if (Bookingcanceldetails.status == "AlreadyCancelled")
                {
                    bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.AlreadyCancelled;
                }
                else if (Bookingcanceldetails.status == "CannotBeCancelled")
                {
                    bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.CannotBeCancelled;
                }
                else if (Bookingcanceldetails.status == "UnknownReference")
                {
                    bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.UnknownReference;
                }
                else if (Bookingcanceldetails.status == "Error")
                {
                    bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.Error;
                }
                else
                {
                    bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.none;
                }

                int CacelSts = bookcandt.Cancellation_Status;

                BLayer.BookingExternalInventory.UpdateCancellationStatus(reqbook.BookingExtInvReqId, CacelSts);

                bookcandt.Cancellation_Number = Bookingcanceldetails.cancellation_number;
                if (Bookingcanceldetails.cancelled_date == null) { Bookingcanceldetails.cancelled_date = Convert.ToString(DateTime.Now); }
                bookcandt.Cancelled_Date = Bookingcanceldetails.cancelled_date;
                bookcandt.Cancellation_Response = ResponseCancel;
                BLayer.BookingExternalInventory.SaveBookingCancelResponse(bookcandt);
            }
            return RedirectToAction("Index", "TransactionList", new { area = "Admin" });
        }
    }
}