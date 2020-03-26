using System.Collections.Generic;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class Tamarind : DataLink
    {
        public void UpdateCity(string Name,string cityid)
        {
            string City = string.Empty;
            string[] pList = Name.Split(',');
            if (pList.Length > 1)
            {
                City  = pList[0];
                //BLayer.Country.UpdateGDSCountry(result);
            }
            else
            {
                City = Name;
            }

            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, City));
            parameter.Add(Connection.GetParameter("pCityid", DataPlug.DataType._Varchar, cityid));
            Connection.ExecuteQueryScalar("Sp_Tamarind_City_Upd", parameter);
        }
        public void UpdateCurrency(string Name)
        {
            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, Name));
            Connection.ExecuteQueryScalar("Sp_Tamarind_currency_Upd", parameter);
        }
        public void UpdateCountry(string Name)
        {
            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, Name));
            Connection.ExecuteQueryScalar("Sp_Tamarind_Country_Upd", parameter);
        }
        public void UpdateHotelList(int HotelID,string HotelName)
        {
            List<DataPlug.Parameter> parameters = new List<DataPlug.Parameter>();
            parameters.Add(Connection.GetParameter("pHotelId", DataPlug.DataType._BigInt, HotelID));
            parameters.Add(Connection.GetParameter("pHotelName", DataPlug.DataType._Varchar, HotelName));
            Connection.ExecuteQueryScalar("sp_tamarind_hotellist", parameters);
        }
        public  decimal  APIPrice(long userid)
        {
            string sql = "SELECT sell_markup_inPercentage FROM users_api_pricemarkup WHERE api_code = 4 and customer_id=" + userid;
            return Connection.ToDecimal(Connection.ExecuteSQLQueryScalar(sql));
        }
        public decimal  APIPriceAll()
        {
            string sql = "SELECT markup_per FROM ggn_api_pricemarkup WHERE api_code = 4";
            return Connection.ToDecimal(Connection.ExecuteSQLQueryScalar(sql));
        }
    }
}
