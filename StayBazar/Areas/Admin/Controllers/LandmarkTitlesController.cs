using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class LandmarkTitlesController : Controller
    {
        //
        // GET: /Admin/LandmarkTitles/
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Index()
        {
            try
            {
                List<CLayer.LandmarkTitles> titles = BLayer.LandmarkTitles.GetAll();
                ViewBag.LandmarkTitle = new Models.LandmarkTitlesModel() { LandmarkTitleId = 0, LandmarkTitle = "" };
                return View(titles);
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
        public ActionResult Edit(LandmarkTitlesModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.LandmarkTitles pt = new CLayer.LandmarkTitles()
                    {
                        LandmarkTitleId = data.LandmarkTitleId,
                        LandmarkTitle = data.LandmarkTitle
                    };
                    BLayer.LandmarkTitles.Save(pt);
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
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Saved = false;
                LandmarkTitlesModel mbt = new LandmarkTitlesModel() { LandmarkTitleId = 0 };

                CLayer.LandmarkTitles pt = BLayer.LandmarkTitles.Get(id);

                if (pt != null)
                    mbt = new LandmarkTitlesModel()
                    {
                        LandmarkTitleId = pt.LandmarkTitleId,
                        LandmarkTitle = pt.LandmarkTitle
                    };

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
                BLayer.LandmarkTitles.Delete(id);
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