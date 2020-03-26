using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Security.Claims;

namespace StayBazar.Lib.Security.Data
{
    public class UserClaimsData : DataLink
    {
        /// <summary>
        /// Returns a ClaimsIdentity instance given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public ClaimsIdentity FindByUserId(string userId)
        {
            ClaimsIdentity claims = new ClaimsIdentity();
            string commandText = "Select * from UserClaims where UserId = '"+ userId +"'";

            DataTable dt = Connection.GetSQLTable(commandText);
            foreach (DataRow row in dt.Rows)
            {
                Claim claim = new Claim(Connection.ToString(row["ClaimType"]), Connection.ToString(row["ClaimValue"]));
                claims.AddClaim(claim);
            }

            return claims;
        }

        /// <summary>
        /// Deletes all claims from a user given a userId
        /// </summary>
        /// <param name="userId">The user's id</param>
        /// <returns></returns>
        public int Delete(string userId)
        {
            string commandText = "Delete from UserClaims where UserId ='"+ userId +"'";

            return Connection.ExecuteSqlQuery(commandText);
        }

        /// <summary>
        /// Inserts a new claim in UserClaims table
        /// </summary>
        /// <param name="userClaim">User's claim to be added</param>
        /// <param name="userId">User's id</param>
        /// <returns></returns>
        public int Insert(Claim userClaim, string userId)
        {
            string commandText = "Insert into UserClaims (ClaimValue, ClaimType, UserId) values (@value, @type, @userId)";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@value", userClaim.Value);
            parameters.Add("@type", userClaim.Type);
            parameters.Add("@userId", userId);

            return Connection.ExecuteSqlQuery(commandText, parameters);
        }

        /// <summary>
        /// Deletes a claim from a user 
        /// </summary>
        /// <param name="user">The user to have a claim deleted</param>
        /// <param name="claim">A claim to be deleted from user</param>
        /// <returns></returns>
        public int Delete(IdentityUser user, Claim claim)
        {
            string commandText = "Delete from UserClaims where UserId = @userId and @ClaimValue = @value and ClaimType = @type";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("@userId", user.Id);
            parameters.Add("@value", claim.Value);
            parameters.Add("@type", claim.Type);

            return Connection.ExecuteSqlQuery(commandText, parameters);
        }
    }
}
