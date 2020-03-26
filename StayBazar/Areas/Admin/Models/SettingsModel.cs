using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class SettingsModel
    {
        //Mails
        //  [Required(ErrorMessage="Required")]
        [Display(Name = "SMTP Server")]
        public string MailServer { get; set; }
        [Display(Name = "Port")]

        [Range(1, 10000, ErrorMessage = "Incorrect Port Number")]
        public int Port { get; set; }
        [Display(Name = "Mail Security")]
        public int MailSecurity { get; set; }

        
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        [Display(Name = "Query Email")]
        public string QueryEmail { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        [Display(Name = "Customer Query Email")]
        public string CustomerQueryCCMails { get; set; }

        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        [Display(Name = "SupplierPaymentSchedule Email")]
        public string SupplierPaymentScheduleEmail { get; set; }

        [Display(Name = "Query Password")]
        public string QueryPassword { get; set; }
        [Display(Name = "Reservation Email")]
        public string ReservationEmail { get; set; }
        [Display(Name = "Reservation Password")]

        public string ReservationPassword { get; set; }

        [Display(Name = "Support Email")]
        public string SupportEmail { get; set; }

        [Display(Name = "Support Password")]
        public string SupportEmailPassword { get; set; }

        [Display(Name = "Cc added for Customer communications")]
        public string CCCustomercommunications { get; set; }

        [Display(Name = "Cc added for Supplier  communications")]
        public string CCSuppliercommunications { get; set; }

        //signature image
        [Display(Name = "Signature Image")]
        public string SignatureImage { get; set; }
        //bank details
        [Display(Name = "Bank Details")]
        public string bank { get; set; }
        [Display(Name = "Gross Margin (%)")]
        public string Grossmarginperc { get; set; }
        
        //Gateway details
        [Display(Name = "Refund Link")]
        public string RefundLink { get; set; }
        [Display(Name = "Account ID")]
        public string GatewayAccountId { get; set; }
        [Display(Name = "Description")]
        public string GatewayDescription { get; set; }
        [Display(Name = "Return URL")]
        public string GatewayReturnURL { get; set; }
        [Display(Name = "Secret Key")]
        public string GatewaySecretKey { get; set; }

        [Display(Name = "Offline Payment ReturnURL")]
        public string OfflinePaymentGatewayReturnURL { get; set; }

        [Display(Name = "Offline Payment Paypal ReturnURL")]
        public string OfflinePaymentPaypalGatewayReturnURL { get; set; }
        //google and 3rd party
        [Display(Name = "Google Location Link ")]
        public string GoogleLocationLink { get; set; }
        [Display(Name = "Google API Key")]
        public string GoogleAPIKey { get; set; }
        [Display(Name = "SMS API Key")]
        public string SMSAPI { get; set; }
        [Display(Name = "SMS Account ID")]
        public string SMSAccountId { get; set; }
        [Display(Name = "SMS Link")]
        public string SMSLink { get; set; }
        [Display(Name = "SMS UserName")]
        public string SMSUserName { get; set; }
        [Display(Name = "SMS Password")]
        public string SMSPassword { get; set; }

        //  Markups and rates
        //[Display(Name = "")]
        [Display(Name = "B2B Markup Long Term")]
        public double B2BMarkupLongTerm { get; set; }
        [Display(Name = "B2B Markup Shorterm")]
        public double B2BMarkupShortTerm { get; set; }
        [Display(Name = "B2C Markup Long Term")]
        public double B2CMarkupLongTerm { get; set; }
        [Display(Name = "B2C Markup Short Term")]
        public double B2CMarkupShortTerm { get; set; }
        [Display(Name = "B2B Discount Long Term")]
        public double B2BStdLongTerm { get; set; }
        [Display(Name = "B2B Discount Short Term")]
        public double B2BStdShortTerm { get; set; }
        [Display(Name = "Default Cancellation Charges")]
        public double CancellationCharges { get; set; }
        [Display(Name = "Default Cancellation Period")]
        public double CancellationPeriod { get; set; }
        //Gateway charges
        [Display(Name = "Amex")]
        public double GTAmex { get; set; }
        [Display(Name = "Credit card")]
        public double GTCredit { get; set; }
        [Display(Name = "Cash card")]
        public double GTCashCard { get; set; }
        [Display(Name = "Debit card")]
        public double GTDebit { get; set; }
        [Display(Name = "Net banking")]
        public double GTNetbanking { get; set; }
        //Widget nos
        [Display(Name = "Most popular widget property count")]
        public int WMostPopular { get; set; }
        [Display(Name = "Deals widget property count")]
        public int WDeals { get; set; }
        [Display(Name = "New properties widget property count")]
        public int WNewProperties { get; set; }
        [Display(Name = "Special offers property count")]
        public int WSpecial { get; set; }
        //others
        [Display(Name = "Default Corporate users")]
        public int MaximumCorpUsers { get; set; }
        [Display(Name = "Booking Lock Time (minutes)")]
        public int BookingLockTime { get; set; }
        [Display(Name = "Property Maximum image files")]
        public int GalleryMaxFiles { get; set; }
        [Display(Name = "Public page lock key")]
        public string PageLock { get; set; }
        [Display(Name = "Staybazar contact no.")]
        public string ContactNumber { get; set; }
        //select lists
        public SelectList SecurityListing { get; set; }

        [Display(Name = "Partial Payment Percentage For B2C")]
        public string B2CPartialPaymentsPctge { get; set; }

        [Display(Name = "Partial Payment Percentage For B2B")]
        public string B2BPartialPaymentsPctge { get; set; }

        [Display(Name = "PartialPayment reminder start day from checkIn date (days)")]
        public string PartialPaymentFirstReminder { get; set; }

        [Display(Name = "PartialPayment cancellation day from checkIn date (days)")]
        public string PartialPaymentSecondReminder { get; set; }

        //For Corporate bookings credit 
        [Display(Name = "No: of Days for Credit (only for b2b corporate bookings)")]
        public string CreditDays { get; set; }
        [Display(Name = "Credit Limit(INR) (only for b2b corporate bookings)")]
        public string CreditAmount { get; set; }

        [Display(Name = "Corporate same day booking allow expiry time ( hour )")]
        public string CorporateSmedybookrxp { get; set; }

        [Display(Name = "Default Password")]
        public string Default_Password { get; set; }


        //paypal settings
        [Display(Name = "Paypal Commission")]
        public double paypalcommission { get; set; }

        [Display(Name = "Paypal Request Url")]
        public string PaypalRequestURL { get; set; }

        [Display(Name = "Paypal Return Url")]
        public string Paypal_ReturnUrl { get; set; }

        [Display(Name = "Paypal Signature")]
        public string Paypal_Signature { get; set; }

        [Display(Name = "Paypal Token Url")]
        public string Paypal_TokenUrl { get; set; }

        [Display(Name = "Paypal User")]
        public string Paypal_User { get; set; }

        [Display(Name = "Paypal Password")]
        public string Paypal_Pwd { get; set; }

        [Display(Name = "Paypal Confirmation Url")]
        public string Paypal_Confirmation_URL { get; set; }


        [Display(Name = "Paypal Cancel Url")]
        public string Paypal_CancelUrl { get; set; }

        //Chat settings
        public int Chat { get; set; }

        [Display(Name = "Livserv chat")]
        [AllowHtml]
        public string Liveservechat { get; set; }

        [Display(Name = "Zopim chat")]
        [AllowHtml]
        public string Zopimchat { get; set; }



        //Maximojo settings
        [Display(Name = "Hotel Availability")]
        public string MaximojoHotelAvailability { get; set; }

        [Display(Name = "Booking Availability")]
        public string MaximojoBookingAvailability { get; set; }

        [Display(Name = "Booking Submit")]
        public string MaximojoBookingSubmit { get; set; }

        [Display(Name = "Booking Verify")]
        public string MaximojoBookingVerify { get; set; }

        [Display(Name = "Booking Cancel")]
        public string MaximojoBookingCancel { get; set; }

        [Display(Name = "UserName")]
        public string MaximojoUserName { get; set; }

        [Display(Name = "Password")]
        public string MaximojoPassword { get; set; }

        [Display(Name = "Partner Code")]
        public string MaximojoPartnerCode { get; set; }

        [Display(Name = "Invoice Due Date (Days)")]
        public string InvDueDays { get; set; }

        [Display(Name = "Account Mail")]
        public string AccountMail { get; set; }



        public string SupplierInvoicerequestMails  { get; set; }
        public string Bookingdeletealertmails { get; set; }


        public string BookingDraftalertmails { get; set; }
        public string Bookingrejectedalertmails { get; set; }

        //GST Tax Rates start
        [Display(Name = "CGST Tax Rate")]
        public decimal CGSTTaxRate { get; set; }

        [Display(Name = "SGST Tax Rate")]
        public decimal SGSTTaxRate { get; set; }

        [Display(Name = "IGST Tax Rate")]
        public decimal IGSTTaxRate { get; set; }

        //GST Tax Rates end



        public enum chatset
        {
            Off = 1,
            Livserve = 2,
            Zopim = 3,
            TimeBased = 4

        }
        public SettingsModel()
        {
            List<SelectListItem> mset = new List<SelectListItem>();
            mset.Add(new SelectListItem() { Text = "No Authentication", Value = "0", Selected = true });
            mset.Add(new SelectListItem() { Text = "Simple Authentication", Value = "1" });
            mset.Add(new SelectListItem() { Text = "Simple With SSL", Value = "2" });
            SecurityListing = new SelectList(mset, "Value", "Text");
        }
    }
}