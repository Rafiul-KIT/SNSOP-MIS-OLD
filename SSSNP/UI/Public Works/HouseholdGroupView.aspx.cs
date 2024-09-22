using System;
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
    public partial class HouseholdGroupView : System.Web.UI.Page
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
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=16"; // Ensure AttachmentType is set correctly
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

                // Save the uploaded file to the server
                string folderPath = Server.MapPath("~/Component_2/HouseholdGridView/");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, Path.GetFileName(fileUpload.FileName));
                fileUpload.SaveAs(filePath);

                // Store the file path for further processing
                Session["UploadedFilePath"] = filePath;

                AddSuccessMessage("File uploaded successfully. You can now analyze it.");
                btnAnalyze.Enabled = true;
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
                string filePath = Session["UploadedFilePath"] as string;
                if (string.IsNullOrEmpty(filePath))
                {
                    AddErrorMessage("No file found for analysis.");
                    return;
                }

                // Analyze the file using EPPlus (or any other Excel library)
                using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(filePath)))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    // Create a DataTable to hold the analyzed data
                    DataTable dt = new DataTable();
                    dt.Columns.Add("GroupName", typeof(string));
                    dt.Columns.Add("Latitude", typeof(decimal));
                    dt.Columns.Add("Longitude", typeof(decimal));

                    // Loop through the worksheet rows and add to DataTable
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["GroupName"] = worksheet.Cells[row, 1].Text;
                        dr["Latitude"] = decimal.Parse(worksheet.Cells[row, 2].Text);
                        dr["Longitude"] = decimal.Parse(worksheet.Cells[row, 3].Text);
                        dt.Rows.Add(dr);
                    }

                    // Bind the data to RadGrid for display
                    RadGrid.DataSource = dt;
                    RadGrid.DataBind();

                    // Store the analyzed data in session for importing
                    Session["AnalyzedData"] = dt;

                    AddSuccessMessage("File analyzed successfully. You can now import it.");
                    btnImport.Enabled = true;
                }
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
                DataTable analyzedData = Session["AnalyzedData"] as DataTable;
                if (analyzedData == null || analyzedData.Rows.Count == 0)
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
                            // Insert records individually with CreatedBy, CreatedDate, ModifiedBy, ModifiedDate
                            foreach (DataRow row in analyzedData.Rows)
                            {
                                bool insertSuccess = InsertHouseholdGroupIntoDB(
                                    conn,
                                    transaction,
                                    row["GroupName"].ToString(),
                                    decimal.Parse(row["Latitude"].ToString()),
                                    decimal.Parse(row["Longitude"].ToString())
                                );

                                if (!insertSuccess)
                                {
                                    transaction.Rollback();
                                    AddErrorMessage("Import failed for Group: " + row["GroupName"]);
                                    return;
                                }
                            }

                            // Commit transaction
                            transaction.Commit();
                            AddSuccessMessage("Data imported successfully.");
                            btnImport.Enabled = false;
                            btnAnalyze.Enabled = false;

                            // You can refresh the grid here if necessary
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


        private bool InsertHouseholdGroupIntoDB(SqlConnection conn, SqlTransaction transaction, string groupName, decimal latitude, decimal longitude)
        {
            string query = @"INSERT INTO tbl_com2_Household_Group_View (GroupName, Latitude, Longitude, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)
                     VALUES (@GroupName, @Latitude, @Longitude, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate)";

            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                // Safely convert Session["user_id"] to integer
                int userId;
                bool isUser = int.TryParse(Session["uid"].ToString(), out userId);
                if (isUser)
                {
                    // Add the parameters to the SQL command
                    cmd.Parameters.AddWithValue("@GroupName", groupName);
                    cmd.Parameters.AddWithValue("@Latitude", latitude);
                    cmd.Parameters.AddWithValue("@Longitude", longitude);
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
