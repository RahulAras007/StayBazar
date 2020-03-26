using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using Microsoft.AspNet.Identity;

namespace StayBazar.Lib.Security.Data
{
    public class UserData : DataLayer.DataProvider.DataLink
    {
        /// <summary>
        /// Returns the user's name given a user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetUserName(string userId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pId", DataPlug.DataType._BigInt, Convert.ToInt64(userId)));
            object val = Connection.ExecuteQueryScalar("", param);
            return Connection.ToString(val);
        }

        /// <summary>
        /// Returns a User ID given a user name
        /// </summary>
        /// <param name="userName">The user's name</param>
        /// <returns></returns>
        public string GetUserId(string userName)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, userName));
            object obj = Connection.ExecuteQueryScalar("user_GetUserId", param);
            return Connection.ToString(obj);    
        }

        /// <summary>
        /// Returns an IdentityUser given the user's id
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public IdentityUser GetUserById(string userId)
        {
            IdentityUser user = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pId", DataPlug.DataType._Varchar, userId));
            DataTable dt = Connection.GetTable("user_GetUser1", param);


            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                user = new IdentityUser();
                user.Id = Connection.ToString(row["UserId"]);
                user.UserName = Connection.ToString(row["Email"]);
                user.PasswordHash = Connection.ToString(row["PasswordHash"]) == "" ? null : Connection.ToString(row["PasswordHash"]);
                if (user.PasswordHash == null)
                {
                    user.PasswordHash = "";
                }

                user.SecurityStamp = Connection.ToString(row["SecurityStamp"]) == "" ? null : Connection.ToString(row["SecurityStamp"]);
            }

            return user;
        }

        /// <summary>
        /// Returns a list of IdentityUser instances given a user name
        /// </summary>
        /// <param name="userName">User's name</param>
        /// <returns></returns>
        public List<IdentityUser> GetUserByName(string userName)
        {
            List<IdentityUser> users = new List<IdentityUser>();
          //  string commandText = "Select * from `User` where Email ='" + userName + "'";
            if (userName == null || userName =="") return users;
            if (userName.Length > 100) userName = userName.Substring(0, 99);
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, userName));
            DataTable dt = Connection.GetTable("user_GetUserByName",param);
            foreach (DataRow row in dt.Rows)
            {
                IdentityUser user = new IdentityUser();
                user.Id = Connection.ToString(row["UserId"]);
                user.UserName = Connection.ToString(row["Email"]);
                user.PasswordHash = Connection.ToString(row["PasswordHash"]) == "" ? null : Connection.ToString(row["PasswordHash"]);
                if (user.PasswordHash == null)
                {
                    user.PasswordHash = "";
                }
                user.SecurityStamp = Connection.ToString(row["SecurityStamp"]) == "" ? null : Connection.ToString(row["SecurityStamp"]);
                users.Add(user);
            }

            return users;
        }

        /// <summary>
        /// Return the user's password hash
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public string GetPasswordHash(string userId)
        {
            string commandText = "Select PasswordHash from `User` where UserId = '" + userId + "'";
            string passHash = Connection.ToString(Connection.ExecuteSQLQueryScalar(commandText));
            if (passHash=="")
            {
                return null;
            }

            return passHash;
        }

        /// <summary>
        /// Sets the user's password hash
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public int SetPasswordHash(string userId, string passwordHash)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("vUserId", DataPlug.DataType._BigInt, Convert.ToInt64(userId)));
            param.Add(Connection.GetParameter("vPasswordHash", DataPlug.DataType._Varchar, passwordHash));
            return Connection.ExecuteQuery("security_SetPasswordHash", param);
        }

        /// <summary>
        /// Returns the user's security stamp
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public string GetSecurityStamp(string userId)
        {
            string commandText = "Select SecurityStamp from `User` where  UserId = '" + userId + "'";

            string result = Connection.ToString(Connection.ExecuteSQLQueryScalar(commandText));
            return result;
        }

        /// <summary>
        /// Inserts a new user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Insert(IdentityUser user)
        {
            user.Id = "0";
            return Save(user);
        }

        private int Save(IdentityUser user)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, Convert.ToInt64(user.Id)));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, user.UserName));
            param.Add(Connection.GetParameter("pPasswordHash", DataPlug.DataType._Varchar, user.PasswordHash));
            param.Add(Connection.GetParameter("pSecStamp", DataPlug.DataType._Varchar, user.SecurityStamp));

            return Connection.ExecuteQuery("security_UserSave", param);
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        private int Delete(string userId)
        {
            string commandText = "Delete from `User` where UserId ='" + userId + "'";
            return Connection.ExecuteSqlQuery(commandText);
        }

        /// <summary>
        /// Deletes a user from the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Delete(IdentityUser user)
        {
            return Delete(user.Id);
        }

        /// <summary>
        /// Updates a user in the Users table
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Update(IdentityUser user)
        {
            return Save(user);
        }
    }
}
