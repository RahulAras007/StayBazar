using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class TransactionListModel
    {
        public List<CLayer.BookingDetails> Items { get; set; }
        public List<CLayer.Booking> Bookinglist { get; set; }

        //Pagination
        public int Status { get; set; }//CLayer.ObjectStatus.StatusType
         [Display(Name = "Search By Days")]
        public int day { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int days { get; set; }
        [DataType(DataType.Date)]
        public string StartDate { get; set; }
        [Required(ErrorMessage = "End date required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        public SelectList StatusTrancationTypes { get; set; }

        public TransactionListModel()
        {            
            List<KeyValuePair<int, string>> objStatustypes = new List<KeyValuePair<int, string>>();
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Active, CLayer.ObjectStatus.StatusType.Active.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Blocked, CLayer.ObjectStatus.StatusType.Blocked.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Deleted, CLayer.ObjectStatus.StatusType.Deleted.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.NotVerified, "Recent"));
            StatusTrancationTypes = new SelectList(objStatustypes, "Key", "Value");
            Bookinglist = new List<CLayer.Booking>();
            Items = new List<CLayer.BookingDetails>();
        }
    }
}