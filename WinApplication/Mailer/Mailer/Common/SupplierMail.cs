using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;

namespace Mailer.Common
{
    public class SupplierMail
    {
        private const string URL_SUPPLIER_COUNT = "SupMailRecordCountLink";
        private const string URL_SUPPLIER_LIST = "SupMailRecordsLink";
        private const string URL_SUPPLIERMAIL = "SupEmailLink";
        private const string MAIL_SUBJECT = "SupMailSubject";
        private const string URL_SUPPROP_EMAILIDS = "SupPropEmailIdLink";
        private const string SB_MAIL = "EmailId";
        
        private const int RECORDS_LIMIT = 300;

        public void Start()
        {
            SystemLogger logger = new SystemLogger();
            Console.WriteLine("Fetching suppliers..");
            WebClient wc = new WebClient();
            string url = ConfigurationManager.AppSettings.Get(URL_SUPPLIER_COUNT);
          //  url += "from=" + (DateTime.Today.AddDays(-1)).ToString("dd/MM/yyyy");
            string result = wc.DownloadString(url);
            int cnt = 0;
            int.TryParse(result, out cnt);
            Console.WriteLine(cnt + " suppliers found.");
            if (cnt == 0) return;
            url = ConfigurationManager.AppSettings.Get(URL_SUPPLIER_LIST);
            url += "limit=" + RECORDS_LIMIT.ToString() + "&start=";
            string customercare = ConfigurationManager.AppSettings.Get(SB_MAIL);
            int cpage = 0;
            string ids = "";
            long sid;
            string[] strids;
            string supEmailUrl = ConfigurationManager.AppSettings.Get(URL_SUPPLIERMAIL);
            supEmailUrl += "supplierId=";
            //&SupplierId=310&FromDate=16/11/2014&
            string propEmailUrl = ConfigurationManager.AppSettings.Get(URL_SUPPROP_EMAILIDS);
            string[] supData;
            string[] propData;
            string[] propIds;
            string pid,pemail;

            bool foundEmptyPropertyEmail = false;
            Common.Mailer supplierMailer = new Mailer();
            System.Net.Mail.MailMessage mailMsg = new System.Net.Mail.MailMessage();
            mailMsg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings.Get("EmailId"), "Staybazar");
            mailMsg.IsBodyHtml = true;
            mailMsg.Subject = ConfigurationManager.AppSettings.Get(MAIL_SUBJECT);

            //string toMail = "";
            string supMail = "";
            Console.WriteLine("Sending Mails..");

            while (cnt > 0)
            {
                ids = wc.DownloadString(url + (cpage * RECORDS_LIMIT).ToString());
                if (ids == null) return;
                if (ids.Trim() == "") return;
                strids = ids.Split(',');
                
                    foreach (string id in strids)
                    {
                        try
                        { 
                            supData = id.Split('#');
                            if (supData.Length != 2) continue;
               
                            
                            sid = 0;
                            long.TryParse(supData[0], out sid);
                            //request html body
                            foundEmptyPropertyEmail = false;
                            //send mail
                            if (sid > 0)
                            {
                              supMail = supData[1].Trim();
#if DEBUG
                        //    toMail = "testmail@gmail.com";
#endif
                                ids = wc.DownloadString(propEmailUrl + supData[0].Trim());
                                propIds = ids.Split(',');
                                
                                foreach(string propId in propIds)
                                {
                                    propData = propId.Split('#');
                                    if (propData.Length != 2) continue;
                                    pemail = propData[0].Trim();
                                    pid = propData[1].Trim();
                                    if(pemail=="none")
                                    {
                                        foundEmptyPropertyEmail = true;
                                        continue;
                                    }
                                    mailMsg.Body = Common.Mailer.MessageFromHtml(supEmailUrl + sid.ToString() + "&propertyId=" + pid );
                                    //send seperate mail to property mail Id , cc to supplier email id
                                    if (mailMsg.Body != "")
                                    {
                                        mailMsg.To.Clear();
                                        mailMsg.To.Add(pemail);
                                        mailMsg.CC.Clear();
                                        mailMsg.CC.Add(supMail);
                                        mailMsg.CC.Add(customercare);
                                        try
                                        {
                                            supplierMailer.SendMail(mailMsg);
                                        }
                                        catch (Exception ex) { logger.LogError(ex.Message); }
                                    }
                                }

                                if(foundEmptyPropertyEmail)
                                {
                                    mailMsg.Body = Common.Mailer.MessageFromHtml(supEmailUrl + sid.ToString() + "&propertyId=0");
                                    if (mailMsg.Body != "")
                                    {
                                        mailMsg.To.Clear();
                                        mailMsg.To.Add(supMail);
                                        mailMsg.CC.Clear();
                                        mailMsg.CC.Add(customercare);
                                        try
                                        {
                                            supplierMailer.SendMail(mailMsg);
                                        }
                                        catch (Exception ex) { logger.LogError(ex.Message); }
                                    }
                                }
                            }
                        }
                        catch (Exception ex) { logger.LogError(ex.Message); }
                    }
                
                //increments
                cnt = cnt - RECORDS_LIMIT;
                if (cnt < 0) cnt = 0;
                
                cpage++;
            }

        }
    }
}
