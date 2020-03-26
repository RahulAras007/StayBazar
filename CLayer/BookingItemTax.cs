using System;


namespace CLayer
{
    public class BookingItemTax
    {
        public long BookingItemId { get; set; }
        public long TaxId { get; set; }
        public double Rate { get; set; }
        public bool OnGrandTotal { get; set; }
        public double Amount { get; set; }

        //for display
        public string Title { get; set; }
    }
}
