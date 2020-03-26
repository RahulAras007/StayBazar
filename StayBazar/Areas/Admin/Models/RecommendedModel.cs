using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class RecommendedModel
    {
        [Range(1,long.MaxValue,ErrorMessage="Please choose a property")]
        public long PropertyId { get; set; }
        public int Status { get; set; }
        [Required(ErrorMessage="Start date required")]
        [DataType(DataType.Date)]
     // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name="Start Date (dd/mm/yyyy)")]
        public string StartDate { get; set; }
        [Required(ErrorMessage="End date required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date (dd/mm/yyyy)")]
        public string EndDate { get; set; }
        [Range(0,100,ErrorMessage="It should be between 0 and 100")]
        public int Order { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<CLayer.Recommended> Items { get; set; }
        public SelectList StatusList { get; set; }
        public string Title { get; set; }
        public int ManageFor { get; set; }
        public int InventoryAPIType { get; set; }

        public RecommendedModel()
        {
            Items = new List<CLayer.Recommended>();
            StartDate =  DateTime.Today.ToShortDateString();
            EndDate = DateTime.Today.AddDays(1).ToShortDateString();
            List<SelectListItem> lst = new List<SelectListItem>();
            lst.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Active.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Active).ToString() });
            lst.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Disabled.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Disabled).ToString() });
            StatusList = new SelectList(lst,"Value","Text");
            Status = (int)CLayer.ObjectStatus.StatusType.Active;
            PropertyId = 0;
            Title = "None";
        }
    }
}