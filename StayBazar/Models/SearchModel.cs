using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Models
{
    public class SearchModel
    {
        public SimpleSearchModel SearchCoordinates { get; set; }
        public SearchResultModel Results { get; set; }

        public SearchModel()
        {
            Results = new SearchResultModel();
        }
    }

    public class SimpleSearchModel
    {
        public string Destination { get; set; }
        //[Required]
        //[DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        //[Required]
        //[DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }
        public string Adults { get; set; }
        public string Children { get; set; }
        public string StayType { get; set; }
        public string Bedrooms { get; set; }

        public int rangeBudgetMax { get; set; }
        public int rangeBudgetMin { get; set; }
        public int starRatingRange { get; set; }
        public int distanceInKm { get; set; }
        public string features { get; set; }
        public int beds { get; set; }
        public int CurrentPage { get; set; }
        public int OrderBy { get; set; }
        public string Location { get; set; }
        public long PropertyId { get; set; }
        public string HiddenDestination { get; set; }
        public DateTime HiddenCheckIn { get; set; }
        public DateTime HiddenCheckOut { get; set; }
        public string HidAdults { get; set; }
        public string HidChildren { get; set; }
        public string HidStayType { get; set; }
        public string HidBedrooms { get; set; }
        public string Country { get; set; }
        public string SessionID { get; set; }
        public int SequenceNumber { get; set; }
        public string SecurityToken { get; set; }
        public string Moreindicator { get; set; }
        public string title { get; set; }
        public string RateCardDetailedId { get; set; }
        public decimal TaxPercentage { get; set; }
        public int GSTSlab { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int AccommodationTpeID { get; set; }
        public int StayCategoryID { get; set; }
        public decimal AmountWithTax { get; set; }
        public decimal Amount { get; set; }
        public decimal price { get; set; }
        public int ResultIndex { get; set; }
        public string TraceID { get; set; }
        public int ErrorCode { get; set; }
        public int InventoryAPIType { get; set; }
        public string Description { get; set; }
        public SimpleSearchModel()
        {

            Destination = "";
            Adults = "1";
            Children = "0";
            Bedrooms = "0";
            StayType = "0";
            CheckIn = DateTime.MinValue;
            CheckOut = DateTime.MinValue;          
        }
    }

    public class SearchParamModel
    {
        public string Destination { get; set; }
        //[Required]
        //[DataType(DataType.Date)]
        public string CheckIn { get; set; }
        //[Required]
        //[DataType(DataType.Date)]
        public string CheckOut { get; set; }
        public string Adults { get; set; }
        public string Children { get; set; }
        public string StayType { get; set; }
        public string Bedrooms { get; set; }

        public int rangeBudgetMax { get; set; }
        public int rangeBudgetMin { get; set; }
        public int starRatingRange { get; set; }
        public int distanceInKm { get; set; }
        public string features { get; set; }
        public int beds { get; set; }
        public int CurrentPage { get; set; }
        public int OrderBy { get; set; }
        public string Location { get; set; }
        public long PropertyId { get; set; }
        public string HiddenDestination { get; set; }
        public DateTime HiddenCheckIn { get; set; }
        public DateTime HiddenCheckOut { get; set; }
        public string HidAdults { get; set; }
        public string HidChildren { get; set; }
        public string HidStayType { get; set; }
        public string HidBedrooms { get; set; }
        public string paramResult { get; set; }

        public string SearchLocation { get; set; }
        public string SessionID { get; set; }
        public int SequenceNumber { get; set; }
        public string SecurityToken { get; set; }
        public string Moreindicator { get; set; }
        public string title { get; set; }
        public string HotelID { get; set; }
        public string APIType { get; set; }

        //Added by rahul
        public string RateCardDetailedId { get; set; }
        public decimal TaxPercentage { get; set; }
        public int GSTSlab { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public int AccommodationTpeID { get; set; }
        public int StayCategoryID { get; set; }
        public decimal AmountWithTax { get; set; }
        public decimal Amount { get; set; }
        public decimal price { get; set; }
        public int ResultIndex { get; set; }
        public string TraceID { get; set; }
        public int ErrorCode { get; set; }
        public int InventoryAPIType { get; set; }
        public string Description { get; set; }
        public SearchParamModel()
        {
            Destination = "";
            Adults = "1";
            Children = "0";
            Bedrooms = "1";
            StayType = "0";
            CheckIn = DateTime.MinValue.ToShortDateString();
            CheckOut = DateTime.MinValue.ToShortDateString();
        }

        public SimpleSearchModel GetSimpleSearch()
        {
            SimpleSearchModel sm = new SimpleSearchModel();
            sm.Destination = Destination;
            DateTime t = DateTime.MinValue;
            DateTime.TryParse(CheckIn, out t);
            sm.CheckIn = t;
            t = DateTime.MinValue;
            DateTime.TryParse(CheckOut, out t);
            sm.CheckOut = t;
            sm.Adults = Adults;
            sm.Children = Children;
            sm.StayType = StayType;
            sm.Bedrooms = Bedrooms;

            sm.rangeBudgetMax = rangeBudgetMax;
            sm.rangeBudgetMin = rangeBudgetMin;
            sm.starRatingRange = starRatingRange;
            sm.distanceInKm = distanceInKm;
            sm.features = features;
            sm.beds = beds;
            sm.CurrentPage = CurrentPage;
            sm.OrderBy = OrderBy;
            sm.Location = Location;
            sm.PropertyId = PropertyId;
            sm.HiddenDestination = HiddenDestination;
            sm.HiddenCheckIn = HiddenCheckIn;
            sm.HiddenCheckOut = HiddenCheckOut;
            sm.HidAdults = HidAdults;
            sm.HidChildren = HidChildren;
            sm.HidStayType = HidStayType;
            sm.HidBedrooms = HidBedrooms;
            sm.SessionID = SessionID;
            sm.SequenceNumber = SequenceNumber;
            sm.SecurityToken = SecurityToken;
            sm.Moreindicator = Moreindicator;
            sm.RateCardDetailedId = RateCardDetailedId;
            sm.TaxPercentage = TaxPercentage;
            sm.StateId = StateId;
            sm.CityId = CityId;
            sm.AccommodationTpeID = AccommodationTpeID;
            sm.StayCategoryID = StayCategoryID;
            sm.Amount = Amount;
            sm.AmountWithTax = AmountWithTax;
            sm.ResultIndex = ResultIndex;
            sm.TraceID = TraceID;
            //sm.ErrorCode = ErrorCode;
            sm.Description = Description;
            sm.InventoryAPIType = InventoryAPIType;
            return sm;
        }
    }

    public class SearchResultModel
    {
        public int MaxCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public string Destination { get; set; }
        public List<CLayer.SearchResult> Results { get; set; }
        public bool IsSearched { get; set; }
        public string Moreindicator { get; set; }
        public string SessionID { get; set; }
        public int SequenceNumber { get; set; }
        public string SecurityToken { get; set; }

        public SearchResultModel()
        {
            MaxCount = 0;
            CurrentPageIndex = 1;
            Results = new List<CLayer.SearchResult>();
            IsSearched = false;
        }
        public SimpleSearchModel SearchCoordinates { get; set; }
    }



}