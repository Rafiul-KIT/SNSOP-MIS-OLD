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
using SSSNP.Service;
using System.Diagnostics;
using System.Data.SqlClient;
using SSSNP_SNSOP.Core.Model;

namespace SSSNP_SNSOP
{
    public partial class ViewAttachmentHandler : System.Web.UI.Page
    {
        private readonly string _queryValue = "AttachmentValue";
        private readonly string _queryType = "AttachmentType";
        private HandlerAttachment _type;
        private object _attachmentValue;
        private GeoLocationDto _location;

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
            ENCRYPTED_PAYROLL_DATA = 10,    // Add new case for encrypted payroll data 
            MNE_MODULE_Pdf_Download = 11,   // Add new case for MNE_MODULE_Pdf_Download
            MNE_MODULE_Exl_Download = 12,   // Add new case for MNE_MODULE_Exl_Download
            PDF_PAYROLL_DATA = 13,
            CASH_TRANSFER_Pdf_Download = 14,
            CASH_TRANSFER_Exl_Download = 15,
            HOUSEHOLD_GROUP_VIEW_EXCEL_TEMPLATE = 16,
            TRAINING_ATTENDANCE_TRACKING_TEMPLATE = 17,
            BUSINESS_PLAN_DEVELOPMENT_TEMPLATE = 18,
            LIVELIHOOD_GRANT_DISBURSEMENT_TEMPLATE = 19,
            BENEFICIARY_RANDOMIZATION_ENROLLMENT_TEMPLATE = 20,
            FINANCIAL_SERVICE_LINKAGE_TEMPLATE = 21,
            ECONOMIC_OPPORTUNITY_AUDIT_RECORD_DOWNLOAD = 22
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.LoadGeoLocation();
            if (!Page.IsPostBack)
            {
                this.LoadAttachmentHandler();
                
            }
        }

        private void LoadGeoLocation()
        {
            _location = Session["GeoLocation"] as GeoLocationDto;
            /*if (_location == null)
            {
                throw new InvalidOperationException("Location details are missing from the session.");
            }*/
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
                case HandlerAttachment.CASH_TRANSFER_Pdf_Download:
                    CashTransferPdfHandler();
                    return "OK";
                case HandlerAttachment.CASH_TRANSFER_Exl_Download:
                    CashTransferxlHandler();
                    return "OK";
                case HandlerAttachment.HOUSEHOLD_GROUP_VIEW_EXCEL_TEMPLATE:
                    HouseholdTemplateExlTemplate();
                    return "OK";
                case HandlerAttachment.TRAINING_ATTENDANCE_TRACKING_TEMPLATE:
                    TrainingAttendanceTrackingTemplate();
                    return "OK";
                //break;
                case HandlerAttachment.BUSINESS_PLAN_DEVELOPMENT_TEMPLATE:
                    BusinessPlanDevelopmentTemplate();
                    return "OK";
                case HandlerAttachment.BENEFICIARY_RANDOMIZATION_ENROLLMENT_TEMPLATE:
                    BeneficiaryRandomizationEnrollmentTemplate();
                    return "OK";
                case HandlerAttachment.FINANCIAL_SERVICE_LINKAGE_TEMPLATE:
                    FinancialServiceLinkageTrackingTemplate();
                    return "OK";
                case HandlerAttachment.LIVELIHOOD_GRANT_DISBURSEMENT_TEMPLATE:
                    LivelihoodGrantDisbursementTemplate();
                    return "OK";
                case HandlerAttachment.ECONOMIC_OPPORTUNITY_AUDIT_RECORD_DOWNLOAD:
                    EconomicOppontunityAuditRecordDownload();
                    return "OK";
            }

            return "";
        }

        private void BusinessPlanDevelopmentTemplate()
        {
            try
            {
                int? stateId = _location?.StateId;
                int? countyId = _location?.CountyId;
                int? payamId = _location?.PayamId;
                int? bomaId = _location?.BomaId;


                Component2Service service = new Component2Service();

                HashSet<string> householdNumbers = service.GetValidHouseholdNumberValues(stateId, countyId, payamId, bomaId);
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Business Plan Development Template");

                    // Adding the three columns
                    worksheet.Cells[1, 1].Value = "Household Number";
                    worksheet.Cells[1, 2].Value = "BusinessPlanStatus";
                    worksheet.Cells[1, 3].Value = "BusinessType";

                    // Set headers to be bold
                    for (int col = 1; col <= 3; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    // Pre-fill the Household Number column with the fetched household numbers
                    var householdNumberList = householdNumbers.ToList();
                    if (householdNumberList.Count > 0)
                    {
                        for (int row = 0; row < householdNumberList.Count; row++)
                        {
                            worksheet.Cells[row + 2, 1].Value = householdNumberList[row]; // +2 to skip the header row
                        }

                        // Lock the first column (Household Number) and make it read-only
                        worksheet.Cells["A2:A" + (householdNumberList.Count + 1)].Style.Locked = true;
                    }

                    // Define options for the drop-downs in columns 2 and 3
                    string[] yesNoOptions = { "Yes", "No" };
                    string[] farmNonFarmOptions = { "Farm", "Non-Farm" };

                    // Add drop-down for the Business Plan Status field (Column 2)
                    var businessPlanStatusValidation = worksheet.DataValidations.AddListValidation("B2:B1000");
                    foreach (string option in yesNoOptions)
                    {
                        businessPlanStatusValidation.Formula.Values.Add(option);
                    }

                    // Add drop-down for the Business Type field (Column 3)
                    var businessTypeValidation = worksheet.DataValidations.AddListValidation("C2:C1000");
                    foreach (string option in farmNonFarmOptions)
                    {
                        businessTypeValidation.Formula.Values.Add(option);
                    }

                    // Protect the worksheet so that locked cells (Household Number) are read-only
                    worksheet.Protection.IsProtected = true;

                    // Unlock other cells so they can be edited by the user
                    worksheet.Cells["B2:C1000"].Style.Locked = false;

                    // Auto-fit columns to adjust the size
                    worksheet.Cells.AutoFitColumns();

                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=BusinessPlanDevelopmentTemplate.xlsx");

                    // Write the file to the response stream
                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.Flush();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the error if needed
                Debug.WriteLine($"Error: {ex}");
            }
        }



        private void FinancialServiceLinkageTrackingTemplate()
        {
            try
            {
                int? stateId = _location?.StateId;
                int? countyId = _location?.CountyId;
                int? payamId = _location?.PayamId;
                int? bomaId = _location?.BomaId;


                Component2Service service = new Component2Service();

                HashSet<string> householdNumbers = service.GetValidHouseholdNumberValues(stateId, countyId, payamId, bomaId);
                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Financial Services Linkage Template");

                    // Adding the three columns
                    worksheet.Cells[1, 1].Value = "Household Number";
                    worksheet.Cells[1, 2].Value = "Connected Financial Services";
                    worksheet.Cells[1, 3].Value = "Entity Name";

                    // Set headers to be bold
                    for (int col = 1; col <= 3; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    // Pre-fill the Household Number column with the fetched household numbers
                    var householdNumberList = householdNumbers.ToList();
                    if (householdNumberList.Count > 0)
                    {
                        for (int row = 0; row < householdNumberList.Count; row++)
                        {
                            worksheet.Cells[row + 2, 1].Value = householdNumberList[row]; // +2 to skip the header row
                        }

                        // Lock the first column (Household Number) and make it read-only
                        worksheet.Cells["A2:A" + (householdNumberList.Count + 1)].Style.Locked = true;
                    }

                    // Define options for the drop-down in the 2nd column
                    string[] yesNoOptions = { "Yes", "No" };

                    // Add drop-down for the Connected Financial Services field (Column 2)
                    var connectedFinancialServicesValidation = worksheet.DataValidations.AddListValidation("B2:B1000");
                    foreach (string option in yesNoOptions)
                    {
                        connectedFinancialServicesValidation.Formula.Values.Add(option);
                    }

                    // Protect the worksheet so that locked cells (Household Number) are read-only
                    worksheet.Protection.IsProtected = true;

                    // Unlock other cells so they can be edited by the user
                    worksheet.Cells["B2:C1000"].Style.Locked = false;

                    // Auto-fit columns to adjust the size
                    worksheet.Cells.AutoFitColumns();

                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=FinancialServicesLinkageTrackingTemplate.xlsx");

                    // Write the file to the response stream
                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.Flush();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the error if needed
                Debug.WriteLine($"Error: {ex}");
            }
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
                if (dt == null) throw new Exception("Report Summary is missing.");

                // Generate PDF from the DataTable
                GeneratePDF(dt);
            }
            catch (Exception ex)
            {
                LogError("Error in MNEModulePDFHandler: " + ex.Message, ex);
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

        // Cash Transfer pdf started
        private void CashTransferPdfHandler()
        {
            try
            {
                // Retrieve the CashTransferSummaryData object from the session
                var cashTransferSummaryData = Session["CashTransferSummary"] as CashTransferSummaryMNE.CashTransferSummaryData;

                if (cashTransferSummaryData == null || cashTransferSummaryData.ReportData == null)
                    throw new Exception("Cash Transfer Summary data is missing.");

                // Generate PDF using the DataTable from the CashTransferSummaryData
                GenerateCPDF(cashTransferSummaryData);
            }
            catch (Exception ex)
            {
                LogError("Error in Cash Transfer Summary: " + ex.Message, ex);
                throw;
            }
        }

        private void GenerateCPDF(CashTransferSummaryMNE.CashTransferSummaryData cashTransferSummaryData)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Set the document size to legal and rotate it to landscape for more width
                using (Document document = new Document(PageSize.LEGAL.Rotate(), 10f, 10f, 10f, 10f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();

                    // Set the font and size
                    Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);
                    Font font = FontFactory.GetFont(FontFactory.HELVETICA, 8);
                    Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);

                    // Add the title at the top
                    Paragraph title = new Paragraph("Cash Transfer Summary", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    document.Add(title);

                    document.Add(new Paragraph(" ")); // Add a space after the title

                    // Display the totals from CashTransferSummaryData above the table
                    Paragraph totalPaidBeneficiaries = new Paragraph("Total Paid Beneficiaries: " + cashTransferSummaryData.TotalPaidBeneficiaries, headerFont);
                    Paragraph totalUnpaidBeneficiaries = new Paragraph("Total Unpaid Beneficiaries: " + cashTransferSummaryData.TotalUnpaidBeneficiaries, headerFont);
                    Paragraph totalPaidAmount = new Paragraph("Total Paid Amount(USD): " + cashTransferSummaryData.TotalPaidAmount.ToString("N2"), headerFont);

                    document.Add(totalPaidBeneficiaries);
                    document.Add(totalUnpaidBeneficiaries);
                    document.Add(totalPaidAmount);
                    document.Add(new Paragraph(" ")); // Add a space before the table

                    // Retrieve the DataTable from CashTransferSummaryData
                    DataTable dt = cashTransferSummaryData.ReportData;

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

                    // Add data rows
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
                Response.AddHeader("Content-Disposition", "attachment; filename=CashTransferSummary.pdf");
                Response.BinaryWrite(ms.ToArray());
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }



        // Cash Transfer pdf ended

        // Cash Transfer Download started
        // Cash Transfer Excel Download started
        private void CashTransferxlHandler()
        {
            try
            {
                // Retrieve the CashTransferSummaryData object from the session
                var cashTransferSummaryData = Session["CashTransferSummary"] as CashTransferSummaryMNE.CashTransferSummaryData;

                if (cashTransferSummaryData == null || cashTransferSummaryData.ReportData == null)
                    throw new Exception("Cash Transfer Summary data is missing.");

                // Generate Excel using the DataTable from the CashTransferSummaryData
                GenerateCExcel(cashTransferSummaryData);
            }
            catch (Exception ex)
            {
                LogError("Error in Cash Transfer Summary: " + ex.Message, ex);
            }
        }

        private void GenerateCExcel(CashTransferSummaryMNE.CashTransferSummaryData cashTransferSummaryData)
        {
            using (ExcelPackage package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Set the font for headers
                worksheet.Cells.Style.Font.Size = 12;
                worksheet.Cells.Style.Font.Name = "Arial";

                // Add the title at the top
                worksheet.Cells["A1"].Value = "Cash Transfer Summary";
                worksheet.Cells["A1"].Style.Font.Bold = true;
                worksheet.Cells["A1"].Style.Font.Size = 16;
                worksheet.Cells["A1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Merge cells for title
                worksheet.Cells["A1:B1"].Merge = true;

                // Add the summary totals below the title
                worksheet.Cells["A3"].Value = "Total Paid Beneficiaries";
                worksheet.Cells["B3"].Value = cashTransferSummaryData.TotalPaidBeneficiaries;

                worksheet.Cells["A4"].Value = "Total Unpaid Beneficiaries";
                worksheet.Cells["B4"].Value = cashTransferSummaryData.TotalUnpaidBeneficiaries;

                worksheet.Cells["A5"].Value = "Total Paid Amount(USD)";
                worksheet.Cells["B5"].Value = cashTransferSummaryData.TotalPaidAmount.ToString("N2");

                // Leave an empty row before adding the table headers and data
                int startRow = 7;

                // Retrieve the DataTable from CashTransferSummaryData
                DataTable dt = cashTransferSummaryData.ReportData;

                // Add the headers
                for (int col = 0; col < dt.Columns.Count; col++)
                {
                    worksheet.Cells[startRow, col + 1].Value = dt.Columns[col].ColumnName;
                    worksheet.Cells[startRow, col + 1].Style.Font.Bold = true;
                }

                // Add the data
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        // Handle potential null or DBNull values gracefully
                        var cellValue = dt.Rows[row][col];
                        worksheet.Cells[startRow + row + 1, col + 1].Value = cellValue == DBNull.Value ? "" : cellValue.ToString();
                    }
                }

                // Auto-fit columns for all cells
                worksheet.Cells.AutoFitColumns();

                // Set other styling options as needed (e.g., borders, alignment, number formats)

                byte[] fileContents = package.GetAsByteArray();

                // Write the stream to the response
                Response.ClearHeaders();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("Content-Disposition", "attachment; filename = CashTransferSummary.xlsx");
                Response.AddHeader("Content-Length", fileContents.Length.ToString());

                Response.BinaryWrite(fileContents);
                Response.Flush();
                Response.Close();
            }
        }


        // Cash Transfer Download ended

        //Household group view excel template Started
        private void HouseholdTemplateExlTemplate()
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Create a new worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Household Group Template");

                    // Add headers to the worksheet (reflecting your table schema)
                    worksheet.Cells[1, 1].Value = "Group Name";
                    worksheet.Cells[1, 2].Value = "Latitude";
                    worksheet.Cells[1, 3].Value = "Longitude";
                    /*worksheet.Cells[1, 4].Value = "Created By";
                    worksheet.Cells[1, 5].Value = "Created Date";
                    worksheet.Cells[1, 6].Value = "Modified By";
                    worksheet.Cells[1, 7].Value = "Modified Date";*/

                    // Set headers to be bold
                    for (int col = 1; col <= 3; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }


                    // Add data validation for Latitude (between -90 and 90)
                    var latitudeValidation = worksheet.DataValidations.AddDecimalValidation(worksheet.Cells[2, 2, 1048576, 2].Address);
                    latitudeValidation.ShowErrorMessage = true;
                    latitudeValidation.ErrorTitle = "Invalid Latitude";
                    latitudeValidation.Error = "Please enter a latitude between -90 and 90.";
                    latitudeValidation.Formula.Value = -90;
                    latitudeValidation.Formula2.Value = 90;

                    // Add data validation for Longitude (between -180 and 180)
                    var longitudeValidation = worksheet.DataValidations.AddDecimalValidation(worksheet.Cells[2, 3, 1048576, 3].Address);
                    longitudeValidation.ShowErrorMessage = true;
                    longitudeValidation.ErrorTitle = "Invalid Longitude";
                    longitudeValidation.Error = "Please enter a longitude between -180 and 180.";
                    longitudeValidation.Formula.Value = -180;
                    longitudeValidation.Formula2.Value = 180;

                    worksheet.Cells.AutoFitColumns();
                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=HouseholdGroupViewTemplate.xlsx");

                    // Write the file to the response stream
                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.Flush();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the error if needed
                System.Diagnostics.Debug.WriteLine($"Error: {ex}");
            }
        }


        //Household group view excel template Ended


        //Training and Attendance Tracking Template Start
        private void TrainingAttendanceTrackingTemplate()
        {
            try
            {
                int? stateId = _location?.StateId;
                int? countyId = _location?.CountyId;
                int? payamId = _location?.PayamId;
                int? bomaId = _location?.BomaId;


                Component2Service service = new Component2Service();
                HashSet<string> householdNumbers = service.GetValidHouseholdNumberValues(stateId, countyId, payamId, bomaId);

                using (ExcelPackage package = new ExcelPackage())
                {
                    // Create a new worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Training Attendance Tracking Template");

                    // Add headers to the worksheet (reflecting your table schema)
                    worksheet.Cells[1, 1].Value = "Household Number";
                    worksheet.Cells[1, 2].Value = "Course Code";
                    worksheet.Cells[1, 3].Value = "Course Duration";
                    worksheet.Cells[1, 4].Value = "Course Attended";
                    worksheet.Cells[1, 5].Value = "Course Held";
                    worksheet.Cells[1, 6].Value = "Classes Missed";
                    worksheet.Cells[1, 7].Value = "Why Missed";

                    // Set headers to be bold
                    for (int col = 1; col <= 7; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    var householdNumberList = householdNumbers.ToList(); // Convert HashSet to List
                    
                    if (householdNumberList.Count > 0)  // Check if there are any items in the list
                    {
                        for (int row = 0; row < householdNumberList.Count; row++)
                        {
                            worksheet.Cells[row + 2, 1].Value = householdNumberList[row]; // +2 to skip the header row
                        }

                        // Lock the first column (Household Number) and make it read-only
                        worksheet.Cells["A2:A" + (householdNumberList.Count + 1)].Style.Locked = true;  // Ensure valid range
                    }

                    // Add dropdown for Course Code in column B (2nd column)
                    var courseCodes = new List<string> { "", "FINLIT", "LIVETECH", "LIFESKILL", "SAVINGS" }; // Empty string for blank option
                    var validation = worksheet.DataValidations.AddListValidation("B2:B100"); // Adjust the range as needed
                    foreach (var courseCode in courseCodes)
                    {
                        validation.Formula.Values.Add(courseCode); // Adds each course code including the blank option
                    }

                    validation.ShowErrorMessage = false;
                    validation.ErrorTitle = "Invalid Selection";
                    validation.Error = "Please select a course code from the list.";

                    worksheet.Protection.IsProtected = true;
                    worksheet.Cells["B2:G1000"].Style.Locked = false;
                    worksheet.Cells.AutoFitColumns();

                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=TrainingAndAttendanceTrackingTemplate.xlsx");

                    // Write the file to the response stream
                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.Flush();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the error if needed
                System.Diagnostics.Debug.WriteLine($"Error: {ex}");
            }
        }

        //Training and Attendance Tracking Template End

        //BeneficiaryRandomizationEnrollmentTemplate start
        private void BeneficiaryRandomizationEnrollmentTemplate()
        {
            try
            {
                int? stateId = _location?.StateId;
                int? countyId = _location?.CountyId;
                int? payamId = _location?.PayamId;
                int? bomaId = _location?.BomaId;


                Component2Service service = new Component2Service();

                // Call the new method to get household numbers along with group info
                List<HouseholdGroupLinkageModel> householdGroupData = service.GetMappedHouseholdNumberValues(stateId, countyId, payamId, bomaId);

                using (ExcelPackage package = new ExcelPackage())
                {
                    // Create the worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Beneficiary Enrollment Template");

                    // Define the headers
                    worksheet.Cells[1, 1].Value = "Household Number";
                    worksheet.Cells[1, 2].Value = "Interested In Participating";
                    worksheet.Cells[1, 3].Value = "Survives Targeting";
                    worksheet.Cells[1, 4].Value = "Treatment Arm";
                    worksheet.Cells[1, 5].Value = "HH Enrollment Status";
                    worksheet.Cells[1, 6].Value = "Group ID";
                    worksheet.Cells[1, 7].Value = "Group Name";
                    worksheet.Cells[1, 8].Value = "Enrolled Member ID";

                    // Set headers to be bold
                    for (int col = 1; col <= 8; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    // Pre-fill the Household Number, Group ID, and Group Name columns with the fetched data
                    for (int row = 0; row < householdGroupData.Count; row++)
                    {
                        var data = householdGroupData[row];

                        worksheet.Cells[row + 2, 1].Value = data.HouseholdNumber; // Household Number
                        worksheet.Cells[row + 2, 6].Value = data.GroupId.HasValue ? data.GroupId.Value.ToString() : ""; // Group ID
                        worksheet.Cells[row + 2, 7].Value = !string.IsNullOrEmpty(data.GroupName) ? data.GroupName : ""; // Group Name
                    }

                    // Lock the Household Number, Group ID, and Group Name columns (Columns 1, 6, and 7)
                    worksheet.Cells["A2:A" + (householdGroupData.Count + 1)].Style.Locked = true;
                    worksheet.Cells["F2:F" + (householdGroupData.Count + 1)].Style.Locked = true;
                    worksheet.Cells["G2:G" + (householdGroupData.Count + 1)].Style.Locked = true;

                    // Define the Yes/No options for the drop-downs
                    string[] yesNoOptions = { "Yes", "No" };
                    string[] treatmentArm = { "Treatment", "Control" };

                    // Add drop-down for the InterestedInParticipating field (Column 2)
                    var validation1 = worksheet.DataValidations.AddListValidation("B2:B1000");
                    foreach (string option in yesNoOptions)
                    {
                        validation1.Formula.Values.Add(option);
                    }

                    // Add drop-down for the SurvivesTargeting field (Column 3)
                    var validation2 = worksheet.DataValidations.AddListValidation("C2:C1000");
                    foreach (string option in yesNoOptions)
                    {
                        validation2.Formula.Values.Add(option);
                    }

                    // Add drop-down for the TreatmentArm field (Column 4)
                    var validation3 = worksheet.DataValidations.AddListValidation("D2:D1000");
                    foreach (string option in treatmentArm)
                    {
                        validation3.Formula.Values.Add(option);
                    }

                    // Add drop-down for the HHEnrollmentStatus field (Column 5)
                    var validation4 = worksheet.DataValidations.AddListValidation("E2:E1000");
                    foreach (string option in yesNoOptions)
                    {
                        validation4.Formula.Values.Add(option);
                    }

                    // Protect the worksheet so that locked cells (Household Number, Group ID, Group Name) are read-only
                    worksheet.Protection.IsProtected = true;

                    // Unlock other cells so they can be edited by the user
                    worksheet.Cells["B2:E1000"].Style.Locked = false;
                    worksheet.Cells["H2:H1000"].Style.Locked = false; // Allow users to edit "Enrolled Member ID"

                    // Auto-fit columns to adjust the size
                    worksheet.Cells.AutoFitColumns();

                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=BeneficiaryEnrollmentTemplate.xlsx");

                    // Write the file to the response stream
                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.Flush();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the error if needed
                Debug.WriteLine($"Error: {ex}");
            }
        }



        private void LivelihoodGrantDisbursementTemplate()
        {
            try
            {
                int? stateId = _location?.StateId;
                int? countyId = _location?.CountyId;
                int? payamId = _location?.PayamId;
                int? bomaId = _location?.BomaId;



                Component2Service service = new Component2Service();
                HashSet<string> householdNumbers = service.GetValidHouseholdNumberValues(stateId, countyId, payamId, bomaId);

                using (ExcelPackage package = new ExcelPackage())
                {
                    // Create a new worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Livelihood Grant Disbursement Template");

                    // Add headers to the worksheet (reflecting your table schema)
                    worksheet.Cells[1, 1].Value = "Household Number";
                    worksheet.Cells[1, 2].Value = "Payment Cycle";
                    worksheet.Cells[1, 3].Value = "Payment Amount";
                    worksheet.Cells[1, 4].Value = "Payment Date" + "\n" + "(mm-dd-yyyy)";
                    worksheet.Cells[1, 4].Style.WrapText = true; 

                    // Set headers to be bold
                    for (int col = 1; col <= 4; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    var householdNumberList = householdNumbers.ToList(); // Convert HashSet to List

                    if (householdNumberList.Count > 0)  // Check if there are any items in the list
                    {
                        for (int row = 0; row < householdNumberList.Count; row++)
                        {
                            worksheet.Cells[row + 2, 1].Value = householdNumberList[row]; // +2 to skip the header row
                        }

                        // Lock the first column (Household Number) and make it read-only
                        worksheet.Cells["A2:A" + (householdNumberList.Count + 1)].Style.Locked = true;  // Ensure valid range
                    }

                    // Suppose we know the number of data rows to be inserted
                    // In this example, assume we are inserting 10 rows of data dynamically
                    int dataRows = 20; // Replace this with the actual number of data rows to be inserted

                    // Add a dropdown for "Payment Cycle" (column 2)
                    var paymentCycleValidation = worksheet.DataValidations.AddListValidation(worksheet.Cells[2, 2, dataRows + 1, 2].Address);  // Apply to data rows only
                    paymentCycleValidation.ShowErrorMessage = true;
                    paymentCycleValidation.AllowBlank = true;
                    paymentCycleValidation.ErrorTitle = "Invalid Payment Cycle";
                    paymentCycleValidation.Error = "Please select 'Installment One' or 'Installment Two'.";
                    paymentCycleValidation.Formula.Values.Add("Installment One");
                    paymentCycleValidation.Formula.Values.Add("Installment Two");

                    // Set the format for "Payment Date" as DateTime for the actual data rows
                    worksheet.Cells[2, 4, dataRows + 1, 4].Style.Numberformat.Format = "MM-dd-yyyy"; // or any DateTime format

                    // Apply data validation for the "Payment Date" column (only to the actual data rows)
                    var dateValidation = worksheet.DataValidations.AddDateTimeValidation(worksheet.Cells[2, 4, dataRows + 1, 4].Address);
                    dateValidation.ShowErrorMessage = true;
                    dateValidation.ErrorTitle = "Invalid Date";
                    dateValidation.Error = "Please enter a valid date in the format dd-mm-yyyy.";
                    dateValidation.Prompt = "Select a date or enter manually in the format dd-mm-yyyy";
                    dateValidation.AllowBlank = true;
                    dateValidation.Formula.Value = new DateTime(2000, 1, 1); // Minimum allowed date
                    dateValidation.Formula2.Value = new DateTime(2100, 12, 31); // Maximum allowed date

                    worksheet.Protection.IsProtected = true;
                    worksheet.Cells["B2:G1000"].Style.Locked = false;

                    // Auto-fit columns for better appearance
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(4).Width = 15;

                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=LivelihoodGrantDisbursementTemplate.xlsx");

                    // Write the file to the response stream
                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.Flush();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                // Log the error if needed
                System.Diagnostics.Debug.WriteLine($"Error: {ex}");
            }
        }

        private void EconomicOppontunityAuditRecordDownload()
        {
            // Retrieve the selected audit table and data from the session
            string selectedAuditTable = Session["SelectedAuditTable"] as string;
            DataTable auditRecords = Session["AuditRecordData"] as DataTable;

            // Step 1: Check if session data is still valid
            if (string.IsNullOrEmpty(selectedAuditTable) || auditRecords == null || auditRecords.Rows.Count == 0)
            {
                // If no data, respond with a message or handle the error gracefully
                Response.Clear();
                Response.Write("No data available for download.");
                Response.End();
                return;
            }

            // Step 2: Create the PDF document in Landscape orientation
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 10f);  // Rotate A4 to Landscape
            MemoryStream memoryStream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            pdfDoc.Open();

            // Step 3: Add title to the PDF document
            Paragraph title = new Paragraph("Audit Record Report - " + selectedAuditTable);
            title.Alignment = Element.ALIGN_CENTER;
            title.SpacingAfter = 20f;
            pdfDoc.Add(title);

            // Step 4: Create a table with the same number of columns as the DataTable
            PdfPTable pdfTable = new PdfPTable(auditRecords.Columns.Count)
            {
                WidthPercentage = 100,
                HorizontalAlignment = Element.ALIGN_LEFT
            };

            // Step 5: Set table column headers dynamically from the DataTable
            foreach (DataColumn column in auditRecords.Columns)
            {
                PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, FontFactory.GetFont("Arial", 10, Font.BOLD)))
                {
                    BackgroundColor = BaseColor.LIGHT_GRAY,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                pdfTable.AddCell(headerCell);
            }

            // Step 6: Add rows to the PDF table dynamically from the DataTable
            foreach (DataRow row in auditRecords.Rows)
            {
                foreach (var cellData in row.ItemArray)
                {
                    PdfPCell dataCell = new PdfPCell(new Phrase(cellData?.ToString() ?? "", FontFactory.GetFont("Arial", 10)))
                    {
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };
                    pdfTable.AddCell(dataCell);
                }
            }

            // Step 7: Add the table to the PDF document
            pdfDoc.Add(pdfTable);

            // Step 8: Close the document
            pdfDoc.Close();

            // Step 9: Send the PDF as a response for download
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=AuditRecord_" + selectedAuditTable + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(memoryStream.ToArray());
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }


        private void LogError(string v, Exception ex)
        {
            throw new NotImplementedException();
        }


    }
}