using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    [HandleError(ExceptionType = typeof(System.Data.DataException), View = "DatabaseError")]
    public class SalutationController : Controller
    {
        //
        // GET: /Salutation/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {
                List<CLayer.Salutation> salutationlist = BLayer.Salutation.GetAll();
                ViewBag.Salutation = new SalutationModel() { SalutationId = 0, Title = "" };
                return View(salutationlist);
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
        public ActionResult Edit(Models.SalutationModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.Salutation salutation = new CLayer.Salutation()
                    {
                        SalutationId = data.SalutationId,
                        Title = data.Title
                    };
                    BLayer.Salutation.Save(salutation);

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
                Models.SalutationModel mSalutation = new SalutationModel() { SalutationId = 0 };

                CLayer.Salutation staycategory = BLayer.Salutation.Get(id);

                if (staycategory != null)
                    mSalutation = new SalutationModel() 
                    {
                        SalutationId = staycategory.SalutationId, 
                        Title = staycategory.Title
                    };

                return PartialView("_Edit", mSalutation);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.Salutation.Delete(id);
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