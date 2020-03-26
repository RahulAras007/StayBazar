using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Country
    {

        public static List<CLayer.Country> GetAll()
        {
            DataLayer.Country country = new DataLayer.Country();
            return country.GetAll();
        }
        public static List<CLayer.Country> GetList()
        {
            DataLayer.Country country = new DataLayer.Country();
            return country.GetList();
        }

        public static List<CLayer.Country> GetAllForProperty()
        {
            DataLayer.Country country = new DataLayer.Country();
            return country.GetAllForProperty();
        }
        public static int Save(CLayer.Country countrydata)
        {
            DataLayer.Country country = new DataLayer.Country();
            countrydata.Validate();
            return country.Save(countrydata);
        }

        //
        // TODO Delete country
        //

        public static CLayer.Country Get(int countryId)
        {
            DataLayer.Country country = new DataLayer.Country();
            return country.Get(countryId);
        }
        public static string  UpdateGDSCountry(string CountryName)
        {
            DataLayer.Country country = new DataLayer.Country();
            return country.UpdateGDSCountry(CountryName);
        }
        public static List<CLayer.GDSCountry> GetGDSCountry(string CountryName)
        {
            DataLayer.Country country = new DataLayer.Country();
            return country.GetGDSCountry(CountryName);
        }
        public static int  GDSSave(CLayer.Country pcountry)
        {
            DataLayer.Country country = new DataLayer.Country();
            return country.GDSSave(pcountry);
        }
        public static int GDSKeywordSave(CLayer.Country pcountry)
        {
            DataLayer.Country country = new DataLayer.Country();
            return country.GDSKeywordSave(pcountry);
        }
    }
}
