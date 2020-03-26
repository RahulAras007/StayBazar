using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
   public class MargintrackingReport
    {
       public long Offline_BookingId { get; set; }
        public long BookingId { get; set; }
        public long TotalRows { get; set; }
        public long SlNo { get; set; }
        public DateTime BookingDate { get; set; }
        public string PropertyName { get; set; }
        public string salepersonaname { get; set; }
        public string City { get; set; }
        public string PropertyCity { get; set; }
        public long NumberofNights { get; set; }
        public long NumberofRoom { get; set; }
        public string SupplierName { get; set; }
        //    public string State { get; set; }
        //     public string AccomodationType { get; set; }
        //     public long NumberofAccomodation { get; set; }
        //     public long PeriodofStay { get; set; }
        //     public long NumberofNights { get; set; }
        //      public string CustomeType { get; set; }
        public string orderno { get; set; }
        public string CustomerName { get; set; }
        public decimal CustomerBillvalue { get; set; }
        public decimal SupplierRate { get; set; }
//        public string SupplierName { get; set; }
  //      public long LuxuryTax { get; set; }
 //       public long ServiceTax { get; set; }
//        public long TotalSupplierValue { get; set; }
  //      public long AmountPaid { get; set; }
  //      public string Modeofpayment { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
    //    public string TaxType { get; set; }
  //      public decimal TaxRate { get; set; }
  //      public long TotalSupplierBuyCost { get; set; }
 //       public long BookingRate { get; set; }
//        public long BookingValue { get; set; }
 //       public long TotalBookingValue { get; set; }
 //       public long GrossMargin { get; set; }
 //       public long AgentCommissionPayable { get; set; }
 //       public long NetMargin { get; set; }
  //      public double NetMarginPerc { get; set; }
 //       public int NoOfGuests { get; set; }
        public string GuestName { get; set; }
        public string InvoiceNumber { get; set; }

        public decimal CustomerValueBeforeGST { get; set; }
        public decimal SupplierValueBeforeGST { get; set; }

        public string BookingType { get; set; }
        public long BookingTypeid { get; set; }
        public string BillingEntity { get; set; }
        public decimal ManagementFee { get; set; }

        public decimal TacAmount { get; set; }

        public decimal SaleAmountDirect { get; set; }
        public decimal SaleAmountTac { get; set; }

        public long offlinestatus { get; set; }

        public decimal TAConlyAmount { get; set; }

        public decimal DirectOnlyAmount { get; set; }   
        public decimal ORCAmount { get; set; }
        public int TotalNights { get; set; }
        public string HotelConformationNo { get; set; }
        public string CustomerReferenceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}
