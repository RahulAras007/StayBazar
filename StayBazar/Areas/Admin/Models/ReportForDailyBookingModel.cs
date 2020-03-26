using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class ReportForDailyBookingModel
    {

       // [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Current Date")]
        public DateTime CurrentDate { get; set; }

       
        public DateTime CurrentDate1 { get; set; }
        
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start { get; set; }
        public int Status { get; set; }
        public long PropertyId { get; set; }
        public bool ForPrint { get; set; }
        public bool ForPdf { get; set; }
        public List<CLayer.ReportForDailyBooking> ReportForDailyBookingList { get; set; }
        public ReportForDailyBookingModel()
        {
            CurrentDate =DateTime.Today ;
            ReportForDailyBookingList = new List<CLayer.ReportForDailyBooking>();
            ForPrint = false;
            ForPdf = false;
        }
    }
}