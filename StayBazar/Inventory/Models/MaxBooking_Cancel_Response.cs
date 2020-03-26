using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Inventory.Models
{
    public class MaxBooking_Cancel_Response
    {
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
            public string reservation_id { get; set; }
            public string status { get; set; }
            public string cancelled_date { get; set; }
            public string cancellation_number { get; set; }
            public CustomerSupport customer_support { get; set; }
        }
    }
}