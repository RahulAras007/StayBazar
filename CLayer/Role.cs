using System;
using System.Collections.Generic;

namespace CLayer
{
    public class Role
    {
        public const string ADMINISTRATOR = "Administrator";
        public const string STAFF = "Staff";
        public const string SUPPLIER = "Supplier";
        public const string AGENT = "Agent";
        public const string CORPORATE = "Corporate";
        public const string CORPORATE_USER = "CorporateUser";
        public const string CUSTOMER = "Customer";
        public const string AFFLIATE = "Affiliate";

        public long Id { get; set; }
        public string Name { get; set; }

        public enum Roles
        {
            Administrator = 1,
            Staff = 2,
            Supplier = 3,
            Agent = 4,
            Corporate = 5,
            CorporateUser = 6,
            Customer = 7,
            Affiliate = 8,
            SalesPerson=9
        };

        public enum CorporateRoles
        {
            Administrator = 1,
            Staff = 2
        };
        public enum B2BRoles
        {
            Administrator = 1,
            Staff = 2,
            Supplier =3
        }
        public static Roles GetRoleForSearch(Roles currentRole)
        {
            if (currentRole == Roles.CorporateUser || currentRole == Roles.Corporate) 
                currentRole = Roles.Corporate;
           // if (currentRole != Roles.Corporate && currentRole != Roles.Agent && currentRole != Roles.Supplier && currentRole != Roles.Customer)
            else
                currentRole = Roles.Customer;
            return currentRole;
        }
    }
}
