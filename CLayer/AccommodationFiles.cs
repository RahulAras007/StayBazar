using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class AccommodationFiles : ICommunication
    {
        public long FileId { get; set; }
        public long AccommodationId { get; set; }
        public string FileName { get; set; }

        public void Validate()
        {
            FileName = Utils.CutString(FileName, 255);
        }
    }
}
