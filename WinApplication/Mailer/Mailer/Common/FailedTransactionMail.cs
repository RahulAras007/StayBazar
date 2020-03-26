using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;


namespace Mailer.Common
{
    public class FailedTransactionMail
    {
        private const string URL_FAILEDTRANSACTION = "FailedBookingLink";
        private const string CUSTOMERCARE = "FailedTransactionEmail";
        private const string FAILEDTRANSACTIONCC = "FailedTransactionCCMail";

        public void Start()
        {
            SystemLogger logger = new SystemLogger();
            Console.WriteLine("Fetching Failed transactions..");
            WebClient wc = new WebClient();
            string url = ConfigurationManager.AppSettings.Get(URL_FAILEDTRANSACTION);
            try
            {
                string result = wc.DownloadString(url);

                System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
                mailMsg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get("EmailId"), "Staybazar");
                mailMsg.IsBodyHtml = true;
                // mailMsg.Subject = ConfigurationManager.AppSettings.Get(MAIL_SUBJECT);
                mailMsg.Subject = "Notice: Failed Transactions Report";

                result = Mailer.MessageFromHtml(url);
                if (result != "")
                {
                    mailMsg.Body = result;
                    mailMsg.To.Add(new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get(CUSTOMERCARE), "Staybazar"));
                    string emailids = ConfigurationManager.AppSettings.Get(FAILEDTRANSACTIONCC);
                    if (emailids != "")
                    {
                        string[] emails = emailids.Split(',');
                        foreach (string ToEMailId in emails)
                        {
                            mailMsg.CC.Add(new System.Net.Mail.MailAddress(ToEMailId));
                        }
                    }
                    //mailMsg.CC.Add(new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get(FAILEDTRANSACTIONCC), "Staybazar"));
                    Mailer mail = new Mailer();
//#if !DEBUG
                    mail.SendMail(mailMsg);
                    //  throw new Exception("testing");
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
