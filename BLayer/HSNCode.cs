using System;
using System.Collections.Generic;


namespace BLayer
{
    public class HSNCode
    {
        public static List<CLayer.HSNCode> GetAll()
        {
            DataLayer.HSNCode hsn = new DataLayer.HSNCode();
            return hsn.GetAll();
        }

        public static void Delete(long id)
        {
            DataLayer.HSNCode hsn = new DataLayer.HSNCode();
            hsn.Delete(id);  
        }

        public static long Save(CLayer.HSNCode data)
        {
            DataLayer.HSNCode hsn = new DataLayer.HSNCode();
            return hsn.Save(data);
        }

        public static CLayer.HSNCode Get(long id)
        {
            DataLayer.HSNCode hsn = new DataLayer.HSNCode();
            return hsn.Get(id);
        }
    }
}
