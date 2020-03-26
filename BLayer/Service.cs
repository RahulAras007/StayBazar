using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Service
    {
        public static int GetSupplierCountForMail(DateTime forDate)
        {
            DataLayer.Service srv = new DataLayer.Service();
            return srv.GetSupplierCountForMail(forDate);
        }

        public static List<string> GetSupplierIDsForMail(DateTime forDate,int start,int limit)
        {
            DataLayer.Service srv = new DataLayer.Service();
            return srv.GetSupplierIDsForMail(forDate,start,limit);
        }
        public static List<string> GetPartialPaymentList()
        {
            DataLayer.Service srv = new DataLayer.Service();
            return srv.GetPartialPaymentList();
        }

        public static List<string> GetPartialPaymentBCancellation()
        {
            DataLayer.Service srv = new DataLayer.Service();
            return srv.GetPartialPaymentBCancellation();
        }
    }
}
