using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace StayBazar.Areas.Admin.Controllers
{
     [Common.AdminRolePermission(AllowAllRoles=true)]
    public class ConditionController : Controller
    {
        // GET: /Admin/Condition/
         public ActionResult Index(long?id)     
        {       //  
            Models.ConditionModel search = new Models.ConditionModel();
            search.AccommodationId =id;
            List<CLayer.Condition> users = BLayer.Condition.GetAllbyaccomodation(0, 25, id);
            search.TotalRows = 0; 
            search.ConditionList = users;
            search.PropertyId = BLayer.Accommodation.GetPropertyId(id.Value);
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View(search);
        }
        [HttpPost]
        public ActionResult Pager(Models.ConditionModel data)
        {

            Models.ConditionModel search = new Models.ConditionModel();
            List<CLayer.Condition> users = BLayer.Condition.GetAllbyaccomodation((data.currentPage - 1) * data.Limit, data.Limit, data.AccommodationId);
            search.TotalRows = 0;
            search.ConditionList = users;
            search.PropertyId = BLayer.Accommodation.GetPropertyId((long)data.AccommodationId);
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = data.currentPage;
            ViewBag.Filter = search;
            return View(search);
        }
        [HttpPost]
        public ActionResult Details(long ConditionId)
        {
            Models.ConditionModel search = new Models.ConditionModel();
            CLayer.Condition get = BLayer.Condition.GetOnebyConditionId(ConditionId);
            if (get != null)
            {
                search = new Models.ConditionModel
                {             
                    AccommodationId = get.AccommodationId,
                    ConditionId =get.ConditionId,
                    ConditionText =get.ConditionText
                };
            }
            return PartialView("_Details", search);
        }
        [HttpPost]
        public ActionResult Save(Models.ConditionModel data)
        {
            try
            {
                
                CLayer.Condition d = new CLayer.Condition()
                {
                    AccommodationId = data.AccommodationId,
                    ConditionId=data.ConditionId,
                    ConditionText=data.ConditionText

                };
                data.AccommodationId = BLayer.Condition.Save(d);
                return RedirectToAction("index", new { id = data.AccommodationId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return RedirectToAction("index", new { id = data.AccommodationId });
            }
        }
        public ActionResult Delete(long ConditionId,long AccommodationId)
        {

            long AccommodationId1 = BLayer.Condition.Delete(ConditionId);//delete
            Models.ConditionModel search = new Models.ConditionModel();//get Index()
            List<CLayer.Condition> users = BLayer.Condition.GetAllbyaccomodation(0, 25, AccommodationId);
            search.TotalRows = 0;
            search.ConditionList= users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return PartialView("_List", search);
        }
	}
}