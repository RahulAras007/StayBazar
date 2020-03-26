using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
   public  class Offers
    {
       public static List<CLayer.Offers> GetForPropertyCalc(long propertyId, DateTime checkIn, DateTime checkOut)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetForPropertyCalc(propertyId, (int)CLayer.ObjectStatus.StatusType.Active, checkIn, checkIn);
       }
        public static List<CLayer.Offers> GetForAccommodationCalc(long accId, DateTime checkIn,DateTime checkOut)
        {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetForAccommodationCalc(accId, (int) CLayer.ObjectStatus.StatusType.Active,checkIn,checkIn);   
       }
       public static List<CLayer.Offers> GetForProperty(long propertyId)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetForProperty(propertyId, (int) CLayer.ObjectStatus.StatusType.Active);   
       }

       public static List<CLayer.Offers> GetForAccommodation(long accommodationId)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetForAccommodation(accommodationId, (int)CLayer.ObjectStatus.StatusType.Active);
       }

       public static List<CLayer.Accommodation>PropertyListAccommodationListByOfferId(int Status, long OfferId, int SearchValue, int Start, int Limit)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.PropertyListAccommodationListByOfferId(Status, OfferId, SearchValue, Start, Limit);   
       }
       public static long SaveAccommodationProperty(CLayer.Offers data)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.SaveAccommodationProperty(data);         
       }
       public static List<CLayer.Accommodation> GetAccommodation(int Status, int StayCategoryId, string SearchString, int SearchValue, int Start, int Limit)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetAccommodation(Status, StayCategoryId, SearchString, SearchValue, Start, Limit);
       }
       public static List<CLayer.Accommodation> GetProperties(int Status, string SearchString, int SearchValue, int Start, int Limit)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetProperties(Status,  SearchString, SearchValue, Start, Limit);
       }     
       public static long Save(CLayer.Offers data)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.Save(data);
       }
       public static CLayer.Offers GetByOfferId(long OfferId,int Status)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetByOfferId(OfferId,Status);
       }
      //###Offer List Active or out of date
       public static List<CLayer.Offers> GetAllByStatus(CLayer.ObjectStatus.StatusType status, int Start, int Limit)
       {
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetAllBySatus(status,Start,Limit);
       }
       public static List<CLayer.Offers> GetAllByTab(int Status, string SearchString, int Tab, int Start, int Limit)
       {
           //GetAllExpiredandActive(int Status,int Tab,int Start,int Limit)
           DataLayer.Offers Offer = new DataLayer.Offers();
           return Offer.GetAllExpiredandActive(Status, SearchString,Tab, Start, Limit);

         }
       public static void Delete(long offerId)
       {
           DataLayer.Offers task = new DataLayer.Offers();
           task.DeleteOffer(offerId);
           return;
       }
       public static long DeleteProperty(long PropertyId)
       {
           DataLayer.Offers task = new DataLayer.Offers();
           long id = task.DeleteProperty(PropertyId);
           return (id);
       }
       public static long DeleteAccommodation(long AccommodationId)
       {
           DataLayer.Offers task = new DataLayer.Offers();
           long id= task.DeleteAccommodation(AccommodationId);
           return(id);
       }     
       public static int EditStatusChange(CLayer.Offers data)
       {
           DataLayer.Offers task = new DataLayer.Offers();
           return task.EditStatusChange(data);
       }


    }
}
