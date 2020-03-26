﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Configuration;
using System.Drawing;

namespace StayBazar.Areas.Admin.Controllers
{
    [Common.AdminRolePermission(AllowAllRoles = true)]
    public class CustomController : Controller
    {
        #region CommonFunctions
        private void Save(Models.UserDocumentsModel data)
        {
            #region Bank Account details for Supplier
            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                string b2bname = BLayer.B2BUser.Getb2bname(data.UserId);
                if (data.BankName != "")
                {
                    CLayer.BankAccount account = new CLayer.BankAccount()
                    {
                        BankAccountId = data.BankAccountId,
                        UserId = data.UserId,
                        AccountName = b2bname,// data.Name,//Business name b2b                       
                        AccountNumber = data.AccountNumber,
                        BankName = data.BankName,
                        BranchAddress = data.BranchAddress,
                        RTGSNumber = data.RTGSNumber,
                        MICRCode = data.MICRCode
                    };
                    BLayer.BankAccount.Save(account);
                }
            }
            if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
            {
                string b2bname = BLayer.B2BUser.Getb2bname(data.UserId);
                if (data.BankName != "")
                {
                    CLayer.BankAccount account = new CLayer.BankAccount()
                    {
                        BankAccountId = data.BankAccountId,
                        UserId = data.UserId,
                        AccountName = b2bname,// data.Name,//Business name b2b                       
                        AccountNumber = data.AccountNumber,
                        BankName = data.BankName,
                        BranchAddress = data.BranchAddress,
                        RTGSNumber = data.RTGSNumber,
                        MICRCode = data.MICRCode
                    };
                    BLayer.BankAccount.Save(account);
                }
            }
            #endregion
            #region File Attatchments
            if (data.ServiceTaxReg != null && data.ServiceTaxReg.ContentLength > 0)
            {
                CLayer.UserFiles servicetax = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "ServiceTax" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.ServiceTaxReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.ServiceTaxRegNo
                };
                BLayer.UserFiles.Save(servicetax);
                SaveDocument(data.ServiceTaxReg, data.UserId, servicetax.FileName);
            }
            if (data.VATReg != null && data.VATReg.ContentLength > 0)
            {
                CLayer.UserFiles vat = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "VAT" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.VATReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.VATRegNo
                };
                BLayer.UserFiles.Save(vat);
                SaveDocument(data.VATReg, data.UserId, vat.FileName);
            }
            if (data.BusinessRegistrationCertificate != null && data.BusinessRegistrationCertificate.ContentLength > 0)
            {
                CLayer.UserFiles brc = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "BRC" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.BusinessRegistrationCertificate.FileName),
                    Document = (int)CLayer.UserFiles.Documents.BusinessRegistrationCertificate
                };
                BLayer.UserFiles.Save(brc);
                SaveDocument(data.BusinessRegistrationCertificate, data.UserId, brc.FileName);
            }
            if (data.PANCard != null && data.PANCard.ContentLength > 0)
            {
                CLayer.UserFiles pan = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "PAN" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.PANCard.FileName),
                    Document = (int)CLayer.UserFiles.Documents.PANCard
                };
                BLayer.UserFiles.Save(pan);
                SaveDocument(data.PANCard, data.UserId, pan.FileName);
            }
            if(data.CorporateLogo!=null && data.CorporateLogo.ContentLength > 0)
            {
                CLayer.UserFiles corporatelogo = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "CORPORATELOGO" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CorporateLogo.FileName),
                    Document = (int)CLayer.UserFiles.Documents.CorporateLogo
                };
                BLayer.UserFiles.Save(corporatelogo);
                SaveDocument(data.CorporateLogo, data.UserId, corporatelogo.FileName);
            }
            if (data.CopyOfCheque != null && data.CopyOfCheque.ContentLength > 0)
            {
                CLayer.UserFiles cc = new CLayer.UserFiles()
                {
                    UserId = data.UserId,
                    FileId = 0,
                    FileName = "Cheque" + data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CopyOfCheque.FileName),
                    Document = (int)CLayer.UserFiles.Documents.CopyOfCheque
                };

                BLayer.UserFiles.Save(cc);
                SaveDocument(data.CopyOfCheque, data.UserId, cc.FileName);
            }


            #endregion

            //#region File Attatchments
            //if (data.ServiceTaxReg != null)
            //{
            //    CLayer.UserFiles servicetax = new CLayer.UserFiles()
            //    {
            //        UserId = data.UserId,
            //        FileId = 0,
            //        FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.ServiceTaxReg.FileName),
            //        Document = (int)CLayer.UserFiles.Documents.ServiceTaxRegNo
            //    };
            //    BLayer.UserFiles.Save(servicetax);
            //    SaveDocument(data.ServiceTaxReg, data.UserId, servicetax.FileName);
            //}
            //if (data.VATReg != null)
            //{
            //    CLayer.UserFiles vat = new CLayer.UserFiles()
            //    {
            //        UserId = data.UserId,
            //        FileId = 0,
            //        FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.VATReg.FileName),
            //        Document = (int)CLayer.UserFiles.Documents.VATRegNo
            //    };
            //    BLayer.UserFiles.Save(vat);
            //    SaveDocument(data.VATReg, data.UserId, vat.FileName);
            //}
            //if (data.BusinessRegistrationCertificate != null)
            //{
            //    CLayer.UserFiles brc = new CLayer.UserFiles()
            //    {
            //        UserId = data.UserId,
            //        FileId = 0,
            //        FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.BusinessRegistrationCertificate.FileName),
            //        Document = (int)CLayer.UserFiles.Documents.BusinessRegistrationCertificate
            //    };
            //    BLayer.UserFiles.Save(brc);
            //    SaveDocument(data.BusinessRegistrationCertificate, data.UserId, brc.FileName);
            //}
            //if (data.PANCard != null)
            //{
            //    CLayer.UserFiles pan = new CLayer.UserFiles()
            //    {
            //        UserId = data.UserId,
            //        FileId = 0,
            //        FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.PANCard.FileName),
            //        Document = (int)CLayer.UserFiles.Documents.PANCard
            //    };
            //    BLayer.UserFiles.Save(pan);
            //    SaveDocument(data.PANCard, data.UserId, pan.FileName);
            //}
            //if (data.CopyOfCheque != null)
            //{
            //    CLayer.UserFiles cc = new CLayer.UserFiles()
            //    {
            //        UserId = data.UserId,
            //        FileId = 0,
            //        FileName = data.UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CopyOfCheque.FileName),
            //        Document = (int)CLayer.UserFiles.Documents.CopyOfCheque
            //    };

            //    BLayer.UserFiles.Save(cc);
            //    SaveDocument(data.CopyOfCheque, data.UserId,cc.FileName);
            //}


            //#endregion
        }
        private void SaveDocument(HttpPostedFileBase file, long UserId, string fname)
        {
            var fileName = fname;
            var path = System.IO.Path.Combine(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/"), fileName);
            //try
            //{
            //    if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
            //}
            //catch
            //{

            //}


            if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/"));
            }



            file.SaveAs(path);
            ModelState.Clear();
        }

        #endregion
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserDocumentdataSave(Models.UserDocumentsModel data)
        {

            string b2bname = BLayer.B2BUser.Getb2bname(data.UserId);
            CLayer.B2B b2b = new CLayer.B2B()
            {
                Name = b2bname,
                B2BId = data.UserId,
                UserType = data.UserType,
                ServiceTaxRegNo = data.ServiceTaxRegNo,
                PANNo = data.PANNo,
                VATRegNo = data.VATRegNo,
                //ContactDesignation = data.ContactDesignation
            };
            data.B2BId = BLayer.B2B.Savedoc(b2b);
            if (data.UserType == (int)CLayer.Role.Roles.Agent)
            {
                int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                maxContentLength = maxContentLength * maxFileSize;
                //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                string[] allowedFileExtensions = extensions.Split(',');
                if (data.ServiceTaxReg != null || data.BusinessRegistrationCertificate != null ||
                    data.PANCard != null || data.CopyOfCheque != null ||data.CorporateLogo!=null)
                {

                    //data.ServiceTaxReg.ContentLength > 0 || data.BusinessRegistrationCertificate.ContentLength > 0 ||data.PANCard.ContentLength > 0 || data.CopyOfCheque.ContentLength > 0

                    //if (!allowedFileExtensions.Contains(data.ServiceTaxReg.FileName.Substring(data.ServiceTaxReg.FileName.LastIndexOf('.'))) ||
                    //    !allowedFileExtensions.Contains(data.BusinessRegistrationCertificate.FileName.Substring(data.BusinessRegistrationCertificate.FileName.LastIndexOf('.'))) ||
                    //    !allowedFileExtensions.Contains(data.PANCard.FileName.Substring(data.PANCard.FileName.LastIndexOf('.'))) ||
                    //    !allowedFileExtensions.Contains(data.CopyOfCheque.FileName.Substring(data.CopyOfCheque.FileName.LastIndexOf('.'))))
                    //{
                    //    ViewBag.Message = "Invalid document type. Please use these file types: " + string.Join(", ", allowedFileExtensions);
                    //    return View("TravelAgent", data);
                    //}
                    // if (data.ServiceTaxReg.ContentLength > maxContentLength ||
                    //    data.BusinessRegistrationCertificate.ContentLength > maxContentLength ||
                    //    data.PANCard.ContentLength > maxContentLength ||
                    //    data.CopyOfCheque.ContentLength > maxContentLength)
                    //{
                    //    ViewBag.Message = "Your file is too large, maximum allowed size is: 1 MB";
                    //}
                    //else
                    //{
                    data.UserType = (int)CLayer.Role.Roles.Agent;
                    Save(data);
                    //}
                }

                return RedirectToAction("Details", "TravelAgent", new { id = data.UserId });
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                maxContentLength = maxContentLength * maxFileSize;
                //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                string[] allowedFileExtensions = extensions.Split(',');

                if (data.ServiceTaxReg != null || data.VATReg != null)
                {
                    data.UserType = (int)CLayer.Role.Roles.Supplier;
                    Save(data);
                }
                else
                {
                    data.UserType = (int)CLayer.Role.Roles.Affiliate;
                    Save(data);
                    ViewBag.Message = "The files are not selected";
                }

                return RedirectToAction("Details", "Supplier", new { id = data.UserId });
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
            {
                int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                maxContentLength = maxContentLength * maxFileSize;
                //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                string[] allowedFileExtensions = extensions.Split(',');

                if (data.ServiceTaxReg != null || data.VATReg != null)
                {
                    data.UserType = (int)CLayer.Role.Roles.Affiliate;
                    Save(data);
                }
                else
                {
                    data.UserType = (int)CLayer.Role.Roles.Affiliate;
                    Save(data);
                    ViewBag.Message = "The files are not selected";
                }

                return RedirectToAction("Details", "Affiliate", new { id = data.UserId });
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Corporate)
            {
                int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                maxContentLength = maxContentLength * maxFileSize;
                //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                string[] allowedFileExtensions = extensions.Split(',');
                if (data.ServiceTaxReg != null || data.PANCard != null || data.CorporateLogo != null)
                {
                    data.UserType = (int)CLayer.Role.Roles.Corporate;
                    Save(data);
                }
                else
                {
                    data.UserType = (int)CLayer.Role.Roles.Corporate;
                    Save(data);
                    ViewBag.Message = "The files are not selected";
                }
                return RedirectToAction("Details", "Corporate", new { id = data.UserId });
            }
            else
            {
                return View();
            }
        }
    }
}