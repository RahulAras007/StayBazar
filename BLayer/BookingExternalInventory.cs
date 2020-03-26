using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class BookingExternalInventory
    {
        public static long Save(CLayer.BookingExternalInventory data)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.Save(data);
        }

        public static long SaveBookingCancelResponse(CLayer.BookingExternalInventory data)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.SaveBookingCancelResponse(data);
        }

        public static CLayer.BookingExternalInventory GetAllDetailsByRoomIdandHotelId(string RoomId,string HotelId)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.GetAllDetailsByRoomIdandHotelId(RoomId, HotelId);
        }

        public static CLayer.BookingExternalInventory GetExternalBookingInvetoryCanceldetById(long bookinBookingExtInvReqId, string ReservattionId)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.GetExternalBookingInvetoryCanceldetById(bookinBookingExtInvReqId, ReservattionId);
        }
        public static List<CLayer.BookingExternalInventory> GetExternalInventoryReqDetailsByBookingId(long BookingId)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.GetExternalInventoryReqDetailsByBookingId(BookingId);
        }

        public static CLayer.BookingExternalInventory getAllDetailsfromExternalRequest(long BookingExtInvReqId)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.getAllDetailsfromExternalRequest(BookingExtInvReqId);
        }
        
        public static List<CLayer.BookingExternalInventory> GetExternalInventoryCancelDetailsByBookingId(long BookingId)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.GetExternalInventoryCancelDetailsByBookingId(BookingId);
        }
        public static long SaveBookingSubmitResponse(CLayer.BookingExternalInventory data)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.SaveBookingSubmitResponse(data);
        }


        public static int GetExternalInventoryReqByBookingId(long BookingId)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.GetExternalInventoryReqByBookingId(BookingId);
        }

        public static long GetPropertyIdByBookingId(long BookingId)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.GetPropertyIdByBookingId(BookingId);
        }
        //*this is for getting property inventry id of offlinebooking id 
        //*Done by rahul on 06-02-2020
        public static long GetPropertyIdByOfflineBookingId(long BookingId)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            return acc.GetPropertyIdByOfflineBookingId(BookingId);
        }
        //***Ends here***
        public static List<CLayer.BookingExternalInventory> GetAllForSearch(string searchString, int searchItem, int start, int limit, int Status)
        {
            DataLayer.BookingExternalInventory user = new DataLayer.BookingExternalInventory();
            return user.GetAllForSearch(searchString, searchItem, start, limit, Status);
        }
        public static void UpdateCancellationStatus(long BookingExtInvReqId, int CacelSts)
        {
            DataLayer.BookingExternalInventory acc = new DataLayer.BookingExternalInventory();
            acc.UpdateCancellationStatus(BookingExtInvReqId,CacelSts);
        }

    }
}
