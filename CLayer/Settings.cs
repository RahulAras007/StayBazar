using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Settings
    {
      
        //Paypal
        public const string PAYPAL_USER = "Paypal_User";
        public const string PAYPAL_PWD = "Paypal_Pwd";
        public const string PAYPAL_SIGNAUTRE = "Paypal_Signature";
        public const string PAYPAL_RETURNURL = "Paypal_ReturnUrl";
        public const string PAYPAL_CANCELURL = "Paypal_CancelUrl";
        public const string PAYPAL_REQUEST_URL = "Paypal_RequestURL";
        public const string PAYPAL_TOKEN_URL = "Paypal_TokenUrl";
        public const string PAYPAL_CONFIRM_PAY_URL = "Paypal_Confirmation_URL";
        public const string PAYPAL_COMMISSION = "paypalcommission";



        //Maximojo
        public const string MAXIMOJOBOOKINGAVAILIBILITY = "MaximojoBookingAvailability";
        public const string MAXIMOJOBOOKINGCANCEL = "MaximojoBookingCancel";
        public const string MAXIMOJOBOOKINGSUBMIT = "MaximojoBookingSubmit";
        public const string MAXIMOJOBOOKINGVERIFY = "MaximojoBookingVerify";
        public const string MAXIMOJOHOTELAVAILIBILITY = "MaximojoHotelAvailability";
        public const string MAXIMOJOPASSWORD = "MaximojoPassword";
        public const string MAXIMOJOUSERNAME = "MaximojoUserName";
        public const string MAXIMOJOPARTNERCODE = "MaximojoPartnerCode";

        //GST Tax rates
        public const string CGST_TAXRATE = "Cgst_TaxRate";
        public const string SGST_TAXRATE = "Sgst_TaxRate";
        public const string IGST_TAXRATE = "Igst_TaxRate";

        //GST tax rates end


        //chat
        public const string CHAT = "Chat";
        public const string LIVSERV = "Liveservechat";
        public const string ZOPIM = "Zopimchat";
        //settings
        public const string SERVER = "Mail_Server";
        public const string MAIL_PORT = "Mail_Port";
       
        public const string IS_SECURE = "Mail_Security";
        public const string GALLERY_MAX_FILES = "Gallery_Max_Files";
        public const string GOOGLE_API_KEY = "GoogleAPIKey";
        public const string GOOGLE_API_QUERY = "GoogleAPIQuery";
        public const string CORP_MAX_USERS = "Corporate_Max_Users";
        public const string DEFAULT_PASSWORD = "Default_Password";
        //widget
        public const string WIDGET_MOST_POPULAR_NO = "WidgetMostPopularNo";
        public const string WIDGET_NEW_PROPERTIES_NO = "WidgetNewPropertiesNo";
        public const string WIDGET_SPECIAL_OFFERS="widgetSpecialOffers";
        public const string WIDGET_DEALSOFTHEDAYPROFILE_NO = "WidgetDealsOfTheDayProfileNo";
        //property settings
        public const string B2C_MARKUP_SHORT_TERM = "B2CMarkupShortTerm";
        public const string B2C_MARKUP_LONG_TERM = "B2CMarkupLongTerm";
        public const string B2B_MARKUP_SHORT_TERM = "B2BMarkupShortTerm";
        public const string B2B_MARKUP_LONG_TERM = "B2BMarkupLongTerm";
        public const string B2B_STD_SHORT_TERM_DIS = "B2BStdShortTermDis";
        public const string B2B_STD_LONG_TERM_DIS = "B2BStdLongTermDis";
        public const string LOCKIN_TIME = "AccommodationLockInTime";
        public const string CANCELLATION_PERIOD = "CancellationPeriod";
        public const string CANCELLATION_CHARGES = "CancellationCharges";
        //EBS
        public const string PAY_ACCOUNT_ID = "PaymentGateway_AccountId";
        public const string PAY_RETURN_URL = "PaymentGateway_ReturnURL";
        public const string PAY_DESCRIPTION = "PaymentGateway_Description";
        public const string PAY_SECRET_KEY = "PaymentGateway_SecretKey";
        public const string PARTIAL_PAYMENT_PERCENTAGE_B2C = "B2CPartialPaymentPercentage";
        public const string PARTIAL_PAYMENT_PERCENTAGE_B2B = "B2BPartialPaymentPercentage";

        //for signature
        public const string SIGNATURE_IMAGE = "SignatureImage";
        //For Corporate bookings credit 
        public const string BOOKING_CREDIT_DAYS_B2BCORPORATE = "BookingcreditDaysforb2bcorporate";
        public const string BOOKING_CREDIT_AMOUNT_B2BCORPORATE = "BookingcreditAmountforb2bcorporate";

        public const string INVOICE_DUE_DAYS = "Invoiceduedays";
       // public const string ACCOUNTMAIL = "AccountMail";
        

        public const string CORPORATE_SAMEDAY_BOOKING_EXP = "CorporateSamedaybookexpiry";
        //Offline Payment
        public const string OFFLINE_PAY_RETURN_URL = "OfflinePaymentGateway_ReturnURL";
        public const string OFFLINE_PAYPAL_RETURN_URL = "OfflinePaymentPaypal_ReturnUrl";

        public const string PARTIAL_PAYMENT_FIRST_INSTALLMENT = "PartialPaymentFirstReminder";
        public const string PARTIAL_PAYMENT_SECOND_INSTALLMENT = "PartialPaymentSecondReminder";
        //SMS
        public const string SMS_URL = "SMSUrl";
        public const string SMS_SENDER_ID = "SMSId";
        public const string SMS_API = "SMSAPI";
        public const string SMS_USERNAME = "SMSUserName";
        public const string SMS_PASSWORD = "SMSPassWord";
        public const string SMS_REMINDER_TIME = "SMSReminderTime";//24 hours format

        //communication
        public const string QUERY_MAILID = "Query_Email";
        public const string STAYB_CONTACTNO = "SBContactNo";
        public const string PUBLIC_PAGE_LOCK = "SBPublicPageLock";
        public const string REGIS_EMAIL = "Reg_EMail";
        public const string REGIS_EMAIL_PASSWORD = "Reg_Mail_Password";
        public const string QUERY_EMAIL_PASSWORD = "QueryEmailPassword";

     //   public const string Signature = "Signature";

        public const string SUPPORT_EMAIL = "Support_Email";
        public const string SUPPORT_EMAIL_PASSWORD = "Support_Email_Password";

        public const string CC_CUSTOMERCOMMUNICATION = "CCCustomercommunications";
        public const string CC_SUPPLIERCOMMUNICATION = "CCSuppliercommunications";
        public const string ACCOUNT_EMAILS = "AccountMail";
        public const string SUPPLIER_INVOICE_REQUEST_MAILS = "SupplierInvoicerequestmails";
        public const string BOOK_DELETE_ALERT_EMAILS = "Bookingdeletealertmails";


        public const string Booking_Draft_alert_mails = "BookingDraftalertmails";
        public const string Booking_rejected_alert_mails = "Bookingrejectedalertmails";
        //Payment Type
        public const string PAYMENT_MODE_TESTING = "TEST";
        public const string Bank = "bank";
        public const string Grossmarginperc = "Grossmarginperc";
        public const string PAYMENT_MODE_LIVE = "LIVE";
        public const string PAYMENT_REFUND_LINK = "EBSRefundLink";
        //TransactionFees
        public const string TRANSACTION_CHARGE_CREDITCARD = "TFees_Credit";
        public const string TRANSACTION_CHARGE_DEBITCARD = "TFees_Debit";
        public const string TRANSACTION_CHARGE_NETBANKING = "TFees_NetBanking";
        public const string TRANSACTION_CHARGE_AMEX = "TFees_Amex";
        public const string TRANSACTION_CHARGE_CASHCARD = "TFees_CashCard";

        public const decimal TRANSACTION_MIN_AMOUNT = 0;
        public const decimal TRANSACTION_MAX_AMOUNT = 100000;
        public const string SUPPLIERPAYMENTSCHEDULEEMAIL = "SupplierPaymentScheduleEmail";
        public const string CUSTOMERQUERYCCMAILS = "CustomerQueryCCMails";

        public const string STAYBAZARWBSUSERNAME = "StayBazarWBSUserName";
        public const string STAYBAZARWBSPASSWORD = "StayBazarWBSPassword";
        public const string STAYBAZARWBSURL = "StayBazarWBSUrl";
        public const string STAYBAZARWBSHOTELMULTISINGLEACTION = "StayBazarWBSHotelMultiSingleAction";
        public const string STAYBAZARWBSOFFICE = "StayBazarWBSOffice";
        public const string AMADEUSWBSCOUNTRY = "AmadeusWBSCountry";

        public const string AMADEUSNOIMAGE = "AmadeusNoImage";
        public const string STAYBAZARWBSPNRADDACTION = "StayBazarpnraddaction";
        public const string STAYBAZARWBSHOTELSELLACTION = "StayBazarhotelsellaction";
       
        public const string STAYBAZARWBSHOTELPNRCANCELACTION = "StayBazarhotelpnrcancelaction";
        public const string STAYBAZARWBSHOTELPNRRETRIEVEACTION = "StayBazarhotelpnrretrieveaction";
        public const string STAYBAZARWBSHOTELCOMPLETERESERVATION = "StayBazarWBSHotelCompleteReservation";
        public const string  STAYBAZARDEFAULTBILLINGENTITY = "Staybazardefaultbillingentity";
        public const string STAYBAZARWBSSIGNOUT = "StayBazarWBSSignOut";

        public const string CORPORATETESTEMAIL = "CorporateTestEmail";

        public const string GDSCORPORATE = "GdsCorporate";
        public const string GDSCORPORATEUSER = "GdsCorporateuser";
        public const string GDSCORPORATEAPPROVER1 = "GdsCorporateApprover1";
        public const string GDSCORPORATEAPPROVER2 = "GdsCorporateApprover2";




        public enum MailSecurityType
        {
            NoAuthentication = 0,
            SimpleAuthentication = 1,
            SimpleWithSSL = 2
        };

        public string Value { get; set; }
        public string Key { get; set; }


    }
}
