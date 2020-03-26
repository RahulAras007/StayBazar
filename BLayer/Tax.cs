using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLayer
{
    public class Tax
    {
        public static  List<CLayer.BookingItemTax> GetAllByBookingItem(long bookingItemId)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.GetAllByBookingItem(bookingItemId);
        }
        public static void Delete(int taxId)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            task.Delete(taxId);
        }

        public static void DeletePropertyTax(long Id, long Tid)
        {
            DataLayer.Tax pttax = new DataLayer.Tax();
            pttax.DeletePropertyTax(Id,Tid);
        }
        public static List<CLayer.Tax> GetAllForProperty(long propertyId,DateTime checkIn,DateTime bookingDate)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.GetAllForProperty(propertyId,checkIn,bookingDate);
        }

        public static List<CLayer.Tax> GetAllTypeTaxRate(long BookingId)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.GetAllTypeTaxRate(BookingId);
        }
        /// <summary>
        /// Calculate tax on total amount
        /// Sorting by PriceUpTo is important
        /// </summary>
        /// <param name="propertyTaxes">Sorting by PriceUpTo is important</param>
        /// <param name="totalAmount"></param>
        /// <returns></returns>
        public static decimal GetTaxOnAmount(List<CLayer.Tax> propertyTaxes,decimal totalAmount,long bookingItemId)
        {
            decimal taxAmnt = 0;
            decimal calcTax = 0;
            int i, cnt;
            CLayer.BookingItemTax itemTax = new CLayer.BookingItemTax();
            List<CLayer.Tax> calcTaxes = propertyTaxes.Where(m => m.PriceUpto >= totalAmount || m.Unlimited == true).OrderBy(m => m.PriceUpto).ToList(); // .OrderByDescending(m => m.Unlimited).GroupBy(m =>m.TaxTitleId && m.Country && m.StateId && m.CityId).ToList();
            cnt = calcTaxes.Count;
            itemTax.BookingItemId = bookingItemId;
            for(i=0;i<cnt;i++)
            {
                if (calcTaxes[i].Unlimited || totalAmount <= calcTaxes[i].PriceUpto)
                {
                    calcTax = totalAmount * calcTaxes[i].Rate / 100;
                    itemTax.TaxId = calcTaxes[i].TaxId;
                    itemTax.Amount = (double) calcTax;
                    itemTax.Rate = (double) calcTaxes[i].Rate;
                    itemTax.OnGrandTotal = calcTaxes[i].OnTotalAmount;
                    
                    AddTaxForBookingItem(itemTax);
                    taxAmnt = taxAmnt + calcTax;
                }
            }

            return Math.Round(taxAmnt, 2);
        }
        public static List<CLayer.Tax> GetPropertyTax()
        {
            DataLayer.Tax Tax = new DataLayer.Tax();
            return Tax.GetPropertyTax();
        }
        public static List<CLayer.Tax> GetPropertyTaxById(long PropertyId)
        {
            DataLayer.Tax Tax = new DataLayer.Tax();
            return Tax.GetPropertyTaxById(PropertyId);
        }
        public static void AddTaxForBookingItem(CLayer.BookingItemTax data)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            task.AddTaxForBookingItem(data);
        }
        public static CLayer.Tax Get(int taxId)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.Get(taxId);
        }
        public static List<CLayer.Tax>GetAllByTaxtTitleId(int taxtitleId)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.GetAllByTaxtTitleId(taxtitleId);
        }
        public static List<CLayer.Tax> GetAll()
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.GetAll();
        }
        public static int Save(CLayer.Tax data)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.Save(data);
        }

        public static int SaveTax(CLayer.Tax data)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.SaveTax(data);
        }     
        public static List<CLayer.Tax> GetNonStandardOnly()
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.GetNonStandardOnly();
        }

        public List<CLayer.Tax> GetOnStatus(CLayer.ObjectStatus.StatusType status)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.GetOnStatus(status);
        }

        public static List<CLayer.Tax> GetStandardOnly()
        {
            DataLayer.Tax task = new DataLayer.Tax();
            return task.GetStandardOnly();
        }

        

        public void SetStatus(int taxId, CLayer.ObjectStatus.StatusType status)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            task.SetStatus(taxId, status);
            return;
        }
        public static void AddAmadeusTaxRates(CLayer.Tax data)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            task.AddAmadeusTaxRates(data);
        }
        public static long GetAmadeusTaxRates(string BookingCode,long PropertyID,long BookingID=0)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            long result=task.GetAmadeusTaxRates(BookingCode, PropertyID,BookingID);
            return result;
        }
        public static List<CLayer.Tax> GetAmadeusTaxRates(long PropertyID)
        {
            DataLayer.Tax task = new DataLayer.Tax();
            List<CLayer.Tax> result = task.GetAmadeusTaxRates(PropertyID);
            return result;
        }
    }
}