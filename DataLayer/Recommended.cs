using System;
using System.Collections.Generic;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
   public class Recommended : DataLink
    {
        public List<CLayer.Recommended> GetAll()
        {
            List<CLayer.Recommended> result = new List<CLayer.Recommended>();
            DataTable dt = Connection.GetTable("recommended_GetAll");
            CLayer.Recommended temp;
            foreach(DataRow dr in dt.Rows)
            {
                temp = new  CLayer.Recommended();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.EndDate = Connection.ToDate(dr["EndDate"]);
                temp.StartDate = Connection.ToDate(dr["StartDate"]);
                temp.Order = Connection.ToInteger(dr["Order"]);
                temp.Status = Connection.ToInteger(dr["Status"]);
                temp.UpdatedByUser = Connection.ToString(dr["UpdatedBy"]);
                temp.UpdatedDate = Connection.ToDate(dr["UpdatedDate"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Supplier = Connection.ToString(dr["Name"]);
                temp.ManageFor = Connection.ToInteger(dr["ManageFor"]);
             //   temp.AccommodationType = Connection.ToString(dr["AccType"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Recommended> GetAllByManage(long ManageFor)
        {
            List<CLayer.Recommended> result = new List<CLayer.Recommended>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pManageFor", DataPlug.DataType._BigInt, ManageFor));
            DataTable dt = Connection.GetTable("recommendedbymanage_GetAll",param);
            CLayer.Recommended temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Recommended();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.EndDate = Connection.ToDate(dr["EndDate"]);
                temp.StartDate = Connection.ToDate(dr["StartDate"]);
                temp.Order = Connection.ToInteger(dr["Order"]);
                temp.Status = Connection.ToInteger(dr["Status"]);
                temp.UpdatedByUser = Connection.ToString(dr["UpdatedBy"]);
                temp.UpdatedDate = Connection.ToDate(dr["UpdatedDate"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Supplier = Connection.ToString(dr["Name"]);
                temp.ManageFor = Connection.ToInteger(dr["ManageFor"]);
                //   temp.AccommodationType = Connection.ToString(dr["AccType"]);
                result.Add(temp);
            }
            return result;
        }
       public void Save(CLayer.Recommended data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._Date, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._Date, data.EndDate));
            param.Add(Connection.GetParameter("pOrder", DataPlug.DataType._Int, data.Order));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt, data.UpdatedBy));
            param.Add(Connection.GetParameter("pManageFor", DataPlug.DataType._Int, data.ManageFor));
            Connection.ExecuteQueryScalar("recommended_Save", param);
        }
        public void SaveWithGDS(CLayer.Recommended data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._Date, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._Date, data.EndDate));
            param.Add(Connection.GetParameter("pOrder", DataPlug.DataType._Int, data.Order));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt, data.UpdatedBy));
            param.Add(Connection.GetParameter("pManageFor", DataPlug.DataType._Int, data.ManageFor));
            Connection.ExecuteQueryScalar("recommended_SaveWithGDS", param);
        }
        public long GDSRateSave(CLayer.GDSRates data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            Boolean isB2c = (data.RateFor == (int)CLayer.Role.Roles.Customer);

            param.Add(Connection.GetParameter("pRateId", DataPlug.DataType._BigInt, data.RateId));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, data.AccommodationId));
            param.Add(Connection.GetParameter("pRateFor", DataPlug.DataType._Int, data.RateFor));
            param.Add(Connection.GetParameter("pRate", DataPlug.DataType._Decimal, Math.Round(data.Rate)));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._Date, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._Date, data.EndDate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt, data.UpdatedBy));
            param.Add(Connection.GetParameter("pBookingCode", DataPlug.DataType._Varchar, data.BookingCode));

            object obj = Connection.ExecuteQueryScalar("gdsrates_Save", param);
            return Connection.ToLong(obj);
        }

        public CLayer.Recommended GetData(long propertyId, long ManageFor)
       {
           CLayer.Recommended result = null;
           List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
           param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
           param.Add(Connection.GetParameter("pManageFor", DataPlug.DataType._BigInt, ManageFor));
           DataTable dt = Connection.GetTable("recommended_Get",param);
           if(dt.Rows.Count>0)
           {
               result = new CLayer.Recommended()
               {
                   StartDate = Connection.ToDate(dt.Rows[0]["StartDate"]),
                   EndDate = Connection.ToDate(dt.Rows[0]["EndDate"]),
                   Status = Connection.ToInteger(dt.Rows[0]["Status"]),
                   Order = Connection.ToInteger(dt.Rows[0]["Order"]),
                   UpdatedBy = Connection.ToLong(dt.Rows[0]["UpdatedBy"]),
                   UpdatedDate = Connection.ToDate(dt.Rows[0]["UpdatedDate"]),
                   Title = Connection.ToString(dt.Rows[0]["Title"]),
                   ManageFor = Connection.ToInteger(dt.Rows[0]["ManageFor"])
               };
           }

           return result;
       }

       public void Remove(long id, long ManageFor)
       {
           string sql = "Delete from recommended Where PropertyId=" + id.ToString() + " and  ManageFor=" + ManageFor.ToString();
           Connection.ExecuteSqlQuery(sql);
       }
        public List<CLayer.Recommended> GetAllByManageWithGDS(long ManageFor)
        {
            List<CLayer.Recommended> result = new List<CLayer.Recommended>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pManageFor", DataPlug.DataType._BigInt, ManageFor));
            DataTable dt = Connection.GetTable("recommendedbymanage_GetAllWithGDS", param);
            CLayer.Recommended temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Recommended();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.EndDate = Connection.ToDate(dr["EndDate"]);
                temp.StartDate = Connection.ToDate(dr["StartDate"]);
                temp.Order = Connection.ToInteger(dr["Order"]);
                temp.Status = Connection.ToInteger(dr["Status"]);
                temp.UpdatedByUser = Connection.ToString(dr["UpdatedBy"]);
                temp.UpdatedDate = Connection.ToDate(dr["UpdatedDate"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Supplier = Connection.ToString(dr["Name"]);
                temp.ManageFor = Connection.ToInteger(dr["ManageFor"]);
                temp.Amount = Math.Round(Connection.ToDecimal(dr["amount"]), 0);
                //   temp.AccommodationType = Connection.ToString(dr["AccType"]);
                result.Add(temp);
            }
            return result;
        }
    }
}
