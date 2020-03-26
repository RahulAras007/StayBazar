using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class UserDocumentsModel
    {
        public long UserId { get; set; }
        public int UserType { get; set; }
        public long FileId { get; set; }
        public string FilenName { get; set; }
        public long B2BId { get; set; }


        public string Name { get; set; } //b2b name

        [Display(Name = "Service Tax Reg.No")]
        public string ServiceTaxRegNo { get; set; }
        [Display(Name = "VAT Reg.No")]
        public string VATRegNo { get; set; }

        // For Bank Details for Supplier
        public long BankAccountId { get; set; }
        [Display(Name = "Account number")]
        public string AccountNumber { get; set; }
        [Display(Name = "Bank")]
        public string BankName { get; set; }
        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }
        [Display(Name = "RTGS Number")]
        public string RTGSNumber { get; set; }
        [Display(Name = "MICR Code")]
        public string MICRCode { get; set; }
        [Display(Name = "PAN No")]
        public string PANNo { get; set; }

        //
        [Display(Name = "Contact Designation")]
        public string ContactDesignation { get; set; }
        public HttpPostedFileBase ServiceTaxReg { get; set; }
        public HttpPostedFileBase VATReg { get; set; }
        public HttpPostedFileBase BusinessRegistrationCertificate { get; set; }
        public HttpPostedFileBase PANCard { get; set; }
        public HttpPostedFileBase CopyOfCheque { get; set; }
        public HttpPostedFileBase CorporateLogo { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        public SelectList AccountTypeList { get; set; }
        public UserDocumentsModel()
        {
            var list = new List<SelectListItem>
            {
        new SelectListItem{ Text="Savings Bank", Value = "Savings Bank" },
        new SelectListItem{ Text="Current", Value = "Current" }
            };

            SelectList sl = new SelectList(list, "Value", "Text");
            AccountTypeList = sl;
        }
    }
}