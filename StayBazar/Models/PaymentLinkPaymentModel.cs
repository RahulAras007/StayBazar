using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Models
{
    public class PaymentLinkPaymentModel
    {
        public long UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = " is required")]
        public string FirstName { get; set; }
        public List<CLayer.OfflineBooking> OfflineBookingList { get; set; }
        public Guid PaymentGuid { get; set; }
        public decimal GrandTotal { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        public int CityId { get; set; }

        [Required]
        [Display(Name = "State")]
        public int State { get; set; }

        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Required]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Pin Code")]
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = " is required")]
        [Display(Name = "Address Line")]
        public string Address { get; set; }
    }

}