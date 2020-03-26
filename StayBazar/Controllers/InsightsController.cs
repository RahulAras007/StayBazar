using System;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Controllers
{
    public class InsightsController : Controller
    {
        [AllowAnonymous]
        [OutputCache(Duration = 10)]
        public ActionResult Index()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    string userId = User.Identity.GetUserId();
                    long id = 0;
                    if (long.TryParse(userId, out id))
                    {
                        if (id > 0)
                        {
                            CLayer.Role.Roles rle = BLayer.User.GetRole(id);
                            if (rle == CLayer.Role.Roles.Corporate || rle == CLayer.Role.Roles.CorporateUser)
                                return View("~/Views/Account/Home.cshtml");
                        }
                    }
                }
            }
            catch (Exception ex)
            { Common.LogHandler.HandleError(ex); }
            return View();
        }
        
        [AllowAnonymous]
        public ActionResult TRAVELLER()
        {
            ViewBag.Message = "TRAVELLER";
            return View();
        }
        [AllowAnonymous]
        public ActionResult ADMIN_DESK()
        {
            ViewBag.Message = "ADMIN DESK";
            return View();
        }
        [AllowAnonymous]
        public ActionResult FINANCEACCOUNTS()
        {
            ViewBag.Message = "FINANCE / ACCOUNTS";
            return View();
        }

        [AllowAnonymous]
        public ActionResult TRAVELDESK()
        {
            ViewBag.Message = "TRAVEL DESK.";
            return View();
        }
        [AllowAnonymous]
        public ActionResult ORGANISATION()
        {
            ViewBag.Message = "ORGANISATION";
            return View();
        }
        [AllowAnonymous]
        public ActionResult MANAGEMENT()
        {
            ViewBag.Message = "MANAGEMENT";
            return View();
        }
       
    }
}