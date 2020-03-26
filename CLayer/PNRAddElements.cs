using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public partial class PNRAddElements
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

            private PNR_Reply pNR_ReplyField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
            public PNR_Reply PNR_Reply
            {
                get
                {
                    return this.pNR_ReplyField;
                }
                set
                {
                    this.pNR_ReplyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A", IsNullable = false)]
        public partial class PNR_Reply
        {

            private PNR_ReplyPnrHeader pnrHeaderField;

            private PNR_ReplySecurityInformation securityInformationField;

            private PNR_ReplySbrPOSDetails sbrPOSDetailsField;

            private PNR_ReplySbrCreationPosDetails sbrCreationPosDetailsField;

            private PNR_ReplySbrUpdatorPosDetails sbrUpdatorPosDetailsField;

            private PNR_ReplyTravellerInfo travellerInfoField;

            private PNR_ReplyDataElementsMaster dataElementsMasterField;

            /// <remarks/>
            public PNR_ReplyPnrHeader pnrHeader
            {
                get
                {
                    return this.pnrHeaderField;
                }
                set
                {
                    this.pnrHeaderField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySecurityInformation securityInformation
            {
                get
                {
                    return this.securityInformationField;
                }
                set
                {
                    this.securityInformationField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrPOSDetails sbrPOSDetails
            {
                get
                {
                    return this.sbrPOSDetailsField;
                }
                set
                {
                    this.sbrPOSDetailsField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrCreationPosDetails sbrCreationPosDetails
            {
                get
                {
                    return this.sbrCreationPosDetailsField;
                }
                set
                {
                    this.sbrCreationPosDetailsField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrUpdatorPosDetails sbrUpdatorPosDetails
            {
                get
                {
                    return this.sbrUpdatorPosDetailsField;
                }
                set
                {
                    this.sbrUpdatorPosDetailsField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyTravellerInfo travellerInfo
            {
                get
                {
                    return this.travellerInfoField;
                }
                set
                {
                    this.travellerInfoField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyDataElementsMaster dataElementsMaster
            {
                get
                {
                    return this.dataElementsMasterField;
                }
                set
                {
                    this.dataElementsMasterField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyPnrHeader
        {

            private PNR_ReplyPnrHeaderReservationInfo reservationInfoField;

            /// <remarks/>
            public PNR_ReplyPnrHeaderReservationInfo reservationInfo
            {
                get
                {
                    return this.reservationInfoField;
                }
                set
                {
                    this.reservationInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyPnrHeaderReservationInfo
        {

            private PNR_ReplyPnrHeaderReservationInfoReservation reservationField;

            /// <remarks/>
            public PNR_ReplyPnrHeaderReservationInfoReservation reservation
            {
                get
                {
                    return this.reservationField;
                }
                set
                {
                    this.reservationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyPnrHeaderReservationInfoReservation
        {

            private string companyIdField;

            /// <remarks/>
            public string companyId
            {
                get
                {
                    return this.companyIdField;
                }
                set
                {
                    this.companyIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySecurityInformation
        {

            private PNR_ReplySecurityInformationResponsibilityInformation responsibilityInformationField;

            private PNR_ReplySecurityInformationQueueingInformation queueingInformationField;

            private string cityCodeField;

            /// <remarks/>
            public PNR_ReplySecurityInformationResponsibilityInformation responsibilityInformation
            {
                get
                {
                    return this.responsibilityInformationField;
                }
                set
                {
                    this.responsibilityInformationField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySecurityInformationQueueingInformation queueingInformation
            {
                get
                {
                    return this.queueingInformationField;
                }
                set
                {
                    this.queueingInformationField = value;
                }
            }

            /// <remarks/>
            public string cityCode
            {
                get
                {
                    return this.cityCodeField;
                }
                set
                {
                    this.cityCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySecurityInformationResponsibilityInformation
        {

            private string typeOfPnrElementField;

            private string officeIdField;

            /// <remarks/>
            public string typeOfPnrElement
            {
                get
                {
                    return this.typeOfPnrElementField;
                }
                set
                {
                    this.typeOfPnrElementField = value;
                }
            }

            /// <remarks/>
            public string officeId
            {
                get
                {
                    return this.officeIdField;
                }
                set
                {
                    this.officeIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySecurityInformationQueueingInformation
        {

            private string queueingOfficeIdField;

            /// <remarks/>
            public string queueingOfficeId
            {
                get
                {
                    return this.queueingOfficeIdField;
                }
                set
                {
                    this.queueingOfficeIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrPOSDetails
        {

            private PNR_ReplySbrPOSDetailsSbrUserIdentificationOwn sbrUserIdentificationOwnField;

            private PNR_ReplySbrPOSDetailsSbrSystemDetails sbrSystemDetailsField;

            private PNR_ReplySbrPOSDetailsSbrPreferences sbrPreferencesField;

            /// <remarks/>
            public PNR_ReplySbrPOSDetailsSbrUserIdentificationOwn sbrUserIdentificationOwn
            {
                get
                {
                    return this.sbrUserIdentificationOwnField;
                }
                set
                {
                    this.sbrUserIdentificationOwnField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrPOSDetailsSbrSystemDetails sbrSystemDetails
            {
                get
                {
                    return this.sbrSystemDetailsField;
                }
                set
                {
                    this.sbrSystemDetailsField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrPOSDetailsSbrPreferences sbrPreferences
            {
                get
                {
                    return this.sbrPreferencesField;
                }
                set
                {
                    this.sbrPreferencesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrPOSDetailsSbrUserIdentificationOwn
        {

            private PNR_ReplySbrPOSDetailsSbrUserIdentificationOwnOriginIdentification originIdentificationField;

            /// <remarks/>
            public PNR_ReplySbrPOSDetailsSbrUserIdentificationOwnOriginIdentification originIdentification
            {
                get
                {
                    return this.originIdentificationField;
                }
                set
                {
                    this.originIdentificationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrPOSDetailsSbrUserIdentificationOwnOriginIdentification
        {

            private object inHouseIdentification1Field;

            /// <remarks/>
            public object inHouseIdentification1
            {
                get
                {
                    return this.inHouseIdentification1Field;
                }
                set
                {
                    this.inHouseIdentification1Field = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrPOSDetailsSbrSystemDetails
        {

            private PNR_ReplySbrPOSDetailsSbrSystemDetailsDeliveringSystem deliveringSystemField;

            /// <remarks/>
            public PNR_ReplySbrPOSDetailsSbrSystemDetailsDeliveringSystem deliveringSystem
            {
                get
                {
                    return this.deliveringSystemField;
                }
                set
                {
                    this.deliveringSystemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrPOSDetailsSbrSystemDetailsDeliveringSystem
        {

            private object companyIdField;

            /// <remarks/>
            public object companyId
            {
                get
                {
                    return this.companyIdField;
                }
                set
                {
                    this.companyIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrPOSDetailsSbrPreferences
        {

            private PNR_ReplySbrPOSDetailsSbrPreferencesUserPreferences userPreferencesField;

            /// <remarks/>
            public PNR_ReplySbrPOSDetailsSbrPreferencesUserPreferences userPreferences
            {
                get
                {
                    return this.userPreferencesField;
                }
                set
                {
                    this.userPreferencesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrPOSDetailsSbrPreferencesUserPreferences
        {

            private object codedCountryField;

            /// <remarks/>
            public object codedCountry
            {
                get
                {
                    return this.codedCountryField;
                }
                set
                {
                    this.codedCountryField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrCreationPosDetails
        {

            private PNR_ReplySbrCreationPosDetailsSbrUserIdentificationOwn sbrUserIdentificationOwnField;

            private PNR_ReplySbrCreationPosDetailsSbrSystemDetails sbrSystemDetailsField;

            private PNR_ReplySbrCreationPosDetailsSbrPreferences sbrPreferencesField;

            /// <remarks/>
            public PNR_ReplySbrCreationPosDetailsSbrUserIdentificationOwn sbrUserIdentificationOwn
            {
                get
                {
                    return this.sbrUserIdentificationOwnField;
                }
                set
                {
                    this.sbrUserIdentificationOwnField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrCreationPosDetailsSbrSystemDetails sbrSystemDetails
            {
                get
                {
                    return this.sbrSystemDetailsField;
                }
                set
                {
                    this.sbrSystemDetailsField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrCreationPosDetailsSbrPreferences sbrPreferences
            {
                get
                {
                    return this.sbrPreferencesField;
                }
                set
                {
                    this.sbrPreferencesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrCreationPosDetailsSbrUserIdentificationOwn
        {

            private PNR_ReplySbrCreationPosDetailsSbrUserIdentificationOwnOriginIdentification originIdentificationField;

            /// <remarks/>
            public PNR_ReplySbrCreationPosDetailsSbrUserIdentificationOwnOriginIdentification originIdentification
            {
                get
                {
                    return this.originIdentificationField;
                }
                set
                {
                    this.originIdentificationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrCreationPosDetailsSbrUserIdentificationOwnOriginIdentification
        {

            private object inHouseIdentification1Field;

            /// <remarks/>
            public object inHouseIdentification1
            {
                get
                {
                    return this.inHouseIdentification1Field;
                }
                set
                {
                    this.inHouseIdentification1Field = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrCreationPosDetailsSbrSystemDetails
        {

            private PNR_ReplySbrCreationPosDetailsSbrSystemDetailsDeliveringSystem deliveringSystemField;

            /// <remarks/>
            public PNR_ReplySbrCreationPosDetailsSbrSystemDetailsDeliveringSystem deliveringSystem
            {
                get
                {
                    return this.deliveringSystemField;
                }
                set
                {
                    this.deliveringSystemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrCreationPosDetailsSbrSystemDetailsDeliveringSystem
        {

            private object companyIdField;

            /// <remarks/>
            public object companyId
            {
                get
                {
                    return this.companyIdField;
                }
                set
                {
                    this.companyIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrCreationPosDetailsSbrPreferences
        {

            private PNR_ReplySbrCreationPosDetailsSbrPreferencesUserPreferences userPreferencesField;

            /// <remarks/>
            public PNR_ReplySbrCreationPosDetailsSbrPreferencesUserPreferences userPreferences
            {
                get
                {
                    return this.userPreferencesField;
                }
                set
                {
                    this.userPreferencesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrCreationPosDetailsSbrPreferencesUserPreferences
        {

            private object codedCountryField;

            /// <remarks/>
            public object codedCountry
            {
                get
                {
                    return this.codedCountryField;
                }
                set
                {
                    this.codedCountryField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetails
        {

            private PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwn sbrUserIdentificationOwnField;

            private PNR_ReplySbrUpdatorPosDetailsSbrSystemDetails sbrSystemDetailsField;

            private PNR_ReplySbrUpdatorPosDetailsSbrPreferences sbrPreferencesField;

            /// <remarks/>
            public PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwn sbrUserIdentificationOwn
            {
                get
                {
                    return this.sbrUserIdentificationOwnField;
                }
                set
                {
                    this.sbrUserIdentificationOwnField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrUpdatorPosDetailsSbrSystemDetails sbrSystemDetails
            {
                get
                {
                    return this.sbrSystemDetailsField;
                }
                set
                {
                    this.sbrSystemDetailsField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplySbrUpdatorPosDetailsSbrPreferences sbrPreferences
            {
                get
                {
                    return this.sbrPreferencesField;
                }
                set
                {
                    this.sbrPreferencesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwn
        {

            private PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwnOriginIdentification originIdentificationField;

            private string originatorTypeCodeField;

            /// <remarks/>
            public PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwnOriginIdentification originIdentification
            {
                get
                {
                    return this.originIdentificationField;
                }
                set
                {
                    this.originIdentificationField = value;
                }
            }

            /// <remarks/>
            public string originatorTypeCode
            {
                get
                {
                    return this.originatorTypeCodeField;
                }
                set
                {
                    this.originatorTypeCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwnOriginIdentification
        {

            private string inHouseIdentification1Field;

            /// <remarks/>
            public string inHouseIdentification1
            {
                get
                {
                    return this.inHouseIdentification1Field;
                }
                set
                {
                    this.inHouseIdentification1Field = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetailsSbrSystemDetails
        {

            private PNR_ReplySbrUpdatorPosDetailsSbrSystemDetailsDeliveringSystem deliveringSystemField;

            /// <remarks/>
            public PNR_ReplySbrUpdatorPosDetailsSbrSystemDetailsDeliveringSystem deliveringSystem
            {
                get
                {
                    return this.deliveringSystemField;
                }
                set
                {
                    this.deliveringSystemField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetailsSbrSystemDetailsDeliveringSystem
        {

            private string companyIdField;

            private string locationIdField;

            /// <remarks/>
            public string companyId
            {
                get
                {
                    return this.companyIdField;
                }
                set
                {
                    this.companyIdField = value;
                }
            }

            /// <remarks/>
            public string locationId
            {
                get
                {
                    return this.locationIdField;
                }
                set
                {
                    this.locationIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetailsSbrPreferences
        {

            private PNR_ReplySbrUpdatorPosDetailsSbrPreferencesUserPreferences userPreferencesField;

            /// <remarks/>
            public PNR_ReplySbrUpdatorPosDetailsSbrPreferencesUserPreferences userPreferences
            {
                get
                {
                    return this.userPreferencesField;
                }
                set
                {
                    this.userPreferencesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetailsSbrPreferencesUserPreferences
        {

            private string codedCountryField;

            /// <remarks/>
            public string codedCountry
            {
                get
                {
                    return this.codedCountryField;
                }
                set
                {
                    this.codedCountryField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfo
        {

            private PNR_ReplyTravellerInfoElementManagementPassenger elementManagementPassengerField;

            private PNR_ReplyTravellerInfoPassengerData passengerDataField;

            private PNR_ReplyTravellerInfoEnhancedPassengerData enhancedPassengerDataField;

            /// <remarks/>
            public PNR_ReplyTravellerInfoElementManagementPassenger elementManagementPassenger
            {
                get
                {
                    return this.elementManagementPassengerField;
                }
                set
                {
                    this.elementManagementPassengerField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyTravellerInfoPassengerData passengerData
            {
                get
                {
                    return this.passengerDataField;
                }
                set
                {
                    this.passengerDataField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyTravellerInfoEnhancedPassengerData enhancedPassengerData
            {
                get
                {
                    return this.enhancedPassengerDataField;
                }
                set
                {
                    this.enhancedPassengerDataField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoElementManagementPassenger
        {

            private PNR_ReplyTravellerInfoElementManagementPassengerReference referenceField;

            private string segmentNameField;

            private byte lineNumberField;

            /// <remarks/>
            public PNR_ReplyTravellerInfoElementManagementPassengerReference reference
            {
                get
                {
                    return this.referenceField;
                }
                set
                {
                    this.referenceField = value;
                }
            }

            /// <remarks/>
            public string segmentName
            {
                get
                {
                    return this.segmentNameField;
                }
                set
                {
                    this.segmentNameField = value;
                }
            }

            /// <remarks/>
            public byte lineNumber
            {
                get
                {
                    return this.lineNumberField;
                }
                set
                {
                    this.lineNumberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoElementManagementPassengerReference
        {

            private string qualifierField;

            private byte numberField;

            /// <remarks/>
            public string qualifier
            {
                get
                {
                    return this.qualifierField;
                }
                set
                {
                    this.qualifierField = value;
                }
            }

            /// <remarks/>
            public byte number
            {
                get
                {
                    return this.numberField;
                }
                set
                {
                    this.numberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoPassengerData
        {

            private PNR_ReplyTravellerInfoPassengerDataTravellerInformation travellerInformationField;

            /// <remarks/>
            public PNR_ReplyTravellerInfoPassengerDataTravellerInformation travellerInformation
            {
                get
                {
                    return this.travellerInformationField;
                }
                set
                {
                    this.travellerInformationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoPassengerDataTravellerInformation
        {

            private PNR_ReplyTravellerInfoPassengerDataTravellerInformationTraveller travellerField;

            private PNR_ReplyTravellerInfoPassengerDataTravellerInformationPassenger passengerField;

            /// <remarks/>
            public PNR_ReplyTravellerInfoPassengerDataTravellerInformationTraveller traveller
            {
                get
                {
                    return this.travellerField;
                }
                set
                {
                    this.travellerField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyTravellerInfoPassengerDataTravellerInformationPassenger passenger
            {
                get
                {
                    return this.passengerField;
                }
                set
                {
                    this.passengerField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoPassengerDataTravellerInformationTraveller
        {

            private string surnameField;

            private byte quantityField;

            /// <remarks/>
            public string surname
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

            /// <remarks/>
            public byte quantity
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoPassengerDataTravellerInformationPassenger
        {

            private string firstNameField;

            /// <remarks/>
            public string firstName
            {
                get
                {
                    return this.firstNameField;
                }
                set
                {
                    this.firstNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoEnhancedPassengerData
        {

            private PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformation enhancedTravellerInformationField;

            /// <remarks/>
            public PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformation enhancedTravellerInformation
            {
                get
                {
                    return this.enhancedTravellerInformationField;
                }
                set
                {
                    this.enhancedTravellerInformationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformation
        {

            private PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformationTravellerNameInfo travellerNameInfoField;

            private PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformationOtherPaxNamesDetails otherPaxNamesDetailsField;

            /// <remarks/>
            public PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformationTravellerNameInfo travellerNameInfo
            {
                get
                {
                    return this.travellerNameInfoField;
                }
                set
                {
                    this.travellerNameInfoField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformationOtherPaxNamesDetails otherPaxNamesDetails
            {
                get
                {
                    return this.otherPaxNamesDetailsField;
                }
                set
                {
                    this.otherPaxNamesDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformationTravellerNameInfo
        {

            private byte quantityField;

            /// <remarks/>
            public byte quantity
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyTravellerInfoEnhancedPassengerDataEnhancedTravellerInformationOtherPaxNamesDetails
        {

            private string nameTypeField;

            private string referenceNameField;

            private string displayedNameField;

            private string surnameField;

            private string givenNameField;

            /// <remarks/>
            public string nameType
            {
                get
                {
                    return this.nameTypeField;
                }
                set
                {
                    this.nameTypeField = value;
                }
            }

            /// <remarks/>
            public string referenceName
            {
                get
                {
                    return this.referenceNameField;
                }
                set
                {
                    this.referenceNameField = value;
                }
            }

            /// <remarks/>
            public string displayedName
            {
                get
                {
                    return this.displayedNameField;
                }
                set
                {
                    this.displayedNameField = value;
                }
            }

            /// <remarks/>
            public string surname
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

            /// <remarks/>
            public string givenName
            {
                get
                {
                    return this.givenNameField;
                }
                set
                {
                    this.givenNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMaster
        {

            private object marker2Field;

            private PNR_ReplyDataElementsMasterDataElementsIndiv[] dataElementsIndivField;

            /// <remarks/>
            public object marker2
            {
                get
                {
                    return this.marker2Field;
                }
                set
                {
                    this.marker2Field = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("dataElementsIndiv")]
            public PNR_ReplyDataElementsMasterDataElementsIndiv[] dataElementsIndiv
            {
                get
                {
                    return this.dataElementsIndivField;
                }
                set
                {
                    this.dataElementsIndivField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndiv
        {

            private PNR_ReplyDataElementsMasterDataElementsIndivElementManagementData elementManagementDataField;

            private PNR_ReplyDataElementsMasterDataElementsIndivMiscellaneousRemarks miscellaneousRemarksField;

            private PNR_ReplyDataElementsMasterDataElementsIndivExtendedRemark extendedRemarkField;

            private PNR_ReplyDataElementsMasterDataElementsIndivTicketElement ticketElementField;

            private PNR_ReplyDataElementsMasterDataElementsIndivOtherDataFreetext otherDataFreetextField;

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivElementManagementData elementManagementData
            {
                get
                {
                    return this.elementManagementDataField;
                }
                set
                {
                    this.elementManagementDataField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivMiscellaneousRemarks miscellaneousRemarks
            {
                get
                {
                    return this.miscellaneousRemarksField;
                }
                set
                {
                    this.miscellaneousRemarksField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivExtendedRemark extendedRemark
            {
                get
                {
                    return this.extendedRemarkField;
                }
                set
                {
                    this.extendedRemarkField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivTicketElement ticketElement
            {
                get
                {
                    return this.ticketElementField;
                }
                set
                {
                    this.ticketElementField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivOtherDataFreetext otherDataFreetext
            {
                get
                {
                    return this.otherDataFreetextField;
                }
                set
                {
                    this.otherDataFreetextField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivElementManagementData
        {

            private PNR_ReplyDataElementsMasterDataElementsIndivElementManagementDataReference referenceField;

            private string segmentNameField;

            private byte lineNumberField;

            private bool lineNumberFieldSpecified;

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivElementManagementDataReference reference
            {
                get
                {
                    return this.referenceField;
                }
                set
                {
                    this.referenceField = value;
                }
            }

            /// <remarks/>
            public string segmentName
            {
                get
                {
                    return this.segmentNameField;
                }
                set
                {
                    this.segmentNameField = value;
                }
            }

            /// <remarks/>
            public byte lineNumber
            {
                get
                {
                    return this.lineNumberField;
                }
                set
                {
                    this.lineNumberField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlIgnoreAttribute()]
            public bool lineNumberSpecified
            {
                get
                {
                    return this.lineNumberFieldSpecified;
                }
                set
                {
                    this.lineNumberFieldSpecified = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivElementManagementDataReference
        {

            private string qualifierField;

            private byte numberField;

            /// <remarks/>
            public string qualifier
            {
                get
                {
                    return this.qualifierField;
                }
                set
                {
                    this.qualifierField = value;
                }
            }

            /// <remarks/>
            public byte number
            {
                get
                {
                    return this.numberField;
                }
                set
                {
                    this.numberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivMiscellaneousRemarks
        {

            private PNR_ReplyDataElementsMasterDataElementsIndivMiscellaneousRemarksRemarks remarksField;

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivMiscellaneousRemarksRemarks remarks
            {
                get
                {
                    return this.remarksField;
                }
                set
                {
                    this.remarksField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivMiscellaneousRemarksRemarks
        {

            private string typeField;

            private string freetextField;

            /// <remarks/>
            public string type
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
            public string freetext
            {
                get
                {
                    return this.freetextField;
                }
                set
                {
                    this.freetextField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivExtendedRemark
        {

            private PNR_ReplyDataElementsMasterDataElementsIndivExtendedRemarkStructuredRemark structuredRemarkField;

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivExtendedRemarkStructuredRemark structuredRemark
            {
                get
                {
                    return this.structuredRemarkField;
                }
                set
                {
                    this.structuredRemarkField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivExtendedRemarkStructuredRemark
        {

            private string typeField;

            private string freetextField;

            /// <remarks/>
            public string type
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
            public string freetext
            {
                get
                {
                    return this.freetextField;
                }
                set
                {
                    this.freetextField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivTicketElement
        {

            private PNR_ReplyDataElementsMasterDataElementsIndivTicketElementTicket ticketField;

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivTicketElementTicket ticket
            {
                get
                {
                    return this.ticketField;
                }
                set
                {
                    this.ticketField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivTicketElementTicket
        {

            private string indicatorField;

            private uint dateField;

            private string officeIdField;

            /// <remarks/>
            public string indicator
            {
                get
                {
                    return this.indicatorField;
                }
                set
                {
                    this.indicatorField = value;
                }
            }

            /// <remarks/>
            public uint date
            {
                get
                {
                    return this.dateField;
                }
                set
                {
                    this.dateField = value;
                }
            }

            /// <remarks/>
            public string officeId
            {
                get
                {
                    return this.officeIdField;
                }
                set
                {
                    this.officeIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivOtherDataFreetext
        {

            private PNR_ReplyDataElementsMasterDataElementsIndivOtherDataFreetextFreetextDetail freetextDetailField;

            private string longFreetextField;

            /// <remarks/>
            public PNR_ReplyDataElementsMasterDataElementsIndivOtherDataFreetextFreetextDetail freetextDetail
            {
                get
                {
                    return this.freetextDetailField;
                }
                set
                {
                    this.freetextDetailField = value;
                }
            }

            /// <remarks/>
            public string longFreetext
            {
                get
                {
                    return this.longFreetextField;
                }
                set
                {
                    this.longFreetextField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMasterDataElementsIndivOtherDataFreetextFreetextDetail
        {

            private byte subjectQualifierField;

            private string typeField;

            /// <remarks/>
            public byte subjectQualifier
            {
                get
                {
                    return this.subjectQualifierField;
                }
                set
                {
                    this.subjectQualifierField = value;
                }
            }

            /// <remarks/>
            public string type
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


    }
}
