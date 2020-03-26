using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class BookingApprovalModel
    {
        public string SearchString { get; set; }// The string to search
        public int SearchValue { get; set; }// The type of the search string
        public enum TransactionsSearchValue { Name = 1, Phone = 2, Email = 3, ConfirmationNo = 4 }
        [Display(Name = "Status")]
    
        public int Status { get; set; }//CLayer.ObjectStatus.StatusType
        public long UserId { get; set; }
        public int Booking_Approval_Id { get; set; }//CLayer.ObjectStatus.StatusType
        public long BookingId { get; set; }
        public long approver_id { get; set; }
        public DateTime CreatedDate { get; set; }
        public long approval_order { get; set; }
        public long approval_status { get; set; }
        public List<CLayer.BookingApprovals> Approvals { get; set; }


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
        public List<CLayer.Booking> pendingApprovalBookinglist { get; set; }
        public List<CLayer.Booking> ReadyForBookinglist { get; set; }
        public List<CLayer.Booking> RejectedBookinglist { get; set; }
        public List<CLayer.Booking> BookingApprovalHistory { get; set; }

        //   public int HistoryType { get; set; }
        public BookingApprovalModel()
        {
            FromDate = DateTime.Today;
            ToDate = DateTime.Today;
            Approvals = new List<CLayer.BookingApprovals>();
            pendingApprovalBookinglist = new List<CLayer.Booking>();
            ReadyForBookinglist = new List<CLayer.Booking>();
            RejectedBookinglist = new List<CLayer.Booking>();
            BookingApprovalHistory = new List<CLayer.Booking>();
        }

    }


    //public class BookingHistoryDetailModel
    //{
    //    public long UserId { get; set; }
    //    public long BookingId { get; set; }
    //    public DateTime BookingDate { get; set; }
    //    public List<CLayer.BookingDetails> BookingDetail { get; set; }
    //    public BookingHistoryDetailModel()
    //    {
    //        BookingDetail = new List<CLayer.BookingDetails>();
    //    }
    //}
    //public class BookingHistoryItem
    //{
    //    public long UserId { get; set; }
    //    public long BookingId { get; set; }
    //    public DateTime BookingDate { get; set; }
    //    public List<CLayer.BookingItem> Items { get; set; }
    //    public BookingHistoryItem()
    //    {
    //        Items = new List<CLayer.BookingItem>();
    //    }

    //}
}


