using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class GDSTransactionsLog
    {
        public long GDSID { get; set; }
        public int GDSType { get; set; }
        public string GDSTransID { get; set; }
        public string GDSSearchCriteria { get; set; }
        public DateTime  CreatedAt { get; set; }
        public string GDSRequest { get; set; }
        public string GDSResult { get; set; }
        public string  GDSError { get; set; }
        public string GDSErrorMessage { get; set; }
        public int UserID { get; set; }
        public long  BookingID { get; set; }
        public string  GDSTypeValue { get; set; }
        public int GDSBookingType { get; set; }
    }
}
