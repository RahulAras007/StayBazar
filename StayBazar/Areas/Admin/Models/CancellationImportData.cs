using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class CancellationImportData
    {
        public string date_of_cancelation { get; set; }
        public string customer_name { get; set; }
        public string customer_state { get; set; }
        public string property_name { get; set; }
        public string property_city { get; set; }
        public string property_state { get; set; }
        public string accomodation_type { get; set; }
        public string room_category_type { get; set; }
        public string check_in { get; set; }
        public string check_out { get; set; }
        public string guest_name { get; set; }
        public long cancellation_code { get; set; }
        public long UserID { get; set; }
        public long Itilite_booking_ID { get; set; }
    }
}