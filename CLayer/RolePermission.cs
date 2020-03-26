using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class RolePermission
    {
        public long RoleId { get; set; }
        public long ModuleId { get; set; }
        public string ModuleName { get; set; }
        public string Controllers { get; set; }
        public bool HasAccess { get; set; }
        public string Heading { get; set; }
        public long HeadingId { get; set; }

        //public RolePermission()
        //{
        //    Controllers = new List<string>();
        //}
    }
}
