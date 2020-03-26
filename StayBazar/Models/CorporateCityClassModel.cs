using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class CorporateCityClassModel
    {
    public long CityClassID { get; set; }
        [Required]
        [Display(Name = "City Class Code")]
        public string CityClassCode { get; set; }
        [Display(Name = "City Class Description")]
        public string CityClassDescription { get; set; }
        public long CountryID { get; set; }
        public long CityID { get; set; }
        public long StateID { get; set; }
        public long B2B_ID { get; set; }

        public string CountryIDs { get; set; }
        public string StateIDs { get; set; }
        public string CityIDs { get; set; }

        public string CountryNames { get; set; }
        public string StateNames { get; set; }
        public string CityNames { get; set; }

        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }
        public int CityId { get; set; }
        [Display(Name = "State")]
        public int State { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }

        public List<CLayer.CityClass> CityClass { get; set; }

        public CorporateCityClassModel()
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
                //cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                CityList = new SelectList(cities, "CityId", "Name");
            }

        }

     


    }

}
