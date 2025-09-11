using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Winning.Outp.External.wdpatsign
{
    /// <summary>
    /// DES加解密
    /// </summary>
    public class DESUtils
    {
        private static readonly string sKey = "healthjingan2016";
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string EncryptString(string str)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = UTF8Encoding.UTF8.GetBytes(str);
            des.Mode = CipherMode.ECB;
            des.Key = UTF8Encoding.UTF8.GetBytes(sKey).Take(8).ToArray();
            des.IV = UTF8Encoding.UTF8.GetBytes(sKey).Take(8).ToArray();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var retB = Base64StringURLSafe(ms.ToArray());
            return retB;
        }
        public static string Base64StringURLSafe(byte[] bytes)
        {
            string base64String = Convert.ToBase64String(bytes);
            return base64String.Replace("+", "-")
                .Replace("/", "_")
                .Replace("=", "");
        }
        public static byte[] FromBase64StringURLSafe(string SafeString)
        {
            SafeString = SafeString.Replace("-", "+").Replace("_", "/");
            var base64 = Encoding.ASCII.GetBytes(SafeString);
            var padding = base64.Length * 3 % 4;//(base64.Length*6 % 8)/2
            if (padding != 0)
            {
                SafeString = SafeString.PadRight(SafeString.Length + padding, '=');
            }
            return Convert.FromBase64String(SafeString);
        }
        //解密
        public static string DecryptString(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            byte[] inputByteArray = FromBase64StringURLSafe(pToDecrypt);
            byte[] key2 = UTF8Encoding.UTF8.GetBytes(sKey);
            des.Key = UTF8Encoding.UTF8.GetBytes(sKey).Take(8).ToArray();
            des.IV = UTF8Encoding.UTF8.GetBytes(sKey).Take(8).ToArray();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return UTF8Encoding.UTF8.GetString(ms.ToArray());
        }
    }
}
