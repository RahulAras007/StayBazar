using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StayBazar.Areas.Admin.Models
{
    public class OfflinePaymentModel
    {
        public List<CLayer.OfflinePayment> OfflinePayList { get; set; }      
        public SelectList StatusTrancationTypes { get; set; }
        public string SearchString { get; set; }// The string to search
        public int SearchValue { get; set; }// The type of the search string
        //Pagination
        public int Status { get; set; }//CLayer.ObjectStatus.StatusType        
        public int day { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int days { get; set; }
        [Required(ErrorMessage = "Start date required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "End date required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        public long UserId { get; set; }
        public enum OfflinePaymentSearchValue { Name = 1, Phone = 2, Email = 3, ReferenceNumber = 4 }
    }
}