using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Models;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SearchSupplierController : Controller
    {
        public const int PAGELIMIT = 25;
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Index(SearchSupplierModel model, long B2bId)
        {

            try
            {
                if (model.Name == null) model.Name = "";
                SearchSupplierModel data = new SearchSupplierModel();
                List<CLayer.B2B> SupplierList = new List<CLayer.B2B>();
                string name = model.Name;
                data.SearchString = model.Name;
                data.CurrentPage = 1;
                int totr = 0;
                SupplierList = BLayer.B2B.GetSearchAllSupplier(name, 0, PAGELIMIT, out totr);
                data.SearchList = SupplierList;
                data.B2BId = B2bId;
                data.TotalRows = totr;
                return View("Index", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }


        }
        public ActionResult GetSupplierList(SearchSupplierModel model)
        {
            try
            {
                if (model.Name == null) model.Name = "";
                SearchSupplierModel data = new SearchSupplierModel();
                List<CLayer.B2B> SupplierList = new List<CLayer.B2B>();
                string name = model.Name;
                data.SearchString = model.Name;
                data.CurrentPage = 1;
                int totr = 0;
                data.B2BId = model.B2BId;
                SupplierList = BLayer.B2B.GetSearchAllSupplier(name, 0, PAGELIMIT, out totr);
                data.SearchList = SupplierList;
                data.TotalRows = totr;
                return View("Index", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
        public ActionResult Pager(SearchSupplierModel model)
        {
            try
            {
                if (model.SearchString == null) model.SearchString = "";
                SearchSupplierModel data = new SearchSupplierModel();
                List<CLayer.B2B> SupplierList = new List<CLayer.B2B>();
                string name = model.SearchString;
                data.SearchString = model.SearchString;
                data.CurrentPage = model.CurrentPage;
                int totr = 0;
                data.B2BId = model.B2BId;
                SupplierList = BLayer.B2B.GetSearchAllSupplier(name, ((model.CurrentPage - 1) * PAGELIMIT), PAGELIMIT, out totr);
                data.TotalRows = totr;
                data.SearchList = SupplierList;
                //ViewBag.
                return View("Details", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
       [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult AddSupplier(long UserId,long B2bId)
        {
            //long B2bId = 0;
            //long.TryParse(User.Identity.GetUserId(), out B2bId);
            CLayer.B2B data = new CLayer.B2B();
            data.B2BId = B2bId;
            data.UserId = UserId;
            data.UserType = (int)CLayer.Role.Roles.Supplier;
            BLayer.B2B.SaveSupplierForAffiliate(data);
            //return RedirectToAction("Index", "SearchSupplier", new { B2bId=B2bId});
            return RedirectToAction("Details", "Affiliate", new {id=B2bId, B2bId = B2bId, activeTab = 4 });
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Update(Models.UserDetailModel data)
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
                    PANNo = data.PANNo
                };
                BLayer.B2B.Update(b2b);

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
                return RedirectToAction("SupplierDetails", new { id = data.UserId, B2bId = data.AffiliateId });
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
            return View("SupplierDetails", data);
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult SupplierDetails(long? id, long? B2bId)
        {
            Areas.Admin.Models.UserDetailModel details = null;
            CLayer.User data = BLayer.User.Get(id.Value);
            if (data != null)
            {
                details = new Areas.Admin.Models.UserDetailModel()
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
                    PANNo = data.PANNo,
                    AffiliateId=B2bId.Value
                };
            }
            CLayer.B2B b2b = BLayer.B2B.Get(id.Value);
            if (b2b != null)
            {
                details.MaxStaff = b2b.MaximumStaff;
                details.Name = b2b.Name;
                details.PANNo = b2b.PANNo;
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
            return View(details);
        }
        [Common.RoleRequired(Administrator = true)]
        public ActionResult DeleteSupplier(long Id, long B2bId)
        {
            BLayer.User.SetDeleteStatus(Id);
            return RedirectToAction("SupplierDetails", "SearchSupplier", new { id = Id,B2bId = B2bId, activeTab=4 });
        }
    }
}