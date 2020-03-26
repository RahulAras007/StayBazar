using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Controllers
{
    public class PropertyListController : Controller
    {
        //
        // GET: /PropertyList/
        [Common.RoleRequired(Supplier=true)]
        public ActionResult Index()
        {
            try
            {
                List<CLayer.Property> propertylist = new List<CLayer.Property>();
                long ownerId = BLayer.User.GetUserId(User.Identity.Name);
                propertylist = BLayer.Property.GetAll(ownerId);
                return View(propertylist);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
        [Common.RoleRequired(Supplier = true)]
        public ActionResult Delete(long id)
        {
            try
            {
                if(id< 1)
                {
                    return RedirectToAction("Index");
                }
                long uid = 0;
                long.TryParse(User.Identity.GetUserId(), out uid);
                if(BLayer.Property.IsOwnerProperty(id, uid))
                {
                    BLayer.Property.Delete(id);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
            return RedirectToAction("Index");
        }
    }
}