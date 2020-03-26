using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace StayBazar.Models
{
    public class GuestModel
    {
            [Required(ErrorMessage="Email required")]
            [Display(Name = "Email")]
            [EmailAddress(ErrorMessage="Invalid Email")]
            public string Email { get; set; }
            
            [Required]
            [Display(Name = "Retype Email")]
            [Compare("Email",ErrorMessage="Emails are not same")]
            [EmailAddress(ErrorMessage = "Invalid Email")]
            public string RetypeEmail { get; set; }

            [Required]
       //     [DataType(DataType.PhoneNumber)]
            [Display(Name = "Mobile No")]
            [MinLength(10, ErrorMessage = "Enter at least ten digits")]
            [RegularExpression(@"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Invalid Phone Number!")]
            public string Phone { get; set; }

            public long BookingId { get; set; }
    }
}