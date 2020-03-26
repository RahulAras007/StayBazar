using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace Mailer.Common
{
    public class Mailer
    {
        private string _server;
        private string _port;
        private string _secured;
        private string _username;
        private string _password;

        private bool LoadSettings()
        {
            bool result = true;

            _server = ConfigurationManager.AppSettings.Get("MailServer");
            _port = ConfigurationManager.AppSettings.Get("Port");
            _secured = ConfigurationManager.AppSettings.Get("AuthType");
            _username = ConfigurationManager.AppSettings.Get("EmailId");
            _password = ConfigurationManager.AppSettings.Get("Auth");

            if (_server == "" || _port == "" || _secured == "") result = false;

            return result;
        }

        public void SendMail(MailMessage msg)
        {
            
       //     msg.From = new MailAddress(_username, "Staybazar");
            SmtpClient cl = GetServer();
//#if !DEBUG
            cl.Send(msg);
//#endif
        }

        private SmtpClient GetServer()
        {
            LoadSettings();
            SmtpClient cl = new SmtpClient(_server, Convert.ToInt32(_port));
            int sec = 1;
            int.TryParse(_secured, out sec);

            switch (sec)
            {
                case 2:
                    cl.EnableSsl = true;
                    // cl.UseDefaultCredentials = false;
                    cl.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    cl.Credentials = new System.Net.NetworkCredential(_username, _password);
                    break;
                case 1:
                    //    cl.UseDefaultCredentials = false;
                    cl.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    cl.Credentials = new System.Net.NetworkCredential(_username, _password);
                    break;
            }

            return cl;
        }

        public static string MessageFromHtml(string url)
        {
            WebClient wc = new WebClient();
            string html = wc.DownloadString(url);
            if (html == null || html == "") return "";
            int i, len;
            i = html.IndexOf("<!--START-->");
            if (i < 0) return "";
            html = html.Substring(i);
            
            len = html.Length;
            i = html.LastIndexOf("<!--END-->");
            html = html.Substring(0, len - (len - i) + 8);
            html = html.Replace("<!--START-->","");
            html = html.Replace("<!--END-->","");
            return html;
        }
    }
}
