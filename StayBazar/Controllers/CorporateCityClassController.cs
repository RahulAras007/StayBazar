using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using StayBazar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{
    public class CorporateCityClassController : Controller
    {
        // GET: CorporateCityClass
        public ActionResult Index()
        {
            //return View();
            try
            {
                List<CLayer.CityClass> CityClassList = BLayer.CityClass.GetAll();
                ViewBag.CityClassTitle = new CorporateCityClassModel() { CityClassID = 0, CityClassCode = "", CityClassDescription = "", CountryID = 0, CityID = 0, StateID = 0, B2B_ID = 0 };
                return View(CityClassList);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        // GET: CorporateCityClass/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CorporateCityClass/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CorporateCityClass/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: CorporateCityClass/Edit/5
        public ActionResult Edit(int id = 0)
        {
            try
            {
                Models.CorporateCityClassModel data = new Models.CorporateCityClassModel() { CityClassID = 0 };

                CLayer.CityClass cityclass = new CLayer.CityClass();
                cityclass = BLayer.CityClass.GetDetails(id);

                if (cityclass != null)

                    data = new Models.CorporateCityClassModel()
                    {
                        CityClassID = cityclass.CityClassID,
                        CityClassCode = cityclass.CityClassCode,
                        CityClassDescription = cityclass.CityClassDescription,
                        B2B_ID = cityclass.B2B_ID,
                        CountryIDs = cityclass.CountryIDs,
                        StateIDs = cityclass.StateIDs,
                        CityIDs = cityclass.CityIDs,
                        CountryNames = cityclass.CountryNames,
                        StateNames = cityclass.StateNames,
                        CityNames = cityclass.CityNames

                    };
                return PartialView("~/Views/CorporateCityClass/_Edit.cshtml", data);


                //return Json(cityclass, "application/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        // POST: CorporateCityClass/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: CorporateCityClass/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CorporateCityClass/Delete/5
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
        public ActionResult SaveDetails(int CityClassID,string CityClassCode, string CityClassDesc, string ddlCountryID, string ddlstateID, string ddlCityID)
        {
            try
            {
                long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);
                CLayer.CityClassResult result = new CLayer.CityClassResult();
                CLayer.CityClass cityclass = new CLayer.CityClass();
                cityclass.CityClassCode = CityClassCode;
                cityclass.CityClassDescription = CityClassDesc;
                cityclass.CountryIDs = ddlCountryID;
                cityclass.StateIDs = ddlstateID;
                cityclass.CityIDs = ddlCityID;
                cityclass.CityClassID = CityClassID;
                cityclass.B2B_ID = cid;

                //string[] CityIds = ddlCityID.Split(',');
                //if (CityClassID>0)
                //{
                    result = BLayer.CityClass.CheckCityIfExist(ddlCityID, CityClassID);

                    if (result.Result == "0")
                    {
                        result = BLayer.CityClass.Save(cityclass);
                    }
                //}
                //else
                //{
                //    result = BLayer.CityClass.Save(cityclass);
                //}
                
                return Json(result, "application/json", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
    }
}
