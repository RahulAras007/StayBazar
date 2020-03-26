using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class OfflinePropertyBooking
    {
        public long OfflineBookingId { get; set; }

        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerCountry { get; set; }
        public string CustomerCountryname { get; set; }
        public int CustomerState { get; set; }
        public string CustomerStatename { get; set; }
        public int CustomerCity { get; set; }
        public string CustomerCityname { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }

        //Property Details
        public long PropertyId { get; set; }
        public long CustomPropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyContactNo { get; set; }
        public string PropertyEmail { get; set; }
        public string PropertyCaretakerName { get; set; }
        public int PropertyCountry { get; set; }
        public string PropertyCountryname { get; set; }
        public int PropertyState { get; set; }
        public string PropertyStatename { get; set; }
        public int PropertyCity { get; set; }
        public string PropertyCityname { get; set; }

        //Supplier Details
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierMobile { get; set; }
        public string SupplierEmail { get; set; }
        public int SupplierCountry { get; set; }

        public string SupplierCountryname { get; set; }
        public int SupplierState { get; set; }
        public string SupplierStatename { get; set; }
        public int SupplierCity { get; set; }
        public string SupplierCityname { get; set; }

        //Booking Details
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public long NoOfNight { get; set; }
        public long NoOfPaxAdult { get; set; }
        public long NoOfPaxChild { get; set; }
        public long NoOfRooms { get; set; }
        public long StayCategory { get; set; }
        public string StayCategoryName { get; set; }
        public long AccommodatoinType { get; set; }
        public string AccommodatoinTypename { get; set; }
        public long Accommodationtypeid { get; set; }
        public long Accommodationid { get; set; }
        public string Accommodationname { get; set; }
        public long StayCategoryid { get; set; }

        //Pricing Details

        //buy price
        public decimal AvgDailyRateBefreStaxForBuyPrice { get; set; }
        public decimal StaxForBuyPrice { get; set; }
        public decimal TotalAmtForBuyPrice { get; set; }
        public decimal BuyPriceforotherservicesForBuyprice { get; set; }
        public decimal StaxForotherBuyPrice { get; set; }
        public decimal TotalAmtotherForBuyPrice { get; set; }
        public decimal TotalBuyPrice { get; set; }

        //sale price
        public decimal AvgDailyRateBefreStaxForSalePrice { get; set; }
        public decimal StaxForSalePrice { get; set; }
        public decimal TotalAmtForSalePrice { get; set; }
        public decimal BuyPriceforotherservicesForSalePrice { get; set; }
        public decimal StaxForotherForSalePrice { get; set; }
        public decimal TotalAmtotherForSalePrice { get; set; }
        public decimal TotalSalePrice { get; set; }

        //Other service details
        public string OtherService { get; set; }

        public DateTime CreatedTime { get; set; }

        public bool sendmailtosupplier { get; set; }
        public bool sendmailtocustomer { get; set; }
        public string ConfirmationNumber { get; set; }

        public string BookingDate { get; set; }

        public long TotalRows { get; set; }
        public int SaveStatus { get; set; }
        public enum Statuslist { Save = 1, Submit = 2, Cancel = 3, Delete = 4 }

    }
}
