using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class UserFiles : ICommunication
    {
        public long FileId { get; set; }
        public string FileName { get; set; }
        public long UserId { get; set; }
        public int Document { get; set; }

        public enum Documents
        {
            ServiceTaxRegNo = 1,
            VATRegNo = 2,
            BusinessRegistrationCertificate = 3,
            PANCard = 4,
            CopyOfCheque = 5,
            CorporateLogo=6
        }

        public void Validate()
        {
            FileName = Utils.CutString(FileName, 255);
        }
    }
}
