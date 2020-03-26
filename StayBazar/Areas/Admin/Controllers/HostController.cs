using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;
using StayBazar.Lib.Security;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class HostController : Controller
    {
        //
        // GET: /Admin/Host/

        private List<CLayer.User> InitData(int status)
        {
            return BLayer.User.GetAllStaff(status);
        }

        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {
                return View(InitData((int)HostModel.StatusTypes.Active));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission]
        public ActionResult Blocked()
        {
            try
            {
                return View("Index", InitData((int)HostModel.StatusTypes.Blocked));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission]
        public ActionResult RecentlyDeleted()
        {
            try
            {
                return View("Index", InitData((int)HostModel.StatusTypes.Deleted));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission]
        public ActionResult New()
        {
            try
            {
                ViewBag.Head = "New Staff";
                HostModel usr = new HostModel() { UserId = 0, Status = (int)HostModel.StatusTypes.Active };
                usr.SelectedSbEntity = new List<CLayer.SBEntity>();
                return View("Edit", usr);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(HostModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.User pt = new CLayer.User()
                    {
                        UserId = data.UserId,
                        SalutationId = data.SalutationId,
                        FirstName = data.FirstName,
                        LastName = data.LastName,
                        DateOfBirth = Convert.ToDateTime(data.DateOfBirth),
                        salesregion = data.SalesRegion,
                        Sex = data.Sex,
                        UserTypeId = data.UserTypeId,
                        Status = data.Status,
                        Email = data.Email,
                        OPSEmail = data.OPSEmail


                    };
                    long saved = BLayer.User.Save(pt);

                    CLayer.Address address = new CLayer.Address()
                    {
                        AddressId = data.AddressId,
                        UserId = data.UserId,
                        AddressText = data.Address,
                        CityId = data.CityId,
                        State = data.State,
                        CountryId = data.CountryId,
                        ZipCode = data.ZipCode,
                        Mobile = data.Mobile,
                        AddressType = (int)CLayer.Address.AddressTypes.Primary
                    };
                    if (data.City != null && data.City != "")
                        address.City = data.City;
                    if (data.CityId > 0)
                        address.City = BLayer.City.Get(data.CityId).Name;
                    address.AddressType = (int)CLayer.Address.AddressTypes.Primary;
                    BLayer.Address.Save(address);
                    
                    //update phone
                    pt.Phone = data.Phone;
                    pt.UserId = saved;
                    List<long> sbentylst = new List<long>();
                    if (data.SbEntities != null)
                    {
                        sbentylst = data.SbEntityList.Where(p => !data.SbEntities.Any(p2 => p2 == p.EntityId)).Select(c => c.EntityId).ToList();
                    }
                    else
                    {
                        sbentylst = data.SbEntityList.Select(c => c.EntityId).ToList();
                    }
                    BLayer.User.SaveInfo(pt, sbentylst);


                    if (saved > 0)
                    {

                        if (data.UserId == 0)
                        {
                            UserManager<StayBazar.Lib.Security.IdentityUser> usrmngr = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                            String userId = User.Identity.GetUserId();

                            String newPassword = BLayer.Settings.GetValue(CLayer.Settings.DEFAULT_PASSWORD);
                            String hashedNewPassword = usrmngr.PasswordHasher.HashPassword(newPassword);
                            BLayer.User.SetPassword(saved, hashedNewPassword);

                        }
                        else
                        {
                            if (data.PasswordHash != null)
                            {
                                UserManager<StayBazar.Lib.Security.IdentityUser> usrmngr = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());

                                String newPassword = data.PasswordHash;
                                String hashedNewPassword = usrmngr.PasswordHasher.HashPassword(newPassword);
                                BLayer.User.SetPassword(saved, hashedNewPassword);
                            }
                        }
                        if (data.UserTypeId == (int)CLayer.Role.Roles.SalesPerson || data.UserTypeId == 16)
                        {
                            BLayer.User.SaveSalesRegion(saved, pt.salesregion);
                        }
                        BLayer.User.AddUserRole(saved, data.UserTypeId);
                        ViewBag.Saved = true;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Saved = false;
                        ViewBag.ErrorMessage = "Other user with this EmailId exists.";
                        ViewBag.Head = "New Staff";
                        return View("Edit", data);
                    }
                }
                else
                {
                    if (data.UserId == 0) { ViewBag.Head = "New Staff"; }
                    ViewBag.Saved = false;
                    return View("Edit", data);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission]
        public ActionResult Edit(int id)
        {
            try
            {
                Admin.Models.HostModel hostModel = new Admin.Models.HostModel();
                if (id != 0)
                {
                    CLayer.User user = BLayer.User.Get(id);
                    if (user != null)
                    {
                        hostModel.UserId = user.UserId;
                        hostModel.SalutationId = user.SalutationId;
                        hostModel.FirstName = user.FirstName;
                        hostModel.SalesRegion = user.salesregion;
                        hostModel.LastName = user.LastName;
                        hostModel.DateOfBirth = user.DateOfBirth.ToShortDateString();
                        hostModel.Sex = user.Sex;
                        hostModel.UserTypeId = user.UserTypeId;
                        hostModel.Status = user.Status;
                        hostModel.Email = user.Email;
                        hostModel.OPSEmail = user.OPSEmail;
                        hostModel.CreatedDate = user.CreatedDate;
                        List<CLayer.Address> addr = BLayer.Address.GetOnUser(id);
                        if (addr.Count > 0)
                        {
                            hostModel.AddressId = addr[0].AddressId;
                            hostModel.Address = addr[0].AddressText;
                            hostModel.Phone = addr[0].Phone;
                            hostModel.Mobile = addr[0].Mobile;
                            hostModel.State = addr[0].State;
                            hostModel.CityId = addr[0].CityId;
                            hostModel.City = addr[0].City;
                            hostModel.CountryId = addr[0].CountryId;
                            hostModel.ZipCode = addr[0].ZipCode;
                            hostModel.Mobile = addr[0].Mobile;
                        }
                        //else
                        //{
                        //    hostModel.CityId = 7;
                        //    hostModel.State =3;
                        //    hostModel.CountryId = 1;
                        //}
                  
                        hostModel.LoadPlacesStaff();
                        if (addr.Count > 0)
                        {
                            if (addr[0].CityId > 0)
                            {
                                hostModel.CityId = addr[0].CityId;
                            }
                            else
                            {
                                hostModel.City = addr[0].City;
                            }
                        }


                        hostModel.SbEntities = hostModel.SbEntityList.Select(c => c.EntityId).ToList();
                        hostModel.SelectedSbEntity = new List<CLayer.SBEntity>();
                        if (user.SbEntities != null)
                        {
                            if (user.SbEntities != "")
                            {
                                List<long> sbentylst = new List<long>(Array.ConvertAll(user.SbEntities.Split(','), long.Parse));
                                hostModel.SbEntities = hostModel.SbEntityList.Where(p => !sbentylst.Any(p2 => p2 == p.EntityId)).Select(c => c.EntityId).ToList();
                            }
                        }
                        //get phone
                        CLayer.User userAddressdata = BLayer.User.getuserAddressdata(id);
                        ViewBag.Head = user.FirstName + " " + user.LastName;
                        if (userAddressdata != null)
                        {
                            hostModel.Phone = userAddressdata.Phone;
                        }
                    }
                }
                return View("Edit", hostModel);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission]
        public ActionResult Block(int id)
        {
            try
            {
                BLayer.User.SetStatus(id.ToString(), (int)HostModel.StatusTypes.Blocked);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission]
        public ActionResult Restore(int id)
        {
            try
            {
                BLayer.User.SetStatus(id.ToString(), (int)HostModel.StatusTypes.Active);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.RoleRequired]
        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.User.SetStatus(id.ToString(), (int)HostModel.StatusTypes.Deleted);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission]
        [HttpPost]
        public string Actionset(string data, string type)
        {
            string action = "Index";
            try
            {
                if (type == "block")
                {
                    BLayer.User.SetStatus(data, (int)HostModel.StatusTypes.Blocked);
                }
                if (type == "restore")
                {
                    BLayer.User.SetStatus(data, (int)HostModel.StatusTypes.Active);
                }
                if (type == "delete")
                {
                    BLayer.User.SetStatus(data, (int)HostModel.StatusTypes.Deleted);
                }
            }
            catch (Exception) { action = "0"; }
            return action;
        }
      
    }
}