using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//using System.IO.file
namespace StayBazar
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            ///Dynamic ignore routes
            try
            {
                List<string> igrts = Common.Utils.GetIgnoredRoutes();
                foreach (string s in igrts)
                {
                    routes.IgnoreRoute(s);
                }
            }
            catch { }
            //Dynamic Custom routes
            //List<Common.Utils.CustomRoute> crts = Common.Utils.GetCustomRoutes();
            //int i = 1;
            //foreach(Common.Utils.CustomRoute cr in crts)
            //{
            //    routes.MapRoute(
            //   name: "croute"+i.ToString(),
            //   url: cr.Path,
            //   defaults: new { controller = cr.Controller, action = "propertylistingterms", id = UrlParameter.Optional },
            //   namespaces: new String[] { "StayBazar.Controllers" }
            //    );
            //    i++;
            //}
            //routes.IgnoreRoute("serivce_apartment/{city}");
            //routes.IgnoreRoute("serivce_apartment/{city}/{location}");
            /* routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
             );*/

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "account", action = "index", id = UrlParameter.Optional }
            // );
            routes.MapRoute(
              name: "Accounthome",
              url: "business",
              defaults: new { controller = "account", action = "home" }
           );





            routes.MapRoute(
           name: "Accountbl1",
           url: "business-corporate-login",
           defaults: new { controller = "account", action = "BusinessLogin", type =1 }
        );

            routes.MapRoute(
           name: "Accountbl2",
           url: "business-supplier-login",
           defaults: new { controller = "account", action = "BusinessLogin", type = 2 }
        );


            routes.MapRoute(
           name: "Accountbl3",
           url: "business-travel-agent-login",
           defaults: new { controller = "account", action = "BusinessLogin", type = 3 }
        );





            routes.MapRoute(
                name: "list11",
                url: "propertylistingterms/{id}",
                defaults: new { controller = "Home", action = "propertylistingterms", id = UrlParameter.Optional },
                namespaces: new String[] { "StayBazar.Controllers" }
            );
            routes.MapRoute(
                name: "CustomApartment",
                url: "serviced-apartments/{city}/{location}/{id}",
                defaults: new { controller = "Property", action = "Index", id = UrlParameter.Optional },
                namespaces: new String[] { "StayBazar.Controllers" }
            );
            routes.MapRoute(
               name: "JoinAccount",
               url: "Account/Register/{Thanks}",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional },
               namespaces: new String[] { "StayBazar.Controllers" }
           );
            routes.MapRoute(
              name: "JoinCorp",
              url: "Join/Corporate/{Thanks}",
              defaults: new { controller = "Join", action = "Corporate", id = UrlParameter.Optional },
              namespaces: new String[] { "StayBazar.Controllers" }
          );
            routes.MapRoute(
           name: "Joinsupper",
           url: "Join/Supplier/{Thanks}",
           defaults: new { controller = "Join", action = "Supplier", id = UrlParameter.Optional },
           namespaces: new String[] { "StayBazar.Controllers" }
       );
            routes.MapRoute(
            name: "JoinAffi",
            url: "Join/Affiliates/{Thanks}",
            defaults: new { controller = "Join", action = "Affiliates", id = UrlParameter.Optional },
            namespaces: new String[] { "StayBazar.Controllers" }
        );
            routes.MapRoute(
          name: "Jointra",
          url: "Join/TravelAgent/{Thanks}",
          defaults: new { controller = "Join", action = "TravelAgent", id = UrlParameter.Optional },
          namespaces: new String[] { "StayBazar.Controllers" }
      );




            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new String[] { "StayBazar.Controllers" }
           );


            /*    routes.MapRoute(
                   name: "SearchCity",
                   url: "{controller}/{action}/{city}",
                   defaults: new { controller = "Search", action = "Index", City = UrlParameter.Optional },
                   namespaces: new String[] { "StayBazar.Controllers" }
               );
                */

        }


    }
}
