using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace StayBazar.Areas.Admin.Models
{
    public class ReportDailyPropertyBookingModel
    {
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public string FromDate { get; set; }
        public string ToDate { get; set; }
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
        public long SupplierId { get; set; }
        public List<CLayer.Property> PropertyList { get; set; }
        public List<CLayer.Property> SelectedProperty { get; set; }
        public List<string> properties { get; set; }
        public List<CLayer.ReportDailyPropertyBooking> ReportList { get; set; }

        public ReportDailyPropertyBookingModel()
        {
            FromDate = DateTime.Today.AddDays(-2).ToShortDateString();
            ToDate = DateTime.Today.AddDays(-1).ToShortDateString();
            ReportList = new List<CLayer.ReportDailyPropertyBooking>();
            ForPrint = false;
            ForPdf = false;
            PropertyList = new List<CLayer.Property>();
            foreach (CLayer.Property Property in PropertyList)
            {
                PropertyList.Add(new CLayer.Property
                {
                    PropertyId = Property.PropertyId,
                    Title = Property.Title,

                });
            }
        }
    }
}