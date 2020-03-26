using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class SBEntity
    {
        public long EntityId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long GSTStateId { get; set; }
        public string GSTNo { get; set; }
        public long UserId { get; set; }
        public string Phone { get; set; }

        public string State { get; set; }
    }
}
