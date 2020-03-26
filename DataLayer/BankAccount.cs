using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class BankAccount : DataLink
    {
        public CLayer.BankAccount GetOnUser(long UserId)
        {
            CLayer.BankAccount result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            DataTable dt = Connection.GetTable("bankaccount_GetOnUser", param);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.BankAccount()
                {
                    BankAccountId = Connection.ToLong(dt.Rows[0]["BankAccountId"]),
                    UserId = Connection.ToLong(dt.Rows[0]["UserId"]),
                    AccountName = Connection.ToString(dt.Rows[0]["AccountName"]),
                    AccountNumber = Connection.ToString(dt.Rows[0]["AccountNumber"]),
                    BankName = Connection.ToString(dt.Rows[0]["BankName"]),
                    BranchAddress = Connection.ToString(dt.Rows[0]["BranchAddress"]),
                    RTGSNumber = Connection.ToString(dt.Rows[0]["RTGSNumber"]),
                    MICRCode = Connection.ToString(dt.Rows[0]["MIRCCode"])
                };
            }           
            return result;
        }
        public long Save(CLayer.BankAccount data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBankAccountId", DataPlug.DataType._BigInt, data.BankAccountId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            param.Add(Connection.GetParameter("pAccountName", DataPlug.DataType._Varchar, data.AccountName));
            param.Add(Connection.GetParameter("pAccountNumber", DataPlug.DataType._Varchar, data.AccountNumber));
            param.Add(Connection.GetParameter("pBankName", DataPlug.DataType._Varchar, data.BankName));
            param.Add(Connection.GetParameter("pBranchAddress", DataPlug.DataType._Varchar, data.BranchAddress));
            param.Add(Connection.GetParameter("pRTGSNumber", DataPlug.DataType._Varchar, data.RTGSNumber));
            param.Add(Connection.GetParameter("pMIRCCode", DataPlug.DataType._Varchar, data.MICRCode));
            object result = Connection.ExecuteQueryScalar("bankaccount_Save", param);
            return Connection.ToInteger(result);
        }
    }
}
