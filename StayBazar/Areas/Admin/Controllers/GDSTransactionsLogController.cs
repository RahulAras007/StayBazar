using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class GDSTransactionsLogController : Controller
    {
        // GET: Admin/GDSTransactionsLog
        public ActionResult Index()
        {
            Models.GDSTransactionLogModel  model = new Models.GDSTransactionLogModel();
            model.GDSTransactionsLog= BLayer.GDSTransactionsLog.GetGDSTransactionLog();
                       
            return View(model);
        }
    }
}