using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SuggestController : Controller
    {
        //
        // GET: /Admin/Suggest/
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Index()
        {
            try
            {
                return View(BLayer.Suggest.GetAll());
            }
            catch (Exception ex)
            {
                StayBazar.Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Verified()
        {
            try
            {
                return View("Index", BLayer.Suggest.GetAll((int)StayBazar.Models.SuggestModel.StatusTypes.Verified));
            }
            catch (Exception ex)
            {
                StayBazar.Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Rejected()
        {
            try
            {
                return View("Index", BLayer.Suggest.GetAll((int)StayBazar.Models.SuggestModel.StatusTypes.Rejected));
            }
            catch (Exception ex)
            {
                StayBazar.Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Edit(int Id)
        {
            try
            {
                CLayer.Suggest suggest = BLayer.Suggest.Get(Id);
                if (suggest.Status == (int)StayBazar.Models.SuggestModel.StatusTypes.NotViewed)
                {
                    BLayer.Suggest.SetStatus(Id, (int)StayBazar.Models.SuggestModel.StatusTypes.Viewed);
                }
                return View(suggest);
            }
            catch (Exception ex)
            {
                StayBazar.Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Verify(int Id)
        {
            CLayer.Suggest suggest = BLayer.Suggest.Get(Id);
            BLayer.Suggest.SetStatus(Id, (int)StayBazar.Models.SuggestModel.StatusTypes.Verified);
            if (suggest.Status == (int)StayBazar.Models.SuggestModel.StatusTypes.Viewed)
            {
                return RedirectToAction("Inbox");
            }
            else if (suggest.Status == (int)StayBazar.Models.SuggestModel.StatusTypes.Verified)
            {
                return RedirectToAction("Verified");
            }
            return RedirectToAction("Rejected");
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Reject(int Id)
        {
            CLayer.Suggest suggest = BLayer.Suggest.Get(Id);
            BLayer.Suggest.SetStatus(Id, (int)StayBazar.Models.SuggestModel.StatusTypes.Rejected);
            if (suggest.Status == (int)StayBazar.Models.SuggestModel.StatusTypes.Viewed)
            {
                return RedirectToAction("Inbox");
            }
            else if (suggest.Status == (int)StayBazar.Models.SuggestModel.StatusTypes.Verified)
            {
                return RedirectToAction("Verified");
            }
            return RedirectToAction("Rejected");
        }
        [Common.RoleRequired(Administrator = true)]
        public ActionResult Delete(int Id)
        {
            CLayer.Suggest suggest = BLayer.Suggest.Get(Id);
            BLayer.Suggest.SetStatus(Id, (int)StayBazar.Models.SuggestModel.StatusTypes.Deleted);
            if (suggest.Status == (int)StayBazar.Models.SuggestModel.StatusTypes.Viewed)
            {
                return RedirectToAction("Inbox");
            }
            else if (suggest.Status == (int)StayBazar.Models.SuggestModel.StatusTypes.Verified)
            {
                return RedirectToAction("Verified");
            }
            return RedirectToAction("Rejected");
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public string Actionset(string data, string type)
        {
            string action = "Index";
            try
            {

                int status = 1;
                if (data.IndexOf(",") > -1) { string[] ids = data.Split(','); status = BLayer.Suggest.Get(Convert.ToInt32(ids[0])).Status; }
                else { status = BLayer.Suggest.Get(Convert.ToInt32(data)).Status; }

                if (type == "markusread")
                {
                    BLayer.Suggest.SetStatus(data, (int)StayBazar.Models.SuggestModel.StatusTypes.Viewed);
                }
                if (type == "markusunread")
                {
                    BLayer.Suggest.SetStatus(data, (int)StayBazar.Models.SuggestModel.StatusTypes.NotViewed);
                }
                if (type == "verify")
                {
                    BLayer.Suggest.SetStatus(data, (int)StayBazar.Models.SuggestModel.StatusTypes.Verified);
                }
                if (type == "reject")
                {
                    BLayer.Suggest.SetStatus(data, (int)StayBazar.Models.SuggestModel.StatusTypes.Rejected);
                }
                if (type == "delete")
                {
                    BLayer.Suggest.SetStatus(data, (int)StayBazar.Models.SuggestModel.StatusTypes.Deleted);
                }

                if (status == (int)StayBazar.Models.SuggestModel.StatusTypes.Viewed || status == (int)StayBazar.Models.SuggestModel.StatusTypes.NotViewed)
                {
                    action = "Index";
                }
                else if (status == (int)StayBazar.Models.SuggestModel.StatusTypes.Verified)
                {
                    action = "Verified";
                }
                else
                {
                    action = "Rejected";
                }
            }
            catch (Exception) { action = "Index"; }

            return action;
        }
    }
}