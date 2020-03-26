using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Discount
    {
        public long PropertyId { get; set; }
        public long B2BId { get; set; }
        public double ShortTermRate { get; set; }
        public double LongTermRate { get; set; }

        //for display
        public string B2BName { get; set; }
        public double BaseShortTerm { get; set; }
        public double BaseLongTerm { get; set; }
    }
}
