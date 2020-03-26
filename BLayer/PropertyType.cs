using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class PropertyType
    {
        public static List<CLayer.PropertyType> GetAll()
        {
            DataLayer.PropertyType properttype = new DataLayer.PropertyType();
            return properttype.GetAll();
        }

        public static List<CLayer.PropertyType> GetActiveList()
        {
            DataLayer.PropertyType properttype = new DataLayer.PropertyType();
            return properttype.GetActiveList();
        }

        public static int Save(CLayer.PropertyType propertytypedata)
        {
            DataLayer.PropertyType properttype = new DataLayer.PropertyType();
            return properttype.Save(propertytypedata);
        }

        public static void Delete(int PropertyTypeId)
        {
            DataLayer.PropertyType properttype = new DataLayer.PropertyType();
            properttype.Delete(PropertyTypeId);
        }

        public static CLayer.PropertyType Get(int PropertyTypeId)
        {
            DataLayer.PropertyType properttype = new DataLayer.PropertyType();
            return properttype.Get(PropertyTypeId);
        }
    }
}
