using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class CreditBooking
    {
        public static void SaveCorBookingPayment(CLayer.CreditBooking data)
        {
            DataLayer.CreditBooking bi = new DataLayer.CreditBooking();
            bi.SaveCorBookingPayment(data);
        }

        public static void SetCreditBookingstatus(int status, long bookingId)
        {
            DataLayer.CreditBooking book = new DataLayer.CreditBooking();
            book.SetCreditBookingstatus(status, bookingId);
        }
        public static CLayer.ObjectStatus.Corpcreditbookstatus GetCorpCreditPaymentStatus(long bookingId)
        {
            DataLayer.CreditBooking bok = new DataLayer.CreditBooking();
            return (CLayer.ObjectStatus.Corpcreditbookstatus)bok.GetCorpCreditPaymentStatus(bookingId);
        }

        public static CLayer.CreditBooking GetAllCredBookDetailsbyBookid(long bookingId)
        {
            DataLayer.CreditBooking book = new DataLayer.CreditBooking();
            return book.GetAllCredBookDetailsbyBookid(bookingId);
        }

        public static long GetCountForBookings(long bookuserid,DateTime FDate,DateTime Tdate)
        {
            DataLayer.CreditBooking bok = new DataLayer.CreditBooking();
            return bok.GetCountForBookings(bookuserid,FDate, Tdate);
        }
        public static void SetCreditBookingPaid(bool Paid, long bookingId)
        {
            DataLayer.CreditBooking book = new DataLayer.CreditBooking();
            book.SetCreditBookingPaid(Paid, bookingId);
        }

    }
}
