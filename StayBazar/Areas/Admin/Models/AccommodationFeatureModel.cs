using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class AccommodationFeatureModel
    {
        public long AccommodationFeatureId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage="is required")]
        public string Title { get; set; }
        [Display(Name="Choose display style")]
        public string Style { get; set; }
        public bool Showfeatures { get; set; }
        public AccommodationFeatureModel()
        {
           
        }
    }
}