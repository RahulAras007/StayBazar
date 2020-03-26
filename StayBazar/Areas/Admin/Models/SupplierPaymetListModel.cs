using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class SupplierPaymetListModel
    {

        public List<CLayer.SupplierPaymetList> SupPayList { get; set; }
        public enum SearchTypeValues { Property = 1, Supplier = 2, Email = 3 }

        public int searchType { get; set; }
        public int Limit { get; set; }
        public int currentPage { get; set; }
        public string searchText { get; set; }
        public long TotalRows { get; set; }
    }
}