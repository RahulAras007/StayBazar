using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Models
{
    public class ForgotPasswordModel
    {
        [EmailAddress(ErrorMessage="Invalid email")]
        [Required(ErrorMessage = "email required")]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public bool Status { get; set; }
        public string ErrorMsg { get; set; }
        public ForgotPasswordModel()
        {
            Status = false;
        }
    }
}