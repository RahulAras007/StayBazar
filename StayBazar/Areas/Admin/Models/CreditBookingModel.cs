using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class CreditBookingModel
    {

        public List<CLayer.BookingDetails> Items { get; set; }
        public List<CLayer.Booking> Bookinglist { get; set; }
        public List<CLayer.Address> Addresslist { get; set; }
        public SelectList PaidStatusTypes { get; set; }
        public string CorporateName { get; set; }
        // [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "From")]
        public DateTime FromDate { get; set; }
        public int Status { get; set; }//CLayer.ObjectStatus.StatusType      
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "To")]
        public DateTime ToDate { get; set; }
        public SelectList StatusTrancationTypes { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start  { get; set; }
        public bool ForPrint { get; set; }
        public bool ForPdf { get; set; }
        public List<CLayer.CreditBookingReport> CreditBookingList { get; set; }
        public enum CreditbookingSearchValue { Name = 1, Phone = 2, Email = 3, ConfirmationNo = 4 }

        public decimal BookingAmount { get; set; }
        public string CreditComment { get; set; }

        public long bookid { get; set; }

        public string Paymentdate { get; set; }

        public bool Paid { get; set; }

        public long UpdatedBy { get; set; }
        public int IsCorpbookingpaid { get; set; }
        public CreditBookingModel()
        {
            FromDate = DateTime.Today; ;
            ToDate = DateTime.Today; ;
            CreditBookingList = new List<CLayer.CreditBookingReport>();
            ForPrint = false;
            ForPdf = false;

            List<KeyValuePair<int, string>> objStatustypes1 = new List<KeyValuePair<int, string>>();
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.Corpcreditbookstatus.All, CLayer.ObjectStatus.Corpcreditbookstatus.All.ToString()));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.Corpcreditbookstatus.Paid, CLayer.ObjectStatus.Corpcreditbookstatus.Paid.ToString()));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.Corpcreditbookstatus.NotPaid, CLayer.ObjectStatus.Corpcreditbookstatus.NotPaid.ToString()));
           
            PaidStatusTypes = new SelectList(objStatustypes1, "Key", "Value");

            List<KeyValuePair<int, string>> objStatustypes = new List<KeyValuePair<int, string>>();
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Active, CLayer.ObjectStatus.StatusType.Active.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Blocked, CLayer.ObjectStatus.StatusType.Blocked.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Deleted, CLayer.ObjectStatus.StatusType.Deleted.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.NotVerified, "Recent"));
            StatusTrancationTypes = new SelectList(objStatustypes, "Key", "Value");

            Addresslist = new List<CLayer.Address>();
            Bookinglist = new List<CLayer.Booking>();
            Items = new List<CLayer.BookingDetails>();

        }
    }
}