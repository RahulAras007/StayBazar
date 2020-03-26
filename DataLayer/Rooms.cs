using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Rooms : DataLink
    {
        public List<CLayer.Rooms> GetAll(long propertyid)
        {
            List<CLayer.Rooms> result = new List<CLayer.Rooms>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            DataTable dt = Connection.GetTable("room_GetOnProperty", param);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Rooms()
                {
                    RoomId = Connection.ToLong(dr["RoomId"]),
                    RoomTypeId = Connection.ToInteger(dr["RoomTypeId"]),
                    RoomType = Connection.ToString(dr["Title"]),
                    RoomCount = Connection.ToInteger(dr["RoomCount"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Description = Connection.ToString(dr["Description"])
                });
            }
            RoomType rt = new RoomType();
            List<CLayer.RoomType> types = rt.GetAll();
            if (result.Count() < types.Count()) { result.Add(new CLayer.Rooms()); }
            return result;
        }

        public CLayer.Rooms Get(long roomid)
        {
            CLayer.Rooms room = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomId", DataPlug.DataType._BigInt, roomid));
            DataTable dt = Connection.GetTable("room_Get", param);
            if (dt.Rows.Count > 0)
            {
                room = new CLayer.Rooms();
                room.RoomId = Connection.ToLong(dt.Rows[0]["RoomId"]);
                room.RoomTypeId = Connection.ToInteger(dt.Rows[0]["RoomTypeId"]);
                room.RoomCount = Connection.ToInteger(dt.Rows[0]["RoomCount"]);
                room.PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]);
                room.Description = Connection.ToString(dt.Rows[0]["Description"]);
            }
            return room;
        }

        public void Delete(long roomid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomId", DataPlug.DataType._BigInt, roomid));
            Connection.ExecuteQuery("room_Delete", param);
            return;
        }

        public long Save(CLayer.Rooms room)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRoomId", DataPlug.DataType._BigInt, room.RoomId));
            param.Add(Connection.GetParameter("pRoomTypeId", DataPlug.DataType._Int, room.RoomTypeId));
            param.Add(Connection.GetParameter("pRoomCount", DataPlug.DataType._Int, room.RoomCount));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, room.PropertyId));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Varchar, room.Description));
            object result = Connection.ExecuteQueryScalar("room_Save", param);
            return Connection.ToLong(result);
        }
    }
}
