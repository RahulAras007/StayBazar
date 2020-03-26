using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{

    [Common.RoleRequired(Supplier = true)]
    public class InventoryController : Controller
    {
        // GET: /Admin/Inventory/
        public ActionResult Index(long? id)
        
        {         
            Models.InventoryModel search = new Models.InventoryModel();
            search.AccommodationId =id;
            List<CLayer.Inventory> users = BLayer.Inventory.GetAllbyaccomodation(0, 25, id);
            search.TotalRows = 0; 
            search.InventoryList = users;
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
        //Pager Waiting list
        [HttpPost]
        public ActionResult Pager(Models.InventoryModel data)
        {
            
            Models.InventoryModel search = new Models.InventoryModel();
            List<CLayer.Inventory> users = BLayer.Inventory.GetAllbyaccomodation((data.currentPage - 1) * data.Limit, data.Limit, data.AccommodationId);
            search.TotalRows = 0;
            search.InventoryList = users;
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
        //[HttpPost]
        public ActionResult Filter(long AccommodationId)
        
        {
          
            Models.InventoryModel search = new Models.InventoryModel();
            List<CLayer.Inventory> users = BLayer.Inventory.GetAllbyaccomodation(0, 25, AccommodationId);
            search.TotalRows = 0;
            search.InventoryList = users;
            search.PropertyId = BLayer.Accommodation.GetPropertyId(AccommodationId);
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return PartialView("_List", search);        
        }
         [HttpPost]
        public ActionResult Details(long InventoryId)
        {
            Models.InventoryModel search = new Models.InventoryModel();
            CLayer.Inventory get = BLayer.Inventory.GetOnebyInventoryId(InventoryId);
            if (get != null)
            {
                search = new Models.InventoryModel
                {
                    AccommodationId=get.AccommodationId,
                    InventoryId=get.InventoryId,
                    Quantity=get.Quantity,
                    StartDay=get.FromDate.Day,
                    EndDay=get.ToDate.Day,
                    StartMonth=get.FromDate.Month,
                    EndMonth=get.ToDate.Month 

                };
            }
            return PartialView("_Details", search);
        }
        [HttpPost]
         public ActionResult Save(Models.InventoryModel data)
        
       {
            try
            {
                DateTime ToDate1;
                if (data.StartMonth > data.EndMonth)
                {
                ToDate1 = new DateTime(DateTime.Today.Year +1, data.EndMonth, data.EndDay);         
                }
                else
                {
                    ToDate1 = new DateTime(DateTime.Today.Year, data.EndMonth, data.EndDay);             
                }            
                CLayer.Inventory d = new CLayer.Inventory()
                {                 
                    AccommodationId = data.AccommodationId,
                    InventoryId = data.InventoryId,
                    Quantity = data.Quantity,
                    FromDate = new DateTime(DateTime.Today.Year, data.StartMonth, data.StartDay),
                    ToDate = ToDate1
                };
             long AccId  = BLayer.Inventory.save(d);
              return RedirectToAction("index", new { id = data.AccommodationId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return RedirectToAction("index", new { id = data.AccommodationId });
            }
        }    
        public ActionResult Delete(long InventoryId, long AccommodationId)
        {
          
            long AccommodationId1= BLayer.Inventory.Delete(InventoryId, AccommodationId);//delete
            Models.InventoryModel search = new Models.InventoryModel();
            List<CLayer.Inventory> users = BLayer.Inventory.GetAllbyaccomodation(0, 25, AccommodationId);
            search.TotalRows = 0;
            search.InventoryList = users;
            search.PropertyId = BLayer.Accommodation.GetPropertyId(AccommodationId);
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