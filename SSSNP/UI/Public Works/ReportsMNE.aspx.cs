using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
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

            // Validate file type and size
            string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
            int maxFileSize = 5 * 1024 * 1024; // 5 MB in bytes

            // Allowed file extensions
            string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx" };

            if (!allowedExtensions.Contains(fileExtension))
            {
                lblMessageError.Text = "Only PDF, DOC, DOCX, XLS, and XLSX files are allowed.";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Visible = true;
                DivFailure.Visible = true;
                return;
            }

            if (fileUpload.PostedFile.ContentLength > maxFileSize)
            {
                lblMessageError.Text = "File size cannot exceed 5 MB.";
                lblMessageError.ForeColor = System.Drawing.Color.Red;
                lblMessageError.Visible = true;
                DivFailure.Visible = true;
                return;
            }

            // Define the file name and file path
            string fileName = fileUpload.FileName;

            
            string baseFolderPath = ConfigurationManager.AppSettings["PaymentCycleDocumentPath"];
            string folderPath = Path.Combine(baseFolderPath, "ReportsMnE");
            string filePath = Path.Combine(folderPath, fileName);
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Check if the file already exists in the database
                string checkQuery = "SELECT COUNT(1) FROM tbl_mne_Report WHERE FileName = @FileName";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@FileName", fileName);

                    int fileExists = (int)checkCmd.ExecuteScalar();

                    if (fileExists > 0)
                    {
                        // Display message if file already exists
                        lblMessageError.Text = "This file is already in the system.";
                        lblMessageError.ForeColor = System.Drawing.Color.Red;
                        lblMessageError.Visible = true;
                        DivFailure.Visible = true;
                        return;
                    }
                }

                // Attempt to save the file and perform database insertion
                try
                {
                    // Save the file
                    fileUpload.SaveAs(filePath);

                    // Insert the file information into the database
                    string insertQuery = "INSERT INTO tbl_mne_Report (FileName, FilePath, UploadedBy, UploadedDate, ReportType, Status) " +
                                         "VALUES (@FileName, @FilePath, @UploadedBy, @UploadedDate, @ReportType, @Status)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@FileName", fileName);
                        insertCmd.Parameters.AddWithValue("@FilePath", filePath);
                        insertCmd.Parameters.AddWithValue("@UploadedBy", uploadedBy);
                        insertCmd.Parameters.AddWithValue("@UploadedDate", uploadedDate);
                        insertCmd.Parameters.AddWithValue("@ReportType", reportType);
                        insertCmd.Parameters.AddWithValue("@Status", status);

                        insertCmd.ExecuteNonQuery();
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
                finally
                {
                    conn.Close();
                }
            }
        }


        protected void ddlReportType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            ClearLabels();
            ViewState["ReportMode"] = "Saved";
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

            ViewState["ReportType"] = reportType;
            ViewState["FromDate"] = fromDate;
            ViewState["ToDate"] = toDate;

            RadGrid.CurrentPageIndex = 0;
            RadGrid.Rebind();
            RadGrid.Visible = true;
        }



        protected void btnShowDeletedRecords_Click(object sender, EventArgs e)
        {
            ClearLabels();
            ViewState["ReportMode"] = "Deleted";
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
           
            ViewState["ReportType"] = reportType;
            ViewState["FromDate"] = fromDate;
            ViewState["ToDate"] = toDate;


            RadGrid.CurrentPageIndex = 0;
            RadGrid.Rebind();
            RadGrid.Visible = true;
        }



        private DataTable GetSavedReportData(int pageIndex, int pageSize, out int totalRecords, DateTime? fromDate = null, DateTime? toDate = null, string reportType = null)
        {
            totalRecords = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Calculate total records
                string countQuery = @"
            SELECT COUNT(*)
            FROM tbl_mne_Report AS r
            WHERE r.Status = 'Active'";

                if (fromDate.HasValue)
                {
                    countQuery += " AND r.UploadedDate >= @FromDate";
                }

                if (toDate.HasValue)
                {
                    countQuery += " AND r.UploadedDate <= @ToDate";
                }

                if (!string.IsNullOrEmpty(reportType))
                {
                    countQuery += " AND r.ReportType = @ReportType";
                }

                using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                {
                    if (fromDate.HasValue)
                    {
                        countCmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                    }
                    if (toDate.HasValue)
                    {
                        countCmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                    }
                    if (!string.IsNullOrEmpty(reportType))
                    {
                        countCmd.Parameters.AddWithValue("@ReportType", reportType);
                    }

                    totalRecords = Convert.ToInt32(countCmd.ExecuteScalar());
                }

                // Fetch paginated data
                string dataQuery = @"
            SELECT * FROM (
                SELECT ROW_NUMBER() OVER (ORDER BY r.UploadedDate ASC) AS RowNum, 
                       r.Id, 
                       r.FileName, 
                       r.ReportType, 
                       u.User_name AS UploadedBy, 
                       r.UploadedDate
                FROM tbl_mne_Report AS r
                LEFT JOIN tbl_Users AS u ON r.UploadedBy = u.intUserid
                WHERE r.Status = 'Active'";

                if (fromDate.HasValue)
                {
                    dataQuery += " AND r.UploadedDate >= @FromDate";
                }
                if (toDate.HasValue)
                {
                    dataQuery += " AND r.UploadedDate <= @ToDate";
                }
                if (!string.IsNullOrEmpty(reportType))
                {
                    dataQuery += " AND r.ReportType = @ReportType";
                }

                dataQuery += @"
            ) AS Result
            WHERE RowNum BETWEEN @StartRow AND @EndRow";

                int startRow = (pageIndex * pageSize) + 1;
                int endRow = (pageIndex + 1) * pageSize;

                using (SqlCommand dataCmd = new SqlCommand(dataQuery, conn))
                {
                    if (fromDate.HasValue)
                    {
                        dataCmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                    }
                    if (toDate.HasValue)
                    {
                        dataCmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                    }
                    if (!string.IsNullOrEmpty(reportType))
                    {
                        dataCmd.Parameters.AddWithValue("@ReportType", reportType);
                    }

                    dataCmd.Parameters.AddWithValue("@StartRow", startRow);
                    dataCmd.Parameters.AddWithValue("@EndRow", endRow);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(dataCmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }

            }

            

        }



        private DataTable GetDeletedReportData(int pageIndex, int pageSize, out int totalRecords, DateTime? fromDate = null, DateTime? toDate = null, string reportType = null)
        {
            totalRecords = 0;

            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Step 1: Calculate total record count
                string countQuery = @"
            SELECT COUNT(*)
            FROM tbl_mne_Report AS r
            WHERE r.Status = 'Inactive'";

                if (fromDate.HasValue)
                {
                    countQuery += " AND r.DeletedDate >= @FromDate";
                }

                if (toDate.HasValue)
                {
                    countQuery += " AND r.DeletedDate <= @ToDate";
                }

                if (!string.IsNullOrEmpty(reportType))
                {
                    countQuery += " AND r.ReportType = @ReportType";
                }

                using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                {
                    if (fromDate.HasValue)
                    {
                        countCmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                    }
                    if (toDate.HasValue)
                    {
                        countCmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                    }
                    if (!string.IsNullOrEmpty(reportType))
                    {
                        countCmd.Parameters.AddWithValue("@ReportType", reportType);
                    }

                    totalRecords = Convert.ToInt32(countCmd.ExecuteScalar());
                }

                // Step 2: Fetch paginated data
                string dataQuery = @"
            SELECT * FROM (
                SELECT ROW_NUMBER() OVER (ORDER BY r.DeletedDate ASC) AS RowNum, 
                       r.Id, 
                       r.FileName, 
                       r.ReportType, 
                       u.User_name AS DeletedBy, 
                       r.DeletedDate
                FROM tbl_mne_Report AS r
                LEFT JOIN tbl_Users AS u ON r.DeletedBy = u.intUserid
                WHERE r.Status = 'Inactive'";

                if (fromDate.HasValue)
                {
                    dataQuery += " AND r.DeletedDate >= @FromDate";
                }
                if (toDate.HasValue)
                {
                    dataQuery += " AND r.DeletedDate <= @ToDate";
                }
                if (!string.IsNullOrEmpty(reportType))
                {
                    dataQuery += " AND r.ReportType = @ReportType";
                }

                dataQuery += @"
            ) AS Result
            WHERE RowNum BETWEEN @StartRow AND @EndRow";

                int startRow = (pageIndex * pageSize) + 1;
                int endRow = (pageIndex + 1) * pageSize;

                using (SqlCommand dataCmd = new SqlCommand(dataQuery, conn))
                {
                    if (fromDate.HasValue)
                    {
                        dataCmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                    }
                    if (toDate.HasValue)
                    {
                        dataCmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                    }
                    if (!string.IsNullOrEmpty(reportType))
                    {
                        dataCmd.Parameters.AddWithValue("@ReportType", reportType);
                    }

                    dataCmd.Parameters.AddWithValue("@StartRow", startRow);
                    dataCmd.Parameters.AddWithValue("@EndRow", endRow);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(dataCmd))
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
            if (e.Item is GridDataItem dataItem)
            {
                
                object dataKey = dataItem.GetDataKeyValue("Id");
                if (dataKey == null)
                {
                    
                    return;
                }

                
                int reportId = Convert.ToInt32(dataKey);

                
                switch (e.CommandName)
                {
                    case "Download":
                        DownloadFile(reportId);
                        break;

                    case "Delete":
                        DeleteFile(reportId);
                        break;

                    case "Recover":
                        RecoverFile(reportId);
                        break;

                    case "Remove":
                        RemoveFile(reportId);
                        break;

                    default:
                        
                        break;
                }
            }
        }





        private void DownloadFile(int reportId)
        {
            try
            {
                int attachmentId = reportId;
                int attachmentType = 23;

                string redirectUrl = $"~/ViewAttachmentHandler.aspx?AttachmentType={attachmentType}&AttachmentValue={attachmentId}";

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

        private void RecoverFile(int reportId)
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

                // Retrieve the file path from the database
                string filePath = GetFilePath(reportId, conn);
                string fullPath = Path.IsPathRooted(filePath) ? filePath : Server.MapPath(filePath);
              
                    if (File.Exists(fullPath))
                    {
                        try
                        {
                            File.Delete(fullPath); // Delete file from the disk
                        }
                        catch (Exception ex)
                        {
                            // Log error if necessary and notify user
                            Debug.WriteLine("Error deleting file: " + ex.Message);
                            lblMessageError.Text = "Failed to delete the file from disk.";
                            lblMessageError.ForeColor = System.Drawing.Color.Red;
                            lblMessageError.Visible = true;
                            DivFailure.Visible = true;
                            conn.Close();
                            return; // Exit without deleting database entry if file deletion fails
                        }
                    }
                    else
                    {
                        lblMessageError.Text = "File not found on disk.";
                        lblMessageError.ForeColor = System.Drawing.Color.Red;
                        lblMessageError.Visible = true;
                        DivFailure.Visible = true;
                    }
                

                // Permanently delete from the database
                string query = "DELETE FROM tbl_mne_Report WHERE Id = @ReportId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReportId", reportId);
                    cmd.ExecuteNonQuery();
                }

                // Notify success
                lblMessageSuccess.Text = "File permanently removed from database and disk.";
                lblMessageSuccess.ForeColor = System.Drawing.Color.Green;
                lblMessageSuccess.Visible = true;
                DivSuccess.Visible = true;
                conn.Close();
            }

            // Refresh the grid to reflect changes
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
                conn.Close();
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
            int pageIndex = RadGrid.CurrentPageIndex;
            int pageSize = RadGrid.PageSize;

            string reportType = ViewState["ReportType"] as string;
            DateTime? fromDate = ViewState["FromDate"] as DateTime?;
            DateTime? toDate = ViewState["ToDate"] as DateTime?;

            int totalRecords;
            
            if (ViewState["ReportMode"] != null && ViewState["ReportMode"].ToString() == "Deleted")
            {
                RadGrid.DataSource = GetDeletedReportData(pageIndex, pageSize, out totalRecords, fromDate, toDate, reportType);
            }
            else
            {
                RadGrid.DataSource = GetSavedReportData(pageIndex, pageSize, out totalRecords, fromDate, toDate, reportType);
            }

            RadGrid.VirtualItemCount = totalRecords;

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
            // Clear specific ViewState values
            ViewState["ReportType"] = null;
            ViewState["FromDate"] = null;
            ViewState["ToDate"] = null;
            ViewState["ReportMode"] = null;
            // Refresh the page to reset controls
            Response.Redirect(Request.RawUrl, false);
        }
    }
}