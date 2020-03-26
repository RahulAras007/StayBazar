using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class SupplierPaymetList
    {

        public long SupplierPaymentId { get; set; }

        public long SupplierId { get; set; }

        public string OfflineSupplierEmail { get; set; }

        public decimal SupplierPayment { get; set; }

        public DateTime dtPayment { get; set; }

        public decimal grossAmount { get; set; }

        public decimal tdsAmount { get; set; }

        public decimal netAmtPaid { get; set; }
        public double Buyprice { get; set; }
        public double AmountPaid { get; set; }
        public string modeofPayment { get; set; }
        public DateTime CheckIn_date { get; set; }
        public DateTime CheckOut_date { get; set; }
        public string bank { get; set; }

        public string SupplierName { get; set; }

        public long Prop_ID { get; set; }
        public long TotalRows { get; set; }

        public string Prop_Name { get; set; }

        public string OrderNo { get; set; }
        

    }
}
