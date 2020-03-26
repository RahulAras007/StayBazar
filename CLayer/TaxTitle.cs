using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class TaxTitle
    {
        public int TaxTitleId { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public bool CanDelete { get; set; }
        public void Validate()
        {
            Title = Utils.CutString(Title, 100);
        }

    }
}
