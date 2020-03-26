using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class StatusController : Controller
    {
        //
        // GET: /Admin/Status/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Update(Models.StatusModel data)
        {
            BLayer.User.SetStatus(data.UserId.ToString(), data.Status);
            return PartialView("_Status", data);
        }
	}
}