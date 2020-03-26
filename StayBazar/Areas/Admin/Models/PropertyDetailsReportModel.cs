using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace StayBazar.Areas.Admin.Models
{
    public class PropertyDetailsReportModel
    {
        public string CurrentDate { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public string SearchString1 { get; set; }
        public int SearchValue1 { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start { get; set; }
        public int Status { get; set; }
        public long PropertyId { get; set; }
        public bool ForPrint { get; set; }
        public bool ForPdf { get; set; }
        public long SupplierId { get; set; }
        public List<CLayer.PropertyDetailsReport> ReportList { get; set; }
        public DataTable propertyDetails { get; set; }
        public enum propertyDetailsSearchValue { All = 0, Name = 1, City = 2, Location = 3, Accommodation_Category = 4, Accommodation_Type = 5 }
        public PropertyDetailsReportModel()
        {
            CurrentDate = DateTime.Today.ToShortDateString();
            ReportList = new List<CLayer.PropertyDetailsReport>();
            ForPrint = false;
            ForPdf = false;
        }

    }
}