using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace EatHealthy
{
    public class PasswordHelper
    {
        private static PasswordHelper passwordHelper;

        private PasswordHelper()
        {

        }

        public static PasswordHelper GetPasswordHelper()
        {
            if(passwordHelper == null)
            {
                passwordHelper = new PasswordHelper();
            }
            return passwordHelper;
        }
        private const string SecurityKey = "8i4h5fuirehi43hfuriu4eiou834gitr";
        public string EncryptPlainTextToCipherText(string PlainText)
        {
            byte[] toEncryptedArray = UTF8Encoding.UTF8.GetBytes(PlainText);

            MD5CryptoServiceProvider objMD5CryptoService = new MD5CryptoServiceProvider();
            byte[] securityKeyArray = objMD5CryptoService.ComputeHash(UTF8Encoding.UTF8.GetBytes(SecurityKey));
            objMD5CryptoService.Clear();

            var objTripleDESCryptoService = new TripleDESCryptoServiceProvider();
            objTripleDESCryptoService.Key = securityKeyArray;
            objTripleDESCryptoService.Mode = CipherMode.ECB;
            objTripleDESCryptoService.Padding = PaddingMode.PKCS7;


            var objCrytpoTransform = objTripleDESCryptoService.CreateEncryptor();
            byte[] resultArray = objCrytpoTransform.TransformFinalBlock(toEncryptedArray, 0, toEncryptedArray.Length);
            objTripleDESCryptoService.Clear();
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    }
}