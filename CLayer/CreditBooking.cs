using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class CreditBooking
    {
        public decimal BookingAmount { get; set; }
        public string CreditComment { get; set; }
        public long bookid { get; set; }
     
        public string Paymentdate { get; set; }
        public bool Paid { get; set; }
        public long UpdatedBy { get; set; }
    }
}
