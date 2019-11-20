using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace Alading.Core.Helper
{
    public class SecurityHelper
    {
        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="decryptString"></param>
        /// <returns></returns>
        public static string TripleDESDecrypt(string decryptString)
        {
            return Cryptographer.DecryptSymmetric("TripleDESCryptoServiceProvider", decryptString);
        }

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="encryptString"></param>
        /// <returns></returns>
        public static string TripleDESEncrypt(string encryptString)
        {
            return Cryptographer.EncryptSymmetric("TripleDESCryptoServiceProvider", encryptString);
        }
    }
}
