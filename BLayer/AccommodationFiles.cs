using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class AccommodationFiles
    {
        public static List<CLayer.AccommodationFiles> GetOnAccommodation(long accommodationId)
        {
            DataLayer.AccommodationFiles ac = new DataLayer.AccommodationFiles();
            return ac.GetOnAccommodation(accommodationId);
        }

        public static CLayer.AccommodationFiles GetSingleOnAccommodation(long accommodationId)
        {
            DataLayer.AccommodationFiles ac = new DataLayer.AccommodationFiles();
            return ac.GetSingleOnAccommodation(accommodationId);
        }

        public static int Save(CLayer.AccommodationFiles file)
        {
            DataLayer.AccommodationFiles ac = new DataLayer.AccommodationFiles();
            file.Validate();
            return ac.Save(file);
        }

        public static void Delete(long fileId)
        {
            DataLayer.AccommodationFiles ac = new DataLayer.AccommodationFiles();
            ac.Delete(fileId);
        }

        public static void DeleteOnAccommodation(long accommodationId)
        {
            DataLayer.AccommodationFiles ac = new DataLayer.AccommodationFiles();
            ac.DeleteOnAccommodation(accommodationId);
        }

        public static CLayer.AccommodationFiles Get(long fileId)
        {
            DataLayer.AccommodationFiles ac = new DataLayer.AccommodationFiles();
            return ac.Get(fileId);
        }
    }
}
