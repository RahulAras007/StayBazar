using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class PropertyFeatureController : Controller
    {
        //
        // GET: /PropertyFeature/

        private List<CLayer.PropertyFeature> InitData()
        {
            List<CLayer.PropertyFeature> pts = BLayer.PropertyFeature.GetAll();
            ViewBag.PropertyFeature = new PropertyFeatureModel() { PropertyFeatureId = 0, Title = "", Style = "",Showfeatures=false };
            return pts;
        }
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {
                return View(InitData());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Models.PropertyFeatureModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.PropertyFeature pt = new CLayer.PropertyFeature()
                    {
                        PropertyFeatureId = data.PropertyFeatureId,
                        Title = data.Title,
                        Showfeatures = data.Showfeatures,
                        Style = data.Style
                    };
                    BLayer.PropertyFeature.Save(pt);
                    ViewBag.Saved = true;
                }
                else
                    ViewBag.Saved = false;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Saved = false;
                Models.PropertyFeatureModel mbt = new PropertyFeatureModel() { PropertyFeatureId = 0 };

                CLayer.PropertyFeature pt = BLayer.PropertyFeature.Get(id);

                if (pt != null)
                    mbt = new PropertyFeatureModel() { PropertyFeatureId = pt.PropertyFeatureId, Title = pt.Title, Style = pt.Style,Showfeatures=pt.Showfeatures };

                return PartialView("_Edit", mbt);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.RoleRequired(Administrator = true)]
        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.PropertyFeature.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
    }
}