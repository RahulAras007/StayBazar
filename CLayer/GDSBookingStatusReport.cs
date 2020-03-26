using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class GDSBookingStatusReport
    {
        public long AccommodationId { get; set; }
        public long BookingId { get; set; }
        public long TotalRows { get; set; }
        public string orderno { get; set; }
        public DateTime  BookingDate { get; set; }
        public int BookingItemId { get; set; }
        public string StayCategory { get; set; }
        public DateTime  checkin { get; set; }
        public DateTime  checkout { get; set; }
        public int noofaccommodations { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfChildren { get; set; }
        public int NoOfGuests { get; set; }
        public decimal Amount { get; set; }
        public int NoOfDays { get; set; }
        public decimal  TotalComForSB { get; set; }
        public decimal TotalComForOther { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalTax { get; set; }
     
        public int AccommodationTypeId { get; set; }
        public int StayCategoryId { get; set; }
        public string AccommodationType { get; set; }
       
        public long PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public string PropertyAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string CountryName { get; set; }
        public string State { get; set; }
        public string StateName { get; set; }
        public string DailyRate { get; set; }
        public string ForUserFirstName { get; set; }
        public string ForUserLastName { get; set; }
        public string ForUserEmail { get; set; }
        public string ForUserMobile { get; set; }
        public string FirstDayCharge { get; set; }
        public string B2BDiscount { get; set; }
        public string MarkupForSB { get; set; }
        public string TotalRateTax { get; set; }
        public string TotalGuestTax { get; set; }
        public string HotelConfirmNumber { get; set; }
        public string BookingStatusID { get; set; }
        public string BookingStatus { get; set; }
        public string invoicenumber { get; set; }
        public DateTime  invoicedate { get; set; }
        public DateTime  duedate { get; set; }
        public string InvoiceStatusID { get; set; }
        public string InvoiceStatus { get; set; }
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int BillingEntityID { get; set; }
        public int BookingEntityID { get; set; }
        public string BookingEntity { get; set; }
        public string BillingEntity { get; set; }
      
        public string CGSTTitle { get; set; }
        public string SGSTTitle { get; set; }
        public string IGSTTitle { get; set; }
        public decimal TotalCGSTTaxAmount { get; set; }
        public decimal  TotalSGSTTaxAmount { get; set; }
        public decimal TotalIGSTTaxAmount { get; set; }

    }
}
