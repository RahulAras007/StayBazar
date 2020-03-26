using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class TBOService
    {
        public string ClientId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EndUserIp { get; set; }
    }
    public class RoomGuests
    {
        public int NoOfAdults { get; set; }
        public int NoOfChild { get; set; }
        public IList ChildAge { get; set; }
    }
    public class TBOHotelSearch
    {
        //public string ClientId;
        //public string UserName;
        //public string Password;
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string CheckInDate { get; set; }
        public int NoOfNights { get; set; }
        public string CountryCode { get; set; }
        public int CityId { get; set; }
        public string PreferredCurrency { get; set; }
        public string GuestNationality { get; set; }
        public string NoOfRooms { get; set; }
        public List<RoomGuests> RoomGuests = new List<RoomGuests>();
        //public int NoOfAdults;
        //public int NoOfChild;
        //public int[] ChildAge = new int[10];
        public int MaxRating { get; set; }
        public int MinRating { get; set; }
        public string PreferredHotel { get; set; }
        public decimal ReviewScore { get; set; }
        public bool IsNearBySearchAllowed { get; set; }
        public int resultResultCount { get; set; }

    }
    public class TBOHotelRoom
    {
        public string EndUserIp { get; set; }
        public string TokenId { get; set; }
        public string TraceId { get; set; }
        public int ResultIndex { get; set; }
        public string HotelCode { get; set; }
    }
}
