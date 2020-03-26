using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class BookingStatusReport
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
        public long NumberofRooms { get; set; }
        public string CustomeType { get; set; }
        public string orderno { get; set; }
        public string CustomerName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime Maildate { get; set; }
        public decimal CustomerBillvalue { get; set; }
        public decimal SupplierRate { get; set; }
        public string SupplierName { get; set; }
        public string SupplierInvoiceNumber { get; set; }
        public DateTime SupplierInvoiceDate { get; set; }
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
        public string GuestName { get; set; }

        public string SalesRegion { get; set; }
        public string SalesPerson { get; set; }
        public int NoOfPaxAdult { get; set; }
        public int NoOfPaxChild { get; set; }
        public decimal AvgDailyRateBefreStaxForBuyPrice { get; set; }
        public decimal AvgDailyRateBefreStaxForSalePrice { get; set; }
        public decimal amount { get; set; }
        public decimal StaxForBuyPrice { get; set; }
        public decimal StaxForSalePrice { get; set; }
        public decimal TotalBuyPrice { get; set; }
        public decimal TotalSalePrice { get; set; }

        public decimal StaxForotherForSalePrice { get; set; }//
        public decimal StaxForotherBuyPrice { get; set; }//
        public decimal BuyPriceforotherservicesForBuyprice { get; set; }//
        public decimal BuyPriceforotherservicesForSalePrice { get; set; }//




        public string SBBillingEntity { get; set; }
        public string SBBookingEntity { get; set; }

        public string SACCode { get; set; }

        public string InputGSTType1 { get; set; }
        public decimal InputGSTAmount1 { get; set; }
        public string InputGSTType2 { get; set; }
        public decimal InputGSTAmount2 { get; set; }

        public string GSTType1 { get; set; }
        public decimal GSTAmount1 { get; set; }
        public string GSTType2 { get; set; }
        public decimal GSTAmount2 { get; set; }
        public decimal TotalGSTAmount { get; set; }

        public int BookingType { get; set; }

        public double bSGST { get; set; }
        public double bCGST { get; set; }
        public double bIGST { get; set; }

        public double obSGST { get; set; }
        public double obCGST { get; set; }
        public double obIGST { get; set; }

        public double SGST { get; set; }
        public double CGST { get; set; }
        public double IGST { get; set; }

        public double btSGST { get; set; }
        public double btCGST { get; set; }
        public double btIGST { get; set; }


        public double oSGST { get; set; }
        public double oCGST { get; set; }
        public double oIGST { get; set; }

        public double DirectAmount { get; set; }
        public double ORCAmount { get; set; }

        public double VBuyPriceBeforeTax { get; set; }
        public double VSalePriceBeforeTax { get; set; }
        public double VBuyPriceTotal { get; set; }
        public double vSalePriceTotal { get; set; }
        public double vBuyTax { get; set; }
        public double vSaleGST { get; set; }
        public int TotalNights { get; set; }

    }
}
