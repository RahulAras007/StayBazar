using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class PropertyDetailsReport
    {
        public string SupplierBusinessName { get; set; }
        public string LoginID { get; set; }
        public string SupplierEmailID { get; set; }
        public string SupplierContactName { get; set; }
        public string SupplierAddress { get; set; }
        public string  SupplierPhone { get; set; }
        public string SupplierMobile { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierState { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public Decimal B2CMarkupShortTerm { get; set; }
        public Decimal B2CMarkupLongTerm { get; set; }
        public Decimal B2BMarkupShortTerm { get; set; }
        public Decimal B2BMarkupLongTerm { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public DateTime JoinedDate { get; set; }

        public string Accommodation_Description { get; set; }
        public string AccommodationType { get; set; }
        public string Quantity { get; set; }
        public Decimal DailyRate { get; set; }
        public Decimal WeeklyRate { get; set; }

        public Decimal MonthlyRate { get; set; }
        public Decimal LongTermRate { get; set; }
        public Decimal GuestRate { get; set; }
        public long Bedrooms { get; set; }
        public string SupplierTotalAccommodations { get; set; }
        public long Accommodation_MaxPeople { get; set; }
        public long Adults { get; set; }
        public long Children { get; set; }


    }
}
