using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class CityModel
    {
        public int CityId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "is required")]
        public string Name { get; set; }
        public int StateId { get; set; }
        public bool ForListing { get; set; }
         [Display(Name = "KeyWords")]
        public string Keywords { get; set; }
    }
}