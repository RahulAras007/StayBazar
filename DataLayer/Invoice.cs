using System;
using System.Collections.Generic;
using DataLayer.DataProvider;
using System.Data;


namespace DataLayer
{
    public class Invoice : DataLink
    {
        public CLayer.InvoiceNumberData GetOldInvoiceNumber(long stateId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._BigInt, stateId));
            DataTable dt = Connection.GetTable("State_OldInvoiceNumber", param);
            if (dt.Rows.Count == 0) return null;
            CLayer.InvoiceNumberData data = new CLayer.InvoiceNumberData();
            data.InvoiceNumber = Connection.ToString(dt.Rows[0]["IncNumber"]);
            data.InvoiceDate =  Connection.ToDate(dt.Rows[0]["InvDate"]);
            return data;
        }
        public CLayer.InvoiceNumberData GetOldGDSInvoiceNumber(long stateId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._BigInt, stateId));
            DataTable dt = Connection.GetTable("State_OldGDSInvoiceNumber", param);
            if (dt.Rows.Count == 0) return null;
            CLayer.InvoiceNumberData data = new CLayer.InvoiceNumberData();
            data.InvoiceNumber = Connection.ToString(dt.Rows[0]["IncNumber"]);
            data.InvoiceDate = Connection.ToDate(dt.Rows[0]["InvDate"]);
            return data;
        }
        public List<CLayer.OfflineBooking> GetAllBooking(int status, string searchString, int searchItem, int start, int limit,out int totalRows)
        {
            List<CLayer.OfflineBooking> bookings = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataPlug.Parameter paramo = Connection.GetParameter("pTotalRows", DataPlug.DataType._Int,0,0,ParameterDirection.Output);
            param.Add(paramo);
            DataTable ds = Connection.GetTable("Invoice_Search", param);
            totalRows = Connection.ToInteger(paramo.Value); // Connection.ToInteger(ds.Tables[0].Rows[0][0]);
            foreach (DataRow dr in ds.Rows)
            {
                bookings.Add(new CLayer.OfflineBooking()
                {
                    OfflineBookingId = Connection.ToLong(dr["Offline_BookingId"]),
                    InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]),
                    //CustomerId = Connection.ToLong(dr["CustomerId"]),
                    //PropertyId = Connection.ToLong(dr["PropertyId"]),
                    //SupplierId = Connection.ToLong(dr["SupplierId"]),

                    //CustomerAddress = Connection.ToString(dr["CustomerAddress"]),
                    //CustomerCityname = Connection.ToString(dr["CustomerCityname"]),
                    //CustomerCountryname = Connection.ToString(dr["CustomerCountryname"]),
                    //CustomerStatename = Connection.ToString(dr["CustomerStatename"]),
                    //CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    //CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                    //GuestEmail = Connection.ToString(dr["GuestEmail"]),
                    //GuestName = Connection.ToString(dr["GuestName"]),
                    CheckIn = Connection.ToDate(dr["CheckIn_Date"]),
                    CheckOut = Connection.ToDate(dr["CheckOut_Date"]),

                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    //PropertyAddress = Connection.ToString(dr["PropertyAddress"]),
                    //PropertyCaretakerName = Connection.ToString(dr["PropertyCaretakerName"]),
                    //PropertyCountryname = Connection.ToString(dr["PropertyCountryname"]),
                    //PropertyStatename = Connection.ToString(dr["PropertyStatename"]),
                    PropertyCityname = Connection.ToString(dr["PropertyCityname"]),
                    //PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]),
                    //PropertyEmail = Connection.ToString(dr["PropertyEmail"]),

                    //SupplierName = Connection.ToString(dr["SupplierName"]),
                    ////SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    ////SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    ////SupplierAddress = Connection.ToString(dr["SupplierAddress"]),
                    ////SupplierCountryname = Connection.ToString(dr["SupplierCountryname"]),
                    ////SupplierStatename = Connection.ToString(dr["SupplierStatename"]),
                    ////SupplierCityname = Connection.ToString(dr["SupplierCityname"]),

                    //NoOfNight = Connection.ToLong(dr["NoOfNight"]),
                    //NoOfPaxAdult = Connection.ToLong(dr["NoOfPaxAdult"]),
                    //NoOfPaxChild = Connection.ToLong(dr["NoOfPaxChild"]),
                    //NoOfRooms = Connection.ToLong(dr["NoOfRooms"]),

                    //Accommodationid = Connection.ToLong(dr["Accommodationid"]),
                    //Accommodationname = Connection.ToString(dr["Accommodationname"]),

                    //Accommodationtypeid = Connection.ToLong(dr["Accommodationtypeid"]),
                    //AccommodatoinTypename = Connection.ToString(dr["AccommodatoinTypename"]),

                    //StayCategory = Connection.ToLong(dr["StayCategory"]),
                    //StayCategoryName = Connection.ToString(dr["StayCategoryName"]),

                    //OtherService = Connection.ToString(dr["OtherService"]),
                    //sendmailtocustomer = Connection.ToBoolean(dr["sendmailtocustomer"]),
                    //sendmailtosupplier = Connection.ToBoolean(dr["sendmailtosupplier"]),


                    //AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    //StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]),
                    //TotalAmtForBuyPrice = Connection.ToDecimal(dr["TotalAmtForBuyPrice"]),
                    //BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]),
                    //StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]),
                    //TotalAmtotherForBuyPrice = Connection.ToDecimal(dr["TotalAmtotherForBuyPrice"]),

                    //AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    //StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    //TotalAmtForSalePrice = Connection.ToDecimal(dr["TotalAmtForSalePrice"]),
                    //BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    //StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    //TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),

                    //TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]),
                    TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]),

                    CreatedTime = Connection.ToDate(dr["BookingDate"]),
                    SaveStatus = Connection.ToInteger(dr["SaveStatus"]),
                    InvoiceStatus = Connection.ToInteger(dr["InvoiceStatus"])
                  
                    //CreatedBy = Connection.ToInteger(dr["CreatedBy"]),
                    //CreatedName = Connection.ToString(dr["CraetedFirstname"]) + ' ' + Connection.ToString(dr["CraetedLastname"]),
                    //TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return bookings;
        }

        public long Save(CLayer.Invoice data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pInvoiceId", DataPlug.DataType._BigInt, data.InvoiceId));
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, data.OfflineBookingId));
            param.Add(Connection.GetParameter("pToAddress", DataPlug.DataType._Varchar, data.ToAddress));
            param.Add(Connection.GetParameter("pInvoiceDate", DataPlug.DataType._Date, data.InvoiceDate));
            param.Add(Connection.GetParameter("pDueDate", DataPlug.DataType._Date, data.DueDate));
            param.Add(Connection.GetParameter("pMailDate", DataPlug.DataType._Date, data.MailedDate));
            param.Add(Connection.GetParameter("pReimbursements", DataPlug.DataType._Decimal, data.Reimbursements));
            param.Add(Connection.GetParameter("pDiscounts", DataPlug.DataType._Decimal, data.Discounts));
            param.Add(Connection.GetParameter("pOthers", DataPlug.DataType._Decimal, data.Others));
            param.Add(Connection.GetParameter("pInvoiceType", DataPlug.DataType._Int, data.InvoiceType));
            param.Add(Connection.GetParameter("pInvoiceNumber", DataPlug.DataType._Varchar, data.InvoiceNumber));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pHtmlSection1", DataPlug.DataType._Text, data.HtmlSection1));
            param.Add(Connection.GetParameter("pHtmlSection2", DataPlug.DataType._Text, data.HtmlSection2));
            param.Add(Connection.GetParameter("pHtmlSection3", DataPlug.DataType._Text, data.HtmlSection3));
            param.Add(Connection.GetParameter("pHtmlSection4", DataPlug.DataType._Text, data.HtmlSection4));

            object obj = Connection.ExecuteQueryScalar("Invoice_Save", param);

            return Connection.ToLong(obj);
        }
        public long GDSSave(CLayer.Invoice data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pInvoiceId", DataPlug.DataType._BigInt, data.InvoiceId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pToAddress", DataPlug.DataType._Varchar, data.ToAddress));
            param.Add(Connection.GetParameter("pInvoiceDate", DataPlug.DataType._Date, data.InvoiceDate));
            param.Add(Connection.GetParameter("pDueDate", DataPlug.DataType._Date, data.DueDate));
            param.Add(Connection.GetParameter("pMailDate", DataPlug.DataType._Date, data.MailedDate));
            param.Add(Connection.GetParameter("pReimbursements", DataPlug.DataType._Decimal, data.Reimbursements));
            param.Add(Connection.GetParameter("pDiscounts", DataPlug.DataType._Decimal, data.Discounts));
            param.Add(Connection.GetParameter("pOthers", DataPlug.DataType._Decimal, data.Others));
            param.Add(Connection.GetParameter("pInvoiceType", DataPlug.DataType._Int, data.InvoiceType));
            param.Add(Connection.GetParameter("pInvoiceNumber", DataPlug.DataType._Varchar, data.InvoiceNumber));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pHtmlSection1", DataPlug.DataType._Text, data.HtmlSection1));
            param.Add(Connection.GetParameter("pHtmlSection2", DataPlug.DataType._Text, data.HtmlSection2));
            param.Add(Connection.GetParameter("pHtmlSection3", DataPlug.DataType._Text, data.HtmlSection3));
            param.Add(Connection.GetParameter("pHtmlSection4", DataPlug.DataType._Text, data.HtmlSection4));

            object obj = Connection.ExecuteQueryScalar("GDSInvoice_Save", param);

            return Connection.ToLong(obj);
        }
        public int GetStatus(long invoiceId)
        {
            string sql = "Select ifnull(Status,1) as status From Invoices Where invoiceId=" + invoiceId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }
        public CLayer.Invoice GetInvoiceByOfflineBooking(long offlineBookingId)
        {
            string sql = "Select * from Invoices Where InvoiceType=1 and OfflineBookingId=" + offlineBookingId.ToString() + " Limit 1";
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.Invoice data = null;
            if(dt.Rows.Count == 0)
            { return data;  }

            data = new CLayer.Invoice();
            DataRow dr = dt.Rows[0];

            data.InvoiceId = Connection.ToLong(dr["InvoiceId"]);
            data.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
            data.Discounts = Connection.ToDouble(dr["Discounts"]);
            data.DueDate = Connection.ToDate(dr["DueDate"]);
            data.HtmlSection1 = Connection.ToString(dr["HtmlSection1"]);
            data.HtmlSection2 = Connection.ToString(dr["HtmlSection2"]);
            data.HtmlSection3 = Connection.ToString(dr["HtmlSection3"]);
            data.HtmlSection4 = Connection.ToString(dr["HtmlSection4"]);
            data.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
            data.InvoiceType = Connection.ToInteger(dr["InvoiceType"]);
            data.MailedDate = Connection.ToDate(dr["MailedDate"]);
            data.OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]);
            data.Others = Connection.ToDouble(dr["Others"]);
            data.Reimbursements = Connection.ToDouble(dr["Reimbursements"]);
            data.Status = Connection.ToInteger(dr["Status"]);
            data.ToAddress = Connection.ToString(dr["ToAddress"]);
            
            return data;
        }
        public long UpdateGDSInvoiceByBookingID(CLayer.Invoice data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pInvoiceId", DataPlug.DataType._BigInt, data.InvoiceId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pInvoiceDate", DataPlug.DataType._Date, data.InvoiceDate));
            param.Add(Connection.GetParameter("pInvoiceNumber", DataPlug.DataType._Varchar, data.InvoiceNumber));
           

            object obj = Connection.ExecuteQueryScalar("GDSInvoice_Updateinvoicenumber", param);

            return Connection.ToLong(obj);


        }
        public CLayer.Invoice GetGDSInvoiceByBookingID(long BookingId)
        {
            string sql = "Select * from GDSInvoices Where InvoiceType=1 and BookingId=" + BookingId.ToString() + " Limit 1";
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.Invoice data = null;
            if (dt.Rows.Count == 0)
            { return data; }

            data = new CLayer.Invoice();
            DataRow dr = dt.Rows[0];

            data.InvoiceId = Connection.ToLong(dr["InvoiceId"]);
            data.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
            data.Discounts = Connection.ToDouble(dr["Discounts"]);
            data.DueDate = Connection.ToDate(dr["DueDate"]);
            data.HtmlSection1 = Connection.ToString(dr["HtmlSection1"]);
            data.HtmlSection2 = Connection.ToString(dr["HtmlSection2"]);
            data.HtmlSection3 = Connection.ToString(dr["HtmlSection3"]);
            data.HtmlSection4 = Connection.ToString(dr["HtmlSection4"]);
            data.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
            data.InvoiceType = Connection.ToInteger(dr["InvoiceType"]);
            data.MailedDate = Connection.ToDate(dr["MailedDate"]);
            data.BookingId = Connection.ToLong(dr["BookingId"]);
            data.Others = Connection.ToDouble(dr["Others"]);
            data.Reimbursements = Connection.ToDouble(dr["Reimbursements"]);
            data.Status = Connection.ToInteger(dr["Status"]);
            data.ToAddress = Connection.ToString(dr["ToAddress"]);

            return data;
        }
        public void SetMailedDate(long invoiceId,DateTime mailedDate)
        {
            string sql = "Update invoices Set MailedDate='" + mailedDate.ToString("yyyy-MM-dd") + "' where  InvoiceId=" + invoiceId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public CLayer.Invoice GetProformaByOfflineBooking(long offlineBookingId)
        {
            string sql = "Select * from Invoices Where InvoiceType=2 and OfflineBookingId=" + offlineBookingId.ToString() + " Limit 1";
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.Invoice data = null;
            if (dt.Rows.Count == 0)
            { return data; }

            data = new CLayer.Invoice();
            DataRow dr = dt.Rows[0];

            data.InvoiceId = Connection.ToLong(dr["InvoiceId"]);
            data.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
            data.Discounts = Connection.ToDouble(dr["Discounts"]);
            data.DueDate = Connection.ToDate(dr["DueDate"]);
            data.HtmlSection1 = Connection.ToString(dr["HtmlSection1"]);
            data.HtmlSection2 = Connection.ToString(dr["HtmlSection2"]);
            data.HtmlSection3 = Connection.ToString(dr["HtmlSection3"]);
            data.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
            data.InvoiceType = Connection.ToInteger(dr["InvoiceType"]);
            data.MailedDate = Connection.ToDate(dr["MailedDate"]);
            data.OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]);
            data.Others = Connection.ToDouble(dr["Others"]);
            data.Reimbursements = Connection.ToDouble(dr["Reimbursements"]);
            data.Status = Connection.ToInteger(dr["Status"]);
            data.ToAddress = Connection.ToString(dr["ToAddress"]);

            return data;
        }
        public CLayer.Invoice GetInvoice(long invoiceId)
        {
            string sql = "Select * from Invoices Where InvoiceId=" + invoiceId.ToString() ;
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.Invoice data = null;
            if (dt.Rows.Count == 0)
            { return data; }

            data = new CLayer.Invoice();
            DataRow dr = dt.Rows[0];

            data.InvoiceId = Connection.ToLong(dr["InvoiceId"]);
            data.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
            data.Discounts = Connection.ToDouble(dr["Discounts"]);
            data.DueDate = Connection.ToDate(dr["DueDate"]);
            data.HtmlSection1 = Connection.ToString(dr["HtmlSection1"]);
            data.HtmlSection2 = Connection.ToString(dr["HtmlSection2"]);
            data.HtmlSection3 = Connection.ToString(dr["HtmlSection3"]);
            data.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
            data.InvoiceType = Connection.ToInteger(dr["InvoiceType"]);
            data.MailedDate = Connection.ToDate(dr["MailedDate"]);
            data.OfflineBookingId = Connection.ToLong(dr["OfflineBookingId"]);
            data.Others = Connection.ToDouble(dr["Others"]);
            data.Reimbursements = Connection.ToDouble(dr["Reimbursements"]);
            data.Status = Connection.ToInteger(dr["Status"]);
            data.ToAddress = Connection.ToString(dr["ToAddress"]);

            return data;
        }

        

    }
}
