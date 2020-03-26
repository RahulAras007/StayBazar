using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class B2BUser : DataLink
    {
        //Doen by rahul
        //public List<CLayer.B2BUser> GetCorporateUsers(string id)
        //{

        //    DataTable dt = Connection.GetSQLTable("SELECT u.FirstName as Name,b2b.UserId as ID,b2b.userType as Type from b2b_user b2b Join user u on u.UserId=b2b.UserId WHERE b2b.B2BId=" + id);
        //    List<CLayer.B2BUser> CorporateUserlist = new List<CLayer.B2BUser>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        CorporateUserlist.Add(new CLayer.B2BUser()
        //        {
        //            UserId = Connection.ToInteger(dr["ID"]),
        //            FirstName = Connection.ToString(dr["Name"]),
        //            UserType =Connection.ToInteger(dr["Type"])
        //        });
        //    }
        //    return CorporateUserlist;
        //}
        public List<CLayer.B2BUser> GetCorporateName()
        {
            DataTable dt = Connection.GetSQLTable("Select u.FirstName,b2b.B2BId from b2b_user b2b Join user u on u.UserId = b2b.B2BId group by u.FirstName ");
            List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.B2BUser()
                {
                    B2BId = Connection.ToInteger(dr["B2BId"]),
                    FirstName = Connection.ToString(dr["FirstName"])
                });
            }
            return result;
        }
        //public List<CLayer.B2BUser> GetAssistedBookingStatus()
        //{
        //    DataTable dt = Connection.GetSQLTable("Select AssistedBooking_Flag from b2b_user where b2bid=2384 ");
        //    List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        result.Add(new CLayer.B2BUser()
        //        {
        //            AssistedBooking_Flag = Connection.ToString(dr["AssistedBooking_Flag"])
        //        });
        //    }
        //    return result;
        //}
        public List<CLayer.B2BUser> GetOnCorporateUserList(int B2BId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._Int, B2BId));
            DataTable dt = Connection.GetTable("Get_CorporateUserName", param);
            List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
            foreach(DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.B2BUser()
                {
                    UserId=Connection.ToInteger(dr["ID"]),
                    FirstName=Connection.ToString(dr["Name"])
                });
            }
            return result;
        }
        //public List<CLayer.B2BUser> GetCorporateName1(string id)
        //{
        //    DataTable dt = Connection.GetSQLTable("Select u.FirstName,b2b.B2BId from b2b_user b2b Join user u on u.UserId=b2b.B2BId where b2b.B2BId=" + id);
        //    List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        result.Add(new CLayer.B2BUser()
        //        {
        //            B2BId = Connection.ToInteger(dr["B2BId"]),
        //            FirstName = Connection.ToString(dr["FirstName"])

        //        });
        //    }
        //    return result;
        //}

        //This is for getting Corporate Id for login user Id from B2b_user table - this is Corporate Profile Page
        public List<CLayer.B2BUser> getoncorporateusername(string id)
        {
            DataTable dt = Connection.GetSQLTable("Select b2b.B2BId from b2b_user b2b where b2b.userid = " + id);
            List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.B2BUser()
                {
                    B2BId = Connection.ToInteger(dr["B2BId"]),
                });
            }
            return result;
        }
        //This is for getting CorporateUser or AssistedCorporate User who are under Corporate-this is Corporate Profile Page
        public List<CLayer.B2BUser> getAllAssistedCorporateUserNames(int id,string loginid)
        {
            DataTable dt = Connection.GetSQLTable("Select u.FirstName,b2b.UserId from b2b_user b2b Join user u on u.UserId = b2b.UserId where b2b.B2BId = " + id + " and b2b.userid <>  " + loginid + " group by u.FirstName ");
            List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
            foreach(DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.B2BUser()
                {
                    UserId = Connection.ToInteger(dr["UserId"]),
                    FirstName = Connection.ToString(dr["FirstName"])
                });
            }
            return result;
        }
        public List<CLayer.B2BUser> getAllCorporateUserName(string id)
        {

            DataTable dt = Connection.GetSQLTable("Select u.FirstName,b2b.UserId from b2b_user b2b Join user u on u.UserId = b2b.UserId where b2b.B2BId = " + id );
            List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.B2BUser()
                {
                    UserId = Connection.ToInteger(dr["UserId"]),
                    FirstName = Connection.ToString(dr["FirstName"])
                });
            }
            return result;
        }
        public List<CLayer.B2BUser> getMyAssistedUsername(string id)
        {
            DataTable dt = Connection.GetSQLTable("Select UserId,FirstName from user where MyAssistedBookerId = " + id);
            List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
            foreach(DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.B2BUser()
                {
                    UserId=Connection.ToInteger(dr["UserId"]),
                    FirstName=Connection.ToString(dr["FirstName"]),
                });
            }
            return result;
        }
        public List<CLayer.B2BUser> GetOnCorporateUserList1(string B2BId)
        {
            DataTable dt = Connection.GetSQLTable("Select u.FirstName,b2b.UserId from b2b_user b2b Join user u on u.UserId = b2b.UserId where b2b.B2BId = " + B2BId );
            List<CLayer.B2BUser> result = new List<CLayer.B2BUser>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.B2BUser()
                {
                    UserId = Connection.ToInteger(dr["UserId"]),
                    FirstName = Connection.ToString(dr["FirstName"])

                });
            }
            return result;
        }

        //----
        public void SaveAgentCommission(long userId,double commission)
        {
            string sql = "Update b2b set MarkupPercent=" + commission.ToString() + " Where b2bId=" + userId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }

        public double GetAgentCommission(long userId)
        {
            string sql = "Select MarkupPercent From b2b Where b2bId=" + userId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToDouble(obj);
        }

        public void DeleteB2bHotels(long userID)
        {
            string sql = "DELETE FROM b2b_Hotels WHERE userid=" + userID.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        //Corperate CheckStaffLimit for saving 
        public int B2B_CheckStaffLimit(long b2bId)
        {                
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, b2bId));
             param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int,(int)CLayer.ObjectStatus.StatusType.Deleted ));
       // B2B_CheckStaffLimit`(pB2BId BIGINT,pStatus INT
            object stafflimit = Connection.ExecuteQueryScalar("B2B_CheckStaffLimit", param);
            int s = Connection.ToInteger(stafflimit);
            return s;           
        }
         
        public void Save(CLayer.B2BUser data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, data.UserId));
            param.Add(Connection.GetParameter("pB2BId", DataPlug.DataType._BigInt, data.B2BId));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, data.UserType));
            Connection.ExecuteQuery("b2buser_Save", param);
        }

        public long SaveCorporateUser(CLayer.User userDetails,long corporateId,int corporateUserType)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userDetails.UserId));
            param.Add(Connection.GetParameter("pCorporateId", DataPlug.DataType._BigInt, corporateId));
            param.Add(Connection.GetParameter("pCorporateUserType", DataPlug.DataType._Int, corporateUserType));
            param.Add(Connection.GetParameter("pSalutationId", DataPlug.DataType._Int, userDetails.SalutationId));
            param.Add(Connection.GetParameter("pFirstName", DataPlug.DataType._Varchar, userDetails.FirstName));
            param.Add(Connection.GetParameter("pLastName", DataPlug.DataType._Varchar, userDetails.LastName));
            param.Add(Connection.GetParameter("pDateOfBirth", DataPlug.DataType._Date, userDetails.DateOfBirth));
            param.Add(Connection.GetParameter("pSex", DataPlug.DataType._Int, userDetails.Sex));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, userDetails.UserTypeId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, userDetails.Status));
            param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, userDetails.Email));
            param.Add(Connection.GetParameter("pMaximumDailyEntitlement", DataPlug.DataType._BigInt, userDetails.MaximumDailyEntitlement));
            param.Add(Connection.GetParameter("pGradeID", DataPlug.DataType._BigInt, userDetails.GradeID));
            param.Add(Connection.GetParameter("pCostCentre", DataPlug.DataType._BigInt, userDetails.CostCentre));
            object result = Connection.ExecuteQueryScalar("corporate_SaveUser", param);
            return Connection.ToInteger(result);
        }
        public void SaveB2BHotels(CLayer.B2BHotels  data)
        {
            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("pB2BHotel_ID", DataPlug.DataType._BigInt, data.B2BHotel_ID));
            parameter.Add(Connection.GetParameter("pPropertyID", DataPlug.DataType._BigInt, data.PropertyID));
            parameter.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, data.Title));
            parameter.Add(Connection.GetParameter("pHotelOrder", DataPlug.DataType._Int, data.HotelOrder));
            parameter.Add(Connection.GetParameter("pUserId", DataPlug.DataType._Int, data.UserID));
            Connection.ExecuteQueryScalar("b2bHotels_save", parameter);
        }

        public CLayer.Role.CorporateRoles GetRole(long userId,long corporateId)
        {
            string sql = "Select userType from b2b_user Where userId=" + userId.ToString() + " And B2BId=" + corporateId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            if(dt != null && dt.Rows.Count > 0)
            {
                return (CLayer.Role.CorporateRoles) Connection.ToInteger(dt.Rows[0]["userType"]);
            }
            return CLayer.Role.CorporateRoles.Staff;   
        }

        public int GetCountMaxNoOfUsers(long pB2BId)
        {
            int statud = (int)CLayer.ObjectStatus.StatusType.Deleted;
            string sql = "SELECT COUNT(u.UserId) FROM b2b_user b INNER JOIN `user` u ON  b.userId = u.userId  WHERE b.b2bId=" + pB2BId + " AND u.Status <>" + statud;           
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);            
        }
        public int GetCountLimitOfUsers(long pB2BId)
        {
            string sql = "SELECT MaximumStaff  FROM b2b WHERE  B2BId=" + pB2BId;
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);            
        }
        public string Getb2bname(long pB2BId)
        {
            string sql = "SELECT Name  FROM b2b WHERE  B2BId=" + pB2BId;
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public CLayer.B2BApprovers GetApproverUsers(long userId,long BookingID)
        {
            string sql = "  SELECT b.userid,IFNULL(a.approver_id,0) AS b2b_approver_id,a.approver_order AS Approver_Order,IFNULL(ba.approval_status,0) AS ApprovalStatus,(SELECT CONCAT(Firstname,' ',lastname) FROM `user` WHERE userid=a.approver_id) AS  UserName,(SELECT email FROM `user` WHERE userid=a.approver_id) AS approveremail,(SELECT passwordhash FROM `user` WHERE userid=a.approver_id) AS passwordhash " +
                         " FROM b2b_approvers a INNER JOIN USER b ON a.user_id=b.userid AND a.user_id=" + userId.ToString() + "" +
                         " LEFT JOIN booking_approvals ba ON ba.approver_id = a.approver_id AND ba.booking_id = " + BookingID.ToString() + " LIMIT 1";

            //string sql = " SELECT b.userid,IFNULL(a.approver_id, 0) AS b2b_approver_id, a.approver_order AS Approver_Order,IFNULL(ba.approval_status, 0) AS ApprovalStatus,(SELECT CONCAT(Firstname, ' ', lastname) FROM `user` WHERE userid = a.approver_id) AS UserName,(SELECT email FROM `user` WHERE userid = a.approver_id) AS approveremail,(SELECT passwordhash FROM `user` WHERE userid = a.approver_id) AS passwordhash," +
            //            " b1.approvedstatus  FROM b2b_approvers a" +
            //            " INNER JOIN USER b ON a.user_id = b.userid AND a.user_id = " + userId.ToString() + "" +
            //            " LEFT JOIN booking_approvals ba ON ba.approver_id = a.approver_id AND ba.booking_id = " + BookingID.ToString() + "" +
            //            "  LEFT JOIN booking b1 ON ba.booking_id = b1.bookingid   WHERE b1.approvedstatus != 2   LIMIT 1";

            DataTable dt = Connection.GetSQLTable(sql);
            CLayer.B2BApprovers objResult = new CLayer.B2BApprovers();
            if (dt != null && dt.Rows.Count > 0)
            {
                objResult.b2b_approver_id = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
                objResult.user_id = Connection.ToLong(dt.Rows[0]["userid"]);
                objResult.approver_id = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
                objResult.approver_order= Connection.ToInteger(dt.Rows[0]["Approver_Order"]);
                objResult.created_by = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
             //   objResult.created_date = Connection.ToDate(dt.Rows[0]["created_date"]);
                objResult.approver_email = Connection.ToString(dt.Rows[0]["approveremail"]);
                objResult.approver_password  = Connection.ToString(dt.Rows[0]["passwordhash"]);
                objResult.username = Connection.ToString(dt.Rows[0]["UserName"]);
                objResult.status = Connection.ToInteger(dt.Rows[0]["ApprovalStatus"]);

            }
            return objResult;
        }
        public CLayer.B2BApprovers GetApproverUsersExists(long userId, long BookingID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingID", DataPlug.DataType._BigInt, BookingID));
            param.Add(Connection.GetParameter("pUserID", DataPlug.DataType._BigInt, userId));
            DataTable dt = Connection.GetTable("CheckBooking_Approvers", param);
            CLayer.B2BApprovers objResult = new CLayer.B2BApprovers();
            if (dt != null && dt.Rows.Count > 0)
            {
                objResult.b2b_approver_id = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
                objResult.user_id = Connection.ToLong(dt.Rows[0]["userid"]);
                objResult.approver_id = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
                objResult.approver_order = Connection.ToInteger(dt.Rows[0]["Approver_Order"]);
                objResult.created_by = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
                //   objResult.created_date = Connection.ToDate(dt.Rows[0]["created_date"]);
                objResult.approver_email = Connection.ToString(dt.Rows[0]["approveremail"]);
                objResult.approver_password = Connection.ToString(dt.Rows[0]["passwordhash"]);
                objResult.username = Connection.ToString(dt.Rows[0]["UserName"]);
                objResult.status = Connection.ToInteger(dt.Rows[0]["ApprovalStatus"]);

            }
            //  return ids;
            return objResult;
        }


       public CLayer.B2BApprovers CheckBookingApproversExist(long userId, long BookingID)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pUserID", DataPlug.DataType._BigInt, userId));
            param.Add(Connection.GetParameter("pBookingID", DataPlug.DataType._BigInt, BookingID));
            DataTable dt = Connection.GetTable("CheckBookingApproversExists", param);
            CLayer.B2BApprovers objResult = new CLayer.B2BApprovers();
            if (dt != null && dt.Rows.Count > 0)
            {
                objResult.b2b_approver_id = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
                objResult.user_id = Connection.ToLong(dt.Rows[0]["userid"]);
                objResult.approver_id = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
                objResult.approver_order = Connection.ToInteger(dt.Rows[0]["Approver_Order"]);
                objResult.created_by = Connection.ToLong(dt.Rows[0]["b2b_approver_id"]);
                //   objResult.created_date = Connection.ToDate(dt.Rows[0]["created_date"]);
                objResult.approver_email = Connection.ToString(dt.Rows[0]["approveremail"]);
                objResult.approver_password = Connection.ToString(dt.Rows[0]["passwordhash"]);
                objResult.username = Connection.ToString(dt.Rows[0]["UserName"]);
                objResult.status = Connection.ToInteger(dt.Rows[0]["ApprovalStatus"]);

            }
            //  return ids;
            return objResult;
        }
        public List<CLayer.B2BHotels> GetAssignedB2bHotelsList(long userID)
        {
            List<CLayer.B2BHotels> Hotels = new List<CLayer.B2BHotels>();
            string sql = "SELECT b.*,p.country AS CountryID,(SELECT NAME FROM COUNTRY WHERE COUNTRYID=p.country) AS Country,p.state AS StateID," +
                        "(SELECT NAME FROM state WHERE stateid = p.state AND countryid = p.country) AS State," +
                        " CityID, city FROM b2b_hotels b INNER JOIN property p ON b.propertyid = p.propertyid WHERE userid = "+ userID + "";
 //"SELECT * FROM b2b_hotels where userid=" + userID.ToString() + "";
            DataTable dataTable = Connection.GetSQLTable(sql); 
            foreach (DataRow dr in dataTable.Rows)
            {
                Hotels.Add(new CLayer.B2BHotels()
                {
                    PropertyID = Connection.ToLong(dr["PropertyID"]),
                    Title  = Connection.ToString(dr["Title"]),
                    HotelOrder  = Connection.ToInteger(dr["HotelOrder"]),
                    City = Connection.ToString(dr["City"]),
                    State= Connection.ToString(dr["State"]),
                    Country = Connection.ToString(dr["Country"])
                });
            }
            return Hotels;
        }
        public int GetNoofApprovers(long pUserID)
        {
            string sql = "SELECT COUNT(*)  FROM b2b_approvers WHERE user_id=" + pUserID;
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }
        public long GetMaximumDailyEntitlement(long pUserID)
        {
            string sql = "SELECT ifnull(MaximumDailyEntitlement,0)  FROM b2b_user WHERE userid=" + pUserID;
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        public long GetDailyLodgingEntitlement(long pUserID,long pCityID)
        {
            List<DataPlug.Parameter> parameter = new List<DataPlug.Parameter>();
            parameter.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, pUserID));
            parameter.Add(Connection.GetParameter("pCityID", DataPlug.DataType._BigInt, pCityID));

            DataTable dt = Connection.GetTable("GetDailyLodgingEntilementAmount", parameter);
            long objResult = 0;
            if (dt != null && dt.Rows.Count > 0)
            {
                objResult = Connection.ToLong(dt.Rows[0]["Amount"]);
            }
            return objResult;
        }
        //Done By Rahul 07-01-2020
        public void SetAssistedFlagIfChecked(long userId)
        {
            Connection.ExecuteSqlQuery("UPDATE `user` SET `AssistedBooking_Flag`='1' where `userid` = " + userId.ToString());
            return;
        }
        public void SetAssistedFlagIfUnChecked(long userId)
        {
            Connection.ExecuteSqlQuery("UPDATE `user` SET `AssistedBooking_Flag`='0' where `userid` = " + userId.ToString());
            return;
        }
    }
}
