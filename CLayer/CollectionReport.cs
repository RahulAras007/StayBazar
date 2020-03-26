using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
   public  class CollectionReport
    {
	

        public string BookingId { get; set; }
        public long TotalRows { get; set; }
        public long SlNo { get; set; }
        public DateTime BookingDate { get; set; }
        public string PropertyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AccomodationType { get; set; }
        public long NumberofAccomodation { get; set; }
        public long PeriodofStay { get; set; }
        public int NumberofNights { get; set; }
        public string CustomeType { get; set; }
        public string CustomerName { get; set; }
        public string BookingRefNo { get; set; }
        public long SupplierRate { get; set; }
        public long LuxuryTax { get; set; }
        public long ServiceTax { get; set; }
        public decimal TotalSupplierValue { get; set; }
        public long AmountPaid { get; set; }
        public string Modeofpayment { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string TaxType { get; set; }
        public long TaxRate { get; set; }
        public long TotalSupplierBuyCost { get; set; }
        public long BookingRate { get; set; }
        public long BookingValue { get; set; }
        public long TotalBookingValue { get; set; }
        public string CustomerPaymenMode{ get; set; }
        public double Paymentgatewaycommission  { get; set; }
        public long Paymentgatewaycharges{ get; set; }
        public long NetCreditReceivableinbankaccount { get; set; }
    }
}
