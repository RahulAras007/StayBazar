using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class UnSubscribeModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Is required")]
        [EmailAddress(ErrorMessage = "Requires a valid email address")]
        public string Mailaddress { get; set; }       
    }
}