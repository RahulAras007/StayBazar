using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using StayBazar.Lib.Security;

namespace StayBazar.Controllers
{
    public class CorporateUsersController : Controller
    {
        #region Custom Methods
        private Models.CorporateModel InitialData()
        {
            Models.CorporateModel data = new Models.CorporateModel();
            long cid = 0;
            long.TryParse(User.Identity.GetUserId(), out cid);
            data.Users = BLayer.B2B.GetAllCorporateUsers(cid);
            
            return data;
        }
        #endregion

        // GET: /CorporateUsers/
        public ActionResult Index()
        {
            try
            {
                Models.CorporateModel data = InitialData();
                return View(data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View();
        }

        public ActionResult SaveDetails(Models.CorporateUserModel data, string DdlApproverID, string ApproverOrdersList,string pB2BHotels)
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
                {
                }
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
                usr.MaximumDailyEntitlement = data.MaximumDailyEntitlement;
                usr.GradeID = data.GradeID;
                usr.CostCentre = data.CostCentre;
                usr.UserTypeId = (int)CLayer.Role.Roles.CorporateUser;
                //long cid = 0;
                long.TryParse(User.Identity.GetUserId(), out cid);
                long userId = BLayer.B2BUser.SaveCorporateUser(usr, cid, data.UserTypeId);
                if (userId < 0) { return View("_general", "-2"); }
                    

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

                BLayer.ApprovalOrder.DeleteB2bApproversRecords(data.UserId);

                //ApprovalOrder Status  ./Start
                CLayer.ApprovalOrder approvalOrder = new CLayer.ApprovalOrder();
                if (DdlApproverID != "" && ApproverOrdersList != "")
                {
                    string[] ApproverIDsArray = DdlApproverID.Split(',');
                    string[] ApproverOrdersListArray = ApproverOrdersList.Split(',');

                    int arrayApproverID = 0;

                    for (int arrayApproverOrder = 0; arrayApproverOrder < ApproverOrdersListArray.Length; arrayApproverID++, arrayApproverOrder++)
                    {
                        approvalOrder.b2b_approver_id = data.B2BApproverID;
                        if (data.UserId > 0)
                        {
                            approvalOrder.user_id = data.UserId;
                        }
                        else
                        {
                            approvalOrder.user_id = cid;
                        }
                        approvalOrder.approver_id = Convert.ToInt64(ApproverIDsArray[arrayApproverID]);
                        approvalOrder.approver_order = Convert.ToInt32(ApproverOrdersListArray[arrayApproverOrder]);
                        approvalOrder.created_by = (int)cid;
                        BLayer.ApprovalOrder.SaveApprovalOrder(approvalOrder);
                    }
                }

                //  ./End

                //Manage b2b_hotels
                if(!string.IsNullOrEmpty(pB2BHotels.Replace("-","")))
                {
                    SaveB2bHotels(data, pB2BHotels);
                }

              

                //Manage b2b_hotels end


                Models.CorporateModel mdata = InitialData();
                return View("_List", mdata);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("_general");
        }

        public void SaveB2bHotels(Models.CorporateUserModel data,string pB2BHotels)
        {
            BLayer.B2BUser.DeleteB2bHotels(data.UserId);

            long cid = 0;
            long.TryParse(User.Identity.GetUserId(), out cid);

            CLayer.B2BHotels  objB2BHotels = new CLayer.B2BHotels();
            if (pB2BHotels != "")
            {
                string[] HotelIDsArray = pB2BHotels.Split('-');
                if(HotelIDsArray.Length>0)
                {
                    string[] HotelListArray = HotelIDsArray[0].Split(',');
                    for (int i = 0; i < HotelListArray.Length; i++)
                    {
                        objB2BHotels.B2BHotel_ID = 0;
                        if (data.UserId > 0)
                        {
                            objB2BHotels.UserID = data.UserId;
                        }
                        else
                        {
                            objB2BHotels.UserID = cid;
                        }
                        objB2BHotels.HotelOrder = Convert.ToInt64(HotelIDsArray[1].Split(',')[i]);
                        objB2BHotels.PropertyID = Convert.ToInt64(HotelListArray[i]);
                        objB2BHotels.Title = data.HotelName;

                        BLayer.B2BUser.SaveB2BHotels(objB2BHotels);
                    }
                }

             
            }

        }

        public ActionResult GetDetails(long id)
        {
            try
            {
                Session["id"] = id;
                
                Models.CorporateUserModel cm = new Models.CorporateUserModel();
                if (id != 0)
                {
                    CLayer.User usr = BLayer.User.Get(id);
                    if (usr != null)
                    {
                        long cid = 0;
                        long.TryParse(User.Identity.GetUserId(), out cid);
                        cm.UserId = id;
                        cm.id = id;
                        Session["id"] = id;
                        cm.SalutationId = usr.SalutationId;
                        cm.FirstName = usr.FirstName;
                        cm.LastName = usr.LastName;
                        cm.StatusId = usr.Status;
                        cm.UserTypeId = (int)BLayer.B2BUser.GetRole(id, cid);
                        cm.Email = usr.Email;
                        cm.Sex = usr.Sex;
                        cm.StatusId = usr.Status;
                        cm.MaximumDailyEntitlement = usr.MaximumDailyEntitlement;
                        cm.GradeID = usr.GradeID;
                        cm.CostCentre = usr.CostCentre;
                        List<CLayer.Address> addr = BLayer.Address.GetOnUser(id);
                        if (addr.Count > 0)
                        {
                            cm.AddressId = addr[0].AddressId;
                            cm.Address = addr[0].AddressText;
                            cm.Phone = addr[0].Phone;
                            cm.Mobile = addr[0].Mobile;
                            cm.State = addr[0].State;
                            cm.CityId = addr[0].CityId;
                            cm.City = addr[0].City;
                            cm.CountryId = addr[0].CountryId;
                        }
                        cm.LoadPlaces();
                        if (addr[0].CityId > 0)
                        {
                            cm.CityId = addr[0].CityId;
                        }
                        else
                        {
                            cm.City = addr[0].City;
                        }
                    }
                    cm.ApprovalOrder = BLayer.ApprovalOrder.GetAssignedB2bApproversList(id);
                    cm.B2BHotelsList = BLayer.B2BUser.GetAssignedB2bHotelsList(id);
                }
                return View("_Details", cm);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                Redirect("~/ErrorPage");
            }
            return View();
        }

        public ActionResult Remove(long id)
        {
            try
            {
                BLayer.User.SetDeleteStatus(id);
                Models.CorporateModel mdata = InitialData();
                return View("_List", mdata);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                Redirect("~/ErrorPage");
            }
            return View();
        }

        //To chnage `user` - `IsApprover` to false
        public int SetIsApproverStatus(long isApproverStatus)
        {
            BLayer.ApprovalOrder.SetIsApproverStatus(isApproverStatus);
            return 1;
        }


    }
}