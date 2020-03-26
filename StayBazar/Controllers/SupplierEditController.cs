using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;

namespace StayBazar.Controllers
{
    public class SupplierEditController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(long? id)
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
                    PANNo = data.PANNo
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
        public ActionResult Delete(long Userid)
        {
            long B2BId = 0;
            long.TryParse(User.Identity.GetUserId(), out B2BId);
            BLayer.B2B.DeleteSupplierAfffiliate(Userid,B2BId);
            return RedirectToAction("Index","SupplierList");
        }
      
	}
}