using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StayBazar.Models;
using StayBazar.Lib.Security;
using System.Configuration;
using System.Drawing;

namespace StayBazar.Areas.Admin.Controllers
{
    public class JoinRequestsController : Controller
    {
        [Common.AdminRolePermission(AllowAllRoles=true)]
        #region CommonFunctions
        private void Save(Models.UserDocumentsModel data)
        {
            #region Bank Account details for Supplier
            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                string b2bname = BLayer.B2BUser.Getb2bname(data.UserId);
                if (data.BankName != "")
                {
                    CLayer.BankAccount account = new CLayer.BankAccount()
                    {
                        BankAccountId = data.BankAccountId,
                        UserId = data.UserId,
                        AccountName = b2bname,// data.Name,//Business name b2b                       
                        AccountNumber = data.AccountNumber,
                        BankName = data.BankName,
                        BranchAddress = data.BranchAddress,
                        RTGSNumber = data.RTGSNumber,
                        MICRCode = data.MICRCode
                    };
                    BLayer.BankAccount.Save(account);
                }
            }
            if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
            {
                string b2bname = BLayer.B2BUser.Getb2bname(data.UserId);
                if (data.BankName != "")
                {
                    CLayer.BankAccount account = new CLayer.BankAccount()
                    {
                        BankAccountId = data.BankAccountId,
                        UserId = data.UserId,
                        AccountName = b2bname,// data.Name,//Business name b2b                       
                        AccountNumber = data.AccountNumber,
                        BankName = data.BankName,
                        BranchAddress = data.BranchAddress,
                        RTGSNumber = data.RTGSNumber,
                        MICRCode = data.MICRCode
                    };
                    BLayer.BankAccount.Save(account);
                }
            }
            #endregion
            #region File Attatchments
            if (data.ServiceTaxReg != null && data.ServiceTaxReg.ContentLength > 0)
            {
                CLayer.UserFiles servicetax = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "ServiceTax" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.ServiceTaxReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.ServiceTaxRegNo
                };
                BLayer.UserFiles.Save(servicetax);
                SaveDocument(data.ServiceTaxReg, data.UserId, servicetax.FileName);
            }
            if (data.VATReg != null && data.VATReg.ContentLength > 0)
            {
                CLayer.UserFiles vat = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "VAT" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.VATReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.VATRegNo
                };
                BLayer.UserFiles.Save(vat);
                SaveDocument(data.VATReg, data.UserId, vat.FileName);
            }
            if (data.BusinessRegistrationCertificate != null && data.BusinessRegistrationCertificate.ContentLength > 0)
            {
                CLayer.UserFiles brc = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "BRC" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.BusinessRegistrationCertificate.FileName),
                    Document = (int)CLayer.UserFiles.Documents.BusinessRegistrationCertificate
                };
                BLayer.UserFiles.Save(brc);
                SaveDocument(data.BusinessRegistrationCertificate, data.UserId, brc.FileName);
            }
            if (data.PANCard != null && data.PANCard.ContentLength > 0)
            {
                CLayer.UserFiles pan = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "PAN" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.PANCard.FileName),
                    Document = (int)CLayer.UserFiles.Documents.PANCard
                };
                BLayer.UserFiles.Save(pan);
                SaveDocument(data.PANCard, data.UserId, pan.FileName);
            }
            if (data.CorporateLogo != null && data.CorporateLogo.ContentLength > 0)
            {
                CLayer.UserFiles corporatelogo = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "CORPORATELOGO" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CorporateLogo.FileName),
                    Document = (int)CLayer.UserFiles.Documents.CorporateLogo
                };
                BLayer.UserFiles.Save(corporatelogo);
                SaveDocument(data.CorporateLogo, data.UserId, corporatelogo.FileName);
            }
            if (data.CopyOfCheque != null && data.CopyOfCheque.ContentLength > 0)
            {
                CLayer.UserFiles cc = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "Cheque" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CopyOfCheque.FileName),
                    Document = (int)CLayer.UserFiles.Documents.CopyOfCheque
                };

                BLayer.UserFiles.Save(cc);
                SaveDocument(data.CopyOfCheque, data.UserId, cc.FileName);
            }


            #endregion
        }
        private void SaveDocument(HttpPostedFileBase file, long UserId, string fname)
        {
            var fileName = fname;
            var path = System.IO.Path.Combine(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/"), fileName);    
            if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/"));
            }
            file.SaveAs(path);
            ModelState.Clear();
        }

        #endregion    
        //
        // GET: /Admin/JoinRequests/
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Index()
        {
            //return View();
            return RedirectToAction("Corporate");
        }
        [Common.AdminRolePermission(ModuleId=Common.AdminRolePermission.SUPPLIER_OWNER_REGISTRATION)]
        public ActionResult Supplier(bool? rejected)
        {
            try
            {
                if (rejected.HasValue)
                {
                    if ((bool)rejected.Value)
                        return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Supplier, (int)CLayer.ObjectStatus.StatusType.Rejected));
                    else
                        return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Supplier));
                }
                else
                {
                    return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Supplier));
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(ModuleId = Common.AdminRolePermission.TRAVEL_AGENT_REGISTRATION)]
        public ActionResult TravelAgent(bool? rejected)
        {
            try
            {
                if (rejected.HasValue)
                {
                    if ((bool)rejected.Value)
                        return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Agent, (int)CLayer.ObjectStatus.StatusType.Rejected));
                    else
                        return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Agent));
                }
                else
                {
                    return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Agent));
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission(ModuleId = Common.AdminRolePermission.CORPORATE_REGISTRATION)]
        public ActionResult Corporate(bool? rejected)
        {
            try
            {
                if (rejected.HasValue)
                {
                    if ((bool)rejected.Value)
                        return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Corporate, (int)CLayer.ObjectStatus.StatusType.Rejected));
                    else
                        return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Corporate));
                }
                else
                {
                    return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Corporate));
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(ModuleId = Common.AdminRolePermission.AFFILIATE_REGISTRATION)]
        public ActionResult Affiliates(bool? rejected)
        {
            try
            {
                if (rejected.HasValue)
                {
                    if ((bool)rejected.Value)
                        return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Affiliate, (int)CLayer.ObjectStatus.StatusType.Rejected));
                    else
                        return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Affiliate));
                }
                else
                {
                    return View(BLayer.B2B.Getall((int)CLayer.Role.Roles.Affiliate));
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Details(long Id)
        {
            try
            {
                CLayer.B2B data = BLayer.B2B.Get(Id);
                Models.B2BModel b2b = new Models.B2BModel()
                {
                    B2BId = data.B2BId,
                    UserId = data.B2BId,
                    Name = data.Name,
                    Email = data.Email,
                    UserType = data.UserType,
                    CompanyRegNo=data.CompanyRegNo,
                    ServiceTaxRegNo = data.ServiceTaxRegNo,
                    VATRegNo = data.VATRegNo,
                    RequestStatus = data.RequestStatus,
                    PropertyDescription = data.PropertyDescription,
                    AvailableProperties = data.AvailableProperties,
                    Addresses = BLayer.Address.GetOnUser(data.B2BId)
                   
                };
                b2b.BankDetails = new CLayer.BankAccount();
                b2b.BankDetails = BLayer.BankAccount.GetOnUser(data.B2BId);
                CLayer.User usr = BLayer.User.Get(Id);
                if (usr != null)
                    b2b.ContactName = usr.FirstName ;
                if (data.RequestStatus == (int)CLayer.ObjectStatus.StatusType.NotVerified ||
                    data.RequestStatus == (int)CLayer.ObjectStatus.StatusType.Unread)
                    BLayer.B2B.SetStatus(Id, (int)CLayer.ObjectStatus.StatusType.Read);
                return View(b2b);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Markasunread(long Id)
        {
            try
            {
                CLayer.B2B data = BLayer.B2B.Get(Id);
                BLayer.B2B.SetStatus(Id, (int)CLayer.ObjectStatus.StatusType.Unread);
                if (data.UserType == (int)CLayer.Role.Roles.Supplier)
                    return RedirectToAction("Supplier");
                else if (data.UserType == (int)CLayer.Role.Roles.Agent)
                    return RedirectToAction("TravelAgent");
                else if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
                    return RedirectToAction("Affiliates");
                return RedirectToAction("Corporate");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public async Task<ActionResult> Accept(long Id)
        {
            try
            {
                CLayer.B2B data = BLayer.B2B.Get(Id);
                BLayer.B2B.SetStatus(Id, (int)CLayer.ObjectStatus.StatusType.Accepted);
                string Password = Guid.NewGuid().ToString().Substring(0, 6);
                UserManager<StayBazar.Lib.Security.IdentityUser> usrmngr = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                String hashedNewPassword = usrmngr.PasswordHasher.HashPassword(Password);
                BLayer.User.SetPassword(data.UserId, hashedNewPassword);
                BLayer.User.SetStatus(data.UserId.ToString(), (int)Models.HostModel.StatusTypes.Active);
                BLayer.B2B.SetApprovalDate(data.UserId, DateTime.Today);
                //string querymail = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.To.Add(data.Email);
               // msg.From = new System.Net.Mail.MailAddress(querymail);
                msg.Body = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("B2BRegistration") + Id.ToString() + "&pass=" + Password + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    //"Your staybazar account request has been accepted. User Name: " + data.Email + " , Password:" + Password;
                msg.IsBodyHtml = true;
                msg.Subject = "Staybazar Account";
                Common.Mailer ml = new Common.Mailer();

                if (data.UserType == (int)CLayer.Role.Roles.Supplier)
                {
                    // add bcc only for Suppliercommunications
                    string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                    if (BccEmailsforsupp != "")
                    {
                        string[] emails = BccEmailsforsupp.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }
                }
                else               
                {
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
                
                }

                await ml.SendMailAsyncForBooking(msg,Common.Mailer.MailType.Query);

                if (data.UserType == (int)CLayer.Role.Roles.Supplier)
                    return RedirectToAction("Supplier");
                else if (data.UserType == (int)CLayer.Role.Roles.Agent)
                    return RedirectToAction("TravelAgent");
                else if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
                    return RedirectToAction("Affiliates");
                return RedirectToAction("Corporate");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Reject(long Id)
        {
            try
            {
                CLayer.B2B data = BLayer.B2B.Get(Id);
                BLayer.B2B.SetStatus(Id, (int)CLayer.ObjectStatus.StatusType.Rejected);
                if (data.UserType == (int)CLayer.Role.Roles.Supplier)
                    return RedirectToAction("Supplier");
                else if (data.UserType == (int)CLayer.Role.Roles.Agent)
                    return RedirectToAction("TravelAgent");
                else if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
                    return RedirectToAction("Affiliates");
                return RedirectToAction("Corporate");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.RoleRequired(Administrator = true)]
        public ActionResult Delete(long Id)
        {
            CLayer.B2B data = BLayer.B2B.Get(Id);
            BLayer.B2B.Delete(Id);
            if (System.IO.Directory.Exists(Server.MapPath("~/Files/Users/" + Id.ToString())))
            {
                System.IO.Directory.Delete(Server.MapPath("~/Files/Users/" + Id.ToString()), true);
            }
            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
                return RedirectToAction("Supplier", new { rejected = true });
            else if (data.UserType == (int)CLayer.Role.Roles.Agent)
                return RedirectToAction("TravelAgent", new { rejected = true });
            else if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
                return RedirectToAction("Affiliates", new { rejected = true });
            return RedirectToAction("Corporate", new { rejected = true });
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult CorporateDetails(long id)
        {
            Models.UserDetailModel details = null;
            CLayer.User data = BLayer.User.Get(id);
            if (data != null)
            {

                // details.FullName = data.FirstName + data.LastName;
                details = new Models.UserDetailModel()
                {
                    UserId = data.UserId,
                    SalutationId = data.SalutationId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    DateOfBirth = data.DateOfBirth.ToShortDateString(),
                    Status = data.Status,
                    Email = data.Email,
                   
                    CreatedDate = data.CreatedDate,
                    DeletedDate = data.DeletedDate,
                    LastLoginOn = data.LastLoginOn
                };
            }
            CLayer.B2B b2b = BLayer.B2B.Get(id);
            if (b2b != null)
            {
                details.MaxStaff = b2b.MaximumStaff;
                details.Name = b2b.Name;
                details.RequestStatus = b2b.RequestStatus;
              
            }
            //if (id > 0)
            //{
            //    ViewData["B2BId"] = id;
            //}
            if (b2b.RequestStatus == (int)CLayer.ObjectStatus.StatusType.NotVerified ||
                 b2b.RequestStatus == (int)CLayer.ObjectStatus.StatusType.Unread)
                BLayer.B2B.SetStatus(id, (int)CLayer.ObjectStatus.StatusType.Read);
            CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(id);
            if (adrs != null)
            {
                details.Address = adrs.AddressText;
                details.AddressId = adrs.AddressId;
                details.State = adrs.State;

                details.CountryId = adrs.CountryId;
                details.Phone = adrs.Phone;
                details.Mobile = adrs.Mobile;
                details.ZipCode = adrs.ZipCode;
                if (adrs.CityId > 0)
                {
                    details.City = BLayer.City.Get(adrs.CityId).Name;
                    details.CityId = adrs.CityId;
                }
                else
                {
                    if (adrs.City != null && adrs.City != "")
                        details.City = adrs.City;
                }

            }


            CLayer.Address BillingAddress = BLayer.Address.GetBillingAddress(id);
            if (BillingAddress != null)
            {
                details.BillingAddressType = BillingAddress.BillingAddressType;
                details.BillingAddressId = BillingAddress.BillingAddressId;
                details.BillingAddress = BillingAddress.BillingAddress;
                details.BillingCityId = BillingAddress.BillingCityId;
                details.BillingState = BillingAddress.BillingState;
                details.BillingCity = BillingAddress.BillingCity;
                details.BillingCountryId = BillingAddress.BillingCountryId;
                details.BillingZipCode = BillingAddress.BillingZipCode;
                details.IsClicked = false;
            }
            else
            {
                details.BillingAddress = details.Address;
                details.BillingAddressId = 0;
                details.BillingAddress = details.Address;
                details.BillingCityId = details.CityId;
                details.BillingCity = details.City;
                details.BillingState = details.State;
                details.BillingCountryId = details.CountryId;
                details.Phone = details.Phone;
                details.Mobile = details.Mobile;
                details.BillingAddressType = (int)CLayer.Address.AddressTypes.Primary;
                details.BillingZipCode = details.ZipCode;
                details.IsClicked = true;
            }

            if (details.BillingZipCode == details.ZipCode && details.BillingAddress == details.Address && details.BillingCityId == details.CityId && details.BillingCity == details.City && details.BillingState == details.State && details.BillingCountryId == details.CountryId)
                details.IsClicked = true;



            details.LoadPlaces();

            return View(details);
        }

        [Common.AdminRolePermission(ModuleId = Common.AdminRolePermission.CORPORATE_REGISTRATION)]
        public ActionResult CorporateUpdate(Models.UserDetailModel data)
        {
            if (ModelState.IsValid)
            {
                CLayer.User usr = new CLayer.User()
                {
                    UserId = data.UserId,
                    SalutationId = data.SalutationId,
                    FirstName = data.FirstName,
                    LastName = data.FirstName,
                    Businessname = data.Name,//Businessname
                    Email = data.Email
                };
                BLayer.User.Update(usr);

                CLayer.B2B b2b = new CLayer.B2B()
                {
                    B2BId = data.UserId,
                    Name = data.Name,//Businessname
                    ContactDesignation=data.ContactDesignation
                };
                BLayer.B2B.Update(b2b);
                //if (data.UserId > 0)
                //{
                //    ViewData["B2BId"] = data.UserId;
                //}
                CLayer.Address adrs = new CLayer.Address()
                {
                    AddressId = data.AddressId,
                    UserId = data.UserId,
                    AddressText = data.Address,
                    // CityId = data.CityId,
                    State = data.State,
                    CountryId = data.CountryId,
                    Phone = data.Phone,
                    ZipCode = data.ZipCode,
                    Mobile = data.Mobile,
                    AddressType = (int)CLayer.Address.AddressTypes.Normal
                };


                if (data.CityId > 0)
                {
                    adrs.City = BLayer.City.Get(data.CityId).Name;
                    adrs.CityId = data.CityId;
                }
                else
                {
                    if (data.City != null && data.City != "")
                        adrs.City = data.City;
                }
                BLayer.Address.Save(adrs);

                //billing address Save
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
                    BLayer.Address.Save(BillingAddress);
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
                    BLayer.Address.Save(BillingAddress);
                }
                ViewBag.Message = "Your details updated successfully";
                return RedirectToAction("CorporateDetails", new { id = data.UserId });
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
            ViewBag.Message = "Your details updated successfully";
            return View("CorporateDetails", data);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult SupplierDetails(long? id)
        {
            Models.UserDetailModel details = null;
            CLayer.User data = BLayer.User.Get(id.Value);
            if (data != null)
            {
                details = new Models.UserDetailModel()
                {
                    UserId = data.UserId,
                    SalutationId = data.SalutationId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    DateOfBirth = data.DateOfBirth.ToShortDateString(),
                    Status = data.Status,
                    Email = data.Email,
                    CreatedDate = data.CreatedDate,
                    DeletedDate = data.DeletedDate,
                    LastLoginOn = data.LastLoginOn,
                    Phone = data.Phone,
                    Mobile = data.Mobile,
                    PANNo = data.PANNo
                };
            }
            //if (id > 0)
            //{
            //    ViewData["B2BId"] = id;
            //}
            CLayer.B2B b2b = BLayer.B2B.Get(id.Value);
            if (b2b != null)
            {
                details.MaxStaff = b2b.MaximumStaff;
                details.Name = b2b.Name;
                details.PANNo = b2b.PANNo;
                details.RequestStatus = b2b.RequestStatus;
                details.PropertyDescription = b2b.PropertyDescription;
                details.AvailableProperties = b2b.AvailableProperties;
            }

            CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(id.Value);
            if (adrs != null)
            {
                details.Address = adrs.AddressText;
                details.AddressId = adrs.AddressId;
                details.State = adrs.State;

                details.CountryId = adrs.CountryId;
                details.Phone = adrs.Phone;
                details.Mobile = adrs.Mobile;
                details.ZipCode = adrs.ZipCode;
                details.CityId = adrs.CityId;
                details.City = adrs.City;
                if (adrs.CityId > 0)
                {
                    details.City = BLayer.City.Get(adrs.CityId).Name;
                    details.CityId = adrs.CityId;
                }
                else
                {
                    if (adrs.City != null && adrs.City != "")
                        details.City = adrs.City;
                }
                details.LoadPlaces();
            }
            return View(details);
        }

        [Common.AdminRolePermission(ModuleId = Common.AdminRolePermission.SUPPLIER_OWNER_REGISTRATION)]
        public ActionResult SupplierUpdate(Models.UserDetailModel data)
        {
          
                CLayer.User usr = new CLayer.User()
                {
                    UserId = data.UserId,
                    Businessname = data.Name,//Businessname
                    SalutationId = data.SalutationId,
                    FirstName = data.FirstName,
                    LastName = data.FirstName,
                    Email = data.Email
                };
                BLayer.User.Update(usr);
              
                CLayer.B2B b2b = new CLayer.B2B()     
                {
                    B2BId = data.UserId,
                    Name = data.Name,//Businessname
                   PropertyDescription=data.PropertyDescription,
                   AvailableProperties=data.AvailableProperties               
                };
               

                BLayer.B2B.Update(b2b);
                //if (data.UserId > 0)
                //{
                //    ViewData["B2BId"] = data.UserId;
                //}
                CLayer.Address adrs = new CLayer.Address()
                {
                    AddressId = data.AddressId,
                    UserId = data.UserId,
                    AddressText = data.Address,
                    //CityId = data.CityId,
                    State = data.State,
                    CountryId = data.CountryId,
                    Phone = data.Phone,
                    ZipCode = data.ZipCode,
                    Mobile = data.Mobile,
                    AddressType = (int)CLayer.Address.AddressTypes.Primary
                };
                if (data.CityId > 0)
                {
                    adrs.City = BLayer.City.Get(data.CityId).Name;
                    adrs.CityId = data.CityId;
                }
                else
                {
                    if (data.City != null && data.City != "")
                        adrs.City = data.City;
                }


                BLayer.Address.Save(adrs);
                ViewBag.Message = "Your details updated successfully";
                return RedirectToAction("SupplierDetails", new { id = data.UserId });
          
            
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult TravelAgentDetails(long id)
        {
            Models.UserDetailModel details = null;
            CLayer.User data = BLayer.User.Get(id);
            if (data != null)
            {
                details = new Models.UserDetailModel()
                {
                    UserId = data.UserId,
                    SalutationId = data.SalutationId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    DateOfBirth = data.DateOfBirth.ToShortDateString(),
                    Status = data.Status,
                    Email = data.Email,
                    CreatedDate = data.CreatedDate,
                    DeletedDate = data.DeletedDate,
                    LastLoginOn = data.LastLoginOn
                };
            }
            //if (id > 0)
            //{
            //    ViewData["B2BId"] = id;
            //}
            CLayer.B2B b2b = BLayer.B2B.Get(id);
            if (b2b != null)
            {
                details.MaxStaff = b2b.MaximumStaff;
                details.Name = b2b.Name;
                details.RequestStatus = b2b.RequestStatus;
            }
            CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(id);
            if (adrs != null)
            {
                details.Address = adrs.AddressText;
                details.AddressId = adrs.AddressId;
                details.State = adrs.State;

                details.CountryId = adrs.CountryId;
                details.Phone = adrs.Phone;
                details.Mobile = adrs.Mobile;
                details.ZipCode = adrs.ZipCode;
                if (adrs.CityId > 0)
                {
                    details.City = BLayer.City.Get(adrs.CityId).Name;
                    details.CityId = adrs.CityId;
                }
                else
                {
                    if (adrs.City != null && adrs.City != "")
                        details.City = adrs.City;
                }
                details.LoadPlaces();
            }
            return View(details);
        }
        [Common.AdminRolePermission(ModuleId = Common.AdminRolePermission.TRAVEL_AGENT_REGISTRATION)]
        public ActionResult TravelAgentUpdate(Models.UserDetailModel data)
        {
            if (ModelState.IsValid)
            {

                CLayer.User usr = new CLayer.User()
                {
                    UserId = data.UserId,
                    SalutationId = data.SalutationId,
                    Businessname = data.Name,//Businessname
                    FirstName = data.FirstName,
                    LastName = data.FirstName,
                    Email = data.Email
                };
                BLayer.User.Update(usr);

                CLayer.B2B b2b = new CLayer.B2B()
                {
                    B2BId = data.UserId,
                    Name = data.Name, //Businessname
                   
                };
                BLayer.B2B.Update(b2b);
                //if (data.UserId > 0)
                //{
                //    ViewData["B2BId"] = data.UserId;
                //}

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
                if (data.CityId > 0)
                {
                    adrs.City = BLayer.City.Get(data.CityId).Name;
                    adrs.CityId = data.CityId;
                }
                else
                {
                    if (data.City != null && data.City != "")
                        adrs.City = data.City;
                }
                BLayer.Address.Save(adrs);
                ViewBag.Message = "Your details updated successfully";
                return RedirectToAction("TravelAgentDetails", new { id = data.UserId });
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
            return View("TravelAgentDetails", data);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult AffiliateDetails(long? id)
        {

            Models.UserDetailModel details = null;
            CLayer.User data = BLayer.User.Get(id.Value);
            if (data != null)
            {
                details = new Models.UserDetailModel()
                
                {

                    UserId = data.UserId,
                    SalutationId = data.SalutationId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    DateOfBirth = data.DateOfBirth.ToShortDateString(),
                    Status = data.Status,
                    Email = data.Email,
                    CreatedDate = data.CreatedDate,
                    DeletedDate = data.DeletedDate,
                    LastLoginOn = data.LastLoginOn,
                    Phone = data.Phone,
                    Mobile = data.Mobile,
                    PANNo = data.PANNo


                };
            }
            //if (id > 0)
            //{
            //    ViewData["B2BId"] = id;
            //}
            CLayer.B2B b2b = BLayer.B2B.Get(id.Value);
            if (b2b != null)
            {
                details.MaxStaff = b2b.MaximumStaff;
                details.Name = b2b.Name;
                details.PANNo = b2b.PANNo;
                details.RequestStatus = b2b.RequestStatus;
                details.PropertyDescription = b2b.PropertyDescription;
                details.AvailableProperties = b2b.AvailableProperties;
            }

            CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(id.Value);
            if (adrs != null)
            {
                details.Address = adrs.AddressText;
                details.AddressId = adrs.AddressId;
                details.State = adrs.State;

                details.CountryId = adrs.CountryId;
                details.Phone = adrs.Phone;
                details.Mobile = adrs.Mobile;
                details.ZipCode = adrs.ZipCode;

                if (adrs.CityId > 0)
                {
                    details.City = BLayer.City.Get(adrs.CityId).Name;
                    details.CityId = adrs.CityId;
                }
                else
                {
                    if (adrs.City != null && adrs.City != "")
                        details.City = adrs.City;
                }
                details.LoadPlaces();
            }

            List<CLayer.B2B> SupplierList = new List<CLayer.B2B>();
            SupplierList = BLayer.B2B.GetAllSupplier(id.Value);
            details.SupplierList = SupplierList;

            return View(details);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult AffiliateUpdate(Models.UserDetailModel data)
        {
            if (ModelState.IsValid)
            {
                CLayer.User usr = new CLayer.User()
                {
                    UserId = data.UserId,
                    Businessname = data.Name,//Businessname
                    SalutationId = data.SalutationId,
                    FirstName = data.FirstName,
                    LastName = data.FirstName,
                    Email = data.Email
                };
                BLayer.User.Update(usr);

                CLayer.B2B b2b = new CLayer.B2B()
                {
                    B2BId = data.UserId,
                    Name = data.Name,//Businessname
                    //RequestStatus = data.RequestStatus,
                    PropertyDescription=data.PropertyDescription,
                    AvailableProperties=data.AvailableProperties
                    //PANNo = data.PANNo
                };
                BLayer.B2B.Update(b2b);
                //if (data.UserId > 0)
                //{
                //    ViewData["B2BId"] = data.UserId;
                //}
                CLayer.Address adrs = new CLayer.Address()
                {

                    AddressId = data.AddressId,
                    UserId = data.UserId,
                    AddressText = data.Address,
                    //CityId = data.CityId,
                    State = data.State,
                    CountryId = data.CountryId,
                    Phone = data.Phone,
                    ZipCode = data.ZipCode,
                    Mobile = data.Mobile,
                    AddressType = (int)CLayer.Address.AddressTypes.Primary
                };
                if (data.CityId > 0)
                {
                    adrs.City = BLayer.City.Get(data.CityId).Name;
                    adrs.CityId = data.CityId;
                }
                else
                {
                    if (data.City != null && data.City != "")
                        adrs.City = data.City;
                }


                BLayer.Address.Save(adrs);
                ViewBag.Message = "Your details updated successfully";
                return RedirectToAction("AffiliateDetails", new { id = data.UserId });
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
            return View("AffiliateDetails", data);
        }


        [HttpPost]
        public ActionResult UserDocumentdataSave(Models.UserDocumentsModel data)
        {

            string b2bname = BLayer.B2BUser.Getb2bname(data.UserId);
            CLayer.B2B b2b = new CLayer.B2B()
            {
                Name = b2bname,
                B2BId = data.UserId,
                UserType = data.UserType,
                ServiceTaxRegNo = data.ServiceTaxRegNo,
                PANNo = data.PANNo,
                VATRegNo = data.VATRegNo,

            };
            data.B2BId = BLayer.B2B.Savedoc(b2b);
            if (data.UserType == (int)CLayer.Role.Roles.Agent)
            {
                int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                maxContentLength = maxContentLength * maxFileSize;
                //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                string[] allowedFileExtensions = extensions.Split(',');
                if (data.ServiceTaxReg != null || data.BusinessRegistrationCertificate != null ||
                    data.PANCard != null || data.CopyOfCheque != null)
                {                  
                    data.UserType = (int)CLayer.Role.Roles.Agent;
                    Save(data);
                }

                return RedirectToAction("TravelAgentDetails", "JoinRequests", new { id = data.UserId });
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                maxContentLength = maxContentLength * maxFileSize;
                //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                string[] allowedFileExtensions = extensions.Split(',');

                if (data.ServiceTaxReg != null || data.VATReg != null)
                {
                    data.UserType = (int)CLayer.Role.Roles.Supplier;
                    Save(data);
                }
                else
                {
                    data.UserType = (int)CLayer.Role.Roles.Affiliate;
                    Save(data);
                    ViewBag.Message = "The files are not selected";
                }

                return RedirectToAction("SupplierDetails", "JoinRequests", new { id = data.UserId });
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
            {
                int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                maxContentLength = maxContentLength * maxFileSize;
                //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                string[] allowedFileExtensions = extensions.Split(',');

                if (data.ServiceTaxReg != null || data.VATReg != null)
                {
                    data.UserType = (int)CLayer.Role.Roles.Affiliate;
                    Save(data);
                }
                else
                {
                    data.UserType = (int)CLayer.Role.Roles.Affiliate;
                    Save(data);
                    ViewBag.Message = "The files are not selected";
                }

                return RedirectToAction("AffiliateDetails", "JoinRequests", new { id = data.UserId });
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Corporate)
            {
                int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                maxContentLength = maxContentLength * maxFileSize;
                //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                string[] allowedFileExtensions = extensions.Split(',');
                if (data.ServiceTaxReg != null || data.CorporateLogo!=null)
                {
                    data.UserType = (int)CLayer.Role.Roles.Corporate;
                    Save(data);
                }
                else
                {
                    data.UserType = (int)CLayer.Role.Roles.Corporate;
                    Save(data);
                    ViewBag.Message = "The files are not selected";
                }
                return RedirectToAction("CorporateDetails", "JoinRequests", new { id = data.UserId });
            }
            else
            {
                return View();
            }
        }



    }
}