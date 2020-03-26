using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StayBazar.Models;

namespace StayBazar.Controllers
{
    public class SupplierListController : Controller
    {
    
        public ActionResult Index()
        {
            try
            {
                List<CLayer.B2B> SupplierList = new List<CLayer.B2B>();
                long userId = 0;
                long.TryParse(User.Identity.GetUserId(),out userId);
                SupplierList = BLayer.B2B.GetAllSupplier(userId);
                return View(SupplierList);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
        
	}
}