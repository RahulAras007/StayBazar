using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class BookingItem : DataLink
    {
        public void UpdateCheckInAndOut(long bookingId, DateTime checkIn, DateTime checkOut)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, checkIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, checkOut));
            Connection.ExecuteQuery("bookingitems_UpdateCheckInOut", param);
        }
        public string GetRatesApplied(long bookingItemId)
        {
            string sql = "Select RatesApplied From BookingItems Where BookingItemId=" + bookingItemId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public void UpdateHotelConfirmNumber(long BookingID, long AccommodationID, string HotelConfirmNumber)
        {
            string sql = "UPDATE BookingItems SET hotelconfirmnumber='"+HotelConfirmNumber.ToString()+"' WHERE bookingID=" + BookingID + " AND AccommodationID="+ AccommodationID +"";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            //  return Connection.ToLong(obj);
        }
        public void UpdateGDSCurrencyDetails(CLayer.BookingItem pBookingItem)
        {
            try
            {
                string sql = "UPDATE BookingItems SET GDSCountry=" + pBookingItem.GDSCountry + ",GDSConversionRate=" + pBookingItem.GDSConversionRate + "," +
                     "GDSAmount=" + pBookingItem.GDSAmount + ",GDSConvertedAmount=" + pBookingItem.GDSConvertedAmount + " WHERE bookingID=" + pBookingItem.BookingId + " AND AccommodationID=" + pBookingItem.AccommodationId + "";
                object obj = Connection.ExecuteSQLQueryScalar(sql);
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
            //  return Connection.ToLong(obj);
        }
        public void UpdateGDSError(long BookingID, long AccommodationID, string GDSError)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookingID));
            param.Add(Connection.GetParameter("pAccommodationID", DataPlug.DataType._BigInt, AccommodationID));
            param.Add(Connection.GetParameter("pGDSError", DataPlug.DataType._Text, GDSError));
         
            Connection.ExecuteQuery("bookingitems_UpdateGDSError", param);
            //  return Connection.ToLong(obj);
        }
        public List<CLayer.BookingItem> GetAccomodationFromBookingCode(string BookingCode)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingCode", DataPlug.DataType._Varchar , BookingCode));
            DataTable dt = Connection.GetTable("GetAccommodationFromBookingCode", param);
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            CLayer.BookingItem rt;
            foreach (DataRow dr in dt.Rows)
            {
                rt = new CLayer.BookingItem();
                rt.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                rt.BookingId = Connection.ToLong(dr["bookingID"]);
             
                result.Add(rt);
            }
            return result;
           

        }
       public List<CLayer.BookingItem> GetIncompleteGDSBookings()
        {
                  

           DataTable dt = Connection.GetTable("GetIncompleteGDSBookings");
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            CLayer.BookingItem rt;
            foreach (DataRow dr in dt.Rows)
            {
                rt = new CLayer.BookingItem();
                rt.BookingId = Connection.ToLong(dr["BookingID"]);
                rt.Status = Connection.ToInteger(dr["BookingStatus"]);
                rt.HotelConfirmNumber = Connection.ToString(dr["HotelConfirmNumber"]);

                result.Add(rt);
            }
            return result;
        }

        public long  GetCustomBookByItemId(long bookingItemId)
        {
            string sql = "Select IsCustomBook From BookingItems Where BookingItemId=" + bookingItemId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }

        public string GetOfferAppliedAsString(long bookingItemId)
        {
            string sql = "Select OfferTitle from bookingitem_offer Where BookingItemId=" + bookingItemId.ToString();
            DataTable dt = Connection.GetSQLTable(sql);
            string result = "";
            foreach (DataRow dr in dt.Rows)
            {
                if (result.Length > 0) result = result + ", ";
                result = result + Connection.ToString(dr["OfferTitle"]);
            }
            return result;
        }
        public int GetBookedUserId(long bookingItemId)
        {
            string sql = "Select b.ByUserId From b.booking on bi.bookingitems on b.BookingId=bi.BookingId Where bi.BookingItemId=" + bookingItemId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToInteger(obj);
        }

        public void AddOffer(long bookingItemId, long offerId,string offerTitle)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBItemId", DataPlug.DataType._BigInt, bookingItemId));
            param.Add(Connection.GetParameter("pOfferId", DataPlug.DataType._BigInt, offerId));
            param.Add(Connection.GetParameter("pTitle", DataPlug.DataType._Varchar, offerTitle));
            Connection.ExecuteQuery("bookingoffer_Save", param);
        }

        public CLayer.ObjectStatus.StatusType GetStatus(long bookingItemId)
        {
            string sql = "Select Status from bookingitems where BookingItemId=" + bookingItemId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return (CLayer.ObjectStatus.StatusType)Connection.ToInteger(obj);
        }
        public void SetStatus(long bookingItemId, CLayer.ObjectStatus.StatusType status)
        {
            string sql = "Update bookingitems Set Status=" + ((int)status).ToString() + " Where BookingItemId=" + bookingItemId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void DeleteAllItemsByBookingId(long bookingId)
        {
            string sql = "Delete from  bookingitems  Where bookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void DeleteAllTaxItemsByBookingId(long bookingId)
        {
            string sql = "Delete from  bookingitems_propertytaxes  Where bookingId=" + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        
        public decimal GetFirstDayCharge(long bookingItemId)
        {
            string sql = "Select FirstDayCharge from bookingitems where BookingItemId=" + bookingItemId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToDecimal(obj);
        }
        public decimal GetGrantTotalTaxbyBookingId(long BookingId)
        {
            string sql = "SELECT SUM(bite.TotalRateTax)  FROM bookingitems bite WHERE bite.`BookingId`=" + BookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToDecimal(obj);
        }
        public string  GetGDSCountry(long BookingId)
        {
            string sql = "SELECT IFNULL(c.name,0) AS CountryName  FROM bookingitems bite INNER JOIN country c ON IFNULL(GDSCountry,0)=c.countryid  WHERE bite.`BookingId`=" + BookingId.ToString() +" LIMIT 1";
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public void SetCancAmount(long bookitemId,decimal cancAccCharge,decimal totalAmount)
        {
            string sql = "Update bookingitems set CancAccCharge=CancAccCharge+" + cancAccCharge.ToString() + " ,TotalAmount= " + totalAmount.ToString() +
                " Where BookingItemId=" + bookitemId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public void SetCancAmount(long bookitemId, decimal refund)
        {
            string sql = "Update bookingitems set TotalAmount=TotalAmount - " + refund.ToString() +
                " Where BookingItemId=" + bookitemId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }


        public void UpdateCustomBook(long bookitemId, long IsCustomBook)
        {
            string sql = "Update bookingitems set IsCustomBook=" + IsCustomBook.ToString() +
                " Where BookingItemId=" + bookitemId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }

        //*Added by Rahul for Updating the values for BilingFor and BookingFor Columns in Booking Table
        public void UpdateBillingAndBookingForInBookingTable(long bookingId, string BookingFor,string BillingFor,long AssistedCorporateUserID)
        {
            string sql = "Update booking set BookingFor = '" + BookingFor.ToString() +
                "' , BillingFor = '" + BillingFor.ToString() + "' , Assisted_By = " + AssistedCorporateUserID + " Where BookingId = " + bookingId.ToString();
            Connection.ExecuteSqlQuery(sql);
        }
        public string GetBookingFor(long BookingId)
        {
            string sql = "SELECT IFNULL(BookingFor,0) AS BookingFor  FROM booking WHERE BookingId = " + BookingId.ToString() ;
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public string GetBillingFor(long BookingId)
        {
            string sql = "SELECT IFNULL(BillingFor,0) AS BillingFor  FROM booking WHERE BookingId = " + BookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToString(obj);
        }
        public long GetAssisted_By(long BookingId)
        {
            string sql = "SELECT IFNULL(Assisted_By,0) AS Assisted_By  FROM booking WHERE BookingId = " + BookingId.ToString();
            object obj = Connection.ExecuteSQLQueryScalar(sql);
            return Connection.ToLong(obj);
        }
        //***
        public void SaveAmounts(CLayer.BookingItem data)
        {
            
            //BookingItem_Update
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingItemId", DataPlug.DataType._BigInt, data.BookingItemId));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pTotalAmount", DataPlug.DataType._Decimal, data.TotalAmount));
            param.Add(Connection.GetParameter("pTotalTax", DataPlug.DataType._Decimal, data.TotalTax));
            param.Add(Connection.GetParameter("pSBMarkup", DataPlug.DataType._Decimal, data.CommissionSB));
            param.Add(Connection.GetParameter("pB2BDiscount", DataPlug.DataType._Decimal, data.CorporateDiscountAmount));
            param.Add(Connection.GetParameter("pGuestAmount", DataPlug.DataType._Decimal, data.GuestAmount));
            param.Add(Connection.GetParameter("pFirstDayCharge", DataPlug.DataType._Decimal, data.FirstDayCharge));
            param.Add(Connection.GetParameter("pRatesApplied", DataPlug.DataType._Varchar, data.RatesApplied));
            param.Add(Connection.GetParameter("pTotalRateTax", DataPlug.DataType._Decimal, data.TotalRateTax));
            param.Add(Connection.GetParameter("pTotalGuestTax", DataPlug.DataType._Decimal, data.TotalGuestTax));         
            object id = Connection.ExecuteQueryScalar("bookingItem_Update", param);
        }

        //***Save Amount for offlinebookings bookingdetails 
        //**Added by rahul on 29/02/20
        public void SaveBookingDetailsAmounts(CLayer.BookingItem data)
        {

            //BookingItem_Update
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingItemId", DataPlug.DataType._BigInt, data.OfflineBookingItemId));
            param.Add(Connection.GetParameter("pAmount", DataPlug.DataType._Decimal, data.Amount));
            param.Add(Connection.GetParameter("pTotalAmount", DataPlug.DataType._Decimal, data.TotalAmount));
            param.Add(Connection.GetParameter("pTotalTax", DataPlug.DataType._Decimal, data.TotalTax));
            param.Add(Connection.GetParameter("pSBMarkup", DataPlug.DataType._Decimal, data.CommissionSB));
            param.Add(Connection.GetParameter("pB2BDiscount", DataPlug.DataType._Decimal, data.CorporateDiscountAmount));
            param.Add(Connection.GetParameter("pGuestAmount", DataPlug.DataType._Decimal, data.GuestAmount));
            param.Add(Connection.GetParameter("pFirstDayCharge", DataPlug.DataType._Decimal, data.FirstDayCharge));
            param.Add(Connection.GetParameter("pRatesApplied", DataPlug.DataType._Varchar, data.RatesApplied));
            param.Add(Connection.GetParameter("pTotalRateTax", DataPlug.DataType._Decimal, data.TotalRateTax));
            param.Add(Connection.GetParameter("pTotalGuestTax", DataPlug.DataType._Decimal, data.TotalGuestTax));

            param.Add(Connection.GetParameter("pPurchaseRateAfterTax", DataPlug.DataType._Decimal, data.PurchaseRateAfterTax));
            param.Add(Connection.GetParameter("pPurchaseRateBeforeTax", DataPlug.DataType._Decimal, data.PurchaseRateBeforeTax));
            param.Add(Connection.GetParameter("pSellRateAfterTax", DataPlug.DataType._Decimal, data.SellRateAfterTax));
            param.Add(Connection.GetParameter("pSellRateBeforeTax", DataPlug.DataType._Decimal, data.SellRateBeforeTax));



            object id = Connection.ExecuteQueryScalar("booking_offlinebookingdetails_Update", param);
        }
        //***


        //PartialPayment
        public void SavePartialAmount(long bookingId, decimal Partialamountperc, decimal Partialamount, decimal remainingamount)
        {
            //BookingItem_Update
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pbookingId", DataPlug.DataType._BigInt, bookingId));
            param.Add(Connection.GetParameter("pPartialamountperc", DataPlug.DataType._Decimal, Partialamountperc));
            param.Add(Connection.GetParameter("pPartialamount", DataPlug.DataType._Decimal, Partialamount));
            param.Add(Connection.GetParameter("premainingamount", DataPlug.DataType._Decimal, remainingamount));
            param.Add(Connection.GetParameter("pPaymentStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.PartialPaymentStatus.Cart));
            object id = Connection.ExecuteQueryScalar("booking_PartialAmountUpdate", param);
        }
        public long SaveIntialData(CLayer.BookingItem data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pForUserId", DataPlug.DataType._BigInt, data.ForUser));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt,data.BookingId));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt,data.AccommodationId));
            param.Add(Connection.GetParameter("pNoOfAccommodations", DataPlug.DataType._Int, data.NoOfAccommodations));
            param.Add(Connection.GetParameter("pNoOfAdults", DataPlug.DataType._Int, data.NoOfAdults));
            param.Add(Connection.GetParameter("pNoOfDays", DataPlug.DataType._Int, data.NoOfDays));
            param.Add(Connection.GetParameter("pNoOfChildren", DataPlug.DataType._Int,data.NoOfChildren));
            param.Add(Connection.GetParameter("pNoOfGuests", DataPlug.DataType._Int, data.NoOfGuests));
            param.Add(Connection.GetParameter("pLockInTime", DataPlug.DataType._DateTime, data.LockInTime));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, data.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, data.CheckOut));
            object id = Connection.ExecuteQueryScalar("bookingitem_SaveInitialData", param);
            return Connection.ToLong(id);
        }


        //*Added by rahul for Saving BookingItems table data to Booking details table on 28/02/20
        public long SaveIntialBookingDetailsData(CLayer.BookingItem data)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, data.Status));
            param.Add(Connection.GetParameter("pForUserId", DataPlug.DataType._BigInt, data.ForUser));
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, data.BookingId));
            param.Add(Connection.GetParameter("pAccommodationId", DataPlug.DataType._BigInt, data.AccommodationId));
            param.Add(Connection.GetParameter("pNoOfAccommodations", DataPlug.DataType._Int, data.NoOfAccommodations));
            param.Add(Connection.GetParameter("pNoOfAdults", DataPlug.DataType._Int, data.NoOfAdults));
            param.Add(Connection.GetParameter("pNoOfDays", DataPlug.DataType._Int, data.NoOfDays));
            param.Add(Connection.GetParameter("pNoOfChildren", DataPlug.DataType._Int, data.NoOfChildren));
            param.Add(Connection.GetParameter("pCheckIn", DataPlug.DataType._Date, data.CheckIn));
            param.Add(Connection.GetParameter("pCheckOut", DataPlug.DataType._Date, data.CheckOut));
            param.Add(Connection.GetParameter("pLockInTime", DataPlug.DataType._DateTime, data.LockInTime));
            object id = Connection.ExecuteQueryScalar("bookingdetails_SaveInitialData", param);
            return Connection.ToLong(id);
        }
        //*****

        public void SaveGDSCountry(CLayer.BookingItem data)
        { 
            string sql = "Update bookingitems set GDSCountry=" + data.GDSCountry.ToString() +
                " Where BookingItemId=" + data.BookingItemId.ToString() +" and bookingid="+ data.BookingId +"";
            Connection.ExecuteSqlQuery(sql);
        }
        public long SaveBookingtaxdata(long BookId, long BItId, long TaxTitleId, decimal rate, decimal TaxAmountBItem)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookId));
            param.Add(Connection.GetParameter("pBookingItemId", DataPlug.DataType._BigInt, BItId));
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._Int, TaxTitleId));
            param.Add(Connection.GetParameter("prate", DataPlug.DataType._Decimal, rate));
            param.Add(Connection.GetParameter("pTotalTax", DataPlug.DataType._Decimal, TaxAmountBItem));
       
            object id = Connection.ExecuteQueryScalar("bookingitem_SaveBookingtaxdata", param);
            return Connection.ToLong(id);
        }

        //**Added by Rahul
        //**For saving Offline booking - offlinebookingtaxesGST
        public long SaveOfflineBookingtaxdata(long BookId, long BItId, long TaxTitleId, decimal rate, decimal TaxAmountBItem)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, BookId));
            param.Add(Connection.GetParameter("pBookingItemId", DataPlug.DataType._BigInt, BItId));
            param.Add(Connection.GetParameter("pTaxTitleId", DataPlug.DataType._Int, TaxTitleId));
            param.Add(Connection.GetParameter("prate", DataPlug.DataType._Decimal, rate));
            param.Add(Connection.GetParameter("pTotalTax", DataPlug.DataType._Decimal, TaxAmountBItem));

            object id = Connection.ExecuteQueryScalar("booking_SaveBookingDetailstaxdata", param);
            return Connection.ToLong(id);
        }



        public List<CLayer.BookingItem> GetAllUnderCart(int status,long userId)
        {
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, status));
            param.Add(Connection.GetParameter("pUserId", DataPlug.DataType._BigInt, userId));

            DataTable dt = Connection.GetTable("Booking_GetCartItems", param);
            CLayer.BookingItem bi;
            foreach(DataRow dr in dt.Rows)
            {
                bi = new CLayer.BookingItem();
                bi.BookingItemId = Connection.ToLong(dr["BookingItemId"]);
                bi.BookingId = Connection.ToLong(dr["BookingId"]);
                bi.AccommodationTitle = Connection.ToString(dr["AccommodationType"]);
                bi.CheckIn = Connection.ToDate(dr["CheckIn"]);
                bi.CheckOut = Connection.ToDate(dr["CheckOut"]);
                bi.NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]);
                bi.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                bi.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                bi.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                bi.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                bi.Amount = Connection.ToDecimal(dr["Amount"]);
                bi.PropertyTitle = Connection.ToString(dr["PropertyTitle"]);
                bi.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                bi.ForUserFirstName = Connection.ToString(dr["ForUserFirstName"]);
                bi.ForUserLastName = Connection.ToString(dr["ForUserLastName"]);
                bi.ForUserEmail = Connection.ToString(dr["ForUserEmail"]);
                bi.ForUserMobile = Connection.ToString(dr["ForUserMobile"]);
                bi.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                bi.TotalTax = Connection.ToDecimal(dr["TotalTax"]);
                result.Add(bi);
            }
            return result;
        }

        public List<CLayer.BookingItem> GetAllDetailsForPartialPay(long bookingId,bool IsAmadeus=false)
        {
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
            string Sql = (IsAmadeus) ? "booking_GetGDSAllItemsForPartialPay" : "booking_GetAllItemsForPartialPay";
            DataTable dt = Connection.GetTable(Sql, param);
            CLayer.BookingItem bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new CLayer.BookingItem();
                bi.BookingItemId = Connection.ToLong(dr["BookingItemId"]);
                bi.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                bi.BookingId = Connection.ToLong(dr["BookingId"]);
                bi.AccommodationTitle = Connection.ToString(dr["AccommodationType"]);
                bi.CheckIn = Connection.ToDate(dr["CheckIn"]);
                bi.quantity = Connection.ToInteger(dr["quantity"]);  
                bi.CheckOut = Connection.ToDate(dr["CheckOut"]);
                bi.NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]);
                bi.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                bi.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                bi.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                bi.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                bi.Amount = Connection.ToDecimal(dr["Amount"]);
                bi.PropertyTitle = Connection.ToString(dr["PropertyTitle"]);
                bi.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                bi.PropertyId = Connection.ToLong(dr["PropertyId"]);             
                bi.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                bi.TotalTax = Connection.ToDecimal(dr["TotalTax"]);
                bi.AccommodationTypeT = Connection.ToString(dr["AccommodationType"]);
                bi.StayCategoryT = Connection.ToString(dr["StayCategory"]);
                bi.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                bi.FirstDayCharge = Connection.ToDecimal(dr["FirstDayCharge"]);
                bi.CommissionSB = Connection.ToDecimal(dr["TotalComForSB"]);


                result.Add(bi);
            }
            return result;
        }


        public List<CLayer.BookingItem> GetAllDetailsforoffline(long bookingId)
        {
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));

            DataTable dt = Connection.GetTable("booking_GetAllItems", param);
            CLayer.BookingItem bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new CLayer.BookingItem();
                bi.BookingItemId = Connection.ToLong(dr["BookingItemId"]);
                bi.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                bi.BookingId = Connection.ToLong(dr["BookingId"]);
                bi.AccommodationTitle = Connection.ToString(dr["AccommodationType"]);
                bi.CheckIn = Connection.ToDate(dr["CheckIn"]);
                bi.AccommodationTypeId = Connection.ToLong(dr["AccommodationTypeId"]);
                bi.CheckOut = Connection.ToDate(dr["CheckOut"]);
                bi.NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]);
                bi.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                bi.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                bi.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                bi.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                bi.Amount = Connection.ToDecimal(dr["Amount"]);
                bi.PropertyTitle = Connection.ToString(dr["PropertyTitle"]);
                bi.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                bi.PropertyId = Connection.ToLong(dr["PropertyId"]);
                bi.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                bi.TotalTax = Connection.ToDecimal(dr["TotalTax"]);
                bi.AccommodationTypeT = Connection.ToString(dr["AccommodationType"]);
                bi.StayCategoryT = Connection.ToString(dr["StayCategory"]);
                bi.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                bi.FirstDayCharge = Connection.ToDecimal(dr["FirstDayCharge"]);
                bi.CommissionSB = Connection.ToDecimal(dr["MarkupForSB"]);
                bi.TotalCommissionforother = Connection.ToDecimal(dr["TotalComForOther"]);  
                bi.CorporateDiscountAmount = Connection.ToDecimal(dr["B2BDiscount"]);
              
                result.Add(bi);
            }
            return result;
        }
        public List<CLayer.BookingItem> GetAllDetails(long bookingId,bool IsAmedus=false)
        {
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            try
            {
                param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
                DataTable dt;
                if (IsAmedus)
                {
                    dt = Connection.GetTable("booking_GetAllItemsAmedus", param);
                }
                else
                {
                    dt = Connection.GetTable("booking_GetAllItems", param);
                }


                CLayer.BookingItem bi;
                foreach (DataRow dr in dt.Rows)
                {
                    bi = new CLayer.BookingItem();
                    bi.BookingItemId = Connection.ToLong(dr["BookingItemId"]);
                    bi.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                    bi.BookingId = Connection.ToLong(dr["BookingId"]);
                    bi.AccommodationTitle = Connection.ToString(dr["AccommodationType"]);
                    bi.CheckIn = Connection.ToDate(dr["CheckIn"]);
                    bi.CheckOut = Connection.ToDate(dr["CheckOut"]);
                    bi.NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]);
                    bi.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                    bi.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                    bi.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                    bi.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                    bi.Amount = Connection.ToDecimal(dr["Amount"]);
                    bi.PropertyTitle = Connection.ToString(dr["PropertyTitle"]);
                    bi.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                    bi.PropertyId = Connection.ToLong(dr["PropertyId"]);
                    //bi.ForUserFirstName = Connection.ToString(dr["ForUserFirstName"]);
                    //bi.ForUserLastName = Connection.ToString(dr["ForUserLastName"]);
                    // bi.ForUserEmail = Connection.ToString(dr["ForUserEmail"]);
                    // bi.ForUserMobile = Connection.ToString(dr["ForUserMobile"]);
                    bi.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                    bi.TotalTax = Connection.ToDecimal(dr["TotalTax"]);
                    bi.AccommodationTypeT = Connection.ToString(dr["AccommodationType"]);
                    bi.StayCategoryT = Connection.ToString(dr["StayCategory"]);
                    bi.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                    bi.FirstDayCharge = Connection.ToDecimal(dr["FirstDayCharge"]);
                    //    bi.CommissionSB = Connection.ToDecimal(dr["TotalComForSB"]);
                    bi.CommissionSB = Connection.ToDecimal(dr["MarkupForSB"]);
                    bi.TotalCommissionforother = Connection.ToDecimal(dr["TotalComForOther"]);
                    bi.CorporateDiscountAmount = Connection.ToDecimal(dr["B2BDiscount"]);
                    bi.HotelConfirmNumber = Connection.ToString(dr["HotelConfirmNumber"]);
                    bi.TotalRateTax=Connection.ToDecimal(dr["TotalRateTax"]);
                    bi.CountryName = Connection.ToString(dr["CountryName"]);

                    result.Add(bi);
                }
            }
            catch(Exception ex)
            {
                result = null;
            }
          
            return result;
        }

        //*This is for getting details of offlinebooking
        //*Done by rahul on 06-02-2020
        public List<CLayer.BookingItem> GetAllOfflineDetails(long bookingId, bool IsAmedus = false)
        {
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            try
            {
                param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
                DataTable dt;
                
                    dt = Connection.GetTable("offlinebooking_GetAllItems", param);
                


                CLayer.BookingItem bi;
                foreach (DataRow dr in dt.Rows)
                {
                    bi = new CLayer.BookingItem();
                    bi.BookingItemId = Connection.ToLong(dr["BookingId"]);
                    bi.AccommodationId = Connection.ToLong(dr["AccommodatoinTypeId"]);
                    bi.BookingId = Connection.ToLong(dr["Offline_BookingId"]);
                    bi.AccommodationTitle = Connection.ToString(dr["AccommodationType"]);
                    bi.CheckIn = Connection.ToDate(dr["CheckIn_date"]);
                    bi.CheckOut = Connection.ToDate(dr["CheckOut_date"]);
                    bi.NoOfAccommodations = Connection.ToInteger(dr["NoOfRooms"]);
                    bi.NoOfAdults = Connection.ToInteger(dr["NoOfPaxAdult"]);
                    bi.NoOfChildren = Connection.ToInteger(dr["NoOfPaxChild"]);
                    bi.NoOfDays = Connection.ToInteger(dr["Noofnight"]);
                    bi.Amount = Connection.ToDecimal(dr["TotalBuyPrice"]);
                    bi.PropertyTitle = Connection.ToString(dr["PropertyTitle"]);
                    bi.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                    bi.PropertyId = Connection.ToLong(dr["PropertyId"]);
                    bi.TotalAmount = Connection.ToDecimal(dr["TotalAmtForBuyPrice"]);
                    bi.TotalTax = Connection.ToDecimal(dr["StaxForBuyPrice"]);
                    bi.AccommodationTypeT = Connection.ToString(dr["AccommodationType"]);
                    bi.StayCategoryT = Connection.ToString(dr["StayCategory"]);
                    bi.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                    bi.HotelConfirmNumber = Connection.ToString(dr["HotelConfirmNumber"]);
                    bi.CountryName = Connection.ToString(dr["CountryName"]);

                    result.Add(bi);
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }
        //********Ends Here*********
        public List<CLayer.BookingItem> GetAllBookingDetailsForApproval(long bookingId, bool IsAmedus = false)
        {
            List<CLayer.BookingItem> result = new List<CLayer.BookingItem>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            try
            {
                param.Add(Connection.GetParameter("pBookingId", DataPlug.DataType._BigInt, bookingId));
                DataTable dt;
                if (IsAmedus)
                {
                    dt = Connection.GetTable("booking_GetAllItemsamedusForApproval", param);
                }
                else
                {
                    dt = Connection.GetTable("booking_GetAllItems", param);
                }


                CLayer.BookingItem bi;
                foreach (DataRow dr in dt.Rows)
                {
                    bi = new CLayer.BookingItem();
                    bi.BookingItemId = Connection.ToLong(dr["BookingItemId"]);
                    bi.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                    bi.BookingId = Connection.ToLong(dr["BookingId"]);
                    bi.AccommodationTitle = Connection.ToString(dr["AccommodationType"]);
                    bi.CheckIn = Connection.ToDate(dr["CheckIn"]);
                    bi.CheckOut = Connection.ToDate(dr["CheckOut"]);
                    bi.NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]);
                    bi.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                    bi.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                    bi.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                    bi.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                    bi.Amount = Connection.ToDecimal(dr["Amount"]);
                    bi.PropertyTitle = Connection.ToString(dr["PropertyTitle"]);
                    bi.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                    bi.PropertyId = Connection.ToLong(dr["PropertyId"]);
                    //bi.ForUserFirstName = Connection.ToString(dr["ForUserFirstName"]);
                    //bi.ForUserLastName = Connection.ToString(dr["ForUserLastName"]);
                    // bi.ForUserEmail = Connection.ToString(dr["ForUserEmail"]);
                    // bi.ForUserMobile = Connection.ToString(dr["ForUserMobile"]);
                    bi.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                    bi.TotalTax = Connection.ToDecimal(dr["TotalTax"]);
                    bi.AccommodationTypeT = Connection.ToString(dr["AccommodationType"]);
                    bi.StayCategoryT = Connection.ToString(dr["StayCategory"]);
                    bi.DailyRate = Connection.ToDecimal(dr["DailyRate"]);
                    bi.FirstDayCharge = Connection.ToDecimal(dr["FirstDayCharge"]);
                    //    bi.CommissionSB = Connection.ToDecimal(dr["TotalComForSB"]);
                    bi.CommissionSB = Connection.ToDecimal(dr["MarkupForSB"]);
                    bi.TotalCommissionforother = Connection.ToDecimal(dr["TotalComForOther"]);
                    bi.CorporateDiscountAmount = Connection.ToDecimal(dr["B2BDiscount"]);
                    bi.HotelConfirmNumber = Connection.ToString(dr["HotelConfirmNumber"]);
                    bi.TotalRateTax = Connection.ToDecimal(dr["TotalRateTax"]);
                    bi.CountryName = Connection.ToString(dr["CountryName"]);
                    bi.cityName = Connection.ToString(dr["City"]);
                    if (IsAmedus)
                    {
                        bi.ApproverName = Connection.ToString(dr["ApproverName"]);
                        bi.RejectionNote = Connection.ToString(dr["RejectionNote"]);
                    }
                    

                    result.Add(bi);
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }

        public long getofferId(long pPropertyId,long pAccommodationId)
        {
            string sql = "";         
            if (pPropertyId > 0)
            {
               sql = "SELECT * FROM offer_property WHERE PropertyId=" + pPropertyId;
            }
            else 
            {
                 sql = " SELECT * FROM offer_accommodation WHERE AccommodationId=" + pAccommodationId;
            }
            object obj = Connection.ExecuteSQLQueryScalar(sql);          
            return Connection.ToLong(obj);
            // offer_getofferAccIdPropIdBoth`(IN pPropertyId BIGINT, IN pAccommodationId BIGINT)
        }
       

        public CLayer.BookingItem GetDetails(long bookingItemId)
        {
           CLayer.BookingItem result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pBookingItemId", DataPlug.DataType._BigInt, bookingItemId));

            DataTable dt = Connection.GetTable("bookingitems_GetDetails", param);
            CLayer.BookingItem bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new CLayer.BookingItem();
                bi.BookingItemId = Connection.ToLong(dr["BookingItemId"]);
                bi.BookingId = Connection.ToLong(dr["BookingId"]);
                bi.AccommodationTitle = Connection.ToString(dr["AccommodationType"]);
                bi.CheckIn = Connection.ToDate(dr["CheckIn"]);
                bi.CheckOut = Connection.ToDate(dr["CheckOut"]);
                bi.NoOfAccommodations = Connection.ToInteger(dr["NoOfAccommodations"]);
                bi.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                bi.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                bi.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                bi.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                bi.Amount = Connection.ToDecimal(dr["Amount"]);
                bi.PropertyTitle = Connection.ToString(dr["PropertyTitle"]);
                bi.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                bi.ForUserFirstName = Connection.ToString(dr["ForUserFirstName"]);
                bi.ForUserLastName = Connection.ToString(dr["ForUserLastName"]);
                bi.ForUserEmail = Connection.ToString(dr["ForUserEmail"]);
                bi.ForUserMobile = Connection.ToString(dr["ForUserMobile"]);
                bi.TotalAmount = Connection.ToDecimal(dr["TotalAmount"]);
                bi.TotalTax = Connection.ToDecimal(dr["TotalTax"]);
                result = bi;
            }
            return result;
        }

        public CLayer.BookingItem GetTotalTaxDetails(long bookingItemId)
        {
           CLayer.BookingItem result = null;
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pbookingItemId", DataPlug.DataType._BigInt, bookingItemId));
            DataTable dt = Connection.GetTable("bookingitems_GetTotalRateTax", param);
            CLayer.BookingItem bi;
            foreach (DataRow dr in dt.Rows)
            {
                bi = new CLayer.BookingItem();
                bi.TotalRateTax = Connection.ToDecimal(dr["TotalRateTax"]);
                bi.TotalGuestTax = Connection.ToDecimal(dr["TotalGuestTax"]);
                result = bi;
            }
            return result;
        }
    }
}
