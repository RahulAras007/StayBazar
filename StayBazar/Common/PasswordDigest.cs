using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Globalization;
using System.Configuration;
namespace StayBazar.Common
{
    public class PasswordDigest
    {
        public PasswordDigest()
        {
        }

        /*Generate hashed password*/
        public static string GenerateHashedPassword(string Nonce, string Created, string Password)
        {
            string result = string.Empty;
            byte[] combined = buildBytes(Nonce, Created, Password);
            result = System.Convert.ToBase64String(SHAOneHash(combined));
            return result;

            
        }

        /* Generate nonce */
        public static string GenerateNonce(string extra = "")
        {
            string result = "";
            SHA1 sha1 = SHA1.Create();

            Random rand = new Random();
            StringBuilder sb = new StringBuilder(1024);
            while (result.Length < 8)
            {
                sb.Length = 0;
                string[] generatedRandoms = new string[4];

                for (int i = 0; i < 4; i++)
                {
                    sb.Append(rand.Next());
                }

                sb.Append("|")
                    .Append(extra);

                result += Convert.ToBase64String(
                    sha1.ComputeHash(Encoding.ASCII.GetBytes(sb.ToString()))
                    ).TrimEnd('=')
                     .Replace("/", "")
                     .Replace("+", "");
            }
            return result.Substring(0, 8);
        }
        public static string GenerateTime(bool IsMilliSeconds = true)
        {
            string Created = string.Empty;
            if (IsMilliSeconds)
            {
                Created = DateTime.Now.AddHours(-5).AddMinutes(-30).ToString("yyyy-MM-ddTHH:mm:ss:fffZ", DateTimeFormatInfo.InvariantInfo);
            }
            else
            {
                Created = DateTime.Now.AddHours(-5).AddMinutes(-30).ToString("yyyy-MM-ddTHH:mm:ssZ", DateTimeFormatInfo.InvariantInfo);
            }

            return Created;
        }
        public static byte[] buildBytes(string nonce, string createdString, string basedPassword)
        {
            byte[] nonceBytes = System.Convert.FromBase64String(nonce);
            byte[] time = Encoding.UTF8.GetBytes(createdString);
            byte[] pwd = SHAOneHash(Encoding.UTF8.GetBytes(basedPassword));

            byte[] operand = new byte[nonceBytes.Length + time.Length + pwd.Length];
            Array.Copy(nonceBytes, operand, nonceBytes.Length);
            Array.Copy(time, 0, operand, nonceBytes.Length, time.Length);
            Array.Copy(pwd, 0, operand, nonceBytes.Length + time.Length, pwd.Length);

            return operand;
        }
        public static byte[] SHAOneHash(byte[] data)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(data);
                return hash;
            }
        }

    }
}