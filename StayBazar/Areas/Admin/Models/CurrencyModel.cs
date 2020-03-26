using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class CurrencyModel
    {
        public long CurrencyId { get; set; }
        [Display(Name = "Code")]
        [Required(ErrorMessage = "Currency Title required")]
        public string Title { get; set; }
        [Display(Name = "Symbol Name(fa-rupee)")]
        public string Symbol { get; set; }
        [Display(Name = "Exchange code")]
        public string Exchangecode { get; set; }
        [Display(Name = "Conversion Rate")]
       [Required(ErrorMessage = "Conversion Rate required")]
        public decimal ConversionRate { get; set; }
        [Display(Name = "Conversion Percentage")]
        [Required(ErrorMessage = "Conversion Percentage required")]
        public decimal ConversionPercentage { get; set; }
        public DateTime LastUpdate { get; set; }
        [Display(Name = "Is Default")]
        public bool IsDefault { get; set; }
        public int Status { get; set; }
        [Display(Name = "Status")]
        public SelectList StatusList { get; set; }

        public enum StatusTypes { Enabled = (int)CLayer.ObjectStatus.StatusType.Active, Hidden = (int)CLayer.ObjectStatus.StatusType.Disabled }

        public CurrencyModel() {
            ConversionRate = 1;
            Array status = Enum.GetValues(typeof(StatusTypes));
            List<SelectListItem> statusItems = new List<SelectListItem>(status.Length);
            foreach (var i in status)
            {
                statusItems.Add(new SelectListItem { Text = Enum.GetName(typeof(StatusTypes), i), Value = ((int)i).ToString() });
            }
            StatusList = new SelectList(statusItems, "Value", "Text");
        }
    }
}