using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class PNRAddElementsConfirmError
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

            private PNR_ReplyGeneralErrorInfo generalErrorInfoField;

            private PNR_ReplyPnrHeader pnrHeaderField;

            private PNR_ReplySecurityInformation securityInformationField;

            private PNR_ReplySbrPOSDetails sbrPOSDetailsField;

            private PNR_ReplySbrCreationPosDetails sbrCreationPosDetailsField;

            private PNR_ReplySbrUpdatorPosDetails sbrUpdatorPosDetailsField;

            private PNR_ReplyTravellerInfo travellerInfoField;

            private PNR_ReplyOriginDestinationDetails originDestinationDetailsField;

            private PNR_ReplyDataElementsMaster dataElementsMasterField;

            /// <remarks/>
            public PNR_ReplyGeneralErrorInfo generalErrorInfo
            {
                get
                {
                    return this.generalErrorInfoField;
                }
                set
                {
                    this.generalErrorInfoField = value;
                }
            }

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
            public PNR_ReplyOriginDestinationDetails originDestinationDetails
            {
                get
                {
                    return this.originDestinationDetailsField;
                }
                set
                {
                    this.originDestinationDetailsField = value;
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
        public partial class PNR_ReplyGeneralErrorInfo
        {

            private PNR_ReplyGeneralErrorInfoErrorOrWarningCodeDetails errorOrWarningCodeDetailsField;

            private PNR_ReplyGeneralErrorInfoErrorWarningDescription errorWarningDescriptionField;

            /// <remarks/>
            public PNR_ReplyGeneralErrorInfoErrorOrWarningCodeDetails errorOrWarningCodeDetails
            {
                get
                {
                    return this.errorOrWarningCodeDetailsField;
                }
                set
                {
                    this.errorOrWarningCodeDetailsField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyGeneralErrorInfoErrorWarningDescription errorWarningDescription
            {
                get
                {
                    return this.errorWarningDescriptionField;
                }
                set
                {
                    this.errorWarningDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyGeneralErrorInfoErrorOrWarningCodeDetails
        {

            private PNR_ReplyGeneralErrorInfoErrorOrWarningCodeDetailsErrorDetails errorDetailsField;

            /// <remarks/>
            public PNR_ReplyGeneralErrorInfoErrorOrWarningCodeDetailsErrorDetails errorDetails
            {
                get
                {
                    return this.errorDetailsField;
                }
                set
                {
                    this.errorDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyGeneralErrorInfoErrorOrWarningCodeDetailsErrorDetails
        {

            private ushort errorCodeField;

            private string errorCategoryField;

            private string errorCodeOwnerField;

            /// <remarks/>
            public ushort errorCode
            {
                get
                {
                    return this.errorCodeField;
                }
                set
                {
                    this.errorCodeField = value;
                }
            }

            /// <remarks/>
            public string errorCategory
            {
                get
                {
                    return this.errorCategoryField;
                }
                set
                {
                    this.errorCategoryField = value;
                }
            }

            /// <remarks/>
            public string errorCodeOwner
            {
                get
                {
                    return this.errorCodeOwnerField;
                }
                set
                {
                    this.errorCodeOwnerField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyGeneralErrorInfoErrorWarningDescription
        {

            private PNR_ReplyGeneralErrorInfoErrorWarningDescriptionFreeTextDetails freeTextDetailsField;

            private string[] freeTextField;

            /// <remarks/>
            public PNR_ReplyGeneralErrorInfoErrorWarningDescriptionFreeTextDetails freeTextDetails
            {
                get
                {
                    return this.freeTextDetailsField;
                }
                set
                {
                    this.freeTextDetailsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("freeText")]
            public string[] freeText
            {
                get
                {
                    return this.freeTextField;
                }
                set
                {
                    this.freeTextField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyGeneralErrorInfoErrorWarningDescriptionFreeTextDetails
        {

            private byte textSubjectQualifierField;

            private string sourceField;

            private byte encodingField;

            /// <remarks/>
            public byte textSubjectQualifier
            {
                get
                {
                    return this.textSubjectQualifierField;
                }
                set
                {
                    this.textSubjectQualifierField = value;
                }
            }

            /// <remarks/>
            public string source
            {
                get
                {
                    return this.sourceField;
                }
                set
                {
                    this.sourceField = value;
                }
            }

            /// <remarks/>
            public byte encoding
            {
                get
                {
                    return this.encodingField;
                }
                set
                {
                    this.encodingField = value;
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
        public partial class PNR_ReplyOriginDestinationDetails
        {

            private object originDestinationField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfo itineraryInfoField;

            /// <remarks/>
            public object originDestination
            {
                get
                {
                    return this.originDestinationField;
                }
                set
                {
                    this.originDestinationField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfo itineraryInfo
            {
                get
                {
                    return this.itineraryInfoField;
                }
                set
                {
                    this.itineraryInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfo
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoElementManagementItinerary elementManagementItineraryField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProduct travelProductField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoItineraryMessageAction itineraryMessageActionField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoRelatedProduct relatedProductField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoSelectionDetails selectionDetailsField;

            private object itineraryfreeFormTextField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProduct hotelProductField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformations rateInformationsField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoGeneralOption[] generalOptionField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfo hotelReservationInfoField;

            private object markerRailTourField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoReferenceForSegment referenceForSegmentField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoElementManagementItinerary elementManagementItinerary
            {
                get
                {
                    return this.elementManagementItineraryField;
                }
                set
                {
                    this.elementManagementItineraryField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProduct travelProduct
            {
                get
                {
                    return this.travelProductField;
                }
                set
                {
                    this.travelProductField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoItineraryMessageAction itineraryMessageAction
            {
                get
                {
                    return this.itineraryMessageActionField;
                }
                set
                {
                    this.itineraryMessageActionField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoRelatedProduct relatedProduct
            {
                get
                {
                    return this.relatedProductField;
                }
                set
                {
                    this.relatedProductField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoSelectionDetails selectionDetails
            {
                get
                {
                    return this.selectionDetailsField;
                }
                set
                {
                    this.selectionDetailsField = value;
                }
            }

            /// <remarks/>
            public object itineraryfreeFormText
            {
                get
                {
                    return this.itineraryfreeFormTextField;
                }
                set
                {
                    this.itineraryfreeFormTextField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProduct hotelProduct
            {
                get
                {
                    return this.hotelProductField;
                }
                set
                {
                    this.hotelProductField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformations rateInformations
            {
                get
                {
                    return this.rateInformationsField;
                }
                set
                {
                    this.rateInformationsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("generalOption")]
            public PNR_ReplyOriginDestinationDetailsItineraryInfoGeneralOption[] generalOption
            {
                get
                {
                    return this.generalOptionField;
                }
                set
                {
                    this.generalOptionField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfo hotelReservationInfo
            {
                get
                {
                    return this.hotelReservationInfoField;
                }
                set
                {
                    this.hotelReservationInfoField = value;
                }
            }

            /// <remarks/>
            public object markerRailTour
            {
                get
                {
                    return this.markerRailTourField;
                }
                set
                {
                    this.markerRailTourField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoReferenceForSegment referenceForSegment
            {
                get
                {
                    return this.referenceForSegmentField;
                }
                set
                {
                    this.referenceForSegmentField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoElementManagementItinerary
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoElementManagementItineraryReference referenceField;

            private string segmentNameField;

            private byte lineNumberField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoElementManagementItineraryReference reference
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
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoElementManagementItineraryReference
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
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProduct
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductProduct productField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductBoardpointDetail boardpointDetailField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductCompanyDetail companyDetailField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductProduct product
            {
                get
                {
                    return this.productField;
                }
                set
                {
                    this.productField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductBoardpointDetail boardpointDetail
            {
                get
                {
                    return this.boardpointDetailField;
                }
                set
                {
                    this.boardpointDetailField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductCompanyDetail companyDetail
            {
                get
                {
                    return this.companyDetailField;
                }
                set
                {
                    this.companyDetailField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductProduct
        {

            private uint depDateField;

            private uint arrDateField;

            /// <remarks/>
            public uint depDate
            {
                get
                {
                    return this.depDateField;
                }
                set
                {
                    this.depDateField = value;
                }
            }

            /// <remarks/>
            public uint arrDate
            {
                get
                {
                    return this.arrDateField;
                }
                set
                {
                    this.arrDateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductBoardpointDetail
        {

            private string cityCodeField;

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
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoTravelProductCompanyDetail
        {

            private string identificationField;

            /// <remarks/>
            public string identification
            {
                get
                {
                    return this.identificationField;
                }
                set
                {
                    this.identificationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoItineraryMessageAction
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoItineraryMessageActionBusiness businessField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoItineraryMessageActionBusiness business
            {
                get
                {
                    return this.businessField;
                }
                set
                {
                    this.businessField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoItineraryMessageActionBusiness
        {

            private byte functionField;

            /// <remarks/>
            public byte function
            {
                get
                {
                    return this.functionField;
                }
                set
                {
                    this.functionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoRelatedProduct
        {

            private byte quantityField;

            private string statusField;

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

            /// <remarks/>
            public string status
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoSelectionDetails
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoSelectionDetailsSelection selectionField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoSelectionDetailsSelection selection
            {
                get
                {
                    return this.selectionField;
                }
                set
                {
                    this.selectionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoSelectionDetailsSelection
        {

            private string optionField;

            /// <remarks/>
            public string option
            {
                get
                {
                    return this.optionField;
                }
                set
                {
                    this.optionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProduct
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductProperty propertyField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductHotelRoom hotelRoomField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductNegotiated negotiatedField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductOtherHotelInfo otherHotelInfoField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductProperty property
            {
                get
                {
                    return this.propertyField;
                }
                set
                {
                    this.propertyField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductHotelRoom hotelRoom
            {
                get
                {
                    return this.hotelRoomField;
                }
                set
                {
                    this.hotelRoomField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductNegotiated negotiated
            {
                get
                {
                    return this.negotiatedField;
                }
                set
                {
                    this.negotiatedField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductOtherHotelInfo otherHotelInfo
            {
                get
                {
                    return this.otherHotelInfoField;
                }
                set
                {
                    this.otherHotelInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductProperty
        {

            private string providerNameField;

            private string codeField;

            private string nameField;

            /// <remarks/>
            public string providerName
            {
                get
                {
                    return this.providerNameField;
                }
                set
                {
                    this.providerNameField = value;
                }
            }

            /// <remarks/>
            public string code
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
            public string name
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductHotelRoom
        {

            private byte occupancyField;

            private string typeCodeField;

            /// <remarks/>
            public byte occupancy
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
            public string typeCode
            {
                get
                {
                    return this.typeCodeField;
                }
                set
                {
                    this.typeCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductNegotiated
        {

            private byte rateCodeField;

            /// <remarks/>
            public byte rateCode
            {
                get
                {
                    return this.rateCodeField;
                }
                set
                {
                    this.rateCodeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelProductOtherHotelInfo
        {

            private string currencyCodeField;

            /// <remarks/>
            public string currencyCode
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformations
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformationsRatePrice ratePriceField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformationsRateInfo rateInfoField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformationsRatePrice ratePrice
            {
                get
                {
                    return this.ratePriceField;
                }
                set
                {
                    this.ratePriceField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformationsRateInfo rateInfo
            {
                get
                {
                    return this.rateInfoField;
                }
                set
                {
                    this.rateInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformationsRatePrice
        {

            private decimal rateAmountField;

            /// <remarks/>
            public decimal rateAmount
            {
                get
                {
                    return this.rateAmountField;
                }
                set
                {
                    this.rateAmountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoRateInformationsRateInfo
        {

            private string ratePlanField;

            /// <remarks/>
            public string ratePlan
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoGeneralOption
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoGeneralOptionOptionDetail optionDetailField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoGeneralOptionOptionDetail optionDetail
            {
                get
                {
                    return this.optionDetailField;
                }
                set
                {
                    this.optionDetailField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoGeneralOptionOptionDetail
        {

            private string typeField;

            private string[] freetextField;

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
            [System.Xml.Serialization.XmlElementAttribute("freetext")]
            public string[] freetext
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
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfo
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoHotelPropertyInfo hotelPropertyInfoField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCompanyIdentification companyIdentificationField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDates requestedDatesField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetails roomRateDetailsField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCancelOrConfirmNbr cancelOrConfirmNbrField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoBookingSource bookingSourceField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDeposit guaranteeOrDepositField;

            private object bookingIndicatorField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoHotelPropertyInfo hotelPropertyInfo
            {
                get
                {
                    return this.hotelPropertyInfoField;
                }
                set
                {
                    this.hotelPropertyInfoField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCompanyIdentification companyIdentification
            {
                get
                {
                    return this.companyIdentificationField;
                }
                set
                {
                    this.companyIdentificationField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDates requestedDates
            {
                get
                {
                    return this.requestedDatesField;
                }
                set
                {
                    this.requestedDatesField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetails roomRateDetails
            {
                get
                {
                    return this.roomRateDetailsField;
                }
                set
                {
                    this.roomRateDetailsField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCancelOrConfirmNbr cancelOrConfirmNbr
            {
                get
                {
                    return this.cancelOrConfirmNbrField;
                }
                set
                {
                    this.cancelOrConfirmNbrField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoBookingSource bookingSource
            {
                get
                {
                    return this.bookingSourceField;
                }
                set
                {
                    this.bookingSourceField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDeposit guaranteeOrDeposit
            {
                get
                {
                    return this.guaranteeOrDepositField;
                }
                set
                {
                    this.guaranteeOrDepositField = value;
                }
            }

            /// <remarks/>
            public object bookingIndicator
            {
                get
                {
                    return this.bookingIndicatorField;
                }
                set
                {
                    this.bookingIndicatorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoHotelPropertyInfo
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoHotelPropertyInfoHotelReference hotelReferenceField;

            private string hotelNameField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoHotelPropertyInfoHotelReference hotelReference
            {
                get
                {
                    return this.hotelReferenceField;
                }
                set
                {
                    this.hotelReferenceField = value;
                }
            }

            /// <remarks/>
            public string hotelName
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoHotelPropertyInfoHotelReference
        {

            private string chainCodeField;

            private string cityCodeField;

            private string hotelCodeField;

            /// <remarks/>
            public string chainCode
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

            /// <remarks/>
            public string hotelCode
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCompanyIdentification
        {

            private string travelSectorField;

            private string companyCodeContextField;

            private string companyCodeField;

            private string companyNameField;

            /// <remarks/>
            public string travelSector
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

            /// <remarks/>
            public string companyCodeContext
            {
                get
                {
                    return this.companyCodeContextField;
                }
                set
                {
                    this.companyCodeContextField = value;
                }
            }

            /// <remarks/>
            public string companyCode
            {
                get
                {
                    return this.companyCodeField;
                }
                set
                {
                    this.companyCodeField = value;
                }
            }

            /// <remarks/>
            public string companyName
            {
                get
                {
                    return this.companyNameField;
                }
                set
                {
                    this.companyNameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDates
        {

            private string businessSemanticField;

            private string timeModeField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDatesBeginDateTime beginDateTimeField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDatesEndDateTime endDateTimeField;

            /// <remarks/>
            public string businessSemantic
            {
                get
                {
                    return this.businessSemanticField;
                }
                set
                {
                    this.businessSemanticField = value;
                }
            }

            /// <remarks/>
            public string timeMode
            {
                get
                {
                    return this.timeModeField;
                }
                set
                {
                    this.timeModeField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDatesBeginDateTime beginDateTime
            {
                get
                {
                    return this.beginDateTimeField;
                }
                set
                {
                    this.beginDateTimeField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDatesEndDateTime endDateTime
            {
                get
                {
                    return this.endDateTimeField;
                }
                set
                {
                    this.endDateTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDatesBeginDateTime
        {

            private ushort yearField;

            private byte monthField;

            private byte dayField;

            /// <remarks/>
            public ushort year
            {
                get
                {
                    return this.yearField;
                }
                set
                {
                    this.yearField = value;
                }
            }

            /// <remarks/>
            public byte month
            {
                get
                {
                    return this.monthField;
                }
                set
                {
                    this.monthField = value;
                }
            }

            /// <remarks/>
            public byte day
            {
                get
                {
                    return this.dayField;
                }
                set
                {
                    this.dayField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRequestedDatesEndDateTime
        {

            private ushort yearField;

            private byte monthField;

            private byte dayField;

            /// <remarks/>
            public ushort year
            {
                get
                {
                    return this.yearField;
                }
                set
                {
                    this.yearField = value;
                }
            }

            /// <remarks/>
            public byte month
            {
                get
                {
                    return this.monthField;
                }
                set
                {
                    this.monthField = value;
                }
            }

            /// <remarks/>
            public byte day
            {
                get
                {
                    return this.dayField;
                }
                set
                {
                    this.dayField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetails
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformation roomInformationField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsTariffDetails tariffDetailsField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformation roomInformation
            {
                get
                {
                    return this.roomInformationField;
                }
                set
                {
                    this.roomInformationField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsTariffDetails tariffDetails
            {
                get
                {
                    return this.tariffDetailsField;
                }
                set
                {
                    this.tariffDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformation
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformationRoomRateIdentifier roomRateIdentifierField;

            private string bookingCodeField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformationGuestCountDetails guestCountDetailsField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformationRoomRateIdentifier roomRateIdentifier
            {
                get
                {
                    return this.roomRateIdentifierField;
                }
                set
                {
                    this.roomRateIdentifierField = value;
                }
            }

            /// <remarks/>
            public string bookingCode
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
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformationGuestCountDetails guestCountDetails
            {
                get
                {
                    return this.guestCountDetailsField;
                }
                set
                {
                    this.guestCountDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformationRoomRateIdentifier
        {

            private string roomTypeField;

            private byte ratePlanCodeField;

            /// <remarks/>
            public string roomType
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
            public byte ratePlanCode
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsRoomInformationGuestCountDetails
        {

            private byte numberOfUnitField;

            private string unitQualifierField;

            /// <remarks/>
            public byte numberOfUnit
            {
                get
                {
                    return this.numberOfUnitField;
                }
                set
                {
                    this.numberOfUnitField = value;
                }
            }

            /// <remarks/>
            public string unitQualifier
            {
                get
                {
                    return this.unitQualifierField;
                }
                set
                {
                    this.unitQualifierField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsTariffDetails
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsTariffDetailsTariffInfo tariffInfoField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsTariffDetailsTariffInfo tariffInfo
            {
                get
                {
                    return this.tariffInfoField;
                }
                set
                {
                    this.tariffInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoRoomRateDetailsTariffDetailsTariffInfo
        {

            private byte rateTypeField;

            private decimal amountField;

            private string currencyField;

            private string ratePlanIndicatorField;

            /// <remarks/>
            public byte rateType
            {
                get
                {
                    return this.rateTypeField;
                }
                set
                {
                    this.rateTypeField = value;
                }
            }

            /// <remarks/>
            public decimal amount
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
            public string currency
            {
                get
                {
                    return this.currencyField;
                }
                set
                {
                    this.currencyField = value;
                }
            }

            /// <remarks/>
            public string ratePlanIndicator
            {
                get
                {
                    return this.ratePlanIndicatorField;
                }
                set
                {
                    this.ratePlanIndicatorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCancelOrConfirmNbr
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCancelOrConfirmNbrReservation reservationField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCancelOrConfirmNbrReservation reservation
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
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoCancelOrConfirmNbrReservation
        {

            private string companyIdField;

            private uint controlNumberField;

            private byte controlTypeField;

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
            public uint controlNumber
            {
                get
                {
                    return this.controlNumberField;
                }
                set
                {
                    this.controlNumberField = value;
                }
            }

            /// <remarks/>
            public byte controlType
            {
                get
                {
                    return this.controlTypeField;
                }
                set
                {
                    this.controlTypeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoBookingSource
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoBookingSourceOriginIdentification originIdentificationField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoBookingSourceOriginIdentification originIdentification
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
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoBookingSourceOriginIdentification
        {

            private uint originatorIdField;

            /// <remarks/>
            public uint originatorId
            {
                get
                {
                    return this.originatorIdField;
                }
                set
                {
                    this.originatorIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDeposit
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositPaymentInfo paymentInfoField;

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositCreditCardInfo creditCardInfoField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositPaymentInfo paymentInfo
            {
                get
                {
                    return this.paymentInfoField;
                }
                set
                {
                    this.paymentInfoField = value;
                }
            }

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositCreditCardInfo creditCardInfo
            {
                get
                {
                    return this.creditCardInfoField;
                }
                set
                {
                    this.creditCardInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositPaymentInfo
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositPaymentInfoPaymentDetails paymentDetailsField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositPaymentInfoPaymentDetails paymentDetails
            {
                get
                {
                    return this.paymentDetailsField;
                }
                set
                {
                    this.paymentDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositPaymentInfoPaymentDetails
        {

            private byte formOfPaymentCodeField;

            private byte paymentTypeField;

            private byte serviceToPayField;

            /// <remarks/>
            public byte formOfPaymentCode
            {
                get
                {
                    return this.formOfPaymentCodeField;
                }
                set
                {
                    this.formOfPaymentCodeField = value;
                }
            }

            /// <remarks/>
            public byte paymentType
            {
                get
                {
                    return this.paymentTypeField;
                }
                set
                {
                    this.paymentTypeField = value;
                }
            }

            /// <remarks/>
            public byte serviceToPay
            {
                get
                {
                    return this.serviceToPayField;
                }
                set
                {
                    this.serviceToPayField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositCreditCardInfo
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositCreditCardInfoFormOfPayment formOfPaymentField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositCreditCardInfoFormOfPayment formOfPayment
            {
                get
                {
                    return this.formOfPaymentField;
                }
                set
                {
                    this.formOfPaymentField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoHotelReservationInfoGuaranteeOrDepositCreditCardInfoFormOfPayment
        {

            private string typeField;

            private string vendorCodeField;

            private string creditCardNumberField;

            private ushort expiryDateField;

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
            public string vendorCode
            {
                get
                {
                    return this.vendorCodeField;
                }
                set
                {
                    this.vendorCodeField = value;
                }
            }

            /// <remarks/>
            public string creditCardNumber
            {
                get
                {
                    return this.creditCardNumberField;
                }
                set
                {
                    this.creditCardNumberField = value;
                }
            }

            /// <remarks/>
            public ushort expiryDate
            {
                get
                {
                    return this.expiryDateField;
                }
                set
                {
                    this.expiryDateField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoReferenceForSegment
        {

            private PNR_ReplyOriginDestinationDetailsItineraryInfoReferenceForSegmentReference referenceField;

            /// <remarks/>
            public PNR_ReplyOriginDestinationDetailsItineraryInfoReferenceForSegmentReference reference
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyOriginDestinationDetailsItineraryInfoReferenceForSegmentReference
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
