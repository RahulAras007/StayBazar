using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Suggest
    {
        public static List<CLayer.Suggest> GetAll()// For getting all the suggestions with the status NotViewed and Viewd
        {
            DataLayer.Suggest suggest = new DataLayer.Suggest();
            return suggest.GetAll();
        }

        public static List<CLayer.Suggest> GetAll(int status)
        {
            DataLayer.Suggest suggest = new DataLayer.Suggest();
            return suggest.GetAll(status);
        }

        public static int Save(CLayer.Suggest suggestdata)
        {
            DataLayer.Suggest suggest = new DataLayer.Suggest();
            return suggest.Save(suggestdata);
        }

        public static CLayer.Suggest Get(int InfoId)
        {
            DataLayer.Suggest suggest = new DataLayer.Suggest();
            return suggest.Get(InfoId);
        }

        //
        // TODO Use the following delete functions for deleting "propertysuggestion". Not being used now.
        //
        public static void Delete(int InfoId)
        {
            DataLayer.Suggest suggest = new DataLayer.Suggest();
            suggest.Delete(InfoId);
            return;
        }


        public static void Delete(string InfoIds)
        {
            DataLayer.Suggest suggest = new DataLayer.Suggest();
            suggest.Delete(InfoIds);
            return;
        }

        public static void SetStatus(int InfoId, int Status)
        {
            DataLayer.Suggest suggest = new DataLayer.Suggest();
            suggest.SetStatus(InfoId, Status);
            return;
        }

        public static void SetStatus(string InfoIds, int Status)
        {
            DataLayer.Suggest suggest = new DataLayer.Suggest();
            suggest.SetStatus(InfoIds, Status);
            return;
        }
    }
}
