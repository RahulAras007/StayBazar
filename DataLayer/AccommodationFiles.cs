using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AccommodationFiles : DataLink
    {
        public List<CLayer.AccommodationFiles> GetOnAccommodation(long accommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            DataTable dt = Connection.GetTable("accommodationfiles_GetOnAccommodation", param);
            List<CLayer.AccommodationFiles> result = new List<CLayer.AccommodationFiles>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.AccommodationFiles()
                {
                    FileId = Connection.ToLong(dr["FileId"]),
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    FileName = Connection.ToString(dr["FileName"])
                });
            }
            return result;
        }

        public CLayer.AccommodationFiles GetSingleOnAccommodation(long accommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            DataTable dt = Connection.GetTable("accommodationfiles_GetSingleOnAccommodation", param);
            CLayer.AccommodationFiles result = null;
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.AccommodationFiles()
                {
                    FileId = Connection.ToLong(dt.Rows[0]["FileId"]),
                    AccommodationId = Connection.ToLong(dt.Rows[0]["AccommodationId"]),
                    FileName = Connection.ToString(dt.Rows[0]["FileName"])
                };
            }
            return result;
        }

        public int Save(CLayer.AccommodationFiles file)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, file.FileId));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, file.AccommodationId));
            param.Add(Connection.GetParameter("pFileName", DataPlug.DataType._Varchar, file.FileName));
            object result = Connection.ExecuteQueryScalar("accommodationfiles_Save", param);
            return Connection.ToInteger(result);
        }

        public void Delete(long fileId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, fileId));
            Connection.ExecuteQuery("accommodationfiles_Delete", param);
        }

        public void DeleteOnAccommodation(long accommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            Connection.ExecuteQuery("accommodationfiles_DeleteOnAccommodation", param);
        }

        public CLayer.AccommodationFiles Get(long fileId)
        {
            CLayer.AccommodationFiles result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFileId", DataPlug.DataType._BigInt, fileId));
            DataTable dt = Connection.GetTable("accommodationfiles_Get", param);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.AccommodationFiles()
                {
                    FileId = Connection.ToLong(dt.Rows[0]["FileId"]),
                    AccommodationId = Connection.ToLong(dt.Rows[0]["AccommodationId"]),
                    FileName = Connection.ToString(dt.Rows[0]["FileName"])
                };
            }
            return result;
        }
    }
}
