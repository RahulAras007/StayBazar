using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class ReportForFailedTransactions
    {
        public long BookingId { get; set; }
        public long TotalRows { get; set; }
        public long SerNo{ get; set; }
        public long TotalbookingAmount { get; set; }
        public string SupplierName{ get; set; }
        public string  PropertyName{ get; set; }
        public string AccomodationType{ get; set; }
        public DateTime Checkindate{ get; set; }
        public DateTime CheckoutDate{ get; set; }
        public string Ratetype{ get; set; }
        public string CustomerName { get; set; }
        //public long TotalbookingAmount{ get; set; }
        public long StayBazarCommission{ get; set; }
        public int CurrentStatus{ get; set; }
        public DateTime BookingDateTime{ get; set; }
        public string Issue{ get; set; }
        public string ResolvedBy { get; set; }
        public DateTime ResolutionTime{ get; set; }
        public string Resolution{ get; set; }
        public bool AssignedtoCustomerService{ get; set; }
        public string CustomerServicePersonAssigned { get; set; }
        public long TotalbookingAmountcancelled { get; set; }
        public long NoofAdults { get; set;}
        public long NoofChildren { get; set; }
        public string Destination { get; set; }
        public string BookingCategory { get; set; }
        public long Noofaccomodations { get; set; }
        public string PropertyCity { get; set; }
        public string CustomerMobile { get; set; }

    }
}
