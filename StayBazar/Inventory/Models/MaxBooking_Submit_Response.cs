using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Inventory.Models
{
    public class MaxBooking_Submit_Response
    {
        public class Hotel
        {
            public string name { get; set; }
            public string street { get; set; }
            public string city { get; set; }
            public string postal_code { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public string desc { get; set; }
            public string phone { get; set; }
            public string checkinout_policy { get; set; }
        }

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

        public class Receipt
        {
            public List<LineItem> line_items { get; set; }
            public FinalPriceAtBooking final_price_at_booking { get; set; }
            public FinalPriceAtCheckout final_price_at_checkout { get; set; }
        }

        public class Reservation
        {
            public string reservation_id { get; set; }
            public string status { get; set; }
            public string checkin_date { get; set; }
            public string checkout_date { get; set; }
            public string hotel_id { get; set; }
            public Hotel hotel { get; set; }
            public Customer customer { get; set; }
            public List<Room> rooms { get; set; }
            public string legal_text { get; set; }
            public string comments { get; set; }
            public Receipt receipt { get; set; }
        }

        public class PhoneNumber
        {
            public string contact { get; set; }
            public string description { get; set; }
        }

        public class CustomerSupport
        {
            public List<PhoneNumber> phone_numbers { get; set; }
        }

        public class RootObject
        {
            public string reference_id { get; set; }
            public string status { get; set; }
            public Reservation reservation { get; set; }
            public CustomerSupport customer_support { get; set; }
        }
    }
}