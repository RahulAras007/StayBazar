using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class TravelAgentController : Controller
    {
        //
        // GET: /Admin/TravelAgents/
        //[Common.AdminRolePermission(AllowAllRoles=true)]
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.UserSearchModel search = new Models.UserSearchModel();
            List<CLayer.User> users = BLayer.User.GetAllB2BForSearch((int)CLayer.ObjectStatus.StatusType.Active, "", 1, (int)CLayer.Role.Roles.Agent, 0, 25);
            search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
            search.SearchString = "";
            search.SearchValue = 1;
            search.userlist = users;
            search.TotalRows = 0;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View(search);
        }
        [HttpPost]
        public string SaveComm(long commUser, double agentCommission)
        {
            try
            {
                if (commUser > 0 && agentCommission > 0)
                {
                    BLayer.B2BUser.SaveAgentCommission(commUser, agentCommission);
                    return "true";
                }
            }catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return "false";
        }

        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public ActionResult Filter(Models.UserSearchModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.User> users = BLayer.User.GetAllB2BForSearch(data.Status, data.SearchString, data.SearchValue, (int)CLayer.Role.Roles.Agent, 0, 25);
            ViewBag.Filter = new Models.UserSearchModel();
            Models.UserSearchModel forPager = new Models.UserSearchModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
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

        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public ActionResult Pager1(Models.UserSearchModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.User> users = BLayer.User.GetAllB2BForSearch(data.Status, data.SearchString, data.SearchValue, (int)CLayer.Role.Roles.Agent, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.UserSearchModel();
            Models.UserSearchModel forPager = new Models.UserSearchModel()
            {
                Status = data.Status,
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

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Details(long id)
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
            CLayer.B2B b2b = BLayer.B2B.Get(id);
            if (b2b != null)
            {
                details.MaxStaff = b2b.MaximumStaff;
                details.Name = b2b.Name;
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

        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Update(Models.UserDetailModel data)
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
                    Name = data.Name//Businessname
                };
                BLayer.B2B.Update(b2b);


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
                    ZipCode=data.ZipCode,
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
                return RedirectToAction("Details", new { id = data.UserId });
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

        [Common.RoleRequired(Administrator = true)]
        public ActionResult Delete(long Id)
        {
            BLayer.User.SetDeleteStatus(Id);
            return RedirectToAction("Index");
        }



        [HttpPost]
        public ActionResult Pager(Models.TransactionsModel data)
        {
            Models.UserDetailModel Ud = new Models.UserDetailModel();
            long id = Ud.UserId; 
            List<CLayer.Booking> users = BLayer.Transaction.GetUserId(id, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.TransactionsModel();
            Models.TransactionsModel forPager = new Models.TransactionsModel()
            {

                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("~/Areas/Admin/Views/Common/_TransactionList.cshtml", users);
        }
    }
}