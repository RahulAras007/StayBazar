using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class City
    {
        public static List<CLayer.City> GetOnState(int stateId)
        {
            DataLayer.City city = new DataLayer.City();
            return city.GetOnState(stateId);
        }

        public static List<CLayer.City> GetCityByname(string name)
        {
            DataLayer.City city = new DataLayer.City();
            return city.GetCityByname(name);
        }
        public static CLayer.City Get(int? cityId)
        {
            DataLayer.City city = new DataLayer.City();
            return city.Get(cityId);
        }
        public static CLayer.City GetCityID(string CityName)
        {
            DataLayer.City city = new DataLayer.City();
            return city.GetCityID(CityName);
        }

        public static int Save(CLayer.City data)
        {
            DataLayer.City city = new DataLayer.City();
            data.Validate();
            return city.Save(data);
        }

        public static string GetLocation(string search)
        {
            DataLayer.City city = new DataLayer.City();
            return city.GetLocation(search);
        }

        public static List<CLayer.City> GetAllForListing()
        {
            DataLayer.City city = new DataLayer.City();
            return city.GetAllForListing();
        }
        public static string GetCityAll()
        {
            DataLayer.City city = new DataLayer.City();
            return city.GetCityAll();
        }
        public static int UpdateGDSCity(int StateID, string City = "Others")
        {
            DataLayer.City city = new DataLayer.City();
            return city.UpdateGDSCity(StateID, City);
        }
        public static string GetTamarindCityID(string CityName)
        {
            DataLayer.City city = new DataLayer.City();
            return city.GetTamarindCityID(CityName);
        }
    }
}
