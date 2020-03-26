using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using System.Net;


namespace StayBazar.Controllers
{
    public class PartialPaymentMailController : Controller
    {


        #region CustomMethods

        public Models.BookingModel LoadVal(long bookingId)
        {
            Models.BookingModel data = new Models.BookingModel();
            //    CLayer.Address wh = BLayer.Bookings.GetBookedByUser(bookingId);
            List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
            if (adr.Count == 0) adr.Add(new CLayer.Address());
            data.OrderedBy = adr[0];
            data.Items = BLayer.BookingItem.GetAllDetails(bookingId);
            CLayer.Booking bdata = BLayer.Bookings.GetDetails(bookingId);
            data.BookingDetails.OrderNo = bdata.OrderNo;
            data.BookingDetails.BookingDate = bdata.BookingDate;
            data.Supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
            data.BookingId = bookingId;
            return data;
        }

        #endregion
        // GET: PartialPaymentMail
        private const string URL_PARTIALPAYMENT = "PartialPaymentLink";

        [AllowAnonymous]
        public async Task<string> Email()
        {
            WebClient wc = new WebClient();
            try
            {
                Models.BookingModel data = new Models.BookingModel();
                List<CLayer.Booking> dt = BLayer.Bookings.GetPartialBookingDetails();
                string url = ConfigurationManager.AppSettings.Get(URL_PARTIALPAYMENT);
                long BookId = 0;
                CLayer.Booking PaymentDetails = null;
                MailMessage mm = new MailMessage();
                mm.Bcc.Add(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                mm.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                Common.Mailer mlr = new Common.Mailer();
                mm.IsBodyHtml = true;
                mm.Subject = "Reminder: Payment pending";
                foreach (CLayer.Booking book in dt)
                {
                    PaymentDetails = book;
                    BookId = book.BookingId;
                    string result = wc.DownloadString(url + BookId);
                    mm.To.Clear();
                    mm.To.Add(book.Email);
                    //mm.ReplyToList.Add("jisbinj@gmail.com");                  
                    mm.Body = result;
                    await mlr.SendMailAsyncWithoutFields(mm);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
            //return View();
        }
        [AllowAnonymous]
        public ActionResult GetPartialBookingDetails(long BookId)
        {
            try
            {
                //CLayer.Booking data = BLayer.Bookings.GetPartialBookDetailsbyBookId(BookId);
                return View("SendMail", LoadVal(BookId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());
        }

        [AllowAnonymous]
        public ActionResult GetPartialBCancelDetails(long BookId)
        {
            try
            {
                // CLayer.Booking data = BLayer.Bookings.GetPartialBCancelDetailsbyBookId(BookId);
                return View("SendMailForCancel", LoadVal(BookId));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new Models.BookingModel());
        }
    }
}
