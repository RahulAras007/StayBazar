using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/
        [Common.RoleRequired(Administrator = true, Staff = true,SalesPerson=true)]
        public ActionResult Index()
        {
            return View();
        }
	}
}