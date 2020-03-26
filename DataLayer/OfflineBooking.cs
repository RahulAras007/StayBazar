using CLayer;
using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public class OfflineBooking : DataLink
    {
        public bool HasSupplierPayment(string orderNo)
        {
            string sql = "SELECT COUNT(*) FROM suplierpayment_booking WHERE bookingreference='" + orderNo + "'";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return (Connection.ToInteger(obj) > 0);
        }
        public CLayer.HSNCode GetBookingTypeHSN(long offlinebookingId)
        {
            CLayer.HSNCode result = new CLayer.HSNCode() { Code = "", CodeId = 0, NatureOfService = "" };
            string sql = "select h.* from hsncode h inner join offline_bookings o on h.CodeId = o.HSNCodeId where o.offline_bookingid =" + offlinebookingId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);

            if (dt.Rows.Count > 0)
                if (dt.Rows.Count > 0)
                {
                    result.CodeId = Connection.ToLong(dt.Rows[0]["CodeId"]);
                    result.Code = Connection.ToString(dt.Rows[0]["HSNCode"]);
                    result.NatureOfService = Connection.ToString(dt.Rows[0]["NatureOfService"]);
                }
            return result;
        }
        public void FixAmounts(long offlineBookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, offlineBookingId));
            Connection.ExecuteQuery("offline_booking_AmountFix", param);
        }
        public int GetBillingEntityState(long offlineBookingId)
        {
            string sql = "select sb.GSTStateId from offline_bookings ob inner join sbentity sb on ob.SBEntityEntityIdBilling = sb.EntityId Where ob.offline_bookingId=" + offlineBookingId.ToString();
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar(sql));
        }
        public int GetTaxType(long offlineBookingId)
        {
            string sql = "SELECT IFNULL(isgst,1) AS tax FROM offline_bookings WHERE offline_bookingid=" + offlineBookingId.ToString();
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar(sql));
        }
        public int GetBookingType(long offlineBookingId)
        {
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar("Select BookingType from offline_bookings where offline_bookingid=" + offlineBookingId.ToString()));
        }
        public void SaveBookingTypeData(long bookingId, int bookingType, double gst, double tacAmount, double directAmount, bool gstIncluded, double percent, int hsnCodeId)
        {
            string sql = "Update offline_bookings Set HSNCodeId=" + hsnCodeId.ToString() + ",TACAmount=" + tacAmount.ToString();
            sql = sql + ",BookingType=" + bookingType.ToString() + ",TACGSTIncluded=" + gstIncluded.ToString() + ",DirectPercent=" + percent.ToString();
            sql = sql + ",DirectAmount=" + directAmount.ToString() + ",BookingTypeGST=" + gst.ToString() + "  where offline_bookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }

        public CLayer.OfflineBooking GetBookingTypeData(long offlineBookingId)
        {
            CLayer.OfflineBooking data = null;
            // direct sql statements 
            string sql = "Select HSNCodeId,TACAmount,BookingType,TACGSTIncluded,DirectPercent,DirectAmount,BookingTypeGST From offline_bookings Where offline_bookingid=" + offlineBookingId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);

            if (dt.Rows.Count > 0)
            {
                data = new CLayer.OfflineBooking();
                data.OfflineBookingId = offlineBookingId;
                data.BookingType = (CLayer.ObjectStatus.OfflineBookingType)Connection.ToInteger(dt.Rows[0]["BookingType"]);
                data.BookingTypeTAC = Connection.ToDouble(dt.Rows[0]["TACAmount"]);
                data.BookingTypeAmount = Connection.ToDouble(dt.Rows[0]["DirectAmount"]);
                data.BookingTypePercent = Connection.ToDouble(dt.Rows[0]["DirectPercent"]);
                data.BookingTypeGSTIncluded = Connection.ToBoolean(dt.Rows[0]["TACGSTIncluded"]);
                data.BookingTypeGST = Connection.ToDouble(dt.Rows[0]["BookingTypeGST"]);
                data.HSNCodeCodeID = Connection.ToInteger(dt.Rows[0]["HSNCodeId"]);
                
            }

            return data;
        }

        public List<KeyValuePair<string, double>> DefaultBookingTypeTaxes()
        {
            List<KeyValuePair<string, double>> taxes = new List<KeyValuePair<string, double>>();
            string sql = "SELECT offline_bookingid FROM offline_bookingtype_taxes ORDER BY offline_bookingid DESC LIMIT 1";
            long id = Connection.ToLong(Connection.ExecuteSQLQueryScalar(sql));
            if (id > 0)
            {
                sql = "SELECT * FROM offline_bookingtype_taxes WHERE offline_bookingId =" + id.ToString();
                DataTable dt = Connection.GetSQLTable(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    taxes.Add(new KeyValuePair<string, double>(Connection.ToString(dr["Title"]), Connection.ToDouble(dr["Percent"])));
                }
            }

            return taxes;
        }

        

        public void AddBookingTypeTaxes(long offlineBookingId, List<KeyValuePair<string, double>> taxes)
        {
            string sql = "DELETE FROM offline_bookingtype_taxes WHERE offline_bookingid = " + offlineBookingId.ToString();
            // clear this bookings tax first
            Connection.ExecuteSqlQuery(sql);
            //now add updated taxes
            foreach (KeyValuePair<string, double> item in taxes)
            {
                if (item.Value <= 0) continue;
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, offlineBookingId));
                param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, item.Key));
                param.Add(Connection.GetParameter("pPercent", DataPlug.DataType._Decimal, item.Value));

                Connection.ExecuteQuery("Offline_BookingType_AddTax", param);
            }
        }

        public List<KeyValuePair<string, double>> GetBookingTypeTaxes(long bookingId)
        {
            List<KeyValuePair<string, double>> taxes = new List<KeyValuePair<string, double>>();
            DataTable dt = Connection.GetSQLTable("Select Title,Percent from offline_bookingtype_taxes where offline_bookingid=" + bookingId);
            foreach (DataRow dr in dt.Rows)
            {
                taxes.Add(new KeyValuePair<string, double>(Connection.ToString(dr["Title"]), Connection.ToDouble(dr["Percent"])));
            }
            return taxes;
        }
        
        public DataTable SupplierPaymentPendingReport(DateTime fromdate,DateTime todate)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("fromdate", DataPlug.DataType._DateTime, fromdate));
            param.Add(Connection.GetParameter("todate", DataPlug.DataType._DateTime, todate));

            DataTable val = Connection.GetTable("Report_SupplierPaymentPending", param);

            return val;
        }

        public bool CanSendInvoiceMail(string name, string email, int type)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, email));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Varchar, type));

            object val = Connection.ExecuteQueryScalar("OfflineBooking_NoInvoiceMail", param);

            return (Connection.ToInteger(val) == 0);
        }

        public CLayer.OfflineBooking SetOfflinePricingDetails(CLayer.OfflineBooking data)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, data.SupplierId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pAccommodationid", DataPlug.DataType._BigInt, data.Accommodationid));
            param.Add(Connection.GetParameter("pAccommodationtypeid", DataPlug.DataType._BigInt, data.Accommodationtypeid));
            param.Add(Connection.GetParameter("pStayCategoryid", DataPlug.DataType._BigInt, data.StayCategoryid));
            param.Add(Connection.GetParameter("pNoOfNight", DataPlug.DataType._Varchar, data.NoOfNight));
            param.Add(Connection.GetParameter("pNoOfRooms", DataPlug.DataType._Varchar, data.NoOfRooms));
            param.Add(Connection.GetParameter("pNoOfPaxAdult", DataPlug.DataType._Varchar, data.NoOfPaxAdult));
            param.Add(Connection.GetParameter("pNoOfPaxChild", DataPlug.DataType._Varchar, data.NoOfPaxChild));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._DateTime, data.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._DateTime, data.CheckOut));

            DataTable dt = Connection.GetTable("OfflineBooking_SetPriceDetails", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.TotalAmtForBuyPrice = Connection.ToDecimal(dt.Rows[0]["TotalAmountaccnbuyprice"]);
                result.TotalAmtotherForBuyPrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtotherForBuyPrice"]);
                result.AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dt.Rows[0]["DailyRateWOutStaxbuyprice"]);
                result.BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dt.Rows[0]["BuyPriceforotherservicesForBuyprice"]);
                result.StaxForBuyPrice = Connection.ToDecimal(dt.Rows[0]["ServiceTaxbuyprice"]);
                result.StaxForotherBuyPrice = Connection.ToDecimal(dt.Rows[0]["StaxForotherBuyPrice"]);

                result.TotalAmtForSalePrice = Connection.ToDecimal(dt.Rows[0]["TotalAmountaccnsaleprice"]);
                result.TotalAmtotherForSalePrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtotherForSalePrice"]);
                result.AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dt.Rows[0]["DailyRateWOutStaxsaleprice"]);
                result.BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dt.Rows[0]["BuyPriceforotherservicesForSalePrice"]);
                result.StaxForSalePrice = Connection.ToDecimal(dt.Rows[0]["ServiceTaxsaleprice"]);
                result.StaxForotherForSalePrice = Connection.ToDecimal(dt.Rows[0]["StaxForotherForSalePrice"]);
            }
            return result;
        }


        public CLayer.OfflineBooking GetOtherAmountsForOfflineBooking(long OfflineBookingId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            DataTable dt = Connection.GetTable("OfflineBooking_GetOtherAmountsIncluded", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.ReimbursementsAmount = Connection.ToDecimal(dt.Rows[0]["Reimbursements"]);
                result.DiscountAmount = Connection.ToDecimal(dt.Rows[0]["Discount"]);
                result.OthersAmount = Connection.ToDecimal(dt.Rows[0]["Others"]);
                result.natureofreimbursements = Connection.ToString(dt.Rows[0]["NatureOfReimbursements"]);
                result.AdvanceReceived = Connection.ToDecimal(dt.Rows[0]["AdvanceReceived"]);
            }
            return result;
        }

        public void UpdateOtherAmountsForOfflineBooking(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pReimbursementsAmount", DataPlug.DataType._BigInt, data.ReimbursementsAmount));
            param.Add(Connection.GetParameter("pNatureofReimbursementsAmount", DataPlug.DataType._Varchar, data.natureofreimbursements));
            param.Add(Connection.GetParameter("pOthersAmount", DataPlug.DataType._BigInt, data.OthersAmount));
            param.Add(Connection.GetParameter("pDiscountAmount", DataPlug.DataType._BigInt, data.DiscountAmount));
            param.Add(Connection.GetParameter("pAdvanceReceived", DataPlug.DataType._BigInt, data.AdvanceReceived));
            DataTable dt = Connection.GetTable("OfflineBooking_UpdateOtherAmounts", param);
        }
        public void DeleteSupplierPaymentModeForOfflineBooking(int offlineBookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._Int, offlineBookingId));
            DataTable dt = Connection.GetTable("OfflineBooking_DeleteSupplierPayments", param);

        }

        public void UpdateSupplierPaymentModeForOfflineBooking(List<CLayer.SupplierPaymentSchedule> data)
        {

            foreach (CLayer.SupplierPaymentSchedule item in data)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pPaymentScheduleId", DataPlug.DataType._Int, item.PaymentscheduleId));
                param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._Int, item.BookingId));
                param.Add(Connection.GetParameter("pSupplierPaymentMode", DataPlug.DataType._Int, item.SupplierPaymentMode));

                if ((item.SupplierPaymentModeDate == null) || (item.SupplierPaymentModeDate.ToString().Contains("0001")))
                {
                    param.Add(Connection.GetParameter("pSupplierPaymentDate", DataPlug.DataType._DateTime, DBNull.Value));
                }
                else
                {
                    param.Add(Connection.GetParameter("pSupplierPaymentDate", DataPlug.DataType._DateTime, item.SupplierPaymentModeDate));
                }

                param.Add(Connection.GetParameter("pSupplierPaymentAmount", DataPlug.DataType._Decimal, item.SupplierPaymentAmount));
                param.Add(Connection.GetParameter("pSupplierCreditDays", DataPlug.DataType._Int, item.SupplierCreditDays));

                DataTable dt = Connection.GetTable("OfflineBooking_UpdateSupplierPaymentMode", param);
            }

        }
        public CLayer.OfflineBooking GetSupplierDetails(long id)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomPropertyId", DataPlug.DataType._BigInt, id));


            DataTable dt = Connection.GetTable("Supplier_DetailsByCustomPropertyId", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.SupplierName = Connection.ToString(dt.Rows[0]["SupplierName"]);
                result.SupplierAddress = Connection.ToString(dt.Rows[0]["SupplierAddress"]);
                result.SupplierCityname = Connection.ToString(dt.Rows[0]["SupplierCityname"]);
                result.SupplierCity = Connection.ToInteger(dt.Rows[0]["SupplierCity"]);
                result.SupplierState = Connection.ToInteger(dt.Rows[0]["SupplierState"]);
                result.SupplierCountry = Connection.ToInteger(dt.Rows[0]["SupplierCountry"]);
                result.SupplierPinCode = Connection.ToString(dt.Rows[0]["SupplierPinCode"]);

                result.SupplierMobile = Connection.ToString(dt.Rows[0]["SupplierMobile"]);
                result.SupplierEmail = Connection.ToString(dt.Rows[0]["SupplierEmail"]);
                result.AccountNumber = Connection.ToString(dt.Rows[0]["AccountNumber"]);
                result.BankName = Connection.ToString(dt.Rows[0]["BankName"]);

                result.BranchAddress = Connection.ToString(dt.Rows[0]["BranchAddress"]);
                result.AccountName = Connection.ToString(dt.Rows[0]["AccountName"]);
                result.AccountType = Connection.ToString(dt.Rows[0]["AccountType"]);
                result.IFSCcode = Connection.ToString(dt.Rows[0]["IFSCcode"]);
                result.CareTakerPhone = Connection.ToString(dt.Rows[0]["CareTakerPhone"]);
                result.CareTakerEmail = Connection.ToString(dt.Rows[0]["CareTakerEmail"]);
                result.BranchName = Connection.ToString(dt.Rows[0]["BranchName"]);
                result.MICRcode = Connection.ToString(dt.Rows[0]["MICRcode"]);
            }
            return result;
        }

        public CLayer.OfflineBooking GetCustomerGstRegNoByStateId(long Customerid, long Stateid, int status)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomerid", DataPlug.DataType._BigInt, Customerid));
            param.Add(Connection.GetParameter("pStateid", DataPlug.DataType._BigInt, Stateid));
            param.Add(Connection.GetParameter("pstatus", DataPlug.DataType._Int, status));
            DataTable dt = Connection.GetTable("GetCustomerGstRegNoByStateId", param);
            CLayer.OfflineBooking result = null;
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.SubCustomerGstStateId = Stateid;
                result.SubCustomerCity = Connection.ToInteger(dt.Rows[0]["CityId"]);
                result.SubCustomerCityname = Connection.ToString(dt.Rows[0]["CityName"]);
                result.SubCustomerAddress = Connection.ToString(dt.Rows[0]["Address"]);
                result.SubCustomerpinCode = Connection.ToString(dt.Rows[0]["PinCode"]);
                result.SubCustomerGstRegNo = Connection.ToString(dt.Rows[0]["GSTRegNO"]);
            }

            return result;
        }



        public CLayer.OfflineBooking GetOfflinegstDetailsById(long OffGSTId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOffGSTId", DataPlug.DataType._BigInt, OffGSTId));
            DataTable dt = Connection.GetTable("GetOfflinegstDetailsById", param);
            CLayer.OfflineBooking result = null;
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.SubCustomerCity = Connection.ToInteger(dt.Rows[0]["CityId"]);
                result.SubCustomerCityname = Connection.ToString(dt.Rows[0]["CityName"]);
                result.SubCustomerAddress = Connection.ToString(dt.Rows[0]["Address"]);
                result.SubCustomerpinCode = Connection.ToString(dt.Rows[0]["PinCode"]);
                result.SubCustomerGstRegNo = Connection.ToString(dt.Rows[0]["GSTRegNO"]);
            }

            return result;
        }


        public string GetPropertyGstRegNoByPropertyid(long Propertyid, int type)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyid", DataPlug.DataType._BigInt, Propertyid));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._BigInt, type));
            object result = Connection.ExecuteQueryScalar("GetPropertyGstRegNoByPropertyid", param);
            return Connection.ToString(result);
        }
        public string SavePropertyGstRegNoByPropertyid(long Propertyid, int type, string PropertyGstRegNo)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyid", DataPlug.DataType._BigInt, Propertyid));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._BigInt, type));
            param.Add(Connection.GetParameter("pPropertyGstRegNo", DataPlug.DataType._Varchar, PropertyGstRegNo));
            object result = Connection.ExecuteQueryScalar("SavePropertyGstRegNoByPropertyid", param);
            return Connection.ToString(result);
        }

        public List<CLayer.Invoice> GetDetailsNotSubmittedandGenerated()
        {
            List<CLayer.Invoice> bookings = new List<CLayer.Invoice>();
            DataTable dt = Connection.GetTable("Invoice_GetDetailsNotSubmittedandGenerated");
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.Invoice()
                {
                    InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]),
                    InvoiceDate = Connection.ToDate(dr["InvoiceDate"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    OfflineBookingId = Connection.ToInteger(dr["Offline_BookingId"]),
                    CheckStatus = Connection.ToString(dr["Inv_Sts"]),
                    IsGst = Connection.ToInteger(dr["IsGST"]),
                    BookingDetailsId = Connection.ToLong(dr["BookingDetailsId"])
                });
            }
            return bookings;
        }

        public int GetUserType(long offlineCustomerId)
        {
            string sql = "Select CustomerType From offlinebooking_customer Where offline_CustomerId=" + offlineCustomerId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }
        public CLayer.User GetCustomerPaymentMode(long Customerid, int type)
        {
            CLayer.User obj = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomerid", DataPlug.DataType._BigInt, Customerid));
            param.Add(Connection.GetParameter("pUsertype", DataPlug.DataType._Int, type));
            DataTable result = Connection.GetTable("GetCustomerPaymentMode", param);
            if (result != null)
            {
                if (result.Rows.Count > 0)
                {
                    foreach (DataRow dr in result.Rows)
                    {
                        obj = new CLayer.User();

                        obj.CustomerPaymentMode = Connection.ToInteger((dr["CustomerPaymentMode"]));
                        obj.CustomerPaymentModeCreditDays = Connection.ToDecimal(dr["CustomerPaymentModeCreditDays"]);
                    }
                }
            }
            return obj;
        }
        public List<CLayer.OfflineBooking> GetBookingDetailsAfterSubmitForCustomer()
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            DataTable dt = Connection.GetTable("Invoice_GetBookingDetailsAfterSubmitForCustomer");
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    CustomerName = Connection.ToString((dr["Name"].ToString())),
                    CustomerId = Connection.ToInteger(dr["Offline_CustomerId"]),
                    CustomerEmail = Connection.ToString(dr["Email"]),
                    OfflineBookingId = Connection.ToInteger(dr["Offline_BookingId"])
                });
            }
            return bookings;
        }
        public long SaveOfflineBookingCustomerNEWForUser(CLayer.OfflineBooking userdetails)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            var UserType = 0;
            if (userdetails.UserType == 5)
            {
                UserType = 5;
            }
            else
            {
                UserType = 7;
            }
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._Varchar, userdetails.OfflineBookingId));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, userdetails.CustomerName));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, userdetails.CustomerEmail));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, userdetails.CustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._Varchar, userdetails.CustomerCity));
            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._Varchar, userdetails.CustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._Varchar, userdetails.CustomerState));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, userdetails.CustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, userdetails.CustomerMobile));
            param.Add(Connection.GetParameter("pCustomerpinCode", DataPlug.DataType._Varchar, userdetails.CustomerpinCode));

            param.Add(Connection.GetParameter("pCustomerType", DataPlug.DataType._Varchar, UserType));
            param.Add(Connection.GetParameter("pBillingAddress", DataPlug.DataType._Varchar, userdetails.BillingAddress));
            param.Add(Connection.GetParameter("pBillingState", DataPlug.DataType._Varchar, userdetails.BillingState));
            param.Add(Connection.GetParameter("pBillingCity", DataPlug.DataType._Varchar, userdetails.BillingCity));
            param.Add(Connection.GetParameter("pPinCode", DataPlug.DataType._Varchar, userdetails.PinCode));
            param.Add(Connection.GetParameter("pBillingMobile", DataPlug.DataType._Varchar, userdetails.BillingMobile));
            param.Add(Connection.GetParameter("pContactPerson", DataPlug.DataType._Varchar, userdetails.ContactPerson));
            param.Add(Connection.GetParameter("pOfficeNO", DataPlug.DataType._Varchar, userdetails.OfficeNO));
            param.Add(Connection.GetParameter("pGuestName1", DataPlug.DataType._Varchar, userdetails.GuestName1));
            param.Add(Connection.GetParameter("pGuestEmail1", DataPlug.DataType._Varchar, userdetails.GuestEmail1));
            param.Add(Connection.GetParameter("pBillingCountryId", DataPlug.DataType._Varchar, userdetails.BillingCountryId));
            param.Add(Connection.GetParameter("pBillingCityname", DataPlug.DataType._Varchar, userdetails.BillingCityname));
            param.Add(Connection.GetParameter("pCustomerPaymentMode", DataPlug.DataType._Int, 0));
            param.Add(Connection.GetParameter("pCreditDays", DataPlug.DataType._Decimal, 0));


            object result = Connection.ExecuteQueryScalar("OfflineBooking_CustomerSaveNew", param);
            return Connection.ToLong(result);
        }


        public long SaveCustomerGSTState(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSubCustomerGstStateId", DataPlug.DataType._BigInt, data.SubCustomerGstStateId));
            param.Add(Connection.GetParameter("pCustomerGstRegNo", DataPlug.DataType._Varchar, data.SubCustomerGstRegNo));
            param.Add(Connection.GetParameter("pCustomerTableType", DataPlug.DataType._Int, data.CustomerTableType));
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._BigInt, data.SubCustomerid));
            param.Add(Connection.GetParameter("pSubCustomerAddress", DataPlug.DataType._Varchar, data.SubCustomerAddress));
            param.Add(Connection.GetParameter("pSubCustomerCity", DataPlug.DataType._BigInt, data.SubCustomerCity));
            param.Add(Connection.GetParameter("pSubCustomerCityname", DataPlug.DataType._Varchar, data.SubCustomerCityname));
            param.Add(Connection.GetParameter("pSubCustomerpinCode", DataPlug.DataType._Varchar, data.SubCustomerpinCode));
            object result = Connection.ExecuteQueryScalar("SaveCustomerGSTState", param);
            return Connection.ToLong(result);
        }
        public long SaveOfflineBookingCustomerNEW(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            var CustomerType = 0;
            if (data.CustomerType == 5)
            {
                CustomerType = 5;
            }
            else
            {
                CustomerType = 7;
            }
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._Varchar, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, data.CustomerName));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, data.CustomerEmail));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, data.CustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._Varchar, data.CustomerCity));
            param.Add(Connection.GetParameter("pCustomerpinCode", DataPlug.DataType._Varchar, data.CustomerpinCode));

            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._Varchar, data.CustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._Varchar, data.CustomerState));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, data.CustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, data.CustomerMobile));

            param.Add(Connection.GetParameter("pCustomerType", DataPlug.DataType._Varchar, CustomerType));
            param.Add(Connection.GetParameter("pBillingAddress", DataPlug.DataType._Varchar, data.BillingAddress));
            param.Add(Connection.GetParameter("pBillingState", DataPlug.DataType._Varchar, data.BillingState));
            param.Add(Connection.GetParameter("pBillingCity", DataPlug.DataType._Varchar, data.BillingCity));
            param.Add(Connection.GetParameter("pPinCode", DataPlug.DataType._Varchar, data.PinCode));
            param.Add(Connection.GetParameter("pBillingMobile", DataPlug.DataType._Varchar, data.BillingMobile));
            param.Add(Connection.GetParameter("pContactPerson", DataPlug.DataType._Varchar, data.ContactPerson));
            param.Add(Connection.GetParameter("pOfficeNO", DataPlug.DataType._Varchar, data.OfficeNO));
            param.Add(Connection.GetParameter("pGuestName1", DataPlug.DataType._Varchar, data.GuestName1));
            param.Add(Connection.GetParameter("pGuestEmail1", DataPlug.DataType._Varchar, data.GuestEmail1));
            param.Add(Connection.GetParameter("pBillingCountryId", DataPlug.DataType._Varchar, data.BillingCountryId));
            param.Add(Connection.GetParameter("pBillingCityname", DataPlug.DataType._Varchar, data.BillingCityname));

            param.Add(Connection.GetParameter("pCustomerPaymentMode", DataPlug.DataType._Int, data.CustomerPaymentMode));
            param.Add(Connection.GetParameter("pCreditDays", DataPlug.DataType._Decimal, data.CreditDays));




            object result = Connection.ExecuteQueryScalar("OfflineBooking_CustomerSaveNew", param);
            return Connection.ToLong(result);
        }

        public long SaveMasterOfflineBookingCustomer(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            var CustomerType = 0;
            if (data.CustomerType == 5)
            {
                CustomerType = 5;
            }
            else
            {
                CustomerType = 7;
            }
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._BigInt, data.CustomerId));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, data.CustomerName));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, data.CustomerEmail));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, data.CustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._Varchar, data.CustomerCity));
            param.Add(Connection.GetParameter("pCustomerpinCode", DataPlug.DataType._Varchar, data.CustomerpinCode));
            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._Varchar, data.CustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._Varchar, data.CustomerState));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, data.CustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, data.CustomerMobile));
            param.Add(Connection.GetParameter("pCustomerType", DataPlug.DataType._Varchar, CustomerType));
            param.Add(Connection.GetParameter("pBillingAddress", DataPlug.DataType._Varchar, data.BillingAddress));
            param.Add(Connection.GetParameter("pBillingState", DataPlug.DataType._Varchar, data.BillingState));
            param.Add(Connection.GetParameter("pBillingCity", DataPlug.DataType._Varchar, data.BillingCity));
            param.Add(Connection.GetParameter("pPinCode", DataPlug.DataType._Varchar, data.PinCode));
            param.Add(Connection.GetParameter("pBillingMobile", DataPlug.DataType._Varchar, data.BillingMobile));
            param.Add(Connection.GetParameter("pContactPerson", DataPlug.DataType._Varchar, data.ContactPerson));
            param.Add(Connection.GetParameter("pOfficeNO", DataPlug.DataType._Varchar, data.OfficeNO));
            param.Add(Connection.GetParameter("pGuestName1", DataPlug.DataType._Varchar, data.GuestName1));
            param.Add(Connection.GetParameter("pGuestEmail1", DataPlug.DataType._Varchar, data.GuestEmail1));
            param.Add(Connection.GetParameter("pBillingCountryId", DataPlug.DataType._Varchar, data.BillingCountryId));
            param.Add(Connection.GetParameter("pBillingCityname", DataPlug.DataType._Varchar, data.BillingCityname));
            param.Add(Connection.GetParameter("pNoInvoiceMail", DataPlug.DataType._Bool, data.NoInvoiceMail));

            param.Add(Connection.GetParameter("pCustomerPaymentMode", DataPlug.DataType._Int, data.CustomerPaymentMode));
            param.Add(Connection.GetParameter("pCreditDays", DataPlug.DataType._Decimal, data.CreditDays));

            object result = Connection.ExecuteQueryScalar("SaveMasterOfflineBookingCustomer", param);
            return Connection.ToLong(result);
        }


        public long EditOfflineBookingCustomer(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._Int, data.CustomerId));
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._Varchar, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, data.CustomerName1));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, data.CustomerEmail1));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, data.CustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._Varchar, data.CustomerCity));
            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._Varchar, data.CustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._Varchar, data.CustomerState));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, data.CustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, data.CustomerMobile1));
            param.Add(Connection.GetParameter("pCustomerpinCode", DataPlug.DataType._Varchar, data.CustomerpinCode));

            param.Add(Connection.GetParameter("pCustomerType", DataPlug.DataType._Varchar, data.CustomerType));
            param.Add(Connection.GetParameter("pBillingAddress", DataPlug.DataType._Varchar, data.BillingAddress));
            param.Add(Connection.GetParameter("pBillingState", DataPlug.DataType._Varchar, data.BillingState));
            param.Add(Connection.GetParameter("pBillingCity", DataPlug.DataType._Varchar, data.BillingCity));
            param.Add(Connection.GetParameter("pPinCode", DataPlug.DataType._Varchar, data.PinCode));
            param.Add(Connection.GetParameter("pBillingMobile", DataPlug.DataType._Varchar, data.BillingMobile));
            param.Add(Connection.GetParameter("pContactPerson", DataPlug.DataType._Varchar, data.ContactPerson));
            param.Add(Connection.GetParameter("pOfficeNO", DataPlug.DataType._Varchar, data.OfficeNO));
            param.Add(Connection.GetParameter("pGuestName1", DataPlug.DataType._Varchar, data.GuestName1));
            param.Add(Connection.GetParameter("pGuestEmail1", DataPlug.DataType._Varchar, data.GuestEmail1));
            param.Add(Connection.GetParameter("pBillingCountryId", DataPlug.DataType._Varchar, data.BillingCountryId));
            param.Add(Connection.GetParameter("pBillingCityname", DataPlug.DataType._Varchar, data.BillingCityname));

            object result = Connection.ExecuteQueryScalar("EditOfflineBookingCustomer_Save", param);
            return Connection.ToLong(result);
        }

        public long CreateNew(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._Varchar, data.CustomerId));
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._Varchar, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, data.CustomerName1));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, data.CustomerEmail1));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, data.CustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._Varchar, data.CustomerCity));
            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._Varchar, data.CustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._Varchar, data.CustomerState));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, data.CustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, data.CustomerMobile1));

            param.Add(Connection.GetParameter("pCustomerType", DataPlug.DataType._Varchar, data.CustomerType));
            param.Add(Connection.GetParameter("pBillingAddress", DataPlug.DataType._Varchar, data.BillingAddress));
            param.Add(Connection.GetParameter("pBillingState", DataPlug.DataType._Varchar, data.BillingState));
            param.Add(Connection.GetParameter("pBillingCity", DataPlug.DataType._Varchar, data.BillingCity));
            param.Add(Connection.GetParameter("pPinCode", DataPlug.DataType._Varchar, data.PinCode));
            param.Add(Connection.GetParameter("pBillingMobile", DataPlug.DataType._Varchar, data.BillingMobile));
            param.Add(Connection.GetParameter("pContactPerson", DataPlug.DataType._Varchar, data.ContactPerson));
            param.Add(Connection.GetParameter("pOfficeNO", DataPlug.DataType._Varchar, data.OfficeNO));
            param.Add(Connection.GetParameter("pGuestName1", DataPlug.DataType._Varchar, data.GuestName1));
            param.Add(Connection.GetParameter("pGuestEmail1", DataPlug.DataType._Varchar, data.GuestEmail1));
            param.Add(Connection.GetParameter("pBillingCountryId", DataPlug.DataType._Varchar, data.BillingCountryId));
            param.Add(Connection.GetParameter("pBillingCityname", DataPlug.DataType._Varchar, data.BillingCityname));




            object result = Connection.ExecuteQueryScalar("EditOfflineBookingCustomer_AddNew", param);
            return Connection.ToLong(result);
        }

        public void RemoveCustomerEntry(long offlineBookingId)
        {
            string sql = "Delete From offlinebooking_customer Where offline_bookingid=" + offlineBookingId;
            Connection.ExecuteSqlQuery(sql);
        }
        public long SaveOfflineBookingCustomer(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._Varchar, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, data.CustomerName));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, data.CustomerEmail));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, data.CustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._Varchar, data.CustomerCity));
            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._Varchar, data.CustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._Varchar, data.CustomerState));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, data.CustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, data.CustomerMobile));

            object result = Connection.ExecuteQueryScalar("OfflineBooking_CustomerSave", param);
            return Connection.ToLong(result);
        }

        public long SaveUserAddressForOfflineBook(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._BigInt, data.CustomerId));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, data.CustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._BigInt, data.CustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._BigInt, data.CustomerState));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._BigInt, data.CustomerCity));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, data.CustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, data.CustomerMobile));
            object result = Connection.ExecuteQueryScalar("OfflineBooking_CustomerAddressSave", param);
            return Connection.ToLong(result);
        }

        public void SetPaymentRefNo(long OfflineBookId, string refNumber)
        {
            string sql = "Update offline_bookings Set OrderNo='" + refNumber + "' Where Offline_BookingId=" + OfflineBookId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }



        public void UpdateSaveStatus(long OfflineBookId, int SaveStatus)
        {
            string sql = "Update offline_bookings Set SaveStatus='" + SaveStatus + "' Where Offline_BookingId=" + OfflineBookId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void UpdateGDSHotelConfirmNumber(long OfflineBookId, string GDSHotelConfirmNumber)
        {
            string sql = "Update bookingdetails Set GDSHotelConfirmNumber='" + GDSHotelConfirmNumber + "',HotelConformationNo='"+ GDSHotelConfirmNumber + "' Where Offline_BookingId=" + OfflineBookId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }

        public void UpdateDraftbookingStatus(long OfflineBookId, int SaveStatus)
        {
            string sql = "Update offline_bookings Set DraftbookingStatus ='" + SaveStatus + "' Where Offline_BookingId=" + OfflineBookId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void UpdateNoinvoicemail(CLayer.OfflineBooking Data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pNoInvoiceMail", DataPlug.DataType._Bool, Data.NoInvoiceMail));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, Data.CustomerName1));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, Data.CustomerEmail1));
            param.Add(Connection.GetParameter("pCustomerType", DataPlug.DataType._BigInt, Data.CustomerType));
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._BigInt, Data.CustomerId));

            Connection.ExecuteQueryScalar("offlinebookingcustomer_UpdateNoinvoicemail", param);
        }


        public int GetNoInvoiceMailCount(CLayer.OfflineBooking Data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, Data.CustomerName));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, Data.CustomerEmail));
            param.Add(Connection.GetParameter("pCustomerType", DataPlug.DataType._BigInt, Data.CustomerType));

            object result = Connection.ExecuteQueryScalar("OfflineBooking_NoInvoiceMail", param);
            return Connection.ToInteger(result);
        }
        public string  GetGSTRegNo(long offlineBookingId)
        {
            
            string sql = "SELECT  IFNULL(customergstregno, '') AS Customergstregno FROM offline_bookings WHERE offline_bookingid = " + offlineBookingId.ToString();
            return Connection.ToString(Connection.ExecuteSQLQueryScalar(sql));
        }



        public int GetOfflineSaveStatus(long OfflineBookId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));
            object result = Connection.ExecuteQueryScalar("OfflineBooking_GetSaveStatus", param);
            return Connection.ToInteger(result);
        }
        public decimal GetOfflineTotalAmountForBuyPrice(long OfflineBookId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookId));
            object result = Connection.ExecuteQueryScalar("OfflineBooking_GetTotalAmountForBuyPrice", param);
            return Connection.ToDecimal(result);
        }
        public void DeleteCustomProperty(long CustomPropertyId)
        {
            string sql = "Delete from  offline_customproperty  Where CustomPropertyId=" + CustomPropertyId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void DeletevendorpaymentDetails(long VendorpaymentsId)
        {
            string sql = "Delete from  vendorpayments  Where VendorpaymentsId=" + VendorpaymentsId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
       

        public void DeleteOfflineBooking(long OfflineBookingId)
        {
            string sql = "update  offline_bookings set SaveStatus = 4  Where Offline_BookingId=" + OfflineBookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void DeleteBookingDetails(long BookedID, long LoginUserid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookedID", DataPlug.DataType._BigInt, BookedID));
            param.Add(Connection.GetParameter("pLoginUserid", DataPlug.DataType._BigInt, LoginUserid));
            Connection.ExecuteQueryScalar("DeleteBookingDetails", param);
        }

        public string ExistOrderno(long OfflineBookId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));
            object result = Connection.ExecuteQueryScalar("OfflineBooking_ExistOrderno", param);
            return Connection.ToString(result);
        }
        public string InvoiceMaildate(long OfflineBookId, int InvoiceTypeId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));
            param.Add(Connection.GetParameter("pInvoiceTypeId", DataPlug.DataType._Int, InvoiceTypeId));
            object result = Connection.ExecuteQueryScalar("invoice_MailedDate", param);
            return Connection.ToString(result);
        }

        public List<CLayer.OfflineBooking> GetAllcreatedUsers()
        {
            List<CLayer.OfflineBooking> createdusersList = new List<CLayer.OfflineBooking>();

            createdusersList.Add(new CLayer.OfflineBooking()
            {
                CreatedBy = 0,
                CreatedName = "All"
            });

            DataTable dt = Connection.GetSQLTable("SELECT DISTINCT u.`UserId` AS CreatedBy,u.FirstName,u.`LastName`   FROM `offline_bookings` of  INNER JOIN `user` u ON  u.`UserId` = of.`CreatedBy`");
            foreach (DataRow dr in dt.Rows)
            {
                createdusersList.Add(new CLayer.OfflineBooking()
                {
                    CreatedBy = Connection.ToLong(dr["CreatedBy"]),
                    CreatedName = Connection.ToString(dr["FirstName"]) + ' ' + Connection.ToString(dr["LastName"])
                });
            }

            return createdusersList;
        }

        public List<CLayer.OfflineBooking> GetProbSBEntity(int stateID)
        {
            List<CLayer.OfflineBooking> createdusersList = new List<CLayer.OfflineBooking>();
 
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._BigInt, stateID));

            DataTable  ds = Connection.GetTable("sp_GetProbEntity", param);
            foreach (DataRow dr in ds.Rows)
            {
                createdusersList.Add(new CLayer.OfflineBooking()
                {
                    SBEntityEntityId = Connection.ToInteger(dr["EntityId"]),
                    SBEntityName = Connection.ToString(dr["NAME"])
                });
            }
            return createdusersList;
        }

        public List<CLayer.OfflineBooking> GetSBEntity()
        {
            List<CLayer.OfflineBooking> createdusersList = new List<CLayer.OfflineBooking>();

            DataTable dt = Connection.GetSQLTable("SELECT a.EntityId,CONCAT(b.name ,' - ',a.NAME ) AS NAME FROM  SBEntity  AS a JOIN state AS b ON a.`StateId`=b.`StateId` WHERE STATUS=1");
            foreach (DataRow dr in dt.Rows)
            {
                createdusersList.Add(new CLayer.OfflineBooking()
                {
                    SBEntityEntityId = Connection.ToInteger(dr["EntityId"]),
                    SBEntityName = Connection.ToString(dr["NAME"])
                });
            }

            return createdusersList;
        }

        public List<CLayer.OfflineBooking> GetHSNCode()
        {
            List<CLayer.OfflineBooking> createdusersList = new List<CLayer.OfflineBooking>();

            DataTable dt = Connection.GetSQLTable("SELECT COdeId,NatureOfService FROM  HSNCode ");
            foreach (DataRow dr in dt.Rows)
            {
                createdusersList.Add(new CLayer.OfflineBooking()
                {
                    HSNCodeCodeID = Connection.ToInteger(dr["COdeId"]),
                    HSNCodeNatureOfService = Connection.ToString(dr["NatureOfService"])
                });
            }

            return createdusersList;
        }

        //done by rahul 22-11-19
        //public List<CLayer.OfflineBooking> GetCostCentreCode()
        //{
        //    List<CLayer.OfflineBooking> costcentrecodeList = new List<CLayer.OfflineBooking>();
        //    DataTable dt = Connection.GetSQLTable("Select CostCentreCode from ggn_cost_centre");
        //    foreach(DataRow dr in dt.Rows)
        //    {
        //        costcentrecodeList.Add(new CLayer.OfflineBooking()
        //        {
        //            CostCentreCodeID = Connection.ToInteger(dr["CostCentreCode"])
        //        });
        //    }
        //    return costcentrecodeList;
        //}



        public void UpdateOfflineBookingCustomer(long OfflineBookId, long OfflineCustomerId)
        {
            string sql = "Update  offline_bookings  Set UserId='" + OfflineCustomerId + "' Where Offline_BookingId=" + OfflineBookId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void UpdateOfflineBookingGstForNewCustomer(long OfflineBookId, long OldOfflineCustomerId, long NewOfflineCustomerId, string Email)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookId));
            param.Add(Connection.GetParameter("pOldOfflineCustomerId", DataPlug.DataType._BigInt, OldOfflineCustomerId));
            param.Add(Connection.GetParameter("pNewOfflineCustomerId", DataPlug.DataType._BigInt, NewOfflineCustomerId));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, Email));
            object result = Connection.ExecuteQueryScalar("UpdateOfflineBookingGstForNewCustomer", param);
        }

        public void UpdateOfflineBookingCustomerNew(long OfflineBookId, long OfflineCustomerId)
        {
            string sql = "Update  offlinebooking_customer  Set Offline_BookingId='" + OfflineBookId + "' Where Offline_CustomerId=" + OfflineCustomerId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }


        public long SaveOfflineBookingDetails(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._BigInt, data.CustomerId));
            param.Add(Connection.GetParameter("pGuestName", DataPlug.DataType._Varchar, data.GuestName));
            param.Add(Connection.GetParameter("pGuestEmail", DataPlug.DataType._Varchar, data.GuestEmail));
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, data.SupplierId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pAccommodationid", DataPlug.DataType._BigInt, data.Accommodationid));
            param.Add(Connection.GetParameter("pAccommodationtypeid", DataPlug.DataType._BigInt, data.Accommodationtypeid));
            param.Add(Connection.GetParameter("pStayCategoryid", DataPlug.DataType._BigInt, data.StayCategoryid));
            param.Add(Connection.GetParameter("pStayCategoryName", DataPlug.DataType._BigInt, data.StayCategoryName));
            param.Add(Connection.GetParameter("pNoOfNight", DataPlug.DataType._BigInt, data.NoOfNight));
            param.Add(Connection.GetParameter("pNoOfRooms", DataPlug.DataType._BigInt, data.NoOfRooms));
            param.Add(Connection.GetParameter("pNoOfPaxAdult", DataPlug.DataType._BigInt, data.NoOfPaxAdult));
            param.Add(Connection.GetParameter("pNoOfPaxChild", DataPlug.DataType._BigInt, data.NoOfPaxChild));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._DateTime, data.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._DateTime, data.CheckOut));
            param.Add(Connection.GetParameter("pAccommodatoinTypename", DataPlug.DataType._Varchar, data.AccommodatoinTypename));


            param.Add(Connection.GetParameter("pTotalAmtForBuyPrice", DataPlug.DataType._Decimal, data.TotalAmtForBuyPrice));
            param.Add(Connection.GetParameter("pTotalAmtotherForBuyPrice", DataPlug.DataType._Decimal, data.TotalAmtotherForBuyPrice));
            param.Add(Connection.GetParameter("pAvgDailyRateBefreStaxForBuyPrice", DataPlug.DataType._Decimal, data.AvgDailyRateBefreStaxForBuyPrice));
            param.Add(Connection.GetParameter("pBuyPriceforotherservicesForBuyprice", DataPlug.DataType._Decimal, data.BuyPriceforotherservicesForBuyprice));
            param.Add(Connection.GetParameter("pStaxForBuyPrice", DataPlug.DataType._Decimal, data.StaxForBuyPrice));
            param.Add(Connection.GetParameter("pStaxForotherBuyPrice", DataPlug.DataType._Decimal, data.StaxForotherBuyPrice));

            param.Add(Connection.GetParameter("pTotalAmtForSalePrice", DataPlug.DataType._Decimal, data.TotalAmtForSalePrice));
            param.Add(Connection.GetParameter("pTotalAmtotherForSalePrice", DataPlug.DataType._Decimal, data.TotalAmtotherForSalePrice));
            param.Add(Connection.GetParameter("pAvgDailyRateBefreStaxForSalePrice", DataPlug.DataType._Decimal, data.AvgDailyRateBefreStaxForSalePrice));
            param.Add(Connection.GetParameter("pBuyPriceforotherservicesForSalePrice", DataPlug.DataType._Decimal, data.BuyPriceforotherservicesForSalePrice));
            param.Add(Connection.GetParameter("pStaxForSalePrice", DataPlug.DataType._Decimal, data.StaxForSalePrice));
            param.Add(Connection.GetParameter("pStaxForotherForSalePrice", DataPlug.DataType._Decimal, data.StaxForotherForSalePrice));
            param.Add(Connection.GetParameter("pOtherService", DataPlug.DataType._Varchar, data.OtherService));


            param.Add(Connection.GetParameter("pTotalBuyPrice", DataPlug.DataType._Varchar, data.TotalBuyPrice));
            param.Add(Connection.GetParameter("pTotalSalePrice", DataPlug.DataType._Varchar, data.TotalSalePrice));

            param.Add(Connection.GetParameter("psendmailtocustomer", DataPlug.DataType._Bool, data.sendmailtocustomer));
            param.Add(Connection.GetParameter("psendmailtosupplier", DataPlug.DataType._Bool, data.sendmailtosupplier));

            param.Add(Connection.GetParameter("pCustomPropertyId", DataPlug.DataType._BigInt, data.CustomPropertyId));
            param.Add(Connection.GetParameter("pSaveStatus", DataPlug.DataType._Int, data.SaveStatus));

            param.Add(Connection.GetParameter("pCreatedBy", DataPlug.DataType._Int, data.CreatedBy));
            param.Add(Connection.GetParameter("pCreatedDate", DataPlug.DataType._DateTime, data.CreatedTime));

            param.Add(Connection.GetParameter("pSalesPersonId", DataPlug.DataType._Int, data.SalesPersonId));
            param.Add(Connection.GetParameter("pSalesRegion", DataPlug.DataType._Varchar, data.SalesRegion));

            param.Add(Connection.GetParameter("pNewCustomerReferenceNo", DataPlug.DataType._Varchar, data.NewCustomerReferenceNo));


            param.Add(Connection.GetParameter("pMailContent", DataPlug.DataType._Varchar, data.MailContent));
            param.Add(Connection.GetParameter("pHotelConformationNo", DataPlug.DataType._Varchar, data.HotelConfirmationNo));

            param.Add(Connection.GetParameter("pHotelFacility", DataPlug.DataType._Varchar, data.HotelFacility));
            param.Add(Connection.GetParameter("pAssistedBy", DataPlug.DataType._BigInt, data.AssistedBy));
            param.Add(Connection.GetParameter("pNewBillingFor", DataPlug.DataType._Varchar, data.NewBillingFor));
            param.Add(Connection.GetParameter("pCorporate_ID", DataPlug.DataType._BigInt, data.CorporateID));
            
            

            object result = Connection.ExecuteQueryScalar("OfflineBooking_Save", param);
            return Connection.ToLong(result);
        }
        public long SaveOfflineBookingDetailsGST(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._BigInt, data.CustomerId));
            param.Add(Connection.GetParameter("pGuestName", DataPlug.DataType._Varchar, data.GuestName));
            param.Add(Connection.GetParameter("pGuestEmail", DataPlug.DataType._Varchar, data.GuestEmail));
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, data.SupplierId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pAccommodationid", DataPlug.DataType._BigInt, data.Accommodationid));
            param.Add(Connection.GetParameter("pAccommodationtypeid", DataPlug.DataType._BigInt, data.Accommodationtypeid));
            param.Add(Connection.GetParameter("pStayCategoryid", DataPlug.DataType._BigInt, data.StayCategoryid));
            param.Add(Connection.GetParameter("pStayCategoryName", DataPlug.DataType._BigInt, data.StayCategoryName));
            param.Add(Connection.GetParameter("pNoOfNight", DataPlug.DataType._BigInt, data.NoOfNight));
            param.Add(Connection.GetParameter("pNoOfRooms", DataPlug.DataType._BigInt, data.NoOfRooms));
            param.Add(Connection.GetParameter("pNoOfPaxAdult", DataPlug.DataType._BigInt, data.NoOfPaxAdult));
            param.Add(Connection.GetParameter("pNoOfPaxChild", DataPlug.DataType._BigInt, data.NoOfPaxChild));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._DateTime, data.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._DateTime, data.CheckOut));
            param.Add(Connection.GetParameter("pAccommodatoinTypename", DataPlug.DataType._Varchar, data.AccommodatoinTypename));


            param.Add(Connection.GetParameter("pTotalAmtForBuyPrice", DataPlug.DataType._Decimal, data.TotalAmtForBuyPrice));
            param.Add(Connection.GetParameter("pTotalAmtotherForBuyPrice", DataPlug.DataType._Decimal, data.TotalAmtotherForBuyPrice));
            param.Add(Connection.GetParameter("pAvgDailyRateBefreStaxForBuyPrice", DataPlug.DataType._Decimal, data.AvgDailyRateBefreStaxForBuyPrice));
            param.Add(Connection.GetParameter("pBuyPriceforotherservicesForBuyprice", DataPlug.DataType._Decimal, data.BuyPriceforotherservicesForBuyprice));
            param.Add(Connection.GetParameter("pStaxForBuyPrice", DataPlug.DataType._Decimal, data.StaxForBuyPrice));
            param.Add(Connection.GetParameter("pStaxForotherBuyPrice", DataPlug.DataType._Decimal, data.StaxForotherBuyPrice));

            param.Add(Connection.GetParameter("pTotalAmtForSalePrice", DataPlug.DataType._Decimal, data.TotalAmtForSalePrice));
            param.Add(Connection.GetParameter("pTotalAmtotherForSalePrice", DataPlug.DataType._Decimal, data.TotalAmtotherForSalePrice));
            param.Add(Connection.GetParameter("pAvgDailyRateBefreStaxForSalePrice", DataPlug.DataType._Decimal, data.AvgDailyRateBefreStaxForSalePrice));
            param.Add(Connection.GetParameter("pBuyPriceforotherservicesForSalePrice", DataPlug.DataType._Decimal, data.BuyPriceforotherservicesForSalePrice));
            param.Add(Connection.GetParameter("pStaxForSalePrice", DataPlug.DataType._Decimal, data.StaxForSalePrice));
            param.Add(Connection.GetParameter("pStaxForotherForSalePrice", DataPlug.DataType._Decimal, data.StaxForotherForSalePrice));
            param.Add(Connection.GetParameter("pOtherService", DataPlug.DataType._Varchar, data.OtherService));


            param.Add(Connection.GetParameter("pTotalBuyPrice", DataPlug.DataType._Varchar, data.TotalBuyPrice));
            param.Add(Connection.GetParameter("pTotalSalePrice", DataPlug.DataType._Varchar, data.TotalSalePrice));

            param.Add(Connection.GetParameter("psendmailtocustomer", DataPlug.DataType._Bool, data.sendmailtocustomer));
            param.Add(Connection.GetParameter("psendmailtosupplier", DataPlug.DataType._Bool, data.sendmailtosupplier));

            param.Add(Connection.GetParameter("pCustomPropertyId", DataPlug.DataType._BigInt, data.CustomPropertyId));
            param.Add(Connection.GetParameter("pSaveStatus", DataPlug.DataType._Int, data.SaveStatus));

            param.Add(Connection.GetParameter("pCreatedBy", DataPlug.DataType._Int, data.CreatedBy));
            param.Add(Connection.GetParameter("pCreatedDate", DataPlug.DataType._DateTime, data.CreatedTime));

            param.Add(Connection.GetParameter("pSalesPersonId", DataPlug.DataType._Int, data.SalesPersonId));
            param.Add(Connection.GetParameter("pSalesRegion", DataPlug.DataType._Varchar, data.SalesRegion));

            param.Add(Connection.GetParameter("pNewCustomerReferenceNo", DataPlug.DataType._Varchar, data.NewCustomerReferenceNo));


            param.Add(Connection.GetParameter("pMailContent", DataPlug.DataType._Varchar, data.MailContent));
            param.Add(Connection.GetParameter("pHotelConformationNo", DataPlug.DataType._Varchar, data.HotelConfirmationNo));

            param.Add(Connection.GetParameter("pHotelFacility", DataPlug.DataType._Varchar, data.HotelFacility));
            param.Add(Connection.GetParameter("pSBEntityEntityId", DataPlug.DataType._Int, data.SBEntityEntityId));
            param.Add(Connection.GetParameter("pSBEntityEntityId1", DataPlug.DataType._Int, data.SBEntityEntityId1));
            param.Add(Connection.GetParameter("pPayeeID", DataPlug.DataType._BigInt, data.PayeeID));

            param.Add(Connection.GetParameter("pCustomerGstStateId", DataPlug.DataType._Varchar, data.CustomerGstStateId));
            param.Add(Connection.GetParameter("pCustomerGstRegNo", DataPlug.DataType._Varchar, data.CustomerGstRegNo));
            param.Add(Connection.GetParameter("pPropertyGstRegNo", DataPlug.DataType._Varchar, data.PropertyGstRegNo));


            param.Add(Connection.GetParameter("pFromCustomer", DataPlug.DataType._Varchar, data.FromCustomer));
            param.Add(Connection.GetParameter("pFromCustomerId", DataPlug.DataType._Varchar, data.FromCustomerId));

            param.Add(Connection.GetParameter("pcancellationpolicy", DataPlug.DataType._Varchar, data.cancellationpolicy));

            param.Add(Connection.GetParameter("pCustomerPaymentMode", DataPlug.DataType._Int, data.CustomerPaymentModeId));
            param.Add(Connection.GetParameter("PInventoryAPIType", DataPlug.DataType._Int, data.InventoryAPIType));

            param.Add(Connection.GetParameter("pCreditDays", DataPlug.DataType._Decimal, data.CustomerPaymentCreditPeriod));
            param.Add(Connection.GetParameter("pAssistedBy", DataPlug.DataType._BigInt, data.AssistedBy));
            param.Add(Connection.GetParameter("pBookingForSelf", DataPlug.DataType._Varchar, data.BookingForSelf));
            param.Add(Connection.GetParameter("pNewBillingFor", DataPlug.DataType._Varchar, data.NewBillingFor));
            param.Add(Connection.GetParameter("pCorporate_ID", DataPlug.DataType._BigInt, data.CorporateID));

            object result = Connection.ExecuteQueryScalar("OfflineBooking_SaveGST", param);
            return Connection.ToLong(result);
        }



        public long SaveCustomPropertyotherDetails(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pCustomPropertyId", DataPlug.DataType._Varchar, data.CustomPropertyId));
            param.Add(Connection.GetParameter("pPropertyName", DataPlug.DataType._Varchar, data.PropertyName));
            param.Add(Connection.GetParameter("pPropertyAddress", DataPlug.DataType._Varchar, data.PropertyAddress));
            param.Add(Connection.GetParameter("pPropertyPinCode", DataPlug.DataType._Varchar, data.PropertyPinCode));
            param.Add(Connection.GetParameter("pPropertyCity", DataPlug.DataType._BigInt, data.PropertyCity));
            param.Add(Connection.GetParameter("pPropertyCityname", DataPlug.DataType._Varchar, data.PropertyCityname));

            param.Add(Connection.GetParameter("pPropertyState", DataPlug.DataType._BigInt, data.PropertyState));
            param.Add(Connection.GetParameter("pPropertyCountry", DataPlug.DataType._BigInt, data.PropertyCountry));
            param.Add(Connection.GetParameter("pPropertyContactNo", DataPlug.DataType._Varchar, data.PropertyContactNo));
            param.Add(Connection.GetParameter("pPropertyCaretakerName", DataPlug.DataType._Varchar, data.PropertyCaretakerName));
            param.Add(Connection.GetParameter("pPropertyEmail", DataPlug.DataType._Varchar, data.PropertyEmail));

            param.Add(Connection.GetParameter("pSupplierName", DataPlug.DataType._Varchar, data.SupplierName));
            param.Add(Connection.GetParameter("pSupplierAddress", DataPlug.DataType._Varchar, data.SupplierAddress));
            param.Add(Connection.GetParameter("pSupplierPinCode", DataPlug.DataType._Varchar, data.SupplierPinCode));
            param.Add(Connection.GetParameter("pSupplierCity", DataPlug.DataType._BigInt, data.SupplierCity));
            param.Add(Connection.GetParameter("pSupplierCityname", DataPlug.DataType._Varchar, data.SupplierCityname));

            param.Add(Connection.GetParameter("pSupplierState", DataPlug.DataType._BigInt, data.SupplierState));
            param.Add(Connection.GetParameter("pSupplierCountry", DataPlug.DataType._BigInt, data.SupplierCountry));
            param.Add(Connection.GetParameter("pSupplierMobile", DataPlug.DataType._Varchar, data.SupplierMobile));
            param.Add(Connection.GetParameter("pSupplierEmail", DataPlug.DataType._Varchar, data.SupplierEmail));

            param.Add(Connection.GetParameter("pAccountNumber", DataPlug.DataType._Varchar, data.AccountNumber));
            param.Add(Connection.GetParameter("pBankName", DataPlug.DataType._Varchar, data.BankName));
            param.Add(Connection.GetParameter("pBranchName", DataPlug.DataType._Varchar, data.BranchName));
            param.Add(Connection.GetParameter("pBranchAddress", DataPlug.DataType._Varchar, data.BranchAddress));
            param.Add(Connection.GetParameter("pAccountName", DataPlug.DataType._Varchar, data.AccountName));
            param.Add(Connection.GetParameter("pAccountType", DataPlug.DataType._Varchar, data.AccountType));
            param.Add(Connection.GetParameter("pIFSCcode", DataPlug.DataType._Varchar, data.IFSCcode));
            param.Add(Connection.GetParameter("pMICRcode", DataPlug.DataType._Varchar, data.MICRcode));
            param.Add(Connection.GetParameter("pCareTakerPhone", DataPlug.DataType._Varchar, data.CareTakerPhone));
            param.Add(Connection.GetParameter("pCareTakerEmail", DataPlug.DataType._Varchar, data.CareTakerEmail));
            param.Add(Connection.GetParameter("pGSTRegistrationNo", DataPlug.DataType._Varchar, data.GSTRegistrationNo));
            param.Add(Connection.GetParameter("pPAN", DataPlug.DataType._Varchar, data.PAN));


            object result = Connection.ExecuteQueryScalar("Offline_CustomPropertyotherSave", param);
            return Connection.ToLong(result);
        }
        public long SaveCustomPropertyDetails(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pPropertyName", DataPlug.DataType._Varchar, data.PropertyName));
            param.Add(Connection.GetParameter("pPropertyAddress", DataPlug.DataType._Varchar, data.PropertyAddress));
            param.Add(Connection.GetParameter("pPropertyCity", DataPlug.DataType._BigInt, data.PropertyCity));
            param.Add(Connection.GetParameter("pPropertyCityname", DataPlug.DataType._Varchar, data.PropertyCityname));

            param.Add(Connection.GetParameter("pPropertyState", DataPlug.DataType._BigInt, data.PropertyState));
            param.Add(Connection.GetParameter("pPropertyCountry", DataPlug.DataType._BigInt, data.PropertyCountry));
            param.Add(Connection.GetParameter("pPropertyContactNo", DataPlug.DataType._Varchar, data.PropertyContactNo));
            param.Add(Connection.GetParameter("pPropertyCaretakerName", DataPlug.DataType._Varchar, data.PropertyCaretakerName));
            param.Add(Connection.GetParameter("pPropertyEmail", DataPlug.DataType._Varchar, data.PropertyEmail));

            param.Add(Connection.GetParameter("pSupplierName", DataPlug.DataType._Varchar, data.SupplierName));
            param.Add(Connection.GetParameter("pSupplierAddress", DataPlug.DataType._Varchar, data.SupplierAddress));
            param.Add(Connection.GetParameter("pSupplierCity", DataPlug.DataType._BigInt, data.SupplierCity));
            param.Add(Connection.GetParameter("pSupplierCityname", DataPlug.DataType._Varchar, data.SupplierCityname));

            param.Add(Connection.GetParameter("pSupplierState", DataPlug.DataType._BigInt, data.SupplierState));
            param.Add(Connection.GetParameter("pSupplierCountry", DataPlug.DataType._BigInt, data.SupplierCountry));
            param.Add(Connection.GetParameter("pSupplierMobile", DataPlug.DataType._Varchar, data.SupplierMobile));
            param.Add(Connection.GetParameter("pSupplierEmail", DataPlug.DataType._Varchar, data.SupplierEmail));

            object result = Connection.ExecuteQueryScalar("Offline_CustomPropertySave", param);
            return Connection.ToLong(result);
        }

        public long SavePropertyDetails(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));

            param.Add(Connection.GetParameter("pPropertyName", DataPlug.DataType._Varchar, data.PropertyName));
            param.Add(Connection.GetParameter("pPropertyAddress", DataPlug.DataType._Varchar, data.PropertyAddress));
            param.Add(Connection.GetParameter("pPropertyCity", DataPlug.DataType._BigInt, data.PropertyCity));
            param.Add(Connection.GetParameter("pPropertyCityname", DataPlug.DataType._Varchar, data.PropertyCityname));

            param.Add(Connection.GetParameter("pPropertyState", DataPlug.DataType._BigInt, data.PropertyState));
            param.Add(Connection.GetParameter("pPropertyCountry", DataPlug.DataType._BigInt, data.PropertyCountry));
            param.Add(Connection.GetParameter("pPropertyContactNo", DataPlug.DataType._Varchar, data.PropertyContactNo));
            param.Add(Connection.GetParameter("pPropertyCaretakerName", DataPlug.DataType._Varchar, data.PropertyCaretakerName));
            param.Add(Connection.GetParameter("pPropertyEmail", DataPlug.DataType._Varchar, data.PropertyEmail));

            param.Add(Connection.GetParameter("pPropertyPinCode", DataPlug.DataType._Varchar, data.PropertyPinCode));
            param.Add(Connection.GetParameter("pSupplierPinCode", DataPlug.DataType._Varchar, data.SupplierPinCode));


            param.Add(Connection.GetParameter("pSupplierName", DataPlug.DataType._Varchar, data.SupplierName));
            param.Add(Connection.GetParameter("pSupplierAddress", DataPlug.DataType._Varchar, data.SupplierAddress));
            param.Add(Connection.GetParameter("pSupplierCity", DataPlug.DataType._BigInt, data.SupplierCity));
            param.Add(Connection.GetParameter("pSupplierCityname", DataPlug.DataType._Varchar, data.SupplierCityname));

            param.Add(Connection.GetParameter("pSupplierState", DataPlug.DataType._BigInt, data.SupplierState));
            param.Add(Connection.GetParameter("pSupplierCountry", DataPlug.DataType._BigInt, data.SupplierCountry));
            param.Add(Connection.GetParameter("pSupplierMobile", DataPlug.DataType._Varchar, data.SupplierMobile));
            param.Add(Connection.GetParameter("pSupplierEmail", DataPlug.DataType._Varchar, data.SupplierEmail));

            object result = Connection.ExecuteQueryScalar("OfflineBooking_PropertySave", param);
            return Connection.ToLong(result);
        }
        //Done By Rahul
        public long SaveCostCentre(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pCostCentreCode", DataPlug.DataType._BigInt, data.CostCentreCode));
            param.Add(Connection.GetParameter("pCostCentrePercentage", DataPlug.DataType._BigInt, data.CostCentrePercentage));
            object result = Connection.ExecuteQueryScalar("SaveCostCentre", param);
            return Connection.ToLong(result);
        }
        public long SaveEditCostCentre(int id, CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCostCentre_ID", DataPlug.DataType._BigInt, data.CostCentre_ID));
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pCostCentreCode", DataPlug.DataType._BigInt, data.CostCentreCode));
            param.Add(Connection.GetParameter("pCostCentrePercentage", DataPlug.DataType._BigInt, data.CostCentrePercentage));
            object result = Connection.ExecuteQueryScalar("SaveEditCostCentre", param);
            return Connection.ToLong(result);
        }
        public void DeleteCostCentre(int Id)
        {
            string sql = "Delete From offlinebooking_costcentre Where CostCentre_ID=" + Id.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        //----
        public long SaveVendorDetails(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("PvendorId", DataPlug.DataType._BigInt, data.vendorId));
            param.Add(Connection.GetParameter("pVendorpaymentsId", DataPlug.DataType._BigInt, data.VendorpaymentsId));
            param.Add(Connection.GetParameter("pOffline_BookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pVendorsname", DataPlug.DataType._Varchar, data.vendorname));
            param.Add(Connection.GetParameter("pVendoraddress", DataPlug.DataType._Varchar, data.vendoraddress));
            param.Add(Connection.GetParameter("pAddress1", DataPlug.DataType._Varchar, data.address1));

            param.Add(Connection.GetParameter("pAddress2", DataPlug.DataType._Varchar, data.address2));
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, data.City));
            param.Add(Connection.GetParameter("pPin", DataPlug.DataType._Varchar, data.pin));
            param.Add(Connection.GetParameter("pContactperson", DataPlug.DataType._Varchar, data.contactperson));
            param.Add(Connection.GetParameter("pContactno", DataPlug.DataType._Varchar, data.contactno));

            param.Add(Connection.GetParameter("pEmailaddress", DataPlug.DataType._Varchar, data.emailaddress));
            param.Add(Connection.GetParameter("pNatureofservice", DataPlug.DataType._Varchar, data.natureofservice));

            param.Add(Connection.GetParameter("pByPriceBeforeTax", DataPlug.DataType._Decimal, data.ByPriceBeforeTax));
            param.Add(Connection.GetParameter("pSalePriceGST", DataPlug.DataType._Decimal, data.SalePriceGST));
            param.Add(Connection.GetParameter("pByPriceTotal", DataPlug.DataType._Decimal, data.ByPriceTotal));
            param.Add(Connection.GetParameter("pSalePriceBeforeTax", DataPlug.DataType._Decimal, data.SalePriceBeforeTax));
            param.Add(Connection.GetParameter("pByPriceGST", DataPlug.DataType._Decimal, data.ByPriceGST));
            param.Add(Connection.GetParameter("pSalePriceTotal", DataPlug.DataType._Decimal, data.SalePriceTotal));
            param.Add(Connection.GetParameter("PHSNCode", DataPlug.DataType._BigInt, data.HSNCodeCodeID));
            param.Add(Connection.GetParameter("pPlaceOfSupply", DataPlug.DataType._Int, data.PlaceOfSupply));

            object result = Connection.ExecuteQueryScalar("VendorPayment_Save_gst", param);
            return Connection.ToLong(result);
        }

        public CLayer.OfflineBooking GetPayeeDetails(long PayeeID)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPayeeID", DataPlug.DataType._BigInt, PayeeID));

            DataTable dt = Connection.GetTable("GetPayeeDetails", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();

                result.PayeeName = Connection.ToString(dt.Rows[0]["Name"]);
                result.AmountPayable = Connection.ToLong(dt.Rows[0]["Amount"]);
                result.AccountNumber = Connection.ToString(dt.Rows[0]["AccountNumber"]);
                result.IFSCcode = Connection.ToString(dt.Rows[0]["IFSCcode"]);
                result.BankName = Connection.ToString(dt.Rows[0]["BankName"]);
                result.BranchName = Connection.ToString(dt.Rows[0]["BranchName"]);
                result.PAN = Connection.ToString(dt.Rows[0]["PAN"]);
            }
            return result;
        }

        public CLayer.OfflineBooking GetAllDetailsById(long OfflineBookId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));

            DataTable dt = Connection.GetTable("OfflineBooking_GetAll", param);

            if (dt.Rows.Count > 0)
            {
                bool IsSupplierPaymentMailSend = false;
               if(dt.Columns.Contains("IsSupplierPaymentMailSend"))
                {
                    if (dt.Rows[0]["IsSupplierPaymentMailSend"] != null)
                    {
                        IsSupplierPaymentMailSend = Connection.ToBoolean(dt.Rows[0]["IsSupplierPaymentMailSend"]);
                    }
                }
               

                result = new CLayer.OfflineBooking();
                result.OfflineBookingId = OfflineBookId;
                //Primary Details
                result.PropertyId = Connection.ToInteger(dt.Rows[0]["PropertyId"]);
                result.SupplierId = Connection.ToInteger(dt.Rows[0]["SupplierId"]);
                result.CustomerId = Connection.ToInteger(dt.Rows[0]["UserId"]);
                result.CustomPropertyId = Connection.ToInteger(dt.Rows[0]["CustomPropertyId"]);
                //Pricing Details
                result.TotalAmtForBuyPrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtForBuyPrice"]);
                result.TotalAmtotherForBuyPrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtotherForBuyPrice"]);
                result.AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dt.Rows[0]["AvgDailyRateBefreStaxForBuyPrice"]);
                result.BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dt.Rows[0]["BuyPriceforotherservicesForBuyprice"]);
                result.StaxForBuyPrice = Connection.ToDecimal(dt.Rows[0]["StaxForBuyPrice"]);
                result.StaxForotherBuyPrice = Connection.ToDecimal(dt.Rows[0]["StaxForotherBuyPrice"]);

                result.TotalAmtForSalePrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtForSalePrice"]);
                result.TotalAmtotherForSalePrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtotherForSalePrice"]);
                result.AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dt.Rows[0]["AvgDailyRateBefreStaxForSalePrice"]);
                result.BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dt.Rows[0]["BuyPriceforotherservicesForSalePrice"]);
                result.StaxForSalePrice = Connection.ToDecimal(dt.Rows[0]["StaxForSalePrice"]);
                result.StaxForotherForSalePrice = Connection.ToDecimal(dt.Rows[0]["StaxForotherForSalePrice"]);
                result.TotalBuyPrice = Connection.ToDecimal(dt.Rows[0]["TotalBuyPrice"]);
                result.TotalSalePrice = Connection.ToDecimal(dt.Rows[0]["TotalSalePrice"]);
                //Booking Details

                result.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn_date"]);
                result.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut_date"]);
                result.Accommodationid = Connection.ToInteger(dt.Rows[0]["AccommodationId"]);
                result.Accommodationtypeid = Connection.ToInteger(dt.Rows[0]["AccommodatoinTypeId"]);
                result.AccommodatoinTypename = Connection.ToString(dt.Rows[0]["AccommodatoinTypename"]);
                result.NoOfNight = Connection.ToInteger(dt.Rows[0]["NoOfNight"]);
                result.NoOfRooms = Connection.ToInteger(dt.Rows[0]["NoOfRooms"]);
                result.NoOfPaxAdult = Connection.ToInteger(dt.Rows[0]["NoOfPaxAdult"]);
                result.NoOfPaxChild = Connection.ToInteger(dt.Rows[0]["NoOfPaxChild"]);
                result.GuestEmail = Connection.ToString(dt.Rows[0]["GuestEmail"]);
                result.GuestName = Connection.ToString(dt.Rows[0]["GuestName"]);

                result.CreatedTime = Connection.ToDate(dt.Rows[0]["CreatedDate"]);
                result.CreatedBy = Connection.ToInteger(dt.Rows[0]["CreatedBy"]);
                result.StayCategoryid = Connection.ToInteger(dt.Rows[0]["StayCategoryid"]);
                result.SaveStatus = Connection.ToInteger(dt.Rows[0]["SaveStatus"]);
                //email send option 
                result.sendmailtocustomer = Connection.ToBoolean(dt.Rows[0]["IsSendCustomerMail"]);
                result.sendmailtosupplier = Connection.ToBoolean(dt.Rows[0]["IsSendSupplierMail"]);

                result.ConfirmationNumber = Connection.ToString(dt.Rows[0]["OrderNo"]);
                result.OtherService = Connection.ToString(dt.Rows[0]["OtherService"]);

                result.SalesPersonId = Connection.ToInteger(dt.Rows[0]["SalesPersonId"]);
                result.SalesRegion = Connection.ToString(dt.Rows[0]["SalesRegion"]);

                result.NewCustomerReferenceNo = Connection.ToString(dt.Rows[0]["CustomerReferenceNo"]);
                result.MailContent = Connection.ToString(dt.Rows[0]["MailContent"]);
                result.HotelConfirmationNo = Connection.ToString(dt.Rows[0]["HotelConformationNo"]);

                result.CustomPropertyId = Connection.ToInteger(dt.Rows[0]["CustomPropertyId"]);
                result.HotelFacility = Connection.ToString(dt.Rows[0]["HotelFacility"]);
                result.SBEntityEntityId1 = Connection.ToInteger(dt.Rows[0]["SBEntityEntityIdBilling"]);
                result.SBEntityEntityId = Connection.ToInteger(dt.Rows[0]["SBEntityEntityId"]);
                result.PayeeID = Connection.ToInteger(dt.Rows[0]["PayeeID"]);

                result.CustomerGstStateId = Connection.ToInteger(dt.Rows[0]["CustomerGstStateId"]);
                result.CustomerGstRegNo = Connection.ToString(dt.Rows[0]["CustomerGstRegNo"]);
                result.PropertyGstRegNo = Connection.ToString(dt.Rows[0]["PropertyGstRegNo"]);

                result.FromCustomer = Connection.ToLong(dt.Rows[0]["FromCustomer"]);
                result.FromCustomerId = Connection.ToLong(dt.Rows[0]["FromCustomerId"]);
                result.cancellationpolicy = Connection.ToString(dt.Rows[0]["cancellationpolicy"]);
                result.CustomerPaymentModeId = Connection.ToInteger(dt.Rows[0]["CustomerPaymentMode"]);
                result.CustomerPaymentCreditPeriod = Connection.ToDecimal(dt.Rows[0]["CreditDays"]);

                result.InventoryAPIType = Connection.ToInteger(dt.Rows[0]["InventoryAPIType"]);
                result.IsSupplierPaymentMailSend = IsSupplierPaymentMailSend;
                result.PlaceOfSupplyName = Connection.ToString(dt.Rows[0]["PlaceOfSupplyName"]);
            }
            return result;
        }

        public List<CLayer.OfflineBooking> GetAllOfflineDetails()
        {
            CLayer.OfflineBooking result = null;
            List<CLayer.OfflineBooking> lstresult = new List<CLayer.OfflineBooking>();

            DataTable dt = Connection.GetTable("OfflineBooking_GetAllBooking");

            if (dt.Rows.Count > 0)
            {

                for (int count = 0; count < dt.Rows.Count; count++)
                {
                    result = new CLayer.OfflineBooking();
                    result.SupplierPaymentScheduleList = new List<SupplierPaymentSchedule>();

                    result.OfflineBookingId = Connection.ToInteger(dt.Rows[count]["Offline_BookingId"]);
                    result.CheckIn = Connection.ToDate(dt.Rows[count]["CheckIn_date"]);
                    result.CheckOut = Connection.ToDate(dt.Rows[count]["CheckOut_date"]);

                    var OfflineBookingAllSupplierPayments = GetAllSupplierPayments(result.OfflineBookingId);
                    foreach (var data in OfflineBookingAllSupplierPayments)
                    {
                        SupplierPaymentSchedule spschedule = new SupplierPaymentSchedule();
                        spschedule.SupplierPaymentMode = data.SupplierPaymentMode;
                        spschedule.SupplierPaymentModeDate = data.SupplierPaymentModeDate;
                        spschedule.SupplierCreditDays = data.SupplierCreditDays;
                        result.SupplierPaymentScheduleList.Add(spschedule);

                    }

                    lstresult.Add(result);
                }


            }
            return lstresult;
        }

        public CLayer.OfflineBooking GetOfflineboikingdetailsforBookDeleteRequest(long OfflineBookId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));
            DataTable dt = Connection.GetTable("GetOfflineboikingdetailsforBookDeleteRequest", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.OfflineBookingId = OfflineBookId;
                result.CustomerName = Connection.ToString(dt.Rows[0]["UserName"]);
                result.ConfirmationNumber = Connection.ToString(dt.Rows[0]["bookingid"]);
                result.InvoiceNumber = Connection.ToString(dt.Rows[0]["InvoiceNumber"]);
                result.InvoiceStatus = Connection.ToInteger(dt.Rows[0]["InvoiceStatus"]);
            }
            return result;
        }

        public CLayer.OfflineBooking GetAllpropertyDetails(long OfflineBookId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));

            DataTable dt = Connection.GetTable("OfflineBooking_GetAllProperty", param);

            result = new CLayer.OfflineBooking();
            if (dt.Rows.Count > 0)
            {
                

                result.PropertyName = Connection.ToString(dt.Rows[0]["PropertyName"]);
                result.PropertyAddress = Connection.ToString(dt.Rows[0]["PropertyAddress"]);
                result.PropertyCityname = Connection.ToString(dt.Rows[0]["PropertyCityname"]);
                result.PropertyStatename = Connection.ToString(dt.Rows[0]["PropertyStatename"]);
                result.PropertyCountryname = Connection.ToString(dt.Rows[0]["PropertyCountryname"]);
                result.PropertyContactNo = Connection.ToString(dt.Rows[0]["PropertyContactNo"]);
                result.PropertyCaretakerName = Connection.ToString(dt.Rows[0]["PropertyCaretakerName"]);
                result.PropertyEmail = Connection.ToString(dt.Rows[0]["PropertyEmail"]);
                result.PropertyPinCode = Connection.ToString(dt.Rows[0]["PropertyPinCode"]);

                result.SupplierPinCode = Connection.ToString(dt.Rows[0]["SupplierPinCode"]);
                result.SupplierName = Connection.ToString(dt.Rows[0]["SupplierName"]);
                result.SupplierAddress = Connection.ToString(dt.Rows[0]["SupplierAddress"]);
                result.SupplierCityname = Connection.ToString(dt.Rows[0]["SupplierCityname"]);
                result.SupplierStatename = Connection.ToString(dt.Rows[0]["SupplierStatename"]);
                result.SupplierCountryname = Connection.ToString(dt.Rows[0]["SupplierCountryname"]);
                result.SupplierMobile = Connection.ToString(dt.Rows[0]["SupplierMobile"]);
                result.SupplierEmail = Connection.ToString(dt.Rows[0]["SupplierEmail"]);


                result.PropertyCity = Connection.ToInteger(dt.Rows[0]["PropertyCity"]);
                result.PropertyCountry = Connection.ToInteger(dt.Rows[0]["PropertyCountry"]);
                result.PropertyState = Connection.ToInteger(dt.Rows[0]["PropertyState"]);
                result.SupplierCity = Connection.ToInteger(dt.Rows[0]["SupplierCity"]);
                result.SupplierCountry = Connection.ToInteger(dt.Rows[0]["SupplierCountry"]);
                result.SupplierState = Connection.ToInteger(dt.Rows[0]["SupplierState"]);
            }
            return result;
        }

        public CLayer.OfflineBooking GetAllpropertyDetailsForPaymentScedule(long OfflineBookId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));

            DataTable dt = Connection.GetTable("OfflineBooking_GetAllPropertyForPaymentScedule", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();

                result.PropertyName = Connection.ToString(dt.Rows[0]["PropertyName"]);
                result.PropertyAddress = Connection.ToString(dt.Rows[0]["PropertyAddress"]);
                result.PropertyCityname = Connection.ToString(dt.Rows[0]["PropertyCityname"]);
                result.PropertyStatename = Connection.ToString(dt.Rows[0]["PropertyStatename"]);
                result.PropertyCountryname = Connection.ToString(dt.Rows[0]["PropertyCountryname"]);
                result.PropertyContactNo = Connection.ToString(dt.Rows[0]["PropertyContactNo"]);
                result.PropertyCaretakerName = Connection.ToString(dt.Rows[0]["PropertyCaretakerName"]);
                result.PropertyEmail = Connection.ToString(dt.Rows[0]["PropertyEmail"]);
                result.PropertyPinCode = Connection.ToString(dt.Rows[0]["PropertyPinCode"]);

                result.SupplierPinCode = Connection.ToString(dt.Rows[0]["SupplierPinCode"]);
                result.SupplierName = Connection.ToString(dt.Rows[0]["SupplierName"]);
                result.SupplierAddress = Connection.ToString(dt.Rows[0]["SupplierAddress"]);
                result.SupplierCityname = Connection.ToString(dt.Rows[0]["SupplierCityname"]);
                result.SupplierStatename = Connection.ToString(dt.Rows[0]["SupplierStatename"]);
                result.SupplierCountryname = Connection.ToString(dt.Rows[0]["SupplierCountryname"]);
                result.SupplierMobile = Connection.ToString(dt.Rows[0]["SupplierMobile"]);
                result.SupplierEmail = Connection.ToString(dt.Rows[0]["SupplierEmail"]);


                result.PropertyCity = Connection.ToInteger(dt.Rows[0]["PropertyCity"]);
                result.PropertyCountry = Connection.ToInteger(dt.Rows[0]["PropertyCountry"]);
                result.PropertyState = Connection.ToInteger(dt.Rows[0]["PropertyState"]);
                result.SupplierCity = Connection.ToInteger(dt.Rows[0]["SupplierCity"]);
                result.SupplierCountry = Connection.ToInteger(dt.Rows[0]["SupplierCountry"]);
                result.SupplierState = Connection.ToInteger(dt.Rows[0]["SupplierState"]);

                if (Connection.ToInteger(dt.Rows[0]["CustomPropertyId"]) > 0)
                {
                    result.AccountNumber = Connection.ToString(dt.Rows[0]["AccountNumber"]);
                    result.AccountType = Connection.ToString(dt.Rows[0]["AccountType"]);
                    result.AccountName = Connection.ToString(dt.Rows[0]["AccountName"]);
                    result.BankName = Connection.ToString(dt.Rows[0]["BankName"]);
                    result.BranchName = Connection.ToString(dt.Rows[0]["BranchName"]);
                    result.IFSCcode = Connection.ToString(dt.Rows[0]["IFSCcode"]);
                    result.PropertyEmail = Connection.ToString(dt.Rows[0]["PropertyEmail"]);
                }
                else
                {

                    result.AccountNumber = Connection.ToString(dt.Rows[0]["B_AccountNumber"]);
                    result.AccountType = Connection.ToString(dt.Rows[0]["B_AccountType"]);
                    result.AccountName = Connection.ToString(dt.Rows[0]["B_AccountName"]);
                    result.BankName = Connection.ToString(dt.Rows[0]["B_BankName"]);
                    result.BranchName = Connection.ToString(dt.Rows[0]["B_BranchName"]);
                    result.IFSCcode = Connection.ToString(dt.Rows[0]["B_IFSCcode"]);
                    //result.PropertyEmail = Connection.ToString(dt.Rows[0]["B_PropertyEmail"]);

                }
            }
            return result;
        }


        public List<CLayer.TaxPercentage> GetAll_OfflineBookingTaxes(long OfflineBookId, string TaxType)
        {
            List<CLayer.TaxPercentage> res = new List<CLayer.TaxPercentage>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("poffline_Id", DataPlug.DataType._BigInt, OfflineBookId));
            param.Add(Connection.GetParameter("ptaxType", DataPlug.DataType._Varchar, TaxType));

            DataTable dt = Connection.GetTable("offline_booking_GelAll_Taxes", param);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CLayer.TaxPercentage result = new CLayer.TaxPercentage();
                    result.TaxPerID = Connection.ToLong(dr["taxId"]);
                    result.TaxOfflineBookingID = Connection.ToLong(dr["offline_Id"]);
                    result.TaxTitle = Connection.ToString(dr["taxTitle"]);
                    result.TaxPercent = Connection.ToString(dr["taxPercentage"]);
                    result.TaxType = Connection.ToString(dr["taxType"]);
                    res.Add(result);
                }
            }
            return res;
        }

        public List<CLayer.TaxPercentage> GetAll_OfflineBookingTaxesGST(long OfflineBookId, string TaxType, string type, long bookingid)
        {
            List<CLayer.TaxPercentage> res = new List<CLayer.TaxPercentage>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("poffline_Id", DataPlug.DataType._BigInt, OfflineBookId));
            param.Add(Connection.GetParameter("pbookingid", DataPlug.DataType._BigInt, bookingid));
            param.Add(Connection.GetParameter("ptaxType", DataPlug.DataType._Varchar, TaxType));
            param.Add(Connection.GetParameter("ptype", DataPlug.DataType._Varchar, type));

            DataTable dt = Connection.GetTable("offline_booking_GelAll_TaxesGST", param);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    CLayer.TaxPercentage result = new CLayer.TaxPercentage();
                    result.TaxPerID = Connection.ToLong(dr["taxId"]);
                    result.TaxOfflineBookingID = Connection.ToLong(dr["offline_Id"]);
                    result.TaxTitle = Connection.ToString(dr["taxTitle"]);
                    result.TaxPercent = Connection.ToString(dr["taxPercentage"]);
                    result.TaxType = Connection.ToString(dr["taxType"]);
                    result.Type = Connection.ToString(dr["Type"]);
                    result.BookingID = Connection.ToLong(dr["BookingId"]);
                    result.rowSet = "";
                    res.Add(result);
                }
            }
            return res;
        }



        public void save_OfflineTaxes(List<CLayer.TaxPercentage> data)
        {
            foreach (CLayer.TaxPercentage item in data)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("ptaxId", DataPlug.DataType._BigInt, item.TaxPerID));
                param.Add(Connection.GetParameter("poffline_Id", DataPlug.DataType._BigInt, item.TaxOfflineBookingID));
                param.Add(Connection.GetParameter("ptaxTitle", DataPlug.DataType._Varchar, item.TaxTitle));
                param.Add(Connection.GetParameter("ptaxPercentage", DataPlug.DataType._Varchar, item.TaxPercent));
                param.Add(Connection.GetParameter("ptaxType", DataPlug.DataType._Varchar, item.TaxType));
                object res = Connection.ExecuteQueryScalar("offline_booking_Tax_SAVE", param);
                Connection.ToLong(res);
            }
        }

        public void save_OfflineTaxesMultiple(List<CLayer.TaxPercentage> data)
        {
            long i = 0;
            double tp;

            foreach (CLayer.TaxPercentage item in data)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                //  param.Add(Connection.GetParameter("ptaxId", DataPlug.DataType._BigInt, item.TaxPerID));
                // add new code for delete old DeleteMultipleTax()
                param.Add(Connection.GetParameter("ptaxId", DataPlug.DataType._BigInt, 0));
                param.Add(Connection.GetParameter("poffline_Id", DataPlug.DataType._BigInt, item.TaxOfflineBookingID));
                param.Add(Connection.GetParameter("ptaxTitle", DataPlug.DataType._Varchar, item.TaxTitle));

                tp = 0;
                double.TryParse(item.TaxPercent, out tp);

                param.Add(Connection.GetParameter("ptaxPercentage", DataPlug.DataType._Varchar, tp.ToString()));   // item.TaxPercent));
                param.Add(Connection.GetParameter("ptaxType", DataPlug.DataType._Varchar, item.TaxType));
                param.Add(Connection.GetParameter("pType", DataPlug.DataType._Varchar, item.Type));
                param.Add(Connection.GetParameter("pBookingID", DataPlug.DataType._BigInt, item.BookingID));

                object res = Connection.ExecuteQueryScalar("offline_booking_Tax_SAVE_Multiple", param);

                i = Connection.ToLong(res);
            }


        }


        public void DeleteMultipleTax(long Bookingid)
        {
            string sql = "Delete From offline_bookings_taxes_gst Where BookingId=" + Bookingid.ToString();
            Connection.ExecuteSqlQuery(sql);
        }


        public List<CLayer.OfflineBooking> vendorrofflinebooklist(long offlinebookId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, offlinebookId));

            DataTable dt = Connection.GetTable("getvendorPaymentsByofflinebookingId", param);

            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            CLayer.OfflineBooking temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.OfflineBooking();
                temp.OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]);
                temp.vendorId = Connection.ToLong(dr["VendorpaymentsId"]);
                temp.vendorname = Connection.ToString(dr["Vendorsname"]);
                temp.vendoraddress = Connection.ToString(dr["Vendoraddress"]);

                temp.address1 = Connection.ToString(dr["Address1"]);
                temp.address2 = Connection.ToString(dr["Address2"]);
                temp.City = Connection.ToString(dr["City"]);
                temp.pin = Connection.ToString(dr["pin"]);
                temp.contactperson = Connection.ToString(dr["contactperson"]);
                temp.contactno = Connection.ToString(dr["contactno"]);
                temp.emailaddress = Connection.ToString(dr["Emailaddress"]);
                temp.natureofservice = Connection.ToString(dr["Natureofservice"]);
                temp.valuebeforeservicetax = Connection.ToDecimal(dr["Valuebeforeservicetax"]);
                temp.servicetaxamount = Connection.ToDecimal(dr["Servicetaxamount"]);
                temp.totalamountpayable = Connection.ToDecimal(dr["Totalamountpayable"]);

                result.Add(temp);
            }

            return result;
        }
        public CLayer.OfflineBooking vendorrofflinebooklistByVid(long pVendorId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pVendorpaymentsId", DataPlug.DataType._BigInt, pVendorId));

            DataTable dt = Connection.GetTable("getvendorPaymentsByVendorpaymentsId", param);

            CLayer.OfflineBooking result = new CLayer.OfflineBooking();
            CLayer.OfflineBooking temp = new CLayer.OfflineBooking();
            ;
            foreach (DataRow dr in dt.Rows)
            {
                temp.OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]);

                temp.vendorId = Connection.ToLong(dr["VendorpaymentsId"]);
                temp.vendorname = Connection.ToString(dr["Vendorsname"]);
                temp.vendoraddress = Connection.ToString(dr["Vendoraddress"]);

                temp.address1 = Connection.ToString(dr["Address1"]);
                temp.address2 = Connection.ToString(dr["Address2"]);
                temp.City = Connection.ToString(dr["City"]);
                temp.pin = Connection.ToString(dr["pin"]);
                temp.contactperson = Connection.ToString(dr["contactperson"]);
                temp.contactno = Connection.ToString(dr["contactno"]);
                temp.emailaddress = Connection.ToString(dr["Emailaddress"]);
                temp.natureofservice = Connection.ToString(dr["Natureofservice"]);
                temp.valuebeforeservicetax = Connection.ToDecimal(dr["Valuebeforeservicetax"]);
                temp.servicetaxamount = Connection.ToDecimal(dr["Servicetaxamount"]);
                temp.totalamountpayable = Connection.ToDecimal(dr["Totalamountpayable"]);


            }

            return temp;
        }

        public CLayer.OfflineBooking GetOfflineBookingCustomerDetailsByID(long CustomPropertyId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._BigInt, CustomPropertyId));
            DataTable dt = Connection.GetTable("OfflineBookingCustomerEditFromMaster", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.CustomerName = Connection.ToString(dt.Rows[0]["Name"]);
                result.CustomerEmail = Connection.ToString(dt.Rows[0]["Email"]);
                result.CustomerMobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.CustomerCountry = Connection.ToInteger(dt.Rows[0]["CountryId"]);
                result.CustomerState = Connection.ToInteger(dt.Rows[0]["StateId"]);
                result.CustomerCity = Connection.ToInteger(dt.Rows[0]["cityId"]);
                result.CustomerCityname = Connection.ToString(dt.Rows[0]["City"]);
                result.BillingCountryId = Connection.ToInteger(dt.Rows[0]["BillingCountryId"]);
                result.CustomerAddress = Connection.ToString(dt.Rows[0]["Address"]);
                result.CustomerType = Connection.ToInteger(dt.Rows[0]["CustomerType"]);
                result.CustomerpinCode = Connection.ToString(dt.Rows[0]["CustomerpinCode"]);
                result.BillingAddress = Connection.ToString(dt.Rows[0]["BillingAddress"]);
                result.BillingState = Connection.ToInteger(dt.Rows[0]["BillingStateId"]);
                result.BillingCity = Connection.ToInteger(dt.Rows[0]["BillingcityId"]);
                result.PinCode = Connection.ToString(dt.Rows[0]["BillingPINCode"]);
                result.BillingMobile = Connection.ToString(dt.Rows[0]["BillingMobile"]);
                result.ContactPerson = Connection.ToString(dt.Rows[0]["BillingContactPersonName"]);
                result.OfficeNO = Connection.ToString(dt.Rows[0]["BillingOfficeNO"]);
                //result.GuestName1 = Connection.ToString(dt.Rows[0]["GuestName"]);
                //result.GuestEmail1 = Connection.ToString(dt.Rows[0]["GuestEmail"]);
                result.BillingCityname = Connection.ToString(dt.Rows[0]["BillingCity"]);
                result.NoInvoiceMail = Connection.ToBoolean(dt.Rows[0]["NoInvoiceMail"]);

                result.CustomerPaymentMode = Connection.ToInteger(dt.Rows[0]["CustomerPaymentMode"]);
                result.CreditDays = Connection.ToDecimal(dt.Rows[0]["CreditDays"]);

            }
            return result;
        }



        public CLayer.OfflineBooking GetDetailsByCustomProperyId(long CustomPropertyId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomPropertyId", DataPlug.DataType._BigInt, CustomPropertyId));
            DataTable dt = Connection.GetTable("OfflineBooking_GetdetailsByCustomPropertyId", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();

                result.PropertyName = Connection.ToString(dt.Rows[0]["PropertyName"]);
                result.PropertyAddress = Connection.ToString(dt.Rows[0]["PropertyAddress"]);
                result.PropertyCityname = Connection.ToString(dt.Rows[0]["PropertyCityname"]);
                result.PropertyStatename = Connection.ToString(dt.Rows[0]["PropertyStatename"]);
                result.PropertyCountryname = Connection.ToString(dt.Rows[0]["PropertyCountryname"]);
                result.PropertyContactNo = Connection.ToString(dt.Rows[0]["PropertyContactNo"]);
                result.PropertyCaretakerName = Connection.ToString(dt.Rows[0]["PropertyCaretakerName"]);
                result.PropertyEmail = Connection.ToString(dt.Rows[0]["PropertyEmail"]);
                result.PropertyPinCode = Connection.ToString(dt.Rows[0]["PropertyPincode"]);

                result.SupplierName = Connection.ToString(dt.Rows[0]["SupplierName"]);
                result.SupplierAddress = Connection.ToString(dt.Rows[0]["SupplierAddress"]);
                result.SupplierCityname = Connection.ToString(dt.Rows[0]["SupplierCityname"]);
                result.SupplierStatename = Connection.ToString(dt.Rows[0]["SupplierStatename"]);
                result.SupplierCountryname = Connection.ToString(dt.Rows[0]["SupplierCountryname"]);
                result.SupplierMobile = Connection.ToString(dt.Rows[0]["SupplierMobile"]);
                result.SupplierEmail = Connection.ToString(dt.Rows[0]["SupplierEmail"]);
                result.SupplierPinCode = Connection.ToString(dt.Rows[0]["SupplierPincode"]);

                result.PropertyCity = Connection.ToInteger(dt.Rows[0]["PropertyCity"]);
                result.PropertyCountry = Connection.ToInteger(dt.Rows[0]["PropertyCountry"]);
                result.PropertyState = Connection.ToInteger(dt.Rows[0]["PropertyState"]);

                result.SupplierCity = Connection.ToInteger(dt.Rows[0]["SupplierCity"]);
                result.SupplierCountry = Connection.ToInteger(dt.Rows[0]["SupplierCountry"]);
                result.SupplierState = Connection.ToInteger(dt.Rows[0]["SupplierState"]);

                result.AccountNumber = Connection.ToString(dt.Rows[0]["AccountNumber"]);
                result.BankName = Connection.ToString(dt.Rows[0]["BankName"]);
                result.BranchAddress = Connection.ToString(dt.Rows[0]["BranchAddress"]);
                result.AccountName = Connection.ToString(dt.Rows[0]["AccountName"]);
                result.AccountType = Connection.ToString(dt.Rows[0]["AccountType"]);
                result.IFSCcode = Connection.ToString(dt.Rows[0]["IFSCcode"]);
                result.CareTakerPhone = Connection.ToString(dt.Rows[0]["CareTakerPhone"]);
                result.CareTakerEmail = Connection.ToString(dt.Rows[0]["CareTakerEmail"]);
                result.BranchName = Connection.ToString(dt.Rows[0]["BranchName"]);
                result.MICRcode = Connection.ToString(dt.Rows[0]["MICRcode"]);
                result.GSTRegistrationNo = Connection.ToString(dt.Rows[0]["GSTRegistrationNo"]);
                result.PAN = Connection.ToString(dt.Rows[0]["PANNo"]);
            }
            return result;
        }


        public CLayer.OfflineBooking GetAllCustomerBillingaddress(long OfflineBookId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));

            DataTable dt = Connection.GetTable("OfflineBooking_GetAllCustomerBillingaddress", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.OfflineBookingId = OfflineBookId;
                result.BillingAddress = Connection.ToString(dt.Rows[0]["BillingAddress"]);
                result.BillingCityname = Connection.ToString(dt.Rows[0]["BillingCityname"]);
                result.BillingCountryname = Connection.ToString(dt.Rows[0]["BillingCountryname"]);
                result.BillingStatename = Connection.ToString(dt.Rows[0]["BillingStatename"]);
                result.BillingpinCode = Connection.ToString(dt.Rows[0]["BillingPINCode"]);
            }
            return result;
        }

        public List<CLayer.SupplierPaymentSchedule> GetAllSupplierPayments(long OfflineBookId)
        {
            CLayer.SupplierPaymentSchedule result = null;
            List<CLayer.SupplierPaymentSchedule> resultList = new List<CLayer.SupplierPaymentSchedule>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));

            DataTable dt = Connection.GetTable("OfflineBooking_GetAllSupplierPayments", param);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    result = new CLayer.SupplierPaymentSchedule();
                    result.PaymentscheduleId = Connection.ToInteger(dr["PaymentscheduleId"]);
                    result.BookingId = Connection.ToInteger(dr["BookingId"]);
                    result.SupplierPaymentMode = Connection.ToInteger(dr["SupplierPaymentMode"]);
                    if (dr["SupplierPaymentDate"].ToString() != "")
                    {
                        result.SupplierPaymentModeDate = GetDBDatetimeValue(dr, "SupplierPaymentDate");
                    }
                    result.SupplierPaymentAmount = Connection.ToDecimal(dr["SupplierPaymentAmount"]);
                    result.SupplierCreditDays = Connection.ToInteger(dr["SupplierCreditDays"]); resultList.Add(result);
                }

            }
            return resultList;
        }
        public DateTime GetDBDatetimeValue(DataRow dr, string FieldKey)
        {
            DataRow dResult = null;
            if ((dr[FieldKey] != DBNull.Value) || (dr[FieldKey] != null))
            {
                dResult = (DataRow)dr;
            }
            else
            {
                dr[FieldKey] = 0;
                dResult = (DataRow)dr;
            }

            return (DateTime)dr[FieldKey];
        }
        public CLayer.OfflineBooking GetAllCustomerDetails(long OfflineBookId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookId));

            DataTable dt = Connection.GetTable("OfflineBooking_GetAllCustomerDetails", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.OfflineBookingId = OfflineBookId;
                result.CustomerName = Connection.ToString(dt.Rows[0]["customername"]);
                result.CustomerEmail = Connection.ToString(dt.Rows[0]["customeremail"]);
                result.CustomerAddress = Connection.ToString(dt.Rows[0]["customeraddress"]);
                result.CustomerMobile = Connection.ToString(dt.Rows[0]["customermobile"]);
                result.CustomerpinCode = Connection.ToString(dt.Rows[0]["CustomerPinCode"]);
                result.CustomerType = Connection.ToInteger(dt.Rows[0]["CustomerType"]);

                result.CustomerCountry = Connection.ToInteger(dt.Rows[0]["customercountryid"]);
                result.CustomerCity = Connection.ToInteger(dt.Rows[0]["customercityid"]);
                result.CustomerState = Connection.ToInteger(dt.Rows[0]["customerstateid"]);

                result.CustomerCountryname = Connection.ToString(dt.Rows[0]["customercountryname"]);
                result.CustomerStatename = Connection.ToString(dt.Rows[0]["customerstatename"]);
                result.CustomerCityname = Connection.ToString(dt.Rows[0]["customercityname"]);
                result.OfflineBookingId = Connection.ToInteger(dt.Rows[0]["Offline_BookingId"]);

            }
            return result;
        }

        public List<CLayer.OfflineBooking> GetAllForSearch(string searchString, int searchItem, int start, int limit, int Status, long userId)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("puserId", DataPlug.DataType._BigInt, userId));
            DataSet ds = Connection.GetDataSet("OfflineBooking_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]),
                    CustomerId = Connection.ToLong(dr["CustomerId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    SupplierId = Connection.ToLong(dr["SupplierId"]),

                    CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
                    CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    GuestEmail = Connection.ToString(dr["GuestEmail"]),
                    GuestName = Connection.ToString(dr["GuestName"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),

                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    PropertyCaretakerName = Connection.ToString(dr["PropertyCaretakerName"]),
                    PropertyCountryname = Connection.ToString(dr["PropertyCountryname"]),
                    PropertyStatename = Connection.ToString(dr["PropertyStatename"]),
                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),
                    PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]),
                    PropertyEmail = Connection.ToString(dr["PropertyEmail"]),

                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    //SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    //SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    //SupplierAddress = Connection.ToString(dr["SupplierAddress"]),
                    //SupplierCountryname = Connection.ToString(dr["SupplierCountryname"]),
                    //SupplierStatename = Connection.ToString(dr["SupplierStatename"]),
                    //SupplierCityname = Connection.ToString(dr["SupplierCityname"]),

                    NoOfNight = Connection.ToLong(dr["NoOfNight"]),
                    NoOfPaxAdult = Connection.ToLong(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToLong(dr["NoOfPaxChild"]),
                    NoOfRooms = Connection.ToLong(dr["NoOfRooms"]),

                    Accommodationid = Connection.ToLong(dr["Accommodationid"]),
                    Accommodationname = Connection.ToString(dr["Accommodationname"]),

                    Accommodationtypeid = Connection.ToLong(dr["Accommodationtypeid"]),
                    AccommodatoinTypename = Connection.ToString(dr["AccommodatoinTypename"]),

                    StayCategory = Connection.ToLong(dr["StayCategory"]),
                    StayCategoryName = Connection.ToString(dr["StayCategoryName"]),

                    OtherService = Connection.ToString(dr["OtherService"]),
                    sendmailtocustomer = Connection.ToBoolean(dr["sendmailtocustomer"]),
                    sendmailtosupplier = Connection.ToBoolean(dr["sendmailtosupplier"]),


                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]),
                    TotalAmtForBuyPrice = Connection.ToDecimal(dr["TotalAmtForBuyPrice"]),
                    BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]),
                    StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]),
                    TotalAmtotherForBuyPrice = Connection.ToDecimal(dr["TotalAmtotherForBuyPrice"]),

                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    TotalAmtForSalePrice = Connection.ToDecimal(dr["TotalAmtForSalePrice"]),
                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),

                    TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]),
                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),
                    HotelConfirmationNo = Connection.ToString(dr["HotelConformationNo"]),
                    CreatedTime = Connection.ToDate(dr["CreatedDate"]),
                    SaveStatus = Connection.ToInteger(dr["SaveStatus"]),
                    CreatedBy = Connection.ToInteger(dr["CreatedBy"]),
                    CreatedName = Connection.ToString(dr["CraetedFirstname"]) + ' ' + Connection.ToString(dr["CraetedLastname"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return bookings;
        }

        public List<CLayer.OfflineBooking> GetAllForSearch_customer(string searchString, int searchItem, int start, int limit, int Status, long Userid, DateTime From, DateTime To)
        {

            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, Userid));

            param.Add(Connection.GetParameter("pFrom", DataPlug.DataType._DateTime, From));
            param.Add(Connection.GetParameter("pTo", DataPlug.DataType._DateTime, To));
            DataSet ds = Connection.GetDataSet("OfflineBooking_GetSearchByUser", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]),
                    CustomerId = Connection.ToLong(dr["CustomerId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    SupplierId = Connection.ToLong(dr["SupplierId"]),

                    CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
                    CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    GuestEmail = Connection.ToString(dr["GuestEmail"]),
                    GuestName = Connection.ToString(dr["GuestName"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),

                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    PropertyCaretakerName = Connection.ToString(dr["PropertyCaretakerName"]),
                    PropertyCountryname = Connection.ToString(dr["PropertyCountryname"]),
                    PropertyStatename = Connection.ToString(dr["PropertyStatename"]),
                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),
                    PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]),
                    PropertyEmail = Connection.ToString(dr["PropertyEmail"]),

                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    //SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    //SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    //SupplierAddress = Connection.ToString(dr["SupplierAddress"]),
                    //SupplierCountryname = Connection.ToString(dr["SupplierCountryname"]),
                    //SupplierStatename = Connection.ToString(dr["SupplierStatename"]),
                    //SupplierCityname = Connection.ToString(dr["SupplierCityname"]),

                    NoOfNight = Connection.ToLong(dr["NoOfNight"]),
                    NoOfPaxAdult = Connection.ToLong(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToLong(dr["NoOfPaxChild"]),
                    NoOfRooms = Connection.ToLong(dr["NoOfRooms"]),

                    Accommodationid = Connection.ToLong(dr["Accommodationid"]),
                    Accommodationname = Connection.ToString(dr["Accommodationname"]),

                    Accommodationtypeid = Connection.ToLong(dr["Accommodationtypeid"]),
                    AccommodatoinTypename = Connection.ToString(dr["AccommodatoinTypename"]),

                    StayCategory = Connection.ToLong(dr["StayCategory"]),
                    StayCategoryName = Connection.ToString(dr["StayCategoryName"]),

                    OtherService = Connection.ToString(dr["OtherService"]),
                    sendmailtocustomer = Connection.ToBoolean(dr["sendmailtocustomer"]),
                    sendmailtosupplier = Connection.ToBoolean(dr["sendmailtosupplier"]),


                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]),
                    TotalAmtForBuyPrice = Connection.ToDecimal(dr["TotalAmtForBuyPrice"]),
                    BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]),
                    StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]),
                    TotalAmtotherForBuyPrice = Connection.ToDecimal(dr["TotalAmtotherForBuyPrice"]),

                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    TotalAmtForSalePrice = Connection.ToDecimal(dr["TotalAmtForSalePrice"]),
                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),

                    TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]),
                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),

                    CreatedTime = Connection.ToDate(dr["CreatedDate"]),
                    SaveStatus = Connection.ToInteger(dr["SaveStatus"]),
                    CreatedBy = Connection.ToInteger(dr["CreatedBy"]),
                    CreatedName = Connection.ToString(dr["CraetedFirstname"]) + ' ' + Connection.ToString(dr["CraetedLastname"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return bookings;
        }

        public List<CLayer.OfflineBooking> GetAllForSearch_Manage(string searchString, int searchItem, int start, int limit, int Status, long Userid)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pUserid", DataPlug.DataType._BigInt, Userid));
            DataSet ds = Connection.GetDataSet("OfflineBooking_GetSearch_Manage", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]),
                    CustomerId = Connection.ToLong(dr["CustomerId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    SupplierId = Connection.ToLong(dr["SupplierId"]),

                    CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
                    CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    GuestEmail = Connection.ToString(dr["GuestEmail"]),
                    GuestName = Connection.ToString(dr["GuestName"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),

                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    PropertyCaretakerName = Connection.ToString(dr["PropertyCaretakerName"]),
                    PropertyCountryname = Connection.ToString(dr["PropertyCountryname"]),
                    PropertyStatename = Connection.ToString(dr["PropertyStatename"]),
                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),
                    PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]),
                    PropertyEmail = Connection.ToString(dr["PropertyEmail"]),

                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    //SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    //SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    //SupplierAddress = Connection.ToString(dr["SupplierAddress"]),
                    //SupplierCountryname = Connection.ToString(dr["SupplierCountryname"]),
                    //SupplierStatename = Connection.ToString(dr["SupplierStatename"]),
                    //SupplierCityname = Connection.ToString(dr["SupplierCityname"]),

                    NoOfNight = Connection.ToLong(dr["NoOfNight"]),
                    NoOfPaxAdult = Connection.ToLong(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToLong(dr["NoOfPaxChild"]),
                    NoOfRooms = Connection.ToLong(dr["NoOfRooms"]),

                    Accommodationid = Connection.ToLong(dr["Accommodationid"]),
                    Accommodationname = Connection.ToString(dr["Accommodationname"]),

                    Accommodationtypeid = Connection.ToLong(dr["Accommodationtypeid"]),
                    AccommodatoinTypename = Connection.ToString(dr["AccommodatoinTypename"]),

                    StayCategory = Connection.ToLong(dr["StayCategory"]),
                    StayCategoryName = Connection.ToString(dr["StayCategoryName"]),

                    OtherService = Connection.ToString(dr["OtherService"]),
                    sendmailtocustomer = Connection.ToBoolean(dr["sendmailtocustomer"]),
                    sendmailtosupplier = Connection.ToBoolean(dr["sendmailtosupplier"]),


                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]),
                    TotalAmtForBuyPrice = Connection.ToDecimal(dr["TotalAmtForBuyPrice"]),
                    BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]),
                    StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]),
                    TotalAmtotherForBuyPrice = Connection.ToDecimal(dr["TotalAmtotherForBuyPrice"]),

                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    TotalAmtForSalePrice = Connection.ToDecimal(dr["TotalAmtForSalePrice"]),
                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),

                    TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]),
                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),

                    CreatedTime = Connection.ToDate(dr["CreatedDate"]),
                    SaveStatus = Connection.ToInteger(dr["SaveStatus"]),
                    CreatedBy = Connection.ToInteger(dr["CreatedBy"]),
                    CreatedName = Connection.ToString(dr["CraetedFirstname"]) + ' ' + Connection.ToString(dr["CraetedLastname"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    booking_creationdate = Connection.ToDate(dr["booking_creationdate"]),

                    SupplierPaymentName = Connection.ToString(dr["SupplierPaymentName"]),
                    CustomerPaymentName = Connection.ToString(dr["CustomerPaymentName"]),
                    SupplierPaymentMode = Connection.ToInteger(dr["SupplierPaymentMode"]),
                    CustomerPaymentMode = Connection.ToInteger(dr["CustomerPaymentMode"]),
                    CreditDays = Connection.ToDecimal(dr["CustomerCreditDays"]),
                    SupplierCreditDays = Connection.ToInteger(dr["SupplierCreditDays"]),

                });
            }
            return bookings;
        }

        public List<CLayer.OfflineBooking> GetAllPropertyByCusPropId(string searchString, int searchItem, int start, int limit)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));

            DataSet ds = Connection.GetDataSet("OfflineBooking_GetCustomPropertySearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    CustomPropertyId = Connection.ToLong(dr["CustomPropertyId"]),

                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    PropertyCaretakerName = Connection.ToString(dr["PropertyCaretakerName"]),
                    PropertyCountryname = Connection.ToString(dr["PropertyCountryname"]),
                    PropertyStatename = Connection.ToString(dr["PropertyStatename"]),
                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),
                    PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]),
                    PropertyEmail = Connection.ToString(dr["PropertyEmail"]),

                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    //SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    //SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    //SupplierAddress = Connection.ToString(dr["SupplierAddress"]),
                    //SupplierCountryname = Connection.ToString(dr["SupplierCountryname"]),
                    //SupplierStatename = Connection.ToString(dr["SupplierStatename"]),
                    //SupplierCityname = Connection.ToString(dr["SupplierCityname"]),

                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return bookings;
        }


        public List<CLayer.OfflineBooking> GetAlCustomerList(string searchString, int start, int limit)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            param.Add(Connection.GetParameter("pSearch", DataPlug.DataType._Varchar, searchString));
            DataSet ds = Connection.GetDataSet("OfflineBooking_GetOfflineBookingAllCustomerListFromMaster", param);


            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    CustomerId = Connection.ToLong(dr["CustomerId"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    CustomerCountryname = Connection.ToString(dr["CustomerCountryName"]),
                    CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    CustomerType = Connection.ToInteger(dr["CustomerType"]),
                    NoInvoiceMail = Connection.ToBoolean(dr["NoInvoiceMail"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return bookings;
        }

        public List<CLayer.OfflineBooking> GetAllBookingsByCusPropId(long CustomPropertyId, int start, int limit)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pCustomPropertyId", DataPlug.DataType._BigInt, CustomPropertyId));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));

            DataSet ds = Connection.GetDataSet("OfflineBooking_GetAllBookingsByCusPropId", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    CustomPropertyId = Connection.ToLong(dr["CustomPropertyId"]),
                    ConfirmationNumber = Connection.ToString(dr["ConfirmationNumber"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    BookingDate = Connection.ToString(dr["BookingDate"]),

                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return bookings;
        }


        public CLayer.OfflineBooking UserDetailsByOffliceBookingId(CLayer.OfflineBooking data)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("PCustomerId", DataPlug.DataType._BigInt, data.CustomerId));

            DataTable dt = Connection.GetTable("GetUserDetailsByOffliceBookingId", param);

            if (dt.Rows.Count > 0)
            {



                result = new CLayer.OfflineBooking();
                result.CustomerName = Connection.ToString(dt.Rows[0]["FirstName"]);
                result.CustomerEmail = Connection.ToString(dt.Rows[0]["Email"]);
                result.CustomerAddress = Connection.ToString(dt.Rows[0]["Address"]);
                result.CustomerCity = Connection.ToInteger(dt.Rows[0]["CityId"]);
                result.CustomerCountry = Connection.ToInteger(dt.Rows[0]["Country"]);
                result.CustomerState = Connection.ToInteger(dt.Rows[0]["State"]);
                result.CustomerCityname = Connection.ToString(dt.Rows[0]["City"]);
                result.CustomerMobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.CustomerpinCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                result.UserType = Connection.ToInteger(dt.Rows[0]["UserType"]);
                if (result.UserType == (int)CLayer.Role.Roles.Corporate)
                {
                    result.BillingAddress = Connection.ToString(dt.Rows[0]["Address"]);
                    result.BillingState = Connection.ToInteger(dt.Rows[0]["State"]);
                    result.BillingCity = Connection.ToInteger(dt.Rows[0]["CityId"]);
                    result.PinCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                    result.BillingMobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                    //  result.ContactPerson = Connection.ToString(dt.Rows[0]["TotalAmountaccnbuyprice"]);
                    // result.OfficeNO = Connection.ToString(dt.Rows[0]["TotalAmountaccnbuyprice"]);
                    //  result.GuestName1 = Connection.ToString(dt.Rows[0]["TotalAmountaccnbuyprice"]);
                    // result.GuestEmail1 = Connection.ToString(dt.Rows[0]["TotalAmountaccnbuyprice"]);
                    result.BillingCountryId = Connection.ToInteger(dt.Rows[0]["Country"]);
                    result.BillingCityname = Connection.ToString(dt.Rows[0]["City"]);

                }


            }
            return result;
        }

        public List<object> GetSupplier(string term)
        {


            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            DataTable dt = Connection.GetTable("Supplier_autocompelete", param);
            List<object> result = new List<object>();
            string SupplierName;
            int CustomPropertyId;
            foreach (DataRow dr in dt.Rows)
            {
                SupplierName = Connection.ToString(dr["SupplierName"]);
                CustomPropertyId = Connection.ToInteger(dr["CustomPropertyId"]);
                result.Add(new { value = SupplierName, id = CustomPropertyId });

            }

            return result;

        }

        public DataTable GetGSTInvoiceReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null,long LoginUserid=0)
        {
            List<CLayer.OfflineBooking> CustomerInvoiceReport = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._BigInt, Limit));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pLoginUserid", DataPlug.DataType._BigInt, LoginUserid)); 
            DataTable ds = Connection.GetTable("Report_GSTInvoice", param);

            return ds;

        }

        public List<CLayer.OfflineBooking> CustomerInvoiceGSTReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            List<CLayer.OfflineBooking> CustomerInvoiceReport = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._BigInt, Limit));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));

            DataSet ds = Connection.GetDataSet("GetCustomerInvoiceGSTReport", param);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CustomerInvoiceReport.Add(new CLayer.OfflineBooking()
                {

                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),
                    AccommodatoinTypename = Connection.ToString(dr["Title"]),
                    InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    InvoiceDate = Connection.ToDate(dr["InvoiceDate"]),
                    Guestname = Connection.ToString(dr["Guestname"]),
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),
                    Noofnight = Connection.ToInteger(dr["Noofnight"]),
                    NoOfRooms = Connection.ToInteger(dr["NoOfRooms"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    FirstName = Connection.ToString(dr["Name"]),
                    City = Connection.ToString(dr["cityname"]),
                    //Swatchbharath = Connection.ToDouble(dr["Swatch_bharath"]),
                    //KrishiKalyan = Connection.ToDouble(dr["Krishi_Kalyan"]),
                    //SwatchbharathOthers = Connection.ToDouble(dr["Swatch_bharath_others"]),
                    //KrishiKalyanOthers = Connection.ToDouble(dr["Krishi_Kalyan_others"]),
                    //StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    NoOfPaxAdult = Connection.ToInteger(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToInteger(dr["NoOfPaxChild"]),
                    Natureofservice = Connection.ToString(dr["Natureofservice"]),
                    Valuebeforeservicetax = Connection.ToInteger(dr["Valuebeforeservicetax"]),
                    Totalamountpayable = Connection.ToInteger(dr["Totalamountpayable"]),
                    SalesPerson = Connection.ToString(dr["SalesPerson"]),

                    CustomerStatename = Connection.ToString(dr["customerstate"]),
                    PropertyStatename = Connection.ToString(dr["propertystate"]),
                    SBEntityName = Connection.ToString(dr["BillingEntity"]),

                    //bSGST = Connection.ToDouble(dr["bSGST"]),
                    //bCGST = Connection.ToDouble(dr["bCGST"]),
                    //bIGST = Connection.ToDouble(dr["bIGST"]),
                    //obSGST = Connection.ToDouble(dr["obSGST"]),
                    //obCGST = Connection.ToDouble(dr["obCGST"]),
                    //obIGST = Connection.ToDouble(dr["obIGST"]),
                    SGST = Connection.ToDouble(dr["SGST"]),
                    CGST = Connection.ToDouble(dr["CGST"]),
                    IGST = Connection.ToDouble(dr["IGST"]),
                    btSGST = Connection.ToDouble(dr["btSGST"]),
                    btCGST = Connection.ToDouble(dr["btCGST"]),
                    btIGST = Connection.ToDouble(dr["btIGST"]),
                    oSGST = Connection.ToDouble(dr["oSGST"]),
                    oCGST = Connection.ToDouble(dr["oCGST"]),
                    oIGST = Connection.ToDouble(dr["oIGST"]),

                    DirectAmount = Connection.ToDouble(dr["DirectAmount"]),
                    BookingTypeInt = Connection.ToInteger(dr["BookingType"]),
                    ReimbursementsAmount = Connection.ToDecimal(dr["Reimbursements"]),
                    DiscountAmount = Connection.ToDecimal(dr["Discount"]),
                    OthersAmount = Connection.ToDecimal(dr["Others"]),

                    //      VBuyPriceBeforeTax = Connection.ToDouble(dr["VBuyPriceBeforeTax"]);
                    VSalePriceBeforeTax = Connection.ToDouble(dr["VSalePriceBeforeTax"]),
                    //     tmp.VBuyPriceTotal = Connection.ToDouble(dr["VBuyPriceTotal"]);
                    vSalePriceTotal = Connection.ToDouble(dr["vSalePriceTotal"]),
                    //      tmp.vBuyTax = Connection.ToDouble(dr["vBuyTax"]);
                    vSaleGST = Connection.ToDouble(dr["vSaleGST"]),
                    TotalNights = Connection.ToInteger(dr["totalnights"])

                });
            }

            return CustomerInvoiceReport;
        }
        public List<CLayer.OfflineBooking> offlinePaymentReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            List<CLayer.OfflineBooking> offlinePaymentReport = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._BigInt, Limit));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));

            DataSet ds = Connection.GetDataSet("GetofflinePaymentReport", param);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                offlinePaymentReport.Add(new CLayer.OfflineBooking()
                {

                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),
                    AccommodatoinTypename = Connection.ToString(dr["Title"]),

                    PropertyName = Connection.ToString(dr["PropertyName"]),

                    Guestname = Connection.ToString(dr["Guestname"]),
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),
                    Noofnight = Connection.ToInteger(dr["Noofnight"]),
                    NoOfRooms = Connection.ToInteger(dr["NoOfRooms"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    FirstName = Connection.ToString(dr["Name"]),
                    City = Connection.ToString(dr["cityname"]),

                    NoOfPaxAdult = Connection.ToInteger(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToInteger(dr["NoOfPaxChild"]),

                    SalesPerson = Connection.ToString(dr["SalesPerson"]),

                    CustomerStatename = Connection.ToString(dr["customerstate"]),
                    PropertyStatename = Connection.ToString(dr["propertystate"]),
                    PropertyCityname = Connection.ToString(dr["cityname"]),
                    SBEntityName = Connection.ToString(dr["BillingEntity"]),


                    SGST = Connection.ToDouble(dr["SGST"]),
                    CGST = Connection.ToDouble(dr["CGST"]),
                    IGST = Connection.ToDouble(dr["IGST"]),
                    btSGST = Connection.ToDouble(dr["btSGST"]),
                    btCGST = Connection.ToDouble(dr["btCGST"]),
                    btIGST = Connection.ToDouble(dr["btIGST"]),
                    oSGST = Connection.ToDouble(dr["oSGST"]),
                    oCGST = Connection.ToDouble(dr["oCGST"]),
                    oIGST = Connection.ToDouble(dr["oIGST"]),

                    DirectAmount = Connection.ToDouble(dr["DirectAmount"]),
                    BookingTypeInt = Connection.ToInteger(dr["BookingType"]),
                    ReimbursementsAmount = Connection.ToDecimal(dr["Reimbursements"]),
                    DiscountAmount = Connection.ToDecimal(dr["Discount"]),
                    OthersAmount = Connection.ToDecimal(dr["Others"]),

                    //      VBuyPriceBeforeTax = Connection.ToDouble(dr["VBuyPriceBeforeTax"]);
                    //VSalePriceBeforeTax = Connection.ToDouble(dr["VSalePriceBeforeTax"]),
                    ////     tmp.VBuyPriceTotal = Connection.ToDouble(dr["VBuyPriceTotal"]);
                    //vSalePriceTotal = Connection.ToDouble(dr["vSalePriceTotal"]),
                    ////      tmp.vBuyTax = Connection.ToDouble(dr["vBuyTax"]);
                    //vSaleGST = Connection.ToDouble(dr["vSaleGST"]),
                    TotalNights = Connection.ToInteger(dr["totalnights"]),

                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),
                    TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]),

                    SupplierPaymentMode = Connection.ToInteger(dr["totalnights"]),
                    PaymentScheduleDate1 = Connection.ToDate(dr["PaymentScheduleDate1"]),
                    PaymentScheduleDate2 = Connection.ToDate(dr["PaymentScheduleDate2"]),
                    PaymentScheduleDate3 = Connection.ToDate(dr["PaymentScheduleDate3"]),
                    PaymentScheduleAmount1 = Connection.ToDecimal(dr["PaymentScheduleAmount1"]),
                    PaymentScheduleAmount2 = Connection.ToDecimal(dr["PaymentScheduleAmount2"]),
                    PaymentScheduleAmount3 = Connection.ToDecimal(dr["PaymentScheduleAmount3"]),
                    SalesRegion = Connection.ToString(dr["Region"]),
                    BankName = Connection.ToString(dr["BankName"]),
                    BranchName = Connection.ToString(dr["BranchName"]),
                    BeneficiaryName = Connection.ToString(dr["BeneficiaryName"]),
                    AccountNo = Connection.ToString(dr["AccountNo"]),
                    AccountType = Connection.ToString(dr["AccountType"]),
                    IFSCcode = Connection.ToString(dr["IFSCCode"]),
                    PropertyEmailID = Connection.ToString(dr["PropertyEmailID"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerPaymentModeId = Connection.ToInteger(dr["CustomerPaymentMode"]),
                    CustomerPayment = GetCustomerPaymentMode(Connection.ToInteger(dr["CustomerPaymentMode"]))


                });
            }

            return offlinePaymentReport;
        }

        public DataTable BankUploadReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null, bool IsExcelDownload = true)
        {
            DataTable offlinePaymentReport = new DataTable();
            //List<CLayer.OfflineBooking> offlinePaymentReport = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._BigInt, Limit));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));

            DataSet ds = Connection.GetDataSet("GetBankUploadReport", param);

            offlinePaymentReport = ds.Tables[0];
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    offlinePaymentReport.Add(new CLayer.OfflineBooking()
            //    {
            //        PropertyBank = Connection.ToString(dr["PropertyBank"]),
            //        TotalSalePrice = Connection.ToDecimal(dr["AmountPayable"]),
            //        BeneficiaryName = Connection.ToString(dr["BeneficiaryName"]),
            //        AccountNumber= Connection.ToString(dr["AccountNo"]),
            //        PropertyEmail = Connection.ToString(dr["PropertyEmail"]),
            //        IFSCCode =Connection.ToString(dr["IFSCCode"]),
            //        AccountType= Connection.ToString(dr["AccountType"]),
            //        AccountTypeCode = Connection.ToString(dr["AccountTypeCode"])
            //    });
            //}

            return offlinePaymentReport;
        }

        public List<CLayer.OfflineBooking> BankUploadReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            List<CLayer.OfflineBooking> offlinePaymentReport = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._BigInt, Limit));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));

            DataSet ds = Connection.GetDataSet("GetBankUploadReport", param);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                offlinePaymentReport.Add(new CLayer.OfflineBooking()
                {
                    PropertyBank = Connection.ToString(dr["PropertyBank"]),
                    AmountPayableValue = Connection.ToString(dr["AmountPayableValue"]),
                    BeneficiaryName = Connection.ToString(dr["BeneficiaryName"]),
                    BankUploadReportDate = Connection.ToString(dr["BankUploadReportDate"]),
                    AccountNumber = Connection.ToString(dr["AccountNo"]),
                    PropertyEmail = Connection.ToString(dr["PropertyEmail"]),
                    BankUploadText1 = Connection.ToString(dr["BankUploadText1"]),
                    BankUploadText2 = Connection.ToString(dr["BankUploadText2"]),
                    BankUploadTextBlank = Connection.ToString(dr["BankUploadTextBlank"]),
                    IFSCCode = Connection.ToString(dr["IFSCCode"]),
                    AccountTypeCode = Connection.ToString(dr["AccountTypeCode"]),
                    StayBazarWebsite = Connection.ToString(dr["StayBazarWebsite"])
                });
            }

            return offlinePaymentReport;
        }
        private string GetCustomerPaymentMode(int pModeID = 0)
        {
            string CustomerPaymentMode = string.Empty;

            switch (pModeID)
            {
                case 1:
                    CustomerPaymentMode = "Advance Payment";
                    break;
                case 2:
                    CustomerPaymentMode = "Payment On CheckOut";
                    break;
                case 3:
                    CustomerPaymentMode = "Credit";
                    break;
            }
            return CustomerPaymentMode;
        }
        public List<CLayer.OfflineBooking> CustomerInvoiceReport(string SearchString, long Limit, DateTime? fromDT = null, DateTime? toDT = null)
        {
            List<CLayer.OfflineBooking> CustomerInvoiceReport = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._BigInt, Limit));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));

            DataSet ds = Connection.GetDataSet("GetCustomerInvoiceReport", param);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CustomerInvoiceReport.Add(new CLayer.OfflineBooking()
                {

                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),
                    AccommodatoinTypename = Connection.ToString(dr["Title"]),
                    InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    InvoiceDate = Connection.ToDate(dr["InvoiceDate"]),
                    Guestname = Connection.ToString(dr["Guestname"]),
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),
                    Noofnight = Connection.ToInteger(dr["Noofnight"]),
                    NoOfRooms = Connection.ToInteger(dr["NoOfRooms"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    FirstName = Connection.ToString(dr["Name"]),
                    City = Connection.ToString(dr["cityname"]),
                    Swatchbharath = Connection.ToDouble(dr["Swatch_bharath"]),
                    KrishiKalyan = Connection.ToDouble(dr["Krishi_Kalyan"]),
                    SwatchbharathOthers = Connection.ToDouble(dr["Swatch_bharath_others"]),
                    KrishiKalyanOthers = Connection.ToDouble(dr["Krishi_Kalyan_others"]),
                    StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    NoOfPaxAdult = Connection.ToInteger(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToInteger(dr["NoOfPaxChild"]),
                    Natureofservice = Connection.ToString(dr["Natureofservice"]),
                    Valuebeforeservicetax = Connection.ToInteger(dr["Valuebeforeservicetax"]),
                    Servicetaxamount = Connection.ToInteger(dr["Servicetaxamount"]),
                    Totalamountpayable = Connection.ToInteger(dr["Totalamountpayable"]),
                    SalesPerson = Connection.ToString(dr["SalesPerson"])


                });
            }

            return CustomerInvoiceReport;
        }




        public CLayer.OfflineBooking OfflineBookingAlreadyExistsChecking(string CustomerName, string GuestName, string PropertyName, string CheckIn, string CheckOut, long SalesPersonId, long OfflineBookingId)
        {
            CLayer.OfflineBooking result = new CLayer.OfflineBooking();
            result.result = "";
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustName", DataPlug.DataType._Varchar, CustomerName));
            param.Add(Connection.GetParameter("pGuestname", DataPlug.DataType._Varchar, GuestName));
            param.Add(Connection.GetParameter("pPrtyName", DataPlug.DataType._Varchar, PropertyName));
            param.Add(Connection.GetParameter("PcheckIN", DataPlug.DataType._Varchar, CheckIn));
            param.Add(Connection.GetParameter("Pcheckout", DataPlug.DataType._Varchar, CheckOut));
            param.Add(Connection.GetParameter("POfflineBookingId", DataPlug.DataType._Varchar, OfflineBookingId));
            param.Add(Connection.GetParameter("pSalesPersonId", DataPlug.DataType._Varchar, SalesPersonId));

            StringBuilder sw = new StringBuilder();
            sw.Append("0");
            DataTable dt = Connection.GetTable("OfflineBookingAlreadyExistsChecking", param);
            if (dt.Rows.Count > 0)
            {
                sw.Clear();
                foreach (DataRow dr in dt.Rows)
                {
                    var abc = Connection.ToString(dt.Rows[0]["Orderno"]);

                    sw.Append(abc);
                    sw.Append(",");
                }
            }

            result.result = sw.ToString();

            return result;
        }

        //public List<CLayer.OfflineBooking> SaveGSTIn(string GSTRegNo, string StateName, int StateID, int CustID,int OffGSTId)
        //{
        //    List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
        //    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

        //    param.Add(Connection.GetParameter("pOffGSTId", DataPlug.DataType._Int, OffGSTId));
        //    param.Add(Connection.GetParameter("pStateID", DataPlug.DataType._Int, StateID));
        //    param.Add(Connection.GetParameter("pCustID", DataPlug.DataType._Int, CustID));
        //    param.Add(Connection.GetParameter("pGSTRegNo", DataPlug.DataType._Varchar, GSTRegNo));
        //    param.Add(Connection.GetParameter("pStateName", DataPlug.DataType._Varchar, StateName));



        //    DataSet ds = Connection.GetDataSet("OfflineBooking_Customer_SaveGSt", param);


        //    return bookings;
        //}


        public void SaveGSTIn(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOffGSTId", DataPlug.DataType._Int, data.OffGSTId));
            param.Add(Connection.GetParameter("pSubCustomerGstStateId", DataPlug.DataType._Int, data.SubCustomerGstStateId));
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._Int, data.SubCustomerid));
            param.Add(Connection.GetParameter("pCustomerGstRegNo", DataPlug.DataType._Varchar, data.SubCustomerGstRegNo));
            param.Add(Connection.GetParameter("pCustomerTableType", DataPlug.DataType._Int, data.CustomerTableType));

            param.Add(Connection.GetParameter("pSubCustomerAddress", DataPlug.DataType._Varchar, data.SubCustomerAddress));
            param.Add(Connection.GetParameter("pSubCustomerCity", DataPlug.DataType._BigInt, data.SubCustomerCity));
            param.Add(Connection.GetParameter("pSubCustomerCityname", DataPlug.DataType._Varchar, data.SubCustomerCityname));
            param.Add(Connection.GetParameter("pSubCustomerpinCode", DataPlug.DataType._Varchar, data.SubCustomerpinCode));


            //param.Add(Connection.GetParameter("pStateName", DataPlug.DataType._Varchar, StateName));
            Connection.ExecuteQueryScalar("OfflineBooking_Customer_SaveGST", param);
        }
        public void GSTDelete(int OFFGSTID)
        {
            string sql = "Delete From offlinecustomergst_details Where OffGSTId=" + OFFGSTID.ToString();
            Connection.ExecuteSqlQuery(sql);

        }


        public List<CLayer.OfflineBooking> GSTList(int Cust)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pcustid", DataPlug.DataType._Int, Cust));
            DataSet ds = Connection.GetDataSet("GetGSTListForOfflineCustomer", param);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OffGSTId = Connection.ToLong(dr["OffGSTId"]),
                    GSTstateid = Connection.ToLong(dr["Stateid"]),
                    GSTRegistrationNo = Connection.ToString(dr["GSTRegNO"]),
                    StateOfRegistration = Connection.ToString(dr["StateName"]),

                    //SubCustomerCity = Connection.ToInteger(dr["CityId"]),
                    //SubCustomerCityname = Connection.ToString(dr["CityName"]),
                    //SubCustomerAddress = Connection.ToString(dr["Address"]),
                    //SubCustomerpinCode = Connection.ToString(dr["PinCode"])

                    //CustomerEmail = Connection.ToString(dr["OfflinebookingCustomerId"]),

                });
            }
            return bookings;
        }




        public long SavePayee(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPayeeID", DataPlug.DataType._BigInt, data.PayeeID));
            param.Add(Connection.GetParameter("pAmountPayable", DataPlug.DataType._BigInt, data.AmountPayable));
            param.Add(Connection.GetParameter("pPayeeName", DataPlug.DataType._Varchar, data.PayeeName));
            param.Add(Connection.GetParameter("pAccountNumber", DataPlug.DataType._Varchar, data.AccountNumber));
            param.Add(Connection.GetParameter("pIFSCcode", DataPlug.DataType._Varchar, data.IFSCcode));
            param.Add(Connection.GetParameter("pBankName", DataPlug.DataType._Varchar, data.BankName));
            param.Add(Connection.GetParameter("pBranchName", DataPlug.DataType._Varchar, data.BranchName));
            param.Add(Connection.GetParameter("pPAN", DataPlug.DataType._Varchar, data.PAN));
            object result = Connection.ExecuteQueryScalar("SavePayee", param);
            return Connection.ToLong(result);
        }


        public string GetGSTStateCode(int stateid)
        {
            string sql = "SELECT GSTStateCode FROM state WHERE StateId=" + stateid.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }

        public string StaybazarBookingEntity(int bookingid)
        {
            string sql = "SELECT B.StateId FROM offline_bookings AS A JOIN  sbentity AS B ON A.SBEntityEntityIdBilling = B.EntityId WHERE Offline_BookingId = " + bookingid.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public string GetBookingCustomerStateID(int bookingid)
        {
            string sql = "SELECT  StateId FROM offlinebooking_customer WHERE Offline_BookingId = " + bookingid.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }

        //karthikms added on 4-11-2019 2801-2806
        public string GetBookingPropertyStateID(int bookingid)
        {
            string sql = "SELECT  PropertyState FROM offlinebooking_property WHERE Offline_BookingId = " + bookingid.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public string StaybazarGstSlab(int SlabCode,decimal Amt)
        {
            string slab = "SELECT HSNCode from hsncode where CodeId = " + SlabCode.ToString();
            object obj1 = Connection.ExecuteSQLQueryScalar(slab);
            string sql = "SELECT  gst_igst_rate FROM mac_gst_slabs WHERE gst_sac_code = '" + obj1.ToString() + "' and gst_slab_from <= " + Amt.ToString() + " and gst_slab_to >=" + Amt.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }

        public long GetBillingStateId(long entityid)
        {
            string sql = "SELECT stateid FROM SBEntity WHERE entityid=" + entityid.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        

        public long SaveMultipleBooking(CLayer.OfflineBooking data)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pGuestName", DataPlug.DataType._Varchar, data.GuestName));
            param.Add(Connection.GetParameter("pGuestEmail", DataPlug.DataType._Varchar, data.GuestEmail));
            param.Add(Connection.GetParameter("pAccommodationid", DataPlug.DataType._BigInt, data.Accommodationid));
            param.Add(Connection.GetParameter("pAccommodationtypeid", DataPlug.DataType._BigInt, data.Accommodationtypeid));
            param.Add(Connection.GetParameter("pStayCategoryid", DataPlug.DataType._BigInt, data.StayCategoryid));
            param.Add(Connection.GetParameter("pStayCategoryName", DataPlug.DataType._Varchar, data.StayCategoryName));
            param.Add(Connection.GetParameter("pNoOfNight", DataPlug.DataType._BigInt, data.NoOfNight));
            param.Add(Connection.GetParameter("pNoOfRooms", DataPlug.DataType._BigInt, data.NoOfRooms));
            param.Add(Connection.GetParameter("pNoOfPaxAdult", DataPlug.DataType._BigInt, data.NoOfPaxAdult));
            param.Add(Connection.GetParameter("pNoOfPaxChild", DataPlug.DataType._BigInt, data.NoOfPaxChild));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._DateTime, data.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._DateTime, data.CheckOut));
            param.Add(Connection.GetParameter("pAccommodatoinTypename", DataPlug.DataType._Varchar, data.AccommodatoinTypename));


            param.Add(Connection.GetParameter("pTotalAmtForBuyPrice", DataPlug.DataType._Decimal, data.TotalAmtForBuyPrice));
            param.Add(Connection.GetParameter("pTotalAmtotherForBuyPrice", DataPlug.DataType._Decimal, data.TotalAmtotherForBuyPrice));
            param.Add(Connection.GetParameter("pAvgDailyRateBefreStaxForBuyPrice", DataPlug.DataType._Decimal, data.AvgDailyRateBefreStaxForBuyPrice));
            param.Add(Connection.GetParameter("pBuyPriceforotherservicesForBuyprice", DataPlug.DataType._Decimal, data.BuyPriceforotherservicesForBuyprice));
            param.Add(Connection.GetParameter("pStaxForBuyPrice", DataPlug.DataType._Decimal, data.StaxForBuyPrice));
            param.Add(Connection.GetParameter("pStaxForotherBuyPrice", DataPlug.DataType._Decimal, data.StaxForotherBuyPrice));

            param.Add(Connection.GetParameter("pTotalAmtForSalePrice", DataPlug.DataType._Decimal, data.TotalAmtForSalePrice));
            param.Add(Connection.GetParameter("pTotalAmtotherForSalePrice", DataPlug.DataType._Decimal, data.TotalAmtotherForSalePrice));
            param.Add(Connection.GetParameter("pAvgDailyRateBefreStaxForSalePrice", DataPlug.DataType._Decimal, data.AvgDailyRateBefreStaxForSalePrice));
            param.Add(Connection.GetParameter("pBuyPriceforotherservicesForSalePrice", DataPlug.DataType._Decimal, data.BuyPriceforotherservicesForSalePrice));
            param.Add(Connection.GetParameter("pStaxForSalePrice", DataPlug.DataType._Decimal, data.StaxForSalePrice));
            param.Add(Connection.GetParameter("pStaxForotherForSalePrice", DataPlug.DataType._Decimal, data.StaxForotherForSalePrice));


            param.Add(Connection.GetParameter("pTotalBuyPrice", DataPlug.DataType._Varchar, data.TotalBuyPrice));
            param.Add(Connection.GetParameter("pTotalSalePrice", DataPlug.DataType._Varchar, data.TotalSalePrice));
            param.Add(Connection.GetParameter("pHotelConformationNo", DataPlug.DataType._Varchar, data.HotelConfirmationNo));
            param.Add(Connection.GetParameter("pHSNCode", DataPlug.DataType._BigInt, data.HSNCodeCodeID));
            param.Add(Connection.GetParameter("pSupplierCancellationDone", DataPlug.DataType._BigInt, data.SupplierCancellationDone));
            param.Add(Connection.GetParameter("pGuestPhone", DataPlug.DataType._Varchar, data.GuestPhone));
            param.Add(Connection.GetParameter("pNatureOfOtherService", DataPlug.DataType._Varchar, data.OtherServiceNature));
            param.Add(Connection.GetParameter("pPlaceOfSupply", DataPlug.DataType._Int, data.PlaceOfSupply));
            param.Add(Connection.GetParameter("pHSNCodeForSalesService", DataPlug.DataType._Int, data.HSNCodeForSalesService));

            object result = Connection.ExecuteQueryScalar("SaveMultipleBooking", param);

            return Connection.ToLong(result);


        }
        public List<CLayer.OfflineBooking> BookedList(long id)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("Pid", DataPlug.DataType._BigInt, id));
            DataTable dt = Connection.GetTable("OfflineBookedListGST", param);

            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            CLayer.OfflineBooking temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.OfflineBooking();
                temp.BookingId = Connection.ToLong(dr["BookingId"]);
                temp.OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]);
                temp.CheckIn = Connection.ToDate(dr["CheckIn_date"]);
                temp.CheckOut = Connection.ToDate(dr["CheckOut_date"]);
                temp.HotelConfirmationNo = Connection.ToString(dr["HotelConformationNo"]);
                result.Add(temp);
            }

            return result;
        }
        public CLayer.OfflineBooking EditBookedDetails(long id)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("Pid", DataPlug.DataType._BigInt, id));
            DataTable dt = Connection.GetTable("GetBookedDetailsGST", param);

            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            foreach (DataRow dr in dt.Rows)
            {
                data = new CLayer.OfflineBooking();
                data.Accommodationid = Connection.ToLong(dr["Accommodationid"]);
                data.Accommodationtypeid = Connection.ToLong(dr["AccommodatoinTypeId"]);
                data.AccommodatoinTypename = Connection.ToString(dr["AccommodatoinTypename"]);
                data.StayCategoryName = Connection.ToString(dr["StayCategoryName"]);
                data.StayCategoryid = Connection.ToLong(dr["StayCategoryid"]);
                data.NoOfNight = Connection.ToLong(dr["NoOfNight"]);
                data.NoOfRooms = Connection.ToLong(dr["NoOfRooms"]);
                data.NoOfPaxAdult = Connection.ToLong(dr["NoOfPaxAdult"]);
                data.NoOfPaxChild = Connection.ToLong(dr["NoOfPaxChild"]);
                data.CheckIn = Connection.ToDate(dr["CheckIn_date"]);
                data.CheckOut = Connection.ToDate(dr["CheckOut_date"]);

                data.GuestEmail = Connection.ToString(dr["GuestEmail"]);
                data.GuestName = Connection.ToString(dr["GuestName"]);
                data.TotalAmtForBuyPrice = Connection.ToDecimal(dr["TotalAmtForBuyPrice"]);
                data.AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]);
                data.StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]);
                data.BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]);
                data.StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]);
                data.TotalAmtotherForBuyPrice = Connection.ToDecimal(dr["TotalAmtotherForBuyPrice"]);
                data.TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]);

                data.TotalAmtForSalePrice = Connection.ToDecimal(dr["TotalAmtForSalePrice"]);
                data.AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]);
                data.StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]);
                data.BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]);
                data.StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]);
                data.TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]);
                data.TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]);
                data.HotelConfirmationNo = Connection.ToString(dr["HotelConformationNo"]);

                data.BookingId = Connection.ToLong(dr["BookingId"]);
                data.OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]);
                data.HSNCodeCodeID = Connection.ToInteger(dr["HSNCode"]);
                data.SupplierCancellationDone = Connection.ToBoolean(dr["SupplierCancellationDone"]);

                data.GuestPhone = Connection.ToString(dr["GuestPhone"]);
                data.OtherServiceNature = Connection.ToString(dr["NatureOfOtherService"]);

                data.PlaceOfSupply = Connection.ToInteger(dr["PlaceOfSupply"]);
                data.HSNCodeForSalesService = Connection.ToInteger(dr["HSNCodeForSalesService"]);


            }

            return data;
        }


        public List<object> GetAccommodationTypeName(string term)
        {


            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            DataTable dt = Connection.GetTable("AccommodationTypeName_autocompelete", param);
            List<object> result = new List<object>();
            string Title;
            int AccommodationTypeId;
            foreach (DataRow dr in dt.Rows)
            {
                Title = Connection.ToString(dr["Title"]);
                AccommodationTypeId = Connection.ToInteger(dr["AccommodationTypeId"]);
                result.Add(new { value = Title, id = AccommodationTypeId });

            }

            return result;

        }
        public List<object> GetVendor(string term)
        {


            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            DataTable dt = Connection.GetTable("Vendor_autocompelete", param);
            List<object> result = new List<object>();
            string Vendorsname;
            int VendorpaymentsId;
            foreach (DataRow dr in dt.Rows)
            {
                Vendorsname = Connection.ToString(dr["Vendorsname"]);
                VendorpaymentsId = Connection.ToInteger(dr["VendorpaymentsId"]);
                result.Add(new { value = Vendorsname, id = VendorpaymentsId });

            }

            return result;

        }

        public CLayer.OfflineBooking GetVendorDetails(long id)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pVendorpaymentsId", DataPlug.DataType._BigInt, id));


            DataTable dt = Connection.GetTable("GetVendorDetails", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();

                result.vendorId = Connection.ToLong(dt.Rows[0]["VendorpaymentsId"]);
                result.vendorname = Connection.ToString(dt.Rows[0]["Vendorsname"]);
                result.vendoraddress = Connection.ToString(dt.Rows[0]["Vendoraddress"]);
                result.address1 = Connection.ToString(dt.Rows[0]["Address1"]);
                result.address2 = Connection.ToString(dt.Rows[0]["Address2"]);
                result.City = Connection.ToString(dt.Rows[0]["City"]);
                result.pin = Connection.ToString(dt.Rows[0]["Pin"]);

                result.ContactPerson = Connection.ToString(dt.Rows[0]["Contactperson"]);
                result.contactno = Connection.ToString(dt.Rows[0]["Contactno"]);
                result.emailaddress = Connection.ToString(dt.Rows[0]["Emailaddress"]);

            }
            return result;
        }
        public CLayer.OfflineBooking GetVendorDetailsAutoComplete(long id)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pVendorpaymentsId", DataPlug.DataType._BigInt, id));


            DataTable dt = Connection.GetTable("GetVendorDetailsAutoComplete", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();

                result.vendorId = Connection.ToLong(dt.Rows[0]["VendorpaymentsId"]);
                result.vendorname = Connection.ToString(dt.Rows[0]["Vendorsname"]);
                result.vendoraddress = Connection.ToString(dt.Rows[0]["Vendoraddress"]);
                result.address1 = Connection.ToString(dt.Rows[0]["Address1"]);
                result.address2 = Connection.ToString(dt.Rows[0]["Address2"]);
                result.City = Connection.ToString(dt.Rows[0]["City"]);
                result.pin = Connection.ToString(dt.Rows[0]["Pin"]);

                result.ContactPerson = Connection.ToString(dt.Rows[0]["Contactperson"]);
                result.contactno = Connection.ToString(dt.Rows[0]["Contactno"]);
                result.emailaddress = Connection.ToString(dt.Rows[0]["Emailaddress"]);

            }
            return result;
        }
        public void SaveVendotTax(List<CLayer.TaxPercentage> data)
        {
            long i = 0;

            foreach (CLayer.TaxPercentage item in data)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("ptaxId", DataPlug.DataType._BigInt, item.TaxPerID));
                param.Add(Connection.GetParameter("poffline_Id", DataPlug.DataType._BigInt, item.TaxOfflineBookingID));
                param.Add(Connection.GetParameter("ptaxTitle", DataPlug.DataType._Varchar, item.TaxTitle));
                param.Add(Connection.GetParameter("ptaxPercentage", DataPlug.DataType._Varchar, item.TaxPercent));
                param.Add(Connection.GetParameter("ptaxType", DataPlug.DataType._Varchar, item.TaxType));
                param.Add(Connection.GetParameter("pType", DataPlug.DataType._Varchar, item.Type));
                param.Add(Connection.GetParameter("pvendorId", DataPlug.DataType._BigInt, item.vendorId));

                object res = Connection.ExecuteQueryScalar("Vendor_Tax_SAVE", param);

                i = Connection.ToLong(res);
            }



        }
        public List<CLayer.OfflineBooking> VendorList(long id)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("Pid", DataPlug.DataType._BigInt, id));
            DataTable dt = Connection.GetTable("VenderListGST", param);

            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            CLayer.OfflineBooking temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.OfflineBooking();
                temp.VendorpaymentsId = Connection.ToLong(dr["VendorpaymentsId"]);
                temp.vendorname = Connection.ToString(dr["Vendorsname"]);
                temp.SalePriceTotal = Connection.ToLong(dr["SalePriceTotal"]);
                temp.ByPriceTotal = Connection.ToLong(dr["ByPriceTotal"]);
                temp.natureofservice = Connection.ToString(dr["Natureofservice"]);
                result.Add(temp);
            }

            return result;
        }

        public CLayer.OfflineBooking EditVendorDetails(long id)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pVendorpaymentsId", DataPlug.DataType._BigInt, id));
            DataTable dt = Connection.GetTable("GetVendorDetails", param);

            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            foreach (DataRow dr in dt.Rows)
            {
                data = new CLayer.OfflineBooking();
                data.VendorpaymentsId = Connection.ToLong(dr["VendorpaymentsId"]);
                data.OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]);
                data.vendorname = Connection.ToString(dr["Vendorsname"]);
                data.vendoraddress = Connection.ToString(dr["Vendoraddress"]);
                data.address1 = Connection.ToString(dr["Address1"]);
                data.address2 = Connection.ToString(dr["Address2"]);
                data.City = Connection.ToString(dr["City"]);
                data.pin = Connection.ToString(dr["Pin"]);
                data.ContactPerson = Connection.ToString(dr["Contactperson"]);
                data.contactno = Connection.ToString(dr["Contactno"]);
                data.emailaddress = Connection.ToString(dr["Emailaddress"]);

                data.natureofservice = Connection.ToString(dr["Natureofservice"]);
                data.ByPriceBeforeTax = Connection.ToLong(dr["ByPriceBeforeTax"]);
                data.ByPriceTotal = Connection.ToLong(dr["ByPriceTotal"]);
                data.SalePriceBeforeTax = Connection.ToLong(dr["SalePriceBeforeTax"]);
                data.SalePriceTotal = Connection.ToLong(dr["SalePriceTotal"]);
                data.ByPriceGST = Connection.ToLong(dr["ByPriceGST"]);
                data.SalePriceGST = Connection.ToLong(dr["SalePriceGST"]);
                data.PlaceOfSupply = Connection.ToInteger(dr["PlaceOFSupply"]);

                data.vendorId = Connection.ToLong(dr["Vendorid"]);
            }

            return data;
        }
        //public CLayer.OfflineBooking StatusUpdateOfflineBookingEdit(long id)
        //{
        //    CLayer.OfflineBooking result = new CLayer.OfflineBooking();
        //    result.result = 1;
        //    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
        //    param.Add(Connection.GetParameter("pid", DataPlug.DataType._BigInt, id));


        //    DataTable dt = Connection.GetTable("StatusUpdateOfflineBookingEdit", param);

        //    if (dt.Rows.Count > 0)
        //    {
        //        int data = Connection.ToInteger(dt.Rows[0]["InvoiceId"]);

        //        if (data > 0)
        //        {
        //       //     string text = ("\"\""); 
        //            var invoiceid= Connection.ToInteger(dt.Rows[0]["InvoiceId"]);

        //            string sql = "Update invoices Set Status=" + ( (int) CLayer.ObjectStatus.InvoiceStatus.Saved).ToString() +" ,HtmlSection1='' ,HtmlSection2='', HtmlSection2='' Where InvoiceId=" + invoiceid.ToString();
        //            //Connection.ExecuteSqlQuery(sql);
        //            result.result = 2;

        //        }

        //    }
        //    return result;
        //}


        public int GetOfflinebookingIsGST(long OfflineBookingId)
        {
            string sql = "Select IsGST From offline_bookings Where Offline_BookingId=" + OfflineBookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }
        public long GetBookingDetailHSN(long BookingDetailsId)
        {
            string sql = "SELECT h.`CodeId` FROM  `bookingdetails` b INNER JOIN `hsncode` h ON h.`CodeId`=b.`HSNCode` WHERE BookingId =" + BookingDetailsId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }
        public CLayer.OfflineBooking GetOfflinebookingMinDatesIsGST(long OfflineBookingId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            DataTable dt = Connection.GetTable("OfflineBooking_GetOfflinebookingMinDatesIsGST", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn_date"]);
                result.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut_date"]);
            }
            return result;
        }
        //Done by rahul
        //public List<CLayer.OfflineBooking> GetCostCentreDetails()
        //{
        //    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
        //    DataTable dt = Connection.GetTable("GetCostCentreDetails", param);
        //    List<CLayer.OfflineBooking> coastcentredetail = new List<CLayer.OfflineBooking>();
        //    foreach(DataRow dr in dt.Rows)
        //    {
        //        coastcentredetail.Add(new CLayer.OfflineBooking()
        //        {
        //            CostCentre_ID=Connection.ToInteger(dr["CostCentre_ID"]),
        //            CostCentreCode = Connection.ToInteger(dr["CostCenter_Codee"]),
        //            CostCentrePercentage = Connection.ToInteger(dr["Percentage"])
        //        });
        //    }
        //    return coastcentredetail;
        //}
        //-----
        public List<CLayer.OfflineBooking> GetMultipleBookingDetailsGST(long offlinebookId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, offlinebookId));
            DataTable dt = Connection.GetTable("GetMultipleBookingDetailsGSTById", param);
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    BookingDetailsId = Connection.ToLong(dr["BookingId"]),
                    OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]),
                    GuestEmail = Connection.ToString(dr["GuestEmail"]),
                    GuestName = Connection.ToString(dr["GuestName"]),
                    CheckIn = Connection.ToDate(dr["CheckIn_date"]),
                    CheckOut = Connection.ToDate(dr["CheckOut_date"]),
                    NoOfNight = Connection.ToLong(dr["NoOfNight"]),
                    NoOfPaxAdult = Connection.ToLong(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToLong(dr["NoOfPaxChild"]),
                    NoOfRooms = Connection.ToLong(dr["NoOfRooms"]),
                    Accommodationid = Connection.ToLong(dr["Accommodationid"]),
                    Accommodationname = Connection.ToString(dr["AccommodatoinTypename"]),
                    Accommodationtypeid = Connection.ToLong(dr["AccommodatoinTypeId"]),
                    AccommodatoinTypename = Connection.ToString(dr["AccommodatoinTypeName"]),
                    StayCategory = Connection.ToLong(dr["StayCategoryId"]),
                    StayCategoryName = Connection.ToString(dr["StayCategoryName"]),
                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]),
                    TotalAmtForBuyPrice = Connection.ToDecimal(dr["TotalAmtForBuyPrice"]),
                    BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]),
                    StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]),
                    TotalAmtotherForBuyPrice = Connection.ToDecimal(dr["TotalAmtotherForBuyPrice"]),
                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    TotalAmtForSalePrice = Connection.ToDecimal(dr["TotalAmtForSalePrice"]),
                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    HotelConfirmationNo = Connection.ToString(dr["HotelConformationNo"]),
                    TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]),
                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),
                    OtherServiceNature= Connection.ToString(dr["NatureOfOtherService"])
                });
            }
            return bookings;
        }



        public List<CLayer.OfflineBooking> GetMultipleVendorDetailsGST(long offlinebookId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, offlinebookId));
            DataTable dt = Connection.GetTable("GetMultipleVendorDetailsGSTById", param);
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    VendorpaymentsId = Connection.ToLong(dr["VendorpaymentsId"]),
                    OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]),
                    vendorname = Connection.ToString(dr["Vendorsname"]),
                    vendoraddress = Connection.ToString(dr["Vendoraddress"]),
                    address1 = Connection.ToString(dr["Address1"]),
                    address2 = Connection.ToString(dr["Address2"]),
                    City = Connection.ToString(dr["City"]),
                    pin = Connection.ToString(dr["Pin"]),
                    ContactPerson = Connection.ToString(dr["Contactperson"]),
                    contactno = Connection.ToString(dr["Contactno"]),
                    emailaddress = Connection.ToString(dr["Emailaddress"]),

                    natureofservice = Connection.ToString(dr["Natureofservice"]),
                    ByPriceBeforeTax = Connection.ToLong(dr["ByPriceBeforeTax"]),
                    ByPriceTotal = Connection.ToLong(dr["ByPriceTotal"]),
                    SalePriceBeforeTax = Connection.ToLong(dr["SalePriceBeforeTax"]),
                    SalePriceTotal = Connection.ToLong(dr["SalePriceTotal"]),
                    ByPriceGST = Connection.ToLong(dr["ByPriceGST"]),

                    vendorId = Connection.ToLong(dr["Vendorid"]),
                });
            }
            return bookings;
        }

        public CLayer.OfflineBooking GetBookingDetailsGST(long BookingDetailsId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingDetailsId", DataPlug.DataType._BigInt, BookingDetailsId));
            DataTable dt = Connection.GetTable("OfflineBooking_GetBookingDetailsGST", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.BookingDetailsId = Connection.ToLong(dt.Rows[0]["BookingId"]);
                result.OfflineBookingId = Connection.ToLong(dt.Rows[0]["Offline_BookingId"]);
                result.GuestEmail = Connection.ToString(dt.Rows[0]["GuestEmail"]);
                result.GuestName = Connection.ToString(dt.Rows[0]["GuestName"]);
                result.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn_date"]);
                result.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut_date"]);
                result.NoOfNight = Connection.ToLong(dt.Rows[0]["NoOfNight"]);
                result.NoOfPaxAdult = Connection.ToLong(dt.Rows[0]["NoOfPaxAdult"]);
                result.NoOfPaxChild = Connection.ToLong(dt.Rows[0]["NoOfPaxChild"]);
                result.NoOfRooms = Connection.ToLong(dt.Rows[0]["NoOfRooms"]);
                result.Accommodationid = Connection.ToLong(dt.Rows[0]["Accommodationid"]);
                result.Accommodationname = Connection.ToString(dt.Rows[0]["AccommodatoinTypename"]);
                result.Accommodationtypeid = Connection.ToLong(dt.Rows[0]["AccommodatoinTypeId"]);
                result.AccommodatoinTypename = Connection.ToString(dt.Rows[0]["AccommodatoinTypeName"]);
                result.StayCategory = Connection.ToLong(dt.Rows[0]["StayCategoryId"]);
                result.StayCategoryName = Connection.ToString(dt.Rows[0]["StayCategoryName"]);
                result.AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dt.Rows[0]["AvgDailyRateBefreStaxForBuyPrice"]);
                result.StaxForBuyPrice = Connection.ToDecimal(dt.Rows[0]["StaxForBuyPrice"]);
                result.TotalAmtForBuyPrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtForBuyPrice"]);
                result.BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dt.Rows[0]["BuyPriceforotherservicesForBuyprice"]);
                result.StaxForotherBuyPrice = Connection.ToDecimal(dt.Rows[0]["StaxForotherBuyPrice"]);
                result.TotalAmtotherForBuyPrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtotherForBuyPrice"]);
                result.AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dt.Rows[0]["AvgDailyRateBefreStaxForSalePrice"]);
                result.StaxForSalePrice = Connection.ToDecimal(dt.Rows[0]["StaxForSalePrice"]);
                result.TotalAmtForSalePrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtForSalePrice"]);
                result.BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dt.Rows[0]["BuyPriceforotherservicesForSalePrice"]);
                result.StaxForotherForSalePrice = Connection.ToDecimal(dt.Rows[0]["StaxForotherForSalePrice"]);
                result.TotalAmtotherForSalePrice = Connection.ToDecimal(dt.Rows[0]["TotalAmtotherForSalePrice"]);
                result.HotelConfirmationNo = Connection.ToString(dt.Rows[0]["HotelConformationNo"]);
                result.TotalBuyPrice = Connection.ToDecimal(dt.Rows[0]["TotalBuyPrice"]);
                result.TotalSalePrice = Connection.ToDecimal(dt.Rows[0]["TotalSalePrice"]);
            }
            return result;
        }



        public CLayer.OfflineBooking GetSBEntityAddressDetailsByOffId(long OfflineBookingId)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            DataTable dt = Connection.GetTable("OfflineBooking_GetSBEntityAddressDetailsByOffId", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.SbEntityBillingName = Connection.ToString(dt.Rows[0]["SbEntityName"]);
                result.SbEntityBillingAddress = Connection.ToString(dt.Rows[0]["SbEntityAddress"]);
                result.SbEntityBillingCountry = Connection.ToString(dt.Rows[0]["SbEntityCountry"]);
                result.SbEntityBillingState = Connection.ToString(dt.Rows[0]["SbEntityState"]);
                result.SbEntityBillingPhone = Connection.ToString(dt.Rows[0]["SbEntityPhone"]);
                result.SbEntityBillingGSTNo = Connection.ToString(dt.Rows[0]["GSTNo"]);
            }
            return result;
        }



        public List<CLayer.OfflineBooking> GetOfflinebookingsAtCheckInOutNow()
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            DataTable dt = Connection.GetTable("SupplierInvoice_GetOfflinebookingsAtCheckInOutNow");
            foreach (DataRow dr in dt.Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]),
                    SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    PropertyEmail = Connection.ToString(dr["PropertyEmail"]),
                    ConfirmationNumber = Connection.ToString(dr["OrderNo"])
                });
            }
            return bookings;
        }
        public int CheckOfflineCustomerExist(string CustomerName1, string CustomerEmail1, int CustomerType, long CustomerId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, CustomerEmail1));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, CustomerName1));
            param.Add(Connection.GetParameter("pOfflineCustomerId", DataPlug.DataType._BigInt, CustomerId));
            param.Add(Connection.GetParameter("pCustomerType", DataPlug.DataType._BigInt, CustomerType));
            object obj = Connection.ExecuteQueryScalar("OfflineBooking_CheckOfflineCustomerExist", param);
            return Connection.ToInteger(obj); ;
        }
        public CLayer.OfflineBooking GetGSTAddressByState(long SubCustomerGstStateId, string CustomerGstRegNo, long CustomerId, int CustomerTableType)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSubCustomerGstStateId", DataPlug.DataType._Varchar, SubCustomerGstStateId));
            param.Add(Connection.GetParameter("pCustomerGstRegNo", DataPlug.DataType._Varchar, CustomerGstRegNo));
            param.Add(Connection.GetParameter("pCustomerTableType", DataPlug.DataType._Int, CustomerTableType));
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._Varchar, CustomerId));

            DataTable dt = Connection.GetTable("OfflineBooking_GetGSTAddressByState", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.SubCustomerAddress = Connection.ToString(dt.Rows[0]["SubCustomerAddress"]);
                result.SubCustomerpinCode = Connection.ToString(dt.Rows[0]["SubCustomerpinCode"]);
                result.SubCustomerCityname = Connection.ToString(dt.Rows[0]["SubCustomerCityname"]);
                result.SubCustomerCity = Connection.ToInteger(dt.Rows[0]["SubCustomerCity"]);
            }
            return result;
        }
        public bool CheckOfflineBookingDeleteorNot(long OfflineBookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            object val = Connection.ExecuteQueryScalar("CheckOfflineBookingDeleteorNot", param);
            return Connection.ToBoolean(val);
        }

        public bool CheckOfflineSubBookingDeleteorNot(long OfflineBookingId, long BookedID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            param.Add(Connection.GetParameter("pBookedID", DataPlug.DataType._BigInt, BookedID));
            object val = Connection.ExecuteQueryScalar("CheckOfflineSubBookingDeleteorNot", param);
            return Connection.ToBoolean(val);
        }
        public List<CLayer.OfflineBooking> SearchforBookingFor(string name, int custid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            param.Add(Connection.GetParameter("pCustid", DataPlug.DataType._BigInt, custid));
            DataTable dt = Connection.GetTable("GetBookingFor", param);

            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            CLayer.OfflineBooking temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.OfflineBooking();
                temp.CustomerId = Connection.ToLong(dr["BookingForCustomerId"]);
                temp.CustomerName = Connection.ToString(dr["Name"]);
                temp.CustomerEmail = Connection.ToString(dr["Email"]);
                temp.CustomerAddress = Connection.ToString(dr["Address"]);
                temp.CustomerMobile = Connection.ToString(dr["Mobile"]);
                temp.CustomerCountry = Connection.ToInteger(dr["CountryId"]);
                temp.CustomerState = Connection.ToInteger(dr["StateId"]);
                temp.CustomerCity = Connection.ToInteger(dr["cityId"]);
                temp.CustomerCityname = Connection.ToString(dr["City"]);
                temp.ZipCode = Connection.ToString(dr["CustomerPinCode"]);
                result.Add(temp);
            }

            return result;
        }


        public void SaveBookingForToOfflinebooking_bookingfor(CLayer.OfflineBooking data, long OfflineBookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            param.Add(Connection.GetParameter("pBookingForID", DataPlug.DataType._Varchar, data.BookingForID));
            param.Add(Connection.GetParameter("PCustomerMasterID", DataPlug.DataType._Varchar, data.MasterCustomerID));

            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, data.DirectCustomerName));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, data.DirectCustomerEmail));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, data.DirectCustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._Varchar, data.DirectCustomerCity));
            param.Add(Connection.GetParameter("pCustomerpinCode", DataPlug.DataType._Varchar, data.DirectPinCode));

            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._Varchar, data.DirectCustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._Varchar, data.DirectCustomerState));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, data.DirectCustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, data.DirectCustomerMobile));

            Connection.ExecuteQueryScalar("SaveBookingForToOfflinebooking_bookingfor", param);

        }


        public long SaveOfflineBookingCustomerBookingFor(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("PCustomerMasterID", DataPlug.DataType._Varchar, data.CustomerId));
            param.Add(Connection.GetParameter("pCustomerName", DataPlug.DataType._Varchar, data.CustomerName));
            param.Add(Connection.GetParameter("pCustomerEmail", DataPlug.DataType._Varchar, data.CustomerEmail));
            param.Add(Connection.GetParameter("pCustomerAddress", DataPlug.DataType._Varchar, data.CustomerAddress));
            param.Add(Connection.GetParameter("pCustomerCity", DataPlug.DataType._Varchar, data.CustomerCity));
            param.Add(Connection.GetParameter("pCustomerpinCode", DataPlug.DataType._Varchar, data.PinCode));

            param.Add(Connection.GetParameter("pCustomerCountry", DataPlug.DataType._Varchar, data.CustomerCountry));
            param.Add(Connection.GetParameter("pCustomerState", DataPlug.DataType._Varchar, data.CustomerState));
            param.Add(Connection.GetParameter("pCustomerCityname", DataPlug.DataType._Varchar, data.CustomerCityname));
            param.Add(Connection.GetParameter("pCustomerMobile", DataPlug.DataType._Varchar, data.CustomerMobile));

            object result = Connection.ExecuteQueryScalar("OfflineBooking_CustomerBookingFor", param);
            return Connection.ToLong(result);
        }
        public long GetBookingForID(long id)
        {
            string sql = "SELECT BookingForId FROM offlinebooking_bookingfor WHERE OfflineBookingId=" + id.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }

        public CLayer.OfflineBooking GetBookingFor(long id)
        {
            CLayer.OfflineBooking result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("PBookingForID", DataPlug.DataType._BigInt, id));


            DataTable dt = Connection.GetTable("GetBookingForEdit", param);

            if (dt.Rows.Count > 0)
            {
                result = new CLayer.OfflineBooking();
                result.DirectCustomerName = Connection.ToString(dt.Rows[0]["Name"]);
                result.DirectCustomerAddress = Connection.ToString(dt.Rows[0]["Address"]);
                result.DirectCustomerCityname = Connection.ToString(dt.Rows[0]["City"]);
                result.DirectCustomerCity = Connection.ToInteger(dt.Rows[0]["cityId"]);
                result.DirectCustomerState = Connection.ToInteger(dt.Rows[0]["StateId"]);
                result.DirectCustomerCountry = Connection.ToInteger(dt.Rows[0]["CountryId"]);
                result.DirectCustomerMobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.DirectCustomerEmail = Connection.ToString(dt.Rows[0]["Email"]);
                result.DirectPinCode = Connection.ToString(dt.Rows[0]["CustomerPinCode"]);
                result.MasterCustomerID = Connection.ToInteger(dt.Rows[0]["CustomerMasterID"]);
                result.BookingForID = Connection.ToInteger(dt.Rows[0]["BookingForCustomerId"]);
                result.CustomerCountryname = Connection.ToString(dt.Rows[0]["countryname"]);
            }
            return result;
        }





        public List<CLayer.OfflineBooking> getIGSTdetails(long OfflineBookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOffline_BookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            DataTable dt = Connection.GetTable("getIGSTdetails", param);

            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            CLayer.OfflineBooking temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.OfflineBooking();
                temp.CheckIn = Connection.ToDate(dr["CheckIn_date"]);
                temp.CheckOut = Connection.ToDate(dr["CheckOut_date"]);
                temp.HotelConfirmationNo = Connection.ToString(dr["HotelConformationNo"]);
                temp.GuestName = Connection.ToString(dr["Guestname"]);
                result.Add(temp);
            }




            return result;
        }


        public long GetSupplierStateID(long offlineBookingId)
        {
            long State = 0;
            string State1 = "";
            string PropertyId = " SELECT PropertyId FROM offline_bookings WHERE Offline_BookingId=" + offlineBookingId.ToString();

            int PropertyId1 = Connection.ToInteger(Connection.ExecuteSQLQueryScalar(PropertyId));
            if (PropertyId1 != 0)
            {
                State1 = "SELECT b.State FROM property AS a JOIN address AS b ON a.OwnerId=b.UserId WHERE a.PropertyId=" + PropertyId1.ToString();
                State = Connection.ToLong(Connection.ExecuteSQLQueryScalar(State1));
            }
            else
            {
                string CustomPropertyId = "SELECT CustomPropertyId FROM offline_bookings WHERE Offline_BookingId=" + offlineBookingId.ToString();

                int CustomPropertyId1 = Connection.ToInteger(Connection.ExecuteSQLQueryScalar(CustomPropertyId));
                if (CustomPropertyId1 != 0)
                {
                    State1 = "SELECT  SupplierState FROM offline_customproperty WHERE CustomPropertyId =" + CustomPropertyId1.ToString();
                    State = Connection.ToLong(Connection.ExecuteSQLQueryScalar(State1));
                }
            }

            return State;
        }

        public DataTable GetAllBillingEntityStateID()
        {

            string stateIds = " SELECT DISTINCT StateId FROM sbentity WHERE stateId IS NOT NULL";
            DataTable dt = Connection.GetSQLTable(stateIds);
            return dt;
        }
        public CLayer.OfflineBooking GetAllOfflineDetailsByOfflinebookid(long OfflineBookingId)
        {
            CLayer.OfflineBooking result = null;
            List<CLayer.OfflineBooking> lstresult = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookId", DataPlug.DataType._BigInt, OfflineBookingId));
            DataTable dt = Connection.GetTable("OfflineBooking_GetAll", param);

            result = new CLayer.OfflineBooking();
            result.SupplierPaymentScheduleList = new List<SupplierPaymentSchedule>();
            result.OfflineBookingId = OfflineBookingId;
            result.CheckIn = Connection.ToDate(dt.Rows[0]["CheckIn_date"]);
            result.CheckOut = Connection.ToDate(dt.Rows[0]["CheckOut_date"]);

            var OfflineBookingAllSupplierPayments = GetAllSupplierPayments(OfflineBookingId);
            if (OfflineBookingAllSupplierPayments != null)
            {
                foreach (var data in OfflineBookingAllSupplierPayments)
                {
                    SupplierPaymentSchedule spschedule = new SupplierPaymentSchedule();
                    spschedule.SupplierPaymentMode = data.SupplierPaymentMode;
                    spschedule.SupplierPaymentModeDate = data.SupplierPaymentModeDate;
                    spschedule.SupplierCreditDays = data.SupplierCreditDays;
                    result.SupplierPaymentScheduleList.Add(spschedule);
                }
            }
            return result;
        }



        public void UpdateSupplierPaymentmailsendData(long OfflineBookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            Connection.GetTable("UpdateSupplierPaymentmailsendData", param);
        }

        public long GetInvoiceIDByOfflineBookingID(long offlineBookingId)
        {
            long InvoiceID = 0;
            string sql = "Select InvoiceId from Invoices Where InvoiceType=1 and Status=1 and OfflineBookingId=" + offlineBookingId.ToString() + " Limit 1";
            InvoiceID = Connection.ToLong(Connection.ExecuteSQLQueryScalar(sql));
            return InvoiceID;
        }


        public DataTable GetCheckInDates(long bookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            DataTable dates = Connection.GetTable("sms_GetMinimumCheckinDate", param);
            return dates;
        }

        public List<CLayer.OfflineBooking> GetGuestDetails()
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            DataTable dtDetails = Connection.GetTable("sms_GetAllOffLineIds");
            List<CLayer.OfflineBooking> listGuestDetils = new List<CLayer.OfflineBooking>();

            if (dtDetails.Rows.Count>0)
            {
                foreach (DataRow row in dtDetails.Rows)
                {
                    CLayer.OfflineBooking guestDetail = new CLayer.OfflineBooking();
                    guestDetail.OfflineBookingId = Connection.ToLong(row["Offline_BookingId"]);
                    listGuestDetils.Add(guestDetail);
                }
            }
            return listGuestDetils;
        }

        public DataTable GetPhoneNumber(long OfflineBookingId, string timer)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            param.Add(Connection.GetParameter("pTimer", DataPlug.DataType._Int, timer));
            DataTable data = Connection.GetTable("sms_GuestPhoneNumberAfterApproval", param);
            return data;
        }
        public void SetCusPaymentLinkStatus(string userids,string status,string LoggedInUser,Guid PaymentGuid)
        {
            
            Connection.ExecuteSqlQuery("UPDATE `offline_bookings` SET `SendCustomerPaymentLinkStatus`= '" + status  + "', PaymentUserId = '" + LoggedInUser + "', PaymentLinkId ='" + PaymentGuid + "' WHERE Offline_BookingId IN(" + userids + ")");
            return;
        }
        public List<CLayer.OfflineBooking> GetAllForSelected_PaymentList(string searchString, int searchItem, int start, int limit, int Status)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

           
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
          
            DataSet ds = Connection.GetDataSet("SP_CustomerPaymentLink", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]),
                    CustomerId = Connection.ToLong(dr["CustomerId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    SupplierId = Connection.ToLong(dr["SupplierId"]),

                    CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
                    CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    GuestEmail = Connection.ToString(dr["GuestEmail"]),
                    GuestName = Connection.ToString(dr["GuestName"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),

                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    PropertyCaretakerName = Connection.ToString(dr["PropertyCaretakerName"]),
                    PropertyCountryname = Connection.ToString(dr["PropertyCountryname"]),
                    PropertyStatename = Connection.ToString(dr["PropertyStatename"]),
                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),
                    PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]),
                    PropertyEmail = Connection.ToString(dr["PropertyEmail"]),

                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    //SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    //SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    //SupplierAddress = Connection.ToString(dr["SupplierAddress"]),
                    //SupplierCountryname = Connection.ToString(dr["SupplierCountryname"]),
                    //SupplierStatename = Connection.ToString(dr["SupplierStatename"]),
                    //SupplierCityname = Connection.ToString(dr["SupplierCityname"]),

                    NoOfNight = Connection.ToLong(dr["NoOfNight"]),
                    NoOfPaxAdult = Connection.ToLong(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToLong(dr["NoOfPaxChild"]),
                    NoOfRooms = Connection.ToLong(dr["NoOfRooms"]),

                    Accommodationid = Connection.ToLong(dr["Accommodationid"]),
                    Accommodationname = Connection.ToString(dr["Accommodationname"]),

                    Accommodationtypeid = Connection.ToLong(dr["Accommodationtypeid"]),
                    AccommodatoinTypename = Connection.ToString(dr["AccommodatoinTypename"]),

                    StayCategory = Connection.ToLong(dr["StayCategory"]),
                    StayCategoryName = Connection.ToString(dr["StayCategoryName"]),

                    OtherService = Connection.ToString(dr["OtherService"]),
                    sendmailtocustomer = Connection.ToBoolean(dr["sendmailtocustomer"]),
                    sendmailtosupplier = Connection.ToBoolean(dr["sendmailtosupplier"]),


                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]),
                    TotalAmtForBuyPrice = Connection.ToDecimal(dr["TotalAmtForBuyPrice"]),
                    BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]),
                    StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]),
                    TotalAmtotherForBuyPrice = Connection.ToDecimal(dr["TotalAmtotherForBuyPrice"]),

                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    TotalAmtForSalePrice = Connection.ToDecimal(dr["TotalAmtForSalePrice"]),
                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),

                    TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]),
                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),

                    CreatedTime = Connection.ToDate(dr["CreatedDate"]),
                    SaveStatus = Connection.ToInteger(dr["SaveStatus"]),
                    CreatedBy = Connection.ToInteger(dr["CreatedBy"]),
                    CreatedName = Connection.ToString(dr["CraetedFirstname"]) + ' ' + Connection.ToString(dr["CraetedLastname"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    booking_creationdate = Connection.ToDate(dr["booking_creationdate"]),

                    SupplierPaymentName = Connection.ToString(dr["SupplierPaymentName"]),
                    CustomerPaymentName = Connection.ToString(dr["CustomerPaymentName"]),
                    SupplierPaymentMode = Connection.ToInteger(dr["SupplierPaymentMode"]),
                    CustomerPaymentMode = Connection.ToInteger(dr["CustomerPaymentMode"]),
                    CreditDays = Connection.ToDecimal(dr["CustomerCreditDays"]),
                    SupplierCreditDays = Connection.ToInteger(dr["SupplierCreditDays"]),

                });
            }
            return bookings;
        }
        //public List<CLayer.OfflineBooking> GetAllForPaymentList_Details(string searchString)
        //{
        //    List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
        //    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

        //    param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            
        //    DataSet ds = Connection.GetDataSet("SP_OfflineBooking_GetSearch_PaymentDetails", param);
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        bookings.Add(new CLayer.OfflineBooking()
        //        {
        //            OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]),

                    
        //            CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
        //            CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
        //            CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
        //            CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
        //            CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
        //            CustomerName = Connection.ToString(dr["CustomerName"]),
        //            CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
        //            //GuestEmail = Connection.ToString(dr["GuestEmail"]),
        //            GuestName = Connection.ToString(dr["GuestName"]),
        //            CheckIn = Connection.ToDate(dr["CheckIn"]),
        //            CheckOut = Connection.ToDate(dr["CheckOut"]),

        //            PropertyName = Connection.ToString(dr["PropertyName"]),
                    
        //            PropertyCityname = Connection.ToString(dr["PropertyCityname"]),

        //            ConfirmationNumber = Connection.ToString(dr["orderno"]),

        //            TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),

        //            PayableSalePrice = Connection.ToDecimal(dr["TotalSalePrice"])- Connection.ToDecimal(dr["AdvanceReceived"]),
        //            SumTotalSalePrice = Connection.ToDecimal(dr["SumofSalePrice"]),
        //            SumofAdvanceReceived = Connection.ToDecimal(dr["SumofAdvanceReceived"]),
        //            AdvanceReceived = Connection.ToDecimal(dr["AdvanceReceived"]),
                    
        //            PaymentLinkStatus = Connection.ToString(dr["SendCustomerPaymentLinkStatus"])

        //        });
        //    }
        //    return bookings;
        //}

        public List<CLayer.OfflineBooking> GetAllForPaymentList_Details(string id)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, id));

            DataSet ds = Connection.GetDataSet("SP_OfflineBooking_GetSearch_PaymentDetails", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]),


                    CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
                    CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    //GuestEmail = Connection.ToString(dr["GuestEmail"]),
                    GuestName = Connection.ToString(dr["GuestName"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),

                    PropertyName = Connection.ToString(dr["PropertyName"]),

                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),

                    ConfirmationNumber = Connection.ToString(dr["orderno"]),

                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),

                    PayableSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]) - Connection.ToDecimal(dr["AdvanceReceived"]),
                    SumTotalSalePrice = Connection.ToDecimal(dr["SumofSalePrice"]),
                    SumofAdvanceReceived = Connection.ToDecimal(dr["SumofAdvanceReceived"]),
                    AdvanceReceived = Connection.ToDecimal(dr["AdvanceReceived"]),
                    PaymentLinkID = Connection.ToString(dr["PaymentLinkID"]),
                    PaymentLinkStatus = Connection.ToString(dr["SendCustomerPaymentLinkStatus"])

                });
            }
            return bookings;
        }
        public void CustomerLinkAdvUpdate(long id, long AdvAmt)
        {

            Connection.ExecuteSqlQuery("UPDATE `offline_bookings` SET `AdvanceReceived`= '" + AdvAmt + "' WHERE Offline_BookingId IN(" + id + ")");
            return;
        }
        public void CustomerPaymentDetailsUpdate(string Emailid, string offline_bookingid)
        {
            Connection.ExecuteSqlQuery("UPDATE `offlinebooking_customer` SET `Email`= '" + Emailid + "' WHERE Offline_BookingId IN(" + offline_bookingid + ")");
            //Connection.ExecuteSqlQuery("UPDATE `offlinecustomermaster` SET `Email`= '" + Emailid + "' WHERE offline_customerid IN(" + offline_customerid + ")");
            //Connection.ExecuteSqlQuery("UPDATE `offline_bookings` SET `AdvanceReceived`= '" + Advance_amt + "' WHERE Offline_BookingId IN(" + offline_bookingid + ")");
            return;
        }
        public List<CLayer.OfflineBooking> GetAllForPaymentList_DetailsForMail(Guid PaymentLinkId)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            
            param.Add(Connection.GetParameter("pPaymentLinkId", DataPlug.DataType._Char, PaymentLinkId));
           
            DataSet ds = Connection.GetDataSet("SP_OfflineBooking_GetSearch_PaymentDetailsMail", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]),
                    
                    CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
                    CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),
                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),

                    PayableSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]) - Connection.ToDecimal(dr["AdvanceReceived"]),
                    SumTotalSalePrice = Connection.ToDecimal(dr["SumofSalePrice"]),
                    SumofAdvanceReceived = Connection.ToDecimal(dr["SumofAdvanceReceived"]),
                    AdvanceReceived = Connection.ToDecimal(dr["AdvanceReceived"]),
                    PaymentLinkID = Connection.ToString(dr["PaymentLinkID"]),
                    PaymentLinkStatus = Connection.ToString(dr["SendCustomerPaymentLinkStatus"]),
                    GuestName = Connection.ToString(dr["GuestName"]),
                    NoOfRooms = Connection.ToInteger(dr["NoOfRooms"])
                    //PaymentLinkID = PaymentLinkId
                });
            }

            return bookings;
        }
        public List<CLayer.OfflineBooking> GetofflinebookingCostCentre(long OfflineBookingId)
        {
            List<CLayer.OfflineBooking> costcentres = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            DataSet ds = Connection.GetDataSet("SP_Get_offlinebooking_costcentre", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                costcentres.Add(new CLayer.OfflineBooking()
                {
                    CostCentreID_Name=Connection.ToString(dr["CostCentreName"]),
                    CostCentre_ID=Connection.ToInteger(dr["CostCentre_ID"]),
                    CostCentreCode=Connection.ToInteger(dr["CostCenter_Codee"]),
                    CostCentrePercentage=Connection.ToInteger(dr["Percentage"]),
                    OfflineBookingId=Connection.ToLong(dr["Offline_BookingId"])
                });

            }
                return costcentres;
        }
        public List<CLayer.OfflineBooking> GetID_offlinebookingCostCentre(int ID)
        {
            List<CLayer.OfflineBooking> getcostcentre = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCostCentreId", DataPlug.DataType._BigInt, ID));
            DataSet ds = Connection.GetDataSet("SP_GET_ID_Offlinebooking_CostCentre", param);
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                getcostcentre.Add(new CLayer.OfflineBooking()
                {
                    CostCentre_ID = Connection.ToInteger(dr["CostCentre_ID"]),
                    CostCentreCode = Connection.ToInteger(dr["CostCenter_Codee"]),
                    CostCentrePercentage = Connection.ToInteger(dr["Percentage"]),
                    OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"])
                });
            }
            return getcostcentre;
        }
    }
}