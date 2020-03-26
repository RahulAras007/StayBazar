using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Mail
    {
     
         public static string Savemail(CLayer.Mail Mail)
           {
               DataLayer.Mail task = new DataLayer.Mail();
               return task.SaveMail(Mail);
           }
        public static long UnSubscribed(CLayer.Mail Mail)
           {
               DataLayer.Mail task = new DataLayer.Mail();
               return task.UnSubscribed(Mail);
           }
        public static int  CountOfSubscribed()
        {
            DataLayer.Mail task = new DataLayer.Mail();
            return task.CountOfSubscribed();
        }
        public static int CountOfUnSubscribed()
        {
            DataLayer.Mail task = new DataLayer.Mail();
            return task.CountOfUnSubscribed();
        }
         public static string getMail()
        {
            DataLayer.Mail task = new DataLayer.Mail();
            return task.GetMail();
        }
        public static string GetMailUnSubscribed()
         {
             DataLayer.Mail task = new DataLayer.Mail();
             return task.GetMailUnSubscribed();
         }
      public static List<CLayer.Mail> GetAllId(bool UnSubscribed)
        {
            DataLayer.Mail sub = new DataLayer.Mail();
            return sub.GetAllId(UnSubscribed);           
        }
    }
}
