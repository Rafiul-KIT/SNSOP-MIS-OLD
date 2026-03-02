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
            Session["SelectedAuditTable"] = null;
            Session["AuditRecordData"] = null;
            string selectedAuditTable = ddlAuditRecordType.SelectedValue;
            Session["SelectedAuditTable"] = selectedAuditTable;

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

            // Ensure both fromDate and toDate are provided
            if (!fromDate.HasValue || !toDate.HasValue)
            {
                lblMessage.Text = "Both From Date and To Date are required.";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
                gvAuditRecords.Visible = false;
                return;
            }

            // Add one day to the toDate to include the full day
            toDate = toDate.Value.AddDays(1);

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
                        lblMessage.Text = "Please select a valid module.";
                        lblMessage.CssClass = "text-danger";
                        lblMessage.Visible = true;
                        gvAuditRecords.Visible = false;
                        return;
                }

                // Check if data was found
                if (auditRecords == null || auditRecords.Rows.Count == 0)
                {
                    lblMessage.Text = "No records found for the selected module and date range.";
                    lblMessage.CssClass = "text-danger";
                    lblMessage.Visible = true;
                    gvAuditRecords.Visible = false;
                }
                else
                {
                    // Store the records in ViewState for later use in RadGrid
                    ViewState["AuditRecords"] = auditRecords;
                    Session["AuditRecordData"] = auditRecords;
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
            // Check if the session contains valid data
            DataTable auditRecords = Session["AuditRecordData"] as DataTable;
            string selectedAuditTable = Session["SelectedAuditTable"] as string;

            if (auditRecords == null || auditRecords.Rows.Count == 0 || string.IsNullOrEmpty(selectedAuditTable))
            {
                // If no data is available, show a message to the user and do not redirect
                lblMessage.Text = "No data available for download.";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
                return;
            }

            // If data is available, proceed to download
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=22";
            Response.Redirect(redirectUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();

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
