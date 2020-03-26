using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Review : DataLink
    {
        public long Save(CLayer.Review feedback)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, feedback.PropertyId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, feedback.UserId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, feedback.BookingId));
            param.Add(Connection.GetParameter("pRating", DataPlug.DataType._Int, feedback.Rating));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Varchar, feedback.Description));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.NotVerified));

            param.Add(Connection.GetParameter("pAccommodationCity", DataPlug.DataType._Varchar, feedback.AccommodationCity));
            param.Add(Connection.GetParameter("pAccessibility", DataPlug.DataType._Int, feedback.Accessibility));
            param.Add(Connection.GetParameter("pEasiness", DataPlug.DataType._Int, feedback.Easiness));
            param.Add(Connection.GetParameter("pCleanlinessBed", DataPlug.DataType._Int, feedback.CleanlinessBed));
            param.Add(Connection.GetParameter("pCleanlinessBath", DataPlug.DataType._Int, feedback.CleanlinessBath));
            param.Add(Connection.GetParameter("pSleepQuality", DataPlug.DataType._Int, feedback.SleepQuality));
            param.Add(Connection.GetParameter("pStaffbehave", DataPlug.DataType._Int, feedback.Staffbehave));
            param.Add(Connection.GetParameter("pOverallExp", DataPlug.DataType._Int, feedback.OverallExp));
            param.Add(Connection.GetParameter("pConsiderfornext", DataPlug.DataType._Int, feedback.Considerfornext));

            object result = Connection.ExecuteQueryScalar("review_Save", param);
            return Connection.ToLong(result);

        }
        public long Chechreviewids(CLayer.Review feedback)
        {
            long pPropertyId = feedback.PropertyId;
            long pBookingId = feedback.BookingId;
            string sql = "SELECT COUNT(BookingId) FROM review WHERE BookingId=" + pBookingId + " and PropertyId=" + pPropertyId + "";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);

        }
        public long Chechreviewids1(long PropertyId, long BookingId)
        {
            long pPropertyId = PropertyId;
            long pBookingId = BookingId;
            string sql = "SELECT COUNT(BookingId) FROM review WHERE BookingId=" + pBookingId + " and PropertyId=" + pPropertyId + "";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);

        }
        public long VerifiedUpdate(CLayer.Review feedback)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, feedback.PropertyId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, feedback.UserId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, feedback.BookingId));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Varchar, feedback.Description));
            param.Add(Connection.GetParameter("pUpdatedUserId", DataPlug.DataType._BigInt, feedback.UpdatedBy));
            param.Add(Connection.GetParameter("pUpdatedDate", DataPlug.DataType._DateTime, feedback.UpdatedDate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, feedback.Status));
            param.Add(Connection.GetParameter("pIsRecommended", DataPlug.DataType._Bool, feedback.Recommended));

            param.Add(Connection.GetParameter("pAccommodationCity", DataPlug.DataType._Varchar, feedback.AccommodationCity));
            param.Add(Connection.GetParameter("pAccessibility", DataPlug.DataType._Int, feedback.Accessibility));
            param.Add(Connection.GetParameter("pEasiness", DataPlug.DataType._Int, feedback.Easiness));
            param.Add(Connection.GetParameter("pCleanlinessBed", DataPlug.DataType._Int, feedback.CleanlinessBed));
            param.Add(Connection.GetParameter("pCleanlinessBath", DataPlug.DataType._Int, feedback.CleanlinessBath));
            param.Add(Connection.GetParameter("pSleepQuality", DataPlug.DataType._Int, feedback.SleepQuality));
            param.Add(Connection.GetParameter("pStaffbehave", DataPlug.DataType._Int, feedback.Staffbehave));
            param.Add(Connection.GetParameter("pOverallExp", DataPlug.DataType._Int, feedback.OverallExp));
            param.Add(Connection.GetParameter("pConsiderfornext", DataPlug.DataType._Int, feedback.Considerfornext));

            //param.Add(Connection.GetParameter("pRating", DataPlug.DataType._Int, feedback.Rating));
            object result = Connection.ExecuteQueryScalar("review_Update", param);
            return Connection.ToLong(result);
        }

        public void Delete(long PropertyId, long UserId, long BookingId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingId));
            Connection.ExecuteQueryScalar("review_Delete", param);
        }
        public List<CLayer.Review> GetUserId(int Start, int Limit, long id, int status)
        {
            List<CLayer.Review> result = new List<CLayer.Review>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, id));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            DataSet ds = Connection.GetDataSet("review_GetUserId", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Review()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    UserId = Connection.ToLong(dr["UserId"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    Rating = Connection.ToInteger(dr["Rating"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    ReviewDate = Connection.ToDate(dr["ReviewDate"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Title = Connection.ToString(dr["Title"]),
                    Location = Connection.ToString(dr["Location"]),
                    Address = Connection.ToString(dr["Address"]),
                    Description = Connection.ToString(dr["Description"]),
                    UpdatedBy = Connection.ToLong(dr["UpdatedBy"]),
                    UpdatedDate = Connection.ToDate(dr["UpdatedDate"]),
                    Recommended = Connection.ToBoolean(dr["IsRecommended"]),

                    AccommodationCity = Connection.ToString(dr["AccommodationCity"]),
                    Accessibility = Connection.ToInteger(dr["Accessibilitytoproperty"]),
                    Easiness = Connection.ToInteger(dr["EasinessInCheckIn"]),
                    CleanlinessBed = Connection.ToInteger(dr["CleanlinessBedroom"]),
                    CleanlinessBath = Connection.ToInteger(dr["CleanlinessBathroom"]),
                    SleepQuality = Connection.ToInteger(dr["SleepQuality"]),
                    Staffbehave = Connection.ToInteger(dr["StaffBehavior"]),
                    Considerfornext = Connection.ToInteger(dr["ConsiderForNextTravel"]),
                    OverallExp = Connection.ToInteger(dr["OverallExperience"]),

                    //BookingItemId=Connection.ToLong(dr["BookingItemId"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }

        public List<CLayer.Review> GetpropertyId(long propertyId, int Status)
        {
            List<CLayer.Review> result = new List<CLayer.Review>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            DataSet ds = Connection.GetDataSet("review_GetPropertyId", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Review()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    UserId = Connection.ToLong(dr["UserId"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    Rating = Connection.ToInteger(dr["Rating"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    ReviewDate = Connection.ToDate(dr["ReviewDate"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Title = Connection.ToString(dr["Title"]),
                    Location = Connection.ToString(dr["Location"]),
                    Address = Connection.ToString(dr["Address"]),
                    Description = Connection.ToString(dr["Description"]),
                    BookingItemId = Connection.ToLong(dr["BookingItemId"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }

        public List<CLayer.Review> GetAll(int Start, int Limit)
        {
            List<CLayer.Review> result = new List<CLayer.Review>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("review_GetAll", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Review()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    UserId = Connection.ToLong(dr["UserId"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    Rating = Connection.ToInteger(dr["Rating"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    ReviewDate = Connection.ToDate(dr["ReviewDate"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Title = Connection.ToString(dr["Title"]),
                    Location = Connection.ToString(dr["Location"]),
                    Address = Connection.ToString(dr["Address"]),
                    Description = Connection.ToString(dr["Description"]),
                    UpdatedBy = Connection.ToLong(dr["UpdatedBy"]),
                    UpdatedDate = Connection.ToDate(dr["UpdatedDate"]),
                    Recommended = Connection.ToBoolean(dr["IsRecommended"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }

        public List<CLayer.Review> AllNameSearch(string SearchString, int SearchItem, int SearchStatus, int Start, int Limit)
        {
            List<CLayer.Review> result = new List<CLayer.Review>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pSearchItem", DataPlug.DataType._Int, SearchItem));
            param.Add(Connection.GetParameter("pSearchStatus", DataPlug.DataType._Int, SearchStatus));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("review_GetNameSearch", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Review()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    UserId = Connection.ToLong(dr["UserId"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    Rating = Connection.ToInteger(dr["Rating"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    ReviewDate = Connection.ToDate(dr["ReviewDate"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Title = Connection.ToString(dr["Title"]),
                    Location = Connection.ToString(dr["Location"]),
                    Address = Connection.ToString(dr["Address"]),
                    Description = Connection.ToString(dr["Description"]),
                    UpdatedBy = Connection.ToLong(dr["UpdatedBy"]),
                    UpdatedDate = Connection.ToDate(dr["UpdatedDate"]),
                    Recommended = Connection.ToBoolean(dr["IsRecommended"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }
        public List<CLayer.Review> RecommendedView(string SearchString, int Status, bool Recommended, int Start, int Limit)
        {
            List<CLayer.Review> result = new List<CLayer.Review>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pRecommended", DataPlug.DataType._Bool, Recommended));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("review_Recommended", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.Review()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    UserId = Connection.ToLong(dr["UserId"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    Rating = Connection.ToInteger(dr["Rating"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    ReviewDate = Connection.ToDate(dr["ReviewDate"]),
                    FirstName = Connection.ToString(dr["FirstName"]),
                    LastName = Connection.ToString(dr["LastName"]),
                    Title = Connection.ToString(dr["Title"]),
                    Location = Connection.ToString(dr["Location"]),
                    Address = Connection.ToString(dr["Address"]),
                    Description = Connection.ToString(dr["Description"]),
                    UpdatedBy = Connection.ToLong(dr["UpdatedBy"]),
                    UpdatedDate = Connection.ToDate(dr["UpdatedDate"]),
                    Recommended = Connection.ToBoolean(dr["IsRecommended"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }

        public CLayer.Review Get(long PropertyId, long UserId, long BookingId)
        {
            CLayer.Review rev = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, UserId));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingId));
            DataTable dt = Connection.GetTable("review_Details", param);
            if (dt.Rows.Count > 0)
            {
                rev = new CLayer.Review();
                rev.PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]);
                rev.UserId = Connection.ToLong(dt.Rows[0]["UserId"]);
                rev.BookingId = Connection.ToLong(dt.Rows[0]["BookingId"]);
                rev.Rating = Connection.ToInteger(dt.Rows[0]["Rating"]);
                rev.Status = Connection.ToInteger(dt.Rows[0]["Status"]);
                rev.ReviewDate = Connection.ToDate(dt.Rows[0]["ReviewDate"]);
                rev.FirstName = Connection.ToString(dt.Rows[0]["FirstName"]);
                rev.LastName = Connection.ToString(dt.Rows[0]["LastName"]);
                rev.Title = Connection.ToString(dt.Rows[0]["Title"]);
                rev.Location = Connection.ToString(dt.Rows[0]["Location"]);
                rev.Address = Connection.ToString(dt.Rows[0]["Address"]);
                rev.Description = Connection.ToString(dt.Rows[0]["Description"]);
                rev.UpdatedBy = Connection.ToLong(dt.Rows[0]["UpdatedBy"]);
                rev.UpdatedDate = Connection.ToDate(dt.Rows[0]["UpdatedDate"]);
                rev.Recommended = Connection.ToBoolean(dt.Rows[0]["IsRecommended"]);
                rev.AccommodationCity = Connection.ToString(dt.Rows[0]["AccommodationCity"]);
                rev.Accessibility = Connection.ToInteger(dt.Rows[0]["Accessibilitytoproperty"]);
                rev.Easiness = Connection.ToInteger(dt.Rows[0]["EasinessInCheckIn"]);
                rev.CleanlinessBed = Connection.ToInteger(dt.Rows[0]["CleanlinessBedroom"]);
                rev.CleanlinessBath = Connection.ToInteger(dt.Rows[0]["CleanlinessBathroom"]);
                rev.SleepQuality = Connection.ToInteger(dt.Rows[0]["SleepQuality"]);
                rev.Staffbehave = Connection.ToInteger(dt.Rows[0]["StaffBehavior"]);
                rev.Considerfornext = Connection.ToInteger(dt.Rows[0]["ConsiderForNextTravel"]);
                rev.OverallExp = Connection.ToInteger(dt.Rows[0]["OverallExperience"]);


            }
            return rev;
        }


        public List<string> GetReviewSendBookingList()
        {
            List<string> ids = new List<string>();
            DataTable dt = Connection.GetTable("GetReviewSendBookingListforEmail");
            foreach (DataRow dr in dt.Rows)
            {
                ids.Add(Connection.ToString(dr["BookingId"]) + "#" + Connection.ToString(dr["ForUserEmail"]) + "#" + Connection.ToString(dr["bookinguseremail"]));
            }
            return ids;
        }

        //public List<CLayer.Property> ProprtyGet(long PropertyId)
        //{
        //    List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
        //    param.Add(Connection.GetParameter("pStatusproperty", DataPlug.DataType._BigInt, PropertyId));         
        //    DataTable dt = Connection.GetTable("review_propertyGet", param);
        //    List<CLayer.Property> result = new List<CLayer.Property>();
        //    CLayer.Property temp;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        temp = new CLayer.Property();
        //        temp.PropertyId = Connection.ToLong(dr["PropertyId"]);
        //        temp.Title = Connection.ToString(dr["Title"]);             
        //        temp.bookingitemsTotalAmount = Connection.ToDecimal(dr["bookingitemsTotalAmount"]);
        //        temp.Location = Connection.ToString(dr["Location"]);//Vyttilla Eranakullam
        //        temp.City = Connection.ToString(dr["CityName"]);
        //        temp.FileName = Connection.ToString(dr["FileName"]);
        //        result.Add(temp);
        //    }
        //    return result;
        //}

    }
}


