using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CLayer
{
    public partial class  GDSBookingDetailsAdv
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

            private OTA_HotelDescriptiveInfoRS oTA_HotelDescriptiveInfoRSField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://www.opentravel.org/OTA/2003/05")]
            public OTA_HotelDescriptiveInfoRS OTA_HotelDescriptiveInfoRS
            {
                get
                {
                    return this.oTA_HotelDescriptiveInfoRSField;
                }
                set
                {
                    this.oTA_HotelDescriptiveInfoRSField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.opentravel.org/OTA/2003/05", IsNullable = false)]
        public partial class OTA_HotelDescriptiveInfoRS
        {

            private object successField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContents hotelDescriptiveContentsField;

            private string echoTokenField;

            private string targetField;

            private decimal versionField;

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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContents HotelDescriptiveContents
            {
                get
                {
                    return this.hotelDescriptiveContentsField;
                }
                set
                {
                    this.hotelDescriptiveContentsField = value;
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
            public string Target
            {
                get
                {
                    return this.targetField;
                }
                set
                {
                    this.targetField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContents
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContent hotelDescriptiveContentField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContent HotelDescriptiveContent
            {
                get
                {
                    return this.hotelDescriptiveContentField;
                }
                set
                {
                    this.hotelDescriptiveContentField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContent
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfo hotelInfoField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfo facilityInfoField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicy[] policiesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfo areaInfoField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfo affiliationInfoField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfos contactInfosField;

            private string currencyCodeField;

            private string languageCodeField;

            private byte unitOfMeasureField;

            private string timeZoneField;

            private string chainCodeField;

            private string hotelCodeField;

            private string hotelNameField;

            private string hotelCodeContextField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfo HotelInfo
            {
                get
                {
                    return this.hotelInfoField;
                }
                set
                {
                    this.hotelInfoField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfo FacilityInfo
            {
                get
                {
                    return this.facilityInfoField;
                }
                set
                {
                    this.facilityInfoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Policy", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicy[] Policies
            {
                get
                {
                    return this.policiesField;
                }
                set
                {
                    this.policiesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfo AreaInfo
            {
                get
                {
                    return this.areaInfoField;
                }
                set
                {
                    this.areaInfoField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfo AffiliationInfo
            {
                get
                {
                    return this.affiliationInfoField;
                }
                set
                {
                    this.affiliationInfoField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfos ContactInfos
            {
                get
                {
                    return this.contactInfosField;
                }
                set
                {
                    this.contactInfosField = value;
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
            public string LanguageCode
            {
                get
                {
                    return this.languageCodeField;
                }
                set
                {
                    this.languageCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte UnitOfMeasure
            {
                get
                {
                    return this.unitOfMeasureField;
                }
                set
                {
                    this.unitOfMeasureField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string TimeZone
            {
                get
                {
                    return this.timeZoneField;
                }
                set
                {
                    this.timeZoneField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfo
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoHotelName hotelNameField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodes categoryCodesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptions descriptionsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoPosition positionField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoService[] servicesField;

            private ushort whenBuiltField;

            private byte hotelStatusCodeField;

            private byte iSO9000CertifiedIndField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoHotelName HotelName
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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodes CategoryCodes
            {
                get
                {
                    return this.categoryCodesField;
                }
                set
                {
                    this.categoryCodesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptions Descriptions
            {
                get
                {
                    return this.descriptionsField;
                }
                set
                {
                    this.descriptionsField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoPosition Position
            {
                get
                {
                    return this.positionField;
                }
                set
                {
                    this.positionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Service", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoService[] Services
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
            public ushort WhenBuilt
            {
                get
                {
                    return this.whenBuiltField;
                }
                set
                {
                    this.whenBuiltField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte HotelStatusCode
            {
                get
                {
                    return this.hotelStatusCodeField;
                }
                set
                {
                    this.hotelStatusCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte ISO9000CertifiedInd
            {
                get
                {
                    return this.iSO9000CertifiedIndField;
                }
                set
                {
                    this.iSO9000CertifiedIndField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoHotelName
        {

            private string hotelShortNameField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string HotelShortName
            {
                get
                {
                    return this.hotelShortNameField;
                }
                set
                {
                    this.hotelShortNameField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodes
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesLocationCategory[] locationCategoryField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesSegmentCategory segmentCategoryField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesHotelCategory[] hotelCategoryField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfo[] guestRoomInfoField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("LocationCategory")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesLocationCategory[] LocationCategory
            {
                get
                {
                    return this.locationCategoryField;
                }
                set
                {
                    this.locationCategoryField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesSegmentCategory SegmentCategory
            {
                get
                {
                    return this.segmentCategoryField;
                }
                set
                {
                    this.segmentCategoryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("HotelCategory")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesHotelCategory[] HotelCategory
            {
                get
                {
                    return this.hotelCategoryField;
                }
                set
                {
                    this.hotelCategoryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("GuestRoomInfo")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfo[] GuestRoomInfo
            {
                get
                {
                    return this.guestRoomInfoField;
                }
                set
                {
                    this.guestRoomInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesLocationCategory
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesSegmentCategory
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesHotelCategory
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfo
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescription[] multimediaDescriptionsField;

            private byte codeField;

            private byte quantityField;

            private bool quantityFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("MultimediaDescription", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescription[] MultimediaDescriptions
            {
                get
                {
                    return this.multimediaDescriptionsField;
                }
                set
                {
                    this.multimediaDescriptionsField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItem[] imageItemsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItems textItemsField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("ImageItem", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItem[] ImageItems
            {
                get
                {
                    return this.imageItemsField;
                }
                set
                {
                    this.imageItemsField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItems TextItems
            {
                get
                {
                    return this.textItemsField;
                }
                set
                {
                    this.textItemsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItemImageFormat[] imageFormatField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItemDescription descriptionField;

            private byte categoryField;

            private System.DateTime lastModifyDateTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("ImageFormat")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItemImageFormat[] ImageFormat
            {
                get
                {
                    return this.imageFormatField;
                }
                set
                {
                    this.imageFormatField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItemDescription Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Category
            {
                get
                {
                    return this.categoryField;
                }
                set
                {
                    this.categoryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public System.DateTime LastModifyDateTime
            {
                get
                {
                    return this.lastModifyDateTimeField;
                }
                set
                {
                    this.lastModifyDateTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItemImageFormat
        {

            private string uRLField;

            private ushort widthField;

            private ushort heightField;

            private string recordIDField;

            private byte formatField;

            private string fileNameField;

            private uint fileSizeField;

            private bool isOriginalIndicatorField;

            private bool isOriginalIndicatorFieldSpecified;

            private string dimensionCategoryField;

            /// <remarks/>
            public string URL
            {
                get
                {
                    return this.uRLField;
                }
                set
                {
                    this.uRLField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort Width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort Height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RecordID
            {
                get
                {
                    return this.recordIDField;
                }
                set
                {
                    this.recordIDField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Format
            {
                get
                {
                    return this.formatField;
                }
                set
                {
                    this.formatField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string FileName
            {
                get
                {
                    return this.fileNameField;
                }
                set
                {
                    this.fileNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public uint FileSize
            {
                get
                {
                    return this.fileSizeField;
                }
                set
                {
                    this.fileSizeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool IsOriginalIndicator
            {
                get
                {
                    return this.isOriginalIndicatorField;
                }
                set
                {
                    this.isOriginalIndicatorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool IsOriginalIndicatorSpecified
            {
                get
                {
                    return this.isOriginalIndicatorFieldSpecified;
                }
                set
                {
                    this.isOriginalIndicatorFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string DimensionCategory
            {
                get
                {
                    return this.dimensionCategoryField;
                }
                set
                {
                    this.dimensionCategoryField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionImageItemDescription
        {

            private string languageField;

            private string captionField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Caption
            {
                get
                {
                    return this.captionField;
                }
                set
                {
                    this.captionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsDescription[] textItemField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Description", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsDescription[] TextItem
            {
                get
                {
                    return this.textItemField;
                }
                set
                {
                    this.textItemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsDescription
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptions
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsRenovation renovationField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescription[] multimediaDescriptionsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsRenovation Renovation
            {
                get
                {
                    return this.renovationField;
                }
                set
                {
                    this.renovationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("MultimediaDescription", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescription[] MultimediaDescriptions
            {
                get
                {
                    return this.multimediaDescriptionsField;
                }
                set
                {
                    this.multimediaDescriptionsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsRenovation
        {

            private ushort renovationCompletionDateField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public ushort RenovationCompletionDate
            {
                get
                {
                    return this.renovationCompletionDateField;
                }
                set
                {
                    this.renovationCompletionDateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItems textItemsField;

            private byte infoCodeField;

            private bool infoCodeFieldSpecified;

            private byte additionalDetailCodeField;

            private bool additionalDetailCodeFieldSpecified;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItems TextItems
            {
                get
                {
                    return this.textItemsField;
                }
                set
                {
                    this.textItemsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte InfoCode
            {
                get
                {
                    return this.infoCodeField;
                }
                set
                {
                    this.infoCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool InfoCodeSpecified
            {
                get
                {
                    return this.infoCodeFieldSpecified;
                }
                set
                {
                    this.infoCodeFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte AdditionalDetailCode
            {
                get
                {
                    return this.additionalDetailCodeField;
                }
                set
                {
                    this.additionalDetailCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AdditionalDetailCodeSpecified
            {
                get
                {
                    return this.additionalDetailCodeFieldSpecified;
                }
                set
                {
                    this.additionalDetailCodeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsDescription[] textItemField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Description", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsDescription[] TextItem
            {
                get
                {
                    return this.textItemField;
                }
                set
                {
                    this.textItemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsDescription
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoPosition
        {

            private decimal latitudeField;

            private decimal longitudeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Latitude
            {
                get
                {
                    return this.latitudeField;
                }
                set
                {
                    this.latitudeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public decimal Longitude
            {
                get
                {
                    return this.longitudeField;
                }
                set
                {
                    this.longitudeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoService
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptions multimediaDescriptionsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceFeature[] featuresField;

            private byte codeField;

            private bool codeFieldSpecified;

            private byte proximityCodeField;

            private bool proximityCodeFieldSpecified;

            private byte businessServiceCodeField;

            private bool businessServiceCodeFieldSpecified;

            private byte includedField;

            private bool includedFieldSpecified;

            private byte mealPlanCodeField;

            private bool mealPlanCodeFieldSpecified;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptions MultimediaDescriptions
            {
                get
                {
                    return this.multimediaDescriptionsField;
                }
                set
                {
                    this.multimediaDescriptionsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Feature", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceFeature[] Features
            {
                get
                {
                    return this.featuresField;
                }
                set
                {
                    this.featuresField = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte ProximityCode
            {
                get
                {
                    return this.proximityCodeField;
                }
                set
                {
                    this.proximityCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ProximityCodeSpecified
            {
                get
                {
                    return this.proximityCodeFieldSpecified;
                }
                set
                {
                    this.proximityCodeFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte BusinessServiceCode
            {
                get
                {
                    return this.businessServiceCodeField;
                }
                set
                {
                    this.businessServiceCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool BusinessServiceCodeSpecified
            {
                get
                {
                    return this.businessServiceCodeFieldSpecified;
                }
                set
                {
                    this.businessServiceCodeFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Included
            {
                get
                {
                    return this.includedField;
                }
                set
                {
                    this.includedField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool IncludedSpecified
            {
                get
                {
                    return this.includedFieldSpecified;
                }
                set
                {
                    this.includedFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MealPlanCode
            {
                get
                {
                    return this.mealPlanCodeField;
                }
                set
                {
                    this.mealPlanCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool MealPlanCodeSpecified
            {
                get
                {
                    return this.mealPlanCodeFieldSpecified;
                }
                set
                {
                    this.mealPlanCodeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptions
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescription multimediaDescriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescription MultimediaDescription
            {
                get
                {
                    return this.multimediaDescriptionField;
                }
                set
                {
                    this.multimediaDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescriptionTextItems textItemsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescriptionTextItems TextItems
            {
                get
                {
                    return this.textItemsField;
                }
                set
                {
                    this.textItemsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescriptionTextItemsDescription[] textItemField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Description", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescriptionTextItemsDescription[] TextItem
            {
                get
                {
                    return this.textItemField;
                }
                set
                {
                    this.textItemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceMultimediaDescriptionsMultimediaDescriptionTextItemsDescription
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceFeature
        {

            private byte securityCodeField;

            private byte proximityCodeField;

            private bool proximityCodeFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte SecurityCode
            {
                get
                {
                    return this.securityCodeField;
                }
                set
                {
                    this.securityCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte ProximityCode
            {
                get
                {
                    return this.proximityCodeField;
                }
                set
                {
                    this.proximityCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ProximityCodeSpecified
            {
                get
                {
                    return this.proximityCodeFieldSpecified;
                }
                set
                {
                    this.proximityCodeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfo
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRooms meetingRoomsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRooms guestRoomsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurants restaurantsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRooms MeetingRooms
            {
                get
                {
                    return this.meetingRoomsField;
                }
                set
                {
                    this.meetingRoomsField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRooms GuestRooms
            {
                get
                {
                    return this.guestRoomsField;
                }
                set
                {
                    this.guestRoomsField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurants Restaurants
            {
                get
                {
                    return this.restaurantsField;
                }
                set
                {
                    this.restaurantsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRooms
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoom meetingRoomField;

            private byte meetingRoomCountField;

            private byte smallestRoomSpaceField;

            private byte largestRoomSpaceField;

            private byte totalRoomSpaceField;

            private byte largestSeatingCapacityField;

            private byte smallestSeatingCapacityField;

            private byte totalRoomSeatingCapacityField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoom MeetingRoom
            {
                get
                {
                    return this.meetingRoomField;
                }
                set
                {
                    this.meetingRoomField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MeetingRoomCount
            {
                get
                {
                    return this.meetingRoomCountField;
                }
                set
                {
                    this.meetingRoomCountField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte SmallestRoomSpace
            {
                get
                {
                    return this.smallestRoomSpaceField;
                }
                set
                {
                    this.smallestRoomSpaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte LargestRoomSpace
            {
                get
                {
                    return this.largestRoomSpaceField;
                }
                set
                {
                    this.largestRoomSpaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte TotalRoomSpace
            {
                get
                {
                    return this.totalRoomSpaceField;
                }
                set
                {
                    this.totalRoomSpaceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte LargestSeatingCapacity
            {
                get
                {
                    return this.largestSeatingCapacityField;
                }
                set
                {
                    this.largestSeatingCapacityField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte SmallestSeatingCapacity
            {
                get
                {
                    return this.smallestSeatingCapacityField;
                }
                set
                {
                    this.smallestSeatingCapacityField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte TotalRoomSeatingCapacity
            {
                get
                {
                    return this.totalRoomSeatingCapacityField;
                }
                set
                {
                    this.totalRoomSeatingCapacityField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoom
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomDimension dimensionField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMeetingRoomCapacity[] availableCapacitiesField;

            private string roomNameField;

            private byte meetingRoomCapacityField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomDimension Dimension
            {
                get
                {
                    return this.dimensionField;
                }
                set
                {
                    this.dimensionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("MeetingRoomCapacity", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMeetingRoomCapacity[] AvailableCapacities
            {
                get
                {
                    return this.availableCapacitiesField;
                }
                set
                {
                    this.availableCapacitiesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RoomName
            {
                get
                {
                    return this.roomNameField;
                }
                set
                {
                    this.roomNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MeetingRoomCapacity
            {
                get
                {
                    return this.meetingRoomCapacityField;
                }
                set
                {
                    this.meetingRoomCapacityField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomDimension
        {

            private byte areaField;

            private byte heightField;

            private byte lengthField;

            private byte widthField;

            private byte unitOfMeasureCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Area
            {
                get
                {
                    return this.areaField;
                }
                set
                {
                    this.areaField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Height
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Length
            {
                get
                {
                    return this.lengthField;
                }
                set
                {
                    this.lengthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Width
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte UnitOfMeasureCode
            {
                get
                {
                    return this.unitOfMeasureCodeField;
                }
                set
                {
                    this.unitOfMeasureCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMeetingRoomCapacity
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMeetingRoomCapacityOccupancy occupancyField;

            private byte meetingRoomFormatCodeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMeetingRoomCapacityOccupancy Occupancy
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
            public byte MeetingRoomFormatCode
            {
                get
                {
                    return this.meetingRoomFormatCodeField;
                }
                set
                {
                    this.meetingRoomFormatCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMeetingRoomCapacityOccupancy
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRooms
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoom guestRoomField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoom GuestRoom
            {
                get
                {
                    return this.guestRoomField;
                }
                set
                {
                    this.guestRoomField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoom
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoomTypeRoom typeRoomField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoomAmenity[] amenitiesField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoomTypeRoom TypeRoom
            {
                get
                {
                    return this.typeRoomField;
                }
                set
                {
                    this.typeRoomField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Amenity", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoomAmenity[] Amenities
            {
                get
                {
                    return this.amenitiesField;
                }
                set
                {
                    this.amenitiesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoomTypeRoom
        {

            private string roomTypeCodeField;

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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomsGuestRoomAmenity
        {

            private byte roomAmenityCodeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte RoomAmenityCode
            {
                get
                {
                    return this.roomAmenityCodeField;
                }
                set
                {
                    this.roomAmenityCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurants
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurant restaurantField;

            private byte quantityField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurant Restaurant
            {
                get
                {
                    return this.restaurantField;
                }
                set
                {
                    this.restaurantField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurant
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantInfoCodes infoCodesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantCuisineCodes cuisineCodesField;

            private string restaurantNameField;

            private byte offerBreakfastField;

            private byte offerLunchField;

            private byte offerDinnerField;

            private byte offerBrunchField;

            private byte proximityCodeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantInfoCodes InfoCodes
            {
                get
                {
                    return this.infoCodesField;
                }
                set
                {
                    this.infoCodesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantCuisineCodes CuisineCodes
            {
                get
                {
                    return this.cuisineCodesField;
                }
                set
                {
                    this.cuisineCodesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RestaurantName
            {
                get
                {
                    return this.restaurantNameField;
                }
                set
                {
                    this.restaurantNameField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte OfferBreakfast
            {
                get
                {
                    return this.offerBreakfastField;
                }
                set
                {
                    this.offerBreakfastField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte OfferLunch
            {
                get
                {
                    return this.offerLunchField;
                }
                set
                {
                    this.offerLunchField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte OfferDinner
            {
                get
                {
                    return this.offerDinnerField;
                }
                set
                {
                    this.offerDinnerField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte OfferBrunch
            {
                get
                {
                    return this.offerBrunchField;
                }
                set
                {
                    this.offerBrunchField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte ProximityCode
            {
                get
                {
                    return this.proximityCodeField;
                }
                set
                {
                    this.proximityCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantInfoCodes
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantInfoCodesInfoCode infoCodeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantInfoCodesInfoCode InfoCode
            {
                get
                {
                    return this.infoCodeField;
                }
                set
                {
                    this.infoCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantInfoCodesInfoCode
        {

            private string nameField;

            private byte codeField;

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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantCuisineCodes
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantCuisineCodesCuisineCode cuisineCodeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantCuisineCodesCuisineCode CuisineCode
            {
                get
                {
                    return this.cuisineCodeField;
                }
                set
                {
                    this.cuisineCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantCuisineCodesCuisineCode
        {

            private byte codeField;

            private byte isMainField;

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
            public byte IsMain
            {
                get
                {
                    return this.isMainField;
                }
                set
                {
                    this.isMainField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicy
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCommissionPolicy commissionPolicyField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicy cancelPolicyField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePayment[] guaranteePaymentPolicyField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfo policyInfoField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyTaxPolicy[] taxPoliciesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPolicies petsPoliciesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirements stayRequirementsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCommissionPolicy CommissionPolicy
            {
                get
                {
                    return this.commissionPolicyField;
                }
                set
                {
                    this.commissionPolicyField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicy CancelPolicy
            {
                get
                {
                    return this.cancelPolicyField;
                }
                set
                {
                    this.cancelPolicyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("GuaranteePayment", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePayment[] GuaranteePaymentPolicy
            {
                get
                {
                    return this.guaranteePaymentPolicyField;
                }
                set
                {
                    this.guaranteePaymentPolicyField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfo PolicyInfo
            {
                get
                {
                    return this.policyInfoField;
                }
                set
                {
                    this.policyInfoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("TaxPolicy", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyTaxPolicy[] TaxPolicies
            {
                get
                {
                    return this.taxPoliciesField;
                }
                set
                {
                    this.taxPoliciesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPolicies PetsPolicies
            {
                get
                {
                    return this.petsPoliciesField;
                }
                set
                {
                    this.petsPoliciesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirements StayRequirements
            {
                get
                {
                    return this.stayRequirementsField;
                }
                set
                {
                    this.stayRequirementsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCommissionPolicy
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCommissionPolicyPaymentCompany paymentCompanyField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCommissionPolicyPaymentCompany PaymentCompany
            {
                get
                {
                    return this.paymentCompanyField;
                }
                set
                {
                    this.paymentCompanyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCommissionPolicyPaymentCompany
        {

            private string nameField;

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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicy
        {

            private object cancelPenaltyField;

            /// <remarks/>
            public object CancelPenalty
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePayment
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentAcceptedPayment[] acceptedPaymentsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentText[] descriptionField;

            private byte paymentCodeField;

            private bool paymentCodeFieldSpecified;

            private string guaranteeTypeField;

            private string typeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("AcceptedPayment", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentAcceptedPayment[] AcceptedPayments
            {
                get
                {
                    return this.acceptedPaymentsField;
                }
                set
                {
                    this.acceptedPaymentsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Text", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentText[] Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentAcceptedPayment
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentAcceptedPaymentPaymentCard paymentCardField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentAcceptedPaymentPaymentCard PaymentCard
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentAcceptedPaymentPaymentCard
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentText
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfo
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfoDescription descriptionField;

            private System.DateTime checkInTimeField;

            private System.DateTime checkOutTimeField;

            private byte kidsStayFreeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfoDescription Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte KidsStayFree
            {
                get
                {
                    return this.kidsStayFreeField;
                }
                set
                {
                    this.kidsStayFreeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfoDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfoDescriptionText textField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfoDescriptionText Text
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfoDescriptionText
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyTaxPolicy
        {

            private uint amountField;

            private string currencyCodeField;

            private byte decimalPlacesField;

            private byte chargeUnitField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public uint Amount
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPolicies
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicy petsPolicyField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicy PetsPolicy
            {
                get
                {
                    return this.petsPolicyField;
                }
                set
                {
                    this.petsPolicyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicy
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicyDescription descriptionField;

            private byte petsPolicyCodeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicyDescription Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte PetsPolicyCode
            {
                get
                {
                    return this.petsPolicyCodeField;
                }
                set
                {
                    this.petsPolicyCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicyDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicyDescriptionText textField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicyDescriptionText Text
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPetsPoliciesPetsPolicyDescriptionText
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirements
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirement stayRequirementField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirement StayRequirement
            {
                get
                {
                    return this.stayRequirementField;
                }
                set
                {
                    this.stayRequirementField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirement
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirementDescription descriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirementDescription Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirementDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirementDescriptionText textField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirementDescriptionText Text
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyStayRequirementsStayRequirementDescriptionText
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfo
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPoint[] refPointsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRecreations recreationsField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("RefPoint", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPoint[] RefPoints
            {
                get
                {
                    return this.refPointsField;
                }
                set
                {
                    this.refPointsField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRecreations Recreations
            {
                get
                {
                    return this.recreationsField;
                }
                set
                {
                    this.recreationsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPoint
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptions multimediaDescriptionsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportations transportationsField;

            private string directionField;

            private byte distanceField;

            private bool distanceFieldSpecified;

            private byte unitOfMeasureCodeField;

            private bool unitOfMeasureCodeFieldSpecified;

            private byte indexPointCodeField;

            private bool indexPointCodeFieldSpecified;

            private string nameField;

            private byte primaryIndicatorField;

            private bool primaryIndicatorFieldSpecified;

            private string toFromField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptions MultimediaDescriptions
            {
                get
                {
                    return this.multimediaDescriptionsField;
                }
                set
                {
                    this.multimediaDescriptionsField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportations Transportations
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Direction
            {
                get
                {
                    return this.directionField;
                }
                set
                {
                    this.directionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Distance
            {
                get
                {
                    return this.distanceField;
                }
                set
                {
                    this.distanceField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool DistanceSpecified
            {
                get
                {
                    return this.distanceFieldSpecified;
                }
                set
                {
                    this.distanceFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte UnitOfMeasureCode
            {
                get
                {
                    return this.unitOfMeasureCodeField;
                }
                set
                {
                    this.unitOfMeasureCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool UnitOfMeasureCodeSpecified
            {
                get
                {
                    return this.unitOfMeasureCodeFieldSpecified;
                }
                set
                {
                    this.unitOfMeasureCodeFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte IndexPointCode
            {
                get
                {
                    return this.indexPointCodeField;
                }
                set
                {
                    this.indexPointCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool IndexPointCodeSpecified
            {
                get
                {
                    return this.indexPointCodeFieldSpecified;
                }
                set
                {
                    this.indexPointCodeFieldSpecified = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte PrimaryIndicator
            {
                get
                {
                    return this.primaryIndicatorField;
                }
                set
                {
                    this.primaryIndicatorField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool PrimaryIndicatorSpecified
            {
                get
                {
                    return this.primaryIndicatorFieldSpecified;
                }
                set
                {
                    this.primaryIndicatorFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string ToFrom
            {
                get
                {
                    return this.toFromField;
                }
                set
                {
                    this.toFromField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptions
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescription multimediaDescriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescription MultimediaDescription
            {
                get
                {
                    return this.multimediaDescriptionField;
                }
                set
                {
                    this.multimediaDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItems textItemsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItems TextItems
            {
                get
                {
                    return this.textItemsField;
                }
                set
                {
                    this.textItemsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem textItemField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem TextItem
            {
                get
                {
                    return this.textItemField;
                }
                set
                {
                    this.textItemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription descriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportations
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportation transportationField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportation Transportation
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportation
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptions multimediaDescriptionsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptions MultimediaDescriptions
            {
                get
                {
                    return this.multimediaDescriptionsField;
                }
                set
                {
                    this.multimediaDescriptionsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptions
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescription multimediaDescriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescription MultimediaDescription
            {
                get
                {
                    return this.multimediaDescriptionField;
                }
                set
                {
                    this.multimediaDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItems textItemsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItems TextItems
            {
                get
                {
                    return this.textItemsField;
                }
                set
                {
                    this.textItemsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem textItemField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem TextItem
            {
                get
                {
                    return this.textItemField;
                }
                set
                {
                    this.textItemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription[] descriptionField;

            private string titleField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Description")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription[] Description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationsTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription
        {

            private string languageField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRecreations
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRecreationsRecreation recreationField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRecreationsRecreation Recreation
            {
                get
                {
                    return this.recreationField;
                }
                set
                {
                    this.recreationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRecreationsRecreation
        {

            private byte codeField;

            private byte proximityCodeField;

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
            public byte ProximityCode
            {
                get
                {
                    return this.proximityCodeField;
                }
                set
                {
                    this.proximityCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfo
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAward[] awardsField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Award", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAward[] Awards
            {
                get
                {
                    return this.awardsField;
                }
                set
                {
                    this.awardsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAward
        {

            private string providerField;

            private byte ratingField;

            private string ratingSymbolField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string Provider
            {
                get
                {
                    return this.providerField;
                }
                set
                {
                    this.providerField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Rating
            {
                get
                {
                    return this.ratingField;
                }
                set
                {
                    this.ratingField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string RatingSymbol
            {
                get
                {
                    return this.ratingSymbolField;
                }
                set
                {
                    this.ratingSymbolField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfos
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfo contactInfoField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfo ContactInfo
            {
                get
                {
                    return this.contactInfoField;
                }
                set
                {
                    this.contactInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfo
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddresses addressesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoPhone[] phonesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoURLs uRLsField;

            private byte locationField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddresses Addresses
            {
                get
                {
                    return this.addressesField;
                }
                set
                {
                    this.addressesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Phone", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoPhone[] Phones
            {
                get
                {
                    return this.phonesField;
                }
                set
                {
                    this.phonesField = value;
                }
            }

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoURLs URLs
            {
                get
                {
                    return this.uRLsField;
                }
                set
                {
                    this.uRLsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Location
            {
                get
                {
                    return this.locationField;
                }
                set
                {
                    this.locationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddresses
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddressesAddress addressField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddressesAddress Address
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddressesAddress
        {

            private string[] addressLineField;

            private string cityNameField;

            private uint postalCodeField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddressesAddressCountryName countryNameField;

            private byte useTypeField;

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
            public uint PostalCode
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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddressesAddressCountryName CountryName
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte UseType
            {
                get
                {
                    return this.useTypeField;
                }
                set
                {
                    this.useTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoAddressesAddressCountryName
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoPhone
        {

            private byte phoneTechTypeField;

            private string phoneNumberField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte PhoneTechType
            {
                get
                {
                    return this.phoneTechTypeField;
                }
                set
                {
                    this.phoneTechTypeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string PhoneNumber
            {
                get
                {
                    return this.phoneNumberField;
                }
                set
                {
                    this.phoneNumberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfosContactInfoURLs
        {

            private string uRLField;

            /// <remarks/>
            public string URL
            {
                get
                {
                    return this.uRLField;
                }
                set
                {
                    this.uRLField = value;
                }
            }
        }


    }
}
