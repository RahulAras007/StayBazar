using System;
using System.Collections.Generic;
using System.Data;


namespace BLayer
{
    public class User
    {
        public static void SetLoginDate(long userId, string dateVal)
        {
            DataLayer.User user = new DataLayer.User();
            user.SetLoginDate(userId, dateVal);
        }
        public static void SetName(long userId, string firstName, string lastName)
        {
            DataLayer.User user = new DataLayer.User();
            user.SetName(userId, firstName, lastName);
        }
        public static CLayer.ObjectStatus.StatusType GetStatus(long userId)
        {
            DataLayer.User user = new DataLayer.User();
            return (CLayer.ObjectStatus.StatusType)user.GetStatus(userId);
        }
        public static List<CLayer.User> GetAllStaff(int status)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllStaff(status);
        }
        
        public static List<CLayer.User> GetAllSalesperson()
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllSalesperson();
        }
        public static List<CLayer.User> GetAllSalespersonandRegionalmanager()
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllSalespersonandRegionalmanager();
        }

        public static long CreateGuest(string email, string phone)
        {
            DataLayer.User user = new DataLayer.User();
            return user.CreateGuest(email, phone, CLayer.ObjectStatus.StatusType.NotVerified, CLayer.Role.Roles.Customer, CLayer.Address.AddressTypes.Primary);
        }

        public static List<CLayer.User> GetAllCustomerForSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllCustomerForSearch(status, searchString, searchItem, userType, start, limit);
        }

        public static List<CLayer.User> GetAllB2BForSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllB2BForSearch(status, searchString, searchItem, userType, start, limit);
        }

        //Excel report
        public static DataTable GetAllB2BForExcel(int status, string searchString, int searchItem, int userType)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllB2BForExcel(status, searchString, searchItem, userType);
        }

        public static List<CLayer.User> GetAllPropertyStatusWithType(int statusfor, int status, int Type, string searchString, int searchItem, int userType, int start, int limit)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllPropertyStatusWithType(statusfor, status, Type, searchString, searchItem, userType, start, limit);
        }
        public static List<CLayer.User> GetAllPropertyStatus(int statusfor, int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllPropertyStatus(statusfor, status, searchString, searchItem, userType, start, limit);
        }
        public static bool IsUniqueEmail(long userId, string email)
        {
            DataLayer.User user = new DataLayer.User();
            return user.IsUniqueEmail(userId, email);
        }
        public static int GetRole(string email)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetRole(email);
        }
        //Done by rahul for getting AssistedBookingFlag on 16-01-2020
        public static int GetAssistedBookingFlag(string email)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAssistedBookingFlag(email);
        }
        //*GetCountofMyAssistedBooker 
        public static long GetCountofMyAssistedBooker(string id)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetCountofMyAssistedBooker(id);
        }
        public static CLayer.Role.Roles GetRole(long userId)
        {
            DataLayer.User user = new DataLayer.User();
            int i = user.GetRole(userId);
            if (i == 0) i = 1;
            return (CLayer.Role.Roles)i;
        }

        public static CLayer.User Get(long userid)
        {
            DataLayer.User user = new DataLayer.User();
            return user.Get(userid);
        }
        public static CLayer.User getuserAddressdata(long userid)
        {
            DataLayer.User user = new DataLayer.User();
            return user.getuserAddressdata(userid);
        }
        public static CLayer.User GetCountrUser(long userid)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetCountrUser(userid);
        }

        public static long Save(CLayer.User data)
        {
            DataLayer.User user = new DataLayer.User();
            data.Validate();
            return user.Save(data);
        }
        public static void SaveInfo(CLayer.User data, List<long> SbEntities)
        {
            DataLayer.User user = new DataLayer.User();
            user.SaveInfo(data, SbEntities);
        }

        public static long Update(CLayer.User data)
        {
            DataLayer.User user = new DataLayer.User();
            data.Validate();
            return user.Update(data);

        }
        /// <summary>
        /// Set Status with Delete Date
        /// </summary>
        /// <param name="userids"></param>
        /// <param name="status"></param>
        public static void SetStatus(string userids, int status)
        {
            DataLayer.User user = new DataLayer.User();
            user.SetStatus(userids, status);
        }
        public static void SaveSalesRegion(long userids, string SalesRegion)
        {
            DataLayer.User user = new DataLayer.User();
            user.SaveSalesRegion(userids, SalesRegion);
        }
        public static void SetDeleteStatus(long userId)
        {
            DataLayer.User user = new DataLayer.User();
            user.SetDeleteStatus(userId, (int)CLayer.ObjectStatus.StatusType.Deleted);
        }
        public static void SetCustomerPaymentMode(long userId, int CustomerPaymentMode, decimal CustomerPaymentModeCreditDays = 0)
        {
            DataLayer.User user = new DataLayer.User();
            user.SetCustomerPaymentMode(userId, CustomerPaymentMode, CustomerPaymentModeCreditDays);
        }

        public static void SetPassword(long UserId, string PasswordHash)
        {
            DataLayer.User user = new DataLayer.User();
            user.SetPassword(UserId, PasswordHash);
        }

        public static string GetPassword(long userId)
        {
            DataLayer.User usr = new DataLayer.User();
            return usr.GetPassword(userId);
        }

        public static void AddUserRole(long userid, long roleid)
        {
            DataLayer.User user = new DataLayer.User();
            user.AddUserRole(userid, roleid);
        }

        public static void SetToken(long userid, string token)
        {
            DataLayer.User user = new DataLayer.User();
            user.SetToken(userid, token);
        }

        public static string GetToken(long userid)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetToken(userid);
        }
        public static bool Verify(string token)
        {
            DataLayer.User user = new DataLayer.User();
            return user.Verify(token);
        }
        public static string GetEmail(long userId)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetEmail(userId);
        }
        public static long GetUserId(string username)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetUserId(username);
        }

        public static long GetUserIdbystatus(string username)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetUserIdbystatus(username);
        }
        public static long GetUserByKey(string Pkey)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetUserByKey(Pkey);
        }
        public static long SaveLogins(long UserId, string ProviderKey, string LoginProvider)
        {
            DataLayer.User user = new DataLayer.User();
            return user.SaveLogins(UserId, ProviderKey, LoginProvider);
        }
        public static long SaveUser(CLayer.User data)
        {
            DataLayer.User user = new DataLayer.User();
            data.Validate();
            return user.SaveUser(data);
        }
        public static string GetSalesRegion(long id)
        {
            DataLayer.User usr = new DataLayer.User();
            return usr.GetSalesRegion(id);
        }


        public static void SaveGST(CLayer.OfflineBooking data)
        {
            DataLayer.User usr = new DataLayer.User();
            usr.SaveGST(data);
        }

        public static List<CLayer.User> GSTList(int status)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GSTList(status);
        }

        public static void GSTDelete(int B2bGSTId)
        {
            DataLayer.User user = new DataLayer.User();
            user.GSTDelete(B2bGSTId);
        }
        public static CLayer.GDSUserAddress GDUSUserDetails(long UserID)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GDUSUserDetails(UserID);
        }
        public static bool GetApproverStatus(string email)
        {
            DataLayer.User usr = new DataLayer.User();
            return usr.GetApproverStatus(email);
        }
        public static long GetMaximumEntitlement(long pUserID)
        {
            DataLayer.User usr = new DataLayer.User();
            return usr.GetMaximumEntitlement(pUserID);
        }
        //Done By Rahul 24-12-2019
        public static List<CLayer.User> GetAllAssistedBooker(int status)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllAssistedBooker(status);
        }
        //public static string GetCorporateUserID(string Name)
        //{
        //    DataLayer.User bi = new DataLayer.User();
        //    return bi.GetCorporateUserID(Name);
        //}


    }
}
