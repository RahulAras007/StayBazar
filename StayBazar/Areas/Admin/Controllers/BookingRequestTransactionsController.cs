using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using StayBazar.Common;


namespace StayBazar.Areas.Admin.Controllers
{
    public class BookingRequestTransactionsController : Controller
    {
        //[Common.AdminRolePermission(AllowAllRoles=true)]
        // GET: /Admin/Transactions/
        [Common.AdminRolePermission]
        public ActionResult Index(string searchstring, int? searchitem)
        {
            Models.TransactionsModel search = new Models.TransactionsModel();

            if (searchstring == null)
            {
                searchstring = "";
            }
            if (searchitem == null)
            {
                searchitem = 1;
            }

            List<CLayer.Booking> users = BLayer.Transaction.GetAllForSearchBookingRequest((int)CLayer.ObjectStatus.BookingRequestStatus.All, searchstring, searchitem.Value, 0, 25);
            search.SearchString = "";
            search.SearchValue = searchitem.Value;
            search.TotalRows = 0;
            search.Bookinglist = users;
            if (users.Count > 0)
            {
                search.TotalRows = users[0].TotalRows;
            }
            search.Limit = 25;
            search.currentPage = 1;
            ViewBag.Filter = search;
            return View(search);
        }
        [HttpPost]
        public ActionResult Pager(Models.TransactionsModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            List<CLayer.Booking> users = new List<CLayer.Booking>();
            if (data.InventoryAPIType <= 2)
            {
                users = BLayer.Transaction.GetAllForSearchBookingRequestWithType(data.Status, data.InventoryAPIType, data.SearchString, data.SearchValue, 0, 25);

            }
            else
            {
                users = BLayer.Transaction.GetAllForSearchBookingRequest(data.Status, data.SearchString, data.SearchValue, 0, 25);

            }
            //   List<CLayer.Booking> users = BLayer.Transaction.GetAllForSearchBookingRequest(data.Status, data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit);
            ViewBag.Filter = new Models.TransactionsModel();
            Models.TransactionsModel forPager = new Models.TransactionsModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = data.currentPage
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }

        [HttpPost]
        public ActionResult Filter(Models.TransactionsModel data)
        {
            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.Booking> users = new List<CLayer.Booking>();
            if (data.InventoryAPIType<=2)
            {
                users = BLayer.Transaction.GetAllForSearchBookingRequestWithType(data.Status,data.InventoryAPIType, data.SearchString, data.SearchValue, 0, 25);

            }
            else
            {
                users = BLayer.Transaction.GetAllForSearchBookingRequest(data.Status, data.SearchString, data.SearchValue, 0, 25);

            }
            ViewBag.Filter = new Models.TransactionsModel();
            Models.TransactionsModel forPager = new Models.TransactionsModel()
            {
                Status = data.Status,
                SearchString = data.SearchString,
                SearchValue = data.SearchValue,
                TotalRows = 0,
                Limit = 25,
                currentPage = 1
            };
            if (users.Count > 0)
            {
                forPager.TotalRows = users[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            return PartialView("_List", users);
        }

        [HttpPost]
        public ActionResult FillBookedByAddressSearch(long BookingId)
        {
            List<CLayer.Address> users = BLayer.Transaction.BookedByAddressSearch(BookingId);
            return PartialView("_AddressBookedList", users);
        }
        [HttpPost]
        public ActionResult Booking_GetBookedFor(long BookingId)
        {

            List<CLayer.Address> users = BLayer.Bookings.GetBookedForUser(BookingId);
            return PartialView("_AddressForBooking", users);
        }
        //Edit Status
        [HttpPost]
        public ActionResult StatusCancel(long BookingId)
        {
            //try
            //{
            if (BookingId > 0)
            {
                BLayer.Transaction.CancelAllTransactions(BookingId);
                //ViewBag.Saved = true;
                //return RedirectToAction("Index");
            }
            else
            {
                // ViewBag.Saved = false; 
            }
            return RedirectToAction("Index");

            //}
            //catch (Exception ex)
            //{
            //    Common.LogHandler.HandleError(ex);
            //    return RedirectToAction("Index");
            //}

        }
        //RESEND EMAIL
        public async Task<bool> ResendemailC(long bookingId)
        {
            try
            {
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
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
        public async Task<bool> ResendemailS(long bookingId)
        {
            try
            {
                if (bookingId < 1) return false;
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);
                try
                {
                    string message = "";
                    Common.Mailer ml = new Common.Mailer();
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
            return true;
        }

        //RESEND SMS
        public async Task<bool> ResendsmsC(long bookingId)
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
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }
        public async Task<bool> ResendsmsS(long bookingId)
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
                    string phone = "";
                    bool b = false;
                    string smsmsg = "";
                    smsmsg = Common.SMSGateway.GetSupplierBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                        details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
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
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
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
        public bool GDSBookingConfirm(long bookingId)
        {
            long propertyId = 0;
            bool returnvalue = false;

            int InventoryAPIType = 0;
            try
            {
                StayBazar.Controllers.BookingController objBookingController = new StayBazar.Controllers.BookingController();

                long userId = objBookingController.GetUserId();
                if (bookingId <= 0)
                {
                    bookingId = BLayer.Bookings.GetCartId(userId);
                }

                string IpAdds = GetIPAddress();
                propertyId = BLayer.BookingExternalInventory.GetPropertyIdByBookingId(bookingId);
                InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(bookingId));
                Session["InventoryAPIType"] = InventoryAPIType;
                ViewBag.AmadeusRates = TempData["AmadeusRates"];
                TempData.Keep("AmadeusRates");

                List<CLayer.BookingItem> bookitems = new List<CLayer.BookingItem>();
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    bookitems = BLayer.BookingItem.GetAllDetails(bookingId, true);
                }
                else
                {
                    bookitems = BLayer.BookingItem.GetAllDetails(bookingId);
                }

                string HotelId = BLayer.Property.GetHotelId(BLayer.Bookings.GetPropertyId(bookingId));


                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    #region HOTEL SELL
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELSELLACTION); ;

                    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                    srch.HotelCode = HotelId;



                    List<CLayer.RoomStaysResult> RoomStaysResultList = APIUtility.AmedusHotelRoomStaysResultList;

                    RoomStaysResultList = (List<CLayer.RoomStaysResult>)TempData["RoomStaysResult"];
                    TempData.Keep("RoomStaysResult");
                    string SoapHeaderStateful = string.Empty;
                    foreach (var item in RoomStaysResultList)
                    {
                        string BookingCode = item.BookingCode;
                        string HotelSellSoapBody = APIUtility.Hotel_Sell(HotelId, BookingCode);
                        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        string result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);

                        if (!APIUtility.CheckHotelBookingErrorExists(result))
                        {
                            Serializer HotelSell = new Serializer();
                            CLayer.HotelSell.Envelope HotelSellResult = HotelSell.Deserialize<CLayer.HotelSell.Envelope>(result);
                            Session["HotelSellStatus"] = "success";
                            Session["SessionId"] = HotelSellResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelSellResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelSellResult.Header.Session.SecurityToken;
                            returnvalue = true;
                        }
                        else
                        {
                            Serializer HotelSellError = new Serializer();
                            CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(result);
                            Session["HotelSellStatus"] = "error";
                            Session["SessionId"] = HotelSellErrorResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelSellErrorResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelSellErrorResult.Header.Session.SecurityToken;
                            ViewBag.HotelSellResult = "error";
                            TempData["Errorcomes"] = "error";
                            #region PNR CANCEL


                            string resultCancel = APIUtility.ExecutePNRCancel("", (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRGeneration);

                            returnvalue = false;
                            
                            #endregion

                        }



                    }
                    #endregion

                    #region PNR MULTIELEMENTS-FINAL
                    Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
                    SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                    string BookingCodeFinal = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCodeFinal, "test@test.com", 11);

                    TempData["RoomStaysResult"] = RoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = RoomStaysResultList;

                    string resultFinal = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);


                    Serializer pnrser = new Serializer();

                    CLayer.PNRAddElementsConfirm.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElementsConfirm.Envelope>(resultFinal);
                    Session["ControlNumber"] = PNRAddResult.Body.PNR_Reply.pnrHeader.reservationInfo.reservation.controlNumber;


                    if (APIUtility.CheckHotelBookingErrorExists(resultFinal))
                    {

                        Serializer HotelSellError = new Serializer();
                        CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(resultFinal);
                        Session["HotelSellStatus"] = "error";
                        Session["SessionId"] = HotelSellErrorResult.Header.Session.SessionId;
                        Session["SequenceNumber"] = Convert.ToInt32(HotelSellErrorResult.Header.Session.SequenceNumber);
                        Session["SecurityToken"] = HotelSellErrorResult.Header.Session.SecurityToken;

                        #region PNR CANCEL
                        string ControlNumber = Convert.ToString(Session["ControlNumber"]);
                        string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize);
                        returnvalue = false; 
                        #endregion

                    }
                    else
                    {
                        returnvalue = true;
                    }

                    #endregion
                    List<CLayer.BookingItem> objBookItems = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCodeFinal);
                    foreach (var item in objBookItems)
                    {
                        BLayer.BookingItem.UpdateHotelConfirmNumber(item.BookingId, item.AccommodationId, Convert.ToString(Session["ControlNumber"]));
                    }

                }
            }
            catch(Exception ex)
            {
              
                returnvalue = false;
                throw ex;
            }
            return returnvalue;
        }
    

        //Confirm Booking
        public async Task<bool> BookingConfirm(long bookingId)
        {
            try
            {

                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);

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

        public async Task<bool> BookingConfirmbyCredit(long bookingId)
        {
            try
            {

                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Success, bookingId);

                //send customer email/message               
                //email
                if (bookingId < 1) return false;
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetailsAmadeus(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                try
                {
                    string message = "";
                    Common.Mailer ml = new Common.Mailer();
                    //for normal user mail body
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("BConfirmationMailbyCredit") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
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


        public bool BookingDecline(long BookingId,string ControlNumber="")
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Decline, BookingId);
               
                #region GDS Booking Cancel      
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(BookingId));
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    ControlNumber = BLayer.Bookings.GetGDSBookingControlNumber(BookingId);
                    if(string.IsNullOrEmpty(ControlNumber))
                        {
                        ControlNumber = Convert.ToString(System.Web.HttpContext.Current.Session["ControlNumber"]);
                    }
                   
                    int OptionCode = (ControlNumber == "") ? 0 : (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize;
                    string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, OptionCode);
                }
                #endregion 

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return false;
            }
            return true;
        }

        public bool BookingCancel(long BookingId, string ControlNumber = "")
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, BookingId);

                #region GDS Booking Cancel      
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(BLayer.Bookings.GetPropertyId(BookingId));
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    ControlNumber = BLayer.Bookings.GetGDSBookingControlNumber(BookingId);
                    if (string.IsNullOrEmpty(ControlNumber))
                    {
                        ControlNumber = Convert.ToString(System.Web.HttpContext.Current.Session["ControlNumber"]);
                    }

                    int OptionCode = (ControlNumber == "") ? 0 : (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize;
                    string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, OptionCode);

                    #region Transaction Log 

                    string LoggedInUser =Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUser"]);
                    string currentUrl = string.Empty;
                   
                    try
                    {
                        APIUtility.GenerateGDSTransactionLog(currentUrl, resultCancel, Convert.ToInt32(LoggedInUser), (int)CLayer.ObjectStatus.GDSType.PNRCancel, BookingId);

                    }
                    catch (Exception ex)
                    {
                        APIUtility.GenerateGDSTransactionLog(currentUrl, resultCancel, Convert.ToInt32(LoggedInUser), (int)CLayer.ObjectStatus.GDSType.PNRCancel, BookingId);

                    }

                    #endregion Transaction log end
                }
                #endregion

          


            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return false;
            }
            return true;
        }
    }
}