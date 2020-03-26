using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class ReportDailyPropertyBooking
    {
        public string PropertyName { get; set; }
        public string PropertyLocation { get; set; }
        public string PropertyCity { get; set; }
        public string AccomodationType { get; set; }
        public string BookingCategory { get; set; }
        public DateTime BookingDate { get; set; }
        public long NoofaccomodationsBooked { get; set; }
        public long NoofAccomodationsCancelled { get; set; }
        public long InventoryAllocatedtoStayBazar { get; set; }
        public long InventoryBalance { get; set; }
        public long TotalRows { get; set; }
        public long BookingId { get; set; }
        public long AccomodationId { get; set; }
        public string PropertyAddress { get; set; }
        public long SupplierId { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Pincode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public DateTime Checkindate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public string TotalbookingAmount { get; set; }
        public string RateType { get; set; }
        public string CustomerPhone { get; set; }
    }
}
