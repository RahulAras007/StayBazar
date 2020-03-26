using DataLayer.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataLayer
{
    public class Report : DataLink
    {
        public List<CLayer.OfflineBooking> GetORCReport(DateTime? fromDT = null, DateTime? toDT = null)
        {
            List<CLayer.OfflineBooking> CustomerInvoiceReport = new List<CLayer.OfflineBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pfromDT", DataPlug.DataType._Date, fromDT));
            param.Add(Connection.GetParameter("ptoDT", DataPlug.DataType._Date, toDT));


            DataSet ds = Connection.GetDataSet("Report_ORCReport", param);


            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                CustomerInvoiceReport.Add(new CLayer.OfflineBooking()
                {

                    BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]),
                    BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]),
                    ConfirmationNumber = Connection.ToString(dr["orderno"]),
                    AccommodatoinTypename = Connection.ToString(dr["Title"]),
                    InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    InvoiceDate = Connection.ToDate(dr["InvoiceDate"]),
                    Guestname = Connection.ToString(dr["Guestname"]),
                    CheckIn_date = Connection.ToDate(dr["CheckIn_date"]),
                    CheckOut_date = Connection.ToDate(dr["CheckOut_date"]),
                    Noofnight = Connection.ToInteger(dr["Noofnight"]),
                    NoOfRooms = Connection.ToInteger(dr["NoOfRooms"]),
                    TotalAmtotherForSalePrice = Connection.ToDecimal(dr["TotalAmtotherForSalePrice"]),
                    StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]),
                    AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]),
                    AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]),
                    FirstName = Connection.ToString(dr["Name"]),
                    City = Connection.ToString(dr["cityname"]),
                    //Swatchbharath = Connection.ToDouble(dr["Swatch_bharath"]),
                    //KrishiKalyan = Connection.ToDouble(dr["Krishi_Kalyan"]),
                    //SwatchbharathOthers = Connection.ToDouble(dr["Swatch_bharath_others"]),
                    //KrishiKalyanOthers = Connection.ToDouble(dr["Krishi_Kalyan_others"]),
                    //StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]),
                    NoOfPaxAdult = Connection.ToInteger(dr["NoOfPaxAdult"]),
                    NoOfPaxChild = Connection.ToInteger(dr["NoOfPaxChild"]),
                    Natureofservice = Connection.ToString(dr["Natureofservice"]),
                    Valuebeforeservicetax = Connection.ToInteger(dr["Valuebeforeservicetax"]),
                    Totalamountpayable = Connection.ToInteger(dr["Totalamountpayable"]),
                    SalesPerson = Connection.ToString(dr["SalesPerson"]),

                    CustomerStatename = Connection.ToString(dr["customerstate"]),
                    PropertyStatename = Connection.ToString(dr["propertystate"]),
                    SBEntityName = Connection.ToString(dr["BillingEntity"]),

                    //bSGST = Connection.ToDouble(dr["bSGST"]),
                    //bCGST = Connection.ToDouble(dr["bCGST"]),
                    //bIGST = Connection.ToDouble(dr["bIGST"]),
                    //obSGST = Connection.ToDouble(dr["obSGST"]),
                    //obCGST = Connection.ToDouble(dr["obCGST"]),
                    //obIGST = Connection.ToDouble(dr["obIGST"]),
                    //SGST = Connection.ToDouble(dr["SGST"]),
                    //CGST = Connection.ToDouble(dr["CGST"]),
                    //IGST = Connection.ToDouble(dr["IGST"]),
                    //btSGST = Connection.ToDouble(dr["btSGST"]),
                    //btCGST = Connection.ToDouble(dr["btCGST"]),
                    //btIGST = Connection.ToDouble(dr["btIGST"]),
                    //oSGST = Connection.ToDouble(dr["oSGST"]),
                    //oCGST = Connection.ToDouble(dr["oCGST"]),
                    //oIGST = Connection.ToDouble(dr["oIGST"]),

                    AgentName = Connection.ToString(dr["AgentName"]),
                    AgentAccount =  Connection.ToString(dr["AccountNumber"]),
                    AgentBank = Connection.ToString(dr["BankName"]),
                    AgentBranch = Connection.ToString(dr["BranchName"]),
                    AgentPAN = Connection.ToString(dr["PAN"]),
                    DirectAmount = Connection.ToDouble(dr["DirectAmount"]),
                    BookingTypeInt = Connection.ToInteger(dr["BookingType"]),
                    ReimbursementsAmount = Connection.ToDecimal(dr["Reimbursements"]),
                    DiscountAmount = Connection.ToDecimal(dr["Discount"]),
                    OthersAmount = Connection.ToDecimal(dr["Others"]),

                    VBuyPriceBeforeTax = Connection.ToDouble(dr["VBuyPriceBeforeTax"]),
                    VSalePriceBeforeTax = Connection.ToDouble(dr["VSalePriceBeforeTax"]),
                    //     tmp.VBuyPriceTotal = Connection.ToDouble(dr["VBuyPriceTotal"]);
                    vSalePriceTotal = Connection.ToDouble(dr["vSalePriceTotal"]),
                    //      tmp.vBuyTax = Connection.ToDouble(dr["vBuyTax"]);
                    vSaleGST = Connection.ToDouble(dr["vSaleGST"]),
                    TotalNights = Connection.ToInteger(dr["totalnights"]),
                    ORCAmount = Connection.ToDouble(dr["ORCAmount"])

                });
            }

            return CustomerInvoiceReport;
        }


        public List<CLayer.ReportRoomInventory> ReportRoomInventoryAvailability(string SearchString, int SearchValue, DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.ReportRoomInventory> result = new List<CLayer.ReportRoomInventory>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pSearchValue", DataPlug.DataType._Int, SearchValue));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_RoomInventoryAvailability", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.ReportRoomInventory()
                {
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    AddressOfProperty = Connection.ToString(dr["AddressOfProperty"]),
                    AccomodationType = Connection.ToString(dr["AccomodationType"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    TotalInventoryAllocated = Connection.ToLong(dr["TotalInventoryAllocated"]),
                    InventoryBooked = Connection.ToLong(dr["InventoryBooked"]),
                    PropertyCity = Connection.ToString(dr["PropertyCity"]),
                    PropertyZipCode = Connection.ToString(dr["PropertyZipCode"]),
                    PropertyEmail = Connection.ToString(dr["PropertyEmail"]),
                    State = Connection.ToString(dr["PropertyState"]),
                    //AccomodationDate = Connection.ToDate(dr["AccomodationDate"]),
                    // FromDate = Connection.ToDate(dr["FromDate"]),
                    //ToDate = Connection.ToDate(dr["ToDate"]),                
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }
        public List<CLayer.ReportForSuppliersRegistration> ReportSuppliersRegistration(int SearchValue, int UserType, int Status, DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.ReportForSuppliersRegistration> result = new List<CLayer.ReportForSuppliersRegistration>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus", DataPlug.DataType._Int, Status));
            param.Add(Connection.GetParameter("pUserType", DataPlug.DataType._Int, UserType));
            param.Add(Connection.GetParameter("pSearchValue", DataPlug.DataType._Int, SearchValue));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_SuppliersRegistration", param);
            //`report_SuppliersRegistration`(IN pSearchValue INT,IN pStatus INT,IN pCurrentDate DATE,IN pStart INT, IN pLimit INT)
            CLayer.ReportForSuppliersRegistration crf;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                crf = new CLayer.ReportForSuppliersRegistration()
                 {
                     B2BId = Connection.ToLong(dr["B2BId"]),
                     PropertyId = Connection.ToLong(dr["PropertyId"]),
                     SupplierName = Connection.ToString(dr["SupplierName"]),
                     RegistrationStartDate = Connection.ToDate(dr["RegistrationStartDate"]),
                     ApprovalDate = Connection.ToDate(dr["ApprovalDate"]),
                     City = Connection.ToString(dr["City"]),
                     State = Connection.ToString(dr["State"]),
                     Country = Connection.ToString(dr["Country"]),
                     PropertyName = Connection.ToString(dr["PropertyName"]),
                     Noofproperties = Connection.ToInteger(dr["Noofproperties"]),
                     TotalAccomodationInventory = Connection.ToLong(dr["TotalAccomodationInventory"]),
                     AllocationforStayBazar = Connection.ToLong(dr["accommodationcount"]),
                     CurrentStatus = Connection.ToInteger(dr["CurrentStatus"]),
                     PropertyCity = Connection.ToString(dr["PropertyCity"]),
                     PropertyCountry = Connection.ToString(dr["PropertyCountry"]),
                     PropertyLocation = Connection.ToString(dr["PropertyLocation"]),
                     PropertyState = Connection.ToString(dr["PropertyState"]),
                     TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                 };
                if (crf.CurrentStatus == (int)CLayer.ObjectStatus.StatusType.NotVerified)
                {
                    crf.Noofproperties = Connection.ToInteger(dr["available"]);
                }
                result.Add(crf);
            }
            return result;
        }
        public List<CLayer.ReportForDailyBooking> DailyBooking(DateTime CurrentDate, int Start, int Limit)
        {
            List<CLayer.ReportForDailyBooking> result = new List<CLayer.ReportForDailyBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pCurrentDate", DataPlug.DataType._DateTime, CurrentDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_DailyBooking", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.ReportForDailyBooking()
                {
                    // SerNo = Connection.ToInteger(dr["SerNo"]), 
                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    Destination = Connection.ToString(dr["Destination"]),
                    AccomodationType = Connection.ToString(dr["AccomodationType"]),
                    BookingCategory = Connection.ToString(dr["BookingCategory"]),
                    //RateType = Connection.ToString(dr["RateType"]),
                    Checkindate = Connection.ToDate(dr["Checkindate"]),
                    CheckoutDate = Connection.ToDate(dr["CheckoutDate"]),
                    Noofaccomodations = Connection.ToInteger(dr["Noofaccomodations"]),
                    NoofAdults = Connection.ToInteger(dr["NoofAdults"]),
                    NoofChildren = Connection.ToInteger(dr["NoofChildren"]),
                    TotalbookingAmount = (long)Math.Round(Connection.ToDouble(dr["TotalbookingAmount"])),
                    StayBazarCommission = (long)Math.Round(Connection.ToDouble(dr["StayBazarCommission"])),
                    TravelAgentCommission = (long)Math.Round(Connection.ToDouble(dr["TComm"])),
                    BookingStatus = Connection.ToInteger(dr["BookingStatus"]),
                    //OriginalBookingAmout = Connection.ToInteger(dr["OriginalBookingAmout"]),
                    NewBookingAmount = (long)Math.Round(Connection.ToDouble(dr["newTotal"])),
                    //OriginalCommission = Connection.ToInteger(dr["OriginalCommission"]),
                    NewCommission = (long)Math.Round(Connection.ToDouble(dr["newMarkup"])),
                    RateType = Connection.ToString(dr["Ratetype"]),
                    TotalbookingAmountcancelled = (long)Math.Round(Connection.ToDouble(dr["TotalbookingAmountcancelled"])),
                    //StayBazarCommissioncancelled = Connection.ToInteger(dr["StayBazarCommissioncancelled"]),
                    Chargesforchange = (long)Math.Round(Connection.ToDouble(dr["TotalServiceCharge"])),
                    CurrentStatus = Connection.ToInteger(dr["CurrentStatus"]),
                    BookingTime = Connection.ToDate(dr["BookingTime"]),
                    // Issue = Connection.ToString(dr["Issue"]),
                    //ResolutionTime = Connection.ToDate(dr["ResolutionTime"]),
                    //Resolution = Connection.ToInteger(dr["CurrentStatus"]),
                    ResolvedBy = Connection.ToInteger(dr["ResolvedBy"]),
                    NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                    TotalRefund = Connection.ToDouble(dr["TotalRefund"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }
        public List<CLayer.ReportDailyInventoryAndBooking> DailyInventoryAndBooking(long supplierId, DateTime CurrentDate, int Start, int Limit)
        {
            List<CLayer.ReportDailyInventoryAndBooking> result = new List<CLayer.ReportDailyInventoryAndBooking>();

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, supplierId));
            param.Add(Connection.GetParameter("pCurrentDate", DataPlug.DataType._DateTime, CurrentDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_InventoryAndBooking", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.ReportDailyInventoryAndBooking()
                {
                    SupplierId = Connection.ToLong(dr["SupplierId"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyLocation = Connection.ToString(dr["PropertyLocation"]),
                    PropertyCity = Connection.ToString(dr["PropertyCity"]),
                    AccomodationType = Connection.ToString(dr["AccomodationType"]),
                    BookingCategory = Connection.ToString(dr["BookingCategory"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    AccomodationId = Connection.ToLong(dr["AccommodationId"]),
                    NoofaccomodationsBooked = Connection.ToLong(dr["NoofAccomodationsBooked"]),
                    NoofAccomodationsCancelled = Connection.ToLong(dr["noofacccancelled"]),
                    InventoryAllocatedtoStayBazar = Connection.ToLong(dr["accommodationcount"]),
                    InventoryBalance = Connection.ToLong(dr["invavailable"]),
                    Pincode = Connection.ToString(dr["PropertyZipCode"]),
                    Country = Connection.ToString(dr["countryname"]),
                    State = Connection.ToString(dr["Statename"])

                    // TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }
        public DataTable PropertyDetailsReport(string SearchString, int Searchvalue)
        {
            List<CLayer.PropertyDetailsReport> result = new List<CLayer.PropertyDetailsReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pSearchvalue", DataPlug.DataType._Int, Searchvalue));

            DataSet ds = Connection.GetDataSet("report_PropertyDetails", param);
            DataTable PropertyDetails = ds.Tables[0];
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                result.Add(new CLayer.PropertyDetailsReport()
                {
                    SupplierBusinessName = Connection.ToString(dr["Supplier_Business_Name"]),
                    LoginID = Connection.ToString(dr["Login_ID"]),
                    SupplierEmailID = Connection.ToString(dr["Supplier_Email_ID"]),
                    SupplierContactName = Connection.ToString(dr["Supplier_Contact_Name"]),
                    SupplierAddress = Connection.ToString(dr["Supplier_Address"]),
                    SupplierPhone = Connection.ToString(dr["Supplier_Phone"]),
                    SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    SupplierCity = Connection.ToString(dr["Supplier_City"]),
                    SupplierState = Connection.ToString(dr["Supplier_State"]),
                    PropertyName = Connection.ToString(dr["Property_Name"]),
                    PropertyAddress = Connection.ToString(dr["Property_Address"]),
                    B2CMarkupShortTerm = Connection.ToDecimal(dr["B2CMarkupShortTerm"]),
                    B2CMarkupLongTerm = Connection.ToDecimal(dr["B2CMarkupLongTerm"]),
                    B2BMarkupShortTerm = Connection.ToDecimal(dr["B2BMarkupShortTerm"]),
                    B2BMarkupLongTerm = Connection.ToDecimal(dr["B2BMarkupLongTerm"]),
                    PropertyCity = Connection.ToString(dr["Property_City"]),
                    PropertyState = Connection.ToString(dr["Property_State"]),
                    JoinedDate = Connection.ToDate(dr["Joined_Date"]),
                    Accommodation_Description = Connection.ToString(dr["Accommodation_Description"]),
                    AccommodationType = Connection.ToString(dr["AccommodationType"]),
                    Quantity = Connection.ToString(dr["Quantity"]),
                    DailyRate = Connection.ToDecimal(dr["DailyRate"]),
                    WeeklyRate = Connection.ToDecimal(dr["WeeklyRate"]),
                    MonthlyRate = Connection.ToDecimal(dr["MonthlyRate"]),
                    LongTermRate = Connection.ToDecimal(dr["LongTermRate"]),
                    GuestRate = Connection.ToDecimal(dr["GuestRate"]),
                    Bedrooms = Connection.ToLong(dr["Bedrooms"]),
                    SupplierTotalAccommodations = Connection.ToString(dr["SupplierTotalAccommodations"]),
                    Accommodation_MaxPeople = Connection.ToLong(dr["Accommodation_MaxPeople"]),
                    Adults = Connection.ToLong(dr["Adults"]),
                    Children = Connection.ToLong(dr["Children"])
                });
            }
            return PropertyDetails;
        }


        public DataSet PropertyDetailsReport_Pager(string SearchString, int Searchvalue, int Limit, int Start)
        {
            List<CLayer.PropertyDetailsReport> result = new List<CLayer.PropertyDetailsReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();

            param.Add(Connection.GetParameter("pSearchString", DataPlug.DataType._Varchar, SearchString));
            param.Add(Connection.GetParameter("pSearchvalue", DataPlug.DataType._Int, Searchvalue));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));

            DataSet ds = Connection.GetDataSet("report_PropertyDetails_Report", param);
            //DataTable PropertyDetails = ds.Tables[1];
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.PropertyDetailsReport()
                {
                    SupplierBusinessName = Connection.ToString(dr["Supplier_Business_Name"]),
                    LoginID = Connection.ToString(dr["Login_ID"]),
                    SupplierEmailID = Connection.ToString(dr["Supplier_Email_ID"]),
                    SupplierContactName = Connection.ToString(dr["Supplier_Contact_Name"]),
                    SupplierAddress = Connection.ToString(dr["Supplier_Address"]),
                    SupplierPhone = Connection.ToString(dr["Supplier_Phone"]),
                    SupplierMobile = Connection.ToString(dr["SupplierMobile"]),
                    SupplierCity = Connection.ToString(dr["Supplier_City"]),
                    SupplierState = Connection.ToString(dr["Supplier_State"]),
                    PropertyName = Connection.ToString(dr["Property_Name"]),
                    PropertyAddress = Connection.ToString(dr["Property_Address"]),
                    B2CMarkupShortTerm = Connection.ToDecimal(dr["B2CMarkupShortTerm"]),
                    B2CMarkupLongTerm = Connection.ToDecimal(dr["B2CMarkupLongTerm"]),
                    B2BMarkupShortTerm = Connection.ToDecimal(dr["B2BMarkupShortTerm"]),
                    B2BMarkupLongTerm = Connection.ToDecimal(dr["B2BMarkupLongTerm"]),
                    PropertyCity = Connection.ToString(dr["Property_City"]),
                    PropertyState = Connection.ToString(dr["Property_State"]),
                    JoinedDate = Connection.ToDate(dr["Joined_Date"]),
                    Accommodation_Description = Connection.ToString(dr["Accommodation_Description"]),
                    AccommodationType = Connection.ToString(dr["AccommodationType"]),
                    Quantity = Connection.ToString(dr["Quantity"]),
                    DailyRate = Connection.ToDecimal(dr["DailyRate"]),
                    WeeklyRate = Connection.ToDecimal(dr["WeeklyRate"]),
                    MonthlyRate = Connection.ToDecimal(dr["MonthlyRate"]),
                    LongTermRate = Connection.ToDecimal(dr["LongTermRate"]),
                    GuestRate = Connection.ToDecimal(dr["GuestRate"]),
                    Bedrooms = Connection.ToLong(dr["Bedrooms"]),
                    SupplierTotalAccommodations = Connection.ToString(dr["SupplierTotalAccommodations"]),
                    Accommodation_MaxPeople = Connection.ToLong(dr["Accommodation_MaxPeople"]),
                    Adults = Connection.ToLong(dr["Adults"]),
                    Children = Connection.ToLong(dr["Children"])
                });
            }
            return ds;
        }


        public DataTable PropertyTaxReport()
        {
            DataSet ds = Connection.GetDataSet("report_PropertyTax");
            DataTable PropertyTax = ds.Tables[0];
            return PropertyTax;
        }

        public DataTable ReportCityWiseCount()
        {
            DataSet ds = Connection.GetDataSet("Report_CitywiseCount");
            DataTable CityRateWiseCount = ds.Tables[0];
            return CityRateWiseCount;
        }
        public List<CLayer.ReportDailyPropertyBooking> DailyPropertyBooking(long supplierId, string properties, DateTime fromDate, DateTime toDate)
        {
            List<CLayer.ReportDailyPropertyBooking> result = new List<CLayer.ReportDailyPropertyBooking>();

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, supplierId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._Varchar, properties));
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, fromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, toDate));
            DataTable ds = Connection.GetTable("report_DailyPropertyBooking", param);
            foreach (DataRow dr in ds.Rows)
            {
                result.Add(new CLayer.ReportDailyPropertyBooking()
                {
                    SupplierId = Connection.ToLong(dr["SupplierId"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyLocation = Connection.ToString(dr["PropertyLocation"]),
                    PropertyCity = Connection.ToString(dr["PropertyCity"]),
                    AccomodationType = Connection.ToString(dr["AccomodationType"]),
                    BookingCategory = Connection.ToString(dr["BookingCategory"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    AccomodationId = Connection.ToLong(dr["AccommodationId"]),
                    NoofaccomodationsBooked = Connection.ToLong(dr["NoofAccomodationsBooked"]),
                    NoofAccomodationsCancelled = Connection.ToLong(dr["noofacccancelled"]),
                    InventoryAllocatedtoStayBazar = Connection.ToLong(dr["accommodationcount"]),
                    InventoryBalance = Connection.ToLong(dr["invavailable"]),
                    Pincode = Connection.ToString(dr["PropertyZipCode"]),
                    Country = Connection.ToString(dr["countryname"]),
                    State = Connection.ToString(dr["Statename"])

                    // TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }
        public List<CLayer.ReportDailyPropertyBooking> DailyPropertyBookingForEmail(long supplierId, long propertyId, DateTime fromDate, DateTime toDate)
        {
            List<CLayer.ReportDailyPropertyBooking> result = new List<CLayer.ReportDailyPropertyBooking>();

            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, supplierId));
            param.Add(Connection.GetParameter("pPropertyId", DataPlug.DataType._BigInt, propertyId));
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, fromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, toDate));
            DataTable ds = Connection.GetTable("report_DailyPropertyBookingForEmail", param);
            foreach (DataRow dr in ds.Rows)
            {
                result.Add(new CLayer.ReportDailyPropertyBooking()
                {
                    SupplierId = Connection.ToLong(dr["SupplierId"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyLocation = Connection.ToString(dr["PropertyLocation"]),
                    PropertyCity = Connection.ToString(dr["PropertyCity"]),
                    AccomodationType = Connection.ToString(dr["AccomodationType"]),
                    BookingCategory = Connection.ToString(dr["BookingCategory"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    AccomodationId = Connection.ToLong(dr["AccommodationId"]),
                    NoofaccomodationsBooked = Connection.ToLong(dr["NoofAccomodationsBooked"]),
                    NoofAccomodationsCancelled = Connection.ToLong(dr["noofacccancelled"]),
                    InventoryAllocatedtoStayBazar = Connection.ToLong(dr["accommodationcount"]),
                    InventoryBalance = Connection.ToLong(dr["invavailable"]),
                    Pincode = Connection.ToString(dr["PropertyZipCode"]),
                    Country = Connection.ToString(dr["countryname"]),
                    State = Connection.ToString(dr["Statename"])

                    // TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }

        public List<CLayer.ReportDailyPropertyBooking> DailyPropertyBookingcartForEmail(DateTime fromDate)
        {
            List<CLayer.ReportDailyPropertyBooking> result = new List<CLayer.ReportDailyPropertyBooking>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, fromDate));
            DataTable ds = Connection.GetTable("DailyPropertyBookingcartForEmail", param);
            foreach (DataRow dr in ds.Rows)
            {
                result.Add(new CLayer.ReportDailyPropertyBooking()
                {
                    SupplierId = Connection.ToLong(dr["SupplierId"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyLocation = Connection.ToString(dr["PropertyLocation"]),
                    PropertyCity = Connection.ToString(dr["PropertyCity"]),
                    AccomodationType = Connection.ToString(dr["AccomodationType"]),
                    BookingCategory = Connection.ToString(dr["BookingCategory"]),
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    AccomodationId = Connection.ToLong(dr["AccommodationId"]),
                    NoofaccomodationsBooked = Connection.ToLong(dr["NoofAccomodationsBookedcart"]),
                    InventoryAllocatedtoStayBazar = Connection.ToLong(dr["accommodationcount"]),
                    InventoryBalance = Connection.ToLong(dr["invavailable"]),
                    Pincode = Connection.ToString(dr["PropertyZipCode"]),
                    Country = Connection.ToString(dr["countryname"]),
                    State = Connection.ToString(dr["Statename"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerEmail = Connection.ToString(dr["CustomerEmail"]),
                    Checkindate = Connection.ToDate(dr["Checkindate"]),
                    CheckoutDate = Connection.ToDate(dr["CheckoutDate"]),
                    TotalbookingAmount = Connection.ToString(dr["TotalbookingAmount"]),
                    RateType = Connection.ToString(dr["RateType"]),
                    CustomerPhone = Connection.ToString(dr["CustomerPhone"])
                    // TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }



        public List<CLayer.CorporateDiscounts> ReportCorporateDiscounts(long supplierid)
        {
            List<CLayer.CorporateDiscounts> result = new List<CLayer.CorporateDiscounts>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pSupplierId", DataPlug.DataType._BigInt, supplierid));
            DataTable ds = Connection.GetTable("report_CorporateDiscounts", param);
            foreach (DataRow dr in ds.Rows)
            {
                result.Add(new CLayer.CorporateDiscounts()
                {
                    SupplierId = supplierid,
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    PropertyLoaction = Connection.ToString(dr["PropertyLoaction"]),
                    PropertyCity = Connection.ToString(dr["PropertyCity"]),
                    AddLongTerm = Connection.ToDecimal(dr["AddLongTerm"]),
                    AddShortTerm = Connection.ToDecimal(dr["AddShortTerm"]),
                    B2BMarkupLongTerm = Connection.ToDecimal(dr["B2BMarkupLongTerm"]),
                    B2BMarkupShortTerm = Connection.ToDecimal(dr["B2BMarkupShortTerm"]),
                    B2bStdLongDiscounts = Connection.ToDecimal(dr["B2bStdLongDiscounts"]),
                    B2bStdShortDiscounts = Connection.ToDecimal(dr["B2bStdShortDiscounts"]),
                    CorporateId = Connection.ToLong(dr["CorporateId"]),
                    Corporatename = Connection.ToString(dr["Corporatename"]),
                    PropertyId = Connection.ToLong(dr["PropertyId"]),
                    Usercode = Connection.ToString(dr["Usercode"]),
                    // TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }

        public List<CLayer.ReportForFailedTransactions> FailedTransation(int Status1, int Status2, DateTime CurrentDate, int Start, int Limit)
        {
            List<CLayer.ReportForFailedTransactions> result = new List<CLayer.ReportForFailedTransactions>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pStatus1", DataPlug.DataType._Int, Status1));
            param.Add(Connection.GetParameter("pStatus2", DataPlug.DataType._Int, Status2));
            param.Add(Connection.GetParameter("pCurrentDate", DataPlug.DataType._DateTime, CurrentDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_BookingwithFaildTransaction", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.ReportForFailedTransactions()
                {
                    BookingId = Connection.ToLong(dr["BookingId"]),
                    SupplierName = Connection.ToString(dr["SupplierName"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    BookingDateTime = Connection.ToDate(dr["BookingDateTime"]),
                    ResolvedBy = Connection.ToString(dr["ResolvedBy"]),
                    //ResolutionTime = Connection.ToDate(dr["ResolutionTime"]),
                    StayBazarCommission = (long)Math.Round(Connection.ToDouble(dr["Commission"])),
                    Checkindate = Connection.ToDate(dr["Checkindate"]),
                    CheckoutDate = Connection.ToDate(dr["CheckoutDate"]),
                    Noofaccomodations = (long)Math.Round(Connection.ToDouble(dr["Noofaccomodations"])),
                    NoofAdults = Connection.ToLong(dr["NoofAdults"]),
                    NoofChildren = Connection.ToLong(dr["NoofChildren"]),
                    TotalbookingAmount = (long)Math.Round(Connection.ToDouble(dr["TotalbookingAmount"])),
                    CurrentStatus = Connection.ToInteger(dr["CurrentStatus"]),
                    // //Destination = Connection.ToString(dr["Destination"]),
                    AccomodationType = Connection.ToString(dr["AccomodationType"]),
                    BookingCategory = Connection.ToString(dr["BookingCategory"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    Ratetype = Connection.ToString(dr["Ratetype"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    PropertyCity = Connection.ToString(dr["PropertyCity"]),
                    CustomerMobile = Connection.ToString(dr["CustomerMobile"]),
                });
            }
            return result;

        }
        public List<CLayer.SupplierPaymentReport> SupplierPaymentReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.SupplierPaymentReport> result = new List<CLayer.SupplierPaymentReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_SupplierPayment", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.SupplierPaymentReport()
                 {
                     BookingDate = Connection.ToDate(dr["BookingDate"]),
                     BookingId = Connection.ToString(dr["OrderNo"]),
                     PropertyName = Connection.ToString(dr["PropertyName"]),
                     City = Connection.ToString(dr["PropertyCity"]),
                     State = Connection.ToString(dr["PropertyState"]),
                     AccomodationType = Connection.ToString(dr["AccomodationType"]),
                     NumberofAccomodation = Connection.ToLong(dr["NumberofAccomodation"]),
                     CheckIn = Connection.ToDate(dr["CheckIn"]),
                     CheckOut = Connection.ToDate(dr["CheckOut"]),
                     NumberofNights = Connection.ToLong(dr["NumberofNights"]),
                     TaxType = Connection.ToString(dr["TaxType"]),
                     TaxRate = Connection.ToDecimal(dr["TaxRate"]),
                     CustomeType = Connection.ToString(dr["CustomeType"]),
                     CustomerName = Connection.ToString(dr["CustomerName"]),
                     SupplierRate = Math.Round(Connection.ToDecimal(dr["supratepre"])),
                     SupplierRatePayable = Math.Round(Connection.ToDecimal(dr["suprate"])),
                     TotalSupplierValue = Math.Round(Connection.ToDecimal(dr["totalsupval"])),
                     NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]),
                     //LuxuryTax = Connection.ToLong(dr["LuxuryTax"]),
                     //ServiceTax = Connection.ToLong(dr["ServiceTax"]),
                     //TotalSupplierValue = Connection.ToLong(dr["TotalSupplierValue"]),
                     AmountPaid = Math.Round(Connection.ToDecimal(dr["TotalAmount"])),
                     Modeofpayment = Connection.ToString(dr["Modeofpayment"]),
                     TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                 });
            }
            return result;
        }
        public List<CLayer.GrossMarrginReport> GrossMarrginReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.GrossMarrginReport> result = new List<CLayer.GrossMarrginReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_GrossMarrgin", param);
            CLayer.GrossMarrginReport tmp;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                tmp = new CLayer.GrossMarrginReport();

                tmp.BookingDate = Connection.ToDate(dr["BookingDate"]);
                tmp.orderno = Connection.ToString(dr["BookingId"]);
                tmp.PropertyName = Connection.ToString(dr["PropertyName"]);
                tmp.City = Connection.ToString(dr["PropertyCity"]);
                tmp.State = Connection.ToString(dr["PropertyState"]);
                tmp.AccomodationType = Connection.ToString(dr["AccomodationType"]);
                tmp.NumberofAccomodation = Connection.ToLong(dr["NumberofAccomodation"]);
                tmp.CheckIn = Connection.ToDate(dr["CheckIn"]);
                tmp.CheckOut = Connection.ToDate(dr["CheckOut"]);
                tmp.NumberofNights = Connection.ToLong(dr["NumberofNights"]);
                tmp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                tmp.TaxType = Connection.ToString(dr["TaxType"]);
                tmp.TaxRate = Connection.ToDecimal(dr["TotalTax"]);
                tmp.CustomeType = Connection.ToString(dr["CustomeType"]);
                tmp.CustomerName = Connection.ToString(dr["CustomerName"]);
                tmp.SupplierRate = (long)Math.Round(Connection.ToDouble(dr["SupplierPreTax"]));
                tmp.TotalSupplierBuyCost = (long)Math.Round(Connection.ToDouble(dr["SupplierBuyCost"]));
                tmp.BookingRate = (long)Math.Round(Connection.ToDouble(dr["BookingRate"]));
                tmp.BookingValue = (long)Math.Round(Connection.ToDouble(dr["BookValuePre"]));
                tmp.TotalBookingValue = (long)Math.Round(Connection.ToDouble(dr["TotalBookValue"]));
                tmp.GrossMargin = (long)Math.Round(Connection.ToDouble(dr["grossmargin"]));
                tmp.NetMargin = (long)Math.Round(Connection.ToDouble(dr["netmargin"]));
                tmp.NetMarginPerc = (long)Math.Round(Connection.ToDouble(dr["netpercmargin"]), 2);
                //LuxuryTax = Connection.ToLong(dr["LuxuryTax"]);
                //ServiceTax = Connection.ToLong(dr["ServiceTax"]);
                //tmp.TotalSupplierValue = Connection.ToLong(dr["TotalSupplierValue"]);
                //AmountPaid = Connection.ToLong(dr["AmountPaid"]);
                //Modeofpayment = Connection.ToString(dr["Modeofpayment"]);                  
                tmp.TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]);
                tmp.AgentCommissionPayable = (long)Math.Round(Connection.ToDouble(dr["travcomm"]), 2);
                result.Add(tmp);
            }
            return result;
        }
        public List<CLayer.MargintrackingReport> MargintrackingReport(DateTime FromDate, DateTime ToDate, int Start, int Limit,long LoginUserid)
        {
            List<CLayer.MargintrackingReport> result = new List<CLayer.MargintrackingReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pLoginUserid", DataPlug.DataType._Int, LoginUserid)); 
            DataSet ds = Connection.GetDataSet("report_Margintracking", param);
            CLayer.MargintrackingReport tmp;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                tmp = new CLayer.MargintrackingReport();
                tmp.Offline_BookingId = Connection.ToLong(dr["Offline_BookingId"]);
                tmp.BookingType = Connection.ToString(dr["Booking_Category"]);
                tmp.BookingTypeid = Connection.ToLong(dr["BookingType"]);
                tmp.BillingEntity = Connection.ToString(dr["BillingEntity"]);
                tmp.BookingDate = Connection.ToDate(dr["BookingDate"]);
                tmp.orderno = Connection.ToString(dr["orderno"]);
                tmp.PropertyName = Connection.ToString(dr["PropertyName"]);
                tmp.PropertyCity = Connection.ToString(dr["PropertyCity"]);
                tmp.salepersonaname = Connection.ToString(dr["salepersonaname"]);
                tmp.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
                tmp.CustomerValueBeforeGST = Connection.ToDecimal(dr["CustomerValueBeforeGST"]);
                tmp.SupplierValueBeforeGST = Connection.ToDecimal(dr["SupplierValueBeforeGST"]);

                
                tmp.NumberofRoom = Connection.ToLong(dr["NoOfRooms"]);
                tmp.NumberofNights = Connection.ToLong(dr["Noofnight"]);
                tmp.SupplierName = Connection.ToString(dr["supplier_name"]);
                //   tmp.State = Connection.ToString(dr["PropertyState"]);
                //  tmp.AccomodationType = Connection.ToString(dr["AccomodationType"]);
                //    tmp.NumberofAccomodation = Connection.ToLong(dr["NumberofAccomodation"]);
                tmp.CheckIn = Connection.ToDate(dr["CheckIn"]);
                tmp.CheckOut = Connection.ToDate(dr["CheckOut"]);
                //  tmp.NumberofNights = Connection.ToLong(dr["NumberofNights"]);
                //    tmp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                //  tmp.TaxType = Connection.ToString(dr["TaxType"]);
                //     tmp.TaxRate = Connection.ToDecimal(dr["TotalTax"]);
                //  tmp.CustomeType = Connection.ToString(dr["CustomeType"]);
                tmp.CustomerName = Connection.ToString(dr["CustomerName"]);
                tmp.City = Connection.ToString(dr["City"]);
                //     tmp.SupplierRate = (long)Math.Round(Connection.ToDouble(dr["SupplierPreTax"]));
                //   tmp.TotalSupplierBuyCost = (long)Math.Round(Connection.ToDouble(dr["SupplierBuyCost"]));
                //    tmp.BookingRate = (long)Math.Round(Connection.ToDouble(dr["BookingRate"]));
                //  tmp.BookingValue = (long)Math.Round(Connection.ToDouble(dr["BookValuePre"]));
                //   tmp.TotalBookingValue = (long)Math.Round(Connection.ToDouble(dr["TotalBookValue"]));
                //  tmp.GrossMargin = (long)Math.Round(Connection.ToDouble(dr["grossmargin"]));
                //    tmp.NetMargin = (long)Math.Round(Connection.ToDouble(dr["netmargin"]));
                //   tmp.NetMarginPerc = (long)Math.Round(Connection.ToDouble(dr["netpercmargin"]), 2);
                //LuxuryTax = Connection.ToLong(dr["LuxuryTax"]);
                //ServiceTax = Connection.ToLong(dr["ServiceTax"]);
                tmp.SupplierRate = Connection.ToDecimal(dr["TotalBuyPrice"]);
                //   tmp.SupplierName = Connection.ToString(dr["SupplierName"]);
                tmp.GuestName = Connection.ToString(dr["GuestName"]);
                tmp.CustomerBillvalue = Connection.ToDecimal(dr["TotalSalePrice"]);
                //AmountPaid = Connection.ToLong(dr["AmountPaid"]);
                //Modeofpayment = Connection.ToString(dr["Modeofpayment"]);                  
                tmp.TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]);
                //   tmp.AgentCommissionPayable = (long)Math.Round(Connection.ToDouble(dr["travcomm"]), 2);
                tmp.ManagementFee = Connection.ToDecimal(dr["ManagementFee"]);
                tmp.TacAmount = Connection.ToDecimal(dr["TacAmount"]);
                tmp.SaleAmountTac = Connection.ToDecimal(dr["SaleAmountTac"]);
                tmp.SaleAmountDirect = Connection.ToDecimal(dr["SaleAmountDirect"]);
                tmp.TAConlyAmount = Connection.ToDecimal(dr["TAConlyAmount"]);
                tmp.DirectOnlyAmount = Connection.ToDecimal(dr["DirectOnlyAmount"]);
                tmp.offlinestatus = Connection.ToLong(dr["offlinestatus"]);
                tmp.ORCAmount = Connection.ToDecimal(dr["ORCAmount"]);
                tmp.TotalNights = Connection.ToInteger(dr["totalnights"]);
                //Added by Rahul on 28-01-2020
                tmp.CustomerReferenceNo = Connection.ToString(dr["CustomerReferenceNo"]);
                tmp.HotelConformationNo = Connection.ToString(dr["HotelConformationNo"]);
                //Added by Rahul on 04-02-2020
                tmp.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
                result.Add(tmp);
            }
            return result;
        }
        public List<CLayer.PendingCustomerInvoiceReport> PendingCustomerInvoiceReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.PendingCustomerInvoiceReport> result = new List<CLayer.PendingCustomerInvoiceReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_PendingCustomerInvoice", param);
            CLayer.PendingCustomerInvoiceReport tmp;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                tmp = new CLayer.PendingCustomerInvoiceReport();

                tmp.BookingDate = Connection.ToDate(dr["BookingDate"]);
                tmp.orderno = Connection.ToString(dr["orderno"]);
                tmp.PropertyName = Connection.ToString(dr["PropertyName"]);
                tmp.City = Connection.ToString(dr["PropertyCity"]);
                tmp.CustomerName = Connection.ToString(dr["CustomerName"]);
                tmp.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
                tmp.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
                tmp.NoOfRooms = Connection.ToInteger(dr["NumberofAccomodation"]);
                tmp.NumberofNights = Connection.ToInteger(dr["Noofnight"]);
                tmp.TotalBookingValue = Connection.ToDecimal(dr["TotalSalePrice"]);
                tmp.guestname = Connection.ToString(dr["Guestname"]);
                //   tmp.State = Connection.ToString(dr["PropertyState"]);
                //  tmp.AccomodationType = Connection.ToString(dr["AccomodationType"]);
                //    tmp.NumberofAccomodation = Connection.ToLong(dr["NumberofAccomodation"]);
                tmp.CheckIn = Connection.ToDate(dr["CheckIn"]);
                tmp.CheckOut = Connection.ToDate(dr["CheckOut"]);
                //  tmp.NumberofNights = Connection.ToLong(dr["NumberofNights"]);
                //    tmp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                //  tmp.TaxType = Connection.ToString(dr["TaxType"]);
                //     tmp.TaxRate = Connection.ToDecimal(dr["TotalTax"]);
                //  tmp.CustomeType = Connection.ToString(dr["CustomeType"]);
                //    tmp.CustomerName = Connection.ToString(dr["CustomerName"]);
                //     tmp.SupplierRate = (long)Math.Round(Connection.ToDouble(dr["SupplierPreTax"]));
                //   tmp.TotalSupplierBuyCost = (long)Math.Round(Connection.ToDouble(dr["SupplierBuyCost"]));
                //    tmp.BookingRate = (long)Math.Round(Connection.ToDouble(dr["BookingRate"]));
                //  tmp.BookingValue = (long)Math.Round(Connection.ToDouble(dr["BookValuePre"]));
                //   tmp.TotalBookingValue = (long)Math.Round(Connection.ToDouble(dr["TotalBookValue"]));
                //  tmp.GrossMargin = (long)Math.Round(Connection.ToDouble(dr["grossmargin"]));
                //    tmp.NetMargin = (long)Math.Round(Connection.ToDouble(dr["netmargin"]));
                //   tmp.NetMarginPerc = (long)Math.Round(Connection.ToDouble(dr["netpercmargin"]), 2);
                //LuxuryTax = Connection.ToLong(dr["LuxuryTax"]);
                //ServiceTax = Connection.ToLong(dr["ServiceTax"]);
                tmp.SupplierRate = Connection.ToDecimal(dr["TotalBuyPrice"]);
                tmp.SupplierName = Connection.ToString(dr["SupplierName"]);

                tmp.CustomerBillvalue = Connection.ToDecimal(dr["TotalSalePrice"]);
                //AmountPaid = Connection.ToLong(dr["AmountPaid"]);
                //Modeofpayment = Connection.ToString(dr["Modeofpayment"]);                  
                tmp.TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]);
                //   tmp.AgentCommissionPayable = (long)Math.Round(Connection.ToDouble(dr["travcomm"]), 2);
                result.Add(tmp);
            }
            return result;
        }
        public List<CLayer.BookingStatusReport> BookingStatusReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.BookingStatusReport> result = new List<CLayer.BookingStatusReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_BookingStatus", param);
            CLayer.BookingStatusReport tmp;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                tmp = new CLayer.BookingStatusReport();

                tmp.BookingDate = Connection.ToDate(dr["BookingDate"]);
                tmp.orderno = Connection.ToString(dr["orderno"]);
                tmp.PropertyName = Connection.ToString(dr["PropertyName"]);
                tmp.City = Connection.ToString(dr["PropertyCity"]);
                tmp.CustomerName = Connection.ToString(dr["CustomerName"]);
                tmp.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
                tmp.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
                tmp.AccomodationType = Connection.ToString(dr["AccommodationtypeName"]);

                tmp.GuestName = Connection.ToString(dr["Guestname"]);
                tmp.SalesPerson = Connection.ToString(dr["SalesPerson"]);
                tmp.SalesRegion = Connection.ToString(dr["SalesRegion"]);

                tmp.NoOfPaxAdult = Connection.ToInteger(dr["NoOfPaxAdult"]);
                tmp.NoOfPaxChild = Connection.ToInteger(dr["NoOfPaxChild"]);
                tmp.AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]);
                tmp.AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]);
                tmp.StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]);
                tmp.StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]);
                tmp.TotalBuyPrice = (long)Math.Round(Connection.ToDouble(dr["TotalBuyPrice"]), 2);
                tmp.TotalSalePrice = (long)Math.Round(Connection.ToDouble(dr["TotalSalePrice"]), 2);

                tmp.StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]);
                tmp.StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]);
                tmp.BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]);
                tmp.BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]);

                tmp.amount = (long)Math.Round(Connection.ToDouble(dr["amount"]), 2);

                tmp.NumberofNights = Connection.ToInteger(dr["Noofnight"]);
                tmp.NumberofRooms = Connection.ToInteger(dr["NoOfRooms"]);

                //   tmp.State = Connection.ToString(dr["PropertyState"]);
                //  tmp.AccomodationType = Connection.ToString(dr["AccomodationType"]);
                //    tmp.NumberofAccomodation = Connection.ToLong(dr["NumberofAccomodation"]);
                tmp.CheckIn = Connection.ToDate(dr["CheckIn"]);
                tmp.CheckOut = Connection.ToDate(dr["CheckOut"]);
                //  tmp.NumberofNights = Connection.ToLong(dr["NumberofNights"]);
                //    tmp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                //  tmp.TaxType = Connection.ToString(dr["TaxType"]);
                //     tmp.TaxRate = Connection.ToDecimal(dr["TotalTax"]);
                //  tmp.CustomeType = Connection.ToString(dr["CustomeType"]);
                tmp.CustomerName = Connection.ToString(dr["CustomerName"]);
                tmp.Maildate = Connection.ToDate(dr["Maileddate"]);
                tmp.SupplierInvoiceDate = Connection.ToDate(dr["supplierInvoiceDate"]);

                //     tmp.SupplierRate = (long)Math.Round(Connection.ToDouble(dr["SupplierPreTax"]));
                //   tmp.TotalSupplierBuyCost = (long)Math.Round(Connection.ToDouble(dr["SupplierBuyCost"]));
                //    tmp.BookingRate = (long)Math.Round(Connection.ToDouble(dr["BookingRate"]));
                //  tmp.BookingValue = (long)Math.Round(Connection.ToDouble(dr["BookValuePre"]));
                //   tmp.TotalBookingValue = (long)Math.Round(Connection.ToDouble(dr["TotalBookValue"]));
                //  tmp.GrossMargin = (long)Math.Round(Connection.ToDouble(dr["grossmargin"]));
                //    tmp.NetMargin = (long)Math.Round(Connection.ToDouble(dr["netmargin"]));
                //   tmp.NetMarginPerc = (long)Math.Round(Connection.ToDouble(dr["netpercmargin"]), 2);
                //LuxuryTax = Connection.ToLong(dr["LuxuryTax"]);
                //ServiceTax = Connection.ToLong(dr["ServiceTax"]);
                tmp.SupplierRate = Connection.ToDecimal(dr["TotalBuyPrice"]);
                tmp.SupplierName = Connection.ToString(dr["SupplierName"]);

                tmp.CustomerBillvalue = Connection.ToDecimal(dr["TotalSalePrice"]);
                //AmountPaid = Connection.ToLong(dr["AmountPaid"]);
                //Modeofpayment = Connection.ToString(dr["Modeofpayment"]);                  
                tmp.TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]);
                //   tmp.AgentCommissionPayable = (long)Math.Round(Connection.ToDouble(dr["travcomm"]), 2);
                result.Add(tmp);
            }
            return result;
        }
        public List<CLayer.CollectionReport> CollectionReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.CollectionReport> result = new List<CLayer.CollectionReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_Collection", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.CollectionReport()
                {
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingId = Connection.ToString(dr["BookingId"]),
                    BookingRefNo = Connection.ToString(dr["BookingRefNo"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    City = Connection.ToString(dr["PropertyCity"]),
                    State = Connection.ToString(dr["PropertyState"]),
                    AccomodationType = Connection.ToString(dr["AccomodationType"]),
                    NumberofAccomodation = Connection.ToLong(dr["NumberofAccomodation"]),
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    NumberofNights = Connection.ToInteger(dr["NumberofNights"]),
                    CustomeType = Connection.ToString(dr["CustomeType"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CustomerPaymenMode = Connection.ToString(dr["paymenttype"]),
                    //SupplierRate = Connection.ToLong(dr["SupplierRate"]),
                    //LuxuryTax = Connection.ToLong(dr["LuxuryTax"]),
                    //ServiceTax = Connection.ToLong(dr["ServiceTax"]),
                    TotalSupplierValue = Math.Round(Connection.ToDecimal(dr["TotalSupplierValue"])),
                    Paymentgatewaycommission = Math.Round(Connection.ToDouble(dr["payperc"]), 2),
                    Paymentgatewaycharges = (long)Math.Round(Connection.ToDouble(dr["gatewaycharge"]), 2),
                    NetCreditReceivableinbankaccount = (long)Math.Round(Connection.ToDouble(dr["bankamnt"]), 2),
                    //AmountPaid = Connection.ToLong(dr["AmountPaid"]),
                    //Modeofpayment = Connection.ToString(dr["Modeofpayment"]),                  
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"])
                });
            }
            return result;
        }


        public List<CLayer.CreditBookingReport> CorporateCreditBookingsReport(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.CreditBookingReport> result = new List<CLayer.CreditBookingReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            DataSet ds = Connection.GetDataSet("report_CorporateCreditBookings", param);
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                result.Add(new CLayer.CreditBookingReport()
                {
                    BookingDate = Connection.ToDate(dr["BookingDate"]),
                    BookingId = Connection.ToString(dr["BookingId"]),
                    BookingRefNo = Connection.ToString(dr["BookingRefNo"]),
                    PropertyName = Connection.ToString(dr["PropertyName"]),
                    //City = Connection.ToString(dr["PropertyCity"]),
                    //State = Connection.ToString(dr["PropertyState"]),            
                    CheckIn = Connection.ToDate(dr["CheckIn"]),
                    CheckOut = Connection.ToDate(dr["CheckOut"]),
                    CustomerName = Connection.ToString(dr["CustomerName"]),
                    CorporateName = Connection.ToString(dr["CorporateName"]),
                    TotalAmount = Math.Round(Connection.ToDecimal(dr["TotalAmount"])),
                    IsCorpbookingpaid = Connection.ToInteger(dr["IsCorpbookingpaid"]),
                    TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]),
                    FromDate = FromDate,
                    ToDate = ToDate

                });
            }
            return result;
        }
        public List<CLayer.GDSBookingStatusReport> GDSBookingStatusReportGST(DateTime FromDate, DateTime ToDate, int Start, int Limit)
        {
            List<CLayer.GDSBookingStatusReport> result = new List<CLayer.GDSBookingStatusReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));

            DataSet ds = Connection.GetDataSet("report_GDSBookingStatusGST", param);

            CLayer.GDSBookingStatusReport tmp;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                tmp = new CLayer.GDSBookingStatusReport();
                tmp.AccommodationId = Connection.ToLong(dr["AccommodationId"]);
                tmp.BookingId = Connection.ToLong(dr["BookingId"]);
                tmp.orderno = Connection.ToString(dr["orderno"]);
                tmp.BookingDate = Connection.ToDate(dr["BookingDate"]);
                tmp.BookingItemId = Connection.ToInteger(dr["BookingItemId"]);
                tmp.StayCategory = Connection.ToString(dr["StayCategory"]);
                tmp.checkin = Connection.ToDate(dr["checkin"]);
                tmp.checkout = Connection.ToDate(dr["checkout"]);
                tmp.noofaccommodations = Connection.ToInteger(dr["noofaccommodations"]);
                tmp.NoOfAdults = Connection.ToInteger(dr["NoOfAdults"]);
                tmp.NoOfChildren = Connection.ToInteger(dr["NoOfChildren"]);
                tmp.NoOfGuests = Connection.ToInteger(dr["NoOfGuests"]);
                tmp.Amount = Connection.ToDecimal(dr["Amount"]);
                tmp.NoOfDays = Connection.ToInteger(dr["NoOfDays"]);
                tmp.TotalComForSB = Connection.ToDecimal(dr["TotalComForSB"]);
                tmp.TotalComForOther = Connection.ToDecimal(dr["TotalComForOther"]);
                tmp.TotalAmount =(long)Math.Round(Connection.ToDouble(dr["TotalAmount"]),2);
                tmp.TotalTax = Connection.ToDecimal(dr["TotalTax"]);
                tmp.AccommodationId = Connection.ToInteger(dr["AccommodationId"]);
                tmp.AccommodationTypeId = Connection.ToInteger(dr["AccommodationTypeId"]);
                tmp.StayCategoryId = Connection.ToInteger(dr["StayCategoryId"]);
                tmp.AccommodationType = Connection.ToString(dr["AccommodationType"]);
                tmp.StayCategory = Connection.ToString(dr["StayCategory"]);
                tmp.PropertyId = Connection.ToLong(dr["PropertyId"]);
                tmp.PropertyTitle = Connection.ToString(dr["PropertyTitle"]);
                tmp.PropertyAddress = Connection.ToString(dr["PropertyAddress"]);
                tmp.City = Connection.ToString(dr["City"]);
                tmp.Country = Connection.ToString(dr["Country"]);
                tmp.CountryName = Connection.ToString(dr["CountryName"]);
                tmp.State = Connection.ToString(dr["State"]);
                tmp.StateName = Connection.ToString(dr["StateName"]);
                tmp.DailyRate = Connection.ToString(dr["DailyRate"]);
                tmp.ForUserFirstName = Connection.ToString(dr["ForUserFirstName"]);
                tmp.ForUserLastName = Connection.ToString(dr["ForUserLastName"]);
                tmp.ForUserEmail = Connection.ToString(dr["ForUserEmail"]);
                tmp.ForUserMobile = Connection.ToString(dr["ForUserMobile"]);
                tmp.FirstDayCharge = Connection.ToString(dr["FirstDayCharge"]);
                tmp.B2BDiscount = Connection.ToString(dr["B2BDiscount"]);
                tmp.MarkupForSB = Connection.ToString(dr["MarkupForSB"]);
                tmp.TotalRateTax = Connection.ToString(dr["TotalRateTax"]);
                tmp.TotalGuestTax = Connection.ToString(dr["TotalGuestTax"]);
                tmp.HotelConfirmNumber = Connection.ToString(dr["HotelConfirmNumber"]);
                tmp.BookingStatus = Connection.ToString(dr["BookingStatus"]);
                tmp.invoicenumber = Connection.ToString(dr["invoicenumber"]);
                tmp.invoicedate = Connection.ToDate(dr["invoicedate"]);
                tmp.duedate = Connection.ToDate(dr["duedate"]);
                tmp.InvoiceStatus = Connection.ToString(dr["InvoiceStatus"]);
                tmp.UserID = Connection.ToInteger(dr["UserID"]);
                tmp.FirstName = Connection.ToString(dr["FirstName"]);
                tmp.LastName = Connection.ToString(dr["LastName"]);
                tmp.Email = Connection.ToString(dr["Email"]);
                tmp.BillingEntityID = Connection.ToInteger(dr["BillingEntityID"]);
                tmp.BookingEntityID = Connection.ToInteger(dr["BookingEntityID"]);
                tmp.BookingEntity = Connection.ToString(dr["BookingEntity"]);
                tmp.BillingEntity = Connection.ToString(dr["BillingEntity"]);
                tmp.HotelConfirmNumber = Connection.ToString(dr["HotelConfirmNumber"]);
                tmp.CGSTTitle = Connection.ToString(dr["CGSTTitle"]);
                tmp.SGSTTitle = Connection.ToString(dr["SGSTTitle"]);
                tmp.IGSTTitle = Connection.ToString(dr["IGSTTitle"]);
                tmp.TotalCGSTTaxAmount = Connection.ToDecimal(dr["TotalCGSTTaxAmount"]);
                tmp.TotalSGSTTaxAmount = Connection.ToDecimal(dr["TotalSGSTTaxAmount"]);
                tmp.TotalIGSTTaxAmount = Connection.ToDecimal(dr["TotalIGSTTaxAmount"]);

                tmp.BookingStatus = GetBookingStatus(Connection.ToInteger(dr["BookingStatus"]));
                tmp.InvoiceStatus = GetInvoiceStatus(Connection.ToInteger(dr["InvoiceStatus"]));


                result.Add(tmp);
            }
            return result;
        }
        private string GetInvoiceStatus(int value)
        {
            //Saved = 1,
            //Submitted = 2
            string result = string.Empty;
            switch(value)
            {
                case 1:
                    result = "Saved";
                    break;
                case 2:
                    result = "Submitted";
                    break;

            }
                
            return result;

        }
        private string GetBookingStatus(int value)
        {

            string result = string.Empty;
            switch (value)
            {
                case 1:
                    result = "Cart";
                    break;
                case 2:
                    result = "Success";
                    break;
                case 3:
                    result = "CheckOut";
                    break;
                case 4:
                    result = "Cancelled";
                    break;
                case 5:
                    result = "Failed";
                    break;
                case 6:
                    result = "Deleted";
                    break;
                case 7:
                    result = "BookingRequest";
                    break;
                case 8:
                    result = "Decline";
                    break;
                case 9:
                    result = "Offline";
                    break;
                case 10:
                    result = "Offlineconfirm";
                    break;

            }
            return result;

        }
        public List<CLayer.BookingStatusReport> BookingStatusReportGST(DateTime FromDate, DateTime ToDate, int Start, int Limit,long LoginUserid)
        {
            List<CLayer.BookingStatusReport> result = new List<CLayer.BookingStatusReport>();
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            param.Add(Connection.GetParameter("pFromDate", DataPlug.DataType._DateTime, FromDate));
            param.Add(Connection.GetParameter("pToDate", DataPlug.DataType._DateTime, ToDate));
            param.Add(Connection.GetParameter("pStart", DataPlug.DataType._Int, Start));
            param.Add(Connection.GetParameter("pLimit", DataPlug.DataType._Int, Limit));
            param.Add(Connection.GetParameter("pLoginUserid", DataPlug.DataType._Int, LoginUserid)); 
            DataSet ds = Connection.GetDataSet("report_BookingStatusGST", param);

            CLayer.BookingStatusReport tmp;
            foreach (DataRow dr in ds.Tables[1].Rows)
            {
                tmp = new CLayer.BookingStatusReport();
                tmp.BookingDate = Connection.ToDate(dr["BookingDate"]);
                tmp.orderno = Connection.ToString(dr["orderno"]);
                tmp.PropertyName = Connection.ToString(dr["PropertyName"]);
                tmp.City = Connection.ToString(dr["PropertyCity"]);
                tmp.CustomerName = Connection.ToString(dr["CustomerName"]);
                tmp.InvoiceDate = Connection.ToDate(dr["InvoiceDate"]);
                tmp.InvoiceNumber = Connection.ToString(dr["InvoiceNumber"]);
                tmp.AccomodationType = Connection.ToString(dr["AccommodationtypeName"]);
                tmp.GuestName = Connection.ToString(dr["Guestname"]);
                tmp.SalesPerson = Connection.ToString(dr["SalesPerson"]);
                tmp.SalesRegion = Connection.ToString(dr["SalesRegion"]);
                tmp.NoOfPaxAdult = Connection.ToInteger(dr["NoOfPaxAdult"]);
                tmp.NoOfPaxChild = Connection.ToInteger(dr["NoOfPaxChild"]);
                tmp.AvgDailyRateBefreStaxForBuyPrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForBuyPrice"]);
                tmp.AvgDailyRateBefreStaxForSalePrice = Connection.ToDecimal(dr["AvgDailyRateBefreStaxForSalePrice"]);
                tmp.StaxForBuyPrice = Connection.ToDecimal(dr["StaxForBuyPrice"]);
                tmp.StaxForSalePrice = Connection.ToDecimal(dr["StaxForSalePrice"]);
                tmp.TotalBuyPrice = (long)Math.Round(Connection.ToDouble(dr["TotalBuyPrice"]), 2);
                tmp.TotalSalePrice = (long)Math.Round(Connection.ToDouble(dr["TotalSalePrice"]), 2);
                tmp.StaxForotherBuyPrice = Connection.ToDecimal(dr["StaxForotherBuyPrice"]);
                tmp.StaxForotherForSalePrice = Connection.ToDecimal(dr["StaxForotherForSalePrice"]);
                tmp.BuyPriceforotherservicesForSalePrice = Connection.ToDecimal(dr["BuyPriceforotherservicesForSalePrice"]);
                tmp.BuyPriceforotherservicesForBuyprice = Connection.ToDecimal(dr["BuyPriceforotherservicesForBuyprice"]);
                tmp.amount = (long)Math.Round(Connection.ToDouble(dr["amount"]), 2);
                tmp.NumberofNights = Connection.ToInteger(dr["Noofnight"]);
                tmp.NumberofRooms = Connection.ToInteger(dr["NoOfRooms"]);
                tmp.CheckIn = Connection.ToDate(dr["CheckIn"]);
                tmp.CheckOut = Connection.ToDate(dr["CheckOut"]);
                tmp.CustomerName = Connection.ToString(dr["CustomerName"]);
                tmp.Maildate = Connection.ToDate(dr["Maileddate"]);
                tmp.SupplierInvoiceDate = Connection.ToDate(dr["supplierInvoiceDate"]);
                tmp.SupplierRate = Connection.ToDecimal(dr["TotalBuyPrice"]);
                tmp.SupplierName = Connection.ToString(dr["SupplierName"]);
                tmp.CustomerBillvalue = Connection.ToDecimal(dr["TotalSalePrice"]);
                tmp.TotalRows = Connection.ToLong(ds.Tables[0].Rows[0]["NumberOfRows"]);
                tmp.SACCode = Connection.ToString(dr["HSNCode"]);
                tmp.SBBillingEntity = Connection.ToString(dr["BillingEntity"]);
                
                tmp.SBBookingEntity = Connection.ToString(dr["BookingEntity"]);



                tmp.bSGST = Connection.ToDouble(dr["bSGST"]);
                tmp.bCGST = Connection.ToDouble(dr["bCGST"]);
                tmp.bIGST = Connection.ToDouble(dr["bIGST"]);
                tmp.obSGST = Connection.ToDouble(dr["obSGST"]);
                tmp.obCGST = Connection.ToDouble(dr["obCGST"]);
                tmp.obIGST = Connection.ToDouble(dr["obIGST"]);
                tmp.SGST = Connection.ToDouble(dr["SGST"]);
                tmp.CGST = Connection.ToDouble(dr["CGST"]);
                tmp.IGST = Connection.ToDouble(dr["IGST"]);
                tmp.btSGST = Connection.ToDouble(dr["btSGST"]);
                tmp.btCGST = Connection.ToDouble(dr["btCGST"]);
                tmp.btIGST = Connection.ToDouble(dr["btIGST"]);
                tmp.oSGST = Connection.ToDouble(dr["oSGST"]);
                tmp.oCGST = Connection.ToDouble(dr["oCGST"]);
                tmp.oIGST = Connection.ToDouble(dr["oIGST"]);
                tmp.ORCAmount = Connection.ToDouble(dr["ORCAmount"]);
                tmp.BookingType = Connection.ToInteger(dr["BookingType"]);
                if (tmp.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
                {
                    tmp.DirectAmount = Connection.ToDouble(dr["DirectAmount"]);
                }
                else {
                    if (tmp.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.TAC)
                    {
                        tmp.DirectAmount = Connection.ToDouble(dr["TACAmount"]);
                    }
                    else
                        tmp.DirectAmount = 0;
                }
                
                //
                tmp.VBuyPriceBeforeTax = Connection.ToDouble(dr["VBuyPriceBeforeTax"]);
                tmp.VSalePriceBeforeTax = Connection.ToDouble(dr["VSalePriceBeforeTax"]);
                tmp.VBuyPriceTotal = Connection.ToDouble(dr["VBuyPriceTotal"]);
                tmp.vSalePriceTotal = Connection.ToDouble(dr["vSalePriceTotal"]);
                tmp.vBuyTax = Connection.ToDouble(dr["vBuyTax"]);
                tmp.vSaleGST = Connection.ToDouble(dr["vSaleGST"]);
                tmp.TotalNights = Connection.ToInteger(dr["totalnights"]);
                result.Add(tmp);
            }
            return result;
        }
    }
}

