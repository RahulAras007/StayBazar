using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Configuration;
using System.Data;
using StayBazar.Lib.Security;



namespace StayBazar.Areas.Admin.Controllers
{
    
    public class CorporateController : Controller
    {
        //corporate
        // GET: /Admin/Corpearte/
        private const int PAGE_LIMIT = 25;

        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.UserSearchModel search = new Models.UserSearchModel();
            List<CLayer.User> users = BLayer.User.GetAllB2BForSearch((int)CLayer.ObjectStatus.StatusType.Active, "", 0, (int)CLayer.Role.Roles.Corporate, 0, PAGE_LIMIT);
            search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
            search.SearchString = "";
            search.SearchValue = 1;
            search.userlist = users;
            search.TotalRows = 0;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = PAGE_LIMIT;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View(search);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public ActionResult Filter(Models.UserSearchModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            if (data.SearchString == "") data.SearchValue = 0;
            List<CLayer.User> users = BLayer.User.GetAllB2BForSearch(data.Status, data.SearchString, data.SearchValue, (int)CLayer.Role.Roles.Corporate, 0, PAGE_LIMIT);
            ViewBag.Filter = new Models.UserSearchModel();

            Models.UserSearchModel forPager = new Models.UserSearchModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                userlist = users,
                TotalRows = 0,
                Limit = PAGE_LIMIT,
                currentPage = 1
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }

        [Common.AdminRolePermission]
        public FileStreamResult ExcelView(Models.UserSearchModel data)
        {
            DataTable users = new DataTable();
            try
            {
                if (data.SearchString == null) data.SearchString = "";
                if (data.SearchString == "") data.SearchValue = 0;
                users = BLayer.User.GetAllB2BForExcel(data.Status, data.SearchString, data.SearchValue, (int)CLayer.Role.Roles.Corporate);

               
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new FileStreamResult(DataTableToExcel.GetExcelStream(users, "Report", true, null, null, null), DataTableToExcel.CONTENT_TYPE_XLSX)
            {
                FileDownloadName = "CorporateDetails.xlsx"
            };
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public ActionResult Pager1(Models.UserSearchModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            if (data.SearchString == "") data.SearchValue = 0;
            List<CLayer.User> users = BLayer.User.GetAllB2BForSearch(data.Status, data.SearchString, data.SearchValue, (int)CLayer.Role.Roles.Corporate, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.UserSearchModel();
            Models.UserSearchModel forPager = new Models.UserSearchModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                userlist = users,
                TotalRows = 0,
                Limit = PAGE_LIMIT,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }

      //  [Common.AdminRolePermission(AllowAllRoles=true)]
        [Common.AdminRolePermission]
        public ActionResult Details(long id)
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
                    LastLoginOn = data.LastLoginOn,
                    CustomerPaymentMode=data.CustomerPaymentMode,
                    CustomerPaymentModeCreditDays =data.CustomerPaymentModeCreditDays
                    
                };
            }
            CLayer.B2B b2b = BLayer.B2B.Get(id);
            if (b2b != null)
            {
                details.MaxStaff = b2b.MaximumStaff;
                details.Name = b2b.Name;
                details.CreditPeriod = b2b.CreditPeriod;
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

            return View("~/Areas/Admin/Views/Corporate/Details.cshtml", details);
        }

       // [Common.AdminRolePermission(AllowAllRoles=true)]
        [Common.AdminRolePermission]
        public ActionResult Update(Models.UserDetailModel data)
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
                long UserId = BLayer.User.Update(usr);
                if (UserId > 0)
                {
                    CLayer.B2B b2b = new CLayer.B2B()
                    {
                        B2BId = data.UserId,
                        Name = data.Name,//Businessname
                        ContactDesignation = data.ContactDesignation
                    };
                    BLayer.B2B.Update(b2b);

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
                    return RedirectToAction("Details", new { id = data.UserId });
                }
                else
                {
                    ViewBag.Message = "The email id already used by someone else";

                }
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
            return View("Details", data);
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public string SetMaxStaff(long b2bId, int maxstaff)
        {
            BLayer.B2B.SetMaxStaff(b2bId, maxstaff);
            return "true";
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public string SetCreditPeriod(long b2bId, int CreditPeriod)
        {
            BLayer.B2B.SetCreditPeriod(b2bId, CreditPeriod);
            return "true";
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        public string SetCustomerPaymentMode(long b2bId, int CustomerPaymentMode,decimal CustomerPaymentModeCreditDays = 0)
        {
            BLayer.User.SetCustomerPaymentMode(b2bId, CustomerPaymentMode, CustomerPaymentModeCreditDays);
            //return "true";
            ViewBag.Message = "Customer Payment Mode updated successfully";
            return "true";
        }

        [Common.RoleRequired(Administrator = true)]
        public ActionResult Delete(long Id)
        {
            BLayer.User.SetDeleteStatus(Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult TransactionList(int id)
        {
            Models.TransactionListModel search = new Models.TransactionListModel();
            List<CLayer.Booking> users = BLayer.Transaction.GetUserId(id, 0, 25);
            search.TotalRows = 0;
            search.Bookinglist = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View("~/Areas/Admin/Views/Common/_TransactionList.cshtml", search);
        }
        [HttpPost]
        public ActionResult Pager(Models.TransactionsModel data)
        {
            List<CLayer.Booking> users = BLayer.Transaction.GetUserId(data.UserId, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.TransactionListModel();
            Models.TransactionListModel forPager = new Models.TransactionListModel()
            {
                TotalRows = 0,
                Limit = PAGE_LIMIT,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("~/Areas/Admin/Views/Common/_TransactionList.cshtml", users);
        }
        [HttpPost]
        public ActionResult Search(string corporateName)
        {
            List<CLayer.B2B> result = new List<CLayer.B2B>();
            try
            {
                result = BLayer.B2B.SearchCorporate(corporateName);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/Property/_CorporateList.cshtml", result);
        }

        [Common.RoleRequired(Administrator = true)]
        [HttpPost]
        public ActionResult SaveCBookCredit(Models.UserDetailModel data)
        {
            try
            {
                CLayer.B2B payment = new CLayer.B2B()
                {
                    IsCreditBooking = data.IsCreditBooking,
                    CreditDays = data.CreditDays,
                    CreditAmount = data.CreditAmount,
                    TotalCreditAmount=data.CreditAmount,
                    UserId=data.UserId
                };
                long id = BLayer.B2B.SaveCBookCredit(payment);
                return null;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage"); //                
            }
        }


        
        [Common.RoleRequired(Administrator = true)]
        [HttpPost]
        public ActionResult AllowCBookSamedaybook(Models.UserDetailModel data)
        {
            try
            {
                CLayer.B2B book = new CLayer.B2B()
                {
                    IsCorpBookingtoday=data.IsCorpBookingtoday,
                    UserId=data.UserId
                };
                long id = BLayer.B2B.AllowCBookSamedaybook(book.IsCorpBookingtoday,book.UserId);
                return null;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage"); //                
            }
        }


        [AllowAnonymous]
        public async Task<bool> ResendregemailCorp(long UserId, string Password)
        {
            try
            {
                string message = "";
                Common.Mailer ml = new Common.Mailer();
                CLayer.B2B data = BLayer.B2B.Get(UserId);

                UserManager<StayBazar.Lib.Security.IdentityUser> usrmngr = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                String hashedNewPassword = usrmngr.PasswordHasher.HashPassword(Password);
                BLayer.User.SetPassword(UserId, hashedNewPassword);
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("B2BRegistration") + UserId.ToString() + "&pass=" + Password);
            
            

                msg.To.Add(data.Email);
                msg.Bcc.Add("support@staybazar.com");         
                msg.Subject = "Staybazar Account";    
                msg.Body = message;
                msg.IsBodyHtml = true;
                try
                {
                    await ml.SendMailAsyncForBooking(msg, Common.Mailer.MailType.Reservation);
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
                return true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return false;
            }
          
        }

        public void SaveGST(Models.UserDetailModel model, int CustID)
        {
            long custgststeid = 0;
            try
            {
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                data.SubCustomerAddress = model.SubCustomerAddress;
                data.SubCustomerCity = model.SubCustomerCity;
                data.SubCustomerCityname = model.SubCustomerCityname;
                data.SubCustomerpinCode = model.SubCustomerpinCode;
                data.SubCustomerGstRegNo = model.GSTRegistrationNo;
                data.SubCustomerGstStateId = Convert.ToInt32(model.StateOfRegistration);
                data.SubCustomerid = CustID;
                data.CustomerTableType = 1;
                data.OffGSTId = model.B2bGSTId;
                BLayer.User.SaveGST(data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
        }

        public ActionResult GSTList(int custid)
        {
            List<CLayer.User> users = BLayer.User.GSTList(custid);
            return View("_GSTListCorporate", users);
        }

        public void GSTDelete(int B2bGSTId)
        {
            BLayer.User.GSTDelete(B2bGSTId);
        }


        [AllowAnonymous]
        public ActionResult GetCorporategstDetailsById(long B2bGSTId)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            try
            {
                CLayer.OfflineBooking GstDetails = BLayer.OfflineBooking.GetOfflinegstDetailsById(B2bGSTId);
                model.HiddenSubCustomerCity = GstDetails.SubCustomerCity;
                model.HiddenSubCustomerCityname = GstDetails.SubCustomerCityname;
                model.HiddenSubCustomerAddress = GstDetails.SubCustomerAddress;
                model.HiddenSubCustomerpinCode = GstDetails.SubCustomerpinCode;
                model.HiddenSubCustomerGstRegNo = GstDetails.SubCustomerGstRegNo;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/OfflineBookingGST/HiddenGstAddress.cshtml", model);
        }
    }

}