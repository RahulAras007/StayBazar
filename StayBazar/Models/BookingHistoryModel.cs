using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class BookingHistoryModel
    {
        [Display(Name = "Status")]
        public int Status { get; set; }//CLayer.ObjectStatus.StatusType
        public long UserId { get; set; }
        public long BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public int Limit { get; set; }
        public int Day { get; set; }
        public int Type { get; set; }
        public int Searchvalue { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public enum UserBookingHistoryType { Recent = 1, History = 2 }
        public enum searchingperiod { Three = 90, Six = 180, Twelve = 364, All = 400 }
        public List<CLayer.Booking> Bookinglist { get; set; }
        public List<CLayer.Booking> CreditBookinglist { get; set; }
        public List<CLayer.Booking> OtherBookinglist { get; set; }
        public List<CLayer.Booking> pendingApprovalBookinglist { get; set; }

        public List<CLayer.Booking> ReadyForBookinglist { get; set; }
        public List<CLayer.Booking> RejectedBookinglist { get; set; }

        public List<CLayer.OfflineBooking> OtherofflineBookinglist { get; set; }
        public BookingHistoryModel Boklist { get; set; }
        public int UserTypeId { get; set; }

        //   public int HistoryType { get; set; }
        public BookingHistoryModel()
        {
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;

            Bookinglist = new List<CLayer.Booking>();
            CreditBookinglist = new List<CLayer.Booking>();
            pendingApprovalBookinglist = new List<CLayer.Booking>();
            ReadyForBookinglist=new List<CLayer.Booking>();
            RejectedBookinglist= new List<CLayer.Booking>();

        }
       
    }
    
 
    public class BookingHistoryDetailModel
    {
        public long UserId { get; set; }
        public long BookingId { get; set; }
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
}


