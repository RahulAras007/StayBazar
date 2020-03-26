using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
   public class ReportForSuppliersRegistration
    {
        public string SupplierName { get; set; }
        public DateTime RegistrationStartDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Noofproperties { get; set; }
        public long TotalAccomodationInventory { get; set; }
        public long AllocationforStayBazar { get; set; }
        public int CurrentStatus { get; set; }
        public int Status { get; set; }
        public DateTime CurrentDate { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start { get; set; }
        public string PropertyName { get; set; }
        public string PropertyLocation { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public string PropertyCountry { get; set; }
       public long B2BId{ get; set; }
       public long PropertyId { get; set; }
    }
}
