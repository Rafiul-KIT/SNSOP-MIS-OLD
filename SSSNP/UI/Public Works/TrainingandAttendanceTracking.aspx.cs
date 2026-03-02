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
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.UI;


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
            btnSearch.Enabled = true;
            lnkDownloadTemplate.Visible = false;
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

                    //for checking header
                    
                    List<string> expectedHeaders = new List<string>
                    {
                        "Household Number", "Course Code", "Class Attendance Date"
                    };

                    // Validate headers
                    for (int col = 1; col <= expectedHeaders.Count; col++)
                    {
                        var header = worksheet.Cells[1, col].Text.Trim();
                        if (header != expectedHeaders[col - 1])
                        {
                            AddErrorMessage($"Invalid file format. The header '{header}' does not match the expected header '{expectedHeaders[col - 1]}' in column {col}.");
                            return;
                        }
                    }

                    // Create a list to hold the parsed data
                    List<TrainingAndAttendanceModel> attendanceList = new List<TrainingAndAttendanceModel>();

                    // Loop through the worksheet rows and populate the list
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming row 1 contains headers
                    {
                        // Handle null or empty values for each column
                        var courseCode = string.IsNullOrWhiteSpace(worksheet.Cells[row, 2].Text) ? null : worksheet.Cells[row, 2].Text;
                        //var courseDuration = string.IsNullOrWhiteSpace(worksheet.Cells[row, 3].Text) ? (int?)null : int.Parse(worksheet.Cells[row, 3].Text);
                        var classAttendanceDate = DateTime.TryParse(worksheet.Cells[row, 3].Text, out DateTime parsedDate) ? (DateTime?)parsedDate: null;

                        if (string.IsNullOrWhiteSpace(courseCode))
                        {
                            continue; // Skip this row
                        }

                        TrainingAndAttendanceModel attendance = new TrainingAndAttendanceModel
                        {
                            HouseholdNumber = worksheet.Cells[row, 1].Text,
                            CourseCode = courseCode,
                            ClassAttendanceDate = classAttendanceDate,
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
            Session["IsAnalyzing"] = true;
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
                dt.Columns.Add("ClassAttendanceDate", typeof(DateTime)); // Change to object to support null values
                dt.Columns.Add("ValidationStatus", typeof(string)); // For error messages

                // Store error messages for rows with validation issues
                int serial = 1;

                // Fetch valid household numbers for validation
                var validHouseholdNumbers = _component2Service.GetValidHouseholdNumbers();
                var validAttendanceList = attendanceList.Where(grant => !string.IsNullOrWhiteSpace(grant.HouseholdNumber)).ToList();

                var uploadedDataList = new List<TrainingAndAttendanceAuditModel>();
                int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
                Guid myuuid = Guid.NewGuid();
                string FileId = myuuid.ToString().Replace("-", "").Substring(0, 10);

                // Iterate over attendanceList and validate each entry
                foreach (var attendance in validAttendanceList)
                { 
                    var validationResult = _component2Service.IsValidData(attendance, validHouseholdNumbers); // Call validation

                    // Populate DataTable with the data
                    DataRow dr = dt.NewRow();
                    dr["Id"] = serial;
                    dr["HouseholdNumber"] = attendance.HouseholdNumber;
                    dr["CourseCode"] = attendance.CourseCode;
                    dr["ClassAttendanceDate"] = attendance.ClassAttendanceDate;

                    TrainingAndAttendanceAuditModel auditEntityObj = new TrainingAndAttendanceAuditModel();
                    auditEntityObj.FileId = FileId;
                    auditEntityObj.HouseholdNumber = attendance.HouseholdNumber;
                    auditEntityObj.CourseCode = attendance.CourseCode;
                    auditEntityObj.ClassAttendanceDate = attendance.ClassAttendanceDate;

                    // Collect error messages to display in the ValidationStatus column
                    if (!validationResult.isValid)
                    {
                        auditEntityObj.Validity = false;
                        auditEntityObj.InvalidReason = validationResult.errorMessage;
                        dr["ValidationStatus"] = validationResult.errorMessage;
                    }
                    else
                    {
                        auditEntityObj.Validity = true;
                        dr["ValidationStatus"] = "Valid"; // Set to "Valid" if no errors
                    }

                    auditEntityObj.UploadedBy = userId;
                    uploadedDataList.Add(auditEntityObj);

                    dt.Rows.Add(dr);
                    serial++;
                }

                Session["UploadedTrainingAndAttendanceAllData"] = uploadedDataList;

                // Bind the data to RadGrid for display
                Session["AnalyzedData"] = dt;
                int totalRecordCount = dt.Rows.Count;
                Session["TotalRecordCount"] = totalRecordCount;
                RadGrid.VirtualItemCount = totalRecordCount;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.DataSource = null;
                RadGrid.DataSource = dt;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = true;
                RadGrid.DataBind();
                RadGrid.Visible = true;

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

                var allDataList = Session["UploadedTrainingAndAttendanceAllData"] as List<TrainingAndAttendanceAuditModel>;
                if (allDataList != null && allDataList.Count > 0)
                {
                    _component2Service.SaveAuditLog(allDataList, "tbl_com2_Training_Attendance_Audit");
                }
                Session["UploadedTrainingAndAttendanceAllData"] = null;

                // Filter valid entries from the attendance list
                /*var validEntries = attendanceList.Where(a => _component2Service.IsValidData(a, _component2Service.GetValidHouseholdNumbers()).isValid).ToList();

                if (validEntries.Count == 0)
                {
                    AddErrorMessage("No valid data available for import.");
                    return;
                }*/

                // Save the valid data using the service layer
                var response = _component2Service.SaveTrainingAndAttendanceTracking(attendanceList);

                if (response.IsSuccess)
                {
                    AddSuccessMessage($"{attendanceList.Count} valid entries imported successfully.");
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
            ClearMessages();
            Session["IsAnalyzing"] = false;

            try
            {
                int totalRecordCount = GetTrainingAttendanceDataCount();
                Session["TotalRecordCount"] = totalRecordCount;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.VirtualItemCount = totalRecordCount; // Set total item count for pagination
                RadGrid.DataSource = null;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = false;
                RadGrid.Rebind();
                RadGrid.Visible = true;
            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }





        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataTable dt;

                if (Session["IsAnalyzing"] != null && (bool)Session["IsAnalyzing"])
                {
                    // Fetch analyzed data from session
                    dt = Session["AnalyzedData"] as DataTable;
                }
                else
                {
                    // Fetch the data for the search result
                    dt = GetTrainingAttendanceData(RadGrid.CurrentPageIndex, RadGrid.PageSize);
                }

                // Bind the fetched data to the RadGrid
                RadGrid.DataSource = dt;

                if (Session["TotalRecordCount"] != null && int.TryParse(Session["TotalRecordCount"].ToString(), out int totalRecordCount))
                {
                    // Set total item count for pagination
                    RadGrid.VirtualItemCount = totalRecordCount;
                }
                else
                {
                    // If no total record count in session, set default to 0
                    RadGrid.VirtualItemCount = 0;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during data binding
                AddErrorMessage("Data binding failed: " + ex.Message);
            }
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
                string query = @"SELECT Id, HouseholdNumber, CourseCode, ClassAttendanceDate 
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
            if (e.Item is GridDataItem dataItem)
            {
                DataTable analyzedData = Session["AnalyzedData"] as DataTable;
                // Check if the analysis has been performed
                if (Session["IsAnalyzing"] != null && (bool)Session["IsAnalyzing"])
                {
                    // Get the analyzed data from the session
                    
                    if (analyzedData != null)
                    {
                        // Get the current row's HouseholdNumber or any identifier
                        string householdNumber = dataItem["HouseholdNumber"].Text;

                        // Find the row in the analyzedData DataTable that matches the current row
                        var rows = analyzedData.AsEnumerable()
                                               .Where(r => r.Field<string>("HouseholdNumber") == householdNumber);

                        // Check if there's an invalid entry
                        if (rows.Any(r => r.Field<string>("ValidationStatus") != "Valid"))
                        {
                            dataItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFB1B4"); // Highlight invalid
                        }
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

            // Hide the error message after 10 seconds
            ScriptManager.RegisterStartupScript(this, GetType(), "HideError",
                "setTimeout(function() { document.getElementById('" + DivFailure.ClientID + "').style.display = 'none'; }, 10000);", true);
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