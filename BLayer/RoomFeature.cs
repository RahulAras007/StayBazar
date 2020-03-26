using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
   public class RoomFeature
    {
        public static List<CLayer.RoomFeature> GetAll()
        {
            DataLayer.RoomFeature roomfeature = new DataLayer.RoomFeature();
            return roomfeature.GetAll();
        }

        public static int Save(CLayer.RoomFeature roomfeaturedata)
        {
            DataLayer.RoomFeature roomfeature = new DataLayer.RoomFeature();
            return roomfeature.Save(roomfeaturedata);
        }

        public static void Delete(int RoomFeatureId)
        {
            DataLayer.RoomFeature roomfeature = new DataLayer.RoomFeature();
            roomfeature.Delete(RoomFeatureId);
        }

        public static CLayer.RoomFeature Get(int RoomFeatureId)
        {
            DataLayer.RoomFeature roomfeature = new DataLayer.RoomFeature();
            return roomfeature.Get(RoomFeatureId);
        }
    }
}
