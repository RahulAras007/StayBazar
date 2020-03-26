using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Microsoft.AspNet.Identity;
using MySql.Data.MySqlClient;
using StayBazar.Areas.Admin.Models;

namespace StayBazar.Areas.Admin.Controllers
{
    public class CancellationImportController : Controller
    {
        // GET: Admin/CancellationImport
        public ActionResult Import_CancellationData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Import_CancellationData(HttpPostedFileBase file)
        {
            string userid = User.Identity.GetUserId();
            string lsz_Dateofcancellation;
            string lsz_checkin;
            string lsz_checkout;
            DataSet ds = new DataSet();
            if (Request.Files["file"].ContentLength > 0)
            {
                string fileExtension = System.IO.Path.GetExtension(Request.Files["file"].FileName);
                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["file"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }
                    Request.Files["file"].SaveAs(fileLocation);
                    string excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                  fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    if (fileExtension == ".xls")
                    {
                        excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                    }

                    else if (fileExtension == ".xlsx")
                    {
                        excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                        fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }
                    OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                    excelConnection.Open();
                    DataTable dt = new DataTable();
                    dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    if (dt == null)
                    {
                        return null;
                    }
                    String[] excelSheets = new String[dt.Rows.Count];
                    int t = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        excelSheets[t] = row["Table_Name"].ToString();
                        t++;
                    }
                    OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);
                    string query = string.Format("Select * from [{0}]", excelSheets[0]);
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                    {
                        dataAdapter.Fill(ds);
                    }
                    excelConnection.Close();

                }
                if (fileExtension.ToString().ToLower().Equals(".xml"))
                {
                    string fileLocation = Server.MapPath("~/Content/") + Request.Files["FileUpload"].FileName;
                    if (System.IO.File.Exists(fileLocation))
                    {
                        System.IO.File.Delete(fileLocation);
                    }

                    Request.Files["FileUpload"].SaveAs(fileLocation);
                    XmlTextReader xmlreader = new XmlTextReader(fileLocation);

                    ds.ReadXml(xmlreader);
                    xmlreader.Close();
                }
                string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                MySqlConnection con = new MySqlConnection(conn);
                string deletequery = "Delete from cancellationimport where UserID=" + userid;
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand(deletequery, con);
                cmd1.ExecuteNonQuery();
                con.Close();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString() != "")
                    {
                        lsz_Dateofcancellation = String.Format("{0:yyyy-MM-dd}", ds.Tables[0].Rows[i][0]);
                        lsz_checkin = String.Format("{0:yyyy-MM-dd HH:mm:ss}", ds.Tables[0].Rows[i][8]);
                        lsz_checkout = String.Format("{0:yyyy-MM-dd HH:mm:ss}", ds.Tables[0].Rows[i][9]);
                        string query = "Insert into cancellationimport(date_of_cancelation,customer_name,customer_state,property_name,property_city,property_state,accomodation_type,room_category_type,check_in,check_out,guest_name,UserID,Itilite_booking_ID) Values('" +
                        lsz_Dateofcancellation +
                        "','" + ds.Tables[0].Rows[i][1].ToString() +
                        "','" + ds.Tables[0].Rows[i][2].ToString() +
                        "','" + ds.Tables[0].Rows[i][3].ToString() +
                        "','" + ds.Tables[0].Rows[i][4].ToString() +
                        "','" + ds.Tables[0].Rows[i][5].ToString() +
                        "','" + ds.Tables[0].Rows[i][6].ToString() +
                        "','" + ds.Tables[0].Rows[i][7].ToString() +
                        "','" + lsz_checkin +
                        "','" + lsz_checkout +
                        "','" + ds.Tables[0].Rows[i][10].ToString() +
                        "','" + userid.ToString() +
                          "','" + ds.Tables[0].Rows[i][11].ToString() +
                        "')";
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }


            }
            return View();
        }
        public ActionResult DownloadFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/Website_Data/ExcelTemplate/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "cancellationExcelSheet.xls");
            string fileName = "cancellationExcelSheet.xls";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult DeleteRecord_of_CancellationData()
        {
            string userid = User.Identity.GetUserId();
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(conn);
            string query = "Delete from cancellationimport where UserID=" + userid;
            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Import_CancellationData", "CancellationImport", new { area = "Admin" });
        }
        public ActionResult DisplayCancellationImportData()
        {
            List<CancellationImportData> cancellationimportdata = new List<CancellationImportData>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "Select * from cancellationimport";
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            cancellationimportdata.Add(new CancellationImportData()
                            {
                                date_of_cancelation = Convert.ToString(string.Format("{0:yyyy-MM-dd}", rdr["date_of_cancelation"])),
                                customer_name = Convert.ToString(rdr["customer_name"]),
                                customer_state = Convert.ToString(rdr["customer_state"]),
                                property_name = Convert.ToString(rdr["property_name"]),
                                property_city = Convert.ToString(rdr["property_city"]),
                                property_state = Convert.ToString(rdr["property_state"]),
                                accomodation_type = Convert.ToString(rdr["accomodation_type"]),
                                room_category_type = Convert.ToString(rdr["room_category_type"]),
                                check_in = Convert.ToString(string.Format("{0:yyyy-MM-dd HH:mm:ss}", rdr["check_in"])),
                                check_out = Convert.ToString(string.Format("{0:yyyy-MM-dd HH:mm:ss}", rdr["check_out"])),
                                guest_name = Convert.ToString(rdr["guest_name"]),
                                cancellation_code = Convert.ToInt32(rdr["cancellation_code"]),
                                UserID = Convert.ToInt32(rdr["UserID"]),
                                Itilite_booking_ID = Convert.ToInt32(rdr["Itilite_booking_ID"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(cancellationimportdata);
        }
    }
}