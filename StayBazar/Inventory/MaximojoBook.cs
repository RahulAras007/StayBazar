using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace StayBazar.Inventory
{
    public class MaximojoBook
    {

        //Booking hotel availibility
        public static string Maximojo_Hotel_Availabilty()
        {
            string authusepass = GetBase64number();


            string BookAvailUrl = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOHOTELAVAILIBILITY) + BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPARTNERCODE);
            WebRequest maxirqst = HttpWebRequest.Create(BookAvailUrl);
            maxirqst.Method = "POST";
            maxirqst.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authusepass);
            string postdata = "city=kochi";
            postdata = postdata + "&Country=india";
            postdata = postdata + "&start_date=2016-03-05";
            postdata = postdata + "&end_date=2016-03-06";
            postdata = postdata + "&party=" + "[{ #adults# : 1},{ #adults# : 1}]";
            postdata = postdata + "&query_key=3cc343af545749818805dd199a914dee_270320151728_1_15";


            string poststring = postdata.Replace('#', '"');

            if (!String.IsNullOrEmpty(poststring))
            {
                maxirqst.ContentType = "application/x-www-form-urlencoded";
                byte[] byteData = UTF8Encoding.UTF8.GetBytes(poststring);
                maxirqst.ContentLength = byteData.Length;

                using (Stream postStream = maxirqst.GetRequestStream())
                {
                    postStream.Write(byteData, 0, byteData.Length);
                    postStream.Close();
                }
            }
            ((HttpWebRequest)maxirqst).KeepAlive = false;

            StreamReader rsps = new StreamReader(maxirqst.GetResponse().GetResponseStream());
            string Responsejson = rsps.ReadToEnd();
            Debug.WriteLine(Responsejson);
            return Responsejson;
        }

        //Booking availibility
        public static string Maximojo_Booking_Availabilty(string hotel_id, string Start_date, string end_date, int party_adult, int Party_chidren, int Noacc, string query_key, string booking_session_id, string booking_request_id)
        {
            string BookAvailResponsejson = "";
            try
            {
                string authusepass = GetBase64number();

                string BookAvailUrl = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGAVAILIBILITY) + BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPARTNERCODE);
                WebRequest maxirqst = HttpWebRequest.Create(BookAvailUrl);
                maxirqst.Method = "POST";
                maxirqst.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authusepass);

                // no of children

                string gh = "";
                for (int i = 0; i < Party_chidren; i++)
                {
                    gh = gh + "5,";
                }

                gh = gh.TrimEnd(',');

                string partylist = "";

                for (int i = 0; i < Noacc; i++)
                {

                    partylist = partylist + "{ #adults# : " + party_adult + " ,#children# : [ " + gh + " ]} ,";
                }

                if (Noacc == 0)
                {
                    partylist = "{ #adults# : " + party_adult + " ,#children# : [ " + gh + " ]}";
                }

                partylist = partylist.TrimEnd(',');

                string postdata = "hotel={ #hotel_id#: " + "#" + hotel_id + "#" + "}";
                postdata = postdata + "&start_date=" + Start_date;
                postdata = postdata + "&end_date=" + end_date;
                postdata = postdata + "&party=" + "[" + partylist + "]";
                postdata = postdata + "&query_key=" + query_key;
                postdata = postdata + "&booking_session_id=" + booking_session_id;
                postdata = postdata + "&booking_request_id=" + booking_request_id;


                string poststring = postdata.Replace('#', '"');

                if (!String.IsNullOrEmpty(poststring))
                {
                    maxirqst.ContentType = "application/x-www-form-urlencoded";
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(poststring);
                    maxirqst.ContentLength = byteData.Length;

                    using (Stream postStream = maxirqst.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                        postStream.Close();
                    }

                }


                ((HttpWebRequest)maxirqst).KeepAlive = false;

                StreamReader rsps = new StreamReader(maxirqst.GetResponse().GetResponseStream());
                BookAvailResponsejson = rsps.ReadToEnd();
                Debug.WriteLine(BookAvailResponsejson);

            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from  External Booking Submit Request(maximojo.cs,Maximojo_Booking_Availabilty) on ," + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);

            }
            return BookAvailResponsejson;

        }

        //Checking Booking availability
        public static List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> BookingAvailDeserializeJsonResponse(string hotel_id, string Start_date, string end_date, int party_adult, int Party_chidren, int Noacc, string query_key, string booking_session_id, string booking_request_id)
        {
            List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> Roomtypeids = new List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails>();
            try
            {
                #region
                string Rooms_Details = StayBazar.Inventory.MaximojoBook.Maximojo_Booking_Availabilty(hotel_id, Start_date, end_date, party_adult, Party_chidren, Noacc, query_key, booking_session_id, booking_request_id);
                var Bookingdetails = JsonConvert.DeserializeObject<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.RootObject>(Rooms_Details);
                if (Bookingdetails.room_types_array != null)
                {
                    int cout = Bookingdetails.room_types_array.Count;
                    foreach (var ss in Bookingdetails.room_types_array)
                    {
                        var hrdt = new StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails
                        {
                            hotel_id = Bookingdetails.hotel_id,
                            hotel_name = Bookingdetails.hotel_details.name,
                            roomtype_id = ss.partner_data.RoomId,
                            room_name = ss.name,
                            room_desc = ss.description,
                            query_key = Bookingdetails.query_key,
                            final_price_at_bookingamt = ss.final_price_at_booking.amount,
                            final_price_at_bookingamtcurr = ss.final_price_at_booking.currency,
                            final_price_at_checkoutamt = ss.final_price_at_checkout.amount,
                            final_price_at_checkoutamtcurr = ss.final_price_at_checkout.currency,
                            DomainId = ss.partner_data.DomainId,
                            RatePlanId = ss.partner_data.RatePlanId,
                            PromotionId = ss.partner_data.PromotionId
                        };
                        Roomtypeids.Add(hrdt);
                    }

                }

                #endregion
            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from  External Booking Submit Request(Maximojo.cs,BookingAvailDeserializeJsonResponse) on ," + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
            }
            return Roomtypeids;

        }

        //Booking submit with payment details
        public static string MaximojoBook_Booking_Submit_paypaldetails(StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.RootObject data)
        {
            string BookSubResponsejson = "";

            try
            {
                string json = new JavaScriptSerializer().Serialize(data);

                string authusepass = GetBase64number();
                string BookSubmitUrl = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGSUBMIT) + BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPARTNERCODE);
                WebRequest maxirqst = HttpWebRequest.Create(BookSubmitUrl);
                maxirqst.Method = "POST";
                maxirqst.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authusepass);

                if (!String.IsNullOrEmpty(json))
                {
                    maxirqst.ContentType = "application/json";
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(json);
                    maxirqst.ContentLength = byteData.Length;

                    using (Stream postStream = maxirqst.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                        postStream.Close();
                    }

                }
                ((HttpWebRequest)maxirqst).KeepAlive = false;
                StreamReader rsps = new StreamReader(maxirqst.GetResponse().GetResponseStream());
                BookSubResponsejson = rsps.ReadToEnd();
                Debug.WriteLine(BookSubResponsejson);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
            return BookSubResponsejson;
        }

        //Booking submit without payment details
        public static string MaximojoBook_Booking_Submit_Without_paypaldetails(StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.RootObject data)
        {
            string BookSubResponsejson = "";
            try
            {
                string json = new JavaScriptSerializer().Serialize(data);
                string authusepass = GetBase64number();
                string BookSubmitUrl = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGSUBMIT) + BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPARTNERCODE);
                WebRequest maxirqst = HttpWebRequest.Create(BookSubmitUrl);
                maxirqst.Method = "POST";
                maxirqst.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authusepass);

                if (!String.IsNullOrEmpty(json))
                {
                    maxirqst.ContentType = "application/json";
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(json);
                    maxirqst.ContentLength = byteData.Length;

                    using (Stream postStream = maxirqst.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                        postStream.Close();
                    }

                }
                ((HttpWebRequest)maxirqst).KeepAlive = false;

                StreamReader rsps = new StreamReader(maxirqst.GetResponse().GetResponseStream());
                BookSubResponsejson = rsps.ReadToEnd();
                Debug.WriteLine(BookSubResponsejson);

            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from  External Booking Submit Request(Maximojo.cs , MaximojoBook_Booking_Submit_Without_paypaldetails) on ," + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);

            }


            return BookSubResponsejson;
        }

        //Booking cancel
        public static string MaximojoBook_Booking_Cancel(StayBazar.Inventory.Models.MaxBooking_Cancel.RootObject data)
        {

            string BookcancelResponsejson = "";
            try
            {
                string json = new JavaScriptSerializer().Serialize(data);

                string authusepass = GetBase64number();
                string BookSubmitUrl = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGCANCEL) + BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPARTNERCODE);
                WebRequest maxirqst = HttpWebRequest.Create(BookSubmitUrl);
                maxirqst.Method = "POST";
                maxirqst.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authusepass);

                if (!String.IsNullOrEmpty(json))
                {
                    maxirqst.ContentType = "application/json";
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(json);
                    maxirqst.ContentLength = byteData.Length;

                    using (Stream postStream = maxirqst.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                        postStream.Close();
                    }

                }

                ((HttpWebRequest)maxirqst).KeepAlive = false;

                StreamReader rsps = new StreamReader(maxirqst.GetResponse().GetResponseStream());
                BookcancelResponsejson = rsps.ReadToEnd();
                Debug.WriteLine(BookcancelResponsejson);

            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from  External Booking Submit Request(Maximojo.cs , MaximojoBook_Booking_Cancel) on ," + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);

            }
            return BookcancelResponsejson;

        }

        //Booking Verify
        public static string Maximojo_Booking_Verify(string Hotel_Id, string reference_id, string reservation_id)
        {
            string BookVerifyResponsejson = "";
            try
            {
                string authusepass = GetBase64number();
                string BookVerifyUrl = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOBOOKINGVERIFY) + BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPARTNERCODE) + "&hotel_id=" + Hotel_Id + "&reference_id=" + reference_id + "&reservation_id=" + reservation_id;
                WebRequest maxirqst = HttpWebRequest.Create(BookVerifyUrl);
                maxirqst.Method = "POST";
                maxirqst.Headers.Add(HttpRequestHeader.Authorization, "Basic " + authusepass);
                maxirqst.ContentType = "application/x-www-form-urlencoded";
                ((HttpWebRequest)maxirqst).KeepAlive = false;

                StreamReader rsps = new StreamReader(maxirqst.GetResponse().GetResponseStream());
                BookVerifyResponsejson = rsps.ReadToEnd();
                Debug.WriteLine(BookVerifyResponsejson);
            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from  External Booking Submit Request(maximojo.cs , Maximojo_Booking_Verify) on ," + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);

            }
            return BookVerifyResponsejson;
        }


        public static string GetBase64number()
        {
            string UserName = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOUSERNAME);
            string Password = BLayer.Settings.GetValue(CLayer.Settings.MAXIMOJOPASSWORD);
            string CompUsePass = UserName.Trim() + ":" + Password.Trim();

            var encodestring = System.Text.Encoding.UTF8.GetBytes(CompUsePass);
            string OutPut = Convert.ToBase64String(encodestring);

            return OutPut;
        }
    }
}