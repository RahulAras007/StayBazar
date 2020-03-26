using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class AccommodationFeatureController : Controller
    {
        //
        // GET: /AccomodationFeature/
        private List<CLayer.AccommodationFeature> InitData()
        {
            List<CLayer.AccommodationFeature> AccomodationFeatureList = BLayer.AccommodationFeature.GetAll();
            ViewBag.AccommodationFeature = new AccommodationFeatureModel() { AccommodationFeatureId = 0, Title = "", Style = "",Showfeatures=false };
            return AccomodationFeatureList;
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
        public ActionResult Edit(Models.AccommodationFeatureModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.AccommodationFeature pt = new CLayer.AccommodationFeature()
                    {
                        AccommodationFeatureId = data.AccommodationFeatureId,
                        // RoomTypeId = data.RoomTypeId,
                        Title = data.Title,
                        Style = data.Style,
                        Showfeatures=data.Showfeatures
                    };
                    BLayer.AccommodationFeature.Save(pt);
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
                Models.AccommodationFeatureModel mbt = new AccommodationFeatureModel() { AccommodationFeatureId = 0 };

                CLayer.AccommodationFeature pt = BLayer.AccommodationFeature.Get(id);

                if (pt != null)
                    mbt = new AccommodationFeatureModel() { AccommodationFeatureId = pt.AccommodationFeatureId, Title = pt.Title, Style = pt.Style,Showfeatures=pt.Showfeatures };

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
                BLayer.AccommodationFeature.Delete(id);
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