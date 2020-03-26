using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Address : DataLink
    {
        public void Delete(long AddressId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAddressId", DataPlug.DataType._BigInt, AddressId));
            Connection.ExecuteQuery("address_Delete", param);
        }

        public void DeleteOnUser(long UserId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            Connection.ExecuteQuery("Address_DeleteOnUser", param);
        }

        public List<CLayer.Address> GetOnUser(long UserId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            DataTable dt = Connection.GetTable("address_GetOnUser", param);
            List<CLayer.Address> result = new List<CLayer.Address>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Address()
                {
                    AddressId = Connection.ToLong(dr["AddressId"]),
                    UserId = Connection.ToLong(dr["UserId"]),
                    CityId = Connection.ToInteger(dr["CityId"]),
                    AddressText = Connection.ToString(dr["Address"]),
                    City = Connection.ToString(dr["City"]),
                    State = Connection.ToInteger(dr["State"]),
                    CountryId = Connection.ToInteger(dr["Country"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    AddressType = Connection.ToInteger(dr["Type"])
                });
            }
            return result;
        }

        public CLayer.Address GetOnUserId(long UserId, int Addresstype)
        {
            CLayer.Address result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Addresstype));
            DataTable dt = Connection.GetTable("address_GetOnUserType", param);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.Address();
                result.AddressId = Connection.ToLong(dt.Rows[0]["AddressId"]);
                result.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                result.CityId = Connection.ToInteger(dt.Rows[0]["CityId"]);
                result.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                result.City = Connection.ToString(dt.Rows[0]["City"]);
                result.State = Connection.ToInteger(dt.Rows[0]["State"]);
                result.CountryId = Connection.ToInteger(dt.Rows[0]["Country"]);
                result.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                result.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                result.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.AddressType = Connection.ToInteger(dt.Rows[0]["Type"]);
           
            }
            return result;
        }

        public CLayer.Address GetPrimaryOnUser(long UserId)
        {
            CLayer.Address address = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            DataTable dt = Connection.GetTable("address_GetPrimaryOnUser", param);
            if (dt.Rows.Count > 0)
            {
                address = new CLayer.Address();
                address.AddressId = Connection.ToLong(dt.Rows[0]["AddressId"]);
                address.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                address.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                address.CityId = Connection.ToInteger(dt.Rows[0]["CityId"]);
                address.City = Connection.ToString(dt.Rows[0]["City"]);
                address.State = Connection.ToInteger(dt.Rows[0]["State"]);
                address.CountryId = Connection.ToInteger(dt.Rows[0]["Country"]);
                address.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                address.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                address.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                address.AddressType = Connection.ToInteger(dt.Rows[0]["Type"]);
            }
            return address;
        }
        public CLayer.Address GetBillingAddress(long UserId)
        {
            CLayer.Address address = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            DataTable dt = Connection.GetTable("address_GetBillingAddress", param);
            if (dt.Rows.Count > 0)
            {
                address = new CLayer.Address();
                address.BillingAddressId = Connection.ToLong(dt.Rows[0]["AddressId"]);
                address.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                address.BillingAddress = Connection.ToString(dt.Rows[0]["Address"]);
                address.BillingCityId = Connection.ToInteger(dt.Rows[0]["CityId"]);
                address.BillingCity = Connection.ToString(dt.Rows[0]["City"]);
                address.BillingState = Connection.ToInteger(dt.Rows[0]["State"]);
                address.BillingCountryId = Connection.ToInteger(dt.Rows[0]["Country"]);
                address.BillingZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                address.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                address.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                address.BillingAddressType = Connection.ToInteger(dt.Rows[0]["Type"]);
            }
            return address;
        }
          public CLayer.Address GetAddressOnUserId(long UserId)
        {
            CLayer.Address address = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            DataTable dt = Connection.GetTable("address_GetonUserIdByType", param);
            if (dt.Rows.Count > 0)
            {
                    address = new CLayer.Address();
                    address.Firstname = Connection.ToString(dt.Rows[0]["Firstname"]);
                    address.Lastname = Connection.ToString(dt.Rows[0]["LastName"]);
                    address.Email = Connection.ToString(dt.Rows[0]["Email"]);
                    address.City = Connection.ToString(dt.Rows[0]["City"]);
                    address.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                    address.Country = Connection.ToString(dt.Rows[0]["Country"]);
                    address.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                    address.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                    address.StateName = Connection.ToString(dt.Rows[0]["State"]);
                    address.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);

      }
            return address;
        }




        public CLayer.Address Get(long AddressId)
        {
            CLayer.Address address = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAddressId", DataPlug.DataType._BigInt, AddressId));
            DataTable dt = Connection.GetTable("address_Get", param);
            if (dt.Rows.Count > 0)
            {
                address = new CLayer.Address();
                address.AddressId = Connection.ToLong(dt.Rows[0]["AddressId"]);
                address.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                address.AddressText = Connection.ToString(dt.Rows[0]["Address"]);
                address.CityId = Connection.ToInteger(dt.Rows[0]["CityId"]);
                address.City = Connection.ToString(dt.Rows[0]["City"]);
                address.State = Connection.ToInteger(dt.Rows[0]["State"]);
                address.CountryId = Connection.ToInteger(dt.Rows[0]["Country"]);
                address.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                address.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                address.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                address.AddressType = Connection.ToInteger(dt.Rows[0]["Type"]);
            }
            return address;
        }

        public long Save(CLayer.Address data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAddressId", DataPlug.DataType._BigInt, data.AddressId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, data.AddressText));
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, data.CityId));
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, data.City));
            param.Add(Connection.GetParameter("pState", DataPlug.DataType._Int, data.State));
            param.Add(Connection.GetParameter("pCountry", DataPlug.DataType._Int, data.CountryId));
            param.Add(Connection.GetParameter("pZipCode", DataPlug.DataType._Varchar, data.ZipCode));
            param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, data.Phone));
            param.Add(Connection.GetParameter("pMobile", DataPlug.DataType._Varchar, data.Mobile));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, data.AddressType));
            object result = Connection.ExecuteQueryScalar("address_Save", param);
            return Connection.ToInteger(result);
        }

        public long BillingAddress(CLayer.Address data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAddressId", DataPlug.DataType._BigInt, data.AddressId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, data.AddressText));
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, data.CityId));
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, data.City));
            param.Add(Connection.GetParameter("pState", DataPlug.DataType._Int, data.State));
            param.Add(Connection.GetParameter("pCountry", DataPlug.DataType._Int, data.CountryId));
            param.Add(Connection.GetParameter("pZipCode", DataPlug.DataType._Varchar, data.ZipCode));
            param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, data.Phone));
            param.Add(Connection.GetParameter("pMobile", DataPlug.DataType._Varchar, data.Mobile));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, data.AddressType));
            object result = Connection.ExecuteQueryScalar("addressbilling_Save", param);
            return Connection.ToInteger(result);
        }
        public int GetStatidOnUserId(long UserId)
        {
            string sql = "select State from address where UserId =" + UserId;
            return Connection.ToInteger(Connection.ExecuteSQLQueryScalar(sql));
        }
    }
}