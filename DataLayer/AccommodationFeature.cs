using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
   public  class AccommodationFeature : DataLink
    {
        public List<CLayer.AccommodationFeature> GetAll()
        {
            List<CLayer.AccommodationFeature> AccomodationFeaturelist = new List<CLayer.AccommodationFeature>();

            DataTable dt = Connection.GetTable("accommodationfeature_GetAll");
            foreach (DataRow dr in dt.Rows)
            {
                AccomodationFeaturelist.Add(new CLayer.AccommodationFeature()
                {
                    AccommodationFeatureId = Connection.ToLong(dr["AccommodationFeatureId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Style = Connection.ToString(dr["Style"]),
                    CanDelete = ! Connection.ToBoolean(dr["CanDelete"]),
                    Showfeatures = Connection.ToBoolean(dr["IsShow"]),
                });
            }

            return AccomodationFeaturelist;
        }

        public int Save(CLayer.AccommodationFeature AccomodationFeature)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFeatureId", DataPlug.DataType._BigInt, AccomodationFeature.AccommodationFeatureId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, AccomodationFeature.Title));
            param.Add(Connection.GetParameter("pStyle", DataPlug.DataType._Varchar, AccomodationFeature.Style));
            param.Add(Connection.GetParameter("pIsShow", DataPlug.DataType._Bool, AccomodationFeature.Showfeatures));
            int result = Connection.ExecuteQuery("accommodationfeature_Save", param);
            return result;
        }

        public int SaveAccommodationFeature(CLayer.AccommodationFeature data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, data.AccommodationId));
            param.Add(Connection.GetParameter("pAccommodationFeatureId", DataPlug.DataType._BigInt, data.AccommodationFeatureId));
            object result = Connection.ExecuteQueryScalar("accommodationfeature_SaveAccommodation", param);
            return Connection.ToInteger(result);
        }

        public void Delete(int AccomodationFeatureID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pId", DataPlug.DataType._Int, AccomodationFeatureID));
            Connection.ExecuteQuery("accommodationfeature_Delete", param);
            return;
        }

        public void DeleteFeatureOnAccommodation(long accommodationId)
        {
            Connection.ExecuteSqlQuery("DELETE FROM accommodation_feature WHERE AccommodationId=" + accommodationId.ToString());
        }

        public CLayer.AccommodationFeature Get(int AccomodationFeatureID)
        {
            CLayer.AccommodationFeature roomtype = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFeatureId", DataPlug.DataType._BigInt, AccomodationFeatureID));
            DataTable dt = Connection.GetTable("accommodationfeature_Get", param);
            if (dt.Rows.Count > 0)
            {
                roomtype = new CLayer.AccommodationFeature();
                roomtype.AccommodationFeatureId = Connection.ToLong(dt.Rows[0]["AccommodationFeatureId"]);
                roomtype.Title = Connection.ToString(dt.Rows[0]["Title"]);
                roomtype.Style = Connection.ToString(dt.Rows[0]["Style"]);
                roomtype.Showfeatures = Connection.ToBoolean(dt.Rows[0]["IsShow"]);
            }
            return roomtype;
        }

        public List<CLayer.AccommodationFeature> GetAllWithSelectedForAccommodation(long accommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            DataTable dt = Connection.GetTable("accommodationfeature_GetAllForAccommodation", param);

            List<CLayer.AccommodationFeature> result = new List<CLayer.AccommodationFeature>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.AccommodationFeature()
                {
                    AccommodationFeatureId = Connection.ToLong(dr["AccommodationFeatureId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Style = Connection.ToString(dr["Style"]),
                    IsUsed = Connection.ToBoolean(dr["Used"])
                });
            }
            return result;
        }
    }
}
