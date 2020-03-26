using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Property : ICommunication
    {
        public long PropertyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string OfferTitle { get; set; }
        public CLayer.ObjectStatus.StatusType Status { get; set; }
        public long OwnerId { get; set; }
        public string PositionLat { get; set; }
        public string PositionLng { get; set; }
        public string Address { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public int CityId { get; set; }
        public string ZipCode { get; set; }
        public int Rating { get; set; }           
        public bool IsManualReview { get; set; }
        public Decimal SumRating { get; set; }
        public int CountRating { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }

        public string GSTRegistrationNo { get; set; }
        public string PanNo { get; set; }
        public string CheckInTime { get; set; }
        public string CheckInhr { get; set; }
        public string CheckInmin { get; set; }
        public string CheckInclock { get; set; }
        public string CheckOutTime { get; set; }
        public string CheckOuthr { get; set; }
        public string CheckOutmin { get; set; }
        public string CheckOutClock { get; set; }

        public int AgeLimit { get; set; }
        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }

        public int NoOfAccommodations { get; set; }
        public string Email { get; set; }
        public decimal B2CMarkupShortTerm { get; set; }
        public decimal B2CMarkupLongTerm { get; set; }
        public decimal B2BMarkupShortTerm{ get; set; }
        public decimal B2BMarkupLongTerm{ get; set; }
        public decimal B2BStdShortTermDis{ get; set; }
        public decimal B2BStdLongTermDis { get; set; }

        public double CancellationCharge { get; set; }
        public int CancellationPeriod { get; set; }
        public CLayer.ObjectStatus.CancellationType CancellationMethod { get; set; }
        public bool IsChargeAppliesToRefund { get; set; }

        public decimal DistanceFromCity { get; set; }

        //for display property_Mostpopular
        public string Countryname { get; set; }
        public string Statename { get; set; }
        public string SupplierName { get; set; }


        public string SupplierAddress { get; set; }
        public int SupplierCountry { get; set; }
        public int SupplierState { get; set; }
        public string SupplierCity { get; set; }
        public int SupplierCityId { get; set; }
        public string SupplierZipCode { get; set; }
        public string SuppierMobile { get; set; }

        public decimal bookingitemsTotalAmount { get; set; }
        public string FileName { get; set; }
        //PropertyStatus
        public int PropertyFor { get; set; }
       
        public int PropertyInventoryType { get; set; }
        public string BusinessName { get; set; }
        public decimal B2CPartialPaymentsPcte { get; set; }
        public decimal B2BPartialPaymentsPcte { get; set; }

        public string HotelID { get; set; }

        public string TBOHotelId { get; set; }
        public string TBOFlag { get; set; }
        public string TamarindHotelId { get; set; }
        public string TamarindFlag { get; set; }


        public string NonTitlesHotelCode { get; set; }
        public string NonDescriptionsHotelCode { get; set; }
        public string NonImagesHotelCode { get; set; }
        public string FormattedDescription { get; set; }
        public int InventoryAPIType { get; set; }
        public string InventoryAPITypeValue { get; set; }
        public long MaximumEntitlement { get; set; }


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PropertyOrder { get; set; }
        public string RateCardDetailedId { get; set; }
        public decimal TaxPercentage { get; set; }
        public int GSTSlab { get; set; }
        //public string EntityName { get; set; }
        public void Validate()
        {
            //Description = Utils.CutString(Description, 8000);
            Location = Utils.CutString(Location, 50);
            PositionLat = Utils.CutString(PositionLat, 15);
            PositionLng = Utils.CutString(PositionLng, 15);
            Address = Utils.CutString(Address, 500);
            City = Utils.CutString(City, 50);
            ZipCode = Utils.CutString(ZipCode, 10);
        }
    }
    public class GDSProperties
    {
        public long Propertyid { get; set; }
        public string Hotel_Id { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}