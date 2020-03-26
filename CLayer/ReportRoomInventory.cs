using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class ReportRoomInventory
    {
        public long BookingId { get; set; }
        public long PropertyId { get; set; }
        public string SupplierName { get; set; }
        public string PropertyName { get; set; }
        public string AddressOfProperty { get; set; }
        public string AccomodationType { get; set; }
        public DateTime AccomodationDate { get; set; }
        public long TotalInventoryAllocated { get; set; }
        public long InventoryBooked { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime BookingDate { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public int Start { get; set; }
        public string PropertyCity{ get; set; }         
        public string PropertyZipCode{ get; set; }
        public string PropertyEmail { get; set; }
        public string State { get; set; }
        //public List<ReportRoomInventory> ReportRoomInventoryList { get; set; }
        //public ReportRoomInventory()
        //{
        //    ReportRoomInventoryList = new List<CLayer.ReportRoomInventory>();
        //}
    }
}
