using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class User : DataProvider.DataLink
    {
        
        public void SetName(long userId, string firstName, string lastName)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pFirstName", DataPlug.DataType._Varchar, firstName));
            param.Add(Connection.GetParameter("pLastName", DataPlug.DataType._Varchar, lastName));
            Connection.ExecuteQuery("user_SetName", param);
        }
        public void SetLoginDate(long userId, string dateVal)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pLastLogin", DataPlug.DataType._Varchar, dateVal));
            Connection.ExecuteQuery("user_setlastlogin", param);
        }
        public int GetStatus(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            object obj = Connection.ExecuteQueryScalar("user_GetStatus", param);
            return Connection.ToInteger(obj);
        }
        public long CreateGuest(string email, string phone, CLayer.ObjectStatus.StatusType userStatus, CLayer.Role.Roles userRole, CLayer.Address.AddressTypes addrType)
        {
            //`(pEmail VARCHAR(200),pPhone VARCHAR(20),pStatus INT, pToken VARCHAR(100),pUType INT,pAType INT)
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, email));
            param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, phone));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)userStatus));
            param.Add(Connection.GetParameter("pUType", DataPlug.DataType._Int, (int)userRole));
            param.Add(Connection.GetParameter("pToken", DataPlug.DataType._Varchar, Guid.NewGuid().ToString()));
            param.Add(Connection.GetParameter("pAType", DataPlug.DataType._Int, (int)addrType));

            object obj = Connection.ExecuteQueryScalar("user_CreateGuest", param);
            return Connection.ToLong(obj);
        }
        public List<CLayer.User> GetAllStaff(int status)
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            DataTable dt = Connection.GetTable("user_GetAllStaff", param);
            foreach (DataRow dr in dt.Rows)
            {
                users.Add(new CLayer.User()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    Salutation = Connection.ToString(dr["Salutation"]),
                    //Businessname = Connection.ToString(dr["Businessname"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    DateOfBirth = Connection.ToDate(dr["DateOfBirth"]),
                    Sex = Connection.ToInteger(dr["Sex"]),
                    UserType = Connection.ToString(dr["UserType"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    Email = Connection.ToString(dr["Email"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    LastLoginOn = Connection.ToDate(dr["LastLoginOn"])
                });
            }

            return users;
        }
        
        public List<CLayer.User> GetAllSalesperson()
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            DataTable dt = Connection.GetTable("user_GetAllSalesperson", param);
            foreach (DataRow dr in dt.Rows)
            {
                users.Add(new CLayer.User()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    Salutation = Connection.ToString(dr["Salutation"]),
                    Name = Connection.ToString(dr["FirstName"]) + " " + Connection.ToString(dr["LastName"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    DateOfBirth = Connection.ToDate(dr["DateOfBirth"]),
                    Sex = Connection.ToInteger(dr["Sex"]),
                    UserType = Connection.ToString(dr["UserType"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    Email = Connection.ToString(dr["Email"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    LastLoginOn = Connection.ToDate(dr["LastLoginOn"])
                });
            }

            return users;
        }

        public List<CLayer.User> GetAllSalespersonandRegionalmanager()
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            DataTable dt = Connection.GetTable("user_GetAllSalespersonandRegionalmanager", param);
            foreach (DataRow dr in dt.Rows)
            {
                users.Add(new CLayer.User()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    Salutation = Connection.ToString(dr["Salutation"]),
                    Name = Connection.ToString(dr["FirstName"]) + " " + Connection.ToString(dr["LastName"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    DateOfBirth = Connection.ToDate(dr["DateOfBirth"]),
                    Sex = Connection.ToInteger(dr["Sex"]),
                    UserType = Connection.ToString(dr["UserType"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    Email = Connection.ToString(dr["Email"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    LastLoginOn = Connection.ToDate(dr["LastLoginOn"])
                });
            }

            return users;
        }
        public List<CLayer.User> GetAllCustomerForSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, userType));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("user_GetAllCustomerForSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                users.Add(new CLayer.User()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    Salutation = Connection.ToString(dr["Salutation"]),
                    //Businessname = Connection.ToString(dr["Businessname"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Email = Connection.ToString(dr["Email"]),
                    City = Connection.ToString(dr["City"]),
                    State = Connection.ToString(dr["State"]),
                    Country = Connection.ToString(dr["Country"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    LastLoginOn = Connection.ToDate(dr["LastLoginOn"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    OrderId = Connection.ToString(dr["OrderNo"])
                });
            }

            return users;
        }

        //Excel report
        public DataTable GetAllB2BForExcel(int status, string searchString, int searchItem, int userType)
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, userType));
            DataTable ds = Connection.GetTable("user_GetAllB2BForExcel", param);
            
            return ds;
        }

        public List<CLayer.User> GetAllB2BForSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, userType));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("user_GetAllB2BForSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                users.Add(new CLayer.User()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    // Salutation = Connection.ToString(dr["Salutation"]),
                    Businessname = Connection.ToString(dr["Businessname"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Email = Connection.ToString(dr["Email"]),
                    City = Connection.ToString(dr["City"]),
                    State = Connection.ToString(dr["State"]),
                    Country = Connection.ToString(dr["Country"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    LastLoginOn = Connection.ToDate(dr["LastLoginOn"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    PANNo = Connection.ToString(dr["PANNo"]),
                    UserCode = Connection.ToString(dr["UserCode"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }

            return users;
        }
        public List<CLayer.User> GetAllPropertyStatus(int statusfor, int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusFor", DataPlug.DataType._Int, statusfor));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, userType));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("propertymanage_GetAllForSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                users.Add(new CLayer.User()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    // Salutation = Connection.ToString(dr["Salutation"]),
                    Businessname = Connection.ToString(dr["Businessname"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Email = Connection.ToString(dr["Email"]),
                    City = Connection.ToString(dr["City"]),
                    State = Connection.ToString(dr["State"]),
                    Country = Connection.ToString(dr["Country"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    LastLoginOn = Connection.ToDate(dr["LastLoginOn"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    PANNo = Connection.ToString(dr["PANNo"]),
                    UserCode = Connection.ToString(dr["UserCode"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyId = Connection.ToLong(dr["propertyId"]),
                    Location = Connection.ToString(dr["Location"]),
                    InventoryAPIType = Connection.ToInteger(dr["InventoryAPIType"])

                });
            }
            return users;
        }

        public List<CLayer.User> GetAllPropertyStatusWithType(int statusfor, int status, int Type, string searchString, int searchItem, int userType, int start, int limit)
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatusFor", DataPlug.DataType._Int, statusfor));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, Type));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, userType));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("propertymanage_GetAllForSearchWithType", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                users.Add(new CLayer.User()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    // Salutation = Connection.ToString(dr["Salutation"]),
                    Businessname = Connection.ToString(dr["Businessname"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Email = Connection.ToString(dr["Email"]),
                    City = Connection.ToString(dr["City"]),
                    State = Connection.ToString(dr["State"]),
                    Country = Connection.ToString(dr["Country"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    LastLoginOn = Connection.ToDate(dr["LastLoginOn"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    PANNo = Connection.ToString(dr["PANNo"]),
                    UserCode = Connection.ToString(dr["UserCode"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyId = Connection.ToLong(dr["propertyId"]),
                    Location = Connection.ToString(dr["Location"]),
                    InventoryAPIType = Connection.ToInteger(dr["InventoryAPIType"])

                });
            }
            return users;
        }

        public void SaveSalesRegion(long userids, string SalesRegion)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userids));
            //param.Add(Connection.GetParameter("pBusinessname", DataPlug.DataType._Varchar, data.Businessname));
            param.Add(Connection.GetParameter("pSalesRegion", DataPlug.DataType._Varchar, SalesRegion));

            Connection.ExecuteQueryScalar("staff_Save", param);

        }
        public void SetStatus(string userids, int status)
        {
            Connection.ExecuteSqlQuery("UPDATE `user` SET `Status`=" + status.ToString() + ",DeletedDate=NOW() WHERE UserId IN(" + userids + ")");
            return;
        }
        public void SetDeleteStatus(long userId, int status)
        {
            Connection.ExecuteSqlQuery("UPDATE `user` SET `Status`=" + status.ToString() + ",DeletedDate=NOW() WHERE UserId =" + userId.ToString());
            return;
        }
        public void SetCustomerPaymentMode(long userId, int CustomerPaymentMode, decimal CustomerPaymentModeCreditDays)
        {
            CustomerPaymentModeCreditDays = CustomerPaymentMode < 3 ? 0 : CustomerPaymentModeCreditDays;
            Connection.ExecuteSqlQuery("UPDATE `user` SET `CustomerPaymentMode`=" + CustomerPaymentMode + ",CustomerPaymentModeCreditDays=" + CustomerPaymentModeCreditDays + " WHERE UserId =" + userId.ToString());
            return;
        }

        public CLayer.User Get(long userid)
        {
            CLayer.User user = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userid));
            DataTable dt = Connection.GetTable("user_Get", param);
            if (dt.Rows.Count > 0)
            {
                user = new CLayer.User();
                user.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                user.SalutationId = Connection.ToInteger(dt.Rows[0]["SalutationId"]);
                user.FirstName = Connection.ToString(dt.Rows[0]["FirstName"]);
                user.salesregion = Connection.ToString(dt.Rows[0]["SalesRegion"]);
                user.LastName = Connection.ToString(dt.Rows[0]["LastName"]);
                user.DateOfBirth = Connection.ToDate(dt.Rows[0]["DateOfBirth"]);
                user.Sex = Connection.ToInteger(dt.Rows[0]["Sex"]);
                user.UserTypeId = Connection.ToInteger(dt.Rows[0]["UserType"]);
                user.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                user.Email = Connection.ToString(dt.Rows[0]["Email"]);
                user.CreatedDate = Connection.ToDate(dt.Rows[0]["CreatedDate"]);
                user.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                user.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                user.OPSEmail = Connection.ToString(dt.Rows[0]["OPSEmail"]);
                user.MaximumDailyEntitlement = Connection.ToInteger(dt.Rows[0]["MaximumDailyEntitlement"]);
                user.GradeID = Connection.ToInteger(dt.Rows[0]["GradeID"]);
                user.CustomerPaymentMode = Connection.ToInteger(dt.Rows[0]["CustomerPaymentMode"]);
                user.CustomerPaymentModeCreditDays = Connection.ToDecimal(dt.Rows[0]["CustomerPaymentModeCreditDays"]);
                user.SbEntities = Connection.ToString(dt.Rows[0]["SbEntities"]);
                user.Address = Connection.ToString(dt.Rows[0]["Address"]);
                user.Country = Connection.ToString(dt.Rows[0]["Country"]);
                user.State = Connection.ToString(dt.Rows[0]["State"]);
                user.City = Connection.ToString(dt.Rows[0]["City"]);
                user.CostCentre = Connection.ToInteger(dt.Rows[0]["CostCentre"]);
                user.AssistedBookingFlag = Connection.ToInteger(dt.Rows[0]["AssistedBooking_Flag"]);
            }
            return user;
        }

        public CLayer.User getuserAddressdata(long userid)
        {
            CLayer.User user = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userid));
            DataTable dt = Connection.GetTable("GetuserAddressdata", param);
            if (dt.Rows.Count > 0)
            {
                user = new CLayer.User();
                user.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
            }
            return user;
        }


        public CLayer.User GetCountrUser(long userid)
        {
            CLayer.User user = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userid));
            DataTable dt = Connection.GetTable("user_Get", param);
            if (dt.Rows.Count > 0)
            {
                user = new CLayer.User();
                user.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                user.SalutationId = Connection.ToInteger(dt.Rows[0]["SalutationId"]);
                user.FirstName = Connection.ToString(dt.Rows[0]["FirstName"]);
                user.LastName = Connection.ToString(dt.Rows[0]["LastName"]);
                user.DateOfBirth = Connection.ToDate(dt.Rows[0]["DateOfBirth"]);
                user.Sex = Connection.ToInteger(dt.Rows[0]["Sex"]);
                user.UserTypeId = Connection.ToInteger(dt.Rows[0]["UserType"]);
                user.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                user.Email = Connection.ToString(dt.Rows[0]["Email"]);
                user.CreatedDate = Connection.ToDate(dt.Rows[0]["CreatedDate"]);
                user.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                user.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                user.Country = Connection.ToString(dt.Rows[0]["Country"]);
            }
            return user;
        }






        public long Update(CLayer.User data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            //param.Add(Connection.GetParameter("pBusinessname", DataPlug.DataType._Varchar, data.Businessname));
            param.Add(Connection.GetParameter("pFirstName", DataPlug.DataType._Varchar, data.FirstName));
            param.Add(Connection.GetParameter("pLastName", DataPlug.DataType._Varchar, data.LastName));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, data.Email));
            object result = Connection.ExecuteQueryScalar("user_Update", param);
            return Connection.ToLong(result);
        }

        public long Save(CLayer.User data)
        {


            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            if (data.SalutationId == 0)
                param.Add(Connection.GetParameter("pSalutationId", DataPlug.DataType._Int, DBNull.Value));
            else
                param.Add(Connection.GetParameter("pSalutationId", DataPlug.DataType._Int, data.SalutationId));
            // param.Add(Connection.GetParameter("pBusinessName", DataPlug.DataType._Varchar, data.Businessname));
            param.Add(Connection.GetParameter("pFirstName", DataPlug.DataType._Varchar, data.FirstName));
            param.Add(Connection.GetParameter("pLastName", DataPlug.DataType._Varchar, data.LastName));
            param.Add(Connection.GetParameter("pDateOfBirth", DataPlug.DataType._Date, data.DateOfBirth));
            param.Add(Connection.GetParameter("pSex", DataPlug.DataType._Int, data.Sex));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, data.UserTypeId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, data.Email));
            param.Add(Connection.GetParameter("pOPSEmail", DataPlug.DataType._Varchar, data.OPSEmail));
            param.Add(Connection.GetParameter("PMyAssistedBookerId", DataPlug.DataType._BigInt, data.MyAssistedBookerId));

            object result = Connection.ExecuteQueryScalar("user_Save", param);
            return Connection.ToLong(result);
        }

        public void SaveInfo(CLayer.User data, List<long> SbEntities)
        {
            string strgSbEntities = "";
            if (SbEntities != null)
            {
                if (SbEntities.Count > 0)
                {
                    strgSbEntities = string.Join(",", SbEntities);
                }
            }
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, data.Phone));
            param.Add(Connection.GetParameter("pSbEntities", DataPlug.DataType._Varchar, strgSbEntities));
            Connection.ExecuteQueryScalar("user_SaveInfo", param);

        }
        public void SetPassword(long userId, string passwordHash)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pPasswordHash", DataPlug.DataType._Varchar, passwordHash));
            Connection.ExecuteQuery("user_ChangePassword", param);
        }

        public string GetPassword(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            Object obj = Connection.ExecuteQueryScalar("user_GetPassword", param);
            return Connection.ToString(obj);
        }
        public bool IsUniqueEmail(long userId, string email)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, email));
            Object obj = Connection.ExecuteQueryScalar("user_CheckEmail", param);
            return Connection.ToInteger(obj) < 1;
        }

        public void AddUserRole(long userid, long roleid)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userid));
            param.Add(Connection.GetParameter("pRoleId", DataPlug.DataType._Int, roleid));
            Connection.ExecuteQuery("userroles_Save", param);
        }

        public void SetToken(long userid, string token)
        {
            Connection.ExecuteSqlQuery("UPDATE `user` SET Token='" + token + "' WHERE UserId=" + userid.ToString());
        }

        public string GetEmail(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            object email = Connection.ExecuteQueryScalar("user_GetEmail", param);
            return Connection.ToString(email);
        }

        public string GetToken(long userid)
        {
            object obj = Connection.ExecuteSQLQueryScalar("Select Token From user WHERE UserId=" + userid.ToString());
            return Connection.ToString(obj);
        }
        public bool Verify(string token)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pToken", DataPlug.DataType._Varchar, token));
            object obj = Connection.ExecuteQueryScalar("user_Verify", param);
            return Convert.ToBoolean(obj);
        }

        public long GetUserId(string username)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, username));
            object obj = Connection.ExecuteQueryScalar("user_GetUserId", param);
            return Connection.ToLong(obj);
        }

        public long GetUserIdbystatus(string username)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, username));
            object obj = Connection.ExecuteQueryScalar("user_GetUserIdbystatus", param);
            return Connection.ToLong(obj);
        }
        public int GetRole(string email)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, email));
            object obj = Connection.ExecuteQueryScalar("user_GetRole", param);
            return Connection.ToInteger(obj);
        }
        public int GetAssistedBookingFlag(string email)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, email));
            object obj = Connection.ExecuteQueryScalar("user_GetAssistedBookingFlagType", param);
            return Connection.ToInteger(obj);
        }
        //*Added by rahul for getting count of AssistedBooking for login ID
        public long GetCountofMyAssistedBooker(string Id)
        {
            string sql = "select count(*) from user where MyAssistedBookerId = " + Id.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        public  bool  GetApproverStatus(string email)
        {
            string Sql = "Select (CASE WHEN IFNULL(isapprover,0)=0 THEN 'NO' ELSE 'YES' END) AS IsApprover From user WHERE email='" + email.ToString() + "'";
            object obj = Connection.ExecuteSQLQueryScalar(Sql);
            string  result = Connection.ToString(obj);
 
            return (result=="YES"?true:false);
        }

        public decimal GetSupplierPaymentBybookingid(long OfflineBookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOfflineBookingId", DataPlug.DataType._BigInt, OfflineBookingId));
            object obj = Connection.ExecuteQueryScalar("GetSupplierPaymentBybookingid", param);
            return Connection.ToDecimal(obj);
        }

        public int GetRole(long userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));
            object obj = Connection.ExecuteQueryScalar("user_GetRoleById", param);
            return Connection.ToInteger(obj);
        }
        public long GetUserByKey(string Pkey)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pKey", DataPlug.DataType._Varchar, Pkey));
            object UserId = Connection.ExecuteQueryScalar("userkey_Get", param);
            return Connection.ToLong(UserId);
        }
        public long SaveLogins(long UserId, string ProviderKey, string LoginProvider)
        {

            //string commandText = "Insert into userlogins (LoginProvider, ProviderKey, UserId) values (@loginProvider, @providerKey, @userId)";
            //List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            //param.Add(Connection.GetParameter("loginProvider", DataPlug.DataType._Varchar, LoginProvider));
            //param.Add(Connection.GetParameter("providerKey", DataPlug.DataType._Varchar, ProviderKey));
            //param.Add(Connection.GetParameter("userId", DataPlug.DataType._BigInt, UserId));         
            //return Connection.ExecuteQuery(commandText, param);

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            param.Add(Connection.GetParameter("pProviderKey", DataPlug.DataType._Varchar, ProviderKey));
            param.Add(Connection.GetParameter("pLoginProvider", DataPlug.DataType._Varchar, LoginProvider));
            object result = Connection.ExecuteQueryScalar("userlogins_Save", param);
            return Connection.ToLong(result);
        }
        public long SaveUser(CLayer.User data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            if (data.SalutationId == 0)
                param.Add(Connection.GetParameter("pSalutationId", DataPlug.DataType._Int, DBNull.Value));
            else
                param.Add(Connection.GetParameter("pSalutationId", DataPlug.DataType._Int, data.SalutationId));
            // param.Add(Connection.GetParameter("pBusinessName", DataPlug.DataType._Varchar, data.Businessname));
            param.Add(Connection.GetParameter("pFirstName", DataPlug.DataType._Varchar, data.FirstName));
            param.Add(Connection.GetParameter("pLastName", DataPlug.DataType._Varchar, data.LastName));
            param.Add(Connection.GetParameter("pDateOfBirth", DataPlug.DataType._Date, data.DateOfBirth));
            param.Add(Connection.GetParameter("pSex", DataPlug.DataType._Int, data.Sex));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, data.UserTypeId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, data.Email));
            object result = Connection.ExecuteQueryScalar("userexternal_Save", param);
            return Connection.ToLong(result);
        }


        public string GetSalesRegion(long Id)
        {
            object obj = Connection.ExecuteSQLQueryScalar("Select SalesRegion From staff WHERE UserId=" + Id.ToString());
            return Connection.ToString(obj);
        }

        public void SaveGST(CLayer.OfflineBooking data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pOffGSTId", DataPlug.DataType._Int, data.OffGSTId));
            param.Add(Connection.GetParameter("pSubCustomerGstStateId", DataPlug.DataType._Int, data.SubCustomerGstStateId));
            param.Add(Connection.GetParameter("pCustomerId", DataPlug.DataType._Int, data.SubCustomerid));
            param.Add(Connection.GetParameter("pCustomerGstRegNo", DataPlug.DataType._Varchar, data.SubCustomerGstRegNo));
            param.Add(Connection.GetParameter("pCustomerTableType", DataPlug.DataType._Int, data.CustomerTableType));

            param.Add(Connection.GetParameter("pSubCustomerAddress", DataPlug.DataType._Varchar, data.SubCustomerAddress));
            param.Add(Connection.GetParameter("pSubCustomerCity", DataPlug.DataType._BigInt, data.SubCustomerCity));
            param.Add(Connection.GetParameter("pSubCustomerCityname", DataPlug.DataType._Varchar, data.SubCustomerCityname));
            param.Add(Connection.GetParameter("pSubCustomerpinCode", DataPlug.DataType._Varchar, data.SubCustomerpinCode));
            Connection.ExecuteQueryScalar("OfflineBooking_Customer_SaveGST", param);
        }

        public List<CLayer.User> GSTList(int Cust)
        {
            List<CLayer.User> bookings = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pcustid", DataPlug.DataType._Int, Cust));
            DataSet ds = Connection.GetDataSet("GetGSTListForCorporate", param);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                bookings.Add(new CLayer.User()
                {
                    B2bGSTId = Connection.ToLong(dr["OffGSTId"]),
                    GSTstateid = Connection.ToLong(dr["Stateid"]),
                    GSTRegistrationNo = Connection.ToString(dr["GSTRegNO"]),
                    StateOfRegistration = Connection.ToString(dr["StateName"]),
                    //CustomerEmail = Connection.ToString(dr["OfflinebookingCustomerId"]),

                });
            }
            return bookings;
        }

        public void GSTDelete(int B2bGSTId)
        {
            string sql = "Delete From offlinecustomergst_details Where OffGSTId=" + B2bGSTId.ToString();
            Connection.ExecuteSqlQuery(sql);

        }
        public CLayer.GDSUserAddress GDUSUserDetails(long UserID)
        {
            string sql = "SELECT CONCAT(IFNULL(b.firstname,''), '', IFNULL(b.lastname,''))AS Fullname , A.*,B.* FROM address a INNER JOIN USER b ON a.userid=b.userid AND a.userid=" + UserID + " AND a.type=1";

            DataTable dt = Connection.GetSQLTable(sql.ToString());
            CLayer.GDSUserAddress result = null;
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.GDSUserAddress();
                result.GDSUserID = Connection.ToLong(dt.Rows[0]["UserId"]);
                result.FirstName = Connection.ToString(dt.Rows[0]["Firstname"]);
                result.LastName = Connection.ToString(dt.Rows[0]["Lastname"]);
                result.FullName = Connection.ToString(dt.Rows[0]["Fullname"]);
                result.Phone = Connection.ToString(dt.Rows[0]["Phone"]);
                result.Mobile = Connection.ToString(dt.Rows[0]["Mobile"]);
                result.Email = Connection.ToString(dt.Rows[0]["Email"]);
            }

            return result;

        }
       
        public long GetMaximumEntitlement(long userId)
        {
            string Sql =" SELECT IFNULL(MaximumDailyEntitlement, 0) FROM b2b_user WHERE userid='" + userId.ToString() + "'";
            object obj = Connection.ExecuteSQLQueryScalar(Sql);
            return Connection.ToLong(obj);
        }
        //Done By Rahul 24-12-2019
        public List<CLayer.User> GetAllAssistedBooker(int status)
        {
            List<CLayer.User> users = new List<CLayer.User>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            DataTable dt = Connection.GetTable("user_GetAssistedBooker", param);
            foreach (DataRow dr in dt.Rows)
            {
                users.Add(new CLayer.User()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    Salutation = Connection.ToString(dr["Salutation"]),
                    //Businessname = Connection.ToString(dr["Businessname"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    DateOfBirth = Connection.ToDate(dr["DateOfBirth"]),
                    Sex = Connection.ToInteger(dr["Sex"]),
                    UserType = Connection.ToString(dr["UserType"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    Email = Connection.ToString(dr["Email"]),
                    CreatedDate = Connection.ToDate(dr["CreatedDate"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    LastLoginOn = Connection.ToDate(dr["LastLoginOn"])
                });
            }

            return users;
        }
        //public string GetCorporateUserID(string Name)
        //{
        //    string sql = "select userid from user where firstname = '" + Name + "'";
        //    object obj = Connection.ExecuteSQLQueryScalar(sql);
        //    return (Connection.ToString(obj));
        //}
        //----
    }
}
