using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Controllers
{
    public class TamarindHotelBookController : Controller
    {
        // GET: TamarindHotelBook

        public List<CLayer.RoomStaysResult> AmedusRoomStaysResultList = null;
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Book(Models.SimpleBookingModel data)
        {
            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(data.PropertyId);

            if (InventoryAPIType == 5 || InventoryAPIType == 4)
            {
                string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
                string HotelId = BLayer.Property.GetHotelId(data.PropertyId);
                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                srch.HotelCode = HotelId;
                srch.CheckIn = data.BookCheckIn;
                srch.CheckOut = data.BookCheckOut;

                string result = TamrindMultiSingleAvailability(srch);

                DataSet lds_auth = new DataSet();

                lds_auth.ReadXml(GenerateStreamFromString(result));
                long userId = GetUserId();
                decimal price = 0.0M;
                decimal amount = 0.0M;
                if (lds_auth.Tables[0].Rows[0][0].ToString() != "FAIL")
                {
                    Session["GDSRateConversion"] = "0";
                    Session["GDSCurrencyCode"] = "INR";

                    var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();
                    var RoomStayItemListAdvanced = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStays>();
                    List<CLayer.RoomStaysResult> RoomStaysResultList = null;
                    RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                    CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();

                    decimal TamrindAPIPercentage = 0.0M;
                    TamrindAPIPercentage = BLayer.Tamarind.APIPrice(userId);
                    if (TamrindAPIPercentage == 0)
                    {
                        TamrindAPIPercentage = BLayer.Tamarind.APIPriceAll();
                    }

                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(lds_auth.Tables["hotel"].Rows[0]["TotalPrice"]);
                    objRoomStay.BookingCode = "CB00A30";
                    objRoomStay.AmountAfterTax = (TamrindAPIPercentage == 0) ? amount = price : amount = (price / TamrindAPIPercentage) * 100;
                    objRoomStay.CurrencyCode = "INR";
                    objRoomStay.MinAmountAfterTax = objRoomStay.AmountAfterTax;

                    CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                    ObjRoomRateDescription.Name = "test";

                    objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;

                    objRoomStay.RoomTypeCode = "";
                    objRoomStay.RatePlanCode = "";

                    RoomStaysResultList.Add(objRoomStay);

                    AmedusRoomStaysResultList = RoomStaysResultList;
                }

                if (data.BookingData == null)
                {
                    data = new Models.SimpleBookingModel();
                    if (Request.Cookies.Get("BookingData").Value == "")
                    {
                        return RedirectToAction("Index", "Book");
                    }
                    string s = Request.Cookies.Get("BookingData").Value;
                    //  if(s!= "") s = HttpUtility.HtmlDecode
                    data.BookingData = HttpUtility.UrlDecode(s);
                    DateTime td = DateTime.Now;
                    s = Request.Cookies.Get("BookCheckIn").Value;
                    s = HttpUtility.UrlDecode(s);
                    DateTime.TryParse(s, out td);
                    data.BookCheckIn = td;
                    td = DateTime.Now.AddDays(7);
                    s = Request.Cookies.Get("BookCheckOut").Value;
                    s = HttpUtility.UrlDecode(s);
                    DateTime.TryParse(s, out td);
                    data.BookCheckOut = td;
                }

                if (data == null) return RedirectToAction("Index");
                BLayer.Bookings.ClearCart(GetUserId());
                RedirectToAction("BookAccommodation", "Boking", data);

            }
            return RedirectToAction("Index", "Booking");
        }
        public static string TamrindMultiSingleAvailability(CLayer.SearchCriteria sch)
        {
            TamarindService.TamarindDataServiceClient client = new TamarindService.TamarindDataServiceClient();

            client.ClientCredentials.UserName.UserName = "STAYBAZARXMLTEST";
            client.ClientCredentials.UserName.Password = "STAYBAZARXMLTEST";

            string CityId = BLayer.City.GetTamarindCityID(sch.Destination);

            //SearchController hotellist = new SearchController();
            //hotellist.TamarindHotelList_Save(CityId);

            var dateAndTime = sch.CheckIn;
            TamarindService.HotelSearchParam HotelParm = new TamarindService.HotelSearchParam();
            HotelParm.AgentMarket = "0";
            HotelParm.AllowOnrequest = Convert.ToBoolean("True");
            HotelParm.CityID = "BOM";
            HotelParm.CheckinDate = Convert.ToDateTime(dateAndTime);
            HotelParm.Nights = 1;
            HotelParm.SingleBed = 1;
            HotelParm.DoubleBed = 0;
            HotelParm.TwinBed = 0;
            HotelParm.ExtraBed = 0;
            HotelParm.OriginCountryID = "IN";
            HotelParm.HotelID = sch.HotelCode;
            HotelParm.Rating = "";
            string hotel = client.GetHotels(HotelParm);

            return hotel;
        }
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public long GetUserId()
        {
            long userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                long.TryParse(User.Identity.GetUserId(), out userId);
            }
            else
                if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] != null)
            {
                userId = (long)Session[CLayer.ObjectStatus.GUEST_ID_SESSION];
            }
            return userId;
        }
    }
}