using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class BookingItem
    {
        public long OfflineBookingItemId { get; set; }
        public long BookingItemId { get; set; }
        public long BookingId { get; set; }
        public long AccommodationId { get; set; }
        public long AccommodationTypeId { get; set; }
        public decimal Amount { get; set; }
        public int NoOfDays { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfGuests { get; set; }
        public string Remarks { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CommissionSB { get; set; }
        public decimal TotalCommissionforother { get; set; }
        public decimal CorporateDiscountAmount { get; set; }
        public int Status { get; set; }       
        public decimal CancelServiceCharge { get; set; }
        public decimal CancelAccommodationCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal GuestAmount { get; set; }
        public int NoOfAccommodations { get; set; }
        public int NoOfChildren { get; set; }
        public decimal FirstDayCharge { get; set; }
        public string RatesApplied { get; set; }
        public decimal PurchaseRateAfterTax { get; set; }
        public decimal TaxRate { get; set; }
        public decimal MarkUpRate { get; set; }
        public decimal PurchaseRateBeforeTax { get; set; }
        public decimal SellRateAfterTax { get; set; }
        public decimal SellRateBeforeTax { get; set; }
        public decimal MarginAmount { get; set; }
        //other data
        public DateTime LockInTime { get; set; }
       
        public string  AccommodationTypeT { get; set; }
        public string StayCategoryT { get; set; }
        public decimal DailyRate { get; set; }
        //For display
        public string AccommodationTitle { get; set; }
        public string PropertyTitle { get; set; }
        public string PropertyAddress { get; set; }
        public long PropertyId { get; set; }
        //Booking For
        public long ForUser { get; set; }
        public long UserId { get; set; }
        public long ForBookingUserId { get; set; }
        public string ForUserFirstName { get; set; }
        public string ForUserLastName { get; set; }
        public string ForUserEmail { get; set; }
        public string ForUserMobile { get; set; }
        public string ForUserAddress { get; set; }
        public string  ForState { get; set; }
        public string  ForCity { get; set; }
        public string ForCountry { get; set; }
        public string ZipCode { get; set; }
        public long TotalRows { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int quantity { get; set; }
        //Partialamiount
        public decimal Partialamountperc { get; set; }
        public decimal Partialamount { get; set; }
        public decimal remainingamount { get; set; }

        //tax
        public decimal TotalRateTax { get; set; }
        public decimal TotalGuestTax { get; set; }

        public decimal PaypalCommission { get; set; }
        public string CountryName { get; set; }


        public decimal Cus_SupplierAmount { get; set; }
        public decimal Cus_TotalAmount { get; set; }
        public decimal Cus_TaxAmount { get; set; }
        public decimal Cus_GrandTotalAmount { get; set; }
        public int IsCustomBook { get; set; }
        public string HotelConfirmNumber { get; set; }

        public long GDSCountry { get; set; }
        public decimal GDSAmount { get; set; }
        public decimal GDSConversionRate { get; set; }
        public decimal GDSConvertedAmount { get; set; }
        public string cityName { get; set; }
        public string ApproverName { get; set; }
        public string RejectionNote { get; set; }
    }
}
