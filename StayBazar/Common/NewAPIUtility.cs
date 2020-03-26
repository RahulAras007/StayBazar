using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Common
{
    public class NewAPIUtility
    {
        public static string GDSCountryCode = string.Empty;
        public static string GDSCurrencyCode = string.Empty;

        public static string SetHotelMultiSingleAvailabilityBody(CLayer.SearchCriteria sch)
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
            Body = Body + "<ibas:GetHotelList>";
            Body = Body + "<ibas:param>";
            Body = Body + "<tam:CityID>BOM</tam:CityID>";
            Body = Body + "<tam:StarRating></tam:StarRating>";
            Body = Body + "</ibas:param>";
            Body = Body + "</ibas:GetHotelList>";
            Body = Body + "</soapenv:Body>";

            return Body;
        }
        public static string SetSoapHeader()
        {
            string Header = string.Empty;
            Header = Header + "<soapenv:Header>";
            Header = Header + "<ibas:Security xmlns:ibas= \"http://www.tamarindtours.in//IBaseDetails\">";
            Header = Header + "<ibas:Username>StayBazarWBSUserName</ibas:Username>";
            Header = Header + "<ibas:Password>StayBazarWBSUserName</ibas:Password>";
            Header = Header + "</ibas:Security>";
            Header = Header + "</soapenv:Header>";

            return Header;
        }
    }
}