using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Accommodation
    {
        public static CLayer.Accommodation Get(long AccommodationId)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            return acc.Get(AccommodationId);
        }

        public static List<CLayer.Accommodation> GetAllForOwnerProperty(long propertyId, int rateType)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            return acc.GetAllForOwnerProperty(propertyId, rateType);
        }

        public static List<CLayer.Accommodation> GetAllAccByPropertyid(long propertyId)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            return acc.GetAllAccByPropertyid(propertyId);
        }
        public static long Save(CLayer.Accommodation data)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            data.Validate();
            return acc.Save(data);
        }
        public static long Save_API(CLayer.Accommodation data)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            data.Validate();
            return acc.Save_API(data);
        }
        public static long Save_Amadeus(CLayer.Accommodation data)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            data.Validate();
            return acc.Save_Amadeus(data);
        }
        public static void Accommodation_Amadeus_Delete(long pPropertyId)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            // data.Validate();
            acc.Accommodation_Amadeus_Delete(pPropertyId);
        }
        public static long GetPropertyId(long accommodationId)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            return acc.GetPropertyId(accommodationId);
        }

        //*Added by rahul for getting Inventory API Type
        public static long GetInventoryAPIType(long accommodationId)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            return acc.GetInventoryAPIType(accommodationId);
        }

        public static string GetPropertyTitle(long accommodationId)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            return acc.GetPropertyTitle(accommodationId);
        }
        public static string GetAccommodationTitle(long accommodationId)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            return acc.GetAccommodationTitle(accommodationId);
        }

        public static long getAccommodationCountByStatus(int status, long BookingId)
        {
            DataLayer.Accommodation acc = new DataLayer.Accommodation();
            return acc.getAccommodationCountByStatus(status, BookingId);
        }
        public static void Delete(long AccommodationId)
        {
            DataLayer.Accommodation Accommodation = new DataLayer.Accommodation();
            Accommodation.Delete(AccommodationId);
        }
        public static int SetRoomId(long AccommodationId, string RoomId)
        {
            DataLayer.Accommodation property = new DataLayer.Accommodation();
            return property.SetRoomId(AccommodationId, RoomId);
        }
        public static string GetRoomId(long AccommodationId)
        {
            DataLayer.Accommodation property = new DataLayer.Accommodation();
            return property.GetRoomId(AccommodationId);
        }
        public static void UpdateGDSAccommodationAvailability(List<string> codes, long PropertyId)
        {
            DataLayer.Accommodation property = new DataLayer.Accommodation();
             property.UpdateGDSAccommodationAvailability(codes, PropertyId);
        }
    }
}
