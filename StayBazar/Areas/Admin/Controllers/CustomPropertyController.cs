using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class CustomPropertyController : Controller
    {
        public const int ROWS_PER_PAGE = 80;
        public const int SEARCH_NAME = 1;

        private void SetViewBagSettings(string searchString,int currentPage)
        {
            ViewBag.SearchString = searchString;
            ViewBag.currentPage = currentPage;
        }
        // GET: Admin/CustomProperty
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> Properties = BLayer.OfflineBooking.GetAllPropertyByCusPropId("", SEARCH_NAME, 0, ROWS_PER_PAGE);
            SetViewBagSettings("", 1);
            return View("Index", Properties);
        }

        [Common.AdminRolePermission]
        public ActionResult Filter(string searchString)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> Properties = BLayer.OfflineBooking.GetAllPropertyByCusPropId(searchString, SEARCH_NAME, 0, ROWS_PER_PAGE);
            SetViewBagSettings("", 1);
            return View("Index", Properties);
        }

        [Common.AdminRolePermission]
        public ActionResult Pager(string SearchStringCache,int currentPage)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            if (currentPage < 1) currentPage = 1;
            List<CLayer.OfflineBooking> Properties = BLayer.OfflineBooking.GetAllPropertyByCusPropId(SearchStringCache, SEARCH_NAME, (currentPage - 1) * ROWS_PER_PAGE, ROWS_PER_PAGE);
            SetViewBagSettings(SearchStringCache, currentPage);
            return View("_List", Properties);
        }

        [Common.AdminRolePermission]
        public ActionResult AddNew()
        {

            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            return View("Property", data);

        }
        [Common.AdminRolePermission]
        public ActionResult SaveProperty(Models.OfflineBookingModel model)
        {
            int count = BLayer.Property.GetPropertybyEmail(model.PropertyEmail);

            CLayer.OfflineBooking data = new CLayer.OfflineBooking();

            data.CustomPropertyId = model.CustomPropertyId;
            data.PropertyName = model.PropertyName;
            data.PropertyAddress = model.PropertyAddress;
            data.PropertyCity = model.PropertyCity;
            data.PropertyCityname = model.PropertyCityname;
            data.PropertyState = model.PropertyState;
            data.PropertyCountry = model.PropertyCountry;
            data.PropertyContactNo = model.PropertyContactNo;
            data.PropertyCaretakerName = model.PropertyCaretakerName;
            data.PropertyEmail = model.PropertyEmail;
            data.PropertyPinCode = model.PropertyPinCode;

            data.SupplierName = model.SupplierName;
            data.SupplierAddress = model.SupplierAddress;
            data.SupplierCity = model.SupplierCity;
            data.SupplierCityname = model.SupplierCityname;
            data.SupplierState = model.SupplierState;
            data.SupplierCountry = model.SupplierCountry;
            data.SupplierMobile = model.SupplierMobile;
            data.SupplierEmail = model.SupplierEmail;
            data.SupplierPinCode = model.SupplierPinCode;

            data.AccountNumber = model.AccountNumber;
            data.BankName = model.BankName;
            data.BranchName = model.BranchName;
            data.BranchAddress = model.BranchAddress;
            data.AccountName = model.AccountName;
            data.AccountType = model.AccountType;
            data.IFSCcode = model.IFSCcode;
            data.MICRcode = model.MICRcode;
            data.CareTakerPhone = model.CareTakerPhone;
            data.CareTakerEmail = model.CareTakerEmail;

            data.GSTRegistrationNo = model.GSTRegistrationNo;
            data.PAN = model.PAN;
            if (count == 0)
            {
                long CustomPropertyId = BLayer.OfflineBooking.SaveCustomPropertyotherDetails(data);
            }

            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllPropertyByCusPropId("", SEARCH_NAME, 0, ROWS_PER_PAGE);
            SetViewBagSettings("", 1);
            return View("Index", users);
        }


        //[HttpPost]
        public long SavePropertyFromOffline(Models.OfflineBookingModel model)
        {
            int count = BLayer.Property.GetPropertybyEmail(model.PropertyEmail1);

            CLayer.OfflineBooking data = new CLayer.OfflineBooking();

            data.CustomPropertyId = model.CustomPropertyId;
            data.PropertyName = model.PropertyName1;
            data.PropertyAddress = model.PropertyAddress1;
            data.PropertyCity = model.PropertyCity1;
            data.PropertyCityname = model.PropertyCityname1;
            data.PropertyState = model.PropertyState1;
            data.PropertyCountry = model.PropertyCountry1;
            data.PropertyContactNo = model.PropertyContactNo1;
            data.PropertyCaretakerName = model.PropertyCaretakerName1;
            data.PropertyEmail = model.PropertyEmail1;
            data.PropertyPinCode = model.PropertyPinCode1;

            data.SupplierName = model.SupplierName1;
            data.SupplierAddress = model.SupplierAddress;
            data.SupplierPinCode = model.SupplierPinCode;
            data.SupplierCity = model.SupplierCity1;
            data.SupplierCityname = model.SupplierCityname1;
            data.SupplierState = model.SupplierState;
            data.SupplierCountry = model.SupplierCountry;
            data.SupplierMobile = model.SupplierMobile;
            data.SupplierEmail = model.SupplierEmail;

            data.AccountNumber = model.AccountNumber;
            data.BankName = model.BankName;
            data.BranchName = model.BranchName;
            data.BranchAddress = model.BranchAddress;
            data.AccountName = model.AccountName;
            data.AccountType = model.AccountType;
            data.IFSCcode = model.IFSCcode;
            data.MICRcode = model.MICRcode;
            data.CareTakerPhone = model.CareTakerPhone;
            data.CareTakerEmail = model.CareTakerEmail;

            if (model.SubPropertyGstRegNo == null) { model.SubPropertyGstRegNo = ""; }
            data.SubPropertyGstRegNo = model.SubPropertyGstRegNo;
            data.PAN = model.PAN;
            long CustomPropertyId = 0;
            if (count == 0)
            {
                 CustomPropertyId= BLayer.OfflineBooking.SaveCustomPropertyotherDetails(data);
                 BLayer.OfflineBooking.SavePropertyGstRegNoByPropertyid(CustomPropertyId, 2, data.SubPropertyGstRegNo);
            }


            return CustomPropertyId;

        }


        [Common.AdminRolePermission]
        public ActionResult EditCustomProperty(long CustomPropertyId)
        {

            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            CLayer.OfflineBooking model = BLayer.OfflineBooking.GetDetailsByCustomProperyId(CustomPropertyId);
            data.CustomPropertyId = model.CustomPropertyId;
            data.PropertyName = model.PropertyName;
            data.PropertyAddress = model.PropertyAddress;
            data.PropertyCity = model.PropertyCity;
            data.PropertyCityname = model.PropertyCityname;
            data.PropertyState = model.PropertyState;
            data.PropertyCountry = model.PropertyCountry;
            data.PropertyContactNo = model.PropertyContactNo;
            data.PropertyCaretakerName = model.PropertyCaretakerName;
            data.PropertyEmail = model.PropertyEmail;
            data.PropertyPinCode = model.PropertyPinCode;

            data.SupplierName = model.SupplierName;
            data.SupplierAddress = model.SupplierAddress;
            data.SupplierCity = model.SupplierCity;
            data.SupplierCityname = model.SupplierCityname;
            data.SupplierState = model.SupplierState;
            data.SupplierCountry = model.SupplierCountry;
            data.SupplierMobile = model.SupplierMobile;
            data.SupplierEmail = model.SupplierEmail;
            data.SupplierPinCode = model.SupplierPinCode;

            data.LoadPlaces();

            data.AccountNumber = model.AccountNumber;
            data.BankName = model.BankName;
            data.BranchName = model.BranchName;
            data.BranchAddress = model.BranchAddress;
            data.AccountName = model.AccountName;
            data.AccountType = model.AccountType;
            data.IFSCcode = model.IFSCcode;
            data.MICRcode = model.MICRcode;
            data.CareTakerPhone = model.CareTakerPhone;
            data.CareTakerEmail = model.CareTakerEmail;
            data.GSTRegistrationNo = model.GSTRegistrationNo;
            data.PAN = model.PAN;
            
            List<CLayer.OfflineBooking> Bookings = BLayer.OfflineBooking.GetAllBookingsByCusPropId(CustomPropertyId, 0, 25);
            data.BookedpropertyList = Bookings;

            return View("Property", data);
        }

        [Common.AdminRolePermission]
        public ActionResult Custompropdelete(long CustomPropertyId)
        {

            BLayer.OfflineBooking.DeleteCustomProperty(CustomPropertyId);

            List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllPropertyByCusPropId("", SEARCH_NAME, 0, ROWS_PER_PAGE);

            SetViewBagSettings("", 1);
            return View("Index", users);

        }


    }
}