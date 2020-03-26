using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class SalutationModel
    {
        public long SalutationId { get; set; }
        [Required(ErrorMessage = "is required")]
        [StringLength(10)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public SalutationModel() { }
    }
}