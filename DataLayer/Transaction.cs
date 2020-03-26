using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Transaction : DataLink
    {
        // public CLayer.Transaction             
        public bool IsExist(string paymentId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPaymentId", DataPlug.DataType._Varchar, paymentId));
            object result = Connection.ExecuteQueryScalar("transaction_IsExist", param);
            return Connection.ToInteger(result) > 0;
        }

        public bool IsExistForPaypal(string transactionid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("ptransactionid", DataPlug.DataType._Varchar, transactionid));
            object result = Connection.ExecuteQueryScalar("transaction_IsExistForPaypal", param);
            return Connection.ToInteger(result) > 0;
        }


        public int getGatewaytypebybookid(long bookingid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pbookingid", DataPlug.DataType._BigInt, bookingid));
            object result = Connection.ExecuteQueryScalar("transaction_getGatewaytypebybookid", param);
            return Connection.ToInteger(result);
        }
        public void SavePayment(CLayer.Transaction data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPaymentId", DataPlug.DataType._Varchar, data.PaymentId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pTransactionId", DataPlug.DataType._Varchar, data.TransactionId));
            param.Add(Connection.GetParameter("pIsFlagged", DataPlug.DataType._Bool, data.IsFlagged));
            param.Add(Connection.GetParameter("pResponseCode", DataPlug.DataType._Varchar, data.ResponseCode));
            param.Add(Connection.GetParameter("pPaymentType", DataPlug.DataType._Varchar, data.PaymentType));
            param.Add(Connection.GetParameter("pTransactionType", DataPlug.DataType._Varchar, (int)CLayer.ObjectStatus.TransactionType.Payment));
            param.Add(Connection.GetParameter("pNotes", DataPlug.DataType._Varchar, data.Notes));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pDollerRate", DataPlug.DataType._Decimal, data.DollerRate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)data.Status));
            param.Add(Connection.GetParameter("pDateCreated", DataPlug.DataType._DateTime, data.DateCreated));
            Connection.ExecuteQuery("transaction_Save", param);

        }

        public void SaveCancelTrans(CLayer.Transaction data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPaymentId", DataPlug.DataType._Varchar, data.PaymentId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pTransactionId", DataPlug.DataType._Varchar, data.TransactionId));
            param.Add(Connection.GetParameter("pIsFlagged", DataPlug.DataType._Bool, data.IsFlagged));
            param.Add(Connection.GetParameter("pResponseCode", DataPlug.DataType._Varchar, data.ResponseCode));
            param.Add(Connection.GetParameter("pPaymentType", DataPlug.DataType._Varchar, data.PaymentType));
            param.Add(Connection.GetParameter("pTransactionType", DataPlug.DataType._Varchar, (int)data.TransactionType));
            param.Add(Connection.GetParameter("pNotes", DataPlug.DataType._Varchar, data.Notes));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pDollerRate", DataPlug.DataType._Decimal, data.DollerRate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)data.Status));
            param.Add(Connection.GetParameter("pDateCreated", DataPlug.DataType._DateTime, data.DateCreated));
            Connection.ExecuteQuery("transaction_Save", param);

        }

        public void SaveRefundAmount(string TransactionId, string Paymentid, decimal Refundamount)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTransactionId", DataPlug.DataType._Varchar, TransactionId));
            param.Add(Connection.GetParameter("pPaymentid", DataPlug.DataType._Varchar, Paymentid));
            param.Add(Connection.GetParameter("pRefundamount", DataPlug.DataType._Decimal, Refundamount));
            param.Add(Connection.GetParameter("pGatewaytype", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.Gateway.PayPal));
            Connection.ExecuteQuery("transaction_SaveRefundAmountforpaypal", param);

        }
        public void updategatewaytype(string TransactionId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTransactionId", DataPlug.DataType._Varchar, TransactionId));
            param.Add(Connection.GetParameter("pGatewaytype", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.Gateway.PayPal));
            Connection.ExecuteQuery("transaction_Updategatewaytype", param);

        }

        public void SavePartialPayment(CLayer.Transaction data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPaymentId", DataPlug.DataType._Varchar, data.PaymentId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pTransactionId", DataPlug.DataType._Varchar, data.TransactionId));
            param.Add(Connection.GetParameter("pIsFlagged", DataPlug.DataType._Bool, data.IsFlagged));
            param.Add(Connection.GetParameter("pResponseCode", DataPlug.DataType._Varchar, data.ResponseCode));
            param.Add(Connection.GetParameter("pPaymentType", DataPlug.DataType._Varchar, data.PaymentType));
            param.Add(Connection.GetParameter("pTransactionType", DataPlug.DataType._Varchar, (int)CLayer.ObjectStatus.TransactionType.Payment));
            param.Add(Connection.GetParameter("pNotes", DataPlug.DataType._Varchar, data.Notes));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)data.Status));
            param.Add(Connection.GetParameter("pDateCreated", DataPlug.DataType._DateTime, data.DateCreated));

            Connection.ExecuteQuery("booking_PartialPaymentSave", param);

        }

        public void UpdateAmountAndStatus(CLayer.Transaction data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPaymentId", DataPlug.DataType._Varchar, data.PaymentId));
            param.Add(Connection.GetParameter("pTryAmount", DataPlug.DataType._Decimal, data.TryAmount));
            param.Add(Connection.GetParameter("pTryTime", DataPlug.DataType._DateTime, data.TryTime));
            param.Add(Connection.GetParameter("pTotalAmount", DataPlug.DataType._Decimal, data.TotalAmount));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)data.Status));
            Connection.ExecuteQuery("transaction_MarkAsTrying", param);
        }
        public void SetNote(string transactionId, string paymentId, string note)
        {
            if (note != null && note.Length > 999) note = note.Substring(0, 998);
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTId", DataPlug.DataType._Varchar, transactionId));
            param.Add(Connection.GetParameter("pPayId", DataPlug.DataType._Varchar, paymentId));
            param.Add(Connection.GetParameter("pNote", DataPlug.DataType._Varchar, note));
            Connection.ExecuteQuery("transaction_SetNotes", param);
        }
        public List<CLayer.Booking> GetAllPartialPaymentForSearch(int status, string searchString, int searchItem, int start, int limit,long userid )
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            
            if (userid == 0)
            {
                param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, 0));
            }
            else
            { 
                param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userid));
            }
            DataSet ds = Connection.GetDataSet("PartialPaymenttransaction_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    // AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    //AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    //Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]),
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"]),
                });
            }
            return bookings;
        }


        public List<CLayer.Booking> GetAllCorporateCreditBookingsForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("CorporateCreditBookings_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    // AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    //AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    //Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    CorporateName = Connection.ToString(dr["Corpname"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),

                });
            }
            return bookings;
        }


        public List<CLayer.Booking> GetAllCorporateCreditBookingsRequestForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("CorporateCreditBookingsRequest_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    // AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    //AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    //Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    CorporateName = Connection.ToString(dr["Corpname"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),

                });
            }
            return bookings;
        }



        public List<CLayer.Booking> GetAllPartialPaymentCancelledForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("PartialPaymenttransactioncancelled_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    // AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    //AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    //Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]),
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"]),
                    RefundAmount = Connection.ToDecimal(dr["TotalRefund"])
                });
            }
            return bookings;
        }


        public void RefundAllTransactions(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pRefundedSts", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.TransactionStatus.Refunded));
            param.Add(Connection.GetParameter("pPaymentSts", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.TransactionStatus.Payment));
            param.Add(Connection.GetParameter("pPartRefundSts", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.TransactionStatus.PartialRefund));
            Connection.ExecuteQuery("transaction_RefundAll", param);
        }
        public List<CLayer.Transaction> GetAllRefundable(long bookingId)
        {
            List<CLayer.Transaction> result = new List<CLayer.Transaction>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            DataTable dt = Connection.GetTable("transaction_GetAllRefundable", param);
            CLayer.Transaction temp;
            int respcode;
            CLayer.ObjectStatus.TransactionType tranType;
            CLayer.ObjectStatus.TransactionStatus tranStatus;

            foreach (DataRow dr in dt.Rows)
            {
                respcode = Connection.ToInteger(dr["ResponseCode"]);
                tranType = (CLayer.ObjectStatus.TransactionType)Connection.ToInteger(dr["TransactionType"]);
                tranStatus = (CLayer.ObjectStatus.TransactionStatus)Connection.ToInteger(dr["Status"]);
                if (tranType == CLayer.ObjectStatus.TransactionType.Payment && (respcode == 0) && (tranStatus == CLayer.ObjectStatus.TransactionStatus.PartialRefund || tranStatus == CLayer.ObjectStatus.TransactionStatus.Payment))
                {
                    temp = new CLayer.Transaction();
                    temp.BookingId = bookingId;
                    temp.Amount = Connection.ToDouble(dr["Amount"]);
                    temp.TotalAmount = Connection.ToDouble(dr["TotalAmount"]);
                    temp.TransactionId = Connection.ToString(dr["TransactionId"]);
                    temp.DateCreated = Connection.ToDate(dr["DateCreated"]);
                    temp.IsFlagged = Connection.ToBoolean(dr["IsFlagged"]);
                    temp.Notes = Connection.ToString(dr["Notes"]);
                    temp.PaymentId = Connection.ToString(dr["PaymentId"]);
                    temp.PaymentType = Connection.ToInteger(dr["paycode"]);
                    temp.TryAmount = Connection.ToDecimal(dr["TryAmount"]);
                    temp.TransactionType = tranType;
                    temp.RefundedAmount = Connection.ToDouble(dr["RefundedAmount"]);
                    temp.Status = tranStatus;
                    temp.ResponseCode = respcode;
                    result.Add(temp);
                }

            }
            return result;
        }

        #region Funcitons for booking

        public List<CLayer.Booking> VerifiedByDate(int Status, long VerifiedBy, int Start, int Limit, DateTime StartDate, DateTime EndDate)
        {
            //Transaction List
            List<CLayer.Booking> result = new List<CLayer.Booking>();


            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pVerifiedBy", DataPlug.DataType._BigInt, VerifiedBy));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._DateTime, StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._DateTime, EndDate));
            DataSet ds = Connection.GetDataSet("transaction_VerifiedByDate", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                    //Verifyid = Connection.ToInteger(dr["VerifiedBy"]),
                    //verifydate = Connection.ToString(dr["VerifiedDate"])
                });
            }
            return result;
        }
        public List<CLayer.Booking> NotVerified(int Status, int days, int Start, int Limit)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("transactionGet_NotVerified", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    propertyPin = Connection.ToString(dr["propertyPin"]),
                    propertyEmail = Connection.ToString(dr["propertyEmail"]),
                    propertyCity = Connection.ToString(dr["propertyCity"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }
        public List<CLayer.Booking> VerifiedById(int Status, long VerifiedBy, int days, int Start, int Limit)
        {
            //error got
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pVerifiedBy", DataPlug.DataType._BigInt, VerifiedBy));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("transactionGet_VerifiedById", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["Title"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    //bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    //BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    //AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["Location"]),
                    // Quantity = Connection.ToInteger(dr["Quantity"]),
                    // BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    propertyPin = Connection.ToString(dr["propertyPin"]),
                    propertyEmail = Connection.ToString(dr["propertyEmail"]),
                    propertyCity = Connection.ToString(dr["propertyCity"])
                    //Verifyid = Connection.ToInteger(dr["VerifiedBy"]),
                    //verifydate = Connection.ToString(dr["VerifiedDate"])

                });
            }
            return result;
        }



        //Get Booking List or Details For Booking Members by UserId
        public List<CLayer.Booking> VerifiedByUserId(long UserId, int Start, int Limit)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("transactionGet_ByUserId", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }


        public int StatusChange(CLayer.Booking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.BookingStatus.Success));
            param.Add(Connection.GetParameter("pVerifiedBy", DataPlug.DataType._BigInt, data.ByUserId));
            param.Add(Connection.GetParameter("pVerifiedDate", DataPlug.DataType._DateTime, DateTime.Now));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, data.BookingId));
            object result = Connection.ExecuteQueryScalar("transaction_UpdateStatus", param);
            return Connection.ToInteger(result);
        }
        //Transactions fill and  search
        public List<CLayer.Booking> GetAllForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("transaction_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    InventoryAPIType =Connection.ToInteger(dr["InventoryAPIType"])
                    
                });
            }
            return bookings;
        }

        public List<CLayer.Booking> GetAllForSearchWithType(int status,int Type, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("transaction_GetSearchWithType", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    InventoryAPIType = Connection.ToInteger(dr["InventoryAPIType"])

                });
            }
            return bookings;
        }

        public List<CLayer.Booking> GetAllForSearchBookingofflineRequest(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("transaction_GetSearchBookingofflineRequest", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]),
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"]),
                    RefundAmount = Connection.ToDecimal(dr["TotalRefund"])
                });
            }
            return bookings;
        }
        public List<CLayer.Booking> GetAllForSearchBookingRequestWithType(int status,int Type, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int,Type));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("transaction_GetSearchBookingRequestWithType", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]),
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"]),
                    RefundAmount = Connection.ToDecimal(dr["TotalRefund"]),
                    InventoryAPIType = Connection.ToInteger(dr["InventoryAPIType"])
                });
            }
            return bookings;
        }

        public List<CLayer.Booking> GetAllForSearchBookingRequest(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("transaction_GetSearchBookingRequest", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]),
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"]),
                    RefundAmount = Connection.ToDecimal(dr["TotalRefund"]),
                    InventoryAPIType =Connection.ToInteger(dr["InventoryAPIType"])
                });
            }
            return bookings;
        }
        //Transactions fill and  search       
        public List<CLayer.Booking> FillForSearch(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("transaction_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return bookings;
        }
        //Transactions Booking Buyer address
        public List<CLayer.Address> SearchBookedByAddress(long pBookingId)
        {
            List<CLayer.Address> bookings = new List<CLayer.Address>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, pBookingId));
            DataTable dt = Connection.GetTable("transaction_SearchAddressBookedBy", param);
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.Address()
                {
                    Firstname = Connection.ToString(dr["BuyerFirstname"]),
                    Lastname = Connection.ToString(dr["BuyerLastName"]),
                    Email = Connection.ToString(dr["BuyerEmail"]),
                    City = Connection.ToString(dr["BuyerCity"]),
                    AddressText = Connection.ToString(dr["BuyerAddress"]),
                    Country = Connection.ToString(dr["BuyerCountry"]),
                    Phone = Connection.ToString(dr["BuyerPhone"]),
                    Mobile = Connection.ToString(dr["BuyerMobile"]),
                    StateName = Connection.ToString(dr["BuyerState"])
                });
            }
            return bookings;
        }
        //Transactions Booking For Address


        public List<CLayer.Booking> BookingPendingApprovalsForCorporate_GetOnUserSearch(long userid,string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userid));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, 0));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("bookingPendingApprovalsForCorporate_GetOnUserSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    propertyPin = Connection.ToString(dr["propertyPin"]),
                    propertyEmail = Connection.ToString(dr["propertyEmail"]),
                    propertyCity = Connection.ToString(dr["propertyCity"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]),
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"]),
                    approval_order = Connection.ToInteger(dr["approval_order"]),
                    approval_status = Connection.ToInteger(dr["approval_status"]),
                    booking_approval_id = Connection.ToInteger(dr["booking_approval_id"]),
                    Approver_id = Connection.ToInteger(dr["Approver_id"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"])

                });
            }
            return result;
        }
        public List<CLayer.Booking> BookingPendingApprovals_GetOnUserSearch(long userid, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userid));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, 0));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("bookingPendingApprovals_GetOnUserSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    propertyPin = Connection.ToString(dr["propertyPin"]),
                    propertyEmail = Connection.ToString(dr["propertyEmail"]),
                    propertyCity = Connection.ToString(dr["propertyCity"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]),
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"]),
                    approval_order = Connection.ToInteger(dr["approval_order"]),
                    approval_status = Connection.ToInteger(dr["approval_status"]),
                    booking_approval_id = Connection.ToInteger(dr["booking_approval_id"]),
                    Approver_id = Connection.ToInteger(dr["Approver_id"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"])

                });
            }
            return result;
        }


        public List<CLayer.Booking> GtBookingApprovalHistoryDetailsForCoroporateAdmin(long userid,string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userid));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, 0));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("bookingApprovalHistory_CorporateAdminSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    Notes = Connection.ToString(dr["Notes"]),
                    propertyLocation = Connection.ToString(dr["propertyLocation"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
                    FirstName = Connection.ToString(dr["sellerFirstname"]),
                    LastName = Connection.ToString(dr["sellerLastName"]),
                    Email = Connection.ToString(dr["sellerEmail"]),
                    City = Connection.ToString(dr["sellerCity"]),
                    Phone = Connection.ToString(dr["sellerPhone"]),
                    Address = Connection.ToString(dr["sellerAddress"]),
                    CountryName = Connection.ToString(dr["sellerCountry"]),
                    Mobile = Connection.ToString(dr["sellerMobile"]),
                    StateName = Connection.ToString(dr["sellerState"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    propertyPin = Connection.ToString(dr["propertyPin"]),
                    propertyEmail = Connection.ToString(dr["propertyEmail"]),
                    propertyCity = Connection.ToString(dr["propertyCity"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]),
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"]),
                    approval_order = Connection.ToInteger(dr["approval_order"]),
                    approval_status = Connection.ToInteger(dr["approval_status"]),
                    booking_approval_id = Connection.ToInteger(dr["booking_approval_id"]),
                    Approver_id = Connection.ToInteger(dr["Approver_id"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    ApproverName = Connection.ToString(dr["ApproverName"]),
                    RejectionNote = Connection.ToString(dr["RejectionNote"])


                });
            }
            return result;

        }
        #endregion

    }
}
