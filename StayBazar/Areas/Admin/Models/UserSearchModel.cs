using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class UserSearchModel
    {
        [Display(Name = "Status")]
        public int Status { get; set; }//CLayer.ObjectStatus.StatusType
        public SelectList StatusTypes { get; set; }
        public string SearchString { get; set; }// The string to search
        public int SearchValue { get; set; }// The type of the search string
        public int UserType { get; set; }
        public int PropertyStatus { get; set; }//CLayer.ObjectStatus.StatusType
        public int InventoryAPIType { get; set; }//CLayer.ObjectStatus.StatusType
        public SelectList PropertyStatusTypes { get; set; }
        public SelectList InventoryAPITypes { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public bool Statusfor { get; set; }
        public enum RegularUserValues { Name = 1, Email = 2, City = 3, Phone = 4, OrderID = 5 }
        public enum SupplierValues { Name = 1, Email = 2, City = 3, Location = 7, Phone = 4, UserCode = 5, State = 6 }
        public enum CorporateValues { Name = 1, Email = 2, City = 3, Phone = 4, State = 6 }
        public enum AffiliateValues { Name = 1, Email = 2, City = 3, Phone = 4, UserCode = 5, State = 6 }
        public enum PropertyValue { Name = 1, Email = 2, City = 3, Location = 5, Phone = 4, State = 6 }
        public List<CLayer.User> userlist { get; set; }

        public UserSearchModel()
        {
            List<KeyValuePair<int, string>> objPropertyStatusTypes = new List<KeyValuePair<int, string>>();
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Active, CLayer.ObjectStatus.StatusType.Active.ToString()));
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Disabled, CLayer.ObjectStatus.StatusType.Disabled.ToString()));
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Deleted, CLayer.ObjectStatus.StatusType.Deleted.ToString()));
            PropertyStatusTypes = new SelectList(objPropertyStatusTypes, "Key", "Value");

            List<KeyValuePair<int, string>> objStatustypes = new List<KeyValuePair<int, string>>();
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Active, CLayer.ObjectStatus.StatusType.Active.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Blocked, CLayer.ObjectStatus.StatusType.Blocked.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Deleted, CLayer.ObjectStatus.StatusType.Deleted.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.NotVerified, "Non-Verified"));
            StatusTypes = new SelectList(objStatustypes, "Key", "Value");

            List<KeyValuePair<int, string>> objInventoryAPItypes = new List<KeyValuePair<int, string>>();
            objInventoryAPItypes.Add(new KeyValuePair<int, string>(3, "All"));
            objInventoryAPItypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.InventoryAPIType.Amadeus, CLayer.ObjectStatus.InventoryAPIType.Amadeus.ToString()));
            objInventoryAPItypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.InventoryAPIType.Maxmojo, CLayer.ObjectStatus.InventoryAPIType.Maxmojo.ToString()));
            objInventoryAPItypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.InventoryAPIType.SBInventory, CLayer.ObjectStatus.InventoryAPIType.SBInventory.ToString()));
            //objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.NotVerified, "Non-Verified"));
            InventoryAPITypes = new SelectList(objInventoryAPItypes, "Key", "Value");



        }
    }

    public class UserDetailModel
    {
        public long UserId { get; set; }
        [Display(Name = "Salutation")]
        public int SalutationId { get; set; }
        public SelectList Salutations { get; set; }

        [Display(Name = "Business Name")]
        //[Required(ErrorMessage = " is required")]
        public string Name { get; set; }

        [Display(Name = "Business Name")]
        public string Businessname { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = " is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }//Firstname + Lastname

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
        public int Sex { get; set; }
        [Display(Name = "Gender")]
        public SelectList SexList { get; set; }
        public enum sex { Female = 1, Male = 2 }
        public int Status { get; set; }
        [Display(Name = "Email")]
        //[Required(ErrorMessage = " is required")]
        //[EmailAddress(ErrorMessage = "Requires a valid email address")]
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime LastLoginOn { get; set; }
        public string LastEditedBy { get; set; }
        public string LastEditedDate { get; set; }
        public int MaxStaff { get; set; }
        [Required(ErrorMessage = " is required")]
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "Enter Customer payment mode")]
        [Display(Name = "Customer Payment Mode")]
        public int CustomerPaymentMode { get; set; }

        //[Display(Name = "Credit Days")]
        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:0.#}")]
        //[RegularExpression(@"^[0-9]+(\.[0-9]{1,2})$", ErrorMessage = "Valid Decimal number with maximum 2 decimal places.")]
        public decimal CustomerPaymentModeCreditDays { get; set; }
        //
        //For Address
        //
        public long AddressId { get; set; }
        [Required(ErrorMessage = " is required")]
        [Display(Name = "Address Line")]
        public string Address { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        public int CityId { get; set; }
        [Display(Name = "State")]
        public int State { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "Phone")]
        [MaxLength(50, ErrorMessage = "Max length 50 characters")]
        public string Phone { get; set; }
        [Required(ErrorMessage = " is required")]
        [Display(Name = "Mobile")]
        [MaxLength(50, ErrorMessage = "Max length 50 characters")]
        public string Mobile { get; set; }
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
        public int AddressType { get; set; }


        //
        //
        //

        //
        // For Bank Details for Supplier
        public long BankAccountId { get; set; }
        [Display(Name = "Account number")]
        public string AccountNumber { get; set; }
        [Display(Name = "Bank")]
        public string BankName { get; set; }
        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }
        [Display(Name = "RTGS Number")]
        public string RTGSNumber { get; set; }
        [Display(Name = "MICR Code")]
        public string MICRCode { get; set; }
        //
        [Display(Name = "PAN No")]
        public string PANNo { get; set; }

        public List<CLayer.B2B> SupplierList { get; set; }

        public long AffiliateId { get; set; }
        // FOR GST
        [Display(Name = "GST Registration No")]
        public string GSTRegistrationNo { get; set; }
        [Display(Name = "State Of Registration")]
        public string StateOfRegistration { get; set; }
        public long B2bGSTId { get; set; }
        public SelectList CusStateList { get; set; }

        // For Billing Address for Corporate
        [Display(Name = "Pin Code")]
        public string BillingZipCode { get; set; }
        public long BillingAddressId { get; set; }
        [Display(Name = "Address")]
        public string BillingAddress { get; set; }
        [Display(Name = "City")]
        public int BillingCityId { get; set; }
        public string BillingCity { get; set; }
        [Display(Name = "State")]
        public int BillingState { get; set; }
        [Display(Name = "Country")]
        public int BillingCountryId { get; set; }
        public SelectList BillingCountryList { get; set; }
        public SelectList BillingStateList { get; set; }
        public SelectList BillingCityList { get; set; }
        public bool IsClicked { get; set; }
        public int BillingAddressType { get; set; }
        //for request confirmation
        [Display(Name = "Request Status")]
        public int RequestStatus { get; set; }
        [Display(Name = "Property Description")]
        public string PropertyDescription { get; set; }
        [Display(Name = "No. Of Properties available")]
        public int AvailableProperties { get; set; }
        [Display(Name = "Contact Designation")]
        public string ContactDesignation { get; set; }

        //Corporate Credit Booking
        public int IsCreditBooking { get; set; }
        public long CreditDays { get; set; }
        public decimal CreditAmount { get; set; }

        public int IsCorpBookingtoday { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Credit Period")]
        public long CreditPeriod { get; set; }


        [Display(Name = "Address")]
        public string SubCustomerAddress { get; set; }

        [Display(Name = "Pin Code")]
        public string SubCustomerpinCode { get; set; }


        public string SubCustomerCityname { get; set; }
        public string HiddenSubCustomerCityname { get; set; }

        public long HiddenSubCustomerCity { get; set; }
        public string HiddenSubCustomerAddress { get; set; }
        public string HiddenSubCustomerpinCode { get; set; }
        public string HiddenSubCustomerGstRegNo { get; set; }
        public long HiddenCustomerTableType { get; set; }

        [Display(Name = "City")]
        public int SubCustomerCity { get; set; }
        public SelectList SubCusCityList { get; set; }

        [Display(Name = "Customer Payment Mode")]
        //public int CustomerPaymentMode { get; set; }
        public SelectList CustomerPaymentMode_Manage { get; set; }
        //[Display(Name = "Credit Days")]
        //public decimal CustomerPaymentModeCreditDays { get; set; }
        public UserDetailModel()
        {



            List<CLayer.Salutation> salutations = BLayer.Salutation.GetAll();
            Salutations = new SelectList(salutations, "SalutationId", "Title");

            List<CLayer.Country> countries = BLayer.Country.GetAll();
            CountryList = new SelectList(countries, "CountryId", "Name");
            BillingCountryList = new SelectList(countries, "CountryId", "Name");


            if (countries.Count > 0)
            {
                List<CLayer.State> states = BLayer.State.GetOnCountry((int)countries[0].CountryId);
                StateList = new SelectList(states, "StateId", "Name");
                BillingStateList = new SelectList(states, "StateId", "Name");
                List<CLayer.City> cities = null;
                if (states.Count > 0)
                {
                    cities = BLayer.City.GetOnState((int)states[0].StateId);
                }
                cities.Add(new CLayer.City() { CityId = -1, Name = "None" });
                CityList = new SelectList(cities, "CityId", "Name");
                BillingCityList = new SelectList(cities, "CityId", "Name");
            }
            List<SelectListItem> sexItems = new List<SelectListItem>(2);
            sexItems.Add(new SelectListItem { Text = "Male", Value = "2" });
            sexItems.Add(new SelectListItem { Text = "Female", Value = "1" });
            SexList = new SelectList(sexItems, "Value", "Text");



            List<CLayer.City> SubCusCityList1 = new List<CLayer.City>();
            SubCusCityList = new SelectList(SubCusCityList1, "CityId", "Name");

            List<KeyValuePair<int, string>> objCustomerPaymentMode_mang = new List<KeyValuePair<int, string>>();
            //objCustomerPaymentMode_mang.Add(new KeyValuePair<int, string>(0, "Select"));
            objCustomerPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.CustomerPaymentModelist.AdvancePayment, "Advance Payment"));
            objCustomerPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.CustomerPaymentModelist.PaymentOnCheckOut, "Payment Check-out"));
            objCustomerPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.CustomerPaymentModelist.Credit, "Credit"));
            CustomerPaymentMode_Manage = new SelectList(objCustomerPaymentMode_mang, "Key", "Value");
        }
        public void LoadPlaces()
        {
            List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
            StateList = new SelectList(states, "StateId", "Name");
            List<CLayer.City> cities = new List<CLayer.City>();
            if (states.Count > 0)
            {
                cities = BLayer.City.GetOnState(State);
            }
            cities.Add(new CLayer.City() { CityId = 0, Name = "Other" });
            CityList = new SelectList(cities, "CityId", "Name");

            List<CLayer.State> Billingstates = BLayer.State.GetOnCountry(BillingCountryId);
            BillingStateList = new SelectList(Billingstates, "StateId", "Name");
            List<CLayer.City> Billingcities = null;
            if (Billingstates.Count > 0)
            {
                Billingcities = BLayer.City.GetOnState(BillingState);
            }
            else
            {
                Billingcities = new List<CLayer.City>();
            }
            Billingcities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            BillingCityList = new SelectList(Billingcities, "CityId", "Name");
        }
    }
}