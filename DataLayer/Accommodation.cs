using System;
using System.Collections.Generic;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
    public class Accommodation : DataLink
    {
        public CLayer.Accommodation Get(long AccommodationId)
        {
            CLayer.Accommodation result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, AccommodationId));
            DataTable dt = Connection.GetTable("accommodation_Get", param);
            if (dt.Rows.Count > 0)
            {
                result = new CLayer.Accommodation()
                {
                    AccommodationId = Connection.ToLong(dt.Rows[0]["AccommodationId"]),
                    AccommodationTypeId = Connection.ToInteger(dt.Rows[0]["AccommodationTypeId"]),
                    StayCategoryId = Connection.ToInteger(dt.Rows[0]["StayCategoryId"]),
                    AccommodationCount = Connection.ToInteger(dt.Rows[0]["AccommodationCount"]),
                    PropertyId = Connection.ToLong(dt.Rows[0]["PropertyId"]),
                    Description = Connection.ToString(dt.Rows[0]["Description"]),
                    Location = Connection.ToString(dt.Rows[0]["Location"]),
                    MaxNoOfPeople = Connection.ToInteger(dt.Rows[0]["MaxNoOfPeople"]),
                    MaxNoOfChildren = Connection.ToInteger(dt.Rows[0]["MaxNoOfChildren"]),
                    MinNoOfPeople = Connection.ToInteger(dt.Rows[0]["MinNoOfPeople"]),
                    BedRooms = Connection.ToInteger(dt.Rows[0]["BedRooms"]),
                    Area = Connection.ToDecimal(dt.Rows[0]["Area"]),
                    TotalAccommodations = Connection.ToInteger(dt.Rows[0]["TotalAccommodations"]),
                    Status = Connection.ToInteger(dt.Rows[0]["Status"])
                };
            }
            return result;
        }

        public List<CLayer.Accommodation> GetAllForOwnerProperty(long propertyId, int rateType)
        {
            List<CLayer.Accommodation> result = new List<CLayer.Accommodation>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, rateType));
            DataTable dt = Connection.GetTable("accommodation_GetAllForProperty", param);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Accommodation()
                {
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    Status = Connection.ToInteger(dr["Status"]),
                    StayCategory = Connection.ToString(dr["Category"]),
                    AccommodationType = Connection.ToString(dr["AType"]),
                    AccommodationCount = Connection.ToInteger(dr["AccommodationCount"]),
                    MaxNoOfPeople = Connection.ToInteger(dr["MaxNoOfPeople"]),
                    MaxNoOfChildren = Connection.ToInteger(dr["MaxNoOfChildren"]),
                    MinNoOfPeople = Connection.ToInteger(dr["MinNoOfPeople"]),
                    AccommodationTypeId = Connection.ToInteger(dr["AccommodationTypeId"]),
                    StayCategoryId = Connection.ToInteger(dr["StayCategoryId"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Description = Connection.ToString(dr["Description"]),
                //    Location = Connection.ToString(dr["Location"]),
                    BedRooms = Connection.ToInteger(dr["BedRooms"]),
                    Area = Connection.ToDecimal(dr["Area"]),
                    Rate = Connection.ToDecimal(dr["Rate"])
                });
            }
            return result;
        }

        public List<CLayer.Accommodation> GetAllAccByPropertyid(long propertyId)
        {
            List<CLayer.Accommodation> result = new List<CLayer.Accommodation>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));        
            DataTable dt = Connection.GetTable("accommodation_GetAllByPropertyId", param);
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.Accommodation()
                {
                    AccommodationId = Connection.ToLong(dr["AccommodationId"]),
                    RoomType = Connection.ToString(dr["RoomType"]),
                    RoomTypeCode = Connection.ToString(dr["RoomTypeCode"]),
                    BookingCode = Connection.ToString(dr["BookingCode"])
                  //  Availability=Connection.ToBoolean(dr["availability"])
                });
            }
            return result;
        }
        public long Save(CLayer.Accommodation data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, data.AccommodationId));
            param.Add(Connection.GetParameter("pAccommodationTypeId", DataPlug.DataType._Int, data.AccommodationTypeId));
            param.Add(Connection.GetParameter("pStayCategoryId", DataPlug.DataType._Int, data.StayCategoryId));
            param.Add(Connection.GetParameter("pAccommodationCount", DataPlug.DataType._Int, data.AccommodationCount));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Text, data.Description));
           //param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, data.Location));
            param.Add(Connection.GetParameter("pMaxNoOfPeople", DataPlug.DataType._Int, data.MaxNoOfPeople));
            param.Add(Connection.GetParameter("pMaxNoOfChildren", DataPlug.DataType._Int, data.MaxNoOfChildren));
            param.Add(Connection.GetParameter("pMinNoOfPeople", DataPlug.DataType._Int, data.MinNoOfPeople));
            param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Int, data.BedRooms));
            param.Add(Connection.GetParameter("pArea", DataPlug.DataType._Decimal, data.Area));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt,data.UpdatedById ));
            param.Add(Connection.GetParameter("pTotalAccommodations", DataPlug.DataType._Int,data.TotalAccommodations ));
            object result = Connection.ExecuteQueryScalar("accommodation_save", param);
            return Connection.ToInteger(result);
        }
        public long Save_Amadeus(CLayer.Accommodation data)
        {
            try
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, data.AccommodationId));
                param.Add(Connection.GetParameter("pAccommodationTypeId", DataPlug.DataType._Int, data.AccommodationTypeId));
                param.Add(Connection.GetParameter("pStayCategoryId", DataPlug.DataType._Int, data.StayCategoryId));
                param.Add(Connection.GetParameter("pAccommodationCount", DataPlug.DataType._Int, data.AccommodationCount));
                param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
                param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Text, data.Description));
                //param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, data.Location));
                param.Add(Connection.GetParameter("pMaxNoOfPeople", DataPlug.DataType._Int, data.MaxNoOfPeople));
                param.Add(Connection.GetParameter("pMaxNoOfChildren", DataPlug.DataType._Int, data.MaxNoOfChildren));
                param.Add(Connection.GetParameter("pMinNoOfPeople", DataPlug.DataType._Int, data.MinNoOfPeople));
                param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Int, data.BedRooms));
                param.Add(Connection.GetParameter("pArea", DataPlug.DataType._Decimal, data.Area));
                param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
                param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt, data.UpdatedById));
                param.Add(Connection.GetParameter("pTotalAccommodations", DataPlug.DataType._Int, data.TotalAccommodations));
                param.Add(Connection.GetParameter("pRoomType", DataPlug.DataType._Varchar, data.RoomType));
                param.Add(Connection.GetParameter("pRoomTypeCode", DataPlug.DataType._Varchar, data.RoomTypeCode));
                param.Add(Connection.GetParameter("pSourceofBusiness", DataPlug.DataType._Varchar, data.SourceofBusiness));
                param.Add(Connection.GetParameter("pBookingCode", DataPlug.DataType._Varchar, data.BookingCode));
                param.Add(Connection.GetParameter("pRatePlanCode", DataPlug.DataType._Varchar, data.RatePlanCode));
                param.Add(Connection.GetParameter("pRoomStayRPH", DataPlug.DataType._Varchar, data.RoomStayRPH));

                object result = Connection.ExecuteQueryScalar("accommodation_amadeus_save", param);
                return Connection.ToInteger(result);
            }
            catch(Exception ex22)
            {
                return 0;
            }

        }
        public void Accommodation_Amadeus_Delete(long PropertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, PropertyId));
            
            object result = Connection.ExecuteQueryScalar("accommodation_amadeus_Delete", param);
           
        }
        public long GetPropertyId(long accommodationId)
        {
            string sql = "Select PropertyId From accommodation Where AccommodationId=" + accommodationId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
      public long GetInventoryAPIType(long propertyid)
        {
            string sql = "Select InventoryAPIType from property where PropertyId = " + propertyid.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }

        public string GetPropertyTitle(long accommodationId)
        {
            string sql = "Select p.Title From property p inner join  accommodation  a on p.PropertyId = a.PropertyId Where a.AccommodationId=" + accommodationId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }

        public string GetAccommodationTitle(long accommodationId)
        {
            string sql = "SELECT act.Title FROM accommodation a INNER JOIN accommodationtype act ON a.AccommodationTypeId = act.AccommodationTypeId WHERE a.AccommodationId =" + accommodationId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
         public  long getAccommodationCountByStatus(int status, long BookingId)
        {
            string sql = "SELECT COUNT(bi.AccommodationId) FROM  booking b INNER JOIN bookingitems bi ON bi.BookingId=b.BookingId WHERE b.Status="+ status +" AND b.BookingId=" + BookingId;
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
         public void Delete(long AccommodationId)
         {
             List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
             param.Add(Connection.GetParameter("pAccId", DataPlug.DataType._BigInt, AccommodationId));
             Connection.ExecuteQuery("Accommodation_Delete", param);
             return;
         }

         public int SetRoomId(long AccommodationId, string RoomId)
         {
             List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
             param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, AccommodationId));
             param.Add(Connection.GetParameter("pRoomId", DataPlug.DataType._Varchar, RoomId));
             object result = Connection.ExecuteQueryScalar("property_SetRoomId", param);
             return Connection.ToInteger(result);
         }
         public string GetRoomId(long AccommodationId)
         {
             List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
             param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, AccommodationId));
             object obj = Connection.ExecuteQueryScalar("property_GetRoomId", param);
             return Connection.ToString(obj);
         }
        public void UpdateGDSAccommodationAvailability(List<string> codes,long PropertyId)
        {
            string fullcsv = string.Join(",", codes);
            fullcsv = "" + fullcsv + "";

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCodes", DataPlug.DataType._Text, fullcsv));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt , PropertyId));
            Connection.ExecuteQuery("gds_updateAccommodationAvailability", param);

           
        }
        public long Save_API(CLayer.Accommodation data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationTypeId", DataPlug.DataType._Int, data.AccommodationTypeId));
            param.Add(Connection.GetParameter("pStayCategoryId", DataPlug.DataType._Int, data.StayCategoryId));
            param.Add(Connection.GetParameter("pAccommodationCount", DataPlug.DataType._Int, data.AccommodationCount));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, data.PropertyId));
            param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Text, data.Description));
            //param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, data.Location));
            param.Add(Connection.GetParameter("pMaxNoOfPeople", DataPlug.DataType._Int, data.MaxNoOfPeople));
            param.Add(Connection.GetParameter("pMaxNoOfChildren", DataPlug.DataType._Int, data.MaxNoOfChildren));
            param.Add(Connection.GetParameter("pMinNoOfPeople", DataPlug.DataType._Int, data.MinNoOfPeople));
            param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Int, data.BedRooms));
            param.Add(Connection.GetParameter("pArea", DataPlug.DataType._Decimal, data.Area));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt, data.UpdatedById));
            param.Add(Connection.GetParameter("pTotalAccommodations", DataPlug.DataType._Int, data.TotalAccommodations));
            object result = Connection.ExecuteQueryScalar("sp_accommodation_save_byapi", param);
            return Connection.ToInteger(result);
        }
    }
}
