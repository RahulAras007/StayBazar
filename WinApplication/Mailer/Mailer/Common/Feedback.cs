using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;

namespace Mailer.Common
{
    public class Feedback
    {
        private const string URL_REVIEWSENDLINK = "ReviewSendLink";
        private const string URL_REVIEWSENDBOOKINGLIST = "ReviewSendBookingList";

        public void Start()
        {
            SystemLogger logger = new SystemLogger();
            Console.WriteLine("Fetching Feedback..");
            WebClient wc = new WebClient();

            string url = ConfigurationManager.AppSettings.Get(URL_REVIEWSENDBOOKINGLIST);
            string urlbody = ConfigurationManager.AppSettings.Get(URL_REVIEWSENDLINK);
        
            string ids = "";
            string[] strids;
            string[] RevData;
            string Bookid, Useremail, BookingUseremail;


            Common.Mailer FeedbackMailer = new Mailer();
            System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            mailMsg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get("EmailId"), "Staybazar");
            mailMsg.IsBodyHtml = true;
            mailMsg.Subject = "Feedback Form - Staybazar ";

            Console.WriteLine("Sending Mails..");

            ids = wc.DownloadString(url);
            if (ids == null) return;
            if (ids.Trim() == "") return;
            strids = ids.Split(',');
            foreach (string revdt in strids)
            {
                RevData = revdt.Split('#');
                if (RevData.Length != 3) continue;             
                Bookid = RevData[0].Trim();
                Useremail = RevData[1].Trim();
                BookingUseremail = RevData[2].Trim();

                string result = wc.DownloadString(urlbody + Bookid);
                mailMsg.Body = result;
                if (mailMsg.Body != "")
                {
                    mailMsg.To.Clear();

                    if (BookingUseremail != null && BookingUseremail != "")
                    {
                        if (BookingUseremail != Useremail)
                        {
                            mailMsg.To.Add(BookingUseremail);
                            mailMsg.Bcc.Add(Useremail);
                        }
                        else
                        {
                            mailMsg.To.Add(Useremail);
                        }

                    }
                    else 
                    {
                        mailMsg.To.Add(Useremail);                    
                    }

                   
                    try
                    {
                        FeedbackMailer.SendMail(mailMsg);
                    }
                    catch (Exception ex) { logger.LogError(ex.Message); }
                }
            }
        }




    }
}
