using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace StayBazar.Areas.Admin.Models
{
    public class ReportForFailedTransactionsModel
    {
       // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime CurrentDate { get; set; }
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
        public List<CLayer.ReportForFailedTransactions> ReportList { get; set; }
        public ReportForFailedTransactionsModel()
        {
            CurrentDate = DateTime.Today; 
            ReportList = new List<CLayer.ReportForFailedTransactions>();
            ForPrint = false;
            ForPdf = false;
        }
    }
}