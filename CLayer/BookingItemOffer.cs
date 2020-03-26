using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class BookingItemOffer
    {
        public long BookingItem { get; set; }
        public long OfferId { get; set; }
        public string OfferTitle { get; set; }
        //for display
        public long AccommodationId { get; set; }
    }
}
