using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class City : DataLink
    {
        public List<CLayer.City> GetOnState(int stateId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._Int, stateId));
            DataTable dt = Connection.GetTable("city_GetOnState", param);
            List<CLayer.City> result = new List<CLayer.City>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.City()
                {
                    CityId = Connection.ToInteger(dr["CityId"]),
                    Name = Connection.ToString(dr["Name"]),
                    StateId = Connection.ToInteger(dr["StateId"]),
                    Keywords = Connection.ToString(dr["Keywords"]),
                });
            }
            return result;
        }

        public List<CLayer.City> GetCityByname(string name)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("PName", DataPlug.DataType._Varchar, name));
            DataTable dt = Connection.GetTable("city_GetOnStateByName", param);
            List<CLayer.City> result = new List<CLayer.City>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.City()
                {
                    CityId = Connection.ToInteger(dr["CityId"]),
                    Name = Connection.ToString(dr["Name"]),
                    StateId = Connection.ToInteger(dr["StateId"]),
                    Keywords = Connection.ToString(dr["Keywords"]),


                });
            }
            return result;
        }


        public CLayer.City Get(int? cityId)
        {
            CLayer.City city = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, cityId));
            DataTable dt = Connection.GetTable("city_Get", param);
            if (dt.Rows.Count > 0)
            {
                city = new CLayer.City()
                {
                    CityId = Connection.ToInteger(dt.Rows[0]["CityId"]),
                    Name = Connection.ToString(dt.Rows[0]["Name"]),
                    StateId = Connection.ToInteger(dt.Rows[0]["StateId"]),
                    ForListing = Connection.ToBoolean(dt.Rows[0]["ForListing"]),
                    Keywords = Connection.ToString(dt.Rows[0]["Keywords"])
                };
            }
            return city;
        }

        public int Save(CLayer.City data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, data.CityId));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, data.Name));
            param.Add(Connection.GetParameter("pStateId", DataPlug.DataType._Int, data.StateId));
            param.Add(Connection.GetParameter("pForListing", DataPlug.DataType._Bool, data.ForListing));
            param.Add(Connection.GetParameter("pKeywords", DataPlug.DataType._Varchar, data.Keywords));
            object result = Connection.ExecuteQueryScalar("city_Save", param);
            return Connection.ToInteger(result);
        }

        public string GetLocation(string search)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, search));
            DataTable result = Connection.GetTable("common_getLocation", param);
            string city, state, country;
            city = "";
            state = country = "";
            StringBuilder sb = new StringBuilder();
            if (result.Rows.Count > 0)
            {
                city = Connection.ToString(result.Rows[0]["city"]);
                state = Connection.ToString(result.Rows[0]["state"]);
                country = Connection.ToString(result.Rows[0]["country"]);
            }

            if (state != "")
            {
                if (city != "") city = city + ",";
                city = city + state;
            }
            if (country != "")
            {
                if (city != "") city = city + ",";
                city = city + country;
            }
            return city;
        }

        public List<CLayer.City> GetAllForListing()
        {
            DataTable dt = Connection.GetTable("City_GetAllForListing", null);
            List<CLayer.City> result = new List<CLayer.City>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.City()
                {
                    CityId = Connection.ToInteger(dr["CityId"]),
                    Name = Connection.ToString(dr["Name"]),
                    StateId = Connection.ToInteger(dr["StateId"]),
                    RoomCount = Connection.ToInteger(dr["RoomCount"])
                });
            }
            return result;
        }

        public string GetCityAll()
        {
            DataTable dt = Connection.GetSQLTable("Select Name from city");
            StringBuilder ct = new StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                ct.Append("'");
                ct.Append(Connection.ToString(dr["Name"]));
                ct.Append("',");
            }
            return ct.ToString();
        }
        public CLayer.City GetCityID(string CityName)
        {
            CLayer.City city = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, CityName));
            DataTable dt = Connection.GetTable("cityID_Get", param);
            if (dt.Rows.Count > 0)
            {
                city = new CLayer.City()
                {
                    CityId = Connection.ToInteger(dt.Rows[0]["CityId"]),
                    Name = Connection.ToString(dt.Rows[0]["Name"]),
                    StateId = Connection.ToInteger(dt.Rows[0]["StateId"]),
                    ForListing = Connection.ToBoolean(dt.Rows[0]["ForListing"]),
                    Keywords = Connection.ToString(dt.Rows[0]["Keywords"])
                };
            }
            else
            {
                city = new CLayer.City()
                {
                    CityId = 0,
                    Name = "",
                    StateId = 0,
                    //ForListing = Connection.ToBoolean(dt.Rows[0]["ForListing"]),
                    //Keywords = Connection.ToString(dt.Rows[0]["Keywords"])
                };
            }
            return city;
        }
        public int UpdateGDSCity(int StateID, string City = "Others")
        {
            int result = 0;
            string sql = "SELECT COUNT(*) FROM City WHERE StateID='" + StateID + "' and name='" + City + "'";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            if (Connection.ToInteger(obj) < 1)
            {
                CLayer.City objCity = new CLayer.City();
                objCity.CityId = 0;
                objCity.Name = City;
                objCity.StateId = StateID;
                objCity.ForListing = true;
                objCity.Keywords = City;
                result = Save(objCity);
            }
            else
            {
                sql = "SELECT cityid FROM City WHERE StateID='" + StateID + "' and name='" + City + "'";
                obj = Connection.ExecuteSQLQueryScalar(sql);

                result = Connection.ToInteger(obj);
            }
            return result;
        }
        public string GetTamarindCityID(string CityName)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, CityName));
            object result = Connection.ExecuteQueryScalar("sp_tamarind_Getcityid", param);
            return Connection.ToString(result);
        }
    }
}

