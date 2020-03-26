using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class CityController : Controller
    {
        //
        // GET: /Admin/City/
        private List<CLayer.City> InitData(int stateId)
        {
            List<CLayer.City> pts = BLayer.City.GetOnState(stateId);
            ViewBag.City = new CityModel() { CityId = 0, Name = "", StateId = stateId ,Keywords="", ForListing = false};
            return pts;
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Index(int id)
        {
            try
            {
                return View(InitData(id));
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
        public ActionResult Edit(StayBazar.Areas.Admin.Models.CityModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.City pt = new CLayer.City()
                    {
                        CityId = data.CityId,
                        Name = data.Name,
                        StateId = data.StateId,
                        ForListing = data.ForListing,
                        Keywords = data.Keywords
                    };
                    BLayer.City.Save(pt);
                    ViewBag.Saved = true;
                }
                else
                    ViewBag.Saved = false;
                return RedirectToAction("Index", new { id = data.StateId });
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
                StayBazar.Areas.Admin.Models.CityModel mbt = new CityModel() { CityId = 0 };

                CLayer.City pt = BLayer.City.Get(id);

                if (pt != null)
                    mbt = new CityModel()
                    {
                        CityId = pt.CityId,
                        Name = pt.Name,
                        StateId = pt.StateId,
                        ForListing = pt.ForListing,
                        Keywords=pt.Keywords

                    };
                return PartialView("_Edit", mbt);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
    }
}