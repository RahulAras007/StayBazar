using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public partial class HotelAvailability
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

            private OTA_HotelAvailRSWarning[] warningsField;

            private OTA_HotelAvailRSHotelStay[] hotelStaysField;

            private OTA_HotelAvailRSRoomStays roomStaysField;

            private OTA_HotelAvailRSService[] servicesField;

            private OTA_HotelAvailRSCurrencyConversions currencyConversionsField;

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
            [System.Xml.Serialization.XmlArrayItemAttribute("Warning", IsNullable = false)]
            public OTA_HotelAvailRSWarning[] Warnings
            {
                get
                {
                    return this.warningsField;
                }
                set
                {
                    this.warningsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("HotelStay", IsNullable = false)]
            public OTA_HotelAvailRSHotelStay[] HotelStays
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
            public OTA_HotelAvailRSCurrencyConversions CurrencyConversions
            {
                get
                {
                    return this.currencyConversionsField;
                }
                set
                {
                    this.currencyConversionsField = value;
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
        public partial class OTA_HotelAvailRSWarning
        {

            private byte typeField;

            private string statusField;

            private string tagField;

            private ushort codeField;

            private bool codeFieldSpecified;

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
            public string Status
            {
                get
                {
                    return this.statusField;
                }
                set
                {
                    this.statusField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Tag
            {
                get
                {
                    return this.tagField;
                }
                set
                {
                    this.tagField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort Code
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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool CodeSpecified
            {
                get
                {
                    return this.codeFieldSpecified;
                }
                set
                {
                    this.codeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStay
        {

            private OTA_HotelAvailRSHotelStayBasicPropertyInfo basicPropertyInfoField;

            private string roomStayRPHField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStayBasicPropertyInfo BasicPropertyInfo
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
            public string RoomStayRPH
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
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfo
        {

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessages vendorMessagesField;

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoAddress addressField;

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePosition relativePositionField;

            private string chainCodeField;

            private string hotelCodeField;

            private string hotelCityCodeField;

            private string hotelNameField;

            private string hotelCodeContextField;

            private string chainNameField;

            private byte areaIDField;

            private byte hotelSegmentCategoryCodeField;

            private bool hotelSegmentCategoryCodeFieldSpecified;

            private byte supplierIntegrationLevelField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessages VendorMessages
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
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoAddress Address
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
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePosition RelativePosition
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
            public byte HotelSegmentCategoryCode
            {
                get
                {
                    return this.hotelSegmentCategoryCodeField;
                }
                set
                {
                    this.hotelSegmentCategoryCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool HotelSegmentCategoryCodeSpecified
            {
                get
                {
                    return this.hotelSegmentCategoryCodeFieldSpecified;
                }
                set
                {
                    this.hotelSegmentCategoryCodeFieldSpecified = value;
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
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessages
        {

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessage vendorMessageField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessage VendorMessage
            {
                get
                {
                    return this.vendorMessageField;
                }
                set
                {
                    this.vendorMessageField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessage
        {

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSection subSectionField;

            private byte infoTypeField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSection SubSection
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
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSection
        {

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionText[] paragraphField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Text", IsNullable = false)]
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionText[] Paragraph
            {
                get
                {
                    return this.paragraphField;
                }
                set
                {
                    this.paragraphField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionText
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
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoAddress
        {

            private string[] addressLineField;

            private string cityNameField;

            private string postalCodeField;

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoAddressCountryName countryNameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("AddressLine")]
            public string[] AddressLine
            {
                get
                {
                    return this.addressLineField;
                }
                set
                {
                    this.addressLineField = value;
                }
            }

            /// <remarks/>
            public string CityName
            {
                get
                {
                    return this.cityNameField;
                }
                set
                {
                    this.cityNameField = value;
                }
            }

            /// <remarks/>
            public string PostalCode
            {
                get
                {
                    return this.postalCodeField;
                }
                set
                {
                    this.postalCodeField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoAddressCountryName CountryName
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
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoAddressCountryName
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
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePosition
        {

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportations transportationsField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportations Transportations
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
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportations
        {

            private OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation transportationField;

            /// <remarks/>
            public OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation Transportation
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
        public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation
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
        public partial class OTA_HotelAvailRSRoomStays
        {

            private OTA_HotelAvailRSRoomStaysRoomStay[] roomStayField;

            private string moreIndicatorField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("RoomStay")]
            public OTA_HotelAvailRSRoomStaysRoomStay[] RoomStay
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string MoreIndicator
            {
                get
                {
                    return this.moreIndicatorField;
                }
                set
                {
                    this.moreIndicatorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStay
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomTypes roomTypesField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlans ratePlansField;

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRates roomRatesField;

            private OTA_HotelAvailRSRoomStaysRoomStayGuestCounts guestCountsField;

            private OTA_HotelAvailRSRoomStaysRoomStayTimeSpan timeSpanField;

            private OTA_HotelAvailRSRoomStaysRoomStayTotal totalField;

            private OTA_HotelAvailRSRoomStaysRoomStayServiceRPH[] serviceRPHsField;

            private string sourceOfBusinessField;

            private string availabilityStatusField;

            private string infoSourceField;

            private byte rPHField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomTypes RoomTypes
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
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomTypes
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomTypesRoomType roomTypeField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomTypesRoomType RoomType
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomTypesRoomType
        {

            private byte isConvertedField;

            private string roomTypeField;

            private string roomTypeCodeField;

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

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuarantee[] guaranteeField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenalties cancelPenaltiesField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCommission commissionField;

            private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanMealsIncluded mealsIncludedField;

            private string ratePlanCodeField;

            private string rateIndicatorField;

            private string availabilityStatusField;

            private byte ratePlanTypeField;

            private bool ratePlanTypeFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Guarantee")]
            public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuarantee[] Guarantee
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
            public byte RatePlanType
            {
                get
                {
                    return this.ratePlanTypeField;
                }
                set
                {
                    this.ratePlanTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool RatePlanTypeSpecified
            {
                get
                {
                    return this.ratePlanTypeFieldSpecified;
                }
                set
                {
                    this.ratePlanTypeFieldSpecified = value;
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

            private bool guaranteeCodeFieldSpecified;

            private string guaranteeTypeField;

            private System.DateTime holdTimeField;

            private bool holdTimeFieldSpecified;

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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool GuaranteeCodeSpecified
            {
                get
                {
                    return this.guaranteeCodeFieldSpecified;
                }
                set
                {
                    this.guaranteeCodeFieldSpecified = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
            public System.DateTime HoldTime
            {
                get
                {
                    return this.holdTimeField;
                }
                set
                {
                    this.holdTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool HoldTimeSpecified
            {
                get
                {
                    return this.holdTimeFieldSpecified;
                }
                set
                {
                    this.holdTimeFieldSpecified = value;
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

            private byte bookingSourceAllowedIndField;

            private bool bookingSourceAllowedIndFieldSpecified;

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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte BookingSourceAllowedInd
            {
                get
                {
                    return this.bookingSourceAllowedIndField;
                }
                set
                {
                    this.bookingSourceAllowedIndField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool BookingSourceAllowedIndSpecified
            {
                get
                {
                    return this.bookingSourceAllowedIndFieldSpecified;
                }
                set
                {
                    this.bookingSourceAllowedIndFieldSpecified = value;
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

            private bool cancelPolicyIndicatorFieldSpecified;

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

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool CancelPolicyIndicatorSpecified
            {
                get
                {
                    return this.cancelPolicyIndicatorFieldSpecified;
                }
                set
                {
                    this.cancelPolicyIndicatorFieldSpecified = value;
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

            private string policyCodeField;

            private bool nonRefundableField;

            private bool nonRefundableFieldSpecified;

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

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool NonRefundableSpecified
            {
                get
                {
                    return this.nonRefundableFieldSpecified;
                }
                set
                {
                    this.nonRefundableFieldSpecified = value;
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

            private bool nmbrOfNightsFieldSpecified;

            private decimal amountField;

            private bool amountFieldSpecified;

            private string currencyCodeField;

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

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool NmbrOfNightsSpecified
            {
                get
                {
                    return this.nmbrOfNightsFieldSpecified;
                }
                set
                {
                    this.nmbrOfNightsFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AmountSpecified
            {
                get
                {
                    return this.amountFieldSpecified;
                }
                set
                {
                    this.amountFieldSpecified = value;
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
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCommission
        {

            private string statusTypeField;

            private decimal percentField;

            private bool percentFieldSpecified;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanMealsIncluded
        {

            private byte breakfastField;

            private byte mealPlanIndicatorField;

            private bool mealPlanIndicatorFieldSpecified;

            private byte mealPlanCodesField;

            private bool mealPlanCodesFieldSpecified;

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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MealPlanIndicator
            {
                get
                {
                    return this.mealPlanIndicatorField;
                }
                set
                {
                    this.mealPlanIndicatorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool MealPlanIndicatorSpecified
            {
                get
                {
                    return this.mealPlanIndicatorFieldSpecified;
                }
                set
                {
                    this.mealPlanIndicatorFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MealPlanCodes
            {
                get
                {
                    return this.mealPlanCodesField;
                }
                set
                {
                    this.mealPlanCodesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool MealPlanCodesSpecified
            {
                get
                {
                    return this.mealPlanCodesFieldSpecified;
                }
                set
                {
                    this.mealPlanCodesFieldSpecified = value;
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

            private string ratePlanCategoryField;

            private byte ratePlanTypeField;

            private bool ratePlanTypeFieldSpecified;

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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RatePlanCategory
            {
                get
                {
                    return this.ratePlanCategoryField;
                }
                set
                {
                    this.ratePlanCategoryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte RatePlanType
            {
                get
                {
                    return this.ratePlanTypeField;
                }
                set
                {
                    this.ratePlanTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool RatePlanTypeSpecified
            {
                get
                {
                    return this.ratePlanTypeFieldSpecified;
                }
                set
                {
                    this.ratePlanTypeFieldSpecified = value;
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

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateGuaranteePayment[] paymentPoliciesField;

            private System.DateTime effectiveDateField;

            private bool effectiveDateFieldSpecified;

            private System.DateTime expireDateField;

            private bool expireDateFieldSpecified;

            private string rateTimeUnitField;

            private byte minLOSField;

            private bool minLOSFieldSpecified;

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
            [System.Xml.Serialization.XmlArrayItemAttribute("GuaranteePayment", IsNullable = false)]
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateGuaranteePayment[] PaymentPolicies
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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool EffectiveDateSpecified
            {
                get
                {
                    return this.effectiveDateFieldSpecified;
                }
                set
                {
                    this.effectiveDateFieldSpecified = value;
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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ExpireDateSpecified
            {
                get
                {
                    return this.expireDateFieldSpecified;
                }
                set
                {
                    this.expireDateFieldSpecified = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MinLOS
            {
                get
                {
                    return this.minLOSField;
                }
                set
                {
                    this.minLOSField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool MinLOSSpecified
            {
                get
                {
                    return this.minLOSFieldSpecified;
                }
                set
                {
                    this.minLOSFieldSpecified = value;
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

            private bool amountBeforeTaxFieldSpecified;

            private string currencyCodeField;

            private decimal amountAfterTaxField;

            private bool amountAfterTaxFieldSpecified;

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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AmountBeforeTaxSpecified
            {
                get
                {
                    return this.amountBeforeTaxFieldSpecified;
                }
                set
                {
                    this.amountBeforeTaxFieldSpecified = value;
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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AmountAfterTaxSpecified
            {
                get
                {
                    return this.amountAfterTaxFieldSpecified;
                }
                set
                {
                    this.amountAfterTaxFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateGuaranteePayment
        {

            private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateGuaranteePaymentAmountPercent amountPercentField;

            private byte paymentCodeField;

            private bool paymentCodeFieldSpecified;

            private string guaranteeTypeField;

            private System.DateTime holdTimeField;

            private bool holdTimeFieldSpecified;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateGuaranteePaymentAmountPercent AmountPercent
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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool PaymentCodeSpecified
            {
                get
                {
                    return this.paymentCodeFieldSpecified;
                }
                set
                {
                    this.paymentCodeFieldSpecified = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
            public System.DateTime HoldTime
            {
                get
                {
                    return this.holdTimeField;
                }
                set
                {
                    this.holdTimeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool HoldTimeSpecified
            {
                get
                {
                    return this.holdTimeFieldSpecified;
                }
                set
                {
                    this.holdTimeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateGuaranteePaymentAmountPercent
        {

            private decimal amountField;

            private string currencyCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
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

            private bool amountBeforeTaxFieldSpecified;

            private decimal amountAfterTaxField;

            private string currencyCodeField;

            private byte additionalFeesExcludedIndicatorField;

            private bool additionalFeesExcludedIndicatorFieldSpecified;

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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AmountBeforeTaxSpecified
            {
                get
                {
                    return this.amountBeforeTaxFieldSpecified;
                }
                set
                {
                    this.amountBeforeTaxFieldSpecified = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte AdditionalFeesExcludedIndicator
            {
                get
                {
                    return this.additionalFeesExcludedIndicatorField;
                }
                set
                {
                    this.additionalFeesExcludedIndicatorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AdditionalFeesExcludedIndicatorSpecified
            {
                get
                {
                    return this.additionalFeesExcludedIndicatorFieldSpecified;
                }
                set
                {
                    this.additionalFeesExcludedIndicatorFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotalTax
        {

            private string typeField;

            private byte codeField;

            private decimal percentField;

            private bool percentFieldSpecified;

            private byte chargeUnitField;

            private bool chargeUnitFieldSpecified;

            private decimal amountField;

            private bool amountFieldSpecified;

            private string currencyCodeField;

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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Amount
            {
                get
                {
                    return this.amountField;
                }
                set
                {
                    this.amountField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AmountSpecified
            {
                get
                {
                    return this.amountFieldSpecified;
                }
                set
                {
                    this.amountFieldSpecified = value;
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

            private bool amountBeforeTaxFieldSpecified;

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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AmountBeforeTaxSpecified
            {
                get
                {
                    return this.amountBeforeTaxFieldSpecified;
                }
                set
                {
                    this.amountBeforeTaxFieldSpecified = value;
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

            private bool inclusiveFieldSpecified;

            private byte typeField;

            private string idField;

            private byte quantityField;

            private bool quantityFieldSpecified;

            private string servicePricingTypeField;

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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool InclusiveSpecified
            {
                get
                {
                    return this.inclusiveFieldSpecified;
                }
                set
                {
                    this.inclusiveFieldSpecified = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Quantity
            {
                get
                {
                    return this.quantityField;
                }
                set
                {
                    this.quantityField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool QuantitySpecified
            {
                get
                {
                    return this.quantityFieldSpecified;
                }
                set
                {
                    this.quantityFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ServicePricingType
            {
                get
                {
                    return this.servicePricingTypeField;
                }
                set
                {
                    this.servicePricingTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSCurrencyConversions
        {

            private OTA_HotelAvailRSCurrencyConversionsCurrencyConversion currencyConversionField;

            /// <remarks/>
            public OTA_HotelAvailRSCurrencyConversionsCurrencyConversion CurrencyConversion
            {
                get
                {
                    return this.currencyConversionField;
                }
                set
                {
                    this.currencyConversionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSCurrencyConversionsCurrencyConversion
        {

            private decimal rateConversionField;

            private string sourceCurrencyCodeField;

            private string requestedCurrencyCodeField;

            private byte decimalPlacesField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal RateConversion
            {
                get
                {
                    return this.rateConversionField;
                }
                set
                {
                    this.rateConversionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string SourceCurrencyCode
            {
                get
                {
                    return this.sourceCurrencyCodeField;
                }
                set
                {
                    this.sourceCurrencyCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RequestedCurrencyCode
            {
                get
                {
                    return this.requestedCurrencyCodeField;
                }
                set
                {
                    this.requestedCurrencyCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte DecimalPlaces
            {
                get
                {
                    return this.decimalPlacesField;
                }
                set
                {
                    this.decimalPlacesField = value;
                }
            }
        }


    }
    //public partial class HotelAvailability
    //{
    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    //    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", IsNullable = false)]
    //    public partial class Envelope
    //    {

    //        private EnvelopeHeader headerField;

    //        private EnvelopeBody bodyField;

    //        /// <remarks/>
    //        public EnvelopeHeader Header
    //        {
    //            get
    //            {
    //                return this.headerField;
    //            }
    //            set
    //            {
    //                this.headerField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public EnvelopeBody Body
    //        {
    //            get
    //            {
    //                return this.bodyField;
    //            }
    //            set
    //            {
    //                this.bodyField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    //    public partial class EnvelopeHeader
    //    {

    //        private string toField;

    //        private From fromField;

    //        private string actionField;

    //        private string messageIDField;

    //        private RelatesTo relatesToField;

    //        private Session sessionField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
    //        public string To
    //        {
    //            get
    //            {
    //                return this.toField;
    //            }
    //            set
    //            {
    //                this.toField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
    //        public From From
    //        {
    //            get
    //            {
    //                return this.fromField;
    //            }
    //            set
    //            {
    //                this.fromField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
    //        public string Action
    //        {
    //            get
    //            {
    //                return this.actionField;
    //            }
    //            set
    //            {
    //                this.actionField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
    //        public string MessageID
    //        {
    //            get
    //            {
    //                return this.messageIDField;
    //            }
    //            set
    //            {
    //                this.messageIDField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.w3.org/2005/08/addressing")]
    //        public RelatesTo RelatesTo
    //        {
    //            get
    //            {
    //                return this.relatesToField;
    //            }
    //            set
    //            {
    //                this.relatesToField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
    //        public Session Session
    //        {
    //            get
    //            {
    //                return this.sessionField;
    //            }
    //            set
    //            {
    //                this.sessionField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/08/addressing")]
    //    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
    //    public partial class From
    //    {

    //        private string addressField;

    //        /// <remarks/>
    //        public string Address
    //        {
    //            get
    //            {
    //                return this.addressField;
    //            }
    //            set
    //            {
    //                this.addressField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.w3.org/2005/08/addressing")]
    //    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.w3.org/2005/08/addressing", IsNullable = false)]
    //    public partial class RelatesTo
    //    {

    //        private string relationshipTypeField;

    //        private string valueField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RelationshipType
    //        {
    //            get
    //            {
    //                return this.relationshipTypeField;
    //            }
    //            set
    //            {
    //                this.relationshipTypeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlTextAttribute()]
    //        public string Value
    //        {
    //            get
    //            {
    //                return this.valueField;
    //            }
    //            set
    //            {
    //                this.valueField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/2010/06/Session_v3")]
    //    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://xml.amadeus.com/2010/06/Session_v3", IsNullable = false)]
    //    public partial class Session
    //    {

    //        private string sessionIdField;

    //        private byte sequenceNumberField;

    //        private string securityTokenField;

    //        private string transactionStatusCodeField;

    //        /// <remarks/>
    //        public string SessionId
    //        {
    //            get
    //            {
    //                return this.sessionIdField;
    //            }
    //            set
    //            {
    //                this.sessionIdField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public byte SequenceNumber
    //        {
    //            get
    //            {
    //                return this.sequenceNumberField;
    //            }
    //            set
    //            {
    //                this.sequenceNumberField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string SecurityToken
    //        {
    //            get
    //            {
    //                return this.securityTokenField;
    //            }
    //            set
    //            {
    //                this.securityTokenField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string TransactionStatusCode
    //        {
    //            get
    //            {
    //                return this.transactionStatusCodeField;
    //            }
    //            set
    //            {
    //                this.transactionStatusCodeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    //    public partial class EnvelopeBody
    //    {

    //        private OTA_HotelAvailRS oTA_HotelAvailRSField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //        public OTA_HotelAvailRS OTA_HotelAvailRS
    //        {
    //            get
    //            {
    //                return this.oTA_HotelAvailRSField;
    //            }
    //            set
    //            {
    //                this.oTA_HotelAvailRSField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.opentravel.org/OTA/2003/05", IsNullable = false)]
    //    public partial class OTA_HotelAvailRS
    //    {

    //        private object successField;

    //        private OTA_HotelAvailRSWarning[] warningsField;

    //        private OTA_HotelAvailRSHotelStay[] hotelStaysField;

    //        private OTA_HotelAvailRSRoomStays roomStaysField;

    //        private OTA_HotelAvailRSService[] servicesField;

    //        private OTA_HotelAvailRSCurrencyConversions currencyConversionsField;

    //        private string echoTokenField;

    //        private decimal versionField;

    //        private string primaryLangIDField;

    //        /// <remarks/>
    //        public object Success
    //        {
    //            get
    //            {
    //                return this.successField;
    //            }
    //            set
    //            {
    //                this.successField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlArrayItemAttribute("Warning", IsNullable = false)]
    //        public OTA_HotelAvailRSWarning[] Warnings
    //        {
    //            get
    //            {
    //                return this.warningsField;
    //            }
    //            set
    //            {
    //                this.warningsField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlArrayItemAttribute("HotelStay", IsNullable = false)]
    //        public OTA_HotelAvailRSHotelStay[] HotelStays
    //        {
    //            get
    //            {
    //                return this.hotelStaysField;
    //            }
    //            set
    //            {
    //                this.hotelStaysField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStays RoomStays
    //        {
    //            get
    //            {
    //                return this.roomStaysField;
    //            }
    //            set
    //            {
    //                this.roomStaysField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlArrayItemAttribute("Service", IsNullable = false)]
    //        public OTA_HotelAvailRSService[] Services
    //        {
    //            get
    //            {
    //                return this.servicesField;
    //            }
    //            set
    //            {
    //                this.servicesField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSCurrencyConversions CurrencyConversions
    //        {
    //            get
    //            {
    //                return this.currencyConversionsField;
    //            }
    //            set
    //            {
    //                this.currencyConversionsField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string EchoToken
    //        {
    //            get
    //            {
    //                return this.echoTokenField;
    //            }
    //            set
    //            {
    //                this.echoTokenField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public decimal Version
    //        {
    //            get
    //            {
    //                return this.versionField;
    //            }
    //            set
    //            {
    //                this.versionField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string PrimaryLangID
    //        {
    //            get
    //            {
    //                return this.primaryLangIDField;
    //            }
    //            set
    //            {
    //                this.primaryLangIDField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSWarning
    //    {

    //        private byte typeField;

    //        private string statusField;

    //        private string tagField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte Type
    //        {
    //            get
    //            {
    //                return this.typeField;
    //            }
    //            set
    //            {
    //                this.typeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string Status
    //        {
    //            get
    //            {
    //                return this.statusField;
    //            }
    //            set
    //            {
    //                this.statusField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string Tag
    //        {
    //            get
    //            {
    //                return this.tagField;
    //            }
    //            set
    //            {
    //                this.tagField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStay
    //    {

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfo basicPropertyInfoField;

    //        private string roomStayRPHField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfo BasicPropertyInfo
    //        {
    //            get
    //            {
    //                return this.basicPropertyInfoField;
    //            }
    //            set
    //            {
    //                this.basicPropertyInfoField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RoomStayRPH
    //        {
    //            get
    //            {
    //                return this.roomStayRPHField;
    //            }
    //            set
    //            {
    //                this.roomStayRPHField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfo
    //    {

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessages vendorMessagesField;

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoAddress addressField;

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePosition relativePositionField;

    //        private string chainCodeField;

    //        private string hotelCodeField;

    //        private string hotelCityCodeField;

    //        private string hotelNameField;

    //        private string hotelCodeContextField;

    //        private string chainNameField;

    //        private byte areaIDField;

    //        private byte hotelSegmentCategoryCodeField;

    //        private byte supplierIntegrationLevelField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessages VendorMessages
    //        {
    //            get
    //            {
    //                return this.vendorMessagesField;
    //            }
    //            set
    //            {
    //                this.vendorMessagesField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoAddress Address
    //        {
    //            get
    //            {
    //                return this.addressField;
    //            }
    //            set
    //            {
    //                this.addressField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePosition RelativePosition
    //        {
    //            get
    //            {
    //                return this.relativePositionField;
    //            }
    //            set
    //            {
    //                this.relativePositionField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string ChainCode
    //        {
    //            get
    //            {
    //                return this.chainCodeField;
    //            }
    //            set
    //            {
    //                this.chainCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string HotelCode
    //        {
    //            get
    //            {
    //                return this.hotelCodeField;
    //            }
    //            set
    //            {
    //                this.hotelCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string HotelCityCode
    //        {
    //            get
    //            {
    //                return this.hotelCityCodeField;
    //            }
    //            set
    //            {
    //                this.hotelCityCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string HotelName
    //        {
    //            get
    //            {
    //                return this.hotelNameField;
    //            }
    //            set
    //            {
    //                this.hotelNameField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string HotelCodeContext
    //        {
    //            get
    //            {
    //                return this.hotelCodeContextField;
    //            }
    //            set
    //            {
    //                this.hotelCodeContextField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string ChainName
    //        {
    //            get
    //            {
    //                return this.chainNameField;
    //            }
    //            set
    //            {
    //                this.chainNameField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte AreaID
    //        {
    //            get
    //            {
    //                return this.areaIDField;
    //            }
    //            set
    //            {
    //                this.areaIDField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte HotelSegmentCategoryCode
    //        {
    //            get
    //            {
    //                return this.hotelSegmentCategoryCodeField;
    //            }
    //            set
    //            {
    //                this.hotelSegmentCategoryCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte SupplierIntegrationLevel
    //        {
    //            get
    //            {
    //                return this.supplierIntegrationLevelField;
    //            }
    //            set
    //            {
    //                this.supplierIntegrationLevelField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessages
    //    {

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessage vendorMessageField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessage VendorMessage
    //        {
    //            get
    //            {
    //                return this.vendorMessageField;
    //            }
    //            set
    //            {
    //                this.vendorMessageField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessage
    //    {

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSection subSectionField;

    //        private byte infoTypeField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSection SubSection
    //        {
    //            get
    //            {
    //                return this.subSectionField;
    //            }
    //            set
    //            {
    //                this.subSectionField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte InfoType
    //        {
    //            get
    //            {
    //                return this.infoTypeField;
    //            }
    //            set
    //            {
    //                this.infoTypeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSection
    //    {

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionParagraph paragraphField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionParagraph Paragraph
    //        {
    //            get
    //            {
    //                return this.paragraphField;
    //            }
    //            set
    //            {
    //                this.paragraphField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionParagraph
    //    {

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionParagraphText textField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionParagraphText Text
    //        {
    //            get
    //            {
    //                return this.textField;
    //            }
    //            set
    //            {
    //                this.textField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoVendorMessagesVendorMessageSubSectionParagraphText
    //    {

    //        private byte formattedField;

    //        private string valueField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte Formatted
    //        {
    //            get
    //            {
    //                return this.formattedField;
    //            }
    //            set
    //            {
    //                this.formattedField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlTextAttribute()]
    //        public string Value
    //        {
    //            get
    //            {
    //                return this.valueField;
    //            }
    //            set
    //            {
    //                this.valueField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoAddress
    //    {

    //        private string[] addressLineField;

    //        private string cityNameField;

    //        private string postalCodeField;

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoAddressCountryName countryNameField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute("AddressLine")]
    //        public string[] AddressLine
    //        {
    //            get
    //            {
    //                return this.addressLineField;
    //            }
    //            set
    //            {
    //                this.addressLineField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string CityName
    //        {
    //            get
    //            {
    //                return this.cityNameField;
    //            }
    //            set
    //            {
    //                this.cityNameField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public string PostalCode
    //        {
    //            get
    //            {
    //                return this.postalCodeField;
    //            }
    //            set
    //            {
    //                this.postalCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoAddressCountryName CountryName
    //        {
    //            get
    //            {
    //                return this.countryNameField;
    //            }
    //            set
    //            {
    //                this.countryNameField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoAddressCountryName
    //    {

    //        private string codeField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string Code
    //        {
    //            get
    //            {
    //                return this.codeField;
    //            }
    //            set
    //            {
    //                this.codeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePosition
    //    {

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportations transportationsField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportations Transportations
    //        {
    //            get
    //            {
    //                return this.transportationsField;
    //            }
    //            set
    //            {
    //                this.transportationsField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportations
    //    {

    //        private OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation transportationField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation Transportation
    //        {
    //            get
    //            {
    //                return this.transportationField;
    //            }
    //            set
    //            {
    //                this.transportationField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSHotelStayBasicPropertyInfoRelativePositionTransportationsTransportation
    //    {

    //        private byte transportationCodeField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte TransportationCode
    //        {
    //            get
    //            {
    //                return this.transportationCodeField;
    //            }
    //            set
    //            {
    //                this.transportationCodeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStays
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStay[] roomStayField;

    //        private string moreIndicatorField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute("RoomStay")]
    //        public OTA_HotelAvailRSRoomStaysRoomStay[] RoomStay
    //        {
    //            get
    //            {
    //                return this.roomStayField;
    //            }
    //            set
    //            {
    //                this.roomStayField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string MoreIndicator
    //        {
    //            get
    //            {
    //                return this.moreIndicatorField;
    //            }
    //            set
    //            {
    //                this.moreIndicatorField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStay
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomTypes roomTypesField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRatePlans ratePlansField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRates roomRatesField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayGuestCounts guestCountsField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayTimeSpan timeSpanField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayServiceRPHs serviceRPHsField;

    //        private string sourceOfBusinessField;

    //        private string availabilityStatusField;

    //        private string infoSourceField;

    //        private byte rPHField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomTypes RoomTypes
    //        {
    //            get
    //            {
    //                return this.roomTypesField;
    //            }
    //            set
    //            {
    //                this.roomTypesField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRatePlans RatePlans
    //        {
    //            get
    //            {
    //                return this.ratePlansField;
    //            }
    //            set
    //            {
    //                this.ratePlansField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRates RoomRates
    //        {
    //            get
    //            {
    //                return this.roomRatesField;
    //            }
    //            set
    //            {
    //                this.roomRatesField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayGuestCounts GuestCounts
    //        {
    //            get
    //            {
    //                return this.guestCountsField;
    //            }
    //            set
    //            {
    //                this.guestCountsField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayTimeSpan TimeSpan
    //        {
    //            get
    //            {
    //                return this.timeSpanField;
    //            }
    //            set
    //            {
    //                this.timeSpanField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayServiceRPHs ServiceRPHs
    //        {
    //            get
    //            {
    //                return this.serviceRPHsField;
    //            }
    //            set
    //            {
    //                this.serviceRPHsField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string SourceOfBusiness
    //        {
    //            get
    //            {
    //                return this.sourceOfBusinessField;
    //            }
    //            set
    //            {
    //                this.sourceOfBusinessField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string AvailabilityStatus
    //        {
    //            get
    //            {
    //                return this.availabilityStatusField;
    //            }
    //            set
    //            {
    //                this.availabilityStatusField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string InfoSource
    //        {
    //            get
    //            {
    //                return this.infoSourceField;
    //            }
    //            set
    //            {
    //                this.infoSourceField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte RPH
    //        {
    //            get
    //            {
    //                return this.rPHField;
    //            }
    //            set
    //            {
    //                this.rPHField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomTypes
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomTypesRoomType roomTypeField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomTypesRoomType RoomType
    //        {
    //            get
    //            {
    //                return this.roomTypeField;
    //            }
    //            set
    //            {
    //                this.roomTypeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomTypesRoomType
    //    {

    //        private byte isConvertedField;

    //        private string roomTypeField;

    //        private string roomTypeCodeField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte IsConverted
    //        {
    //            get
    //            {
    //                return this.isConvertedField;
    //            }
    //            set
    //            {
    //                this.isConvertedField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RoomType
    //        {
    //            get
    //            {
    //                return this.roomTypeField;
    //            }
    //            set
    //            {
    //                this.roomTypeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RoomTypeCode
    //        {
    //            get
    //            {
    //                return this.roomTypeCodeField;
    //            }
    //            set
    //            {
    //                this.roomTypeCodeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlans
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlan ratePlanField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlan RatePlan
    //        {
    //            get
    //            {
    //                return this.ratePlanField;
    //            }
    //            set
    //            {
    //                this.ratePlanField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlan
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuarantee guaranteeField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenalties cancelPenaltiesField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCommission commissionField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanMealsIncluded mealsIncludedField;

    //        private string ratePlanCodeField;

    //        private string rateIndicatorField;

    //        private string availabilityStatusField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuarantee Guarantee
    //        {
    //            get
    //            {
    //                return this.guaranteeField;
    //            }
    //            set
    //            {
    //                this.guaranteeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenalties CancelPenalties
    //        {
    //            get
    //            {
    //                return this.cancelPenaltiesField;
    //            }
    //            set
    //            {
    //                this.cancelPenaltiesField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCommission Commission
    //        {
    //            get
    //            {
    //                return this.commissionField;
    //            }
    //            set
    //            {
    //                this.commissionField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanMealsIncluded MealsIncluded
    //        {
    //            get
    //            {
    //                return this.mealsIncludedField;
    //            }
    //            set
    //            {
    //                this.mealsIncludedField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RatePlanCode
    //        {
    //            get
    //            {
    //                return this.ratePlanCodeField;
    //            }
    //            set
    //            {
    //                this.ratePlanCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RateIndicator
    //        {
    //            get
    //            {
    //                return this.rateIndicatorField;
    //            }
    //            set
    //            {
    //                this.rateIndicatorField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string AvailabilityStatus
    //        {
    //            get
    //            {
    //                return this.availabilityStatusField;
    //            }
    //            set
    //            {
    //                this.availabilityStatusField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanGuarantee
    //    {

    //        private byte guaranteeCodeField;

    //        private string guaranteeTypeField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte GuaranteeCode
    //        {
    //            get
    //            {
    //                return this.guaranteeCodeField;
    //            }
    //            set
    //            {
    //                this.guaranteeCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string GuaranteeType
    //        {
    //            get
    //            {
    //                return this.guaranteeTypeField;
    //            }
    //            set
    //            {
    //                this.guaranteeTypeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCancelPenalties
    //    {

    //        private byte cancelPolicyIndicatorField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte CancelPolicyIndicator
    //        {
    //            get
    //            {
    //                return this.cancelPolicyIndicatorField;
    //            }
    //            set
    //            {
    //                this.cancelPolicyIndicatorField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanCommission
    //    {

    //        private string statusTypeField;

    //        private decimal percentField;

    //        private bool percentFieldSpecified;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string StatusType
    //        {
    //            get
    //            {
    //                return this.statusTypeField;
    //            }
    //            set
    //            {
    //                this.statusTypeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public decimal Percent
    //        {
    //            get
    //            {
    //                return this.percentField;
    //            }
    //            set
    //            {
    //                this.percentField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlIgnoreAttribute()]
    //        public bool PercentSpecified
    //        {
    //            get
    //            {
    //                return this.percentFieldSpecified;
    //            }
    //            set
    //            {
    //                this.percentFieldSpecified = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRatePlansRatePlanMealsIncluded
    //    {

    //        private byte breakfastField;

    //        private byte mealPlanIndicatorField;

    //        private byte mealPlanCodesField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte Breakfast
    //        {
    //            get
    //            {
    //                return this.breakfastField;
    //            }
    //            set
    //            {
    //                this.breakfastField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte MealPlanIndicator
    //        {
    //            get
    //            {
    //                return this.mealPlanIndicatorField;
    //            }
    //            set
    //            {
    //                this.mealPlanIndicatorField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte MealPlanCodes
    //        {
    //            get
    //            {
    //                return this.mealPlanCodesField;
    //            }
    //            set
    //            {
    //                this.mealPlanCodesField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRates
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRate roomRateField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRate RoomRate
    //        {
    //            get
    //            {
    //                return this.roomRateField;
    //            }
    //            set
    //            {
    //                this.roomRateField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRate
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRates ratesField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescription roomRateDescriptionField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotal totalField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateServiceRPHs serviceRPHsField;

    //        private string bookingCodeField;

    //        private string roomTypeCodeField;

    //        private byte numberOfUnitsField;

    //        private string ratePlanCodeField;

    //        private string availabilityStatusField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRates Rates
    //        {
    //            get
    //            {
    //                return this.ratesField;
    //            }
    //            set
    //            {
    //                this.ratesField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescription RoomRateDescription
    //        {
    //            get
    //            {
    //                return this.roomRateDescriptionField;
    //            }
    //            set
    //            {
    //                this.roomRateDescriptionField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotal Total
    //        {
    //            get
    //            {
    //                return this.totalField;
    //            }
    //            set
    //            {
    //                this.totalField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateServiceRPHs ServiceRPHs
    //        {
    //            get
    //            {
    //                return this.serviceRPHsField;
    //            }
    //            set
    //            {
    //                this.serviceRPHsField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string BookingCode
    //        {
    //            get
    //            {
    //                return this.bookingCodeField;
    //            }
    //            set
    //            {
    //                this.bookingCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RoomTypeCode
    //        {
    //            get
    //            {
    //                return this.roomTypeCodeField;
    //            }
    //            set
    //            {
    //                this.roomTypeCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte NumberOfUnits
    //        {
    //            get
    //            {
    //                return this.numberOfUnitsField;
    //            }
    //            set
    //            {
    //                this.numberOfUnitsField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RatePlanCode
    //        {
    //            get
    //            {
    //                return this.ratePlanCodeField;
    //            }
    //            set
    //            {
    //                this.ratePlanCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string AvailabilityStatus
    //        {
    //            get
    //            {
    //                return this.availabilityStatusField;
    //            }
    //            set
    //            {
    //                this.availabilityStatusField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRates
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRate rateField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRate Rate
    //        {
    //            get
    //            {
    //                return this.rateField;
    //            }
    //            set
    //            {
    //                this.rateField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRate
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateBase baseField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPolicies paymentPoliciesField;

    //        private string rateTimeUnitField;

    //        private System.DateTime effectiveDateField;

    //        private bool effectiveDateFieldSpecified;

    //        private System.DateTime expireDateField;

    //        private bool expireDateFieldSpecified;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateBase Base
    //        {
    //            get
    //            {
    //                return this.baseField;
    //            }
    //            set
    //            {
    //                this.baseField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPolicies PaymentPolicies
    //        {
    //            get
    //            {
    //                return this.paymentPoliciesField;
    //            }
    //            set
    //            {
    //                this.paymentPoliciesField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RateTimeUnit
    //        {
    //            get
    //            {
    //                return this.rateTimeUnitField;
    //            }
    //            set
    //            {
    //                this.rateTimeUnitField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
    //        public System.DateTime EffectiveDate
    //        {
    //            get
    //            {
    //                return this.effectiveDateField;
    //            }
    //            set
    //            {
    //                this.effectiveDateField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlIgnoreAttribute()]
    //        public bool EffectiveDateSpecified
    //        {
    //            get
    //            {
    //                return this.effectiveDateFieldSpecified;
    //            }
    //            set
    //            {
    //                this.effectiveDateFieldSpecified = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
    //        public System.DateTime ExpireDate
    //        {
    //            get
    //            {
    //                return this.expireDateField;
    //            }
    //            set
    //            {
    //                this.expireDateField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlIgnoreAttribute()]
    //        public bool ExpireDateSpecified
    //        {
    //            get
    //            {
    //                return this.expireDateFieldSpecified;
    //            }
    //            set
    //            {
    //                this.expireDateFieldSpecified = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRateBase
    //    {

    //        private decimal amountBeforeTaxField;

    //        private string currencyCodeField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public decimal AmountBeforeTax
    //        {
    //            get
    //            {
    //                return this.amountBeforeTaxField;
    //            }
    //            set
    //            {
    //                this.amountBeforeTaxField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string CurrencyCode
    //        {
    //            get
    //            {
    //                return this.currencyCodeField;
    //            }
    //            set
    //            {
    //                this.currencyCodeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPolicies
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment guaranteePaymentField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment GuaranteePayment
    //        {
    //            get
    //            {
    //                return this.guaranteePaymentField;
    //            }
    //            set
    //            {
    //                this.guaranteePaymentField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment
    //    {

    //        private byte paymentCodeField;

    //        private string guaranteeTypeField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte PaymentCode
    //        {
    //            get
    //            {
    //                return this.paymentCodeField;
    //            }
    //            set
    //            {
    //                this.paymentCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string GuaranteeType
    //        {
    //            get
    //            {
    //                return this.guaranteeTypeField;
    //            }
    //            set
    //            {
    //                this.guaranteeTypeField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescription
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText[] textField;

    //        private string nameField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlElementAttribute("Text")]
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText[] Text
    //        {
    //            get
    //            {
    //                return this.textField;
    //            }
    //            set
    //            {
    //                this.textField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string Name
    //        {
    //            get
    //            {
    //                return this.nameField;
    //            }
    //            set
    //            {
    //                this.nameField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText
    //    {

    //        private byte formattedField;

    //        private string valueField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte Formatted
    //        {
    //            get
    //            {
    //                return this.formattedField;
    //            }
    //            set
    //            {
    //                this.formattedField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlTextAttribute()]
    //        public string Value
    //        {
    //            get
    //            {
    //                return this.valueField;
    //            }
    //            set
    //            {
    //                this.valueField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateTotal
    //    {

    //        private decimal amountAfterTaxField;

    //        private bool amountAfterTaxFieldSpecified;

    //        private string currencyCodeField;

    //        private decimal amountBeforeTaxField;

    //        private bool amountBeforeTaxFieldSpecified;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public decimal AmountAfterTax
    //        {
    //            get
    //            {
    //                return this.amountAfterTaxField;
    //            }
    //            set
    //            {
    //                this.amountAfterTaxField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlIgnoreAttribute()]
    //        public bool AmountAfterTaxSpecified
    //        {
    //            get
    //            {
    //                return this.amountAfterTaxFieldSpecified;
    //            }
    //            set
    //            {
    //                this.amountAfterTaxFieldSpecified = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string CurrencyCode
    //        {
    //            get
    //            {
    //                return this.currencyCodeField;
    //            }
    //            set
    //            {
    //                this.currencyCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public decimal AmountBeforeTax
    //        {
    //            get
    //            {
    //                return this.amountBeforeTaxField;
    //            }
    //            set
    //            {
    //                this.amountBeforeTaxField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlIgnoreAttribute()]
    //        public bool AmountBeforeTaxSpecified
    //        {
    //            get
    //            {
    //                return this.amountBeforeTaxFieldSpecified;
    //            }
    //            set
    //            {
    //                this.amountBeforeTaxFieldSpecified = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateServiceRPHs
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateServiceRPHsServiceRPH serviceRPHField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateServiceRPHsServiceRPH ServiceRPH
    //        {
    //            get
    //            {
    //                return this.serviceRPHField;
    //            }
    //            set
    //            {
    //                this.serviceRPHField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateServiceRPHsServiceRPH
    //    {

    //        private byte rPHField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte RPH
    //        {
    //            get
    //            {
    //                return this.rPHField;
    //            }
    //            set
    //            {
    //                this.rPHField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayGuestCounts
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayGuestCountsGuestCount guestCountField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayGuestCountsGuestCount GuestCount
    //        {
    //            get
    //            {
    //                return this.guestCountField;
    //            }
    //            set
    //            {
    //                this.guestCountField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayGuestCountsGuestCount
    //    {

    //        private byte ageQualifyingCodeField;

    //        private byte countField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte AgeQualifyingCode
    //        {
    //            get
    //            {
    //                return this.ageQualifyingCodeField;
    //            }
    //            set
    //            {
    //                this.ageQualifyingCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte Count
    //        {
    //            get
    //            {
    //                return this.countField;
    //            }
    //            set
    //            {
    //                this.countField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayTimeSpan
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayTimeSpanStartDateWindow startDateWindowField;

    //        private OTA_HotelAvailRSRoomStaysRoomStayTimeSpanEndDateWindow endDateWindowField;

    //        private System.DateTime startField;

    //        private System.DateTime endField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayTimeSpanStartDateWindow StartDateWindow
    //        {
    //            get
    //            {
    //                return this.startDateWindowField;
    //            }
    //            set
    //            {
    //                this.startDateWindowField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayTimeSpanEndDateWindow EndDateWindow
    //        {
    //            get
    //            {
    //                return this.endDateWindowField;
    //            }
    //            set
    //            {
    //                this.endDateWindowField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
    //        public System.DateTime Start
    //        {
    //            get
    //            {
    //                return this.startField;
    //            }
    //            set
    //            {
    //                this.startField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "date")]
    //        public System.DateTime End
    //        {
    //            get
    //            {
    //                return this.endField;
    //            }
    //            set
    //            {
    //                this.endField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayTimeSpanStartDateWindow
    //    {

    //        private string dOWField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string DOW
    //        {
    //            get
    //            {
    //                return this.dOWField;
    //            }
    //            set
    //            {
    //                this.dOWField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayTimeSpanEndDateWindow
    //    {

    //        private string dOWField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string DOW
    //        {
    //            get
    //            {
    //                return this.dOWField;
    //            }
    //            set
    //            {
    //                this.dOWField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayServiceRPHs
    //    {

    //        private OTA_HotelAvailRSRoomStaysRoomStayServiceRPHsServiceRPH serviceRPHField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSRoomStaysRoomStayServiceRPHsServiceRPH ServiceRPH
    //        {
    //            get
    //            {
    //                return this.serviceRPHField;
    //            }
    //            set
    //            {
    //                this.serviceRPHField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSRoomStaysRoomStayServiceRPHsServiceRPH
    //    {

    //        private byte rPHField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte RPH
    //        {
    //            get
    //            {
    //                return this.rPHField;
    //            }
    //            set
    //            {
    //                this.rPHField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSService
    //    {

    //        private byte serviceRPHField;

    //        private string serviceInventoryCodeField;

    //        private byte inclusiveField;

    //        private bool inclusiveFieldSpecified;

    //        private byte typeField;

    //        private string idField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte ServiceRPH
    //        {
    //            get
    //            {
    //                return this.serviceRPHField;
    //            }
    //            set
    //            {
    //                this.serviceRPHField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string ServiceInventoryCode
    //        {
    //            get
    //            {
    //                return this.serviceInventoryCodeField;
    //            }
    //            set
    //            {
    //                this.serviceInventoryCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte Inclusive
    //        {
    //            get
    //            {
    //                return this.inclusiveField;
    //            }
    //            set
    //            {
    //                this.inclusiveField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlIgnoreAttribute()]
    //        public bool InclusiveSpecified
    //        {
    //            get
    //            {
    //                return this.inclusiveFieldSpecified;
    //            }
    //            set
    //            {
    //                this.inclusiveFieldSpecified = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte Type
    //        {
    //            get
    //            {
    //                return this.typeField;
    //            }
    //            set
    //            {
    //                this.typeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string ID
    //        {
    //            get
    //            {
    //                return this.idField;
    //            }
    //            set
    //            {
    //                this.idField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSCurrencyConversions
    //    {

    //        private OTA_HotelAvailRSCurrencyConversionsCurrencyConversion currencyConversionField;

    //        /// <remarks/>
    //        public OTA_HotelAvailRSCurrencyConversionsCurrencyConversion CurrencyConversion
    //        {
    //            get
    //            {
    //                return this.currencyConversionField;
    //            }
    //            set
    //            {
    //                this.currencyConversionField = value;
    //            }
    //        }
    //    }

    //    /// <remarks/>
    //    [System.SerializableAttribute()]
    //    [System.ComponentModel.DesignerCategoryAttribute("code")]
    //    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
    //    public partial class OTA_HotelAvailRSCurrencyConversionsCurrencyConversion
    //    {

    //        private decimal rateConversionField;

    //        private string sourceCurrencyCodeField;

    //        private string requestedCurrencyCodeField;

    //        private byte decimalPlacesField;

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public decimal RateConversion
    //        {
    //            get
    //            {
    //                return this.rateConversionField;
    //            }
    //            set
    //            {
    //                this.rateConversionField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string SourceCurrencyCode
    //        {
    //            get
    //            {
    //                return this.sourceCurrencyCodeField;
    //            }
    //            set
    //            {
    //                this.sourceCurrencyCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public string RequestedCurrencyCode
    //        {
    //            get
    //            {
    //                return this.requestedCurrencyCodeField;
    //            }
    //            set
    //            {
    //                this.requestedCurrencyCodeField = value;
    //            }
    //        }

    //        /// <remarks/>
    //        [System.Xml.Serialization.XmlAttributeAttribute()]
    //        public byte DecimalPlaces
    //        {
    //            get
    //            {
    //                return this.decimalPlacesField;
    //            }
    //            set
    //            {
    //                this.decimalPlacesField = value;
    //            }
    //        }
    //    }



    //}
}
