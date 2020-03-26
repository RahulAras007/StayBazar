using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Inventory.Models
{
    public class MaxBookingAvailibility_Response
    {

        //public class Party
        //{
        //    public int adults { get; set; }
        //}

        //public class FinalPriceAtBooking
        //{
        //    public decimal amount { get; set; }
        //    public string currency { get; set; }
        //}

        //public class FinalPriceAtCheckout
        //{
        //    public decimal amount { get; set; }
        //    public string currency { get; set; }
        //}

        //public class PartnerData
        //{
        //    public string hotelId { get; set; }
        //    public string domainId { get; set; }
        //    public string roomId { get; set; }
        //    public string ratePlanId { get; set; }
        //    public string promotionId { get; set; }
        //}

        //public class Price
        //{
        //    public decimal amount { get; set; }
        //    public string currency { get; set; }
        //}

        //public class LineItem
        //{
        //    public Price price { get; set; }
        //    public string type { get; set; }
        //    public bool paid_at_checkout { get; set; }
        //}

        //public class RoomTypesArray
        //{
        //    public string name { get; set; }
        //    public FinalPriceAtBooking final_price_at_booking { get; set; }
        //    public FinalPriceAtCheckout final_price_at_checkout { get; set; }
        //    public string description { get; set; }
        //    public PartnerData partner_data { get; set; }
        //    public List<LineItem> line_items { get; set; }
        //    public List<string> amenities { get; set; }
        //    public string refundable { get; set; }
        //    public string cancellation_policy { get; set; }
        //}

        //public class HotelDetails
        //{
        //    public string name { get; set; }
        //    public string street { get; set; }
        //    public string city { get; set; }
        //    public string postal_code { get; set; }
        //    public string state { get; set; }
        //    public string country { get; set; }
        //    public double latitude { get; set; }
        //    public double longitude { get; set; }
        //    public string desc { get; set; }
        //    public string phone { get; set; }
        //    public List<string> amenities { get; set; }
        //    public string checkinout_policy { get; set; }
        //}

        //public class PhoneNumber
        //{
        //    public string contact { get; set; }
        //    public string description { get; set; }
        //}

        //public class CustomerSupport
        //{
        //    public List<PhoneNumber> phone_numbers { get; set; }
        //}

        //public class RootObject
        //{
        //    public string hotel_id { get; set; }
        //    public string start_date { get; set; }
        //    public string end_date { get; set; }
        //    public List<Party> party { get; set; }
        //    public string query_key { get; set; }
        //    public List<RoomTypesArray> room_types_array { get; set; }
        //    public HotelDetails hotel_details { get; set; }
        //    public List<string> accepted_credit_cards { get; set; }
        //    public CustomerSupport customer_support { get; set; }
        //}

        public class HotelroomsDetails
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
        }


        public class Party
        {
            public int adults { get; set; }
            public List<int> children { get; set; }
        }

        public class FinalPriceAtBooking
        {
            public decimal amount { get; set; }
            public string currency { get; set; }
        }

        public class FinalPriceAtCheckout
        {
            public decimal amount { get; set; }
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

        public class Price
        {
            public double amount { get; set; }
            public string currency { get; set; }
        }

        public class LineItem
        {
            public Price price { get; set; }
            public string type { get; set; }
            public bool paid_at_checkout { get; set; }
            public string description { get; set; }
        }

        public class Item
        {
            public double daysOut { get; set; }
            public double amount { get; set; }
            public string amountType { get; set; }
        }

        public class CancellationPolicy
        {
            public string id { get; set; }
            public string text { get; set; }
            public List<Item> items { get; set; }
        }

        public class RoomTypesArray
        {
            public string name { get; set; }
            public FinalPriceAtBooking final_price_at_booking { get; set; }
            public FinalPriceAtCheckout final_price_at_checkout { get; set; }
            public string description { get; set; }
            public PartnerData partner_data { get; set; }
            public List<LineItem> line_items { get; set; }
            public List<object> amenities { get; set; }
            public string refundable { get; set; }
            public CancellationPolicy cancellation_policy { get; set; }
        }

        public class HotelDetails
        {
            public string name { get; set; }
            public string street { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string postal_code { get; set; }
            public string country { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string phone { get; set; }
            public string desc { get; set; }
            public List<string> images { get; set; }
            public List<string> amenities { get; set; }
            public string checkinout_policy { get; set; }
        }

        public class PhoneNumber
        {
            public string contact { get; set; }
            public string description { get; set; }
        }

        public class Email
        {
            public string contact { get; set; }
            public string description { get; set; }
        }

        public class CustomerSupport
        {
            public List<PhoneNumber> phone_numbers { get; set; }
            public List<Email> emails { get; set; }
        }

        public class RootObject
        {
            public string hotel_id { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
            public List<Party> party { get; set; }
            public string query_key { get; set; }
            public List<RoomTypesArray> room_types_array { get; set; }
            public HotelDetails hotel_details { get; set; }
            public List<string> accepted_credit_cards { get; set; }
            public CustomerSupport customer_support { get; set; }
        }


    }
}