using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Landmarks : ICommunication
    {
        public long LandmarkId { get; set; }
        public long PropertyId { get; set; }
        public int LandmarkTitleId { get; set; }
        public string Landmark { get; set; }
        public decimal Range { get; set; }
        public void Validate()
        {
            Landmark = Utils.CutString(Landmark, 100);
        }
    }
}