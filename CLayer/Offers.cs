using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Offers : ICommunication
    {
        public long OfferId { get; set; }
        public int NoOfDays { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public long AccommodationId { get; set; }
        public decimal Amount { get; set; }
        public int OfferFor { get; set; }
        public int RateType { get; set; }
        public decimal SBCommission { get; set; }
        public string OfferTitle { get; set; }
        public int FreeDays { get; set; }
        public long PropertyId { get; set; }
        public int OfferType { get; set; }
        public int StayCategoryId { get; set; }
//Display
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public long TotalRows { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Ids { get; set; }
       
        public void Validate()
        {
            OfferTitle = Utils.CutString(OfferTitle, 200);       
            
        }
    }
}
