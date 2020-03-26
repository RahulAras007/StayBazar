using System;
using System.Collections.Generic;


namespace BLayer
{
    public class Recommended
    {
        public static List<CLayer.Recommended> GetAll()
        {
            DataLayer.Recommended rec = new DataLayer.Recommended();
            return rec.GetAll();
        }
        public static List<CLayer.Recommended> GetAllByManage(long ManageFor)
        {
            DataLayer.Recommended rec = new DataLayer.Recommended();
            return rec.GetAllByManage(ManageFor);
        }
        public static void Save(CLayer.Recommended data)
        {
            DataLayer.Recommended rec = new DataLayer.Recommended();
            rec.Save(data);
        }
        public static CLayer.Recommended GetData(long propertyId, long ManageFor)
        {
            DataLayer.Recommended rec = new DataLayer.Recommended();
            return rec.GetData(propertyId,ManageFor);
        }
        public static void Remove(long propertyId, long ManageFor)
        {
            DataLayer.Recommended rec = new DataLayer.Recommended();
            rec.Remove(propertyId,ManageFor);
        }
        public static List<CLayer.Recommended> GetAllByManageWithGDS(long ManageFor)
        {
            DataLayer.Recommended rec = new DataLayer.Recommended();
            return rec.GetAllByManageWithGDS(ManageFor);
        }
        public static void SaveWithGDS(CLayer.Recommended data)
        {
            DataLayer.Recommended rec = new DataLayer.Recommended();
            rec.SaveWithGDS(data);
        }
    }
}
