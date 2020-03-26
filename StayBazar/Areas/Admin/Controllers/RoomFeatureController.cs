using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class RoomFeatureController : Controller
    {
        //
        // GET: /RoomFeature/
        //[Authorize(Roles = "Administrator , Staff")]
        //private List<CLayer.RoomFeature> InitData()
        //{
        //    List<CLayer.RoomFeature> RoomFeatureList = BLayer.RoomFeature.GetAll();
        //    ViewBag.RoomFeature = new RoomFeatureModel() { RoomFeatureId = 0, Title = "" };
        //    return RoomFeatureList;
        //}
        //[Authorize(Roles = "Administrator , Staff")]
        //public ActionResult Index()
        //{
        //    try
        //    {
        //        return View(InitData());
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //        return Redirect("~/Admin/ErrorPage");
        //    }
        //}
        //[Authorize(Roles = "Administrator , Staff")]
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult Edit(Models.RoomFeatureModel data)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            CLayer.RoomFeature pt = new CLayer.RoomFeature()
        //            {
        //                RoomFeatureId = data.RoomFeatureId,
        //                RoomTypeId = data.RoomTypeId,
        //                Title = data.Title
        //            };
        //            BLayer.RoomFeature.Save(pt);
        //            ViewBag.Saved = true;
        //        }
        //        else
        //            ViewBag.Saved = false;
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //        return Redirect("~/Admin/ErrorPage");
        //    }
        //}
        //[Authorize(Roles = "Administrator , Staff")]
        //public ActionResult Edit(int id)
        //{
        //    try
        //    {
        //        ViewBag.Saved = false;
        //        Models.RoomFeatureModel mbt = new RoomFeatureModel() { RoomFeatureId = 0 };

        //        CLayer.RoomFeature pt = BLayer.RoomFeature.Get(id);

        //        if (pt != null)
        //            mbt = new RoomFeatureModel() { RoomFeatureId = pt.RoomFeatureId, RoomTypeId = pt.RoomTypeId, Title = pt.Title };

        //        return PartialView("_Edit", mbt);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //        return Redirect("~/Admin/ErrorPage");
        //    }
        //}
        //[Authorize(Roles = "Administrator , Staff")]
        //public ActionResult Delete(int id)
        //{
        //    try
        //    {
        //        BLayer.RoomFeature.Delete(id);
        //        return RedirectToAction("Index");
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //        return Redirect("~/Admin/ErrorPage");
        //    }
       // }
    }
}