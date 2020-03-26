using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SupplierInvoice : DataLink
    {

        public long Save(CLayer.SupplierInvoice data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierInvoiceID", DataPlug.DataType._BigInt, data.SupplierInvoiceID));
            param.Add(Connection.GetParameter("pPropertyID", DataPlug.DataType._Int, data.PropertyId));
            param.Add(Connection.GetParameter("pSupplierID", DataPlug.DataType._Int, data.SupplierId));
            param.Add(Connection.GetParameter("pInvoiceNumber", DataPlug.DataType._Text, data.InvoiceNumber));
            param.Add(Connection.GetParameter("pInvoiceDate", DataPlug.DataType._DateTime, data.InvoiceDate));
            param.Add(Connection.GetParameter("pServiceTaxRegNumber", DataPlug.DataType._Text, data.ServiceTaxRegNumber));
            param.Add(Connection.GetParameter("pPAN", DataPlug.DataType._Text, data.PAN_Number));
            param.Add(Connection.GetParameter("pBaseAmount", DataPlug.DataType._Decimal, data.BaseAmount));
            param.Add(Connection.GetParameter("pLuxuryTax", DataPlug.DataType._Decimal, data.LuxuryTax));
            param.Add(Connection.GetParameter("pServiceTax", DataPlug.DataType._Decimal, data.ServiceTax));
            param.Add(Connection.GetParameter("pTotalInvoiceValue", DataPlug.DataType._Decimal, data.TotalInvoiceValue));
            param.Add(Connection.GetParameter("pEntryDate", DataPlug.DataType._DateTime, data.EntryDate));
            param.Add(Connection.GetParameter("ptxtTotalAdjustmentResult", DataPlug.DataType._Decimal, data.txtTotalAdjustmentResult));
            param.Add(Connection.GetParameter("pPropertyEmailAddresss", DataPlug.DataType._Text, data.PropertyEmailAddresss));
            param.Add(Connection.GetParameter("pPropertyType", DataPlug.DataType._Text, data.PropertyType));
            param.Add(Connection.GetParameter("pTaxType", DataPlug.DataType._Text, data.TaxType));
            param.Add(Connection.GetParameter("pIsSupInvoicedone", DataPlug.DataType._Bool, data.IsSupInvoicedone));
            object result = Connection.ExecuteQueryScalar("SupplierInvoice_save", param);
            return Connection.ToInteger(result);
        }

        public void saveSupplierInvoiceBooking(List<CLayer.OfflineBooking> data)
        {
            foreach (var item in data)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                //param.Add(Connection.GetParameter("pSupplierInvoiceBooking_ID", DataPlug.DataType._BigInt, 0));
                param.Add(Connection.GetParameter("pBookingRefNumber", DataPlug.DataType._Text, item.ConfirmationNumber));
                param.Add(Connection.GetParameter("pSupplierInvoiceID", DataPlug.DataType._BigInt, item.SupplierInvoiceID));
                param.Add(Connection.GetParameter("pOpen", DataPlug.DataType._Bool, item.isOpen));

                param.Add(Connection.GetParameter("pSupInvoiceValueBRef", DataPlug.DataType._Decimal, item.SupInvoiceValueBRef));
                param.Add(Connection.GetParameter("pPaidValueBRef", DataPlug.DataType._Decimal, item.PaidValueBRef));
                object result = Connection.ExecuteQueryScalar("SupplierInvoiceBooking_save", param);
            }
        }

        public List<CLayer.SupplierInvoice> getSupplierInvoiceList(string searchText, int searchType, int Start, int Limit, int TaxType, int Status)
        {
            List<CLayer.SupplierInvoice> result = new List<CLayer.SupplierInvoice>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSearchText", DataPlug.DataType._Text, searchText));
            param.Add(Connection.GetParameter("pSearchType", DataPlug.DataType._Int, searchType));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pTaxType", DataPlug.DataType._Int, TaxType));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status)); 
            DataSet ds = Connection.GetDataSet("getGetSupplierInvoiceList_Pager", param);
            CLayer.SupplierInvoice res = null;
            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    res = new CLayer.SupplierInvoice();
                    res.SupplierInvoiceID = Connection.ToLong(dr["SupplierInvoiceID"]);
                    res.PropertyId = Connection.ToLong(dr["PropertyID"]);
                    res.Property_Name = Connection.ToString(dr["Title"]);
                    res.SupplierId = Connection.ToLong(dr["SupplierID"]);
                    res.SupplierName = Connection.ToString(dr["SupplierName"]);
                    res.City = Connection.ToString(dr["City"]);
                    res.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
                    res.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
                    res.ServiceTaxRegNumber = Connection.ToString(dr["ServiceTaxRegNumber"]);
                    res.PAN_Number = Connection.ToString(dr["PAN"]);
                    res.BaseAmount = Connection.ToDecimal(dr["BaseAmount"]);
                    res.LuxuryTax = Connection.ToDecimal(dr["LuxuryTax"]);
                    res.ServiceTax = Connection.ToDecimal(dr["ServiceTax"]);
                    res.TotalInvoiceValue = Connection.ToDecimal(dr["TotalInvoiceValue"]);
                    res.EntryDate = Connection.ToDate(dr["EntryDate"]);
                    res.TaxType = Connection.ToInteger(dr["TaxType"]);
                    res.TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]);
                    result.Add(res);
                }
            }
            return result;
        }

        public List<CLayer.OfflineBooking> SupplierInvoiceBookingList(long PropID, long ID, string BookingRefIDs, string PropertyEmailAddresss, string PropertyType, int searchTypeBooking, string searchTextBooking, int Start, int Limit, int TaxType, out int TotalRows_Booking)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pProp_ID", DataPlug.DataType._BigInt, PropID));
            param.Add(Connection.GetParameter("pSupplierInvoice_ID", DataPlug.DataType._BigInt, ID));
            param.Add(Connection.GetParameter("psearchTypeBooking", DataPlug.DataType._Int, searchTypeBooking));
            param.Add(Connection.GetParameter("psearchTextBooking", DataPlug.DataType._Text, searchTextBooking));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start == 1 ? 0 : Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pPropertyEmailAddresss", DataPlug.DataType._Text, PropertyEmailAddresss));
            param.Add(Connection.GetParameter("pPropertyType", DataPlug.DataType._Text, PropertyType));
            DataSet ds = new DataSet();

            if (TaxType == (int)CLayer.SupplierInvoice.TaxTypes.ServiceTax)
            {
                ds = Connection.GetDataSet("SupplierInvoicePropertyBooking_List", param);
            }
            else
            {
                ds = Connection.GetDataSet("SupplierInvoicePropertyBooking_ListForGST", param);
            }

            DataTable dt_Row = ds.Tables[0];
            if (dt_Row.Rows.Count > 0)
                TotalRows_Booking = Connection.ToInteger(dt_Row.Rows[0]["NumberOfRows"]);
            else
                TotalRows_Booking = 0;
            DataTable dt = ds.Tables[1];
            CLayer.OfflineBooking res = null;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    res = new CLayer.OfflineBooking();
                    res.PropertyId = Connection.ToLong(dr["Prop_ID"]);
                    res.OfflineBookingId = Connection.ToLong(dr["Primary_ID"]);
                    res.ConfirmationNumber = Connection.ToString(dr["OrderNo"]);
                    //res.Amount = Connection.ToLong(dr["Amount"]);
                    res.CheckIn_date = Connection.ToDate(dr["CheckIn_date"]);
                    res.CheckOut_date = Connection.ToDate(dr["CheckOut_date"]);
                    //res.BookingDate = Connection.ToDate(dr["BookingDate"]);
                    res.PropertyName = Connection.ToString(dr["PropertyName"]);
                    res.PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]);
                    res.PropertyEmail = Connection.ToString(dr["PropertyEmail"]);
                    res.SupplierName = Connection.ToString(dr["SupplierName"]);
                    res.SupplierMobile = Connection.ToString(dr["SupplierMobile"]);
                    res.SupplierEmail = Connection.ToString(dr["SupplierEmail"]);
                    res.TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]);
                    res.City = Connection.ToString(dr["City"]);
                    res.AmtTobePaid = Connection.ToDecimal(dr["AmtTobePaid"]);
                    foreach (string st in BookingRefIDs.Split(',').ToList<string>())
                    {
                        if (st == Connection.ToString(dr["OrderNo"]))
                        {
                            res.isCheckBook = true;
                        }
                    }
                    result.Add(res);
                }
            }
            return result;
        }

        public void deleteSupplierInvoiceSavedBookingListItem(string refNum = "", long supplierInvID = 0)
        {
            string sql = "DELETE FROM SupplierInvoice_Booking WHERE `BookingRefNumber`='" + refNum + "' AND `SupplierInvoiceID`=" + supplierInvID;
            Connection.ExecuteSqlQuery(sql);
        }


        public List<CLayer.OfflineBooking> SupplierInvoiceSavedBookingList(long PropID, long ID, string BookingRefIDs, string PropertyEmailAddresss, string PropertyType)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            DataTable dt = new DataTable();
            if (ID > 0)
            {
                param.Add(Connection.GetParameter("pProp_ID", DataPlug.DataType._BigInt, PropID));
                param.Add(Connection.GetParameter("pSupplierInvoice_ID", DataPlug.DataType._BigInt, ID));
                param.Add(Connection.GetParameter("pPropertyEmailAddresss", DataPlug.DataType._Text, PropertyEmailAddresss));
                param.Add(Connection.GetParameter("pPropertyType", DataPlug.DataType._Text, PropertyType));
                dt = Connection.GetTable("SupplierInvoicePropertySavedBooking_List", param);
            }
            else
            {
                param.Add(Connection.GetParameter("pProp_ID", DataPlug.DataType._BigInt, PropID));
                param.Add(Connection.GetParameter("pBookingIDs", DataPlug.DataType._Varchar, BookingRefIDs));
                param.Add(Connection.GetParameter("pPropertyEmailAddresss", DataPlug.DataType._Text, PropertyEmailAddresss));
                param.Add(Connection.GetParameter("pPropertyType", DataPlug.DataType._Text, PropertyType));
                dt = Connection.GetTable("SupplierInvoicePropertyBooking_List_ForSaving", param);
            }

            //param.Add(Connection.GetParameter("pProp_ID", DataPlug.DataType._BigInt, PropID));
            //param.Add(Connection.GetParameter("pSupplierInvoice_ID", DataPlug.DataType._BigInt, ID));
            //DataTable dt = Connection.GetTable("SupplierInvoicePropertySavedBooking_List", param);
            CLayer.OfflineBooking res = null;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    res = new CLayer.OfflineBooking();
                    res.PropertyId = Connection.ToLong(dr["Prop_ID"]);
                    res.OfflineBookingId = Connection.ToLong(dr["Primary_ID"]);
                    res.ConfirmationNumber = Connection.ToString(dr["OrderNo"]);
                    //res.Amount = Connection.ToLong(dr["Amount"]);
                    res.CheckIn_date = Connection.ToDate(dr["CheckIn_date"]);
                    res.CheckOut_date = Connection.ToDate(dr["CheckOut_date"]);
                    //res.BookingDate = Connection.ToDate(dr["BookingDate"]);
                    res.PropertyName = Connection.ToString(dr["PropertyName"]);
                    res.PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]);
                    res.PropertyEmail = Connection.ToString(dr["PropertyEmail"]);
                    res.SupplierName = Connection.ToString(dr["SupplierName"]);
                    res.SupplierMobile = Connection.ToString(dr["SupplierMobile"]);
                    res.SupplierEmail = Connection.ToString(dr["SupplierEmail"]);
                    res.TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]);
                    res.City = Connection.ToString(dr["City"]);
                    res.isOpen = Connection.ToBoolean(dr["open"]);
                    res.SupInvoiceValueBRef = Connection.ToDecimal(dr["SupInvoiceValueBRef"]);
                    res.PaidValueBRef = Connection.ToDecimal(dr["PaidValueBRef"]);
                    res.AmtTobePaid = Connection.ToDecimal(dr["AmtTobePaid"]);
                    result.Add(res);
                }
            }
            return result;
        }

        public List<CLayer.SupplierInvoice> getSupplierInvoiceList_Report(long Start, long Limit, DateTime? fromDT, DateTime? toDT)
        {
            fromDT = fromDT == DateTime.MinValue ? null : fromDT;
            toDT = toDT == DateTime.MinValue ? null : toDT;
            List<CLayer.SupplierInvoice> result = new List<CLayer.SupplierInvoice>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._BigInt, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._BigInt, Limit));
            DataSet ds = Connection.GetDataSet("getGetSupplierInvoiceList_Report", param);
            CLayer.SupplierInvoice res = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    res = new CLayer.SupplierInvoice();
                    res.SupplierInvoiceID = Connection.ToLong(dr["SupplierInvoiceID"]);
                    res.PropertyId = Connection.ToLong(dr["PropertyID"]);
                    res.Property_Name = Connection.ToString(dr["Title"]);
                    res.SupplierId = Connection.ToLong(dr["SupplierID"]);
                    res.SupplierName = Connection.ToString(dr["SupplierName"]);
                    res.City = Connection.ToString(dr["City"]);
                    res.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
                    res.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
                    res.ServiceTaxRegNumber = Connection.ToString(dr["ServiceTaxRegNumber"]);
                    res.PAN_Number = Connection.ToString(dr["PAN"]);
                    res.BaseAmount = Connection.ToDecimal(dr["BaseAmount"]);
                    res.LuxuryTax = Connection.ToDecimal(dr["LuxuryTax"]);
                    res.ServiceTax = Connection.ToDecimal(dr["ServiceTax"]);
                    res.TotalInvoiceValue = Connection.ToDecimal(dr["TotalInvoiceValue"]);
                    res.EntryDate = Connection.ToDate(dr["EntryDate"]);

                    res.SupplierInvoiceBooking_ID = Connection.ToLong(dr["SupplierInvoiceBooking_ID"]);
                    res.BookingRefNumber = Connection.ToString(dr["BookingRefNumber"]);
                    res.CheckIn_date = Connection.ToDate(dr["CheckIn_date"]);
                    res.CheckOut_date = Connection.ToDate(dr["CheckOut_date"]);
                    res.BookinCreatedDT = Connection.ToDate(dr["BookinCreatedDT"]);

                    res.TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]);

                    result.Add(res);
                }
            }
            return result;
        }

        public List<CLayer.OfflineBooking> getSupplierInvoicePendings_Report(long Start, long Limit, DateTime? fromDT, DateTime? toDT)
        {
            fromDT = fromDT == DateTime.MinValue ? null : fromDT;
            toDT = toDT == DateTime.MinValue ? null : toDT;
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._BigInt, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._BigInt, Limit));
            DataSet ds = Connection.GetDataSet("getGetSupplierInvoicePending_Report", param);
            CLayer.OfflineBooking res = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    res = new CLayer.OfflineBooking();
                    res.PropertyId = Connection.ToLong(dr["Prop_ID"]);
                    res.OfflineBookingId = Connection.ToLong(dr["Primary_ID"]);
                    //res.UserId = Connection.ToString(dr["UserId"]);
                    res.City = Connection.ToString(dr["City"]);
                    res.CustomerName = Connection.ToString(dr["CustomerFirstname"]) + " " + Connection.ToString(dr["CustomerLastName"]);
                    res.GuestName = Connection.ToString(dr["Guestname"]);
                    res.NoOfRooms = Connection.ToLong(dr["NoOfRooms"]);
                    res.ConfirmationNumber = Connection.ToString(dr["OrderNo"]);
                    res.SupplierName = Connection.ToString(dr["SupplierName"]);
                    res.CheckIn = Connection.ToDate(dr["CheckIn_Date"]);
                    res.CheckOut = Connection.ToDate(dr["CheckOut_Date"]);
                    res.Accommodationname = Connection.ToString(dr["AccommodatoinTypeName"]);
                    res.BookingDate = Connection.ToString(dr["BookingDate"]);
                    res.TotalBuyPrice = Connection.ToDecimal(dr["TotalBuyPrice"]);
                    res.TotalSalePrice = Connection.ToDecimal(dr["TotalSalePrice"]);
                    res.PropertyName = Connection.ToString(dr["PropertyName"]);
                    res.SupplierEmail = Connection.ToString(dr["SupplierEmail"]);
                    res.SupplierMobile = Connection.ToString(dr["SupplierMobile"]);
                    res.PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]);
                    res.PropertyEmail = Connection.ToString(dr["PropertyEmail"]);
                    res.TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]);
                    result.Add(res);
                }
            }
            return result;
        }

        public CLayer.SupplierInvoice getGetSupplierInvoicedetails(long ID)
        {
            CLayer.SupplierInvoice result = new CLayer.SupplierInvoice();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierInvID", DataPlug.DataType._BigInt, ID));
            DataTable dt = Connection.GetTable("getGetSupplierInvoicedetails", param);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.SupplierInvoice();
                result.SupplierInvoiceID = Connection.ToLong(dt.Rows[0]["SupplierInvoiceID"]);
                result.PropertyId = Connection.ToLong(dt.Rows[0]["PropertyID"]);
                result.Property_Name = Connection.ToString(dt.Rows[0]["Title"]);
                result.SupplierId = Connection.ToLong(dt.Rows[0]["SupplierID"]);
                result.InvoiceNumber = Connection.ToString(dt.Rows[0]["InvoiceNumber"]);
                result.InvoiceDate = Connection.ToDate(dt.Rows[0]["InvoiceDate"]);
                result.ServiceTaxRegNumber = Connection.ToString(dt.Rows[0]["ServiceTaxRegNumber"]);
                result.PAN_Number = Connection.ToString(dt.Rows[0]["PAN"]);
                result.BaseAmount = Connection.ToDecimal(dt.Rows[0]["BaseAmount"]);
                result.LuxuryTax = Connection.ToDecimal(dt.Rows[0]["LuxuryTax"]);
                result.ServiceTax = Connection.ToDecimal(dt.Rows[0]["ServiceTax"]);
                result.TotalInvoiceValue = Connection.ToDecimal(dt.Rows[0]["TotalInvoiceValue"]);
                result.EntryDate = Connection.ToDate(dt.Rows[0]["EntryDate"]);
                result.txtTotalAdjustmentResult = Connection.ToDecimal(dt.Rows[0]["TotalAdjustment"]);
                result.PropertyEmailAddresss = Connection.ToString(dt.Rows[0]["PropertyEmailAddress"]);
                result.PropertyType = Connection.ToString(dt.Rows[0]["PropertyType"]);
                result.TaxType = Connection.ToInteger(dt.Rows[0]["TaxType"]);
                result.IsSupInvoicedone = Connection.ToBoolean(dt.Rows[0]["IsSupInvoicedone"]);
                result.BookingRefNumber = Connection.ToString(dt.Rows[0]["BookingReferenceIDNo"]);
                result.CheckedBookingRefNumber = Connection.ToString(dt.Rows[0]["CheckedBookingReferenceIDNo"]);

            }
            return result;
        }

        public void DeleteSupplierInvoice(long ID)
        {
            string sql = "delete FROM SupplierInvoice WHERE SupplierInvoiceID=" + ID;
            Connection.ExecuteSqlQuery(sql);
        }
        public decimal GetAdjustmentAmount(string bookIdList, decimal totalInvoiceValue)
        {
            //CLayer.SupplierInvoice result = new CLayer.SupplierInvoice();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            bookIdList = bookIdList.Replace(",", "','");
            bookIdList = "'" + bookIdList + "'";

            param.Add(Connection.GetParameter("bookidlist", DataPlug.DataType._Varchar, bookIdList));


            //string newNum = string.Empty;
            //foreach (string num in bookIdList.Split(',').ToList<string>())
            //{
            //    string orgNum = num == null ? "" : num;
                
            //    if(orgNum!="")
            //    {
            //        if(newNum== string.Empty)
            //        {
            //            newNum = "'"+orgNum;
            //        }
            //        else
            //        {
            //            newNum = newNum+"'," + orgNum + "'";
            //        }
            //    }

            //}



            param.Add(Connection.GetParameter("totalinvoicevalue", DataPlug.DataType._Decimal, totalInvoiceValue));
            object result = Connection.ExecuteQueryScalar("GetAdjustementAmountForSupplierInvoice", param);
            return Connection.ToDecimal(result);
        }
    }
}
