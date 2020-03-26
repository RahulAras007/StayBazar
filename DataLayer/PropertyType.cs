using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class PropertyType : DataLink
    {
        public List<CLayer.PropertyType> GetAll()
        {
            DataTable dt = Connection.GetSQLTable("SELECT *,(select count(*) from property where" +
                                                  " property.PropertyTypeId=propertytype.PropertyTypeId) As candelete" +
                                                  "  FROM propertytype order by Title");
            List<CLayer.PropertyType> result = new List<CLayer.PropertyType>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.PropertyType()
                {
                    PropertyTypeId = Connection.ToLong(dr["PropertyTypeId"]),
                    Title = Connection.ToString(dr["Title"]),
                    CanDelete = (Connection.ToInteger(dr["candelete"]) < 1)
                });
            }
            return result;
        }

        public List<CLayer.PropertyType> GetActiveList()
        {
            DataTable dt = Connection.GetSQLTable("SELECT PropertyTypeId,Title  FROM propertytype order by Title");
            List<CLayer.PropertyType> result = new List<CLayer.PropertyType>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.PropertyType()
                {
                    PropertyTypeId = Connection.ToLong(dr["PropertyTypeId"]),
                    Title = Connection.ToString(dr["Title"])
                });
            }
            return result;
        }

        public int Save(CLayer.PropertyType propertytype)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyTypeId", DataPlug.DataType._BigInt, propertytype.PropertyTypeId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, propertytype.Title));
            object result = Connection.ExecuteQueryScalar("propertytype_Save", param);
            return Connection.ToInteger(result);
        }

        public void Delete(int PropertyTypeId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyTypeId", DataPlug.DataType._BigInt, PropertyTypeId));
            Connection.ExecuteQuery("propertytype_Delete", param);
            return;
        }

        public CLayer.PropertyType Get(int PropertyTypeId)
        {
            CLayer.PropertyType propertytype = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyTypeId", DataPlug.DataType._BigInt, PropertyTypeId));
            DataTable dt = Connection.GetTable("propertytype_Get", param);
            if (dt.Rows.Count > 0)
            {
                propertytype = new CLayer.PropertyType();
                propertytype.PropertyTypeId = Connection.ToLong(dt.Rows[0]["PropertyTypeId"]);
                propertytype.Title = Connection.ToString(dt.Rows[0]["Title"]);
            }
            return propertytype;
        }
    }
}
