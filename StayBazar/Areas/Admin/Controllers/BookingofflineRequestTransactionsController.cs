using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class BookingofflineRequestTransactionsController : Controller
    {
        [Common.AdminRolePermission]
        // GET: Admin/BookingofflineRequestTransactions
        public ActionResult Index(string searchstring, int? searchitem)
        {
            Models.TransactionsModel search = new Models.TransactionsModel();

            if (searchstring == null)
            {
                searchstring = "";
            }
            if (searchitem == null)
            {
                searchitem = 0;
            }

            List<CLayer.Booking> users = BLayer.Transaction.GetAllForSearchBookingofflineRequest((int)CLayer.ObjectStatus.BookingRequestStatus.All, searchstring, searchitem.Value, 0, 25);
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


            if (data.SearchString == "")
            {
                data.SearchValue = 0;
            }
            List<CLayer.Booking> users = BLayer.Transaction.GetAllForSearchBookingofflineRequest(data.Status, data.SearchString, data.SearchValue, (data.currentPage - 1) * data.Limit, data.Limit);
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
            {
                data.SearchString = "";
            }
            if (data.SearchString == "")
            {
                data.SearchValue = 0;
            }

            if (data.SearchString == null)
                data.SearchString = "";
            List<CLayer.Booking> users = BLayer.Transaction.GetAllForSearchBookingofflineRequest(data.Status, data.SearchString, data.SearchValue, 0, 25);
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
                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("Bofflinebookconfirm") + bookingId.ToString());
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
                msg.Subject = "Booking Payment";
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


        //Confirm Booking
        public async Task<bool> BookingConfirm(long bookingId)
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.offlineconfirm, bookingId);

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
                    //message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("Bofflinebookconfirm") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("Bofflinebookconfirm") + bookingId.ToString());
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
                    msg.Subject = "Booking Payment";
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

                    //send supplier email/message 
                    //message
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

                    //email
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
                    //        //   message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntimation") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    //        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntimation") + bookingId.ToString());
                    //        System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();
                    //        supplierMsg.To.Add(supplier.Email);
                    //        supplierMsg.Subject = "Booking Confirmation";
                    //        supplierMsg.Body = message;

                    //        string BccEmailsforsupp = BLayer.Settings.GetValue("CCSuppliercommunications ");
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


        public bool BookingDecline(long BookingId)
        {
            try
            {
                BLayer.Bookings.SetStatus((int)CLayer.ObjectStatus.BookingStatus.Decline, BookingId);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return false;
            }
            return true;
        }

        public ActionResult BookingModify(long BookingId)
        {
            Models.OfflinePropertyBookingModel model = new Models.OfflinePropertyBookingModel();
            long PropertyId = BLayer.Bookings.GetPropertyId(BookingId);
            CLayer.Property Pdata = BLayer.Property.Get(PropertyId);
            CLayer.B2B supdata = BLayer.B2B.Get(Pdata.OwnerId);

            CLayer.Booking offedit = BLayer.Bookings.GetBookDetailsByBookingId(BookingId);
            List<CLayer.BookingItem> Items = BLayer.BookingItem.GetAllDetailsforoffline(BookingId);

            model.PropertyId = PropertyId;
            model.PropertyName = Pdata.Title;
            model.PropertyAddress = Pdata.Address;
            model.PropertyCity = Pdata.CityId;
            model.PropertyCityname = Pdata.City;
            model.PropertyState = Pdata.State;
            model.PropertyCountry = Pdata.Country;
            model.PropertyContactNo = Pdata.Phone;
            model.PropertyEmail = Pdata.Email;
            model.SupplierName = supdata.Name;


            model.Accommodationid = offedit.AccommodationId;
            model.Accommodationtypeid = offedit.AccommodationTypeId;
            model.AccommodatoinType = Convert.ToInt16(offedit.AccommodationType);
            string ccc = BLayer.Accommodation.GetAccommodationTitle(offedit.AccommodationId);
            model.AccommodationTypeName = ccc;
            model.StayCategoryid = offedit.StayCategoryId;
            model.CheckIn = offedit.CheckIn.ToString("dd/MM/yyyy");
            model.CheckOut = offedit.CheckOut.ToString("dd/MM/yyyy");
            //model.NoOfNight = offedit.NoOfNight;
            //model.NoOfRooms = offedit.NoOfRooms;
            model.NoOfPaxAdult = offedit.NoOfAdults;
            model.NoOfPaxChild = offedit.NoOfChildren;
            model.Type = 2;
            model.bookingitmList = Items;
            model.Count = Items.Count;

            model.BookingId = BookingId;
            model.CustomerId = BLayer.Bookings.GetBookedByUserId(BookingId);
            return View("_BookingModify", model);
        }


        public ActionResult AddAccomodation(int Count)
        {
            Models.OfflinePropertyBookingModel model = new Models.OfflinePropertyBookingModel();
            model.Type = 1;
            model.Count = Count;
            return View("~/Areas/Admin/Views/BookingofflineRequestTransactions/BookingAccDetails.cshtml", model);
        }

        public ActionResult GetAccommodationList(long Pid, long? BId, long? CusId, string ChIn, string Chout)
        {
            Models.OfflinePropertyBookingModel model = new Models.OfflinePropertyBookingModel();
            model.Type = 1;
            model.PropertyId = Pid;
            model.CustomerId = CusId.Value;
            model.BookingId = BId.Value;
            model.CheckIn = ChIn;
            model.CheckOut = Chout;
            return View("~/Areas/Admin/Views/BookingofflineRequestTransactions/_PropertyAccommodationList.cshtml", model);
        }


        public ActionResult Modifybook(Areas.Admin.Models.OfflinePropertyBookingModel.SimpleBookingModel Model)
        {
            try
            {
                BookAccommodation(Model);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return RedirectToAction("BookingModify", "BookingofflineRequestTransactions", new { Area = "Admin", BookingId = Model.BookingId_OffLine });
        }
        private int BookAccommodation(Areas.Admin.Models.OfflinePropertyBookingModel.SimpleBookingModel data)
        {

            long userId = data.BookingUserId_OffLine;
            long bookingId = data.BookingId_OffLine;

            if (data.BookingData == "")
            {
                return 0;
            }

            string[] bookitems = data.BookingData.Split(new char[] { '#' }, StringSplitOptions.RemoveEmptyEntries);
            if (bookitems.Length == 0)
            {
                return 0;
            }
            long id;
            int nums, kids, adults, guests;
            decimal Cus_sup_amt, Cus_Total_amt, Cus_Tax_amt, Cus_grdtotal_amt;
            List<CLayer.BookingAccDetails> bookings = new List<CLayer.BookingAccDetails>();
            List<long> accIds = new List<long>();
            CLayer.BookingAccDetails tmp;
            foreach (string s in bookitems)
            {
                if (s == "") continue;
                string[] ids = s.Split(new char[] { ',' });
                if (ids.Length != 9) continue;


                id = 0;
                adults = 0;
                kids = 0;
                guests = 0;
                nums = 0;
                Cus_sup_amt = 0;
                Cus_Total_amt = 0;
                Cus_Tax_amt = 0;
                Cus_grdtotal_amt = 0;

                long.TryParse(ids[0], out id);
                if (id == 0) continue;
                if (accIds.Contains(id)) continue;
                int.TryParse(ids[1], out nums);
                if (nums == 0) continue;
                int.TryParse(ids[2], out adults);
                //   if (adults == 0) continue;
                int.TryParse(ids[3], out kids);
                int.TryParse(ids[4], out guests);


                decimal.TryParse(ids[5], out Cus_sup_amt);
                decimal.TryParse(ids[6], out Cus_Total_amt);
                decimal.TryParse(ids[7], out Cus_Tax_amt);
                decimal.TryParse(ids[8], out Cus_grdtotal_amt);

                tmp = new CLayer.BookingAccDetails();
                tmp.AccCount = nums;
                tmp.AccommodationId = id;
                tmp.Adults = adults;
                tmp.Children = kids;
                tmp.Guest = guests;
                tmp.Cus_SupplierAmount = Cus_sup_amt;
                tmp.Cus_TotalAmount = Cus_Total_amt;
                tmp.Cus_TaxAmount = Cus_Tax_amt;
                tmp.Cus_GrandTotalAmount = Cus_grdtotal_amt;


                accIds.Add(id);
                bookings.Add(tmp);
            }
            if (accIds.Count == 0) return 1;
            //  List<CLayer.Rates> accrates = BLayer.Rate.GetTotalRates(accIds, data.BookCheckIn, data.BookCheckOut, BLayer.User.GetRole(userId), userId);
            return BookAccommodations(bookings, data.BookCheckIn, data.BookCheckOut, userId, bookingId);
        }

        public static int BookAccommodations(List<CLayer.BookingAccDetails> bookingInfo, DateTime checkIn, DateTime checkOut, long userId, long bookingId, string orderNo = "")
        {
            long PropId = BLayer.Accommodation.GetPropertyId(bookingInfo[0].AccommodationId);
            decimal tax = BLayer.PropertyTax.GetTotalTax(PropId);
            long InventoryAPIType = BLayer.Accommodation.GetInventoryAPIType(PropId);
            int result = 0;
            List<long> accIds = new List<long>();

            foreach (CLayer.BookingAccDetails t in bookingInfo)
            {
                accIds.Add(t.AccommodationId);
            }

            List<CLayer.Rates> accrates;
            CLayer.Role.Roles rle = BLayer.User.GetRole(userId);
            if (rle == CLayer.Role.Roles.CorporateUser || rle == CLayer.Role.Roles.Corporate)
                accrates = BLayer.Rate.GetTotalRates(accIds, checkIn, checkOut, CLayer.Role.Roles.Corporate, userId, InventoryAPIType);
            else
                accrates = BLayer.Rate.GetTotalRates(accIds, checkIn, checkOut, CLayer.Role.Roles.Customer, userId, InventoryAPIType);

            int cnt, i;

            cnt = bookingInfo.Count;
            CLayer.Booking bk = new CLayer.Booking();
            bk.BookingDate = DateTime.Today;
            bk.CheckIn = checkIn;
            bk.CheckOut = checkOut;
            bk.NoOfDays = (checkOut - checkIn).Days;
            bk.Status = (int)CLayer.ObjectStatus.BookingStatus.Offline;
            bk.BookingId = bookingId;
            bk.ByUserId = userId;
            bk.BookingId = BLayer.Bookings.SaveInitialDataForOfflinBeforConfirm(bk);


            BLayer.Bookings.SetPaymentRefNo(bk.BookingId, rle, orderNo);
            bookingId = bk.BookingId;
            List<CLayer.BookingItem> items = new List<CLayer.BookingItem>();
            CLayer.BookingItem bitem;
            DateTime lockIn = DateTime.Now;
            int minutes = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.LOCKIN_TIME), out minutes);
            lockIn = lockIn.AddMinutes(minutes);


            //Delete All Booking Items Befor Modify to New Booking by booking id

            BLayer.BookingItem.DeleteAllItemsByBookingId(bookingId);
            BLayer.BookingItem.DeleteAllTaxItemsByBookingId(bookingId);

            for (i = 0; i < cnt; i++)
            {
                foreach (CLayer.Rates rt in accrates)
                {
                    if (bookingInfo[i].AccommodationId == rt.AccommodationId)
                    {
                        if (bookingInfo[i].AccCount <= rt.NoofAcc && bookingInfo[i].AccCount != 0)
                        {
                            bitem = new CLayer.BookingItem();
                            bitem.CheckIn = checkIn;
                            bitem.CheckOut = checkOut;
                            bitem.BookingId = bookingId;
                            bitem.AccommodationId = rt.AccommodationId;
                            bitem.ForUser = userId;
                            bitem.NoOfAccommodations = bookingInfo[i].AccCount;
                            bitem.NoOfAdults = bookingInfo[i].Adults;
                            bitem.NoOfChildren = bookingInfo[i].Children;
                            bitem.NoOfGuests = bookingInfo[i].Guest;

                            bitem.Cus_SupplierAmount = bookingInfo[i].Cus_SupplierAmount;
                            bitem.Cus_TotalAmount = bookingInfo[i].Cus_TotalAmount;
                            bitem.Cus_TaxAmount = bookingInfo[i].Cus_TaxAmount;
                            bitem.Cus_GrandTotalAmount = bookingInfo[i].Cus_GrandTotalAmount;

                            bitem.LockInTime = lockIn;
                            bitem.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            items.Add(bitem);
                            bitem.BookingItemId = BLayer.BookingItem.SaveIntialData(bitem);
                        }
                    }
                }
            }

            long propertyId = BLayer.Accommodation.GetPropertyId(accrates[0].AccommodationId);
            List<CLayer.Tax> taxes = BLayer.Tax.GetAllForProperty(propertyId, checkIn, bk.BookingDate);
            List<CLayer.Tax> onTotalAmountTaxes = taxes.Where(m => m.OnTotalAmount == true).OrderBy(m => m.PriceUpto).ToList();
            List<CLayer.Tax> ordinaryTaxes = taxes.Where(m => m.OnTotalAmount == false).OrderBy(m => m.PriceUpto).ToList();
            decimal totalTax = 0;
            decimal guestRate = 0;
            decimal tt = 0;
            StringBuilder ratApplied = new StringBuilder();
            foreach (CLayer.BookingItem item in items)
            {
                foreach (CLayer.Rates rat in accrates)
                {
                    if (item.AccommodationId == rat.AccommodationId)
                    {
                        totalTax = 0;
                        ratApplied.Clear();
                        guestRate = ((rat.GuestRate + rat.TotalGuestTax) * item.NoOfGuests);
                        item.TotalAmount = ((rat.Amount + rat.TotalRateTax) * item.NoOfAccommodations) + guestRate;
                        totalTax = BLayer.Tax.GetTaxOnAmount(ordinaryTaxes, item.TotalAmount, item.BookingItemId);
                        totalTax = totalTax + BLayer.Tax.GetTaxOnAmount(onTotalAmountTaxes, item.TotalAmount + totalTax, item.BookingItemId);
                        item.Amount = Math.Round((rat.SupplierRate * item.NoOfAccommodations) + (rat.SupplierGuestRate * item.NoOfGuests));
                        if (rat.RateChanges != null && rat.RateChanges.Count > 0)
                        {
                            for (int ri = 0; ri < rat.RateChanges.Count; ri++)
                            {
                                if (ratApplied.Length > 0) ratApplied.Append(",");
                                ratApplied.Append(rat.RateChanges[ri].StartDate);
                                ratApplied.Append("#");
                                tt = (decimal)Math.Round(rat.RateChanges[ri].DayTotalCharge + (rat.RateChanges[ri].DayTotalGuestCharge * item.NoOfGuests));
                                ratApplied.Append(tt);
                            }
                            tt = (decimal)Math.Round(rat.RateChanges[0].DayTotalCharge + (rat.RateChanges[0].DayTotalGuestCharge * item.NoOfGuests));
                        }
                        else
                            tt = 0;
                        item.FirstDayCharge = tt;
                        item.TotalTax = totalTax;
                        item.TotalRateTax = Math.Round(rat.TotalRateTax * item.NoOfAccommodations);
                        item.TotalGuestTax = Math.Round(rat.TotalGuestTax * item.NoOfGuests);
                        // include tax with amount                    
                        item.TotalAmount = Math.Round(item.TotalAmount);
                        item.GuestAmount = Math.Round(guestRate);

                        item.RatesApplied = ratApplied.ToString();
                        item.CorporateDiscountAmount = rat.CorpDiscount;
                        item.CommissionSB = rat.SBMarkup;
                        if (item.NoOfGuests > 0)
                        {
                            item.CommissionSB += rat.SBGuestMarkup;
                            item.CorporateDiscountAmount += rat.CorpGuestDiscount;
                        }


                        BLayer.BookingItem.SaveAmounts(item);
                        BLayer.BookingItem.UpdateCustomBook(item.BookingItemId, 0);

                        //update custom amount
                        if (item.Cus_GrandTotalAmount > 0)
                        {
                            item.Amount = item.Cus_GrandTotalAmount;
                            item.TotalAmount = item.Cus_GrandTotalAmount;
                            item.TotalTax = item.Cus_TaxAmount;
                            item.TotalRateTax = item.Cus_TaxAmount;
                            item.CommissionSB = item.Cus_TotalAmount - item.Cus_SupplierAmount;
                            item.GuestAmount = 0;
                            item.TotalGuestTax = 0;
                            item.CorporateDiscountAmount = 0;
                            BLayer.BookingItem.SaveAmounts(item);
                            BLayer.BookingItem.UpdateCustomBook(item.BookingItemId, 1);
                        }



                        // save tax rate for each bookingitemid

                        long BItId = item.BookingItemId;
                        long BookId = item.BookingId;
                        long Pid = BLayer.Accommodation.GetPropertyId(rat.AccommodationId);
                        List<CLayer.Tax> tax1 = BLayer.Tax.GetPropertyTaxById(Pid);

                        if (item.Cus_GrandTotalAmount > 0)
                        {
                           
                        }
                        else
                        {
                            foreach (CLayer.Tax tx in tax1)
                            {
                                decimal TaxAmountBItem = Math.Round(((tx.Rate / tax) * ((rat.TotalRateTax * item.NoOfAccommodations) + (rat.TotalGuestTax * item.NoOfGuests))));
                                BLayer.BookingItem.SaveBookingtaxdata(BookId, BItId, tx.TaxTitleId, tx.Rate, TaxAmountBItem);
                            }
                            if (rat.AppliedOffers != null && rat.AppliedOffers.Count > 0)
                            {
                                foreach (CLayer.BookingItemOffer bo in rat.AppliedOffers)
                                {
                                    BLayer.BookingItem.AddOffer(item.BookingItemId, bo.OfferId, bo.OfferTitle);
                                }
                            }
                        }
                    }
                }
            }


            BLayer.Bookings.UpdateAmounts(bookingId);


            //add paypal commision outside india users
            long UserId = BLayer.Bookings.GetBookedByUserId(bookingId);
            CLayer.User dat = BLayer.User.GetCountrUser(UserId);
            string ct = dat.Country;
            if (ct != "India")
            {
                decimal paypalcomm = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_COMMISSION));
                decimal GrandTotalAmt = Math.Round(BLayer.Bookings.GetTotal(bookingId));
                decimal TotalAmtIncPayComm = Math.Round(GrandTotalAmt + (paypalcomm * GrandTotalAmt / 100));
                BLayer.Bookings.SavePaypalComm((GrandTotalAmt * paypalcomm / 100), bookingId);
                BLayer.Bookings.UpdateTotalAmtIncPayComm(TotalAmtIncPayComm, bookingId);

            }




            //PartialPaymentbooking

            CLayer.Role.Roles UsertypeId = BLayer.User.GetRole(userId);
            decimal Partialamountperc = 0;
            if (UsertypeId == CLayer.Role.Roles.Customer)
            {
                Partialamountperc = Math.Round(BLayer.Property.GetPropertyB2CpartialamountPerc(propertyId));
            }
            if (UsertypeId == CLayer.Role.Roles.Administrator || UsertypeId == CLayer.Role.Roles.Corporate || UsertypeId == CLayer.Role.Roles.Affiliate || UsertypeId == CLayer.Role.Roles.Supplier || UsertypeId == CLayer.Role.Roles.CorporateUser)
            {
                Partialamountperc = Math.Round(BLayer.Property.GetPropertyB2BpartialamountPerc(propertyId));
            }

            decimal Partialamount = Math.Round((Partialamountperc * Math.Round(BLayer.Bookings.GetTotal(bookingId))) / 100);
            decimal remainingamount = Math.Round(Math.Round(BLayer.Bookings.GetTotal(bookingId)) - Partialamount);
            BLayer.BookingItem.SavePartialAmount(bookingId, Partialamountperc, Partialamount, remainingamount);

            result = 1;

            return result;
        }

    }
}