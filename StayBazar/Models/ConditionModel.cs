using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Models
{
    public class ConditionModel
    {
        public long ConditionId { get; set; }
        public long? AccommodationId { get; set; }
        [Required(ErrorMessage = "Condition required")]
        [Display(Name = "Condition")]
        public string ConditionText { get; set; }
        public long PropertyId { get; set; }
        //list
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        //public string SearchString { get; set; }
        //public int SearchValue { get; set; }
        public List<CLayer.Condition> ConditionList { get; set; }
        public ConditionModel()
        {
            ConditionList = new List<CLayer.Condition>();

        }
    }
}