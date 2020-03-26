using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class Service : DataLink
    {
        public int GetSupplierCountForMail(DateTime forDate)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingDate", DataPlug.DataType._Date, forDate));
            object obj = Connection.ExecuteQueryScalar("b2b_GetSupplierCountForMail", param);
            return Connection.ToInteger(obj);
        }
        public List<string> GetSupplierIDsForMail(DateTime forDate,int start,int limit)
        {
            List<string> ids = new List<string>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingDate", DataPlug.DataType._Date, forDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataTable dt = Connection.GetTable("b2b_GetSupplierIdsForMail", param);

            foreach(DataRow dr in dt.Rows)
            {
                 ids.Add(Connection.ToString(dr["Userid"]) + "#" + Connection.ToString(dr["Email"]));
            }
            return ids;
        }
        public List<string> GetPartialPaymentList()
        {
            List<string> ids = new List<string>();
            DataTable dt = Connection.GetTable("PartialPayment_GetAllDetailsForEmail");
            foreach (DataRow dr in dt.Rows)
            {
                string items = Connection.ToString(dr["BookingId"]) + "#" + Connection.ToString(dr["ForUserEmail"]);
                if (!ids.Contains(items))
                {
                    ids.Add(Connection.ToString(dr["BookingId"]) + "#" + Connection.ToString(dr["ForUserEmail"]));
                }
            }
            return ids;
        }

        public List<string> GetPartialPaymentBCancellation()
        {
            List<string> ids = new List<string>();
            DataTable dt = Connection.GetTable("PartialPayment_GetAllBCancellationForEmail");

            foreach (DataRow dr in dt.Rows)
            {
                ids.Add(Connection.ToString(dr["BookingId"]) + "#" + Connection.ToString(dr["ForUserEmail"]));
            }
            return ids;
        }
    }
}
