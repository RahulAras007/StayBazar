using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.IO;
using System.Reflection;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using System.Xml;
using System.Net;


namespace ExcelReport
{
    public class CleadGDSTransactionLog
    {
        public void ClearGDSLog()
        {
            try
            {
                #region cleargdstransactionlog
                long GDSTransCount = BLayer.GDSTransactionsLog.ClearGDSTransactionCount();

                #endregion
            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
            }
        }
    }
}
