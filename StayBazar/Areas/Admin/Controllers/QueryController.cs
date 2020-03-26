using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc;
using System.Threading.Tasks;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
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
                if (id.HasValue)
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
                }else
                    ViewBag.MsgType = (int)CLayer.ObjectStatus.MsgType.Query;
                // return View();
                return RedirectToAction("Inbox", new { id = ViewBag.MsgType });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [AllowAnonymous]
        public ActionResult Posted()
        {
            return View();
        }
        [Common.AdminRolePermission]
        public ActionResult Inbox(int? id)
        {
            try
            {
                if (id.HasValue)
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
                }else
                    ViewBag.MsgType = (int)CLayer.ObjectStatus.MsgType.Query;
                List<CLayer.Query> lst = BLayer.Query.GetAll((CLayer.ObjectStatus.MsgType) ViewBag.MsgType);
                ViewBag.Query = new QueryModel() { Body = "", Subject = "", Phone = "", Email = "", Name = "", QueryId = 0,MsgType = ViewBag.MsgType, CreatedOn = DateTime.Now, Status = 0 };
                return View(lst);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Archivebox(int? id)
        {
            try
            {
                if (id.HasValue)
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
                else
                    ViewBag.MsgType = (int)CLayer.ObjectStatus.MsgType.Query;
                List<CLayer.Query> lst = BLayer.Query.GetArchive((CLayer.ObjectStatus.MsgType) ViewBag.MsgType);
                ViewBag.Query = new QueryModel() { Body = "", Subject = "", Phone = "", Email = "", Name = "", QueryId = 0,MsgType = ViewBag.MsgType, CreatedOn = DateTime.Now, Status = 0 };
                return View("Inbox", lst);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Edit(int id, int mgid)
        {
            try
            {
                if (Convert.ToBoolean( mgid))
                {
                    switch (mgid)
                    {
                        case (int)CLayer.ObjectStatus.MsgType.Query:
                            ViewBag.Head = "Query";
                            ViewBag.MsgType = mgid;
                            break;
                        case (int)CLayer.ObjectStatus.MsgType.Complaint:
                            ViewBag.Head = "Complaint";
                            ViewBag.MsgType = mgid;
                            break;
                        case (int)CLayer.ObjectStatus.MsgType.Feedback:
                            ViewBag.Head = "Feedback";
                            ViewBag.MsgType = mgid;
                            break;
                        default:
                            ViewBag.Head = "Query";
                            ViewBag.MsgType = (int)CLayer.ObjectStatus.MsgType.Query;
                            break;
                    }
                }
                else
                    ViewBag.MsgType = (int)CLayer.ObjectStatus.MsgType.Query;

                QueryModel qry = new QueryModel() { QueryId = 0 };
                CLayer.Query query = BLayer.Query.Get(id);
                if (query != null)
                    qry = new QueryModel()
                    {
                        QueryId = query.QueryId,
                        Name = query.Name,
                        Email = query.Email,
                        Phone = query.Phone,
                        Subject = query.Subject,
                        Body = query.Body,
                        CreatedOn = query.CreatedOn,
                        Status = query.Status
                    };
                if (qry.Status < 2)
                    BLayer.Query.SetStatus(id, (int)QueryModel.status.Viewed);
                return PartialView("_Query", qry);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [CaptchaMvc.Attributes.CaptchaVerify("Capcha Is  Not valid")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> Save(QueryModel data)
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
                    BLayer.Query.Save(query);
                    string querymail = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

                    //string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CUSTOMERQUERYCCMAILS);
                    //if (BccEmailsforsupp.Trim() != "")
                    //{
                    //    string[] emails = BccEmailsforsupp.Split(',');
                    //    for (int i = 0; i < emails.Length; ++i)
                    //    {
                    //        msg.Bcc.Add(emails[i]);
                    //    }
                    //}

                    msg.To.Add(querymail);
                    msg.ReplyToList.Add(data.Email);
                   // msg.From = new System.Net.Mail.MailAddress(querymail);
                    msg.Body = data.Body;
                    msg.Subject = data.Subject;
                    Common.Mailer ml = new Common.Mailer();
                    await ml.SendMailAsync(msg,Common.Mailer.MailType.Query);
                    return RedirectToAction("Posted");
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
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.RoleRequired(Administrator = true)]
        public ActionResult Delete(int id)
        {
            try
            {
                CLayer.Query q = BLayer.Query.Get(id);
                BLayer.Query.Delete(id);
                if (q.Status == (int)QueryModel.status.Archived)
                    return RedirectToAction("Archivebox");
                else
                    return RedirectToAction("Inbox");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Unread(int id)
        {
            try
            {
                BLayer.Query.SetStatus(id, (int)QueryModel.status.NotViewed);
                return RedirectToAction("Inbox");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Archive(int id)
        {
            try
            {
                BLayer.Query.SetStatus(id, (int)QueryModel.status.Archived);
                return RedirectToAction("Inbox");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Unarchive(int id)
        {
            try
            {
                BLayer.Query.SetStatus(id, (int)QueryModel.status.Viewed);
                return RedirectToAction("Inbox");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public string Actionset(string data, string type)
        {
            string action = "Inbox";

            try
            {

                if (type == "markusunread")
                {
                    BLayer.Query.SetStatus(data, (int)QueryModel.status.NotViewed);
                }
                if (type == "markusread")
                {
                    BLayer.Query.SetStatus(data, (int)QueryModel.status.Viewed);
                }
                if (type == "archive")
                {
                    BLayer.Query.SetStatus(data, (int)QueryModel.status.Archived);
                }
                if (type == "restore")
                {
                    action = "Archivebox";
                    BLayer.Query.SetStatus(data, (int)QueryModel.status.Viewed);
                }
                if (type == "delete")
                {
                    int status = 0;
                    if (data.IndexOf(",") > -1) { string[] ids = data.Split(','); status = BLayer.Query.Get(Convert.ToInt32(ids[0])).Status; }
                    else { status = BLayer.Query.Get(Convert.ToInt32(data)).Status; }
                    if (status == 2) { action = "Archivebox"; }

                    BLayer.Query.Delete(data);
                }
            }
            catch (Exception) { action = "0"; }

            return action;
        }
    }
}