using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data;
namespace StayBazar.Areas.Admin.Models
{
    public class TaxModel
    {
        public int TaxId { get; set; }
        public int TaxTitleId { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]    
        public string Title { get; set; }

        [Display(Name = "Rate")]
        [Required(ErrorMessage = "Rate is required")]
        public decimal Rate { get; set; }

        [Display(Name = "Country")]
        public int? CountryId { get; set; }
        public string Country { get; set; }

        [Display(Name = "State")]
        public int? StateId { get; set; }
        public string State { get; set; }

        [Display(Name = "City")]
        public int? CityId { get; set; }
        public string City { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Status")]
        public int Status { get; set; }

        [Required(ErrorMessage = "Start date required")]
        [DataType(DataType.Date)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = "Start date required")]
        [DataType(DataType.Date)]
        public string EndDate { get; set; }

        [Display(Name = "On Date")]
        public int OnDate { get; set; }

        [Display(Name = "Apply on Total Amount")]
        public bool OnTotalAmount { get; set; }

        public bool Unlimited { get; set; }

        [Display(Name = "Price Slab")]
        public decimal PriceUpto { get; set; }

        public long UpdatedBy { get; set; }


        public DateTime UpdatedDate { get; set; }

        [Display(Name = "Is Standard")]
        public bool IsStandard { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }



        public enum OnDateE { Booking = 1, Checkin  = 2 }
       
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
        public SelectList TaxTitleList { get; set; }

        //for select property

        public long AccommodationId { get; set; }
        public long PropertyId { get; set; }
          [Display(Name = "Property")]
        public string PropertyTitle { get; set; }
        public int StayCategoryId { get; set; }
        public enum serchacc { Property = 1, City = 2, State = 3, Supplier = 4 }  
        public AccommodationModel Accommodation { get; set; }//Model        
        public List<CLayer.Accommodation> Accommodations { get; set; }
        public string SearchString { get; set; }
        public string SearchStringForAccommodation { get; set; }
        public int SearchValueForAccommodation { get; set; }
        public int SearchValueForProperty { get; set; }
        public int SearchValue { get; set; }
        public string Ids { get; set; }
        public List<CLayer.Tax> PropertyTaxlist { get; set; }
        public DataTable PropertyTaxReport { get; set; }
        public long SPropertyId { get; set; }
        public int STaxTitle { get; set; }
        public decimal SRate { get; set; }
        public long SAccommodationId { get; set; }
        public int SStayCategoryId { get; set; }

        public TaxModel()
        {
            StartDate = DateTime.Today.ToShortDateString();
            EndDate = DateTime.Today.AddDays(1).ToShortDateString();

            List<CLayer.Country> countries = BLayer.Country.GetAll();
            countries.Insert(0, new CLayer.Country() { CountryId = 0, Name = "Select" });
            CountryList = new SelectList(countries, "CountryId", "Name");
            if (countries.Count > 0)
            {
                List<CLayer.State> states = BLayer.State.GetOnCountry((int)countries[0].CountryId);
                states.Insert(0, new CLayer.State() { StateId = 0, Name = "Select" });
                StateList = new SelectList(states, "StateId", "Name");
                List<CLayer.City> cities = null;
                if (states.Count > 0)
                {
                    cities = BLayer.City.GetOnState((int)states[0].StateId);
                }
                cities.Insert(0, new CLayer.City() { CityId = 0, Name = "Select" });
                cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                CityList = new SelectList(cities, "CityId", "Name");
            }

            List<CLayer.TaxTitle> taxtitle = BLayer.TaxTitle.GetAll();
            TaxTitleList = new SelectList(taxtitle, "TaxTitleId", "Title");
            Accommodations = new List<CLayer.Accommodation>();
            Accommodation = new AccommodationModel();//Model

        }
        public TaxModel(int countryId,int stateId, int cityId)
        {
            StartDate = DateTime.Today.ToShortDateString();
            EndDate = DateTime.Today.AddDays(1).ToShortDateString();

            List<CLayer.Country> countries = BLayer.Country.GetAll();
            CountryList = new SelectList(countries, "CountryId", "Name");
            List<CLayer.State> states;
            List<CLayer.City> cities = new List<CLayer.City>();

            if (countryId > 0)
            {
                states = BLayer.State.GetOnCountry(countryId);
                
            }
            else
                states = new List<CLayer.State>();


            if (stateId > 0)
            {             
                cities = BLayer.City.GetOnState(stateId);
            }
            else
            cities = new List<CLayer.City>();
           

            states.Insert(0, new CLayer.State() { CountryId = -1, Name = "Select" });
            cities.Insert(0, new CLayer.City() { CityId = -1, Name = "Select" });

            StateList = new SelectList(states, "StateId", "Name");
            CityList = new SelectList(cities, "CityId", "Name");
            CountryId = countryId;
            StateId = stateId;
            CityId = cityId;
            List<CLayer.TaxTitle> taxtitle = BLayer.TaxTitle.GetAll();
            TaxTitleList = new SelectList(taxtitle, "TaxTitleId", "Title");
        }      
    }


}