using SSSNP.Service;
using SSSNP_SNSOP.Core.Model.Component2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

namespace SSSNP_SNSOP.UI.Public_Works
{
    
    public partial class TrainingandAttendanceTracking : Page
    {
        private Component2Service _component2Service;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize the service
            _component2Service = new Component2Service();

            if (!IsPostBack)
            {
                // Disable buttons initially
                btnAnalyze.Enabled = false;
                btnImport.Enabled = false;
                RadGrid.Visible = false;
            }
        }

        protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        {
            // Redirect to the ViewAttachmentHandler to download the Excel file
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=17";
            Response.Redirect(redirectUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
            try
            {
                if (!fileUpload.HasFile)
                {
                    AddErrorMessage("Please select a file to upload.");
                    return;
                }

                string extension = Path.GetExtension(fileUpload.FileName);
                if (extension != ".xlsx")
                {
                    AddErrorMessage("Invalid file format. Please upload an Excel (.xlsx) file.");
                    return;
                }

                // Parse the Excel file into a list of TrainingAttendance objects
                using (var package = new OfficeOpenXml.ExcelPackage(fileUpload.PostedFile.InputStream))
                {
                    int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    // Create a list to hold the parsed data
                    List<TrainingAndAttendanceModel> attendanceList = new List<TrainingAndAttendanceModel>();

                    // Loop through the worksheet rows and populate the list
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming row 1 contains headers
                    {
                        TrainingAndAttendanceModel attendance = new TrainingAndAttendanceModel
                        {
                            HouseholdNumber = worksheet.Cells[row, 1].Text,
                            CourseCode = worksheet.Cells[row, 2].Text,
                            CourseDuration = int.Parse(worksheet.Cells[row, 3].Text),
                            ClassesAttended = int.Parse(worksheet.Cells[row, 4].Text),
                            ClassesHeld = int.Parse(worksheet.Cells[row, 5].Text),
                            ClassesMissed = int.Parse(worksheet.Cells[row, 6].Text),
                            WhyMissed = worksheet.Cells[row, 7].Text,
                            CreatedBy = userId,
                            ModifiedBy = userId
                        };

                        attendanceList.Add(attendance);
                    }

                    // Store the list in session for further processing
                    Session["UploadedAttendanceData"] = attendanceList;

                    AddSuccessMessage("File uploaded and data parsed successfully. You can now analyze it.");
                    btnAnalyze.Enabled = true;
                }
            }
            catch /*(Exception ex)*/
            {
                //AddErrorMessage("File upload failed: " + ex.Message);
            }
        }

        // Update the btnAnalyze_Click method to match the new method signature
        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
            var _component2Service = new Component2Service();

            try
            {
                // Retrieve the list of TrainingAttendance objects from session
                var attendanceList = Session["UploadedAttendanceData"] as List<TrainingAndAttendanceModel>;
                if (attendanceList == null || attendanceList.Count == 0)
                {
                    AddErrorMessage("No file found for analysis.");
                    return;
                }

                // Create a DataTable to hold the analyzed data (for RadGrid display)
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("HouseholdNumber", typeof(string));
                dt.Columns.Add("CourseCode", typeof(string));
                dt.Columns.Add("CourseDuration", typeof(int));
                dt.Columns.Add("ClassesAttended", typeof(int));
                dt.Columns.Add("ClassesHeld", typeof(int));
                dt.Columns.Add("ClassesMissed", typeof(int));
                dt.Columns.Add("WhyMissed", typeof(string));

                // Store error messages for invalid rows
                var errorMessages = new List<string>();
                int serial = 1;

                // Fetch valid household numbers for validation
                var validHouseholdNumbers = _component2Service.GetValidHouseholdNumbers();

                // Iterate over attendanceList and validate each entry
                foreach (var attendance in attendanceList)
                {
                    var validationResult = _component2Service.IsValidData(attendance, validHouseholdNumbers); // Call validation

                    if (!validationResult.isValid) // If not valid, store the error message
                    {
                        errorMessages.Add($"Row {serial}: {validationResult.errorMessage}");
                        serial++;
                        continue; // Skip adding this row to the DataTable
                    }

                    // Populate DataTable with the valid data
                    DataRow dr = dt.NewRow();
                    dr["Id"] = serial;
                    dr["HouseholdNumber"] = attendance.HouseholdNumber;
                    dr["CourseCode"] = attendance.CourseCode;
                    dr["CourseDuration"] = attendance.CourseDuration;
                    dr["ClassesAttended"] = attendance.ClassesAttended;
                    dr["ClassesHeld"] = attendance.ClassesHeld;
                    dr["ClassesMissed"] = attendance.ClassesMissed;
                    dr["WhyMissed"] = attendance.WhyMissed;
                    dt.Rows.Add(dr);

                    serial++;
                }

                // If there are validation errors, display them and do not bind the data
                if (errorMessages.Count > 0)
                {
                    AddErrorMessage(string.Join("<br />", errorMessages));
                    return;
                }

                // Bind the valid data to RadGrid for display
                RadGrid.DataSource = dt;
                RadGrid.Visible = true;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.DataBind();
                RadGrid.VirtualItemCount = dt.Rows.Count;

                AddSuccessMessage("File analyzed successfully. You can now import it.");
                btnImport.Enabled = true;
            }
            catch (Exception ex)
            {
                AddErrorMessage("File analysis failed: " + ex.Message);
            }
        }






        protected void btnImport_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
            try
            {
                // Retrieve the list of TrainingAttendance objects from session
                var attendanceList = Session["UploadedAttendanceData"] as List<TrainingAndAttendanceModel>;
                if (attendanceList == null || attendanceList.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                // Save the data using the service layer
                var response = _component2Service.SaveTrainingAndAttendanceTracking(attendanceList);

                if (response.IsSuccess)
                {
                    AddSuccessMessage("Data imported successfully.");
                    btnImport.Enabled = false;
                    btnAnalyze.Enabled = false;
                }
                else
                {
                    AddErrorMessage("Import failed: " + response.Message);
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data import failed: " + ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            ClearMessages(); // Clear any previous messages

            try
            {
                RadGrid.Visible = true;
                Session["TotalRecordCount"] = GetTrainingAttendanceDataCount();
                RadGrid.CurrentPageIndex = 0;
                RadGrid.DataBind();

            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }



        // Fetch data from the database and provide for RadGrid
        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = GetTrainingAttendanceData(RadGrid.CurrentPageIndex, RadGrid.PageSize); // Fetch all data
            RadGrid.DataSource = dt;
            Session["TotalRecordCount"] = GetTrainingAttendanceDataCount();
            RadGrid.VirtualItemCount = (int)Session["TotalRecordCount"];
        }


        // Fetch paginated data from the database
        private int GetTrainingAttendanceDataCount()
        {
            int totalRecords = 0;
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open(); // Ensure the connection is opened

                    string query = @"SELECT COUNT(*) FROM tbl_com2_Training_Attendance WHERE 1=1;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        totalRecords = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw; // Log or handle the exception as necessary
            }

            return totalRecords;
        }


        private DataTable GetTrainingAttendanceData(int pageIndex, int pageSize)
        {
            string query = @"SELECT Id, HouseholdNumber, CourseCode, CourseDuration, ClassesAttended, ClassesHeld, ClassesMissed, WhyMissed 
        FROM tbl_com2_Training_Attendance ORDER BY Id 
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            // Update the total record count for pagination
            RadGrid.VirtualItemCount = (int)Session["TotalRecordCount"];
            return dt;
        }



        private void AddSuccessMessage(string message)
        {
            DivSuccess.Visible = true;
            lblMessageSuccess.Text = message;

            // Hide the success message after 5 seconds
            ScriptManager.RegisterStartupScript(this, GetType(), "HideSuccess",
                "setTimeout(function() { document.getElementById('" + DivSuccess.ClientID + "').style.display = 'none'; }, 5000);", true);
        }

        private void AddErrorMessage(string message)
        {
            DivFailure.Visible = true;
            lblMessageError.Text = message;

            // Hide the error message after 5 seconds
            ScriptManager.RegisterStartupScript(this, GetType(), "HideError",
                "setTimeout(function() { document.getElementById('" + DivFailure.ClientID + "').style.display = 'none'; }, 5000);", true);
        }

        protected void ClearMessages()
        {
            DivSuccess.Visible = false;
            DivFailure.Visible = false;
            lblMessageSuccess.Text = string.Empty;
            lblMessageError.Text = string.Empty;
        }
    }
}