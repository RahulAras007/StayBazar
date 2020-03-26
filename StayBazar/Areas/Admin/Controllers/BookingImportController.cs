using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Web;
using System.Xml;
using StayBazar.Areas.Admin.Models;
//using System.Linq;
//using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace StayBazar.Areas.Admin.Controllers
{
    public class BookingImportController : Controller
    {

        public ActionResult Import_BookingData()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Import_BookingData(HttpPostedFileBase file)
        {
            string userid = User.Identity.GetUserId();
            DataSet ds = new DataSet();
            string lsz_bookingdate;
            string lsz_checkin;
            string lsz_checkout;
            string lsz_query;

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
                string deletequery = "Delete from bookingimport where UserID=" + userid;
                con.Open();
                MySqlCommand cmd1 = new MySqlCommand(deletequery, con);

                cmd1.ExecuteNonQuery();

                con.Close();
                string lsz_gst_state;
                string plosup;
                int hsncode;
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {


                    if (ds.Tables[0].Rows[i][0].ToString() != "")
                    {
                        string Place_of_supply = ds.Tables[0].Rows[i][30].ToString();
                        string SupPAN = ds.Tables[0].Rows[i][30].ToString();
                        lsz_gst_state = BLayer.Bookings.GetGstState(ds.Tables[0].Rows[i][6].ToString());
                        if (lsz_gst_state == null)
                        {
                            plosup = ds.Tables[0].Rows[i][3].ToString();
                            hsncode = 998552;
                        }
                        else
                        {
                            plosup = ds.Tables[0].Rows[i][6].ToString();
                            hsncode = 996311;
                        }
                        lsz_bookingdate = String.Format("{0:yyyy-MM-dd}", ds.Tables[0].Rows[i][0]);
                        lsz_checkin = String.Format("{0:yyyy-MM-dd HH:mm:ss}", ds.Tables[0].Rows[i][9]);
                        lsz_checkout = String.Format("{0:yyyy-MM-dd HH:mm:ss}", ds.Tables[0].Rows[i][10]);
                        lsz_query = "Insert into bookingimport(date_of_booking,itilite_booking_id,customer_name,customer_state,property_name,property_city,property_state,stay_option,accomodation_type,check_in,check_out,guest_name,no_of_room,no_of_nights,total_room_nights,base_buy_rate_INR,input_sgst,input_cgst,input_igst,total_buy_value,base_sell_rate,output_sgst,output_cgst,output_igst,total_sale_value_including_gst,hotel_confirmation_number,supplier,supplier_pan,property_address,property_country,property_gst_no,property_contact_person,property_contact_number,property_email_address,place_of_supply,hsn_code,UserID,data_from,data_from_email_address) Values('" +
                       lsz_bookingdate +
                       "','" + ds.Tables[0].Rows[i][1].ToString() +
                        "','" + ds.Tables[0].Rows[i][2].ToString() +
                        "','" + ds.Tables[0].Rows[i][3].ToString() +
                        "','" + ds.Tables[0].Rows[i][4].ToString() +
                        "','" + ds.Tables[0].Rows[i][5].ToString() +
                        "','" + ds.Tables[0].Rows[i][6].ToString() +
                        "','" + ds.Tables[0].Rows[i][7].ToString() +
                        "','" + ds.Tables[0].Rows[i][8].ToString() +
                        "','" + lsz_checkin +
                        "','" + lsz_checkout +
                        "','" + ds.Tables[0].Rows[i][11].ToString() +
                        "','" + ds.Tables[0].Rows[i][12].ToString() +
                        "','" + ds.Tables[0].Rows[i][13].ToString() +
                        "','" + ds.Tables[0].Rows[i][14].ToString() +
                        "','" + ds.Tables[0].Rows[i][15].ToString() +
                        "','" + ds.Tables[0].Rows[i][16].ToString() +
                        "','" + ds.Tables[0].Rows[i][17].ToString() +
                        "','" + ds.Tables[0].Rows[i][18].ToString() +
                        "','" + ds.Tables[0].Rows[i][19].ToString() +
                        "','" + ds.Tables[0].Rows[i][20].ToString() +
                        "','" + ds.Tables[0].Rows[i][21].ToString() +
                        "','" + ds.Tables[0].Rows[i][22].ToString() +
                        "','" + ds.Tables[0].Rows[i][23].ToString() +
                        "','" + ds.Tables[0].Rows[i][24].ToString() +
                        "','" + ds.Tables[0].Rows[i][25].ToString() +
                        "','" + ds.Tables[0].Rows[i][27].ToString() +
                        "','" + SupPAN.Substring(3, 11) +

                        "','" + ds.Tables[0].Rows[i][28].ToString() +
                        "','" + ds.Tables[0].Rows[i][29].ToString() +
                        "','" + ds.Tables[0].Rows[i][30].ToString() +
                        "','" + ds.Tables[0].Rows[i][31].ToString() +
                        "','" + ds.Tables[0].Rows[i][32].ToString() +
                        "','" + ds.Tables[0].Rows[i][33].ToString() +
                         "','" + plosup +
                          "','" + hsncode +
                        "','" + userid.ToString() +
                        "','" + ds.Tables[0].Rows[i][34].ToString() +
                        "','" + ds.Tables[0].Rows[i][35].ToString() +
                        "')";
                        con.Open();
                        MySqlCommand cmd = new MySqlCommand(lsz_query, con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                    }

                }
                con.Open();
                string lsz_query1 = "update bookingimport set corporate_user_id = (select u.userid from user u " +
"where u.firstname = bookingimport.customer_name and usertype = 5) " +
"where exists(select u1.userid from user u1 " +
"where u1.firstname = bookingimport.customer_name and usertype = 5) ";
                string lsz_query2 = "update bookingimport set customer_state_id=(select s.stateid from state s " +
"where s.name = bookingimport.customer_state and countryid = 1 and s.tboflag is null) " +
"where exists(select s1.name from state s1 " +
"where s1.name= bookingimport.customer_state and countryid = 1 and tboflag is null) ";
                string lsz_query3 = "update bookingimport set customer_GST_number=(select ocd.GSTRegNO from offlinecustomergst_details ocd " +
"where ocd.OfflinebookingCustomerId = bookingimport.corporate_user_id and ocd.Stateid = bookingimport.customer_state_id) " +
"where exists(select ocd1.GSTRegNO from offlinecustomergst_details ocd1 " +
"where ocd1.OfflinebookingCustomerId= bookingimport.corporate_user_id and ocd1.Stateid= bookingimport.customer_state_id)";
                string lsz_query4 = "update bookingimport set property_state_id=(select s.stateid from state s " +
"where s.name = bookingimport.property_state and countryid = 1 and s.tboflag is null) " +
"where exists(select s1.name from state s1 " +
"where s1.name= bookingimport.property_state and countryid = 1 and tboflag is null) ";
                MySqlCommand cmd2 = new MySqlCommand(lsz_query1, con);
                MySqlCommand cmd3 = new MySqlCommand(lsz_query2, con);
                MySqlCommand cmd4 = new MySqlCommand(lsz_query3, con);
                MySqlCommand cmd5 = new MySqlCommand(lsz_query4, con);
                cmd2.ExecuteNonQuery();
                cmd3.ExecuteNonQuery();
                cmd4.ExecuteNonQuery();
                cmd5.ExecuteNonQuery();
                con.Close();
            }


            return View();
        }
        public ActionResult DownloadFile()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "/Website_Data/ExcelTemplate/";
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + "bookingExcelSheet.xls");
            string fileName = "bookingExcelSheet.xls";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public ActionResult DisplayBookingData()
        {
            string userid = User.Identity.GetUserId();
            List<BookingImportData> bookingimportdata = new List<BookingImportData>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string query = "Select * from bookingimport where UserID=" + userid;
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
                            bookingimportdata.Add(new BookingImportData()
                            {


                                date_of_booking = Convert.ToString(string.Format("{0:yyyy-MM-dd}", rdr["date_of_booking"])),
                                customer_name = Convert.ToString(rdr["customer_name"]),
                                customer_state = Convert.ToString(rdr["customer_state"]),
                                property_name = Convert.ToString(rdr["property_name"]),
                                property_city = Convert.ToString(rdr["property_city"]),
                                property_state = Convert.ToString(rdr["property_state"]),
                                stay_option = Convert.ToString(rdr["stay_option"]),
                                accomodation_type = Convert.ToString(rdr["accomodation_type"]),
                                check_in = Convert.ToString(string.Format("{0:yyyy-MM-dd HH:mm:ss}", rdr["check_in"])),
                                check_out = Convert.ToString(string.Format("{0:yyyy-MM-dd HH:mm:ss}", rdr["check_out"])),
                                guest_name = Convert.ToString(rdr["guest_name"]),
                                no_of_room = Convert.ToInt16(rdr["no_of_room"]),
                                no_of_nights = Convert.ToInt16(rdr["no_of_nights"]),
                                total_room_nights = Convert.ToInt16(rdr["total_room_nights"]),
                                base_buy_rate_INR = Convert.ToDecimal(rdr["base_buy_rate_INR"]),
                                input_sgst = Convert.ToInt32(rdr["input_sgst"]),
                                input_cgst = Convert.ToInt32(rdr["input_cgst"]),
                                input_igst = Convert.ToInt32(rdr["input_igst"]),
                                total_buy_value = Convert.ToDecimal(rdr["total_buy_value"]),
                                base_sell_rate = Convert.ToDecimal(rdr["base_sell_rate"]),
                                output_sgst = Convert.ToInt32(rdr["output_sgst"]),
                                output_cgst = Convert.ToInt32(rdr["output_cgst"]),
                                output_igst = Convert.ToInt32(rdr["output_igst"]),
                                total_sale_value_including_gst = Convert.ToDecimal(rdr["total_sale_value_including_gst"]),
                                hotel_confirmation_number = Convert.ToString(rdr["hotel_confirmation_number"]),
                                supplier = Convert.ToString(rdr["supplier"]),
                                booking_code = Convert.ToInt32(rdr["booking_code"]),
                                property_address = Convert.ToString(rdr["property_address"]),
                                property_country = Convert.ToString(rdr["property_country"]),
                                property_gst_no = Convert.ToString(rdr["property_gst_no"]),
                                property_contact_person = Convert.ToString(rdr["property_contact_person"]),
                                property_contact_number = Convert.ToString(rdr["property_contact_number"]),
                                property_email_address = Convert.ToString(rdr["property_email_address"]),
                                UserID = Convert.ToInt32(rdr["UserID"]),
                                Itilite_booking_ID = Convert.ToString(rdr["Itilite_booking_ID"]),
                                import_status = Convert.ToString(rdr["import_status"]),
                                corporate_user_id = Convert.ToString(rdr["corporate_user_id"]),
                                customer_GST_number = Convert.ToString(rdr["customer_GST_number"]),
                                customer_state_id = Convert.ToString(rdr["customer_state_id"]),
                                data_from = Convert.ToString(rdr["data_from"]),
                                data_from_email_address = Convert.ToString(rdr["data_from_email_address"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(bookingimportdata);
        }

        public ActionResult SaveData()
        {
            string userid = User.Identity.GetUserId();
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            MySqlConnection con = new MySqlConnection(conn);
            string queryA = "Insert into country(Name) " +
"Select upper(ltrim(rtrim(a.property_country))) " +
"from bookingimport a " +
"left outer join country b on upper(ltrim(rtrim(b.name))) = upper(ltrim(rtrim(a.property_country))) " +
"and b.tamarind_flag is null and b.tbo_flag is null " +
"where userid = " + userid + " " +
" and b.countryid is null " +
"group by a.property_country;";

            string queryB = "Insert into state(Name,CountryId) " +
"Select upper(ltrim(rtrim(a.property_state))),1 countryid " +
"from bookingimport a " +
"left outer join state d on " +
"upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.property_state))) " +
"and d.tboflag is null and d.countryid = 1 " +
"where a.userid = " + userid + " " +
"and d.stateid is null " +
"group by upper(ltrim(rtrim(a.property_state))); ";

            string queryC = "Insert into city(Name,stateId) " +
"Select upper(ltrim(rtrim(a.property_city))),c.stateId " +
"from bookingimport a " +
"inner join state c on upper(ltrim(rtrim(a.property_state))) = upper(ltrim(rtrim(c.name))) " +
"and c.tboflag is null and c.countryid = 1 " +
"left outer join city d on upper(ltrim(rtrim(d.name))) = upper(ltrim(rtrim(a.property_city))) " +
"and c.stateid = d.stateid " +
"and d.tamarind_flag is null and d.tboflag is null " +
"where a.userid = " + userid + " " +
"and d.cityid is null " +
"group by upper(ltrim(rtrim(a.property_city))); ";


            string queryD = "Insert into user(FirstName,UserType,status,Email) " +
"select trim(a.data_from) as Name,'3' as Type,'1' as status,trim(a.data_from_email_address) as Email " +
"from bookingimport a " +
"LEFT OUTER join user e on trim(e.firstname) = trim(a.data_from) and e.usertype = 3 " +
"and trim(e.email) = trim(a.data_from_email_address) " +
"where a.userid = " + userid + " " +
"and isnull(E.USERID) " +
"group by trim(a.data_from),trim(a.data_from_email_address)";


            string queryE = "insert into b2b(b2bId,name,UserCode, " +
"MarkupPercent, CommissionPercent, servicetaxregno, vatregno, " +
"maximumstaff, availableproperties, requeststatus, approvaldate, panno, " +
"iscreditbooking, creditdays, creditamount, totalcreditamount, corpbookingtodayexp) " +
"select e.UserId,trim(a.data_from), '-' UserCode, " +
"0 MarkupPercent,0 CommissionPercent, '-' servicetaxregno,'-' vatregno, " +
"0 maximumstaff,0 availableproperties,9 requeststatus,curdate() approvaldate,'-' panno, " +
"0 iscreditbooking,60 creditdays,0 creditamount,0 totalcreditamount, curdate() corpbookingtodayexp " +
"from bookingimport a " +
"Join user e on trim(e.firstname) = trim(a.data_from) and e.usertype = 3 " +
"and trim(e.email) = trim(a.data_from_email_address) " +
"left outer Join b2b ad on ad.b2bId = e.UserId " +
"where isnull(ad.b2bId) and a.userid = " + userid + " " +
"group by trim(a.data_from),trim(a.data_from_email_address); ";

            string queryF = "insert into address(UserId,Address,Country,State,CityId,City,Mobile,Type) " +
"select e.userid,a.data_from address,1 countryid,a.property_state_id state, d.cityid,a.property_city city, " +
"0 mobile,e.usertype as Type " +
"from bookingimport a " +
"inner " +
"join user e on trim(e.firstname) = trim(a.data_from) and e.usertype = 3 and " +
"trim(e.email) = trim(a.data_from_email_address) " +
"inner join city d on upper(d.name) = upper(a.property_city) " +
"and d.tamarind_flag is null and d.tboflag is null and d.stateid = a.property_state_id " +
"left outer join address f on f.userid = e.userid " +
"where a.userid = " + userid + " " +
"and isnull(f.userid) " +
"group by e.userid; ";


            string queryG = "insert into property(Title,Location,Status,OwnerId,Address,Country,State,City, " +
"CityId,Email,Mobile,PropertyFor,GSTRegistrationNo) " +
"select trim(a.property_name),left(a.property_address,200),1,u.userid ownerid, a.property_address, " +
"1 countryid,a.property_state_id,ci.name,ci.cityid, " +
"trim(a.property_email_address),a.property_contact_number, " +
"u.usertype,a.property_gst_no " +
"from bookingimport a " +
"inner join user u on trim(u.firstname) = trim(a.data_from) and u.usertype = 3 " +
"and trim(u.email) = trim(a.data_from_email_address) " +
"inner join city ci on upper(ltrim(rtrim(ci.name)))= upper(ltrim(rtrim(a.property_city))) " +
"and ci.tamarind_flag is null and ci.tboflag is null and ci.stateid = a.property_state_id " +
"left outer join property p on p.OwnerId = u.userid and " +
"ltrim(rtrim(upper(p.Title))) = ltrim(rtrim(upper(a.property_name))) " +
"and p.country = 1 and p.state = a.property_state_id and p.cityid = ci.cityid " +
"and trim(p.GSTRegistrationNo) = trim(a.property_gst_no) " +
"where a.userid = " + userid + " " +
"and isnull(p.ownerid) " +
"group by u.userid,trim(a.property_name),trim(a.property_state),trim(a.property_city),trim(a.property_gst_no); ";

            string queryH = "INSERT INTO ACCOMMODATIONTYPE (TITLE) " +
"SELECT A.accomodation_type TITLE FROM BOOKINGIMPORT A " +
"LEFT OUTER JOIN ACCOMMODATIONTYPE B ON TRIM(A.ACCOMODATION_TYPE) = TRIM(B.TITLE) " +
"WHERE ISNULL(ACCOMMODATIONTYPEID) " +
"GROUP BY ACCOMODATION_TYPE ";

            string queryL = "INSERT INTO staycategory (Title) " +
"SELECT A.stay_option TITLE FROM BOOKINGIMPORT A " +
"LEFT OUTER JOIN staycategory B ON TRIM(A.stay_option) = TRIM(B.TITLE) " +
"WHERE ISNULL(CategoryId) " +
"GROUP BY stay_option";
            string queryI = "create temporary table bookingimportids " +
"select booking_code, a.loginuserid from bookingimport_offline_bookings_vu a " +
"where a.loginuserid = " + userid;

            string queryJ = "update bookingimport set import_status = 'Y' " +
"where booking_code in (select booking_code from bookingimportids where loginuserid = " + userid + " ) ";

            string queryK = "drop table bookingimportids ";

            string queryM = "insert into offlinecustomergst_details(offlinebookingcustomerid,gstregno,stateid,statename,type) " +
"select uc.userid as offlinebookingcustomerid,'0' as gstregno,a.customer_state_id as stateid,customer_state as state_name,1 as type " +
"from bookingimport a " +
"inner " +
"join user uc on uc.FirstName = a.customer_name and uc.usertype = 5 " +
"left join offlinecustomergst_details b on uc.userid = b.offlinebookingcustomerid " +
"and a.customer_state_id = b.stateid " +
"where isnull(b.offlinebookingcustomerid) and a.userid = " + userid + " " +
"group by b.offlinebookingcustomerid,a.customer_state_id ";

            string query = "insert into offline_bookings(UserId,Guestname,PropertyId,SupplierId,CheckIn_date, " +
"CheckOut_date,Noofnight,NoOfRooms,StayCategoryId,AccommodatoinTypeId,AccommodatoinTypeName, " +
"AvgDailyRateBefreStaxForBuyPrice,StaxForBuyPrice,TotalAmtForBuyPrice,TotalBuyPrice, " +
"AvgDailyRateBefreStaxForSalePrice,StaxForSalePrice,TotalAmtForSalePrice,TotalSalePrice " +
",CreatedDate,IsSendCustomerMail,IsSendSupplierMail,SaveStatus,HotelConformationNo, " +
"booking_creationdate,CreatedBy,SalesPersonId,SalesRegion,CustomerReferenceNo, " +
"IsGST,SBEntityEntityId,SBEntityEntityIdBilling,PropertyGstRegNo,booking_code,HSNCode, " +
"CustomerGstRegNo,CustomerGstStateId,FromCustomerId,FromCustomer,PlaceOfSupply,CustomerPaymentMode ) " +
"select customeruserid, guest_name, PropertyId, supplieruserid, " +
"check_in, check_out, no_of_nights, no_of_room, CategoryId, " +
"AccommodationTypeId, accomodation_type, base_buy_rate_INR, " +
"BuyGST, total_buy_value, total_buy_value, base_sell_rate, " +
"SellGST, total_sale_value_including_gst, totalsalevalueincludinggst, date_of_booking, " +
"1 as IsSendCustomerMail,1 as IsSendSupplierMail,5 as SaveStatus, " +
"hotel_confirmation_number, dateofbooking, loginuserid,2043 as createduserid, " +
"'North' as salesregion, itilite_booking_id,2 as ISGST, " +
"SBEntityEntityId, SBEntityEntityIdBilling, " +
"property_gst_no, booking_code, " +
"HSNcode, GSTRegNO, Stateid, UserId,1 as fromcustomer ,POSStateID,1 " +
"from bookingimport_offline_bookings_vu " +
"where loginuserid = " + userid;

            string query1 = "insert into offlinebooking_property(Offline_BookingId,PropertyName, " +
"PropertyAddress,PropertyCityname,PropertyCity,PropertyState,PropertyCountry, " +
"PropertyContactNo,PropertyCaretakerName,PropertyEmail,SupplierName,SupplierAddress,SupplierCityname, " +
"SupplierCity,SupplierState,SupplierCountry,SupplierMobile,SupplierEmail) " +
"select ofb.Offline_BookingId as OfflineBookingId,pr.title as PropertyName, " +
"pr.address as PropertyAddress,pr.City as propertyCity_Name,pr.CityId as Property_cityId, " +
"pr.State as PropertyState,pr.Country as PropertyCountry,pr.mobile as PropertyMobile, " +
"bimp.property_contact_person as ContactPerson, " +
"pr.email as PropertyEmail,bimp.supplier as Supplier, " +
"pr.Address as SupplierAddress,pr.City as SupplierCity,pr.CityId as SupplierCityId, " +
"pr.State as SupplierState,pr.Country as SupplierCountry,pr.Mobile, " +
"pr.Email " +
"from bookingimport bimp " +
"inner join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"inner join property pr on pr.PropertyId = ofb.PropertyId " +
"and pr.country = 1 " +
"and ltrim(rtrim(upper(bimp.property_name))) = ltrim(rtrim(upper(pr.Title))) " +
"and pr.state = bimp.property_state_id " +
"and trim(pr.GSTRegistrationNo) = trim(bimp.property_gst_no) " +
"left outer join offlinebooking_property ofp on ofb.offline_bookingid = ofp.offline_bookingid " +
"where bimp.userid = " + userid + " " +
"and isnull(ofp.offline_bookingid) ";
            string query2 = "insert into offlinebooking_customer(Offline_BookingId,Name,Email,Mobile,Address,CountryId, " +
"StateId,cityId,City,CustomerType,ContactPerson,MobileNO,GuestName,CustomerPinCode,BillingAddress,BillingStateId,BillingcityId,BillingCountryId,BillingPINCode) " +
"select ofb.Offline_BookingId,bimp.customer_name,u.Email,addr.Mobile,addr.Address, " +
"addr.Country,addr.State,addr.CityId,addr.City,u.UserType,bimp.property_contact_person,addr.Mobile, " +
"ofb.Guestname,addr.ZipCode,ocd.Address,ocd.Stateid,ocd.CityId,1,ocd.PinCode " +
"from bookingimport bimp " +
"inner " +
"join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"inner " +
"join user u on u.UserId = ofb.UserId and u.UserType = 5 " +
"inner join address addr on addr.UserId = u.UserId " +
"left outer join offlinecustomergst_details ocd on ocd.GSTRegNO=ofb.CustomerGstRegNo " +
"and ocd.OfflinebookingCustomerId=ofb.UserId and ocd.Stateid=ofb.CustomerGstStateId " +
"left outer join offlinebooking_customer obc on obc.offline_bookingid = ofb.offline_bookingid " +
"where u.UserType = 5 and bimp.userid = " + userid + " " +
"and isnull(obc.offline_bookingid)";
            string query3 = "insert into bookingdetails(Offline_BookingId,CheckIn_date,CheckOut_date,Noofnight, " +
"NoOfRooms,StayCategoryId,AccommodatoinTypeId,AccommodatoinTypeName, " +
"AvgDailyRateBefreStaxForBuyPrice,StaxForBuyPrice,TotalAmtForBuyPrice,TotalBuyPrice, " +
"AvgDailyRateBefreStaxForSalePrice,StaxForSalePrice,TotalAmtForSalePrice, " +
"TotalSalePrice,HotelConformationNo,Guestname,HSNCode,PlaceOfSupply,Guestemail) " +
"select ofb.Offline_BookingId,bimp.check_in,bimp.check_out,bimp.no_of_nights, " +
"bimp.no_of_room,st.CategoryId,acct.AccommodationTypeId,bimp.accomodation_type, " +
"bimp.base_buy_rate_INR, " +
"if((bimp.input_sgst + bimp.input_cgst + bimp.input_igst)<>0,Round(((bimp.input_sgst + bimp.input_cgst + bimp.input_igst) * 100) / (total_buy_value - bimp.input_sgst - bimp.input_cgst - bimp.input_igst )),0 )as BuyGST, " +
"bimp.total_buy_value,bimp.total_buy_value,bimp.base_sell_rate, " +
"if((bimp.output_sgst + bimp.output_cgst + bimp.output_igst)<>0,Round(((bimp.output_sgst + bimp.output_cgst + bimp.output_igst) * 100) / (total_sale_value_including_gst - bimp.output_sgst - bimp.output_cgst - bimp.output_igst)),18)as SellGST, " +
"bimp.total_sale_value_including_gst,bimp.total_sale_value_including_gst, " +
"bimp.hotel_confirmation_number,bimp.guest_name,ofb.HSNCode,ofb.PlaceOfSupply,bimp.data_from_email_address " +
"from bookingimport bimp " +
"Join accommodationtype_vu acct on acct.Title = bimp.accomodation_type " +
"Join staycategory st on st.Title = bimp.stay_option " +
"Join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"left outer join bookingdetails bds on bds.offline_bookingid = ofb.offline_bookingid " +
"where " +
"bimp.userid = " + userid + " " +
"and isnull(bds.offline_bookingid) ";
            string query4 = "insert into offline_bookings_taxes_gst(offline_Id,taxTitle,taxPercentage,taxType, " +
"isLastUpdate,BookingId,TYPE) " +
"select ofb.Offline_BookingId,'SGST' as Title, " +
"ROUND(ofb.StaxForBuyPrice/2) as Percentage,'GST' as TaxType, " +
"1 as LastUpdated,bd.BookingId as BookingId,'Buy price' as Type " +
"from bookingimport bimp " +
"Join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"Join bookingdetails bd on bd.Offline_BookingId = ofb.Offline_BookingId " +
"where bimp.input_igst = 0 and bimp.userid = " + userid;
            string query5 = "insert into offline_bookings_taxes_gst(offline_Id,taxTitle,taxPercentage,taxType, " +
"isLastUpdate,BookingId,TYPE) " +
"select ofb.Offline_BookingId,'CGST' as Title, " +
"ROUND(ofb.StaxForBuyPrice/2) as Percentage,'GST' as TaxType, " +
"1 as LastUpdated,bd.BookingId as BookingId,'Buy price' as Type " +
"from bookingimport bimp " +
"Join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"Join bookingdetails bd on bd.Offline_BookingId = ofb.Offline_BookingId " +
"where bimp.input_igst = 0 and bimp.userid = " + userid;
            string query6 = "insert into offline_bookings_taxes_gst(offline_Id,taxTitle,taxPercentage,taxType, " +
"isLastUpdate,BookingId,TYPE) " +
"select ofb.Offline_BookingId,'IGST' as Title,ROUND(ofb.StaxForBuyPrice) as Percentage,'GST' as TaxType, " +
"1 as LastUpdated,bd.BookingId as BookingId,'Buy price' as Type " +
"from bookingimport bimp " +
"Join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"Join bookingdetails bd on bd.Offline_BookingId = ofb.Offline_BookingId " +
"where bimp.input_igst <> 0 and bimp.userid = " + userid;
            string query7 = "insert into offline_bookings_taxes_gst(offline_Id,taxTitle,taxPercentage,taxType, " +
"isLastUpdate,BookingId,TYPE) " +
"select ofb.Offline_BookingId,'SGST' as Title, " +
"ROUND(ofb.StaxForSalePrice/2) as Percentage,'GST' as TaxType, " +
"1 as LastUpdated,bd.BookingId as BookingId,'Sale price' as Type " +
"from bookingimport bimp " +
"Join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"Join bookingdetails bd on bd.Offline_BookingId = ofb.Offline_BookingId " +
" where bimp.output_igst = 0 and bimp.userid = " + userid;
            string query8 = "insert into offline_bookings_taxes_gst(offline_Id,taxTitle,taxPercentage,taxType,isLastUpdate,BookingId,TYPE) " +
"select ofb.Offline_BookingId,'CGST' as Title, " +
"ROUND(ofb.StaxForSalePrice/2) as Percentage,'GST' as TaxType, " +
"1 as LastUpdated,bd.BookingId as BookingId,'Sale price' as Type " +
"from bookingimport bimp " +
"Join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"Join bookingdetails bd on bd.Offline_BookingId = ofb.Offline_BookingId " +
"where bimp.output_igst = 0 and bimp.userid = " + userid;
            string query9 = "insert into offline_bookings_taxes_gst(offline_Id,taxTitle,taxPercentage,taxType, " +
"isLastUpdate,BookingId,TYPE) " +
"select ofb.Offline_BookingId,'IGST' as Title, " +
"ROUND(ofb.StaxForSalePrice) as Percentage,'GST' as TaxType, " +
"1 as LastUpdated,bd.BookingId as BookingId,'Sale price' as Type " +
"from bookingimport bimp " +
"Join offline_bookings ofb on ofb.booking_code = bimp.booking_code " +
"Join bookingdetails bd on bd.Offline_BookingId = ofb.Offline_BookingId " +
"where bimp.output_igst <> 0 and bimp.userid = " + userid;
            string query10 = "UPDATE OFFLINE_BOOKINGS SET ORDERNO = CONCAT('ROB',RIGHT(CONCAT('00000',HEX(OFFLINE_BOOKINGID+1000)),5)) WHERE ORDERNO IS NULL AND BOOKING_CODE IS NOT NULL ";
            con.Open();
            MySqlCommand cmdA = new MySqlCommand(queryA, con);
            MySqlCommand cmdB = new MySqlCommand(queryB, con);
            MySqlCommand cmdC = new MySqlCommand(queryC, con);
            MySqlCommand cmdD = new MySqlCommand(queryD, con);
            MySqlCommand cmdE = new MySqlCommand(queryE, con);
            MySqlCommand cmdF = new MySqlCommand(queryF, con);
            MySqlCommand cmdG = new MySqlCommand(queryG, con);
            MySqlCommand cmdH = new MySqlCommand(queryH, con);
            MySqlCommand cmdL = new MySqlCommand(queryL, con);
            MySqlCommand cmdI = new MySqlCommand(queryI, con);
            MySqlCommand cmdJ = new MySqlCommand(queryJ, con);
            MySqlCommand cmdK = new MySqlCommand(queryK, con);
            MySqlCommand cmdM = new MySqlCommand(queryM, con);



            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlCommand cmd1 = new MySqlCommand(query1, con);
            MySqlCommand cmd2 = new MySqlCommand(query2, con);
            MySqlCommand cmd3 = new MySqlCommand(query3, con);
            MySqlCommand cmd4 = new MySqlCommand(query4, con);
            MySqlCommand cmd5 = new MySqlCommand(query5, con);
            MySqlCommand cmd6 = new MySqlCommand(query6, con);
            MySqlCommand cmd7 = new MySqlCommand(query7, con);
            MySqlCommand cmd8 = new MySqlCommand(query8, con);
            MySqlCommand cmd9 = new MySqlCommand(query9, con);
            MySqlCommand cmd10 = new MySqlCommand(query10, con);


            cmdA.ExecuteNonQuery();
            cmdB.ExecuteNonQuery();
            cmdC.ExecuteNonQuery();
            cmdD.ExecuteNonQuery();
            cmdE.ExecuteNonQuery();
            cmdF.ExecuteNonQuery();
            cmdG.ExecuteNonQuery();
            cmdH.ExecuteNonQuery();
            cmdL.ExecuteNonQuery();
            cmdI.ExecuteNonQuery();
            cmdJ.ExecuteNonQuery();
            cmdK.ExecuteNonQuery();
            cmdM.ExecuteNonQuery();


            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            cmd2.ExecuteNonQuery();
            cmd3.ExecuteNonQuery();
            cmd4.ExecuteNonQuery();
            cmd5.ExecuteNonQuery();
            cmd6.ExecuteNonQuery();
            cmd7.ExecuteNonQuery();
            cmd8.ExecuteNonQuery();
            cmd9.ExecuteNonQuery();
            cmd10.ExecuteNonQuery();
            con.Close();

            return RedirectToAction("Import_BookingData", "BookingImport", new { area = "Admin" });
        }


    }
}