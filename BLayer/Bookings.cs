using System;
using System.Collections.Generic;
using System.Text;

namespace BLayer
{
    public class Bookings
    {
        //for booking
        private const int MAX_LENGTH_BOOKING_REFNO = 20;
        private const int MAX_LENGTH_REFNO = 5;
        private const int BOOKING_ID_SEED = 1000;

        //postfix
        private const string REFNO_POSTFIX_CORP = "CB";
        private const string REFNO_POSTFIX_TRA = "TB";
        private const string REFNO_POSTFIX_REG = "RB";

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

        //public static void Merge(long mainBookingId,long copyBookingId )
        //{
        //    List<CLayer.BookingItem> sitems = BLayer.BookingItem.GetAllDetails(mainBookingId);
        //    List<CLayer.BookingItem> citems = BLayer.BookingItem.GetAllDetails(copyBookingId);

        //    DateTime cin, cout;
        //    cin = sitems[0].CheckIn;
        //    cout = sitems[0].CheckOut;



        //}

        public static void SetUpdatedDate(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            bok.SetUpdatedDate(bookingId);
        }

        public static void SetLastRefundAmt(long bookingId, decimal PreviewRefundAmt)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            bok.SetLastRefundAmt(bookingId, PreviewRefundAmt);
        }
        public static bool CanRestore(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.CanRestore(bookingId);
        }
        public static string GetCurrentApproverNameForMail(long BookingID)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetCurrentApproverNameForMail(BookingID);
        }
        //*Added by rahul on 06-02-2020
        //*this is for getting login user id of offlinebookings
        public static string GetCurrentApproverNameForMailOfOfflineBooking(long BookingID)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetCurrentApproverNameForMailOfOfflineBooking(BookingID);
        }
        //****Ends Here**
        public static void CopyGuestDetails(long oldBookingId, long newBookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            bok.CopyGuestDetails(oldBookingId, newBookingId);
        }
        public static void SetDateForAllItems(long bookingId, DateTime checkIn, DateTime checkOut)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            bok.SetDateForAllItems(bookingId, checkIn, checkOut);
        }
        public static long GetBookedByUserId(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetBookedByUserId(bookingId);
        }
        //Added by rahul for gettting user id of offlinebooking on 06-02-2020
        public static long GetOfflineBookedByUserId(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetOfflineBookedByUserId(bookingId);
        }

        public static string GetBookingordernoByBId(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetBookingordernoByBId(bookingId);
        }
        public static string GetGDSBookingControlNumber(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetGDSBookingControlNumber(bookingId);
        }
        public static string GetHotelID(long PropertyId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetHotelId(PropertyId);
        }
        public static CLayer.ObjectStatus.BookingStatus GetStatus(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return (CLayer.ObjectStatus.BookingStatus)bok.GetStatus(bookingId);
        }
        public static CLayer.ObjectStatus.PartialPaymentStatus GetPartialPaymentStatus(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return (CLayer.ObjectStatus.PartialPaymentStatus)bok.GetPartialPaymentStatus(bookingId);
        }
        public static long GetBookingId(string paymentToken)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetBookingId(paymentToken);
        }
        public static long GetPropertyId(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetPropertyId(bookingId);
        }

        //*Added by rahul on 03/03/20
        //*This is for getting property id of API offline bookings
        public static long GetAPIOfflineBookingPropertyId(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetAPIOfflineBookingPropertyId(bookingId);
        }
        //**


        //*Added by rahul on 06-02-2020 for getting offlinebooking property id
        public static long GetOfflineBookingPropertyId(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetOfflineBookingPropertyId(bookingId);
        }
        //****End***

        public static decimal GetTotalcreditbookamount(long BuserId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetTotalcreditbookamount(BuserId);
        }


        public static decimal GetTotalcreditbookamountforReport(long BuserId, DateTime FDate, DateTime Tdate)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetTotalcreditbookamountforReport(BuserId, FDate, Tdate);
        }
        public static string GetRefNoById(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetRefNoById(bookingId);
        }

        public static decimal GetFirstDayCharge(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.GetFirstDayCharge(bookingId);
        }

        public static decimal getPaypalCommAmt(long bookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            return bok.getPaypalCommAmt(bookingId);
        }
        public static long GetPaymentoption(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();

            return bi.GetPaymentoption(bookingId);

        }

        public static long Getgatewaytype(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.Getgatewaytype(bookingId);
        }
        public static void UpdateCharges(long bookingId, decimal cancellationCharge, decimal serviceCharge, bool addToExisting = true)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            bok.UpdateCharges(bookingId, cancellationCharge, serviceCharge, addToExisting);
        }
        public static void SetRefund(long bookingId, decimal refundAmount, bool addToExisting = true)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            bok.SetRefund(bookingId, refundAmount, addToExisting);
        }

        public static void MergeAndRemove(long mainBookingId, long copyBookingId)
        {
            DataLayer.Booking bok = new DataLayer.Booking();
            bok.MergeAndRemove(mainBookingId, copyBookingId);
        }
        public static decimal GetTotalCancellationCharge(CLayer.Property cancCharge, decimal totalAmount, decimal firstDayCharge, DateTime checkIn)
        {
            decimal charge = 0;

            if (checkIn.AddDays(-1 * (double)cancCharge.CancellationPeriod) <= DateTime.Now)
            {
                switch (cancCharge.CancellationMethod)
                {
                    case CLayer.ObjectStatus.CancellationType.FixedNight:
                        charge = (decimal)cancCharge.CancellationCharge * firstDayCharge;
                        break;
                    case CLayer.ObjectStatus.CancellationType.FixedPercent:
                        charge = totalAmount * (decimal)cancCharge.CancellationCharge / 100;
                        break;
                    case CLayer.ObjectStatus.CancellationType.VariableNights:
                        int days = (checkIn - DateTime.Today).Days;
                        if (days > 0 && days < cancCharge.CancellationPeriod)
                        {
                            charge = firstDayCharge * days;
                        }
                        break;
                }

            }

            return Math.Round(charge);
        }

        public static long GetIdFromRefNo(string refNumber)
        {
            int idx = refNumber.LastIndexOf("-");
            if (idx < 0) return 0;
            string ids = refNumber.Substring(idx + 1);
            return long.Parse(ids, System.Globalization.NumberStyles.HexNumber);
        }
        public static void UpdateTotals(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.UpdateTotals(bookingId);
        }
        public static void SetServiceCharge(long bookingId, double serviceCharge)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.SetServiceCharge(bookingId, serviceCharge);
        }
        public static CLayer.Booking GetDetails(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetDetails(bookingId);
        }
        //*This is for getting details of offlinebooking
        //*Done by rahul on 06-02-2020
        public static CLayer.Booking GetOfflineBookingDetails(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetOfflineBookingDetails(bookingId);
        }
        //*****Ends here***
        public static CLayer.Booking GetDetailsAmedus(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetDetailsAmedus(bookingId);
        }
        public static CLayer.Booking GetDetails(string bookingRefNo)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetDetails(bookingRefNo);
        }
        //todo added by Keji
        public static CLayer.Booking GetDetailsSMS(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetDetailsSMS(bookingId);
        }
        public static List<long> GetBookingsByOrder(string orderNumber)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookingsByOrder(orderNumber);
        }
        public static long GetBookingIdByOrder(string orderNumber)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookingIdByOrder(orderNumber);
        }
        public static CLayer.User GetSupplierDetails(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetSupplierDetails(bookingId);
        }
        //*Added by rahul
        //*This is for getting supplier details of offline bookings
        public static CLayer.User GetSupplierDetailsofOfflineBooking(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetSupplierDetailsofOfflineBooking(bookingId);
        }
        //**End here**
        public static CLayer.User GetSupplierDetailsAmadeus(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetSupplierDetailsAmadeus(bookingId);
        }
        /// <summary>
        /// Sets payment type and adds more time for lock-in-period
        /// </summary>
        /// <param name="payType"></param>
        /// <param name="bookingId"></param>
        public static void SetPayType(int payType, long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.SetPayType(payType, bookingId);
        }

        public static DateTime GetCheckinByBooking(long bookingId)
        {
            DataLayer.Booking obj = new DataLayer.Booking();
            return obj.GetCheckinByBooking(bookingId);
        }


        public static void SaveBookingRefundAmt(long BookingId, decimal TotalRefundAmt, int Type)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.SaveBookingRefundAmt(BookingId, TotalRefundAmt, Type);
        }

        public static void SetTryingGateway(long bookingId, CLayer.ObjectStatus.Gateway gatewayType)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.UpdateTryingGateway(bookingId, (int)gatewayType);
        }
        public static void SetPaymentToken(long bookingId, string payToken)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.UpdatePaymentToken(bookingId, payToken);
        }

        public static void SetStatus(int status, long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.SetStatus(status, bookingId);
        }

        public static void SetPartialPaymentStatus(int status, long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.SetPartialPaymentStatus(status, bookingId);
        }



        public static CLayer.Booking GetDataForPayment(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetDataForPayment(bookingId);
        }
        public static void ClearCart(long userId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.ClearCart(userId);
        }
        //*Added by rahul on 07/03/20 for clearing bookings in offlinebookings ClearOfflinebookingCart
        public static void ClearOfflinebookingCart(long userId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.ClearOfflinebookingCart(userId);
        }
        //*
        public static decimal GetTotal(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetTotal(bookingId);
        }
        public static void UpdateAmounts(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.UpdateAmounts(bookingId);
        }

        //*Added by rahul on 29/02/20
        //*This is for Updating Amounts in offlinebooking table
        public static void UpdateOfflinebookingAmounts(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.UpdateOfflinebookingAmounts(bookingId);
        }
        //**



        public static void SavePaypalComm(decimal paypalcomm, long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.SavePaypalComm(paypalcomm, bookingId);
        }

        public static void UpdateTotalAmtIncPayComm(decimal TotalAmtIncPayComm, long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.UpdateTotalAmtIncPayComm(TotalAmtIncPayComm, bookingId);
        }

        //*Added by Rahul on 29/02/20
        //*This is for Updating the Payment for Offline Booking
        public static void UpdateOfflineBookingTotalAmtIncPayComm(decimal TotalAmtIncPayComm, long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            book.UpdateOfflineBookingTotalAmtIncPayComm(TotalAmtIncPayComm, bookingId);
        }
        //**

        public static void SetPaymentRefNo(long bookingId, CLayer.Role.Roles userRole, string orderNo = "")
        {
            DataLayer.Booking book = new DataLayer.Booking();
            if (orderNo == "") orderNo = GetRefNo(bookingId, userRole);
            book.SetPaymentRefNo(bookingId, orderNo);
        }

        //*Added by rahul on 28/02/2020 for storing order number in offlinebookings
        public static void SetOfflinePaymentRefNo(long bookingId, CLayer.Role.Roles userRole, string orderNo = "")
        {
            DataLayer.Booking book = new DataLayer.Booking();
            if (orderNo == "") orderNo = GetRefNo(bookingId, userRole);
            book.SetOfflinePaymentRefNo(bookingId, orderNo);
        }
        //******
        public static long SaveInitialData(CLayer.Booking data)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.SaveInitialData(data);
        }


        //***Added by rahul on 28/02/20 for Saving Initial data for offline bookings 
        public static long SaveOfflineBookingInitialData(CLayer.Booking data)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.SaveOfflinebookingInitialData(data);
        }
        //**

        public static long SaveInitialDataForOfflinBeforConfirm(CLayer.Booking data)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.SaveInitialDataForOfflinBeforConfirm(data);
        }

        public static long GetCartIdAfterCleaning(long userId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetCartIdAfterCleaning(userId);
        }

        //*Added By rahul for clearing the cart of offline booking id on 06/03/2020
        public static long GetOfflinebookingCartIdAfterCleaning(long userId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetOfflinebookingCartIdAfterCleaning(userId);
        }
        //***

        public static long GetCartId(long userId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookingCartId(userId);
        }

        public static long GetBookingIdForPartialPayByUserId(long userId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookingIdForPartialPayByUserId(userId);
        }
        public static long GetCartIdForPartialPay(long Status, long userId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetCartIdForPartialPay(Status, userId);
        }


        public static long IsBookingRefNoExist(string RefNo)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.IsBookingRefNoExist(RefNo);
        }
        public static long GetBookId(long userId, long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookId(userId, bookingId);
        }


        public static decimal GetPaypalCommissionByBookId(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetPaypalCommissionByBookId(bookingId);
        }
        public static decimal GetRefundAmt(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetRefundAmt(bookingId);
        }

        public static decimal GetTotalRefundAmt(long bookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetTotalRefundAmt(bookingId);
        }
        public static List<CLayer.Booking> GetForSupplier(long supplierId, int forDays)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookingsForSupplier(supplierId, forDays);
        }
        ////booking History
        //public static List<CLayer.Booking> GetHistoryForUser(int status, long userId, int days)
        //{
        //    DataLayer.Booking book = new DataLayer.Booking();
        //    return book.GtBookingHistoryForUser(status,userId, days);
        //}

        //for booking notify
        public static List<CLayer.Booking> GetAllBookingNotify(int status, string searchString, int searchItem, int start, int limit)
        {
            DataLayer.Booking user = new DataLayer.Booking();
            return user.GetAllBookingNotify(status, searchString, searchItem, start, limit);
        }
        public static List<CLayer.BookingDetails> GetItemDetailsHistoryForUser(int status, long userId, int days)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GtBookingItemDetailsHistoryForUser(status, userId, days);
        }

        public static List<CLayer.Booking> GetRecentDataForAdmin(int days)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookingRecentDataForAdmin(days);
        }

        public static CLayer.Address GetBookedByUser(long pBookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookedByUser(pBookingId);
        }

        //*Added by rahul on 07-02-2020
        //*For Getting the detils of offline booking user
        public static CLayer.Address GetOfflineBookedByUser(long pBookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetOfflineBookedByUser(pBookingId);
        }

        //**End
        
        public static CLayer.Booking GetBookDetailsByBookingId(long pBookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookDetailsByBookingId(pBookingId);
        }

        public static List<CLayer.Address> GetBookedForUser(long pBookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            List<CLayer.Address> result = book.GetBookedForUser(pBookingId);
            if (result.Count == 0)
            {
                //CLayer.Address adr = 
                long userId = BLayer.Bookings.GetBookedByUserId(pBookingId);//*This is for booking
                // result.Add(adr);
            }
            return result;
        }

        //*Added by rahul on 03/03/20
        //*This for getting Address of user for API offline booking
        public static List<CLayer.Address> GetAPIOfflineBookingBookedForUser(long pBookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            List<CLayer.Address> result = book.GetAPIOfflineBookingBookedForUser(pBookingId);
            if (result.Count == 0)
            {
                //CLayer.Address adr = 
                long userId = BLayer.Bookings.GetOfflineBookedByUserId(pBookingId);//*This is for booking
                // result.Add(adr);
            }
            return result;
        }
        //****END



        //Added by rahul for offlinebooking for getting address of booking user
        public static List<CLayer.Address> GetOfflineBookedForUser(long pBookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            List<CLayer.Address> result = book.GetOfflineBookedForUser(pBookingId);
            if (result.Count == 0)
            {
                //CLayer.Address adr = 
                long userId = BLayer.Bookings.GetOfflineBookedByUserId(pBookingId);
                // result.Add(adr);
            }
            return result;
        }


        public static List<CLayer.Booking> GetPartialBookingDetails()
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetPartialBookingDetails();
        }

        public static CLayer.Booking GetPartialBookDetailsbyBookId(long BookId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetPartialBookDetailsbyBookId(BookId);
        }

        public static CLayer.Booking GetPartialBCancelDetailsbyBookId(long BookId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetPartialBCancelDetailsbyBookId(BookId);
        }
        public static List<CLayer.Address> GetBookingVerifiedAddress(long pBookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookingVerifiedAddress(pBookingId);
        }
        public static List<CLayer.Address> GetpropertyAddress(long propertyId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetpropertyAddress(propertyId);
        }

        public static CLayer.Booking GetBookedForBookingUserId(long BookingUserId)
        {
            DataLayer.Booking forview = new DataLayer.Booking();
            return forview.GetBookedForBookingUserId(BookingUserId);
        }
        public static List<CLayer.BookingItem> GetForbookingList(long UserId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetAllBookingForUserData(UserId);
        }
        public static List<CLayer.BookingItem> GetAllGuestForbookingList(long UserId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetAllGuestForbookingList(UserId);
        }
        public static long SaveBookingFor(CLayer.Booking data)
        {
            DataLayer.Booking bk = new DataLayer.Booking();
            return bk.SaveBookingFor(data);
        }
        public static void DeleteForUser(long BookingUserId, long userId, long bookingId)
        {
            DataLayer.Booking task = new DataLayer.Booking();
            task.DeleteForUser(BookingUserId, userId, bookingId);
        }
        public static void UpdateBooking(long ForBookingUserId, long bookingId)
        {
            DataLayer.Booking task = new DataLayer.Booking();
            task.UpdateBooking(ForBookingUserId, bookingId);
        }

        public static void UpdatePaymentOption(long PayOption, long bookingId)
        {
            DataLayer.Booking task = new DataLayer.Booking();
            task.UpdatePaymentOption(PayOption, bookingId);
        }
        public static CLayer.Booking GetDataForCreditBooking(long bookingId)
        {
            DataLayer.Booking bk = new DataLayer.Booking();
            return bk.GetDataForCreditBooking(bookingId);
        }
        public static void UpdateBookingforUser(long UserId, long bookingId)
        {
            DataLayer.Booking task = new DataLayer.Booking();
            task.UpdateBookingforUser(UserId, bookingId);
        }
        public static void UpdatePropertyInventoryType(long bookingId, int PropertyInventoryType)
        {
            DataLayer.Booking task = new DataLayer.Booking();
            task.UpdatePropertyInventoryType(bookingId, PropertyInventoryType);
        }

        public static void UpdateStatus(long bookingId, int Status)
        {
            DataLayer.Booking task = new DataLayer.Booking();
            task.UpdateStatus(bookingId, Status);
        }
        public static int GetBookingType(long BookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetBookingType(BookingId);

        }
        public static int GetGDSbookingIsIGST(long BookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetGDSbookingIsIGST(BookingId);
        }
        public static CLayer.Booking GetSBEntityAddressDetailsByBookingId(long BookingID, int StateID)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetSBEntityAddressDetailsByBookingId(BookingID, StateID);

        }
        public static CLayer.Booking GetAllGDSCustomerBillingaddress(long BookingId, int UserID)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetAllGDSCustomerBillingaddress(BookingId, UserID);
        }
        public static CLayer.Booking GetAllGDSCustomerDetails(long BookingId, int UserID)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetAllGDSCustomerDetails(BookingId, UserID);
        }
        public static CLayer.Booking GetAllGDSpropertyDetails(long BookingId)
        {
            DataLayer.Booking book = new DataLayer.Booking();
            return book.GetAllGDSpropertyDetails(BookingId);
        }
        public static List<CLayer.BookingApprovals> GetAllCorporateBookingApprovals(long corporateId)
        {
            DataLayer.Booking b2b = new DataLayer.Booking();
            return b2b.GetAllCorporateBookingApprovals(corporateId);
        }
        public static long BookingApprovalsSave(CLayer.BookingApprovals data)
        {
            DataLayer.Booking b2b = new DataLayer.Booking();
            return b2b.BookingApprovalsSave(data);
        }
        public static long GetApprovalStatus(long BookingId, long ApproverID = 0)
        {
            DataLayer.Booking b2b = new DataLayer.Booking();
            return b2b.GetApprovalStatus(BookingId, ApproverID);
        }
        public static bool GetBookingApprovalStatus(long Approverid, long BookingId)
        {
            DataLayer.Booking b2b = new DataLayer.Booking();
            return b2b.GetBookingApprovalStatus(Approverid, BookingId);
        }
        public static CLayer.Booking GetNextApproverDetails(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetNextApproverDetails(bookingId);
        }

        //Added by rahul on 06-02-2020 for getting next approver id for Offline Bookings
        public static CLayer.Booking GetNextApproverDetailsForOfflineBooking(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetNextApproverDetailsForOfflineBooking(bookingId);
        }


        public static long GetNextApproverID(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetNextApproverID(bookingId);
        }
        public static List<CLayer.Booking> GetPreviousApproverDetails(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetPreviousApproverDetails(bookingId);
        }
        public static CLayer.Booking GetBookingUserDetails(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetBookingUserDetails(bookingId);
        }
        //*Added by rahul on 07-02-2020
        //*This is for getting Offline Booked User Details
        public static CLayer.Booking GetOfflineBookingUserDetails(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetBookingUserDetails(bookingId);
        }
        //***END***


        public static string GetApproverNameByID(long ApproverID)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetApproverNameByID(ApproverID);
        }
        public static string GetCrediBookingApprover(long ApproverID)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetCrediBookingApprover(ApproverID);
        }
        public static string GetGDSBookingRejectionNote(long bookingId)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetGDSBookingRejectionNote(bookingId);
        }
        public static string GetGstState(string state)
        {
            DataLayer.Booking bi = new DataLayer.Booking();
            return bi.GetGstState(state);
        }
    }
}
