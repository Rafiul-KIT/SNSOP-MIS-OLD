using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using SSSNP_SNSOP.PDF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using SSSNP.Entity.Payroll_Download;
using SSSNP.Entity.Payroll;
using SSSNP_SNSOP.UI.Public_Works;
using System.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SSSNP_SNSOP
{
    public partial class ViewAttachmentHandler : System.Web.UI.Page
    {
        private readonly string _queryValue = "AttachmentValue";
        private readonly string _queryType = "AttachmentType";
        private HandlerAttachment _type;
        private object _attachmentValue;

        public enum HandlerAttachment
        {
            NONE = 0,
            NON_GBV_ATTACHMENT = 1,
            NON_GBV_DOWNLOAD_TEMPLATE_FILE = 2,
            GBV_ATTACHMENT = 3,
            GBV_DOWNLOAD_TEMPLATE_FILE = 4,
            NON_GBV_ATTACHMENT_DEDICATED = 5,
            ATTACHMENT_DRAFT = 6,
            GBV_ATTACHMENT_DEDICATED = 7,
            SYSTEM_REPORT = 8,
            SYSTEM_REPORT_ELRP = 9,
            ENCRYPTED_PAYROLL_DATA = 10,// Add new case for encrypted payroll data
            CASH_TRANSFER_SUMMARY = 11 // Add new case for cash transfer summary download
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.LoadAttachmentHandler();

            }
        }
        private string LoadAttachmentHandler()
        {
            if (this.GetHandlerAttachment == HandlerAttachment.NONE) { return ""; }

            // Handler
            switch (this.GetHandlerAttachment)
            {
                case HandlerAttachment.NON_GBV_ATTACHMENT:
                    Non_GBVAttachmentHandler(this.GetAttachmentValue);
                    return "OK";
                case HandlerAttachment.NON_GBV_DOWNLOAD_TEMPLATE_FILE:
                    Non_GBVAttachmentHandler(true);
                    return "OK";
                case HandlerAttachment.GBV_ATTACHMENT:
                    GBVAttachmentHandler(this.GetAttachmentValue);
                    return "OK";
                case HandlerAttachment.GBV_DOWNLOAD_TEMPLATE_FILE:
                    Non_GBVAttachmentHandler(false);
                    return "OK";
                case HandlerAttachment.NON_GBV_ATTACHMENT_DEDICATED:
                    Non_GBVAttachmentDedicatedHandler(this.GetAttachmentValue);
                    return "OK";
                case HandlerAttachment.ATTACHMENT_DRAFT:
                    AttachmentDraftHandler(this.GetAttachmentValue);
                    return "OK";
                case HandlerAttachment.GBV_ATTACHMENT_DEDICATED:
                    GBVAttachmentDedicatedHandler(this.GetAttachmentValue);
                    return "OK";
                case HandlerAttachment.SYSTEM_REPORT:
                    SystemReportHandler();
                    return "OK";
                case HandlerAttachment.SYSTEM_REPORT_ELRP:
                    SystemReportHandler_ELRP();
                    return "OK";
                case HandlerAttachment.ENCRYPTED_PAYROLL_DATA:
                    EncryptedPayrollDataHandler();
                    return "OK";
                case HandlerAttachment.CASH_TRANSFER_SUMMARY:
                    CashTransferSummaryHandler();
                    return "OK";
                    //break;

            }

            return "";
        }

        private void Non_GBVAttachmentHandler(object valueFunction)
        {
            if (valueFunction == null) return;
            int value = SSSNPHelper.TryParseInt(valueFunction.ToString());
            if (value == 0) return;

            NonGBVAttachment attachment = SSSNPContext.SSSNPManager.GetNonGBVAttachment(value);

            Response.ClearHeaders();
            Response.ContentType = attachment.ContentType;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + attachment.Filename);
            Response.AddHeader("Content-Length", attachment.FileAttached.Length.ToString());

            try
            {
                Response.BinaryWrite((byte[])attachment.FileAttached);
            }
            catch { }

            Response.Flush();
            Response.Close();

        }

        private void Non_GBVAttachmentHandler(bool isNonGBVTemplate)
        {
            string filenameFull;
            string filename;

            if (isNonGBVTemplate)
            {
                filenameFull = Server.MapPath("~/Template/NonGBVTemplateFile.xlsx");
                filename = "NonGBVTemplateFile.xlsx";
            }
            else
            {
                filenameFull = Server.MapPath("~/Template/GBVTemplateFile.xlsx");
                filename = "GBVTemplateFile.xlsx";
            }

            try
            {
                byte[] buffer;

                using (FileStream fs = new FileStream(filenameFull, FileMode.Open))
                {

                    buffer = new byte[(int)fs.Length];
                    fs.Read(buffer, 0, (int)fs.Length);
                    fs.Close();
                }

                Response.ClearHeaders();
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
                Response.AddHeader("Content-Length", buffer.Length.ToString());


                Response.BinaryWrite((byte[])buffer);
            }
            catch (Exception)
            {
                throw;
            }

            Response.Flush();
            Response.Close();

        }

        private void GBVAttachmentHandler(object valueFunction)
        {
            if (valueFunction == null) return;
            int value = SSSNPHelper.TryParseInt(valueFunction.ToString());
            if (value == 0) return;

            GBVAttachment attachment = SSSNPContext.SSSNPManager.GetGBVAttachment(value);


            Response.ClearHeaders();
            Response.ContentType = attachment.ContentType;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + attachment.Filename);
            Response.AddHeader("Content-Length", attachment.FileAttached.Length.ToString());

            try
            {
                Response.BinaryWrite((byte[])attachment.FileAttached);
            }
            catch { }

            Response.Flush();
            Response.Close();

        }
        private void SystemReportHandler()
        {
            SystemReportGenerate sGenerate = new SystemReportGenerate();
            if (sGenerate.CreatePdfDocument())
            {
                string strFileName = String.Format("SystemReportGenerated_{0}_{1}.pdf", DateTime.Today.ToString("yyyyMMdd"), DateTime.Now.ToString("hhmm"));
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + "SystemReportGenerated.pdf");
                Response.AddHeader("Content-Length", sGenerate.DownloadPdfDocument().ToArray().Length.ToString());

                try
                {
                    Response.BinaryWrite((byte[])sGenerate.DownloadPdfDocument().ToArray());
                }
                catch { }

                Response.Flush();
                Response.Close();
            }
        }

        private void SystemReportHandler_ELRP()
        {
            //year and month from querystring
            int year = Convert.ToInt32(Request.QueryString["year"]);
            int month = Convert.ToInt32(Request.QueryString["month"]);


            SystemReportGenerate sGenerate = new SystemReportGenerate(true, year, month);
            if (sGenerate.CreatePdfDocument())
            {
                string strFileName = String.Format("SystemReportGenerated_{0}_{1}_ELRP.pdf", DateTime.Today.ToString("yyyyMMdd"), DateTime.Now.ToString("hhmm"));
                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + strFileName);
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + "SystemReportGenerated.pdf");
                Response.AddHeader("Content-Length", sGenerate.DownloadPdfDocument().ToArray().Length.ToString());

                try
                {
                    Response.BinaryWrite((byte[])sGenerate.DownloadPdfDocument().ToArray());
                }
                catch { }

                Response.Flush();
                Response.Close();
            }
        }


        private void Non_GBVAttachmentDedicatedHandler(object valueFunction)
        {
            if (valueFunction == null) return;
            int value = SSSNPHelper.TryParseInt(valueFunction.ToString());
            if (value == 0) return;

            NonGBVAttachmentDedicated attachment = SSSNPContext.SSSNPManager.GetNonGBVAttachmentDedicated(value);


            Response.ClearHeaders();
            Response.ContentType = attachment.ContentType;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + attachment.Filename);
            Response.AddHeader("Content-Length", attachment.FileAttached.Length.ToString());

            try
            {
                Response.BinaryWrite((byte[])attachment.FileAttached);
            }
            catch { }

            Response.Flush();
            Response.Close();

        }
        private void GBVAttachmentDedicatedHandler(object valueFunction)
        {
            if (valueFunction == null) return;
            int value = SSSNPHelper.TryParseInt(valueFunction.ToString());
            if (value == 0) return;

            GBVAttachmentDedicated attachment = SSSNPContext.SSSNPManager.GetGBVAttachmentDedicated(value);


            Response.ClearHeaders();
            Response.ContentType = attachment.ContentType;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + attachment.Filename);
            Response.AddHeader("Content-Length", attachment.FileAttached.Length.ToString());

            try
            {
                Response.BinaryWrite((byte[])attachment.FileAttached);
            }
            catch { }

            Response.Flush();
            Response.Close();

        }
        private void AttachmentDraftHandler(object valueFunction)
        {
            if (valueFunction == null) return;
            Guid value = Guid.Empty;
            Guid.TryParse(valueFunction.ToString(), out value);
            if (value == Guid.Empty) return;

            AttachmentDraft attachment = SSSNPContext.SSSNPManager.GetAttachmentDraft(value);


            Response.ClearHeaders();
            Response.ContentType = attachment.ContentType;
            Response.AddHeader("Content-Disposition", "attachment;filename=" + attachment.Filename);
            Response.AddHeader("Content-Length", attachment.FileAttached.Length.ToString());

            try
            {
                Response.BinaryWrite((byte[])attachment.FileAttached);
            }
            catch { }

            Response.Flush();
            Response.Close();

        }
        private HandlerAttachment GetHandlerAttachment
        {
            get
            {
                try
                {
                    int result;
                    int.TryParse(Request.QueryString[_queryType], out result);

                    return (HandlerAttachment)result; ;
                }
                catch (Exception ex)
                {
                    return HandlerAttachment.NONE;
                }
            }
        }

        private object GetAttachmentValue
        {
            get
            {
                try
                {
                    return Request.QueryString[_queryValue];
                }
                catch (Exception ex)
                {

                    return null;
                }
            }
        }

        //downlaod encrypted payroll data module start
        private void EncryptedPayrollDataHandler()
        {
            if (Session["PDResponse"] != null)
            {
                // Retrieve the ApiResponse object from the session
                PayrollDownloadResponse apiResponse = Session["PDResponse"] as PayrollDownloadResponse;
               
                string json = JsonConvert.SerializeObject(apiResponse);

                // Print the JSON string for debugging
                // System.Diagnostics.Debug.WriteLine(json);

                // Hexadecimal key (256-bit) and IV (128-bit)
                string hexKey = "dd8de506bd4a90418afe9372f01b979b6f0df2e8aae22ec90b09a0bc197dc80c";
                string hexIv = "4a6f8e2f8a0c5d3e4b6c8d9e0f1a2b3c";

                byte[] key = HexStringToByteArray(hexKey);
                byte[] iv = HexStringToByteArray(hexIv);

                try
                {
                    // Encrypt the data
                    byte[] encryptedData;
                    encryptedData = EncryptData(json, key, iv);

                    // Set the response headers and content
                    // Response.Clear();
                    Response.ClearHeaders();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", "attachment; filename=encryptedPayrollData.bin");
                    Response.AddHeader("Content-Length", encryptedData.Length.ToString());

                
                    Response.BinaryWrite((byte[])encryptedData);
                }
                catch (Exception ex)
                {
                    throw;
                }

                Response.Flush(); // Ensure all data is sent to the client
                // HttpContext.Current.ApplicationInstance.CompleteRequest(); // End the request without aborting the thread
                Response.Close();
            }
        }

        private byte[] EncryptData(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        private byte[] HexStringToByteArray(string hex)
        {
            if (hex.Length % 2 != 0)
                throw new ArgumentException("Hex string must have an even length.");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        private void LogError(Exception ex)
        {
            // Implement your logging logic here
            System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
        }
        //downlaod encrypted payroll data module end




        // Cash transfer summary started
        private void CashTransferSummaryHandler()
        {
            try
            {
                // Retrieve the data from the session
                var dt = Session["CashTransferSummaryData"] as DataTable;
                if (dt == null)
                {
                    throw new Exception("Cash transfer summary data is missing.");
                }

                // Generate PDF from the DataTable
                GeneratePDF(dt);
            }
            catch (Exception ex)
            {
                LogError("Error in CashTransferSummaryHandler: " + ex.Message, ex);
            }
        }

        private void GeneratePDF(DataTable dt)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (Document document = new Document())
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();
                    PdfPTable table = new PdfPTable(dt.Columns.Count);

                    // Add headers
                    foreach (DataColumn column in dt.Columns)
                    {
                        table.AddCell(new Phrase(column.ColumnName));
                    }

                    // Add data
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            table.AddCell(new Phrase(row[col].ToString()));
                        }
                    }

                    document.Add(table);
                    document.Close();
                }

                // Write the stream to the response
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=CashTransferSummary.pdf");
                Response.BinaryWrite(ms.ToArray());
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        private void LogError(string message, Exception ex)
        {
            // Log error details to a file, event log, or database.
            System.Diagnostics.Debug.WriteLine($"{message}: {ex}");
        }



    }
}