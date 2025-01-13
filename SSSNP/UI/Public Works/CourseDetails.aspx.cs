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


namespace SSSNP.UI.Public_Works
{
    public partial class CourseDetails : System.Web.UI.Page
    {
        private bool isAnalyzed = false;
        public int skipCount = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Disable buttons initially
                btnAnalyze.Enabled = false;
                btnImport.Enabled = false;
                RadGrid.Visible = false;
            }
            btnSearch.Enabled = true;
            //lnkDownloadTemplate.Visible = false;
        }

        protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        {
            // Redirect to the ViewAttachmentHandler to download the Excel file
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=32";
            Response.Redirect(redirectUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ClearMessages();
            try
            {
                try
                {
                    if (!fileUpload.HasFile)
                    {
                        AddErrorMessage("Please select a file to upload.");
                        return;
                    }

                    //AddSuccessMessage($"File uploaded: {fileUpload.FileName}");
                }
                catch (Exception ex)
                {
                    AddErrorMessage("An error occurred: " + ex.Message);
                }

                string extension = Path.GetExtension(fileUpload.FileName);
                if (extension != ".xlsx")
                {
                    AddErrorMessage("Invalid file format. Please upload an Excel (.xlsx) file.");
                    return;
                }

                using (var package = new OfficeOpenXml.ExcelPackage(fileUpload.PostedFile.InputStream))
                {
                    int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    List<string> expectedHeaders = new List<string>
                    {
                        "Course Code", "Class Holding Date", "Duration of held class (Minutes)"
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


                    List<CourseDetailsModel> Course = new List<CourseDetailsModel>();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string courseCode = worksheet.Cells[row, 1].Text;

                        DateTime classHoldingDate = worksheet.Cells[row, 2].GetValue<DateTime>();

                        int classDurationMinutes;
                        if (!int.TryParse(worksheet.Cells[row, 3].Text, out classDurationMinutes))
                        {
                            classDurationMinutes = 0;
                        }

                        Course.Add(new CourseDetailsModel
                        {
                            CourseCode = courseCode,
                            ClassHoldingDate = classHoldingDate,
                            ClassDurationMinutes = classDurationMinutes,
                            CreatedBy = userId,
                            ModifiedBy = userId
                        });
                    }

                    Session["CourseDetails"] = Course;
                    AddSuccessMessage("File uploaded successfully. You can now analyze the data.");
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
            Session["IsAnalyzing"] = true;
            try
            {
                Component2Service service = new Component2Service();

                List<CourseDetailsModel> Course = Session["CourseDetails"] as List<CourseDetailsModel>;
                if (Course == null || Course.Count == 0)
                {
                    AddErrorMessage("No data available for analysis.");
                    return;
                }

                Dictionary<string, int> existingCourseCode = service.GetExistingCourseCode();
                List<CourseDetailsModel> courseCodeList = existingCourseCode
                    .Select(kvp => new CourseDetailsModel
                    {
                        CourseCode = kvp.Key
                    })
                    .ToList();

                HashSet<string> uniqueCourseCode = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var validCourse = new List<CourseDetailsModel>();

                // Create a DataTable to hold the data for display in the grid
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("CourseCode", typeof(string));
                dt.Columns.Add("ClassHoldingDate", typeof(DateTime));
                dt.Columns.Add("ClassDurationMinutes", typeof(int));
                dt.Columns.Add("ValidationStatus", typeof(string));

                int skipCount = 0;
                Dictionary<string, int> existingCourseDetails = service.GetExistingCourseDetails();
                foreach (var course in Course)
                {
                    DataRow dr = dt.NewRow();
                    dr["CourseCode"] = course.CourseCode;
                    dr["ClassHoldingDate"] = course.ClassHoldingDate;
                    dr["ClassDurationMinutes"] = course.ClassDurationMinutes;

                    if (!string.IsNullOrEmpty(course.CourseCode) && service.IsRecordExist_CourseDetails(course.CourseCode, course.ClassHoldingDate))
                    {
                        dr["Id"] = existingCourseDetails[course.CourseCode];
                    }
                    else
                    {
                        dr["Id"] = DBNull.Value; 
                    }


                    var validationResult = service.IsValidData(course, courseCodeList);


                    if (!validationResult.isValid)
                    {
                        dr["ValidationStatus"] = validationResult.errorMessage; // Store the error message
                    }
                    else
                    {
                        dr["ValidationStatus"] = validationResult.errorMessage; // No error
                    }
                    validCourse.Add(course);
                    dt.Rows.Add(dr);

                    /*if (uniqueCourseCode.Contains(course.CourseCode))
                    {
                        // Skip this group because it's a duplicate in the uploaded Excel file
                        skipCount++;
                        continue;
                    }
                    else
                    {
                        // Add the groupName to the HashSet to track it as already added
                        uniqueCourseCode.Add(course.CourseCode);
                        validCourse.Add(course);
                    }*/

                    // dt.Rows.Add(dr);
                }

                // Bind the DataTable to the RadGrid for display
                Session["AnalyzedData"] = dt;
                Session["ValidCourse"] = validCourse;
                Session["skipCount"] = skipCount;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = true;

                int totalRecordCount = dt.Rows.Count;
                Session["TotalRecordCount"] = totalRecordCount;

                // Trigger pagination setup for analysis results
                RadGrid.VirtualItemCount = totalRecordCount;
                RadGrid.CurrentPageIndex = 0;

                // Bind the DataTable to the RadGrid for display
                RadGrid.DataSource = null;
                RadGrid.DataSource = dt;

                RadGrid.Visible = true;
                isAnalyzed = true;

                AddSuccessMessage("Data analyzed successfully.");
                btnImport.Enabled = true;
                RadGrid.DataBind();
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data analysis failed: " + ex.Message);
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            ClearMessages();
            try
            {
                Component2Service service = new Component2Service();
                List<CourseDetailsModel> Course = Session["ValidCourse"] as List<CourseDetailsModel>;

                if (Course == null || Course.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                Dictionary<string, int> existingCourseCode = service.GetExistingCourseCode();
                List<CourseDetailsModel> courseCodeList = existingCourseCode
                    .Select(kvp => new CourseDetailsModel
                    {
                        CourseCode = kvp.Key
                    })
                    .ToList();

                int skipCount = Session["skipCount"] != null ? (int)Session["skipCount"] : 0;

                // Pass the entire list to the service for processing
                var response = service.SaveCourseDetails(Course, courseCodeList);
                if (!response.IsSuccess)
                {
                    AddErrorMessage(response.Message);
                }
                else
                {
                    AddSuccessMessage(response.Message + $" {skipCount} records were skipped due to redundant data.");
                }
                btnImport.Enabled = false;
                btnAnalyze.Enabled = false;
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
            RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = false;
            try
            {
                int totalRecordCount = GetCourseDetailsCount();
                Session["TotalRecordCount"] = totalRecordCount;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.VirtualItemCount = totalRecordCount; // Set total item count for pagination
                RadGrid.DataSource = null;
                RadGrid.Rebind();
                RadGrid.Visible = true;

            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }

        private int GetCourseDetailsCount()
        {
            int totalRecords = 0;
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open(); // Ensure the connection is opened

                    string query = @"SELECT COUNT(*) FROM tbl_com2_Course_Details WHERE 1=1;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        totalRecords = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Error retrieving total record count: " + ex.Message);
            }

            return totalRecords;
        }
        private DataTable GetCourseDetailsData(int pageIndex, int pageSize)
        {
            string query = @"SELECT Id, CourseCode, ClassHoldingDate, ClassDurationMinutes
                     FROM tbl_com2_Course_Details
                     ORDER BY Id 
                     OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Set OFFSET and PageSize for pagination
                        cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                        cmd.Parameters.AddWithValue("@PageSize", pageSize);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Error retrieving data: " + ex.Message);
            }

            return dt;
        }

        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
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
                    // Fetch the data for search result
                    dt = GetCourseDetailsData(RadGrid.CurrentPageIndex, RadGrid.PageSize);
                }

                RadGrid.DataSource = dt;

                if (Session["TotalRecordCount"] != null && int.TryParse(Session["TotalRecordCount"].ToString(), out int totalRecordCount))
                {
                    // Set total item count for pagination
                    RadGrid.VirtualItemCount = totalRecordCount;
                }
                else
                {
                    RadGrid.VirtualItemCount = 0; // Default to 0 if no valid count is found
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data binding failed: " + ex.Message);
            }
        }

        protected void RadGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem dataItem)
            {
                if (isAnalyzed)
                {
                    string validationStatus = dataItem["ValidationStatus"].Text;

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
        }

        private void AddErrorMessage(string message)
        {
            DivFailure.Visible = true;
            lblMessageError.Text = message;
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