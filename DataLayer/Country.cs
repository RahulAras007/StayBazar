using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Country : DataLink
    {
        public List<CLayer.Country> GetAll()
        {
            DataTable dt = Connection.GetSQLTable("SELECT c.* FROM country c inner join state s on c.CountryId=s.CountryId  where c.Status=1 group by c.CountryId order by IsDefault DESC,c.Name");
            List<CLayer.Country> result = new List<CLayer.Country>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Country()
                {
                    CountryId = Connection.ToLong(dr["CountryId"]),
                    Name = Connection.ToString(dr["Name"]),
                    IsDefault = Connection.ToBoolean(dr["IsDefault"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    ForProperty = Connection.ToBoolean(dr["ForProperty"]),
                    Code = Connection.ToString(dr["Code"])
                });
            }
            return result;
        }

        public List<CLayer.Country> GetList()
        {
            DataTable dt = Connection.GetSQLTable("SELECT c.* FROM country c  order by IsDefault DESC,c.Name");
            List<CLayer.Country> result = new List<CLayer.Country>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Country()
                {
                    CountryId = Connection.ToLong(dr["CountryId"]),
                    Name = Connection.ToString(dr["Name"]),
                    IsDefault = Connection.ToBoolean(dr["IsDefault"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    ForProperty = Connection.ToBoolean(dr["ForProperty"]),
                    Code = Connection.ToString(dr["Code"])
                });
            }
            return result;
        }


        public List<CLayer.Country> GetAllForProperty()
        {
            DataTable dt = Connection.GetSQLTable("SELECT * FROM country Where ForProperty= 1 And Status = 1 order by IsDefault DESC,`Name`");
            List<CLayer.Country> result = new List<CLayer.Country>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Country()
                {
                    CountryId = Connection.ToLong(dr["CountryId"]),
                    Name = Connection.ToString(dr["Name"]),
                    IsDefault = Connection.ToBoolean(dr["IsDefault"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    ForProperty = Connection.ToBoolean(dr["ForProperty"])
                });
            }
            return result;
        }

        public int Save(CLayer.Country country)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._BigInt, country.CountryId));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, country.Name));
            param.Add(Connection.GetParameter("pCode", DataPlug.DataType._Varchar, country.Code));
            param.Add(Connection.GetParameter("pIsDefault", DataPlug.DataType._Bool, country.IsDefault));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, country.Status));
            param.Add(Connection.GetParameter("pForProperty", DataPlug.DataType._Bool, country.ForProperty));
            object result = Connection.ExecuteQueryScalar("country_Save", param);
            return Connection.ToInteger(result);
        }
        public int GDSSave(CLayer.Country country)
        {
            string pKeyWords = country.KeyWords;
            string sql="select keywords from country where countryid="+country.CountryId+"";
            string result = Convert.ToString(Connection.ExecuteSQLQueryScalar(sql));
            if(!string.IsNullOrEmpty(result))
            {
                string[] resultList = result.Split(',');
                for(int i=0;i< resultList.Length;i++)
                {
                    if(resultList[i]!= pKeyWords)
                    {
                        country.KeyWords = result + "," + pKeyWords;
                    }
                    
                }
            }

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._BigInt, country.CountryId));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, country.Name));
            param.Add(Connection.GetParameter("pCode", DataPlug.DataType._Varchar, country.Code));
            param.Add(Connection.GetParameter("pIsDefault", DataPlug.DataType._Bool, country.IsDefault));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, country.Status));
            param.Add(Connection.GetParameter("pForProperty", DataPlug.DataType._Bool, country.ForProperty));
            param.Add(Connection.GetParameter("pKeyWords", DataPlug.DataType._Varchar, country.KeyWords));
            object objresult = Connection.ExecuteQueryScalar("GDScountry_Save", param);
            return Connection.ToInteger(objresult);
        }
        public int GDSKeywordSave(CLayer.Country country)
        {
            string pKeyWords = country.KeyWords;
            string sql = "select keywords from country where countryid=" + country.CountryId + "";
            string result = Convert.ToString(Connection.ExecuteSQLQueryScalar(sql));
            if (!string.IsNullOrEmpty(result))
            {
                string[] resultList = result.Split(',');
                for (int i = 0; i < resultList.Length; i++)
                {
                    if (resultList[i] != pKeyWords)
                    {
                        if(!result.Contains(pKeyWords))
                        {
                            country.KeyWords = result + "," + pKeyWords;
                        }
                        else
                        {
                            country.KeyWords = result;
                        }
                 
                    }
                }
            }
            sql = "Update country set keywords='"+country.KeyWords+"' where countryid="+country.CountryId+"";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }
        //
        // TODO Delete Country
        //
        public CLayer.Country Get(int CountryId)
        {
            CLayer.Country country = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._BigInt, CountryId));
            DataTable dt = Connection.GetTable("country_Get", param);
            if (dt.Rows.Count > 0)
            {
                country = new CLayer.Country();
                country.CountryId = Connection.ToLong(dt.Rows[0]["CountryId"]);
                country.Name = Connection.ToString(dt.Rows[0]["Name"]);
                country.IsDefault = Connection.ToBoolean(dt.Rows[0]["IsDefault"]);
                country.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                country.ForProperty = Connection.ToBoolean(dt.Rows[0]["ForProperty"]);
                country.Code = Connection.ToString(dt.Rows[0]["Code"]);
            }
            return country;
        }
        public string  GetCountryWithKeyWords(string CountryName)
        {
            DataTable dt = Connection.GetSQLTable("SELECT g.CountryID,g.Country,ck.keywords FROM gdscountries g INNER JOIN countrykeywords ck" +
                                                " ON g.countryid = ck.countryid  WHERE keywords LIKE '" + CountryName + "%' LIMIT 1");
            //List<CLayer.GDSCountry> result = new List<CLayer.GDSCountry>();
            string result = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                CLayer.GDSCountry obj = new CLayer.GDSCountry();
                obj.CountryID = Connection.ToInteger(dr["CountryID"]);
                obj.Country = Connection.ToString(dr["Country"]);
                //obj.IATACode = Connection.ToString(dr["IATACode"]);
                //obj.UPSCode = Connection.ToString(dr["UPSCode"]);
                //obj.CurrencyCode = Connection.ToString(dr["CurrencyCode"]);
                //obj.CurrencyName = Connection.ToString(dr["CurrencyName"]);
                result = obj.Country;
            }
            return result;
        }
        public string  UpdateGDSCountry(string CountryName)
        {
            string pKeyword = CountryName;
            string CountryFromKeyWord = GetCountryWithKeyWords(CountryName);
            CountryName = string.IsNullOrEmpty(CountryFromKeyWord) ? CountryName : CountryFromKeyWord;

             string sql = "SELECT COUNT(*) FROM country WHERE NAME='" + CountryName + "'";
             object obj = Connection.ExecuteSQLQueryScalar(sql);
            if(Connection.ToInteger(obj)<1)
            {
                CLayer.Country country = new CLayer.Country();
                country.Name = CountryName;
                country.ForProperty = true;
                country.Status = 1;
             //   country.KeyWords = CountryName;
                Save(country);
            }
            else
            {
                string sqlc = "SELECT CountryID FROM country WHERE NAME='" + CountryName + "'";
                object objc = Connection.ExecuteSQLQueryScalar(sqlc);
                CLayer.Country country = new CLayer.Country();
                country.CountryId = Connection.ToLong(objc);
                country.Name = CountryName;
                country.ForProperty = true;
                country.Status = 1;
                //   country.KeyWords = pKeyword;
                Save(country);

            }
            return CountryName;
        }
        public List<CLayer.GDSCountry> GetGDSCountry(string CountryName)
        {
            //string CountryFromKeyWord = GetCountryWithKeyWords(CountryName);
            //CountryName = string.IsNullOrEmpty(CountryFromKeyWord) ? CountryName : CountryFromKeyWord;


            DataTable dt = Connection.GetSQLTable("SELECT c.CountryID,C.Name AS Country,g.IATACode,g.UPSCode,g.CurrencyCode,g.CurrencyName " +
                " FROM Country c INNER JOIN gdscountries g ON g.country=c.name  WHERE country like '"+ CountryName + "%'");
            List<CLayer.GDSCountry> result = new List<CLayer.GDSCountry>();
            foreach (DataRow dr in dt.Rows)
            {
                CLayer.GDSCountry obj = new CLayer.GDSCountry();
                obj.CountryID = Connection.ToInteger(dr["CountryID"]);
                obj.Country = Connection.ToString(dr["Country"]);
                obj.IATACode = Connection.ToString(dr["IATACode"]);
                obj.UPSCode = Connection.ToString(dr["UPSCode"]);
                obj.CurrencyCode = Connection.ToString(dr["CurrencyCode"]);
                obj.CurrencyName = Connection.ToString(dr["CurrencyName"]);
                result.Add(obj);

            }
            return result;
        }
      
    }
}
