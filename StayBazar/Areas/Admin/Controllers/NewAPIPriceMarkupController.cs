using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class NewAPIPriceMarkupController : Controller
    {
        // GET: Admin/NewAPIPriceMarkup
        private const int PAGE_LIMIT = 10;
        // GET: Admin/APIPriceMarkup
        public ActionResult Index()
        {
            Models.APIPriceMarkupModel data = new Models.APIPriceMarkupModel();
            //List<CLayer.APIPriceMarkup> API = BLayer.APIPriceMarkup.GetAPIPriceMarkup("", 0, PAGE_LIMIT);
            List<CLayer.APIPriceMarkup> API = BLayer.APIPriceMarkup.GetNewAPIPriceMarkUp();
            data.API = API;
            data.currentPage = 1;
            data.Limit = PAGE_LIMIT;
            ViewBag.Filter = data;
            data.TotalRows = 0;
            if (API.Count > 0)
            {
                data.TotalRows = API[0].TotalRows;
            }
            return View(data);
        }
        public ActionResult SaveDetails(Models.APIUserModel data)
        {
            try
            {
                CLayer.APIPriceMarkup API = new CLayer.APIPriceMarkup();
                {
                    API.APIPriceMarkupCode = data.APIPriceMarkupCode;
                    API.SellMarkup = data.SellPrice;
                };
                BLayer.APIPriceMarkup.SaveNewAPIMarkup(API);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");

        }
        public ActionResult GetDetails(long id)
        {
            try
            {
                Session["id"] = id;
                Models.APIUserModel cm = new Models.APIUserModel();
                if (id != 0)
                {
                    CLayer.APIPriceMarkup usr = BLayer.APIPriceMarkup.GetNewAPIDetails(id);
                    if (usr != null)
                    {
                        cm.APIPriceMarkupCode = usr.APIPriceMarkupCode;
                        cm.DescriptionId = usr.DescriptionId;
                        cm.SellPrice = usr.SellMarkup;
                    }
                }
                return View("_Details", cm);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                Redirect("~/ErrorPage");
            }
            return View();
        }
        public ActionResult Remove(long id)
        {
            try
            {
                BLayer.APIPriceMarkup.Delete(id);
                //Models.APIPriceMarkupModel mdata = InitialData();
                //return View("_List", mdata);
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                Redirect("~/ErrorPage");
            }
            return RedirectToAction("Index");
        }
        //[Common.AdminRolePermission(AllowAllRoles = true)]
        //[HttpPost]
        //public ActionResult Filter(Models.PaymentListToCustomerModel data)
        //public ActionResult Filter(string SearchString)
        //{
        //    //if (data.SearchString == null) data.SearchString = "";
        //    if (SearchString == null) SearchString = "";
        //    List<CLayer.APIPriceMarkup> API = BLayer.APIPriceMarkup.GetAPIPriceMarkup(SearchString, 0, 10);
        //    ViewBag.Filter = new Models.APIPriceMarkupModel();
        //    Models.APIPriceMarkupModel forPager = new Models.APIPriceMarkupModel()
        //    {

        //        SearchString = SearchString,
        //        API = API,
        //        Limit = 10,
        //        TotalRows = 0,
        //        currentPage = 1
        //    };
        //    if (API.Count > 0)
        //    {
        //        forPager.TotalRows = API[0].TotalRows;
        //    }
        //    ViewBag.Filter = forPager;
        //    return PartialView("_List", API);
        //}
        [Common.AdminRolePermission(AllowAllRoles = true)]
        [HttpPost]
        public ActionResult Pager1(Models.APIPriceMarkupModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.APIPriceMarkup> API = BLayer.APIPriceMarkup.GetAPIPriceMarkup(data.SearchString, (data.currentPage - 1) * data.Limit, data.Limit);
            //List<CLayer.OfflineBooking> users = BLayer.B2B.SearchPaymentCustomerList(data.SearchString, , );
            ViewBag.Filter = new Models.APIPriceMarkupModel();
            Models.APIPriceMarkupModel data1 = new Models.APIPriceMarkupModel();
            data1.API = API;
            Models.APIPriceMarkupModel forPager = new Models.APIPriceMarkupModel()
            {
                API = API,
                Limit = 25,
                TotalRows = 0,
                currentPage = data.currentPage
            };
            if (API.Count > 0)
            {
                forPager.TotalRows = API[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", data1);
        }
    }
}