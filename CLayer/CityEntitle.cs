using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class CityEntitle
    {
        public long GradeId { get; set; }
        public string GradeCode { get; set; }
        public string Gradedescription { get; set; }
        public long CityCLassID { get; set; }
        public decimal DefaultAmount { get; set; }
        public decimal CityCLassAmt { get; set; }
        public string CityCLassName { get; set; }


    }
    public class CityEntitleSvaeRslt
    {
        public string Result { get; set; }
    }


}