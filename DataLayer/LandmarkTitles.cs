using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class LandmarkTitles : DataLink
    {
        public CLayer.LandmarkTitles Get(int landmarkTitleId)
        {
            CLayer.LandmarkTitles landmarktitle = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pLandmarkTitleId", DataPlug.DataType._Int, landmarkTitleId));
            DataTable dt = Connection.GetTable("landmarktitles_Get", param);
            if (dt.Rows.Count > 0)
            {
                landmarktitle = new CLayer.LandmarkTitles()
                {
                    LandmarkTitleId = Connection.ToInteger(dt.Rows[0]["LandmarkTitleId"]),
                    LandmarkTitle = Connection.ToString(dt.Rows[0]["LandmarkTitle"])
                };
            }
            return landmarktitle;
        }

        public List<CLayer.LandmarkTitles> GetAll()
        {
            List<CLayer.LandmarkTitles> result =  new List<CLayer.LandmarkTitles>();
            DataTable dt = Connection.GetTable("landmarktitles_GetAll", null);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.LandmarkTitles()
                {
                    LandmarkTitleId = Connection.ToInteger(dr["LandmarkTitleId"]),
                    LandmarkTitle = Connection.ToString(dr["LandmarkTitle"])
                });
            }
            return result;
        }

        public void Delete(int landmarkTitleId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pLandmarkTitleId", DataPlug.DataType._Int, landmarkTitleId));
            Connection.ExecuteQuery("landmarktitles_Delete", param);
            return;
        }

        public int Save(CLayer.LandmarkTitles data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pLandmarkTitleId", DataPlug.DataType._Int, data.LandmarkTitleId));
            param.Add(Connection.GetParameter("pLandmarkTitle", DataPlug.DataType._Varchar, data.LandmarkTitle));
            object result = Connection.ExecuteQueryScalar("landmarktitles_Save", param);
            return Connection.ToInteger(result);
        }
    }
}