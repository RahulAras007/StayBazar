using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StayBazar.Areas.Admin.Models
{
    public class SuppierInvoiceModel
    {
        public long PropertyId { get; set; }

        public long CustomPropertyId { get; set; }
        public long CustomSupplierId { get; set; }
        [Display(Name = "Property Name")]
        [Required(ErrorMessage = "Enter Property Name")]
        public string PropertyName { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Invoice Number")]
        public string InvoiceNumber { get; set; }
        [Display(Name = "Invoice Date")]
        public string InvoiceDate { get; set; }
        [Display(Name = "Service Tax Registration Number")]
        public string ServiceTaxRegNumber { get; set; }
        [Display(Name = "PAN")]
        public string PAN_Number { get; set; }
        [Display(Name = "Base Amount")]
        public decimal BaseAmount { get; set; }
        [Display(Name = "Luxury Tax")]
        public decimal LuxuryTax { get; set; }
        [Display(Name = "Service Tax")]
        public decimal ServiceTax { get; set; }
        [Display(Name = "Total Invoice Value")]
        public decimal TotalInvoiceValue { get; set; }


        //[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Of Entry")]
        public string EntryDate { get; set; }

        public decimal txtTotalAdjustmentResult { get; set; }

        public long SupplierId { get; set; }
        public long SupplierInvoiceID { get; set; }
        public bool isFromList { get; set; }

        public string BookingRefIDs { get; set; }
        public string BookingRefIDsWithValue { get; set; }
        
        public string checkedBookingRefIDs { get; set; }
        public string editedBookingRefIDs { get; set; }

        public string searchText { get; set; }
        public int searchType { get; set; }
        public int searchTaxType { get; set; }
        public string searchText1 { get; set; }
        public int searchType1 { get; set; }

        public int searchTypeBooking { get; set; }
        public string searchTextBooking { get; set; }

        public enum SearchTypeValues { Property = 1, Supplier = 2, Email = 3 }

        public enum SearchTypeValuesForBookingList { Property = 1, Supplier = 2, City = 3, BookingReffNo=4 }

        public List<CLayer.SupplierInvoice> SupplierInvList { get; set; }
        public List<CLayer.OfflineBooking> BookingList { get; set; }
        public List<CLayer.OfflineBooking> savedBookingList { get; set; }


        public DateTime FromDate { get; set; }
        public DateTime FromDate1 { get; set; }

        public DateTime ToDate1 { get; set; }
        public DateTime ToDate { get; set; }

        public int currentPage { get; set; }
        public int Limit { get; set; }
        public long TotalRows { get; set; }

        public int currentPage_Booking { get; set; }
        public int Limit_Booking { get; set; }
        public long TotalRows_Booking { get; set; }

        //public int currentPage1 { get; set; }
        //public long TotalRows1 { get; set; }

        public string PropertyEmailAddresss { get; set; }

        public string PropertyType { get; set; }

        public int TaxType { get; set; }

        [Display(Name = "Make Supplier Invoice as Done")]
        public bool IsSupInvoicedone { get; set; }

        public bool IsOpen { get; set; }
        public int Status { get; set; }
        public SelectList StatusTypes { get; set; }
        public SuppierInvoiceModel()
        {
            List<KeyValuePair<int, string>> objStatustypes = new List<KeyValuePair<int, string>>();
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.SupplierInvoiceStatusType.All, CLayer.ObjectStatus.SupplierInvoiceStatusType.All.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.SupplierInvoiceStatusType.Pending, CLayer.ObjectStatus.SupplierInvoiceStatusType.Pending.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.SupplierInvoiceStatusType.Completed, CLayer.ObjectStatus.SupplierInvoiceStatusType.Completed.ToString()));
            StatusTypes = new SelectList(objStatustypes, "Key", "Value");
        }
    }
}