using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Rooms
    {
        public long RoomId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomType { get; set; }
        public int RoomCount { get; set; }
        public long PropertyId { get; set; }
        public string Description { get; set; }
    }
}
