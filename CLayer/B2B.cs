using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class B2B : ICommunication
    {
        public long B2BId { get; set; }
        public string Name { get; set; }
        public string UserCode { get; set; }
        public decimal MarkupPercent { get; set; }
        public decimal CommissionPercent { get; set; }
        public string ServiceTaxRegNo { get; set; }
        public string VATRegNo { get; set; }
        public int MaximumStaff { get; set; }
        public int RequestStatus { get; set; }
        public string ContactDesignation { get; set; }

        public long UserId { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public string PANNo { get; set; }
        //
        // Only for supplier
        public string PropertyDescription { get; set; }
        public int AvailableProperties { get; set; }
       
        //Only For Affiliates
        public string CompanyRegNo { get; set; }

        //only for Corporate Credit Booking
        public int IsCreditBooking { get; set; }
        public long CreditDays { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal TotalCreditAmount { get; set; }
        public long TotalCreditDays { get; set; }
        public int IsCorpBookingtoday { get; set; }
        public long CreditPeriod { get; set; }
        public long ApproverID { get; set; }
        public string ApproverEmail { get; set; }
        public string ApproverName { get; set; }
        public string BookingPhone { get; set; }
        public string BookingMobile { get; set; }

        public void Validate()
        {
            Name = Utils.CutString(Name, 150);
            UserCode = Utils.CutString(UserCode, 25);
            ServiceTaxRegNo = Utils.CutString(ServiceTaxRegNo, 30);
            VATRegNo = Utils.CutString(VATRegNo, 30);
            PropertyDescription = Utils.CutString(PropertyDescription, 150);
        }
    }
}