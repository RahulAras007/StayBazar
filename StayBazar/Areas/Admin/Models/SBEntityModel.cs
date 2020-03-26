using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Areas.Admin.Models
{
    public class SBEntityModel
    {
        public const int COUNTRY_INDIA = 1;

        public long EntityId { get;set;}
        [Required(ErrorMessage = "required")]
        [StringLength(250)]
        [Display(Name = "Entity Name")]
        public string Name { get; set; }
        [StringLength(2000)]
        public string Address { get; set; }
        [Required(ErrorMessage = "required")]
        [Display(Name = "State")]
        public long GSTState { get; set; }
        [Required(ErrorMessage = "required")]
        [Display(Name = "GST Reg. No.")]
        public string GSTNo { get;set;}
        public string Phone { get; set; }
        public SelectList States { get; set; }

        public SBEntityModel()
        {
            GSTState = 0;
           //List<CLayer.State> stes = BLayer.State.GetOnCountry(COUNTRY_INDIA);
           //if(stes.Count > 0)
           //{
           //    stes.Insert(0, new CLayer.State() { StateId = 0, Name = "Select" });
           //}
           //else
           //{
           //    stes.Add(new CLayer.State() { StateId = 0, Name = "Select" });
           //}
           //States = new SelectList(stes,"StateId","Name",0);
        }

        public void LoadState()
        {
            List<CLayer.State> stes = BLayer.State.GetOnCountry(COUNTRY_INDIA);
            //if (stes.Count > 0)
            //{
            //    stes.Insert(0, new CLayer.State() { StateId = 0, Name = "Select" });
            //}
            //else
            //{
            //    stes.Add(new CLayer.State() { StateId = 0, Name = "Select" });
            //}
            States = new SelectList(stes, "StateId", "Name");
        }
    }
}