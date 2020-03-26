using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    
    public class RolesController : Controller
    {
        // GET: Admin/Roles
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            return View(BLayer.RolePermissions.GetAllAdminSide());
        }
        [Common.AdminRolePermission]
        public ActionResult Edit(long id)
        {
            try{
                ViewBag.Head = "Roles - Edit";
                Models.RoleModel rle = new Models.RoleModel();
                CLayer.Role rp = BLayer.RolePermissions.GetRole(id);
                rle.Id = rp.Id;
                rle.Name = rp.Name;
                rle.PermissionIds = "";
                rle.Permissions = BLayer.RolePermissions.GetAllPermissions((int) id);

                foreach( CLayer.RolePermission r in rle.Permissions)
                {
                    if(!r.HasAccess)
                    {
                        if(rle.PermissionIds== "")
                        {
                            rle.PermissionIds = r.ModuleId.ToString();
                        }
                        else
                        {
                            rle.PermissionIds = rle.PermissionIds +","+ r.ModuleId.ToString();
                        }
                    }
                }
                return View(rle);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult New()
        {
            try
            {
                ViewBag.Head = "Roles - Edit";
                Models.RoleModel rle = new Models.RoleModel();
                rle.Id = 0;
                rle.Name = "";
                rle.PermissionIds = "";
                rle.Permissions = BLayer.RolePermissions.GetAllPermissions(); ;
               
                return View("Edit",rle);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Save(Models.RoleModel data)
        {
            try
            {

                CLayer.Role rp = new CLayer.Role();
                rp.Id = data.Id;
                rp.Name = data.Name;
                long roleId = BLayer.RolePermissions.SaveRole(rp);
                string ids = data.PermissionIds;
                if (ids == null) ids = "";
                if (ids != "") ids = ids.Trim();

                if(ids == "")
                {
                    BLayer.RolePermissions.ClearBlockedPermissions(roleId);
                }else
                {
                    string[] id = ids.Split(new char[]{','});
                    List<int> pids = new List<int>();
                    int j = 0;
                    for (int i = 0; i < id.Length;i++ )
                    {
                        j = 0;
                        if(int.TryParse(id[i], out j))
                        {
                            pids.Add(j);
                        }
                    }
                    if (pids.Count > 0)
                    {
                        BLayer.RolePermissions.SaveRolePermission((int)roleId, pids);
                    }
                    else
                    {
                        BLayer.RolePermissions.ClearBlockedPermissions(roleId);
                    }

                }
                
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
           return  RedirectToAction("Index");
        }

        
    }
}