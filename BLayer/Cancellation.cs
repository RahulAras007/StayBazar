using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Cancellation
    {
        

        public static CLayer.CancellationData ModifyBooking(long bookingId,long offlineBookingId, DateTime newCheckIn, DateTime newCheckOut, long userId)
        {
            CLayer.CancellationData result = new CLayer.CancellationData();
            CLayer.Booking bookDetails = BLayer.Bookings.GetDetails(bookingId);
            DateTime checkIn = bookDetails.CheckIn;
            DateTime checkout = bookDetails.CheckOut;
            result.NewBookingExist = false;
            DateTime p1cn, p1co, p2cn, p2co, crdate;
            crdate = p1co = p2co = p1cn = p2cn = DateTime.Today;
            //days to cancel - canceldays
            int cancelDays = 0;
            DateTime cancCheckIn, cancCheckOut;
            cancCheckIn = checkIn;
            cancCheckOut = checkout;
          

            if (newCheckIn > checkIn)
            {
                cancelDays = (newCheckIn - checkIn).Days;
                cancCheckIn = newCheckIn;
            }
            if (newCheckOut < checkout)
            {
                cancelDays = cancelDays + (checkout - newCheckOut).Days;
                cancCheckOut = newCheckOut;
            }
            
            //find days to book
            if (newCheckIn < checkIn)
            {
                p1cn = newCheckIn;
                p1co = checkIn;
            }
            if (newCheckOut > checkout)
            {
                p2cn = checkout;
                p2co = newCheckOut;
            }

            //clean booking cart
            decimal totalCharge = 0;
            long propertyId = BLayer.Bookings.GetPropertyId(bookingId);
            if (cancelDays > 0)
            {
                //calculate the refunds
                decimal firstDay=0;
                decimal remainingAmnt = 0;
                CLayer.Property cancCharges = BLayer.Property.GetCancellationCharges(propertyId);
                
                firstDay = BLayer.Bookings.GetFirstDayCharge(bookingId);
                List<CLayer.BookingItem> items = BLayer.BookingItem.GetAllDetails(bookingId);
                decimal fdi,tci;
                totalCharge = 0;
                
                foreach(CLayer.BookingItem bi in items)
                {
                    fdi = bi.FirstDayCharge;
                    if (cancCharges.IsChargeAppliesToRefund) { tci = Math.Round(BLayer.Bookings.GetTotalCancellationCharge(cancCharges, (fdi * cancelDays), firstDay, checkIn)); }
                    else { tci = 0; }
                    totalCharge = Math.Round(totalCharge + tci);
                    remainingAmnt = Math.Round(fdi * cancelDays - tci);
                   // remainingAmnt = remainingAmnt - tci;
                    bi.TotalAmount = Math.Round(bi.TotalAmount - remainingAmnt);
                    if (bi.TotalAmount < 0) bi.TotalAmount = 0;
                    BLayer.BookingItem.SetCancAmount(bi.BookingItemId, tci,bi.TotalAmount);
                    
                }
                //totalCharge = BLayer.Bookings.GetTotalCancellationCharge(cancCharges, (firstDay * cancelDays), firstDay, checkIn); //bookDetails.Amount
                
                List<CLayer.Transaction> refundable = BLayer.Transaction.GetAllRefundable(bookingId);
                if (refundable.Count > 0)
                {
                    result.ServiceCharge = BLayer.Transaction.GetTotalCancellationServiceCharge(bookingId, ref refundable);
                }
                remainingAmnt = Math.Round(firstDay * cancelDays);
                if(remainingAmnt > 0)
                {
                    remainingAmnt = remainingAmnt - totalCharge;
                    if(remainingAmnt > 0)
                    {
                        BLayer.Transaction.RefundAmount(bookingId, remainingAmnt, totalCharge);
                    }
                }
                BLayer.Bookings.UpdateTotals(bookingId);
                BLayer.BookingItem.UpdateCheckInAndOut(bookingId, cancCheckIn, cancCheckOut);

                //CANCEL EXTERNAL BOOKING REQUEST 

                //set new external  booking request

                BLayer.Bookings.SetUpdatedDate(bookingId);
            }
            result.TotalCancellationCharge = totalCharge;

            int adults, kids, guests;
            List<CLayer.BookingItem> oneitem = BLayer.BookingItem.GetAllDetails(bookingId);
            adults = oneitem[0].NoOfAdults;
            kids = oneitem[0].NoOfChildren;
            guests = oneitem[0].NoOfGuests;

            BLayer.Bookings.ClearCart(userId);
            long oldBookingId = bookingId;
            bookingId = BLayer.Bookings.GetCartIdAfterCleaning(userId);
            bookingId = BLayer.Bookings.GetOfflinebookingCartIdAfterCleaning(userId);


            List<CLayer.BookingAccDetails> binfo = new List<CLayer.BookingAccDetails>();
            if (p1cn != crdate)
            {
                result.NewBookingExist = true;
                foreach (CLayer.BookingItem bi in oneitem)
                {
                    binfo.Add(new CLayer.BookingAccDetails()
                    {
                        AccommodationId = bi.AccommodationId,
                        AccCount = bi.NoOfAccommodations,
                        Adults = adults,
                        Children = kids,
                        Guest = guests
                    });
                }

                BLayer.Rate.BookAccommodations(binfo, p1cn, p1co, userId, bookingId, offlineBookingId,bookDetails.OrderNo);
            }
            if (bookingId == 0) bookingId = BLayer.Bookings.GetCartId(userId);
            if (p2cn != crdate)
            {
                result.NewBookingExist = true;
                binfo.Clear();
                foreach (CLayer.BookingItem bi in oneitem)
                {
                    binfo.Add(new CLayer.BookingAccDetails()
                    {
                        AccommodationId = bi.AccommodationId,
                        AccCount = bi.NoOfAccommodations,
                        Adults = adults,
                        Children = kids,
                        Guest = guests
                    });
                }

                BLayer.Rate.BookAccommodations(binfo, p2cn, p2co, userId, bookingId, offlineBookingId, bookDetails.OrderNo);
            }

            if (bookingId == 0) bookingId = BLayer.Bookings.GetCartId(userId);

            BLayer.Bookings.CopyGuestDetails(oldBookingId, bookingId);

            BLayer.Bookings.UpdateAmounts(bookingId);
            if (bookingId > 0)
            {
                result.NewBookingRate = BLayer.Bookings.GetTotal(bookingId);
            }
            return result;
        }
        public static CLayer.CancellationData GetModifyInfo(long bookingId,long offlinebookings, DateTime newCheckIn, DateTime newCheckOut,long userId)
        {
            CLayer.CancellationData result = new CLayer.CancellationData();
            CLayer.Booking bookDetails = BLayer.Bookings.GetDetails(bookingId);
            DateTime checkIn = bookDetails.CheckIn;
            DateTime checkout = bookDetails.CheckOut;
            int addDays = 0;

                DateTime p1cn, p1co, p2cn, p2co,crdate;
                crdate = p1co = p2co = p1cn = p2cn = DateTime.Today;
                int cancelDays = 0;
                //days to cancel - canceldays
                if (newCheckIn > checkIn)
                {
                    cancelDays = (newCheckIn - checkIn).Days;
                }
                
                if (newCheckOut < checkout)
                {
                    cancelDays = cancelDays + (checkout - newCheckOut).Days;
                }
     
                //additional days
                if(newCheckIn< checkIn)
                {
                    addDays = (checkIn - newCheckIn).Days;
                }
                if (newCheckOut > checkout)
                {
                    addDays = addDays + (newCheckOut - checkout).Days;
                }
                //find days to book
                if (newCheckIn < checkIn)
                {
                    p1cn = newCheckIn;
                    p1co = checkIn;
                }
                if (newCheckOut > checkout)
                {
                    p2cn = checkout;
                    p2co = newCheckOut;
                }
                result.AdditionalDays = addDays;
                result.CancelledDays = cancelDays;
                //find cancellation charge and refund - and process cancellation
                decimal totalCharge = 0;
                long propertyId = BLayer.Bookings.GetPropertyId(bookingId);
                if (cancelDays > 0)
                {
                    decimal firstDay = 0;
                    CLayer.Property cancCharges = BLayer.Property.GetCancellationCharges(propertyId);
                    firstDay = BLayer.Bookings.GetFirstDayCharge(bookingId);
                    if (cancCharges.IsChargeAppliesToRefund)
                    {
                        totalCharge = BLayer.Bookings.GetTotalCancellationCharge(cancCharges, (firstDay * cancelDays), firstDay, checkIn);
                    }
                    result.Refundable = Math.Round((firstDay * cancelDays) - totalCharge); // refundable amount
                    List<CLayer.Transaction> refundable = BLayer.Transaction.GetAllRefundable(bookingId);
                    if (refundable.Count > 0)
                    {
                        result.ServiceCharge = BLayer.Transaction.GetTotalCancellationServiceCharge(bookingId,ref refundable,result.Refundable);
                    }
                    result.Refundable =  Math.Round(result.Refundable - (decimal) result.ServiceCharge);
                    if (result.Refundable < 0) result.Refundable = 0;
                }
                result.TotalCancellationCharge = totalCharge;
                
                int adults, kids, guests;
                List<CLayer.BookingItem> oneitem = BLayer.BookingItem.GetAllDetails(bookingId);
                adults = oneitem[0].NoOfAdults;
                kids = oneitem[0].NoOfChildren;
                guests = oneitem[0].NoOfGuests;
                //find new booking charge and availablity
                BLayer.Bookings.ClearCart(userId);
                bookingId = BLayer.Bookings.GetCartIdAfterCleaning(userId);

                List<CLayer.BookingAccDetails> binfo = new List<CLayer.BookingAccDetails>();
                if (p1cn != crdate)
                {
                    foreach (CLayer.BookingItem bi in oneitem)
                    {
                        binfo.Add(new CLayer.BookingAccDetails()
                        {
                            AccommodationId = bi.AccommodationId,
                            AccCount = bi.NoOfAccommodations,
                            Adults = adults,
                            Children = kids,
                            Guest = guests
                        });
                    }

                    BLayer.Rate.BookAccommodations(binfo, p1cn, p1co, userId, bookingId, offlinebookings, bookDetails.OrderNo);
                }
                if (bookingId == 0) bookingId = BLayer.Bookings.GetCartId(userId);
                if (p2cn != crdate)
                {
                    binfo.Clear();
                    foreach (CLayer.BookingItem bi in oneitem)
                    {
                        binfo.Add(new CLayer.BookingAccDetails()
                        {
                            AccommodationId = bi.AccommodationId,
                            AccCount = bi.NoOfAccommodations,
                            Adults = adults,
                            Children = kids,
                            Guest = guests
                        });
                    }

                    BLayer.Rate.BookAccommodations(binfo, p2cn, p2co, userId, bookingId, offlinebookings, bookDetails.OrderNo);
                }

                if (bookingId == 0) bookingId = BLayer.Bookings.GetCartId(userId);
                BLayer.Bookings.UpdateAmounts(bookingId);
                if (bookingId > 0)
                {
                    result.NewBookingRate = BLayer.Bookings.GetTotal(bookingId);
                }
                BLayer.Bookings.ClearCart(userId);
                return result;
        }
    }
}
