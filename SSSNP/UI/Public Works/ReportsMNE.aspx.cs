using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP.UI.Public_Works
{
    public partial class ReportsMNE : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessageSuccess.Visible = false;
                lblMessageError.Visible = false;
                DivFailure.Visible = false;
                DivSuccess.Visible = false;
            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ClearLabels();

            // Retrieve values from form fields
            string reportType = ddlSubmitReportType.SelectedValue;
            int uploadedBy = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
            DateTime uploadedDate = DateTime.Now;
            string status = "Active";

            // Check if the report type is selected
            if (reportType == "0")
            {
                lblMessageError.Text = "Please select a valid report type.";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Visible = true;
                DivFailure.Visible = true;
                return;
            }

            // Check if a file has been uploaded
            if (!fileUpload.HasFile)
            {
                lblMessageError.Text = "Please upload a file.";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Visible = true;
                DivFailure.Visible = true;
                return;
            }

            // Define the file name and file path
            string fileName = fileUpload.FileName;
            string folderPath = Server.MapPath("~/UI/Uploads/Saved Files");
            string filePath = Path.Combine(folderPath, fileName);
            // Attempt to save the file and perform database operations
            try
            {
                // Save the file
                fileUpload.SaveAs(filePath);

                // Database connection and insertion
                string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO tbl_mne_Report (FileName, FilePath, UploadedBy, UploadedDate, ReportType, Status) " +
                                   "VALUES (@FileName, @FilePath, @UploadedBy, @UploadedDate, @ReportType, @Status)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Define parameters and add values
                        cmd.Parameters.AddWithValue("@FileName", fileName);
                        cmd.Parameters.AddWithValue("@FilePath", filePath);
                        cmd.Parameters.AddWithValue("@UploadedBy", uploadedBy);
                        cmd.Parameters.AddWithValue("@UploadedDate", uploadedDate);
                        cmd.Parameters.AddWithValue("@ReportType", reportType);
                        cmd.Parameters.AddWithValue("@Status", status);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Display success message if everything is successful
                lblMessageSuccess.Text = "Report uploaded successfully!";
                lblMessageSuccess.ForeColor = System.Drawing.Color.Green;
                lblMessageSuccess.Visible = true;
                DivSuccess.Visible = true;
                ddlSubmitReportType.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                // Display an error message if file upload or database insertion fails
                lblMessageError.Text = "An error occurred while uploading the file. Please try again. " + ex.Message;
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Visible = true;
                DivFailure.Visible = true;
            }
        }

        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClearLabels();
            RadGrid.MasterTableView.GetColumn("UploadedBy").Visible = true;
            RadGrid.MasterTableView.GetColumn("UploadedDate").Visible = true;
            RadGrid.MasterTableView.GetColumn("DeletedBy").Visible = false;
            RadGrid.MasterTableView.GetColumn("DeletedDate").Visible = false;
            RadGrid.MasterTableView.GetColumn("Actions").Visible = true;
            RadGrid.MasterTableView.GetColumn("Manage").Visible = false;

            // Get values from the search fields
            string reportType = ddlSearchReportType.SelectedValue != "0" ? ddlSearchReportType.SelectedValue : null;
            DateTime? fromDate = string.IsNullOrWhiteSpace(txtFromDate.Text) ? (DateTime?)null : DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", null);
            DateTime? toDate = string.IsNullOrWhiteSpace(txtToDate.Text) ? (DateTime?)null : DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", null);

            // Call GetReportData with the selected criteria
            DataTable dt = GetSavedReportData(fromDate, toDate, reportType);

            // Bind the grid
            RadGrid.DataSource = dt;
            RadGrid.DataBind();
            RadGrid.Visible = true;
        }



        protected void btnShowDeletedRecords_Click(object sender, EventArgs e)
        {
            ClearLabels();
            RadGrid.MasterTableView.GetColumn("UploadedBy").Visible = false;
            RadGrid.MasterTableView.GetColumn("UploadedDate").Visible = false;
            RadGrid.MasterTableView.GetColumn("DeletedBy").Visible = true;
            RadGrid.MasterTableView.GetColumn("DeletedDate").Visible = true;
            RadGrid.MasterTableView.GetColumn("Manage").Visible = true;
            RadGrid.MasterTableView.GetColumn("Actions").Visible = false;


            // Get values from the search fields
            string reportType = ddlSearchReportType.SelectedValue != "0" ? ddlSearchReportType.SelectedValue : null;
            DateTime? fromDate = string.IsNullOrWhiteSpace(txtFromDate.Text) ? (DateTime?)null : DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", null);
            DateTime? toDate = string.IsNullOrWhiteSpace(txtToDate.Text) ? (DateTime?)null : DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", null);

            // Call GetReportData with the selected criteria
            DataTable dt = GetDeletedReportData(fromDate, toDate, reportType);

            // Bind the grid
            RadGrid.DataSource = dt;
            RadGrid.DataBind();
            RadGrid.Visible = true;
        }



        private DataTable GetSavedReportData(DateTime? fromDate = null, DateTime? toDate = null, string reportType = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    var query = "SELECT Id, FileName, ReportType, UploadedBy, UploadedDate FROM tbl_mne_Report WHERE Status = 'Active'";

                    // Append conditions based on criteria
                    if (fromDate.HasValue)
                    {
                        query += " AND UploadedDate >= @FromDate";
                        cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                    }

                    if (toDate.HasValue)
                    {
                        query += " AND UploadedDate <= @ToDate";
                        cmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                    }

                    if (!string.IsNullOrEmpty(reportType))
                    {
                        query += " AND ReportType = @ReportType";
                        cmd.Parameters.AddWithValue("@ReportType", reportType);
                    }

                    cmd.CommandText = query;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }




        private DataTable GetDeletedReportData(DateTime? fromDate = null, DateTime? toDate = null, string reportType = null)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    var query = "SELECT Id, FileName, ReportType, DeletedBy, DeletedDate FROM tbl_mne_Report WHERE Status = 'inactive'";

                    // Append conditions based on criteria
                    if (fromDate.HasValue)
                    {
                        query += " AND DeletedDate >= @FromDate";
                        cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                    }

                    if (toDate.HasValue)
                    {
                        query += " AND DeletedDate <= @ToDate";
                        cmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                    }

                    if (!string.IsNullOrEmpty(reportType))
                    {
                        query += " AND ReportType = @ReportType";
                        cmd.Parameters.AddWithValue("@ReportType", reportType);
                    }

                    cmd.CommandText = query;
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }



        protected void RadGrid_ItemCommand(object sender, GridCommandEventArgs e)
        {
            int reportId = Convert.ToInt32((e.Item as GridDataItem).GetDataKeyValue("Id"));

            if (e.CommandName == "Download")
            {
                DownloadFile(reportId);
            }
            else if (e.CommandName == "Delete")
            {
                DeleteFile(reportId);
            }
            else if (e.CommandName == "Recycle")
            {
                RecycleFile(reportId);
            }
            else if (e.CommandName == "Remove")
            {
                RemoveFile(reportId);
            }
        }


        private void DownloadFile(int reportId)
        {
            try
            {
                int attachmentId = reportId;
                int attachmentType = 23;

                string redirectUrl = $"~/ViewAttachmentHandler.aspx?AttachmentType={attachmentType}&AttachmentValue={attachmentId}";

                // Log or handle any preparation before redirecting, if necessary
                // Example: Log the attempt to redirect
                //Console.WriteLine("Redirecting to: " + redirectUrl); // Or use a proper logging mechanism

                // Perform the redirect
                Response.Redirect(redirectUrl, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                // Handle any unexpected issues gracefully
                Debug.WriteLine("An issue occurred during file download redirection." + ex.Message); // Replace with actual logging
            }
        }


        private void RemoveFile(int reportId)
        {
            UpdateFileStatus(reportId, "Inactive", DateTime.Now, Session["uid"]);
            lblMessageSuccess.Text = "File moved to deleted records.";
            lblMessageSuccess.Visible = true;
            DivSuccess.Visible = true;
            RadGrid.Rebind();
        }

        private void RecycleFile(int reportId)
        {
            UpdateFileStatus(reportId, "Active", null, null);
            lblMessageSuccess.Text = "File restored successfully.";
            lblMessageSuccess.Visible = true;
            DivSuccess.Visible = true;
            RadGrid.Rebind();
        }

        private void DeleteFile(int reportId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Retrieve file path
                string filePath = GetFilePath(reportId, conn);

                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    File.Delete(filePath);  // Delete file from disk
                }

                // Permanently delete from database
                string query = "DELETE FROM tbl_mne_Report WHERE Id = @ReportId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReportId", reportId);
                    cmd.ExecuteNonQuery();
                }

                lblMessageSuccess.Text = "File permanently removed.";
                lblMessageSuccess.Visible = true;
                DivSuccess.Visible = true;
            }

            RadGrid.Rebind();
        }

        private void UpdateFileStatus(int reportId, string status, DateTime? deletedDate, object deletedBy)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE tbl_mne_Report SET Status = @Status, DeletedDate = @DeletedDate, DeletedBy = @DeletedBy WHERE Id = @ReportId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@DeletedDate", deletedDate ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DeletedBy", deletedBy ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@ReportId", reportId);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private string GetFilePath(int reportId, SqlConnection conn)
        {
            string query = "SELECT FilePath FROM tbl_mne_Report WHERE Id = @ReportId";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ReportId", reportId);
                return cmd.ExecuteScalar() as string;
            }
        }

        private void ClearLabels()
        {
            lblMessageSuccess.Text = string.Empty;
            lblMessageSuccess.Visible = false;

            lblMessageError.Text = string.Empty;
            lblMessageError.Visible = false;

            DivSuccess.Visible = false;
            DivFailure.Visible = false;
        }

        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = GetSavedReportData();
            RadGrid.DataSource = GetDeletedReportData();
        }



        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSearchReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlSubmitReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl, false);
        }
    }
}