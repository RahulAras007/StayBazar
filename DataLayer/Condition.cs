using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Condition : DataLink
    {
        public List<CLayer.Condition> GetAllForAccommodations(string accommodationIds)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccIds", DataPlug.DataType._Varchar, accommodationIds));
            DataTable dt = Connection.GetTable("accommodation_GetAllConditions", param);
            List<CLayer.Condition> result = new List<CLayer.Condition>();
            CLayer.Condition cond;
            foreach (DataRow dr in dt.Rows)
            {
                cond = new CLayer.Condition();
                cond.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                cond.ConditionText = Connection.ToString(dr["ConditionText"]);
                result.Add(cond);
            }
            return result;
        }
        public long Save(CLayer.Condition Condition)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pConditionId", DataPlug.DataType._BigInt, Condition.ConditionId));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, Condition.AccommodationId));
            param.Add(Connection.GetParameter("pConditionText", DataPlug.DataType._Varchar, Condition.ConditionText));
            object result = Connection.ExecuteQueryScalar("condition_Save", param);
            return Connection.ToLong(Condition);
        }
      
        public List<CLayer.Condition> GetAllAccomodationId(int Start, int Limit, long? AccommodationId)
        {
            List<CLayer.Condition> result = new List<CLayer.Condition>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, AccommodationId));
            DataSet ds = Connection.GetDataSet("condition_GetAllAccommodationId", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Condition()
                {
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    ConditionId = Connection.ToLong(dr["ConditionId"]),
                    ConditionText = Connection.ToString(dr["ConditionText"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }
        public long Delete(long ConditionId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pConditionId", DataPlug.DataType._BigInt, ConditionId));
            long accid = Connection.ExecuteQuery("Condition_Delete", param);
            return (accid);
        }
        public CLayer.Condition Get(long ConditionId)
        {
            CLayer.Condition con = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pConditionId", DataPlug.DataType._BigInt, ConditionId));
            DataTable dt = Connection.GetTable("Condition_Details", param);
            if (dt.Rows.Count > 0)
            {
                con= new CLayer.Condition();
                con.AccommodationId = Connection.ToLong(dt.Rows[0]["AccommodationId"]);
                con.ConditionId = Connection.ToLong(dt.Rows[0]["ConditionId"]);
                con.ConditionText = Connection.ToString(dt.Rows[0]["ConditionText"]);             
            }
            return con;
        }
    }
}
