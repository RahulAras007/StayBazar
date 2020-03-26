using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class State : ICommunication
    {
        public int StateId { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public string GSTStateCode { get; set; }


        public void Validate()
        {
            Name = Utils.CutString(Name, 150);
        }
    }
}
