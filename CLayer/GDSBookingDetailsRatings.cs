using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CLayer
{
    public partial class  GDSBookingDetailsRatings
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfo[] contactInfosField;

            private string currencyCodeField;

            private string timeZoneField;

            private string chainCodeField;

            private string hotelCodeField;

            private string hotelNameField;

            private string hotelCodeContextField;

            private string brandNameField;

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
            [System.Xml.Serialization.XmlArrayItemAttribute("ContactInfo", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfo[] ContactInfos
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string BrandName
            {
                get
                {
                    return this.brandNameField;
                }
                set
                {
                    this.brandNameField = value;
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoHotelName
        {

            private string hotelShortNameField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodes
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesLocationCategory locationCategoryField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesSegmentCategory segmentCategoryField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesHotelCategory hotelCategoryField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfo[] guestRoomInfoField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesLocationCategory LocationCategory
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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesHotelCategory HotelCategory
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

            private ushort fileSizeField;

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
            public ushort FileSize
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsTextItem textItemField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsTextItem TextItem
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsTextItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsTextItemDescription descriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsTextItemDescription Description
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoCategoryCodesGuestRoomInfoMultimediaDescriptionTextItemsTextItemDescription
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescription[] multimediaDescriptionsField;

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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItem[] imageItemsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItems textItemsField;

            private byte infoCodeField;

            private bool infoCodeFieldSpecified;

            private byte additionalDetailCodeField;

            private bool additionalDetailCodeFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("ImageItem", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItem[] ImageItems
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItemImageFormat[] imageFormatField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItemDescription descriptionField;

            private byte categoryField;

            private System.DateTime lastModifyDateTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("ImageFormat")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItemImageFormat[] ImageFormat
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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItemDescription Description
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItemImageFormat
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionImageItemDescription
        {

            private string languageField;

            private string captionField;

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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsTextItem textItemField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsTextItem TextItem
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsTextItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsTextItemDescription descriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsTextItemDescription Description
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoDescriptionsMultimediaDescriptionTextItemsTextItemDescription
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceFeature[] featuresField;

            private byte codeField;

            private bool codeFieldSpecified;

            private byte proximityCodeField;

            private bool proximityCodeFieldSpecified;

            private byte quantityField;

            private bool quantityFieldSpecified;

            private byte businessServiceCodeField;

            private bool businessServiceCodeFieldSpecified;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentHotelInfoServiceFeature
        {

            private byte accessibleCodeField;

            private bool accessibleCodeFieldSpecified;

            private byte securityCodeField;

            private bool securityCodeFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte AccessibleCode
            {
                get
                {
                    return this.accessibleCodeField;
                }
                set
                {
                    this.accessibleCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool AccessibleCodeSpecified
            {
                get
                {
                    return this.accessibleCodeFieldSpecified;
                }
                set
                {
                    this.accessibleCodeFieldSpecified = value;
                }
            }

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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool SecurityCodeSpecified
            {
                get
                {
                    return this.securityCodeFieldSpecified;
                }
                set
                {
                    this.securityCodeFieldSpecified = value;
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoom[] guestRoomsField;

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
            [System.Xml.Serialization.XmlArrayItemAttribute("GuestRoom", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoom[] GuestRooms
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoom[] meetingRoomField;

            private byte meetingRoomCountField;

            private byte smallestRoomSpaceField;

            private byte largestRoomSpaceField;

            private byte largestSeatingCapacityField;

            private byte smallestSeatingCapacityField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("MeetingRoom")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoom[] MeetingRoom
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoom
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomDimension dimensionField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMeetingRoomCapacity[] availableCapacitiesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptions multimediaDescriptionsField;

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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptions MultimediaDescriptions
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

            private decimal heightField;

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
            public decimal Height
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptions
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescription multimediaDescriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescription MultimediaDescription
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItems textItemsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItems TextItems
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem textItemField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem TextItem
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription descriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription Description
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoMeetingRoomsMeetingRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoom
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomTypeRoom typeRoomField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenity[] amenitiesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptions multimediaDescriptionsField;

            private byte maxOccupancyField;

            private bool maxOccupancyFieldSpecified;

            private byte maxAdultOccupancyField;

            private bool maxAdultOccupancyFieldSpecified;

            private byte maxChildOccupancyField;

            private bool maxChildOccupancyFieldSpecified;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomTypeRoom TypeRoom
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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenity[] Amenities
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

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptions MultimediaDescriptions
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

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool MaxOccupancySpecified
            {
                get
                {
                    return this.maxOccupancyFieldSpecified;
                }
                set
                {
                    this.maxOccupancyFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MaxAdultOccupancy
            {
                get
                {
                    return this.maxAdultOccupancyField;
                }
                set
                {
                    this.maxAdultOccupancyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool MaxAdultOccupancySpecified
            {
                get
                {
                    return this.maxAdultOccupancyFieldSpecified;
                }
                set
                {
                    this.maxAdultOccupancyFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte MaxChildOccupancy
            {
                get
                {
                    return this.maxChildOccupancyField;
                }
                set
                {
                    this.maxChildOccupancyField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool MaxChildOccupancySpecified
            {
                get
                {
                    return this.maxChildOccupancyFieldSpecified;
                }
                set
                {
                    this.maxChildOccupancyFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomTypeRoom
        {

            private string nameField;

            private string roomTypeCodeField;

            private byte roomCategoryField;

            private bool roomCategoryFieldSpecified;

            private byte bedTypeCodeField;

            private bool bedTypeCodeFieldSpecified;

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
            public byte RoomCategory
            {
                get
                {
                    return this.roomCategoryField;
                }
                set
                {
                    this.roomCategoryField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool RoomCategorySpecified
            {
                get
                {
                    return this.roomCategoryFieldSpecified;
                }
                set
                {
                    this.roomCategoryFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte BedTypeCode
            {
                get
                {
                    return this.bedTypeCodeField;
                }
                set
                {
                    this.bedTypeCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool BedTypeCodeSpecified
            {
                get
                {
                    return this.bedTypeCodeFieldSpecified;
                }
                set
                {
                    this.bedTypeCodeFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenity
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedules operationSchedulesField;

            private byte roomAmenityCodeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedules OperationSchedules
            {
                get
                {
                    return this.operationSchedulesField;
                }
                set
                {
                    this.operationSchedulesField = value;
                }
            }

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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedules
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedulesOperationSchedule operationScheduleField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedulesOperationSchedule OperationSchedule
            {
                get
                {
                    return this.operationScheduleField;
                }
                set
                {
                    this.operationScheduleField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedulesOperationSchedule
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedulesOperationScheduleCharge chargeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedulesOperationScheduleCharge Charge
            {
                get
                {
                    return this.chargeField;
                }
                set
                {
                    this.chargeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomAmenityOperationSchedulesOperationScheduleCharge
        {

            private string typeField;

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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptions
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescription multimediaDescriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescription MultimediaDescription
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItems textItemsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItems TextItems
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem textItemField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem TextItem
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription descriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription Description
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoGuestRoomMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurants
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurant[] restaurantField;

            private byte quantityField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Restaurant")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurant[] Restaurant
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptions multimediaDescriptionsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedules operationSchedulesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantInfoCodes infoCodesField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantCuisineCodes cuisineCodesField;

            private string restaurantNameField;

            private byte maxSeatingCapacityField;

            private byte proximityCodeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptions MultimediaDescriptions
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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedules OperationSchedules
            {
                get
                {
                    return this.operationSchedulesField;
                }
                set
                {
                    this.operationSchedulesField = value;
                }
            }

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
            public byte MaxSeatingCapacity
            {
                get
                {
                    return this.maxSeatingCapacityField;
                }
                set
                {
                    this.maxSeatingCapacityField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptions
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescription multimediaDescriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescription MultimediaDescription
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItems textItemsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItems TextItems
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem textItemField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem TextItem
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription descriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription Description
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedules
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedulesOperationSchedule operationScheduleField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedulesOperationSchedule OperationSchedule
            {
                get
                {
                    return this.operationScheduleField;
                }
                set
                {
                    this.operationScheduleField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedulesOperationSchedule
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedulesOperationScheduleOperationTime[] operationTimesField;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("OperationTime", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedulesOperationScheduleOperationTime[] OperationTimes
            {
                get
                {
                    return this.operationTimesField;
                }
                set
                {
                    this.operationTimesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentFacilityInfoRestaurantsRestaurantOperationSchedulesOperationScheduleOperationTime
        {

            private byte monField;

            private bool monFieldSpecified;

            private byte tueField;

            private bool tueFieldSpecified;

            private byte wedsField;

            private bool wedsFieldSpecified;

            private byte thurField;

            private bool thurFieldSpecified;

            private byte friField;

            private bool friFieldSpecified;

            private byte satField;

            private bool satFieldSpecified;

            private byte sunField;

            private bool sunFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Mon
            {
                get
                {
                    return this.monField;
                }
                set
                {
                    this.monField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool MonSpecified
            {
                get
                {
                    return this.monFieldSpecified;
                }
                set
                {
                    this.monFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Tue
            {
                get
                {
                    return this.tueField;
                }
                set
                {
                    this.tueField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool TueSpecified
            {
                get
                {
                    return this.tueFieldSpecified;
                }
                set
                {
                    this.tueFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Weds
            {
                get
                {
                    return this.wedsField;
                }
                set
                {
                    this.wedsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool WedsSpecified
            {
                get
                {
                    return this.wedsFieldSpecified;
                }
                set
                {
                    this.wedsFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Thur
            {
                get
                {
                    return this.thurField;
                }
                set
                {
                    this.thurField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool ThurSpecified
            {
                get
                {
                    return this.thurFieldSpecified;
                }
                set
                {
                    this.thurFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Fri
            {
                get
                {
                    return this.friField;
                }
                set
                {
                    this.friField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool FriSpecified
            {
                get
                {
                    return this.friFieldSpecified;
                }
                set
                {
                    this.friFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Sat
            {
                get
                {
                    return this.satField;
                }
                set
                {
                    this.satField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool SatSpecified
            {
                get
                {
                    return this.satFieldSpecified;
                }
                set
                {
                    this.satFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Sun
            {
                get
                {
                    return this.sunField;
                }
                set
                {
                    this.sunField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool SunSpecified
            {
                get
                {
                    return this.sunFieldSpecified;
                }
                set
                {
                    this.sunFieldSpecified = value;
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyPolicyInfo policyInfoField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicy cancelPolicyField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePayment[] guaranteePaymentPolicyField;

            private System.DateTime startField;

            private bool startFieldSpecified;

            private System.DateTime endField;

            private bool endFieldSpecified;

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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool StartSpecified
            {
                get
                {
                    return this.startFieldSpecified;
                }
                set
                {
                    this.startFieldSpecified = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool EndSpecified
            {
                get
                {
                    return this.endFieldSpecified;
                }
                set
                {
                    this.endFieldSpecified = value;
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

            private bool checkInTimeFieldSpecified;

            private System.DateTime checkOutTimeField;

            private bool checkOutTimeFieldSpecified;

            private byte usualStayFreeCutoffAgeField;

            private bool usualStayFreeCutoffAgeFieldSpecified;

            private byte kidsStayFreeField;

            private bool kidsStayFreeFieldSpecified;

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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool CheckInTimeSpecified
            {
                get
                {
                    return this.checkInTimeFieldSpecified;
                }
                set
                {
                    this.checkInTimeFieldSpecified = value;
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
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool CheckOutTimeSpecified
            {
                get
                {
                    return this.checkOutTimeFieldSpecified;
                }
                set
                {
                    this.checkOutTimeFieldSpecified = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte UsualStayFreeCutoffAge
            {
                get
                {
                    return this.usualStayFreeCutoffAgeField;
                }
                set
                {
                    this.usualStayFreeCutoffAgeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool UsualStayFreeCutoffAgeSpecified
            {
                get
                {
                    return this.usualStayFreeCutoffAgeFieldSpecified;
                }
                set
                {
                    this.usualStayFreeCutoffAgeFieldSpecified = value;
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

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool KidsStayFreeSpecified
            {
                get
                {
                    return this.kidsStayFreeFieldSpecified;
                }
                set
                {
                    this.kidsStayFreeFieldSpecified = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicy
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenalty cancelPenaltyField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenalty CancelPenalty
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenalty
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyDeadline deadlineField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyPenaltyDescription penaltyDescriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyDeadline Deadline
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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyPenaltyDescription PenaltyDescription
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyDeadline
        {

            private System.DateTime absoluteDeadlineField;

            private string offsetTimeUnitField;

            private byte offsetUnitMultiplierField;

            private string offsetDropTimeField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute(DataType = "time")]
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string OffsetTimeUnit
            {
                get
                {
                    return this.offsetTimeUnitField;
                }
                set
                {
                    this.offsetTimeUnitField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte OffsetUnitMultiplier
            {
                get
                {
                    return this.offsetUnitMultiplierField;
                }
                set
                {
                    this.offsetUnitMultiplierField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string OffsetDropTime
            {
                get
                {
                    return this.offsetDropTimeField;
                }
                set
                {
                    this.offsetDropTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyPenaltyDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyPenaltyDescriptionText textField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyPenaltyDescriptionText Text
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyCancelPolicyCancelPenaltyPenaltyDescriptionText
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePayment
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentPolicyGuaranteePaymentAcceptedPayment[] acceptedPaymentsField;

            private byte paymentCodeField;

            private bool paymentCodeFieldSpecified;

            private string typeField;

            private string guaranteeTypeField;

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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfo
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPoint[] refPointsField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttraction[] attractionsField;

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
            [System.Xml.Serialization.XmlArrayItemAttribute("Attraction", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttraction[] Attractions
            {
                get
                {
                    return this.attractionsField;
                }
                set
                {
                    this.attractionsField = value;
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportation[] transportationsField;

            private string directionField;

            private decimal distanceField;

            private bool distanceFieldSpecified;

            private byte unitOfMeasureCodeField;

            private bool unitOfMeasureCodeFieldSpecified;

            private byte indexPointCodeField;

            private string nameField;

            private byte primaryIndicatorField;

            private bool primaryIndicatorFieldSpecified;

            private string toFromField;

            private byte nearestField;

            private bool nearestFieldSpecified;

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Transportation", IsNullable = false)]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportation[] Transportations
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
            public decimal Distance
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

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte Nearest
            {
                get
                {
                    return this.nearestField;
                }
                set
                {
                    this.nearestField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool NearestSpecified
            {
                get
                {
                    return this.nearestFieldSpecified;
                }
                set
                {
                    this.nearestFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportation
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptions multimediaDescriptionsField;

            private byte transportationCodeField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptions MultimediaDescriptions
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptions
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescription multimediaDescriptionField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescription MultimediaDescription
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescription
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItems textItemsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItems TextItems
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItems
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem textItemField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem TextItem
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItem
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription descriptionField;

            private string titleField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription Description
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoRefPointTransportationMultimediaDescriptionsMultimediaDescriptionTextItemsTextItemDescription
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttraction
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPoints refPointsField;

            private byte attractionCategoryCodeField;

            private string attractionNameField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPoints RefPoints
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
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte AttractionCategoryCode
            {
                get
                {
                    return this.attractionCategoryCodeField;
                }
                set
                {
                    this.attractionCategoryCodeField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string AttractionName
            {
                get
                {
                    return this.attractionNameField;
                }
                set
                {
                    this.attractionNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPoints
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPoint refPointField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPoint RefPoint
            {
                get
                {
                    return this.refPointField;
                }
                set
                {
                    this.refPointField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPoint
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPointTransportations transportationsField;

            private string directionField;

            private decimal distanceField;

            private byte unitOfMeasureCodeField;

            private string toFromField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPointTransportations Transportations
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
            public decimal Distance
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPointTransportations
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPointTransportationsTransportation transportationField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPointTransportationsTransportation Transportation
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAreaInfoAttractionRefPointsRefPointTransportationsTransportation
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

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAwards awardsField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAwards Awards
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAwards
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAwardsAward awardField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAwardsAward Award
            {
                get
                {
                    return this.awardField;
                }
                set
                {
                    this.awardField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentAffiliationInfoAwardsAward
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfo
        {

            private object[] itemsField;

            private byte locationField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Addresses", typeof(OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoAddresses))]
            [System.Xml.Serialization.XmlElementAttribute("Emails", typeof(OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoEmails))]
            [System.Xml.Serialization.XmlElementAttribute("Names", typeof(OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoNames))]
            [System.Xml.Serialization.XmlElementAttribute("Phones", typeof(OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoPhones))]
            [System.Xml.Serialization.XmlElementAttribute("URLs", typeof(OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoURLs))]
            public object[] Items
            {
                get
                {
                    return this.itemsField;
                }
                set
                {
                    this.itemsField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoAddresses
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoAddressesAddress addressField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoAddressesAddress Address
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoAddressesAddress
        {

            private string[] addressLineField;

            private string cityNameField;

            private uint postalCodeField;

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoAddressesAddressCountryName countryNameField;

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
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoAddressesAddressCountryName CountryName
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoAddressesAddressCountryName
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoEmails
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoEmailsEmail emailField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoEmailsEmail Email
            {
                get
                {
                    return this.emailField;
                }
                set
                {
                    this.emailField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoEmailsEmail
        {

            private byte emailTypeField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public byte EmailType
            {
                get
                {
                    return this.emailTypeField;
                }
                set
                {
                    this.emailTypeField = value;
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoNames
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoNamesName nameField;

            /// <remarks/>
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoNamesName Name
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoNamesName
        {

            private string surnameField;

            /// <remarks/>
            public string Surname
            {
                get
                {
                    return this.surnameField;
                }
                set
                {
                    this.surnameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoPhones
        {

            private OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoPhonesPhone[] phoneField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("Phone")]
            public OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoPhonesPhone[] Phone
            {
                get
                {
                    return this.phoneField;
                }
                set
                {
                    this.phoneField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.opentravel.org/OTA/2003/05")]
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoPhonesPhone
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
        public partial class OTA_HotelDescriptiveInfoRSHotelDescriptiveContentsHotelDescriptiveContentContactInfoURLs
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
