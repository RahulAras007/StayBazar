using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CLayer
{
    public partial class  GDSPriceingDetails
    {
        [XmlRoot(ElementName = "From", Namespace = "http://www.w3.org/2005/08/addressing")]
        public class From
        {
            [XmlElement(ElementName = "Address", Namespace = "http://www.w3.org/2005/08/addressing")]
            public string Address { get; set; }
        }

        [XmlRoot(ElementName = "RelatesTo", Namespace = "http://www.w3.org/2005/08/addressing")]
        public class RelatesTo
        {
            [XmlAttribute(AttributeName = "RelationshipType")]
            public string RelationshipType { get; set; }
            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "Session", Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
        public class Session
        {
            [XmlElement(ElementName = "SessionId", Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
            public string SessionId { get; set; }
            [XmlElement(ElementName = "SequenceNumber", Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
            public string SequenceNumber { get; set; }
            [XmlElement(ElementName = "SecurityToken", Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
            public string SecurityToken { get; set; }
            [XmlAttribute(AttributeName = "TransactionStatusCode")]
            public string TransactionStatusCode { get; set; }
        }

        [XmlRoot(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Header
        {
            [XmlElement(ElementName = "To", Namespace = "http://www.w3.org/2005/08/addressing")]
            public string To { get; set; }
            [XmlElement(ElementName = "From", Namespace = "http://www.w3.org/2005/08/addressing")]
            public From From { get; set; }
            [XmlElement(ElementName = "Action", Namespace = "http://www.w3.org/2005/08/addressing")]
            public string Action { get; set; }
            [XmlElement(ElementName = "MessageID", Namespace = "http://www.w3.org/2005/08/addressing")]
            public string MessageID { get; set; }
            [XmlElement(ElementName = "RelatesTo", Namespace = "http://www.w3.org/2005/08/addressing")]
            public RelatesTo RelatesTo { get; set; }
            [XmlElement(ElementName = "Session", Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
            public Session Session { get; set; }
        }

        [XmlRoot(ElementName = "CustLoyalty", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class CustLoyalty
        {
            [XmlAttribute(AttributeName = "TravelSector")]
            public string TravelSector { get; set; }
        }

        [XmlRoot(ElementName = "Customer", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Customer
        {
            [XmlElement(ElementName = "CustLoyalty", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public CustLoyalty CustLoyalty { get; set; }
        }

        [XmlRoot(ElementName = "Profile", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Profile
        {
            [XmlElement(ElementName = "Customer", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Customer Customer { get; set; }
        }

        [XmlRoot(ElementName = "ProfileInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class ProfileInfo
        {
            [XmlElement(ElementName = "Profile", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Profile Profile { get; set; }
        }

        [XmlRoot(ElementName = "Profiles", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Profiles
        {
            [XmlElement(ElementName = "ProfileInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<ProfileInfo> ProfileInfo { get; set; }
        }

        [XmlRoot(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Text
        {
            [XmlAttribute(AttributeName = "Formatted")]
            public string Formatted { get; set; }
            [XmlText]
            public string text { get; set; }
        }

        [XmlRoot(ElementName = "Paragraph", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Paragraph
        {
            [XmlElement(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Text Text { get; set; }
            [XmlAttribute(AttributeName = "Name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "SubSection", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class SubSection
        {
            [XmlElement(ElementName = "Paragraph", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Paragraph> Paragraph { get; set; }
        }

        [XmlRoot(ElementName = "VendorMessage", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class VendorMessage
        {
            [XmlElement(ElementName = "SubSection", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public SubSection SubSection { get; set; }
            [XmlAttribute(AttributeName = "InfoType")]
            public string InfoType { get; set; }
        }

        [XmlRoot(ElementName = "VendorMessages", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class VendorMessages
        {
            [XmlElement(ElementName = "VendorMessage", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<VendorMessage> VendorMessage { get; set; }
        }

        [XmlRoot(ElementName = "CountryName", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class CountryName
        {
            [XmlAttribute(AttributeName = "Code")]
            public string Code { get; set; }
        }

        [XmlRoot(ElementName = "Address", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Address
        {
            [XmlElement(ElementName = "CountryName", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public CountryName CountryName { get; set; }
        }

        [XmlRoot(ElementName = "Transportation", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Transportation
        {
            [XmlAttribute(AttributeName = "TransportationCode")]
            public string TransportationCode { get; set; }
        }

        [XmlRoot(ElementName = "Transportations", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Transportations
        {
            [XmlElement(ElementName = "Transportation", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Transportation Transportation { get; set; }
        }

        [XmlRoot(ElementName = "RelativePosition", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RelativePosition
        {
            [XmlElement(ElementName = "Transportations", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Transportations Transportations { get; set; }
        }

        [XmlRoot(ElementName = "HotelAmenity", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class HotelAmenity
        {
            [XmlAttribute(AttributeName = "Code")]
            public string Code { get; set; }
        }

        [XmlRoot(ElementName = "BasicPropertyInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class BasicPropertyInfo
        {
            [XmlElement(ElementName = "VendorMessages", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public VendorMessages VendorMessages { get; set; }
            [XmlElement(ElementName = "Address", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Address Address { get; set; }
            [XmlElement(ElementName = "RelativePosition", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RelativePosition RelativePosition { get; set; }
            [XmlElement(ElementName = "HotelAmenity", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<HotelAmenity> HotelAmenity { get; set; }
            [XmlAttribute(AttributeName = "ChainCode")]
            public string ChainCode { get; set; }
            [XmlAttribute(AttributeName = "HotelCode")]
            public string HotelCode { get; set; }
            [XmlAttribute(AttributeName = "HotelCityCode")]
            public string HotelCityCode { get; set; }
            [XmlAttribute(AttributeName = "HotelName")]
            public string HotelName { get; set; }
            [XmlAttribute(AttributeName = "HotelCodeContext")]
            public string HotelCodeContext { get; set; }
            [XmlAttribute(AttributeName = "ChainName")]
            public string ChainName { get; set; }
            [XmlAttribute(AttributeName = "AreaID")]
            public string AreaID { get; set; }
            [XmlAttribute(AttributeName = "SupplierIntegrationLevel")]
            public string SupplierIntegrationLevel { get; set; }
        }

        [XmlRoot(ElementName = "HotelStay", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class HotelStay
        {
            [XmlElement(ElementName = "BasicPropertyInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public BasicPropertyInfo BasicPropertyInfo { get; set; }
            [XmlAttribute(AttributeName = "RoomStayRPH")]
            public string RoomStayRPH { get; set; }
        }

        [XmlRoot(ElementName = "HotelStays", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class HotelStays
        {
            [XmlElement(ElementName = "HotelStay", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public HotelStay HotelStay { get; set; }
        }

        [XmlRoot(ElementName = "RoomType", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RoomType
        {
            [XmlAttribute(AttributeName = "IsConverted")]
            public string IsConverted { get; set; }
            [XmlAttribute(AttributeName = "RoomType")]
            public string _RoomType { get; set; }
            [XmlAttribute(AttributeName = "RoomTypeCode")]
            public string RoomTypeCode { get; set; }
        }

        [XmlRoot(ElementName = "RoomTypes", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RoomTypes
        {
            [XmlElement(ElementName = "RoomType", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RoomType RoomType { get; set; }
        }

        [XmlRoot(ElementName = "Guarantee", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Guarantee
        {
            [XmlAttribute(AttributeName = "GuaranteeCode")]
            public string GuaranteeCode { get; set; }
            [XmlAttribute(AttributeName = "GuaranteeType")]
            public string GuaranteeType { get; set; }
        }

        [XmlRoot(ElementName = "PenaltyDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class PenaltyDescription
        {
            [XmlElement(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "CancelPenalty", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class CancelPenalty
        {
            [XmlElement(ElementName = "PenaltyDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<PenaltyDescription> PenaltyDescription { get; set; }
            [XmlAttribute(AttributeName = "PolicyCode")]
            public string PolicyCode { get; set; }
        }

        [XmlRoot(ElementName = "CancelPenalties", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class CancelPenalties
        {
            [XmlElement(ElementName = "CancelPenalty", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public CancelPenalty CancelPenalty { get; set; }
        }

        [XmlRoot(ElementName = "RatePlanDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RatePlanDescription
        {
            [XmlElement(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "Commission", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Commission
        {
            [XmlAttribute(AttributeName = "StatusType")]
            public string StatusType { get; set; }
        }

        [XmlRoot(ElementName = "DetailDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class DetailDescription
        {
            [XmlElement(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "AdditionalDetail", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class AdditionalDetail
        {
            [XmlElement(ElementName = "DetailDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public DetailDescription DetailDescription { get; set; }
            [XmlAttribute(AttributeName = "Type")]
            public string Type { get; set; }
        }

        [XmlRoot(ElementName = "AdditionalDetails", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class AdditionalDetails
        {
            [XmlElement(ElementName = "AdditionalDetail", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<AdditionalDetail> AdditionalDetail { get; set; }
        }

        [XmlRoot(ElementName = "RatePlan", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RatePlan
        {
            [XmlElement(ElementName = "Guarantee", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Guarantee Guarantee { get; set; }
            [XmlElement(ElementName = "CancelPenalties", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public CancelPenalties CancelPenalties { get; set; }
            [XmlElement(ElementName = "RatePlanDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RatePlanDescription RatePlanDescription { get; set; }
            [XmlElement(ElementName = "Commission", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Commission Commission { get; set; }
            [XmlElement(ElementName = "AdditionalDetails", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public AdditionalDetails AdditionalDetails { get; set; }
            [XmlAttribute(AttributeName = "RatePlanCode")]
            public string RatePlanCode { get; set; }
            [XmlAttribute(AttributeName = "RateIndicator")]
            public string RateIndicator { get; set; }
        }

        [XmlRoot(ElementName = "RatePlans", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RatePlans
        {
            [XmlElement(ElementName = "RatePlan", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RatePlan RatePlan { get; set; }
        }

        [XmlRoot(ElementName = "Base", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Base
        {
            [XmlAttribute(AttributeName = "AmountBeforeTax")]
            public string AmountBeforeTax { get; set; }
            [XmlAttribute(AttributeName = "CurrencyCode")]
            public string CurrencyCode { get; set; }
        }

        [XmlRoot(ElementName = "GuaranteePayment", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class GuaranteePayment
        {
            [XmlAttribute(AttributeName = "PaymentCode")]
            public string PaymentCode { get; set; }
            [XmlAttribute(AttributeName = "GuaranteeType")]
            public string GuaranteeType { get; set; }
        }

        [XmlRoot(ElementName = "PaymentPolicies", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class PaymentPolicies
        {
            [XmlElement(ElementName = "GuaranteePayment", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public GuaranteePayment GuaranteePayment { get; set; }
        }

        [XmlRoot(ElementName = "Rate", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Rate
        {
            [XmlElement(ElementName = "Base", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Base Base { get; set; }
            [XmlElement(ElementName = "PaymentPolicies", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public PaymentPolicies PaymentPolicies { get; set; }
            [XmlAttribute(AttributeName = "EffectiveDate")]
            public string EffectiveDate { get; set; }
            [XmlAttribute(AttributeName = "ExpireDate")]
            public string ExpireDate { get; set; }
            [XmlAttribute(AttributeName = "RateTimeUnit")]
            public string RateTimeUnit { get; set; }
            [XmlAttribute(AttributeName = "MinLOS")]
            public string MinLOS { get; set; }
            [XmlAttribute(AttributeName = "MaxLOS")]
            public string MaxLOS { get; set; }
        }

        [XmlRoot(ElementName = "Rates", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Rates
        {
            [XmlElement(ElementName = "Rate", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Rate Rate { get; set; }
        }

        [XmlRoot(ElementName = "RoomRateDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RoomRateDescription
        {
            [XmlElement(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Text> Text { get; set; }
            [XmlAttribute(AttributeName = "Name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "Tax", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Tax
        {
            [XmlAttribute(AttributeName = "Code")]
            public string Code { get; set; }
            [XmlAttribute(AttributeName = "Amount")]
            public string Amount { get; set; }
            [XmlAttribute(AttributeName = "CurrencyCode")]
            public string CurrencyCode { get; set; }
            [XmlAttribute(AttributeName = "ChargeUnit")]
            public string ChargeUnit { get; set; }
        }

        [XmlRoot(ElementName = "Taxes", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Taxes
        {
            [XmlElement(ElementName = "Tax", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Tax> Tax { get; set; }
        }

        [XmlRoot(ElementName = "Total", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Total
        {
            [XmlElement(ElementName = "Taxes", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Taxes Taxes { get; set; }
            [XmlAttribute(AttributeName = "AmountBeforeTax")]
            public string AmountBeforeTax { get; set; }
            [XmlAttribute(AttributeName = "AmountAfterTax")]
            public string AmountAfterTax { get; set; }
            [XmlAttribute(AttributeName = "CurrencyCode")]
            public string CurrencyCode { get; set; }
        }

        [XmlRoot(ElementName = "RoomRate", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RoomRate
        {
            [XmlElement(ElementName = "Rates", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Rates Rates { get; set; }
            [XmlElement(ElementName = "RoomRateDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RoomRateDescription RoomRateDescription { get; set; }
            [XmlElement(ElementName = "Total", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Total Total { get; set; }
            [XmlAttribute(AttributeName = "BookingCode")]
            public string BookingCode { get; set; }
            [XmlAttribute(AttributeName = "RoomTypeCode")]
            public string RoomTypeCode { get; set; }
            [XmlAttribute(AttributeName = "NumberOfUnits")]
            public string NumberOfUnits { get; set; }
            [XmlAttribute(AttributeName = "RatePlanCode")]
            public string RatePlanCode { get; set; }
            [XmlAttribute(AttributeName = "RatePlanCategory")]
            public string RatePlanCategory { get; set; }
        }

        [XmlRoot(ElementName = "RoomRates", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RoomRates
        {
            [XmlElement(ElementName = "RoomRate", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RoomRate RoomRate { get; set; }
        }

        [XmlRoot(ElementName = "GuestCount", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class GuestCount
        {
            [XmlAttribute(AttributeName = "AgeQualifyingCode")]
            public string AgeQualifyingCode { get; set; }
            [XmlAttribute(AttributeName = "Count")]
            public string Count { get; set; }
        }

        [XmlRoot(ElementName = "GuestCounts", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class GuestCounts
        {
            [XmlElement(ElementName = "GuestCount", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public GuestCount GuestCount { get; set; }
        }

        [XmlRoot(ElementName = "StartDateWindow", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class StartDateWindow
        {
            [XmlAttribute(AttributeName = "DOW")]
            public string DOW { get; set; }
        }

        [XmlRoot(ElementName = "EndDateWindow", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class EndDateWindow
        {
            [XmlAttribute(AttributeName = "DOW")]
            public string DOW { get; set; }
        }

        [XmlRoot(ElementName = "TimeSpan", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class TimeSpan
        {
            [XmlElement(ElementName = "StartDateWindow", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public StartDateWindow StartDateWindow { get; set; }
            [XmlElement(ElementName = "EndDateWindow", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public EndDateWindow EndDateWindow { get; set; }
            [XmlAttribute(AttributeName = "Start")]
            public string Start { get; set; }
            [XmlAttribute(AttributeName = "End")]
            public string End { get; set; }
        }

        [XmlRoot(ElementName = "RoomStay", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RoomStay
        {
            [XmlElement(ElementName = "RoomTypes", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RoomTypes RoomTypes { get; set; }
            [XmlElement(ElementName = "RatePlans", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RatePlans RatePlans { get; set; }
            [XmlElement(ElementName = "RoomRates", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RoomRates RoomRates { get; set; }
            [XmlElement(ElementName = "GuestCounts", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public GuestCounts GuestCounts { get; set; }
            [XmlElement(ElementName = "TimeSpan", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public TimeSpan TimeSpan { get; set; }
            [XmlElement(ElementName = "Total", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Total Total { get; set; }
            [XmlAttribute(AttributeName = "MarketCode")]
            public string MarketCode { get; set; }
            [XmlAttribute(AttributeName = "SourceOfBusiness")]
            public string SourceOfBusiness { get; set; }
            [XmlAttribute(AttributeName = "IsAlternate")]
            public string IsAlternate { get; set; }
            [XmlAttribute(AttributeName = "AvailabilityStatus")]
            public string AvailabilityStatus { get; set; }
            [XmlAttribute(AttributeName = "InfoSource")]
            public string InfoSource { get; set; }
            [XmlAttribute(AttributeName = "RPH")]
            public string RPH { get; set; }
        }

        [XmlRoot(ElementName = "RoomStays", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RoomStays
        {
            [XmlElement(ElementName = "RoomStay", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RoomStay RoomStay { get; set; }
        }

        [XmlRoot(ElementName = "OTA_HotelAvailRS", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class OTA_HotelAvailRS
        {
            [XmlElement(ElementName = "Success", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string Success { get; set; }
            [XmlElement(ElementName = "Profiles", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Profiles Profiles { get; set; }
            [XmlElement(ElementName = "HotelStays", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public HotelStays HotelStays { get; set; }
            [XmlElement(ElementName = "RoomStays", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RoomStays RoomStays { get; set; }
            [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
            public string SchemaLocation { get; set; }
            [XmlAttribute(AttributeName = "EchoToken")]
            public string EchoToken { get; set; }
            [XmlAttribute(AttributeName = "Version")]
            public string Version { get; set; }
            [XmlAttribute(AttributeName = "PrimaryLangID")]
            public string PrimaryLangID { get; set; }
            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns { get; set; }
            [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Xsi { get; set; }
        }

        [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Body
        {
            [XmlElement(ElementName = "OTA_HotelAvailRS", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public OTA_HotelAvailRS OTA_HotelAvailRS { get; set; }
        }

        [XmlRoot(ElementName = "Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Envelope
        {
            [XmlElement(ElementName = "Header", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Header Header { get; set; }
            [XmlElement(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
            public Body Body { get; set; }
            [XmlAttribute(AttributeName = "soap", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Soap { get; set; }
            [XmlAttribute(AttributeName = "awsse", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Awsse { get; set; }
            [XmlAttribute(AttributeName = "wsa", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Wsa { get; set; }
        }

    }


}
