using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Controllers
{
    public class OfflinePaymentProcessController : Controller
    {


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
        [AllowAnonymous]
        private Models.OfflinePaymentProcessModel LoadData(long OfflinePaymentId)
        {
            Models.OfflinePaymentProcessModel pay = new Models.OfflinePaymentProcessModel();
            long userId = GetUserId();

            CLayer.OfflinePayment PaymentData = BLayer.OfflinePayment.GetOfflinePaymentDetails(OfflinePaymentId);

            if (PaymentData.Amount > 0)
            {

                pay.account_id = BLayer.Settings.GetValue(CLayer.Settings.PAY_ACCOUNT_ID);
                pay.reference_no = PaymentData.PaymentRefNo;
                pay.amount = Convert.ToDecimal(PaymentData.Amount).ToString();

                if (System.Configuration.ConfigurationManager.AppSettings.Get("PayMode") == "1")
                {
                    pay.mode = CLayer.Settings.PAYMENT_MODE_TESTING;
                }
                else { pay.mode = CLayer.Settings.PAYMENT_MODE_LIVE; }


                //    pay.mode = CLayer.Settings.PAYMENT_MODE_LIVE;

                pay.return_url = BLayer.Settings.GetValue(CLayer.Settings.OFFLINE_PAY_RETURN_URL);
                pay.description = BLayer.Settings.GetValue(CLayer.Settings.PAY_DESCRIPTION);
                string phone;


                //user details
                pay.name = Common.Utils.CutString((PaymentData.Name).Trim(), CLayer.PaymentGateway.EBS.NAME_LENGTH);
                pay.address = Common.Utils.CutString(PaymentData.Address, CLayer.PaymentGateway.EBS.ADDRESS_LENGTH);
                pay.state = Common.Utils.CutString(PaymentData.State, CLayer.PaymentGateway.EBS.STATE_LENGTH);
                pay.country = PaymentData.CountryCode;
                pay.city = Common.Utils.CutString(PaymentData.City, CLayer.PaymentGateway.EBS.CITY_LENGTH);
                if (pay.city == "") pay.city = "city";
                phone = "000000";
                if (PaymentData.Mobile != "")
                {
                    phone = PaymentData.Mobile;
                }
                if (phone.Length < 10) phone = phone.PadLeft(10, '0');
                pay.phone = Common.Utils.CutString(phone, CLayer.PaymentGateway.EBS.PHONE_LENGTH);
                pay.email = Common.Utils.CutString(PaymentData.Email, CLayer.PaymentGateway.EBS.EMAIL_LENGTH);
                pay.postal_code = Common.Utils.CutString(PaymentData.ZipCode, CLayer.PaymentGateway.EBS.ZIP_LENGTH);
                if (pay.postal_code == "") pay.postal_code = "00000";


                //customer details
                pay.ship_name = Common.Utils.CutString((PaymentData.Name).Trim(), CLayer.PaymentGateway.EBS.SHIP_NAME);
                pay.ship_address = Common.Utils.CutString(PaymentData.Address, CLayer.PaymentGateway.EBS.SHIP_ADDR_LENGTH);
                pay.ship_state = Common.Utils.CutString(PaymentData.State, CLayer.PaymentGateway.EBS.STATE_LENGTH);
                pay.ship_country = PaymentData.CountryCode;
                pay.ship_city = Common.Utils.CutString(PaymentData.City, CLayer.PaymentGateway.EBS.CITY_LENGTH);
                if (pay.ship_city == "") pay.ship_city = "city";
                phone = "000000";
                if (PaymentData.Mobile != "")
                {
                    phone = PaymentData.Mobile;
                }
                if (phone.Length < 10) phone = phone.PadLeft(10, '0');
                pay.ship_phone = Common.Utils.CutString(phone, CLayer.PaymentGateway.EBS.PHONE_LENGTH);
                pay.ship_postal_code = Common.Utils.CutString(PaymentData.ZipCode, CLayer.PaymentGateway.EBS.ZIP_LENGTH);
                if (pay.ship_postal_code == "") pay.ship_postal_code = "00000";
                pay.SecurePayment();
                // data.Items = BLayer.BookingItem.GetAllUnderCart(userId);
            }
            else
            {
                pay.amount = "0";
            }

            return pay;
        }

        #endregion
        [AllowAnonymous]
        public ActionResult Index(long OfflinePaymentId)
        {

            Models.OfflinePaymentProcessModel pay = new Models.OfflinePaymentProcessModel() { amount = "0" };
            try
            {
                pay = LoadData(OfflinePaymentId);
                ViewBag.OfflinePaymentId = OfflinePaymentId;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(pay);
        }
        [AllowAnonymous]
        [HttpPost]
        public string SetCheckout(long OffId)//int payType)
        {
            try
            {
                BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.failed, OffId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
        }
    }
}