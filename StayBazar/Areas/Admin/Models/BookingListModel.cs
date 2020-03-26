using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class BookingListModel
    {
        [Display(Name = "Status")]
        public int Status { get; set; }//CLayer.ObjectStatus.StatusType
        public long UserId { get; set; }
        public long BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int Limit { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public enum UserBookingHistoryType { Recent = 1, Past= 2 }
        public enum searchingperiod { Three= 90, Six = 180, Twelve = 364 }
        public List<CLayer.Booking> Bookinglist { get; set; }
        public  BookingListModel  BookingList{get;set;}
        public string ActiveTab { get; set; }
        public BookingListModel()
        {
            Bookinglist = new List<CLayer.Booking>();
        }
    }
      public class BookingHistoryDetailModel
    {
        public long UserId { get; set; }
        public long  BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public List<CLayer.BookingDetails> BookingDetail { get; set; }      
        public BookingHistoryDetailModel()
        {
            BookingDetail = new List<CLayer.BookingDetails>();
        }
    }
    public class BookingHistoryItem
     {
         public long UserId { get; set; }
         public long BookingId { get; set; }
         public DateTime BookingDate { get; set; }
         public List<CLayer.BookingItem> Items { get; set; }
        public BookingHistoryItem()
        {
            Items = new List<CLayer.BookingItem>();
          }

      }
    public class BookingChartModel
    {
        public List<CLayer.BookingDetails> Items { get; set; }

        public BookingChartModel()
        {
            Items = new List<CLayer.BookingDetails>();
        }
    }
}