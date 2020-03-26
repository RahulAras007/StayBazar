using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SupplierPaymetList : DataLink
    {

        public List<CLayer.SupplierPaymetList> getPaymentList(string pSearchString, int start, int limit, int pSearchType = 0)
        {
            List<CLayer.SupplierPaymetList> result = new List<CLayer.SupplierPaymetList>();
            CLayer.SupplierPaymetList res = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Text, pSearchString));
            param.Add(Connection.GetParameter("pSearchType", DataPlug.DataType._Int, pSearchType));
            param.Add(Connection.GetParameter("pstart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("SupplierPaymentList_Get", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                res = new CLayer.SupplierPaymetList()
                {
                    SupplierPaymentId = Connection.ToLong(dr["SupplierPaymentId"]),
                    SupplierId = Connection.ToLong(dr["SupplierId"]),
                    OfflineSupplierEmail = Connection.ToString(dr["OfflineSupplierEmail"]),
                    SupplierPayment = Connection.ToDecimal(dr["SupplierPayment"]),
                    dtPayment = Connection.ToDate(dr["dtPayment"]),
                    grossAmount = Connection.ToDecimal(dr["grossAmount"]),
                    tdsAmount = Connection.ToDecimal(dr["tdsAmount"]),
                    AmountPaid = Connection.ToDouble(dr["Amount"]),
                    netAmtPaid = Connection.ToDecimal(dr["netAmtPaid"]),
                    modeofPayment = Connection.ToString(dr["modeofPayment"]),
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),

                    bank = Connection.ToString(dr["bank"]),
                    SupplierName = Connection.ToString(dr["Supplier_Name"]),
                    Prop_Name = Connection.ToString(dr["Prop_Name"]),
                    Prop_ID = Connection.ToLong(dr["Prop_ID"]),
                    OrderNo = Connection.ToString(dr["OrderNo"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])


                };
                result.Add(res);
            }
            return result;
        }


        public List<CLayer.SupplierPaymetList> getDetails(long SupplierPaymentId)
        {
            List<CLayer.SupplierPaymetList> result = new List<CLayer.SupplierPaymetList>();
            CLayer.SupplierPaymetList res = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierPaymentID", DataPlug.DataType._BigInt, SupplierPaymentId));

            DataSet ds = Connection.GetDataSet("supplierpaymentvoucher_GetAllBySupplierPaymentID", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                result.Add(new CLayer.SupplierPaymetList()
                {
                    SupplierPaymentId = Connection.ToLong(dr["SupplierPaymentId"]),
                    SupplierId = Connection.ToLong(dr["SupplierId"]),
                    OfflineSupplierEmail = Connection.ToString(dr["OfflineSupplierEmail"]),
                    SupplierPayment = Connection.ToDecimal(dr["SupplierPayment"]),
                    dtPayment = Connection.ToDate(dr["dtPayment"]),
                    grossAmount = Connection.ToDecimal(dr["grossAmount"]),
                    tdsAmount = Connection.ToDecimal(dr["tdsAmount"]),
                    netAmtPaid = Connection.ToDecimal(dr["netAmtPaid"]),
                    modeofPayment = Connection.ToString(dr["modeofPayment"]),
                    bank = Connection.ToString(dr["bank"]),
                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    Prop_Name = Connection.ToString(dr["propertyname"]),
                    // Prop_ID = Connection.ToLong(dr["propertyid"]),
                    OrderNo = Connection.ToString(dr["OrderNo"])
                });
            }
            return result;
        }
        public void SetDeleteStatus(long supplierPaymentId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierPaymentID", DataPlug.DataType._BigInt, supplierPaymentId));
            Connection.ExecuteQuery("supplierpayment_delete", param);
        }

    }
}
