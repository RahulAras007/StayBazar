using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class MargintrackingReportModel
    {
        public const int DATE_INTERVAL = -30;
          //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "From")]      
        public string FromDate { get; set; }
         //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "To")]
        public string ToDate { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start  { get; set; }
        public bool ForPrint { get; set; }
        public bool ForPdf { get; set; }
        public List<CLayer.MargintrackingReport> MargintrackingReportList { get; set; }
        public MargintrackingReportModel()
        {
            FromDate = DateTime.Today.AddDays(DATE_INTERVAL).ToString("dd/MM/yyyy");
            ToDate = DateTime.Today.ToString("dd/MM/yyyy");
            MargintrackingReportList = new List<CLayer.MargintrackingReport>();
            ForPrint = false;
            ForPdf = false;
            currentPage = 1;
            Limit = 2000;
        }

        public DateTime GetFromDate(string fromD)
        {
            DateTime fromDate = DateTime.Today.AddDays(DATE_INTERVAL);
            DateTime.TryParse(fromD, out fromDate);
            return fromDate;
        }
        public DateTime GetToDate(string toD)
        {
            DateTime toDate = DateTime.Today;
            DateTime.TryParse(toD, out toDate);
            return toDate;
        }
    }
}