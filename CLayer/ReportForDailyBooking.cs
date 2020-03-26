using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class ReportForDailyBooking
    {
        public long SerNo { get; set; }
        public string SupplierName { get; set; }
        public string PropertyName { get; set; }
        public string Destination { get; set; }
        public string AccomodationType { get; set; }
        public string BookingCategory { get; set; }
        public string RateType { get; set; }
        public DateTime Checkindate { get; set; }
        public DateTime CheckoutDate { get; set; }
        public long Noofaccomodations { get; set; }
        public int NoofAdults { get; set; }
        public int NoofChildren { get; set; }
        public long TotalbookingAmount { get; set; }
        public long StayBazarCommission { get; set; }
        public long TravelAgentCommission { get; set; }
        public int BookingStatus { get; set; }
        public long OriginalBookingAmout { get; set; }
        public long NewBookingAmount { get; set; }
        public long OriginalCommission { get; set; }
        public long NewCommission { get; set; }
    //    public string Ratetype { get; set; }
        public long TotalbookingAmountcancelled { get; set; }
        public long StayBazarCommissioncancelled { get; set; }
        public long Chargesforchange { get; set; }
        public int CurrentStatus { get; set; }
        public DateTime BookingTime { get; set; }
        public string Issue { get; set; }
        public DateTime ResolutionTime { get; set; }
        public double TotalRefund { get; set; }
        //Resolution
        public long ResolvedBy { get; set; }
        public int NoOfGuests { get; set; }
        public DateTime CurrentDate { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start { get; set; }
        public int Status { get; set; }
        public long PropertyId { get; set; }
    }
}
