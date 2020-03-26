using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class User : ICommunication
    {
        public long UserId { get; set; }
        public int SalutationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string salesregion { get; set; }
        public int Sex { get; set; }
        public string UserType { get; set; }
        public int UserTypeId { get; set; }
        public int Status { get; set; }
        public string Email { get; set; }
        public int MaximumDailyEntitlement { get; set; }
        public int GradeID { get; set; }
        public string OPSEmail { get; set; }
        public DateTime LastLoginOn { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeletedDate { get; set; }
        public string Businessname { get; set; }
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
        public string Address { get; set; }
        //for gst
        public string GSTRegistrationNo { get; set; }
        public string StateOfRegistration { get; set; }
        public long GSTstateid { get; set; }
        public long B2bGSTId { get; set; }
        public int CustomerPaymentMode { get; set; }
        public decimal CustomerPaymentModeCreditDays { get; set; }
        public int InventoryAPIType { get; set; }

        public string CorporateName { get; set; }
        public string ContactName { get; set; }
        public string GSTRegNo { get; set; }

        public string SbEntities { get; set; }
        public int CostCentre { get; set; }
        public int AssistedBookingFlag { get; set; }
        public int MyAssistedBookerId { get; set; }
        //status description
        //1=Active,2=Disabled,3=Deleted,4=NotVerified (email),6 = Blocked ,5 - Waiting for Moderation

        public void Validate()
        {
            FirstName = Utils.CutString(FirstName, 100);
            LastName = Utils.CutString(LastName, 100);
            Email = Utils.CutString(Email, 150);
            Businessname = Utils.CutString(Businessname, 150);
        }

    }

    public class GDSUserAddress
    {
        public long GDSUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
       
    }
   
}
