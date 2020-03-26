using System;
using System.Collections.Generic;


namespace BLayer
{
    public class SBEntity
    {
        public static List<CLayer.SBEntity> GetAll()
        {
            DataLayer.SBEntity s = new DataLayer.SBEntity();
            return s.GetAll();
        }

        public static List<CLayer.SBEntity> GetForDropdown()
        {
            DataLayer.SBEntity s = new DataLayer.SBEntity();
            return s.GetForDropdown();
        }

        public static void Delete(long id)
        {
            DataLayer.SBEntity s = new DataLayer.SBEntity();
            s.Delete(id);
        }

        public static long Save(CLayer.SBEntity data)
        {
            DataLayer.SBEntity s = new DataLayer.SBEntity();
            return s.Save(data);
        }


        public static CLayer.SBEntity Get(long id)
        {
            DataLayer.SBEntity s = new DataLayer.SBEntity();
            return s.Get(id);
        }

        public static int SBEntityByStateId(int id)
        {
            DataLayer.SBEntity s = new DataLayer.SBEntity();
            return s.SBEntityByStateId(id);
        }
    }
}
