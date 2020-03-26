using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StayBazar.Models;
using StayBazar.Lib.Security;

namespace StayBazar.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        private List<CLayer.User> InitData(int status)
        {
            return BLayer.User.GetAllStaff(status);
        }

        public ActionResult Index()
        {
            try
            {
                return View(InitData((int)Models.UserModel.StatusTypes.Active));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult Blocked()
        {
            try
            {
                return View("Index", InitData((int)Models.UserModel.StatusTypes.Blocked));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult RecentlyDeleted()
        {
            try
            {
                return View("Index", InitData((int)Models.UserModel.StatusTypes.Deleted));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult New()
        {
            try
            {
                ViewBag.Head = "New User";
                Models.UserModel usr = new Models.UserModel() { UserId = 0, Status = (int)Models.UserModel.StatusTypes.Active };
                return View("Edit", usr);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Models.UserModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.User pt = new CLayer.User()
                    {
                        UserId = data.UserId,
                        SalutationId = data.SalutationId,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        DateOfBirth = Convert.ToDateTime(data.DateOfBirth),
                        Sex = data.Sex,
                        UserTypeId = data.UserTypeId,
                        Status = data.Status,
                        Email = data.Email
                    };
                    long saved = BLayer.User.Save(pt);
                    if (saved > 0)
                    {
                        if (data.UserId == 0)
                        {
                            UserManager<StayBazar.Lib.Security.IdentityUser> UserManager = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                            String userId = User.Identity.GetUserId();
                            String newPassword = BLayer.Settings.GetValue(CLayer.Settings.DEFAULT_PASSWORD);
                            String hashedNewPassword = UserManager.PasswordHasher.HashPassword(newPassword);
                            BLayer.User.SetPassword(saved, hashedNewPassword);
                        }
                        ViewBag.Saved = true;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Saved = true;
                        ViewBag.ErrorMessage = "Other user with this EmailId exists.";
                        return View("Edit", data);
                    }
                }
                else
                {
                    ViewBag.Saved = false;
                    return View("Edit", data);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                CLayer.User user = BLayer.User.Get(id);
                Models.UserModel usermodel = new Models.UserModel()
                {
                    UserId = user.UserId,
                    SalutationId = user.SalutationId,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth.ToShortDateString(),
                    Sex = user.Sex,
                    UserTypeId = user.UserTypeId,
                    Status = user.Status,
                    Email = user.Email
                    //,CreatedDate = user.CreatedDate
                };
                ViewBag.Head = user.FirstName + " " + user.LastName;
                return View("Edit", usermodel);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult Block(int id)
        {
            try
            {
                BLayer.User.SetStatus(id.ToString(), (int)Models.UserModel.StatusTypes.Blocked);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult Restore(int id)
        {
            try
            {
                BLayer.User.SetStatus(id.ToString(), (int)Models.UserModel.StatusTypes.Active);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.User.SetStatus(id.ToString(), (int)Models.UserModel.StatusTypes.Deleted);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [HttpPost]
        public string Actionset(string data, string type)
        {
            string action = "Index";
            try
            {
                if (type == "block")
                {
                    BLayer.User.SetStatus(data, (int)Models.UserModel.StatusTypes.Blocked);
                }
                if (type == "restore")
                {
                    BLayer.User.SetStatus(data, (int)Models.UserModel.StatusTypes.Active);
                }
                if (type == "delete")
                {
                    BLayer.User.SetStatus(data, (int)Models.UserModel.StatusTypes.Deleted);
                }
            }
            catch (Exception) { action = "0"; }
            return action;
        }
    }
}