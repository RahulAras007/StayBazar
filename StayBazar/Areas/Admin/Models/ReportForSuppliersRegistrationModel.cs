using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class ReportForSuppliersRegistrationModel
    {
        public string SupplierName { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string City{ get; set; } 
        public string State{ get; set; }
        public string Country{ get; set; }
        public int Noofproperties { get; set; }
        public int TotalAccomodationInventory { get; set; }
        public long AllocationforStayBazar { get; set; }
        public int CurrentStatus { get; set; }

        public string PropertyName { get; set; }
        public string PropertyLocation { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public string PropertyCountry { get; set; }
        public bool ForPrint { get; set; }
        public bool ForPdf { get; set; }
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
   //     [Display(Name = "Current Date")]
   //     public DateTime CurrentDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start  { get; set; }
         public int Status { get; set; }
         public long B2BId { get; set; }
         public long PropertyId { get; set; }
        public enum SupplierSearchValue { Weekly = 1, Monthly = 2 }
        public List<CLayer.ReportForSuppliersRegistration> ReportForSuppliersRegistrationList { get; set; }
        public ReportForSuppliersRegistrationModel()
        {
            ToDate = DateTime.Today;
            FromDate = DateTime.Today;
            ReportForSuppliersRegistrationList = new List<CLayer.ReportForSuppliersRegistration>();
            ForPrint = false;
            ForPdf = false;
        }
    }
}