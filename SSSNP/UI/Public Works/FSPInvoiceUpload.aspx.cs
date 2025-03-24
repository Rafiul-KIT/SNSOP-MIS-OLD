using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP.UI.Public_Works
{
    public partial class FSPInvoiceUpload : System.Web.UI.Page
    {
        private LookupManagerService _lookupManagerService;
        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeServices();
            // Clear message and preview link on initial load (not postback)
            if (!IsPostBack)
            {
                LoadStates();
                LoadPaymentCycles();
                lblMessage.Text = "";
                hlPreview.Visible = false;
            }
        }

        private void InitializeServices()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Connection string 'SNSDPSkils' is not found or empty.");
                }

                _lookupManagerService = new LookupManagerService(connectionString);
            }
            catch (Exception ex)
            {
                LogError(new Exception("Error initializing services: " + ex.Message, ex));
            }
        }



        private void LoadStates()
        {
            try
            {
                DataTable dtStates = _lookupManagerService.GetStateList();
                ddlState.DataSource = dtStates;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "intStateid";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCounty.Items.Clear();

            ddlCounty.Items.Add(new ListItem("--Select--", ""));

            if (!string.IsNullOrEmpty(ddlState.SelectedValue))
            {
                try
                {
                    int stateId;
                    bool isParsed = int.TryParse(ddlState.SelectedValue, out stateId);
                    if (isParsed)
                    {
                        DataTable dtCounties = _lookupManagerService.GetCountyListByStateId(stateId);
                        ddlCounty.DataSource = dtCounties;
                        ddlCounty.DataTextField = "CountieName";
                        ddlCounty.DataValueField = "intCountieid";
                        ddlCounty.DataBind();
                        ddlCounty.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                    else
                    {
                        LogError(new Exception("ddlState.SelectedValue is not a valid integer: " + ddlState.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
        }

        private void LoadPaymentCycles()
        {
            ddlPaymentCycle.Items.Add(new ListItem("--Select--", ""));
            ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 1", "1"));
            ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 2", "2"));
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (ddlState.SelectedIndex == 0)
            {
                lblMessage.Text = "Please select a State.";
                lblMessage.CssClass = "text-warning";
                return;
            }

            if (ddlCounty.SelectedIndex == 0)
            {
                lblMessage.Text = "Please select a County.";
                lblMessage.CssClass = "text-warning";
                return;
            }

            if (ddlPaymentCycle.SelectedIndex == 0)
            {
                lblMessage.Text = "Please select a Payment Cycle.";
                lblMessage.CssClass = "text-warning";
                return;
            }

            if (fileUpload.HasFile)
            {
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".jpg", ".jpeg", ".png", ".xml", ".csv" };

                if (Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    SqlTransaction transaction = null;
                    try
                    {
                        int userId = GetCurrentUserId();
                        string baseDirectory = ConfigurationManager.AppSettings["InvoiceUploadDirectory"];
                        string fileName = Path.GetFileName(fileUpload.FileName);
                        string filePath = Path.Combine(baseDirectory, fileName);

                        // Ensure the directory exists
                        Directory.CreateDirectory(baseDirectory);

                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
                        {
                            conn.Open();
                            transaction = conn.BeginTransaction();

                            // Save file to disk
                            SaveFile(fileUpload, filePath);

                            // Save record to the database
                            SaveInvoiceRecord(conn, transaction, filePath, userId);

                            // Commit the transaction if both operations succeed
                            transaction.Commit();
                        }

                        // Display success message and preview link
                        lblMessage.Text = "Invoice uploaded successfully!";
                        lblMessage.CssClass = "text-success";

                        string virtualPath = $"/snsop_docs/invoice/{HttpUtility.UrlPathEncode(fileName)}";
                        hlPreview.NavigateUrl = virtualPath;
                        hlPreview.Text = "View Uploaded Invoice";
                        hlPreview.Visible = true;
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        // Rollback the transaction if an error occurs
                        transaction?.Rollback();
                        lblMessage.Text = "Invoice upload failed: " + ex.Message;
                        lblMessage.CssClass = "text-danger";
                        LogError(ex);
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid file type. Please upload PDF, DOC, DOCX, XLS, XLSX, TXT, JPG, JPEG, PNG, XML, or CSV files only.";
                    lblMessage.CssClass = "text-warning";
                }
            }
            else
            {
                lblMessage.Text = "Please select an invoice file to upload.";
                lblMessage.CssClass = "text-warning";
            }
        }

        private void SaveFile(FileUpload fileUpload, string filePath)
        {
            try
            {
                fileUpload.SaveAs(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving the file: " + ex.Message);
            }
        }

        private void SaveInvoiceRecord(SqlConnection conn, SqlTransaction transaction, string filePath, int userId)
        {
            try
            {
                string query = @"
        INSERT INTO invoice_history (state_id, county_id, payment_cycle_id, document_path, created_at, created_by, status)
        VALUES (@StateId, @CountyId, @PaymentCycleId, @DocumentPath, GETDATE(), @UserId, @Status)";

                using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@StateId", ddlState.SelectedValue);
                    cmd.Parameters.AddWithValue("@CountyId", ddlCounty.SelectedValue);
                    cmd.Parameters.AddWithValue("@PaymentCycleId", ddlPaymentCycle.SelectedValue);
                    cmd.Parameters.AddWithValue("@DocumentPath", filePath);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Status", 0);  // Uploaded

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error while saving to database: " + ex.Message);
            }
        }


        private int GetCurrentUserId()
        {
            // Check if the session contains the user ID
            if (Session["uid"] != null)
            {
                return Convert.ToInt32(Session["uid"]);
            }
            else
            {
                throw new Exception("User ID not found in session.");
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear file upload control and message label
            fileUpload.Attributes.Clear();
            lblMessage.Text = "";
            hlPreview.Visible = false;
            ddlState.SelectedIndex = 0;
            ddlCounty.SelectedIndex = 0;
            ddlPaymentCycle.SelectedIndex = 0;
        }

        private void ClearFields()
        {
            ddlState.SelectedIndex = 0;
            ddlCounty.SelectedIndex = 0;
            ddlPaymentCycle.SelectedIndex = 0;
            fileUpload.Attributes.Clear();
        }


        private void LogError(Exception exception)
        {
            string logFilePath = ConfigurationManager.AppSettings["ErrorLogPath"];
            string logMessage = $"[{DateTime.Now}] - {exception.Message}{Environment.NewLine}{exception.StackTrace}{Environment.NewLine}";
            try
            {
                File.AppendAllText(logFilePath, logMessage);
            }
            catch
            {
                // If logging fails, do nothing to avoid recursion.
            }
        }

    }
}
