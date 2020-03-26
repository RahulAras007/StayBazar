using System;
using System.Collections.Generic;
using DataLayer.DataProvider;
using System.Data;


namespace DataLayer
{
   public class BookingHistory : DataLink
    {
       public List<CLayer.Booking> GetBookingHistoryForSupplier(int status, long userId, int days, int Start, int Limit, int Type)
       {
           List<CLayer.Booking> result = new List<CLayer.Booking>();
           List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
           param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
           param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
           param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
           param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
           param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
           param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
           DataSet ds = Connection.GetDataSet("supplier_GetChart", param);
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

               });
           }
           return result;

       }

        //For User History
       public List<CLayer.Booking> GtBookingHistoryForUser(int status, long userId, int days, int Start, int Limit, int Type)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            DataSet ds = Connection.GetDataSet("bookinghistory_GetOnUser", param);
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
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"])
                });
            }
            return result;

        }

        public List<CLayer.Booking> GtBookingApprovalHistoryForUser(int status, long userId, int days, int Start, int Limit, int Type)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            DataSet ds = Connection.GetDataSet("bookingPendingApprovals_GetOnUser", param);
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

        public List<CLayer.Booking> GtBookingApprovalHistoryForCorporate(int status, long userId, int days, int Start, int Limit, int Type)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            DataSet ds = Connection.GetDataSet("bookingPendingApprovalsForCorporate_GetOnUser", param);
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

        //Added by rahul for displaying the list of offlinebooking in Booking Approval for corporate on 06-02-2020
        public List<CLayer.Booking> GtOfflineBookingApprovalHistoryForCorporate(int status, long userId, int days, int Start, int Limit, int Type)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            DataSet ds = Connection.GetDataSet("offlinebookingPendingApprovalsForCorporate_GetOnUser", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Booking()
                {
                    PropertyTitle = Connection.ToString(dr["PropertyTitle"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    AccommodationTypeTitle = Connection.ToString(dr["AccommodationTypeTitle"]),
                    propertyAddress = Connection.ToString(dr["propertyAddress"]),
                    BookingId = Connection.ToLong(dr["offline_bookingid"]),
                    BookingDate = Connection.ToDate(dr["CreatedDate"]),
                    bookingTotalAmount = Connection.ToDecimal(dr["BookingTotalAmount"]),
                    bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfRooms"]),
                    CheckIn = Connection.ToDate(dr["CheckIn_date"]),
                    CheckOut = Connection.ToDate(dr["CheckOut_date"]),
                    BookingStatus = Connection.ToInteger(dr["SaveStatus"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    AccommoLocation = Connection.ToString(dr["AccommoLocation"]),
                    day = Connection.ToInteger(dr["Noofnight"]),
                    NoOfAdults = Connection.ToInteger(dr["NoOfPaxAdult"]),
                    NoOfChildren = Connection.ToInteger(dr["NoOfPaxChild"]),
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
                    Status = Connection.ToInteger(dr["SaveStatus"]),
                    propertyPin = Connection.ToString(dr["propertyPin"]),
                    propertyEmail = Connection.ToString(dr["propertyEmail"]),
                    propertyCity = Connection.ToString(dr["propertyCity"]),
                    approval_order = Connection.ToInteger(dr["approval_order"]),
                    approval_status = Connection.ToInteger(dr["approval_status"]),
                    booking_approval_id = Connection.ToInteger(dr["booking_approval_id"]),
                    Approver_id = Connection.ToInteger(dr["Approver_id"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"])

                });
            }
            return result;

        }






        public List<CLayer.Booking> GtBookingApprovalHistoryDetails(int status, long userId, int days, int Start, int Limit, int Type)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            DataSet ds = Connection.GetDataSet("bookingApprovalHistory_GetOnUser", param);
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

        public List<CLayer.Booking> GtBookingApprovalHistoryDetailsForCoroporateAdmin(int status, long userId, int days, int Start, int Limit, int Type)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            DataSet ds = Connection.GetDataSet("bookingApprovalHistory_CorporateAdmin", param);
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


        public List<CLayer.Booking> GtbookingApprovedOrRejectedBookings(int status, long userId, int days, int Start, int Limit, int Type)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            DataSet ds = Connection.GetDataSet("bookingApprovedOrRejectedBookings_GetOnUser", param);
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
                    RejectionNote= Connection.ToString(dr["RejectionNote"]),


                });
            }
            return result;

        }

        public List<CLayer.Booking> GtbookingCancelledOrFailedOrRejectedBookings(int status, long userId, int days, int Start, int Limit, int Type)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pDays", DataPlug.DataType._Int, days));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            DataSet ds = Connection.GetDataSet("bookingCancelledOrFailedOrRejectedBookings_GetOnUser", param);
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
                    RejectionNote = Connection.ToString(dr["RejectionNote"]),


                });
            }
            return result;

        }


        public List<CLayer.Booking> GetCreditBookForUser(int status, long userId, int Start, int Limit)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("bookinghistory_GetCreditBookForUser", param);
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
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"])
                });
            }
            return result;

        }


       public List<CLayer.Booking> GetOtherBookForUser(int status, long userId, int Start, int Limit)
        {
            List<CLayer.Booking> result = new List<CLayer.Booking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("bookinghistory_GetOtherBookForUser", param);
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
                    PartialPaymentStatus = Connection.ToInteger(dr["PartialPaymentStatus"])
                });
            }
            return result;

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
                    Firstname = Connection.ToString(dr["ForFirstname"]),
                    Lastname = Connection.ToString(dr["ForLastname"]),
                    AddressText = Connection.ToString(dr["ForAddress"]),
                    City = Connection.ToString(dr["ForCity"]),
                    Country = Connection.ToString(dr["ForCountry"]),
                    StateName = Connection.ToString(dr["ForState"]),
                    //Phone = Connection.ToString(dr["ForPhone"]),
                   // Mobile = Connection.ToString(dr["ForMobile"])
                });
            }
            return bookings;
        }

      

    }
}
