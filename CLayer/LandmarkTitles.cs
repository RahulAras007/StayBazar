using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class LandmarkTitles : ICommunication
    {
        public int LandmarkTitleId { get; set; }
        public string LandmarkTitle { get; set; }

        public void Validate()
        {
            LandmarkTitle = Utils.CutString(LandmarkTitle, 100);
        }
    }
}
