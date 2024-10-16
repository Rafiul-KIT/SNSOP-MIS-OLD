using System;
using System.Data;
using System.Web.UI.WebControls;
using SSSNP.Service;
using SSSNP_SNSOP.Core.Repository;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web;

namespace SSSNP.UI.Public_Works
{
    public partial class AuditRecordDownload : System.Web.UI.Page
    {
        private IAuditRecordService _auditRecordService;

        protected void Page_Load(object sender, EventArgs e)
        {
            IAuditRecordRepository auditRepository = new AuditRecordRepository();
            _auditRecordService = new AuditRecordService(auditRepository);
           
            if (!IsPostBack)
            {
                // Initialize the repository and service
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedAuditTable = ddlAuditRecordType.SelectedValue;

            // Retrieve dates from ViewState if available, ensure proper casting
            DateTime? fromDate = ViewState["FromDate"] != null ? (DateTime?)ViewState["FromDate"] : null;
            DateTime? toDate = ViewState["ToDate"] != null ? (DateTime?)ViewState["ToDate"] : null;

            // If ViewState is null, check the textboxes
            if (!fromDate.HasValue && DateTime.TryParse(txtFromDate.Text, out DateTime parsedFromDate))
            {
                fromDate = parsedFromDate;
            }

            if (!toDate.HasValue && DateTime.TryParse(txtToDate.Text, out DateTime parsedToDate))
            {
                toDate = parsedToDate;
            }

            try
            {
                DataTable auditRecords = null;
                // Fetch records based on the selected table
                switch (selectedAuditTable)
                {
                    case "tbl_com2_Household_Enrollment_Data_Audit":
                        auditRecords = _auditRecordService.GetHouseholdEnrollmentAudit(fromDate, toDate);
                        break;
                    case "tbl_com2_Training_Attendance_Audit":
                        auditRecords = _auditRecordService.GetTrainingAttendanceAudit(fromDate, toDate);
                        break;
                    case "tbl_com2_Livelihood_Grant_Disbursement_Audit":
                        auditRecords = _auditRecordService.GetLivelihoodGrantDisbursementAudit(fromDate, toDate);
                        break;
                    case "tbl_com2_Financial_Services_Linkage_Tracking_Audit":
                        auditRecords = _auditRecordService.GetFinancialServicesLinkageAudit(fromDate, toDate);
                        break;
                    case "tbl_com2_Business_Plan_Development_Audit":
                        auditRecords = _auditRecordService.GetBusinessPlanDevelopmentAudit(fromDate, toDate);
                        break;
                    default:
                        lblMessage.Text = "Please select a valid audit record.";
                        lblMessage.CssClass = "text-danger";
                        lblMessage.Visible = true;
                        gvAuditRecords.Visible = false;
                        return;
                }

                // Check if data was found
                if (auditRecords == null || auditRecords.Rows.Count == 0)
                {
                    lblMessage.Text = "No records found for the selected audit and date range.";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    gvAuditRecords.Visible = false;
                }
                else
                {
                    // Store the records in ViewState for later use in RadGrid
                    ViewState["AuditRecords"] = auditRecords;

                    // Rebind the RadGrid (with AutoGenerateColumns="true")
                    gvAuditRecords.Rebind();
                    lblMessage.Visible = false;
                    gvAuditRecords.Visible = true;
                }
            }
            catch (Exception ex)
            {
                // Log the actual exception (optional)
                // LogError(ex); // Uncomment and use a logger if available

                // Display a user-friendly message
                lblMessage.Text = "An error occurred while fetching audit records. Please try again later.";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
                gvAuditRecords.Visible = false;
            }
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve audit records from ViewState
                if (ViewState["AuditRecords"] == null)
                {
                    lblMessage.Text = "No data available for download. Please search first.";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    return;
                }

                DataTable auditRecords = (DataTable)ViewState["AuditRecords"];

                // Check if there are records to download
                if (auditRecords == null || auditRecords.Rows.Count == 0)
                {
                    lblMessage.Text = "No records available for download.";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    return;
                }

                // Set the file name dynamically based on the selected audit record type
                string selectedAuditTable = ddlAuditRecordType.SelectedItem.Text.Replace(" ", "_");
                string fileName = $"{selectedAuditTable}_AuditRecords.pdf";

                // Create a memory stream to hold the PDF
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create the PDF document
                    Document document = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
                    PdfWriter writer = PdfWriter.GetInstance(document, ms);
                    document.Open();

                    // Add a title to the PDF document
                    Font titleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
                    Paragraph title = new Paragraph($"{selectedAuditTable} Audit Records", titleFont)
                    {
                        Alignment = Element.ALIGN_CENTER,
                        SpacingAfter = 20f
                    };
                    document.Add(title);

                    // Create a PDF table with the same number of columns as the DataTable
                    PdfPTable pdfTable = new PdfPTable(auditRecords.Columns.Count)
                    {
                        WidthPercentage = 100,
                        HorizontalAlignment = Element.ALIGN_LEFT
                    };

                    // Set table column headers dynamically
                    foreach (DataColumn column in auditRecords.Columns)
                    {
                        PdfPCell headerCell = new PdfPCell(new Phrase(column.ColumnName, FontFactory.GetFont("Arial", 12, Font.BOLD)))
                        {
                            BackgroundColor = BaseColor.LIGHT_GRAY,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        };
                        pdfTable.AddCell(headerCell);
                    }

                    // Add rows to the PDF table dynamically from the DataTable
                    foreach (DataRow row in auditRecords.Rows)
                    {
                        foreach (var cellData in row.ItemArray)
                        {
                            pdfTable.AddCell(new Phrase(cellData.ToString(), FontFactory.GetFont("Arial", 10)));
                        }
                    }

                    // Add the table to the document
                    document.Add(pdfTable);

                    // Close the document
                    document.Close();

                    // Prepare the PDF for download
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", $"attachment;filename={fileName}");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.BinaryWrite(ms.ToArray());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                // Log the actual exception if necessary
                lblMessage.Text = "An error occurred while downloading the PDF: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
        }

        protected void ddlAuditRecordType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Clear the FromDate and ToDate textboxes
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;

            // Clear any message labels and make it invisible
            lblMessage.Text = string.Empty;
            lblMessage.Visible = false;
            // Optionally, reset ViewState for the date values
            ViewState["FromDate"] = null;
            ViewState["ToDate"] = null;
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtFromDate.Text, out DateTime fromDate))
            {
                // Store the valid date in ViewState
                ViewState["FromDate"] = fromDate;
                lblMessage.Visible = false;
            }
            else
            {
                // Show an error if the date is not valid
                lblMessage.Text = "Invalid From Date. Please enter a valid date.";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
                ViewState["FromDate"] = null; // Clear ViewState if invalid
            }
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            if (DateTime.TryParse(txtToDate.Text, out DateTime toDate))
            {
                // Store the valid date in ViewState
                ViewState["ToDate"] = toDate;
                lblMessage.Visible = false;
            }
            else
            {
                // Show an error if the date is not valid
                lblMessage.Text = "Invalid To Date. Please enter a valid date.";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
                ViewState["ToDate"] = null; // Clear ViewState if invalid
            }
        }


        protected void gvAuditRecords_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            // Retrieve audit records from ViewState
            if (ViewState["AuditRecords"] != null)
            {
                DataTable auditRecords = (DataTable)ViewState["AuditRecords"];
                gvAuditRecords.DataSource = auditRecords;
            }
        }


    }
}
