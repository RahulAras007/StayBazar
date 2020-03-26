using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
   public class GDSTransactionsLog : DataLink
    {
        public void GenerateGDSLog(CLayer.GDSTransactionsLog data)
        {
            try
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pGDSTransID", DataPlug.DataType._Text , data.GDSTransID));
                param.Add(Connection.GetParameter("pGDSSearchCriteria", DataPlug.DataType._Text, data.GDSSearchCriteria));
                param.Add(Connection.GetParameter("pGDSRequest", DataPlug.DataType._Text, data.GDSRequest));
                param.Add(Connection.GetParameter("pGDSResult", DataPlug.DataType._Text , data.GDSResult));
                param.Add(Connection.GetParameter("pGDSError", DataPlug.DataType._Text , data.GDSError));
                param.Add(Connection.GetParameter("pGDSErrorMessage", DataPlug.DataType._Varchar , data.GDSErrorMessage));
                param.Add(Connection.GetParameter("pUserID", DataPlug.DataType._Int, data.UserID));
                param.Add(Connection.GetParameter("pGDSType", DataPlug.DataType._Int, data.GDSType));
                param.Add(Connection.GetParameter("pGDSBookingType", DataPlug.DataType._Int, data.GDSBookingType));
                param.Add(Connection.GetParameter("pBookingID", DataPlug.DataType._BigInt, data.BookingID));

                object result = Connection.ExecuteQueryScalar("GenerateGDSLog", param);
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public long ClearGDSTransactionCount()
        {
            long result = 0;
            try
            {
                object obj = Connection.ExecuteSQLQueryScalar("SELECT COUNT(*) FROM gdstransactionslog WHERE createdat<CURDATE()-INTERVAL 7 DAY");
                result= Connection.ToLong(obj);

                if(result>0)
                {
                    string sql = "DELETE FROM gdstransactionslog WHERE createdat<CURDATE()-INTERVAL 7 DAY";
                    Connection.ExecuteSqlQuery(sql);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public List<CLayer.GDSTransactionsLog> GetGDSTransactionLog()
        {
            try
            {
                List<CLayer.GDSTransactionsLog> result = new List<CLayer.GDSTransactionsLog>();
              
                DataTable dt = Connection.GetTable("GetGDSTransactionLog");
                foreach (DataRow dr in dt.Rows)
                {
                    CLayer.GDSTransactionsLog objResult = new CLayer.GDSTransactionsLog();
                    objResult.GDSID = Connection.ToLong(dr["GDSID"]);
                    objResult.BookingID = Connection.ToLong(dr["BookingID"]);
                    objResult.CreatedAt = Connection.ToDate(dr["CreatedAt"]);
                    objResult.GDSTypeValue = Connection.ToString(dr["GDSTypeValue"]);
                    objResult.GDSRequest = Connection.ToString(dr["GDSRequest"]);
                    objResult.GDSResult = Connection.ToString(dr["GDSResult"]);
                    objResult.GDSError = Connection.ToString(dr["GDSError"]);
                    objResult.GDSErrorMessage = Connection.ToString(dr["GDSErrorMessage"]);
                    objResult.UserID = Connection.ToInteger(dr["UserID"]);
                    objResult.GDSTransID= Connection.ToString(dr["GDSTransID"]);

                    result.Add(objResult);

                }
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
