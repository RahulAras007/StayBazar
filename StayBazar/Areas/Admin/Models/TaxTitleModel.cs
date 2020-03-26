using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class TaxTitleModel
    {
        public int TaxTitleId{ get; set; }
        [Required(ErrorMessage = "is required")]
        [StringLength(20)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        public int Status { get; set; }
        public TaxTitleModel() 
        {
            
        }

        public enum statusTaxTitile{ Disable = 0,  Active = 1 }
       
    }
}