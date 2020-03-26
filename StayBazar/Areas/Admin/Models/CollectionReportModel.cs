using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class CollectionReportModel
    {
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
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
        public List<CLayer.CollectionReport> CollectionReportList { get; set; }
        public CollectionReportModel()
        {
            FromDate = DateTime.Today; ;
            ToDate = DateTime.Today; ;
            CollectionReportList = new List<CLayer.CollectionReport>();
            ForPrint = false;
            ForPdf = false;
        }
    }
}