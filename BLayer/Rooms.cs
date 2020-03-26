using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Rooms
    {
        public static List<CLayer.Rooms> GetAll(long propertyid)
        {
            DataLayer.Rooms rooms = new DataLayer.Rooms();
            return rooms.GetAll(propertyid);
        }

        public static CLayer.Rooms Get(long roomid)
        {
            DataLayer.Rooms rooms = new DataLayer.Rooms();
            return rooms.Get(roomid);
        }

        public static void Delete(long roomid)
        {
            DataLayer.Rooms rooms = new DataLayer.Rooms();
            rooms.Delete(roomid);
        }

        public static long Save(CLayer.Rooms room)
        {
            DataLayer.Rooms rooms = new DataLayer.Rooms();
            return rooms.Save(room);
        }
    }
}
