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
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP_SNSOP.UI.Public_Works
{
    
    public partial class TrainingandAttendanceTracking : Page
    {
        private Component2Service _component2Service;
        private bool isAnalyzed = false;

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
                        // Handle null or empty values for each column
                        var courseDuration = string.IsNullOrWhiteSpace(worksheet.Cells[row, 3].Text) ? (int?)null : int.Parse(worksheet.Cells[row, 3].Text);
                        var classesAttended = string.IsNullOrWhiteSpace(worksheet.Cells[row, 4].Text) ? (int?)null : int.Parse(worksheet.Cells[row, 4].Text);
                        var classesHeld = string.IsNullOrWhiteSpace(worksheet.Cells[row, 5].Text) ? (int?)null : int.Parse(worksheet.Cells[row, 5].Text);
                        var classesMissed = string.IsNullOrWhiteSpace(worksheet.Cells[row, 6].Text) ? (int?)null : int.Parse(worksheet.Cells[row, 6].Text);
                        var whyMissed = string.IsNullOrWhiteSpace(worksheet.Cells[row, 7].Text) ? null : worksheet.Cells[row, 7].Text;

                        TrainingAndAttendanceModel attendance = new TrainingAndAttendanceModel
                        {
                            HouseholdNumber = worksheet.Cells[row, 1].Text,
                            CourseCode = worksheet.Cells[row, 2].Text,
                            CourseDuration = courseDuration ?? 0, // Default to 0 if null
                            ClassesAttended = classesAttended ?? 0, // Default to 0 if null
                            ClassesHeld = classesHeld ?? 0, // Default to 0 if null
                            ClassesMissed = classesMissed ?? 0, // Default to 0 if null
                            WhyMissed = whyMissed, // Allow null for WhyMissed
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
            catch (Exception ex)
            {
                AddErrorMessage("File upload failed: " + ex.Message);
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
                dt.Columns.Add("CourseDuration", typeof(object)); // Change to object to support null values
                dt.Columns.Add("ClassesAttended", typeof(object)); // Change to object to support null values
                dt.Columns.Add("ClassesHeld", typeof(object)); // Change to object to support null values
                dt.Columns.Add("ClassesMissed", typeof(object)); // Change to object to support null values
                dt.Columns.Add("WhyMissed", typeof(string));
                dt.Columns.Add("ValidationStatus", typeof(string)); // For error messages

                // Store error messages for rows with validation issues
                int serial = 1;
                var errorMessages = new List<string>();

                // Fetch valid household numbers for validation
                var validHouseholdNumbers = _component2Service.GetValidHouseholdNumbers();

                // Iterate over attendanceList and validate each entry
                foreach (var attendance in attendanceList)
                {
                    var validationResult = _component2Service.IsValidData(attendance, validHouseholdNumbers); // Call validation

                    // Populate DataTable with the data
                    DataRow dr = dt.NewRow();
                    dr["Id"] = serial;
                    dr["HouseholdNumber"] = attendance.HouseholdNumber;
                    dr["CourseCode"] = attendance.CourseCode;
                    dr["CourseDuration"] = attendance.CourseDuration ?? (object)DBNull.Value; // Handle null values
                    dr["ClassesAttended"] = attendance.ClassesAttended ?? (object)DBNull.Value; // Handle null values
                    dr["ClassesHeld"] = attendance.ClassesHeld ?? (object)DBNull.Value; // Handle null values
                    dr["ClassesMissed"] = attendance.ClassesMissed ?? (object)DBNull.Value; // Handle null values
                    dr["WhyMissed"] = attendance.WhyMissed;

                    // Collect error messages to display in the ValidationStatus column
                    if (!validationResult.isValid)
                    {
                        errorMessages.Add(validationResult.errorMessage);
                        dr["ValidationStatus"] = string.Join(", ", errorMessages); // Add all error messages
                    }
                    else
                    {
                        dr["ValidationStatus"] = "Valid"; // Set to "Valid" if no errors
                    }

                    dt.Rows.Add(dr);
                    serial++;
                }

                // Bind the data to RadGrid for display
                RadGrid.DataSource = dt;
                RadGrid.Visible = true;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.VirtualItemCount = dt.Rows.Count;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = true;
                isAnalyzed = true;
                RadGrid.DataBind();


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

                // Filter valid entries from the attendance list
                var validEntries = attendanceList.Where(a => _component2Service.IsValidData(a, _component2Service.GetValidHouseholdNumbers()).isValid).ToList();

                if (validEntries.Count == 0)
                {
                    AddErrorMessage("No valid data available for import.");
                    return;
                }

                // Save the valid data using the service layer
                var response = _component2Service.SaveTrainingAndAttendanceTracking(validEntries);

                if (response.IsSuccess)
                {
                    AddSuccessMessage($"{validEntries.Count} valid entries imported successfully.");
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
                RadGrid.CurrentPageIndex = 0;
                int pageSize = RadGrid.PageSize;
                Session["TotalRecordCount"] = GetTrainingAttendanceDataCount();
                RadGrid.DataSource = GetTrainingAttendanceData(RadGrid.CurrentPageIndex, pageSize);
                RadGrid.Visible = true;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = false;
                RadGrid.DataBind();
            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }





        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = GetTrainingAttendanceData(RadGrid.CurrentPageIndex, RadGrid.PageSize); // Fetch data
            RadGrid.DataSource = dt;

            // Update total record count for pagination
            int totalRecordCount = GetTrainingAttendanceDataCount();
            RadGrid.VirtualItemCount = totalRecordCount; // Total records in the database
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
                // Log the exception (consider using a logging framework)
                AddErrorMessage("An error occurred while counting records: " + ex.Message);
            }

            return totalRecords;
        }

        private DataTable GetTrainingAttendanceData(int pageIndex, int pageSize)
        {
            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                // Construct the query with parameters for pagination
                string query = @"SELECT Id, HouseholdNumber, CourseCode, CourseDuration, ClassesAttended, ClassesHeld, ClassesMissed, WhyMissed 
                         FROM tbl_com2_Training_Attendance 
                         ORDER BY Id 
                         OFFSET @Offset ROWS 
                         FETCH NEXT @PageSize ROWS ONLY;";

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
                RadGrid.VirtualItemCount = GetTrainingAttendanceDataCount(); // Get total records
            }
            catch (Exception ex)
            {
                // Log the exception (consider using a logging framework)
                AddErrorMessage("An error occurred while retrieving data: " + ex.Message);
            }

            return dt;
        }

        protected void RadGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            // Check if the item is a data row
            if (e.Item is GridDataItem dataItem)
            {
                // Only change color if the data has been analyzed
                if (isAnalyzed)
                {
                    // Get the validation status from the row
                    string validationStatus = dataItem["ValidationStatus"].Text;

                    // If the status is not valid, change the background color
                    if (validationStatus != "Valid")
                    {
                        dataItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFB1B4");
                    }
                }
            }
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