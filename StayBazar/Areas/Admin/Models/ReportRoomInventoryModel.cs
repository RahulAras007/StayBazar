using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace StayBazar.Areas.Admin.Models
{
    public class ReportRoomInventoryModel
    {
        public string SupplierName { get; set; }
        public string PropertyName { get; set; }
        public string AddressOfProperty { get; set; }
        public string AccomodationType	{ get; set; }
        public DateTime AccomodationDate { get; set; }
        public long TotalInventoryAllocated { get; set; }
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "From")]
        public DateTime FromDate { get; set; }
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "To")]
        public DateTime ToDate { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start  { get; set; }
        public bool ForPrint { get; set; }
        public bool ForPdf { get; set; }
        public enum RoomInventorySearchValue { City = 1, Supplier = 2, Property = 3}
        public List<CLayer.ReportRoomInventory> ReportRoomInventoryList { get; set; }
        public ReportRoomInventoryModel()
        {
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;
            ReportRoomInventoryList = new List<CLayer.ReportRoomInventory>();
            ForPrint = false;
            ForPdf = false;
        }
    }
}   
       