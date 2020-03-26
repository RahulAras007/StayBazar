using System;
using System.Collections.Generic;

using System.Text;




namespace CLayer
{
    public class AutoCompletedList
    {
        public string value { get; set; }
        public string label { get; set; }
        public string desc { get; set; }
        public string Location { get; set; }
        public string PropertyId { get; set; }
        public bool bHotelName { get; set; }
        public string City { get; set; }
    }
    public static class Utils
    {
        public static string CutString(this string text, int length)
        {
            if (text == null || text.Length <= length)
            {
                return text;
            }
            else
            {
                return text.Substring(0, length);
            }
        }


        

 
    }
}
