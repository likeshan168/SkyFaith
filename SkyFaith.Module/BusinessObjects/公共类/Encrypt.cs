using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SkyFaith.Module.BusinessObjects.公共类
{
    public class Encrypt
    {
        public static readonly Encrypt Instance = new Encrypt();
        private MD5CryptoServiceProvider md5;
        private Encrypt()
        {
            md5 = new MD5CryptoServiceProvider();
        }

        public string MD5Encrypt(string worldToEncrypt)
        {
            byte[] result = Encoding.UTF8.GetBytes(worldToEncrypt);
            byte[] output = md5.ComputeHash(result);
            return Convert.ToBase64String(output);
        }
    }
}
