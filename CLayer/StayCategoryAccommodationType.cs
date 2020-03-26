using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class StayCategoryAccommodationType
    {
        public string Title { get; set; }
        public int StayCategoryId { get; set; }
        public int AccommodationTypeId { get; set; }
        public bool Selected { get; set; }
        public bool IsUsed { get; set; }
    }
}
