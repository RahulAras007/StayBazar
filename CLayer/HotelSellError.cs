using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    

    public class HotelSellError
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

            private Hotel_SellReply hotel_SellReplyField;

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
            public Hotel_SellReply Hotel_SellReply
            {
                get
                {
                    return this.hotel_SellReplyField;
                }
                set
                {
                    this.hotel_SellReplyField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A", IsNullable = false)]
        public partial class Hotel_SellReply
        {

            private Hotel_SellReplyErrorGroup errorGroupField;

            private Hotel_SellReplyBookingTypeIndicator bookingTypeIndicatorField;

            /// <remarks/>
            public Hotel_SellReplyErrorGroup errorGroup
            {
                get
                {
                    return this.errorGroupField;
                }
                set
                {
                    this.errorGroupField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyBookingTypeIndicator bookingTypeIndicator
            {
                get
                {
                    return this.bookingTypeIndicatorField;
                }
                set
                {
                    this.bookingTypeIndicatorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyErrorGroup
        {

            private Hotel_SellReplyErrorGroupMessageErrorInformation messageErrorInformationField;

            private Hotel_SellReplyErrorGroupErrorDescription errorDescriptionField;

            /// <remarks/>
            public Hotel_SellReplyErrorGroupMessageErrorInformation messageErrorInformation
            {
                get
                {
                    return this.messageErrorInformationField;
                }
                set
                {
                    this.messageErrorInformationField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyErrorGroupErrorDescription errorDescription
            {
                get
                {
                    return this.errorDescriptionField;
                }
                set
                {
                    this.errorDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyErrorGroupMessageErrorInformation
        {

            private Hotel_SellReplyErrorGroupMessageErrorInformationErrorDetails errorDetailsField;

            /// <remarks/>
            public Hotel_SellReplyErrorGroupMessageErrorInformationErrorDetails errorDetails
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyErrorGroupMessageErrorInformationErrorDetails
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyErrorGroupErrorDescription
        {

            private Hotel_SellReplyErrorGroupErrorDescriptionFreeTextDetails freeTextDetailsField;

            private string freeTextField;

            /// <remarks/>
            public Hotel_SellReplyErrorGroupErrorDescriptionFreeTextDetails freeTextDetails
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
            public string freeText
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyErrorGroupErrorDescriptionFreeTextDetails
        {

            private byte textSubjectQualifierField;

            private string informationTypeField;

            private string languageField;

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
            public string informationType
            {
                get
                {
                    return this.informationTypeField;
                }
                set
                {
                    this.informationTypeField = value;
                }
            }

            /// <remarks/>
            public string language
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyBookingTypeIndicator
        {

            private Hotel_SellReplyBookingTypeIndicatorNumberOfRooms numberOfRoomsField;

            /// <remarks/>
            public Hotel_SellReplyBookingTypeIndicatorNumberOfRooms numberOfRooms
            {
                get
                {
                    return this.numberOfRoomsField;
                }
                set
                {
                    this.numberOfRoomsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyBookingTypeIndicatorNumberOfRooms
        {

            private byte quantityField;

            private string statusCodeField;

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
            public string statusCode
            {
                get
                {
                    return this.statusCodeField;
                }
                set
                {
                    this.statusCodeField = value;
                }
            }
        }


    }

}
