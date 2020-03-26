using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataLayer.DataProvider;

namespace DataLayer
{
    public class ExcelImport : DataLink
    {
        public string FixCSV(string data)
        {
            if (data == null || data == "") return "";
            data = data.Replace("  ", " ");
            data = data.Replace(", ,", ",");
            string[] items = data.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            int i, len;
            len = items.Length;
            for (i = 0; i < len; i++)
            {
                if (items[i] != null && items[i] != "") items[i] = items[i].Trim();
            }
            string result = string.Join(",", items);
            i = result.LastIndexOf(",");
            if (i > 0)
            {
                if (i == (result.Length - 1))
                {
                    result = result.Substring(0, i);
                }
            }
            return result;
        }
        private string CutString(object data, int length)
        {
            string text = Connection.ToString(data);
            text = text.Trim();
            if (text.Length <= length)
            {
                return text;
            }
            else
            {
                return text.Substring(0, length);
            }

        }
        //public string FixCSV(string data)
        //{
            
        //}

        public int ImportData()
        {
            //List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            //DataPlug.Parameter pr = Connection.GetParameter("pResult", DataPlug.DataType._Int,0);
            //pr.Direction = ParameterDirection.InputOutput;
            //param.Add(pr);
            //Connection.ExecuteQuery("importToDatabase", param);
            object obj =  Connection.ExecuteQueryScalar("importToDatabase");
            return Connection.ToInteger(obj);
           // return Connection.ToInteger(pr.Value);
        }
        public string GetTheCounts()
        {
            string sql = "SELECT (SELECT COUNT(*) FROM supplierdetails_excel) AS sups, (SELECT COUNT(*) FROM propertydetails_excel) AS prps, (SELECT COUNT(*) FROM accommodations_excel) AS accs ";
            DataTable dt = Connection.GetSQLTable(sql);
            if (dt.Rows.Count > 0)
            {
                string result;
                result = Connection.ToString(dt.Rows[0]["sups"]);
                result = result + "," + Connection.ToString(dt.Rows[0]["prps"]);
                result = result + "," + Connection.ToString(dt.Rows[0]["accs"]);
                return result;
            }
            return "";
        }
        public string ImportToDBFromExcel(DataTable dtSupplierDetails, DataTable dtPorpertyDetails, DataTable dtAccomodationDetails)
        {
            double ara = 0;
            int x;
            if (dtSupplierDetails.Rows.Count > 0)
            {
                string sql = "DELETE FROM supplierdetails_excel";
                object obj = Connection.ExecuteSQLQueryScalar(sql);
            }
            foreach (DataRow dr in dtSupplierDetails.Rows)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();               
                param.Add(Connection.GetParameter("pSupplierName", DataPlug.DataType._Varchar, CutString(dr["Supplier Name"],100), 255));
                param.Add(Connection.GetParameter("pContactName", DataPlug.DataType._Varchar, CutString( dr["Contact Name"],100), 255));
                param.Add(Connection.GetParameter("pEmail", DataPlug.DataType._Varchar, CutString(dr["Email"],150), 255));
                param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, CutString(dr["Address"],500), 500));
                param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, CutString(dr["City"],150), 255));
                param.Add(Connection.GetParameter("pState", DataPlug.DataType._Varchar, CutString(dr["State"],150), 255));
                param.Add(Connection.GetParameter("pCountry", DataPlug.DataType._Varchar, CutString(dr["Country"],150), 255));
                param.Add(Connection.GetParameter("pPincode", DataPlug.DataType._Varchar, CutString(dr["Pincode"],10), 255));
                param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, CutString(dr["Phone"],50), 255));
                param.Add(Connection.GetParameter("pMobile", DataPlug.DataType._Varchar, CutString(dr["Mobile"],50), 255));
                param.Add(Connection.GetParameter("pService_Tax_Number", DataPlug.DataType._Varchar, CutString(dr["Service_Tax_Number"],30), 255));
                param.Add(Connection.GetParameter("pVATNumber", DataPlug.DataType._Varchar, CutString(dr["VAT Number"],30), 255));
                param.Add(Connection.GetParameter("pBank_Account_Number", DataPlug.DataType._Varchar, CutString(dr["Bank_Account_Number"],150), 255));
                param.Add(Connection.GetParameter("pBank_name", DataPlug.DataType._Varchar, CutString(dr["Bank_name"],100), 255));
                param.Add(Connection.GetParameter("pBranch_Address", DataPlug.DataType._Varchar, CutString(dr["Branch_Address"],500), 500));
                param.Add(Connection.GetParameter("pRTGSNumber", DataPlug.DataType._Varchar, CutString(dr["RTGS Number"],150), 255));
                param.Add(Connection.GetParameter("pMICR_Code", DataPlug.DataType._Varchar, CutString(dr["MICR_Code"],150), 255));
                param.Add(Connection.GetParameter("pPANNo", DataPlug.DataType._Varchar, CutString(dr["PAN No"],20), 255));
                object result = Connection.ExecuteQueryScalar("supplierdetails_excel_save", param);
            }


            if (dtSupplierDetails.Rows.Count > 0)
            {
                string sql = "DELETE FROM propertydetails_excel";
                object obj = Connection.ExecuteSQLQueryScalar(sql);
            }
            bool bl;
            string tmp;

            foreach (DataRow dr in dtPorpertyDetails.Rows)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pSupplierName", DataPlug.DataType._Varchar, CutString(dr["Supplier Name"],100), 255));
                param.Add(Connection.GetParameter("pPropertyName", DataPlug.DataType._Varchar, CutString(dr["Property Name"],100), 255));
                param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Varchar, CutString(dr["Description"],8000), 8000));
                param.Add(Connection.GetParameter("pLocation", DataPlug.DataType._Varchar, CutString(dr["Location"],50), 255));
                param.Add(Connection.GetParameter("pAddress", DataPlug.DataType._Varchar, CutString(dr["Address"],500), 500));
                param.Add(Connection.GetParameter("pCity", DataPlug.DataType._Varchar, CutString(dr["City"],150), 255));
                param.Add(Connection.GetParameter("pState", DataPlug.DataType._Varchar, CutString(dr["State"],150), 255));
                param.Add(Connection.GetParameter("pCountry", DataPlug.DataType._Varchar, CutString(dr["Country"],150), 255));
                param.Add(Connection.GetParameter("pPincode", DataPlug.DataType._Varchar, CutString(dr["Pincode"],10), 255));
                param.Add(Connection.GetParameter("pProperty_Features", DataPlug.DataType._Varchar, FixCSV(CutString(dr["Property_Features"], 2000)), 2000));
                param.Add(Connection.GetParameter("pCancellation_Charge", DataPlug.DataType._Double, ToDouble(CutString(dr["Cancellation_Charge"], 50)).ToString()));
                param.Add(Connection.GetParameter("pCancellation_Period_Hours", DataPlug.DataType._Varchar, ToDouble(CutString(dr["Cancellation_Period_Hours"],11)).ToString(), 255));
                param.Add(Connection.GetParameter("pCop_Discount_ShortTerm", DataPlug.DataType._Double, ToDouble(CutString(dr["Cop_Discount_ShortTerm"], 50)).ToString()));
                param.Add(Connection.GetParameter("pCorp_Discount_LongTerm", DataPlug.DataType._Double, ToDouble(CutString(dr["Corp_Discount_LongTerm"], 50)).ToString()));

                param.Add(Connection.GetParameter("pService_Tax", DataPlug.DataType._Double, ToDouble(CutString(dr["Service_Tax"], 50)).ToString()));
                param.Add(Connection.GetParameter("pLuxury_Tax", DataPlug.DataType._Double, ToDouble(CutString(dr["Luxury_Tax"], 50)).ToString()));
                
                param.Add(Connection.GetParameter("pMarkup_B2B_ShortTerm", DataPlug.DataType._Double, ToDouble(CutString(dr["Markup_B2B_ShortTerm"], 50)).ToString()));
                param.Add(Connection.GetParameter("pMarkup_B2B_LongTerm", DataPlug.DataType._Double, ToDouble(CutString(dr["Markup_B2B_LongTerm"], 50)).ToString()));
                param.Add(Connection.GetParameter("pMarkup_B2C_ShortTerm", DataPlug.DataType._Double, ToDouble(CutString(dr["Markup_B2C_ShortTerm"], 50)).ToString()));
                param.Add(Connection.GetParameter("pMarkup_B2C_LongTerm", DataPlug.DataType._Double, ToDouble(CutString(dr["Markup_B2C_LongTerm"], 50)).ToString()));
                param.Add(Connection.GetParameter("pCancellation_Policy", DataPlug.DataType._Varchar, CutString(dr["Cancellation_Policy"],200), 255));
                x = 0;
                int.TryParse(CutString(dr["Cancellation_Value"], 200), out x);
                if (x < 1) x = 1;
                param.Add(Connection.GetParameter("pCancellation_Value", DataPlug.DataType._Double, x));
                param.Add(Connection.GetParameter("pCancellation_Period", DataPlug.DataType._Double, ToDouble(CutString(dr["Cancellation_Period"], 11)).ToString()));
                tmp = CutString(dr["Policy_Applicable_ForRefund"],50).ToLower();
                if(tmp.Length>0) tmp = tmp.Trim();
                if(tmp == "yes" || tmp =="true")
                    bl = true;
                else
                    bl = false;
                param.Add(Connection.GetParameter("pPolicy_Applicable_ForRefund", DataPlug.DataType._Bool,bl));
                param.Add(Connection.GetParameter("pEmailId", DataPlug.DataType._Varchar, CutString(dr["Property_Email_ID"], 255), 255));
                param.Add(Connection.GetParameter("pPhone", DataPlug.DataType._Varchar, CutString(dr["Phone"],50), 255));
                param.Add(Connection.GetParameter("pMobile", DataPlug.DataType._Varchar, CutString(dr["Mobile"],50), 255));
                object result = Connection.ExecuteQueryScalar("propertydetails_excel_save", param);
            }
            if (dtSupplierDetails.Rows.Count > 0)
            {
                string sql = "DELETE FROM accommodations_excel";
                object obj = Connection.ExecuteSQLQueryScalar(sql);
            }
            foreach (DataRow dr in dtAccomodationDetails.Rows)
            {
                List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
                param.Add(Connection.GetParameter("pPropertyName", DataPlug.DataType._Varchar, CutString(dr["Property Name"],100), 255));
                param.Add(Connection.GetParameter("pCategory", DataPlug.DataType._Varchar, CutString(dr["Category"],100), 255));
                param.Add(Connection.GetParameter("pAccommodationType", DataPlug.DataType._Varchar, CutString(dr["Type"],50), 255));
                param.Add(Connection.GetParameter("pDescription", DataPlug.DataType._Varchar, CutString(dr["Description"],3000), 3000));
                param.Add(Connection.GetParameter("pMaxAdults", DataPlug.DataType._Varchar, ToInteger(CutString(dr["Max Adults"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pMaxKids", DataPlug.DataType._Varchar, ToInteger(CutString(dr["Max Kids"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pMaxOccupancy", DataPlug.DataType._Varchar,ToInteger( CutString(dr["Max Occupancy"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pBedRooms", DataPlug.DataType._Varchar, ToInteger(CutString(dr["BedRooms"], 50)).ToString(), 255));
                ara = 0;
                double.TryParse( CutString(dr["Area m#sq"],200),out ara);
                x = 0;
                int.TryParse(CutString(dr["Available Accommodation"], 100), out x);
                param.Add(Connection.GetParameter("pAreamsq", DataPlug.DataType._Varchar,ara.ToString(), 255));
                param.Add(Connection.GetParameter("pAvailableAccommodation", DataPlug.DataType._Varchar,x.ToString(), 255));
                x = 0;
                int.TryParse(CutString(dr["Total Accommodation"],100), out x);
                param.Add(Connection.GetParameter("pTotalAccommodation", DataPlug.DataType._Varchar, x.ToString(), 255));
                param.Add(Connection.GetParameter("pAccommodationFeatures", DataPlug.DataType._Varchar, FixCSV(CutString(dr["Accommodation Features"], 2000)), 2000));
                param.Add(Connection.GetParameter("pB2B_Daily", DataPlug.DataType._Varchar, ToDouble(CutString(dr["B2B_Daily"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2B_Weekly", DataPlug.DataType._Varchar, ToDouble(CutString(dr["B2B_Weekly"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2B_Monthly", DataPlug.DataType._Varchar, ToDouble(CutString(dr["B2B_Monthly"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2B_Long", DataPlug.DataType._Varchar,ToDouble(CutString(dr["B2B_Long"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2B_Guest", DataPlug.DataType._Varchar, ToDouble(CutString(dr["B2B_Guest"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2C_Daily", DataPlug.DataType._Varchar,ToDouble(CutString(dr["B2C_Daily"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2C_Weekly", DataPlug.DataType._Varchar,ToDouble(CutString(dr["B2C_Weekly"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2C_Monthly", DataPlug.DataType._Varchar,ToDouble(CutString(dr["B2C_Monthly"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2C_Long", DataPlug.DataType._Varchar,ToDouble(CutString(dr["B2C_Long"],50)).ToString(), 255));
                param.Add(Connection.GetParameter("pB2C_Guest", DataPlug.DataType._Varchar, ToDouble(CutString(dr["B2C_Guest"], 50)).ToString(), 255));

                object result = Connection.ExecuteQueryScalar("accommodations_excel_save", param);
            }

            return "imported";
        }

        private  double ToDouble(string data)
        {
            double d = 0;
            double.TryParse(data, out d);
            return d;
        }
        private int ToInteger(string data)
        {
            int d = 0;
            int.TryParse(data, out d);
            return d;
        }
    }
}
