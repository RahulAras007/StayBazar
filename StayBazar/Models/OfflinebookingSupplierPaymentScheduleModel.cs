using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Models
{
    public class OfflinebookingSupplierPaymentScheduleModel
    {
        public int PaymentscheduleId {get;set;}

        public int BookingId {get;set;}

        public int SupplierPaymentMode {get;set;}

        public DateTime SupplierPaymentDate{get;set;}

        public decimal SupplierPaymentAmount { get; set; }
    }
}