using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Suggest : DataLink
    {

        public List<CLayer.Suggest> GetAll()
        {
            string query = "SELECT p.*,c.Name as Country FROM propertysuggestion p" +
                           " INNER JOIN country c ON c.CountryId = p.CountryId" +
                           " WHERE (p.Status=0 OR p.Status=1) ORDER BY p.Status ASC,p.SuggestionDate DESC";
            return GetAll(query);
        }

        public List<CLayer.Suggest> GetAll(int status)
        {
            string query = "SELECT p.*,c.Name as Country FROM propertysuggestion p" +
                           " INNER JOIN country c ON c.CountryId = p.CountryId" +
                           " WHERE p.Status=" + status +
                           " ORDER BY p.SuggestionDate DESC";
            return GetAll(query);
        }

        private List<CLayer.Suggest> GetAll(string query)
        {
            DataTable dt = Connection.GetSQLTable(query);
            List<CLayer.Suggest> result = new List<CLayer.Suggest>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Suggest()
                {
                    InfoId = Connection.ToLong(dr["InfoId"]),
                    Name = Connection.ToString(dr["Name"]),
                    Location = Connection.ToString(dr["Location"]),
                    City = Connection.ToString(dr["City"]),
                    Country = Connection.ToString(dr["Country"]),
                    Address = Connection.ToString(dr["Address"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Email = Connection.ToString(dr["Email"]),
                    SuggestionDate = Connection.ToDate(dr["SuggestionDate"]),
                    Status = Connection.ToInteger(dr["Status"])
                });
            }
            return result;
        }

        public int Save(CLayer.Suggest suggest)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pInfoId", DataPlug.DataType._BigInt, suggest.InfoId));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, suggest.Name));
            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, suggest.Location));
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, suggest.City));
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._Int, suggest.CountryId));
            param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, suggest.Address));
            param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, suggest.Phone));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, suggest.Email));
            object result = Connection.ExecuteQueryScalar("propertysuggestion_Save", param);
            return Connection.ToInteger(result);
        }

        public CLayer.Suggest Get(int InfoId)
        {
            CLayer.Suggest suggest = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pInfoId", DataPlug.DataType._BigInt, InfoId));
            DataTable dt = Connection.GetTable("propertysuggestion_Get", param);
            if (dt.Rows.Count > 0)
            {
                suggest = new CLayer.Suggest();
                suggest.InfoId = Connection.ToLong(dt.Rows[0]["InfoId"]);
                suggest.Name = Connection.ToString(dt.Rows[0]["Name"]);
                suggest.Location = Connection.ToString(dt.Rows[0]["Location"]);
                suggest.City = Connection.ToString(dt.Rows[0]["City"]);
                suggest.CountryId = Connection.ToInteger(dt.Rows[0]["CountryId"]);
                suggest.Country = Connection.ToString(dt.Rows[0]["Country"]);
                suggest.Address = Connection.ToString(dt.Rows[0]["Address"]);
                suggest.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                suggest.Email = Connection.ToString(dt.Rows[0]["Email"]);
                suggest.SuggestionDate = Connection.ToDate(dt.Rows[0]["SuggestionDate"]);
                suggest.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
            }
            return suggest;
        }

        public void Delete(int InfoId)
        {
            Connection.ExecuteSqlQuery("DELETE FROM propertysuggestion WHERE InfoId =" + InfoId.ToString());
            return;
        }

        public void Delete(string InfoIds)
        {
            Connection.ExecuteSqlQuery("DELETE FROM propertysuggestion WHERE InfoId IN(" + InfoIds + ")");
            return;
        }

        public void SetStatus(int InfoId, int Status)
        {
            Connection.ExecuteSqlQuery("update propertysuggestion set `Status`=" + Status.ToString() + " WHERE InfoId=" + InfoId.ToString());
            return;
        }

        public void SetStatus(string InfoIds, int Status)
        {
            Connection.ExecuteSqlQuery("update propertysuggestion set `Status`=" + Status.ToString() + " WHERE InfoId IN(" + InfoIds + ")");
            return;
        }
    }
}
