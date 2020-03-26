using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;

namespace Mailer.Common
{
    public class PartialPaymentBCancellation
    {

        private const string URL_PARTIALPAYMENTCANCEL = "PartialPaymentCancelLink";
        private const string URL_PARTIALPAYMENTCANCEL_LIST = "PartialPaymentBCanceList";
        private const string MAIL_CC = "PartialReminderCC";
        public void Start()
        {
            SystemLogger logger = new SystemLogger();
            Console.WriteLine("Fetching PartialPayment..");
            WebClient wc = new WebClient();

            string url = ConfigurationManager.AppSettings.Get(URL_PARTIALPAYMENTCANCEL_LIST);
            string urlbody = ConfigurationManager.AppSettings.Get(URL_PARTIALPAYMENTCANCEL);
            string mailccs = ConfigurationManager.AppSettings.Get(MAIL_CC);

            string ids = "";
            string[] strids;
            string[] payData;
            string Bookid, Useremail;


            Common.Mailer PartialPaymentMailer = new Mailer();
            System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            mailMsg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get("EmailId"), "Staybazar");
            mailMsg.IsBodyHtml = true;
            mailMsg.Subject = "Booking Cancellation";
            if(mailccs != null && mailccs != "")
            {
                mailccs = mailccs.Trim();
                if(mailccs != ""){
                strids = mailccs.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries);
               
                foreach(string s in strids)
                {
                    if (s != null && s != "")
                    {
                        mailMsg.Bcc.Add(s);
                    }
                }
                }
            }
            //mailMsg.CC.Add(mailccs);
            Console.WriteLine("Sending Mails..");

            ids = wc.DownloadString(url);
            if (ids == null) return;
            if (ids.Trim() == "") return;
            strids = ids.Split(',');
            foreach (string paydt in strids)
            {
                payData = paydt.Split('#');
                if (payData.Length != 2) continue;
                Useremail = payData[1].Trim();
                Bookid = payData[0].Trim();
                string result = wc.DownloadString(urlbody + Bookid);
                mailMsg.Body = result;
                if (mailMsg.Body != "")
                {
                    mailMsg.To.Clear();
                    mailMsg.To.Add(Useremail);

                    try
                    {
                        PartialPaymentMailer.SendMail(mailMsg);
                    }
                    catch (Exception ex) { logger.LogError(ex.Message); }
                }
            }
        }
    }
}