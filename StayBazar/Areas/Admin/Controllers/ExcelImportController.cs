using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Threading.Tasks;
using System.Configuration;

namespace StayBazar.Areas.Admin.Controllers
{
    public class ExcelImportController : Controller
    {
        private enum ErrorType
        {
            None = 0,
            FileError = 1,
            OledbConnection = 2,
            SupplierError = 3,
            PropertyError = 4,
            AccommodationError = 5,
            OtherError = 6
        };
        //
        // GET: /Admin/ExcelImport/

        public ActionResult Import(string emaillist)
        {
            ViewBag.Emaillist = emaillist;
            return View();
        }
        [Common.AdminRolePermission]
        public ActionResult Index()
        {
            return View();
        }
        public string FixDataB2BCodes()
        {
            try
            {
                BLayer.B2B.FixB2BCodes();
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return "Done";
        }
        public async Task<ViewResult> DataImport(string EmailList)
        {
            int success = 0;

            try
            {
                success = BLayer.ExcelImport.ImportData();

               // await SendMailsAndSmsForSupplierImport(EmailList);

                BLayer.B2B.FixB2BCodes();
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return View(success);
        }
        [Common.AdminRolePermission]
        public ActionResult ExcelImport()
        {
            ErrorType eloc = ErrorType.None;
        
            string emaillist = "";
            try
            {
                if (Request.Files["FileUpload1"].ContentLength > 0)
                {
                    string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName);
                    string path1 = string.Format("{0}/{1}", Server.MapPath("~/Files/Temp/"), Request.Files["FileUpload1"].FileName);
                    if (System.IO.File.Exists(path1))
                        System.IO.File.Delete(path1);
                    eloc = ErrorType.FileError; //file error
                    Request.Files["FileUpload1"].SaveAs(path1);



                    //Create connection string to Excel work book

                    string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=Excel 12.0;Persist Security Info=False";
                    //Create Connection to Excel work book
                    //  OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    //Create OleDbCommand to fetch data from Excel


                    eloc = ErrorType.OledbConnection; //something happend in opening excel connection


                    DataTable dtSupplierDetails = new DataTable();
                    DataTable dtPropertyDetails = new DataTable();
                    DataTable dtAccomodationDetails = new DataTable();

                    using (OleDbConnection conn = new OleDbConnection(excelConnectionString))
                    {
                        OleDbCommand cmd = new OleDbCommand("SELECT [Supplier Name],[Contact Name],[Email],[Address],[City],[State],[Country],[Pincode],[Phone]" +
                                                      " ,[Mobile],[Service_Tax_Number],[VAT Number],[Bank_Account_Number],[Bank_name],[Branch_Address]" +
                                                      ",[RTGS Number],[MICR_Code],[PAN No] FROM [SupplierDetails$] where [Supplier Name]<>'' and [Email] <>'' ", conn);

                        conn.Open();

                        OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                        eloc = ErrorType.SupplierError;
                        adapter.Fill(dtSupplierDetails);


                        cmd = new OleDbCommand("SELECT [Supplier Name],[Property Name],[Description],[Location],[Address],[City],[State],[Country],[Pincode]" +
                                           ",[Property_Features],[Cancellation_Charge],[Cancellation_Period_Hours],[Cop_Discount_ShortTerm],[Corp_Discount_LongTerm],[Service_Tax],[Luxury_Tax]" +
                                           ",[Markup_B2B_ShortTerm],[Markup_B2B_LongTerm],[Markup_B2C_ShortTerm],[Markup_B2C_LongTerm],[Cancellation_Policy]" +
                                           ",[Cancellation_Value],[Cancellation_Period],[Policy_Applicable_ForRefund],[Property_Email_ID],[Phone],[Mobile] FROM [PropertyDetails$] where [Property Name]<>''", conn);
                        adapter = new OleDbDataAdapter(cmd);
                        eloc = ErrorType.PropertyError;
                        adapter.Fill(dtPropertyDetails);

                        cmd = new OleDbCommand("SELECT [Property Name],[Category],[Type],[Description],[Max Adults],[Max Kids],[Max Occupancy],[BedRooms],[Area m#sq]" +
                                               ",[Available Accommodation],[Total Accommodation],[Accommodation Features],[B2B_Daily],[B2B_Weekly],[B2B_Monthly],[B2B_Long]" +
                                               ",[B2B_Guest],[B2C_Daily],[B2C_Weekly],[B2C_Monthly],[B2C_Long],[B2C_Guest] FROM [Accommodation$] where [Property Name]<>''", conn);
                        adapter = new OleDbDataAdapter(cmd);
                        eloc = ErrorType.AccommodationError;
                        adapter.Fill(dtAccomodationDetails);

                        conn.Close();

                    }
                    eloc = ErrorType.OtherError;
                                    
                    //create Supplier Emaillist
                    if (dtSupplierDetails.Rows.Count > 0)
                    {
                      
                        string[] supplieremaillist = dtSupplierDetails.AsEnumerable().Select(s => s.Field<string>("Email")).ToArray<string>();
                        for (int i = supplieremaillist.Length - 1; i >= 0; i--)
                        {
                            emaillist = emaillist + supplieremaillist[i] + ",";
                        }
                        emaillist = emaillist + "";
                    }

                    BLayer.ExcelImport.ImportToDBFromExcel(dtSupplierDetails, dtPropertyDetails, dtAccomodationDetails);
                    try
                    {
                        if (System.IO.File.Exists(path1))
                        {
                            System.IO.File.Delete(path1);
                        }
                    }

                    catch (Exception ex)
                    {
                        Common.LogHandler.HandleError(ex);
                    }

                }
                else
                {
                    ViewBag.ErrorMessage = "Please upload excel import file";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
                string msg = "";
                switch (eloc)
                {
                    case ErrorType.None:
                    case ErrorType.OtherError:
                        msg = "An Unknown error occured.";
                        break;
                    case ErrorType.FileError:
                        msg = "Cannot access/save file at Server.";
                        break;
                    case ErrorType.OledbConnection:
                        msg = "Issue related to Oledb Connection.";
                        break;
                    case ErrorType.SupplierError:
                        msg = "Supplier details sheet has an issue. Please check the column heading names.";
                        break;
                    case ErrorType.PropertyError:
                        msg = "Property details sheet has an issue. Please check the column heading names.";
                        break;
                    case ErrorType.AccommodationError:
                        msg = "Accommodation details sheet has an issue. Please check the column heading names.";
                        break;
                }
                ViewBag.ErrorMessage = msg + "<br />Technical Details: " + ex.Message;
                return View("Index");
            }



            return RedirectToAction("Import", new { emaillist = emaillist });
        }
        public async Task<bool> SendMailsAndSmsForSupplierImport(string SuEmailList)
        {
            try
            {
                string message = "";
                Common.Mailer ml = new Common.Mailer();

                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

                string emailids = ConfigurationManager.AppSettings.Get("CustomerCareMail");
                if (emailids != "")
                {
                    string[] emails = emailids.Split(',');
                    for (int i = 0; i < emails.Length; ++i)
                    {
                        msg.Bcc.Add(emails[i]);
                    }
                }

                msg.Subject = "Supplier Confirmation";
                msg.IsBodyHtml = true;
          

                string[] SuEmails = SuEmailList.Split(',');
                foreach (string email in SuEmails)
                {
                    if (email != "")
                    {
                        msg.To.Add(email);
                        long userid = BLayer.User.GetUserId(email);

                        //string pass = "Akqp80";
                        message = await Common.Mailer.MessageFromHtml(System.Configuration.ConfigurationManager.AppSettings.Get("SupImportRegistration") + userid);
                        msg.Body = message;
                        try
                        {
                           // await ml.SendMailAsync(msg, Common.Mailer.MailType.Reservation);
                        }
                        catch (Exception ex)
                        {
                            Common.LogHandler.HandleError(ex);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }

            return true;
        }
    }
}