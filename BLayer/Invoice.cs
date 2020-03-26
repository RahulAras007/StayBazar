using System;
using System.Collections.Generic;


namespace BLayer
{
    public class Invoice
    {
        public static CLayer.InvoiceNumberData GetOldInvoiceNumber(long stateId)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GetOldInvoiceNumber(stateId);
        }
        public static CLayer.InvoiceNumberData GetOldGDSInvoiceNumber(long stateId)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GetOldGDSInvoiceNumber(stateId);
        }
        public static List<CLayer.OfflineBooking> GetAllBooking(int status, string searchString, int searchItem, int start, int limit,out int totalRows)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GetAllBooking(status,searchString,searchItem,start,limit,out totalRows);
        }

        public static long Save(CLayer.Invoice data)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.Save(data);
        }
        public static long GDSSave(CLayer.Invoice data)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GDSSave(data);
        }
        public static CLayer.Invoice GetInvoiceByOfflineBooking(long offlineBookingId)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GetInvoiceByOfflineBooking(offlineBookingId);
        }
        public static CLayer.Invoice GetGDSInvoiceByBookingID(long BookingId)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GetGDSInvoiceByBookingID(BookingId);
        }
        public static long UpdateGDSInvoiceByBookingID(CLayer.Invoice data)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.UpdateGDSInvoiceByBookingID(data);
        }
        public static CLayer.Invoice GetProformaByOfflineBooking(long offlineBookingId)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GetProformaByOfflineBooking(offlineBookingId);
        }
        
        public static CLayer.Invoice GetInvoice(long invoiceId)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GetInvoice(invoiceId);
        }
        public static void SetMailedDate(long invoiceId,DateTime mailedDate)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            inv.SetMailedDate(invoiceId,mailedDate);
        }
        public static int GetStatus(long invoiceId)
        {
            DataLayer.Invoice inv = new DataLayer.Invoice();
            return inv.GetStatus(invoiceId);
        }

    }
}
