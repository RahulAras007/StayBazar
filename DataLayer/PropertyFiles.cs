using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class PropertyFiles : DataLink
    {
        public List<CLayer.PropertyFiles> GetAll(long propertyId)//, CLayer.PropertyFiles.FileTypes fileType
        {
            DataTable dt = Connection.GetSQLTable("SELECT * FROM propertyfiles WHERE PropertyId=" +
                                                   propertyId.ToString());// + " AND FileType = " + (int)fileType
            List<CLayer.PropertyFiles> result = new List<CLayer.PropertyFiles>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.PropertyFiles()
                {
                    FileId = Connection.ToLong(dr["FileId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    FileName = Connection.ToString(dr["FileName"])
                });
                //,
                //    FileType = Connection.ToInteger(dr["FileType"])
            }
            return result;
        }

        public int Save(CLayer.PropertyFiles file)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, file.FileId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, file.PropertyId));
            param.Add(Connection.GetParameter("pFileName", DataPlug.DataType._Varchar, file.FileName));
            //param.Add(Connection.GetParameter("pFileType", DataPlug.DataType._Int, file.FileType));
            object result = Connection.ExecuteQueryScalar("propertyfiles_Save", param);
            return Connection.ToInteger(result);
        }

        public void Delete(long fileId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, fileId));
            Connection.ExecuteQuery("propertyfiles_Delete", param);
        }

        public void DeleteOnProperty(long propertyid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            Connection.ExecuteQuery("propertyfiles_DeleteOnProperty", param);
        }

        public CLayer.PropertyFiles Get(long fileId)
        {
            CLayer.PropertyFiles file = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, fileId));
            DataTable dt = Connection.GetTable("propertyfiles_Get", param);
            if (dt.Rows.Count > 0)
            {
                file = new CLayer.PropertyFiles();
                file.FileId = Connection.ToLong(dt.Rows[0]["FileId"]);
                file.PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]);
                file.FileName = Connection.ToString(dt.Rows[0]["FileName"]);
                //file.FileType = Connection.ToInteger(dt.Rows[0]["FileType"]);
            }
            return file;
        }
    }
}
