using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class BookingDetails
    {


        public long BookingId { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalCommissionForSB { get; set; }
        public decimal TotalServiceCharge { get; set; }
        public decimal TotalCancellationCharge { get; set; }
        public string PaymentToken { get; set; }
        public int Status { get; set; }
        public long ByUserId { get; set; }
        public string Notes { get; set; }
        public string OrderNo { get; set; }
        //for listing purposes
        public int BookingStatus { get; set; }
        public decimal bookingTotalAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal CancelServiceCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        //Property
        public long PropertyId { get; set; }
        public string Title { get; set; }
        public string propertyLocation { get; set; }
        public string propertyAddress { get; set; }
        public string PropertyTitle { get; set; }
        public string PositionLat { get; set; }
        public string PositionLng { get; set; }
        public long OwnerId { get; set; }
        public string Address { get; set; }
        public long BookingItemId { get; set; }
        public int BookingItemStatus { get; set; }
        public int Quantity { get; set; }
        //Accommodation
        public long AccommodationId { get; set; }
        public int NoOfAccomodations { get; set; }
        public int AccommodationTypeId { get; set; }
        public string AccommodationType { get; set; }
        public int AccommodationCount { get; set; } //Number of accommodations
        public string AccommodationTypeTitle { get; set; }
        public int NoOfAccommodations { get; set; }
        public string AccommoLocation { get; set; }
        public int NoOfChildren { get; set; }
        public int NoOfDays { get; set; }
        public int NoOfAdults { get; set; }
        public int NoOfGuests { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int MaxNoOfPeople { get; set; }
        public int MaxNoOfChildren { get; set; }
        public int MinNoOfPeople { get; set; }
        public int BedRooms { get; set; }
        public decimal Area { get; set; }
        public string Remarks { get; set; }
        public decimal CommissionSB { get; set; }
        public decimal CorporateDiscountAmount { get; set; }
        public decimal CancelAccommodationCharge { get; set; }
        public string accommodationTypeTitle { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string CategoryName { get; set; }
        public int Country { get; set; }
        public int StayCategoryId { get; set; }
        public string StayCategory { get; set; }
        public decimal Rate { get; set; }
        public decimal bookingitemsTotalAmount { get; set; }
        public decimal GuestAmount { get; set; }
        public DateTime LockInTime { get; set; }
        public long? ForBookingUserId { get; set; }
        public long? ForUser { get; set; }
        //User Details and Address for Admin Booking List
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AddressText { get; set; }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int State { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int AddressType { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Sex { get; set; }
        public string UserType { get; set; }
        //Pagination Gride
        public long TotalRows { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public int day { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }        


        //public long PropertyId { get; set; }
        //public string Title { get; set; }
        ////public Booking b { get; set; }
        ////public BookingItem bi { get; set; }
        ////public Accommodation acc{ get; set; }
        ////public Property p { get; set; }
        ////public Address ad{ get; set; }                 
        //public Booking Main { get; set; }
        //public List<BookingItem> ItemsList { get; set; }
        //public List<Booking> BookingList { get; set; }
        //public List<Accommodation> AccommodationList { get; set; }
        //public List<Address> AddressList { get; set; }
        //public List<Property> PropertyList { get; set; }               
        public BookingDetails()
        {
            
           
        }
    }
}
