using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StayBazar.Models;

namespace StayBazar.Controllers
{
  [HandleError(ExceptionType = typeof(System.Data.DataException), View = "DatabaseError")]
  public class CorporateEmployeeGradesController : Controller
    {
        // GET: CorporateEmployeeGrades
        public ActionResult Index()
        {
          try
          {
            List<CLayer.EmployeeGrades> EmployeeGradesList = BLayer.EmployeeGrades.GetAll();
            ViewBag.EmployeeGradeTitle = new CorporateEmployeeGradeModel() {GradeID=0,GradeCode="",GradeDescription="",B2B_ID=0 };
            return View(EmployeeGradesList);
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
            return View();
        }

        // GET: CorporateEmployeeGrades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CorporateEmployeeGrades/Create
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

        // GET: CorporateEmployeeGrades/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
              ViewBag.Saved = false;
              Models.CorporateEmployeeGradeModel  data = new  Models.CorporateEmployeeGradeModel() { GradeID = 0 };

              CLayer.EmployeeGrades employeegrades = BLayer.EmployeeGrades.Get(id);

              if (employeegrades != null)

                data = new Models.CorporateEmployeeGradeModel()
                {
                  GradeID = employeegrades.GradeID,
                  GradeCode= employeegrades.GradeCode,
                  GradeDescription= employeegrades.GradeDescription,
                  B2B_ID= employeegrades.B2B_ID
                };
              return PartialView("~/Views/CorporateEmployeeGrades/_Edit.cshtml", data);


            }
            catch (Exception ex)
            {
              Common.LogHandler.HandleError(ex);
              return Redirect("~/ErrorPage");
            }
         }

        // POST: CorporateEmployeeGrades/Edit/5
        [HttpPost]
        public ActionResult Edit(Models.CorporateEmployeeGradeModel data)
        {
            try
            {
              if (ModelState.IsValid)
              {
                    long cid = 0;
                    long.TryParse(User.Identity.GetUserId(), out cid);
                    CLayer.EmployeeGrades empGrade = new CLayer.EmployeeGrades();
                {
                  empGrade.GradeID = data.GradeID;
                  empGrade.GradeCode = data.GradeCode;
                  empGrade.GradeDescription = data.GradeDescription;
                  empGrade.B2B_ID = cid;

                };
                BLayer.EmployeeGrades.Save(empGrade);

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
