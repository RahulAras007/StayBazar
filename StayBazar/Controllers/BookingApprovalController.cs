using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StayBazar.Lib.Security;
using System.Threading.Tasks;
using System.Collections;
using Hangfire;
using System.IO;
namespace StayBazar.Controllers
{
    public class BookingApprovalController : Controller
    {
        private const int DATERANGE = -30;
        private const int PAGE_LIMIT = 25;
        private string LoggedInUserID = string.Empty;

         //    private  string IsLive = System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingbyCredit").ToString();
         Models.TestModel smsdata = new Models.TestModel();
        #region Custom Methods

        private Models.BookingApprovalModel InitialData()
        {
            Models.BookingApprovalModel data = new Models.BookingApprovalModel();
            long cid = 0;
            long.TryParse(User.Identity.GetUserId(), out cid);
            data.Approvals= BLayer.Bookings.GetAllCorporateBookingApprovals(cid);
            return data;
        }
        #endregion
        // GET: /CorporateUsers/
        [Authorize]
        public ActionResult Index()
        {
            try
            {
                Models.BookingApprovalModel data = BookingApprovalList(); //InitialData();
                return View(data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View();
        }
        public Models.BookingApprovalModel BookingApprovalList()
        {
            string userid = User.Identity.GetUserId();
            LoggedInUserID = userid;
            CLayer.GDSUserAddress objUser = BLayer.User.GDUSUserDetails(Convert.ToInt64(LoggedInUserID));
            TempData["LoggedinGDSUser"] = objUser.FullName;
            string email = User.Identity.GetUserName();
            CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);

            long id = 0;
            long.TryParse(userid, out id);//70 sumod@gmail.com
            Models.BookingApprovalModel search = new Models.BookingApprovalModel();

            List<CLayer.Booking> pendingApprovalList = new List<CLayer.Booking>();
            List<CLayer.Booking> ApprovalHistoryList = new List<CLayer.Booking>();


            if (role == CLayer.Role.Roles.Corporate)
                {
                //pendingApprovalList = BLayer.BookingHistory.GtOfflineBookingApprovalHistoryForCorporate((int)CLayer.ObjectStatus.BookingStatus.WaitingForApproval, id, 90, 0, PAGE_LIMIT, 1); //*this is for offlinebooking
                pendingApprovalList = BLayer.BookingHistory.GtBookingApprovalHistoryForCorporate((int)CLayer.ObjectStatus.BookingStatus.WaitingForApproval, id, 90, 0, PAGE_LIMIT, 1); //this is for booking
                ApprovalHistoryList = BLayer.BookingHistory.GetBookingApprovalHistoryDetailsForCorporateAdmin(0, id, 90, 0, PAGE_LIMIT, 1);
                }
                else
                {
                pendingApprovalList = BLayer.BookingHistory.GetBookingApprovalHistoryForUser((int)CLayer.ObjectStatus.BookingStatus.WaitingForApproval, id, 90, 0, PAGE_LIMIT, 1);
                ApprovalHistoryList = BLayer.BookingHistory.GetBookingApprovalHistoryDetails(0, id, 90, 0, PAGE_LIMIT, 1);
                }

            search.Status = (int)CLayer.ObjectStatus.StatusType.Active;
            ViewBag.SearchValue = 1;
            search.Day = 90;
            search.Type = 1;
            search.TotalRows = 0;
            ViewBag.TotalRows = 0;
            ViewBag.Limit = 10;
            ViewBag.currentPage = 1;
            search.pendingApprovalBookinglist  = pendingApprovalList;
            search.BookingApprovalHistory = ApprovalHistoryList;
           if (pendingApprovalList.Count > 0)
            {
                search.TotalRows = pendingApprovalList[0].TotalRows;
                ViewBag.TotalRows = pendingApprovalList[0].TotalRows;
            }
          
            search.Limit = PAGE_LIMIT;
            search.currentPage = 1;
            search.FromDate = DateTime.Today.AddDays(DATERANGE);
            search.ToDate = DateTime.Today;
            ViewBag.Filter = search;
            ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(id);
            return search;
        }

        [HttpPost]
        public ActionResult Pager(int currentPage, int Day, int Limit, int Type)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            string email = User.Identity.GetUserName();
            CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);
            List<CLayer.Booking> pendingApprovalList = new List<CLayer.Booking>();

            if (role == CLayer.Role.Roles.Corporate)
                //pendingApprovalList = BLayer.BookingHistory.GtOfflineBookingApprovalHistoryForCorporate((int)CLayer.ObjectStatus.BookingStatus.Success, id, 0, (currentPage - 1) * Limit, Limit, Type);
            pendingApprovalList = BLayer.BookingHistory.GtBookingApprovalHistoryForCorporate((int)CLayer.ObjectStatus.BookingStatus.Success, id, 0, (currentPage - 1) * Limit, Limit, Type);
            else
                pendingApprovalList = BLayer.BookingHistory.GetBookingApprovalHistoryForUser((int)CLayer.ObjectStatus.BookingStatus.Success, id, 0, (currentPage - 1) * Limit, Limit, Type);
                
            ViewBag.Filter = new Models.BookingHistoryModel();
            Models.BookingApprovalModel forPager = new Models.BookingApprovalModel()
            {           
                TotalRows = 0,
                Limit = PAGE_LIMIT,
                currentPage = currentPage
            };
            if (pendingApprovalList.Count > 0)
            {
                forPager.TotalRows = pendingApprovalList[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(id);
            ViewBag.Role = role;

            return View("BookingApprovalList", pendingApprovalList);

        }

        [HttpPost]
        public ActionResult PagerBookingApprovalHistory(int currentPage, int Day, int Limit, int Type)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            string email = User.Identity.GetUserName();
            CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);
            List<CLayer.Booking> ApprovalHistoryList = new List<CLayer.Booking>();

            if (role == CLayer.Role.Roles.Corporate)
                ApprovalHistoryList = BLayer.BookingHistory.GetBookingApprovalHistoryDetailsForCorporateAdmin((int)CLayer.ObjectStatus.BookingStatus.Success, id, 0, (currentPage - 1) * Limit, Limit, Type);
            else
                ApprovalHistoryList=BLayer.BookingHistory.GetBookingApprovalHistoryDetails((int)CLayer.ObjectStatus.BookingStatus.Success, id, 0, (currentPage - 1) * Limit, Limit, Type);

            ViewBag.Filter = new Models.BookingHistoryModel();
            Models.BookingApprovalModel forPager = new Models.BookingApprovalModel()
            {
                TotalRows = 0,
                Limit = PAGE_LIMIT,
                currentPage = currentPage
            };
            if (ApprovalHistoryList.Count > 0)
            {
                forPager.TotalRows = ApprovalHistoryList[0].TotalRows;
            }
            ViewBag.Filter = forPager;
            ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(id);

            return View("BookingApprovalHistory", ApprovalHistoryList);

        }

        [HttpPost]
        public ActionResult FilterBookingApproval(StayBazar.Areas.Admin.Models.TransactionsModel data)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
            string email = User.Identity.GetUserName();
            CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);
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

            List<CLayer.Booking> users = new List<CLayer.Booking>();
            if (role == CLayer.Role.Roles.Corporate)
            {
                users = BLayer.Transaction.BookingPendingApprovalsForCorporate_GetOnUserSearch(id, data.Status, data.SearchString, data.SearchValue, 0, 25);
            }
            else
            {
                users = BLayer.Transaction.BookingPendingApprovals_GetOnUserSearch(id, data.Status, data.SearchString, data.SearchValue, 0, 25);
            }


            ViewBag.Filter = new Models.BookingApprovalModel();
            Models.BookingApprovalModel forPager = new Models.BookingApprovalModel()
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
            ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(id);
            return PartialView("BookingApprovalList", users);
        }

        [HttpPost]
        public ActionResult FilterBookingApprovalHistory(StayBazar.Areas.Admin.Models.TransactionsModel data)
        {
            string userid = User.Identity.GetUserId();
            long id = 0;
            long.TryParse(userid, out id);
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
            List<CLayer.Booking> users = BLayer.Transaction.GetBookingApprovalHistoryDetailsForCorporateAdmin(id,data.Status, data.SearchString, data.SearchValue, 0, 25);
            ViewBag.Filter = new Models.BookingApprovalModel();
            Models.BookingApprovalModel forPager = new Models.BookingApprovalModel()
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
            ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(id);
            return PartialView("BookingApprovalHistory", users);
        }
        public ActionResult SaveDetails(Models.CorporateUserModel data)
        {
            try
            {
                //if (ModelState.IsValid)
                //{
                //CheckStaffLimit
                long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);

                int i = BLayer.B2BUser.B2B_CheckStaffLimit(cid);
                //if (i== 1)// if i is one only add new user here from admin settings
                //{
                if (data.UserId > 0)
                { }
                else
                {
                    if (!BLayer.User.IsUniqueEmail(data.UserId, data.Email))
                        return View("_general");
                }

                CLayer.User usr = new CLayer.User();
                usr.UserId = data.UserId;
                usr.FirstName = data.FirstName;
                usr.LastName = data.LastName;
                usr.SalutationId = data.SalutationId;
                usr.Email = data.Email;
                usr.Status = data.StatusId;
                usr.UserTypeId = (int)CLayer.Role.Roles.CorporateUser;
                //long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);
                long userId = BLayer.B2BUser.SaveCorporateUser(usr, cid, data.UserTypeId);
                if (userId < 0)
                    return View("_general", "-2");
                CLayer.Address address = new CLayer.Address()
                {
                    AddressId = data.AddressId,
                    UserId = userId,
                    AddressText = data.Address,
                    CityId = data.CityId,
                    State = data.State,
                    CountryId = data.CountryId,
                    ZipCode = data.ZipCode,
                    // Phone = data.Phone,
                    Mobile = data.Mobile,
                    AddressType = (int)CLayer.Address.AddressTypes.Primary
                };
                if (data.City != null && data.City != "")
                    address.City = data.City;
                if (data.CityId > 0)
                    address.City = BLayer.City.Get(data.CityId).Name;
                address.AddressType = (int)CLayer.Address.AddressTypes.Primary;
                BLayer.Address.Save(address);
                //password save
                if (data.Password != "" && data.Password != null)
                {
                    if (userId > 0)
                    {
                        UserManager<StayBazar.Lib.Security.IdentityUser> usrmngr = new UserManager<StayBazar.Lib.Security.IdentityUser>(new UserStore());
                        String hashedNewPassword = usrmngr.PasswordHasher.HashPassword(data.Password);
                        BLayer.User.SetPassword(userId, hashedNewPassword);
                    }
                }
                //}

             Models.BookingApprovalModel mdata = InitialData();
                return View("_List", mdata);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("_general");
        }

        //public ActionResult Confirm()
        //{
        //    return RedirectToAction("Index", "Continue");
        //}

        public ActionResult Confirm(CLayer.BookingApprovals BookingApprovals)
        {
            long PropertyId = BLayer.Bookings.GetPropertyId(BookingApprovals.BookingId);
            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropertyId);

            Models.SimpleBookingModel data = new Models.SimpleBookingModel();
            List<CLayer.BookingItem> BookingItems = null;
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                BookingItems = BLayer.BookingItem.GetAllDetails(BookingApprovals.BookingId, true);
            }
            string BookingData = string.Empty;
            long bookingId = 0;
            foreach (var item in BookingItems)
            {
                BookingData = "#" + BookingItems[0].AccommodationId.ToString() + "," + BookingItems[0].NoOfAccommodations.ToString() + "," + BookingItems[0].NoOfAdults.ToString() + "," + BookingItems[0].NoOfChildren.ToString() + "," + BookingItems[0].NoOfGuests.ToString();// + "," + BookingItems[0].NoOfAccommodations.ToString();

                data.BookingData = BookingData;
                data.BookCheckIn = BookingItems[0].CheckIn;
                data.BookCheckOut = BookingItems[0].CheckOut;
                data.PropertyId = PropertyId;
                bookingId = BookingItems[0].BookingId;
                Session["GDSCountry"] = BookingItems[0].CountryName;
            }

            //    SimpleBookingModel data = (SimpleBookingModel)TempData["Bookingdata"];

            if (data != null)
            {
                TempData["Bookingdata"] = data;

                Session["BookingData"] = data.BookingData;
                Session["BookCheckIn"] = data.BookCheckIn;
                Session["BookCheckOut"] = data.BookCheckOut;
                Session["PropertyId"] = data.PropertyId;

            }

            TempData["Bookingdata"] = data;
            TempData["BookingID"] = bookingId;

            ViewBag.Status = "Confirmedforbooking";
            ViewBag.BookingID = BookingApprovals.BookingId;
            long resultStatus = BLayer.Bookings.GetApprovalStatus(BookingApprovals.BookingId);

            long cid = 0;
            long.TryParse(User.Identity.GetUserId(), out cid);
            ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(cid);
            string ApprovalMessage = string.Empty;// "Booking confirmed by " + ViewBag.Approver;

            string userid = User.Identity.GetUserId();
           string   email = User.Identity.GetUserName();
           CLayer.Role.Roles   role = (CLayer.Role.Roles)BLayer.User.GetRole(email);

            string CorpAdmin = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATE);
            string CorpUser = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEUSER);
            string CorpApprover1 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER1);
            string CorpApprover2 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER2);
            string CorporateTestMail = BLayer.Settings.GetValue(CLayer.Settings.CORPORATETESTEMAIL);

            if ((email == CorporateTestMail) || (CorpAdmin == email) || (CorpUser == email) || (CorpApprover1 == email) || (CorpApprover2 == email))
            {
                //   return Json(new { success = true, responseText = "Booking not allowed" }, JsonRequestBehavior.AllowGet);
                BLayer.Bookings.UpdateStatus(BookingApprovals.BookingId,(int)CLayer.ObjectStatus.BookingStatus.Success);
                //var mailSend = BackgroundJob.Enqueue(() => SendMailAndSmsForCorporateBookingSmallVersion(BookingApprovals.BookingId, resultStatus, null, false));
                //BackgroundJob.Enqueue(() => HangFireTest());
                return Json(new { success = true, responseText = "Booking successfully completed" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                //var mailSend = BackgroundJob.Enqueue(() => SendMailAndSmsForCorporateBookingSmallVersion(BookingApprovals.BookingId, resultStatus, null, false));
                //BackgroundJob.Enqueue(() => HangFireTest());

                return Json(new { success = true, responseText = ApprovalMessage }, JsonRequestBehavior.AllowGet);
            }


          
        }

        [Authorize]
        public async Task<ActionResult> Approve(CLayer.BookingApprovals BookingApprovals)
        {
            long offlinebookingid = BookingApprovals.BookingId;
            try
            {

                //long PropertyId = BLayer.Bookings.GetOfflineBookingPropertyId(BookingApprovals.BookingId);//*this is for getting offline booking property id
                long PropertyId = BLayer.Bookings.GetPropertyId(BookingApprovals.BookingId);//This is for getting booking property id
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropertyId);

                long id = 0;
          
                string email = User.Identity.GetUserName();
                CLayer.Role.Roles role = (CLayer.Role.Roles)BLayer.User.GetRole(email);
                if (role == CLayer.Role.Roles.Corporate)
                {
                    BookingApprovals.approver_id = BLayer.Bookings.GetNextApproverID(BookingApprovals.BookingId);
                }
              

                    Models.SimpleBookingModel data = new Models.SimpleBookingModel();
                List<CLayer.BookingItem> BookingItems = null;
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    BookingItems = BLayer.BookingItem.GetAllDetails(BookingApprovals.BookingId, true);
                }
                //else
                //{
                //    BookingItems = BLayer.BookingItem.GetAllOfflineDetails(BookingApprovals.BookingId, true);
                //}
                string BookingData = string.Empty;
                long bookingId = 0;
                foreach (var item in BookingItems)
                {
                    BookingData = "#" + BookingItems[0].AccommodationId.ToString() + "," + BookingItems[0].NoOfAccommodations.ToString() + "," + BookingItems[0].NoOfAdults.ToString() + "," + BookingItems[0].NoOfChildren.ToString() + "," + BookingItems[0].NoOfGuests.ToString();// + "," + BookingItems[0].NoOfAccommodations.ToString();

                    data.BookingData = BookingData;
                    data.BookCheckIn = BookingItems[0].CheckIn;
                    data.BookCheckOut = BookingItems[0].CheckOut;
                    data.PropertyId = PropertyId;
                    bookingId = BookingItems[0].BookingId;
                    Session["GDSCountry"] = BookingItems[0].CountryName;
                }

         

                //    SimpleBookingModel data = (SimpleBookingModel)TempData["Bookingdata"];

                if (data != null)
                {
                    TempData["Bookingdata"] = data;

                    Session["BookingData"] = data.BookingData;
                    Session["BookCheckIn"] = data.BookCheckIn;
                    Session["BookCheckOut"] = data.BookCheckOut;
                    Session["PropertyId"] = data.PropertyId;

                }

                TempData["Bookingdata"] = data;
                TempData["BookingID"] = bookingId;
              

                long resultStatus = BLayer.Bookings.GetApprovalStatus(BookingApprovals.BookingId);
                long result = 0;
              
            
                 result = BLayer.Bookings.BookingApprovalsSave(BookingApprovals);
                string BApproverName= string.Empty;
                CLayer.Booking ApproverDetails = new CLayer.Booking();
                try
                {
                    //ApproverDetails = BLayer.Bookings.GetNextApproverDetailsForOfflineBooking(bookingId);//This is for getting next ApproverID
                    ApproverDetails = BLayer.Bookings.GetNextApproverDetails(bookingId);//This is for getting next approver id of online booking
                   BApproverName = ApproverDetails.ApproverName;
                }
                catch(Exception ex)
                {
                    BApproverName = "";
                }
     

                resultStatus = BLayer.Bookings.GetApprovalStatus(BookingApprovals.BookingId, BookingApprovals.approver_id);
                CLayer.B2B Creditbookingdata = BLayer.B2B.GetbookingcreditforCorporateUser(Convert.ToInt32(BookingApprovals.approver_id));

                bool IsCreditBooking = BLayer.B2B.IsCreditBookingNeeded(BookingApprovals.approver_id);

                if ((IsCreditBooking) && (resultStatus!=(int)CLayer.ObjectStatus.BookingApprovalStatus.ConfirmedforBooking))
       //        if ((resultStatus != (int)CLayer.ObjectStatus.BookingApprovalStatus.ConfirmedforBooking))
                {

                   
                    //bool mailSend = await SendMailsAndSmsForCorporateBooking(BookingApprovals.BookingId, resultStatus);
                    var mailSend = BackgroundJob.Enqueue(() => SendMailAndSmsForCorporateBookingSmallVersion(BookingApprovals.BookingId, resultStatus, Creditbookingdata, IsCreditBooking));

                    BackgroundJob.Enqueue(() => HangFireTest());
                }
                else
                {
                   // IsCreditBooking = false;
                   if(!IsCreditBooking)
                    {
                        var mailSend = BackgroundJob.Enqueue(() => SendMailAndSmsForCorporateBookingSmallVersion(BookingApprovals.BookingId, resultStatus, null, false));
                    }
                    BackgroundJob.Enqueue(() => HangFireTest());
                }
                string ApprovalMessage = string.Empty;
                if (resultStatus==(long)CLayer.ObjectStatus.BookingApprovalStatus.ConfirmedforBooking)
                {
             //       CLayer.B2B Creditbookingdata = BLayer.B2B.GetbookingcreditforCorporateUser(Convert.ToInt32(BookingApprovals.approver_id));

                  BookingController objBooking = new BookingController();
                    string userid = User.Identity.GetUserId();
                    email = User.Identity.GetUserName();
                    role = (CLayer.Role.Roles)BLayer.User.GetRole(email);

                    string CorpAdmin = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATE);
                    string CorpUser = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEUSER);
                    string CorpApprover1 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER1);
                    string CorpApprover2 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER2);
                    string CorporateTestMail = BLayer.Settings.GetValue(CLayer.Settings.CORPORATETESTEMAIL);

                    if (BLayer.B2B.IsCreditBookingNeeded(BookingApprovals.approver_id))
                        {
                            decimal amt = BLayer.Bookings.GetTotalcreditbookamount(Convert.ToInt32(BookingApprovals.approver_id));
                            decimal creditbalceAmt = Creditbookingdata.TotalCreditAmount - amt;

                            decimal BTotalAmt = BLayer.Bookings.GetTotal(BookingApprovals.BookingId);
                            string ApproverName = BLayer.Bookings.GetCrediBookingApprover(BookingApprovals.approver_id);
                        if (BTotalAmt != null)
                            {
                                if (creditbalceAmt > BTotalAmt)
                                {
                                    TempData["CreditLimitTime"] = Creditbookingdata.CreditDays;
                                //stop credit booking
                                //string userid = User.Identity.GetUserId();
                                // email = User.Identity.GetUserName();
                                // role = (CLayer.Role.Roles)BLayer.User.GetRole(email);

                                //string CorpAdmin = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATE);
                                //string CorpUser = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEUSER);
                                //string CorpApprover1 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER1);
                                //string CorpApprover2 = BLayer.Settings.GetValue(CLayer.Settings.GDSCORPORATEAPPROVER2);
                                //string CorporateTestMail = BLayer.Settings.GetValue(CLayer.Settings.CORPORATETESTEMAIL);

                                if ((email == CorporateTestMail) || (CorpAdmin == email) || (CorpUser == email) || (CorpApprover1 == email) || (CorpApprover2 == email))
                                {
                                    BLayer.Bookings.UpdateStatus(BookingApprovals.BookingId, (int)CLayer.ObjectStatus.BookingStatus.Success);//*this status is for booking
                                    //   return Json(new { success = true, responseText = "Booking not allowed" }, JsonRequestBehavior.AllowGet);
                                    return Json(new { success = true, responseText = "Booking successfully completed" }, JsonRequestBehavior.AllowGet);


                                }
                                else
                                {
                                    var objResult = objBooking.CorporateBookingbycreditAmount(BookingApprovals.BookingId, offlinebookingid);


                                    ContentResult objContent = (ContentResult)objResult;
                                    if (objContent.Content.Contains("success"))
                                    {
                                        return Json(new { success = true, responseText = "Booking successfully completed" }, JsonRequestBehavior.AllowGet);
                                    }
                                    else
                                    {
                                        return Json(new { success = true, responseText = "Booking failed. " + objContent.Content }, JsonRequestBehavior.AllowGet);
                                        var mailSend = BackgroundJob.Enqueue(() => SendMailAndSmsForCorporateBookingSmallVersion(BookingApprovals.BookingId, resultStatus, Creditbookingdata, IsCreditBooking));
                                    }
                                }

                            }
                                else
                                {
                                //cancel booking
                                long UserID = BLayer.Bookings.GetBookedByUserId(BookingApprovals.BookingId);
                                objBooking.CancelOrderbyCredit(UserID);
                                //     return Json(new { success = true, responseText = "Booking cancelled by approver- " + ApproverName }, JsonRequestBehavior.AllowGet);
                                return Json(new { success = true, responseText = "Booking cancelled due to "+ BookingApprovals.RejectionNote }, JsonRequestBehavior.AllowGet);


                            }
                        }
                        }
                    else
                    {
                        ViewBag.Status = "Approved";
                        long cid = 0;
                        long.TryParse(User.Identity.GetUserId(), out cid);
                        ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(cid);

                        if (resultStatus == (long)CLayer.ObjectStatus.BookingApprovalStatus.Rejected)
                        {
                            //   ApprovalMessage = "Booking rejected by " + ViewBag.Approver;
                            ApprovalMessage = "Booking rejected successfully";
                        }
                        else
                        {
                            if(BApproverName != "")
                            {
                              //  ApprovalMessage = "Booking approved successfully.";
                                ApprovalMessage = "Booking approved successfully.-"+" This booking required approval from " + ApproverDetails.ApproverName + "-Thanks for your patience, while we seek approval internally.";
                            }
                            else
                            {
                                ApprovalMessage = "Booking approved by " + ViewBag.Approver;

                            }

                            if ((email == CorporateTestMail) || (CorpAdmin == email) || (CorpUser == email) || (CorpApprover1 == email) || (CorpApprover2 == email))
                            {
                                BLayer.Bookings.UpdateStatus(BookingApprovals.BookingId, (int)CLayer.ObjectStatus.BookingStatus.Success);

                                //   return Json(new { success = true, responseText = "Booking not allowed" }, JsonRequestBehavior.AllowGet);
                                return Json(new { success = true, responseText = "Booking successfully completed" }, JsonRequestBehavior.AllowGet);


                            }


                        }
                    }
                }
                else
                {
                    ViewBag.Status = "Approved";
                    long cid = 0;
                    long.TryParse(User.Identity.GetUserId(), out cid);
                    ViewBag.Approver = BLayer.Bookings.GetApproverNameByID(cid);
              
                    if (resultStatus == (long)CLayer.ObjectStatus.BookingApprovalStatus.Rejected)
                    {
                        //BLayer.OfflineBooking.UpdateSaveStatus(BookingApprovals.BookingId, (int)CLayer.OfflineBooking.Statuslist.Cancel);//*this is for offline booking
                        //  ApprovalMessage = "Booking rejected by " + ViewBag.Approver;
                        ApprovalMessage = "Booking rejected successfully ";
                    }
                    else
                    {
                        //ApprovalMessage = "Booking approved successfully ";// + ViewBag.Approver;
                     //   ApprovalMessage = "Booking approved successfully.This booking required approval from " + ApproverDetails.ApproverName +" While we see seek approval thanks for your patience in the meantime.";
                        if (BApproverName != "")
                        {
                         //  ApprovalMessage = "Booking approved successfully.";
                           ApprovalMessage = "Booking approved successfully.-" + " This booking required approval from " + ApproverDetails.ApproverName + "-Thanks for your patience, while we seek approval internally.";
                        }
                        else
                        {
                           // BLayer.OfflineBooking.UpdateSaveStatus(BookingApprovals.BookingId, (int)CLayer.OfflineBooking.Statuslist.Approved);//*this is for offline booking
                            ApprovalMessage = "Booking approved by " + ViewBag.Approver;

                        }
                    }
                }
          
                

                //         return RedirectToAction("SMSTest", "Test", smsdata);
                return Json(new { success = true, responseText = ApprovalMessage}, JsonRequestBehavior.AllowGet);

              

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Json(new { success = false, responseText = "Error" }, JsonRequestBehavior.AllowGet);
            }
            return View();
        }

        public ActionResult Remove(long id)
        {
            try
            {
                BLayer.User.SetDeleteStatus(id);
                Models.BookingApprovalModel mdata = InitialData();
                return View("_List", mdata);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                Redirect("~/ErrorPage");
            }
            return View();
        }
        public void HangFireTest()
        {
            Console.WriteLine("HangFire Job running");
        }

        public bool SendMailAndSmsForCorporateBookingSmallVersion(long bookingId, long approvalstatus,CLayer.B2B p2b,bool IsCreditBooking)
        {
            //, CLayer.B2B pB2B = null, bool IsCreditBooking = false

            if(IsCreditBooking)
            {
              
                return SendMailsAndSmsForCorporateBooking(bookingId, approvalstatus, p2b, IsCreditBooking);
            }
            else
            {
                return SendMailsAndSmsForCorporateBooking(bookingId, approvalstatus, null, false);
            }
                

            return true;



        }
        public bool SendMailsAndSmsForCorporateBooking(long bookingId, long approvalstatus, CLayer.B2B pB2B = null, bool IsCreditBooking = false)
        {
            try
            {
                ArrayList msgList = new ArrayList();
                List<CLayer.Booking> PrevousApproverDetails = new List<CLayer.Booking>();
                string PreviousUsers = string.Empty;
                string BookingMobile = string.Empty;
                string BookingPhone = string.Empty;
                string PreviousUserName = string.Empty;
                string RejectionNote = string.Empty;

                if (bookingId < 1) return false;
                //CLayer.Address byUser = BLayer.Bookings.GetOfflineBookedByUser(bookingId);//*this is for offline booking
                CLayer.Address byUser = BLayer.Bookings.GetBookedByUser(bookingId);//This is for booking
                List<CLayer.Address> forUser = BLayer.Bookings.GetBookedForUser(bookingId);
                if (byUser == null) return false;
                if (forUser.Count == 0) return false;
                CLayer.Role.Roles rle = BLayer.User.GetRole(byUser.UserId);
                CLayer.Booking details = BLayer.Bookings.GetDetailsSMS(bookingId);

                //CLayer.User supplier = BLayer.Bookings.GetSupplierDetailsofOfflineBooking(bookingId);//*This is for offline booking
                CLayer.User supplier = BLayer.Bookings.GetSupplierDetails(bookingId);//*This is for booking

                try
                {

                    //Booking user email send

                    string message = "";

                    Common.Mailer ml = new Common.Mailer();
                    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                    System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();



                    if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.ConfirmedforBooking)
                    {
                        //CLayer.Booking BookingUserDetails = BLayer.Bookings.GetOfflineBookingUserDetails(bookingId);//*This is for Offline booking
                        CLayer.Booking BookingUserDetails = BLayer.Bookings.GetBookingUserDetails(bookingId);//*This is booking

                        if (IsCreditBooking)
                        {
                            forUser[0].Firstname = pB2B.ApproverName;
                            forUser[0].Phone = pB2B.BookingPhone;
                            forUser[0].Mobile = pB2B.BookingMobile;
                            //  forUser[0].Email = pB2B.ApproverEmail;
                        }
                        else
                        {
                            forUser[0].Firstname = BookingUserDetails.ApproverName;
                            forUser[0].Phone = BookingUserDetails.Phone;
                            forUser[0].Mobile = BookingUserDetails.Mobile;
                            forUser[0].Email = BookingUserDetails.BookingUserEmail;
                        }
                       
                    }
                    else if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.Approved)
                    {
                        //approver email
                        CLayer.Booking ApproverDetails = BLayer.Bookings.GetNextApproverDetails(bookingId);
                        //booking user email
                        CLayer.Booking BookingUserDetails = null;// BLayer.Bookings.GetBookingUserDetails(bookingId);

                        if(BookingUserDetails!=null)
                        {
                            if (!string.IsNullOrEmpty(BookingUserDetails.BookingUserEmail))
                            {
                                forUser[0].Email = ApproverDetails.ApproverEmail + "," + BookingUserDetails.BookingUserEmail;
                            }
                        }
                        else
                        {
                            forUser[0].Email = ApproverDetails.ApproverEmail;
                        }
                       
                        forUser[0].Firstname = ApproverDetails.ApproverName;
                        forUser[0].Phone = ApproverDetails.Phone;
                        forUser[0].Mobile = ApproverDetails.Mobile;
                    }
                    else if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.Rejected)
                    {
                        //CLayer.Booking BookingUserDetails = BLayer.Bookings.GetOfflineBookingUserDetails(bookingId);//*This is for offline booking
                        CLayer.Booking BookingUserDetails = BLayer.Bookings.GetBookingUserDetails(bookingId);//*This is for booking

                        PrevousApproverDetails = BLayer.Bookings.GetPreviousApproverDetails(bookingId);
                        PreviousUsers = string.Empty;

                        string mobile = string.Empty;
                        string phone = string.Empty;
                        foreach (var item in PrevousApproverDetails)
                        {
                            PreviousUsers = PreviousUsers + item.ApproverEmail + ",";
                            PreviousUserName = PreviousUserName + item.ApproverName + ",";
                            BookingMobile = BookingMobile + item.Mobile + ",";
                            BookingPhone = BookingPhone + item.Phone + ",";
                        }
                        if (!string.IsNullOrEmpty(BookingUserDetails.BookingUserEmail))
                        {
                            PreviousUsers = PreviousUsers + BookingUserDetails.BookingUserEmail + ",";
                        }
                        //if (!string.IsNullOrEmpty(BookingUserDetails.bookingm))
                        //{
                        //    BookingMobile = BookingMobile + BookingUserDetails.BookingUserEmail + ",";
                        //}
                        //if (!string.IsNullOrEmpty(BookingUserDetails.BookingUserEmail))
                        //{
                        //    BookingPhone = BookingPhone + BookingUserDetails.BookingUserEmail + ",";
                        //}

                        PreviousUsers = PreviousUsers.TrimEnd(',');
                        BookingMobile = BookingMobile.TrimEnd(',');
                        BookingPhone = BookingPhone.TrimEnd(',');
                        forUser[0].Firstname = PreviousUserName;
                        forUser[0].Email = PreviousUsers;
                        forUser[0].Phone = BookingPhone;
                        forUser[0].Mobile = BookingMobile;
                        RejectionNote = BookingUserDetails.RejectionNote;
                    }
                   
                  
                    //long UserID = Convert.ToInt64(LoggedInUserID);
                    //
                    //send mail to user
                    if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.ConfirmedforBooking)
                    {
                        if (IsCreditBooking)
                        {
                            message = Common.Mailer.MessageFromHtmlSync(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingbyCredit") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        }
                        else
                        {
                            message = Common.Mailer.MessageFromHtmlSync(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingConfirmRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                        }

                    }
                    else if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.Approved)
                    {
                        message = Common.Mailer.MessageFromHtmlSync(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingApprovalRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                    }
                    else if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.Rejected)
                    {

                        message = Common.Mailer.MessageFromHtmlSync(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierBookingRejectionRequest") + bookingId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));

                    }

                    if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.ConfirmedforBooking)
                    {
                        if (IsCreditBooking)
                        {
                            customerMsg.Subject = "Booking by credit";
                        }
                        else
                        {
                            customerMsg.Subject = "Booking Confirm Request";
                        }
                    }
                    else if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.Approved)
                    {
                        customerMsg.Subject = "Booking Approval Request";
                    }
                    else if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.Rejected)
                    {
                        customerMsg.Subject = "Booking Rejection Status";
                    }

                    if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.Rejected)
                    {
                   //     To address
                        foreach (var item in PrevousApproverDetails)
                        {
                            // forUser[0].Email = item.Email;

                           // forUser[0].Email = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");



                            customerMsg.To.Add(item.ApproverEmail);

                         
                        }
                        //bcc address

//                        string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
//                        if (BccEmailsforcususer != "")
//                        {
//                            string[] emails = BccEmailsforcususer.Split(',');

//                            for (int i = 0; i < emails.Length; ++i)
//                            {
//#if !DEBUG
//                            //    emails[i] = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
//#endif
//                                customerMsg.Bcc.Add(emails[i]);

//                            }
//                        }

                        customerMsg.Body = message;
                        customerMsg.IsBodyHtml = true;
                        try
                        {

                            ml.SendMailAsyncForBookingSync(customerMsg, Common.Mailer.MailType.Reservation);


                        }
                        catch (Exception ex)
                        {
                            Common.LogHandler.HandleError(ex);
                        }


                    }
                    else
                    {
#if !DEBUG
                      //  forUser[0].Email = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
#endif
                      

                        //To address
                        if (forUser[0].Email != "")
                        {
                            string[] emails = forUser[0].Email.Split(',');

                            for (int i = 0; i < emails.Length; ++i)
                            {
#if !DEBUG
                           //     emails[i] = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
#endif
                                customerMsg.To.Add(emails[i]);
                            }
                        }
            //            forUser[0].Email = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
                        customerMsg.To.Add(forUser[0].Email);

                        //Bcc address
//                        string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
//                        if (BccEmailsforcususer != "")
//                        {
//                            string[] emails = BccEmailsforcususer.Split(',');

//                            for (int i = 0; i < emails.Length; ++i)
//                            {
//#if !DEBUG
//               //                 emails[i] = System.Configuration.ConfigurationManager.AppSettings.Get("TestMail");
//#endif
//                                customerMsg.Bcc.Add(emails[i]);
//                            }
//                        }

                        customerMsg.Body = message;
                        customerMsg.IsBodyHtml = true;
                        try
                        {

                            ml.SendMailAsyncForBookingSync(customerMsg, Common.Mailer.MailType.Reservation);


                        }
                        catch (Exception ex)
                        {
                            Common.LogHandler.HandleError(ex);
                        }
                    }

                    // sending sms
                    try
                    {
#if !DEBUG
                    //    forUser[0].Mobile = System.Configuration.ConfigurationManager.AppSettings.Get("TestSMSNumber");

#endif
                        string phone = forUser[0].Mobile;
                        if (phone == "") phone = forUser[0].Phone;
                        string smsmsg = "";
                        if (approvalstatus == (int)CLayer.ObjectStatus.BookingApprovalStatus.Rejected)
                        {
                            smsmsg = Common.SMSGateway.GetBookingRejectionMessage(forUser[0].Firstname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                           details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO), RejectionNote);
                        }
                        else
                        {
                            smsmsg = Common.SMSGateway.GetBookingApprovalMessage(forUser[0].Firstname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                         details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));

                        }
                        bool b = false;
                        phone = Common.Utils.GetMobileNo(phone);
                        //  block sms
#if !DEBUG

                  //      var smsresult =  SendMailsAndSms(bookingId);

                          if (phone != "")
                              { b =  Common.SMSGateway.SendSMSSync(smsmsg, phone); }
#endif


                        smsdata.emessage = smsmsg;
                        smsdata.message = smsmsg;
                        smsdata.phone = phone;

                        //  return   RedirectToAction("SMSTest", "Test", smsdata);




                        /*     smsmsg = Common.SMSGateway.GetSupplierBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                                 details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));

                             phone = Common.Utils.GetMobileNo(details.Mobile);
                             if (phone != "")
                             { b = await Common.SMSGateway.Send(smsmsg, phone); }

                             phone = Common.Utils.GetMobileNo(supplier.Mobile);
                             if (phone != "")
                             {

                                 b = await Common.SMSGateway.Send(smsmsg, phone);
                             }*/
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
        private async Task<bool> SendMailsAndSms(long bookingId)
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
                    if (phone != "")
                    {
                        string smsmsg = Common.SMSGateway.GetNewBookingMessage(forUser[0].Firstname + " " + forUser[0].Lastname, details.OrderNo, details.CheckIn.ToString("MMM dd,yyyy"),
                        details.CheckOut.ToString("MMM dd,yyyy"), details.PropertyTitle, details.propertyCity, details.AccommodationTypeTitle, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                        bool b = false;
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


    }
}