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
    public partial class HouseholdGroupView : Page
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
            lnkDownloadTemplate.Visible = false;
        }

        protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        {
            // Redirect to the ViewAttachmentHandler to download the Excel file
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=16";
            Response.Redirect(redirectUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ClearMessages();
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
                        "Group Name", "Latitude", "Longitude", "VSLA Formed"
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


                    List<HouseholdGroupModel> householdGroups = new List<HouseholdGroupModel>();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string groupName = worksheet.Cells[row, 1].Text;
                        decimal? latitude = null;
                        decimal? longitude = null;
                        bool? VSLAformed = ParseBoolean(worksheet.Cells[row, 4].Text);
                        
                        if (decimal.TryParse(worksheet.Cells[row, 2].Text, out decimal lat))
                        {
                            latitude = lat;
                        }
                        if (decimal.TryParse(worksheet.Cells[row, 3].Text, out decimal lon))
                        {
                            longitude = lon;
                        }

                        householdGroups.Add(new HouseholdGroupModel
                        {
                            GroupName = groupName,
                            Latitude = latitude,
                            Longitude = longitude,
                            VSLA_formed = VSLAformed,
                            CreatedBy = userId,
                            ModifiedBy = userId
                        });
                    }

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

        private bool? ParseBoolean(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;

            value = value.Trim().ToLower();

            if (value == "yes")
                return true;
            else if (value == "no")
                return false;
            else
                return null; // or throw an exception if you prefer
        }


        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
            Session["IsAnalyzing"] = true;
            try
            {
                Component2Service service = new Component2Service();

                // Retrieve the list of household groups from session
                List<HouseholdGroupModel> householdGroups = Session["HouseholdGroups"] as List<HouseholdGroupModel>;
                if (householdGroups == null || householdGroups.Count == 0)
                {
                    AddErrorMessage("No data available for analysis.");
                    return;
                }

                // Fetch existing GroupNames from the service to check for redundancy
                Dictionary<string, int> existingGroupNames = service.GetExistingGroupNames();

                HashSet<string> uniqueGroupNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                var validGroup = new List<HouseholdGroupModel> ();

                // Create a DataTable to hold the data for display in the grid
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));  // GroupId from database (if redundant)
                dt.Columns.Add("GroupName", typeof(string));
                dt.Columns.Add("Latitude", typeof(decimal));
                dt.Columns.Add("Longitude", typeof(decimal));
                dt.Columns.Add("VSLA_formed", typeof(bool));
                dt.Columns.Add("ValidationStatus", typeof(string));

                int skipCount = 0;
                // Loop through the list and dynamically check for redundancy
                foreach (var group in householdGroups)
                {
                    DataRow dr = dt.NewRow();
                    dr["GroupName"] = group.GroupName ?? (object)DBNull.Value;
                    dr["Latitude"] = group.Latitude ?? (object)DBNull.Value; // Handle null values
                    dr["Longitude"] = group.Longitude ?? (object)DBNull.Value; // Handle null values
                    dr["VSLA_formed"] = group.VSLA_formed ?? (object)DBNull.Value;

                    // Check if the group is redundant
                    if (!string.IsNullOrEmpty(group.GroupName) && existingGroupNames.ContainsKey(group.GroupName))
                    {
                        dr["Id"] = existingGroupNames[group.GroupName]; // Set the GroupId for redundant groups
                    }
                    else
                    {
                        dr["Id"] = DBNull.Value; // Not redundant, no GroupId
                    }

                    var validationResult = service.IsValidData(group);


                    if (!validationResult.isValid)
                    {
                        dr["ValidationStatus"] = validationResult.errorMessage; // Store the error message
                    }
                    else
                    {
                        dr["ValidationStatus"] = validationResult.errorMessage; // No error
                    }

                    dt.Rows.Add(dr);

                    if (uniqueGroupNames.Contains(group.GroupName))
                    {
                        // Skip this group because it's a duplicate in the uploaded Excel file
                        skipCount++;
                        continue;
                    }
                    else
                    {
                        // Add the groupName to the HashSet to track it as already added
                        uniqueGroupNames.Add(group.GroupName);
                        validGroup.Add(group);
                    }

                   // dt.Rows.Add(dr);
                }

                // Bind the DataTable to the RadGrid for display
                Session["AnalyzedData"] = dt;
                Session["ValidGroup"] = validGroup;
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
                List<HouseholdGroupModel> householdGroups = Session["ValidGroup"] as List<HouseholdGroupModel>;

                if (householdGroups == null || householdGroups.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                int skipCount = Session["skipCount"] != null ? (int)Session["skipCount"] : 0;

                // Pass the entire list to the service for processing
                var response = service.SaveHouseholdGroups(householdGroups);
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
                int totalRecordCount = GetHouseholdGroupCount();
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

        private int GetHouseholdGroupCount()
        {
            int totalRecords = 0;
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open(); // Ensure the connection is opened

                    string query = @"SELECT COUNT(*) FROM tbl_com2_Household_Group_View WHERE 1=1;";

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
        private DataTable GetHouseholGroupData(int pageIndex, int pageSize)
        {
            string query = @"SELECT Id, GroupName, Latitude, Longitude, VSLA_formed
                     FROM tbl_com2_Household_Group_View 
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
                    dt = GetHouseholGroupData(RadGrid.CurrentPageIndex, RadGrid.PageSize);
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
                // Check if analysis is done
                if (isAnalyzed)
                {
                    // Validate the ValidationStatus column
                    string validationStatus = dataItem["ValidationStatus"].Text;

                    if (validationStatus != "Valid")
                    {
                        dataItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFB1B4");
                    }
                }

                // Convert VSLA_formed column value from true/false to Yes/No
                TableCell vslaFormedCell = dataItem["VSLA_formed"];
                if (vslaFormedCell != null)
                {
                    string cellValue = vslaFormedCell.Text.Trim();

                    // Convert true/false to Yes/No
                    if (cellValue.Equals("true", StringComparison.OrdinalIgnoreCase))
                    {
                        vslaFormedCell.Text = "Yes";
                    }
                    else if (cellValue.Equals("false", StringComparison.OrdinalIgnoreCase))
                    {
                        vslaFormedCell.Text = "No";
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
