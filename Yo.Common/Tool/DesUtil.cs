using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Yo.Common.Tool
{
    public class DesUtil
    {
        private static byte[] IV = Encoding.Default.GetBytes("yo2019");
        private static byte[] Key = Encoding.Default.GetBytes("@1d^20?Q");
        public static string Decrypt(string encryptedString)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = null;
                try
                {
                    inData = Convert.FromBase64String(encryptedString);
                }
                catch
                {
                    return string.Empty;
                }
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(Key, IV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }
                    return Encoding.Default.GetString(ms.ToArray());
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
            }

        }

        public static string Encrypt(string sourceString)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] inData = null;
                try
                {
                    inData = Encoding.Default.GetBytes(sourceString);
                }
                catch (Exception e)
                {
                    return string.Empty;
                }
                try
                {
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                    {
                        cs.Write(inData, 0, inData.Length);
                        cs.FlushFinalBlock();
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
                catch
                {
                    return string.Empty;
                }
            }

        }
    }
}
