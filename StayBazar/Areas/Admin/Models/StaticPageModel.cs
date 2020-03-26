using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class StaticPageModel
    {
        public long PageId { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Location")]
        public string Location { get; set; }
        public string Image { get; set; }

       // [Required(ErrorMessage = "File name Required")]
        [Display(Name = "File name")]
        public string FileName { get; set; }
        //public string MetaDescription { get; set; }
        //public string MetaKeys { get; set; }
        [Display(Name = "Page Title")]
        public string PageTitle { get; set; }
        //public string Description { get; set; }
        [Display(Name = "Show in widget")]
        public bool ShowInWidget { get; set; }
        [Required(ErrorMessage = "RootFolder Required")]
        [Display(Name = "Static File Path")]
        public string RootFolder { get; set; }
        public string LastUpdate { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public List<CLayer.StaticPage> StaticPropertyList { get; set; }
        [Required]
        [AllowHtml]
        [UIHint("tinymce_full_compressed")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        public StaticPageModel()
        {
            FileName = "index.html";
        }
    }

    public class StaticPageStatus
    {
        public string ErrorMessage { get; set; }
        public string Page { get; set; }
        public bool IsGenerated { get; set; }
    }
}