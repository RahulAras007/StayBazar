using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;

namespace Mailer.Common
{
    public class BookingCartMail
    {
        private const string URL_BOOKINGCART = "BookingCartLink";
        private const string CUSTOMERCARE = "BookingCartMail";
        private const string BOOKINGCARTCC = "BookingCartCCMail";

        public void Start()
        {
            SystemLogger logger = new SystemLogger();
            Console.WriteLine("Fetching Booking Carts..");
            WebClient wc = new WebClient();
            string url = ConfigurationManager.AppSettings.Get(URL_BOOKINGCART);
            try
            {
                string result = wc.DownloadString(url);

                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                mailMsg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get("EmailId"), "Staybazar");

                mailMsg.IsBodyHtml = true;
                // mailMsg.Subject = ConfigurationManager.AppSettings.Get(MAIL_SUBJECT);
                mailMsg.Subject = "Notice: Booking Carts Report";

                try
                {
                    result = Mailer.MessageFromHtml(url);
                }
                catch (Exception ex)
                {
                    logger.LogError( ex.Message);
                }
                if (result != "")
                {
                    mailMsg.Body = result;
                    mailMsg.To.Add(new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get(CUSTOMERCARE), "Staybazar"));
                    string emailids = ConfigurationManager.AppSettings.Get(BOOKINGCARTCC);
                    if (emailids != "")
                    {
                        string[] emails = emailids.Split(',');
                        foreach (string ToEMailId in emails)
                        {
                            mailMsg.CC.Add(new System.Net.Mail.MailAddress(ToEMailId));
                        }
                    }
                    Mailer mail = new Mailer();
                    //#if !DEBUG
                    mail.SendMail(mailMsg);
                    //throw new Exception("testing");
                    //#endif

                }

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

        }
    }
}