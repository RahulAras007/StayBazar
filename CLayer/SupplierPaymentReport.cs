using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
   public class SupplierPaymentReport
    {
        public string BookingId { get; set; }
        public long TotalRows { get; set; }
        public long SlNo{ get; set; }	
        public DateTime BookingDate{ get; set; }                                               
        public string PropertyName{ get; set; }  
        public string City{ get; set; }  
        public string State{ get; set; }  
        public string AccomodationType{ get; set; }  
        public long NumberofAccomodation{ get; set; }  
        public long  PeriodofStay{ get; set; }  
        public long NumberofNights{ get; set; }  
        public string CustomeType{ get; set; }  
        public string CustomerName{ get; set; }  
        public decimal SupplierRate{ get; set; }
        public decimal SupplierRatePayable { get; set; }  
        public decimal LuxuryTax{ get; set; }  
        public decimal  ServiceTax{ get; set; }  
        public decimal TotalSupplierValue{ get; set; }
        public decimal AmountPaid { get; set; }  
        public string Modeofpayment{ get; set; }
        public DateTime CheckIn{ get; set; }
        public DateTime CheckOut { get; set; }
        public string TaxType { get; set; }
        public decimal TaxRate { get; set; }
        public int NoOfGuests { get; set; }
    }
}
