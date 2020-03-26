using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class StaticPage : DataLink
    {
        public void SetUpdateDate(long pageId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPageId", DataPlug.DataType._BigInt, pageId));
            Connection.ExecuteQuery("staticpage_SetUpdateDate", param);
        }
        public List<CLayer.StaticPage> GetAll()
        {
            string sql = "Select * From staticpage";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.StaticPage> result = new List<CLayer.StaticPage>();
            CLayer.StaticPage t;
            DateTime tm;
            foreach(DataRow dr in dt.Rows)
            {
                t = new CLayer.StaticPage();
                t.PageId = Connection.ToLong(dr["PageId"]);
                t.City = Connection.ToString(dr["City"]);
                t.Location = Connection.ToString(dr["Location"]);
                t.PageTitle = Connection.ToString(dr["PageTitle"]);
                tm  = Connection.ToDate(dr["LastUpdate"]);
                if (tm == DateTime.Now)
                    t.LastUpdate = "";
                else
                    t.LastUpdate = tm.ToShortDateString();
                //t.City = Connection.ToString(dr["City"]);
                //t.City = Connection.ToString(dr["City"]);
                //t.City = Connection.ToString(dr["City"]);
                //t.City = Connection.ToString(dr["City"]);
                result.Add(t);
            }
            return result;
        }
        public List<CLayer.StaticPage> GetAllWidget()
        {
            
            DataTable dt = Connection.GetTable("staticpage_GetAllForWidget");
            List<CLayer.StaticPage> result = new List<CLayer.StaticPage>();
            CLayer.StaticPage t;
      
            foreach (DataRow dr in dt.Rows)
            {
                t = new CLayer.StaticPage();
                t.PageId = Connection.ToLong(dr["PageId"]);
                t.City = Connection.ToString(dr["City"]);
                t.Location = Connection.ToString(dr["Location"]);
          //      t.PageTitle = Connection.ToString(dr["PageTitle"]);
                t.Image = Connection.ToString(dr["Image"]);
                t.RootFolder = Connection.ToString(dr["RootFolder"]);
                t.Description = Connection.ToString(dr["Description"]);
                t.Description = CLayer.HtmlRemoval.StripTagsRegex(t.Description);
                if (t.Description.Length > 100) t.Description = t.Description.Substring(0, 100)+"..";
       //         tm = Connection.ToDate(dr["LastUpdate"]);
        //        if (tm == DateTime.Now)
      //              t.LastUpdate = "";
        //        else
         //           t.LastUpdate = tm.ToShortDateString();
                result.Add(t);
            }
            return result;
        }
        public CLayer.StaticPage GetPage(long pageId)
        {
            string sql = "Select * From staticpage Where PageId=" + pageId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.StaticPage result = null;
            if(dt.Rows.Count>0)
            {
                DataRow dr = dt.Rows[0];
               result= new CLayer.StaticPage();
                result.PageId = Connection.ToLong(dr["PageId"]);
                result.City = Connection.ToString(dr["City"]);
                result.Location = Connection.ToString(dr["Location"]);
                result.PageTitle = Connection.ToString(dr["PageTitle"]);
                result.Description = Connection.ToString(dr["Description"]);

                result.FileName = Connection.ToString(dr["FileName"]);
                result.Image = Connection.ToString(dr["Image"]);

                result.ShowInWidget = Connection.ToBoolean(dr["ShowInWidget"]);
                result.RootFolder = Connection.ToString(dr["RootFolder"]);
            }
            return result;
        }

        

        public void Delete(long pageId)
        {
            string sql = "Delete From StaticPage where PageId=" + pageId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void RemoveStaticProperty(long PropertyId)
        {
            string sql = "Delete From staticproperties where PropertyId=" + PropertyId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public long Save(CLayer.StaticPage data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPageId", DataPlug.DataType._BigInt, data.PageId));
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, data.City));

            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, data.Location));
            param.Add(Connection.GetParameter("pImage", DataPlug.DataType._Varchar, data.Image));
            param.Add(Connection.GetParameter("pFilename", DataPlug.DataType._Varchar, data.FileName));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Varchar, data.Description));
            param.Add(Connection.GetParameter("pShowInWidget", DataPlug.DataType._Bool, data.ShowInWidget));
            param.Add(Connection.GetParameter("pRootFolder", DataPlug.DataType._Varchar, data.RootFolder));
            param.Add(Connection.GetParameter("pPageTitle", DataPlug.DataType._Varchar, data.PageTitle));
            object obj = Connection.ExecuteQueryScalar("staticpage_Save", param);
            return Connection.ToLong(obj);
            // B2
            //staticpage_Save
            // pDescription VARCHAR(3000),pShownInWidget BOOL,pRootFolder VARCHAR(255),pPageTitle VARCHAR(500)
        }
        public long SaveProperty(CLayer.StaticPage data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPageId", DataPlug.DataType._BigInt, data.PageId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._Varchar, data.PropertyId));
            object obj = Connection.ExecuteQueryScalar("staticpageProperty_Save", param);
            return Connection.ToLong(obj);
        }
    }
}
