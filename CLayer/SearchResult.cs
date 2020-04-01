using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace CLayer
{
    public class SearchResult
    {
        //public string Destination { get; set; }
        public long PropertyId { get; set; }
        public decimal Amount { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string ImageFile { get; set; }
        public bool  IsImageExists { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }
        public int Distance { get; set; }
        public int StarRate { get; set; }
        public string City { get; set; }
        public bool IsDistanceFromCity { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public long PropertyCount { get; set; }
        public long Avail { get; set; }

        public string HotelID { get; set; }
        public int InventoryAPIType { get; set; }
        public string APIType { get; set; }
        public string GDSCurrencyCode { get; set; }
        public decimal GDSRateConversion { get; set; }
        public string StarRating { get; set; }
        public string LocationName { get; set; }
        public int EntitlementOrder { get; set; }
        public string TraceID { get; set; }
        public long MaximumDailyEntitlement { get; set; }
        //*Added By Rahul
        public string RateCardDetailedId { get; set; }
        public decimal TaxPercentage { get; set; }
        public int GSTSlab { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int AccommodationTpeID { get; set; }
        public int StayCategoryID { get; set; }
        
        public decimal AmountWithTax { get; set; }
        public decimal price { get; set; }
        public int ResultIndex { get; set; }
        public int ErrorCode { get; set; }
        public List<RoomStaysResult> RoomStaysResultList { get; set; }

        public SearchResult()
        {
            EntitlementOrder = 0;
        }
    }
    public class RoomStaysResult
    {
        public string SourceofBusiness { get; set; }
        public string RoomType { get; set; }
        public string RoomTypeCode { get; set; }

        public string RoomTypeDescription { get; set; }

        public string RoomID { get; set; }

        public string RatePlanCode { get; set; }

        public string BookingCode { get; set; }

        public string RateTimeUnit { get; set; }

        public decimal  AmountBeforeTax { get; set; }

        public decimal AmountAfterTax { get; set; }

        public decimal MinAmountAfterTax { get; set; }

        public RoomRateDescription RoomRateDescriptionItem { get; set; }

        public RoomRateTotal RoomRateTotalItem { get; set; }

        public string CurrencyCode { get; set; }

        public bool IsMealPlanBreakfast { get; set; }
        public decimal CommissionPercent { get; set; }
        public string CommissionStatusType { get; set; }

    }
    public class RoomRateDescription
    {
        public string Name { get; set; }
        public Array   Description { get; set; }
    }
    public class RoomRateTotal
    {
        public decimal AmountAfterTax { get; set; }
        public string CurrencyCode { get; set; }

        public List<RoomRateTotalTaxes> Taxes { get; set; }
    }
    public class RoomRateTotalTaxes
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Percent { get; set; }
    }
}
