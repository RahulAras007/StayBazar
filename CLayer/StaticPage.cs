using System;
using System.Collections.Generic;


namespace CLayer
{
    public class StaticPage
    {
        public long PageId { get; set; }
        public string City { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public string FileName { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeys { get; set; }
        public string PageTitle { get; set; }
        public string Description { get; set; }
        public bool ShowInWidget { get; set; }
        public string RootFolder { get; set; }
        public string LastUpdate { get; set; }
        public long PropertyId { get; set; }
    }
}
