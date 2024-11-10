using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
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
            // Retrieve values from form fields
            string reportType = ddlReportType.SelectedValue;
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
                ddlReportType.SelectedIndex = 0;
                
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
            RadGrid.Visible = true;
            GetReportData();
        }


        private DataTable GetReportData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            string query = "SELECT Id, FileName, ReportType FROM tbl_mne_Report"; // Adjust query if needed

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }


        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            RadGrid.DataSource = GetReportData();
        }


    }
}
