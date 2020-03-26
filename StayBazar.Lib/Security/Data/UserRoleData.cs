using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;

namespace StayBazar.Lib.Security.Data
{
    public class UserRoleData : DataLink
    {
        /// <summary>
        /// Returns a list of user's roles
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public List<string> FindByUserId(string userId)
        {
            List<string> roles = new List<string>();
            string commandText = "Select Roles.Name from UserRoles, Roles where UserRoles.UserId = '" + userId + "' and UserRoles.RoleId = Roles.Id";

            DataTable dt = Connection.GetSQLTable(commandText);
            foreach (DataRow row in dt.Rows)
            {
                roles.Add(Connection.ToString(row["Name"]));
            }

            return roles;
        }

        /// <summary>
        /// Deletes all roles from a user in the UserRoles table
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            string commandText = "Delete from UserRoles where UserId = '" + userId + "'";

            return Connection.ExecuteSqlQuery(commandText);
        }

        /// <summary>
        /// Inserts a new role for a user in the UserRoles table
        /// </summary>
        /// <param name="user">The User</param>
        /// <param name="roleId">The Role's id</param>
        /// <returns></returns>
        public int Insert(IdentityUser user, string roleId)
        {
            string commandText = "Insert into UserRoles (UserId, RoleId) values ('"+ user.Id + "', "+ roleId.ToString() +")";
            return Connection.ExecuteSqlQuery(commandText);
        }
    }
}
