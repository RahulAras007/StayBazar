using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class PropertyTypeController : Controller
    {
        //
        // GET: /PropertyType/
        [Authorize(Roles = "Administrator , Staff")]
        private List<CLayer.PropertyType> InitData()
        {
            List<CLayer.PropertyType> pts = BLayer.PropertyType.GetAll();
            ViewBag.PropertyType = new PropertyTypeModel() { PropertyTypeId = 0, Title = "" };
            return pts;
        }
        [Authorize(Roles = "Administrator , Staff")]
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
        [Authorize(Roles = "Administrator , Staff")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(PropertyTypeModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.PropertyType pt = new CLayer.PropertyType() { PropertyTypeId = data.PropertyTypeId, Title = data.Title };
                    BLayer.PropertyType.Save(pt);
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
        [Authorize(Roles = "Administrator , Staff")]
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Saved = false;
                PropertyTypeModel mbt = new PropertyTypeModel() { PropertyTypeId = 0 };

                CLayer.PropertyType pt = BLayer.PropertyType.Get(id);

                if (pt != null)
                    mbt = new PropertyTypeModel() { PropertyTypeId = pt.PropertyTypeId, Title = pt.Title };

                return PartialView("_Edit", mbt);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Authorize(Roles = "Administrator , Staff")]
        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.PropertyType.Delete(id);
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