using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
   public  class HotelSell
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

            private Hotel_SellReplyBookingTypeIndicator bookingTypeIndicatorField;

            private Hotel_SellReplyRoomStayData roomStayDataField;

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

            /// <remarks/>
            public Hotel_SellReplyRoomStayData roomStayData
            {
                get
                {
                    return this.roomStayDataField;
                }
                set
                {
                    this.roomStayDataField = value;
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

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayData
        {

            private object markerRoomStayDataField;

            private Hotel_SellReplyRoomStayDataPnrInfo pnrInfoField;

            private Hotel_SellReplyRoomStayDataGlobalBookingInfo globalBookingInfoField;

            private Hotel_SellReplyRoomStayDataRoomListInfo roomListInfoField;

            /// <remarks/>
            public object markerRoomStayData
            {
                get
                {
                    return this.markerRoomStayDataField;
                }
                set
                {
                    this.markerRoomStayDataField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataPnrInfo pnrInfo
            {
                get
                {
                    return this.pnrInfoField;
                }
                set
                {
                    this.pnrInfoField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfo globalBookingInfo
            {
                get
                {
                    return this.globalBookingInfoField;
                }
                set
                {
                    this.globalBookingInfoField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfo roomListInfo
            {
                get
                {
                    return this.roomListInfoField;
                }
                set
                {
                    this.roomListInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataPnrInfo
        {

            private Hotel_SellReplyRoomStayDataPnrInfoTattooReference tattooReferenceField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataPnrInfoTattooReference tattooReference
            {
                get
                {
                    return this.tattooReferenceField;
                }
                set
                {
                    this.tattooReferenceField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataPnrInfoTattooReference
        {

            private Hotel_SellReplyRoomStayDataPnrInfoTattooReferenceReferenceDetails referenceDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataPnrInfoTattooReferenceReferenceDetails referenceDetails
            {
                get
                {
                    return this.referenceDetailsField;
                }
                set
                {
                    this.referenceDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataPnrInfoTattooReferenceReferenceDetails
        {

            private string typeField;

            private byte valueField;

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
            public byte value
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfo
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoHotelPropertyInfo hotelPropertyInfoField;

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoForceSellIndicator forceSellIndicatorField;

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoIndividualCompanyId individualCompanyIdField;

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingInfo bookingInfoField;

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingSource bookingSourceField;

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformation globalPriceInformationField;

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativeParties representativePartiesField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoHotelPropertyInfo hotelPropertyInfo
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
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoForceSellIndicator forceSellIndicator
            {
                get
                {
                    return this.forceSellIndicatorField;
                }
                set
                {
                    this.forceSellIndicatorField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoIndividualCompanyId individualCompanyId
            {
                get
                {
                    return this.individualCompanyIdField;
                }
                set
                {
                    this.individualCompanyIdField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingInfo bookingInfo
            {
                get
                {
                    return this.bookingInfoField;
                }
                set
                {
                    this.bookingInfoField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingSource bookingSource
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
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformation globalPriceInformation
            {
                get
                {
                    return this.globalPriceInformationField;
                }
                set
                {
                    this.globalPriceInformationField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativeParties representativeParties
            {
                get
                {
                    return this.representativePartiesField;
                }
                set
                {
                    this.representativePartiesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoHotelPropertyInfo
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoHotelPropertyInfoHotelReference hotelReferenceField;

            private string hotelNameField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoHotelPropertyInfoHotelReference hotelReference
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoHotelPropertyInfoHotelReference
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoForceSellIndicator
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoForceSellIndicatorStatusDetails statusDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoForceSellIndicatorStatusDetails statusDetails
            {
                get
                {
                    return this.statusDetailsField;
                }
                set
                {
                    this.statusDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoForceSellIndicatorStatusDetails
        {

            private string indicatorField;

            private byte actionField;

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
            public byte action
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoIndividualCompanyId
        {

            private string companyNameField;

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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingInfo
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingInfoReservation reservationField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingInfoReservation reservation
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingInfoReservation
        {

            private string companyIdField;

            private string controlNumberField;

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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingSource
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingSourceOriginIdentification originIdentificationField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingSourceOriginIdentification originIdentification
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoBookingSourceOriginIdentification
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformation
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformationGlobalPrice globalPriceField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformationGlobalPrice globalPrice
            {
                get
                {
                    return this.globalPriceField;
                }
                set
                {
                    this.globalPriceField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformationGlobalPrice
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformationGlobalPriceTariffInfo tariffInfoField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformationGlobalPriceTariffInfo tariffInfo
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoGlobalPriceInformationGlobalPriceTariffInfo
        {

            private string currencyField;

            private byte dailyTotalIndicatorField;

            private decimal totalAmountField;

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
            public byte dailyTotalIndicator
            {
                get
                {
                    return this.dailyTotalIndicatorField;
                }
                set
                {
                    this.dailyTotalIndicatorField = value;
                }
            }

            /// <remarks/>
            public decimal totalAmount
            {
                get
                {
                    return this.totalAmountField;
                }
                set
                {
                    this.totalAmountField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativeParties
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantList occupantListField;

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantPreferences occupantPreferencesField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantList occupantList
            {
                get
                {
                    return this.occupantListField;
                }
                set
                {
                    this.occupantListField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantPreferences occupantPreferences
            {
                get
                {
                    return this.occupantPreferencesField;
                }
                set
                {
                    this.occupantPreferencesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantList
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantListPassengerReference passengerReferenceField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantListPassengerReference passengerReference
            {
                get
                {
                    return this.passengerReferenceField;
                }
                set
                {
                    this.passengerReferenceField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantListPassengerReference
        {

            private string typeField;

            private byte valueField;

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
            public byte value
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantPreferences
        {

            private Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantPreferencesOccupantPreferences occupantPreferencesField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantPreferencesOccupantPreferences occupantPreferences
            {
                get
                {
                    return this.occupantPreferencesField;
                }
                set
                {
                    this.occupantPreferencesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataGlobalBookingInfoRepresentativePartiesOccupantPreferencesOccupantPreferences
        {

            private string occupantLanguageField;

            /// <remarks/>
            public string occupantLanguage
            {
                get
                {
                    return this.occupantLanguageField;
                }
                set
                {
                    this.occupantLanguageField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfo
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRoomStayIndex roomStayIndexField;

            private Hotel_SellReplyRoomStayDataRoomListInfoMarkLinesAndRateDesc markLinesAndRateDescField;

            private Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkup commissionAndMarkupField;

            private Hotel_SellReplyRoomStayDataRoomListInfoCancellationPolicies[] cancellationPoliciesField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformation requestableInformationField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRateChanges rateChangesField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRoomStayIndex roomStayIndex
            {
                get
                {
                    return this.roomStayIndexField;
                }
                set
                {
                    this.roomStayIndexField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoMarkLinesAndRateDesc markLinesAndRateDesc
            {
                get
                {
                    return this.markLinesAndRateDescField;
                }
                set
                {
                    this.markLinesAndRateDescField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkup commissionAndMarkup
            {
                get
                {
                    return this.commissionAndMarkupField;
                }
                set
                {
                    this.commissionAndMarkupField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("cancellationPolicies")]
            public Hotel_SellReplyRoomStayDataRoomListInfoCancellationPolicies[] cancellationPolicies
            {
                get
                {
                    return this.cancellationPoliciesField;
                }
                set
                {
                    this.cancellationPoliciesField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformation requestableInformation
            {
                get
                {
                    return this.requestableInformationField;
                }
                set
                {
                    this.requestableInformationField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRateChanges rateChanges
            {
                get
                {
                    return this.rateChangesField;
                }
                set
                {
                    this.rateChangesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRoomStayIndex
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRoomStayIndexSequenceDetails sequenceDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRoomStayIndexSequenceDetails sequenceDetails
            {
                get
                {
                    return this.sequenceDetailsField;
                }
                set
                {
                    this.sequenceDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRoomStayIndexSequenceDetails
        {

            private byte numberField;

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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoMarkLinesAndRateDesc
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoMarkLinesAndRateDescFreeTextQualification freeTextQualificationField;

            private string[] freeTextField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoMarkLinesAndRateDescFreeTextQualification freeTextQualification
            {
                get
                {
                    return this.freeTextQualificationField;
                }
                set
                {
                    this.freeTextQualificationField = value;
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoMarkLinesAndRateDescFreeTextQualification
        {

            private byte textSubjectQualifierField;

            private string informationTypeIdField;

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
            public string informationTypeId
            {
                get
                {
                    return this.informationTypeIdField;
                }
                set
                {
                    this.informationTypeIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkup
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkupCommissionInfo commissionInfoField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkupCommissionInfo commissionInfo
            {
                get
                {
                    return this.commissionInfoField;
                }
                set
                {
                    this.commissionInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkupCommissionInfo
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkupCommissionInfoCommissionDetails commissionDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkupCommissionInfoCommissionDetails commissionDetails
            {
                get
                {
                    return this.commissionDetailsField;
                }
                set
                {
                    this.commissionDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoCommissionAndMarkupCommissionInfoCommissionDetails
        {

            private string typeField;

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

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoCancellationPolicies
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancellationDateTime cancellationDateTimeField;

            private Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancelationPolicyDescription cancelationPolicyDescriptionField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancellationDateTime cancellationDateTime
            {
                get
                {
                    return this.cancellationDateTimeField;
                }
                set
                {
                    this.cancellationDateTimeField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancelationPolicyDescription cancelationPolicyDescription
            {
                get
                {
                    return this.cancelationPolicyDescriptionField;
                }
                set
                {
                    this.cancelationPolicyDescriptionField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancellationDateTime
        {

            private string businessSemanticField;

            private string timeModeField;

            private Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancellationDateTimeDateTime dateTimeField;

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
            public Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancellationDateTimeDateTime dateTime
            {
                get
                {
                    return this.dateTimeField;
                }
                set
                {
                    this.dateTimeField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancellationDateTimeDateTime
        {

            private ushort yearField;

            private byte monthField;

            private byte dayField;

            private byte hourField;

            private byte minutesField;

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

            /// <remarks/>
            public byte hour
            {
                get
                {
                    return this.hourField;
                }
                set
                {
                    this.hourField = value;
                }
            }

            /// <remarks/>
            public byte minutes
            {
                get
                {
                    return this.minutesField;
                }
                set
                {
                    this.minutesField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancelationPolicyDescription
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancelationPolicyDescriptionFreeTextQualification freeTextQualificationField;

            private string freeTextField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancelationPolicyDescriptionFreeTextQualification freeTextQualification
            {
                get
                {
                    return this.freeTextQualificationField;
                }
                set
                {
                    this.freeTextQualificationField = value;
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
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoCancellationPoliciesCancelationPolicyDescriptionFreeTextQualification
        {

            private byte textSubjectQualifierField;

            private string informationTypeIdField;

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
            public string informationTypeId
            {
                get
                {
                    return this.informationTypeIdField;
                }
                set
                {
                    this.informationTypeIdField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformation
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDates requestedDatesField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetails roomRateDetailsField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDeposit guaranteeOrDepositField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDates requestedDates
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
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetails roomRateDetails
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
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDeposit guaranteeOrDeposit
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDates
        {

            private string businessSemanticField;

            private string timeModeField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDatesBeginDateTime beginDateTimeField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDatesEndDateTime endDateTimeField;

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
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDatesBeginDateTime beginDateTime
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
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDatesEndDateTime endDateTime
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDatesBeginDateTime
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRequestedDatesEndDateTime
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetails
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformation roomInformationField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsSpecialInfo[] specialInfoField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirement[] bookingRequirementField;

            private object markerOfExtraField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsTariffInformation tariffInformationField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformation roomInformation
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
            [System.Xml.Serialization.XmlElementAttribute("specialInfo")]
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsSpecialInfo[] specialInfo
            {
                get
                {
                    return this.specialInfoField;
                }
                set
                {
                    this.specialInfoField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("bookingRequirement")]
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirement[] bookingRequirement
            {
                get
                {
                    return this.bookingRequirementField;
                }
                set
                {
                    this.bookingRequirementField = value;
                }
            }

            /// <remarks/>
            public object markerOfExtra
            {
                get
                {
                    return this.markerOfExtraField;
                }
                set
                {
                    this.markerOfExtraField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsTariffInformation tariffInformation
            {
                get
                {
                    return this.tariffInformationField;
                }
                set
                {
                    this.tariffInformationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformation
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformationRoomRateIdentifier roomRateIdentifierField;

            private string bookingCodeField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformationGuestCountDetails guestCountDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformationRoomRateIdentifier roomRateIdentifier
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
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformationGuestCountDetails guestCountDetails
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformationRoomRateIdentifier
        {

            private string roomTypeField;

            private string ratePlanCodeField;

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
            public string ratePlanCode
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsRoomInformationGuestCountDetails
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsSpecialInfo
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsSpecialInfoFreeTextDetails freeTextDetailsField;

            private string freeTextField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsSpecialInfoFreeTextDetails freeTextDetails
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
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsSpecialInfoFreeTextDetails
        {

            private byte textSubjectQualifierField;

            private string informationTypeField;

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
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirement
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementGuaranteeDepositStatusInfo guaranteeDepositStatusInfoField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementPaymentInformation paymentInformationField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementCreditCardInformation[] creditCardInformationField;

            private object markerOfBookingRequirementField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementGuaranteeDepositStatusInfo guaranteeDepositStatusInfo
            {
                get
                {
                    return this.guaranteeDepositStatusInfoField;
                }
                set
                {
                    this.guaranteeDepositStatusInfoField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementPaymentInformation paymentInformation
            {
                get
                {
                    return this.paymentInformationField;
                }
                set
                {
                    this.paymentInformationField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("creditCardInformation")]
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementCreditCardInformation[] creditCardInformation
            {
                get
                {
                    return this.creditCardInformationField;
                }
                set
                {
                    this.creditCardInformationField = value;
                }
            }

            /// <remarks/>
            public object markerOfBookingRequirement
            {
                get
                {
                    return this.markerOfBookingRequirementField;
                }
                set
                {
                    this.markerOfBookingRequirementField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementGuaranteeDepositStatusInfo
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementGuaranteeDepositStatusInfoStatusDetails statusDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementGuaranteeDepositStatusInfoStatusDetails statusDetails
            {
                get
                {
                    return this.statusDetailsField;
                }
                set
                {
                    this.statusDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementGuaranteeDepositStatusInfoStatusDetails
        {

            private string indicatorField;

            private string actionField;

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
            public string action
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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementPaymentInformation
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementPaymentInformationPaymentDetails paymentDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementPaymentInformationPaymentDetails paymentDetails
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementPaymentInformationPaymentDetails
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementCreditCardInformation
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementCreditCardInformationFormOfPayment formOfPaymentField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementCreditCardInformationFormOfPayment formOfPayment
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsBookingRequirementCreditCardInformationFormOfPayment
        {

            private string typeField;

            private string vendorCodeField;

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
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsTariffInformation
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsTariffInformationTariffInfo tariffInfoField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsTariffInformationTariffInfo tariffInfo
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationRoomRateDetailsTariffInformationTariffInfo
        {

            private decimal amountField;

            private string currencyField;

            private string dailyTotalIndicatorField;

            private byte statusField;

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
            public string dailyTotalIndicator
            {
                get
                {
                    return this.dailyTotalIndicatorField;
                }
                set
                {
                    this.dailyTotalIndicatorField = value;
                }
            }

            /// <remarks/>
            public byte status
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDeposit
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositPaymentInfo paymentInfoField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfo groupCreditCardInfoField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositPaymentInfo paymentInfo
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
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfo groupCreditCardInfo
            {
                get
                {
                    return this.groupCreditCardInfoField;
                }
                set
                {
                    this.groupCreditCardInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositPaymentInfo
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositPaymentInfoPaymentDetails paymentDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositPaymentInfoPaymentDetails paymentDetails
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositPaymentInfoPaymentDetails
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfo
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoCreditCardInfo creditCardInfoField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoConcealedCreditCardInfo concealedCreditCardInfoField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoFortknoxIds fortknoxIdsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoCreditCardInfo creditCardInfo
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

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoConcealedCreditCardInfo concealedCreditCardInfo
            {
                get
                {
                    return this.concealedCreditCardInfoField;
                }
                set
                {
                    this.concealedCreditCardInfoField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoFortknoxIds fortknoxIds
            {
                get
                {
                    return this.fortknoxIdsField;
                }
                set
                {
                    this.fortknoxIdsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoCreditCardInfo
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoCreditCardInfoCcInfo ccInfoField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoCreditCardInfoCcInfo ccInfo
            {
                get
                {
                    return this.ccInfoField;
                }
                set
                {
                    this.ccInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoCreditCardInfoCcInfo
        {

            private string vendorCodeField;

            private ulong cardNumberField;

            private ushort expiryDateField;

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
            public ulong cardNumber
            {
                get
                {
                    return this.cardNumberField;
                }
                set
                {
                    this.cardNumberField = value;
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoConcealedCreditCardInfo
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoConcealedCreditCardInfoCcInfo ccInfoField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoConcealedCreditCardInfoCcInfo ccInfo
            {
                get
                {
                    return this.ccInfoField;
                }
                set
                {
                    this.ccInfoField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoConcealedCreditCardInfoCcInfo
        {

            private string cardNumberField;

            /// <remarks/>
            public string cardNumber
            {
                get
                {
                    return this.cardNumberField;
                }
                set
                {
                    this.cardNumberField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoFortknoxIds
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoFortknoxIdsReferenceDetails referenceDetailsField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoFortknoxIdsReferenceDetails referenceDetails
            {
                get
                {
                    return this.referenceDetailsField;
                }
                set
                {
                    this.referenceDetailsField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRequestableInformationGuaranteeOrDepositGroupCreditCardInfoFortknoxIdsReferenceDetails
        {

            private string typeField;

            private ulong valueField;

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
            public ulong value
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRateChanges
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangeAmountInformation rateChangeAmountInformationField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformation rateChangePeriodInformationField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangeAmountInformation rateChangeAmountInformation
            {
                get
                {
                    return this.rateChangeAmountInformationField;
                }
                set
                {
                    this.rateChangeAmountInformationField = value;
                }
            }

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformation rateChangePeriodInformation
            {
                get
                {
                    return this.rateChangePeriodInformationField;
                }
                set
                {
                    this.rateChangePeriodInformationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangeAmountInformation
        {

            private Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangeAmountInformationTariffInfo tariffInfoField;

            /// <remarks/>
            public Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangeAmountInformationTariffInfo tariffInfo
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangeAmountInformationTariffInfo
        {

            private decimal amountField;

            private string currencyField;

            private string dailyTotalIndicatorField;

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
            public string dailyTotalIndicator
            {
                get
                {
                    return this.dailyTotalIndicatorField;
                }
                set
                {
                    this.dailyTotalIndicatorField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformation
        {

            private string businessSemanticField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformationBeginDateTime beginDateTimeField;

            private Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformationEndDateTime endDateTimeField;

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
            public Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformationBeginDateTime beginDateTime
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
            public Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformationEndDateTime endDateTime
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformationBeginDateTime
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
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://xml.amadeus.com/HBKRCR_17_1_1A")]
        public partial class Hotel_SellReplyRoomStayDataRoomListInfoRateChangesRateChangePeriodInformationEndDateTime
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


    }
}
