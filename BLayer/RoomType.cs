using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
   public class RoomType
    {
       public static void SetPropertyType(int roomTypeId, string propertyTypesCSV)
       {
           DataLayer.RoomType roomtypeGA = new DataLayer.RoomType();
           roomtypeGA.SetPropertyType(roomTypeId, propertyTypesCSV);
       }
       public static List<CLayer.PropertyRoomType> GetAllPropertyType(int roomTypeId)
       {
           DataLayer.RoomType roomtypeGA = new DataLayer.RoomType();
           return roomtypeGA.GetAllPropertyType(roomTypeId);
       }
        public static List<CLayer.RoomType> GetAll()
        {
            DataLayer.RoomType roomtypeGA = new DataLayer.RoomType();
            List<CLayer.RoomType> roomtypelist = new List<CLayer.RoomType>();
            roomtypelist = roomtypeGA.GetAll();
            return roomtypelist;
        }

        public static int Save(CLayer.RoomType roomtype)
        {
            DataLayer.RoomType roomtypeS = new DataLayer.RoomType();
            int result = roomtypeS.Save(roomtype);
            return result;
        }

        public static void Delete(int RoomTypeID)
        {
            DataLayer.RoomType roomtypeD = new DataLayer.RoomType();
            roomtypeD.Delete(RoomTypeID);            
            return;
        }

        public static CLayer.RoomType Get(int RoomTypeID)
        {
            DataLayer.RoomType roomtypeG = new DataLayer.RoomType();
            CLayer.RoomType roomtype = new CLayer.RoomType();
            roomtype = roomtypeG.Get(RoomTypeID);
            return roomtype;
        }
    }
}
