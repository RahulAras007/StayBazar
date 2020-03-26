using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;

namespace StayBazar.Lib.Security.Data
{
    public class RoleData : DataLink
    {
        

        /// <summary>
        /// Deltes a role from the Roles table
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns></returns>
        public int Delete(string roleId)
        {

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pId", DataPlug.DataType._BigInt, Convert.ToInt32(roleId)));

            return Connection.ExecuteQuery("role_Delete", param);
        }

        /// <summary>
        /// Inserts a new Role in the Roles table
        /// </summary>
        /// <param name="roleName">The role's name</param>
        /// <returns>Role Id</returns>
        public int Insert(IdentityRole role)
        {
            //string commandText = "Insert into Roles (Id, Name) values (@id, @name)";
            //Dictionary<string, object> parameters = new Dictionary<string, object>();
            //parameters.Add("@name", role.Name);
            //parameters.Add("@id", role.Id);

            //return Connection.ExecuteSqlQuery(commandText, parameters);
            return SaveRole(role);
        }

        /// <summary>
        /// Returns a role name given the roleId
        /// </summary>
        /// <param name="roleId">The role Id</param>
        /// <returns>Role name</returns>
        public string GetRoleName(string roleId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pId", DataPlug.DataType._Varchar, roleId));

            object val = Connection.ExecuteQueryScalar("role_GetName", param);
            return Connection.ToString(val);
        }

        /// <summary>
        /// Returns the role Id given a role name
        /// </summary>
        /// <param name="roleName">Role's name</param>
        /// <returns>Role's Id</returns>
        public string GetRoleId(string roleName)    
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, roleName));

            object val = Connection.ExecuteQueryScalar("role_GetName", param);
            return Connection.ToString(val);
        }

        
        /// <summary>
        /// Gets the IdentityRole given the role Id
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IdentityRole GetRoleById(string roleId)
        {
            var roleName = GetRoleName(roleId);
            IdentityRole role = null;

            if(roleName != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;

        }

        /// <summary>
        /// Gets the IdentityRole given the role name
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public IdentityRole GetRoleByName(string roleName)
        {
            var roleId = GetRoleId(roleName);
            IdentityRole role = null;

            if (roleId != null)
            {
                role = new IdentityRole(roleName, roleId);
            }

            return role;
        }

        private int SaveRole(IdentityRole role)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pId", DataPlug.DataType._Varchar, role.Id));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, role.Name));
            object val = Connection.ExecuteQueryScalar("role_Save", param);
            return Connection.ToInteger(val);
        }

        public int Update(IdentityRole role)
        {
            return SaveRole(role);
        }
    }
}
