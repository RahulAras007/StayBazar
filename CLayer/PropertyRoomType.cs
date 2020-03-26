using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class PropertyRoomType
    {
        public string Title { get; set; }
        public int PropertyTypeId { get; set; }
        public int RoomTypeId { get; set; }
        public bool Selected { get; set; }
        public bool IsUsed { get; set; }
    }
}
