using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class ExternalInventoryBookingRequestController : Controller
    {

        // GET: /Admin/Transactions/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.ExternalInventoryBookingRequestModel search = new Models.ExternalInventoryBookingRequestModel();
            List<CLayer.BookingExternalInventory> users = BLayer.BookingExternalInventory.GetAllForSearch("", 1, 0, 25, 0);
            search.SearchString = "";
            search.SearchValue = 1;
            search.TotalRows = 0;
            search.ExternalInventoryBookRequestList = users;
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
        public ActionResult Pager(Models.ExternalInventoryBookingRequestModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.BookingExternalInventory> users = BLayer.BookingExternalInventory.GetAllForSearch(data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit, data.SaveStatus);
            ViewBag.Filter = new Models.OfflineBookingModel();
            Models.OfflineBookingModel forPager = new Models.OfflineBookingModel()
            {
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

        [HttpPost]
        public ActionResult Filter(Models.ExternalInventoryBookingRequestModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.BookingExternalInventory> users = BLayer.BookingExternalInventory.GetAllForSearch(data.SearchString, data.SearchValue, 0, 25, data.SaveStatus);
            ViewBag.Filter = new Models.OfflineBookingModel();
            Models.ExternalInventoryBookingRequestModel forPager = new Models.ExternalInventoryBookingRequestModel()
            {
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
            return PartialView("List", users);
        }

        [Common.AdminRolePermission]
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
            return RedirectToAction("Index", "ExternalInventoryBookingRequest", new { area = "Admin" });
        }


        public ActionResult CancelExternalRequestEach(long BookingExtInvReqId)
        {
            CLayer.BookingExternalInventory reqbook = BLayer.BookingExternalInventory.getAllDetailsfromExternalRequest(BookingExtInvReqId);
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
            return RedirectToAction("Index", "ExternalInventoryBookingRequest", new { area = "Admin" });
        }
    }
}
