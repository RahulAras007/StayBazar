using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class ExternalInventoryBookingRequestModel
    {
        public string hotel_id { get; set; }
        public string query_key { get; set; }
        public string hotel_name { get; set; }
        public string hotel_des { get; set; }
        public string room_name { get; set; }
        public string roomtype_id { get; set; }
        public string room_desc { get; set; }
        public decimal final_price_at_bookingamt { get; set; }
        public string final_price_at_bookingamtcurr { get; set; }
        public decimal final_price_at_checkoutamt { get; set; }
        public string final_price_at_checkoutamtcurr { get; set; }
        public string DomainId { get; set; }
        public string RatePlanId { get; set; }
        public string PromotionId { get; set; }

        public string reservation_id { get; set; }
        public string Reference_Id { get; set; }
        public string IpAddtress { get; set; }
        public long BookingId { get; set; }
        public int BookingStatus { get; set; }
        public int ReservationStatus { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public long CustomerId { get; set; }
        public string Response { get; set; }
        public int BookingApiType { get; set; }
        public string StatusDetails { get; set; }

        public string CreateDate { get; set; }

        public enum BookingStatusenum { None = 0, Success = 1, Failure = 2, Pending = 3, Verified = 4, Cancel = 5 }
        public enum ReservationStatusenum { none = 0, Booked = 1, error = 2 }
        public enum BookingApiTypes { Maximojo = 1 }

        //Cancel 
        public long BookingExtInvReqId { get; set; }
        public string Cancellation_Number { get; set; }
        public int Cancellation_Status { get; set; }
        public string Cancelled_Date { get; set; }
        public string Cancellation_Response { get; set; }
        public enum CancellationStatus { none = 0, Success = 1, AlreadyCancelled = 2, CannotBeCancelled = 3, UnknownReference = 4, Error = 5 }



        public List<CLayer.BookingExternalInventory> ExternalInventoryBookRequestList { get; set; }


        public int currentPage { get; set; }
        public int SearchValue { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public long TotalRows { get; set; }


        public int SaveStatus { get; set; }

        public SelectList SavedStatusTypes { get; set; }

        public ExternalInventoryBookingRequestModel()
        {
            List<KeyValuePair<int, string>> objStatustypes1 = new List<KeyValuePair<int, string>>();
            objStatustypes1.Add(new KeyValuePair<int, string>(0, "All"));
            objStatustypes1.Add(new KeyValuePair<int, string>(1, "Booking Request"));
            objStatustypes1.Add(new KeyValuePair<int, string>(2, "Cancelled"));
            SavedStatusTypes = new SelectList(objStatustypes1, "Key", "Value");
          }
    }
}