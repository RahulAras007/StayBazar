using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using CaptchaMvc.Infrastructure;
using CaptchaMvc.Interface;
using CaptchaMvc.Models;
using Hangfire;
using Hangfire.Common;
using Hangfire.MemoryStorage;
using System.Threading.Tasks;

namespace StayBazar
{
    public static class MyTasks
    {
        public static async Task<int> MinuteTick()
        {
            await Task.Run(() =>
            {
                BackgroundJob.Schedule(() => MyTasks.Tick(), TimeSpan.FromSeconds(30));
            });

            return 1;
        }

        public static async Task<int> Tick()
        {
            await Task.Run(() =>
            {
                //using (HangFireDemo.Models.HangFireDBEntities db = new Models.HangFireDBEntities())
                //{
                //    db.Tracks.Add(new Models.Track
                //    { StampTime = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss tt") });
                //    db.SaveChanges();

                //}
            });

            return 1;
        }
    }
    public class MvcApplication : System.Web.HttpApplication
    {
        private BackgroundJobServer _backgroundJobServer;
        protected void Application_Start()
        {
            
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var captchaManager = (DefaultCaptchaManager)CaptchaUtils.CaptchaManager;
            captchaManager.CharactersFactory = () => "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            captchaManager.PlainCaptchaPairFactory = length =>
            {
                string randomText = RandomText.Generate(captchaManager.CharactersFactory(), length);
                bool ignoreCase = false;//This parameter is responsible for ignoring case.
                return new KeyValuePair<string, ICaptchaValue>(Guid.NewGuid().ToString("N"),
                    new StringCaptchaValue(randomText, randomText, ignoreCase));
            };
            GlobalConfiguration.Configuration.UseMemoryStorage();

            GlobalConfiguration.Configuration.UseMemoryStorage();

            _backgroundJobServer = new BackgroundJobServer();
            RecurringJob.AddOrUpdate(() => MyTasks.MinuteTick(), Cron.Minutely);


        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
            HttpContext.Current.Response.AddHeader("X-XSS-Protection", "1");
            HttpContext.Current.Response.AddHeader("X-Content-Type-Options", "nosniff");
            //HttpContext.Current.Response.AddHeader("Cache-Control", "no-cache,no-store,must-revalidate,private");
           // Response.CacheControl = "no-cache,no-store,must-revalidate,private";
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;
            
            // clear login session after logout
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
            Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            //Response.Cache.

        }

        protected void Application_PreSendRequestHeaders(Object sender, EventArgs e)
        {
            Response.Headers.Set("Cache-Control", "no-cache,no-store,must-revalidate,private");
        }
        protected void Application_End(object sender, EventArgs e)
        {
            _backgroundJobServer.Dispose();
        }
        //protected void Session_Start(Object sender, EventArgs e)
        //{
        //    int dc = BLayer.Currency.GetDefault();
        //    Session[CLayer.ObjectStatus.SITE_CURRENCY] = dc;
        //}
    }
}
