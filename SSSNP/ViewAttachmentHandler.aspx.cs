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
using OfficeOpenXml;
using Image = iTextSharp.text.Image;

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
            PDF_PAYROLL_DATA = 13, 
            MNE_MODULE_Pdf_Download = 11,// Add new case for MNE_MODULE_Pdf_Download
            MNE_MODULE_Exl_Download = 12 // Add new case for MNE_MODULE_Exl_Download
         
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
                case HandlerAttachment.PDF_PAYROLL_DATA:
                    GeneratePayrollPdf();
                    return "OK";
                case HandlerAttachment.MNE_MODULE_Exl_Download:
                    MNEModuleExlHandler();
                    return "OK";
                case HandlerAttachment.MNE_MODULE_Pdf_Download:
                    MNEModulePdfHandler();
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
                    LogError(ex);
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

        //download payroll data in pdf started
        public void GeneratePayrollPdf()
        {
            // Retrieve the ApiResponse object from the session
            PayrollDownloadResponse apiResponse = Session["PDResponse"] as PayrollDownloadResponse;
            if (apiResponse == null)
            {
                throw new InvalidOperationException("apiResponse is not initialized in the session.");
            }

            // Retrieve geo location data from session
            string stateName = Session["SelectedStateName"] as string ?? "N/A";
            string countyName = Session["SelectedCountyName"] as string ?? "N/A";
            string payamName = Session["SelectedPayamName"] as string ?? "N/A";
            string bomaName = Session["SelectedBomaName"] as string ?? "N/A";

            // Define a custom page size with increased width
            Rectangle customPageSize = new Rectangle(PageSize.LEGAL.Width + 200f, PageSize.LEGAL.Height);

            // Create a new PDF document
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(customPageSize.Rotate(), 25, 25, 30, 30); // Increased width and rotated to landscape
                PdfWriter.GetInstance(document, ms);
                document.Open();

                // Create a font for the title with size 16 and bold
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);

                Paragraph title = new Paragraph("Payroll Data", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);

                // Add Wage Request label with current date
                Paragraph wageRequest = new Paragraph("Wage Request: " + DateTime.Now.ToString("dd-MM-yyyy"))
                {
                    Alignment = Element.ALIGN_LEFT
                };
                document.Add(wageRequest);

                // Add geo location data (State, County, Payam, Boma)
                document.Add(new Paragraph("State: " + stateName));
                document.Add(new Paragraph("County: " + countyName));
                document.Add(new Paragraph("Payam: " + payamName));
                document.Add(new Paragraph("Boma: " + bomaName));

                // Add space before main table
                document.Add(new Paragraph(" "));

                // Create table with 24 columns for payroll details
                PdfPTable table = new PdfPTable(19)
                {
                    WidthPercentage = 100
                };

                // Set widths for each column to match the section sizes
                table.SetWidths(new float[] {
            1f, 1.5f, 2f, 1f, 1f,            // Beneficiary (5 columns: Photo, ID, Name, Gender, Age)
            1f, 1.5f, 2f, 1f, 1f,            // First Alternate (5 columns: Photo, ID, Name, Gender, Age)
            1f, 1.5f, 2f, 1f, 1f,            // Second Alternate (5 columns: Photo, ID, Name, Gender, Age)
            1f, 1.5f, 1.5f,                  // Payment (3 columns: Cycle, Support Type, Wage Total)
            1.5f                               // Signature (1 column)
        });

                // Add section headers
                AddSectionHeaderToPdfTable(table, "Beneficiary", 5);
                AddSectionHeaderToPdfTable(table, "First Alternate", 5);
                AddSectionHeaderToPdfTable(table, "Second Alternate", 5);
                AddSectionHeaderToPdfTable(table, "Payment", 3);
                AddSectionHeaderToPdfTable(table, "Signature", 1);

                // Add payroll data headers (columns)
                AddCellToPdfTableHeader(table, "ID");
                AddCellToPdfTableHeader(table, "Photo");
                AddCellToPdfTableHeader(table, "Name");
                AddCellToPdfTableHeader(table, "Gender");
                AddCellToPdfTableHeader(table, "Age");

                AddCellToPdfTableHeader(table, "ID");
                AddCellToPdfTableHeader(table, "Photo");
                AddCellToPdfTableHeader(table, "Name");
                AddCellToPdfTableHeader(table, "Gender");
                AddCellToPdfTableHeader(table, "Age");

                AddCellToPdfTableHeader(table, "ID");
                AddCellToPdfTableHeader(table, "Photo");
                AddCellToPdfTableHeader(table, "Name");
                AddCellToPdfTableHeader(table, "Gender");
                AddCellToPdfTableHeader(table, "Age");

                AddCellToPdfTableHeader(table, "Cycle");
                AddCellToPdfTableHeader(table, "Support Type");
                AddCellToPdfTableHeader(table, "Wage Total");

                AddCellToPdfTableHeader(table, "Sign");

                // Iterate through payroll data in the container and add rows
                foreach (var container in apiResponse.data)
                {
                    foreach (var payroll in container.payrollData)
                    {
                        // Beneficiary Details
                        AddCellToPdfTableBody(table, payroll.intBenid.ToString());
                        AddPhotoToPdfTable(table, payroll.houseHoldDetail.biometric.Photo);
                        AddCellToPdfTableBody(table, $"{payroll.houseHoldDetail.firstName} {payroll.houseHoldDetail.middleName} {payroll.houseHoldDetail.lastName}");
                        AddCellToPdfTableBody(table, GetGenderString(payroll.houseHoldDetail.gender)); // Updated gender logic
                        AddCellToPdfTableBody(table, payroll.houseHoldDetail.age.ToString());

                        // First Alternate
                        if (payroll.alternates.Count > 0)
                        {
                            var firstAlternate = payroll.alternates[0];
                            AddCellToPdfTableBody(table, firstAlternate.id.ToString());  // ID for the first alternate
                            AddPhotoToPdfTable(table, firstAlternate.biometric.Photo);
                            AddCellToPdfTableBody(table, $"{firstAlternate.firstName} {firstAlternate.middleName} {firstAlternate.lastName}");
                            AddCellToPdfTableBody(table, GetGenderString(firstAlternate.gender)); // Updated gender logic
                            AddCellToPdfTableBody(table, firstAlternate.age.ToString());
                        }
                        else
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                AddCellToPdfTableBody(table, string.Empty);
                            }
                        }

                        // Second Alternate
                        if (payroll.alternates.Count > 1)
                        {
                            var secondAlternate = payroll.alternates[1];
                            AddCellToPdfTableBody(table, secondAlternate.id.ToString());  // ID for the second alternate
                            AddPhotoToPdfTable(table, secondAlternate.biometric.Photo);
                            AddCellToPdfTableBody(table, $"{secondAlternate.firstName} {secondAlternate.middleName} {secondAlternate.lastName}");
                            AddCellToPdfTableBody(table, GetGenderString(secondAlternate.gender)); // Updated gender logic
                            AddCellToPdfTableBody(table, secondAlternate.age.ToString());
                        }
                        else
                        {
                            for (int j = 0; j < 5; j++)
                            {
                                AddCellToPdfTableBody(table, string.Empty);
                            }
                        }

                        // Payment and Signature
                        AddCellToPdfTableBody(table, payroll.paymentCycle);
                        AddCellToPdfTableBody(table, payroll.supportType);
                        AddCellToPdfTableBody(table, payroll.wageRateUSDTotal.ToString("F2"));
                        AddCellToPdfTableBody(table, string.Empty); // Sign - can be left blank or populated as needed
                    }
                }

                // Add the table to the document
                document.Add(table);

                // Close the document
                document.Close();

                // Send the document to the client
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=PayrollData.pdf");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }

        // Helper methods remain unchanged
        private void AddPhotoToPdfTable(PdfPTable table, string base64Photo)
        {
            if (!string.IsNullOrEmpty(base64Photo))
            {
                byte[] imageBytes = Convert.FromBase64String(base64Photo);
                Image img = Image.GetInstance(imageBytes);
                img.ScaleToFit(40f, 50f); // Set image size to fit within the fixed cell height

                PdfPCell imageCell = new PdfPCell(img, true)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Padding = 2f,// Match the fixed height of other cells// Add some padding to prevent overlap
                    FixedHeight = 50f 
                };
                table.AddCell(imageCell);
            }
            else
            {
                PdfPCell emptyCell = new PdfPCell(new Phrase(string.Empty))
                {
                    FixedHeight = 50f // Match the fixed height of other cells
                };
                table.AddCell(emptyCell);
            }
        }

        private void AddSectionHeaderToPdfTable(PdfPTable table, string headerText, int colspan)
        {
            PdfPCell headerCell = new PdfPCell(new Phrase(headerText, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)))
            {
                Colspan = colspan,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 30f // Set a fixed height of 50 points for section cells
            };
            table.AddCell(headerCell);
        }

        private void AddCellToPdfTableHeader(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)))
            {
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 30f // Set a fixed height of 50 points for body cells
            };
            table.AddCell(cell);
        }

        private void AddCellToPdfTableBody(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, 8)))
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE
            };
            table.AddCell(cell);
        }

        // Helper method to handle gender logic
        private string GetGenderString(int genderValue)
        {
            return genderValue == 0 ? "Male" : genderValue == 1 ? "Female" : "N/A";
        }


        //download payroll data in pdf end

        // MNE module pdf started
        private void MNEModulePdfHandler()
        {
            try
            {
                // Retrieve the data from the session
                var dt = Session["ReportPdf"] as DataTable;
                if (dt == null) throw new Exception("Cash transfer summary data is missing.");

                // Generate PDF from the DataTable
                GeneratePDF(dt);
            }
            catch (Exception ex)
            {
                LogError("Error in CashTransferSummaryHandler: " + ex.Message, ex);
                throw;
            }
        }

        private void GeneratePDF(DataTable dt)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Set the document size to legal and rotate it to landscape for more width
                using (Document document = new Document(PageSize.LEGAL.Rotate(), 10f, 10f, 10f, 10f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();

                    // Set the font and size
                    Font font = FontFactory.GetFont(FontFactory.HELVETICA, 8);

                    // Calculate column widths based on the number of columns
                    float[] columnWidths = new float[dt.Columns.Count];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        columnWidths[i] = 1f; // Adjust as necessary
                    }

                    PdfPTable table = new PdfPTable(dt.Columns.Count)
                    {
                        WidthPercentage = 100
                    };
                    table.SetWidths(columnWidths);

                    // Add headers with font styling
                    foreach (DataColumn column in dt.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, font))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE,
                            BackgroundColor = new BaseColor(230, 230, 250), // Light color for headers
                            Padding = 5
                        };
                        table.AddCell(headerCell);
                    }

                    // Add data
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            PdfPCell dataCell = new PdfPCell(new Phrase(row[col].ToString(), font))
                            {
                                HorizontalAlignment = Element.ALIGN_LEFT,
                                VerticalAlignment = Element.ALIGN_MIDDLE,
                                Padding = 5
                            };
                            table.AddCell(dataCell);
                        }
                    }

                    document.Add(table);
                    document.Close();
                }

                // Write the stream to the response
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "attachment; filename=ReportSummary.pdf");
                Response.BinaryWrite(ms.ToArray());
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }

        // MNE module pdf ended

        // Excel Download started
        private void MNEModuleExlHandler()
        {
            try
            {
                // Retrieve the data from the session
                var dt = Session["ReportExl"] as DataTable;
                if (dt == null)
                {
                    throw new Exception("Report data is missing.");
                }

                // Generate PDF from the DataTable
                GenerateExcel(dt);
            }
            catch (Exception ex)
            {
                LogError("Error in MNEModuleExlHandler: " + ex.Message, ex);
            }
        }

        private void GenerateExcel(DataTable dt)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");


                // Add the headers
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    worksheet.Cells[1, col + 1].Value = dt.Columns[col].ColumnName;
                    // Optional: Format header cells for better visibility
                    worksheet.Cells[1, col + 1].Style.Font.Bold = true;
                }

                // Add the data
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        // Handle potential null or DBNull values gracefully
                        var cellValue = dt.Rows[row][col];
                        worksheet.Cells[row + 2, col + 1].Value = cellValue == DBNull.Value ? "" : cellValue.ToString();
                    }
                }

                // Auto-fit columns for all cells
                worksheet.Cells.AutoFitColumns();

                // Set other styling options as needed (e.g., borders, alignment, number formats)

                byte[] fileContents = package.GetAsByteArray();

                // Write the stream to the response
                Response.ClearHeaders();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename = ReportSummary.xlsx");
                Response.AddHeader("Content-Length", fileContents.Length.ToString());

                Response.BinaryWrite(fileContents);
                //HttpContext.Current.ApplicationInstance.CompleteRequest();

                Response.Flush(); 
                Response.Close();
            }
        }

        // Excel Download ended

        private void LogError(string v, Exception ex)
        {
            throw new NotImplementedException();
        }


    }
}