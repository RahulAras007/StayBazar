using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class StateController : Controller
    {
        //
        // GET: /Admin/State/

        private List<CLayer.State> InitData(int countryId)
        {
            List<CLayer.State> pts = BLayer.State.GetOnCountry(countryId);
            ViewBag.State = new StateModel() { StateId = 0, Name = "", CountryId = countryId };
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
        public ActionResult Edit(StayBazar.Areas.Admin.Models.StateModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.State pt = new CLayer.State()
                    {
                        StateId = data.StateId,
                        Name = data.Name,
                        CountryId = data.CountryId,
                        GSTStateCode=data.GSTStateCode
                    };
                    BLayer.State.Save(pt);
                    ViewBag.Saved = true;
                }
                else
                    ViewBag.Saved = false;
                return RedirectToAction("Index", new { id = data.CountryId });
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
                StayBazar.Areas.Admin.Models.StateModel mbt = new StateModel() { StateId = 0 };

                CLayer.State pt = BLayer.State.Get(id);

                if (pt != null)
                    mbt = new StateModel()
                    {
                        StateId = pt.StateId,
                        Name = pt.Name,
                        CountryId = pt.CountryId,
                        GSTStateCode=pt.GSTStateCode
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