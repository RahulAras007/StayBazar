using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class TransactionsModel
    {

        public List<CLayer.BookingDetails> Items { get; set; }
        public List<CLayer.Booking> Bookinglist { get; set; }
        public List<CLayer.Address> Addresslist { get; set; }

        public SelectList StatusTrancationTypes { get; set; }
        public string SearchString { get; set; }// The string to search
        public int SearchValue { get; set; }// The type of the search string
        public int InventoryAPIType { get; set; }//CLayer.ObjectStatus.StatusType
        public SelectList InventoryAPITypes { get; set; }
        //Pagination
        public int Status { get; set; }//CLayer.ObjectStatus.StatusType        
        public int day { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int days { get; set; }
        [Required(ErrorMessage = "Start date required")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public string StartDate { get; set; }
       
        [Required(ErrorMessage = "End date required")]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public string EndDate { get; set; }
        public long UserId { get; set; }
        public enum TransactionsSearchValue { Name= 1, Phone = 2,Email=3,ConfirmationNo =4}

        public SelectList BookingReqStatus { get; set; }
        public SelectList BookingOfflineStatus { get; set; }
        public TransactionsModel()
        {            
            List<KeyValuePair<int, string>> objStatustypes = new List<KeyValuePair<int, string>>();
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Active, CLayer.ObjectStatus.StatusType.Active.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Blocked, CLayer.ObjectStatus.StatusType.Blocked.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Deleted, CLayer.ObjectStatus.StatusType.Deleted.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.NotVerified, "Recent"));
            StatusTrancationTypes = new SelectList(objStatustypes, "Key", "Value");

            List<KeyValuePair<int, string>> objStatustypes1 = new List<KeyValuePair<int, string>>();
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.BookingRequestStatus.All, CLayer.ObjectStatus.BookingRequestStatus.All.ToString()));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.BookingRequestStatus.Request, CLayer.ObjectStatus.BookingRequestStatus.Request.ToString()));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.BookingRequestStatus.Declined, CLayer.ObjectStatus.BookingRequestStatus.Declined.ToString()));

            BookingReqStatus = new SelectList(objStatustypes1, "Key", "Value");

            List<KeyValuePair<int, string>> objInventoryAPItypes = new List<KeyValuePair<int, string>>();
            objInventoryAPItypes.Add(new KeyValuePair<int, string>(3, "All"));
            objInventoryAPItypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.InventoryAPIType.Amadeus, CLayer.ObjectStatus.InventoryAPIType.Amadeus.ToString()));
            objInventoryAPItypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.InventoryAPIType.Maxmojo, CLayer.ObjectStatus.InventoryAPIType.Maxmojo.ToString()));
            objInventoryAPItypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.InventoryAPIType.SBInventory, CLayer.ObjectStatus.InventoryAPIType.SBInventory.ToString()));
            //objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.NotVerified, "Non-Verified"));
            InventoryAPITypes = new SelectList(objInventoryAPItypes, "Key", "Value");


            List<KeyValuePair<int, string>> objStatustypes2 = new List<KeyValuePair<int, string>>();
            objStatustypes2.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.BookingOfflineStatus.All, CLayer.ObjectStatus.BookingOfflineStatus.All.ToString()));
            objStatustypes2.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.BookingOfflineStatus.Request, CLayer.ObjectStatus.BookingOfflineStatus.Request.ToString()));
            objStatustypes2.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.BookingOfflineStatus.Confirmed, CLayer.ObjectStatus.BookingOfflineStatus.Confirmed.ToString()));

            BookingOfflineStatus = new SelectList(objStatustypes2, "Key", "Value");

            Addresslist = new List<CLayer.Address>();
            Bookinglist = new List<CLayer.Booking>();
            Items = new List<CLayer.BookingDetails>();
        }
    }
}