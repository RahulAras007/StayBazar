using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class InvoiceListModel
    {
        public string SearchString { get; set; }
        public int Status { get; set; }
        public int SearchItem { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public int TotalRows { get; set; }
        public string SearchValue { get; set; }
        public int SearchStatus { get; set; }
        public int ItemSearch { get; set; }

        public List<CLayer.OfflineBooking> Bookings { get; set; }
        public SelectList ListTypes { get; set; }

        public InvoiceListModel()
        {

            //ListTypes = new List<SelectListItem>();
            //ListTypes.Add(new SelectListItem(){ Text = "Not Generated", Value="1"});
            //ListTypes.Add(new SelectListItem() { Text = "Not Approved", Value = "2" });
            //ListTypes.Add(new SelectListItem() { Text = "Approved", Value = "3" });
            //Bookings = new List<CLayer.OfflineBooking>();

            List<KeyValuePair<int, string>> objPropertyStatusTypes = new List<KeyValuePair<int, string>>();
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>(1, "Not Generated"));
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>(2, "Not Approved"));
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>(3, "Approved"));
            objPropertyStatusTypes.Add(new KeyValuePair<int, string>(4, "Deleted"));
            ListTypes = new SelectList(objPropertyStatusTypes, "Key", "Value");

        }
    }
}