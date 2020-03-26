using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
   public class CreditBookingReport
    {
        public string BookingId { get; set; }
        public long TotalRows { get; set; }
        public long SlNo { get; set; }
        public DateTime BookingDate { get; set; }
        public string PropertyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string AccomodationType { get; set; }
        public string CustomeType { get; set; }
        public string CustomerName { get; set; }
        public string BookingRefNo { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalAmount { get; set; }
        public string CorporateName { get; set; }
        public int IsCorpbookingpaid { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
