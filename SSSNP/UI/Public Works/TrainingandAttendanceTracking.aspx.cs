using SSSNP.Component_2.AttendanceTrackingGridView;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;


namespace SSSNP.UI.Public_Works
{
    public partial class TrainingandAttendanceTracking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=17"; // Ensure AttachmentType is set correctly
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
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    // Create a list to hold the parsed data
                    List<TrainingAndAttendanceTracking> attendanceList = new List<TrainingAndAttendanceTracking>();

                    // Loop through the worksheet rows and populate the list
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming row 1 contains headers
                    {
                        TrainingAndAttendanceTracking attendance = new TrainingAndAttendanceTracking
                        {
                            HouseholdNumber = worksheet.Cells[row, 1].Text,
                            CourseCode = worksheet.Cells[row, 2].Text,
                            CourseDuration = (int)(int.TryParse(worksheet.Cells[row, 3].Text, out int courseDuration) ? courseDuration : (int?)null),
                            ClassesAttended = (int)(int.TryParse(worksheet.Cells[row, 4].Text, out int classesAttended) ? classesAttended : (int?)null),
                            ClassesHeld = (int)(int.TryParse(worksheet.Cells[row, 5].Text, out int classesHeld) ? classesHeld : (int?)null),
                            ClassesMissed = (int)(int.TryParse(worksheet.Cells[row, 6].Text, out int classesMissed) ? classesMissed : (int?)null),
                            WhyMissed = worksheet.Cells[row, 7].Text
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
                var attendanceList = Session["UploadedAttendanceData"] as List<TrainingAndAttendanceTracking>;
                if (attendanceList == null || attendanceList.Count == 0)
                {
                    AddErrorMessage("No file found for analysis.");
                    return;
                }

                // Create a DataTable to hold the analyzed data (for RadGrid display)
                DataTable dt = new DataTable();
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
                var attendanceList = Session["UploadedAttendanceData"] as List<TrainingAndAttendanceTracking>;
                if (attendanceList == null || attendanceList.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                // Start database operation with transaction
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            foreach (var attendance in attendanceList)
                            {
                                bool insertSuccess = InsertTrainingAttendanceIntoDB(
                                    conn,
                                    transaction,
                                    attendance.HouseholdNumber,
                                    attendance.CourseCode,
                                    attendance.CourseDuration,
                                    attendance.ClassesAttended,
                                    attendance.ClassesHeld,
                                    attendance.ClassesMissed,
                                    attendance.WhyMissed
                                );

                                if (!insertSuccess)
                                {
                                    transaction.Rollback();
                                    AddErrorMessage("Import failed for Household Number: " + attendance.HouseholdNumber);
                                    return;
                                }
                            }

                            // Commit transaction
                            transaction.Commit();
                            AddSuccessMessage("Data imported successfully.");
                            btnImport.Enabled = false;
                            btnAnalyze.Enabled = false;
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            LogError(ex);
                            AddErrorMessage("Data import failed: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
                AddErrorMessage("Data import failed: " + ex.Message);
            }
        }



        private bool InsertTrainingAttendanceIntoDB(SqlConnection conn, SqlTransaction transaction, string householdNumber, string courseCode, int? courseDuration, int? classesAttended, int? classesHeld, int? classesMissed, string whyMissed)
        {
            string query = @"INSERT INTO tbl_com2_Training_Attendance (HouseholdNumber, CourseCode, CourseDuration, ClassesAttended, ClassesHeld, ClassesMissed, WhyMissed, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
                     VALUES (@HouseholdNumber, @CourseCode, @CourseDuration, @ClassesAttended, @ClassesHeld, @ClassesMissed, @WhyMissed, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate)";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                // Safely convert Session["user_id"] to integer
                int userId;
                bool isUser = int.TryParse(Session["uid"].ToString(), out userId);
                if (isUser)
                {
                    // Add the parameters to the SQL command
                    cmd.Parameters.AddWithValue("@HouseholdNumber", householdNumber);
                    cmd.Parameters.AddWithValue("@CourseCode", courseCode);
                    cmd.Parameters.AddWithValue("@CourseDuration", (object)courseDuration ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ClassesAttended", (object)classesAttended ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ClassesHeld", (object)classesHeld ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ClassesMissed", (object)classesMissed ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@WhyMissed", whyMissed);
                    cmd.Parameters.AddWithValue("@CreatedBy", userId);
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", userId);
                    cmd.Parameters.AddWithValue("@ModifiedDate", DateTime.Now);

                    try
                    {
                        // Execute the query
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (Exception ex)
                    {
                        // Log error if any exception occurs
                        LogError(ex);
                        return false;
                    }
                }
                else
                {
                    // Log an error if user_id is missing or invalid
                    LogError(new Exception("Invalid user ID in session."));
                    return false;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Clear previous messages
            ClearMessages();

            try
            {
                // Fetch data from the database and bind it to RadGrid
                DataTable dt = GetTrainingAttendanceData();
                if (dt.Rows.Count > 0)
                {
                    RadGrid.DataSource = dt;
                    RadGrid.DataBind();
                    AddSuccessMessage("Search completed successfully.");
                }
                else
                {
                    AddErrorMessage("No data found.");
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }

        private DataTable GetTrainingAttendanceData()
        {
            // Define the query to fetch data from the database
            string query = @"SELECT HouseholdNumber, CourseCode, CourseDuration, ClassesAttended, ClassesHeld, ClassesMissed, WhyMissed 
                             FROM tbl_com2_Training_Attendance";

            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        conn.Open();
                        sda.Fill(dt);
                    }
                }
            }

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


        private void LogError(Exception ex)
        {
            string logFilePath = Server.MapPath("~/ErrorLogs/");

            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }

            string filePath = Path.Combine(logFilePath, "ErrorLog_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine("Time: " + DateTime.Now.ToString());
                sw.WriteLine("Message: " + ex.Message);
                sw.WriteLine("StackTrace: " + ex.StackTrace);
                sw.WriteLine("---------------------------------------------------------");
            }
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
