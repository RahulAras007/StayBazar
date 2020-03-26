using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission(AllowAllRoles=true)]
    
    public class OfferController : Controller
    {
//#Private Region
         public long savedata(Models.OfferModel Getdata)
         {
            
             CLayer.Offers OfferData = new CLayer.Offers()
             {
                 OfferTitle = Getdata.OfferTitle,
                 OfferId = Getdata.OfferId,
                 NoOfDays = Getdata.NoOfDays,
                 StartDate = Getdata.StartDate,
                 EndDate = Getdata.EndDate,
                 Status = (int)CLayer.ObjectStatus.StatusType.Active,
                 AccommodationId = Getdata.AccommodationId,
                 Amount = Getdata.Amount,
                 OfferFor = Getdata.OfferFor,
                 RateType = Getdata.RateType,
                 SBCommission = Getdata.SBCommission,
                 FreeDays = Getdata.FreeDays,
                 OfferType = Getdata.OfferType,
                 StayCategoryId = Getdata.StayCategoryId
             };
          long id= BLayer.Offers.Save(OfferData);
          return (id);

      
         }
         public static string CSVNumericValidation(string csvValues)
         {
             string result = "";
             string[] ids = csvValues.Split(',');
             int i, len;
             long val;
             len = ids.Count();
             for (i = 0; i < len; i++)
             {
                 val = 0;
                 if (long.TryParse(ids[i].Trim(), out val))
                 {
                     if (val > 0)
                     {
                         if (result != "") result = result + ",";
                         result = result + val.ToString();
                     }
                 }
             }
             return result;
         }
         [OutputCache(VaryByParam = "*", Duration = 0, NoStore = true)]
         //[Route("/Admin/Offer/")]
         public ActionResult Index(long? OfferId)
         {                                 
           if (OfferId.HasValue)
             {
                 Models.OfferModel Setdata1 = new Models.OfferModel() { OfferId = 0 };
                 ViewBag.Saved = false;
                 CLayer.Offers Getdata1 = BLayer.Offers.GetByOfferId(OfferId.Value, (int)CLayer.ObjectStatus.StatusType.All);
                 if (Getdata1 != null)
                 {
                     Setdata1 = new Models.OfferModel()
                     {
                         SOfferTitle = Getdata1.OfferTitle,
                         SNoOfDays = Getdata1.NoOfDays,
                         SStartDate = Getdata1.StartDate,
                         SEndDate = Getdata1.EndDate,
                         SAccommodationId = Getdata1.AccommodationId,
                         SAmount = Getdata1.Amount,
                         SOfferFor = Getdata1.OfferFor,
                         SRateType = Getdata1.RateType,
                         SSBCommission = Getdata1.SBCommission,
                         SFreeDays = Getdata1.FreeDays,
                         SPropertyId = Getdata1.PropertyId,
                         SOfferType = Getdata1.OfferType,
                         SStayCategoryId = Getdata1.StayCategoryId,
                         SearchValue = Getdata1.OfferType,

                         //second
                         OfferTitle = Getdata1.OfferTitle,
                         NoOfDays = Getdata1.NoOfDays,
                         StartDate = Getdata1.StartDate,
                         EndDate = Getdata1.EndDate,
                         AccommodationId = Getdata1.AccommodationId,
                         Amount = Getdata1.Amount,
                         OfferFor = Getdata1.OfferFor,
                         RateType = Getdata1.RateType,
                         SBCommission = Getdata1.SBCommission,
                         FreeDays = Getdata1.FreeDays,
                         PropertyId = Getdata1.PropertyId,
                         OfferType = Getdata1.OfferType,
                         StayCategoryId = Getdata1.StayCategoryId
                     };
                     Setdata1.OfferId = OfferId.Value;
                     Setdata1.SOfferId = OfferId.Value;
                 }
                return View("~/Areas/Admin/Views/Offer/Index.cshtml",Setdata1);
             }
             else
             {
                 Models.OfferModel search = new Models.OfferModel();
                 ViewBag.Filter = search;
                 return View("~/Areas/Admin/Views/Offer/Index.cshtml", search);
             }                
         }
         public ActionResult OfferDetails(long OfferId)
        {
                Models.OfferModel Setdata1 = new Models.OfferModel() { OfferId = 0 };
                ViewBag.Saved = false;
                CLayer.Offers Getdata1 = BLayer.Offers.GetByOfferId(OfferId, (int)CLayer.ObjectStatus.StatusType.All);
                if (Getdata1 != null)
                {
                    Setdata1 = new Models.OfferModel()
                    {
                        SOfferTitle = Getdata1.OfferTitle,
                        SOfferId = Getdata1.OfferId,
                        SNoOfDays = Getdata1.NoOfDays,
                        SStartDate = Getdata1.StartDate,
                        SEndDate = Getdata1.EndDate,
                        SAccommodationId = Getdata1.AccommodationId,
                        SAmount = Getdata1.Amount,
                        SOfferFor = Getdata1.OfferFor,
                        SRateType = Getdata1.RateType,
                        SSBCommission = Getdata1.SBCommission,
                        SFreeDays = Getdata1.FreeDays,
                        SPropertyId = Getdata1.PropertyId,
                        SOfferType = Getdata1.OfferType,
                        SStayCategoryId = Getdata1.StayCategoryId,
                        //second

                        OfferTitle = Getdata1.OfferTitle,
                        OfferId = Getdata1.OfferId,
                        NoOfDays = Getdata1.NoOfDays,
                        StartDate = Getdata1.StartDate,
                        EndDate = Getdata1.EndDate,
                        AccommodationId = Getdata1.AccommodationId,
                        Amount = Getdata1.Amount,
                        OfferFor = Getdata1.OfferFor,
                        RateType = Getdata1.RateType,
                        SBCommission = Getdata1.SBCommission,
                        FreeDays = Getdata1.FreeDays,
                        PropertyId = Getdata1.PropertyId,
                        OfferType = Getdata1.OfferType,
                        StayCategoryId = Getdata1.StayCategoryId
                    };
                }
                return View(
                    "Index",Setdata1);
        }
         [HttpPost]
         public ActionResult Pager(Models.OfferModel data)
         {
             Models.OfferModel search = new Models.OfferModel();
             List<CLayer.Offers> offer = BLayer.Offers.GetAllByStatus(data.Status, (data.currentPage - 1) * data.Limit, data.Limit);
             ViewBag.Filter = new Models.TransactionsModel();
             search.offerlist = offer;
             Models.TransactionsModel forPager = new Models.TransactionsModel()
             {
                 TotalRows = 0,
                 Limit = 25,
                 currentPage = data.currentPage
             };
             if (search.offerlist.Count > 0)
             {
                 search.TotalRows = offer[0].TotalRows;
             }
             ViewBag.Filter = forPager;
             return PartialView("_List", search);
         }          
         public ActionResult Get(long? OfferId)
         {
             Models.OfferModel Setdata = new Models.OfferModel() { OfferId = 0 };
             if (OfferId.HasValue)
             {
                 ViewBag.Saved = false;               
                 CLayer.Offers Getdata = BLayer.Offers.GetByOfferId(OfferId.Value, (int)CLayer.ObjectStatus.StatusType.Active);
                 if (Getdata != null)
                     Setdata = new Models.OfferModel()
                     {
                         OfferTitle = Getdata.OfferTitle,
                         OfferId = Getdata.OfferId,
                         NoOfDays = Getdata.NoOfDays,
                         StartDate = Getdata.StartDate,
                         EndDate = Getdata.EndDate,
                         AccommodationId = Getdata.AccommodationId,
                         Amount = Getdata.Amount,
                         OfferFor = Getdata.OfferFor,
                         RateType = Getdata.RateType,
                         SBCommission = Getdata.SBCommission,
                         FreeDays = Getdata.FreeDays,
                         PropertyId = Getdata.PropertyId,
                         OfferType = Getdata.OfferType,
                         StayCategoryId = Getdata.StayCategoryId
                     };
             }
                 return PartialView("~/Areas/Admin/Views/Offer/_Details.cshtml", Setdata);             
         }
         [HttpPost]
         public ActionResult Save(Models.OfferModel Getdata)
         {
             try
             {
                 Models.OffersaveModel Setdata = new Models.OffersaveModel()
                 {
                     SOfferTitle = Getdata.OfferTitle,
                     SOfferId = Getdata.OfferId,
                     SNoOfDays = Getdata.NoOfDays,
                     SStartDate = Getdata.StartDate,
                     SEndDate = Getdata.EndDate,
                     SAccommodationId = Getdata.AccommodationId,
                     SAmount = Getdata.Amount,
                     OfferFor = Getdata.OfferFor,
                     SRateType = Getdata.RateType,
                     SBCommission = Getdata.SBCommission,
                     SFreeDays = Getdata.FreeDays,
                     SPropertyId = Getdata.PropertyId,
                     SOfferType = Getdata.OfferType
                 };
                long id=savedata(Getdata);
                return RedirectToAction("Index", new { OfferId = id });
             }
             catch (Exception ex)
             {
                 Common.LogHandler.HandleError(ex);
                 return Redirect("~/Admin/ErrorPage");//, new { id = Getdata.AccommodationId });                 
             }
         }
         [HttpPost]//search
         public ActionResult GetAccommodationList(Models.OfferModel data)
         {
             if (data.SearchValueForAccommodation == 0) data.SearchValueForAccommodation = data.SearchValue;
             StayBazar.Areas.Admin.Models.OffersaveModel search = new StayBazar.Areas.Admin.Models.OffersaveModel();
             List<CLayer.Accommodation> acc = BLayer.Offers.GetAccommodation((int)CLayer.ObjectStatus.StatusType.Active,data.StayCategoryId,data.SearchString,data.SearchValueForAccommodation,0,25);
             ViewBag.Filter = new Models.OfferModel();
             search.Accommodations = acc;
             Models.OfferModel forPager = new Models.OfferModel()
             {
                 TotalRows = 0,
                 Limit = 25,
                 currentPage = 1
             };
             if (search.Accommodations.Count > 0)
             {
                 search.Accommodation.TotalRows = acc[0].TotalRows;
             }
             ViewBag.Filter = forPager;
             return PartialView("_AccommodationList", search);
         }
         [HttpPost]//search
         public ActionResult GetPropertiesList(Models.OfferModel data)
         {
            // if(data.SearchValueForProperty==0)
             StayBazar.Areas.Admin.Models.OffersaveModel search = new StayBazar.Areas.Admin.Models.OffersaveModel();
             List<CLayer.Accommodation> acc = BLayer.Offers.GetProperties((int)CLayer.ObjectStatus.StatusType.Active, data.SearchString, data.SearchValueForProperty, 0, 25);
             ViewBag.Filter = new Models.OfferModel();
             search.Accommodations = acc;
             Models.OfferModel forPager = new Models.OfferModel()
             {
                 TotalRows = 0,
                 Limit = 25,
                 currentPage = 1
             };
             if (search.Accommodations.Count > 0)
             {
                 search.Accommodation.TotalRows = acc[0].TotalRows;
             }
             ViewBag.Filter = forPager;
             return PartialView("_PropertyList", search);
         }
         [HttpPost]//List PropertyList and Accommodation While Edit
         public ActionResult PropertyListAccommodationListByOfferId(Models.OfferModel data)
         {
             int type = 0;
            if( data.SearchValue>0)
            {
                type = data.SearchValue;
                data.SearchValueForAccommodation = type;
            }
            else
            {
                type = data.SearchValueForAccommodation;
            }
            if (type == 1)
                 {
                     StayBazar.Areas.Admin.Models.OffersaveModel search = new StayBazar.Areas.Admin.Models.OffersaveModel();
                     List<CLayer.Accommodation> acc = BLayer.Offers.PropertyListAccommodationListByOfferId((int)CLayer.ObjectStatus.StatusType.Active, data.OfferId, data.SearchValueForAccommodation, 0, 25);
                     ViewBag.Filter = new Models.OfferModel();
                     search.Accommodations = acc;
                     Models.OfferModel forPager = new Models.OfferModel()
                     {
                         TotalRows = 0,
                         Limit = 25,
                         currentPage = 1
                     };
                     if (search.Accommodations.Count > 0)
                     {
                         search.Accommodation.TotalRows = acc[0].TotalRows;
                     }
                     ViewBag.Filter = forPager;
                     return PartialView("_AccommodationList", search);
                 }
            else 
                 {
                     StayBazar.Areas.Admin.Models.OffersaveModel search = new StayBazar.Areas.Admin.Models.OffersaveModel();
                     List<CLayer.Accommodation> acc = BLayer.Offers.PropertyListAccommodationListByOfferId((int)CLayer.ObjectStatus.StatusType.Active, data.OfferId, data.SearchValueForAccommodation, 0, 25);
                     ViewBag.Filter = new Models.OfferModel();
                     search.Accommodations = acc;
                     Models.OfferModel forPager = new Models.OfferModel()
                     {
                         TotalRows = 0,
                         Limit = 25,
                         currentPage = 1
                     };
                     if (search.Accommodations.Count > 0)
                     {
                         search.Accommodation.TotalRows = acc[0].TotalRows;
                     }
                     ViewBag.Filter = forPager;
                     return PartialView("_PropertyList", search);
                 }
                                                       
         }
         [HttpPost]
         public ActionResult SaveAccommodationProperty(Models.OfferModel data)
         {
             string idss = "";           
             try
             {
                // if (data.SearchString == null) data.SearchString = "";
                 if(data.Ids !="" && data.Ids!=null)
                 {  
                     idss = CSVNumericValidation(data.Ids);
                 }
             else
                 {
                 idss = ""; 
                 }                    
                     CLayer.Offers of = new CLayer.Offers()
                     {
                         OfferTitle = data.SOfferTitle,
                         OfferId = data.SOfferId,
                         NoOfDays = data.SNoOfDays,
                         StartDate = data.SStartDate,
                         EndDate = data.SEndDate,
                         AccommodationId = data.SAccommodationId,
                         Amount = data.SAmount,
                         OfferFor = data.SOfferFor,
                         RateType = data.SRateType,
                         SBCommission = data.SSBCommission,
                         FreeDays = data.SFreeDays,
                         PropertyId = data.SPropertyId,
                         OfferType = data.SOfferType,
                         StayCategoryId = data.SStayCategoryId,
                         Ids = idss
                     };
                     of.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                     long id = BLayer.Offers.Save(of);
                     of.OfferId = id;
                     if (data.Ids != "" && data.Ids != null)
                     {
                         BLayer.Offers.SaveAccommodationProperty(of);
                     }
                     return RedirectToAction("index","OfferList");
                 }            
             catch (Exception ex)
             {
                 Common.LogHandler.HandleError(ex);
                 return Redirect("~/Admin/ErrorPage"); //                
             }
         }
         public ActionResult GetAllOfferList()
          {
              Models.OfferModel search = new Models.OfferModel();
              List<CLayer.Offers> offer = BLayer.Offers.GetAllByStatus(CLayer.ObjectStatus.StatusType.Active, 0, 25);
              search.offerlist = offer;
              search.TotalRows = 0;
              if (search.offerlist.Count > 0)
              {
                  search.TotalRows = offer[0].TotalRows;
              }
              search.Limit = 25;
              search.currentPage = 1;
              ViewBag.Filter = search;
              return PartialView("_List", search);
          }
         public ActionResult Delete(long? OfferId)
         {
             try
             {
                 long Id = OfferId.Value;
                 BLayer.Offers.Delete(Id);
                 return RedirectToAction("Index");
             }
             catch (Exception ex)
             {
                 Common.LogHandler.HandleError(ex);
                 return RedirectToAction("index");//, new { id = Getdata.AccommodationId });                 
             }
         }
         public ActionResult DeleteProperty(long? PropertyId)
         {
             try
             {
                 long OfferId=BLayer.Offers.DeleteProperty(PropertyId.Value);
                 return RedirectToAction("Index", new { OfferId = OfferId });
             }
             catch (Exception ex)
             {
                 Common.LogHandler.HandleError(ex);
                 return RedirectToAction("Index", "OfferList");//, new { id = Getdata.AccommodationId });                 
             }
         }
         public ActionResult DeleteAccommodation(long? AccommodationId)
         {
             try
             {

              long OfferId= BLayer.Offers.DeleteAccommodation(AccommodationId.Value);
              return RedirectToAction("Index", new { OfferId = OfferId });
             //return RedirectToAction("Index", new { OfferId = OfferId });
              }
             catch (Exception ex)
             {
                 Common.LogHandler.HandleError(ex);
                 return RedirectToAction("Index", "OfferList");//, new { id = Getdata.AccommodationId });                 
             }
         }
	}
}