using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class B2BUser
    {
        public long UserId { get; set; }
        public long B2BId { get; set; }
        public int UserType { get; set; }
        //Done By rahul
        public string FirstName { get; set; }
        public string AssistedBooking_Flag { get; set; }


    }
    public class B2BApprovers
    {
        public long b2b_approver_id { get; set; }
        public long user_id { get; set; }
        public long approver_id { get; set; }
        public long approver_order { get; set; }
        public DateTime  created_date { get; set; }
        public long created_by { get; set; }
        public string approver_email { get; set; }
        public string  approver_password { get; set; }
        public string username { get; set; }

        public int status { get; set; }

    }
    public class B2BHotels
    {
        public long B2BHotel_ID { get; set; }
        public long PropertyID { get; set; }
        public string  Title { get; set; }
        public long HotelOrder { get; set; }
        public DateTime  CreatedDate { get; set; }
        public long UserID { get; set; }
        public long CityID { get; set; }
        public string  City { get; set; }
        public long CountryID { get; set; }
        public string Country { get; set; }
        public long StateD { get; set; }
        public string State { get; set; }

    }
}
