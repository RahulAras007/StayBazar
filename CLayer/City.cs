using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class City : ICommunication
    {
        public int CityId { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }
        public bool ForListing { get; set; }
        public int RoomCount { get; set; }
        public string Keywords { get; set; }
        public void Validate()
        {
            Name = Utils.CutString(Name, 150);
        }
    }
}
