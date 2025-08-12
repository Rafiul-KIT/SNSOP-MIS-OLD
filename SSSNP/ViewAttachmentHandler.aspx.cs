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
using System.Configuration;
using SSSNP_SNSOP.Core.Model.Component2;

namespace SSSNP_SNSOP
{
    public partial class ViewAttachmentHandler : System.Web.UI.Page
    {
        private readonly string _queryValue = "AttachmentValue";
        private readonly string _queryType = "AttachmentType";
        private HandlerAttachment _type;
        private object _attachmentValue;
        private GeoLocationDto _location;
        private General genDbAccess = new General();

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
            ECONOMIC_OPPORTUNITY_AUDIT_RECORD_DOWNLOAD = 22,
            MNE_REPORT_DOWNLOAD = 23,
            ID_CARD_DOWNLOAD = 24,
            PAYROLL_RECONCILE = 30,
            COURSE_ENTITY_TEMPLATE = 31,
            COURSE_DETAILS_TEMPLATE = 32
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
                    TrainingAttendanceTrackingTemplate(this.GetAttachmentValue);
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
                case HandlerAttachment.PAYROLL_RECONCILE:
                    PayrollReconcileTemplate();
                    return "OK";
                case HandlerAttachment.MNE_REPORT_DOWNLOAD:
                    DownloadFile(GetAttachmentValueAsInt);
                    return "OK";
                case HandlerAttachment.ID_CARD_DOWNLOAD:
                    DownloadIdCard(GetAttachmentValueAsInt);
                    return "OK";
                case HandlerAttachment.COURSE_ENTITY_TEMPLATE:
                    CourseEntityTemplate();
                    return "OK";
                case HandlerAttachment.COURSE_DETAILS_TEMPLATE:
                    CourseDetailsTemplate();
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
                    worksheet.Cells[1, 2].Value = "Business Plan Status";
                    worksheet.Cells[1, 3].Value = "Business Type";
                    worksheet.Cells[1, 4].Value = "Plan Submitted";
                    worksheet.Cells[1, 5].Value = "Plan Screened";
                    worksheet.Cells[1, 6].Value = "Plan Approved";
                    worksheet.Cells[1, 7].Value = "Why Rejected";
                    worksheet.Cells[1, 8].Value = "Business Description";
                    worksheet.Cells[1, 9].Value = "How Spent";
                    worksheet.Cells[1, 10].Value = "Proportions of Meetings in Person (%)";
                    worksheet.Cells[1, 11].Value = "Proportions of Meetings in Group (%)";

                    // Set headers to be bold
                    for (int col = 1; col <= 11; col++)
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

                    // Yes/No dropdowns for D, E, F
                    var planSubmittedValidation = worksheet.DataValidations.AddListValidation("D2:D1000");
                    foreach (var option in yesNoOptions) planSubmittedValidation.Formula.Values.Add(option);

                    var planScreenedValidation = worksheet.DataValidations.AddListValidation("E2:E1000");
                    foreach (var option in yesNoOptions) planScreenedValidation.Formula.Values.Add(option);

                    var planApprovedValidation = worksheet.DataValidations.AddListValidation("F2:F1000");
                    foreach (var option in yesNoOptions) planApprovedValidation.Formula.Values.Add(option);

                    var whyRejectedValidation = worksheet.DataValidations.AddCustomValidation("G2:G1000");
                    whyRejectedValidation.Formula.ExcelFormula = "OR($F2=\"No\",LEN($G2)=0)";
                    whyRejectedValidation.ShowErrorMessage = true;
                    whyRejectedValidation.ErrorTitle = "Invalid Entry";
                    whyRejectedValidation.Error = "Only fill this if 'Plan Approved' = 'No'. Leave it blank otherwise.";
                    whyRejectedValidation.ErrorStyle = OfficeOpenXml.DataValidation.ExcelDataValidationWarningStyle.stop;


                    // Percentage validations for In-Person and Group columns
                    var inPersonPercentageValidation = worksheet.DataValidations.AddDecimalValidation("J2:J1000");
                    inPersonPercentageValidation.Operator = OfficeOpenXml.DataValidation.ExcelDataValidationOperator.between;
                    inPersonPercentageValidation.Formula.Value = 0;
                    inPersonPercentageValidation.Formula2.Value = 100;
                    inPersonPercentageValidation.ShowErrorMessage = true;
                    inPersonPercentageValidation.Error = "Enter a number between 0 and 100.";

                    var groupPercentageValidation = worksheet.DataValidations.AddCustomValidation("K2:K1000");
                    groupPercentageValidation.Formula.ExcelFormula =
                        "OR(AND(ISNUMBER(J2),ISNUMBER(K2),J2>=0,J2<=100,K2>=0,K2<=100,J2+K2=100),AND(J2=\"\",K2=\"\"))";
                    groupPercentageValidation.ShowErrorMessage = true;
                    groupPercentageValidation.Error = "Enter 0–100 and ensure In-Person % + Group % = 100 (or leave both blank).";


                    // Optional formatting
                    worksheet.Cells["J2:J1000"].Style.Numberformat.Format = "0.00";
                    worksheet.Cells["K2:K1000"].Style.Numberformat.Format = "0.00";

                    // Protect the worksheet so that locked cells (Household Number) are read-only
                    worksheet.Protection.IsProtected = true;

                    // Unlock other cells so they can be edited by the user
                    worksheet.Cells["B2:K1000"].Style.Locked = false;

                    // Auto-fit columns to adjust the size
                    worksheet.Cells.AutoFitColumns();

                    // Set the content type and headers for the download
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=BusinessPlanDevelopmentTemplate.xlsx");
                    Response.BinaryWrite(package.GetAsByteArray());
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                    return;
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
                    worksheet.Cells[1, 4].Value = "Financial Service Connection Date";
                    worksheet.Cells[1, 5].Value = "Deposit Account Opened";
                    worksheet.Cells[1, 6].Value = "Deposit Account Opening Date";
                    worksheet.Cells[1, 7].Value = "Account Last Activity (Deposit/Withdraw) Date";

                    // Set headers to be bold
                    for (int col = 1; col <= 7; col++)
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

                    // Add drop-down for the Deposit Account Opened field (Column 5)
                    var depositAccountOpenedValidation = worksheet.DataValidations.AddListValidation("E2:E1000");
                    foreach (string option in yesNoOptions)
                    {
                        depositAccountOpenedValidation.Formula.Values.Add(option);
                    }

                    // Format date columns for consistency (Columns 4, 6, and 7)
                    worksheet.Cells["D2:D1000"].Style.Numberformat.Format = "mm-dd-yyyy";
                    worksheet.Cells["F2:F1000"].Style.Numberformat.Format = "mm-dd-yyyy";
                    worksheet.Cells["G2:G1000"].Style.Numberformat.Format = "mm-dd-yyyy";

                    // Protect the worksheet so that locked cells (Household Number) are read-only
                    worksheet.Protection.IsProtected = true;

                    // Unlock other cells so they can be edited by the user
                    worksheet.Cells["B2:G1000"].Style.Locked = false;

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

        private int GetAttachmentValueAsInt
        {

            get
            {
                try
                {
                    return Convert.ToInt32(Request.QueryString[_queryValue]);
                }
                catch
                {
                    return 0; // or handle default as appropriate
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

                    string SanitizeFileNamePart(string input)
                    {
                        var invalidChars = System.IO.Path.GetInvalidFileNameChars();
                        return new string(input.Where(c => !invalidChars.Contains(c)).ToArray()).Replace(" ", "_");
                    }

                    // Get and sanitize boma
                    string boma = Session["SelectedBomaName"]?.ToString();
                    boma = !string.IsNullOrWhiteSpace(boma) ? SanitizeFileNamePart(boma) : null;

                    // Get and sanitize paymentCycle
                    string paymentCycle = Session["SelectedPaymentCycle"]?.ToString();
                    paymentCycle = !string.IsNullOrWhiteSpace(paymentCycle) ? SanitizeFileNamePart(paymentCycle) : null;

                    // Get and sanitize Support Type
                    string supportType = Session["SelectedSupportType"]?.ToString();
                    supportType = !string.IsNullOrWhiteSpace(supportType) ? SanitizeFileNamePart(supportType) : null;

                    // Build filename parts
                    List<string> parts = new List<string>();
                    if (!string.IsNullOrEmpty(boma)) parts.Add(boma);
                    if (!string.IsNullOrEmpty(paymentCycle)) parts.Add(paymentCycle);
                    if (!string.IsNullOrEmpty(supportType)) parts.Add(supportType);

                    // Combine parts + extension
                    string fileName = (parts.Count > 0 ? string.Join("_", parts) : "PayrollData") + ".bin";

                    // Set header
                    Response.ClearHeaders();
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
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

        private string DecryptData(byte[] encryptedData, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedData))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted data as a string
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
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
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                // Attach the custom PageNumberHelper to the writer
                writer.PageEvent = new PageNumberHelper();

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

                // Create table with 19 columns for payroll details
                PdfPTable table = new PdfPTable(18)
                {
                    WidthPercentage = 100
                };

                // Adjust column widths: ID and Age reduced, Signature increased
                table.SetWidths(new float[] {
            1f, 1.5f, 1.3f, 1f, 1f,          // Beneficiary (5 columns: Photo, ID, Name, Gender, Age)
            1f, 1.5f, 1.3f, 1f, 1f,          // First Alternate (5 columns: Photo, ID, Name, Gender, Age)
            1.2f, 1.2f, 1f, 1f, 1f, 1f, 1f,   // Payment (3 columns: Cycle, Support Type, Wage Total)
            2f                                 // Signature (1 column)
        });
                // Add section headers
                AddSectionHeaderToPdfTable(table, "Beneficiary", 5);
                AddSectionHeaderToPdfTable(table, "Alternate", 5);
                //AddSectionHeaderToPdfTable(table, "Second Alternate", 5);
                AddSectionHeaderToPdfTable(table, "Payment", 7);
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

                // AddCellToPdfTableHeader(table, "ID");
                // AddCellToPdfTableHeader(table, "Photo");
                // AddCellToPdfTableHeader(table, "Name");
                // AddCellToPdfTableHeader(table, "Gender");
                // AddCellToPdfTableHeader(table, "Age");

                AddCellToPdfTableHeader(table, "Cycle");
                AddCellToPdfTableHeader(table, "Support Type");
                AddCellToPdfTableHeader(table, "Total Attendance"); 
                AddCellToPdfTableHeader(table, "Wage Rate (USD)");
                AddCellToPdfTableHeader(table, "Wage Total");
                AddCellToPdfTableHeader(table, "Exchange Rate At Time");
                AddCellToPdfTableHeader(table, "Wage Rate SSP Total");

                AddCellToPdfTableHeader(table, "Sign");

                table.HeaderRows = 2;

                // Combine all payroll data from all containers and sort by beneficiary name
                List<PDPayrollDataDto> sortedPayrollData = apiResponse.data
                    .SelectMany(container => container.payrollData)
                    .GroupBy(payroll => payroll.supportType) // Grouping by Support Type (DIS, LIPW, etc.)
                    .OrderBy(group => group.Key) // Sort groups alphabetically (DIS first, then LIPW, etc.)
                    .SelectMany(group => group.OrderBy(payroll =>
                        payroll.houseHoldDetail.firstName +
                        payroll.houseHoldDetail.middleName +
                        payroll.houseHoldDetail.lastName)) // Sorting within each group
                    .ToList();

                // Iterate through payroll data in the container and add rows
                foreach (var payroll in sortedPayrollData)
                {
                    bool hasTwoAlternates = payroll.alternates.Count > 1;

                    // Beneficiary Details
                    AddCellToPdfTableBody(table, payroll.intBenid.ToString(), false, hasTwoAlternates);
                    string beneficiaryPhoto = payroll.houseHoldDetail.biometric != null ? payroll.houseHoldDetail.biometric.Photo : null;
                    AddPhotoToPdfTable(table, beneficiaryPhoto, hasTwoAlternates);
                    AddCellToPdfTableBody(table, $"{payroll.houseHoldDetail.firstName}\n{payroll.houseHoldDetail.middleName}\n{payroll.houseHoldDetail.lastName}", true, hasTwoAlternates);
                    AddCellToPdfTableBody(table, GetGenderString(payroll.houseHoldDetail.gender), false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.houseHoldDetail.age.ToString(), false, hasTwoAlternates);

                    if (payroll.alternates.Count == 1)
                    {
                        // Single Alternate (occupies full height)
                        var alternate = payroll.alternates[0];

                        AddCellToPdfTableBody(table, alternate.id.ToString(), false, hasTwoAlternates);
                        AddPhotoToPdfTable(table, alternate.biometric?.Photo, hasTwoAlternates);
                        AddCellToPdfTableBody(table, $"{alternate.firstName}\n{alternate.middleName}\n{alternate.lastName}", true, hasTwoAlternates);
                        AddCellToPdfTableBody(table, GetGenderString(alternate.gender), false, hasTwoAlternates);
                        AddCellToPdfTableBody(table, alternate.age.ToString(), false, hasTwoAlternates);
                    }
                    else if (payroll.alternates.Count > 1)
                    {
                        // Two Alternates (stacked in the same columns)
                        var firstAlternate = payroll.alternates[0];
                        var secondAlternate = payroll.alternates[1];

                        // ID Column
                        PdfPTable idTable = new PdfPTable(1);
                        idTable.WidthPercentage = 100;
                        idTable.AddCell(new PdfPCell(new Phrase(firstAlternate.id.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        idTable.AddCell(new PdfPCell(new Phrase(secondAlternate.id.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        table.AddCell(new PdfPCell(idTable) { FixedHeight = 100f });

                        // Photo Column
                        PdfPTable photoTable = new PdfPTable(1);
                        photoTable.WidthPercentage = 100;
                        AddPhotoToPdfTable(photoTable, firstAlternate.biometric?.Photo, hasTwoAlternates);
                        AddPhotoToPdfTable(photoTable, secondAlternate.biometric?.Photo, hasTwoAlternates);
                        table.AddCell(new PdfPCell(photoTable) { FixedHeight = 100f });

                        // Name Column
                        PdfPTable nameTable = new PdfPTable(1);
                        nameTable.WidthPercentage = 100;
                        nameTable.AddCell(new PdfPCell(new Phrase($"{firstAlternate.firstName}\n{firstAlternate.middleName}\n{firstAlternate.lastName}",
                            FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        nameTable.AddCell(new PdfPCell(new Phrase($"{secondAlternate.firstName}\n{secondAlternate.middleName}\n{secondAlternate.lastName}",
                            FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        table.AddCell(new PdfPCell(nameTable) { FixedHeight = 100f });

                        // Gender Column
                        PdfPTable genderTable = new PdfPTable(1);
                        genderTable.WidthPercentage = 100;
                        genderTable.AddCell(new PdfPCell(new Phrase(GetGenderString(firstAlternate.gender),
                            FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        genderTable.AddCell(new PdfPCell(new Phrase(GetGenderString(secondAlternate.gender),
                            FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        table.AddCell(new PdfPCell(genderTable) { FixedHeight = 100f });

                        // Age Column
                        PdfPTable ageTable = new PdfPTable(1);
                        ageTable.WidthPercentage = 100;
                        ageTable.AddCell(new PdfPCell(new Phrase(firstAlternate.age.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        ageTable.AddCell(new PdfPCell(new Phrase(secondAlternate.age.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        table.AddCell(new PdfPCell(ageTable) { FixedHeight = 100f });
                    }
                    else
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            AddCellToPdfTableBody(table, string.Empty);
                        }
                    }

                    string cycleText = payroll.paymentCycle;

                    if (Session["isCombined"] != null && (bool)Session["isCombined"])
                    {
                        if (!string.IsNullOrEmpty(cycleText) && cycleText.StartsWith("Merge:"))
                        {
                            cycleText = cycleText.Substring(cycleText.IndexOf(":") + 1).Trim();

                            var cycleParts = cycleText
                                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                                .Select(c => c.Trim())
                                .OrderBy(c =>
                                {
                                    var tokens = c.Split(' ');
                                    return int.TryParse(tokens.Last(), out int n) ? n : int.MaxValue;
                                })
                                .ToList();

                            if (cycleParts.Count > 1)
                            {
                                cycleText = string.Join(",\n", cycleParts);
                            }
                            else
                            {
                                cycleText = cycleParts.FirstOrDefault() ?? "";
                            }
                        }
                    }

                    AddCellToPdfTableBody(table, cycleText, false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.supportType);
                    AddCellToPdfTableBody(table, payroll.totAttendance.ToString() ?? "N/A", false, hasTwoAlternates);                   
                    AddCellToPdfTableBody(table, payroll.wageRateUSD.ToString(), false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.wageRateUSDTotal.ToString(), false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.exchangerateattime.ToString());
                    AddCellToPdfTableBody(table, payroll.wageRateSSPTotal.ToString(), false, hasTwoAlternates);
                    // Signature with increased height
                    PdfPCell signatureCell = new PdfPCell(new Phrase(string.Empty))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        FixedHeight = 100f // Increased height for thumbprint accommodation
                    };
                    table.AddCell(signatureCell);
                }

                // Add the table to the document
                document.Add(table);

                // Close the document
                document.Close();

                string SanitizeFileNamePart(string input)
                {
                    var invalidChars = System.IO.Path.GetInvalidFileNameChars();
                    return new string(input.Where(c => !invalidChars.Contains(c)).ToArray()).Replace(" ", "_");
                }

                // Get and sanitize boma
                string boma = Session["SelectedBomaName"]?.ToString();
                boma = !string.IsNullOrWhiteSpace(boma) ? SanitizeFileNamePart(boma) : null;

                // Get and sanitize paymentCycle
                string paymentCycle = Session["SelectedPaymentCycle"]?.ToString();
                paymentCycle = !string.IsNullOrWhiteSpace(paymentCycle) ? SanitizeFileNamePart(paymentCycle) : null;

                // Get and sanitize Support Type
                string supportType = Session["SelectedSupportType"]?.ToString();
                supportType = !string.IsNullOrWhiteSpace(supportType) ? SanitizeFileNamePart(supportType) : null;

                // Build filename parts
                List<string> parts = new List<string>();
                if (!string.IsNullOrEmpty(boma)) parts.Add(boma);
                if (!string.IsNullOrEmpty(paymentCycle)) parts.Add(paymentCycle);
                if (!string.IsNullOrEmpty(supportType)) parts.Add(supportType);

                // Combine parts + extension
                string fileName = (parts.Count > 0 ? string.Join("_", parts) : "PayrollData") + ".pdf";

                // Set header
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(ms.ToArray());
                Response.End();
            }
        }

        // Helper methods remain unchanged
        private void AddPhotoToPdfTable(PdfPTable table, string base64Photo, bool hasTwoAlternates)
        {
            string emptyPhoto = "C:/mis_published_folder/SNSOP/Resource/Images/not-available-2.jpg";

            if (!string.IsNullOrEmpty(base64Photo))
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Photo);
                    Image img = Image.GetInstance(imageBytes);
                    img.ScaleToFit(40f, 50f); // Set image size to fit within the fixed cell height

                    PdfPCell imageCell = new PdfPCell(img, true)
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Padding = 2f,
                        FixedHeight = hasTwoAlternates ? 50f : 50f
                    };
                    table.AddCell(imageCell);
                }
                catch (Exception)
                {
                    // If base64 string is invalid, use the empty photo image
                    AddEmptyPhotoToPdfTable(table, emptyPhoto);
                }
            }
            else
            {
                // If no base64 photo is provided, use the empty photo image
                AddEmptyPhotoToPdfTable(table, emptyPhoto);
            }
        }

        private void AddEmptyPhotoToPdfTable(PdfPTable table, string emptyPhotoPath)
        {
            try
            {
                Image emptyImg = Image.GetInstance(emptyPhotoPath);
                emptyImg.ScaleToFit(40f, 50f); // Set image size to fit within the fixed cell height

                PdfPCell emptyCell = new PdfPCell(emptyImg, true)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Padding = 2f,
                    FixedHeight = 50f
                };
                table.AddCell(emptyCell);
            }
            catch (Exception)
            {
                // Fallback in case the empty photo file is missing or invalid
                Font fallbackFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                PdfPCell fallbackCell = new PdfPCell(new Phrase("No Image", fallbackFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 50f
                };
                table.AddCell(fallbackCell);
            }
        }


        private void AddSectionHeaderToPdfTable(PdfPTable table, string headerText, int colspan)
        {
            PdfPCell headerCell = new PdfPCell(new Phrase(headerText, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)))
            {
                Colspan = colspan,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 30f
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
                FixedHeight = 30f
            };
            table.AddCell(cell);
        }

        private void AddCellToPdfTableBody(PdfPTable table, string text, bool isNameCell = false, bool hasTwoAlternates = false)
        {
            Phrase content;
            if (isNameCell && !string.IsNullOrEmpty(text))
            {
                string[] nameParts = text.Split(' ');
                content = new Phrase(string.Join("\n", nameParts), FontFactory.GetFont(FontFactory.HELVETICA, 10));
            }
            else
            {
                content = new Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, 10));
            }

            PdfPCell cell = new PdfPCell(content)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = hasTwoAlternates ? 100f : 50f
            };
            table.AddCell(cell);
        }

        public class PageNumberHelper : PdfPageEventHelper
        {
            private Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                // Create a footer paragraph with the page number
                Paragraph footer = new Paragraph("Page " + writer.PageNumber, footerFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };

                // Set the footer position
                float x = document.PageSize.Width / 2;
                float y = document.BottomMargin / 2;

                // Write the footer at the bottom center
                ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase(footer), x, y, 0);
            }
        }


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
                    worksheet.Cells[1, 4].Value = "VSLA Formed";
                    /*worksheet.Cells[1, 4].Value = "Created By";
                    worksheet.Cells[1, 5].Value = "Created Date";
                    worksheet.Cells[1, 6].Value = "Modified By";
                    worksheet.Cells[1, 7].Value = "Modified Date";*/

                    // Set headers to be bold
                    for (int col = 1; col <= 4; col++)
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
                    
                    // Add data validation for VSLA formed (Yes/No)
                    var vslaFormedValidation = worksheet.DataValidations.AddListValidation(worksheet.Cells[2, 4, 1048576, 4].Address);
                    vslaFormedValidation.ShowErrorMessage = true;
                    vslaFormedValidation.ErrorTitle = "Invalid Selection";
                    vslaFormedValidation.Error = "Please select 'Yes' or 'No' from the drop-down list.";
                    vslaFormedValidation.Formula.Values.Add("Yes");
                    vslaFormedValidation.Formula.Values.Add("No");

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
        private void TrainingAttendanceTrackingTemplate(object trainingDate)
        {
            try
            {
                int? stateId = _location?.StateId;
                int? countyId = _location?.CountyId;
                int? payamId = _location?.PayamId;
                int? bomaId = _location?.BomaId;

                string TrainingDate = HttpUtility.UrlDecode(trainingDate.ToString());

                Component2Service service = new Component2Service();
                HashSet<string> householdNumbers = service.GetValidHouseholdNumberValues(stateId, countyId, payamId, bomaId);

                // Fetch course codes dynamically from the database
                Dictionary<string, int> existingCourseCode = service.GetExistingCourseCode();
                List<string> courseCodeList = existingCourseCode.Keys.ToList(); // Extracting only the course codes

                using (ExcelPackage package = new ExcelPackage())
                {
                    // Create a new worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Training Attendance Tracking Template");

                    // Add headers to the worksheet (reflecting the updated table schema)
                    worksheet.Cells[1, 1].Value = "Household Number";
                    worksheet.Cells[1, 2].Value = "Course Code";
                    worksheet.Cells[1, 3].Value = "Class Attendance Date"; // New column for training date


                    // Set headers to be bold
                    for (int col = 1; col <= 3; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    var householdNumberList = householdNumbers.ToList(); // Convert HashSet to List

                    if (householdNumberList.Count > 0) // Check if there are any items in the list
                    {
                        for (int row = 0; row < householdNumberList.Count; row++)
                        {
                            worksheet.Cells[row + 2, 1].Value = householdNumberList[row]; // Household Number (Locked)
                            worksheet.Cells[row + 2, 3].Value = TrainingDate; // Populate Training Date column
                        }

                        // Lock the first column (Household Number) and make it read-only
                        worksheet.Cells["A2:A" + (householdNumberList.Count + 1)].Style.Locked = true;
                        worksheet.Cells["C2:C" + (householdNumberList.Count + 1)].Style.Locked = true;
                    }

                    // Add dropdown for Course Code in column B (2nd column) using the fetched course codes
                    var validation = worksheet.DataValidations.AddListValidation("B2:B1000"); // Adjust range as needed
                    validation.Formula.Values.Add("");
                    foreach (var courseCode in courseCodeList)
                    {
                        validation.Formula.Values.Add(courseCode);
                    }

                    validation.ShowErrorMessage = false;
                    validation.ErrorTitle = "Invalid Selection";
                    validation.Error = "Please select a valid course code from the list.";

                    worksheet.Protection.IsProtected = true;
                    worksheet.Cells["B2:B" + (householdNumberList.Count + 1)].Style.Locked = false; // Unlock Course Code & Attendance Date
                    worksheet.Cells.AutoFitColumns();

                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=TrainingAttendanceTracking.xlsx");

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
                List<Core.Model.HouseholdGroupLinkageModel> householdGroupData = service.GetMappedHouseholdNumberValues(stateId, countyId, payamId, bomaId);

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
                    //worksheet.Cells[1, 6].Value = "Group ID";
                    worksheet.Cells[1, 6].Value = "Group Name";
                    worksheet.Cells[1, 7].Value = "Enrolled Member ID";
                    worksheet.Cells[1, 8].Value = "Reason For Why Not Enrolled";

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
                        //worksheet.Cells[row + 2, 6].Value = data.GroupId.HasValue ? (object)data.GroupId.Value : null; // Group ID
                        worksheet.Cells[row + 2, 6].Value = !string.IsNullOrEmpty(data.GroupName) ? data.GroupName : ""; // Group Name
                    }

                    // Lock the Household Number, Group ID, and Group Name columns (Columns 1, 6, and 7)
                    worksheet.Cells["A2:A" + (householdGroupData.Count + 1)].Style.Locked = true;
                    worksheet.Cells["F2:F" + (householdGroupData.Count + 1)].Style.Locked = true;
                    //worksheet.Cells["G2:G" + (householdGroupData.Count + 1)].Style.Locked = true;

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

                    // Add validation for the Enrolled Member ID field (Column 7) to only allow integers between 1 and 5, or leave blank
                    var validation5 = worksheet.DataValidations.AddCustomValidation("G2:G1000");
                    validation5.Formula.ExcelFormula = "OR(ISBLANK(G2), AND(ISNUMBER(G2), G2 >= 1, G2 <= 5))";
                    validation5.ShowErrorMessage = true;
                    validation5.ErrorTitle = "Invalid Input";
                    validation5.Error = "Please enter a whole number between 1 and 5, or leave the cell blank.";
                    validation5.PromptTitle = "Input Restriction";
                    validation5.Prompt = "Enter a whole number between 1 and 5, or leave blank.";

                    // Add conditional validation for "Reason for why not" (Column 8)
                    for (int row = 2; row <= 100; row++)
                    {
                        var conditionalValidation = worksheet.DataValidations.AddCustomValidation($"H{row}");
                        conditionalValidation.Formula.ExcelFormula = $"IF(E{row}=\"No\", TRUE, ISBLANK(H{row}))";
                        conditionalValidation.ShowErrorMessage = true;
                        conditionalValidation.ErrorTitle = "Invalid Input";
                        conditionalValidation.Error = "This field can only be edited if HH Enrollment Status is 'No'. It must be blank otherwise.";
                        conditionalValidation.PromptTitle = "Input Restriction";
                        conditionalValidation.Prompt = "Enter a reason only if HH Enrollment Status is 'No'.";
                    }

                    // Protect the worksheet so that locked cells (Household Number, Group ID, Group Name) are read-only
                    worksheet.Protection.IsProtected = true;

                    // Unlock other cells so they can be edited by the user
                    worksheet.Cells["B2:E1000"].Style.Locked = false;
                    worksheet.Cells["G2:G1000"].Style.Locked = false; // Allow users to edit "Enrolled Member ID"
                    worksheet.Cells["H2:H1000"].Style.Locked = false;

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
                    worksheet.Cells[1, 5].Value = "Actual Payment Date" + "\n" + "(mm-dd-yyyy)";
                    worksheet.Cells[1, 5].Style.WrapText = true;

                    // Set headers to be bold
                    for (int col = 1; col <= 5; col++)
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
                    paymentCycleValidation.Error = "Please select valid cycle.";
                    paymentCycleValidation.Formula.Values.Add("Installment 1");
                    paymentCycleValidation.Formula.Values.Add("Installment 2");
                    paymentCycleValidation.Formula.Values.Add("Installment 3");
                    paymentCycleValidation.Formula.Values.Add("Installment 4");
                    paymentCycleValidation.Formula.Values.Add("Installment 5");
                    paymentCycleValidation.Formula.Values.Add("Installment 6");
                    paymentCycleValidation.Formula.Values.Add("Installment 7");
                    paymentCycleValidation.Formula.Values.Add("Installment 8");
                    paymentCycleValidation.Formula.Values.Add("Installment 9");

                    // Set the format for "Payment Date" as DateTime for the actual data rows
                    worksheet.Cells[2, 4, dataRows + 1, 4].Style.Numberformat.Format = "MM-dd-yyyy"; // or any DateTime format

                    // Apply data validation for the "Payment Date" column (only to the actual data rows)
                    var dateValidation = worksheet.DataValidations.AddDateTimeValidation(worksheet.Cells[2, 4, dataRows + 1, 4].Address);
                    dateValidation.ShowErrorMessage = true;
                    dateValidation.ErrorTitle = "Invalid Date";
                    dateValidation.Error = "Please enter a valid date in the format MM-dd-yyyy. Also ensure you machine's date format is in dd-mm-yyyy";
                    dateValidation.Prompt = "Select a date or enter manually in the format MM-dd-yyyy. Also ensure you machine's date format is in dd-MM-yyyy";
                    dateValidation.AllowBlank = true;
                    dateValidation.Formula.Value = new DateTime(2000, 1, 1); // Minimum allowed date
                    dateValidation.Formula2.Value = new DateTime(2100, 12, 31); // Maximum allowed date

                    // Set the format for "Actual Payment Date" as DateTime for the actual data rows
                    worksheet.Cells[2, 5, dataRows + 1, 5].Style.Numberformat.Format = "MM-dd-yyyy"; // or any DateTime format

                    // Apply data validation for the "Actual Payment Date" column (only to the actual data rows)
                    var actualDateValidation = worksheet.DataValidations.AddDateTimeValidation(worksheet.Cells[2, 5, dataRows + 1, 5].Address);
                    actualDateValidation.ShowErrorMessage = true;
                    actualDateValidation.ErrorTitle = "Invalid Date";
                    actualDateValidation.Error = "Please enter a valid date in the format MM-dd-yyyy. Also ensure you machine's date format is in dd-mm-yyyy";
                    actualDateValidation.Prompt = "Select a date or enter manually in the format MM-dd-yyyy. Also ensure you machine's date format is in dd-MM-yyyy";
                    actualDateValidation.AllowBlank = true;
                    actualDateValidation.Formula.Value = new DateTime(2000, 1, 1); // Minimum allowed date
                    actualDateValidation.Formula2.Value = new DateTime(2100, 12, 31); // Maximum allowed date

                    worksheet.Protection.IsProtected = true;
                    worksheet.Cells["B2:G1000"].Style.Locked = false;

                    // Auto-fit columns for better appearance
                    worksheet.Cells.AutoFitColumns();
                    worksheet.Column(4).Width = 15;
                    worksheet.Column(5).Width = 15;


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
            string reportName = "";
            switch(selectedAuditTable)
            {
                case "tbl_com2_Household_Enrollment_Data_Audit":
                    reportName = "Beneficiary Randomization and Enrollment";
                    break;
                case "tbl_com2_Training_Attendance_Audit":
                    reportName = "Training and Attendance Tracking";
                    break;
                case "tbl_com2_Business_Plan_Development_Audit":
                    reportName = "Business Plan Development";
                    break;
                case "tbl_com2_Livelihood_Grant_Disbursement_Audit":
                    reportName = "Livelihood Grant Disbursement";
                    break;
                case "tbl_com2_Financial_Services_Linkage_Tracking_Audit":
                    reportName = "Financial Services Linkage Tracking";
                    break;
            }

            Paragraph title = new Paragraph("Audit Record Report - " + reportName);
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

        private void PayrollReconcileTemplate()
        {
            try
            {
                PayrollReconcileRequestDto dto = Session["PayrollReconcileRequest"] as PayrollReconcileRequestDto;

                int? stateId = dto?.StateId;
                int? countyId = dto?.CountyId;
                int? payamId = dto?.PayamId;
                int? bomaId = dto?.BomaId;
                string ReqNo = string.IsNullOrEmpty(dto?.ReqNo?.Trim()) ? dto?.ReqNo?.Trim() : null;
                string WorkCode = string.IsNullOrEmpty(dto?.WorkCode?.Trim()) ? dto?.WorkCode?.Trim() : null;

                DataSet ds = genDbAccess.GetReconciledUnpaidPayrollKIT(stateId, countyId, payamId, bomaId, ReqNo, WorkCode);
                if (ds == null || ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count <= 0)
                {
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=PayrollReconcile.xlsx");

                    using (ExcelPackage package = new ExcelPackage())
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Payroll");

                        Response.ClearHeaders();
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("Content-Disposition", "attachment; filename=PayrollReconcile.xlsx");

                        Response.BinaryWrite(package.GetAsByteArray());
                        Response.Flush();
                        Response.Close();
                        return;
                    }
                }

                using (ExcelPackage package = new ExcelPackage())
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Payroll");

                    worksheet.Cells[1, 1].Value = "Household Number";
                    worksheet.Cells[1, 2].Value = "Enrollment Number";
                    worksheet.Cells[1, 3].Value = "First Name";
                    worksheet.Cells[1, 4].Value = "Sur Name";
                    worksheet.Cells[1, 5].Value = "Last Name";
                    worksheet.Cells[1, 6].Value = "Payment Status";
                    worksheet.Cells[1, 7].Value = "Payment Center";
                    worksheet.Cells[1, 8].Value = "Paid To";
                    worksheet.Cells[1, 9].Value = "Wage";
                    worksheet.Cells[1, 10].Value = "Attendance";
                    worksheet.Cells[1, 11].Value = "Exchange Rate";
                    worksheet.Cells[1, 12].Value = "Amount in SSP";
                    worksheet.Cells[1, 13].Value = "Work Code";
                    worksheet.Cells[1, 14].Value = "Work Id";
                    worksheet.Cells[1, 15].Value = "Transaction Id";
                    worksheet.Cells[1, 16].Value = "UUID";
                    worksheet.Cells[1, 17].Value = "Payroll Start Date";
                    worksheet.Cells[1, 18].Value = "Payroll End Date";
                    worksheet.Cells[1, 19].Value = "Created At";
                    worksheet.Cells[1, 20].Value = "Generated At";
                    worksheet.Cells[1, 21].Value = "Latitude";
                    worksheet.Cells[1, 22].Value = "Longitude";
                    worksheet.Cells[1, 1].Style.WrapText = true;

                    // Set headers to be bold
                    for (int col = 1; col <= 4; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    for (int row = 0; row < ds.Tables[0].Rows.Count; row++)
                    {
                        worksheet.Cells[row + 2, 1].Value = ds.Tables[0].Rows[row]["household_number"].ToString();
                        worksheet.Cells["A2:A" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 2].Value = ds.Tables[0].Rows[row]["EnrollmentNo"].ToString();
                        worksheet.Cells["B2:B" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        if (ds.Tables[0].Rows[row]["NameofHousehold"] != null)
                        {
                            var names = ds.Tables[0].Rows[row]["NameofHousehold"].ToString().Split(' ');
                            if (names != null && names.Length > 0)
                            {
                                if (names.Length >= 3)
                                {
                                    worksheet.Cells[row + 2, 3].Value = names[0];
                                    worksheet.Cells["C2:C" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                                    worksheet.Cells[row + 2, 4].Value = names[1];
                                    worksheet.Cells["D2:D" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                                    worksheet.Cells[row + 2, 5].Value = names[2];
                                    worksheet.Cells["E2:E" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;
                                }
                            }
                        }

                        worksheet.Cells[row + 2, 6].Value = ds.Tables[0].Rows[row]["status"].ToString();
                        worksheet.Cells[row + 2, 7].Value = ""; // payment center
                        worksheet.Cells[row + 2, 8].Value = ""; // paid to

                        worksheet.Cells[row + 2, 9].Value = ds.Tables[0].Rows[row]["AmountInSSP"].ToString();
                        worksheet.Cells["I2:I" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 10].Value = ds.Tables[0].Rows[row]["TotAttendance"].ToString();
                        worksheet.Cells["J2:J" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 11].Value = ds.Tables[0].Rows[row]["Exchangerateattime"].ToString();
                        worksheet.Cells["K2:K" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 12].Value = ds.Tables[0].Rows[row]["AmountInSSP"].ToString();
                        worksheet.Cells["L2:L" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 13].Value = ds.Tables[0].Rows[row]["WorkCode"].ToString();
                        worksheet.Cells["M2:M" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 14].Value = ds.Tables[0].Rows[row]["intWorkid"].ToString();
                        worksheet.Cells["N2:N" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 15].Value = ds.Tables[0].Rows[row]["trans_id"].ToString();
                        worksheet.Cells["O2:O" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 16].Value = ds.Tables[0].Rows[row]["uuid"].ToString();
                        worksheet.Cells["P2:P" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 17].Value = ds.Tables[0].Rows[row]["ReqFromDate"].ToString();
                        worksheet.Cells["Q2:Q" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 18].Value = ds.Tables[0].Rows[row]["ReqToDate"].ToString();
                        worksheet.Cells["R2:R" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 19].Value = ds.Tables[0].Rows[row]["Created_dt"].ToString();
                        worksheet.Cells["S2:S" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 20].Value = ds.Tables[0].Rows[row]["Modified_dt"].ToString();
                        worksheet.Cells["T2:T" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 21].Value = ds.Tables[0].Rows[row]["latitude"].ToString();
                        worksheet.Cells["U2:U" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;

                        worksheet.Cells[row + 2, 22].Value = ds.Tables[0].Rows[row]["longitude"].ToString();
                        worksheet.Cells["V2:V" + (ds.Tables[0].Rows.Count + 1)].Style.Locked = true;
                    }

                    worksheet.Protection.IsProtected = true;
                    worksheet.Cells.AutoFitColumns();

                    var statusValidation = worksheet.DataValidations.AddListValidation(worksheet.Cells[2, 6, 100000, 6].Address);
                    statusValidation.ShowErrorMessage = true;
                    statusValidation.ErrorTitle = "Invalid Status";
                    statusValidation.Error = "Please select status.";
                    statusValidation.Formula.Values.Add("UNPAID");
                    statusValidation.Formula.Values.Add("PAID");

                    worksheet.Cells["F2:F10000"].Style.Locked = false;
                    worksheet.Cells["G2:G10000"].Style.Locked = false;
                    worksheet.Cells["H2:H10000"].Style.Locked = false;

                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=PayrollReconcile.xlsx");

                    Response.BinaryWrite(package.GetAsByteArray());
                    Response.Flush();
                    Response.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex}");
            }
        }


        private void DownloadFile(int reportId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT FilePath FROM tbl_mne_Report WHERE Id = @ReportId AND Status = 'Active'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReportId", reportId);
                    conn.Open();
                    string filePath = cmd.ExecuteScalar() as string;

                    if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                    {
                        Response.Clear();
                        Response.ContentType = "application/octet-stream";
                        Response.AppendHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(filePath)}");
                        Response.TransmitFile(filePath);
                        Response.End();
                    }
                    else
                    {
                        Response.Write("File not found or inactive.");
                    }
                }
            }
        }

        private void DownloadIdCard(int benId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT id_card_path FROM tbl_BeneficiaryDet WHERE intBenid = @ID AND id_card_generation_status = 1";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID", benId);
                    conn.Open();
                    string filePath = cmd.ExecuteScalar() as string;
                    string genericPath = ConfigurationManager.AppSettings["PaymentCycleDocumentPath"].ToString() +
                                    "\\" + "id_cards" + "\\";
                    string fullPath = genericPath + filePath;

                    if (!string.IsNullOrEmpty(fullPath) && File.Exists(fullPath))
                    {
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AppendHeader("Content-Disposition", $"attachment; filename={Path.GetFileName(fullPath)}");
                        Response.TransmitFile(fullPath);
                        Response.End();
                    }
                    else
                    {
                        Response.Write("ID card file not found or not yet generated.");
                    }
                }
            }
        }

        private void CourseEntityTemplate()
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Create a new worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Course Entity Template");

                    // Add headers to the worksheet (reflecting your table schema)
                    worksheet.Cells[1, 1].Value = "Course Code";
                    worksheet.Cells[1, 2].Value = "Course Name";
                    worksheet.Cells[1, 3].Value = "Course Duration";

                    // Set headers to be bold
                    for (int col = 1; col <= 3; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    worksheet.Cells.AutoFitColumns();
                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=CourseEntityTemplate.xlsx");

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

        private void CourseDetailsTemplate()
        {
            try
            {
                using (ExcelPackage package = new ExcelPackage())
                {
                    // Create a new worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Course Details Template");

                    Component2Service service = new Component2Service();
                    Dictionary<string, int> existingCourseCode = service.GetExistingCourseCode();
                    List<CourseDetailsModel> courseCodeList = existingCourseCode
                        .Select(kvp => new CourseDetailsModel
                        {
                            CourseCode = kvp.Key
                        })
                        .ToList();
                    var validation = worksheet.DataValidations.AddListValidation("A2:A1000"); // Adjust the range as needed

                    foreach (var course in courseCodeList)
                    {
                        validation.Formula.Values.Add(course.CourseCode);
                    }

                    // Add headers to the worksheet (reflecting your table schema)
                    worksheet.Cells[1, 1].Value = "Course Code";
                    worksheet.Cells[1, 2].Value = "Class Holding Date (mm/dd/yyyy)";
                    worksheet.Cells[1, 3].Value = "Duration of held class (Minutes)";

                    // Set headers to be bold
                    for (int col = 1; col <= 3; col++)
                    {
                        worksheet.Cells[1, col].Style.Font.Bold = true;
                    }

                    worksheet.Cells.AutoFitColumns();
                    // Set the content type and headers for the download
                    Response.ClearHeaders();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("Content-Disposition", "attachment; filename=CourseDetailsTemplate.xlsx");

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
        private void LogError(string v, Exception ex)
        {
            throw new NotImplementedException();
        }


    }
}