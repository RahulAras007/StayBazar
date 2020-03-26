using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class PNRAddElementsConfirm
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

            private PNR_ReplyOriginDestinationDetails originDestinationDetailsField;

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

            private string controlNumberField;

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
            public string controlNumber
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySecurityInformation
        {

            private PNR_ReplySecurityInformationResponsibilityInformation responsibilityInformationField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySecurityInformationResponsibilityInformation
        {

            private string typeOfPnrElementField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrPOSDetails
        {

            private PNR_ReplySbrPOSDetailsSbrUserIdentificationOwn sbrUserIdentificationOwnField;

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
        public partial class PNR_ReplySbrCreationPosDetails
        {

            private PNR_ReplySbrCreationPosDetailsSbrUserIdentificationOwn sbrUserIdentificationOwnField;

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
        public partial class PNR_ReplySbrUpdatorPosDetails
        {

            private PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwn sbrUserIdentificationOwnField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwn
        {

            private PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwnOriginIdentification originIdentificationField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplySbrUpdatorPosDetailsSbrUserIdentificationOwnOriginIdentification
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
        public partial class PNR_ReplyOriginDestinationDetails
        {

            private object originDestinationField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/PNRACC_15_1_1A")]
        public partial class PNR_ReplyDataElementsMaster
        {

            private object marker2Field;

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
        }




    }
}
