using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Query : DataLink
    {
        public List<CLayer.Query> GetAll(int msgType)
        {
            DataTable dt = Connection.GetSQLTable("SELECT QueryId,Subject,`Status`,CreatedOn FROM query WHERE `Status` != 2 AND MsgType="+ msgType.ToString() +" ORDER BY `Status` ASC,CreatedOn DESC");
            List<CLayer.Query> result = new List<CLayer.Query>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Query()
                {
                    QueryId = Connection.ToLong(dr["QueryId"]),
                    Subject = Connection.ToString(dr["Subject"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    CreatedOn = Connection.ToDate(dr["CreatedOn"])
                });
            }
            return result;
        }

        public List<CLayer.Query> GetArchive(int msgType)
        {
            DataTable dt = Connection.GetSQLTable("SELECT QueryId,Subject,`Status`,CreatedOn FROM query WHERE `Status` = 2 AND MsgType=" + msgType.ToString() + " ORDER BY CreatedOn DESC");
            List<CLayer.Query> result = new List<CLayer.Query>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Query()
                {
                    QueryId = Connection.ToLong(dr["QueryId"]),
                    Subject = Connection.ToString(dr["Subject"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    CreatedOn = Connection.ToDate(dr["CreatedOn"])
                }   );
            }
            return result;
        }

        public int Save(CLayer.Query query)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, query.Name));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, query.Email));
            param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, query.Phone));
            param.Add(Connection.GetParameter("pSubject", DataPlug.DataType._Varchar, query.Subject));
            param.Add(Connection.GetParameter("pBody", DataPlug.DataType._Varchar, query.Body));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, (int) query.MessageType));
            object result = Connection.ExecuteQueryScalar("query_Save", param);
            return Connection.ToInteger(result);
        }
        public int Savefeedback(string email,string feedback)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, ""));
            param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, ""));
            param.Add(Connection.GetParameter("pSubject", DataPlug.DataType._Varchar, ""));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, 0));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar,email));
            param.Add(Connection.GetParameter("pBody", DataPlug.DataType._Varchar, feedback));
            object result = Connection.ExecuteQueryScalar("query_Save", param);
            return Connection.ToInteger(result);
        }
        //public string Name { get; set; }
        //public string ContactNo { get; set; }
        //public string Email { get; set; }
        //public string EventName { get; set; }
        //public string EventLocation { get; set; }
        //public string CheckIn { get; set; }
        //public string CheckOut { get; set; }
        //public long NoOfAdult { get; set; }
        //public long NoOfChild { get; set; }
        public int SaveQueryForm(CLayer.FormSubmitcs data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, data.Name));
            param.Add(Connection.GetParameter("pContactno", DataPlug.DataType._Varchar, data.ContactNo));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, data.Email));
            param.Add(Connection.GetParameter("pEventName", DataPlug.DataType._Varchar, data.EventName));
            param.Add(Connection.GetParameter("pEventLocation", DataPlug.DataType._Varchar, data.EventLocation));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, data.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, data.CheckOut));
            param.Add(Connection.GetParameter("pNoOfAdult", DataPlug.DataType._BigInt, data.NoOfAdult));
            param.Add(Connection.GetParameter("pNoOfChild", DataPlug.DataType._BigInt, data.NoOfChild));
            param.Add(Connection.GetParameter("pWebsite", DataPlug.DataType._Varchar, data.Website));
            param.Add(Connection.GetParameter("pRooms", DataPlug.DataType._BigInt, data.NoOfRooms));
            object result = Connection.ExecuteQueryScalar("queryForm_Save", param);
            return Connection.ToInteger(result);
        }
        
        public CLayer.Query Get(int QueryId)
        {
            CLayer.Query query = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pQueryId", DataPlug.DataType._BigInt, QueryId));
            DataTable dt = Connection.GetTable("query_Get", param);
            if (dt.Rows.Count > 0)
            {
                query = new CLayer.Query();
                query.QueryId = Connection.ToLong(dt.Rows[0]["QueryId"]);
                query.Name = Connection.ToString(dt.Rows[0]["Name"]);
                query.Email = Connection.ToString(dt.Rows[0]["Email"]);
                query.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                query.Subject = Connection.ToString(dt.Rows[0]["Subject"]);
                query.Body = Connection.ToString(dt.Rows[0]["Body"]);
                query.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                query.CreatedOn = Connection.ToDate(dt.Rows[0]["CreatedOn"]);
            }
            return query;
        }

        public void Delete(int QueryId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pQueryId", DataPlug.DataType._BigInt, QueryId));
            Connection.ExecuteQuery("query_Delete", param);
            return;
        }

        public void Delete(string QueryIds)
        {
            Connection.ExecuteSqlQuery("DELETE FROM `query` WHERE QueryId IN(" + QueryIds + ")");
            return;
        }

        public void SetStatus(int QueryId, int Status)
        {
            Connection.ExecuteSqlQuery("update `query` set `Status`=" + Status.ToString() + " WHERE QueryId=" + QueryId.ToString());
            return;
        }

        public void SetStatus(string QueryIds, int Status)
        {
            Connection.ExecuteSqlQuery("update `query` set `Status`=" + Status.ToString() + " WHERE QueryId IN(" + QueryIds + ")");
            return;
        }
    }
}
