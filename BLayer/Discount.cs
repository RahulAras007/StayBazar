using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Discount
    {
        public static CLayer.Discount GetDiscount(long b2bId, long propertyId)
        {
            DataLayer.Discount dis = new DataLayer.Discount();
            return dis.GetDiscount(b2bId, propertyId);
        }
        public static void Delete(long b2bId, long propertyId)
        {
            DataLayer.Discount dis = new DataLayer.Discount();
            dis.Delete(b2bId, propertyId);
        }
        public static CLayer.Discount Get(long b2bId,long propertyId)
        {
            DataLayer.Discount dis = new DataLayer.Discount();
            return dis.Get(b2bId, propertyId);
        }
        public static List<CLayer.Discount> GetAll(long propertyId)
        {
            DataLayer.Discount dis = new DataLayer.Discount();
            return dis.GetAll(propertyId);
        }
        public static void Save(CLayer.Discount data)
        {
            DataLayer.Discount dis = new DataLayer.Discount();
            dis.Save(data);
        }
        public static CLayer.Discount GetStdDiscount(long propertyId)
        {
            DataLayer.Discount dis = new DataLayer.Discount();
            return dis.GetStdDiscount(propertyId);
        }
        public static void SaveStdDiscount(CLayer.Discount data)
        {
            DataLayer.Discount dis = new DataLayer.Discount();
            dis.SaveStdDiscount(data);
        }
    }
}
