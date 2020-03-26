using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CLayer
{
    public partial class  GDSBookingDetails
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

        [XmlRoot(ElementName = "SegmentCategory", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class SegmentCategory
        {
            [XmlAttribute(AttributeName = "Code")]
            public string Code { get; set; }
        }

        [XmlRoot(ElementName = "Description", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Description
        {
            [XmlAttribute(AttributeName = "Language")]
            public string Language { get; set; }
            [XmlText]
            public string __Text { get; set; }
            [XmlElement(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Text> Text { get; set; }
            [XmlAttribute(AttributeName = "Caption")]
            public string Caption { get; set; }
        }

        [XmlRoot(ElementName = "TextItem", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class TextItem
        {
            [XmlElement(ElementName = "Description", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Description> Description { get; set; }
            [XmlAttribute(AttributeName = "Title")]
            public string Title { get; set; }
        }

        [XmlRoot(ElementName = "TextItems", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class TextItems
        {
            [XmlElement(ElementName = "TextItem", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public TextItem TextItem { get; set; }
        }

        [XmlRoot(ElementName = "MultimediaDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class MultimediaDescription
        {
            [XmlElement(ElementName = "TextItems", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public TextItems TextItems { get; set; }
            [XmlElement(ElementName = "ImageItems", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public ImageItems ImageItems { get; set; }
            [XmlAttribute(AttributeName = "InfoCode")]
            public string InfoCode { get; set; }
            [XmlAttribute(AttributeName = "AdditionalDetailCode")]
            public string AdditionalDetailCode { get; set; }
        }

        [XmlRoot(ElementName = "ImageFormat", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class ImageFormat
        {
            [XmlElement(ElementName = "URL", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string URL { get; set; }
            [XmlAttribute(AttributeName = "Width")]
            public string Width { get; set; }
            [XmlAttribute(AttributeName = "Height")]
            public string Height { get; set; }
            [XmlAttribute(AttributeName = "RecordID")]
            public string RecordID { get; set; }
            [XmlAttribute(AttributeName = "Format")]
            public string Format { get; set; }
            [XmlAttribute(AttributeName = "FileName")]
            public string FileName { get; set; }
            [XmlAttribute(AttributeName = "FileSize")]
            public string FileSize { get; set; }
            [XmlAttribute(AttributeName = "IsOriginalIndicator")]
            public string IsOriginalIndicator { get; set; }
            [XmlAttribute(AttributeName = "DimensionCategory")]
            public string DimensionCategory { get; set; }
        }

        [XmlRoot(ElementName = "ImageItem", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class ImageItem
        {
            [XmlElement(ElementName = "ImageFormat", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<ImageFormat> ImageFormat { get; set; }
            [XmlElement(ElementName = "Description", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Description Description { get; set; }
            [XmlAttribute(AttributeName = "Category")]
            public string Category { get; set; }
            [XmlAttribute(AttributeName = "LastModifyDateTime")]
            public string LastModifyDateTime { get; set; }
        }

        [XmlRoot(ElementName = "ImageItems", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class ImageItems
        {
            [XmlElement(ElementName = "ImageItem", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<ImageItem> ImageItem { get; set; }
        }

        [XmlRoot(ElementName = "MultimediaDescriptions", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class MultimediaDescriptions
        {
            [XmlElement(ElementName = "MultimediaDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<MultimediaDescription> MultimediaDescription { get; set; }
        }

        [XmlRoot(ElementName = "GuestRoomInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class GuestRoomInfo
        {
            [XmlElement(ElementName = "MultimediaDescriptions", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public MultimediaDescriptions MultimediaDescriptions { get; set; }
            [XmlAttribute(AttributeName = "Code")]
            public string Code { get; set; }
        }

        [XmlRoot(ElementName = "CategoryCodes", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class CategoryCodes
        {
            [XmlElement(ElementName = "SegmentCategory", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public SegmentCategory SegmentCategory { get; set; }
            [XmlElement(ElementName = "GuestRoomInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public GuestRoomInfo GuestRoomInfo { get; set; }
        }

        [XmlRoot(ElementName = "Descriptions", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Descriptions
        {
            [XmlElement(ElementName = "MultimediaDescriptions", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public MultimediaDescriptions MultimediaDescriptions { get; set; }
        }

        [XmlRoot(ElementName = "Position", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Position
        {
            [XmlAttribute(AttributeName = "Latitude")]
            public string Latitude { get; set; }
            [XmlAttribute(AttributeName = "Longitude")]
            public string Longitude { get; set; }
        }

        [XmlRoot(ElementName = "Service", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Service
        {
            [XmlAttribute(AttributeName = "BusinessServiceCode")]
            public string BusinessServiceCode { get; set; }
            [XmlAttribute(AttributeName = "Code")]
            public string Code { get; set; }
            [XmlElement(ElementName = "Features", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Features Features { get; set; }
            [XmlElement(ElementName = "MultimediaDescriptions", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public MultimediaDescriptions MultimediaDescriptions { get; set; }
        }

        [XmlRoot(ElementName = "Feature", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Feature
        {
            [XmlAttribute(AttributeName = "AccessibleCode")]
            public string AccessibleCode { get; set; }
            [XmlAttribute(AttributeName = "SecurityCode")]
            public string SecurityCode { get; set; }
        }

        [XmlRoot(ElementName = "Features", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Features
        {
            [XmlElement(ElementName = "Feature", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Feature> Feature { get; set; }
        }

        [XmlRoot(ElementName = "Services", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Services
        {
            [XmlElement(ElementName = "Service", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Service> Service { get; set; }
        }

        [XmlRoot(ElementName = "HotelInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class HotelInfo
        {
            [XmlElement(ElementName = "CategoryCodes", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public CategoryCodes CategoryCodes { get; set; }
            [XmlElement(ElementName = "Descriptions", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Descriptions Descriptions { get; set; }
            [XmlElement(ElementName = "Position", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Position Position { get; set; }
            [XmlElement(ElementName = "Services", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Services Services { get; set; }
        }

        [XmlRoot(ElementName = "TypeRoom", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class TypeRoom
        {
            [XmlAttribute(AttributeName = "RoomTypeCode")]
            public string RoomTypeCode { get; set; }
        }

        [XmlRoot(ElementName = "Amenity", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Amenity
        {
            [XmlAttribute(AttributeName = "RoomAmenityCode")]
            public string RoomAmenityCode { get; set; }
        }

        [XmlRoot(ElementName = "Amenities", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Amenities
        {
            [XmlElement(ElementName = "Amenity", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Amenity> Amenity { get; set; }
        }

        [XmlRoot(ElementName = "GuestRoom", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class GuestRoom
        {
            [XmlElement(ElementName = "TypeRoom", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public TypeRoom TypeRoom { get; set; }
            [XmlElement(ElementName = "Amenities", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Amenities Amenities { get; set; }
        }

        [XmlRoot(ElementName = "GuestRooms", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class GuestRooms
        {
            [XmlElement(ElementName = "GuestRoom", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public GuestRoom GuestRoom { get; set; }
        }

        [XmlRoot(ElementName = "FacilityInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class FacilityInfo
        {
            [XmlElement(ElementName = "GuestRooms", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public GuestRooms GuestRooms { get; set; }
        }

        [XmlRoot(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Text
        {
            [XmlAttribute(AttributeName = "Language")]
            public string Language { get; set; }
            [XmlText]
            public string text { get; set; }
        }

        [XmlRoot(ElementName = "StayRequirement", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class StayRequirement
        {
            [XmlElement(ElementName = "Description", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Description Description { get; set; }
        }

        [XmlRoot(ElementName = "StayRequirements", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class StayRequirements
        {
            [XmlElement(ElementName = "StayRequirement", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public StayRequirement StayRequirement { get; set; }
        }

        [XmlRoot(ElementName = "Policy", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Policy
        {
            [XmlElement(ElementName = "StayRequirements", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public StayRequirements StayRequirements { get; set; }
            [XmlElement(ElementName = "CommissionPolicy", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public CommissionPolicy CommissionPolicy { get; set; }
            [XmlElement(ElementName = "PolicyInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public PolicyInfo PolicyInfo { get; set; }
            [XmlElement(ElementName = "GuaranteePaymentPolicy", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public GuaranteePaymentPolicy GuaranteePaymentPolicy { get; set; }
        }

        [XmlRoot(ElementName = "PaymentCompany", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class PaymentCompany
        {
            [XmlAttribute(AttributeName = "Name")]
            public string Name { get; set; }
        }

        [XmlRoot(ElementName = "CommissionPolicy", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class CommissionPolicy
        {
            [XmlElement(ElementName = "PaymentCompany", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public PaymentCompany PaymentCompany { get; set; }
        }

        [XmlRoot(ElementName = "PolicyInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class PolicyInfo
        {
            [XmlAttribute(AttributeName = "CheckInTime")]
            public string CheckInTime { get; set; }
        }

        [XmlRoot(ElementName = "GuaranteePayment", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class GuaranteePayment
        {
            [XmlElement(ElementName = "Description", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Description Description { get; set; }
            [XmlAttribute(AttributeName = "GuaranteeType")]
            public string GuaranteeType { get; set; }
        }

        [XmlRoot(ElementName = "GuaranteePaymentPolicy", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class GuaranteePaymentPolicy
        {
            [XmlElement(ElementName = "GuaranteePayment", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public GuaranteePayment GuaranteePayment { get; set; }
        }

        [XmlRoot(ElementName = "Policies", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Policies
        {
            [XmlElement(ElementName = "Policy", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Policy> Policy { get; set; }
        }

        [XmlRoot(ElementName = "Transportation", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Transportation
        {
            [XmlElement(ElementName = "MultimediaDescriptions", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public MultimediaDescriptions MultimediaDescriptions { get; set; }
        }

        [XmlRoot(ElementName = "Transportations", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Transportations
        {
            [XmlElement(ElementName = "Transportation", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Transportation Transportation { get; set; }
        }

        [XmlRoot(ElementName = "RefPoint", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RefPoint
        {
            [XmlElement(ElementName = "Transportations", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Transportations Transportations { get; set; }
        }

        [XmlRoot(ElementName = "RefPoints", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class RefPoints
        {
            [XmlElement(ElementName = "RefPoint", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RefPoint RefPoint { get; set; }
        }

        [XmlRoot(ElementName = "Recreation", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Recreation
        {
            [XmlAttribute(AttributeName = "Code")]
            public string Code { get; set; }
        }

        [XmlRoot(ElementName = "Recreations", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Recreations
        {
            [XmlElement(ElementName = "Recreation", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Recreation Recreation { get; set; }
        }

        [XmlRoot(ElementName = "AreaInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class AreaInfo
        {
            [XmlElement(ElementName = "RefPoints", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public RefPoints RefPoints { get; set; }
            [XmlElement(ElementName = "Recreations", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Recreations Recreations { get; set; }
        }

        [XmlRoot(ElementName = "ProgramDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class ProgramDescription
        {
            [XmlElement(ElementName = "Text", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Text Text { get; set; }
        }

        [XmlRoot(ElementName = "LoyalProgram", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class LoyalProgram
        {
            [XmlElement(ElementName = "ProgramDescription", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public ProgramDescription ProgramDescription { get; set; }
        }

        [XmlRoot(ElementName = "LoyalPrograms", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class LoyalPrograms
        {
            [XmlElement(ElementName = "LoyalProgram", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public LoyalProgram LoyalProgram { get; set; }
        }

        [XmlRoot(ElementName = "AffiliationInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class AffiliationInfo
        {
            [XmlElement(ElementName = "LoyalPrograms", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public LoyalPrograms LoyalPrograms { get; set; }
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
            [XmlElement(ElementName = "AddressLine", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string AddressLine { get; set; }
            [XmlElement(ElementName = "CityName", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string CityName { get; set; }
            [XmlElement(ElementName = "PostalCode", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string PostalCode { get; set; }
            [XmlElement(ElementName = "CountryName", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public CountryName CountryName { get; set; }
            [XmlAttribute(AttributeName = "UseType")]
            public string UseType { get; set; }
        }

        [XmlRoot(ElementName = "Addresses", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Addresses
        {
            [XmlElement(ElementName = "Address", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Address Address { get; set; }
        }

        [XmlRoot(ElementName = "Phone", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Phone
        {
            [XmlAttribute(AttributeName = "PhoneTechType")]
            public string PhoneTechType { get; set; }
            [XmlAttribute(AttributeName = "PhoneNumber")]
            public string PhoneNumber { get; set; }
        }

        [XmlRoot(ElementName = "Phones", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Phones
        {
            [XmlElement(ElementName = "Phone", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public List<Phone> Phone { get; set; }
        }
        //[XmlRoot(ElementName = "Email", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        //public class Email
        //{
        //  //  [XmlAttribute(AttributeName = "PhoneTechType")]
        //    public string EmailName { get; set; }
           
        //}
        [XmlRoot(ElementName = "Emails", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class Emails
        {
            [XmlElement(ElementName = "Email", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string Email { get; set; }
        }
        [XmlRoot(ElementName = "ContactInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class ContactInfo
        {
            [XmlElement(ElementName = "Addresses", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Addresses Addresses { get; set; }
            [XmlElement(ElementName = "Phones", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Phones Phones { get; set; }
            [XmlAttribute(AttributeName = "Location")]
            public string Location { get; set; }
            [XmlElement(ElementName = "Emails", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Emails Emails { get; set; }
        }

        [XmlRoot(ElementName = "ContactInfos", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class ContactInfos
        {
            [XmlElement(ElementName = "ContactInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public ContactInfo ContactInfo { get; set; }
        }

        [XmlRoot(ElementName = "HotelDescriptiveContent", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class HotelDescriptiveContent
        {
            [XmlElement(ElementName = "HotelInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public HotelInfo HotelInfo { get; set; }
            [XmlElement(ElementName = "FacilityInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public FacilityInfo FacilityInfo { get; set; }
            [XmlElement(ElementName = "Policies", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public Policies Policies { get; set; }
            [XmlElement(ElementName = "AreaInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public AreaInfo AreaInfo { get; set; }
            [XmlElement(ElementName = "AffiliationInfo", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public AffiliationInfo AffiliationInfo { get; set; }
            [XmlElement(ElementName = "ContactInfos", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public ContactInfos ContactInfos { get; set; }
            [XmlAttribute(AttributeName = "HotelCode")]
            public string HotelCode { get; set; }
            [XmlAttribute(AttributeName = "HotelName")]
            public string HotelName { get; set; }
            [XmlAttribute(AttributeName = "HotelCodeContext")]
            public string HotelCodeContext { get; set; }
        }

        [XmlRoot(ElementName = "HotelDescriptiveContents", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class HotelDescriptiveContents
        {
            [XmlElement(ElementName = "HotelDescriptiveContent", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public HotelDescriptiveContent HotelDescriptiveContent { get; set; }
        }

        [XmlRoot(ElementName = "OTA_HotelDescriptiveInfoRS", Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public class OTA_HotelDescriptiveInfoRS
        {
            [XmlElement(ElementName = "Success", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public string Success { get; set; }
            [XmlElement(ElementName = "HotelDescriptiveContents", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public HotelDescriptiveContents HotelDescriptiveContents { get; set; }
            [XmlAttribute(AttributeName = "xmlns")]
            public string Xmlns { get; set; }
            [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
            public string Xsi { get; set; }
            [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
            public string SchemaLocation { get; set; }
            [XmlAttribute(AttributeName = "EchoToken")]
            public string EchoToken { get; set; }
            [XmlAttribute(AttributeName = "Target")]
            public string Target { get; set; }
            [XmlAttribute(AttributeName = "Version")]
            public string Version { get; set; }
        }

        [XmlRoot(ElementName = "Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public class Body
        {
            [XmlElement(ElementName = "OTA_HotelDescriptiveInfoRS", Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public OTA_HotelDescriptiveInfoRS OTA_HotelDescriptiveInfoRS { get; set; }
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
