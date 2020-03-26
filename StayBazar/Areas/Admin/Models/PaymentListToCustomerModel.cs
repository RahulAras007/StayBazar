using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace StayBazar.Areas.Admin.Models
{
    public class PaymentListToCustomerModel
    {
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public SelectList ListTypes { get; set; }
        public enum RegularUserValues { Name = 1, Email = 2, City = 3, Phone = 4, OrderID = 5 }
        public int SearchValue { get; set; }// The type of the search string
        public List<CLayer.OfflineBooking> userlist { get; set; }
        public int Status { get; set; }
        public int currentPage { get; set; }
        public long TotalRows { get; set; }
        public SelectList PropertyStatusTypes { get; set; }
        public SelectList StatusTypes { get; set; }
        public int InventoryAPIType { get; set; }//CLayer.ObjectStatus.StatusType
        public SelectList InventoryAPITypes { get; set; }
        public PaymentListToCustomerModel()
        {


        }
    }
    public class CustomerDetailModel
    {
        public long UserId { get; set; }
        public long OfflineBookingId { get; set; }
        [Display(Name = "Accommodatoin Name")]
        [Required(ErrorMessage = "Select Accommodation Type")]
        public string AccommodationTypeName { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = " is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = " is required")]
        [EmailAddress(ErrorMessage = "Requires a valid email address")]
        public string Email { get; set; }


        public List<CLayer.User> Bookinglist { get; set; }
        public string SearchString { get; set; }
        public List<CLayer.OfflineBooking> OfflineBookingList { get; set; }

        [Display(Name = "Advance Received")]
        public decimal AdvanceReceived { get; set; }

    }
    //public void LoadPlaces()
    //{
    //    List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
    //    StateList = new SelectList(states, "StateId", "Name");
    //    List<CLayer.City> cities = new List<CLayer.City>();
    //    if (states.Count > 0)
    //    {
    //        cities = BLayer.City.GetOnState(State);
    //    }
    //    cities.Add(new CLayer.City() { CityId = 0, Name = "Other" });
    //    CityList = new SelectList(cities, "CityId", "Name");

    //    List<CLayer.State> Billingstates = BLayer.State.GetOnCountry(BillingCountryId);
    //    BillingStateList = new SelectList(Billingstates, "StateId", "Name");
    //    List<CLayer.City> Billingcities = null;
    //    if (Billingstates.Count > 0)
    //    {
    //        Billingcities = BLayer.City.GetOnState(BillingState);
    //    }
    //    else
    //    {
    //        Billingcities = new List<CLayer.City>();
    //    }
    //    Billingcities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
    //    BillingCityList = new SelectList(Billingcities, "CityId", "Name");
    //}
    public class CustomerPaymentDetailModel
    {
        public long UserId { get; set; }
        public long OfflineBookingId { get; set; }
        [Display(Name = "Accommodatoin Name")]
        [Required(ErrorMessage = "Select Accommodation Type")]
        public string AccommodationTypeName { get; set; }

        public List<CLayer.User> Bookinglist { get; set; }
        public string SearchString { get; set; }
        public List<CLayer.OfflineBooking> OfflineBookingList { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = " is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Advance Received")]
        public decimal  AdvanceReceived { get; set; }
    }
}

//}