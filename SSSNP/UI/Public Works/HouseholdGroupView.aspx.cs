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
    public partial class HouseholdGroupView : Page
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

                using (var package = new OfficeOpenXml.ExcelPackage(fileUpload.FileContent))
                {
                    int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    List<HouseholdGroupModel> householdGroups = new List<HouseholdGroupModel>();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string groupName = worksheet.Cells[row, 1].Text;
                        decimal? latitude = null;
                        decimal? longitude = null;

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
                            CreatedBy = userId,
                            ModifiedBy = userId
                        }) ;
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

        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
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

        protected void btnImport_Click(object sender, EventArgs e)
        {
            ClearMessages();
            try
            {
                Component2Service service = new Component2Service();
                List<HouseholdGroupModel> householdGroups = Session["HouseholdGroups"] as List<HouseholdGroupModel>;

                if (householdGroups == null || householdGroups.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                // Pass the entire list to the service for processing
                var response = service.SaveHouseholdGroups(householdGroups);
                if (!response.IsSuccess)
                {
                    AddErrorMessage(response.Message);
                }
                else
                {
                    AddSuccessMessage(response.Message);
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data import failed: " + ex.Message);
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
