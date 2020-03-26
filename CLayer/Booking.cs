using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Booking
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
        public string PropertyEmail { get; set; }
        public string propertyLocation { get; set; }
        public string propertyAddress { get; set; }
        public string PropertyTitle { get; set; } 
        public string propertyPin { get; set; } 
        public string propertyEmail { get; set; } 
        public string propertyCity { get; set; } 


        
        public string PositionLat { get; set; }
        public string PositionLng { get; set; }
        public long OwnerId { get; set; }
        public string Address { get; set; }
        public long AddressId { get; set; }
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
        public long BookingUserId { get; set; }
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
        //Pagination Grid
        public long TotalRows { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public int day { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PaymentFirstinstallment { get; set; }
        public decimal PaymentSecondinstallment { get; set; }
        public decimal PartialPaymentPercentage { get; set; }
        public int PartialPaymentStatus { get; set; }

        public decimal RefundAmount { get; set; }
        public string CorporateName { get; set; }

        public int InventoryAPIType { get; set; }
        public string HotelConfirmNumber { get; set; }

        public string SbEntityName { get; set; }
        public string SbEntityAddress { get; set; }
        public string SbEntityCountry { get; set; }
        public string SbEntityState { get; set; }
        public string SbEntityPhone { get; set; }
        public string SbEntityGSTNo { get; set; }

        public string BillingAddress { get; set; }
        public string BillingCityname { get; set; }
        public string BillingStatename { get; set; }
        public string BillingCountryname { get; set; }
        public string BillingpinCode { get; set; }

        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerpinCode { get; set; }
        public int CustomerType { get; set; }
        public int CustomerCountry { get; set; }
        public int CustomerCity { get; set; }
        public int CustomerState { get; set; }
        public string CustomerCountryname { get; set; }
        public string CustomerStatename { get; set; }
        public string CustomerCityname { get; set; }

        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyCityname { get; set; }
        public string PropertyStatename { get; set; }
        public string PropertyCountryname { get; set; }
        public string PropertyContactNo { get; set; }
        public string PropertyCaretakerName { get; set; }
    
        public string PropertyPinCode { get; set; }
        public int approval_order { get; set; }
        public int approval_status { get; set; }
        public string approvalstatus { get; set; }
        public int Approver_id { get; set; }
        public int booking_approval_id { get; set; }
        public DateTime CreatedDate { get; set; }

        public string ApproverEmail { get; set; }
        public string ApproverName { get; set; }
        public string BookingUserEmail { get; set; }
        public string BookingUserName { get; set; }

        public string RejectionNote { get; set; }
    }

    public class BookingAccDetails
    {
        public long AccommodationId { get; set; }
        public int AccCount { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public int Guest { get; set; }

        public decimal Cus_SupplierAmount { get; set; }
        public decimal Cus_TotalAmount { get; set; }
        public decimal Cus_TaxAmount { get; set; }
        public decimal Cus_GrandTotalAmount { get; set; }
    }
    public class BookingApprovals
    {
        public long Booking_Approval_Id { get; set; }//CLayer.ObjectStatus.StatusType
        public long BookingId { get; set; }
        public long approver_id { get; set; }
        public string  approver { get; set; }
        public DateTime CreatedDate { get; set; }
        public int approval_order { get; set; }
        public int approval_status { get; set; }
        public string  approvalstatus { get; set; }
        public int Maxapproval_order { get; set; }
        public string RejectionNote { get; set; }
    }
}