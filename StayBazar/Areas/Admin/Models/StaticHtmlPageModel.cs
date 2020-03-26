using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class StaticHtmlPageModel
    {
        public long PageId { get; set; }      
        public string City { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string FileName { get; set; }
        public string PageTitle { get; set; }
        public string Description { get; set; }
        public bool ShowInWidget { get; set; }
        public string RootFolder { get; set; }
        public string LastUpdate { get; set; }
        public List<CLayer.SearchResult> PropertyList { get; set; }
        public List<CLayer.SearchResult> PropertyAdd { get; set; }
        public HttpPostedFile ImageFile { get; set; }
        public string Destination { get; set; }
        public int CurrentPage { get; set; }
        public int MaxCount { get; set; }
        public bool IsSearched { get; set; }
        public int TotalRows { get; set; }
        public string SearchString { get; set; }
    }
}