using System;
using System.Collections.Generic;

namespace CLayer
{
    public class CancellationData
    {
        public decimal TotalCancellationCharge { get; set; }
        public decimal NewBookingRate { get; set; }
        public double ServiceCharge { get; set; }
        public bool NewBookingExist { get; set; }
        //for modification
        public decimal Refundable { get; set; }
        public int AdditionalDays { get; set; }
        public int CancelledDays { get; set; }
        public CancellationData()
        {
            AdditionalDays = 0;
            CancelledDays = 0;
        }
    }
}
