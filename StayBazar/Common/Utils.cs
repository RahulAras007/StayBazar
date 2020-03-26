using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Security;
using System.Drawing;
using System.Configuration;

namespace StayBazar.Common
{
    public static class Utils
    {
        public const string SERARCH_MAX_ROWS = "SearchResultRowCount";
        private const int MOBILE_LEN = 10;
        //public const string ROUTE_LIST = "ROUTES_LIST";
        public const string NO_GST = "NOT__REGISTERED";
        public class CustomRoute
        {
            public string Controller { get; set; }
            public string Action { get; set; }
            public string Path { get; set; }
        }

        public static string GetImageAlt(string altText)
        {
            return GetImageAlt(altText, "Staybazar");
        }
        public static string GetImageAlt(string altText, string defaultIfEmpty)
        {
            if (altText == null) return defaultIfEmpty;
            altText = altText.Trim();
            if (altText == "") return defaultIfEmpty;
            altText = altText.Replace("'", "").Replace("\"", "").Replace("  ", " ");
            if (altText == "") return defaultIfEmpty;
            return altText;
        }

        //for paypal transaction result processing
        /// <summary>
        /// For paypal transaction result processing
        /// </summary>
        /// <param name="key">key to search. For example "TOKEN"</param>
        /// <param name="source">source string</param>
        /// <returns>value as string. Retruns empty string if key not found</returns>
        public static string GetValueFromResultString(string key, string source)
        {
            string rkey = key + "=";
            string[] items = source.Split('&');
            foreach (string s in items)
            {
                if (s.Contains(rkey))
                {
                    string[] val = s.Split('=');
                    if (val.Length < 2) return "";
                    return val[1].Trim();
                }
            }
            return "";
        }

     

      
        public static List<CustomRoute> GetCustomRoutes()
        {
            List<CustomRoute> routes = new List<CustomRoute>();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string rt = "";
            string[] vals;
            string[] spchar = new string[] { "," };
            CustomRoute cr;
            for (int i = 1; i < 201; i++)
            {
                if (System.Configuration.ConfigurationManager.AppSettings.Get("customroute" + i.ToString()) != null)
                {
                    rt = System.Configuration.ConfigurationManager.AppSettings.Get("customroute" + i.ToString()).Trim();
                    if (rt != "")
                    {
                        rt = rt.Replace(" ", "");
                        if (rt.Length == 0) continue;
                        vals = rt.Split(spchar, StringSplitOptions.RemoveEmptyEntries);
                        if (vals.Length == 3)
                        {
                            cr = new CustomRoute();
                            cr.Controller = vals[0];
                            cr.Action = vals[1];
                            cr.Path = vals[2];
                            routes.Add(cr);
                        }
                    }
                    else
                        break;
                }
                else
                    break;
            }

            return routes;
        }
        public static List<string> GetIgnoredRoutes()
        {
            List<string> routes = new List<string>();
            string rt = "";
            string[] rts;
            string[] spchar = new string[] { "," };
            for (int i = 1; i < 6; i++)
            {
                if (System.Configuration.ConfigurationManager.AppSettings.Get("route" + i.ToString()) != null)
                {
                    rt = System.Configuration.ConfigurationManager.AppSettings.Get("route" + i.ToString()).Trim();
                    if (rt != "")
                    {
                        rt = rt.Trim();
                        rt = rt.Replace(" ", "");
                        if (rt.Length == 0) continue;
                        rts = rt.Split(spchar, StringSplitOptions.RemoveEmptyEntries);
                        routes.AddRange(rts.ToList<string>());
                    }
                }
            }
            return routes;
        }

        /*    public static List<string> GetIgnoredRoutes()
            {
                List<string>  routes = new List<string>();
                string rlis="";
                if(HttpContext.Current.Application.Get(ROUTE_LIST)!= null)
                {
                    rlis = HttpContext.Current.Application.Get(ROUTE_LIST).ToString();
                }
                if(rlis != "")
                {
                    string[] rts = rlis.Split(new string[] {","}, StringSplitOptions.RemoveEmptyEntries);
                    routes = rts.ToList<string>();
                }
                return routes;
            }

            public static void StoreIgnoredRoutes()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string rt = "";

                for(int i =1;i<6;i++)
                {
                    if(System.Configuration.ConfigurationManager.AppSettings.Get("route" + i.ToString())!= null)
                    {
                        rt = System.Configuration.ConfigurationManager.AppSettings.Get("route" + i.ToString());
                        if(rt!= "")
                        {
                            rt = rt.Trim();
                            rt = rt.Replace(" ", "");
                            if (rt.Length == 0) continue;
                            if (sb.Length > 0) sb.Append(",");
                            sb.Append(rt);
                        }
                    }
                }
                if (sb.Length > 0)
                    HttpContext.Current.Application.Set(ROUTE_LIST,sb.ToString());
                else
                    HttpContext.Current.Application.Set(ROUTE_LIST,"");
            }*/
        /// <summary>
        /// Extract phone no from CSV or numbers seperated with &
        /// </summary>
        /// <param name="multiMobiles"></param>
        /// <returns></returns>
        public static string GetMobileNo(string multiMobiles)
        {
            if (multiMobiles == null || multiMobiles == "") return "";
            multiMobiles = multiMobiles.Trim();
            multiMobiles = multiMobiles.Replace(" ", "").Replace("[", "").Replace("]", "")
                .Replace("(", "").Replace(")", "").Replace("[", "").Replace("-", "")
                .Replace(".", "").Replace("&", ",").Replace("'", "").Replace("\"", "");
            int len = multiMobiles.Length;
            if (multiMobiles == "") return "";
            string splt = "";
            if (multiMobiles.IndexOf(",") > -1)
                splt = ",";
            else
            {
                if (multiMobiles.IndexOf("\\") > -1)
                    splt = "\\";
                else
                {
                    if (multiMobiles.IndexOf("/") > -1)
                        splt = "/";
                }
            }
            if (splt != "")
            {
                string[] split = multiMobiles.Split(new string[] { splt }, StringSplitOptions.RemoveEmptyEntries);
                return split[0];
            }
            return multiMobiles;
        }


        public static bool IsCrossedMaxDate(ref DateTime checkIn, ref DateTime checkOut)
        {
            bool crossed = false;
            DateTime max = DateTime.Today.AddDays(240);
            if (checkOut > max)
            {
                int days = (checkOut - max).Days;
                if (days > 0)
                {
                    days = days * -1;
                    checkOut = checkOut.AddDays(days);
                    checkIn = checkIn.AddDays(days);
                    crossed = true;
                }
            }
            return crossed;
        }

        public static double DistanceCalc(double lat, double lng, double dlat, double dlng)
        {
            double r = (((Math.Acos(Math.Sin((lat * Math.PI / 180)) *
             Math.Sin((dlat * Math.PI / 180)) + Math.Cos((lat * Math.PI / 180)) *
             Math.Cos((dlat * Math.PI / 180)) * Math.Cos(((lng - dlng) *
             Math.PI / 180)))) * 180 / Math.PI) * 60 * 1.609344);
            return r;
        }
        public static double GetDistance(double Lat1,
                  double Long1, double Lat2, double Long2)
        {
            /*
                The Haversine formula according to Dr. Math.
                http://mathforum.org/library/drmath/view/51879.html
                
                dlon = lon2 - lon1
                dlat = lat2 - lat1
                a = (sin(dlat/2))^2 + cos(lat1) * cos(lat2) * (sin(dlon/2))^2
                c = 2 * atan2(sqrt(a), sqrt(1-a)) 
                d = R * c
                
                Where
                    * dlon is the change in longitude
                    * dlat is the change in latitude
                    * c is the great circle distance in Radians.
                    * R is the radius of a spherical Earth.
                    * The locations of the two points in 
                        spherical coordinates (longitude and 
                        latitude) are lon1,lat1 and lon2, lat2.
            */
            double dDistance = Double.MinValue;
            double dLat1InRad = Lat1 * (Math.PI / 180.0);
            double dLong1InRad = Long1 * (Math.PI / 180.0);
            double dLat2InRad = Lat2 * (Math.PI / 180.0);
            double dLong2InRad = Long2 * (Math.PI / 180.0);

            double dLongitude = dLong2InRad - dLong1InRad;
            double dLatitude = dLat2InRad - dLat1InRad;

            // Intermediate result a.
            double a = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                       Math.Cos(dLat1InRad) * Math.Cos(dLat2InRad) *
                       Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            // Intermediate result c (great circle distance in Radians).
            double c = 2.0 * Math.Asin(Math.Sqrt(a));

            // Distance.
            // const Double kEarthRadiusMiles = 3956.0;
            const Double kEarthRadiusKms = 6376.5;
            dDistance = kEarthRadiusKms * c;

            return dDistance;
        }

        public static bool IsGuest()
        {
            if (HttpContext.Current.Session[CLayer.ObjectStatus.GUEST_ID_SESSION] != null && ((long)HttpContext.Current.Session[CLayer.ObjectStatus.GUEST_ID_SESSION] > 0))
            {
                return true;
            }
            return false;
        }

        public static string SecureInputString(string input)
        {
            input = input.Replace("'", "");
            input = input.Replace("<", "");
            input = input.Replace("<", "");
            input = input.Replace(";", "");
            input = input.Replace("\"", "");
            input = input.Replace("%", "");
            return input;
        }

        //public static string SecureString(string input)
        //{
        //    input = input.Replace("'", "");
        //    input = input.Replace("<", "");
        //    input = input.Replace("<", "");
        //    input = input.Replace(";", "");
        //    return input;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Abbreviate(string text, int length)
        {
            if (text.Length <= length)
            {
                return text;
            }

            char[] delimiters = new char[] { ' ', '.', ',', ':', ';' };
            int index = text.LastIndexOfAny(delimiters, length - 3);

            if (index > (length / 2))
            {
                return text.Substring(0, index) + "...";
            }
            else
            {
                return text.Substring(0, length - 3) + "...";
            }
        }

        public static string CutString(string text, int length)
        {
            if (text.Length <= length)
            {
                return text;
            }
            else
            {
                return text.Substring(0, length);
            }

        }

        public static string ConvertShortLengthString(string text, int NoCharacter)
        {

            string s = text;
            if (text.Length > NoCharacter)
            {
                text = text.Substring(0, NoCharacter) + "..";
                s = text;
                return s;
            }
            else
            {
                return s;
            }

        }

        public static string GetStarRating(int rating)
        {
            System.Text.StringBuilder stars = new System.Text.StringBuilder(); ;
            stars.Append("<span class=\"star\">");

            for (int i = 1; i < 6; i++)
            {
                if (i <= rating)
                    stars.Append("<i class=\"glyphicon glyphicon-star st-active\"></i>");
                else
                    stars.Append("<i class=\"glyphicon glyphicon-star\"></i>");
            }
            stars.Append("</span>");
            return stars.ToString();
        }

        public static string CSVNumericValidation(string csvValues)
        {
            string result = "";
            string[] ids = csvValues.Split(',');
            int i, len;
            long val;
            len = ids.Count();
            for (i = 0; i < len; i++)
            {
                val = 0;
                if (long.TryParse(ids[i].Trim(), out val))
                {
                    if (val > 0)
                    {
                        if (result != "") result = result + ",";
                        result = result + val.ToString();
                    }
                }
            }
            return result;
        }

        public class Location
        {
            public decimal Longitude;
            public decimal Lattitude;
        };
        public static async Task<Location> GetLocation(string searchText)
        {
            Location pos = new Location();
            pos.Longitude = 0;
            pos.Lattitude = 0;
            try
            {

                string apiQuery = BLayer.Settings.GetValue(CLayer.Settings.GOOGLE_API_QUERY);
                apiQuery = apiQuery.Replace("[ADDR]", HttpUtility.UrlEncode(searchText));
                apiQuery = apiQuery.Replace("[API]", BLayer.Settings.GetValue(CLayer.Settings.GOOGLE_API_KEY));

                System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
                string result = await client.GetStringAsync(apiQuery);
                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(result);
                string status = doc.SelectSingleNode("/GeocodeResponse/status").InnerText;
                if (status.ToUpper().Trim() == "OK")
                {
                    string lat = doc.SelectSingleNode("/GeocodeResponse/result/geometry/location/lat").InnerText;
                    string longitude = doc.SelectSingleNode("/GeocodeResponse/result/geometry/location/lng").InnerText;
                    decimal.TryParse(lat, out pos.Lattitude);
                    decimal.TryParse(longitude, out pos.Longitude);
                }
            }
            catch (Exception ex)
            { Common.LogHandler.LogError(ex); }
            return pos;
        }
   
        public static string GetRatingPercentage(double currentRating)
        {
            if (currentRating < 1) return "0%";
            double perc = currentRating / 5 * 100;
            return Math.Round(perc, 0).ToString() + "%";
        }
        // long maxHeight;
        public static Image ScaleImage(Image image, int maxHeight)
        {
            // maxHeight = Convert.ToInt32(ConfigurationManager.AppSettings.Get("MaxImgHeight"));
            var ratio = (double)maxHeight / image.Height;
            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);
            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                g.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }



        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return FirstLetterToUpper(words);
        }
        public static string NumberToWordsWithFirstLower(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 100000) > 0)
            {
                words += NumberToWords(number / 100000) + " lakh ";
                number %= 100000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        public static string FirstLetterToUpper(string str)
        {
            if (str == null) return null;
            if (str.Length > 1) return char.ToUpper(str[0]) + str.Substring(1);
            return str.ToUpper();
        }

        public static string[] SplitEmails(string Email)
        {
            string[] emaillist = new string[] { };
            try
            {
                if (Email != null && Email != "")
                {
                    Email = Email.Replace(" ", "").Replace(";", ",");
                    emaillist = Email.Split(',').Where(x => !string.IsNullOrEmpty(x)).Distinct().ToArray();
                }
            }
            catch (Exception ex)
            {
                Common.LogHandler.HandleError(ex);
            }
            return emaillist;
        }
    }
}