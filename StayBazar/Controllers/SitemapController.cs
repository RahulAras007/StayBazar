using StayBazar.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{
    public class SitemapController : Controller
    {
        // GET: Sitemap
        [AllowAnonymous]
        public ActionResult Index()
        {
            List<CLayer.Property> dt = BLayer.Property.GetAllPropertiesForsitemap();
            return new SitemapActionResult(dt);
           
        }
    }
}