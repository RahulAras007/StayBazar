using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{
    public class SuggestController : Controller
    {
        //
        // GET: /Suggest/
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                return View(new Models.SuggestModel());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [AllowAnonymous]
        public ActionResult Posted()
        {
            return View();
        }

        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Models.SuggestModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.Suggest suggest = new CLayer.Suggest()
                    {
                        InfoId = data.InfoId,
                        Name = data.Name,
                        Location = data.Location,
                        City = data.City,
                        CountryId = data.CountryId,
                        Address = data.Address,
                        Phone = data.Phone,
                        Email = data.Email
                    };
                    BLayer.Suggest.Save(suggest);
                    return RedirectToAction("Posted");
                }
                else
                {
                    return RedirectToAction("index");
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
    }
}