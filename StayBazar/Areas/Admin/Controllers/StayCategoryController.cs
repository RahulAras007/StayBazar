using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    [HandleError(ExceptionType = typeof(System.Data.DataException), View = "DatabaseError")]
    public class StayCategoryController : Controller
    {
        //
        // GET: /StayCategory/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {
                List<CLayer.StayCategory> staycategorylist = BLayer.StayCategory.GetAll();
                ViewBag.StayCategory = new StayCategoryModels() { CategoryId = 0, Title = "" };
                return View(staycategorylist);
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
        public ActionResult Edit(Models.StayCategoryModels data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.StayCategory staycategory = new CLayer.StayCategory()
                    {
                        CategoryId = data.CategoryId,
                        Title = data.Title
                    };
                    BLayer.StayCategory.Save(staycategory);

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
                Models.StayCategoryModels mStayCategory = new StayCategoryModels() { CategoryId = 0 };


                CLayer.StayCategory staycategory = BLayer.StayCategory.Get(id);

                if (staycategory != null)
                    mStayCategory = new StayCategoryModels() { CategoryId = staycategory.CategoryId, Title = staycategory.Title };

                return PartialView("_Edit", mStayCategory);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.RoleRequired]
        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.StayCategory.Delete(id);
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