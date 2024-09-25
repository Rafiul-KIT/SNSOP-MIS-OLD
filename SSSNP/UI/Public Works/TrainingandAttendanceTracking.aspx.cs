using SSSNP.Service;
using SSSNP_SNSOP.Core.Model.Component2;
using System;
using System.Collections.Generic;
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
        private bool isSearchClicked = false;
        private Component2Service _component2Service;
        private int totalRecords;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize the service
            _component2Service = new Component2Service();

            if (!IsPostBack)
            {
                // Disable buttons initially
                btnAnalyze.Enabled = false;
                btnImport.Enabled = false;
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
                            CourseDuration = (int)(int.TryParse(worksheet.Cells[row, 3].Text, out int courseDuration) ? courseDuration : (int?)null),
                            ClassesAttended = (int)(int.TryParse(worksheet.Cells[row, 4].Text, out int classesAttended) ? classesAttended : (int?)null),
                            ClassesHeld = (int)(int.TryParse(worksheet.Cells[row, 5].Text, out int classesHeld) ? classesHeld : (int?)null),
                            ClassesMissed = (int)(int.TryParse(worksheet.Cells[row, 6].Text, out int classesMissed) ? classesMissed : (int?)null),
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
            catch (Exception ex)
            {
                AddErrorMessage("File upload failed: " + ex.Message);
            }
        }

        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
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

                // Populate DataTable with the data from the list
                foreach (var attendance in attendanceList)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = attendance.Id;
                    dr["HouseholdNumber"] = attendance.HouseholdNumber;
                    dr["CourseCode"] = attendance.CourseCode;
                    dr["CourseDuration"] = attendance.CourseDuration;
                    dr["ClassesAttended"] = attendance.ClassesAttended;
                    dr["ClassesHeld"] = attendance.ClassesHeld;
                    dr["ClassesMissed"] = attendance.ClassesMissed;
                    dr["WhyMissed"] = attendance.WhyMissed;
                    dt.Rows.Add(dr);
                }

                // Bind the data to RadGrid for display
                RadGrid.DataSource = dt;
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
                // Call a method to fetch all data without any filters
                DataTable dt = GetTrainingAttendanceData(true); // Pass true to indicate all records

                // Bind the data to RadGrid
                RadGrid.DataSource = dt;
                RadGrid.VirtualItemCount = totalRecords; // Update total record count for pagination
                RadGrid.Rebind(); // Rebind RadGrid to refresh data

                if (dt.Rows.Count == 0)
                {
                    AddErrorMessage("No records found.");
                }

                // Set the flag to true indicating the search was performed
                isSearchClicked = true;
            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }



        // Fetch data from the database and provide for RadGrid
        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (isSearchClicked) // Only fetch data if the search button has been clicked
            {
                DataTable dt = GetTrainingAttendanceData(true); // Fetch all data
                RadGrid.VirtualItemCount = totalRecords; // Set total number of records for paging
                RadGrid.DataSource = dt;
            }
        }


        // Handle Page Size change
        protected void RadGrid_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            RadGrid.PageSize = e.NewPageSize;
            RadGrid.Rebind();
        }

        // Handle Page Index change
        protected void RadGrid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            RadGrid.CurrentPageIndex = e.NewPageIndex;
            RadGrid.Rebind();
        }

        // Fetch paginated data from the database
        private DataTable GetTrainingAttendanceData(bool fetchAll = false)
        {
            string query;

            if (fetchAll)
            {
                query = @"SELECT Id, HouseholdNumber, CourseCode, CourseDuration, ClassesAttended, ClassesHeld, ClassesMissed, WhyMissed 
                  FROM tbl_com2_Training_Attendance
                  ORDER BY Id;";

                totalRecords = 0; // Reset total records to 0 since we'll fetch all
            }
            else
            {
                query = @"SELECT Id, HouseholdNumber, CourseCode, CourseDuration, ClassesAttended, ClassesHeld, ClassesMissed, WhyMissed 
                  FROM tbl_com2_Training_Attendance
                  ORDER BY Id
                  OFFSET (@PageIndex - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;

                  -- Total record count
                  SELECT COUNT(*) FROM tbl_com2_Training_Attendance;";
            }

            DataTable dt = new DataTable();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // If not fetching all, set parameters for pagination
                    if (!fetchAll)
                    {
                        cmd.Parameters.AddWithValue("@PageIndex", RadGrid.CurrentPageIndex + 1); // Page index is 1-based
                        cmd.Parameters.AddWithValue("@PageSize", RadGrid.PageSize); // Page size
                    }

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Load the data into the DataTable
                        dt.Load(reader);

                        // If fetching all data, set totalRecords based on the number of rows returned
                        if (fetchAll)
                        {
                            totalRecords = dt.Rows.Count; // Get the total records for all data
                        }
                        else
                        {
                            // Move to the second result set and get total record count
                            if (reader.NextResult() && reader.Read())
                            {
                                totalRecords = reader.GetInt32(0);
                            }
                        }
                    }
                }
            }

            // Update the virtual item count for pagination
            RadGrid.VirtualItemCount = totalRecords;

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