using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class AccommodationFeature
    {
        public static List<CLayer.AccommodationFeature> GetAll()
        {
            DataLayer.AccommodationFeature af = new DataLayer.AccommodationFeature();
            return af.GetAll();
        }

        public static int Save(CLayer.AccommodationFeature data)
        {
            DataLayer.AccommodationFeature af = new DataLayer.AccommodationFeature();
            data.Validate();
            return af.Save(data);
        }

        public static int SaveAccommodationFeature(CLayer.AccommodationFeature data)
        {
            DataLayer.AccommodationFeature AccomodationFeature = new DataLayer.AccommodationFeature();
            return AccomodationFeature.SaveAccommodationFeature(data);
        }

        public static void Delete(int AccomodationFeatureId)
        {
            DataLayer.AccommodationFeature AccomodationFeature = new DataLayer.AccommodationFeature();
            AccomodationFeature.Delete(AccomodationFeatureId);
        }

        public static void DeleteFeatureOnAccommodation(long accommodationId)
        {
            DataLayer.AccommodationFeature AccomodationFeature = new DataLayer.AccommodationFeature();
            AccomodationFeature.DeleteFeatureOnAccommodation(accommodationId);
        }

        public static CLayer.AccommodationFeature Get(int accomodationFeatureId)
        {
            DataLayer.AccommodationFeature af = new DataLayer.AccommodationFeature();
            return af.Get(accomodationFeatureId);
        }

        public static List<CLayer.AccommodationFeature> GetAllWithSelectedForAccommodation(long accommodationId)
        {
            DataLayer.AccommodationFeature AccomodationFeature = new DataLayer.AccommodationFeature();
            return AccomodationFeature.GetAllWithSelectedForAccommodation(accommodationId);
        }
    }
}
