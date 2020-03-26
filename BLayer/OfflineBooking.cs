using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class OfflineBooking
    {
        //for booking
        private const int MAX_LENGTH_BOOKING_REFNO = 20;
        private const int MAX_LENGTH_REFNO = 5;
        private const int BOOKING_ID_SEED = 1000;

        //postfix
        private const string REFNO_POSTFIX_CORP = "COB";
        private const string REFNO_POSTFIX_TRA = "TOB";
        private const string REFNO_POSTFIX_REG = "ROB";

        public static string GetCategoryName(int bookingType)
        {
            if (bookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
                return "Direct";
            if (bookingType == (int)CLayer.ObjectStatus.OfflineBookingType.TAC)
                return "TAC";
            return "Regular";
        }

        public static bool HasSupplierPayment(string orderNo)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.HasSupplierPayment(orderNo);
        }

        public static string GetRefNo(long id, CLayer.Role.Roles userRole)
        {
            string ids = (id + BOOKING_ID_SEED).ToString("X");
            ids = ids.PadLeft(MAX_LENGTH_REFNO, '0');
            switch (userRole)
            {
                case CLayer.Role.Roles.Corporate:
                case CLayer.Role.Roles.CorporateUser:
                    ids = REFNO_POSTFIX_CORP + ids;
                    break;
                case CLayer.Role.Roles.Agent:
                    ids = REFNO_POSTFIX_TRA + ids;
                    break;
                default:
                    ids = REFNO_POSTFIX_REG + ids;
                    break;
            }
            return ids;
        }

        public static DataTable SupplierPaymentPendingReport(DateTime fromdate,DateTime todate)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.SupplierPaymentPendingReport(fromdate,todate);
        }
        public static bool CanSendInvoiceMail(string name, string email, int type)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.CanSendInvoiceMail(name, email, type);
        }

        public static CLayer.OfflineBooking SetOfflinePricingDetails(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.SetOfflinePricingDetails(data);
        }
        public static CLayer.OfflineBooking GetOtherAmountsForOfflineBooking(long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetOtherAmountsForOfflineBooking(OfflineBookingId);
        }

        public static void UpdateOtherAmountsForOfflineBooking(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.UpdateOtherAmountsForOfflineBooking(data);
        }
        public static void UpdateSupplierPaymentModeForOfflineBooking(List<CLayer.SupplierPaymentSchedule> data)
        {
            DataLayer.OfflineBooking supplierpayment = new DataLayer.OfflineBooking();
            supplierpayment.UpdateSupplierPaymentModeForOfflineBooking(data);
        }
        public static void DeleteSupplierPaymentModeForOfflineBooking(int offlineBookingId)
        {
            DataLayer.OfflineBooking supplierpayment = new DataLayer.OfflineBooking();
            supplierpayment.DeleteSupplierPaymentModeForOfflineBooking(offlineBookingId);
        }

        public static CLayer.OfflineBooking GetSupplierDetails(long id)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetSupplierDetails(id);
        }
        public static CLayer.OfflineBooking GetCustomerGstRegNoByStateId(long Customerid, long Stateid, int status)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetCustomerGstRegNoByStateId(Customerid, Stateid, status);
        }

        public static CLayer.User GetCustomerPaymentMode(long Customerid,int type)
        {
            DataLayer.OfflineBooking CustomerPaymentMode = new DataLayer.OfflineBooking();
            return CustomerPaymentMode.GetCustomerPaymentMode(Customerid,type);
        }
        public static CLayer.OfflineBooking GetOfflinegstDetailsById(long OffGSTId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetOfflinegstDetailsById(OffGSTId);
        }

        public static string GetPropertyGstRegNoByPropertyid(long Propertyid, int type)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetPropertyGstRegNoByPropertyid(Propertyid, type);
        }
        public static string SavePropertyGstRegNoByPropertyid(long Propertyid, int type, string PropertyGstRegNo)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.SavePropertyGstRegNoByPropertyid(Propertyid, type, PropertyGstRegNo);
        }

        public static long SaveOfflineBookingCustomerNEWForUser(CLayer.OfflineBooking userdetails)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveOfflineBookingCustomerNEWForUser(userdetails);
        }

        public static long SaveCustomerGSTState(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveCustomerGSTState(data);
        }
        public static long SaveOfflineBookingCustomerNEW(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveOfflineBookingCustomerNEW(data);
        }
        public static long SaveMasterOfflineBookingCustomer(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveMasterOfflineBookingCustomer(data);
        }
        public static long EditOfflineBookingCustomer(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.EditOfflineBookingCustomer(data);
        }
        public static long CreateNew(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.CreateNew(data);
        }

        public static void RemoveCustomerEntry(long offlineBookingId)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            user.RemoveCustomerEntry(offlineBookingId);
        }

        public static long SaveOfflineBookingCustomer(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveOfflineBookingCustomer(data);
        }

        public static long SaveUserAddressForOfflineBook(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveUserAddressForOfflineBook(data);
        }
        public static long SaveOfflineBookingDetails(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveOfflineBookingDetails(data);
        }
        public static long SaveOfflineBookingDetailsGST(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveOfflineBookingDetailsGST(data);
        }
        public static void SetPaymentRefNo(long OfflineBookId, CLayer.Role.Roles userRole, string orderNo = "")
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            if (orderNo == "") orderNo = GetRefNo(OfflineBookId, userRole);
            Offbook.SetPaymentRefNo(OfflineBookId, orderNo);

        }

        public static void UpdateSaveStatus(long OfflineBookId, int SaveStatus)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.UpdateSaveStatus(OfflineBookId, SaveStatus);
        }

        public static void UpdateDraftbookingStatus(long OfflineBookId, int SaveStatus)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.UpdateDraftbookingStatus(OfflineBookId, SaveStatus);
        }
        public static void UpdateNoinvoicemail(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.UpdateNoinvoicemail(data);

        }
        public static int GetNoInvoiceMailCount(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.GetNoInvoiceMailCount(data);

        }


        public static int GetOfflineSaveStatus(long OfflineBookId)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.GetOfflineSaveStatus(OfflineBookId);
        }
        public static decimal GetOfflineTotalAmountForBuyPrice(long OfflineBookId)
        {
            DataLayer.OfflineBooking OffTotalAmount = new DataLayer.OfflineBooking();
            return OffTotalAmount.GetOfflineTotalAmountForBuyPrice(OfflineBookId);
        }
        public static string ExistOrderno(long OfflineBookId)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.ExistOrderno(OfflineBookId);
        }
        public static string InvoiceMaildate(long OfflineBookId, int InvoiceTypeId)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.InvoiceMaildate(OfflineBookId, InvoiceTypeId);
        }


        public static void UpdateOfflineBookingCustomer(long OfflineBookId, long OfflineCustomerId)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.UpdateOfflineBookingCustomer(OfflineBookId, OfflineCustomerId);
        }
        public static void UpdateOfflineBookingGstForNewCustomer(long OfflineBookId, long OldOfflineCustomerId, long NewOfflineCustomerId, string Email)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.UpdateOfflineBookingGstForNewCustomer(OfflineBookId, OldOfflineCustomerId, NewOfflineCustomerId, Email);
        }

        public static void UpdateOfflineBookingCustomerNew(long OfflineBookId, long OfflineCustomerId)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.UpdateOfflineBookingCustomerNew(OfflineBookId, OfflineCustomerId);
        }

        public static int GetUserType(long offlineCustomerId)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            int usertype = user.GetUserType(offlineCustomerId);
            if (usertype == 0) usertype = (int)CLayer.Role.Roles.Customer;
            return usertype;
        }
        public static long SavePropertyDetails(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SavePropertyDetails(data);
        }

        public static CLayer.OfflineBooking getVendorDetailsbyVid(long pVendorId)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            return vend.vendorrofflinebooklistByVid(pVendorId);
        }
        public static List<CLayer.OfflineBooking> getVendorDetails(long pOfflineCustomerId)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            return vend.vendorrofflinebooklist(pOfflineCustomerId);
        }

        public static List<CLayer.TaxPercentage> GetAll_OfflineBookingTaxes(long OfflineBookId, string TaxType)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            return vend.GetAll_OfflineBookingTaxes(OfflineBookId, TaxType);
        }
        public static List<CLayer.TaxPercentage> GetAll_OfflineBookingTaxesGST(long OfflineBookId, string TaxType, string type, long bookingid)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            return vend.GetAll_OfflineBookingTaxesGST(OfflineBookId, TaxType, type, bookingid);
        }

        public static void save_OfflineTaxes(List<CLayer.TaxPercentage> data)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            vend.save_OfflineTaxes(data);
        }

        public static long SaveVendorDetails(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveVendorDetails(data);
        }
        public static long SaveCustomPropertyDetails(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveCustomPropertyDetails(data);
        }

        public static long SaveCustomPropertyotherDetails(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveCustomPropertyotherDetails(data);
        }
        public static CLayer.OfflineBooking GetAllDetailsById(long OfflineBookId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetAllDetailsById(OfflineBookId);
        }

        public static List<CLayer.OfflineBooking> GetAllOfflineDetails()
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetAllOfflineDetails();
        }

       


        public static CLayer.OfflineBooking GetOfflineboikingdetailsforBookDeleteRequest(long OfflineBookId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetOfflineboikingdetailsforBookDeleteRequest(OfflineBookId);
        }

        public static CLayer.OfflineBooking GetPayeeDetails(long PayeeID)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetPayeeDetails(PayeeID);
        }
        public static CLayer.OfflineBooking GetAllpropertyDetails(long OfflineBookId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetAllpropertyDetails(OfflineBookId);
        }
        public static CLayer.OfflineBooking GetAllpropertyDetailsForPaymentScedule(long OfflineBookId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetAllpropertyDetailsForPaymentScedule(OfflineBookId);
        }
       

        public static List<CLayer.OfflineBooking> GetAlCustomerList(string searchString, int start, int limit)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetAlCustomerList(searchString, start, limit);
        }
        public static CLayer.OfflineBooking GetOfflineBookingCustomerDetailsByID(long CustomPropertyId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetOfflineBookingCustomerDetailsByID(CustomPropertyId);
        }


        public static CLayer.OfflineBooking GetDetailsByCustomProperyId(long CustomPropertyId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetDetailsByCustomProperyId(CustomPropertyId);
        }

        public static CLayer.OfflineBooking GetAllCustomerDetails(long OfflineBookId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetAllCustomerDetails(OfflineBookId);
        }
        public static CLayer.OfflineBooking GetAllCustomerBillingaddress(long OfflineBookId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetAllCustomerBillingaddress(OfflineBookId);
        }

        public static List<CLayer.SupplierPaymentSchedule> GetAllSupplierPaymentSchedule(long OfflineBookId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetAllSupplierPayments(OfflineBookId);
        }

        public static List<CLayer.OfflineBooking> GetAllForSearch(string searchString, int searchItem, int start, int limit, int Status, long userId)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetAllForSearch(searchString, searchItem, start, limit, Status, userId);
        }

        public static List<CLayer.OfflineBooking> GetAllForSearch_Manage(string searchString, int searchItem, int start, int limit, int Status, long Userid)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetAllForSearch_Manage(searchString, searchItem, start, limit, Status, Userid);
        }
        public static List<CLayer.OfflineBooking> GetAllForSearch_customer(string searchString, int searchItem, int start, int limit, int Status, long Userid, DateTime From, DateTime To)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetAllForSearch_customer(searchString, searchItem, start, limit, Status, Userid, From, To);
        }
        public static List<CLayer.OfflineBooking> GetAllPropertyByCusPropId(string searchString, int searchItem, int start, int limit)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetAllPropertyByCusPropId(searchString, searchItem, start, limit);
        }

        public static List<CLayer.OfflineBooking> GetAllBookingsByCusPropId(long CustomPropertyId, int start, int limit)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetAllBookingsByCusPropId(CustomPropertyId, start, limit);
        }
        public static void DeleteCustomProperty(long CustomPropertyId)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.DeleteCustomProperty(CustomPropertyId);
        }
        public static void DeleteVendorDetails(long VendorpaymentsId)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.DeletevendorpaymentDetails(VendorpaymentsId);
        }
        public static void DeleteOfflineBooking(long OfflineBookingId)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.DeleteOfflineBooking(OfflineBookingId);
        }
        public static List<CLayer.OfflineBooking> GetAllcreatedUsers()
        {
            DataLayer.OfflineBooking createdusers = new DataLayer.OfflineBooking();
            return createdusers.GetAllcreatedUsers();
        }

        public static List<CLayer.OfflineBooking> GetSBEntity()
        {
            DataLayer.OfflineBooking createdusers = new DataLayer.OfflineBooking();
            return createdusers.GetSBEntity();
        }
        public static List<CLayer.OfflineBooking> GetProbSBEntity(int StateId)
        {
            DataLayer.OfflineBooking createdusers = new DataLayer.OfflineBooking();
            return createdusers.GetProbSBEntity(StateId);
        }
        public static List<CLayer.OfflineBooking> GetHSNCode()
        {
            DataLayer.OfflineBooking createdusers = new DataLayer.OfflineBooking();
            return createdusers.GetHSNCode();
        }
        //Done by rahul 22-11-19
        //public static List<CLayer.OfflineBooking> GetCostCentreCode()
        //{
        //    DataLayer.OfflineBooking costcentrecode = new DataLayer.OfflineBooking();
        //    return costcentrecode.GetCostCentreCode();
        //}
        public static List<CLayer.Invoice> GetDetailsNotSubmittedandGenerated()
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetDetailsNotSubmittedandGenerated();
        }
        public static List<CLayer.OfflineBooking> GetBookingDetailsAfterSubmitForCustomer()
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingDetailsAfterSubmitForCustomer();
        }


        public static CLayer.OfflineBooking UserDetailsByOffliceBookingId(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.UserDetailsByOffliceBookingId(data);
        }



        public static List<object> GetSupplier(string term)
        {
            DataLayer.OfflineBooking list = new DataLayer.OfflineBooking();
            return list.GetSupplier(term);
        }
        public static DataTable GSTInvoiceReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null,long LoginUserid=0)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetGSTInvoiceReport(SearchString, Limit, fromDT, toDT, LoginUserid);
        }

        public static List<CLayer.OfflineBooking> CustomerInvoiceReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.CustomerInvoiceReport(SearchString, Limit, fromDT, toDT);
        }

        public static List<CLayer.OfflineBooking> CustomerInvoiceGSTReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.CustomerInvoiceGSTReport(SearchString, Limit, fromDT, toDT);
        }
        public static List<CLayer.OfflineBooking> offlinePaymentReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.offlinePaymentReport(SearchString, Limit, fromDT, toDT);
        }
        public static List<CLayer.OfflineBooking> BankUploadReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.BankUploadReport(SearchString, Limit, fromDT, toDT);
        }
        public static DataTable  BankUploadReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null,bool IsExcelDownload=true)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.BankUploadReport(SearchString, Limit, fromDT, toDT,true);
        }
        //public static CLayer.OfflineBooking StatusUpdateOfflineBookingEdit(long id)
        //{
        //    DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
        //    return book.StatusUpdateOfflineBookingEdit(id);
        //}

        public static CLayer.OfflineBooking OfflineBookingAlreadyExistsChecking(string CustomerName, string GuestName, string PropertyName, string CheckIn, string CheckOut, long SalesPersonId, long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.OfflineBookingAlreadyExistsChecking(CustomerName, GuestName, PropertyName, CheckIn, CheckOut, SalesPersonId, OfflineBookingId);
        }
        public static void SaveGSTIn(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            user.SaveGSTIn(data);
        }
        public static List<CLayer.OfflineBooking> GetGSTList(int cust)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GSTList(cust);
        }
        public static void GSTDelete(int OFFGSTID)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            user.GSTDelete(OFFGSTID);
        }


        public static long SavePayee(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SavePayee(data);
        }

        public static string GetGSTStateCode(int stateid)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetGSTStateCode(stateid);
        }
        public static string StaybazarBookingEntity(int bookingid)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.StaybazarBookingEntity(bookingid);
        }
        public static string GetBookingCustomerStateID(int bookingid)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingCustomerStateID(bookingid);
        }

        //karthikms added on 4-11-2019 560-565
        public static string GetBookingPropertyStateID(int bookingid)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingPropertyStateID(bookingid);
        }

        //karthikms added on 2-11-2019
        public static string StaybazarGstSlab(int SlabCode, decimal Amt)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.StaybazarGstSlab(SlabCode,Amt);
        }
        //Done by Rahul
        public static long SaveCostCentre(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking savecodecentre = new DataLayer.OfflineBooking();
            return savecodecentre.SaveCostCentre(data);
        }
        public static long SaveEditCostCentre(int id, CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.SaveEditCostCentre(id,data);
        }
        public static void DeleteCostCentre(int Id)
        {
            DataLayer.OfflineBooking deletecostcentre = new DataLayer.OfflineBooking();
            deletecostcentre.DeleteCostCentre(Id);
        }
        public static List<CLayer.OfflineBooking> GetofflinebookingCostCentre(long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetofflinebookingCostCentre(OfflineBookingId);
        }
        public static List<CLayer.OfflineBooking> GetID_offlinebookingCostCentre(int ID)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetID_offlinebookingCostCentre(ID);
        }
        //---
        public static long SaveMultipleBooking(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.SaveMultipleBooking(data);
        }
        public static List<CLayer.OfflineBooking> BookedList(long id)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.BookedList(id);
        }
        public static CLayer.OfflineBooking EditBookedDetails(long id)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.EditBookedDetails(id);
        }

        public static List<object> GetAccommodationTypeName(string term)
        {
            DataLayer.OfflineBooking list = new DataLayer.OfflineBooking();
            return list.GetAccommodationTypeName(term);
        }
        public static List<object> GetVendor(string term)
        {
            DataLayer.OfflineBooking list = new DataLayer.OfflineBooking();
            return list.GetVendor(term);
        }
        public static CLayer.OfflineBooking GetVendorDetails(long id)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetVendorDetails(id);
        }
        public static CLayer.OfflineBooking GetVendorDetailsAutoComplete(long id)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetVendorDetailsAutoComplete(id);
        }


        public static void SaveVendotTax(List<CLayer.TaxPercentage> data)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            vend.SaveVendotTax(data);
        }
        public static List<CLayer.OfflineBooking> VendorList(long id)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.VendorList(id);
        }
        public static CLayer.OfflineBooking EditVendorDetails(long id)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.EditVendorDetails(id);
        }
        public static int GetOfflinebookingIsGST(long OfflineBookingId)
        {
            DataLayer.OfflineBooking Off = new DataLayer.OfflineBooking();
            int Isgst = Off.GetOfflinebookingIsGST(OfflineBookingId);
            return Isgst;
        }

        public static long GetBookingDetailHSN(long BookingDetailsId)
        {
            DataLayer.OfflineBooking Off = new DataLayer.OfflineBooking();
            long Isgst = Off.GetBookingDetailHSN(BookingDetailsId);
            return Isgst;
        }
        public static CLayer.OfflineBooking GetOfflinebookingMinDatesIsGST(long OfflineBookingId)
        {
            DataLayer.OfflineBooking Off = new DataLayer.OfflineBooking();
            return Off.GetOfflinebookingMinDatesIsGST(OfflineBookingId);
        }
        public static List<CLayer.OfflineBooking> GetMultipleBookingDetailsGST(long pOfflineCustomerId)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            return vend.GetMultipleBookingDetailsGST(pOfflineCustomerId);
        }
        public static List<CLayer.OfflineBooking> GetMultipleVendorDetailsGST(long pOfflineCustomerId)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            return vend.GetMultipleVendorDetailsGST(pOfflineCustomerId);
        }
        public static CLayer.OfflineBooking GetBookingDetailsGST(long BookingDetailsId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingDetailsGST(BookingDetailsId);
        }

        public static void save_OfflineTaxesMultiple(List<CLayer.TaxPercentage> data)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            vend.save_OfflineTaxesMultiple(data);
        }

        public static void DeleteMultipleTax(long Bookingid)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            vend.DeleteMultipleTax(Bookingid);
        }



        public static long GetBillingStateId(long id)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            return Offbook.GetBillingStateId(id);
        }



        public static CLayer.OfflineBooking GetSBEntityAddressDetailsByOffId(long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetSBEntityAddressDetailsByOffId(OfflineBookingId);
        }



        public static void SaveBookingTypeData(long bookingId, int bookingType, double gst, double tacAmount, double directAmount, bool gstIncluded, double percent, int hsnCodeId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.SaveBookingTypeData(bookingId, bookingType, gst, tacAmount, directAmount, gstIncluded, percent, hsnCodeId);
        }

        public static List<KeyValuePair<string, double>> GetDefaultBookingTypeTaxes()
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.DefaultBookingTypeTaxes();
        }

        public static void AddBookingTypeTaxes(long offlineBookingId, List<KeyValuePair<string, double>> taxes)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.AddBookingTypeTaxes(offlineBookingId, taxes);
        }
        public static List<KeyValuePair<string, double>> GetBookingTypeTaxes(long bookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingTypeTaxes(bookingId);
        }
        public static CLayer.OfflineBooking GetBookingTypeData(long offlineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingTypeData(offlineBookingId);
        }
        public static int GetBookingType(long offlineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingType(offlineBookingId);

        }
        public static int GetTaxType(long offlineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetTaxType(offlineBookingId);
        }

        public static int GetBillingEntityState(long offlineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBillingEntityState(offlineBookingId);
        }

        public static void FixAmounts(long offlineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.FixAmounts(offlineBookingId);
        }

        public static CLayer.HSNCode GetBookingTypeHSN(long offlineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingTypeHSN(offlineBookingId);
        }


        public static List<CLayer.OfflineBooking> GetOfflinebookingsAtCheckInOutNow()
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetOfflinebookingsAtCheckInOutNow();
        }
        public static int CheckOfflineCustomerExist(string CustomerName1, string CustomerEmail1, int CustomerType, long CustomerId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.CheckOfflineCustomerExist(CustomerName1, CustomerEmail1, CustomerType, CustomerId);
        }
        public static CLayer.OfflineBooking GetGSTAddressByState(long SubCustomerGstStateId, string CustomerGstRegNo, long CustomerId, int CustomerTableType)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetGSTAddressByState(SubCustomerGstStateId, CustomerGstRegNo, CustomerId, CustomerTableType);
        }
        public static bool CheckOfflineBookingDeleteorNot(long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.CheckOfflineBookingDeleteorNot(OfflineBookingId);
        }


        public static bool CheckOfflineSubBookingDeleteorNot(long OfflineBookingId, long BookedID)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.CheckOfflineSubBookingDeleteorNot(OfflineBookingId, BookedID);
        }
        public static List<CLayer.OfflineBooking> SearchforBookingFor(string name, int custid)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.SearchforBookingFor(name, custid);
        }

        public static List<CLayer.OfflineBooking> getIGSTdetails(long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.getIGSTdetails(OfflineBookingId);
        }


        public static void SaveBookingForToOfflinebooking_bookingfor(CLayer.OfflineBooking data, long OfflineBookingId)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            user.SaveBookingForToOfflinebooking_bookingfor(data, OfflineBookingId);
        }
        public static long GetBookingForID(long offlineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingForID(offlineBookingId);

        }
        public static CLayer.OfflineBooking GetBookingFor(long id)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetBookingFor(id);
        }
        public static long SaveOfflineBookingCustomerBookingFor(CLayer.OfflineBooking data)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.SaveOfflineBookingCustomerBookingFor(data);
        }

        public static long GetSupplierStateID(long offlineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetSupplierStateID(offlineBookingId);

        }
        public static string GetAllBillingEntityIdsCSV()
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            DataTable dt = book.GetAllBillingEntityStateID();
            string ids = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (ids != "")
                {
                    ids = ids + ",";
                }
                ids = ids + dr[0].ToString();
            }
            return ids;
        }
        public static void DeleteBookingDetails(long BookedID, long LoginUserid)
        {
            DataLayer.OfflineBooking Offbook = new DataLayer.OfflineBooking();
            Offbook.DeleteBookingDetails(BookedID, LoginUserid);
        }

        public static long GetInvoiceIDByOfflineBookingID(long pOfflineCustomerId)
        {
            DataLayer.OfflineBooking vend = new DataLayer.OfflineBooking();
            return vend.GetInvoiceIDByOfflineBookingID(pOfflineCustomerId);
        }
        public static  CLayer.OfflineBooking  GetAllOfflineDetailsByOfflinebookid(long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetAllOfflineDetailsByOfflinebookid(OfflineBookingId);
        }


        public static void UpdateSupplierPaymentmailsendData(long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.UpdateSupplierPaymentmailsendData(OfflineBookingId);
        }
        public static void UpdateGDSHotelConfirmNumber(long OfflineBookId, string GDSHotelConfirmNumber)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.UpdateGDSHotelConfirmNumber(OfflineBookId, GDSHotelConfirmNumber);
        }
        public DataTable GetCheckInDates(long bookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetCheckInDates(bookingId);
        }

        public List<CLayer.OfflineBooking> GetGuestDetails()
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetGuestDetails();
        }


        public DataTable GetPhoneNumber(long OfflineBookingId,string timer)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetPhoneNumber(OfflineBookingId, timer);
        }
        public static string  GetGSTRegNo(long OfflineBookingId)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            return book.GetGSTRegNo(OfflineBookingId);
        }
        public static void SetCusPaymentLinkStatus(string userids,string LoggedInUser,Guid PaymentGuid)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.SetCusPaymentLinkStatus(userids,"Y", LoggedInUser, PaymentGuid);
        }
        public static List<CLayer.OfflineBooking> GetAllForSelected_PaymentList(string searchString, int searchItem, int start, int limit, int Status)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();

            return user.GetAllForSelected_PaymentList(searchString, searchItem, start, limit, Status);
           

        }
        //public static List<CLayer.OfflineBooking> GetAllForPaymentList_Details(string searchString)
        //{
        //    DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
        //    return user.GetAllForPaymentList_Details(searchString);
        //}
        public static List<CLayer.OfflineBooking> GetAllForPaymentList_Details(string id)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetAllForPaymentList_Details(id);
        }
       
        public static void CustomerPaymentDetailsUpdate(string Emailid,string offline_bookingid)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.CustomerPaymentDetailsUpdate(Emailid,  offline_bookingid);
        }
        public static List<CLayer.OfflineBooking> GetAllForPaymentList_DetailsForMail(Guid PaymentLinkId)
        {
            DataLayer.OfflineBooking user = new DataLayer.OfflineBooking();
            return user.GetAllForPaymentList_DetailsForMail(PaymentLinkId);
        }
        public static void CustomerLinkAdvUpdate(long id, long AdvAmt)
        {
            DataLayer.OfflineBooking book = new DataLayer.OfflineBooking();
            book.CustomerLinkAdvUpdate(id, AdvAmt);
        }
    }
}
