using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace StayBazar.Areas.Admin.Controllers
{

    public class OfferListController : Controller
    {
        [Common.AdminRolePermission]
       public ActionResult Index(long ?id)      
       {
           if (id > 0)
           {
               Models.offersearch search = new Models.offersearch();
               List<CLayer.Offers> offer = BLayer.Offers.GetAllByTab((int)CLayer.ObjectStatus.StatusType.Active, "", 1, 0, 25);
               search.offerlist = offer;   
               ViewBag.Filter = search;
               return View(search);
           }
           else
           {
               Models.offersearch search = new Models.offersearch();
               List<CLayer.Offers> offer = BLayer.Offers.GetAllByTab((int)CLayer.ObjectStatus.StatusType.Active,"", 1, 0, 25);
               search.offerlist = offer;
               ViewBag.Filter = new Models.OfferModel();
               Models.offersearch forPager = new Models.offersearch()
               {
                   SearchString = "",
                   SearchValue = 1,
                   offerlist = offer,
                   TotalRows = 0,
                   Limit = 25,
                   currentPage = 1
               };
               if (offer.Count > 0)
               {
                   forPager.TotalRows = offer[0].TotalRows;
               }
               ViewBag.Filter = search;
               return View(search);
           }
                  
        }      
        //Update Status Active or Disabled 
        [Common.AdminRolePermission]
       [HttpPost]
       public string StatusEdit(int OfferId, int Status)
        {
            try
            {
                string userid = User.Identity.GetUserId();
                long id = 0;
                long.TryParse(userid, out id);
                if (ModelState.IsValid)
                {
                    CLayer.Offers dataStatus = new CLayer.Offers()
                    {
                        OfferId = OfferId,
                        Status = Status,
                        UpdatedBy=id,
                        UpdatedDate=DateTime.Now
                    };
                    BLayer.Offers.EditStatusChange(dataStatus); 
                    ViewBag.Saved = true;                             
                }
                else
                { ViewBag.Saved = false; }                    
                return OfferId.ToString();
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "0";
            }          
        }
      [HttpPost]
      public ActionResult Pager(Models.offersearch data)
       {
           if (data.SearchString == null) data.SearchString = "";
           Models.offersearch search = new Models.offersearch();
           List<CLayer.Offers> offer = BLayer.Offers.GetAllByTab((int)CLayer.ObjectStatus.StatusType.Active, data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit);
           ViewBag.Filter = new Models.OfferModel();
           search.offerlist = offer;
           Models.offersearch forPager = new Models.offersearch()
           {
               Status =data.Status,
               SearchString = data.SearchString,
               SearchValue = data.SearchValue,
                currentPage = data.currentPage,
               offerlist = offer,
               TotalRows = 0,
               Limit = 25,             
           };
           if (offer.Count > 0)
           {
               forPager.TotalRows = offer[0].TotalRows;
           }
           ViewBag.Filter = forPager;
           return PartialView("_List", offer);
       }
       [HttpPost]
       public ActionResult Filter(Models.offersearch data)
        {
            if (data.SearchString == null)
            data.SearchString = "";
            Models.offersearch search = new Models.offersearch();
            List<CLayer.Offers> offer = BLayer.Offers.GetAllByTab((int)CLayer.ObjectStatus.StatusType.Active, data.SearchString, data.SearchValue, 0, 25);
            search.offerlist = offer; 
            ViewBag.Filter = new Models.OfferModel();
            Models.offersearch forPager = new Models.offersearch()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                offerlist=offer,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1
            };
            if (offer.Count > 0)
            {
                forPager.TotalRows = offer[0].TotalRows;
            }          
            ViewBag.Filter = forPager;
            return PartialView("_List", offer);
        }
        [Common.AdminRolePermission]
        [HttpPost]
       public ActionResult Delete(Models.offersearch data)
         {
           try
             {     
             if (data.SearchString == null) 
                 data.SearchString = "";
             Models.offersearch search = new Models.offersearch();
             BLayer.Offers.Delete(data.Id);
             List<CLayer.Offers> offer;
             if (data.Limit > 0)
             {
                  offer = BLayer.Offers.GetAllByTab((int)CLayer.ObjectStatus.StatusType.Active, data.SearchString, data.SearchValue, data.currentPage, data.Limit);
             }
             else
             {
               offer = BLayer.Offers.GetAllByTab((int)CLayer.ObjectStatus.StatusType.Active, "", 1, 0, 25);
             }
             ViewBag.Filter = new Models.OfferModel();
             search.offerlist = offer;
             Models.offersearch forPager = new Models.offersearch()
             {
                 Status = data.Status,
                 SearchString = data.SearchString,
                 SearchValue = data.SearchValue,
                 currentPage = data.currentPage,
                 offerlist = offer,
                 TotalRows = 0,
                 Limit = 25,
             };
             if (offer.Count > 0)
             {
                 forPager.TotalRows = offer[0].TotalRows;
             }
             ViewBag.Filter = forPager;
             return PartialView("_List", offer);
             }
           catch (Exception ex)
           {
               Common.LogHandler.HandleError(ex);
               return RedirectToAction("index");//, new { id = Getdata.AccommodationId });                 
           }
         }
	}
}