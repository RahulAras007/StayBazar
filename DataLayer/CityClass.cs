using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CityClass: DataLink
    {
        public List<CLayer.CityClass> GetAll()
        {
            List<CLayer.CityClass> CityClasslist = new List<CLayer.CityClass>();

            DataTable dt = Connection.GetSQLTable("SELECT * FROM cityclass");     
            foreach (DataRow dr in dt.Rows)
            {
            CityClasslist.Add(new CLayer.CityClass()
            {
              CityClassID = Connection.ToLong(dr["CityClassID"]),
              CityClassCode = Connection.ToString(dr["CityName"]),
              CityClassDescription = Connection.ToString(dr["CityDescription"]),
              //CountryID= Connection.ToLong(dr["CountryID"]),
              //StateID = Connection.ToLong(dr["StateID"]),
              //CityID= Connection.ToLong(dr["CityID"]),
              B2B_ID = Connection.ToLong(dr["B2b_ID"])
            });
            }
            return CityClasslist;
        }
        public CLayer.CityClass GetDetails(int id)
        {
            CLayer.CityClass CityClass = new CLayer.CityClass();


            string countryid="";
            string countryname="";
            string stateid = "";
            string satetname = "";
            string cityid = "";
            string cityname = "";

            //DataTable dt = Connection.GetSQLTable("SELECT * FROM cityclass CC  WHERE CC.CityclassID="+ id +"");

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("CityClassId", DataPlug.DataType._BigInt, id));
            DataTable dt = Connection.GetTable("cityclassBasedCities_Data", param);

            if (dt.Rows.Count>0)
            {
                CityClass.CityClassID = Convert.ToInt64(dt.Rows[0]["CityClassID"]);
                CityClass.CityClassCode = Convert.ToString(dt.Rows[0]["CityName"]);
                CityClass.CityClassDescription = Convert.ToString(dt.Rows[0]["CityDescription"]);
                CityClass.B2B_ID = Convert.ToInt64(dt.Rows[0]["B2b_ID"]);
                for(int i=0;i<=dt.Rows.Count-1;i++)
                {
                    countryid= countryid+","+ Convert.ToString(dt.Rows[i]["CountryID"]);
                    countryname = countryname + "," + Convert.ToString(dt.Rows[i]["CountryName"]);
                    stateid = stateid + "," + Convert.ToString(dt.Rows[i]["StateID"]);
                    satetname = satetname + "," + Convert.ToString(dt.Rows[i]["StateName"]);
                    cityid = cityid + "," + Convert.ToString(dt.Rows[i]["CityID"]);
                    cityname = cityname + "," + Convert.ToString(dt.Rows[i]["CityNames"]);
                }

                CityClass.CountryIDs = countryid.Substring(1, countryid.Length-1);
                CityClass.StateIDs = stateid.Substring(1, stateid.Length - 1);
                CityClass.CityIDs = cityid.Substring(1, cityid.Length - 1);

                CityClass.CountryNames = countryname.Substring(1, countryname.Length - 1);
                CityClass.StateNames = satetname.Substring(1, satetname.Length - 1);
                CityClass.CityNames = cityname.Substring(1, cityname.Length - 1);
            }
            return CityClass;
        }
        public CLayer.CityClassResult CheckCityIfExist(string cityid, int CityClassID)
        {
            CLayer.CityClassResult rslt = new CLayer.CityClassResult();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            
            param.Add(Connection.GetParameter("CityId", DataPlug.DataType._Varchar, cityid));
            param.Add(Connection.GetParameter("pCityClassID", DataPlug.DataType._BigInt, CityClassID));
            string Reslt = Convert.ToString(Connection.ExecuteQueryScalar("ExistCityInCityClass", param));

            rslt.Result = Reslt.ToString();
            return rslt;
        }
            public CLayer.CityClassResult Save(CLayer.CityClass cityclass)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            CLayer.CityClassResult r = new CLayer.CityClassResult();
            param.Add(Connection.GetParameter("pCityClassId", DataPlug.DataType._BigInt, cityclass.CityClassID));
            param.Add(Connection.GetParameter("pCityClassCode", DataPlug.DataType._Varchar, cityclass.CityClassCode));
            param.Add(Connection.GetParameter("pCityClassDesription", DataPlug.DataType._Varchar, cityclass.CityClassDescription));
            param.Add(Connection.GetParameter("pB2B_ID", DataPlug.DataType._BigInt, cityclass.B2B_ID));
            Int64 id = Convert.ToInt64(Connection.ExecuteQueryScalar("cityclass_Save", param));


            string[] CountryIds = cityclass.CountryIDs.Split(',');
            string[] StateIds = cityclass.StateIDs.Split(',');
            string[] CityIds =cityclass.CityIDs.Split(',');

            for(int i=0;i<= CityIds.Length-1;i++)
            {
                List<DataPlug.Parameter> param1 = new List<DataPlug.Parameter>();
                param1.Add(Connection.GetParameter("pCityClassId", DataPlug.DataType._BigInt, id));
                param1.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._BigInt, Convert.ToInt64(CountryIds[i])));
                param1.Add(Connection.GetParameter("pStateId", DataPlug.DataType._BigInt, Convert.ToInt64(StateIds[i])));
                param1.Add(Connection.GetParameter("pCityId", DataPlug.DataType._BigInt, Convert.ToInt64(CityIds[i])));
                Int64 CityId = Convert.ToInt64(Connection.ExecuteQueryScalar("cityclassBasedCities_Save", param1));
            }
                r.Result = "Success";
            return r;
        }

        public void Delete(int CityClassId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCityClassId", DataPlug.DataType._BigInt, CityClassId));
            Connection.ExecuteQuery("CityClass_Delete", param);
            return;
        }

        public CLayer.CityClass GetById(int CityClassId)
        {
            CLayer.CityClass taxTi = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCityClassID", DataPlug.DataType._BigInt, CityClassId));
            DataTable dt = Connection.GetTable("CityClassById_Get", param);
            if (dt.Rows.Count > 0)
            {
                taxTi = new CLayer.CityClass();
                taxTi.CityClassID= Connection.ToInteger(dt.Rows[0]["CityClassID"]);
                taxTi.CityClassCode = Connection.ToString(dt.Rows[0]["CityClassCode"]);
                taxTi.CityClassDescription = Connection.ToString(dt.Rows[0]["GradeDescription"]);
                taxTi.CountryID = Connection.ToInteger(dt.Rows[0]["CountryID"]);
                taxTi.StateID = Connection.ToInteger(dt.Rows[0]["StateID"]);
                taxTi.CityID = Connection.ToInteger(dt.Rows[0]["CityID"]);
            }
            return taxTi;
        }
    }
}
