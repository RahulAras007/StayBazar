using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class BookingUserController : Controller
    {
        //
        // GET: /BookingUser/

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

        [AllowAnonymous]
        public ActionResult Index(long? BookingUserId)
        {
            Models.SaveBookingUserModel data = null;
            try
            {
                data = new Models.SaveBookingUserModel();
                long userId = 0;
                long.TryParse(User.Identity.GetUserId(), out userId);
                if (BookingUserId > 0)
                {
                    CLayer.Booking Getdata1 = BLayer.Bookings.GetBookedForBookingUserId(BookingUserId.Value);
                    if (Getdata1 != null)
                    {
                        data = new Models.SaveBookingUserModel()
                        {
                            FirstName = Getdata1.FirstName,
                            LastName = Getdata1.LastName,
                            AddressId = Getdata1.AddressId,
                            Address = Getdata1.Address,
                            CountryId = Getdata1.Country,
                            State = Getdata1.State,
                            //CityId = Getdata1.CityId,
                            //City = Getdata1.City,
                            Phone = Getdata1.Phone,
                            Email = Getdata1.Email,
                            ZipCode = Getdata1.ZipCode,
                            Mobile = Getdata1.Mobile

                        };


                        if (Getdata1.City != null && Getdata1.City != "")
                        {
                            data.City = Getdata1.City;
                        }
                        else
                        {
                            if (Getdata1.CityId > 0)
                                data.City = BLayer.City.Get(Getdata1.CityId).Name;
                        }

                        //if (data.State > 0)
                        //{
                        //    List<CLayer.City> cities = null;
                        //    cities = BLayer.City.GetOnState(data.State);
                        //    cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                        //    data.CityList = new SelectList(cities, "CityId", "Name");
                        //}

                        data.LoadPlaces();
                    }

                }
            }catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(data);
        }

        public ActionResult SaveDetails(Models.SaveBookingUserModel data)
        {
            try
            {
                CLayer.Address adr = new CLayer.Address();
                adr.AddressId = data.AddressId;
                adr.AddressType = (int)CLayer.Address.AddressTypes.Normal;
                adr.AddressText = data.Address;
                if (data.City != null && data.City != "")
                {
                    adr.City = data.City;
                }
                else
                {
                    if (data.CityId > 0)
                        adr.City = BLayer.City.Get(data.CityId).Name;
                }
                adr.State = data.State;
                adr.CountryId = data.CountryId;
                adr.Phone = data.Phone;
                adr.Mobile = data.Mobile;
                // adr.UserId = 1;
                adr.CityId = data.CityId;
              //  adr.City = data.City;
                adr.ZipCode = data.ZipCode;               
                
                adr.UserId = 0;
                long id = BLayer.Address.Save(adr);
              
                CLayer.Booking usr = new CLayer.Booking();
                usr.ForBookingUserId = data.BookingUserId;
                usr.FirstName = data.FirstName;
                usr.LastName = data.LastName;
                usr.Email = data.Email;
                usr.Mobile = data.Mobile;
                usr.AddressId = id;
                long userId = GetUserId();
                usr.ByUserId = userId;


                long ForBookingUserId = BLayer.Bookings.SaveBookingFor(usr);//add new for booking user
                long bookingId = BLayer.Bookings.GetCartId(userId);//getbookingId
                BLayer.Bookings.UpdateBooking(ForBookingUserId, bookingId);//update bookingitems ForuserId
                return RedirectToAction("Index", "Booking");
            } catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
         
        }


    }
}

        