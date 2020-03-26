using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class B2BModel
    {
        public long B2BId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Display(Name = "User Code")]
        public string UserCode { get; set; }
        //For Affiliate
        [Display(Name = "Company Reg.No")]
        public string CompanyRegNo { get; set; }
        [Display(Name = "Service Tax Reg.No")]
        public string ServiceTaxRegNo { get; set; }
        [Display(Name = "VAT Reg.No")]
        public string VATRegNo { get; set; }
        [Display(Name = "Max No.Of Staff")]
        public int MaximumStaff { get; set; }
        [Display(Name = "Request Status")]
        public int RequestStatus { get; set; }

        public long UserId { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "User Type")]
        public int UserType { get; set; }
        //For Supplier
        public CLayer.BankAccount BankDetails { get; set; }
        //
        // For Display in Admin side
        public List<CLayer.Address> Addresses { get; set; }
        [Display(Name = "Markup Percent")]
        public decimal MarkupPercent { get; set; }
        [Display(Name = "Commission Percent")]
        public decimal CommissionPercent { get; set; }
        [Display(Name = "Property Description")]
        public string PropertyDescription { get; set; }
        [Display(Name = "No. Of Properties available")]
        public int AvailableProperties { get; set; }

        [Display(Name = "PAN No")]
        public string PANNo { get; set; }
        [Display(Name = "Contact Designation")]
        public string ContactDesignation { get; set; }
    }
}