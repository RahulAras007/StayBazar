using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using CaptchaMvc;

namespace StayBazar.Controllers
{
    public class JoinController : Controller
    {

        #region CommonFunctions

        private void Save(Models.B2BModel data)
        {
            string tempName = "";
            if (data.UserType != (int)CLayer.Role.Roles.Agent)
            {
                if (data.ContactName == null)
                    tempName = data.Name.TrimStart();//Business Name
                else
                    tempName = data.ContactName.TrimStart();// Name
            }

            else
                tempName = data.ContactName.TrimStart();


            string tFirstName = tempName;
            string tLastName = "";

            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                tFirstName = tempName;
                tLastName = tempName;
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Corporate)
            {
                tFirstName = tempName;
                tLastName = tempName;
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Agent)
            {
                tFirstName = tempName;
                tLastName = tempName;

            }
            else
            {
                // tFirstName = data.ContactName.TrimStart();
                // tLastName = data.ContactName.TrimStart();
                //---
                int firstspace = tempName.IndexOf(' ');
                if (firstspace > 1)
                    if (firstspace < data.ContactName.Length - 1)
                    {
                        tFirstName = tempName.Split(' ')[0].ToString();
                        tLastName = tempName.Substring(firstspace + 1, tempName.Length - tFirstName.Length - 1);
                    }
                //----

            }
            CLayer.User usr = new CLayer.User()
            {
                SalutationId = 1,

                FirstName = tFirstName,
                LastName = "",
                Email = data.Email,
                UserTypeId = data.UserType,
                Status = (int)CLayer.ObjectStatus.StatusType.NotVerified
            };
            long UsrId = BLayer.User.Save(usr);

            CLayer.B2B b2b = new CLayer.B2B()
            {
                B2BId = UsrId,
                Name = data.Name,//Business Name
                ServiceTaxRegNo = data.ServiceTaxRegNo,
                PANNo = data.PANNo,
                VATRegNo = data.VATRegNo,
                ContactDesignation=data.ContactDesignation
            };

            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                b2b.PropertyDescription = data.PropertyDescription;
                b2b.AvailableProperties = data.AvailableProperties;
            }
            data.B2BId = BLayer.B2B.Save(b2b);

            CLayer.Address address = new CLayer.Address()
            {
                AddressId = 0,
                UserId = UsrId,
                AddressText = data.Address,
                CityId = data.CityId,
                State = data.State,
                CountryId = data.CountryId,
                ZipCode = data.ZipCode,
                Phone = data.Phone,
                Mobile = data.Mobile,
                AddressType = (int)CLayer.Address.AddressTypes.Normal
            };

            if (data.City != null && data.City != "")
                address.City = data.City;
            if (data.CityId > 0)
                address.City = BLayer.City.Get(data.CityId).Name;
            address.AddressType = (int)CLayer.Address.AddressTypes.Normal;
            BLayer.Address.Save(address);

            #region Billing Address For Corporate have to be here before "save" line of the address object
            if (data.UserType == (int)CLayer.Role.Roles.Corporate)
            {
                if (data.IsClicked == true) {
                    CLayer.Address billingaddress = new CLayer.Address()
                    {
                        AddressId = 0,
                        UserId = UsrId,
                        AddressText = data.Address,
                        CityId = data.CityId,                    
                        State = data.State,
                        CountryId = data.CountryId,
                        ZipCode = data.ZipCode,
                        Phone = data.Phone,
                        Mobile = data.Mobile,
                        AddressType = (int)CLayer.Address.AddressTypes.Primary
                    };
                    if (data.City != null && data.City != "")
                        billingaddress.City = data.City;
                    if (data.CityId > 0)
                        billingaddress.City = BLayer.City.Get(data.CityId).Name;
                    BLayer.Address.Save(billingaddress);
                }
                else{
                    if (data.BillingAddress != "")
                    {
                        CLayer.Address billingaddress = new CLayer.Address()
                        {
                            AddressId = 0,
                            UserId = UsrId,
                            AddressText = data.BillingAddress,
                            CityId = data.BillingCityId,
                            State = data.BillingState,
                            CountryId = data.BillingCountryId,
                            ZipCode =data.BillingZipCode,
                            Phone = data.Phone,
                            Mobile =data.Mobile,
                            AddressType = (int)CLayer.Address.AddressTypes.Primary
                        };
                        if (data.BillingCity != null && data.BillingCity != "")
                            billingaddress.City = data.BillingCity;
                        if (data.BillingCityId > 0)
                            billingaddress.City = BLayer.City.Get(data.BillingCityId).Name;
                        BLayer.Address.Save(billingaddress);
                
                }
                //if (data.BillingAddress != "")
                //{
                //    CLayer.Address billingaddress = new CLayer.Address()
                //    {
                //        AddressId = 0,
                //        UserId = UsrId,
                //        AddressText = data.BillingAddress,
                //        CityId = data.BillingCityId,
                //        State = data.BillingState,
                //        CountryId = data.BillingCountryId,
                //        ZipCode = "",
                //        Phone = "",
                //        Mobile = "",
                //        AddressType = (int)CLayer.Address.AddressTypes.Normal
                //    };
                //    if (data.BillingCity != null && data.BillingCity != "")
                //        billingaddress.City = data.BillingCity;
                //    if (data.BillingCityId > 0)
                //        billingaddress.City = BLayer.City.Get(data.BillingCityId).Name;
                //    BLayer.Address.Save(billingaddress);
                }
            }
            #endregion

            #region Bank Account details for Supplier
            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                if (data.BankName != "")
                {
                    CLayer.BankAccount account = new CLayer.BankAccount()
                    {
                        BankAccountId = 0,
                        UserId = UsrId,
                        AccountName = data.Name,//Business name
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
            if (data.ServiceTaxReg != null)
            {
                CLayer.UserFiles servicetax = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.ServiceTaxReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.ServiceTaxRegNo
                };
                BLayer.UserFiles.Save(servicetax);
                SaveDocument(data.ServiceTaxReg, UsrId,servicetax.FileName);
            }
            if (data.VATReg != null)
            {
                CLayer.UserFiles vat = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.VATReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.VATRegNo
                };
                BLayer.UserFiles.Save(vat);
                SaveDocument(data.VATReg, UsrId,vat.FileName);
            }
            if (data.BusinessRegistrationCertificate != null)
            {
                CLayer.UserFiles brc = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.BusinessRegistrationCertificate.FileName),
                    Document = (int)CLayer.UserFiles.Documents.BusinessRegistrationCertificate
                };
                BLayer.UserFiles.Save(brc);
                SaveDocument(data.BusinessRegistrationCertificate, UsrId,brc.FileName);
            }
            if (data.PANCard != null)
            {
                CLayer.UserFiles pan = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.PANCard.FileName),
                    Document = (int)CLayer.UserFiles.Documents.PANCard
                };
                BLayer.UserFiles.Save(pan);
                SaveDocument(data.PANCard, UsrId,pan.FileName);
            }
            if (data.CorporateLogo != null)
            {
                CLayer.UserFiles corporatelogo = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CorporateLogo.FileName),
                    Document = (int)CLayer.UserFiles.Documents.CorporateLogo
                };
                BLayer.UserFiles.Save(corporatelogo);
                SaveDocument(data.CorporateLogo, UsrId, corporatelogo.FileName);
            }
            if (data.CopyOfCheque != null)
            {
                CLayer.UserFiles cc = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CopyOfCheque.FileName),
                    Document = (int)CLayer.UserFiles.Documents.CopyOfCheque
                };
                BLayer.UserFiles.Save(cc);
                SaveDocument(data.CopyOfCheque, UsrId,cc.FileName);
            }
            #endregion
        }
        private void Save3(Models.AffiliatesB2BModel data)
        {
            string tempName = "";
            if (data.UserType != (int)CLayer.Role.Roles.Agent)


                if (data.ContactName == null)
                    tempName = data.Name.TrimStart();//Business Name
                else
                    tempName = data.ContactName.TrimStart();// Name


            else
                tempName = data.ContactName.TrimStart();


            string tFirstName = tempName;
            string tLastName = "";

            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                tFirstName = tempName;
                tLastName = tempName;
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Corporate)
            {
                tFirstName = tempName;
                tLastName = tempName;
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Agent)
            {
                tFirstName = tempName;
                tLastName = tempName;

            }
            else
            {
                // tFirstName = data.ContactName.TrimStart();
                // tLastName = data.ContactName.TrimStart();
                //---
                int firstspace = tempName.IndexOf(' ');
                if (firstspace > 1)
                    if (firstspace < data.ContactName.Length - 1)
                    {
                        tFirstName = tempName.Split(' ')[0].ToString();
                        tLastName = tempName.Substring(firstspace + 1, tempName.Length - tFirstName.Length - 1);
                    }
                //----

            }
            CLayer.User usr = new CLayer.User()
            {
                SalutationId = 1,

                FirstName = tFirstName,
                LastName = "",
                Email = data.Email,
                UserTypeId = data.UserType,
                Status = (int)CLayer.ObjectStatus.StatusType.NotVerified
            };
            long UsrId = BLayer.User.Save(usr);

            CLayer.B2B b2b = new CLayer.B2B()
            {
                B2BId = UsrId,
                Name = data.Name,//Business Name
                ServiceTaxRegNo = data.ServiceTaxRegNo,
                CompanyRegNo=data.CompanyRegNo,
                PANNo = data.PANNo,
                VATRegNo = data.VATRegNo
            };
            //if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            //{
            //    b2b.PropertyDescription = data.PropertyDescription;
            //    b2b.AvailableProperties = data.AvailableProperties;
            //}
            if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
            {
                b2b.PropertyDescription = "";
                b2b.AvailableProperties = 0;
            }
            data.B2BId = BLayer.B2B.Save(b2b);

            CLayer.Address address = new CLayer.Address()
            {
                AddressId = 0,
                UserId = UsrId,
                AddressText = data.Address,
                CityId = data.CityId,
                State = data.State,
                CountryId = data.CountryId,
                ZipCode = data.ZipCode,
                Phone = data.Phone,
                Mobile = data.Mobile,
                AddressType = (int)CLayer.Address.AddressTypes.Primary
            };

            if (data.City != null && data.City != "")
                address.City = data.City;
            if (data.CityId > 0)
                address.City = BLayer.City.Get(data.CityId).Name;
            address.AddressType = (int)CLayer.Address.AddressTypes.Primary;
            BLayer.Address.Save(address);

            #region Billing Address For Corporate have to be here before "save" line of the address object
            if (data.UserType == (int)CLayer.Role.Roles.Corporate)
            {
                if (data.BillingAddress != "")
                {

                    CLayer.Address billingaddress = new CLayer.Address()
                    {
                        AddressId = 0,
                        UserId = UsrId,
                        AddressText = data.BillingAddress,
                        CityId = data.BillingCityId,
                        State = data.BillingState,
                        CountryId = data.BillingCountryId,
                        ZipCode = "",
                        Phone = "",
                        Mobile = "",
                        AddressType = (int)CLayer.Address.AddressTypes.Primary
                    };
                    if (data.BillingCity != null && data.BillingCity != "")
                        billingaddress.City = data.BillingCity;
                    if (data.BillingCityId > 0)
                        billingaddress.City = BLayer.City.Get(data.BillingCityId).Name;
                    BLayer.Address.Save(billingaddress);
                }
            }
            #endregion

            #region Bank Account details for Supplier
            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                if (data.BankName != "")
                {
                    CLayer.BankAccount account = new CLayer.BankAccount()
                    {
                        BankAccountId = 0,
                        UserId = UsrId,
                        AccountName = data.Name,//Business name
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
            #region Bank Account details for Supplier
            if (data.UserType == (int)CLayer.Role.Roles.Affiliate)
            {
                if (data.BankName != "")
                {
                    CLayer.BankAccount account = new CLayer.BankAccount()
                    {
                        BankAccountId = 0,
                        UserId = UsrId,
                        AccountName = data.Name,//Business name
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
            if (data.ServiceTaxReg != null)
            {
                CLayer.UserFiles servicetax = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.ServiceTaxReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.ServiceTaxRegNo
                };

                BLayer.UserFiles.Save(servicetax);
                SaveDocument(data.ServiceTaxReg, UsrId,servicetax.FileName);
            }
            if (data.VATReg != null)
            {
                CLayer.UserFiles vat = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.VATReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.VATRegNo
                };
                BLayer.UserFiles.Save(vat);
                SaveDocument(data.VATReg, UsrId,vat.FileName);
            }
            if (data.BusinessRegistrationCertificate != null)
            {
                CLayer.UserFiles brc = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.BusinessRegistrationCertificate.FileName),
                    Document = (int)CLayer.UserFiles.Documents.BusinessRegistrationCertificate
                };
                BLayer.UserFiles.Save(brc);
                SaveDocument(data.BusinessRegistrationCertificate, UsrId,brc.FileName);
            }
            if (data.PANCard != null)
            {
                CLayer.UserFiles pan = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.PANCard.FileName),
                    Document = (int)CLayer.UserFiles.Documents.PANCard
                };
                BLayer.UserFiles.Save(pan);
                SaveDocument(data.PANCard, UsrId,pan.FileName);
            }
            if (data.CopyOfCheque != null)
            {
                CLayer.UserFiles cc = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CopyOfCheque.FileName),
                    Document = (int)CLayer.UserFiles.Documents.CopyOfCheque
                };
                BLayer.UserFiles.Save(cc);
                SaveDocument(data.CopyOfCheque, UsrId, cc.FileName);
            }
            #endregion
        }

        private void Save2(Models.SupplierB2BModel data)
        {
            string tempName = "";
            if (data.UserType != (int)CLayer.Role.Roles.Agent)


                if (data.ContactName == null)
                    tempName = data.Name.TrimStart();//Business Name
                else
                    tempName = data.ContactName.TrimStart();// Name


            else
                tempName = data.ContactName.TrimStart();


            string tFirstName = tempName;
            string tLastName = "";

            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                tFirstName = tempName;
                tLastName = tempName;
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Corporate)
            {
                tFirstName = tempName;
                tLastName = tempName;
            }
            else if (data.UserType == (int)CLayer.Role.Roles.Agent)
            {
                tFirstName = tempName;
                tLastName = tempName;

            }
            else
            {
                // tFirstName = data.ContactName.TrimStart();
                // tLastName = data.ContactName.TrimStart();
                //---
                int firstspace = tempName.IndexOf(' ');
                if (firstspace > 1)
                    if (firstspace < data.ContactName.Length - 1)
                    {
                        tFirstName = tempName.Split(' ')[0].ToString();
                        tLastName = tempName.Substring(firstspace + 1, tempName.Length - tFirstName.Length - 1);
                    }
                //----

            }
            CLayer.User usr = new CLayer.User()
            {
                SalutationId = 1,

                FirstName = tFirstName,
                LastName = "",
                Email = data.Email,
                UserTypeId = data.UserType,
                Status = (int)CLayer.ObjectStatus.StatusType.NotVerified
            };
            long UsrId = BLayer.User.Save(usr);

            CLayer.B2B b2b = new CLayer.B2B()
            {
                B2BId = UsrId,
                Name = data.Name,//Business Name
                ServiceTaxRegNo = data.ServiceTaxRegNo,
                PANNo = data.PANNo,
                VATRegNo = data.VATRegNo
            };

            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                b2b.PropertyDescription = data.PropertyDescription;
                b2b.AvailableProperties = data.AvailableProperties;
            }
            data.B2BId = BLayer.B2B.Save(b2b);

            CLayer.Address address = new CLayer.Address()
            {
                AddressId = 0,
                UserId = UsrId,
                AddressText = data.Address,
                CityId = data.CityId,
                State = data.State,
                CountryId = data.CountryId,
                ZipCode = data.ZipCode,
                Phone = data.Phone,
                Mobile = data.Mobile,
                AddressType = (int)CLayer.Address.AddressTypes.Primary
            };

            if (data.City != null && data.City != "")
                address.City = data.City;
            if (data.CityId > 0)
                address.City = BLayer.City.Get(data.CityId).Name;
            address.AddressType = (int)CLayer.Address.AddressTypes.Primary;
            BLayer.Address.Save(address);

            #region Billing Address For Corporate have to be here before "save" line of the address object
            if (data.UserType == (int)CLayer.Role.Roles.Corporate)
            {
                if (data.BillingAddress != "")
                {

                    CLayer.Address billingaddress = new CLayer.Address()
                    {
                        AddressId = 0,
                        UserId = UsrId,
                        AddressText = data.BillingAddress,
                        CityId = data.BillingCityId,
                        State = data.BillingState,
                        CountryId = data.BillingCountryId,
                        ZipCode = "",
                        Phone = "",
                        Mobile = "",
                        AddressType = (int)CLayer.Address.AddressTypes.Primary
                    };
                    if (data.BillingCity != null && data.BillingCity != "")
                        billingaddress.City = data.BillingCity;
                    if (data.BillingCityId > 0)
                        billingaddress.City = BLayer.City.Get(data.BillingCityId).Name;
                    BLayer.Address.Save(billingaddress);
                }
            }
            #endregion

            #region Bank Account details for Supplier
            if (data.UserType == (int)CLayer.Role.Roles.Supplier)
            {
                if (data.BankName != "")
                {
                    CLayer.BankAccount account = new CLayer.BankAccount()
                    {
                        BankAccountId = 0,
                        UserId = UsrId,
                        AccountName = data.Name,//Business name
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
            if (data.ServiceTaxReg != null)
            {
                CLayer.UserFiles servicetax = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.ServiceTaxReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.ServiceTaxRegNo
                };
                BLayer.UserFiles.Save(servicetax);
                SaveDocument(data.ServiceTaxReg, UsrId,servicetax.FileName);
            }
            if (data.VATReg != null)
            {
                CLayer.UserFiles vat = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.VATReg.FileName),
                    Document = (int)CLayer.UserFiles.Documents.VATRegNo
                };
                BLayer.UserFiles.Save(vat);
                SaveDocument(data.VATReg, UsrId,vat.FileName);
            }
            if (data.BusinessRegistrationCertificate != null)
            {
                CLayer.UserFiles brc = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.BusinessRegistrationCertificate.FileName),
                    Document = (int)CLayer.UserFiles.Documents.BusinessRegistrationCertificate
                };
                BLayer.UserFiles.Save(brc);
                SaveDocument(data.BusinessRegistrationCertificate, UsrId,brc.FileName);
            }
            if (data.PANCard != null)
            {
                CLayer.UserFiles pan = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.PANCard.FileName),
                    Document = (int)CLayer.UserFiles.Documents.PANCard
                };
                BLayer.UserFiles.Save(pan);
                SaveDocument(data.PANCard, UsrId,pan.FileName);
            }
            if (data.CopyOfCheque != null)
            {
                CLayer.UserFiles cc = new CLayer.UserFiles()
                {
                    UserId = UsrId,
                    FileId = 0,
                    FileName = UsrId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(data.CopyOfCheque.FileName),
                    Document = (int)CLayer.UserFiles.Documents.CopyOfCheque
                };
                BLayer.UserFiles.Save(cc);
                SaveDocument(data.CopyOfCheque, UsrId,cc.FileName);
            }
            #endregion
        }

        private void SaveDocument(HttpPostedFileBase file, long UserId,string FileName)
        {
            //var fileName = UserId.ToString() + DateTime.Now.ToString("ddMMyyyyHHmmss") + System.IO.Path.GetFileName(file.FileName);
            if (!System.IO.Directory.Exists(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/")))
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/"));
            }
            var path = System.IO.Path.Combine(Server.MapPath("~/Files/Users/" + UserId.ToString() + "/"), FileName);
            file.SaveAs(path);
            ModelState.Clear();
        }

        #endregion

        #region PageCallers

        //
        // GET: /Join/
        [AllowAnonymous]
        public ActionResult Index()
        {
            Models.B2BModel m = new Models.B2BModel();

            return View();
        }

        [AllowAnonymous]
        public ActionResult Supplier(string Thanks)
        {
            if (Thanks != null && Thanks != "")
                return View("RequestPosted");
            else
                 return View(new Models.SupplierB2BModel() { UserType = (int)CLayer.Role.Roles.Supplier });
        }

        [AllowAnonymous]
        public ActionResult TravelAgent(string Thanks)
        {
            if (Thanks != null && Thanks != "")
                return View("RequestPosted");
            else
             return View(new Models.B2BModel() { UserType = (int)CLayer.Role.Roles.Agent });
        }

        [AllowAnonymous]
        public ActionResult Corporate(string Thanks)
        {
            if(Thanks != null && Thanks != "")
                return View("RequestPosted");
            else
               return View(new Models.B2BModel() { UserType = (int)CLayer.Role.Roles.Corporate });  
        }
        //[AllowAnonymous]
        //public ActionResult Test()
        //{
        //    Response.Redirect("/Join/Corporate/Thank-You");
        //    return View();
        //    //return RedirectToAction("Corporate", new { Thanks="Thank-You"});
        //}
        [AllowAnonymous]
        public ActionResult Affiliates(string Thanks)
        {
            if (Thanks != null && Thanks != "")
               return View("RequestPosted");   
            else
                return View(new Models.AffiliatesB2BModel() { UserType = (int)CLayer.Role.Roles.Affiliate });
        }
        //[AllowAnonymous]
        //public ActionResult ThankYou()
        //{
        //    return View("RequestPosted");
        //}


        #endregion

        #region SubmitActions
        //SupplierSave
        [CaptchaMvc.Attributes.CaptchaVerify("Captcha Is  Not valid")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SupplierSave(Models.SupplierB2BModel data)
        {
            try
            {
                if (data != null)
                {
                    data.LoadPlaces();
                }
                if (ModelState.IsValid)
                {
                    ViewBag.Message = null;
                    if (BLayer.User.GetUserIdbystatus(data.Email) > 0)
                    {
                        ViewBag.Message = "The email id already used by someone else";
                        return View("Supplier", data);
                    }
                    else
                    {
                        int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                        string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                        int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                        maxContentLength = maxContentLength * maxFileSize;
                        //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                        string[] allowedFileExtensions = extensions.Split(',');
                     //   data.LoadPlaces();
                        if (data.ServiceTaxReg != null && data.VATReg != null && data.ServiceTaxReg.ContentLength > 0 && data.VATReg.ContentLength > 0)
                        {
                            if (!allowedFileExtensions.Contains(data.ServiceTaxReg.FileName.Substring(data.ServiceTaxReg.FileName.LastIndexOf('.'))) ||
                                !allowedFileExtensions.Contains(data.VATReg.FileName.Substring(data.VATReg.FileName.LastIndexOf('.'))))
                            {
                                ViewBag.Message = "Please use files of type: " + string.Join(", ", allowedFileExtensions);
                                return View("Supplier", data);
                            }
                            else if (data.ServiceTaxReg.ContentLength > maxContentLength || data.VATReg.ContentLength > maxContentLength)
                            {
                                ViewBag.Message = "Your file is too large, maximum allowed size is: 1 MB";
                                return View("Supplier", data);
                            }
                            else
                            {
                                data.UserType = (int)CLayer.Role.Roles.Supplier;
                                Save2(data);
                               // return RedirectToAction("ThankYou");
                                Response.Redirect("/Join/Supplier/Thank-You",true);
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Message = "The files are not selected";
                            return View("Supplier", data);
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Please fill all the required fields";
                    return View("Supplier", data);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [CaptchaMvc.Attributes.CaptchaVerify("Captcha Is  Not valid")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TravelAgentSave(Models.B2BModel data)
        {
            try
            {
                if(data != null)
                {
                    data.LoadPlaces();
                }
                if (ModelState.IsValid)
                {

                    ViewBag.Message = null;
                    if (BLayer.User.GetUserIdbystatus(data.Email) > 0)
                    {
                        ViewBag.Message = "The email id already used by someone else";
                        return View("TravelAgent", data);
                    }
                    else
                    {
                        int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                        string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                        int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                        maxContentLength = maxContentLength * maxFileSize;
                        //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                        string[] allowedFileExtensions = extensions.Split(',');

                        if (data.ServiceTaxReg != null && data.BusinessRegistrationCertificate != null &&
                            data.PANCard != null && data.CopyOfCheque != null &&
                            data.ServiceTaxReg.ContentLength > 0 && data.BusinessRegistrationCertificate.ContentLength > 0 &&
                            data.PANCard.ContentLength > 0 && data.CopyOfCheque.ContentLength > 0)
                        {
                            if (!allowedFileExtensions.Contains(data.ServiceTaxReg.FileName.Substring(data.ServiceTaxReg.FileName.LastIndexOf('.'))) ||
                                !allowedFileExtensions.Contains(data.BusinessRegistrationCertificate.FileName.Substring(data.BusinessRegistrationCertificate.FileName.LastIndexOf('.'))) ||
                                !allowedFileExtensions.Contains(data.PANCard.FileName.Substring(data.PANCard.FileName.LastIndexOf('.'))) ||
                                !allowedFileExtensions.Contains(data.CopyOfCheque.FileName.Substring(data.CopyOfCheque.FileName.LastIndexOf('.'))))
                            {
                                ViewBag.Message = "Invalid document type. Please use these file types: " + string.Join(", ", allowedFileExtensions);
                                return View("TravelAgent", data);
                            }
                            else if (data.ServiceTaxReg.ContentLength > maxContentLength ||
                                data.BusinessRegistrationCertificate.ContentLength > maxContentLength ||
                                data.PANCard.ContentLength > maxContentLength ||
                                data.CopyOfCheque.ContentLength > maxContentLength)
                            {
                                ViewBag.Message = "Your file is too large, maximum allowed size is: 1 MB";
                                return View("TravelAgent", data);
                            }
                            else
                            {
                                data.UserType = (int)CLayer.Role.Roles.Agent;
                                Save(data);
                                Response.Redirect("/Join/TravelAgent/Thank-You",true);
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Message = "The documents are not selected";
                            return View("TravelAgent", data);
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Please fill all the required fields";
                    return View("TravelAgent", data);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [CaptchaMvc.Attributes.CaptchaVerify("Captcha Is  Not valid")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CorporateSave(Models.B2BModel data)
        {
            try
            {
                if (data != null)
                {
                    data.LoadPlaces();
                }
                //if (ModelState.IsValid)
                //{
                    ViewBag.Message = null;
                    if (BLayer.User.GetUserIdbystatus(data.Email) > 0)
                    {
                        ViewBag.Message = "The email id already used by someone else";
                        return View("Corporate", data);
                    }
                    else
                    {
                        int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                        string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                        int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                        maxContentLength = maxContentLength * maxFileSize;
                        //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                        string[] allowedFileExtensions = extensions.Split(',');

                        if (data.ServiceTaxReg != null && data.PANCard != null || data.CorporateLogo!=null)
                        {
                            if (!allowedFileExtensions.Contains(data.ServiceTaxReg.FileName.Substring(data.ServiceTaxReg.FileName.LastIndexOf('.'))) ||
                                  !allowedFileExtensions.Contains(data.PANCard.FileName.Substring(data.PANCard.FileName.LastIndexOf('.'))) ||
                                  !allowedFileExtensions.Contains(data.CorporateLogo.FileName.Substring(data.CorporateLogo.FileName.LastIndexOf('.')))
                                )
                            {
                                ViewBag.Message = "Please use files of type: " + string.Join(", ", allowedFileExtensions);
                                return View("Corporate", data);
                            }
                            else if (data.ServiceTaxReg.ContentLength > maxContentLength || data.PANCard.ContentLength > maxContentLength ||data.CorporateLogo.ContentLength>maxContentLength)
                            {
                                ViewBag.Message = "Your file is too large, maximum allowed size is: 1 MB";
                                return View("Corporate", data);
                            }
                            else
                            {
                                data.UserType = (int)CLayer.Role.Roles.Corporate;
                                Save(data);
                                Response.Redirect("/Join/Corporate/Thank-You", true);
                                return View();
                            }
                        }
                        else
                        {
                            data.UserType = (int)CLayer.Role.Roles.Corporate;
                            Save(data);
                            Response.Redirect("/Join/Corporate/Thank-You", true);
                            return View();
                        }
                    }
                //}
                //else
                //{
                //    ViewBag.Message = "Please fill all the required fields";
                //    return View("Corporate", data);
                //}
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }

        [CaptchaMvc.Attributes.CaptchaVerify("Captcha Is  Not valid")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AffiliatesSave(Models.AffiliatesB2BModel data)
        {
            try
            {
                if (data != null)
                {
                    data.LoadPlaces();
                }
                if (ModelState.IsValid)
                {
                    ViewBag.Message = null;
                    if (BLayer.User.GetUserIdbystatus(data.Email) > 0)
                    {
                        ViewBag.Message = "The email id already used by someone else";
                        return View("Affiliates", data);
                    }
                    else
                    {
                        int maxContentLength = 1024 * 1024;// 1 MB  //*3; //3 MB
                        string extensions = ConfigurationManager.AppSettings.Get("DocumentFileTypes");
                        int maxFileSize = Convert.ToInt32(ConfigurationManager.AppSettings.Get("FileSizeInMB"));
                        maxContentLength = maxContentLength * maxFileSize;
                        //string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".doc", ".xlsx", ".docx", ".xls" };
                        string[] allowedFileExtensions = extensions.Split(',');

                        if (data.ServiceTaxReg != null && data.VATReg != null && data.ServiceTaxReg.ContentLength > 0 && data.VATReg.ContentLength > 0)
                        {
                            if (!allowedFileExtensions.Contains(data.ServiceTaxReg.FileName.Substring(data.ServiceTaxReg.FileName.LastIndexOf('.'))) ||
                                !allowedFileExtensions.Contains(data.VATReg.FileName.Substring(data.VATReg.FileName.LastIndexOf('.'))))
                            {
                                ViewBag.Message = "Please use files of type: " + string.Join(", ", allowedFileExtensions);
                                return View("Affiliates", data);
                            }
                            else if (data.ServiceTaxReg.ContentLength > maxContentLength || data.VATReg.ContentLength > maxContentLength)
                            {
                                ViewBag.Message = "Your file is too large, maximum allowed size is: 1 MB";
                                return View("Affiliates", data);
                            }
                            else
                            {
                                data.UserType = (int)CLayer.Role.Roles.Affiliate;
                                Save3(data);
                                Response.Redirect("/Join/Affiliates/Thank-You",true);
                                return View();
                            }
                        }
                        else
                        {
                            ViewBag.Message = "The files are not selected";
                            return View("Affiliates", data);
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Please fill all the required fields";
                    return View("Affiliates", data);
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                return Redirect("~/ErrorPage");
            }
        }
        #endregion
    }
}