using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class AccommodationType : DataLink
    {
        public void SetCategoryType(int typeID, string categoriesCSV)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTypeId", DataPlug.DataType._Int, typeID));
            param.Add(Connection.GetParameter("pCategories", DataPlug.DataType._Varchar, categoriesCSV));
            Connection.ExecuteQuery("accommodationtype_SetStayType", param);
        }
        public List<CLayer.StayCategoryAccommodationType> GetAllStayCategoryType(int accommodationTypeId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTypeId", DataPlug.DataType._Int, accommodationTypeId));
            DataTable dt = Connection.GetTable("accommodationtype_GetaccommodationTypes", param);
            List<CLayer.StayCategoryAccommodationType> result = new List<CLayer.StayCategoryAccommodationType>();
            CLayer.StayCategoryAccommodationType tmp;
            foreach (DataRow dr in dt.Rows)
            {
                tmp = new CLayer.StayCategoryAccommodationType();
                tmp.IsUsed = Connection.ToInteger(dr["isused"]) > 0;
                tmp.Selected = Connection.ToInteger(dr["isselected"]) > 0;
                tmp.Title = Connection.ToString(dr["Title"]);
                tmp.StayCategoryId = Connection.ToInteger(dr["StayCategoryId"]);
                //tmp.RoomTypeId = Connection.ToInteger(dr["PropertyTypeId"]);
                result.Add(tmp);
            }
            return result;
        }
        public List<CLayer.AccommodationType> GetAll()
        {
            List<CLayer.AccommodationType> typelist = new List<CLayer.AccommodationType>();

            DataTable dt = Connection.GetTable("accommodationtype_GetAll");
            foreach (DataRow dr in dt.Rows)
            {
                typelist.Add(new CLayer.AccommodationType()
                {
                    AccommodationTypeId = Connection.ToInteger(dr["AccommodationTypeId"]),
                    Title = Connection.ToString(dr["Title"]),
                    CanDelete = Connection.ToBoolean(dr["CanDelete"])
                });
            }

            return typelist;
        }

        public List<CLayer.Accommodation> GetBypropertyid(long id)
        {
            List<CLayer.Accommodation> typelist = new List<CLayer.Accommodation>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pid", DataPlug.DataType._Int, id)); 
            DataTable dt = Connection.GetTable("accommodationtype_GetAllbypropertyid", param);
            foreach (DataRow dr in dt.Rows)
            {
                typelist.Add(new CLayer.Accommodation()
                {
                    AccommodationTypeId = Connection.ToInteger(dr["AccommodationTypeId"]),
                    AccommodationType = Connection.ToString(dr["AccommodationType"]),
                    AccommodationId = Connection.ToInteger(dr["AccommodationId"])          
                });
            }

            return typelist;
        }

        public int Save(CLayer.AccommodationType data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTypeId", DataPlug.DataType._BigInt, data.AccommodationTypeId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, data.Title));
            object result = Connection.ExecuteQueryScalar("accommodationtype_Save", param);
            return Connection.ToInteger(result);
        }

        public void Delete(int accTypeId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTypeId", DataPlug.DataType._BigInt, accTypeId));
            Connection.ExecuteQuery("accommodationtype_Delete", param);
            return;
        }

        public CLayer.AccommodationType Get(int accTypeId)
        {
            CLayer.AccommodationType accType = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTypeId", DataPlug.DataType._BigInt, accTypeId));
            DataTable dt = Connection.GetTable("accommodationtype_Get", param);
            if (dt.Rows.Count > 0)
            {
                accType = new CLayer.AccommodationType();
                accType.AccommodationTypeId = Connection.ToInteger(dt.Rows[0]["AccommodationTypeId"]);
                accType.Title = Connection.ToString(dt.Rows[0]["Title"]);
            }
            return accType;
        }
        public int AccommodationTypeSave(string title,int CatID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, title));
            param.Add(Connection.GetParameter("pCatID", DataPlug.DataType._Varchar, CatID));
            object result = Connection.ExecuteQueryScalar("sp_accommodationtype_Save", param);
            return Connection.ToInteger(result);
        }
        public int TBOAccommodationTypeSave(string title)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, title));
            object result = Connection.ExecuteQueryScalar("sp_TBO_accommodationtype_Save", param);
            return Connection.ToInteger(result);
        }
        public int GetAccommodationTypeTamCatID(long id)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, id));
            object result = Connection.ExecuteQueryScalar("Sp_GetAccommodation_tamcatid", param);
            return Connection.ToInteger(result);
        }
    }
}
