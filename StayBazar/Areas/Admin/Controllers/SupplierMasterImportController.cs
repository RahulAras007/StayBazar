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
    public class SupplierMasterImportController : Controller
    {
        // GET: Admin/SupplierMasterImport
        public ActionResult Import_SupplierMasterData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Import_SupplierMasterData(HttpPostedFileBase file)
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
                string deletequery = "Delete from suppliermasterimport where UserID=" + userid;
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand(deletequery, con);
                cmd1.ExecuteNonQuery();
                con.Close();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (ds.Tables[0].Rows[i][0].ToString() != "")
                    {
                        string query = "Insert into suppliermasterimport(supplier_name,property_name,supplier_PAN,full_address,supplier_city,supplier_state,supplier_country,supplier_gst_no,supplier_contact_person,supplier_contact_number,supplier_email_address,UserID) Values('" +
                        ds.Tables[0].Rows[i][0].ToString() +
                        "','" + ds.Tables[0].Rows[i][1].ToString() +
                        "','" + ds.Tables[0].Rows[i][2].ToString() +
                        "','" + ds.Tables[0].Rows[i][3].ToString() +
                        "','" + ds.Tables[0].Rows[i][4].ToString() +
                        "','" + ds.Tables[0].Rows[i][5].ToString() +
                        "','" + ds.Tables[0].Rows[i][6].ToString() +
                        "','" + ds.Tables[0].Rows[i][7].ToString() +
                        "','" + ds.Tables[0].Rows[i][8].ToString() +
                        "','" + ds.Tables[0].Rows[i][9].ToString() +
                        "','" + ds.Tables[0].Rows[i][10].ToString() +
                         "','" + userid.ToString() +
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
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "suppliermasterExcelSheet.xls");
            string fileName = "suppliermasterExcelSheet.xls";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public ActionResult DisplaySupplierMasterData()
        {
            string userid = User.Identity.GetUserId();
            List<SupplierMasterImportData> suppliermasterimportdata = new List<SupplierMasterImportData>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "Select * from suppliermasterimport where userid = " + userid;
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
                            suppliermasterimportdata.Add(new SupplierMasterImportData()
                            {


                                supplier_name = Convert.ToString(rdr["supplier_name"]),
                                property_name = Convert.ToString(rdr["property_name"]),
                                full_address = Convert.ToString(rdr["full_address"]),
                                supplier_state = Convert.ToString(rdr["supplier_state"]),
                                supplier_country = Convert.ToString(rdr["supplier_country"]),
                                supplier_gst_no = Convert.ToString(rdr["supplier_gst_no"]),
                                supplier_contact_person = Convert.ToString(rdr["supplier_contact_person"]),
                                supplier_contact_number = Convert.ToInt64(rdr["supplier_contact_number"]),
                                supplier_email_address = Convert.ToString(rdr["supplier_email_address"]),
                                supplier_master_code = Convert.ToInt32(rdr["supplier_master_code"]),
                                UserID = Convert.ToInt32(rdr["UserID"]),
                                supplier_PAN = Convert.ToString(rdr["supplier_PAN"]),
                                supplier_city = Convert.ToString(rdr["supplier_city"]),
                                import_status = Convert.ToString(rdr["import_status"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(suppliermasterimportdata);
        }

        public ActionResult SaveData()
        {
            string userid = User.Identity.GetUserId();
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(conn);
            string query = "Insert into country(Name)  " +
                            "Select upper(ltrim(rtrim(a.supplier_country))) from suppliermasterimport a " +
                            "left outer join country b on upper(ltrim(rtrim(b.name))) = upper(ltrim(rtrim(a.supplier_country))) " +
                            "and b.tamarind_flag is null and b.tbo_flag is null " +
                            "where userid = " + userid + " " +
                            "and b.countryid is null " +
                            "group by a.supplier_country; ";


            string query1 = "Insert into state(Name,CountryId)  "+
                            "Select upper(ltrim(rtrim(supplier_state))),c.CountryId " +
                            "from suppliermasterimport a " +
                            "inner join country c on upper(ltrim(rtrim(a.supplier_country))) = upper(ltrim(rtrim(c.name))) " +
                            "and c.tamarind_flag is null and c.tbo_flag is null " +
                            "left outer join state d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.supplier_state))) and d.tboflag is null " +
                            "and c.countryid = d.countryid where a.userid = " + userid + " " +
                            "and d.stateid is null " +
                            "group by c.countryid,d.stateid,a.supplier_state; ";


            string query2 = "Insert into city(Name,stateId) "+
                            "Select upper(ltrim(rtrim(a.supplier_city))),c.stateId " +
                            "from suppliermasterimport a " +
                            "inner join state c on upper(ltrim(rtrim(a.supplier_state))) = upper(ltrim(rtrim(c.name))) " +
                            "and c.tboflag is null " +
                            "left outer join city d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.supplier_city))) and c.stateid = d.stateid " +
                            "and d.tamarind_flag is null and d.tboflag is null " +
                            "where a.userid = " + userid + " " +
                            "and d.cityid is null " +
                            "group by c.stateid,d.cityid,a.supplier_city; ";


            string query3 = "Insert into user(FirstName,UserType,status,Email) "+
                            "select a.supplier_name as Name,'3' as Type,'1' as status,a.supplier_email_address as Email " +
                            "from suppliermasterimport a " +
                            "inner " +
                            "join country b on upper(b.name) = upper(a.supplier_country) " +
                            "and b.tamarind_flag is null and b.tbo_flag is null " +
                            "inner join state c on upper(c.name) = upper(a.supplier_state) " +
                            "and c.tboflag is null " +
                            "and c.countryid = b.countryid " +
                            "inner join city d on upper(d.name) = upper(a.supplier_city) " +
                            "and d.tamarind_flag is null and d.tboflag is null and d.stateid = c.stateid " +
                            "LEFT OUTER join user e on e.firstname = a.supplier_name and e.usertype = 3 " +
                            "and e.email = a.supplier_email_address " +
                            "LEFT JOIN ADDRESS F ON F.COUNTRY = B.COUNTRYID AND F.STATE = C.STATEID AND F.CITYID = D.CITYID " +
                            "AND F.USERID = E.USERID " +
                            "where a.userid = " + userid + " " +
                            "and isnull(E.USERID) group by a.supplier_name,a.supplier_email_address";


            string query6 = "insert into b2b(b2bId,name,UserCode, "+
                            "MarkupPercent, CommissionPercent, servicetaxregno, vatregno, " +
                            "maximumstaff, availableproperties, requeststatus, approvaldate, panno, " +
                            "iscreditbooking, creditdays, creditamount, totalcreditamount, corpbookingtodayexp) " +
                            "select u.UserId,max(sup.supplier_name) supplier_name, '-' UserCode, " +
                            "0 MarkupPercent,0 CommissionPercent, '-' servicetaxregno,'-' vatregno, " +
                            "0 maximumstaff,0 availableproperties,9 requeststatus,curdate() approvaldate,'-' panno, " +
                            "0 iscreditbooking,60 creditdays,0 creditamount,0 totalcreditamount, curdate() corpbookingtodayexp " +
                            "from user u " +
                            "Join suppliermasterimport sup on u.FirstName = sup.supplier_name " +
                            "left outer Join b2b ad on ad.b2bId = u.UserId " +
                            "where u.UserType = 3  and isnull(ad.b2bId) " +
                            "Group By u.UserId";



            string query4 = "insert into address(UserId,Address,Country,State,CityId,City,Mobile,Type) "+
                            "select e.userid,a.full_address,b.countryid,c.stateid,d.cityid,a.supplier_city, " +
                            "a.supplier_contact_number,e.usertype as Type " +
                            "from suppliermasterimport a " +
                            "inner " +
                            "join user e on e.firstname = a.supplier_name and e.usertype = 3 and e.email = a.supplier_email_address " +
                            "inner join country b on upper(b.name) = upper(a.supplier_country) " +
                            "and b.tamarind_flag is null and b.tbo_flag is null " +
                            "inner join state c on upper(c.name) = upper(a.supplier_state) " +
                            "and c.tboflag is null " +
                            "and c.countryid = b.countryid " +
                            "inner join city d on upper(d.name) = upper(a.supplier_city) " +
                            "and d.tamarind_flag is null and d.tboflag is null and d.stateid = c.stateid " +
                            "left outer join address f on f.userid = e.userid " +
                            "where a.userid = " + userid + " " +
                            "and isnull(f.userid) " +
                            "group by e.userid; ";


            string query5 = "insert into property(Title,Location,Status,OwnerId,Address,Country,State,City, "+
                            "CityId,Email,Mobile,PropertyFor,GSTRegistrationNo) " +
                            "select a.property_name,a.full_address,1,u.userid,a.full_address,co.countryid, " +
                            "s.stateid,ci.name,ci.cityid,a.supplier_email_address,a.supplier_contact_number, " +
                            "u.usertype,a.supplier_gst_no " +
                            "from suppliermasterimport a " +
                            "inner join user u on u.firstname = a.supplier_name and u.usertype = 3 and u.email = a.supplier_email_address " +
                            "inner join country co on upper(ltrim(rtrim(co.name)))= upper(ltrim(rtrim(a.supplier_country))) " +
                            "and co.tamarind_flag is null and co.tbo_flag is null " +
                            "inner join state s on upper(ltrim(rtrim(s.name))) = upper(ltrim(rtrim(a.supplier_state))) " +
                            "and s.tboflag is null and s.countryid = co.countryid " +
                            "inner join city ci on upper(ltrim(rtrim(ci.name)))= upper(ltrim(rtrim(a.supplier_city))) " +
                            "and ci.tamarind_flag is null and ci.tboflag is null and ci.stateid = s.stateid " +
                            "left outer join property p on " +
                            "p.OwnerId = u.userid and " +
                            "ltrim(rtrim(upper(p.Title))) = ltrim(rtrim(upper(a.property_name))) " +
                            "where a.userid = " + userid + " " +
                            "and isnull(p.ownerid) group by u.userid,a.property_name; ";



            con.Open();
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlCommand cmd1 = new MySqlCommand(query1, con);
            MySqlCommand cmd2 = new MySqlCommand(query2, con);
            MySqlCommand cmd3 = new MySqlCommand(query3, con);
            MySqlCommand cmd6 = new MySqlCommand(query6, con);
            MySqlCommand cmd4 = new MySqlCommand(query4, con);
            MySqlCommand cmd5 = new MySqlCommand(query5, con);
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("Import_SupplierMasterData", "SupplierMasterImport", new { area = "Admin" });
        }
    }
}