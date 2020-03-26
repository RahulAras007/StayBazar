using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class HSNCodeController : Controller
    {
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.HSNCodeModel model = new Models.HSNCodeModel();
            model.CodeId = 0;
            return View(model);
        }
        [Common.AdminRolePermission]
        public ActionResult Edit(long id)
        {
            Models.HSNCodeModel model = new Models.HSNCodeModel();
            try
            {
                CLayer.HSNCode data = BLayer.HSNCode.Get(id);
                if (data == null)
                {
                    model.CodeId = 0;
                    return View("_Edit", model);
                }
                model.CodeId = data.CodeId;
                model.NatureOfService = data.NatureOfService;
                model.HSNCode = data.Code;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("_Edit", model);
        }


        [Common.AdminRolePermission]
        public ActionResult Delete(long id)
        {

            try
            {
                BLayer.HSNCode.Delete(id);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        public ActionResult Save(Models.HSNCodeModel model)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.HSNCode data = new CLayer.HSNCode();
                    long cid = 0;
                    long.TryParse(User.Identity.GetUserId(), out cid);
                    data.CodeId = model.CodeId;
                    data.NatureOfService = model.NatureOfService;
                    data.Code = model.HSNCode;
                   
                    BLayer.HSNCode.Save(data);
                }


            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
    }
}