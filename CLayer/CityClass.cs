using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class CityClass
    {
        public long CityClassID { get; set; }
        public string CityClassCode { get; set; }
        public string CityClassDescription { get; set; }
        public long CountryID { get; set; }
        public long CityID { get; set; }
        public long StateID { get; set; }
        public long B2B_ID { get; set; }
        public string CountryIDs { get; set; }
        public string StateIDs { get; set; }
        public string CityIDs { get; set; }

        public string CountryNames { get; set; }
        public string StateNames { get; set; }
        public string CityNames { get; set; }
    }
    public class CityClassResult
    {
        public string Result { get; set; }
    }
}
