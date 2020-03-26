using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class RolePermissions : DataLink
    {
        public long SaveRole(CLayer.Role role)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pId", DataPlug.DataType._Varchar, role.Id));
            param.Add(Connection.GetParameter("pName", DataPlug.DataType._Varchar, role.Name));
            object val = Connection.ExecuteQueryScalar("role_Save", param);
            return Connection.ToLong(val);
        }

        public void ClearBlockedPermissions(long roleId)
        {
            string sql = "Delete From Blocked_Permissions where RoleId=" + roleId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }

         public void SaveRolePermission(int roleId, int moduleId)
        {
            string sql = "Delete From Blocked_Permissions where ModuleId=" + moduleId.ToString() +" And RoleId=" + roleId.ToString();
            Connection.ExecuteSqlQuery(sql);

            sql = "Insert into Blocked_Permissions(ModuleId,RoleId) Values(" + moduleId.ToString() + "," + roleId.ToString() + ")";
            Connection.ExecuteSqlQuery(sql);

        }

         public void SaveRolePermission(int roleId, List<int> moduleIds)
         {
             ClearBlockedPermissions(roleId);

             if (moduleIds.Count == 0) return;
             string ids = string.Join(",", moduleIds);


             string sql = "Insert into Blocked_Permissions(RoleId,ModuleId)  SELECT " + roleId.ToString() + " as roleId,m.ModuleId  FROM modulenames m WHERE m.ModuleId IN (" + ids + ")";
             Connection.ExecuteSqlQuery(sql);
         }

        public List<CLayer.RolePermission> GetAllPermissions(int roleId)
         {
             string sql = "SELECT m.ModuleId,IFNULL(b.ModuleId,0) AS blocked,ModuleName,Controllers,HeadName,m.HeadingId " +
             " FROM modulenames m " +
            " inner join moduleheading h on m.HeadingId = h.HeadingId " +
            " LEFT JOIN blocked_permissions b ON m.ModuleId = b.ModuleId AND b.RoleId=" + roleId.ToString() +" order by m.HeadingId,ModuleName ";

             DataTable dt = Connection.GetSQLTable(sql);
             List<CLayer.RolePermission> result = new List<CLayer.RolePermission>();
             CLayer.RolePermission rp = null;
             string conts = "";
         //  List<string> csv;

            foreach(DataRow dr in dt.Rows)
            {
                rp =  new CLayer.RolePermission();
                rp.ModuleId = Connection.ToInteger(dr["ModuleId"]);
                rp.ModuleName = Connection.ToString(dr["ModuleName"]);
                rp.HasAccess = (Connection.ToInteger(dr["blocked"]) == 0); // non-zero means it is in blocked permission list
                rp.Heading = Connection.ToString(dr["HeadName"]);
                rp.HeadingId = Connection.ToLong(dr["HeadingId"]);
                conts = Connection.ToString(dr["Controllers"]);
                //if(conts!="")
                //{
                //    csv = new List<string>(conts.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                //}
                //else { csv = new List<string>(); }
                rp.Controllers = conts;
                result.Add(rp);
            }

            return result;
         }

        public List<CLayer.RolePermission> GetAllPermissions()
        {
            string sql = "SELECT m.ModuleId,ModuleName,Controllers,HeadName,m.HeadingId " +
            " FROM modulenames m " +
           " inner join moduleheading h on m.HeadingId = h.HeadingId order by m.HeadingId , ModuleName";

            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.RolePermission> result = new List<CLayer.RolePermission>();
            CLayer.RolePermission rp = null;
            string conts = "";
            //  List<string> csv;

            foreach (DataRow dr in dt.Rows)
            {
                rp = new CLayer.RolePermission();
                rp.ModuleId = Connection.ToInteger(dr["ModuleId"]);
                rp.ModuleName = Connection.ToString(dr["ModuleName"]);
                rp.HasAccess =true; // non-zero means it is in blocked permission list
                rp.Heading = Connection.ToString(dr["HeadName"]);
                rp.HeadingId = Connection.ToLong(dr["HeadingId"]);
                conts = Connection.ToString(dr["Controllers"]);
                //if(conts!="")
                //{
                //    csv = new List<string>(conts.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                //}
                //else { csv = new List<string>(); }
                rp.Controllers = conts;
                result.Add(rp);
            }

            return result;
        }

        public List<CLayer.RolePermission> GetBlockedPermissions(int roleId)
        {
            string sql = "SELECT m.ModuleId,IFNULL(b.ModuleId,0) AS blocked,ModuleName,Controllers,HeadName,m.HeadingId " +
            " FROM modulenames m " +
            " inner join moduleheading h on m.HeadingId = h.HeadingId " +
            " INNER JOIN blocked_permissions b ON m.ModuleId = b.ModuleId AND b.RoleId=" + roleId.ToString() + " order by m.ModuleId ";

            DataTable dt = Connection.GetSQLTable(sql);
            List<CLayer.RolePermission> result = new List<CLayer.RolePermission>();
            CLayer.RolePermission rp = null;
            string conts = "";
            //  List<string> csv;

            foreach (DataRow dr in dt.Rows)
            {
                rp = new CLayer.RolePermission();
                rp.ModuleId = Connection.ToInteger(dr["ModuleId"]);
                rp.ModuleName = Connection.ToString(dr["ModuleName"]);
                rp.HasAccess = (Connection.ToInteger(dr["blocked"]) == 0); // non-zero means it is in blocked permission list
                rp.Heading = Connection.ToString(dr["HeadName"]);
                rp.HeadingId = Connection.ToLong(dr["HeadingId"]);
                conts = Connection.ToString(dr["Controllers"]);
                //if(conts!="")
                //{
                //    csv = new List<string>(conts.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                //}
                //else { csv = new List<string>(); }
                rp.Controllers = conts;
                result.Add(rp);
            }

            return result;
        }


        public List<CLayer.Role> GetAllAdminSide()
        {
            string sql = "SELECT * FROM roles WHERE Id < 3 OR Id = 9 OR Id > 11";
            DataTable dt = Connection.GetSQLTable(sql);

            List<CLayer.Role> result = new List<CLayer.Role>();
            
            foreach(DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Role() { Id= Connection.ToLong(dr["Id"]), Name = Connection.ToString(dr["name"])  });
            }
            return result;
        }

        public CLayer.Role GetRole(long id)
        {
            String sql = "Select * from Roles Where Id=" + id.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count == 0) return null;

            CLayer.Role r = new CLayer.Role();
            r.Id = Connection.ToLong(dt.Rows[0]["Id"]);
            r.Name = Connection.ToString(dt.Rows[0]["Name"]);

            return r;

        }
    }
}
