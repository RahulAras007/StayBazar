using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class ServiceController : Controller
    {
        //public string Index()
        //{
        //    return "I found nothing";
        //}
        public string GetMailSupplierIds(int start,int limit)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
               List<string> ids = BLayer.Service.GetSupplierIDsForMail(DateTime.Today.AddDays(-1),start,limit);
               if(ids.Count>0) sb.Append(String.Join(",", ids));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return sb.ToString();
        }

        public string GetMailSupplierIdsCount()//DateTime from)
        {
            long cnt = 0;
            try
            {
                cnt = BLayer.Service.GetSupplierCountForMail(DateTime.Today.AddDays(-1));
            }
            catch(Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                cnt = 0;
            }
            return cnt.ToString();
        }

        public string GetPropertyEmailNId(long supplierId)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            try
            {
               List<string> ids = BLayer.Property.GetPropertyEmailAndId(supplierId,DateTime.Today.AddDays(-1));
               result.Append(String.Join(",", ids));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return result.ToString();
        }
        // get booklist 
        public string GetPartialPaymentBookedList()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            try
            {
                List<string> ids = BLayer.Service.GetPartialPaymentList();
                result.Append(String.Join(",", ids));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return result.ToString();
        }
        public string GetPartialPaymentBCancellation()
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            try
            {
                List<string> ids = BLayer.Service.GetPartialPaymentBCancellation();
                string[] payData;
                foreach (string canc in ids)
                {
                    payData = canc.Split('#');
                    long BookingId = Convert.ToInt32(payData[0]);
                    BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.BookingCancel, BookingId);
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, BookingId);

                    //Cancel external booking request
                    ExternalBookRequestCancel(BookingId);
                }
                result.Append(String.Join(",", ids));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return result.ToString();
        }

        public ActionResult ExternalBookRequestCancel(long bookingId)
        {
            try
            {
                List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(bookingId);
                foreach (CLayer.BookingExternalInventory reqbook in DtBookReq)
                {
                    var Book_Cancelobj = new StayBazar.Inventory.Models.MaxBooking_Cancel.RootObject
                    {
                        hotel_id = reqbook.hotel_id,
                        reservation_id = reqbook.reservation_id
                    };

                    string ResponseCancel = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Cancel(Book_Cancelobj);

                    StayBazar.Inventory.Models.MaxBooking_Cancel_Response.RootObject Bookingcanceldetails = JsonConvert.DeserializeObject<StayBazar.Inventory.Models.MaxBooking_Cancel_Response.RootObject>(ResponseCancel);

                    //Saving Cancel Details of external booking request

                    CLayer.BookingExternalInventory bookcandt = new CLayer.BookingExternalInventory();

                    bookcandt.BookingExtInvReqId = reqbook.BookingExtInvReqId;
                    bookcandt.BookingId = reqbook.BookingId;
                    bookcandt.reservation_id = reqbook.reservation_id;

                    if (Bookingcanceldetails.status == "Success")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.Success;
                        //UPDATE BOOKING EXTERNAL INVENTORY REQUEST STATUS CHANGE
                    }
                    else if (Bookingcanceldetails.status == "AlreadyCancelled")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.AlreadyCancelled;
                    }
                    else if (Bookingcanceldetails.status == "CannotBeCancelled")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.CannotBeCancelled;
                    }
                    else if (Bookingcanceldetails.status == "UnknownReference")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.UnknownReference;
                    }
                    else if (Bookingcanceldetails.status == "Error")
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.Error;
                    }
                    else
                    {
                        bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.none;
                    }
                    //UPDATE BOOKING STATUS
                    int CacelSts = bookcandt.Cancellation_Status;
                    BLayer.BookingExternalInventory.UpdateCancellationStatus(reqbook.BookingExtInvReqId, CacelSts);
                    bookcandt.Cancellation_Number = Bookingcanceldetails.cancellation_number;
                    if (Bookingcanceldetails.cancelled_date == null) { Bookingcanceldetails.cancelled_date = Convert.ToString(DateTime.Now); }
                    bookcandt.Cancelled_Date = Bookingcanceldetails.cancelled_date;
                    bookcandt.Cancellation_Response = ResponseCancel;
                    BLayer.BookingExternalInventory.SaveBookingCancelResponse(bookcandt);
                }
            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from External Booking Request Cancel ,partial payment cancel by task manager (service , ExternalBookRequestCancel) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
            }
            return null;


        }
    }
}