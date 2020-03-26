using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{
    public class NotFoundController : Controller
    {
        //
        // GET: /NotFound/
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
	}
}