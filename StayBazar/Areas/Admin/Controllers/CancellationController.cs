using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace StayBazar.Areas.Admin.Controllers
{
    public class CancellationController : Controller
    {
        #region CustomMethods
/*
        public async Task<bool> SendModifyMailsAndSms(long bookingId)
        {
            try
            {
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                CLayer.Booking details = BLayer.Bookings.GetDetails(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                try
                {
                    string phone = forUser[0].Mobile;
                    if (phone == "") phone = forUser[0].Phone;
                    if (phone != "")
                    {
                        string smsmsg = Common.SMSGateway.GetModifyMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToShortDateString(), details.CheckOut.ToShortDateString(), details.PropertyTitle, details.City, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        b = await Common.SMSGateway.Send(smsmsg, phone);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
                try
                {
                    string message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CChangeConfirmMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    Common.Mailer ml = new Common.Mailer();
                    //User Email
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    msg.To.Add(forUser[0].Email);
                    msg.Subject = "Booking Change Confirmation";
                    msg.Body = message;

                    msg.IsBodyHtml = true;
                    await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                    //Supplier Email
                    if (supplier.Email != "" || details.PropertyEmail != "")
                    {
                        if (supplier.Email == "")
                        {
                            supplier.Email = details.PropertyEmail;
                            details.PropertyEmail = "";
                        }
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SChangeConfirmMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                        supplierMsg.To.Add(supplier.Email);
                        supplierMsg.Subject = "Booking Change Confirmation";
                        supplierMsg.Body = message;

                        if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                        supplierMsg.IsBodyHtml = true;
                        await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
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


        public async Task<bool> SendCancMailsAndSms(long bookingId)
        {
            try
            {
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                CLayer.Booking details = BLayer.Bookings.GetDetails(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                try
                {
                    string phone = forUser[0].Mobile;
                    if (phone == "") phone = forUser[0].Phone;
                    if (phone != "")
                    {
                        string smsmsg = Common.SMSGateway.GetCancellationMsg(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.PropertyTitle, details.City, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
                        b = await Common.SMSGateway.Send(smsmsg, phone);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
                try
                {
                    string message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("UCancMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    Common.Mailer ml = new Common.Mailer();
                    //User Email
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    msg.To.Add(forUser[0].Email);
                    msg.Subject = "Booking Cancellation";
                    msg.Body = message;

                    msg.IsBodyHtml = true;
                    await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                    //Supplier Email
                    if (supplier.Email != "" || details.PropertyEmail != "")
                    {
                        if (supplier.Email == "")
                        {
                            supplier.Email = details.PropertyEmail;
                            details.PropertyEmail = "";
                        }
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SCancMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                        supplierMsg.To.Add(supplier.Email);
                        supplierMsg.Subject = "Booking Cancellation";
                        supplierMsg.Body = message;

                        if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                        supplierMsg.IsBodyHtml = true;
                        await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
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

        public Models.ModifyModel ModifyBooking(Models.ModifyModel data)
        {
            CLayer.Booking bookDetails = BLayer.Bookings.GetDetails(data.BookingId);
            long userid = BLayer.Bookings.GetBookedByUserId(data.BookingId);
            CLayer.CancellationData cdata = BLayer.Cancellation.ModifyBooking(data.BookingId, data.CheckIn, data.CheckOut, userid);

            data.NewBookingExists = cdata.NewBookingExist;
            return data;
        }

        public Models.ModifyModel ChangeDatesInfo(Models.ModifyModel data)
        {
            if (!(data.CheckIn > DateTime.Today && data.CheckOut > data.CheckIn))
            {
                data.Failed = true;
                data.Msg = "Invalid Check-In and Check-Out dates";
                return data;
            }
            CLayer.Booking bookDetails = BLayer.Bookings.GetDetails(data.BookingId);
            if (bookDetails == null)
            {
                data.Failed = true;
                data.Msg = "Invalid Booking details";
                return data;
            }
            DateTime checkIn = bookDetails.CheckIn;
            DateTime checkout = bookDetails.CheckOut;

            if (data.CheckIn > checkout || data.CheckOut < checkIn)
            {
                data.Failed = true;
                data.Msg = "Please cancel the current booking and make a new booking!";
                return data;
                //If you go forward
                //check avaliablity and mark it!!
                //cancel the current one
                //forward it to payment gateway
            }
            else
            {
                long userid = BLayer.Bookings.GetBookedByUserId(data.BookingId);
                CLayer.CancellationData cdata = BLayer.Cancellation.GetModifyInfo(data.BookingId, data.CheckIn, data.CheckOut, userid);

                data.CancellationCharge = (double)cdata.TotalCancellationCharge;
                data.ServiceCharge = cdata.ServiceCharge;
                CLayer.Booking bdet = BLayer.Bookings.GetDetails(BLayer.Bookings.GetCartId(userid));

                if (bdet == null)
                    data.NewBookingAmount = 0;
                else
                    data.NewBookingAmount = (double)bdet.TotalAmount;

                data.CurrentTotalAmount = (double)bookDetails.TotalAmount;
                data.Postback = true;
            }

            return data;
        }

        public Models.CancelModel CancelAllTransactions(long bookingId)
        {
            long userid = 0;
            long.TryParse(User.Identity.GetUserId(), out userid);
            CLayer.Role.Roles urole = BLayer.User.GetRole(userid);
            Models.CancelModel cancel = new Models.CancelModel();
            cancel.CancellationCharge = 0;

            if (!(urole == CLayer.Role.Roles.Administrator || urole == CLayer.Role.Roles.Staff))
            {
                if (userid != BLayer.Bookings.GetBookedByUserId(bookingId))
                {
                    cancel.CanCancel = false;
                    return cancel;
                }
            }
            cancel.CanCancel = true;
            // BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, bookingId);
            BLayer.Transaction.CancelAllTransactions(bookingId);
            return cancel;
        }
        public Models.CancelModel CancelBookingItem(long bookingItemId)
        {
            long userid = 0;
            long.TryParse(User.Identity.GetUserId(), out userid);
            CLayer.Role.Roles urole = BLayer.User.GetRole(userid);
            Models.CancelModel cancel = new Models.CancelModel();
            cancel.CancellationCharge = 0;
            if (!(urole == CLayer.Role.Roles.Administrator || urole == CLayer.Role.Roles.Staff))
            {
                if (userid != BLayer.BookingItem.GetBookedUserId(bookingItemId))
                {
                    cancel.CanCancel = false;
                    return cancel;
                }
            }
            cancel.CanCancel = true;
            BLayer.Transaction.CancelAccommodation(bookingItemId);
            return cancel;
        }
        */

        public async Task<bool> SendModifyMailsAndSms(long bookingId)
        {
            try
            {
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                try
                {
                    string phone = forUser[0].Mobile;
                    if (phone == "") phone = forUser[0].Phone;
                    string smsmsg = Common.SMSGateway.GetModifyMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"), details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    bool b = false;
                    phone = Common.Utils.GetMobileNo(phone);
                    if (phone != "")
                    { b = await Common.SMSGateway.Send(smsmsg, phone); }
                    phone = Common.Utils.GetMobileNo(details.Mobile);

                    smsmsg = Common.SMSGateway.GetSupplierModifyMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                      details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));


                    if (phone != "")
                    { b = await Common.SMSGateway.Send(smsmsg, phone); }
                    phone = Common.Utils.GetMobileNo(supplier.Mobile);
                    if (phone != "")
                    {
                      
                        b = await Common.SMSGateway.Send(smsmsg, phone);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
                try
                {
                    string message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("CChangeConfirmMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    Common.Mailer ml = new Common.Mailer();
                    //User Email
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    msg.To.Add(forUser[0].Email);
                    if (forUser[0].Email != byUser.Email)
                    {
                        msg.CC.Add(byUser.Email);
                    }


                    // add bcc for Customercommunications
                    string BccEmailsforcus = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcus != "")
                    {
                        string[] emails = BccEmailsforcus.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }


                    //for corporate admins
                    if (rle == CLayer.Role.Roles.CorporateUser)
                    {
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
                    //else
                    //{
                    //    //copy to travel Agent
                    //    if (rle == CLayer.Role.Roles.Agent)
                    //    {
                    //        if (byUser != null)
                    //        {
                    //            string em = BLayer.User.GetEmail(byUser.UserId);
                    //            if (em != null && em != "")
                    //            {
                    //                msg.CC.Add(em);
                    //            }
                    //        }
                    //    }
                    //}



                    msg.Subject = "Booking Change Confirmation";
                    msg.Body = message;

                    msg.IsBodyHtml = true;
                    try { 
                    await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex) { Common.LogHandler.HandleError(ex); }
                    //Supplier Email
                    if (supplier.Email != "" || details.PropertyEmail != "")
                    {
                        if (supplier.Email == "")
                        {
                            supplier.Email = details.PropertyEmail;
                            details.PropertyEmail = "";
                        }
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SChangeConfirmMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                        supplierMsg.To.Add(supplier.Email);
                        supplierMsg.Subject = "Booking Change Confirmation";
                        supplierMsg.Body = message;

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

                        if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                        supplierMsg.IsBodyHtml = true;
                        await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
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


        public async Task<bool> SendCancMailsAndSms(long bookingId)
        {
            try
            {
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                try
                {
                    string phone = forUser[0].Mobile;
                    if (phone == "") phone = forUser[0].Phone;
                    string smsmsg = Common.SMSGateway.GetCancellationMsg(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.PropertyTitle, details.propertyCity, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    
                    bool b = false;
                    phone = Common.Utils.GetMobileNo(phone);
                    if (phone != "")
                    { b = await Common.SMSGateway.Send(smsmsg, phone); }
                    phone = Common.Utils.GetMobileNo(details.Mobile);

                    smsmsg = Common.SMSGateway.GetSupplierCancellationMsg(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                      details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));

                    if (phone != "")
                    { b = await Common.SMSGateway.Send(smsmsg, phone); }
                    phone = Common.Utils.GetMobileNo(supplier.Mobile);
                    if (phone != "")
                    {
                      
                        b = await Common.SMSGateway.Send(smsmsg, phone); 
                    }
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
                try
                {
                    string message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("UCancMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    Common.Mailer ml = new Common.Mailer();
                    //User Email
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    msg.To.Add(forUser[0].Email);
                    if (forUser[0].Email != byUser.Email)
                    {
                        msg.CC.Add(byUser.Email);
                    }

                    // add bcc for Customercommunications
                    string BccEmailsforcus = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                    if (BccEmailsforcus != "")
                    {
                        string[] emails = BccEmailsforcus.Split(',');
                        for (int i = 0; i < emails.Length; ++i)
                        {
                            msg.Bcc.Add(emails[i]);
                        }
                    }


                    //for corporate admins
                    if (rle == CLayer.Role.Roles.CorporateUser)
                    {
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
                    //else
                    //{
                    //    //copy to travel Agent
                    //    if (rle == CLayer.Role.Roles.Agent)
                    //    {
                    //        if (byUser != null)
                    //        {
                    //            string em = BLayer.User.GetEmail(byUser.UserId);
                    //            if (em != null && em != "")
                    //            {
                    //                msg.CC.Add(em);
                    //            }
                    //        }
                    //    }
                    //}
                    msg.Subject = "Cancellation Confirmation";
                    msg.Body = message;

                    msg.IsBodyHtml = true;
                    try
                    {
                        await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                    }
                    catch (Exception ex) { Common.LogHandler.HandleError(ex); }
                    //Supplier Email
                    if (supplier.Email != "" || details.PropertyEmail != "")
                    {
                        if (supplier.Email == "")
                        {
                            supplier.Email = details.PropertyEmail;
                            details.PropertyEmail = "";
                        }
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SCancMail") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                        supplierMsg.To.Add(supplier.Email);
                        supplierMsg.Subject = "Cancellation Confirmation";
                        supplierMsg.Body = message;

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

                        if (details.PropertyEmail != "") supplierMsg.To.Add(details.PropertyEmail);
                        supplierMsg.IsBodyHtml = true;
                        await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
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

        public Models.ModifyModel ModifyBooking(Models.ModifyModel data)
        {
            long offlinebookingid = data.BookingId;
            CLayer.Booking bookDetails = BLayer.Bookings.GetDetails(data.BookingId);
            long userid = BLayer.Bookings.GetBookedByUserId(data.BookingId);
            CLayer.CancellationData cdata = BLayer.Cancellation.ModifyBooking(data.BookingId, offlinebookingid, data.CheckIn, data.CheckOut, userid);

            data.NewBookingExists = cdata.NewBookingExist;
            return data;
        }

        public Models.ModifyModel ChangeDatesInfo(Models.ModifyModel data)
        {
            long offlinebookingid = data.BookingId;
            if (data.CheckOut <= data.CheckIn)
            {
                data.Failed = true;
                data.Msg = "Invalid Check-In and Check-Out dates";
                return data;
            }
            else
            {
                if (data.CheckIn <= DateTime.Today)
                {
                    data.Failed = true;
                    data.Msg = "Sorry, you cannot modify the booking";
                    return data;
                }
            }
            CLayer.Booking bookDetails = BLayer.Bookings.GetDetails(data.BookingId);
            if (bookDetails == null)
            {
                data.Failed = true;
                data.Msg = "Invalid Booking details";
                return data;
            }
            DateTime checkIn = bookDetails.CheckIn;
            DateTime checkout = bookDetails.CheckOut;
            data.OldCIn = checkIn;
            data.OldCOt = checkout;
            if(data.CheckIn == checkIn && data.CheckOut == checkout)
            {
                data.Failed = true;
                data.Msg = "No changes have been made as your dates selected remain same as original dates";
                return data;
            }
            if (data.CheckIn > checkout || data.CheckOut < checkIn)
            {
                data.Failed = true;
                data.Msg = "Please cancel the current booking and make a new booking!";
                return data;
                //If you go forward
                //check avaliablity and mark it!!
                //cancel the current one
                //forward it to payment gateway
            }
            else
            {
                long userid = BLayer.Bookings.GetBookedByUserId(data.BookingId);
                CLayer.CancellationData cdata = BLayer.Cancellation.GetModifyInfo(data.BookingId, offlinebookingid, data.CheckIn, data.CheckOut, userid);
                data.Refundable = cdata.Refundable;
                data.AdditionalDays = cdata.AdditionalDays;
                data.CancelledDays = cdata.CancelledDays;
                data.CancellationCharge = (double)cdata.TotalCancellationCharge;
                data.ServiceCharge = cdata.ServiceCharge;
                // CLayer.Booking bdet = BLayer.Bookings.GetDetails(BLayer.Bookings.GetCartId(userid));

                //  if (bdet == null)
                data.NewBookingAmount = (double)Math.Round(cdata.NewBookingRate);
                //  else
                //       data.NewBookingAmount = (double) bdet.TotalAmount;

                data.CurrentTotalAmount = (double)bookDetails.TotalAmount;
                data.Postback = true;
            }

            return data;
        }

        public Models.CancelModel CancelAllTransactions(long bookingId)
        {
            long userid = 0;
            long.TryParse(User.Identity.GetUserId(), out userid);
            CLayer.Role.Roles urole = BLayer.User.GetRole(userid);
            Models.CancelModel cancel = new Models.CancelModel();
            cancel.CancellationCharge = 0;

            if (!(urole == CLayer.Role.Roles.Administrator || urole == CLayer.Role.Roles.Staff))
            {
                if (userid != BLayer.Bookings.GetBookedByUserId(bookingId))
                {
                    cancel.CanCancel = false;
                    return cancel;
                }
            }
            cancel.CanCancel = true;
            // BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, bookingId);
            BLayer.Transaction.CancelAllTransactions(bookingId);
            return cancel;
        }
        //public Models.CancelModel CancelBookingItem(long bookingItemId)
        //{
        //    long userid = 0;
        //    long.TryParse(User.Identity.GetUserId(), out userid);
        //    CLayer.Role.Roles urole = BLayer.User.GetRole(userid);
        //    Models.CancelModel cancel = new Models.CancelModel();
        //    cancel.CancellationCharge = 0;
        //    if (!(urole == CLayer.Role.Roles.Administrator || urole == CLayer.Role.Roles.Staff))
        //    {
        //        if (userid != BLayer.BookingItem.GetBookedUserId(bookingItemId))
        //        {
        //            cancel.CanCancel = false;
        //            return cancel;
        //        }
        //    }
        //    cancel.CanCancel = true;
        //    BLayer.Transaction.CancelAccommodation(bookingItemId);
        //    return cancel;
        //}

        #endregion
        //
        // GET: /Cancellation/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DeclineDetails(long bookingId)
        {
            Models.CancelModel cancel = new Models.CancelModel();
            try
            {

                if (BLayer.Bookings.GetStatus(bookingId) != CLayer.ObjectStatus.BookingStatus.BookingRequest)
                {
                    cancel.CanCancel = false;
                    return View("~/Areas/Admin/Views/Cancellation/_Details.cshtml", cancel);
                }
                cancel.IsAccommodationCancel = false;
                cancel.CancellationCharge = 0;

                CLayer.Booking detail = BLayer.Bookings.GetDetails(bookingId);
                cancel.Amount = detail.TotalAmount;            
                cancel.RefundAmount = cancel.Amount;


                //Partial payment first amount paid               
                long Paymentoption = BLayer.Bookings.GetPaymentoption(bookingId);
                CLayer.ObjectStatus.PartialPaymentStatus PartialPayStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                {
                    if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                    {
                        CLayer.Booking data = BLayer.Bookings.GetDataForPayment(bookingId);
                        cancel.Amount = data.PaymentFirstinstallment;
                        cancel.RefundAmount = data.PaymentFirstinstallment;


                    }
                }
                if (cancel.RefundAmount < 1) cancel.RefundAmount = 0;
                cancel.CanCancel = (cancel.RefundAmount > CLayer.Settings.TRANSACTION_MIN_AMOUNT);
                cancel.Id = bookingId;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                cancel = new Models.CancelModel();
            }
            return View("_Details", cancel);
        }

        public ActionResult CancelDetails(long bookingId)
        {
            Models.CancelModel cancel = new Models.CancelModel();
            try
            {

                if (BLayer.Bookings.GetStatus(bookingId) != CLayer.ObjectStatus.BookingStatus.Success)
                {
                    cancel.CanCancel = false;
                    return View("~/Areas/Admin/Views/Cancellation/_Details.cshtml", cancel);
                }
                cancel.IsAccommodationCancel = false;
                cancel.CancellationCharge = 0;
                // CLayer.Transaction trans = BLayer.Transaction.
                CLayer.Booking detail = BLayer.Bookings.GetDetails(bookingId);
                cancel.Amount = detail.TotalAmount;
                //TODO find firstDaycharge
                decimal firstDayCharge = BLayer.Bookings.GetFirstDayCharge(bookingId);
                List<CLayer.Transaction> refundable = BLayer.Transaction.GetAllRefundable(bookingId);
                cancel.ServiceCharge = (decimal)BLayer.Transaction.GetTotalCancellationServiceCharge(bookingId, ref refundable);
                CLayer.Property prp = BLayer.Property.GetCancellationCharges(BLayer.Bookings.GetPropertyId(bookingId));
                //todo change total amount to amount without tax
                cancel.CancellationCharge = BLayer.Bookings.GetTotalCancellationCharge(prp, detail.TotalAmount, firstDayCharge, detail.CheckIn);

                cancel.RefundAmount = cancel.Amount - cancel.ServiceCharge - cancel.CancellationCharge;


                //Partial payment first amount paid               
                long Paymentoption = BLayer.Bookings.GetPaymentoption(bookingId);
                CLayer.ObjectStatus.PartialPaymentStatus PartialPayStatus = BLayer.Bookings.GetPartialPaymentStatus(bookingId);
                if (Paymentoption == (int)CLayer.ObjectStatus.PaymentOption.PartialPayment)
                {
                    if (PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.InitialPaymentSuccess || PartialPayStatus == CLayer.ObjectStatus.PartialPaymentStatus.SecondPaymentFailed)
                    {
                        CLayer.Booking data = BLayer.Bookings.GetDataForPayment(bookingId);
                        cancel.Amount = data.PaymentFirstinstallment;
                        cancel.RefundAmount = data.PaymentFirstinstallment - cancel.ServiceCharge - cancel.CancellationCharge;
                       
                    }
                }
                if (cancel.RefundAmount < 1) cancel.RefundAmount = 0;
                cancel.CanCancel = (cancel.RefundAmount > CLayer.Settings.TRANSACTION_MIN_AMOUNT);
                cancel.Id = bookingId;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                cancel = new Models.CancelModel();
            }
            return View("_Details", cancel);
        }



        public ActionResult CancelAccDetails(long itemId)
        {
            Models.CancelModel cancel = new Models.CancelModel();
            try
            {
                if (itemId < 1) return View("_Details", cancel);
                cancel.CancellationCharge = 0;
                cancel.IsAccommodationCancel = true;
                // CLayer.Transaction trans = BLayer.Transaction.
                CLayer.BookingItem detail = BLayer.BookingItem.GetDetails(itemId);

                if (detail == null || BLayer.Bookings.GetStatus(detail.BookingId) != CLayer.ObjectStatus.BookingStatus.Success)
                {
                    cancel.CanCancel = false;
                    return View("~/Areas/Admin/Views/Cancellation/_Details.cshtml", cancel);
                }

                cancel.Amount = detail.TotalAmount;
                //TODO find firstDaycharge
                decimal firstDayCharge = BLayer.BookingItem.GetFirstDayCharge(itemId);
                List<CLayer.Transaction> refundable = BLayer.Transaction.GetAllRefundable(detail.BookingId);
                cancel.ServiceCharge = (decimal)BLayer.Transaction.GetTotalCancellationServiceCharge(detail.BookingId, ref refundable, detail.Amount);
                CLayer.Property prp = BLayer.Property.GetCancellationCharges(BLayer.Bookings.GetPropertyId(detail.BookingId));
                cancel.CancellationCharge = BLayer.Bookings.GetTotalCancellationCharge(prp, detail.TotalAmount, firstDayCharge, detail.CheckIn);
                //cancellation charge applied on detail.totalamount , not on detail.amount which is supplier rate
                cancel.RefundAmount = cancel.Amount - cancel.ServiceCharge - cancel.CancellationCharge;
                cancel.CanCancel = (cancel.RefundAmount > CLayer.Settings.TRANSACTION_MIN_AMOUNT);
                cancel.Id = itemId;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                cancel = new Models.CancelModel();
            }
            return View("_Details", cancel);
        }
         [Common.AdminRolePermission(AllowAllRoles=true)]
        [HttpPost]
        public async Task<ActionResult> CancelBooking(long bookingId, decimal? CancAmt)
        {
            Models.CancelModel cancel = new Models.CancelModel();
            try
            {
                if (bookingId > 0)
                {
                    cancel = CancelAllTransactions(bookingId);
                    cancel.Id = bookingId;
                   
                    if (cancel.CanCancel)
                    {
                        BLayer.Bookings.SetLastRefundAmt(bookingId, CancAmt.Value);

                        CLayer.ObjectStatus.BookingStatus BStatus = BLayer.Bookings.GetStatus(bookingId);

                        if (BStatus == CLayer.ObjectStatus.BookingStatus.Cancelled)
                        {
                            bool b = await SendCancMailsAndSms(bookingId);
                        }
                    }

                    if (cancel.CanCancel)
                    {
                        // BOOKING CANCEL FROM EXTERNAL INVENTORY REQUEST

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

                            int CacelSts = bookcandt.Cancellation_Status;
                            BLayer.BookingExternalInventory.UpdateCancellationStatus(reqbook.BookingExtInvReqId, CacelSts);

                            bookcandt.Cancellation_Number = Bookingcanceldetails.cancellation_number;
                            if (Bookingcanceldetails.cancelled_date == null) { Bookingcanceldetails.cancelled_date = Convert.ToString(DateTime.Now); }
                            bookcandt.Cancelled_Date = Bookingcanceldetails.cancelled_date;
                            bookcandt.Cancellation_Response = ResponseCancel;
                            BLayer.BookingExternalInventory.SaveBookingCancelResponse(bookcandt);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                cancel = new Models.CancelModel();
            }
            ViewBag.bookingid = bookingId;
            return PartialView("_Processed");
        }

        //[HttpPost]
        //public ActionResult CancelAccommodation(long itemId)
        //{
        //    Models.CancelModel cancel = new Models.CancelModel();
        //    try
        //    {
        //        cancel = CancelBookingItem(itemId);
        //        cancel.Id = itemId;
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //        cancel = new Models.CancelModel();
        //    }
        //    return PartialView("_Processed");
        //}
        [AllowAnonymous]
        public ActionResult CancellationPolicy(long pId)
        {
            return PartialView("_CancelPolicy", pId);
        }

        [AllowAnonymous]
        public ActionResult PolicyText(long pId)
        {
            return PartialView("_PolicyText", pId);
        }

        public ActionResult Modify(long bookingId)
        {
            Models.ModifyModel data = new Models.ModifyModel();
            try
            {

                long userid = 0;
                long.TryParse(User.Identity.GetUserId(), out userid);
                CLayer.Role.Roles urole = BLayer.User.GetRole(userid);

                if (!(urole == CLayer.Role.Roles.Administrator || urole == CLayer.Role.Roles.Staff))
                {
                    if (userid != BLayer.Bookings.GetBookedByUserId(bookingId))
                    {
                        data.Failed = true;
                        data.Msg = "You cannot Modify/Cancel the booking.";
                        return PartialView("_ModificationInfo", data);
                    }
                }
                CLayer.Booking bk = BLayer.Bookings.GetDetails(bookingId);
                if (bk == null)
                {
                    data.Failed = true;
                    data.Msg = "Booking data not found";
                    return PartialView("_ModificationInfo", data);
                }
                data.CheckIn = bk.CheckIn;
                data.CheckOut = bk.CheckOut;
                data.BookingId = bookingId;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
            return PartialView("_ModificationInfo", data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Change(Models.ModifyModel data)
        {
            try
            {
                Models.ModifyModel daa;// = new Models.ModifyModel();
                daa = ChangeDatesInfo(data);
                return PartialView("_ModificationInfo", daa);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView();
        }
        [HttpPost]
        public async Task<ActionResult> ChangeBooking(Models.ModifyModel data)
        {
            try
            {
                Models.ModifyModel da = ModifyBooking(data);
                //CANCEL OLD EXTERNAL BOOKING REQUEST 
                List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(data.BookingId);
                if (DtBookReq != null)
                {
                    if (DtBookReq.Count > 0)
                    {
                        ExternalBookRequestCancel(data.BookingId);
                    }
                }            
                if (da.NewBookingExists)
                    return RedirectToAction("Index", "Booking");
                else
                {
                    BLayer.Bookings.SetLastRefundAmt(data.BookingId,data.Refundable);
                 
                    // set new external  booking request
                    string HotelId = BLayer.Property.GetHotelId(BLayer.Bookings.GetPropertyId(data.BookingId));
                    if (HotelId != null && HotelId != "")
                    {
                        BookingSubmitRequest(data.BookingId);
                    }
                 
                    da.Msg = "The change request is under processing. You will get your refund  as soon as possible";
                    bool status = await SendModifyMailsAndSms(data.BookingId);
                    return View("_ModifyResult", da);
                }
                //     return PartialView("_ModificationInfo", da);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView();
        }

        protected string GetIPAddress()
        {
            var context = System.Web.HttpContext.Current;
            string ip = String.Empty;

            if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                ip = context.Request.UserHostAddress;
            return ip;
        }
        public bool BookingSubmitRequest(long bookingId)
        {
            long propertyId = 0;
           
            try
            {
                string IpAdds = GetIPAddress();
                propertyId = BLayer.BookingExternalInventory.GetPropertyIdByBookingId(bookingId);
                List<CLayer.BookingItem> bookitems = BLayer.BookingItem.GetAllDetails(bookingId);
                string HotelId = BLayer.Property.GetHotelId(BLayer.Bookings.GetPropertyId(bookingId));

                if (HotelId != null && HotelId != "")
                {
                    List<CLayer.Address> adr = BLayer.Bookings.GetBookedForUser(bookingId);
                    CLayer.Address byAddress = BLayer.Bookings.GetBookedByUser(bookingId);
                    CLayer.Booking data = BLayer.Bookings.GetDetails(bookingId);

                    string query_key = data.OrderNo + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                    string BookingsessionId = "bs" + "_" + bookingId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                    string BookingrequestId = "br" + "_" + bookingId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");

                    string errorinrequest = "";

                    #region

                    foreach (CLayer.BookingItem bi in bookitems)
                    {
                        long accid = bi.AccommodationId;
                        string RoomId = BLayer.Accommodation.GetRoomId(accid);

                        if (RoomId != "" && RoomId != null)
                        {
                            int totaladult = bi.NoOfAdults + bi.NoOfGuests;
                            List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> HotelRoomDetailsList = StayBazar.Inventory.MaximojoBook.BookingAvailDeserializeJsonResponse(HotelId, data.CheckIn.ToString("yyyy-MM-dd"), data.CheckOut.ToString("yyyy-MM-dd"), totaladult, bi.NoOfChildren, bi.NoOfAccommodations, query_key, BookingsessionId, BookingrequestId);

                            //Hotel rooms list filter by rooms only plan

                            List<string> newroomtypelist = new List<string>();
                            List<string> Multipleroomtypes = new List<string>();

                            foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails u in HotelRoomDetailsList)
                            {
                                if (!newroomtypelist.Contains(u.roomtype_id))
                                {
                                    newroomtypelist.Add(u.roomtype_id);
                                }
                                else
                                {
                                    if (!Multipleroomtypes.Contains(u.roomtype_id))
                                    {
                                        Multipleroomtypes.Add(u.roomtype_id);
                                    }
                                }
                            }

                            List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails> NewHotelRoomDetailsList = new List<StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails>();

                            foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails RId in HotelRoomDetailsList)
                            {
                                if (!Multipleroomtypes.Contains(RId.roomtype_id))
                                {
                                    NewHotelRoomDetailsList.Add(RId);
                                }
                                else
                                {

                                    //// check name contains Room Only
                                    //string[] SplitRoomonlyterm = RId.room_name.Split('-');
                                    //if (SplitRoomonlyterm[1].Trim() == "Room only")
                                    //{
                                    //    NewHotelRoomDetailsList.Add(RId);
                                    //}

                                    ////if (RId.RatePlanId == System.Configuration.ConfigurationManager.AppSettings.Get("rateplanidformaximojo"))
                                    ////{
                                    ////    NewHotelRoomDetailsList.Add(RId);
                                    ////}

                                    if (RId.room_name.Contains("Room only"))
                                    {
                                        NewHotelRoomDetailsList.Add(RId);
                                    }

                                }
                            }



// add rooms without room only type (add min one)

                            foreach (string ff in Multipleroomtypes)
                            {
                                if (!NewHotelRoomDetailsList.Exists(a => a.roomtype_id == ff))
                                {
                                    foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails RId in HotelRoomDetailsList)
                                    {
                                        if (RId.roomtype_id == ff)
                                        {
                                            if (!NewHotelRoomDetailsList.Exists(a => a.roomtype_id == ff))
                                            {
                                                NewHotelRoomDetailsList.Add(RId);
                                            }
                                        }
                                    }
                                }
                            }




                            if (NewHotelRoomDetailsList.Exists(a => a.roomtype_id == RoomId))
                            {
                                #region
                                foreach (StayBazar.Inventory.Models.MaxBookingAvailibility_Response.HotelroomsDetails ro in NewHotelRoomDetailsList)
                                {
                                    if (RoomId == ro.roomtype_id)
                                    {
                                        // room only rateplan id check
                                        //if (ro.RatePlanId == System.Configuration.ConfigurationManager.AppSettings.Get("rateplanidformaximojo"))
                                        //{
                                            try
                                            {
                                                #region
                                                //BOOKING REQUEST PASS
                                                //CLayer.BookingExternalInventory roomdet = BLayer.BookingExternalInventory.GetAllDetailsByRoomIdandHotelId(RoomId, HotelId);
                                                List<StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room> RoomsList = new List<StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room>();
                                                StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room Lrooms = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room();

                                                StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party fff = new Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party();
                                                fff.adults = bi.NoOfAdults;

                                                List<object> chdrn = new List<object>();

                                                for (int i = 0; i < bi.NoOfChildren; i++)
                                                {
                                                    chdrn.Add(5);
                                                }

                                                fff.children = chdrn;

                                                //multiple accommoadtion 
                                                for (int i = 0; i < bi.NoOfAccommodations; i++)
                                                {
                                                    Lrooms.party = fff;
                                                    Lrooms.traveler_first_name = byAddress.Firstname;
                                                    Lrooms.traveler_last_name = byAddress.Firstname;
                                                    RoomsList.Add(Lrooms);
                                                }


                                                var Book_WihoutPayobj = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.RootObject
                                                {
                                                    checkin_date = data.CheckIn.ToString("yyyy-MM-dd"),
                                                    checkout_date = data.CheckOut.ToString("yyyy-MM-dd"),
                                                    hotel_id = HotelId,
                                                    reference_id = data.OrderNo + "_" + ro.roomtype_id + "_" + ro.RatePlanId + "_" + DateTime.Now.ToString("yyyyMMdd_hhmmsstt"),
                                                    ip_address = IpAdds,
                                                    customer = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Customer
                                                    {
                                                        first_name = byAddress.Firstname,
                                                        last_name = byAddress.Firstname,
                                                        email = byAddress.Email,
                                                        phone_number = byAddress.Mobile,
                                                        country = byAddress.CountryCode
                                                    },
                                                    final_price_at_booking = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.FinalPriceAtBooking
                                                    {
                                                        amount = Convert.ToDouble(ro.final_price_at_bookingamt),
                                                        currency = ro.final_price_at_bookingamtcurr
                                                    },
                                                    final_price_at_checkout = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.FinalPriceAtCheckout
                                                    {
                                                        amount = Convert.ToDouble(ro.final_price_at_checkoutamt),
                                                        currency = ro.final_price_at_checkoutamtcurr
                                                    },
                                                    partner_data = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.PartnerData
                                                    {
                                                        DomainId = ro.DomainId,
                                                        HotelId = HotelId,
                                                        PromotionId = ro.PromotionId,
                                                        RatePlanId = ro.RatePlanId,
                                                        RoomId = ro.roomtype_id
                                                    },
                                                    rooms = RoomsList
                                                };


                                                //bookingsubmitWithoutPay
                                                string ResponseBookSub = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Submit_Without_paypaldetails(Book_WihoutPayobj);

                                                errorinrequest = ResponseBookSub;
                                                StayBazar.Inventory.Models.MaxBooking_Submit_Response.RootObject Bookingdetails = JsonConvert.DeserializeObject<StayBazar.Inventory.Models.MaxBooking_Submit_Response.RootObject>(ResponseBookSub);


                                                //Save Booking Response by booking submit 

                                                CLayer.BookingExternalInventory ExternalBook = new CLayer.BookingExternalInventory();
                                                ExternalBook.BookingId = bookingId;
                                                ExternalBook.hotel_id = Bookingdetails.reservation.hotel_id;
                                                ExternalBook.hotel_name = Bookingdetails.reservation.hotel.name;

                                                ExternalBook.Reference_Id = Bookingdetails.reference_id;
                                                ExternalBook.reservation_id = Bookingdetails.reservation.reservation_id;


                                                if (Bookingdetails.status == "Success")
                                                {
                                                    ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Success;
                                                }
                                                else if (Bookingdetails.status == "Failure")
                                                {
                                                    ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Failure;
                                                }
                                                else if (Bookingdetails.status == "Pending")
                                                {
                                                    ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.Pending;
                                                }
                                                else
                                                {
                                                    ExternalBook.BookingStatus = (int)CLayer.BookingExternalInventory.BookingStatusenum.None;
                                                }


                                                if (Bookingdetails.reservation.status == "Booked")
                                                {
                                                    ExternalBook.ReservationStatus = (int)CLayer.BookingExternalInventory.ReservationStatusenum.Booked;
                                                }
                                                else
                                                {
                                                    ExternalBook.ReservationStatus = (int)CLayer.BookingExternalInventory.ReservationStatusenum.none;
                                                }

                                                //if status failure/pending/reservation status error
                                                string VerifyRequestResp = "Booked Successfully";

                                                if (ExternalBook.BookingStatus != 1 || ExternalBook.ReservationStatus != 1)
                                                {
                                                    VerifyRequestResp = StayBazar.Inventory.MaximojoBook.Maximojo_Booking_Verify(HotelId, Bookingdetails.reference_id, Bookingdetails.reservation.reservation_id);
                                                }
                                                ExternalBook.StatusDetails = VerifyRequestResp;
                                                ExternalBook.roomtype_id = ro.roomtype_id;
                                                ExternalBook.room_name = ro.room_name;
                                                ExternalBook.room_desc = ro.room_desc;
                                                ExternalBook.CheckInDate = Bookingdetails.reservation.checkin_date;
                                                ExternalBook.CheckOutDate = Bookingdetails.reservation.checkout_date;
                                                ExternalBook.CustomerId = byAddress.UserId;
                                                ExternalBook.IpAddtress = IpAdds;
                                                ExternalBook.Response = ResponseBookSub;
                                                ExternalBook.BookingApiType = (int)CLayer.BookingExternalInventory.BookingApiTypes.Maximojo;
                                                ExternalBook.final_price_at_bookingamt = Convert.ToDecimal(Bookingdetails.reservation.receipt.final_price_at_booking.amount);
                                                ExternalBook.final_price_at_checkoutamt = Convert.ToDecimal(Bookingdetails.reservation.receipt.final_price_at_checkout.amount);
                                                ExternalBook.DomainId = ro.DomainId;
                                                ExternalBook.PromotionId = ro.PromotionId;
                                                ExternalBook.RatePlanId = ro.RatePlanId;
                                                ExternalBook.query_key = query_key;
                                                ExternalBook.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.none;
                                                ExternalBook.BookingsessionId = BookingsessionId;
                                                ExternalBook.BookingrequestId = BookingrequestId;
                                                long BookingExtInvReqId = BLayer.BookingExternalInventory.SaveBookingSubmitResponse(ExternalBook);

                                                //Save rooms details

                                                CLayer.BookingExternalInventory roomdt = new CLayer.BookingExternalInventory();
                                                roomdt.BookingExtInvReqId = BookingExtInvReqId;
                                                roomdt.hotel_id = ro.hotel_id;
                                                roomdt.hotel_name = ro.hotel_name;
                                                roomdt.roomtype_id = ro.roomtype_id;
                                                roomdt.room_name = ro.room_name;
                                                roomdt.room_desc = ro.room_desc;
                                                roomdt.final_price_at_bookingamt = ro.final_price_at_bookingamt;
                                                roomdt.final_price_at_bookingamtcurr = ro.final_price_at_bookingamtcurr;
                                                roomdt.final_price_at_checkoutamt = ro.final_price_at_checkoutamt;
                                                roomdt.final_price_at_checkoutamtcurr = ro.final_price_at_checkoutamtcurr;
                                                roomdt.DomainId = ro.DomainId;
                                                roomdt.PromotionId = ro.PromotionId;
                                                roomdt.RatePlanId = ro.RatePlanId;
                                                BLayer.BookingExternalInventory.Save(roomdt);

                                                //CANCELLATION WHEN BOOKING FAILED 

                                                if (Bookingdetails.status != "Success")
                                                {
                                                    // BOOKING CANCEL FROM EXTERNAL INVENTORY REQUEST

                                                    ExternalBookRequestCancel(bookingId);

                                                    //Redirecting after cancelling  all booked by user                                               
                                                    TempData["Errorcomes"] = "RequestFailed";
                                                    return true;
                                                    //return View("~/Views/Payment/RequestFailed.cshtml", propertyId);

                                                }
                                                #endregion
                                            }
                                            catch (Exception ex)
                                            {
                                                ExternalBookRequestCancel(bookingId);
                                                Exception Error = new Exception("#Error from  External Booking Submit Request(Cancellation , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex + errorinrequest);
                                                Common.LogHandler.HandleError(Error);
                                                TempData["Errorcomes"] = "internalerror";
                                                return true;
                                                //return View("~/Views/Payment/RequestFailed.cshtml", propertyId);
                                            }
                                        //}
                                    }
                                }
                                #endregion
                            }
                            else
                            {
                                // BOOKING CANCEL FROM EXTERNAL INVENTORY REQUEST
                                ExternalBookRequestCancel(bookingId);
                                //Redirecting after cancelling  all booked by user                                               
                                TempData["Errorcomes"] = "RequestFailed";
                                return true;
                            }
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                ExternalBookRequestCancel(bookingId);
                Exception Error = new Exception("#Error from External Booking Submit Request(Cancellation , BookingSubmitRequest) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
                TempData["Errorcomes"] = "internalerror";
                return true;
            }
            return false;

        }
        public ActionResult ExternalBookRequestCancel(long bookingId)
        {

            try
            {
                List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(bookingId);
                if (DtBookReq != null)
                {
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
            }
            catch (Exception ex)
            {
                Exception Error = new Exception("#Error from External Booking Request Cancel (cancellation , ExternalBookRequestCancel) for bookingId :- " + bookingId + " , " + DateTime.Now + " , " + ex);
                Common.LogHandler.HandleError(Error);
            }
            return null;


        }


        [AllowAnonymous]
        public ContentResult CheckAccommodationAvailable(long BookingId, string CheckIn, string CheckOut)
        {
            List<CLayer.BookingItem> bookitems = BLayer.BookingItem.GetAllDetails(BookingId);

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
                string HotelId = BLayer.Property.GetHotelId(Propertyid);
                string QueryKey = HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                string BookingSessionId = "bs_" + HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                string BookingRequestId = "br_" + HotelId + DateTime.Now.ToString("yyyyMMdd_hhmmsstt");
                if (RoomId != "" && RoomId != null)
                {

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
            else
            {
                return Content("true");
            }
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
                errormessage = accomodations + " are not available please change your selected criteria";
            }
            else
            {
                errormessage = accomodations + " not available please change your selected criteria";
            }



            return errormessage;
        }
	}
}