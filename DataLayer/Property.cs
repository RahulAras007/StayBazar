using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web;
namespace DataLayer
{
    public class Property : DataLink
    {
        public long GetSupplierId(long propertyId)
        {
            object obj = Connection.ExecuteSQLQueryScalar("Select ownerId from property where propertyId=" + propertyId);
            return Connection.ToLong(obj);
        }

        public bool HasValidAccountForCustomProperty(long propertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            object value = Connection.ExecuteQueryScalar("CustomProperty_HasValidAccount", param);

            return (Connection.ToInteger(value) == 0);
        }

        public bool HasValidAccountForProperty(long propertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            object value = Connection.ExecuteQueryScalar("Property_HasValidAccount", param);

            return (Connection.ToInteger(value) == 0);
        }

        public List<string> GetPropertyEmailAndId(long supplierId, DateTime forDate)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, supplierId));
            param.Add(Connection.GetParameter("pForDate", DataPlug.DataType._Date, forDate));
            DataTable dt = Connection.GetTable("b2b_GetPropEmailAndIdForEmail", param);
            List<string> result = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(Connection.ToString(dr["Email"]) + "#" + Connection.ToString(dr["PropertyId"]));
            }
            return result;
        }

        public CLayer.Property PropertyForMostPopularGDS(long PropertyId)
        {
            string qry = "SELECT DISTINCT g.propertyid,g.startdate,g.enddate,Hotel_ID AS HotelCode,country,(SELECT NAME AS CountryName FROM Country WHERE CountryID = Country LIMIT 1) AS CountryName, city," +
                          " state,(SELECT NAME FROM state WHERE stateid = pr.state AND countryid = pr.country) AS StateName " +
                        " FROM property pr INNER JOIN recommended g ON pr.propertyid = g.PropertyId  WHERE pr.status = 1 AND InventoryAPITYPE = 2 AND g.propertyid=" + PropertyId + "";
            DataTable dt = Connection.GetSQLTable(qry);
            CLayer.Property cal = null;
            if (dt.Rows.Count > 0)
            {
                cal = new CLayer.Property();
                cal.PropertyId = PropertyId;
                cal.StartDate = Connection.ToDate(dt.Rows[0]["startdate"]);
                cal.EndDate = Connection.ToDate(dt.Rows[0]["enddate"]);
                cal.HotelID = Connection.ToString(dt.Rows[0]["HotelCode"]);
                cal.Country = Connection.ToInteger(dt.Rows[0]["country"]);
                cal.Countryname = Connection.ToString(dt.Rows[0]["CountryName"]);
                cal.City = Connection.ToString(dt.Rows[0]["city"]);
                cal.State = Connection.ToInteger(dt.Rows[0]["state"]);
                cal.Statename = Connection.ToString(dt.Rows[0]["StateName"]);


            }
            return cal;
        }
        public CLayer.Property PropertyForAutoComplete(long PropertyId, DateTime pStart, DateTime pEnd)
        {
            string qry = "SELECT DISTINCT Hotel_ID AS HotelCode,country,(SELECT NAME AS CountryName FROM Country WHERE CountryID = Country LIMIT 1) AS CountryName, city," +
                          " state,(SELECT NAME FROM state WHERE stateid = pr.state AND countryid = pr.country) AS StateName " +
                        " FROM property pr  WHERE pr.status = 1 AND InventoryAPITYPE = 2 AND pr.propertyid=" + PropertyId + "";
            DataTable dt = Connection.GetSQLTable(qry);
            CLayer.Property cal = null;
            if (dt.Rows.Count > 0)
            {
                cal = new CLayer.Property();
                cal.PropertyId = PropertyId;
                cal.StartDate = pStart;
                cal.EndDate = pEnd;
                cal.HotelID = Connection.ToString(dt.Rows[0]["HotelCode"]);
                cal.Country = Connection.ToInteger(dt.Rows[0]["country"]);
                cal.Countryname = Connection.ToString(dt.Rows[0]["CountryName"]);
                cal.City = Connection.ToString(dt.Rows[0]["city"]);
                cal.State = Connection.ToInteger(dt.Rows[0]["state"]);
                cal.Statename = Connection.ToString(dt.Rows[0]["StateName"]);

            }
            return cal;
        }
        public int GetPropertybyEmail(string Email)
        {
            int count = 0;
            string qry = "SELECT COUNT(*) as count FROM  `property` WHERE Email = '" + Email + "' ";
            DataTable dt = Connection.GetSQLTable(qry);
            if (dt.Rows.Count > 0)
            {

                count = Connection.ToInteger(dt.Rows[0]["count"]);

            }
            return count;
        }

        public CLayer.Property PropertyAvgRate(long PropertyId)
        {
            string qry = "SELECT SUM(Rating) AS SM,COUNT(PropertyId) AS CNT FROM review r WHERE r.Status=" + ((int)CLayer.ObjectStatus.StatusType.Verified) + " and r.PropertyId=" + PropertyId;
            DataTable dt = Connection.GetSQLTable(qry);
            CLayer.Property cal = null;
            if (dt.Rows.Count > 0)
            {
                cal = new CLayer.Property();
                cal.PropertyId = PropertyId;
                cal.SumRating = Connection.ToDecimal(dt.Rows[0]["SM"]);
                cal.CountRating = Connection.ToInteger(dt.Rows[0]["CNT"]);
            }
            return cal;
        }
        public void SetCommission(CLayer.RateCommission commission)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("Update property Set B2CMarkupShortTerm=");
            sql.Append(commission.B2CShortTerm);
            sql.Append(",B2CMarkupLongTerm=");
            sql.Append(commission.B2CLongTerm);
            sql.Append(",B2BMarkupShortTerm=");
            sql.Append(commission.B2BShortTerm);
            sql.Append(",B2BMarkupLongTerm=");
            sql.Append(commission.B2BLongTerm);
            sql.Append(" Where PropertyId=");
            sql.Append(commission.PropertyId);
            Connection.ExecuteSqlQuery(sql.ToString());
        }

        public CLayer.Property GetCancellationCharges(long propertyId)
        {
            string sql = "Select CancellationCharge,CancellationPeriod,CancellationType,CanChargeForModification From property Where propertyId=" + propertyId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.Property rc = null;
            if (dt.Rows.Count > 0)
            {
                rc = new CLayer.Property();
                rc.PropertyId = propertyId;
                rc.CancellationCharge = Connection.ToDouble(dt.Rows[0]["CancellationCharge"]);
                rc.CancellationPeriod = Connection.ToInteger(dt.Rows[0]["CancellationPeriod"]);
                rc.CancellationMethod = (CLayer.ObjectStatus.CancellationType)Connection.ToInteger(dt.Rows[0]["CancellationType"]);
                rc.IsChargeAppliesToRefund = Connection.ToBoolean(dt.Rows[0]["CanChargeForModification"]);
            }
            return rc;
        }

        public CLayer.Property GetPartialPayment(long propertyId)
        {
            string sql = "Select B2CPartialPaymentsPercentage,B2BPartialPaymentsPercentage From property Where propertyId=" + propertyId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.Property rc = null;
            if (dt.Rows.Count > 0)
            {
                rc = new CLayer.Property();
                rc.PropertyId = propertyId;
                rc.B2CPartialPaymentsPcte = Connection.ToDecimal(dt.Rows[0]["B2CPartialPaymentsPercentage"]);
                rc.B2BPartialPaymentsPcte = Connection.ToDecimal(dt.Rows[0]["B2BPartialPaymentsPercentage"]);
            }
            return rc;
        }

        public CLayer.RateCommission GetCommission(long propertyId)
        {
            string sql = "Select B2CMarkupShortTerm,B2CMarkupLongTerm,B2BMarkupShortTerm,B2BMarkupLongTerm From property Where propertyId=" + propertyId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.RateCommission rc = null;
            if (dt.Rows.Count > 0)
            {
                rc = new CLayer.RateCommission();
                rc.PropertyId = propertyId;
                rc.B2CShortTerm = Connection.ToDouble(dt.Rows[0]["B2CMarkupShortTerm"]);
                rc.B2CLongTerm = Connection.ToDouble(dt.Rows[0]["B2CMarkupLongTerm"]);
                rc.B2BShortTerm = Connection.ToDouble(dt.Rows[0]["B2BMarkupShortTerm"]);
                rc.B2BLongTerm = Connection.ToDouble(dt.Rows[0]["B2BMarkupLongTerm"]);
            }
            return rc;
        }
        public List<CLayer.Property> SearchProperty(string searchText, bool bySupplier)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSearchText", DataPlug.DataType._Varchar, searchText));
            param.Add(Connection.GetParameter("pBySupplier", DataPlug.DataType._Bool, bySupplier));
            DataTable dt = Connection.GetTable("property_SearchBySupplierNTitle", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.SupplierName = Connection.ToString(dr["Supplier"]);
                result.Add(temp);
            }
            return result;
        }
        public int DeleteGDSPropertyImages(long PropertyID)
        {
            int result = 0;
            try
            {
                string sql = "DELETE  FROM gdspropertyimageurls WHERE PropertyID =" + PropertyID.ToString();
                object obj = Connection.ExecuteSQLQueryScalar(sql);

                result = Connection.ToInteger(obj);
            }
            catch (Exception ex)
            {
                result = -1;
            }
            return result;
        }
        public long GetGDSPropertyImagesCount(long PropertyID)
        {
            long result = 0;
            try
            {
                string sql = "select count(*) FROM gdspropertyimageurls WHERE  PropertyID =" + PropertyID.ToString();

                object obj = Connection.ExecuteSQLQueryScalar(sql);

                result = Connection.ToInteger(obj);
            }
            catch //(Exception ex)
            {
                result = -1;
            }
            return result;
        }

        public List<string> GetGDSHotelAllImages(long propertyid, int limit = 0)
        {
            string sql = "SELECT distinct imageurl FROM gdspropertyimageurls WHERE propertyid=" + propertyid;
            if (limit > 0)
            {
                if (limit > 50) { limit = 50; }
                sql = sql + " LIMIT " + limit.ToString();
            }
            DataTable dt = Connection.GetSQLTable(sql);
            List<string> result = new List<string>();
            string t = "";
            foreach (DataRow item in dt.Rows)
            {
                t = Connection.ToString(item[0]);
                if (t != "") { result.Add(t); }
            }

            return result;
        }

        public List<CLayer.Property> GetAllPropertiesForratesave()
        {
            string sql = "select pr.PropertyId from property pr WHERE pr.status = 1";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllGDSProperties()
        {
            string sql = "SELECT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr WHERE pr.status = 1 AND InventoryAPITYPE=2";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllGDSPropertiesWithOutData()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr  " +
                        " LEFT JOIN gdspropertyimageurls g ON pr.propertyid = g.PropertyId " +
                        " WHERE pr.status = 1 AND InventoryAPITYPE = 2 AND(g.propertyid IS NULL OR pr.description IS NULL OR pr.description = '') ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllTamarindPropertyDescriptionsWithOutData()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr " +
                           " WHERE pr.status = 1 AND InventoryAPITYPE = 4 AND(pr.description IS NULL OR pr.description = '' )  ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllTBOPropertyDescriptionsWithOutData()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr " +
                           " WHERE pr.status = 1 AND InventoryAPITYPE = 5 AND(pr.description IS NULL OR pr.description = '' )  ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllGDSPropertyDescriptionsWithOutData()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr " +
                           " WHERE pr.status = 1 AND InventoryAPITYPE = 2 AND(pr.description IS NULL OR pr.description = '' )  ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllGDSFormattedPropertyDescriptionsWithOutData()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr " +
                           " WHERE pr.status = 1 AND InventoryAPITYPE = 2 AND(pr.Formatteddescription IS NULL OR pr.Formatteddescription = '')  ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllGDSPropertyTitlesWithOutData()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr " +
                           " WHERE pr.status = 1 AND InventoryAPITYPE = 2 AND(pr.Title IS NULL OR pr.Title = '')  ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllTamarindPropertyTitlesWithOutData()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr " +
                           " WHERE pr.status = 1 AND InventoryAPITYPE = 2 AND(pr.Title IS NULL OR pr.Title = '')  ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> GetAllTBOPropertyTitlesWithOutData()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode FROM property pr " +
                           " WHERE pr.status = 1 AND InventoryAPITYPE = 2 AND(pr.Title IS NULL OR pr.Title = '')  ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                result.Add(temp);
            }
            return result;
        }
        public bool IsOwnerProperty(long propertyId, long ownerId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pOwnerId", DataPlug.DataType._BigInt, ownerId));
            object obj = Connection.ExecuteQueryScalar("property_IsOwnedBy", param);
            return Connection.ToInteger(obj) > 0;
        }
        public List<CLayer.Property> GetAll(long ownerId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOwnerId", DataPlug.DataType._BigInt, ownerId));
            DataTable dt = Connection.GetTable("property_GetAll", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Property()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Description = Connection.ToString(dr["Description"]),
                    Location = Connection.ToString(dr["Location"]),
                    Status = (CLayer.ObjectStatus.StatusType)Connection.ToInteger(dr["Status"]),
                    OwnerId = Connection.ToLong(dr["OwnerId"]),
                    PositionLat = Connection.ToString(dr["PositionLat"]),
                    PositionLng = Connection.ToString(dr["PositionLng"]),
                    Address = Connection.ToString(dr["Address"]),
                    Country = Connection.ToInteger(dr["Country"]),
                    State = Connection.ToInteger(dr["State"]),
                    City = Connection.ToString(dr["City"]),
                    Rating = Connection.ToInteger(dr["Rating"]),
                    IsManualReview = Connection.ToBoolean(dr["IsManualReview"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"])
                });
            }
            return result;
        }
        public List<CLayer.Property> propertydisable_GetAll()
        {
            DataTable dt = Connection.GetTable("propertydisable_GetAll");
            List<CLayer.Property> result = new List<CLayer.Property>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Property()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Description = Connection.ToString(dr["Description"]),
                    Location = Connection.ToString(dr["Location"]),
                    Status = (CLayer.ObjectStatus.StatusType)Connection.ToInteger(dr["Status"]),
                    OwnerId = Connection.ToLong(dr["OwnerId"]),
                    PositionLat = Connection.ToString(dr["PositionLat"]),
                    PositionLng = Connection.ToString(dr["PositionLng"]),
                    Address = Connection.ToString(dr["Address"]),
                    Country = Connection.ToInteger(dr["Country"]),
                    State = Connection.ToInteger(dr["State"]),
                    City = Connection.ToString(dr["City"]),
                    Rating = Connection.ToInteger(dr["Rating"]),
                    IsManualReview = Connection.ToBoolean(dr["IsManualReview"]),
                    NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]),
                    BusinessName = Connection.ToString(dr["BusinessName"]),
                    SupplierName = Connection.ToString(dr["SupplierName"]),
                });
            }
            return result;
        }

        public List<CLayer.Property> GetAllPropertiesForsitemap()
        {
            string sql = "select pr.PropertyId,pr.Title,pr.city,pr.location from property pr WHERE pr.status =1";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.City = Connection.ToString(dr["city"]);
                temp.Location = Connection.ToString(dr["location"]);
                result.Add(temp);
            }
            return result;
        }
        public long Save(CLayer.Property property)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, property.PropertyId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, property.Title));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Text, property.Description));
            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, property.Location));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)property.Status));
            param.Add(Connection.GetParameter("pPositionLat", DataPlug.DataType._Decimal, property.PositionLat));
            param.Add(Connection.GetParameter("pPositionLng", DataPlug.DataType._Decimal, property.PositionLng));
            param.Add(Connection.GetParameter("pOwnerId", DataPlug.DataType._BigInt, property.OwnerId));
            param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, property.Address));
            param.Add(Connection.GetParameter("pCountry", DataPlug.DataType._Int, property.Country));
            param.Add(Connection.GetParameter("pState", DataPlug.DataType._Int, property.State));
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, property.CityId));
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, property.City));
            param.Add(Connection.GetParameter("pZipCode", DataPlug.DataType._Varchar, property.ZipCode));
            param.Add(Connection.GetParameter("pRating", DataPlug.DataType._Int, property.Rating));//rating
            param.Add(Connection.GetParameter("pIsManualReview", DataPlug.DataType._Bool, property.IsManualReview));//bool
            param.Add(Connection.GetParameter("pB2CMarkupLongTerm", DataPlug.DataType._Decimal, property.B2CMarkupLongTerm));
            param.Add(Connection.GetParameter("pB2CMarkupShortTerm", DataPlug.DataType._Decimal, property.B2CMarkupShortTerm));
            param.Add(Connection.GetParameter("pB2BMarkupShortTerm", DataPlug.DataType._Decimal, property.B2BMarkupShortTerm));
            param.Add(Connection.GetParameter("pB2BMarkupLongTerm", DataPlug.DataType._Decimal, property.B2BMarkupLongTerm));
            param.Add(Connection.GetParameter("pB2BStdShortTermDis", DataPlug.DataType._Decimal, property.B2BStdShortTermDis));
            param.Add(Connection.GetParameter("pB2BStdLongTermDis", DataPlug.DataType._Decimal, property.B2BStdLongTermDis));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, property.Email));
            param.Add(Connection.GetParameter("pPhoneNo", DataPlug.DataType._Varchar, property.Phone));
            param.Add(Connection.GetParameter("pMobileNo", DataPlug.DataType._Varchar, property.Mobile));
            param.Add(Connection.GetParameter("pAgeLimit", DataPlug.DataType._BigInt, property.AgeLimit));
            param.Add(Connection.GetParameter("pPageTitle", DataPlug.DataType._Varchar, property.PageTitle));
            param.Add(Connection.GetParameter("pMetaDesption", DataPlug.DataType._Varchar, property.MetaDescription));
            param.Add(Connection.GetParameter("pCheckInTime", DataPlug.DataType._Varchar, property.CheckInTime));
            param.Add(Connection.GetParameter("pCheckOutTime", DataPlug.DataType._Varchar, property.CheckOutTime));
            param.Add(Connection.GetParameter("pPropertyFor", DataPlug.DataType._Int, property.PropertyFor));
            param.Add(Connection.GetParameter("pPropertyInventoryType", DataPlug.DataType._Int, property.PropertyInventoryType));
            param.Add(Connection.GetParameter("pGSTRegistrationNo", DataPlug.DataType._Varchar, property.GSTRegistrationNo));


            object result = Connection.ExecuteQueryScalar("property_Save", param);

            param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, result));
            param.Add(Connection.GetParameter("pDistanceFromCity", DataPlug.DataType._Decimal, property.DistanceFromCity));
            Connection.ExecuteQueryScalar("property_distancefromcity_save", param);

            return Connection.ToLong(result);

        }

        public long SaveAmadeus_Property(CLayer.Property property)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            DataPlug.Parameter pPropertyId = Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, 0);
            pPropertyId.Direction = ParameterDirection.InputOutput;
            param.Add(pPropertyId);
            //      param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, property.PropertyId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, property.Title));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Text, property.Description));
            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, property.Location));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)property.Status));
            param.Add(Connection.GetParameter("pOwnerId", DataPlug.DataType._BigInt, property.OwnerId));
            param.Add(Connection.GetParameter("pPositionLat", DataPlug.DataType._Decimal, property.PositionLat));
            param.Add(Connection.GetParameter("pPositionLng", DataPlug.DataType._Decimal, property.PositionLng));

            param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, property.Address));
            param.Add(Connection.GetParameter("pCountryId", DataPlug.DataType._Int, property.Country));
            param.Add(Connection.GetParameter("pState", DataPlug.DataType._Int, property.State));
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, property.City));
            param.Add(Connection.GetParameter("pCityID", DataPlug.DataType._Int, property.CityId));

            param.Add(Connection.GetParameter("pZipCode", DataPlug.DataType._Varchar, property.ZipCode));
            param.Add(Connection.GetParameter("pInventoryAPIType", DataPlug.DataType._Int, property.PropertyInventoryType));
            param.Add(Connection.GetParameter("pHotelId", DataPlug.DataType._Varchar, property.HotelID));

            param.Add(Connection.GetParameter("pTBOHotelId", DataPlug.DataType._Varchar, property.TBOHotelId));
            param.Add(Connection.GetParameter("pTBOFlag", DataPlug.DataType._Char, property.TBOFlag));
            param.Add(Connection.GetParameter("pTamarindHotelId", DataPlug.DataType._Varchar, property.TamarindHotelId));
            param.Add(Connection.GetParameter("pTamarindFlag", DataPlug.DataType._Char, property.TamarindFlag));
            param.Add(Connection.GetParameter("pRateCardDetailedId", DataPlug.DataType._Varchar, property.RateCardDetailedId));
            param.Add(Connection.GetParameter("pTaxPercentage", DataPlug.DataType._Decimal, property.TaxPercentage));
            param.Add(Connection.GetParameter("pGSTFlag", DataPlug.DataType._Int, property.GSTSlab));
            object result = Connection.ExecuteQueryScalar("property_Amadeus_Save", param);


            //     Connection.ExecuteQueryScalar("property_distancefromcity_save", param);

            return Connection.ToLong(result);

        }
        public int SetStatus(long propertyId, CLayer.ObjectStatus.StatusType status)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)status));
            object result = Connection.ExecuteQueryScalar("property_SetStatus", param);
            return Connection.ToInteger(result);
        }
        public int SetPosition(long propertyId, string positionLat, string positionLng)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pPositionLat", DataPlug.DataType._Decimal, positionLat));
            param.Add(Connection.GetParameter("pPositionLng", DataPlug.DataType._Decimal, positionLng));
            object result = Connection.ExecuteQueryScalar("property_SetPosition", param);
            return Connection.ToInteger(result);
        }


        public int SetHotelIdAPI(long propertyId, string HotelId, int InventoryAPIType)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pHotelId", DataPlug.DataType._Varchar, HotelId));
            param.Add(Connection.GetParameter("pInventoryAPIType", DataPlug.DataType._Int, InventoryAPIType));
            object result = Connection.ExecuteQueryScalar("property_SetHotelIdAPIType", param);
            return Connection.ToInteger(result);
        }
        public void Delete(long propertyid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            Connection.ExecuteQuery("property_Delete", param);
            return;
        }
        public bool CanDelete(long propertyid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            object obj = Connection.ExecuteQueryScalar("property_CanDelete", param);
            return (Connection.ToInteger(obj) == 0);
        }

        public string GetHotelId(long propertyid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            object obj = Connection.ExecuteQueryScalar("property_GetHotelId", param);
            return Connection.ToString(obj);
        }

        public string GetRateCardDetailId(long propertyid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            object obj = Connection.ExecuteQueryScalar("Sp_GetPropertyRateCardDetail", param);
            return Connection.ToString(obj);
        }
        public string GetTamrindCityID(long propertyid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            object obj = Connection.ExecuteQueryScalar("Sp_GetTamrindCityId", param);
            return Connection.ToString(obj);
        }
        public int GetInventoryAPITypeId(long propertyid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            object obj = Connection.ExecuteQueryScalar("property_GetInventoryAPITypeId", param);
            return Connection.ToInteger(obj);
        }
        public int GetInventoryAPITypeIdd(long propertyid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            object obj = Connection.ExecuteQueryScalar("property_GetInventoryAPITypeIdAddedByRahul", param);
            return Connection.ToInteger(obj);
        }
        public string GetGDSHotelImage(long propertyid)
        {
            string sql = "SELECT imageurl FROM gdspropertyimageurls WHERE propertyid=" + propertyid + " ORDER BY createddate DESC LIMIT 1";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            var s = Connection.ToString(obj);

            return s;
        }
        public CLayer.SearchResult GetGDSImageandDesctiption(long PropertyID)
        {
            string sql = "SELECT p.propertyid,s.ImageUrl,p.Description FROM gdspropertyimageurls s INNER JOIN Property p ON s.propertyid=p.propertyid  WHERE p.propertyid = " + PropertyID + " ORDER BY s.createddate DESC LIMIT 1";


            DataTable dt = Connection.GetSQLTable(sql);

            CLayer.SearchResult result = new CLayer.SearchResult();
            foreach (DataRow dr in dt.Rows)
            {
                result = new CLayer.SearchResult();
                result.PropertyId = Connection.ToLong(dr["propertyid"]);
                result.Description = Connection.ToString(dr["Description"]);
                if (result.Description.Length > 130)
                {
                    result.Description = result.Description.Substring(0, 129);
                }
                result.ImageFile = Connection.ToString(dr["ImageUrl"]);

            }
            return result;
        }
        public int GetPropertyInventorytype(long PropertyId)
        {
            string sql = "Select PropertyInventoryType From Property Where PropertyId='" + PropertyId + "'";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            var s = Connection.ToInteger(obj);
            return Connection.ToInteger(obj);
        }
        public CLayer.Property GetPosition(long propertyid)
        {
            CLayer.Property property = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            DataTable dt = Connection.GetTable("property_GetPosition", param);
            if (dt.Rows.Count > 0)
            {
                property = new CLayer.Property();
                property.PropertyId = propertyid;
                property.PositionLat = Connection.ToString(dt.Rows[0]["PositionLat"]);
                property.PositionLng = Connection.ToString(dt.Rows[0]["PositionLng"]);
            }
            return property;
        }
        public CLayer.Property Get(long propertyid)
        {
            CLayer.Property property = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            DataTable dt = Connection.GetTable("property_Get", param);
            if (dt.Rows.Count > 0)
            {
                property = new CLayer.Property();
                property.PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]);
                property.Title = Connection.ToString(dt.Rows[0]["Title"]);
                property.Description = Connection.ToString(dt.Rows[0]["Description"]);
                property.Location = Connection.ToString(dt.Rows[0]["Location"]);
                property.Status = (CLayer.ObjectStatus.StatusType)Connection.ToInteger(dt.Rows[0]["Status"]);
                property.OwnerId = Connection.ToLong(dt.Rows[0]["OwnerId"]);
                property.PositionLat = Connection.ToString(dt.Rows[0]["PositionLat"]);
                property.PositionLng = Connection.ToString(dt.Rows[0]["PositionLng"]);
                property.Address = Connection.ToString(dt.Rows[0]["Address"]);
                property.Country = Connection.ToInteger(dt.Rows[0]["Country"]);
                //property.CountryId = Connection.ToInteger(dt.Rows[0]["Country"]);
                property.State = Connection.ToInteger(dt.Rows[0]["State"]);
                property.City = Connection.ToString(dt.Rows[0]["cityname"]);
                property.CityId = Connection.ToInteger(dt.Rows[0]["CityId"]);
                property.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                property.Rating = Connection.ToInteger(dt.Rows[0]["Rating"]);
                property.IsManualReview = Connection.ToBoolean(dt.Rows[0]["IsManualReview"]);
                property.Email = Connection.ToString(dt.Rows[0]["Email"]);
                property.DistanceFromCity = Connection.ToDecimal(dt.Rows[0]["DistanceFromCity"]);
                property.Statename = Connection.ToString(dt.Rows[0]["statename"]);
                property.Countryname = Connection.ToString(dt.Rows[0]["countryname"]);
                property.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                property.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                property.CheckInTime = Connection.ToString(dt.Rows[0]["CheckInTime"]);
                property.CheckOutTime = Connection.ToString(dt.Rows[0]["CheckOutTime"]);
                property.PageTitle = Connection.ToString(dt.Rows[0]["PageTitle"]);
                property.MetaDescription = Connection.ToString(dt.Rows[0]["MetaDescription"]);
                property.AgeLimit = Connection.ToInteger(dt.Rows[0]["ChildAgeLimit"]);
                property.PropertyFor = Connection.ToInteger(dt.Rows[0]["PropertyFor"]);
                property.PropertyInventoryType = Connection.ToInteger(dt.Rows[0]["PropertyInventoryType"]);
                property.GSTRegistrationNo = Connection.ToString(dt.Rows[0]["GSTRegistrationNo"]);


            }
            return property;
        }
        public CLayer.Property GetCheckTime(long propertyid)
        {
            CLayer.Property property = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            DataTable dt = Connection.GetTable("property_GetCheckTime", param);
            if (dt.Rows.Count > 0)
            {
                property = new CLayer.Property();
                property.CheckInTime = Connection.ToString(dt.Rows[0]["CheckInTime"]);
                property.CheckOutTime = Connection.ToString(dt.Rows[0]["CheckOutTime"]);
                property.AgeLimit = Connection.ToInteger(dt.Rows[0]["ChildAgeLimit"]);
                property.MetaDescription = Connection.ToString(dt.Rows[0]["MetaDescription"]);
                property.PageTitle = Connection.ToString(dt.Rows[0]["PageTitle"]);
            }
            return property;
        }
        public CLayer.ObjectStatus.StatusType GetStatus(long propertyid)
        {
            CLayer.ObjectStatus.StatusType status = CLayer.ObjectStatus.StatusType.Draft;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            DataTable dt = Connection.GetTable("property_GetStatus", param);
            if (dt.Rows.Count > 0)
            {

                status = (CLayer.ObjectStatus.StatusType)Connection.ToInteger(dt.Rows[0]["Status"]);

            }
            return status;
        }

        public int SavePartialPay(CLayer.Property data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2CPartialPay", DataPlug.DataType._Decimal, data.B2CPartialPaymentsPcte));
            param.Add(Connection.GetParameter("pB2bPartialPay", DataPlug.DataType._Decimal, data.B2BPartialPaymentsPcte));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._Int, data.PropertyId));
            object result = Connection.ExecuteQueryScalar("partialpayment_Save", param);
            return Connection.ToInteger(result);

        }
        /*  public List<CLayer.SearchResult> Search(out int totalCount,CLayer.SearchCriteria data)
          {
              List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
              DataPlug.Parameter totalRows = Connection.GetParameter("pTotalRows", DataPlug.DataType._Int,0,0,ParameterDirection.InputOutput);
              param.Add(Connection.GetParameter("pDestination", DataPlug.DataType._Varchar, data.Destination));
              param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, data.CheckIn));
              param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, data.CheckOut));
              param.Add(Connection.GetParameter("pAdults", DataPlug.DataType._Int, data.Adults));
              param.Add(Connection.GetParameter("pChildren", DataPlug.DataType._Int, data.Children));
              param.Add(Connection.GetParameter("pStayType", DataPlug.DataType._Int, data.StayType));
              param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Int, data.Bedrooms));
              param.Add(Connection.GetParameter("pStartingRow", DataPlug.DataType._Int, data.StaringRow));
              param.Add(Connection.GetParameter("pRowCount", DataPlug.DataType._Int, data.NoOfRows));
              param.Add(Connection.GetParameter("pRateType", DataPlug.DataType._Int, (int) data.UserType));
              param.Add(Connection.GetParameter("pDeaultType", DataPlug.DataType._Int, (int) data.DefaultUserType));
              param.Add(totalRows);
             // param.Add(Connection.GetParameter("pStayGroup", DataPlug.DataType._Varchar, stayTypeGroup));
              DataTable dt = Connection.GetTable("property_Search", param);
              List<CLayer.SearchResult> results = new List<CLayer.SearchResult>();
              CLayer.SearchResult temp;
          //    int total = 0;
              foreach(DataRow dr in dt.Rows)
              {
                  temp = new CLayer.SearchResult();
                 // temp.Destination = Connection.ToString(dr["Destination"]);
                 // if (total == 0) total = Connection.ToInteger(dr["TotalCount"]);
                  temp.Location = Connection.ToString(dr["Location"]);
                  temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                  temp.Amount = Connection.ToDecimal(dr["Amount"]);
                  temp.StarRate = Connection.ToInteger(dr["Rating"]);
                  temp.Title = Connection.ToString(dr["Title"]);
                  temp.Description = Connection.ToString(dr["Description"]);
                  temp.ImageFile = Connection.ToString(dr["ImageFile"]);
                  temp.Longitude = Connection.ToString(dr["PositionLng"]);
                  temp.Lattitude = Connection.ToString(dr["PositionLat"]);
                  results.Add(temp);
              }
              totalCount = Connection.ToInteger(totalRows.Value); // Connection.ToInteger(totalRows.Value);
              return results;
          }*/
        public List<CLayer.SearchResult> SearchWithFilter(out int totalCount, CLayer.SearchCriteria criteria)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            // DataPlug.Parameter totalRows = Connection.GetParameter("pTotalRows", DataPlug.DataType._Int,0);
            // totalRows.Direction = ParameterDirection.Output;
            // length cutting
            const int LOCATION_LENGTH = 1000;

            if (criteria.Location != null && criteria.Location != "")
            {
                try
                {
                    string strloc = criteria.Location;
                    string[] locs = strloc.Split(new char[] { ',' });
                    strloc = "";
                    string tstr;
                    foreach (string s in locs)
                    {
                        if (s.Trim() != "")
                        {
                            tstr = " p.Location LIKE '%" + s + "%' ";
                            if (strloc != "") tstr = " OR " + tstr;
                            tstr = strloc + tstr;
                            if (tstr.Length >= LOCATION_LENGTH) break;
                            strloc = tstr;
                        }
                    }
                    if (locs.Length > 1)
                    {
                        strloc = " AND (" + strloc + ") ";
                    }
                    else
                        strloc = " AND " + strloc;
                    criteria.Location = strloc;
                }
                catch { criteria.Location = ""; }
            }
            if (criteria.Destination != null && criteria.Destination.Length > 100) criteria.Destination = criteria.Destination.Substring(0, 100);
            if (criteria.StayTypeGroup != null && criteria.StayTypeGroup.Length > 500) criteria.StayTypeGroup = criteria.StayTypeGroup.Substring(0, 500);
            if (criteria.Features != null && criteria.Features.Length > 200) criteria.Features = criteria.Features.Substring(0, 200);
            //  if (criteria.Location != null && criteria.Location.Length > 100) criteria.Location = criteria.Location.Substring(0, 1000);
            //
            if (criteria.SortOrder == 0) { criteria.SortOrder = CLayer.SearchCriteria.SortBy.PriceAsc; }
            DataPlug.Parameter total = Connection.GetParameter("pTotalRows", DataPlug.DataType._Int, 0);
            total.Direction = ParameterDirection.InputOutput;
            param.Add(total);
            param.Add(Connection.GetParameter("pDestination", DataPlug.DataType._Varchar, criteria.Destination));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, criteria.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, criteria.CheckOut));
            param.Add(Connection.GetParameter("pAdults", DataPlug.DataType._Int, criteria.Adults));
            param.Add(Connection.GetParameter("pChildren", DataPlug.DataType._Int, criteria.Children));
            param.Add(Connection.GetParameter("pStayType", DataPlug.DataType._Int, criteria.StayType));
            param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Int, criteria.Bedrooms));
            //   param.Add(totalRows);
            param.Add(Connection.GetParameter("pStayGroup", DataPlug.DataType._Varchar, criteria.StayTypeGroup));
            param.Add(Connection.GetParameter("pStartingRow", DataPlug.DataType._Int, criteria.StaringRow));
            param.Add(Connection.GetParameter("pRowCount", DataPlug.DataType._Int, criteria.NoOfRows));

            param.Add(Connection.GetParameter("pRangeBudgetMax", DataPlug.DataType._Int, criteria.RangeBudgetMax));
            param.Add(Connection.GetParameter("pRangeBudgetMin", DataPlug.DataType._Int, criteria.RangeBudgetMin));
            param.Add(Connection.GetParameter("pStarRatingRange", DataPlug.DataType._Int, criteria.StarRatingRange));
            param.Add(Connection.GetParameter("pFeatures", DataPlug.DataType._Varchar, criteria.Features));
            //param.Add(Connection.GetParameter("pBeds", DataPlug.DataType._Int, criteria.Beds));
            param.Add(Connection.GetParameter("pDistance", DataPlug.DataType._Int, criteria.DistanceInKm));
            param.Add(Connection.GetParameter("pRateType", DataPlug.DataType._Int, (int)criteria.UserType));
            param.Add(Connection.GetParameter("pDefaultRateType", DataPlug.DataType._Int, (int)criteria.DefaultUserType));
            param.Add(Connection.GetParameter("pOrderBy", DataPlug.DataType._Int, (int)criteria.SortOrder));
            param.Add(Connection.GetParameter("pBookingDayCount", DataPlug.DataType._Int, (criteria.CheckOut - criteria.CheckIn).TotalDays));
            param.Add(Connection.GetParameter("pLattitude", DataPlug.DataType._Decimal, criteria.Lattitude));
            param.Add(Connection.GetParameter("pLongitude", DataPlug.DataType._Decimal, criteria.Longitude));
            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, criteria.Location));
            param.Add(Connection.GetParameter("pCorpId", DataPlug.DataType._BigInt, criteria.LoggedInUser));
            DataTable dt;
            try
            {
                dt = Connection.GetTable("property_SearchFilter", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<CLayer.SearchResult> results = new List<CLayer.SearchResult>();
            CLayer.SearchResult temp;
            string ImageRootPath = AppDomain.CurrentDomain.BaseDirectory + "/Files/Property/";

            //  totalCount = -1;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.SearchResult();
                // temp.Destination = Connection.ToString(dr["Destination"]);
                //     if (totalCount == -1) totalCount = Connection.ToInteger(dr["totalrows"]);
                temp.Location = Connection.ToString(dr["Location"]);
                temp.City = Connection.ToString(dr["CityName"]);
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Amount = Connection.ToDecimal(dr["Amount"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Description = Connection.ToString(dr["Description"]);
                temp.ImageFile = Connection.ToString(dr["ImageFile"]);


                string path = ImageRootPath + temp.PropertyId + "/" + temp.ImageFile;

                temp.IsImageExists = (System.IO.File.Exists(path) == true) ? true : false;
                temp.Longitude = Connection.ToString(dr["PositionLng"]);
                temp.Lattitude = Connection.ToString(dr["PositionLat"]);
                temp.StarRate = Connection.ToInteger(dr["Rating"]);
                temp.Distance = (int)(Connection.ToDouble(dr["distance"]) * 100);
                temp.IsDistanceFromCity = (Connection.ToInteger(dr["DistanceType"]) > 0);
                temp.InventoryAPIType = Connection.ToInteger(dr["InventoryAPIType"]);
                temp.APIType = GetAPIType(temp.InventoryAPIType);
                temp.LocationName = GetLocationName(Connection.ToString(dr["Address"]));

                results.Add(temp);
            }
            totalCount = Connection.ToInteger(total.Value);
            return results;
        }

        public string GetAPIType(int pValue)
        {
            string result = string.Empty;
            switch (pValue)
            {
                case 1:
                    result = "maxmojo";
                    break;
                case 2:
                    result = "amadeus";
                    break;
                default:
                    result = "sb";
                    break;
            }
            return result;
        }
        public string GetLocationName(string pValue)
        {
            string result = string.Empty;
            string[] resultList = pValue.Split(',');
            try
            {
                if (resultList.Length > 0)
                {
                    result = resultList[resultList.Length - 1];
                }
                else
                {
                    result = pValue;
                }

            }
            catch (Exception ex)
            {

            }
            return result.Trim();
        }
        public List<CLayer.SearchResult> SearchWithFilter_Amadeus1(out int totalCount, CLayer.SearchCriteria criteria)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            // DataPlug.Parameter totalRows = Connection.GetParameter("pTotalRows", DataPlug.DataType._Int,0);
            // totalRows.Direction = ParameterDirection.Output;
            // length cutting
            const int LOCATION_LENGTH = 1000;

            if (criteria.Location != null && criteria.Location != "")
            {
                try
                {
                    string strloc = criteria.Location;
                    string[] locs = strloc.Split(new char[] { ',' });
                    strloc = "";
                    string tstr;
                    foreach (string s in locs)
                    {
                        if (s.Trim() != "")
                        {
                            tstr = " p.Location LIKE '%" + s + "%' ";
                            if (strloc != "") tstr = " OR " + tstr;
                            tstr = strloc + tstr;
                            if (tstr.Length >= LOCATION_LENGTH) break;
                            strloc = tstr;
                        }
                    }
                    if (locs.Length > 1)
                    {
                        strloc = " AND (" + strloc + ") ";
                    }
                    else
                        strloc = " AND " + strloc;
                    criteria.Location = strloc;
                }
                catch { criteria.Location = ""; }
            }
            if (criteria.Destination != null && criteria.Destination.Length > 100) criteria.Destination = criteria.Destination.Substring(0, 100);
            if (criteria.StayTypeGroup != null && criteria.StayTypeGroup.Length > 500) criteria.StayTypeGroup = criteria.StayTypeGroup.Substring(0, 500);
            if (criteria.Features != null && criteria.Features.Length > 200) criteria.Features = criteria.Features.Substring(0, 200);
            //  if (criteria.Location != null && criteria.Location.Length > 100) criteria.Location = criteria.Location.Substring(0, 1000);
            //
            if (criteria.SortOrder == 0) { criteria.SortOrder = CLayer.SearchCriteria.SortBy.PriceAsc; }
            DataPlug.Parameter total = Connection.GetParameter("pTotalRows", DataPlug.DataType._Int, 0);
            total.Direction = ParameterDirection.InputOutput;
            param.Add(total);
            param.Add(Connection.GetParameter("pDestination", DataPlug.DataType._Varchar, criteria.Destination));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, criteria.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, criteria.CheckOut));
            param.Add(Connection.GetParameter("pAdults", DataPlug.DataType._Int, criteria.Adults));
            param.Add(Connection.GetParameter("pChildren", DataPlug.DataType._Int, criteria.Children));
            param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Int, criteria.Bedrooms));
            param.Add(Connection.GetParameter("pRateType", DataPlug.DataType._Int, (int)criteria.UserType));
            param.Add(Connection.GetParameter("pOrderBy", DataPlug.DataType._Int, (int)criteria.SortOrder));
            param.Add(Connection.GetParameter("pStartingRow", DataPlug.DataType._Int, criteria.StaringRow));
            param.Add(Connection.GetParameter("pRowCount", DataPlug.DataType._Int, criteria.NoOfRows));
            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, criteria.Location));
            DataTable dt;
            try
            {
                dt = Connection.GetTable("property_SearchFilter_Amadeus1", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<CLayer.SearchResult> results = new List<CLayer.SearchResult>();
            CLayer.SearchResult temp;
            //  totalCount = -1;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.SearchResult();
                // temp.Destination = Connection.ToString(dr["Destination"]);
                //     if (totalCount == -1) totalCount = Connection.ToInteger(dr["totalrows"]);
                temp.Location = Connection.ToString(dr["Location"]);
                temp.City = Connection.ToString(dr["CityName"]);
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Amount = Connection.ToDecimal(dr["Amount"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Description = Connection.ToString(dr["Description"]);
                temp.ImageFile = Connection.ToString(dr["ImageFile"]);
                temp.Longitude = Connection.ToString(dr["PositionLng"]);
                temp.Lattitude = Connection.ToString(dr["PositionLat"]);
                temp.StarRate = Connection.ToInteger(dr["Rating"]);
                temp.Distance = (int)(Connection.ToDouble(dr["distance"]) * 100);
                temp.IsDistanceFromCity = (Connection.ToInteger(dr["DistanceType"]) > 0);

                results.Add(temp);
            }
            totalCount = Connection.ToInteger(total.Value);
            return results;
        }

        public List<CLayer.SearchResult> SearchWithFilter_Amadeus(out int totalCount, CLayer.SearchCriteria criteria)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            // DataPlug.Parameter totalRows = Connection.GetParameter("pTotalRows", DataPlug.DataType._Int,0);
            // totalRows.Direction = ParameterDirection.Output;
            // length cutting
            const int LOCATION_LENGTH = 1000;

            if (criteria.Location != null && criteria.Location != "")
            {
                try
                {
                    string strloc = criteria.Location;
                    string[] locs = strloc.Split(new char[] { ',' });
                    strloc = "";
                    string tstr;
                    foreach (string s in locs)
                    {
                        if (s.Trim() != "")
                        {
                            tstr = " p.Location LIKE '%" + s + "%' ";
                            if (strloc != "") tstr = " OR " + tstr;
                            tstr = strloc + tstr;
                            if (tstr.Length >= LOCATION_LENGTH) break;
                            strloc = tstr;
                        }
                    }
                    if (locs.Length > 1)
                    {
                        strloc = " AND (" + strloc + ") ";
                    }
                    else
                        strloc = " AND " + strloc;
                    criteria.Location = strloc;
                }
                catch { criteria.Location = ""; }
            }
            if (criteria.Destination != null && criteria.Destination.Length > 100) criteria.Destination = criteria.Destination.Substring(0, 100);
            if (criteria.StayTypeGroup != null && criteria.StayTypeGroup.Length > 500) criteria.StayTypeGroup = criteria.StayTypeGroup.Substring(0, 500);
            if (criteria.Features != null && criteria.Features.Length > 200) criteria.Features = criteria.Features.Substring(0, 200);
            //  if (criteria.Location != null && criteria.Location.Length > 100) criteria.Location = criteria.Location.Substring(0, 1000);
            //
            if (criteria.SortOrder == 0) { criteria.SortOrder = CLayer.SearchCriteria.SortBy.PriceAsc; }
            DataPlug.Parameter total = Connection.GetParameter("pTotalRows", DataPlug.DataType._Int, 0);
            total.Direction = ParameterDirection.InputOutput;
            param.Add(total);
            param.Add(Connection.GetParameter("pDestination", DataPlug.DataType._Varchar, criteria.Destination));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, criteria.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, criteria.CheckOut));
            param.Add(Connection.GetParameter("pAdults", DataPlug.DataType._Int, criteria.Adults));
            param.Add(Connection.GetParameter("pChildren", DataPlug.DataType._Int, criteria.Children));
            param.Add(Connection.GetParameter("pStayType", DataPlug.DataType._Int, criteria.StayType));
            param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Int, criteria.Bedrooms));
            //   param.Add(totalRows);
            param.Add(Connection.GetParameter("pStayGroup", DataPlug.DataType._Varchar, criteria.StayTypeGroup));
            param.Add(Connection.GetParameter("pStartingRow", DataPlug.DataType._Int, criteria.StaringRow));
            param.Add(Connection.GetParameter("pRowCount", DataPlug.DataType._Int, criteria.NoOfRows));

            param.Add(Connection.GetParameter("pRangeBudgetMax", DataPlug.DataType._Int, criteria.RangeBudgetMax));
            param.Add(Connection.GetParameter("pRangeBudgetMin", DataPlug.DataType._Int, criteria.RangeBudgetMin));
            param.Add(Connection.GetParameter("pStarRatingRange", DataPlug.DataType._Int, criteria.StarRatingRange));
            param.Add(Connection.GetParameter("pFeatures", DataPlug.DataType._Varchar, criteria.Features));
            //param.Add(Connection.GetParameter("pBeds", DataPlug.DataType._Int, criteria.Beds));
            param.Add(Connection.GetParameter("pDistance", DataPlug.DataType._Int, criteria.DistanceInKm));
            param.Add(Connection.GetParameter("pRateType", DataPlug.DataType._Int, (int)criteria.UserType));
            param.Add(Connection.GetParameter("pDefaultRateType", DataPlug.DataType._Int, (int)criteria.DefaultUserType));
            param.Add(Connection.GetParameter("pOrderBy", DataPlug.DataType._Int, (int)criteria.SortOrder));
            param.Add(Connection.GetParameter("pBookingDayCount", DataPlug.DataType._Int, (criteria.CheckOut - criteria.CheckIn).TotalDays));
            param.Add(Connection.GetParameter("pLattitude", DataPlug.DataType._Decimal, criteria.Lattitude));
            param.Add(Connection.GetParameter("pLongitude", DataPlug.DataType._Decimal, criteria.Longitude));
            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, criteria.Location));
            param.Add(Connection.GetParameter("pCorpId", DataPlug.DataType._BigInt, criteria.LoggedInUser));
            DataTable dt;
            try
            {
                dt = Connection.GetTable("property_SearchFilter_Amadeus", param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<CLayer.SearchResult> results = new List<CLayer.SearchResult>();
            CLayer.SearchResult temp;
            //  totalCount = -1;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.SearchResult();
                // temp.Destination = Connection.ToString(dr["Destination"]);
                //     if (totalCount == -1) totalCount = Connection.ToInteger(dr["totalrows"]);
                temp.Location = Connection.ToString(dr["Location"]);
                temp.City = Connection.ToString(dr["CityName"]);
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Amount = Connection.ToDecimal(dr["Amount"]);
                temp.Title = Connection.ToString(dr["Title"]);
                temp.Description = Connection.ToString(dr["Description"]);
                temp.ImageFile = Connection.ToString(dr["ImageFile"]);
                temp.Longitude = Connection.ToString(dr["PositionLng"]);
                temp.Lattitude = Connection.ToString(dr["PositionLat"]);
                temp.StarRate = Connection.ToInteger(dr["Rating"]);
                temp.Distance = (int)(Connection.ToDouble(dr["distance"]) * 100);
                temp.IsDistanceFromCity = (Connection.ToInteger(dr["DistanceType"]) > 0);

                results.Add(temp);
            }
            totalCount = Connection.ToInteger(total.Value);
            return results;
        }


        public void SaveCancellationDetails(long propertyId, double charge, int period, CLayer.ObjectStatus.CancellationType cancType, bool appliesForRefund)
        {
            string sql = "Update property Set CancellationCharge=" + charge.ToString() + " ,CancellationPeriod=" + period.ToString() + ",CancellationType=" + ((int)cancType).ToString() + ",CanChargeForModification=" + appliesForRefund.ToString() + " Where propertyId=" + propertyId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }

        //For showing the accommodations satisfying the search criteria
        public List<CLayer.Accommodation> GetDetailsForBooking(long propertyId, CLayer.SearchCriteria criteria)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, criteria.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, criteria.CheckOut));
            param.Add(Connection.GetParameter("pAdults", DataPlug.DataType._Int, criteria.Adults));
            param.Add(Connection.GetParameter("pChildren", DataPlug.DataType._Int, criteria.Children));
            param.Add(Connection.GetParameter("pStayType", DataPlug.DataType._Int, criteria.StayType));
            param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Int, criteria.Bedrooms));
            //   param.Add(totalRows);
            param.Add(Connection.GetParameter("pStayGroup", DataPlug.DataType._Varchar, criteria.StayTypeGroup));
            param.Add(Connection.GetParameter("pStartingRow", DataPlug.DataType._Int, criteria.StaringRow));

            param.Add(Connection.GetParameter("pRangeBudgetMax", DataPlug.DataType._Int, criteria.RangeBudgetMax));
            param.Add(Connection.GetParameter("pRangeBudgetMin", DataPlug.DataType._Int, criteria.RangeBudgetMin));
            param.Add(Connection.GetParameter("pFeatures", DataPlug.DataType._Varchar, criteria.Features));
            //param.Add(Connection.GetParameter("pBeds", DataPlug.DataType._Int, criteria.Beds));
            param.Add(Connection.GetParameter("pRateType", DataPlug.DataType._Int, (int)criteria.UserType));
            param.Add(Connection.GetParameter("pDefaultRateType", DataPlug.DataType._Int, (int)criteria.DefaultUserType));
            param.Add(Connection.GetParameter("pOrderBy", DataPlug.DataType._Int, (int)criteria.SortOrder));
            param.Add(Connection.GetParameter("pBookingDayCount", DataPlug.DataType._Int, (criteria.CheckOut - criteria.CheckIn).TotalDays));

            DataTable dt = Connection.GetTable("property_GetDetailForBooking", param);
            List<CLayer.Accommodation> results = new List<CLayer.Accommodation>();

            foreach (DataRow dr in dt.Rows)
            {
                results.Add(new CLayer.Accommodation()
                {
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    StayCategory = Connection.ToString(dr["Category"]),
                    AccommodationType = Connection.ToString(dr["AType"]),
                    AccommodationCount = Connection.ToInteger(dr["AccommodationCount"]),
                    MaxNoOfPeople = Connection.ToInteger(dr["MaxNoOfPeople"]),
                    MaxNoOfChildren = Connection.ToInteger(dr["MaxNoOfChildren"]),
                    MinNoOfPeople = Connection.ToInteger(dr["MinNoOfPeople"]),
                    AccommodationTypeId = Connection.ToInteger(dr["AccommodationTypeId"]),
                    StayCategoryId = Connection.ToInteger(dr["StayCategoryId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Description = Connection.ToString(dr["Description"]),
                    //   Location = Connection.ToString(dr["Location"]),
                    BedRooms = Connection.ToInteger(dr["BedRooms"]),
                    Area = Connection.ToDecimal(dr["Area"]),
                    Rate = Connection.ToDecimal(dr["Amount"])
                });
            }

            return results;
        }
        public List<CLayer.SearchResult> GetStaticPagePrpty(long PageId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPageId", DataPlug.DataType._BigInt, PageId));
            DataTable dt = Connection.GetTable("staticpageproperty_GetAll", param);
            List<CLayer.SearchResult> result = new List<CLayer.SearchResult>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.SearchResult()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Title = Connection.ToString(dr["Title"]),
                    Description = Connection.ToString(dr["Description"]),
                    IsDistanceFromCity = Connection.ToBoolean(dr["DistanceFromCity"]),
                    ImageFile = Connection.ToString(dr["FileName"]),
                    //Amount = Connection.ToDecimal(dr["Amount"]),
                    Distance = Convert.ToInt32(Connection.ToDecimal(dr["DistanceFromCity"])),
                    StarRate = Connection.ToInteger(dr["Rating"]),
                    Location = Connection.ToString(dr["Location"]),
                    City = Connection.ToString(dr["City"])
                });
            }
            return result;
        }
        public List<CLayer.SearchResult> GetAllPrptyStatic()
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            DataTable dt = Connection.GetTable("staticpageallproperty_GetAll", param);
            List<CLayer.SearchResult> result = new List<CLayer.SearchResult>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.SearchResult()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Title = Connection.ToString(dr["Title"]),
                    ImageFile = Connection.ToString(dr["FileName"]),
                    //Description = Connection.ToString(dr["Description"]),
                    //IsDistanceFromCity = Connection.ToBoolean(dr["DistanceFromCity"]),                 
                    Distance = Convert.ToInt32(Connection.ToDecimal(dr["DistanceFromCity"])),
                    StarRate = Connection.ToInteger(dr["Rating"]),
                    Location = Connection.ToString(dr["Location"]),
                    City = Connection.ToString(dr["City"])
                });
            }
            return result;
        }
        public string Getcity(string City)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (City.Length > 100) City = City.Substring(0, 100);
            param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, City));
            object result = Connection.ExecuteQueryScalar("searchwithcity_GetAll", param);
            return Connection.ToString(result);
        }
        public decimal GetPropertyB2CpartialamountPerc(long Pid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, Pid));
            object result = Connection.ExecuteQueryScalar("GetProperty_partialpaymentperc", param);
            return Connection.ToDecimal(result);
        }
        public decimal GetPropertyB2BpartialamountPerc(long Pid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, Pid));
            object result = Connection.ExecuteQueryScalar("GetProperty_B2Bpartialpaymentperc", param);
            return Connection.ToDecimal(result);
        }
        public List<string> GetLocation(string Location)
        {
            List<string> result = new List<string>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, Location));
            DataTable dt = Connection.GetTable("property_SearchLocation", param);
            if (dt.Rows.Count > 0)
            {
                result.Add(Connection.ToString(dt.Rows[0]["Name"]));
                result.Add(Connection.ToString(dt.Rows[0]["Location"]));
            }
            return result;


        }

        public DataTable GetAccommodationDetailsFrompropertyid(long id)
        {
            string sql = "SELECT  DISTINCT a.BookingCode ,a.accommodationid,a.propertyid,a.RatePlanCode,a.RoomTypeCode,a.RoomType,a.RoomId,b.`Hotel_Id` FROM accommodation AS a  JOIN `property` AS b  ON a.PropertyId=b.`PropertyId` WHERE  a.BookingCode IS NOT NULL AND a.RatePlanCode IS NOT NULL AND a.RoomTypeCode IS NOT NULL AND b.`Hotel_Id` IS NOT NULL AND  a.PropertyId=" + id + "  ORDER BY AccommodationId DESC;";
            DataTable dt = Connection.GetSQLTable(sql);
            return dt;
        }
        public long GetPropertyIdFromTamarindId(long id)
        {
            string sql = "SELECT PropertyId FROM `property` WHERE tamarind_hotelid =" + id;
            object dt = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(dt);
        }
        public long GetPropertyIdFromTBOId(long id)
        {
            string sql = "SELECT PropertyId FROM `property`  WHERE tbo_hotelid=" + id;
            object dt = Connection.GetSQLTable(sql);
            return Convert.ToInt64(dt);
        }
        public DataTable GetHotelIDFrompropertyid(long id)
        {
            string sql = "SELECT  Hotel_Id FROM `property`  WHERE PropertyId=" + id;
            DataTable dt = Connection.GetSQLTable(sql);
            return dt;
        }
        public DataTable GetHotelIDFromTamarindid(long id)
        {
            string sql = "SELECT tamarind_hotelid FROM `property` WHERE PropertyId=" + id;
            DataTable dt = Connection.GetSQLTable(sql);
            return dt;
        }
        public DataTable GetHotelIDFromTBOid(long id)
        {
            string sql = "SELECT tbo_hotelid FROM `property` WHERE PropertyId=" + id;
            DataTable dt = Connection.GetSQLTable(sql);
            return dt;
        }
        public DataTable GetHotelFormattedDescription(long id)
        {
            string sql = "SELECT  Description as FormattedDescription,Rating FROM `property`  WHERE PropertyId=" + id;
            DataTable dt = Connection.GetSQLTable(sql);
            return dt;
        }
        public int GetPropertyInventoryAPIType(long PropertyId)
        {
            string sql = "Select InventoryAPIType From Property Where PropertyId='" + PropertyId + "'";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }

        public void GDSSaveImageurl(long propertyid, string url)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            param.Add(Connection.GetParameter("Purl", DataPlug.DataType._Varchar, url));
            object result = Connection.ExecuteQueryScalar("GDSSaveImageurl", param);


        }
        public void GDSUpdatePropertyTitle(long PropertyId, string Title)
        {
            try
            {
                string sql = "Update Property set Title='" + Title + "' Where PropertyId='" + PropertyId + "'";
                object obj = Connection.ExecuteSQLQueryScalar(sql); ;
            }
            catch (Exception ex)
            {

            }
        }
        public void GDSUpdatePropertyStarRatings(long PropertyId, int pRate)
        {
            try
            {
                string sql = "Update Property set Rating=" + pRate + ",Ratings="+ pRate + " Where PropertyId='" + PropertyId + "'";
                object obj = Connection.ExecuteSQLQueryScalar(sql); ;
            }
            catch (Exception ex)
            {

            }
        }
        public void GDSUpdatePropertyDescription(long PropertyId, string Description)
        {
            try
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
                param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Text, Description));

                object result = Connection.ExecuteQueryScalar("GDSSavePropertyDescription", param);
            }
            catch (Exception ex)
            {

            }
        }
        public void GDSUpdatePropertyDescriptionFormatted(long PropertyId, string Description, int pStarRatings, string pResponse = "")
        {
            try
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
                param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Text, Description));
                param.Add(Connection.GetParameter("pStarRatings", DataPlug.DataType._Int, pStarRatings));
                param.Add(Connection.GetParameter("pResponse", DataPlug.DataType._Text, pResponse));

                object result = Connection.ExecuteQueryScalar("GDSSavePropertyDescriptionFormatted", param);
            }
            catch (Exception ex)
            {

            }
        }

        public void GDSUpdatePropertyContactNumbers(long PropertyId, string Phone, string Mobile, string Email = "")
        {
            try
            {
                string sql = "UPDATE `property` SET `Phone` = '" + Phone + "',Mobile='" + Mobile + "',Email='" + Email + "' WHERE `PropertyId`=" + PropertyId + "";
                object obj = Connection.ExecuteSQLQueryScalar(sql);
            }
            catch (Exception ex)
            {

            }




        }
        public void GDSSavePropertyDescriptions(long pID, long propertyid, CLayer.DetailContents pDetailContents)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pID", DataPlug.DataType._BigInt, pID));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            param.Add(Connection.GetParameter("pDescriptions", DataPlug.DataType._Text, pDetailContents.Details));
            param.Add(Connection.GetParameter("pSection", DataPlug.DataType._BigInt, pDetailContents.Section));
            param.Add(Connection.GetParameter("pBlockType", DataPlug.DataType._Int, 0));
            param.Add(Connection.GetParameter("pSortOrder", DataPlug.DataType._BigInt, pDetailContents.Order));
            object result = Connection.ExecuteQueryScalar("GDSSaveFormattedDescriptions", param);

        }

        public DataTable SearchForGDSProperties(List<string> codes)
        {
            string fullcsv = string.Join("','", codes);
            fullcsv = "'" + fullcsv + "'";

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, fullcsv));
            DataTable dt = Connection.GetTable("gds_GetHotelCodes", param);

            string insertcsv = "";

            if (dt.Rows.Count != codes.Count)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    codes.Remove(Connection.ToString(dr[0]));
                }
                foreach (string item in codes)
                {
                    if (insertcsv != "") { insertcsv = insertcsv + ","; }
                    insertcsv = insertcsv + "('',2,'" + item + "',1)";
                }
            }

            param.Clear();
            param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, insertcsv));
            param.Add(Connection.GetParameter("pFullCodes", DataPlug.DataType._Text, fullcsv));
            dt = Connection.GetTable("gds_GetSearchProperties", param);

            return dt;
        }

        public DataTable SearchForGDSPropertiesWithUser(List<string> codes)
        {
            string fullcsv = string.Join("','", codes);
            fullcsv = "'" + fullcsv + "'";

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, fullcsv));
            DataTable dt = Connection.GetTable("gds_GetHotelCodes", param);

            string insertcsv = "";

            if (dt.Rows.Count != codes.Count)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    codes.Remove(Connection.ToString(dr[0]));
                }
                foreach (string item in codes)
                {
                    if (insertcsv != "") { insertcsv = insertcsv + ","; }
                    insertcsv = insertcsv + "('',2,'" + item + "',1)";
                }
            }

            param.Clear();
            param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, insertcsv));
            param.Add(Connection.GetParameter("pFullCodes", DataPlug.DataType._Text, fullcsv));

            dt = Connection.GetTable("gds_GetSearchPropertiesWithUser", param);

            return dt;
        }

        public List<CLayer.Property> GetAllGDSPropertiesRecommended()
        {
            string sql = "SELECT DISTINCT pr.PropertyId AS PropertyId,Hotel_ID AS HotelCode,country,(SELECT NAME AS CountryName FROM Country WHERE CountryID=Country LIMIT 1) AS CountryName FROM property pr  " +
                        " INNER JOIN recommended g ON pr.propertyid = g.PropertyId  WHERE pr.status = 1 AND InventoryAPITYPE = 2  ";
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.HotelID = Connection.ToString(dr["HotelCode"]);
                temp.Country = Connection.ToInteger(dr["Country"]);
                temp.Countryname = Connection.ToString(dr["CountryName"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Property> PropertyGetOnCity(int CityId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCityId", DataPlug.DataType._Int, CityId));
            DataTable dt = Connection.GetTable("Property_GetOnCity", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            //  propertyid,title,location,description,cityid,STATUS,state,country
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Property()
                {
                    PropertyId = Connection.ToInteger(dr["propertyid"]),
                    CityId = Connection.ToInteger(dr["cityid"]),
                    Title = Connection.ToString(dr["title"]),
                    State = Connection.ToInteger(dr["state"]),
                    Country = Connection.ToInteger(dr["country"])
                });
            }
            return result;
        }
        public List<CLayer.Property> GetDefaultHotels(long pUserID,string pDestination)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            
            param.Add(Connection.GetParameter("pUserID", DataPlug.DataType._BigInt , pUserID));
            param.Add(Connection.GetParameter("pDestination", DataPlug.DataType._Varchar , pDestination));
            DataTable dt = Connection.GetTable("GetDefaultHotels", param);
            List<CLayer.Property> result = new List<CLayer.Property>();
            //  propertyid,title,location,description,cityid,STATUS,state,country
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Property()
                {
                    PropertyId = Connection.ToInteger(dr["propertyid"]),
                    City = Connection.ToString(dr["city"]),
                    CityId = Connection.ToInteger(dr["cityid"]),
                    Title = Connection.ToString(dr["title"]),
                    State = Connection.ToInteger(dr["state"]),
                    MaximumEntitlement = Connection.ToLong(dr["MaximumEntitlement"]),

                    //   Country = Connection.ToInteger(dr["country"])
                });
            }
            return result;
        }
        public CLayer.Property GetBookingPropertyDetails(long propertyId)
        {
            System.Text.StringBuilder sql = new System.Text.StringBuilder();
            sql.Append("SELECT propertyid,CityID,State,Country FROM PROPERTY ");
            sql.Append(" where propertyid=" + propertyId + "");


            DataTable dt = Connection.GetSQLTable(sql.ToString());
            CLayer.Property result = null;
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.Property();
                result.CityId = Connection.ToInteger(dt.Rows[0]["CityID"]);
                result.State = Connection.ToInteger(dt.Rows[0]["State"]);
                result.Country = Connection.ToInteger(dt.Rows[0]["Country"]);
            }

            return result;
        }
        public string GetPropertyTamarindFlag(long PropertyID,string PropertyName)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyID));
            param.Add(Connection.GetParameter("pPropertyName", DataPlug.DataType._Varchar, PropertyName));
            object dt = Connection.ExecuteQueryScalar("sp_GetProperty_TamarindFlag", param);
            string result = dt.ToString();
            return result;
        }
        public CLayer.Property GetTamarind(long propertyid)
        {
            CLayer.Property property = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyid));
            DataTable dt = Connection.GetTable("property_GetTamarnd", param);
            if (dt.Rows.Count > 0)
            {
                property = new CLayer.Property();
                property.PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]);
                property.Title = Connection.ToString(dt.Rows[0]["Title"]);
                property.Description = Connection.ToString(dt.Rows[0]["Description"]);
                property.Location = Connection.ToString(dt.Rows[0]["Location"]);
                property.Status = (CLayer.ObjectStatus.StatusType)Connection.ToInteger(dt.Rows[0]["Status"]);
                property.OwnerId = Connection.ToLong(dt.Rows[0]["OwnerId"]);
                property.PositionLat = Connection.ToString(dt.Rows[0]["PositionLat"]);
                property.PositionLng = Connection.ToString(dt.Rows[0]["PositionLng"]);
                property.Address = Connection.ToString(dt.Rows[0]["Address"]);
                property.Country = Connection.ToInteger(dt.Rows[0]["Country"]);
                //property.CountryId = Connection.ToInteger(dt.Rows[0]["Country"]);
                property.State = Connection.ToInteger(dt.Rows[0]["State"]);
                property.City = Connection.ToString(dt.Rows[0]["cityname"]);
                property.CityId = Connection.ToInteger(dt.Rows[0]["CityId"]);
                property.ZipCode = Connection.ToString(dt.Rows[0]["ZipCode"]);
                property.Rating = Connection.ToInteger(dt.Rows[0]["Rating"]);
                property.IsManualReview = Connection.ToBoolean(dt.Rows[0]["IsManualReview"]);
                property.Email = Connection.ToString(dt.Rows[0]["Email"]);
                property.DistanceFromCity = Connection.ToDecimal(dt.Rows[0]["DistanceFromCity"]);
                property.Statename = Connection.ToString(dt.Rows[0]["statename"]);
                property.Countryname = Connection.ToString(dt.Rows[0]["countryname"]);
                property.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                property.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                property.CheckInTime = Connection.ToString(dt.Rows[0]["CheckInTime"]);
                property.CheckOutTime = Connection.ToString(dt.Rows[0]["CheckOutTime"]);
                property.PageTitle = Connection.ToString(dt.Rows[0]["PageTitle"]);
                property.MetaDescription = Connection.ToString(dt.Rows[0]["MetaDescription"]);
                property.AgeLimit = Connection.ToInteger(dt.Rows[0]["ChildAgeLimit"]);
                property.PropertyFor = Connection.ToInteger(dt.Rows[0]["PropertyFor"]);
                property.PropertyInventoryType = Connection.ToInteger(dt.Rows[0]["PropertyInventoryType"]);
                property.GSTRegistrationNo = Connection.ToString(dt.Rows[0]["GSTRegistrationNo"]);


            }
            return property;
        }
        public int GetTamarindInventoryAPITypeId(long tamarindhotelid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTamarindHotelId", DataPlug.DataType._BigInt, tamarindhotelid));
            object obj = Connection.ExecuteQueryScalar("property_GetTamarindInventoryAPITypeId", param);
            return Connection.ToInteger(obj);
        }
        public int GetTBOInventoryAPITypeId(long tamarindhotelid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pTamarindHotelId", DataPlug.DataType._BigInt, tamarindhotelid));
            object obj = Connection.ExecuteQueryScalar("property_GetTBOInventoryAPITypeId", param);
            return Connection.ToInteger(obj);
        }
    }
}
