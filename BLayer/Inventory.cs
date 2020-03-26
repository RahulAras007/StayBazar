using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Inventory
    {
       
        public static long save(CLayer.Inventory invenData)
        {
            DataLayer.Inventory task = new DataLayer.Inventory();          
            return task.Update(invenData);
        }
        public static long  Delete(long InventoryId, long AccommodationId)
        {
            DataLayer.Inventory task = new DataLayer.Inventory();
            long accid= task.Delete(InventoryId, AccommodationId);
            return(accid);
        }
        public static CLayer.Inventory GetOnebyInventoryId(long InventoryId)
        {
            DataLayer.Inventory getinventory = new DataLayer.Inventory();
            return getinventory.GetOnAccommodationId(InventoryId);
        }

        public static List<CLayer.Inventory> AccommodationListByProperty( long? accomodationId)
        {
            DataLayer.Inventory getinventory = new DataLayer.Inventory();
            return getinventory.AccommodationByProperty(accomodationId);
        }
       
        public static List<CLayer.Inventory> GetAllbyaccomodation(int Start, int Limit, long? accomodationId)
        {
            DataLayer.Inventory task = new DataLayer.Inventory();
            return task.GetAllAccomodationId(Start, Limit, accomodationId);
        }
        public static long APIsave(CLayer.Inventory invenData)
        {
            DataLayer.Inventory task = new DataLayer.Inventory();
            return task.APIUpdate(invenData);
        }
    }
}
