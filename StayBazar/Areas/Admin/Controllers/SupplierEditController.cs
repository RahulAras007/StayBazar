using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SupplierEditController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }
          [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Details(long? id, long? B2bId)
        {

            Areas.Admin.Models.UserDetailModel details = null;
            CLayer.User data = null;
             if(id.HasValue && id.Value > 0) 
                 BLayer.User.Get(id.Value);
             
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
            else
            {
                details = new Areas.Admin.Models.UserDetailModel(){UserId = 0};
            }
            CLayer.B2B b2b = BLayer.B2B.Get(id.Value);
            if (b2b != null)
            {
                details.MaxStaff = b2b.MaximumStaff;
                details.Name = b2b.Name;
                details.PANNo = b2b.PANNo;
            }

            CLayer.Address adrs = null;
            if(id.HasValue && id.Value > 0) BLayer.Address.GetPrimaryOnUser(id.Value);
            if (adrs != null)
            {
                details.Address = adrs.AddressText;
                details.AddressId = adrs.AddressId;
                details.State = adrs.State;

                details.CountryId = adrs.CountryId;
                details.Phone = adrs.Phone;
                details.Mobile = adrs.Mobile;
                details.ZipCode = adrs.ZipCode;
                details.City = "";
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
                details.CityId = adrs.CityId;
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
                return RedirectToAction("Details", new { id = data.UserId,B2bId=data.AffiliateId });
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
        public ActionResult DeleteSupplier(long Id, long B2bId)
        {
            BLayer.User.SetDeleteStatus(Id);
            return RedirectToAction("Details", "SupplierEdit", new { id = Id,B2bId = B2bId, activeTab=4 });
        }
          [Common.AdminRolePermission(AllowAllRoles=true)]
        public ActionResult Delete(long Userid, long B2bId)
        {
            BLayer.B2B.DeleteSupplierAfffiliate(Userid,B2bId);
            return RedirectToAction("Details", "Affiliate", new { id = B2bId, B2bId = B2bId, activeTab = 4 });
        }

    }
}