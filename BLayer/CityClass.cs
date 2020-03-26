using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class CityClass
    {
        public static List<CLayer.CityClass> GetAll()
        {
            DataLayer.CityClass cityclass = new DataLayer.CityClass();
            return cityclass.GetAll();
        }

        public static CLayer.CityClass GetDetails(int id)
        {
            DataLayer.CityClass cityclass = new DataLayer.CityClass();
            return cityclass.GetDetails(id);
        }
        public static CLayer.CityClassResult Save(CLayer.CityClass C)
        {

            DataLayer.CityClass cityclass = new DataLayer.CityClass();

            return cityclass.Save(C);
        }

        public static CLayer.CityClassResult CheckCityIfExist(string CityId, int CityClassID)
        {

            DataLayer.CityClass cityclass = new DataLayer.CityClass();

            return cityclass.CheckCityIfExist(CityId,  CityClassID);
        }
        //public static void Delete(int TaxTitleId)
        //{
        //    DataLayer.TaxTitle taxtitDe = new DataLayer.TaxTitle();
        //    taxtitDe.Delete(TaxTitleId);
        //    return;
        //}

        //public static CLayer.TaxTitle Get(int TaxTitleId)
        //{
        //    DataLayer.TaxTitle getbyid = new DataLayer.TaxTitle();
        //    return getbyid.GetById(TaxTitleId);
        //}

    }
}
