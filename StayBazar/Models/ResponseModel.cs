using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Models
{
    public class ResponseModel
    {
        public CLayer.ObjectStatus.GateWayResponse PaymentResponse { get; set; }
        public long BookingId { get; set; }
        public long OffinePaymentId { get; set; }
    }
}