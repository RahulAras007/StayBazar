using System;
using System.Collections.Generic;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class QueryModel
    {
        public long QueryId { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject line required")]
        public string Subject { get; set; }
        [Display(Name = "Message/Query")]
        [Required(ErrorMessage = "Message/Query is required")]
        public string Body { get; set; }
        public int MsgType { get; set; }
        public DateTime CreatedOn { get; set; }
        public int Status { get; set; }

        public enum status
        {
            NotViewed = 0,
            Viewed = 1,
            Archived = 2
        }

        public QueryModel() { }
    }
}