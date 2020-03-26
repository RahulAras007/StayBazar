using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class SupplierInvoice
    {
        public long PropertyId { get; set; }
        public string Property_Name { get; set; }

        public long CustomPropertyId { get; set; }
        public long CustomSupplierId { get; set; }
        public string PropertyName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string ServiceTaxRegNumber { get; set; }
        public string PAN_Number { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal LuxuryTax { get; set; }
        public decimal ServiceTax { get; set; }
        public decimal TotalInvoiceValue { get; set; }
        public DateTime EntryDate { get; set; }

        public long SupplierId { get; set; }
        public string SupplierName { get; set; }

        public long SupplierInvoiceID { get; set; }

        public decimal txtTotalAdjustmentResult { get; set; }



        public string City { get; set; }

        public long SupplierInvoiceBooking_ID { get; set; }

        public string BookingRefNumber { get; set; }

        public string CheckedBookingRefNumber { get; set; }





        public DateTime CheckIn_date { get; set; }

        public DateTime CheckOut_date { get; set; }

        public DateTime BookinCreatedDT { get; set; }

        public long TotalRows { get; set; }

        public string PropertyEmailAddresss { get; set; }

        public string PropertyType { get; set; }
        public int TaxType { get; set; }

        public enum TaxTypes { ServiceTax = 1, GST = 2 }
        public bool IsSupInvoicedone { get; set; }
    }
}
