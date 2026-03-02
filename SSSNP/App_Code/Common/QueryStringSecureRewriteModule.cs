using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


public class QueryStringSecureRewriteModule : IHttpModule
{
    private const string PARAMETER_NAME = "enc=";
    private const string ENCRYPTION_KEY = "key";

    public void Dispose()
    {
       // do nothing
    }

    public void Init(HttpApplication context)
    {
        context.BeginRequest += new EventHandler(context_BeginRequest);
    }

    private void context_BeginRequest(object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        string query = string.Empty;
        string path = string.Empty;
        try
        {
            if (context.Request.Url.OriginalString.Contains("aspx") && context.Request.RawUrl.Contains("?"))
            {
                query = ExtractQuery(context.Request.RawUrl);
                path = GetVirtualPath();
                if (query.StartsWith(PARAMETER_NAME, StringComparison.OrdinalIgnoreCase))
                {
                    // Decrypts the query string and rewrites the path.
                    string rawQuery = query.Replace(PARAMETER_NAME, string.Empty);
                    string decryptedQuery = Decrypt(rawQuery);
                    context.RewritePath(path, string.Empty, decryptedQuery);
                }
                else if (context.Request.HttpMethod == "GET")
                {
                    // Encrypt the query string and redirects to the encrypted URL.
                    // Remove if you don't want all query strings to be encrypted automatically.
                    string encryptedQuery = Encrypt(query);
                    context.Response.Redirect(path + encryptedQuery, false);
                }
            }
        }
        catch (Exception ex)
        {
            // m_Logger.Error("An error occurred while parsing the query string in the URL: " + path, ex);
            //context.Response.Redirect("~/index.aspx");
            context.Response.Redirect("~/frmuserlogin.aspx");
        }

    }
    private static string GetVirtualPath()
    {
        string path = HttpContext.Current.Request.RawUrl;
        path = path.Substring(0, path.IndexOf("?"));
        path = path.Substring(path.LastIndexOf("/") + 1);
        return path;
    }
  
    private static string ExtractQuery(string url)
    {
        int index = url.IndexOf("?") + 1;
        return url.Substring(index);
    }
    #region Encryption/decryption
   
    private readonly static byte[] SALT = Encoding.ASCII.GetBytes(ENCRYPTION_KEY.Length.ToString());
    private static string Encrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] plainText = Encoding.Unicode.GetBytes(inputText);
        PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);
        using (ICryptoTransform encryptor = rijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainText, 0, plainText.Length);
                    cryptoStream.FlushFinalBlock();
                    return "?" + PARAMETER_NAME + Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }
    }
    private static string Decrypt(string inputText)
    {
        RijndaelManaged rijndaelCipher = new RijndaelManaged();
        byte[] encryptedData = Convert.FromBase64String(inputText);
        PasswordDeriveBytes secretKey = new PasswordDeriveBytes(ENCRYPTION_KEY, SALT);
        using (ICryptoTransform decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16)))
        {
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] plainText = new byte[encryptedData.Length];
                    int decryptedCount = cryptoStream.Read(plainText, 0, plainText.Length);
                    return Encoding.Unicode.GetString(plainText, 0, decryptedCount);
                }
            }
        }
    }
    #endregion

}
