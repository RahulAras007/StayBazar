using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Landmarks : DataLink
    {
        public CLayer.Landmarks Get(long landmarkId)
        {
            CLayer.Landmarks landmark = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pLandmarkId", DataPlug.DataType._BigInt, landmarkId));
            DataTable dt = Connection.GetTable("landmarks_Get", param);
            if (dt.Rows.Count > 0)
            {
                landmark = new CLayer.Landmarks()
                {
                    LandmarkId = Connection.ToLong(dt.Rows[0]["LandmarkId"]),
                    PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]),
                    LandmarkTitleId = Connection.ToInteger(dt.Rows[0]["LandmarkTitleId"]),
                    Landmark = Connection.ToString(dt.Rows[0]["Landmark"]),
                    Range = Connection.ToDecimal(dt.Rows[0]["Range"])
                };
            }
            return landmark;
        }

        public List<CLayer.Landmarks> GetOnProperty(long propertyId)
        {
            List<CLayer.Landmarks> landmarks = new List<CLayer.Landmarks>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            DataTable dt = Connection.GetTable("landmarks_GetOnProperty", param);
            foreach (DataRow dr in dt.Rows)
            {
                landmarks.Add(new CLayer.Landmarks()
                {
                    LandmarkId = Connection.ToLong(dr["LandmarkId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    LandmarkTitleId = Connection.ToInteger(dr["LandmarkTitleId"]),
                    Landmark = Connection.ToString(dr["Landmark"]),
                    Range = Connection.ToDecimal(dr["Range"])
                });
            }
            return landmarks;
        }

        public void Delete(long landmarkId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pLandmarkId", DataPlug.DataType._BigInt, landmarkId));
            Connection.ExecuteQuery("landmarks_Delete", param);
            return;
        }

        public void DeleteOnProperty(long propertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            Connection.ExecuteQuery("landmarks_DeleteOnProperty", param);
            return;
        }

        public long Save(CLayer.Landmarks data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pLandmarkId", DataPlug.DataType._BigInt, data.LandmarkId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pLandmarkTitleId", DataPlug.DataType._Int, data.LandmarkTitleId));
            param.Add(Connection.GetParameter("pLandmark", DataPlug.DataType._Varchar, data.Landmark));
            param.Add(Connection.GetParameter("pRange", DataPlug.DataType._Varchar, data.Range));
            object result = Connection.ExecuteQueryScalar("landmarks_Save", param);
            return Connection.ToInteger(result);
        }
    }
}
