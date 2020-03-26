using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
   public  class Inventory
    {
        public long? AccommodationId { get; set; }
        public long InventoryId { get; set; }
        public int Quantity { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
       //for list
        public long TotalRows { get; set; }
        public int currentPage { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public int SearchValue { get; set; }
       //drop
        public long PropertyId  { get; set; }
        public string Category { get; set; }
        public List<Inventory>InventoryList{ get; set; }
        public Inventory()
        {
            InventoryList = new List<CLayer.Inventory>();
        }
    }
}
