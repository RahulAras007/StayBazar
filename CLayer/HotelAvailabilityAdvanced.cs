﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public partial class HotelAvailabilityAdvanced
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

            private OTA_HotelAvailRSWarnings warningsField;

            private OTA_HotelAvailRSHotelStays hotelStaysField;

            private OTA_HotelAvailRSRoomStay[] roomStaysField;

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
            public OTA_HotelAvailRSWarnings Warnings
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
            [System.Xml.Serialization.XmlArrayItemAttribute("RoomStay", IsNullable = false)]
            public OTA_HotelAvailRSRoomStay[] RoomStays
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
        public partial class OTA_HotelAvailRSWarnings
        {

            private OTA_HotelAvailRSWarningsWarning warningField;

            /// <remarks/>
            public OTA_HotelAvailRSWarningsWarning Warning
            {
                get
                {
                    return this.warningField;
                }
                set
                {
                    this.warningField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSWarningsWarning
        {

            private byte typeField;

            private string statusField;

            private string tagField;

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

            private string roomStayRPHField;

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
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfo
        {

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddress addressField;

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoRelativePosition relativePositionField;

            private string chainCodeField;

            private string hotelCodeField;

            private string hotelCityCodeField;

            private string hotelNameField;

            private string hotelCodeContextField;

            private string chainNameField;

            private byte areaIDField;

            private byte supplierIntegrationLevelField;

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
        public partial class OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddress
        {

            private string addressLineField;

            private string cityNameField;

            private OTA_HotelAvailRSHotelStaysHotelStayBasicPropertyInfoAddressCountryName countryNameField;

            /// <remarks/>
            public string AddressLine
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
        public partial class OTA_HotelAvailRSRoomStay
        {

            private OTA_HotelAvailRSRoomStayRoomTypes roomTypesField;

            private OTA_HotelAvailRSRoomStayRatePlans ratePlansField;

            private OTA_HotelAvailRSRoomStayRoomRates roomRatesField;

            private OTA_HotelAvailRSRoomStayGuestCounts guestCountsField;

            private OTA_HotelAvailRSRoomStayTimeSpan timeSpanField;

            private OTA_HotelAvailRSRoomStayTotal totalField;

            private OTA_HotelAvailRSRoomStayServiceRPHs serviceRPHsField;

            private string sourceOfBusinessField;

            private string availabilityStatusField;

            private string infoSourceField;

            private byte rPHField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRoomTypes RoomTypes
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
            public OTA_HotelAvailRSRoomStayRatePlans RatePlans
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
            public OTA_HotelAvailRSRoomStayRoomRates RoomRates
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
            public OTA_HotelAvailRSRoomStayGuestCounts GuestCounts
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
            public OTA_HotelAvailRSRoomStayTimeSpan TimeSpan
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
            public OTA_HotelAvailRSRoomStayTotal Total
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
            public OTA_HotelAvailRSRoomStayServiceRPHs ServiceRPHs
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
        public partial class OTA_HotelAvailRSRoomStayRoomTypes
        {

            private OTA_HotelAvailRSRoomStayRoomTypesRoomType roomTypeField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRoomTypesRoomType RoomType
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
        public partial class OTA_HotelAvailRSRoomStayRoomTypesRoomType
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
        public partial class OTA_HotelAvailRSRoomStayRatePlans
        {

            private OTA_HotelAvailRSRoomStayRatePlansRatePlan ratePlanField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRatePlansRatePlan RatePlan
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
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlan
        {

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanGuarantee guaranteeField;

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenalties cancelPenaltiesField;

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanCommission commissionField;

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanMealsIncluded mealsIncludedField;

            private string ratePlanCodeField;

            private string rateIndicatorField;

            private string availabilityStatusField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanGuarantee Guarantee
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
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenalties CancelPenalties
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
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanCommission Commission
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
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanMealsIncluded MealsIncluded
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanGuarantee
        {

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanGuaranteeGuaranteeAccepted[] guaranteesAcceptedField;

            private byte guaranteeCodeField;

            private string guaranteeTypeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("GuaranteeAccepted", IsNullable = false)]
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanGuaranteeGuaranteeAccepted[] GuaranteesAccepted
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
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanGuaranteeGuaranteeAccepted
        {

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanGuaranteeGuaranteeAcceptedPaymentCard paymentCardField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanGuaranteeGuaranteeAcceptedPaymentCard PaymentCard
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
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanGuaranteeGuaranteeAcceptedPaymentCard
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
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenalties
        {

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenalty cancelPenaltyField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenalty CancelPenalty
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenalty
        {

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyDeadline deadlineField;

            private OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyAmountPercent amountPercentField;

            private string policyCodeField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyDeadline Deadline
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
            public OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyAmountPercent AmountPercent
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyDeadline
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
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanCancelPenaltiesCancelPenaltyAmountPercent
        {

            private decimal amountField;

            private bool amountFieldSpecified;

            private string currencyCodeField;

            private byte nmbrOfNightsField;

            private bool nmbrOfNightsFieldSpecified;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanCommission
        {

            private string statusTypeField;

            private decimal percentField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStayRatePlansRatePlanMealsIncluded
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
        public partial class OTA_HotelAvailRSRoomStayRoomRates
        {

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRate roomRateField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRate RoomRate
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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRate
        {

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateRates ratesField;

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateRoomRateDescription roomRateDescriptionField;

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateTotal totalField;

            private string bookingCodeField;

            private string roomTypeCodeField;

            private byte numberOfUnitsField;

            private string ratePlanCodeField;

            private string availabilityStatusField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateRates Rates
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
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateRoomRateDescription RoomRateDescription
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
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateTotal Total
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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateRates
        {

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRate rateField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRate Rate
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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRate
        {

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRateBase baseField;

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRatePaymentPolicies paymentPoliciesField;

            private System.DateTime effectiveDateField;

            private System.DateTime expireDateField;

            private string rateTimeUnitField;

            private byte minLOSField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRateBase Base
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
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRatePaymentPolicies PaymentPolicies
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRateBase
        {

            private decimal amountAfterTaxField;

            private string currencyCodeField;

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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRatePaymentPolicies
        {

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment guaranteePaymentField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment GuaranteePayment
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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateRatesRatePaymentPoliciesGuaranteePayment
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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateRoomRateDescription
        {

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateRoomRateDescriptionText[] textField;

            private string nameField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Text")]
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateRoomRateDescriptionText[] Text
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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateRoomRateDescriptionText
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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateTotal
        {

            private OTA_HotelAvailRSRoomStayRoomRatesRoomRateTotalTax[] taxesField;

            private decimal amountAfterTaxField;

            private string currencyCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Tax", IsNullable = false)]
            public OTA_HotelAvailRSRoomStayRoomRatesRoomRateTotalTax[] Taxes
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
        public partial class OTA_HotelAvailRSRoomStayRoomRatesRoomRateTotalTax
        {

            private string typeField;

            private byte codeField;

            private decimal percentField;

            private byte chargeUnitField;

            private bool chargeUnitFieldSpecified;

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
        public partial class OTA_HotelAvailRSRoomStayGuestCounts
        {

            private OTA_HotelAvailRSRoomStayGuestCountsGuestCount guestCountField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayGuestCountsGuestCount GuestCount
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
        public partial class OTA_HotelAvailRSRoomStayGuestCountsGuestCount
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
        public partial class OTA_HotelAvailRSRoomStayTimeSpan
        {

            private OTA_HotelAvailRSRoomStayTimeSpanStartDateWindow startDateWindowField;

            private OTA_HotelAvailRSRoomStayTimeSpanEndDateWindow endDateWindowField;

            private System.DateTime startField;

            private System.DateTime endField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayTimeSpanStartDateWindow StartDateWindow
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
            public OTA_HotelAvailRSRoomStayTimeSpanEndDateWindow EndDateWindow
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
        public partial class OTA_HotelAvailRSRoomStayTimeSpanStartDateWindow
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
        public partial class OTA_HotelAvailRSRoomStayTimeSpanEndDateWindow
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
        public partial class OTA_HotelAvailRSRoomStayTotal
        {

            private decimal amountAfterTaxField;

            private string currencyCodeField;

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
        public partial class OTA_HotelAvailRSRoomStayServiceRPHs
        {

            private OTA_HotelAvailRSRoomStayServiceRPHsServiceRPH serviceRPHField;

            /// <remarks/>
            public OTA_HotelAvailRSRoomStayServiceRPHsServiceRPH ServiceRPH
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelAvailRSRoomStayServiceRPHsServiceRPH
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
    }
