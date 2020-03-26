using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class SubscriptionsModel
    {
        public string Mailaddress { get; set; }
        public bool UnSubscribed { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<CLayer.Mail> SubscriptionsList { get; set; }
        
        public int CurrentPage { get; set; }
        public int MaxCount { get; set; }
        public int TotalRows { get; set; }
        public bool IsSearched { get; set; }

        public SubscriptionsModel()
        {
            SubscriptionsList =new List<CLayer.Mail>();
        }
    }
}