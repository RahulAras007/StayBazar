using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Xml.Linq;
//using Microsoft.AspNet.Identity;

namespace ExcelReport
{
    public class APIUtility
    {

        public APIUtility()
        {

        }
        public static List<CLayer.RoomStaysResult> AmedusHotelRoomStaysResultList = null;
        public static string SoapHeaderStateful = string.Empty;
        public static int SessionSequenceNumber = 0;
        public static string loggedInUser = string.Empty;
        public static string GDSCountryCode = string.Empty ;
        public static string GDSCurrencyCode = string.Empty ;
        public static string SetSoapHeader(string Url, string Action, bool IsStateless = true)
        {
            string StayBazarWBSUserName = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSUSERNAME);
            string StayBazarWBSPassword = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPASSWORD);
            string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

            string Nonce = PasswordDigest.GenerateNonce();
            string Password = StayBazarWBSPassword;
            string Created = PasswordDigest.GenerateTime();
            string HashedPassword = PasswordDigest.GenerateHashedPassword(Nonce, Created, Password);



            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<add:MessageID xmlns:add = \"http://www.w3.org/2005/08/addressing\">468d905f - 181a - d404 - d776 - 863aa49cca4b</add:MessageID>";
            Header = Header + "<add:Action xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Action + "</add:Action>";
            Header = Header + "<add:To xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Url + "</add:To>";
            Header = Header + "<link:TransactionFlowLink xmlns:link = \"http://wsdl.amadeus.com/2010/06/ws/Link_v1\"/>";
            Header = Header + "<oas:Security xmlns:oas = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
            Header = Header + "<oas:UsernameToken oas1:Id = \"UsernameToken -1\" xmlns:oas1 = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">";
            Header = Header + "<oas:Username>" + StayBazarWBSUserName + "</oas:Username>";
            Header = Header + "<oas:Nonce EncodingType = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">" + Nonce + "</oas:Nonce>";
            Header = Header + "<oas:Password Type = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\">" + HashedPassword + "</oas:Password>";
            Header = Header + "<oas1:Created>" + Created + "</oas1:Created>";
            Header = Header + "</oas:UsernameToken>";
            Header = Header + "</oas:Security>";
            Header = Header + "<AMA_SecurityHostedUser xmlns = \"http://xml.amadeus.com/2010/06/Security_v1\">";
            Header = Header + "<UserID AgentDutyCode = \"SU\" POS_Type = \"1\" PseudoCityCode = \"" + StayBazarWBSOffice + "\" RequestorType = \"U\" />";
            Header = Header + "</AMA_SecurityHostedUser>";
            if(!IsStateless)
            {
                Header = Header +"<awsse:Session TransactionStatusCode = \"Start\" xmlns:awsse = \"http://xml.amadeus.com/2010/06/Session_v3\" />";
            }
            Header = Header + "</soapenv:Header>";

            return Header;
        }
        public static string SetStatefulSoapHeader(string Action,string SessionId,int SequenceNumber,string SecurityToken )
        {
            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<awsse:Session TransactionStatusCode = \"InSeries\" xmlns:awsse = \"http://xml.amadeus.com/2010/06/Session_v3\" >";
            Header = Header + "<awsse:SessionId>"+ SessionId + "</awsse:SessionId>";
            Header = Header + "<awsse:SequenceNumber>"+ SequenceNumber + "</awsse:SequenceNumber>";
            Header = Header + "<awsse:SecurityToken>"+ SecurityToken + "</awsse:SecurityToken>";
            Header = Header + "</awsse:Session>";
            Header = Header + "<add:MessageID xmlns:add = \"http://www.w3.org/2005/08/addressing\">64f06787 - bf23 - 9ad2 - 417e-ff096170184e</add:MessageID>";
            Header = Header + "<add:Action xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Action + "</add:Action>";
            Header = Header + "<add:To xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL) + "</add:To>";
            Header = Header + "</soapenv:Header>";
            return Header;
        }
     
        public static string SetHotelMultiSingleAvailabilityBody(CLayer.SearchCriteria sch)
        {
            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelAvailRQ xmlns = \"http://www.opentravel.org/OTA/2003/05\" EchoToken = \"MultiSingle\" Version = \"4.000\" SummaryOnly = \"true\" AvailRatesOnly = \"true\" OnRequestInd = \"true\" RateRangeOnly = \"true\" ExactMatchOnly = \"false\" SearchCacheLevel = \"VeryRecent\" RequestedCurrency = \"INR\" RateDetailsInd = \"true\" >";
            Body = Body + "<AvailRequestSegments>";
            Body = Body + "<AvailRequestSegment InfoSource = \"Distribution\">";
            Body = Body + "<HotelSearchCriteria AvailableOnlyIndicator = \"true\">";
            Body = Body + "<Criterion ExactMatch = \"true\">";
            //Body = Body + "<HotelRef HotelCityCode = \"BLR\"/>";
         //   Body = Body + "<RefPoint CountryCode=\"IN\" Name=\"" + sch.Destination + "\" />";
            Body = Body + "<Address><CityName>" + sch.Destination + "</CityName><CountryName Code = 'IN' /></Address>";
             Body = Body + "<StayDateRange Start = \"" + sch.CheckIn.ToString("yyyy-MM-dd") + "\" End = \"" + sch.CheckOut.ToString("yyyy-MM-dd") + "\"/>";
            Body = Body + "<RoomStayCandidates>";
            Body = Body + "<RoomStayCandidate RoomID = \"1\" Quantity = \""+BedRoomsQuantity+"\">";
            Body = Body + "<GuestCounts >";
            Body = Body + "<GuestCount AgeQualifyingCode = \"10\" Count = \""+ GuestCount+"\"/>";
            Body = Body + "</GuestCounts>";
            Body = Body + "</RoomStayCandidate>";
            Body = Body + "</RoomStayCandidates>";
            Body = Body + "</Criterion>";
            Body = Body + "</HotelSearchCriteria>";
            Body = Body + "</AvailRequestSegment>";
            Body = Body + "</AvailRequestSegments>";
            Body = Body + "</OTA_HotelAvailRQ>";
            Body = Body + "</soapenv:Body>";

            return Body;
        }
        public static string SetHotelMultiSingleAvailabilityBody(CLayer.SearchCriteria sch,string HotelCode)
        {
            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 2 : GuestCount;

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelAvailRQ xmlns = \"http://www.opentravel.org/OTA/2003/05\" EchoToken = \"MultiSingle\" Version = \"4.000\" SummaryOnly = \"true\" AvailRatesOnly = \"true\" OnRequestInd = \"true\" RateRangeOnly = \"true\" ExactMatchOnly = \"false\" SearchCacheLevel = \"VeryRecent\" RequestedCurrency = \"INR\" RateDetailsInd = \"true\" >";
            Body = Body + "<AvailRequestSegments>";
            Body = Body + "<AvailRequestSegment InfoSource = \"Distribution\">";
            Body = Body + "<HotelSearchCriteria AvailableOnlyIndicator = \"true\">";
            Body = Body + "<Criterion ExactMatch = \"true\">";
            Body = Body + "<HotelRef HotelCode = \""+ HotelCode + "\"/>";
            Body = Body + "<StayDateRange Start = \"" + sch.CheckIn.ToString("yyyy-MM-dd") + "\" End = \"" + sch.CheckOut.ToString("yyyy-MM-dd") + "\"/>";
            Body = Body + "<RoomStayCandidates>";
            Body = Body + "<RoomStayCandidate RoomID = \"1\" Quantity = \"" + BedRoomsQuantity + "\">";
            Body = Body + "<GuestCounts >";
            Body = Body + "<GuestCount AgeQualifyingCode = \"10\" Count = \"" + GuestCount + "\"/>";
            Body = Body + "</GuestCounts>";
            Body = Body + "</RoomStayCandidate>";
            Body = Body + "</RoomStayCandidates>";
            Body = Body + "</Criterion>";
            Body = Body + "</HotelSearchCriteria>";
            Body = Body + "</AvailRequestSegment>";
            Body = Body + "</AvailRequestSegments>";
            Body = Body + "</OTA_HotelAvailRQ>";
            Body = Body + "</soapenv:Body>";

            return Body;
        }
        public static string PNR_Cancel(string ControlRefNumber="",int optioncode=0)
        {
            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<PNR_Cancel xmlns = \"http://xml.amadeus.com/PNRXCL_15_1_1A\" >";
            if (!string.IsNullOrEmpty(ControlRefNumber))
            {
                Body = Body + "<reservationInfo>";
                Body = Body + "<reservation>";
                Body = Body + "<controlNumber>" + ControlRefNumber + "</controlNumber>";
                Body = Body + "</reservation></reservationInfo>";
                Body = Body + "<pnrActions>";
                Body = Body + "<optionCode>" + optioncode + "</optionCode>";
                Body = Body + "</pnrActions>";
                Body = Body + "<cancelElements>";
                Body = Body + "<entryType>E</entryType>";
                Body = Body + "<element>";
                Body = Body + "<identifier>ST</identifier>";
                Body = Body + "<number>1</number>";
                Body = Body + "</element></cancelElements>";

            }
            else
            {
                Body = Body + "<pnrActions>";
                Body = Body + "<optionCode>0</optionCode>";
                Body = Body + "</pnrActions>";
                Body = Body + "<cancelElements>";
                Body = Body + "<entryType>E</entryType>";
                Body = Body + "<element>";
                Body = Body + "<identifier>OT</identifier>";
                Body = Body + "<number>2</number>";
                Body = Body + "</element>";
                Body = Body + "</cancelElements>";
            }
            Body = Body + "</PNR_Cancel>";
            Body = Body + "</soapenv:Body>";
            return Body;
        }
        public static string PNR_Retrieve(string ControlRefNumber)
        {
            string Body = string.Empty;
            Body = Body + "<PNR_Retrieve xmlns=\"http://xml.amadeus.com/PNRRET_15_1_1A\">";
            Body = Body + "<retrievalFacts>";
            Body = Body + "<retrieve>";
            Body = Body + "<type>1</type>";
            Body = Body + "</retrieve>";
            Body = Body + "<personalFacts>";
            Body = Body + "<travellerInformation>";
            Body = Body + "<traveller>";
            Body = Body + "<surname>TEST</surname>";
            Body = Body + "</traveller>";
            Body = Body + "</travellerInformation>";
            Body = Body + "<productInformation>";
            Body = Body + "<product>";
            Body = Body + "<depDate>"+ DateTime.Now.ToString("ddMMyy")+"</depDate>";
            Body = Body + "</product>";
            Body = Body + "</productInformation>";
            Body = Body + "</personalFacts>";
            Body = Body + "</retrievalFacts>";
            Body = Body + "</PNR_Retrieve>";

            return Body;
        }
        public static string PNR_AddMultielements(string HotelCode,string BookingCode,string Email="",int optioncode=0)
        {
            //string Header = string.Empty;
            //Header = SetSoapHeader(Url, Action, false);

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<PNR_AddMultiElements xmlns =\"http://xml.amadeus.com/PNRADD_15_1_1A\">";
            if (optioncode<=0)
            {

                Body = Body + "<pnrActions>";
                Body = Body + "<optionCode>" + optioncode + "</optionCode>";
                Body = Body + "</pnrActions>";
                Body = Body + "<travellerInfo>";
                Body = Body + "<elementManagementPassenger>";
                Body = Body + "<reference>";
                Body = Body + "<qualifier>PR</qualifier>";
                Body = Body + "<number>1</number>";
                Body = Body + "</reference>";
                Body = Body + "<segmentName>NM</segmentName>";
                Body = Body + "</elementManagementPassenger>";
                Body = Body + "<passengerData>";
                Body = Body + "<travellerInformation>";
                Body = Body + "<traveller>";
                Body = Body + "<surname>TEST</surname>";
                Body = Body + "<quantity>1</quantity>";
                Body = Body + "</traveller>";
                Body = Body + "<passenger>";
                Body = Body + "<firstName>TESTMR</firstName>";
                Body = Body + "</passenger>";
                Body = Body + "</travellerInformation>";
                Body = Body + "</passengerData>";
                Body = Body + "</travellerInfo>";
                Body = Body + "<dataElementsMaster>";
                Body = Body + "<marker1/>";
                Body = Body + "<dataElementsIndiv>";
                Body = Body + "<elementManagementData>";
                Body = Body + "<segmentName>RM</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<miscellaneousRemark>";
                Body = Body + "<remarks>";
                Body = Body + "<type>RM</type>";
                Body = Body + "<freetext>BasePrice:FREETEXT</freetext>";
                Body = Body + "</remarks>";
                Body = Body + "</miscellaneousRemark>";
                Body = Body + "</dataElementsIndiv>";
                Body = Body + "<dataElementsIndiv>";
                Body = Body + "<elementManagementData>";
                Body = Body + "<reference>";
                Body = Body + "<qualifier>OT</qualifier>";
                Body = Body + "<number>3</number>";
                Body = Body + "</reference>";
                Body = Body + "<segmentName>RF</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<freetextData>";
                Body = Body + "<freetextDetail>";
                Body = Body + "<subjectQualifier>3</subjectQualifier>";
                Body = Body + "<type>3</type>";
                Body = Body + "</freetextDetail>";
                Body = Body + "<longFreetext>TST0008G1MV</longFreetext>";
                Body = Body + "</freetextData>";
                Body = Body + "</dataElementsIndiv>";
                Body = Body + "<dataElementsIndiv>";
                Body = Body + "<elementManagementData>";
                Body = Body + "<reference>";
                Body = Body + "<qualifier>OT</qualifier>";
                Body = Body + "<number>3</number>";
                Body = Body + "</reference>";
                Body = Body + "<segmentName>TK</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<ticketElement>";
                Body = Body + "<ticket>";
                Body = Body + "<indicator>TL</indicator>";
                Body = Body + "<date>" + DateTime.Now.ToString("ddMMyy") + "</date>";
                Body = Body + "</ticket>";
                Body = Body + "</ticketElement>";
                Body = Body + "</dataElementsIndiv>";
                Body = Body + "<dataElementsIndiv>";
                Body = Body + "<elementManagementData>";
                Body = Body + "<reference>";
                Body = Body + "<qualifier>OT</qualifier>";
                Body = Body + "<number>3</number>";
                Body = Body + "</reference>";
                Body = Body + "<segmentName>AP</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<freetextData>";
                Body = Body + "<freetextDetail>";
                Body = Body + "<subjectQualifier>3</subjectQualifier>";
                Body = Body + "<type>P02</type>";
                Body = Body + "</freetextDetail>";
                Body = Body + "<longFreetext>" + HttpContext.Current.Session["LoggedInEmail"] + "</longFreetext> ";
                Body = Body + "</freetextData>";
                Body = Body + "</dataElementsIndiv>";
              
            }
            else
            {
                Body = Body + "<pnrActions>";
                Body = Body + "<optionCode>" + optioncode + "</optionCode>";
                Body = Body + "</pnrActions>";
                Body = Body + "<dataElementsMaster>";
                Body = Body + "<marker1/>";
                Body = Body + "<dataElementsIndiv>";
                Body = Body + "<elementManagementData>";
                Body = Body + "<segmentName>RM</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<miscellaneousRemark>";
                Body = Body + "<remarks>";
                Body = Body + "<type>RM</type>";
                Body = Body + "<freetext> Base Price: FREE TEXT, Hotel Taxes: 0INR </freetext>";
                Body = Body + "</remarks>";
                Body = Body + "</miscellaneousRemark>";
                Body = Body + "</dataElementsIndiv>";
                Body = Body + "<dataElementsIndiv>";
                Body = Body + "<elementManagementData>";
                Body = Body + " <segmentName>RF</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<freetextData>";
                Body = Body + "<freetextDetail>";
                Body = Body + "<subjectQualifier>3</subjectQualifier>";
                Body = Body + "<type>P22</type>";
                Body = Body + "</freetextDetail>";
                Body = Body + "<longFreetext>TST0008G1MV</longFreetext>";
                Body = Body + "</freetextData>";
                Body = Body + "</dataElementsIndiv>";
            }
            Body = Body + "</dataElementsMaster>";
            Body = Body + "</PNR_AddMultiElements>";
            Body = Body + "</soapenv:Body>";


            return Body;
        }
        public static string Hotel_Sell(string PNR,string BookingCode)
        {
            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<Hotel_Sell xmlns=\"http://xml.amadeus.com/HBKRCQ_17_1_1A\">"; 
            Body = Body + "<roomStayData>";
            Body = Body + "<markerRoomStayData></markerRoomStayData>";
            Body = Body + "<globalBookingInfo>";
            Body = Body + "<markerGlobalBookingInfo></markerGlobalBookingInfo>";
            Body = Body + "<bookingSource>";
            Body = Body + "<originIdentification>";
            Body = Body + "<originatorId>96601746</originatorId>";
            Body = Body + "</originIdentification>";
            Body = Body + "</bookingSource>";
            Body = Body + "<representativeParties>";
            Body = Body + "<occupantList>";
            Body = Body + "<passengerReference>";
            Body = Body + "<type>BHO</type>";
            Body = Body + "<value>2</value>";
            Body = Body + "</passengerReference>";
            Body = Body + "</occupantList>";
            Body = Body + "</representativeParties>";
            Body = Body + "</globalBookingInfo>";
            Body = Body + "<roomList>";
            Body = Body + "<markerRoomstayQuery></markerRoomstayQuery>";
            Body = Body + "<roomRateDetails>";
            Body = Body + "<marker></marker>";
            Body = Body + "<hotelProductReference>";
            Body = Body + "<referenceDetails>";
            Body = Body + "<type>BC</type>";
            Body = Body + "<value>"+BookingCode+"</value>";
            Body = Body + "</referenceDetails>";
            Body = Body + "</hotelProductReference>";
            Body = Body + "<markerOfExtra></markerOfExtra>";
            Body = Body + "</roomRateDetails>";
            Body = Body + "<guaranteeOrDeposit>";
            Body = Body + "<paymentInfo>";
            Body = Body + "<paymentDetails>";
            Body = Body + "<formOfPaymentCode>1</formOfPaymentCode>";
            Body = Body + "<paymentType>1</paymentType>";
            Body = Body + "<serviceToPay>3</serviceToPay>";
            Body = Body + "</paymentDetails>";
            Body = Body + "</paymentInfo>";
            Body = Body + "<groupCreditCardInfo>";
            Body = Body + "<creditCardInfo>";
            Body = Body + "<ccInfo>";
            Body = Body + "<vendorCode>CA</vendorCode>";
            Body = Body + "<cardNumber>5275009999993833</cardNumber>";
            Body = Body + "<securityId>111</securityId>";
            Body = Body + "<expiryDate>0618</expiryDate>";
            Body = Body + "<ccHolderName>AMADEUSTEST</ccHolderName>";
            Body = Body + "</ccInfo>";
            Body = Body + "</creditCardInfo>";
            Body = Body + "</groupCreditCardInfo>";
            Body = Body + "</guaranteeOrDeposit>";
            Body = Body + "</roomList>";
            Body = Body + "</roomStayData>";
            Body = Body + "</Hotel_Sell>";
            Body = Body + "</soapenv:Body>";

            return Body;
        }
        public static string PNR_AddMultielements_Confirm(string HotelCode)
        {
            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "</soapenv:Body>";
            return Body;
        }
        public static string hotel_descriptiveinfor_SetSoapHeader(string Url, string Action, bool IsStateless = true)
        {
            string StayBazarWBSUserName = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSUSERNAME);
            string StayBazarWBSPassword = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPASSWORD);
            string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

            string Nonce = PasswordDigest.GenerateNonce();
            string Password = StayBazarWBSPassword;
            string Created = PasswordDigest.GenerateTime();
            string HashedPassword = PasswordDigest.GenerateHashedPassword(Nonce, Created, Password);


            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<add:MessageID xmlns:add = \"http://www.w3.org/2005/08/addressing\"> 34118c5a - a639 - 779a - 1b88 - 2111f9dbfff2</add:MessageID>";
            Header = Header + "<add:Action xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Action + "</add:Action>";
            Header = Header + "<add:To xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Url + "</add:To>";
            Header = Header + "<link:TransactionFlowLink xmlns:link = \"http://wsdl.amadeus.com/2010/06/ws/Link_v1\"/>";
            Header = Header + "<oas:Security xmlns:oas = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
            Header = Header + "<oas:UsernameToken oas1:Id = \"UsernameToken -1\" xmlns:oas1 = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">";
            Header = Header + "<oas:Username>" + StayBazarWBSUserName + "</oas:Username>";
            Header = Header + "<oas:Nonce EncodingType = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">" + Nonce + "</oas:Nonce>";
            Header = Header + "<oas:Password Type = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\">" + HashedPassword + "</oas:Password>";
            Header = Header + "<oas1:Created>" + Created + "</oas1:Created>";
            Header = Header + "</oas:UsernameToken>";
            Header = Header + "</oas:Security>";
            Header = Header + "<AMA_SecurityHostedUser xmlns = \"http://xml.amadeus.com/2010/06/Security_v1\">";
            Header = Header + "<UserID AgentDutyCode = \"SU\" POS_Type = \"1\" PseudoCityCode = \"" + StayBazarWBSOffice + "\" RequestorType = \"U\"/>";
            Header = Header + "</AMA_SecurityHostedUser>";
            Header = Header + "<awsse:Session TransactionStatusCode=\"Start\" xmlns:awsse=\"http://xml.amadeus.com/2010/06/Session_v3\"/>";
            Header = Header = Header + "</soapenv:Header>";

            return Header;

        }



        public static string hotel_descriptiveinfor_detailsbody(string HotelCode)
        {
            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelDescriptiveInfoRQ EchoToken = \"withParsing\" PrimaryLangID = \"en\" Version = \"6.001\">";
            Body = Body + "<HotelDescriptiveInfos>";
            Body = Body + " <HotelDescriptiveInfo HotelCode = \""+ HotelCode+"\">";
            Body = Body + "<HotelInfo SendData = \"true\"/>";
            Body = Body + "<FacilityInfo SendGuestRooms = \"true\" SendMeetingRooms = \"true\" SendRestaurants = \"true\"/>";
            Body = Body + "<Policies SendPolicies = \"true\"/>";
            Body = Body + "<AreaInfo SendAttractions = \"true\" SendRecreations = \"true\" SendRefPoints = \"true\"/>";
            Body = Body + "<AffiliationInfo SendAwards = \"true\" SendLoyalPrograms = \"true\" />";
            Body = Body + "  <ContactInfo SendData = \"true\"/>";
            Body = Body + "  <MultimediaObjects SendData = \"true\"/>";
            Body = Body + " <ContentInfos>";
            Body = Body + " <ContentInfo Name = \"SecureMultimediaURLs\"/>";
            Body = Body + "</ContentInfos>";
            Body = Body + " </HotelDescriptiveInfo>";
            Body = Body + "</HotelDescriptiveInfos>";
            Body = Body + "  </OTA_HotelDescriptiveInfoRQ>";
            Body = Body + " </soapenv:Body>";

            return Body;
        }
        public static string Hotel_CompleteReservationDetails(string ControlNumber)
        {
            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<Hotel_CompleteReservationDetails xmlns=\"http://xml.amadeus.com/HCRDRQ_17_1_1A\" >";
            Body = Body + "<retrievalKeyGroup>";
            Body = Body + "<retrievalKey>";
            Body = Body + "<reservation>";
            Body = Body + "<companyId>1A</companyId>";
            Body = Body + "<controlNumber>"+ ControlNumber +"</controlNumber>";
            Body = Body + "<controlType>P</controlType>";
            Body = Body + "</reservation>";
            Body = Body + "</retrievalKey>";
            Body = Body + "<tattooID>";
            Body = Body + "<referenceDetails>";
            Body = Body + "<type>S</type>";
            Body = Body + "<value>3</value>";
            Body = Body + "</referenceDetails>";
            Body = Body + "</tattooID>";
            Body = Body + "</retrievalKeyGroup>";
            Body = Body + "</Hotel_CompleteReservationDetails>";
            Body = Body + " </soapenv:Body>";

            return Body;
        }

        public static string hotel_descriptiveinfor_Priceing_SetSoapHeaderbody(string Url, string Action, string SecurityToken, string SessionId, string SequenceNumber)
        {
            string StayBazarWBSUserName = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSUSERNAME);
            string StayBazarWBSPassword = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPASSWORD);
            string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

            string Nonce = PasswordDigest.GenerateNonce();
            string Password = StayBazarWBSPassword;
            string Created = PasswordDigest.GenerateTime();
            string HashedPassword = PasswordDigest.GenerateHashedPassword(Nonce, Created, Password);

            string wbsurl = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL);
            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<awsse:Session TransactionStatusCode =\"InSeries\" xmlns:awsse=\"http://xml.amadeus.com/2010/06/Session_v3\">";
            Header = Header + "<awsse:SessionId>"+SessionId+"</awsse:SessionId>";
            Header = Header + "<awsse:SequenceNumber>"+SequenceNumber+"</awsse:SequenceNumber>";
            Header = Header + "<awsse:SecurityToken>"+SecurityToken+"</awsse:SecurityToken>";

            Header = Header + "</awsse:Session>";
            Header = Header + "<add:MessageID xmlns:add=\"http://www.w3.org/2005/08/addressing\">85ebd474-da3c-232e-8c00-266a76d46f08 </add:MessageID>";
            Header = Header + "<add:Action xmlns:add =\"http://www.w3.org/2005/08/addressing\">http://webservices.amadeus.com/Hotel_EnhancedPricing_2.0</add:Action>";
            Header = Header + "<add:To xmlns:add =\"http://www.w3.org/2005/08/addressing\">"+ wbsurl +"</add:To>";
            Header = Header + "</soapenv:Header>";

            return Header;
        }

        public static string hotel_descriptiveinfor_Priceing_detailsBody(string hotelcode, string RoomTypeCode, string RatePlanCode, string BookingCode, string Start, string end, string ChainCode, string AgeQualifyingCode, string count)
        {
            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 2 : GuestCount;

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelAvailRQ xmlns=\"http://www.opentravel.org/OTA/2003/05\" EchoToken=\"Pricing\" Version=\"4.000\" PrimaryLangID=\"EN\" SummaryOnly=\"false\" RateRangeOnly=\"false\">";
            Body = Body + "<AvailRequestSegments>";
            Body = Body + "<AvailRequestSegment>";
            Body = Body + "<HotelSearchCriteria>";
            Body = Body + "<Criterion ExactMatch=\"true\">";
            Body = Body + "<HotelRef  HotelCode=\"" + hotelcode + "\" />";
            Body = Body + "<StayDateRange Start=\"" + Start + "\" End=\"" + end + "\"/>";
            Body = Body + "<RatePlanCandidates>";
            Body = Body + "<RatePlanCandidate RatePlanCode=\"" + RatePlanCode + "\"/>";
            Body = Body + "</RatePlanCandidates>";
            Body = Body + "<RoomStayCandidates>";
            Body = Body + "<RoomStayCandidate RoomTypeCode=\"" + RoomTypeCode + "\" RoomID=\"1\" Quantity=\""+ BedRoomsQuantity+"\" BookingCode=\"" + BookingCode + "\">";
            Body = Body + "<GuestCounts IsPerRoom=\"true\">";
            Body = Body + "<GuestCount AgeQualifyingCode=\"" + AgeQualifyingCode + "\" Count=\"" + GuestCount + "\"/>";
            Body = Body + "</GuestCounts>";
            Body = Body + "</RoomStayCandidate>";
            Body = Body + "</RoomStayCandidates>";
            Body = Body + "</Criterion>";
            Body = Body + "</HotelSearchCriteria>";
            Body = Body + "</AvailRequestSegment>";
            Body = Body + "</AvailRequestSegments>";
            Body = Body + "</OTA_HotelAvailRQ>";
            Body = Body + " </soapenv:Body>";

            return Body;
        }
        public static void GenerateGDSTransactionLog(string GDSSearchCriteria = "", string GDSResult = "", int GDSUserID = 0, int GDSType = 0, long BookingID = 0)
        {
            try
            {
                Random r = new Random();
                int num = r.Next();
                string GDSTransID = Convert.ToString(BookingID) + "-" + num.ToString() + "-" + DateTime.Now.ToString("yyyyMMdd HH:mm:ss.fff", CultureInfo.InvariantCulture);


                CLayer.GDSTransactionsLog objLog = new CLayer.GDSTransactionsLog();
                objLog.BookingID = BookingID;
                objLog.GDSTransID = GDSTransID;
                objLog.GDSSearchCriteria = GDSSearchCriteria;
                try
                {
                    objLog.GDSRequest = Convert.ToString(HttpContext.Current.Session["GDSRequest"]);
                }
                catch
                {
                    objLog.GDSRequest = "";
                }
                          
                objLog.GDSResult = GDSResult;
                if (GDSResult.ToLower().Contains("errorcode"))
                {
                    objLog.GDSError = GDSResult;
                    objLog.GDSErrorMessage = ReadGDSError(GDSResult, GDSType);
                    string Log = objLog.GDSErrorMessage + "-" + GDSTransID;
                    LogHandler.AddLog(Log);
                }
                objLog.UserID = GDSUserID;
                objLog.GDSType = GDSType;
                BLayer.GDSTransactionsLog.GenerateGDSLog(objLog);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetErrorNodeValue(XmlDocument xmlDoc, string nodeRoot, string NodeType, string nodeError, string nodeWarning, XmlNamespaceManager xmlnsManager)
        {
            XmlNode node = null;
            string error = string.Empty;
            node = xmlDoc.SelectSingleNode(nodeRoot + NodeType + nodeError, xmlnsManager);

            return error;


        }
        public static string SetSoapHeaderPriceingdetails(string Url, string Action, bool IsStateless = true)
        {
            string StayBazarWBSUserName = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSUSERNAME);
            string StayBazarWBSPassword = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPASSWORD);
            string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

            string Nonce = PasswordDigest.GenerateNonce();
            string Password = StayBazarWBSPassword;
            string Created = PasswordDigest.GenerateTime();
            string HashedPassword = PasswordDigest.GenerateHashedPassword(Nonce, Created, Password);



            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<add:MessageID xmlns:add = \"http://www.w3.org/2005/08/addressing\">468d905f - 181a - d404 - d776 - 863aa49cca4b</add:MessageID>";
            Header = Header + "<add:Action xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Action + "</add:Action>";
            Header = Header + "<add:To xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Url + "</add:To>";
            Header = Header + "<link:TransactionFlowLink xmlns:link = \"http://wsdl.amadeus.com/2010/06/ws/Link_v1\"/>";
            Header = Header + "<oas:Security xmlns:oas = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd\">";
            Header = Header + "<oas:UsernameToken oas1:Id = \"UsernameToken -1\" xmlns:oas1 = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd\">";
            Header = Header + "<oas:Username>" + StayBazarWBSUserName + "</oas:Username>";
            Header = Header + "<oas:Nonce EncodingType = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary\">" + Nonce + "</oas:Nonce>";
            Header = Header + "<oas:Password Type = \"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest\">" + HashedPassword + "</oas:Password>";
            Header = Header + "<oas1:Created>" + Created + "</oas1:Created>";
            Header = Header + "</oas:UsernameToken>";
            Header = Header + "</oas:Security>";
            Header = Header + "<AMA_SecurityHostedUser xmlns = \"http://xml.amadeus.com/2010/06/Security_v1\">";
            Header = Header + "<UserID AgentDutyCode = \"SU\" POS_Type = \"1\" PseudoCityCode = \"" + StayBazarWBSOffice + "\" RequestorType = \"U\" />";
            Header = Header + "</AMA_SecurityHostedUser>";
            Header = Header + "<awsse:Session TransactionStatusCode = \"Start\" xmlns:awsse = \"http://xml.amadeus.com/2010/06/Session_v3\" />";
          
            Header = Header + "</soapenv:Header>";

            return Header;
        }
        public static string SetHotelMultiSingleAvailabilityBodyPriceingdetails(string HotelCode,string start, string end)
        {
            int BedRoomsQuantity = 1;
            try
            {
                BedRoomsQuantity=Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            }
            catch
            {
                BedRoomsQuantity = 1;
            }
            int Adults = 1;
            int Children = 0;
            int GuestCount = 0;
            try
            {
                 GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            }
            catch
            {
                GuestCount = Adults + Children;
            }



            
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;


            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelAvailRQ xmlns = \"http://www.opentravel.org/OTA/2003/05\" EchoToken = \"MultiSingle\" Version = \"4.000\" SummaryOnly = \"true\" AvailRatesOnly = \"true\" OnRequestInd = \"true\" RateRangeOnly = \"true\" ExactMatchOnly = \"false\" SearchCacheLevel = \"VeryRecent\" RequestedCurrency = \"INR\" RateDetailsInd = \"true\" >";
            Body = Body + "<AvailRequestSegments>";
            Body = Body + "<AvailRequestSegment InfoSource = \"Distribution\">";
            Body = Body + "<HotelSearchCriteria AvailableOnlyIndicator = \"true\">";
            Body = Body + "<Criterion ExactMatch = \"true\">";
            Body = Body + "<HotelRef HotelCode = \"" + HotelCode + "\"/>";
            Body = Body + "<StayDateRange Start = \"" + start + "\" End = \"" + end + "\"/>";
            Body = Body + "<RoomStayCandidates>";
            Body = Body + "<RoomStayCandidate RoomID = \"1\" Quantity = \""+BedRoomsQuantity+"\">";
            Body = Body + "<GuestCounts >";
            Body = Body + "<GuestCount AgeQualifyingCode = \"10\" Count = \""+ GuestCount + "\"/>";
            Body = Body + "</GuestCounts>";
            Body = Body + "</RoomStayCandidate>";
            Body = Body + "</RoomStayCandidates>";
            Body = Body + "</Criterion>";
            Body = Body + "</HotelSearchCriteria>";
            Body = Body + "</AvailRequestSegment>";
            Body = Body + "</AvailRequestSegments>";
            Body = Body + "</OTA_HotelAvailRQ>";
            Body = Body + "</soapenv:Body>";

            return Body;

        }

        public  static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        public  static XmlDocument CreateSoapEnvelope(string XmlText)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(string.Format(@"{0}", XmlText));
            return soapEnvelopeDocument;
        }

        public  static HttpWebRequest InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
                return webRequest;
            }
        }
        public static bool CheckHotelBookingErrorExists(string result)
        {
            bool error = false;


            error = (result.ToLower().Contains("errorcode")) ? true : false;


            //bool response=false ;
            //XmlDocument soapEnvelopeXml = new XmlDocument();
            //soapEnvelopeXml.LoadXml(result);
            ////Select the book node with the matching attribute value.
            //XmlNode nodeToFind;
            //XmlElement root = soapEnvelopeXml.DocumentElement;
            //XmlNodeList nodelist;

            //var nsmgr = new XmlNamespaceManager(soapEnvelopeXml.NameTable);
            //nsmgr.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");


            //System.Xml.XmlElement subroot = this.GetChildByName(root, "soapenv:Body", soapEnvelopeXml);
            //System.Xml.XmlElement subroot1 = this.GetChildByName(subroot, "Hotel_SellReply", soapEnvelopeXml);
            //System.Xml.XmlElement subroot2 = this.GetChildByName(subroot1, "errorGroup", soapEnvelopeXml);

            //// Selects all the title elements that have an attribute named lang
            //nodelist = root.SelectNodes("//soapenv:Body/Hotel_SellReply/errorGroup");

            //if (nodelist != null)
            //{
            //    // It was found, manipulate it.
            //}
            //else
            //{
            //    // It was not found.
            //}
            return error;
        }
        public static string  ExecutePNRCancel(string ControlNumber,int pOptionCode=0)
        {
            string result = string.Empty;
            try
            {
                string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELPNRCANCELACTION);

                string pnrcancelSoapBody = APIUtility.PNR_Cancel(ControlNumber, pOptionCode);
                string SessionId = Convert.ToString(HttpContext.Current.Session["SessionId"]);
                int SequenceNumber = Convert.ToInt32(HttpContext.Current.Session["SequenceNumber"]);
                string SecurityToken = Convert.ToString(HttpContext.Current.Session["SecurityToken"]);

                SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, SessionId, SequenceNumber, SecurityToken);
                result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, pnrcancelSoapBody);

            }
            catch (Exception ex)
            {
                result = "";
            }
            return result;
        }
        public static string ExecutePNRCancel(string ControlNumber, int pOptionCode = 0,bool StateLess=false )
        {
            string result = string.Empty;
            try
            {
                string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELPNRCANCELACTION);

                string pnrcancelSoapBody = APIUtility.PNR_Cancel(ControlNumber, pOptionCode);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELPNRCANCELACTION); //"http://webservices.amadeus.com/Hotel_MultiSingleAvailability_10.0";

                string SoapHeader = string.Empty;
                string SoapBody = string.Empty;


                SoapHeader = APIUtility.SetSoapHeader(_url, _action, true );

                string SoapHeaderStateless = SoapHeader;
                result = APIUtility.GetAmedusResult(Action, SoapHeaderStateless, pnrcancelSoapBody);

            }
            catch (Exception ex)
            {
                result = "";
            }
            return result;
        }
        public static string GetAmedusResult(string Action,string pSoapHeadr, string pSoapBody, string XMLtext = "")
        {
            string soapResult = string.Empty;
            try
            {
                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL);
                var _action = Action; 

                string SoapHeader = pSoapHeadr;
                string SoapBody = pSoapBody;

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                switch(Action)
                {
                    case "http://webservices.amadeus.com/HBKRCQ_17_1_1A":
                        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\"  xmlns:hbk=\"http://xml.amadeus.com/HBKRCQ_17_1_1A\">";
                        break;
                    case "http://webservices.amadeus.com/PNRADD_15_1_1A":
                        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\" xmlns:pnr=\"http://xml.amadeus.com/PNRADD_15_1_1A\">";
                        break;
                    case "http://webservices.amadeus.com/PNRXCL_15_1_1A":
                        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\" xmlns:pnr=\"http://xml.amadeus.com/PNRXCL_15_1_1A\">";
                        break;
                    case "http://webservices.amadeus.com/PNRRET_15_1_1A":
                        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\" xmlns:pnr=\"http://xml.amadeus.com/PNRRET_15_1_1A\">";
                        break;
                    case "http://webservices.amadeus.com/HCRDRQ_17_1_1A":
                        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\" xmlns:hcr=\"http://xml.amadeus.com/HCRDRQ_17_1_1A\">";
                        break;

                    default:
                        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                        break;


                }
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";

                XmlDocument soapEnvelopeXml = new XmlDocument();

        soapEnvelopeXml.LoadXml(SoapEnvelop);


                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                // do something usefull here like update your UI.
                asyncResult.AsyncWaitHandle.WaitOne();

                // get the response from the completed web request.

                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                    Console.Write(soapResult);
                }
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    soapResult = text;

                }
            }

            return soapResult;
        }

        public static string ReadGDSError(string xmlResponse, int GDSType)
        {
            string Message = string.Empty;
            try
            {

                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlResponse);

                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XmlNode node = null;
                string nodeRoot = "/soapenv:Envelope/soapenv:Body/";
                string nodeError = "/si:errorGroup/si:errorDescription/si:freeText";
                string nodeWarning = "/si:generalErrorInfo/si:errorWarningDescription";

                xmlnsManager.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                xmlnsManager.AddNamespace("awsse", "http://xml.amadeus.com/2010/06/Session_v3");
                xmlnsManager.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");

                #region  GDS Error
                switch (GDSType)
                {
                    case (int)CLayer.ObjectStatus.GDSType.HotelSell:
                        xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/HBKRCR_17_1_1A");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:Hotel_SellReply" + nodeError, xmlnsManager);
                        break;
                    case (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElements:
                        xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/PNRADD_14_1_1A");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:PNR_Reply" + nodeError, xmlnsManager);
                        break;
                    case (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailability:
                        xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelAvailRS" + nodeError, xmlnsManager);
                        break;
                    case (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailabilityLiveSearch:
                        xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelAvailRS" + nodeError, xmlnsManager);
                        break;
                    case (int)CLayer.ObjectStatus.GDSType.HotelEnhancedPricing:
                        xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelAvailRS" + nodeError, xmlnsManager);
                        break;
                    case (int)CLayer.ObjectStatus.GDSType.HotelDescriptiveinfo:
                        xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelDescriptiveInfoRQ" + nodeError, xmlnsManager);
                        break;
                    case (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElementsConfirm:
                        xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/PNRADD_14_1_1A");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:PNR_Reply" + nodeError, xmlnsManager);
                        break;
                    case (int)CLayer.ObjectStatus.GDSType.PNRRetrieve:
                        xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/PNRACC_14_1_1A");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:PNR_Reply" + nodeError, xmlnsManager);
                        break;
                    case (int)CLayer.ObjectStatus.GDSType.PNRCancel:
                        xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/PNRXCL_15_1_1A");
                        node = xmlDoc.SelectSingleNode(nodeRoot + "si:PNR_Reply" + nodeError, xmlnsManager);
                        break;

                }
                #endregion 
                // You'd access the full path like this
                if (node != null)
                {
                    Message = node.InnerText;
                }
                else
                {
                    node = xmlDoc.SelectSingleNode("/soapenv:Envelope/soapenv:Body/si:PNR_Reply/si:generalErrorInfo/si:errorWarningDescription", xmlnsManager);
                    foreach (XmlNode item in node)
                    {
                        if (item.Name != "freeTextDetails")
                        {

                            Message = Message + "," + item.InnerText.Trim();
                        }
                    }
                    Message = Message.Substring(1, Message.Length - 1);
                }
                Message = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Message);

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Message;
        }

        public static string GenerateRoomDescription(string RoomTypeCode)
        {
            string result = string.Empty;
            if (RoomTypeCode == "ROH")
            {
                result = RoomTypeDescriptionFirst(RoomTypeCode);
            }
            else
            {
                result = RoomTypeDescriptionFirst(RoomTypeCode) + " " + RoomTypeDescriptionSecond(RoomTypeCode);
            }
            // +" "+ RoomTypeDescriptionThird(RoomTypeCode);
            result = result.ToLower().Contains("run of the house") ? "Run of the house" : result;

            return result;
        }

        public static string RoomTypeDescriptionFirst(string pValue)
        {
            string pFirst = pValue.Substring(0, 1);
            string pMain = pValue;

            switch (pFirst)
            {

                case "A":
                case "E":
                    pValue = "Delux room,";
                    break;
                case "B":
                case "I":
                case "F":
                case "S":
                    pValue = "Semi delux room,";
                    break;
                case "C":
                case "G":
                case "T":
                case "J":
                case "*":
                    pValue = "Standard room,";
                    break;
                case "D":
                case "H":
                case "K":
                case "U":
                    pValue = "Minimum,";
                    break;

                case "P":
                case "M":
                case "R":
                case "L":
                    pValue = "Room,";

                    break;
                default:
                    pValue = "Run of the House";
                    break;


            }
            if (pMain == "ROH")
            {
                pValue = "Run of the House";
            }

            return pValue;

        }
        //public static string RoomTypeDescriptionSecond1(string pValue)
        //{
        //    string pSecond = pValue.Substring(1, 1);



        //    switch (pSecond)
        //    {
        //        case "1":
        //            if (pValue.Substring(2, 1) == "*")
        //            {
        //                pValue = "One bed in room," + " " + "Bed size can be increased on request*";
        //            }
        //            else
        //            {
        //                pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
        //            }

        //            break;
        //        case "2":
        //            if (pValue.Substring(2, 1) == "*")
        //            {
        //                pValue = "Two beds in room," + " " + "Bed size can be increased on request*";
        //            }
        //            else
        //            {
        //                pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
        //            }
        //            break;
        //        case "3":
        //            if (pValue.Substring(2, 1) == "*")
        //            {
        //                pValue = "Three beds in room," + " " + "Bed size can be increased on request*";
        //            }
        //            else
        //            {
        //                pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
        //            }
        //            break;
        //        case "4":
        //            if (pValue.Substring(2, 1) == "*")
        //            {
        //                pValue = "Three beds in room," + " " + "Bed size can be increased on request*";
        //            }
        //            else
        //            {
        //                pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
        //            }

        //            break;
        //        case "*":
        //            if (pValue.Substring(2, 1) == "*")
        //            {
        //                pValue = "Number of beds and bed size can be increased on request*";
        //            }
        //            else
        //            {
        //                pValue = RoomTypeDescriptionThird(pValue.Substring(2, 1)) + "," + " Number of beds can be increased on request*";
        //            }

        //            break;
        //        default:
        //            pValue = pValue + " " + "Minimum";
        //            break;


        //    }

        //    return pValue;

        //}

        public static string RoomTypeDescriptionSecond(string pValue)
        {
            string pSecond = pValue.Substring(1, 1);

            if (pSecond == "*")
            {
                if (pValue.Substring(2, 1) == "*")
                {
                    pValue = "Number of beds and bed size can be increased on request*";
                }
                else
                {
                    pValue = RoomTypeDescriptionThird(pValue.Substring(2, 1)) + "," + " Number of beds can be increased on request*";
                }
            }
            else
            {
                if (pValue.Substring(2, 1) == "*")
                {
                    pValue = pSecond.ToString() + " bed in room," + " " + "Bed size can be increased on request*";
                }
                else
                {
                    pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
                }
            }
            //else
            //{
            //    pValue = pValue + " " + "Minimum";
            //}


            //switch (pSecond)
            //{
            //    case "1":
            //        if (pValue.Substring(2, 1) == "*")
            //        {
            //            pValue = "One bed in room," + " " + "Bed size can be increased on request*";
            //        }
            //        else
            //        {
            //            pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
            //        }

            //        break;
            //    case "2":
            //        if (pValue.Substring(2, 1) == "*")
            //        {
            //            pValue = "Two beds in room," + " " + "Bed size can be increased on request*";
            //        }
            //        else
            //        {
            //            pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
            //        }
            //        break;
            //    case "3":
            //        if (pValue.Substring(2, 1) == "*")
            //        {
            //            pValue = "Three beds in room," + " " + "Bed size can be increased on request*";
            //        }
            //        else
            //        {
            //            pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
            //        }
            //        break;
            //    case "4":
            //        if (pValue.Substring(2, 1) == "*")
            //        {
            //            pValue = "Three beds in room," + " " + "Bed size can be increased on request*";
            //        }
            //        else
            //        {
            //            pValue = pSecond + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
            //        }

            //        break;
            //    case "*":
            //        if (pValue.Substring(2, 1) == "*")
            //        {
            //            pValue = "Number of beds and bed size can be increased on request*";
            //        }
            //        else
            //        {
            //            pValue = RoomTypeDescriptionThird(pValue.Substring(2, 1)) + "," + " Number of beds can be increased on request*";
            //        }

            //        break;
            //    default:
            //        pValue = pValue + " " + "Minimum";
            //        break;


            //}

            return pValue;

        }
        public static bool DoesImageExistRemotely(string uriToImage)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToImage);

            request.Method = "HEAD";

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (WebException) { return false; }
            catch
            {
                return false;
            }
        }
        public static string RoomTypeDescriptionThird(string pValue)
        {
            switch (pValue)
            {
                case "S":
                    pValue = "single bed";
                    break;
                case "T":
                    pValue = "twin bed";
                    break;
                case "D":
                    pValue = "double bed";
                    break;
                case "Q":
                    pValue = "queen size bed";
                    break;
                case "K":
                    pValue = "king size bed";
                    break;
                case "W":
                    pValue = "water bed bed";
                    break;
                case "P":
                    pValue = "Standard bed";
                    break;
                case "*":
                    pValue = "Bed size can be increased on request*";
                    break;

            }

            return pValue;

        }
        public static decimal GetGDSConvertedAmount(decimal amount, decimal RateConversion)
        {
            decimal result = 0;
            try
            {
                //if ((HttpContext.Current.Session["GDSCurrencyConversion"] != null) || (Convert.ToString(HttpContext.Current.Session["GDSCurrencyConversion"]) != ""))
                //{
                //    CLayer.GDSCurrencyConversions objCurrencyConversions = (CLayer.GDSCurrencyConversions)HttpContext.Current.Session["GDSCurrencyConversion"];
                //    result = amount * Math.Round(objCurrencyConversions.RateConversion, 4);

                //    result = Math.Round(result, 0);
                //}
                //else
                //{
                //    result = amount;
                //}
                if (RateConversion > 0)
                {
                    result = amount * Math.Round(RateConversion, 4);
                }
                else
                {
                    result = amount;
                }

            }
            catch (Exception ex)
            {
                // Common.LogHandler.HandleError(ex);
                throw ex;
            }
            return result;
        }
        public static int GetStarRating(string pXml)
        {
            int result = 0;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(pXml);
                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XDocument doc = XDocument.Parse(xmlDoc.InnerXml.ToString());

                var Affliations = from p in doc.Descendants()
                                  where p.Name.LocalName == "Award"
                                  select p;

                foreach (var item in Affliations)
                {

                    result = Convert.ToInt32(GetStringValue(item, "Rating"));
                }
            }
            catch (Exception ex)
            {
                // Common.LogHandler.AddLog(ex.Message);
                throw ex;
            }
            return result;

        }
        private static string GetStringValue(XElement NodeItem, string pField)
        {
            string pValue = string.Empty;
            try
            {
                pValue = (NodeItem.Attribute(pField).Name.LocalName != pField) ? "" : NodeItem.Attribute(pField).Value;

            }
            catch (Exception ex)
            {
                pValue = "";
            }
            return pValue;
        }
    }
}