using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace Alading.WebService
{
    public class HashProvider
    {
        public static string GetHash(string data)
        {
            HashAlgorithm a = HashAlgorithm.Create("SHA512");

            byte[] d = Encoding.UTF8.GetBytes(data);

            byte[] s = a.ComputeHash(d);

            return Convert.ToBase64String(s);
        }

        public static string GetHash(string data, string key)
        {
            HMACSHA512 a = new HMACSHA512();

            byte[] d = Encoding.UTF8.GetBytes(data);
            byte[] k = Encoding.UTF8.GetBytes(key);

            a.Key = k;

            byte[] s = a.ComputeHash(d);

            return Convert.ToBase64String(s);
        }
    }
}
