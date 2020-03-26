using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SBEntityController : Controller
    {
        // GET: Admin/SBEntity
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.SBEntityModel model = new Models.SBEntityModel();
            model.EntityId = 0;
            model.LoadState();
            return View(model);
        }
        [Common.AdminRolePermission]
        public ActionResult Edit(long id)
        {
            Models.SBEntityModel model = new Models.SBEntityModel();
            try
            {
                CLayer.SBEntity data = BLayer.SBEntity.Get(id);
                if(data == null)
                {
                    model.LoadState();
                    model.EntityId = 0;
                    return View("_Edit", model);
                }
                model.EntityId = data.EntityId;
                model.Name = data.Name;
                model.Address = data.Address;
                model.GSTState = data.GSTStateId;
                model.GSTNo = data.GSTNo;
                model.Phone = data.Phone;
                model.LoadState();
            }catch(Exception ex)
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
                BLayer.SBEntity.Delete(id);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        public ActionResult Save(Models.SBEntityModel model)
        {
            
            try
            {
                if(ModelState.IsValid)
                {
                    CLayer.SBEntity data = new CLayer.SBEntity();
                    long cid = 0;
                    long.TryParse(User.Identity.GetUserId(), out cid);
                    data.Name = model.Name;
                    data.Address = model.Address;
                    data.GSTStateId = model.GSTState;
                    data.GSTNo = model.GSTNo;
                    data.StateId = data.GSTStateId;
                    data.CountryId = Models.SBEntityModel.COUNTRY_INDIA;
                    data.EntityId = model.EntityId;
                    data.Phone = model.Phone;
                    data.UserId = cid;
                    BLayer.SBEntity.Save(data);
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