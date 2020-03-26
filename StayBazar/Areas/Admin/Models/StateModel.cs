using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class StateModel
    {
        public int StateId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "is required")]
        public string Name { get; set; }
    
        [Display(Name = "GST State Code")]
        [Required(ErrorMessage = "is required")]
        public string GSTStateCode { get; set; }

        public int CountryId { get; set; }
    }
}