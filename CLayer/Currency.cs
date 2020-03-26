using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLayer
{
    public class Currency : ICommunication
    {
        public long CurrencyId { get; set; }
        public string Title { get; set; }
        public string Symbol { get; set; }
        public decimal ConversionRate { get; set; }
        public decimal ConversionPercentage { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool IsDefault { get; set; }
        public int Status { get; set; }
        public string Exchangecode { get; set; }
        public void Validate()
        {
            Title = Utils.CutString(Title, 50);
            Symbol = Utils.CutString(Symbol, 3);
        }
    }
    public class GDSCurrencyConversions
    {
        public decimal RateConversion { get; set; }
        public string SourceCurrencyCode { get; set; }
        public string RequestedCurrencyCode { get; set; }
        public int DecimalPlaces { get; set; }
    }
    public class GDSCommission
    {
        public string CommissionStatusType { get; set; }
        public decimal CommissionPercent { get; set; }
        public decimal CommissionPayableAmount { get; set; }
        public string CommissionComment { get; set; }
    }
}
