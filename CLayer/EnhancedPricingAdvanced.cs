using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class EnhancedPricingAdvanced
    {
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
        public partial class Envelope
        {

            private EnvelopeHeader headerField;

            private EnvelopeBody bodyField;

            /// <remarks/>
            public EnvelopeHeader Header
            {
                get
                {
                    return this.headerField;
                }
                set
                {
                    this.headerField = value;
                }
            }

            /// <remarks/>
            public EnvelopeBody Body
            {
                get
                {
                    return this.bodyField;
                }
                set
                {
                    this.bodyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public partial class EnvelopeHeader
        {

            private string toField;

            private From fromField;

            private string actionField;

            private string messageIDField;

            private RelatesTo relatesToField;

            private Session sessionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
            public string To
            {
                get
                {
                    return this.toField;
                }
                set
                {
                    this.toField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
            public From From
            {
                get
                {
                    return this.fromField;
                }
                set
                {
                    this.fromField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
            public string Action
            {
                get
                {
                    return this.actionField;
                }
                set
                {
                    this.actionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
            public string MessageID
            {
                get
                {
                    return this.messageIDField;
                }
                set
                {
                    this.messageIDField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
            public RelatesTo RelatesTo
            {
                get
                {
                    return this.relatesToField;
                }
                set
                {
                    this.relatesToField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
            public Session Session
            {
                get
                {
                    return this.sessionField;
                }
                set
                {
                    this.sessionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/08/addressing")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
        public partial class From
        {

            private string addressField;

            /// <remarks/>
            public string Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/08/addressing")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
        public partial class RelatesTo
        {

            private string relationshipTypeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RelationshipType
            {
                get
                {
                    return this.relationshipTypeField;
                }
                set
                {
                    this.relationshipTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://xml.amadeus.com/2010/06/Session_v3", IsNullable = false)]
        public partial class Session
        {

            private string sessionIdField;

            private byte sequenceNumberField;

            private string securityTokenField;

            private string transactionStatusCodeField;

            /// <remarks/>
            public string SessionId
            {
                get
                {
                    return this.sessionIdField;
                }
                set
                {
                    this.sessionIdField = value;
                }
            }

            /// <remarks/>
            public byte SequenceNumber
            {
                get
                {
                    return this.sequenceNumberField;
                }
                set
                {
                    this.sequenceNumberField = value;
                }
            }

            /// <remarks/>
            public string SecurityToken
            {
                get
                {
                    return this.securityTokenField;
                }
                set
                {
                    this.securityTokenField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string TransactionStatusCode
            {
                get
                {
                    return this.transactionStatusCodeField;
                }
                set
                {
                    this.transactionStatusCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
        public partial class EnvelopeBody
        {

            private OTA_HotelAvailRS oTA_HotelAvailRSField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public OTA_HotelAvailRS OTA_HotelAvailRS
            {
                get
                {
                    return this.oTA_HotelAvailRSField;
                }
                set
                {
                    this.oTA_HotelAvailRSField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.opentravel.org/OTA/2003/05", IsNullable = false)]
        public partial class OTA_HotelAvailRS
        {

            private object successField;

            private OTA_HotelAvailRSProfileInfo[] profilesField;

            private OTA_HotelAvailRSHotelStays hotelStaysField;

            private OTA_HotelAvailRSRoomStays roomStaysField;

            private OTA_HotelAvailRSService[] servicesField;

            private string echoTokenField;

            private decimal versionField;

            private string primaryLangIDField;

            /// <remarks/>
            public object Success
            {
                get
                {
                    return this.successField;
                }
                set
                {
                    this.successField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("ProfileInfo", IsNullable = false)]
            public OTA_HotelAvailRSProfileInfo[] Profiles
            {
                get
                {
                    return this.profilesField;
                }
                set
                {
                    this.profilesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSHotelStays HotelStays
            {
                get
                {
                    return this.hotelStaysField;
                }
                set
                {
                    this.hotelStaysField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStays RoomStays
            {
                get
                {
                    return this.roomStaysField;
                }
                set
                {
                    this.roomStaysField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Service", IsNullable = false)]
            public OTA_HotelAvailRSService[] Services
            {
                get
                {
                    return this.servicesField;
                }
                set
                {
                    this.servicesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string EchoToken
            {
                get
                {
                    return this.echoTokenField;
                }
                set
                {
                    this.echoTokenField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string PrimaryLangID
            {
                get
                {
                    return this.primaryLangIDField;
                }
                set
                {
                    this.primaryLangIDField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSProfileInfo
        {

            private OTA_HotelAvailRSProfileInfoProfile profileField;

            /// <remarks/>
            public OTA_HotelAvailRSProfileInfoProfile Profile
            {
                get
                {
                    return this.profileField;
                }
                set
                {
                    this.profileField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSProfileInfoProfile
        {

            private OTA_HotelAvailRSProfileInfoProfileCustomer customerField;

            /// <remarks/>
            public OTA_HotelAvailRSProfileInfoProfileCustomer Customer
            {
                get
                {
                    return this.customerField;
                }
                set
                {
                    this.customerField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSProfileInfoProfileCustomer
        {

            private OTA_HotelAvailRSProfileInfoProfileCustomerCustLoyalty custLoyaltyField;

            /// <remarks/>
            public OTA_HotelAvailRSProfileInfoProfileCustomerCustLoyalty CustLoyalty
            {
                get
                {
                    return this.custLoyaltyField;
                }
                set
                {
                    this.custLoyaltyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSProfileInfoProfileCustomerCustLoyalty
        {

            private byte travelSectorField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte TravelSector
            {
                get
                {
                    return this.travelSectorField;
                }
                set
                {
                    this.travelSectorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStays
        {

            private OTA_HotelAvailRSHotelStaysHotelStay hotelStayField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStay HotelStay
            {
                get
                {
                    return this.hotelStayField;
                }
                set
                {
                    this.hotelStayField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStay
        {

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfo basicPropertyInfoField;

            private byte roomStayRPHField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfo BasicPropertyInfo
            {
                get
                {
                    return this.basicPropertyInfoField;
                }
                set
                {
                    this.basicPropertyInfoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte RoomStayRPH
            {
                get
                {
                    return this.roomStayRPHField;
                }
                set
                {
                    this.roomStayRPHField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfo
        {

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessage[] vendorMessagesField;

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddress addressField;

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePosition relativePositionField;

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoHotelAmenity[] hotelAmenityField;

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoPolicy policyField;

            private string chainCodeField;

            private string hotelCodeField;

            private string hotelCityCodeField;

            private string hotelNameField;

            private string hotelCodeContextField;

            private string chainNameField;

            private byte areaIDField;

            private byte supplierIntegrationLevelField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("VendorMessage", IsNullable = false)]
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessage[] VendorMessages
            {
                get
                {
                    return this.vendorMessagesField;
                }
                set
                {
                    this.vendorMessagesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddress Address
            {
                get
                {
                    return this.addressField;
                }
                set
                {
                    this.addressField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePosition RelativePosition
            {
                get
                {
                    return this.relativePositionField;
                }
                set
                {
                    this.relativePositionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("HotelAmenity")]
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoHotelAmenity[] HotelAmenity
            {
                get
                {
                    return this.hotelAmenityField;
                }
                set
                {
                    this.hotelAmenityField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoPolicy Policy
            {
                get
                {
                    return this.policyField;
                }
                set
                {
                    this.policyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ChainCode
            {
                get
                {
                    return this.chainCodeField;
                }
                set
                {
                    this.chainCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HotelCode
            {
                get
                {
                    return this.hotelCodeField;
                }
                set
                {
                    this.hotelCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HotelCityCode
            {
                get
                {
                    return this.hotelCityCodeField;
                }
                set
                {
                    this.hotelCityCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HotelName
            {
                get
                {
                    return this.hotelNameField;
                }
                set
                {
                    this.hotelNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HotelCodeContext
            {
                get
                {
                    return this.hotelCodeContextField;
                }
                set
                {
                    this.hotelCodeContextField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ChainName
            {
                get
                {
                    return this.chainNameField;
                }
                set
                {
                    this.chainNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte AreaID
            {
                get
                {
                    return this.areaIDField;
                }
                set
                {
                    this.areaIDField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte SupplierIntegrationLevel
            {
                get
                {
                    return this.supplierIntegrationLevelField;
                }
                set
                {
                    this.supplierIntegrationLevelField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessage
        {

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessageParagraph[] subSectionField;

            private byte infoTypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Paragraph", IsNullable = false)]
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessageParagraph[] SubSection
            {
                get
                {
                    return this.subSectionField;
                }
                set
                {
                    this.subSectionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte InfoType
            {
                get
                {
                    return this.infoTypeField;
                }
                set
                {
                    this.infoTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessageParagraph
        {

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessageParagraphText textField;

            private string nameField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessageParagraphText Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoVendorMessageParagraphText
        {

            private byte formattedField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Formatted
            {
                get
                {
                    return this.formattedField;
                }
                set
                {
                    this.formattedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddress
        {

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddressCountryName countryNameField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddressCountryName CountryName
            {
                get
                {
                    return this.countryNameField;
                }
                set
                {
                    this.countryNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddressCountryName
        {

            private string codeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Code
            {
                get
                {
                    return this.codeField;
                }
                set
                {
                    this.codeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePosition
        {

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePositionTransportations transportationsField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePositionTransportations Transportations
            {
                get
                {
                    return this.transportationsField;
                }
                set
                {
                    this.transportationsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePositionTransportations
        {

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation transportationField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation Transportation
            {
                get
                {
                    return this.transportationField;
                }
                set
                {
                    this.transportationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation
        {

            private byte transportationCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte TransportationCode
            {
                get
                {
                    return this.transportationCodeField;
                }
                set
                {
                    this.transportationCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoHotelAmenity
        {

            private byte codeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Code
            {
                get
                {
                    return this.codeField;
                }
                set
                {
                    this.codeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoPolicy
        {

            private System.DateTime checkInTimeField;

            private System.DateTime checkOutTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
            public System.DateTime CheckInTime
            {
                get
                {
                    return this.checkInTimeField;
                }
                set
                {
                    this.checkInTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
            public System.DateTime CheckOutTime
            {
                get
                {
                    return this.checkOutTimeField;
                }
                set
                {
                    this.checkOutTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStays
        {

            private OTA_HotelAvailRSRoomStaysRoomStay roomStayField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStay RoomStay
            {
                get
                {
                    return this.roomStayField;
                }
                set
                {
                    this.roomStayField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStay
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomType[] roomTypesField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlans ratePlansField;

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRates roomRatesField;

            private OTA_HotelAvailRSRoomStaysRoomStayGuestCounts guestCountsField;

            private OTA_HotelAvailRSRoomStaysRoomStayTimeSpan timeSpanField;

            private OTA_HotelAvailRSRoomStaysRoomStayTotal totalField;

            private OTA_HotelAvailRSRoomStaysRoomStayServiceRPH[] serviceRPHsField;

            private string marketCodeField;

            private string sourceOfBusinessField;

            private byte isAlternateField;

            private string availabilityStatusField;

            private string infoSourceField;

            private byte rPHField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("RoomType", IsNullable = false)]
            public OTA_HotelAvailRSRoomStaysRoomStayRoomType[] RoomTypes
            {
                get
                {
                    return this.roomTypesField;
                }
                set
                {
                    this.roomTypesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlans RatePlans
            {
                get
                {
                    return this.ratePlansField;
                }
                set
                {
                    this.ratePlansField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRates RoomRates
            {
                get
                {
                    return this.roomRatesField;
                }
                set
                {
                    this.roomRatesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayGuestCounts GuestCounts
            {
                get
                {
                    return this.guestCountsField;
                }
                set
                {
                    this.guestCountsField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayTimeSpan TimeSpan
            {
                get
                {
                    return this.timeSpanField;
                }
                set
                {
                    this.timeSpanField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayTotal Total
            {
                get
                {
                    return this.totalField;
                }
                set
                {
                    this.totalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("ServiceRPH", IsNullable = false)]
            public OTA_HotelAvailRSRoomStaysRoomStayServiceRPH[] ServiceRPHs
            {
                get
                {
                    return this.serviceRPHsField;
                }
                set
                {
                    this.serviceRPHsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string MarketCode
            {
                get
                {
                    return this.marketCodeField;
                }
                set
                {
                    this.marketCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string SourceOfBusiness
            {
                get
                {
                    return this.sourceOfBusinessField;
                }
                set
                {
                    this.sourceOfBusinessField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte IsAlternate
            {
                get
                {
                    return this.isAlternateField;
                }
                set
                {
                    this.isAlternateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string AvailabilityStatus
            {
                get
                {
                    return this.availabilityStatusField;
                }
                set
                {
                    this.availabilityStatusField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string InfoSource
            {
                get
                {
                    return this.infoSourceField;
                }
                set
                {
                    this.infoSourceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte RPH
            {
                get
                {
                    return this.rPHField;
                }
                set
                {
                    this.rPHField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomType
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomTypeOccupancy occupancyField;

            private byte isConvertedField;

            private bool isConvertedFieldSpecified;

            private string roomTypeField;

            private string roomTypeCodeField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomTypeOccupancy Occupancy
            {
                get
                {
                    return this.occupancyField;
                }
                set
                {
                    this.occupancyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte IsConverted
            {
                get
                {
                    return this.isConvertedField;
                }
                set
                {
                    this.isConvertedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool IsConvertedSpecified
            {
                get
                {
                    return this.isConvertedFieldSpecified;
                }
                set
                {
                    this.isConvertedFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RoomType
            {
                get
                {
                    return this.roomTypeField;
                }
                set
                {
                    this.roomTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RoomTypeCode
            {
                get
                {
                    return this.roomTypeCodeField;
                }
                set
                {
                    this.roomTypeCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomTypeOccupancy
        {

            private byte maxOccupancyField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MaxOccupancy
            {
                get
                {
                    return this.maxOccupancyField;
                }
                set
                {
                    this.maxOccupancyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlans
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlan ratePlanField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlan RatePlan
            {
                get
                {
                    return this.ratePlanField;
                }
                set
                {
                    this.ratePlanField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlan
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuarantee guaranteeField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenalties cancelPenaltiesField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanRatePlanDescription ratePlanDescriptionField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCommission commissionField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanMealsIncluded mealsIncludedField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanAdditionalDetail[] additionalDetailsField;

            private string ratePlanCodeField;

            private string rateIndicatorField;

            private string ratePlanNameField;

            private string availabilityStatusField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuarantee Guarantee
            {
                get
                {
                    return this.guaranteeField;
                }
                set
                {
                    this.guaranteeField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenalties CancelPenalties
            {
                get
                {
                    return this.cancelPenaltiesField;
                }
                set
                {
                    this.cancelPenaltiesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanRatePlanDescription RatePlanDescription
            {
                get
                {
                    return this.ratePlanDescriptionField;
                }
                set
                {
                    this.ratePlanDescriptionField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCommission Commission
            {
                get
                {
                    return this.commissionField;
                }
                set
                {
                    this.commissionField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanMealsIncluded MealsIncluded
            {
                get
                {
                    return this.mealsIncludedField;
                }
                set
                {
                    this.mealsIncludedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("AdditionalDetail", IsNullable = false)]
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanAdditionalDetail[] AdditionalDetails
            {
                get
                {
                    return this.additionalDetailsField;
                }
                set
                {
                    this.additionalDetailsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RatePlanCode
            {
                get
                {
                    return this.ratePlanCodeField;
                }
                set
                {
                    this.ratePlanCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RateIndicator
            {
                get
                {
                    return this.rateIndicatorField;
                }
                set
                {
                    this.rateIndicatorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RatePlanName
            {
                get
                {
                    return this.ratePlanNameField;
                }
                set
                {
                    this.ratePlanNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string AvailabilityStatus
            {
                get
                {
                    return this.availabilityStatusField;
                }
                set
                {
                    this.availabilityStatusField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuarantee
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuaranteeGuaranteeAccepted[] guaranteesAcceptedField;

            private byte guaranteeCodeField;

            private string guaranteeTypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("GuaranteeAccepted", IsNullable = false)]
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuaranteeGuaranteeAccepted[] GuaranteesAccepted
            {
                get
                {
                    return this.guaranteesAcceptedField;
                }
                set
                {
                    this.guaranteesAcceptedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte GuaranteeCode
            {
                get
                {
                    return this.guaranteeCodeField;
                }
                set
                {
                    this.guaranteeCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string GuaranteeType
            {
                get
                {
                    return this.guaranteeTypeField;
                }
                set
                {
                    this.guaranteeTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuaranteeGuaranteeAccepted
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuaranteeGuaranteeAcceptedPaymentCard paymentCardField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuaranteeGuaranteeAcceptedPaymentCard PaymentCard
            {
                get
                {
                    return this.paymentCardField;
                }
                set
                {
                    this.paymentCardField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuaranteeGuaranteeAcceptedPaymentCard
        {

            private string cardCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CardCode
            {
                get
                {
                    return this.cardCodeField;
                }
                set
                {
                    this.cardCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenalties
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenalty cancelPenaltyField;

            private byte cancelPolicyIndicatorField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenalty CancelPenalty
            {
                get
                {
                    return this.cancelPenaltyField;
                }
                set
                {
                    this.cancelPenaltyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte CancelPolicyIndicator
            {
                get
                {
                    return this.cancelPolicyIndicatorField;
                }
                set
                {
                    this.cancelPolicyIndicatorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenalty
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyDeadline deadlineField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyAmountPercent amountPercentField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyPenaltyDescription penaltyDescriptionField;

            private string policyCodeField;

            private bool nonRefundableField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyDeadline Deadline
            {
                get
                {
                    return this.deadlineField;
                }
                set
                {
                    this.deadlineField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyAmountPercent AmountPercent
            {
                get
                {
                    return this.amountPercentField;
                }
                set
                {
                    this.amountPercentField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyPenaltyDescription PenaltyDescription
            {
                get
                {
                    return this.penaltyDescriptionField;
                }
                set
                {
                    this.penaltyDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string PolicyCode
            {
                get
                {
                    return this.policyCodeField;
                }
                set
                {
                    this.policyCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool NonRefundable
            {
                get
                {
                    return this.nonRefundableField;
                }
                set
                {
                    this.nonRefundableField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyDeadline
        {

            private System.DateTime absoluteDeadlineField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime AbsoluteDeadline
            {
                get
                {
                    return this.absoluteDeadlineField;
                }
                set
                {
                    this.absoluteDeadlineField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyAmountPercent
        {

            private byte nmbrOfNightsField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte NmbrOfNights
            {
                get
                {
                    return this.nmbrOfNightsField;
                }
                set
                {
                    this.nmbrOfNightsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyPenaltyDescription
        {

            private string textField;

            /// <remarks/>
            public string Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanRatePlanDescription
        {

            private string textField;

            /// <remarks/>
            public string Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCommission
        {

            private string statusTypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string StatusType
            {
                get
                {
                    return this.statusTypeField;
                }
                set
                {
                    this.statusTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanMealsIncluded
        {

            private byte breakfastField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Breakfast
            {
                get
                {
                    return this.breakfastField;
                }
                set
                {
                    this.breakfastField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanAdditionalDetail
        {

            private string[] detailDescriptionField;

            private byte typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Text", IsNullable = false)]
            public string[] DetailDescription
            {
                get
                {
                    return this.detailDescriptionField;
                }
                set
                {
                    this.detailDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRates
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRate roomRateField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRate RoomRate
            {
                get
                {
                    return this.roomRateField;
                }
                set
                {
                    this.roomRateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRate
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRates ratesField;

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescription roomRateDescriptionField;

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotal totalField;

            private string bookingCodeField;

            private string roomTypeCodeField;

            private byte numberOfUnitsField;

            private string ratePlanCodeField;

            private string availabilityStatusField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRates Rates
            {
                get
                {
                    return this.ratesField;
                }
                set
                {
                    this.ratesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescription RoomRateDescription
            {
                get
                {
                    return this.roomRateDescriptionField;
                }
                set
                {
                    this.roomRateDescriptionField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotal Total
            {
                get
                {
                    return this.totalField;
                }
                set
                {
                    this.totalField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string BookingCode
            {
                get
                {
                    return this.bookingCodeField;
                }
                set
                {
                    this.bookingCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RoomTypeCode
            {
                get
                {
                    return this.roomTypeCodeField;
                }
                set
                {
                    this.roomTypeCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte NumberOfUnits
            {
                get
                {
                    return this.numberOfUnitsField;
                }
                set
                {
                    this.numberOfUnitsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RatePlanCode
            {
                get
                {
                    return this.ratePlanCodeField;
                }
                set
                {
                    this.ratePlanCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string AvailabilityStatus
            {
                get
                {
                    return this.availabilityStatusField;
                }
                set
                {
                    this.availabilityStatusField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRates
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRate rateField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRate Rate
            {
                get
                {
                    return this.rateField;
                }
                set
                {
                    this.rateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRate
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateBase baseField;

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPolicies paymentPoliciesField;

            private System.DateTime effectiveDateField;

            private System.DateTime expireDateField;

            private string rateTimeUnitField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateBase Base
            {
                get
                {
                    return this.baseField;
                }
                set
                {
                    this.baseField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPolicies PaymentPolicies
            {
                get
                {
                    return this.paymentPoliciesField;
                }
                set
                {
                    this.paymentPoliciesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
            public System.DateTime EffectiveDate
            {
                get
                {
                    return this.effectiveDateField;
                }
                set
                {
                    this.effectiveDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
            public System.DateTime ExpireDate
            {
                get
                {
                    return this.expireDateField;
                }
                set
                {
                    this.expireDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RateTimeUnit
            {
                get
                {
                    return this.rateTimeUnitField;
                }
                set
                {
                    this.rateTimeUnitField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateBase
        {

            private decimal amountBeforeTaxField;

            private string currencyCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal AmountBeforeTax
            {
                get
                {
                    return this.amountBeforeTaxField;
                }
                set
                {
                    this.amountBeforeTaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CurrencyCode
            {
                get
                {
                    return this.currencyCodeField;
                }
                set
                {
                    this.currencyCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPolicies
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment guaranteePaymentField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment GuaranteePayment
            {
                get
                {
                    return this.guaranteePaymentField;
                }
                set
                {
                    this.guaranteePaymentField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment
        {

            private byte paymentCodeField;

            private string guaranteeTypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte PaymentCode
            {
                get
                {
                    return this.paymentCodeField;
                }
                set
                {
                    this.paymentCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string GuaranteeType
            {
                get
                {
                    return this.guaranteeTypeField;
                }
                set
                {
                    this.guaranteeTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescription
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText[] textField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Text")]
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText
        {

            private byte formattedField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Formatted
            {
                get
                {
                    return this.formattedField;
                }
                set
                {
                    this.formattedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotal
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTax[] taxesField;

            private decimal amountBeforeTaxField;

            private decimal amountAfterTaxField;

            private string currencyCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Tax", IsNullable = false)]
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTax[] Taxes
            {
                get
                {
                    return this.taxesField;
                }
                set
                {
                    this.taxesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal AmountBeforeTax
            {
                get
                {
                    return this.amountBeforeTaxField;
                }
                set
                {
                    this.amountBeforeTaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal AmountAfterTax
            {
                get
                {
                    return this.amountAfterTaxField;
                }
                set
                {
                    this.amountAfterTaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CurrencyCode
            {
                get
                {
                    return this.currencyCodeField;
                }
                set
                {
                    this.currencyCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTax
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTaxTaxDescription taxDescriptionField;

            private string typeField;

            private byte codeField;

            private decimal percentField;

            private bool percentFieldSpecified;

            private byte chargeUnitField;

            private bool chargeUnitFieldSpecified;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTaxTaxDescription TaxDescription
            {
                get
                {
                    return this.taxDescriptionField;
                }
                set
                {
                    this.taxDescriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Code
            {
                get
                {
                    return this.codeField;
                }
                set
                {
                    this.codeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Percent
            {
                get
                {
                    return this.percentField;
                }
                set
                {
                    this.percentField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool PercentSpecified
            {
                get
                {
                    return this.percentFieldSpecified;
                }
                set
                {
                    this.percentFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte ChargeUnit
            {
                get
                {
                    return this.chargeUnitField;
                }
                set
                {
                    this.chargeUnitField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ChargeUnitSpecified
            {
                get
                {
                    return this.chargeUnitFieldSpecified;
                }
                set
                {
                    this.chargeUnitFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTaxTaxDescription
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTaxTaxDescriptionText[] textField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Text")]
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTaxTaxDescriptionText[] Text
            {
                get
                {
                    return this.textField;
                }
                set
                {
                    this.textField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Name
            {
                get
                {
                    return this.nameField;
                }
                set
                {
                    this.nameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTaxTaxDescriptionText
        {

            private byte formattedField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Formatted
            {
                get
                {
                    return this.formattedField;
                }
                set
                {
                    this.formattedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayGuestCounts
        {

            private OTA_HotelAvailRSRoomStaysRoomStayGuestCountsGuestCount guestCountField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayGuestCountsGuestCount GuestCount
            {
                get
                {
                    return this.guestCountField;
                }
                set
                {
                    this.guestCountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayGuestCountsGuestCount
        {

            private byte ageQualifyingCodeField;

            private byte countField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte AgeQualifyingCode
            {
                get
                {
                    return this.ageQualifyingCodeField;
                }
                set
                {
                    this.ageQualifyingCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Count
            {
                get
                {
                    return this.countField;
                }
                set
                {
                    this.countField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayTimeSpan
        {

            private OTA_HotelAvailRSRoomStaysRoomStayTimeSpanStartDateWindow startDateWindowField;

            private OTA_HotelAvailRSRoomStaysRoomStayTimeSpanEndDateWindow endDateWindowField;

            private System.DateTime startField;

            private System.DateTime endField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayTimeSpanStartDateWindow StartDateWindow
            {
                get
                {
                    return this.startDateWindowField;
                }
                set
                {
                    this.startDateWindowField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayTimeSpanEndDateWindow EndDateWindow
            {
                get
                {
                    return this.endDateWindowField;
                }
                set
                {
                    this.endDateWindowField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
            public System.DateTime Start
            {
                get
                {
                    return this.startField;
                }
                set
                {
                    this.startField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
            public System.DateTime End
            {
                get
                {
                    return this.endField;
                }
                set
                {
                    this.endField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayTimeSpanStartDateWindow
        {

            private string dOWField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string DOW
            {
                get
                {
                    return this.dOWField;
                }
                set
                {
                    this.dOWField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayTimeSpanEndDateWindow
        {

            private string dOWField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string DOW
            {
                get
                {
                    return this.dOWField;
                }
                set
                {
                    this.dOWField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayTotal
        {

            private decimal amountBeforeTaxField;

            private decimal amountAfterTaxField;

            private string currencyCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal AmountBeforeTax
            {
                get
                {
                    return this.amountBeforeTaxField;
                }
                set
                {
                    this.amountBeforeTaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal AmountAfterTax
            {
                get
                {
                    return this.amountAfterTaxField;
                }
                set
                {
                    this.amountAfterTaxField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string CurrencyCode
            {
                get
                {
                    return this.currencyCodeField;
                }
                set
                {
                    this.currencyCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayServiceRPH
        {

            private byte rPHField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte RPH
            {
                get
                {
                    return this.rPHField;
                }
                set
                {
                    this.rPHField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSService
        {

            private byte serviceRPHField;

            private string serviceInventoryCodeField;

            private byte inclusiveField;

            private byte typeField;

            private string idField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte ServiceRPH
            {
                get
                {
                    return this.serviceRPHField;
                }
                set
                {
                    this.serviceRPHField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ServiceInventoryCode
            {
                get
                {
                    return this.serviceInventoryCodeField;
                }
                set
                {
                    this.serviceInventoryCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Inclusive
            {
                get
                {
                    return this.inclusiveField;
                }
                set
                {
                    this.inclusiveField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Type
            {
                get
                {
                    return this.typeField;
                }
                set
                {
                    this.typeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ID
            {
                get
                {
                    return this.idField;
                }
                set
                {
                    this.idField = value;
                }
            }
        }



    }
}
