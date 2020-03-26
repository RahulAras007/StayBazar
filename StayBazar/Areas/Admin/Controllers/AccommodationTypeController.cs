using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class AccommodationTypeController : Controller
    {
        //
        // GET: /RoomType/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {
                List<CLayer.AccommodationType> roomtypes = BLayer.AccommodationType.GetAll();
                ViewBag.AccommodationType = new Models.AccommodationTypeModel() { AccommodationTypeId = 0, Title = "", StayTypesAssigned = "" };
                return View(roomtypes);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Models.AccommodationTypeModel data)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CLayer.AccommodationType roomtype = new CLayer.AccommodationType()
                    {
                        AccommodationTypeId = data.AccommodationTypeId,
                        Title = data.Title
                    };
                    int roomId = BLayer.AccommodationType.Save(roomtype);
                    string types = "";
                    if (data.StayTypesAssigned != null)
                    {
                        types = data.StayTypesAssigned.Trim();
                        //the following method is for preventing hidden text field modification
                        /* int ids;
                         string[] splitted = types.Split(new char[] {','});
                         if (splitted.Length == 0)
                             types = "";
                         else
                             if(splitted.Length == 1)
                             {
                                 if (int.TryParse(splitted[0], out ids))
                                     types = ids.ToString();
                             }
                             else
                             {
                                 types = "";
                                 foreach(string s in splitted)
                                 {
                                     if (int.TryParse(s, out ids))
                                     {
                                         if (types != "") types = types + ",";
                                         types = ids.ToString();
                                     }
                                        
                                 }
                             }*/

                    }
                    BLayer.AccommodationType.SetCategoryType(roomId, types);
                    ViewBag.Saved = true;
                }
                else
                    ViewBag.Saved = false;
                return RedirectToAction("Index");
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
                ViewBag.Saved = false;
                Models.AccommodationTypeModel mRoomType = new Models.AccommodationTypeModel() { AccommodationTypeId = 0 };


                CLayer.AccommodationType roomtype = BLayer.AccommodationType.Get(id);

                if (roomtype != null)
                    mRoomType = new Models.AccommodationTypeModel() { AccommodationTypeId = roomtype.AccommodationTypeId, Title = roomtype.Title };

                return PartialView("_edit", mRoomType);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Common.RoleRequired(Administrator = true)]
        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.AccommodationType.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
    }
}