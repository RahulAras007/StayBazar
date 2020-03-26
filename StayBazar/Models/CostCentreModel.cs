using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayBazar.Models
{
    public class CostCentreModel
    {
        [Required]
        [Display(Name = "Cost Centre Code")]
        public long CostCentreCode { get; set; }
        [Display(Name = "Cost Centre Name")]
        public string CostCentreName { get; set; }
        public long B2B_ID { get; set; }
    }
}