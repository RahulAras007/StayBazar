using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Tamarind
    {
        public static void UpdateCity(string Name,string cityid)
        {
            DataLayer.Tamarind  bok = new DataLayer.Tamarind();
            bok.UpdateCity(Name, cityid);
        }
        public static void UpdateCurrency(string Name)
        {
            DataLayer.Tamarind bok = new DataLayer.Tamarind();
            bok.UpdateCurrency(Name);
        }
        public static void UpdateCountry(string Name)
        {
            DataLayer.Tamarind bok = new DataLayer.Tamarind();
            bok.UpdateCountry(Name);
        }
        public static void UpdateHotelList(int HotelID, string HotelName)
        {
            DataLayer.Tamarind hotel = new DataLayer.Tamarind();
            hotel.UpdateHotelList(HotelID, HotelName);
        }
        public static void UpdateDestinationCityId(int HotelID, string HotelName)
        {
            DataLayer.Tamarind hotel = new DataLayer.Tamarind();
            hotel.UpdateHotelList(HotelID, HotelName);
        }
        public static decimal  APIPrice(long userid)
        {
            DataLayer.Tamarind API = new DataLayer.Tamarind();
            return API.APIPrice(userid);
        }
        public static decimal  APIPriceAll()
        {
            DataLayer.Tamarind API = new DataLayer.Tamarind();
            return API.APIPriceAll();
        }
    }
}
