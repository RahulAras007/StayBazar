using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Landmarks
    {
        public static CLayer.Landmarks Get(long landmarkId)
        {
            DataLayer.Landmarks landmark = new DataLayer.Landmarks();
            return landmark.Get(landmarkId);
        }

        public static List<CLayer.Landmarks> GetOnProperty(long propertyId)
        {
            DataLayer.Landmarks landmark = new DataLayer.Landmarks();
            return landmark.GetOnProperty(propertyId);
        }

        public static void Delete(long landmarkId)
        {
            DataLayer.Landmarks landmark = new DataLayer.Landmarks();
            landmark.Delete(landmarkId);
            return;
        }

        public static void DeleteOnProperty(long propertyId)
        {
            DataLayer.Landmarks landmark = new DataLayer.Landmarks();
            landmark.DeleteOnProperty(propertyId);
            return;
        }

        public static long Save(CLayer.Landmarks data)
        {
            DataLayer.Landmarks landmark = new DataLayer.Landmarks();
            data.Validate();
            return landmark.Save(data);
        }
    }
}
