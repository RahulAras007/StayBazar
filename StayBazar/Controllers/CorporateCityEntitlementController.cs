using StayBazar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{
    public class CorporateCityEntitlementController : Controller
    {
        // GET: CorporateEmployeeGrades
        public ActionResult Index()
        {
            try
            {
                CLayer.CityEntitle CityClassList = new CLayer.CityEntitle();
                ViewBag.CityEntitle= new CorporateCityEntitlement() { GradeId = 0, GradeCode = "", Gradedescription = ""};
                return View(CityClassList);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        // GET: CorporateEmployeeGrades/Details/5
        public ActionResult Details(int id)
        {
            List<CLayer.CityEntitle> employeegrades = BLayer.CityEntitle.Get(id);
            return Json(employeegrades, "application/json", JsonRequestBehavior.AllowGet);
        }

        // GET: CorporateEmployeeGrades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CorporateCityEntitlement/Savedetails
        [HttpPost]
        public ActionResult Savedetails(string ClsID,string Amount,Models.CorporateCityEntitlement data)
        {
            try
            {
                CLayer.CityEntitle details = new CLayer.CityEntitle();
                details.GradeId = data.GradeId;
                details.DefaultAmount = data.DefaultAmount;
                string[] classid;
                string[] classAmt;
                classid = ClsID.Split(',');
                classAmt= Amount.Split(',');
                CLayer.CityEntitleSvaeRslt Rslt = BLayer.CityEntitle.Savedetails(classid, classAmt, details);


                return Json(Rslt, "application/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        // GET: CorporateEmployeeGrades/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CorporateEmployeeGrades/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CorporateEmployeeGrades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CorporateEmployeeGrades/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
