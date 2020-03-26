using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class BookingImportData
    {
        public string date_of_booking { get; set; }
        public string customer_name { get; set; }
        public string customer_state { get; set; }
        public string property_name { get; set; }
        public string property_city { get; set; }
        public string property_state { get; set; }
        public string stay_option { get; set; }
        public string accomodation_type { get; set; }
        public string check_in { get; set; }
        public string check_out { get; set; }
        public string guest_name { get; set; }
        public int no_of_room { get; set; }
        public int no_of_nights { get; set; }
        public int total_room_nights { get; set; }
        public decimal base_buy_rate_INR { get; set; }
        public long input_sgst { get; set; }
        public long input_cgst { get; set; }
        public long input_igst { get; set; }
        public decimal total_buy_value { get; set; }
        public decimal base_sell_rate { get; set; }
        public long output_sgst { get; set; }
        public long output_cgst { get; set; }
        public long output_igst { get; set; }
        public decimal total_sale_value_including_gst { get; set; }
        public string hotel_confirmation_number { get; set; }
        public string supplier { get; set; }
        public string property_address { get; set; }
        public string property_country { get; set; }
        public string property_gst_no { get; set; }
        public string property_contact_person { get; set; }
        public string property_contact_number { get; set; }
        public string property_email_address { get; set; }
        public long booking_code { get; set; }
        public long UserID { get; set; }
        public string Itilite_booking_ID { get; set; }
        public string import_status { get; set; } 
        public  string PlaceOfSupply { get; set; }
        public  string corporate_user_id { get; set; }
        public  string customer_GST_number { get; set; }
        public  string customer_state_id { get; set; }
        public  string data_from { get; set; }
        public  string data_from_email_address { get; set; }
    }
}