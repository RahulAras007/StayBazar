using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace StayBazar.Models
{
    public class ModifyModel
    {
        [Required(ErrorMessage="required")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date, ErrorMessage="Invalid date")]
        public DateTime CheckIn { get; set; }
        [Required(ErrorMessage = "required")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date, ErrorMessage = "Invalid date")]
        public DateTime CheckOut { get; set; }
        public long BookingId { get; set; }
        public string Msg { get; set; }
        public bool Failed { get; set; }
        public double CancellationCharge { get; set; }
        public double CurrentTotalAmount { get; set; }
        public double NewBookingAmount { get; set; }
        public double ServiceCharge { get; set; }
        public bool Postback { get; set; }
        public DateTime OldCIn { get; set; }
        public DateTime OldCOt { get; set; }
        //internal use
        public bool NewBookingExists { get; set; }
        public decimal Refundable { get; set; }
        public int AdditionalDays { get; set; }
        public int CancelledDays { get; set; }
        public ModifyModel()
        {
            Failed = false;
            Postback = false;
            Msg = "";
            NewBookingExists = false;
        }
    }
}