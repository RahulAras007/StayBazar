using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    
    public class SettingsController : Controller
    {
        #region Custom Methods

        public Models.SettingsModel LoadValues()
        {
            Models.SettingsModel st = new Models.SettingsModel();
            int i;
            //Email Settings
            st.QueryEmail = BLayer.Settings.GetValue(CLayer.Settings.QUERY_MAILID);
            st.CustomerQueryCCMails = BLayer.Settings.GetValue(CLayer.Settings.CUSTOMERQUERYCCMAILS);
            st.QueryPassword = BLayer.Settings.GetValue(CLayer.Settings.QUERY_EMAIL_PASSWORD);
            st.MailServer = BLayer.Settings.GetValue(CLayer.Settings.SERVER);
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.MAIL_PORT), out i);
            st.Port = i;
            st.ReservationEmail = BLayer.Settings.GetValue(CLayer.Settings.REGIS_EMAIL);
            st.ReservationPassword = BLayer.Settings.GetValue(CLayer.Settings.REGIS_EMAIL_PASSWORD);
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.IS_SECURE), out i);
            st.MailSecurity = i;
            st.SupportEmail = BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL);
            st.SupportEmailPassword = BLayer.Settings.GetValue(CLayer.Settings.SUPPORT_EMAIL_PASSWORD);
            // signature image
            st.SignatureImage = BLayer.Settings.GetValue(CLayer.Settings.SIGNATURE_IMAGE);
            //paypal
            st.paypalcommission = Convert.ToDouble(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_COMMISSION));
            st.Paypal_CancelUrl = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_CANCELURL));
            st.Paypal_Confirmation_URL = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_CONFIRM_PAY_URL));
            st.Paypal_Pwd = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_PWD));
            st.Paypal_ReturnUrl = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_RETURNURL));
            st.Paypal_Signature = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_SIGNAUTRE));
            st.Paypal_TokenUrl = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_TOKEN_URL));
            st.Paypal_User = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_USER));
            st.PaypalRequestURL = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_REQUEST_URL));


            //maximojo
            st.MaximojoBookingAvailability = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGAVAILIBILITY);
            st.MaximojoBookingCancel = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGCANCEL));
            st.MaximojoBookingSubmit = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGSUBMIT));
            st.MaximojoBookingVerify = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGVERIFY));
            st.MaximojoHotelAvailability = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOHOTELAVAILIBILITY);
            st.MaximojoPassword = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPASSWORD));
            st.MaximojoUserName = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOUSERNAME));

            st.MaximojoPartnerCode = Convert.ToString(BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPARTNERCODE));


            //chat set
            st.Chat = Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.CHAT));
            st.Liveservechat = BLayer.Settings.GetValue(CLayer.Settings.LIVSERV);
            st.Zopimchat = BLayer.Settings.GetValue(CLayer.Settings.ZOPIM);

            //add Cc For Customercommunication
            st.CCCustomercommunications = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
            //add Cc For Suppliercommunications
            st.CCSuppliercommunications = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);

            //Gateway settings
            st.RefundLink = BLayer.Settings.GetValue(CLayer.Settings.PAYMENT_REFUND_LINK);
            st.GatewayAccountId = BLayer.Settings.GetValue(CLayer.Settings.PAY_ACCOUNT_ID);
            st.GatewayDescription = BLayer.Settings.GetValue(CLayer.Settings.PAY_DESCRIPTION);
            st.GatewayReturnURL = BLayer.Settings.GetValue(CLayer.Settings.PAY_RETURN_URL);
            st.GatewaySecretKey = BLayer.Settings.GetValue(CLayer.Settings.PAY_SECRET_KEY);
            st.B2CPartialPaymentsPctge = BLayer.Settings.GetValue(CLayer.Settings.PARTIAL_PAYMENT_PERCENTAGE_B2C);
            st.B2BPartialPaymentsPctge = BLayer.Settings.GetValue(CLayer.Settings.PARTIAL_PAYMENT_PERCENTAGE_B2B);


            st.PartialPaymentFirstReminder = BLayer.Settings.GetValue(CLayer.Settings.PARTIAL_PAYMENT_FIRST_INSTALLMENT);
            st.PartialPaymentSecondReminder = BLayer.Settings.GetValue(CLayer.Settings.PARTIAL_PAYMENT_SECOND_INSTALLMENT);
            st.OfflinePaymentGatewayReturnURL = BLayer.Settings.GetValue(CLayer.Settings.OFFLINE_PAY_RETURN_URL);
            st.OfflinePaymentPaypalGatewayReturnURL = BLayer.Settings.GetValue(CLayer.Settings.OFFLINE_PAYPAL_RETURN_URL);
            //3rd party
            st.GoogleAPIKey = BLayer.Settings.GetValue(CLayer.Settings.GOOGLE_API_KEY);
            st.GoogleLocationLink = BLayer.Settings.GetValue(CLayer.Settings.GOOGLE_API_QUERY);
            st.SMSAccountId = BLayer.Settings.GetValue(CLayer.Settings.SMS_SENDER_ID);
            st.SMSLink = BLayer.Settings.GetValue(CLayer.Settings.SMS_URL);
            st.SMSAPI = BLayer.Settings.GetValue(CLayer.Settings.SMS_API);
            st.SMSUserName = BLayer.Settings.GetValue(CLayer.Settings.SMS_USERNAME);
            st.SMSPassword = BLayer.Settings.GetValue(CLayer.Settings.SMS_PASSWORD);

            //For b2b corporate booking credit
            st.CreditAmount = BLayer.Settings.GetValue(CLayer.Settings.BOOKING_CREDIT_AMOUNT_B2BCORPORATE);
            st.CreditDays = BLayer.Settings.GetValue(CLayer.Settings.BOOKING_CREDIT_DAYS_B2BCORPORATE);

            st.CorporateSmedybookrxp = BLayer.Settings.GetValue(CLayer.Settings.CORPORATE_SAMEDAY_BOOKING_EXP);

            st.bank = BLayer.Settings.GetValue(CLayer.Settings.Bank);
            st.Grossmarginperc = BLayer.Settings.GetValue(CLayer.Settings.Grossmarginperc);
            st.Default_Password = BLayer.Settings.GetValue(CLayer.Settings.DEFAULT_PASSWORD);

            st.AccountMail = BLayer.Settings.GetValue(CLayer.Settings.ACCOUNT_EMAILS);
            st.SupplierInvoicerequestMails = BLayer.Settings.GetValue(CLayer.Settings.SUPPLIER_INVOICE_REQUEST_MAILS);
            st.Bookingdeletealertmails = BLayer.Settings.GetValue(CLayer.Settings.BOOK_DELETE_ALERT_EMAILS);
            st.SupplierPaymentScheduleEmail = BLayer.Settings.GetValue(CLayer.Settings.SUPPLIERPAYMENTSCHEDULEEMAIL);         

            st.BookingDraftalertmails = BLayer.Settings.GetValue(CLayer.Settings.Booking_Draft_alert_mails);
            st.Bookingrejectedalertmails = BLayer.Settings.GetValue(CLayer.Settings.Booking_rejected_alert_mails);

            //markups
            double d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.B2B_MARKUP_LONG_TERM), out d);
            st.B2BMarkupLongTerm = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.B2B_MARKUP_SHORT_TERM), out d);
            st.B2BMarkupShortTerm = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.B2C_MARKUP_LONG_TERM), out d);
            st.B2CMarkupLongTerm = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.B2C_MARKUP_SHORT_TERM), out d);
            st.B2CMarkupShortTerm = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.B2B_STD_LONG_TERM_DIS), out d);
            st.B2BStdLongTerm = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.B2B_STD_SHORT_TERM_DIS), out d);
            st.B2BStdShortTerm = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.CANCELLATION_PERIOD), out d);
            st.CancellationPeriod = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.CANCELLATION_CHARGES), out d);
            st.CancellationCharges = d;

            //Gateway charges
            d = 0;
            string s = BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_AMEX);
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_AMEX), out d);
            st.GTAmex = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_CASHCARD), out d);
            st.GTCashCard = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_CREDITCARD), out d);
            st.GTCredit = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_DEBITCARD), out d);
            st.GTDebit = d;
            d = 0;
            double.TryParse(BLayer.Settings.GetValue(CLayer.Settings.TRANSACTION_CHARGE_NETBANKING), out d);
            st.GTNetbanking = d;
            //widgets
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.WIDGET_DEALSOFTHEDAYPROFILE_NO), out i);
            st.WDeals = i;
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.WIDGET_MOST_POPULAR_NO), out i);
            st.WMostPopular = i;
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.WIDGET_NEW_PROPERTIES_NO), out i);
            st.WNewProperties = i;
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.WIDGET_SPECIAL_OFFERS), out i);
            st.WSpecial = i;
            //Others
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.CORP_MAX_USERS), out i);
            st.MaximumCorpUsers = i;
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.GALLERY_MAX_FILES), out i);
            st.GalleryMaxFiles = i;
            st.PageLock = BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
            i = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.LOCKIN_TIME), out i);
            st.BookingLockTime = i;
            st.ContactNumber = BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO);



            st.InvDueDays = BLayer.Settings.GetValue(CLayer.Settings.INVOICE_DUE_DAYS);

            //GST Tax rates
            decimal dc;
            dc = 0;
            decimal.TryParse(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE), out dc);
            st.CGSTTaxRate = dc;

            decimal ds;
            ds = 0;
            decimal.TryParse(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE), out ds);
            st.SGSTTaxRate = ds;

            decimal di;
            di = 0;
            decimal.TryParse(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE), out di);
            st.IGSTTaxRate = di;

            //GST Tax rates end

            return st;
        }
        
        #endregion
        //
        // GET: /Admin/Settings/
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            try
            {
                return View(LoadValues());

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.SettingsModel());
        }

        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SavePaypal(Models.SettingsModel data)
        {
            try
            {
                if (data.paypalcommission > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_COMMISSION, data.paypalcommission.ToString());
                }

                if (data.Paypal_CancelUrl != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_CANCELURL, data.Paypal_CancelUrl.ToString());
                }

                if (data.Paypal_Confirmation_URL != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_CONFIRM_PAY_URL, data.Paypal_Confirmation_URL.ToString());
                }

                if (data.Paypal_Pwd != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_PWD, data.Paypal_Pwd.ToString());
                }

                if (data.Paypal_User != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_USER, data.Paypal_User.ToString());
                }

                if (data.Paypal_ReturnUrl != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_RETURNURL, data.Paypal_ReturnUrl.ToString());
                }

                if (data.Paypal_Signature != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_SIGNAUTRE, data.Paypal_Signature.ToString());
                }

                if (data.Paypal_TokenUrl != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_TOKEN_URL, data.Paypal_TokenUrl.ToString());
                }

                if (data.PaypalRequestURL != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.PAYPAL_REQUEST_URL, data.PaypalRequestURL.ToString());
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
       [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveMaximojo(Models.SettingsModel data)
        {
            try
            {
                if (data.MaximojoBookingAvailability != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAXIMOJOBOOKINGAVAILIBILITY, data.MaximojoBookingAvailability.ToString());
                }

                if (data.MaximojoBookingCancel != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAXIMOJOBOOKINGCANCEL, data.MaximojoBookingCancel.ToString());
                }

                if (data.MaximojoBookingSubmit != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAXIMOJOBOOKINGSUBMIT, data.MaximojoBookingSubmit.ToString());
                }

                if (data.MaximojoBookingVerify != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAXIMOJOBOOKINGVERIFY, data.MaximojoBookingVerify.ToString());
                }

                if (data.MaximojoHotelAvailability != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAXIMOJOHOTELAVAILIBILITY, data.MaximojoHotelAvailability.ToString());
                }

                if (data.MaximojoPassword != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAXIMOJOPASSWORD, data.MaximojoPassword.ToString());
                }

                if (data.MaximojoUserName != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAXIMOJOUSERNAME, data.MaximojoUserName.ToString());
                }
                if (data.MaximojoPartnerCode != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAXIMOJOPARTNERCODE, data.MaximojoPartnerCode.ToString());
                }

             
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveChat(Models.SettingsModel data)
        {
            try
            {
                if (data.Chat > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.CHAT, data.Chat.ToString());
                }
                if (data.Liveservechat != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.LIVSERV, data.Liveservechat.ToString());
                }
                if (data.Zopimchat != null)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.ZOPIM, data.Zopimchat.ToString());
                }     
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult MailSave(Models.SettingsModel data)
        {
            try
            {

                if (data.QueryEmail != null)
                {
                    data.QueryEmail = data.QueryEmail.Trim();
                    if (data.QueryEmail != "") BLayer.Settings.SetValue(CLayer.Settings.QUERY_MAILID, data.QueryEmail);
                }
                if (data.QueryPassword != null)
                {
                    data.QueryPassword = data.QueryPassword.Trim();
                    if (data.QueryPassword != "") BLayer.Settings.SetValue(CLayer.Settings.QUERY_EMAIL_PASSWORD, data.QueryPassword);
                }
                if (data.MailServer != null)
                {
                    data.MailServer = data.MailServer.Trim();
                    if (data.MailServer != "") BLayer.Settings.SetValue(CLayer.Settings.SERVER, data.MailServer);
                }
                if (data.Port > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.MAIL_PORT, data.Port.ToString());
                }
                if (data.ReservationEmail != null)
                {
                    data.ReservationEmail = data.ReservationEmail.Trim();
                    if (data.ReservationEmail != "") BLayer.Settings.SetValue(CLayer.Settings.REGIS_EMAIL, data.ReservationEmail);
                }
                if (data.ReservationPassword != null)
                {
                    data.ReservationPassword = data.ReservationPassword.Trim();
                    if (data.ReservationPassword != "") BLayer.Settings.SetValue(CLayer.Settings.REGIS_EMAIL_PASSWORD, data.ReservationPassword);
                }
                if (data.SupportEmail != null)
                {
                    data.SupportEmail = data.SupportEmail.Trim();
                    if (data.SupportEmail != "") BLayer.Settings.SetValue(CLayer.Settings.SUPPORT_EMAIL, data.SupportEmail);
                }
                if (data.SupportEmailPassword != null)
                {
                    data.SupportEmailPassword = data.SupportEmailPassword.Trim();
                    if (data.SupportEmailPassword != "") BLayer.Settings.SetValue(CLayer.Settings.SUPPORT_EMAIL_PASSWORD, data.SupportEmailPassword);
                }
                if (data.MailSecurity > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.IS_SECURE, data.MailSecurity.ToString());
                }

                if (data.CCCustomercommunications != null)
                {
                    data.CCCustomercommunications = data.CCCustomercommunications.Trim();
                    if (data.CCCustomercommunications != "") BLayer.Settings.SetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION, data.CCCustomercommunications);
                }

                if (data.CCSuppliercommunications != null)
                {
                    data.CCSuppliercommunications = data.CCSuppliercommunications.Trim();
                    if (data.CCSuppliercommunications != "") BLayer.Settings.SetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION, data.CCSuppliercommunications);
                }

                if (data.AccountMail != null)
                {
                    data.AccountMail = data.AccountMail.Trim();
                    if (data.AccountMail != "") BLayer.Settings.SetValue(CLayer.Settings.ACCOUNT_EMAILS, data.AccountMail);
                }


                if (data.SupplierInvoicerequestMails != null)
                {
                    data.SupplierInvoicerequestMails = data.SupplierInvoicerequestMails.Trim();
                    if (data.SupplierInvoicerequestMails != "") BLayer.Settings.SetValue(CLayer.Settings.SUPPLIER_INVOICE_REQUEST_MAILS, data.SupplierInvoicerequestMails);
                }

                if (data.Bookingdeletealertmails != null)
                {
                    data.Bookingdeletealertmails = data.Bookingdeletealertmails.Trim();
                    if (data.Bookingdeletealertmails != "") BLayer.Settings.SetValue(CLayer.Settings.BOOK_DELETE_ALERT_EMAILS, data.Bookingdeletealertmails);
                }


                if (data.BookingDraftalertmails != null)
                {
                    data.BookingDraftalertmails = data.BookingDraftalertmails.Trim();
                    if (data.BookingDraftalertmails != "") BLayer.Settings.SetValue(CLayer.Settings.Booking_Draft_alert_mails, data.BookingDraftalertmails);
                }

                if (data.Bookingrejectedalertmails != null)
                {
                    data.Bookingrejectedalertmails = data.Bookingrejectedalertmails.Trim();
                    if (data.Bookingrejectedalertmails != "") BLayer.Settings.SetValue(CLayer.Settings.Booking_rejected_alert_mails, data.Bookingrejectedalertmails);
                }


                if (data.SupplierPaymentScheduleEmail != null)
                {
                    data.SupplierPaymentScheduleEmail = data.SupplierPaymentScheduleEmail.Trim();
                    if (data.SupplierPaymentScheduleEmail != "") BLayer.Settings.SetValue(CLayer.Settings.SUPPLIERPAYMENTSCHEDULEEMAIL, data.SupplierPaymentScheduleEmail);
                }
                if (data.CustomerQueryCCMails != null)
                {
                    data.CustomerQueryCCMails = data.CustomerQueryCCMails.Trim();
                    if (data.CustomerQueryCCMails != "") BLayer.Settings.SetValue(CLayer.Settings.CUSTOMERQUERYCCMAILS, data.CustomerQueryCCMails);
                }

                

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveGateway(Models.SettingsModel data)
        {
            try
            {
                if (data.RefundLink != null)
                {
                    data.RefundLink = data.RefundLink.Trim();
                    if (data.RefundLink != "") BLayer.Settings.SetValue(CLayer.Settings.PAYMENT_REFUND_LINK, data.RefundLink);
                }
                if (data.GatewayAccountId != null)
                {
                    data.GatewayAccountId = data.GatewayAccountId.Trim();
                    if (data.GatewayAccountId != "") BLayer.Settings.SetValue(CLayer.Settings.PAY_ACCOUNT_ID, data.GatewayAccountId);
                }
                if (data.GatewayDescription != null)
                {
                    data.GatewayDescription = data.GatewayDescription.Trim();
                    if (data.GatewayDescription != "") BLayer.Settings.SetValue(CLayer.Settings.PAY_DESCRIPTION, data.GatewayDescription);
                }
                if (data.GatewayReturnURL != null)
                {
                    data.GatewayReturnURL = data.GatewayReturnURL.Trim();
                    if (data.GatewayReturnURL != "") BLayer.Settings.SetValue(CLayer.Settings.PAY_RETURN_URL, data.GatewayReturnURL);
                }
                if (data.GatewaySecretKey != null)
                {
                    data.GatewaySecretKey = data.GatewaySecretKey.Trim();
                    if (data.GatewaySecretKey != "") BLayer.Settings.SetValue(CLayer.Settings.PAY_SECRET_KEY, data.GatewaySecretKey);
                }

                if (data.OfflinePaymentGatewayReturnURL != null)
                {
                    data.OfflinePaymentGatewayReturnURL = data.OfflinePaymentGatewayReturnURL.Trim();
                    if (data.OfflinePaymentGatewayReturnURL != "") BLayer.Settings.SetValue(CLayer.Settings.OFFLINE_PAY_RETURN_URL, data.OfflinePaymentGatewayReturnURL);
                }
                if (data.OfflinePaymentPaypalGatewayReturnURL != null)
                {
                    data.OfflinePaymentPaypalGatewayReturnURL = data.OfflinePaymentPaypalGatewayReturnURL.Trim();
                    if (data.OfflinePaymentPaypalGatewayReturnURL != "") BLayer.Settings.SetValue(CLayer.Settings.OFFLINE_PAYPAL_RETURN_URL, data.OfflinePaymentPaypalGatewayReturnURL);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveAPI(Models.SettingsModel data)
        {
            try
            {

                if (data.GoogleAPIKey != null)
                {
                    data.GoogleAPIKey = data.GoogleAPIKey.Trim();
                    if (data.GoogleAPIKey != "") BLayer.Settings.SetValue(CLayer.Settings.GOOGLE_API_KEY, data.GoogleAPIKey);
                }
                if (data.GoogleLocationLink != null)
                {
                    data.GoogleLocationLink = data.GoogleLocationLink.Trim();
                    if (data.GoogleLocationLink != "") BLayer.Settings.SetValue(CLayer.Settings.GOOGLE_API_QUERY, data.GoogleLocationLink);
                }
                if (data.SMSAccountId != null)
                {
                    data.SMSAccountId = data.SMSAccountId.Trim();
                    if (data.SMSAccountId != "") BLayer.Settings.SetValue(CLayer.Settings.SMS_SENDER_ID, data.SMSAccountId);
                }
                if (data.SMSLink != null)
                {
                    data.SMSLink = data.SMSLink.Trim();
                    if (data.SMSLink != "") BLayer.Settings.SetValue(CLayer.Settings.SMS_URL, data.SMSLink);
                }
                if (data.SMSAPI != null)
                {
                    data.SMSAPI = data.SMSAPI.Trim();
                    if (data.SMSAPI != "") BLayer.Settings.SetValue(CLayer.Settings.SMS_API, data.SMSAPI);
                }
                if (data.SMSUserName != null)
                {
                    data.SMSUserName = data.SMSUserName.Trim();
                    if (data.SMSUserName != "") BLayer.Settings.SetValue(CLayer.Settings.SMS_USERNAME, data.SMSUserName);
                }
                if (data.SMSPassword != null)
                {
                    data.SMSPassword = data.SMSPassword.Trim();
                    if (data.SMSAPI != "") BLayer.Settings.SetValue(CLayer.Settings.SMS_PASSWORD, data.SMSPassword);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveMarkup(Models.SettingsModel data)
        {
            try
            {
                if (data.B2BMarkupLongTerm > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.B2B_MARKUP_LONG_TERM, data.B2BMarkupLongTerm.ToString());
                }
                if (data.B2BMarkupShortTerm > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.B2B_MARKUP_SHORT_TERM, data.B2BMarkupShortTerm.ToString());
                }
                if (data.B2CMarkupLongTerm > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.B2C_MARKUP_LONG_TERM, data.B2CMarkupLongTerm.ToString());
                }
                if (data.B2CMarkupShortTerm > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.B2C_MARKUP_SHORT_TERM, data.B2CMarkupShortTerm.ToString());
                }
                if (data.B2BStdLongTerm > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.B2B_STD_LONG_TERM_DIS, data.B2BStdLongTerm.ToString());
                }
                if (data.B2BStdShortTerm > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.B2B_STD_SHORT_TERM_DIS, data.B2BStdShortTerm.ToString());
                }
                if (data.CancellationPeriod > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.CANCELLATION_PERIOD, data.CancellationPeriod.ToString());
                }
                if (data.CancellationCharges > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.CANCELLATION_CHARGES, data.CancellationCharges.ToString());
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveCharges(Models.SettingsModel data)
        {
            try
            {

                if (data.GTAmex > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.TRANSACTION_CHARGE_AMEX, data.GTAmex.ToString());
                }
                if (data.GTCashCard > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.TRANSACTION_CHARGE_CASHCARD, data.GTCashCard.ToString());
                }
                if (data.GTCredit > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.TRANSACTION_CHARGE_CREDITCARD, data.GTCredit.ToString());
                }
                if (data.GTDebit > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.TRANSACTION_CHARGE_DEBITCARD, data.GTDebit.ToString());
                }
                if (data.GTNetbanking > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.TRANSACTION_CHARGE_NETBANKING, data.GTNetbanking.ToString());
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveWidgets(Models.SettingsModel data)
        {
            try
            {
                if (data.WDeals > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.WIDGET_DEALSOFTHEDAYPROFILE_NO, data.WDeals.ToString());
                }
                if (data.WMostPopular > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.WIDGET_MOST_POPULAR_NO, data.WMostPopular.ToString());
                }
                if (data.WNewProperties > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.WIDGET_NEW_PROPERTIES_NO, data.WNewProperties.ToString());
                }
                if (data.WSpecial > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.WIDGET_SPECIAL_OFFERS, data.WSpecial.ToString());
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }
        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveOther(Models.SettingsModel data)
        {
            try
            {
                if (data.MaximumCorpUsers > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.CORP_MAX_USERS, data.MaximumCorpUsers.ToString());
                }
                if (data.GalleryMaxFiles > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.GALLERY_MAX_FILES, data.GalleryMaxFiles.ToString());
                }
                if (data.BookingLockTime > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.LOCKIN_TIME, data.BookingLockTime.ToString());
                }
                if (data.PageLock != null)
                {
                    data.PageLock = data.PageLock.Trim();
                    if (data.PageLock != "") BLayer.Settings.SetValue(CLayer.Settings.PUBLIC_PAGE_LOCK, data.PageLock);
                }
                if (data.ContactNumber != "")
                {
                    BLayer.Settings.SetValue(CLayer.Settings.STAYB_CONTACTNO, data.ContactNumber);
                }
                if (data.B2CPartialPaymentsPctge != null)
                {
                    data.B2CPartialPaymentsPctge = data.B2CPartialPaymentsPctge.Trim();
                    if (data.B2CPartialPaymentsPctge != "") BLayer.Settings.SetValue(CLayer.Settings.PARTIAL_PAYMENT_PERCENTAGE_B2C, data.B2CPartialPaymentsPctge);
                }
                if (data.B2BPartialPaymentsPctge != null)
                {
                    data.B2BPartialPaymentsPctge = data.B2BPartialPaymentsPctge.Trim();
                    if (data.B2BPartialPaymentsPctge != "") BLayer.Settings.SetValue(CLayer.Settings.PARTIAL_PAYMENT_PERCENTAGE_B2B, data.B2BPartialPaymentsPctge);
                }
                if (data.PartialPaymentFirstReminder != null)
                {
                    data.PartialPaymentFirstReminder = data.PartialPaymentFirstReminder.Trim();
                    if (data.PartialPaymentFirstReminder != "") BLayer.Settings.SetValue(CLayer.Settings.PARTIAL_PAYMENT_FIRST_INSTALLMENT, data.PartialPaymentFirstReminder);
                }
                if (data.PartialPaymentSecondReminder != null)
                {
                    data.PartialPaymentSecondReminder = data.PartialPaymentSecondReminder.Trim();
                    if (data.PartialPaymentSecondReminder != "") BLayer.Settings.SetValue(CLayer.Settings.PARTIAL_PAYMENT_SECOND_INSTALLMENT, data.PartialPaymentSecondReminder);
                }

                if (data.CreditAmount != null)
                {

                    data.CreditAmount = data.CreditAmount.Trim();
                    if (data.CreditAmount != "") BLayer.Settings.SetValue(CLayer.Settings.BOOKING_CREDIT_AMOUNT_B2BCORPORATE, data.CreditAmount);
                }

                if (data.CreditDays != null)
                {
                    data.CreditDays = data.CreditDays.Trim();
                    if (data.CreditDays != "") BLayer.Settings.SetValue(CLayer.Settings.BOOKING_CREDIT_DAYS_B2BCORPORATE, data.CreditDays);
                }

                if (data.CorporateSmedybookrxp != null)
                {
                    data.CorporateSmedybookrxp = data.CorporateSmedybookrxp.Trim();
                    if (data.CorporateSmedybookrxp != "") BLayer.Settings.SetValue(CLayer.Settings.CORPORATE_SAMEDAY_BOOKING_EXP, data.CorporateSmedybookrxp);
                }

                if (data.Default_Password != null)
                {
                    data.Default_Password = data.Default_Password.Trim();
                    if (data.Default_Password != "") BLayer.Settings.SetValue(CLayer.Settings.DEFAULT_PASSWORD, data.Default_Password);
                }
                if (data.bank != null)
                {
                    data.bank = data.bank.Trim();
                    if (data.bank != "") BLayer.Settings.SetValue(CLayer.Settings.Bank, data.bank);
                }
                if (data.Grossmarginperc != null)
                {
                    data.Grossmarginperc = data.Grossmarginperc.Trim();
                    if (data.Grossmarginperc != "") BLayer.Settings.SetValue(CLayer.Settings.Grossmarginperc, data.Grossmarginperc);
                }
                if (data.InvDueDays != null)
                {
                    data.InvDueDays = data.InvDueDays.Trim();
                    if (data.InvDueDays != "") BLayer.Settings.SetValue(CLayer.Settings.INVOICE_DUE_DAYS, data.InvDueDays);
                }
              
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }

        [Common.AdminRolePermission]
        [HttpPost]
        public ActionResult SaveGSTTaxRates(Models.SettingsModel data)
        {
            try
            {
                if (data.CGSTTaxRate > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.CGST_TAXRATE, data.CGSTTaxRate.ToString());
                }
                if (data.SGSTTaxRate > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.SGST_TAXRATE, data.SGSTTaxRate.ToString());
                }
                if (data.IGSTTaxRate > 0)
                {
                    BLayer.Settings.SetValue(CLayer.Settings.IGST_TAXRATE, data.IGSTTaxRate.ToString());
                }
               

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index");
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UploadFile()
        {
            string _imgname = string.Empty;
            if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
            {
                var pic = System.Web.HttpContext.Current.Request.Files["MyImages"];
                if (pic.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(pic.FileName);
                    var _ext = Path.GetExtension(pic.FileName);

                    _imgname = Guid.NewGuid().ToString();
                    var _comPath = Server.MapPath("/Files/signature/") + _imgname + _ext;
                    _imgname = _imgname + _ext;

                    ViewBag.Msg = _comPath;
                    var path = _comPath;

                    // Saving Image in Original Mode
                    pic.SaveAs(path);
                    string signpath="Files\\signature\\" + _imgname ;
                    if (path != null)
                    {
                       path = path.Trim();
                       if (path != "") BLayer.Settings.SetValue(CLayer.Settings.SIGNATURE_IMAGE, signpath);
                    }
                    // resizing image
                    MemoryStream ms = new MemoryStream();
                    WebImage img = new WebImage(_comPath);
                    int Height = int.Parse(ConfigurationManager.AppSettings.Get("SignatureImageHieght").ToString());
                    if (img.Height > Height)
                       img.Resize(img.Width, 80,true);
                    img.Save(_comPath);
                    // end resize
                }
            }
            return Json(Convert.ToString(_imgname), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult saveimg(Models.SettingsModel data)
        {
            if (data.SignatureImage != null)
            {
                data.SignatureImage = data.SignatureImage.Trim();
                if (data.SignatureImage != "") BLayer.Settings.SetValue(CLayer.Settings.SIGNATURE_IMAGE, data.SignatureImage);
            }
            return View();
        }
        public string GetNewLock()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }
    }
}