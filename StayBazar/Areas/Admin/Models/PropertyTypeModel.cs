using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class PropertyTypeModel
    {
        public long PropertyTypeId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Propert Type Title Required")]
        public string Title { get; set; }

        public PropertyTypeModel() { }
    }
}