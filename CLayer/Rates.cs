using System;
using System.Collections.Generic;

namespace CLayer
{
    public class Rates
    {
        public long RateId { get; set; }
        public long AccommodationId { get; set; }
        public int RateFor { get; set; }
        public decimal DailyRate { get; set; }
        public decimal CalcDailyRate { get; set; }
        public decimal MonthlyRate { get; set; }
        public decimal WeeklyRate { get; set; }
        public decimal LongTermRate { get; set; }
        public decimal GuestRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }

        public decimal CalcLongTermDailyRate { get; set; }
        public decimal CalcShortTermDailyRate { get; set; }

        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        //Display
        public string UpdatedByUser { get; set; }
        public int RateType { get; set; }
        public decimal Amount { get; set; }
        public int NoofAcc { get; set; }
        public decimal SBMarkup { get; set; }
        public decimal SBGuestMarkup { get; set; }
        public decimal CorpDiscount { get; set; }
        public decimal CorpGuestDiscount { get; set; }
        public const int LONG_TERM_DAYS = 30;
        public decimal FirstDayCharge { get; set; }
        public decimal SupplierRate { get; set; }
        public decimal SupplierGuestRate { get; set; }
        public List<RateValues> RateChanges { get; set; }
        public List<CLayer.BookingItemOffer> AppliedOffers { get; set; }
        public List<GDSRateTaxes> TaxesList { get; set; }
        public decimal taxpercent { get; set; }
        public decimal PurchaseRateAfterTax { get; set; }
        public decimal TaxRate { get; set; }
        public decimal MarkUpRate { get; set; }
        public decimal PurchaseRateBeforeTax { get; set; }
        public decimal SellRateAfterTax { get; set; }
        public decimal SellRateBeforeTax { get; set; }
        public decimal MarginAmount { get; set; }

        // total tax
        public decimal TotalRateTax { get; set; }
        public decimal TotalGuestTax { get; set; }

        public decimal SGSTTax { get; set; }
        public decimal CGSTTax { get; set; }
        public decimal IGSTTax { get; set; }
        public bool IsCustStateEqualtoBillingEntity { get; set; }
        public decimal SGSTTaxPercent { get; set; }
        public decimal CGSTTaxPercent { get; set; }
        public decimal IGSTTaxPercent { get; set; }
        public string BookingCode { get; set; }
        public string GuarenteeCode { get; set; }
        public decimal GDSAmount { get; set; }

        public string CommissionStatusType { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal CommissionPayableAmount { get; set; }
        public string CommissionComment { get; set; }

        public class RateValues
        {
            public double DayCharge { get; set; }
            public double DayGuestCharge { get; set; }
            public double DayTotalCharge { get; set; }
            public double DayTotalGuestCharge { get; set; }
            public string StartDate { get; set; }
            public long AccommodationId { get; set; }
        }
    }
    public class GDSRateTaxes
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Percent { get; set; }
        public string ChargeUnit { get; set; }
    }
    public class GDSRates
    {
        public long RateId { get; set; }
        public long AccommodationId { get; set; }
        public int RateFor { get; set; }
        public decimal Rate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Status { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string BookingCode { get; set; }
    }
}
