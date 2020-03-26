using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Threading.Tasks;

namespace StayBazar.Controllers
{
    public class FeedbackController : Controller
    {
        //
        // GET: /Feedback/
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<string> FeedbackSave(string email, string feedback)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.To.Add(ConfigurationManager.AppSettings.Get("FeedbackEmail"));
                mm.CC.Add(ConfigurationManager.AppSettings.Get("FeedbackCC"));
                mm.From = new MailAddress(ConfigurationManager.AppSettings.Get("FeedbackEmail"));
                mm.ReplyToList.Add(email);
                mm.Subject = "Feedback";             
                mm.Body = "<strong>Email Id:</strong> " + email + "<br /><strong>Feedback:</strong><br />" + feedback;
                mm.IsBodyHtml = true;
                
               Common.Mailer mlr = new Common.Mailer();
                await mlr.SendMailAsyncWithoutFields(mm);
               
               // BLayer.Query.Savefeedback(email, feedback);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
        }
	}
}