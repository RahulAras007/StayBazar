using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BLayer
{
    public class BookingItem
    {
        public static void UpdateCheckInAndOut(long bookingId, DateTime checkIn, DateTime checkOut)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.UpdateCheckInAndOut(bookingId, checkIn, checkOut);
        }
        public static string GetOfferAppliedAsString(long bookingItemId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetOfferAppliedAsString(bookingItemId);
        }
        public static List<CLayer.BookingItem> GetIncompleteGDSBookings()
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetIncompleteGDSBookings();
        }
        public static void AddOffer(long bookingItemId, long offerId, string offerTitle)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.AddOffer(bookingItemId, offerId, offerTitle);
        }
        public static string GetRatesApplied(long bookingItemId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetRatesApplied(bookingItemId);
        }
        public static long GetCustomBookByItemId(long bookingItemId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetCustomBookByItemId(bookingItemId);
        }



        public static string GetRatesAppliedFormatted(long bookingItemId, string dateSeperator, string commaSeperator)
        {
            string result = "";
            string s = GetRatesApplied(bookingItemId);
            if (s != null && s != "")
            {
                result = s.Replace("#", dateSeperator);
                result = result.Replace(",", commaSeperator);
            }
            return result;
        }
        public static string GetRatesAppliedHtmlFormatted(long bookingItemId, bool doNotShowForSingleItem = false)
        {
            string result = "";
            string s = GetRatesApplied(bookingItemId);
            if (s != null && s != "")
            {
                string[] rts = s.Split(',');
                string[] rtf;
                if (doNotShowForSingleItem && rts.Length < 2) return "";
                foreach (string sr in rts)
                {
                    rtf = sr.Split('#');
                    if (rtf.Length == 2)
                    {
                        if (result != "") { result = result + ", "; }
                        result = result + rtf[0] + " onwards <span class=\"fa cxprice fa-rupee\"><span class=\"cxcurele\">" + rtf[1] + "</span>.00</span> ";
                    }
                }
            }
            return result;

        }

        public static int GetBookedUserId(long bookingItemId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetBookedUserId(bookingItemId);
        }
        public static List<CLayer.BookingItem> GetAccomodationFromBookingCode(string BookingCode)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetAccomodationFromBookingCode(BookingCode);
        }

        public CLayer.ObjectStatus.StatusType GetStatus(long bookingItemId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetStatus(bookingItemId);
        }
        public static void SetStatus(long bookingItemId, CLayer.ObjectStatus.StatusType status)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.SetStatus(bookingItemId, status);
        }
        public static void DeleteAllItemsByBookingId(long bookingId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.DeleteAllItemsByBookingId(bookingId);
        }
        public static void DeleteAllTaxItemsByBookingId(long bookingId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.DeleteAllTaxItemsByBookingId(bookingId);
        }


        public static CLayer.BookingItem GetDetails(long bookingItemId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetDetails(bookingItemId);
        }

        public static CLayer.BookingItem GetTotalTaxDetails(long bookingItemId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetTotalTaxDetails(bookingItemId);
        }
        public static decimal GetFirstDayCharge(long bookingItemId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetFirstDayCharge(bookingItemId);
        }

        public static decimal GetGrantTotalTaxbyBookingId(long BookingId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetGrantTotalTaxbyBookingId(BookingId);
        }
        public static void SaveAmounts(CLayer.BookingItem data)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.SaveAmounts(data);
        }

        //***Save Amount for offlinebookings bookingdetails 
        //**Added by rahul on 29/02/20
        public static void SaveBookingDetailsAmounts(CLayer.BookingItem data)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.SaveBookingDetailsAmounts(data);
        }
        //***

        public static void SavePartialAmount(long bookingId, decimal Partialamountperc, decimal Partialamount, decimal remainingamount)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.SavePartialAmount(bookingId, Partialamountperc, Partialamount, remainingamount);
        }
        public static void SetCancAmount(long bookitemId, decimal cancAccCharge, decimal totalAmount)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.SetCancAmount(bookitemId, cancAccCharge, totalAmount);
        }
        public static void UpdateCustomBook(long bookitemId, long IsCustomBook)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.UpdateCustomBook(bookitemId, IsCustomBook);
        }
        //*Added by Rahul for Updating the values for BilingFor and BookingFor Columns in Booking Table UpdateBillingAndBookingForInBookingTable
        public static void UpdateBillingAndBookingForInBookingTable(long bookingid, string BookingFor, string BillingFor ,long AssistedCorporateUserID)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.UpdateBillingAndBookingForInBookingTable(bookingid, BookingFor, BillingFor, AssistedCorporateUserID);
        }
        public static string GetBookingFor(long BookingId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetBookingFor(BookingId);

        }
        public static string GetBillingFor(long BookingId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetBillingFor(BookingId);

        }
        public static long GetAssisted_By(long BookingId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetAssisted_By(BookingId);

        }
        //****

        public static List<CLayer.BookingItem> GetAllDetails(long bookingId, bool IsAmedus = false)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetAllDetails(bookingId, IsAmedus);
        }



        //*This is for getting details of offlinebooking
        //*Done by rahul on 06-02-2020
        public static List<CLayer.BookingItem> GetAllOfflineDetails(long bookingId, bool IsAmedus = false)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetAllOfflineDetails(bookingId, IsAmedus);
        }
        //*--End----

        public static List<CLayer.BookingItem> GetAllDetailsforoffline(long bookingId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetAllDetailsforoffline(bookingId);
        }
        public static List<CLayer.BookingItem> GetAllDetailsForPartialPay(long bookingId, bool IsAmadeus = false)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetAllDetailsForPartialPay(bookingId, IsAmadeus);
        }
        public static long getofferId(long pPropertyId, long pAccommodationId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.getofferId(pPropertyId, pAccommodationId);
        }
        public static List<CLayer.BookingItem> GetAllUnderCart(long userId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetAllUnderCart((int)CLayer.ObjectStatus.BookingStatus.Cart, userId);
        }
        public static long SaveIntialData(CLayer.BookingItem data)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            if (data.NoOfDays < 1 && data.CheckOut > data.CheckIn) data.NoOfDays = (data.CheckOut - data.CheckIn).Days;
            return bi.SaveIntialData(data);
        }

        //*Added by rahul for Saving BookingItems table data to Booking details table on 28/02/20
        public static long SaveIntialBookingDetailsData(CLayer.BookingItem data)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            if (data.NoOfDays < 1 && data.CheckOut > data.CheckIn) data.NoOfDays = (data.CheckOut - data.CheckIn).Days;
            return bi.SaveIntialBookingDetailsData(data);
        }

        //******

        public static string GetGDSCountry(long BookingId)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetGDSCountry(BookingId);

        }
        public static void  SaveGDSCountry(CLayer.BookingItem data)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
       
            bi.SaveGDSCountry(data);
        }

        public static long SaveBookingtaxdata(long BookId, long BItId, long TaxTitleId, decimal rate, decimal TaxAmountBItem)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.SaveBookingtaxdata(BookId, BItId, TaxTitleId, rate, TaxAmountBItem);
        }

        //**Added by Rahul
        //**For saving Offline booking - offlinebookingtaxesGST
        public static long SaveOfflineBookingtaxdata(long BookId, long BItId, long TaxTitleId, decimal rate, decimal TaxAmountBItem)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.SaveOfflineBookingtaxdata(BookId, BItId, TaxTitleId, rate, TaxAmountBItem);
        }

        public static void UpdateHotelConfirmNumber(long bookingId, long AccommodationID, string HotelConfirmNumber)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.UpdateHotelConfirmNumber(bookingId, AccommodationID, HotelConfirmNumber);
        }
        public static void UpdateHotelGDSError(long bookingId, long AccommodationID, string GDSError)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.UpdateGDSError(bookingId, AccommodationID, GDSError);
        }
        public static void UpdateGDSCurrencyDetails(CLayer.BookingItem pBookingItem)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            bi.UpdateGDSCurrencyDetails(pBookingItem);
        }
        public static List<CLayer.BookingItem> GetAllBookingDetailsForApproval(long bookingId, bool IsAmedus = false)
        {
            DataLayer.BookingItem bi = new DataLayer.BookingItem();
            return bi.GetAllBookingDetailsForApproval(bookingId, IsAmedus);
        }

    }
}
