using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace StayBazar.Areas.Admin.Models
{
    public class RateModel
    {
        public List<CLayer.Rates> Rates { get; set; }
        public Models.RateCommissionModel Commission { get; set; }
        public long PropertyId { get; set; }
        public long RateAccommodationId { get; set; }

        public RateModel()
        {
            Rates = new List<CLayer.Rates>();
            Commission = new RateCommissionModel();
        }
    }
}