using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using StayBazar.Models;
using Microsoft.AspNet.Identity;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace StayBazar.Controllers
{
    public class OfflinePaymentController : Controller
    {
        public const string PAYMENT_FAILED_LINK = "PaymentError";
        // GET: OfflinePayment
        #region Custom methods

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
        #endregion
        [AllowAnonymous]
        public ActionResult Index()
        {
            long UserId = GetUserId();
            CLayer.Address byAddress = BLayer.Address.GetAddressOnUserId(UserId);
            CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(UserId);
            OfflinePaymentModel model = new OfflinePaymentModel();
            if (byAddress != null && adrs != null)
            {
                model.Name = byAddress.Firstname;
                model.Email = byAddress.Email;
                model.Mobile = adrs.Mobile;
                model.CityId = adrs.CityId;
                model.City = adrs.City;
                model.State = adrs.State;
                model.CountryId = adrs.CountryId;
                model.ZipCode = adrs.ZipCode;
                model.Address = adrs.AddressText;
                if (model.State > 0)
                {
                    List<CLayer.City> cities = null;
                    cities = BLayer.City.GetOnState(model.State);
                    cities.Add(new CLayer.City() { CityId = -1, Name = "Other" });
                    model.CityList = new SelectList(cities, "CityId", "Name");
                }
                model.LoadPlaces();

                return View(model);
            }
            else
            {
                return View(model);
            }
        }
        [AllowAnonymous]
        public ActionResult SaveOfflinePay(Models.OfflinePaymentModel data)
        {
            try
            {
                long UserId = GetUserId();
                CLayer.OfflinePayment dt = new CLayer.OfflinePayment();
                dt.Name = data.Name;
                dt.Amount = data.Amount;
                dt.ReferenceNumber = data.ReferenceNumber;
                dt.Message = data.Message;
                dt.UserId = UserId;
                dt.Address = data.Address;
                dt.CountryId = data.CountryId;
                dt.StateId = data.State;
                dt.CityId = data.CityId;
                if (data.City != null && data.City != "")
                {
                    dt.City = data.City;
                }
                if (data.CityId > 0)
                {
                    dt.City = BLayer.City.Get(data.CityId).Name;
                }
                dt.Email = data.Email;
                dt.Mobile = data.Mobile;
                dt.ZipCode = data.ZipCode;

                dt.Gatewaytype = (int)CLayer.ObjectStatus.Gateway.EBS;
                CLayer.Role.Roles rle = BLayer.User.GetRole(UserId);
                long OfflinePaymentId = BLayer.OfflinePayment.SaveInitialPaymentData(dt);
                string PaymentRefNo = " ";
                long OffPayId = BLayer.OfflinePayment.SetPaymentRefNo(OfflinePaymentId, rle, PaymentRefNo);
                return RedirectToAction("Index", "OfflinePaymentProcess", new { OfflinePaymentId = OfflinePaymentId });
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return RedirectToAction("Index", "Home");
            }

        }


        [AllowAnonymous]
        public RedirectResult SaveOfflinePaybyPaypal(Models.OfflinePaymentModel data)
        {
            string errorPage = System.Configuration.ConfigurationManager.AppSettings.Get(PAYMENT_FAILED_LINK);
            try
            {
                long UserId = GetUserId();
                CLayer.OfflinePayment dt = new CLayer.OfflinePayment();
                dt.Name = data.Name;
                dt.Amount = data.Amount;
                dt.ReferenceNumber = data.ReferenceNumber;
                dt.Message = data.Message;
                dt.UserId = UserId;
                dt.Address = data.Address;
                dt.CountryId = data.CountryId;
                dt.StateId = data.State;
                dt.CityId = data.CityId;
                if (data.City != null && data.City != "")
                {
                    dt.City = data.City;
                }
                if (data.CityId > 0)
                {
                    dt.City = BLayer.City.Get(data.CityId).Name;
                }
                dt.Email = data.Email;
                dt.Mobile = data.Mobile;
                dt.ZipCode = data.ZipCode;
                dt.Gatewaytype = (int)CLayer.ObjectStatus.Gateway.PayPal;
                CLayer.Role.Roles rle = BLayer.User.GetRole(UserId);
                long OfflinePaymentId = BLayer.OfflinePayment.SaveInitialPaymentData(dt);
                string PaymentRefNo = " ";

                string token = "";
             
                try
                {         
                    //load paypal url from settings
                    string url = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_TOKEN_URL);
                    WebRequest rqst = HttpWebRequest.Create(url);

                    rqst.Method = "POST";
                    string user, pwd, signature, returnurl, cancelurl;
                    user = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_USER);
                    pwd = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_PWD);
                    signature = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_SIGNAUTRE);
                    returnurl = BLayer.Settings.GetValue(CLayer.Settings.OFFLINE_PAYPAL_RETURN_URL);
                    cancelurl = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_CANCELURL);

                    CLayer.Currency cur = BLayer.Currency.Get("USD");
                    if (cur == null)
                    {
                        throw new Exception("Cannot find USD conversion rate for Paypal booking");
                    }
                    decimal amount = data.Amount;
                    amount = Math.Round(amount * cur.ConversionRate, 2, MidpointRounding.AwayFromZero);

                    string ProBookDes = data.Message;
                    string postdata = "METHOD=SetExpressCheckout&VERSION=109.0";
                    postdata = postdata + "&USER=" + user + "&PWD=" + pwd + "&SIGNATURE=" + signature;
                    postdata = postdata + "&PAYMENTREQUEST_0_AMT=" + amount.ToString("F2") + "&PAYMENTREQUEST_0_CURRENCYCODE=USD" + "&PAYMENTREQUEST_0_DESC=" + ProBookDes;
                    postdata = postdata + "&RETURNURL=" + Server.UrlEncode(returnurl);
                    postdata = postdata + "&CANCELURL=" + Server.UrlEncode(cancelurl);
                    postdata = postdata + "&PAYMENTREQUEST_0_PAYMENTACTION=Sale";

                    if (!String.IsNullOrEmpty(postdata))
                    {
                        rqst.ContentType = "application/x-www-form-urlencoded";
                        byte[] byteData = UTF8Encoding.UTF8.GetBytes(postdata);
                        rqst.ContentLength = byteData.Length;
                        using (Stream postStream = rqst.GetRequestStream())
                        {
                            postStream.Write(byteData, 0, byteData.Length);
                            postStream.Close();
                        }
                    }
                    ((HttpWebRequest)rqst).KeepAlive = false;
                    StreamReader rsps = new StreamReader(rqst.GetResponse().GetResponseStream());
                    string strRsps = rsps.ReadToEnd();
                    Debug.WriteLine(strRsps);
                    token = PaymentController.GetToken(strRsps);
                    if (token == "")
                    {
                        //(int)CLayer.ObjectStatus.OfflinePyamentStatus.Processing
                        BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.failed, OfflinePaymentId);
                        return Redirect(errorPage);
                    }
                }
                catch (Exception ex)
                {
                    Common.LogHandler.HandleError(ex);
                    BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.failed, OfflinePaymentId);
                    return Redirect(errorPage);
                }

                string red = BLayer.Settings.GetValue(CLayer.Settings.PAYPAL_REQUEST_URL) + token;

                token = Server.UrlDecode(token);
                //save to database
                PaymentRefNo = token;
                long OffPayId = BLayer.OfflinePayment.SetPaymentRefNo(OfflinePaymentId, rle, PaymentRefNo);
                BLayer.OfflinePayment.SetStatus((int)CLayer.ObjectStatus.OfflinePyamentStatus.Processing, OfflinePaymentId);
                return Redirect(red);


            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect(errorPage);
            }

           


        }
        [AllowAnonymous]
        public ActionResult GetDetails(string Email)
        {

            OfflinePaymentModel model = new OfflinePaymentModel();
            long UserId = BLayer.User.GetUserId(Email);
            if (UserId > 0)
            {
                CLayer.Address byAddress = BLayer.Address.GetAddressOnUserId(UserId);
                CLayer.Address adrs = BLayer.Address.GetPrimaryOnUser(UserId);
                if (byAddress != null && adrs != null)
                {
                    model.Name = byAddress.Firstname;
                    model.Email = byAddress.Email;
                    model.Mobile = adrs.Mobile;
                    model.CityId = adrs.CityId;
                    model.State = adrs.State;
                    model.CountryId = adrs.CountryId;
                    model.ZipCode = adrs.ZipCode;
                    model.Address = adrs.AddressText;
                    model.LoadPlaces();
                    return View("_Details", model);
                }
                else
                {
                    return View("_Details", model);
                }
            }
            else
            {
                return View("_Details", model);
            }
        }
        [AllowAnonymous]
        public ActionResult OffPayConfirmationMail(long OfflinePayId)
        {
            try
            {
                //CLayer.OfflinePayment PaymentData = BLayer.OfflinePayment.GetOfflinePaymentDetails(OfflinePayId);
                //return View(PaymentData);
                string CustomerGuid = BLayer.OfflinePayment.GetOfflinePaymentGuid(OfflinePayId);
                if (CustomerGuid == null || CustomerGuid == "" )
                {
                    List<CLayer.OfflinePayment> PaymentData = BLayer.OfflinePayment.GetOfflinePaymentDetails2(OfflinePayId);
                    return View(PaymentData);
                }
                else
                {
                    //CLayer.OfflinePayment PaymentData = null;
              
            
                    List<CLayer.OfflinePayment> PaymentData = BLayer.OfflinePayment.GetOfflinePaymentDetails1(OfflinePayId);

                    //List<CLayer.OfflineBooking> users = BLayer.OfflineBooking.GetAllForPaymentList_Details(data.CustomerName);

                    //PaymentData = new CLayer.OfflinePayment();
                    //{
                    //    OfflineBookingList 
                    //};
                    //PaymentData.OfflineBookingList = details;
                    return View(PaymentData);
                }
                
                //return View(PaymentData);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(new CLayer.OfflinePayment());

        }
    }
}