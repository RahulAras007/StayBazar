using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class StayCategoryModels
    {
        public long CategoryId { get; set; }
        [Required(ErrorMessage = "Stay Category Type Title Required")]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public StayCategoryModels() { }
    }
}