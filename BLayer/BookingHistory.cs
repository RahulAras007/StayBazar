using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer
{
   public class BookingHistory
    {
        //booking History
       public static List<CLayer.Booking> GetBookingHistoryForSupplier(int status, long userId, int days, int start, int limit, int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GetBookingHistoryForSupplier(status, userId, days, start, limit, Type);
        }
        //booking History
        public static List<CLayer.Booking> GetHistoryForUser(int status, long userId, int days,int start, int limit,int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GtBookingHistoryForUser(status, userId, days, start, limit, Type);
        }
        public static List<CLayer.Booking> GetBookingApprovalHistoryForUser(int status, long userId, int days, int start, int limit, int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GtBookingApprovalHistoryForUser(status, userId, days, start, limit, Type);
        }
        public static List<CLayer.Booking> GtBookingApprovalHistoryForCorporate(int status, long userId, int days, int Start, int Limit, int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GtBookingApprovalHistoryForCorporate(status, userId, days, Start, Limit, Type);
        }
        //Added by rahul for displaying the list of offlinebooking in Booking Approval for corporate on 06-02-2020
        public static List<CLayer.Booking> GtOfflineBookingApprovalHistoryForCorporate(int status, long userId, int days, int Start, int Limit, int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GtOfflineBookingApprovalHistoryForCorporate(status, userId, days, Start, Limit, Type);
        }
        
        public static List<CLayer.Booking> GetBookingApprovalHistoryDetails(int status, long userId, int days, int start, int limit, int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GtBookingApprovalHistoryDetails(status, userId, days, start, limit, Type);
        }
        public static List<CLayer.Booking> GetBookingApprovalHistoryDetailsForCorporateAdmin(int status, long userId, int days, int start, int limit, int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GtBookingApprovalHistoryDetailsForCoroporateAdmin(status, userId, days, start, limit, Type);
        }

        public static  List<CLayer.Booking> GtbookingApprovedOrRejectedBookings(int status, long userId, int days, int start, int limit, int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GtbookingApprovedOrRejectedBookings(status, userId, days, start, limit, Type);

        }
        public static List<CLayer.Booking> GtbookingCancelledOrFailedOrRejectedBookings(int status, long userId, int days, int start, int limit, int Type)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GtbookingCancelledOrFailedOrRejectedBookings(status, userId, days, start, limit, Type);
        }

        public static List<CLayer.Booking> GetCreditBookForUser(int status, long userId, int start, int limit)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GetCreditBookForUser(status, userId, start, limit);
        }
        public static List<CLayer.Booking> GetOtherBookForUser(int status, long userId, int start, int limit)
        {
            DataLayer.BookingHistory book = new DataLayer.BookingHistory();
            return book.GetOtherBookForUser(status, userId, start, limit);
        }
       
        //Booking Buyer address
        public static List<CLayer.Address> BookedByAddressSearch(long BookingId)
        {
            DataLayer.BookingHistory user = new DataLayer.BookingHistory();
            return user.SearchBookedByAddress(BookingId);
        }
       //Booking Buyer For address
        public static List<CLayer.Address> GetBookedForUser(long BookingId)
        {
            DataLayer.BookingHistory user = new DataLayer.BookingHistory();
            return user.GetBookedForUser(BookingId);
        }
       
    }
}
