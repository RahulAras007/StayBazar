using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class RoomFeature
    {
        public long RoomFeatureId { get; set; }     
        public string Title { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public bool CanDelete { get; set; }
    }
}
