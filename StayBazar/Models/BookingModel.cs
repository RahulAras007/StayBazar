using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;

namespace StayBazar.Models
{
    public class BookingModel
    {
        public CLayer.Property PropertyDetails { get; set; }
        public List<CLayer.BookingItem> Items { get; set; }
        public CLayer.Offers offeritems { get; set; }
        public CLayer.Booking BookingDetails { get; set; }
        public bool UserType { get; set; }
        public long BUBookingId { get; set; }
        public long BUUserId { get; set; }
        public long BookingId { get; set; }
        public int PropertyInventoryType { get; set; }
        public string PropertyTitle{ get; set; }
        public string PropertyAddress{ get; set; }

        //*Added by rahul on 09/03/20
        public string BookingForSelf { get; set; }
        public int CorporateID { get; set; }
        public int CorporateUserID { get; set; }
        public SelectList CorporateUserName { get; set; }
        public SelectList CorporateName { get; set; }
        public string NewBillingFor { get; set; }
        public int AssistedCorporateUserID { get; set; }
        public SelectList CorporateUserName_ForCorporateUsers { get; set; }
        //*
        public string ForUserFirstName{ get; set; }
        public string ForUserLastName{ get; set; }
        public string ForUserEmail{ get; set; }
        public string ForUserMobile{ get; set; }
        public long ForBookingUserId { get; set; }
        public BookingForUserModel Forbookings { get; set; }
        public long BookingItemId { get; set; }
        public CLayer.Address OrderedBy { get; set; }
        public CLayer.User Supplier { get; set; }
        public bool ForPrint { get; set; }
        public bool ForPdf { get; set; }
        public bool partialpayment { get; set; }

        public int InventoryAPIType { get; set; }
        public string LoggedInUserName { get; set; }

        //   public string OrderNo { get; set; }
        public BookingModel()
        {
            Items = new List<CLayer.BookingItem>();
            OrderedBy = new CLayer.Address();
            Forbookings = new BookingForUserModel();
            BookingDetails = new CLayer.Booking();
            offeritems = new CLayer.Offers();
            Supplier = new CLayer.User();
            ForPrint = false;
            ForPdf = false;

            List<CLayer.B2BUser> CorporateList = BLayer.B2BUser.GetCorporateName();
            CorporateName = new SelectList(CorporateList, "B2BId", "FirstName");
            //For getting Corporate User's Name under Corporate
            List<CLayer.B2BUser> CorporateUserList = BLayer.B2BUser.GetOnCorporateUserList((int)CorporateList[0].B2BId);
            CorporateUserName = new SelectList(CorporateUserList, "UserId", "FirstName");
        }
    }
    public class BookingForUserModel
    {
        public long BookingId { get; set; }
        public long UserId { get; set; }
        public long ForUser { get; set; }
        public long ForBookingUserId { get; set; }
        public string ForUserFirstName { get; set; }
        public string ForUserLastName { get; set; }
        public string ForUserEmail { get; set; }
        public string ForUserMobile { get; set; }
        public string ForUserAddress { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }

        public string ForState { get; set; }
        public string ForCity { get; set; }
        public string ForCountry { get; set; }
        public string ZipCode { get; set; }

        public enum SearchValuebtn { Name = 1, ID = 2 }
        public List<CLayer.BookingItem> Items { get; set; }
        public BookingForUserModel()
        {
            Items = new List<CLayer.BookingItem>();
        }
    }

    public class SimpleBookingModel
    {
        public string BookingData { get; set; }
        public DateTime BookCheckIn { get; set; }
        public DateTime BookCheckOut { get; set; }
        public long PropertyId { get; set; }
       
    }


    public class SaveBookingUserModel
    {
        public long BookingUserId { get; set; }
        public long UserId { get; set; }
        [Required(ErrorMessage = "is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Is required")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [Display(Name = "Email")]       
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
         [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid mobile Number!")]
        [Display(Name = "Mobile No")]
        [Required(ErrorMessage = "is required")]
        public string Mobile { get; set; }


        
        //Address
        public long AddressId { get; set; }
        [Required(ErrorMessage = "is required")]
        [Display(Name = "Address Line")]
        public string Address { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        public int CityId { get; set; }
        [Display(Name = "State")]
        [Range(1,int.MaxValue,ErrorMessage="is required")]
        public int State { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
         [Required(ErrorMessage = "is required")]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
        public int AddressType { get; set; }
         

        public SaveBookingUserModel()
        {


            List<CLayer.Country> countries = BLayer.Country.GetAll();
            CountryList = new SelectList(countries, "CountryId", "Name");

            if (countries.Count > 0)
            {
                List<CLayer.State> states = BLayer.State.GetOnCountry((int)countries[0].CountryId);
                StateList = new SelectList(states, "StateId", "Name");
                List<CLayer.City> cities = new List<CLayer.City>();
                if (states.Count > 0)
                {
                    cities = BLayer.City.GetOnState((int)states[0].StateId);
                    if (cities.Count > 0)
                        CityId = cities[0].CityId;
                    else
                        CityId = -1;
                }
                cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                CityList = new SelectList(cities, "CityId", "Name");
            }

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
            //cities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            //CityList = new SelectList(cities, "CityId", "Name");
        }
    }
}