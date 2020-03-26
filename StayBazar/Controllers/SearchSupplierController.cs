using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StayBazar.Models;

namespace StayBazar.Controllers
{
    public class SearchSupplierController : Controller
    {
        public const int PAGELIMIT = 25;
        public ActionResult Index(SearchSupplierModel model)
        {

            try
            {
                if (model.Name == null) model.Name = "";
                SearchSupplierModel data = new SearchSupplierModel();
                List<CLayer.B2B> SupplierList = new List<CLayer.B2B>();
                string name = model.Name;
                data.SearchString = model.Name;
                data.CurrentPage = 1;
                int totr = 0;
                SupplierList = BLayer.B2B.GetSearchAllSupplier(name, 0, PAGELIMIT, out totr);
                data.SearchList = SupplierList;
                data.TotalRows = totr;
                return View("Index", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }


        }
        public ActionResult GetSupplierList(SearchSupplierModel model)
        {
            try
            {
                if (model.Name == null) model.Name = "";
                SearchSupplierModel data = new SearchSupplierModel();
                List<CLayer.B2B> SupplierList = new List<CLayer.B2B>();
                string name = model.Name;
                data.SearchString = model.Name;
                data.CurrentPage = 1;
                int totr = 0;
                SupplierList = BLayer.B2B.GetSearchAllSupplier(name, 0, PAGELIMIT, out totr);
                data.SearchList = SupplierList;
                data.TotalRows = totr;
                return View("Index", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
        public ActionResult Pager(SearchSupplierModel model)
        {
            try
            {
                if (model.SearchString == null) model.SearchString = "";
                SearchSupplierModel data = new SearchSupplierModel();
                List<CLayer.B2B> SupplierList = new List<CLayer.B2B>();
                string name = model.SearchString;
                data.SearchString = model.SearchString;
                data.CurrentPage = model.CurrentPage;
                int totr = 0;
                SupplierList = BLayer.B2B.GetSearchAllSupplier(name, ((model.CurrentPage - 1) * PAGELIMIT), PAGELIMIT, out totr);
                data.TotalRows = totr;
                data.SearchList = SupplierList;
                return View("Details", data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
        public ActionResult AddSupplier(long UserId)
        {

            long B2bId = 0;
            long.TryParse(User.Identity.GetUserId(), out B2bId);
            CLayer.B2B data = new CLayer.B2B();
            data.B2BId = B2bId;
            data.UserId = UserId;
            data.UserType = (int)CLayer.Role.Roles.Supplier;
            BLayer.B2B.SaveSupplierForAffiliate(data);
            return RedirectToAction("Index", "SupplierList");

        }
    }
}