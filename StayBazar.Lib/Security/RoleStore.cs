using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using StayBazar.Lib.Security.Data;

namespace StayBazar.Lib.Security
{
    /// <summary>
    /// Class that implements the key ASP.NET Identity role store iterfaces
    /// </summary>
    public class RoleStore : IRoleStore<IdentityRole>
    {
        private RoleData roleTable;

        /// <summary>
        /// Default constructor that initializes a new MySQLDatabase
        /// instance using the Default Connection string
        /// </summary>
        public RoleStore()
        {
            roleTable = new RoleData();
        }

        ///// <summary>
        ///// Constructor that takes a MySQLDatabase as argument 
        ///// </summary>
        ///// <param name="database"></param>
        //public RoleStore(MySQLDatabase database)
        //{
        //    Database = database;
        //    roleTable = new RoleTable(database);
        //}

        public Task CreateAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            roleTable.Insert(role);

            return Task.FromResult<object>(null);
        }

        public Task DeleteAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            roleTable.Delete(role.Id);

            return Task.FromResult<Object>(null);
        }

        public Task<IdentityRole> FindByIdAsync(string roleId)
        {
            IdentityRole result = roleTable.GetRoleById(roleId);

            return Task.FromResult<IdentityRole>(result);
        }

        public Task<IdentityRole> FindByNameAsync(string roleName)
        {
            IdentityRole result = roleTable.GetRoleByName(roleName);

            return Task.FromResult<IdentityRole>(result);
        }

        public Task UpdateAsync(IdentityRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("user");
            }

            roleTable.Update(role);

            return Task.FromResult<Object>(null);
        }
        public void Dispose()
        { }
    }
}
