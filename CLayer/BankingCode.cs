using System;


namespace CLayer
{
    public class BankingCode
    {
        public string PaymentGatewayId { get; set; }
        public string Code { get; set; }
        public ObjectStatus.PaymentMethod PayType { get; set; }
        public string BankingTitle { get; set; }
    }
}
