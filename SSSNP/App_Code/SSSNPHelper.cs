using CSDS.Core.Framework.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;

namespace SSSNP_SNSOP
{
    public static class SSSNPHelper
    {
        public static int TryParseInt(string value)
        {
            int result;
            int.TryParse(value.CheckSafeString(), out result);

            return result;
        }
        public static string TryParseString(string value)
        {
            return value.CheckSafeString();
        }
        public static bool TryParseBool(string value)
        {
            bool result;
            bool.TryParse(value.CheckSafeString(), out result);

            return result;
        }
        public static bool TryParseYesNo(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (value.ToLower() == "yes") return true;
            else { return false; }
            
        }
        public static bool? TryParseYesNo (string value, bool nullSafe)
        {
            if (nullSafe)
            {
                if (value == null) return null;
                if (string.IsNullOrEmpty(value)) return null;
                return TryParseYesNo(value);
            }
            else { return TryParseYesNo(value); }
            
        }
        public static DateTime TryParseDateTime(string value)
        {
            DateTime result;
            value = value.Replace("-", "/");
            DateTime.TryParseExact(value.CheckSafeString(), "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out result);

            return result; // (result != DateTime.MinValue) ? (DateTime?)result : null;
        }
        public static DateTime? TryParseDateTime(string value, bool nullSafe)
        {
            DateTime? result;
            result = TryParseDateTime(value);
            if (nullSafe ) return  (result != DateTime.MinValue) ? (DateTime?)result : null;
            return result; 

        }

        public static string getSignedQrData(string json)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(@"C:\snsop.p12", "SmKM3pXfZraDbpPy",
    X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                RSACryptoServiceProvider rsacsp = (RSACryptoServiceProvider)cert.PrivateKey;

                CspParameters cspParam = new CspParameters();
                cspParam.KeyContainerName = rsacsp.CspKeyContainerInfo.KeyContainerName;
                cspParam.KeyNumber = rsacsp.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2;
                RSACryptoServiceProvider aescsp = new RSACryptoServiceProvider(cspParam);
                aescsp.PersistKeyInCsp = false;
                var dataToSign = json;
                byte[] signedBA = aescsp.SignData(Encoding.UTF8.GetBytes(dataToSign), "SHA256");
                bool isValid = aescsp.VerifyData(Encoding.UTF8.GetBytes(dataToSign), "SHA256", signedBA);
                string result = Convert.ToBase64String(signedBA);
                Console.WriteLine("\nSignature Base64:\n" + result);
                Console.WriteLine("\nIs signature verified:\n" + isValid);

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error when trying to get signed QR data\n" + e.ToString());
                return null;
            }
        }

        public static string EncryptString(string key, string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string key, string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

    }
}