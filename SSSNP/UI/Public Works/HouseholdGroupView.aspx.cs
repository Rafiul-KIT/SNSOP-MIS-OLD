using SSSNP.Component_2.HouseholdGroupView;
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

                // Read the uploaded file directly from the stream
                using (var package = new OfficeOpenXml.ExcelPackage(fileUpload.FileContent))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    // Create a list to hold the data
                    List<HouseholdGroup> householdGroups = new List<HouseholdGroup>();

                    // Loop through the rows of the worksheet and populate the list
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string groupName = worksheet.Cells[row, 1].Text;
                        string latitudeText = worksheet.Cells[row, 2].Text;
                        string longitudeText = worksheet.Cells[row, 3].Text;

                        // Try to parse Latitude and Longitude, and handle invalid data
                        decimal? latitude = null;
                        decimal? longitude = null;

                        if (!string.IsNullOrEmpty(latitudeText) && decimal.TryParse(latitudeText, out decimal parsedLatitude))
                        {
                            latitude = parsedLatitude;
                        }

                        if (!string.IsNullOrEmpty(longitudeText) && decimal.TryParse(longitudeText, out decimal parsedLongitude))
                        {
                            longitude = parsedLongitude;
                        }

                        // Add to the list
                        householdGroups.Add(new HouseholdGroup
                        {
                            GroupName = groupName,
                            Latitude = latitude,
                            Longitude = longitude
                        });
                    }

                    // Store the list in session for further analysis or display in grid
                    Session["HouseholdGroups"] = householdGroups;

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
            try
            {
                // Retrieve the list of household groups from session
                List<HouseholdGroup> householdGroups = Session["HouseholdGroups"] as List<HouseholdGroup>;
                if (householdGroups == null || householdGroups.Count == 0)
                {
                    AddErrorMessage("No data available for analysis.");
                    return;
                }

                // Fetch existing GroupNames to check for redundancy
                Dictionary<string, int> existingGroupNames = GetExistingGroupNames(); // Contains GroupName -> GroupId

                // Create a DataTable to hold the data for display in the grid
                DataTable dt = new DataTable();
                dt.Columns.Add("GroupId", typeof(int));  // GroupId from database (if redundant)
                dt.Columns.Add("GroupName", typeof(string));
                dt.Columns.Add("Latitude", typeof(decimal));
                dt.Columns.Add("Longitude", typeof(decimal));

                // Loop through the list and dynamically check for redundancy
                foreach (var group in householdGroups)
                {
                    DataRow dr = dt.NewRow();
                    dr["GroupName"] = group.GroupName;
                    dr["Latitude"] = group.Latitude ?? (object)DBNull.Value; // Handle null values
                    dr["Longitude"] = group.Longitude ?? (object)DBNull.Value; // Handle null values

                    // Check if the group is redundant
                    if (!string.IsNullOrEmpty(group.GroupName) && existingGroupNames.ContainsKey(group.GroupName))
                    {
                        dr["GroupId"] = existingGroupNames[group.GroupName]; // Set the GroupId for redundant groups
                    }
                    else
                    {
                        dr["GroupId"] = DBNull.Value; // Not redundant, no GroupId
                    }

                    dt.Rows.Add(dr);
                }

                // Bind the DataTable to the RadGrid for display
                RadGrid.DataSource = dt;
                RadGrid.DataBind();

                AddSuccessMessage("Data analyzed successfully.");
                btnImport.Enabled = true;
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data analysis failed: " + ex.Message);
            }
        }


        private Dictionary<string, int> GetExistingGroupNames()
        {
            Dictionary<string, int> groupNames = new Dictionary<string, int>();

            string query = "SELECT Id, GroupName FROM tbl_com2_Household_Group_View";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string groupName = reader["GroupName"].ToString();
                        int Id = (int)reader["Id"];
                        groupNames.Add(groupName, Id);
                    }
                }
            }

            return groupNames;
        }



        protected void btnImport_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
            try
            {
                // Retrieve the list from session
                List<HouseholdGroup> householdGroups = Session["HouseholdGroups"] as List<HouseholdGroup>;
                if (householdGroups == null || householdGroups.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                // Fetch existing GroupNames to check for redundancy
                Dictionary<string, int> existingGroupNames = GetExistingGroupNames(); // Contains GroupName -> GroupId

                // Start database operation with transaction
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int insertedRowCount = 0;
                            foreach (HouseholdGroup group in householdGroups)
                            {
                                // Skip rows with null values or redundant groups
                                if (!string.IsNullOrEmpty(group.GroupName) &&
                                    group.Latitude.HasValue &&
                                    group.Longitude.HasValue &&
                                    !existingGroupNames.ContainsKey(group.GroupName))
                                {
                                    bool insertSuccess = InsertHouseholdGroupIntoDB(
                                        conn,
                                        transaction,
                                        group.GroupName,
                                        group.Latitude.Value,
                                        group.Longitude.Value
                                    );

                                    if (!insertSuccess)
                                    {
                                        transaction.Rollback();
                                        AddErrorMessage($"Import failed for Group: {group.GroupName}. Rolling back changes.");
                                        return;
                                    }

                                    insertedRowCount++;
                                }
                            }

                            // Commit transaction
                            transaction.Commit();
                            AddSuccessMessage($"{insertedRowCount} rows successfully imported. Redundant rows were not inserted.");
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
       
        protected void RadGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) // Check if the item is a data row
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                // Get the value of the "GroupId" field directly
                string groupIdText = dataItem["GroupId"].Text;

                // If the groupId is not empty or null, mark the row as redundant
                if (!string.IsNullOrEmpty(groupIdText) && groupIdText != "&nbsp;") // Check if groupId is valid
                {
                    dataItem.CssClass = "redundant-row"; // Apply the CSS class for redundant rows
                }
            }
        }



        private void AddSuccessMessage(string message)
        {
            DivSuccess.Visible = true;
            lblMessageSuccess.Text = message;

            // Hide the success message after 5 seconds
           /* ScriptManager.RegisterStartupScript(this, GetType(), "HideSuccess",
                "setTimeout(function() { document.getElementById('" + DivSuccess.ClientID + "').style.display = 'none'; }, 5000);", true);*/
        }

        private void AddErrorMessage(string message)
        {
            DivFailure.Visible = true;
            lblMessageError.Text = message;

            // Hide the error message after 5 seconds
           /* ScriptManager.RegisterStartupScript(this, GetType(), "HideError",
                "setTimeout(function() { document.getElementById('" + DivFailure.ClientID + "').style.display = 'none'; }, 5000);", true);*/
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
