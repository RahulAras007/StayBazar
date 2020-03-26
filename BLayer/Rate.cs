using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;


namespace BLayer
{
    public class Rate
    {

        public static void RateRefresh(long propertyId)
        {
            DataLayer.Rate r = new DataLayer.Rate();
            r.RateRefresh(propertyId);
        }
        // 1 = success, 0 = failed

        public static int BookAccommodations(List<CLayer.BookingAccDetails> bookingInfo, DateTime checkIn, DateTime checkOut, long userId, long bookingId, long offlinebookingId, string orderNo = "", List<CLayer.RoomStaysResult> objRoomStayResult=null,List<CLayer.Rates> pAmedusRates=null, int CountryID=0 )
        {
            long PropId = BLayer.Accommodation.GetPropertyId(bookingInfo[0].AccommodationId);
            decimal tax = BLayer.PropertyTax.GetTotalTax(PropId);
            long InventoryAPIID = BLayer.Accommodation.GetInventoryAPIType(PropId);
            int result = 0;
            List<long> accIds = new List<long>();
            //long PId = BLayer.Bookings.GetPropertyId(bookingId);


            foreach (CLayer.BookingAccDetails t in bookingInfo)
            {
                accIds.Add(t.AccommodationId);
            }
            //List<CLayer.Tax> taxes = BLayer.Tax.GetAll()
            List<CLayer.Rates> accrates;
            CLayer.Role.Roles rle = BLayer.User.GetRole(userId);
            if (rle == CLayer.Role.Roles.CorporateUser || rle == CLayer.Role.Roles.Corporate)
                accrates = BLayer.Rate.GetTotalRates(accIds, checkIn, checkOut, CLayer.Role.Roles.Corporate, userId,InventoryAPIID);
            else
                accrates = BLayer.Rate.GetTotalRates(accIds, checkIn, checkOut, CLayer.Role.Roles.Customer, userId, InventoryAPIID);

            int cnt, i;


            //Amadeus Rates
           int InventoryAPIType= BLayer.Property.GetInventoryAPITypeId(PropId);
       
            List<CLayer.Rates> AmedusRatesList=new List<CLayer.Rates>();
            if (InventoryAPIType==(int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                accrates = pAmedusRates;
            }
            //Amadeus Rates end

            cnt = bookingInfo.Count;
            CLayer.Booking bk = new CLayer.Booking();
            CLayer.Booking bk1 = new CLayer.Booking();
            bk.BookingDate = DateTime.Today;
            bk.CheckIn = checkIn;
            bk.CheckOut = checkOut;
            bk.NoOfDays = (checkOut - checkIn).Days;
            bk.Status = (int)CLayer.ObjectStatus.BookingStatus.Cart;
            bk.BookingId = bookingId;
            bk.ByUserId = userId;
            bk.InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropId);

            bk.BookingId = BLayer.Bookings.SaveInitialData(bk);
            // CLayer.Role.Roles rle = (CLayer.Role.Roles) BLayer.User.GetRole(userId);
            BLayer.Bookings.SetPaymentRefNo(bk.BookingId, rle, orderNo);


            //*Added by rahul for saving initial data to offline bookings on 28/02/20
            bk1.BookingDate = DateTime.Today;
            bk1.CheckIn = checkIn;
            bk1.CheckOut = checkOut;
            bk1.NoOfDays = (checkOut - checkIn).Days;
            bk1.Status = (int)CLayer.ObjectStatus.BookingStatus.Cart;
            bk1.BookingId = offlinebookingId;
            bk1.ByUserId = userId;
            bk1.InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropId);
            bk1.BookingId = BLayer.Bookings.SaveOfflineBookingInitialData(bk1);

            

            //*Added by rahul on 28/02/2020 for storing order number in offlinebookings
            BLayer.Bookings.SetOfflinePaymentRefNo(bk1.BookingId, rle, orderNo);
            //****

            bookingId = bk.BookingId;
            List<CLayer.BookingItem> items = new List<CLayer.BookingItem>();
            List<CLayer.BookingItem> ofitems = new List<CLayer.BookingItem>();
            CLayer.BookingItem bitem;
            CLayer.BookingItem ofbitem;
            DateTime lockIn = DateTime.Now;
            int minutes = 0;
            int.TryParse(BLayer.Settings.GetValue(CLayer.Settings.LOCKIN_TIME), out minutes);
            lockIn = lockIn.AddMinutes(minutes);




            //// change no of accommodation for external inventory
            //int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(PropId);
            //List<CLayer.Rates> newaccrates = new List<CLayer.Rates>();
            //if (InventoryAPIType != 0)
            //{
            //    if (accrates.Count > 0)
            //    {
            //        string HotelId = BLayer.Property.GetHotelId(PropId);
            //        if (HotelId != null && HotelId != "")
            //        {
            //            foreach (CLayer.Rates item in accrates)
            //            {
            //                string RoomId = BLayer.Accommodation.GetRoomId(item.AccommodationId);
            //                if (RoomId != null && RoomId != "")
            //                {
            //                    if (item.NoofAcc < 1)
            //                    {
            //                        item.NoofAcc = 10;
            //                    }
            //                }
            //                newaccrates.Add(item);
            //            }
            //        }
            //    }
            //}
            //if (newaccrates.Count == 0)
            //{
            //    newaccrates = accrates;
            //}


            CLayer.BookingItem bGDSCountryItem = new CLayer.BookingItem();

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
                            bitem.LockInTime = lockIn;
                            bitem.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            bitem.GDSCountry = CountryID;
                            items.Add(bitem);
                            bitem.BookingItemId = BLayer.BookingItem.SaveIntialData(bitem);
                            bGDSCountryItem = bitem;


                            //*Added by rahul for Saving BookingDetails data on 28/02/20
                            ofbitem = new CLayer.BookingItem();
                            ofbitem.CheckIn = checkIn;
                            ofbitem.CheckOut = checkOut;
                            ofbitem.BookingId = bk1.BookingId;
                            ofbitem.AccommodationId = rt.AccommodationId;
                            ofbitem.ForUser = userId;
                            ofbitem.NoOfAccommodations = bookingInfo[i].AccCount;
                            ofbitem.NoOfAdults = bookingInfo[i].Adults;
                            ofbitem.NoOfChildren = bookingInfo[i].Children;
                            ofbitem.NoOfGuests = bookingInfo[i].Guest;
                            ofbitem.LockInTime = lockIn;
                            ofbitem.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            ofbitem.GDSCountry = CountryID;
                            ofitems.Add(ofbitem);
                            ofbitem.OfflineBookingItemId = BLayer.BookingItem.SaveIntialBookingDetailsData(ofbitem);
                        }
                    }
                }
            }

            //update gdscountry in booking items
             BLayer.BookingItem.SaveGDSCountry(bGDSCountryItem);


            //calculate tax and amount for each accommodation
            //considering booking is for accommodations in a single property
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
                        if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                        {
                            item.TotalAmount = (rat.Amount + rat.TotalRateTax);
                        }
                        else
                        {
                            item.TotalAmount = ((rat.Amount + rat.TotalRateTax) * item.NoOfAccommodations) + guestRate;
                        }
                            
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
                        if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                        {
                            item.TotalRateTax = Math.Round(rat.TotalRateTax);
                            item.TotalGuestTax = Math.Round(rat.TotalGuestTax);
                        }
                        else
                        {
                            item.TotalRateTax = Math.Round(rat.TotalRateTax * item.NoOfAccommodations);
                            item.TotalGuestTax = Math.Round(rat.TotalGuestTax * item.NoOfGuests);
                        }
                        
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
                       
                        // save tax rate for each bookingitemid

                        long BItId = item.BookingItemId;
                        long BookId = item.BookingId;
                        long Pid = BLayer.Accommodation.GetPropertyId(rat.AccommodationId);
                        List<CLayer.Tax> tax1 = BLayer.Tax.GetPropertyTaxById(Pid);

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
            BLayer.Bookings.UpdateAmounts(bookingId);

            //***This is for saving rates for offline bookings - booking details
            foreach (CLayer.BookingItem item in ofitems)
            {
                foreach (CLayer.Rates rat in accrates)
                {
                    if (item.AccommodationId == rat.AccommodationId)
                    {
                        totalTax = 0;
                        ratApplied.Clear();
                        guestRate = ((rat.GuestRate + rat.TotalGuestTax) * item.NoOfGuests);
                        if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                        {
                            item.TotalAmount = (rat.Amount + rat.TotalRateTax);
                        }
                        else
                        {
                            item.TotalAmount = ((rat.Amount + rat.TotalRateTax) * item.NoOfAccommodations) + guestRate;
                        }

                        totalTax = BLayer.Tax.GetTaxOnAmount(ordinaryTaxes, item.TotalAmount, item.OfflineBookingItemId);
                        totalTax = totalTax + BLayer.Tax.GetTaxOnAmount(onTotalAmountTaxes, item.TotalAmount + totalTax, item.OfflineBookingItemId);
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
                        if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                        {
                            item.TotalRateTax = Math.Round(rat.TotalRateTax);
                            item.TotalGuestTax = Math.Round(rat.TotalGuestTax);
                        }
                        else
                        {
                            item.TotalRateTax = Math.Round(rat.TotalRateTax * item.NoOfAccommodations);
                            item.TotalGuestTax = Math.Round(rat.TotalGuestTax * item.NoOfGuests);
                        }

                        // include tax with amount                    
                        item.TotalAmount = Math.Round(item.TotalAmount);
                        item.GuestAmount = Math.Round(guestRate);

                        item.RatesApplied = ratApplied.ToString();
                        item.CorporateDiscountAmount = rat.CorpDiscount;
                        item.CommissionSB = rat.SBMarkup;
                        item.SellRateAfterTax = rat.SellRateAfterTax;
                        item.SellRateBeforeTax = rat.SellRateBeforeTax;
                        item.PurchaseRateAfterTax = rat.PurchaseRateAfterTax;
                        item.PurchaseRateBeforeTax = rat.PurchaseRateBeforeTax;
                        if (item.NoOfGuests > 0)
                        {
                            item.CommissionSB += rat.SBGuestMarkup;
                            item.CorporateDiscountAmount += rat.CorpGuestDiscount;
                        }



                        BLayer.BookingItem.SaveBookingDetailsAmounts(item);

                        // save tax rate for each bookingitemid

                        long BItId = item.BookingItemId;
                        long BookId = item.BookingId;
                        long Pid = BLayer.Accommodation.GetPropertyId(rat.AccommodationId);
                        List<CLayer.Tax> tax1 = BLayer.Tax.GetPropertyTaxById(Pid);

                        foreach (CLayer.Tax tx in tax1)
                        {
                            decimal TaxAmountBItem = Math.Round(((tx.Rate / tax) * ((rat.TotalRateTax * item.NoOfAccommodations) + (rat.TotalGuestTax * item.NoOfGuests))));
                            BLayer.BookingItem.SaveOfflineBookingtaxdata(BookId, BItId, tx.TaxTitleId, tx.Rate, TaxAmountBItem);
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

            BLayer.Bookings.UpdateOfflinebookingAmounts(bk1.BookingId);




            //add paypal commision outside india users
            long UserId = BLayer.Bookings.GetBookedByUserId(bookingId);
            CLayer.User dat = BLayer.User.GetCountrUser(UserId);
            string ct = dat.Country;
            if (ct != "India")
            {
                decimal paypalcomm = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_COMMISSION));
                decimal GrandTotalAmt = Math.Round(BLayer.Bookings.GetTotal(bookingId));
                decimal TotalAmtIncPayComm  = Math.Round(GrandTotalAmt + (paypalcomm * GrandTotalAmt / 100));
                BLayer.Bookings.SavePaypalComm((GrandTotalAmt*paypalcomm/100), bookingId);
                BLayer.Bookings.UpdateTotalAmtIncPayComm(TotalAmtIncPayComm, bookingId);
                BLayer.Bookings.UpdateOfflineBookingTotalAmtIncPayComm(TotalAmtIncPayComm, bk1.BookingId);
               
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
 

        public static List<CLayer.Rates> GetTotalRates(List<long> accIds, DateTime checkIn, DateTime checkOut, CLayer.Role.Roles rateType, long loggedInUserId,long InventoryAPIID)
        {
            List<CLayer.Rates> arates;
            StringBuilder ids = new StringBuilder();
            bool isCorporate = false;
            long TAMInventoryAPIID = InventoryAPIID;
            long corpId = 0;
            if (rateType == CLayer.Role.Roles.Corporate)
            {
                CLayer.Role.Roles rle = BLayer.User.GetRole(loggedInUserId);
                if (rle == CLayer.Role.Roles.CorporateUser)
                {
                    corpId = BLayer.B2B.GetCorporateIdOfUser(loggedInUserId);
                }
                else
                    corpId = loggedInUserId;
                isCorporate = true;
            }
            

            foreach (long id in accIds)
            {
                ids.Append(",");
                ids.Append(id);
            }
            ids.Remove(0, 1);

            List<CLayer.Rates> result = new List<CLayer.Rates>();
            if (TAMInventoryAPIID == 4 || TAMInventoryAPIID == 5)
            {
                arates = GetAccommodationRatesForAPI(ids.ToString(), checkIn, checkOut, rateType);
            }
            else
            {
                arates = GetAccommodationRates(ids.ToString(), checkIn, checkOut, rateType);
            }
            
            DateTime currentDate;
            decimal total, currate, curguest_rate, guest,PurchaseRateaAfterTaxs,totalPurchaseRateAfterTaxs,PurchaseRateBeforeTax,SellRateAfterTax,SellRateBeforeTax,Prch_Rate_AT, Prch_Rate_ATX,PurchaseRateAfterTax;

            int cnt, idx;
            int days = (checkOut - checkIn).Days;
            long propertyId = BLayer.Accommodation.GetPropertyId(accIds[0]);
            int inventory = 0;
            CLayer.Discount dicnt = null;

            if (isCorporate)
            {

                dicnt = BLayer.Discount.GetDiscount(corpId, propertyId);
            }

            CLayer.RateCommission rc = BLayer.Property.GetCommission(propertyId);

            
            List<CLayer.Offers> propertyOffers = BLayer.Offers.GetForPropertyCalc(propertyId, checkIn, checkOut);
            bool hasPrOffers = (propertyOffers.Count > 0);
            List<CLayer.Offers> accOffers;

            CLayer.Offers propOffer = null;
            if (hasPrOffers) propOffer = propertyOffers[0];
            CLayer.Offers curOffer = null;
            DateTime offEndDate, offStartDate;
            offStartDate = offEndDate = DateTime.Today;

            List<CLayer.BookingItemOffer> appliedOffers;
            List<CLayer.Rates.RateValues> bookingRates;

            int noOfDays = (checkOut - checkIn).Days;
            int reduceDateBy = 0;
            int calcDays = 0;
            
            long curOfferId = 0;
            decimal curOldRate = 0;
            List<DateTime> freeDays = new List<DateTime>();
            bool freeDaysExist = false;
            foreach (long id in accIds)
            {

                List<CLayer.Rates> accrates = arates.Where(m => m.AccommodationId == id).OrderBy(x => x.StartDate).ToList();
                cnt = accrates.Count();
                if (cnt > 0)
                    inventory = accrates[0].NoofAcc;
                else
                {
                    inventory = 0;
                    result.Add(new CLayer.Rates()
                    {
                        Amount = 0,
                        GuestRate = 0,
                        AccommodationId = id,
                        NoofAcc = 0,
                        RateChanges = new List<CLayer.Rates.RateValues>(),  //FirstDayCharge = fctotal,
                        SBMarkup = 0,
                        CorpDiscount = 0,
                        AppliedOffers = new List<CLayer.BookingItemOffer>()
                    });
                    continue;
                }
                accOffers = BLayer.Offers.GetForAccommodationCalc(id, checkIn, checkOut);
                appliedOffers = new List<CLayer.BookingItemOffer>();
                curOffer = null;
                bookingRates = new List<CLayer.Rates.RateValues>();
                if (accOffers.Count > 0)
                    curOffer = accOffers[0];
                else
                {
                    curOffer = null;
                    if (hasPrOffers) curOffer = propOffer;
                }
                curOfferId = 0;
                currentDate = checkIn;
                total = 0;
                guest = 0;
                PurchaseRateaAfterTaxs = 0;
                totalPurchaseRateAfterTaxs = 0;
                currate = 0;
                curOldRate = 0;
                curguest_rate = 0;
                SellRateBeforeTax = 0;
                PurchaseRateBeforeTax = 0;
                Prch_Rate_AT = 0;
                Prch_Rate_ATX = 0;
                PurchaseRateAfterTax = 0;
                SellRateAfterTax = 0;
                freeDays.Clear();
                //calculate if free nights offer is available..
                reduceDateBy = 0;

                if (curOffer != null && (!isCorporate)) //offer is not available for corporate
                {
                    //find offer applicable date 
                    if (checkIn > curOffer.StartDate)
                        offStartDate = checkIn;
                    else
                        offStartDate = curOffer.StartDate;
                    if (checkOut < curOffer.EndDate)
                        offEndDate = checkOut;
                    else
                        offEndDate = curOffer.EndDate;


                    if (curOffer.RateType == (int)CLayer.ObjectStatus.OfferRateType.OfferFreeRate)
                    {
                        int tot = curOffer.FreeDays + curOffer.NoOfDays; //total days -Example:  for 4 days booking 2 days free.. So total - minimum- 6days booking should be there 
                        int hwmany = 0;
                        calcDays = (offEndDate - offStartDate).Days;
                        if (calcDays >= tot)
                        {
                            hwmany = calcDays / tot;
                            reduceDateBy = hwmany * curOffer.FreeDays;
                            //  currentDate = currentDate.AddDays(reduceDateBy); //reduce freedays from calculation

                            if (reduceDateBy == 1)
                            {
                                if (offEndDate == checkOut) offEndDate = offEndDate.AddDays(-1);
                                if (offEndDate >= checkIn) freeDays.Add(offEndDate);
                            }
                            else if (reduceDateBy > 1)
                            {
                                if (offEndDate == checkOut) offEndDate = offEndDate.AddDays(-1);
                                while (reduceDateBy > 0)
                                {
                                    if (offEndDate >= checkIn) freeDays.Add(offEndDate);
                                    offEndDate.AddDays(-1 * reduceDateBy);
                                    reduceDateBy--;
                                }
                            }
                            //to do offer applied markit
                            curOfferId = curOffer.OfferId;
                            appliedOffers.Add(new CLayer.BookingItemOffer() { OfferId = curOffer.OfferId, OfferTitle = curOffer.Title, AccommodationId = id });
                        }
                        curOffer = null; //avoid checking other offer types
                    }
                }
                freeDaysExist = (freeDays.Count > 0);

                // find tax here

                decimal tax = BLayer.PropertyTax.GetTotalTax(propertyId);

                while (currentDate < checkOut)
                {

                    //never reset currate or curguest_rate here, if a rate is not found for the date then old rate is used
                    for (idx = 0; idx < cnt; idx++)
                    {
                        if (currentDate >= accrates[idx].StartDate && currentDate <= accrates[idx].EndDate)
                        {

                            currate = accrates[idx].Amount;
                            curguest_rate = accrates[idx].GuestRate;
                            PurchaseRateaAfterTaxs = accrates[idx].PurchaseRateAfterTax;
                            Prch_Rate_AT = accrates[idx].PurchaseRateAfterTax;
                            break;
                        }
                    }
                    if (curOldRate != currate)
                    {
                        bookingRates.Add(new CLayer.Rates.RateValues() { StartDate = currentDate.ToShortDateString(), DayCharge = (double)currate, DayGuestCharge = (double)curguest_rate, DayTotalCharge = 0, DayTotalGuestCharge = 0 });
                        curOldRate = currate;
                    }
                    if (curOffer != null && (!isCorporate))
                    {
                        if (currentDate >= offStartDate && currentDate <= offEndDate)
                        {
                            if (curOfferId != curOffer.OfferId && curOfferId != 0)
                            {
                                curOfferId = curOffer.OfferId;
                                appliedOffers.Add(new CLayer.BookingItemOffer() { OfferId = curOffer.OfferId, OfferTitle = curOffer.Title, AccommodationId = id });
                            }

                            switch ((CLayer.ObjectStatus.OfferRateType)curOffer.RateType)
                            {
                                case CLayer.ObjectStatus.OfferRateType.OfferFlatRate:
                                    total = total + curOffer.Amount;
                                    guest = guest + curguest_rate;
                                    break;
                                case CLayer.ObjectStatus.OfferRateType.OfferPercentageRate:
                                    total = Math.Round(total + (currate - (currate * curOffer.Amount / 100)));
                                    guest = Math.Round(guest + (curguest_rate - (curguest_rate * curOffer.Amount / 100)));
                                    break;
                            }
                        }
                        else
                        {
                            total = total + currate;
                            totalPurchaseRateAfterTaxs = totalPurchaseRateAfterTaxs + PurchaseRateaAfterTaxs;
                            guest = guest + curguest_rate;
                            Prch_Rate_ATX = Prch_Rate_ATX + Prch_Rate_AT;
                        }
                    }
                    else
                    {
                        //perform calculations here
                        if (freeDaysExist)
                        {
                            if (!freeDays.Contains(currentDate))
                            {
                                total = total + currate;
                                totalPurchaseRateAfterTaxs = totalPurchaseRateAfterTaxs + PurchaseRateaAfterTaxs;
                                guest = guest + curguest_rate;
                                Prch_Rate_ATX = Prch_Rate_ATX + Prch_Rate_AT;
                            }
                        }
                        else
                        {
                            total = total + currate;
                            totalPurchaseRateAfterTaxs = totalPurchaseRateAfterTaxs + PurchaseRateaAfterTaxs;
                            guest = guest + curguest_rate;
                            Prch_Rate_ATX = Prch_Rate_ATX + Prch_Rate_AT;
                        }

                    }
                    currentDate = currentDate.AddDays(1);
                }

                decimal CalcTotalTax = Math.Round((total * tax) / 100);
                decimal CalcGuestTax = Math.Round((guest * tax) / 100);

                //reduce tax from supplier rate
                SellRateAfterTax = total;
                total = Math.Round(total / (1 + (tax / 100)));
                guest = Math.Round(guest / (1 + (tax / 100)));
                totalPurchaseRateAfterTaxs = Math.Round(totalPurchaseRateAfterTaxs / (1 + (tax / 100)));
                SellRateBeforeTax = totalPurchaseRateAfterTaxs;
                PurchaseRateBeforeTax = total;
                PurchaseRateAfterTax = Prch_Rate_ATX;

                //calculate corp discount and SB Markup here..


                //SB Markup
                decimal SBMarkup_rate = 0;
                decimal SBMarkup_guest = 0;
               
                if (days < CLayer.Rates.LONG_TERM_DAYS)
                {
                    if (isCorporate)
                    {
                        SBMarkup_rate = Math.Round(total * ((decimal)rc.B2BShortTerm) / 100);
                        SBMarkup_guest = Math.Round(guest * ((decimal)rc.B2BShortTerm) / 100);
                        //first day charge
                        //diff rates calculation
                        for (int ai = 0; ai < bookingRates.Count; ai++)
                        {
                            bookingRates[ai].DayTotalCharge = bookingRates[ai].DayCharge + Math.Round(bookingRates[ai].DayCharge * rc.B2BShortTerm / 100);
                            bookingRates[ai].DayTotalGuestCharge = bookingRates[ai].DayGuestCharge + Math.Round(bookingRates[ai].DayGuestCharge * rc.B2BShortTerm / 100);
                        }

                    }
                    else
                    {

                        if(TAMInventoryAPIID == 4 || TAMInventoryAPIID == 5)
                        {
                            SBMarkup_rate = Math.Round(total - totalPurchaseRateAfterTaxs);
                            SBMarkup_guest = Math.Round(guest * ((decimal)rc.B2CShortTerm) / 100);
                        }
                        else
                        {
                            SBMarkup_rate = Math.Round(total * ((decimal)rc.B2CShortTerm) / 100);
                            SBMarkup_guest = Math.Round(guest * ((decimal)rc.B2CShortTerm) / 100);
                        }
                        ////first day charge
                         for (int ai = 0; ai < bookingRates.Count; ai++)
                        {
                            bookingRates[ai].DayTotalCharge = bookingRates[ai].DayCharge + Math.Round(bookingRates[ai].DayCharge * rc.B2CShortTerm / 100);
                            bookingRates[ai].DayTotalGuestCharge = bookingRates[ai].DayGuestCharge + Math.Round(bookingRates[ai].DayGuestCharge * rc.B2CShortTerm / 100);
                        }
                    }
                }
                else
                {
                    if (isCorporate)
                    {
                        SBMarkup_rate = Math.Round(total * ((decimal)rc.B2BLongTerm) / 100);
                        SBMarkup_guest = Math.Round(guest * ((decimal)rc.B2BLongTerm) / 100);
                        //first day charge
                        //diff rates calc
                        for (int ai = 0; ai < bookingRates.Count; ai++)
                        {
                            bookingRates[ai].DayTotalCharge = bookingRates[ai].DayTotalCharge + Math.Round(bookingRates[ai].DayCharge * rc.B2BLongTerm / 100);
                            bookingRates[ai].DayTotalGuestCharge = bookingRates[ai].DayTotalGuestCharge + Math.Round(bookingRates[ai].DayGuestCharge * rc.B2BLongTerm / 100);
                        }
                    }
                    else
                    {
                        if (TAMInventoryAPIID == 4 || TAMInventoryAPIID == 5)
                        {
                            SBMarkup_rate = Math.Round(total - totalPurchaseRateAfterTaxs);
                            SBMarkup_guest = Math.Round(guest * ((decimal)rc.B2CLongTerm) / 100);
                        }
                        else
                        {
                            SBMarkup_rate = Math.Round(total * ((decimal)rc.B2CLongTerm) / 100);
                            SBMarkup_guest = Math.Round(guest * ((decimal)rc.B2CLongTerm) / 100);
                        }
                        
                        //diff rates calc
                        for (int ai = 0; ai < bookingRates.Count; ai++)
                        {
                            bookingRates[ai].DayTotalCharge = bookingRates[ai].DayTotalCharge + Math.Round(bookingRates[ai].DayCharge * rc.B2CLongTerm / 100);
                            bookingRates[ai].DayTotalGuestCharge = bookingRates[ai].DayTotalGuestCharge + Math.Round(bookingRates[ai].DayGuestCharge * rc.B2CLongTerm / 100);
                        }
                    }
                }
                //Corporate Discount
                decimal add_discount = 0;
                decimal base_discount = 0;

                decimal guest_add_discount = 0;
                decimal guest_base_discount = 0;

                if (isCorporate)
                {
                    if (days < CLayer.Rates.LONG_TERM_DAYS)
                    {
                        if (dicnt.ShortTermRate > 0)
                        {
                            add_discount = Math.Round(total * (decimal)dicnt.ShortTermRate / 100);
                            guest_add_discount = Math.Round(guest * (decimal)dicnt.ShortTermRate / 100);
                            //first day charge
                            //individual rates
                            for (int ai = 0; ai < bookingRates.Count; ai++)
                            {
                                bookingRates[ai].DayTotalCharge = bookingRates[ai].DayTotalCharge - Math.Round(bookingRates[ai].DayCharge * dicnt.ShortTermRate / 100);
                                bookingRates[ai].DayTotalGuestCharge = bookingRates[ai].DayTotalGuestCharge - Math.Round(bookingRates[ai].DayGuestCharge * dicnt.ShortTermRate / 100);
                            }
                        }

                        if (dicnt.BaseShortTerm > 0)
                        {
                            base_discount = Math.Round(total * (decimal)dicnt.BaseShortTerm / 100);
                            guest_base_discount = Math.Round(guest * (decimal)dicnt.BaseShortTerm / 100);
                            //first day charge
                             for (int ai = 0; ai < bookingRates.Count; ai++)
                            {
                                bookingRates[ai].DayTotalCharge = bookingRates[ai].DayTotalCharge - Math.Round(bookingRates[ai].DayCharge * dicnt.BaseShortTerm / 100, 2);
                                bookingRates[ai].DayTotalGuestCharge = bookingRates[ai].DayTotalGuestCharge - Math.Round(bookingRates[ai].DayGuestCharge * dicnt.BaseShortTerm / 100, 2);
                            }
                        }
                    }
                    else
                    {
                        if (dicnt.LongTermRate > 0)
                        {
                            add_discount = Math.Round(total * (decimal)dicnt.LongTermRate / 100);
                            guest_add_discount = Math.Round(guest * (decimal)dicnt.LongTermRate / 100);
                            //first day charge
                           //diff rates calc
                            for (int ai = 0; ai < bookingRates.Count; ai++)
                            {
                                bookingRates[ai].DayTotalCharge = bookingRates[ai].DayTotalCharge - Math.Round(bookingRates[ai].DayCharge * dicnt.LongTermRate / 100);
                                bookingRates[ai].DayTotalGuestCharge = bookingRates[ai].DayTotalGuestCharge - Math.Round(bookingRates[ai].DayGuestCharge * dicnt.LongTermRate / 100);
                            }
                        }
                        if (dicnt.BaseLongTerm > 0)
                        {
                            base_discount = Math.Round(total * (decimal)dicnt.BaseLongTerm / 100);
                            guest_add_discount = Math.Round(guest * (decimal)dicnt.BaseLongTerm / 100);
                            //first day charge
                            //diff rates calc
                            for (int ai = 0; ai < bookingRates.Count; ai++)
                            {
                                bookingRates[ai].DayTotalCharge = bookingRates[ai].DayTotalCharge - Math.Round(bookingRates[ai].DayCharge * dicnt.BaseLongTerm / 100);
                                bookingRates[ai].DayTotalGuestCharge = bookingRates[ai].DayTotalGuestCharge - Math.Round(bookingRates[ai].DayGuestCharge * dicnt.BaseLongTerm / 100);
                            }
                        }
                    }
                }
                decimal suppRate = total;
                decimal suppGuestRate = guest;


                if (TAMInventoryAPIID == 4 || TAMInventoryAPIID == 5)
                {
                    total = Math.Round(total);
                    guest = Math.Round(guest + SBMarkup_guest - guest_add_discount - guest_base_discount);
                }
                else
                {
                    total = Math.Round(total + SBMarkup_rate - add_discount - base_discount);
                    guest = Math.Round(guest + SBMarkup_guest - guest_add_discount - guest_base_discount);
                }
                    

                //find tax on total amount and guest rate

                decimal totaltax = (total * tax) / 100;
                decimal guesttax = (guest * tax) / 100;

                result.Add(new CLayer.Rates()
                {
                    SupplierRate = suppRate + CalcTotalTax,
                    SupplierGuestRate = guest + CalcGuestTax,
                    Amount = total,
                    GuestRate = guest,
                    AccommodationId = id,
                    NoofAcc = inventory,
                    RateChanges = bookingRates,  //FirstDayCharge = fctotal,
                    SBMarkup = SBMarkup_rate, //(SBMarkup_rate + SBMarkup_guest),
                    SBGuestMarkup = SBMarkup_guest,
                    CorpDiscount = (add_discount + base_discount),
                    CorpGuestDiscount = (guest_add_discount + guest_base_discount),
                    AppliedOffers = appliedOffers,
                    TotalRateTax = totaltax,
                    TotalGuestTax = guesttax,
                    PurchaseRateAfterTax = PurchaseRateAfterTax,
                    SellRateBeforeTax = PurchaseRateBeforeTax,
                    PurchaseRateBeforeTax= SellRateBeforeTax,
                    SellRateAfterTax= SellRateAfterTax


                });
            }
            return result;
        }
        public static List<CLayer.Rates> GetAccommodationRates(string AccIds, DateTime checkIn, DateTime checkOut, CLayer.Role.Roles rateType)
        {
            DataLayer.Rate dr = new DataLayer.Rate();
            return dr.GetAccommodationRates(AccIds, checkIn, checkOut, (checkOut - checkIn).Days, rateType, CLayer.Role.Roles.Customer);
        }
        //**Added by rahul on 04/03/2020
        //*This is for getting rates for property Accommodation for API
        public static List<CLayer.Rates> GetAccommodationRatesForAPI(string AccIds, DateTime checkIn, DateTime checkOut, CLayer.Role.Roles rateType)
        {
            DataLayer.Rate dr = new DataLayer.Rate();
            return dr.GetAccommodationRatesForAPI(AccIds, checkIn, checkOut, (checkOut - checkIn).Days, rateType, CLayer.Role.Roles.Customer);
        }

        public static List<CLayer.Rates> GetAll(long accommodationId)
        {
            DataLayer.Rate dr = new DataLayer.Rate();
            return dr.GetAll(accommodationId);
        }
        public static List<CLayer.Rates> GetAllRates(long accommodationId)
        {
            DataLayer.Rate rates = new DataLayer.Rate();
            return rates.GetAllRates(accommodationId);
        }

        public static List<CLayer.Rates> GetAllRatesByAccId(long accommodationId)
        {
            DataLayer.Rate rates = new DataLayer.Rate();
            return rates.GetAllRatesByAccId(accommodationId);
        }

        public static List<CLayer.Rates> GetCalcDailyRates(long accommodationId)
        {
            DataLayer.Rate rates = new DataLayer.Rate();
            return rates.GetCalcDailyRates(accommodationId);
        }
        public static long Save(CLayer.Rates data)
        {
            DataLayer.Rate dr = new DataLayer.Rate();
            if (data.WeeklyRate < 1) data.WeeklyRate = data.DailyRate;
            if (data.MonthlyRate < 1) data.MonthlyRate = data.DailyRate;
            if (data.LongTermRate < 1) data.LongTermRate = data.DailyRate;
            return dr.Save(data);
        }
        public static long APISave(CLayer.Rates data)
        {
            DataLayer.Rate dr = new DataLayer.Rate();
            if (data.WeeklyRate < 1) data.WeeklyRate = data.DailyRate;
            if (data.MonthlyRate < 1) data.MonthlyRate = data.DailyRate;
            if (data.LongTermRate < 1) data.LongTermRate = data.DailyRate;
            return dr.APISave(data);
        }
        public static long DynamicSave(CLayer.Rates data)
        {
            DataLayer.Rate dr = new DataLayer.Rate();
            if (data.WeeklyRate < 1) data.WeeklyRate = data.DailyRate;
            if (data.MonthlyRate < 1) data.MonthlyRate = data.DailyRate;
            if (data.LongTermRate < 1) data.LongTermRate = data.DailyRate;
            return dr.DynamicSave(data);
        }
        public static List<CLayer.Rates> GetAll(DateTime startDate, DateTime endDate, long accommodationId)
        {
            DataLayer.Rate dr = new DataLayer.Rate();
            return dr.GetAll(startDate, endDate, accommodationId);
        }
        public static void Delete(DateTime startDate, DateTime endDate, long accommodationId)
        {
            DataLayer.Rate dr = new DataLayer.Rate();
            dr.Delete(startDate, endDate, accommodationId);
        }
        public static long GDSRateSave(CLayer.GDSRates data)
        {
            DataLayer.Rate dr = new DataLayer.Rate();

            return dr.GDSRateSave(data);
        }
    }
}
