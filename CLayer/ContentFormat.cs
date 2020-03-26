using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CLayer
{
    public class ContentFormat
    {
        public int BlockType { get; set; }
        public string InfoCode { get; set; }
        public string AdditionalDetailCode { get; set; }
        public int StarRatings { get; set; }
        public List<ContentLines> lines { get; set; }
    }
    public class ContentLines
    {
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
