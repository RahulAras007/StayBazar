using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class InventoryModel
    {
        public long? AccommodationId { get; set; }
        public long InventoryId { get; set; }
        [Required(ErrorMessage = "is required")]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Display(Name = "Start Date")]
        public DateTime FromDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime ToDate { get; set; }
        //for list
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public List<CLayer.Inventory> InventoryList { get; set; }
        ////drop
        public SelectList Accomodationlist { get; set; }
        public SelectList Accomodations { get; set; }
        public long PropertyId { get; set; }
        public string Category { get; set; }

        //date

        public int StartMonth { get; set; }
        public int StartDay { get; set; }
        public int EndMonth { get; set; }
        public int EndDay { get; set; }

        public SelectList Months { get; set; }
        public SelectList DaysInMonth { get; set; }

        public InventoryModel()
        {
            InventoryList = new List<CLayer.Inventory>();

            //date
            List<SelectListItem> items = new List<SelectListItem>();
            DateTime temp;
            for (int i = 1; i < 13; i++)
            {
                temp = new DateTime(2014, i, 1);
                items.Add(new SelectListItem() { Text = temp.ToString("MMM"), Value = i.ToString() });
            }
            Months = new SelectList(items, "Value", "Text");

            items = new List<SelectListItem>();
            for (int i = 1; i < 32; i++)
            {
                items.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            DaysInMonth = new SelectList(items, "Value", "Text");
        }
    }
}
