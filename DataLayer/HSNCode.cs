using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class HSNCode : DataLink
    {
        public List<CLayer.HSNCode> GetAll()
        {
            List<CLayer.HSNCode> typelist = new List<CLayer.HSNCode>();

            DataTable dt = Connection.GetSQLTable("Select CodeId,HSNCode,NatureOfService from HSNCode Where Status = 1");
            foreach (DataRow dr in dt.Rows)
            {
                typelist.Add(new CLayer.HSNCode()
                {
                    CodeId = Connection.ToLong(dr["CodeId"]),
                    NatureOfService = Connection.ToString(dr["NatureOfService"]),
                    Code = Connection.ToString(dr["HSNCode"]),
                    Status = 1
                });
            }

            return typelist;
        }

        

        public void Delete(long id)
        {

            Connection.ExecuteSqlQuery("Update HSNCode Set Status=" + ((int)CLayer.ObjectStatus.StatusType.Deleted).ToString() + " Where CodeId=" + id.ToString());

        }

        public long Save(CLayer.HSNCode data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCodeId", DataPlug.DataType._BigInt, data.CodeId));
            param.Add(Connection.GetParameter("pService", DataPlug.DataType._Varchar, data.NatureOfService));
            param.Add(Connection.GetParameter("pCode", DataPlug.DataType._Varchar, data.Code));
       

            object result = Connection.ExecuteQueryScalar("HSNCode_Save", param);
            return Connection.ToLong(result);
        }



        public CLayer.HSNCode Get(long id)
        {
            CLayer.HSNCode result = null;

            DataTable dt = Connection.GetSQLTable("Select * from HSNCode Where CodeId=" + id.ToString());
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                result = new CLayer.HSNCode();
                result.CodeId = Connection.ToLong(dr["CodeId"]);
                result.NatureOfService = Connection.ToString(dr["NatureOfService"]);
                result.Code = Connection.ToString(dr["HSNCode"]);
                result.Status = Connection.ToInteger(dr["Status"]);
            }
            return result;
        }
    }
}
