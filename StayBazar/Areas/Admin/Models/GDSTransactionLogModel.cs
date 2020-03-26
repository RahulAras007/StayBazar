using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StayBazar.Areas.Admin.Models
{
    public class GDSTransactionLogModel
    {
        
        public List<CLayer.GDSTransactionsLog> GDSTransactionsLog { get; set; }
       
    }
}