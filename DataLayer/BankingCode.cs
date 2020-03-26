using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class BankingCode : DataLink
    {
        public CLayer.ObjectStatus.PaymentMethod GetPaymentMethod(string code)
        {
            string sql = "Select PayType From Bankingcodes Where code='" + code + "'";
            DataTable dt = Connection.GetSQLTable(sql);
            if(dt.Rows.Count > 0 )
            {
                return (CLayer.ObjectStatus.PaymentMethod) Connection.ToInteger(dt.Rows[0]["PayType"]);
            }
            return CLayer.ObjectStatus.PaymentMethod.None;
        }

        public CLayer.BankingCode GetBankingCode(string code)
        {
            CLayer.BankingCode result = null;
            string sql = "Select * From Bankingcodes Where code='" + code + "'";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.BankingCode();
                result.BankingTitle = Connection.ToString(dt.Rows[0]["BankingTitle"]);
                result.PaymentGatewayId = Connection.ToString(dt.Rows[0]["PaymentGatewayId"]);
                result.PayType = (CLayer.ObjectStatus.PaymentMethod) Connection.ToInteger(dt.Rows[0]["PayType"]);
                result.Code = code;
            }
            return result;
        }

        public void Save(CLayer.BankingCode data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPayType", DataPlug.DataType._Int, data.PayType));
            param.Add(Connection.GetParameter("pPaymentGatewayId", DataPlug.DataType._Char, data.PaymentGatewayId));
            param.Add(Connection.GetParameter("pBankingTitle", DataPlug.DataType._Varchar, data.BankingTitle));
            param.Add(Connection.GetParameter("pCode", DataPlug.DataType._Varchar, data.Code));
            Connection.ExecuteQuery("bankingcode_Save", param);
        }
    }
}
