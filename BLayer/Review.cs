using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer
{
   public  class Review
    {
         public static long Save(CLayer.Review feedback)
           {
               DataLayer.Review task = new DataLayer.Review();
               feedback.Validate();
               return task.Save(feedback);
           }

         //public static List<CLayer.Property> GetProperty(long propertyId)
         //{
         //   //long propertyId=  feedback.PropertyId;
         //    DataLayer.Review property = new DataLayer.Review();
         //    return property.ProprtyGet(propertyId );
         //}
        public static long Chechreviewids(CLayer.Review feedback)
         {
             DataLayer.Review task = new DataLayer.Review();
             feedback.Validate();
             return task.Chechreviewids(feedback);
        }
        public static long Chechreviewids1(long p,long b)
        {
            DataLayer.Review task = new DataLayer.Review();
            return task.Chechreviewids1(p,b);
        }
         public static long Verified(CLayer.Review feedback)
         {
             DataLayer.Review task = new DataLayer.Review();
             feedback.Validate();
             return task.VerifiedUpdate(feedback);
         }
         public static void Delete(long propertyId, long UserId, long BookingId)
         {
             DataLayer.Review task = new DataLayer.Review();
             task.Delete(propertyId, UserId, BookingId);
             return;
         }
         public static CLayer.Review Get(long propertyId, long UserId, long BookingId)
         {
             DataLayer.Review rev = new DataLayer.Review();
             return rev.Get(propertyId, UserId, BookingId);
         }                     
         public static List<CLayer.Review> GetGetUserId(int Start, int Limit, long id, int Status)
           {
                DataLayer.Review ReviewG = new DataLayer.Review();
                return ReviewG.GetUserId(Start, Limit, id, Status);
            }
         public static List<CLayer.Review> ByProperty(long propertyId,int Status)
           {
               DataLayer.Review ReviewG = new DataLayer.Review();
               return ReviewG.GetpropertyId(propertyId, Status);
           }
         public static List<CLayer.Review> GetAll(int Start, int Limit)
         {
             DataLayer.Review ReviewG = new DataLayer.Review();
             return ReviewG.GetAll(Start, Limit);
         }
         public static List<CLayer.Review> AllNameByFilter(string SearchString, int SearchItem,int Status, int Start, int Limit)
         {
             DataLayer.Review ReviewG = new DataLayer.Review();
             return ReviewG.AllNameSearch(SearchString, SearchItem, Status,Start, Limit);
         }

         public static List<CLayer.Review> RecommendedByStatus(string SearchString, int Status,bool Recommended, int Start, int Limit)
         {
             DataLayer.Review ReviewG = new DataLayer.Review();
             return ReviewG.RecommendedView(SearchString, Status,Recommended ,Start, Limit);
         }

         public static List<string> GetReviewSendBookingList()
         {
             DataLayer.Review srv = new DataLayer.Review();
             return srv.GetReviewSendBookingList();
         }
    }
}


