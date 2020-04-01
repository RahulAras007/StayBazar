using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Text;
using System.Threading.Tasks;
using StayBazar.Common;
using System.Xml;
using System.Net;
using System.IO;
using System.Data;
using System.Globalization;
using System.Collections;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class PropertyController : Controller
    {
        //
        // GET: /Property/

        #region Custom Methods
        public const int PictureCount = 8;

        private Models.SimpleSearchModel FixInputs(Models.SimpleSearchModel md)
        {
            int a;
            if (md == null) md = new Models.SimpleSearchModel();
            if (md.Adults == null || md.Adults == "")
                md.Adults = "1";
            else
            {
                a = 1;
                int.TryParse(md.Adults, out a);
                md.Adults = a.ToString();
                if (a < 1 || a > 8) md.Adults = "1";

            }
            if (md.Children == null || md.Children == "")
                md.Children = "0";
            else
            {
                a = 0;
                int.TryParse(md.Children, out a);
                md.Children = a.ToString();
                if (a < 0 || a > 8) md.Children = "0";

            }
            if (md.Bedrooms == null || md.Bedrooms == "")
                md.Bedrooms = "1";
            else
            {
                a = 0;
                int.TryParse(md.Bedrooms, out a);
                md.Bedrooms = a.ToString();
                if (a < 0 || a > 8) md.Bedrooms = "0";

            }
            if (md.StayType == null)
                md.StayType = "0";
            else
            {
                a = 0;
                int.TryParse(md.StayType, out a);
                md.StayType = a.ToString();
                if (a < 0 || a > 20000) md.StayType = "0";

            }

            //get userrole
            long userId = 0;
            long.TryParse(User.Identity.GetUserId(), out userId);
            CLayer.Role.Roles role = new CLayer.Role.Roles();
            if (User.Identity.IsAuthenticated)
            {
                long.TryParse(User.Identity.GetUserId(), out userId);
                role = BLayer.User.GetRole(userId);
            }

            if (role == CLayer.Role.Roles.Administrator)
            {

                if (md.CheckIn == null || md.CheckOut == null || md.CheckIn < DateTime.Today || md.CheckOut <= DateTime.Today)
                {
                    md.CheckIn = DateTime.Today.AddDays(7);
                    md.CheckOut = md.CheckIn.AddDays(1);
                }
            }

            else if (role == CLayer.Role.Roles.Corporate)
            {

                DateTime Credirbookingsmedayexp = BLayer.B2B.GetbookingsmedayforCorp(userId);

                if (Credirbookingsmedayexp < DateTime.Now)
                {
                    if (md.CheckIn == null || md.CheckOut == null || md.CheckIn <= DateTime.Today || md.CheckOut <= DateTime.Today)
                    {
                        md.CheckIn = DateTime.Today.AddDays(7);
                        md.CheckOut = md.CheckIn.AddDays(1);
                    }
                }
                else
                {
                    if (md.CheckIn == null || md.CheckOut == null || md.CheckIn < DateTime.Today || md.CheckOut <= DateTime.Today)
                    {
                        md.CheckIn = DateTime.Today.AddDays(7);
                        md.CheckOut = md.CheckIn.AddDays(1);
                    }
                }



            }
            else
            {
                if (md.CheckIn == null || md.CheckOut == null || md.CheckIn <= DateTime.Today || md.CheckOut <= DateTime.Today)
                {
                    md.CheckIn = DateTime.Today.AddDays(7);
                    md.CheckOut = md.CheckIn.AddDays(1);
                }
            }


            if (md.CheckOut <= md.CheckIn)
            {
                md.CheckOut = md.CheckIn.AddDays(1);
            }

            if (md.CheckIn >= DateTime.Today.AddMonths(10))
            {
                md.CheckIn = DateTime.Today.AddMonths(9).AddDays(25);
                md.CheckOut = md.CheckIn.AddDays(1);
            }
            if (md.beds < 0 || md.beds > 8) md.beds = 0;
            if (md.CurrentPage < 1) md.CurrentPage = 1;


            if (md.distanceInKm < 1 || md.distanceInKm > 20) md.distanceInKm = 0;

            if (md.HidAdults == null)
                md.HidAdults = md.Adults;
            else
            {
                a = 1;
                if (!int.TryParse(md.Adults, out a))
                    md.HidAdults = md.Adults;
                else
                    md.HidAdults = a.ToString();

                if (a < 1 || a > 8) md.HidAdults = md.Adults;
            }
            if (md.HidBedrooms == null || md.HidBedrooms == "")
                md.HidBedrooms = md.Bedrooms;
            else
            {
                a = 1;
                if (!int.TryParse(md.Adults, out a))
                    md.HidBedrooms = md.Bedrooms;
                else
                    md.HidBedrooms = a.ToString();

                if (a < 0 || a > 8) md.HidBedrooms = md.Bedrooms;
            }
            if (md.HidChildren == null || md.HidChildren == "")
                md.HidChildren = md.Children;
            else
            {
                a = 0;
                if (!int.TryParse(md.Adults, out a))
                    md.HidChildren = md.Children;
                else
                    md.HidChildren = a.ToString();

                if (a < 0 || a > 8) md.HidChildren = md.Children;
            }
            if (md.HiddenCheckIn == null || md.HiddenCheckIn <= DateTime.Today)
                md.HiddenCheckIn = md.CheckIn;
            if (md.HiddenCheckOut == null || md.HiddenCheckOut <= DateTime.Today)
                md.HiddenCheckOut = md.CheckOut;
            if (md.HiddenCheckOut <= md.HiddenCheckIn)
            {
                md.HiddenCheckOut = md.HiddenCheckIn.AddDays(1);
            }

            if (md.HiddenCheckIn >= DateTime.Today.AddMonths(10))
            {
                md.HiddenCheckIn = DateTime.Today.AddMonths(9).AddDays(25);
                md.HiddenCheckOut = md.CheckIn.AddDays(1);
            }

            if (md.HiddenDestination == null || md.HiddenDestination == "")
            {
                md.HiddenDestination = md.Destination;
            }
            if (md.HidStayType == null || md.HidStayType == "")
                md.HidStayType = md.StayType;
            else
            {
                a = 0;
                if (int.TryParse(md.HidStayType, out a))
                {
                    md.HidStayType = a.ToString();
                    if (a < 0 || a > 20000) md.HidStayType = "0";
                }
                else
                    md.HidStayType = md.StayType;


            }
            //text parameters
            //SecureInputString
            if (md.features == null) md.features = "";
            if (md.Destination == null) md.Destination = "";
            if (md.features != "") md.features = Common.Utils.SecureInputString(md.features);
            if (md.Destination != "") md.Destination = Common.Utils.SecureInputString(md.Destination);
            return md;
        }

        private async Task<Models.PropertyModel> GetAccommodationsForEmptyData(long propertyId)
        {

            Models.SimpleSearchModel bookingData = new Models.SimpleSearchModel();
            bookingData.PropertyId = propertyId;

            Models.PropertyModel property = new Models.PropertyModel();
            property.CameFromSearchPage = false;

            if (bookingData.PropertyId > 0)
            {
                long userId = 0;
                CLayer.Role.Roles role = CLayer.Role.Roles.Customer;
                if (User.Identity.IsAuthenticated)
                {
                    long.TryParse(User.Identity.GetUserId(), out userId);
                    role = BLayer.User.GetRole(userId);
                }

                bool isCorp = false;

                if (role == CLayer.Role.Roles.Corporate || role == CLayer.Role.Roles.CorporateUser) isCorp = true;


                CLayer.Property data = BLayer.Property.Get(bookingData.PropertyId);
                if (data != null)
                {
                    property = new Models.PropertyModel()
                    {
                        PropertyId = data.PropertyId,
                        Title = data.Title,
                        Description = data.Description,
                        Location = data.Location,
                        Address = data.Address,
                        Country = data.Country,
                        State = data.State,
                        City = data.City,
                        ZipCode = data.ZipCode,
                        Status = (int)data.Status,
                        PositionLat = data.PositionLat,
                        PositionLng = data.PositionLng,
                        Rating = data.Rating,
                        CityName = data.City,
                        StateName = data.Statename,
                        CountryName = data.Countryname
                    };
                    try
                    {
                        if (property.PositionLat == "0" || property.PositionLat == "")
                        {
                            string location = property.CityName + ", " + property.StateName;
                            if (property.ZipCode != "") location = location + " " + property.ZipCode;
                            location = location + ", " + property.CountryName;
                            Common.Utils.Location pos;

                            pos = await Common.Utils.GetLocation(location);
                            property.PositionLat = pos.Lattitude.ToString();
                            property.PositionLng = pos.Longitude.ToString();
                        }
                    }
                    catch (Exception ex)
                    { Common.LogHandler.HandleError(ex); }
                    if (bookingData.HiddenCheckIn == DateTime.MinValue)
                    {
                        bookingData.HiddenCheckIn = DateTime.Today.AddDays(7.0);
                    }
                    if (bookingData.HiddenCheckOut == DateTime.MinValue)
                    { bookingData.HiddenCheckOut = bookingData.HiddenCheckIn.AddDays(1); }
                    if (bookingData.HiddenCheckIn > bookingData.HiddenCheckOut)
                    {
                        DateTime t = bookingData.HiddenCheckIn;
                        bookingData.HiddenCheckIn = bookingData.HiddenCheckOut;
                        bookingData.HiddenCheckOut = t;
                    }
                    DateTime cin, cout;
                    cin = bookingData.HiddenCheckIn;
                    cout = bookingData.HiddenCheckOut;
                    if (Common.Utils.IsCrossedMaxDate(ref cin, ref cout))
                    {
                        bookingData.CheckIn = cin;
                        bookingData.CheckOut = cout;
                    }

                    CLayer.SearchCriteria sc = new CLayer.SearchCriteria();
                    int tx = 0;
                    int.TryParse(bookingData.HidAdults, out tx);
                    sc.Adults = tx;
                    tx = 0;
                    int.TryParse(bookingData.HidBedrooms, out tx);
                    sc.Bedrooms = tx;
                    sc.CheckIn = bookingData.HiddenCheckIn;
                    sc.CheckOut = bookingData.HiddenCheckOut;
                    tx = 0;
                    int.TryParse(bookingData.HidChildren, out tx);
                    sc.Children = tx;

                    sc.Features = bookingData.features;
                    sc.RangeBudgetMax = bookingData.rangeBudgetMax;
                    sc.RangeBudgetMin = bookingData.rangeBudgetMin;
                    sc.StayTypeGroup = "";
                    tx = 0;
                    int.TryParse(bookingData.StayType, out tx);
                    sc.StayType = tx;

                    property.Accommodations = new Models.PropertyAccommmodationModel()
                    {
                        Accommodations = BLayer.Property.GetDetailsForBooking(bookingData.PropertyId, sc)
                        //chosen accommodations
                    };




                    // Checking Hotelid,roomid available

                    int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(bookingData.PropertyId);

                    if (InventoryAPIType != 0)
                    {
                        //MAXIMOJO CHECKING 
                        if (bookingData.CheckIn == DateTime.MinValue)
                        {
                            bookingData.CheckIn = DateTime.Now.AddDays(7);
                        }
                        if (bookingData.CheckOut == DateTime.MinValue)
                        {
                            bookingData.CheckOut = bookingData.CheckIn.AddDays(1);
                        }
                        string HotelId = BLayer.Property.GetHotelId(bookingData.PropertyId);
                        string QueryKey = HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                        string BookingSessionId = "bs_" + HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                        string BookingRequestId = "br_" + HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                        List<long> NoinvetAcc = new List<long>();
                        if (property.Accommodations.Accommodations.Count > 0)
                        {
                            if (HotelId != null && HotelId != "")
                            {
                                HotelId = HotelId.Trim();

                                //booking available
                                List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> HotelRoomList = StayBazar.Inventory.MaximojoBook.BookingAvailDeserializeJsonResponse(HotelId, bookingData.CheckIn.ToString("yyyy-MM-dd"), bookingData.CheckOut.ToString("yyyy-MM-dd"), Convert.ToInt16(bookingData.Adults), Convert.ToInt16(bookingData.Children), Convert.ToInt16(bookingData.Bedrooms), QueryKey, BookingSessionId, BookingRequestId);

                                //rooms only checking here

                                foreach (CLayer.Accommodation acc in property.Accommodations.Accommodations)
                                {
                                    string RoomId = BLayer.Accommodation.GetRoomId(acc.AccommodationId);
                                    if (RoomId != null && RoomId != "")
                                    {
                                        RoomId = RoomId.Trim();
                                        if (!HotelRoomList.Exists(a => a.roomtype_id == RoomId))
                                        {
                                            NoinvetAcc.Add(acc.AccommodationId);
                                        }
                                    }
                                }
                            }
                        }
                        property.Accommodations.Accommodations.RemoveAll(ap => NoinvetAcc.Any(y => y == ap.AccommodationId));
                    }







                    StringBuilder strAccIds = new StringBuilder();
                    List<long> accIds = new List<long>();
                    foreach (CLayer.Accommodation acc in property.Accommodations.Accommodations)
                    {
                        if (!accIds.Contains(acc.AccommodationId))
                        {
                            accIds.Add(acc.AccommodationId);
                            strAccIds.Append(",");
                            strAccIds.Append(acc.AccommodationId);
                        }
                    }
                    if (strAccIds.Length > 0) strAccIds.Remove(0, 1);
                    //Get rates and availability
                    List<CLayer.Rates> rtes = new List<CLayer.Rates>();
                    if (accIds.Count > 0)
                    {
                        rtes = BLayer.Rate.GetTotalRates(accIds, bookingData.HiddenCheckIn, bookingData.HiddenCheckOut, ((isCorp) ? CLayer.Role.Roles.Corporate : CLayer.Role.Roles.Customer), userId, InventoryAPIType);
                    }
                    int cnt, idx;
                    cnt = property.Accommodations.Accommodations.Count();


                    //// change no of accommodation for external inventory
                    //List<CLayer.Rates> NewRates = new List<CLayer.Rates>();
                    //if (InventoryAPIType != 0)
                    //{
                    //    if (rtes.Count > 0)
                    //    {
                    //        string HotelId = BLayer.Property.GetHotelId(bookingData.PropertyId);
                    //        if (HotelId != null && HotelId != "")
                    //        {
                    //            foreach (CLayer.Rates item in rtes)
                    //            {
                    //                string RoomId = BLayer.Accommodation.GetRoomId(item.AccommodationId);
                    //                if (RoomId != null && RoomId != "")
                    //                {
                    //                    if (item.NoofAcc < 1)
                    //                    {
                    //                        item.NoofAcc = 10;
                    //                    }
                    //                }
                    //                NewRates.Add(item);
                    //            }
                    //        }
                    //    }
                    //}
                    //if (NewRates.Count == 0)
                    //{
                    //    NewRates = rtes;
                    //}


                    foreach (CLayer.Rates item in rtes)
                    {
                        for (idx = 0; idx < cnt; idx++)
                        {
                            if (property.Accommodations.Accommodations[idx].AccommodationId == item.AccommodationId)
                            {
                                property.Accommodations.Accommodations[idx].Rate = item.Amount;
                                property.Accommodations.Accommodations[idx].GuestRate = item.GuestRate;
                                property.Accommodations.Accommodations[idx].AccommodationCount = item.NoofAcc;
                            }
                        }
                    }
                    //accommodation conditions
                    List<CLayer.Condition> conds;

                    if (accIds.Count == 0)
                        conds = new List<CLayer.Condition>();
                    else
                        conds = BLayer.Condition.GetAllForAccommodations(strAccIds.ToString());

                    property.AccConditions = conds;
                    //load property pictures
                    property.Pictures = new Models.PropertyPicturesModel() { Pictures = BLayer.PropertyFiles.GetAll(data.PropertyId) };
                    property.Landmarks = new Models.PropertyLandmarkModel()
                    {
                        Landmarks = BLayer.Landmarks.GetOnProperty(data.PropertyId)
                    };

                    property.Filter.HiddenCheckIn = bookingData.HiddenCheckIn;
                    property.Filter.HiddenCheckOut = bookingData.HiddenCheckOut;
                    property.Filter.Adults = bookingData.HidAdults;
                    property.Filter.Children = bookingData.HidChildren;
                    property.Filter.PropertyId = bookingData.PropertyId;
                    property.Filter.rangeBudgetMax = bookingData.rangeBudgetMax;
                    property.Filter.rangeBudgetMin = bookingData.rangeBudgetMin;
                    property.Filter.starRatingRange = bookingData.starRatingRange;
                    property.Filter.HidStayType = bookingData.HidStayType;
                    property.Filter.HiddenDestination = bookingData.HiddenDestination;
                    property.Filter.Location = bookingData.Location;
                    property.Filter.features = bookingData.features;
                    property.Filter.Bedrooms = bookingData.HidBedrooms;

                    property.Filter.Destination = bookingData.HiddenDestination;
                    property.Filter.StayType = bookingData.HidStayType;
                    property.Filter.HidAdults = bookingData.HidAdults;
                    property.Filter.HidBedrooms = bookingData.HidBedrooms;
                    property.Filter.HidChildren = bookingData.HidChildren;
                    property.Filter.CheckIn = bookingData.HiddenCheckIn;
                    property.Filter.CheckOut = bookingData.HiddenCheckOut;
                    property.Filter.PropertyId = bookingData.PropertyId;

                }

            }
            return property;
        }

        private async Task<Models.PropertyModel> GetAccommodationsByFilter(Models.SimpleSearchModel bookingData)
        {

            Models.PropertyModel property = new Models.PropertyModel();
            List<CLayer.RoomStaysResult> RoomStaysResultList = null;
            try
            {
                if (bookingData.PropertyId > 0)
                {
                    long userId = 0;
                    CLayer.Role.Roles role = CLayer.Role.Roles.Customer;

                    if (User.Identity.IsAuthenticated)
                    {
                        long.TryParse(User.Identity.GetUserId(), out userId);
                        if (userId > 0)
                        {
                            role = BLayer.User.GetRole(userId);
                        }
                    }
                    //string email = User.Identity.GetUserName();
                    // = (CLayer.Role.Roles)BLayer.User.GetRole(email);


                    bool isCorp = false;

                    if (role == CLayer.Role.Roles.Corporate || role == CLayer.Role.Roles.CorporateUser) isCorp = true;

                    CLayer.Property data = BLayer.Property.Get(bookingData.PropertyId);


                    if (data != null)
                    {
                        property = new Models.PropertyModel()
                        {
                            PropertyId = data.PropertyId,
                            Title = data.Title,
                            Description = data.Description,
                            Location = data.Location,
                            Address = data.Address,
                            Country = data.Country,
                            State = data.State,
                            City = data.City,
                            ZipCode = data.ZipCode,
                            Status = (int)data.Status,
                            PositionLat = data.PositionLat,
                            PositionLng = data.PositionLng,
                            Rating = data.Rating,
                            CityName = data.City,
                            StateName = data.Statename,
                            CountryName = data.Countryname
                        };
                        try
                        {
                            if (property.PositionLat == "0" || property.PositionLat == "")
                            {
                                string location = property.CityName + ", " + property.StateName;
                                if (property.ZipCode != "") location = location + " " + property.ZipCode;
                                location = location + ", " + property.CountryName;
                                Common.Utils.Location pos;

                                pos = await Common.Utils.GetLocation(location);
                                property.PositionLat = pos.Lattitude.ToString();
                                property.PositionLng = pos.Longitude.ToString();
                            }
                        }
                        catch (Exception ex)
                        { Common.LogHandler.HandleError(ex); }
                        if (bookingData.HiddenCheckIn == DateTime.MinValue)
                        {
                            bookingData.HiddenCheckIn = DateTime.Today;
                        }
                        if (bookingData.HiddenCheckOut == DateTime.MinValue)
                        { bookingData.HiddenCheckOut = bookingData.HiddenCheckIn.AddDays(1); }
                        if (bookingData.HiddenCheckIn > bookingData.HiddenCheckOut)
                        {
                            DateTime t = bookingData.HiddenCheckIn;
                            bookingData.HiddenCheckIn = bookingData.HiddenCheckOut;
                            bookingData.HiddenCheckOut = t;
                        }

                        CLayer.SearchCriteria sc = new CLayer.SearchCriteria();
                        int tx = 0;
                        int.TryParse(bookingData.HidAdults, out tx);
                        sc.Adults = tx;
                        tx = 0;
                        // int.TryParse(bookingData.HidBedrooms, out tx);
                        //  sc.Bedrooms = tx;
                        sc.Bedrooms = bookingData.beds;
                        sc.CheckIn = bookingData.HiddenCheckIn;
                        sc.CheckOut = bookingData.HiddenCheckOut;
                        tx = 0;
                        int.TryParse(bookingData.HidChildren, out tx);
                        sc.Children = tx;

                        sc.Features = bookingData.features;
                        sc.RangeBudgetMax = bookingData.rangeBudgetMax;
                        sc.RangeBudgetMin = bookingData.rangeBudgetMin;
                        sc.StayTypeGroup = "";
                        tx = 0;
                        int.TryParse(bookingData.StayType, out tx);
                        sc.StayType = tx;
                        int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(bookingData.PropertyId);
                        Session["InventoryAPIType"] = InventoryAPIType;
                        if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                        {
                            property.Accommodations = new Models.PropertyAccommmodationModel()
                            {
                                Accommodations = BLayer.Property.GetDetailsForBooking(bookingData.PropertyId, sc)

                            };
                        }
                        else
                        {
                            property.Accommodations = new Models.PropertyAccommmodationModel()
                            {
                                Accommodations = BLayer.Property.GetDetailsForBooking(bookingData.PropertyId, sc)
                                //chosen accommodations
                            };
                        }



                        //check inventory api 

                        if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Maxmojo)
                        {
                            // Checking Hotelid,roomid available

                            string HotelId = BLayer.Property.GetHotelId(bookingData.PropertyId);

                            List<long> NoinvetAcc = new List<long>();

                            if (property.Accommodations.Accommodations.Count > 0)
                            {
                                if (HotelId != null && HotelId != "")
                                {
                                    HotelId = HotelId.Trim();
                                    //booking available
                                    List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> HotelRoomList = StayBazar.Inventory.MaximojoBook.BookingAvailDeserializeJsonResponse(HotelId, bookingData.CheckIn.ToString("yyyy-MM-dd"), bookingData.CheckOut.ToString("yyyy-MM-dd"), Convert.ToInt16(bookingData.Adults), Convert.ToInt16(bookingData.Children), Convert.ToInt16(bookingData.Bedrooms), "3cc343af545749818805dd199a914dee_IN273314_270320151728_1_1", "545749818805dd199a914dee3cc343af", "CMID-9818805dd199a914defe3cc199a");
                                    //rooms only checking here

                                    foreach (CLayer.Accommodation acc in property.Accommodations.Accommodations)
                                    {
                                        string RoomId = BLayer.Accommodation.GetRoomId(acc.AccommodationId);

                                        if (RoomId != null && RoomId != "")
                                        {
                                            RoomId = RoomId.Trim();
                                            if (!HotelRoomList.Exists(a => a.roomtype_id == RoomId))
                                            {
                                                NoinvetAcc.Add(acc.AccommodationId);
                                            }
                                        }
                                    }
                                }
                            }

                            property.Accommodations.Accommodations.RemoveAll(ap => NoinvetAcc.Any(y => y == ap.AccommodationId));
                        }
                        else if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                        {
                            // Checking Hotelid,roomid available

                            string HotelId = BLayer.Property.GetHotelId(bookingData.PropertyId);


                            //Get accommodations amedus

                            CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                            srch.HotelCode = HotelId;
                            srch.CheckIn = bookingData.CheckIn;
                            srch.CheckOut = bookingData.CheckOut;

                            string result = SearchController.HotelMultiSingleAvailability(srch, false, HotelId);

                            Serializer ser = new Serializer();
                            CLayer.HotelAvailability.Envelope HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);

                            string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION);



                            long AmadeusPropertyID = 0;
                            string CityName = string.Empty;
                            var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                            var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();
                            Session["SessionId"] = HotelResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelResult.Header.Session.SecurityToken;
                            Session["HotelCode"] = HotelId;

                            string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                            APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                            RoomStaysResultList = null;
                            foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelItem = item;

                                string RoomStayRPH = item.RoomStayRPH;
                                RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                if (!string.IsNullOrEmpty(RoomStayRPH))
                                {

                                    string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                    RoomStaysResultList = SearchController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList);
                                }
                            }

                            ViewBag.AmedusRoomsList = RoomStaysResultList.Count;

                        }






                        StringBuilder strAccIds = new StringBuilder();
                        List<long> accIds = new List<long>();
                        foreach (CLayer.Accommodation acc in property.Accommodations.Accommodations)
                        {
                            if (!accIds.Contains(acc.AccommodationId))
                            {
                                accIds.Add(acc.AccommodationId);
                                strAccIds.Append(",");
                                strAccIds.Append(acc.AccommodationId);
                            }
                        }
                        if (strAccIds.Length > 0) strAccIds.Remove(0, 1);
                        //Get rates and availability
                        List<CLayer.Rates> rtes;
                        if (accIds.Count == 0)
                            rtes = new List<CLayer.Rates>();
                        else
                            rtes = BLayer.Rate.GetTotalRates(accIds, bookingData.HiddenCheckIn, bookingData.HiddenCheckOut, ((isCorp) ? CLayer.Role.Roles.Corporate : CLayer.Role.Roles.Customer), userId, InventoryAPIType);
                        int cnt, idx;
                        cnt = property.Accommodations.Accommodations.Count();
                        //ViewBag.AmedusRoomsList = RoomStaysResultList.Count;
                        if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                        {
                            rtes = new List<CLayer.Rates>();
                            CLayer.Rates AmadeusRates = new CLayer.Rates();

                            //   AmadeusRates.GuestRate = 100;
                            AmadeusRates.NoofAcc = 10;
                            //    AmadeusRates.Amount = RoomStaysResultList.Min().AmountAfterTax;
                            AmadeusRates.Amount = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().AmountAfterTax;
                            AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);

                            //   AmadeusRates.TotalRateTax= (RoomStaysResultList.Min().AmountAfterTax)-(RoomStaysResultList.Min().AmountBeforeTax);
                            foreach (var accom in BLayer.Accommodation.GetAllAccByPropertyid(bookingData.PropertyId))
                            {
                                foreach (var item in RoomStaysResultList.Where(x => x.BookingCode == accom.BookingCode))
                                {
                                    item.AmountAfterTax = (Convert.ToString(Session["GDSCountry"]) == "") ? item.AmountAfterTax : APIUtility.GetGDSConvertedAmount(item.AmountAfterTax);
                                    if (AmadeusRates.Amount == item.AmountAfterTax)
                                    {
                                        AmadeusRates.AccommodationId = accom.AccommodationId;
                                        break;
                                    }
                                }
                            }
                            rtes.Add(AmadeusRates);
                        }

                        //// change no of accommodation for external inventory
                        //List<CLayer.Rates> NewRates = new List<CLayer.Rates>();
                        //if (InventoryAPIType != 0)
                        //{
                        //    if (rtes.Count > 0)
                        //    {
                        //        string HotelId = BLayer.Property.GetHotelId(bookingData.PropertyId);
                        //        if (HotelId != null && HotelId != "")
                        //        {
                        //            foreach (CLayer.Rates item in rtes)
                        //            {
                        //                string RoomId = BLayer.Accommodation.GetRoomId(item.AccommodationId);
                        //                if (RoomId != null && RoomId != "")
                        //                {
                        //                    if (item.NoofAcc < 1)
                        //                    {
                        //                        item.NoofAcc = 10;
                        //                    }
                        //                }
                        //                NewRates.Add(item);
                        //            }
                        //        }
                        //    }
                        //}
                        //if (NewRates.Count == 0)
                        //{
                        //    NewRates = rtes;
                        //}




                        foreach (CLayer.Rates item in rtes)
                        {
                            for (idx = 0; idx < cnt; idx++)
                            {
                                if (property.Accommodations.Accommodations[idx].AccommodationId == item.AccommodationId)
                                {
                                    property.Accommodations.Accommodations[idx].Rate = item.Amount;
                                    property.Accommodations.Accommodations[idx].GuestRate = item.GuestRate;
                                    property.Accommodations.Accommodations[idx].AccommodationCount = item.NoofAcc;
                                    if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                                    {
                                        property.Accommodations.Accommodations[idx].MaxNoOfPeople = 1;
                                    }


                                }
                            }
                        }
                        //accommodation conditions
                        List<CLayer.Condition> conds;
                        if (accIds.Count == 0)
                            conds = new List<CLayer.Condition>();
                        else
                            conds = BLayer.Condition.GetAllForAccommodations(strAccIds.ToString());
                        property.AccConditions = conds;
                        //load property pictures
                        property.Pictures = new Models.PropertyPicturesModel() { Pictures = BLayer.PropertyFiles.GetAll(data.PropertyId) };
                        property.Landmarks = new Models.PropertyLandmarkModel()
                        {
                            Landmarks = BLayer.Landmarks.GetOnProperty(data.PropertyId)
                        };
                        if (Request.HttpMethod == "GET")
                        {
                            property.Filter.HiddenCheckIn = bookingData.CheckIn;
                            property.Filter.HiddenCheckOut = bookingData.CheckOut;
                            property.Filter.Adults = bookingData.Adults;
                            property.Filter.Children = bookingData.Children;
                            property.Filter.PropertyId = bookingData.PropertyId;
                            property.Filter.rangeBudgetMax = bookingData.rangeBudgetMax;
                            property.Filter.rangeBudgetMin = bookingData.rangeBudgetMin;
                            property.Filter.starRatingRange = bookingData.starRatingRange;
                            property.Filter.HidStayType = bookingData.StayType;
                            property.Filter.HiddenDestination = bookingData.Destination;
                            property.Filter.Location = bookingData.Location;
                            property.Filter.features = bookingData.features;
                            property.Filter.Bedrooms = bookingData.Bedrooms;
                            property.Filter.distanceInKm = bookingData.distanceInKm;
                            property.Filter.Destination = bookingData.Destination;
                            property.Filter.StayType = bookingData.StayType;
                            property.Filter.HidAdults = bookingData.Adults;
                            property.Filter.HidBedrooms = bookingData.Bedrooms;
                            property.Filter.HidChildren = bookingData.Children;
                            property.Filter.CheckIn = bookingData.CheckIn;
                            property.Filter.CheckOut = bookingData.CheckOut;
                            property.Filter.PropertyId = bookingData.PropertyId;
                        }
                        else
                        {
                            property.Filter.HiddenCheckIn = bookingData.HiddenCheckIn;
                            property.Filter.HiddenCheckOut = bookingData.HiddenCheckOut;
                            property.Filter.Adults = bookingData.HidAdults;
                            property.Filter.Children = bookingData.HidChildren;
                            property.Filter.PropertyId = bookingData.PropertyId;
                            property.Filter.rangeBudgetMax = bookingData.rangeBudgetMax;
                            property.Filter.rangeBudgetMin = bookingData.rangeBudgetMin;
                            property.Filter.starRatingRange = bookingData.starRatingRange;
                            property.Filter.HidStayType = bookingData.HidStayType;
                            property.Filter.HiddenDestination = bookingData.HiddenDestination;
                            property.Filter.Location = bookingData.Location;
                            property.Filter.features = bookingData.features;
                            property.Filter.Bedrooms = bookingData.HidBedrooms;

                            property.Filter.Destination = bookingData.HiddenDestination;
                            property.Filter.StayType = bookingData.HidStayType;
                            property.Filter.HidAdults = bookingData.HidAdults;
                            property.Filter.HidBedrooms = bookingData.HidBedrooms;
                            property.Filter.HidChildren = bookingData.HidChildren;
                            property.Filter.CheckIn = bookingData.HiddenCheckIn;
                            property.Filter.CheckOut = bookingData.HiddenCheckOut;
                            property.Filter.PropertyId = bookingData.PropertyId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                property = null;
            }

            return property;
        }

        public List<CLayer.Rates> GetAmedusRates(Models.SimpleSearchModel bookingData, List<CLayer.RoomStaysResult> RoomStaysResultList, string HotelCode)
        {
            List<CLayer.Rates> rtes = new List<CLayer.Rates>();
            CLayer.Rates AmadeusRates = new CLayer.Rates();
            string AgeQualifyingCode = "10";
            int GuestCount = Convert.ToInt32(Session["Adults"]) + Convert.ToInt32(Session["Children"]); ;



            try
            {
                //Pricing details from enhanced pricing details API
                string SecurityToken = Convert.ToString(Session["SecurityToken"]);
                string SequenceNumber = Convert.ToString(Session["SequenceNumber"]);
                string SessionId = Convert.ToString(Session["SessionId"]);

                AmadeusRates.NoofAcc = 10;




                foreach (var accom in BLayer.Accommodation.GetAllAccByPropertyid(bookingData.PropertyId))
                {
                    foreach (var item in RoomStaysResultList.OrderBy(x => x.AmountAfterTax).Take(1).ToList())
                    {
                        string pricingresult = GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, item.RoomTypeCode, HotelCode, item.RatePlanCode, item.BookingCode, bookingData.CheckIn.ToString("yyyy-MM-dd"), bookingData.CheckOut.ToString("yyyy-MM-dd"), "", AgeQualifyingCode, GuestCount.ToString(), bookingData.PropertyId);
                        AmadeusRates.Amount = item.AmountAfterTax;
                        AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);

                    }
                    foreach (var item in RoomStaysResultList.Where(x => x.BookingCode == accom.BookingCode))
                    {
                        item.AmountAfterTax = (Convert.ToString(Session["GDSCountry"]) == "") ? item.AmountAfterTax : APIUtility.GetGDSConvertedAmount(item.AmountAfterTax);
                        if (AmadeusRates.Amount == item.AmountAfterTax)
                        {
                            AmadeusRates.AccommodationId = accom.AccommodationId;
                            break;
                        }
                    }
                    rtes.Add(AmadeusRates);
                }
            }
            catch (Exception ex)
            {
                rtes = null;
            }
            return rtes;
        }


        #endregion
        private const int GDSTHUMBNAIL_COUNT = 100;
        private const string DEFAULT_ADULTS_COUNT = "1";
        private const string DEFAULT_CHILDREN_COUNT = "0";
        private const string DEFAULT_BEDROOMS_COUNT = "1";

        public async Task<ActionResult> Index(long id)
        {

            int Inventorytype = BLayer.Property.GetPropertyInventoryAPIType(id);
            Models.SimpleSearchModel data = (Models.SimpleSearchModel)TempData["SearchCriteria"];
            TempData.Keep("SearchCriteria");
            string starts = "";
            string ends = "";

            if (data != null)
            {
                starts = Convert.ToString(data.CheckIn);
                ends = Convert.ToString(data.CheckOut);
                ViewBag.Destination = Convert.ToString(data.Destination);
                ViewBag.CheckIn = Convert.ToString(data.CheckIn.ToString("dd/MM/yyyy"));
                ViewBag.CheckOut = Convert.ToString(data.CheckOut.ToString("dd/MM/yyyy"));
                ViewBag.Adults = Convert.ToString(data.Adults);
                ViewBag.Bedrooms = Convert.ToString(data.Bedrooms);
                ViewBag.StayType = Convert.ToString(data.StayType);
                ViewBag.Children = Convert.ToString(data.Children);
            }

            if (Inventorytype == 2)
            {
                if (!string.IsNullOrEmpty(Request["start"]) && (!string.IsNullOrEmpty(Request["end"])))
                {

                    DateTime dStarts = DateTime.Parse(Request["start"]);
                    DateTime dEnds = DateTime.Parse(Request["end"]);


                    CLayer.Property rcData = BLayer.Property.PropertyForAutoComplete(id, dStarts, dEnds);


                    if (rcData != null)
                    {
                        starts = rcData.StartDate.ToString("yyyy-MM-dd");
                        ends = rcData.EndDate.ToString("yyyy-MM-dd");
                        return (await AmadeusPropertyDetails(rcData, starts, ends));
                    }

                }
                else if ((starts == "") && (ends == "") || (Request["propertyid"] == null))
                {
                    CLayer.Property rcData = BLayer.Property.PropertyForMostPopularGDS(id);
                    if (rcData != null)
                    {
                        starts = rcData.StartDate.ToString("yyyy-MM-dd");
                        ends = rcData.EndDate.ToString("yyyy-MM-dd");
                        return (await AmadeusPropertyDetails(rcData, starts, ends));
                    }
                }
                else
                {
                    return (await AmadeusPropertyDetails(id, starts, ends));
                }
            }

            Models.PropertyModel property = new Models.PropertyModel();
            try
            {


                property = await GetAccommodationsForEmptyData(id);
                if (!BLayer.Property.IsValid(id))
                {
                    return RedirectToAction("Index", "Home");
                }
                property.PropertyId = id;
                property.CameFromSearchPage = false;
                if (User.Identity.IsAuthenticated)
                {
                    long uid = 0;
                    long.TryParse(User.Identity.GetUserId(), out uid);
                    if (uid > 0)
                    {
                        property.UserType = BLayer.User.GetRole(uid);
                    }
                }

            }
            catch (Exception ex)
            { Common.LogHandler.HandleError(ex); }
            return View(property);
        }

        public ActionResult BProperty(int? id)
        {
            try
            {
                if (id.HasValue)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        return RedirectToAction("Index", new { id = id.Value });
                    }
                    else
                    {
                        return RedirectToAction("BusinessLogin", "Account", new { ReturnUrl = "/Property/Index/" + id.Value.ToString(), type = "1" });
                    }
                }
                else
                {
                    return RedirectToAction("Home", "Account");
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("Index", new { id = 0 });
        }

        /*     public async Task<ActionResult> Show(long propertyid, string destination, int adults, int children, int bedrooms, int category, DateTime checkIn, DateTime checkOut)
             {
                 Models.SimpleSearchModel bookingData;
                 bookingData = new Models.SimpleSearchModel();

                 Models.PropertyModel property = new Models.PropertyModel();
                 try
                 {
                     if (adults < 1 || adults > 8) adults = 1;
                     if (children < 1 || children > 8) children = 1;
                     if (bedrooms < 1 || bedrooms > 8) bedrooms = 1;
                     if (category < 0 || category > 10000) category = 0;
                     if (destination == null) destination = "";
                     bookingData.Destination = destination;
                     bookingData.PropertyId = propertyid;
                     bookingData.Adults = adults.ToString();
                     bookingData.Children = children.ToString();
                     bookingData.HidBedrooms = bedrooms.ToString();
                     bookingData.Bedrooms = bookingData.HidBedrooms;
                     bookingData.StayType = category.ToString();
                     if (checkIn == null) checkIn = DateTime.MinValue;
                     if (checkOut == null) checkOut = DateTime.MinValue;

                     bookingData.CheckIn = checkIn;
                     bookingData.CheckOut = checkOut;

                     if (bookingData.CheckIn >= DateTime.Today.AddMonths(10))
                     {
                         bookingData.CheckIn = DateTime.Today.AddMonths(9).AddDays(25);
                         bookingData.CheckOut = bookingData.CheckIn.AddDays(1);
                     }
                     bookingData.HiddenCheckIn = bookingData.CheckIn;
                     bookingData.HiddenCheckOut = bookingData.CheckOut;


                     property = await GetAccommodationsByFilter(bookingData);
                     if (User.Identity.IsAuthenticated)
                     {
                         long uid = 0;
                         long.TryParse(User.Identity.GetUserId(), out uid);
                         if (uid > 0)
                         {
                             property.UserType = BLayer.User.GetRole(uid);
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     Common.LogHandler.HandleError(ex);
                 }
                 return View("Index", property);
             }*/
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
        public static string TBOHotelRoomRequest(CLayer.SearchCriteria room)
        {
            string tokenid = BLayer.TBO.TokenID();
            CLayer.TBOHotelRoom param1 = new CLayer.TBOHotelRoom
            {
                EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
                TokenId = tokenid,
                TraceId = room.TraceId,
                ResultIndex = room.ResultIndex,
                HotelCode = room.HotelCode
            };

            string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(param1);
            string authurl = "http://api.tektravels.com/BookingEngineService_Hotel/hotelservice.svc/rest/GetHotelRoom";

            string responsedata = BLayer.TBO.GetResponse(requestdata, authurl);
            return responsedata;
        }
        public async Task<ActionResult> Property(Models.SearchParamModel data)
        {
            Session["ReturnUrl"] = Request.Url.AbsoluteUri;
            Models.PropertyModel property = new Models.PropertyModel();
            ViewBag.Adults = data.Adults;
            string Tamarind_flag = "N";
            if (!string.IsNullOrEmpty(data.Destination))
            {
                ViewBag.Destination = data.Destination.ToLower();
            }
            long ldt_property = 0;
            Models.PropertyModel objData;
            objData = new Models.PropertyModel();
            if (data.InventoryAPIType == 4)
            {

                objData.Title = data.title;
                objData.CityId = data.CityId;
                objData.State = data.StateId;
                objData.HotelId = data.HotelID;
                objData.InventoryAPIType = Convert.ToInt32(data.InventoryAPIType);
                objData.TamarindFlag = "Y";
                objData.RateCardDetailedId = data.RateCardDetailedId;
                objData.OwnerId = GetUserId();
                objData.TaxPercentage = data.TaxPercentage;
                objData.GSTSlab = data.GSTSlab;
                ldt_property = await SaveAmadeus_Property(objData);
                Models.AccommodationModel acc;
                acc = new Models.AccommodationModel();
                acc.AccommodationTypeId = data.AccommodationTpeID;
                acc.StayCategoryId = data.StayCategoryID;
                acc.PropertyId = ldt_property;
                acc.CheckIn = Convert.ToDateTime(data.CheckIn);
                acc.CheckOut = Convert.ToDateTime(data.CheckOut);
                acc.Amount = data.price;
                acc.AmountWithTax = data.Amount;
                AccommodationSaveByAPI(acc);
            }
            else if (data.InventoryAPIType == 5)
            {
                objData.Title = data.title;
                objData.CityId = data.CityId;
                objData.State = data.StateId;
                objData.HotelId = data.HotelID;
                objData.TBOHotelId = data.HotelID;
                objData.InventoryAPIType = 5;
                objData.TBOFlag = "Y";
                objData.OwnerId = GetUserId();
                objData.TaxPercentage = data.TaxPercentage;
                objData.GSTSlab = data.GSTSlab;
                ldt_property = await SaveAmadeus_Property(objData);
                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();

                srch.TraceId = data.TraceID;
                srch.ResultIndex = data.ResultIndex;
                srch.HotelCode = data.HotelID;
                string TBORoomResult = TBOHotelRoomRequest(srch);
                DataSet lds_tboroom = new DataSet();
                lds_tboroom.ReadXml(GenerateStreamFromString(TBORoomResult));
                if (int.Parse(lds_tboroom.Tables["Error"].Rows[0]["ErrorCode"].ToString()) == 0)
                {
                    for (int j = 0; j < 1; j++)
                    {
                       decimal price = Convert.ToDecimal((lds_tboroom.Tables[4].Rows[j]["OfferedPriceRoundedOff"]).ToString());
                       decimal ld_apigst = Convert.ToDecimal((lds_tboroom.Tables[4].Rows[j]["TotalGSTAmount"]).ToString());
                       decimal Tax = (10 * (price + ld_apigst)) / 100;
                       decimal amount = price + ld_apigst + Tax;
                       int ldt_catid = BLayer.AccommodationType.TBOAccommodationTypeSave(lds_tboroom.Tables[2].Rows[j]["RoomTypeName"].ToString());
                        Models.AccommodationModel acc;
                        acc = new Models.AccommodationModel();
                        acc.AccommodationTypeId = ldt_catid;
                        acc.StayCategoryId = 39;//default set as hotel 
                        acc.PropertyId = ldt_property;
                        acc.Description = data.Description;
                        acc.CheckIn = Convert.ToDateTime(data.CheckIn);
                        acc.CheckOut = Convert.ToDateTime(data.CheckOut);
                        acc.Amount = price;
                        acc.AmountWithTax = amount;
                        AccommodationSaveByAPI(acc);

                    }
                }
               
            }


            ViewBag.Bedrooms = data.Bedrooms;
            ViewBag.CheckIn = data.CheckIn;
            ViewBag.CheckOut = data.CheckOut;
            ViewBag.Children = data.Children;
            ViewBag.StayType = data.StayType;

            Session["Adults"] = data.Adults;
            Session["Bedrooms"] = data.Bedrooms;
            Session["Children"] = data.Children;

            //System.Web.HttpContext.Current.Session["Adults"] = data.Adults;
            //System.Web.HttpContext.Current.Session["Children"] = data.Children;
            //System.Web.HttpContext.Current.Session["Bedrooms"] = data.Bedrooms;


            try
            {


                int Inventorytype = BLayer.Property.GetPropertyInventoryAPIType(data.PropertyId);
                int TamarindInventoryType = BLayer.Property.GetTamarindInventoryAPITypeId(data.PropertyId);
                int TBOInventoryType = BLayer.Property.GetTBOInventoryAPITypeId(data.PropertyId);

                if (Inventorytype == 2)
                {
                    DateTime starts = DateTime.Parse(data.CheckIn);
                    DateTime ends = DateTime.Parse(data.CheckOut);

                    var firstdate = starts.ToString("yyyy-MM-dd");
                    var lastdate = ends.ToString("yyyy-MM-dd");

                    return (await AmadeusPropertyDetails(data.PropertyId, firstdate, lastdate, data));
                }
                if (Inventorytype == 4 || TamarindInventoryType == 4)
                {
                    Tamarind_flag = BLayer.Property.GetPropertyTamarindFlag(data.PropertyId, data.title);

                    DateTime starts = DateTime.Parse(data.CheckIn);
                    DateTime ends = DateTime.Parse(data.CheckOut);

                    var firstdate = starts.ToString("yyyy-MM-dd");
                    var lastdate = ends.ToString("yyyy-MM-dd");

                    long PropertyId = ldt_property;
                    //long PropertyId = BLayer.Property.GetPropertyIdFromTamarindId(data.PropertyId);
                    data.PropertyId = PropertyId;
                    //return (await TamrindPropertyDetails(PropertyId, firstdate, lastdate, data));

                }
                if (Inventorytype == 5 || TBOInventoryType == 5)
                {
                    DateTime starts = DateTime.Parse(data.CheckIn);
                    DateTime ends = DateTime.Parse(data.CheckOut);

                    var firstdate = starts.ToString("yyyy-MM-dd");
                    var lastdate = ends.ToString("yyyy-MM-dd");

                    long PropertyId = ldt_property;
                    //long PropertyId = BLayer.Property.GetPropertyIdFromTamarindId(data.PropertyId);
                    data.PropertyId = PropertyId;
                    //return (await TBOPropertyDetails(data.PropertyId, firstdate, lastdate, data));

                }


                Models.SimpleSearchModel bookingData = data.GetSimpleSearch();
                bookingData = FixInputs(bookingData);
                property = await GetAccommodationsByFilter(bookingData);
                if (User.Identity.IsAuthenticated)
                {
                    long uid = 0;
                    long.TryParse(User.Identity.GetUserId(), out uid);
                    if (uid > 0)
                    {
                        property.UserType = BLayer.User.GetRole(uid);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("Index", property);
        }
        public async Task<long> SaveAmadeus_Property(Models.PropertyModel data)
        {
            try
            {

                CLayer.Property prprty = new CLayer.Property()
                {

                    PropertyId = data.PropertyId,
                    Title = data.Title,
                    Description = data.Description,
                    Location = data.Location,
                    Status = (CLayer.ObjectStatus.StatusType)data.Status,
                    OwnerId = data.OwnerId,
                    Address = data.Address,
                    Country = data.Country,
                    State = data.State,
                    City = data.City,
                    CityId = data.CityId,
                    ZipCode = data.ZipCode,
                    PropertyInventoryType = data.InventoryAPIType,
                    HotelID = data.HotelId,
                    TBOHotelId = data.TBOHotelId,
                    TBOFlag = data.TBOFlag,
                    TamarindHotelId = data.TamarindHotelId,
                    TamarindFlag = data.TamarindFlag,
                    RateCardDetailedId = data.RateCardDetailedId,
                    TaxPercentage = data.TaxPercentage,
                    GSTSlab = data.GSTSlab
                };
                string loc = "";
                try
                {

                    string statename = data.StateName;
                    string cityname;
                    //if (data.Country == 0)
                    //{

                    //}
                    if (data.CityId < 1)
                    {
                        cityname = data.City;
                    }
                    else
                    {
                        cityname = BLayer.City.Get(data.CityId).Name;
                        data.City = cityname;
                        prprty.City = cityname;
                    }
                    string Countryname = data.CountryName;// BLayer.Country.Get(data.Country).Name;
                    loc = cityname + "," + statename + "," + Countryname + "," + data.ZipCode;
                    string qAdddress = data.Title + "," + data.Address + "," + loc;

                    //Common.Utils.Location pos;

                    //pos = await Common.Utils.GetLocation(qAdddress);
                    prprty.PositionLat = null;
                    prprty.PositionLng = null;

                }
                catch (Exception ex)
                {
                    Common.LogHandler.LogError(ex);
                }


                data.PropertyId = BLayer.Property.SaveAmadeus_Property(prprty);
                return data.PropertyId;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return 0;
            }
        }
        public void AccommodationSaveByAPI(Models.AccommodationModel data)
        {
            try
            {

                string userid = (User == null) ? "0" : User.Identity.GetUserId();
                long id = 0;
                long.TryParse(userid, out id);
                //if (ModelState.IsValid)
                //{
                CLayer.Accommodation accmdtn = new CLayer.Accommodation()
                {
                    AccommodationId = data.AccommodationId,
                    PropertyId = data.PropertyId,
                    AccommodationTypeId = data.AccommodationTypeId,
                    StayCategoryId = data.StayCategoryId,
                    AccommodationCount = 1,
                    Description = data.Description,
                    //Location = data.Location,
                    MaxNoOfPeople = 0,
                    MaxNoOfChildren = 0,
                    MinNoOfPeople = 0,
                    BedRooms = 0,
                    Area = 0,
                    Status = 1,
                    TotalAccommodations = 1,
                    UpdatedById = id
                };
                if (accmdtn.MaxNoOfPeople < accmdtn.MinNoOfPeople) accmdtn.MaxNoOfPeople = accmdtn.MinNoOfPeople;
                if (accmdtn.MaxNoOfPeople < accmdtn.MaxNoOfChildren) accmdtn.MaxNoOfPeople = accmdtn.MaxNoOfChildren;
                long accId = BLayer.Accommodation.Save_API(accmdtn);

                CLayer.Inventory inv = new CLayer.Inventory()
                {
                    AccommodationId = accId,
                    Quantity = 2,
                    FromDate = data.CheckIn,
                    ToDate = data.CheckOut
                };
                BLayer.Inventory.APIsave(inv);
                DateTime enddate = (data.CheckIn).AddDays(20);
                CLayer.Rates rate = new CLayer.Rates()
                {
                    AccommodationId = accId,
                    RateFor = 7,
                    DailyRate = data.Amount,
                    WeeklyRate = 0,
                    LongTermRate = 0,
                    MonthlyRate = 0,
                    GuestRate = 0,
                    StartDate = data.CheckIn,
                    EndDate = enddate,
                    Status = 1,
                    UpdatedBy = id,
                    SellRateAfterTax = data.AmountWithTax
                };
                BLayer.Rate.APISave(rate);


                if (data.RoomId == null) { data.RoomId = ""; }

                //  BLayer.Accommodation.SetRoomId(data.AccommodationId, data.RoomId.Trim());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
        }

        //public static string TBOMultiSingleAvailability(string hotelcode, string start, string end)
        //{
        //    string TBOresult = TBOHotelSearch(hotelcode);
        //}

        //**
        public static string HotelMultiSingleAvailability(string hotelcode, string start, string end)
        {
            string soapResult = string.Empty;
            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = "http://api.tektravels.com";//BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = "http://api.tektravels.com/BookingEngineService_Hotel/hotelservice.svc/rest/GetHotelRoom";//BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/Hotel_MultiSingleAvailability_10.0";


                string SoapHeader = APIUtility.SetSoapHeaderPriceingdetails(_url, _action);
                string SoapBody = APIUtility.SetHotelMultiSingleAvailabilityBodyPriceingdetails(hotelcode, start, end);

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";

                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(SoapEnvelop);

                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                webRequest.Proxy = null;
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
        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public async Task<ActionResult> TamrindPropertyDetails(long id, string start, string end, Models.SearchParamModel data = null)
        {
            LogHandler.AddLog("start:");
            LogHandler.AddLog(DateTime.Now.ToString());
            List<CLayer.SearchResult> TamarindResultList = new List<CLayer.SearchResult>();

            List<CLayer.Property> objDescriptions = BLayer.Property.GetAllTamarindPropertyDescriptionsWithOutData();
            List<CLayer.Property> objTitles = BLayer.Property.GetAllTamarindPropertyTitlesWithOutData();

            DataSet HotelResult = new DataSet();

            TempData["DetailUrl"] = Request.Url.AbsoluteUri;
            TempData.Keep("DetailUrl");
            string PropertyAddress = string.Empty;

            string gdscurrencycode = Convert.ToString(Request["gdscurrencycode"]);
            decimal gdsrateconversion = Convert.ToDecimal(Request["gdsrateconversion"]);

            DateTime startss = DateTime.Parse(start);
            DateTime endss = DateTime.Parse(end);
            StringBuilder Description = new StringBuilder();
            var startt = startss.ToString("yyyy-MM-dd");
            var endd = endss.ToString("yyyy-MM-dd");

            Models.PropertyModel property = new Models.PropertyModel();
            List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
            CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();

            List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();

            try
            {
                var hotelcode = "";
                var ChainCode = "HS";
                var AgeQualifyingCode = "10";
                var count = "2";
                property.PropertyId = id;
                string result = "";

                DataTable dtHoelCode = BLayer.Property.GetHotelIDFromTamarindid(id);
                hotelcode = dtHoelCode.Rows[0]["tamarind_hotelid"].ToString();
                result = TamrindMultiSingleAvailability(data);

                HotelResult.ReadXml(GenerateStreamFromString(result));

                long AmadeusPropertyID = 0;
                string CityName = string.Empty;

                var HotelItems = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();
                SearchController objSearch = new SearchController();

                long StateID = 0;
                string StateName = "";
                string City = Convert.ToString(Request["Destination"]);
                int CountryID = 0;
                long CityId = 0;
                long OwnerID = 0;
                int GDSStateID = 0;
                int GDSCityID = 0;
                string GDStateName = "";
                string Destination = Convert.ToString(Request["Destination"]);

                if (City != null && City != "")
                {
                    City = Common.Utils.SecureInputString(City);
                    Session["GDSCountry"] = APIUtility.GetGDSCountry(City);

                    string CountryName = Convert.ToString(Session["GDSCountry"]);

                    if (CountryName.ToUpper() == "INDIA")
                    {
                        CountryName = "";
                        Session["GDSCountry"] = "";

                    }

                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(CountryName);
                    int GDSCountryCount = (CountryName != "") ? GDSCountryList.Count : 0;
                    string sDestination = string.Empty;

                    if (GDSCountryCount > 0)
                    {
                        sDestination = APIUtility.GetGDSDestination(City);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        sDestination = APIUtility.GetGDSDestination(City);

                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (CountryName == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }

                        }
                    }
                }
                OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    StateID = BLayer.State.GetStateID(Destination).StateId;
                    StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                    CityId = BLayer.City.GetCityID(City).CityId;
                    CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;

                }
                else
                {
                    GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), Destination);
                    GDStateName = BLayer.State.Get(GDSStateID).Name;
                    GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                    CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                }
                if (HotelResult.Tables["hotel"].Rows.Count > 0)
                {
                    gdscurrencycode = Session["GDSCurrencyCode"].ToString();
                    gdsrateconversion = 0;

                    Session["GDSCurrencyCode"] = gdscurrencycode;
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;
                    }

                    List<string> HotelIDS = new List<string>();

                    for (int i = 0; i < HotelResult.Tables["hotel"].Rows.Count; i++)
                    {
                        //HotelItems = HotelResult.Tables["hotel"].Rows;
                        Models.PropertyModel objData = new Models.PropertyModel();
                        objData.PropertyId = 0;
                        objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase((HotelResult.Tables["hotel"].Rows[i]["HotelName"]).ToString().ToLower());

                        objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                        objData.OwnerId = OwnerID;

                        objData.CityName = Destination;
                        CityName = Destination;
                        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                        {
                            objData.State = Convert.ToInt32(StateID);
                            objData.StateName = StateName;
                            objData.City = CityName;
                            objData.CityId = Convert.ToInt32(CityId);
                        }
                        else
                        {

                            objData.State = GDSStateID;
                            objData.StateName = GDStateName;
                            objData.City = objData.CityName;
                            objData.CityId = GDSCityID;
                        }


                        objData.Address = "";
                        objData.ZipCode = "";

                        objData.Country = CountryID;
                        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                        {
                            objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                            gdscurrencycode = "INR";
                            gdsrateconversion = 0;
                            Session["GDSRateConversion"] = "0";
                            Session["GDSCurrencyCode"] = "INR";
                        }
                        else
                        {
                            objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                        }

                        if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                        {
                            objData.Location = objData.CityName + ", " + objData.StateName;
                            if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                            objData.Location = objData.Location + ", " + objData.CountryName;
                        }

                        string FullAddress = string.Empty;
                        List<string> LocationList = new List<string>();
                        LocationList.Add(objData.CityName);
                        LocationList.Add(objData.StateName);
                        LocationList.Add(objData.ZipCode);
                        LocationList.Add(objData.CountryName);

                        foreach (string itemLoc in LocationList)
                        {
                            if (!string.IsNullOrEmpty(itemLoc))
                            {
                                FullAddress = FullAddress + itemLoc + ",";
                            }
                        }
                        objData.Location = FullAddress.TrimEnd(',');
                        property.Location = objData.Location;
                        property.Address = APIUtility.FirstLetterToUpperCase(objData.Address + " " + property.Location);
                        //     PropertyAddress = property.Address;
                        PropertyAddress = APIUtility.ReplaceFirstOccurrence(property.Address, Destination, "");
                        PropertyAddress = APIUtility.ReplaceFirstOccurrence(PropertyAddress, objData.CountryName, "").Replace(",,", ",");

                        objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                        objData.HotelId = hotelcode;

                        AmadeusPropertyID = await objSearch.SaveAmadeus_Property(objData);

                        long AccomodationId = 0;
                        //string RoomStayRPH = item.RoomStayRPH;

                    }
                }

            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
            }
            return View("GDSIndex", property);
        }
        public async Task<ActionResult> TBOPropertyDetails(long id, string start, string end, Models.SearchParamModel data = null)
        {
            LogHandler.AddLog("start:");
            LogHandler.AddLog(DateTime.Now.ToString());
            List<CLayer.SearchResult> TBOResultList = new List<CLayer.SearchResult>();

            List<CLayer.Property> objDescriptions = BLayer.Property.GetAllTBOPropertyDescriptionsWithOutData();
            List<CLayer.Property> objTitles = BLayer.Property.GetAllTBOPropertyTitlesWithOutData();

            TempData["DetailUrl"] = Request.Url.AbsoluteUri;
            TempData.Keep("DetailUrl");
            string PropertyAddress = string.Empty;

            string gdscurrencycode = Convert.ToString(Request["gdscurrencycode"]);
            decimal gdsrateconversion = Convert.ToDecimal(Request["gdsrateconversion"]);

            DataSet HotelResult = new DataSet();

            DateTime startss = DateTime.Parse(start);
            DateTime endss = DateTime.Parse(end);
            StringBuilder Description = new StringBuilder();
            var startt = startss.ToString("yyyy-MM-dd");
            var endd = endss.ToString("yyyy-MM-dd");

            Models.PropertyModel property = new Models.PropertyModel();
            List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
            CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();

            List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();

            try
            {
                var hotelcode = "";

                var ChainCode = "HS";
                var AgeQualifyingCode = "10";
                var count = "2";
                property.PropertyId = id;
                string result = "";

                property.InventoryAPIType = 5;
                DataTable dtHoelCode = BLayer.Property.GetHotelIDFromTBOid(id);
                hotelcode = dtHoelCode.Rows[0]["tbo_hotelid"].ToString();
                result = TBOHotelSearch(hotelcode);

                HotelResult.ReadXml(GenerateStreamFromString(result));

                long AmadeusPropertyID = 0;
                string CityName = string.Empty;

                var HotelItems = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();
                SearchController objSearch = new SearchController();

                long StateID = 0;
                string StateName = "";
                string City = Convert.ToString(Request["Destination"]);
                int CountryID = 0;
                long CityId = 0;
                long OwnerID = 0;
                int GDSStateID = 0;
                int GDSCityID = 0;
                string GDStateName = "";
                string Destination = Convert.ToString(Request["Destination"]);

                if (City != null && City != "")
                {
                    City = Common.Utils.SecureInputString(City);
                    Session["GDSCountry"] = APIUtility.GetGDSCountry(City);

                    string CountryName = Convert.ToString(Session["GDSCountry"]);

                    if (CountryName.ToUpper() == "INDIA")
                    {
                        CountryName = "";
                        Session["GDSCountry"] = "";
                    }
                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(CountryName);
                    int GDSCountryCount = (CountryName != "") ? GDSCountryList.Count : 0;
                    string sDestination = string.Empty;

                    if (GDSCountryCount > 0)
                    {
                        sDestination = APIUtility.GetGDSDestination(City);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        sDestination = APIUtility.GetGDSDestination(City);

                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (CountryName == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }

                        }
                    }
                }
                OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    StateID = BLayer.State.GetStateID(Destination).StateId;
                    StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                    CityId = BLayer.City.GetCityID(City).CityId;
                    CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;

                }
                else
                {
                    GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), Destination);
                    GDStateName = BLayer.State.Get(GDSStateID).Name;
                    GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                    CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                }


                //if (HotelResult.Tables["hotel"].Rows.Count > 0)
                //{
                //    if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                //    { }
                //}
            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
            }
            return View("GDSIndex", property);

        }
        public async Task<ActionResult> AmadeusPropertyDetails(long id, string start, string end, Models.SearchParamModel data = null)
        {
            LogHandler.AddLog("start:");
            LogHandler.AddLog(DateTime.Now.ToString());
            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();

            List<CLayer.Property> objDescriptions = BLayer.Property.GetAllGDSPropertyDescriptionsWithOutData();
            List<CLayer.Property> objTitles = BLayer.Property.GetAllGDSPropertyTitlesWithOutData();

            TempData["DetailUrl"] = Request.Url.AbsoluteUri;
            TempData.Keep("DetailUrl");
            string PropertyAddress = string.Empty;

            string gdscurrencycode = Convert.ToString(Request["gdscurrencycode"]);
            decimal gdsrateconversion = Convert.ToDecimal(Request["gdsrateconversion"]); ;

            DateTime startss = DateTime.Parse(start);
            DateTime endss = DateTime.Parse(end);
            StringBuilder Description = new StringBuilder();
            var startt = startss.ToString("yyyy-MM-dd");
            var endd = endss.ToString("yyyy-MM-dd");

            Models.PropertyModel property = new Models.PropertyModel();
            List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
            CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();

            List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();

            try
            {
                DataTable dtHoelCode = BLayer.Property.GetHotelIDFrompropertyid(id);
                var hotelcode = dtHoelCode.Rows[0]["Hotel_Id"].ToString();


                var ChainCode = "HS";
                var AgeQualifyingCode = "10";
                var count = "2";
                property.PropertyId = id;
                property.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;

                string result = HotelMultiSingleAvailability(hotelcode, startt, endd);

                Serializer ser1 = new Serializer();
                CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();

                try
                {

                    HotelResult = ser1.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                }
                catch (Exception ex)
                {
                    try
                    {
                        HotelResultAdv = ser1.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                    }
                    catch (Exception ex1)
                    {
                        HotelResultAdvFirst = ser1.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                    }

                }



                #region property details update

                long AmadeusPropertyID = 0;
                string CityName = string.Empty;

                var HotelItems = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();
                SearchController objSearch = new SearchController();


                long StateID = 0;
                string StateName = "";
                string City = Convert.ToString(Request["Destination"]);
                int CountryID = 0;
                long CityId = 0;
                long OwnerID = 0;
                int GDSStateID = 0;
                int GDSCityID = 0;
                string GDStateName = "";
                string Destination = Convert.ToString(Request["Destination"]);

                #region country and destination
                if (City != null && City != "")
                {
                    City = Common.Utils.SecureInputString(City); //srch.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");
                    Session["GDSCountry"] = APIUtility.GetGDSCountry(City);
                    string CountryName = Convert.ToString(Session["GDSCountry"]);

                    if (CountryName.ToUpper() == "INDIA")
                    {
                        CountryName = "";
                        Session["GDSCountry"] = "";
                    }
                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(CountryName);
                    int GDSCountryCount = (CountryName != "") ? GDSCountryList.Count : 0;
                    string sDestination = string.Empty;
                    if (GDSCountryCount > 0)
                    {
                        sDestination = APIUtility.GetGDSDestination(City);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        sDestination = APIUtility.GetGDSDestination(City);

                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (CountryName == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }

                        }
                    }

                }
                #endregion

                OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    StateID = BLayer.State.GetStateID(Destination).StateId;
                    StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                    CityId = BLayer.City.GetCityID(City).CityId;
                    CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;

                }
                else
                {
                    GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), Destination);
                    GDStateName = BLayer.State.Get(GDSStateID).Name;
                    GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                    CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                }

                #region Hotel update

                if (HotelResult.Body != null)
                {

                    if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        Session["GDSCurrencyConversion"] = objCurrencyConversion;

                        gdscurrencycode = objCurrencyConversion.SourceCurrencyCode;
                        gdsrateconversion = objCurrencyConversion.RateConversion;

                        Session["GDSRateConversion"] = gdsrateconversion;
                        Session["GDSCurrencyCode"] = gdscurrencycode;

                    }
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;

                    }


                    if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                    {

                        List<string> HotelIDS = new List<string>();

                        foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                        {
                            HotelItems = item;
                            Models.PropertyModel objData = new Models.PropertyModel();
                            objData.PropertyId = 0;
                            objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());


                            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            objData.OwnerId = OwnerID;

                            objData.CityName = Destination;
                            CityName = Destination;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.State = Convert.ToInt32(StateID);
                                objData.StateName = StateName;
                                objData.City = CityName;
                                objData.CityId = Convert.ToInt32(CityId);
                            }
                            else
                            {

                                objData.State = GDSStateID;
                                objData.StateName = GDStateName;
                                objData.City = objData.CityName;
                                objData.CityId = GDSCityID;
                            }

                            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                            objData.Country = CountryID;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                gdscurrencycode = "INR";
                                gdsrateconversion = 0;
                                Session["GDSRateConversion"] = "0";
                                Session["GDSCurrencyCode"] = "INR";
                            }
                            else
                            {
                                objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                            }

                            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                            {
                                objData.Location = objData.CityName + ", " + objData.StateName;
                                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                objData.Location = objData.Location + ", " + objData.CountryName;
                            }
                            string FullAddress = string.Empty;
                            List<string> LocationList = new List<string>();
                            LocationList.Add(objData.CityName);
                            LocationList.Add(objData.StateName);
                            LocationList.Add(objData.ZipCode);
                            LocationList.Add(objData.CountryName);
                            foreach (string itemLoc in LocationList)
                            {
                                if (!string.IsNullOrEmpty(itemLoc))
                                {
                                    FullAddress = FullAddress + itemLoc + ",";
                                }
                            }
                            objData.Location = FullAddress.TrimEnd(',');
                            property.Location = objData.Location;
                            property.Address = APIUtility.FirstLetterToUpperCase(objData.Address + " " + property.Location);
                            //     PropertyAddress = property.Address;
                            PropertyAddress = APIUtility.ReplaceFirstOccurrence(property.Address, Destination, "");
                            PropertyAddress = APIUtility.ReplaceFirstOccurrence(PropertyAddress, objData.CountryName, "").Replace(",,", ",");


                            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            objData.HotelId = item.BasicPropertyInfo.HotelCode;


                            AmadeusPropertyID = await objSearch.SaveAmadeus_Property(objData);



                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            //   RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {

                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                                for (int i = 0; i < RoomStayRPHList.Length; i++)
                                {

                                    var RoomStays = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                    RoomStayItemList = RoomStays;
                                    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();
                                    AccomodationId = 0;
                                    foreach (var Accitem in objAcc.Where(x => x.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                    {
                                        //if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                        //{
                                        //    AccomodationId = Accitem.AccommodationId;
                                        //    break;
                                        //}
                                        //if (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode)
                                        //{
                                        AccomodationId = Accitem.AccommodationId;
                                        break;
                                        //; }
                                    }

                                    if (AccomodationId == 0)
                                    {

                                        foreach (var RoomStayItem in RoomStays)
                                        {
                                            dataAccommodation.AccommodationId = AccomodationId;
                                            dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                            dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                            dataAccommodation.PropertyId = AmadeusPropertyID;
                                            dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                            dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                            if (RoomStayItem.RoomTypes != null)
                                            {
                                                dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                                dataAccommodation.RoomTypeDescription = APIUtility.RoomTypeDescriptionFirst(dataAccommodation.RoomTypeCode);
                                            }



                                            dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                            if (RoomStayItem.RoomRates != null)
                                            {
                                                dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                                dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                                dataAccommodation.Description = RoomStayItem.RoomRates.RoomRate.RoomRateDescription.Text[0].ToString();
                                            }
                                            if (RoomStayItem.RatePlans != null)
                                            {
                                                dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                            }


                                            objSearch.AccommodationSave(dataAccommodation);


                                        }
                                    }

                                }
                                RoomStaysResultLists = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, gdscurrencycode, gdsrateconversion);
                            }

                        }

                    }



                    #endregion

                }
                else if (HotelResultAdvFirst.Body != null)
                {

                    if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        Session["GDSCurrencyConversion"] = objCurrencyConversion;

                        gdscurrencycode = objCurrencyConversion.SourceCurrencyCode;
                        gdsrateconversion = objCurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = gdsrateconversion;
                        Session["GDSCurrencyCode"] = gdscurrencycode;
                    }
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;
                    }



                    if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                    {

                        foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                        {
                            HotelItemAdvFirst = item;
                            Models.PropertyModel objData = new Models.PropertyModel();
                            objData.PropertyId = 0;
                            objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            objData.OwnerId = OwnerID;

                            objData.CityName = Destination;
                            CityName = Destination;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.State = Convert.ToInt32(StateID);
                                objData.StateName = StateName;
                                objData.City = CityName;
                                objData.CityId = Convert.ToInt32(CityId);
                            }
                            else
                            {

                                objData.State = GDSStateID;
                                objData.StateName = GDStateName;
                                objData.City = objData.CityName;
                                objData.CityId = GDSCityID;
                            }

                            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                            objData.Country = CountryID;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.CountryName = BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                gdscurrencycode = "INR";
                                gdsrateconversion = 0;
                                Session["GDSRateConversion"] = "0";
                                Session["GDSCurrencyCode"] = "INR";
                            }
                            else
                            {
                                objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                            }

                            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                            {
                                objData.Location = objData.CityName + ", " + objData.StateName;
                                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                objData.Location = objData.Location + ", " + objData.CountryName;
                            }

                            string FullAddress = string.Empty;
                            List<string> LocationList = new List<string>();
                            LocationList.Add(objData.CityName);
                            LocationList.Add(objData.StateName);
                            LocationList.Add(objData.ZipCode);
                            LocationList.Add(objData.CountryName);
                            foreach (string itemLoc in LocationList)
                            {
                                if (!string.IsNullOrEmpty(itemLoc))
                                {
                                    FullAddress = FullAddress + itemLoc + ",";
                                }
                            }
                            objData.Location = FullAddress.TrimEnd(',');
                            property.Location = objData.Location;
                            property.Address = APIUtility.FirstLetterToUpperCase(objData.Address + " " + property.Location);
                            //   PropertyAddress = property.Address;
                            PropertyAddress = APIUtility.ReplaceFirstOccurrence(property.Address, Destination, "");
                            PropertyAddress = APIUtility.ReplaceFirstOccurrence(PropertyAddress, objData.CountryName, "").Replace(",,", ",");



                            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            objData.HotelId = item.BasicPropertyInfo.HotelCode;

                            AmadeusPropertyID = await objSearch.SaveAmadeus_Property(objData);



                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            //     List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {
                                //BLayer.Accommodation.Accommodation_Amadeus_Delete(AmadeusPropertyID);
                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                                for (int i = 0; i < RoomStayRPHList.Length; i++)
                                {

                                    var RoomStays = HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                    RoomStayItemListAdvFirst = RoomStays;
                                    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();

                                    AccomodationId = 0;
                                    foreach (var Accitem in objAcc.Where(x => x.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                    {
                                        //if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                        //{
                                        //    AccomodationId = Accitem.AccommodationId;
                                        //    break;
                                        //}
                                        //if  (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode)
                                        //{
                                        AccomodationId = Accitem.AccommodationId;
                                        break;
                                        // }
                                    }
                                    if (AccomodationId == 0)
                                    {
                                        foreach (var RoomStayItem in RoomStays)
                                        {
                                            dataAccommodation.AccommodationId = AccomodationId;
                                            dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                            dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                            dataAccommodation.PropertyId = AmadeusPropertyID;
                                            dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                            dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                            if (RoomStayItem.RoomTypes != null)
                                            {
                                                dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                            }


                                            dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                            if (RoomStayItem.RoomRates != null)
                                            {
                                                dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                                dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                            }
                                            if (RoomStayItem.RatePlans != null)
                                            {
                                                dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                            }


                                            objSearch.AccommodationSave(dataAccommodation);
                                        }

                                    }

                                }
                                //       RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Request["gdscurrencycode"]), Convert.ToDecimal(Request["gdsrateconversion"]));
                                RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, gdscurrencycode, gdsrateconversion);
                            }
                        }
                    }
                }

                #endregion



                DataTable details = BLayer.Property.GetAccommodationDetailsFrompropertyid(id);
                if (details.Rows.Count > 0)
                {
                    hotelcode = details.Rows[0]["Hotel_Id"].ToString();
                }


                Serializer ser = new Serializer();
                string hotel = GetGDS_Hotel_Details(hotelcode);



                #region Transaction Log 

                APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), hotel, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelDescriptiveinfo, 0);

                #endregion Transaction log end


                CLayer.GDSBookingDetails.Envelope ss = new CLayer.GDSBookingDetails.Envelope();
                ss = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);

                CLayer.GDSBookingDetailsAdv.Envelope ssAdv = new CLayer.GDSBookingDetailsAdv.Envelope();


                var DescriptionItemList = objDescriptions.Where(x => x.HotelID == hotelcode).ToList();

                DataTable dtHotel = BLayer.Property.GetHotelFormattedDescription(id);
                string FormattedDescription = string.Empty;
                int Ratings = 0;
                if (dtHotel.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtHotel.Rows)
                    {
                        FormattedDescription = Convert.ToString(dr["FormattedDescription"]);
                        Ratings = Convert.ToInt32(dr["Rating"]);
                    }
                }


                if (string.IsNullOrEmpty(FormattedDescription))
                {
                    FormattedDescription = GDSProcess.GDSFormatDescription.GetFormattedDescription(hotel);
                    Ratings = GDSProcess.GDSFormatDescription.StarRatings;
                    BLayer.Property.GDSUpdatePropertyDescriptionFormatted(id, FormattedDescription, GDSProcess.GDSFormatDescription.StarRatings, "");
                }

                int LocalStarRating = APIUtility.GetStarRating(hotel);
                if (LocalStarRating > 0)
                {
                    BLayer.Property.GDSUpdatePropertyStarRatings(id, LocalStarRating);
                    Ratings = LocalStarRating;

                }


                if (!string.IsNullOrEmpty(FormattedDescription))
                {
                    property.Description = FormattedDescription;

                    property.FormattedDescription = FormattedDescription;
                }
                else
                {
                    if (ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo != null)
                    {
                        var description = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;

                        if (description != null)
                        {
                            var descriptionItem = description.MultimediaDescriptions.MultimediaDescription;

                            var Descriptions = from order in descriptionItem
                                               where order.TextItems != null
                                               select order;
                            string HotelDescription = string.Empty;
                            foreach (var desc in Descriptions)
                            {
                                if (desc.InfoCode != null)
                                {
                                    foreach (var datas in desc.TextItems.TextItem.Description)
                                    {


                                        HotelDescription = HotelDescription + datas.__Text + "#256#";

                                    }
                                }
                                //    HotelDescription = HotelDescription + "<br>";

                            }
                            Description.Append(HotelDescription);

                            if (DescriptionItemList != null)
                            {
                                if (DescriptionItemList.Count > 0)
                                {
                                    BLayer.Property.GDSUpdatePropertyDescription(id, Description.ToString());
                                }
                            }
                            property.Description = Description.Replace("-Property description-", "").ToString();
                            int pLength = property.Description.Trim().Length - 1;
                            property.Description = property.Description.Trim().Substring(0, 1).ToUpper().ToString() + property.Description.Trim().Substring(1, pLength);

                        }
                    }
                }








                var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var SecurityToken = HotelResult.Header.Session.SecurityToken;
                var SessionId = HotelResult.Header.Session.SessionId;
                var SequenceNumber = HotelResult.Header.Session.SequenceNumber.ToString();

                //Roomstayresult
                //List<CLayer.RoomStaysResult> RoomStaysResultList = null;
                //RoomStaysResultList = null;
                //foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                //{
                //    HotelItem = item;

                //    string RoomStayRPH = item.RoomStayRPH;
                //    RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                //    if (!string.IsNullOrEmpty(RoomStayRPH))
                //    {
                //        string[] RoomStayRPHList1 = RoomStayRPH.Split(' ');

                //        RoomStaysResultList = SearchController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList1, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                //    }
                //}
                //Roomstayresult end

                foreach (DataRow dt in details.Rows)
                {
                    Accommodationsss = new CLayer.Accommodation();
                    var BookingCode = dt["BookingCode"].ToString();
                    var RatePlanCode = dt["RatePlanCode"].ToString();
                    var RoomTypeCode = dt["RoomTypeCode"].ToString();
                    long AccommodationID = Convert.ToInt64(dt["accommodationid"]);
                    var RoomStaysitem = RoomStaysResultLists.Where(x => x.BookingCode == BookingCode).ToList();
                    //  var RoomStaysitem = RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList();

                    foreach (var Ritem in RoomStaysitem)
                    {
                        var item = Ritem.RoomRateDescriptionItem;
                        CLayer.RoomRateDescription objRoomRateDescription = new CLayer.RoomRateDescription();
                        objRoomRateDescription.Name = item.Name;
                        Accommodationsss.AccommodationType = objRoomRateDescription.Name;// RoomStaysResultList.Where(x => x.BookingCode == BookingCode).FirstOrDefault().RoomRateDescriptionItem.Name;//.RoomRateDescription.Name;

                        var descriptionItem = Ritem.RoomRateDescriptionItem.Description;

                        string RateTypeDesc = string.Empty;
                        foreach (CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText m in descriptionItem)
                        {
                            RateTypeDesc = RateTypeDesc + m.Value + ".";
                        }
                        Accommodationsss.Description = RateTypeDesc;

                        Accommodationsss.RoomTypeCode = Ritem.RoomTypeCode;

                        Accommodationsss.RoomTypeDecription = (Ritem.RoomTypeCode == "ROH") ? "Run of house*" : APIUtility.GenerateRoomDescription(Ritem.RoomTypeCode);
                        Accommodationsss.RoomTypeDecription = string.IsNullOrEmpty(Accommodationsss.RoomTypeDecription) ? "" : Accommodationsss.RoomTypeDecription.TrimEnd(',');


                        var rate = Ritem.AmountAfterTax;
                        Accommodationsss.Rate = Convert.ToDecimal(rate);
                        Accommodationsss.MaxNoOfChildren = 2;
                        Accommodationsss.MaxNoOfPeople = 3;
                        Accommodationsss.MaxNoOfPeople = 1;
                        Accommodationsss.AccommodationCount = 3;
                        Accommodationsss.AccommodationId = AccommodationID;
                        Accommodationsss.MealPlan = (Ritem.IsMealPlanBreakfast) ? "Breakfast" : "";
                        Accommodationsss.CommissionPercent = APIUtility.fnNumberwithoutzero(Convert.ToString(Ritem.CommissionPercent)) + "%";

                        Accommodationss.Add(Accommodationsss);
                    }

                }
                //Find images for property

                long imageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                #region UPDATE PROPERTY IMAGES
                long ImageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                if (ImageCount < 1)
                {
                    // BLayer.Property.DeleteGDSPropertyImages(id);
                    List<CLayer.PropertyFiles> pictlist = GetGDSImages(hotel, id);
                    if (pictlist.Count > 0)
                    {
                        property.Pictures.Pictures = pictlist.ToList();
                    }
                }
                else
                {
                    List<string> images = BLayer.Property.GetGDSHotelAllImages(id);
                    List<CLayer.PropertyFiles> picturelist = new List<CLayer.PropertyFiles>();
                    CLayer.PropertyFiles picture = null;
                    foreach (string item in images)
                    {
                        picture = new CLayer.PropertyFiles();
                        picture.FileName = item;
                        picture.PropertyId = id;
                        picturelist.Add(picture);
                    }
                    property.Pictures.Pictures = picturelist.ToList();
                }
                //}


                #endregion
                var ssw = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;





                if (ssw != null)
                {

                    var sswList = ssw;

                }


                CLayer.GDSBookingDetails.Envelope GDSBookingDetails = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);
                property.Title = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelName;
                DateTime starts = DateTime.Parse(start);
                DateTime ends = DateTime.Parse(end);
                property.Filter.HiddenCheckIn = starts;
                property.Filter.HiddenCheckOut = ends;
                property.Filter.Adults = data.Adults;
                property.Filter.Bedrooms = data.Bedrooms;
                property.Filter.Children = data.Children;

                property.Filter.Destination = CityName;
                property.Filter.CheckIn = starts;
                property.Filter.CheckOut = ends;

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses != null)
                {
                    property.ZipCode = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.PostalCode;
                    property.Address = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.AddressLine;
                    property.StateName = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                    property.City = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                    property.Location = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;

                }

                property.PropertyId = id;

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails != null)
                {
                    property.Email = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails.Email.ToString();
                }

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones != null)
                {
                    if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").Count() > 0)
                    {
                        property.Phone = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").FirstOrDefault().PhoneNumber;

                    }
                    if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").Count() > 0)
                    {
                        property.Mobile = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").FirstOrDefault().PhoneNumber;
                    }
                    if ((!string.IsNullOrEmpty(property.Phone)) || (!string.IsNullOrEmpty(property.Mobile)) || (!string.IsNullOrEmpty(property.Email)))
                    {
                        BLayer.Property.GDSUpdatePropertyContactNumbers(id, property.Phone, property.Mobile, property.Email);
                    }
                }

                property.Address = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(PropertyAddress.ToLower());
                property.HotelId = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelCode;
                if (User.Identity.IsAuthenticated)
                {
                    long uid = 0;
                    long.TryParse(User.Identity.GetUserId(), out uid);
                    if (uid > 0)
                    {
                        property.UserType = BLayer.User.GetRole(uid);
                    }
                }

                property.Accommodations.Accommodations = Accommodationss;
                property.Rating = Ratings;

            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
            }
            return View("GDSIndex", property);
            //  return View("Index", property);
        }

        public async Task<ActionResult> AmadeusPropertyDetails(CLayer.Property pValue, string start, string end, Models.SearchParamModel data = null)
        {
            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();

            List<CLayer.Property> objDescriptions = BLayer.Property.GetAllGDSPropertyDescriptionsWithOutData();
            List<CLayer.Property> objTitles = BLayer.Property.GetAllGDSPropertyTitlesWithOutData();

            TempData["DetailUrl"] = Request.Url.AbsoluteUri;
            TempData.Keep("DetailUrl");
            string PropertyAddress = string.Empty;

            DateTime startss = pValue.StartDate;
            DateTime endss = pValue.EndDate;
            StringBuilder Description = new StringBuilder();
            var startt = startss.ToString("yyyy-MM-dd");
            var endd = endss.ToString("yyyy-MM-dd");

            Models.PropertyModel property = new Models.PropertyModel();
            List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
            CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();
            long id = pValue.PropertyId;

            List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();

            try
            {
                DataTable dtHoelCode = BLayer.Property.GetHotelIDFrompropertyid(id);
                var hotelcode = dtHoelCode.Rows[0]["Hotel_Id"].ToString();


                var ChainCode = "HS";
                var AgeQualifyingCode = "10";
                var count = "2";
                property.PropertyId = id;
                property.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;

                string result = HotelMultiSingleAvailability(hotelcode, startt, endd);

                Serializer ser1 = new Serializer();
                CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();

                try
                {

                    HotelResult = ser1.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                }
                catch (Exception ex)
                {
                    try
                    {
                        HotelResultAdv = ser1.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                    }
                    catch (Exception ex1)
                    {
                        HotelResultAdvFirst = ser1.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                    }

                }



                #region property details update

                long AmadeusPropertyID = 0;
                string CityName = string.Empty;

                var HotelItems = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();
                SearchController objSearch = new SearchController();


                long StateID = pValue.State;
                string StateName = pValue.Statename;
                string City = pValue.City; //Convert.ToString(Request["Destination"]);
                int CountryID = pValue.Country;
                long CityId = 0;
                long OwnerID = 0;
                int GDSStateID = 0;
                int GDSCityID = 0;
                string GDStateName = "";
                string Destination = pValue.City; //Convert.ToString(Request["Destination"]);
                string GDSCurrencyCode = "INR";
                decimal GDSRateConversion = 0;

                OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    StateID = BLayer.State.GetStateID(Destination).StateId;
                    StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                    CityId = BLayer.City.GetCityID(City).CityId;
                    CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;
                }
                else
                {
                    GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), Destination);
                    GDStateName = BLayer.State.Get(GDSStateID).Name;
                    GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                    CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;

                }

                #region Hotel update

                if (HotelResult.Body != null)
                {

                    if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        GDSCurrencyCode = objCurrencyConversion.SourceCurrencyCode;
                        GDSRateConversion = objCurrencyConversion.RateConversion;

                        Session["GDSCurrencyConversion"] = objCurrencyConversion;
                    }
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;
                    }


                    if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                    {

                        List<string> HotelIDS = new List<string>();

                        foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                        {
                            HotelItems = item;
                            Models.PropertyModel objData = new Models.PropertyModel();
                            objData.PropertyId = 0;
                            objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());


                            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            objData.OwnerId = OwnerID;

                            objData.CityName = Destination;
                            CityName = Destination;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.State = Convert.ToInt32(StateID);
                                objData.StateName = StateName;
                                objData.City = CityName;
                                objData.CityId = Convert.ToInt32(CityId);
                            }
                            else
                            {

                                objData.State = GDSStateID;
                                objData.StateName = GDStateName;
                                objData.City = objData.CityName;
                                objData.CityId = GDSCityID;
                            }

                            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                            objData.Country = CountryID;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                            }
                            else
                            {
                                objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                            }

                            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                            {
                                objData.Location = objData.CityName + ", " + objData.StateName;
                                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                objData.Location = objData.Location + ", " + objData.CountryName;
                            }
                            string FullAddress = string.Empty;
                            List<string> LocationList = new List<string>();
                            LocationList.Add(objData.CityName);
                            LocationList.Add(objData.StateName);
                            LocationList.Add(objData.ZipCode);
                            LocationList.Add(objData.CountryName);
                            foreach (string itemLoc in LocationList)
                            {
                                if (!string.IsNullOrEmpty(itemLoc))
                                {
                                    FullAddress = FullAddress + itemLoc + ",";
                                }
                            }
                            objData.Location = FullAddress.TrimEnd(',');
                            property.Location = objData.Location;

                            property.Address = property.Location;

                            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            objData.HotelId = item.BasicPropertyInfo.HotelCode;


                            AmadeusPropertyID = await objSearch.SaveAmadeus_Property(objData);



                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {

                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                                for (int i = 0; i < RoomStayRPHList.Length; i++)
                                {

                                    var RoomStays = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                    RoomStayItemList = RoomStays;
                                    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();
                                    AccomodationId = 0;
                                    foreach (var Accitem in objAcc.Where(x => x.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                    {
                                        //if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                        //{
                                        //    AccomodationId = Accitem.AccommodationId;
                                        //    break;
                                        //}
                                        //if (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode)
                                        //{
                                        AccomodationId = Accitem.AccommodationId;
                                        break;
                                        //; }
                                    }

                                    if (AccomodationId == 0)
                                    {

                                        foreach (var RoomStayItem in RoomStays)
                                        {
                                            dataAccommodation.AccommodationId = AccomodationId;
                                            dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                            dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                            dataAccommodation.PropertyId = AmadeusPropertyID;
                                            dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                            dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                            if (RoomStayItem.RoomTypes != null)
                                            {
                                                dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                                dataAccommodation.RoomTypeDescription = APIUtility.RoomTypeDescriptionFirst(dataAccommodation.RoomTypeCode);
                                            }



                                            dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                            if (RoomStayItem.RoomRates != null)
                                            {
                                                dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                                dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                                dataAccommodation.Description = RoomStayItem.RoomRates.RoomRate.RoomRateDescription.Text[0].ToString();
                                            }
                                            if (RoomStayItem.RatePlans != null)
                                            {
                                                dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                            }


                                            objSearch.AccommodationSave(dataAccommodation);


                                        }
                                    }

                                }
                                RoomStaysResultLists = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, GDSCurrencyCode, GDSRateConversion, true);
                            }

                        }

                    }



                    #endregion

                }
                else if (HotelResultAdvFirst.Body != null)
                {

                    if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        GDSCurrencyCode = objCurrencyConversion.SourceCurrencyCode;
                        GDSRateConversion = objCurrencyConversion.RateConversion;

                        Session["GDSCurrencyConversion"] = objCurrencyConversion;
                    }
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;
                    }



                    if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                    {

                        foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                        {
                            HotelItemAdvFirst = item;
                            Models.PropertyModel objData = new Models.PropertyModel();
                            objData.PropertyId = 0;
                            objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            objData.OwnerId = OwnerID;

                            objData.CityName = Destination;
                            CityName = Destination;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.State = Convert.ToInt32(StateID);
                                objData.StateName = StateName;
                                objData.City = CityName;
                                objData.CityId = Convert.ToInt32(CityId);
                            }
                            else
                            {

                                objData.State = GDSStateID;
                                objData.StateName = GDStateName;
                                objData.City = objData.CityName;
                                objData.CityId = GDSCityID;
                            }

                            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                            objData.Country = CountryID;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.CountryName = BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                            }
                            else
                            {
                                objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                            }

                            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                            {
                                objData.Location = objData.CityName + ", " + objData.StateName;
                                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                objData.Location = objData.Location + ", " + objData.CountryName;
                            }

                            string FullAddress = string.Empty;
                            List<string> LocationList = new List<string>();
                            LocationList.Add(objData.CityName);
                            LocationList.Add(objData.StateName);
                            LocationList.Add(objData.ZipCode);
                            LocationList.Add(objData.CountryName);
                            foreach (string itemLoc in LocationList)
                            {
                                if (!string.IsNullOrEmpty(itemLoc))
                                {
                                    FullAddress = FullAddress + itemLoc + ",";
                                }
                            }
                            objData.Location = FullAddress.TrimEnd(',');
                            property.Location = objData.Location;
                            ;
                            property.Address = APIUtility.FirstLetterToUpperCase(objData.Address + " " + property.Location);
                            PropertyAddress = property.Address;


                            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            objData.HotelId = item.BasicPropertyInfo.HotelCode;

                            AmadeusPropertyID = await objSearch.SaveAmadeus_Property(objData);



                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            // List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {
                                //BLayer.Accommodation.Accommodation_Amadeus_Delete(AmadeusPropertyID);
                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                                for (int i = 0; i < RoomStayRPHList.Length; i++)
                                {

                                    var RoomStays = HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                    RoomStayItemListAdvFirst = RoomStays;
                                    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();

                                    AccomodationId = 0;
                                    foreach (var Accitem in objAcc.Where(x => x.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                    {
                                        //if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                        //{
                                        //    AccomodationId = Accitem.AccommodationId;
                                        //    break;
                                        //}
                                        //if  (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode)
                                        //{
                                        AccomodationId = Accitem.AccommodationId;
                                        break;
                                        // }
                                    }
                                    if (AccomodationId == 0)
                                    {
                                        foreach (var RoomStayItem in RoomStays)
                                        {
                                            dataAccommodation.AccommodationId = AccomodationId;
                                            dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                            dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                            dataAccommodation.PropertyId = AmadeusPropertyID;
                                            dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                            dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                            if (RoomStayItem.RoomTypes != null)
                                            {
                                                dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                            }


                                            dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                            if (RoomStayItem.RoomRates != null)
                                            {
                                                dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                                dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                            }
                                            if (RoomStayItem.RatePlans != null)
                                            {
                                                dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                            }


                                            objSearch.AccommodationSave(dataAccommodation);
                                        }

                                    }

                                }
                                RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, GDSCurrencyCode, GDSRateConversion, true);
                            }
                        }
                    }
                }

                #endregion



                DataTable details = BLayer.Property.GetAccommodationDetailsFrompropertyid(id);
                if (details.Rows.Count > 0)
                {
                    hotelcode = details.Rows[0]["Hotel_Id"].ToString();
                }


                Serializer ser = new Serializer();
                string hotel = GetGDS_Hotel_Details(hotelcode);


                #region Transaction Log 

                APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), hotel, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelDescriptiveinfo, 0);

                #endregion Transaction log end


                CLayer.GDSBookingDetails.Envelope ss = new CLayer.GDSBookingDetails.Envelope();
                ss = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);

                CLayer.GDSBookingDetailsAdv.Envelope ssAdv = new CLayer.GDSBookingDetailsAdv.Envelope();


                var DescriptionItemList = objDescriptions.Where(x => x.HotelID == hotelcode).ToList();

                DataTable dtHotel = BLayer.Property.GetHotelFormattedDescription(id);
                string FormattedDescription = string.Empty;
                int Ratings = 0;
                if (dtHotel.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtHotel.Rows)
                    {
                        FormattedDescription = Convert.ToString(dr["FormattedDescription"]);
                        Ratings = Convert.ToInt32(dr["Rating"]);
                    }
                }
                if (string.IsNullOrEmpty(FormattedDescription))
                {
                    FormattedDescription = GDSProcess.GDSFormatDescription.GetFormattedDescription(hotel);
                    Ratings = GDSProcess.GDSFormatDescription.StarRatings;
                    BLayer.Property.GDSUpdatePropertyDescriptionFormatted(id, FormattedDescription, GDSProcess.GDSFormatDescription.StarRatings, "");
                }
                int LocalStarRating = APIUtility.GetStarRating(hotel);
                if (LocalStarRating > 0)
                {
                    BLayer.Property.GDSUpdatePropertyStarRatings(id, LocalStarRating);
                    Ratings = LocalStarRating;
                }


                if (!string.IsNullOrEmpty(FormattedDescription))
                {
                    property.Description = FormattedDescription;

                    property.FormattedDescription = FormattedDescription;
                }
                else
                {
                    if (ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo != null)
                    {
                        var description = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;

                        if (description != null)
                        {
                            var descriptionItem = description.MultimediaDescriptions.MultimediaDescription;

                            var Descriptions = from order in descriptionItem
                                               where order.TextItems != null
                                               select order;
                            string HotelDescription = string.Empty;
                            foreach (var desc in Descriptions)
                            {
                                if (desc.InfoCode != null)
                                {
                                    foreach (var datas in desc.TextItems.TextItem.Description)
                                    {


                                        HotelDescription = HotelDescription + datas.__Text + "#256#";

                                    }
                                }
                                //    HotelDescription = HotelDescription + "<br>";

                            }
                            Description.Append(HotelDescription);

                            if (DescriptionItemList != null)
                            {
                                if (DescriptionItemList.Count > 0)
                                {
                                    BLayer.Property.GDSUpdatePropertyDescription(id, Description.ToString());
                                }
                            }
                            property.Description = Description.Replace("-Property description-", "").ToString();
                            int pLength = property.Description.Trim().Length - 1;
                            property.Description = property.Description.Trim().Substring(0, 1).ToUpper().ToString() + property.Description.Trim().Substring(1, pLength);

                        }
                    }
                }








                var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var SecurityToken = HotelResult.Header.Session.SecurityToken;
                var SessionId = HotelResult.Header.Session.SessionId;
                var SequenceNumber = HotelResult.Header.Session.SequenceNumber.ToString();

                //Roomstayresult
                //List<CLayer.RoomStaysResult> RoomStaysResultList = null;
                //RoomStaysResultList = null;
                //foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                //{
                //    HotelItem = item;

                //    string RoomStayRPH = item.RoomStayRPH;
                //    RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                //    if (!string.IsNullOrEmpty(RoomStayRPH))
                //    {
                //        string[] RoomStayRPHList1 = RoomStayRPH.Split(' ');

                //        RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList1, GDSCurrencyCode, GDSRateConversion, true);
                //        //  RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, GDSCurrencyCode, GDSRateConversion, true);
                //    }
                //}
                //Roomstayresult end

                foreach (DataRow dt in details.Rows)
                {
                    Accommodationsss = new CLayer.Accommodation();
                    var BookingCode = dt["BookingCode"].ToString();
                    var RatePlanCode = dt["RatePlanCode"].ToString();
                    var RoomTypeCode = dt["RoomTypeCode"].ToString();
                    long AccommodationID = Convert.ToInt64(dt["accommodationid"]);

                    var RoomStaysitem = RoomStaysResultLists.Where(x => x.BookingCode == BookingCode).ToList();

                    foreach (var Ritem in RoomStaysitem)
                    {
                        var item = Ritem.RoomRateDescriptionItem;
                        CLayer.RoomRateDescription objRoomRateDescription = new CLayer.RoomRateDescription();
                        objRoomRateDescription.Name = item.Name;
                        Accommodationsss.AccommodationType = objRoomRateDescription.Name;// RoomStaysResultList.Where(x => x.BookingCode == BookingCode).FirstOrDefault().RoomRateDescriptionItem.Name;//.RoomRateDescription.Name;

                        var descriptionItem = Ritem.RoomRateDescriptionItem.Description;

                        string RateTypeDesc = string.Empty;
                        foreach (CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText m in descriptionItem)
                        {
                            RateTypeDesc = RateTypeDesc + m.Value + ".";
                        }
                        Accommodationsss.Description = RateTypeDesc;

                        Accommodationsss.RoomTypeCode = Ritem.RoomTypeCode;

                        Accommodationsss.RoomTypeDecription = (Ritem.RoomTypeCode == "ROH") ? "Run of house*" : APIUtility.GenerateRoomDescription(Ritem.RoomTypeCode);
                        Accommodationsss.RoomTypeDecription = string.IsNullOrEmpty(Accommodationsss.RoomTypeDecription) ? "" : Accommodationsss.RoomTypeDecription.TrimEnd(',');


                        var rate = Ritem.AmountAfterTax;
                        Accommodationsss.Rate = Convert.ToDecimal(rate);
                        Accommodationsss.MaxNoOfChildren = 2;
                        Accommodationsss.MaxNoOfPeople = 3;
                        Accommodationsss.MaxNoOfPeople = 1;
                        Accommodationsss.AccommodationCount = 3;
                        Accommodationsss.AccommodationId = AccommodationID;
                        Accommodationsss.MealPlan = (Ritem.IsMealPlanBreakfast) ? "Breakfast" : "";
                        Accommodationsss.CommissionPercent = APIUtility.fnNumberwithoutzero(Convert.ToString(Ritem.CommissionPercent)) + "%";

                        Accommodationss.Add(Accommodationsss);
                    }

                }
                //Find images for property

                long imageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                #region UPDATE PROPERTY IMAGES
                long ImageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                if (ImageCount < 1)
                {
                    // BLayer.Property.DeleteGDSPropertyImages(id);
                    List<CLayer.PropertyFiles> pictlist = GetGDSImages(hotel, id);
                    if (pictlist.Count > 0)
                    {
                        property.Pictures.Pictures = pictlist.ToList();
                    }
                }
                else
                {
                    List<string> images = BLayer.Property.GetGDSHotelAllImages(id);
                    List<CLayer.PropertyFiles> picturelist = new List<CLayer.PropertyFiles>();
                    CLayer.PropertyFiles picture = null;
                    foreach (string item in images)
                    {
                        picture = new CLayer.PropertyFiles();
                        picture.FileName = item;
                        picture.PropertyId = id;
                        picturelist.Add(picture);
                    }
                    property.Pictures.Pictures = picturelist.ToList();
                }
                //}


                #endregion
                var ssw = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;





                if (ssw != null)
                {

                    var sswList = ssw;

                }


                CLayer.GDSBookingDetails.Envelope GDSBookingDetails = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);
                property.Title = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelName;
                DateTime starts = DateTime.Parse(start);
                DateTime ends = DateTime.Parse(end);
                property.Filter.HiddenCheckIn = starts;
                property.Filter.HiddenCheckOut = ends;
                //property.Filter.Adults = data.Adults;
                //property.Filter.Bedrooms = data.Bedrooms;
                //property.Filter.Children = data.Children;
                property.Filter.Adults = DEFAULT_ADULTS_COUNT;
                property.Filter.Bedrooms = DEFAULT_BEDROOMS_COUNT;
                property.Filter.Children = DEFAULT_CHILDREN_COUNT;
                property.Filter.Destination = City;
                property.Filter.CheckIn = starts;
                property.Filter.CheckOut = ends;


                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses != null)
                {
                    property.ZipCode = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.PostalCode;
                    property.Address = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.AddressLine;
                    property.StateName = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                    property.City = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                    property.Location = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;

                }

                property.PropertyId = id;

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails != null)
                {
                    property.Email = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails.Email.ToString();
                }

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones != null)
                {
                    if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").Count() > 0)
                    {
                        property.Phone = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").FirstOrDefault().PhoneNumber;

                    }
                    if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").Count() > 0)
                    {
                        property.Mobile = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").FirstOrDefault().PhoneNumber;
                    }
                    if ((!string.IsNullOrEmpty(property.Phone)) || (!string.IsNullOrEmpty(property.Mobile)) || (!string.IsNullOrEmpty(property.Email)))
                    {
                        BLayer.Property.GDSUpdatePropertyContactNumbers(id, property.Phone, property.Mobile, property.Email);
                    }
                }

                property.Address = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(PropertyAddress.ToLower());
                property.HotelId = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelCode;
                if (User.Identity.IsAuthenticated)
                {
                    long uid = 0;
                    long.TryParse(User.Identity.GetUserId(), out uid);
                    if (uid > 0)
                    {
                        property.UserType = BLayer.User.GetRole(uid);
                    }
                }

                property.Accommodations.Accommodations = Accommodationss;
                property.Rating = Ratings;

            }
            catch (Exception ex)
            {

            }
            return View("GDSIndex", property);
            //  return View("Index", property);
        }

        public async Task<ActionResult> AmadeusPropertyDetailsConfirm(long id, string start, string end, bool ConfirmDates, Models.SearchParamModel data = null)
        {
            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();

            List<CLayer.Property> objDescriptions = BLayer.Property.GetAllGDSPropertyDescriptionsWithOutData();
            List<CLayer.Property> objTitles = BLayer.Property.GetAllGDSPropertyTitlesWithOutData();

            TempData["DetailUrl"] = Request.Url.AbsoluteUri;
            TempData.Keep("DetailUrl");
            string PropertyAddress = string.Empty;

            string gdscurrencycode = Convert.ToString(Request["gdscurrencycode"]);
            decimal gdsrateconversion = Convert.ToDecimal(Request["gdsrateconversion"]); ;

            DateTime startss = DateTime.Parse(start);
            DateTime endss = DateTime.Parse(end);
            StringBuilder Description = new StringBuilder();
            var startt = startss.ToString("yyyy-MM-dd");
            var endd = endss.ToString("yyyy-MM-dd");

            Models.PropertyModel property = new Models.PropertyModel();
            List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
            CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();

            List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();

            try
            {
                DataTable dtHoelCode = BLayer.Property.GetHotelIDFrompropertyid(id);
                var hotelcode = dtHoelCode.Rows[0]["Hotel_Id"].ToString();


                var ChainCode = "HS";
                var AgeQualifyingCode = "10";
                var count = "2";
                property.PropertyId = id;
                property.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;

                string result = HotelMultiSingleAvailability(hotelcode, startt, endd);

                Serializer ser1 = new Serializer();
                CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();

                //try
                //{

                //    HotelResult = ser1.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                //}
                //catch (Exception ex)
                //{
                //    try
                //    {
                //        HotelResultAdv = ser1.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                //    }
                //    catch (Exception ex1)
                //    {
                //        HotelResultAdvFirst = ser1.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                //    }

                //}



                #region property details update

                long AmadeusPropertyID = 0;
                string CityName = string.Empty;

                var HotelItems = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();
                SearchController objSearch = new SearchController();


                long StateID = 0;
                string StateName = "";
                string City = Convert.ToString(Request["Destination"]);
                int CountryID = 0;
                long CityId = 0;
                long OwnerID = 0;
                int GDSStateID = 0;
                int GDSCityID = 0;
                string GDStateName = "";
                string Destination = Convert.ToString(Request["Destination"]);

                #region country and destination
                if (City != null && City != "")
                {
                    City = Common.Utils.SecureInputString(City); //srch.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");
                    Session["GDSCountry"] = APIUtility.GetGDSCountry(City);
                    string CountryName = Convert.ToString(Session["GDSCountry"]);

                    if (CountryName.ToUpper() == "INDIA")
                    {
                        CountryName = "";
                        Session["GDSCountry"] = "";
                    }
                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(CountryName);
                    int GDSCountryCount = (CountryName != "") ? GDSCountryList.Count : 0;
                    string sDestination = string.Empty;
                    if (GDSCountryCount > 0)
                    {
                        sDestination = APIUtility.GetGDSDestination(City);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        sDestination = APIUtility.GetGDSDestination(City);

                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (CountryName == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }

                        }
                    }

                }
                #endregion

                OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    StateID = BLayer.State.GetStateID(Destination).StateId;
                    StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                    CityId = BLayer.City.GetCityID(City).CityId;
                    CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;

                }
                else
                {
                    GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), Destination);
                    GDStateName = BLayer.State.Get(GDSStateID).Name;
                    GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                    CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                }

                #region Hotel update

                if (HotelResult.Body != null)
                {

                    if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        Session["GDSCurrencyConversion"] = objCurrencyConversion;

                        gdscurrencycode = objCurrencyConversion.SourceCurrencyCode;
                        gdsrateconversion = objCurrencyConversion.RateConversion;

                        Session["GDSRateConversion"] = gdsrateconversion;
                        Session["GDSCurrencyCode"] = gdscurrencycode;

                    }
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;

                    }


                    if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                    {

                        List<string> HotelIDS = new List<string>();

                        foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                        {
                            HotelItems = item;
                            Models.PropertyModel objData = new Models.PropertyModel();
                            objData.PropertyId = 0;
                            objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());


                            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            objData.OwnerId = OwnerID;

                            objData.CityName = Destination;
                            CityName = Destination;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.State = Convert.ToInt32(StateID);
                                objData.StateName = StateName;
                                objData.City = CityName;
                                objData.CityId = Convert.ToInt32(CityId);
                            }
                            else
                            {

                                objData.State = GDSStateID;
                                objData.StateName = GDStateName;
                                objData.City = objData.CityName;
                                objData.CityId = GDSCityID;
                            }

                            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                            objData.Country = CountryID;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                gdscurrencycode = "INR";
                                gdsrateconversion = 0;
                                Session["GDSRateConversion"] = "0";
                                Session["GDSCurrencyCode"] = "INR";
                            }
                            else
                            {
                                objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                            }

                            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                            {
                                objData.Location = objData.CityName + ", " + objData.StateName;
                                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                objData.Location = objData.Location + ", " + objData.CountryName;
                            }
                            string FullAddress = string.Empty;
                            List<string> LocationList = new List<string>();
                            LocationList.Add(objData.CityName);
                            LocationList.Add(objData.StateName);
                            LocationList.Add(objData.ZipCode);
                            LocationList.Add(objData.CountryName);
                            foreach (string itemLoc in LocationList)
                            {
                                if (!string.IsNullOrEmpty(itemLoc))
                                {
                                    FullAddress = FullAddress + itemLoc + ",";
                                }
                            }
                            objData.Location = FullAddress.TrimEnd(',');
                            property.Location = objData.Location;
                            property.Address = APIUtility.FirstLetterToUpperCase(objData.Address + " " + property.Location);
                            //     PropertyAddress = property.Address;
                            PropertyAddress = APIUtility.ReplaceFirstOccurrence(property.Address, Destination, "");
                            PropertyAddress = APIUtility.ReplaceFirstOccurrence(PropertyAddress, objData.CountryName, "").Replace(",,", ",");


                            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            objData.HotelId = item.BasicPropertyInfo.HotelCode;


                            AmadeusPropertyID = await objSearch.SaveAmadeus_Property(objData);



                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            //   RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {

                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                                for (int i = 0; i < RoomStayRPHList.Length; i++)
                                {

                                    var RoomStays = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                    RoomStayItemList = RoomStays;
                                    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();
                                    AccomodationId = 0;
                                    foreach (var Accitem in objAcc.Where(x => x.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                    {
                                        //if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                        //{
                                        //    AccomodationId = Accitem.AccommodationId;
                                        //    break;
                                        //}
                                        //if (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode)
                                        //{
                                        AccomodationId = Accitem.AccommodationId;
                                        break;
                                        //; }
                                    }

                                    if (AccomodationId == 0)
                                    {

                                        foreach (var RoomStayItem in RoomStays)
                                        {
                                            dataAccommodation.AccommodationId = AccomodationId;
                                            dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                            dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                            dataAccommodation.PropertyId = AmadeusPropertyID;
                                            dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                            dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                            if (RoomStayItem.RoomTypes != null)
                                            {
                                                dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                                dataAccommodation.RoomTypeDescription = APIUtility.RoomTypeDescriptionFirst(dataAccommodation.RoomTypeCode);
                                            }



                                            dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                            if (RoomStayItem.RoomRates != null)
                                            {
                                                dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                                dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                                dataAccommodation.Description = RoomStayItem.RoomRates.RoomRate.RoomRateDescription.Text[0].ToString();
                                            }
                                            if (RoomStayItem.RatePlans != null)
                                            {
                                                dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                            }


                                            objSearch.AccommodationSave(dataAccommodation);


                                        }
                                    }

                                }
                                RoomStaysResultLists = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, gdscurrencycode, gdsrateconversion);
                            }

                        }

                    }



                    #endregion

                }
                else if (HotelResultAdvFirst.Body != null)
                {

                    if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        Session["GDSCurrencyConversion"] = objCurrencyConversion;

                        gdscurrencycode = objCurrencyConversion.SourceCurrencyCode;
                        gdsrateconversion = objCurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = gdsrateconversion;
                        Session["GDSCurrencyCode"] = gdscurrencycode;
                    }
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;
                    }



                    if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                    {

                        foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                        {
                            HotelItemAdvFirst = item;
                            Models.PropertyModel objData = new Models.PropertyModel();
                            objData.PropertyId = 0;
                            objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            objData.OwnerId = OwnerID;

                            objData.CityName = Destination;
                            CityName = Destination;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.State = Convert.ToInt32(StateID);
                                objData.StateName = StateName;
                                objData.City = CityName;
                                objData.CityId = Convert.ToInt32(CityId);
                            }
                            else
                            {

                                objData.State = GDSStateID;
                                objData.StateName = GDStateName;
                                objData.City = objData.CityName;
                                objData.CityId = GDSCityID;
                            }

                            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                            objData.Country = CountryID;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.CountryName = BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                gdscurrencycode = "INR";
                                gdsrateconversion = 0;
                                Session["GDSRateConversion"] = "0";
                                Session["GDSCurrencyCode"] = "INR";
                            }
                            else
                            {
                                objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                            }

                            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                            {
                                objData.Location = objData.CityName + ", " + objData.StateName;
                                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                objData.Location = objData.Location + ", " + objData.CountryName;
                            }

                            string FullAddress = string.Empty;
                            List<string> LocationList = new List<string>();
                            LocationList.Add(objData.CityName);
                            LocationList.Add(objData.StateName);
                            LocationList.Add(objData.ZipCode);
                            LocationList.Add(objData.CountryName);
                            foreach (string itemLoc in LocationList)
                            {
                                if (!string.IsNullOrEmpty(itemLoc))
                                {
                                    FullAddress = FullAddress + itemLoc + ",";
                                }
                            }
                            objData.Location = FullAddress.TrimEnd(',');
                            property.Location = objData.Location;
                            property.Address = APIUtility.FirstLetterToUpperCase(objData.Address + " " + property.Location);
                            //   PropertyAddress = property.Address;
                            PropertyAddress = APIUtility.ReplaceFirstOccurrence(property.Address, Destination, "");
                            PropertyAddress = APIUtility.ReplaceFirstOccurrence(PropertyAddress, objData.CountryName, "").Replace(",,", ",");



                            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            objData.HotelId = item.BasicPropertyInfo.HotelCode;

                            AmadeusPropertyID = await objSearch.SaveAmadeus_Property(objData);



                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            //     List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {
                                //BLayer.Accommodation.Accommodation_Amadeus_Delete(AmadeusPropertyID);
                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                                for (int i = 0; i < RoomStayRPHList.Length; i++)
                                {

                                    var RoomStays = HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                    RoomStayItemListAdvFirst = RoomStays;
                                    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();

                                    AccomodationId = 0;
                                    foreach (var Accitem in objAcc.Where(x => x.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                    {
                                        //if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                        //{
                                        //    AccomodationId = Accitem.AccommodationId;
                                        //    break;
                                        //}
                                        //if  (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode)
                                        //{
                                        AccomodationId = Accitem.AccommodationId;
                                        break;
                                        // }
                                    }
                                    if (AccomodationId == 0)
                                    {
                                        foreach (var RoomStayItem in RoomStays)
                                        {
                                            dataAccommodation.AccommodationId = AccomodationId;
                                            dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                            dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                            dataAccommodation.PropertyId = AmadeusPropertyID;
                                            dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                            dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                            if (RoomStayItem.RoomTypes != null)
                                            {
                                                dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                            }


                                            dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                            if (RoomStayItem.RoomRates != null)
                                            {
                                                dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                                dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                            }
                                            if (RoomStayItem.RatePlans != null)
                                            {
                                                dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                            }


                                            objSearch.AccommodationSave(dataAccommodation);
                                        }

                                    }

                                }
                                //       RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Request["gdscurrencycode"]), Convert.ToDecimal(Request["gdsrateconversion"]));
                                RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, gdscurrencycode, gdsrateconversion);
                            }
                        }
                    }
                }

                #endregion



                DataTable details = BLayer.Property.GetAccommodationDetailsFrompropertyid(id);
                if (details.Rows.Count > 0)
                {
                    hotelcode = details.Rows[0]["Hotel_Id"].ToString();
                }


                Serializer ser = new Serializer();
                string hotel = GetGDS_Hotel_Details(hotelcode);



                #region Transaction Log 

                APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), hotel, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelDescriptiveinfo, 0);

                #endregion Transaction log end


                CLayer.GDSBookingDetails.Envelope ss = new CLayer.GDSBookingDetails.Envelope();
                ss = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);

                CLayer.GDSBookingDetailsAdv.Envelope ssAdv = new CLayer.GDSBookingDetailsAdv.Envelope();


                var DescriptionItemList = objDescriptions.Where(x => x.HotelID == hotelcode).ToList();

                DataTable dtHotel = BLayer.Property.GetHotelFormattedDescription(id);
                string FormattedDescription = string.Empty;
                int Ratings = 0;
                if (dtHotel.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtHotel.Rows)
                    {
                        FormattedDescription = Convert.ToString(dr["FormattedDescription"]);
                        Ratings = Convert.ToInt32(dr["Rating"]);
                    }
                }
                if (string.IsNullOrEmpty(FormattedDescription))
                {
                    FormattedDescription = GDSProcess.GDSFormatDescription.GetFormattedDescription(hotel);
                    Ratings = GDSProcess.GDSFormatDescription.StarRatings;
                    BLayer.Property.GDSUpdatePropertyDescriptionFormatted(id, FormattedDescription, GDSProcess.GDSFormatDescription.StarRatings, "");
                }
                int LocalStarRating = APIUtility.GetStarRating(hotel);
                if (LocalStarRating > 0)
                {
                    BLayer.Property.GDSUpdatePropertyStarRatings(id, LocalStarRating);
                    Ratings = LocalStarRating;
                }
                if (!string.IsNullOrEmpty(FormattedDescription))
                {
                    property.Description = FormattedDescription;

                    property.FormattedDescription = FormattedDescription;
                }
                else
                {
                    if (ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo != null)
                    {
                        var description = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;

                        if (description != null)
                        {
                            var descriptionItem = description.MultimediaDescriptions.MultimediaDescription;

                            var Descriptions = from order in descriptionItem
                                               where order.TextItems != null
                                               select order;
                            string HotelDescription = string.Empty;
                            foreach (var desc in Descriptions)
                            {
                                if (desc.InfoCode != null)
                                {
                                    foreach (var datas in desc.TextItems.TextItem.Description)
                                    {


                                        HotelDescription = HotelDescription + datas.__Text + "#256#";

                                    }
                                }
                                //    HotelDescription = HotelDescription + "<br>";

                            }
                            Description.Append(HotelDescription);

                            if (DescriptionItemList != null)
                            {
                                if (DescriptionItemList.Count > 0)
                                {
                                    BLayer.Property.GDSUpdatePropertyDescription(id, Description.ToString());
                                }
                            }
                            property.Description = Description.Replace("-Property description-", "").ToString();
                            int pLength = property.Description.Trim().Length - 1;
                            property.Description = property.Description.Trim().Substring(0, 1).ToUpper().ToString() + property.Description.Trim().Substring(1, pLength);

                        }
                    }
                }








                var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var SecurityToken = HotelResult.Header.Session.SecurityToken;
                var SessionId = HotelResult.Header.Session.SessionId;
                var SequenceNumber = HotelResult.Header.Session.SequenceNumber.ToString();

                //Roomstayresult
                //List<CLayer.RoomStaysResult> RoomStaysResultList = null;
                //RoomStaysResultList = null;
                //foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                //{
                //    HotelItem = item;

                //    string RoomStayRPH = item.RoomStayRPH;
                //    RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                //    if (!string.IsNullOrEmpty(RoomStayRPH))
                //    {
                //        string[] RoomStayRPHList1 = RoomStayRPH.Split(' ');

                //        RoomStaysResultList = SearchController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList1, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                //    }
                //}
                //Roomstayresult end

                foreach (DataRow dt in details.Rows)
                {
                    Accommodationsss = new CLayer.Accommodation();
                    var BookingCode = dt["BookingCode"].ToString();
                    var RatePlanCode = dt["RatePlanCode"].ToString();
                    var RoomTypeCode = dt["RoomTypeCode"].ToString();
                    long AccommodationID = Convert.ToInt64(dt["accommodationid"]);
                    var RoomStaysitem = RoomStaysResultLists.Where(x => x.BookingCode == BookingCode).ToList();
                    //  var RoomStaysitem = RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList();

                    foreach (var Ritem in RoomStaysitem)
                    {
                        var item = Ritem.RoomRateDescriptionItem;
                        CLayer.RoomRateDescription objRoomRateDescription = new CLayer.RoomRateDescription();
                        objRoomRateDescription.Name = item.Name;
                        Accommodationsss.AccommodationType = objRoomRateDescription.Name;// RoomStaysResultList.Where(x => x.BookingCode == BookingCode).FirstOrDefault().RoomRateDescriptionItem.Name;//.RoomRateDescription.Name;

                        var descriptionItem = Ritem.RoomRateDescriptionItem.Description;

                        string RateTypeDesc = string.Empty;
                        foreach (CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText m in descriptionItem)
                        {
                            RateTypeDesc = RateTypeDesc + m.Value + ".";
                        }
                        Accommodationsss.Description = RateTypeDesc;

                        Accommodationsss.RoomTypeCode = Ritem.RoomTypeCode;

                        Accommodationsss.RoomTypeDecription = (Ritem.RoomTypeCode == "ROH") ? "Run of house*" : APIUtility.GenerateRoomDescription(Ritem.RoomTypeCode);
                        Accommodationsss.RoomTypeDecription = string.IsNullOrEmpty(Accommodationsss.RoomTypeDecription) ? "" : Accommodationsss.RoomTypeDecription.TrimEnd(',');


                        var rate = Ritem.AmountAfterTax;
                        Accommodationsss.Rate = Convert.ToDecimal(rate);
                        Accommodationsss.MaxNoOfChildren = 2;
                        Accommodationsss.MaxNoOfPeople = 3;
                        Accommodationsss.MaxNoOfPeople = 1;
                        Accommodationsss.AccommodationCount = 3;
                        Accommodationsss.AccommodationId = AccommodationID;
                        Accommodationsss.MealPlan = (Ritem.IsMealPlanBreakfast) ? "Breakfast" : "";
                        Accommodationsss.CommissionPercent = APIUtility.fnNumberwithoutzero(Convert.ToString(Ritem.CommissionPercent)) + "%";

                        Accommodationss.Add(Accommodationsss);
                    }

                }
                //Find images for property

                long imageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                #region UPDATE PROPERTY IMAGES
                long ImageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                if (ImageCount < 1)
                {
                    // BLayer.Property.DeleteGDSPropertyImages(id);
                    List<CLayer.PropertyFiles> pictlist = GetGDSImages(hotel, id);
                    if (pictlist.Count > 0)
                    {
                        property.Pictures.Pictures = pictlist.ToList();
                    }
                }
                else
                {
                    List<string> images = BLayer.Property.GetGDSHotelAllImages(id);
                    List<CLayer.PropertyFiles> picturelist = new List<CLayer.PropertyFiles>();
                    CLayer.PropertyFiles picture = null;
                    foreach (string item in images)
                    {
                        picture = new CLayer.PropertyFiles();
                        picture.FileName = item;
                        picture.PropertyId = id;
                        picturelist.Add(picture);
                    }
                    property.Pictures.Pictures = picturelist.ToList();
                }
                //}


                #endregion
                var ssw = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;





                if (ssw != null)
                {

                    var sswList = ssw;

                }


                CLayer.GDSBookingDetails.Envelope GDSBookingDetails = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);
                property.Title = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelName;
                DateTime starts = DateTime.Parse(start);
                DateTime ends = DateTime.Parse(end);
                property.Filter.HiddenCheckIn = starts;
                property.Filter.HiddenCheckOut = ends;
                property.Filter.Adults = data.Adults;
                property.Filter.Bedrooms = data.Bedrooms;
                property.Filter.Children = data.Children;

                property.Filter.Destination = CityName;
                property.Filter.CheckIn = starts;
                property.Filter.CheckOut = ends;



                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses != null)
                {
                    property.ZipCode = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.PostalCode;
                    property.Address = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.AddressLine;
                    property.StateName = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                    property.City = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                    property.Location = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;

                }

                property.PropertyId = id;

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails != null)
                {
                    property.Email = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails.Email.ToString();
                }

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones != null)
                {
                    if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").Count() > 0)
                    {
                        property.Phone = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").FirstOrDefault().PhoneNumber;

                    }
                    if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").Count() > 0)
                    {
                        property.Mobile = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").FirstOrDefault().PhoneNumber;
                    }
                    if ((!string.IsNullOrEmpty(property.Phone)) || (!string.IsNullOrEmpty(property.Mobile)) || (!string.IsNullOrEmpty(property.Email)))
                    {
                        BLayer.Property.GDSUpdatePropertyContactNumbers(id, property.Phone, property.Mobile, property.Email);
                    }
                }

                property.Address = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(PropertyAddress.ToLower());
                property.HotelId = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelCode;
                if (User.Identity.IsAuthenticated)
                {
                    long uid = 0;
                    long.TryParse(User.Identity.GetUserId(), out uid);
                    if (uid > 0)
                    {
                        property.UserType = BLayer.User.GetRole(uid);
                    }
                }

                property.Accommodations.Accommodations = Accommodationss;
                property.Rating = Ratings;

            }
            catch (Exception ex)
            {

            }
            return View("GDSIndex", property);
            //  return View("Index", property);
        }

        public static List<CLayer.RoomStaysResult> GetRoomStays(CLayer.HotelAvailability.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();

            for (int i = 0; i < pRPH.Length; i++)
            {
                var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                if (RoomStayItem[0].RoomRates != null)
                {
                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Rates.Rate.Base.AmountBeforeTax);
                    objRoomStay.AmountBeforeTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax);

                    objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                    objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                    objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);


                    CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                    ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

                    ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
                    objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;
                }

                if (RoomStayItem[0].RatePlans != null)
                {
                    objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
                    if (RoomStayItem[0].RatePlans.RatePlan.MealsIncluded != null)
                    {
                        objRoomStay.IsMealPlanBreakfast = RoomStayItem[0].RatePlans.RatePlan.MealsIncluded.Breakfast > 0 ? true : false;
                    }
                    if (RoomStayItem[0].RatePlans.RatePlan.Commission != null)
                    {
                        objRoomStay.CommissionStatusType = RoomStayItem[0].RatePlans.RatePlan.Commission.StatusType;
                        objRoomStay.CommissionPercent = RoomStayItem[0].RatePlans.RatePlan.Commission.Percent;
                    }


                }
                if (RoomStayItem[0].RoomTypes != null)
                {
                    objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
                    objRoomStay.RoomTypeCode = RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
                }



                RoomStaysResultList.Add(objRoomStay);

            }
            return RoomStaysResultList;
        }

        public string GetGDS_priceing_Details(string SecurityToken, string SessionId, string SequenceNumber, string RoomTypeCode, string hotelcode, string RatePlanCode, string BookingCode, string Start, string end, string ChainCode, string AgeQualifyingCode, string count, long id)
        {
            string soapResult = string.Empty;

            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                                                                                      //  var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/OTA_HotelDescriptiveInfoRQ_07.1_1A2007A";
                                                                                      // var _url = "https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = "http://webservices.amadeus.com/Hotel_EnhancedPricing_2.0";



                string SoapHeader = APIUtility.hotel_descriptiveinfor_Priceing_SetSoapHeaderbody(_url, _action, SecurityToken, SessionId, SequenceNumber);
                string SoapBody = APIUtility.hotel_descriptiveinfor_Priceing_detailsBody(hotelcode, RoomTypeCode, RatePlanCode, BookingCode, Start, end, ChainCode, AgeQualifyingCode, count);

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(SoapEnvelop);

                System.Web.HttpContext.Current.Session["GDSRequest"] = Convert.ToString(soapEnvelopeXml.InnerXml);


                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                webRequest.Proxy = null;
                webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // do something usefull here like update your UI.                // suspend this thread until call is complete. You might want to

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
        public string GetGDS_Hotel_Details(string HotelCode)
        {
            string soapResult = string.Empty;

            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                                                                                      //  var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/OTA_HotelDescriptiveInfoRQ_07.1_1A2007A";
                                                                                      // var _url = "https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = "http://webservices.amadeus.com/OTA_HotelDescriptiveInfoRQ_07.1_1A2007A";


                string SoapHeader = APIUtility.hotel_descriptiveinfor_SetSoapHeader(_url, _action);
                string SoapBody = APIUtility.hotel_descriptiveinfor_detailsbody(HotelCode);

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(SoapEnvelop);

                System.Web.HttpContext.Current.Session["GDSRequest"] = Convert.ToString(soapEnvelopeXml.InnerXml);


                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                webRequest.Proxy = null;
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


        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
#if DEBUG
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
#endif 

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
#if !DEBUG
            webRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 200;
            ServicePointManager.MaxServicePointIdleTime = 2000;
            ServicePointManager.SetTcpKeepAlive(false, 0, 0);
#endif 

            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static XmlDocument CreateSoapEnvelope(string XmlText)
        {
            XmlDocument soapEnvelopeDocument = new XmlDocument();
            soapEnvelopeDocument.LoadXml(string.Format(@"{0}", XmlText));
            return soapEnvelopeDocument;
        }

        private static HttpWebRequest InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
                return webRequest;
            }
        }

        public ActionResult GetAccommodation(int id)
        {
            try
            {
                CLayer.Accommodation data = BLayer.Accommodation.Get(id);
                if (data != null)
                {
                    Models.AccommodationModel result = new Models.AccommodationModel()
                    {
                        AccommodationId = data.AccommodationId,
                        Description = data.Description,
                        Area = data.Area,
                        StayCategoryId = data.StayCategoryId,
                        AccommodationTypeId = data.AccommodationTypeId
                    };
                    result.AccommodationPictures = new Models.AccommodationPicturesModel()
                    {
                        FileId = 0,
                        AccommodationId = data.AccommodationId,
                        PropertyId = data.PropertyId,
                        FileName = "",
                        AccommodationPhoto = null,
                        AccommodationPictures = BLayer.AccommodationFiles.GetOnAccommodation(data.AccommodationId)
                    };
                    return PartialView("_AccommodationGallery", result);
                }
                else
                {
                    return PartialView("_AccommodationGallery", new Models.AccommodationModel());
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return PartialView("_AccommodationGallery", new Models.AccommodationModel());
            }
        }

        [AllowAnonymous]
        public ContentResult CheckAccommodationAvailable(long PropertyId, string BookingData, string CheckIn, string CheckOut, string BookingForSelfValue, string NewBillingForValue)
        {
            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropertyId);

            CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
            if (InventoryAPIType == 4) //Tamarind
            {
                string HotelId = BLayer.Property.GetHotelId(PropertyId);
                string RateCardDetailId = BLayer.Property.GetRateCardDetailId(PropertyId);
                string cityid = BLayer.Property.GetTamrindCityID(PropertyId);
                int CategoryID = 0;

                if (HotelId != null && HotelId != "")
                {
                    List<CLayer.BookingItem> bookitems = new List<CLayer.BookingItem>();
                    string[] BookDat = BookingData.Split('#');

                    foreach (string hh in BookDat)
                    {
                        if (hh != "")
                        {
                            string[] dtdata = hh.Split(',');
                            if (Convert.ToInt32(dtdata[1]) != 0)
                            {
                                CLayer.BookingItem ghgh = new CLayer.BookingItem();
                                ghgh.AccommodationId = Convert.ToInt32(dtdata[0]);
                                ghgh.NoOfAccommodations = Convert.ToInt32(dtdata[1]);
                                ghgh.NoOfAdults = Convert.ToInt32(dtdata[4]);
                                ghgh.NoOfChildren = Convert.ToInt32(dtdata[3]);
                                bookitems.Add(ghgh);
                            }
                        }
                    }

                    List<long> NoinvetAcc = new List<long>();
                    foreach (CLayer.BookingItem bi in bookitems)
                    {
                        CategoryID = BLayer.AccommodationType.GetAccommodationTypeTamCatID(bi.AccommodationId);
                        long AccId = bi.AccommodationId;
                        int acc = bi.NoOfAccommodations;
                        int adult = bi.NoOfAdults;
                        int totaladult = bi.NoOfAdults + bi.NoOfGuests;
                        int Children = bi.NoOfChildren;
                        string RoomId = BLayer.Accommodation.GetRoomId(AccId);

                        //var dt = DateTime.ParseExact(CheckIn, "ddd, dd MMM yyyy hh:mm:ss zzz", CultureInfo.InvariantCulture);
                        srch.HotelID = HotelId;
                        srch.RateCardDetailId = RateCardDetailId;
                        srch.cityid = cityid;
                        srch.CategoryID = CategoryID;
                        srch.CheckIn = DateTime.Now;

                        string result = TamrindInsertCartItem(srch);

                        DataSet lds_auth = new DataSet();
                        lds_auth.ReadXml(GenerateStreamFromString(result));

                        if (lds_auth.Tables[0].Rows[0][0].ToString() != "SUCCESS")
                        {
                            NoinvetAcc.Add(bi.AccommodationId);
                        }
                    }

                    if (NoinvetAcc.Count > 0)
                    {
                        string alertmessage = CheckaccDetailsforalert(NoinvetAcc);
                        return Content(alertmessage);
                    }

                }

            }
            if (InventoryAPIType != 0 && InventoryAPIType != 4 && InventoryAPIType != 5) // MAXIMOJO
            {
                string HotelId = BLayer.Property.GetHotelId(PropertyId);

                if (HotelId != null && HotelId != "")
                {
                    HotelId = HotelId.Trim();
                    List<CLayer.BookingItem> bookitems = new List<CLayer.BookingItem>();
                    string[] BookDat = BookingData.Split('#');
                    foreach (string hh in BookDat)
                    {
                        if (hh != "")
                        {
                            string[] dtdata = hh.Split(',');
                            if (Convert.ToInt32(dtdata[1]) != 0)
                            {
                                CLayer.BookingItem ghgh = new CLayer.BookingItem();
                                ghgh.AccommodationId = Convert.ToInt32(dtdata[0]);
                                ghgh.NoOfAccommodations = Convert.ToInt32(dtdata[1]);
                                ghgh.NoOfAdults = Convert.ToInt32(dtdata[2]) + Convert.ToInt32(dtdata[4]);
                                ghgh.NoOfChildren = Convert.ToInt32(dtdata[3]);
                                bookitems.Add(ghgh);
                            }
                        }
                    }


                    List<long> NoinvetAcc = new List<long>();
                    foreach (CLayer.BookingItem bi in bookitems)
                    {
                        long AccId = bi.AccommodationId;
                        int acc = bi.NoOfAccommodations;
                        int adult = bi.NoOfAdults;
                        int totaladult = bi.NoOfAdults + bi.NoOfGuests;
                        int Children = bi.NoOfChildren;
                        string RoomId = BLayer.Accommodation.GetRoomId(AccId);

                        long Propertyid = BLayer.Accommodation.GetPropertyId(AccId);
                        string QueryKey = HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                        string BookingSessionId = "bs_" + HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                        string BookingRequestId = "br_" + HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                        if (RoomId != "" && RoomId != null)
                        {
                            RoomId = RoomId.Trim();
                            List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> HotelRoomDetailsList = StayBazar.Inventory.MaximojoBook.BookingAvailDeserializeJsonResponse(HotelId, Convert.ToDateTime(CheckIn).ToString("yyyy-MM-dd"), Convert.ToDateTime(CheckOut).ToString("yyyy-MM-dd"), totaladult, Children, acc, QueryKey, BookingSessionId, BookingRequestId);
                            if (!HotelRoomDetailsList.Exists(a => a.roomtype_id == RoomId))
                            {
                                NoinvetAcc.Add(bi.AccommodationId);
                                //return Content("false");
                            }
                        }
                    }
                    if (NoinvetAcc.Count > 0)
                    {
                        string alertmessage = CheckaccDetailsforalert(NoinvetAcc);
                        return Content(alertmessage);
                    }
                }
            }

            return Content("true");
        }
        public string CheckaccDetailsforalert(List<long> accids)
        {
            string accomodations = "";
            string errormessage = "";
            foreach (long acc in accids)
            {
                string accname = BLayer.Accommodation.GetAccommodationTitle(acc);
                accomodations = accomodations + accname + ",";
            }

            accomodations = accomodations.TrimEnd(',');

            if (accids.Count > 1)
            {
                errormessage = accomodations + " are not available for options you have selected";
            }
            else
            {
                errormessage = accomodations + " not available for options you have selected";
            }

            return errormessage;
        }
        private List<CLayer.PropertyFiles> GetGDSImages(string response, long id)
        {
            List<CLayer.PropertyFiles> picturelist = new List<CLayer.PropertyFiles>();
            XmlDocument xmlDoc = new XmlDocument();
            string a = string.Empty;
            //  List<CLayer.PropertyFiles> pictureslist = new List<CLayer.PropertyFiles>();
            try
            {
                // xmlDoc.Load(@"F:\HotelDescriptiveIno.xml");
                xmlDoc.LoadXml(response);
                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XmlNode node = null;
                XmlNode nodeImages = null;
                XmlNode nodeguestImages = null;

                string nodeRoot = "/soapenv:Envelope/soapenv:Body/";
                string nodevalue = "/si:OTA_HotelDescriptiveInfoRS/si:HotelDescriptiveContents/si:HotelDescriptiveContent/si:HotelInfo/si:Descriptions";//"/si:MultimediaDescriptions/si:MultimediaDescription/si:ImageItems/si:ImageItem/si:ImageFormat/si:URL";
                string nodevalueguestroom = "/si:OTA_HotelDescriptiveInfoRS/si:HotelDescriptiveContents/si:HotelDescriptiveContent/si:HotelInfo/si:CategoryCodes/si:CategoryCodes/si:GuestRoomInfo";//si:MultimediaDescriptions/si:MultimediaDescription/si:ImageItems/si:ImageItem/si:ImageFormat/si:URL";

                xmlnsManager.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                xmlnsManager.AddNamespace("awsse", "http://xml.amadeus.com/2010/06/Session_v3");
                xmlnsManager.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");
                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");

                nodeImages = xmlDoc.SelectSingleNode(nodeRoot + nodevalue, xmlnsManager);
                nodeguestImages = xmlDoc.SelectSingleNode(nodeRoot + nodevalueguestroom, xmlnsManager);

                #region MAIN IMAGES
                if (nodeImages != null)
                {
                    XmlNodeList list = nodeImages.SelectNodes("//si:MultimediaDescriptions/si:MultimediaDescription", xmlnsManager);
                    int PictureCount = 0;
                    int nodes = 0;
                    if (list != null)
                    {
                        foreach (XmlNode item in list)
                        {
                            var subItem = item.SelectNodes("//si:ImageItems", xmlnsManager);
                            if (subItem != null)
                            {
                                foreach (XmlNode sItem in subItem)
                                {
                                    IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDateTime(r.Attributes["LastModifyDateTime"].Value));
                                    // IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDecimal(r.Attributes["LastModifyDateTime"].Value));
                                    if (subInnerItem != null)
                                    {
                                        foreach (XmlNode sItem1 in subInnerItem)
                                        {
                                            var subUrlItem = sItem1.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager);
                                            if (subUrlItem != null)
                                            {
                                                PictureCount = subInnerItem.Count();
                                                foreach (XmlNode UItem in subUrlItem)
                                                {
                                                    var UrlItem = UItem.ChildNodes[0].InnerText;  //url
                                                    bool bImageExists = true;
                                                    if (!string.IsNullOrEmpty(UrlItem))
                                                    {
                                                        bImageExists = APIUtility.DoesImageExistRemotely(UrlItem);
                                                    }

                                                    if (bImageExists)
                                                    {
                                                        CLayer.PropertyFiles picture = new CLayer.PropertyFiles();
                                                        picture.FileName = UrlItem;// itemimg.ImageFormat.Where(x => x.DimensionCategory == "E").ToList()[0].URL;
                                                        picture.PropertyId = id;
                                                        picturelist.Add(picture);
                                                        BLayer.Property.GDSSaveImageurl(id, picture.FileName);
                                                    }
                                                    nodes++;
                                                    if (nodes == PictureCount) break;
                                                }
                                            }
                                            if (nodes == PictureCount) break;
                                        }

                                    }
                                    if (nodes == PictureCount) break;
                                }
                            }
                            if (nodes == PictureCount) break;
                        }
                    }

                }
                #endregion


                #region GUESTROOMIMAGES
                if (nodeguestImages != null)
                {
                    XmlNodeList listGuestRoomImages = nodeguestImages.SelectNodes("//si:MultimediaDescriptions/si:MultimediaDescription", xmlnsManager);
                    if (listGuestRoomImages != null)
                    {
                        int PictureCount = 0;
                        int nodes = 0;
                        foreach (XmlNode item in listGuestRoomImages)
                        {
                            var subItem = item.SelectNodes("//si:ImageItems", xmlnsManager);
                            if (subItem != null)
                            {
                                foreach (XmlNode sItem in subItem)
                                {
                                    IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDateTime(r.Attributes["LastModifyDateTime"].Value));
                                    // IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDecimal(r.Attributes["LastModifyDateTime"].Value));
                                    if (subInnerItem != null)
                                    {
                                        foreach (XmlNode sItem1 in subInnerItem)
                                        {
                                            var subUrlItem = sItem1.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager);
                                            if (subUrlItem != null)
                                            {
                                                PictureCount = subInnerItem.Count();
                                                foreach (XmlNode UItem in subUrlItem)
                                                {
                                                    var UrlItem = UItem.ChildNodes[0].InnerText;  //url
                                                    bool bImageExists = true;
                                                    if (!string.IsNullOrEmpty(UrlItem))
                                                    {
                                                        bImageExists = APIUtility.DoesImageExistRemotely(UrlItem);
                                                    }

                                                    if (bImageExists)
                                                    {
                                                        CLayer.PropertyFiles picture = new CLayer.PropertyFiles();
                                                        picture.FileName = UrlItem;// itemimg.ImageFormat.Where(x => x.DimensionCategory == "E").ToList()[0].URL;
                                                        picture.PropertyId = id;
                                                        picturelist.Add(picture);
                                                    }
                                                    nodes++;
                                                    if (nodes == PictureCount) break;
                                                }
                                            }
                                            if (nodes == PictureCount) break;
                                        }

                                    }
                                    if (nodes == PictureCount) break;
                                }
                            }
                            if (nodes == PictureCount) break;
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
            }
            return picturelist;

        }

        public static List<CLayer.RoomStaysResult> GetRoomStays(CLayer.HotelAvailability.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0, bool IsMostPopular = false)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
            try
            {


                for (int i = 0; i < pRPH.Length; i++)
                {
                    var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                    CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                    if (RoomStayItem[0].RoomRates != null)
                    {

                        objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Rates.Rate.Base.AmountBeforeTax);
                        objRoomStay.AmountBeforeTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax);

                        objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                        objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                        // objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                        objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;
                        //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        //{
                        //    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                        if ((RequestedCurrencyCode != objRoomStay.CurrencyCode) && (RequestedCurrencyCode != ""))
                        {
                            objRoomStay.AmountAfterTax = Math.Round(objRoomStay.AmountAfterTax * ConversionRate, 0);

                        }
                        else
                        {
                            objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                        }
                        //}

                        CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                        ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

                        ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
                        objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;


                        objRoomStay.RoomTypeCode = RoomStayItem[0].RoomRates.RoomRate.RoomTypeCode;
                        objRoomStay.RatePlanCode = RoomStayItem[0].RoomRates.RoomRate.RatePlanCode;


                        //CLayer.RoomRateTotal ObjRoomRateTotal = new CLayer.RoomRateTotal();
                        //List<CLayer.RoomRateTotalTaxes> ObjRoomRateTotalTaxesList = new List<CLayer.RoomRateTotalTaxes>();
                        //CLayer.RoomRateTotalTaxes ObjRoomRateTotalTaxes = new CLayer.RoomRateTotalTaxes();
                        //ObjRoomRateTotalTaxes.Type= RoomStayItem[0].RoomRates.RoomRate[-

                    }

                    if (RoomStayItem[0].RatePlans != null)
                    {
                        objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
                        if (RoomStayItem[0].RatePlans.RatePlan.MealsIncluded != null)
                        {
                            objRoomStay.IsMealPlanBreakfast = RoomStayItem[0].RatePlans.RatePlan.MealsIncluded.Breakfast > 0 ? true : false;
                        }

                        if (RoomStayItem[0].RatePlans.RatePlan.Commission != null)
                        {
                            objRoomStay.CommissionStatusType = RoomStayItem[0].RatePlans.RatePlan.Commission.StatusType;
                            objRoomStay.CommissionPercent = RoomStayItem[0].RatePlans.RatePlan.Commission.Percent;
                        }
                    }
                    if (RoomStayItem[0].RoomTypes != null)
                    {
                        objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
                        //  objRoomStay.RoomTypeDescription = APIUtility.GenerateRoomDescription(RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode);
                        // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;

                    }




                    RoomStaysResultList.Add(objRoomStay);

                }
                // return RoomStaysResultList;
            }
            catch (Exception ex)
            {
                RoomStaysResultList = null;
            }
            return RoomStaysResultList;
        }
        public static List<CLayer.RoomStaysResult> GetRoomStaysAdv(CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0, bool IsMostPopular = false)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();

            for (int i = 0; i < pRPH.Length; i++)
            {
                var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                if (RoomStayItem[0].RoomRates != null)
                {

                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Total.AmountBeforeTax);
                    objRoomStay.AmountBeforeTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax);

                    objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                    objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                    objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;
                    //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    //{
                    //    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                    if ((RequestedCurrencyCode != objRoomStay.CurrencyCode) && (RequestedCurrencyCode != ""))
                    {
                        objRoomStay.AmountAfterTax = Math.Round(objRoomStay.AmountAfterTax * ConversionRate, 0);
                    }
                    else
                    {
                        objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                    }
                    //}


                    CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                    ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

                    ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
                    objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;


                    objRoomStay.RoomTypeCode = RoomStayItem[0].RoomRates.RoomRate.RoomTypeCode;
                    objRoomStay.RatePlanCode = RoomStayItem[0].RoomRates.RoomRate.RatePlanCode;



                    //CLayer.RoomRateTotal ObjRoomRateTotal = new CLayer.RoomRateTotal();
                    //List<CLayer.RoomRateTotalTaxes> ObjRoomRateTotalTaxesList = new List<CLayer.RoomRateTotalTaxes>();
                    //CLayer.RoomRateTotalTaxes ObjRoomRateTotalTaxes = new CLayer.RoomRateTotalTaxes();
                    //ObjRoomRateTotalTaxes.Type= RoomStayItem[0].RoomRates.RoomRate[-

                }

                if (RoomStayItem[0].RatePlans != null)
                {
                    objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
                    if (RoomStayItem[0].RatePlans.RatePlan.MealsIncluded != null)
                    {
                        objRoomStay.IsMealPlanBreakfast = RoomStayItem[0].RatePlans.RatePlan.MealsIncluded.Breakfast > 0 ? true : false;
                    }
                    if (RoomStayItem[0].RatePlans.RatePlan.Commission != null)
                    {
                        objRoomStay.CommissionStatusType = RoomStayItem[0].RatePlans.RatePlan.Commission.StatusType;
                        objRoomStay.CommissionPercent = RoomStayItem[0].RatePlans.RatePlan.Commission.Percent;
                    }
                }
                if (RoomStayItem[0].RoomTypes != null)
                {
                    objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
                    //     objRoomStay.RoomTypeDescription = APIUtility.GenerateRoomDescription(RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode);

                    // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
                }




                RoomStaysResultList.Add(objRoomStay);

            }
            return RoomStaysResultList;
        }
        public static string TBOHotelSearch(string hotelcode)
        {
            string tokenid = BLayer.TBO.TokenID();
            //var traceid = Session["TraceID"].ToString();

            CLayer.TBOHotelRoom param1 = new CLayer.TBOHotelRoom
            {
                EndUserIp = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString(),
                TokenId = tokenid,
                TraceId = System.Web.HttpContext.Current.Session["TraceID"].ToString(),
                ResultIndex = 2,
                HotelCode = hotelcode
            };

            string requestdata = Newtonsoft.Json.JsonConvert.SerializeObject(param1);
            string authurl = "http://api.tektravels.com/BookingEngineService_Hotel/hotelservice.svc/rest/GetHotelRoom";

            string responsedata = BLayer.TBO.GetResponse(requestdata, authurl);

            return responsedata;
        }
        public static string TamrindMultiSingleAvailability(Models.SearchParamModel sch)
        {
            TamarindService.TamarindDataServiceClient client = new TamarindService.TamarindDataServiceClient();

            client.ClientCredentials.UserName.UserName = "STAYBAZARXMLTEST";
            client.ClientCredentials.UserName.Password = "STAYBAZARXMLTEST";

            string CityId = BLayer.City.GetTamarindCityID(sch.Destination);

            SearchController hotellist = new SearchController();
            hotellist.TamarindHotelList_Save(CityId);

            var dateAndTime = sch.CheckIn;
            TamarindService.HotelSearchParam HotelParm = new TamarindService.HotelSearchParam();
            HotelParm.AgentMarket = "0";
            HotelParm.AllowOnrequest = Convert.ToBoolean("True");
            HotelParm.CityID = CityId;
            HotelParm.CheckinDate = Convert.ToDateTime(dateAndTime);
            HotelParm.Nights = 1;
            HotelParm.SingleBed = 1;
            HotelParm.DoubleBed = 0;
            HotelParm.TwinBed = 0;
            HotelParm.ExtraBed = 0;
            HotelParm.OriginCountryID = "IN";
            //HotelParm.HotelID = "566";
            HotelParm.HotelID = sch.PropertyId.ToString();
            HotelParm.Rating = "";
            string hotel = client.GetHotels(HotelParm);

            return hotel;
        }
        public static string TamrindInsertCartItem(CLayer.SearchCriteria sch)
        {
            BookingServiceRef.BookingServiceClient client = new BookingServiceRef.BookingServiceClient();

            client.ClientCredentials.UserName.UserName = "STAYBAZARXMLTEST";
            client.ClientCredentials.UserName.Password = "STAYBAZARXMLTEST";

            CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
            BookingServiceRef.CartPaxDetails cartPax = new BookingServiceRef.CartPaxDetails
            {

                CartItemID = 0,
                LeadName = true,
                PaxFirstName = "test",
                PaxID = 0,
                PaxLastName = "test",
                PaxSalutation = "Ms",
                RoomType = "double"

            };

            BookingServiceRef.CartPaxDetails[] test = new BookingServiceRef.CartPaxDetails[] { cartPax };

            BookingServiceRef.CartItem InsCartItem = new BookingServiceRef.CartItem();
            InsCartItem.HotelID = Convert.ToInt32(sch.HotelID);
            InsCartItem.HotelRateCartDetailID = sch.RateCardDetailId;
            InsCartItem.CityID = sch.cityid;
            InsCartItem.CheckInDate = sch.CheckIn;
            InsCartItem.NoOfNights = sch.NoOfNights;
            InsCartItem.BreakFast = false;
            InsCartItem.Lunch = false;
            InsCartItem.Dinner = false;
            InsCartItem.RoomCategoryID = sch.CategoryID;
            InsCartItem.SingleRooms = 1;
            InsCartItem.DoubleRooms = 0;
            InsCartItem.TwinRooms = 0;
            InsCartItem.ExtraBeds = 0;
            InsCartItem.AllowOnrequest = true;
            InsCartItem.Passengers = test;
            InsCartItem.OriginCountryID = "IN";
            string Cart = client.InsertCartItem(InsCartItem);

            return Cart;
        }
    }
}
