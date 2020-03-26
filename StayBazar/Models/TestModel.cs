using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StayBazar.Models
{
    public class TestModel
    {
        public string phone { get; set; }
        public string message { get; set; }
        public string emessage { get; set; }
        public int SMethod { get; set; }

        public TestModel()
        {
            SMethod = 1;
        }
    }
}