using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataLayer.DataProvider;
using System.Data;

namespace DataLayer
{
   
    public class Utils : DataLink
    {
        public List<object> GetAutoList(string term)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            param.Add(Connection.GetParameter("pPropertyStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            DataTable dt = Connection.GetTable("xtra_autocompelete", param);
            List<object> result = new List<object>();
            string a, b;
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                a = Connection.ToString(dr["a"]);
                //b = Connection.ToString(dr["b"]);
                //i = Connection.ToInteger(dr["num"]);
               // result.Add(new { value = a, label = a, desc = stringCnt(a, b, i) });
                result.Add(new { value = a, label = a, desc = stringCnt(a) });

            }
            return result;
        }
        public List<object> GetAutoListGDS(string term)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            param.Add(Connection.GetParameter("pPropertyStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            DataTable dt = Connection.GetTable("xtra_autocompelete_gds", param);
            List<object> result = new List<object>();
            string a, b;
            int ix;
            bool IsHotelName = false;
            string cCity;
            string cLocation;
            string cPropertyId;
            
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                a = Connection.ToString(dr["a"]);
                b = (term.ToLower() == a.ToLower()) ?"": Connection.ToString(dr["num"]);
                ix = Connection.ToInteger(dr["ix"]);
                cCity= Connection.ToString(dr["city"]);
                cLocation = Connection.ToString(dr["location"]);
                cPropertyId = Connection.ToString(dr["propertyId"]);
                IsHotelName = Connection.ToInteger(dr["IsHotelName"]) == 1 ? true : false;


                //b = Connection.ToString(dr["b"]);
                //i = Connection.ToInteger(dr["num"]);
                // result.Add(new { value = a, label = a, desc = stringCnt(a, b, i) });
                result.Add(new { value = a, label = b, desc = stringCnt(a), City=cCity,Location=cLocation,PropertyId=cPropertyId,bHotelName= IsHotelName });

            }
            return result;
        }
        public List<object> GetAutoListforaccommn(string term)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            DataTable dt = Connection.GetTable("Accommodation_autocompelete", param);
            List<object> result = new List<object>();
            string a, b;
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                a = Connection.ToString(dr["a"]);
                result.Add(new { value = a });

            }
            return result;
        }

        public List<object> GetAutoListforaccommnForOffline(string term)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            DataTable dt = Connection.GetTable("Accommodation_autocompeleteForOffline", param);
            List<object> result = new List<object>();
            string a, b, c;
            int i;
            foreach (DataRow dr in dt.Rows)
            {
                a = Connection.ToString(dr["a"]);
                b = Connection.ToString(dr["b"]);

                result.Add(new { Text = b, value = a });
            }
            return result;
        }

        private string stringCnt(string a, string b="", int num=0)
        {
            string r = a;
            //string r = a + ", " + b;
            //if (num > 1)
            //{
            //    r = r + "   (" + num.ToString() + " Properties)";
            //}
            //else
            //{
            //    if (num == 1)
            //    {
            //        r = r + "   (" + num.ToString() + " Property)";
            //    }
            //}
            return r;
        }
        public List<object> GetLocations(string term)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            param.Add(Connection.GetParameter("pPropertyStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            DataTable dt = Connection.GetTable("xtra_getlocations", param);
            List<object> result = new List<object>();
            string a, b;
            // int i;
            foreach (DataRow dr in dt.Rows)
            {
                a = Connection.ToString(dr["a"]);
                b = Connection.ToString(dr["b"]);
                result.Add(new { value = a, label = a, desc = a + ", " + b });

            }
            return result;
        }
        public List<string> GetLocationFilter(string term)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            param.Add(Connection.GetParameter("pPropertyStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            DataTable dt = Connection.GetTable("GetAll_locations", param);
            List<string> result = new List<string>();
            string a, b;
            foreach (DataRow dr in dt.Rows)
            {
                a = Connection.ToString(dr["a"]);
                b = Connection.ToString(dr["b"]);
                result.Add(a);

            }
            return result;
        }
        public List<CLayer.SearchResult> GetLocationmultiFilter(string term, int type)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 100) term = term.Substring(0, 100);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, type));
            param.Add(Connection.GetParameter("pPropertyStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            DataTable dt = Connection.GetTable("GetAll_multilocations", param);
            List<CLayer.SearchResult> result = new List<CLayer.SearchResult>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.SearchResult()
                {
                    Location = Connection.ToString(dr["Location"]),
                    PropertyCount = Connection.ToInteger(dr["pno"])
                    //City = Connection.ToString(dr["City"])                    
                });
            }
            return result;
        }
        public List<string> GetLocationFilterGDS(string term)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            param.Add(Connection.GetParameter("pPropertyStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            DataTable dt = Connection.GetTable("GetAll_locations_gds", param);
            List<string> result = new List<string>();
            string a, b;
            foreach (DataRow dr in dt.Rows)
            {
                a = GetLocationName(Connection.ToString(dr["a"]));
                b = Connection.ToString(dr["b"]);
                result.Add(a);

            }
            return result;
        }
        public List<CLayer.SearchResult> GetLocationmultiFilterGDS(string term, int type)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 100) term = term.Substring(0, 100);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            param.Add(Connection.GetParameter("pType", DataPlug.DataType._Int, type));
            param.Add(Connection.GetParameter("pPropertyStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            DataTable dt = Connection.GetTable("GetAll_multilocations_gds", param);
            List<CLayer.SearchResult> result = new List<CLayer.SearchResult>();
            foreach (DataRow dr in dt.Rows)
            {
                result.Add(new CLayer.SearchResult()
                {
                    Location = GetLocationName(Connection.ToString(dr["Location"])),
                    PropertyCount = Connection.ToInteger(dr["pno"])
                    //City = Connection.ToString(dr["City"])                    
                });
            }


            return result;
        }
        public string GetLocationName(string pValue)
        {
            string result = string.Empty;
            string[] resultList = pValue.Split(',');
            try
            {
                if (resultList.Length > 0)
                {
                    result = ToTitleCase(resultList[resultList.Length - 1]);
                }
                else
                {
                    result = ToTitleCase(pValue);
                }

            }
            catch (Exception ex)
            {

            }
            return result.Trim();
        }
        public string ToTitleCase(string str)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }
        public List<CLayer.AutoCompletedList> GetAutoListGDSAll(string term)
        {
            List<DataPlug.Parameter> param = new List<DataPlug.Parameter>();
            if (term.Length > 50) term = term.Substring(0, 50);
            param.Add(Connection.GetParameter("pTerm", DataPlug.DataType._Varchar, term));
            param.Add(Connection.GetParameter("pPropertyStatus", DataPlug.DataType._Int, (int)CLayer.ObjectStatus.StatusType.Active));
            DataTable dt = Connection.GetTable("xtra_autocompelete_gds", param);
            List<CLayer.AutoCompletedList> result = new List<CLayer.AutoCompletedList>();
            string a, b;
            int ix;
            bool IsHotelName = false;
            string cCity;
            string cLocation;
            string cPropertyId;

            int i;
            foreach (DataRow dr in dt.Rows)
            {

               string  Location = Connection.ToString(dr["a"]);

                b = (term.ToLower() == Location.ToLower()) ? "" : Connection.ToString(dr["num"]);
                ix = Connection.ToInteger(dr["ix"]);
                cCity = Connection.ToString(dr["city"]);

                cLocation = Connection.ToString(dr["location"]);
                cPropertyId = Connection.ToString(dr["propertyId"]);
                IsHotelName = Connection.ToInteger(dr["IsHotelName"]) == 1 ? true : false;

                if(!IsHotelName)
                {
                    string[] aList = Location.Split(',');
                    if (aList.Length > 2)
                    {
                        string LocationAdvanced = aList[1].Trim() + "," + aList[2].Trim();
                        if (aList[1].ToLower().Trim() == aList[2].ToLower().Trim())
                        {

                            Location = aList[0].Trim() + "," + aList[1].Trim();

                        }
                        else
                        {
                            LocationAdvanced = GetFormattedLocation(LocationAdvanced);
                            Location = aList[0] + "," + LocationAdvanced;
                        }

                    }
                    else if (aList.Length == 2)
                    {
                        string LocationAdvanced = aList[1].Trim();
                        LocationAdvanced = GetFormattedLocation(LocationAdvanced);
                        Location = aList[0] + "," + LocationAdvanced;

                    }
                }


                CLayer.AutoCompletedList objResult = new CLayer.AutoCompletedList();
                objResult.value = Location;
                objResult.label = b;
                objResult.desc = stringCnt(Location);
                objResult.City = cCity;
                objResult.Location =  cLocation;
                objResult.PropertyId = cPropertyId;
                objResult.bHotelName = IsHotelName;
                result.Add(objResult);


            }

            var resultlist = result.GroupBy(x => x.value).Select(y => y.First());
            List<CLayer.AutoCompletedList> resultFinal = new List<CLayer.AutoCompletedList>();

            foreach (var item in resultlist)
            {
                CLayer.AutoCompletedList objResult = new CLayer.AutoCompletedList();
                objResult.value = item.value;
                objResult.label = item.label;
                objResult.desc = item.desc;
                objResult.City = item.City;
                objResult.Location = item.Location;
                objResult.PropertyId = item.PropertyId;
                objResult.bHotelName = item.bHotelName;
                resultFinal.Add(objResult);
                //Add to other List
            }
            return resultFinal;
        }
        public string GetFormattedLocation(string pDestination)
        {
            string result = string.Empty;
            string[] DestinationList = pDestination.Split(',');
            if (DestinationList.Length >= 1)
            {
                switch (DestinationList[0].ToLower())
                {
                    case "uae":
                    case "u a e":
                    case "u.a.e":
                    case "u. a. e.":
                    case "united arab emirates":
                        result = "UAE";
                        break;
                    case "u s":
                    case "us":
                    case "usa":
                    case "united states":
                    case "united states of america":
                    case "u s a":
                    case "america":
                        result = "USA";
                        break;
                    case "uk":
                    case "u k":
                    case "united kingdom":
                    case "united kingdom of great britain":
                    case "great britain":
                    case "britain":
                    case "england":
                        result = "U K";
                        break;
                    case "china":
                    case "china, people’s republic of":
                        result = "China";
                        break;
                    case "ireland":
                    case "ireland, republic of":
                        result = "Ireland";
                        break;

                }
            }
            else
            {
                result = pDestination;
            }

            return result;
        }
    }
}
