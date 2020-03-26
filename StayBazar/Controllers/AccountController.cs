using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Net.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using StayBazar.Models;
using StayBazar.Lib.Security;

namespace StayBazar.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore()))
        //: this(new UserManager<IdentityUser>(new UserStore<IdentityUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<StayBazar.Lib.Security.IdentityUser> userManager)
        {
            UserManager = userManager;
            UserManager.UserValidator = new UserValidator<StayBazar.Lib.Security.IdentityUser>(userManager) { AllowOnlyAlphanumericUserNames = false };
        }

        public UserManager<StayBazar.Lib.Security.IdentityUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Session["EnableExternalAuth"] = true;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult LoginForBookingApproval(string returnUrl)
        {
            Session["EnableExternalAuth"] = true;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        public ActionResult MyStays(string returnUrl)
        {
            Session["EnableExternalAuth"] = true;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult ClientPartners()
        {

            return View("~/Views/Widget/_ClientPartners.cshtml");
        }

        [AllowAnonymous]
        public ActionResult Forgot()
        {
            Models.ForgotPasswordModel data = new ForgotPasswordModel();
            return View("Forgot", data);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [CaptchaMvc.Attributes.CaptchaVerify("Code is not valid")]
        public async Task<ActionResult> Reset(Models.ForgotPasswordModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    long id = BLayer.User.GetUserId(data.Email);
                    if (id > 0)
                    {
                        string newPass = Guid.NewGuid().ToString();
                        newPass = (newPass.Replace("-", "")).Substring(0, 6);
                        UserManager<StayBazar.Lib.Security.IdentityUser> usrmngr = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                        String hashedNewPassword = usrmngr.PasswordHasher.HashPassword(newPass);
                        BLayer.User.SetPassword(id, hashedNewPassword);
                        string key = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                        string htmlBody = "";
                        htmlBody = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("PassReset") + id.ToString() + "&code=" + newPass + "&key=" + key);

                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        msg.To.Add(data.Email);
                        msg.Body = htmlBody;
                        msg.Subject = "Password Reset";
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
                        data.Status = true;
                    }
                    else
                    {
                        data.ErrorMsg = "We could not find the Email specified.";
                    }

                }
                else
                {
                    ModelState.AddModelError("Email", "Invalid Email");
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                data.ErrorMsg = "Sorry, we could not process your request. Try after sometime.";
            }
            return View("Forgot", data);
        }
        //[AllowAnonymous]
        //public async Task<ActionResult> ResetLink(string token)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return View("Reset", data);
        //}

        [AllowAnonymous]
        public ActionResult BusinessLogin(string returnUrl, int? type)
        {
            //if (type.HasValue)
            //{

            ViewBag.ReturnUrl = returnUrl;
            ViewBag.type = type;

            return View("Login");
            // }
            //else
            //{
            //    //ViewBag.ReturnUrl = type.Value;
            //    return View();
            //}
        }
        [AllowAnonymous]
        public ActionResult Home()
        {
            //ViewBag.ReturnUrl = type.Value;
            return View();
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (model.UserName == null) model.UserName = "";
                if (model.Password == null) model.Password = "";

                model.UserName = model.UserName.Trim();
                model.Password = model.Password.Trim();

                var user = await UserManager.FindAsync(model.UserName, model.Password);

                if (user != null)
                {
                    long userId = Convert.ToInt32(user.Id);
                    string email = BLayer.User.GetEmail(userId);
                    long usertype = BLayer.User.GetRole(email);
                    List<CLayer.Role> rls = BLayer.RolePermissions.GetAllAdminSide();

                    Session["LoggedInUser"] = userId;
                    Session["LoggedInEmail"] = email;
                    Session["UserName"] = user.UserName;

                    List<long> roletypes = rls.Select(col => Convert.ToInt64(col.Id)).ToList();

                    if (usertype == (int) CLayer.Role.Roles.Staff || usertype == (int) CLayer.Role.Roles.SalesPerson || usertype == (int) CLayer.Role.Roles.Administrator
                        || roletypes.Contains(usertype))
                    {
                        await SignInAsync(user, model.RememberMe);
                        long id = 0;
                        if (long.TryParse(user.Id, out id))
                        {
                            BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                        }
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }else if (usertype == (int)CLayer.Role.Roles.Customer)
                    {
                        var status = BLayer.User.GetStatus(userId);
                        if (status != CLayer.ObjectStatus.StatusType.NotVerified && status != CLayer.ObjectStatus.StatusType.Blocked)
                        {
                            await SignInAsync(user, model.RememberMe);
                            long id = 0;
                            if (long.TryParse(user.Id, out id))
                            {
                                BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                            }
                            if (returnUrl != null && returnUrl.ToLower().Contains("logoff")) returnUrl = null;
                            if (returnUrl == null)
                                return RedirectToAction("Index", "Home");
                            else
                                return RedirectToLocal(returnUrl);

                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid email/password or not verified");
                        }
                    }
                    else
                    {
                        await SignInAsync(user, model.RememberMe);
                        long id = 0;
                        if (long.TryParse(user.Id, out id))
                        {
                            BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                        }
                        if (returnUrl != null && returnUrl.ToLower().Contains("logoff")) returnUrl = null;
                        return RedirectToLocal(returnUrl);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public   async Task<bool> GetLogin(LoginViewModel model, string returnUrl)
        {
            
            bool result=false ;
            

            if (ModelState.IsValid)
            {
                if (model.UserName == null) model.UserName = "";
                if (model.Password == null) model.Password = "";

                model.UserName = model.UserName.Trim();
                model.Password = model.Password.Trim();

                var user = await UserManager.FindAsync(model.UserName, model.Password);

                if (user != null)
                {
                    long userId = Convert.ToInt32(user.Id);
                    string email = BLayer.User.GetEmail(userId);
                    long usertype = BLayer.User.GetRole(email);
                    List<CLayer.Role> rls = BLayer.RolePermissions.GetAllAdminSide();

                    Session["LoggedInUser"] = userId;
                    Session["LoggedInEmail"] = email;
                    Session["UserName"] = user.UserName;

                    List<long> roletypes = rls.Select(col => Convert.ToInt64(col.Id)).ToList();

                    if (usertype == (int)CLayer.Role.Roles.Staff || usertype == (int)CLayer.Role.Roles.SalesPerson || usertype == (int)CLayer.Role.Roles.Administrator
                        || roletypes.Contains(usertype))
                    {
                        await SignInAsync(user, model.RememberMe);
                        long id = 0;
                        if (long.TryParse(user.Id, out id))
                        {
                            BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                        }
                        //   return RedirectToAction("Index", "Home", new { area = "Admin" });
                        result = true;
                        return result;
                    }
                    else if (usertype == (int)CLayer.Role.Roles.Customer)
                    {
                        var status = BLayer.User.GetStatus(userId);
                        if (status != CLayer.ObjectStatus.StatusType.NotVerified && status != CLayer.ObjectStatus.StatusType.Blocked)
                        {
                            await SignInAsync(user, model.RememberMe);
                            long id = 0;
                            if (long.TryParse(user.Id, out id))
                            {
                                BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                            }
                            if (returnUrl != null && returnUrl.ToLower().Contains("logoff")) returnUrl = null;
                            if (returnUrl == null)
                            //return RedirectToAction("Index", "Home");
                            {
                                result = false ;
                                return result;
                            }
                            else
                            {
                                result = true;
                                return result;
                                 //RedirectToLocal(returnUrl);
                            }
                          


                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid email/password or not verified");
                        }
                    }
                    else
                    {
                        await SignInAsync(user, model.RememberMe);
                        long id = 0;
                        if (long.TryParse(user.Id, out id))
                        {
                            BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                        }
                        if (returnUrl != null && returnUrl.ToLower().Contains("logoff")) returnUrl = null;
                        //  return RedirectToLocal(returnUrl);
                        result = true;
                        return result;
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return result;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginForBooking(LoginViewModel model)
        {
            ViewBag.LoginError = false;
            ViewBag.GuestError = false;
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    long id = 0;
                    if (long.TryParse(User.Identity.GetUserId(), out id))
                    {
                        BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                    }
                    // SimpleBookingModel data = (SimpleBookingModel)frm("BookingData");
                    // TempData["Bookingdata"] = data;

                    Session["LoggedInUser"] =Convert.ToInt64(user.Id);
                    Session["LoggedInEmail"] = BLayer.User.GetEmail(Convert.ToInt64(Session["LoggedInUser"]));
                    Session["UserName"] = user.UserName;

                    return RedirectToAction("Book", "Booking");
                }
                else
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "Invalid email or password.";
                }
            }

            // If we got this far, something failed, redisplay form
            return View("~/Views/Continue/Index.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LoginForBookingApproval(LoginViewModel model)
        {
            ViewBag.LoginError = false;
            ViewBag.GuestError = false;
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    long id = 0;
                    if (long.TryParse(User.Identity.GetUserId(), out id))
                    {
                        BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                    }
                    // SimpleBookingModel data = (SimpleBookingModel)frm("BookingData");
                    // TempData["Bookingdata"] = data;

                    Session["LoggedInUser"] = Convert.ToInt64(user.Id);
                    Session["LoggedInEmail"] = BLayer.User.GetEmail(Convert.ToInt64(Session["LoggedInUser"]));
                    Session["UserName"] = user.UserName;

                    return RedirectToAction("Index", "BookingApproval");
                }
                else
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "Invalid email or password.";
                }
            }

            // If we got this far, something failed, redisplay form
            return View("~/Views/Continue/Index.cshtml");
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MyStays(LoginViewModel model)
        {
            ViewBag.LoginError = false;
            ViewBag.GuestError = false;
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    long id = 0;
                    if (long.TryParse(User.Identity.GetUserId(), out id))
                    {
                        BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                    }
                    // SimpleBookingModel data = (SimpleBookingModel)frm("BookingData");
                    // TempData["Bookingdata"] = data;

                    Session["LoggedInUser"] = Convert.ToInt64(user.Id);
                    Session["LoggedInEmail"] = BLayer.User.GetEmail(Convert.ToInt64(Session["LoggedInUser"]));
                    Session["UserName"] = user.UserName;

                    return RedirectToAction("Index", "BookingHistory");
                }
                else
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "Invalid email or password.";
                }
            }

            // If we got this far, something failed, redisplay form
            return View("~/Views/Continue/Index.cshtml");
        }

        //Partial Booking

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PLoginForBooking(LoginViewModel model)
        {
            ViewBag.LoginError = false;
            ViewBag.GuestError = false;
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    long id = 0;
                    if (long.TryParse(User.Identity.GetUserId(), out id))
                    {
                        BLayer.User.SetLoginDate(id, DateTime.Today.ToShortDateString());
                    }
                    return RedirectToAction("PartialPayment", "Booking", new { bookingId = model.BookingId });
                }
                else
                {
                    ViewBag.LoginError = true;
                    ViewBag.Message = "Invalid email or password.";
                }
            }

            // If we got this far, something failed, redisplay form
            return View("~/Views/Continue/Index.cshtml");
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register(string Thanks)
        {
            if (Thanks != null && Thanks == "Thank-You")
            {
                return View(new UserModel() { UserId = 1 });
            }

            Models.UserModel usr = new UserModel() { UserId = 0 };
            return View(usr);
        }

        ////
        //// POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new StayBazar.Lib.Security.IdentityUser() { UserName = model.UserName };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            //user.Id= resul
        //            await SignInAsync(user, isPersistent: false);
        //            return RedirectToAction("Index", "Home");
        //        }
        //        else
        //        {
        //            AddErrors(result);
        //        }
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // POST: /Account/Register
        [CaptchaMvc.Attributes.CaptchaVerify("Capcha Is  Not valid")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public async Task<ActionResult> Register(UserModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Message = null;
                    string tFirstName = model.FirstName;
                    string tLastName = "";
                    if (model.Businessname != null)
                    {
                        string tempName = model.Businessname;
                        int firstspace = tempName.IndexOf(' ');
                        if (firstspace > 1)
                            if (firstspace < model.Businessname.Length - 1)
                            {
                                tFirstName = tempName.Split(' ')[0].ToString();
                                tLastName = tempName.Substring(firstspace + 1, tempName.Length - tFirstName.Length - 1);
                            }
                    }

                    if (model.FirstName == null)
                    {
                        model.FirstName = tFirstName;
                    }
                    CLayer.User usr = new CLayer.User()
                    {
                        UserId = 0,
                        SalutationId = model.SalutationId,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Businessname = model.Businessname,
                        Email = model.Email,
                        Mobile = model.Moblie,
                        Phone = model.Phone,
                        ZipCode = model.ZipCode,

                        UserTypeId = (int)CLayer.Role.Roles.Customer,
                        Status = (int)CLayer.ObjectStatus.StatusType.NotVerified
                    };
                    model.UserId = BLayer.User.Save(usr);

                    if (model.UserId > 0)
                    {
                        CLayer.Address adrs = new CLayer.Address()
                        {
                            AddressId = model.AddressId,
                            UserId = model.UserId,
                            AddressText = model.Address,
                            CityId = model.CityId,
                            State = model.State,
                            CountryId = model.CountryId,
                            Phone = model.Phone,
                            Mobile = model.Moblie,
                            ZipCode = model.ZipCode,
                            AddressType = (int)CLayer.Address.AddressTypes.Primary
                        };
                        if (model.City != null && model.City != "")
                            adrs.City = model.City;
                        if (model.CityId > 0)
                            adrs.City = BLayer.City.Get(model.CityId).Name;
                        BLayer.Address.Save(adrs);

                        UserManager<StayBazar.Lib.Security.IdentityUser> usrmngr = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                        String hashedNewPassword = usrmngr.PasswordHasher.HashPassword(model.Password);
                        BLayer.User.SetPassword(model.UserId, hashedNewPassword);
                        Guid token = Guid.NewGuid();
                        BLayer.User.SetToken(model.UserId, token.ToString());
                        // string querymail = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
                        System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                        msg.To.Add(model.Email);

                        // add bcc for Customercommunications
                        string BccEmailsforcus = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                        if (BccEmailsforcus != "")
                        {
                            string[] emails = BccEmailsforcus.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                msg.Bcc.Add(emails[i]);
                            }
                        }

                        //     msg.From = new System.Net.Mail.MailAddress(querymail);
                        //  string url = Request.Url.ToString().Replace("Register", "Verify").ToString() + "?token=" + token;
                        //System.IO.TextReader txt= new  System.IO.StreamReader(Server.MapPath("~/Files/Mailer/verification.config"));
                        //string emailFormat = txt.ReadToEnd();
                        //txt.Close();
                        //emailFormat = emailFormat.Replace("#url", url);
                        string url = System.Configuration.ConfigurationManager.AppSettings.Get("B2CRegistration");
                        url = url + model.UserId + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                        string htmlBody = await Common.Mailer.MessageFromHtml(url);
                        msg.Body = htmlBody;
                        msg.Subject = "Staybazar Account Verification";
                        msg.IsBodyHtml = true;
                        Common.Mailer ml = new Common.Mailer();
                        try
                        {
                            await ml.SendMailAsyncForBooking(msg, Common.Mailer.MailType.Query);
                        }
                        catch (Exception ex)
                        { //TODO add the exception to mailer list
                            Common.LogHandler.LogError(ex);
                        }
                        ViewBag.Message = "Your staybazar account has been created successfully. A verification mail has been sent to " + model.Email;

                        return RedirectToAction("Register", "Account", new { Thanks = "Thank-You" });
                        //Response.Redirect("/Account/Register/Thank-You");
                        //return View(new UserModel() { UserId = model.UserId });

                        //  return RedirectToAction("");
                    }
                    else
                    {
                        ViewBag.Message = "The email id already used by someone else";

                    }
                    return View(model);
                }
                ViewBag.Message = "Something failed please try again";
                return View(model);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Errorpage");
            }
        }

        //
        // GET: /Account/Verify
        [AllowAnonymous]
        public ActionResult Verify(string token)
        {
            if (token == null)
            {
                ViewBag.Message = "Invalid verification request. No token found.";
            }
            else
            {
                bool success = BLayer.User.Verify(token);
                if (success)
                {
                    ViewBag.Message = "Your account has been verified successfully.";
                }
                else
                {
                    ViewBag.Message = "Invalid verification token.";
                }
            }
            return View();
        }
        private bool IsBusiness()
        {
            bool status = false;
            long l = 0;
            long.TryParse(User.Identity.GetUserId(), out l);
            if (l > 0)
            {
                CLayer.Role.Roles rle = BLayer.User.GetRole(l);
                switch (rle)
                {
                    case CLayer.Role.Roles.Affiliate:
                    case CLayer.Role.Roles.Agent:
                    case CLayer.Role.Roles.Corporate:
                    case CLayer.Role.Roles.Supplier:
                        status = true;
                        break;
                    default:
                        status = false;
                        break;
                }
            }

            return status;
        }

        private bool IsCorporateUser()
        {
            bool status = false;
            long l = 0;
            long.TryParse(User.Identity.GetUserId(), out l);
            if (l > 0)
            {
                CLayer.Role.Roles rle = BLayer.User.GetRole(l);
                if (rle == CLayer.Role.Roles.CorporateUser)
                {
                    status = true;
                }

            }
            return status;
        }

        public new ActionResult Profile()
        {
            //int userid
            long userid = Convert.ToInt64(User.Identity.GetUserId());
            CLayer.User usr = BLayer.User.Get(userid);

            string bname = "";
            if (IsBusiness())
            {
                CLayer.B2B data1 = BLayer.B2B.Get(userid);
                if (data1 == null)
                {
                    bname = "";
                }
                else
                {
                    bname = data1.Name;
                }
            }

            Models.ProfileModel data = new ProfileModel()
            {

                UserId = usr.UserId,
                SalutationId = usr.SalutationId,
                FirstName = usr.FirstName,
                LastName = usr.LastName,
                Businessname = bname,

                //ServiceTaxRegNo = data1.ServiceTaxRegNo,
                // DateOfBirth = usr.DateOfBirth.ToShortDateString(),
                Sex = usr.Sex,
                Email = usr.Email,
                Status = usr.Status,
                UserTypeId = usr.UserTypeId,
                AddressId = 0,
                Mobile = usr.Mobile,
                ZipCode = usr.ZipCode,
                Phone = usr.Phone

            };

            if (IsCorporateUser())
            {
                if (userid > 0)
                {
                    long corpUserId = BLayer.B2B.GetCorporateIdOfUser(userid);
                    if (corpUserId > 0)
                    {
                        string BusinessName1 = BLayer.B2B.GetBusinessName(corpUserId);
                        data.Businessname = BusinessName1;
                        List<CLayer.Address> Businessaddress = BLayer.Address.GetOnUser(corpUserId);
                        data.Busineessaddressarray = Businessaddress[0];
                    }
                }
            }


            CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(userid);
            if (adrs != null)
            {
                data.AddressId = adrs.AddressId;
                data.Address = adrs.AddressText;
                data.CityId = adrs.CityId;
                data.City = adrs.City;
                data.State = adrs.State;
                data.CountryId = adrs.CountryId;
                data.Phone = adrs.Phone;
                data.Mobile = adrs.Mobile;
                data.ZipCode = adrs.ZipCode;
                data.AddressType = adrs.AddressType;
            }

            if (data.State > 0)
            {
                List<CLayer.City> cities = null;
                cities = BLayer.City.GetOnState(data.State);
                cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                data.CityList = new SelectList(cities, "CityId", "Name");
            }
            if (usr.UserTypeId == (int)CLayer.Role.Roles.Corporate)
            {
                CLayer.Address BillingAddress = BLayer.Address.GetBillingAddress(userid);
                if (BillingAddress != null)
                {
                    data.BillingAddressType = BillingAddress.BillingAddressType;
                    data.BillingAddressId = BillingAddress.BillingAddressId;
                    data.BillingAddress = BillingAddress.BillingAddress;
                    data.BillingCityId = BillingAddress.BillingCityId;
                    data.BillingState = BillingAddress.BillingState;
                    data.BillingCity = BillingAddress.BillingCity;
                    data.BillingCountryId = BillingAddress.BillingCountryId;
                    data.BillingZipCode = BillingAddress.BillingZipCode;
                    data.IsClicked = false;
                }
                else
                {
                    data.BillingAddress = data.Address;
                    data.BillingAddressId = 0;
                    data.BillingAddress = data.Address;
                    data.BillingCityId = data.CityId;
                    data.BillingCity = data.City;
                    data.BillingState = data.State;
                    data.BillingCountryId = data.CountryId;
                    data.Phone = data.Phone;
                    data.Mobile = data.Mobile;
                    data.BillingAddressType = (int)CLayer.Address.AddressTypes.Primary;
                    data.BillingZipCode = data.ZipCode;
                    data.IsClicked = true;
                }

                if (data.BillingZipCode == data.ZipCode && data.BillingAddress == data.Address && data.BillingCityId == data.CityId && data.BillingCity == data.City && data.BillingState == data.State && data.BillingCountryId == data.CountryId)
                    data.IsClicked = true;
            }

            if (usr.UserTypeId == (int)CLayer.Role.Roles.Supplier)
            {
                CLayer.BankAccount acnt = BLayer.BankAccount.GetOnUser(userid);
                if (acnt != null)
                {
                    data.BankAccountId = acnt.BankAccountId;
                    data.AccountNumber = acnt.AccountNumber;
                    data.BankName = acnt.BankName;
                    data.BranchAddress = acnt.BranchAddress;
                    data.RTGSNumber = acnt.RTGSNumber;
                    data.MICRCode = acnt.MICRCode;
                }
            }
            if (usr.UserTypeId == (int)CLayer.Role.Roles.Affiliate)
            {
                CLayer.BankAccount acnt = BLayer.BankAccount.GetOnUser(userid);
                if (acnt != null)
                {
                    data.BankAccountId = acnt.BankAccountId;
                    data.AccountNumber = acnt.AccountNumber;
                    data.BankName = acnt.BankName;
                    data.BranchAddress = acnt.BranchAddress;
                    data.RTGSNumber = acnt.RTGSNumber;
                    data.MICRCode = acnt.MICRCode;
                }
            }
            data.LoadPlaces();
            return View(data);
        }

        public ActionResult Update(ProfileModel data)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    long userid = Convert.ToInt64(User.Identity.GetUserId());
                    if (IsCorporateUser())
                    {
                        if (userid > 0)
                        {
                            long corpUserId = BLayer.B2B.GetCorporateIdOfUser(userid);
                            if (corpUserId > 0)
                            {
                                string BusinessName1 = BLayer.B2B.GetBusinessName(corpUserId);
                                data.Businessname = BusinessName1;
                                List<CLayer.Address> Businessaddress = BLayer.Address.GetOnUser(corpUserId);
                                data.Busineessaddressarray = Businessaddress[0];

                            }
                        }
                    }
                    if (data.Businessname == null) { data.Businessname = ""; }
                    string tempName = data.Businessname;
                    string tFirstName = tempName;
                    string tLastName = "";
                    if (data.UserTypeId != (int)CLayer.Role.Roles.Customer && data.UserTypeId != (int)CLayer.Role.Roles.CorporateUser && data.UserTypeId != (int)CLayer.Role.Roles.Administrator && data.UserTypeId != (int)CLayer.Role.Roles.Staff)
                    {

                        int firstspace = tempName.IndexOf(' ');
                        if (firstspace > 1)
                            if (firstspace < data.Businessname.Length - 1)
                            {
                                tFirstName = tempName.Split(' ')[0].ToString();
                                tLastName = tempName.Substring(firstspace + 1, tempName.Length - tFirstName.Length - 1);
                            }

                        // data.SalutationId =0;
                        if (data.FirstName == null)
                        {
                            data.FirstName = tFirstName;
                        }
                    }
                    CLayer.User usr = new CLayer.User()
                    {
                        UserId = data.UserId,
                        SalutationId = data.SalutationId,
                        Businessname = data.Businessname,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        DateOfBirth = Convert.ToDateTime(data.DateOfBirth),
                        Sex = data.Sex,
                        Email = data.Email,
                        UserTypeId = data.UserTypeId,
                        ZipCode = data.ZipCode,
                        Status = data.Status,
                        MyAssistedBookerId=data.AssistedCorporateID
                    };
                    BLayer.User.Save(usr);
                    if (IsBusiness())
                    {

                        CLayer.B2B b2b = new CLayer.B2B()
                        {


                            B2BId = data.UserId,
                            Name = data.Businessname,//Business Name
                            ContactDesignation = data.ContactDesignation
                        };
                        BLayer.B2B.SaveBusinessname(b2b);

                    }

                    CLayer.Address adrs = new CLayer.Address()
                    {
                        AddressId = data.AddressId,
                        UserId = data.UserId,
                        AddressText = data.Address,
                        CityId = data.CityId,
                        State = data.State,
                        CountryId = data.CountryId,
                        Phone = data.Phone,
                        Mobile = data.Mobile,
                        ZipCode = data.ZipCode,
                        AddressType = (int)CLayer.Address.AddressTypes.Primary
                    };
                    if (data.UserTypeId == (int)CLayer.Role.Roles.Corporate)
                    {
                        adrs.AddressType = (int)CLayer.Address.AddressTypes.Normal;
                    }
                    if (data.City != null && data.City != "")
                    {
                        adrs.City = data.City;
                    }
                    if (data.CityId > 0)
                    {
                        adrs.City = BLayer.City.Get(data.CityId).Name;
                    }
                    BLayer.Address.Save(adrs);//main address
                    if (data.UserTypeId == (int)CLayer.Role.Roles.Corporate)
                    {
                        CLayer.Address BillingAddress;
                        if (!data.IsClicked)
                        {
                            BillingAddress = new CLayer.Address()
                            {
                                AddressId = data.BillingAddressId,
                                AddressText = data.BillingAddress,
                                ZipCode = data.BillingZipCode,
                                UserId = data.UserId,
                                CityId = data.BillingCityId,
                                State = data.BillingState,
                                CountryId = data.BillingCountryId,
                                Phone = data.Phone,
                                Mobile = data.Mobile,
                                AddressType = (int)CLayer.Address.AddressTypes.Primary
                            };
                            if (data.BillingCity != null && data.BillingCity != "")
                            {
                                BillingAddress.City = data.BillingCity;
                            }
                            if (data.BillingCityId > 0)
                            {
                                BillingAddress.City = BLayer.City.Get(data.BillingCityId).Name;
                            }
                        }
                        else
                        {
                            BillingAddress = new CLayer.Address()
                            {
                                AddressId = data.BillingAddressId,
                                AddressText = data.Address,
                                UserId = data.UserId,
                                ZipCode = data.ZipCode,
                                CityId = data.CityId,
                                City = data.City,
                                State = data.State,
                                CountryId = data.CountryId,
                                Phone = data.Phone,
                                Mobile = data.Mobile,
                                AddressType = (int)CLayer.Address.AddressTypes.Primary
                            };
                            if (data.City != null && data.City != "")
                            {
                                BillingAddress.City = data.City;
                            }
                            if (data.CityId > 0)
                            {
                                BillingAddress.City = BLayer.City.Get(data.CityId).Name;
                            }
                        }

                        data.BillingAddress = BillingAddress.AddressText;
                        data.UserId = BillingAddress.UserId;
                        data.BillingCityId = BillingAddress.CityId;
                        data.BillingState = BillingAddress.State;
                        data.BillingCountryId = BillingAddress.CountryId;
                        data.BillingZipCode = BillingAddress.ZipCode;
                        //data.Phone = BillingAddress.Phone;
                        //data.Mobile = BillingAddress.Mobile;
                        data.BillingAddressType = (int)CLayer.Address.AddressTypes.Primary;
                        data.BillingAddressId = BLayer.Address.Save(BillingAddress);
                        data.LoadPlaces();

                    }
                    if (data.UserTypeId == (int)CLayer.Role.Roles.Supplier)
                    {
                        CLayer.BankAccount acnt = new CLayer.BankAccount()
                        {
                            BankAccountId = data.BankAccountId,
                            UserId = data.UserId,
                            AccountName = data.FirstName + " " + data.LastName,
                            AccountNumber = data.AccountNumber,
                            BankName = data.BankName,
                            BranchAddress = data.BranchAddress,
                            RTGSNumber = data.RTGSNumber,
                            MICRCode = data.MICRCode
                        };
                        BLayer.BankAccount.Save(acnt);
                        //if (data.State > 0)
                        //{
                        //    List<CLayer.City> cities = null;
                        //    cities = BLayer.City.GetOnState(data.State);
                        //    cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                        //    data.CityList = new SelectList(cities, "CityId", "Name");
                        //}


                        //if (userid > 0)
                        //{
                        //    long corpUserId = BLayer.B2B.GetCorporateIdOfUser(userid);
                        //    if (corpUserId > 0)
                        //    {
                        //        string BusinessName1 = BLayer.B2B.GetBusinessName(corpUserId);
                        //        data.Businessname = BusinessName1;
                        //        List<CLayer.Address> Businessaddress = BLayer.Address.GetOnUser(corpUserId);
                        //        data.Busineessaddressarray = Businessaddress[0];
                        //    }
                        //}

                        data.LoadPlaces();
                    }
                    if (data.UserTypeId == (int)CLayer.Role.Roles.Affiliate)
                    {
                        CLayer.BankAccount acnt = new CLayer.BankAccount()
                        {
                            BankAccountId = data.BankAccountId,
                            UserId = data.UserId,
                            AccountName = data.FirstName + " " + data.LastName,
                            AccountNumber = data.AccountNumber,
                            BankName = data.BankName,
                            BranchAddress = data.BranchAddress,
                            RTGSNumber = data.RTGSNumber,
                            MICRCode = data.MICRCode
                        };
                        BLayer.BankAccount.Save(acnt);
                        //if (data.State > 0)
                        //{
                        //    List<CLayer.City> cities = null;
                        //    cities = BLayer.City.GetOnState(data.State);
                        //    cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                        //    data.CityList = new SelectList(cities, "CityId", "Name");
                        //}


                        data.LoadPlaces();
                    }
                    ViewBag.Message = "Your details updated successfully";
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    string err = "";
                    foreach (ModelError me in errors)
                    {
                        err += me.ErrorMessage.ToString();
                    }
                    ViewBag.Message = err;
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                ViewBag.Message = "Cannot save changes. An error occured.";
            }
            return View("Profile", data);
        }
        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword.Trim(), model.NewPassword.Trim());
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword.Trim());
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl, string forbooking)
        {
            // Request a redirect to the external login provider

            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl, forbooking = forbooking }));
        }
        public ClaimsIdentity GetBasicUserIdentity(string name)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };
            return new ClaimsIdentity(
              claims, DefaultAuthenticationTypes.ApplicationCookie);
        }
        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl, string forbooking)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            ViewBag.Externallogin = "Yes";
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            //var user = await UserManager.FindAsync(loginInfo.DefaultUserName,"");

            long dt1 = 0;
            if (loginInfo.Email != null)
            {
                dt1 = BLayer.User.GetUserId(loginInfo.Email);
            }
            else
            {
                string Key = loginInfo.Login.ProviderKey;
                dt1 = BLayer.User.GetUserByKey(Key);
            }

            string useremail = BLayer.User.GetEmail(dt1);
            //string dt = Convert.ToString(dt1);
            ViewBag.exloginemail = loginInfo.Email;

            if (dt1 > 0)
            {
                IdentityUser user1 = new IdentityUser();
                user1.Id = Convert.ToString(dt1);

                if (loginInfo.DefaultUserName != null)
                {
                    user1.UserName = loginInfo.DefaultUserName;
                }
                else
                {
                    if (loginInfo.Email != null)
                    {
                        user1.UserName = loginInfo.Email;
                    }
                    else
                    {
                        if (useremail != "")
                        {
                            user1.UserName = useremail;
                        }
                        else
                        {
                            user1.UserName = "";
                        }
                    }
                }
                //user1.UserName = useremail;             
                await SignInAsync(user1, false);
                if (forbooking == "yes")
                {
                    return RedirectToAction("Book", "Booking");
                }
                else
                {
                    if (returnUrl != null && returnUrl.ToLower().Contains("logoff")) returnUrl = null;
                    if (returnUrl == null || returnUrl == "")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToLocal(returnUrl);
                }
            }
            else
            {
                //If the user does not have an account, then prompt the user to create an account
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.ReturnUrl1 = forbooking;
                ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { UserName = loginInfo.DefaultUserName, Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }
        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.Identity.GetUserId(), loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl, string forbooking)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                ViewBag.LoginProvider = info.Login.LoginProvider;
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }

                CLayer.User pt = new CLayer.User()
                {
                    FirstName = model.UserName,
                    UserId = 0,
                    Email = model.Email,
                    Mobile = model.Moblie,
                    UserTypeId = (int)CLayer.Role.Roles.Customer,
                    Status = (int)CLayer.ObjectStatus.StatusType.Active
                };
                var users = BLayer.User.SaveUser(pt);

                //save user address
                CLayer.Address adrs = new CLayer.Address()
                {
                    UserId = users,
                    AddressText = model.Address,
                    CityId = model.CityId,
                    State = model.State,
                    CountryId = model.CountryId,
                    Mobile = model.Moblie,
                    ZipCode = model.ZipCode,

                    AddressType = (int)CLayer.Address.AddressTypes.Primary
                };
                if (model.City != null && model.City != "")
                    adrs.City = model.City;
                if (model.CityId > 0)
                    adrs.City = BLayer.City.Get(model.CityId).Name;
                BLayer.Address.Save(adrs);

                // save user login details
                string ProviderKey = info.Login.ProviderKey;
                string LoginProvider = info.Login.LoginProvider;
                long UserId = users;
                var data = BLayer.User.SaveLogins(UserId, ProviderKey, LoginProvider);

                IdentityUser user = new IdentityUser();
                user.Id = Convert.ToString(users);
                user.UserName = model.UserName;


                ////var result = await UserManager.CreateAsync(dt);             
                ////if (result.Succeeded)
                ////{
                ////    result = await UserManager.AddLoginAsync(dt.Id, info.Login);
                ////    if (result.Succeeded)
                ////    {
                await SignInAsync(user, false);
                if (forbooking == "yes")
                {
                    return RedirectToAction("Book", "Booking");
                }
                else
                {
                    if (returnUrl != null && returnUrl.ToLower().Contains("logoff")) returnUrl = null;
                    if (returnUrl == null || returnUrl == "")
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToLocal(returnUrl);
                }

            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        //
        // POST: /Account/LogOff
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session["LoggedInUser"] = "";
            AuthenticationManager.SignOut();
            //return RedirectToAction("LogoffRedirect");
            return RedirectToAction("Login", "Account");
        }
        [AllowAnonymous]
        public ActionResult LogoffRedirect()
        { //dummy action
            try
            {
                //  Response.Cookies.Clear();
                Session["LoggedInUser"] = "";
            }
            catch { }
            return RedirectToAction("Login", "Account");
        }
        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(StayBazar.Lib.Security.IdentityUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user.PasswordHash == "")
            {
                return false;
            }
            if (user != null)
            {
                return user.PasswordHash != null;
            }

            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [AllowAnonymous]
        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                context.RequestContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}