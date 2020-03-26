using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class StayCategory
    {
        public static List<CLayer.StayCategory> GetAll()
        {
            DataLayer.StayCategory staycategoryGA = new DataLayer.StayCategory();
            return staycategoryGA.GetAll();
        }

        public static List<CLayer.StayCategory> GetActiveList()
        {
            DataLayer.StayCategory staycategoryGA = new DataLayer.StayCategory();
            return staycategoryGA.GetActiveList();
        }
        public static List<CLayer.StayCategory> GetBypropertyid(long id)
        {
            DataLayer.StayCategory accType = new DataLayer.StayCategory();
            return accType.GetBypropertyid(id);
        }
        public static int Save(CLayer.StayCategory staycategory)
        {
            DataLayer.StayCategory staycategoryS = new DataLayer.StayCategory();
            staycategory.Validate();
            int result = staycategoryS.Save(staycategory);
            return result;
        }

        public static void Delete(int CategoryID)
        {
            DataLayer.StayCategory staycategoryD = new DataLayer.StayCategory();
            staycategoryD.Delete(CategoryID);
            return;
        }
        public static DataTable GetCategoryHotel()
        {
            DataLayer.StayCategory staycategoryD = new DataLayer.StayCategory();
            return staycategoryD.GetCategoryHotel();
            
        }
        

        public static CLayer.StayCategory Get(int CategoryID)
        {
            DataLayer.StayCategory staycategoryG = new DataLayer.StayCategory();
            CLayer.StayCategory staycategory = new CLayer.StayCategory();
            staycategory = staycategoryG.Get(CategoryID);
            return staycategory;
        }
    }
}
