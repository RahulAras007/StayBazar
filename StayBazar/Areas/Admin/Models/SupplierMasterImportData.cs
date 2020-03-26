using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class SupplierMasterImportData
    {
        public string supplier_name { get; set; }
        public string property_name { get; set; }
        public string full_address { get; set; }
        public string supplier_state { get; set; }
        public string supplier_country { get; set; }
        public string supplier_gst_no { get; set; }
        public string supplier_contact_person { get; set; }
        public long supplier_contact_number { get; set; }
        public string supplier_email_address { get; set; }
        public long supplier_master_code { get; set; }
        public long UserID { get; set; }
        public string supplier_PAN { get; set; }
        public string supplier_city { get; set; }
        public string import_status { get; set; }
    }
}