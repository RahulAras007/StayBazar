using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StayBazar.Areas.Admin.Controllers
{
    public class OfflineBookingCustomerListController : Controller
    {
        private const int PAGER_SIZE = 50;
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> Customer = BLayer.OfflineBooking.GetAlCustomerList("", 0, PAGER_SIZE);
            model.CustomCustomerList = Customer;


            model.SearchString = "";
            model.SearchValue = 0;
            model.SearchStringCache = "";
            model.CustomCustomerList = Customer;
            model.TotalRows = 0;
            if (Customer.Count > 0)
            {
                model.TotalRows = Customer[0].TotalRows;
            }
            model.Limit = PAGER_SIZE;
            model.currentPage = 1;

            ViewBag.Filter = model;


            return View(model);
        }
        public ActionResult CreateCustomer(Models.OfflineBookingModel model)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            if (model.CustomerId != 0)
            {
                return View(model);
            }
            else
            {
                return View(data);
            }

        }

        [Common.AdminRolePermission]
        public ActionResult EditCustomer(long CustomerId)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            CLayer.OfflineBooking model = BLayer.OfflineBooking.GetOfflineBookingCustomerDetailsByID(CustomerId);
            data.CustomerId = model.CustomerId;
            data.CustomerName1 = model.CustomerName;
            data.CustomerEmail1 = model.CustomerEmail;
            data.CustomerMobile1 = model.CustomerMobile;
            data.CustomerCountry1 = model.CustomerCountry;
            data.CustomerState1 = model.CustomerState;
            data.CustomerCity1 = model.CustomerCity;
            data.CustomerCityname = model.CustomerCityname;
            data.BillingCountryId = model.BillingCountryId;
            data.CustomerAddress1 = model.CustomerAddress;
            data.CustomerType = model.CustomerType;
            data.BillingAddress = model.BillingAddress;
            data.BillingState = model.BillingState;
            data.BillingCity = model.BillingCity;
            data.PinCode = model.PinCode;
            data.BillingMobile = model.BillingMobile;
            data.ContactPerson = model.ContactPerson;
            data.OfficeNO = model.OfficeNO;
            //data.GuestName1 = model.GuestName1;
            //data.GuestEmail1 = model.GuestEmail1;
            data.BillingCityname = model.BillingCityname;
            data.CustomerpinCode = model.CustomerpinCode;
            data.NoInvoiceMail = model.NoInvoiceMail;
            data.CustomerPaymentMode = model.CustomerPaymentMode;
            data.CreditDays = model.CreditDays;

           




            //data.LoadCustomerGstStatesExceptIncludedByCustomerId(model.CustomerId);


            // data.LoadPlaces();
            //List<CLayer.OfflineBooking> Bookings = BLayer.OfflineBooking.GetAllBookingsByCusPropId(CustomPropertyId, 0, 25);
            //data.BookedpropertyList = Bookings;
            return View("EditCustomer", data);
        }

        [Common.AdminRolePermission]
        public ActionResult SaveCustomer(Models.OfflineBookingModel model)
        {
            
            long OfflineCustomerMasterId = 0;
            long CustID = model.CustomerId;
            try
            {
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                data.CustomerId = model.CustomerId;
                data.CustomerName1 = model.CustomerName1;
                data.CustomerEmail1 = model.CustomerEmail1;
                data.CustomerMobile1 = model.CustomerMobile1;
                data.CustomerCountry = model.CustomerCountry1;
                data.CustomerState = model.CustomerState1;
                if (model.CustomerCityname == null) model.CustomerCityname = "";
                data.CustomerCity = model.CustomerCity1;
                //data.CustomerCity = -1; // other

                data.CustomerCityname = model.CustomerCityname;
                data.BillingCountryId = model.BillingCountryId;
                data.CustomerAddress = model.CustomerAddress1;
                data.CustomerType = model.CustomerType;
                data.BillingAddress = model.BillingAddress;
                data.BillingState = model.BillingState;
                if (model.BillingCityname == null) model.BillingCityname = "";
                //if (model.BillingCityname == "")
                data.BillingCity = model.BillingCity;
                //else
                //    data.BillingCity = -1; //other
                data.PinCode = model.PinCode;
                data.BillingMobile = model.BillingMobile;
                data.ContactPerson = model.ContactPerson;
                data.OfficeNO = model.OfficeNO;
                data.GuestName1 = model.GuestName1;
                data.GuestEmail1 = model.GuestEmail1;
                data.BillingCityname = model.BillingCityname;
                data.CustomerpinCode = model.CustomerpinCode;
                data.NoInvoiceMail = model.NoInvoiceMail;

                data.CustomerPaymentMode = model.CustomerPaymentMode;
                data.CreditDays = model.CreditDays;

                long OfflineCustomerId = BLayer.OfflineBooking.EditOfflineBookingCustomer(data);

                //Save to customer master table
                data.CustomerName = model.CustomerName1;
                data.CustomerEmail = model.CustomerEmail1;
                data.CustomerMobile = model.CustomerMobile1;

                OfflineCustomerMasterId = BLayer.OfflineBooking.SaveMasterOfflineBookingCustomer(data);

                //Update Noinvoicemail bool (name,email,usertype)
                data.CustomerId = OfflineCustomerId;
                BLayer.OfflineBooking.UpdateNoinvoicemail(data);
                model.CustomerId = OfflineCustomerId;
                ViewBag.hidtypeid = model.CustomerType;
                ViewBag.hidcustid = OfflineCustomerId;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            //ViewBag.Message = "Customer details updated";
            TempData["Success"] = (CustID)>0? "Customer details updated": "Customer details added";
    
            return RedirectToAction("EditCustomer", new { CustomerId = OfflineCustomerMasterId });

        }



        public ActionResult Pager(Models.OfflineBookingModel data)
        {
            if (data.SearchStringCache == null) data.SearchStringCache = "";
            //   if (data.SearchString == "") 
            data.SearchValue = 0;
            List<CLayer.OfflineBooking> cutomers = BLayer.OfflineBooking.GetAlCustomerList(data.SearchStringCache, (data.currentPage - 1) * PAGER_SIZE, PAGER_SIZE);
            ViewBag.Filter = new Models.UserSearchModel();

            Models.OfflineBookingModel forPager = new Models.OfflineBookingModel()
            {
                SearchStringCache = data.SearchStringCache,
                SearchValue = data.SearchValue,
                CustomCustomerList = cutomers,
                TotalRows = 0,
                Limit = PAGER_SIZE,
                currentPage = data.currentPage
            };
            if (cutomers.Count > 0)
            {
                forPager.TotalRows = cutomers[0].TotalRows;
            }
            forPager.CustomCustomerList = cutomers;
            //    ViewBag.Filter = forPager;

            return PartialView("List", forPager);
        }

        public ActionResult Filter(Models.OfflineBookingModel data)
        {
            if (data.SearchString == null) data.SearchString = "";
            // if (data.SearchString == "") data.SearchValue = 0;
            data.SearchStringCache = data.SearchString;
            data.SearchValue = 0;
            List<CLayer.OfflineBooking> cutomers = BLayer.OfflineBooking.GetAlCustomerList(data.SearchString, 0, PAGER_SIZE);

            data.CustomCustomerList = cutomers;

            //    data.SearchString = "";
            data.SearchValue = 0;
            //     data.SearchStringCache = "";
            data.CustomCustomerList = cutomers;
            data.TotalRows = 0;
            if (cutomers.Count > 0)
            {
                data.TotalRows = cutomers[0].TotalRows;
            }
            data.Limit = PAGER_SIZE;
            data.currentPage = 1;

            //     ViewBag.Filter = data;


            return View("Index", data);

        }


        public void SaveGST(Models.OfflineBookingModel model, int CustID)
        {
            long custgststeid = 0;
            try
            {
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                data.SubCustomerAddress = model.SubCustomerAddress;
                data.SubCustomerCity = model.SubCustomerCity;
                data.SubCustomerCityname = model.SubCustomerCityname;
                data.SubCustomerpinCode = model.SubCustomerpinCode;
                data.SubCustomerGstRegNo = model.GSTRegistrationNo;
                data.SubCustomerGstStateId = Convert.ToInt32(model.StateOfRegistration);
                data.SubCustomerid = CustID;
                data.CustomerTableType = 2;
                data.OffGSTId = model.OffGSTId;
                BLayer.OfflineBooking.SaveGSTIn(data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }


            //
        }

        public ActionResult GSTList(int custid)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> GSTList = BLayer.OfflineBooking.GetGSTList(custid);
            model.CustomCustomerList = GSTList;
            return View("OfflineBookingCustomerGST", model);
        }

        public void GSTDelete(int OFFGSTID)
        {
            BLayer.OfflineBooking.GSTDelete(OFFGSTID);
        }

        [AllowAnonymous]
        public ActionResult GetOfflinegstDetailsById(long OffGSTId)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            try
            {
                CLayer.OfflineBooking GstDetails = BLayer.OfflineBooking.GetOfflinegstDetailsById(OffGSTId);
                model.HiddenSubCustomerCity = GstDetails.SubCustomerCity;
                model.HiddenSubCustomerCityname = GstDetails.SubCustomerCityname;
                model.HiddenSubCustomerAddress = GstDetails.SubCustomerAddress;
                model.HiddenSubCustomerpinCode = GstDetails.SubCustomerpinCode;
                model.HiddenSubCustomerGstRegNo = GstDetails.SubCustomerGstRegNo;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/OfflineBookingGST/HiddenGstAddress.cshtml", model);
        }

    }
}