using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using Newtonsoft.Json;

namespace StayBazar.Controllers
{
    public class SubmitFormController : Controller
    {
        // GET: SubmitForm
        [AllowAnonymous]
        public ActionResult Index(string webname)
        {
            Models.FormSubmitModel Model = new Models.FormSubmitModel();
            Model.Website = webname;
            return View(Model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> FormSubmit(Models.FormSubmitModel Model)
        {
            try
            {
                try
                {
                    if (Model != null)
                    {
                        CLayer.FormSubmitcs data = new CLayer.FormSubmitcs();
                        data.Name = Model.Name;
                        data.ContactNo = Model.ContactNo;
                        data.Email = Model.Email;
                        data.EventName = Model.EventName;
                        data.EventLocation = Model.EventLocation;
                        data.CheckIn = Convert.ToDateTime(Model.CheckIn).ToString("yyyy-MM-dd");
                        data.CheckOut = Convert.ToDateTime(Model.CheckOut).ToString("yyyy-MM-dd");
                        data.NoOfAdult = Model.NoOfAdult;
                        data.NoOfChild = Model.NoOfChild;
                        data.Website = Model.Website;
                        data.NoOfRooms = Model.NoOfRooms;
                        BLayer.Query.SaveQueryForm(data);

                    }
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }


                MailMessage mm = new MailMessage();
                string emailidsto = ConfigurationManager.AppSettings.Get("QueryMailTo");
                if (emailidsto != "")
                {
                    string[] emailsto = emailidsto.Split(',');
                    for (int i = 0; i < emailsto.Length; ++i)
                    {
                        mm.To.Add(emailsto[i]);
                    }
                }

             
                
                string emailidsbcc = ConfigurationManager.AppSettings.Get("QueryMailBcc");
                if (emailidsbcc != "")
                {
                    string[] emailsbcc = emailidsbcc.Split(',');
                    for (int i = 0; i < emailsbcc.Length; ++i)
                    {
                        mm.CC.Add(emailsbcc[i]);
                    }
                }
                string QuerySwimindiaTo = ConfigurationManager.AppSettings.Get("QuerySwimindiato");
                mm.CC.Add(QuerySwimindiaTo);


                mm.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                mm.Subject = "Query from SwimIndia User";
                mm.Body = "<strong>Name:</strong> " + Model.Name + "<br /><strong>Email Address:</strong> " + Model.Email + "<br /><strong>Contact No:</strong> " + Model.ContactNo + "<br /><strong>Meet Name:</strong> " + Model.EventName + "<br /><strong>Meet Location:</strong> " + Model.EventLocation + "<br /><strong>No Of Travellers:- </strong>" + "<br /><strong style='margin-left: 25px;'>Adult:</strong> " + Model.NoOfAdult + "<br /><strong style='margin-left: 25px;'>Children:</strong> " + Model.NoOfChild + "<br /><strong>No Of Rooms Required:</strong> " + Model.NoOfRooms + "<br /><strong>Check In Date:</strong> " + Model.CheckIn + "<br /><strong>Check Out Date:</strong> " + Model.CheckOut;
                mm.IsBodyHtml = true;
                Common.Mailer mlr = new Common.Mailer();
                await mlr.SendMailAsyncWithoutFields(mm);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }




            string message = "";
            message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("QueryMailCustomer") + Model.Name.ToString());
            MailMessage mm1 = new MailMessage();
            mm1.To.Add(Model.Email);


            string emailidsto1 = ConfigurationManager.AppSettings.Get("QueryMailTo");
            if (emailidsto1 != "")
            {
                string[] emailsto1 = emailidsto1.Split(',');
                for (int i = 0; i < emailsto1.Length; ++i)
                {
                    mm1.To.Add(emailsto1[i]);
                }
            }

            string emailidsbcc1 = ConfigurationManager.AppSettings.Get("QueryMailBcc");
            if (emailidsbcc1 != "")
            {
                string[] emailsbcc1 = emailidsbcc1.Split(',');
                for (int i = 0; i < emailsbcc1.Length; ++i)
                {
                    mm1.CC.Add(emailsbcc1[i]);
                }
            }

            mm1.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
            mm1.Subject = "Query Recieved";
            mm1.Body = message;
            mm1.IsBodyHtml = true;
            Common.Mailer mlr1 = new Common.Mailer();

            try
            {
                await mlr1.SendMailAsyncWithoutFields(mm1);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            return RedirectToAction("ThankYou", "SubmitForm");
        }
        [AllowAnonymous]
        public ActionResult ThankYou()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult CustomerMail(string name)
        {
            Models.SuggestModel ddd = new Models.SuggestModel();
            ddd.Name = name;
            return View("_CustomerMail", ddd);
        }
    }
}