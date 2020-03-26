using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using DataLayer.DataProvider;
using Newtonsoft.Json;

namespace DataLayer
{
    public class TBO : DataLink
    {
        public void UpdateCountry(string code, string Name)
        {
            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("pCode", DataPlug.DataType._Varchar, code));
            parameter.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, Name));
            Connection.ExecuteQueryScalar("Sp_TBO_Country_Upd", parameter);
        }
        public int UpdateDestinationCityId(int CityID, string Destination, string state, string statecode,
                                            string country, string countrycode)
        {
            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("pCityID", DataPlug.DataType._BigInt, CityID));
            parameter.Add(Connection.GetParameter("pDestination", DataPlug.DataType._Varchar, Destination));
            parameter.Add(Connection.GetParameter("pState", DataPlug.DataType._Varchar, state));
            parameter.Add(Connection.GetParameter("pStateCode", DataPlug.DataType._Varchar, statecode));
            parameter.Add(Connection.GetParameter("pCountry", DataPlug.DataType._Varchar, country));
            parameter.Add(Connection.GetParameter("pCountryCode", DataPlug.DataType._Varchar, countrycode));
            //Connection.ExecuteQueryScalar("Sp_TBO_DestinationCityId", parameter);
            object result = Connection.ExecuteQuery("Sp_TBO_DestinationCityId", parameter);
            return Convert.ToInt32(result);
        }
        public string CountryCode(string country)
        {
            string sql = "SELECT Tbo_code FROM country_codes WHERE Name= '" + country+"'";
            return Connection.ToString(Connection.ExecuteSQLQueryScalar(sql));
        }
        public int GetTBOCityId(string city)
        {
            string sql = "SELECT TboCityId FROM city WHERE Name like '%" + city + "%' and TboFlag ='Y' limit 1";
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar(sql));
        }
        public string TokenID()
        {
            CLayer.TBOService param = new CLayer.TBOService
            {
                ClientId = "ApiIntegrationNew",
                UserName = "STAYBAZAR",
                Password = "STAYBAZAR@123",
                EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString()
            };

            string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(param);
            string authurl = "http://api.tektravels.com/SharedServices/SharedData.svc/rest/Authenticate";
            string responsedata;
            responsedata = GetResponse(requestdata, authurl);

            DataSet lds_auth = new DataSet();

            lds_auth.ReadXml(TBO.GenerateStreamFromString(responsedata));

            string tokenid;
            tokenid = "";
            if (int.Parse(lds_auth.Tables["Error"].Rows[0]["ErrorCode"].ToString()) == 0)
            {
                // no error has occured
                if (int.Parse(lds_auth.Tables["Main"].Rows[0]["Status"].ToString()) == 1)
                {
                    // status is ok
                    tokenid = lds_auth.Tables["Main"].Rows[0]["TokenId"].ToString();
                }

            }
            return tokenid;
        }
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public string GetResponse(string requestData, string url)
        {
            string responseXML = string.Empty;
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(requestData);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/json";
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");
                Stream dataStream = request.GetRequestStream();
                dataStream.Write(data, 0, data.Length);
                dataStream.Close();
                WebResponse webResponse = request.GetResponse();
                Stream rsp = webResponse.GetResponseStream();
                if (rsp == null)
                {
                    //throw exception
                }
                //using (StreamReader readStream = new StreamReader(new GZipStream(rsp, CompressionMode.Decompress)))
                using (StreamReader readStream = new StreamReader(rsp))
                {
                    responseXML = JsonConvert.DeserializeXmlNode(readStream.ReadToEnd(), "Main").InnerXml;
                }
                return responseXML;
            }
            catch (WebException webEx)
            {
                //get the response stream
                WebResponse response = webEx.Response;
                Stream stream = response.GetResponseStream();
                String responseMessage = new StreamReader(stream).ReadToEnd();
                return responseMessage;
            }
        }
        public decimal  APIPrice(long userid)
        {
            string sql = "SELECT sell_markup_inPercentage FROM users_api_pricemarkup WHERE api_code= 5 and customer_id=" + userid;
            return Connection.ToDecimal(Connection.ExecuteSQLQueryScalar(sql));
        }
        public decimal APIPriceAll()
        {
            string sql = "SELECT markup_per FROM ggn_api_pricemarkup WHERE api_code = 5";
            return Connection.ToDecimal(Connection.ExecuteSQLQueryScalar(sql));
        }
        public int UpdateHotelList(string HotelID, string HotelName)
        {
            List<DataPlug.Parameter> parameters = new List<DataPlug.Parameter>();
            parameters.Add(Connection.GetParameter("pHotelId", DataPlug.DataType._Varchar, HotelID));
            parameters.Add(Connection.GetParameter("pHotelName", DataPlug.DataType._Varchar, HotelName));
            object result = Connection.ExecuteQueryScalar("sp_tbo_hotellist", parameters);
            return Convert.ToInt32(result);
        }
        public string GetCityName(int city)
        {
            string sql = "SELECT Name FROM city WHERE TboCityId = " + city + " and TboFlag ='Y' limit 1";
            return (Connection.ExecuteSQLQueryScalar(sql)).ToString();
        }
    }
}
