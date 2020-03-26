using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Address : ICommunication
    {
        public long AddressId { get; set; }
        public long UserId { get; set; }
        public string AddressText { get; set; }
        public int CountryId { get; set; }
        public int State { get; set; }
        public int CityId { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public int AddressType { get; set; }
        public int BillingAddressType { get; set; }
        //for listing purposes
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Country { get; set; }
        public string StateName { get; set; }        
        public string Email { get; set; }
        public string CountryCode { get; set; }

        public string AccommodationTitle { get; set; }
        public string PropertyTitle { get; set; }
        public long PropertyId { get; set; }
        public string PropertyAddress { get; set; }
        public string ForUserEmail { get; set; }
        public string ForUserMobile { get; set; }
        // For Billing Address for Corporate
        public long BillingAddressId { get; set; }
        public string BillingAddress { get; set; }
        public int BillingCityId { get; set; }
        public string BillingCity { get; set; }
        public int BillingState { get; set; }
        public int BillingCountryId { get; set; }
        public string BillingZipCode { get; set; }
        public long ForBookingUserId { get; set; }
        public long BookingItemId { get; set; }
        public long BookingId { get; set; }
        public DateTime datetime { get; set; }
        public enum AddressTypes { Primary = 1, Normal = 2 }
        // Address Types
        // 1 = Primary (Billing), 2 = Normal

        public void Validate()
        {
            AddressText = Utils.CutString(AddressText, 500);
            City = Utils.CutString(City, 50);
            ZipCode = Utils.CutString(ZipCode, 10);
            Phone = Utils.CutString(Phone, 50);
            Mobile = Utils.CutString(Mobile, 50);
        }
    }
}
