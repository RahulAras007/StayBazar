using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DataProvider
{
    public class DataLink
    {
        protected DataPlug Connection { get; set; }

        public DataLink()
        {
            Connection = DataPlugFactory.GetInstance(); 
        }
    }
}
