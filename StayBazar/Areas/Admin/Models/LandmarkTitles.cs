using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class LandmarkTitlesModel
    {
        public int LandmarkTitleId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Landmark Title is required")]
        public string LandmarkTitle { get; set; }

        public LandmarkTitlesModel() { }
    }
}