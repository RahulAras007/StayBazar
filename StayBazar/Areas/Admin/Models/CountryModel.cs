using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace StayBazar.Areas.Admin.Models
{
    public class CountryModel
    {
        public long CountryId { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "is required")]
        public string Name { get; set; }
        [Display(Name = "Is Default")]
        public bool IsDefault { get; set; }
        [Display(Name = "For Property")]
        public bool ForProperty { get; set; }
        public int Status { get; set; }
        [Display(Name = "Status")]
        public SelectList StatusList { get; set; }
        [Display(Name = "Country Code (3 letter)")]
        [Required(ErrorMessage = "is required")]
        [MaxLength(3, ErrorMessage = "It should be 3 letter code")]
        public string Code { get; set; }
        public enum StatusTypes { Enabled = 1, Disable= 2 }

        public CountryModel()
        {
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