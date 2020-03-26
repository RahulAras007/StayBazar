using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class RoomTypeModels
    {
        public int RoomTypeId { get; set; }
        [Required(ErrorMessage = "Room Type Title Required")]
        [StringLength(50)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        public RoomTypeModels() { }
        public string PropertyTypesAssigned {get; set;}
    }
}