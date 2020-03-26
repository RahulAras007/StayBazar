using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Inventory.Models
{
    public class MaxBooking_Cancel
    {
        public class RootObject
        {
            public string hotel_id { get; set; }
            public string reservation_id { get; set; }
        }
    }
}