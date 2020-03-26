using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class CreditBooking : DataLink
    {
        public void SaveCorBookingPayment(CLayer.CreditBooking data)
        {          
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.bookid));          
            param.Add(Connection.GetParameter("pCreditComment", DataPlug.DataType._Varchar, data.CreditComment));
            //param.Add(Connection.GetParameter("pPaid", DataPlug.DataType._Bool, data.Paid));
            param.Add(Connection.GetParameter("pPaymentdate", DataPlug.DataType._Varchar, data.Paymentdate));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._Varchar, data.UpdatedBy));
            param.Add(Connection.GetParameter("pUpdatedDate", DataPlug.DataType._DateTime, DateTime.Today));
            object id = Connection.ExecuteQueryScalar("CreditBooking_Save", param);

        }
        public void SetCreditBookingstatus(int status, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            Connection.ExecuteQuery("booking_SetCreditBookingstatus", param);
        }
        public int GetCorpCreditPaymentStatus(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            object obj = Connection.ExecuteQueryScalar("booking_GetCorpCreditPaymentStatus", param);
            return Connection.ToInteger(obj);
        }
        public CLayer.CreditBooking GetAllCredBookDetailsbyBookid(long BookingId)
        {
            CLayer.CreditBooking CorpCdtbook = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int,BookingId));
            DataTable dt = Connection.GetTable("CorpCreditbooking_GetAll", param);
            if (dt.Rows.Count > 0)
            {
                CorpCdtbook = new CLayer.CreditBooking();
                CorpCdtbook.bookid = BookingId;
                CorpCdtbook.CreditComment = Connection.ToString(dt.Rows[0]["CreditComment"]);
                CorpCdtbook.Paymentdate = Connection.ToString(dt.Rows[0]["Paymentdate"]);
            }
            return CorpCdtbook;
        }
        public long GetCountForBookings(long bookuserid, DateTime FDate, DateTime Tdate)
        {
            string sql = "Select Count(*) From booking Where PaymentOption = 3 and ( BookingDate >= '" + FDate.ToString("yyyy/MM/dd HH:mm:ss") + "' and  BookingDate <=  '" + Tdate.ToString("yyyy/MM/dd HH:mm:ss") + "' )  and status = 2 and ByUserId=" + bookuserid.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        public void SetCreditBookingPaid(bool Paid, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pPaid", DataPlug.DataType._Bool, Paid));
            Connection.ExecuteQuery("booking_SetCreditBookingPaid", param);
        }

    }
}
