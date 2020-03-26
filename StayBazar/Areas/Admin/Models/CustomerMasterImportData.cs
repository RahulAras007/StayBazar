using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class CustomerMasterImportData
    {
       
        public string customer_name { get; set; }
        public string full_address { get; set; }
        public string customer_city { get; set; }
        public string customer_state { get; set; }
        public string customer_country { get; set; }
        public string customer_email { get; set; }
        public string customer_mobile { get; set; }
        public string customer_type { get; set; }
        public string customer_gst_no { get; set; }
        public string pincode { get; set; }
        public long customermaster_code { get; set; }
        public long UserID { get; set; }
        public string import_status { get; set; }
    }
}