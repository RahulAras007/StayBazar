using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class OfflinePayment
    {
      
        public string Name { get; set; }      
        public string ReferenceNumber { get; set; }       
        public decimal Amount { get; set; }      
        public string Message { get; set; }
        public long UserId { get; set; }
        public string PaymentRefNo { get; set; }
        public long TotalRows { get; set; }
        public long OfflinePaymentId { get; set; }

        public string LoginUsername { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OfflinePaymentStatus { get; set; }
        public string Mobile { get; set; }

        public int Gatewaytype { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public int StateId { get; set; }
        public int CityId { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
        public Guid CustomerGuid { get; set; }


        //karthikms added on 19-10-2019
        public string CustomerName { get; set; }
        public int CustomerCity { get; set; }
        public string ConfirmationNumber { get; set; }
        public string PropertyName { get; set; }
        public string PropertyCityname { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string GuestName { get; set; }
        public long NoOfRooms { get; set; }
        public decimal TotalSalePrice { get; set; }
        public decimal AdvanceReceived { get; set; }
        public decimal PayableSalePrice { get; set; }
        public decimal SumTotalSalePrice { get; set; }
        public string PaymentLinkID { get; set; }

        public List<CLayer.OfflinePayment> OfflineBookingList { get; set; }
    }
}
