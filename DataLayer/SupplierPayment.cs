using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{

    public class SupplierPayment : DataProvider.DataLink
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
        public CLayer.SupplierPayment GetPaymentdetails(string refId)
        {
            CLayer.SupplierPayment user = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pRefId", DataPlug.DataType._Varchar, refId));
            DataTable dt = Connection.GetTable("OfflineBooking_GetAllByRefid", param);
            if (dt.Rows.Count > 0)
            {
                user = new CLayer.SupplierPayment();

                user.Buyprice = Connection.ToDouble(dt.Rows[0]["TotalBuyPrice"]);
                user.propertyname = Connection.ToString(dt.Rows[0]["PropertyName"]);
                user.propertycity = Connection.ToString(dt.Rows[0]["PropertyCityname"]);
                //     FirstName = Connection.ToString(dr["FirstName"]),
                //  LastName = Connection.ToString(dr["LastName"]),
                user.SupplierEmail = Connection.ToString(dt.Rows[0]["SupplierEmail"]);
                user.SupplierName = Connection.ToString(dt.Rows[0]["SupplierName"]);
                user.Buyprice = Connection.ToDouble(dt.Rows[0]["TotalBuyPrice"]);
                user.AmountPaid = Connection.ToDouble(dt.Rows[0]["amount"]);
                user.SupplierMobile = Connection.ToString(dt.Rows[0]["SupplierMobile"]);
                user.OrderId = Connection.ToString(dt.Rows[0]["Orderno"]);
                user.CheckIn_date = Connection.ToDate(dt.Rows[0]["CheckIn_date"]);
                user.CustomerName = Connection.ToString(dt.Rows[0]["CustName"]);
                //   DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                user.CheckOut_date = Connection.ToDate(dt.Rows[0]["CheckOut_date"]);
                user.BookingDate = Connection.ToDate(dt.Rows[0]["BookingDate"]);
                user.supplierId = Connection.ToLong(dt.Rows[0]["SupplierId"]);
                user.supplierPayment = Connection.ToBoolean(dt.Rows[0]["supplierPayment"]);


            }
            return user;
        }
        public List<CLayer.SupplierPayment> GetAllCustomerForSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            List<CLayer.SupplierPayment> users = new List<CLayer.SupplierPayment>();
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
                users.Add(new CLayer.SupplierPayment()
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
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    OrderId = Connection.ToString(dr["OrderNo"])
                });
            }

            return users;
        }
        public List<CLayer.SupplierPayment> GetAllB2BForSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            List<CLayer.SupplierPayment> users = new List<CLayer.SupplierPayment>();
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
                users.Add(new CLayer.SupplierPayment()
                {
                    UserId = Connection.ToLong(dr["UserId"]),
                    // Salutation = Connection.ToString(dr["Salutation"]),
                    propertyname = Connection.ToString(dr["Title"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Email = Connection.ToString(dr["Email"]),
                    SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    SupplierName = Connection.ToString(dr["SupplierName"]),

                    City = Connection.ToString(dr["City"]),
                    State = Connection.ToString(dr["State"]),
                    Country = Connection.ToString(dr["Country"]),
                    Phone = Connection.ToString(dr["Phone"]),
                    Mobile = Connection.ToString(dr["Mobile"]),
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    PANNo = Connection.ToString(dr["PANNo"]),
                    UserCode = Connection.ToString(dr["UserCode"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }

            return users;
        }

        public List<CLayer.SupplierPayment> GetAllSupllierPaymentVoucher(long SupplierPaymentId)
        {
            List<CLayer.SupplierPayment> users = new List<CLayer.SupplierPayment>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierPaymentID", DataPlug.DataType._Int, SupplierPaymentId));

            DataSet ds = Connection.GetDataSet("supplierpaymentvoucher_GetAllBySupplierPaymentID", param);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                users.Add(new CLayer.SupplierPayment()
                {
                    grossAmount = Connection.ToDecimal(dr["grossAmount"]),
                    tdsAmount = Connection.ToDecimal(dr["tdsAmount"]),
                    netAmtPaid = Connection.ToDecimal(dr["netAmtPaid"]),
                    modeofPayment = Connection.ToString(dr["modeofPayment"]),
                    BAnk = Connection.ToString(dr["bank"]),
                    NoOfPaxChild = Connection.ToLong(dr["NoOfPaxChild"]),
                    NoOfPaxAdult = Connection.ToLong(dr["NoOfPaxAdult"]),
                    NoofRooms = Connection.ToLong(dr["NoofRooms"]),
                    Guestname = Connection.ToString(dr["Guestname"]),
                    // Salutation = Connection.ToString(dr["Salutation"]),
                    propertyname = Connection.ToString(dr["PropertyName"]),
                    propertycity = Connection.ToString(dr["propertycity"]),
                    Region = Connection.ToString(dr["Region"]),
                    Amount = Connection.ToDecimal(dr["Amount"]),
                    SupplierCity = Connection.ToString(dr["SupplierCity"]),
                    //     FirstName = Connection.ToString(dr["FirstName"]),
                    //  LastName = Connection.ToString(dr["LastName"]),
                    SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    Buyprice = Connection.ToDouble(dr["TotalBuyPrice"]),

                    pdtPayment= Connection.ToDate(dr["dtPayment"]),
                    AmountPaid = Connection.ToDouble(dr["amount"]),
                    SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    OrderId = Connection.ToString(dr["Orderno"]),
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    //   DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    NoofDays = Connection.ToLong(dr["Noofnight"]),
                    Payamount = Connection.ToDecimal(dr["PayAmount"])
                    //  Status = Connection.ToInteger(dr["Status"]),
                    // PANNo = Connection.ToString(dr["PANNo"]),
                    // UserCode = Connection.ToString(dr["UserCode"]),

                });
            }

            return users;
        }
        public List<CLayer.SupplierPayment> GetAllSupllierPaymentSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            List<CLayer.SupplierPayment> users = new List<CLayer.SupplierPayment>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, searchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, searchItem));
            //       param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, userType));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, limit));
            DataSet ds = Connection.GetDataSet("OfflineBookingSupplierpayment_GetSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                users.Add(new CLayer.SupplierPayment()
                {

                    // Salutation = Connection.ToString(dr["Salutation"]),
                    propertyname = Connection.ToString(dr["PropertyName"]),
                    //     FirstName = Connection.ToString(dr["FirstName"]),
                    //  LastName = Connection.ToString(dr["LastName"]),
                    SupplierEmail = Connection.ToString(dr["SupplierEmail"]),
                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    Buyprice = Connection.ToDouble(dr["TotalBuyPrice"]),
                    supplierPayment = Connection.ToBoolean(dr["supplierPayment"]),
                    AmountPaid = Connection.ToDouble(dr["amount"]),
                    SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    OrderId = Connection.ToString(dr["Orderno"]),
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    //   DeletedDate = Connection.ToDate(dr["DeletedDate"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    //  Status = Connection.ToInteger(dr["Status"]),
                    // PANNo = Connection.ToString(dr["PANNo"]),
                    // UserCode = Connection.ToString(dr["UserCode"]),
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
                    Location = Connection.ToString(dr["Location"])
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
        public long SaveSupplierPaymentDetails(CLayer.SupplierPayment data)
        {


            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierPaymentId", DataPlug.DataType._BigInt, data.supplierPaymentId));

            // param.Add(Connection.GetParameter("pBusinessName", DataPlug.DataType._Varchar, data.Businessname));
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, data.supplierId));
            param.Add(Connection.GetParameter("pOfflineSupplierEmail", DataPlug.DataType._Varchar, data.SupplierEmail));
            param.Add(Connection.GetParameter("pbookingreference", DataPlug.DataType._Varchar, data.OrderId));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pSupplierPayment", DataPlug.DataType._Bool, data.supplierPayment));

            param.Add(Connection.GetParameter("pstatus", DataPlug.DataType._Bool, data.Status));

            object result = Connection.ExecuteQueryScalar("SupplierPaymentDetails_Save", param);
            return Connection.ToLong(result);
        }
        public long Save(CLayer.SupplierPayment data)
        {


            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierPaymentId", DataPlug.DataType._BigInt, data.supplierPaymentId));

            // param.Add(Connection.GetParameter("pBusinessName", DataPlug.DataType._Varchar, data.Businessname));
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, data.FirstName));
            param.Add(Connection.GetParameter("pOfflineSupplierEmail", DataPlug.DataType._Varchar, data.SupplierEmail));
            param.Add(Connection.GetParameter("pbookingreference", DataPlug.DataType._Varchar, data.OrderId));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pSupplierPayment", DataPlug.DataType._Bool, data.supplierPayment));
            param.Add(Connection.GetParameter("pdtPayment", DataPlug.DataType._DateTime, data.pdtPayment));
            param.Add(Connection.GetParameter("pgrossAmount", DataPlug.DataType._Decimal, data.grossAmount));
            param.Add(Connection.GetParameter("ptdsAmount", DataPlug.DataType._Decimal, data.tdsAmount));
            param.Add(Connection.GetParameter("pnetAmtPaid", DataPlug.DataType._Decimal, data.netAmtPaid));
            param.Add(Connection.GetParameter("pmodeofPayment", DataPlug.DataType._Varchar, data.modeofPayment));
            param.Add(Connection.GetParameter("pbank", DataPlug.DataType._Varchar, data.BAnk));


            object result = Connection.ExecuteQueryScalar("SupplierPayment_Save", param);
            return Connection.ToLong(result);
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
    }

}
