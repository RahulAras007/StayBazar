using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace StayBazar.Areas.Admin.Models
{
    public class AccommodationTypeModel
    {
        public int AccommodationTypeId { get; set; }
        [Required(ErrorMessage = "Type Title Required")]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public AccommodationTypeModel() { }
        public string StayTypesAssigned {get; set;}
    }
}