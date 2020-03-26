using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class Rate : DataLink
    {
        public void RateRefresh(long propertyId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            Connection.ExecuteQuery("rate_Refresh", param);
        }
        public List<CLayer.Rates> GetAccommodationRates(string AccIds, DateTime checkIn, DateTime checkOut, int noOfDays, CLayer.Role.Roles rateType, CLayer.Role.Roles defaultType)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccs", DataPlug.DataType._Varchar, AccIds));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, checkIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, checkOut));
            param.Add(Connection.GetParameter("pRateType", DataPlug.DataType._Int, rateType));
            param.Add(Connection.GetParameter("pDefaultRateType", DataPlug.DataType._Int, (int)defaultType));
            param.Add(Connection.GetParameter("pBookingDayCount", DataPlug.DataType._Int, (int)noOfDays));
            DataTable dt = Connection.GetTable("accommodation_GetAvailabilityAndRate", param);

            List<CLayer.Rates> result = new List<CLayer.Rates>();
            CLayer.Rates rt;
            foreach (DataRow dr in dt.Rows)
            {
                rt = new CLayer.Rates();
                rt.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                rt.Amount = Connection.ToDecimal(dr["Amount"]);
                rt.GuestRate = Connection.ToDecimal(dr["GuestRate"]);
                rt.StartDate = Connection.ToDate(dr["startdate"]);
                rt.EndDate = Connection.ToDate(dr["enddate"]);
                rt.RateType = Connection.ToInteger(dr["AType"]);
                rt.NoofAcc = Connection.ToInteger(dr["noofacc"]);
                result.Add(rt);
            }
            return result;
        }
        //**Added by rahul on 04/03/2020
        //*This is for getting rates for property Accommodation for API
        public List<CLayer.Rates> GetAccommodationRatesForAPI(string AccIds, DateTime checkIn, DateTime checkOut, int noOfDays, CLayer.Role.Roles rateType, CLayer.Role.Roles defaultType)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccs", DataPlug.DataType._Varchar, AccIds));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, checkIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, checkOut));
            param.Add(Connection.GetParameter("pRateType", DataPlug.DataType._Int, rateType));
            param.Add(Connection.GetParameter("pDefaultRateType", DataPlug.DataType._Int, (int)defaultType));
            param.Add(Connection.GetParameter("pBookingDayCount", DataPlug.DataType._Int, (int)noOfDays));
            DataTable dt = Connection.GetTable("accommodation_GetAvailabilityAndRateForAPI", param);

            List<CLayer.Rates> result = new List<CLayer.Rates>();
            CLayer.Rates rt;
            foreach (DataRow dr in dt.Rows)
            {
                rt = new CLayer.Rates();
                rt.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                rt.Amount = Connection.ToDecimal(dr["Amount"]);
                rt.GuestRate = Connection.ToDecimal(dr["GuestRate"]);
                rt.StartDate = Connection.ToDate(dr["startdate"]);
                rt.EndDate = Connection.ToDate(dr["enddate"]);
                rt.RateType = Connection.ToInteger(dr["AType"]);
                rt.NoofAcc = Connection.ToInteger(dr["noofacc"]);
                rt.PurchaseRateAfterTax = Connection.ToDecimal(dr["DailyRate"]);
                result.Add(rt);
            }
            return result;
        }


        public void Delete(DateTime startDate, DateTime endDate, long accommodationId)
        {
            string sql = "Delete from rates where StartDate='" + startDate.ToString("yyyy-MM-dd") + "'And EndDate='" + endDate.ToString("yyyy-MM-dd") + "' And AccommodationId=" + accommodationId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }

        public List<CLayer.Rates> GetAll(long accommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            DataTable dt = Connection.GetTable("rates_GetAll", param);
            List<CLayer.Rates> result = new List<CLayer.Rates>();
            CLayer.Rates temp;
            foreach (DataRow dr in dt.Rows)
            {
                temp = new CLayer.Rates();
                temp.StartDate = Connection.ToDate(dr["StartDate"]);
                temp.EndDate = Connection.ToDate(dr["EndDate"]);
                temp.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                temp.UpdatedByUser = Connection.ToString(dr["Name"]);
                temp.UpdatedDate = Connection.ToDate(dr["UpdatedDate"]);
                result.Add(temp);
            }
            return result;
        }
        public List<CLayer.Rates> GetAllRates(long accommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            List<CLayer.Rates> result = new List<CLayer.Rates>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            DataTable dt = Connection.GetTable("ratesaccomodation_GetAll", param);
            CLayer.Rates rt1;
            foreach (DataRow dr in dt.Rows)
            {
                rt1 = new CLayer.Rates();
                rt1.RateFor = Connection.ToInteger(dr["RateFor"]);
                rt1.RateId = Connection.ToLong(dr["RateId"]);
                rt1.AccommodationId = accommodationId;
                rt1.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                rt1.CalcDailyRate = Connection.ToDecimal(dr["CalcDailyRate"]);
                rt1.GuestRate = Connection.ToDecimal(dr["GuestRate"]);
                rt1.LongTermRate = Connection.ToDecimal(dr["LongTermRate"]);
                rt1.MonthlyRate = Connection.ToDecimal(dr["MonthlyRate"]);
                rt1.WeeklyRate = Connection.ToDecimal(dr["WeeklyRate"]);
                result.Add(rt1);
            }
            return result;
        }


        public List<CLayer.Rates> GetAllRatesByAccId(long accommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            List<CLayer.Rates> result = new List<CLayer.Rates>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            DataTable dt = Connection.GetTable("ratesaccomodation_GetAllByAccId", param);
            CLayer.Rates rt1;
            foreach (DataRow dr in dt.Rows)
            {
                rt1 = new CLayer.Rates();
                rt1.RateFor = Connection.ToInteger(dr["RateFor"]);
                rt1.RateId = Connection.ToLong(dr["RateId"]);
                rt1.AccommodationId = accommodationId;
                rt1.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                rt1.CalcDailyRate = Connection.ToDecimal(dr["CalcDailyRate"]);
                rt1.GuestRate = Connection.ToDecimal(dr["GuestRate"]);
                rt1.LongTermRate = Connection.ToDecimal(dr["LongTermRate"]);
                rt1.MonthlyRate = Connection.ToDecimal(dr["MonthlyRate"]);
                rt1.WeeklyRate = Connection.ToDecimal(dr["WeeklyRate"]);
                rt1.StartDate = Connection.ToDate(dr["StartDate"]);
                rt1.EndDate = Connection.ToDate(dr["EndDate"]);
                rt1.UpdatedBy = Connection.ToInteger(dr["UpdatedBy"]);     
      
                result.Add(rt1);
            }
            return result;
        }

      
        public List<CLayer.Rates> GetCalcDailyRates(long accommodationId)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            List<CLayer.Rates> result = new List<CLayer.Rates>();
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, accommodationId));
            DataTable dt = Connection.GetTable("ratesaccomodation_GetCalcDailyRates", param);
            CLayer.Rates rt1;
            foreach (DataRow dr in dt.Rows)
            {
                rt1 = new CLayer.Rates();
                rt1.RateFor = Connection.ToInteger(dr["RateFor"]);
                rt1.RateId = Connection.ToLong(dr["RateId"]);
                rt1.AccommodationId = accommodationId;
                rt1.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                rt1.CalcDailyRate = Connection.ToDecimal(dr["CalcDailyRate"]);
                rt1.GuestRate = Connection.ToDecimal(dr["GuestRate"]);
                rt1.LongTermRate = Connection.ToDecimal(dr["LongTermRate"]);
                rt1.MonthlyRate = Connection.ToDecimal(dr["MonthlyRate"]);
                rt1.WeeklyRate = Connection.ToDecimal(dr["WeeklyRate"]);
                result.Add(rt1);
            }
            return result;
        }

        public long Save(CLayer.Rates data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            Boolean isB2c = (data.RateFor == (int)CLayer.Role.Roles.Customer);

            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt,data.AccommodationId));
            param.Add(Connection.GetParameter("pRateId", DataPlug.DataType._BigInt, data.RateId));
            param.Add(Connection.GetParameter("pRateFor", DataPlug.DataType._Int, data.RateFor));
            param.Add(Connection.GetParameter("pDailyRate", DataPlug.DataType._Decimal, Math.Round(data.DailyRate)));
            param.Add(Connection.GetParameter("pWeeklyRate", DataPlug.DataType._Decimal, Math.Round(data.WeeklyRate)));
            param.Add(Connection.GetParameter("pMonthlyRate", DataPlug.DataType._Decimal, Math.Round(data.MonthlyRate)));
            param.Add(Connection.GetParameter("pLongTermRate", DataPlug.DataType._Decimal, Math.Round(data.LongTermRate)));
            param.Add(Connection.GetParameter("pGuestRate", DataPlug.DataType._Decimal, Math.Round(data.GuestRate)));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._Date, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._Date, data.EndDate));
            param.Add(Connection.GetParameter("IsB2CMarkup", DataPlug.DataType._Bool, isB2c));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pUser", DataPlug.DataType._BigInt, data.UpdatedBy));

            object obj = Connection.ExecuteQueryScalar("rates_Save", param);
            return Connection.ToLong(obj);
        }
        public long APISave(CLayer.Rates data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            Boolean isB2c = (data.RateFor == (int)CLayer.Role.Roles.Customer);

            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, data.AccommodationId));
            param.Add(Connection.GetParameter("pRateFor", DataPlug.DataType._Int, data.RateFor));
            param.Add(Connection.GetParameter("pDailyRate", DataPlug.DataType._Decimal, Math.Round(data.DailyRate)));
            param.Add(Connection.GetParameter("pWeeklyRate", DataPlug.DataType._Decimal, Math.Round(data.WeeklyRate)));
            param.Add(Connection.GetParameter("pMonthlyRate", DataPlug.DataType._Decimal, Math.Round(data.MonthlyRate)));
            param.Add(Connection.GetParameter("pLongTermRate", DataPlug.DataType._Decimal, Math.Round(data.LongTermRate)));
            param.Add(Connection.GetParameter("pGuestRate", DataPlug.DataType._Decimal, Math.Round(data.GuestRate)));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._Date, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._Date, data.EndDate));
            param.Add(Connection.GetParameter("IsB2CMarkup", DataPlug.DataType._Bool, isB2c));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pUser", DataPlug.DataType._BigInt, data.UpdatedBy));
            param.Add(Connection.GetParameter("pSellRateAfterTax", DataPlug.DataType._BigInt, data.SellRateAfterTax));

            object obj = Connection.ExecuteQueryScalar("sp_API_rates_Save", param);
            return Connection.ToLong(obj);
        }

        public long DynamicSave(CLayer.Rates data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            Boolean isB2c = (data.RateFor == (int)CLayer.Role.Roles.Customer);

            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt,data.AccommodationId));
            param.Add(Connection.GetParameter("pRateId", DataPlug.DataType._BigInt, data.RateId));
            param.Add(Connection.GetParameter("pRateFor", DataPlug.DataType._Int, data.RateFor));
            param.Add(Connection.GetParameter("pDailyRate", DataPlug.DataType._Decimal, Math.Round(data.DailyRate)));
            param.Add(Connection.GetParameter("pWeeklyRate", DataPlug.DataType._Decimal, Math.Round(data.WeeklyRate)));
            param.Add(Connection.GetParameter("pMonthlyRate", DataPlug.DataType._Decimal, Math.Round(data.MonthlyRate)));
            param.Add(Connection.GetParameter("pLongTermRate", DataPlug.DataType._Decimal, Math.Round(data.LongTermRate)));
            param.Add(Connection.GetParameter("pGuestRate", DataPlug.DataType._Decimal, Math.Round(data.GuestRate)));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._Date, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._Date, data.EndDate));
            param.Add(Connection.GetParameter("IsB2CMarkup", DataPlug.DataType._Bool, isB2c));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pUser", DataPlug.DataType._BigInt, data.UpdatedBy));

            object obj = Connection.ExecuteQueryScalar("dynamicrates_Save", param);
            return Connection.ToLong(obj);
        }
        public long GDSRateSave(CLayer.GDSRates data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            Boolean isB2c = (data.RateFor == (int)CLayer.Role.Roles.Customer);

            param.Add(Connection.GetParameter("pRateId", DataPlug.DataType._BigInt, data.RateId));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, data.AccommodationId));
            param.Add(Connection.GetParameter("pRateFor", DataPlug.DataType._Int, data.RateFor));
            param.Add(Connection.GetParameter("pRate", DataPlug.DataType._Decimal, Math.Round(data.Rate)));
            param.Add(Connection.GetParameter("pStartDate", DataPlug.DataType._Date, data.StartDate));
            param.Add(Connection.GetParameter("pEndDate", DataPlug.DataType._Date, data.EndDate));
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pUpdatedBy", DataPlug.DataType._BigInt, data.UpdatedBy));
            param.Add(Connection.GetParameter("pBookingCode", DataPlug.DataType._Varchar, data.BookingCode));

            object obj = Connection.ExecuteQueryScalar("gdsrates_Save", param);
            return Connection.ToLong(obj);
        }
        public List<CLayer.Rates> GetAll(DateTime startDate, DateTime endDate, long accommodationId)
        {
            List<CLayer.Rates> result = new List<CLayer.Rates>();
            StringBuilder sql = new StringBuilder();
            sql.Append("Select RateId,RateFor,DailyRate,MonthlyRate,WeeklyRate,LongTermRate,GuestRate,StartDate,EndDate From rates Where StartDate='");
            sql.Append(startDate.Year);
            sql.Append("-");
            sql.Append(startDate.Month);
            sql.Append("-");
            sql.Append(startDate.Day);
            sql.Append("' And EndDate='");
            sql.Append(endDate.Year);
            sql.Append("-");
            sql.Append(endDate.Month);
            sql.Append("-");
            sql.Append(endDate.Day);
            sql.Append("' And AccommodationId=");
            sql.Append(accommodationId);

            DataTable dt = Connection.GetSQLTable(sql.ToString());
            CLayer.Rates rt;
            foreach (DataRow dr in dt.Rows)
            {
                rt = new CLayer.Rates();
                rt.RateFor = Connection.ToInteger(dr["RateFor"]);
                rt.RateId = Connection.ToLong(dr["RateId"]);
                rt.AccommodationId = accommodationId;
                rt.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                rt.MonthlyRate = Connection.ToDecimal(dr["MonthlyRate"]);
                rt.WeeklyRate = Connection.ToDecimal(dr["WeeklyRate"]);
                rt.LongTermRate = Connection.ToDecimal(dr["LongTermRate"]);
                rt.GuestRate = Connection.ToDecimal(dr["GuestRate"]);
                rt.StartDate = Connection.ToDate(dr["StartDate"]);
                rt.EndDate = Connection.ToDate(dr["EndDate"]);
                result.Add(rt);
            }

            return result;
        }
    }
}
