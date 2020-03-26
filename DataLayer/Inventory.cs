using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Inventory : DataLink
    {
        public long  Delete(long InventoryId, long AccommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pInventoryId", DataPlug.DataType._BigInt, InventoryId));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, AccommodationId));
            long accid= Connection.ExecuteQuery("inventory_Delete", param);
            return(accid);
        }
        public long Update(CLayer.Inventory inventory)
        {          
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pInventoryId", DataPlug.DataType._BigInt, inventory.InventoryId));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, inventory.AccommodationId));
            param.Add(Connection.GetParameter("pQuantity", DataPlug.DataType._Int, inventory.Quantity));
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._Date, inventory.FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._Date, inventory.ToDate));
            object result = Connection.ExecuteQueryScalar("inventory_Save", param);
            return Connection.ToLong(result);
        }
        public CLayer.Inventory GetOnAccommodationId(long InventoryId)
        {
            CLayer.Inventory Inverntory = new CLayer.Inventory();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pInventoryId", DataPlug.DataType._BigInt, InventoryId));
            DataTable dt = Connection.GetTable("inventory_GetOnInventoryId", param);
            foreach (DataRow dr in dt.Rows)
            {
                if (dt.Rows.Count > 0)
                {
                    Inverntory = new CLayer.Inventory();
                    Inverntory.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                    Inverntory.InventoryId = Connection.ToLong(dr["InventoryId"]);
                    Inverntory.Quantity = Connection.ToInteger(dr["Quantity"]);
                    Inverntory.FromDate = Connection.ToDate(dr["FromDate"]);
                    Inverntory.ToDate = Connection.ToDate(dr["ToDate"]);
                }
            }
            return Inverntory;
        }
        public List<CLayer.Inventory> GetAllAccomodationId(int Start, int Limit, long? AccommodationId)
        {
            List<CLayer.Inventory> result = new List<CLayer.Inventory>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, AccommodationId));        
            DataSet ds = Connection.GetDataSet("inventory_GetAllAccommodationId", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Inventory()
                {
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    InventoryId = Connection.ToLong(dr["InventoryId"]),
                    Quantity = Connection.ToInteger(dr["Quantity"]),
                    FromDate = Connection.ToDate(dr["FromDate"]),
                    ToDate = Connection.ToDate(dr["ToDate"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }

        public List<CLayer.Inventory> AccommodationByProperty(long? accomodationId)
        {
            List<CLayer.Inventory> result = new List<CLayer.Inventory>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccomodationId", DataPlug.DataType._BigInt, accomodationId));
            DataTable dt = Connection.GetTable("inventory_accomodationByProperty", param);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Inventory()
                {
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    Category = Connection.ToString(dr["Category"]),
                });
            }
            return result;
        }

        public CLayer.Review Get(long PropertyId, long UserId, long BookingId)
        {
            CLayer.Review rev = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingId));
            DataTable dt = Connection.GetTable("review_Details", param);
            if (dt.Rows.Count > 0)
            {
                rev = new CLayer.Review();
                rev.PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]);
                rev.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                rev.BookingId = Connection.ToLong(dt.Rows[0]["BookingId"]);
                rev.Rating = Connection.ToInteger(dt.Rows[0]["Rating"]);
                rev.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                rev.ReviewDate = Connection.ToDate(dt.Rows[0]["ReviewDate"]);
                rev.FirstName = Connection.ToString(dt.Rows[0]["FirstName"]);
                rev.LastName = Connection.ToString(dt.Rows[0]["LastName"]);
                rev.Title = Connection.ToString(dt.Rows[0]["Title"]);
                rev.Location = Connection.ToString(dt.Rows[0]["Location"]);
                rev.Address = Connection.ToString(dt.Rows[0]["Address"]);
                rev.Description = Connection.ToString(dt.Rows[0]["Description"]);
                rev.UpdatedBy = Connection.ToLong(dt.Rows[0]["UpdatedBy"]);
                rev.UpdatedDate = Connection.ToDate(dt.Rows[0]["UpdatedDate"]);
                rev.Recommended = Connection.ToBoolean(dt.Rows[0]["IsRecommended"]);
            }
            return rev;
        }
        public long APIUpdate(CLayer.Inventory inventory)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, inventory.AccommodationId));
            param.Add(Connection.GetParameter("pQuantity", DataPlug.DataType._Int, inventory.Quantity));
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._Date, inventory.FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._Date, inventory.ToDate));
            object result = Connection.ExecuteQueryScalar("SP_inventory_Save", param);
            return Connection.ToLong(result);
        }
    }
}
