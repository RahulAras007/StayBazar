using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;
namespace StayBazar.Areas.Admin.Controllers
{
    public class CountryController : Controller
    {
        //
        // GET: Admin/Country/
        [Common.AdminRolePermission]
        private List<CLayer.Country> InitData()
        {
            List<CLayer.Country> pts = BLayer.Country.GetList();
            ViewBag.Country = new CountryModel() { CountryId = 0, Name = "", IsDefault = false, ForProperty = false, Status = (int)Areas.Admin.Models.CountryModel.StatusTypes.Enabled };
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
        public ActionResult Edit(StayBazar.Areas.Admin.Models.CountryModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.Country pt = new CLayer.Country()
                    {
                        CountryId = data.CountryId,
                        Name = data.Name,
                        IsDefault = data.IsDefault,
                        ForProperty = data.ForProperty,
                        Status = data.Status,
                        Code = data.Code
                    };
                    BLayer.Country.Save(pt);
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
                StayBazar.Areas.Admin.Models.CountryModel mbt = new CountryModel() { CountryId = 0 };

                CLayer.Country pt = BLayer.Country.Get(id);

                if (pt != null)
                    mbt = new CountryModel()
                    {
                        CountryId = pt.CountryId,
                        Name = pt.Name,
                        IsDefault = pt.IsDefault,
                        ForProperty = pt.ForProperty,
                        Status = pt.Status,
                        Code = pt.Code
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