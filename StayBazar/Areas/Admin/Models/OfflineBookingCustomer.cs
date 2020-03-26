using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class OfflineBookingCustomer
    {
        public long OfficeCoustomerID { get; set; }
        public long OfficeBookingID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Customer Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CustomerEmail { get; set; }

         [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Enter Customer Mobile")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string CustomerMobile { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Customer Country")]
        [Required(ErrorMessage = "Select Customer Country")]
        public int CustomerCountry { get; set; }

        [Display(Name = "Customer State")]
        [Required(ErrorMessage = "Select Customer State")]
        public int CustomerState { get; set; }

        [Display(Name = "Customer City")]
        [Required(ErrorMessage = "Select Customer City")]
        public int CustomerCity { get; set; }

        public long CoustomerID { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Customer Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Select Customer Country")]
        public int BillingCountry { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Select Customer State")]
        public int BillingState { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Select Customer City")]
        public int BillingCity { get; set; }

        [Display(Name = "Contact Person Name")]
        [Required(ErrorMessage = "Enter Contact Person's Name")]
        public string ContactPerson { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Enter Billing Customer Mobile No")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string BillingMobile { get; set; }

        [Display(Name = "PIN Code")]
        public string PinCode { get; set; }

        [Display(Name = "Office NO")]
        public string OfficeNO { get; set; }

        [Display(Name = "Guest Name")]

        public string GuestName { get; set; }


        [Display(Name = "Guest Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string GuestEmail { get; set; }


    
        [Display(Name = "Country")]
        public int BillingCountryId { get; set; }

        public SelectList BillingCountryList { get; set; }
        public SelectList BillingStateList { get; set; }
        public SelectList BillingCityList { get; set; }

    }
}