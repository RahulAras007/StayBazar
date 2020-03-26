using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class GDSTransactionsLog
    {

        public static void GenerateGDSLog(CLayer.GDSTransactionsLog data)
        {
            DataLayer.GDSTransactionsLog b2b = new DataLayer.GDSTransactionsLog();
            b2b.GenerateGDSLog(data);
        }
        public static List<CLayer.GDSTransactionsLog> GetGDSTransactionLog()
        {
            DataLayer.GDSTransactionsLog b2b = new DataLayer.GDSTransactionsLog();
            return b2b.GetGDSTransactionLog();
        }
        public static long  ClearGDSTransactionCount()
        {
            DataLayer.GDSTransactionsLog b2b = new DataLayer.GDSTransactionsLog();
            return b2b.ClearGDSTransactionCount();
        }
    }
}
