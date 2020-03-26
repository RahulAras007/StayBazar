using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class LandmarkTitles
    {
        public static CLayer.LandmarkTitles Get(int landmarkTitleId)
        {
            DataLayer.LandmarkTitles titles = new DataLayer.LandmarkTitles();
            return titles.Get(landmarkTitleId);
        }

        public static List<CLayer.LandmarkTitles> GetAll()
        {
            DataLayer.LandmarkTitles titles = new DataLayer.LandmarkTitles();
            return titles.GetAll();
        }

        public static void Delete(int landmarkTitleId)
        {
            DataLayer.LandmarkTitles titles = new DataLayer.LandmarkTitles();
            titles.Delete(landmarkTitleId);
            return;
        }

        public static int Save(CLayer.LandmarkTitles data)
        {
            DataLayer.LandmarkTitles titles = new DataLayer.LandmarkTitles();
            data.Validate();
            return titles.Save(data);
        }
    }
}