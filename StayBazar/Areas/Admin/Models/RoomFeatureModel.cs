using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class RoomFeatureModel
    {
        public long RoomFeatureId { get; set; }
        public int RoomTypeId { get; set; }
        [Display(Name = "Room Type")]
        public SelectList RoomType { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage="is required")]
        public string Title { get; set; }

        public RoomFeatureModel()
        {
            List<CLayer.RoomType> roomtype = BLayer.RoomType.GetAll();
            RoomType = new SelectList(roomtype, "RoomTypeId", "Title");
        }
    }
}