using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Rotativa;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SupplierPaymentController : Controller
    {
        // GET: Admin/SupplierPayment
        public ActionResult Index()
        {
            Models.SupplierPaymentModel search = new Models.SupplierPaymentModel();
            List<CLayer.SupplierPayment> users = BLayer.SupplierPayment.GetAllSupllierPaymentSearch((int)CLayer.ObjectStatus.SupplierPaymentStatusType.Pending, "", 0, (int)CLayer.Role.Roles.Supplier, 0, 25);
            search.Status = (int)CLayer.ObjectStatus.SupplierPaymentStatusType.All;
            search.SearchString = "";
            search.SearchValue = 0;
            search.userlist = users;
            search.TotalRows = 0;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View(search);

        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public ActionResult Pager1(Models.SupplierPaymentModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            if (data.SearchString == "") data.SearchValue = 0;
            List<CLayer.SupplierPayment> users = BLayer.SupplierPayment.GetAllSupllierPaymentSearch(data.Status, data.SearchString, data.SearchValue, (int)CLayer.Role.Roles.Supplier, (data.currentPage - 1) * data.Limit, data.Limit);

            ViewBag.Filter = new Models.SupplierPaymentModel();

            Models.SupplierPaymentModel forPager = new Models.SupplierPaymentModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                userlist = users,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;

            return PartialView("_List", users);
        }
        [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public ActionResult Filter(Models.SupplierPaymentModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            if (data.SearchString == "") data.SearchValue = 0;
            data.Limit = 25;
            data.currentPage = 1;
            List<CLayer.SupplierPayment> users = BLayer.SupplierPayment.GetAllSupllierPaymentSearch(data.Status, data.SearchString, data.SearchValue, (int)CLayer.Role.Roles.Supplier, (data.currentPage - 1) * data.Limit, data.Limit);


            ViewBag.Filter = new Models.SupplierPaymentModel();
            Models.SupplierPaymentModel forPager = new Models.SupplierPaymentModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                userlist = users,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;

            return PartialView("_List", users);
        }
        public ActionResult SelectOrder(IEnumerable<string> offlineBookingSelected)
        {
            Models.SupplierPaymentModel model = new Models.SupplierPaymentModel();
            model.dtPayment = Convert.ToString(DateTime.Now);

            model.bookinreferno = offlineBookingSelected;

            model.Status = 0;
            if (model.bookinreferno == null)
            {
                RedirectToAction("index");
            }
            return View("~/Areas/Admin/Views/SupplierPayment/PaymentDetails.cshtml", model);
        }

        public long SaveSupplierPayment(Models.SupplierPaymentModel dat)
        {

            CLayer.SupplierPayment sup = new CLayer.SupplierPayment();
            sup.BAnk = dat.Bank;
            sup.supplierPaymentId = dat.supplierPaymentId;
            sup.supplierId = 0;
            sup.SupplierEmail = dat.SupplierEmail;
            sup.OrderId = "";// dat.Supplier.OrderId;
            sup.Amount = 0;// dat.Supplier.Amount;
            //    sup.supplierPayment = dat.Supplier.supplierPayment;
            sup.grossAmount = dat.GrossAmount;
            sup.modeofPayment = dat.ModeofPayment;
            sup.netAmtPaid = dat.NetAmount;
            sup.tdsAmount = dat.TdsAmount;
            DateTime dtPayment = DateTime.MinValue;
            DateTime.TryParse(dat.dtPayment, out dtPayment);
            sup.pdtPayment = dtPayment;
            long count = BLayer.SupplierPayment.SaveSupplierPayment(sup);
            dat.supplierPaymentId = count;
            return count;
        }
        public void SaveSupplierPaymentDetails(long SupplierPaymentId, long SupplierId, string SupplierEmail, string bookingreference, decimal amount, bool supplierPayment, bool BookRefNo1, int status)
        {

            CLayer.SupplierPayment sup = new CLayer.SupplierPayment();
            sup.supplierPaymentId = SupplierPaymentId;
            sup.supplierId = SupplierId;
            sup.SupplierEmail = SupplierEmail;
            sup.OrderId = bookingreference;
            sup.Amount = amount;
            sup.supplierPayment = supplierPayment;
            sup.Status = status;
            long count = BLayer.SupplierPayment.SaveSupplierPaymentDetails(sup);
            Models.SupplierPaymentModel search = new Models.SupplierPaymentModel();
            List<CLayer.SupplierPayment> users = BLayer.SupplierPayment.GetAllSupllierPaymentSearch((int)CLayer.ObjectStatus.StatusType.Active, "", 0, (int)CLayer.Role.Roles.Supplier, 0, 25);
            search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
            search.SearchString = "";
            search.SearchValue = 0;
            search.userlist = users;
            search.TotalRows = 0;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            //if (BookRefNo1)
            //{
            //    return RedirectToAction("ReportPdf", new { SupplierPaymentId = SupplierPaymentId });
            //}
            // return View("~/Areas/Admin/Views/SupplierPayment/Index", search);


        }
        public ActionResult ReportPdf(long SupplierPaymentId)
        {
            Models.SupplierPaymentModel data = new Models.SupplierPaymentModel();
            List<CLayer.SupplierPayment> users = BLayer.SupplierPayment.GetAllSupllierPaymentVoucher(SupplierPaymentId);

            //List<CLayer.PropertyDetailsReport> Reportlist;
            try
            {
                data.supplierPaymentId = SupplierPaymentId;
                data.SupplierList = users;

                //data.ReportList = Reportlist;                       
                data.ForPrint = true;
                data.ForPdf = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return new ViewAsPdf("~/Areas/Admin/Views/SupplierPayment/SupplierPaymentVoucher.cshtml", data);
        }
    }
}