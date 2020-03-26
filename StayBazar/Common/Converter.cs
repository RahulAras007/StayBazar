using System;


namespace StayBazar.Common
{
    public class Converter
    {
        public static  string ToString(object databaseObject)
        {
            string s = string.Empty;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                s = databaseObject.ToString();
            }
            return s;
        }

        public static  double ToDouble(object databaseObject)
        {
            double d = 0;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                double.TryParse(databaseObject.ToString(), out d);
            }

            return d;
        }
        public static  decimal ToDecimal(object databaseObject)
        {
            decimal d = 0;

            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                decimal.TryParse(databaseObject.ToString(), out d);
            }
            return d;
        }

        public static  int ToInteger(object databaseObject)
        {
            int i = 0;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                int.TryParse(databaseObject.ToString(), out i);
            }
            return i;
        }

        public static  short ToShort(object databaseObject)
        {
            short i = 0;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                if (!short.TryParse(databaseObject.ToString(), out i))
                {
                    i = 0;
                }
            }
            return i;
        }

        public static  ulong ToULong(object databaseObject)
        {
            ulong i = 0;

            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                i = ulong.Parse(databaseObject.ToString());
            }
            return i;
        }

        public static  long ToLong(object databaseObject)
        {
            long i = 0;

            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                i = long.Parse(databaseObject.ToString());
            }
            return i;
        }

        public static  bool ToBoolean(object databaseObject)
        {
            bool b = false;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                b = Convert.ToBoolean(databaseObject);
            }
            return b;
        }

        public static  System.DateTime ToDate(object databaseObject)
        {
            System.DateTime d = new System.DateTime();
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                System.DateTime.TryParse(databaseObject.ToString(), out d);
            }
            return d;
        }
        public static string ToStringDate(object databaseObject)
        {
            System.DateTime d = new System.DateTime();
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                System.DateTime.TryParse(databaseObject.ToString(), out d);
                return d.ToString("dd/MM/yyyy");
            }
            else { return ""; }

        }
        public static  Nullable<DateTime> ToNDate(object databaseObject)
        {
            System.DateTime d = new System.DateTime();
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                System.DateTime.TryParse(databaseObject.ToString(), out d);
            }
            return d;
        }

        public static  double MoneyToDouble(object databaseObject)
        {
            double d = 0;
            if (databaseObject != null && databaseObject != System.DBNull.Value)
            {
                if (!double.TryParse(databaseObject.ToString(), out d))
                {
                    d = 0;
                }
            }
            return d;
        }
    }
}