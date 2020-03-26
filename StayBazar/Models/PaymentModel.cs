using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Web;
using System.Text;

namespace StayBazar.Models
{
    public class PaymentModel
    {
        public string account_id { get; set; }
        public string reference_no { get; set; }
        public string amount { get; set; }
        public string mode { get; set; }
        public string description { get; set; }
        public string return_url { get; set; }
        //customer billing details
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string postal_code { get; set; }
        //delivery address

        public string ship_name { get; set; }
        public string ship_address { get; set; }
        public string ship_city { get; set; }
        public string ship_state { get; set; }
        public string ship_country { get; set; }
        public string ship_postal_code { get; set; }
        public string ship_phone { get; set; }
        //security
        public string secure_hash { get; set; }


        //public void SecurePayment()
        //{

        //    string secretKey = BLayer.Settings.GetValue(CLayer.Settings.PAY_SECRET_KEY); // System.Configuration.ConfigurationManager.AppSettings.Get("SecureHashForEBS");
        //    //string input = secretKey + "|" + account_id + "|" + amount + "|" + reference_no + "|"
        //    //+ return_url + "|" + mode;
        //    string algo= "SHA512";
        //    int Channel = 0;
        //    string CURRENCY = "INR";
           
        //    var param1 = (dynamic)null;

        //    string input = secretKey + "|" + account_id + "|" + address + "|" + algo + "|" + amount + "|null|null|null|" + param1 + "|" + Channel + "|" + city + "|" + country + "|" + CURRENCY + "|" + description + "|" + email + "|" + mode + "|" + name + "|null|" + phone + "|" + postal_code + "|" + reference_no + "|" + return_url + "|" + ship_address + "|" + ship_city + "|" + ship_country + "|" + ship_name + "|" + ship_phone + "|" + ship_postal_code + "|" + state + "|" + ship_state;


        //    MD5 md5 = System.Security.Cryptography.MD5.Create();
        //    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes (input);
        //    byte[] hashBytes = md5.ComputeHash (inputBytes);
        //    StringBuilder sb = new StringBuilder(); 
        //    for (int i = 0; i < hashBytes.Length; i++)
        //    {
        //    sb.Append (hashBytes[i].ToString ("X2"));
        //    }
        //    secure_hash = sb.ToString();// computeHash(input); //
        //   System.Web.HttpContext.Current.Session["secure_hash"] = secure_hash;
        //}
        public void SecurePayment()
        {
            string secretKey = BLayer.Settings.GetValue(CLayer.Settings.PAY_SECRET_KEY); // System.Configuration.ConfigurationManager.AppSettings.Get("SecureHashForEBS");
            string input = secretKey + "|" + account_id + "|" + amount + "|" + reference_no + "|"
            + return_url + "|" + mode;
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            secure_hash = sb.ToString();
        }
        static string computeHash(string input)
        {

            byte[] data = null;


            data = HashAlgorithm.Create("SHA512").ComputeHash(Encoding.ASCII.GetBytes(input));


            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString().ToUpper();

        }

    }
}