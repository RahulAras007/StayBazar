using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{

    public class SupplierPayment : ICommunication
    {
        public long UserId { get; set; }
        public int SalutationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SupplierName { get; set; }
             public string SupplierCity { get; set; }
        public string SupplierEmail { get; set; }
        public string SupplierMobile { get; set; }
        public string Guestname { get; set; }

        public int Sex { get; set; }
        public string UserType { get; set; }
        public int UserTypeId { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public long NoOfPaxAdult { get; set; }
        public long NoofRooms { get; set; }
        public long NoofDays { get; set; }
        public long NoOfPaxChild { get; set; }
        public DateTime CheckIn_date { get; set; }
        public DateTime CheckOut_date { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime BookingDate { get; set; }
        public string propertyname { get; set; }
        public string propertycity { get; set; }
        public string CustomerName { get; set; }
        public double Buyprice { get; set; }
        public double AmountPaid { get; set; }
        public string PropertyName { get; set; }
        public long PropertyId { get; set; }
        public long AccomodationId { get; set; }
        public string FullName { get; set; }//Firstname + Lastname
        //for display purposes
        public string Salutation { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string ZipCode { get; set; }
        public long TotalRows { get; set; }
        public long BookingUserId { get; set; }
        public string PANNo { get; set; }
        public string OrderId { get; set; }
        public string UserCode { get; set; }
        public long supplierPaymentId { get; set; }
        public long supplierId { get; set; }
        public decimal Amount { get; set; }
        public decimal grossAmount { get; set; }

        public decimal tdsAmount { get; set; }
       
        public string modeofPayment { get; set; }
        public decimal netAmtPaid { get; set; }
        public string BAnk { get; set; }
        public decimal Payamount { get; set; }
        public DateTime pdtPayment { get; set; }
        public string Region { get; set; }
        public bool supplierPayment { get; set; }
        //status description
        //1=Active,2=Disabled,3=Deleted,4=NotVerified (email),6 = Blocked ,5 - Waiting for Moderation

        public void Validate()
        {
            FirstName = Utils.CutString(FirstName, 100);
            LastName = Utils.CutString(LastName, 100);
            Email = Utils.CutString(Email, 150);
            propertyname = Utils.CutString(propertyname, 150);
        }
    }
}
