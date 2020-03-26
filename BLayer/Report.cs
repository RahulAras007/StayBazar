using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BLayer
{
    public class Report
    {
        public static List<CLayer.ReportDailyPropertyBooking> DailyPropertyBooking(long supplierId, string properties, DateTime fromDate, DateTime toDate)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.DailyPropertyBooking(supplierId, properties, fromDate, toDate);
        }
        public static List<CLayer.ReportDailyPropertyBooking> DailyPropertyBookingForEmail(long supplierId, long propertyId, DateTime fromDate, DateTime toDate)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.DailyPropertyBookingForEmail(supplierId, propertyId, fromDate, toDate);
        }
        public static List<CLayer.ReportDailyPropertyBooking> DailyPropertyBookingcartForEmail(DateTime fromDate)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.DailyPropertyBookingcartForEmail(fromDate);
        }


        public static List<CLayer.CorporateDiscounts> ReportCorporateDiscounts(long Supplierid)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.ReportCorporateDiscounts(Supplierid);
        }

        public static List<CLayer.ReportRoomInventory> ReportRoomInventoryAvailability(string SearchString, int SearchValue, DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.ReportRoomInventoryAvailability(SearchString, SearchValue, FromDate, ToDate, Start, Limit);
        }

        public static List<CLayer.OfflineBooking> GetORCReport(DateTime? fromDT = null, DateTime? toDT = null)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.GetORCReport(fromDT, toDT);
        }
        public static List<CLayer.ReportForSuppliersRegistration> ReportForSuppliersRegistration(int SearchValue, int UserType, int Status, DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.ReportSuppliersRegistration(SearchValue, UserType, Status, FromDate, ToDate, Start, Limit);
        }

        public static List<CLayer.ReportForDailyBooking> DailyBooking(DateTime CurrentDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.DailyBooking(CurrentDate, Start, Limit);
        }
        public static List<CLayer.ReportDailyInventoryAndBooking> DailyInventoryAndBooking(long supplierId, DateTime CurrentDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.DailyInventoryAndBooking(supplierId, CurrentDate, Start, Limit);
        }
        public static DataTable PropertyDetailsReport(string SearchString, int Searchvalue)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.PropertyDetailsReport(SearchString, Searchvalue);
        }

        public static DataSet PropertyDetailsReport_Pager(string SearchString, int Searchvalue, int Limit, int Start)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.PropertyDetailsReport_Pager(SearchString, Searchvalue, Limit, Start);
        }

        public static DataTable ReportCityWiseCount()
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.ReportCityWiseCount();
        }
        public static DataTable PropertyTaxReport()
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.PropertyTaxReport();
        }
        public static List<CLayer.ReportForFailedTransactions> FailedTransation(int Status1, int Status2, DateTime CurrentDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.FailedTransation(Status1, Status2, CurrentDate, Start, Limit);
        }
        public static List<CLayer.SupplierPaymentReport> SupplierPaymentReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.SupplierPaymentReport(FromDate, ToDate, Start, Limit);
        }

        public static List<CLayer.GrossMarrginReport> GrossMarrginReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.GrossMarrginReport(FromDate, ToDate, Start, Limit);
        }
        public static List<CLayer.MargintrackingReport> MargintrackingReport(DateTime FromDate, DateTime ToDate, int Start, int Limit,long LoginUserid)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.MargintrackingReport(FromDate, ToDate, Start, Limit, LoginUserid);
        }
        public static List<CLayer.PendingCustomerInvoiceReport> PendingCustomerInvoiceReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.PendingCustomerInvoiceReport(FromDate, ToDate, Start, Limit);
        }
        public static List<CLayer.BookingStatusReport> BookingStatusReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.BookingStatusReport(FromDate, ToDate, Start, Limit);
        }
        public static List<CLayer.CollectionReport> CollectionReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.CollectionReport(FromDate, ToDate, Start, Limit);
        }

        public static List<CLayer.CreditBookingReport> CorporateCreditBookingsReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.CorporateCreditBookingsReport(FromDate, ToDate, Start, Limit);
        }
        public static List<CLayer.BookingStatusReport> BookingStatusReportGST(DateTime FromDate, DateTime ToDate, int Start, int Limit,long LoginUserid)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.BookingStatusReportGST(FromDate, ToDate, Start, Limit, LoginUserid);
        }
        public static List<CLayer.GDSBookingStatusReport> GDSBookingStatusReportGST(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            DataLayer.Report task = new DataLayer.Report();
            return task.GDSBookingStatusReportGST(FromDate, ToDate, Start, Limit);
        }
    }
}
