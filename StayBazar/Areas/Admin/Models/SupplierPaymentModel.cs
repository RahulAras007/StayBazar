using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
  
    public class SupplierPaymentModel
    {
        [Display(Name = "Status")]
        public int Status { get; set; }//CLayer.ObjectStatus.SupplierPaymentStatusType
        public SelectList StatusTypes { get; set; }
        public string SearchString { get; set; }// The string to search
        public int SearchValue { get; set; }// The type of the search string
        public int UserType { get; set; }
        public int PropertyStatus { get; set; }//CLayer.ObjectStatus.StatusType
        public SelectList PropertyStatusTypes { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public bool Statusfor { get; set; }
        public bool ForPrint { get; set; }
        public bool ForPdf { get; set; }
        public long supplierId { get; set; }
        public string SupplierEmail { get; set; }
        public bool supplierPayment { get; set; }
        public long supplierPaymentId { get; set; }
        [Display(Name = "Gross Amount")]
        //[Required(ErrorMessage = " is required")]
        public decimal GrossAmount { get; set; }
        [Display(Name = "Mode of Payment")]
        //[Required(ErrorMessage = " is required")]
        public string ModeofPayment { get; set; }
        [Display(Name = "Net Amount")]
        //[Required(ErrorMessage = " is required")]
        public decimal NetAmount { get; set; }
        [Display(Name = "TDS Amount")]
        //[Required(ErrorMessage = " is required")]
        public decimal TdsAmount { get; set; }
        [Display(Name = "Bank")]
        //[Required(ErrorMessage = " is required")]
        public string Bank { get; set; }

        public string dtPayment { get; set; }
        public DateTime pdtPayment { get; set; }

        public List<string> bankname { get; set; }
        public List<string> payment { get; set; }
        public IEnumerable<string> bookinreferno { get; set; }
        public enum SupplierPaymentValues {  ConfirmationNo = 1, Name = 4, Phone = 2,Email=3 }
        public enum RegularUserValues { Name = 1, Email = 2, City = 3, Phone = 4, OrderID = 5 }
        public enum SupplierValues { Name = 1, Email = 2, City = 3, Location = 7, Phone = 4, UserCode = 5, State = 6 }
        public enum CorporateValues { Name = 1, Email = 2, City = 3, Phone = 4, State = 6 }
        public enum AffiliateValues { Name = 1, Email = 2, City = 3, Phone = 4, UserCode = 5, State = 6 }
        public enum PropertyValue { Name = 1, Email = 2, City = 3, Location = 5, Phone = 4, State = 6 }
        public List<CLayer.SupplierPayment> userlist { get; set; }
        public CLayer.SupplierPayment Supplier { get; set; }
        public List<CLayer.SupplierPayment> SupplierList { get; set; }
        public SupplierPaymentModel()
        {

            List<KeyValuePair<int, string>> objPropertyStatusTypes = new List<KeyValuePair<int, string>>();

            objPropertyStatusTypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Active, CLayer.ObjectStatus.StatusType.Active.ToString()));
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Disabled, CLayer.ObjectStatus.StatusType.Disabled.ToString()));
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Deleted, CLayer.ObjectStatus.StatusType.Deleted.ToString()));
            PropertyStatusTypes = new SelectList(objPropertyStatusTypes, "Key", "Value");
            List<string> paymentmode = new List<string>();
            paymentmode.Add("Check");
            paymentmode.Add("Cash");
            paymentmode.Add("Internet Bank");
            payment = paymentmode;
            Bank = BLayer.Settings.GetValue(CLayer.Settings.Bank);
            List<string> bnk = new List<string>();
            if(Bank.Contains(",")){
                string[] words = Bank.Split(',');
                foreach (string word in words)
                {
                    int i=0;
                    bnk.Insert(i, word);
                    i = i + 1;
                }
            }
            bankname = bnk;
          

            List<KeyValuePair<int, string>> objStatustypes = new List<KeyValuePair<int, string>>();
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.SupplierPaymentStatusType.Pending, CLayer.ObjectStatus.SupplierPaymentStatusType.Pending.ToString()));

            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.SupplierPaymentStatusType.All, CLayer.ObjectStatus.SupplierPaymentStatusType.All.ToString()));
            objStatustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.SupplierPaymentStatusType.Paid, CLayer.ObjectStatus.SupplierPaymentStatusType.Paid.ToString()));
           
            StatusTypes = new SelectList(objStatustypes, "Key", "Value");
        }
    }

    public class SupplierPaymentDetailModel
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
        public double Buyprice { get; set; }
        public string SupplierName { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierMobile { get; set; }
        public string LastEditedBy { get; set; }
        public string LastEditedDate { get; set; }
        public int MaxStaff { get; set; }
        [Required(ErrorMessage = " is required")]
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }
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
        public SupplierPaymentDetailModel()
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