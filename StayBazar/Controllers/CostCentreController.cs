using Microsoft.AspNet.Identity;
using StayBazar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{
    public class CostCentreController : Controller
    {
        // GET: CostCenter
        public ActionResult Index()
        {
            try
            {
                List<CLayer.CostCentre> CostCentreList = BLayer.CostCentre.GetAll();
                ViewBag.CostCentreTitle = new CostCentreModel() { CostCentreCode = 0, CostCentreName = "", B2B_ID = 0 };
                return View(CostCentreList);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Saved = false;
                Models.CostCentreModel data = new Models.CostCentreModel() { CostCentreCode = 0 };

                CLayer.CostCentre B2BCostCentre = BLayer.CostCentre.Get(id);

                if (B2BCostCentre != null)

                    data = new Models.CostCentreModel()
                    {

                        CostCentreCode = B2BCostCentre.CostCentreCode,
                        CostCentreName = B2BCostCentre.CostcentreName,
                        B2B_ID = B2BCostCentre.B2B_ID
                    };
                return PartialView("~/Views/CostCentre/_Edit.cshtml", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
        [HttpPost]
        public ActionResult Edit(Models.CostCentreModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    long cid = 0;
                    long.TryParse(User.Identity.GetUserId(), out cid);
                    CLayer.CostCentre empcost = new CLayer.CostCentre();
                    {
                        empcost.CostCentreCode  = data.CostCentreCode;
                        empcost.CostcentreName  = data.CostCentreName;
                        empcost.B2B_ID = cid;

                    };
                    BLayer.CostCentre.Save(empcost);

                    ViewBag.Saved = true;
                }
                else
                    ViewBag.Saved = false;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
    }
}