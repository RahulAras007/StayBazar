using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class TestimonialModel
    {
        public long TestimonialId { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage="is required")]
        public string Description { get; set; }
        [Display(Name = "Picture")]
        public string Picture { get; set; }
        public int Status { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "is required")]
        public string Title { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "is required")]
        public string Name { get; set; }
        [Display(Name = "Company")]
        public string Company { get; set; }
        [Display(Name = "Show In Widget")]
        public bool ShowInWidget { get; set; }
        public HttpPostedFileBase photo { get; set; }

        public enum StatusList { Enabled = 1, Disabled = 2 }
    }
}