using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace CLayer
{
    public class RatesAccommodationModel
    {
        public long AccommodationId { get; set; }
        //public long RRateId { get; set; }
        //public long CRateId { get; set; }
        public decimal RCDaily { get; set; }
        public decimal RCWeekly { get; set; }
        public decimal RCMonthly { get; set; }
        public decimal RCLong { get; set; }
        public decimal RCGuest { get; set; }
        public decimal CCDaily { get; set; }
        public decimal CCWeekly { get; set; }
        public decimal CCMonthly { get; set; }
        public decimal CCLong { get; set; }
        public decimal CCGuest { get; set; }
        public int StartDate { get; set; }
        public int EndDate { get; set; }     
    }
}
