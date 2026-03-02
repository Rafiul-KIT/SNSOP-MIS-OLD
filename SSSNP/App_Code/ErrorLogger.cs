using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace SSSNP_SNSOP.App_Code
{
    public class ErrorLogger
    {
        private string _filename = "ErrorLog.txt";
        private string _errorMessage;
        public ErrorLogger()
        {

        }
        public void LogError(Exception ex)
        {
            if (ex != null)
            {
                // Collect the information and store it in the File;
                string errStack = "";
                string errMsg = "";
                string errInner = "";

                Exception tex = ex;

                while (tex != null)
                {
                    if (errStack.Length > 0)
                    {
                        errStack += "\r\n";
                        errInner += "\r\n";
                    }
                    errStack += tex.StackTrace ?? "";

                    if (tex.InnerException != null) errInner += tex.InnerException.Message;

                    tex = tex.InnerException;
                }

                if (errStack.Length > 0)
                {
                    errStack += "\r\n";
                }
                errStack += System.Environment.StackTrace ?? "";

                errMsg = ex.Message ?? "";

                string errPageURL = "<no page>\r\n";
                try
                {
                    errPageURL = HttpContext.Current.Request.Url.ToString();
                    errPageURL += "\r\n";
                }
                catch (Exception)
                {
                    // DO NOTHING
                }

                #region ErrorMessage 
                StringBuilder sb = new StringBuilder();

                sb.Append("## Error Log Timestamp: " + DateTime.Now.ToString() + " =========================================================== \r\n");
                sb.Append(errInner + "\r\n");
                sb.Append(errPageURL + "\r\n");
                sb.Append(errMsg);
                sb.Append(errStack);
                sb.Append("\r\n## End Error Log =========================================================== ");

                this._errorMessage = sb.ToString();

                #endregion

                CreateLogErrorFile(errMsg, errStack, errInner + errPageURL);


            }

        }
        private void CreateLogErrorFile(string errMsg, string errStack, string errInner)
        {
            string filename = _filename;
                       
            try
            {
                filename = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFullPath"];
            }
            catch (Exception)
            {

                filename = _filename;
            }
            
                        
            using (StreamWriter w = File.AppendText(filename))
            {

                w.WriteLine("## Error Log Timestamp: " + DateTime.Now.ToString() + " ===========================================================");
                w.WriteLine(errInner);
                w.WriteLine(errMsg);
                w.WriteLine(errStack);
                w.WriteLine("## End Error Log =========================================================== \r\n");

            }
        }

    }
}