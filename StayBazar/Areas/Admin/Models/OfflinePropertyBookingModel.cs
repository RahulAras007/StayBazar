using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Models
{
    public class OfflinePropertyBookingModel
    {
        //Customer details
        public long BookingId { get; set; }
        public string BookingData { get; set; }
        public DateTime BookCheckIn { get; set; }
        public DateTime BookCheckOut { get; set; }

        public long BookingId_OffLine { get; set; }
        public long BookingUserId_OffLine { get; set; }
        public long OfflineBookingId { get; set; }

        public long CustomerId { get; set; }
        

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Enter Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Enter Customer Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string CustomerEmail { get; set; }

        [Display(Name = "Mobile")]
        [Required(ErrorMessage = "Enter Customer Mobile")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string CustomerMobile { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Enter Customer Address")]
        public string CustomerAddress { get; set; }


        [Display(Name = "Customer Country")]
        [Required(ErrorMessage = "Select Customer Country")]
        public int CustomerCountry { get; set; }

        [Display(Name = "Customer State")]
        [Required(ErrorMessage = "Select Customer State")]
        public int CustomerState { get; set; }

        [Display(Name = "Customer City")]
        [Required(ErrorMessage = "Select Customer City")]
        public int CustomerCity { get; set; }
        [Required(ErrorMessage = "Enter Customer City")]
        public string CustomerCityname { get; set; }

        [Display(Name = "Guest Name")]

        public string GuestName { get; set; }


        [Display(Name = "Guest Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string GuestEmail { get; set; }



        //Property Details
        public long PropertyId { get; set; }

        public long CustomPropertyId { get; set; }
        public long CustomSupplierId { get; set; }
        [Display(Name = "Property Name")]
        [Required(ErrorMessage = "Enter Property Name")]
        public string PropertyName { get; set; }

        [Display(Name = "Property Address")]
        [Required(ErrorMessage = "Enter Property Address")]
        public string PropertyAddress { get; set; }

        [Display(Name = "Property Contact Number")]
        [Required(ErrorMessage = "Enter Property Contact Number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string PropertyContactNo { get; set; }

        [Display(Name = "Property Email")]
        [Required(ErrorMessage = "Enter Property Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string PropertyEmail { get; set; }

        [Display(Name = "Caretaker Name")]
        public string PropertyCaretakerName { get; set; }


        [Display(Name = "Property Country")]
        [Required(ErrorMessage = "Enter Property Country")]
        public int PropertyCountry { get; set; }

        [Required(ErrorMessage = "Enter Property State")]
        [Display(Name = "Property State")]
        public int PropertyState { get; set; }

        [Display(Name = "Property City")]
        [Required(ErrorMessage = "Select Property City")]
        public int PropertyCity { get; set; }

        [Required(ErrorMessage = "Enter Property City")]
        public string PropertyCityname { get; set; }



        //Supplier Details
        public long SupplierId { get; set; }

        public long SupplierIdforprop { get; set; }
        [Display(Name = "Supplier Name")]
        //[Required(ErrorMessage = "Enter Supplier Name")]
        public string SupplierName { get; set; }

        [Display(Name = "Supplier Address")]
        [Required(ErrorMessage = "Enter Supplier Address")]
        public string SupplierAddress { get; set; }

        [Display(Name = "Supplier Mobile")]
        [Required(ErrorMessage = "Enter Supplier Mobile")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string SupplierMobile { get; set; }

        [Display(Name = "Supplier Email")]
        [Required(ErrorMessage = "Enter Supplier Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string SupplierEmail { get; set; }

        [Display(Name = "Supplier Country")]
        [Required(ErrorMessage = "Enter Supplier Country")]
        public int SupplierCountry { get; set; }

        [Display(Name = "Supplier State")]
        [Required(ErrorMessage = "Enter Supplier State")]
        public int SupplierState { get; set; }

        [Display(Name = "Supplier City")]
        [Required(ErrorMessage = "Select Supplier City")]
        public int SupplierCity { get; set; }

        public string SupplierCityname { get; set; }

        //Booking Details
        [Display(Name = "Check In Date")]
        [Required]
        public string CheckIn { get; set; }

        [Display(Name = "Check Out Date")]
        [Required]
        public string CheckOut { get; set; }

        [Display(Name = "No: Of Night")]
        [Range(1, Int64.MaxValue, ErrorMessage = "No of Night must be at least 1")]
        public long NoOfNight { get; set; }


        [Display(Name = "No: Of Accomodation")]
        public long NoOfAccomodation { get; set; }



        [Display(Name = "Adult")]
        public long NoOfPaxAdult { get; set; }

        [Display(Name = "Child")]
        public long NoOfPaxChild { get; set; }

        [Display(Name = "No Of Rooms")]
        [Range(1, Int64.MaxValue, ErrorMessage = "No of Rooms must be at least 1")]
        public long NoOfRooms { get; set; }

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

        [Display(Name = "Service tax - other services ")]
        [Required]
        public decimal StaxForotherBuyPrice { get; set; }

        [Display(Name = "Total Amount - other services")]
        [Required]
        public decimal TotalAmtotherForBuyPrice { get; set; }


        [Display(Name = "Total Buy Price")]
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

        [Display(Name = "Total Sale Price")]
        [Required]
        public decimal TotalSalePrice { get; set; }


        //Other service details
        [Display(Name = "Details Of other services ")]
        public string OtherService { get; set; }

        public DateTime CreatedTime { get; set; }


        public SelectList CusCountryList { get; set; }
        public SelectList CusCityList { get; set; }
        public SelectList CusStateList { get; set; }

        public SelectList PropCountryList { get; set; }
        public SelectList PropCityList { get; set; }
        public SelectList PropStateList { get; set; }

        public SelectList SupCountryList { get; set; }
        public SelectList SupCityList { get; set; }
        public SelectList SupStateList { get; set; }


        [Display(Name = "Don't send mail to supplier")]
        public bool sendmailtosupplier { get; set; }
        [Display(Name = "Don't send mail to Customer")]
        public bool sendmailtocustomer { get; set; }



        public List<CLayer.OfflineBooking> OfflineBookingList { get; set; }


        public int currentPage { get; set; }
        public int SearchValue { get; set; }
        public int Limit { get; set; }
        public string SearchString { get; set; }
        public long TotalRows { get; set; }


        public enum OfflineBookingStatusValues { Name = 1, Phone = 2, Email = 3, city = 4 }

        public List<CLayer.OfflineBooking> CustompropertyList { get; set; }

        public List<CLayer.OfflineBooking> BookedpropertyList { get; set; }

        public int SaveStatus { get; set; }


        public int Count { get; set; }
        public int Type { get; set; }
        public SelectList SavedStatusTypes { get; set; }

        public List<CLayer.BookingItem> bookingitmList { get; set; }

        public string csvvalues { get; set; }



        public OfflinePropertyBookingModel()
        {

            List<KeyValuePair<int, string>> objStatustypes1 = new List<KeyValuePair<int, string>>();
            objStatustypes1.Add(new KeyValuePair<int, string>(0, "All"));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.Statuslist.Save, "Saved"));
            objStatustypes1.Add(new KeyValuePair<int, string>((int)CLayer.OfflineBooking.Statuslist.Submit, "Submitted"));

            SavedStatusTypes = new SelectList(objStatustypes1, "Key", "Value");



            //for customer

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



            //for property

            List<CLayer.Country> propcountries = BLayer.Country.GetAll();
            PropCountryList = new SelectList(propcountries, "CountryId", "Name");
            if (propcountries.Count > 0)
            {
                List<CLayer.State> propstates = BLayer.State.GetOnCountry((int)propcountries[0].CountryId);
                PropStateList = new SelectList(propstates, "StateId", "Name");

                List<CLayer.City> Propcities = null;
                if (propstates.Count > 0)
                {
                    Propcities = BLayer.City.GetOnState((int)propstates[0].StateId);
                }
                else
                {
                    Propcities = new List<CLayer.City>();
                }
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
                SupCityList = new SelectList(Supcities, "CityId", "Name");
            }

        }
        public void LoadPlaces()
        {
            //for customer

            List<CLayer.State> Cusstates = BLayer.State.GetOnCountry(CustomerCountry);
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


            //for property

            List<CLayer.State> Propstates = BLayer.State.GetOnCountry(PropertyCountry);
            PropStateList = new SelectList(Propstates, "StateId", "Name");

            List<CLayer.City> Propcities = null;
            if (Propstates.Count > 0)
            {
                Propcities = BLayer.City.GetOnState(PropertyState);
            }
            else
            {
                Propcities = new List<CLayer.City>();
            }
            Propcities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            PropCityList = new SelectList(Propcities, "CityId", "Name");



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
            Supcities.Add(new CLayer.City() { CityId = -1, Name = "Not avilable" });
            SupCityList = new SelectList(Supcities, "CityId", "Name");

        }

        public class SimpleBookingModel
        {
            public string BookingData { get; set; }
            public DateTime BookCheckIn { get; set; }
            public DateTime BookCheckOut { get; set; }
            public long PropertyId { get; set; }
            public long BookingId_OffLine { get; set; }
            public long BookingUserId_OffLine { get; set; }
        }
    }
}