using System;
using System.Collections.Generic;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Booking : DataLink
    {
        public void SetUpdatedDate(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            Connection.ExecuteQuery("booking_SetUpdateDate", param);
        }

        public void SetLastRefundAmt(long bookingId, decimal PreviewRefundAmt)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pPreviewRefundAmt", DataPlug.DataType._Decimal, PreviewRefundAmt));
            Connection.ExecuteQuery("booking_SetPreviewRefundAmt", param);
        }
        public bool CanRestore(long bookingId)
        {
            string sql = "SELECT COALESCE(SUM(property_checkDate(accommodationId,checkin,checkout,noofaccommodations)),1) AS availornot FROM bookingitems INNER JOIN booking WHERE bookingitems.bookingid=" + bookingId.ToString() + " and booking.status=" + ((int)CLayer.ObjectStatus.BookingStatus.CheckOut).ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return (Connection.ToInteger(obj) == 0);
        }
        public string GetGDSBookingControlNumber(long bookingId)
        {
            string sql = "SELECT bi.HotelConfirmNumber FROM booking b INNER JOIN bookingitems bi ON b.bookingid = bi.bookingid WHERE inventoryapitype = 2 and b.BookingID=" + bookingId + " LIMIT 1";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return (Connection.ToString(obj));
        }
        public string GetGDSBookingRejectionNote(long bookingId)
        {
            string sql = "	SELECT ifnull(RejectionNote,'') FROM booking_approvals ba WHERE ba.booking_id=" + bookingId + " AND approval_status=3 LIMIT 1";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return (Connection.ToString(obj));
        }
        public int GetBookingType(long BookingId)
        {
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar("SELECT PropertyInventoryType as BookingType FROM BOOKING WHERE bookingid=" + BookingId.ToString()));
        }
        public void CopyGuestDetails(long oldBookingId, long newBookingId)
        {
            string sql = "Update BookingItems o, BookingItems n Set n.ForUserId=o.ForUserId,n.ForBookingUser=o.ForBookingUser Where o.BookingId=" + oldBookingId.ToString() + " And n.BookingId=" + newBookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void MergeAndRemove(long mainBookingId, long copyBookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pMainBookingId", DataPlug.DataType._BigInt, mainBookingId));
            param.Add(Connection.GetParameter("pCopyBookingId", DataPlug.DataType._BigInt, copyBookingId));
            param.Add(Connection.GetParameter("pDeleteStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.Deleted));
            param.Add(Connection.GetParameter("pDisabled", DataPlug.DataType._Int, CLayer.ObjectStatus.StatusType.Disabled));
            Connection.ExecuteQuery("booking_MergeAndRemove", param);
        }
        public void SetDateForAllItems(long bookingId, DateTime checkIn, DateTime checkOut)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._DateTime, checkIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._DateTime, checkOut));
            Connection.ExecuteQuery("booking_SetDates", param);
        }
        public void SetServiceCharge(long bookingId, double serviceCharge)
        {
            string sql = "Update booking Set TotalServiceCharge=" + serviceCharge.ToString() + " Where BookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void SetRefund(long bookingId, decimal refundAmount, bool addToExisting = true)
        {
            string sql = "Update booking Set TotalRefund= ";
            if (addToExisting) sql = sql + "TotalRefund + ";
            sql = sql + refundAmount.ToString() + ",TotalAmount=TotalAmount-" + refundAmount.ToString() + " Where BookingId=" + bookingId.ToString(); // ",TotalAmount=TotalAmount-"+ refundAmount.ToString() +
            Connection.ExecuteSqlQuery(sql);
        }
        //update total cancellation and total service charge of a booking
        public void UpdateCharges(long bookingId, decimal cancellationCharge, decimal serviceCharge, bool addToExisting = true)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pServiceChg", DataPlug.DataType._Decimal, serviceCharge));
            param.Add(Connection.GetParameter("pCancChg", DataPlug.DataType._Decimal, cancellationCharge));
            param.Add(Connection.GetParameter("pOverwrite", DataPlug.DataType._Bool, addToExisting));
            Connection.ExecuteQuery("booking_SetSnCCharges", param);
        }
        public void UpdateTotals(long bookingId)
        {
            //booking_FixTotals
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            Connection.ExecuteQuery("booking_FixTotals", param);
        }
        public int GetStatus(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            object obj = Connection.ExecuteQueryScalar("booking_GetStatus", param);
            return Connection.ToInteger(obj);
        }

        public int GetPartialPaymentStatus(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            object obj = Connection.ExecuteQueryScalar("booking_GetPartialPaymentStatus", param);
            return Connection.ToInteger(obj);
        }
        public long GetPaymentoption(long bookingId)
        {
            string sql = "Select b.paymentoption From booking b  Where b.BookingId=" + bookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }

        public long Getgatewaytype(long bookingId)
        {
            string sql = "Select b.GatewayType From booking b  Where b.BookingId=" + bookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }

        public long GetBookedByUserId(long bookingId)
        {
            string sql = "Select ByUserId From booking Where BookingId=" + bookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        //Added by rahul for gettting user id of offlinebooking on 06-02-2020
        public long GetOfflineBookedByUserId(long bookingId)
        {
            string sql = "Select createdby From offline_bookings Where Offline_BookingId=" + bookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        //******Ends Here***
        public DateTime GetCheckinByBooking(long bookingId)
        {
            string sql = "SELECT checkin FROM booking b INNER JOIN bookingitems bi ON b.BookingId=" + bookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToDate(obj);
        }

        public string GetBookingordernoByBId(long bookingId)
        {
            string sql = "Select OrderNo From booking Where BookingId=" + bookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public int GetGDSbookingIsIGST(long BookingId)
        {
            string sql = "SELECT (CASE WHEN IGSTTitle IS NOT NULL THEN 1 ELSE 0 END) AS ISGST FROM Amadeustaxrates WHERE Bookingid=" + BookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }
        public string GetHotelId(long PropertyId)
        {
            string sql = "Select HotelId From Property Where PropertyId=" + PropertyId.ToString() + " and inventoryAPIType=" + (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus + "";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public decimal GetFirstDayCharge(long bookingId)
        {
            string sql = "SELECT SUM(FirstDayCharge) FROM  bookingitems WHERE Bookingid = " + bookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToDecimal(obj);
        }

        public decimal getPaypalCommAmt(long bookingId)
        {
            string sql = "SELECT Paypalcommission FROM  booking WHERE Bookingid = " + bookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToDecimal(obj);
        }
        public string GetRefNoById(long bookingId)
        {
            string sql = "Select OrderNo from booking where bookingId=" + bookingId.ToString();
            object val = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(val);
        }
        public long GetBookingId(string paymentToken)
        {
            string sql = "Select BookingId From booking Where PaymentToken='" + paymentToken + "'";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        public long GetPropertyId(long bookingId)
        {
            //
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            object obj = Connection.ExecuteQueryScalar("booking_GetPropertyId", param);
            return Connection.ToLong(obj);
        }
        //*Added by rahul on 03/03/20
        //*This is for getting property id of API offline bookings
        public long GetAPIOfflineBookingPropertyId(long bookingId)
        {
            //
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            object obj = Connection.ExecuteQueryScalar("booking_offlinebookingGetPropertyId", param);
            return Connection.ToLong(obj);
        }
        //***End


        //*Added by rahul on 06-02-2020 for getting offlinebooking property id
        public long GetOfflineBookingPropertyId(long bookingId)
        {
            //
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            object obj = Connection.ExecuteQueryScalar("Offlinebooking_GetPropertyId", param);
            return Connection.ToLong(obj);
        }

        //****End**

        public decimal GetTotalcreditbookamount(long BuserId)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBuserId", DataPlug.DataType._BigInt, BuserId));
            object obj = Connection.ExecuteQueryScalar("creditbooking_GetTotalcreditbookamount", param);
            return Connection.ToDecimal(obj);
        }

        public CLayer.Booking GetSBEntityAddressDetailsByBookingId(long BookingID, int StateID)
        {
            CLayer.Booking ids = new CLayer.Booking();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingID));
            param.Add(Connection.GetParameter("pSBBillingEntityID", DataPlug.DataType._BigInt, StateID));
            DataTable dt = Connection.GetTable("Booking_GetSBEntityAddressDetailsByBookingId", param);
            if (dt.Rows.Count > 0)
            {
                ids.SbEntityName = Connection.ToString(dt.Rows[0]["SbEntityName"]);
                ids.SbEntityAddress = Connection.ToString(dt.Rows[0]["SbEntityAddress"]);
                ids.SbEntityCountry = Connection.ToString(dt.Rows[0]["SbEntityCountry"]);
                ids.SbEntityState = Connection.ToString(dt.Rows[0]["SbEntityState"]);
                ids.SbEntityPhone = Connection.ToString(dt.Rows[0]["SbEntityPhone"]);
                ids.SbEntityGSTNo = Connection.ToString(dt.Rows[0]["SbEntityGSTNo"]);
            }

            return ids;

        }

        public decimal GetTotalcreditbookamountforReport(long BuserId, DateTime FDate, DateTime Tdate)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBuserId", DataPlug.DataType._BigInt, BuserId));
            param.Add(Connection.GetParameter("pFDate", DataPlug.DataType._DateTime, FDate.ToString("yyyy/MM/dd HH:mm:ss")));
            param.Add(Connection.GetParameter("pTdate", DataPlug.DataType._DateTime, Tdate.ToString("yyyy/MM/dd HH:mm:ss")));
            object obj = Connection.ExecuteQueryScalar("creditbooking_GetTotalcreditbookamountforReport", param);
            return Connection.ToDecimal(obj);
        }

        public CLayer.User GetSupplierDetails(long bookingId)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.Append("SELECT bb.Name,u.Firstname,u.Lastname,a.Phone,a.Mobile,u.Email FROM booking b INNER JOIN bookingitems bi ");
            sql.Append(" ON b.bookingId = bi.BookingId INNER JOIN accommodation ac ON bi.AccommodationId = ac.AccommodationId ");
            sql.Append(" INNER JOIN property p ON ac.PropertyId = p.PropertyId ");
            sql.Append(" INNER JOIN `user` u ON p.OwnerId = u.UserId ");
            sql.Append(" INNER JOIN b2b bb ON u.Userid = bb.b2bid ");
            sql.Append(" INNER JOIN address a ON u.UserId = a.UserId ");
            sql.Append(" Where b.BookingId=");
            sql.Append(bookingId);
            sql.Append(" GROUP BY a.addressId ");
            sql.Append("ORDER BY a.Type ");

            DataTable dt = Connection.GetSQLTable(sql.ToString());
            CLayer.User result = null;
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.User();
                result.Businessname = Connection.ToString(dt.Rows[0]["Name"]);
                result.FirstName = Connection.ToString(dt.Rows[0]["Firstname"]);
                result.LastName = Connection.ToString(dt.Rows[0]["Lastname"]);
                result.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                result.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.Email = Connection.ToString(dt.Rows[0]["Email"]);
            }

            return result;
        }

        //*Added by rahul
        //*This is for getting supplier details of offline bookings
        public CLayer.User GetSupplierDetailsofOfflineBooking(long bookingId)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.Append("SELECT bb.Name,u.Firstname,u.Lastname,a.Phone,a.Mobile,u.Email FROM offline_bookings ofb INNER JOIN bookingdetails bd ");
            sql.Append(" ON ofb.Offline_BookingId = bd.Offline_BookingId INNER JOIN accommodation ac ON bd.AccommodatoinTypeId = ac.AccommodationTypeId ");
            sql.Append(" INNER JOIN property p ON ac.PropertyId = p.PropertyId ");
            sql.Append(" INNER JOIN `user` u ON p.OwnerId = u.UserId ");
            sql.Append(" INNER JOIN b2b bb ON u.Userid = bb.b2bid ");
            sql.Append(" INNER JOIN address a ON u.UserId = a.UserId ");
            sql.Append(" Where ofb.Offline_BookingId=");
            sql.Append(bookingId);
            sql.Append(" GROUP BY a.addressId ");
            sql.Append("ORDER BY a.Type ");

            DataTable dt = Connection.GetSQLTable(sql.ToString());
            CLayer.User result = null;
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.User();
                result.Businessname = Connection.ToString(dt.Rows[0]["Name"]);
                result.FirstName = Connection.ToString(dt.Rows[0]["Firstname"]);
                result.LastName = Connection.ToString(dt.Rows[0]["Lastname"]);
                result.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                result.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.Email = Connection.ToString(dt.Rows[0]["Email"]);
            }

            return result;
        }

        //******Ends here***




        public CLayer.User GetSupplierDetailsAmadeus(long bookingId)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.Append("SELECT bb.Name,u.Firstname,u.Lastname,ifnull(a.Phone,'') as Phone,ifnull(a.Mobile,'') as Mobile,ifnull(u.Email,'') as Email FROM booking b INNER JOIN bookingitems bi ");
            sql.Append(" ON b.bookingId = bi.BookingId INNER JOIN accommodation ac ON bi.AccommodationId = ac.AccommodationId ");
            sql.Append(" INNER JOIN property p ON ac.PropertyId = p.PropertyId ");
            sql.Append(" INNER JOIN `user` u ON p.OwnerId = u.UserId ");
            sql.Append(" INNER JOIN b2b bb ON u.Userid = bb.b2bid ");
            sql.Append(" LEFT JOIN address a ON u.UserId = a.UserId ");
            sql.Append(" Where b.BookingId=");
            sql.Append(bookingId);
            sql.Append(" GROUP BY a.addressId ");
            sql.Append("ORDER BY a.Type ");

            DataTable dt = Connection.GetSQLTable(sql.ToString());
            CLayer.User result = null;
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.User();
                result.Businessname = Connection.ToString(dt.Rows[0]["Name"]);
                result.FirstName = Connection.ToString(dt.Rows[0]["Firstname"]);
                result.LastName = Connection.ToString(dt.Rows[0]["Lastname"]);
                result.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                result.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.Email = Connection.ToString(dt.Rows[0]["Email"]);
            }

            return result;
        }
        public long GetBookingIdByOrder(string orderNumber)
        {
            string sql = "Select BookingId from booking Where OrderNo='" + orderNumber + "'";
            object id = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(id);
        }

        public List<long> GetBookingsByOrder(string orderNumber)
        {
            List<long> ids = new List<long>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOrderNo", DataPlug.DataType._Varchar, orderNumber));
            param.Add(Connection.GetParameter("pDeleteStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.BookingStatus.Deleted));
            DataTable dt = Connection.GetTable("booking_GetAllByOrder", param);
            foreach (DataRow dr in dt.Rows)
            {
                ids.Add(Connection.ToLong(dr["BookingId"]));
            }
            return ids;
        }

        public void UpdateBooking(long ForBookingUserId, long bookingId)
        {

            string sql1 = "SELECT ForUserId FROM BookingItems Where BookingId=" + bookingId.ToString();
            long id = Connection.ExecuteSqlQuery(sql1);

            string sql = "UPDATE BookingItems  SET ForUserId=null, ForBookingUser='" + ForBookingUserId + "' Where BookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);

        }
        public void UpdatePaymentOption(long PayOption, long bookingId)
        {
            string sql = "UPDATE booking  SET PaymentOption='" + PayOption + "',IsCorpbookingpaid = 0  Where BookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);

        }

        public void UpdateTryingGateway(long bookingId, int gatewayType)
        {
            string sql = "UPDATE booking  SET GatewayType=" + gatewayType.ToString() + " Where BookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void UpdatePaymentToken(long bookingId, string payToken)
        {
            string sql = "UPDATE booking  SET PaymentToken='" + payToken + "' Where BookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void UpdateBookingforUser(long UserId, long bookingId)
        {

            string sql1 = "SELECT ForUserId FROM BookingItems Where BookingId=" + bookingId.ToString();
            long id = Connection.ExecuteSqlQuery(sql1);

            string sql = "UPDATE BookingItems  SET ForBookingUser=null,ForUserId='" + UserId + "' Where BookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);

        }
        public void UpdatePropertyInventoryType(long bookingId, long PropertyInventoryType)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pPropertyInventoryType", DataPlug.DataType._Int, PropertyInventoryType));
            Connection.ExecuteQuery("booking_updatePropertyInventoryType", param);

        }
        public void UpdateStatus(long bookingId, long status)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pstatus", DataPlug.DataType._Int, status));
            Connection.ExecuteQuery("booking_updatestatus", param);

        }
        public void SetPaymentRefNo(long bookingId, string refNumber)
        {
            string sql = "Update booking Set OrderNo='" + refNumber + "' Where BookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }


        //*Added by rahul on 28/02/2020 for storing order number in offlinebookings
        public void SetOfflinePaymentRefNo(long bookingId, string refNumber)
        {
            string sql = "Update offline_bookings Set OrderNo='" + refNumber + "' Where Offline_BookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }

        //*********
        public void SetPayType(int payType, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pPaymentType", DataPlug.DataType._Int, bookingId));
            Connection.ExecuteQuery("booking_SetReadyForPayment", param);
        }

        public void SaveBookingRefundAmt(long BookingId, decimal TotalRefundAmt, int Type)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingId));
            param.Add(Connection.GetParameter("pTotalRefundAmt", DataPlug.DataType._Decimal, TotalRefundAmt));
            param.Add(Connection.GetParameter("pBookingChangeType", DataPlug.DataType._Int, Type));
            Connection.ExecuteQuery("booking_SaveRefundAmount", param);
        }
        public void SetStatus(int status, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            Connection.ExecuteQuery("booking_SetStatus", param);
        }


        public void SetPartialPaymentStatus(int status, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            Connection.ExecuteQuery("booking_SetPartialPaymentStatus", param);
        }
        public CLayer.Booking GetDataForPayment(long bookingId)
        {
            CLayer.Booking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            DataTable dt = Connection.GetTable("booking_GetDetailsForPayment", param);
            foreach (DataRow dr in dt.Rows)
            {
                result = new CLayer.Booking();
                result.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                result.OrderNo = Connection.ToString(dr["OrderNo"]);
                result.PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]);
                result.PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"]);
            }
            return result;
        }
        public CLayer.Booking GetDataForCreditBooking(long bookingId)
        {
            string Sql = " SELECT b.BookingID,bi.CheckIn,bi.CheckOut,bi.NoofAccommodations,bi.NoofDays,bi.NoofAdults,bi.NoofChildren,bi.NoofGuests,ac.accommodationid,ac.PropertyID FROM booking b INNER JOIN bookingitems bi ON b.bookingid=bi.bookingid " +
                         " INNER JOIN accommodation ac ON bi.accommodationid = ac.accommodationid where  b.bookingid=" + bookingId + "";

            CLayer.Booking result = null;

            DataTable dt = Connection.GetSQLTable(Sql);
            foreach (DataRow dr in dt.Rows)
            {
                result = new CLayer.Booking();
                result.BookingId = Connection.ToLong(dr["BookingId"]);
                result.CheckIn = Connection.ToDate(dr["CheckIn"]);
                result.CheckOut = Connection.ToDate(dr["CheckOut"]);
                result.NoOfAccomodations = Connection.ToInteger(dr["NoofAccommodations"]);
                result.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                result.NoOfDays = Connection.ToInteger(dr["NoofDays"]);
                result.NoOfGuests = Connection.ToInteger(dr["NoofGuests"]);
                result.NoOfChildren = Connection.ToInteger(dr["NoofChildren"]);
                result.AccommodationId = Connection.ToLong(dr["accommodationid"]);
                result.PropertyId = Connection.ToLong(dr["PropertyID"]);

            }
            return result;
        }
        public void UpdateAmounts(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            Connection.ExecuteQueryScalar("Booking_UpdateAmounts", param);
        }

        //*Added by rahul on 29/02/20
        //*This is for Updating Amounts in offlinebooking table
        public void UpdateOfflinebookingAmounts(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            Connection.ExecuteQueryScalar("Booking_OfflineBookingUpdateAmounts", param);
        }

        //**

        public void SavePaypalComm(decimal paypalcomm, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("ppaypalcomm", DataPlug.DataType._Decimal, paypalcomm));
            Connection.ExecuteQueryScalar("Booking_SavePaypalComm", param);
        }

        public void UpdateTotalAmtIncPayComm(decimal TotalAmtIncPayComm, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pTotalAmtIncPayComm", DataPlug.DataType._Decimal, TotalAmtIncPayComm));
            Connection.ExecuteQueryScalar("Booking_UpdateTotalAmtIncPayComm", param);
        }

        //*Added by Rahul on 29/02/20
        //*This is for Updating the Payment for Offline Booking
        public void UpdateOfflineBookingTotalAmtIncPayComm(decimal TotalAmtIncPayComm, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pTotalAmtIncPayComm", DataPlug.DataType._Decimal, TotalAmtIncPayComm));
            Connection.ExecuteQueryScalar("Booking_OfflineBookingUpdateTotalAmtIncPayComm", param);
        }
        //***

        public long SaveInitialData(CLayer.Booking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pByUserId", DataPlug.DataType._BigInt, data.ByUserId));
            param.Add(Connection.GetParameter("pBookingDate", DataPlug.DataType._Date, data.BookingDate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.Cart));
            param.Add(Connection.GetParameter("pInventoryAPIType", DataPlug.DataType._Int, data.InventoryAPIType));
            Object obj = Connection.ExecuteQueryScalar("booking_SaveInitialData", param);
            return Connection.ToInteger(obj);
        }

        //*Added by rahul for saving initial data to offline bookings on 28/02/20
        public long SaveOfflinebookingInitialData(CLayer.Booking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pByUserId", DataPlug.DataType._BigInt, data.ByUserId));
            param.Add(Connection.GetParameter("pBookingDate", DataPlug.DataType._Date, data.BookingDate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.OfflineBookingCart));
            param.Add(Connection.GetParameter("pInventoryAPIType", DataPlug.DataType._Int, data.InventoryAPIType));
            Object obj = Connection.ExecuteQueryScalar("booking_SaveOfflineBookingInitialData", param);
            return Connection.ToInteger(obj);
        }

        public long SaveInitialDataForOfflinBeforConfirm(CLayer.Booking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pByUserId", DataPlug.DataType._BigInt, data.ByUserId));
            param.Add(Connection.GetParameter("pBookingDate", DataPlug.DataType._Date, data.BookingDate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.Offline));
            Object obj = Connection.ExecuteQueryScalar("booking_SaveInitialData", param);
            return Connection.ToInteger(obj);
        }
        public long GetCartIdAfterCleaning(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.Cart));
            Object obj = Connection.ExecuteQueryScalar("booking_IsExist", param);
            return Connection.ToInteger(obj);
        }

        //*Added by rahul for clearing the cart of offlinebookings on 06/03/2020
        public long GetOfflinebookingCartIdAfterCleaning(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.OfflineBookingCart));
            Object obj = Connection.ExecuteQueryScalar("booking_OfflinebookingsIsExist", param);
            return Connection.ToInteger(obj);
        }

        //***

        public long GetBookingCartId(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.Cart));
            Object obj = Connection.ExecuteQueryScalar("Booking_GetBookingCartId", param);
            return Connection.ToInteger(obj);
        }

        //*Added by rahul on 02/03/2020
        //*This is for getting OfflineBooking Id 
        public long GetBooking_OfflineBookingCartId(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.Cart));
            Object obj = Connection.ExecuteQueryScalar("Booking_OfflineBookingGetBookingCartId", param);
            return Connection.ToInteger(obj);
        }
        //****
        public long GetBookingIdForPartialPayByUserId(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            Object obj = Connection.ExecuteQueryScalar("Booking_GetBookingIdForPartialPayByUserId", param);
            return Connection.ToInteger(obj);
        }
        public long GetCartIdForPartialPay(long Status, long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            Object obj = Connection.ExecuteQueryScalar("Booking_GetBookingCartIdForPartialPay", param);
            return Connection.ToInteger(obj);
        }

        public long IsBookingRefNoExist(string RefNo)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRefNo", DataPlug.DataType._Varchar, RefNo));
            Object obj = Connection.ExecuteQueryScalar("Booking_IsBookingRefNoExist", param);
            return Connection.ToInteger(obj);
        }
        public long GetBookId(long userId, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            // param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess));
            Object obj = Connection.ExecuteQueryScalar("Booking_GetBookingIdForPayment", param);
            return Connection.ToInteger(obj);
        }


        public decimal GetPaypalCommissionByBookId(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            Object obj = Connection.ExecuteQueryScalar("Booking_GetPaypalCommissionByBookId", param);
            return Connection.ToDecimal(obj);
        }
        //For Supplier recent/Past
        public List<CLayer.Booking> GetBookingsForSupplier(long supplierId, int days)
        {

            List<CLayer.Booking> result = new List<CLayer.Booking>();

            CLayer.Booking temp;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, supplierId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            DataTable dt = Connection.GetTable("Supplier_GetReservations", param);
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Booking();
                temp.Title = Connection.ToString(dr["Title"]);
                temp.BookingId = Connection.ToLong(dr["BookingId"]);
                temp.CheckIn = Connection.ToDate(dr["CheckIn"]);
                temp.CheckOut = Connection.ToDate(dr["CheckOut"]);
                temp.BookingDate = Connection.ToDate(dr["BookingDate"]);
                temp.Address = Connection.ToString(dr["Address"]);
                temp.NoOfAccomodations = Connection.ToInteger(dr["NoOfAccommodations"]);
                temp.day = Connection.ToInteger(dr["NoOfDays"]);
                temp.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                temp.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                temp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                temp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                //temp.NoOfAccomodations = Connection.ToInteger(dr["NoOfAccomodations"]);
                temp.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                temp.Notes = Connection.ToString(dr["Notes"]);
                temp.OrderNo = Connection.ToString(dr["OrderNo"]);
                temp.Location = Connection.ToString(dr["Location"]);
                temp.BookingId = Connection.ToLong(dr["BookingId"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                result.Add(temp);
            }

            return result;

        }
        //For User History
        //public List<CLayer.Booking> GtBookingHistoryForUser(int status,long userId, int days)
        //{
        //    List<CLayer.Booking> result = new List<CLayer.Booking>();
        //    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
        //    param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
        //    param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
        //    param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));      
        //    DataSet ds = Connection.GetDataSet("bookinghistory_GetOnUser", param);
        //    foreach (DataRow dr in ds.Tables[1].Rows)
        //    {
        //        result.Add(new CLayer.Booking()
        //        {
        //            PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
        //            AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
        //            propertyAddress = Connection.ToString(dr["propertyAddress"]),
        //            BookingId = Connection.ToLong(dr["BookingId"]),
        //            BookingDate = Connection.ToDate(dr["BookingDate"]),
        //            bookingTotalAmount = Connection.ToDecimal(dr["bookingTotalAmount"]),
        //            bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
        //            NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
        //            CheckIn = Connection.ToDate(dr["CheckIn"]),
        //            CheckOut = Connection.ToDate(dr["CheckOut"]),
        //            BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
        //            OrderNo = Connection.ToString(dr["OrderNo"]),
        //            AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
        //            day = Connection.ToInteger(dr["NoOfDays"]),
        //            NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
        //            NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
        //            NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
        //            Notes = Connection.ToString(dr["Notes"]),
        //            propertyLocation = Connection.ToString(dr["propertyLocation"]),
        //            Quantity = Connection.ToInteger(dr["Quantity"]),
        //            BookingItemStatus = Connection.ToInteger(dr["BookingItemStatus"]),
        //            FirstName = Connection.ToString(dr["sellerFirstname"]),
        //            LastName = Connection.ToString(dr["sellerLastName"]),
        //            Email = Connection.ToString(dr["sellerEmail"]),
        //            City = Connection.ToString(dr["sellerCity"]),
        //            Phone = Connection.ToString(dr["sellerPhone"]),
        //            Address = Connection.ToString(dr["sellerAddress"]),
        //            CountryName = Connection.ToString(dr["sellerCountry"]),
        //            Mobile = Connection.ToString(dr["sellerMobile"]),
        //            StateName = Connection.ToString(dr["sellerState"]),
        //            TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
        //        });
        //    }
        //    return result; 

        //}
        //For User History

        //for Booking notify

        public List<CLayer.Booking> GetAllBookingNotify(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("BookingNotify_GetAll", param);
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
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return bookings;
        }
        public List<CLayer.BookingDetails> GtBookingItemDetailsHistoryForUser(int status, long userId, int days)
        {
            List<CLayer.BookingDetails> result = new List<CLayer.BookingDetails>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            //CLayer.BookingDetails temp;
            DataTable dt = Connection.GetTable("bookinghistory_GetOnUser", param);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.BookingDetails()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Title = Connection.ToString(dr["Title"])

                });
            }
            return result;
        }

        public string GetCurrentApproverNameForMail(long BookingID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingID));

            Object obj = Connection.ExecuteQueryScalar("GetCurrentApproverName", param);
            return Connection.ToString(obj);
        }

        //*Added by rahul on 06-02-2020
        //*this is for getting login user id of offlinebookings
        public string GetCurrentApproverNameForMailOfOfflineBooking(long BookingID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingID));

            Object obj = Connection.ExecuteQueryScalar("GetCurrentApproverNameForOfflineBooking", param);
            return Connection.ToString(obj);
        }
        //****Ends Here**

        //Get All BookingList For Admin as Recent/Past accroding to the CheckOutDay
        public List<CLayer.Booking> GetBookingRecentDataForAdmin(int days)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.BookingStatus.Cart));
            CLayer.Booking temp;
            DataTable dt = Connection.GetTable("bookinglist_GetAll", param);
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Booking();
                temp.BookingId = Connection.ToLong(dr["BookingId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.CheckIn = Connection.ToDate(dr["CheckIn"]);
                temp.CheckOut = Connection.ToDate(dr["CheckOut"]);
                temp.BookingDate = Connection.ToDate(dr["BookingDate"]);
                temp.FirstName = Connection.ToString(dr["FirstName"]);
                temp.LastName = Connection.ToString(dr["LastName"]);
                temp.Address = Connection.ToString(dr["Address"]);
                temp.NoOfAccomodations = Connection.ToInteger(dr["NoOfAccomodations"]);
                temp.day = Connection.ToInteger(dr["NoOfDays"]);
                temp.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                temp.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                temp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                temp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                temp.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                temp.Notes = Connection.ToString(dr["Notes"]);
                temp.OrderNo = Connection.ToString(dr["OrderNo"]);
                temp.Location = Connection.ToString(dr["Location"]);
                temp.BookingId = Connection.ToLong(dr["BookingId"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                result.Add(temp);
            }
            return result;
        }

        public CLayer.Booking GetBookedForBookingUserId(long BookingUserId)
        {
            CLayer.Booking Forbk = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingUserId", DataPlug.DataType._BigInt, BookingUserId));
            DataTable dt = Connection.GetTable("transation_GetbyForBookingUserId", param);

            if (dt.Rows.Count > 0)
            {
                Forbk = new CLayer.Booking();
                Forbk.ForBookingUserId = BookingUserId;
                Forbk.BookingUserId = BookingUserId;
                Forbk.FirstName = Connection.ToString(dt.Rows[0]["FirstName"]);
                Forbk.LastName = Connection.ToString(dt.Rows[0]["LastName"]);
                Forbk.Email = Connection.ToString(dt.Rows[0]["Email"]);
                Forbk.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                Forbk.AddressId = Connection.ToLong(dt.Rows[0]["AddressId"]);
                //Forbk.AddressText = Connection.ToString(dt.Rows[0]["AddressText"]);
                Forbk.Address = Connection.ToString(dt.Rows[0]["Address"]);
                //Forbk.AddressType = Connection.ToInteger(dt.Rows[0]["AddressType"]);
                //Forbk.ByUserId = Connection.ToLong(dt.Rows[0]["ByUserId"]);
                Forbk.CityId = Connection.ToInteger(dt.Rows[0]["CityId"]);
                Forbk.Country = Connection.ToInteger(dt.Rows[0]["Country"]);
                Forbk.State = Connection.ToInteger(dt.Rows[0]["State"]);
                Forbk.City = Connection.ToString(dt.Rows[0]["City"]);
                Forbk.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                Forbk.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
            }
            return Forbk;
        }

        public List<CLayer.Address> GetpropertyAddress(long propertyId)
        {
            List<CLayer.Address> proper = new List<CLayer.Address>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._Int, propertyId));
            DataTable dt = Connection.GetTable("transaction_PropertyAddress", param);
            foreach (DataRow dr in dt.Rows)
            {
                proper.Add(new CLayer.Address()
                {

                    Country = Connection.ToString(dr["CountryName"]),
                    City = Connection.ToString(dr["City"]),
                    StateName = Connection.ToString(dr["StateName"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    Phone = Connection.ToString(dr["Phone"])
                });
            }
            return proper;

        }


        public List<CLayer.Address> GetBookedForUser(long pBookingId)
        {
            List<CLayer.Address> bookings = new List<CLayer.Address>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, pBookingId));
            DataTable dt = Connection.GetTable("transaction_GetBookedForAddress", param);
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.Address()
                {
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    Firstname = Connection.ToString(dr["ForFirstname"]),
                    Lastname = Connection.ToString(dr["ForLastname"]),
                    AddressText = Connection.ToString(dr["ForAddress"]),
                    City = Connection.ToString(dr["ForCity"]),
                    Country = Connection.ToString(dr["ForCountry"]),
                    StateName = Connection.ToString(dr["ForState"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["ForUserMobile"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Email = Connection.ToString(dr["ForUserEmail"]),
                    ForBookingUserId = Connection.ToLong(dr["BookingForUserId"]),
                    CountryCode = Connection.ToString(dr["CountryCode"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    ForUserEmail = Connection.ToString(dr["ForUserEmail"]),
                    ForUserMobile = Connection.ToString(dr["ForUserMobile"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"])
                });
            }
            return bookings;
        }

        //*Added by rahul for getting address of API booking from offlineBookings
        public List<CLayer.Address> GetAPIOfflineBookingBookedForUser(long pBookingId)
        {
            List<CLayer.Address> bookings = new List<CLayer.Address>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, pBookingId));
            DataTable dt = Connection.GetTable("transaction_OfflineBookingGetBookedForAddress", param);
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.Address()
                {
                    BookingId = Connection.ToLong(dr["Offline_BookingId"]),
                    Firstname = Connection.ToString(dr["ForFirstname"]),
                    Lastname = Connection.ToString(dr["ForLastname"]),
                    AddressText = Connection.ToString(dr["ForAddress"]),
                    City = Connection.ToString(dr["ForCity"]),
                    Country = Connection.ToString(dr["ForCountry"]),
                    StateName = Connection.ToString(dr["ForState"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["ForUserMobile"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Email = Connection.ToString(dr["ForUserEmail"]),
                    ForBookingUserId = Connection.ToLong(dr["BookingForUserId"]),
                    CountryCode = Connection.ToString(dr["CountryCode"]),
                    BookingItemId = Connection.ToLong(dr["BookingId"]),
                    ForUserEmail = Connection.ToString(dr["ForUserEmail"]),
                    ForUserMobile = Connection.ToString(dr["ForUserMobile"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"])
                });
            }
            return bookings;
        }

        //**Ends here

        //Added by rahul for offlinebooking for getting address of booking user
        public List<CLayer.Address> GetOfflineBookedForUser(long pBookingId)
        {
            List<CLayer.Address> bookings = new List<CLayer.Address>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, pBookingId));
            DataTable dt = Connection.GetTable("offlinebookingtransaction_GetBookedForAddress", param);
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.Address()
                {
                    BookingId = Connection.ToLong(dr["Offline_BookingId"]),
                    Firstname = Connection.ToString(dr["ForFirstname"]),
                    Lastname = Connection.ToString(dr["ForLastname"]),
                    AddressText = Connection.ToString(dr["ForAddress"]),
                    City = Connection.ToString(dr["ForCity"]),
                    Country = Connection.ToString(dr["ForCountry"]),
                    StateName = Connection.ToString(dr["ForState"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["ForUserMobile"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Email = Connection.ToString(dr["ForUserEmail"]),
                    ForBookingUserId = Connection.ToLong(dr["BookingForUserId"]),
                    CountryCode = Connection.ToString(dr["CountryCode"]),
                    BookingItemId = Connection.ToLong(dr["BookingId"]),
                    ForUserEmail = Connection.ToString(dr["ForUserEmail"]),
                    ForUserMobile = Connection.ToString(dr["ForUserMobile"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"])
                });
            }
            return bookings;
        }


        //*******Ends here***


        public List<CLayer.Booking> GetPartialBookingDetails()
        {
            List<CLayer.Booking> bookings = new List<CLayer.Booking>();
            DataTable dt = Connection.GetTable("PartialPayment_GetAllDetailsForEmail");
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.Booking()
                {
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    Mobile = Connection.ToString(dr["ForUserMobile"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Email = Connection.ToString(dr["ForUserEmail"]),
                    ByUserId = Connection.ToInteger(dr["ForUserId"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    PropertyId = Connection.ToInteger(dr["PropertyId"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    ForBookingUserId = Connection.ToLong(dr["BookingForUserId"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    PartialPaymentPercentage = Connection.ToDecimal(dr["PartialPaymentPercentage"]),
                    PaymentFirstinstallment = Connection.ToDecimal(dr["PaymentFirstinstallment"]),
                    PaymentSecondinstallment = Connection.ToDecimal(dr["PaymentSecondinstallment"])
                });
            }
            return bookings;
        }


        public CLayer.Booking GetPartialBookDetailsbyBookId(long BookId)
        {
            CLayer.Booking Forbk = new CLayer.Booking();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, BookId));
            DataTable dt = Connection.GetTable("PartialPayment_GetAllDetailsByBookId", param);
            if (dt.Rows.Count > 0)
            {
                Forbk = new CLayer.Booking();
                Forbk.BookingId = Connection.ToLong(dt.Rows[0]["BookingId"]);
                Forbk.BookingDate = Connection.ToDate(dt.Rows[0]["BookingDate"]);
                Forbk.Mobile = Connection.ToString(dt.Rows[0]["ForUserMobile"]);
                Forbk.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                Forbk.Email = Connection.ToString(dt.Rows[0]["ForUserEmail"]);
                Forbk.ByUserId = Connection.ToInteger(dt.Rows[0]["ForUserId"]);
                Forbk.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn"]);
                Forbk.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut"]);
                Forbk.PropertyId = Connection.ToInteger(dt.Rows[0]["PropertyId"]);
                Forbk.PropertyTitle = Connection.ToString(dt.Rows[0]["PropertyTitle"]);
                Forbk.propertyAddress = Connection.ToString(dt.Rows[0]["propertyAddress"]);
                Forbk.ForBookingUserId = Connection.ToLong(dt.Rows[0]["BookingForUserId"]);
                Forbk.BookingItemId = Connection.ToLong(dt.Rows[0]["BookingItemId"]);
                Forbk.PartialPaymentPercentage = Connection.ToDecimal(dt.Rows[0]["PartialPaymentPercentage"]);
                Forbk.PaymentFirstinstallment = Connection.ToDecimal(dt.Rows[0]["PaymentFirstinstallment"]);
                Forbk.PaymentSecondinstallment = Connection.ToDecimal(dt.Rows[0]["PaymentSecondinstallment"]);
                Forbk.PartialPaymentStatus = Connection.ToInteger(dt.Rows[0]["PartialPaymentStatus"]);

            }
            return Forbk;
        }

        public CLayer.Booking GetPartialBCancelDetailsbyBookId(long BookId)
        {
            CLayer.Booking Forbk = new CLayer.Booking();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, BookId));
            DataTable dt = Connection.GetTable("PartialPayment_GetAllBCancelDetailsByBookId", param);
            if (dt.Rows.Count > 0)
            {
                Forbk = new CLayer.Booking();
                Forbk.FirstName = Connection.ToString(dt.Rows[0]["FirstName"]);
                Forbk.OrderNo = Connection.ToString(dt.Rows[0]["OrderNo"]);
                Forbk.BookingId = Connection.ToLong(dt.Rows[0]["BookingId"]);
                Forbk.TotalAmount = Connection.ToDecimal(dt.Rows[0]["TotalAmount"]);
                Forbk.BookingDate = Connection.ToDate(dt.Rows[0]["BookingDate"]);
                Forbk.Mobile = Connection.ToString(dt.Rows[0]["ForUserMobile"]);
                Forbk.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                Forbk.Email = Connection.ToString(dt.Rows[0]["ForUserEmail"]);
                Forbk.ByUserId = Connection.ToInteger(dt.Rows[0]["ForUserId"]);
                Forbk.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn"]);
                Forbk.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut"]);
                Forbk.PropertyId = Connection.ToInteger(dt.Rows[0]["PropertyId"]);
                Forbk.PropertyTitle = Connection.ToString(dt.Rows[0]["PropertyTitle"]);
                Forbk.propertyAddress = Connection.ToString(dt.Rows[0]["propertyAddress"]);
                Forbk.ForBookingUserId = Connection.ToLong(dt.Rows[0]["BookingForUserId"]);
                Forbk.BookingItemId = Connection.ToLong(dt.Rows[0]["BookingItemId"]);
                Forbk.PartialPaymentPercentage = Connection.ToDecimal(dt.Rows[0]["PartialPaymentPercentage"]);
                Forbk.PaymentFirstinstallment = Connection.ToDecimal(dt.Rows[0]["PaymentFirstinstallment"]);
                Forbk.PaymentSecondinstallment = Connection.ToDecimal(dt.Rows[0]["PaymentSecondinstallment"]);
                Forbk.Address = Connection.ToString(dt.Rows[0]["Address"]);
                Forbk.City = Connection.ToString(dt.Rows[0]["City"]);


            }
            return Forbk;
        }
        public List<CLayer.Address> GetBookingVerifiedAddress(long pBookingId)
        {
            List<CLayer.Address> bookings = new List<CLayer.Address>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, pBookingId));
            DataTable dt = Connection.GetTable("GetBookingVerifiedby_bookingId", param);
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.Address()
                {
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    Firstname = Connection.ToString(dr["Firstname"]),
                    Lastname = Connection.ToString(dr["Lastname"]),
                    AddressText = Connection.ToString(dr["Address"]),
                    City = Connection.ToString(dr["City"]),
                    Country = Connection.ToString(dr["Country"]),
                    StateName = Connection.ToString(dr["State"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Email = Connection.ToString(dr["Email"]),
                    datetime = Connection.ToDate(dr["VerifiedDate"]),
                });
            }
            return bookings;
        }

        public CLayer.Address GetBookedByUser(long pBookingId)
        {
            CLayer.Address bookings = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, pBookingId));
            DataTable dt = Connection.GetTable("booking_BookedByUser", param);
            foreach (DataRow dr in dt.Rows)
            {
                bookings = new CLayer.Address()
                {
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    Firstname = Connection.ToString(dr["ForFirstname"]),
                    Lastname = Connection.ToString(dr["ForLastname"]),
                    AddressText = Connection.ToString(dr["ForAddress"]),
                    City = Connection.ToString(dr["ForCity"]),
                    Country = Connection.ToString(dr["ForCountry"]),
                    StateName = Connection.ToString(dr["ForState"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["ForUserMobile"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Email = Connection.ToString(dr["ForUserEmail"]),
                    ForBookingUserId = Connection.ToLong(dr["BookingForUserId"]),
                    CountryCode = Connection.ToString(dr["CountryCode"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    ForUserEmail = Connection.ToString(dr["ForUserEmail"]),
                    ForUserMobile = Connection.ToString(dr["ForUserMobile"]),
                    UserId = Connection.ToLong(dr["UserId"])
                };
            }
            return bookings;
        }

        //*Added by rahul on 07-02-2020
        //*For Getting the detils of offline booking user
        public CLayer.Address GetOfflineBookedByUser(long pBookingId)
        {
            CLayer.Address bookings = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, pBookingId));
            DataTable dt = Connection.GetTable("Offlinebooking_BookedByUser", param);
            foreach (DataRow dr in dt.Rows)
            {
                bookings = new CLayer.Address()
                {
                    BookingId = Connection.ToLong(dr["Offline_BookingId"]),
                    Firstname = Connection.ToString(dr["ForFirstname"]),
                    Lastname = Connection.ToString(dr["ForLastname"]),
                    AddressText = Connection.ToString(dr["ForAddress"]),
                    City = Connection.ToString(dr["ForCity"]),
                    Country = Connection.ToString(dr["ForCountry"]),
                    StateName = Connection.ToString(dr["ForState"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["ForUserMobile"]),
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Email = Connection.ToString(dr["ForUserEmail"]),
                    ForBookingUserId = Connection.ToLong(dr["BookingForUserId"]),
                    CountryCode = Connection.ToString(dr["CountryCode"]),
                    BookingItemId = Connection.ToLong(dr["BookingId"]),
                    ForUserEmail = Connection.ToString(dr["ForUserEmail"]),
                    ForUserMobile = Connection.ToString(dr["ForUserMobile"]),
                    UserId = Connection.ToLong(dr["UserId"])
                };
            }
            return bookings;
        }

        //***End**



        public CLayer.Booking GetBookDetailsByBookingId(long pBookingId)
        {
            CLayer.Booking bookings = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Int, pBookingId));
            DataTable dt = Connection.GetTable("booking_GetBookDetailsByBookingId", param);
            foreach (DataRow dr in dt.Rows)
            {
                bookings = new CLayer.Booking()
                {
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    NoOfDays = Connection.ToInteger(dr["NoOfDays"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]),
                    Remarks = Connection.ToString(dr["Remarks"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),

                };
            }
            return bookings;
        }


        //For User 
        public List<CLayer.BookingItem> GetAllBookingForUserData(long Userid)
        {
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, Userid));
            //param.Add(Connection.GetParameter("pSearchstring", DataPlug.DataType._Varchar, SearchString));
            //param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            //param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("booking_GetAllForUserData", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.BookingItem()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    ForBookingUserId = Connection.ToLong(dr["ForBookingUserId"]),
                    ForUserFirstName = Connection.ToString(dr["ForUserFirstName"]),
                    ForUserLastName = Connection.ToString(dr["ForUserLastName"]),
                    ForUserMobile = Connection.ToString(dr["ForUserMobile"]),
                    ForUserEmail = Connection.ToString(dr["ForUserEmail"]),
                    ForUserAddress = Connection.ToString(dr["ForUserAddress"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    ForState = Connection.ToString(dr["ForState"]),
                    ForCity = Connection.ToString(dr["ForCity"]),
                    ForCountry = Connection.ToString(dr["ForCountry"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;

        }


        public List<CLayer.BookingItem> GetAllGuestForbookingList(long Userid)
        {
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, Userid));
            //param.Add(Connection.GetParameter("pSearchstring", DataPlug.DataType._Varchar, SearchString));
            //param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            //param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("booking_GetAllGuestForUserData", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.BookingItem()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    ForBookingUserId = Connection.ToLong(dr["ForBookingUserId"]),
                    ForUserFirstName = Connection.ToString(dr["ForUserFirstName"]),
                    ForUserLastName = Connection.ToString(dr["ForUserLastName"]),
                    ForUserMobile = Connection.ToString(dr["ForUserMobile"]),
                    ForUserEmail = Connection.ToString(dr["ForUserEmail"]),
                    ForUserAddress = Connection.ToString(dr["ForUserAddress"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    ForState = Connection.ToString(dr["ForState"]),
                    ForCity = Connection.ToString(dr["ForCity"]),
                    ForCountry = Connection.ToString(dr["ForCountry"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;

        }
        public long UpdateBookingItemsFor(long bookingId, long ForBookingUserId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pbookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pForBookingUserId", DataPlug.DataType._BigInt, ForBookingUserId));
            object result = Connection.ExecuteQueryScalar("booking_UpdateItemsUserFor", param);
            return Connection.ToLong(result);
        }
        public long SaveBookingFor(CLayer.Booking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingUserId", DataPlug.DataType._Int, data.ForBookingUserId));
            param.Add(Connection.GetParameter("pFirstName", DataPlug.DataType._Varchar, data.FirstName));
            param.Add(Connection.GetParameter("pLastName", DataPlug.DataType._Varchar, data.LastName));
            param.Add(Connection.GetParameter("pMobile", DataPlug.DataType._Varchar, data.Mobile));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, data.Email));
            param.Add(Connection.GetParameter("pAddressId", DataPlug.DataType._BigInt, data.AddressId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.ByUserId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            object result = Connection.ExecuteQueryScalar("bookingForUser_Save", param);
            return Connection.ToLong(result);
        }

        public void DeleteForUser(long BookingUserId, long userId, long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingUserId", DataPlug.DataType._BigInt, BookingUserId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Deleted));
            Connection.ExecuteQueryScalar("Booking_ForUserDelete", param);
        }
        public decimal GetTotal(long bookingId)
        {
            string sql = "Select TotalAmount from booking where BookingId=" + bookingId.ToString();
            object val = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToDecimal(val);
        }
        public decimal GetRefundAmt(long bookingId)
        {
            string sql = "Select LastRefundAmt from booking where BookingId=" + bookingId.ToString();
            object val = Connection.ExecuteSQLQueryScalar(sql);
            if (val == null || val == "")
            {
                val = 0;
            }
            return Connection.ToDecimal(val);
        }

        public decimal GetTotalRefundAmt(long bookingId)
        {
            string sql = "Select TotalRefund from booking where BookingId=" + bookingId.ToString();
            object val = Connection.ExecuteSQLQueryScalar(sql);
            if (val == null || val == "")
            {
                val = 0;
            }
            return Connection.ToDecimal(val);
        }
        public void ClearCart(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.Cart));
            Connection.ExecuteQueryScalar("booking_ClearCart", param);
        }
        //*Added by rahul on 07/03/20 for clearing bookings in offlinebookings
        public void ClearOfflinebookingCart(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.BookingStatus.OfflineBookingCart));
            Connection.ExecuteQueryScalar("booking_OfflinebookingClearCart", param);
        }
        //**
        public CLayer.Booking GetDetails(long bookingId)
        {
            string sql = "SELECT p.Title,p.Address,p.City,b.Status,p.Email,p.Mobile,bi.CheckIn,bi.CheckOut,b.OrderNo,b.BookingDate,b.TotalAmount,bi.HotelConfirmNumber FROM booking b INNER JOIN bookingItems bi ON b.BookingId = bi.BookingId  INNER JOIN accommodation a ON bi.accommodationId =  a.accommodationId " +
                " INNER JOIN property p ON a.propertyId= p.propertyId WHERE b.BookingId= " + bookingId.ToString() + " GROUP BY b.BookingId ";
            CLayer.Booking detail = null;
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                detail = new CLayer.Booking();
                detail.BookingId = bookingId;
                detail.PropertyTitle = Connection.ToString(dt.Rows[0]["Title"]);
                detail.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn"]);
                detail.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut"]);
                detail.OrderNo = Connection.ToString(dt.Rows[0]["OrderNo"]);
                detail.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                detail.BookingDate = Connection.ToDate(dt.Rows[0]["BookingDate"]);
                detail.TotalAmount = Connection.ToDecimal(dt.Rows[0]["TotalAmount"]);
                detail.PropertyEmail = Connection.ToString(dt.Rows[0]["Email"]);
                detail.City = Connection.ToString(dt.Rows[0]["City"]);
                detail.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                detail.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                detail.HotelConfirmNumber = Connection.ToString(dt.Rows[0]["HotelConfirmNumber"]);
            }
            return detail;
        }

        //*This is for getting details of offlinebooking
        //*Done by rahul on 06-02-2020
        public CLayer.Booking GetOfflineBookingDetails(long bookingId)
        {
            string sql = "SELECT p.Title,p.Address,p.City,ofb.SaveStatus,p.Email,p.Mobile,bd.CheckIn_date,bd.CheckOut_date, " +
                        "ofb.OrderNo,ofb.CreatedDate,ofb.TotalSalePrice,bd.HotelConformationNo " +
                        "FROM offline_bookings ofb  " +
                        "INNER JOIN bookingdetails bd ON ofb.Offline_BookingId = bd.Offline_BookingId " +
                        "INNER JOIN accommodation a ON bd.AccommodatoinTypeId =  a.AccommodationTypeId " +
                        "INNER JOIN property p ON a.propertyId= p.propertyId  " +
                        "WHERE ofb.Offline_BookingId = " + bookingId.ToString() + " " +
                        "GROUP BY ofb.Offline_BookingId ";
            CLayer.Booking detail = null;
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                detail = new CLayer.Booking();
                detail.BookingId = bookingId;
                detail.PropertyTitle = Connection.ToString(dt.Rows[0]["Title"]);
                detail.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn_date"]);
                detail.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut_date"]);
                detail.OrderNo = Connection.ToString(dt.Rows[0]["OrderNo"]);
                detail.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                detail.BookingDate = Connection.ToDate(dt.Rows[0]["CreatedDate"]);
                detail.TotalAmount = Connection.ToDecimal(dt.Rows[0]["TotalSalePrice"]);
                detail.PropertyEmail = Connection.ToString(dt.Rows[0]["Email"]);
                detail.City = Connection.ToString(dt.Rows[0]["City"]);
                detail.Status = Connection.ToInteger(dt.Rows[0]["SaveStatus"]);
                detail.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                detail.HotelConfirmNumber = Connection.ToString(dt.Rows[0]["HotelConformationNo"]);
            }
            return detail;
        }


        //******Ends Here*******

        public CLayer.Booking GetDetailsAmedus(long bookingId)
        {
            string sql = "SELECT p.Title,p.Address,p.City,b.Status,p.Email,p.Mobile,bi.CheckIn,bi.CheckOut,b.OrderNo,b.BookingDate,b.TotalAmount,bi.HotelConfirmNumber FROM booking b INNER JOIN bookingItems bi ON b.BookingId = bi.BookingId  INNER JOIN accommodation a ON bi.accommodationId =  a.accommodationId " +
                " INNER JOIN property p ON a.propertyId= p.propertyId WHERE b.BookingId= " + bookingId.ToString() + " GROUP BY b.BookingId ";
            CLayer.Booking detail = null;
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                detail = new CLayer.Booking();
                detail.BookingId = bookingId;
                detail.PropertyTitle = Connection.ToString(dt.Rows[0]["Title"]);
                detail.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn"]);
                detail.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut"]);
                detail.OrderNo = Connection.ToString(dt.Rows[0]["OrderNo"]);
                detail.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                detail.BookingDate = Connection.ToDate(dt.Rows[0]["BookingDate"]);
                detail.TotalAmount = Connection.ToDecimal(dt.Rows[0]["TotalAmount"]);
                detail.PropertyEmail = Connection.ToString(dt.Rows[0]["Email"]);
                detail.City = Connection.ToString(dt.Rows[0]["City"]);
                detail.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                detail.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                detail.HotelConfirmNumber = Connection.ToString(dt.Rows[0]["HotelConfirmNumber"]);
            }
            return detail;
        }
        public CLayer.Booking GetDetails(string bookingRefNo)
        {
            string sql = "SELECT b.BookingId,p.Title,p.Address,p.City,b.Status,p.Email,p.Mobile,bi.CheckIn,bi.CheckOut,b.OrderNo,b.BookingDate,b.TotalAmount FROM booking b INNER JOIN bookingItems bi ON b.BookingId = bi.BookingId  INNER JOIN accommodation a ON bi.accommodationId =  a.accommodationId " +
                " INNER JOIN property p ON a.propertyId= p.propertyId WHERE b.OrderNo= '" + bookingRefNo + "' GROUP BY b.BookingId ";
            CLayer.Booking detail = null;
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                detail = new CLayer.Booking();
                detail.BookingId = Connection.ToLong(dt.Rows[0]["BookingId"]);
                detail.PropertyTitle = Connection.ToString(dt.Rows[0]["Title"]);
                detail.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn"]);
                detail.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut"]);
                detail.OrderNo = Connection.ToString(dt.Rows[0]["OrderNo"]);
                detail.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                detail.BookingDate = Connection.ToDate(dt.Rows[0]["BookingDate"]);
                detail.TotalAmount = Connection.ToDecimal(dt.Rows[0]["TotalAmount"]);
                detail.PropertyEmail = Connection.ToString(dt.Rows[0]["Email"]);
                detail.City = Connection.ToString(dt.Rows[0]["City"]);
                detail.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                detail.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
            }
            return detail;
        }
        //todo added by Keji
        public CLayer.Booking GetDetailsSMS(long bookingId)
        {
            string sql = "SELECT p.Title,p.Mobile,p.Address,p.Email,bi.CheckIn,bi.CheckOut,b.OrderNo,b.BookingDate,b.TotalAmount,aat.Title as AccommodationTypeTitle,p.City as propertyCity,bi.HotelConfirmNumber FROM booking b " +
                     " INNER JOIN bookingItems bi ON b.BookingId = bi.BookingId " +
                     " INNER JOIN accommodation a ON bi.accommodationId =  a.accommodationId " +
                     " INNER JOIN accommodationtype aat ON aat.AccommodationTypeId=a.AccommodationTypeId" +
                     " INNER JOIN property p ON a.propertyId= p.propertyId WHERE b.BookingId= " + bookingId.ToString() + " GROUP BY a.accommodationId ";
            CLayer.Booking detail = null;
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                detail = new CLayer.Booking();
                detail.PropertyTitle = Connection.ToString(dt.Rows[0]["Title"]);
                detail.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn"]);
                detail.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut"]);
                detail.OrderNo = Connection.ToString(dt.Rows[0]["OrderNo"]);
                detail.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                detail.BookingDate = Connection.ToDate(dt.Rows[0]["BookingDate"]);
                detail.TotalAmount = Connection.ToDecimal(dt.Rows[0]["TotalAmount"]);
                detail.PropertyEmail = Connection.ToString(dt.Rows[0]["Email"]);
                detail.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                detail.AccommodationTypeTitle = Connection.ToString(dt.Rows[0]["AccommodationTypeTitle"]);
                detail.propertyCity = Connection.ToString(dt.Rows[0]["propertyCity"]);
                detail.HotelConfirmNumber = Connection.ToString(dt.Rows[0]["HotelConfirmNumber"]);
            }
            return detail;
        }
        public CLayer.Booking GetAllGDSCustomerBillingaddress(long BookingId, int UserID)
        {
            CLayer.Booking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingId));
            param.Add(Connection.GetParameter("pUserID", DataPlug.DataType._Int, UserID));

            DataTable dt = Connection.GetTable("Booking_GetAllGDSCustomerBillingaddress", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.Booking();
                result.BookingId = BookingId;
                result.BillingAddress = Connection.ToString(dt.Rows[0]["BillingAddress"]);
                result.BillingCityname = Connection.ToString(dt.Rows[0]["BillingCityname"]);
                result.BillingCountryname = Connection.ToString(dt.Rows[0]["BillingCountryName"]);
                result.BillingStatename = Connection.ToString(dt.Rows[0]["BillingStateName"]);
                result.BillingpinCode = Connection.ToString(dt.Rows[0]["BillingPINCode"]);
            }
            return result;
        }
        public CLayer.Booking GetAllGDSCustomerDetails(long BookingId, int UserID)
        {
            CLayer.Booking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingId));
            param.Add(Connection.GetParameter("pUserID", DataPlug.DataType._Int, UserID));

            DataTable dt = Connection.GetTable("Booking_GetAllGDSCustomerDetails", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.Booking();
                result.BookingId = BookingId;
                result.CustomerName = Connection.ToString(dt.Rows[0]["customerfirstname"]) + " " + Connection.ToString(dt.Rows[0]["customerlastname"]);
                result.CustomerEmail = Connection.ToString(dt.Rows[0]["customeremail"]);
                result.CustomerAddress = Connection.ToString(dt.Rows[0]["customeraddress"]);
                result.CustomerMobile = Connection.ToString(dt.Rows[0]["customermobile"]);
                result.CustomerpinCode = Connection.ToString(dt.Rows[0]["CustomerPinCode"]);
                result.CustomerType = Connection.ToInteger(dt.Rows[0]["CustomerType"]);

                result.CustomerCountry = Connection.ToInteger(dt.Rows[0]["customercountryid"]);
                result.CustomerCity = Connection.ToInteger(dt.Rows[0]["customercityname"]);
                result.CustomerState = Connection.ToInteger(dt.Rows[0]["customerstateid"]);

                result.CustomerCountryname = Connection.ToString(dt.Rows[0]["customercountryname"]);
                result.CustomerStatename = Connection.ToString(dt.Rows[0]["customerstatename"]);
                result.CustomerCityname = Connection.ToString(dt.Rows[0]["customercityname"]);


            }
            return result;
        }
        public CLayer.Booking GetAllGDSpropertyDetails(long BookingId)
        {
            CLayer.Booking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingId));

            DataTable dt = Connection.GetTable("Booking_GetAllGDSProperty", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.Booking();

                result.PropertyName = Connection.ToString(dt.Rows[0]["PropertyName"]);
                result.PropertyAddress = Connection.ToString(dt.Rows[0]["PropertyAddress"]);
                result.PropertyCityname = Connection.ToString(dt.Rows[0]["PropertyCityname"]);
                result.PropertyStatename = Connection.ToString(dt.Rows[0]["PropertyStatename"]);
                result.PropertyCountryname = Connection.ToString(dt.Rows[0]["PropertyCountryname"]);
                result.PropertyContactNo = Connection.ToString(dt.Rows[0]["PropertyContactNo"]);
                // result.PropertyCaretakerName = Connection.ToString(dt.Rows[0]["PropertyCaretakerName"]);
                result.PropertyEmail = Connection.ToString(dt.Rows[0]["PropertyEmail"]);
                result.PropertyPinCode = Connection.ToString(dt.Rows[0]["PropertyPinCode"]);

                //result.SupplierPinCode = Connection.ToString(dt.Rows[0]["SupplierPinCode"]);
                //result.SupplierName = Connection.ToString(dt.Rows[0]["SupplierName"]);
                //result.SupplierAddress = Connection.ToString(dt.Rows[0]["SupplierAddress"]);
                //result.SupplierCityname = Connection.ToString(dt.Rows[0]["SupplierCityname"]);
                //result.SupplierStatename = Connection.ToString(dt.Rows[0]["SupplierStatename"]);
                //result.SupplierCountryname = Connection.ToString(dt.Rows[0]["SupplierCountryname"]);
                //result.SupplierMobile = Connection.ToString(dt.Rows[0]["SupplierMobile"]);
                //result.SupplierEmail = Connection.ToString(dt.Rows[0]["SupplierEmail"]);


                //result.PropertyCity = Connection.ToInteger(dt.Rows[0]["PropertyCity"]);
                //result.PropertyCountry = Connection.ToInteger(dt.Rows[0]["PropertyCountry"]);
                //result.PropertyState = Connection.ToInteger(dt.Rows[0]["PropertyState"]);
                //result.SupplierCity = Connection.ToInteger(dt.Rows[0]["SupplierCity"]);
                //result.SupplierCountry = Connection.ToInteger(dt.Rows[0]["SupplierCountry"]);
                //result.SupplierState = Connection.ToInteger(dt.Rows[0]["SupplierState"]);
            }
            return result;
        }
        public List<CLayer.BookingApprovals> GetAllCorporateBookingApprovals(long pCorporateID)
        {
            List<CLayer.BookingApprovals> ids = new List<CLayer.BookingApprovals>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCorporateID", DataPlug.DataType._BigInt, pCorporateID));

            DataTable dt = Connection.GetTable("BookingApprovals_Get", param);
            foreach (DataRow dr in dt.Rows)
            {
                CLayer.BookingApprovals objIds = new CLayer.BookingApprovals();
                objIds.Booking_Approval_Id = Connection.ToLong(dr["booking_approval_id"]);
                objIds.BookingId = Connection.ToLong(dr["BookingID"]);
                objIds.approver_id = Connection.ToLong(dr["Approver_id"]);
                objIds.approver = Connection.ToString(dr["Approver"]);
                objIds.CreatedDate = Connection.ToDate(dr["CreatedDate"]);
                objIds.approval_order = Connection.ToInteger(dr["ApprovalOrder"]);
                objIds.approval_status = Connection.ToInteger(dr["ApprovalStatusID"]);
                objIds.approvalstatus = Connection.ToString(dr["ApprovalStatus"]);
                ids.Add(objIds);
            }
            return ids;
        }
        public List<CLayer.BookingApprovals> GetAllCorporateBookingApprovalsForConfirm(long pCorporateID)
        {
            List<CLayer.BookingApprovals> ids = new List<CLayer.BookingApprovals>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCorporateID", DataPlug.DataType._BigInt, pCorporateID));

            DataTable dt = Connection.GetTable("BookingApprovalsForConfirm_Get", param);
            foreach (DataRow dr in dt.Rows)
            {
                CLayer.BookingApprovals objIds = new CLayer.BookingApprovals();
                objIds.Booking_Approval_Id = Connection.ToLong(dr["booking_approval_id"]);
                objIds.BookingId = Connection.ToLong(dr["BookingID"]);
                objIds.approver_id = Connection.ToLong(dr["Approver_id"]);
                objIds.approver = Connection.ToString(dr["Approver"]);
                objIds.CreatedDate = Connection.ToDate(dr["CreatedDate"]);
                objIds.approval_order = Connection.ToInteger(dr["ApprovalOrder"]);
                objIds.approval_status = Connection.ToInteger(dr["ApprovalStatusID"]);
                objIds.approvalstatus = Connection.ToString(dr["ApprovalStatus"]);
                ids.Add(objIds);
            }
            return ids;
        }
        public long BookingApprovalsSave(CLayer.BookingApprovals data)
        {
            //List<CLayer.BookingApprovals> objApprovalList= GetMaxApprovalOrderAndApprovalOrder(data);
            //foreach(var item in objApprovalList)
            //{
            //    data.approval_order = item.approval_order;
            //    data.Maxapproval_order = item.Maxapproval_order;
            //}

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingApproval_id", DataPlug.DataType._BigInt, data.Booking_Approval_Id));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pApproverId", DataPlug.DataType._BigInt, data.approver_id));
            param.Add(Connection.GetParameter("pApprovalOrder", DataPlug.DataType._Int, data.approval_order));
            param.Add(Connection.GetParameter("pApprovalStatus", DataPlug.DataType._BigInt, data.approval_status));
            param.Add(Connection.GetParameter("pRejectionNote", DataPlug.DataType._Text, data.RejectionNote));
            //   param.Add(Connection.GetParameter("pMaxApprovalOrder", DataPlug.DataType._Int, data.approval_order));
            //    param.Add(Connection.GetParameter("pUserID", DataPlug.DataType._BigInt , 0));
            object result = Connection.ExecuteQueryScalar("bookingapprovals_save", param);
            return Connection.ToLong(result);
        }
        public List<CLayer.BookingApprovals> GetMaxApprovalOrderAndApprovalOrder(CLayer.BookingApprovals data)
        {
            List<CLayer.BookingApprovals> ids = new List<CLayer.BookingApprovals>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingApproval_id", DataPlug.DataType._BigInt, data.Booking_Approval_Id));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pApproverId", DataPlug.DataType._BigInt, data.approver_id));
            param.Add(Connection.GetParameter("pApprovalOrder", DataPlug.DataType._Int, data.approval_order));
            param.Add(Connection.GetParameter("pApprovalStatus", DataPlug.DataType._BigInt, data.approval_status));
            param.Add(Connection.GetParameter("pMaxApprovalOrder", DataPlug.DataType._Int, data.approval_order));
            param.Add(Connection.GetParameter("pUserID", DataPlug.DataType._BigInt, 0));
            DataTable dt = Connection.GetTable("GetMaxApprovalOrderAndApprovalOrder", param);
            foreach (DataRow dr in dt.Rows)
            {
                CLayer.BookingApprovals objIds = new CLayer.BookingApprovals();
                objIds.approval_order = Connection.ToInteger(dr["pApprovalOrder"]);
                objIds.Maxapproval_order = Connection.ToInteger(dr["pMaxApprovalOrder"]);
                ids.Add(objIds);
            }
            return ids;

        }
        public long GetApprovalStatus(long bookinglId, long ApproverID = 0)
        {
            string sql = "SELECT approval_status FROM booking_approvals WHERE approver_id=" + ApproverID + " AND booking_id=" + bookinglId + "";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        public string GetCrediBookingApprover(long ApproverID)
        {
            string sql = "SELECT Concat(firstname,' ',lastname) from user where userid=" + ApproverID + "";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public bool GetBookingApprovalStatus(long Approverid, long BookingId)
        {
            string sql = " SELECT * FROM booking_approvals WHERE approver_id=" + Approverid + " AND booking_id=" + BookingId + " AND approval_status > " + (int)CLayer.ObjectStatus.BookingApprovalStatus.ConfirmedforBooking + "";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CLayer.Booking GetNextApproverDetails(long bookingId)
        {
            CLayer.Booking objApproval = new CLayer.Booking();
            string sql = "SELECT (SELECT CONCAT(Firstname,' ',lastname) FROM `user` WHERE userid=bb.approver_id) as 'UserName',(SELECT email FROM `user` WHERE userid=bb.approver_id) as `Email` ,ifnull(ad.phone,'') as phone,ifnull(ad.mobile,'') as mobile      FROM BOOKING B LEFT JOIN booking_approvals BA ON B.BookingID=BA.Booking_id " +
                        "  LEFT JOIN address ad ON b.byuserid=ad.userid  LEFT JOIN b2b_approvers bb ON b.byuserid = bb.user_id  WHERE b.bookingid = " + bookingId + " AND bb.approver_order > IFNULL((SELECT IFNULL(approval_order, 0) FROM booking_approvals WHERE booking_id =" + bookingId + "),0) LIMIT 1";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                objApproval.ApproverName = Connection.ToString(dt.Rows[0]["UserName"]);
                objApproval.ApproverEmail = Connection.ToString(dt.Rows[0]["Email"]);
                objApproval.Phone = Connection.ToString(dt.Rows[0]["phone"]);
                objApproval.Mobile = Connection.ToString(dt.Rows[0]["mobile"]);

            }
            return objApproval;
        }

        //Added by rahul on 06-02-2020 for getting next approver id for Offline Bookings
        public CLayer.Booking GetNextApproverDetailsForOfflineBooking(long bookingId)
        {
            CLayer.Booking objApproval = new CLayer.Booking();
            string sql = "SELECT (SELECT CONCAT(Firstname,' ',lastname) " +
                            "FROM `user` " +
                            "WHERE userid=bb.approver_id) as 'UserName', " +
                            "(SELECT email  " +
                            "FROM `user` " +
                            "WHERE userid=bb.approver_id) as `Email` ,ifnull(ad.phone,'') as phone, " +
                            "ifnull(ad.mobile,'') as mobile " +
                            "FROM offline_bookings ofb " +
                            "LEFT JOIN booking_approvals BA ON ofb.Offline_BookingId=BA.Booking_id " +
                            "LEFT JOIN address ad ON ofb.createdby=ad.userid " +
                            "LEFT JOIN b2b_approvers bb ON ofb.createdby = bb.user_id " +
                            "WHERE ofb.Offline_BookingId = " + bookingId + " " +
                            "AND bb.approver_order > IFNULL((SELECT IFNULL(approval_order, 0) " +
                            "FROM booking_approvals " +
                            "WHERE ofb.Offline_BookingId = " + bookingId + " " +
                            "),0) LIMIT 1 ";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                objApproval.ApproverName = Connection.ToString(dt.Rows[0]["UserName"]);
                objApproval.ApproverEmail = Connection.ToString(dt.Rows[0]["Email"]);
                objApproval.Phone = Connection.ToString(dt.Rows[0]["phone"]);
                objApproval.Mobile = Connection.ToString(dt.Rows[0]["mobile"]);

            }
            return objApproval;
        }

        //****End**



        public long GetNextApproverID(long bookingId)
        {
            CLayer.Booking objApproval = new CLayer.Booking();
            long ApproverID = 0;
            string sql = "SELECT bb.approver_id  FROM BOOKING B LEFT JOIN booking_approvals BA ON B.BookingID=BA.Booking_id " +
                        "  LEFT JOIN address ad ON b.byuserid=ad.userid  LEFT JOIN b2b_approvers bb ON b.byuserid = bb.user_id  WHERE b.bookingid = " + bookingId + " AND bb.approver_order > IFNULL((SELECT IFNULL(approval_order, 0) FROM booking_approvals WHERE booking_id =" + bookingId + "),0) LIMIT 1";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                ApproverID = Connection.ToLong(dt.Rows[0]["approver_id"]);

            }
            return ApproverID;
        }

        //Added by rahul on 06-02-2020 for getting approver id for Offline Bookings
        public long GetNextApproverIDForOfflineBooking(long bookingId)
        {
            CLayer.Booking objApproval = new CLayer.Booking();
            long ApproverID = 0;
            string sql = "SELECT bb.approver_id " +
                        "FROM offline_bookings ofb " +
                        "LEFT JOIN booking_approvals BA ON ofb.Offline_BookingId=BA.Booking_id " +
                        "LEFT JOIN address ad ON ofb.createdby=ad.userid " +
                        "LEFT JOIN b2b_approvers bb ON ofb.createdby = bb.user_id  " +
                        "WHERE ofb.Offline_BookingId = " + bookingId + " " +
                        "AND bb.approver_order > IFNULL((SELECT IFNULL(approval_order, 0) " +
                        "FROM booking_approvals WHERE booking_id = " + bookingId + " " +
                        " ),0) LIMIT 1; ";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                ApproverID = Connection.ToLong(dt.Rows[0]["approver_id"]);

            }
            return ApproverID;
        }

        //****End**




        public List<CLayer.Booking> GetPreviousApproverDetails(long bookingId)
        {
            List<CLayer.Booking> ids = new List<CLayer.Booking>();

            string sql = "SELECT DISTINCT Sub.UserID,Sub.UserName,Sub.Email, Sub.phone,Sub.mobile FROM " +
                       " (SELECT bb.approver_id AS UserID, (SELECT CONCAT(IFNULL(firstname,''),' ',IFNULL(lastname,'')) FROM `user` WHERE userid = bb.approver_id) AS 'UserName',(SELECT email FROM `user` WHERE userid = bb.approver_id) AS `Email`,ifnull(ad.phone,'') as phone,ifnull(ad.mobile,'') as mobile " +
                       " FROM BOOKING B LEFT JOIN booking_approvals BA ON B.BookingID = BA.Booking_id " +
                       "  LEFT JOIN address ad ON b.byuserid=ad.userid " +
                       " LEFT JOIN b2b_approvers bb ON b.byuserid = bb.user_id  WHERE b.bookingid = " + bookingId + " AND" +
                       "  bb.approver_order < IFNULL((SELECT IFNULL(max(approval_order), 0) FROM booking_approvals WHERE booking_id =" + bookingId + "),0) " +
                       "  UNION " +
                       "  SELECT distinct bb.USER_id AS UserID,(SELECT CONCAT(IFNULL(firstname,''),' ',IFNULL(lastname,'')) FROM `user` WHERE userid = bb.user_id) AS 'UserName',(SELECT email FROM `user` WHERE userid = bb.user_id) AS `Email`,ifnull(ad.phone,'') as phone,ifnull(ad.mobile,'') as mobile " +
                       " FROM BOOKING B LEFT JOIN booking_approvals BA ON B.BookingID = BA.Booking_id  " +
                        "  LEFT JOIN address ad ON b.byuserid=ad.userid " +
                       " LEFT JOIN b2b_approvers bb ON b.byuserid = bb.user_id  WHERE b.bookingid = " + bookingId + " )SUB; ";
            DataTable dt = Connection.GetSQLTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                CLayer.Booking objApproval = new CLayer.Booking();

                //objApproval.ApproverName = Connection.ToString(dt.Rows[0]["UserName"]);
                //objApproval.ApproverEmail = Connection.ToString(dt.Rows[0]["Email"]);
                //objApproval.Phone = Connection.ToString(dt.Rows[0]["phone"]);
                //objApproval.Mobile = Connection.ToString(dt.Rows[0]["mobile"]);
                objApproval.ApproverName = Connection.ToString(dr["UserName"]);
                objApproval.ApproverEmail = Connection.ToString(dr["Email"]);
                objApproval.Phone = Connection.ToString(dr["phone"]);
                objApproval.Mobile = Connection.ToString(dr["mobile"]);

                ids.Add(objApproval);
            }
            return ids;
        }
        public string GetGstState(string state)
        {
            string sql = "select gst_state_name from ggn_gst_states where gst_state_name = '" + state + "'";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return (Connection.ToString(obj));
        }
        public CLayer.Booking GetBookingUserDetails(long bookingId)
        {
            CLayer.Booking objBookingUser = new CLayer.Booking();
            string sql = " SELECT(SELECT CONCAT(Firstname, ' ', lastname) AS `UserName` FROM `user` WHERE userid = b.byuserid) as `UserName` ," +
                        "  (SELECT Email AS `UserName` FROM `user` WHERE userid = b.byuserid) as `Email`,ifnull(ad.phone,'') as phone,ifnull(ad.mobile,'') as mobile " +
                         "  FROM BOOKING B LEFT JOIN booking_approvals BA ON B.BookingID = BA.Booking_id  " +
                         "  LEFT JOIN address ad ON b.byuserid=ad.userid " +
                         " LEFT JOIN b2b_approvers bb ON b.byuserid = bb.user_id  WHERE b.bookingid =" + bookingId + "  LIMIT 1    ";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                objBookingUser.BookingUserName = Connection.ToString(dt.Rows[0]["UserName"]);
                objBookingUser.BookingUserEmail = Connection.ToString(dt.Rows[0]["Email"]);
                objBookingUser.Phone = Connection.ToString(dt.Rows[0]["phone"]);
                objBookingUser.Mobile = Connection.ToString(dt.Rows[0]["mobile"]);

            }
            return objBookingUser;

        }

        //*Added by rahul on 07-02-2020
        //*This is for getting Offline Booked User Details
        public CLayer.Booking GetOfflineBookingUserDetails(long bookingId)
        {
            CLayer.Booking objBookingUser = new CLayer.Booking();
            string sql = "SELECT(SELECT CONCAT(Firstname, ' ', lastname) AS `UserName` " +
                        "FROM `user` " +
                        "WHERE userid = ofb.createdby) as `UserName`, " +
                        "(SELECT Email AS `UserName` " +
                        "FROM `user` " +
                        "WHERE userid = ofb.createdby) as `Email`,ifnull(ad.phone,'') as phone,ifnull(ad.mobile,'') as mobile " +
                        "FROM offline_bookings ofb " +
                        "LEFT JOIN booking_approvals BA ON ofb.Offline_BookingId = BA.Booking_id  " +
                        "LEFT JOIN address ad ON ofb.createdby=ad.userid " +
                        "LEFT JOIN b2b_approvers bb ON ofb.createdby = bb.user_id  " +
                        "WHERE ofb.Offline_BookingId = " + bookingId +
                        "LIMIT 1 ";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                objBookingUser.BookingUserName = Connection.ToString(dt.Rows[0]["UserName"]);
                objBookingUser.BookingUserEmail = Connection.ToString(dt.Rows[0]["Email"]);
                objBookingUser.Phone = Connection.ToString(dt.Rows[0]["phone"]);
                objBookingUser.Mobile = Connection.ToString(dt.Rows[0]["mobile"]);

            }
            return objBookingUser;

        }
        //***END**



        public string GetApproverNameByID(long ApproverID)
        {
            string sql = "SELECT CONCAT(IFNULL(firstname,''),' ',IFNULL(lastname,'')) FROM USER WHERE userid=" + ApproverID + "";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public CLayer.Booking GetBookingDetailsByBookingUserId(long BookingUserId)
        {
            CLayer.Booking Forbk = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingUserId", DataPlug.DataType._BigInt, BookingUserId));
            DataTable dt = Connection.GetTable("transation_GetbyForBookingUserId", param);

            if (dt.Rows.Count > 0)
            {
                Forbk = new CLayer.Booking();
                Forbk.ForBookingUserId = BookingUserId;
                Forbk.BookingUserId = BookingUserId;
                Forbk.FirstName = Connection.ToString(dt.Rows[0]["FirstName"]);
                Forbk.LastName = Connection.ToString(dt.Rows[0]["LastName"]);
                Forbk.Email = Connection.ToString(dt.Rows[0]["Email"]);
                Forbk.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                Forbk.AddressId = Connection.ToLong(dt.Rows[0]["AddressId"]);
                //Forbk.AddressText = Connection.ToString(dt.Rows[0]["AddressText"]);
                Forbk.Address = Connection.ToString(dt.Rows[0]["Address"]);
                //Forbk.AddressType = Connection.ToInteger(dt.Rows[0]["AddressType"]);
                //Forbk.ByUserId = Connection.ToLong(dt.Rows[0]["ByUserId"]);
                Forbk.CityId = Connection.ToInteger(dt.Rows[0]["CityId"]);
                Forbk.Country = Connection.ToInteger(dt.Rows[0]["Country"]);
                Forbk.State = Connection.ToInteger(dt.Rows[0]["State"]);
                Forbk.City = Connection.ToString(dt.Rows[0]["City"]);
                Forbk.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                Forbk.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
            }
            return Forbk;
        }
    }

}
