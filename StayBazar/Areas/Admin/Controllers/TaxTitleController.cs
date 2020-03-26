using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    [HandleError(ExceptionType = typeof(System.Data.DataException), View = "DatabaseError")]
    public class TaxTitleController : Controller
    {
        //
        // GET: /Admin/TaxTitle/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {             
                List<CLayer.TaxTitle> Taxtitlelist = BLayer.TaxTitle.GetAll();
                ViewBag.TaxTitle= new TaxTitleModel() { TaxTitleId = 0, Title = "",Status=0 };
                return View(Taxtitlelist);
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
        public ActionResult Editdata1(Models.TaxTitleModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.TaxTitle Ttitle = new CLayer.TaxTitle()
                    {
                        TaxTitleId =data.TaxTitleId,
                        Title = data.Title,
                        Status =data.Status
                    };
                    BLayer.TaxTitle.Save(Ttitle);

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
        public ActionResult Editdata1(int Id)
        {
            try
            {
                ViewBag.Saved = false;
                Models.TaxTitleModel data = new TaxTitleModel(){ TaxTitleId = 0 };

                CLayer.TaxTitle staycategory = BLayer.TaxTitle.Get(Id);

                if (staycategory != null)
                
                    data = new Models.TaxTitleModel()
                      {
                          TaxTitleId = staycategory.TaxTitleId,
                          Title = staycategory.Title,
                          Status = staycategory.Status
                      };
                return PartialView("~/Areas/Admin/Views/TaxTitle/_Edit.cshtml", data);
                
                
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
            
        }
        [Common.AdminRolePermission]
        public ActionResult Delete(int TaxTitleId)
        {
            try
            {
                BLayer.TaxTitle.Delete(TaxTitleId);
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