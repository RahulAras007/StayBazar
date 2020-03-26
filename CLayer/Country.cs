using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class Country : ICommunication
    {
        public long CountryId { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public int Status { get; set; }
        public bool ForProperty {get; set;}
        public string Code { get; set; }
        public string KeyWords { get; set; }
        public void Validate()
        {
            Name = Utils.CutString(Name, 100);
        }
    }
    public class GDSCountry
    {
        public int CountryID { get; set; }
        public string Country { get; set; }
        public string UPSCode { get; set; }
        public string IATACode { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
}
