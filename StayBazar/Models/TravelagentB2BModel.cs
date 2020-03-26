using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class TravelagentB2BModel
    {
        public long B2BId { get; set; }
        [Display(Name = "Business Name")]
        [Required(ErrorMessage = "Is requred")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Is required")]
        [Display(Name = "Contact Name")]
        public string ContactName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Is required")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+[A-Za-z0-9.-]+\.[A-Za-z] {2,4}",
        //ErrorMessage = "Email is not valid")]  
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Is required")]
        [Display(Name = "Service Tax Reg.No")]
        public string ServiceTaxRegNo { get; set; }
        [Display(Name = "VAT Reg.No")]
        public string VATRegNo { get; set; }
        public int UserType { get; set; }       
        // For Address
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Is required")]
        public string Address { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        public string City { get; set; }
        [Display(Name = "State")]
        public int State { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Is required")]
        [MinLength(10, ErrorMessage = "Enter at least ten digits")]
        //[StringLength(15, ErrorMessage = "Enter at least ten digits")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Phone Number!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Is required")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Phone Number!")]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Is required")]
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }
        public SelectList CountryList { get; set; }
        public SelectList CityList { get; set; }
        public SelectList StateList { get; set; }       
        // For Bank Details for Supplier
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
        [Display(Name = "PAN No")]
        public string PANNo { get; set; }
        // Property Details for Supplier
        [Display(Name = "Property Description")]
        public string PropertyDescription { get; set; }
        [Display(Name = "No. Of Properties available")]
        public int AvailableProperties { get; set; }
        // For Billing Address for Corporate
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
        //

        public HttpPostedFileBase ServiceTaxReg { get; set; }
        public HttpPostedFileBase VATReg { get; set; }
        public HttpPostedFileBase BusinessRegistrationCertificate { get; set; }
        public HttpPostedFileBase PANCard { get; set; }
        public HttpPostedFileBase CopyOfCheque { get; set; }

        public TravelagentB2BModel()
        {
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
              //  cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                CityList = new SelectList(cities, "CityId", "Name");
                BillingCityList = new SelectList(cities, "CityId", "Name");
            }
        }
        public void LoadPlaces()
        {
            List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
            StateList = new SelectList(states, "StateId", "Name");
            BillingStateList = new SelectList(states, "StateId", "Name");
            List<CLayer.City> cities = null;
            if (states.Count > 0)
            {
                cities = BLayer.City.GetOnState(State);
            }
            cities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            CityList = new SelectList(cities, "CityId", "Name");
            BillingCityList = new SelectList(cities, "CityId", "Name");
        }
    }
}