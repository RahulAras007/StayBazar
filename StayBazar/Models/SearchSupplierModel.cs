using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Models
{
    public class SearchSupplierModel
    {
        public long B2BId { get; set; }    
        public string Name { get; set; }
        public int UserType { get; set; }
        public string Email { get; set; }
        public string SearchString { get; set; }
        public List<CLayer.B2B> SearchList { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRows { get; set; }      
        public SearchSupplierModel()
        {
            SearchList = new List<CLayer.B2B>();


        }
    }
}