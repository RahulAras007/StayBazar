using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class InvoiceNumberData
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }

    }
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public long OfflineBookingId { get; set; }
        public string ToAddress { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime InvoiceDate { get; set; }
        public double Reimbursements { get; set; }
        public double Discounts { get; set; }
        public double Others { get; set; }
        public DateTime MailedDate { get; set; }
        public int InvoiceType { get; set; }
        public int Status { get; set; }
        public string HtmlSection1 { get; set; }
        public string HtmlSection2 { get; set; }
        public string HtmlSection3 { get; set; }
        public string CheckStatus { get; set; }
        public string HtmlSection4 { get; set; }
        public int IsGst { get; set; }
        public long BookingDetailsId { get; set; }
        public long BookingId { get; set; }
    }
}
