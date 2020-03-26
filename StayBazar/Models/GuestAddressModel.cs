using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class GuestAddressModel
    {
        [Required(ErrorMessage = "is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "is required")]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "is required")]
        public string Address { get; set; }
        public string City { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        [Display(Name = "State")]
        public int State { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        public string Pincode { get; set; }
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
        public int AddressType { get; set; }

        public GuestAddressModel()
        {
            

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

         }

        //public void LoadPlaces()
        //{
        //    List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
        //    StateList = new SelectList(states, "StateId", "Name");
        //   StateList = new SelectList(states, "StateId", "Name");
        //    List<CLayer.City> cities = null;
        //    if (states.Count > 0)
        //    {
        //        cities = BLayer.City.GetOnState(State);
        //    }
        //    cities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
        //    CityList = new SelectList(cities, "CityId", "Name");
        //   CityList = new SelectList(cities, "CityId", "Name");
        //}
    }
}