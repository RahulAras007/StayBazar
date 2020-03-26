using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class PropertyFiles
    {
        public static List<CLayer.PropertyFiles> GetAll(long propertyId)//, CLayer.PropertyFiles.FileTypes fileType
        {
            DataLayer.PropertyFiles pf = new DataLayer.PropertyFiles();
            return pf.GetAll(propertyId);//, fileType
        }

        public static int Save(CLayer.PropertyFiles file)
        {
            DataLayer.PropertyFiles pf = new DataLayer.PropertyFiles();
            file.Validate();
            return pf.Save(file);
        }

        public static void Delete(long fileId)
        {
            DataLayer.PropertyFiles pf = new DataLayer.PropertyFiles();
            pf.Delete(fileId);
        }

        public static void DeleteOnProperty(long propertyid)
        {
            DataLayer.PropertyFiles pf = new DataLayer.PropertyFiles();
            pf.DeleteOnProperty(propertyid);
        }

        public static CLayer.PropertyFiles Get(long fileId)
        {
            DataLayer.PropertyFiles pf = new DataLayer.PropertyFiles();
            return pf.Get(fileId);
        }
    }
}
