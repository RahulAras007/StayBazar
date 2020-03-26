using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace StayBazar.Models
{
    public class OfflinePaymentModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

       
        [Display(Name = "Reference No")]
        public string ReferenceNumber { get; set; }
        [Required]
        [Range(1,1000000,ErrorMessage="Invalid amount")]
        [Display(Name = "Amount")]
        [DisplayFormat(DataFormatString = "{0:0.##}", ApplyFormatInEditMode = true)]
        public decimal Amount { get; set; }

        [Display(Name = "Comments")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string Mobile { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }


        public long AddressId { get; set; }

        [Required(ErrorMessage = " is required")]
        [Display(Name = "Address Line")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        public int CityId { get; set; }
        [Required]
        [Display(Name = "State")]
        public int State { get; set; }
        [Required]
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        [Required]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Pin Code")]
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }

        public int Gatewaytype { get; set; }

        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }

        public List<CLayer.OfflinePayment> OfflineBookingList { get; set; }

        public OfflinePaymentModel()
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
                else
                {
                    cities = new List<CLayer.City>();
                }
                cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                CityList = new SelectList(cities, "CityId", "Name");
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
        }

    }
}