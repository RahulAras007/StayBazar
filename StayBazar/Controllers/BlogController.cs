using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
         public ActionResult Index()
        {
            string feed = "https://www.staybazar.com/blog/index.php/feed/";
            string errorString = "";

            try
            {
                if (String.IsNullOrEmpty(feed))
                {
                    throw new ArgumentNullException("feed");
                }
                    using (XmlReader reader = XmlReader.Create(feed))
                    {
                        SyndicationFeed rssData = SyndicationFeed.Load(reader);



                        return View(rssData);
                    }
            }
            catch (ArgumentNullException)
            {
                errorString = "No url for Rss feed specified.";
            }
            catch (SecurityException)
            {
                errorString = "You do not have permission to access the specified Rss feed.";
            }
            catch (FileNotFoundException)
            {
                errorString = "The Rss feed was not found.";
            }
            catch (UriFormatException)
            {
                errorString = "The Rss feed specified was not a valid URI.";
            }
            catch (Exception)
            {
                errorString = "An error occured accessing the RSS feed.";
            }

            var errorResult = new ContentResult();
            errorResult.Content = errorString;
            return errorResult;

        }
    }
}