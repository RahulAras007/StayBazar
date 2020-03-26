using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class RateValueModel
    {
        public long AccommodationId { get; set; }

        public long RRateId { get; set; }
        //public long SRateId { get; set; }
        //public long TRateId { get; set; }
        public long CRateId { get; set; }

        public decimal RCDaily { get; set; }
        public decimal RCWeekly { get; set; }
        public decimal RCMonthly { get; set; }
        public decimal RCLong { get; set; }
        public decimal RCGuest { get; set; }

        public decimal CCDaily { get; set; }
        public decimal CCWeekly { get; set; }
        public decimal CCMonthly { get; set; }
        public decimal CCLong { get; set; }
        public decimal CCGuest { get; set; }

        //public decimal SCDaily { get; set; }
        //public decimal SCWeekly { get; set; }
        //public decimal SCMonthly { get; set; }
        //public decimal SCGuest { get; set; }

        //public decimal TCDaily { get; set; }
        //public decimal TCWeekly { get; set; }
        //public decimal TCMonthly { get; set; }
        //public decimal TCGuest { get; set; }

        public int StartMonth { get; set; }
        public int StartDay { get; set; }
        public int EndMonth { get; set; }
        public int EndDay { get; set; }

        public SelectList Months { get; set; }
        public SelectList DaysInMonth { get; set; }

        public RateValueModel()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            DateTime temp;
            for(int i =1;i<13;i++)
            {
                temp = new DateTime(2014, i, 1);
                items.Add(new SelectListItem() { Text = temp.ToString("MMM"), Value = i.ToString() });
            }
            Months = new SelectList(items,"Value","Text");

            items = new List<SelectListItem>();
            for (int i = 1; i < 32; i++)
            {
                items.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            DaysInMonth = new SelectList(items, "Value", "Text");
        }
    }

    public class RateCommissionModel{

        public long ComPropertyId {get;set;}
        [Required(ErrorMessage="Required")]
        [Range(0.5,100.00,ErrorMessage="Invalid")]
        public double B2CShortTerm { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(0, 100.00, ErrorMessage = "Invalid")]
        public double B2CLongTerm { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(0, 100.00, ErrorMessage = "Invalid")]
        public double B2BShortTerm { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(0, 100.00, ErrorMessage = "Invalid")]
        public double B2BLongTerm {get;set;}
        [Required(ErrorMessage = "Required")]
        [Range(0, 1000000.00, ErrorMessage = "Invalid")]
        public double CancellationCharges { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(0, 100000.00, ErrorMessage = "Invalid")]
        public int CancellationPeriod { get; set; }
        public int CancellationType { get; set; }
        public bool AppliesToRefund { get; set; }
        //[Required(ErrorMessage = "Required")]
        //[Range(0, 100.00, ErrorMessage = "Invalid")]
        //public decimal CommissionBA {get;set;}

        public void TakeCopy(CLayer.RateCommission data)
        {
            B2BShortTerm = data.B2BShortTerm;
            B2BLongTerm = data.B2BLongTerm;
            B2CLongTerm = data.B2CLongTerm;
            B2CShortTerm = data.B2CShortTerm;
            ComPropertyId = data.PropertyId;
            CancellationType = (int)CLayer.ObjectStatus.CancellationType.FixedNight;
        }
    }

    public class B2BDiscountModel
    {
        public long B2BId { get; set; }
        public long B2BPropertyId { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(0, 100.00, ErrorMessage = "Invalid rate")]
        public double B2BDShortTerm { get; set; }
        [Required(ErrorMessage = "Required")]
        [Range(0, 100.00, ErrorMessage = "Invalid rate")]
        public double B2BDLongTerm { get; set; }
        public string B2BName { get; set; }
    }
}