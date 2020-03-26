using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class SupplierInvoiceDetailsController : Controller
    {
        public const int LIMIT = 50;
        public const int LIMIT_Booking = 150;
        // GET: Admin/SupplierInvoiceDetails
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            model.isFromList = true;
            model.TaxType = (int)CLayer.SupplierInvoice.TaxTypes.ServiceTax;
            return View(model);
        }
        [Common.AdminRolePermission]
        public ActionResult GSTIndex()
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            model.isFromList = true;
            model.TaxType = (int)CLayer.SupplierInvoice.TaxTypes.GST;
            model.EntryDate = DateTime.Today.ToShortDateString();
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/Index.cshtml", model);
        }
        public ActionResult Filter(Models.SuppierInvoiceModel model)
        {
            Models.SuppierInvoiceModel data = new Models.SuppierInvoiceModel();
            try
            {
                model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(model.searchText, model.searchType, 0, LIMIT, model.TaxType,model.Status);
                model.currentPage = 1;
                model.Limit = LIMIT;
                if (model.SupplierInvList != null && model.SupplierInvList.Count() > 0)
                {
                    model.TotalRows = model.SupplierInvList[0].TotalRows;
                }
                model.isFromList = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_List.cshtml", model);
        }


        public ActionResult Pager(Models.SuppierInvoiceModel model)
        {
            Models.SuppierInvoiceModel data = new Models.SuppierInvoiceModel();
            try
            {
                data.searchText = model.searchText1;
                data.searchType = model.searchType1;
                data.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(data.searchText, data.searchType, (model.currentPage - 1) * LIMIT, LIMIT, model.TaxType, model.Status);
                data.currentPage = model.currentPage;
                data.Limit = LIMIT;
                if (data.SupplierInvList != null && data.SupplierInvList.Count() > 0)
                {
                    data.TotalRows = data.SupplierInvList[0].TotalRows;
                }
                data.isFromList = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_List.cshtml", data);
        }

        public ActionResult getInvPropertyBookingList_Filter_Html()
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvPropertyBookingList_Filter.cshtml", model);
        }

        public ActionResult getInvPropertyBookingList(long propID, long SupplierInvoiceID, string PropertyEmailAddresss, string PropertyType, string BookingRefIDs = "", int TaxType = 2)
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            int TotalRows_Booking;
            try
            {
                if (propID > 0)// && SupplierInvoiceID > 0
                {
                    model.BookingList = BLayer.SupplierInvoice.SupplierInvoiceBookingList(propID, SupplierInvoiceID, BookingRefIDs, PropertyEmailAddresss, PropertyType, 0, LIMIT_Booking, TaxType, out TotalRows_Booking);
                    model.currentPage_Booking = 1;
                    model.Limit_Booking = LIMIT_Booking;
                    model.TotalRows_Booking = TotalRows_Booking;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvPropertyBookingList.cshtml", model);
        }

        //[HttpPost]
        public ActionResult getInvPropertyBookingList_Filter(int searchTypeBooking, string searchTextBooking, long propID, long SupplierInvoiceID, string PropertyEmailAddresss, string PropertyType, string BookingRefIDs = "")
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            int TotalRows_Booking;
            try
            {
                if (propID > 0)// && SupplierInvoiceID > 0
                {
                    model.BookingList = BLayer.SupplierInvoice.SupplierInvoiceBookingList(propID, SupplierInvoiceID, BookingRefIDs, PropertyEmailAddresss, PropertyType, 1, LIMIT_Booking,2, out TotalRows_Booking, searchTypeBooking, searchTextBooking);
                    model.currentPage_Booking = 1;
                    model.Limit_Booking = LIMIT_Booking;
                    model.TotalRows_Booking = TotalRows_Booking;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvPropertyBookingList.cshtml", model);
        }

        public ActionResult getInvPropertyBookingList_Filter_Pager(int searchTypeBooking, string searchTextBooking, long propID, long SupplierInvoiceID, int currentPage_Booking, string PropertyEmailAddresss, string PropertyType, string BookingRefIDs = "")
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            int TotalRows_Booking;
            try
            {
                if (propID > 0)// && SupplierInvoiceID > 0
                {
                    model.BookingList = BLayer.SupplierInvoice.SupplierInvoiceBookingList(propID, SupplierInvoiceID, BookingRefIDs, PropertyEmailAddresss, PropertyType, (currentPage_Booking - 1) * LIMIT_Booking, LIMIT_Booking,2, out TotalRows_Booking, searchTypeBooking, searchTextBooking);
                    model.currentPage_Booking = currentPage_Booking;
                    model.Limit_Booking = LIMIT_Booking;
                    model.TotalRows_Booking = TotalRows_Booking;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvPropertyBookingList.cshtml", model);
        }


        public decimal GetAdjustmentAmount(string bookidlist, decimal totalInvoiceValue)
        {
            return BLayer.SupplierInvoice.GetAdjustmentAmount(bookidlist, totalInvoiceValue);
        }

        public ActionResult getInvPropertySavedBookingList(long propID, long SupplierInvoiceID, string PropertyEmailAddresss, string PropertyType)
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {
                if (propID > 0 && SupplierInvoiceID > 0)
                {
                    model.BookingList = BLayer.SupplierInvoice.SupplierInvoiceSavedBookingList(propID, SupplierInvoiceID, "", PropertyEmailAddresss, PropertyType);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvPropertySavedBookingList.cshtml", model);
        }


        public ActionResult getSelectedBookings(string BookingRefNo)
        {
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvSelectedBookings.cshtml", (object)BookingRefNo.ToString());
        }

        public ActionResult deleteSupplierInvoiceSavedBookingListItemTableRow(long propID, string BookingRefNo, string PropertyEmailAddresss, string PropertyType)
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {
                if (propID > 0)
                {
                    model.savedBookingList = BLayer.SupplierInvoice.SupplierInvoiceSavedBookingList(propID, 0, BookingRefNo, PropertyEmailAddresss, PropertyType);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvPropertySavedBookingList.cshtml", model);
        }

        public ActionResult deleteSupplierInvoiceSavedBookingListItem(long propID, string refNum, long supplierInvID, string PropertyEmailAddresss, string PropertyType)
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {
                BLayer.SupplierInvoice.deleteSupplierInvoiceSavedBookingListItem(refNum, supplierInvID);
                if (propID > 0 && supplierInvID > 0)
                {
                    model.savedBookingList = BLayer.SupplierInvoice.SupplierInvoiceSavedBookingList(propID, supplierInvID, "", PropertyEmailAddresss, PropertyType);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvPropertySavedBookingList.cshtml", model);
        }

        public ActionResult Details(long ID = 0, bool isFromList = false)
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            int TotalRows_Booking;
            try
            {
                if (ID > 0)
                {
                    CLayer.SupplierInvoice data = new CLayer.SupplierInvoice();
                    data = BLayer.SupplierInvoice.getGetSupplierInvoicedetails(ID);
                    model.SupplierInvoiceID = data.SupplierInvoiceID;
                    model.PropertyId = data.PropertyId;
                    model.SupplierId = data.SupplierId;
                    model.PropertyName = data.Property_Name;
                    model.InvoiceNumber = data.InvoiceNumber;
                    model.InvoiceDate = data.InvoiceDate == null ? "" : Convert.ToString(data.InvoiceDate.ToString("dd/MM/yyyy"));
                    model.ServiceTaxRegNumber = data.ServiceTaxRegNumber;
                    model.PAN_Number = data.PAN_Number;
                    model.BaseAmount = data.BaseAmount;
                    model.LuxuryTax = data.LuxuryTax;
                    model.ServiceTax = data.ServiceTax;
                    model.TotalInvoiceValue = data.TotalInvoiceValue;
                    model.EntryDate = data.EntryDate == null ? "" : Convert.ToString(data.EntryDate.ToString("dd/MM/yyyy"));
                    model.txtTotalAdjustmentResult = data.txtTotalAdjustmentResult;
                    model.isFromList = isFromList;
                    model.PropertyEmailAddresss = data.PropertyEmailAddresss;
                    model.PropertyType = data.PropertyType;
                    model.TaxType = data.TaxType;
                    model.IsSupInvoicedone = data.IsSupInvoicedone;

                    model.BookingRefIDs = data.BookingRefNumber;
                    model.checkedBookingRefIDs = data.CheckedBookingRefNumber;
                    if (data.PropertyId > 0 && ID > 0)
                    {
                        model.BookingList = BLayer.SupplierInvoice.SupplierInvoiceBookingList(data.PropertyId, ID, "", data.PropertyEmailAddresss, data.PropertyType, 0, LIMIT_Booking,2, out TotalRows_Booking);
                        model.currentPage_Booking = 1;
                        model.Limit_Booking = LIMIT_Booking;
                        model.TotalRows_Booking = TotalRows_Booking;
                        //model.savedBookingList = null;
                        model.savedBookingList = BLayer.SupplierInvoice.SupplierInvoiceSavedBookingList(data.PropertyId, ID, "", data.PropertyEmailAddresss, data.PropertyType);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/Index.cshtml", model);
        }

        public ActionResult saveSelectedBookingList(string BookingRefIDs, long SupplierInvID, long PropertyId, string PropertyEmailAddresss, string PropertyType, string editedBookingRefIDs)
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {
                List<CLayer.OfflineBooking> lis = new List<CLayer.OfflineBooking>();
                if (SupplierInvID > 0)
                {
                    foreach (string num in editedBookingRefIDs.Split(',').ToList<string>())
                    {
                        string dupe_num = num == null ? "" : num;
                        if (dupe_num.Trim() != "")
                        {
                            CLayer.OfflineBooking rs = new CLayer.OfflineBooking();
                            rs.ConfirmationNumber = num;
                            rs.SupplierInvoiceID = SupplierInvID;
                            rs.isOpen = false;
                            rs.SupInvoiceValueBRef = 0;
                            rs.PaidValueBRef = 0;
                            lis.Add(rs);
                        }
                    }
                    if (lis != null)
                    {
                        if (lis.Count() > 0)
                        {
                            BLayer.SupplierInvoice.saveSupplierInvoiceBooking(lis);
                        }
                    }
                }
                model.savedBookingList = BLayer.SupplierInvoice.SupplierInvoiceSavedBookingList(PropertyId, SupplierInvID, BookingRefIDs, PropertyEmailAddresss, PropertyType);

            }
            catch (Exception ex)
            {

            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_InvPropertySavedBookingList.cshtml", model);
        }

        [HttpPost]
        public ActionResult saveSupplierInvoiceDetails(Models.SuppierInvoiceModel model)
        {
            try
            {
                CLayer.SupplierInvoice data = new CLayer.SupplierInvoice();
                data.SupplierInvoiceID = model.SupplierInvoiceID;
                data.PropertyId = model.PropertyId;
                data.SupplierId = model.SupplierId;
                data.InvoiceNumber = model.InvoiceNumber;
                data.InvoiceDate = model.InvoiceDate == null ? DateTime.Now : DateTime.ParseExact(model.InvoiceDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                data.ServiceTaxRegNumber = model.ServiceTaxRegNumber;
                data.PAN_Number = model.PAN_Number;
                data.BaseAmount = model.BaseAmount;
                data.LuxuryTax = model.LuxuryTax;
                data.ServiceTax = model.ServiceTax;
                data.TotalInvoiceValue = model.TotalInvoiceValue;
                data.EntryDate = model.EntryDate == null ? DateTime.Now : DateTime.ParseExact(model.EntryDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                data.txtTotalAdjustmentResult = model.txtTotalAdjustmentResult;
                data.PropertyEmailAddresss = model.PropertyEmailAddresss;
                data.PropertyType = model.PropertyType;
                data.TaxType = model.TaxType;
                data.IsSupInvoicedone = model.IsSupInvoicedone;
                long ID = BLayer.SupplierInvoice.Save(data);
                data.SupplierInvoiceID = ID;
                if (ID > 0)
                {

                    if (model.BookingRefIDsWithValue != null)
                    {
                        if (model.BookingRefIDsWithValue.Length > 0)
                        {
                            List<CLayer.OfflineBooking> lis = new List<CLayer.OfflineBooking>();

                            foreach (string num in model.BookingRefIDsWithValue.Split(',').ToList<string>())
                            {
                                string[] numwtvle = num.Split('#');
                                string BkgNum = numwtvle[0];

                                bool isChecked = false;
                                string dupe_num = BkgNum == null ? "" : BkgNum;
                                if (dupe_num.Trim() != "")
                                {
                                    if (model.checkedBookingRefIDs != null)
                                    {

                                        foreach (string chkNum in model.checkedBookingRefIDs.Split(',').ToList<string>())
                                        {
                                            string checkedNum = chkNum == null ? "" : chkNum;
                                            if (checkedNum != "")
                                            {
                                                if (dupe_num == checkedNum)
                                                {
                                                    isChecked = true;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        isChecked = false;

                                    }

                                    CLayer.OfflineBooking rs = new CLayer.OfflineBooking();
                                    rs.ConfirmationNumber = BkgNum;
                                    rs.SupplierInvoiceID = ID;
                                    rs.isOpen = isChecked;

                                    decimal SupInvoiceValueBRef = 0;
                                    if (numwtvle[1] == null) { SupInvoiceValueBRef = 0; }
                                    else { SupInvoiceValueBRef = Convert.ToDecimal(numwtvle[1]); }

                                    decimal PaidValueBRef = 0;
                                    if (numwtvle[2] == null) { PaidValueBRef = 0; }
                                    else { PaidValueBRef = Convert.ToDecimal(numwtvle[2]); }

                                    rs.SupInvoiceValueBRef = SupInvoiceValueBRef;
                                    rs.PaidValueBRef = PaidValueBRef;
                                    lis.Add(rs);
                                }
                            }
                            if (lis != null)
                            {
                                if (lis.Count() > 0)
                                {
                                    BLayer.SupplierInvoice.saveSupplierInvoiceBooking(lis);
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            if (model.TaxType == (int)CLayer.SupplierInvoice.TaxTypes.ServiceTax)
            {
                if (model.isFromList)
                {
                    return RedirectToAction("getSupplierInvoiceList");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (model.isFromList)
                {
                    return RedirectToAction("getSupplierInvoiceListForgst");
                }
                else
                {
                    return RedirectToAction("GSTIndex");
                }
            }
        }


        [Common.AdminRolePermission]
        public ActionResult getSupplierInvoiceList()
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {
                //model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(model.searchText, model.searchType);
                model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(model.searchText, model.searchType, 0, LIMIT, (int)CLayer.SupplierInvoice.TaxTypes.ServiceTax,model.Status);
                model.currentPage = 1;
                model.Limit = LIMIT;
                model.TaxType = (int)CLayer.SupplierInvoice.TaxTypes.ServiceTax;
                if (model.SupplierInvList != null && model.SupplierInvList.Count() > 0)
                {
                    model.TotalRows = model.SupplierInvList[0].TotalRows;
                }
                model.isFromList = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/SupplierInvoiceList.cshtml", model);
        }



        [Common.AdminRolePermission]
        public ActionResult getSupplierInvoiceListForgst()
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {
                //model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(model.searchText, model.searchType);
                model.Status = 2;
                model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(model.searchText, model.searchType, 0, LIMIT, (int)CLayer.SupplierInvoice.TaxTypes.GST, model.Status);
                model.currentPage = 1;
                model.Limit = LIMIT;
                model.TaxType = (int)CLayer.SupplierInvoice.TaxTypes.GST;
                if (model.SupplierInvList != null && model.SupplierInvList.Count() > 0)
                {
                    model.TotalRows = model.SupplierInvList[0].TotalRows;
                }
                model.isFromList = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/SupplierInvoiceList.cshtml", model);
        }
        public ActionResult DeleteSupplierInvoice(int currentPage, int TotalRows, long ID = 0)
        {
            Models.SuppierInvoiceModel model = new Models.SuppierInvoiceModel();
            try
            {
                if (ID > 0)
                    BLayer.SupplierInvoice.DeleteSupplierInvoice(ID);
                //model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(model.searchText, model.searchType);
                if (currentPage > 0)
                {
                    model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(model.searchText, model.searchType, (currentPage - 1) * LIMIT, LIMIT, (int)CLayer.SupplierInvoice.TaxTypes.GST, model.Status);
                    model.currentPage = currentPage;
                    model.Limit = LIMIT;
                }
                else
                {
                    model.SupplierInvList = BLayer.SupplierInvoice.getSupplierInvoiceList(model.searchText, model.searchType, 0, LIMIT, (int)CLayer.SupplierInvoice.TaxTypes.GST, model.Status);
                    model.currentPage = 1;
                    model.Limit = LIMIT;
                }
                if (model.SupplierInvList != null && model.SupplierInvList.Count() > 0)
                {
                    model.TotalRows = model.SupplierInvList[0].TotalRows;
                }
                model.isFromList = true;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/SupplierInvoiceDetails/_List.cshtml", model);
            //return RedirectToAction("getSupplierInvoiceList");
        }




    }
}