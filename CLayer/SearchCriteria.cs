using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class SearchCriteria
    {
        //Main criteria
        public string Destination { get;set; }
        public DateTime CheckIn { get;set; }
        public DateTime CheckOut { get;set; }
        public int Adults { get;set; }
        public int Children { get;set; }
        public int StayType { get;set; }
        public int Bedrooms { get;set; }
        public string StayTypeGroup { get; set; }

        //Filter data
        public int RangeBudgetMax { get; set; }
        public int RangeBudgetMin { get; set; }
        public int StarRatingRange { get; set; }
        public int DistanceInKm { get; set; }
        public string Features { get; set; }

        public object Stay { get; set; }
        public CLayer.Role.Roles UserType { get; set; }

        public int StaringRow { get; set; }
        public int NoOfRows { get; set; }
        public CLayer.Role.Roles DefaultUserType { get; set; }
        public decimal Longitude { get; set; }
        public decimal Lattitude { get; set; }
        public SortBy SortOrder { get; set; }
        public string Location { get; set; }
        public string Country { get; set; }

        public string HotelCode { get; set; }

        public long LoggedInUser { get; set; }
        public string SessionID { get; set; }
        public int SequenceNumber { get; set; }
        public string SecurityToken { get; set; }
        public string Moreindicator { get; set; }
        public string HotelID { get; set; }
        public string RateCardDetailId { get; set; }
        public string cityid { get; set; }
        public int CategoryID { get; set; }
        public string APICheckIn { get; set; }
        public int NoOfNights { get; set; }
        public string TraceId { get; set; }
        public int ResultIndex { get; set; }
        public enum SortBy
        {
            Distance = 1,
            PriceAsc = 2,
            PriceDesc = 3
        }

        public SearchCriteria()
        {
            LoggedInUser = 0;
            Adults = 0;
            Children = 0;
            StayType = 0;
            Bedrooms = 1;
            StayTypeGroup = "";
            StaringRow = 0;
            NoOfRows = 25;
            RangeBudgetMax = 0;
            RangeBudgetMin = 0;
            StarRatingRange = 0;
            Features = "";
            DistanceInKm = 0;
            UserType = Role.Roles.Customer;
            DefaultUserType = Role.Roles.Customer;
            SortOrder = SortBy.PriceAsc;
            Lattitude = 0;
            Longitude = 0;
            HotelCode = "";
            HotelID = "";
            RateCardDetailId = "";
            cityid = "";
            CategoryID = 0;
            NoOfNights = 1;
        }
    }
}
