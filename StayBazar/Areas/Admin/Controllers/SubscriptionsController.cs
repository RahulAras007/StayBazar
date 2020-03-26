using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.IO;



namespace StayBazar.Areas.Admin.Controllers
{
    public class SubscriptionsController : Controller
    {
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.SubscriptionsModel data = new Models.SubscriptionsModel();

            data.SubscriptionsList = BLayer.Mail.GetAllId(true);
            //data.MaxCount = 21;
            //data.CurrentPage = 1;
            return View(data);
        }
        //subscribed

        public ActionResult Download()
        {
            string s = BLayer.Mail.getMail();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=EmailIds.csv");
            Response.ContentType = "text/plain";
            Response.Write(s);

            Response.End();
            return View();
        }
        public ActionResult DownloadUnSubscribed()
        {
            string s = BLayer.Mail.GetMailUnSubscribed();
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment; filename=EmailIds.csv");
            Response.ContentType = "text/plain";
            Response.Write(s);

            Response.End();     
            return View();
        }


        //public ActionResult Applypager(Models.SubscriptionsModel data)
        //{         
        //    data.SubscriptionsList = BLayer.Mail.GetAllId(true);
        //    data.CurrentPage = data.CurrentPage;
        //    data.TotalRows= data.MaxCount;
        //    data.IsSearched = true;
        //    return View("_List",data);
        //}

    }
}