using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace StayBazar.Areas.Admin.Models
{
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public long OfflineBookingId { get; set; }
        [Display(Name = "Send Mail")]
        public Boolean IsMailed { get; set; }
     //   public string ToAddress { get; set; }
        public string InvoiceNumber { get; set; }
        [Display(Name = "Invoice Date")]
        public String InvoiceDate { get; set; }
        [Display(Name = "Due Date")]
        public DateTime DueDate { get; set; }
        //public DateTime DueDate { get; set; }
        //public DateTime InvoiceDate { get; set; }
        //public double Reimbursements { get; set; }
        //public double Discounts { get; set; }
        //public double Others { get; set; }
        //public DateTime MailedDate { get; set; }
        public int InvoiceType { get; set; }
        public int Status { get; set; }
       // [Required]
        [AllowHtml]
      //  [UIHint("tinymce_full_compressed")]
        [Display(Name = "Description")]
        public string HtmlSection1 { get; set; }
     //   [Required]
        [AllowHtml]
     //   [UIHint("tinymce_full_compressed")]
        [Display(Name = "Description")]
        public string HtmlSection2 { get; set; }
     //   [Required]
        [AllowHtml]
     //   [UIHint("tinymce_full_compressed")]
        [Display(Name = "Description")]
        public string HtmlSection3 { get; set; }

         [AllowHtml]
        public string HtmlSection4 { get; set; }
        public int OPType { get; set; }
        public int ShowText { get; set; }
        //[AllowHtml]
        //public string HtmlMid { get; set; }
        public string CustomerName { get; set; }
        public long BookingId { get; set; }
        public string LoggedInUser { get; set; }
        public Invoice()
        {
            InvoiceDate = DateTime.Today.ToString("dd/MM/yyyy");
            OPType = 1; // 1 - normal save, 2- save and send mail, 3 - reset content
            ShowText = 1;
        }
    }
}