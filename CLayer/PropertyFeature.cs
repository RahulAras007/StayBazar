using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class PropertyFeature : ICommunication
    {
        public long PropertyFeatureId { get; set; }
        public string Title { get; set; }
        public string Style { get; set; }
        public bool CanDelete { get; set; }
        public bool Showfeatures { get; set; }
        //for property linking
        public long PropertyId { get; set; }
        public bool IsUsed { get; set; }



        public void Validate()
        {
            Title = Utils.CutString(Title, 50);
        }
    }
}
