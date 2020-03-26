using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StayBazar.Models
{
    public class CorporateEmployeeGradeModel
    {
    public long GradeID { get; set; }
    [Required]
    [Display(Name = "Grade Code")]
    public string GradeCode { get; set; }
    [Display(Name = "Grade Description")]
    public string GradeDescription { get; set; }
    public long B2B_ID { get; set; }

    public CorporateEmployeeGradeModel()
        {
           
        }
    }
}
