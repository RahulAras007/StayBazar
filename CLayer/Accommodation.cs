
using System;

namespace CLayer
{
    public class Accommodation : ICommunication
    {
        public long AccommodationId { get; set; }
        public int AccommodationTypeId { get; set; }
        public int StayCategoryId { get; set; }
        public int AccommodationCount { get; set; } //Number of accommodations
        public long PropertyId { get; set; }

        public string Description { get; set; }
        public string Location { get; set; } // Not used

        public int MaxNoOfPeople { get; set; }
        public int MaxNoOfChildren { get; set; }
        public int MinNoOfPeople { get; set; }
        public int BedRooms { get; set; }
        public decimal Area { get; set; }
        public long UpdatedById { get; set; }
        public int Status { get; set; }
        
        //for listing purposes
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public string PropertyTitle { get; set; }
        public string StayCategory { get; set; }
        public string AccommodationType { get; set; }
        public decimal Rate { get; set; }
        public decimal GuestRate { get; set; }
        public int TotalAccommodations { get; set; }
        public long TotalRows { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        //Properties List
        public long OfferId { get; set; } 
        public string Address{ get; set; }                                  
        public string Statename { get; set; }
        public string City { get; set; }
        public string Ownername { get; set; }

        public string RoomType { get; set; }
        public string RoomTypeCode { get; set; }

        public string RoomTypeDecription { get; set; }
        public string SourceofBusiness { get; set; }

        public string BookingCode { get; set; }
        public string RatePlanCode { get; set; }
        public string cancel { get; set; }

        public int RoomStayRPH { get; set; }
        public bool Availability { get; set; }

        public string MealPlan { get; set; }
        public string CommissionType { get; set; }
        public string CommissionPercent { get; set; }
       


        public void Validate()
        {
            //Description = Utils.CutString(Description, 500);
            Location = Utils.CutString(Location, 100);
        }
    }
}