using StayBazar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using BLayer;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Models
{
    
    public class OfflineBookingModel
    {
       
        //Customer details
        public string SecurityToken { get; set; }
        public string SessionId { get; set; }
        public string SequenceNumber { get; set; }
        public string hotelcode { get; set; }
        public int InventoryAPIType { get; set; }


        public long OfflineBookingId { get; set; }
        public long BookingId { get; set; }
        public long VendorpaymentsId { get; set; }
        public string ConfirmationNumber { get; set; }
        public long CustomerId { get; set; }
        public string CategoryType { get; set; }
        public int GSTStateDiffrent { get; set; }

        public int BookingType { get; set; }
        public SelectList BookingTypes { get; set; }

        //Done by Rahul
        //public string BillingFor { get; set; }
        public string BookingForSelf { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter Customer Name")]
        public string CustomerName { get; set; }
        public string CustomerName2 { get; set; }
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter Customer Name")]
        public string CustomerName1 { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Customer Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Customer Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CustomerEmail1 { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Enter Customer Mobile")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string CustomerMobile { get; set; }
        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Enter Customer Mobile")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string CustomerMobile1 { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Customer Address")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Customer Address")]
        public string CustomerAddress1 { get; set; }
        public int CustomerType { get; set; }



        [Display(Name = "Customer Country")]
        [Required(ErrorMessage = "Select Customer Country")]
        public int CustomerCountry { get; set; }

        [Display(Name = "Customer Country")]
        [Required(ErrorMessage = "Select Customer Country")]
        public int CustomerCountry1 { get; set; }

        [Display(Name = "Customer State")]
        [Required(ErrorMessage = "Select Customer State")]
        public int CustomerState { get; set; }

        [Display(Name = "Customer State")]
        [Required(ErrorMessage = "Select Customer State")]
        public int CustomerState1 { get; set; }

        [Display(Name = "Customer City")]
        [Required(ErrorMessage = "Select Customer City")]
        public int CustomerCity { get; set; }

        [Display(Name = "Customer City")]
        [Required(ErrorMessage = "Select Customer City")]
        public int CustomerCity1 { get; set; }

        [Display(Name = "Customer City")]
        [Required(ErrorMessage = "Select Customer City")]
        public int CustomerCity2 { get; set; }

        [Required(ErrorMessage = "Enter Customer City")]
        public string CustomerCityname { get; set; }

        [Required(ErrorMessage = "Enter Customer City")]
        public string BillingCityname { get; set; }

        [Display(Name = "Guest Name")]
        [Required(ErrorMessage = "Enter Guest Name")]
        public string GuestName { get; set; }

        [Display(Name = "Guest Name")]
        public string GuestName1 { get; set; }


        [Display(Name = "Guest Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Enter Guest Email Address")]
        public string GuestEmail { get; set; }

        [Display(Name = "Guest Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string GuestEmail1 { get; set; }

        //[Required(ErrorMessage = "Enter Guest Phone")]
        [Display(Name = "Guest Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string GuestPhone { get; set; }

        //vendors details
        [Display(Name = "Vendor Id")]
        public long vendorId { get; set; }
        [Display(Name = "Vendor Name")]
        //[Required(ErrorMessage = "Enter Vendor Name")]
        public string vendorname { get; set; }
        [Display(Name = "Vendor Address")]

        public string vendoraddress { get; set; }
        [Display(Name = "Address1")]
        public string address1 { get; set; }
        [Display(Name = "Address2")]
        public string address2 { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "Pin")]
        public string pin { get; set; }

        [Display(Name = "Pin Code")]
        public string CustomerpinCode { get; set; }
        [Display(Name = "Pin Code")]
        public string CustomerpinCode1 { get; set; }


        public SelectList CustomerGstStateList { get; set; }
        [Display(Name = "State Of Registration")]
        public long CustomerGstStateId { get; set; }
        [Display(Name = "GST Registration No")]
        [StringLength(15, ErrorMessage = "GST Registration No '15' characters", MinimumLength = 15)]
        public string CustomerGstRegNo { get; set; }


        public SelectList SubCustomerGstStateList { get; set; }
        [Display(Name = "GST Registration No")]
        [StringLength(15, ErrorMessage = "GST Registration No '15' characters", MinimumLength = 15)]
        public string SubCustomerGstRegNo { get; set; }
        public int CustomerTableType { get; set; }


        [Display(Name = "State Of Registration")]
        public long SubCustomerGstStateId { get; set; }
        //[Display(Name = "Contact Person")]
        //public string contactperson { get; set; }


        [Display(Name = "Contact No")]
        public string contactno { get; set; }
        [Display(Name = "Email Address")]

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string emailaddress { get; set; }

        [Display(Name = "Nature Of Service")]
        public string natureofservice { get; set; }
        [Display(Name = "value before service tax")]


        public decimal valuebeforeservicetax { get; set; }
        [Display(Name = "Service Tax Amount")]
        public decimal servicetaxamount { get; set; }
        [Display(Name = "Total Amount Payable")]
        public decimal totalamountpayable { get; set; }
        public List<CLayer.OfflineBooking> Vendorlist { get; set; }
        //Property Details
        public long PropertyId { get; set; }

        public long CustomPropertyId { get; set; }
        public long CustomSupplierId { get; set; }
        [Display(Name = "Property Name")]
        [Required(ErrorMessage = "Enter Property Name")]
        public string PropertyName { get; set; }

        [Display(Name = "Property Name")]
        [Required(ErrorMessage = "Enter Property Name")]
        public string PropertyName1 { get; set; }

        [Display(Name = "PIN Code")]
        public string PropertyPinCode { get; set; }
        [Display(Name = "PIN Code")]
        public string PropertyPinCode1 { get; set; }

        [Display(Name = "Property Address")]
        [Required(ErrorMessage = "Enter Property Address")]
        public string PropertyAddress { get; set; }

        [Display(Name = "GST Registration No")]
        [StringLength(15, ErrorMessage = "GST Registration No '15' characters", MinimumLength = 15)]
        public string PropertyGstRegNo { get; set; }

        [Display(Name = "Property Address")]
        [Required(ErrorMessage = "Enter Property Address")]
        public string PropertyAddress1 { get; set; }

        [Display(Name = "Property Contact Number")]
        [Required(ErrorMessage = "Enter Property Contact Number")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PropertyContactNo { get; set; }

        [Display(Name = "Property Contact Number")]
        [Required(ErrorMessage = "Enter Property Contact Number")]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PropertyContactNo1 { get; set; }

        [Display(Name = "Property Email")]
        [Required(ErrorMessage = "Enter Property Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string PropertyEmail { get; set; }

        [Display(Name = "Property Email")]
        [Required(ErrorMessage = "Enter Property Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string PropertyEmail1 { get; set; }

        [Display(Name = "Caretaker Name")]
        public string PropertyCaretakerName { get; set; }

        [Display(Name = "Caretaker Name")]
        public string PropertyCaretakerName1 { get; set; }


        [Display(Name = "Property Country")]
        [Required(ErrorMessage = "Enter Property Country")]
        public int PropertyCountry { get; set; }

        [Display(Name = "Property Country")]
        [Required(ErrorMessage = "Enter Property Country")]
        public int PropertyCountry1 { get; set; }

        [Required(ErrorMessage = "Enter Property State")]
        [Display(Name = "Property State")]
        public int PropertyState { get; set; }

        [Required(ErrorMessage = "Enter Property State")]
        [Display(Name = "Property State")]
        public int PropertyState1 { get; set; }

        [Required(ErrorMessage = "Enter Property State")]
        [Display(Name = "Property State")]
        public int PropertyState2 { get; set; }

        [Display(Name = "Property City")]
        [Required(ErrorMessage = "Select Property City")]
        public int PropertyCity { get; set; }
        [Display(Name = "Property City")]
        [Required(ErrorMessage = "Select Property City")]
        public int PropertyCity1 { get; set; }

        [Required(ErrorMessage = "Enter Property City")]
        public string PropertyCityname { get; set; }

        //karthikms added on 30-10-2019
        public string SbEntityName { get; set; }

        [Required(ErrorMessage = "Enter Property City")]
        public string PropertyCityname1 { get; set; }

        //Supplier Details
        public long SupplierId { get; set; }


        [Display(Name = "Supplier Name")]
        //[Required(ErrorMessage = "Enter Supplier Name")]
        public string SupplierName1 { get; set; }

        public long SupplierIdforprop { get; set; }
        [Display(Name = "Supplier Name")]
        //[Required(ErrorMessage = "Enter Supplier Name")]
        public string SupplierName { get; set; }

        [Display(Name = "Supplier Address")]
        [Required(ErrorMessage = "Enter Supplier Address")]
        public string SupplierAddress { get; set; }
        public string SupplierAddress1 { get; set; }

        [Display(Name = "PIN Code")]
        public string SupplierPinCode { get; set; }
        public string SupplierPinCode1 { get; set; }

        [Display(Name = "Supplier Mobile")]
        [Required(ErrorMessage = "Enter Supplier Mobile")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string SupplierMobile { get; set; }
        public string SupplierMobile1 { get; set; }

        [Display(Name = "Supplier Email")]
        [Required(ErrorMessage = "Enter Supplier Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string SupplierEmail { get; set; }
        public string SupplierEmail1 { get; set; }

        [Display(Name = "Supplier Country")]
        [Required(ErrorMessage = "Enter Supplier Country")]
        public int SupplierCountry { get; set; }
        public int SupplierCountry1 { get; set; }

        [Display(Name = "Supplier State")]
        [Required(ErrorMessage = "Enter Supplier State")]
        public int SupplierState { get; set; }
        public int SupplierState1 { get; set; }

        [Display(Name = "Supplier City")]
        [Required(ErrorMessage = "Select Supplier City")]
        public int SupplierCity { get; set; }
        [Display(Name = "Supplier City")]
        [Required(ErrorMessage = "Select Supplier City")]
        public int SupplierCity1 { get; set; }

        public string SupplierCityname { get; set; }
        public string SupplierCityname1 { get; set; }

        //Booking Details
        [Display(Name = "Check In Date")]
        [Required]
        public string CheckIn { get; set; }

        [Display(Name = "Check Out Date")]
        [Required]
        public string CheckOut { get; set; }

        [Display(Name = "Booking Date")]
        [Required]
        public string BookingDate { get; set; }

        [Display(Name = "Staybazar Booking Entity")]
        public int SBEntity { get; set; }
        [Display(Name = "Staybazar Billing Entity")]
        public int SBEntity1 { get; set; }


        public int HSNCode { get; set; }
        public int HSNCodeForSalesService { get; set; }
        public int PlaceOfSupply { get; set; }

        [Display(Name = "No: Of Night")]
        [Range(1, Int64.MaxValue, ErrorMessage = "No of Night must be at least 1")]
        public long NoOfNight { get; set; }

        [Display(Name = "Adult")]
        public long NoOfPaxAdult { get; set; }

        [Display(Name = "Child")]
        public long NoOfPaxChild { get; set; }

        [Display(Name = "No Of Rooms")]
        [Range(1, Int64.MaxValue, ErrorMessage = "No of Rooms must be at least 1")]
        public long NoOfRooms { get; set; }
        public string SalesRegion { get; set; }
        [Display(Name = "StayCategory")]
        [Required]
        public long StayCategory { get; set; }


        [Display(Name = "StayCategory Name")]
        public string StayCategoryName { get; set; }

        [Display(Name = "Accommodatoin Name")]
        [Required(ErrorMessage = "Select Accommodation Type")]
        public string AccommodationTypeName { get; set; }

        [Display(Name = "Accommodatoin Type")]
        [Required]
        public long AccommodatoinType { get; set; }

        public long Accommodationtypeid { get; set; }
        public long Accommodationid { get; set; }
        public long StayCategoryid { get; set; }

        //Pricing Details

        //buy price


        [Display(Name = "Average Daily Rate Before Service tax")]
        [Required]
        public decimal AvgDailyRateBefreStaxForBuyPrice { get; set; }

        [Display(Name = "Service tax")]
        [Required]
        public decimal StaxForBuyPrice { get; set; }

        [Display(Name = "Total Amount - Accommodation")]
        [Required]
        public decimal TotalAmtForBuyPrice { get; set; }


        [Display(Name = "Buy Price for other services")]
        [Required]
        public decimal BuyPriceforotherservicesForBuyprice { get; set; }

        [Display(Name = "Service tax - other services")]
        [Required]
        public decimal StaxForotherBuyPrice { get; set; }

        [Display(Name = "Total Amount - other services")]
        [Required]
        public decimal TotalAmtotherForBuyPrice { get; set; }


        [Display(Name = "Total Buy Value")]
        [Required]
        public decimal TotalBuyPrice { get; set; }

        //sale price


        [Display(Name = "Average Daily Rate Before Service tax")]
        [Required]
        public decimal AvgDailyRateBefreStaxForSalePrice { get; set; }

        [Display(Name = "Service tax")]
        [Required]
        public decimal StaxForSalePrice { get; set; }

        [Display(Name = "Total Amount")]
        [Required]
        public decimal TotalAmtForSalePrice { get; set; }




        [Display(Name = "Sale Price for other services")]
        [Required]
        public decimal BuyPriceforotherservicesForSalePrice { get; set; }

        [Display(Name = "Service tax - other services ")]
        [Required]
        public decimal StaxForotherForSalePrice { get; set; }

        [Display(Name = "Total Amount - other services")]
        [Required]
        public decimal TotalAmtotherForSalePrice { get; set; }

        [Display(Name = "Total Sale Value")]
        [Required]
        public decimal TotalSalePrice { get; set; }


        //Other service details
        [Display(Name = "Details Of other services ")]
        public string OtherService { get; set; }

        //Other service details
        [Display(Name = "Nature Of other services ")]
        public string OtherServiceNature { get; set; }

        //mail details
        [Display(Name = "CC Emails (Please put comma between emails)")]
        public string MailContent { get; set; }

        [Display(Name = "Hotel Confirmation Number")]
        public string HotelConformationNo { get; set; }

        public long PayeeID { get; set; }

        [Display(Name = "Payee Name")]
        public string PayeeName { get; set; }
        [Display(Name = "Amount Payable")]
        public long AmountPayable { get; set; }



        [Display(Name = "Customer Payment Mode")]
        [Required(ErrorMessage = "Select Customer Payment Mode")]
        public int CustomerPaymentModeId { get; set; }
        public SelectList CustomerPaymentModeList { get; set; }
        //done by rahul 23/11/19
        public string NewBillingFor { get; set; }
        public int AssistedBy { get; set; }
        public int CorporateUserID1 { get; set; }
        public int AssistedCorporateUserID { get; set; }
        public int CorporateUserID { get; set; }
        public int CorporateID1 { get; set; }
        public int CorporateID { get; set; }
        public int CostCentre_ID { get; set; }
        public int CostCenter_Codee { get; set; }
        [Required(ErrorMessage = "Enter Percentage")]
        [Range(1, 100)]
        public int CostCentrePercentage { get; set; }
        //---
        [Display(Name = "Credit Days")]
        public decimal CustomerPaymentCreditPeriod { get; set; }

        public DateTime CreatedTime { get; set; }

        public SelectList SBEntityLIst { get; set; }
        public SelectList SBEntityLIst1 { get; set; }
        public SelectList HSNCodeList { get; set; }

        public SelectList CusCountryList { get; set; }
        public SelectList CusCityList { get; set; }
        public SelectList CusStateList { get; set; }

        public SelectList CusPaymentModeList { get; set; }

        public SelectList PropCountryList { get; set; }
        public SelectList PropCityList { get; set; }
        public SelectList PropStateList { get; set; }

        public SelectList SupCountryList { get; set; }
        public SelectList SupCityList { get; set; }
        public SelectList SupStateList { get; set; }

        public SelectList BillingCountryList { get; set; }
        public SelectList BillingStateList { get; set; }
        public SelectList BillingCityList { get; set; }


        [Display(Name = "Don't send mail to supplier")]
        public bool sendmailtosupplier { get; set; }
        [Display(Name = "Don't send mail to Customer")]
        public bool sendmailtocustomer { get; set; }

        public List<TaxPercentage> TaxPercentageList_Service { get; set; }
        public List<TaxPercentage> TaxPercentageList_Others { get; set; }
        public List<TaxPercentage> TaxPercentageList_ServiceByPrice { get; set; }
        public List<TaxPercentage> TaxPercentageList_OthersByPrice { get; set; }
        public List<TaxPercentage> TaxPercentageList_ServiceSalePrice { get; set; }
        public List<TaxPercentage> TaxPercentageList_OthersSalePrice { get; set; }

        public List<CLayer.OfflineBooking> OfflineBookingList { get; set; }
        public DataTable OfflineBookingTable { get; set; }

        public int currentPage { get; set; }
        public int SearchValue { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public string SearchStringCache { get; set; }
        public long TotalRows { get; set; }


        public enum OfflineBookingStatusValues { Name = 1, Phone = 2, Email = 3, Booking_ID = 4 }

        public List<CLayer.OfflineBooking> CustompropertyList { get; set; }
        public List<CLayer.OfflineBooking> CustomCustomerList { get; set; }
        public List<CLayer.OfflineBooking> BookedpropertyList { get; set; }
        public List<CLayer.OfflineBooking> BookingList { get; set; }




        public int SaveStatus { get; set; }

        public int CreatedUser { get; set; }

        public SelectList SavedStatusTypes { get; set; }
        public SelectList SavedStatusTypes_Manage { get; set; }

        public SelectList CustomerPaymentMode_Manage { get; set; }

        public SelectList CreatedUsers { get; set; }

        public int Direction { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedName { get; set; }
        [Display(Name = "Sales Person")]
        [Required(ErrorMessage = "Select SalesPerson")]
        public long SalesPersonId { get; set; }
        public SelectList SalesPerson { get; set; }
        public string SalesPersonName { get; set; }
        public SelectList ECostCentre { get; set; }
        //Done by Rahul
        public SelectList CorporateUserName1 { get; set; }
        public SelectList MyAssistedUsers { get; set; }
        public SelectList CorporateUserName_ForCorporateUsers { get; set; }
        public SelectList CorporateName1 { get; set; }
        public string CorpUserName { get; set; }
        public SelectList CorporateUserName { get; set; }
        public SelectList CorporateName { get; set; }
        //--
        [Display(Name = "Account No")]
        public string AccountNumber { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "Branch Name")]
        public string BranchName { get; set; }

        [Display(Name = "Branch Address")]
        public string BranchAddress { get; set; }

        [Display(Name = "Account Name")]
        public string AccountName { get; set; }

        [Display(Name = "Account Type")]
        public string AccountType { get; set; }

        public SelectList AccountTypeList { get; set; }

        [Display(Name = "RTGS / IFSC Code")]
        public string IFSCcode { get; set; }

        [Display(Name = "PAN")]
        public string PAN { get; set; }


        [Display(Name = "MICR Code")]
        public string MICRcode { get; set; }

        [Display(Name = "Care Taker Phone")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string CareTakerPhone { get; set; }
        public long OffGSTId { get; set; }
        [Display(Name = "GST Registration No")]
        public string GSTRegistrationNo { get; set; }
        [Display(Name = "State Of Registration")]
        public string StateOfRegistration { get; set; }



        [Display(Name = "Care Taker Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CareTakerEmail { get; set; }


        [Display(Name = "GST Registration No")]
        public string SubPropertyGstRegNo { get; set; }
        //Offline booking coustmer start


        [Display(Name = "Address")]
        public string SubCustomerAddress { get; set; }

        [Display(Name = "Pin Code")]
        public string SubCustomerpinCode { get; set; }


        public string SubCustomerCityname { get; set; }
        public string HiddenSubCustomerCityname { get; set; }

        public long HiddenSubCustomerCity { get; set; }
        public string HiddenSubCustomerAddress { get; set; }
        public string HiddenSubCustomerpinCode { get; set; }
        public string HiddenSubCustomerGstRegNo { get; set; }
        public long HiddenCustomerTableType { get; set; }

        [Display(Name = "City")]
        public int SubCustomerCity { get; set; }
        public SelectList SubCusCityList { get; set; }



        public long OfficeCoustomerID { get; set; }
        public long OfficeBookingID { get; set; }
        public long CoustomerID { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Customer Address")]
        public string BillingAddress { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "Select Customer Country")]
        public int BillingCountry { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "Select Customer State")]
        public int BillingState { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "Select Customer City")]
        public int BillingCity { get; set; }

        public int BillingCity1 { get; set; }
        public int BillingState1 { get; set; }
        public int BillingCountryId1 { get; set; }
        public string PinCode1 { get; set; }
        public string BillingMobile1 { get; set; }
        public string ContactPerson1 { get; set; }
        public string OfficeNO1 { get; set; }
        public string BillingAddress1 { get; set; }

        [Display(Name = "Contact Person Name")]
        [Required(ErrorMessage = "Enter Contact Person's Name")]
        public string ContactPerson { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Enter Billing Customer Mobile No")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string BillingMobile { get; set; }

        [Display(Name = "PIN Code")]
        public string PinCode { get; set; }

        [Display(Name = "Office NO")]
        public string OfficeNO { get; set; }

        [Display(Name = "Country")]
        public int BillingCountryId { get; set; }

        public long UserType { get; set; }
        public string UserTypeName { get; set; }
        public long BillingStateID { get; set; }


        [Display(Name = "Customer Reference No")]
        public string NewCustomerReferenceNo { get; set; }


        [Display(Name = "Amenities")]
        [AllowHtml]
        public string HotelFacility { get; set; }

        [Display(Name = "Don't send Invoice mail")]
        public bool NoInvoiceMail { get; set; }
        public bool NoInvoiceMail1 { get; set; }

        [Display(Name = "Customer Payment Mode")]
        [Required(ErrorMessage = "Select customer payment mode")]
        public int CustomerPaymentMode { get; set; }
        public decimal CreditDays { get; set; }

        [Display(Name = "Buy price Before Tax")]
        public long ByPriceBeforeTax { get; set; }
        [Display(Name = "Sale price Before Tax")]
        public long SalePriceBeforeTax { get; set; }
        [Display(Name = "GST")]
        public long SalePriceGST { get; set; }
        [Display(Name = "GST")]
        public long ByPriceGST { get; set; }

        [Display(Name = "Total Sale Value")]
        public long SalePriceTotal { get; set; }
        [Display(Name = "Total By Value")]
        public long ByPriceTotal { get; set; }
        public long BookingDetailsId { get; set; }

        public double BookingTypeGST { get; set; }
        public double BookingTypeAmount { get; set; }
        public double BookingTypePercent { get; set; }
        public double BookingTypeTAC { get; set; }
        public bool BookingTypeGSTIncluded { get; set; }
        public int BookingTypeHSNCode { get; set; }
        public SelectList BookingTypeHSNCodes { get; set; }
        public List<TaxPercentage> BookingTypeTaxes { get; set; }

        [Display(Name = "Reimbursements")]
        public decimal ReimbursementsAmount { get; set; }

        [Display(Name = "Nature Of Reimbursements")]
        public string natureofreimbursements { get; set; }

        [Display(Name = "Discount")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Others")]
        public decimal OthersAmount { get; set; }

        [Display(Name = "Advance Received")]
        public decimal AdvanceReceived { get; set; }
       

        public long FromCustomer { get; set; }
        public long FromCustomerId { get; set; }
        //Added by rahul 22-11-19
        
        
        [Display(Name ="Percentage")]
        public int Percentage { get; set; }
         //-----------
        // Booking For 
        public long BookingForID { get; set; }
        [Display(Name = "Customer Name")]
       // [Required(ErrorMessage = "Enter Customer Name")]
        public string DirectName { get; set; }

        [Display(Name = "Customer Country")]
        //[Required(ErrorMessage = "Select Customer Country")]
        public int DirectCountry { get; set; }

        [Display(Name = "Customer State")]
        //[Required(ErrorMessage = "Select Customer State")]
        public int DirectState { get; set; }

        [Display(Name = "Mobile")]
       // [Required(ErrorMessage = "Enter Customer Mobile")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string DirectMobile { get; set; }

        [Display(Name = "Email")]
        //[Required(ErrorMessage = "Enter Customer Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string DirectEmail { get; set; }

        [Display(Name = "Customer City")]
        //[Required(ErrorMessage = "Select Customer City")]
        public long DirectCity { get; set; }

        [Display(Name = "Address")]
        //[Required(ErrorMessage = "Enter Customer Address")]
        public string DirectAddress { get; set; }

        public string DirectpinCode { get; set; }
        [Display(Name = "Customer Name")]
       // [Required(ErrorMessage = "Enter Customer Name")]
        public string DirectNameNew { get; set; }

        [Display(Name = "Customer Country")]
        //[Required(ErrorMessage = "Select Customer Country")]
        public long DirectCountryNew { get; set; }

        [Display(Name = "Customer State")]
       // [Required(ErrorMessage = "Select Customer State")]
        public long DirectStateNew { get; set; }

        [Display(Name = "Mobile")]
       // [Required(ErrorMessage = "Enter Customer Mobile")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string DirectMobileNew { get; set; }

        [Display(Name = "Email")]
       // [Required(ErrorMessage = "Enter Customer Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string DirectEmailnew { get; set; }

        [Display(Name = "Customer City")]
        //[Required(ErrorMessage = "Select Customer City")]
        public long DirectCityNew { get; set; }

        [Display(Name = "Address")]
       // [Required(ErrorMessage = "Enter Customer Address")]
        public string DirectAddressNew { get; set; }

        [Display(Name = "Pin Code")]
        public string DirectpinCodeNew { get; set; }

       // [Required(ErrorMessage = "Enter Customer City")]
        public string DirectCitynames { get; set; }

       // [Required(ErrorMessage = "Enter Customer City")]
        public string DirectCitynameNew { get; set; }
        public long MasterCustomerID { get; set; }

        public SelectList DirectCountryList { get; set; }
        public SelectList DirectCityList { get; set; }
        public SelectList DirectStateList { get; set; }
        public long LoginUserid { get; set; }

        [Display(Name = "Supplier Cancellation Done ?")]
        public bool SupplierCancellationDone { get; set; }

        [Display(Name = "Cancelation Policy")]
        public string cancellationpolicy  { get; set; }

        [Required(ErrorMessage = "Select Supplier payment mode")]
        [Display(Name = "Payment Mode")]
        public int SupplierPaymentMode { get; set; }
        public DateTime SupplierPaymentDate { get; set; }
        public DateTime SupplierPaymentAmount { get; set; }

        [Display(Name = "Supplier Payment Credit Days")]
        public int SupplierPaymentCreditDays { get; set; }
        public SelectList SupplierPaymentMode_Manage { get; set; }
        public List<CLayer.OfflineBooking> costcentre { get; set; }
        public void SetBookingTypeTax(List<KeyValuePair<String, double>> taxes)
        {
            if (taxes != null)
            {
                int i, j, cnt;
                cnt = taxes.Count;
                if (cnt > 4) { cnt = 4; }
                j = 0;
                for (i = 0; i < cnt; i++)
                {
                    if (taxes[i].Value > 0)
                    {
                        BookingTypeTaxes[j] = new TaxPercentage() { TaxTitle = taxes[i].Key, TaxPercent = taxes[i].Value.ToString() };
                        j++;
                    }
                }
            }
        }
        public int PropertySupplierCountry { get; set; }
        public int PropertySupplierstate { get; set; }

        public string PropertySupplierstateName { get; set; }
        public string PropertySuppliercityname { get; set; }
        //Offline booking coustmer end      
        public OfflineBookingModel()
        {

            CheckIn = DateTime.Now.ToShortDateString();
            CheckOut = DateTime.Now.AddDays(1).ToShortDateString();

            BookingType = (int)CLayer.ObjectStatus.OfflineBookingType.Regular;
            BookingTypeTaxes = new List<TaxPercentage>();
            BookingTypeTaxes.Add(new TaxPercentage() { TaxTitle = "", TaxPercent = "" });
            BookingTypeTaxes.Add(new TaxPercentage() { TaxTitle = "", TaxPercent = "" });
            BookingTypeTaxes.Add(new TaxPercentage() { TaxTitle = "", TaxPercent = "" });
            BookingTypeTaxes.Add(new TaxPercentage() { TaxTitle = "", TaxPercent = "" });

            BookingTypeGST = 0;
            BookingTypePercent = 0;
            BookingTypeTAC = 0;

            var btypes = new List<SelectListItem>
            {
        new SelectListItem{ Text="Select", Value = "0" },
        new SelectListItem{ Text="Regular Booking", Value = ((int) CLayer.ObjectStatus.OfflineBookingType.Regular ).ToString() },
        new SelectListItem{ Text="TAC Booking", Value = ((int) CLayer.ObjectStatus.OfflineBookingType.TAC ).ToString() },
        new SelectListItem{ Text="Direct Booking", Value = ((int) CLayer.ObjectStatus.OfflineBookingType.Direct ).ToString() }
            };
            BookingTypes = new SelectList(btypes, "Value", "Text", btypes[0]);


            var paymodes = new List<SelectListItem>
            {
       
        new SelectListItem{ Text="Advance Payment", Value = ((int) CLayer.ObjectStatus.CustomerPaymentMode.Advance_Payment ).ToString() },
        new SelectListItem{ Text="Payment on check-out", Value = ((int) CLayer.ObjectStatus.CustomerPaymentMode.Payment_on_check_out ).ToString() },
        new SelectListItem{ Text="Credit", Value = ((int) CLayer.ObjectStatus.CustomerPaymentMode.Credit ).ToString() }
            };
            CustomerPaymentModeList = new SelectList(paymodes, "Value", "Text", paymodes[0]);




            var list = new List<SelectListItem>
            {
        new SelectListItem{ Text="Savings Bank", Value = "Savings Bank" },
        new SelectListItem{ Text="Current", Value = "Current" }
            };

            SelectList sl = new SelectList(list, "Value", "Text");
            AccountTypeList = sl;

            

            List<CLayer.User> SalesPersons = BLayer.User.GetAllSalespersonandRegionalmanager();
            // SalesPersons.Insert(0, (new CLayer.User { UserId = 0, Name = "Select" }));
            SalesPerson = new SelectList(SalesPersons, "UserId", "Name");

            List<CLayer.OfflineBooking> CreatedUser = BLayer.OfflineBooking.GetAllcreatedUsers();
            CreatedUsers = new SelectList(CreatedUser, "CreatedBy", "CreatedName");

            //done by rahul
          
            //This is for displaying Corporate user For Admin
            List<CLayer.B2BUser> CorporateList = BLayer.B2BUser.GetCorporateName();
            CorporateName = new SelectList(CorporateList, "B2BId", "FirstName");
            //For getting Corporate User's Name under Corporate
            List<CLayer.B2BUser> CorporateUserList = BLayer.B2BUser.GetOnCorporateUserList((int)CorporateList[0].B2BId);
            CorporateUserName = new SelectList(CorporateUserList, "UserId", "FirstName");


            //This is for displaying Corporate user For Corporate


            CorpUserName = System.Web.HttpContext.Current.User.Identity.GetUserId();
            List<CLayer.B2BUser> CorporateUserList1 = BLayer.B2BUser.getoncorporateuserlist1(CorpUserName);
            CorporateUserName1 = new SelectList(CorporateUserList1, "UserId", "FirstName");


            var myid = System.Web.HttpContext.Current.User.Identity.GetUserId();//This is for getting Login User Id
            List<CLayer.B2BUser> CorporateName_UnderLogin = BLayer.B2BUser.getoncorporateusername(myid);
            CorporateName1 = new SelectList(CorporateName_UnderLogin, "B2BId", "B2BId");
            //Below Code is used for AssistedCorporate User list
            if (CorporateName_UnderLogin.Count > 0)
            {
                List<CLayer.B2BUser> CorporateAssistedUserList = BLayer.B2BUser.getAllAssistedCorporateUserNames((int)CorporateName_UnderLogin[0].B2BId, myid);
                CorporateUserName_ForCorporateUsers = new SelectList(CorporateAssistedUserList, "UserId", "FirstName");
            }
            else
            {
                List<CLayer.B2BUser> CorporateAssistedUserList = BLayer.B2BUser.getAllCorporateUserName(myid);
                CorporateUserName_ForCorporateUsers = new SelectList(CorporateAssistedUserList, "UserId", "FirstName");
            }


            List<CLayer.B2BUser> MyAssistedUserList = BLayer.B2BUser.getMyAssistedUsername(myid);
            MyAssistedUsers = new SelectList(MyAssistedUserList, "UserId", "FirstName");
            //CorpUserName= (int)WebSecurity.CurrentUserId;

            //CorpUserName = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUser"]);
            //List<CLayer.B2BUser> CorporateListName = BLayer.B2BUser.GetCorporateUsers(CorpUserName);
            //CorporateUserNameList = new SelectList(CorporateListName, "ID", "Name");



            List<CLayer.CostCentre> listCostCentre = BLayer.CostCentre.GetAll();
            ECostCentre = new SelectList(listCostCentre, "CostCentreCode", "CostcentreName");

            //---------


            List<CLayer.OfflineBooking> HSNCode = BLayer.OfflineBooking.GetHSNCode();
            HSNCodeList = new SelectList(HSNCode, "HSNCodeCodeID", "HSNCodeNatureOfService");

            List<CLayer.HSNCode> BTypeHsn = BLayer.HSNCode.GetAll();
            BTypeHsn.Add(new CLayer.HSNCode() { NatureOfService = "Select", CodeId = 0 });
            BookingTypeHSNCodes = new SelectList(BTypeHsn, "CodeId", "NatureOfService");

            List<CLayer.OfflineBooking> SBEntity = BLayer.OfflineBooking.GetSBEntity();
            SBEntityLIst = new SelectList(SBEntity, "SBEntityEntityId", "SBEntityName");

            //karthikms added on 30-10-2019
            List<CLayer.OfflineBooking> SBEntity1 = BLayer.OfflineBooking.GetSBEntity();
            SBEntityLIst1 = new SelectList(SBEntity1, "SBEntityEntityId", "SBEntityName");

            List<KeyValuePair<int, string>> objStatustypes1 = new List<KeyValuePair<int, string>>();
            objStatustypes1.Add(new KeyValuePair<int, string>(0, "All"));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.Statuslist.Save, "Saved"));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.Statuslist.Submit, "Submitted"));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.Statuslist.Approved, "Approved"));

            SavedStatusTypes = new SelectList(objStatustypes1, "Key", "Value");

            List<KeyValuePair<int, string>> objStatustypes_mang = new List<KeyValuePair<int, string>>();
            objStatustypes_mang.Add(new KeyValuePair<int, string>(0, "All"));
            objStatustypes_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.Statuslist.Submit, "Submitted"));
            objStatustypes_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.Statuslist.Approved, "Approved"));
            objStatustypes_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.Statuslist.Draft, "Draft"));
            SavedStatusTypes_Manage = new SelectList(objStatustypes_mang, "Key", "Value");

            List<KeyValuePair<int, string>> objCustomerPaymentMode_mang = new List<KeyValuePair<int, string>>();
            //objCustomerPaymentMode_mang.Add(new KeyValuePair<int, string>(0, "Select"));
            objCustomerPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.CustomerPaymentModelist.AdvancePayment, "Advance Payment"));
            objCustomerPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.CustomerPaymentModelist.PaymentOnCheckOut, "Payment Check-out"));
            objCustomerPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.CustomerPaymentModelist.Credit, "Credit"));
            CustomerPaymentMode_Manage = new SelectList(objCustomerPaymentMode_mang, "Key", "Value");

            //for customer

            
            List<CLayer.Country> BillingCountryLists = BLayer.Country.GetAll();
            BillingCountryList = new SelectList(BillingCountryLists, "CountryId", "Name");

            if (BillingCountryLists.Count > 0)
            {
                List<CLayer.State> Cusstates = BLayer.State.GetOnCountry((int)BillingCountryLists[0].CountryId);
                CusStateList = new SelectList(Cusstates, "StateId", "Name");

                List<CLayer.City> Cuscities = null;
                if (Cusstates.Count > 0)
                {
                    Cuscities = BLayer.City.GetOnState((int)Cusstates[0].StateId);
                }
                else
                {
                    Cuscities = new List<CLayer.City>();
                }
                Cuscities.Add(new CLayer.City() { CityId = -1, Name = "Others" });
                CusCityList = new SelectList(Cuscities, "CityId", "Name");
            }


            List<CLayer.Country> Cuscountries = BLayer.Country.GetAll();
            CusCountryList = new SelectList(Cuscountries, "CountryId", "Name");
            if (Cuscountries.Count > 0)
            {
                List<CLayer.State> Cusstates = BLayer.State.GetOnCountry((int)Cuscountries[0].CountryId);
                CusStateList = new SelectList(Cusstates, "StateId", "Name");

                List<CLayer.City> Cuscities = null;
                if (Cusstates.Count > 0)
                {
                    Cuscities = BLayer.City.GetOnState((int)Cusstates[0].StateId);
                }
                else
                {
                    Cuscities = new List<CLayer.City>();
                }
                CusCityList = new SelectList(Cuscities, "CityId", "Name");
            }
            // bookng For
            List<CLayer.Country> DirectCuscountries = BLayer.Country.GetAll();
            DirectCountryList = new SelectList(DirectCuscountries, "CountryId", "Name");
            if (DirectCuscountries.Count > 0)
            {
                List<CLayer.State> Cusstates = BLayer.State.GetOnCountry((int)DirectCuscountries[0].CountryId);
                DirectStateList = new SelectList(Cusstates, "StateId", "Name");

                List<CLayer.City> Cuscities = null;
                if (Cusstates.Count > 0)
                {
                    Cuscities = BLayer.City.GetOnState((int)Cusstates[0].StateId);
                }
                else
                {
                    Cuscities = new List<CLayer.City>();
                }
                DirectCityList = new SelectList(Cuscities, "CityId", "Name");
            }


            //for property

            List<CLayer.Country> propcountries = BLayer.Country.GetAll();
            PropCountryList = new SelectList(propcountries, "CountryId", "Name");
            if (propcountries.Count > 0)
            {
                List<CLayer.State> propstates = BLayer.State.GetOnCountry((int)propcountries[0].CountryId);
                PropStateList = new SelectList(propstates, "StateId", "Name");

                List<CLayer.City> Propcities = null;
                //List < CLayer.OfflineBooking > PropEntity = null;
                if (propstates.Count > 0)
                {
                    Propcities = BLayer.City.GetOnState((int)propstates[0].StateId);

                    //PropEntity = BLayer.OfflineBooking.GetProbSBEntity((int)propstates[0].StateId);
                    //SBEntityLIst = new SelectList(PropEntity, "SBEntityEntityId", "SBEntityName");
                }
                else
                {
                    Propcities = new List<CLayer.City>();
                }
                Propcities.Add(new CLayer.City() { CityId = -1, Name = "Others" });
                PropCityList = new SelectList(Propcities, "CityId", "Name");
            }



            //for supplier
            List<CLayer.Country> Supcountries = BLayer.Country.GetAll();
            SupCountryList = new SelectList(Supcountries, "CountryId", "Name");
            if (Supcountries.Count > 0)
            {
                List<CLayer.State> Supstates = BLayer.State.GetOnCountry((int)Supcountries[0].CountryId);
                SupStateList = new SelectList(Supstates, "StateId", "Name");

                List<CLayer.City> Supcities = null;
                if (Supstates.Count > 0)
                {
                    Supcities = BLayer.City.GetOnState((int)Supstates[0].StateId);
                }
                else
                {
                    Supcities = new List<CLayer.City>();
                }
                Supcities.Add(new CLayer.City() { CityId = -1, Name = "Others" });
                SupCityList = new SelectList(Supcities, "CityId", "Name");
            }


            //GstCustomerStateList
            //  CustomerGstStateList
            //List<CLayer.State> CusGststeList = new List<CLayer.State>();
            List<CLayer.State> CusGststeList = new List<CLayer.State>();
            CusGststeList.Add(new CLayer.State() { StateId = -1, Name = "Not avilable" });
            //CusGststeList.AddRange(BLayer.State.GetCustGstStateList(Convert.ToInt32(MasterCustomerID),1));
            CustomerGstStateList = new SelectList(CusGststeList, "StateId", "Name");




            List<CLayer.State> SubCusGststeList = new List<CLayer.State>();
            SubCusGststeList.Add((new CLayer.State() { StateId = 0, Name = "Select" }));
            SubCusGststeList.AddRange(BLayer.State.GetAllState());
            SubCustomerGstStateList = new SelectList(SubCusGststeList, "StateId", "Name");


            List<CLayer.City> SubCusCityList1 = null;
            if (SubCusGststeList.Count > 0)
            {
                SubCusCityList1 = BLayer.City.GetOnState((int)SubCusGststeList[0].StateId);
                SubCusCityList1.Add(new CLayer.City() { CityId = -1, Name = "Others" });
            }
            else
            {
                SubCusCityList1 = new List<CLayer.City>();
            }

            SubCusCityList = new SelectList(SubCusCityList1, "CityId", "Name");

            List<KeyValuePair<int, string>> objSupplierPaymentMode_mang = new List<KeyValuePair<int, string>>();
            //objSupplierPaymentMode_mang.Add(new KeyValuePair<int, string>(0, "Select"));
            objSupplierPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.SupplierPaymentModelist.Credit, "Credit"));
            objSupplierPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.SupplierPaymentModelist.FullAmountBeforeCheckin, "Full amount before check-in"));
            objSupplierPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.SupplierPaymentModelist.FullAmountBeforeCheckout, "Full amount before check-out"));
            objSupplierPaymentMode_mang.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.SupplierPaymentModelist.OnInstalments, "On instalments"));
            SupplierPaymentMode_Manage = new SelectList(objSupplierPaymentMode_mang, "Key", "Value");
        }
        public void LoadgstcitiesbystatePlaces(int StateId)
        {
            List<CLayer.City> Cusstate = BLayer.City.GetOnState(StateId);
            SubCusCityList = new SelectList(Cusstate, "CityId", "Name");
        }
        public void LoadPlaces()
        {
            // for billing address
            List<CLayer.State> Cusstate = BLayer.State.GetOnCountry(CustomerCountry);
            BillingStateList = new SelectList(Cusstate, "StateId", "Name");

            List<CLayer.City> Cuscitie = null;
            if (Cusstate.Count > 0)
            {
                Cuscitie = BLayer.City.GetOnState(BillingState);
            }
            else
            {
                Cuscitie = new List<CLayer.City>();
            }
            Cuscitie.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            CusCityList = new SelectList(Cuscitie, "CityId", "Name");


            List<CLayer.State> Cusstates = BLayer.State.GetOnCountry(CustomerCountry1);
            CusStateList = new SelectList(Cusstates, "StateId", "Name");

            List<CLayer.City> Cuscities = null;
            if (Cusstates.Count > 0)
            {
                Cuscities = BLayer.City.GetOnState(CustomerState);
            }
            else
            {
                Cuscities = new List<CLayer.City>();
            }
            Cuscities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            CusCityList = new SelectList(Cuscities, "CityId", "Name");




            //for customer

            List<CLayer.State> Cusstatess = BLayer.State.GetOnCountry(CustomerCountry);
            CusStateList = new SelectList(Cusstatess, "StateId", "Name");

            List<CLayer.City> Cuscitiess = null;
            if (Cusstatess.Count > 0)
            {
                Cuscitiess = BLayer.City.GetOnState(CustomerState);
            }
            else
            {
                Cuscitiess = new List<CLayer.City>();
            }
            Cuscitiess.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            CusCityList = new SelectList(Cuscitiess, "CityId", "Name");


            //for property

            List<CLayer.State> Propstates = BLayer.State.GetOnCountry(PropertyCountry);
            PropStateList = new SelectList(Propstates, "StateId", "Name");

            List<CLayer.City> Propcities = null;
            //List<CLayer.OfflineBooking> PropEntity = null;
            if (Propstates.Count > 0)
            {
                Propcities = BLayer.City.GetOnState(PropertyState);

                //PropEntity = BLayer.OfflineBooking.GetProbSBEntity(PropertyState);
             
            }
            else
            {
                Propcities = new List<CLayer.City>();
            }
            Propcities.Add(new CLayer.City() { CityId = -1, Name = "Others" });
            Propcities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            PropCityList = new SelectList(Propcities, "CityId", "Name");
            //SBEntityLIst = new SelectList(PropEntity, "SBEntityEntityId", "SBEntityName");



            //for supplier

            List<CLayer.State> Supstates = BLayer.State.GetOnCountry(SupplierCountry);
            SupStateList = new SelectList(Supstates, "StateId", "Name");

            List<CLayer.City> Supcities = null;
            if (Supstates.Count > 0)
            {
                Supcities = BLayer.City.GetOnState(SupplierState);
            }
            else
            {
                Supcities = new List<CLayer.City>();
            }
            Supcities.Add(new CLayer.City() { CityId = -1, Name = "Others" });
            Supcities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            SupCityList = new SelectList(Supcities, "CityId", "Name");

        }
        public void LoadBookingForCity()
        {
            List<CLayer.State> Cusstate = BLayer.State.GetOnCountry(DirectCountry);
            DirectStateList = new SelectList(Cusstate, "StateId", "Name");

            List<CLayer.City> Cuscitie = null;
            if (Cusstate.Count > 0)
            {
                Cuscitie = BLayer.City.GetOnState(DirectState);
            }
            else
            {
                Cuscitie = new List<CLayer.City>();
            }
            Cuscitie.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            DirectCityList = new SelectList(Cuscitie, "CityId", "Name");
        }


        public DateTime FromDate { get; set; }
        public DateTime FromDate1 { get; set; }

        public DateTime ToDate1 { get; set; }
        public DateTime ToDate { get; set; }
        public List<CLayer.SupplierPaymentSchedule> SupplierPaymentScheduleList { get; set; }

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


}
