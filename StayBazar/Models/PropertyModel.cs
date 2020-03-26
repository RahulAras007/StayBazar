using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Security;

namespace StayBazar.Models
{
    public class PropertyModel
    {
        public long PropertyId { get; set; }
        public long OwnerId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        [AllowHtml]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Display(Name = "Description")]
        [AllowHtml]
        public string PropertyDescription { get; set; }
        [Display(Name = "Location")]
        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }
        [Display(Name = "Status")]
        public int Status { get; set; }
        [Display(Name = "Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        //    public int CountryId { get; set; }
        [Display(Name = "Country")]
        public int Country { get; set; }
        [Display(Name = "State")]
        public int State { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        public string City { get; set; }
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }
        public string PositionLat { get; set; }
        public string PositionLng { get; set; }
        [EmailAddress(ErrorMessage = "requires a valid email address")]
        public string Email { get; set; }
        public SelectList StatusList { get; set; }
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
        public bool CameFromSearchPage { get; set; }
        #region sections
        //   public PropertyRates Rates { get; set; }
        public PropertyLandmarkModel Landmarks { get; set; }
        public PropertyPicturesModel Pictures { get; set; }
        public PropertyFeaturesModel Features { get; set; }
        public PropertyAccommmodationModel Accommodations { get; set; }
        #endregion

        public SelectList CheckHourlist { get; set; }
        public SelectList CheckMinlist { get; set; }
        public SelectList Checkclock { get; set; }
        [Display(Name = "Check-in Time(AM/PM)")]
        public string CheckInTime { get; set; }
        public string CheckInhr { get; set; }
        public string CheckInmin { get; set; }
        public string CheckInclock { get; set; }
        [Display(Name = "Check-out Time(AM/PM)")]
        public string CheckOutTime { get; set; }
        public string CheckOuthr { get; set; }
        public string CheckOutmin { get; set; }
        public string CheckOutClock { get; set; }
        [Display(Name = "Children Age Limit")]
        public int AgeLimit { get; set; }

        public string PageTitle { get; set; }
        public string MetaDescription { get; set; }

        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }
        public Models.SimpleSearchModel Filter { get; set; }
        public List<CLayer.Condition> AccConditions { get; set; }

        public string ActiveTab { get; set; }

        [Display(Name = "Distance From City (KM) - Leave as 0 for automatic calculation")]
        public Decimal DistanceFromCity { get; set; }
        [Display(Name = "Property For")]
        public int PropertyFor { get; set; }
        public int Rating { get; set; }
        public CLayer.Role.Roles UserType { get; set; }

        public string HotelId { get; set; }
        public string TBOHotelId { get; set; }
        public string TBOFlag { get; set; }
        public string TamarindHotelId { get; set; }
        public string TamarindFlag { get; set; }
        public int InventoryAPIType{ get; set; }

        public string SecurityToken { get; set; }
        public string SessionId { get; set; }
        public string SequenceNumber { get; set; }
        public string hotelcode { get; set; }
       // public string Address { get; set; }

            public string Image { get; set; }
        public string FormattedDescription { get; set; }

        public string RateCardDetailedId { get; set; }
        public decimal TaxPercentage { get; set; }

        public int GSTSlab { get; set; }

        public CLayer.Accommodation Accommodationss = new CLayer.Accommodation();


        //*Added by rahul on 09/03/20
        public string BookingForSelf { get; set; }
        public int CorporateID { get; set; }
        public int CorporateUserID { get; set; }
        public SelectList CorporateUserName { get; set; }
        public SelectList CorporateName { get; set; }
        public string NewBillingFor { get; set; }
        public int AssistedCorporateUserID { get; set; }
        public SelectList CorporateUserName_ForCorporateUsers { get; set; }
        public SelectList CorporateName1 { get; set; }
        public SelectList MyAssistedUsers { get; set; }
        //*
        public PropertyModel()
        {
            UserType = CLayer.Role.Roles.Customer;
            City = "";
            CityId = -1;
            AccConditions = new List<CLayer.Condition>();
            Filter = new SimpleSearchModel();
            List<SelectListItem> statusItems = new List<SelectListItem>();
            statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Active.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Active).ToString() });
            statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Draft.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Draft).ToString() });
            statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Disabled.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Disabled).ToString() });
            //statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Deleted.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Active).ToString() });
            StatusList = new SelectList(statusItems, "Value", "Text");
            CameFromSearchPage = true;
            List<CLayer.Country> countries = BLayer.Country.GetAllForProperty();
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
                else { cities = new List<CLayer.City>(); }
                cities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
                CityList = new SelectList(cities, "CityId", "Name");
            }
            Pictures = new PropertyPicturesModel();
            Features = new PropertyFeaturesModel();
            Accommodations = new PropertyAccommmodationModel();
            CheckHourlist = new SelectList(new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" });
            CheckMinlist = new SelectList(new[] { "00", "15", "30", "45" });
            Checkclock = new SelectList(new[] { "AM", "PM" });
            Status = (int)CLayer.ObjectStatus.StatusType.Active;



            List<CLayer.B2BUser> CorporateList = BLayer.B2BUser.GetCorporateName();
            CorporateName = new SelectList(CorporateList, "B2BId", "FirstName");
            //For getting Corporate User's Name under Corporate
            List<CLayer.B2BUser> CorporateUserList = BLayer.B2BUser.GetOnCorporateUserList((int)CorporateList[0].B2BId);
            CorporateUserName = new SelectList(CorporateUserList, "UserId", "FirstName");

            var myid = System.Web.HttpContext.Current.User.Identity.GetUserId();//This is for getting Login User Id
            List<CLayer.B2BUser> CorporateName_UnderLogin = BLayer.B2BUser.getoncorporateusername(myid);
            CorporateName1 = new SelectList(CorporateName_UnderLogin, "B2BId", "B2BId");
            //Below Code is used for AssistedCorporate User list
            if (CorporateName_UnderLogin.Count > 0)
            {
                List<CLayer.B2BUser> CorporateAssistedUserList = BLayer.B2BUser.getAllAssistedCorporateUserNames((int)CorporateName_UnderLogin[0].B2BId, myid);
                CorporateUserName_ForCorporateUsers = new SelectList(CorporateAssistedUserList, "UserId", "FirstName");
            }
            else
            {
                List<CLayer.B2BUser> CorporateAssistedUserList = BLayer.B2BUser.getAllCorporateUserName(myid);
                CorporateUserName_ForCorporateUsers = new SelectList(CorporateAssistedUserList, "UserId", "FirstName");
            }

            List<CLayer.B2BUser> MyAssistedUserList = BLayer.B2BUser.getMyAssistedUsername(myid);
            MyAssistedUsers = new SelectList(MyAssistedUserList, "UserId", "FirstName");
        }

        public void LoadPlaces()
        {
            List<CLayer.State> states = BLayer.State.GetOnCountry(Country);
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
            cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
            CityList = new SelectList(cities, "CityId", "Name");
        }
    }

    public class PropertyRatesModel
    {
        public long PropertyId { get; set; }
        [Display(Name = "Daily Rate")]
        public decimal DailyRate { get; set; }
        [Display(Name = "Rate For Individual")]
        public decimal RateForIndividual { get; set; }
        [Display(Name = "Monthly Rate")]
        public decimal MonthlyRate { get; set; }
        [Display(Name = "Weekly Rate")]
        public decimal WeeklyRate { get; set; }
    }

    public class PropertyPicturesModel
    {
        public long PropertyId { get; set; }
        public long FileId { get; set; }
        public string FilenName { get; set; }
        public HttpPostedFileBase photo { get; set; }
        public List<CLayer.PropertyFiles> Pictures { get; set; }

        public PropertyPicturesModel()
        {
            Pictures = new List<CLayer.PropertyFiles>();
        }
    }

    public class PropertyFeaturesModel
    {
        public long PropertyId { get; set; }
        public List<CLayer.PropertyFeature> Features { get; set; }
        public string FeatureSet { get; set; }

        public PropertyFeaturesModel()
        {
            Features = new List<CLayer.PropertyFeature>();
        }
    }

    public class AccommodationPicturesModel
    {
        public long FileId { get; set; }
        public long AccommodationId { get; set; }
        public long PropertyId { get; set; }
        public string FileName { get; set; }
        public HttpPostedFileBase AccommodationPhoto { get; set; }
        public List<CLayer.AccommodationFiles> AccommodationPictures { get; set; }

        public AccommodationPicturesModel()
        {
            AccommodationPictures = new List<CLayer.AccommodationFiles>();
        }
    }

    public class AccommodationFeaturesModel
    {
        public long AccommodationId { get; set; }
        public List<CLayer.AccommodationFeature> Features { get; set; }
        public string FeatureSet { get; set; }

        public AccommodationFeaturesModel()
        {
            Features = new List<CLayer.AccommodationFeature>();
        }
    }

    public class AccommodationModel
    {
        public long AccommodationId { get; set; }
        [Display(Name = "Accommodation type")]
        public int AccommodationTypeId { get; set; }
        public SelectList AccommodationTypes { get; set; }
        [Display(Name = "Stay category")]
        public int StayCategoryId { get; set; }
        public SelectList StayCategories { get; set; }
        [Display(Name = "Number of allocated accommodations")]
        public int AccommodationCount { get; set; }
        public long PropertyId { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }
        [Display(Name = "Description")]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "Description")]
        [AllowHtml]
        public string AccDescription { get; set; }
        [Display(Name = "Max. number of people(includes guests)")]
        public int MaxNoOfPeople { get; set; }
        [Display(Name = "Number of children")]
        public int MaxNoOfChildren { get; set; }
        [Display(Name = "Min. number of people")]
        public int MinNoOfPeople { get; set; }
        public int BedRooms { get; set; }
        public string RoomId { get; set; }
        public string RoomType { get; set; }
        public string RoomTypeCode { get; set; }
        public string SourceofBusiness { get; set; }

        public decimal Area { get; set; }
        [Display(Name = "Total accommodations")]
        public int TotalAccommodations { get; set; }
        [Display(Name = "Status")]
        public int Status { get; set; }
        public SelectList StatusList { get; set; }
        public enum StatusTypes { Enabled = 1, Disabled = 2 }

        public AccommodationPicturesModel AccommodationPictures { get; set; }
        public AccommodationFeaturesModel AccommodationFeatures { get; set; }

        public string ActiveTab { get; set; }

        public string BookingCode { get; set; }

        public string RatePlanCode { get; set; }

        public int RoomStayRPH { get; set; }

        public string RoomTypeDescription { get; set; }

        //karthikms added this code on 24-04-2020 for inventory save
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public decimal Amount { get; set; }
        public decimal AmountWithTax { get; set; }

        public AccommodationModel()
        {
            List<CLayer.AccommodationType> acctypes = BLayer.AccommodationType.GetAll();
            AccommodationTypes = new SelectList(acctypes, "AccommodationTypeId", "Title");

            List<CLayer.StayCategory> categories = BLayer.StayCategory.GetActiveList();
            StayCategories = new SelectList(categories, "CategoryId", "Title");

            AccommodationPictures = new AccommodationPicturesModel();

            Array status = Enum.GetValues(typeof(StatusTypes));
            List<SelectListItem> statusItems = new List<SelectListItem>(status.Length);
            foreach (var i in status)
            {
                statusItems.Add(new SelectListItem { Text = Enum.GetName(typeof(StatusTypes), i), Value = ((int)i).ToString() });
            }
            StatusList = new SelectList(statusItems, "Value", "Text");
        }
    }

    public class PropertyAccommmodationModel
    {
        public long PropertyId { get; set; }
        public AccommodationModel Accommodation { get; set; }
        public List<CLayer.Accommodation> Accommodations { get; set; }



        public PropertyAccommmodationModel()
        {
            Accommodation = new AccommodationModel();
            Accommodations = new List<CLayer.Accommodation>();
        }
    }

    public class LandmarkModel
    {

        public long LandmarkId { get; set; }
        public long PropertyId { get; set; }
        [Display(Name = "Landmark")]
        public int LandmarkTitleId { get; set; }
        public string Landmark { get; set; }
        [Display(Name = "Range")]
        public decimal Range { get; set; }
        public SelectList LandmarksTitles { get; set; }

        public LandmarkModel()
        {
            List<CLayer.LandmarkTitles> titles = BLayer.LandmarkTitles.GetAll();
            titles.Add(new CLayer.LandmarkTitles() { LandmarkTitleId = -1, LandmarkTitle = "Other" });
            LandmarksTitles = new SelectList(titles, "LandmarkTitleId", "LandmarkTitle");
        }
    }

    public class PropertyLandmarkModel
    {
        public long PropertyId { get; set; }
        public LandmarkModel Landmark { get; set; }
        public List<CLayer.Landmarks> Landmarks { get; set; }

        public PropertyLandmarkModel() { Landmarks = new List<CLayer.Landmarks>(); }
    }

    public class PropertyMostpopularModel
    {
        public long PropertyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public CLayer.ObjectStatus.StatusType Status { get; set; }
        public long OwnerId { get; set; }
        public string PositionLat { get; set; }
        public string PositionLng { get; set; }
        public string Address { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int Rating { get; set; }
        public int NoOfAccommodations { get; set; }
        public string Countryname { get; set; }
        public string Statename { get; set; }
        public string SupplierName { get; set; }
        public decimal bookingitemsTotalAmount { get; set; }
        public List<CLayer.Property> PropertypopularList { get; set; }
        public PropertyMostpopularModel()
        {
            PropertypopularList = new List<CLayer.Property>();
        }
    }

    public class DealsOftheday
    {
        public long PropertyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public CLayer.ObjectStatus.StatusType Status { get; set; }
        public long OwnerId { get; set; }
        public string PositionLat { get; set; }
        public string PositionLng { get; set; }
        public string Address { get; set; }
        public int Country { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public int Rating { get; set; }
        public int NoOfAccommodations { get; set; }
        public string Countryname { get; set; }
        public string Statename { get; set; }
        public string SupplierName { get; set; }
        public decimal bookingitemsTotalAmount { get; set; }
        public List<CLayer.Property> PropertypopularList { get; set; }
        public DealsOftheday()
        {
            PropertypopularList = new List<CLayer.Property>();
        }
    }
}