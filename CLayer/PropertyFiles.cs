using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class PropertyFiles : ICommunication
    {
        public long FileId { get; set; }
        public long PropertyId { get; set; }
        public string FileName { get; set; }
        public bool  IsValid { get; set; }
        //public int FileType { get; set; }

        //public enum FileTypes
        //{
        //    images = 1,
        //    videos = 2,
        //    docs = 3
        //};

        public void Validate()
        {
            FileName = Utils.CutString(FileName, 250);
        }
    }
}
