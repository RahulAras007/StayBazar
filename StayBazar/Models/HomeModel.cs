using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Models
{
    public class HomeModel
    {
        public SimpleSearchModel Search { get; set; }
        public HomeModel()
        {
            Search = new SimpleSearchModel();
        }
    }
}