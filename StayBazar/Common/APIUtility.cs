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
using Microsoft.AspNet.Identity;
using System.Xml.Linq;
using System.Text.RegularExpressions;

namespace StayBazar.Common
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
        public static string GDSCountryCode = string.Empty;
        public static string GDSCurrencyCode = string.Empty;
        public static string GDSKeyWord = string.Empty;
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
            if (!IsStateless)
            {
                Header = Header + "<awsse:Session TransactionStatusCode = \"Start\" xmlns:awsse = \"http://xml.amadeus.com/2010/06/Session_v3\" />";
            }
            Header = Header + "</soapenv:Header>";

            return Header;
        }
        public static string SetStatefulSoapHeader(string Action, string SessionId, int SequenceNumber, string SecurityToken, bool IsCompleteReservation = false)
        {
            string NodeAddress = (IsCompleteReservation) ? "https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZR" : BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL);

            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<awsse:Session TransactionStatusCode = \"InSeries\" xmlns:awsse = \"http://xml.amadeus.com/2010/06/Session_v3\" >";
            Header = Header + "<awsse:SessionId>" + SessionId + "</awsse:SessionId>";
            Header = Header + "<awsse:SequenceNumber>" + SequenceNumber + "</awsse:SequenceNumber>";
            Header = Header + "<awsse:SecurityToken>" + SecurityToken + "</awsse:SecurityToken>";
            Header = Header + "</awsse:Session>";
            Header = Header + "<add:MessageID xmlns:add = \"http://www.w3.org/2005/08/addressing\">64f06787 - bf23 - 9ad2 - 417e-ff096170184e</add:MessageID>";
            Header = Header + "<add:Action xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Action + "</add:Action>";
            Header = Header + "<add:To xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + NodeAddress + "</add:To>";
            Header = Header + "</soapenv:Header>";
            return Header;
        }
        public static string SetStatefulSoapHeader(string Action, string SessionId, int SequenceNumber, string SecurityToken,string MoreDataIndicator)
        {
            string NodeAddress =  BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL);

            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<awsse:Session TransactionStatusCode = \"InSeries\" xmlns:awsse = \"http://xml.amadeus.com/2010/06/Session_v3\">";
            Header = Header + "<awsse:SessionId>" + SessionId + "</awsse:SessionId>";
            Header = Header + "<awsse:SequenceNumber>" + SequenceNumber + "</awsse:SequenceNumber>";
            Header = Header + "<awsse:SecurityToken>" + SecurityToken + "</awsse:SecurityToken>";
            Header = Header + "</awsse:Session>";
            Header = Header + "<add:MessageID xmlns:add = \"http://www.w3.org/2005/08/addressing\">64f06787 - bf23 - 9ad2 - 417e-ff096170184e</add:MessageID>";
            Header = Header + "<add:Action xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + Action + "</add:Action>";
            Header = Header + "<add:To xmlns:add = \"http://www.w3.org/2005/08/addressing\">" + NodeAddress + "</add:To>";
            Header = Header + "</soapenv:Header>";
            return Header;
        }
        public static string SetSecuritySignOutSoapHeader(string Action, string SessionId, int SequenceNumber, string SecurityToken, bool IsCompleteReservation = false)
        {
            string NodeAddress = (IsCompleteReservation) ? BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL) : BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL);

            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<wbs:Session xmlns:wbs =\"http://xml.amadeus.com/ws/2009/01/WBS_Session-2.0.xsd\">";
            Header = Header + "<wbs:SessionId>" + SessionId + "</wbs:SessionId>";
            Header = Header + "<wbs:SequenceNumber>" + SequenceNumber + "</wbs:SequenceNumber>";
            Header = Header + "<wbs:SecurityToken>" + SecurityToken + "</wbs:SecurityToken>";
            Header = Header + "</wbs:Session></soapenv:Header>";
            return Header;
        }
        public static string SetHotelMultiSingleAvailabilityBody(CLayer.SearchCriteria sch)
        {
            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            //   GuestCount = GuestCount <= 0 ? 2 : GuestCount;

            GuestCount = GuestCount <= 0 ? 1 : GuestCount;

            GDSCountryCode = Convert.ToString(HttpContext.Current.Session["GDSCountryCode"]);
            GDSCurrencyCode = Convert.ToString(HttpContext.Current.Session["GDSCurrencyCode"]);

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelAvailRQ xmlns = \"http://www.opentravel.org/OTA/2003/05\" EchoToken = \"MultiSingle\" Version = \"4.000\" SummaryOnly = \"true\" AvailRatesOnly = \"true\" OnRequestInd = \"true\" RateRangeOnly = \"true\" ExactMatchOnly = \"false\" SearchCacheLevel = \"VeryRecent\" RequestedCurrency = \"INR\" RateDetailsInd = \"true\" >";
            Body = Body + "<AvailRequestSegments>";
            Body = Body + "<AvailRequestSegment InfoSource = \"Distribution\">";
            Body = Body + "<HotelSearchCriteria AvailableOnlyIndicator = \"true\">";
            Body = Body + "<Criterion ExactMatch = \"true\">";
            Body = Body + "<Address><CityName>" + sch.Destination + "</CityName><CountryName Code = '" + GDSCountryCode + "' /></Address>";
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
        public static string SetHotelMultiSingleAvailabilityBodyMore(CLayer.SearchCriteria sch)
        {
            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            //   GuestCount = GuestCount <= 0 ? 2 : GuestCount;

            GuestCount = GuestCount <= 0 ? 1 : GuestCount;

            GDSCountryCode = Convert.ToString(HttpContext.Current.Session["GDSCountryCode"]);
            GDSCurrencyCode = Convert.ToString(HttpContext.Current.Session["GDSCurrencyCode"]);

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelAvailRQ xmlns = \"http://www.opentravel.org/OTA/2003/05\" EchoToken = \"MultiSingle\" Version = \"4.000\" SummaryOnly = \"true\" AvailRatesOnly = \"true\" OnRequestInd = \"true\" RateRangeOnly = \"true\" ExactMatchOnly = \"false\" SearchCacheLevel = \"VeryRecent\" RequestedCurrency = \"INR\" RateDetailsInd = \"true\" >";
            Body = Body + "<AvailRequestSegments>";
            Body = Body + "<AvailRequestSegment InfoSource = \"Distribution\" MoreDataEchoToken=\"" + sch.Moreindicator + "\">";
            Body = Body + "<HotelSearchCriteria AvailableOnlyIndicator = \"true\">";
            Body = Body + "<Criterion ExactMatch = \"true\" >";
            Body = Body + "<Address><CityName>" + sch.Destination + "</CityName><CountryName Code = '" + GDSCountryCode + "' /></Address>";
            Body = Body + "<StayDateRange Start = \"" + sch.CheckIn.ToString("yyyy-MM-dd") + "\" End = \"" + sch.CheckOut.ToString("yyyy-MM-dd") + "\"/>";
            Body = Body + "<RoomStayCandidates>";
            Body = Body + "<RoomStayCandidate RoomID = \"1\" Quantity = \"" + BedRoomsQuantity + "\">";
            Body = Body + "<GuestCounts>";
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
        public static string SetHotelMultiSingleAvailabilityBody(CLayer.SearchCriteria sch, string HotelCode)
        {
            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            //  GuestCount = GuestCount <= 0 ? 2 : GuestCount;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;

            GDSCountryCode = Convert.ToString(HttpContext.Current.Session["GDSCountryCode"]);
            GDSCurrencyCode = Convert.ToString(HttpContext.Current.Session["GDSCurrencyCode"]);

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelAvailRQ xmlns = \"http://www.opentravel.org/OTA/2003/05\" EchoToken = \"MultiSingle\" Version = \"4.000\" SummaryOnly = \"true\" AvailRatesOnly = \"true\" OnRequestInd = \"true\" RateRangeOnly = \"true\" ExactMatchOnly = \"false\" SearchCacheLevel = \"VeryRecent\" RequestedCurrency = \"INR\"  RateDetailsInd = \"true\" >";
            Body = Body + "<AvailRequestSegments>";
            Body = Body + "<AvailRequestSegment InfoSource = \"Distribution\">";
            Body = Body + "<HotelSearchCriteria AvailableOnlyIndicator = \"true\">";
            Body = Body + "<Criterion ExactMatch = \"true\">";
            Body = Body + "<HotelRef HotelCode = \"" + HotelCode + "\"/>";
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
        public static string PNR_Cancel(string ControlRefNumber = "", int optioncode = 0)
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
            Body = Body + "<soapenv:Body>";
            Body = Body + "<PNR_Retrieve xmlns=\"http://xml.amadeus.com/PNRRET_15_1_1A\">";
            Body = Body + "<retrievalFacts>";
            Body = Body + "<retrieve>";
            Body = Body + "<type>2</type>";
            Body = Body + "</retrieve>";
            Body = Body + "<reservationOrProfileIdentifier>";
            Body = Body + "<reservation>";
            Body = Body + "<controlNumber>" + ControlRefNumber + "</controlNumber>";
            Body = Body + "</reservation>";
            Body = Body + "</reservationOrProfileIdentifier>";
            Body = Body + "</retrievalFacts>";
            Body = Body + "</PNR_Retrieve>";
            Body = Body + "</soapenv:Body>";

            return Body;
        }
        public static string SecuritySignOut()
        {
            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<Security_SignOut xmlns = \"http://xml.amadeus.com/VLSSLQ_06_1_1A\"></Security_SignOut>";
            Body = Body + "</soapenv:Body>";
            return Body;
        }
        public static string PNR_AddMultielements(string HotelCode, string BookingCode, string Email = "", int optioncode = 0)
        {

            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;
            int BookingCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) > 1 ? Convert.ToInt32(HttpContext.Current.Session["Adults"]) : 1;



            long LoggedInUser = 0;
            LoggedInUser = Convert.ToInt64(HttpContext.Current.Session[CLayer.ObjectStatus.GUEST_ID_SESSION]);
            if (LoggedInUser == 0)
            {
                LoggedInUser = Convert.ToInt64(HttpContext.Current.Session["LoggedInUser"]);
            }
            CLayer.GDSUserAddress GDSUser = BLayer.User.GDUSUserDetails(LoggedInUser);


            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<PNR_AddMultiElements xmlns =\"http://xml.amadeus.com/PNRADD_15_1_1A\">";
            string InnerBody = string.Empty;

            if (optioncode <= 0)
            {
                Body = Body + "<pnrActions>";
                Body = Body + "<optionCode>" + optioncode + "</optionCode>";
                Body = Body + "</pnrActions>";
                //Body = Body + "<travellerInfo>";
                //Body = Body + "<elementManagementPassenger>";
                //Body = Body + "<reference>";
                //Body = Body + "<qualifier>PR</qualifier>";
                //Body = Body + "<number>1</number>";
                //Body = Body + "</reference>";
                //Body = Body + "<segmentName>NM</segmentName>";
                //Body = Body + "</elementManagementPassenger>";
                //Body = Body + "<passengerData>";
                //Body = Body + "<travellerInformation>";
                //Body = Body + "<traveller>";
                //Body = Body + "<surname>Test</surname>";
                //Body = Body + "<quantity>1</quantity>";
                //Body = Body + "</traveller>";
                //Body = Body + "<passenger>";
                //Body = Body + "<firstName>"+ GDSUser.FirstName.Replace("Mr.","")+ "</firstName>";
                //Body = Body + "</passenger>";
                //Body = Body + "</travellerInformation>";
                //Body = Body + "</passengerData>";
                //Body = Body + "</travellerInfo>";
                for (int i = 1; i <= BookingCount; i++)
                {
                    InnerBody = InnerBody + "<travellerInfo>";
                    InnerBody = InnerBody + "<elementManagementPassenger>";
                    InnerBody = InnerBody + "<reference>";
                    InnerBody = InnerBody + "<qualifier>PR</qualifier>";
                    InnerBody = InnerBody + "<number>" + i + "</number>";
                    InnerBody = InnerBody + "</reference>";
                    InnerBody = InnerBody + "<segmentName>NM</segmentName>";
                    InnerBody = InnerBody + "</elementManagementPassenger>";
                    InnerBody = InnerBody + "<passengerData>";
                    InnerBody = InnerBody + "<travellerInformation>";
                    InnerBody = InnerBody + "<traveller>";
                    InnerBody = InnerBody + "<surname>TEST</surname>";
                    InnerBody = InnerBody + "<quantity>1</quantity>";
                    InnerBody = InnerBody + "</traveller>";
                    InnerBody = InnerBody + "<passenger>";
                    InnerBody = InnerBody + "<firstName>" + GDSUser.FirstName.Replace("Mr.", "") + "</firstName>";
                    InnerBody = InnerBody + "<type>ADT</type>";
                    InnerBody = InnerBody + "</passenger>";
                    InnerBody = InnerBody + "</travellerInformation>";
                    InnerBody = InnerBody + "</passengerData>";
                    InnerBody = InnerBody + "</travellerInfo>";

                    //Body = Body + "<travellerInfo>";
                    //Body = Body + "<elementManagementPassenger>";
                    //Body = Body + "<reference>";
                    //Body = Body + "<qualifier>PR</qualifier>";
                    //Body = Body + "<number>2</number>";
                    //Body = Body + "</reference>";
                    //Body = Body + "<segmentName>NM</segmentName>";
                    //Body = Body + "</elementManagementPassenger>";
                    //Body = Body + "<passengerData>";
                    //Body = Body + "<travellerInformation>";
                    //Body = Body + "<traveller>";
                    //Body = Body + "<surname>TEST</surname>";
                    //Body = Body + "<quantity>1</quantity>";
                    //Body = Body + "</traveller>";
                    //Body = Body + "<passenger>";
                    //Body = Body + "<firstName>TWO MR</firstName>";
                    //Body = Body + "<type>ADT</type>";
                    //Body = Body + "</passenger>";
                    //Body = Body + "</travellerInformation>";
                    //Body = Body + "</passengerData>";
                    //Body = Body + "</travellerInfo>";
                }

                Body = Body + InnerBody;
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
                Body = Body + "<number>1</number>";
                Body = Body + "</reference>";
                Body = Body + "<segmentName>AP</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<freetextData>";
                Body = Body + "<freetextDetail>";
                Body = Body + "<subjectQualifier>3</subjectQualifier>";
                Body = Body + "</freetextDetail>";
                Body = Body + "<longFreetext>" + GDSUser.Mobile + " agency number</longFreetext>";
                Body = Body + "</freetextData>";
                Body = Body + "</dataElementsIndiv>";
                Body = Body + "<dataElementsIndiv>";
                Body = Body + "<elementManagementData>";
                Body = Body + "<reference>";
                Body = Body + "<qualifier>OT</qualifier>";
                Body = Body + "<number>1</number>";
                Body = Body + "</reference>";
                Body = Body + "<segmentName>AP</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<freetextData>";
                Body = Body + "<freetextDetail>";
                Body = Body + "<subjectQualifier>3</subjectQualifier>";
                Body = Body + "<type>P02</type>";
                Body = Body + "</freetextDetail>";
                Body = Body + "<longFreetext>" + GDSUser.Email + "</longFreetext> ";
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

        public static string PNR_AddMultielementsOffline(string HotelCode, string BookingCode, string Email = "", int optioncode = 0,bool IsOffline=true)
        {

            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;
            int BookingCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) > 1 ? Convert.ToInt32(HttpContext.Current.Session["Adults"]) : 1;



            long LoggedInUser = 0;
            LoggedInUser = Convert.ToInt64(HttpContext.Current.Session[CLayer.ObjectStatus.GUEST_ID_SESSION]);
            if (LoggedInUser == 0)
            {
                LoggedInUser = Convert.ToInt64(HttpContext.Current.Session["LoggedInUser"]);
            }
            CLayer.GDSUserAddress GDSUser = BLayer.User.GDUSUserDetails(LoggedInUser);


            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<PNR_AddMultiElements xmlns =\"http://xml.amadeus.com/PNRADD_15_1_1A\">";
            string InnerBody = string.Empty;

            if (optioncode <= 0)
            {
                Body = Body + "<pnrActions>";
                Body = Body + "<optionCode>" + optioncode + "</optionCode>";
                Body = Body + "</pnrActions>";
                //Body = Body + "<travellerInfo>";
                //Body = Body + "<elementManagementPassenger>";
                //Body = Body + "<reference>";
                //Body = Body + "<qualifier>PR</qualifier>";
                //Body = Body + "<number>1</number>";
                //Body = Body + "</reference>";
                //Body = Body + "<segmentName>NM</segmentName>";
                //Body = Body + "</elementManagementPassenger>";
                //Body = Body + "<passengerData>";
                //Body = Body + "<travellerInformation>";
                //Body = Body + "<traveller>";
                //Body = Body + "<surname>Test</surname>";
                //Body = Body + "<quantity>1</quantity>";
                //Body = Body + "</traveller>";
                //Body = Body + "<passenger>";
                //Body = Body + "<firstName>"+ GDSUser.FirstName.Replace("Mr.","")+ "</firstName>";
                //Body = Body + "</passenger>";
                //Body = Body + "</travellerInformation>";
                //Body = Body + "</passengerData>";
                //Body = Body + "</travellerInfo>";
                for (int i = 1; i <= BookingCount; i++)
                {
                    InnerBody = InnerBody + "<travellerInfo>";
                    InnerBody = InnerBody + "<elementManagementPassenger>";
                    InnerBody = InnerBody + "<reference>";
                    InnerBody = InnerBody + "<qualifier>PR</qualifier>";
                    InnerBody = InnerBody + "<number>" + i + "</number>";
                    InnerBody = InnerBody + "</reference>";
                    InnerBody = InnerBody + "<segmentName>NM</segmentName>";
                    InnerBody = InnerBody + "</elementManagementPassenger>";
                    InnerBody = InnerBody + "<passengerData>";
                    InnerBody = InnerBody + "<travellerInformation>";
                    InnerBody = InnerBody + "<traveller>";
                    InnerBody = InnerBody + "<surname>TEST</surname>";
                    InnerBody = InnerBody + "<quantity>1</quantity>";
                    InnerBody = InnerBody + "</traveller>";
                    InnerBody = InnerBody + "<passenger>";
                 //   InnerBody = InnerBody + "<firstName>"+ HttpContext.Current.Session["GuestName"] +"</firstName>";
                    InnerBody = InnerBody + "<firstName>TESTMR</firstName>";
                    InnerBody = InnerBody + "<type>ADT</type>";
                    InnerBody = InnerBody + "</passenger>";
                    InnerBody = InnerBody + "</travellerInformation>";
                    InnerBody = InnerBody + "</passengerData>";
                    InnerBody = InnerBody + "</travellerInfo>";

                    //Body = Body + "<travellerInfo>";
                    //Body = Body + "<elementManagementPassenger>";
                    //Body = Body + "<reference>";
                    //Body = Body + "<qualifier>PR</qualifier>";
                    //Body = Body + "<number>2</number>";
                    //Body = Body + "</reference>";
                    //Body = Body + "<segmentName>NM</segmentName>";
                    //Body = Body + "</elementManagementPassenger>";
                    //Body = Body + "<passengerData>";
                    //Body = Body + "<travellerInformation>";
                    //Body = Body + "<traveller>";
                    //Body = Body + "<surname>TEST</surname>";
                    //Body = Body + "<quantity>1</quantity>";
                    //Body = Body + "</traveller>";
                    //Body = Body + "<passenger>";
                    //Body = Body + "<firstName>TWO MR</firstName>";
                    //Body = Body + "<type>ADT</type>";
                    //Body = Body + "</passenger>";
                    //Body = Body + "</travellerInformation>";
                    //Body = Body + "</passengerData>";
                    //Body = Body + "</travellerInfo>";
                }

                Body = Body + InnerBody;
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
                Body = Body + "<number>1</number>";
                Body = Body + "</reference>";
                Body = Body + "<segmentName>AP</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<freetextData>";
                Body = Body + "<freetextDetail>";
                Body = Body + "<subjectQualifier>3</subjectQualifier>";
                Body = Body + "</freetextDetail>";
              //  Body = Body + "<longFreetext>"+HttpContext.Current.Session["GuestPhone"]+" agency number</longFreetext>";
                Body = Body + "<longFreetext>1234567898 agency number</longFreetext>";
                Body = Body + "</freetextData>";
                Body = Body + "</dataElementsIndiv>";
                Body = Body + "<dataElementsIndiv>";
                Body = Body + "<elementManagementData>";
                Body = Body + "<reference>";
                Body = Body + "<qualifier>OT</qualifier>";
                Body = Body + "<number>1</number>";
                Body = Body + "</reference>";
                Body = Body + "<segmentName>AP</segmentName>";
                Body = Body + "</elementManagementData>";
                Body = Body + "<freetextData>";
                Body = Body + "<freetextDetail>";
                Body = Body + "<subjectQualifier>3</subjectQualifier>";
                Body = Body + "<type>P02</type>";
                Body = Body + "</freetextDetail>";
                //  Body = Body + "<longFreetext>"+ HttpContext.Current.Session["GuestEmail"] +"</longFreetext> ";
                Body = Body + "<longFreetext>test@test.com</longFreetext> ";
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

        public static string PNR_AddMultielements1(string HotelCode, string BookingCode, string Email = "", int optioncode = 0)
        {
            //string Header = string.Empty;
            //Header = SetSoapHeader(Url, Action, false);

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<PNR_AddMultiElements xmlns =\"http://xml.amadeus.com/PNRADD_15_1_1A\">";
            if (optioncode <= 0)
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
        public static string Hotel_Sell(string PNR, string BookingCode)
        {
            DateTime Checkin = Convert.ToDateTime(HttpContext.Current.Session["CheckIn"]);

            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;
            int BookingCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) > 1 ? Convert.ToInt32(HttpContext.Current.Session["Adults"]) : 1;
            BedRoomsQuantity = BookingCount <= 1 ? 1 : BedRoomsQuantity;

            string GuaranteeCode = Convert.ToString(HttpContext.Current.Session["GuaranteeCode"]);
            int PaymentType = (GuaranteeCode == "31") ? 1 : 2;

            string Body = string.Empty;
            string InnerBody = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<Hotel_Sell xmlns=\"http://xml.amadeus.com/HBKRCQ_17_1_1A\">";
            for (int i = 1; i <= BedRoomsQuantity; i++)
            {
                InnerBody = InnerBody + "<roomStayData>";
                InnerBody = InnerBody + "<markerRoomStayData></markerRoomStayData>";
                InnerBody = InnerBody + "<globalBookingInfo>";
                InnerBody = InnerBody + "<markerGlobalBookingInfo></markerGlobalBookingInfo>";
                InnerBody = InnerBody + "<bookingSource>";
                InnerBody = InnerBody + "<originIdentification>";
                InnerBody = InnerBody + "<originatorId>96601746</originatorId>";
                InnerBody = InnerBody + "</originIdentification>";
                InnerBody = InnerBody + "</bookingSource>";
                InnerBody = InnerBody + "<representativeParties>";
                InnerBody = InnerBody + "<occupantList>";
                InnerBody = InnerBody + "<passengerReference>";
                InnerBody = InnerBody + "<type>BHO</type>";
                InnerBody = InnerBody + "<value>2</value>";
                InnerBody = InnerBody + "</passengerReference>";
                InnerBody = InnerBody + "</occupantList>";
                InnerBody = InnerBody + "</representativeParties>";
                InnerBody = InnerBody + "</globalBookingInfo>";
                InnerBody = InnerBody + "<roomList>";
                InnerBody = InnerBody + "<markerRoomstayQuery></markerRoomstayQuery>";
                InnerBody = InnerBody + "<roomRateDetails>";
                InnerBody = InnerBody + "<marker></marker>";
                InnerBody = InnerBody + "<hotelProductReference>";
                InnerBody = InnerBody + "<referenceDetails>";
                InnerBody = InnerBody + "<type>BC</type>";
                InnerBody = InnerBody + "<value>" + BookingCode + "</value>";
                InnerBody = InnerBody + "</referenceDetails>";
                InnerBody = InnerBody + "</hotelProductReference>";
                InnerBody = InnerBody + "<markerOfExtra></markerOfExtra>";
                InnerBody = InnerBody + "</roomRateDetails>";
                InnerBody = InnerBody + "<guaranteeOrDeposit>";
                InnerBody = InnerBody + "<paymentInfo>";
                InnerBody = InnerBody + "<paymentDetails>";
                InnerBody = InnerBody + "<formOfPaymentCode>1</formOfPaymentCode>";
                InnerBody = InnerBody + "<paymentType>" + PaymentType + "</paymentType>";
                InnerBody = InnerBody + "<serviceToPay>3</serviceToPay>";
                InnerBody = InnerBody + "</paymentDetails>";
                InnerBody = InnerBody + "</paymentInfo>";
                InnerBody = InnerBody + "<groupCreditCardInfo>";
                InnerBody = InnerBody + "<creditCardInfo>";
                InnerBody = InnerBody + "<ccInfo>";
                InnerBody = InnerBody + "<vendorCode>VI</vendorCode>";
                InnerBody = InnerBody + "<cardNumber>4444333322221111</cardNumber>";
                //InnerBody = InnerBody + "<cardNumber>4111111111111111</cardNumber>";
                InnerBody = InnerBody + "<securityId>123</securityId>";
                InnerBody = InnerBody + "<expiryDate>" + Checkin.AddMonths(1).ToString("MMyy") + "</expiryDate>";
                InnerBody = InnerBody + "<ccHolderName>AMADEUSTEST</ccHolderName>";
                InnerBody = InnerBody + "</ccInfo>";
                InnerBody = InnerBody + "</creditCardInfo>";
                InnerBody = InnerBody + "</groupCreditCardInfo>";
                InnerBody = InnerBody + "</guaranteeOrDeposit>";
                InnerBody = InnerBody + "</roomList>";
                InnerBody = InnerBody + "</roomStayData>";
            }
            Body = Body + InnerBody;
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
            Body = Body + " <HotelDescriptiveInfo HotelCode = \"" + HotelCode + "\">";
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
            Body = Body + "<Hotel_CompleteReservationDetails xmlns=\"http://xml.amadeus.com/HCRDRQ_17_1_1A\">";
            Body = Body + "<retrievalKeyGroup>";
            Body = Body + "<retrievalKey>";
            Body = Body + "<reservation>";
            Body = Body + "<companyId>1A</companyId>";
            Body = Body + "<controlNumber>" + ControlNumber + "</controlNumber>";
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
            Header = Header + "<awsse:SessionId>" + SessionId + "</awsse:SessionId>";
            Header = Header + "<awsse:SequenceNumber>" + SequenceNumber + "</awsse:SequenceNumber>";
            Header = Header + "<awsse:SecurityToken>" + SecurityToken + "</awsse:SecurityToken>";

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
            //  GuestCount = GuestCount <= 0 ? 2 : GuestCount;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;

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
            Body = Body + "<RoomStayCandidate RoomTypeCode=\"" + RoomTypeCode + "\" RoomID=\"1\" Quantity=\"" + BedRoomsQuantity + "\" BookingCode=\"" + BookingCode + "\">";
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
        public static string SetHotelMultiSingleAvailabilityBodyPriceingdetails(string HotelCode, string start, string end)
        {
            int BedRoomsQuantity = Convert.ToInt32(HttpContext.Current.Session["Bedrooms"]);
            int GuestCount = Convert.ToInt32(HttpContext.Current.Session["Adults"]) + Convert.ToInt32(HttpContext.Current.Session["Children"]); ;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;

            GDSCountryCode = Convert.ToString(HttpContext.Current.Session["GDSCountryCode"]);
            GDSCurrencyCode = Convert.ToString(HttpContext.Current.Session["GDSCurrencyCode"]);

            string Body = string.Empty;
            Body = Body + "<soapenv:Body>";
            Body = Body + "<OTA_HotelAvailRQ xmlns = \"http://www.opentravel.org/OTA/2003/05\" EchoToken = \"MultiSingle\" Version = \"4.000\" SummaryOnly = \"true\" AvailRatesOnly = \"true\" OnRequestInd = \"true\" RateRangeOnly = \"true\" ExactMatchOnly = \"false\" SearchCacheLevel = \"VeryRecent\" RequestedCurrency = \"INR\"  RateDetailsInd = \"true\" >";
            Body = Body + "<AvailRequestSegments>";
            Body = Body + "<AvailRequestSegment InfoSource = \"Distribution\">";
            Body = Body + "<HotelSearchCriteria AvailableOnlyIndicator = \"true\">";
            Body = Body + "<Criterion ExactMatch = \"true\">";
            Body = Body + "<HotelRef HotelCode = \"" + HotelCode + "\"/>";
            Body = Body + "<StayDateRange Start = \"" + start + "\" End = \"" + end + "\"/>";
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

        public static HttpWebRequest CreateWebRequest(string url, string action)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //   ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        public static XmlDocument CreateSoapEnvelope(string XmlText)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(string.Format(@"{0}", XmlText));
            return soapEnvelopeDocument;
        }

        public static HttpWebRequest InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
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
        public static string ExecutePNRCancel(string ControlNumber, int pOptionCode = 0)
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
        public static string GetAmedusResult(string Action, string pSoapHeadr, string pSoapBody, string XMLtext = "")
        {
            string soapResult = string.Empty;
            try
            {
                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL);
                var _action = Action;

                string SoapHeader = pSoapHeadr;
                string SoapBody = pSoapBody;

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                switch (Action)
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
                    case "http://xml.amadeus.com/VLSSLQ_06_1_1A":
                        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:wbs=\"http://xml.amadeus.com/ws/2009/01/WBS_Session-2.0.xsd\" xmlns:vls=\"http://xml.amadeus.com/VLSSLQ_06_1_1A\">";
                        break;

                    default:
                        SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                        break;


                }
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";

                XmlDocument soapEnvelopeXml = new XmlDocument();

                soapEnvelopeXml.LoadXml(SoapEnvelop);

                HttpContext.Current.Session["GDSRequest"] = Convert.ToString(soapEnvelopeXml.InnerXml);


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

        public static void GenerateGDSTransactionLog(string GDSSearchCriteria = "", string GDSResult = "", int GDSUserID = 0, int GDSType = 0, long BookingID = 0,int GDSBookingType=1)
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
                objLog.GDSRequest = Convert.ToString(HttpContext.Current.Session["GDSRequest"]);
                objLog.GDSResult = GDSResult;
                if (GDSResult.ToLower().Contains("errorcode"))
                {
                    objLog.GDSError = GDSResult;
                  
                    if(GDSType==3)
                    {
                        objLog.GDSErrorMessage = GetHotelRateError(GDSResult);
                        objLog.GDSErrorMessage = objLog.GDSErrorMessage==""? ReadGDSError(GDSResult, GDSType) : objLog.GDSErrorMessage;
                    }
                    else
                    {
                        objLog.GDSErrorMessage = ReadGDSError(GDSResult, GDSType);
                    }
                    string Log = objLog.GDSErrorMessage + "-" + GDSTransID;
                    Common.LogHandler.AddLog(Log);
                }
                objLog.UserID = GDSUserID;
                objLog.GDSType = GDSType;
                objLog.GDSBookingType = GDSBookingType;
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
        //public static CLayer.Rates ReadGDSPrice(string xmlResponse, int GDSType)
        //{
        //    string Message = string.Empty;
        //    CLayer.Rates AmadeusRates = new CLayer.Rates();
        //    try
        //    {

        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.LoadXml(xmlResponse);

        //        XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
        //        XmlNode node = null;
        //        string nodeRoot = "/soapenv:Envelope/soapenv:Body/";
        //        string nodevalue = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RoomRates";

        //        string nodeError = "/si:errorGroup/si:errorDescription/si:freeText";
        //        string nodeWarning = "/si:generalErrorInfo/si:errorWarningDescription";

        //        xmlnsManager.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
        //        xmlnsManager.AddNamespace("awsse", "http://xml.amadeus.com/2010/06/Session_v3");
        //        xmlnsManager.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");

        //        #region  GDS Error
        //        switch (GDSType)
        //        {
        //            case (int)CLayer.ObjectStatus.GDSType.HotelSell:
        //                xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/HBKRCR_17_1_1A");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:Hotel_SellReply" + nodeError, xmlnsManager);
        //                break;
        //            case (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElements:
        //                xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/PNRADD_14_1_1A");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:PNR_Reply" + nodeError, xmlnsManager);
        //                break;
        //            case (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailability:
        //                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelAvailRS" + nodeError, xmlnsManager);
        //                break;
        //            case (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailabilityLiveSearch:
        //                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelAvailRS" + nodeError, xmlnsManager);
        //                break;
        //            case (int)CLayer.ObjectStatus.GDSType.HotelEnhancedPricing:
        //                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelAvailRS" + nodevalue, xmlnsManager);
        //                break;
        //            case (int)CLayer.ObjectStatus.GDSType.HotelDescriptiveinfo:
        //                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelDescriptiveInfoRQ" + nodeError, xmlnsManager);
        //                break;
        //            case (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElementsConfirm:
        //                xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/PNRADD_14_1_1A");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:PNR_Reply" + nodeError, xmlnsManager);
        //                break;
        //            case (int)CLayer.ObjectStatus.GDSType.PNRRetrieve:
        //                xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/PNRACC_14_1_1A");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:PNR_Reply" + nodeError, xmlnsManager);
        //                break;
        //            case (int)CLayer.ObjectStatus.GDSType.PNRCancel:
        //                xmlnsManager.AddNamespace("si", "http://xml.amadeus.com/PNRXCL_15_1_1A");
        //                node = xmlDoc.SelectSingleNode(nodeRoot + "si:PNR_Reply" + nodeError, xmlnsManager);
        //                break;

        //        }
        //        #endregion 
        //        // You'd access the full path like this
        //        if (node != null)
        //        {
        //            foreach (XmlNode item in node)
        //            {
        //                AmadeusRates.NoofAcc = 10;
        //                decimal TaxPercent = node.SelectSingleNode("/RoomRate/Rate/", xmlnsManager);
        //                TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
        //                decimal TaxAmount = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
        //                AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
        //                AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
        //                AmadeusRates.taxpercent = TaxPercent;

        //            }

        //                AmadeusRates.NoofAcc = 10;
        //            decimal TaxPercent = RatesItemAdv.RoomRate.Total.Taxes[0].Percent;
        //            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
        //            decimal TaxAmount = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
        //            AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
        //            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
        //            AmadeusRates.taxpercent = TaxPercent;

        //            AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
        //            AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
        //            AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

        //            AmadeusRates.BookingCode = BookingCode;

        //            if (CustomerStateID == BillingEntityStateID)
        //            {

        //                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
        //                AmadeusRates.SGSTTax = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
        //                AmadeusRates.CGSTTax = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));
        //                AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax - (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
        //                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
        //            }
        //            else
        //            {
        //                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
        //                AmadeusRates.IGSTTax = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (AmadeusRates.IGSTTaxPercent / 100));
        //                AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax - AmadeusRates.IGSTTax;
        //                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
        //            }


        //            //}
        //        }
        //        if (PricingResult != null)
        //        {
        //            if (PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
        //            {
        //                AmadeusRates.GuarenteeCode = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();
        //            }
        //        }

        //        if (PricingResultAdv != null)
        //        {
        //            if (PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
        //            {
        //                AmadeusRates.GuarenteeCode = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();

        //            }
        //        }

        //        Session["GuaranteeCode"] = AmadeusRates.GuarenteeCode;
        //        Message = node.InnerText;
        //        }
        //        else
        //        {
        //            //node = xmlDoc.SelectSingleNode("/soapenv:Envelope/soapenv:Body/si:PNR_Reply/si:generalErrorInfo/si:errorWarningDescription", xmlnsManager);
        //            //foreach (XmlNode item in node)
        //            //{
        //            //    if (item.Name != "freeTextDetails")
        //            //    {

        //            //        Message = Message + "," + item.InnerText.Trim();
        //            //    }
        //            //}
        //            //Message = Message.Substring(1, Message.Length - 1);
        //        }
        //      //  Message = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Message);

        //    }
        //    catch (Exception ex)
        //    {
        //        Message = ex.Message;
        //    }
        //    return AmadeusRates;
        //}
        public static string computeHash(string input, string name)
        {

            byte[] data = null;
            switch (name)
            {

                case "MD5":
                    data = HashAlgorithm.Create("MD5").ComputeHash(Encoding.ASCII.GetBytes(input));
                    break;
                case "SHA1":
                    data = HashAlgorithm.Create("SHA1").ComputeHash(Encoding.ASCII.GetBytes(input));
                    break;
                case "SHA512":
                    data = HashAlgorithm.Create("SHA512").ComputeHash(Encoding.ASCII.GetBytes(input));
                    break;
                default:
                    break;
            }

            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString().ToUpper();

        }
        public static string GetGDSCountry(string pValue)
        {
            string result = string.Empty;
            string[] pList = pValue.Split(',');
            if (pList.Length > 1)
            {
                result = pList[pList.Length - 1];
                GDSKeyWord = result.Trim();
                result = BLayer.Country.UpdateGDSCountry(GDSKeyWord);
            }

            return result;

        }
        public static string GetGDSDestination(string pValue)
        {
            string result = string.Empty;
            string[] pList = pValue.Split(',');
            if (pList.Length > 1)
            {
                result = pList[0];
                //BLayer.Country.UpdateGDSCountry(result);
            }
            else
            {
                result = pValue;
            }


            return result;

        }
        public static decimal GetGDSConvertedAmount(decimal amount)
        {
            decimal result = 0;
            try
            {
                if ((HttpContext.Current.Session["GDSCurrencyConversion"] != null) || (Convert.ToString(HttpContext.Current.Session["GDSCurrencyConversion"]) != ""))
                {
                    CLayer.GDSCurrencyConversions objCurrencyConversions = (CLayer.GDSCurrencyConversions)HttpContext.Current.Session["GDSCurrencyConversion"];
                    result = amount * Math.Round(objCurrencyConversions.RateConversion, 4);

                    result = Math.Round(result, 0);
                }
                else
                {
                    result = amount;
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return result;
        }
        public static decimal GetBookingAmount(decimal amount,string BookingCurrencyCode,string RequestedCurrencyCode, decimal ConversionRate)
        {
            
            if (RequestedCurrencyCode != BookingCurrencyCode)
            {
               amount  = Math.Round(amount * ConversionRate, 0);

            }
            else
            {
                amount  = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? amount : APIUtility.GetGDSConvertedAmount(amount);
            }
            return amount;
        }
        public static string GetCountryName(string pName)
        {
            string result = string.Empty;
            switch (pName.ToUpper())
            {
                case "US":
                case "USA":
                case "U S":
                case "U S A":
                    result = "United States";
                    break;
                case "UK":
                case "U K":
                    result = "United Kingdom";
                    break;
                case "UAE":
                case "U A E":
                    result = "United Arab Emirates";
                    break;
                default:
                    result = pName;
                    break;
            }

            return result;
        }
        public static string UppercaseFirst(string s)
        {
            // Check for empty string.
            if (string.IsNullOrEmpty(s.Trim()))
            {
                return string.Empty;
            }
            // Return char and concat substring.
            return char.ToUpper(s[0]) + s.Substring(1);
        }
       
        public static decimal  GetGSTRate(decimal pAmount)
        {
//            Property daily rate up to Rs. 999 / -            :               No GST

//·         Property daily rate is Rs. 1000 & above, up to Rs. 2,499 / -                :               GST @ 12 % (SGST: 6 %, CGST @ 6 %)

//·         Property daily rate is between Rs. 2500 / -&Rs. 7,499 / -(both numbers included)             :               GST @ 18 % (SGST – 9 %,  CGST – 9 %).

//·         Property daily rate is Rs. 7,500 / -or more: GST @ 28 % (SGST – 14 %,  CGST – 14 %).

//·         Above are current rate slabs, which can change any time.These are applicable only on our ‘Buy’ and not our ‘Sell’. On ‘Sell’, above point(1) is applicable.

//·         We take input credit of GST based on above.Below criteria should be kept in mind:


            decimal result = 0;
           if(pAmount<=999)
            {
                result = 0;
            }
           else if((pAmount>=1000) && (pAmount <=2499))
            {
                result = 12;
            }
           else if ((pAmount >= 2500) && (pAmount <= 7499))
            {
                result = 18;
            }
           else if (pAmount >= 7500)
            {
                result = 28;
            }

            return result;
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
            switch (result.ToLower())
            {
                case "run of the house":
                    result = "Run of the house*";
                    break;
                case "room bed":
                    result = "Standard room*";
                    break;
                default:
                    result = result.Trim(',');
                    break;

            }
        //    result = result.ToLower().Contains("run of the house") ? "Run of the house" : result.Trim(',');
            return result;
        }

        public static string RoomTypeDescriptionFirst(string pValue)
        {
            string pFirst = pValue.Substring(0, 1);


            switch (pFirst)
            {

                case "A":
                    pValue = "Superior room";
                    break;
                case "B":
                    pValue = "Business room";
                    break;
                case "C":
                    pValue = "Concierge/Exective Suite";
                    break;
                case "D":
                    pValue = "Delux room";
                    break;
                case "E":
                    pValue = "Exective room";
                    break;
                case "F":
                    pValue = "Family room";
                    break;
                case "G":
                    pValue = "Comfort room";
                    break;
                case "H":
                    pValue = "Accessible room";
                    break;
                case "I":
                    pValue = "Budget room";
                    break;
                case "L":
                    pValue = "Studio";
                    break;
                case "J":
                case "M":
                case "T":
                    pValue = "Standard room";
                    break;
                case "N":
                    pValue = "Non smoking room";
                    break;
                case "K":
                case "U":
                    pValue = "Minimum";
                    break;
                case "S":
                    pValue = "Junior suite/Mini suite";
                    break;
                case "P":
                    pValue = "Executive";
                    break;
                case "R":
                    pValue = "Residential apartment";
                    break;
                case "V":
                    pValue = "Villa";
                    break;
                default:
                    pValue = "Room";
                    break;


            }

            return pValue;

        }
        public static string RoomTypeDescriptionSecond(string pValue)
        {
            string pSecond = pValue.Substring(1, 1);

            if (pSecond == "*")
            {
                if (pValue.Substring(2, 1) == "*")
                {
                    // pValue = "Number of beds and bed size can be increased on request*";
                    pValue = "";
                }
                else
                {
                    pValue = "with " + RoomTypeDescriptionThird(pValue.Substring(2, 1)) + "";// + " Number of beds can be increased on request*";
                }
            }
            else
            {
                if (pValue.Substring(2, 1) == "*")
                {
                    pValue = "with " + Utils.NumberToWordsWithFirstLower(Convert.ToInt32(pSecond.ToString())) + " bed";// + "Bed size can be increased on request*";
                }
                else
                {
                    switch (pSecond)
                    {
                        case "1":
                        case "2":
                        case "3":
                        case "4":
                        case "5":
                        case "6":
                        case "7":
                        case "8":
                        case "9":
                        case "0":
                            pValue = "with " + Utils.NumberToWordsWithFirstLower(Convert.ToInt32(pSecond)) + " " + RoomTypeDescriptionThird(pValue.Substring(2, 1));
                            break;
                        default:
                            pValue = RoomTypeDescriptionThird(pValue.Substring(2, 1));
                            break;


                    }

                }
            }


            return pValue;

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
                    pValue = "water bed";
                    break;
                case "P":
                    pValue = "Standard bed";
                    break;
                case "*":
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    pValue = "";
                    break;
                default:
                    pValue = "bed";
                    break;

            }

            return pValue;

        }
        //public static int GetMaxNoofPeople(string pValue)
        //{
        //    int result = 0;
        //    if(pValue.ToLower().Contains("single bed"))
        //    {

        //    }
        //    else if (pValue.ToLower().Contains("twin bed"))
        //    {

        //    }
        //    else if (pValue.ToLower().Contains("double bed"))
        //    {

        //    }
        //    else if (pValue.ToLower().Contains("single bed"))
        //    {

        //    }
        //    else if (pValue.ToLower().Contains("single bed"))
        //    {

        //    }
        //    else if (pValue.ToLower().Contains("single bed"))
        //    {

        //    }
        //    else if (pValue.ToLower().Contains("single bed"))
        //    {

        //    }
        //    return result;

        //}
        public static ulong WordsToNumbers(string words)
        {
            if (string.IsNullOrEmpty(words)) return 0;

            words = words.Trim();
            words += ' ';

            ulong number = 0;
            string[] singles = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            string[] teens = new string[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            string[] tens = new string[] { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninty" };
            string[] powers = new string[] { "", "thousand", "million", "billion", "trillion", "quadrillion", "quintillion" };

            for (int i = powers.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(powers[i]))
                {
                    int index = words.IndexOf(powers[i]);

                    if (index >= 0 && words[index + powers[i].Length] == ' ')
                    {
                        ulong count = WordsToNumbers(words.Substring(0, index));
                        number += count * (ulong)Math.Pow(1000, i);
                        words = words.Remove(0, index);
                    }
                }
            }

            {
                int index = words.IndexOf("hundred");

                if (index >= 0 && words[index + "hundred".Length] == ' ')
                {
                    ulong count = WordsToNumbers(words.Substring(0, index));
                    number += count * 100;
                    words = words.Remove(0, index);
                }
            }

            for (int i = tens.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(tens[i]))
                {
                    int index = words.IndexOf(tens[i]);

                    if (index >= 0 && words[index + tens[i].Length] == ' ')
                    {
                        number += (uint)(i * 10);
                        words = words.Remove(0, index);
                    }
                }
            }

            for (int i = teens.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(teens[i]))
                {
                    int index = words.IndexOf(teens[i]);

                    if (index >= 0 && words[index + teens[i].Length] == ' ')
                    {
                        number += (uint)(i + 10);
                        words = words.Remove(0, index);
                    }
                }
            }

            for (int i = singles.Length - 1; i >= 0; i--)
            {
                if (!string.IsNullOrEmpty(singles[i]))
                {
                    int index = words.IndexOf(singles[i] + ' ');

                    if (index >= 0 && words[index + singles[i].Length] == ' ')
                    {
                        number += (uint)(i);
                        words = words.Remove(0, index);
                    }
                }
            }

            return number;
        }
        public static  string GetHotelRateError(string pXML)
        {
            string ErrorCode = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            string a = string.Empty;
            try
            {
                xmlDoc.LoadXml(pXML);
                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XDocument doc = XDocument.Parse(xmlDoc.InnerXml.ToString());

                var RateErrors = from p in doc.Descendants()
                                 where p.Name.LocalName == "Error"
                                 select p;

                foreach (var p in RateErrors)
                {
                    Console.WriteLine(p);
                    ErrorCode = p.Attribute("Code").Value;
                }
            }
            catch (Exception ex)
            {
                ErrorCode = ex.Message;
            }
            return ErrorCode;
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
            catch (Exception ex) {
                if (ex.Message.Contains("404"))
                {
                    return false;
                }
              
            }
            return true;
     
        }

        public static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", " ");
        }
        public static string FirstLetterToUpperCase(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;

            return char.ToUpper(s[0]) + s.Substring(1);
        }
        public static string GetMoreIndicator(string pValue)
        {
            string result = string.Empty;
            try
            {
                if(!string.IsNullOrEmpty(pValue))
                {
                    result = pValue;
                }
                else
                {
                    result = "";
                }
           
            }
            catch (Exception ex)
            {
                result = "";
                throw ex;
            }
            return result;
        }
        public static string ReplaceFirstOccurrence(string Source, string Find, string Replace)
        {
            Source = Source.ToLower();
            Find = Find.ToLower();
            int Count = 0;
            string result = string.Empty;
            Count = CountStringOccurrences(Source, Find);
            int Place = Source.IndexOf(Find);
            result = (Count > 1) ? Source.Remove(Place, Find.Length).Insert(Place, Replace) : Source;
            if(result.StartsWith(","))
            {
                result = result.TrimStart(',').Trim();
            }
            return result;
        }
        public static int CountStringOccurrences(string text, string pattern)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(pattern, i)) != -1)
            {
                i += pattern.Length;
                count++;
            }
            return count;
        }
        private static string GetXMLNodeValue(XmlNode NodeItem, string pField)
        {
            string pValue = string.Empty;
            try
            {
                pValue = (NodeItem == null) ? "" : NodeItem.InnerText;
            }
            catch (Exception ex)
            {
                pValue = "";
            }
            return pValue;
        }
        private static int GetIntegerValue(XElement NodeItem, string pField)
        {
            int pValue = 0;
            try
            {
                pValue = (NodeItem.Attribute(pField).Name.LocalName != pField) ? 0 : Convert.ToInt32(NodeItem.Attribute(pField).Value);
            }
            catch (Exception ex)
            {
                pValue = 0;
            }
            return pValue;
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
        private static decimal GetDecimalValue(XElement NodeItem, string pField)
        {
            decimal pValue = 0;
            try
            {
                pValue = (NodeItem.Attribute(pField).Name.LocalName != pField) ? 0 : Convert.ToDecimal(NodeItem.Attribute(pField).Value);
            }
            catch (Exception ex)
            {
                pValue = 0;
            }
            return pValue;
        }
        public static string fnNumberwithoutzero(string Value)
        {
            object fnNumberwithoutzero = 0;

            string[] parts = Value.Split('.');
            if (parts.Length == 2)
            {
                object firsthalf = parts[0];
                object secondhalf = parts[1].ToString().TrimEnd("0".ToCharArray());

                if (secondhalf == "")
                    fnNumberwithoutzero = firsthalf;
                else
                    fnNumberwithoutzero = firsthalf + "." + secondhalf;
            }

            return fnNumberwithoutzero.ToString();
        }
        public static  List<CLayer.GDSCurrencyConversions> GetCurrencyConversions(string pXml)
        {
            List<CLayer.GDSCurrencyConversions> CurrencyConversionList = new List<CLayer.GDSCurrencyConversions>();
            XmlDocument xmlDoc = new XmlDocument();
            string a = string.Empty;
            try
            {
                //  xmlDoc.Load(@"F:\Hotels.xml");
                xmlDoc.LoadXml(pXml);
                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XDocument doc = XDocument.Parse(xmlDoc.InnerXml.ToString());

                var CurrencyConversionDetails = from p in doc.Descendants()
                             where p.Name.LocalName == "CurrencyConversion"
                             select p;

                foreach (var item in CurrencyConversionDetails)
                {
                    CLayer.GDSCurrencyConversions objCurrency = new CLayer.GDSCurrencyConversions();
                    objCurrency.RateConversion= GetDecimalValue(item, "RateConversion");
                    objCurrency.DecimalPlaces =Convert.ToInt32(GetStringValue(item, "DecimalPlaces"));
                    objCurrency.SourceCurrencyCode= GetStringValue(item, "SourceCurrencyCode");
                    objCurrency.RequestedCurrencyCode = GetStringValue(item, "RequestedCurrencyCode");
                    CurrencyConversionList.Add(objCurrency);
                }
               
            }
            catch (Exception ex)
            {
                CurrencyConversionList = null;
            }
            return CurrencyConversionList;
        }
        public static  int GetStarRating(string pXml)
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
            catch(Exception ex)
            {
                Common.LogHandler.AddLog(ex.Message);
            }
            return result;

        }
    }
}