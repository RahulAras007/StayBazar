using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class State : DataLink
    {
        public long GetInvoiceNumber(long stateId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._BigInt, stateId));
            return Connection.ToLong(Connection.ExecuteQueryScalar("State_InvoiceNumber", param));
        }

        public string GetGSTCode(long stateId)
        {
            string sql = "SELECT GSTStateCode FROM state WHERE stateid= " + stateId.ToString();
            return Connection.ToString(Connection.ExecuteSQLQueryScalar(sql));
        }
        public List<CLayer.State> GetOnCountry(int countryId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._Int, countryId));
            DataTable dt = Connection.GetTable("state_GetOnCountry", param);
            List<CLayer.State> result = new List<CLayer.State>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.State()
                {
                    StateId = Connection.ToInteger(dr["StateId"]),
                    Name = Connection.ToString(dr["Name"]),
                    CountryId = Connection.ToInteger(dr["CountryId"])
                });
            }
            return result;
        }
        public List<CLayer.State> GetAllState()
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            DataTable dt = Connection.GetTable("GetAllState", param);
            List<CLayer.State> result = new List<CLayer.State>();
          
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.State()
                {
                    StateId = Connection.ToInteger(dr["StateId"]),
                    Name = Connection.ToString(dr["Name"])
                });
            }
            return result;
        }

        public List<CLayer.State> GetCustGstStateList(int Customerid, int Type)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomerid", DataPlug.DataType._Int, Customerid));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            //param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            
            DataTable dt = Connection.GetTable("GetCustGstStateList", param);
            List<CLayer.State> result = new List<CLayer.State>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.State()
                {
                    StateId = Connection.ToInteger(dr["StateId"]),
                    Name = Connection.ToString(dr["Name"])
                });
            }
            return result;
        }
        public CLayer.State Get(int stateId)
        {
            CLayer.State state = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._Int, stateId));
            DataTable dt = Connection.GetTable("state_Get", param);
            if (dt.Rows.Count > 0)
            {
                state = new CLayer.State();
                state.StateId = Connection.ToInteger(dt.Rows[0]["StateId"]);
                state.Name = Connection.ToString(dt.Rows[0]["Name"]);
                state.CountryId = Connection.ToInteger(dt.Rows[0]["CountryId"]);
                state.GSTStateCode= Connection.ToString(dt.Rows[0]["GSTStateCode"]);
            }
            else
            {
                state = new CLayer.State();
                state.StateId = 0;
                state.Name = "";
                state.CountryId = 0;
                state.GSTStateCode = "";
            }
            return state;
        }
        public CLayer.State GetStateID(string  cityname)
        {
            CLayer.State state = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, cityname));
            DataTable dt = Connection.GetTable("stateID_Get", param);
            if (dt.Rows.Count > 0)
            {
                state = new CLayer.State();
                state.StateId = Connection.ToInteger(dt.Rows[0]["StateId"]);
                state.Name = Connection.ToString(dt.Rows[0]["Name"]);
                //state.CountryId = Connection.ToInteger(dt.Rows[0]["CountryId"]);
                //state.GSTStateCode = Connection.ToString(dt.Rows[0]["GSTStateCode"]);
            }
            else
            {
                state = new CLayer.State();
                state.StateId = 0;
                state.Name = "";

            }
            return state;
        }
        public CLayer.State GetStateID(string cityname,long CountryID)
        {
                    

            CLayer.State state = null;
            string sql = "SELECT StateId,Name FROM state WHERE countryid="+ CountryID + " AND NAME='"+ cityname + "'";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            if (dt.Rows.Count > 0)
            {
                state = new CLayer.State();
                state.StateId = Connection.ToInteger(dt.Rows[0]["StateId"]);
                state.Name = Connection.ToString(dt.Rows[0]["Name"]);
                //state.CountryId = Connection.ToInteger(dt.Rows[0]["CountryId"]);
                //state.GSTStateCode = Connection.ToString(dt.Rows[0]["GSTStateCode"]);
            }
            else
            {
                state = new CLayer.State();
                state.StateId = 0;
                state.Name = "";

            }
            return state;
        }
        public int Save(CLayer.State state)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._Int, state.StateId));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, state.Name));
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._Int, state.CountryId));
            param.Add(Connection.GetParameter("pGSTStateCode", DataPlug.DataType._Varchar, state.GSTStateCode));
            object result = Connection.ExecuteQueryScalar("state_Save", param);
            return Connection.ToInteger(result);
        }
        public int  GetBillingEntityStateID(long  PropertyID)
        {
            string sql = "SELECT state FROM property WHERE propertyid="+ PropertyID + " AND state IN(SELECT stateid FROM sbentity)";
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar(sql));
        }
        public int GetCustomerStateID(long CustomerID)
        {
            string sql = "SELECT state FROM address WHERE userid="+ CustomerID;
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar(sql));
        }
        public int UpdateGDSState(int  CountryID,string State= "Others")
        {
            int result = 0;
            string sql = "SELECT COUNT(*) FROM state WHERE CountryID='" + CountryID + "' and Name='"+State+"'";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            if (Connection.ToInteger(obj) < 1)
            {
                CLayer.State objState = new CLayer.State();
                objState.CountryId = CountryID;
                objState.Name = State;
                objState.StateId = 0;
                objState.GSTStateCode = State;
                 result = Save(objState);
            }
            else
            {
                 sql = "SELECT stateid FROM state WHERE CountryID='" + CountryID + "' and Name='" + State + "'";
                 obj = Connection.ExecuteSQLQueryScalar(sql);

                result = Connection.ToInteger(obj);
            }
            return result;
        }
    }
}
