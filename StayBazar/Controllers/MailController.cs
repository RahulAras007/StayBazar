using StayBazar.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class MailController : Controller
    {
        #region CustomMethods

        public Models.BookingModel LoadVal(long bookingId)
        {
            Models.BookingModel data = new Models.BookingModel();
            //    CLayer.Address wh = BLayer.Bookings.GetBookedByUser(bookingId);
            //List<CLayer.Address> adr = BLayer.Bookings.GetOfflineBookedForUser(bookingId);//This is for offlineboookings
            List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);//This is for API booking
            if (adr.Count == 0) adr.Add(new CLayer.Address());
            data.OrderedBy = adr[0];
            // data.Items = BLayer.BookingItem.GetAllDetails(bookingId);
           //long propertyId = BLayer.BookingExternalInventory.GetPropertyIdByOfflineBookingId(bookingId);//*this is for getting property id of offline_booking
         long propertyId = BLayer.BookingExternalInventory.GetPropertyIdByBookingId(bookingId);//this is for getting property id of booking
            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(bookingId));
            ViewBag.InventoryAPIType = InventoryAPIType;
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data.Items = BLayer.BookingItem.GetAllDetails(bookingId, true);
            }
            else
            {
                //data.Items = BLayer.BookingItem.GetAllOfflineDetails(bookingId);
                data.Items = BLayer.BookingItem.GetAllDetails(bookingId);//*This is getting details for booking or API Booking
            }
            //CLayer.Booking bdata = BLayer.Bookings.GetOfflineBookingDetails(bookingId);//*This is for getting orderno and other details of Offline bookings
            CLayer.Booking bdata = BLayer.Bookings.GetDetails(bookingId);//*This is for getting orderno and other details of online bookings or API booking
            data.BookingDetails.OrderNo = bdata.OrderNo;
            data.BookingDetails.BookingDate = bdata.BookingDate;
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                data.Supplier = BLayer.Bookings.GetSupplierDetailsAmadeus(bookingId);
            }
            else
            {
               // data.Supplier = BLayer.Bookings.GetSupplierDetailsofOfflineBooking(bookingId);//This is for getting supplier detials of Offline Booking 
                data.Supplier = BLayer.Bookings.GetSupplierDetails(bookingId);//This is for getting supplier detials of Online Booking or API booking
            }
               
            data.BookingId = bookingId;
           
            //data.LoggedInUserName = BLayer.Bookings.GetCurrentApproverNameForMailOfOfflineBooking(bookingId);//*This is for getting userid of OfflineBooking
            data.LoggedInUserName = BLayer.Bookings.GetCurrentApproverNameForMail(bookingId);//*this is for getting user id of Online booking or APi 

            ViewBag.propertyId = propertyId;

            //Models.SimpleBookingModel Amedusdata = (Models.SimpleBookingModel)TempData["AmedusData"];
            //List<CLayer.Tax> objAmadeusTaxRates = (List<CLayer.Tax>)Session["Amedustaxrates"];
            //ViewBag.Amadeustaxrates = Session["Amedustaxrates"];
            return data;
        }
        public StayBazar.Areas.Admin.Models.OfflineBookingModel LoadValoff(long OfflineBookingId)
        {
            StayBazar.Areas.Admin.Models.OfflineBookingModel data = new StayBazar.Areas.Admin.Models.OfflineBookingModel();
            CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
            data.OfflineBookingId = OfflineBookingId;
            data.CustomerId = Offlinedata.CustomerId;
            data.PropertyId = Offlinedata.PropertyId;
            data.SupplierId = Offlinedata.SupplierId;
            return data;
        }
        public StayBazar.Areas.Admin.Models.OfflineBookingModel LoadValoffGST(long OfflineBookingId, long BookingDetailsId)
        {
            StayBazar.Areas.Admin.Models.OfflineBookingModel data = new StayBazar.Areas.Admin.Models.OfflineBookingModel();
            data.BookingDetailsId = BookingDetailsId;
            data.OfflineBookingId = OfflineBookingId;
            return data;
        }


        public StayBazar.Areas.Admin.Models.OfflineBookingModel LoadOffBookDetails(long OfflineBookingId, long LoginUserid)
        {
            StayBazar.Areas.Admin.Models.OfflineBookingModel data = new StayBazar.Areas.Admin.Models.OfflineBookingModel();
            data.OfflineBookingId = OfflineBookingId;
            data.LoginUserid = LoginUserid;
            return data;
        }
        public StayBazar.Areas.Admin.Models.OfflineBookingModel LoadOffSubBookDetails(long OfflineBookingId, long LoginUserid, long BookingDetailsId)
        {
            StayBazar.Areas.Admin.Models.OfflineBookingModel data = new StayBazar.Areas.Admin.Models.OfflineBookingModel();
            data.OfflineBookingId = OfflineBookingId;
            data.BookingDetailsId = BookingDetailsId;
            data.LoginUserid = LoginUserid;
            return data;
        }


        public StayBazar.Areas.Admin.Models.OfflineBookingModel LoadOffDraftBookDetails(long OfflineBookingId, long CreatedByUserid)
        {
            StayBazar.Areas.Admin.Models.OfflineBookingModel data = new StayBazar.Areas.Admin.Models.OfflineBookingModel();
            data.OfflineBookingId = OfflineBookingId;
            data.LoginUserid = CreatedByUserid;
            return data;
        }

        #endregion
        //
        // GET: /Mail/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NewCode(long id, string key, string code)
        {
            try
            {
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key == lck)
                {
                    return View("PassReset", id);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("PassReset", 0);
        }

        public ActionResult ChangeConfirm(long id, string key)
        {
            try
            {
                //if (!key.HasValue) return View(new Models.BookingModel());

                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return View(new Models.BookingModel());
                }
                return View(LoadVal(id));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public List<CLayer.Tax> GetAmadeusTaxRates(long bookingId,int InventoryAPIType)
        {

            List<CLayer.Tax> taxrates = BLayer.Tax.GetAllTypeTaxRate(bookingId);

            List<CLayer.Rates> AmadeusRates = (List<CLayer.Rates>)Session["AmadeusRates"];
            TempData.Keep("AmadeusRates");
            List<CLayer.Tax> Amadeustaxrates = new List<CLayer.Tax>();
           
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                foreach (var item in AmadeusRates)
                {

                    CLayer.Tax objTax = new CLayer.Tax();

                    if (item.IsCustStateEqualtoBillingEntity)
                    {
                        // objTax.Title = "CGST";
                        objTax.TotalCGSTTaxAmount = Math.Round(item.CGSTTax, 2, MidpointRounding.AwayFromZero);
                        objTax.TotalSGSTTaxAmount = Math.Round(item.SGSTTax, 2, MidpointRounding.AwayFromZero);
                        objTax.CGSTTitle = "CGST";
                        objTax.SGSTTitle = "SGST";
                    }
                    else
                    {
                        objTax.TotalIGSTTaxAmount = Math.Round(item.IGSTTax, 2, MidpointRounding.AwayFromZero);
                        objTax.IGSTTitle = "IGST";
                    }

                    Amadeustaxrates.Add(objTax);
                }

            }
           
            return Amadeustaxrates;
        }
        public ActionResult BConfirmation(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public ActionResult BConfirmationbyCredit(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public ActionResult Bofflinebookconfirm(long bookingId)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }


        public ActionResult OfflineproceedMail(long bookingId)
        {
            try
            {
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }

        public ActionResult BRequest(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }

        public ActionResult SupportBRequest(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }


        public ActionResult SupplierBRequest(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
    
        public ActionResult SupplierBRequestbyCredit(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public ActionResult SupplierBApprovalRequest(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public ActionResult SupplierBConfirmRequest(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public ActionResult SupplierBConfirmResult(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public ActionResult SupplierBbyCredit(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public ActionResult SupplierBRejectionRequest(long bookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());

        }
        public ActionResult SChangeConfirm(long id, string key)
        {
            try
            {
                // if (!key.HasValue) return View(new Models.BookingModel());
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return View(new Models.BookingModel());
                }
                return View("SupModIntimation", LoadVal(id));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("SupModIntimation", new Models.BookingModel());
        }

        public ActionResult SupplierIntimation(long bookingId, string key)
        {
            try
            {
                // if (!key.HasValue) return View(new Models.BookingModel());

#if !DEBUG
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return View(new Models.BookingModel());
                }
#endif
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());
        }
        public ActionResult CorporateIntimation(long bookingId, string key)
        {
            try
            {
                // if (!key.HasValue) return View(new Models.BookingModel());
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return View(new Models.BookingModel());
                }
                return View(LoadVal(bookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());
        }
        public ActionResult B2CRegistration(long id, string key)
        {
            try
            {
                // if (!key.HasValue) return View(new Models.BookingModel());
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return View(0);
                }
                if (id < 1)
                    return View(0);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(id);
        }

        public ActionResult SupImportRegistration(long id, string pass, string key)
        {
            List<object> val = new List<object>();
            try
            {
                val.Add(id);
                val.Add(pass);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(val);
        }

        public ActionResult B2BRegistration(long id, string pass, string key)
        {
            List<object> val = new List<object>();
            try
            {
                // if (!key.HasValue) return View(new Models.BookingModel());
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new List<object>() { 0, "none" });
                //}
                //if (id < 1)
                //    return View(new List<object>() { 0, "none" });

                val.Add(id);
                val.Add(pass);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(val);
        }
        public ActionResult MailSubmit(string Mailadd)
        {
            string msg = "";
            try
            {
                Models.MailDataModel data = new Models.MailDataModel();
                CLayer.Mail m = new CLayer.Mail()
                {
                    Mailaddress = Mailadd
                };
                msg = BLayer.Mail.Savemail(m);
                return null;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return null;
        }
        public ActionResult Guest(long id, string pass, string key)
        {
            long val = 0;
            try
            {
                // if (!key.HasValue) return View(new Models.BookingModel());
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return View(0);
                }
                if (id < 1)
                    return View(0);

                val = id;
                ViewBag.Password = pass;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(val);
        }
        public ActionResult UCancel(long id, string key)
        {
            try
            {
                //// if (!key.HasValue) return View(new Models.BookingModel());
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return View(new Models.BookingModel());
                }
                if (id < 1)
                    return View(new Models.BookingModel());
                return View("Cancellation", LoadVal(id));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Cancellation", new Models.BookingModel());
        }
        public ActionResult SupCancelIntimation(long id, string key)
        {
            try
            {
                // if (!key.HasValue) return View(new Models.BookingModel());
                string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                if (key != lck)
                {
                    return View("SCancellation", new Models.BookingModel());
                }
                return View("SCancellation", LoadVal(id));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("SCancellation", new Models.BookingModel());
        }

        public ActionResult OfflineBookingConfirmation(long OfflineBookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());
                //}


                return View("~/Areas/Admin/Views/OfflineBooking/OfflineBookingCofirmation.cshtml", LoadValoff(OfflineBookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }
        public ActionResult SupplierIntemationOfflineBook(long OfflineBookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());
                //}

                return View("~/Areas/Admin/Views/OfflineBooking/SupplierIntemationOfflineBook.cshtml", LoadValoff(OfflineBookingId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }



        public ActionResult OfflineBookingConfirmationGST(long OfflineBookingId, long BookingDetailsId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());
                //}


                return View("~/Areas/Admin/Views/OfflineBookingGST/OfflineBookingCofirmation.cshtml", LoadValoffGST(OfflineBookingId, BookingDetailsId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }

        public ActionResult OfflineBookingConfirmationBeforeCheckin(long OfflineBookingId, long BookingDetailsId, string key)
        {
            try
            {

                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());
                //}


                return View("~/Areas/Admin/Views/OfflineBookingGST/OfflineBookingConfirmationBeforeCheckin.cshtml", LoadValoffGST(OfflineBookingId, BookingDetailsId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }
        public ActionResult SupplierIntemationOfflineBookGST(long OfflineBookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());
                //}


                return View("~/Areas/Admin/Views/OfflineBookingGST/SupplierIntemationOfflineBook.cshtml", LoadValoffGST(OfflineBookingId, 0));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }
        public ActionResult SupplierInvoiceOfflineBookGST(long OfflineBookingId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());
                //}

                return View("~/Areas/Admin/Views/OfflineBookingGST/SupplierInvoice.cshtml", LoadValoffGST(OfflineBookingId, 0));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }

        public ActionResult OfflineBookingDeleteRequest(long OfflineBookingId, long LoginUserid)
        {
            try
            {
                return View("~/Areas/Admin/Views/OfflineBookingGST/OfflineBookingDeleteRequest.cshtml", LoadOffBookDetails(OfflineBookingId, LoginUserid));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }
        public ActionResult OfflineSubBookingDeleteRequest(long OfflineBookingId, long LoginUserid, long BookingDetailsId)
        {
            try
            {
                return View("~/Areas/Admin/Views/OfflineBookingGST/OfflineSubBookingDeleteRequest.cshtml", LoadOffSubBookDetails(OfflineBookingId, LoginUserid, BookingDetailsId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }

        public ActionResult OfflineBookingSavedDraftRequest(long OfflineBookingId, long CreatedByUserid)
        {
            try
            {
                return View("~/Areas/Admin/Views/OfflineBookingGST/OfflineBookingSavedDraftRequest.cshtml",
                    LoadOffDraftBookDetails(OfflineBookingId, CreatedByUserid));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }
        public ActionResult OfflineBookingSavedDraftReject(long OfflineBookingId, long CreatedByUserid)
        {
            try
            {
                return View("~/Areas/Admin/Views/OfflineBookingGST/OfflineBookingSavedDraftReject.cshtml",
                    LoadOffDraftBookDetails(OfflineBookingId, CreatedByUserid));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }
        public long GetUserId()
        {
            long userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                long.TryParse(User.Identity.GetUserId(), out userId);
            }
            else
                if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] != null)
            {
                userId = (long)Session[CLayer.ObjectStatus.GUEST_ID_SESSION];
            }
            return userId;
        }
        public ActionResult OfflinebookingSupplierPaymentSchedule(long OfflineBookId, string key)
        {
            try
            {
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());
                //}


                OfflineBookingModel objOfflineBookingModel = new OfflineBookingModel();



                var Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookId);
                objOfflineBookingModel.OfflineBookingId = OfflineBookId;
                //var OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookId);

                var OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetailsForPaymentScedule(OfflineBookId);

                var CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookId);

                var SupplierPaymentSchedule = BLayer.OfflineBooking.GetAllSupplierPaymentSchedule(OfflineBookId);

                if (Offlinedata != null)
                {
                    var SalesPerson = BLayer.User.Get(Offlinedata.SalesPersonId);
                    objOfflineBookingModel.SalesPersonName = SalesPerson.FirstName + " " + SalesPerson.LastName;
                }



                //if (Offlinedata.PayeeID>0)
                //{
                //    var payeedetails = BLayer.OfflineBooking.GetPayeeDetails(Offlinedata.PayeeID);

                //    objOfflineBookingModel.PayeeName = payeedetails.PayeeName;
                //    objOfflineBookingModel.AmountPayable = payeedetails.AmountPayable;
                //    objOfflineBookingModel.AccountNumber = payeedetails.AccountNumber; 
                //    objOfflineBookingModel.IFSCcode = payeedetails.IFSCcode;
                //    objOfflineBookingModel.BankName = payeedetails.BankName;
                //    objOfflineBookingModel.BranchName = payeedetails.BranchName;
                //    objOfflineBookingModel.PAN = payeedetails.PAN;
                //}



                if (Offlinedata != null)
                {
                    objOfflineBookingModel.ConfirmationNumber = Offlinedata.ConfirmationNumber;
                    objOfflineBookingModel.GuestName = Offlinedata.GuestName;
                    objOfflineBookingModel.CheckIn = Offlinedata.CheckIn.ToShortDateString();
                    objOfflineBookingModel.CheckOut = Offlinedata.CheckOut.ToShortDateString();
                    objOfflineBookingModel.NoOfPaxAdult = Offlinedata.NoOfPaxAdult;
                    objOfflineBookingModel.NoOfPaxChild = Offlinedata.NoOfPaxChild;
                    objOfflineBookingModel.NoOfRooms = Offlinedata.NoOfRooms;
                    objOfflineBookingModel.NoOfNight = Offlinedata.NoOfNight;
                    objOfflineBookingModel.AvgDailyRateBefreStaxForSalePrice = Offlinedata.AvgDailyRateBefreStaxForSalePrice;
                    objOfflineBookingModel.BuyPriceforotherservicesForSalePrice = Offlinedata.BuyPriceforotherservicesForSalePrice;
                    objOfflineBookingModel.TotalSalePrice = Offlinedata.TotalSalePrice;
                    objOfflineBookingModel.CustomerPaymentMode = Offlinedata.CustomerPaymentModeId;
                }


                //-----------------------------------------------------------------------------------------------------------------------

                objOfflineBookingModel.SupplierPaymentScheduleList = new List<CLayer.SupplierPaymentSchedule>();
                if (SupplierPaymentSchedule != null)
                {
                    foreach (var data in SupplierPaymentSchedule)
                    {
                        CLayer.SupplierPaymentSchedule objPaymentSchedule = new CLayer.SupplierPaymentSchedule();
                        objPaymentSchedule.PaymentscheduleId = data.PaymentscheduleId;
                        objPaymentSchedule.BookingId = data.BookingId;
                        objPaymentSchedule.SupplierPaymentMode = data.SupplierPaymentMode;
                        objPaymentSchedule.SupplierPaymentModeDate = data.SupplierPaymentModeDate;
                        objPaymentSchedule.SupplierPaymentAmount = data.SupplierPaymentAmount;
                        objPaymentSchedule.SupplierCreditDays = data.SupplierCreditDays;

                        objOfflineBookingModel.SupplierPaymentScheduleList.Add(objPaymentSchedule);
                    }
                }



                //-----------------------------------------------------------------------------------------------------------------------

                if (OfflinePropertydata != null)
                {
                    objOfflineBookingModel.AccountNumber = OfflinePropertydata.AccountNumber;
                    objOfflineBookingModel.AccountName = OfflinePropertydata.AccountName;
                    objOfflineBookingModel.IFSCcode = OfflinePropertydata.IFSCcode;
                    objOfflineBookingModel.BankName = OfflinePropertydata.BankName;
                    objOfflineBookingModel.BranchName = OfflinePropertydata.BranchName;
                    objOfflineBookingModel.PAN = OfflinePropertydata.PAN;
                    objOfflineBookingModel.AccountType = OfflinePropertydata.AccountType;
                    objOfflineBookingModel.SupplierName = OfflinePropertydata.SupplierName;
                    objOfflineBookingModel.PropertyName = OfflinePropertydata.PropertyName;
                    objOfflineBookingModel.PropertyCityname = OfflinePropertydata.PropertyCityname;
                    objOfflineBookingModel.TotalBuyPrice = Offlinedata.TotalBuyPrice;
                    objOfflineBookingModel.PropertyEmail = OfflinePropertydata.PropertyEmail;

                }


                //-----------------------------------------------------------------------------------------------------------------------

                if (CustomerDetails != null)
                {
                    objOfflineBookingModel.CustomerName = CustomerDetails.CustomerName;
                    objOfflineBookingModel.CustomerCityname = CustomerDetails.CustomerCityname;
                    objOfflineBookingModel.emailaddress = CustomerDetails.CustomerEmail;
                }


                return View("~/Areas/Admin/Views/OfflineBookingGST/OfflinebookingSupplierPaymentSchedule.cshtml", objOfflineBookingModel);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new StayBazar.Areas.Admin.Models.OfflineBookingModel());

        }
        public ActionResult PaymentRequest(Guid PaymentGuid)
        {
            try
            {
             
                //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                //if (key != lck)
                //{
                //    return View(new Models.BookingModel());
                //}
                return View(LoadValOffPaymentRequest(PaymentGuid));
                
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.PaymentRequestModel());

        }
        public Models.PaymentRequestModel  LoadValOffPaymentRequest(Guid PaymentGuid)
        {
            //long UserId = GetUserId();
            //CLayer.Address byAddress = BLayer.Address.GetAddressOnUserId(UserId);
            //CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(UserId);
            Models.PaymentRequestModel details = null;
            //CLayer.OfflineBooking data = BLayer.OfflineBooking.GetOfflineBookingCustomerDetailsByID(CustomerId);
            long LoggedInUser = Convert.ToInt64(System.Web.HttpContext.Current.Session["LoggedInUser"]);
            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForPaymentList_DetailsForMail(PaymentGuid);
            //if (byAddress != null && adrs != null)
            //{
            //    details = new Models.PaymentRequestModel()
            //    {
            //        Name = byAddress.Firstname,
            //        Email = byAddress.Email,
            //        Mobile = adrs.Mobile,
            //        CityId = adrs.CityId,
            //        City = adrs.City,
            //        State = adrs.State,
            //        CountryId = adrs.CountryId,
            //        ZipCode = adrs.ZipCode,
            //        Address = adrs.AddressText,
            //        OfflineBookingList = users,
            //        GrandTotal = users.First().SumTotalSalePrice - users.First().AdvanceReceived,
            //        PaymentGuid = PaymentGuid,
            //    };
            //}
            //else
            //{
                details = new Models.PaymentRequestModel()
                {

                    OfflineBookingList = users,
                    GrandTotal = users.First().SumTotalSalePrice - users.First().AdvanceReceived,
                    Name = users.First().CustomerName,
                    PaymentGuid = PaymentGuid,
                };
            //}
            //data.UserId = CustomerId;
            return details;
        }
     
    }
}