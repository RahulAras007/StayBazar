using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UserFiles : DataLink
    {
        public List<CLayer.UserFiles> GetAll(long userId)
        {
            DataTable dt = Connection.GetSQLTable("SELECT * FROM userfiles WHERE UserId=" +
                                                   userId.ToString());
            List<CLayer.UserFiles> result = new List<CLayer.UserFiles>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.UserFiles()
                {
                    FileId = Connection.ToLong(dr["FileId"]),
                    FileName = Connection.ToString(dr["FileName"]),
                    UserId = Connection.ToLong(dr["UserId"]),
                    Document = Connection.ToInteger(dr["Document"])
                });
            }
            return result;
        }

        public int Save(CLayer.UserFiles file)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, file.FileId));
            param.Add(Connection.GetParameter("pFileName", DataPlug.DataType._Varchar, file.FileName));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, file.UserId));
            param.Add(Connection.GetParameter("pDocument", DataPlug.DataType._Int, file.Document));
            object result = Connection.ExecuteQueryScalar("userfiles_Save", param);
            return Connection.ToInteger(result);
        }
        public long isCheck(long pUserId,int pDocument)
        {

        
            string sql = "SELECT COUNT(UserId) INTO usercount FROM userfiles WHERE UserId="+pUserId +"AND Document="+pDocument;
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        public void Delete(long fileId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, fileId));
            Connection.ExecuteQuery("userfiles_Delete", param);
        }

        public void DeleteOnUser(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            Connection.ExecuteQuery("userfiles_DeleteOnProperty", param);
        }

        public CLayer.UserFiles Get(long fileId)
        {
            CLayer.UserFiles file = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, fileId));
            DataTable dt = Connection.GetTable("userfiles_Get", param);
            if (dt.Rows.Count > 0)
            {
                file = new CLayer.UserFiles();
                file.FileId = Connection.ToLong(dt.Rows[0]["FileId"]);
                file.FileName = Connection.ToString(dt.Rows[0]["FileName"]);
                file.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                file.Document = Connection.ToInteger(dt.Rows[0]["Document"]);
            }
            return file;
        }

        public CLayer.UserFiles GetOnUserAndType(long UserId, int Type)
        {
            CLayer.UserFiles file = null;
            DataTable dt = Connection.GetSQLTable("SELECT * FROM userfiles WHERE UserId=" +
                                                   UserId.ToString() + " AND Document=" +
                                                   Type.ToString());
            if (dt.Rows.Count > 0)
            {
                file = new CLayer.UserFiles();
                file.FileId = Connection.ToLong(dt.Rows[0]["FileId"]);
                file.FileName = Connection.ToString(dt.Rows[0]["FileName"]);
                file.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                file.Document = Connection.ToInteger(dt.Rows[0]["Document"]);
            }
            return file;
        }

        public List<CLayer.UserFiles> GetOnUser(long UserId)
        {
            DataTable dt = Connection.GetSQLTable("SELECT * FROM userfiles WHERE UserId=" +
                                                   UserId.ToString());
            List<CLayer.UserFiles> result = new List<CLayer.UserFiles>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.UserFiles()
                {
                    FileId = Connection.ToLong(dr["FileId"]),
                    FileName = Connection.ToString(dr["FileName"]),
                    UserId = Connection.ToLong(dr["UserId"]),
                    Document = Connection.ToInteger(dr["Document"])
                });
            }
            return result;
        }
    }
}
