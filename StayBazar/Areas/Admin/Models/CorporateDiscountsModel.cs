using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Areas.Admin.Models
{
    public class CorporateDiscountsModel
    {

        public long SupplierId { get; set; }
       
        public decimal B2bStdShortDiscounts { get; set; }
        public decimal B2bStdLongDiscounts { get; set; }

        public decimal B2BMarkupShortTerm { get; set; }
        public decimal B2BMarkupLongTerm { get; set; }

        public decimal AddShortTerm { get; set; }
        public decimal AddLongTerm { get; set; }
        public string Usercode { get; set; }
        public long CorporateId { get; set; }
        public string Corporatename { get; set; }

        public string Suppliername { get; set; }
        public long PropertyId { get; set; }
        public string PropertyName { get; set; }

        public string PropertyCity { get; set; }

        public string PropertyLoaction { get; set; }
        public List<CLayer.CorporateDiscounts> ReportList { get; set; }

    }
}