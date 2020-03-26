using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class NumberGenerator : DataLink
    {
        public enum NumberType
        {
            Invoice = 1,
            Proforma = 2
        }



        public CLayer.InvoiceNumberData GetNumber(string guid, NumberType nType)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, (int) nType));
            param.Add(Connection.GetParameter("pGuidVal", DataPlug.DataType._Varchar, guid));
            DataTable dt = Connection.GetTable("GenerateNumber", param);

            if (dt.Rows.Count == 0) return null;
            CLayer.InvoiceNumberData data = new CLayer.InvoiceNumberData();
            data.InvoiceNumber = Connection.ToString(dt.Rows[0]["IncNumber"]);
            data.InvoiceDate = Connection.ToDate(dt.Rows[0]["InvDate"]);
            return data;
        }
    }
}
