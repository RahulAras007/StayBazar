using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLayer
{
    public class OfflineBooking
    {
        
        public int InventoryAPIType { get; set; }
        public long OfflineBookingId { get; set; }
        public long BookingId { get; set; }
        public ObjectStatus.OfflineBookingType BookingType { get; set; }
        //Done by rahul
        public string BookingFor { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerCountry { get; set; }
        public string CustomerCountryname { get; set; }
        public int CustomerState { get; set; }
        public string CustomerStatename { get; set; }
        public int CustomerCity { get; set; }
        public string CustomerCityname { get; set; }
        public string GuestName { get; set; }
        public string GuestEmail { get; set; }

        public string GuestPhone { get; set; }
        public int CustomerPaymentModeId { get; set; }
        public decimal CustomerPaymentCreditPeriod { get; set; }
        //vendors details
        public long CostCentreID { get; set; }
        public long vendorId { get; set; }
        public long VendorpaymentsId { get; set; }
        public string vendorname { get; set; }

        public string vendoraddress { get; set; }

        public string address1 { get; set; }

        public string address2 { get; set; }

        public string City { get; set; }

        public string pin { get; set; }

        public string contactperson { get; set; }

        public string contactno { get; set; }

        public string emailaddress { get; set; }

        public string natureofservice { get; set; }

        public decimal valuebeforeservicetax { get; set; }

        public decimal servicetaxamount { get; set; }

        public decimal totalamountpayable { get; set; }
        //Property Details
        public long PropertyId { get; set; }
        public long CustomPropertyId { get; set; }
        public string PropertyName { get; set; }
        public string PropertyAddress { get; set; }
        public string PropertyContactNo { get; set; }
        public string PropertyEmail { get; set; }
        public string PropertyPinCode { get; set; }
        public string PropertyCaretakerName { get; set; }
        public int PropertyCountry { get; set; }
        public string PropertyCountryname { get; set; }
        public int PropertyState { get; set; }
        public string PropertyStatename { get; set; }
        public int PropertyCity { get; set; }
        public string PropertyCityname { get; set; }

        //Supplier Details
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPinCode { get; set; }
        public string SupplierMobile { get; set; }
        public string SupplierEmail { get; set; }
        public int SupplierCountry { get; set; }

        public string SupplierCountryname { get; set; }
        public int SupplierState { get; set; }
        public string SupplierStatename { get; set; }
        public int SupplierCity { get; set; }
        public string SupplierCityname { get; set; }

        //Booking Details
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public long NoOfNight { get; set; }
        public long NoOfPaxAdult { get; set; }
        public long NoOfPaxChild { get; set; }
        public long NoOfRooms { get; set; }
        public long StayCategory { get; set; }
        public string StayCategoryName { get; set; }
        public long AccommodatoinType { get; set; }
        public string AccommodatoinTypename { get; set; }
        public long Accommodationtypeid { get; set; }
        public long Accommodationid { get; set; }
        public string Accommodationname { get; set; }
        public long StayCategoryid { get; set; }


        //Pricing Details
        public string SalesRegion { get; set; }
        //buy price
        public decimal AvgDailyRateBefreStaxForBuyPrice { get; set; }
        public decimal StaxForBuyPrice { get; set; }
        public decimal TotalAmtForBuyPrice { get; set; }
        public decimal BuyPriceforotherservicesForBuyprice { get; set; }
        public decimal StaxForotherBuyPrice { get; set; }
        public decimal TotalAmtotherForBuyPrice { get; set; }
        public decimal TotalBuyPrice { get; set; }

        //sale price
        public decimal AvgDailyRateBefreStaxForSalePrice { get; set; }
        public decimal StaxForSalePrice { get; set; }
        public decimal TotalAmtForSalePrice { get; set; }
        public decimal BuyPriceforotherservicesForSalePrice { get; set; }
        public decimal StaxForotherForSalePrice { get; set; }
        public decimal TotalAmtotherForSalePrice { get; set; }
        public decimal TotalSalePrice { get; set; }

        public string Natureofservice { get; set; }

        public long Valuebeforeservicetax { get; set; }
        public long Servicetaxamount { get; set; }
        public long Totalamountpayable { get; set; }





        //Other service details
        public string OtherService { get; set; }

        //Nature of Other service details
        public string OtherServiceNature { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime booking_creationdate { get; set; }


        public bool sendmailtosupplier { get; set; }
        public bool sendmailtocustomer { get; set; }
        public string ConfirmationNumber { get; set; }
        public bool isOpen { get; set; }

        public decimal SupInvoiceValueBRef { get; set; }
        public decimal PaidValueBRef { get; set; }
        public string BookingDate { get; set; }

        public long TotalRows { get; set; }
        public int SaveStatus { get; set; }
        public enum Statuslist { Save = 1, Submit = 2, Cancel = 3, Delete = 4, Approved = 5,Draft=6,WaitingForApproval=11 }

        public enum CustomerPaymentModelist {AdvancePayment=1,PaymentOnCheckOut=2,Credit=3 }

        public enum SupplierPaymentModelist { Credit = 1, FullAmountBeforeCheckin = 2, FullAmountBeforeCheckout = 3,OnInstalments = 4 }

        public enum DraftbookingStatus { Draft = 1, Approved = 2, Reject = 3}
        public long SalesPersonId { get; set; }
        public string SalesPerson { get; set; }
        public bool isCheckBook { get; set; }


        public long SupplierInvoiceID { get; set; }
        public DateTime BookinCreatedDT { get; set; }

        public DateTime CheckIn_date { get; set; }

        public DateTime CheckOut_date { get; set; }

        public string AccountNumber { get; set; }

        public string BankName { get; set; }

        public string BranchAddress { get; set; }

        public string AccountName { get; set; }

        public string AccountType { get; set; }

        public string IFSCcode { get; set; }

        public string CareTakerPhone { get; set; }

        public string CareTakerEmail { get; set; }
        public string SubPropertyGstRegNo { get; set; }
        public string BranchName { get; set; }
        public string PAN { get; set; }

        public string MICRcode { get; set; }
        public string NewCustomerReferenceNo { get; set; }
        public long CustomerGstStateId { get; set; }
        public string CustomerReferenceNo { get; set; }
        public string PropertyGstRegNo { get; set; }
        public string CustomerGstRegNo { get; set; }
        public long UserType { get; set; }
        public string BillingAddress { get; set; }
        public int BillingCountry { get; set; }
        public int BillingState { get; set; }
        public int BillingCity { get; set; }
        public string ContactPerson { get; set; }
        public string BillingMobile { get; set; }
        public string PinCode { get; set; }
        public string OfficeNO { get; set; }
        public int BillingCountryId { get; set; }
        public string CustomerName1 { get; set; }
        public string CustomerEmail1 { get; set; }
        public string CustomerMobile1 { get; set; }
        public string GuestName1 { get; set; }
        public string GuestEmail1 { get; set; }
        public int CustomerType { get; set; }
        public string BillingCityname { get; set; }
        public string BillingCountryname { get; set; }
        public string BillingStatename { get; set; }
        public string CustomerpinCode { get; set; }
        public string BillingpinCode { get; set; }

        public string CategoryType { get; set; }
        public string MailContent { get; set; }
        public string HotelConfirmationNo { get; set; }

        public string ZipCode { get; set; }

        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string Guestname { get; set; }
        public int Noofnight { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double Swatchbharath { get; set; }
        public double KrishiKalyan { get; set; }
        public double SwatchbharathOthers { get; set; }
        public double KrishiKalyanOthers { get; set; }
        public string HotelFacility { get; set; }

        public long AmountPayable { get; set; }
        public string PayeeName { get; set; }

        // for display
        public int InvoiceStatus { get; set; }
        //for offline booking edit - status saved -* 1-no need to update *2 need to update
        public string result { get; set; }

        public bool NoInvoiceMail { get; set; }

        public int CustomerPaymentMode { get; set; }
        public string CustomerPayment { get; set; }
        public decimal CreditDays { get; set; }
        public string GSTRegistrationNo { get; set; }
        public string StateOfRegistration { get; set; }
        public long GSTstateid { get; set; }
        public long OffGSTId { get; set; }
        
        public bool SupplierCancellationDone { get; set; }
        public int HSNCodeCodeID { get; set; }
        public long PayeeID { get; set; }
        public string HSNCodeNatureOfService { get; set; }
        public int PlaceOfSupply { get; set; }
        public String PlaceOfSupplyName { get; set; }
        public int HSNCodeForSalesService { get; set; }
        //done by rahul
        //public string BillingFor { get; set; }
        public string BookingForSelf { get; set; }
        public string NewBillingFor { get; set; }
        public long CorporateID { get; set; }
        public long AssistedBy { get; set; }
        public string CostCentreID_Name { get; set;}
        public int CostCentre_ID { get; set; }
        public int CostCentreCode { get; set; }
        public int CostCentrePercentage { get; set; }
        //--
        public int SBEntityEntityId { get; set; }
        public int SBEntityEntityId1 { get; set; }
        public string SBEntityName { get; set; }

        public long ByPriceBeforeTax { get; set; }
        public long SalePriceGST { get; set; }
        public long ByPriceTotal { get; set; }
        public long SalePriceBeforeTax { get; set; }
        public long ByPriceGST { get; set; }
        public long SalePriceTotal { get; set; }
        public enum OfflineBookingsType { Normal = 1, GST = 2 }

        public long BookingDetailsId { get; set; }

        public string SbEntityBillingName { get; set; }
        public string SbEntityBillingAddress { get; set; }
        public string SbEntityBillingCountry { get; set; }
        public string SbEntityBillingState { get; set; }
        public string SbEntityBillingPhone { get; set; }
        public string SbEntityBillingGSTNo { get; set; }
        public double BookingTypeGST { get; set; }
        public double BookingTypeAmount { get; set; }
        public double BookingTypePercent { get; set; }
        public double BookingTypeTAC { get; set; }
        public bool BookingTypeGSTIncluded { get; set; }

        public decimal ReimbursementsAmount { get; set; }
        public string natureofreimbursements { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal OthersAmount { get; set; }
        public decimal AdvanceReceived { get; set; }  

        public string SubCustomerAddress { get; set; }
        public string SubCustomerpinCode { get; set; }
        public string SubCustomerCityname { get; set; }
        public int SubCustomerCity { get; set; }
        public string SubCustomerGstRegNo { get; set; }
        public long SubCustomerGstStateId { get; set; }

        public string PropertyEmailID { get; set; }

        // For reporting
        //public double bSGST { get; set; }
        //public double bCGST { get; set; }
        //public double bIGST { get; set; }

 public long SubCustomerid { get; set; }
        public int CustomerTableType { get; set; }
        public long FromCustomer { get; set; }
        public long FromCustomerId { get; set; }    //public double obSGST { get; set; }
        public string cancellationpolicy { get; set; }
        //public double obCGST { get; set; }
        //public double obIGST { get; set; }

        public double SGST { get; set; }
        public double CGST { get; set; }
        public double IGST { get; set; }

        public double btSGST { get; set; }
        public double btCGST { get; set; }
        public double btIGST { get; set; }


        public double oSGST { get; set; }
        public double oCGST { get; set; }
        public double oIGST { get; set; }

        public double DirectAmount { get; set; }
        public double ORCAmount { get; set; }
        public int BookingTypeInt { get; set; }

        // Booking For  
        public long BookingForID { get; set; }
        public long MasterCustomerID { get; set; }
        public string DirectCustomerName { get; set; }
        public string DirectCustomerEmail { get; set; }
        public string DirectCustomerMobile { get; set; }
        public string DirectCustomerAddress { get; set; }
        public int DirectCustomerCountry { get; set; }
        public int DirectCustomerState { get; set; }
        public long DirectCustomerCity { get; set; }
        public string DirectCustomerCityname { get; set; }
        public string DirectPinCode { get; set; }



        public double VBuyPriceBeforeTax { get; set; }
        public double VSalePriceBeforeTax { get; set; }
  //      public double VBuyPriceTotal { get; set; }
        public double vSalePriceTotal { get; set; }
  //      public double vBuyTax { get; set; }
         public double vSaleGST { get; set; }
        public int TotalNights { get; set; }

        public string AgentName { get; set; }
        public string AgentBank { get; set; }
        public string AgentAccount { get; set; }
        public string AgentBranch { get; set; }
        public string AgentPAN { get; set; }

        public decimal AmtTobePaid { get; set; }

        public int SupplierPaymentMode { get; set; }
        public DateTime SupplierPaymentModeDate { get; set; }
        public decimal SupplierPaymentAmount { get; set; }

        public int SupplierCreditDays { get; set; }


        public DateTime  PaymentScheduleDate1 { get; set; }
        public DateTime PaymentScheduleDate2 { get; set; }
        public DateTime PaymentScheduleDate3 { get; set; }

        public decimal PaymentScheduleAmount1 { get; set; }
        public decimal PaymentScheduleAmount2 { get; set; }
        public decimal PaymentScheduleAmount3 { get; set; }

        public string BeneficiaryName { get; set; }
        public string AccountNo { get; set; }
        public string PropertyBank { get; set; }
        public string AccountTypeCode { get; set; }
        public string IFSCCode { get; set; }

        public string AmountPayableValue { get; set; }
        public string BankUploadReportDate { get; set; }

        public string BankUploadText1 { get; set; }
        public string BankUploadText2 { get; set; }
        public string BankUploadTextBlank { get; set; }

        public string StayBazarWebsite { get; set; }

        public string SupplierPaymentName { get; set; }

        public string CustomerPaymentName { get; set; }

        public List<SupplierPaymentSchedule> SupplierPaymentScheduleList { get; set; }
        public Boolean IsSupplierPaymentMailSend { get; set; }
        public decimal TotalBuyPriceforotherservicesForBuyprice { get; set; }
        public decimal PayableSalePrice { get; set; }
        public decimal SumTotalSalePrice { get; set; }
        public decimal SumofAdvanceReceived { get; set; }
        public string PaymentLinkStatus { get; set; }
        public string PaymentLinkID { get; set; }
        public void ObjectStatus()
        {
            BookingType = CLayer.ObjectStatus.OfflineBookingType.Regular;
        }
    }


    public class TaxPercentage
    {
        public long TaxPerID { get; set; }
        public long TaxOfflineBookingID { get; set; }
        public string TaxTitle { get; set; }
        public string TaxPercent { get; set; }
        public string TaxType { get; set; }
        public string rowSet { get; set; }
        public string Type { get; set; }
        public long BookingID { get; set; }
        public long vendorId { get; set; }
        



    }

    public class SupplierPaymentSchedule
    {
        public int PaymentscheduleId { get; set; }

        public int BookingId { get; set; }
        public int SupplierPaymentMode { get; set; }
        public DateTime SupplierPaymentModeDate { get; set; }
        public decimal SupplierPaymentAmount { get; set; }
public int SupplierCreditDays { get; set; }    }
}
