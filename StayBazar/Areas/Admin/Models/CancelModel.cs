using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class CancelModel
    {
        public bool IsAccommodationCancel { get; set; }
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public decimal CancellationCharge { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal RefundAmount { get; set; }
        public bool CanCancel { get; set; }
        public string Message { get; set; }
    }
}