using System;
using System.Collections.Generic;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class OfflinePayment : DataLink
    {
        public long SaveInitialPaymentData(CLayer.OfflinePayment data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._Int, data.UserId));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, data.Name));
            param.Add(Connection.GetParameter("pRefNumber", DataPlug.DataType._Varchar, data.ReferenceNumber));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pMessage", DataPlug.DataType._Varchar, data.Message));

            param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, data.Address));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, data.Email));
            param.Add(Connection.GetParameter("pMobile", DataPlug.DataType._Varchar, data.Mobile));
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._Int, data.CountryId));
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._Int, data.StateId));
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, data.CityId));
            param.Add(Connection.GetParameter("pCityName", DataPlug.DataType._Varchar, data.City));
            param.Add(Connection.GetParameter("pZipCode", DataPlug.DataType._Varchar, data.ZipCode));
            param.Add(Connection.GetParameter("pGatewaytype", DataPlug.DataType._Int, data.Gatewaytype));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.OfflinePyamentStatus.Processing));
            object result = Connection.ExecuteQueryScalar("offlineBookingPayment_Save", param);
            return Connection.ToLong(result);
        }
        public void SaveOfflineTras(CLayer.OfflineTransaction data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPaymentId", DataPlug.DataType._Varchar, data.PaymentId));
            param.Add(Connection.GetParameter("pOfflinePaymentId", DataPlug.DataType._BigInt, data.OfflinePaymentId));
            param.Add(Connection.GetParameter("pTransactionId", DataPlug.DataType._Varchar, data.TransactionId));
            param.Add(Connection.GetParameter("pIsFlagged", DataPlug.DataType._Bool, data.IsFlagged));
            param.Add(Connection.GetParameter("pResponseCode", DataPlug.DataType._Varchar, data.ResponseCode));
            param.Add(Connection.GetParameter("pPaymentType", DataPlug.DataType._Varchar, data.PaymentType));
            param.Add(Connection.GetParameter("pTransactionType", DataPlug.DataType._Varchar, (int)CLayer.ObjectStatus.TransactionType.Payment));
            param.Add(Connection.GetParameter("pNotes", DataPlug.DataType._Varchar, data.Notes));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pDateCreated", DataPlug.DataType._DateTime, data.DateCreated));

            Connection.ExecuteQuery("OfflinePaymenttransaction_Save", param);

        }

        public List<CLayer.OfflinePayment> ReportOfflinePayment_GetAll(int status, string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.OfflinePayment> bookings = new List<CLayer.OfflinePayment>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("Report_OfflinePaymentGetall", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.OfflinePayment()
                {
                    LoginUsername = Connection.ToString(dr["LoginUsername"]),
                    Name = Connection.ToString(dr["PayUserName"]),
                    Amount = Connection.ToDecimal(dr["PayAmount"]),
                    Message = Connection.ToString(dr["Message"]),
                    ReferenceNumber = Connection.ToString(dr["PayUserRefNo"]),
                    PaymentRefNo = Connection.ToString(dr["PaymentReferenceNo"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    State = Connection.ToString(dr["State"]),
                    City = Connection.ToString(dr["City"]),
                    Email = Connection.ToString(dr["Email"]),
                    Country = Connection.ToString(dr["CountryName"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    OfflinePaymentStatus = Connection.ToInteger(dr["OfflinePaymentStatus"]),
                    OfflinePaymentId = Connection.ToInteger(dr["OfflinePaymentId"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                });
            }
            return bookings;
        }

        public bool IsExist(string paymentId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPaymentId", DataPlug.DataType._Varchar, paymentId));
            object result = Connection.ExecuteQueryScalar("Offlinetransaction_IsExist", param);
            return Connection.ToInteger(result) > 0;
        }
        public void SetStatus(int status, long OfflinePayId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflinePayId", DataPlug.DataType._BigInt, OfflinePayId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            Connection.ExecuteQuery("OfflinePayment_SetStatus", param);
        }
        public long SetPaymentRefNo(long OfflinePaymentId,string PaymentRefNo)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflinePaymentId", DataPlug.DataType._Int, OfflinePaymentId));
            param.Add(Connection.GetParameter("pPaymentRefNo", DataPlug.DataType._Varchar, PaymentRefNo));
            object result = Connection.ExecuteQueryScalar("sp_offlineCustomerPayment_SetPaymentRefNo", param);
            return Connection.ToLong(result);
        }
        public long SetCustomerPaymentRefNo(long OfflinePaymentId, string PaymentRefNo, Guid PaymentGuid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflinePaymentId", DataPlug.DataType._Int, OfflinePaymentId));
            param.Add(Connection.GetParameter("pPaymentRefNo", DataPlug.DataType._Varchar, PaymentRefNo));
            param.Add(Connection.GetParameter("pPaymentlinkid", DataPlug.DataType._Char, PaymentGuid));
            object result = Connection.ExecuteQueryScalar("sp_offlineCustomerPayment_SetPaymentRefNo", param);
            return Connection.ToLong(result);
        }
        public CLayer.OfflinePayment GetOfflinePaymentDetails(long OfflinePaymentId)
        {
            CLayer.OfflinePayment result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflinePayId", DataPlug.DataType._BigInt, OfflinePaymentId));
            DataTable  dt = Connection.GetTable("OfflinePayment_GetDetails", param);
            if (dt.Rows.Count > 0)
            {

                result = new CLayer.OfflinePayment();
                result.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                result.Amount = Connection.ToDecimal(dt.Rows[0]["Amount"]);
                result.Name = Connection.ToString(dt.Rows[0]["Name"]);
                result.Message = Connection.ToString(dt.Rows[0]["Message"]);
                result.ReferenceNumber = Connection.ToString(dt.Rows[0]["ReferenceNo"]);
                result.PaymentRefNo = Connection.ToString(dt.Rows[0]["PaymentReferenceNo"]);
                result.CreatedDate = Connection.ToDate(dt.Rows[0]["CreatedDate"]);
                result.Email = Connection.ToString(dt.Rows[0]["Email"]);
                result.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.Address = Connection.ToString(dt.Rows[0]["Address"]);
                result.Country = Connection.ToString(dt.Rows[0]["CountryName"]);
                result.State = Connection.ToString(dt.Rows[0]["StateName"]);
                result.City = Connection.ToString(dt.Rows[0]["CityName"]);
                result.CountryCode = Connection.ToString(dt.Rows[0]["CountryCode"]);
                result.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                //result.PaymentLinkID = (Guid)(dt.Rows[0]["PaymentLinkGuidId"]);
                result.PaymentLinkID = Connection.ToString(dt.Rows[0]["PaymentLinkGuidId"]);
              
            }
            return result;
        }
        public List<CLayer.OfflinePayment> GetOfflinePaymentDetails2(long OfflinePaymentId)
        {
            List<CLayer.OfflinePayment> result = new List<CLayer.OfflinePayment>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflinePayId", DataPlug.DataType._BigInt, OfflinePaymentId));
            DataSet ds = Connection.GetDataSet("OfflinePayment_GetDetails", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                result.Add(new CLayer.OfflinePayment()
                { 
                   //result = new CLayer.OfflinePayment(),
                    UserId = Connection.ToLong(dr["UserId"]),
                    Amount = Connection.ToDecimal(dr["Amount"]),
                    Name = Connection.ToString(dr["Name"]),
                    Message = Connection.ToString(dr["Message"]),
                    ReferenceNumber = Connection.ToString(dr["ReferenceNo"]),
                    PaymentRefNo = Connection.ToString(dr["PaymentReferenceNo"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    Email = Connection.ToString(dr["Email"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    Address = Connection.ToString(dr["Address"]),
                    Country = Connection.ToString(dr["CountryName"]),
                    State = Connection.ToString(dr["StateName"]),
                    City = Connection.ToString(dr["CityName"]),
                    CountryCode = Connection.ToString(dr["CountryCode"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                //result.PaymentLinkID = (Guid)(dt.Rows[0]["PaymentLinkGuidId"]);
                PaymentLinkID = Connection.ToString(ds.Tables[0].Rows[0]["PaymentLinkGuidId"])
                });
            }   
            return result;
        }
        public List<CLayer.OfflinePayment> GetOfflinePaymentDetails1(long OfflinePaymentId)
        {
            List<CLayer.OfflinePayment> bookings = new List<CLayer.OfflinePayment>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pOfflinePayId", DataPlug.DataType._BigInt, OfflinePaymentId));

            DataSet ds = Connection.GetDataSet("Sp_Confirmation_Mail", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bookings.Add(new CLayer.OfflinePayment()
                {
                    //OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]),

                    //CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    //CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    //CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
                    //CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    //CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    //CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),
                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),

                    PayableSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]) - Connection.ToDecimal(dr["AdvanceReceived"]),
                    SumTotalSalePrice = Connection.ToDecimal(dr["SumofSalePrice"]),
                    //SumofAdvanceReceived = Connection.ToDecimal(dr["SumofAdvanceReceived"]),
                    AdvanceReceived = Connection.ToDecimal(dr["AdvanceReceived"]),
                    PaymentLinkID = Connection.ToString(ds.Tables[0].Rows[0]["PaymentLinkID"]),
                    //PaymentLinkStatus = Connection.ToString(dr["SendCustomerPaymentLinkStatus"]),
                    GuestName = Connection.ToString(dr["GuestName"]),
                    NoOfRooms = Connection.ToInteger(dr["NoOfRooms"]),
                    //PaymentLinkID = Connection.ToString(dr["PaymentLinkId"])
                });
            }
            return bookings;
        }
        public string GetOfflinePaymentGuid(long OfflinePaymentId)
        {
            //string result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflinePayId", DataPlug.DataType._BigInt, OfflinePaymentId));
            DataTable dt = Connection.GetTable("OfflinePayment_GetCustomerGUid", param);
            object result = Connection.ExecuteQueryScalar("OfflinePayment_GetCustomerGUid", param);
            return Connection.ToString(result);
        }
        public long GetOfflinePayByRefNo(string OfflinePayRefNo)
        {
            List<long> ids = new List<long>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflinePayRefNo", DataPlug.DataType._Varchar, OfflinePayRefNo));
            //param.Add(Connection.GetParameter("pDeleteStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.BookingStatus.Deleted));
            object result = Connection.ExecuteQueryScalar("offlinepayment_GetAllByRefNo", param);
            return Connection.ToLong(result);
        }
        public long SaveInitialCustomerPaymentData(CLayer.OfflinePayment data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._Int, data.UserId));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, data.Name));
            param.Add(Connection.GetParameter("pRefNumber", DataPlug.DataType._Varchar, data.ReferenceNumber));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pMessage", DataPlug.DataType._Varchar, data.Message));

            param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, data.Address));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, data.Email));
            param.Add(Connection.GetParameter("pMobile", DataPlug.DataType._Varchar, data.Mobile));
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._Int, data.CountryId));
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._Int, data.StateId));
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, data.CityId));
            param.Add(Connection.GetParameter("pCityName", DataPlug.DataType._Varchar, data.City));
            param.Add(Connection.GetParameter("pZipCode", DataPlug.DataType._Varchar, data.ZipCode));
            param.Add(Connection.GetParameter("pGatewaytype", DataPlug.DataType._Int, data.Gatewaytype));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.OfflinePyamentStatus.Processing));
            param.Add(Connection.GetParameter("pPaymentlinkid", DataPlug.DataType._Char, data.CustomerGuid));
            object result = Connection.ExecuteQueryScalar("SP_offlineCustomerPayment_Save", param);
            return Connection.ToLong(result);
        }
        public long GetOfflinePaymentUserID(Guid PaymentGuid)
        {
        
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPaymentGuid", DataPlug.DataType._Char, PaymentGuid));
            //param.Add(Connection.GetParameter("pDeleteStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.BookingStatus.Deleted));
            object result = Connection.ExecuteQueryScalar("sp_offlinepayment_GetUserID", param);
            return Connection.ToLong(result);
        }

    }
}
