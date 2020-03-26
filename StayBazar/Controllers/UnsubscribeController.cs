using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class UnSubscribeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UnSubscribed(Models.UnSubscribeModel data)
        {
            try
            {
                CLayer.Mail pt = new CLayer.Mail()
                {
                    Mailaddress = data.Mailaddress,

                };
            long i=  BLayer.Mail.UnSubscribed(pt);
               // return RedirectToAction("Index");
            }
            catch
            {
                //return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
	}
}