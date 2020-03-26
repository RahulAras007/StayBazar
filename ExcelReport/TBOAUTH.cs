using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelReport
{
    public class RoomGuests {
        public int NoOfAdults { get; set; }
        public int NoOfChild { get; set; }
        public IList ChildAge { get; set; }
    }
    class Country
    {
        public string ClientId;
        public string EndUserIp;
        public string TokenId;
    }
    class destination
    {
        public string ClientId;
        public string EndUserIp;
        public string TokenId;
        public string CountryCode;
    }
    class HotelSearch {
        //public string ClientId;
        //public string UserName;
        //public string Password;
        public string EndUserIp;
        public string TokenId;
        public string CheckInDate;
        public int NoOfNights;
        public string CountryCode;
        public int CityId;
        public string PreferredCurrency;
        public string GuestNationality;
        public string NoOfRooms;
        public List<RoomGuests> RoomGuests = new List<RoomGuests>();
        //public int NoOfAdults;
        //public int NoOfChild;
        public int[] ChildAge = new int[10];
        public int MaxRating;
        public int MinRating;
        public string PreferredHotel;
        public decimal ReviewScore;
        public bool IsNearBySearchAllowed;
        public int resultResultCount;
     
    }
    class TBOAUTH
    {
        public string ClientId;
        public string UserName;
        public string Password;
        public string EndUserIp;
    }
}
