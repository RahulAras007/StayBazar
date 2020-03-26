using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
     public class StayCategory : DataProvider.DataLink
    {
        public List<CLayer.StayCategory> GetAll()
        {
            List<CLayer.StayCategory> staycategorylist = new List<CLayer.StayCategory>();

            DataTable dt = Connection.GetSQLTable("SELECT *,(CASE WHEN (SELECT COUNT(*) FROM accommodation p WHERE p.StayCategoryId=c.CategoryId)>0 THEN 0 ELSE 1 END ) " +
                     " AS CanDelete FROM staycategory c  ORDER BY Title ASC");
            foreach (DataRow dr in dt.Rows)
            {
                staycategorylist.Add(new CLayer.StayCategory()
                {
                    CategoryId = Connection.ToLong(dr["CategoryId"]),
                    Title = Connection.ToString(dr["Title"]),
                    CanDelete = Connection.ToBoolean(dr["CanDelete"])
                });
            }
            return staycategorylist;
        }
        
        public List<CLayer.StayCategory> GetActiveList()
        {
            List<CLayer.StayCategory> staycategorylist = new List<CLayer.StayCategory>();

            DataTable dt = Connection.GetTable("staycategory_GetList");

            foreach (DataRow dr in dt.Rows)
            {
                staycategorylist.Add(new CLayer.StayCategory()
                {
                    CategoryId = Connection.ToLong(dr["CategoryId"]),
                    Title = Connection.ToString(dr["Title"])
                });
            }

            return staycategorylist;
        }
        public List<CLayer.StayCategory> GetBypropertyid(long id)
        {
            List<CLayer.StayCategory> typelist = new List<CLayer.StayCategory>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pid", DataPlug.DataType._Int, id));
            DataTable dt = Connection.GetTable("StayCategoryIdtype_GetAllbypropertyid", param);
            foreach (DataRow dr in dt.Rows)
            {
                typelist.Add(new CLayer.StayCategory()
                {
                    CategoryId = Connection.ToInteger(dr["CategoryId"]),
                    Title = Connection.ToString(dr["Title"])
                });
            }

            return typelist;
        }

        public int Save(CLayer.StayCategory staycategory)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCategoryId", DataPlug.DataType._BigInt, staycategory.CategoryId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, staycategory.Title));
            int result = Connection.ExecuteQuery("staycategory_Save", param);
            return result;
        }

        public void Delete(int CategoryID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCategoryId", DataPlug.DataType._BigInt, CategoryID));
            Connection.ExecuteQuery("staycategory_Delete", param);
            return;
        }

        public CLayer.StayCategory Get(int CategoryID)
        {
            CLayer.StayCategory staycategory = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCategoryId", DataPlug.DataType._BigInt, CategoryID));
            DataTable dt = Connection.GetTable("staycategory_Get", param);
            if (dt.Rows.Count > 0)
            {
                staycategory = new CLayer.StayCategory();
                staycategory.CategoryId = Connection.ToLong(dt.Rows[0]["CategoryId"]);
                staycategory.Title = Connection.ToString(dt.Rows[0]["Title"]);
            }
            return staycategory;
        }

        public DataTable GetCategoryHotel()
        {
            string code = "Hotel";
            DataTable dt = Connection.GetSQLTable("Select * From StayCategory Where Title like '"+code +"'");

            //string sql = "Select * From StayCategory Where Title like Hotel ";
            //object obj = Connection.ExecuteSQLQueryScalar(sql);
            return dt;
        }
    }
}
