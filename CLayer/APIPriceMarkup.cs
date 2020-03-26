using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class APIPriceMarkup
    {
        public long APIPriceMarkupCode { get; set; }
        public string APIDescription { get; set; }
        public int DescriptionId { get; set; }
        public long CustomerId { get; set; }
        public string SellMarkup { get; set; }
        public long TotalRows { get; set; }
        public string CustomerName { get; set; }
    }
    public class APIOfflineCustomer
    {
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
