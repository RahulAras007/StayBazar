using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class AccommodationFeature : ICommunication
    {
        public long AccommodationFeatureId { get; set; }
        public string Title { get; set; }
        public string Style { get; set; }
        public bool CanDelete { get; set; }
        public bool Showfeatures { get; set; }
        //for accommodation linking
        public long AccommodationId { get; set; }
        public bool IsUsed { get; set; }

        public void Validate()
        {
            Title = Utils.CutString(Title, 30);
        }
    }
}
