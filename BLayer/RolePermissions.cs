using System;
using System.Collections.Generic;


namespace BLayer
{
    public class RolePermissions
    {
        public static List<CLayer.RolePermission> GetBlockedPermissions(int roleId)
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
            return rp.GetBlockedPermissions(roleId);
        }

        public static List<CLayer.RolePermission> GetAllPermissions(int roleId)
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
            return rp.GetAllPermissions(roleId);
        }

        public static List<CLayer.RolePermission> GetAllPermissions()
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
            return rp.GetAllPermissions();
        }

        public static void SaveRolePermission(int roleId, int moduleId)
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
            rp.SaveRolePermission(roleId, moduleId);
        }

        public static void SaveRolePermission(int roleId, List<int> moduleId)
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
            rp.SaveRolePermission(roleId, moduleId);
        }

        public static List<CLayer.Role> GetAllAdminSide()
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
           return rp.GetAllAdminSide();
        }

        public static CLayer.Role GetRole(long id)
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
            return rp.GetRole(id);
        }
        public static long SaveRole(CLayer.Role role)
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
            return rp.SaveRole(role);
        }
        public static void ClearBlockedPermissions(long roleId)
        {
            DataLayer.RolePermissions rp = new DataLayer.RolePermissions();
            rp.ClearBlockedPermissions(roleId);
        }
    }
}
