using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class TranRestoreModel
    {
        public string Message { get; set; }
        public bool ShowMsg { get; set; }
        public bool Failed { get; set; }
        public long BookingId { get; set; }
        public string TransactionId { get; set; }
        public string PaymentId { get; set; }
        public int PaymentCode { get; set; }
        public string Notes { get; set; }
        public double Amount { get; set; }
    }
}