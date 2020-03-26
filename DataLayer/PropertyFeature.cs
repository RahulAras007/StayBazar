using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PropertyFeature:DataLink
    {
        public List<CLayer.PropertyFeature> GetAll()
        {
            DataTable dt = Connection.GetSQLTable("SELECT *,(select count(*) from property_propertyfeature ppf where" +
                                                  " ppf.PropertyFeatureId=propertyfeature.PropertyFeatureId) As candelete" +
                                                  "  FROM propertyfeature order by Title");
            List<CLayer.PropertyFeature> result = new List<CLayer.PropertyFeature>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.PropertyFeature()
                {
                    PropertyFeatureId = Connection.ToLong(dr["PropertyFeatureId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Style = Connection.ToString(dr["Style"]),
                    Showfeatures = Connection.ToBoolean(dr["IsShow"]),
                    CanDelete = (Connection.ToInteger(dr["candelete"]) < 1)
                });
            }
            return result;
        }

        public List<CLayer.PropertyFeature> GetAllSearch()
        {
            DataTable dt = Connection.GetSQLTable("SELECT *,(select count(*) from property_propertyfeature ppf where" +
                                                  " ppf.PropertyFeatureId=propertyfeature.PropertyFeatureId) As candelete" +
                                                  "  FROM propertyfeature where IsShow=1  order by Title ");
            List<CLayer.PropertyFeature> result = new List<CLayer.PropertyFeature>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.PropertyFeature()
                {
                    PropertyFeatureId = Connection.ToLong(dr["PropertyFeatureId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Style = Connection.ToString(dr["Style"]),
                    Showfeatures = Connection.ToBoolean(dr["IsShow"]),
                    CanDelete = (Connection.ToInteger(dr["candelete"]) < 1)
                });
            }
            return result;
        }
        public int Save(CLayer.PropertyFeature propertyfeature)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyFeatureId", DataPlug.DataType._BigInt, propertyfeature.PropertyFeatureId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, propertyfeature.Title));
            param.Add(Connection.GetParameter("pStyle", DataPlug.DataType._Varchar, propertyfeature.Style));
            param.Add(Connection.GetParameter("pIsShow", DataPlug.DataType._Bool, propertyfeature.Showfeatures));
            object result = Connection.ExecuteQueryScalar("propertyfeature_Save", param);
            return Connection.ToInteger(result);
        }

        public int SavePropertyFeature(CLayer.PropertyFeature data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pPropertyFeatureId", DataPlug.DataType._BigInt, data.PropertyFeatureId));
            object result = Connection.ExecuteQueryScalar("propertyfeature_SaveProperty", param);
            return Connection.ToInteger(result);
        }

        public void Delete(int PropertyFeatureId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyFeatureId", DataPlug.DataType._BigInt, PropertyFeatureId));
            Connection.ExecuteQuery("propertyfeature_Delete", param);
            return;
        }

        public void DeleteFeatureOnProperty(long propertyid)
        {
            Connection.ExecuteSqlQuery("DELETE FROM property_propertyfeature WHERE PropertyId=" + propertyid.ToString());
        }

        public CLayer.PropertyFeature Get(int PropertyFeatureId)
        {
            CLayer.PropertyFeature propertyfeature = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyFeatureId", DataPlug.DataType._BigInt, PropertyFeatureId));
            DataTable dt = Connection.GetTable("propertyfeature_Get", param);
            if (dt.Rows.Count > 0)
            {
                propertyfeature = new CLayer.PropertyFeature();
                propertyfeature.PropertyFeatureId = Connection.ToLong(dt.Rows[0]["PropertyFeatureId"]);
                propertyfeature.Title = Connection.ToString(dt.Rows[0]["Title"]);
                propertyfeature.Style = Connection.ToString(dt.Rows[0]["Style"]);
                propertyfeature.Showfeatures = Connection.ToBoolean(dt.Rows[0]["IsShow"]);
            }
            return propertyfeature;
        }

        public List<CLayer.PropertyFeature> GetAllWithSelectedForProperty(long propertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            DataTable dt = Connection.GetTable("propertyfeature_GetAllForProperty", param);

            List<CLayer.PropertyFeature> result = new List<CLayer.PropertyFeature>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.PropertyFeature()
                {
                    PropertyFeatureId = Connection.ToLong(dr["PropertyFeatureId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Style = Connection.ToString(dr["Style"]),
                    IsUsed = Connection.ToBoolean(dr["Used"])
                });
            }
            return result;
        }

        public List<CLayer.PropertyFeature> GetFeaturesForProperty(long propertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            DataTable dt = Connection.GetTable("propertyfeature_GetPropertyFeatures", param);

            List<CLayer.PropertyFeature> result = new List<CLayer.PropertyFeature>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.PropertyFeature()
                {
                    PropertyFeatureId = Connection.ToLong(dr["PropertyFeatureId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Style = Connection.ToString(dr["Style"])
                    //IsUsed = Connection.ToBoolean(dr["Used"])
                });
            }
            return result;
        }
        public DataTable SaveGDSFeatures(List<string> codes)
        {
            string fullcsv = string.Join("','", codes);
            fullcsv = "'" + fullcsv + "'";

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, fullcsv));
            DataTable dt = Connection.GetTable("gds_GetGDSPropertyFeature", param);

            string insertcsv = "";

            if (dt.Rows.Count != codes.Count)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    codes.Remove(Connection.ToString(dr[0]));
                }
                foreach (string item in codes)
                {
                    if (insertcsv != "") { insertcsv = insertcsv + ","; }
                    insertcsv = insertcsv + "('',2,'" + item + "',1)";
                }
            }

            param.Clear();
            //param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, insertcsv));
            //param.Add(Connection.GetParameter("pFullCodes", DataPlug.DataType._Text, fullcsv));
            //dt = Connection.GetTable("gds_GetSearchProperties", param);

            return dt;
        }
        public DataTable SaveGDSFeatureProperties(List<string> codes)
        {
            string fullcsv = string.Join("','", codes);
            fullcsv = "'" + fullcsv + "'";

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, fullcsv));
            DataTable dt = Connection.GetTable("gds_GetGDSPropertyFeatureSaveProperty", param);

            string insertcsv = "";

            if (dt.Rows.Count != codes.Count)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    codes.Remove(Connection.ToString(dr[0]));
                }
                foreach (string item in codes)
                {
                    if (insertcsv != "") { insertcsv = insertcsv + ","; }
                    insertcsv = insertcsv + "('',2,'" + item + "',1)";
                }
            }

            param.Clear();
            //param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, insertcsv));
            //param.Add(Connection.GetParameter("pFullCodes", DataPlug.DataType._Text, fullcsv));
            //dt = Connection.GetTable("gds_GetSearchProperties", param);

            return dt;
        }
    }
}
