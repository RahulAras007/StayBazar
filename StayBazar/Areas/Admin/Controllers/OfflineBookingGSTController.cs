using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StayBazar.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Script.Serialization;
using StayBazar.Areas.Admin.Models;
using StayBazar.Common;
using System.Net;
using System.IO;
using System.Xml;
using System.Data;
using System.Collections;
using System.Configuration;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Net.Mail;

namespace StayBazar.Areas.Admin.Controllers
{
    public class OfflineBookingGSTController : Controller
    {
        public const string TEXT_GST = "GST";
        public const string TEXT_BUYPRICE = "Buy price";
        public const string TEXT_GSTOTHERS = "GST - Others";
        public const string TEXT_SALEPRICE = "Sale price";
        public static List<CLayer.GDSCurrencyConversions> CurrencyConversionList = new List<CLayer.GDSCurrencyConversions>();


        #region Property variables
        private string RequestedCurrencyCode = "";
        private string PropertyCountryName = string.Empty;
        private string PropertyStateName = string.Empty;
        private string PropertyCityName = string.Empty;

        private int PropertyCountryID = 0;
        private int PropertyStateID = 0;
        private int PropertyCityID = 0;

        private string PropertyNoImage = string.Empty;
        #endregion

        public List<CLayer.RoomStaysResult> AmedusRoomStaysResultList = null;

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
            Session["offlineBookingMode"] = "ADD";

            SetBillingIds();
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            data.BookingDate = Convert.ToString(DateTime.Now);
            try
            {

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
                //data.SetBookingTypeTax(BLayer.OfflineBooking.GetDefaultBookingTypeTaxes());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(data);
        }

        [Common.AdminRolePermission]
        public async Task<Models.OfflineBookingModel> SaveOfflineBooking(Models.OfflineBookingModel model)
        {
            try
            {
                string email = User.Identity.GetUserName();
                int roleId = BLayer.User.GetRole(email);
                // save property as new entry (  custom property  )
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                //int count = BLayer.Property.GetPropertybyEmail(model.PropertyEmail);
                long userId = GetUserId();
                long CustomPropertyId = 0;
                if (roleId == 5)
                {
                    if (model.CorporateUserID1 == 0)
                    {
                        data.CreatedBy = userId;
                        data.AssistedBy = 0;
                        data.CorporateID = 0;
                    }
                    else
                    {
                        data.CreatedBy = userId;
                        data.AssistedBy = model.CorporateID1;
                        data.CorporateID = userId;
                    }
                }
                else
                {
                    if (model.CorporateID == 0)
                    {
                        data.CreatedBy = userId;
                        data.AssistedBy = 0;
                        data.CorporateID = 0;
                    }
                    else
                    {
                        data.CreatedBy = model.CorporateUserID;
                        data.AssistedBy = userId;
                        data.CorporateID = model.CorporateID;
                    }
                }
                DateTime BookingDate = DateTime.MinValue;
                DateTime.TryParse(model.BookingDate, out BookingDate);
                data.CreatedTime = BookingDate;
                data.OfflineBookingId = model.OfficeBookingID;
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
                //        model.PropertyId = 0DiscountAmount
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
                data.GuestPhone = model.GuestPhone;
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


                if (model.PropertyGstRegNo != "")
                {
                    if (data.PropertyId > 0)
                    {
                        string Gstregno = BLayer.OfflineBooking.GetPropertyGstRegNoByPropertyid(data.PropertyId, 1);
                        if (Gstregno == "")
                        {
                            BLayer.OfflineBooking.SavePropertyGstRegNoByPropertyid(data.PropertyId, 1, model.PropertyGstRegNo);
                        }
                    }
                    else if (data.CustomPropertyId > 0)
                    {
                        string Gstregno = BLayer.OfflineBooking.GetPropertyGstRegNoByPropertyid(data.CustomPropertyId, 2);
                        if (Gstregno == "")
                        {
                            BLayer.OfflineBooking.SavePropertyGstRegNoByPropertyid(data.CustomPropertyId, 2, model.PropertyGstRegNo);
                        }
                    }
                }

                long OfflineBookId = BLayer.OfflineBooking.SaveOfflineBookingDetails(data);



                if (OfflineBookId > 0)
                {
                    //check for any invoices
                    CLayer.Invoice indata = BLayer.Invoice.GetInvoiceByOfflineBooking(OfflineBookId);
                    if (indata != null)
                    {
                        BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookId, (int)CLayer.OfflineBooking.Statuslist.Save);
                        //invoice will be available only after approving booking
                        indata.HtmlSection1 = "";
                        indata.HtmlSection2 = "";
                        indata.HtmlSection3 = "";
                        indata.HtmlSection4 = "";
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

                    if (model.CategoryType == "user")
                    {
                        BLayer.OfflineBooking.RemoveCustomerEntry(OfflineBookId);
                        CLayer.OfflineBooking userdetails = new CLayer.OfflineBooking();

                        userdetails = BLayer.OfflineBooking.UserDetailsByOffliceBookingId(data);

                        CLayer.User usr = BLayer.User.Get(data.CustomerId);
                        if (usr == null)
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
            return View("~/Areas/Admin/Views/OfflineBookingGST/VendorPayment.cshtml", model);
        }

        //[Common.AdminRolePermission]
        //public async Task<ActionResult> SubmitOfflineBooking(Models.OfflineBookingModel dt)
        //{
        //    Models.OfflineBookingModel data = await SaveOfflineBookingGST(dt);
        //    Models.OfflineBookingModel data1 = new Models.OfflineBookingModel();
        //    data1.OfflineBookingId = data.OfflineBookingId;

        //   return View("~/Areas/Admin/Views/OfflineBookingGST/MultipleBooking.cshtml", data1);
        //}

        [Common.AdminRolePermission]
        public ActionResult ViewBookingData(long OfflineBookingId, int? Dir)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            data.Direction = Dir.Value;
            data.OfflineBookingId = OfflineBookingId;
            data.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);
            ViewBag.viewonly = "yes";
            return View("~/Areas/Admin/Views/OfflineBookingGST/BookingData.cshtml", data);
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
            data.PlaceOfSupply = model.PlaceOfSupply;
            data.valuebeforeservicetax = model.valuebeforeservicetax;
            data.servicetaxamount = model.servicetaxamount;
            data.totalamountpayable = model.totalamountpayable;

            if (data.OfflineBookingId > 0)
            {

                data.vendorId = BLayer.OfflineBooking.SaveVendorDetails(data);

                model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(data.OfflineBookingId);
            }
            TempData["OfflineBookSuccessMessage"] = "Booking details are saved";
            return View("~/Areas/Admin/Views/OfflineBookingGST/_VendorPaymentList.cshtml", model.Vendorlist);
        }


        public ActionResult NewVendorPayment(long Offlinebookingid)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            model.OfflineBookingId = Offlinebookingid;
            return View("~/Areas/Admin/Views/OfflineBookingGST/VendorPayment.cshtml", model);
        }
        //Done by rahul
        public ActionResult NewCostCentre(long OfflineBookId)
        {
            List<CLayer.OfflineBooking> costcentre = BLayer.OfflineBooking.GetofflinebookingCostCentre(OfflineBookId);
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            data.costcentre = costcentre;
            data.OfflineBookingId = OfflineBookId;
            return View("~/Areas/Admin/Views/OfflineBookingGST/NewCostCentre.cshtml", data);
        }
        public ActionResult SaveCostCentreDetails(Models.OfflineBookingModel model)
        {
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            data.OfflineBookingId = model.OfflineBookingId;
            data.CostCentreCode = model.CostCenter_Codee;
            data.CostCentrePercentage = model.CostCentrePercentage;
            List<CLayer.OfflineBooking> costcentre = BLayer.OfflineBooking.GetofflinebookingCostCentre(data.OfflineBookingId);
            ViewBag.Total = costcentre.Sum(m => m.CostCentrePercentage);
            if (ViewBag.Total <= 100 && (model.CostCentrePercentage + ViewBag.Total) <= 100)
            {
                data.CostCentreID = BLayer.OfflineBooking.SaveCostCentre(data);
                return RedirectToAction("NewCostCentre", new { OfflineBookId = data.OfflineBookingId });
            }
            TempData["Message"] = "Data cannot be Saved because sum of Percentage is greater than 100";
            return RedirectToAction("NewCostCentre", new { OfflineBookId = data.OfflineBookingId });
        }
        public ActionResult DeleteCostCentreDetils(int Id, long Id1)
        {
            BLayer.OfflineBooking.DeleteCostCentre(Id);
            return RedirectToAction("NewCostCentre", new { OfflineBookId = Id1 });
        }
        public ActionResult EditCostCentreDetails(long OfflineBookingId, int CostCentreId)
        {
            List<CLayer.OfflineBooking> costcentre = BLayer.OfflineBooking.GetID_offlinebookingCostCentre(CostCentreId);
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            data.costcentre = costcentre;
            data.CostCentre_ID = CostCentreId;
            data.OfficeBookingID = OfflineBookingId;
            return View("~/Areas/Admin/Views/OfflineBookingGST/EditCostCentreDetails.cshtml", data);
        }
        public ActionResult SaveEditCostCentreDetails(long OfflineBookingId, int CostCentreId, Models.OfflineBookingModel model)
        {
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            data.CostCentre_ID = CostCentreId;
            data.OfflineBookingId = OfflineBookingId;
            data.CostCentreCode = model.CostCenter_Codee;
            data.CostCentrePercentage = model.CostCentrePercentage;
            List<CLayer.OfflineBooking> costcentre = BLayer.OfflineBooking.GetofflinebookingCostCentre(data.OfflineBookingId);
            ViewBag.Total = costcentre.Sum(m => m.CostCentrePercentage);
            List<CLayer.OfflineBooking> costcentre1 = BLayer.OfflineBooking.GetID_offlinebookingCostCentre(CostCentreId);
            ViewBag.Total1 = costcentre1.Sum(m => m.CostCentrePercentage);
            if (ViewBag.Total <= 100 && ((ViewBag.Total - ViewBag.Total1) + model.CostCentrePercentage) <= 100)
            {
                data.CostCentreID = BLayer.OfflineBooking.SaveEditCostCentre(CostCentreId, data);
                return RedirectToAction("NewCostCentre", new { OfflineBookId = OfflineBookingId });
            }
            return RedirectToAction("NewCostCentre", new { OfflineBookId = OfflineBookingId });
        }
        public ActionResult VendorPaymentSubmit(Models.OfflineBookingModel model)
        {

            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            data.vendorId = model.vendorId;
            data.OfflineBookingId = model.OfflineBookingId;
            data.VendorpaymentsId = model.VendorpaymentsId;
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
            data.ByPriceBeforeTax = model.ByPriceBeforeTax;
            data.SalePriceGST = model.SalePriceGST;
            data.ByPriceTotal = model.ByPriceTotal;
            data.SalePriceBeforeTax = model.SalePriceBeforeTax;
            data.ByPriceGST = model.ByPriceGST;
            data.SalePriceTotal = model.SalePriceTotal;
            data.HSNCodeCodeID = model.HSNCode;
            data.PlaceOfSupply = model.PlaceOfSupply;

            if (data.OfflineBookingId > 0)
            {
                if (model.vendorname != null && model.vendorname != "")
                {
                    data.vendorId = BLayer.OfflineBooking.SaveVendorDetails(data);
                }

                //model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(data.OfflineBookingId);
            }

            //tax save in venvendor tax table 
            //if (model.TaxPercentageList_ServiceByPrice != null && model.TaxPercentageList_ServiceByPrice.Count() > 0)
            //{
            //    List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            //    foreach (Models.TaxPercentage item in model.TaxPercentageList_ServiceByPrice)
            //    {
            //        CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
            //        cls.TaxPerID = item.TaxPerID;
            //        cls.TaxOfflineBookingID = model.OfflineBookingId;
            //        cls.TaxTitle = item.TaxTitle;
            //        cls.TaxPercent = item.TaxPercent;
            //        cls.TaxType = "GST";
            //        cls.Type = "Buy price";
            //        cls.vendorId = data.vendorId;
            //        list.Add(cls);
            //    }
            //    BLayer.OfflineBooking.SaveVendotTax(list);
            //}
            //if (model.TaxPercentageList_OthersByPrice != null && model.TaxPercentageList_OthersByPrice.Count() > 0)
            //{
            //    List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            //    foreach (Models.TaxPercentage item in model.TaxPercentageList_OthersByPrice)
            //    {
            //        CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
            //        cls.TaxPerID = item.TaxPerID;
            //        cls.TaxOfflineBookingID = model.OfflineBookingId;
            //        cls.TaxTitle = item.TaxTitle;
            //        cls.TaxPercent = item.TaxPercent;
            //        cls.TaxType = "GST - Others";
            //        cls.Type = "Buy price";
            //        cls.vendorId = data.vendorId;
            //        list.Add(cls);
            //    }
            //    BLayer.OfflineBooking.SaveVendotTax(list);
            //}



            ////sale Price breakUp
            //if (model.TaxPercentageList_ServiceSalePrice != null && model.TaxPercentageList_ServiceSalePrice.Count() > 0)
            //{
            //    List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            //    foreach (Models.TaxPercentage item in model.TaxPercentageList_ServiceSalePrice)
            //    {
            //        CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
            //        cls.TaxPerID = item.TaxPerID;
            //        cls.TaxOfflineBookingID = model.OfflineBookingId;
            //        cls.TaxTitle = item.TaxTitle;
            //        cls.TaxPercent = item.TaxPercent;
            //        cls.TaxType = "GST";
            //        cls.Type = "Sale price";
            //        cls.vendorId = data.vendorId;
            //        list.Add(cls);
            //    }
            //    BLayer.OfflineBooking.SaveVendotTax(list);
            //}
            //if (model.TaxPercentageList_OthersSalePrice != null && model.TaxPercentageList_OthersSalePrice.Count() > 0)
            //{
            //    List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            //    foreach (Models.TaxPercentage item in model.TaxPercentageList_OthersSalePrice)
            //    {
            //        CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
            //        cls.TaxPerID = item.TaxPerID;
            //        cls.TaxOfflineBookingID = model.OfflineBookingId;
            //        cls.TaxTitle = item.TaxTitle;
            //        cls.TaxPercent = item.TaxPercent;
            //        cls.TaxType = "GST - Others";
            //        cls.Type = "Sale price";
            //        cls.vendorId = data.vendorId;
            //        list.Add(cls);
            //    }
            //    BLayer.OfflineBooking.SaveVendotTax(list);
            //}


            Models.OfflineBookingModel MultipleBooking = new Models.OfflineBookingModel();
            MultipleBooking.OfflineBookingId = model.OfflineBookingId;
            MultipleBooking.vendorId = 0;
            MultipleBooking.vendorname = "";
            MultipleBooking.vendoraddress = "";
            MultipleBooking.address1 = "";
            MultipleBooking.address2 = "";
            MultipleBooking.City = "";
            MultipleBooking.pin = "";
            MultipleBooking.ContactPerson = "";
            MultipleBooking.contactno = "";
            MultipleBooking.emailaddress = "";
            MultipleBooking.natureofservice = "";
            MultipleBooking.ByPriceBeforeTax = 0;
            MultipleBooking.SalePriceGST = 0;
            MultipleBooking.ByPriceTotal = 0;
            MultipleBooking.SalePriceBeforeTax = 0;
            MultipleBooking.ByPriceGST = 0;
            MultipleBooking.SalePriceTotal = 0;

            return View("~/Areas/Admin/Views/OfflineBookingGST/_VendorDetails.cshtml", MultipleBooking);

        }


        public ActionResult DeleteVendorDetails(long OfflineBookingId, long VendorId)
        {

            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();

            BLayer.OfflineBooking.DeleteVendorDetails(VendorId);
            model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookingId);

            return View("~/Areas/Admin/Views/OfflineBookingGST/_VendorPaymentList.cshtml", model.Vendorlist);
        }
        //public async Task<bool> OverollSubmit(long OfflineBookingId,int SID, Models.OfflineBookingModel model,FormCollection frm) changed for 4 th page summit is not working 
        public async Task<bool> OverollSubmit(long OfflineBookingId, Models.OfflineBookingModel model)
        {
            if (CheckGrossmarginless(OfflineBookingId))
            {
                if (BLayer.OfflineBooking.GetOfflineSaveStatus(OfflineBookingId) != (int)CLayer.OfflineBooking.Statuslist.Draft)
                {
                    //Send gross margin less issue to  approving authority
                    await DraftBookingRequestAlertMail(OfflineBookingId);
                }
                BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Draft);

                BLayer.OfflineBooking.UpdateDraftbookingStatus(OfflineBookingId, (int)CLayer.OfflineBooking.DraftbookingStatus.Draft);
            }
            else
            {
                BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Submit);
            }
            //change status to submit


            //Add/Update other amounts 
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            data.OfflineBookingId = OfflineBookingId;
            data.ReimbursementsAmount = Math.Round(model.ReimbursementsAmount, 2);
            data.natureofreimbursements = model.natureofreimbursements;
            data.OthersAmount = Math.Round(model.OthersAmount, 2);
            data.DiscountAmount = Math.Round(model.DiscountAmount, 2);
            data.AdvanceReceived = Math.Round(model.AdvanceReceived, 2);

            BLayer.OfflineBooking.UpdateOtherAmountsForOfflineBooking(data);


            //To update supplier payment schedule in offline booking for GST
            //List<CLayer.SupplierPaymentSchedule> dataSupplierList = new List<CLayer.SupplierPaymentSchedule>();
            //decimal TotalSupplierPaymentAmount = 0;
            //for(int i=1;i<2;i++)
            //{
            //    string SupplierPaymentDateID = "dtSupplierPaymentDate" + i;
            //    string SupplierPaymentAmountID = "SupplierPaymentAmount" + i;

            //    CLayer.SupplierPaymentSchedule dataSupplier = new CLayer.SupplierPaymentSchedule();
            //    dataSupplier.BookingId = Convert.ToInt32(OfflineBookingId);
            //    dataSupplier.SupplierPaymentMode = Convert.ToInt32(frm.Get("SupplierPaymentMode"));
            //    dataSupplier.SupplierPaymentModeDate = Convert.ToDateTime(frm.Get(SupplierPaymentDateID));
            //    dataSupplier.SupplierPaymentAmount = Convert.ToDecimal(frm.Get("SupplierPaymentAmountID"));
            //    dataSupplierList.Add(dataSupplier);

            //    TotalSupplierPaymentAmount = TotalSupplierPaymentAmount + dataSupplier.SupplierPaymentAmount;
            //}
            //decimal TamountForBuyPrice = model.TotalAmtForBuyPrice;

            //if(TotalSupplierPaymentAmount== TamountForBuyPrice)
            //{
            //    BLayer.OfflineBooking.UpdateSupplierPaymentModeForOfflineBooking(dataSupplierList);
            //}
            //else
            //{
            //    TempData["msg"] = "<script>alert('Total Supplier Payment should be equal to total amount');</script>";
            //    return false;
            //}





            return true;

        }

        public async Task<ActionResult> OverollSubmitWithSupplier(long OfflineBookingId, int SupplierPaymentMode, string SupplierPaymentDate1, decimal SupplierPaymentAmt1, string SupplierPaymentDate2, decimal SupplierPaymentAmt2, string SupplierPaymentDate3, decimal SupplierPaymentAmt3, Models.OfflineBookingModel model, FormCollection frm)
        {
            string Status = string.Empty;
            try
            {
                if (CheckGrossmarginless(model.OfflineBookingId))
                {
                    if (BLayer.OfflineBooking.GetOfflineSaveStatus(OfflineBookingId) != (int)CLayer.OfflineBooking.Statuslist.Draft)
                    {
                        //Send gross margin less issue to  approving authority
                        await DraftBookingRequestAlertMail(OfflineBookingId);
                    }
                    BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Draft);

                    BLayer.OfflineBooking.UpdateDraftbookingStatus(OfflineBookingId, (int)CLayer.OfflineBooking.DraftbookingStatus.Draft);
                }
                else
                {
                    BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Submit);
                }
                //change status to submit


                //Add/Update other amounts 
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                data.OfflineBookingId = OfflineBookingId;
                data.ReimbursementsAmount = Math.Round(model.ReimbursementsAmount, 2);
                data.natureofreimbursements = model.natureofreimbursements;
                data.OthersAmount = Math.Round(model.OthersAmount, 2);
                data.DiscountAmount = Math.Round(model.DiscountAmount, 2);
                data.AdvanceReceived = Math.Round(model.AdvanceReceived, 2);

                BLayer.OfflineBooking.UpdateOtherAmountsForOfflineBooking(data);


                //To update supplier payment schedule in offline booking for GST

                if (SupplierPaymentMode > 0)
                {
                    List<CLayer.SupplierPaymentSchedule> dataSupplierList = new List<CLayer.SupplierPaymentSchedule>();
                    DateTime SupplierPaymentDate = new DateTime();
                    decimal SupplierPaymentAmount = 0;
                    decimal TotalSupplierPaymentAmount = 0;
                    for (int i = 1; i <= 3; i++)
                    {

                        string SupplierPaymentDateID = "dtSupplierPaymentDate" + i;
                        string SupplierPaymentAmountID = "txtSupplierPaymentAmount" + i;

                        switch (i)
                        {
                            case 1:
                                SupplierPaymentDate = GetSupplierDateTime(SupplierPaymentDate1);
                                SupplierPaymentAmount = SupplierPaymentAmt1;
                                break;
                            case 2:
                                SupplierPaymentDate = GetSupplierDateTime(SupplierPaymentDate2);
                                SupplierPaymentAmount = SupplierPaymentAmt2;
                                break;
                            case 3:
                                SupplierPaymentDate = GetSupplierDateTime(SupplierPaymentDate3);
                                SupplierPaymentAmount = SupplierPaymentAmt3;
                                break;

                        }

                        CLayer.SupplierPaymentSchedule dataSupplier = new CLayer.SupplierPaymentSchedule();
                        dataSupplier.BookingId = Convert.ToInt32(OfflineBookingId);
                        dataSupplier.SupplierPaymentMode = SupplierPaymentMode;
                        dataSupplier.SupplierPaymentModeDate = SupplierPaymentDate;
                        dataSupplier.SupplierPaymentAmount = SupplierPaymentAmount;
                        dataSupplier.SupplierCreditDays = 0;
                        if (dataSupplier.SupplierPaymentAmount > 0)
                        {
                            dataSupplierList.Add(dataSupplier);
                        }


                        TotalSupplierPaymentAmount = TotalSupplierPaymentAmount + dataSupplier.SupplierPaymentAmount;
                    }


                    decimal TamountForBuyPrice = BLayer.OfflineBooking.GetOfflineTotalAmountForBuyPrice(OfflineBookingId);//model.TotalAmtForBuyPrice;


                    if (TotalSupplierPaymentAmount == TamountForBuyPrice)
                    {
                        BLayer.OfflineBooking.DeleteSupplierPaymentModeForOfflineBooking(Convert.ToInt32(OfflineBookingId));
                        BLayer.OfflineBooking.UpdateSupplierPaymentModeForOfflineBooking(dataSupplierList);
                        Status = "Supplier payment mode updated";
                    }
                    else
                    {
                        Status = "Total Supplier Payment should be equal to total amount- Rs." + TamountForBuyPrice.ToString();
                    }

                }
                return Content(Status);
            }
            catch (Exception ex)
            {
                throw ex;


            }


        }
        public async Task<ActionResult> OverollSubmitWithSupplierPaymentID(long OfflineBookingId, int SupplierPaymentMode, int SupplierCreditDays, Models.OfflineBookingModel model, FormCollection frm, string Status = "")
        {
            try
            {
                if (CheckGrossmarginless(model.OfflineBookingId))
                {
                    if (BLayer.OfflineBooking.GetOfflineSaveStatus(OfflineBookingId) != (int)CLayer.OfflineBooking.Statuslist.Draft)
                    {
                        //Send gross margin less issue to  approving authority
                        await DraftBookingRequestAlertMail(OfflineBookingId);
                    }
                    BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Draft);

                    BLayer.OfflineBooking.UpdateDraftbookingStatus(OfflineBookingId, (int)CLayer.OfflineBooking.DraftbookingStatus.Draft);
                }
                else
                {
                    BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Submit);
                }
                //change status to submit

                //#region Amadeus
                //int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(model.PropertyId);
                //Session["InventoryAPIType"] = InventoryAPIType;
                //if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                //{
                //    #region HOTEL SELL
                //    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELSELLACTION); ;

                //    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                //    srch.HotelCode = HotelId;



                //    List<CLayer.RoomStaysResult> RoomStaysResultList = APIUtility.AmedusHotelRoomStaysResultList;

                //    RoomStaysResultList = (List<CLayer.RoomStaysResult>)TempData["RoomStaysResult"];
                //    TempData.Keep("RoomStaysResult");
                //    string SoapHeaderStateful = string.Empty;
                //    foreach (var item in RoomStaysResultList)
                //    {
                //        string BookingCode = item.BookingCode;
                //        string HotelSellSoapBody = APIUtility.Hotel_Sell(HotelId, BookingCode);
                //        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                //        string result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);



                //        if (APIUtility.ReadGDSError(result, (int)CLayer.ObjectStatus.GDSType.HotelSell) == "UNABLE TO PROCESS")
                //        {
                //            result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);
                //        }
                //        #region Transaction Log 

                //        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSell, bookingId);

                //        #endregion Transaction log end

                //        if (!APIUtility.CheckHotelBookingErrorExists(result))
                //        {
                //            Serializer HotelSell = new Serializer();
                //            CLayer.HotelSell.Envelope HotelSellResult = HotelSell.Deserialize<CLayer.HotelSell.Envelope>(result);

                //            Session["HotelSellStatus"] = "success";
                //            Session["SessionId"] = HotelSellResult.Header.Session.SessionId;
                //            Session["SequenceNumber"] = Convert.ToInt32(HotelSellResult.Header.Session.SequenceNumber);
                //            Session["SecurityToken"] = HotelSellResult.Header.Session.SecurityToken;
                //            BookingStatus = false;
                //        }
                //        else
                //        {
                //            Serializer HotelSellError = new Serializer();
                //            CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(result);
                //            Session["HotelSellStatus"] = "error";
                //            Session["SessionId"] = HotelSellErrorResult.Header.Session.SessionId;
                //            Session["SequenceNumber"] = Convert.ToInt32(HotelSellErrorResult.Header.Session.SequenceNumber);
                //            Session["SecurityToken"] = HotelSellErrorResult.Header.Session.SecurityToken;
                //            ViewBag.HotelSellResult = "RequestFailed";
                //            TempData["Errorcomes"] = "RequestFailed";

                //            List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCode);
                //            foreach (var itemerror in objBookItemsError)
                //            {
                //                BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, result);
                //            }


                //            #region PNR CANCEL

                //            StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                //            //   BookingStatus = objBooking.BookingDecline(bookingId);

                //            BookingStatus = objBooking.BookingCancel(bookingId);

                //            //  string resultCancel = APIUtility.ExecutePNRCancel("", (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRGeneration);

                //            BookingStatus = true;
                //            return BookingStatus;
                //            #endregion

                //        }



                //    }
                //    #endregion

                //    #region PNR MULTIELEMENTS-FINAL
                //    Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION);
                //    SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                //    APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                //    string BookingCodeFinal = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                //    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCodeFinal, "test@test.com", 10);

                //    TempData["RoomStaysResult"] = RoomStaysResultList;
                //    APIUtility.AmedusHotelRoomStaysResultList = RoomStaysResultList;

                //    string resultFinal = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);

                //    #region Transaction Log 

                //    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElementsConfirm, bookingId);

                //    #endregion Transaction log end

                //    Serializer pnrser = new Serializer();

                //    CLayer.PNRAddElementsConfirm.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElementsConfirm.Envelope>(resultFinal);
                //    Session["ControlNumber"] = PNRAddResult.Body.PNR_Reply.pnrHeader.reservationInfo.reservation.controlNumber;
                //    //if(string.IsNullOrEmpty(Convert.ToString(Session["ControlNumber"])))
                //    //{
                //    //    Session["ControlNumber"]= PNRAddResult.Body.PNR_Reply.originDestinationDetails.originDestination.
                //    //}

                //    if (APIUtility.CheckHotelBookingErrorExists(resultFinal))
                //    {

                //        //Serializer HotelSellError = new Serializer();
                //        //CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(resultFinal);

                //        Serializer PNRADDError = new Serializer();
                //        CLayer.PNRAddElementsError.Envelope PNRAddErrorResult = null;
                //        CLayer.PNRAddElementsConfirmError.Envelope PNRAddErrorResultConfirm = null;

                //        try
                //        {
                //            PNRAddErrorResult = PNRADDError.Deserialize<CLayer.PNRAddElementsError.Envelope>(resultFinal);
                //        }
                //        catch (Exception ex)
                //        {
                //            PNRAddErrorResultConfirm = PNRADDError.Deserialize<CLayer.PNRAddElementsConfirmError.Envelope>(resultFinal);
                //        }


                //        Session["HotelSellStatus"] = "error";
                //        if (PNRAddErrorResult != null)
                //        {
                //            Session["SessionId"] = PNRAddErrorResult.Header.Session.SessionId;
                //            Session["SequenceNumber"] = Convert.ToInt32(PNRAddErrorResult.Header.Session.SequenceNumber);
                //            Session["SecurityToken"] = PNRAddErrorResult.Header.Session.SecurityToken;
                //            Session["ControlNumber"] = PNRAddErrorResult.Body.PNR_Reply.originDestinationDetails.itineraryInfo.generalOption.Where(x => x.optionDetail.type == "CF").FirstOrDefault();
                //        }
                //        else
                //        {
                //            Session["SessionId"] = PNRAddErrorResultConfirm.Header.Session.SessionId;
                //            Session["SequenceNumber"] = Convert.ToInt32(PNRAddErrorResultConfirm.Header.Session.SequenceNumber);
                //            Session["SecurityToken"] = PNRAddErrorResultConfirm.Header.Session.SecurityToken;
                //            Session["ControlNumber"] = PNRAddErrorResultConfirm.Body.PNR_Reply.originDestinationDetails.itineraryInfo.generalOption.Where(x => x.optionDetail.type == "CF").FirstOrDefault();
                //        }

                //        List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCodeFinal);
                //        foreach (var itemerror in objBookItemsError)
                //        {
                //            BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, resultFinal);
                //        }

                //        #region PNR CANCEL
                //        ViewBag.HotelSellResult = "RequestFailed";
                //        TempData["Errorcomes"] = "RequestFailed";
                //        string ControlNumber = Convert.ToString(Session["ControlNumber"]);
                //        // string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize);
                //        //     return false;
                //        StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                //        //  BookingStatus = objBooking.BookingDecline(bookingId);
                //        BookingStatus = objBooking.BookingCancel(bookingId);
                //        BookingStatus = true;
                //        return BookingStatus;
                //        #endregion

                //    }
                //    else
                //    {
                //        BookingStatus = false;

                //        #region PNR_Retrieve
                //        Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELPNRRETRIEVEACTION);
                //        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                //        APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                //        string PNRRetrieveSoapBody = APIUtility.PNR_Retrieve(Convert.ToString(Session["ControlNumber"]));
                //        string resultRetrieve = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRRetrieveSoapBody);
                //        #region Transaction Log 

                //        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRRetrieve, bookingId);

                //        #endregion Transaction log end
                //        #endregion

                //        //#region Hotel Completereservationinfodetails
                //        //Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELCOMPLETERESERVATION);
                //        //SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"])+1, Session["SecurityToken"].ToString(), true);
                //        //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                //        //string HotelCompleteReservation = APIUtility.Hotel_CompleteReservationDetails(Convert.ToString(Session["ControlNumber"]));
                //        //string resultotelCompleteReservation = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, HotelCompleteReservation);


                //        //#endregion 
                //    }

                //    #endregion
                //    //List<CLayer.BookingItem> objBookItems = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCodeFinal);
                //    //foreach (var item in objBookItems)
                //    //{
                //    //    BLayer.BookingItem.UpdateHotelConfirmNumber(item.BookingId, item.AccommodationId, Convert.ToString(Session["ControlNumber"]));
                //    //    #region Update Converted Amount details
                //    //    if (Session["GDSCurrencyConversion"] != null)
                //    //    {

                //    //        long CountryCode = Convert.ToInt64(Session["GDSCountryID"]);
                //    //        CLayer.GDSCurrencyConversions objCurrency = (CLayer.GDSCurrencyConversions)Session["GDSCurrencyConversion"];
                //    //        CLayer.BookingItem objBookingItem = new CLayer.BookingItem();
                //    //        objBookingItem.BookingId = item.BookingId;
                //    //        objBookingItem.AccommodationId = item.AccommodationId;
                //    //        objBookingItem.GDSAmount = Convert.ToDecimal(Session["GDSAmount"]);
                //    //        objBookingItem.GDSConvertedAmount = Convert.ToDecimal(Session["GDSConvertedAmount"]);
                //    //        objBookingItem.GDSConversionRate = objCurrency.RateConversion;
                //    //        objBookingItem.GDSCountry = CountryCode;

                //    //        BLayer.BookingItem.UpdateGDSCurrencyDetails(objBookingItem);

                //    //    }
                //    //    #endregion
                //    //}



                //}

                //#endregion


                //Add/Update other amounts 
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                data.OfflineBookingId = OfflineBookingId;
                data.ReimbursementsAmount = Math.Round(model.ReimbursementsAmount, 2);
                data.natureofreimbursements = model.natureofreimbursements;
                data.OthersAmount = Math.Round(model.OthersAmount, 2);
                data.DiscountAmount = Math.Round(model.DiscountAmount, 2);
                data.AdvanceReceived = Math.Round(model.AdvanceReceived, 2);


                BLayer.OfflineBooking.UpdateOtherAmountsForOfflineBooking(data);


                //To update supplier payment schedule in offline booking for GST

                if (SupplierPaymentMode > 0)
                {
                    if (SupplierPaymentMode < 4)
                    {
                        List<CLayer.SupplierPaymentSchedule> dataSupplierList = new List<CLayer.SupplierPaymentSchedule>();
                        DateTime SupplierPaymentDate = new DateTime();


                        CLayer.SupplierPaymentSchedule dataSupplier = new CLayer.SupplierPaymentSchedule();
                        dataSupplier.BookingId = Convert.ToInt32(OfflineBookingId);
                        dataSupplier.SupplierPaymentMode = SupplierPaymentMode;
                        dataSupplier.SupplierPaymentModeDate = SupplierPaymentDate;
                        dataSupplier.SupplierPaymentAmount = 0;
                        dataSupplier.SupplierCreditDays = (SupplierPaymentMode == 1) ? SupplierCreditDays : 0;

                        dataSupplierList.Add(dataSupplier);

                        BLayer.OfflineBooking.DeleteSupplierPaymentModeForOfflineBooking(Convert.ToInt32(OfflineBookingId));
                        BLayer.OfflineBooking.UpdateSupplierPaymentModeForOfflineBooking(dataSupplierList);
                        return Content("Supplier payment mode updated");

                    }

                }
                return Content("Supplier payment mode updated");

            }
            catch (Exception ex)
            {
                throw ex;

            }


        }
        //Added by rahul for Booking Approval on 04-02-2020
        public async Task<ActionResult> IsApproverRequiredForOfflineBooking(long OfflineBookingId)
        {
            long userid = GetUserId();
            if (IsBookingApprovalNeeded(userid, OfflineBookingId))
            {
                BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.WaitingForApproval);
                return Content("Approver Needed");
            }
            else
            {

                BLayer.OfflineBooking.UpdateSaveStatus(OfflineBookingId, (int)CLayer.OfflineBooking.Statuslist.Submit);
                return Content("Approver Not Needed");
            }
        }
        //Added by rahul for Booking Approval on 04-02-2020
        public bool IsBookingApprovalNeeded(long userId, long bookingId)
        {
            bool IsApproverNeeded = false;
            int NoofApprovers = BLayer.B2BUser.GetNoofApprovers(userId);
            if (NoofApprovers > 0)
            {
                CLayer.B2BApprovers pb2b_Approvers = BLayer.B2BUser.CheckBookingApproversExist(userId, bookingId);

                if (pb2b_Approvers.b2b_approver_id > 0)
                {
                    IsApproverNeeded = false;
                }
                else
                {
                    IsApproverNeeded = true;
                }
            }
            else
            {
                IsApproverNeeded = false;
            }
            return IsApproverNeeded;
        }
        //Added by rahul for Booking Approval on 04-02-2020
        public async Task<ActionResult> offlinebookingproceed(long BookingID)
        {

            try
            {
                CLayer.User userdt = new CLayer.User();
                try
                {
                    long userid = GetUserId();
                    userdt = BLayer.User.Get(userid);
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                }

                string message = "";
                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("OfflinebookingQuery") + BookingID.ToString());
                MailMessage mm1 = new MailMessage();

                if (userdt != null)
                {
                    if (userdt.Email != null && userdt.Email != "")
                    {
                        mm1.To.Add(userdt.Email);
                    }
                }
                string emailidsbcc1 = ConfigurationManager.AppSettings.Get("OfflineProceedMailBcc");
                if (emailidsbcc1 != "")
                {
                    string[] emailsbcc1 = emailidsbcc1.Split(',');
                    for (int i = 0; i < emailsbcc1.Length; ++i)
                    {
                        mm1.CC.Add(emailsbcc1[i]);
                    }
                }

                mm1.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                mm1.Subject = "Booking Query";
                mm1.Body = message;
                mm1.IsBodyHtml = true;
                Common.Mailer mlr1 = new Common.Mailer();

                try
                {
                    await mlr1.SendMailAsyncWithoutFields(mm1);
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
            return View();
        }

        //--------------END--------------
        private DateTime GetSupplierDateTime(string pDate)
        {
            DateTime dt = new DateTime();
            dt = Convert.ToDateTime(pDate);

            return dt;
        }
        public ActionResult OfflineEdit(long OfflineBookId)
        {
            Session["offlineBookingMode"] = "EDIT";

            SetBillingIds();
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            CLayer.OfflineBooking offedit = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookId);
            if (offedit.PayeeID != 0)
            {
                CLayer.OfflineBooking payeedetails = BLayer.OfflineBooking.GetPayeeDetails(offedit.PayeeID);
                model.PayeeID = offedit.PayeeID;
                model.PayeeName = payeedetails.PayeeName;
                model.AmountPayable = payeedetails.AmountPayable;
                model.AccountNumber = payeedetails.AccountNumber;
                model.IFSCcode = payeedetails.IFSCcode;
                model.BankName = payeedetails.BankName;
                model.BranchName = payeedetails.BranchName;
                model.PAN = payeedetails.PAN;


            }

            CLayer.OfflineBooking customerdetails = BLayer.OfflineBooking.GetAllCustomerDetails(OfflineBookId);

            CLayer.OfflineBooking offprodt = BLayer.OfflineBooking.GetAllpropertyDetails(OfflineBookId);

            model.CustomerGstStateId = offedit.CustomerGstStateId;
            model.CustomerGstRegNo = offedit.CustomerGstRegNo;
            model.PropertyGstRegNo = offedit.PropertyGstRegNo;

            model.FromCustomer = offedit.FromCustomer;
            model.FromCustomerId = offedit.FromCustomerId;

            model.CustomerType = customerdetails.CustomerType;
            model.UserTypeName = Enum.GetName(typeof(CLayer.Role.Roles), customerdetails.CustomerType);
            model.OfficeBookingID = OfflineBookId;
            model.SupplierId = offedit.SupplierId;
            model.PropertyId = offedit.PropertyId;
            model.CustomerId = offedit.CustomerId;
            //model.FromCustomerId = offedit.CustomerId;
            //model.FromCustomer = 1;
            model.SBEntity1 = offedit.SBEntityEntityId1;
            model.SBEntity = offedit.SBEntityEntityId;
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
            model.InventoryAPIType = offprodt.InventoryAPIType;

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

            model.PropertySupplierCountry = offprodt.SupplierCountry;
            model.PropertySupplierstate = offprodt.SupplierState;

            model.PropertySupplierstateName = offprodt.SupplierStatename;
            model.PropertySuppliercityname = offprodt.SupplierCityname;

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
            model.CustomerPaymentModeId = offedit.CustomerPaymentModeId;
            model.CustomerPaymentCreditPeriod = offedit.CustomerPaymentCreditPeriod;
            model.cancellationpolicy = offedit.cancellationpolicy;
            model.LoadPlaces();


            CLayer.OfflineBooking ob = BLayer.OfflineBooking.GetBookingTypeData(OfflineBookId);
            if (ob != null)
            {
                model.BookingType = (int)ob.BookingType;
                model.BookingTypeGST = ob.BookingTypeGST;
                model.BookingTypeAmount = ob.BookingTypeAmount;
                model.BookingTypeGSTIncluded = ob.BookingTypeGSTIncluded;
                model.BookingTypePercent = ob.BookingTypePercent;
                model.BookingTypeTAC = ob.BookingTypeTAC;
                model.BookingTypeHSNCode = ob.HSNCodeCodeID;
                model.SetBookingTypeTax(BLayer.OfflineBooking.GetBookingTypeTaxes(OfflineBookId));

            }
            else
            {
                model.SetBookingTypeTax(BLayer.OfflineBooking.GetDefaultBookingTypeTaxes());
            }
            // Booking For 
            if (model.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
            {
                long id = BLayer.OfflineBooking.GetBookingForID(OfflineBookId);
                if (id > 0)
                {
                    CLayer.OfflineBooking obs = BLayer.OfflineBooking.GetBookingFor(id);
                    model.MasterCustomerID = obs.MasterCustomerID;
                    model.BookingForID = obs.BookingForID;

                    model.DirectName = obs.DirectCustomerName;
                    model.DirectEmail = obs.DirectCustomerEmail;
                    model.DirectMobile = obs.DirectCustomerMobile;
                    model.DirectCountry = obs.DirectCustomerCountry;
                    model.DirectState = obs.DirectCustomerState;
                    model.DirectCity = obs.DirectCustomerCity;
                    model.DirectCitynames = obs.DirectCustomerCityname;
                    model.DirectAddress = obs.DirectCustomerAddress;
                    model.MasterCustomerID = obs.MasterCustomerID;
                    model.DirectpinCode = obs.DirectPinCode;
                    model.LoadBookingForCity();
                }
            }

            //if(model.InventoryAPIType==2)
            //{
            //    return View("~/Areas/Admin/Views/OfflineBookingGST/MultipleBookingGDS.cshtml", model);
            //}
            //else
            //{
            return View("~/Areas/Admin/Views/OfflineBookingGST/OfflineBooking.cshtml", model);
            //}


        }
        public async Task<ActionResult> OfflineBookingFirst(Models.OfflineBookingModel model)
        {

            Models.OfflineBookingModel data = await SaveOfflineBookingGST(model);

            return RedirectToAction("MultipleBooking", new { OfflineBookId = data.OfflineBookingId });


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

                return PartialView("~/Areas/Admin/Views/OfflineBookingGST/_PricingDetails.cshtml", model1);

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/OfflineBookingGST/_PricingDetails.cshtml", new Models.OfflineBookingModel());
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
            return PartialView("~/Areas/Admin/Views/OfflineBookingGST/_B2BCustomerList.cshtml", result);
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
            return View("~/Areas/Admin/Views/OfflineBookingGST/Vendorindex.cshtml", model);
        }


        public long SaveOfflineCustomerToUser(string CustomerName1, string CustomerEmail1, string CustomerMobile1, string GuestName1, string GuestEmail1, int CustomerState1,
        string CustomerCityname, string CustomerAddress1, int CustomerType, string BillingAddress, int BillingCountryId, int BillingState, string PinCode, string BillingMobile,
        string BillingCityname, string ContactPerson, string OfficeNO, int CustomerCountry1, int CustomerCity1, string CustomerpinCode1, bool NoInvoiceMail1, int CustomerPaymentMode = 0, decimal CreditDays = 0, int BillingCity = 0)
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

            data.CustomerPaymentMode = CustomerPaymentMode;
            data.CreditDays = CreditDays;
            data.CustomerPaymentCreditPeriod = CreditDays;
            data.CustomerPaymentModeId = CustomerPaymentMode;
            long OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomerNEW(data);

            //Update Noinvoicemail bool (name,email,usertype)
            data.CustomerName1 = CustomerName1;
            data.CustomerEmail1 = CustomerEmail1;
            data.NoInvoiceMail = NoInvoiceMail1;
            data.CustomerId = OfflineCustomerId;

            BLayer.OfflineBooking.UpdateNoinvoicemail(data);


            //Save to customer master table
            data.CustomerId = 0;
            long OfflineCustomerMasterId = BLayer.OfflineBooking.SaveMasterOfflineBookingCustomer(data);
            return OfflineCustomerMasterId;//id from customer master
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
        public JsonResult OfflineBookingAlreadyExistsChecking(string CustomerName, string GuestName, string PropertyName, DateTime CheckIn, DateTime CheckOut, long SalesPersonId, long OfflineBookingId)
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
        [HttpPost]
        public JsonResult GetCustomerPaymentMode(int CustomerId, string userType)
        {
            int type = 0;
            if (userType == "user")
            {
                type = 1;
            }
            if (userType == "offline booking")
            {
                type = 2;
            }
            var data = BLayer.OfflineBooking.GetCustomerPaymentMode(CustomerId, type);
            return Json(new { success = true, PayModeId = data.CustomerPaymentMode, Period = data.CustomerPaymentModeCreditDays },
                JsonRequestBehavior.AllowGet);
        }



        public long SaveCustomerGSTState(Models.OfflineBookingModel model, long Customerid)
        {
            long custgststeid = 0;
            try
            {
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                data.SubCustomerAddress = model.SubCustomerAddress;
                data.SubCustomerCity = model.SubCustomerCity;
                data.SubCustomerCityname = model.SubCustomerCityname;
                data.SubCustomerpinCode = model.SubCustomerpinCode;
                data.SubCustomerGstRegNo = model.SubCustomerGstRegNo;
                data.SubCustomerGstStateId = model.SubCustomerGstStateId;
                data.SubCustomerid = Customerid;
                data.CustomerTableType = model.CustomerTableType;
                custgststeid = BLayer.OfflineBooking.SaveCustomerGSTState(data);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return custgststeid;
        }

        [AllowAnonymous]
        public string GetCustomerGSTStateList(int Customerid, int Type)
        {
            List<CLayer.State> States = BLayer.State.GetCustGstStateList(Customerid, Type);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            sb.Append("<option value='0' selected >Select</option>");
            foreach (CLayer.State st in States)
            {
                sb.Append("<option value='" + st.StateId + "'>" + st.Name + "</option>");
            }
            return sb.ToString();
        }
        [AllowAnonymous]
        public ActionResult ChangeCustomerGstStateId(long Customerid, long Stateid, int status)
        {
            CLayer.OfflineBooking GstDetails = BLayer.OfflineBooking.GetCustomerGstRegNoByStateId(Customerid, Stateid, status);
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            //model.LoadgstcitiesbystatePlaces(Convert.ToInt32(Stateid));
            model.HiddenSubCustomerCity = GstDetails.SubCustomerCity;
            model.HiddenSubCustomerCityname = GstDetails.SubCustomerCityname;
            model.HiddenSubCustomerAddress = GstDetails.SubCustomerAddress;
            model.HiddenSubCustomerpinCode = GstDetails.SubCustomerpinCode;
            model.HiddenSubCustomerGstRegNo = GstDetails.SubCustomerGstRegNo;

            return View("~/Areas/Admin/Views/OfflineBookingGST/HiddenGstAddress.cshtml", model);
        }
        [AllowAnonymous]
        public string ChangePropertyGstbyPropertyid(long Propertyid, int type)
        {
            string Gstregno = BLayer.OfflineBooking.GetPropertyGstRegNoByPropertyid(Propertyid, type);
            return Gstregno.ToString();
        }

        public string GetGSTStateCode(int stateid)
        {
            string Gstregno = BLayer.OfflineBooking.GetGSTStateCode(stateid);
            return Gstregno;
        }

        public long GetBillingStateId(int id)
        {
            long stateid = BLayer.OfflineBooking.GetBillingStateId(id);
            return stateid;
        }

        public async Task<ActionResult> MultipleBooking(Models.OfflineBookingModel dt)
        {

            Models.OfflineBookingModel data1 = await SaveOfflineBookingGST(dt);
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            if (data1.UserTypeName == (string)CLayer.Role.CORPORATE)
            {
                if (data1.PropertyState != data1.CustomerGstStateId && data1.PropertyState != data1.BillingStateID)
                {
                    data.GSTStateDiffrent = 1;
                }
            }
            data1.PropertyState = dt.PropertyState;
            data1.CustomerGstStateId = dt.CustomerGstStateId;
            data1.UserType = dt.UserType;

            data.NoOfNight = 1;
            data.NoOfRooms = 1;
            data.OfflineBookingId = data1.OfflineBookingId;

            data.BookingDate = Convert.ToString(DateTime.Now);


            List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list1 = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list3 = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list4 = new List<CLayer.TaxPercentage>();
            data.TaxPercentageList_Service = new List<Models.TaxPercentage>();
            data.TaxPercentageList_Others = new List<Models.TaxPercentage>();
            list = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(0, "GST");
            list1 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(0, "GST - Others");
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



            list3 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(0, "GST");
            list4 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(0, "GST - Others");
            foreach (var item in list3)
            {
                Models.TaxPercentage cls = new Models.TaxPercentage();
                cls.TaxPerID = 0;
                cls.TaxOfflineBookingID = 0;
                cls.TaxTitle = item.TaxTitle;
                cls.TaxPercent = item.TaxPercent;
                cls.TaxType = item.TaxType;
                data.TaxPercentageList_Service.Add(cls);
            }
            foreach (var item in list4)
            {
                Models.TaxPercentage cls = new Models.TaxPercentage();
                cls.TaxPerID = 0;
                cls.TaxOfflineBookingID = 0;
                cls.TaxTitle = item.TaxTitle;
                cls.TaxPercent = item.TaxPercent;
                cls.TaxType = item.TaxType;
                data.TaxPercentageList_Others.Add(cls);
            }
            int Inventorytype = BLayer.Property.GetPropertyInventoryAPIType(data1.PropertyId);

            if (Inventorytype == 2)
            {
                DateTime starts = DateTime.Parse(data.CheckIn);
                DateTime ends = DateTime.Parse(data.CheckOut);

                var firstdate = starts.ToString("yyyy-MM-dd");
                var lastdate = ends.ToString("yyyy-MM-dd");

                return (MultipleBookingGDS(data1));
            }


            return View(data);
        }
        public ActionResult MultipleBookingGDS(Models.OfflineBookingModel dt)
        {

            var categoyu = BLayer.StayCategory.GetCategoryHotel();

            dt.StayCategory = 39;
            dt.StayCategory = 39;
            dt.StayCategoryName = "Hotel";
            dt.InventoryAPIType = 2;
            //dt.NoOfNight = 1;
            //dt.NoOfRooms = 1;


            dt.BookingDate = Convert.ToString(DateTime.Now);
            // SaveBooingDetails(dt);
            SaveBooingDetailsGDS(dt);



            return View("~/Areas/Admin/Views/OfflineBookingGST/MultipleBookingGDS.cshtml", dt);
        }


        [Common.AdminRolePermission]
        public async Task<Models.OfflineBookingModel> SaveOfflineBookingGST(Models.OfflineBookingModel model)
        {
            try
            {
                string email = User.Identity.GetUserName();
                int roleId = BLayer.User.GetRole(email);
                // save property as new entry (  custom property  )
                CLayer.OfflineBooking data = new CLayer.OfflineBooking();
                //int count = BLayer.Property.GetPropertybyEmail(model.PropertyEmail);
                long userId = GetUserId();
                long CustomPropertyId = 0;
                //Done by Rahul
                if (roleId == 5)
                {
                    if (model.CorporateUserID1 == 0)
                    {
                        data.CreatedBy = userId;
                        data.AssistedBy = 0;
                        data.CorporateID = 0;
                    }
                    else
                    {
                        data.CreatedBy = userId;
                        data.AssistedBy = model.CorporateUserID1;
                        data.CorporateID = userId;
                    }
                }
                else if (roleId == 6)
                {
                    if (model.AssistedCorporateUserID == 0)
                    {
                        data.CreatedBy = userId;
                        data.AssistedBy = 0;
                        data.CorporateID = 0;
                    }
                    else
                    {
                        data.CreatedBy = userId;
                        data.AssistedBy = model.AssistedCorporateUserID;

                        data.CorporateID = userId;
                    }
                }
                else
                {
                    if (model.CorporateID == 0)
                    {
                        data.CreatedBy = userId;
                        data.AssistedBy = 0;
                        data.CorporateID = 0;
                    }
                    else
                    {
                        data.CreatedBy = model.CorporateUserID;
                        data.AssistedBy = userId;
                        data.CorporateID = model.CorporateID;
                    }
                }
                data.BookingForSelf = model.BookingForSelf;
                data.NewBillingFor = model.NewBillingFor;
                DateTime BookingDate = DateTime.MinValue;
                DateTime.TryParse(model.BookingDate, out BookingDate);

                data.CreatedTime = BookingDate;
                data.OfflineBookingId = model.OfficeBookingID;
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
                data.CustomerPaymentModeId = model.CustomerPaymentModeId;
                data.CustomerPaymentCreditPeriod = model.CustomerPaymentModeId == 3 ? model.CustomerPaymentCreditPeriod : 0;

                //long OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomer(data);
                data.CustomerId = model.CustomerId;
                // long AddressId = BLayer.OfflineBooking.SaveUserAddressForOfflineBook(data);

                data.SupplierId = model.SupplierId;
                data.PropertyId = model.PropertyId;
                var InventoryAPIType = 0;
                int Inventorytype = BLayer.Property.GetPropertyInventoryAPIType(model.PropertyId);
                if (Inventorytype == 2)
                {
                    InventoryAPIType = 2;
                }
                data.InventoryAPIType = InventoryAPIType;

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
                data.OtherServiceNature = model.OtherServiceNature;
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
                data.SBEntityEntityId = model.SBEntity;
                data.SBEntityEntityId1 = model.SBEntity1;

                data.CustomerGstStateId = model.CustomerGstStateId;
                data.CustomerGstRegNo = model.CustomerGstRegNo;
                data.PropertyGstRegNo = model.PropertyGstRegNo;

                data.HotelFacility = model.HotelFacility;
                if (model.PayeeName != null || model.AmountPayable != 0)
                {
                    data.PayeeID = model.PayeeID;
                    data.PayeeName = model.PayeeName;
                    data.AmountPayable = model.AmountPayable;
                    data.AccountNumber = model.AccountNumber;
                    data.IFSCcode = model.IFSCcode;
                    data.BankName = model.BankName;
                    data.BranchName = model.BranchName;
                    data.PAN = model.PAN;

                    long payeeID = BLayer.OfflineBooking.SavePayee(data);
                    data.PayeeID = payeeID;
                }

                if (model.PropertyGstRegNo != "")
                {
                    if (data.PropertyId > 0)
                    {
                        string Gstregno = BLayer.OfflineBooking.GetPropertyGstRegNoByPropertyid(data.PropertyId, 1);
                        if (Gstregno == "")
                        {
                            BLayer.OfflineBooking.SavePropertyGstRegNoByPropertyid(data.PropertyId, 1, model.PropertyGstRegNo);
                        }
                    }
                    else if (data.CustomPropertyId > 0)
                    {
                        string Gstregno = BLayer.OfflineBooking.GetPropertyGstRegNoByPropertyid(data.CustomPropertyId, 2);
                        if (Gstregno == "")
                        {
                            BLayer.OfflineBooking.SavePropertyGstRegNoByPropertyid(data.CustomPropertyId, 2, model.PropertyGstRegNo);
                        }
                    }
                }

                data.FromCustomer = model.FromCustomer;
                data.FromCustomerId = model.FromCustomerId;
                data.cancellationpolicy = model.cancellationpolicy;
                long OfflineBookingId = BLayer.OfflineBooking.SaveOfflineBookingDetailsGST(data);



                // BBOKING FOR  
                if (model.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
                {
                    data.MasterCustomerID = model.MasterCustomerID;
                    data.BookingForID = model.BookingForID;

                    data.DirectCustomerName = model.DirectName;
                    data.DirectCustomerEmail = model.DirectEmail;
                    data.DirectCustomerMobile = model.DirectMobile;
                    data.DirectCustomerCountry = model.DirectCountry;
                    data.DirectCustomerState = model.DirectState;
                    data.DirectCustomerCity = model.DirectCity;
                    data.DirectCustomerCityname = model.DirectCitynames;
                    data.DirectCustomerAddress = model.DirectAddress;
                    data.MasterCustomerID = model.MasterCustomerID;
                    data.DirectPinCode = model.DirectpinCode;
                    BLayer.OfflineBooking.SaveBookingForToOfflinebooking_bookingfor(data, OfflineBookingId);
                }


                if (OfflineBookingId > 0)
                {
                    //check for any invoices
                    CLayer.Invoice indata = BLayer.Invoice.GetInvoiceByOfflineBooking(OfflineBookingId);
                    if (indata != null)
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

                    string existorderno = BLayer.OfflineBooking.ExistOrderno(OfflineBookingId);

                    if (existorderno == "" || existorderno == null)
                    {
                        BLayer.OfflineBooking.SetPaymentRefNo(OfflineBookingId, rle, orderNo);
                    }
                }

                model.OfflineBookingId = OfflineBookingId;
                data.OfflineBookingId = OfflineBookingId;


                if (OfflineBookingId > 0)
                {
                    // save customer details
                    //long OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomer(data);

                    // update userid in offline booking

                    long OfflineCustomerId = 0;

                    if (model.CategoryType == "user")
                    {
                        BLayer.OfflineBooking.RemoveCustomerEntry(OfflineBookingId);
                        CLayer.OfflineBooking userdetails = new CLayer.OfflineBooking();

                        userdetails = BLayer.OfflineBooking.UserDetailsByOffliceBookingId(data);

                        //CLayer.User usr = BLayer.User.Get(data.CustomerId);
                        userdetails.UserType = (int)CLayer.Role.Roles.Customer;
                        data.UserType = (int)CLayer.Role.Roles.Customer;
                        if (model.UserTypeName == "Corporate")
                        {
                            userdetails.UserType = (int)CLayer.Role.Roles.Corporate;
                            data.UserType = (int)CLayer.Role.Roles.Corporate;
                        }

                        //if (usr == null)
                        //{ throw new Exception("User not found in Offline Booking_Save Id : " + data.CustomerId.ToString()); }


                        OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomerNEWForUser(userdetails);

                        //Update Noinvoicemail bool (name,email,usertype)
                        data.CustomerName1 = userdetails.CustomerName;
                        data.CustomerEmail1 = userdetails.CustomerEmail;

                        data.NoInvoiceMail = false;

                        data.CustomerId = OfflineCustomerId;
                        BLayer.OfflineBooking.UpdateNoinvoicemail(data);

                        BLayer.OfflineBooking.UpdateOfflineBookingCustomer(OfflineBookingId, OfflineCustomerId);

                    }
                    else
                    {
                        // BLayer.OfflineBooking.UpdateOfflineBookingCustomerNew(OfflineBookId, data.CustomerId);
                        //Current OfflinebookingCustomerid

                        //data.CustomerType = BLayer.OfflineBooking.GetUserType(data.CustomerId);


                        data.CustomerType = (int)CLayer.Role.Roles.Customer;
                        if (model.UserTypeName == "Corporate")
                        {
                            data.CustomerType = (int)CLayer.Role.Roles.Corporate;
                        }


                        BLayer.OfflineBooking.RemoveCustomerEntry(OfflineBookingId);
                        OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomerNEW(data);


                        //Update offline booking customer  gst 
                        //if (data.CustomerType == 5)
                        //{
                        //    BLayer.OfflineBooking.UpdateOfflineBookingGstForNewCustomer(model.OfflineBookingId, model.CustomerId, OfflineCustomerId, model.CustomerEmail);
                        //}

                        BLayer.OfflineBooking.UpdateOfflineBookingCustomer(OfflineBookingId, OfflineCustomerId);
                        //Update Noinvoicemail bool (name,email,usertype)
                        data.CustomerName1 = model.CustomerName;
                        data.CustomerEmail1 = model.CustomerEmail;
                        data.NoInvoiceMail = model.NoInvoiceMail;
                        data.CustomerId = OfflineCustomerId;
                        BLayer.OfflineBooking.UpdateNoinvoicemail(data);
                    }

                    // save offline booking id in offline-customer table

                    BLayer.OfflineBooking.UpdateOfflineBookingCustomerNew(OfflineBookingId, OfflineCustomerId);

                    if (data.SupplierState == 0)
                    {
                        data.SupplierState = model.PropertySupplierstate;
                    }
                    if (data.SupplierStatename == "" || data.SupplierStatename == null)
                    {
                        data.SupplierStatename = model.PropertySupplierstateName;
                    }
                    // save property details
                    long OfflinePropId = BLayer.OfflineBooking.SavePropertyDetails(data);
                    //   model.Vendorlist = BLayer.OfflineBooking.getVendorDetails(OfflineBookId);
                    //BY price BreakUp


                }

                //Booking Type data save
                //Save tax 
                if (model.BookingTypeTaxes != null && OfflineBookingId > 0)
                {
                    if (model.BookingTypeTaxes.Count > 0)
                    {
                        List<KeyValuePair<string, double>> taxes = new List<KeyValuePair<string, double>>();
                        double ta;
                        foreach (Models.TaxPercentage t in model.BookingTypeTaxes)
                        {
                            ta = 0;
                            if (double.TryParse(t.TaxPercent, out ta))
                            {
                                if (ta > 0) { taxes.Add(new KeyValuePair<string, double>(t.TaxTitle, ta)); }
                            }
                        }
                        BLayer.OfflineBooking.AddBookingTypeTaxes(OfflineBookingId, taxes);
                    }
                }
                //save amounts
                if (OfflineBookingId > 0)
                {
                    if (model.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Regular)
                    {
                        BLayer.OfflineBooking.SaveBookingTypeData(OfflineBookingId, model.BookingType, 0, 0, 0, false, 0, 0);
                    }
                    else
                    {
                        if (model.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.TAC)
                        { BLayer.OfflineBooking.SaveBookingTypeData(OfflineBookingId, model.BookingType, model.BookingTypeGST, model.BookingTypeTAC, 0, model.BookingTypeGSTIncluded, model.BookingTypePercent, model.BookingTypeHSNCode); }
                        else if (model.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
                        { BLayer.OfflineBooking.SaveBookingTypeData(OfflineBookingId, model.BookingType, model.BookingTypeGST, 0, model.BookingTypeAmount, false, model.BookingTypePercent, model.BookingTypeHSNCode); }
                        else
                        { BLayer.OfflineBooking.SaveBookingTypeData(OfflineBookingId, (int)CLayer.ObjectStatus.OfflineBookingType.Regular, 0, 0, 0, false, 0, 0); }
                    }

                    //fix amounts
                    BLayer.OfflineBooking.FixAmounts(OfflineBookingId);
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


        public long SaveCustomerBookingfor(string DirectNameNew, int DirectCountryNew, string DirectEmailnew, int DirectStateNew, string DirectMobileNew,
string DirectAddressNew, string DirectpinCodeNew, string DirectCitynameNew, int MasterCustomerID = 0, int DirectBillingCity = 0, int DirectCityNew = 0)
        {
            long OfflineCustomerId = 0;

            if (DirectCityNew == 0 || DirectCityNew == -1)
            {
                DirectCityNew = -1;
            }
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            data.CustomerName = DirectNameNew;
            data.CustomerEmail = DirectEmailnew;
            data.CustomerMobile = DirectMobileNew;
            data.CustomerCountry = DirectCountryNew;
            data.CustomerState = DirectStateNew;
            data.CustomerCity = DirectCityNew;
            data.CustomerCityname = DirectCitynameNew;
            data.CustomerAddress = DirectAddressNew;
            data.CustomerId = MasterCustomerID;
            data.PinCode = DirectpinCodeNew;
            OfflineCustomerId = BLayer.OfflineBooking.SaveOfflineBookingCustomerBookingFor(data);


            return OfflineCustomerId;
        }



        public ActionResult NewMultipleBooking(long OfflineBookingId, int GSTStateDiffrent)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            model.GSTStateDiffrent = GSTStateDiffrent;
            model.OfflineBookingId = OfflineBookingId;
            model.AccommodationTypeName = "";
            model.NoOfPaxAdult = 0;
            model.NoOfPaxChild = 0;
            model.HotelConformationNo = "";
            model.GuestName = "";
            model.GuestEmail = "";
            model.AvgDailyRateBefreStaxForBuyPrice = 0;
            model.StaxForBuyPrice = 0;
            model.BookingId = 0;
            return View("~/Areas/Admin/Views/OfflineBookingGST/MultipleBooking.cshtml", model);
        }

        public void GDSMultipleBookingDelete(long BookedID, long OfflineBookingId)
        {
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            Models.OfflineBookingModel MultipleBooking = new Models.OfflineBookingModel();
            CLayer.OfflineBooking model = new CLayer.OfflineBooking();

            data.BookingId = model.BookingId;
            data.Accommodationid = model.Accommodationid;
            data.Accommodationtypeid = model.Accommodationtypeid;
            data.AccommodatoinType = model.AccommodatoinType;
            data.AccommodatoinTypename = model.Accommodationname;
            data.StayCategory = model.StayCategory;
            data.StayCategoryName = model.StayCategoryName;
            data.StayCategoryid = model.StayCategoryid;
            data.NoOfNight = model.NoOfNight;
            data.NoOfRooms = model.NoOfRooms;
            data.NoOfPaxAdult = model.NoOfPaxAdult;
            data.NoOfPaxChild = model.NoOfPaxChild;
            DateTime CheckInt = DateTime.MinValue;
            //    DateTime.TryParse(model.CheckIn, out CheckInt);

            DateTime CheckOutt = DateTime.MinValue;
            //  DateTime.TryParse(model.CheckOut, out CheckOutt);

            data.CheckIn = CheckInt;
            data.CheckOut = CheckOutt;
            data.GuestEmail = model.GuestEmail;
            data.GuestName = model.GuestName;
            data.GuestPhone = model.GuestPhone == null ? "" : model.GuestPhone;

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
            data.HotelConfirmationNo = model.HotelConfirmationNo;


            data.CustomPropertyId = model.CustomPropertyId;
            data.OfflineBookingId = model.OfflineBookingId;
            data.HSNCodeCodeID = model.HSNCodeCodeID;

            data.SupplierCancellationDone = model.SupplierCancellationDone;
            data.SaveStatus = (int)CLayer.OfflineBooking.Statuslist.Save;



            long BookingID = BLayer.OfflineBooking.SaveMultipleBooking(data);
        }
        public List<CLayer.Rates> GetGDSRates(Models.OfflineBookingModel model)
        {

            #region LIVE HOTEL SEARCH
            string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
            string HotelId = BLayer.Property.GetHotelId(model.PropertyId);
            CLayer.SearchCriteria srch = new CLayer.SearchCriteria();

            DateTime CheckInt = DateTime.MinValue;
            DateTime.TryParse(model.CheckIn, out CheckInt);

            // Session["CheckIn"] = CheckInt;

            DateTime CheckOutt = DateTime.MinValue;
            DateTime.TryParse(model.CheckOut, out CheckOutt);

            srch.CheckIn = CheckInt;
            srch.CheckOut = CheckOutt;

            srch.HotelCode = HotelId;

            int noaccom = Convert.ToInt32(Session["BedRooms"]);
            int adults = 1;
            int child = 0;
            int guests = adults + child;

            var BookingDataSelected = ',' + noaccom.ToString() + ',' + adults.ToString() + ',' + child.ToString() + ',' + guests.ToString();


            string BookingSelected = "" + BookingDataSelected + "";
            string BookingData = "#" + model.Accommodationid + BookingSelected;

            StayBazar.Models.SimpleBookingModel BookData = new StayBazar.Models.SimpleBookingModel();
            BookData.BookCheckIn = srch.CheckIn;
            BookData.BookCheckOut = srch.CheckOut;
            BookData.BookingData = BookingData;
            BookData.PropertyId = model.PropertyId;



            string result = StayBazar.Controllers.SearchController.HotelMultiSingleAvailability(srch, false, HotelId);

            #region Transaction Log 

            APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailabilityLiveSearch, 0, 2);

            #endregion Transaction log end

            CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
            CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdvanced = new CLayer.HotelAvailabilityAdvanced.Envelope();

            Serializer ser = new Serializer();
            HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);

            Serializer seradv = new Serializer();
            HotelResultAdvanced = seradv.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);




            ///    HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
            var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();
            var RoomStayItemListAdvanced = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStays>();




            Session["SessionId"] = HotelResult.Header.Session.SessionId;
            Session["SequenceNumber"] = Convert.ToInt32(HotelResult.Header.Session.SequenceNumber);
            Session["SecurityToken"] = HotelResult.Header.Session.SecurityToken;
            #endregion

            #region PRICE
            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(model.PropertyId);
            Session["InventoryAPIType"] = InventoryAPIType;
            HotelId = BLayer.Property.GetHotelId(model.PropertyId);
            List<CLayer.RoomStaysResult> objRoomStayResult = new List<CLayer.RoomStaysResult>();
            List<CLayer.Rates> AmadeusRates = new List<CLayer.Rates>();
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {

                objRoomStayResult = APIUtility.AmedusHotelRoomStaysResultList;// GetRoomStayResult(data.PropertyId, data.BookCheckIn, data.BookCheckOut);
                #region ENHANCED PRICING
                StayBazar.Controllers.BookingController objBookingController = new StayBazar.Controllers.BookingController();
                objRoomStayResult = GetRoomStayResult(model.PropertyId, srch.CheckIn, srch.CheckOut);

                Session["RoomStaysResult"] = objRoomStayResult;



                //  Enhanced pricing
                AmadeusRates = GetAmedusRates(BookData, objRoomStayResult, HotelId);
                TempData["AmadeusRates"] = AmadeusRates;
                TempData["InventoryAPIType"] = InventoryAPIType;
                Session["AmadeusRates"] = AmadeusRates;

                ViewBag.amt = AmadeusRates;
                System.Collections.ArrayList BookingCodeList = new System.Collections.ArrayList();
                foreach (var item in AmadeusRates)
                {
                    BookingCodeList.Add(item.BookingCode);
                }

                objRoomStayResult = objRoomStayResult.Where(a => BookingCodeList.Contains(a.BookingCode)).ToList();
                //Enhanced pricing end
                #endregion
            }

            #endregion

            #region PNR ADD MULTIELEMENTS-0      
            Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;
            string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
            APIUtility.SoapHeaderStateful = SoapHeaderStateful;


            AmedusRoomStaysResultList = objRoomStayResult;
            APIUtility.AmedusHotelRoomStaysResultList = AmedusRoomStaysResultList;

            string BookingCode = objRoomStayResult.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
            string PNRADDSoapBody = APIUtility.PNR_AddMultielementsOffline(HotelId, BookingCode, "test@test.com", 0, true);

            TempData["RoomStaysResult"] = AmedusRoomStaysResultList;
            APIUtility.AmedusHotelRoomStaysResultList = AmedusRoomStaysResultList;

            result = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);

            #region Transaction Log 

            APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElements, model.OfficeBookingID, 2);

            #endregion Transaction log end

            Serializer pnrser = new Serializer();

            CLayer.PNRAddElements.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElements.Envelope>(result);
            Session["SessionId"] = PNRAddResult.Header.Session.SessionId;
            Session["SequenceNumber"] = Convert.ToInt32(PNRAddResult.Header.Session.SequenceNumber);
            Session["SecurityToken"] = PNRAddResult.Header.Session.SecurityToken;
            #endregion

            return AmadeusRates;
        }
        public List<CLayer.RoomStaysResult> GetRoomStayResult(long PropertyID, DateTime CheckIn, DateTime CheckOut)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
            // Checking Hotelid,roomid available

            try
            {
                string HotelId = BLayer.Property.GetHotelId(PropertyID);


                //Get accommodations amedus

                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                srch.HotelCode = HotelId;
                srch.CheckIn = CheckIn;
                srch.CheckOut = CheckOut;

                string result = StayBazar.Controllers.SearchController.HotelMultiSingleAvailability(srch, false, HotelId);

                Serializer ser = new Serializer();
                CLayer.HotelAvailability.Envelope HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);

                string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION); ;



                long AmadeusPropertyID = 0;
                string CityName = string.Empty;
                var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();
                Session["SessionId"] = HotelResult.Header.Session.SessionId;
                Session["SequenceNumber"] = HotelResult.Header.Session.SequenceNumber;
                Session["SecurityToken"] = HotelResult.Header.Session.SecurityToken;

                string SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                RoomStaysResultList = null;
                foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                {
                    HotelItem = item;

                    string RoomStayRPH = item.RoomStayRPH;
                    RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                    if (!string.IsNullOrEmpty(RoomStayRPH))
                    {

                        string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                        RoomStaysResultList = StayBazar.Controllers.SearchController.GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList);
                    }
                }
            }
            catch (Exception ex)
            {
                RoomStaysResultList = null;
            }


            // ViewBag.AmedusRoomsList = RoomStaysResultList.Count;


            return RoomStaysResultList;
        }
        public List<CLayer.Rates> GetAmedusRates(StayBazar.Models.SimpleBookingModel bookingData, List<CLayer.RoomStaysResult> RoomStaysResultList, string HotelCode)
        {
            List<CLayer.Rates> rtes = new List<CLayer.Rates>();
            CLayer.Rates AmadeusRates = null;
            string result = "";
            long AccomSelect = 0;
            string BookingCode = "";

            string AgeQualifyingCode = "10";
            // string GuestCount = "2";

            int BedRoomsQuantity = 1;
            int GuestCount = 1;
            BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
            GuestCount = GuestCount <= 0 ? 1 : GuestCount;

            Session["HotelCode"] = HotelCode;
            int TaxAddedPercent = 0;

            //Customer state id
            int CustomerStateID = BLayer.State.GetCustomerStateID(Convert.ToInt32(Session["LoggedInUser"]));

            //Billing entity state id
            int BillingEntityStateID = BLayer.State.GetBillingEntityStateID(bookingData.PropertyId);
            BillingEntityStateID = (BillingEntityStateID == 0) ? Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARDEFAULTBILLINGENTITY)) : BillingEntityStateID;

            try
            {
                //Pricing details from enhanced pricing details API
                string SecurityToken = Convert.ToString(Session["SecurityToken"]);
                string SequenceNumber = Convert.ToString(Session["SequenceNumber"]);
                string SessionId = Convert.ToString(Session["SessionId"]);

                CLayer.EnhancedPricing.Envelope PricingResult = null;

                CLayer.EnhancedPricingAdvanced.Envelope PricingResultAdv = null;

                ArrayList AccommodationSelected = GetAccommodationSelected(bookingData.BookingData);

                var accom = BLayer.Accommodation.GetAllAccByPropertyid(bookingData.PropertyId);
                for (int i = 0; i < AccommodationSelected.Count; i++)
                {
                    AccomSelect = Convert.ToInt64(AccommodationSelected[i]);
                    BookingCode = accom.Where(x => x.AccommodationId == Convert.ToInt64(AccommodationSelected[i])).FirstOrDefault().BookingCode;
                    //  RoomStaysResultList = RoomStaysResultList.Where(x=>x.BookingCode==BookingCode).ToList();
                    foreach (var item in RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList())
                    {
                        AmadeusRates = new CLayer.Rates();
                        StayBazar.Controllers.PropertyController objProperty = new StayBazar.Controllers.PropertyController();
                        result = objProperty.GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, item.RoomTypeCode, HotelCode, item.RatePlanCode, item.BookingCode, bookingData.BookCheckIn.ToString("yyyy-MM-dd"), bookingData.BookCheckOut.ToString("yyyy-MM-dd"), "", AgeQualifyingCode, GuestCount.ToString(), bookingData.PropertyId);

                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelEnhancedPricing, 0, 2);

                        #endregion Transaction log end

                        Serializer ser = new Serializer();

                        CLayer.EnhancedPricing.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItem = null;
                        CLayer.EnhancedPricingAdvanced.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItemAdv = null;
                        try
                        {
                            PricingResult = ser.Deserialize<CLayer.EnhancedPricing.Envelope>(result);
                            RatesItem = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                PricingResultAdv = ser.Deserialize<CLayer.EnhancedPricingAdvanced.Envelope>(result);
                                RatesItemAdv = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
                            }
                            catch (Exception innerex)
                            {
                                AmadeusRates = ReadGDSPrice(result, Convert.ToInt64(AccommodationSelected[i]), BookingCode, CustomerStateID, BillingEntityStateID);
                                rtes.Add(AmadeusRates);
                                return rtes;
                            }

                        }



                        if (RatesItem != null)
                        {
                            string RoomStayCurrencyCode = RatesItem.RoomRate.Total.CurrencyCode;
                            decimal RateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                            string CurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                            decimal AmountAfterTax = APIUtility.GetBookingAmount(Convert.ToDecimal(RatesItem.RoomRate.Total.AmountAfterTax), RoomStayCurrencyCode, CurrencyCode, RateConversion);
                            RatesItem.RoomRate.Total.AmountAfterTax = AmountAfterTax;

                            AmadeusRates.NoofAcc = 10;
                            decimal TaxPercent = RatesItem.RoomRate.Total.Taxes.Tax.Percent;
                            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                            decimal TaxAmount = (RatesItem.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
                            AmadeusRates.Amount = RatesItem.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                            AmadeusRates.GDSAmount = AmadeusRates.Amount;
                            //     AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                            Session["GDSAmount"] = RatesItem.RoomRate.Total.AmountAfterTax;
                            Session["GDSConvertedAmount"] = AmadeusRates.Amount;

                            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                            AmadeusRates.taxpercent = TaxPercent;
                            decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            //AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            AmadeusRates.CGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            AmadeusRates.SGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            AmadeusRates.IGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
                            //  AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : SBTaxPercent;

                            TaxAddedPercent = Convert.ToInt32(AmadeusRates.CGSTTaxPercent + AmadeusRates.SGSTTaxPercent) + 100;
                            Decimal TaxExcludedAmount = AmadeusRates.Amount - TaxAmount;
                            //AmadeusRates.CGSTTaxPercent =  (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent =  (SBTaxPercent / 2);
                            //AmadeusRates.IGSTTaxPercent = SBTaxPercent;

                            AmadeusRates.BookingCode = BookingCode;

                            if (CustomerStateID == BillingEntityStateID)
                            {
                                TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;


                                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                                RatesItem.RoomRate.Total.AmountAfterTax = TaxExcludedAmount;
                                //AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
                                //AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));
                                AmadeusRates.SGSTTax = (AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
                                AmadeusRates.CGSTTax = (AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));
                                AmadeusRates.Amount = TaxExcludedAmount;// APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax)- (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                            }
                            else
                            {
                                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                                //AmadeusRates.IGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                //AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax);// - AmadeusRates.IGSTTax;
                                AmadeusRates.IGSTTax = ((AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                AmadeusRates.Amount = AmountAfterTax;// - AmadeusRates.IGSTTax;
                                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                            }



                        }
                        else if (RatesItemAdv != null)
                        {
                            string RoomStayCurrencyCode = RatesItemAdv.RoomRate.Total.CurrencyCode;
                            decimal RateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                            string CurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                            decimal AmountAfterTax = APIUtility.GetBookingAmount(Convert.ToDecimal(RatesItemAdv.RoomRate.Total.AmountAfterTax), RoomStayCurrencyCode, CurrencyCode, RateConversion);
                            RatesItemAdv.RoomRate.Total.AmountAfterTax = AmountAfterTax;


                            AmadeusRates.NoofAcc = 10;
                            decimal TaxPercent = RatesItemAdv.RoomRate.Total.Taxes[0].Percent;
                            TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                            decimal TaxAmount = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
                            AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                            AmadeusRates.GDSAmount = AmadeusRates.Amount;
                            //       AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                            Session["GDSAmount"] = RatesItemAdv.RoomRate.Total.AmountAfterTax;
                            Session["GDSConvertedAmount"] = AmadeusRates.Amount;

                            AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                            AmadeusRates.taxpercent = TaxPercent;
                            decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                            //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            //AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            AmadeusRates.CGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                            AmadeusRates.SGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                            AmadeusRates.IGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                            TaxAddedPercent = Convert.ToInt32(AmadeusRates.CGSTTaxPercent + AmadeusRates.SGSTTaxPercent) + 100;
                            Decimal TaxExcludedAmount = AmadeusRates.Amount - TaxAmount; //AmadeusRates.Amount * 100 / TaxAddedPercent;
                            //AmadeusRates.CGSTTaxPercent = (SBTaxPercent / 2);
                            //AmadeusRates.SGSTTaxPercent = (SBTaxPercent / 2);
                            //AmadeusRates.IGSTTaxPercent = SBTaxPercent;


                            AmadeusRates.BookingCode = BookingCode;

                            if (CustomerStateID == BillingEntityStateID)
                            {
                                TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;

                                AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                                RatesItemAdv.RoomRate.Total.AmountAfterTax = TaxExcludedAmount;
                                //AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
                                //AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));
                                AmadeusRates.SGSTTax = (AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
                                AmadeusRates.CGSTTax = (AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));
                                AmadeusRates.Amount = TaxExcludedAmount;// APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) - (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                                AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                            }
                            else
                            {
                                AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                                //AmadeusRates.IGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                //AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax);// - AmadeusRates.IGSTTax;
                                AmadeusRates.IGSTTax = ((AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
                                AmadeusRates.Amount = AmountAfterTax;// - AmadeusRates.IGSTTax;
                                AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                            }


                            //}
                        }
                        if (PricingResult != null)
                        {
                            if (PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
                            {
                                AmadeusRates.GuarenteeCode = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();
                            }
                        }

                        if (PricingResultAdv != null)
                        {
                            if (PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
                            {
                                AmadeusRates.GuarenteeCode = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();

                            }
                        }

                        Session["GuaranteeCode"] = AmadeusRates.GuarenteeCode;

                        rtes.Add(AmadeusRates);
                    }
                }

                if (PricingResult != null)
                {
                    Session["SessionId"] = PricingResult.Header.Session.SessionId;
                    Session["SequenceNumber"] = Convert.ToInt32(PricingResult.Header.Session.SequenceNumber);
                    Session["SecurityToken"] = PricingResult.Header.Session.SecurityToken;
                }
                else
                {
                    Session["SessionId"] = PricingResultAdv.Header.Session.SessionId;
                    Session["SequenceNumber"] = Convert.ToInt32(PricingResultAdv.Header.Session.SequenceNumber);
                    Session["SecurityToken"] = PricingResultAdv.Header.Session.SecurityToken;
                }

            }
            catch (Exception ex)
            {
                AmadeusRates = ReadGDSPrice(result, AccomSelect, BookingCode, CustomerStateID, BillingEntityStateID);
                if (AmadeusRates != null)
                {
                    rtes.Add(AmadeusRates);
                }
                else
                {
                    rtes = null;
                }


            }
            return rtes;
        }
        public ArrayList GetAccommodationSelected(string bookingData)
        {
            string[] sAccom = bookingData.Trim().Split('#');
            string AccommodationID = string.Empty;
            ArrayList AccList = new ArrayList();
            for (int i = 1; i <= sAccom.Length - 1; i++)
            {
                string[] AccomID = sAccom[i].Split(',');
                AccList.Add(AccomID[0]);
            }
            return AccList;
        }
        private CLayer.Rates ReadGDSPrice(string xml, long AccommodationSelected, string BookingCode, int CustomerStateID, int BillingEntityStateID)
        {

            XmlDocument xmlDoc = new XmlDocument();


            string Message = string.Empty;
            CLayer.Rates AmadeusRates = new CLayer.Rates();
            try
            {


                xmlDoc.LoadXml(xml);

                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XmlNode node = null;
                XmlNode nodeTax = null;
                XmlNode nodeTotal = null;
                string nodeRoot = "/soapenv:Envelope/soapenv:Body/";
                string nodeRate = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RoomRates";
                string nodetotal = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RoomRates/si:RoomRate/si:Total";
                string nodevalue = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RoomRates/si:RoomRate/si:Total/si:Taxes/si:Tax";
                string nodeGuaranteevalue = "/si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RatePlans/si:RatePlan/si:Guarantee";

                string nodeError = "/si:errorGroup/si:errorDescription/si:freeText";
                string nodeWarning = "/si:generalErrorInfo/si:errorWarningDescription";

                xmlnsManager.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                xmlnsManager.AddNamespace("awsse", "http://xml.amadeus.com/2010/06/Session_v3");
                xmlnsManager.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");


                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");
                nodeTax = xmlDoc.SelectSingleNode(nodeRoot + nodevalue, xmlnsManager);
                node = xmlDoc.SelectSingleNode(nodeRoot + nodeRate, xmlnsManager);
                if (node == null)
                {
                    AmadeusRates = null;
                    return AmadeusRates;
                }
                nodeTotal = xmlDoc.SelectSingleNode(nodeRoot + nodetotal, xmlnsManager);

                XmlNodeList xNodeList = node.SelectNodes(nodeRoot + nodevalue, xmlnsManager);
                XmlNode nodeguarantee = xmlDoc.SelectSingleNode(nodeRoot + "si:OTA_HotelAvailRS/si:RoomStays/si:RoomStay/si:RatePlans/si:RatePlan/si:Guarantee/@GuaranteeCode", xmlnsManager);

                //decimal TaxPercent = 0;
                //decimal AmountAfterTax = 0;
                //decimal AmountBeforeTax = 0;
                string GuarenteeCode = "";
                if (nodeguarantee != null)
                {
                    GuarenteeCode = GetXmlChildNodes(nodeguarantee, "GuaranteeCode");
                }



                if (node != null)
                {

                    AmadeusRates.NoofAcc = 10;

                    List<CLayer.GDSRateTaxes> objTaxesList = new List<CLayer.GDSRateTaxes>();
                    foreach (XmlNode xNode in xNodeList)
                    {
                        CLayer.GDSRateTaxes objTaxes = new CLayer.GDSRateTaxes();

                        objTaxes.Percent = GetXmlChildNodes(xNode, "Percent");
                        objTaxes.Type = GetXmlChildNodes(xNode, "Type");
                        objTaxes.Code = GetXmlChildNodes(xNode, "Code");
                        objTaxes.ChargeUnit = GetXmlChildNodes(xNode, "ChargeUnit");
                        objTaxesList.Add(objTaxes);
                    }

                    AmadeusRates.TaxesList = objTaxesList;
                    string Amount = GetXmlChildNodes(nodeTotal, "AmountAfterTax");
                    //      string Amountbeforetax = GetXmlChildNodes(nodeTotal, "AmountBeforeTax");
                    decimal AmountAfterTax = Convert.ToDecimal(Amount);

                    // AmountAfterTax = (Convert.ToString(Session["GDSCountry"]) == "") ? AmountAfterTax : APIUtility.GetGDSConvertedAmount(AmountAfterTax);

                    string RoomStayCurrencyCode = GetXmlChildNodes(nodeTotal, "CurrencyCode");
                    decimal RateConversion = Convert.ToDecimal(Session["GDSRateConversion"]);
                    string CurrencyCode = Convert.ToString(Session["GDSCurrencyCode"]);
                    AmountAfterTax = APIUtility.GetBookingAmount(AmountAfterTax, RoomStayCurrencyCode, CurrencyCode, RateConversion);




                    AmadeusRates.Amount = (Amount == "") ? 0 : AmountAfterTax;
                    AmadeusRates.GDSAmount = AmadeusRates.Amount;
                    //   AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
                    Session["GDSAmount"] = AmountAfterTax;
                    Session["GDSConvertedAmount"] = AmadeusRates.Amount;
                    decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

                    decimal CGSTPercent = 0;
                    decimal SGSTPercent = 0;
                    decimal IGSTPercent = 0;
                    if (objTaxesList.Count > 1)
                    {
                        CGSTPercent = (objTaxesList[0].Percent == "") ? 0 : Math.Round(Convert.ToDecimal(objTaxesList[0].Percent), 0);
                        SGSTPercent = (objTaxesList[1].Percent == "") ? 0 : Math.Round(Convert.ToDecimal(objTaxesList[1].Percent), 0);
                        IGSTPercent = (objTaxesList[0].Percent == "") ? 0 : Math.Round(Convert.ToDecimal(objTaxesList[0].Percent), 0);
                    }


                    AmadeusRates.NoofAcc = 10;
                    decimal TaxPercent = Convert.ToDecimal(objTaxesList[0].Percent);// RatesItemAdv.RoomRate.Total.Taxes[0].Percent;
                    TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
                    decimal TaxAmount = (AmadeusRates.Amount * (TaxPercent / 100));
                    //AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
                    AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
                                                                                          //        AmadeusRates.taxpercent = TaxPercent;

                    AmadeusRates.CGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
                    AmadeusRates.SGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
                    AmadeusRates.IGSTTaxPercent = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

                    int TaxAddedPercent = Convert.ToInt32(AmadeusRates.CGSTTaxPercent + AmadeusRates.SGSTTaxPercent) + 100;
                    Decimal TaxExcludedAmount = Convert.ToDecimal(AmountAfterTax) - TaxAmount; //AmadeusRates.Amount * 100 / TaxAddedPercent;


                    //AmadeusRates.CGSTTaxPercent = (SBTaxPercent / 2);
                    //AmadeusRates.SGSTTaxPercent = (SBTaxPercent / 2);
                    //AmadeusRates.IGSTTaxPercent = SBTaxPercent;

                    AmadeusRates.BookingCode = BookingCode;

                    if (CustomerStateID == BillingEntityStateID)
                    {
                        TaxExcludedAmount = (TaxAmount == 0) ? AmadeusRates.Amount * 100 / TaxAddedPercent : TaxExcludedAmount;

                        AmadeusRates.IsCustStateEqualtoBillingEntity = true;
                        AmountAfterTax = TaxExcludedAmount;
                        AmadeusRates.SGSTTax = (AmountAfterTax * (AmadeusRates.SGSTTaxPercent / 100));
                        AmadeusRates.CGSTTax = (AmountAfterTax * (AmadeusRates.CGSTTaxPercent / 100));
                        AmadeusRates.Amount = TaxExcludedAmount;// AmountAfterTax -(AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
                        AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
                    }
                    else
                    {
                        AmadeusRates.IsCustStateEqualtoBillingEntity = false;
                        AmadeusRates.IGSTTax = (AmountAfterTax * (AmadeusRates.IGSTTaxPercent / 100));
                        AmadeusRates.Amount = AmountAfterTax; ;// - AmadeusRates.IGSTTax;
                        AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
                    }


                    //}
                }
                AmadeusRates.GuarenteeCode = GuarenteeCode;
                Session["GuaranteeCode"] = AmadeusRates.GuarenteeCode;

            }
            catch (Exception ex)
            {
                AmadeusRates = null;
                throw ex;
            }
            return AmadeusRates;
        }
        private string GetXmlChildNodes(XmlNode xNode, String pField)
        {
            string Result = string.Empty;
            try
            {
                if ((xNode.Attributes["" + pField + ""].HasChildNodes))
                {
                    Result = xNode.Attributes["" + pField + ""].Value;
                }

            }
            catch (Exception ex)
            {
                Result = string.Empty;
            }
            return Result;

        }
        public ActionResult SaveBooingDetailsGDS(Models.OfflineBookingModel model)
        {
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            Models.OfflineBookingModel MultipleBooking = new Models.OfflineBookingModel();

            List<CLayer.Rates> GDSRates = new List<CLayer.Rates>();
            string offlineBookingMode = Convert.ToString(Session["offlineBookingMode"]);
            int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(model.PropertyId);
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                if (offlineBookingMode == "ADD")
                {
                    GDSRates = GetGDSRates(model);
                }
            }
            data.BookingId = model.BookingId;
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

            Session["CheckIn"] = CheckInt;

            DateTime CheckOutt = DateTime.MinValue;
            DateTime.TryParse(model.CheckOut, out CheckOutt);

            data.CheckIn = CheckInt;
            data.CheckOut = CheckOutt;
            data.GuestEmail = model.GuestEmail;
            data.GuestName = model.GuestName;
            data.GuestPhone = model.GuestPhone == null ? "" : model.GuestPhone;

            data.OtherService = model.OtherService;
            data.TotalAmtForBuyPrice = model.TotalAmtForBuyPrice;

            decimal DailyRate = 0;
            decimal IGSTTax = 0;
            decimal CGSTTax = 0;
            decimal SGSTTax = 0;
            decimal SGSTTaxPercent = 0;
            decimal CGSTTaxPercent = 0;
            decimal IGSTTaxPercent = 0;
            decimal TotalGSTTaxPercent = 0;
            decimal TotalGSTTaxAmount = 0;
            decimal TotalByPrice = 0;
            if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
            {
                string GSTREGNO = BLayer.OfflineBooking.GetGSTRegNo(model.OfflineBookingId);
                if (offlineBookingMode == "ADD")
                {
                    if (GDSRates[0].IsCustStateEqualtoBillingEntity)
                    {
                        CGSTTax = GDSRates[0].CGSTTax;
                        SGSTTax = GDSRates[0].SGSTTax;
                        CGSTTaxPercent = GDSRates[0].CGSTTaxPercent;
                        SGSTTaxPercent = GDSRates[0].SGSTTaxPercent;
                        TotalGSTTaxPercent = CGSTTaxPercent + SGSTTaxPercent;
                        TotalGSTTaxAmount = CGSTTax + SGSTTax;

                        model.StaxForBuyPrice = (GSTREGNO == "NOT__REGISTERED") ? 0 : Math.Round(TotalGSTTaxPercent, 0);
                        ViewBag.StaxForBuyPrice = Math.Round(TotalGSTTaxPercent, 0);
                        ViewBag.StaxForBuyPriceamount = Math.Round(TotalGSTTaxAmount, 0);
                    }
                    else
                    {
                        IGSTTax = GDSRates[0].IGSTTax;
                        IGSTTaxPercent = GDSRates[0].IGSTTaxPercent;
                        model.StaxForBuyPrice = (GSTREGNO == "NOT__REGISTERED") ? 0 : Math.Round(TotalGSTTaxPercent, 0);
                        ViewBag.StaxForBuyPrice = Math.Round(IGSTTaxPercent, 0);
                        ViewBag.StaxForBuyPriceamount = Math.Round(IGSTTax, 0);

                    }
                    ViewBag.InventoryAPIType
                        = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                    if (GSTREGNO == "NOT__REGISTERED")
                    {
                        model.AvgDailyRateBefreStaxForBuyPrice = Math.Round(GDSRates[0].GDSAmount);//Math.Round(GDSRates.Sum(X => X.Amount) + ViewBag.StaxForBuyPriceamount);
                        model.StaxForBuyPrice = 0;
                        model.TotalAmtForBuyPrice = 0;
                    }
                    else
                    {
                        model.AvgDailyRateBefreStaxForBuyPrice = Math.Round(GDSRates.Sum(X => X.Amount), 0);
                    }

                }
            }

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


            data.CustomPropertyId = model.CustomPropertyId;
            data.OfflineBookingId = model.OfflineBookingId;
            data.HSNCodeCodeID = model.HSNCode;

            data.SupplierCancellationDone = model.SupplierCancellationDone;
            data.SaveStatus = (int)CLayer.OfflineBooking.Statuslist.Save;



            long BookingID = BLayer.OfflineBooking.SaveMultipleBooking(data);


            // Delete tax 
            BLayer.OfflineBooking.DeleteMultipleTax(BookingID);

            //      double d;
            if (model.TaxPercentageList_ServiceByPrice != null && model.TaxPercentageList_ServiceByPrice.Count() > 0)
            {
                List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                foreach (Models.TaxPercentage item in model.TaxPercentageList_ServiceByPrice)
                {
                    CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = model.OfflineBookingId;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = TEXT_GST; // "GST";
                    cls.Type = TEXT_BUYPRICE; // "Buy price";
                    cls.BookingID = BookingID;
                    ////d = 0;
                    ////if(double.TryParse(item.TaxPercent,out d))
                    ////{
                    ////    if (d > 0)
                    ////    {
                    ////        list.Add(cls);
                    ////    }
                    ////}
                    list.Add(cls);

                }
                BLayer.OfflineBooking.save_OfflineTaxesMultiple(list);
            }
            if (model.TaxPercentageList_OthersByPrice != null && model.TaxPercentageList_OthersByPrice.Count() > 0)
            {
                List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                foreach (Models.TaxPercentage item in model.TaxPercentageList_OthersByPrice)
                {
                    CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = model.OfflineBookingId;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = TEXT_GSTOTHERS; // "GST - Others";
                    cls.Type = TEXT_BUYPRICE; // "Buy price";
                    cls.BookingID = BookingID;
                    //d = 0;
                    //if (double.TryParse(item.TaxPercent, out d))
                    //{
                    //    if (d > 0)
                    //    {
                    //        list.Add(cls);
                    //    }
                    //}
                    list.Add(cls);

                }
                BLayer.OfflineBooking.save_OfflineTaxesMultiple(list);
            }



            //sale Price breakUp
            if (model.TaxPercentageList_ServiceSalePrice != null && model.TaxPercentageList_ServiceSalePrice.Count() > 0)
            {
                List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                foreach (Models.TaxPercentage item in model.TaxPercentageList_ServiceSalePrice)
                {
                    CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = model.OfflineBookingId;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = TEXT_GST; // "GST";
                    cls.Type = TEXT_SALEPRICE; // "Sale price";
                    cls.BookingID = BookingID;
                    //d = 0;
                    //if (double.TryParse(item.TaxPercent, out d))
                    //{
                    //    if (d > 0)
                    //    {
                    //        list.Add(cls);
                    //    }
                    //}
                    list.Add(cls);

                }
                BLayer.OfflineBooking.save_OfflineTaxesMultiple(list);
            }
            if (model.TaxPercentageList_OthersSalePrice != null && model.TaxPercentageList_OthersSalePrice.Count() > 0)
            {
                List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                foreach (Models.TaxPercentage item in model.TaxPercentageList_OthersSalePrice)
                {
                    CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = model.OfflineBookingId;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = TEXT_GSTOTHERS; // "GST - Others";
                    cls.Type = TEXT_SALEPRICE; // "Sale price";
                    cls.BookingID = BookingID;
                    //    list.Add(cls);
                    //d = 0;
                    //if (double.TryParse(item.TaxPercent, out d))
                    //{
                    //    if (d > 0)
                    //    {
                    //        list.Add(cls);
                    //    }
                    //}
                    list.Add(cls);
                }
                BLayer.OfflineBooking.save_OfflineTaxesMultiple(list);
            }

            BLayer.OfflineBooking.FixAmounts(model.OfflineBookingId);

            MultipleBooking.OfflineBookingId = model.OfflineBookingId;
            MultipleBooking.GSTStateDiffrent = model.GSTStateDiffrent;
            MultipleBooking.AccommodationTypeName = "";
            MultipleBooking.NoOfPaxAdult = 0;
            MultipleBooking.NoOfPaxChild = 0;
            MultipleBooking.HotelConformationNo = "";
            MultipleBooking.GuestName = "";
            MultipleBooking.GuestEmail = "";
            MultipleBooking.AvgDailyRateBefreStaxForBuyPrice = 0;
            MultipleBooking.StaxForBuyPrice = 0;
            //  modeldata.OfflineBookingId = model.OfflineBookingId;
            // "NewMultipleBooking", MultipleBooking
            return RedirectToAction("NewMultipleBooking", "OfflineBookingGST", new { OfflineBookingId = model.OfflineBookingId, GSTStateDiffrent = model.GSTStateDiffrent });

            //return RedirectToAction("MultipleBooking", new
            //{
            //    OfficeBookingID = data.OfflineBookingId
            //});
            //
            //
            //

        }

        public ActionResult SaveBooingDetails(Models.OfflineBookingModel model)
        {
            CLayer.OfflineBooking data = new CLayer.OfflineBooking();
            Models.OfflineBookingModel MultipleBooking = new Models.OfflineBookingModel();


            data.BookingId = model.BookingId;
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
            data.GuestPhone = model.GuestPhone == null ? "" : model.GuestPhone;

            Session["GuestEmail"] = data.GuestEmail;
            Session["GuestName"] = data.GuestName;
            Session["GuestPhone"] = data.GuestPhone;

            data.OtherService = model.OtherService;
            data.OtherServiceNature = model.OtherServiceNature;
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


            data.CustomPropertyId = model.CustomPropertyId;
            data.OfflineBookingId = model.OfflineBookingId;
            data.HSNCodeCodeID = model.HSNCode;
            data.PlaceOfSupply = model.PlaceOfSupply;
            data.HSNCodeForSalesService = model.HSNCodeForSalesService;

            data.SupplierCancellationDone = model.SupplierCancellationDone;
            data.SaveStatus = (int)CLayer.OfflineBooking.Statuslist.Save;

            #region GDS BOOKING
            //gds save
            string offlineBookingMode = Convert.ToString(Session["offlineBookingMode"]);
            if (offlineBookingMode == "ADD")
            {
                bool BookingStatus = true;
                int InventoryAPIType = BLayer.Property.GetInventoryAPITypeId(model.PropertyId);
                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    #region HOTEL SELL
                    string Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELSELLACTION); ;

                    CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                    string HotelId = Convert.ToString(Session["HotelCode"]);




                    List<CLayer.RoomStaysResult> RoomStaysResultList = APIUtility.AmedusHotelRoomStaysResultList;

                    RoomStaysResultList = (List<CLayer.RoomStaysResult>)Session["RoomStaysResult"];

                    string SoapHeaderStateful = string.Empty;
                    foreach (var item in RoomStaysResultList)
                    {
                        string BookingCode = item.BookingCode;
                        string HotelSellSoapBody = APIUtility.Hotel_Sell(HotelId, BookingCode);
                        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        string result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);



                        if (APIUtility.ReadGDSError(result, (int)CLayer.ObjectStatus.GDSType.HotelSell) == "UNABLE TO PROCESS")
                        {
                            result = APIUtility.GetAmedusResult(Action, SoapHeaderStateful, HotelSellSoapBody);
                        }
                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSell, model.OfficeBookingID, 2);

                        #endregion Transaction log end

                        if (!APIUtility.CheckHotelBookingErrorExists(result))
                        {
                            Serializer HotelSell = new Serializer();
                            CLayer.HotelSell.Envelope HotelSellResult = HotelSell.Deserialize<CLayer.HotelSell.Envelope>(result);

                            Session["HotelSellStatus"] = "success";
                            Session["SessionId"] = HotelSellResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelSellResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelSellResult.Header.Session.SecurityToken;
                            BookingStatus = false;
                        }
                        else
                        {
                            Serializer HotelSellError = new Serializer();
                            CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(result);
                            Session["HotelSellStatus"] = "error";
                            Session["SessionId"] = HotelSellErrorResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(HotelSellErrorResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = HotelSellErrorResult.Header.Session.SecurityToken;
                            ViewBag.HotelSellResult = "RequestFailed";
                            TempData["Errorcomes"] = "RequestFailed";

                            List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCode);
                            foreach (var itemerror in objBookItemsError)
                            {
                                BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, result);
                            }


                            #region PNR CANCEL

                            StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                            //   BookingStatus = objBooking.BookingDecline(bookingId);

                            BookingStatus = BookingCancel(model.OfficeBookingID, 2, "");

                            //  string resultCancel = APIUtility.ExecutePNRCancel("", (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRGeneration);

                            BookingStatus = true;

                            return JavaScript("GDSBookingFailed()");

                            //    return Content("Selected room not available for Booking");
                            //   return BookingStatus;
                            #endregion

                        }



                    }
                    #endregion

                    #region PNR MULTIELEMENTS-FINAL
                    Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSPNRADDACTION);
                    SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                    APIUtility.SoapHeaderStateful = SoapHeaderStateful;

                    string BookingCodeFinal = RoomStaysResultList.OrderBy(x => x.AmountAfterTax).FirstOrDefault().BookingCode;
                    string PNRADDSoapBody = APIUtility.PNR_AddMultielements(HotelId, BookingCodeFinal, "test@test.com", 10);

                    TempData["RoomStaysResult"] = RoomStaysResultList;
                    APIUtility.AmedusHotelRoomStaysResultList = RoomStaysResultList;

                    string resultFinal = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRADDSoapBody);

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRAddMultiElementsConfirm, model.OfficeBookingID, 2);

                    #endregion Transaction log end

                    Serializer pnrser = new Serializer();

                    CLayer.PNRAddElementsConfirm.Envelope PNRAddResult = pnrser.Deserialize<CLayer.PNRAddElementsConfirm.Envelope>(resultFinal);
                    Session["ControlNumber"] = PNRAddResult.Body.PNR_Reply.pnrHeader.reservationInfo.reservation.controlNumber;
                    //if(string.IsNullOrEmpty(Convert.ToString(Session["ControlNumber"])))
                    //{
                    //    Session["ControlNumber"]= PNRAddResult.Body.PNR_Reply.originDestinationDetails.originDestination.
                    //}

                    if (APIUtility.CheckHotelBookingErrorExists(resultFinal))
                    {

                        //Serializer HotelSellError = new Serializer();
                        //CLayer.HotelSell.Envelope HotelSellErrorResult = HotelSellError.Deserialize<CLayer.HotelSell.Envelope>(resultFinal);

                        Serializer PNRADDError = new Serializer();
                        CLayer.PNRAddElementsError.Envelope PNRAddErrorResult = null;
                        CLayer.PNRAddElementsConfirmError.Envelope PNRAddErrorResultConfirm = null;

                        try
                        {
                            PNRAddErrorResult = PNRADDError.Deserialize<CLayer.PNRAddElementsError.Envelope>(resultFinal);
                        }
                        catch (Exception ex)
                        {
                            PNRAddErrorResultConfirm = PNRADDError.Deserialize<CLayer.PNRAddElementsConfirmError.Envelope>(resultFinal);
                        }


                        Session["HotelSellStatus"] = "error";
                        if (PNRAddErrorResult != null)
                        {
                            Session["SessionId"] = PNRAddErrorResult.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(PNRAddErrorResult.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = PNRAddErrorResult.Header.Session.SecurityToken;
                            Session["ControlNumber"] = PNRAddErrorResult.Body.PNR_Reply.originDestinationDetails.itineraryInfo.generalOption.Where(x => x.optionDetail.type == "CF").FirstOrDefault();
                        }
                        else
                        {
                            Session["SessionId"] = PNRAddErrorResultConfirm.Header.Session.SessionId;
                            Session["SequenceNumber"] = Convert.ToInt32(PNRAddErrorResultConfirm.Header.Session.SequenceNumber);
                            Session["SecurityToken"] = PNRAddErrorResultConfirm.Header.Session.SecurityToken;
                            Session["ControlNumber"] = PNRAddErrorResultConfirm.Body.PNR_Reply.originDestinationDetails.itineraryInfo.generalOption.Where(x => x.optionDetail.type == "CF").FirstOrDefault();
                        }

                        //List<CLayer.BookingItem> objBookItemsError = BLayer.BookingItem.GetAccomodationFromBookingCode(BookingCodeFinal);
                        //foreach (var itemerror in objBookItemsError)
                        //{
                        //    BLayer.BookingItem.UpdateHotelGDSError(itemerror.BookingId, itemerror.AccommodationId, resultFinal);
                        //}

                        #region PNR CANCEL
                        ViewBag.HotelSellResult = "RequestFailed";
                        TempData["Errorcomes"] = "RequestFailed";
                        string ControlNumber = Convert.ToString(Session["ControlNumber"]);
                        // string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize);
                        //     return false;
                        StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController objBooking = new StayBazar.Areas.Admin.Controllers.BookingRequestTransactionsController();
                        //  BookingStatus = objBooking.BookingDecline(bookingId);
                        BookingStatus = BookingCancel(model.OfflineBookingId, 2, ControlNumber);
                        BookingStatus = true;
                        return Content("Selected room not available for booking");
                        #endregion

                    }
                    else
                    {
                        BookingStatus = false;

                        BLayer.OfflineBooking.UpdateGDSHotelConfirmNumber(model.OfflineBookingId, Convert.ToString(Session["ControlNumber"]));

                        #region PNR_Retrieve
                        Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELPNRRETRIEVEACTION);
                        SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"]), Session["SecurityToken"].ToString());
                        APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        string PNRRetrieveSoapBody = APIUtility.PNR_Retrieve(Convert.ToString(Session["ControlNumber"]));
                        string resultRetrieve = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, PNRRetrieveSoapBody);
                        #region Transaction Log 

                        APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), resultFinal, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.PNRRetrieve, model.OfflineBookingId, 2);

                        #endregion Transaction log end
                        #endregion

                        //#region Hotel Completereservationinfodetails
                        //Action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELCOMPLETERESERVATION);
                        //SoapHeaderStateful = APIUtility.SetStatefulSoapHeader(Action, Session["SessionId"].ToString(), Convert.ToInt32(Session["SequenceNumber"])+1, Session["SecurityToken"].ToString(), true);
                        //APIUtility.SoapHeaderStateful = SoapHeaderStateful;
                        //string HotelCompleteReservation = APIUtility.Hotel_CompleteReservationDetails(Convert.ToString(Session["ControlNumber"]));
                        //string resultotelCompleteReservation = APIUtility.GetAmedusResult(Action, APIUtility.SoapHeaderStateful, HotelCompleteReservation);


                        //#endregion 
                    }

                    #endregion

                }
            }

            //gds save end
            #endregion

            long BookingID = BLayer.OfflineBooking.SaveMultipleBooking(data);


            // Delete tax 
            BLayer.OfflineBooking.DeleteMultipleTax(BookingID);

            //      double d;
            if (model.TaxPercentageList_ServiceByPrice != null && model.TaxPercentageList_ServiceByPrice.Count() > 0)
            {
                List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                foreach (Models.TaxPercentage item in model.TaxPercentageList_ServiceByPrice)
                {
                    CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = model.OfflineBookingId;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = TEXT_GST; // "GST";
                    cls.Type = TEXT_BUYPRICE; // "Buy price";
                    cls.BookingID = BookingID;
                    ////d = 0;
                    ////if(double.TryParse(item.TaxPercent,out d))
                    ////{
                    ////    if (d > 0)
                    ////    {
                    ////        list.Add(cls);
                    ////    }
                    ////}
                    list.Add(cls);

                }
                BLayer.OfflineBooking.save_OfflineTaxesMultiple(list);
            }
            if (model.TaxPercentageList_OthersByPrice != null && model.TaxPercentageList_OthersByPrice.Count() > 0)
            {
                List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                foreach (Models.TaxPercentage item in model.TaxPercentageList_OthersByPrice)
                {
                    CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = model.OfflineBookingId;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = TEXT_GSTOTHERS; // "GST - Others";
                    cls.Type = TEXT_BUYPRICE; // "Buy price";
                    cls.BookingID = BookingID;
                    //d = 0;
                    //if (double.TryParse(item.TaxPercent, out d))
                    //{
                    //    if (d > 0)
                    //    {
                    //        list.Add(cls);
                    //    }
                    //}
                    list.Add(cls);

                }
                BLayer.OfflineBooking.save_OfflineTaxesMultiple(list);
            }



            //sale Price breakUp
            if (model.TaxPercentageList_ServiceSalePrice != null && model.TaxPercentageList_ServiceSalePrice.Count() > 0)
            {
                List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                foreach (Models.TaxPercentage item in model.TaxPercentageList_ServiceSalePrice)
                {
                    CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = model.OfflineBookingId;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = TEXT_GST; // "GST";
                    cls.Type = TEXT_SALEPRICE; // "Sale price";
                    cls.BookingID = BookingID;
                    //d = 0;
                    //if (double.TryParse(item.TaxPercent, out d))
                    //{
                    //    if (d > 0)
                    //    {
                    //        list.Add(cls);
                    //    }
                    //}
                    list.Add(cls);

                }
                BLayer.OfflineBooking.save_OfflineTaxesMultiple(list);
            }
            if (model.TaxPercentageList_OthersSalePrice != null && model.TaxPercentageList_OthersSalePrice.Count() > 0)
            {
                List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
                foreach (Models.TaxPercentage item in model.TaxPercentageList_OthersSalePrice)
                {
                    CLayer.TaxPercentage cls = new CLayer.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = model.OfflineBookingId;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = TEXT_GSTOTHERS; // "GST - Others";
                    cls.Type = TEXT_SALEPRICE; // "Sale price";
                    cls.BookingID = BookingID;
                    //    list.Add(cls);
                    //d = 0;
                    //if (double.TryParse(item.TaxPercent, out d))
                    //{
                    //    if (d > 0)
                    //    {
                    //        list.Add(cls);
                    //    }
                    //}
                    list.Add(cls);
                }
                BLayer.OfflineBooking.save_OfflineTaxesMultiple(list);
            }

            BLayer.OfflineBooking.FixAmounts(model.OfflineBookingId);

            MultipleBooking.OfflineBookingId = model.OfflineBookingId;
            MultipleBooking.GSTStateDiffrent = model.GSTStateDiffrent;
            MultipleBooking.AccommodationTypeName = "";
            MultipleBooking.NoOfPaxAdult = 0;
            MultipleBooking.NoOfPaxChild = 0;
            MultipleBooking.HotelConformationNo = "";
            MultipleBooking.GuestName = "";
            MultipleBooking.GuestEmail = "";
            MultipleBooking.AvgDailyRateBefreStaxForBuyPrice = 0;
            MultipleBooking.StaxForBuyPrice = 0;



            //  modeldata.OfflineBookingId = model.OfflineBookingId;
            // "NewMultipleBooking", MultipleBooking
            return RedirectToAction("NewMultipleBooking", "OfflineBookingGST", new { OfflineBookingId = model.OfflineBookingId, GSTStateDiffrent = model.GSTStateDiffrent });

            //return RedirectToAction("MultipleBooking", new
            //{
            //    OfficeBookingID = data.OfflineBookingId
            //});
        }

        public bool BookingCancel(long BookingId, int InventoryAPIType, string ControlNumber = "")
        {
            try
            {
                // BLayer.OfflineBooking.UpdateSaveStatus((int)CLayer.ObjectStatus.BookingStatus.Cancelled, BookingId);

                #region GDS Booking Cancel      

                if (InventoryAPIType == (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus)
                {
                    // ControlNumber = BLayer.Bookings.GetGDSBookingControlNumber(BookingId);
                    if (string.IsNullOrEmpty(ControlNumber))
                    {
                        ControlNumber = Convert.ToString(System.Web.HttpContext.Current.Session["ControlNumber"]);
                    }

                    int OptionCode = (ControlNumber == "") ? 0 : (int)CLayer.ObjectStatus.PNROptionCodes.OptionPNRFinalize;
                    string resultCancel = APIUtility.ExecutePNRCancel(ControlNumber, OptionCode);

                    #region Transaction Log 

                    string LoggedInUser = Convert.ToString(System.Web.HttpContext.Current.Session["LoggedInUser"]);
                    string currentUrl = string.Empty;

                    try
                    {
                        APIUtility.GenerateGDSTransactionLog(currentUrl, resultCancel, Convert.ToInt32(LoggedInUser), (int)CLayer.ObjectStatus.GDSType.PNRCancel, BookingId, 2);

                    }
                    catch (Exception ex)
                    {
                        APIUtility.GenerateGDSTransactionLog(currentUrl, resultCancel, Convert.ToInt32(LoggedInUser), (int)CLayer.ObjectStatus.GDSType.PNRCancel, BookingId, 2);

                    }

                    #endregion Transaction log end
                }
                #endregion 

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return false;
            }
            return true;
        }
        //public List<CLayer.Rates> GetAmedusRates(Models.SimpleBookingModel bookingData, List<CLayer.RoomStaysResult> RoomStaysResultList, string HotelCode)
        //{
        //    List<CLayer.Rates> rtes = new List<CLayer.Rates>();
        //    CLayer.Rates AmadeusRates = null;
        //    string result = "";
        //    long AccomSelect = 0;
        //    string BookingCode = "";

        //    string AgeQualifyingCode = "10";
        //    // string GuestCount = "2";

        //    int BedRoomsQuantity = Convert.ToInt32(Session["Bedrooms"]);
        //    int GuestCount = Convert.ToInt32(Session["Adults"]) + Convert.ToInt32(Session["Children"]); ;
        //    BedRoomsQuantity = BedRoomsQuantity <= 0 ? 1 : BedRoomsQuantity;
        //    GuestCount = GuestCount <= 0 ? 2 : GuestCount;

        //    //Customer state id
        //    int CustomerStateID = BLayer.State.GetCustomerStateID(Convert.ToInt32(Session["LoggedInUser"]));

        //    //Billing entity state id
        //    int BillingEntityStateID = BLayer.State.GetBillingEntityStateID(bookingData.PropertyId);
        //    BillingEntityStateID = (BillingEntityStateID == 0) ? Convert.ToInt32(BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARDEFAULTBILLINGENTITY)) : BillingEntityStateID;

        //    try
        //    {
        //        //Pricing details from enhanced pricing details API
        //        string SecurityToken = Convert.ToString(Session["SecurityToken"]);
        //        string SequenceNumber = Convert.ToString(Session["SequenceNumber"]);
        //        string SessionId = Convert.ToString(Session["SessionId"]);

        //        CLayer.EnhancedPricing.Envelope PricingResult = null;

        //        CLayer.EnhancedPricingAdvanced.Envelope PricingResultAdv = null;

        //        ArrayList AccommodationSelected = GetAccommodationSelected(bookingData.BookingData);

        //        var accom = BLayer.Accommodation.GetAllAccByPropertyid(bookingData.PropertyId);
        //        for (int i = 0; i < AccommodationSelected.Count; i++)
        //        {
        //            AccomSelect = Convert.ToInt64(AccommodationSelected[i]);
        //            BookingCode = accom.Where(x => x.AccommodationId == Convert.ToInt64(AccommodationSelected[i])).FirstOrDefault().BookingCode;
        //            //  RoomStaysResultList = RoomStaysResultList.Where(x=>x.BookingCode==BookingCode).ToList();
        //            foreach (var item in RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList())
        //            {
        //                AmadeusRates = new CLayer.Rates();
        //                PropertyController objProperty = new PropertyController();
        //                result = objProperty.GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, item.RoomTypeCode, HotelCode, item.RatePlanCode, item.BookingCode, bookingData.BookCheckIn.ToString("yyyy-MM-dd"), bookingData.BookCheckOut.ToString("yyyy-MM-dd"), "", AgeQualifyingCode, GuestCount.ToString(), bookingData.PropertyId);

        //                #region Transaction Log 

        //                APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelEnhancedPricing, 0);

        //                #endregion Transaction log end

        //                Serializer ser = new Serializer();

        //                CLayer.EnhancedPricing.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItem = null;
        //                CLayer.EnhancedPricingAdvanced.OTA_HotelAvailRSRoomStaysRoomStayRoomRates RatesItemAdv = null;
        //                try
        //                {
        //                    PricingResult = ser.Deserialize<CLayer.EnhancedPricing.Envelope>(result);
        //                    RatesItem = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
        //                }
        //                catch (Exception ex)
        //                {
        //                    try
        //                    {
        //                        PricingResultAdv = ser.Deserialize<CLayer.EnhancedPricingAdvanced.Envelope>(result);
        //                        RatesItemAdv = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates;
        //                    }
        //                    catch (Exception innerex)
        //                    {
        //                        AmadeusRates = ReadGDSPrice(result, Convert.ToInt64(AccommodationSelected[i]), BookingCode, CustomerStateID, BillingEntityStateID);
        //                        rtes.Add(AmadeusRates);
        //                        return rtes;
        //                    }

        //                }



        //                if (RatesItem != null)
        //                {
        //                    AmadeusRates.NoofAcc = 10;
        //                    decimal TaxPercent = RatesItem.RoomRate.Total.Taxes.Tax.Percent;
        //                    TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
        //                    decimal TaxAmount = (RatesItem.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
        //                    AmadeusRates.Amount = RatesItem.RoomRate.Total.AmountAfterTax;// - TaxAmount;
        //                    AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
        //                    Session["GDSAmount"] = RatesItem.RoomRate.Total.AmountAfterTax;
        //                    Session["GDSConvertedAmount"] = AmadeusRates.Amount;

        //                    AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
        //                    AmadeusRates.taxpercent = TaxPercent;
        //                    decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

        //                    //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) :Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
        //                    //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
        //                    //AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

        //                    AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
        //                    AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
        //                    AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : SBTaxPercent;

        //                    AmadeusRates.BookingCode = BookingCode;

        //                    if (CustomerStateID == BillingEntityStateID)
        //                    {

        //                        AmadeusRates.IsCustStateEqualtoBillingEntity = true;
        //                        AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
        //                        AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));
        //                        AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) - (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
        //                        AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
        //                    }
        //                    else
        //                    {
        //                        AmadeusRates.IsCustStateEqualtoBillingEntity = false;
        //                        AmadeusRates.IGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
        //                        AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItem.RoomRate.Total.AmountAfterTax) - AmadeusRates.IGSTTax;
        //                        AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
        //                    }



        //                }
        //                else if (RatesItemAdv != null)
        //                {
        //                    AmadeusRates.NoofAcc = 10;
        //                    decimal TaxPercent = RatesItemAdv.RoomRate.Total.Taxes[0].Percent;
        //                    TaxPercent = (TaxPercent < 1) ? Math.Round(TaxPercent, 0) : TaxPercent;
        //                    decimal TaxAmount = (RatesItemAdv.RoomRate.Total.AmountAfterTax * (TaxPercent / 100));
        //                    AmadeusRates.Amount = RatesItemAdv.RoomRate.Total.AmountAfterTax;// - TaxAmount;
        //                    AmadeusRates.Amount = (Convert.ToString(Session["GDSCountry"]) == "") ? AmadeusRates.Amount : APIUtility.GetGDSConvertedAmount(AmadeusRates.Amount);
        //                    Session["GDSAmount"] = RatesItem.RoomRate.Total.AmountAfterTax;
        //                    Session["GDSConvertedAmount"] = AmadeusRates.Amount;

        //                    AmadeusRates.AccommodationId = Convert.ToInt64(AccommodationSelected[i]);// accom.Where(x => x.BookingCode == item.BookingCode).FirstOrDefault().AccommodationId;
        //                    AmadeusRates.taxpercent = TaxPercent;
        //                    decimal SBTaxPercent = APIUtility.GetGSTRate(AmadeusRates.Amount);

        //                    //AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.CGST_TAXRATE));
        //                    //AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.SGST_TAXRATE));
        //                    //AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.IGST_TAXRATE));

        //                    AmadeusRates.CGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
        //                    AmadeusRates.SGSTTaxPercent = (TaxPercent > 0) ? (TaxPercent / 2) : (SBTaxPercent / 2);
        //                    AmadeusRates.IGSTTaxPercent = (TaxPercent > 0) ? TaxPercent : SBTaxPercent;
        //                    AmadeusRates.BookingCode = BookingCode;

        //                    if (CustomerStateID == BillingEntityStateID)
        //                    {

        //                        AmadeusRates.IsCustStateEqualtoBillingEntity = true;
        //                        AmadeusRates.SGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.SGSTTaxPercent / 100));
        //                        AmadeusRates.CGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.CGSTTaxPercent / 100));
        //                        AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) - (AmadeusRates.SGSTTax + AmadeusRates.CGSTTax);
        //                        AmadeusRates.TotalRateTax = AmadeusRates.SGSTTax + AmadeusRates.CGSTTax;
        //                    }
        //                    else
        //                    {
        //                        AmadeusRates.IsCustStateEqualtoBillingEntity = false;
        //                        AmadeusRates.IGSTTax = (APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) * (AmadeusRates.IGSTTaxPercent / 100));
        //                        AmadeusRates.Amount = APIUtility.GetGDSConvertedAmount(RatesItemAdv.RoomRate.Total.AmountAfterTax) - AmadeusRates.IGSTTax;
        //                        AmadeusRates.TotalRateTax = AmadeusRates.IGSTTax;
        //                    }


        //                    //}
        //                }
        //                if (PricingResult != null)
        //                {
        //                    if (PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
        //                    {
        //                        AmadeusRates.GuarenteeCode = PricingResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();
        //                    }
        //                }

        //                if (PricingResultAdv != null)
        //                {
        //                    if (PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee != null)
        //                    {
        //                        AmadeusRates.GuarenteeCode = PricingResultAdv.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.Guarantee.GuaranteeCode.ToString();

        //                    }
        //                }

        //                Session["GuaranteeCode"] = AmadeusRates.GuarenteeCode;

        //                rtes.Add(AmadeusRates);
        //            }
        //        }

        //        if (PricingResult != null)
        //        {
        //            Session["SessionId"] = PricingResult.Header.Session.SessionId;
        //            Session["SequenceNumber"] = Convert.ToInt32(PricingResult.Header.Session.SequenceNumber);
        //            Session["SecurityToken"] = PricingResult.Header.Session.SecurityToken;
        //        }
        //        else
        //        {
        //            Session["SessionId"] = PricingResultAdv.Header.Session.SessionId;
        //            Session["SequenceNumber"] = Convert.ToInt32(PricingResultAdv.Header.Session.SequenceNumber);
        //            Session["SecurityToken"] = PricingResultAdv.Header.Session.SecurityToken;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //       // AmadeusRates = ReadGDSPrice(result, AccomSelect, BookingCode, CustomerStateID, BillingEntityStateID);
        //        if (AmadeusRates != null)
        //        {
        //            rtes.Add(AmadeusRates);
        //        }
        //        else
        //        {
        //            rtes = null;
        //        }


        //    }
        //    return rtes;
        //}

        public ActionResult _BookingDetailsList(Models.OfflineBookingModel model)
        {
            Models.OfflineBookingModel MultipleBooking = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            MultipleBooking.OfflineBookingList = BLayer.OfflineBooking.BookedList(model.OfflineBookingId);


            return View("_BookingDetailsList", MultipleBooking.OfflineBookingList);
        }
        public ActionResult EditBookedDetails(long BookingId)
        {
            Session["offlineBookingMode"] = "EDIT";

            CLayer.OfflineBooking result = new CLayer.OfflineBooking();
            result = BLayer.OfflineBooking.EditBookedDetails(BookingId);
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();

            data.Accommodationid = result.Accommodationid;
            data.Accommodationtypeid = result.Accommodationtypeid;
            data.AccommodationTypeName = result.AccommodatoinTypename;
            data.StayCategoryName = result.StayCategoryName;
            data.StayCategoryid = result.StayCategoryid;
            data.NoOfNight = result.NoOfNight;
            data.NoOfRooms = result.NoOfRooms;
            data.NoOfPaxAdult = result.NoOfPaxAdult;
            data.NoOfPaxChild = result.NoOfPaxChild;
            data.CheckIn = Convert.ToString(result.CheckIn.ToShortDateString());
            data.CheckOut = Convert.ToString(result.CheckOut.ToShortDateString());

            data.GuestEmail = result.GuestEmail;
            data.GuestName = result.GuestName;
            data.GuestPhone = result.GuestPhone;

            data.TotalAmtForBuyPrice = result.TotalAmtForBuyPrice;
            data.AvgDailyRateBefreStaxForBuyPrice = result.AvgDailyRateBefreStaxForBuyPrice;
            data.StaxForBuyPrice = result.StaxForBuyPrice;
            data.BuyPriceforotherservicesForBuyprice = result.BuyPriceforotherservicesForBuyprice;
            data.StaxForotherBuyPrice = result.StaxForotherBuyPrice;
            data.TotalAmtotherForBuyPrice = result.TotalAmtotherForBuyPrice;
            data.TotalBuyPrice = result.TotalBuyPrice;

            data.TotalAmtForSalePrice = result.TotalAmtForSalePrice;
            data.AvgDailyRateBefreStaxForSalePrice = result.AvgDailyRateBefreStaxForSalePrice;
            data.StaxForSalePrice = result.StaxForSalePrice;
            data.BuyPriceforotherservicesForSalePrice = result.BuyPriceforotherservicesForSalePrice;
            data.StaxForotherForSalePrice = result.StaxForotherForSalePrice;
            data.TotalAmtotherForSalePrice = result.TotalAmtotherForSalePrice;
            data.TotalSalePrice = result.TotalSalePrice;
            data.HotelConformationNo = result.HotelConfirmationNo;
            data.HSNCode = result.HSNCodeCodeID;
            data.BookingId = result.BookingId;
            data.OfflineBookingId = result.OfflineBookingId;
            data.SupplierCancellationDone = result.SupplierCancellationDone;
            data.OtherServiceNature = result.OtherServiceNature;
            data.PlaceOfSupply = result.PlaceOfSupply;


            CLayer.OfflineBooking offprodt = BLayer.OfflineBooking.GetAllpropertyDetails(data.OfflineBookingId);

            if (offprodt != null)
            {
                data.PropertyState = offprodt.PropertyState;
                data.PropertyCountry = offprodt.PropertyCountry;
                data.SupplierCountry = offprodt.SupplierCountry;
                data.SupplierState = offprodt.SupplierState;
            }


            if (data.UserTypeName == (string)CLayer.Role.CORPORATE)
            {
                if (data.PropertyState != data.CustomerGstStateId && data.PropertyState != data.BillingStateID)
                {
                    data.GSTStateDiffrent = 1;
                }
            }

            List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list1 = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list3 = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list4 = new List<CLayer.TaxPercentage>();
            data.TaxPercentageList_ServiceSalePrice = new List<Models.TaxPercentage>();
            data.TaxPercentageList_ServiceByPrice = new List<Models.TaxPercentage>();

            data.TaxPercentageList_OthersSalePrice = new List<Models.TaxPercentage>();
            data.TaxPercentageList_OthersByPrice = new List<Models.TaxPercentage>();


            list = BLayer.OfflineBooking.GetAll_OfflineBookingTaxesGST(result.OfflineBookingId, "GST - Others", "Sale price", result.BookingId);
            list1 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxesGST(result.OfflineBookingId, "GST", "Sale price", result.BookingId);

            list3 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxesGST(result.OfflineBookingId, "GST - Others", "Buy price", result.BookingId);
            list4 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxesGST(result.OfflineBookingId, "GST", "Buy price", result.BookingId);

            if (list != null)
            {
                foreach (var item in list)
                {
                    Models.TaxPercentage cls = new Models.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = item.TaxOfflineBookingID;
                    cls.BookingID = item.BookingID;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = item.TaxType;
                    cls.Type = item.Type;
                    cls.rowSet = " ";
                    cls.vendorId = 0;
                    data.TaxPercentageList_OthersSalePrice.Add(cls);
                }
            }
            if (list1 != null)
            {
                foreach (var item in list1)
                {
                    Models.TaxPercentage cls = new Models.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = item.TaxOfflineBookingID;
                    cls.BookingID = item.BookingID;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = item.TaxType;
                    cls.Type = item.Type;
                    cls.rowSet = " ";
                    cls.vendorId = 0;
                    data.TaxPercentageList_ServiceSalePrice.Add(cls);
                }
            }
            if (list3 != null)
            {
                foreach (var item in list3)
                {
                    Models.TaxPercentage cls = new Models.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = item.TaxOfflineBookingID;
                    cls.BookingID = item.BookingID;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = item.TaxType;
                    cls.Type = item.Type;
                    cls.rowSet = " ";
                    cls.vendorId = 0;
                    data.TaxPercentageList_OthersByPrice.Add(cls);
                }
            }
            if (list4 != null)
            {
                foreach (var item in list4)
                {
                    Models.TaxPercentage cls = new Models.TaxPercentage();
                    cls.TaxPerID = item.TaxPerID;
                    cls.TaxOfflineBookingID = item.TaxOfflineBookingID;
                    cls.BookingID = item.BookingID;
                    cls.TaxTitle = item.TaxTitle;
                    cls.TaxPercent = item.TaxPercent;
                    cls.TaxType = item.TaxType;
                    cls.Type = item.Type;
                    cls.rowSet = " ";
                    cls.vendorId = 0;
                    data.TaxPercentageList_ServiceByPrice.Add(cls);
                }
            }

            return View("~/Areas/Admin/Views/OfflineBookingGST/MultipleBooking.cshtml", data);
        }
        public ActionResult VendorDetails(long OfflineBookingId)
        {
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();
            data.OfflineBookingId = OfflineBookingId;
            List<CLayer.TaxPercentage> list = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list1 = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list3 = new List<CLayer.TaxPercentage>();
            List<CLayer.TaxPercentage> list4 = new List<CLayer.TaxPercentage>();
            data.TaxPercentageList_Service = new List<Models.TaxPercentage>();
            data.TaxPercentageList_Others = new List<Models.TaxPercentage>();
            list = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(OfflineBookingId, "GST");
            list1 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(OfflineBookingId, "GST - Others");
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



            list3 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(0, "GST");
            list4 = BLayer.OfflineBooking.GetAll_OfflineBookingTaxes(0, "GST - Others");
            foreach (var item in list3)
            {
                Models.TaxPercentage cls = new Models.TaxPercentage();
                cls.TaxPerID = 0;
                cls.TaxOfflineBookingID = 0;
                cls.TaxTitle = item.TaxTitle;
                cls.TaxPercent = item.TaxPercent;
                cls.TaxType = item.TaxType;
                data.TaxPercentageList_Service.Add(cls);
            }
            foreach (var item in list4)
            {
                Models.TaxPercentage cls = new Models.TaxPercentage();
                cls.TaxPerID = 0;
                cls.TaxOfflineBookingID = 0;
                cls.TaxTitle = item.TaxTitle;
                cls.TaxPercent = item.TaxPercent;
                cls.TaxType = item.TaxType;
                data.TaxPercentageList_Others.Add(cls);
            }

            return View("~/Areas/Admin/Views/OfflineBookingGST/_VendorDetails.cshtml", data);
        }

        [AllowAnonymous]
        public ActionResult GetVendor(string term)
        {
            var dummy = new List<object>();
            try
            {
                return Json(BLayer.OfflineBooking.GetVendor(term), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                dummy.Add(new { value = "" });
            }
            return Json(dummy, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetAccommodationTypeName(string term)
        {
            var dummy = new List<object>();
            try
            {
                return Json(BLayer.OfflineBooking.GetAccommodationTypeName(term), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                dummy.Add(new { value = "" });
            }
            return Json(dummy, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetAccommodationTypeNameGDS(long PropertyId, string CheckIn, string CheckOut, string place)
        {
            var dummy = new List<object>();
            List<object> result = new List<object>();
            string Title;
            int AccommodationTypeId;
            try
            {


                DataTable details = BLayer.Property.GetAccommodationDetailsFrompropertyid(PropertyId);
                var hotelcode = details.Rows[0]["Hotel_Id"].ToString();
                Serializer ser = new Serializer();
                Serializer HotelResults = new Serializer();
                CLayer.GDSPriceingDetails.Envelope GDSPriceingDetails = new CLayer.GDSPriceingDetails.Envelope();
                StayBazar.Models.PropertyModel property = new StayBazar.Models.PropertyModel();
                List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
                CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();

                CLayer.SearchCriteria ch = new CLayer.SearchCriteria();

                DateTime start = DateTime.Parse(CheckIn);
                DateTime end = DateTime.Parse(CheckOut);
                ch.CheckIn = start;
                ch.CheckOut = end;
                ch.Destination = place;

                string searchedresult = HotelMultiSingleAvailability(ch, false, "");
                CLayer.HotelAvailability.Envelope HotelResult = HotelResults.Deserialize<CLayer.HotelAvailability.Envelope>(searchedresult);


                var SecurityToken = HotelResult.Header.Session.SecurityToken;
                var roomstaystpe = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay;
                var SessionId = HotelResult.Header.Session.SessionId;
                var SequenceNumber = HotelResult.Header.Session.SequenceNumber.ToString();

                string hotel = GetGDS_Hotel_Details(hotelcode);
                CLayer.GDSBookingDetails.Envelope GDS_Hotel_Details = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);

                var ChainCode = "HS";
                var AgeQualifyingCode = "10";
                var count = "2";


                foreach (DataRow dt in details.Rows)
                {
                    try
                    {
                        CheckIn = start.ToString("yyyy-MM-dd");
                        CheckOut = end.ToString("yyyy-MM-dd");
                        Accommodationsss = new CLayer.Accommodation();
                        var BookingCode = dt["BookingCode"].ToString();
                        var RatePlanCode = dt["RatePlanCode"].ToString();
                        var RoomTypeCode = dt["RoomTypeCode"].ToString();
                        long AccommodationID = Convert.ToInt64(dt["accommodationid"]);

                        string priceing = GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, RoomTypeCode, hotelcode, RatePlanCode, BookingCode, CheckIn, CheckOut, ChainCode, AgeQualifyingCode, count, PropertyId);
                        GDSPriceingDetails = ser.Deserialize<CLayer.GDSPriceingDetails.Envelope>(priceing);
                        if (GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays != null)
                        {
                            result.Add(new { value = GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates.RoomRate.RoomRateDescription.Name, id = AccommodationID });
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }



                return Json(result, JsonRequestBehavior.AllowGet);

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
        public ActionResult GetVendorDetails(int id)
        {
            try
            {
                CLayer.OfflineBooking retdata = BLayer.OfflineBooking.GetVendorDetails(id);

                return Json(retdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }

        [Common.AdminRolePermission(AllowAllRoles = true)]
        [Authorize]
        public ActionResult GetVendorDetailsAutoComplete(int id)
        {
            try
            {
                CLayer.OfflineBooking retdata = BLayer.OfflineBooking.GetVendorDetailsAutoComplete(id);

                return Json(retdata, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/Admin/ErrorPage");
            }
        }


        public ActionResult _vendorList(long OfflineBookingId)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            result = BLayer.OfflineBooking.VendorList(OfflineBookingId);
            return PartialView("~/Areas/Admin/Views/OfflineBookingGST/_vendorList.cshtml", result);
        }

        public ActionResult EditVendorDetails(long BookingId)
        {
            CLayer.OfflineBooking result = new CLayer.OfflineBooking();
            result = BLayer.OfflineBooking.EditVendorDetails(BookingId);
            Models.OfflineBookingModel data = new Models.OfflineBookingModel();

            data.VendorpaymentsId = result.VendorpaymentsId;
            data.OfflineBookingId = result.OfflineBookingId;
            data.vendorname = result.vendorname;
            data.vendoraddress = result.vendoraddress;
            data.address1 = result.address1;
            data.address2 = result.address2;
            data.City = result.City;
            data.pin = result.pin;
            data.ContactPerson = result.ContactPerson;
            data.contactno = result.contactno;
            data.emailaddress = result.emailaddress;

            data.natureofservice = result.natureofservice;
            data.ByPriceBeforeTax = result.ByPriceBeforeTax;
            data.ByPriceTotal = result.ByPriceTotal;
            data.SalePriceBeforeTax = result.SalePriceBeforeTax;
            data.SalePriceTotal = result.SalePriceTotal;
            data.ByPriceGST = result.ByPriceGST;
            data.SalePriceGST = result.SalePriceGST;
            data.PlaceOfSupply = result.PlaceOfSupply;

            data.vendorId = result.vendorId;

            return View("~/Areas/Admin/Views/OfflineBookingGST/_VendorDetails.cshtml", data);
        }

        public ActionResult BookingDetailsPreview(long OfflineBookId)
        {
            Models.OfflineBookingModel model = new Models.OfflineBookingModel();
            model.OfflineBookingId = OfflineBookId;
            CLayer.OfflineBooking data = BLayer.OfflineBooking.GetOtherAmountsForOfflineBooking(OfflineBookId);
            model.ReimbursementsAmount = Math.Round(data.ReimbursementsAmount, 2);
            model.DiscountAmount = Math.Round(data.DiscountAmount, 2);
            model.OthersAmount = Math.Round(data.OthersAmount, 2);
            model.natureofreimbursements = data.natureofreimbursements;
            model.AdvanceReceived = Math.Round(data.AdvanceReceived, 2);
            return View("~/Areas/Admin/Views/OfflineBookingGST/BookingDetailsPreview.cshtml", model);
        }


        //[AllowAnonymous]
        //public ActionResult SendSupplierInvoiceAtCheckin()
        //{


        //    List<CLayer.OfflineBooking> BListGST = BLayer.OfflineBooking.GetMultipleBookingDetailsGST(@usr.OfflineBookingId);
        //    return View();


        //}
        public int CheckOfflineCustomerExist(string CustomerName1, string CustomerEmail1, int CustomerType = 7, long CustomerId = 0)
        {
            int Count = 0;
            try
            {
                Count = BLayer.OfflineBooking.CheckOfflineCustomerExist(CustomerName1, CustomerEmail1, CustomerType, CustomerId);
                return Count;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Count;
            }

        }

        public ActionResult GetGSTAddressByState(Models.OfflineBookingModel model, long Customerid)
        {
            Models.OfflineBookingModel modeldata = new Models.OfflineBookingModel();
            try
            {
                CLayer.OfflineBooking Data = BLayer.OfflineBooking.GetGSTAddressByState(model.SubCustomerGstStateId, model.SubCustomerGstRegNo, Customerid, model.CustomerTableType);
                if (Data != null)
                {
                    modeldata.SubCustomerCity = Data.SubCustomerCity;
                    modeldata.SubCustomerCityname = Data.SubCustomerCityname;
                    modeldata.SubCustomerAddress = Data.SubCustomerAddress;
                    modeldata.SubCustomerpinCode = Data.SubCustomerpinCode;
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View("~/Areas/Admin/Views/OfflineBookingGST/_CustomerGstAddress.cshtml", modeldata);
        }


        [HttpPost]
        public ActionResult SearchforBookingFor(string name, int custid)
        {
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            try
            {
                result = BLayer.OfflineBooking.SearchforBookingFor(name, custid);


                if (result.Count == 0)
                { ViewBag.ErrorMessage = "No matching property found."; }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return PartialView("~/Areas/Admin/Views/OfflineBookingGST/_BookingForList.cshtml", result);
        }


        [AllowAnonymous]
        public bool GetSbBillingStateid(long sbbillingentityid, long PropertyStateId)
        {
            long stateid = BLayer.SBEntity.Get(sbbillingentityid).StateId;
            if (PropertyStateId == stateid)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckgstNumber(int StateId, string gstNumber)
        {
            string StateCode = BLayer.OfflineBooking.GetGSTStateCode(StateId);
            string F1_2 = gstNumber.Substring(0, 2);
            string F3_7 = gstNumber.Substring(2, 5);
            string F8_11 = gstNumber.Substring(7, 4);
            string F12_11 = gstNumber.Substring(11, 1);
            if (StateCode == F1_2 && Regex.IsMatch(F3_7, @"^[a-zA-Z]+$") && Regex.IsMatch(F8_11, @"^\d+$") && Regex.IsMatch(F12_11, @"^[a-zA-Z]+$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string StaybazarBookingEntity(int bookingid)
        {
            string Gstregno = BLayer.OfflineBooking.StaybazarBookingEntity(bookingid);
            return Gstregno;
        }
        public string GetBookingCustomerStateID(int bookingid)
        {
            string Gstper = BLayer.OfflineBooking.GetBookingCustomerStateID(bookingid);
            return Gstper;
        }

        //karthikms on added 4-11-2019 4681-4685
        public string GetBookingPropertyStateID(int bookingid)
        {
            string Gstper = BLayer.OfflineBooking.GetBookingPropertyStateID(bookingid);
            return Gstper;
        }
        public string StaybazarGstSlab(int SlabCode, decimal Amt)
        {
            string Gstregno = BLayer.OfflineBooking.StaybazarGstSlab(SlabCode, Amt);
            return Gstregno;
        }

        public string GetStateIDsTypeDetails(int bookingid)
        {
            CLayer.OfflineBooking offprodt = BLayer.OfflineBooking.GetAllpropertyDetails(bookingid);
            CLayer.OfflineBooking customerdetails = BLayer.OfflineBooking.GetAllCustomerDetails(bookingid);
            CLayer.OfflineBooking offedit = BLayer.OfflineBooking.GetAllDetailsById(bookingid);

            StringBuilder sw = new StringBuilder();
            string PropertyGstRegNo = offedit.PropertyGstRegNo;
            long CustomerType = customerdetails.CustomerType;
            long CustomerGstStateId = offedit.CustomerGstStateId;
            long CustomerState = customerdetails.CustomerState;
            long PropertyState = offprodt.PropertyState;
            long SupplierStateID = BLayer.OfflineBooking.GetSupplierStateID(bookingid);
            int OfflineBookingType = BLayer.OfflineBooking.GetBookingType(bookingid);

            sw.Append(CustomerType);
            sw.Append(",");
            sw.Append(CustomerGstStateId);
            sw.Append(",");
            sw.Append(CustomerState);
            sw.Append(",");
            sw.Append(PropertyState);
            sw.Append(",");
            sw.Append(SupplierStateID);
            sw.Append(",");
            sw.Append(OfflineBookingType);
            sw.Append(",");
            sw.Append(PropertyGstRegNo);
            return sw.ToString();
        }


        public JsonResult getIGSTdetails(long OfflineBookingId)
        {

            List<CLayer.OfflineBooking> bookings = BLayer.OfflineBooking.getIGSTdetails(OfflineBookingId);

            return Json(bookings, JsonRequestBehavior.AllowGet);
        }

        public long GetSupplierStateID(int bookingid)
        {
            long stateid = BLayer.OfflineBooking.GetSupplierStateID(bookingid);

            return stateid;
        }

        public void SetBillingIds()
        {
            string ids = BLayer.OfflineBooking.GetAllBillingEntityIdsCSV();
            ViewBag.BillingIds = ids;
        }


        public async Task<bool> DeleteBookingDetails(long BookedID, long OfflineBookingId)
        {
            try
            {
                string email = User.Identity.GetUserName();
                long LoginUserid = BLayer.User.GetUserId(email);

                if (User.IsInRole("Administrator"))
                {
                    BLayer.OfflineBooking.DeleteBookingDetails(BookedID, LoginUserid);
                    //fix amounts
                    BLayer.OfflineBooking.FixAmounts(OfflineBookingId);
                    return true;
                }
                else
                {
                    if (BLayer.OfflineBooking.CheckOfflineSubBookingDeleteorNot(OfflineBookingId, BookedID))
                    {
                        CLayer.Invoice data = BLayer.Invoice.GetInvoiceByOfflineBooking(OfflineBookingId);
                        if (data != null)
                        {
                            if (data.Status != (int)CLayer.ObjectStatus.InvoiceStatus.Submitted)
                            {
                                BLayer.OfflineBooking.DeleteBookingDetails(BookedID, LoginUserid);
                                //fix amounts
                                BLayer.OfflineBooking.FixAmounts(OfflineBookingId);
                                return true;
                            }
                            else
                            {
                                await SubBookingDeleteRequestAlertMail(OfflineBookingId, BookedID);
                                return false;
                            }
                        }
                        else
                        {
                            BLayer.OfflineBooking.DeleteBookingDetails(BookedID, LoginUserid);
                            //fix amounts
                            BLayer.OfflineBooking.FixAmounts(OfflineBookingId);
                            return true;
                        }
                    }
                    else
                    {
                        await SubBookingDeleteRequestAlertMail(OfflineBookingId, BookedID);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return true;
        }


        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<bool> SubBookingDeleteRequestAlertMail(long OfflineBookingId, long BookedID)
        {
            try
            {
                string message = "";
                Common.Mailer ml = new Common.Mailer();
                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();

                string email = User.Identity.GetUserName();
                long LoginUserid = BLayer.User.GetUserId(email);

                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("OfflineSubBookingDeleteRequest")
                                                              + OfflineBookingId.ToString() + "&LoginUserid=" + LoginUserid + "&BookingDetailsId=" + BookedID);

                string AccountMail = BLayer.Settings.GetValue(CLayer.Settings.BOOK_DELETE_ALERT_EMAILS);
                if (AccountMail != "")
                {
                    string[] Accemails = AccountMail.Split(',');
                    for (int i = 0; i < Accemails.Length; ++i)
                    {
                        MailMsg.To.Add(Accemails[i]);
                    }
                }

                MailMsg.Subject = "Booking Delete Request";
                MailMsg.Body = message;
                MailMsg.IsBodyHtml = true;
                try
                {
                    await ml.SendMailAsync(MailMsg, Common.Mailer.MailType.Support);
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


        public ActionResult LoadList(long OfflineBookingId)
        {
            Models.OfflineBookingModel MultipleBooking = new Models.OfflineBookingModel();
            List<CLayer.OfflineBooking> result = new List<CLayer.OfflineBooking>();
            MultipleBooking.OfflineBookingList = BLayer.OfflineBooking.BookedList(OfflineBookingId);
            MultipleBooking.OfflineBookingId = OfflineBookingId;
            return View("_BookingDetailsList", MultipleBooking);
        }

        public string GetMessageAlertForBookingDelete(long OfflineBookingId, long BookedID)
        {
            string msgerror = "";
            try
            {
                CLayer.OfflineBooking Offlinedata = BLayer.OfflineBooking.GetOfflineboikingdetailsforBookDeleteRequest(OfflineBookingId);
                decimal SupplierPayment = BLayer.SupplierPayment.GetSupplierPaymentBybookingid(OfflineBookingId);
                msgerror = "Deletion could not be allowed since invoice no " + Offlinedata.InvoiceNumber + " has already raised / supplier has been paid Rs." + SupplierPayment + " against this booking. Please contact admin";
                return msgerror;
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return msgerror;
            }
        }

        public bool CheckGrossmarginless(long OfflineBookingId)
        {
            Decimal Gmp = Convert.ToDecimal(BLayer.Settings.GetValue(CLayer.Settings.Grossmarginperc));
            bool grsmrgnless = false;
            try
            {
                CLayer.OfflineBooking obtacdt = BLayer.OfflineBooking.GetBookingTypeData(OfflineBookingId);
                decimal TacAmt = 0;
                if ((int)obtacdt.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.TAC)
                {
                    double TotalTaxPercentChk = 0;
                    double BookingTypeAmountChk = 0;
                    obtacdt.BookingTypeAmount = obtacdt.BookingTypeTAC;
                    List<KeyValuePair<string, double>> BookTypeTax = BLayer.OfflineBooking.GetBookingTypeTaxes(OfflineBookingId);
                    if (obtacdt.BookingTypeGSTIncluded)
                    {
                        for (int i = 0; i < @BookTypeTax.Count; i++)
                        {
                            TotalTaxPercentChk = TotalTaxPercentChk + @BookTypeTax[i].Value;
                        }
                        BookingTypeAmountChk = @Math.Round(obtacdt.BookingTypeAmount, 2) / (1 + (TotalTaxPercentChk / 100));
                        obtacdt.BookingTypeAmount = BookingTypeAmountChk;
                    }
                    TacAmt = Convert.ToDecimal(obtacdt.BookingTypeAmount);
                }
                else if ((int)obtacdt.BookingType == (int)CLayer.ObjectStatus.OfflineBookingType.Direct)
                {
                    TacAmt = Convert.ToDecimal(obtacdt.BookingTypeAmount);
                }

                List<CLayer.OfflineBooking> OfflineBookingList = BLayer.OfflineBooking.GetMultipleBookingDetailsGST(OfflineBookingId);
                foreach (CLayer.OfflineBooking dt in OfflineBookingList)
                {
                    long NoOfNight = dt.NoOfNight;
                    long NoOfRooms = dt.NoOfRooms;

                    decimal AvgDailyRateBefreStaxForSalePrice = dt.AvgDailyRateBefreStaxForSalePrice;
                    decimal BuyPriceforotherservicesForSalePrice = dt.BuyPriceforotherservicesForSalePrice;
                    decimal AvgDailyRateBefreStaxForBuyPrice = dt.AvgDailyRateBefreStaxForBuyPrice;
                    decimal BuyPriceforotherservicesForBuyprice = dt.BuyPriceforotherservicesForBuyprice;

                    decimal grossmarginamtcustomer = ((AvgDailyRateBefreStaxForSalePrice * NoOfNight * NoOfRooms) + BuyPriceforotherservicesForSalePrice);
                    decimal grossmarginamtsupplier = ((AvgDailyRateBefreStaxForBuyPrice * NoOfNight * NoOfRooms) + BuyPriceforotherservicesForBuyprice);
                    decimal grossmarginamt = (grossmarginamtcustomer - grossmarginamtsupplier) + TacAmt;
                    decimal grossmarginPerc = (grossmarginamt / grossmarginamtcustomer) * 100;


                    if (grossmarginPerc < Gmp)
                    {
                        grsmrgnless = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return grsmrgnless;
        }


        [Common.AdminRolePermission(AllowAllRoles = true)]
        public async Task<bool> DraftBookingRequestAlertMail(long OfflineBookingId)
        {
            try
            {
                string message = "";
                Common.Mailer ml = new Common.Mailer();
                System.Net.Mail.MailMessage MailMsg = new System.Net.Mail.MailMessage();

                string email = User.Identity.GetUserName();
                long LoginUserid = BLayer.User.GetUserId(email);
                CLayer.OfflineBooking offedit = BLayer.OfflineBooking.GetAllDetailsById(OfflineBookingId);
                message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("OfflineBookingSavedDraftRequest")
                                                              + OfflineBookingId.ToString() + "&CreatedByUserid=" + offedit.CreatedBy);

                string BookingDraft = BLayer.Settings.GetValue(CLayer.Settings.Booking_Draft_alert_mails);
                if (BookingDraft != "")
                {
                    string[] BookingDrafts = BookingDraft.Split(',');
                    for (int i = 0; i < BookingDrafts.Length; ++i)
                    {
                        MailMsg.To.Add(BookingDrafts[i]);
                    }
                }

                MailMsg.Subject = "Waiting for approval - " + offedit.ConfirmationNumber;
                MailMsg.Body = message;
                MailMsg.IsBodyHtml = true;
                try
                {
                    await ml.SendMailAsync(MailMsg, Common.Mailer.MailType.Support);
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


        public JsonResult GetInvoiceIDByOfflineBookingID(long OfflineBookingId)
        {
            long InvoiceID = 0;
            var invoice_id = BLayer.OfflineBooking.GetInvoiceIDByOfflineBookingID(OfflineBookingId);
            if (invoice_id > 0)
            {
                var email = User.Identity.GetUserName();
                int roleId = BLayer.User.GetRole(email);
                if (roleId == (int)CLayer.Role.Roles.Administrator)
                {
                    InvoiceID = 0;
                }
                else
                {
                    InvoiceID = 1;
                }
            }
            return Json(InvoiceID, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// for gds 
        /// </summary>
        private class SearchResults
        {
            public List<CLayer.SearchResult> Results;
            public int TotalRows;
        }
        public async Task<ActionResult> gdssearch(string place, string CheckIn, string CheckOut, string BeedRooms)
        {
            Session["BedRooms"] = Convert.ToInt32(BeedRooms);
            StayBazar.Models.SimpleSearchModel ss = new StayBazar.Models.SimpleSearchModel();

            DateTime start = DateTime.Parse(CheckIn);
            DateTime end = DateTime.Parse(CheckOut);
            SearchResults searchr = new SearchResults();
            List<CLayer.SearchResult> ch = new List<CLayer.SearchResult>();
            ss.CheckIn = start;
            ss.CheckOut = end;
            ss.Destination = place;

            #region Country Code
            Session["GDSCountry"] = "";
            Session["GDSCountryCode"] = "";
            Session["GDSCurrencyCode"] = "";

            if (ss.Destination != null && ss.Destination != "")
            {
                ss.Destination = Common.Utils.SecureInputString(ss.Destination); //ss.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");

                ss.Country = APIUtility.GetGDSCountry(ss.Destination);


                //   ss.Country = APIUtility.GetCountryName(ss.Country);
                Session["GDSCountry"] = string.IsNullOrEmpty(ss.Country) ? "" : ss.Country;
                // int GlobalSearch = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalSearch"].ToString());

                if (ss.Country.ToUpper() == "INDIA")
                {
                    ss.Country = "";
                    Session["GDSCountry"] = "";
                }
                List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                GDSCountryList = BLayer.Country.GetGDSCountry(ss.Country);
                int GDSCountryCount = (ss.Country != "") ? GDSCountryList.Count : 0;
                if (GDSCountryCount > 0)
                {
                    ss.Destination = APIUtility.GetGDSDestination(ss.Destination);
                    Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                    Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                    Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                }
                else
                {
                    string sDestination = APIUtility.GetGDSDestination(ss.Destination);
                    ss.Destination = (sDestination == "") ? ss.Destination : sDestination;
                    if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                    {
                        if (ss.Country == "")
                        {
                            Session["GDSCountry"] = "";
                            Session["GDSCountryID"] = "1";
                            Session["GDSCountryCode"] = "IN";
                            Session["GDSCurrencyCode"] = "INR";
                        }


                    }

                }
            }
            #endregion

            //  var data = GDSsearchresults(ss);
            var data = await SearchGDS(place, Convert.ToString(start), Convert.ToString(ss.CheckOut), BeedRooms);

            if (data.Results != null)
            {
                ch = data.Results.Where(x => x.Amount > 0).OrderBy(x => x.Amount).ToList();
            }


            return View("~/Areas/Admin/Views/OfflineBookingGST/GdsPropertyList.cshtml", ch);
            // return View("GdsPropertyList", searchr);
        }

        public async Task<ActionResult> gdssearch1(string place, string CheckIn, string CheckOut, string BeedRooms, string PropertyId = "")
        {
            Session["BedRooms"] = Convert.ToInt32(BeedRooms);
            StayBazar.Models.SimpleSearchModel ss = new StayBazar.Models.SimpleSearchModel();

            DateTime start = DateTime.Parse(CheckIn);
            DateTime end = DateTime.Parse(CheckOut);
            SearchResults searchr = new SearchResults();
            List<CLayer.SearchResult> ch = new List<CLayer.SearchResult>();
            ss.CheckIn = start;
            ss.CheckOut = end;
            ss.Destination = place;

            #region Country Code
            Session["GDSCountry"] = "";
            Session["GDSCountryCode"] = "";
            Session["GDSCurrencyCode"] = "";

            if (ss.Destination != null && ss.Destination != "")
            {
                ss.Destination = Common.Utils.SecureInputString(ss.Destination); //ss.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");

                ss.Country = APIUtility.GetGDSCountry(ss.Destination);


                //   ss.Country = APIUtility.GetCountryName(ss.Country);
                Session["GDSCountry"] = string.IsNullOrEmpty(ss.Country) ? "" : ss.Country;
                // int GlobalSearch = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalSearch"].ToString());

                if (ss.Country.ToUpper() == "INDIA")
                {
                    ss.Country = "";
                    Session["GDSCountry"] = "";
                }
                List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                GDSCountryList = BLayer.Country.GetGDSCountry(ss.Country);
                int GDSCountryCount = (ss.Country != "") ? GDSCountryList.Count : 0;
                if (GDSCountryCount > 0)
                {
                    ss.Destination = APIUtility.GetGDSDestination(ss.Destination);
                    Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                    Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                    Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                }
                else
                {
                    string sDestination = APIUtility.GetGDSDestination(ss.Destination);
                    ss.Destination = (sDestination == "") ? ss.Destination : sDestination;
                    if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                    {
                        if (ss.Country == "")
                        {
                            Session["GDSCountry"] = "";
                            Session["GDSCountryID"] = "1";
                            Session["GDSCountryCode"] = "IN";
                            Session["GDSCurrencyCode"] = "INR";
                        }


                    }

                }
            }
            #endregion

            //  var data = GDSsearchresults(ss);
            DataTable dtHotelCode = BLayer.Property.GetHotelIDFrompropertyid(Convert.ToInt64(PropertyId));
            string HotelCode = string.Empty;
            if (dtHotelCode.Rows.Count > 0)
            {
                HotelCode = Convert.ToString(dtHotelCode.Rows[0]["Hotel_Id"]);
            }


            var data = await SearchGDS(ss.Destination, Convert.ToString(start), Convert.ToString(ss.CheckOut), BeedRooms, HotelCode, PropertyId);

            if (data.Results != null)
            {
                ch = data.Results.OrderBy(x => x.Amount).ToList();
            }


            return View("~/Areas/Admin/Views/OfflineBookingGST/GdsPropertyList.cshtml", ch);
            // return View("GdsPropertyList", searchr);
        }

        private async Task<SearchResults> SearchGDS(string place, string CheckIn, string CheckOut, string BeedRooms)
        {
            Session["BedRooms"] = Convert.ToInt32(BeedRooms);
            StayBazar.Models.SimpleSearchModel ss = new StayBazar.Models.SimpleSearchModel();

            DateTime start = DateTime.Parse(CheckIn);
            DateTime end = DateTime.Parse(CheckOut);
            SearchResults searchr = new SearchResults();
            List<CLayer.SearchResult> ch = new List<CLayer.SearchResult>();
            ss.CheckIn = start;
            ss.CheckOut = end;
            ss.Destination = place;

            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();
            try
            {
                int adult, children, staytype, bedrooms;
                adult = 0;
                children = 0;
                staytype = 0;
                bedrooms = Convert.ToInt32(BeedRooms);
                int totalCount = 0;

                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                srch.Adults = adult;
                srch.Children = children;
                srch.StayType = staytype;
                srch.Bedrooms = bedrooms;
                srch.Destination = ss.Destination;
                Session["GDSCountry"] = "";
                Session["GDSCountryCode"] = "";
                Session["GDSCurrencyCode"] = "";

                string pKeyword = APIUtility.GDSKeyWord;

                PropertyNoImage = "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);


                if (srch.Destination != null && srch.Destination != "")
                {
                    srch.Destination = Common.Utils.SecureInputString(srch.Destination); //srch.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");

                    srch.Country = APIUtility.GetGDSCountry(srch.Destination);


                    //   srch.Country = APIUtility.GetCountryName(srch.Country);
                    Session["GDSCountry"] = string.IsNullOrEmpty(srch.Country) ? "" : srch.Country;
                    // int GlobalSearch = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalSearch"].ToString());

                    if (srch.Country.ToUpper() == "INDIA")
                    {
                        srch.Country = "";
                        Session["GDSCountry"] = "";
                    }
                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(srch.Country);
                    int GDSCountryCount = (srch.Country != "") ? GDSCountryList.Count : 0;
                    if (GDSCountryCount > 0)
                    {
                        srch.Destination = APIUtility.GetGDSDestination(srch.Destination);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        string sDestination = APIUtility.GetGDSDestination(srch.Destination);
                        srch.Destination = (sDestination == "") ? srch.Destination : sDestination;
                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (srch.Country == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }


                        }

                    }
                }

                srch.CheckIn = ss.CheckIn;
                srch.CheckOut = ss.CheckOut;
                Session["CheckIn"] = srch.CheckIn;
                Session["CheckOut"] = srch.CheckOut;
                Session["Destination"] = srch.Destination;
                Session["Adults"] = srch.Adults;
                Session["Children"] = srch.Children;
                //srch.StayType = staytype;
                Session["Bedrooms"] = srch.Bedrooms;

                // pass source co-ordinates
                srch.Lattitude = 0;
                srch.Longitude = 0;
                srch.Features = "";
                srch.DistanceInKm = ss.distanceInKm;
                srch.RangeBudgetMax = ss.rangeBudgetMax;
                srch.RangeBudgetMin = ss.rangeBudgetMin;
                srch.StarRatingRange = ss.starRatingRange;
                srch.Location = "";


                if (ss.Destination != null && ss.Destination != "")
                {
                    Common.Utils.Location pos;
                    string loc = BLayer.City.GetLocation(ss.Destination);
                    if (loc == "")
                    {
                        srch.Lattitude = 0;
                        srch.Longitude = 0;
                    }
                    else
                    {
                        pos = await Common.Utils.GetLocation(loc);
                        srch.Lattitude = pos.Lattitude;
                        srch.Longitude = pos.Longitude;
                    }
                }

                int rip = 0;
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get(Common.Utils.SERARCH_MAX_ROWS), out rip);
                srch.NoOfRows = rip;
                int sr = ss.CurrentPage - 1;
                if (sr < 0) sr = 0;
                sr = sr * srch.NoOfRows;
                srch.StaringRow = sr;
                srch.StayTypeGroup = "";


                srch.UserType = CLayer.Role.Roles.Customer;
                //if (data.OrderBy < 1 || data.OrderBy > 3)
                //    srch.SortOrder = CLayer.SearchCriteria.SortBy.PriceAsc;
                //else
                //    srch.SortOrder = (CLayer.SearchCriteria.SortBy)data.OrderBy;


                if (User.Identity.IsAuthenticated)
                {
                    long ud = 0;
                    long.TryParse(User.Identity.GetUserId(), out ud);
                    CLayer.Role.Roles val = BLayer.User.GetRole(ud);
                    srch.UserType = CLayer.Role.GetRoleForSearch(val);
                    if (val == CLayer.Role.Roles.Corporate)
                        srch.LoggedInUser = ud;
                    else
                    {
                        if (val == CLayer.Role.Roles.CorporateUser)
                        {
                            srch.LoggedInUser = BLayer.B2B.GetCorporateIdOfUser(ud);
                        }
                        else
                            srch.LoggedInUser = 0;
                    }
                }
                // return BLayer.Property.Search(out totalCount, data.Destination, data.CheckIn, data.CheckOut, adult, children, staytype, bedrooms, "");
                // return BLayer.Property.SearchWithFilter(out totalCount, srch);
                TempData["SearchCriteria"] = ss;
                bool IsAmadeus = true;
                if (IsAmadeus)
                {
                    //  List<SearchResults> AmadeusResulsts = new List<SearchResults>();

                    //save keywords in country table
                    CLayer.Country pCountry = new CLayer.Country();
                    pCountry.KeyWords = APIUtility.GDSKeyWord;
                    pCountry.CountryId = Convert.ToInt64(Session["GDSCountryID"]);

                    //save keywords in country table end
                    int KeyWords = BLayer.Country.GDSKeywordSave(pCountry);


                    string result = HotelMultiSingleAvailability(srch, true, "");

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailability, 1);

                    #endregion Transaction log end

                    Serializer ser = new Serializer();

                    CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                    CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                    CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();

                    try
                    {

                        HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            HotelResultAdv = ser.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                        }
                        catch (Exception ex1)
                        {
                            HotelResultAdvFirst = ser.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                        }

                    }


                    long AmadeusPropertyID = 0;
                    string CityName = string.Empty;
                    var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                    var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();

                    //var HotelItemAdv = new CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStaysHotelStay..OTA_HotelAvailRSHotelStay();
                    //var RoomStayItemListAdv = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSRoomStaysRoomStay>();


                    long StateID = 0;
                    string StateName = "";
                    string City = srch.Destination;
                    int CountryID = 0;
                    long CityId = 0;
                    long OwnerID = 0;
                    int GDSStateID = 0;
                    int GDSCityID = 0;
                    string GDStateName = "";

                    OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                    if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                    {
                        StateID = BLayer.State.GetStateID(srch.Destination).StateId;
                        StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                        CityId = BLayer.City.GetCityID(City).CityId;
                        CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;
                    }
                    else
                    {
                        GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), srch.Destination);
                        GDStateName = BLayer.State.Get(GDSStateID).Name;
                        GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                        CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                    }


                    StayBazar.Controllers.SearchController objSearch = new StayBazar.Controllers.SearchController();


                    #region Hotel Result
                    if (HotelResult.Body != null)
                    {

                        if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                            CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                            objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                            Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                            objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                            objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                            objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                            Session["GDSCurrencyConversion"] = objCurrencyConversion;
                            CurrencyConversionList = APIUtility.GetCurrencyConversions(result);
                        }
                        if (Convert.ToString(Session["GDSCountry"]) == "")
                        {
                            Session["GDSCurrencyConversion"] = null;
                        }


                        if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                        {

                            List<string> HotelIDS = new List<string>();
                            foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelIDS.Add(item.BasicPropertyInfo.HotelCode);
                            }

                            List<CLayer.GDSProperties> objGDSList = new List<CLayer.GDSProperties>();
                            DataTable dt = BLayer.Property.SearchForGDSProperties(HotelIDS);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    CLayer.GDSProperties objGDS = new CLayer.GDSProperties();
                                    objGDS.Propertyid = Convert.ToInt64(dr["propertyid"]);
                                    objGDS.Hotel_Id = Convert.ToString(dr["Hotel_Id"]);
                                    objGDS.ImageUrl = Convert.ToString(dr["ImageUrl"]);
                                    objGDS.Description = Convert.ToString(dr["Description"]);
                                    objGDSList.Add(objGDS);

                                }
                            }
                            //dt.Select()



                            foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelItem = item;
                                Models.PropertyModel objData = new Models.PropertyModel();
                                objData.PropertyId = 0;
                                objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                                objData.Description = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Description;
                                objData.Image = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().ImageUrl;
                                objData.PropertyId = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Propertyid;



                                objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                objData.OwnerId = OwnerID;
                                //  objData.CityName = item.BasicPropertyInfo.Address.CityName;

                                objData.CityName = srch.Destination;
                                CityName = srch.Destination;
                                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                {
                                    objData.State = Convert.ToInt32(StateID);
                                    objData.StateName = StateName;
                                    objData.City = CityName;
                                    objData.CityId = Convert.ToInt32(CityId);

                                    PropertyCityID = Convert.ToInt32(CityId);
                                    PropertyCityName = CityName;
                                    PropertyStateID = Convert.ToInt32(StateID);
                                    PropertyStateName = StateName;

                                }
                                else
                                {

                                    objData.State = GDSStateID;
                                    //   objData.StateName = "Others";
                                    objData.StateName = GDStateName;
                                    objData.City = objData.CityName;
                                    objData.CityId = GDSCityID;

                                    PropertyCityID = Convert.ToInt32(GDSCityID);
                                    PropertyCityName = objData.CityName;
                                    PropertyStateID = GDSStateID;
                                    PropertyStateName = GDStateName;
                                }

                                objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                                objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                                objData.Country = CountryID;
                                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                {
                                    objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                }
                                else
                                {
                                    objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                                }

                                PropertyCountryID = CountryID;
                                PropertyCountryName = objData.CountryName;


                                if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                                {
                                    objData.Location = objData.CityName + ", " + objData.StateName;
                                    if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                    objData.Location = objData.Location + ", " + objData.CountryName;
                                }



                                objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                objData.HotelId = item.BasicPropertyInfo.HotelCode;

                                AmadeusPropertyID = objData.PropertyId;

                                long AccomodationId = 0;
                                string RoomStayRPH = item.RoomStayRPH;
                                List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                if (!string.IsNullOrEmpty(RoomStayRPH))
                                {

                                    string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                    RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                                }
                                CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
                                AmadeusResults = SetAmadeusResult(HotelItem, RoomStaysResultList, CityName, AmadeusPropertyID, objData.Image, objData.Description);
                                AmadeusResultList.Add(AmadeusResults);

                                // Session["GDSCurrencyCode"] = "";

                            }

                        }

                        #endregion
                        searchr.Results = AmadeusResultList;
                        searchr.TotalRows = searchr.Results.Count;
                    }
                    else if (HotelResultAdvFirst.Body != null)
                    {

                        if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                            CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                            objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                            Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                            objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                            objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                            objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                            Session["GDSCurrencyConversion"] = objCurrencyConversion;
                            CurrencyConversionList = APIUtility.GetCurrencyConversions(result);
                        }
                        if (Convert.ToString(Session["GDSCountry"]) == "")
                        {
                            Session["GDSCurrencyConversion"] = null;
                        }



                        if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                        {
                            List<string> HotelIDS = new List<string>();
                            foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelIDS.Add(item.BasicPropertyInfo.HotelCode);
                            }

                            List<CLayer.GDSProperties> objGDSList = new List<CLayer.GDSProperties>();
                            DataTable dt = BLayer.Property.SearchForGDSProperties(HotelIDS);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    CLayer.GDSProperties objGDS = new CLayer.GDSProperties();
                                    objGDS.Propertyid = Convert.ToInt64(dr["propertyid"]);
                                    objGDS.Hotel_Id = Convert.ToString(dr["Hotel_Id"]);
                                    objGDS.ImageUrl = Convert.ToString(dr["ImageUrl"]);
                                    objGDS.Description = Convert.ToString(dr["Description"]);
                                    objGDSList.Add(objGDS);

                                }
                            }

                            foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelItemAdvFirst = item;
                                Models.PropertyModel objData = new Models.PropertyModel();
                                objData.PropertyId = 0;
                                objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                                //    objData.Description = "";
                                objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                objData.OwnerId = OwnerID;
                                //  objData.CityName = item.BasicPropertyInfo.Address.CityName;
                                objData.Description = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Description;
                                objData.Image = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().ImageUrl;
                                objData.PropertyId = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Propertyid;


                                objData.CityName = srch.Destination;
                                CityName = srch.Destination;
                                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                {
                                    objData.State = Convert.ToInt32(StateID);
                                    objData.StateName = StateName;
                                    objData.City = CityName;
                                    objData.CityId = Convert.ToInt32(CityId);

                                    PropertyCityID = Convert.ToInt32(CityId);
                                    PropertyCityName = CityName;
                                    PropertyStateID = Convert.ToInt32(StateID);
                                    PropertyStateName = StateName;
                                }
                                else
                                {

                                    objData.State = GDSStateID;
                                    //   objData.StateName = "Others";
                                    objData.StateName = GDStateName;
                                    objData.City = objData.CityName;
                                    objData.CityId = GDSCityID;

                                    PropertyCityID = Convert.ToInt32(GDSCityID);
                                    PropertyCityName = objData.CityName;
                                    PropertyStateID = GDSStateID;
                                    PropertyStateName = GDStateName;
                                }

                                objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                                objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                                objData.Country = CountryID;
                                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                {
                                    objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                }
                                else
                                {
                                    objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                                }
                                PropertyCountryID = CountryID;
                                PropertyCountryName = objData.CountryName;

                                if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                                {
                                    objData.Location = objData.CityName + ", " + objData.StateName;
                                    if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                    objData.Location = objData.Location + ", " + objData.CountryName;
                                }



                                objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                objData.HotelId = item.BasicPropertyInfo.HotelCode;

                                AmadeusPropertyID = objData.PropertyId;


                                long AccomodationId = 0;
                                string RoomStayRPH = item.RoomStayRPH;
                                List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                if (!string.IsNullOrEmpty(RoomStayRPH))
                                {

                                    string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                    RoomStaysResultList = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                                }
                                CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
                                AmadeusResults = SetAmadeusResult(HotelItemAdvFirst, RoomStaysResultList, CityName, AmadeusPropertyID, objData.Image, objData.Description);
                                AmadeusResultList.Add(AmadeusResults);
                            }

                        }
                        searchr.Results = AmadeusResultList;
                        searchr.TotalRows = searchr.Results.Count;
                    }

                    else
                    {
                        AmadeusResultList = new List<CLayer.SearchResult>();
                        searchr.Results = AmadeusResultList;
                        searchr.TotalRows = 0;
                    }


                }


            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
                searchr = null;
            }





            return searchr;
        }

        private async Task<SearchResults> SearchGDS(string place, string CheckIn, string CheckOut, string BeedRooms, string HotelCode, string PropertyId)
        {
            Session["BedRooms"] = Convert.ToInt32(BeedRooms);
            StayBazar.Models.SimpleSearchModel ss = new StayBazar.Models.SimpleSearchModel();

            DateTime start = DateTime.Parse(CheckIn);
            DateTime end = DateTime.Parse(CheckOut);
            SearchResults searchr = new SearchResults();
            List<CLayer.SearchResult> ch = new List<CLayer.SearchResult>();
            ss.CheckIn = start;
            ss.CheckOut = end;
            ss.Destination = place;

            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();
            try
            {
                int adult, children, staytype, bedrooms;
                adult = 0;
                children = 0;
                staytype = 0;
                bedrooms = Convert.ToInt32(BeedRooms);
                int totalCount = 0;

                CLayer.SearchCriteria srch = new CLayer.SearchCriteria();
                srch.Adults = adult;
                srch.Children = children;
                srch.StayType = staytype;
                srch.Bedrooms = bedrooms;
                srch.Destination = ss.Destination;
                Session["GDSCountry"] = "";
                Session["GDSCountryCode"] = "";
                Session["GDSCurrencyCode"] = "";

                string pKeyword = APIUtility.GDSKeyWord;

                PropertyNoImage = "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);


                if (srch.Destination != null && srch.Destination != "")
                {
                    srch.Destination = Common.Utils.SecureInputString(srch.Destination); //srch.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");

                    srch.Country = APIUtility.GetGDSCountry(srch.Destination);


                    //   srch.Country = APIUtility.GetCountryName(srch.Country);
                    Session["GDSCountry"] = string.IsNullOrEmpty(srch.Country) ? "" : srch.Country;
                    // int GlobalSearch = Convert.ToInt32(ConfigurationManager.AppSettings["GlobalSearch"].ToString());

                    if (srch.Country.ToUpper() == "INDIA")
                    {
                        srch.Country = "";
                        Session["GDSCountry"] = "";
                    }
                    List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                    GDSCountryList = BLayer.Country.GetGDSCountry(srch.Country);
                    int GDSCountryCount = (srch.Country != "") ? GDSCountryList.Count : 0;
                    if (GDSCountryCount > 0)
                    {
                        srch.Destination = APIUtility.GetGDSDestination(srch.Destination);
                        Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                        Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                        Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                    }
                    else
                    {
                        string sDestination = APIUtility.GetGDSDestination(srch.Destination);
                        srch.Destination = (sDestination == "") ? srch.Destination : sDestination;
                        if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                        {
                            if (srch.Country == "")
                            {
                                Session["GDSCountry"] = "";
                                Session["GDSCountryID"] = "1";
                                Session["GDSCountryCode"] = "IN";
                                Session["GDSCurrencyCode"] = "INR";
                            }


                        }

                    }
                }

                srch.CheckIn = ss.CheckIn;
                srch.CheckOut = ss.CheckOut;
                Session["CheckIn"] = srch.CheckIn;
                Session["CheckOut"] = srch.CheckOut;
                Session["Destination"] = srch.Destination;
                Session["Adults"] = srch.Adults;
                Session["Children"] = srch.Children;
                //srch.StayType = staytype;
                Session["Bedrooms"] = srch.Bedrooms;

                // pass source co-ordinates
                srch.Lattitude = 0;
                srch.Longitude = 0;
                srch.Features = "";
                srch.DistanceInKm = ss.distanceInKm;
                srch.RangeBudgetMax = ss.rangeBudgetMax;
                srch.RangeBudgetMin = ss.rangeBudgetMin;
                srch.StarRatingRange = ss.starRatingRange;
                srch.Location = "";


                if (ss.Destination != null && ss.Destination != "")
                {
                    Common.Utils.Location pos;
                    string loc = BLayer.City.GetLocation(ss.Destination);
                    if (loc == "")
                    {
                        srch.Lattitude = 0;
                        srch.Longitude = 0;
                    }
                    else
                    {
                        pos = await Common.Utils.GetLocation(loc);
                        srch.Lattitude = pos.Lattitude;
                        srch.Longitude = pos.Longitude;
                    }
                }

                int rip = 0;
                int.TryParse(System.Configuration.ConfigurationManager.AppSettings.Get(Common.Utils.SERARCH_MAX_ROWS), out rip);
                srch.NoOfRows = rip;
                int sr = ss.CurrentPage - 1;
                if (sr < 0) sr = 0;
                sr = sr * srch.NoOfRows;
                srch.StaringRow = sr;
                srch.StayTypeGroup = "";


                srch.UserType = CLayer.Role.Roles.Customer;
                //if (data.OrderBy < 1 || data.OrderBy > 3)
                //    srch.SortOrder = CLayer.SearchCriteria.SortBy.PriceAsc;
                //else
                //    srch.SortOrder = (CLayer.SearchCriteria.SortBy)data.OrderBy;


                if (User.Identity.IsAuthenticated)
                {
                    long ud = 0;
                    long.TryParse(User.Identity.GetUserId(), out ud);
                    CLayer.Role.Roles val = BLayer.User.GetRole(ud);
                    srch.UserType = CLayer.Role.GetRoleForSearch(val);
                    if (val == CLayer.Role.Roles.Corporate)
                        srch.LoggedInUser = ud;
                    else
                    {
                        if (val == CLayer.Role.Roles.CorporateUser)
                        {
                            srch.LoggedInUser = BLayer.B2B.GetCorporateIdOfUser(ud);
                        }
                        else
                            srch.LoggedInUser = 0;
                    }
                }
                // return BLayer.Property.Search(out totalCount, data.Destination, data.CheckIn, data.CheckOut, adult, children, staytype, bedrooms, "");
                // return BLayer.Property.SearchWithFilter(out totalCount, srch);
                TempData["SearchCriteria"] = ss;
                bool IsAmadeus = true;
                if (IsAmadeus)
                {
                    //  List<SearchResults> AmadeusResulsts = new List<SearchResults>();

                    //save keywords in country table
                    CLayer.Country pCountry = new CLayer.Country();
                    pCountry.KeyWords = APIUtility.GDSKeyWord;
                    pCountry.CountryId = Convert.ToInt64(Session["GDSCountryID"]);

                    //save keywords in country table end
                    int KeyWords = BLayer.Country.GDSKeywordSave(pCountry);


                    string result = HotelMultiSingleAvailability(HotelCode, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));

                    #region Transaction Log 

                    APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), result, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelSingleAvailability, 1);

                    #endregion Transaction log end

                    Serializer ser = new Serializer();

                    CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                    CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                    CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();

                    try
                    {

                        HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            HotelResultAdv = ser.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                        }
                        catch (Exception ex1)
                        {
                            HotelResultAdvFirst = ser.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                        }

                    }


                    long AmadeusPropertyID = 0;
                    string CityName = string.Empty;
                    var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                    var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                    var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();

                    //var HotelItemAdv = new CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSHotelStaysHotelStay..OTA_HotelAvailRSHotelStay();
                    //var RoomStayItemListAdv = new List<CLayer.HotelAvailabilityAdvanced.OTA_HotelAvailRSRoomStaysRoomStay>();


                    long StateID = 0;
                    string StateName = "";
                    string City = srch.Destination;
                    int CountryID = 0;
                    long CityId = 0;
                    long OwnerID = 0;
                    int GDSStateID = 0;
                    int GDSCityID = 0;
                    string GDStateName = "";


                    CLayer.Property dtLocation = BLayer.Property.Get(Convert.ToInt64(PropertyId));


                    OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                    if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                    {
                        StateID = dtLocation.State;
                        StateName = dtLocation.Statename;
                        CityId = dtLocation.CityId;
                        CountryID = dtLocation.Country;
                    }
                    else
                    {
                        GDSStateID = dtLocation.State; //BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), srch.Destination);
                        GDStateName = dtLocation.Statename; //BLayer.State.Get(GDSStateID).Name;
                        GDSCityID = dtLocation.CityId;  //BLayer.City.UpdateGDSCity(GDSStateID, City);
                        CountryID = dtLocation.Country; //BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                    }


                    StayBazar.Controllers.SearchController objSearch = new StayBazar.Controllers.SearchController();


                    #region Hotel Result
                    if (HotelResult.Body != null)
                    {

                        if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                            CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                            objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                            Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                            objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                            objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                            objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                            Session["GDSCurrencyConversion"] = objCurrencyConversion;
                        }
                        if (Convert.ToString(Session["GDSCountry"]) == "")
                        {
                            Session["GDSCurrencyConversion"] = null;
                        }


                        if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                        {

                            List<string> HotelIDS = new List<string>();
                            foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelIDS.Add(item.BasicPropertyInfo.HotelCode);
                            }

                            List<CLayer.GDSProperties> objGDSList = new List<CLayer.GDSProperties>();
                            DataTable dt = BLayer.Property.SearchForGDSProperties(HotelIDS);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    CLayer.GDSProperties objGDS = new CLayer.GDSProperties();
                                    objGDS.Propertyid = Convert.ToInt64(dr["propertyid"]);
                                    objGDS.Hotel_Id = Convert.ToString(dr["Hotel_Id"]);
                                    objGDS.ImageUrl = Convert.ToString(dr["ImageUrl"]);
                                    objGDS.Description = Convert.ToString(dr["Description"]);
                                    objGDSList.Add(objGDS);

                                }
                            }
                            //dt.Select()



                            foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelItem = item;
                                Models.PropertyModel objData = new Models.PropertyModel();
                                objData.PropertyId = 0;
                                objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                                objData.Description = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Description;
                                objData.Image = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().ImageUrl;
                                objData.PropertyId = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Propertyid;



                                objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                objData.OwnerId = OwnerID;
                                //  objData.CityName = item.BasicPropertyInfo.Address.CityName;

                                objData.CityName = srch.Destination;
                                CityName = srch.Destination;
                                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                {
                                    objData.State = Convert.ToInt32(StateID);
                                    objData.StateName = StateName;
                                    objData.City = CityName;
                                    objData.CityId = Convert.ToInt32(CityId);

                                    PropertyCityID = Convert.ToInt32(CityId);
                                    PropertyCityName = CityName;
                                    PropertyStateID = Convert.ToInt32(StateID);
                                    PropertyStateName = StateName;

                                }
                                else
                                {

                                    objData.State = GDSStateID;
                                    //   objData.StateName = "Others";
                                    objData.StateName = GDStateName;
                                    objData.City = objData.CityName;
                                    objData.CityId = GDSCityID;

                                    PropertyCityID = Convert.ToInt32(GDSCityID);
                                    PropertyCityName = objData.CityName;
                                    PropertyStateID = GDSStateID;
                                    PropertyStateName = GDStateName;
                                }

                                objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                                objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                                objData.Country = CountryID;
                                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                {
                                    objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                }
                                else
                                {
                                    objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                                }

                                PropertyCountryID = CountryID;
                                PropertyCountryName = objData.CountryName;


                                if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                                {
                                    objData.Location = objData.CityName + ", " + objData.StateName;
                                    if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                    objData.Location = objData.Location + ", " + objData.CountryName;
                                }



                                objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                objData.HotelId = item.BasicPropertyInfo.HotelCode;

                                AmadeusPropertyID = objData.PropertyId;

                                long AccomodationId = 0;
                                string RoomStayRPH = item.RoomStayRPH;
                                List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                if (!string.IsNullOrEmpty(RoomStayRPH))
                                {

                                    string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                    RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                                }
                                CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
                                AmadeusResults = SetAmadeusResult(HotelItem, RoomStaysResultList, CityName, AmadeusPropertyID, objData.Image, objData.Description);
                                AmadeusResultList.Add(AmadeusResults);

                                // Session["GDSCurrencyCode"] = "";

                            }

                        }

                        #endregion
                        searchr.Results = AmadeusResultList;
                        searchr.TotalRows = searchr.Results.Count;
                    }
                    else if (HotelResultAdvFirst.Body != null)
                    {

                        if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                        {
                            var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                            CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                            objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                            Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                            objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                            objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                            objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                            Session["GDSCurrencyConversion"] = objCurrencyConversion;
                        }
                        if (Convert.ToString(Session["GDSCountry"]) == "")
                        {
                            Session["GDSCurrencyConversion"] = null;
                        }



                        if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                        {
                            List<string> HotelIDS = new List<string>();
                            foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelIDS.Add(item.BasicPropertyInfo.HotelCode);
                            }

                            List<CLayer.GDSProperties> objGDSList = new List<CLayer.GDSProperties>();
                            DataTable dt = BLayer.Property.SearchForGDSProperties(HotelIDS);
                            if (dt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    CLayer.GDSProperties objGDS = new CLayer.GDSProperties();
                                    objGDS.Propertyid = Convert.ToInt64(dr["propertyid"]);
                                    objGDS.Hotel_Id = Convert.ToString(dr["Hotel_Id"]);
                                    objGDS.ImageUrl = Convert.ToString(dr["ImageUrl"]);
                                    objGDS.Description = Convert.ToString(dr["Description"]);
                                    objGDSList.Add(objGDS);

                                }
                            }

                            foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                            {
                                HotelItemAdvFirst = item;
                                Models.PropertyModel objData = new Models.PropertyModel();
                                objData.PropertyId = 0;
                                objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                                //    objData.Description = "";
                                objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                objData.OwnerId = OwnerID;
                                //  objData.CityName = item.BasicPropertyInfo.Address.CityName;
                                objData.Description = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Description;
                                objData.Image = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().ImageUrl;
                                objData.PropertyId = objGDSList.Where(x => x.Hotel_Id == item.BasicPropertyInfo.HotelCode).FirstOrDefault().Propertyid;


                                objData.CityName = srch.Destination;
                                CityName = srch.Destination;
                                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                {
                                    objData.State = Convert.ToInt32(StateID);
                                    objData.StateName = StateName;
                                    objData.City = CityName;
                                    objData.CityId = Convert.ToInt32(CityId);

                                    PropertyCityID = Convert.ToInt32(CityId);
                                    PropertyCityName = CityName;
                                    PropertyStateID = Convert.ToInt32(StateID);
                                    PropertyStateName = StateName;
                                }
                                else
                                {

                                    objData.State = GDSStateID;
                                    //   objData.StateName = "Others";
                                    objData.StateName = GDStateName;
                                    objData.City = objData.CityName;
                                    objData.CityId = GDSCityID;

                                    PropertyCityID = Convert.ToInt32(GDSCityID);
                                    PropertyCityName = objData.CityName;
                                    PropertyStateID = GDSStateID;
                                    PropertyStateName = GDStateName;
                                }

                                objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                                objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                                objData.Country = CountryID;
                                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                                {
                                    objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                                }
                                else
                                {
                                    objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                                }
                                PropertyCountryID = CountryID;
                                PropertyCountryName = objData.CountryName;

                                if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                                {
                                    objData.Location = objData.CityName + ", " + objData.StateName;
                                    if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                    objData.Location = objData.Location + ", " + objData.CountryName;
                                }



                                objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                                objData.HotelId = item.BasicPropertyInfo.HotelCode;

                                AmadeusPropertyID = objData.PropertyId;


                                long AccomodationId = 0;
                                string RoomStayRPH = item.RoomStayRPH;
                                List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                                if (!string.IsNullOrEmpty(RoomStayRPH))
                                {

                                    string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                    RoomStaysResultList = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                                }
                                CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
                                AmadeusResults = SetAmadeusResult(HotelItemAdvFirst, RoomStaysResultList, CityName, AmadeusPropertyID, objData.Image, objData.Description);
                                AmadeusResultList.Add(AmadeusResults);
                            }

                        }
                        searchr.Results = AmadeusResultList;
                        searchr.TotalRows = searchr.Results.Count;
                    }

                    else
                    {
                        AmadeusResultList = new List<CLayer.SearchResult>();
                        searchr.Results = AmadeusResultList;
                        searchr.TotalRows = 0;
                    }


                }


            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
                searchr = null;
            }





            return searchr;
        }

        //private async Task<SearchResults> GDSsearchresults(StayBazar.Models.SimpleSearchModel data)
        //{
        //    List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();

        //    //DateTime start = DateTime.Parse(data.CheckIn);
        //    //DateTime end = DateTime.Parse(CheckOut);
        //    SearchResults searchr = new SearchResults();
        //    CLayer.SearchCriteria ch = new CLayer.SearchCriteria();
        //    ch.CheckIn = data.CheckIn;
        //    ch.CheckOut = data.CheckOut;
        //    ch.Destination = data.Destination;


        //    string searchedresult = HotelMultiSingleAvailability(ch, true, "");

        //    try {
        //        Serializer ser = new Serializer();
        //        CLayer.HotelAvailability.Envelope HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(searchedresult);

        //        long AmadeusPropertyID = 0;
        //        string CityName = string.Empty;
        //        var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
        //        var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

        //        foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
        //        {
        //            HotelItem = item;
        //            StayBazar.Areas.Admin.Models.PropertyModel objData = new StayBazar.Areas.Admin.Models.PropertyModel();
        //            objData.PropertyId = 0;
        //            objData.Title = item.BasicPropertyInfo.HotelName;
        //            objData.Description = "";
        //            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
        //            objData.OwnerId = BLayer.B2B.GetSupplierID("amadeus");
        //            objData.CityName = item.BasicPropertyInfo.Address.CityName;
        //            CityName = objData.CityName;
        //            objData.State = BLayer.State.GetStateID(objData.CityName).StateId;
        //            objData.StateName = BLayer.State.Get(objData.State).Name;
        //            objData.CityName = item.BasicPropertyInfo.Address.CityName;
        //            //objData.CityId = BLayer.City.GetCityID(objData.City).CityId;
        //            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
        //            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);
        //            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
        //            {
        //                int StateId = BLayer.State.GetStateID(CityName).StateId;
        //                objData.Country = 1;// System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY).ToLower());
        //                objData.CountryName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY).ToLower());
        //            }
        //            else
        //            {
        //                int StateId = BLayer.State.GetStateID(CityName, Convert.ToInt64(Session["GDSCountryID"])).StateId;
        //                objData.Country = StateId;// System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(BLayer.State.Get(StateId).Name.ToLower());
        //                objData.CountryName = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Session["GDSCountry"])).ToLower();
        //            }



        //            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
        //            {
        //                objData.Location = objData.CityName + ", " + objData.StateName;
        //                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
        //                objData.Location = objData.Location + ", " + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
        //            }

        //            objData.Country = BLayer.State.Get(objData.State).CountryId;
        //            objData.CountryName = BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
        //            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
        //            objData.HotelId = item.BasicPropertyInfo.HotelCode;

        //            AmadeusPropertyID = SaveAmadeus_Property(objData);


        //            long AccomodationId = 0;
        //            string RoomStayRPH = item.RoomStayRPH;
        //            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
        //            if (!string.IsNullOrEmpty(RoomStayRPH))
        //            {
        //                //BLayer.Accommodation.Accommodation_Amadeus_Delete(AmadeusPropertyID);
        //                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
        //                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

        //                for (int i = 0; i < RoomStayRPHList.Length; i++)
        //                {

        //                    var RoomStays = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(m => m.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
        //                    RoomStayItemList = RoomStays;
        //                  //  StayBazar.Models.AccommodationModel dataAccommodation = new StayBazar.Models.AccommodationModel();
        //                    StayBazar.Areas.Admin.Models.AccommodationModel dataAccommodation = new StayBazar.Areas.Admin.Models.AccommodationModel();

        //                    foreach (var Accitem in objAcc)
        //                    {
        //                        if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
        //                        {
        //                            AccomodationId = Accitem.AccommodationId;
        //                            break;
        //                        }
        //                    }


        //                    foreach (var RoomStayItem in RoomStays)
        //                    {
        //                        dataAccommodation.AccommodationId = AccomodationId;
        //                        dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
        //                        dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
        //                        dataAccommodation.PropertyId = AmadeusPropertyID;
        //                        dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
        //                        dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
        //                        if (RoomStayItem.RoomTypes != null)
        //                        {
        //                            dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
        //                            dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
        //                        }


        //                        dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
        //                        if (RoomStayItem.RoomRates != null)
        //                        {
        //                            dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
        //                            dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
        //                            dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
        //                        }
        //                        if (RoomStayItem.RatePlans != null)
        //                        {
        //                            dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
        //                        }


        //                        AccommodationSave(dataAccommodation);

        //                    }

        //                }
        //                RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList);
        //            }
        //            CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
        //            AmadeusResults = SetAmadeusResult(HotelItem, RoomStaysResultList, CityName, AmadeusPropertyID);
        //            AmadeusResultList.Add(AmadeusResults);
        //        }
        //        searchr.Results = AmadeusResultList;
        //        searchr.TotalRows = searchr.Results.Count; ;
        //    }
        //    catch(Exception ex)
        //    {

        //    }


        //    return searchr;
        //}

        private CLayer.SearchResult SetAmadeusResult(CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay item, List<CLayer.RoomStaysResult> RoomStaysResultList, string CityName, long PropertyId)
        {
            CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
            try
            {

                AmadeusResults.Title = item.BasicPropertyInfo.HotelName;
                AmadeusResults.Description = "";
                AmadeusResults.PropertyId = PropertyId;
                AmadeusResults.City = item.BasicPropertyInfo.Address.CityName;
                int StateId = BLayer.State.GetStateID(CityName).StateId;
                AmadeusResults.State = BLayer.State.Get(StateId).Name;
                AmadeusResults.Country = BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                AmadeusResults.ImageFile = "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);
                AmadeusResults.IsDistanceFromCity = false;

                AmadeusResults.Avail = 0;
                AmadeusResults.Distance = 0;
                AmadeusResults.StarRate = 0;
                AmadeusResults.Pincode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                if ((AmadeusResults.Lattitude == "0" && AmadeusResults.Longitude == "0") || (AmadeusResults.Lattitude == null && AmadeusResults.Longitude == null))
                {
                    AmadeusResults.Location = AmadeusResults.City + ", " + AmadeusResults.State;
                    if (AmadeusResults.Pincode != "") AmadeusResults.Location = AmadeusResults.Location + " " + AmadeusResults.Pincode;
                    AmadeusResults.Location = AmadeusResults.Location + ", " + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);

                }

                AmadeusResults.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                AmadeusResults.HotelID = item.BasicPropertyInfo.HotelCode;


                AmadeusResults.RoomStaysResultList = RoomStaysResultList;
                //      Decimal TotalRoomAmount = RoomStaysResultList.Select(c => c.AmountAfterTax).Min();
                Decimal AmountMin = 0;
                Decimal MinAmountAfterTax = 0;
                for (int i = 0; i < RoomStaysResultList.Count; i++)
                {
                    if (i == 0)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                    if (RoomStaysResultList[i].AmountAfterTax < AmountMin)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                }
                Decimal TotalRoomAmount = MinAmountAfterTax;


                AmadeusResults.Amount = Math.Round(TotalRoomAmount, 0);
            }
            catch (Exception ex)
            {
                AmadeusResults = null;
            }



            return AmadeusResults;

        }
        public CLayer.SearchResult SetAmadeusResult(CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay item, List<CLayer.RoomStaysResult> RoomStaysResultList, string CityName, long PropertyId, string ImageUrl = "", string Description = "")
        {
            CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
            try
            {
                int StateId = 0;
                // CLayer.SearchResult objImgDesc = BLayer.Property.GetGDSImageandDesctiption(PropertyId);


                AmadeusResults.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());

                Description = (Description.Length > 130) ? Description.Substring(0, 129) : Description;

                AmadeusResults.Description = (Description == null || Description == "") ? "" : Description;
                AmadeusResults.PropertyId = PropertyId;
                AmadeusResults.City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.Address.CityName.ToLower());
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    // StateId = BLayer.State.GetStateID(CityName).StateId;
                    StateId = PropertyStateID;
                    AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyStateName.ToLower());
                    AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyCountryName.ToLower());
                }
                else
                {
                    StateId = PropertyStateID; //BLayer.State.GetStateID(CityName, Convert.ToInt64(Session["GDSCountryID"])).StateId;
                    AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyStateName.ToLower());
                    AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Session["GDSCountry"])).ToLower();
                }

                string AmadeusNonImage = PropertyNoImage;// "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);
                AmadeusResults.ImageFile = ImageUrl; //BLayer.Property.GetGDSHotelImage(PropertyId);
                if (AmadeusResults.ImageFile == "")
                {
                    AmadeusResults.ImageFile = AmadeusNonImage;
                    AmadeusResults.IsImageExists = false;
                }
                else
                {
                    AmadeusResults.IsImageExists = true;
                }

                AmadeusResults.IsDistanceFromCity = false;

                AmadeusResults.Avail = 0;
                AmadeusResults.Distance = 0;
                AmadeusResults.StarRate = 0;
                AmadeusResults.Pincode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);



                if ((AmadeusResults.Lattitude == "0" && AmadeusResults.Longitude == "0") || (AmadeusResults.Lattitude == null && AmadeusResults.Longitude == null))
                {
                    AmadeusResults.Location = AmadeusResults.City + ", " + AmadeusResults.State;
                    if (AmadeusResults.Pincode != "") AmadeusResults.Location = AmadeusResults.Location + " " + AmadeusResults.Pincode;
                    AmadeusResults.Location = AmadeusResults.Location + ", " + AmadeusResults.Country;
                    AmadeusResults.Location = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(AmadeusResults.Location.ToLower());
                }

                AmadeusResults.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                AmadeusResults.APIType = GetAPIType(AmadeusResults.InventoryAPIType);
                AmadeusResults.HotelID = item.BasicPropertyInfo.HotelCode;


                AmadeusResults.RoomStaysResultList = RoomStaysResultList;

                Decimal AmountMin = 0;
                Decimal MinAmountAfterTax = 0;
                for (int i = 0; i < RoomStaysResultList.Count; i++)
                {
                    if (i == 0)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                    if (RoomStaysResultList[i].AmountAfterTax < AmountMin)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                }
                Decimal TotalRoomAmount = MinAmountAfterTax;


                //RoomStaysResultList.Select(c => c.AmountAfterTax).Min();

                AmadeusResults.Amount = Math.Round(TotalRoomAmount, 0);// (Convert.ToString(Session["GDSCountry"]) == "")? TotalRoomAmount:APIUtility.GetGDSConvertedAmount(TotalRoomAmount);
            }
            catch (Exception ex)
            {
                AmadeusResults = null;
            }



            return AmadeusResults;

        }

        public CLayer.SearchResult SetAmadeusResult(CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay item, List<CLayer.RoomStaysResult> RoomStaysResultList, string CityName, long PropertyId, string ImageUrl = "", string Description = "")
        {
            CLayer.SearchResult AmadeusResults = new CLayer.SearchResult();
            try
            {
                int StateId = 0;
                //     CLayer.SearchResult objImgDesc = BLayer.Property.GetGDSImageandDesctiption(PropertyId);


                AmadeusResults.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                Description = (Description.Length > 130) ? Description.Substring(0, 129) : Description;

                AmadeusResults.Description = (Description == null || Description == "") ? "" : Description;
                AmadeusResults.PropertyId = PropertyId;
                AmadeusResults.City = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.Address.CityName.ToLower());
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    // StateId = BLayer.State.GetStateID(CityName).StateId;
                    StateId = PropertyStateID;
                    AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyStateName.ToLower());
                    AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyCountryName.ToLower());
                }
                else
                {
                    StateId = PropertyStateID; //BLayer.State.GetStateID(CityName, Convert.ToInt64(Session["GDSCountryID"])).StateId;
                    AmadeusResults.State = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(PropertyStateName.ToLower());
                    AmadeusResults.Country = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(Convert.ToString(Session["GDSCountry"])).ToLower();
                }

                string AmadeusNonImage = PropertyNoImage;// "/Images/" + BLayer.Settings.GetValue(CLayer.Settings.AMADEUSNOIMAGE);
                AmadeusResults.ImageFile = ImageUrl;//BLayer.Property.GetGDSHotelImage(PropertyId);
                if (AmadeusResults.ImageFile == "")
                {
                    AmadeusResults.ImageFile = AmadeusNonImage;
                    AmadeusResults.IsImageExists = false;
                }
                else
                {
                    AmadeusResults.IsImageExists = true;
                }

                AmadeusResults.IsDistanceFromCity = false;

                AmadeusResults.Avail = 0;
                AmadeusResults.Distance = 0;
                AmadeusResults.StarRate = 0;
                AmadeusResults.Pincode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);



                if ((AmadeusResults.Lattitude == "0" && AmadeusResults.Longitude == "0") || (AmadeusResults.Lattitude == null && AmadeusResults.Longitude == null))
                {
                    AmadeusResults.Location = AmadeusResults.City + ", " + AmadeusResults.State;
                    if (AmadeusResults.Pincode != "") AmadeusResults.Location = AmadeusResults.Location + " " + AmadeusResults.Pincode;
                    AmadeusResults.Location = AmadeusResults.Location + ", " + AmadeusResults.Country;
                    AmadeusResults.Location = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(AmadeusResults.Location.ToLower());
                }

                AmadeusResults.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                AmadeusResults.APIType = GetAPIType(AmadeusResults.InventoryAPIType);
                AmadeusResults.HotelID = item.BasicPropertyInfo.HotelCode;


                AmadeusResults.RoomStaysResultList = RoomStaysResultList;

                Decimal AmountMin = 0;
                Decimal MinAmountAfterTax = 0;
                for (int i = 0; i < RoomStaysResultList.Count; i++)
                {
                    if (i == 0)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                    if (RoomStaysResultList[i].AmountAfterTax < AmountMin)
                    {
                        AmountMin = RoomStaysResultList[i].AmountAfterTax;
                        MinAmountAfterTax = AmountMin;
                    }
                }
                Decimal TotalRoomAmount = MinAmountAfterTax;

                //RoomStaysResultList.Select(c => c.AmountAfterTax).Min();

                AmadeusResults.Amount = TotalRoomAmount;// (Convert.ToString(Session["GDSCountry"]) == "")? TotalRoomAmount:APIUtility.GetGDSConvertedAmount(TotalRoomAmount);
            }
            catch (Exception ex)
            {
                AmadeusResults = null;
            }



            return AmadeusResults;

        }

        public static List<CLayer.RoomStaysResult> GetRoomStays(CLayer.HotelAvailability.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();

            for (int i = 0; i < pRPH.Length; i++)
            {
                var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                if (RoomStayItem[0].RoomRates != null)
                {
                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Rates.Rate.Base.AmountBeforeTax);
                    objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                    objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;

                    CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                    ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

                    ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
                    objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;

                    objRoomStay.RoomTypeCode = RoomStayItem[0].RoomRates.RoomRate.RoomTypeCode;
                    objRoomStay.RatePlanCode = RoomStayItem[0].RoomRates.RoomRate.RatePlanCode;


                    //CLayer.RoomRateTotal ObjRoomRateTotal = new CLayer.RoomRateTotal();
                    //List<CLayer.RoomRateTotalTaxes> ObjRoomRateTotalTaxesList = new List<CLayer.RoomRateTotalTaxes>();
                    //CLayer.RoomRateTotalTaxes ObjRoomRateTotalTaxes = new CLayer.RoomRateTotalTaxes();
                    //ObjRoomRateTotalTaxes.Type= RoomStayItem[0].RoomRates.RoomRate[-

                }

                if (RoomStayItem[0].RatePlans != null)
                {
                    objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
                }
                if (RoomStayItem[0].RoomTypes != null)
                {
                    objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
                    objRoomStay.RoomTypeCode = RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
                }




                RoomStaysResultList.Add(objRoomStay);

            }
            return RoomStaysResultList;
        }
        public static List<CLayer.RoomStaysResult> GetRoomStays(CLayer.HotelAvailability.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
            Decimal AmountMin = 0;
            for (int i = 0; i < pRPH.Length; i++)
            {
                var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                if (RoomStayItem[0].RoomRates != null)
                {

                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Rates.Rate.Base.AmountBeforeTax);
                    objRoomStay.AmountBeforeTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax);

                    objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                    objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                    // objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                    objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;
                    //if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    //{
                    //    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;

                    if (objRoomStay.CurrencyCode != "INR")
                    {
                        if (RequestedCurrencyCode != objRoomStay.CurrencyCode)
                        {
                            if (CurrencyConversionList != null)
                            {
                                ConversionRate = CurrencyConversionList.Where(x => x.SourceCurrencyCode == objRoomStay.CurrencyCode).FirstOrDefault().RateConversion;
                            }
                        }
                    }

                    if (RequestedCurrencyCode != objRoomStay.CurrencyCode)
                    {
                        objRoomStay.AmountAfterTax = Math.Round(objRoomStay.AmountAfterTax * ConversionRate, 0);

                    }
                    else
                    {
                        objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                    }
                    //}
                    if (i == 0)
                    {
                        AmountMin = objRoomStay.AmountAfterTax;
                        objRoomStay.MinAmountAfterTax = AmountMin;
                    }
                    if (objRoomStay.AmountAfterTax < AmountMin)
                    {
                        AmountMin = objRoomStay.AmountAfterTax;
                        objRoomStay.MinAmountAfterTax = AmountMin;
                    }


                    CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                    ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

                    ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
                    objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;


                    objRoomStay.RoomTypeCode = RoomStayItem[0].RoomRates.RoomRate.RoomTypeCode;
                    objRoomStay.RatePlanCode = RoomStayItem[0].RoomRates.RoomRate.RatePlanCode;


                    //CLayer.RoomRateTotal ObjRoomRateTotal = new CLayer.RoomRateTotal();
                    //List<CLayer.RoomRateTotalTaxes> ObjRoomRateTotalTaxesList = new List<CLayer.RoomRateTotalTaxes>();
                    //CLayer.RoomRateTotalTaxes ObjRoomRateTotalTaxes = new CLayer.RoomRateTotalTaxes();
                    //ObjRoomRateTotalTaxes.Type= RoomStayItem[0].RoomRates.RoomRate[-

                }

                if (RoomStayItem[0].RatePlans != null)
                {
                    objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
                }
                if (RoomStayItem[0].RoomTypes != null)
                {
                    objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
                    // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
                }




                RoomStaysResultList.Add(objRoomStay);

            }
            return RoomStaysResultList;
        }
        public static List<CLayer.RoomStaysResult> GetRoomStaysAdv(CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStays RoomStaysList, string[] pRPH, string RequestedCurrencyCode = "", decimal ConversionRate = 0)
        {
            List<CLayer.RoomStaysResult> RoomStaysResultList = new List<CLayer.RoomStaysResult>();
            Decimal AmountMin = 0;

            for (int i = 0; i < pRPH.Length; i++)
            {
                var RoomStayItem = RoomStaysList.RoomStay.Where(s => s.RPH == Convert.ToInt32(pRPH[i])).ToList();
                CLayer.RoomStaysResult objRoomStay = new CLayer.RoomStaysResult();
                if (RoomStayItem[0].RoomRates != null)
                {

                    objRoomStay.AmountBeforeTax = Convert.ToDecimal(RoomStayItem[0].RoomRates.RoomRate.Total.AmountBeforeTax);
                    objRoomStay.AmountBeforeTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountBeforeTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountBeforeTax);

                    objRoomStay.BookingCode = RoomStayItem[0].RoomRates.RoomRate.BookingCode;
                    objRoomStay.AmountAfterTax = RoomStayItem[0].RoomRates.RoomRate.Total.AmountAfterTax;
                    objRoomStay.CurrencyCode = RoomStayItem[0].RoomRates.RoomRate.Total.CurrencyCode;

                    if (objRoomStay.CurrencyCode != "INR")
                    {
                        if (RequestedCurrencyCode != objRoomStay.CurrencyCode)
                        {
                            if (CurrencyConversionList != null)
                            {
                                ConversionRate = CurrencyConversionList.Where(x => x.SourceCurrencyCode == objRoomStay.CurrencyCode).FirstOrDefault().RateConversion;
                            }
                        }
                    }


                    if (RequestedCurrencyCode != objRoomStay.CurrencyCode)
                    {
                        objRoomStay.AmountAfterTax = Math.Round(objRoomStay.AmountAfterTax * ConversionRate, 0);
                    }
                    else
                    {
                        objRoomStay.AmountAfterTax = (Convert.ToString(System.Web.HttpContext.Current.Session["GDSCountry"]) == "") ? objRoomStay.AmountAfterTax : APIUtility.GetGDSConvertedAmount(objRoomStay.AmountAfterTax);
                    }

                    if (i == 0)
                    {
                        AmountMin = objRoomStay.AmountAfterTax;
                        objRoomStay.MinAmountAfterTax = AmountMin;
                    }
                    if (objRoomStay.AmountAfterTax < AmountMin)
                    {
                        AmountMin = objRoomStay.AmountAfterTax;
                        objRoomStay.MinAmountAfterTax = AmountMin;
                    }


                    CLayer.RoomRateDescription ObjRoomRateDescription = new CLayer.RoomRateDescription();
                    ObjRoomRateDescription.Name = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Name;

                    ObjRoomRateDescription.Description = RoomStayItem[0].RoomRates.RoomRate.RoomRateDescription.Text;
                    objRoomStay.RoomRateDescriptionItem = ObjRoomRateDescription;


                    objRoomStay.RoomTypeCode = RoomStayItem[0].RoomRates.RoomRate.RoomTypeCode;
                    objRoomStay.RatePlanCode = RoomStayItem[0].RoomRates.RoomRate.RatePlanCode;



                    //CLayer.RoomRateTotal ObjRoomRateTotal = new CLayer.RoomRateTotal();
                    //List<CLayer.RoomRateTotalTaxes> ObjRoomRateTotalTaxesList = new List<CLayer.RoomRateTotalTaxes>();
                    //CLayer.RoomRateTotalTaxes ObjRoomRateTotalTaxes = new CLayer.RoomRateTotalTaxes();
                    //ObjRoomRateTotalTaxes.Type= RoomStayItem[0].RoomRates.RoomRate[-

                }

                if (RoomStayItem[0].RatePlans != null)
                {
                    objRoomStay.RatePlanCode = RoomStayItem[0].RatePlans.RatePlan.RatePlanCode;
                }
                if (RoomStayItem[0].RoomTypes != null)
                {
                    objRoomStay.RoomType = RoomStayItem[0].RoomTypes.RoomType.RoomType;
                    // objRoomStay.RoomTypeCode= RoomStayItem[0].RoomTypes.RoomType.RoomTypeCode;
                }




                RoomStaysResultList.Add(objRoomStay);

            }
            return RoomStaysResultList;
        }
        public string GetAPIType(int pValue)
        {
            string result = string.Empty;
            switch (pValue)
            {
                case 1:
                    result = "maxmojo";
                    break;
                case 2:
                    result = "amadeus";
                    break;
                default:
                    result = "sb";
                    break;
            }
            return result;
        }
        private void AccommodationSave(StayBazar.Areas.Admin.Models.AccommodationModel data)
        {
            try
            {

                string userid = User.Identity.GetUserId();
                long id = 0;
                long.TryParse(userid, out id);
                //if (ModelState.IsValid)
                //{
                CLayer.Accommodation accmdtn = new CLayer.Accommodation()
                {
                    AccommodationId = data.AccommodationId,
                    PropertyId = data.PropertyId,
                    AccommodationTypeId = data.AccommodationTypeId,
                    StayCategoryId = data.StayCategoryId,
                    AccommodationCount = data.AccommodationCount,
                    Description = data.Description,
                    //Location = data.Location,
                    MaxNoOfPeople = data.MaxNoOfPeople,
                    MaxNoOfChildren = data.MaxNoOfChildren,
                    MinNoOfPeople = data.MinNoOfPeople,
                    BedRooms = data.BedRooms,
                    Area = data.Area,
                    Status = data.Status,
                    TotalAccommodations = data.TotalAccommodations,
                    UpdatedById = id,
                    RoomType = data.RoomType,
                    RoomTypeCode = data.RoomTypeCode,
                    SourceofBusiness = data.SourceofBusiness,
                    BookingCode = data.BookingCode,
                    RatePlanCode = data.RatePlanCode,
                    RoomStayRPH = data.RoomStayRPH

                };
                if (accmdtn.MaxNoOfPeople < accmdtn.MinNoOfPeople) accmdtn.MaxNoOfPeople = accmdtn.MinNoOfPeople;
                if (accmdtn.MaxNoOfPeople < accmdtn.MaxNoOfChildren) accmdtn.MaxNoOfPeople = accmdtn.MaxNoOfChildren;
                long accId = BLayer.Accommodation.Save_Amadeus(accmdtn);
                if (data.RoomId == null) { data.RoomId = ""; }

                //  BLayer.Accommodation.SetRoomId(data.AccommodationId, data.RoomId.Trim());
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);

            }
        }
        public long SaveAmadeus_Property(StayBazar.Areas.Admin.Models.PropertyModel data)
        {
            try
            {

                CLayer.Property prprty = new CLayer.Property()
                {

                    PropertyId = data.PropertyId,
                    Title = data.Title,
                    Description = data.Description,
                    Location = data.Location,
                    Status = (CLayer.ObjectStatus.StatusType)data.Status,
                    OwnerId = data.OwnerId,
                    Address = data.Address,
                    Country = data.Country,
                    State = data.State,
                    City = data.City,
                    CityId = data.CityId,
                    ZipCode = data.ZipCode,
                    PropertyInventoryType = data.InventoryAPIType,
                    HotelID = data.HotelId


                };
                string loc = "";
                try
                {

                    string statename = data.StateName;
                    string cityname;
                    //if (data.Country == 0)
                    //{

                    //}
                    if (data.CityId < 1)
                    {
                        cityname = data.City;
                    }
                    else
                    {
                        cityname = BLayer.City.Get(data.CityId).Name;
                        data.City = cityname;
                        prprty.City = cityname;
                    }
                    string Countryname = data.CountryName;// BLayer.Country.Get(data.Country).Name;
                    loc = cityname + "," + statename + "," + Countryname + "," + data.ZipCode;
                    string qAdddress = data.Title + "," + data.Address + "," + loc;

                    //Common.Utils.Location pos;

                    //pos = await Common.Utils.GetLocation(qAdddress);
                    //prprty.PositionLat = pos.Lattitude.ToString();
                    //prprty.PositionLng = pos.Longitude.ToString();
                }
                catch (Exception ex)
                {
                    Common.LogHandler.LogError(ex);
                }


                data.PropertyId = BLayer.Property.SaveAmadeus_Property(prprty);
                return data.PropertyId;

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return 0;
            }
        }
        public static string HotelMultiSingleAvailability(CLayer.SearchCriteria sch, bool Stateless = true, string XMLtext = "")
        {
            string soapResult = string.Empty;
            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/Hotel_MultiSingleAvailability_10.0";

                string SoapHeader = string.Empty;
                string SoapBody = string.Empty;


                SoapHeader = APIUtility.SetSoapHeader(_url, _action, Stateless);

                SoapBody = APIUtility.SetHotelMultiSingleAvailabilityBody(sch);



                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";

                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(SoapEnvelop);

                try
                {
                    HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                    webRequest.Proxy = null;
                    webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                    webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                    webRequest.Timeout = 45000;
                    // begin async call to web request.
                    IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                    // suspend this thread until call is complete. You might want to
                    // do something usefull here like update your UI.
                    asyncResult.AsyncWaitHandle.WaitOne();


                    // get the response from the completed web request.

                    using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                    {
                        using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                        {
                            soapResult = rd.ReadToEnd();
                        }
                        Console.Write(soapResult);
                    }
                }
                catch (WebException webex)
                {

                    try
                    {
                        HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                        webRequest.Proxy = null;
                        webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                        webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                        webRequest.Timeout = 45000;
                        // begin async call to web request.
                        IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                        // suspend this thread until call is complete. You might want to
                        // do something usefull here like update your UI.
                        asyncResult.AsyncWaitHandle.WaitOne();


                        // get the response from the completed web request.

                        using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                        {
                            using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                            {
                                soapResult = rd.ReadToEnd();
                            }
                            Console.Write(soapResult);
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHandler.HandleError(ex);
                    }

                }
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    soapResult = text;

                }
            }

            return soapResult;
        }

        public static string HotelMultiSingleAvailability(string hotelcode, string start, string end)
        {
            string soapResult = string.Empty;
            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/Hotel_MultiSingleAvailability_10.0";


                string SoapHeader = APIUtility.SetSoapHeaderPriceingdetails(_url, _action);
                string SoapBody = APIUtility.SetHotelMultiSingleAvailabilityBodyPriceingdetails(hotelcode, start, end);

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";

                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(SoapEnvelop);

                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                webRequest.Proxy = null;
                webRequest.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;

                webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);
                webRequest.Timeout = 45000;
                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                // do something usefull here like update your UI.
                asyncResult.AsyncWaitHandle.WaitOne();

                // get the response from the completed web request.

                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                    Console.Write(soapResult);
                }
            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    soapResult = text;

                }
            }

            return soapResult;
        }

        private static HttpWebRequest CreateWebRequest(string url, string action)
        {
            //HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
            //webRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //ServicePointManager.Expect100Continue = false;
            //ServicePointManager.DefaultConnectionLimit = 200;
            //ServicePointManager.MaxServicePointIdleTime = 2000;
            //ServicePointManager.SetTcpKeepAlive(false, 0, 0);
#if DEBUG
            //    ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

#endif 


            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
#if !DEBUG
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;


            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 200;
            ServicePointManager.MaxServicePointIdleTime = 2000;
            ServicePointManager.SetTcpKeepAlive(false, 0, 0);
#endif 

            webRequest.Headers.Add("SOAPAction", action);
            webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            webRequest.Accept = "text/xml";
            webRequest.Method = "POST";
            return webRequest;
        }

        private static HttpWebRequest InsertSoapEnvelopeIntoWebRequest(XmlDocument soapEnvelopeXml, HttpWebRequest webRequest)
        {
            using (Stream stream = webRequest.GetRequestStream())
            {
                soapEnvelopeXml.Save(stream);
                return webRequest;
            }
        }

        public ActionResult GetGDSRoomTypes1(long id, string CheckIn, string CheckOut, string place)
        {
            List<CLayer.SearchResult> AmadeusResultList = new List<CLayer.SearchResult>();

            List<CLayer.Property> objDescriptions = BLayer.Property.GetAllGDSPropertyDescriptionsWithOutData();
            List<CLayer.Property> objTitles = BLayer.Property.GetAllGDSPropertyTitlesWithOutData();

            TempData["DetailUrl"] = Request.Url.AbsoluteUri;
            TempData.Keep("DetailUrl");

            DateTime startss = DateTime.Parse(CheckIn);
            DateTime endss = DateTime.Parse(CheckOut);
            StringBuilder Description = new StringBuilder();
            var startt = startss.ToString("yyyy-MM-dd");
            var endd = endss.ToString("yyyy-MM-dd");

            Models.PropertyModel property = new Models.PropertyModel();
            List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
            CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();

            try
            {
                DataTable dtHoelCode = BLayer.Property.GetHotelIDFrompropertyid(id);
                var hotelcode = dtHoelCode.Rows[0]["Hotel_Id"].ToString();


                var ChainCode = "HS";
                var AgeQualifyingCode = "10";
                var count = "2";
                property.PropertyId = id;
                property.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;

                string result = HotelMultiSingleAvailability(hotelcode, startt, endd);

                Serializer ser1 = new Serializer();
                CLayer.HotelAvailability.Envelope HotelResult = new CLayer.HotelAvailability.Envelope();
                CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
                CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();

                try
                {

                    HotelResult = ser1.Deserialize<CLayer.HotelAvailability.Envelope>(result);
                }
                catch (Exception ex)
                {
                    try
                    {
                        HotelResultAdv = ser1.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(result);
                    }
                    catch (Exception ex1)
                    {
                        HotelResultAdvFirst = ser1.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(result);
                    }

                }



                #region property details update

                long AmadeusPropertyID = 0;
                string CityName = string.Empty;

                var HotelItems = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

                var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
                var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();


                long StateID = 0;
                string StateName = "";
                string City = place; //Convert.ToString(Request["Destination"]);
                int CountryID = 0;
                long CityId = 0;
                long OwnerID = 0;
                int GDSStateID = 0;
                int GDSCityID = 0;
                string GDStateName = "";
                string Destination = place;// Convert.ToString(Request["Destination"]);

                OwnerID = BLayer.B2B.GetSupplierID("amadeus");
                if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                {
                    StateID = BLayer.State.GetStateID(Destination).StateId;
                    StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                    CityId = BLayer.City.GetCityID(City).CityId;
                    CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;
                }
                else
                {
                    GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), Destination);
                    GDStateName = BLayer.State.Get(GDSStateID).Name;
                    GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                    CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
                }

                #region Hotel update

                if (HotelResult.Body != null)
                {

                    if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        Session["GDSCurrencyConversion"] = objCurrencyConversion;
                    }
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;
                    }


                    if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                    {

                        List<string> HotelIDS = new List<string>();

                        foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                        {
                            HotelItems = item;
                            Models.PropertyModel objData = new Models.PropertyModel();
                            objData.PropertyId = 0;
                            objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());


                            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            objData.OwnerId = OwnerID;

                            objData.CityName = Destination;
                            CityName = Destination;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.State = Convert.ToInt32(StateID);
                                objData.StateName = StateName;
                                objData.City = CityName;
                                objData.CityId = Convert.ToInt32(CityId);
                            }
                            else
                            {

                                objData.State = GDSStateID;
                                objData.StateName = GDStateName;
                                objData.City = objData.CityName;
                                objData.CityId = GDSCityID;
                            }

                            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                            objData.Country = CountryID;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                            }
                            else
                            {
                                objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                            }

                            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                            {
                                objData.Location = objData.CityName + ", " + objData.StateName;
                                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                objData.Location = objData.Location + ", " + objData.CountryName;
                            }
                            string FullAddress = string.Empty;
                            List<string> LocationList = new List<string>();
                            LocationList.Add(objData.CityName);
                            LocationList.Add(objData.StateName);
                            LocationList.Add(objData.ZipCode);
                            LocationList.Add(objData.CountryName);
                            foreach (string itemLoc in LocationList)
                            {
                                if (!string.IsNullOrEmpty(itemLoc))
                                {
                                    FullAddress = FullAddress + itemLoc + ",";
                                }
                            }
                            objData.Location = FullAddress.TrimEnd(',');
                            property.Location = objData.Location;

                            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            objData.HotelId = item.BasicPropertyInfo.HotelCode;


                            AmadeusPropertyID = SaveAmadeus_Property(objData);



                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {

                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                                for (int i = 0; i < RoomStayRPHList.Length; i++)
                                {

                                    var RoomStays = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                    RoomStayItemList = RoomStays;
                                    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();
                                    AccomodationId = 0;
                                    foreach (var Accitem in objAcc)
                                    {
                                        //if ((Accitem.RoomStayRPH == Convert.ToInt32(RoomStayRPHList[i])) || (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                        //{
                                        //    AccomodationId = Accitem.AccommodationId;
                                        //    break;
                                        //}
                                        if (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode)
                                        {
                                            AccomodationId = Accitem.AccommodationId;
                                            break;
                                        }
                                    }

                                    if (AccomodationId == 0)
                                    {

                                        foreach (var RoomStayItem in RoomStays)
                                        {
                                            dataAccommodation.AccommodationId = AccomodationId;
                                            dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                            dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                            dataAccommodation.PropertyId = AmadeusPropertyID;
                                            dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                            dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                            if (RoomStayItem.RoomTypes != null)
                                            {
                                                dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                                dataAccommodation.RoomTypeDescription = APIUtility.RoomTypeDescriptionFirst(dataAccommodation.RoomTypeCode);
                                            }



                                            dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                            if (RoomStayItem.RoomRates != null)
                                            {
                                                dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                                dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                                dataAccommodation.Description = RoomStayItem.RoomRates.RoomRate.RoomRateDescription.Text[0].ToString();
                                            }
                                            if (RoomStayItem.RatePlans != null)
                                            {
                                                dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                            }


                                            AccommodationSave(dataAccommodation);


                                        }
                                    }

                                }
                                RoomStaysResultLists = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Request["gdscurrencycode"]), Convert.ToDecimal(Request["gdsrateconversion"]));
                            }

                        }

                    }



                    #endregion

                }
                else if (HotelResultAdvFirst.Body != null)
                {

                    if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                    {
                        var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                        CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                        objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                        Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                        objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                        objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                        objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                        Session["GDSCurrencyConversion"] = objCurrencyConversion;
                    }
                    if (Convert.ToString(Session["GDSCountry"]) == "")
                    {
                        Session["GDSCurrencyConversion"] = null;
                    }



                    if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                    {

                        foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                        {
                            HotelItemAdvFirst = item;
                            Models.PropertyModel objData = new Models.PropertyModel();
                            objData.PropertyId = 0;
                            objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                            objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                            objData.OwnerId = OwnerID;

                            objData.CityName = Destination;
                            CityName = Destination;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.State = Convert.ToInt32(StateID);
                                objData.StateName = StateName;
                                objData.City = CityName;
                                objData.CityId = Convert.ToInt32(CityId);
                            }
                            else
                            {

                                objData.State = GDSStateID;
                                objData.StateName = GDStateName;
                                objData.City = objData.CityName;
                                objData.CityId = GDSCityID;
                            }

                            objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                            objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                            objData.Country = CountryID;
                            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                            {
                                objData.CountryName = BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                            }
                            else
                            {
                                objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                            }

                            if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                            {
                                objData.Location = objData.CityName + ", " + objData.StateName;
                                if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                                objData.Location = objData.Location + ", " + objData.CountryName;
                            }

                            string FullAddress = string.Empty;
                            List<string> LocationList = new List<string>();
                            LocationList.Add(objData.CityName);
                            LocationList.Add(objData.StateName);
                            LocationList.Add(objData.ZipCode);
                            LocationList.Add(objData.CountryName);
                            foreach (string itemLoc in LocationList)
                            {
                                if (!string.IsNullOrEmpty(itemLoc))
                                {
                                    FullAddress = FullAddress + itemLoc + ",";
                                }
                            }
                            objData.Location = FullAddress.TrimEnd(',');
                            property.Location = objData.Location;


                            objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                            objData.HotelId = item.BasicPropertyInfo.HotelCode;

                            AmadeusPropertyID = SaveAmadeus_Property(objData);



                            long AccomodationId = 0;
                            string RoomStayRPH = item.RoomStayRPH;
                            List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                            if (!string.IsNullOrEmpty(RoomStayRPH))
                            {
                                //BLayer.Accommodation.Accommodation_Amadeus_Delete(AmadeusPropertyID);
                                string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                                List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                                for (int i = 0; i < RoomStayRPHList.Length; i++)
                                {

                                    var RoomStays = HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                    RoomStayItemListAdvFirst = RoomStays;
                                    Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();

                                    AccomodationId = 0;
                                    foreach (var Accitem in objAcc)
                                    {
                                        if (Accitem.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode)
                                        {
                                            AccomodationId = Accitem.AccommodationId;
                                            break;
                                        }
                                    }
                                    if (AccomodationId == 0)
                                    {
                                        foreach (var RoomStayItem in RoomStays)
                                        {
                                            dataAccommodation.AccommodationId = AccomodationId;
                                            dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                            dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                            dataAccommodation.PropertyId = AmadeusPropertyID;
                                            dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                            dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                            if (RoomStayItem.RoomTypes != null)
                                            {
                                                dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                            }


                                            dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                            if (RoomStayItem.RoomRates != null)
                                            {
                                                dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                                dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                                dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                            }
                                            if (RoomStayItem.RatePlans != null)
                                            {
                                                dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                            }


                                            AccommodationSave(dataAccommodation);
                                        }

                                    }

                                }
                                RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Request["gdscurrencycode"]), Convert.ToDecimal(Request["gdsrateconversion"]));
                            }
                        }
                    }
                }

                #endregion



                DataTable details = BLayer.Property.GetAccommodationDetailsFrompropertyid(id);
                if (details.Rows.Count > 0)
                {
                    hotelcode = details.Rows[0]["Hotel_Id"].ToString();
                }


                Serializer ser = new Serializer();
                string hotel = GetGDS_Hotel_Details(hotelcode);


                #region Transaction Log 

                APIUtility.GenerateGDSTransactionLog(Request.Url.AbsoluteUri.ToString(), hotel, Convert.ToInt32(User.Identity.GetUserId()), (int)CLayer.ObjectStatus.GDSType.HotelDescriptiveinfo, 0);

                #endregion Transaction log end
                CLayer.GDSBookingDetails.Envelope ss = new CLayer.GDSBookingDetails.Envelope();
                ss = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);

                CLayer.GDSBookingDetailsAdv.Envelope ssAdv = new CLayer.GDSBookingDetailsAdv.Envelope();


                var DescriptionItemList = objDescriptions.Where(x => x.HotelID == hotelcode).ToList();


                if (ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo != null)
                {
                    var description = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;

                    if (description != null)
                    {
                        var descriptionItem = description.MultimediaDescriptions.MultimediaDescription;

                        var Descriptions = from order in descriptionItem
                                           where order.TextItems != null
                                           select order;
                        string HotelDescription = string.Empty;
                        foreach (var desc in Descriptions)
                        {
                            if (desc.InfoCode != null)
                            {
                                foreach (var datas in desc.TextItems.TextItem.Description)
                                {


                                    HotelDescription = HotelDescription + datas.__Text + "#256#";

                                }
                            }
                            //    HotelDescription = HotelDescription + "<br>";

                        }
                        Description.Append(HotelDescription);

                        if (DescriptionItemList != null)
                        {
                            if (DescriptionItemList.Count > 0)
                            {
                                BLayer.Property.GDSUpdatePropertyDescription(id, Description.ToString());
                            }
                        }
                        property.Description = Description.Replace("-Property description-", "").ToString();
                        int pLength = property.Description.Trim().Length - 1;
                        property.Description = property.Description.Trim().Substring(0, 1).ToUpper().ToString() + property.Description.Trim().Substring(1, pLength);

                    }
                }



                var HotelItem = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
                var SecurityToken = HotelResult.Header.Session.SecurityToken;
                var SessionId = HotelResult.Header.Session.SessionId;
                var SequenceNumber = HotelResult.Header.Session.SequenceNumber.ToString();

                //Roomstayresult
                List<CLayer.RoomStaysResult> RoomStaysResultList = null;
                RoomStaysResultList = null;
                foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                {
                    HotelItem = item;

                    string RoomStayRPH = item.RoomStayRPH;
                    RoomStaysResultList = new List<CLayer.RoomStaysResult>();
                    if (!string.IsNullOrEmpty(RoomStayRPH))
                    {
                        string[] RoomStayRPHList1 = RoomStayRPH.Split(' ');

                        RoomStaysResultList = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList1, Convert.ToString(Session["GDSCurrencyCode"]), Convert.ToDecimal(Session["GDSRateConversion"]));
                    }
                }
                //Roomstayresult end

                foreach (DataRow dt in details.Rows)
                {
                    Accommodationsss = new CLayer.Accommodation();
                    var BookingCode = dt["BookingCode"].ToString();
                    var RatePlanCode = dt["RatePlanCode"].ToString();
                    var RoomTypeCode = dt["RoomTypeCode"].ToString();
                    long AccommodationID = Convert.ToInt64(dt["accommodationid"]);

                    var RoomStaysitem = RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList();

                    foreach (var Ritem in RoomStaysitem)
                    {
                        var item = Ritem.RoomRateDescriptionItem;
                        CLayer.RoomRateDescription objRoomRateDescription = new CLayer.RoomRateDescription();
                        objRoomRateDescription.Name = item.Name;
                        Accommodationsss.AccommodationType = objRoomRateDescription.Name;// RoomStaysResultList.Where(x => x.BookingCode == BookingCode).FirstOrDefault().RoomRateDescriptionItem.Name;//.RoomRateDescription.Name;

                        var descriptionItem = Ritem.RoomRateDescriptionItem.Description;

                        string RateTypeDesc = string.Empty;
                        foreach (CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText m in descriptionItem)
                        {
                            RateTypeDesc = RateTypeDesc + m.Value + ".";
                        }
                        Accommodationsss.Description = RateTypeDesc;

                        Accommodationsss.RoomTypeCode = Ritem.RoomTypeCode;

                        Accommodationsss.RoomTypeDecription = (Ritem.RoomTypeCode == "ROH") ? "Run of house*" : APIUtility.GenerateRoomDescription(Ritem.RoomTypeCode);
                        Accommodationsss.RoomTypeDecription = string.IsNullOrEmpty(Accommodationsss.RoomTypeDecription) ? "" : Accommodationsss.RoomTypeDecription.TrimEnd(',');


                        var rate = Ritem.AmountAfterTax;
                        Accommodationsss.Rate = Convert.ToDecimal(rate);
                        Accommodationsss.MaxNoOfChildren = 2;
                        Accommodationsss.MaxNoOfPeople = 3;
                        Accommodationsss.MaxNoOfPeople = 1;
                        Accommodationsss.AccommodationCount = 3;
                        Accommodationsss.AccommodationId = AccommodationID;
                        Accommodationss.Add(Accommodationsss);
                    }

                }
                //Find images for property

                long imageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                #region UPDATE PROPERTY IMAGES
                long ImageCount = BLayer.Property.GetGDSPropertyImagesCount(id);
                if (ImageCount < 1)
                {
                    // BLayer.Property.DeleteGDSPropertyImages(id);
                    List<CLayer.PropertyFiles> pictlist = GetGDSImages(hotel, id);
                    if (pictlist.Count > 0)
                    {
                        property.Pictures.Pictures = pictlist.ToList();
                    }
                }
                else
                {
                    List<string> images = BLayer.Property.GetGDSHotelAllImages(id);
                    List<CLayer.PropertyFiles> picturelist = new List<CLayer.PropertyFiles>();
                    CLayer.PropertyFiles picture = null;
                    foreach (string item in images)
                    {
                        picture = new CLayer.PropertyFiles();
                        picture.FileName = item;
                        picture.PropertyId = id;
                        picturelist.Add(picture);
                    }
                    property.Pictures.Pictures = picturelist.ToList();
                }
                //}


                #endregion
                var ssw = ss.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelInfo.Descriptions;





                if (ssw != null)
                {

                    var sswList = ssw;

                }


                CLayer.GDSBookingDetails.Envelope GDSBookingDetails = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);
                property.Title = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelName;
                DateTime starts = DateTime.Parse(CheckIn);
                DateTime ends = DateTime.Parse(CheckOut);
                property.Filter.HiddenCheckIn = starts;
                property.Filter.HiddenCheckOut = ends;
                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses != null)
                {
                    property.ZipCode = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.PostalCode;
                    property.Address = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.AddressLine;
                    property.StateName = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                    property.City = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                    property.Location = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;

                }

                property.PropertyId = id;

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails != null)
                {
                    property.Email = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails.Email.ToString();
                }

                if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones != null)
                {
                    if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").Count() > 0)
                    {
                        property.Phone = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "1").FirstOrDefault().PhoneNumber;

                    }
                    if (GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").Count() > 0)
                    {
                        property.Mobile = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone.Where(x => x.PhoneTechType == "5").FirstOrDefault().PhoneNumber;
                    }
                    if ((!string.IsNullOrEmpty(property.Phone)) || (!string.IsNullOrEmpty(property.Mobile)) || (!string.IsNullOrEmpty(property.Email)))
                    {
                        BLayer.Property.GDSUpdatePropertyContactNumbers(id, property.Phone, property.Mobile, property.Email);
                    }
                }


                property.HotelId = GDSBookingDetails.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelCode;

                //   property.Accommodations.Accommodations = Accommodationss;

            }
            catch (Exception ex)
            {

            }
            property.Accommodations.Accommodations = Accommodationss;
            return View("~/Areas/Admin/Views/OfflineBookingGST/GDSRoomtypeList.cshtml", property);
            //   return View("GDSIndex", property);
            //  return View("Index", property);
        }

        public ActionResult GetGDSRoomTypes(long id, string CheckIn, string CheckOut, string place)
        {
            DataTable details = BLayer.Property.GetAccommodationDetailsFrompropertyid(id);

            List<CLayer.Property> objDescriptions = BLayer.Property.GetAllGDSPropertyDescriptionsWithOutData();
            List<CLayer.Property> objTitles = BLayer.Property.GetAllGDSPropertyTitlesWithOutData();

            List<string> objNonAccList = new List<string>();

            var hotelcode = (details.Rows.Count > 0) ? details.Rows[0]["Hotel_Id"].ToString() : "";
            Serializer ser = new Serializer();
            Serializer HotelResults = new Serializer();
            CLayer.GDSPriceingDetails.Envelope GDSPriceingDetails = new CLayer.GDSPriceingDetails.Envelope();
            StayBazar.Models.PropertyModel property = new StayBazar.Models.PropertyModel();
            List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
            CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();

            CLayer.SearchCriteria ch = new CLayer.SearchCriteria();

            List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();

            DateTime start = DateTime.Parse(CheckIn);
            DateTime end = DateTime.Parse(CheckOut);
            ch.CheckIn = start;
            ch.CheckOut = end;
            ch.Destination = place;

            DataTable dtHotelCode = BLayer.Property.GetHotelIDFrompropertyid(Convert.ToInt64(id));
            string HotelCode = string.Empty;
            if (dtHotelCode.Rows.Count > 0)
            {
                HotelCode = Convert.ToString(dtHotelCode.Rows[0]["Hotel_Id"]);
            }

            //    string searchedresult = HotelMultiSingleAvailability(ch, false, "");
            string searchedresult = HotelMultiSingleAvailability(HotelCode, start.ToString("yyyy-MM-dd"), end.ToString("yyyy-MM-dd"));


            //  string searchedresult = HotelMultiSingleAvailability(ch, false, "");
            CLayer.HotelAvailability.Envelope HotelResult = HotelResults.Deserialize<CLayer.HotelAvailability.Envelope>(searchedresult);
            CLayer.HotelAvailabilityAdvanced.Envelope HotelResultAdv = new CLayer.HotelAvailabilityAdvanced.Envelope();
            CLayer.HotelAvailabilityAdvancedFirst.Envelope HotelResultAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.Envelope();
            try
            {

                HotelResult = ser.Deserialize<CLayer.HotelAvailability.Envelope>(searchedresult);
            }
            catch (Exception ex)
            {
                try
                {
                    HotelResultAdv = ser.Deserialize<CLayer.HotelAvailabilityAdvanced.Envelope>(searchedresult);
                }
                catch (Exception ex1)
                {
                    HotelResultAdvFirst = ser.Deserialize<CLayer.HotelAvailabilityAdvancedFirst.Envelope>(searchedresult);
                }

            }


            var SecurityToken = HotelResult.Header.Session.SecurityToken;
            var roomstaystpe = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay;
            var SessionId = HotelResult.Header.Session.SessionId;
            var SequenceNumber = HotelResult.Header.Session.SequenceNumber.ToString();


            #region property details update

            long AmadeusPropertyID = 0;
            string CityName = string.Empty;

            var HotelItems = new CLayer.HotelAvailability.OTA_HotelAvailRSHotelStay();
            var RoomStayItemList = new List<CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStay>();

            var HotelItemAdvFirst = new CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSHotelStay();
            var RoomStayItemListAdvFirst = new List<CLayer.HotelAvailabilityAdvancedFirst.OTA_HotelAvailRSRoomStaysRoomStay>();
            //   SearchController objSearch = new SearchController();


            long StateID = 0;
            string StateName = "";
            string City = place;// Convert.ToString(Request["Destination"]);
            int CountryID = 0;
            long CityId = 0;
            long OwnerID = 0;
            int GDSStateID = 0;
            int GDSCityID = 0;
            string GDStateName = "";
            string Destination = City;// Convert.ToString(Request["Destination"]);

            #region country and destination
            if (City != null && City != "")
            {
                City = Common.Utils.SecureInputString(City); //srch.Destination.Replace("'", "").Replace(";", "").Replace("\"", "");
                Session["GDSCountry"] = APIUtility.GetGDSCountry(City);
                string CountryName = Convert.ToString(Session["GDSCountry"]);

                if (CountryName.ToUpper() == "INDIA")
                {
                    CountryName = "";
                    Session["GDSCountry"] = "";
                }
                List<CLayer.GDSCountry> GDSCountryList = new List<CLayer.GDSCountry>();
                GDSCountryList = BLayer.Country.GetGDSCountry(CountryName);
                int GDSCountryCount = (CountryName != "") ? GDSCountryList.Count : 0;
                string sDestination = string.Empty;
                if (GDSCountryCount > 0)
                {
                    sDestination = APIUtility.GetGDSDestination(City);
                    Session["GDSCountryCode"] = GDSCountryList[0].IATACode;
                    Session["GDSCurrencyCode"] = GDSCountryList[0].CurrencyCode;
                    Session["GDSCountryID"] = GDSCountryList[0].CountryID;
                }
                else
                {
                    sDestination = APIUtility.GetGDSDestination(City);

                    if ((string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"]))) || (Convert.ToString(Session["GDSCountry"]).ToUpper() != "INDIA"))
                    {
                        if (CountryName == "")
                        {
                            Session["GDSCountry"] = "";
                            Session["GDSCountryID"] = "1";
                            Session["GDSCountryCode"] = "IN";
                            Session["GDSCurrencyCode"] = "INR";
                        }

                    }
                }

            }
            #endregion

            OwnerID = BLayer.B2B.GetSupplierID("amadeus");
            if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
            {
                StateID = BLayer.State.GetStateID(Destination).StateId;
                StateName = BLayer.State.Get(Convert.ToInt32(StateID)).Name;
                CityId = BLayer.City.GetCityID(City).CityId;
                CountryID = BLayer.State.Get(Convert.ToInt32(StateID)).CountryId;

            }
            else
            {
                GDSStateID = BLayer.State.UpdateGDSState(Convert.ToInt32(Session["GDSCountryID"]), Destination);
                GDStateName = BLayer.State.Get(GDSStateID).Name;
                GDSCityID = BLayer.City.UpdateGDSCity(GDSStateID, City);
                CountryID = BLayer.State.Get(Convert.ToInt32(GDSStateID)).CountryId;
            }

            #region Hotel update
            string gdscurrencycode = "";
            decimal gdsrateconversion = 0;
            string PropertyAddress = string.Empty;
            if (HotelResult.Body != null)
            {

                if (HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                {
                    var item = HotelResult.Body.OTA_HotelAvailRS.CurrencyConversions;
                    CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                    objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                    Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                    objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                    objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                    objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                    Session["GDSCurrencyConversion"] = objCurrencyConversion;

                    gdscurrencycode = objCurrencyConversion.SourceCurrencyCode;
                    gdsrateconversion = objCurrencyConversion.RateConversion;

                    Session["GDSRateConversion"] = gdsrateconversion;
                    Session["GDSCurrencyCode"] = gdscurrencycode;

                }
                if (Convert.ToString(Session["GDSCountry"]) == "")
                {
                    Session["GDSCurrencyConversion"] = null;

                }


                if (HotelResult.Body.OTA_HotelAvailRS.HotelStays != null)
                {

                    List<string> HotelIDS = new List<string>();

                    foreach (var item in HotelResult.Body.OTA_HotelAvailRS.HotelStays)
                    {
                        HotelItems = item;
                        Models.PropertyModel objData = new Models.PropertyModel();
                        objData.PropertyId = 0;
                        objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());


                        objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                        objData.OwnerId = OwnerID;

                        objData.CityName = Destination;
                        CityName = Destination;
                        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                        {
                            objData.State = Convert.ToInt32(StateID);
                            objData.StateName = StateName;
                            objData.City = CityName;
                            objData.CityId = Convert.ToInt32(CityId);
                        }
                        else
                        {

                            objData.State = GDSStateID;
                            objData.StateName = GDStateName;
                            objData.City = objData.CityName;
                            objData.CityId = GDSCityID;
                        }

                        objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                        objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                        objData.Country = CountryID;
                        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                        {
                            objData.CountryName = "India"; //BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                            gdscurrencycode = "INR";
                            gdsrateconversion = 0;
                            Session["GDSRateConversion"] = "0";
                            Session["GDSCurrencyCode"] = "INR";
                        }
                        else
                        {
                            objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                        }

                        if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                        {
                            objData.Location = objData.CityName + ", " + objData.StateName;
                            if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                            objData.Location = objData.Location + ", " + objData.CountryName;
                        }
                        string FullAddress = string.Empty;
                        List<string> LocationList = new List<string>();
                        LocationList.Add(objData.CityName);
                        LocationList.Add(objData.StateName);
                        LocationList.Add(objData.ZipCode);
                        LocationList.Add(objData.CountryName);
                        foreach (string itemLoc in LocationList)
                        {
                            if (!string.IsNullOrEmpty(itemLoc))
                            {
                                FullAddress = FullAddress + itemLoc + ",";
                            }
                        }
                        objData.Location = FullAddress.TrimEnd(',');
                        property.Location = objData.Location;
                        property.Address = APIUtility.FirstLetterToUpperCase(objData.Address + " " + property.Location);
                        //     PropertyAddress = property.Address;
                        PropertyAddress = APIUtility.ReplaceFirstOccurrence(property.Address, Destination, "");
                        PropertyAddress = APIUtility.ReplaceFirstOccurrence(PropertyAddress, objData.CountryName, "").Replace(",,", ",");


                        objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                        objData.HotelId = item.BasicPropertyInfo.HotelCode;


                        AmadeusPropertyID = SaveAmadeus_Property(objData);



                        long AccomodationId = 0;
                        string RoomStayRPH = item.RoomStayRPH;
                        //   RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                        if (!string.IsNullOrEmpty(RoomStayRPH))
                        {

                            string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                            List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                            for (int i = 0; i < RoomStayRPHList.Length; i++)
                            {

                                var RoomStays = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                RoomStayItemList = RoomStays;
                                Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();
                                AccomodationId = 0;
                                foreach (var Accitem in objAcc.Where(x => x.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                {

                                    AccomodationId = Accitem.AccommodationId;
                                    break;

                                }

                                if (AccomodationId == 0)
                                {

                                    foreach (var RoomStayItem in RoomStays)
                                    {
                                        dataAccommodation.AccommodationId = AccomodationId;
                                        dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                        dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                        dataAccommodation.PropertyId = AmadeusPropertyID;
                                        dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                        dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                        if (RoomStayItem.RoomTypes != null)
                                        {
                                            dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                            dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                            if (dataAccommodation.RoomTypeCode != "")
                                            {
                                                dataAccommodation.RoomTypeDescription = APIUtility.RoomTypeDescriptionFirst(dataAccommodation.RoomTypeCode);
                                            }

                                        }



                                        dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                        if (RoomStayItem.RoomRates != null)
                                        {
                                            dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                            dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                            dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                            dataAccommodation.Description = RoomStayItem.RoomRates.RoomRate.RoomRateDescription.Text[0].ToString();
                                        }
                                        if (RoomStayItem.RatePlans != null)
                                        {
                                            dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                        }


                                        AccommodationSave(dataAccommodation);


                                    }

                                }
                                else if (AccomodationId > 0)
                                {
                                    objNonAccList.Add(AccomodationId.ToString());
                                }

                            }
                            RoomStaysResultLists = GetRoomStays(HotelResult.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, gdscurrencycode, gdsrateconversion);
                        }

                    }

                }



                #endregion

            }
            else if (HotelResultAdvFirst.Body != null)
            {

                if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions != null)
                {
                    var item = HotelResultAdvFirst.Body.OTA_HotelAvailRS.CurrencyConversions;
                    CLayer.GDSCurrencyConversions objCurrencyConversion = new CLayer.GDSCurrencyConversions();
                    objCurrencyConversion.RateConversion = item.CurrencyConversion.RateConversion;
                    Session["GDSRateConversion"] = objCurrencyConversion.RateConversion;
                    objCurrencyConversion.DecimalPlaces = item.CurrencyConversion.DecimalPlaces;
                    objCurrencyConversion.RequestedCurrencyCode = item.CurrencyConversion.RequestedCurrencyCode;
                    objCurrencyConversion.SourceCurrencyCode = item.CurrencyConversion.SourceCurrencyCode;
                    Session["GDSCurrencyConversion"] = objCurrencyConversion;

                    gdscurrencycode = objCurrencyConversion.SourceCurrencyCode;
                    gdsrateconversion = objCurrencyConversion.RateConversion;
                    Session["GDSRateConversion"] = gdsrateconversion;
                    Session["GDSCurrencyCode"] = gdscurrencycode;
                }
                if (Convert.ToString(Session["GDSCountry"]) == "")
                {
                    Session["GDSCurrencyConversion"] = null;
                }



                if (HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays != null)
                {

                    foreach (var item in HotelResultAdvFirst.Body.OTA_HotelAvailRS.HotelStays)
                    {
                        HotelItemAdvFirst = item;
                        Models.PropertyModel objData = new Models.PropertyModel();
                        objData.PropertyId = 0;
                        objData.Title = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.BasicPropertyInfo.HotelName.ToLower());
                        objData.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                        objData.OwnerId = OwnerID;

                        objData.CityName = Destination;
                        CityName = Destination;
                        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                        {
                            objData.State = Convert.ToInt32(StateID);
                            objData.StateName = StateName;
                            objData.City = CityName;
                            objData.CityId = Convert.ToInt32(CityId);
                        }
                        else
                        {

                            objData.State = GDSStateID;
                            objData.StateName = GDStateName;
                            objData.City = objData.CityName;
                            objData.CityId = GDSCityID;
                        }

                        objData.Address = (item.BasicPropertyInfo.Address.AddressLine.Count() > 1) ? item.BasicPropertyInfo.Address.AddressLine[0].ToString() + "," + item.BasicPropertyInfo.Address.AddressLine[1].ToString() : item.BasicPropertyInfo.Address.AddressLine[0].ToString();
                        objData.ZipCode = Convert.ToString(item.BasicPropertyInfo.Address.PostalCode);

                        objData.Country = CountryID;
                        if (string.IsNullOrEmpty(Convert.ToString(Session["GDSCountry"])))
                        {
                            objData.CountryName = BLayer.Settings.GetValue(CLayer.Settings.AMADEUSWBSCOUNTRY);
                            gdscurrencycode = "INR";
                            gdsrateconversion = 0;
                            Session["GDSRateConversion"] = "0";
                            Session["GDSCurrencyCode"] = "INR";
                        }
                        else
                        {
                            objData.CountryName = Convert.ToString(Session["GDSCountry"]);
                        }

                        if ((objData.PositionLat == "0" && objData.PositionLng == "0") || (objData.PositionLat == null && objData.PositionLng == null))
                        {
                            objData.Location = objData.CityName + ", " + objData.StateName;
                            if (objData.ZipCode != "") objData.Location = objData.Location + " " + objData.ZipCode;
                            objData.Location = objData.Location + ", " + objData.CountryName;
                        }

                        string FullAddress = string.Empty;
                        List<string> LocationList = new List<string>();
                        LocationList.Add(objData.CityName);
                        LocationList.Add(objData.StateName);
                        LocationList.Add(objData.ZipCode);
                        LocationList.Add(objData.CountryName);
                        foreach (string itemLoc in LocationList)
                        {
                            if (!string.IsNullOrEmpty(itemLoc))
                            {
                                FullAddress = FullAddress + itemLoc + ",";
                            }
                        }
                        objData.Location = FullAddress.TrimEnd(',');
                        property.Location = objData.Location;
                        property.Address = APIUtility.FirstLetterToUpperCase(objData.Address + " " + property.Location);
                        //   PropertyAddress = property.Address;
                        PropertyAddress = APIUtility.ReplaceFirstOccurrence(property.Address, Destination, "");
                        PropertyAddress = APIUtility.ReplaceFirstOccurrence(PropertyAddress, objData.CountryName, "").Replace(",,", ",");



                        objData.InventoryAPIType = (int)CLayer.ObjectStatus.InventoryAPIType.Amadeus;
                        objData.HotelId = item.BasicPropertyInfo.HotelCode;

                        AmadeusPropertyID = SaveAmadeus_Property(objData);



                        long AccomodationId = 0;
                        string RoomStayRPH = item.RoomStayRPH;
                        //     List<CLayer.RoomStaysResult> RoomStaysResultLists = new List<CLayer.RoomStaysResult>();
                        if (!string.IsNullOrEmpty(RoomStayRPH))
                        {
                            //BLayer.Accommodation.Accommodation_Amadeus_Delete(AmadeusPropertyID);
                            string[] RoomStayRPHList = RoomStayRPH.Split(' ');
                            List<CLayer.Accommodation> objAcc = BLayer.Accommodation.GetAllAccByPropertyid(AmadeusPropertyID);

                            for (int i = 0; i < RoomStayRPHList.Length; i++)
                            {

                                var RoomStays = HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays.RoomStay.Where(s => s.RPH == Convert.ToInt32(RoomStayRPHList[i])).ToList();
                                RoomStayItemListAdvFirst = RoomStays;
                                Models.AccommodationModel dataAccommodation = new Models.AccommodationModel();

                                AccomodationId = 0;
                                foreach (var Accitem in objAcc.Where(x => x.BookingCode == RoomStays[0].RoomRates.RoomRate.BookingCode))
                                {
                                    AccomodationId = Accitem.AccommodationId;
                                    break;

                                }
                                if (AccomodationId == 0)
                                {
                                    foreach (var RoomStayItem in RoomStays)
                                    {
                                        dataAccommodation.AccommodationId = AccomodationId;
                                        dataAccommodation.AccommodationTypeId = (int)CLayer.ObjectStatus.AmedusDefaultAccommodationType.SingleBedRoom;
                                        dataAccommodation.StayCategoryId = (int)CLayer.ObjectStatus.AmedusDefaultStayCategory.Hotel;
                                        dataAccommodation.PropertyId = AmadeusPropertyID;
                                        dataAccommodation.Status = (int)CLayer.ObjectStatus.StatusType.Active;
                                        dataAccommodation.RoomStayRPH = Convert.ToInt32(RoomStayRPHList[i]);
                                        if (RoomStayItem.RoomTypes != null)
                                        {
                                            dataAccommodation.RoomType = (RoomStayItem.RoomTypes.RoomType.RoomType == "") ? "" : RoomStayItem.RoomTypes.RoomType.RoomType;
                                            dataAccommodation.RoomTypeCode = RoomStayItem.RoomTypes.RoomType.RoomTypeCode;
                                        }


                                        dataAccommodation.SourceofBusiness = RoomStayItem.SourceOfBusiness;
                                        if (RoomStayItem.RoomRates != null)
                                        {
                                            dataAccommodation.BookingCode = RoomStayItem.RoomRates.RoomRate.BookingCode;
                                            dataAccommodation.RoomTypeCode = RoomStayItem.RoomRates.RoomRate.RoomTypeCode;
                                            dataAccommodation.RatePlanCode = RoomStayItem.RoomRates.RoomRate.RatePlanCode;
                                        }
                                        if (RoomStayItem.RatePlans != null)
                                        {
                                            dataAccommodation.RatePlanCode = RoomStayItem.RatePlans.RatePlan.RatePlanCode;
                                        }


                                        AccommodationSave(dataAccommodation);
                                    }
                                    objNonAccList.Add(AccomodationId.ToString());
                                }
                                else if (AccomodationId > 0)
                                {
                                    objNonAccList.Add(AccomodationId.ToString());
                                }

                            }
                            //       RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, Convert.ToString(Request["gdscurrencycode"]), Convert.ToDecimal(Request["gdsrateconversion"]));
                            RoomStaysResultLists = GetRoomStaysAdv(HotelResultAdvFirst.Body.OTA_HotelAvailRS.RoomStays, RoomStayRPHList, gdscurrencycode, gdsrateconversion);
                        }
                    }
                }
            }

            #endregion

            //if(objNonAccList.Count>0)
            //{
            //    BLayer.Accommodation.UpdateGDSAccommodationAvailability(objNonAccList, id);
            //}
            DataTable detailsAdv = BLayer.Property.GetAccommodationDetailsFrompropertyid(id);
            if (detailsAdv.Rows.Count > 0)
            {
                hotelcode = detailsAdv.Rows[0]["Hotel_Id"].ToString();
            }




            string hotel = GetGDS_Hotel_Details(HotelCode);

            CLayer.GDSBookingDetails.Envelope GDS_Hotel_Details = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);

            var ChainCode = "HS";
            var AgeQualifyingCode = "10";
            var count = "2";
            if (GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses != null)
            {
                property.Address = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.AddressLine;
                property.ZipCode = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.PostalCode;
                property.StateName = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                property.City = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
                property.Location = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;

            }

            property.Title = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelName;
            property.PropertyId = id;
            if (GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones != null)
            {
                property.Mobile = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone[0].PhoneNumber;
            }

            property.SecurityToken = SecurityToken;
            property.SessionId = SessionId;
            property.SequenceNumber = SequenceNumber;
            property.hotelcode = hotelcode;
            property.Country = 1;
            property.CountryName = "India";
            if (GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails != null)
            {
                property.Email = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails.Email;
            }


            //foreach (DataRow dt in details.Rows)
            //{
            //    try
            //    { 
            //        CheckIn = start.ToString("yyyy-MM-dd");
            //        CheckOut = end.ToString("yyyy-MM-dd");
            //        Accommodationsss = new CLayer.Accommodation();
            //        var BookingCode = dt["BookingCode"].ToString();
            //        var RatePlanCode = dt["RatePlanCode"].ToString();
            //        var RoomTypeCode = dt["RoomTypeCode"].ToString();
            //        long AccommodationID = Convert.ToInt64(dt["accommodationid"]);

            //        string priceing = GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, RoomTypeCode, hotelcode, RatePlanCode, BookingCode, CheckIn, CheckOut, ChainCode, AgeQualifyingCode, count, id);
            //        GDSPriceingDetails = ser.Deserialize<CLayer.GDSPriceingDetails.Envelope>(priceing);
            //        if(GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays!=null)
            //        {
            //            Accommodationsss.AccommodationType = GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates.RoomRate.RoomRateDescription.Name;
            //            string GDSRoomTypeCode= GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates.RoomRate.RoomTypeCode;
            //            Accommodationsss.RoomTypeCode = GDSRoomTypeCode;

            //            Accommodationsss.RoomTypeDecription = (GDSRoomTypeCode == "ROH") ? "Run of house*" : APIUtility.GenerateRoomDescription(GDSRoomTypeCode);
            //            Accommodationsss.RoomTypeDecription = string.IsNullOrEmpty(Accommodationsss.RoomTypeDecription) ? "" : Accommodationsss.RoomTypeDecription.TrimEnd(',');


            //            StringBuilder cancel = new StringBuilder();
            //            Accommodationsss.Ownername = GDSPriceingDetails.Body.OTA_HotelAvailRS.HotelStays.HotelStay.BasicPropertyInfo.HotelName;
            //            if (GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.CancelPenalties.CancelPenalty.PenaltyDescription.Count() > 0)
            //            {
            //                var counts = GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.CancelPenalties.CancelPenalty.PenaltyDescription.Count();
            //                foreach (var s in GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.CancelPenalties.CancelPenalty.PenaltyDescription)
            //                {
            //                    cancel.Append(s.Text);
            //                }
            //            }
            //            var ss = cancel.Replace("\"", ",").ToString();
            //            Accommodationsss.cancel = ss;
            //            var rate = GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates.RoomRate.Total.AmountAfterTax;
            //            Accommodationsss.Rate = Convert.ToDecimal(rate);
            //            Accommodationsss.MaxNoOfChildren = 2;
            //            Accommodationsss.MaxNoOfPeople = 3;
            //            Accommodationsss.MaxNoOfPeople = 1;
            //            Accommodationsss.AccommodationCount = 3;
            //            Accommodationsss.AccommodationId = AccommodationID;
            //            Accommodationss.Add(Accommodationsss);
            //        }
            //    }
            //    catch(Exception ex)
            //    {
            //        continue;
            //    }
            //}
            foreach (DataRow dt in detailsAdv.Rows)
            {
                Accommodationsss = new CLayer.Accommodation();
                var BookingCode = dt["BookingCode"].ToString();
                var RatePlanCode = dt["RatePlanCode"].ToString();
                var RoomTypeCode = dt["RoomTypeCode"].ToString();
                long AccommodationID = Convert.ToInt64(dt["accommodationid"]);
                var RoomStaysitem = RoomStaysResultLists.Where(x => x.BookingCode == BookingCode).ToList();
                //  var RoomStaysitem = RoomStaysResultList.Where(x => x.BookingCode == BookingCode).ToList();

                foreach (var Ritem in RoomStaysitem)
                {
                    var item = Ritem.RoomRateDescriptionItem;
                    CLayer.RoomRateDescription objRoomRateDescription = new CLayer.RoomRateDescription();
                    objRoomRateDescription.Name = item.Name;
                    Accommodationsss.AccommodationType = objRoomRateDescription.Name;// RoomStaysResultList.Where(x => x.BookingCode == BookingCode).FirstOrDefault().RoomRateDescriptionItem.Name;//.RoomRateDescription.Name;

                    var descriptionItem = Ritem.RoomRateDescriptionItem.Description;

                    string RateTypeDesc = string.Empty;
                    foreach (CLayer.HotelAvailability.OTA_HotelAvailRSRoomStaysRoomStayRoomRatesRoomRateRoomRateDescriptionText m in descriptionItem)
                    {
                        RateTypeDesc = RateTypeDesc + m.Value + ".";
                    }
                    Accommodationsss.Description = RateTypeDesc;

                    Accommodationsss.RoomTypeCode = Ritem.RoomTypeCode;

                    Accommodationsss.RoomTypeDecription = (Ritem.RoomTypeCode == "ROH") ? "Run of house*" : APIUtility.GenerateRoomDescription(Ritem.RoomTypeCode);
                    Accommodationsss.RoomTypeDecription = string.IsNullOrEmpty(Accommodationsss.RoomTypeDecription) ? "" : Accommodationsss.RoomTypeDecription.TrimEnd(',');


                    var rate = Ritem.AmountAfterTax;
                    Accommodationsss.Rate = Convert.ToDecimal(rate);
                    Accommodationsss.MaxNoOfChildren = 2;
                    Accommodationsss.MaxNoOfPeople = 3;
                    Accommodationsss.MaxNoOfPeople = 1;
                    Accommodationsss.AccommodationCount = 3;
                    Accommodationsss.AccommodationId = AccommodationID;
                    Accommodationss.Add(Accommodationsss);
                }

            }
            property.City = City;
            property.Accommodations.Accommodations = Accommodationss.Where(x => x.Rate > 0).ToList();
            return View("~/Areas/Admin/Views/OfflineBookingGST/GDSRoomtypeList.cshtml", property);
        }


        //public ActionResult GetGDSRoomTypes(long id, string CheckIn, string CheckOut, string place)
        //{
        //DataTable details = BLayer.Property.GetAccommodationDetailsFrompropertyid(id);
        //var hotelcode = details.Rows[0]["Hotel_Id"].ToString();
        //Serializer ser = new Serializer();
        //Serializer HotelResults = new Serializer();
        //CLayer.GDSPriceingDetails.Envelope GDSPriceingDetails = new CLayer.GDSPriceingDetails.Envelope();
        //StayBazar.Models.PropertyModel property = new StayBazar.Models.PropertyModel();
        //List<CLayer.Accommodation> Accommodationss = new List<CLayer.Accommodation>();
        //CLayer.Accommodation Accommodationsss = new CLayer.Accommodation();

        //CLayer.SearchCriteria ch = new CLayer.SearchCriteria();

        //DateTime start = DateTime.Parse(CheckIn);
        //DateTime end = DateTime.Parse(CheckOut);
        //ch.CheckIn = start;
        //ch.CheckOut = end;
        //ch.Destination = place;

        //string searchedresult = HotelMultiSingleAvailability(ch, false, "");
        //CLayer.HotelAvailability.Envelope HotelResult = HotelResults.Deserialize<CLayer.HotelAvailability.Envelope>(searchedresult);

        //var SecurityToken = HotelResult.Header.Session.SecurityToken;
        //var roomstaystpe = HotelResult.Body.OTA_HotelAvailRS.RoomStays.RoomStay;
        //var SessionId = HotelResult.Header.Session.SessionId;
        //var SequenceNumber = HotelResult.Header.Session.SequenceNumber.ToString();

        //string hotel = GetGDS_Hotel_Details(hotelcode);
        //CLayer.GDSBookingDetails.Envelope GDS_Hotel_Details = ser.Deserialize<CLayer.GDSBookingDetails.Envelope>(hotel);

        //var ChainCode = "HS";
        //var AgeQualifyingCode = "10";
        //var count = "2";
        //property.Address = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.AddressLine;
        //property.Title = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.HotelName;
        //property.PropertyId = id;
        //property.ZipCode = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.PostalCode;
        //property.StateName = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
        //property.City = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
        //property.Location = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Addresses.Address.CityName;
        //property.Mobile = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Phones.Phone[0].PhoneNumber;
        //property.SecurityToken = SecurityToken;
        //property.SessionId = SessionId;
        //property.SequenceNumber = SequenceNumber;
        //property.hotelcode = hotelcode;
        //property.Country = 1;
        //property.CountryName = "India";
        //if (GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails != null)
        //{
        //    property.Email = GDS_Hotel_Details.Body.OTA_HotelDescriptiveInfoRS.HotelDescriptiveContents.HotelDescriptiveContent.ContactInfos.ContactInfo.Emails.Email;
        //}


        //foreach (DataRow dt in details.Rows)
        //{
        //    try
        //    {
        //        CheckIn = start.ToString("yyyy-MM-dd");
        //        CheckOut = end.ToString("yyyy-MM-dd");
        //        Accommodationsss = new CLayer.Accommodation();
        //        var BookingCode = dt["BookingCode"].ToString();
        //        var RatePlanCode = dt["RatePlanCode"].ToString();
        //        var RoomTypeCode = dt["RoomTypeCode"].ToString();
        //        long AccommodationID = Convert.ToInt64(dt["accommodationid"]);

        //        string priceing = GetGDS_priceing_Details(SecurityToken, SessionId, SequenceNumber, RoomTypeCode, hotelcode, RatePlanCode, BookingCode, CheckIn, CheckOut, ChainCode, AgeQualifyingCode, count, id);
        //        GDSPriceingDetails = ser.Deserialize<CLayer.GDSPriceingDetails.Envelope>(priceing);
        //        if (GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays != null)
        //        {
        //            Accommodationsss.AccommodationType = GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates.RoomRate.RoomRateDescription.Name;
        //            StringBuilder cancel = new StringBuilder();
        //            Accommodationsss.Ownername = GDSPriceingDetails.Body.OTA_HotelAvailRS.HotelStays.HotelStay.BasicPropertyInfo.HotelName;
        //            if (GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.CancelPenalties.CancelPenalty.PenaltyDescription.Count() > 0)
        //            {
        //                var counts = GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.CancelPenalties.CancelPenalty.PenaltyDescription.Count();
        //                foreach (var s in GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RatePlans.RatePlan.CancelPenalties.CancelPenalty.PenaltyDescription)
        //                {
        //                    cancel.Append(s.Text);
        //                }
        //            }
        //            var ss = cancel.Replace("\"", ",").ToString();
        //            Accommodationsss.cancel = ss;
        //            var rate = GDSPriceingDetails.Body.OTA_HotelAvailRS.RoomStays.RoomStay.RoomRates.RoomRate.Total.AmountAfterTax;
        //            Accommodationsss.Rate = Convert.ToDecimal(rate);
        //            Accommodationsss.MaxNoOfChildren = 2;
        //            Accommodationsss.MaxNoOfPeople = 3;
        //            Accommodationsss.MaxNoOfPeople = 1;
        //            Accommodationsss.AccommodationCount = 3;
        //            Accommodationsss.AccommodationId = AccommodationID;
        //            Accommodationss.Add(Accommodationsss);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        continue;
        //    }
        //}
        //    property.Accommodations.Accommodations = Accommodationss;
        //    return View("~/Areas/Admin/Views/OfflineBookingGST/GDSRoomtypeList.cshtml", property);
        //}

        public string GetGDS_Hotel_Details(string HotelCode)
        {
            string soapResult = string.Empty;

            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                //  var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/OTA_HotelDescriptiveInfoRQ_07.1_1A2007A";
                //var _url = "https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = "http://webservices.amadeus.com/OTA_HotelDescriptiveInfoRQ_07.1_1A2007A";


                string SoapHeader = APIUtility.hotel_descriptiveinfor_SetSoapHeader(_url, _action);
                string SoapBody = APIUtility.hotel_descriptiveinfor_detailsbody(HotelCode);

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(SoapEnvelop);



                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // suspend this thread until call is complete. You might want to
                // do something usefull here like update your UI.
                asyncResult.AsyncWaitHandle.WaitOne();

                // get the response from the completed web request.

                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                    Console.Write(soapResult);
                }

            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    soapResult = text;

                }
            }

            return soapResult;

        }

        public string GetGDS_priceing_Details(string SecurityToken, string SessionId, string SequenceNumber, string RoomTypeCode, string hotelcode, string RatePlanCode, string BookingCode, string Start, string end, string ChainCode, string AgeQualifyingCode, string count, long id)
        {
            string soapResult = string.Empty;

            try
            {
                string StayBazarWBSOffice = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSOFFICE);

                var _url = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSURL); //"https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                //  var _action = BLayer.Settings.GetValue(CLayer.Settings.STAYBAZARWBSHOTELMULTISINGLEACTION); //"http://webservices.amadeus.com/OTA_HotelDescriptiveInfoRQ_07.1_1A2007A";
                //var _url = "https://nodeD1.test.webservices.amadeus.com/1ASIWSTYSZRU";
                var _action = "http://webservices.amadeus.com/Hotel_EnhancedPricing_2.0";



                string SoapHeader = APIUtility.hotel_descriptiveinfor_Priceing_SetSoapHeaderbody(_url, _action, SecurityToken, SessionId, SequenceNumber);
                string SoapBody = APIUtility.hotel_descriptiveinfor_Priceing_detailsBody(hotelcode, RoomTypeCode, RatePlanCode, BookingCode, Start, end, ChainCode, AgeQualifyingCode, count);

                string SoapEnvelop = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
                SoapEnvelop = SoapEnvelop + "<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:sec=\"http://xml.amadeus.com/2010/06/Security_v1\" xmlns:typ=\"http://xml.amadeus.com/2010/06/Types_v1\" xmlns:iat=\"http://www.iata.org/IATA/2007/00/IATA2010.1\" xmlns:app=\"http://xml.amadeus.com/2010/06/AppMdw_CommonTypes_v3\" xmlns:link=\"http://wsdl.amadeus.com/2010/06/ws/Link_v1\" xmlns:ses=\"http://xml.amadeus.com/2010/06/Session_v3\" xmlns:ns=\"http://www.opentravel.org/OTA/2003/05\">";
                SoapEnvelop = SoapEnvelop + SoapHeader + SoapBody;
                SoapEnvelop = SoapEnvelop + "</soapenv:Envelope>";
                XmlDocument soapEnvelopeXml = new XmlDocument();
                soapEnvelopeXml.LoadXml(SoapEnvelop);

                HttpWebRequest webRequest = CreateWebRequest(_url, _action);
                webRequest = InsertSoapEnvelopeIntoWebRequest(soapEnvelopeXml, webRequest);

                // begin async call to web request.
                IAsyncResult asyncResult = webRequest.BeginGetResponse(null, null);

                // do something usefull here like update your UI.                // suspend this thread until call is complete. You might want to

                asyncResult.AsyncWaitHandle.WaitOne();

                // get the response from the completed web request.

                using (WebResponse webResponse = webRequest.EndGetResponse(asyncResult))
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        soapResult = rd.ReadToEnd();
                    }
                    Console.Write(soapResult);
                }

            }
            catch (WebException webex)
            {
                WebResponse errResp = webex.Response;
                using (Stream respStream = errResp.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(respStream);
                    string text = reader.ReadToEnd();
                    soapResult = text;

                }
            }

            return soapResult;

        }

        //public async Task<long> SaveAmadeus_Property(Models.PropertyModel data)
        //{
        //    try
        //    {

        //        CLayer.Property prprty = new CLayer.Property()
        //        {

        //            PropertyId = data.PropertyId,
        //            Title = data.Title,
        //            Description = data.Description,
        //            Location = data.Location,
        //            Status = (CLayer.ObjectStatus.StatusType)data.Status,
        //            OwnerId = data.OwnerId,
        //            Address = data.Address,
        //            Country = data.Country,
        //            State = data.State,
        //            City = data.City,
        //            CityId = data.CityId,
        //            ZipCode = data.ZipCode,
        //            PropertyInventoryType = data.InventoryAPIType,
        //            HotelID = data.HotelId


        //        };
        //        string loc = "";
        //        try
        //        {

        //            string statename = data.StateName;
        //            string cityname;
        //            //if (data.Country == 0)
        //            //{

        //            //}
        //            if (data.CityId < 1)
        //            {
        //                cityname = data.City;
        //            }
        //            else
        //            {
        //                cityname = BLayer.City.Get(data.CityId).Name;
        //                data.City = cityname;
        //                prprty.City = cityname;
        //            }
        //            string Countryname = data.CountryName;// BLayer.Country.Get(data.Country).Name;
        //            loc = cityname + "," + statename + "," + Countryname + "," + data.ZipCode;
        //            string qAdddress = data.Title + "," + data.Address + "," + loc;

        //            Common.Utils.Location pos;

        //            pos = await Common.Utils.GetLocation(qAdddress);
        //            prprty.PositionLat = pos.Lattitude.ToString();
        //            prprty.PositionLng = pos.Longitude.ToString();
        //        }
        //        catch (Exception ex)
        //        {
        //            Common.LogHandler.LogError(ex);
        //        }


        //        data.PropertyId = BLayer.Property.SaveAmadeus_Property(prprty);
        //        return data.PropertyId;

        //    }
        //    catch (Exception ex)
        //    {
        //        Common.LogHandler.HandleError(ex);
        //        return 0;
        //    }
        //}

        private List<CLayer.PropertyFiles> GetGDSImages(string response, long id)
        {
            List<CLayer.PropertyFiles> picturelist = new List<CLayer.PropertyFiles>();
            XmlDocument xmlDoc = new XmlDocument();
            string a = string.Empty;
            //  List<CLayer.PropertyFiles> pictureslist = new List<CLayer.PropertyFiles>();
            try
            {
                // xmlDoc.Load(@"F:\HotelDescriptiveIno.xml");
                xmlDoc.LoadXml(response);
                XmlNamespaceManager xmlnsManager = new System.Xml.XmlNamespaceManager(xmlDoc.NameTable);
                XmlNode node = null;
                XmlNode nodeImages = null;
                XmlNode nodeguestImages = null;

                string nodeRoot = "/soapenv:Envelope/soapenv:Body/";
                string nodevalue = "/si:OTA_HotelDescriptiveInfoRS/si:HotelDescriptiveContents/si:HotelDescriptiveContent/si:HotelInfo/si:Descriptions";//"/si:MultimediaDescriptions/si:MultimediaDescription/si:ImageItems/si:ImageItem/si:ImageFormat/si:URL";
                string nodevalueguestroom = "/si:OTA_HotelDescriptiveInfoRS/si:HotelDescriptiveContents/si:HotelDescriptiveContent/si:HotelInfo/si:CategoryCodes/si:CategoryCodes/si:GuestRoomInfo";//si:MultimediaDescriptions/si:MultimediaDescription/si:ImageItems/si:ImageItem/si:ImageFormat/si:URL";

                xmlnsManager.AddNamespace("soapenv", "http://schemas.xmlsoap.org/soap/envelope/");
                xmlnsManager.AddNamespace("awsse", "http://xml.amadeus.com/2010/06/Session_v3");
                xmlnsManager.AddNamespace("wsa", "http://www.w3.org/2005/08/addressing");
                xmlnsManager.AddNamespace("si", "http://www.opentravel.org/OTA/2003/05");

                nodeImages = xmlDoc.SelectSingleNode(nodeRoot + nodevalue, xmlnsManager);
                nodeguestImages = xmlDoc.SelectSingleNode(nodeRoot + nodevalueguestroom, xmlnsManager);

                #region MAIN IMAGES
                if (nodeImages != null)
                {
                    XmlNodeList list = nodeImages.SelectNodes("//si:MultimediaDescriptions/si:MultimediaDescription", xmlnsManager);
                    int PictureCount = 0;
                    int nodes = 0;
                    if (list != null)
                    {
                        foreach (XmlNode item in list)
                        {
                            var subItem = item.SelectNodes("//si:ImageItems", xmlnsManager);
                            if (subItem != null)
                            {
                                foreach (XmlNode sItem in subItem)
                                {
                                    IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDateTime(r.Attributes["LastModifyDateTime"].Value));
                                    // IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDecimal(r.Attributes["LastModifyDateTime"].Value));
                                    if (subInnerItem != null)
                                    {
                                        foreach (XmlNode sItem1 in subInnerItem)
                                        {
                                            var subUrlItem = sItem1.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager);
                                            if (subUrlItem != null)
                                            {
                                                PictureCount = subInnerItem.Count();
                                                foreach (XmlNode UItem in subUrlItem)
                                                {
                                                    var UrlItem = UItem.ChildNodes[0].InnerText;  //url
                                                    CLayer.PropertyFiles picture = new CLayer.PropertyFiles();
                                                    picture.FileName = UrlItem;// itemimg.ImageFormat.Where(x => x.DimensionCategory == "E").ToList()[0].URL;
                                                    picture.PropertyId = id;
                                                    picturelist.Add(picture);
                                                    BLayer.Property.GDSSaveImageurl(id, picture.FileName);
                                                    nodes++;
                                                    if (nodes == PictureCount) break;
                                                }
                                            }
                                            if (nodes == PictureCount) break;
                                        }

                                    }
                                    if (nodes == PictureCount) break;
                                }
                            }
                            if (nodes == PictureCount) break;
                        }
                    }

                }
                #endregion


                #region GUESTROOMIMAGES
                if (nodeguestImages != null)
                {
                    XmlNodeList listGuestRoomImages = nodeguestImages.SelectNodes("//si:MultimediaDescriptions/si:MultimediaDescription", xmlnsManager);
                    if (listGuestRoomImages != null)
                    {
                        int PictureCount = 0;
                        int nodes = 0;
                        foreach (XmlNode item in listGuestRoomImages)
                        {
                            var subItem = item.SelectNodes("//si:ImageItems", xmlnsManager);
                            if (subItem != null)
                            {
                                foreach (XmlNode sItem in subItem)
                                {
                                    IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDateTime(r.Attributes["LastModifyDateTime"].Value));
                                    // IEnumerable<XmlNode> subInnerItem = sItem.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager).Cast<XmlNode>().OrderByDescending(r => Convert.ToDecimal(r.Attributes["LastModifyDateTime"].Value));
                                    if (subInnerItem != null)
                                    {
                                        foreach (XmlNode sItem1 in subInnerItem)
                                        {
                                            var subUrlItem = sItem1.SelectNodes("//si:ImageItem/si:ImageFormat[@DimensionCategory='E']", xmlnsManager);
                                            if (subUrlItem != null)
                                            {
                                                PictureCount = subInnerItem.Count();
                                                foreach (XmlNode UItem in subUrlItem)
                                                {
                                                    var UrlItem = UItem.ChildNodes[0].InnerText;  //url
                                                    CLayer.PropertyFiles picture = new CLayer.PropertyFiles();
                                                    picture.FileName = UrlItem;// itemimg.ImageFormat.Where(x => x.DimensionCategory == "E").ToList()[0].URL;
                                                    picture.PropertyId = id;
                                                    picturelist.Add(picture);
                                                    nodes++;
                                                    if (nodes == PictureCount) break;
                                                }
                                            }
                                            if (nodes == PictureCount) break;
                                        }

                                    }
                                    if (nodes == PictureCount) break;
                                }
                            }
                            if (nodes == PictureCount) break;
                        }
                    }
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogHandler.AddLog(ex.Message);
            }
            return picturelist;

        }

    }
}