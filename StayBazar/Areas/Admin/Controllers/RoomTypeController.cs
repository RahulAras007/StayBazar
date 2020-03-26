using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class RoomTypeController : Controller
    {
        //
        // GET: /RoomType/
        [Authorize(Roles = "Administrator , Staff")]
        public ActionResult Index()
        {
            try
            {
                List<CLayer.RoomType> roomtypes = BLayer.RoomType.GetAll();
                ViewBag.RoomType = new RoomTypeModels() { RoomTypeId = 0, Title = "" , PropertyTypesAssigned = ""};
                return View(roomtypes);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Authorize(Roles = "Administrator , Staff")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(Models.RoomTypeModels data)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    CLayer.RoomType roomtype = new CLayer.RoomType() { RoomTypeId = data.RoomTypeId, Title = data.Title };
                    int roomId = BLayer.RoomType.Save(roomtype);
                    string types = "";
                    if(data.PropertyTypesAssigned != null)
                    {
                        types = data.PropertyTypesAssigned.Trim();
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
                    BLayer.RoomType.SetPropertyType(roomId,types);
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
        [Authorize(Roles = "Administrator , Staff")]
        public ActionResult Edit(int id)
        {
            try
            {
                ViewBag.Saved = false;
                Models.RoomTypeModels mRoomType = new RoomTypeModels() { RoomTypeId = 0 };


                CLayer.RoomType roomtype = BLayer.RoomType.Get(id);

                if (roomtype != null)
                    mRoomType = new RoomTypeModels() { RoomTypeId = roomtype.RoomTypeId, Title = roomtype.Title };

                return PartialView("_edit", mRoomType);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }
        [Authorize(Roles = "Administrator , Staff")]
        public ActionResult Delete(int id)
        {
            try
            {
                BLayer.RoomType.Delete(id);
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