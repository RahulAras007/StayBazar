using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;

namespace BLayer
{
    public class Condition
    {
        public static List<CLayer.Condition> GetAllForAccommodations(string accommodationIds)
        {
            DataLayer.Condition task = new DataLayer.Condition();
            return task.GetAllForAccommodations(accommodationIds);
        }
        public static long Save(CLayer.Condition data)
        {
            DataLayer.Condition task = new DataLayer.Condition();
            return task.Save(data);
        }

        public static List<CLayer.Condition> GetAllbyaccomodation(int Start, int Limit, long? accomodationId)
        {
            DataLayer.Condition task = new DataLayer.Condition();
            return task.GetAllAccomodationId(Start, Limit, accomodationId);
        }

        public static long Delete(long ConditionId)
        {
            DataLayer.Condition task = new DataLayer.Condition();
            long accid = task.Delete(ConditionId);
            return (accid);
        }
        public static CLayer.Condition GetOnebyConditionId(long ConditionId)
        {
            DataLayer.Condition getinventory = new DataLayer.Condition();
            return getinventory.Get(ConditionId);
        }
    }
}
