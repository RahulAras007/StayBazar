using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class SupplierInvoice
    {

        public static long Save(CLayer.SupplierInvoice data)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            return acc.Save(data);
        }

        public static List<CLayer.SupplierInvoice> getSupplierInvoiceList(string searchText, int searchType, int Start, int Limit,int TaxType,int Status)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            return acc.getSupplierInvoiceList(searchText, searchType, Start, Limit, TaxType,   Status);
        }

        public static CLayer.SupplierInvoice getGetSupplierInvoicedetails(long ID)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            return acc.getGetSupplierInvoicedetails(ID);
        }

        public static void DeleteSupplierInvoice(long ID)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            acc.DeleteSupplierInvoice(ID);
        }

        public static decimal GetAdjustmentAmount(string bookIdList, decimal totalInvoiceValue)
        {
            
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            return acc.GetAdjustmentAmount(bookIdList, totalInvoiceValue);
        }

        public static List<CLayer.OfflineBooking> SupplierInvoiceBookingList(long PropID, long ID, string BookingRefIDs, string PropertyEmailAddresss, string PropertyType, int Start, int Limit, int TaxType, out int TotalRows_Booking, int searchTypeBooking = 0, string searchTextBooking = "")
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            return acc.SupplierInvoiceBookingList(PropID, ID, BookingRefIDs, PropertyEmailAddresss, PropertyType, searchTypeBooking, searchTextBooking, Start, Limit, TaxType, out TotalRows_Booking);
        }

        public static List<CLayer.OfflineBooking> SupplierInvoiceSavedBookingList(long PropID, long ID, string BookingRefIDs, string PropertyEmailAddresss, string PropertyType)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            return acc.SupplierInvoiceSavedBookingList(PropID, ID, BookingRefIDs, PropertyEmailAddresss, PropertyType);
        }

        public static List<CLayer.SupplierInvoice> getSupplierInvoiceList_Report(long Start, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            return acc.getSupplierInvoiceList_Report(Start, Limit, fromDT, toDT);
        }

        public static List<CLayer.OfflineBooking> getSupplierInvoicePendings_Report(long Start, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            return acc.getSupplierInvoicePendings_Report(Start, Limit, fromDT, toDT);
        }

        public static void saveSupplierInvoiceBooking(List<CLayer.OfflineBooking> data)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            acc.saveSupplierInvoiceBooking(data);
        }

        public static void deleteSupplierInvoiceSavedBookingListItem(string refNum, long supplierInvID)
        {
            DataLayer.SupplierInvoice acc = new DataLayer.SupplierInvoice();
            acc.deleteSupplierInvoiceSavedBookingListItem(refNum, supplierInvID);
        }

    }
}
