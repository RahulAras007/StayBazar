using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Areas.Admin.Models
{
    public class ReviewsModel
    {
        public long PropertyId { get; set; }
        public long UserId { get; set; }
        public long BookingId { get; set; }
        public int Rating { get; set; }
        [Required(ErrorMessage = "Is required")]
        [StringLength(2000)]
        [Display(Name = "Feedback")]
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime ReviewDate { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Recommended { get; set; }
        //Display List
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalTax { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal bookingitemsTotalAmount { get; set; }
        public long bBookingItemId { get; set; }
        //Pagination
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public ReviewType ViewType { get; set; }

        public bool fillterNow { get; set; }
        public enum ReviewSearchValues { Reviewer = 1, Property = 2, Both = 3,All=4 }
        public enum ReviewType { Waiting = (int)CLayer.ObjectStatus.StatusType.NotVerified, Moderated = (int)CLayer.ObjectStatus.StatusType.Verified, Rejected = (int)CLayer.ObjectStatus.StatusType.Rejected }

        public List<CLayer.Booking> Bookinglist { get; set; }
        public List<CLayer.Property> PropertyList { get; set; }
        public List<CLayer.Review> ReviewList { get; set; }



        //new feedback 

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
        public ReviewsModel()
        {
            Bookinglist = new List<CLayer.Booking>();
            PropertyList = new List<CLayer.Property>();
            ReviewList = new List<CLayer.Review>();
        }
    }
}