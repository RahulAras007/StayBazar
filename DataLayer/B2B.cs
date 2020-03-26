using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class B2B : DataLink
    {
        public List<CLayer.B2B> GetEmptyCodeRows()
        {
            DataTable dt = Connection.GetTable("b2b_GetAllEmptyCodeRows");
            List<CLayer.B2B> result = new List<CLayer.B2B>();
            CLayer.B2B temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.B2B();
                temp.B2BId = Connection.ToLong(dr["B2BId"]);
                temp.UserType = Connection.ToInteger(dr["UserType"]);
                result.Add(temp);
            }
            return result;
        }
        public void SetCode(long b2bid, string code)
        {
            string sql = "Update b2b Set UserCode='" + code + "' Where b2bid=" + b2bid.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public string GetBusinessName(long userId)
        {
            string sql = "Select Name from b2b Where B2bId=" + userId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public long LastApproverID(long userId, long bookingid)
        {
            string sql = "SELECT approver_id FROM b2b_approvers b2b INNER JOIN booking b ON b2b.user_id = b.byuserid WHERE user_id = " + userId + " AND bookingid = " + bookingid + "  ORDER BY approver_order DESC LIMIT 1;";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }

        public bool IsCreditBookingNeeded(long userId)
        {
            string sql = "SELECT IsCreditBooking FROM b2b WHERE b2bid IN(SELECT b2bid FROM b2b_user b WHERE userid = " + userId.ToString() + ")";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            int result = Connection.ToInteger(obj);
            return (result < 1) ? false : true;
        }
        public void SetApprovalDate(long b2bId, DateTime approvalDate)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, b2bId));
            param.Add(Connection.GetParameter("pApprovalDate", DataPlug.DataType._DateTime, approvalDate));
            Connection.ExecuteQuery("b2b_SetApprovalDate", param);
        }
        public long GetCorporateIdOfUser(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            object result = Connection.ExecuteQueryScalar("b2b_GetCorporateId", param);
            return Connection.ToLong(result);
        }

        public void Delete(long b2bId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, b2bId));
            Connection.ExecuteQuery("b2b_Delete", param);
        }
        public void DeleteSupplierAfffiliate(long UserId, long B2BId, int Usertype)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, B2BId));
            param.Add(Connection.GetParameter("pUsertype", DataPlug.DataType._Int, Usertype));
            Connection.ExecuteQuery("b2b_DeleteSupplierAfffiliate", param);
        }
        public void SaveAffiliate(CLayer.B2B data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.B2BId));
            param.Add(Connection.GetParameter("pCompanyRegNo", DataPlug.DataType._Varchar, data.CompanyRegNo));
            Connection.ExecuteQuery("b2b_SaveAffiliate", param);
        }
        public void SaveSupplierForAffiliate(CLayer.B2B data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.B2BId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._BigInt, data.UserType));
            Connection.ExecuteQuery("b2b_SaveSupplierForAffiliate", param);
        }
        public int SaveCBookCredit(CLayer.B2B data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.UserId));
            param.Add(Connection.GetParameter("pIsCreditBooking", DataPlug.DataType._Int, data.IsCreditBooking));
            param.Add(Connection.GetParameter("pCreditDays", DataPlug.DataType._BigInt, data.CreditDays));
            param.Add(Connection.GetParameter("pCreditAmount", DataPlug.DataType._Decimal, data.CreditAmount));
            param.Add(Connection.GetParameter("pTotalCreditAmount", DataPlug.DataType._Decimal, data.TotalCreditAmount));
            object result = Connection.ExecuteQueryScalar("Corporatebookingcredits_Save", param);
            return Connection.ToInteger(result);

        }
        public int AllowCBookSamedaybook(int IsCorpBookingtoday, long UserId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, UserId));
            param.Add(Connection.GetParameter("pIsCorpBookingtoday", DataPlug.DataType._Int, IsCorpBookingtoday));
            object result = Connection.ExecuteQueryScalar("Corporatebooking_AllowSamedaybook", param);
            return Connection.ToInteger(result);

        }
        public List<CLayer.B2B> SearchCorporate(string name)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            param.Add(Connection.GetParameter("pCorpStatus", DataPlug.DataType._Int, (int)CLayer.Role.Roles.Corporate));
            DataTable dt = Connection.GetTable("b2b_searchcorporate", param);

            List<CLayer.B2B> result = new List<CLayer.B2B>();
            CLayer.B2B temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.B2B();
                temp.Name = Connection.ToString(dr["Name"]);
                temp.B2BId = Connection.ToLong(dr["B2BId"]);
                temp.UserCode = Connection.ToString(dr["UserCode"]);
                result.Add(temp);
            }

            return result;
        }

        public List<CLayer.Property> GetProperties(long supplierId)
        {
            string sql = "Select PropertyId,Title From Property Where OwnerId=" + supplierId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.Property> result = new List<CLayer.Property>();
            CLayer.Property temp = null;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Property();
                temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                temp.Title = Connection.ToString(dr["Title"]);
                result.Add(temp);
            }
            return result;
        }

        public List<CLayer.B2B> SearchSupplier(string name)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            param.Add(Connection.GetParameter("pCorpStatus", DataPlug.DataType._Int, (int)CLayer.Role.Roles.Supplier));
            DataTable dt = Connection.GetTable("b2b_SearchSupplier", param);

            List<CLayer.B2B> result = new List<CLayer.B2B>();
            CLayer.B2B temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.B2B();
                temp.Name = Connection.ToString(dr["Name"]);
                temp.B2BId = Connection.ToLong(dr["B2BId"]);
                temp.UserCode = Connection.ToString(dr["UserCode"]);
                result.Add(temp);
            }

            return result;
        }

        public List<CLayer.B2B> Searchsupplierforofflinebook(string name)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            param.Add(Connection.GetParameter("pCorpStatus", DataPlug.DataType._Int, (int)CLayer.Role.Roles.Supplier));
            DataTable dt = Connection.GetTable("b2b_Searchsupplierforofflinebook", param);

            List<CLayer.B2B> result = new List<CLayer.B2B>();
            CLayer.B2B temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.B2B();
                temp.Name = Connection.ToString(dr["Name"]);
                temp.B2BId = Connection.ToLong(dr["B2BId"]);
                temp.UserCode = Connection.ToString(dr["UserCode"]);
                temp.Email = Connection.ToString(dr["Email"]);
                result.Add(temp);
            }

            return result;
        }


        public List<CLayer.OfflineBooking> Searchsupplierforofflinebookfromcustom(string name)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            DataTable dt = Connection.GetTable("b2b_Searchsupplierforofflinebookfromcustom", param);

            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            CLayer.OfflineBooking temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.OfflineBooking();

                temp.CustomPropertyId = Connection.ToLong(dr["CustomPropertyId"]);
                temp.PropertyName = Connection.ToString(dr["PropertyName"]);
                temp.PropertyEmail = Connection.ToString(dr["PropertyEmail"]);
                temp.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                temp.PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]);
                temp.PropertyCountry = Connection.ToInteger(dr["PropertyCountry"]);
                temp.PropertyState = Connection.ToInteger(dr["PropertyState"]);
                temp.PropertyCity = Connection.ToInteger(dr["PropertyCity"]);
                temp.PropertyCityname = Connection.ToString(dr["Pcityname"]);



                temp.SupplierName = Connection.ToString(dr["SupplierName"]);
                temp.SupplierEmail = Connection.ToString(dr["SupplierEmail"]);
                temp.SupplierAddress = Connection.ToString(dr["SupplierAddress"]);
                temp.SupplierMobile = Connection.ToString(dr["SupplierMobile"]);
                temp.SupplierCountry = Connection.ToInteger(dr["SupplierCountry"]);
                temp.SupplierState = Connection.ToInteger(dr["SupplierState"]);
                temp.SupplierCity = Connection.ToInteger(dr["SupplierCity"]);
                temp.SupplierCityname = Connection.ToString(dr["Scityname"]);



                result.Add(temp);
            }

            return result;
        }
        public List<CLayer.OfflineBooking> Searchcustompropertylist(string name)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            DataTable dt = Connection.GetTable("b2b_SearchCustompropertylist", param);

            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            CLayer.OfflineBooking temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.OfflineBooking();

                temp.CustomPropertyId = Connection.ToLong(dr["CustomPropertyId"]);
                temp.PropertyName = Connection.ToString(dr["PropertyName"]);
                temp.PropertyEmail = Connection.ToString(dr["PropertyEmail"]);
                temp.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                temp.PropertyContactNo = Connection.ToString(dr["PropertyContactNo"]);
                temp.PropertyCountry = Connection.ToInteger(dr["PropertyCountry"]);
                temp.PropertyState = Connection.ToInteger(dr["PropertyState"]);
                temp.PropertyCity = Connection.ToInteger(dr["PropertyCity"]);
                temp.PropertyCityname = Connection.ToString(dr["Pcityname"]);
                temp.PropertyPinCode = Connection.ToString(dr["PropertyPinCode"]);

                temp.SupplierPinCode = Connection.ToString(dr["SupplierPinCode"]);

                temp.SupplierName = Connection.ToString(dr["SupplierName"]);
                temp.SupplierEmail = Connection.ToString(dr["SupplierEmail"]);
                temp.SupplierAddress = Connection.ToString(dr["SupplierAddress"]);
                temp.SupplierMobile = Connection.ToString(dr["SupplierMobile"]);
                temp.SupplierCountry = Connection.ToInteger(dr["SupplierCountry"]);
                temp.SupplierState = Connection.ToInteger(dr["SupplierState"]);
                temp.SupplierCity = Connection.ToInteger(dr["SupplierCity"]);
                temp.SupplierCityname = Connection.ToString(dr["Scityname"]);


                temp.GSTRegistrationNo = Connection.ToString(dr["GSTRegistrationNo"]);
                temp.PAN = Connection.ToString(dr["PANNo"]);
                result.Add(temp);
            }

            return result;
        }

        public List<CLayer.OfflineBooking> SearchcustomerListlist(string name)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            DataTable dt = Connection.GetTable("b2b_SearchcustomerListforofflinebook", param);

            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            CLayer.OfflineBooking temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.OfflineBooking();
                temp.CustomerId = Connection.ToLong(dr["b2bid"]);
                temp.CustomerName = Connection.ToString(dr["customername"]);
                temp.CustomerEmail = Connection.ToString(dr["Email"]);
                temp.CustomerAddress = Connection.ToString(dr["Address"]);
                temp.CustomerMobile = Connection.ToString(dr["Mobile"]);
                temp.CustomerCountry = Connection.ToInteger(dr["countryname"]);
                temp.CustomerState = Connection.ToInteger(dr["statename"]);
                temp.CustomerCity = Connection.ToInteger(dr["city"]);
                temp.CustomerCityname = Connection.ToString(dr["cityname"]);
                temp.UserType = Connection.ToLong(dr["UserType"]);
                //temp.CustomerType = Connection.ToInteger(dr["UserType"]);
                temp.CategoryType = Connection.ToString(dr["UserTableCat"]);
                temp.ZipCode = Connection.ToString(dr["ZipCode"]);
                temp.NoInvoiceMail = Connection.ToBoolean(dr["NoInvoiceMail"]);
                result.Add(temp);
            }

            return result;
        }

        public List<CLayer.OfflineBooking> SearchPaymentCustomerList(string name, int start, int limit)
        {
            List<CLayer.OfflineBooking> users = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("SP_SearchPaymentListToCustomer", param);

            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                users.Add(new CLayer.OfflineBooking()
                {
                    CustomerId = Connection.ToLong(dr["b2bid"]),
                    CustomerName = Connection.ToString(dr["customername"]),
                    CustomerEmail = Connection.ToString(dr["Email"]),
                    CustomerAddress = Connection.ToString(dr["Address"]),
                    CustomerMobile = Connection.ToString(dr["Mobile"]),
                    CustomerCountry = Connection.ToInteger(dr["countryname"]),
                    CustomerState = Connection.ToInteger(dr["statename"]),
                    CustomerCity = Connection.ToInteger(dr["city"]),
                    CustomerCityname = Connection.ToString(dr["cityname"]),
                    UserType = Connection.ToLong(dr["UserType"]),
                    //temp.CustomerType = Connection.ToInteger(dr["UserType"]);
                    CategoryType = Connection.ToString(dr["UserTableCat"]),
                    ZipCode = Connection.ToString(dr["ZipCode"]),
                    NoInvoiceMail = Connection.ToBoolean(dr["NoInvoiceMail"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            
            }
            return users;

        }

public List<CLayer.Property> Searchpropertylist(string name)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
    DataTable dt = Connection.GetTable("b2b_Searchpropertylist", param);

    List<CLayer.Property> result = new List<CLayer.Property>();
    CLayer.Property temp;
    foreach (DataRow dr in dt.Rows)
    {
        temp = new CLayer.Property();
        temp.Title = Connection.ToString(dr["Title"]);
        temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
        temp.BusinessName = Connection.ToString(dr["name"]);
        temp.Email = Connection.ToString(dr["Email"]);
        temp.Address = Connection.ToString(dr["address"]);
        temp.SupplierName = Connection.ToString(dr["FirstName"]);
        temp.Mobile = Connection.ToString(dr["Mobile"]);
        temp.Country = Connection.ToInteger(dr["Country"]);
        temp.State = Connection.ToInteger(dr["State"]);
        temp.CityId = Connection.ToInteger(dr["CityId"]);
        temp.OwnerId = Connection.ToInteger(dr["OwnerId"]);
        temp.City = Connection.ToString(dr["City"]);
        temp.ZipCode = Connection.ToString(dr["PropertyZipcode"]);
        temp.SupplierAddress = Connection.ToString(dr["supaddress"]);
        temp.SupplierCountry = Connection.ToInteger(dr["supcountry"]);
        temp.SupplierState = Connection.ToInteger(dr["supstate"]);
        temp.SupplierCity = Connection.ToString(dr["supcityname"]);
        temp.SupplierCityId = Connection.ToInteger(dr["supcityid"]);
        temp.SupplierZipCode = Connection.ToString(dr["SupplierZipeCode"]);
        temp.SuppierMobile = Connection.ToString(dr["supmobile"]);
        temp.GSTRegistrationNo = Connection.ToString(dr["GSTRegistrationNo"]);
        temp.PanNo = Connection.ToString(dr["PANNo"]);
        //temp.EntityName = Connection.ToString(dr["entityname"]);
        result.Add(temp);
    }

    return result;
}
public List<CLayer.Property> Searchpropertylistaftersup(string name)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
    DataTable dt = Connection.GetTable("b2b_Searchpropertylistaftersup", param);

    List<CLayer.Property> result = new List<CLayer.Property>();
    CLayer.Property temp;
    foreach (DataRow dr in dt.Rows)
    {
        temp = new CLayer.Property();
        temp.Title = Connection.ToString(dr["Title"]);
        temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
        temp.BusinessName = Connection.ToString(dr["name"]);
        temp.Email = Connection.ToString(dr["Email"]);
        temp.Address = Connection.ToString(dr["address"]);
        temp.SupplierName = Connection.ToString(dr["FirstName"]);
        temp.Mobile = Connection.ToString(dr["Mobile"]);
        temp.Country = Connection.ToInteger(dr["Country"]);
        temp.State = Connection.ToInteger(dr["State"]);
        temp.CityId = Connection.ToInteger(dr["CityId"]);
        temp.OwnerId = Connection.ToInteger(dr["OwnerId"]);
        result.Add(temp);
    }
    return result;
}

public List<CLayer.B2B> SearchAffiliate(string name)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, name));
    param.Add(Connection.GetParameter("pCorpStatus", DataPlug.DataType._Int, (int)CLayer.Role.Roles.Affiliate));
    DataTable dt = Connection.GetTable("b2b_SearchSupplier", param);

    List<CLayer.B2B> result = new List<CLayer.B2B>();
    CLayer.B2B temp;
    foreach (DataRow dr in dt.Rows)
    {
        temp = new CLayer.B2B();
        temp.Name = Connection.ToString(dr["Name"]);
        temp.B2BId = Connection.ToLong(dr["B2BId"]);
        temp.UserCode = Connection.ToString(dr["UserCode"]);
        result.Add(temp);
    }

    return result;
}
public List<CLayer.B2B> Getall(int usertype, int status)
{
    string qry = "SELECT u.UserId,u.Email,u.UserType,u.UserId,b.* FROM  b2b b" +
                 " INNER JOIN USER u ON u.UserId = b.B2BId" +
                 " WHERE u.UserType = " + usertype.ToString();
    if (status > 0)
        qry += " AND b.RequestStatus = " + status.ToString();
    else
        qry += " AND b.RequestStatus IN (" + ((int)CLayer.ObjectStatus.StatusType.Unread).ToString() +
               "," + ((int)CLayer.ObjectStatus.StatusType.Read).ToString() +
               "," + ((int)CLayer.ObjectStatus.StatusType.NotVerified).ToString() + ")";
    qry += " ORDER BY b.RequestStatus ASC,b.B2BId DESC";
    DataTable dt = Connection.GetSQLTable(qry);
    List<CLayer.B2B> result = new List<CLayer.B2B>();
    foreach (DataRow dr in dt.Rows)
    {
        result.Add(new CLayer.B2B()
        {
            B2BId = Connection.ToLong(dr["B2BId"]),
            Name = Connection.ToString(dr["Name"]),
            MarkupPercent = Connection.ToDecimal(dr["MarkupPercent"]),
            CompanyRegNo = Connection.ToString(dr["CompanyRegNo"]),
            CommissionPercent = Connection.ToDecimal(dr["CommissionPercent"]),
            UserCode = Connection.ToString(dr["UserCode"]),
            ServiceTaxRegNo = Connection.ToString(dr["ServiceTaxRegNo"]),
            VATRegNo = Connection.ToString(dr["VATRegNo"]),
            UserId = Connection.ToLong(dr["UserId"]),
            Email = Connection.ToString(dr["Email"]),
            UserType = Connection.ToInteger(dr["UserType"]),
            RequestStatus = Connection.ToInteger(dr["RequestStatus"]),
            PropertyDescription = Connection.ToString(dr["PropertyDescription"]),
            AvailableProperties = Connection.ToInteger(dr["AvailableProperties"])
        });
    }
    return result;
}
public long GetSupplierID(string suppliername)
{
    string qry = "SELECT * FROM  b2b b inner join user u on b.b2bid=u.userid where u.UserType =3 and b.name='" + suppliername + "'";

    DataTable dt = Connection.GetSQLTable(qry);

    long B2BId = 0;
    foreach (DataRow dr in dt.Rows)
    {

        B2BId = Connection.ToLong(dr["B2BId"]);


    }
    return B2BId;
}
public List<CLayer.B2B> GetAllSupplier(long userId, int userType)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2bId", DataPlug.DataType._BigInt, userId));
    param.Add(Connection.GetParameter("pType", DataPlug.DataType._BigInt, userType));
    DataTable dt = Connection.GetTable("supplier_GetAll", param);
    List<CLayer.B2B> result = new List<CLayer.B2B>();
    foreach (DataRow dr in dt.Rows)
    {
        result.Add(new CLayer.B2B()
        {

            B2BId = Connection.ToLong(dr["B2BId"]),
            Name = Connection.ToString(dr["Name"]),
            MarkupPercent = Connection.ToDecimal(dr["MarkupPercent"]),
            CommissionPercent = Connection.ToDecimal(dr["CommissionPercent"]),
            UserCode = Connection.ToString(dr["UserCode"]),
            ServiceTaxRegNo = Connection.ToString(dr["ServiceTaxRegNo"]),
            VATRegNo = Connection.ToString(dr["VATRegNo"]),
            UserId = Connection.ToLong(dr["UserId"]),
            Email = Connection.ToString(dr["Email"]),
            UserType = Connection.ToInteger(dr["UserType"]),
            RequestStatus = Connection.ToInteger(dr["RequestStatus"]),
            PropertyDescription = Connection.ToString(dr["PropertyDescription"]),
            AvailableProperties = Connection.ToInteger(dr["AvailableProperties"])

        });
    }
    return result;
}
public List<CLayer.B2B> GetSearchAllSupplier(string name, int start, int limit, int userType, out int totalRows)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    //param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, email));
    param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
    param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
    param.Add(Connection.GetParameter("pCriteria", DataPlug.DataType._Varchar, name));
    param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._BigInt, userType));
    DataTable dt = Connection.GetTable("affiliate_SearchSupplier", param);
    List<CLayer.B2B> result = new List<CLayer.B2B>();
    if (dt.Rows.Count > 0)
    {
        totalRows = Connection.ToInteger(dt.Rows[0]["TotalRows"]);
    }
    else
    {
        totalRows = 0;
    }
    foreach (DataRow dr in dt.Rows)
    {
        result.Add(new CLayer.B2B()
        {

            B2BId = Connection.ToLong(dr["B2BId"]),
            Name = Connection.ToString(dr["Name"]),
            //MarkupPercent = Connection.ToDecimal(dr["MarkupPercent"]),
            //CommissionPercent = Connection.ToDecimal(dr["CommissionPercent"]),
            UserCode = Connection.ToString(dr["UserCode"]),
            //ServiceTaxRegNo = Connection.ToString(dr["ServiceTaxRegNo"]),
            // VATRegNo = Connection.ToString(dr["VATRegNo"]),
            UserId = Connection.ToLong(dr["UserId"]),
            Email = Connection.ToString(dr["Email"]),
            // UserType = Connection.ToInteger(dr["UserType"]),
            //RequestStatus = Connection.ToInteger(dr["RequestStatus"]),
            //  PropertyDescription = Connection.ToString(dr["PropertyDescription"]),
            //AvailableProperties = Connection.ToInteger(dr["AvailableProperties"])

        });
    }
    return result;
}
public CLayer.B2B Get(long b2bId)
{
    CLayer.B2B b2b = null;
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, b2bId));
    DataTable dt = Connection.GetTable("b2b_Get", param);
    if (dt.Rows.Count > 0)
    {
        b2b = new CLayer.B2B();
        b2b.B2BId = Connection.ToLong(dt.Rows[0]["B2BId"]);
        b2b.Name = Connection.ToString(dt.Rows[0]["Name"]);
        b2b.UserCode = Connection.ToString(dt.Rows[0]["UserCode"]);
        b2b.MarkupPercent = Connection.ToDecimal(dt.Rows[0]["MarkupPercent"]);
        b2b.CommissionPercent = Connection.ToDecimal(dt.Rows[0]["CommissionPercent"]);
        b2b.ServiceTaxRegNo = Connection.ToString(dt.Rows[0]["ServiceTaxRegNo"]);
        b2b.VATRegNo = Connection.ToString(dt.Rows[0]["VATRegNo"]);
        b2b.MaximumStaff = Connection.ToInteger(dt.Rows[0]["MaximumStaff"]);
        b2b.RequestStatus = Connection.ToInteger(dt.Rows[0]["RequestStatus"]);
        b2b.Email = Connection.ToString(dt.Rows[0]["Email"]);
        b2b.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
        b2b.CompanyRegNo = Connection.ToString(dt.Rows[0]["CompanyRegNo"]);
        b2b.UserType = Connection.ToInteger(dt.Rows[0]["UserType"]);
        b2b.PropertyDescription = Connection.ToString(dt.Rows[0]["PropertyDescription"]);
        b2b.AvailableProperties = Connection.ToInteger(dt.Rows[0]["AvailableProperties"]);
        b2b.PANNo = Connection.ToString(dt.Rows[0]["PANNo"]);
        b2b.ContactDesignation = Connection.ToString(dt.Rows[0]["ContactDesignation"]);
        b2b.CreditPeriod = Connection.ToInteger(dt.Rows[0]["CreditPeriod"]);

    }
    return b2b;
}
public CLayer.B2B GetbookingcreditforCorte(long b2bId)
{
    CLayer.B2B b2b = null;
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, b2bId));
    DataTable dt = Connection.GetTable("b2bcorporate_Getbookingcredits", param);
    if (dt.Rows.Count > 0)
    {
        b2b = new CLayer.B2B();
        b2b.B2BId = Connection.ToLong(dt.Rows[0]["B2BId"]);
        b2b.Name = Connection.ToString(dt.Rows[0]["Name"]);
        b2b.UserCode = Connection.ToString(dt.Rows[0]["UserCode"]);
        b2b.MarkupPercent = Connection.ToDecimal(dt.Rows[0]["MarkupPercent"]);
        b2b.CommissionPercent = Connection.ToDecimal(dt.Rows[0]["CommissionPercent"]);
        b2b.ServiceTaxRegNo = Connection.ToString(dt.Rows[0]["ServiceTaxRegNo"]);
        b2b.VATRegNo = Connection.ToString(dt.Rows[0]["VATRegNo"]);
        b2b.MaximumStaff = Connection.ToInteger(dt.Rows[0]["MaximumStaff"]);
        b2b.RequestStatus = Connection.ToInteger(dt.Rows[0]["RequestStatus"]);
        b2b.Email = Connection.ToString(dt.Rows[0]["Email"]);
        b2b.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
        b2b.CompanyRegNo = Connection.ToString(dt.Rows[0]["CompanyRegNo"]);
        b2b.UserType = Connection.ToInteger(dt.Rows[0]["UserType"]);
        b2b.PropertyDescription = Connection.ToString(dt.Rows[0]["PropertyDescription"]);
        b2b.AvailableProperties = Connection.ToInteger(dt.Rows[0]["AvailableProperties"]);
        b2b.PANNo = Connection.ToString(dt.Rows[0]["PANNo"]);
        b2b.ContactDesignation = Connection.ToString(dt.Rows[0]["ContactDesignation"]);
        b2b.IsCreditBooking = Connection.ToInteger(dt.Rows[0]["IsCreditBooking"]);
        b2b.CreditDays = Connection.ToInteger(dt.Rows[0]["CreditDays"]);
        b2b.CreditAmount = Connection.ToDecimal(dt.Rows[0]["CreditAmount"]);
        b2b.TotalCreditAmount = Connection.ToDecimal(dt.Rows[0]["TotalCreditAmount"]);

    }
    return b2b;
}

public CLayer.B2B GetbookingcreditforCorporateUser(long b2bId)
{
    CLayer.B2B b2b = null;
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, b2bId));
    DataTable dt = Connection.GetTable("b2bcorporateuser_Getbookingcredits", param);
    if (dt.Rows.Count > 0)
    {
        b2b = new CLayer.B2B();
        b2b.B2BId = Connection.ToLong(dt.Rows[0]["B2BId"]);
        b2b.Name = Connection.ToString(dt.Rows[0]["Name"]);
        b2b.UserCode = Connection.ToString(dt.Rows[0]["UserCode"]);
        b2b.MarkupPercent = Connection.ToDecimal(dt.Rows[0]["MarkupPercent"]);
        b2b.CommissionPercent = Connection.ToDecimal(dt.Rows[0]["CommissionPercent"]);
        b2b.ServiceTaxRegNo = Connection.ToString(dt.Rows[0]["ServiceTaxRegNo"]);
        b2b.VATRegNo = Connection.ToString(dt.Rows[0]["VATRegNo"]);
        b2b.MaximumStaff = Connection.ToInteger(dt.Rows[0]["MaximumStaff"]);
        b2b.RequestStatus = Connection.ToInteger(dt.Rows[0]["RequestStatus"]);
        b2b.Email = Connection.ToString(dt.Rows[0]["Email"]);
        b2b.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
        b2b.CompanyRegNo = Connection.ToString(dt.Rows[0]["CompanyRegNo"]);
        b2b.UserType = Connection.ToInteger(dt.Rows[0]["UserType"]);
        b2b.PropertyDescription = Connection.ToString(dt.Rows[0]["PropertyDescription"]);
        b2b.AvailableProperties = Connection.ToInteger(dt.Rows[0]["AvailableProperties"]);
        b2b.PANNo = Connection.ToString(dt.Rows[0]["PANNo"]);
        b2b.ContactDesignation = Connection.ToString(dt.Rows[0]["ContactDesignation"]);
        b2b.IsCreditBooking = Connection.ToInteger(dt.Rows[0]["IsCreditBooking"]);
        b2b.CreditDays = Connection.ToInteger(dt.Rows[0]["CreditDays"]);
        b2b.CreditAmount = Connection.ToDecimal(dt.Rows[0]["CreditAmount"]);
        b2b.TotalCreditAmount = Connection.ToDecimal(dt.Rows[0]["TotalCreditAmount"]);

        b2b.ApproverID = Connection.ToInteger(dt.Rows[0]["ApproverID"]);
        b2b.ApproverEmail = Connection.ToString(dt.Rows[0]["ApproverEmail"]);
        b2b.ApproverName = Connection.ToString(dt.Rows[0]["ApproverName"]);
        b2b.BookingMobile = Connection.ToString(dt.Rows[0]["BookingMobile"]);
        b2b.BookingPhone = Connection.ToString(dt.Rows[0]["BookingPhone"]);

    }
    return b2b;
}

public DateTime GetbookingsmedayforCorp(long b2bId)
{

    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, b2bId));
    object result = Connection.ExecuteQueryScalar("b2bcorporate_GetbookingsmedayforCorp", param);
    return Connection.ToDate(result);
}

public long Update(CLayer.B2B data)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.B2BId));
    param.Add(Connection.GetParameter("pBusinessName", DataPlug.DataType._Varchar, data.Name));
    //param.Add(Connection.GetParameter("pPANNo", DataPlug.DataType._Varchar, data.PANNo));
    param.Add(Connection.GetParameter("pPropertyDescription", DataPlug.DataType._Varchar, data.PropertyDescription));
    param.Add(Connection.GetParameter("pAvailableProperties", DataPlug.DataType._Int, data.AvailableProperties));
    param.Add(Connection.GetParameter("pContactDesignation", DataPlug.DataType._Varchar, data.ContactDesignation));
    //param.Add(Connection.GetParameter("pPANNo", DataPlug.DataType._Varchar, data.PANNo));           
    object result = Connection.ExecuteQueryScalar("b2b_Update", param);
    return Connection.ToInteger(result);
}
public long SaveBusinessname(CLayer.B2B data)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.B2BId));
    param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, data.Name));
    param.Add(Connection.GetParameter("pContactDesignation", DataPlug.DataType._Varchar, data.ContactDesignation));
    object result = Connection.ExecuteQueryScalar("b2b_SaveBusinessname", param);
    return Connection.ToInteger(result);
}

public long Save(CLayer.B2B data)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.B2BId));
    param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, data.Name));
    param.Add(Connection.GetParameter("pUserCode", DataPlug.DataType._Varchar, data.UserCode));
    param.Add(Connection.GetParameter("pMarkupPercent", DataPlug.DataType._Decimal, data.MarkupPercent));
    param.Add(Connection.GetParameter("pCommissionPercent", DataPlug.DataType._Decimal, data.CommissionPercent));
    param.Add(Connection.GetParameter("pServiceTaxRegNo", DataPlug.DataType._Varchar, data.ServiceTaxRegNo));
    param.Add(Connection.GetParameter("pVATRegNo", DataPlug.DataType._Varchar, data.VATRegNo));
    param.Add(Connection.GetParameter("pPANNo", DataPlug.DataType._Varchar, data.PANNo));
    //param.Add(Connection.GetParameter("pMaximumStaff", DataPlug.DataType._Int, data.MaximumStaff));
    param.Add(Connection.GetParameter("pPropertyDescription", DataPlug.DataType._Varchar, data.PropertyDescription));
    param.Add(Connection.GetParameter("pAvailableProperties", DataPlug.DataType._Int, data.AvailableProperties));
    param.Add(Connection.GetParameter("pContactDesignation", DataPlug.DataType._Varchar, data.ContactDesignation));
    object result = Connection.ExecuteQueryScalar("b2b_Save", param);
    return Connection.ToInteger(result);
}

public long Savedoc(CLayer.B2B data)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.B2BId));
    param.Add(Connection.GetParameter("pServiceTaxRegNo", DataPlug.DataType._Varchar, data.ServiceTaxRegNo));
    param.Add(Connection.GetParameter("pVATRegNo", DataPlug.DataType._Varchar, data.VATRegNo));
    param.Add(Connection.GetParameter("pPANNo", DataPlug.DataType._Varchar, data.PANNo));
    //param.Add(Connection.GetParameter("pContactDesignation", DataPlug.DataType._Varchar, data.ContactDesignation));
    object result = Connection.ExecuteQueryScalar("b2b_Savedoc", param);
    return Connection.ToInteger(result);
}



public void SetStatus(long B2BId, int Status)
{
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, B2BId));
    param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
    object result = Connection.ExecuteQueryScalar("b2b_SetStatus", param);
}

public void SetMaxStaff(long B2BId, int MaxStaff)
{
    Connection.ExecuteSqlQuery("UPDATE b2b SET MaximumStaff=" + MaxStaff.ToString() + " WHERE B2BId=" + B2BId.ToString());
    return;
}
public void SetCreditPeriod(long B2BId, int CreditPeriod)
{
    Connection.ExecuteSqlQuery("UPDATE b2b SET CreditPeriod=" + CreditPeriod.ToString() + " WHERE B2BId=" + B2BId.ToString());
    return;
}

public List<CLayer.User> GetAllCorporateUsers(long corporateId)
{
    List<CLayer.User> result = new List<CLayer.User>();
    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
    param.Add(Connection.GetParameter("pCorporateId", DataPlug.DataType._BigInt, corporateId));
    param.Add(Connection.GetParameter("pDeleteStatus", DataPlug.DataType._Int, CLayer.ObjectStatus.StatusType.Deleted));
    //corporate_GetUsers
    DataTable dt = Connection.GetTable("corporate_GetUsers", param);
    CLayer.User temp;
    foreach (DataRow dr in dt.Rows)
    {
        temp = new CLayer.User()
        {
            Salutation = Connection.ToString(dr["Salutation"]),
            Email = Connection.ToString(dr["Email"]),
            FirstName = Connection.ToString(dr["Firstname"]),
            LastName = Connection.ToString(dr["Lastname"]),
            Status = Connection.ToInteger(dr["Status"]),
            UserId = Connection.ToLong(dr["UserId"]),
            UserTypeId = Connection.ToInteger(dr["CorpUserType"]),
            AssistedBookingFlag=Connection.ToInteger(dr["AssistedBooking_Flag"])//corporate user type :admin, staff
        };
        temp.Name = temp.Salutation + " " + temp.FirstName + " " + temp.LastName;
        result.Add(temp);
    }
    return result;
}
    }
}
