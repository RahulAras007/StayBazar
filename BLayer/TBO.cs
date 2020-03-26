using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class TBO
    {
        //public static void UpdateCity(string Name)
        //{
        //    DataLayer.TBO bok = new DataLayer.Tamarind();
        //    bok.UpdateCity(Name);
        //}
        //public static void UpdateCurrency(string Name)
        //{
        //    DataLayer.TBO bok = new DataLayer.Tamarind();
        //    bok.UpdateCurrency(Name);
        //}
        public static void UpdateCountry(string code,string Name)
        {
            DataLayer.TBO bok = new DataLayer.TBO();
            bok.UpdateCountry(code,Name);
        }
        public static int UpdateDestinationCityId(int CityID, string Destination,string state,string statecode,
            string country,string countrycode)
        {
            DataLayer.TBO hotel = new DataLayer.TBO();
            return hotel.UpdateDestinationCityId(CityID, Destination, state, statecode, country, countrycode);
        }
        public static string TokenID()
        {
            DataLayer.TBO Token = new DataLayer.TBO();
            return Token.TokenID();
        }
        public static string CountryCode(string country)
        {
            DataLayer.TBO Country = new DataLayer.TBO();
            return Country.CountryCode(country);
        }
        public static int GetTBOCityId(string city)
        {
            DataLayer.TBO cityid = new DataLayer.TBO();
            return cityid.GetTBOCityId(city);
        }
        public static string GetResponse(string requestData, string url)
        {
            DataLayer.TBO response = new DataLayer.TBO();
            return response.GetResponse(requestData, url);
        }
        public static decimal  APIPrice(long userid)
        {
            DataLayer.TBO API = new DataLayer.TBO();
            return API.APIPrice(userid);
        }
        public static decimal  APIPriceAll()
        {
            DataLayer.TBO API = new DataLayer.TBO();
            return API.APIPriceAll();
        }
        public static int UpdateHotelList(string HotelID, string HotelName)
        {
            DataLayer.TBO hotel = new DataLayer.TBO();
            return hotel.UpdateHotelList(HotelID, HotelName);
        }
        public static string GetCityName(int cityid)
        {
            DataLayer.TBO cityname = new DataLayer.TBO();
            return cityname.GetCityName(cityid);
        }
    }
}
