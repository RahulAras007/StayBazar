using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class AccommodationType
    {
        public static void SetCategoryType(int accTypeId, string categoryTypesCSV)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            accType.SetCategoryType(accTypeId, categoryTypesCSV);
        }
        public static List<CLayer.StayCategoryAccommodationType> GetAllPropertyType(int roomTypeId)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            return accType.GetAllStayCategoryType(roomTypeId);
        }
        public static List<CLayer.AccommodationType> GetAll()
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            return accType.GetAll();
        }

        public static List<CLayer.Accommodation> GetBypropertyid(long id)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            return accType.GetBypropertyid(id);
        }
        public static int Save(CLayer.AccommodationType roomtype)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            roomtype.Validate();
            return accType.Save(roomtype);
        }
        public static int AccommodationTypeSave(string title,int CatID)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            //roomtype.Validate();
            return accType.AccommodationTypeSave(title,CatID);
        }
        public static int TBOAccommodationTypeSave(string title)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            //roomtype.Validate();
            return accType.TBOAccommodationTypeSave(title);
        }
        public static void Delete(int RoomTypeID)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            accType.Delete(RoomTypeID);
            return;
        }

        public static CLayer.AccommodationType Get(int RoomTypeID)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            return accType.Get(RoomTypeID);
        }
        public static int GetAccommodationTypeTamCatID(long ID)
        {
            DataLayer.AccommodationType accType = new DataLayer.AccommodationType();
            //roomtype.Validate();
            return accType.GetAccommodationTypeTamCatID(ID);
        }
    }
}
