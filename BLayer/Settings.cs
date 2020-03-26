using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Settings
    {
        public static string GetValue(string key)
        {
            return (new DataLayer.Settings()).GetValue(key).Trim();
        }

        public static void SetValue(string key,string value)
        {
            (new DataLayer.Settings()).SetValue(key,value);
        }

        public static CLayer.Settings.MailSecurityType GetMailSecurityType(string value)
        {
            CLayer.Settings.MailSecurityType result;
            int idx = Convert.ToInt32(value);
            result = (CLayer.Settings.MailSecurityType)idx;
            return result;
        }


    }
}
