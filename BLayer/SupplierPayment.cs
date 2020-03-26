using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
   
    public class SupplierPayment
    {
      

   
        public static CLayer.SupplierPayment GetPaymentdetails(string refID)
        {
            DataLayer.SupplierPayment user = new DataLayer.SupplierPayment();
            return user.GetPaymentdetails(refID);
        }
        public static List<CLayer.User> GetAllB2BForSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetAllB2BForSearch(status, searchString, searchItem, userType, start, limit);
        }
        public static List<CLayer.SupplierPayment> GetAllSupllierPaymentVoucher( long SupplierPaymentId)
        {
            DataLayer.SupplierPayment user = new DataLayer.SupplierPayment();
            return user.GetAllSupllierPaymentVoucher(SupplierPaymentId);
        }
        public static List<CLayer.SupplierPayment> GetAllSupllierPaymentSearch(int status, string searchString, int searchItem, int userType, int start, int limit)
        {
            DataLayer.SupplierPayment user = new DataLayer.SupplierPayment();
            return user.GetAllSupllierPaymentSearch(status, searchString, searchItem, userType, start, limit);
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
        public static decimal GetSupplierPaymentBybookingid(long OfflineBookingId)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetSupplierPaymentBybookingid(OfflineBookingId);
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

        public static CLayer.User GetCountrUser(long userid)
        {
            DataLayer.User user = new DataLayer.User();
            return user.GetCountrUser(userid);
        }
        public static long SaveSupplierPayment(CLayer.SupplierPayment data)
        {
            DataLayer.SupplierPayment user = new DataLayer.SupplierPayment();
          
            return user.Save(data);
        }

        public static long SaveSupplierPaymentDetails(CLayer.SupplierPayment data)
        {
            DataLayer.SupplierPayment user = new DataLayer.SupplierPayment();

            return user.SaveSupplierPaymentDetails(data);
        }
        public static long Save(CLayer.User data)
        {
            DataLayer.User user = new DataLayer.User();
            data.Validate();
            return user.Save(data);
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
        //public static void SaveSalesRegion(long userids, string SalesRegion)
        //{
        //    DataLayer.User user = new DataLayer.User();
        //    user.SaveSalesRegion(userids, SalesRegion);
        //}
        public static void SetDeleteStatus(long userId)
        {
            DataLayer.User user = new DataLayer.User();
            user.SetDeleteStatus(userId, (int)CLayer.ObjectStatus.StatusType.Deleted);
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
    }
}
