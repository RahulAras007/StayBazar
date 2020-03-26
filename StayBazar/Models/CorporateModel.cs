using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StayBazar.Lib.Security;

namespace StayBazar.Models
{
    public class CorporateModel
    {
        public List<CLayer.User> Users { get; set; }
        public CorporateUserModel CorpUser { get; set; }
        public string msg1 { get; set; }
        public SelectList AssistedStatusFlag { get; set; }
        public CorporateModel()
        {
            Users = new List<CLayer.User>();
            CorpUser = new CorporateUserModel();
            //List<CLayer.B2BUser> AssistedStatusFlagList = BLayer.B2BUser.GetAssistedBookingStatus();
            //AssistedStatusFlag = new SelectList(AssistedStatusFlagList, "AssistedBooking_Flag", "AssistedBooking_Flag");
        }
    }

    public class CorporateUserModel
    {
        public List<CLayer.B2BHotels> B2BHotelsList { get; set; }

        public List<CLayer.ApprovalOrder> ApprovalOrder { get; set; }
        public long UserId { get; set; }
        public long id { get; set; }
        public int AssistedBooking_Flag { get; set; }
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string Password { get; set; }

        [Display(Name = "Salutation")]
        public SelectList Salutations { get; set; }
        public int SalutationId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Date Of Birth")]
        public string DateOfBirth { get; set; }
       
        [Display(Name = "Gender")]
        public SelectList SexList { get; set; }
        public enum sex { Female = 1, Male = 2 }
        public int Sex { get; set; }

        [Display(Name = "User Type")]
        public int UserTypeId { get; set; }
        public SelectList UserTypes { get; set; }

        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }
        //public string Phone { get; set; }

        [Display(Name = "Status")]
        public SelectList Status { get; set; }
        public int StatusId { get; set; }

        /*
        [Display(Name = "Approval Order")]
        public SelectList ApprovalOrderStatus { get; set; }
        public int ApprovalOrderID { get; set; }
        */

        [Display(Name = "Approver")]
        public SelectList AprovalOrderApproverList { get; set; }
        public int ApproverID { get; set; }
        public string SelectedApproverText { get; set; }

        public long B2BApproverID { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "requires a valid email address")]
        public string Email { get; set; }

        [Display(Name = "Maximum Daily Entitlement")]
        [Required(ErrorMessage ="Is required")]
        public int MaximumDailyEntitlement { get; set; }

        [Display(Name = "Hotel Name")]
        [Required(ErrorMessage = "is required")]
        public string HotelName { get; set; }

        public long B2BPropertyID { get; set; }

        public List<CLayer.Property> HotelOrder { get; set; }








        //[Display(Name = "Password")]
        //[Required(ErrorMessage = "is required")]
        //public string Password { get; set; }
        //[Display(Name = "Confirm Password")]
        //[System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The new password and confirmation password do not match.")]
        //[Required(ErrorMessage = "is required")]
        //public string ConfirmPassword { get; set; }

        //
        //For Address
        //

        public string msg { get; set; }
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
        [Display(Name = "Employee Grade")]
        public int GradeID { get; set; }
        public int CostCentre { get; set; }
        [Required(ErrorMessage = "is required")]
        [Display(Name = "Phone")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Phone Number!")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "is required")]      
        [Display(Name = "Mobile")]
        [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Mobile Number!")]
        public string Mobile { get; set; }
        public SelectList EGradeList { get; set; }
        public SelectList ECostCentreList { get; set; }
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList B2BStateList { get; set; }
        //  public int AddressType { get; set; }
        [Display(Name = "Country")]
        public int BillingCountryId { get; set; }
        public SelectList BillingCountryList { get; set; }
        public SelectList BillingStateList { get; set; }
        public SelectList BillingCityList { get; set; }
       
        public SelectList CityList { get; set; }
        public SelectList HotelList { get; set; }

        public CorporateUserModel()
        {
            AddressId = 0;
            UserId = 0;
            List<CLayer.Salutation> salutations = BLayer.Salutation.GetAll();
            Salutations = new SelectList(salutations, "SalutationId", "Title");

            //To Get all approvers for approval order 'drop-down list'

            UserId = Convert.ToInt64(HttpContext.Current.Session["id"]);

            int loginId = Convert.ToInt32(HttpContext.Current.User.Identity.GetUserId());
            List<CLayer.ApprovalOrderApprover> approvalOrderApprover = BLayer.ApprovalOrderApprover.GetApprovalOrderApprover(loginId, (int)UserId);
            AprovalOrderApproverList = new SelectList(approvalOrderApprover, "userid", "ApproverName");



            //  ./End

            //Setting default hotels for coroprate users
      
            //List<CLayer.B2BHotels> objB2BHotels = BLayer.ApprovalOrderApprover.GetApprovalOrderApprover(loginId, (int)UserId);
            //AprovalOrderApproverList = new SelectList(approvalOrderApprover, "userid", "ApproverName");


            //end

            List<CLayer.Country> countries = BLayer.Country.GetAll();
            CountryList = new SelectList(countries, "CountryId", "Name");

            List<CLayer.EmployeeGrades> EGrades = BLayer.EmployeeGrades.GetAll();
            EGradeList = new SelectList(EGrades, "GradeID", "GradeCode");

            List<CLayer.CostCentre> ECostCentre = BLayer.CostCentre.GetAll();
            ECostCentreList = new SelectList(ECostCentre, "CostCentreCode", "CostcentreName");

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


                List<CLayer.Property> Hotels = null;
                if (cities.Count > 0)
                {
                    Hotels = BLayer.Property.PropertyGetOnCity((int)cities[0].CityId);
                }
                Hotels.Add(new CLayer.Property() { PropertyId  = -1, Title = "Other" });
                HotelList = new SelectList(Hotels, "PropertyId", "Title");

            }

            List<KeyValuePair<int, string>> statustypes = new List<KeyValuePair<int, string>>();
            statustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Active, CLayer.ObjectStatus.StatusType.Active.ToString()));
            statustypes.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.StatusType.Disabled, CLayer.ObjectStatus.StatusType.Disabled.ToString()));
            Status = new SelectList(statustypes, "Key", "Value");

           /* 
            List<KeyValuePair<int, string>> approvalOrder = new List<KeyValuePair<int, string>>();
            approvalOrder.Add(new KeyValuePair<int, string>(0, "Select Approval Order"));
            approvalOrder.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.ApprovalOrder.FirstLevel, CLayer.ObjectStatus.ApprovalOrder.FirstLevel.ToString()));
            approvalOrder.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.ApprovalOrder.SecondLevel, CLayer.ObjectStatus.ApprovalOrder.SecondLevel.ToString()));
            approvalOrder.Add(new KeyValuePair<int, string>((int)CLayer.ObjectStatus.ApprovalOrder.ThridLevel, CLayer.ObjectStatus.ApprovalOrder.ThridLevel.ToString()));
            ApprovalOrderStatus = new SelectList(approvalOrder, "Key", "Value"); 
            */


            Array sexlist = Enum.GetValues(typeof(sex));
            List<SelectListItem> sexItems = new List<SelectListItem>(sexlist.Length);
            foreach (var i in sexlist)
            {
                sexItems.Add(new SelectListItem { Text = Enum.GetName(typeof(sex), i), Value = ((int)i).ToString() });
            }
            SexList = new SelectList(sexItems, "Value", "Text");

            Array types = Enum.GetValues(typeof(CLayer.Role.CorporateRoles));
            List<SelectListItem> typeItems = new List<SelectListItem>(2);

            typeItems.Add(new SelectListItem { Text = CLayer.Role.CorporateRoles.Administrator.ToString(), Value = ((int)CLayer.Role.CorporateRoles.Administrator).ToString() });
            typeItems.Add(new SelectListItem { Text = CLayer.Role.CorporateRoles.Staff.ToString(), Value = ((int)CLayer.Role.CorporateRoles.Staff).ToString() });
            UserTypeId = (int)CLayer.Role.CorporateRoles.Staff;
            UserTypes = new SelectList(typeItems, "Value", "Text");

            ApprovalOrder = new List<CLayer.ApprovalOrder>();
            HotelOrder = new List<CLayer.Property>();
            B2BHotelsList = new List<CLayer.B2BHotels>();
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



    }
}