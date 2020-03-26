using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
namespace StayBazar.Models
{
    public class UserModel
    {
        public long UserId { get; set; }
        public int SalutationId { get; set; }
        [Display(Name = "Salutation")]
        public SelectList Salutations { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "is required")]
        public string FirstName { get; set; }

        [Display(Name = "Business name")]
        public string Businessname { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
        public int Sex { get; set; }
        [Display(Name = "Gender")]
        public SelectList SexList { get; set; }

        public int UserTypeId { get; set; }
        [Display(Name = "User Type")]
        public SelectList UserTypes { get; set; }
        public int Status { get; set; }
        public SelectList StatusList { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "requires a valid email address")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "is required")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        [Required(ErrorMessage = "is required")]
        public string ConfirmPassword { get; set; }
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }
        //For Address
        public long AddressId { get; set; }
        [Display(Name = "Address Line")]
        [Required(ErrorMessage = "is required")]
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Display(Name = "State")]
        public int State { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Display(Name = "Phone No")]
        // [Required(ErrorMessage = "is required")]
        [MinLength(10, ErrorMessage = "Enter at least ten digits")]
        //[StringLength(15, ErrorMessage = "Enter at least ten digits")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Phone Number!")]
        public string Phone { get; set; }


        [Display(Name = "Mobile No")]
        [Required(ErrorMessage = "is required")]
        [MinLength(10, ErrorMessage = "Enter at least ten digits")]
        //[StringLength(15, ErrorMessage = "Enter at least ten digits")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Mobile Number!")]
        public string Moblie { get; set; }

        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
        public int AddressType { get; set; }

        public const string ACTION_INDEX = "index";
        public const string ACTION_BLOCKED = "block";
        public const string ACTION_DELETED = "delete";


        public enum StatusTypes { Active = 1, Blocked = 2, Deleted = 3, NotVerified = 4 }
        public UserModel()
        {
            List<CLayer.Salutation> salutations = BLayer.Salutation.GetAll();
            Salutations = new SelectList(salutations, "SalutationId", "Title");

            List<CLayer.Country> countries = BLayer.Country.GetAll();
            CountryList = new SelectList(countries, "CountryId", "Name");

            if (countries.Count > 0)
            {
                List<CLayer.State> states = BLayer.State.GetOnCountry((int)countries[0].CountryId);
                StateList = new SelectList(states, "StateId", "Name");
                List<CLayer.City> cities = null;
                if (states.Count > 0)
                {
                    cities = BLayer.City.GetOnState((int)states[0].StateId);
                }
                cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                CityList = new SelectList(cities, "CityId", "Name");
            }


            List<SelectListItem> statusItems = new List<SelectListItem>();
            statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Active.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Active).ToString() });
            statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Draft.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Disabled).ToString() });
            StatusList = new SelectList(statusItems, "Value", "Text");

            Array sexlist = Enum.GetValues(typeof(CLayer.ObjectStatus.Sex));
            List<SelectListItem> sexItems = new List<SelectListItem>(sexlist.Length);
            foreach (var i in sexlist)
            {
                sexItems.Add(new SelectListItem { Text = Enum.GetName(typeof(CLayer.ObjectStatus.Sex), i), Value = ((int)i).ToString() });
            }
            SexList = new SelectList(sexItems, "Value", "Text");


            Array types = Enum.GetValues(typeof(CLayer.Role.Roles));
            List<SelectListItem> typeItems = new List<SelectListItem>(types.Length);
            CLayer.Role.Roles ty;
            for (int j = 3; j <= types.Length; j++)
            {
                ty = (CLayer.Role.Roles)j;
                typeItems.Add(new SelectListItem { Text = ty.ToString(), Value = j.ToString() });
            }
            UserTypes = new SelectList(typeItems, "Value", "Text");
        }
        public void LoadPlaces()
        {
            List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
            StateList = new SelectList(states, "StateId", "Name");
            List<CLayer.City> cities = null;
            if (states.Count > 0)
            {
                cities = BLayer.City.GetOnState(State);
            }
            cities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            CityList = new SelectList(cities, "CityId", "Name");
        }

    }

    public class ProfileModel
    {
        public long UserId { get; set; }
        [Display(Name = "Salutation")]
        public int SalutationId { get; set; }
        public SelectList Salutations { get; set; }
        // [Required(ErrorMessage = " is required")]
        [Display(Name = "Business Name")]
        public string Businessname { get; set; }
        //[Required(ErrorMessage = " is required")]
        [Display(Name = "Contact Name")]
        //[Required(ErrorMessage = "is required")]
        public string ContactName { get; set; }
        // For Affiliate
        [Required(ErrorMessage = " is required")]
        [Display(Name = "Company Reg No")]
        public string CompanyRegNo { get; set; }
        [Required(ErrorMessage = " is required")]
        [Display(Name = "Service Tax Reg No")]
        public string ServiceTaxRegNo { get; set; }
        //[Required(ErrorMessage = " is required")]
        [Display(Name = "First Name")]
        //[Required(ErrorMessage = "is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
        public int Sex { get; set; }
        [Display(Name = "Gender")]
        public SelectList SexList { get; set; }
        public enum sex { Female = 1, Male = 2 }
        public int UserTypeId { get; set; }
        [Display(Name = "User Type")]
        public SelectList UserTypes { get; set; }
        public int Status { get; set; }
        [Display(Name = "Email")]
        //[Required(ErrorMessage = "is required")]
        // [EmailAddress(ErrorMessage = "requires a valid email address")]
        public string Email { get; set; }
        public CLayer.Address Busineessaddressarray { get; set; }
        //For Address
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
        //[Required(ErrorMessage = " is required")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Pin Code")]
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }

        [Display(Name = "Phone")]
        // [MinLength(10, ErrorMessage = "Enter at least ten digits")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Phone Number!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = " is required")]
        [Display(Name = "Mobile No")]
        //[MinLength(10, ErrorMessage = "Enter at least ten digits")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Mobile Number!")]
        public string Mobile { get; set; }
        [Display(Name = "Pin Code")]
        public string BillingZipCode { get; set; }

        // For Billing Address for Corporate
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
        public int AssistedCorporateID { get; set; }
        public SelectList CorporateUserNames { get; set; }
        public SelectList AssistedCorporateUserName { get; set; }

        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
        public int AddressType { get; set; }
        public int BillingAddressType { get; set; }
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
        public bool IsClicked { get; set; }
        [Display(Name = "Contact Designation")]
        public string ContactDesignation { get; set; }
        public ProfileModel()
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
                else
                {
                    cities = new List<CLayer.City>();
                }
                cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                CityList = new SelectList(cities, "CityId", "Name");
                BillingCityList = new SelectList(cities, "CityId", "Name");
            }
            CompanyRegNo = "none";
            ServiceTaxRegNo = "none";
            List<SelectListItem> sexItems = new List<SelectListItem>();

            sexItems.Add(new SelectListItem { Text = sex.Male.ToString(), Value = ((int)sex.Male).ToString() });
            sexItems.Add(new SelectListItem { Text = sex.Female.ToString(), Value = ((int)sex.Female).ToString() });

            SexList = new SelectList(sexItems, "Value", "Text");

            Array types = Enum.GetValues(typeof(CLayer.Role.Roles));
            List<SelectListItem> typeItems = new List<SelectListItem>(types.Length);
            CLayer.Role.Roles ty;
            for (int j = 3; j <= types.Length; j++)
            {
                ty = (CLayer.Role.Roles)j;
                typeItems.Add(new SelectListItem { Text = ty.ToString(), Value = j.ToString() });
            }
            UserTypes = new SelectList(typeItems, "Value", "Text");

            //This is Done by Rahul on 09-01-2020 
            //For getting AssistedCorporate User Id in the Corporate MyProfile Page 
            var myid = System.Web.HttpContext.Current.User.Identity.GetUserId();//This is for getting Login User Id
            List<CLayer.B2BUser> CorporateUserList = BLayer.B2BUser.getoncorporateusername(myid);
            CorporateUserNames = new SelectList(CorporateUserList, "B2BId", "B2BId");
            //Below Code is used for AssistedCorporate User list
            if (CorporateUserList.Count > 0)
            {
                List<CLayer.B2BUser> CorporateAssistedUserList = BLayer.B2BUser.getAllAssistedCorporateUserNames((int)CorporateUserList[0].B2BId, myid);
                AssistedCorporateUserName = new SelectList(CorporateAssistedUserList, "UserId", "FirstName");
            }
            else
            {
                List<CLayer.B2BUser> CorporateAssistedUserList = BLayer.B2BUser.getAllCorporateUserName(myid);
                AssistedCorporateUserName = new SelectList(CorporateAssistedUserList, "UserId", "FirstName");
            }
        }


        public void LoadPlaces()
        {
            //List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
            //StateList = new SelectList(states, "StateId", "Name");

            List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
            StateList = new SelectList(states, "StateId", "Name");
            List<CLayer.City> cities = null;
            if (states.Count > 0)
            {
                cities = BLayer.City.GetOnState(State);
            }
            else
            {
                cities = new List<CLayer.City>();
            }
            cities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            CityList = new SelectList(cities, "CityId", "Name");
            //billing address 

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