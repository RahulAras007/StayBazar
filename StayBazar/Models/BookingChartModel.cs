using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Models
{
    public class BookingChartModel
    {
       // public List<CLayer.BookingDetails> Items { get; set; }
        public List<CLayer.Booking> Bookinglist { get; set; }    
        public string ActiveTab { get; set; }
       
        public BookingChartModel()
        {
           // Items = new List<CLayer.BookingDetails>();
            Bookinglist = new List<CLayer.Booking>();
        }


       
    }
}