using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class GDSBookingStatusReportModel
    {
               //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "From")]      
        public DateTime FromDate { get; set; }
         //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
        public List<CLayer.GDSBookingStatusReport> GDSBookingStatusReportList { get; set; }
        public bool IsGSTModel { get; set; } 
        public GDSBookingStatusReportModel()
        {
            IsGSTModel = false;
            DateTime curdate = DateTime.Now;
            curdate = curdate.AddDays(-30);
            FromDate = curdate;
            ToDate = DateTime.Today;
            GDSBookingStatusReportList = new List<CLayer.GDSBookingStatusReport>();
            ForPrint = false;
            ForPdf = false;
            currentPage = 1;
            Limit = 2000;

        }
    }
}