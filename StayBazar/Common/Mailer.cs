using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Threading.Tasks;

namespace StayBazar.Common
{
    public class Mailer
    {
        private string _server;
        private string _port;
        private string _secured;
        private string _username;
        private string _password;


        public enum MailType
        {
            Reservation = 1,
            Query = 2,
            Support = 3
        }

        private bool LoadSettings()
        {
            bool result = true;

            _server = BLayer.Settings.GetValue(CLayer.Settings.SERVER);
            _port = BLayer.Settings.GetValue(CLayer.Settings.MAIL_PORT);
            _secured = BLayer.Settings.GetValue(CLayer.Settings.IS_SECURE);

            if (_server == "" || _port == "" || _secured == "") result = false;

            return result;
        }
        /// <summary>
        /// Send mail. cced to Customer care
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="sendAs"></param>
        /// <returns></returns>
        public async Task SendMailAsync(MailMessage msg, MailType sendAs, bool copyToCustomerCare = true)
        {
            switch (sendAs)
            {
                
                case MailType.Reservation:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.REGIS_EMAIL);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.REGIS_EMAIL_PASSWORD);
                    LogHandler.AddLog("SendMailAsync for the case Reservation ends successfully:-" + DateTime.Now.ToString());
                    LogHandler.AddLog("SendMailAsync for the case Reservation username is:-" + _username.ToString());
                    LogHandler.AddLog("SendMailAsync for the case Reservation password is:-" + _password.ToString());

                    break;
                case MailType.Query:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.QUERY_EMAIL_PASSWORD);
                    break;
                case MailType.Support:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL_PASSWORD);
                    break;
            }

            msg.From = new MailAddress(_username, "Staybazar");
            if (copyToCustomerCare)
            {
                string customercare = System.Configuration.ConfigurationManager.AppSettings.Get("CustomerCareMail");
                msg.Bcc.Add(customercare);
            }
            SmtpClient cl = GetServer();
            LogHandler.AddLog("Get server excecuted successfully:-" + DateTime.Now.ToString());
            await cl.SendMailAsync(msg);
            LogHandler.AddLog("SendMailAsync excecuted successfully:-" + DateTime.Now.ToString());

        }

        //Synchrous version
        public void SendMailAsyncForBookingSync(MailMessage msg, MailType sendAs, bool copyToCustomerCare = true)
        {
            switch (sendAs)
            {
                case MailType.Reservation:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.REGIS_EMAIL);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.REGIS_EMAIL_PASSWORD);
                    break;
                case MailType.Query:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.QUERY_EMAIL_PASSWORD);
                    break;
                case MailType.Support:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL_PASSWORD);
                    break;
            }

            msg.From = new MailAddress(_username, "Staybazar");

            SmtpClient cl = GetServer();
            //     await cl.SendMailAsync(msg);
            //  cl.Send(msg);

#if !DEBUG
            cl.Send(msg);
#endif
        }
        //asynchronous version
        public async Task SendMailAsyncForBooking(MailMessage msg, MailType sendAs, bool copyToCustomerCare = true)
        {
            switch (sendAs)
            {
                case MailType.Reservation:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.REGIS_EMAIL);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.REGIS_EMAIL_PASSWORD);
                    break;
                case MailType.Query:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.QUERY_EMAIL_PASSWORD);
                    break;
                case MailType.Support:
                    _username = BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL);
                    _password = BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL_PASSWORD);
                    break;
            }

            msg.From = new MailAddress(_username, "Staybazar");

            SmtpClient cl = GetServer();
            //     await cl.SendMailAsync(msg);
#if !DEBUG
            await cl.SendMailAsync(msg);
#endif
        }
        /// <summary>
        /// Send mail
        /// </summary>
        /// <param name="msg">message body</param>
        /// <returns></returns>
        public async Task SendMailAsync(MailMessage msg)
        {
            _username = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
            _password = BLayer.Settings.GetValue(CLayer.Settings.QUERY_EMAIL_PASSWORD);
            msg.From = new MailAddress(_username, "Staybazar");
            SmtpClient cl = GetServer();
#if !DEBUG
            await cl.SendMailAsync(msg);
#endif
        }

        /// <summary>
        /// Send mail. Add all the to,from,subject fields in the Message.Authentication - using QUERY EMAIL. 
        /// </summary>
        /// <param name="msg">message body</param>
        /// <returns></returns>
        public async Task SendMailAsyncWithoutFields(MailMessage msg)
        {
            _username = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
            _password = BLayer.Settings.GetValue(CLayer.Settings.QUERY_EMAIL_PASSWORD);
            msg.From = new MailAddress(_username, "Staybazar");
            SmtpClient cl = GetServer();
#if !DEBUG
            await cl.SendMailAsync(msg);
#endif
        }
        private SmtpClient GetServer()
        {
            LoadSettings();
            SmtpClient cl = new SmtpClient(_server, Convert.ToInt32(_port));
            LogHandler.AddLog("Getserver _port is:-" + _port.ToString());
            LogHandler.AddLog("Getserver _server is:-" + _server.ToString());
            CLayer.Settings.MailSecurityType mtype;
            mtype = BLayer.Settings.GetMailSecurityType(_secured);
            switch (mtype)
            {
                case CLayer.Settings.MailSecurityType.SimpleWithSSL:
                    LogHandler.AddLog("Get server for case SimpleWithSSL:-" + DateTime.Now.ToString());
                    //cl.UseDefaultCredentials = false;
                    LogHandler.AddLog("Get server for case SimpleWithSSL username is:-" + _username.ToString());
                    LogHandler.AddLog("Get server for case SimpleWithSSL password is:-" + _password.ToString());
                    cl.EnableSsl = true;
                    cl.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    cl.UseDefaultCredentials = false;
                    cl.Credentials = new System.Net.NetworkCredential(_username, _password);
                    LogHandler.AddLog("Get server for case SimpleWithSSL excecuted successfully:-" + DateTime.Now.ToString());
                    break;
                    

                case CLayer.Settings.MailSecurityType.SimpleAuthentication:
                    //    cl.UseDefaultCredentials = false;
                    LogHandler.AddLog("Get server for case SimpleAuthentication:-" + DateTime.Now.ToString());
                    cl.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    cl.Credentials = new System.Net.NetworkCredential(_username, _password);
                    break;
                case CLayer.Settings.MailSecurityType.NoAuthentication:
                    cl.UseDefaultCredentials = false;
                    LogHandler.AddLog("Get server for case No Authentication:-" + DateTime.Now.ToString());
                    cl.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    //cl.Credentials = new System.Net.NetworkCredential("karthikms", "Easy1234");
                    LogHandler.AddLog("Get server for case SimpleWithSSL username is:-" + _username.ToString());
                    LogHandler.AddLog("Get server for case SimpleWithSSL password is:-" + _password.ToString());
                    cl.Credentials = new System.Net.NetworkCredential(_username, _password);
                    LogHandler.AddLog("Get server for case SimpleWithSSL excecuted successfully:-" + DateTime.Now.ToString());
                    break;
            }

            return cl;
        }

        //Synchronous version
        public static string MessageFromHtmlSync(string url)
        {
            //System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            System.Net.WebClient wc = new System.Net.WebClient();

            string html = wc.DownloadString(url);
            html = html.Substring(html.IndexOf("<table"));
            int i, len;
            len = html.Length;
            i = html.LastIndexOf("</table>");
            html = html.Substring(0, len - (len - i) + 8);
            return html;
        }
        //Asynchornous version
        public static async Task<string> MessageFromHtml(string url)
        {
            LogHandler.AddLog("Process has entered MessageFromHtml function:-" + DateTime.Now.ToString());
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
            LogHandler.AddLog("client.GetStringAsync Functions Starts here:-" + DateTime.Now.ToString());
            string html = await client.GetStringAsync(url);
            LogHandler.AddLog("client.GetStringAsync Functions executes without error:-" + DateTime.Now.ToString());
            html = html.Substring(html.IndexOf("<table"));
            LogHandler.AddLog("Email design using table:-" + DateTime.Now.ToString());
            int i, len;
            len = html.Length;
            i = html.LastIndexOf("</table>");
            html = html.Substring(0, len - (len - i) + 8);
            return html;
        }
    }
}