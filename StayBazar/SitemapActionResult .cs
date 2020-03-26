using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace StayBazar.components
{
    public class SitemapActionResult : ActionResult
    {
        private List<CLayer.Property> _posts;

        public SitemapActionResult(List<CLayer.Property> posts)
        {
            this._posts = posts;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            try
            {
                context.HttpContext.Response.ContentType = "application/xml";
                using (XmlWriter writer = XmlWriter.Create(System.Web.HttpContext.Current.Server.MapPath("Sitemap.xml")))
                {
                    writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                    writer.WriteStartElement("url");
                    writer.WriteElementString("loc", "https://www.staybazar.com");
                    writer.WriteEndElement();
                    foreach (CLayer.Property post in this._posts)
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
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
        }
 
    }
}