using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class RoomFeature : DataProvider.DataLink
    {
        public List<CLayer.RoomFeature> GetAll()
        {
            List<CLayer.RoomFeature> roomfeaturelist = new List<CLayer.RoomFeature>();

            DataTable dt = Connection.GetSQLTable("SELECT rf.*,1 AS CanDelete, " +
                     " rt.Title as RoomType FROM roomfeature rf inner join RoomType rt on rf.RoomTypeId=rt.RoomTypeId  ORDER BY Title ASC");
            foreach (DataRow dr in dt.Rows)
            {
                roomfeaturelist.Add(new CLayer.RoomFeature()
                {
                    RoomFeatureId = Connection.ToLong(dr["RoomFeatureId"]),
                    Title = Connection.ToString(dr["Title"]),
                    RoomTypeId = Connection.ToInteger(dr["RoomTypeId"]),
                    RoomType = Connection.ToString(dr["RoomType"]),
                    CanDelete = Connection.ToBoolean(dr["CanDelete"])
                });
            }

            return roomfeaturelist;
        }

        public int Save(CLayer.RoomFeature roomfeature)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomFeatureId", DataPlug.DataType._BigInt, roomfeature.RoomFeatureId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, roomfeature.Title));
            param.Add(Connection.GetParameter("pRoomTypeId", DataPlug.DataType._Varchar, roomfeature.RoomTypeId));
            int result = Connection.ExecuteQuery("roomfeature_Save", param);
            return result;
        }

        public void Delete(int RoomFeatureID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomFeatureId", DataPlug.DataType._BigInt, RoomFeatureID));
            Connection.ExecuteQuery("roomfeature_Delete", param);
            return;
        }

        public CLayer.RoomFeature Get(int RoomFeatureID)
        {
            CLayer.RoomFeature roomtype = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomFeatureId", DataPlug.DataType._BigInt, RoomFeatureID));
            DataTable dt = Connection.GetTable("roomfeature_Get", param);
            if (dt.Rows.Count > 0)
            {
                roomtype = new CLayer.RoomFeature();
                roomtype.RoomFeatureId = Connection.ToLong(dt.Rows[0]["RoomFeatureId"]);
                roomtype.RoomTypeId = Connection.ToInteger(dt.Rows[0]["RoomTypeId"]);
                roomtype.Title = Connection.ToString(dt.Rows[0]["Title"]);
            }
            return roomtype;
        }
    }
}
