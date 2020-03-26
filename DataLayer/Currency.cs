using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Currency : DataLink
    {

        public List<CLayer.Currency> GetForListing()
        {
            DataTable dt = Connection.GetTable("currency_GetAll");
            List<CLayer.Currency> result = new List<CLayer.Currency>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Currency()
                {
                    CurrencyId = Connection.ToLong(dr["CurrencyId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Symbol = Connection.ToString(dr["Symbol"]),
                    ConversionRate = Connection.ToDecimal(dr["ConversionRate"]),
                   // IsDefault = Connection.ToBoolean(dr["IsDefault"]),
                });
            }
            return result;
        }

        public void SetRate(int currencyId, double rate)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCurrencyId", DataPlug.DataType._BigInt, currencyId));
            param.Add(Connection.GetParameter("pRate", DataPlug.DataType._Decimal, rate));
            Connection.ExecuteQuery("currency_SetRate", param);
            //string sql = "Update currency Set ConversionRate=" + rate.ToString() + " Where CurrencyId=" + currencyId.ToString();
            //Connection.ExecuteSqlQuery(sql);
        }
        public List<CLayer.Currency> GetAll()
        {
            DataTable dt = Connection.GetSQLTable("SELECT * FROM currency order by IsDefault DESC,Title");
            List<CLayer.Currency> result = new List<CLayer.Currency>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Currency()
                {
                    CurrencyId = Connection.ToLong(dr["CurrencyId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Symbol = Connection.ToString(dr["Symbol"]),
                    ConversionRate = Connection.ToDecimal(dr["ConversionRate"]),
                    ConversionPercentage = Connection.ToDecimal(dr["ConversionPercentage"]),
                    LastUpdate = Connection.ToDate(dr["LastUpdate"]),
                    IsDefault = Connection.ToBoolean(dr["IsDefault"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    Exchangecode = Connection.ToString(dr["ExchgCode"])

                });
            }
            return result;
        }

        public int Save(CLayer.Currency currency)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCurrencyId", DataPlug.DataType._BigInt, currency.CurrencyId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, currency.Title));
            param.Add(Connection.GetParameter("pSymbol", DataPlug.DataType._Varchar, currency.Symbol));
            param.Add(Connection.GetParameter("pConversionRate", DataPlug.DataType._Decimal, currency.ConversionRate));
            param.Add(Connection.GetParameter("pConversionPercentage", DataPlug.DataType._Decimal, currency.ConversionPercentage));
            param.Add(Connection.GetParameter("pIsDefault", DataPlug.DataType._Bool, currency.IsDefault));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, currency.Status));
            param.Add(Connection.GetParameter("pExchangecode", DataPlug.DataType._Varchar, currency.Exchangecode));

            object result = Connection.ExecuteQueryScalar("currency_Save", param);
            return Connection.ToInteger(result);
        }


        public int GetDefault()
        {
            object obj = Connection.ExecuteQueryScalar("currency_GetDefault'");
            return Connection.ToInteger(obj);
        }
        //
        // TODO Delete Currency
        //

        public CLayer.Currency Get(string code)
        {
            CLayer.Currency currency = null;
            
            DataTable dt = Connection.GetSQLTable("Select * From currency Where Title like '" + code + "'");
            if (dt.Rows.Count > 0)
            {
                currency = new CLayer.Currency();
                currency.CurrencyId = Connection.ToLong(dt.Rows[0]["CurrencyId"]);
                currency.Title = Connection.ToString(dt.Rows[0]["Title"]);
                currency.Symbol = Connection.ToString(dt.Rows[0]["Symbol"]);
                currency.ConversionRate = Connection.ToDecimal(dt.Rows[0]["ConversionRate"]);
                currency.ConversionPercentage = Connection.ToDecimal(dt.Rows[0]["ConversionPercentage"]);
                currency.LastUpdate = Connection.ToDate(dt.Rows[0]["LastUpdate"]);
                currency.IsDefault = Connection.ToBoolean(dt.Rows[0]["IsDefault"]);
                currency.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                currency.Exchangecode = Connection.ToString(dt.Rows[0]["ExchgCode"]);
            }
            return currency;
        }

        public CLayer.Currency Get(int CurrencyId)
        {
            CLayer.Currency currency = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCurrencyId", DataPlug.DataType._BigInt, CurrencyId));
            DataTable dt = Connection.GetTable("currency_Get", param);
            if (dt.Rows.Count > 0)
            {
                currency = new CLayer.Currency();
                currency.CurrencyId = Connection.ToLong(dt.Rows[0]["CurrencyId"]);
                currency.Title = Connection.ToString(dt.Rows[0]["Title"]);
                currency.Symbol = Connection.ToString(dt.Rows[0]["Symbol"]);
                currency.ConversionRate = Connection.ToDecimal(dt.Rows[0]["ConversionRate"]);
                currency.ConversionPercentage = Connection.ToDecimal(dt.Rows[0]["ConversionPercentage"]);
                currency.LastUpdate = Connection.ToDate(dt.Rows[0]["LastUpdate"]);
                currency.IsDefault = Connection.ToBoolean(dt.Rows[0]["IsDefault"]);
                currency.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                currency.Exchangecode = Connection.ToString(dt.Rows[0]["ExchgCode"]);
            }
            return currency;
        }
    }
}
