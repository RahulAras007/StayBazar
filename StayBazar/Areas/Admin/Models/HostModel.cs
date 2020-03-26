using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace StayBazar.Areas.Admin.Models
{
    public class HostModel
    {
        public long UserId { get; set; }
        public int SalutationId { get; set; }
        [Display(Name = "Salutation")]
        public SelectList Salutations { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "is required")]
        public String FirstName { get; set; }
        [Display(Name = "Last Name")]
        public String LastName { get; set; }
        [Display(Name = "Date Of Birth")]
        public String DateOfBirth { get; set; }
        [Display(Name = "Sales Region")]
        public String SalesRegion { get; set; }
        public int Sex { get; set; }
        [Display(Name = "Gender")]
        public SelectList SexList { get; set; }
        public enum sex { Female = 1, Male = 2 }
        public int UserTypeId { get; set; }
        [Display(Name = "User Type")]
        public SelectList UserTypes { get; set; }
        // public enum types { Administrator = 1, Staff = 2, Supplier = 3, Agent = 4, Company = 5, CompanyUser = 6, Customer = 7, BusinessAffiliate = 8 }
        public int Status { get; set; }
        [Display(Name = "Status")]
        public SelectList StatusList { get; set; }
        public enum StatusTypes { Active = 1, Blocked = 2, Deleted = 3 }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "requires a valid email address")]
        public String Email { get; set; }

        public String Phone { get; set; }
        public String Mobile { get; set; }
        //OPS Email
        [Display(Name = "OPS Email")]
        public String OPSEmail { get; set; }


        public String PasswordHash { get; set; }
        [NotMapped]
       
        [System.Web.Mvc.CompareAttribute("PasswordHash", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassowrd { get; set; }
        public DateTime LastLoginOn { get; set; }
        public string SecurityStamp { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }

        //Action methods
        public const string ACTION_BLOCKED = "blocked";
        public const string ACTION_INDEX = "index";
        public const string ACTION_DELETED = "recentlydeleted";
        //


        public List<CLayer.SBEntity> SbEntityList { get; set; }
        public List<CLayer.SBEntity> SelectedSbEntity { get; set; }

        [Display(Name = "SB Entities")]
        public List<long> SbEntities { get; set; }

        //Address   ./Start
        public long AddressId { get; set; }
        [Required(ErrorMessage = "is required")]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        public string City { get; set; }
        [Display(Name = "State")]
        public int State { get; set; }
        [Display(Name = "Country")]
        public int CountryId { get; set; }
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }

        public SelectList B2BStateList { get; set; }
        [Display(Name = "Country")]
        public int BillingCountryId { get; set; }
        public SelectList BillingCountryList { get; set; }
        public SelectList BillingStateList { get; set; }
        public SelectList BillingCityList { get; set; }

        //  ./End
        public int CorporateID { get; set; }
        public SelectList CorporateName { get; set; }
        //public IList<string> SelectedCorporates { get; set; }

        public HostModel()
        {
            //Done by rahul
            //SelectedCorporates = new List<string>();
            //List<CLayer.B2BUser> CorporateList = BLayer.B2BUser.GetCorporateName();
            //CorporateName = new SelectList(CorporateList, "B2BId", "FirstName");

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
                cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                CityList = new SelectList(cities, "CityId", "Name");
                BillingCityList = new SelectList(cities, "CityId", "Name");

            }



            List<CLayer.Salutation> salutations = BLayer.Salutation.GetAll();
            Salutations = new SelectList(salutations, "SalutationId", "Title");

            Array sexlist = Enum.GetValues(typeof(sex));
            List<SelectListItem> sexItems = new List<SelectListItem>(sexlist.Length);
            foreach (var i in sexlist)
            {
                sexItems.Add(new SelectListItem { Text = Enum.GetName(typeof(sex), i), Value = ((int)i).ToString() });
            }
            SexList = new SelectList(sexItems, "Value", "Text");

            Array types = Enum.GetValues(typeof(CLayer.Role.Roles));
            List<CLayer.Role> rls = BLayer.RolePermissions.GetAllAdminSide();

            List<SelectListItem> typeItems = new List<SelectListItem>();
           
            for (int j = 0; j < rls.Count; j++)
            {
                
                typeItems.Add(new SelectListItem { Text = rls[j].Name, Value = rls[j].Id.ToString() });
            }

            UserTypes = new SelectList(typeItems, "Value", "Text");

            Array status = Enum.GetValues(typeof(StatusTypes));
            List<SelectListItem> statusItems = new List<SelectListItem>(status.Length);
            foreach (var i in status)
            {
                statusItems.Add(new SelectListItem { Text = Enum.GetName(typeof(StatusTypes), i), Value = ((int)i).ToString() });
            }
            StatusList = new SelectList(statusItems, "Value", "Text");

        

            //SbEntityList = new List<CLayer.SBEntity>();
            List<CLayer.SBEntity> SbEntityListdt = BLayer.SBEntity.GetAll();
            SbEntityList = new List<CLayer.SBEntity>();
            foreach (CLayer.SBEntity SBEntity in SbEntityListdt)
            {
                SbEntityList.Add(new CLayer.SBEntity
                {
                    EntityId = SBEntity.EntityId,
                    Name = SBEntity.State + " - " + SBEntity.Name
                });
            }         
        }
        public void LoadPlaces()
        {
            List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
            StateList = new SelectList(states, "StateId", "Name");

            List<CLayer.State> B2Bstates = BLayer.State.GetOnCountry(CountryId);
            B2BStateList = new SelectList(B2Bstates, "StateId", "Name");

            List<CLayer.City> cities = null;
            if (states.Count > 0)
            {
                cities = BLayer.City.GetOnState(State);
            }

            cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
            CityList = new SelectList(cities, "CityId", "Name");
        }
        public void LoadPlacesStaff()
        {
            CountryId = (CountryId < 1) ? 1 : CountryId;
            List<CLayer.State> states = BLayer.State.GetOnCountry(CountryId);
            StateList = new SelectList(states, "StateId", "Name");

            List<CLayer.State> B2Bstates = BLayer.State.GetOnCountry(CountryId);
            B2BStateList = new SelectList(B2Bstates, "StateId", "Name");

            List<CLayer.City> cities = null;
            if (states.Count > 0)
            {
                State = (State < 1) ? 3 : State;
                cities = BLayer.City.GetOnState(State);
            }

            cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
            CityList = new SelectList(cities, "CityId", "Name");
        }
     
    }

}