using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Net.Security;
using System.Security.Claims;
using StayBazar.Models;
using StayBazar.Lib.Security;
using System.Linq;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class ContinueController : Controller
    {
        private void InitViewBag()
        {
            ViewBag.LoginError = false;
            ViewBag.GuestError = false;
            ViewBag.Message = "";
        }
                //
        // GET: /Continue/
        [AllowAnonymous]
        public ActionResult Index()
        {
            SimpleBookingModel data =(SimpleBookingModel)TempData["Bookingdata"];
            if(data !=null)
                {
                Session["BookingData"] = data.BookingData;
                Session["BookCheckIn"] = data.BookCheckIn;
                Session["BookCheckOut"] = data.BookCheckOut;
                Session["PropertyId"] = data.PropertyId;
            }

            if (Convert.ToInt64(TempData["CorpBookingID"]) > 0)
            {
                Session["CorpBookingID"] = Convert.ToInt64(TempData["CorpBookingID"]);
            }

            InitViewBag();
           
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Guest(Models.GuestModel data)
        {
            try
            {
                InitViewBag();
                if (ModelState.IsValid)
                {
                    long guestId = BLayer.User.CreateGuest(data.Email, data.Phone);
                    if (guestId > 0)
                    {
                        if (BLayer.User.GetStatus(guestId) == CLayer.ObjectStatus.StatusType.NotVerified)
                        {
                            string gd = Guid.NewGuid().ToString().Substring(0, 10);
                            UserManager<StayBazar.Lib.Security.IdentityUser> UserManager = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                            string hashedNewPassword = UserManager.PasswordHasher.HashPassword(gd);
                            BLayer.User.SetPassword(guestId, hashedNewPassword);
                            try
                            {
                                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                                msg.To.Add(data.Email);
                                //Send guest mail
                                string url = System.Configuration.ConfigurationManager.AppSettings.Get("GuestMail");
                                url = url + guestId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK) + "&pass=" + gd;
                                string htmlBody = await Common.Mailer.MessageFromHtml(url);
                                msg.Body = htmlBody;
                                msg.Subject = "Staybazar Account";
                                msg.IsBodyHtml = true;
                                Common.Mailer ml = new Common.Mailer();
                                try
                                {
                                    await ml.SendMailAsync(msg, Common.Mailer.MailType.Query);
                                }
                                catch (Exception ex)
                                { //TODO add the exception to mailer list
                                    Common.LogHandler.LogError(ex);
                                }
                            }
                            catch (Exception ex) { Common.LogHandler.LogError(ex); }
                        }
                        Session[CLayer.ObjectStatus.GUEST_ID_SESSION] = guestId;
                        CLayer.Address ad = BLayer.Address.GetOnUserId(guestId, (int)CLayer.Address.AddressTypes.Primary);
                        if (ad == null)
                        {
                            return RedirectToAction("SetAddress", "Continue");
                        }
                        if (ad.CountryId < 1 && ad.State < 1)
                        {
                            return RedirectToAction("SetAddress", "Continue");
                        }
                        return RedirectToAction("Book", "Booking");
                    }
                    ViewBag.GuestError = true;
                    ViewBag.Message = "Sorry we could not create a guest account. Try again later.";
                }
                else
                {
                    ViewBag.GuestError = true;
                    ViewBag.Message = "Sorry we could not create a guest account. Try again.";
                }

            }catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                ViewBag.GuestError = true;
                ViewBag.Message = "Could not create guest login. Technical failure.";
            }
            return View("Index");
        }

        public ActionResult SetAddress()
        {
            return View("GuestAddress",new Models.GuestAddressModel());
        }
        
        public ActionResult SaveAddress(Models.GuestAddressModel data)
        {
            try
            {
                long userId = 0;
                if(Session[CLayer.ObjectStatus.GUEST_ID_SESSION] != null)
                {
                    userId = (long) Session[CLayer.ObjectStatus.GUEST_ID_SESSION];
                }
                else
                {
                    return RedirectToAction("Index", "Continue"); 
                }

                Session["LoggedInUser"] = Convert.ToInt64(userId);
                Session["LoggedInEmail"] = BLayer.User.GetEmail(Convert.ToInt64(Session["LoggedInUser"]));
                Session["UserName"] = data.FirstName+" "+data.Lastname;

                BLayer.User.SetName(userId, data.FirstName, data.Lastname);
                CLayer.Address ad = BLayer.Address.GetOnUserId(userId, (int)CLayer.Address.AddressTypes.Primary);
                if(ad == null)
                {
                    ad = new CLayer.Address();
                    ad.UserId = userId;
                    ad.AddressId = 0;
                }
                    ad.AddressText = data.Address;
                    ad.City = data.City;
                    ad.CityId = data.CityId;
                    ad.State = data.State;
                    ad.CountryId = data.CountryId;
                    ad.Mobile = ad.Mobile;
                    ad.Phone = ad.Phone;
                    ad.ZipCode = data.Pincode;
                    if (data.City != null && data.City != "")
                        ad.City = data.City;
                    if (data.CityId > 0)
                        ad.City = BLayer.City.Get(data.CityId).Name;
                    ad.AddressType = (int)CLayer.Address.AddressTypes.Primary;
                    BLayer.Address.Save(ad);
                    return RedirectToAction("Book", "Booking");
         
            }catch(Exception ex)
            { Common.LogHandler.HandleError(ex); }
            return RedirectToAction("Book", "Booking");
        }


        //Partial Booking

        [AllowAnonymous]
        public ActionResult PIndex(long BookingId)
        {
            InitViewBag();
            ViewBag.BookingId = BookingId;
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PGuest(Models.GuestModel data)
        {
            try
            {
                InitViewBag();
                if (ModelState.IsValid)
                {
                    long guestId = BLayer.User.CreateGuest(data.Email, data.Phone);
                    if (guestId > 0)
                    {
                        if (BLayer.User.GetStatus(guestId) == CLayer.ObjectStatus.StatusType.NotVerified)
                        {
                            string gd = Guid.NewGuid().ToString().Substring(0, 10);
                            UserManager<StayBazar.Lib.Security.IdentityUser> UserManager = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                            string hashedNewPassword = UserManager.PasswordHasher.HashPassword(gd);
                            BLayer.User.SetPassword(guestId, hashedNewPassword);
                            try
                            {
                                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                                msg.To.Add(data.Email);
                                //Send guest mail
                                string url = System.Configuration.ConfigurationManager.AppSettings.Get("GuestMail");
                                url = url + guestId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK) + "&pass=" + gd;
                                string htmlBody = await Common.Mailer.MessageFromHtml(url);
                                msg.Body = htmlBody;
                                msg.Subject = "Staybazar Account";
                                msg.IsBodyHtml = true;
                                Common.Mailer ml = new Common.Mailer();
                                try
                                {
                                    await ml.SendMailAsync(msg, Common.Mailer.MailType.Query);
                                }
                                catch (Exception ex)
                                { //TODO add the exception to mailer list
                                    Common.LogHandler.LogError(ex);
                                }
                            }
                            catch (Exception ex) { Common.LogHandler.LogError(ex); }
                        }
                        Session[CLayer.ObjectStatus.GUEST_ID_SESSION] = guestId;
                        CLayer.Address ad = BLayer.Address.GetOnUserId(guestId, (int)CLayer.Address.AddressTypes.Primary);
                        if (ad == null)
                        {
                            return RedirectToAction("SetAddress", "Continue");
                        }
                        if (ad.CountryId < 1 && ad.State < 1)
                        {
                            return RedirectToAction("SetAddress", "Continue");
                        }
                        return RedirectToAction("PartialPayment", "Booking", new { bookingId= data.BookingId});
                    }
                    ViewBag.GuestError = true;
                    ViewBag.Message = "Sorry we could not create a guest account. Try again later.";
                }
                else
                {
                    ViewBag.GuestError = true;
                    ViewBag.Message = "Sorry we could not create a guest account. Try again.";
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                ViewBag.GuestError = true;
                ViewBag.Message = "Could not create guest login. Technical failure.";
            }
            return View("Index");
        }
	}
}