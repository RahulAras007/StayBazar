using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Collections.Generic;

namespace StayBazar.Models
{
    public class MailDataModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "is required")]
        [EmailAddress(ErrorMessage = "requires a valid email address")]
        public string Email { get; set; }
        public DateTime date { get; set; }
    }
}