using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class OfflineBookingController : Controller
    {
        #region CustomMethods

        public Models.OfflineBookingModel LoadVal(long OfflineBookingId)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
            data.OfflineBookingId = OfflineBookingId;
            data.CustomerId = Offlinedata.CustomerId;
            data.PropertyId = Offlinedata.PropertyId;
            data.SupplierId = Offlinedata.SupplierId;

            return data;
        }

        #endregion

        // GET: Admin/OfflineBooking
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            return View();
        }

        [Common.AdminRolePermission]
        public ActionResult OfflineBooking()
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            data.BookingDate= Convert.ToString(DateTime.Now);

            List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list1 = new List<CLayer.TaxPercentage>();
            data.TaxPercentageList_Service = new List<Models.TaxPercentage>();
            data.TaxPercentageList_Others = new List<Models.TaxPercentage>();
            list = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(0, "Service Tax");
            list1 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(0, "Service Tax - Others");
            foreach (var item in list)
            {
                Models.TaxPercentage cls = new Models.TaxPercentage();
                cls.TaxPerID = 0;
                cls.TaxOfflineBookingID = 0;
                cls.TaxTitle = item.TaxTitle;
                cls.TaxPercent = item.TaxPercent;
                cls.TaxType = item.TaxType;
                data.TaxPercentageList_Service.Add(cls);
            }
            foreach (var item in list1)
            {
                Models.TaxPercentage cls = new Models.TaxPercentage();
                cls.TaxPerID = 0;
                cls.TaxOfflineBookingID = 0;
                cls.TaxTitle = item.TaxTitle;
                cls.TaxPercent = item.TaxPercent;
                cls.TaxType = item.TaxType;
                data.TaxPercentageList_Others.Add(cls);
            }

            return View(data);
        }

        [Common.AdminRolePermission]
        public async Task<Models.OfflineBookingModel> SaveOfflineBooking(Models.OfflineBookingModel model)
        {
            try
            {
                // save property as new entry (  custom property  )
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                //int count = BLayer.Property.GetPropertybyEmail(model.PropertyEmail);
                long userId = GetUserId();
                long CustomPropertyId = 0;
                data.CreatedBy = userId;

                DateTime BookingDate = DateTime.MinValue;
                DateTime.TryParse(model.BookingDate, out BookingDate);

                data.CreatedTime = BookingDate;

                data.PropertyName = model.PropertyName;
                data.PropertyAddress = model.PropertyAddress;
                data.PropertyCity = model.PropertyCity;
                data.PropertyCityname = model.PropertyCityname;
                data.PropertyState = model.PropertyState;
                data.PropertyCountry = model.PropertyCountry;
                data.PropertyContactNo = model.PropertyContactNo;
                data.PropertyCaretakerName = model.PropertyCaretakerName;
                data.PropertyEmail = model.PropertyEmail;
                data.NewCustomerReferenceNo = model.NewCustomerReferenceNo;
                data.SupplierName = model.SupplierName;
                data.SupplierAddress = model.SupplierAddress;
                data.SupplierCity = model.SupplierCity;
                data.SupplierCityname = model.SupplierCityname;
                data.SupplierState = model.SupplierState;
                data.SupplierCountry = model.SupplierCountry;
                data.SupplierMobile = model.SupplierMobile;
                data.SupplierEmail = model.SupplierEmail;
                data.SalesRegion = model.SalesRegion;

                data.PropertyPinCode = model.PropertyPinCode;
                data.SupplierPinCode = model.SupplierPinCode1;
                data.CustomerpinCode = model.CustomerpinCode;
                //if (count == 0)
                //{
                //    CustomPropertyId = BLayer.OfflineBooking.SaveCustomPropertyDetails(data);

                //    if (CustomPropertyId > 0)
                //    {
                //        model.PropertyId = 0;
                //        model.SupplierId = 0;
                //    }
                //}

                data.CustomerName = model.CustomerName;
                data.CustomerEmail = model.CustomerEmail;
                data.CustomerAddress = model.CustomerAddress;
                data.CustomerCity = model.CustomerCity;
                data.CustomerCountry = model.CustomerCountry;
                data.CustomerState = model.CustomerState;
                data.CustomerCityname = model.CustomerCityname;
                data.CustomerMobile = model.CustomerMobile;


                //long OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomer(data);
                data.CustomerId = model.CustomerId;
                // long AddressId = BLayer.OfflineBooking.SaveUserAddressForOfflineBook(data);

                data.SupplierId = model.SupplierId;
                data.PropertyId = model.PropertyId;
               


                data.Accommodationid = model.Accommodationid;
                data.Accommodationtypeid = model.Accommodationtypeid;
                data.AccommodatoinType = model.AccommodatoinType;
                data.AccommodatoinTypename = model.AccommodationTypeName;
                data.StayCategory = model.StayCategory;
                data.StayCategoryName = model.StayCategoryName;
                data.StayCategoryid = model.StayCategoryid;
                data.NoOfNight = model.NoOfNight;
                data.NoOfRooms = model.NoOfRooms;
                data.NoOfPaxAdult = model.NoOfPaxAdult;
                data.NoOfPaxChild = model.NoOfPaxChild;
                DateTime CheckInt = DateTime.MinValue;
                DateTime.TryParse(model.CheckIn, out CheckInt);

                DateTime CheckOutt = DateTime.MinValue;
                DateTime.TryParse(model.CheckOut, out CheckOutt);

                data.CheckIn = CheckInt;
                data.CheckOut = CheckOutt;
                data.GuestEmail = model.GuestEmail;
                data.GuestName = model.GuestName;
                data.OtherService = model.OtherService;
                data.TotalAmtForBuyPrice = model.TotalAmtForBuyPrice;
                data.AvgDailyRateBefreStaxForBuyPrice = model.AvgDailyRateBefreStaxForBuyPrice;
                data.StaxForBuyPrice = model.StaxForBuyPrice;
                data.BuyPriceforotherservicesForBuyprice = model.BuyPriceforotherservicesForBuyprice;
                data.StaxForotherBuyPrice = model.StaxForotherBuyPrice;
                data.TotalAmtotherForBuyPrice = model.TotalAmtotherForBuyPrice;
                data.TotalBuyPrice = model.TotalBuyPrice;

                data.TotalAmtForSalePrice = model.TotalAmtForSalePrice;
                data.AvgDailyRateBefreStaxForSalePrice = model.AvgDailyRateBefreStaxForSalePrice;
                data.StaxForSalePrice = model.StaxForSalePrice;
                data.BuyPriceforotherservicesForSalePrice = model.BuyPriceforotherservicesForSalePrice;
                data.StaxForotherForSalePrice = model.StaxForotherForSalePrice;
                data.TotalAmtotherForSalePrice = model.TotalAmtotherForSalePrice;
                data.TotalSalePrice = model.TotalSalePrice;
                data.MailContent = model.MailContent;
                data.HotelConfirmationNo = model.HotelConformationNo;

                data.sendmailtocustomer = model.sendmailtocustomer;
                data.sendmailtosupplier = model.sendmailtosupplier;

                data.CustomPropertyId = model.CustomPropertyId;
                data.OfflineBookingId = model.OfflineBookingId;

                data.SaveStatus = (int)CLayer.OfflineBooking.Statuslist.Save;
                data.SalesPersonId = model.SalesPersonId;
               

                data.BillingCountryId = model.BillingCountryId1;
                data.BillingState = model.BillingState1;
                data.BillingCity = model.BillingCity1;
                data.PinCode = model.PinCode1;

                data.BillingMobile = model.BillingMobile1;
                data.ContactPerson = model.ContactPerson1;
                data.OfficeNO = model.OfficeNO1;
                data.BillingAddress = model.BillingAddress1;


                data.HotelFacility = model.HotelFacility;

                long OfflineBookId = BLayer.OfflineBooking.SaveOfflineBookingDetails(data);



                if (OfflineBookId > 0)
                {
                    //check for any invoices
                    CLayer.Invoice indata = BLayer.Invoice.GetInvoiceByOfflineBooking(OfflineBookId);
                    if(indata != null)
                    { //invoice will be available only after approving booking
                        indata.HtmlSection1 = "";
                        indata.HtmlSection2 = "";
                        indata.HtmlSection3 = "";
                        indata.Status = (int)CLayer.ObjectStatus.InvoiceStatus.Saved;
                        BLayer.Invoice.Save(indata); //reset the invoice because a change happened after approval of offline booking
                    }
                    // set order no: to offline booking
                    string orderNo = "";
                    CLayer.Role.Roles rle = CLayer.Role.Roles.Customer;

                    string existorderno = BLayer.OfflineBooking.ExistOrderno(OfflineBookId);

                    if (existorderno == "" || existorderno == null)
                    {
                        BLayer.OfflineBooking.SetPaymentRefNo(OfflineBookId, rle, orderNo);
                    }
                }

                model.OfflineBookingId = OfflineBookId;
                data.OfflineBookingId = OfflineBookId;


                if (OfflineBookId > 0)
                {
                    // save customer details
                    //long OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomer(data);

                    // update userid in offline booking

                    long OfflineCustomerId = 0;
                    
                    if(model.CategoryType == "user")
                    {
                        BLayer.OfflineBooking.RemoveCustomerEntry(OfflineBookId);
                        CLayer.OfflineBooking userdetails = new CLayer.OfflineBooking();

                        userdetails = BLayer.OfflineBooking.UserDetailsByOffliceBookingId(data);

                        CLayer.User usr = BLayer.User.Get(data.CustomerId);
                        if(usr == null)
                        { throw new Exception("User not found in Offline Booking_Save Id : " + data.CustomerId.ToString()); }
                        data.UserType = usr.UserTypeId;
                        
                        OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomerNEWForUser(userdetails);

                        //Update Noinvoicemail bool (name,email,usertype)
                        data.CustomerName1 = userdetails.CustomerName;
                        data.CustomerEmail1 = userdetails.CustomerEmail;

                        data.NoInvoiceMail = false;

                        data.CustomerId = OfflineCustomerId;
                        BLayer.OfflineBooking.UpdateNoinvoicemail(data);

                        BLayer.OfflineBooking.UpdateOfflineBookingCustomer(OfflineBookId, OfflineCustomerId);
                        
                    }
                    else
                    {
                        // BLayer.OfflineBooking.UpdateOfflineBookingCustomerNew(OfflineBookId, data.CustomerId);
                        BLayer.OfflineBooking.RemoveCustomerEntry(OfflineBookId);
                        data.CustomerType = BLayer.OfflineBooking.GetUserType(data.CustomerId);
                        OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomerNEW(data);

                         BLayer.OfflineBooking.UpdateOfflineBookingCustomer(OfflineBookId, OfflineCustomerId);
                        //Update Noinvoicemail bool (name,email,usertype)
                        data.CustomerName1 = model.CustomerName;
                        data.CustomerEmail1 = model.CustomerEmail;
                        data.NoInvoiceMail = model.NoInvoiceMail;
                        data.CustomerId = OfflineCustomerId;
                        BLayer.OfflineBooking.UpdateNoinvoicemail(data);
                    }

                    // save offline booking id in offline-customer table

                    BLayer.OfflineBooking.UpdateOfflineBookingCustomerNew(OfflineBookId, OfflineCustomerId);
                    

                    // save property details
                    long OfflinePropId = BLayer.OfflineBooking.SavePropertyDetails(data);
                    model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookId);

                    if (model.TaxPercentageList_Service != null && model.TaxPercentageList_Service.Count() > 0)
                    {
                        List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                        foreach (Models.TaxPercentage item in model.TaxPercentageList_Service)
                        {
                            CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                            cls.TaxPerID = item.TaxPerID;
                            cls.TaxOfflineBookingID = OfflineBookId;
                            cls.TaxTitle = item.TaxTitle;
                            cls.TaxPercent = item.TaxPercent;
                            cls.TaxType = "Service Tax";
                            list.Add(cls);
                        }
                        BLayer.OfflineBooking.save_OfflineTaxes(list);
                    }
                    if (model.TaxPercentageList_Others != null && model.TaxPercentageList_Others.Count() > 0)
                    {
                        List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                        foreach (Models.TaxPercentage item in model.TaxPercentageList_Others)
                        {
                            CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                            cls.TaxPerID = item.TaxPerID;
                            cls.TaxOfflineBookingID = OfflineBookId;
                            cls.TaxTitle = item.TaxTitle;
                            cls.TaxPercent = item.TaxPercent;
                            cls.TaxType = "Service Tax - Others";
                            list.Add(cls);
                        }
                        BLayer.OfflineBooking.save_OfflineTaxes(list);
                    }

                }

                //if (OfflineBookId > 0)
                //{

                //    await OfflineBookingConfirm(OfflineBookId);
                //}

                //  TempData["OfflineBookSuccessMessage"] = "Booking details are saved";
                return model;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
            return model;
        }

        [Common.AdminRolePermission]
        public ActionResult GetofflineVendordetails(long offlinebookingid, long vendorid)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            try
            {
                data = BLayer.OfflineBooking.getVendorDetailsbyVid(vendorid);
                model.vendorId = data.vendorId;
                model.vendorname = data.vendorname;
                model.vendoraddress = data.vendoraddress;
                model.valuebeforeservicetax = data.valuebeforeservicetax;
                model.OfflineBookingId = data.OfflineBookingId;
                model.vendorId = data.vendorId;
                model.OfflineBookingId = data.OfflineBookingId;
                model.vendorname = data.vendorname;
                model.vendoraddress = data.vendoraddress;
                model.address1 = data.address1;
                model.address2 = data.address2;
                model.City = data.City;
                model.pin = data.pin;
                model.ContactPerson = data.contactperson;
                model.contactno = data.contactno;
                model.emailaddress = data.emailaddress;
                model.natureofservice = data.natureofservice;
                model.valuebeforeservicetax = data.valuebeforeservicetax;
                model.servicetaxamount = data.servicetaxamount;
                model.totalamountpayable = data.totalamountpayable;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/OfflineBooking/VendorPayment.cshtml", model);
        }

        [Common.AdminRolePermission]
        public async Task<ActionResult> SubmitOfflineBooking(Models.OfflineBookingModel dt)
        {
            Models.OfflineBookingModel data = await SaveOfflineBooking(dt);
            return View("~/Areas/Admin/Views/OfflineBooking/Vendorindex.cshtml", data);
        }

        [Common.AdminRolePermission]
        public ActionResult ViewBookingData(long OfflineBookingId, int? Dir)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            data.Direction = Dir.Value;
            data.OfflineBookingId = OfflineBookingId;
            data.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);
            ViewBag.viewonly = "yes";
            return View("~/Areas/Admin/Views/OfflineBooking/BookingData.cshtml", data);
        }


        [HttpPost]
        public ActionResult VendorPayment(Models.OfflineBookingModel model)
        {

            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            data.vendorId = model.vendorId;
            data.OfflineBookingId = model.OfflineBookingId;
            data.vendorname = model.vendorname;
            data.vendoraddress = model.vendoraddress;
            data.address1 = model.address1;
            data.address2 = model.address2;
            data.City = model.City;
            data.pin = model.pin;
            data.contactperson = model.ContactPerson;
            data.contactno = model.contactno;
            data.emailaddress = model.emailaddress;
            data.natureofservice = model.natureofservice;

            data.valuebeforeservicetax = model.valuebeforeservicetax;
            data.servicetaxamount = model.servicetaxamount;
            data.totalamountpayable = model.totalamountpayable;

            if (data.OfflineBookingId > 0)
            {

                data.vendorId = BLayer.OfflineBooking.SaveVendorDetails(data);

                model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(data.OfflineBookingId);
            }
            TempData["OfflineBookSuccessMessage"] = "Booking details are saved";
            return View("~/Areas/Admin/Views/OfflineBooking/_VendorPaymentList.cshtml", model.Vendorlist);
        }


        public ActionResult NewVendorPayment(long Offlinebookingid)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            model.OfflineBookingId = Offlinebookingid;
            return View("~/Areas/Admin/Views/OfflineBooking/VendorPayment.cshtml", model);
        }
        public ActionResult VendorPaymentSubmit(Models.OfflineBookingModel model)
        {

            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            data.vendorId = model.vendorId;
            data.OfflineBookingId = model.OfflineBookingId;
            data.vendorname = model.vendorname;
            data.vendoraddress = model.vendoraddress;
            data.address1 = model.address1;
            data.address2 = model.address2;
            data.City = model.City;
            data.pin = model.pin;
            data.contactperson = model.ContactPerson;
            data.contactno = model.contactno;
            data.emailaddress = model.emailaddress;
            data.natureofservice = model.natureofservice;

            data.valuebeforeservicetax = model.valuebeforeservicetax;
            data.servicetaxamount = model.servicetaxamount;
            data.totalamountpayable = model.totalamountpayable;

            if (data.OfflineBookingId > 0)
            {
                if (model.vendorname != null && model.vendorname != "")
                {
                    data.vendorId = BLayer.OfflineBooking.SaveVendorDetails(data);
                }

                model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(data.OfflineBookingId);
            }
            return View("~/Areas/Admin/Views/OfflineBooking/BookingData.cshtml", model);
        }


        public ActionResult DeleteVendorDetails(long OfflineBookingId, long VendorId)
        {

            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();

            BLayer.OfflineBooking.DeleteVendorDetails(VendorId);
            model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);

            return View("~/Areas/Admin/Views/OfflineBooking/_VendorPaymentList.cshtml", model.Vendorlist);
        }
        public async Task<ActionResult> OverollSubmit(long OfflineBookingId)
        {
            //change status to submit
            BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Submit);

            //if (OfflineBookingId > 0)
            //{
            //    await OfflineBookingConfirm(OfflineBookingId);
            //}
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            model.OfflineBookingId = OfflineBookingId;
            model.SaveStatus = (int)CLayer.OfflineBooking.Statuslist.Submit;
            return View("~/Areas/Admin/Views/OfflineBooking/OfflineBookingConfirm.cshtml", model);

        }
        public ActionResult OfflineEdit(long OfflineBookId)
        {
            CLayer.OfflineBooking offedit = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookId);

            CLayer.OfflineBooking customerdetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookId);

            CLayer.OfflineBooking offprodt = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookId);

            Models.OfflineBookingModel model = new Models.OfflineBookingModel();

            model.OfflineBookingId = OfflineBookId;
            model.SupplierId = offedit.SupplierId;
            model.PropertyId = offedit.PropertyId;
            model.CustomerId = offedit.CustomerId;
            //model.FromCustomerId = offedit.CustomerId;
            //model.FromCustomer = 1;
            model.CustomerName = customerdetails.CustomerName;
            model.CustomerMobile = customerdetails.CustomerMobile;
            model.CustomerAddress = customerdetails.CustomerAddress;
            model.CustomerCityname = customerdetails.CustomerCityname;
            model.CustomerCity = customerdetails.CustomerCity;
            model.CustomerState = customerdetails.CustomerState;
            model.CustomerCountry = customerdetails.CustomerCountry;
            model.CustomerEmail = customerdetails.CustomerEmail;
            model.CustomerpinCode = customerdetails.CustomerpinCode;
            model.SalesRegion = offedit.SalesRegion;
            model.BookingDate = Convert.ToString(offedit.CreatedTime);

            model.SaveStatus = offedit.SaveStatus;

            model.PropertyPinCode = offprodt.PropertyPinCode;
            model.SupplierPinCode1 = offprodt.SupplierPinCode;


            model.CustomPropertyId = offedit.CustomPropertyId;
            model.GuestName = offedit.GuestName;
            model.GuestEmail = offedit.GuestEmail;
            model.NewCustomerReferenceNo = offedit.NewCustomerReferenceNo;


            model.PropertyName = offprodt.PropertyName;
            model.PropertyAddress = offprodt.PropertyAddress;
            model.PropertyCity = offprodt.PropertyCity;
            model.PropertyCityname = offprodt.PropertyCityname;
            model.PropertyState = offprodt.PropertyState;
            model.PropertyCountry = offprodt.PropertyCountry;

            model.PropertyContactNo = offprodt.PropertyContactNo;
            model.PropertyCaretakerName = offprodt.PropertyCaretakerName;
            model.PropertyEmail = offprodt.PropertyEmail;

            model.SupplierName = offprodt.SupplierName;
            model.SupplierAddress = offprodt.SupplierAddress;
            model.SupplierCity = offprodt.SupplierCity;
            model.SupplierCityname = offprodt.SupplierCityname;
            model.SupplierState = offprodt.SupplierState;
            model.SupplierCountry = offprodt.SupplierCountry;
            model.SupplierMobile = offprodt.SupplierMobile;
            model.SupplierEmail = offprodt.SupplierEmail;

            model.Accommodationid = offedit.Accommodationid;
            model.Accommodationtypeid = offedit.Accommodationtypeid;
            model.AccommodatoinType = offedit.AccommodatoinType;
            model.AccommodationTypeName = offedit.AccommodatoinTypename;
            model.StayCategoryid = offedit.StayCategoryid;
            model.CheckIn = Convert.ToString(offedit.CheckIn);
            model.CheckOut = Convert.ToString(offedit.CheckOut);
            model.NoOfNight = offedit.NoOfNight;
            model.NoOfRooms = offedit.NoOfRooms;
            model.NoOfPaxAdult = offedit.NoOfPaxAdult;
            model.NoOfPaxChild = offedit.NoOfPaxChild;

            model.TotalAmtForBuyPrice = offedit.TotalAmtForBuyPrice;
            model.AvgDailyRateBefreStaxForBuyPrice = offedit.AvgDailyRateBefreStaxForBuyPrice;
            model.StaxForBuyPrice = offedit.StaxForBuyPrice;
            model.BuyPriceforotherservicesForBuyprice = offedit.BuyPriceforotherservicesForBuyprice;
            model.StaxForotherBuyPrice = offedit.StaxForotherBuyPrice;
            model.TotalAmtotherForBuyPrice = offedit.TotalAmtotherForBuyPrice;

            model.TotalAmtForSalePrice = offedit.TotalAmtForSalePrice;
            model.AvgDailyRateBefreStaxForSalePrice = offedit.AvgDailyRateBefreStaxForSalePrice;
            model.StaxForSalePrice = offedit.StaxForSalePrice;
            model.BuyPriceforotherservicesForSalePrice = offedit.BuyPriceforotherservicesForSalePrice;
            model.StaxForotherForSalePrice = offedit.StaxForotherForSalePrice;
            model.TotalAmtotherForSalePrice = offedit.TotalAmtotherForSalePrice;
            model.TotalBuyPrice = offedit.TotalBuyPrice;
            model.TotalSalePrice = offedit.TotalSalePrice;
            model.OtherService = offedit.OtherService;
            model.sendmailtocustomer = offedit.sendmailtocustomer;
            model.sendmailtosupplier = offedit.sendmailtosupplier;
            model.SalesPersonId = offedit.SalesPersonId;
            model.MailContent = offedit.MailContent;
            model.HotelConformationNo = offedit.HotelConfirmationNo;
            model.HotelFacility = offedit.HotelFacility;


            model.LoadPlaces();

            List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list1 = new List<CLayer.TaxPercentage>();
            model.TaxPercentageList_Service = new List<Models.TaxPercentage>();
            model.TaxPercentageList_Others = new List<Models.TaxPercentage>();
            list = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(OfflineBookId, "Service Tax");
            list1 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(OfflineBookId, "Service Tax - Others");
            foreach (var item in list)
            {
                Models.TaxPercentage cls = new Models.TaxPercentage();
                cls.TaxPerID = item.TaxPerID;
                cls.TaxOfflineBookingID = item.TaxOfflineBookingID;
                cls.TaxTitle = item.TaxTitle;
                cls.TaxPercent = item.TaxPercent;
                cls.TaxType = item.TaxType;
                model.TaxPercentageList_Service.Add(cls);
            }
            foreach (var item in list1)
            {
                Models.TaxPercentage cls = new Models.TaxPercentage();
                cls.TaxPerID = item.TaxPerID;
                cls.TaxOfflineBookingID = item.TaxOfflineBookingID;
                cls.TaxTitle = item.TaxTitle;
                cls.TaxPercent = item.TaxPercent;
                cls.TaxType = item.TaxType;
                model.TaxPercentageList_Others.Add(cls);
            }

            return View("~/Areas/Admin/Views/OfflineBooking/OfflineBooking.cshtml", model);
        }
        public async Task<ActionResult> OfflineBookingFirst(Models.OfflineBookingModel model)
        {

            Models.OfflineBookingModel data = await SaveOfflineBooking(model);

            return RedirectToAction("OfflineEdit", new { OfflineBookId = data.OfflineBookingId });


        }

        //Send email for Offline Booking
        public async Task<bool> OfflineBookingConfirm(long OfflineBookId)
        {
            try
            {
                if (OfflineBookId < 1) return false;

                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookId);

                CLayer.OfflineBooking OfflinePropertydata = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookId);

                //CLayer.Address CustomerDetails = BLayer.Address.GetAddressOnUserId(Offlinedata.CustomerId);

                CLayer.OfflineBooking CustomerDetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookId);

                //  CLayer.Address SupplierDetails = BLayer.Address.GetAddressOnUserId(Offlinedata.SupplierId);
                //  CLayer.B2B SupplierData = BLayer.B2B.Get(Offlinedata.SupplierId);
                //  CLayer.Property PropertyData = BLayer.Property.Get(Offlinedata.PropertyId);

                if (CustomerDetails == null) return false;
                if (OfflinePropertydata == null) return false;

                CLayer.StayCategory Staycategorydetails = BLayer.StayCategory.Get(Convert.ToInt32(Offlinedata.StayCategoryid));
                CLayer.AccommodationType AccommodationTypedetails = BLayer.AccommodationType.Get(Convert.ToInt32(Offlinedata.Accommodationtypeid));

                CLayer.Role.Roles rle = BLayer.User.GetRole(Offlinedata.CustomerId);

                string message = "";
                Common.Mailer ml = new Common.Mailer();

                try
                {

                    if (Offlinedata.sendmailtocustomer == false)
                    {
                        //send email and sms to customer start

                        string link = System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingConfirmation") + OfflineBookId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK);
                        //for normal user mail body
                        message = await Common.Mailer.MessageFromHtml(link);
                        System.Net.Mail.MailMessage customerMsg = new System.Net.Mail.MailMessage();
                        //guest mail added
                        customerMsg.To.Add(CustomerDetails.CustomerEmail);

                        string BccEmailsforcususer = BLayer.Settings.GetValue(CLayer.Settings.CC_CUSTOMERCOMMUNICATION);
                        if (BccEmailsforcususer != "")
                        {
                            string[] emails = BccEmailsforcususer.Split(',');
                            for (int i = 0; i < emails.Length; ++i)
                            {
                                customerMsg.Bcc.Add(emails[i]);
                            }
                        }


                        if (Offlinedata != null)
                        {
                            if (Offlinedata.MailContent != "")
                            {
                                string BccBranchEmails = Offlinedata.MailContent;
                                if (BccBranchEmails != "")
                                {
                                    string[] emails = BccBranchEmails.Split(',');
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        customerMsg.Bcc.Add(emails[i]);
                                    }
                                }
                            }
                        }


                        customerMsg.Subject = "Booking Confirmation";
                        customerMsg.Body = message;

                        customerMsg.IsBodyHtml = true;
                        try
                        {
                            await ml.SendMailAsync(customerMsg, Common.Mailer.MailType.Reservation);
                        }
                        catch (Exception ex)
                        {
                            Common.LogHandler.HandleError(ex);
                        }
                    }
                    //if (Offlinedata.sendmailtocustomer == true)
                    //{
                    //    if (OfflineBookId < 1) return false;
                    //    if (CustomerDetails == null) return false;
                    //    if (OfflinePropertydata == null) return false;

                    //    try
                    //    {
                    //        string phone = CustomerDetails.CustomerMobile;

                    //        string smsmsg = Common.SMSGateway.GetNewBookingMessage(CustomerDetails.CustomerName, Offlinedata.CheckIn.ToString("MMM dd,yyyy"),
                    //            Offlinedata.CheckOut.ToString("MMM dd,yyyy"), OfflinePropertydata.PropertyName, OfflinePropertydata.PropertyCityname, AccommodationTypedetails.Title, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    //        bool b = false;
                    //        phone = Common.Utils.GetMobileNo(phone);

                    //        if (phone != "")
                    //        {
                    //            b = await Common.SMSGateway.Send(smsmsg, phone);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Common.LogHandler.HandleError(ex);
                    //    }

                    //    //send email and sms to customer end

                    //}






                    if (Offlinedata.sendmailtosupplier == false)
                    {

                        //send email and sms to supplier start
                        if (OfflineBookId < 1) return false;
                        try
                        {

                            if (OfflinePropertydata.PropertyEmail != "")
                            {
                                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupplierIntemationOfflineBook") + OfflineBookId.ToString() + "&key=" + BLayer.Settings.GetValue(CLayer.Settings.PUBLIC_PAGE_LOCK));
                                System.Net.Mail.MailMessage supplierMsg = new System.Net.Mail.MailMessage();

                                string BccEmailsforsupp = BLayer.Settings.GetValue(CLayer.Settings.CC_SUPPLIERCOMMUNICATION);
                                if (BccEmailsforsupp != "")
                                {
                                    string[] emails = BccEmailsforsupp.Split(',');
                                    for (int i = 0; i < emails.Length; ++i)
                                    {
                                        supplierMsg.Bcc.Add(emails[i]);
                                    }
                                }


                                if (Offlinedata != null)
                                {
                                    if (Offlinedata.MailContent != "")
                                    {
                                        string BccBranchEmails = Offlinedata.MailContent;
                                        if (BccBranchEmails != "")
                                        {
                                            string[] emails = BccBranchEmails.Split(',');
                                            for (int i = 0; i < emails.Length; ++i)
                                            {
                                                supplierMsg.Bcc.Add(emails[i]);
                                            }
                                        }
                                    }
                                }

                                supplierMsg.Subject = "Booking Confirmation";
                                supplierMsg.Body = message;

                                if (OfflinePropertydata.PropertyEmail != "")
                                {
                                    supplierMsg.To.Add(OfflinePropertydata.PropertyEmail);
                                }
                                supplierMsg.IsBodyHtml = true;

                                try
                                {
                                    await ml.SendMailAsync(supplierMsg, Common.Mailer.MailType.Reservation);
                                }
                                catch (Exception ex)
                                {
                                    Common.LogHandler.HandleError(ex);
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Common.LogHandler.HandleError(ex);
                        }
                    }

                    //if (Offlinedata.sendmailtosupplier == true)
                    //{
                    //    try
                    //    {
                    //        string phone = "";
                    //        bool b = false;
                    //        string smsmsg = "";
                    //        smsmsg = Common.SMSGateway.GetSupplierBookingMessage(CustomerDetails.CustomerName, "", Offlinedata.CheckIn.ToString("MMM dd,yyyy"),
                    //            Offlinedata.CheckOut.ToString("MMM dd,yyyy"), OfflinePropertydata.PropertyName, OfflinePropertydata.PropertyCityname, AccommodationTypedetails.Title, BLayer.Settings.GetValue(CLayer.Settings.STAYB_CONTACTNO));
                    //        phone = Common.Utils.GetMobileNo(OfflinePropertydata.SupplierMobile);
                    //        if (phone != "")
                    //        {

                    //            b = await Common.SMSGateway.Send(smsmsg, phone);
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        Common.LogHandler.HandleError(ex);
                    //    }
                    //    //send email and sms to supplier end
                    //}


                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            return true;
        }


        public ActionResult SetPricingDetailsforOffline(Models.OfflineBookingModel model)
        {
            try
            {
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                if (model.SupplierId == 0)
                {
                    model.SupplierId = model.SupplierIdforprop;
                }
                data.SupplierId = model.SupplierId;
                data.PropertyId = model.PropertyId;
                data.Accommodationid = model.Accommodationid;
                data.Accommodationtypeid = model.Accommodationtypeid;
                data.AccommodatoinType = model.AccommodatoinType;
                data.StayCategoryid = model.StayCategoryid;
                data.NoOfNight = model.NoOfNight;
                data.NoOfRooms = model.NoOfRooms;
                data.NoOfPaxAdult = model.NoOfPaxAdult;
                data.NoOfPaxChild = model.NoOfPaxChild;

                DateTime CheckInt = DateTime.MinValue;
                DateTime.TryParse(model.CheckIn, out CheckInt);

                DateTime CheckOutt = DateTime.MinValue;
                DateTime.TryParse(model.CheckOut, out CheckOutt);


                data.CheckIn = CheckInt;
                data.CheckOut = CheckOutt;


                CLayer.OfflineBooking retdata = BLayer.OfflineBooking.SetOfflinePricingDetails(data);
                Models.OfflineBookingModel model1 = new Models.OfflineBookingModel();
                model1.TotalAmtForBuyPrice = retdata.TotalAmtForBuyPrice;
                model1.AvgDailyRateBefreStaxForBuyPrice = retdata.AvgDailyRateBefreStaxForBuyPrice;
                model1.StaxForBuyPrice = retdata.StaxForBuyPrice;
                model1.BuyPriceforotherservicesForBuyprice = retdata.BuyPriceforotherservicesForBuyprice;
                model1.StaxForotherBuyPrice = retdata.StaxForotherBuyPrice;
                model1.TotalAmtotherForBuyPrice = retdata.TotalAmtotherForBuyPrice;

                model1.TotalAmtForSalePrice = retdata.TotalAmtForSalePrice;
                model1.AvgDailyRateBefreStaxForSalePrice = retdata.AvgDailyRateBefreStaxForSalePrice;
                model1.StaxForSalePrice = retdata.StaxForSalePrice;
                model1.BuyPriceforotherservicesForSalePrice = retdata.BuyPriceforotherservicesForSalePrice;
                model1.StaxForotherForSalePrice = retdata.StaxForotherForSalePrice;
                model1.TotalAmtotherForSalePrice = retdata.TotalAmtotherForSalePrice;

                return PartialView("~/Areas/Admin/Views/OfflineBooking/_PricingDetails.cshtml", model1);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/OfflineBooking/_PricingDetails.cshtml", new Models.OfflineBookingModel());
        }


        [HttpPost]
        public ActionResult Searchforofflinebookcustomers(string name)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            try
            {
                result = BLayer.B2B.SearchcustomerListlist(name);

                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/OfflineBooking/_B2BCustomerList.cshtml", result);
        }

        public long GetUserId()
        {
            long userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                long.TryParse(User.Identity.GetUserId(), out userId);
            }
            else
                if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] != null)
                {
                    userId = (long)Session[CLayer.ObjectStatus.GUEST_ID_SESSION];
                }
            return userId;
        }
        [AllowAnonymous]
        public string GetSalesRegion(long id)
        {
            string salesperson = "";
            if (id > 0)
            {
                salesperson = BLayer.User.GetSalesRegion(id);
            }
            return salesperson;
        }
        //[HttpPost]
        //public JsonResult GetCountries(string Prefix)
        //{

        //    var Countries = (from c in db.Countries
        //                     where c.Name.StartsWith(Prefix)
        //                     select new { c.Name, c.Id });
        //    return Json(Countries, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult GetCountries(string Prefix)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();

            result = BLayer.B2B.SearchcustomerListlist(Prefix);

            var Countries = (from c in result
                             where c.CustomerName.StartsWith(Prefix)
                             select new { c.CustomerName, c.CustomerId });
            return Json(Countries, JsonRequestBehavior.AllowGet);
        }

        public ActionResult BackToVendorDetails(long OfflineBookId)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            if (OfflineBookId > 0)
            {
                model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookId);
                model.OfflineBookingId = OfflineBookId;
            }
            return View("~/Areas/Admin/Views/OfflineBooking/Vendorindex.cshtml", model);
        }


        public long SaveOfflineCustomerToUser(string CustomerName1, string CustomerEmail1, string CustomerMobile1, string GuestName1, string GuestEmail1, int CustomerState1,
        string CustomerCityname, string CustomerAddress1, int CustomerType, string BillingAddress, int BillingCountryId, int BillingState, string PinCode, string BillingMobile,
        string BillingCityname, string ContactPerson, string OfficeNO, int CustomerCountry1  ,int CustomerCity1,string CustomerpinCode1,bool NoInvoiceMail1, int BillingCity=0)
        {
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            data.CustomerName = CustomerName1;
            data.CustomerEmail = CustomerEmail1;
            data.CustomerMobile = CustomerMobile1;
            data.CustomerCountry = CustomerCountry1;
            data.CustomerState = CustomerState1;
            data.CustomerCity = CustomerCity1;
            data.CustomerCityname = CustomerCityname;
            data.BillingCountryId = BillingCountryId;
            data.CustomerAddress = CustomerAddress1;
            data.CustomerType = CustomerType;
            data.BillingAddress = BillingAddress;
            data.BillingState = BillingState;
            data.BillingCity = BillingCity;
            data.PinCode = PinCode;
            data.BillingMobile = BillingMobile;
            data.ContactPerson = ContactPerson;
            data.OfficeNO = OfficeNO;
            data.GuestName1 = GuestName1;
            data.GuestEmail1 = GuestEmail1;
            data.BillingCityname = BillingCityname;
            data.CustomerpinCode = CustomerpinCode1;
        


            long OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomerNEW(data);
            
            //Update Noinvoicemail bool (name,email,usertype)
            data.CustomerName1 = CustomerName1;
            data.CustomerEmail1 = CustomerEmail1;
            data.NoInvoiceMail = NoInvoiceMail1;
            data.CustomerId = OfflineCustomerId;
            BLayer.OfflineBooking.UpdateNoinvoicemail(data);

            //CLayer.User usr = new CLayer.User()
            //{
            //    // UserId = 0,
            //    //  SalutationId = model.SalutationId,
            //    FirstName = CustomerName1,
            //    // LastName = model.LastName,
            //    //Businessname = ,
            //    Email = CustomerEmail1,
            //    Mobile = CustomerMobile1,
            //    //  Phone = model.Phone,
            //    //  ZipCode = model.Customerz,

            //    UserTypeId = CustomerType,
            //    Status = (int)CLayer.ObjectStatus.StatusType.Disabled
            //};
            //var users = BLayer.User.Save(usr);
            //if (users > 0)
            //{
            //    if (CustomerType == 7)
            //    {

            //        CLayer.Address adrs = new CLayer.Address()
            //        {
            //            UserId = users,
            //            AddressText = CustomerAddress1,
            //            CityId = CustomerCity1,
            //            State = BillingState,
            //            CountryId = CustomerCountry1,
            //            City = CustomerCityname,
            //            // Phone = CustomerMobile1,
            //            Mobile = CustomerMobile1,
            //            // ZipCode = PinCode,
            //            AddressType = (int)CLayer.Address.AddressTypes.Primary
            //        };


            //        BLayer.Address.Save(adrs);
            //    }
            //    if (CustomerType == 5)
            //    {
            //        CLayer.Address adrsbilling = new CLayer.Address()
            //        {
            //            UserId = users,
            //            AddressText = BillingAddress,
            //            CityId = BillingCity,
            //            City = BillingCityname,
            //            State = CustomerState1,
            //            CountryId = BillingCountryId,
            //            Phone = BillingMobile,
            //            Mobile = BillingMobile,
            //            ZipCode = PinCode,
            //            AddressType = (int)CLayer.Address.AddressTypes.Primary
            //        };
            //        //if (model.City != null && model.City != "")
            //        //    adrs.City = model.City;
            //        //if (model.CityId > 0)
            //        //    adrs.City = BLayer.City.Get(model.CityId).Name;
            //        BLayer.Address.Save(adrsbilling);

            //    }

            //}

            return OfflineCustomerId;
        }


        [AllowAnonymous]
        public ActionResult GetSupplier(string term)
        {
            var dummy = new List<object>();
            try
            {
                return Json(BLayer.OfflineBooking.GetSupplier(term), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                dummy.Add(new { value = "" });
            }
            return Json(dummy, JsonRequestBehavior.AllowGet);
        }
        [Common.AdminRolePermission(AllowAllRoles = true)]
        [Authorize]
        public ActionResult GetSupplierDetails(int id)
        {
            try
            {
                CLayer.OfflineBooking retdata = BLayer.OfflineBooking.GetSupplierDetails(id);

                return Json(retdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }


        [HttpPost]
        public JsonResult OfflineBookingAlreadyExistsChecking(string CustomerName, string GuestName, string PropertyName , DateTime CheckIn , DateTime CheckOut  , long SalesPersonId,long OfflineBookingId)

        {

            DateTime indate = Convert.ToDateTime(CheckIn);
            DateTime outdate = Convert.ToDateTime(CheckOut);

            string Checkin = indate.ToString("yyyy-MM-dd 00:00:00");  


            string Checkout = outdate.ToString("yyyy-MM-dd 00:00:00");



            var data = BLayer.OfflineBooking.OfflineBookingAlreadyExistsChecking(CustomerName, GuestName, PropertyName, Checkin, Checkout, SalesPersonId, OfflineBookingId);

            return Json(data.result, JsonRequestBehavior.AllowGet);
        }
        //public long StatusUpdateOfflineBookingEdit(long id)
        //{


        //    CLayer.OfflineBooking test =BLayer.OfflineBooking.StatusUpdateOfflineBookingEdit(id);

        //    //@using System.Linq
        //    //@{
        //    //    //Get all blocked permissions
        //    //    string email = User.Identity.GetUserName();
        //    //    int roleId = BLayer.User.GetRole(email);
        //    //    List<CLayer.RolePermission> perm = BLayer.RolePermissions.GetAllPermissions(roleId);
        //    //    bool canEditOfflineBooking = perm.Exists(m => m.ModuleId == StayBazar.Common.AdminRolePermission.OFFLINEBOOKING_EDIT && m.HasAccess);
        //    //}
        //    //@if(canEditOfflineBooking){ ...can now reduce no of days or dates.. }



        //    return test.result;



    }
}