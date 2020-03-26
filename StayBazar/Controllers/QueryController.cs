using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc;
using System.Threading.Tasks;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Controllers
{
    public class QueryController : Controller
    {
        //
        // GET: /Query/
        [AllowAnonymous]
        public ActionResult Index(int? id)
        {
            try
            {
                if (!id.HasValue)
                {
                    ViewBag.Head = "Query";
                    ViewBag.MsgType = (int)CLayer.ObjectStatus.MsgType.Query;
                }
                else
                {
                    switch (id.Value)
                    {
                        case (int)CLayer.ObjectStatus.MsgType.Query:
                            ViewBag.Head = "Query";
                            ViewBag.MsgType = id.Value;
                            break;
                        case (int)CLayer.ObjectStatus.MsgType.Complaint:
                            ViewBag.Head = "Complaint";
                            ViewBag.MsgType = id.Value;
                            break;
                        case (int)CLayer.ObjectStatus.MsgType.Feedback:
                            ViewBag.Head = "Feedback";
                            ViewBag.MsgType = id.Value;
                            break;
                        default:
                            ViewBag.Head = "Query";
                            ViewBag.MsgType = (int)CLayer.ObjectStatus.MsgType.Query;
                            break;

                    }
       
                }

                return View();
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return RedirectToAction("Index","ErrorPage");
            }
        }

        [CaptchaMvc.Attributes.CaptchaVerify("Capcha Is  Not valid")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Save(Models.QueryModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //if (this.IsCaptchaValid("Captcha is not valid"))
                    //{
                    CLayer.Query query = new CLayer.Query()
                    {
                        Name = data.Name,
                        Email = data.Email,
                        Phone = data.Phone,
                        Subject = data.Subject,
                        Body = data.Body,
                       MessageType = (CLayer.ObjectStatus.MsgType) data.MsgType
                    };
                    ViewBag.MsgType = data.MsgType;

                    BLayer.Query.Save(query);
                    try
                    {
                        string querymail = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        msg.To.Add(querymail);
                        msg.ReplyToList.Add(data.Email);
                        //  msg.From = new System.Net.Mail.MailAddress(querymail);
                        string msgt = "";
                        msgt = ((CLayer.ObjectStatus.MsgType)data.MsgType).ToString();
                        msg.Body = msgt + " : " +  data.Body;
                        if (data.MsgType == (int)CLayer.ObjectStatus.MsgType.Query)
                        {
                            string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CUSTOMERQUERYCCMAILS);
                            if (BccEmailsforsupp.Trim() != "")
                            {
                                string[] emails = BccEmailsforsupp.Split(',');
                                for (int i = 0; i < emails.Length; ++i)
                                {
                                    msg.CC.Add(emails[i]);
                                }
                            }
                        }
                        msg.Subject = data.Subject;
                        Common.Mailer ml = new Common.Mailer();

                        await ml.SendMailAsync(msg, Common.Mailer.MailType.Query);
                    }
                    catch (Exception ex) { Common.LogHandler.HandleError(ex); } 
                    return View("~/Views/Query/Posted.cshtml");
                }
                else
                {
                    ViewBag.Message = "Error: captcha is not valid.";
                    return View("Index", data);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return RedirectToAction("Index", "ErrorPage");
            }
        }
    }
}