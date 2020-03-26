using StayBazar.components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SitemapController : Controller
    {
        // GET: Admin/Sitemap
        [AllowAnonymous]
        public bool Index()
        {
            try
            {
                List<CLayer.Property> dt = BLayer.Property.GetAllPropertiesForsitemap();
                using (XmlWriter writer = XmlWriter.Create(System.Web.HttpContext.Current.Server.MapPath("~/Sitemap.xml")))
                {
                    writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", "https://www.staybazar.com");
                    writer.WriteEndElement();
                    foreach (CLayer.Property post in dt)
                    {
                        string data = "https://www.staybazar.com/serviced-apartments/" + post.City.ToLower().Replace(" ", "-") + "/" + post.Location.ToLower().Replace(" ", "-") + "/" + post.PropertyId;
                        writer.WriteStartElement("url");
                        writer.WriteElementString("loc", data);
                        writer.WriteEndElement();
                    }
                    writer.WriteEndElement();
                    writer.Flush();
                    writer.Close();
                    
                }
                return true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return false;
            }

           
        }
     
    }
}