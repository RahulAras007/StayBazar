using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Utility
    {
        public static List<object> GetAutoList(string term)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetAutoList(term);
        }
        public static List<object> GetAutoListGDS(string term)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetAutoListGDS(term);
        }

        public static List<CLayer.AutoCompletedList> GetAutoListGDSAll(string term)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetAutoListGDSAll(term);
        }
        public static List<object> GetAutoListforaccommn(string term)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetAutoListforaccommn(term);
        }
        public static List<object> GetAutoListforaccommnForOffline(string term)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetAutoListforaccommnForOffline(term);
        }
        
        public static List<object> GetLocations(string term)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetLocations(term);
        }
        public static List<string> GetLocationFilter(string term)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetLocationFilter(term);
        }
        public static List<CLayer.SearchResult> GetLocationmultiFilter(string term,int type)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetLocationmultiFilter(term,type);
        }
        public static List<string> GetLocationFilterGDS(string term)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetLocationFilterGDS(term);
        }
        public static  List<CLayer.SearchResult> GetLocationmultiFilterGDS(string term, int type)
        {
            DataLayer.Utils u = new DataLayer.Utils();
            return u.GetLocationmultiFilterGDS(term, type);
        }
    }
}
