using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class CorporateCityEntitlement
    {
        public long GradeId { get; set; }
        [Display(Name = "Grade Code")]
        public string GradeCode { get; set; }
        [Display(Name = "Grade Description")]
        public string Gradedescription { get; set; }
        [Display(Name = "Default Amount")]
        public Decimal DefaultAmount { get; set; }

        public SelectList empgrdaelist { get; set; }
        public CorporateCityEntitlement()
        {

            List<CLayer.EmployeeGrades> empgrades = BLayer.EmployeeGrades.GetAll();
            empgrdaelist = new SelectList(empgrades, "GradeId", "GradeCode");
        }
    }
}