using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Inventory.Models
{
    public class MaxBooking_Submit_WithoutPaydt
    {
        public class Customer
        {
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string phone_number { get; set; }
            public string email { get; set; }
            public string country { get; set; }
        }

        public class Party
        {
            public int adults { get; set; }
            public List<object> children { get; set; }
        }

        public class Room
        {
            public Party party { get; set; }
            public string traveler_first_name { get; set; }
            public string traveler_last_name { get; set; }
        }

        public class FinalPriceAtBooking
        {
            public double amount { get; set; }
            public string currency { get; set; }
        }

        public class FinalPriceAtCheckout
        {
            public double amount { get; set; }
            public string currency { get; set; }
        }

        public class PartnerData
        {
            public string DomainId { get; set; }
            public string HotelId { get; set; }
            public string RoomId { get; set; }
            public string RatePlanId { get; set; }
            public string PromotionId { get; set; }
        }

        public class RootObject
        {
            public string checkin_date { get; set; }
            public string checkout_date { get; set; }
            public string hotel_id { get; set; }
            public string reference_id { get; set; }
            public string ip_address { get; set; }
            public Customer customer { get; set; }
            public List<Room> rooms { get; set; }
            public FinalPriceAtBooking final_price_at_booking { get; set; }
            public FinalPriceAtCheckout final_price_at_checkout { get; set; }
            public PartnerData partner_data { get; set; }
        }

    }
}