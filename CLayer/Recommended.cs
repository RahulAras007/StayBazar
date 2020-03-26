using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CLayer
{
    public class Recommended
    {
        public long PropertyId { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Order { get; set; }
        public long UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        //For display
        public string Title { get; set; }
        public string Supplier { get; set; }
        public string UpdatedByUser { get; set; }

        public int ManageFor { get; set; }
        public Decimal Amount { get; set; }
    }
}
