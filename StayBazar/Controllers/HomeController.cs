using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;


namespace StayBazar.Controllers
{
    public class HomeController : Controller
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
        public ActionResult Index2()
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
        public ActionResult About()
        {
            ViewBag.Message = "About StayBazar";
            return View();
        }
        [AllowAnonymous]
        public ActionResult ContactUs()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [AllowAnonymous]
        public ActionResult PaymentGuidelines()
        {
            ViewBag.Message = "Your Payment Guidelines page.";
            return View();
        }

        [AllowAnonymous]
        public ActionResult propertylistingterms()
        {
            ViewBag.Message = "Your Payment Guidelines page.";
            return View("PropertyListingTerms");
        }
        [AllowAnonymous]
        public ActionResult Careers()
        {
            ViewBag.Message = "Careers page.";
            return View();
        }
        [AllowAnonymous]
        public ActionResult HowStaybazarworks()
        {
            ViewBag.Message = "Your How Staybazar Work page.";
            return View();
        }
        [AllowAnonymous]
        public ActionResult PurposeAndStandards()
        {
            ViewBag.Message = "Your Purpose and Standards page.";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Affiliates()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult SpecialRequirements()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Team()
        {
            ViewBag.Message = "About Team";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Standards()
        {
            ViewBag.Message = "";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Terms()
        {
            ViewBag.Message = "Terms and conditions..";
            return View();
        }
        [AllowAnonymous]
        public ActionResult PrivacyPolicy()
        {
            ViewBag.Message = "Privacy Policy details.";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Privacy()
        {
            ViewBag.Message = "Privacy Policy";
            return View();
        }
        [AllowAnonymous]
        public ActionResult TermsOfUse()
        {
            ViewBag.Message = "Terms of Use";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Sitemap()
        {
            ViewBag.Message = "Sitemap";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Vision()
        {
            ViewBag.Message = "Mission And Vision";
            return View();
        }
        [AllowAnonymous]
        public ActionResult SendUsaMessage()
        {
            ViewBag.Message = "Send Us a Message";
            return View();
        }
        [AllowAnonymous]
        public ActionResult Demo()
        {
            ViewBag.Message = "Demo";
            return View();
        }
    }
}