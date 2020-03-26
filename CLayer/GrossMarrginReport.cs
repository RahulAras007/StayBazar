using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class GrossMarrginReport
    {
        public long BookingId { get; set; }
        public long TotalRows { get; set; }
        public long SlNo { get; set; }
        public DateTime BookingDate { get; set; }
        public string PropertyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AccomodationType { get; set; }
        public long NumberofAccomodation { get; set; }
        public long PeriodofStay { get; set; }
        public long NumberofNights { get; set; }
        public string CustomeType { get; set; }
        public string orderno { get; set; }
        public string CustomerName { get; set; }
        public long SupplierRate { get; set; }
        public long LuxuryTax { get; set; }
        public long ServiceTax { get; set; }
        public long TotalSupplierValue { get; set; }
        public long AmountPaid { get; set; }
        public string Modeofpayment { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string TaxType { get; set; }
        public decimal TaxRate { get; set; }
        public long TotalSupplierBuyCost { get; set; }
        public long BookingRate { get; set; }
        public long BookingValue { get; set; }
        public long TotalBookingValue { get; set; }
        public long GrossMargin { get; set; }
        public long AgentCommissionPayable { get; set; }
        public long NetMargin { get; set; }
        public double NetMarginPerc { get; set; }
        public int NoOfGuests { get; set; }
    }
}
