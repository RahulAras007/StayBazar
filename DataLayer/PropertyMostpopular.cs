using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataLayer
{
    public class PropertyMostpopular : DataLink
    {
        public List<CLayer.Property> MostpopularGet(int PropertyStatus,int UserStstus,int Limit )
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, 1));
            DataTable dt = Connection.GetTable("property_Mostpopular", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                temp.City = Connection.ToString(dr["CityName"]);
                temp.FileName = Connection.ToString(dr["FileName"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> MostpopularForb2bGet(int PropertyStatus, int UserStstus, int Limit)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, 1));
            DataTable dt = Connection.GetTable("propertyforb2b_Mostpopular", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                temp.City = Connection.ToString(dr["CityName"]);
                temp.FileName = Connection.ToString(dr["FileName"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> NewlyCreatedList(int PropertyStatus,int UserStstus,int Limit )
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, 1));
            DataTable dt = Connection.GetTable("property_NewlyCreatedList", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                temp.City = Connection.ToString(dr["CityName"]);
                temp.FileName = Connection.ToString(dr["FileName"]);
                result.Add(temp);
            }
            return result;
        }

        public List<CLayer.Property> SpecialOffers(int PropertyStatus, int UserStstus, int Limit)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, 1));
            DataTable dt = Connection.GetTable("property_SpecialOffers", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                temp.Location = Connection.ToString(dr["Location"]);
                temp.City = Connection.ToString(dr["CityName"]);
                temp.FileName = Connection.ToString(dr["FileName"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> DealsofDay(int PropertyStatus, int UserStstus, int Limit)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int,(int) CLayer.Role.Roles.Customer));
            DataTable dt = Connection.GetTable("property_DealsofDay", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                temp.FileName = Connection.ToString(dr["FileName"]);
                temp.City = Connection.ToString(dr["City"]);
    //            temp.OfferTitle = Connection.ToString(dr["OfferTitle"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> LongStayDels(int PropertyStatus, int UserStstus, int Limit)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, (int) CLayer.Role.Roles.Customer));
            DataTable dt = Connection.GetTable("property_LongStayWidget", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                temp.FileName = Connection.ToString(dr["FileName"]);
                temp.City = Connection.ToString(dr["City"]);
               // temp.OfferTitle = Connection.ToString(dr["OfferTitle"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> InterestedPropertiesGet(long PropertyId,int PropertyStatus, int UserStstus, int Limit)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, 1));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
            DataTable dt = Connection.GetTable("property_InterestedProperties", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                temp.City = Connection.ToString(dr["CityName"]);
                temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                temp.FileName = Connection.ToString(dr["FileName"]);
                result.Add(temp);
            }
            return result;
        }

        public List<CLayer.Property> ProprtyGet(long PropertyId)
        {
           
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
                DataTable dt = Connection.GetTable("review_propertyGet", param);
                List<CLayer.Property> result = new List<CLayer.Property>();
                CLayer.Property temp;
                foreach (DataRow dr in dt.Rows)
                {
                    temp = new CLayer.Property();
                    temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                    temp.Title = Connection.ToString(dr["Title"]);
                    temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                    temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                    temp.City = Connection.ToString(dr["CityName"]);
                    temp.FileName = Connection.ToString(dr["FileName"]);
                    result.Add(temp);
                }         
            return result;
        }
        public List<CLayer.Property> MostpopularGetWithGDS(int PropertyStatus, int UserStstus, int Limit)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, 1));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            DataTable dt = Connection.GetTable("property_MostpopularWithGDS", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                temp.City = Connection.ToString(dr["CityName"]);
                temp.FileName = Connection.ToString(dr["FileName"]);
                temp.InventoryAPIType = Connection.ToInteger(dr["InventoryAPITypeID"]);
                temp.InventoryAPITypeValue = Connection.ToString(dr["InventoryAPIType"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> InterestedPropertiesGetWithGDS(long PropertyId, int PropertyStatus, int UserStstus, int Limit)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._Int, PropertyStatus));
            param.Add(Connection.GetParameter("pStatususer", DataPlug.DataType._Int, UserStstus));
            param.Add(Connection.GetParameter("plimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, 1));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
            DataTable dt = Connection.GetTable("property_InterestedPropertiesWithGDS", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Rating = Connection.ToInteger(dr["Rating"]);
                temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
                temp.City = Connection.ToString(dr["CityName"]);
                temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
                temp.FileName = Connection.ToString(dr["FileName"]);
                temp.InventoryAPIType = Connection.ToInteger(dr["InventoryAPITypeID"]);
                temp.InventoryAPITypeValue = Connection.ToString(dr["InventoryAPIType"]);

                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> PreferedProperties(string Destination,string Userid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pDestination", DataPlug.DataType._Varchar, Destination));
            param.Add(Connection.GetParameter("puserId", DataPlug.DataType._Varchar, Userid));
            DataTable dt = Connection.GetTable("SP_Prefered_properties", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.City = Connection.ToString(dr["City"]);
                temp.InventoryAPIType = Connection.ToInteger(dr["InventoryAPIType"]);
                result.Add(temp);
            }
            return result;
        }
    }
}
