using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class PaymentGateway
    {
        public class EBS
        {
            public const int NAME_LENGTH = 128;
            public const int ADDRESS_LENGTH = 255;
            public const int CITY_LENGTH = 32;
            public const int STATE_LENGTH = 32;
            public const int ZIP_LENGTH = 10;
            public const int PHONE_LENGTH = 20;
            public const int EMAIL_LENGTH = 100;
            public const int SHIP_NAME= 255;
            public const int SHIP_ADDR_LENGTH = 255;
        }
    }
}
