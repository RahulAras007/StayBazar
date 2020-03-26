using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StayBazar.Models;

namespace StayBazar.Controllers
{
    public class PaymentLinkPaymentController : Controller
    {
        public const string PAYMENT_FAILED_LINK = "PaymentError";
        #region Custom methods
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
        //public Models.PaymentRequestModel LoadValPaymentRequest()
        //{
        //    long UserId = GetUserId();
        //    CLayer.Address byAddress = BLayer.Address.GetAddressOnUserId(UserId);
        //    CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(UserId);
        //    PaymentRequestModel model = new PaymentRequestModel();
        //    if (byAddress != null && adrs != null)
        //    {
        //        model.Name = byAddress.Firstname;
        //        model.Email = byAddress.Email;
        //        model.Mobile = adrs.Mobile;
        //        model.CityId = adrs.CityId;
        //        model.City = adrs.City;
        //        model.State = adrs.State;
        //        model.CountryId = adrs.CountryId;
        //        model.ZipCode = adrs.ZipCode;
        //        model.Address = adrs.AddressText;
               
        //    }
        //}
        #endregion

        [AllowAnonymous]
        public ActionResult Index()
        {
            long UserId = GetUserId();
            CLayer.Address byAddress = BLayer.Address.GetAddressOnUserId(UserId);
            CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(UserId);
            PaymentRequestModel model = new PaymentRequestModel();
            if (byAddress != null && adrs != null)
            {
                model.Name = byAddress.Firstname;
                model.Email = byAddress.Email;
                model.Mobile = adrs.Mobile;
                model.CityId = adrs.CityId;
                model.City = adrs.City;
                model.State = adrs.State;
                model.CountryId = adrs.CountryId;
                model.ZipCode = adrs.ZipCode;
                model.Address = adrs.AddressText;
                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        [AllowAnonymous]
        public ActionResult CustomerOfflinePay(Models.PaymentRequestModel data)
        {
            try
            {
                //long UserId = GetUserId();
                long UserId = BLayer.OfflinePayment.GetOfflinePaymentUserID(data.PaymentGuid);
                CLayer.Address byAddress = BLayer.Address.GetAddressOnUserId(UserId);
                CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(UserId);
                CLayer.OfflinePayment dt = new CLayer.OfflinePayment();
                dt.Name = byAddress.Firstname;
                dt.Amount = data.GrandTotal;
                dt.ReferenceNumber = "";
                dt.Message = "";
                dt.UserId = UserId;
                dt.Address = adrs.AddressText;
                dt.CountryId = adrs.CountryId;
                dt.StateId = adrs.State;
                dt.CityId = data.CityId;
                if (adrs.City != null && adrs.City != "")
                {
                    dt.City = adrs.City;
                }
                if (adrs.CityId > 0)
                {
                    dt.City = BLayer.City.Get(adrs.CityId).Name;
                }
                dt.Email = byAddress.Email;
                dt.Mobile = adrs.Mobile;
                dt.ZipCode = adrs.ZipCode;
                dt.CustomerGuid = data.PaymentGuid;
                dt.Gatewaytype = (int)CLayer.ObjectStatus.Gateway.EBS;
                CLayer.Role.Roles rle = BLayer.User.GetRole(UserId);
                long OfflinePaymentId = BLayer.OfflinePayment.SaveInitialCustomerPaymentData(dt);
                string PaymentRefNo = " ";
                long OffPayId = BLayer.OfflinePayment.SetCustomerPaymentRefNo(OfflinePaymentId, rle, PaymentRefNo, data.PaymentGuid);
                return RedirectToAction("Index", "OfflinePaymentProcess", new { OfflinePaymentId = OfflinePaymentId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return RedirectToAction("Index", "Home");
            }

        }
        //public ActionResult PaymentRequest(Guid PaymentGuid)
        //{
        //    try
        //    {
        //        //string lck = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
        //        //if (key != lck)
        //        //{
        //        //    return View(new Models.BookingModel());
        //        //}
        //        return View(LoadValOffPaymentRequest(PaymentGuid));
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //    }
        //    return View(new Models.PaymentLinkPaymentModel());

        //}
        //public Models.PaymentLinkPaymentModel LoadValOffPaymentRequest(Guid PaymentGuid)
        //{
        //    long UserId = GetUserId();
        //    CLayer.Address byAddress = BLayer.Address.GetAddressOnUserId(UserId);
        //    CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(UserId);
        //    Models.PaymentLinkPaymentModel details = null;
        //    //CLayer.OfflineBooking data = BLayer.OfflineBooking.GetOfflineBookingCustomerDetailsByID(CustomerId);
        //    long LoggedInUser = Convert.ToInt64(System.Web.HttpContext.Current.Session["LoggedInUser"]);
        //    List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForPaymentList_DetailsForMail(PaymentGuid);
        //    if (byAddress != null && adrs != null)
        //    {
        //        details = new Models.PaymentLinkPaymentModel()
        //        {
        //            Name = byAddress.Firstname,
        //            Email = byAddress.Email,
        //            Mobile = adrs.Mobile,
        //            CityId = adrs.CityId,
        //            City = adrs.City,
        //            State = adrs.State,
        //            CountryId = adrs.CountryId,
        //            ZipCode = adrs.ZipCode,
        //            Address = adrs.AddressText,
        //            OfflineBookingList = users,
        //            GrandTotal = users.First().SumTotalSalePrice - users.First().AdvanceReceived,
        //            PaymentGuid = PaymentGuid,
        //        };
        //    }
        //    else
        //    {
        //        details = new Models.PaymentLinkPaymentModel()
        //        {
        //            OfflineBookingList = users,
        //            GrandTotal = users.First().SumTotalSalePrice - users.First().AdvanceReceived,
        //            PaymentGuid = PaymentGuid,
        //        };
        //    }
        //    //data.UserId = CustomerId;
        //    return details;
        //}
    }
}