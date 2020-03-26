using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class StaticHtmlPageController : Controller
    {
        //
        // GET: /Admin/StaticHtmlPage/
        [AllowAnonymous]
        public ActionResult Index(long PageId)
        {
            Models.StaticHtmlPageModel data = new Models.StaticHtmlPageModel();
            data.PageId = PageId;
            CLayer.StaticPage obj = BLayer.StaticPage.GetPage(PageId);
            List<CLayer.SearchResult> dtlist1 = BLayer.Property.GetStaticPagePrpty(PageId);
            data.PropertyList = dtlist1;
            string url = Request.Url.AbsoluteUri;
            data.PageTitle = obj.PageTitle;
            data.Description = obj.Description;
            data.Image = obj.Image;
            data.ShowInWidget = obj.ShowInWidget;
            data.Location = obj.Location;
            data.City = obj.City;
            return View(data);
        }
    }
}