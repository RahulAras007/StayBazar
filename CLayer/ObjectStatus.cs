using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class ObjectStatus
    {
        public const string DESCRIPTION_SEPERATOR = "#256#";

        public enum OfflineBookingTaxType
        {
            ServiceTax = 1,
            GST = 2
        }
        public enum OfflineBookingType
        {
            Regular = 1,
            TAC = 2,
            Direct = 3
        }

        public enum InvoiceType
        {
            Invoice = 1,
            Proforma = 2
        }
        public enum InvoiceStatus
        {
            Saved = 1,
            Submitted = 2
        }

        public enum MsgType
        {
            Query = 1,
            Complaint = 2,
            Feedback = 3
        }
        public enum OfferType
        {
            Accommodation = 1,
            Property = 2
        }
        public enum CancellationType
        {
            FixedNight = 1,
            FixedPercent = 2,
            VariableNights = 3
        }
        public enum TransactionType
        {
            Payment = 1,
            Refund = 2
        }

        public enum TransactionStatus
        {
            Payment = 1,
            Refunded = 2,
            PartialRefund = 3,
            TryingForRefund = 4,
            TryingForCanc = 5,
            Failed = 6
        }
        public enum BookingChangeType
        {
            Modify = 1,
            Cancel = 2
        }
        public enum PaymentMethod
        {
            None = 0,
            CreditCard = 1,
            AMEX_JCB = 2,
            DebitCard = 3,
            NetBanking = 4,
            CashCard = 5,
            IMPS = 6,
            RUPAY = 7,
            Test = 8
        }
        public enum PropertFor
        {
            B2B = 1,
            B2C = 2,
            Both = 3,
        }
        public enum ManageFor
        {
            Business = 1,
            Customer = 2

        }
        public enum StatusType
        {
            All = 0,
            Active = 1,
            Disabled = 2, //set for booking items too
            Blocked = 3,
            Deleted = 4,
            Draft = 5,
            Unread = 6,
            NotVerified = 7,
            Read = 8,
            Accepted = 9,
            Verified = 10,
            Rejected = 11

        }
        //Done by rahul 0n 08-01-2020
        public enum AssistedBookingStatusFlagType
        {
            Yes=1,
            No=0
        }
        public enum SupplierPaymentStatusType
        {
            All = 2,
            Pending = 0,
            Paid = 1

        }
        public enum SupplierInvoiceStatusType
        {
            All = 2,
            Pending = 0,
            Completed = 1
        }

        public enum Corpcreditbookstatus
        {
            All = 2,
            NotPaid = 0,
            Paid = 1,
            BookingRequest = 3,
            BookingConfirm = 4
        }
        public enum Sex { Female = 1, Male = 2 }

        public enum RateType
        {
            All = 0,
            NormalDaily = 1,
            NormalWeekly = 2,
            NormalMonthly = 3,
            NormalAdditional = 4,
            CorporateDaily = 5,
            CorporateWeekly = 6,
            CorporateMonthly = 7,
            CorporateAdditional = 8,
            SupplierDaily = 9,
            SupplierWeekly = 10,
            SupplierMonthly = 11,
            SupplierAdditional = 12,
            TravelAgentDaily = 13,
            TravelAgentWeekly = 14,
            TravelAgentMonthly = 15,
            TravelAgentAdditional = 16

        }
        public enum PartialPaymentStatus
        {
            None = 0,
            Cart = 1,
            CheckOut = 2,
            InitialPaymentSuccess = 3,
            InitialPaymentFailed = 4,
            SecondPaymentSuccess = 5,
            SecondPaymentFailed = 6,
            BookingCancel = 7,
            Secondpaycheckout = 8


        }
        public enum PaymentOption
        {
            FullPayment = 1,
            PartialPayment = 2,
            CorporateCreditBooking = 3
        }
        public enum OfferRateType
        {
            OfferPercentageRate = 1,
            OfferFlatRate = 2,
            OfferFreeRate = 3
        }
        public enum BookingStatus
        {
            Cart = 1,
            Success = 2,

            CheckOut = 3,
            Cancelled = 4,
            Failed = 5,
            //WaitingForPaymentVerification=6,
            Deleted = 6,
            BookingRequest = 7,
            Decline = 8,
            Offline = 9,
            offlineconfirm = 10,
            WaitingForApproval = 11,
            Approved = 12,
                OfflineBookingCart=13
       //     Confirmed = 13

        }
        public enum BookingApprovalStatus
        {
            WaitingForApproval=0,
            Approved=1,
            ConfirmedforBooking=2,
            Rejected = 3
        }
        public enum OfflinePyamentStatus
        {
            Processing = 1,
            success = 2,
            failed = 3
        }
        public enum PyamentStatus
        {
            Success = 1,
            Failed = 2,
            Processing = 3,
        }
        public enum PaymentType
        {
            CreditCard = 1,
            DebitCard = 2,
            NetBanking = 3
        }

        public const string GUEST_ID_SESSION = "GuestId";
        public const string SITE_CURRENCY = "Site_Currency";

        public enum Gateway
        {
            EBS = 1,
            PayPal = 2,
            HDFC = 3
        }

        public enum GateWayResponse
        {
            Success = 1,
            Flagged = 2,
            Failed = 3,
            Invalid = 4
        }
        public enum BookingRequestStatus
        {
            All = 0,
            Request = 7,
            Declined = 8

        }

        public enum BookingOfflineStatus
        {
            All = 0,
            Request = 9,
            Confirmed = 10,
            Approved = 5

        }
        public enum PropertyInventoryType
        {
            Online = 1,
            Offline = 2

        }

        public enum CustomerPaymentMode { Advance_Payment = 1, Payment_on_check_out = 2, Credit = 3 }

        public enum InventoryAPIType
        {
            SBInventory = 0,
            Maxmojo = 1,
            Amadeus = 2,
            Tamarind = 4,
            TBO = 5
        }
        public enum AmedusDefaultStayCategory
        {
            Hotel = 39
        }
        public enum AmedusDefaultAccommodationType
        {
            SingleBedRoom = 4
        }
        public enum PNROptionCodes
        {
            OptionPNRGeneration = 0,
            OptionPNRFinalize = 11,
        }
        public enum GDSType
        {
            HotelSingleAvailability = 1,
            HotelSingleAvailabilityLiveSearch = 2,
            HotelEnhancedPricing = 3,
            PNRAddMultiElements = 4,
            HotelSell = 5,
            PNRAddMultiElementsConfirm = 6,
            PNRCancel = 7,
            PNRRetrieve = 8,
            HotelDescriptiveinfo = 9,
            BulkHotelImageDescriptionUpdation = 10,
            BulkCancelIncompleteBookings = 11
            //HotelSingleAvailability = 1,
            //HotelSingleAvailability = 1,
            //HotelSingleAvailability = 1,
            //HotelSingleAvailability = 1,

        }
        public enum GDSGuaranteeCode
        {
            Guarantee = 31,
            Deposit = 8
        }
        public enum APIPriceMarkup
        {
            TamarindAPI = 4,
            TBOAPI = 5
        }
        /*
        public enum ApprovalOrder
        {
            FirstLevel = 1,
            SecondLevel = 2,
            ThridLevel = 3
        }
        */
    }
}
