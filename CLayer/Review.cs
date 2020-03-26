using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
   public class Review: ICommunication
    
    {
        public long PropertyId { get; set; }
        public long UserId { get; set; }
        public long BookingId { get; set; }
        public long BookingItemId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime ReviewDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Recommended { get; set; }       
        public int ViewType { get; set; }
       //listing
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal bookingitemsTotalAmount { get; set; }
       

        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public List<Review> ReviewList { get; set; }


        public string AccommodationCity { get; set; }

        public enum Feedbackratings { Excellent = 1, VeryGood = 2, Average = 3, Poor = 4, Terrible = 5 }
        public enum considernext { Yes = 1, No = 2 }

        public int Accessibility { get; set; }
        public int Easiness { get; set; }
        public int CleanlinessBed { get; set; }
        public int CleanlinessBath { get; set; }
        public int SleepQuality { get; set; }
        public int Staffbehave { get; set; }
        public int OverallExp { get; set; }
        public int Considerfornext { get; set; }


        public void Validate()
        {
            Description = Utils.CutString(Description, 2000);
           
        }

       public Review()
        {
            ReviewList = new List<CLayer.Review>();
        }
    }
}
