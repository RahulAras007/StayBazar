using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Currency
    {
        public static  void SetRate(int currencyId, double rate)
        {
            DataLayer.Currency currency = new DataLayer.Currency();
            currency.SetRate(currencyId, rate);
        }
	    public static List<CLayer.Currency> GetForListing()
        {
            DataLayer.Currency currency = new DataLayer.Currency();
            return currency.GetForListing();
        }
        public static List<CLayer.Currency> GetAll()
        {
            DataLayer.Currency currency = new DataLayer.Currency();
            return currency.GetAll();
        }

        public static int Save(CLayer.Currency currencydata)
        {
            DataLayer.Currency currency = new DataLayer.Currency();
            //currencydata.Validate();
            return currency.Save(currencydata);
        }

        //public static void Delete(int PropertyTypeId)
        //{
        //    DataLayer.PropertyType properttype = new DataLayer.PropertyType();
        //    properttype.Delete(PropertyTypeId);
        //}

        public static CLayer.Currency Get(int CurrencyId)
        {
            DataLayer.Currency currency = new DataLayer.Currency();
            return currency.Get(CurrencyId);
        }
        public static CLayer.Currency Get(string code)
        {
            DataLayer.Currency currency = new DataLayer.Currency();
            return currency.Get(code);
        }
    }
}
