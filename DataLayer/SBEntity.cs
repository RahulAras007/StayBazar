using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class SBEntity : DataLink
    {

        public List<CLayer.SBEntity> GetAll()
        {
            List<CLayer.SBEntity> typelist = new List<CLayer.SBEntity>();

            DataTable dt = Connection.GetSQLTable("Select s.EntityId,s.Name,s.Address,s.CountryId,s.GSTNo,s.StateId,s.GSTStateId,st.Name as State from SBEntity s inner join state st on s.StateId = st.StateId Where Status=1");
            foreach (DataRow dr in dt.Rows)
            {
                typelist.Add(new CLayer.SBEntity()
                {
                    EntityId = Connection.ToLong(dr["EntityId"]),
                    Name = Connection.ToString(dr["Name"]),
                    Address = Connection.ToString(dr["Address"]),
                    CountryId = Connection.ToInteger(dr["CountryId"]),
                    GSTNo = Connection.ToString(dr["GSTNo"]),
                    StateId = Connection.ToInteger(dr["StateId"]),
                    GSTStateId = Connection.ToInteger(dr["GSTStateId"]),
                    State = Connection.ToString(dr["State"])
                });
            }

            return typelist;
        }

        public List<CLayer.SBEntity> GetForDropdown()
        {
            List<CLayer.SBEntity> typelist = new List<CLayer.SBEntity>();

            DataTable dt = Connection.GetSQLTable("Select * from SBEntity Where Status=1");
            foreach (DataRow dr in dt.Rows)
            {
                typelist.Add(new CLayer.SBEntity()
                {
                    EntityId = Connection.ToLong(dr["EntityId"]),
                    Name = Connection.ToString(dr["Name"])
                });
            }

            return typelist;
        }

        public void Delete(long id)
        {

            Connection.ExecuteSqlQuery("Update SBEntity Set Status=" + ((int)CLayer.ObjectStatus.StatusType.Deleted).ToString() + " Where EntityId=" + id.ToString());

        }

        public long Save(CLayer.SBEntity data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEntityId", DataPlug.DataType._BigInt, data.EntityId));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, data.Name));
            param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, data.Address));
            param.Add(Connection.GetParameter("pGSTNo", DataPlug.DataType._Varchar, data.GSTNo));
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._BigInt, data.CountryId));
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._BigInt, data.StateId));
            param.Add(Connection.GetParameter("pGSTStateId", DataPlug.DataType._BigInt, data.GSTStateId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, data.Phone));
            object result = Connection.ExecuteQueryScalar("SBEntity_Save", param);
            return Connection.ToLong(result);
        }



        public CLayer.SBEntity Get(long id)
        {
            CLayer.SBEntity result = null;

            DataTable dt = Connection.GetSQLTable("Select * from SBEntity Where EntityId=" + id.ToString());
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = new CLayer.SBEntity();
                result.EntityId = Connection.ToLong(dr["EntityId"]);
                result.Name = Connection.ToString(dr["Name"]);
                result.Address = Connection.ToString(dr["Address"]);
                result.CountryId = Connection.ToInteger(dr["CountryId"]);
                result.GSTNo = Connection.ToString(dr["GSTNo"]);
                result.StateId = Connection.ToInteger(dr["StateId"]);
                result.GSTStateId = Connection.ToInteger(dr["GSTStateId"]);
                result.Phone = Connection.ToString(dr["Phone"]);
            }
            return result;
        }
        public int SBEntityByStateId(int id)
        {
            string sql = "select count(*) from sbentity where StateId =" + id;
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar(sql));
        }
    }
}
