using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class StaticPage
    {
        public static void SetUpdateDate(long pageId)
        {
            DataLayer.StaticPage st = new DataLayer.StaticPage();
            st.SetUpdateDate(pageId);
        }
        public static List<CLayer.StaticPage> GetAll()
        {
            DataLayer.StaticPage st = new DataLayer.StaticPage();
            return st.GetAll();
        }
        public static List<CLayer.StaticPage> GetAllWidget()
        {
            DataLayer.StaticPage st = new DataLayer.StaticPage();
            return st.GetAllWidget();
        }
        public static CLayer.StaticPage GetPage(long pageId)
        {
            DataLayer.StaticPage st = new DataLayer.StaticPage();
            return st.GetPage(pageId);
        }

        public static void Delete(long pageId)
        {
            DataLayer.StaticPage st = new DataLayer.StaticPage();
            st.Delete(pageId);
        }
        public static long Save(CLayer.StaticPage data)
        {
            DataLayer.StaticPage st = new DataLayer.StaticPage();
           return  st.Save(data);
        }
        public static long SaveProperty(CLayer.StaticPage data)
        {
            DataLayer.StaticPage st = new DataLayer.StaticPage();
            return st.SaveProperty(data);
        }
        public static void RemoveStaticProperty(long PropertyId)
        {
            DataLayer.StaticPage b2b = new DataLayer.StaticPage();
            b2b.RemoveStaticProperty(PropertyId);
        }
    }
}
