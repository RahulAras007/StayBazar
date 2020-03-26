using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class SupplierPaymetList
    {

        public static List<CLayer.SupplierPaymetList> getPaymentList(string pSearchString, int start, int limit, int pSearchType = 0)
        {
            DataLayer.SupplierPaymetList dt = new DataLayer.SupplierPaymetList();
            return dt.getPaymentList(pSearchString, start, limit ,pSearchType);
        }

        public static List<CLayer.SupplierPaymetList> getDetails(long OrderNo)
        {
            DataLayer.SupplierPaymetList dt = new DataLayer.SupplierPaymetList();
            return dt.getDetails(OrderNo);
        }
        public static void DeleteSupPayment(long supplierPaymentId)
        {
            DataLayer.SupplierPaymetList user = new DataLayer.SupplierPaymetList();
            user.SetDeleteStatus(supplierPaymentId);
        }
    }
}
