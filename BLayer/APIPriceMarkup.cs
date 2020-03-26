using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class APIPriceMarkup
    {
       
        public static List<CLayer.APIPriceMarkup> GetAPIPriceMarkup(string name,int start,int limit)
        {
            DataLayer.APIPriceMarkup  API = new DataLayer.APIPriceMarkup();
            return API.GetAPIPriceMarkup(name, start, limit);
        }
        //Added by rahul for Listing New API Markup price for new Screen on 30-01-2020
        public static List<CLayer.APIPriceMarkup> GetNewAPIPriceMarkUp()
        {
            DataLayer.APIPriceMarkup API = new DataLayer.APIPriceMarkup();
            return API.GetNewAPIPriceMarkUp();
        }
        //------END----
        public static CLayer.APIPriceMarkup Get(long id)
        {
            DataLayer.APIPriceMarkup user = new DataLayer.APIPriceMarkup();
            return user.Get(id);
        }
        //Added by rahul for Listing New API Markup price on edit for new Screen on 30-01-2020
        public static CLayer.APIPriceMarkup GetNewAPIDetails(long id)
        {
            DataLayer.APIPriceMarkup user = new DataLayer.APIPriceMarkup();
            return user.GetNewAPIDetails(id);
        }

        //---END-----
        public static int Save(CLayer.APIPriceMarkup t)
        {
            DataLayer.APIPriceMarkup API = new DataLayer.APIPriceMarkup();
            //     t.Validate();
            return API.Save(t);
        }
        //Added by rahul for Saving New API Markup price in edit for new Screen on 30-01-2020
        public static int SaveNewAPIMarkup(CLayer.APIPriceMarkup t)
        {
            DataLayer.APIPriceMarkup API = new DataLayer.APIPriceMarkup();
            return API.SaveNewAPIMarkup(t);
        }
        //---END--------
        public static void Delete(long Id)
        {
            DataLayer.APIPriceMarkup user = new DataLayer.APIPriceMarkup();
            user.Delete(Id);
        }
        public static List<CLayer.APIOfflineCustomer> GetAlCustomerList()
        {
            DataLayer.APIPriceMarkup user = new DataLayer.APIPriceMarkup();
            return user.GetAlCustomerList();
        }
        public static decimal GSTRate(string HSNCode, int price)
        {
            DataLayer.APIPriceMarkup API = new DataLayer.APIPriceMarkup();
            //     t.Validate();
            return API.GSTRate(HSNCode, price);
        }
    }
}
