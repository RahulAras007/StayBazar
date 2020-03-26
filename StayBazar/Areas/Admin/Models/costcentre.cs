using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Areas.Admin.Models
{
    
    public class costcentre
    {
        public int costcentreID { get; set; }
        [Required(ErrorMessage ="Please Enter Percentage")]
        public int percentage { get; set; }
        [Required(ErrorMessage ="Please Select CostCentreCode")]
        public int CostCentrecode{get;set;}

    }
    
    
}