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
    public class CustomerMasterImportController : Controller
    {
        // GET: Admin/CustomerMasterImport
        public ActionResult Import_CustomerMasterData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Import_CustomerMasterData(HttpPostedFileBase file)
        {
            string userid = User.Identity.GetUserId();
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
                string deletequery = "Delete from customermasterimport where UserID=" + userid;
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand(deletequery, con);
                cmd1.ExecuteNonQuery();
                con.Close();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString() != "")
                    {

                       
                        string query = "Insert into customermasterimport(customer_name,full_address,customer_city,customer_state,customer_country,customer_email,customer_mobile,customer_type,customer_gst_no,pincode,UserID) Values('" +
                        ds.Tables[0].Rows[i][0].ToString() + 
                        "','" +
                        ds.Tables[0].Rows[i][1].ToString() +
                        "','" + 
                        ds.Tables[0].Rows[i][2].ToString() + 
                        "','" + 
                        ds.Tables[0].Rows[i][3].ToString() + 
                        "','" + 
                        ds.Tables[0].Rows[i][4].ToString() + 
                        "','" +
                        ds.Tables[0].Rows[i][5].ToString() +
                        "','" +
                        ds.Tables[0].Rows[i][6].ToString() +
                        "','" +
                        ds.Tables[0].Rows[i][7].ToString() +
                        "','" +
                        ds.Tables[0].Rows[i][8].ToString() +
                        "','" +
                         ds.Tables[0].Rows[i][9].ToString() +
                        "','" +
                        userid.ToString() + "')";
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
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "customermasterExcelSheet.xls");
            string fileName = "customermasterExcelSheet.xls";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        
        public ActionResult DisplayCustomerMasterData()
        {
            string userid = User.Identity.GetUserId();

            List<CustomerMasterImportData> customermasterimportdata = new List<CustomerMasterImportData>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "Select * from customermasterimport where userid = " + userid ;
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
                            customermasterimportdata.Add(new CustomerMasterImportData()
                            {
                                customer_name = Convert.ToString(rdr["customer_name"]),
                                full_address = Convert.ToString(rdr["full_address"]),
                                customer_city=Convert.ToString(rdr["customer_city"]),
                                customer_state = Convert.ToString(rdr["customer_state"]),
                                customer_country = Convert.ToString(rdr["customer_country"]),
                                customer_email=Convert.ToString(rdr["customer_email"]),
                                customer_mobile=Convert.ToString(rdr["customer_mobile"]),
                                customer_type=Convert.ToString(rdr["customer_type"]),
                                customer_gst_no = Convert.ToString(rdr["customer_gst_no"]),
                                pincode=Convert.ToString(rdr["pincode"]),
                                customermaster_code = Convert.ToInt32(rdr["customermaster_code"]),
                                UserID = Convert.ToInt32(rdr["UserID"]),
                                import_status=Convert.ToString(rdr["import_status"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(customermasterimportdata);
        }
        public ActionResult SaveData()
        {
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string userid = User.Identity.GetUserId();

            MySqlConnection con = new MySqlConnection(conn);
            string query1 = "Insert into country(Name) " +
                "Select upper(ltrim(rtrim(a.customer_country))) from customermasterimport a " +
                "left outer join country b on upper(ltrim(rtrim(b.name))) = upper(ltrim(rtrim(a.customer_country))) " +
                " and b.tamarind_flag is null and b.tbo_flag is null " +
                "where userid = " + userid + " " + 
                "and b.countryid is null  " +
                "group by upper(ltrim(rtrim(a.customer_country)));";

            string query2 = "Insert into state(Name,CountryId)  " +
                    "Select upper(ltrim(rtrim(customer_state))),1 countryid from customermasterimport a " +
                    "left outer join state d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.customer_state))) and d.tboflag is null " +
                    "and d.countryid = 1 where a.userid = " + userid + " " +
                    "and d.stateid is null " +
                    "group by upper(ltrim(rtrim(a.customer_state)));";

            string query3 = "Insert into city(Name,stateId)  " + 
                    "Select upper(ltrim(rtrim(a.customer_city))),c.stateId from customermasterimport a " +
                    "inner join state c on upper(ltrim(rtrim(a.customer_state))) = upper(ltrim(rtrim(c.name))) " +
                    "and c.tboflag is null " + 
                    "left outer join city d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.customer_city))) and c.stateid = d.stateid " +
                    "and d.tamarind_flag is null and d.tboflag is null " +
                    "where a.userid = " + userid + " " +
                    "and d.cityid is null " +
                    "group by c.stateid,a.customer_city;";




            // insert user for customer - one entry for each customer + city + state + country + email + mobile

            string query4 = "Insert into user(FirstName,UserType,status,Email)  " +
                    "select a.customer_name as Name,'7' as Type,'1' as status,trim(a.customer_email) as Email  " +
                    "from customermasterimport a   " +
                    "inner join state c on upper(ltrim(rtrim(c.name))) = upper(ltrim(rtrim(a.customer_state))) " +
                    "and c.tboflag is null  " +
                    "and c.countryid = 1  " +
                    "inner join city d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.customer_city)))   " +
                    "and d.tamarind_flag is null and d.tboflag is null and d.stateid = c.stateid " +
                    "LEFT OUTER join user e on e.firstname = a.customer_name and e.usertype = 7  " +
                    "and trim(e.email) = trim(a.customer_email) " +
                    "LEFT JOIN ADDRESS F ON F.COUNTRY = 1 AND F.STATE = C.STATEID AND F.CITYID = D.CITYID  " +
                    "AND F.USERID = E.USERID  " +
                    "where a.userid = " + userid + "  " +
                    "and trim(upper(a.customer_type)) = trim(upper('CUSTOMER'))  " +
                    "and isnull(E.USERID)  ";



            //"inner join country b on upper(b.name) = upper(a.customer_country) " +
            //            "and b.tamarind_flag is null and b.tbo_flag is null " +

            // insert address for customers - one for each unique customer
            string query6 = "insert into address(UserId,Address,Country,State,CityId,City,ZipCode,Mobile,Type) " +
                                "select e.userid,a.full_address,1 countryid,c.stateid,d.cityid,a.customer_city,a.pincode, " +
                                "a.customer_mobile,e.usertype as Type " +
                                "from customermasterimport a " +
                                "inner " +
                                "join user e on e.firstname = a.customer_name and e.usertype = 7 and trim(upper(e.email)) = trim(upper(a.customer_email)) " +
                                "inner join state c on upper(ltrim(rtrim(c.name))) = upper(ltrim(rtrim(a.customer_state))) " +
                                "and c.tboflag is null " +
                                "and c.countryid = 1 " +
                                "inner join city d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.customer_city))) " +
                                "and d.tamarind_flag is null and d.tboflag is null and d.stateid = c.stateid " +
                                "left outer join address f on f.userid = e.userid " +
                                "where a.userid = " + userid + "  " +
                                "and trim(upper(a.customer_type)) = trim(upper('Customer')) " +
                                "and isnull(f.userid) " +
                                "group by e.userid";


            //insert user for corporate  - only one for one name
            string query5 = "Insert into user(FirstName,UserType,status,Email)    " +
                                "select a.customer_name as Name,'5' as Type,'1' as status,a.customer_email as Email  " +
                                "from customermasterimport a   " +
                                "inner join state c on upper(ltrim(rtrim(c.name))) = upper(ltrim(rtrim(a.customer_state))) " +
                                "and c.tboflag is null  " +
                                "and c.countryid = 1 " +
                                "inner join city d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.customer_city)))  " +
                                "and d.tamarind_flag is null and d.tboflag is null and d.stateid = c.stateid " +
                                "LEFT OUTER join user e on e.firstname = a.customer_name and e.usertype = 5  " +
                                "and trim(upper(e.email)) = trim(upper(a.customer_email)) " +
                                "LEFT JOIN ADDRESS F ON F.COUNTRY = 1 AND F.STATE = C.STATEID AND F.CITYID = D.CITYID  " +
                                "AND F.USERID = E.USERID  " +
                                "where a.userid = " + userid + "  " +
                                "and trim(upper(a.customer_type))  = trim(upper('Corporate'))  " +
                                "and isnull(E.USERID) group by a.customer_name,a.customer_email ";





            string query7 = "insert into address(UserId,Address,Country,State,CityId,City,ZipCode,Mobile,Type) " +
                        "select e.userid,a.full_address,1 countryid,c.stateid,d.cityid,a.customer_city,a.pincode,  " +
                        "a.customer_mobile,e.usertype as Type " +
                        "from customermasterimport a " +
                        "left outer join user e on e.firstname = a.customer_name and e.usertype = 5 and trim(upper(e.email)) = trim(upper(a.customer_email)) " +
                        "inner join state c on upper(ltrim(rtrim(c.name))) = upper(ltrim(rtrim(a.customer_state)))  " +
                        "and c.tboflag is null " +
                        "and c.countryid = 1 " +
                        "inner join city d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.customer_city))) " +
                        "and d.tamarind_flag is null and d.tboflag is null and d.stateid=c.stateid " +
                        "left outer join address f on f.userid=e.userid "+
                        "where a.userid = " + userid + " " +
                        "and trim(upper(a.customer_type)) = trim(upper('Corporate')) and f.userid is null  " +
                        "group by e.userid;";

            //                        "left outer join address f on f.userid = e.userid " +


            //"inner join country b on upper(b.name) = upper(a.customer_country) " +
            //"and b.tamarind_flag is null and b.tbo_flag is null " +
            //"inner join state c on upper(c.name) = upper(a.customer_state) " +
            //"and c.tboflag is null " +
            //"and c.countryid = b.countryid " +
            //"inner join city d on upper(d.name) = upper(a.customer_city) " +
            //"and d.tamarind_flag is null and d.tboflag is null and d.stateid=c.stateid " +

                string query8 = "insert into b2b(B2BId,Name,VATRegNo) " +
                            "select e.userid,a.customer_name,a.customer_gst_no " +
                            "from customermasterimport a " +
                            "Left outer join user e on e.firstname = a.customer_name and e.usertype = 5 and e.email = a.customer_email " +
                            "left outer join b2b f on f.b2bid=e.userid "+
                            "where a.userid = " + userid + " " +
                            "and trim(upper(a.customer_type)) = trim(upper('Corporate')) and f.b2bid is null " +
                            "group by e.userid; ";

            //

            string query9 = "insert into offlinecustomergst_details(OfflinebookingCustomerId,GSTRegNO,Stateid,StateName, " +
                                "CityId,CityName,Address,PinCode,Type)  " +
                                "select e.userid,a.customer_gst_no,c.stateid,c.name as state_name,  " +
                                "d.cityid,d.name as city_name,  " +
                                "a.full_address,a.pincode,1 as type  " +
                                "from customermasterimport a  " +
                                "inner join user e on e.firstname = a.customer_name and e.usertype = 5 and e.email = a.customer_email  " +
                                "inner join state c on upper(ltrim(rtrim(c.name))) = upper(ltrim(rtrim(a.customer_state)))  " +
                                "and c.tboflag is null  " +
                                "and c.countryid = 1  " +
                                "inner join city d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.customer_city)))  " +
                                "and d.tamarind_flag is null and d.tboflag is null and d.stateid=c.stateid " +
                                "left outer join offlinecustomergst_details f on  " +
                                "f.offlinebookingcustomerid = e.userid and  " +
                                "ltrim(rtrim(upper(f.gstregno))) = ltrim(rtrim(upper(a.customer_gst_no)))  " +
                                "where a.userid = " + userid + "  " +
                                "and trim(upper(a.customer_type)) = trim(upper('Corporate'))  " +
                                "and isnull(f.offlinebookingcustomerid);"; 


            con.Open();
            MySqlCommand cmd = new MySqlCommand(query1, con);
            MySqlCommand cmd1 = new MySqlCommand(query2, con);
            MySqlCommand cmd2 = new MySqlCommand(query3, con);
            MySqlCommand cmd3 = new MySqlCommand(query4, con);
            MySqlCommand cmd4 = new MySqlCommand(query5, con);
            MySqlCommand cmd5 = new MySqlCommand(query6, con);
            MySqlCommand cmd6 = new MySqlCommand(query7, con);
            MySqlCommand cmd7 = new MySqlCommand(query8, con);
            MySqlCommand cmd8 = new MySqlCommand(query9, con);
            
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            cmd7.ExecuteNonQuery();
            cmd8.ExecuteNonQuery();

            cmd.CommandText = "delete from customermasterimport where customermaster_code in (select a.customermaster_code " +
                                "from customermasterimport a  " +
                                "inner join country b on upper(b.name) = upper(a.customer_country) " +
                                "and b.tamarind_flag is null and b.tbo_flag is null " +
                                "inner join state c on upper(c.name) = upper(a.customer_state) " +
                                "and c.tboflag is null  and c.countryid = b.countryid " +
                                "inner join city d on upper(d.name) = upper(a.customer_city) " +
                                "and d.tamarind_flag is null and d.tboflag is null and d.stateid = c.stateid " +
                                "LEFT OUTER join user e on e.firstname = a.customer_name and e.usertype = 7 " +
                                "and e.email = a.customer_email " +
                                "LEFT JOIN ADDRESS F ON F.COUNTRY = B.COUNTRYID AND F.STATE = C.STATEID " +
                                "AND F.CITYID = D.CITYID  AND F.USERID = E.USERID  " +
                                "where a.userid = " + userid + " " +
                                "and a.customer_type = trim(upper('CUSTOMER'))  and E.USERID is not null);";
            //cmd.ExecuteNonQuery();

            cmd.CommandText = "delete from customermasterimport where customermaster_code in (select a.customermaster_code  " +
                                "from customermasterimport a   " +
                                "inner join country b on upper(b.name) = upper(a.customer_country)  " +
                                "and b.tamarind_flag is null and b.tbo_flag is null  " +
                                "inner join state c on upper(c.name) = upper(a.customer_state)  " +
                                "and c.tboflag is null  " +
                                "and c.countryid = b.countryid  " +
                                "inner join city d on upper(d.name) = upper(a.customer_city)  " +
                                "and d.tamarind_flag is null and d.tboflag is null and d.stateid = c.stateid " +
                                "LEFT OUTER join user e on e.firstname = a.customer_name and e.usertype = 5  " +
                                "and e.email = a.customer_email " +
                                "LEFT JOIN ADDRESS F ON F.COUNTRY = B.COUNTRYID AND F.STATE = C.STATEID AND F.CITYID = D.CITYID  " +
                                "AND F.USERID = E.USERID  " +
                                "where a.userid = " + userid + "  " +
                                "and a.customer_type = trim(upper('Corporate'))  " +
                                "and E.USERID is not null group by a.customer_name,a.customer_email);";
            //cmd.ExecuteNonQuery();

            con.Close();
            return RedirectToAction("Import_CustomerMasterData", "CustomerMasterImport", new { area = "Admin" });
        }
    }
}