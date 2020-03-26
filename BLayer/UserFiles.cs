using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class UserFiles
    {
        public static List<CLayer.UserFiles> GetAll(long userId)
        {
            DataLayer.UserFiles pf = new DataLayer.UserFiles();
            return pf.GetAll(userId);
        }

        public static int Save(CLayer.UserFiles file)
        {
            DataLayer.UserFiles pf = new DataLayer.UserFiles();
            file.Validate();
            return pf.Save(file);
        }
         public static long isCheck(long pUserId,int pDocument)
        {
            DataLayer.UserFiles pf = new DataLayer.UserFiles();
            return pf.isCheck(pUserId, pDocument);
        }
        public static void Delete(long fileId)
        {
            DataLayer.UserFiles pf = new DataLayer.UserFiles();
            pf.Delete(fileId);
        }

        public static void DeleteOnProperty(long userId)
        {
            DataLayer.UserFiles pf = new DataLayer.UserFiles();
            pf.DeleteOnUser(userId);
        }

        public static CLayer.UserFiles Get(long fileId)
        {
            DataLayer.UserFiles pf = new DataLayer.UserFiles();
            return pf.Get(fileId);
        }

        public static List<CLayer.UserFiles> GetOnUser(long UserId)
        {
            DataLayer.UserFiles pf = new DataLayer.UserFiles();
            return pf.GetOnUser(UserId);
        }

        public static CLayer.UserFiles GetOnUserAndType(long UserId, int Type)
        {
            DataLayer.UserFiles pf = new DataLayer.UserFiles();
            return pf.GetOnUserAndType(UserId, Type);
        }

        public static CLayer.B2B GETB2B(long b2bId)
        {
            DataLayer.B2B p = new DataLayer.B2B();
            return p.Get(b2bId);
        }
    }
}
