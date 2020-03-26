using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
     public class OfflineTransaction
    {
        public long OfflinePaymentId { get; set; }
        public int PaymentType { get; set; } //ObjectStatus.PaymentMethod
        public int ResponseCode { get; set; }
        public bool IsFlagged { get; set; }
        public ObjectStatus.TransactionType TransactionType { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Notes { get; set; }
        public double Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public ObjectStatus.TransactionStatus Status { get; set; }
        public double RefundedAmount { get; set; }
        public double TotalAmount { get; set; }
        public decimal TryAmount { get; set; }
        public DateTime TryTime { get; set; }
        public double ServiceCharge { get; set; }
        public double CurrentServiceCharge { get; set; }
    }
}
