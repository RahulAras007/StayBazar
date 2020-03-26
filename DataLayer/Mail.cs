using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Mail : DataLink
    {

        public string SaveMail(CLayer.Mail data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEmailId", DataPlug.DataType._Varchar, data.Mailaddress));
            object result = Connection.ExecuteQueryScalar("subscriptions_save", param);
            return Connection.ToString(result);
        }
        public long UnSubscribed(CLayer.Mail data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEmailId", DataPlug.DataType._Varchar, data.Mailaddress));
            object result = Connection.ExecuteQueryScalar("subscriptions_Unsubscribed", param);
            return Connection.ToLong(result);
        }
         public int CountOfSubscribed()
         {
             string sql = "SELECT COUNT(EmailId) AS SubscriptionCount FROM subscriptions WHERE UnSubscribed=0";
             object obj = Connection.ExecuteSQLQueryScalar(sql);
             return Connection.ToInteger(obj);     
         }
        public int CountOfUnSubscribed()
         {
             string sql = "SELECT COUNT(EmailId) AS UnSubscriptionCount FROM subscriptions WHERE UnSubscribed=1";
             object obj = Connection.ExecuteSQLQueryScalar(sql);
             return Connection.ToInteger(obj);    
         }

        public string GetMail()
        {      
            StringBuilder ids=new StringBuilder();
            string sql = "SELECT EmailId FROM subscriptions Where UnSubscribed=0";
            DataTable dt = Connection.GetSQLTable(sql);
            foreach(DataRow row in dt .Rows)
                {
                    ids.Append(Connection.ToString(row["EmailId"]) + ",");              
                }               
             return ids.ToString();
        }
        public string GetMailSubscribed()
        {
            StringBuilder ids = new StringBuilder();
            string sql = "SELECT EmailId FROM subscriptions Where UnSubscribed=0";
            DataTable dt = Connection.GetSQLTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                ids.Append(Connection.ToString(row["EmailId"]) + ",");
            }
            return ids.ToString();
        }
        public string GetMailUnSubscribed()
        {
            StringBuilder ids = new StringBuilder();
            string sql = "SELECT EmailId FROM subscriptions Where UnSubscribed=1";
            DataTable dt = Connection.GetSQLTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                ids.Append(Connection.ToString(row["EmailId"]) + ",");
            }
            return ids.ToString();
        }
        public List<CLayer.Mail> GetAllId(bool UnSubscribed)
        {
            List<CLayer.Mail> result = new List<CLayer.Mail>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUnsubscribed", DataPlug.DataType._Int, UnSubscribed));
            DataTable dt = Connection.GetTable("subscribtions_getid", param);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Mail()
                {
                    Mailaddress = Connection.ToString(dr["EmailId"]),
                    UnSubscribed = Connection.ToBoolean(dr["UnSubscribed"]),
                    CreatedOn = Connection.ToDate(dr["CreatedOn"])                  
                });
            }
            return result;
        }
    }
}



        