using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class TestimonialController : Controller
    {
        //
        // GET: /Testimonial/

        private List<CLayer.Testimonial> InitData(int status)
        {
            return BLayer.Testimonial.GetAll(status);
        }

        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {
                return View(InitData((int)Models.TestimonialModel.StatusList.Enabled));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Inactive()
        {
            try
            {
                return View("Index", InitData((int)Models.TestimonialModel.StatusList.Disabled));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult NewActive()
        {
            try
            {
                ViewBag.Head = "New Testimonial";
                Models.TestimonialModel testimonialmodel = new TestimonialModel() { TestimonialId = 0, Status = (int)Models.TestimonialModel.StatusList.Enabled };
                return View("Edit", testimonialmodel);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult NewInactive()
        {
            try
            {
                ViewBag.Head = "New Testimonial";
                Models.TestimonialModel testimonialmodel = new TestimonialModel() { TestimonialId = 0, Status = (int)Models.TestimonialModel.StatusList.Disabled };
                return View("Edit", testimonialmodel);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Models.TestimonialModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.Testimonial testimonial = new CLayer.Testimonial()
                    {
                        TestimonialId = data.TestimonialId,
                        Name = data.Name,
                        Company = data.Company,
                        Picture = data.Picture,
                        Title = data.Title,
                        Description = data.Description,
                        Status = data.Status,
                        ShowInWidget = data.ShowInWidget
                    };
                    //Picture saving
                    //if (data.photo != null && data.photo.ContentLength > 0)
                    //{
                    //    int MaxContentLength = 1024 * 1024 * 3; //3 MB
                    //    string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf" };
                    //    if (!AllowedFileExtensions.Contains(data.photo.FileName.Substring(data.photo.FileName.LastIndexOf('.'))))
                    //    {
                    //        ModelState.AddModelError(string.Empty, "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                    //    }
                    //    else if (data.photo.ContentLength > MaxContentLength)
                    //    {
                    //        ModelState.AddModelError(string.Empty, "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                    //    }
                    //    else
                    //    {
                    //        var fileName = System.IO.Path.GetFileName(data.photo.FileName);
                    //        var path = System.IO.Path.Combine(Server.MapPath("~/Files/Testimonials"), fileName);
                    //        data.photo.SaveAs(path);
                    //        testimonial.Picture = fileName;
                    //        ModelState.Clear();
                    //    }
                    //}
                    BLayer.Testimonial.Save(testimonial);
                }
                if (data.Status == 1)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Inactive");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View("Edit", data);
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Edit(int id)
        {
            try
            {
                Models.TestimonialModel testimonialmodel = new TestimonialModel() { TestimonialId = 0 };
                CLayer.Testimonial testimonial = BLayer.Testimonial.Get(id);
                if (testimonial != null)
                {
                    testimonialmodel = new TestimonialModel()
                    {
                        TestimonialId = testimonial.TestimonialId,
                        Name = testimonial.Name,
                        Company = testimonial.Company,
                        Picture = testimonial.Picture,
                        Title = testimonial.Title,
                        Description = testimonial.Description,
                        Status = testimonial.Status,
                        ShowInWidget = testimonial.ShowInWidget
                    };
                    ViewBag.Head = testimonial.Title;
                }
                return View("Edit", testimonialmodel);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Delete(int id)
        {
            try
            {
                CLayer.Testimonial t = BLayer.Testimonial.Get(id);
                BLayer.Testimonial.Delete(id);
                if (t.Status == (int)Models.TestimonialModel.StatusList.Enabled)
                    return RedirectToAction("Index");
                else
                    return RedirectToAction("Inactive");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Enable(int id)
        {
            try
            {
                BLayer.Testimonial.SetStatus(id, (int)Models.TestimonialModel.StatusList.Enabled);
                return RedirectToAction("Inactive");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission]
        public ActionResult Disable(int id)
        {
            try
            {
                BLayer.Testimonial.SetStatus(id, (int)Models.TestimonialModel.StatusList.Disabled);
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

                if (type == "disable")
                {
                    BLayer.Testimonial.SetStatus(data, (int)Models.TestimonialModel.StatusList.Disabled);
                }
                if (type == "enable")
                {
                    action = "Inactive";
                    BLayer.Testimonial.SetStatus(data, (int)Models.TestimonialModel.StatusList.Enabled);
                }
                if (type == "delete")
                {
                    int status = 1;
                    if (data.IndexOf(",") > -1) { string[] ids = data.Split(','); status = BLayer.Query.Get(Convert.ToInt32(ids[0])).Status; }
                    else { status = BLayer.Testimonial.Get(Convert.ToInt32(data)).Status; }
                    if (status == 2) { action = "Inactive"; }

                    BLayer.Testimonial.Delete(data);
                }
            }
            catch (Exception) { action = "Index"; }

            return action;
        }
    }
}