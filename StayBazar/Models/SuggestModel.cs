using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class SuggestModel
    {
        public long InfoId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "is required")]
        public string Name { get; set; }
        [Display(Name = "Location")]
        [Required(ErrorMessage = "is required")]
        public string Location { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "is required")]
        public string City { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "Country")]
        public SelectList Country { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "is required")]
        public string Address { get; set; }
        [Display(Name = "Phone")]
        [Required(ErrorMessage = "is required")]
        [Phone(ErrorMessage = "valid phone number required")]
        public string Phone { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "valid email address required")]
        public string Email { get; set; }

        public DateTime SuggestionDate { get; set; }
        public int Status { get; set; }
        public enum StatusTypes { NotViewed = 0, Viewed = 1, Verified = 2, Rejected = 3, Deleted = 4 }

        public SuggestModel()
        {
            List<CLayer.Country> countries = BLayer.Country.GetAllForProperty();
            Country = new SelectList(countries, "CountryId", "Name");
        }
    }
}