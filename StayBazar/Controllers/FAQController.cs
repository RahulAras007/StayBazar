using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Common;

namespace StayBazar.Controllers
{
    public class FAQController : Controller
    {
        //
        // GET: /FAQ/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult ServiceApartment()
        {
            return View("ServiceApartment");
        }
	}
}