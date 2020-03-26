using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Areas.Admin.Models
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



        public SearchParamModel()
        {

            Destination = "";
            Adults = "1";
            Children = "0";
            Bedrooms = "0";
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