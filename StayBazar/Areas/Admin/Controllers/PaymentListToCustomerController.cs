using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using StayBazar.Common;

namespace StayBazar.Areas.Admin.Controllers
{
    public class PaymentListToCustomerController : Controller
    {
        public const int ROWS_PER_PAGE = 25;
        [Common.AdminRolePermission]
        // GET: /Admin/PaymentListToCustomer
        public ActionResult Index()
        {
            Session["PaymentGuid"] = "";
            Models.PaymentListToCustomerModel data = new Models.PaymentListToCustomerModel();
            data.Limit = ROWS_PER_PAGE;
            //data.Start = 0;
            //data.SearchItem = 0;
            //data.Status = 1;
            data.Status = (int)CLayer.ObjectStatus.StatusType.Active;
            //data.TotalRows = 0;
            //int totalRows = 0;
            data.SearchString = "";
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.OfflineBooking> users = BLayer.B2B.SearchPaymentCustomerList(data.SearchString, 0, 25);
            data.userlist = users;
            data.TotalRows = 0;
            if (users.Count > 0)
            {
                data.TotalRows = users[0].TotalRows;
            }
            //data.SearchStatus = data.Status;

            data.SearchValue = 1;
            //data.ItemSearch = data.SearchItem;
            data.currentPage = 1;
            ViewBag.Filter = data;
            return View(data);
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        [HttpPost]
        //public ActionResult Filter(Models.PaymentListToCustomerModel data)
        public ActionResult Filter(string SearchString)
        {
            //if (data.SearchString == null) data.SearchString = "";
            if (SearchString == null) SearchString = "";

            //List<CLayer.OfflineBooking> users = BLayer.B2B.SearchPaymentCustomerList(data.SearchString, 0, 25);
            List<CLayer.OfflineBooking> users = BLayer.B2B.SearchPaymentCustomerList(SearchString, 0, 25);
            //List<CLayer.User> users = BLayer.User.GetAllCustomerForSearch((int)CLayer.ObjectStatus.StatusType.Active, data.SearchString, data.SearchValue, (int)CLayer.Role.Roles.Customer, 0, 25);
            ViewBag.Filter = new Models.PaymentListToCustomerModel();
            Models.PaymentListToCustomerModel forPager = new Models.PaymentListToCustomerModel()
            {

                //SearchString = data.SearchString,
                SearchString = SearchString,
                userlist = users,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        [HttpPost]
        public ActionResult Pager1(Models.PaymentListToCustomerModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.OfflineBooking> users = BLayer.B2B.SearchPaymentCustomerList(data.SearchString, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.PaymentListToCustomerModel();
            Models.PaymentListToCustomerModel forPager = new Models.PaymentListToCustomerModel()
            {
                Status = (int)CLayer.ObjectStatus.StatusType.Active,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                userlist = users,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        public string SetCustomerPaymentMode(long b2bId, int CustomerPaymentMode, decimal CustomerPaymentModeCreditDays = 0)
        {
            BLayer.User.SetCustomerPaymentMode(b2bId, CustomerPaymentMode, CustomerPaymentModeCreditDays);
            return "true";
        }
        [Common.AdminRolePermission]
        public ActionResult CustomerBookingList(long id)
        {
            LogHandler.AddLog("CustomerBookingList:-" + id.ToString());
            Session["CustomerId"] = id;
            string Cus_Email = "";
            Models.CustomerDetailModel details = null;
            //CLayer.User data = BLayer.User.Get(id);
            CLayer.OfflineBooking data = BLayer.OfflineBooking.GetOfflineBookingCustomerDetailsByID(id);
            LogHandler.AddLog("CustomerBookingList data read successfull:-" + DateTime.Now.ToString());
            if (data != null)
            {
                LogHandler.AddLog("If CustomerBookingList data is not null:-" + DateTime.Now.ToString());
                List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForPaymentList_Details(data.CustomerName);
                LogHandler.AddLog("users count:-" + DateTime.Now.ToString());
                //List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForPaymentList_Details(id);
                // Cus_Email = users.First().CustomerEmail;
                foreach (var item in users)
                {
                    if (Cus_Email == "")
                    {
                        if (item.PaymentLinkStatus == "checked" && item.CustomerEmail != "" && Cus_Email == "")
                        {
                            LogHandler.AddLog("PaymentLinkStatus is checked:-" + DateTime.Now.ToString());
                            Cus_Email = users.First().CustomerEmail;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (Cus_Email == "" && users.Count > 0)
                {
                    Cus_Email = users.First().CustomerEmail;
                    //Advance = users.First().AdvanceReceived;
                }
                Session["CustomerEmail"] = Cus_Email;
                details = new Models.CustomerDetailModel()
                {
                    UserId = id,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    Name = data.CustomerName,
                    Email = Cus_Email,
                    AdvanceReceived = 0,
                    OfflineBookingList = users,
                };
            }
            LogHandler.AddLog("CustomerBookingList returns successfull:-" + DateTime.Now.ToString());
            return View(details);
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public string Actionset(string useremailid, string data)
        {
            Session["SelectedId"] = data;
            string action = "Index";
            if (Session["PaymentGuid"].ToString() == "")
            {
                Session["PaymentGuid"] = Guid.NewGuid();
            }

            try
            {
                string LoggedInUser = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUser"]);
                BLayer.OfflineBooking.SetCusPaymentLinkStatus(data, LoggedInUser, (Guid)(Session["PaymentGuid"]));
                BLayer.OfflineBooking.CustomerPaymentDetailsUpdate(useremailid, data);
                //bool isValid = Sendemail(UserID, useremailid).Result;
                //var task = Sendemail(UserID, useremailid);
                //task.Wait();
                //isValid = task.Result;
            }
            catch (Exception) { action = "0"; }
            ViewBag.Message = "Your details updated successfully !!";
            return action;
        }
        [Common.AdminRolePermission]
        public ActionResult CustomerPaymentDetails()
        {
            long id = Convert.ToInt64(Session["CustomerId"]);
            long LoggedInUser = Convert.ToInt64(System.Web.HttpContext.Current.Session["LoggedInUser"]);
            CLayer.OfflineBooking data = BLayer.OfflineBooking.GetOfflineBookingCustomerDetailsByID(id);
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForPaymentList_Details(data.CustomerName);
            Session["CustomerEmail"] = users.First().CustomerEmail;
            Models.CustomerPaymentDetailModel details = null;
            details = new Models.CustomerPaymentDetailModel()
            {
                UserId = id,
                //SalutationId = data.SalutationId,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Name = data.CustomerName,
                //DateOfBirth = data.DateOfBirth.ToShortDateString(),
                //Status = data.Status,
                Email = users.First().CustomerEmail,
                OfflineBookingList = users,
            };
            return View(details);
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        public ActionResult CustomerAdvanceUpdate(long OfflineBookingId, long AdvanceAmt)
        {

            try
            {
                BLayer.OfflineBooking.CustomerLinkAdvUpdate(OfflineBookingId, AdvanceAmt);
            }
            catch (Exception) { ViewBag.Message = "Unable to update the details !!"; }
            ViewBag.Message = "Your details updated successfully !!";

            //return RedirectToAction("CustomerBookingList", new { id = customerid });
            return View();
        }
        //public async Task<bool> Sendemail(long UserId, string Email)
        public async Task<bool> Sendemail(long UserId, string Email)
        {
            try
            {
                if (UserId < 1) return false;
                //CLayer.OfflineBooking data = BLayer.OfflineBooking.GetOfflineBookingCustomerDetailsByID(UserId);
                try
                {
                    string message = "";
                    Common.Mailer ml = new Common.Mailer();

                    //if (data.CustomerEmail != "")
                    //{
                    LogHandler.AddLog("Mail process starts:-" + DateTime.Now.ToString());
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("PaymentRequest")
                                                                    + (Guid)Session["PaymentGuid"]);

                    System.Net.Mail.MailMessage PaymentMsg = new System.Net.Mail.MailMessage();
                    //PaymentMsg.To.Add(Session["CustomerEmail"].ToString());
                    PaymentMsg.To.Add(Email);
                    PaymentMsg.Subject = "Request for Payment";
                    PaymentMsg.Body = message;
                    PaymentMsg.IsBodyHtml = true;
                    try
                    {
                        LogHandler.AddLog("Mail Sending to reservation starts:-" + DateTime.Now.ToString());
                        await ml.SendMailAsync(PaymentMsg, Common.Mailer.MailType.Reservation);
                        LogHandler.AddLog("Mail Sending to reservation Ends:-" + DateTime.Now.ToString());
                    }
                    catch (Exception ex)
                    {
                        LogHandler.AddLog("Mail Sending Exception Eror(244):-" + DateTime.Now.ToString());
                        Common.LogHandler.HandleError(ex);
                    }
                    //}

                }
                catch (Exception ex)
                {
                    LogHandler.AddLog("Mail Sending Exception Eror(252):-" + DateTime.Now.ToString());
                    Common.LogHandler.HandleError(ex);
                }
            }
            catch (Exception ex)
            {
                LogHandler.AddLog("Mail Sending Exception Eror(258):-" + DateTime.Now.ToString());
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }
    }
}