using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Areas.Admin.Models
{
    public class HSNCodeModel
    {
        // GET: Admin/SBEntity
        public long CodeId { get; set; }
        [Required(ErrorMessage = "required")]
        [StringLength(500)]
        [Display(Name = "Nature of service")]
        public string NatureOfService { get; set; }
        [Required(ErrorMessage = "required")]
        [StringLength(50)]
        [Display(Name = "HSN Code")]
        public string HSNCode { get; set; }
    }
}