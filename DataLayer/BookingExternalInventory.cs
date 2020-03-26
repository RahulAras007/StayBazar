using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class BookingExternalInventory : DataLink
    {
        public long Save(CLayer.BookingExternalInventory data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingExtInvReqId", DataPlug.DataType._Varchar, data.BookingExtInvReqId));
            param.Add(Connection.GetParameter("pHotel_id", DataPlug.DataType._Varchar, data.hotel_id));
            param.Add(Connection.GetParameter("pHotel_name", DataPlug.DataType._Varchar, data.hotel_name));
            param.Add(Connection.GetParameter("pRoomtype_id", DataPlug.DataType._Varchar, data.roomtype_id));
            param.Add(Connection.GetParameter("pRoom_name", DataPlug.DataType._Varchar, data.room_name));
            param.Add(Connection.GetParameter("pRoom_desc", DataPlug.DataType._Text, data.room_desc));
            param.Add(Connection.GetParameter("pFinal_price_at_bookingamt", DataPlug.DataType._Decimal, data.final_price_at_bookingamt));
            param.Add(Connection.GetParameter("pFinal_price_at_bookingamtcurr", DataPlug.DataType._Varchar, data.final_price_at_bookingamtcurr));
            param.Add(Connection.GetParameter("pFinal_price_at_checkoutamt", DataPlug.DataType._Decimal, data.final_price_at_checkoutamt));
            param.Add(Connection.GetParameter("pFinal_price_at_checkoutamtcurr", DataPlug.DataType._Varchar, data.final_price_at_checkoutamtcurr));
            param.Add(Connection.GetParameter("pDomainId", DataPlug.DataType._Varchar, data.DomainId));
            param.Add(Connection.GetParameter("pPromotionId", DataPlug.DataType._Varchar, data.PromotionId));
            param.Add(Connection.GetParameter("pRatePlanId", DataPlug.DataType._Varchar, data.RatePlanId));

            object result = Connection.ExecuteQueryScalar("BookingExternalInventory_save", param);
            return Connection.ToInteger(result);
        }


        public long SaveBookingCancelResponse(CLayer.BookingExternalInventory data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingExtInvReqId", DataPlug.DataType._BigInt, data.BookingExtInvReqId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pReservation_id", DataPlug.DataType._Varchar, data.reservation_id));
            param.Add(Connection.GetParameter("pCancellation_Status", DataPlug.DataType._Int, data.Cancellation_Status));
            param.Add(Connection.GetParameter("pCancelled_Date", DataPlug.DataType._Varchar, data.Cancelled_Date));
            param.Add(Connection.GetParameter("pCancellation_Number", DataPlug.DataType._Varchar, data.Cancellation_Number));
            param.Add(Connection.GetParameter("pCancellation_Response", DataPlug.DataType._Text, data.Cancellation_Response));
          

            object result = Connection.ExecuteQueryScalar("BookingExternalInventoryCancel_save", param);
            return Connection.ToInteger(result);
        }

        public CLayer.BookingExternalInventory GetAllDetailsByRoomIdandHotelId(string RoomId, string HotelId)
        {
            CLayer.BookingExternalInventory result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomId", DataPlug.DataType._Varchar, RoomId));
            param.Add(Connection.GetParameter("pHotelId", DataPlug.DataType._Varchar, HotelId));
            DataTable dt = Connection.GetTable("BookExternalInventory_ByRoomIdandHotelId", param);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.BookingExternalInventory()
                {
                    hotel_id = Connection.ToString(dt.Rows[0]["HotelId"]),
                    hotel_name = Connection.ToString(dt.Rows[0]["HotelName"]),
                    roomtype_id = Connection.ToString(dt.Rows[0]["RoomId"]),
                    room_name = Connection.ToString(dt.Rows[0]["RoomName"]),
                    room_desc = Connection.ToString(dt.Rows[0]["RoomDesc"]),
                    final_price_at_bookingamt = Connection.ToDecimal(dt.Rows[0]["FinalPriceAtBookingAmt"]),
                    final_price_at_bookingamtcurr = Connection.ToString(dt.Rows[0]["FinalPriceAtBookingAmtCurr"]),
                    final_price_at_checkoutamt = Connection.ToDecimal(dt.Rows[0]["FinalPriceAtCheckoutAmt"]),
                    final_price_at_checkoutamtcurr = Connection.ToString(dt.Rows[0]["FinalPriceAtCheckoutAmtCurr"]),
                    DomainId = Connection.ToString(dt.Rows[0]["DomainId"]),
                    RatePlanId = Connection.ToString(dt.Rows[0]["RatePlanId"]),
                    PromotionId = Connection.ToString(dt.Rows[0]["PromotionId"])
                };
            }
            return result;
        }


        public CLayer.BookingExternalInventory GetExternalBookingInvetoryCanceldetById(long bookinBookingExtInvReqId, string ReservattionId)
        {
            CLayer.BookingExternalInventory result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pbookinBookingExtInvReqId", DataPlug.DataType._Varchar, bookinBookingExtInvReqId));
            param.Add(Connection.GetParameter("pReservattionId", DataPlug.DataType._Varchar, ReservattionId));
            DataTable dt = Connection.GetTable("GetExternalBookingInvetoryCanceldet_ByIdResId", param);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.BookingExternalInventory()
                {
                    BookingExtInvCancelId = Connection.ToLong(dt.Rows[0]["BookingExtInvCancelId"]),
                    BookingExtInvReqId = Connection.ToLong(dt.Rows[0]["BookingExtInvReqId"]),
                    BookingId = Connection.ToLong(dt.Rows[0]["Booking_Id"]),                    
                    reservation_id = Connection.ToString(dt.Rows[0]["Reservation_Id"]),
                    Cancellation_Status = Connection.ToInteger(dt.Rows[0]["Cancellation_Status"]),
                    Cancelled_Date = Connection.ToString(dt.Rows[0]["Cancelled_Date"]),
                    Cancellation_Number = Connection.ToString(dt.Rows[0]["Cancellation_Number"])
                };
            }
            return result;
        }
        public List<CLayer.BookingExternalInventory> GetExternalInventoryReqDetailsByBookingId(long BookingId)
        {

            List<CLayer.BookingExternalInventory> result = new List<CLayer.BookingExternalInventory>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Varchar, BookingId));
            DataTable dt = Connection.GetTable("GetExternalInventoryReqDetailsByBookingId", param);

            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.BookingExternalInventory()
                {
                    BookingExtInvReqId = Connection.ToLong(dr["BookingExtInvReqId"]),
                    BookingId = Connection.ToLong(dr["Booking_Id"]),
                    hotel_id = Connection.ToString(dr["Hotel_Id"]),
                    Reference_Id = Connection.ToString(dr["Reference_Id"]),
                    reservation_id = Connection.ToString(dr["Reservation_Id"]),
                    BookingStatus = Connection.ToInteger(dr["Status"]),
                    StatusDetails = Connection.ToString(dr["StatusDetails"]),
                    ReservationStatus = Connection.ToInteger(dr["Reservation_Status"]),
                    CheckInDate = Connection.ToString(dr["CheckIn_date"]),
                    CheckOutDate = Connection.ToString(dr["CheckOut_date"]),
                    CustomerId = Connection.ToLong(dr["Customer_Id"]),
                    IpAddtress = Connection.ToString(dr["Ip_Address"]),
                    roomtype_id = Connection.ToString(dr["RoomId"]),
                    room_name = Connection.ToString(dr["RoomName"]),
                    room_desc = Connection.ToString(dr["RoomDesc"]),
                    final_price_at_bookingamt = Connection.ToDecimal(dr["FinalPriceAtBookingAmt"]),
                    final_price_at_checkoutamt = Connection.ToDecimal(dr["FinalPriceAtCheckoutAmt"]),
                    Response = Connection.ToString(dr["Response"]),
                    BookingApiType = Connection.ToInteger(dr["BookingApi_Type"]),
                    CreateDate = Connection.ToString(dr["CreatedDate"]),
                    Cancellation_Status = Connection.ToInteger(dr["Cancellaton_Status"])
                });
            }
           
            return result;
        }



        public CLayer.BookingExternalInventory getAllDetailsfromExternalRequest(long BookingExtInvReqId)
        {

            CLayer.BookingExternalInventory result = new CLayer.BookingExternalInventory();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingExtInvReqId", DataPlug.DataType._Varchar, BookingExtInvReqId));
            DataTable dt = Connection.GetTable("GetAllDetailsfromExternalRequest", param);

           if(dt.Rows.Count > 0)
            {
                result = new CLayer.BookingExternalInventory()
                {
                    BookingExtInvReqId = Connection.ToLong(dt.Rows[0]["BookingExtInvReqId"]),
                    BookingId = Connection.ToLong(dt.Rows[0]["Booking_Id"]),
                    hotel_id = Connection.ToString(dt.Rows[0]["Hotel_Id"]),
                    Reference_Id = Connection.ToString(dt.Rows[0]["Reference_Id"]),
                    reservation_id = Connection.ToString(dt.Rows[0]["Reservation_Id"]),
                    BookingStatus = Connection.ToInteger(dt.Rows[0]["Status"]),
                    StatusDetails = Connection.ToString(dt.Rows[0]["StatusDetails"]),
                    ReservationStatus = Connection.ToInteger(dt.Rows[0]["Reservation_Status"]),
                    CheckInDate = Connection.ToString(dt.Rows[0]["CheckIn_date"]),
                    CheckOutDate = Connection.ToString(dt.Rows[0]["CheckOut_date"]),
                    CustomerId = Connection.ToLong(dt.Rows[0]["Customer_Id"]),
                    IpAddtress = Connection.ToString(dt.Rows[0]["Ip_Address"]),
                    roomtype_id = Connection.ToString(dt.Rows[0]["RoomId"]),
                    room_name = Connection.ToString(dt.Rows[0]["RoomName"]),
                    room_desc = Connection.ToString(dt.Rows[0]["RoomDesc"]),
                    final_price_at_bookingamt = Connection.ToDecimal(dt.Rows[0]["FinalPriceAtBookingAmt"]),
                    final_price_at_checkoutamt = Connection.ToDecimal(dt.Rows[0]["FinalPriceAtCheckoutAmt"]),
                    Response = Connection.ToString(dt.Rows[0]["Response"]),
                    BookingApiType = Connection.ToInteger(dt.Rows[0]["BookingApi_Type"]),
                    CreateDate = Connection.ToString(dt.Rows[0]["CreatedDate"]),
                    Cancellation_Status = Connection.ToInteger(dt.Rows[0]["Cancellaton_Status"])
                };
            }
           
            return result;
        }

        public List<CLayer.BookingExternalInventory> GetExternalInventoryCancelDetailsByBookingId(long BookingId)
        {

            List<CLayer.BookingExternalInventory> result = new List<CLayer.BookingExternalInventory>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Varchar, BookingId));
            DataTable dt = Connection.GetTable("GetExternalInventoryCancelDetailsByBookingId", param);

            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.BookingExternalInventory()
                {
                    BookingExtInvCancelId = Connection.ToLong(dr["BookingExtInvCancelId"]),
                    BookingExtInvReqId = Connection.ToLong(dr["BookingExtInvReqId"]),
                    BookingId = Connection.ToLong(dr["Booking_Id"]),                    
                    reservation_id = Connection.ToString(dr["Reservation_Id"]),
                    Cancellation_Status = Connection.ToInteger(dr["Cancellation_Status"]),
                    Cancelled_Date = Connection.ToString(dr["Cancelled_Date"]),
                    Cancellation_Number = Connection.ToString(dr["Cancellation_Number"])                   
                });
            }
           
            return result;
        }
        public long SaveBookingSubmitResponse(CLayer.BookingExternalInventory data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._Varchar, data.BookingId));
            param.Add(Connection.GetParameter("pHotel_id", DataPlug.DataType._Varchar, data.hotel_id));
            param.Add(Connection.GetParameter("pReference_Id", DataPlug.DataType._Varchar, data.Reference_Id));
            param.Add(Connection.GetParameter("preservation_id", DataPlug.DataType._Varchar, data.reservation_id));
            param.Add(Connection.GetParameter("pBookingStatus", DataPlug.DataType._Varchar, data.BookingStatus));
            param.Add(Connection.GetParameter("pStatusDetails", DataPlug.DataType._Varchar, data.StatusDetails));
            param.Add(Connection.GetParameter("pReservationStatus", DataPlug.DataType._Varchar, data.ReservationStatus));
            param.Add(Connection.GetParameter("pCheckInDate", DataPlug.DataType._Varchar, data.CheckInDate));
            param.Add(Connection.GetParameter("pCheckOutDate", DataPlug.DataType._Varchar, data.CheckOutDate));

            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._Varchar, data.CustomerId));
            param.Add(Connection.GetParameter("pIpAddtress", DataPlug.DataType._Varchar, data.IpAddtress));

            param.Add(Connection.GetParameter("pRoomtype_id", DataPlug.DataType._Varchar, data.roomtype_id));
            param.Add(Connection.GetParameter("pRoom_name", DataPlug.DataType._Varchar, data.room_name));
            param.Add(Connection.GetParameter("pRoom_desc", DataPlug.DataType._Text, data.room_desc));

            param.Add(Connection.GetParameter("pFinal_price_at_bookingamt", DataPlug.DataType._Decimal, data.final_price_at_bookingamt));
            param.Add(Connection.GetParameter("pFinal_price_at_checkoutamt", DataPlug.DataType._Decimal, data.final_price_at_checkoutamt));

            param.Add(Connection.GetParameter("pResponse", DataPlug.DataType._Varchar, data.Response));
            param.Add(Connection.GetParameter("pBookingApiType", DataPlug.DataType._Varchar, data.BookingApiType));

            param.Add(Connection.GetParameter("pDomainId", DataPlug.DataType._Varchar, data.DomainId));
            param.Add(Connection.GetParameter("pPromotionId", DataPlug.DataType._Varchar, data.PromotionId));

            param.Add(Connection.GetParameter("pRatePlanId", DataPlug.DataType._Varchar, data.RatePlanId));
            param.Add(Connection.GetParameter("pquery_key", DataPlug.DataType._Varchar, data.query_key));

            param.Add(Connection.GetParameter("pBookingsessionId", DataPlug.DataType._Varchar, data.BookingsessionId));
            param.Add(Connection.GetParameter("pBookingrequestId", DataPlug.DataType._Varchar, data.BookingrequestId));

            param.Add(Connection.GetParameter("pCancellation_Status", DataPlug.DataType._Varchar, data.Cancellation_Status));

            object result = Connection.ExecuteQueryScalar("BookingExternalInventoryRequest_save", param);
            return Connection.ToInteger(result);
        }


        public int GetExternalInventoryReqByBookingId(long BookingId)
        {

            string sql = "Select count(*) from bookingexternalinventoryrequest where Booking_Id=" + BookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }
        
       
        public long GetPropertyIdByBookingId(long BookingId)
        {

            string sql = "SELECT   p.propertyid FROM booking b INNER JOIN bookingItems bi ON b.BookingId = bi.BookingId  INNER JOIN accommodation a ON bi.accommodationId =  a.accommodationId " +
                " INNER JOIN property p ON a.propertyId= p.propertyId WHERE b.BookingId= " + BookingId.ToString() + " GROUP BY b.BookingId LIMIT 1 ";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        //*this is for getting property inventry id of offlinebooking id 
        //*Done by rahul on 06-02-2020
        public long GetPropertyIdByOfflineBookingId(long BookingId)
        {

            string sql = "SELECT p.propertyid FROM offline_bookings ofb INNER JOIN bookingdetails bd ON ofb.Offline_BookingId = bd.Offline_BookingId  INNER JOIN accommodation a ON bd.AccommodatoinTypeId =  a.AccommodationTypeId " +
                " INNER JOIN property p ON a.propertyId= p.propertyId WHERE ofb.Offline_BookingId= " + BookingId.ToString() + " GROUP BY ofb.Offline_BookingId LIMIT 1 ";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        //**End Here**
        public List<CLayer.BookingExternalInventory> GetAllForSearch(string searchString, int searchItem, int start, int limit, int Status)
        {
            List<CLayer.BookingExternalInventory> bookings = new List<CLayer.BookingExternalInventory>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));

            DataSet ds = Connection.GetDataSet("BookingExternalInventoryRequest_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.BookingExternalInventory()
                {
                    hotel_id = Connection.ToString(dr["Hotel_Id"]),
                    hotel_name = Connection.ToString(dr["PropertyName"]),
                    roomtype_id = Connection.ToString(dr["RoomId"]),
                    room_name = Connection.ToString(dr["RoomName"]),
                    room_desc = Connection.ToString(dr["RoomDesc"]),
                    Reference_Id = Connection.ToString(dr["Reference_Id"]),
                    reservation_id = Connection.ToString(dr["Reservation_Id"]),
                    final_price_at_bookingamt = Connection.ToDecimal(dr["FinalPriceAtBookingAmt"]),
                    final_price_at_checkoutamt = Connection.ToDecimal(dr["FinalPriceAtCheckoutAmt"]),
                    IpAddtress = Connection.ToString(dr["Ip_Address"]),
                    PromotionId = Connection.ToString(dr["PromotionId"]),
                    query_key = Connection.ToString(dr["Query_Key"]),
                    DomainId = Connection.ToString(dr["DomainId"]),
                    CreateDate = Connection.ToString(dr["CreatedDate"]),
                    CustomerId = Connection.ToLong(dr["Customer_Id"]),
                    BookingApiType = Connection.ToInteger(dr["BookingApi_Type"]),
                    BookingExtInvReqId = Connection.ToLong(dr["BookingExtInvReqId"]),
                    BookingId = Connection.ToLong(dr["Booking_Id"]),
                    BookingStatus = Connection.ToInteger(dr["Status"]),
                    ReservationStatus = Connection.ToInteger(dr["Reservation_Status"]),

                    RatePlanId = Connection.ToString(dr["RatePlanId"]),

                    CheckInDate = Connection.ToString(dr["CheckIn_date"]),
                    CheckOutDate = Connection.ToString(dr["CheckOut_date"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                   
                });
            }
            return bookings;
        }

        public void UpdateCancellationStatus(long BookingExtInvReqId, int CacelSts)
        {
            string sql = "Update bookingexternalinventoryrequest  Set Cancellaton_Status=" + CacelSts + " where BookingExtInvReqId = " + BookingExtInvReqId;
            Connection.ExecuteSqlQuery(sql);
        }

    
    }
}
