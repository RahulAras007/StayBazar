using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;
using System.Net.Mail;
using Newtonsoft.Json;

namespace StayBazar.Controllers
{
    public class CommonController : Controller
    {

        //#region CommonFunctions
        //private void Save(Models.UserDocumentsModel data)
        //{
        //    #region Bank Account details for Supplier
        //    if (data.UserType == (int)CLayer.Role.Roles.Supplier)
        //    {
        //        string b2bname = BLayer.B2BUser.Getb2bname(data.UserId);
        //        if (data.BankName != "")
        //        {
        //            CLayer.BankAccount account = new CLayer.BankAccount()
        //            {
        //                BankAccountId = 0,
        //                UserId = data.UserId,
        //                AccountName = b2bname,// data.Name,//Business name b2b                       
        //                AccountNumber = data.AccountNumber,
        //                BankName = data.BankName,
        //                BranchAddress = data.BranchAddress,
        //                RTGSNumber = data.RTGSNumber,
        //                MICRCode = data.MICRCode
        //            };
        //            BLayer.BankAccount.Save(account);
        //        }
        //    }
        //    #endregion

        //    #region File Attatchments
        //    if (data.ServiceTaxReg != null)
        //    {
        //        CLayer.UserFiles servicetax = new CLayer.UserFiles()
        //        {
        //            UserId = data.UserId,
        //            FileId = 0,
        //            FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.ServiceTaxReg.FileName),
        //            Document = (int)CLayer.UserFiles.Documents.ServiceTaxRegNo
        //        };
        //        BLayer.UserFiles.Save(servicetax);
        //        SaveDocument(data.ServiceTaxReg, data.UserId, servicetax.FileName);
        //    }
        //    if (data.VATReg != null)
        //    {
        //        CLayer.UserFiles vat = new CLayer.UserFiles()
        //        {
        //            UserId = data.UserId,
        //            FileId = 0,
        //            FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.VATReg.FileName),
        //            Document = (int)CLayer.UserFiles.Documents.VATRegNo
        //        };
        //        BLayer.UserFiles.Save(vat);
        //        SaveDocument(data.VATReg, data.UserId, vat.FileName);
        //    }
        //    if (data.BusinessRegistrationCertificate != null)
        //    {
        //        CLayer.UserFiles brc = new CLayer.UserFiles()
        //        {
        //            UserId = data.UserId,
        //            FileId = 0,
        //            FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.BusinessRegistrationCertificate.FileName),
        //            Document = (int)CLayer.UserFiles.Documents.BusinessRegistrationCertificate
        //        };
        //        BLayer.UserFiles.Save(brc);
        //        SaveDocument(data.BusinessRegistrationCertificate, data.UserId, brc.FileName);
        //    }
        //    if (data.PANCard != null)
        //    {
        //        CLayer.UserFiles pan = new CLayer.UserFiles()
        //        {
        //            UserId = data.UserId,
        //            FileId = 0,
        //            FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.PANCard.FileName),
        //            Document = (int)CLayer.UserFiles.Documents.PANCard
        //        };
        //        BLayer.UserFiles.Save(pan);
        //        SaveDocument(data.PANCard, data.UserId, pan.FileName);
        //    }
        //    if (data.CopyOfCheque != null)
        //    {
        //        CLayer.UserFiles cc = new CLayer.UserFiles()
        //        {
        //            UserId = data.UserId,
        //            FileId = 0,
        //            FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CopyOfCheque.FileName),
        //            Document = (int)CLayer.UserFiles.Documents.CopyOfCheque
        //        };

        //        //long i = BLayer.UserFiles.isCheck(data.UserId, cc.Document);
        //        //if(i>0)
        //        //{
        //        //    try
        //        //    {
        //        //       // if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        //        //    }
        //        //    catch
        //        //    {

        //        //    }
        //        //}
        //        BLayer.UserFiles.Save(cc);
        //        SaveDocument(data.CopyOfCheque, data.UserId, cc.FileName);
        //    }


        //    #endregion
        //}
        //private void SaveDocument(HttpPostedFileBase file, long UserId, string fname)
        //{
        //    var fileName = fname;
        //    if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/")))
        //    {
        //        System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/"));
        //    }
        //    var path = System.IO.Path.Combine(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/"), fileName);

        //    //try
        //    //{              
        //    //    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        //    //}
        //    //catch
        //    //{

        //    //}
        //    file.SaveAs(path);
        //    ModelState.Clear();
        //}

        //#endregion       


        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public string GetState(int id)// id == CountryId
        {

            List<CLayer.State> states = BLayer.State.GetOnCountry(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            foreach (CLayer.State st in states)
            {
                sb.Append("<option value='" + st.StateId + "'>" + st.Name + "</option>");
            }
            return sb.ToString();
        }

        //Done by rahul
        public string GetCorporateUserName(int id)
        {
            List<CLayer.B2BUser> states = BLayer.B2BUser.GetOnCorporateUserList(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            foreach(CLayer.B2BUser st in states)
            {
                sb.Append("<option value='" + st.UserId + "'>" + st.FirstName + "</option>");
            }
            return sb.ToString();
        }

        //----


        [AllowAnonymous]
        public string GetCity(int id)// id == StateId
        {
            List<CLayer.City> cities = BLayer.City.GetOnState(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            sb.Append("<option value='0' selected >Select</option>");
            foreach (CLayer.City st in cities)
            {
                sb.Append("<option value='" + st.CityId + "'>" + st.Name + "</option>");
            }
            sb.Append("<option value='-1'>Other</option>");
            return sb.ToString();
        }

        [AllowAnonymous]
        public string GetSBEntity(int id)// id == StateId
        {
            List<CLayer.OfflineBooking> SBEntity = BLayer.OfflineBooking.GetProbSBEntity(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("");
            //sb.Append("<option value='0' selected >Select</option>");
            foreach (CLayer.OfflineBooking st in SBEntity)
            {
                sb.Append("<option value='" + st.SBEntityEntityId + "'>" + st.SBEntityName + "</option>"); 
            }
            //sb.Append("<option value='-1'>Other</option>");
            return sb.ToString();
        }

        [AllowAnonymous]
        public string GetCityByName(string  name)// id == StateId
        {
            List<CLayer.City> cities = BLayer.City.GetCityByname(name);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            var cityid = cities[0].CityId;
            var stateid = cities[0].StateId;
            var state = BLayer.State.Get(stateid);
            var countryid = state.CountryId;
           var city= GetCity(cities[0].StateId);

            sb.Append(countryid);
            sb.Append(",");
            sb.Append(stateid);
            sb.Append(",");
            sb.Append(cityid);
            
            //sb.Append("");
            //sb.Append("<option value='0' selected >Select</option>");
            //foreach (CLayer.City st in cities)
            //{
            //    sb.Append("<option value='" + st.CityId + "'>" + st.Name + "</option>");
            //}
            //sb.Append("<option value='-1'>Other</option>");

            return sb.ToString();
        }


        [AllowAnonymous]
        public string GetCityForcustomer(int id)// id == StateId
        {
            List<CLayer.City> cities = BLayer.City.GetOnState(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            sb.Append("<option value='0' selected >Select</option>");
            foreach (CLayer.City st in cities)
            {
                sb.Append("<option value='" + st.CityId + "'>" + st.Name + "</option>");
            }
            //sb.Append("<option value='-1'>Other</option>");
            return sb.ToString();
        }

        [AllowAnonymous]
        public string GetAcctypebypropertyid(int id)
        {
            List<CLayer.Accommodation> cities = BLayer.AccommodationType.GetBypropertyid(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            foreach (CLayer.Accommodation st in cities)
            {
                sb.Append("<option id='" + st.AccommodationId + " '   value='" + st.AccommodationTypeId + "'>" + st.AccommodationType + "</option>");
            }
            sb.Append("<option value='-1'>Other</option>");
            return sb.ToString();
        }

        [AllowAnonymous]
        public string Getstaytypebypropertyid(int id)
        {
            List<CLayer.StayCategory> cities = BLayer.StayCategory.GetBypropertyid(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            foreach (CLayer.StayCategory st in cities)
            {
                sb.Append("<option  value='" + st.CategoryId + "'>" + st.Title + "</option>");
            }

            return sb.ToString();
        }
        [AllowAnonymous]
        public string GetCityForSupplierAutocomplete(int id)// id == StateId
        {
            List<CLayer.City> cities = BLayer.City.GetOnState(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            foreach (CLayer.City st in cities)
            {
                sb.Append("<option value='" + st.CityId + "'>" + st.Name + "</option>");
            }
            return sb.ToString();
        }

        [AllowAnonymous]
        public string GetCityNon(int id)// id == StateId
        {
            List<CLayer.City> cities = BLayer.City.GetOnState(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            foreach (CLayer.City st in cities)
            {
                sb.Append("<option value='" + st.CityId + "'>" + st.Name + "</option>");
            }
            sb.Append("<option value='-1'>None</option>");
            return sb.ToString();
        }
        [AllowAnonymous]
        public ActionResult GetDestination(string term)
        {
            var dummy = new List<object>();
            try
            {
             //   return Json(BLayer.Utility.GetAutoList(term), JsonRequestBehavior.AllowGet);
             //   return Json(BLayer.Utility.GetAutoListGDS(term), JsonRequestBehavior.AllowGet);
                return Json(BLayer.Utility.GetAutoListGDSAll(term), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                dummy.Add(new { value = "", label = "", desc = "" });
            }
            return Json(dummy, JsonRequestBehavior.AllowGet);
        }



        [AllowAnonymous]
        public ActionResult GetAccommodation(string term)
        {
            var dummy = new List<object>();
            try
            {
                return Json(BLayer.Utility.GetAutoListforaccommn(term), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                dummy.Add(new { value = "" });
            }
            return Json(dummy, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public string  GetHotels(int id)
        {
            List<CLayer.Property> Hotels = BLayer.Property.PropertyGetOnCity(id);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("");
            foreach (CLayer.Property  st in Hotels)
            {
                sb.Append("<option value='" + st.PropertyId  + "'>" + st.Title  + "</option>");
            }
            return sb.ToString();
        }

        [AllowAnonymous]
        public ActionResult GetAccommodationForOffline(string term)
        {
            var dummy = new List<object>();
            try
            {
                return Json(BLayer.Utility.GetAutoListforaccommnForOffline(term), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                dummy.Add(new { value = "" });
            }
            return Json(dummy, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public ActionResult GetLocation(string term)
        {
            var dummy = new List<object>();
            try
            {
                return Json(BLayer.Utility.GetLocations(term), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                dummy.Add(new { value = "", label = "", desc = "" });
            }
            return Json(dummy, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public string IsValid()
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                {
                    if (Session[CLayer.ObjectStatus.GUEST_ID_SESSION] == null)
                    {
                        return "false";
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return "true";
        }
        [AllowAnonymous]
        public string IsValidUser()
        {
            try
            {
                if (User.Identity.IsAuthenticated) return "true";

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return "false";
        }
        [AllowAnonymous]
        public string GetLocationFilter(string term)
        {
            System.Text.StringBuilder result = new System.Text.StringBuilder();
            try
            {
                result.Append("<option value=\"0\" selected=\"selected\" >Select</option>");
                List<string> r = BLayer.Utility.GetLocationFilter(term);
                foreach (string prp in r)
                {
                    result.Append("<option value=\"");
                    result.Append(prp);
                    result.Append("\">");
                    result.Append(prp);
                    result.Append("</option>");
                }

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return result.ToString();
        }

        [AllowAnonymous]
        public string DynamicRateSave()
        {
            try
            {
                // update rates for all  property

                List<CLayer.Property> pr = BLayer.Property.GetAllPropertiesForratesave();

                if (pr.Count > 0)
                {
                    foreach (CLayer.Property ProId in pr)
                    {
                        //get all accomodation by propertyid

                        List<CLayer.Accommodation> acc = BLayer.Accommodation.GetAllAccByPropertyid(ProId.PropertyId);
                        if (acc.Count > 0)
                        {
                            foreach (CLayer.Accommodation accid in acc)
                            {
                                //get all rates by accomodation

                                List<CLayer.Rates> allrts = BLayer.Rate.GetAllRatesByAccId(accid.AccommodationId);

                                if (allrts.Count > 0)
                                {
                                    if (allrts != null)
                                    {
                                        foreach (CLayer.Rates ratedt in allrts)
                                        {
                                            CLayer.Rates dt = new CLayer.Rates();
                                            dt.AccommodationId = accid.AccommodationId;
                                            dt.RateId = ratedt.RateId;
                                            dt.RateFor = ratedt.RateFor;
                                            dt.DailyRate = ratedt.DailyRate;
                                            dt.WeeklyRate = ratedt.WeeklyRate;
                                            dt.MonthlyRate = ratedt.MonthlyRate;
                                            dt.LongTermRate = ratedt.LongTermRate;
                                            dt.GuestRate = ratedt.GuestRate;
                                            dt.StartDate = ratedt.StartDate;
                                            dt.EndDate = ratedt.EndDate;
                                            dt.UpdatedBy = ratedt.UpdatedBy;
                                            BLayer.Rate.DynamicSave(dt);
                                        }
                                    }

                                }
                            }
                        }

                    }
                }
                return "true";

            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return "false";
        }


        //[HttpPost]
        //public ActionResult UserDocumentdataSave(Models.UserDocumentsModel data)
        //{
        //    string b2bname = BLayer.B2BUser.Getb2bname(data.UserId);
        //    CLayer.B2B b2b = new CLayer.B2B()
        //    {
        //        Name = b2bname,
        //        B2BId = data.UserId,
        //        UserType = data.UserType,
        //        ServiceTaxRegNo = data.ServiceTaxRegNo,
        //        PANNo = data.PANNo,
        //        VATRegNo = data.VATRegNo
        //    };
        //    data.B2BId = BLayer.B2B.Savedoc(b2b);



        //    if (data.UserType == (int)CLayer.Role.Roles.Agent)
        //    {
        //        int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
        //        string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
        //        int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
        //        maxContentLength = maxContentLength * maxFileSize;
        //        //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
        //        string[] allowedFileExtensions = extensions.Split(',');
        //        if (data.ServiceTaxReg != null || data.BusinessRegistrationCertificate != null ||
        //            data.PANCard != null || data.CopyOfCheque != null ||
        //            data.ServiceTaxReg.ContentLength > 0 && data.BusinessRegistrationCertificate.ContentLength > 0 ||
        //            data.PANCard.ContentLength > 0 || data.CopyOfCheque.ContentLength > 0)
        //        {
        //            //if (!allowedFileExtensions.Contains(data.ServiceTaxReg.FileName.Substring(data.ServiceTaxReg.FileName.LastIndexOf('.'))) ||
        //            //    !allowedFileExtensions.Contains(data.BusinessRegistrationCertificate.FileName.Substring(data.BusinessRegistrationCertificate.FileName.LastIndexOf('.'))) ||
        //            //    !allowedFileExtensions.Contains(data.PANCard.FileName.Substring(data.PANCard.FileName.LastIndexOf('.'))) ||
        //            //    !allowedFileExtensions.Contains(data.CopyOfCheque.FileName.Substring(data.CopyOfCheque.FileName.LastIndexOf('.'))))
        //            //{
        //            //    ViewBag.Message = "Invalid document type. Please use these file types: " + string.Join(", ", allowedFileExtensions);
        //            //    return View("TravelAgent", data);
        //            //}
        //            // if (data.ServiceTaxReg.ContentLength > maxContentLength ||
        //            //    data.BusinessRegistrationCertificate.ContentLength > maxContentLength ||
        //            //    data.PANCard.ContentLength > maxContentLength ||
        //            //    data.CopyOfCheque.ContentLength > maxContentLength)
        //            //{
        //            //    ViewBag.Message = "Your file is too large, maximum allowed size is: 1 MB";
        //            //}
        //            //else
        //            //{
        //            data.UserType = (int)CLayer.Role.Roles.Agent;
        //            Save(data);
        //            //}
        //        }

        //        return RedirectToAction("Account", "Profile");
        //    }
        //    else if (data.UserType == (int)CLayer.Role.Roles.Supplier)
        //    {
        //        int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
        //        string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
        //        int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
        //        maxContentLength = maxContentLength * maxFileSize;
        //        //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
        //        string[] allowedFileExtensions = extensions.Split(',');
        //        if (data.ServiceTaxReg != null && data.VATReg != null && data.ServiceTaxReg.ContentLength > 0 && data.VATReg.ContentLength > 0)
        //        {
        //            if (!allowedFileExtensions.Contains(data.ServiceTaxReg.FileName.Substring(data.ServiceTaxReg.FileName.LastIndexOf('.'))) ||
        //                !allowedFileExtensions.Contains(data.VATReg.FileName.Substring(data.VATReg.FileName.LastIndexOf('.'))))
        //            {
        //                ViewBag.Message = "Please use files of type: " + string.Join(", ", allowedFileExtensions);
        //                return View("Supplier", data);
        //            }
        //            else if (data.ServiceTaxReg.ContentLength > maxContentLength || data.VATReg.ContentLength > maxContentLength)
        //            {
        //                ViewBag.Message = "Your file is too large, maximum allowed size is: 1 MB";
        //                return View("Supplier", data);
        //            }
        //            else
        //            {
        //                data.UserType = (int)CLayer.Role.Roles.Supplier;
        //                Save(data);
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Message = "The files are not selected";
        //        }

        //        return RedirectToAction("Account", "Profile");
        //    }
        //    else if (data.UserType == (int)CLayer.Role.Roles.Corporate)
        //    {
        //        int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
        //        string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
        //        int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
        //        maxContentLength = maxContentLength * maxFileSize;
        //        //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
        //        string[] allowedFileExtensions = extensions.Split(',');

        //        if (data.ServiceTaxReg != null)
        //        {
        //            if (!allowedFileExtensions.Contains(data.ServiceTaxReg.FileName.Substring(data.ServiceTaxReg.FileName.LastIndexOf('.'))))
        //            {
        //                ViewBag.Message = "Please use files of type: " + string.Join(", ", allowedFileExtensions);
        //                return View("Corporate", data);
        //            }
        //            else if (data.ServiceTaxReg.ContentLength > maxContentLength)
        //            {
        //                ViewBag.Message = "Your file is too large, maximum allowed size is: 1 MB";
        //                return View("Corporate", data);
        //            }
        //            else
        //            {
        //                data.UserType = (int)CLayer.Role.Roles.Corporate;
        //                Save(data);
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Message = "The files are not selected";
        //        }
        //        return RedirectToAction("Account", "Profile");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}


        [AllowAnonymous]
        public async Task<string> SendQuery(string name, string email, string phone, string body, long PropertyId)
        {
            try
            {
                CLayer.Property data = new CLayer.Property();
                if (PropertyId > 0)
                {
                    data = BLayer.Property.Get(PropertyId);
                }
                MailMessage mm = new MailMessage();
                mm.To.Add(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                string emailids = ConfigurationManager.AppSettings.Get("EnquiryCC");
                if (emailids != "")
                {
                    string[] emails = emailids.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        mm.CC.Add(emails[i]);
                    }
                }
                mm.From = new MailAddress(ConfigurationManager.AppSettings.Get("CustomerCareMail"));
                mm.ReplyToList.Add(email);
                mm.Subject = "Enquiry";
                mm.Body = "<strong>Name:</strong> " + name + "<br /><strong>Email Id:</strong> " + email + "<br /><strong>Phone No:</strong> " + phone + "<br /><strong>Property:</strong> " + "<a href='https://www.staybazar.com/property/Index/" + PropertyId + "' >" + data.Title + "</a>" + "," + data.Location + "<br /><strong>Comment:</strong><br/> " + body;
                mm.IsBodyHtml = true;
                Common.Mailer mlr = new Common.Mailer();
                await mlr.SendMailAsyncWithoutFields(mm);

                // BLayer.Query.Savefeedback(email, feedback);
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return "false";
            }
            return "true";
        }
        [AllowAnonymous]
        public long ExistEmail(string email)
        {
            long user = BLayer.User.GetUserId(email);
            return user;
        }
        protected string GetIPAddress()
        {
            var context = System.Web.HttpContext.Current;
            string ip = String.Empty;

            if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                ip = context.Request.UserHostAddress;
            return ip;
        }

        [AllowAnonymous]
        public string CheckBookAvail()
        {
            //hotelavailability
            string Book_Hotel_Avail = StayBazar.Inventory.MaximojoBook.Maximojo_Hotel_Availabilty();

            List<CLayer.BookingExternalInventory> DtBookReq = BLayer.BookingExternalInventory.GetExternalInventoryReqDetailsByBookingId(1464);

              foreach (CLayer.BookingExternalInventory reqbook in DtBookReq)
              {
                  var Book_Cancelobj1 = new StayBazar.Inventory.Models.MaxBooking_Cancel.RootObject
                  {
                      hotel_id = reqbook.hotel_id,
                      reservation_id = reqbook.reservation_id
                  };

                  string ResponseCancel1 = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Cancel(Book_Cancelobj1);

                  StayBazar.Inventory.Models.MaxBooking_Cancel_Response.RootObject Bookingcanceldetails = JsonConvert.DeserializeObject<StayBazar.Inventory.Models.MaxBooking_Cancel_Response.RootObject>(ResponseCancel1);

                  //Saving Cancel Details of external booking request
                  CLayer.BookingExternalInventory bookcandt = new CLayer.BookingExternalInventory();

                  bookcandt.BookingExtInvReqId = reqbook.BookingExtInvReqId;
                  bookcandt.BookingId = reqbook.BookingId;
                  bookcandt.reservation_id = reqbook.reservation_id;

                  if (Bookingcanceldetails.status == "Success")
                  {
                      bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.Success;
                  }
                  else if (Bookingcanceldetails.status == "AlreadyCancelled")
                  {
                      bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.AlreadyCancelled;
                  }
                  else if (Bookingcanceldetails.status == "CannotBeCancelled")
                  {
                      bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.CannotBeCancelled;
                  }
                  else if (Bookingcanceldetails.status == "UnknownReference")
                  {
                      bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.UnknownReference;
                  }
                  else if (Bookingcanceldetails.status == "Error")
                  {
                      bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.Error;
                  }
                  else
                  {
                      bookcandt.Cancellation_Status = (int)CLayer.BookingExternalInventory.CancellationStatus.none;
                  }
                  //UPDATE BOOKING STATUS
                  int CacelSts = bookcandt.Cancellation_Status;
                  BLayer.BookingExternalInventory.UpdateCancellationStatus(reqbook.BookingExtInvReqId, CacelSts);
                  bookcandt.Cancellation_Number = Bookingcanceldetails.cancellation_number;

                  bookcandt.Cancelled_Date = DateTime.Now.ToString();
                  bookcandt.Cancellation_Response = ResponseCancel1;
                  BLayer.BookingExternalInventory.SaveBookingCancelResponse(bookcandt);

              }


            string ipA = GetIPAddress();
            //List<StayBazar.Inventory.BookingAvailibility_Response.HotelroomsDetails> HotelRoomList1 = StayBazar.Inventory.MaximojoBook.BookingAvailDeserializeJsonResponse();

            //data for booking submit with Payment details

            StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.RootObject dd = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.RootObject();
            StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Room room1 = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Room { party = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Party { adults = 2 }, traveler_first_name = "111", traveler_last_name = "30" };
            StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Room room2 = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Room { party = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Party { adults = 2 }, traveler_first_name = "111", traveler_last_name = "30" };
            StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Room room3 = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Room { party = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Party { adults = 2 }, traveler_first_name = "111", traveler_last_name = "30" };
            List<StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Room> oList = new List<StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Room>();
            oList.Add(room1);
            oList.Add(room2);
            oList.Add(room3);


            var Book_Payobj = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.RootObject
            {
                checkin_date = "dd",
                checkout_date = "dd",
                hotel_id = "",
                reference_id = "",
                ip_address = "",
                customer = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.Customer
                {
                    first_name = "ddd",
                    last_name = "ff",
                    email = "ff",
                    phone_number = "ff",
                    country = "ff"
                },
                payment_method = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.PaymentMethod
                {
                    billing_address = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.BillingAddress
                    {
                        city = "",
                        country = "",
                        postal_code = "",
                        state = "",
                        street = ""
                    },
                    card_number = "",
                    card_type = "",
                    cardholder_name = "",
                    cvv = "",
                    expiration_month = "",
                    expiration_year = ""
                },

                final_price_at_booking = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.FinalPriceAtBooking
                {
                    amount = 0,
                    currency = "in"
                },
                final_price_at_checkout = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.FinalPriceAtCheckout
                {
                    amount = 0,
                    currency = "in"
                },
                partner_data = new StayBazar.Inventory.Models.MaxBooking_Submit_PayDetails.PartnerData
                {
                    domainId = "",
                    hotelId = "",
                    promotionId = "",
                    ratePlanId = "",
                    roomTypeId = ""
                },
                rooms = oList
            };

            //data for booking submit without Payment details


            StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.RootObject dd1 = new Inventory.Models.MaxBooking_Submit_WithoutPaydt.RootObject();
            StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room room4 = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room { party = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party { adults = 2 }, traveler_first_name = "111", traveler_last_name = "30" };
            StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room room5 = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room { party = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party { adults = 2 }, traveler_first_name = "111", traveler_last_name = "30" };
            StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room room6 = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room { party = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Party { adults = 2 }, traveler_first_name = "111", traveler_last_name = "30" };
            List<StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room> oList1 = new List<StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Room>();
            oList1.Add(room4);
            oList1.Add(room5);
            oList1.Add(room6);

            var Book_WihoutPayobj = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.RootObject
            {
                checkin_date = "dd",
                checkout_date = "dd",
                hotel_id = "",
                reference_id = "",
                ip_address = "",
                customer = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.Customer
                {
                    first_name = "ddd",
                    last_name = "ff",
                    email = "ff",
                    phone_number = "ff",
                    country = "ff"
                },
                final_price_at_booking = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.FinalPriceAtBooking
                {
                    amount = 0,
                    currency = "in"
                },
                final_price_at_checkout = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.FinalPriceAtCheckout
                {
                    amount = 0,
                    currency = "in"
                },
                partner_data = new StayBazar.Inventory.Models.MaxBooking_Submit_WithoutPaydt.PartnerData
                {
                    DomainId = "",
                    HotelId = "",
                    PromotionId = "",
                    RatePlanId = "",
                    RoomId = ""
                },
                rooms = oList1
            };


            //data for booking cancel
            var Book_Cancelobj = new StayBazar.Inventory.Models.MaxBooking_Cancel.RootObject
         {
             hotel_id = "",
             reservation_id = ""
         };



            //hotelavailability
            string Book_Hotel_Avail1 = StayBazar.Inventory.MaximojoBook.Maximojo_Hotel_Availabilty();


            //booking available
           // List<StayBazar.Inventory.BookingAvailibility_Response.HotelroomsDetails> HotelRoomList = StayBazar.Inventory.MaximojoBook.BookingAvailDeserializeJsonResponse();


            //bookingsubmitWithPay
            string ResponseBookSubPay = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Submit_paypaldetails(Book_Payobj);


            //bookingsubmitWithoutPay
            string ResponseBookSub = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Submit_Without_paypaldetails(Book_WihoutPayobj);


            //BookingCancel
            string ResponseCancel = StayBazar.Inventory.MaximojoBook.MaximojoBook_Booking_Cancel(Book_Cancelobj);


            //booking verify 
            string Hotel_Id = "";
            string reference_id = "";
            string reservation_id = "";

            string Booking_VerifyResponse = StayBazar.Inventory.MaximojoBook.Maximojo_Booking_Verify(Hotel_Id, reference_id,reservation_id);

            string kk = "";
            return kk;
        }


        //mail option -  from other website action
      

    }
}