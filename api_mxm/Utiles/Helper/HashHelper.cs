using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace api_mxm.Utiles.Helper
{
    public class HashHelper
    {
        public static string MD5(string word)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(word));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public static string CreateSalt(int tamaño)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            var buff = new byte[tamaño];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public static string GenerateSHA256Hash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed managed = new SHA256Managed();
            byte[] hash = managed.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static string CreateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed managed = new SHA256Managed();
            byte[] hash = managed.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
