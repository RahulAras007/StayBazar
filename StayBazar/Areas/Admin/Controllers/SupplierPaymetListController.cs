using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    
    public class SupplierPaymetListController : Controller
    {
        public const int PAGE_LIMIT = 50;

        // GET: Admin/SupplierPaymetList
        public ActionResult Index()
        {
            Models.SupplierPaymetListModel model = new Models.SupplierPaymetListModel();
            model.searchText = "";
            model.searchType = 1;
            model.Limit = PAGE_LIMIT;
            model.currentPage = 1;
            List<CLayer.SupplierPaymetList> SupPayList = BLayer.SupplierPaymetList.getPaymentList(model.searchText, (model.currentPage - 1) * model.Limit, model.Limit, model.searchType);
            try
            {
                // model.SupPayList = BLayer.SupplierPaymetList.getPaymentList(model.searchText, model.searchType);


                if (SupPayList.Count > 0)
                {
                    model.TotalRows = SupPayList[0].TotalRows;
                }
                else
                    model.TotalRows = 0;
            }
            catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            model.SupPayList = SupPayList;
            ViewBag.Filter = model;
            return View(model);
        }

        public long SaveSupplierPayment(Models.SupplierPaymentModel dat)
        {

            CLayer.SupplierPayment sup = new CLayer.SupplierPayment();
            sup.BAnk = dat.Bank;
            //  sup.supplierPaymentId=dat.
            sup.supplierId = 0;
            sup.SupplierEmail = "email";
            sup.OrderId = "";// dat.Supplier.OrderId;
            sup.Amount = 0;// dat.Supplier.Amount;
            //    sup.supplierPayment = dat.Supplier.supplierPayment;
            sup.pdtPayment = System.DateTime.Now;
            sup.grossAmount = dat.GrossAmount;
            sup.modeofPayment = dat.ModeofPayment;
            sup.netAmtPaid = dat.NetAmount;
            sup.tdsAmount = dat.TdsAmount;
            long count = BLayer.SupplierPayment.SaveSupplierPayment(sup);
            dat.supplierPaymentId = count;
            return count;
        }
        public void SaveSupplierPaymentDetails(long SupplierPaymentId, long SupplierId, string SupplierEmail, string bookingreference, decimal amount, bool supplierPayment, bool BookRefNo1)
        {

            CLayer.SupplierPayment sup = new CLayer.SupplierPayment();
            sup.supplierPaymentId = SupplierPaymentId;
            sup.supplierId = SupplierId;
            sup.SupplierEmail = SupplierEmail;
            sup.OrderId = bookingreference;
            sup.Amount = amount;
            sup.supplierPayment = supplierPayment;
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
        public ActionResult Filter(Models.SupplierPaymetListModel model)
        {
            Models.SupplierPaymetListModel data = new Models.SupplierPaymetListModel();
            try
            {

                model.currentPage = 1;
                model.Limit = PAGE_LIMIT;
                //data.SupPayList = BLayer.SupplierPaymetList.getPaymentList(model.searchText, model.searchType);
                model.SupPayList = BLayer.SupplierPaymetList.getPaymentList(model.searchText,0 , PAGE_LIMIT, model.searchType);
                if (model.SupPayList.Count > 0)
                {
                    model.TotalRows = model.SupPayList[0].TotalRows;
                }
                else
                    model.TotalRows = 0;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierPaymetList/_List.cshtml", model);
        }

        public ActionResult Delete(Models.SupplierPaymetListModel model,long supplierPaymentId)
        {
            Models.SupplierPaymetListModel data = new Models.SupplierPaymetListModel();
            try
            {
                BLayer.SupplierPaymetList.DeleteSupPayment(supplierPaymentId);

                model.currentPage = 1;
                model.Limit = PAGE_LIMIT;
                model.SupPayList = BLayer.SupplierPaymetList.getPaymentList(model.searchText, 0, PAGE_LIMIT, model.searchType);
                if (model.SupPayList.Count > 0)
                {
                    model.TotalRows = model.SupPayList[0].TotalRows;
                }
                else
                    model.TotalRows = 0;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierPaymetList/_List.cshtml", model);
        }
        public ActionResult getDetail(long supplierPaymentId)
        {
            Models.SupplierPaymentModel model = new Models.SupplierPaymentModel();
            try
            {
                List<CLayer.SupplierPayment> data = BLayer.SupplierPayment.GetAllSupllierPaymentVoucher(supplierPaymentId);
                model.supplierPaymentId = supplierPaymentId;
                model.SupplierList = data;
                List<string> stringList = new List<string>();
                foreach (var ss in model.SupplierList)
                {
                    stringList.Add(ss.OrderId);
                    model.SupplierEmail = ss.SupplierEmail;
                    model.TdsAmount = ss.tdsAmount;
                    model.NetAmount = ss.netAmtPaid;
                    model.Bank = ss.BAnk;
                    model.ModeofPayment = ss.modeofPayment;
                    model.GrossAmount = ss.grossAmount;
                    model.pdtPayment = ss.pdtPayment; 

                }
                model.Status = 1;
                model.bookinreferno = stringList;
             //   List<CLayer.SupplierPaymetList> users = BLayer.SupplierPaymetList.getPaymentList(model.searchText, model.searchType);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierPaymetlist/SupplierPaymetlistDetails.cshtml", model);
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
            // return new ViewAsPdf("~/Areas/Admin/Views/SupplierPayment/SupplierPaymentVoucher.cshtml", data);
            var PDFResult = new ViewAsPdf("~/Areas/Admin/Views/SupplierPayment/SupplierPaymentVoucher.cshtml",data)
            {
                FileName = "Voucher.PDF"
           
            };
            return PDFResult;
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        [HttpPost]
        public ActionResult Pager1(string srString, int srType, int currentPage)
        {
            Models.SupplierPaymetListModel model = new Models.SupplierPaymetListModel();
            if (srString == null) srString = "";
            if (srType == 0) srType = 1;
            model.searchText = srString;
            model.searchType = srType;
            model.currentPage = currentPage;
            Models.SupplierPaymetListModel data = new Models.SupplierPaymetListModel();
            //List<CLayer.SupplierPaymetList> SpPayList = BLayer.SupplierPaymetList.getPaymentList(model.searchText, model.searchType);
            List<CLayer.SupplierPaymetList> SupPayList = BLayer.SupplierPaymetList.getPaymentList(model.searchText, (model.currentPage - 1) * PAGE_LIMIT, PAGE_LIMIT, model.searchType);
            ViewBag.Filter = new Models.SupplierPaymentModel();

            Models.SupplierPaymetListModel forPager = new Models.SupplierPaymetListModel()
            {
                searchText = model.searchText,
                searchType = model.searchType,
                Limit = model.Limit,
                currentPage = model.currentPage,
                SupPayList = SupPayList

            };
            if (forPager.SupPayList.Count > 0)
            {
                forPager.TotalRows = forPager.SupPayList[0].TotalRows;
            }
            else
                forPager.TotalRows = 0;

            return PartialView("_List",forPager);
        }
    }
}