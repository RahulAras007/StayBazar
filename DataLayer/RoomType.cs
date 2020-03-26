using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class RoomType : DataProvider.DataLink
    {
        public void SetPropertyType(int roomTypeId,string propertyTypesCSV )
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomTypeId", DataPlug.DataType._Int, roomTypeId));
            param.Add(Connection.GetParameter("pPropertyTypes", DataPlug.DataType._Varchar, propertyTypesCSV));
            Connection.ExecuteQuery("roomtype_SetPropertyType", param);
        }
        public List<CLayer.PropertyRoomType> GetAllPropertyType(int roomTypeId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomTypeId", DataPlug.DataType._Int, roomTypeId));
            DataTable dt = Connection.GetTable("roomtype_GetPropertyTypes", param);
            List<CLayer.PropertyRoomType> result = new List<CLayer.PropertyRoomType>();
            CLayer.PropertyRoomType tmp;
            foreach(DataRow dr in dt.Rows)
            {
                tmp = new CLayer.PropertyRoomType();
                tmp.IsUsed = Connection.ToInteger(dr["isused"]) > 0;
                tmp.Selected = Connection.ToInteger(dr["isselected"]) > 0;
                tmp.Title = Connection.ToString(dr["Title"]);
                tmp.PropertyTypeId = Connection.ToInteger(dr["PropertyTypeId"]);
                //tmp.RoomTypeId = Connection.ToInteger(dr["PropertyTypeId"]);
                result.Add(tmp);
            }
            return result;
        }
        public List<CLayer.RoomType> GetAll()
        {
            List<CLayer.RoomType> roomtypelist = new List<CLayer.RoomType>();         

            DataTable dt = Connection.GetSQLTable("SELECT *,(CASE WHEN (SELECT COUNT(*) FROM room rm WHERE rm.RoomTypeId=rt.RoomTypeId)>0 THEN 0 ELSE 1 END ) " +
                     " AS CanDelete FROM roomtype rt  ORDER BY Title ASC");
            foreach (DataRow dr in dt.Rows)
            {
                roomtypelist.Add(new CLayer.RoomType()
                {
                    RoomTypeId = Connection.ToInteger(dr["RoomTypeId"]),
                    Title = Connection.ToString(dr["Title"]),
                    CanDelete = Connection.ToBoolean(dr["CanDelete"])
                });
            }

            return roomtypelist;
        }

        public int Save(CLayer.RoomType roomtype)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomTypeId", DataPlug.DataType._BigInt, roomtype.RoomTypeId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, roomtype.Title));
            object result = Connection.ExecuteQueryScalar("roomtype_Save", param);
            return Connection.ToInteger(result);
        }

        public void Delete(int RoomTypeID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomTypeId", DataPlug.DataType._BigInt, RoomTypeID));
            Connection.ExecuteQuery("roomtype_Delete", param);
            return;
        }

        public CLayer.RoomType Get(int RoomTypeID)
        {
            CLayer.RoomType roomtype = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomTypeId", DataPlug.DataType._BigInt, RoomTypeID));
            DataTable dt = Connection.GetTable("roomtype_Get", param);
            if (dt.Rows.Count > 0)
            {
                roomtype = new CLayer.RoomType();
                roomtype.RoomTypeId = Connection.ToInteger(dt.Rows[0]["RoomTypeId"]);
                roomtype.Title = Connection.ToString(dt.Rows[0]["Title"]);       
            }
            return roomtype;
        }
    }
    
}
