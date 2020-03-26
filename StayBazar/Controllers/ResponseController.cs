using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using StayBazar.Common;

namespace StayBazar.Controllers
{
    [AllowAnonymous]
    public class ResponseController : Controller
    {
        #region CustomMethod

        private Models.ResponseModel DummyParse(string orderNo)
        {
            long bookingId = 0;
            Models.ResponseModel result = new Models.ResponseModel();

            //CLayer.Transaction trans = new CLayer.Transaction();
            // string[] kvs;
            string orderId = "";
            //foreach (string parm in aQS)
            //{
            //    kvs = parm.Split('=');
            //    if (kvs[1] != null && kvs[1] != "") kvs[1] = kvs[1].Trim();
            //    switch (kvs[0].Trim())
            //    {
            //        case "ResponseCode":
            //            int i = 1;
            //            int.TryParse(kvs[1], out i);
            //            trans.ResponseCode = i;
            //            break;
            //        case "ResponseMessage":
            //            trans.Notes = kvs[1];
            //            break;
            //        case "DateCreated":
            //            DateTime cr = DateTime.Today;
            //            DateTime.TryParse(kvs[1], out cr);
            //            trans.DateCreated = cr;
            //            break;
            //        case "PaymentID":
            //            trans.PaymentId = kvs[1];
            //            break;
            //        case "Amount":
            //            double amt = 0;
            //            double.TryParse(kvs[1], out amt);
            //            trans.Amount = amt;
            //            break;
            //        case "IsFlagged":
            //            kvs[1] = kvs[1].ToLower();
            //            trans.IsFlagged = (kvs[1] == "yes");
            //            break;
            //        case "TransactionID":
            //            trans.TransactionId = kvs[1];
            //            break;
            //        case "PaymentMethod":
            //            int j;
            //            int.TryParse(kvs[1], out j);
            //            trans.PaymentType = (CLayer.ObjectStatus.PaymentMethod)j;
            //            break;
            //        case "MerchantRefNo":
            //            orderId = kvs[1];
            //            break;
            //    }
            //}
            //     bool alreadyMade = false;
            orderId = orderNo;
            if (orderId != "")
            {
                List<long> ids = BLayer.Bookings.GetBookingsByOrder(orderId);

                if (ids.Count > 1)
                {
                    //It is a modified booking
                    CLayer.Booking bone, btwo, bthree;
                    bone = BLayer.Bookings.GetDetails(ids[0]);
                    btwo = BLayer.Bookings.GetDetails(ids[1]);
                    if (btwo.Status != (int)CLayer.ObjectStatus.BookingStatus.CheckOut)
                    {
                        if (bone.Status == (int)CLayer.ObjectStatus.BookingStatus.CheckOut)
                        {
                            bthree = btwo;
                            btwo = bone;
                            bone = bthree;
                        }
                    }
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, btwo.BookingId);
                    bookingId = bone.BookingId;
                    BLayer.Bookings.MergeAndRemove(bone.BookingId, btwo.BookingId);
                    BLayer.Bookings.UpdateTotals(bookingId);
                }
                else
                {
                    if (ids.Count == 0)
                        bookingId = 0;
                    else
                        bookingId = ids[0];
                }

            }
            //trans.BookingId = bookingId;
            //if (!BLayer.Transaction.IsExist(trans.PaymentId))
            //{
            //    //save transaction
            //    if (bookingId == 0)
            //    {
            //        trans.Notes = Common.Utils.CutString(trans.Notes + "\nMessage:\n" + data, 500);
            //    }
            //    BLayer.Transaction.Save(trans);
            //    //send mails and sms
            //}
            //else
            //    alreadyMade = true;
            //if (trans.ResponseCode != 0)
            //{
            //    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bookingId);
            //    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Failed;
            //    result.BookingId = bookingId;
            //    return result;
            //}
            //else
            //    if (trans.ResponseCode == 0 && trans.IsFlagged)
            //    {
            //        BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
            //        if (!alreadyMade)
            //        { await SendMailsAndSms(bookingId); }
            //        result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Flagged;
            //        result.BookingId = bookingId;
            //        return result;
            //    }
            BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
            //if (!alreadyMade) await SendMailsAndSms(bookingId);
            result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success;
            result.BookingId = bookingId;
            return result;
        }

        public string HandleResponse(string DR)
        {
            string sQS;
            string pwd = BLayer.Settings.GetValue(CLayer.Settings.PAY_SECRET_KEY); // Your EBS Secret key

            DR = DR.Replace(' ', '+');
            sQS = Base64Decode(DR);
            DR = StayBazar.Lib.RC4.Decrypt(pwd, sQS, false);

            return DR;
        }

        private string Base64Decode(string sBase64String)
        {
            byte[] sBase64String_bytes =
            Convert.FromBase64String(sBase64String);
            return UnicodeEncoding.Default.GetString(sBase64String_bytes);
            //return UnicodeEncoding.ASCII.GetString(sBase64String_bytes);
        }

        public string base64Decode(string data)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();

                byte[] todecode_byte = Convert.FromBase64String(data);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception("Error in base64Decode" + e.Message);
            }
        }
        //EBS Payment
        public async Task<Models.ResponseModel> ParseResponse(string data)
        {

            //booking  Request cancelling 
            if (data == null || data == "")
            {


            }


            long bookingId = 0;
            Models.ResponseModel result = new Models.ResponseModel();

            if (data == null || data == "")
            {
                result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Invalid;
                result.BookingId = 0;
                return result;
            }
            data = HandleResponse(data);
            string[] aQS = data.Split('&');
            if (aQS.Length < 10)
            {
                result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Invalid;
                result.BookingId = 0;
                return result;
            }
            CLayer.Transaction trans = new CLayer.Transaction();
            string[] kvs;
            string orderId = "";
            foreach (string parm in aQS)
            {
                kvs = parm.Split('=');
                if (kvs.Length < 2) continue;
                if (kvs[1] != null && kvs[1] != "") kvs[1] = kvs[1].Trim();

                switch (kvs[0].Trim())
                {
                    case "ResponseCode":
                        int i = 1;
                        int.TryParse(kvs[1], out i);
                        trans.ResponseCode = i;
                        break;
                    case "ResponseMessage":
                        trans.Notes = kvs[1];
                        break;
                    case "DateCreated":
                        DateTime cr = DateTime.Today;
                        DateTime.TryParse(kvs[1], out cr);
                        trans.DateCreated = cr;
                        break;
                    case "PaymentID":
                        trans.PaymentId = kvs[1];
                        break;
                    case "Amount":
                        double amt = 0;
                        double.TryParse(kvs[1], out amt);
                        trans.Amount = amt;
                        break;
                    case "IsFlagged":
                        kvs[1] = kvs[1].ToLower();
                        trans.IsFlagged = (kvs[1] == "yes");
                        break;
                    case "TransactionID":
                        trans.TransactionId = kvs[1];
                        break;
                    case "PaymentMethod":
                        int j;
                        int.TryParse(kvs[1], out j);
                        //trans.PaymentType =( CLayer.ObjectStatus.PaymentMethod) j;
                        trans.PaymentType = j;
                        break;
                    case "MerchantRefNo":
                        orderId = kvs[1];
                        break;
                }
            }
            bool alreadyMade = false;

            if (orderId != "")
            {
                List<long> ids = BLayer.Bookings.GetBookingsByOrder(orderId);

                if (ids.Count > 1)
                {
                    //It is a modified booking
                    CLayer.Booking bone, btwo, bthree;
                    bone = BLayer.Bookings.GetDetails(ids[0]);
                    btwo = BLayer.Bookings.GetDetails(ids[1]);
                    if (btwo.Status != (int)CLayer.ObjectStatus.BookingStatus.CheckOut)
                    {
                        if (bone.Status == (int)CLayer.ObjectStatus.BookingStatus.CheckOut)
                        {
                            bthree = btwo;
                            btwo = bone;
                            bone = bthree;
                        }
                    }

                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, btwo.BookingId);
                    bookingId = bone.BookingId;
                    BLayer.Bookings.MergeAndRemove(bone.BookingId, btwo.BookingId);
                    BLayer.Bookings.UpdateTotals(bookingId);

                }
                else
                {
                    if (ids.Count == 0)
                        bookingId = 0;
                    else
                        bookingId = ids[0];
                }

            }

            //booking  Request cancelling 

            if (trans.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
            {
                #region PNR_CANCEL
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Decline, bookingId);
                int InvAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(bookingId));
                string ControlNumber = BLayer.Bookings.GetGDSBookingControlNumber(bookingId);
                if (InvAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                  
                    int OptionCode = (ControlNumber == "") ? 0 : (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize;
                    string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, OptionCode);
                }

                #endregion

                List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(bookingId);
                if (DtBookReq != null)
                {
                    if (DtBookReq.Count > 0)
                    {
                        #region

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
                        #endregion
                    }
                }
            }




            trans.BookingId = bookingId;
            if (!BLayer.Transaction.IsExist(trans.PaymentId))
            {
                //save transaction
                if (bookingId == 0)
                {
                    trans.Notes = Common.Utils.CutString(trans.Notes + "\nMessage:\n" + data, 500);
                }

                if (trans.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
                {
                    trans.Status = CLayer.ObjectStatus.TransactionStatus.Failed;
                }
                else
                {
                    trans.Status = CLayer.ObjectStatus.TransactionStatus.Payment;
                }

                BLayer.Transaction.Save(trans);//commented to avoid duplicate transaction saving

                // Save Booking Details to Booking Payments
                //if (trans.ResponseCode == CLayer.Transaction.TRAN_SUCCESS)
                //{
                //    trans.PayStatus = (int)CLayer.ObjectStatus.PyamentStatus.Success;
                //}
                //else
                //{
                //    trans.PayStatus = (int)CLayer.ObjectStatus.PyamentStatus.Failed;
                //}
                BLayer.Transaction.SavePartialPayment(trans);

                //send mails and sms
            }
            else
                alreadyMade = true;
            long Paymentoption = BLayer.Bookings.GetPaymentoption(bookingId);
            CLayer.ObjectStatus.BookingStatus Status = BLayer.Bookings.GetStatus(bookingId);
            CLayer.ObjectStatus.PartialPaymentStatus PartialPaymentStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
            if (trans.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
            {
                if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment || Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.CorporateCreditBooking)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bookingId);
                }

                else if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                {
                    //booking status

                    if (Status != CLayer.ObjectStatus.BookingStatus.Success && Status != CLayer.ObjectStatus.BookingStatus.BookingRequest)
                    {
                        BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bookingId);
                    }

                    //partialpaymentstatus

                    if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut)
                    {
                        BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentFailed, bookingId);
                    }
                    else if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess)
                    {
                        BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed, bookingId);
                    }
                }

                result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Failed;
                result.BookingId = bookingId;
                return result;

            }
            else
            {
                if (trans.ResponseCode == CLayer.Transaction.TRAN_SUCCESS && trans.IsFlagged)
                {
                    //BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                    //if (!alreadyMade)
                    //{ await SendMailsAndSms(bookingId); }
                    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Flagged;
                    //result.BookingId = bookingId;
                    //return result;
                }
                else
                {
                    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success;
                }
            }

          
            //check all external inventory request successfully booked
            long PropertyId = BLayer.Bookings.GetPropertyId(bookingId);
            bool ExtReqSuccess = false;

            int InventoryAPIType = 0;
         
            InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropertyId);
            if (PropertyId > 0)
            {
                
                if (InventoryAPIType != 0)
                {
                    string HotelId = BLayer.Property.GetHotelId(PropertyId); // hotelid
                    if (HotelId != null && HotelId != "")
                    {
                        List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(bookingId);
                        if (DtBookReq != null)
                        {
                            if (DtBookReq.Count > 0)
                            {
                                #region
                                foreach (CLayer.BookingExternalInventory RoomsReq in DtBookReq)
                                {
                                    if (RoomsReq.BookingStatus == (int)CLayer.BookingExternalInventory.BookingStatusenum.Success)
                                    {
                                        ExtReqSuccess = true;
                                    }
                                    else
                                    {
                                        ExtReqSuccess = false;
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }


            //check is online/offline property
           
            if (bookingId > 0)
            {
                long PId = BLayer.Bookings.GetPropertyId(bookingId);
                int Type = BLayer.Property.GetPropertyInventorytype(PId);

                if (Type == (int)CLayer.ObjectStatus.PropertyInventoryType.Offline)
                {
                    ExtReqSuccess = true;
                }
                if(InventoryAPIType== (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    ExtReqSuccess = true;
                }
            }


            if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment)
            {
                if (!ExtReqSuccess)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, bookingId);
                }
                else
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                }


            }
            else if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            {
                if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut || PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart)
                {
                    if (!ExtReqSuccess)
                    {
                        BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, bookingId);
                    }
                    else
                    {
                        BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                    }

                }

                //else if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                //{
                //    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                //}

            }
            else
            {
                if (!ExtReqSuccess)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, bookingId);
                }
                else
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                }
            }



            if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            {
                if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut || PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart)
                {
                    BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess, bookingId);
                }
                else if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                {
                    BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentSuccess, bookingId);
                }
            }


            //if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            //{
            //    CLayer.ObjectStatus.PartialPaymentStatus PartialPaymentStatusformail = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
            //    if (PartialPaymentStatusformail == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentSuccess)
            //    {
            //        if (!alreadyMade) await SendMailsAndSms(bookingId);
            //    }
            //}
            //else
            //{
            if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            {

                CLayer.ObjectStatus.PartialPaymentStatus NewPartialPaymentStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                CLayer.ObjectStatus.BookingStatus sts = BLayer.Bookings.GetStatus(bookingId);

                if (NewPartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess)
                {
                    if (!ExtReqSuccess)
                    {
                        if (!alreadyMade) await SendMailsAndSms(bookingId);                                   // Booking Request Mail
                    }
                    else
                    {

                        if (!alreadyMade) await SendMailsAndSmsAfterSuccessforExternalBookingReq(bookingId);
                    }


                }
                else if (NewPartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentSuccess)
                {
                    if (sts == CLayer.ObjectStatus.BookingStatus.Success)
                    {
                        if (!alreadyMade) await SendMailsAndSmsAfterSuccess(bookingId);                  // Booking Success  Mail if confirmed else no mail send             
                    }
                }

            }
            else
            {
                if (!ExtReqSuccess)
                {
                    if (!alreadyMade) await SendMailsAndSms(bookingId);   // Booking Request Mail
                }
                else
                {
                    if (!alreadyMade) await SendMailsAndSmsAfterSuccessforExternalBookingReq(bookingId);
                }
            }


            //}
            // result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success;
            result.BookingId = bookingId;
            if (!ExtReqSuccess)
            {
                ViewBag.BookingSuccess = "BookingRequest";
            }
            else
            {
                ViewBag.BookingSuccess = "Success";

            }

            return result;
        }


        //Offline Payment
        public async Task<Models.ResponseModel> ParseOfflineResponse(string data)
        {
            //long OffinePaymentId = 0;

            Models.ResponseModel result = new Models.ResponseModel();

            if (data == null || data == "")
            {
                result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Invalid;
                result.OffinePaymentId = 0;
                return result;
            }
            data = HandleResponse(data);
            string[] aQS = data.Split('&');
            if (aQS.Length < 10)
            {
                result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Invalid;
                result.OffinePaymentId = 0;
                return result;
            }
            CLayer.OfflineTransaction Offlinepaytranc = new CLayer.OfflineTransaction();
            string[] kvs;
            string OfflinePayRefNo = "";
            foreach (string parm in aQS)
            {
                kvs = parm.Split('=');
                if (kvs.Length < 2) continue;
                if (kvs[1] != null && kvs[1] != "") kvs[1] = kvs[1].Trim();
                switch (kvs[0].Trim())
                {
                    case "ResponseCode":
                        int i = 1;
                        int.TryParse(kvs[1], out i);
                        Offlinepaytranc.ResponseCode = i;
                        break;
                    case "ResponseMessage":
                        Offlinepaytranc.Notes = kvs[1];
                        break;
                    case "DateCreated":
                        DateTime cr = DateTime.Today;
                        DateTime.TryParse(kvs[1], out cr);
                        Offlinepaytranc.DateCreated = cr;
                        break;
                    case "PaymentID":
                        Offlinepaytranc.PaymentId = kvs[1];
                        break;
                    case "Amount":
                        double amt = 0;
                        double.TryParse(kvs[1], out amt);
                        Offlinepaytranc.Amount = amt;
                        break;
                    case "IsFlagged":
                        kvs[1] = kvs[1].ToLower();
                        Offlinepaytranc.IsFlagged = (kvs[1] == "yes");
                        break;
                    case "TransactionID":
                        Offlinepaytranc.TransactionId = kvs[1];
                        break;
                    case "PaymentMethod":
                        int j;
                        int.TryParse(kvs[1], out j);
                        //trans.PaymentType =( CLayer.ObjectStatus.PaymentMethod) j;
                        Offlinepaytranc.PaymentType = j;
                        break;
                    case "MerchantRefNo":
                        OfflinePayRefNo = kvs[1];
                        break;
                }
            }
            bool alreadyMade = false;
            long Offids = 0;
            if (OfflinePayRefNo != "")
            {
                Offids = BLayer.OfflinePayment.GetOfflinePayByRefNo(OfflinePayRefNo);
            }
            Offlinepaytranc.OfflinePaymentId = Offids;


            if (!BLayer.OfflinePayment.IsExist(Offlinepaytranc.PaymentId))
            {
                //save transaction
                if (Offids == 0)
                {
                    Offlinepaytranc.Notes = Common.Utils.CutString(Offlinepaytranc.Notes + "\nMessage:\n" + data, 500);
                }
                BLayer.OfflinePayment.SaveOfflineTras(Offlinepaytranc);

                //send mails and sms
            }
            else
                alreadyMade = true;

            if (Offlinepaytranc.ResponseCode != 0)
            {

                BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.failed, Offids);
                result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Failed;
                result.OffinePaymentId = Offids;
                return result;

            }
            else
            {
                if (Offlinepaytranc.ResponseCode == 0 && Offlinepaytranc.IsFlagged)
                {
                    //BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                    //if (!alreadyMade)
                    //{ await SendMailsAndSms(bookingId); }
                    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Flagged;
                    //result.BookingId = bookingId;
                    //return result;
                }
                else
                {
                    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success;
                }
            }
            BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.success, Offids);
            if (!alreadyMade)
            {

                await SendMailsAndSmsForOffline(Offids);

            }
            // result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success;

            result.OffinePaymentId = Offids;


            return result;
        }
        public async Task<bool> SendMailsAndSmsForOffline(long OfflinePayId)
        {
            try
            {
                string message = "";
                Common.Mailer ml = new Common.Mailer();
                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("OffPayConfirmationMail") + OfflinePayId.ToString());
                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                List<CLayer.OfflinePayment> PaymentData = BLayer.OfflinePayment.GetOfflinePaymentDetails2(OfflinePayId);
                msg.To.Add(PaymentData.First().Email);
                string emailids = ConfigurationManager.AppSettings.Get("OfflinePaymentEmailCC");
                if (emailids != "")
                {
                    string[] emails = emailids.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        msg.Bcc.Add(emails[i]);
                    }
                }
                msg.Subject = "Payment Confirmation";
                msg.Body = message;
                msg.IsBodyHtml = true;
                try
                {
                    await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                }
                catch (Exception ex)
                { Common.LogHandler.HandleError(ex); }


            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            return true;
        }

        public async Task<bool> SendMailsAndSms(long bookingId)
        {
            try
            {


                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);

                //// set corporate today booking allow to OFF
                //BLayer.B2B.AllowCBookSamedaybook(0, byUser.UserId);

                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                try
                {
                    //user message send option

                    //Block send message to user

                    //string phone = forUser[0].Mobile;
                    //if (phone == "") phone = forUser[0].Phone;
                    //string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                    //    details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    //bool b = false;
                    //phone = Common.Utils.GetMobileNo(phone);

                    //if (phone != "")
                    //{ b = await Common.SMSGateway.Send(smsmsg, phone); }


                    //supplier message send option

                    string smsmsg = "";
                    string phone = "";
                    bool b = false;

                    smsmsg = Common.SMSGateway.GetSupplierBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                        details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));

                    phone = Common.Utils.GetMobileNo(details.Mobile);
                    //if (phone != "")
                    //{ b = await Common.SMSGateway.Send(smsmsg, phone); }



                    phone = Common.Utils.GetMobileNo(supplier.Mobile);

                    //if (phone != "")
                    //{

                    //    b = await Common.SMSGateway.Send(smsmsg, phone);
                    //}
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
                try
                {

                    //Booking user email send

                    string message = "";

                    Common.Mailer ml = new Common.Mailer();
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();

                    //send mail to user
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CustomerBRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    customerMsg.To.Add(forUser[0].Email);
                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcususer != "")
                    {
                        string[] emails = BccEmailsforcususer.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            customerMsg.Bcc.Add(emails[i]);
                        }
                    }
                    customerMsg.Subject = "Booking Request";
                    customerMsg.Body = message;
                    customerMsg.IsBodyHtml = true;
                    try
                    {
                        await ml.SendMailAsyncForBooking(customerMsg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }





                    //send mail to support

                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupportBRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    msg.To.Add(System.Configuration.ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                    string BccEmailsforcus = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcus != "")
                    {
                        string[] emails = BccEmailsforcus.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }
                    //if (forUser[0].Email != byUser.Email)
                    //{
                    //    msg.CC.Add(byUser.Email);
                    //}

                    //if (rle == CLayer.Role.Roles.CorporateUser)
                    //{
                    //    long cid = BLayer.B2B.GetCorporateIdOfUser(byUser.UserId);
                    //    if (cid > 0)
                    //    {
                    //        string em = BLayer.User.GetEmail(cid);
                    //        if (em != null && em != "")
                    //        {
                    //            msg.CC.Add(em);
                    //        }
                    //    }
                    //}
                    msg.Subject = "Booking Request";
                    msg.Body = message;
                    msg.IsBodyHtml = true;
                    try
                    {
                        await ml.SendMailAsyncForBooking(msg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }


                    //Supplier email send
                    if (supplier.Email != "" || details.PropertyEmail != "")
                    {
                        if (supplier.Email == "")
                        {
                            supplier.Email = details.PropertyEmail;
                            details.PropertyEmail = "";
                        }
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                        supplierMsg.To.Add(supplier.Email);

                        // add bcc only for Suppliercommunications
                        string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                        if (BccEmailsforsupp != "")
                        {
                            string[] emails = BccEmailsforsupp.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                supplierMsg.Bcc.Add(emails[i]);
                            }
                        }

                        supplierMsg.Subject = "Booking Request";
                        supplierMsg.Body = message;

                        if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                        supplierMsg.IsBodyHtml = true;
                        await ml.SendMailAsyncForBooking(supplierMsg, Common.Mailer.MailType.Reservation);
                    }



                    ////for corporate admins
                    //if(rle == CLayer.Role.Roles.CorporateUser)
                    //{
                    //    long cid = BLayer.B2B.GetCorporateIdOfUser(byUser.UserId);
                    //    if(cid > 0 )
                    //    {
                    //        CLayer.User corp = BLayer.User.Get(cid);
                    //        if(corp != null)
                    //        {
                    //            message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CorpIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    //            System.Net.Mail.MailMessage corpMsg = new System.Net.Mail.MailMessage();
                    //            corpMsg.To.Add(corp.Email);
                    //            corpMsg.Subject = "Booking Confirmation";
                    //            corpMsg.Body = message;
                    //            corpMsg.IsBodyHtml = true;
                    //            await ml.SendMailAsync(corpMsg,Common.Mailer.MailType.Reservation);
                    //        }
                    //    }
                    //}
                    //else
                    //    if (rle == CLayer.Role.Roles.Agent)
                    //    {

                    //        if (byUser.UserId > 0)
                    //        {
                    //            CLayer.User corp = BLayer.User.Get(byUser.UserId);
                    //            if (corp != null)
                    //            {
                    //                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CorpIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    //                System.Net.Mail.MailMessage corpMsg = new System.Net.Mail.MailMessage();
                    //                corpMsg.To.Add(corp.Email);
                    //                corpMsg.Subject = "Booking Confirmation";
                    //                corpMsg.Body = message;
                    //                corpMsg.IsBodyHtml = true;
                    //                await ml.SendMailAsync(corpMsg, Common.Mailer.MailType.Reservation);
                    //            }
                    //        }
                    //    }


                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }

        public async Task<bool> SendMailsAndSmsAfterSuccess(long bookingId)
        {

            try
            {
                //send customer email/message               
                //email
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                try
                {
                    string message = "";
                    Common.Mailer ml = new Common.Mailer();
                    //for normal user mail body
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("BConfirmationMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    //guest mail added
                    msg.To.Add(forUser[0].Email);
                    if (forUser[0].Email != byUser.Email)
                    {
                        msg.CC.Add(byUser.Email);
                    }

                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcususer != "")
                    {
                        string[] emails = BccEmailsforcususer.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }

                    //for corporate admins
                    if (rle == CLayer.Role.Roles.CorporateUser)
                    {
                        //  message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CorpIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        long cid = BLayer.B2B.GetCorporateIdOfUser(byUser.UserId);
                        if (cid > 0)
                        {
                            string em = BLayer.User.GetEmail(cid);
                            if (em != null && em != "")
                            {
                                msg.CC.Add(em);
                            }
                        }
                    }
                    msg.Subject = "Booking Confirmation";
                    msg.Body = message;

                    msg.IsBodyHtml = true;

                    try
                    {
                        await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                    //message
                    if (bookingId < 1) return false;
                    if (byUser == null) return false;
                    if (forUser.Count == 0) return false;
                    try
                    {
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                            details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);

                        if (phone != "")
                        {
                            b = await Common.SMSGateway.Send(smsmsg, phone);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }


                    //send supplier email/message 

                    //message
                    if (bookingId < 1) return false;
                    if (byUser == null) return false;
                    if (forUser.Count == 0) return false;
                    try
                    {
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                            details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);

                        if (phone != "")
                        {
                            b = await Common.SMSGateway.Send(smsmsg, phone);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                    //email
                    if (bookingId < 1) return false;
                    try
                    {
                        if (supplier.Email != "" || details.PropertyEmail != "")
                        {
                            if (supplier.Email == "")
                            {
                                supplier.Email = details.PropertyEmail;
                                details.PropertyEmail = "";
                            }
                            message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                            System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                            supplierMsg.To.Add(supplier.Email);
                            supplierMsg.Subject = "Booking Confirmation";
                            supplierMsg.Body = message;

                            string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                            if (BccEmailsforsupp != "")
                            {
                                string[] emails = BccEmailsforsupp.Split(',');
                                for (int i = 0; i < emails.Length; ++i)
                                {
                                    supplierMsg.Bcc.Add(emails[i]);
                                }
                            }


                            if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                            supplierMsg.IsBodyHtml = true;

                            try
                            {
                                await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
                            }
                            catch (Exception ex)
                            {
                                Common.LogHandler.HandleError(ex);
                            }

                        }

                    }


                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }



                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }

        public async Task<bool> SendMailsAndSmsAfterSuccessforExternalBookingReq(long bookingId)
        {

            try
            {
                //send customer email/message               
                //email
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                try
                {
                    string message = "";
                    Common.Mailer ml = new Common.Mailer();
                    //for normal user mail body
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("BConfirmationMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    //guest mail added
                    msg.To.Add(forUser[0].Email);
                    if (forUser[0].Email != byUser.Email)
                    {
                        msg.CC.Add(byUser.Email);
                    }

                    string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcususer != "")
                    {
                        string[] emails = BccEmailsforcususer.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }

                    //for corporate admins
                    if (rle == CLayer.Role.Roles.CorporateUser)
                    {
                        //  message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CorpIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        long cid = BLayer.B2B.GetCorporateIdOfUser(byUser.UserId);
                        if (cid > 0)
                        {
                            string em = BLayer.User.GetEmail(cid);
                            if (em != null && em != "")
                            {
                                msg.CC.Add(em);
                            }
                        }
                    }
                    msg.Subject = "Booking Confirmation";
                    msg.Body = message;

                    msg.IsBodyHtml = true;

                    try
                    {
                        await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                    //message
                    if (bookingId < 1) return false;
                    if (byUser == null) return false;
                    if (forUser.Count == 0) return false;
                    try
                    {
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                            details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);

                        if (phone != "")
                        {
                            b = await Common.SMSGateway.Send(smsmsg, phone);
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }


                    //send supplier email/message 

                    ////message
                    //if (bookingId < 1) return false;
                    //if (byUser == null) return false;
                    //if (forUser.Count == 0) return false;
                    //try
                    //{
                    //    string phone = forUser[0].Mobile;
                    //    if (phone == "") phone = forUser[0].Phone;
                    //    string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                    //        details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    //    bool b = false;
                    //    phone = Common.Utils.GetMobileNo(phone);

                    //    if (phone != "")
                    //    {
                    //        b = await Common.SMSGateway.Send(smsmsg, phone);
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    Common.LogHandler.HandleError(ex);
                    //}

                    ////email
                    //if (bookingId < 1) return false;
                    //try
                    //{
                    //    if (supplier.Email != "" || details.PropertyEmail != "")
                    //    {
                    //        if (supplier.Email == "")
                    //        {
                    //            supplier.Email = details.PropertyEmail;
                    //            details.PropertyEmail = "";
                    //        }
                    //        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                    //        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                    //        supplierMsg.To.Add(supplier.Email);
                    //        supplierMsg.Subject = "Booking Confirmation";
                    //        supplierMsg.Body = message;

                    //        string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                    //        if (BccEmailsforsupp != "")
                    //        {
                    //            string[] emails = BccEmailsforsupp.Split(',');
                    //            for (int i = 0; i < emails.Length; ++i)
                    //            {
                    //                supplierMsg.Bcc.Add(emails[i]);
                    //            }
                    //        }


                    //        if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                    //        supplierMsg.IsBodyHtml = true;

                    //        try
                    //        {
                    //            await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            Common.LogHandler.HandleError(ex);
                    //        }

                    //    }

                    //}


                    //catch (Exception ex)
                    //{
                    //    Common.LogHandler.HandleError(ex);
                    //}



                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }

        #endregion
        //
        // GET: /Response/
        [AllowAnonymous]
        public async Task<ActionResult> Index(string DR)
        {
            try
            {
                return View(await ParseResponse(DR));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(CLayer.ObjectStatus.BookingStatus.Failed);
        }

        [AllowAnonymous]
        public async Task<ActionResult> OfflinePayResponse(string DR)
        {
            try
            {
                return View(await ParseOfflineResponse(DR));
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(CLayer.ObjectStatus.OfflinePyamentStatus.failed);
        }


        //Paypal Payment
        //PayPal Success event

        public async Task<ActionResult> PaySuccess(string token, string PayerID)
        {
            if (token == null || token == "")
            {
                Common.LogHandler.AddLog("Paypal Payment Success Event Token is empty");
                return RedirectToAction("Failed", "Payment");
            }
            if (PayerID == null || PayerID == "")
            {
                Common.LogHandler.AddLog("Paypal Payment Success Event PayerId is empty");
                return RedirectToAction("Failed", "Payment");
            }
            long bookingId = 0;
            Models.ResponseModel result = new Models.ResponseModel();


            CLayer.Transaction trans = new CLayer.Transaction();
            trans.ResponseCode = CLayer.Transaction.TRAN_SUCCESS; //success


            trans.PaymentId = PayerID + "&" + token;

            //trans.TransactionId = PayerID;

            string payToken = token;
            payToken = payToken.Replace("'", "");
            long bkid = BLayer.Bookings.GetBookingId(payToken);
            CLayer.Booking bk = BLayer.Bookings.GetDetails(bkid);
            string orderId = bk.OrderNo;
            if (bk == null)
            {
                bk = new CLayer.Booking() { BookingId = 0, OrderNo = "MISSING" };
            }
            string errorPage = System.Configuration.ConfigurationManager.AppSettings.Get(PaymentController.PAYMENT_FAILED_LINK);
            //DoExpressCheckout - finish paypal payment process
            try
            {


                //load paypal url from settings
                string url = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_TOKEN_URL);

                WebRequest rqst = HttpWebRequest.Create(url);

                // only needed, if you use HTTP AUTH
                //CredentialCache creds = new CredentialCache();
                //creds.Add(new Uri(url), "Basic", new NetworkCredential(this.Uname, this.Pwd));
                //rqst.Credentials = creds;
                //rqst.Method = method;
                rqst.Method = "POST";
                string user, pwd, signature, returnurl, cancelurl;

                user = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_USER);
                pwd = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_PWD);
                signature = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_SIGNAUTRE);
                returnurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_RETURNURL);
                cancelurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_CANCELURL);

                //check payment option
                decimal amount = bk.TotalAmount;
                long paymentoption = BLayer.Bookings.GetPaymentoption(bk.BookingId);
                if (paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                {
                    CLayer.ObjectStatus.PartialPaymentStatus PartialPayStatus = BLayer.Bookings.GetPartialPaymentStatus(bk.BookingId);
                    CLayer.Booking data = BLayer.Bookings.GetDataForPayment(bk.BookingId);
                    if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentFailed || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut)
                    {
                        amount = Math.Round(data.PaymentFirstinstallment);
                    }
                    else if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                    {
                        amount = Math.Round(data.PaymentSecondinstallment);
                    }

                }

                CLayer.Currency cur = BLayer.Currency.Get("USD");

                trans.DollerRate = cur.ConversionRate;
                if (cur != null)
                {
                    amount = Math.Round(amount * cur.ConversionRate, 2, MidpointRounding.AwayFromZero);
                }
                trans.Amount = Convert.ToDouble(amount);
                //doExpresscheckout does not care about amount value
                string postdata = "METHOD=DoExpressCheckoutPayment&VERSION=109.0";
                postdata = postdata + "&USER=" + user + "&PWD=" + pwd + "&SIGNATURE=" + signature;
                postdata = postdata + "&TOKEN=" + Server.UrlEncode(token) + "&PAYERID=" + PayerID;
                postdata = postdata + "&PAYMENTREQUEST_0_AMT=" + amount.ToString("F2") + "&PAYMENTREQUEST_0_CURRENCYCODE=USD";
                //   postdata = postdata + "&RETURNURL=" + Server.UrlEncode(returnurl);
                //  postdata = postdata + "&CANCELURL=" + Server.UrlEncode(cancelurl);
                postdata = postdata + "&PAYMENTREQUEST_0_PAYMENTACTION=Sale";

                if (!String.IsNullOrEmpty(postdata))
                {
                    //rqst.ContentType = "application/xml";
                    rqst.ContentType = "application/x-www-form-urlencoded";
                    //st.ContentType = "text/html; charset=UTF-8";
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(postdata);
                    rqst.ContentLength = byteData.Length;
                    using (Stream postStream = rqst.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                        postStream.Close();
                    }
                }
                ((HttpWebRequest)rqst).KeepAlive = false;
                StreamReader rsps = new StreamReader(rqst.GetResponse().GetResponseStream());
                string strRsps = rsps.ReadToEnd();
                trans.Notes = strRsps;
                //   Debug.WriteLine(strRsps);
                string ack = Common.Utils.GetValueFromResultString("ACK", strRsps);
                string TransId = Common.Utils.GetValueFromResultString("PAYMENTINFO_0_TRANSACTIONID", strRsps);
                trans.TransactionId = TransId;
                ack = ack.Trim().ToLower();
                if (ack != "success") //payment failed?
                {
                    //failed do restore and got to error page
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bk.BookingId);
                    //  BLayer.Bookings.SetTryingGateway(bk.BookingId, CLayer.ObjectStatus.Gateway.PayPal);
                    //   return Redirect(errorPage);
                    trans.ResponseCode = CLayer.Transaction.TRAN_FAILED; //failed
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                //revert booking 
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bk.BookingId);
                return Redirect(errorPage);//read it from web.config
            }

            bool alreadyMade = false;

            if (orderId != "")
            {
                List<long> ids = BLayer.Bookings.GetBookingsByOrder(orderId);

                if (ids.Count > 1)
                {
                    //It is a modified booking
                    CLayer.Booking bone, btwo, bthree;
                    bone = BLayer.Bookings.GetDetails(ids[0]);
                    btwo = BLayer.Bookings.GetDetails(ids[1]);
                    if (btwo.Status != (int)CLayer.ObjectStatus.BookingStatus.CheckOut)
                    {
                        if (bone.Status == (int)CLayer.ObjectStatus.BookingStatus.CheckOut)
                        {
                            bthree = btwo;
                            btwo = bone;
                            bone = bthree;
                        }
                    }
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, btwo.BookingId);
                    bookingId = bone.BookingId;
                    BLayer.Bookings.MergeAndRemove(bone.BookingId, btwo.BookingId);
                    BLayer.Bookings.UpdateTotals(bookingId);
                }
                else
                {
                    if (ids.Count == 0)
                        bookingId = 0;
                    else
                        bookingId = ids[0];
                }

            }



            //BOOKING REQUEST CANCELLATION


            if (trans.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
            {
                List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(bookingId);

                if (DtBookReq != null)
                {
                    if (DtBookReq.Count > 0)
                    {
                        #region
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
                        #endregion
                    }
                }
            }




            trans.BookingId = bookingId;
            if (!BLayer.Transaction.IsExistForPaypal(trans.TransactionId))
            {

                //save transaction
                if (bookingId == 0)
                {
                    trans.Notes = Common.Utils.CutString(trans.Notes + "\nMessage:\nPaid through Paypal", 500);
                }


                if (trans.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
                {
                    trans.Status = CLayer.ObjectStatus.TransactionStatus.Failed;
                }
                else
                {
                    trans.Status = CLayer.ObjectStatus.TransactionStatus.Payment;
                }


                BLayer.Transaction.Save(trans);

                //update gateway type for paypal

                BLayer.Transaction.updategatewaytype(trans.TransactionId);

                // Save Booking Details to Booking Payments
                //if (trans.ResponseCode == CLayer.Transaction.TRAN_SUCCESS)
                //{
                //    trans.PayStatus = (int)CLayer.ObjectStatus.PyamentStatus.Success;
                //}
                //else
                //{
                //    trans.PayStatus = (int)CLayer.ObjectStatus.PyamentStatus.Failed;
                //}
                BLayer.Transaction.SavePartialPayment(trans);

                //send mails and sms
            }
            else
                alreadyMade = true;
            long Paymentoption = BLayer.Bookings.GetPaymentoption(bookingId);


            CLayer.ObjectStatus.BookingStatus Status = BLayer.Bookings.GetStatus(bookingId);
            CLayer.ObjectStatus.PartialPaymentStatus PartialPaymentStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
            if (trans.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
            {

                if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment || Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.CorporateCreditBooking)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bookingId);
                }

                else if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                {
                    //booking status

                    if (Status != CLayer.ObjectStatus.BookingStatus.Success && Status != CLayer.ObjectStatus.BookingStatus.BookingRequest)
                    {
                        BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Failed, bookingId);
                    }

                    //partialpaymentstatus

                    if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut)
                    {
                        BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentFailed, bookingId);
                    }
                    else if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess)
                    {
                        BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed, bookingId);
                    }
                }
                result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Failed;
                result.BookingId = bookingId;
                return View("Index", result);
            }
            else
            {
                if (trans.ResponseCode == CLayer.Transaction.TRAN_SUCCESS && trans.IsFlagged)
                {
                    //BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                    //if (!alreadyMade)
                    //{ await SendMailsAndSms(bookingId); }
                    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Flagged;
                    //result.BookingId = bookingId;
                    //return result;
                }
                else
                {
                    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success;
                }
            }

            //check all external inventory request successfully booked
            long PropertyId = BLayer.Bookings.GetPropertyId(bookingId);
            bool ExtReqSuccess = false;

            if (PropertyId > 0)
            {
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropertyId);
                if (InventoryAPIType != 0)
                {
                    string HotelId = BLayer.Property.GetHotelId(PropertyId); // hotelid
                    if (HotelId != null && HotelId != "")
                    {
                        List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(bookingId);

                        if (DtBookReq != null)
                        {
                            if (DtBookReq.Count > 0)
                            {
                                #region
                                foreach (CLayer.BookingExternalInventory RoomsReq in DtBookReq)
                                {
                                    if (RoomsReq.BookingStatus == (int)CLayer.BookingExternalInventory.BookingStatusenum.Success)
                                    {
                                        ExtReqSuccess = true;
                                    }
                                    else
                                    {
                                        ExtReqSuccess = false;
                                        break;
                                    }
                                }
                                #endregion
                            }
                        }
                    }
                }
            }

            //check is online/offline property
            if (bookingId > 0)
            {
                long PId = BLayer.Bookings.GetPropertyId(bookingId);
                int Type = BLayer.Property.GetPropertyInventorytype(PId);

                if (Type == (int)CLayer.ObjectStatus.PropertyInventoryType.Offline)
                {
                    ExtReqSuccess = true;
                }
            }


            if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.FullPayment)
            {
                if (!ExtReqSuccess)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, bookingId);
                }
                else
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                }
            }
            else if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            {
                if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut || PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart)
                {
                    if (!ExtReqSuccess)
                    {
                        BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, bookingId);
                    }
                    else
                    {
                        BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                    }
                }
                //else if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                //{
                //    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                //}
            }
            else
            {
                if (!ExtReqSuccess)
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.BookingRequest, bookingId);
                }
                else
                {
                    BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);
                }
            }




            if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            {
                if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.CheckOut || PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.Cart)
                {
                    BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess, bookingId);
                }
                if (PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                {
                    BLayer.Bookings.SetPartialPaymentStatus((int)CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentSuccess, bookingId);
                }
            }


            //if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            //{
            //    CLayer.ObjectStatus.PartialPaymentStatus PartialPaymentStatusformail = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
            //    if (PartialPaymentStatusformail == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentSuccess)
            //    {
            //        if (!alreadyMade) await SendMailsAndSms(bookingId);
            //    }
            //}
            //else
            //{

            if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
            {

                CLayer.ObjectStatus.PartialPaymentStatus NewPartialPaymentStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                CLayer.ObjectStatus.BookingStatus sts = BLayer.Bookings.GetStatus(bookingId);

                if (NewPartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess)
                {
                    if (!ExtReqSuccess)
                    {
                        if (!alreadyMade) await SendMailsAndSms(bookingId);                      // Booking Request Mail
                    }
                    else
                    {
                        if (!alreadyMade) await SendMailsAndSmsAfterSuccessforExternalBookingReq(bookingId);
                    }
                }
                else if (NewPartialPaymentStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentSuccess)
                {
                    if (sts == CLayer.ObjectStatus.BookingStatus.Success)
                    {
                        if (!ExtReqSuccess)
                        {
                            if (!alreadyMade) await SendMailsAndSmsAfterSuccess(bookingId);                  // Booking Success  Mail if confirmed else no mail send     
                        }
                        else
                        {
                            if (!alreadyMade) await SendMailsAndSmsAfterSuccessforExternalBookingReq(bookingId);
                        }


                    }

                }

            }
            else
            {
                if (!ExtReqSuccess)
                {
                    if (!alreadyMade) await SendMailsAndSms(bookingId);                         // Booking Request Mail
                }
                else
                {
                    if (!alreadyMade) await SendMailsAndSmsAfterSuccessforExternalBookingReq(bookingId);
                }
            }




            //}
            // result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success;
            result.BookingId = bookingId;
            if (!ExtReqSuccess)
            {
                ViewBag.BookingSuccess = "BookingRequest";
            }
            else
            {
                ViewBag.BookingSuccess = "Success";

            }

            //  return result;
            return View("Index", result);
        }


        public async Task<ActionResult> OfflinePaySuccess(string token, string PayerID)
        {
            if (token == null || token == "")
            {
                Common.LogHandler.AddLog("Paypal Payment Success Event Token is empty");
                return RedirectToAction("Failed", "Payment");
            }
            if (PayerID == null || PayerID == "")
            {
                Common.LogHandler.AddLog("Paypal Payment Success Event PayerId is empty");
                return RedirectToAction("Failed", "Payment");
            }

            CLayer.OfflineTransaction Offlinepaytranc = new CLayer.OfflineTransaction();
            Models.ResponseModel result = new Models.ResponseModel();

            long Offids = BLayer.OfflinePayment.GetOfflinePayByRefNo(token);

            CLayer.OfflinePayment offlinedatas = BLayer.OfflinePayment.GetOfflinePaymentDetails(Offids);


            Offlinepaytranc.OfflinePaymentId = Offids;
            Offlinepaytranc.ResponseCode = CLayer.Transaction.TRAN_SUCCESS; //success
            Offlinepaytranc.PaymentId = PayerID + "&" + token;

            if (offlinedatas == null)
            {
                offlinedatas = new CLayer.OfflinePayment() { OfflinePaymentId = 0, PaymentRefNo = "MISSING" };
            }

            string errorPage = System.Configuration.ConfigurationManager.AppSettings.Get(PaymentController.PAYMENT_FAILED_LINK);
            try
            {

                string url = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_TOKEN_URL);

                WebRequest rqst = HttpWebRequest.Create(url);

                rqst.Method = "POST";
                string user, pwd, signature, returnurl, cancelurl;

                user = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_USER);
                pwd = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_PWD);
                signature = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_SIGNAUTRE);
                //returnurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_RETURNURL);
                //cancelurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_CANCELURL);

                //check payment option
                decimal amount = offlinedatas.Amount;

                CLayer.Currency cur = BLayer.Currency.Get("USD");

                if (cur != null)
                {
                    amount = Math.Round(amount * cur.ConversionRate, 2, MidpointRounding.AwayFromZero);
                }
                Offlinepaytranc.Amount = Convert.ToDouble(amount);
                string postdata = "METHOD=DoExpressCheckoutPayment&VERSION=109.0";
                postdata = postdata + "&USER=" + user + "&PWD=" + pwd + "&SIGNATURE=" + signature;
                postdata = postdata + "&TOKEN=" + Server.UrlEncode(token) + "&PAYERID=" + PayerID;
                postdata = postdata + "&PAYMENTREQUEST_0_AMT=" + amount.ToString("F2") + "&PAYMENTREQUEST_0_CURRENCYCODE=USD";
                postdata = postdata + "&PAYMENTREQUEST_0_PAYMENTACTION=Sale";

                if (!String.IsNullOrEmpty(postdata))
                {
                    rqst.ContentType = "application/x-www-form-urlencoded"; ;
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(postdata);
                    rqst.ContentLength = byteData.Length;
                    using (Stream postStream = rqst.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                        postStream.Close();
                    }
                }
                ((HttpWebRequest)rqst).KeepAlive = false;
                StreamReader rsps = new StreamReader(rqst.GetResponse().GetResponseStream());
                string strRsps = rsps.ReadToEnd();
                Offlinepaytranc.Notes = strRsps;
                string ack = Common.Utils.GetValueFromResultString("ACK", strRsps);
                string TransId = Common.Utils.GetValueFromResultString("PAYMENTINFO_0_TRANSACTIONID", strRsps);
                Offlinepaytranc.TransactionId = TransId;
                ack = ack.Trim().ToLower();
                if (ack != "success")
                {
                    BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.failed, Offids);
                    Offlinepaytranc.ResponseCode = CLayer.Transaction.TRAN_FAILED; //failed
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.failed, Offids);
                return Redirect(errorPage);
            }

            bool alreadyMade = false;


            if (!BLayer.Transaction.IsExistForPaypal(Offlinepaytranc.TransactionId))
            {

                //save transaction
                if (Offids == 0)
                {
                    Offlinepaytranc.Notes = Common.Utils.CutString(Offlinepaytranc.Notes + "\nMessage:\nPaid through Paypal", 500);
                }


                if (Offlinepaytranc.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
                {
                    Offlinepaytranc.Status = CLayer.ObjectStatus.TransactionStatus.Failed;
                }
                else
                {
                    Offlinepaytranc.Status = CLayer.ObjectStatus.TransactionStatus.Payment;
                }

                BLayer.OfflinePayment.SaveOfflineTras(Offlinepaytranc);

            }
            else
            {
                alreadyMade = true;
            }

            if (Offlinepaytranc.ResponseCode != CLayer.Transaction.TRAN_SUCCESS)
            {

                BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.failed, Offids);
                result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Failed;
                result.OffinePaymentId = Offids;
                return View("OfflinePayResponse", result);
            }
            else
            {
                if (Offlinepaytranc.ResponseCode == CLayer.Transaction.TRAN_SUCCESS && Offlinepaytranc.IsFlagged)
                {
                    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Flagged;
                }
                else
                {
                    result.PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success;
                }
            }

            if (!alreadyMade)
            {
                await SendMailsAndSmsForOffline(Offids);
            }
            result.OffinePaymentId = Offids;

            return View("OfflinePayResponse", result);
        }





#if DEBUG
        public ActionResult Test(string orderNo)
        {
            try
            {
                Models.ResponseModel rs = new Models.ResponseModel() { BookingId = 11, PaymentResponse = CLayer.ObjectStatus.GateWayResponse.Success };
                rs = DummyParse(orderNo);
                return View("Index", rs);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
            return View("Index", CLayer.ObjectStatus.BookingStatus.Failed);
        }
#endif
    }
}