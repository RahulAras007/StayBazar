using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class PropertyModel
    {
        public long PropertyId { get; set; }
        public long OwnerId { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        [AllowHtml]
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
        [Display(Name = "Country")]
        public int Country { get; set; }
        [Display(Name = "State")]
        public int State { get; set; }
        [Display(Name = "City")]
        public int CityId { get; set; }
        public string City { get; set; }
        [EmailAddress(ErrorMessage = "requires a valid email address")]
        public string Email { get; set; }
        [Display(Name = "Pin Code")]
        public string ZipCode { get; set; }
        public string PositionLat { get; set; }
        public string PositionLng { get; set; }
        public string Image { get; set; }
        public string CityName { get; set; }

        [Display(Name = "Rating")]
        public int Rating { get; set; }
        public SelectList RatingStatusList { get; set; }
        public enum RatingStatus { One = 1, Two = 2, Three = 3, Four = 4, Five = 5 }
        [Display(Name = "Override rating")]
        public bool IsManualReview { get; set; }

        [Display(Name = "Distance From City (KM) - Leave as 0 for automatic calculation")]
        public Decimal DistanceFromCity { get; set; }

        public SelectList StatusList { get; set; }
        public SelectList CountryList { get; set; }
        public SelectList StateList { get; set; }
        public SelectList CityList { get; set; }
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
        [Display(Name = "Page Title")]
        public string PageTitle { get; set; }
        [Display(Name = "Meta Description")]
        public string MetaDescription { get; set; }
        [MaxLength(50, ErrorMessage = "Max length 50 characters")]
        public string Phone { get; set; }
        [MaxLength(50, ErrorMessage = "Max length 50 characters")]
        public string Mobile { get; set; }

        public string ActiveTab { get; set; }
        public int PropertyFor { get; set; }
        public int PropertyInventoryType { get; set; }
        public SelectList TaxTitleList { get; set; }
        public int TaxTitleId { get; set; }
        public string TaxTitle { get; set; }
        public decimal Rate { get; set; }
        public List<CLayer.Property> PropertyList { get; set; }
        public decimal B2CPartialPaymentsPcte { get; set; }
        public decimal B2BPartialPaymentsPcte { get; set; }
        public bool ForB2C { get; set; }
        public bool ForB2B { get; set; }
        [Display(Name = "Hotel Id")]
        public string HotelId { get; set; }

        [Display(Name = "GST Registration No")]
        public string GSTRegistrationNo { get; set; }

        public SelectList InventoryAPITypes { get; set; }
        [Display(Name = "Inventory API")]
        public int InventoryAPITypeId { get; set; }

  
        public int InventoryAPIType { get; set; }

        public string SecurityToken { get; set; }
        public string SessionId { get; set; }
        public string SequenceNumber { get; set; }
        public string hotelcode { get; set; }
        public string StateName { get; set; }
        public string CountryName { get; set; }

        public Models.SimpleSearchModel Filter { get; set; }

        public PropertyModel()
        {
            City = "";
            CityId = -1;
            Filter = new SimpleSearchModel();
            // B2CPartialPaymentsPcte = Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.PARTIAL_PAYMENT_PERCENTAGE));
            //B2BPartialPaymentsPcte = Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.PARTIAL_PAYMENT_PERCENTAGE));
            List<SelectListItem> statusItems = new List<SelectListItem>();
            statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Active.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Active).ToString() });
            statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Draft.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Draft).ToString() });
            statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Disabled.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Disabled).ToString() });
            //statusItems.Add(new SelectListItem() { Text = CLayer.ObjectStatus.StatusType.Deleted.ToString(), Value = ((int)CLayer.ObjectStatus.StatusType.Active).ToString() });
            StatusList = new SelectList(statusItems, "Value", "Text");
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
                else
                {
                    cities = new List<CLayer.City>();
                }
                cities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
                CityList = new SelectList(cities, "CityId", "Name");
            }
            List<CLayer.TaxTitle> taxtitle = BLayer.TaxTitle.GetAll();
            TaxTitleList = new SelectList(taxtitle, "TaxTitleId", "Title");
            Pictures = new PropertyPicturesModel();
            Features = new PropertyFeaturesModel();
            Accommodations = new PropertyAccommmodationModel();
            Status = (int)CLayer.ObjectStatus.StatusType.Active;


            Array Rating = Enum.GetValues(typeof(RatingStatus));
            List<SelectListItem> RatingItems = new List<SelectListItem>(Rating.Length);


            foreach (var j in Rating)
            {
                RatingItems.Add(new SelectListItem { Text = Enum.GetName(typeof(RatingStatus), j), Value = ((int)j).ToString() });
            }
            RatingStatusList = new SelectList(RatingItems, "Value", "Text");
            CheckHourlist = new SelectList(new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" });
            CheckMinlist = new SelectList(new[] { "00", "15", "30", "45" });
            Checkclock = new SelectList(new[] { "AM", "PM" });



            List<KeyValuePair<int, string>> InventoryAPIType = new List<KeyValuePair<int, string>>();
            InventoryAPIType.Add(new KeyValuePair<int, string>(0, "SB Inventory"));
            InventoryAPIType.Add(new KeyValuePair<int, string>(1, "Maximojo"));
            InventoryAPIType.Add(new KeyValuePair<int, string>(2, "Amadeus"));
            InventoryAPIType.Add(new KeyValuePair<int, string>(3, "Itilite"));
            InventoryAPITypes = new SelectList(InventoryAPIType, "Key", "Value");

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
            cities.Add(new CLayer.City() { CityId = -1, Name = "Not available" });
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
        [Display(Name = "Accommodation Type")]
        public int AccommodationTypeId { get; set; }
        public SelectList AccommodationTypes { get; set; }
        [Required(ErrorMessage = "required")]
        [Display(Name = "Stay Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Please choose a category")]
        public int StayCategoryId { get; set; }
        public SelectList StayCategories { get; set; }
        [Display(Name = "Number of allocated accommodations")]
        public int AccommodationCount { get; set; }
        public long PropertyId { get; set; }
        //[Display(Name = "Location")]
        //public string Location { get; set; }
        [Display(Name = "Description")]
        [AllowHtml]
        public string Description { get; set; }
        [Display(Name = "Description")]
        [AllowHtml]
        public string AccDescription { get; set; }
        [Display(Name = "Max. no of people(Includes guests)")]
        public int MaxNoOfPeople { get; set; }
        [Display(Name = "Number of Children")]
        public int MaxNoOfChildren { get; set; }
        [Display(Name = "Min. no of People")]
        public int MinNoOfPeople { get; set; }
        public int BedRooms { get; set; }
        public decimal Area { get; set; }
        [Display(Name = "Status")]
        public int Status { get; set; }
        [Display(Name = "Total Accommodations")]
        public int TotalAccommodations { get; set; }
        public long UpdatedById { get; set; }
        public SelectList StatusList { get; set; }
        public enum StatusTypes { Enabled = 1, Disabled = 2 }
        public string RoomTypeDescription { get; set; }

        public AccommodationPicturesModel AccommodationPictures { get; set; }
        public AccommodationFeaturesModel AccommodationFeatures { get; set; }

        public string ActiveTab { get; set; }

        public string RoomType { get; set; }
        public string RoomTypeCode { get; set; }
        public string SourceofBusiness { get; set; }

        public string BookingCode { get; set; }

        public string RatePlanCode { get; set; }

        public int RoomStayRPH { get; set; }
        //for List

        public string AccommodationType { get; set; }
        public string Staycategory { get; set; }
        public string PropertyTitle { get; set; }

        public string SearchString { get; set; }
        public long TotalRows { get; set; }
        public int SearchValue { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }

        [Display(Name = "Room Id (Inventory API)")]
        public string RoomId { get; set; }

        public AccommodationModel()
        {
            List<CLayer.AccommodationType> acctypes = BLayer.AccommodationType.GetAll();
            AccommodationTypes = new SelectList(acctypes, "AccommodationTypeId", "Title");

            List<CLayer.StayCategory> categories = BLayer.StayCategory.GetActiveList();
            //  categories.Insert(0, new CLayer.StayCategory() { Title="All", CategoryId=0 });
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

    public class OfferAccommodationModel
    {
        public long PropertyId { get; set; }
        public long OfferId { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public enum SearchAccommodationBy { State = 1, City = 2, Titile = 3 }
        public AccommodationModel Accommodation { get; set; }
        public List<CLayer.Accommodation> Accommodations { get; set; }
        public OfferAccommodationModel()
        {
            Accommodation = new AccommodationModel();
            Accommodations = new List<CLayer.Accommodation>();
        }
    }
}