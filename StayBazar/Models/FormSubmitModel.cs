using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class FormSubmitModel
    {
        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        public string Website { get; set; }

        [Required(ErrorMessage = "Enter Contact No:")]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Enter Email Address")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Event Name")]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Enter Event Location")]
        [Display(Name = "Event Location")]
        public string EventLocation { get; set; }
        [Display(Name = "Check In Date")]
        [Required(ErrorMessage = "Enter Check In Date")]
        public string CheckIn { get; set; }

        [Display(Name = "Check Out Date")]
        [Required(ErrorMessage = "Enter Check Out Date")]
        public string CheckOut { get; set; }

        [Required(ErrorMessage = "Enter Adults")]
        [Display(Name = "Adults")]
        public long NoOfAdult { get; set; }



        [Required(ErrorMessage = "Enter Rooms")]
        [Display(Name = "No Of Rooms Required")]
        public long NoOfRooms { get; set; }
        [Required(ErrorMessage = "Enter Children")]
        [Display(Name = "Children")]
        public long NoOfChild { get; set; }
    }
}