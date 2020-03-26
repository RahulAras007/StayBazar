using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class B2BUser
    {
        public static double GetAgentCommission(long userId)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            return b2buser.GetAgentCommission(userId);
        }
        public static void SaveAgentCommission(long userId, double commission)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            b2buser.SaveAgentCommission(userId, commission);
        }
        public static void Save(CLayer.B2BUser data)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            b2buser.Save(data);
        }

        public static long SaveCorporateUser(CLayer.User userDetails, long corporateId, int corporateUserType)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            return b2buser.SaveCorporateUser(userDetails, corporateId, corporateUserType);
        }

        public static CLayer.Role.CorporateRoles GetRole(long userId, long corporateId)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            return b2buser.GetRole(userId, corporateId);
        }
        //function CheckStaffLimit
        public static int B2B_CheckStaffLimit(long b2bId)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            int p = b2buser.B2B_CheckStaffLimit(b2bId);
            return p;
        }
        public static int GetCountMaxNoOfUsers(long pB2BId)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            int p = b2buser.GetCountMaxNoOfUsers(pB2BId);
            return p;
        }

        public static int GetCountLimitOfUsers(long pB2BId)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            int p = b2buser.GetCountLimitOfUsers(pB2BId);
            return p;
        }
        public static string Getb2bname(long pB2BId)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            string p = b2buser.Getb2bname(pB2BId);
            return p;
        }
        public static CLayer.B2BApprovers GetApproverUsers(long UserID, long BookingID)
        {
            DataLayer.B2BUser user = new DataLayer.B2BUser();
            return user.GetApproverUsers(UserID, BookingID);
        }
        public static CLayer.B2BApprovers CheckBookingApproversExist(long UserID, long BookingID)
        {
            DataLayer.B2BUser user = new DataLayer.B2BUser();
            return user.CheckBookingApproversExist(UserID, BookingID);
        }
        public static int GetNoofApprovers(long UserID)
        {
            DataLayer.B2BUser user = new DataLayer.B2BUser();
            return user.GetNoofApprovers(UserID);
        }
        public static void DeleteB2bHotels(long UserID)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            b2buser.DeleteB2bHotels(UserID);
        }
        public static void SaveB2BHotels(CLayer.B2BHotels data)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            b2buser.SaveB2BHotels(data);
        }
        public static  List<CLayer.B2BHotels> GetAssignedB2bHotelsList(long userID)
        {
            DataLayer.B2BUser b2buser = new DataLayer.B2BUser();
            return b2buser.GetAssignedB2bHotelsList(userID);
        }
        public static CLayer.B2BApprovers GetApproverUsersExists(long UserID, long BookingID)
        {
            DataLayer.B2BUser user = new DataLayer.B2BUser();
            return user.GetApproverUsersExists(UserID, BookingID);
        }
   
        public static long GetMaximumDailyEntitlement(long pUserID)
        {
            DataLayer.B2BUser user = new DataLayer.B2BUser();
            return user.GetMaximumDailyEntitlement(pUserID);
        }
        public static long GetDailyLodgingEntitlementAmount(long pUserID,  long pCityID)
        {
            DataLayer.B2BUser user = new DataLayer.B2BUser();
            return user.GetDailyLodgingEntitlement(pUserID, pCityID);
        }
        //Done By rahul
        public static List<CLayer.B2BUser> GetCorporateName()
        {
            DataLayer.B2BUser getcorporateName = new DataLayer.B2BUser();
            return getcorporateName.GetCorporateName();
        }
        //Done by rahul for taking Assisted booking StatusFlag values from b2buser on 08-01-2020
        //public static List<CLayer.B2BUser> GetAssistedBookingStatus()
        //{
        //    DataLayer.B2BUser getcorporateName = new DataLayer.B2BUser();
        //    return getcorporateName.GetAssistedBookingStatus();
        //}
        public static List<CLayer.B2BUser> GetOnCorporateUserList(int B2BId)
        {
            DataLayer.B2BUser CorporateUserName = new DataLayer.B2BUser();
            return CorporateUserName.GetOnCorporateUserList(B2BId);
        }
        //public static List<CLayer.B2BUser> getcorporatename1(string id)
        //{
        //    DataLayer.B2BUser getcorporatename1 = new DataLayer.B2BUser();
        //    return getcorporatename1.GetCorporateName1(id);
        //}
        public static List<CLayer.B2BUser> getoncorporateuserlist1(string b2bid)
        {
            DataLayer.B2BUser corporateusername1 = new DataLayer.B2BUser();
            return corporateusername1.GetOnCorporateUserList1(b2bid);
        }
        public static List<CLayer.B2BUser> getoncorporateusername(string b2bid)
        {
            DataLayer.B2BUser getoncorporateusername1 = new DataLayer.B2BUser();
            return getoncorporateusername1.getoncorporateusername(b2bid);
        }
        public static List<CLayer.B2BUser> getAllAssistedCorporateUserNames(int id,string loginid)
        {
            DataLayer.B2BUser getAllAssistedCorporateUser = new DataLayer.B2BUser();
            return getAllAssistedCorporateUser.getAllAssistedCorporateUserNames(id,loginid);
        }
        public static List<CLayer.B2BUser> getAllCorporateUserName(string id)
        {
            DataLayer.B2BUser getAllCorporateUsers = new DataLayer.B2BUser();
            return getAllCorporateUsers.getAllCorporateUserName(id);
        }
        public static List<CLayer.B2BUser> getMyAssistedUsername(string id)
        {
            DataLayer.B2BUser getAllCorporateUsers = new DataLayer.B2BUser();
            return getAllCorporateUsers.getMyAssistedUsername(id);
        }
        //getAllAssistedCorporateUnderLoginId
        //public static List<CLayer.B2BUser> GetCorporateUsers(string id)
        //{
        //    DataLayer.B2BUser taxtitGet = new DataLayer.B2BUser();
        //    return taxtitGet.GetCorporateUsers(id);
        //}
        //Done by rahul on 07-01-2020
        public static void SetAssistedFlagIfChecked(long userId)
        {
            DataLayer.B2BUser user = new DataLayer.B2BUser();
            user.SetAssistedFlagIfChecked(userId);
        }
        public static void SetAssistedFlagIfUnChecked(long userId)
        {
            DataLayer.B2BUser user = new DataLayer.B2BUser();
            user.SetAssistedFlagIfUnChecked(userId);
        }
    }

}
