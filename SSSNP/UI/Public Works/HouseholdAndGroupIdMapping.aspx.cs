using System;
using System.Data;
using Telerik.Web.UI;
using SSSNP_SNSOP.Core.Repository;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;
using SSSNP_SNSOP.Core.Model.Component2;
using SSSNP.Service;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class HouseholdAndGroupIdMapping : System.Web.UI.Page
    {

        private LookupManagerService _lookupService;
        private Component2Service _component2Service;
        private Component2Repository _component2Repository;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeServices(); // Initialize services here
                if (!IsPostBack)
                {
                    LoadStateDropdown();
                    LoadGroupIDs();
                    DisableDependentDropdowns(); // Initially disable all dependent dropdowns
                    rgResults.Visible = false; // Initially hide the grid
                }
            }
            catch (Exception ex)
            {
                LogError("Error in Page_Load: " + ex.Message, ex);
                throw;
            }
        }

        private void InitializeServices()
        {
            try
            {
                // Fetch connection string
                string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Connection string 'SNSDPSkils' is not found or empty.");
                }

                // Initialize services
                _lookupService = new LookupManagerService(connectionString);
                _component2Repository = new Component2Repository(connectionString);
                _component2Service = new Component2Service();
            }
            catch (Exception ex)
            {
                LogError("Error initializing services: " + ex.Message, ex);
            }
        }

        // Method to populate Group IDs in the dropdown
        protected void rgResults_ItemCreated(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridHeaderItem)
            {
                GridHeaderItem headerItem = e.Item as GridHeaderItem;

                if (headerItem != null)
                {
                    DropDownList ddlGroupIDOutside = (DropDownList)headerItem.FindControl("ddlGroupIDOutside ");

                    if (ddlGroupIDOutside != null)
                    {
                        LoadGroupIDs(); // Ensure this method loads the correct Group IDs
                    }
                }
            }
        }

        // Modified LoadGroupIDs to accept a dropdown list parameter
        private void LoadGroupIDs()
        {
            try
            {
                // Check if the _lookupService is properly initialized
                //if (_lookupService == null)
                //{
                //    LogError("lookupService is null. Ensure it's initialized correctly.", null);
                //    return; // Exit if _lookupService is null to prevent further errors
                //}

                // Get group data from the database (or your repository)
                DataTable groupData = _lookupService.GetGroupIds(); // Fetching GroupId and GroupName

                if (groupData != null && groupData.Rows.Count > 0)
                {
                    // Clear existing items
                    ddlGroupIDOutside.Items.Clear();

                    // Iterate over the rows and combine GroupId and GroupName for display
                    foreach (DataRow row in groupData.Rows)
                    {
                        string displayText = $"{row["Id"]} - {row["GroupName"]}";
                        string value = row["Id"].ToString(); // Using GroupId as the value

                        // Add item to the dropdown
                        ddlGroupIDOutside.Items.Add(new ListItem(displayText, value));
                    }

                    // Optionally add a default item
                    ddlGroupIDOutside.Items.Insert(0, new ListItem("--Select Group ID--", "0"));
                }
            }
            catch (Exception ex)
            {
                LogError("Error loading Group IDs: " + ex.Message, ex);
            }
        }






        private void LogError(string message, Exception ex)
        {
            // Set the label to display the error message
            lblErrorMessage.Text = message;

            // Optionally add more details from the exception
            if (ex != null)
            {
                lblErrorMessage.Text += " Stack Trace: " + ex.StackTrace;
            }

            // Make sure the label is visible
            lblErrorMessage.Visible = true;


            ScriptManager.RegisterStartupScript(this, GetType(), "hideLabel", "hideLabelAfterDelay('" + lblErrorMessage.ClientID + "', 10000);", true);
        }



        private void LoadStateDropdown()
        {
            try
            {
                DataTable dtStates = _lookupService.GetStateList();
                ddlState.DataSource = dtStates;
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "intStateid";
                ddlState.DataBind();
                ddlState.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                LogError("Error loading State dropdown: " + ex.Message, ex);
            }
        }


        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlState.SelectedValue))
                {
                    int stateId = int.Parse(ddlState.SelectedValue);
                    LoadCountyDropdown(stateId);
                    ddlCounty.Enabled = true;  // Enable County dropdown
                    ddlPayam.Enabled = false;  // Disable Payam dropdown
                    ddlBoma.Enabled = false;   // Disable Boma dropdown
                    ClearDropDowns(ddlPayam, ddlBoma);  // Clear dependent dropdowns
                }
                else
                {
                    DisableDependentDropdowns(); // If no state is selected, disable all dropdowns
                }
            }
            catch (Exception ex)
            {
                LogError("Error in ddlState_SelectedIndexChanged: " + ex.Message, ex);
            }
        }


        private void LoadCountyDropdown(int stateId)
        {
            try
            {
                DataTable dtCounties = _lookupService.GetCountyListByStateId(stateId);
                ddlCounty.DataSource = dtCounties;
                ddlCounty.DataTextField = "CountieName";
                ddlCounty.DataValueField = "intCountieid";
                ddlCounty.DataBind();
                ddlCounty.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                LogError("Error loading County dropdown: " + ex.Message, ex);
            }
        }

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
                {
                    int countyId = int.Parse(ddlCounty.SelectedValue);
                    LoadPayamDropdown(countyId);
                    ddlPayam.Enabled = true;  // Enable Payam dropdown
                    ddlBoma.Enabled = false;  // Disable Boma dropdown
                    ClearDropDowns(ddlBoma);  // Clear dependent Boma dropdown
                }
                else
                {
                    ddlPayam.Enabled = false;
                    ddlBoma.Enabled = false;
                    ClearDropDowns(ddlPayam, ddlBoma);
                }
            }
            catch (Exception ex)
            {
                LogError("Error in ddlCounty_SelectedIndexChanged: " + ex.Message, ex);
            }
        }


        private void LoadPayamDropdown(int countyId)
        {
            try
            {
                DataTable dtPayams = _lookupService.GetPayamListByCountyId(countyId);
                ddlPayam.DataSource = dtPayams;
                ddlPayam.DataTextField = "PayamName";
                ddlPayam.DataValueField = "intPayamid";
                ddlPayam.DataBind();
                ddlPayam.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                LogError("Error loading Payam dropdown: " + ex.Message, ex);
            }
        }

        protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(ddlPayam.SelectedValue))
                {
                    int payamId = int.Parse(ddlPayam.SelectedValue);
                    LoadBomaDropdown(payamId);
                    ddlBoma.Enabled = true;  // Enable Boma dropdown
                }
                else
                {
                    ddlBoma.Enabled = false;
                    ClearDropDowns(ddlBoma);
                }
            }
            catch (Exception ex)
            {
                LogError("Error in ddlPayam_SelectedIndexChanged: " + ex.Message, ex);
            }
        }


        private void LoadBomaDropdown(int payamId)
        {
            try
            {
                DataTable dtBomas = _lookupService.GetBomaListByPayamId(payamId);
                ddlBoma.DataSource = dtBomas;
                ddlBoma.DataTextField = "BomaName";
                ddlBoma.DataValueField = "intBomasid";
                ddlBoma.DataBind();
                ddlBoma.Items.Insert(0, new ListItem("--Select--", ""));
            }
            catch (Exception ex)
            {
                LogError("Error loading Boma dropdown: " + ex.Message, ex);
            }
        }

        private void DisableDependentDropdowns()
        {
            ddlCounty.Enabled = false;
            ddlPayam.Enabled = false;
            ddlBoma.Enabled = false;
            ClearDropDowns(ddlCounty, ddlPayam, ddlBoma);
        }

        private void ClearDropDowns(params DropDownList[] dropdowns)
        {
            foreach (var ddl in dropdowns)
            {
                ddl.Items.Clear();
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            ClearMessages();
            try
            {
                SelectedHouseholds.Clear();
                Session["HouseholdData"] = null;
                // Clear any previous error messages
                lblErrorMessage.Visible = false;
                rgResults.Visible = true;

                // Handle dropdown values safely
                SqlParameter stateId = new SqlParameter("@StateId", string.IsNullOrEmpty(ddlState.SelectedValue) || ddlState.SelectedValue == "--Select--" ? (object)DBNull.Value : int.Parse(ddlState.SelectedValue));
                SqlParameter countyId = new SqlParameter("@CountyId", string.IsNullOrEmpty(ddlCounty.SelectedValue) || ddlCounty.SelectedValue == "--Select--" ? (object)DBNull.Value : int.Parse(ddlCounty.SelectedValue));
                SqlParameter payamId = new SqlParameter("@PayamId", string.IsNullOrEmpty(ddlPayam.SelectedValue) || ddlPayam.SelectedValue == "--Select--" ? (object)DBNull.Value : int.Parse(ddlPayam.SelectedValue));
                SqlParameter bomaId = new SqlParameter("@BomaId", string.IsNullOrEmpty(ddlBoma.SelectedValue) || ddlBoma.SelectedValue == "--Select--" ? (object)DBNull.Value : int.Parse(ddlBoma.SelectedValue));

                // Fetch data from the repository method with parameters
                DataTable householdsTable = _component2Repository.GetHouseholdDataByLocation(stateId, countyId, payamId, bomaId);

                if (householdsTable == null)
                {
                    throw new Exception("DataTable is null.");
                }




                if (householdsTable.Rows.Count > 0)
                {
                    btnAssignGroup.Visible = true;
                    ddlGroupIDOutside.Visible = true;
                    lblGroupID.Visible = true;
                    // Store the data in session to persist between postbacks
                    Session["HouseholdData"] = householdsTable;
                    rgResults.Rebind(); // Trigger re-binding of the grid

                }
                else
                {
                    // Handle empty data case and rebind
                    Session["HouseholdData"] = new DataTable();
                    rgResults.Rebind();
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "No records found.";
                    btnAssignGroup.Visible = false;
                    ddlGroupIDOutside.Visible = false;
                    lblGroupID.Visible = false;
                }
                HideLabelsAfterDelay();
            }
            catch (FormatException fex)
            {
                // Log and display format errors
                LogError("FormatException caught: " + fex.Message, fex);
                lblErrorMessage.Text = "An error occurred while processing dropdown values. Please select valid options.";
                lblErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                // Log any other errors that occur and display an error message
                LogError("Error while searching data: " + ex.Message, ex);
                lblErrorMessage.Text = "An error occurred while loading data.";
                lblErrorMessage.Visible = true;
            }
            HideLabelsAfterDelay();
        }


        protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                // Get the DataTable from session
                DataTable householdsTable = Session["HouseholdData"] as DataTable;

                if (householdsTable != null)
                {
                    rgResults.DataSource = householdsTable;
                }
                else
                {
                    // Bind an empty DataTable if no data
                    rgResults.DataSource = new DataTable();
                }
            }
            catch (Exception ex)
            {
                LogError("Error in NeedDataSource: " + ex.Message, ex);
            }
        }


        protected void rgResults_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "AssignGroup")
            {
                // Find the current row where the command was triggered
                GridDataItem dataItem = (GridDataItem)e.Item;

                // Get the household number for this row
                string householdNumber = dataItem.GetDataKeyValue("household_number").ToString();

                // Find the DropDownList in this row and get the selected GroupId
                DropDownList ddlGroupID = (DropDownList)dataItem.FindControl("ddlGroupID");
                if (ddlGroupID != null)
                {
                    int groupId;
                    if (int.TryParse(ddlGroupID.SelectedValue, out groupId))
                    {
                        // Now you can use the groupId and householdNumber to update the group linkage
                        _component2Repository.InsertHouseholdGroupLinkage(householdNumber, groupId);

                        // Optionally, provide feedback to the user (e.g., show a success message)
                        lblSuccessMessage.Visible = true;
                        lblSuccessMessage.Text = "Group ID successfully assigned!";
                    }
                    else
                    {
                        // Handle invalid GroupId
                        lblErrorMessage.Visible = true;
                        lblErrorMessage.Text = "Please select a valid Group ID.";
                    }

                }
            }
        }



        protected void btnAssignGroup_Click(object sender, EventArgs e)
        {
            ClearMessages();
            try
            {
                // Check if the dropdown outside the grid is available and selected
                if (ddlGroupIDOutside == null || ddlGroupIDOutside.SelectedValue == "0")
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Please select a valid Group ID.";
                    return;
                }

                int selectedGroupId = int.Parse(ddlGroupIDOutside.SelectedValue);

                // Create a list to store selected household numbers
                List<string> selectedHouseholdNumbers = new List<string>();

                // Loop through the grid items to find the selected households
                foreach (GridDataItem item in rgResults.MasterTableView.Items)
                {
                    CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        string householdNumber = item.GetDataKeyValue("household_number").ToString();
                        selectedHouseholdNumbers.Add(householdNumber);
                    }
                }

                // Check if any households were selected
                if (selectedHouseholdNumbers.Count == 0)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "No households were selected.";
                    return;
                }

                // Ensure Session["UserId"] is available
                if (Session["uid"] == null)
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "User session has expired. Please log in again.";
                    return;
                }

                // Create an instance of the HouseholdGroupLinkageModel
                var householdGroupLinkageModel = new HouseholdGroupLinkageModel
                {
                    HouseholdNumbers = selectedHouseholdNumbers,
                    GroupId = selectedGroupId,
                    CreatedBy = HttpContext.Current.Session["uid"] != null ? Convert.ToInt32(HttpContext.Current.Session["uid"]) : 0, // Set the creator based on session or any relevant user ID
                    CreatedDate = DateTime.Now, // Current date and time for record creation
                    ModifiedBy = HttpContext.Current.Session["uid"] != null ? Convert.ToInt32(HttpContext.Current.Session["uid"]) : 0,
                    ModifiedDate = DateTime.Now
                };

                // Call the service method to save the household group linkage
                var response = _component2Service.SaveHouseholdGroupLinkage(householdGroupLinkageModel);

                // Display a success or error message based on the response
                if (response.IsSuccess)
                {
                    lblSuccessMessage.Visible = true;
                    lblSuccessMessage.Text = "Group ID assigned successfully to selected households.";

                    // Uncheck the checkboxes and clear selected households
                    foreach (GridDataItem item in rgResults.MasterTableView.Items)
                    {
                        CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                        if (chkSelect != null)
                        {
                            chkSelect.Checked = false; // Uncheck the checkbox
                        }
                    }
                    SelectedHouseholds.Clear();
                    rgResults.Rebind();
                }
                else
                {
                    lblErrorMessage.Visible = true;
                    lblErrorMessage.Text = "Error: " + response.Message;
                }
            }
            catch (Exception ex)
            {
                lblErrorMessage.Visible = true;
                lblErrorMessage.Text = "An error occurred while assigning group ID: " + ex.Message;
            }

            // Set the visibility of the labels for a limited time
            lblSuccessMessage.Visible = true;
            lblErrorMessage.Visible = true;
            HideLabelsAfterDelay();  // Call a function to hide the labels after a delay
        }

        // Function to hide success and error labels after a delay
        private void HideLabelsAfterDelay()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "hideLabels",
                "setTimeout(function() { " +
                "document.getElementById('" + lblSuccessMessage.ClientID + "').style.display='none';" +
                "document.getElementById('" + lblErrorMessage.ClientID + "').style.display='none';" +
                "}, 10000);", true);
        }



        private List<string> SelectedHouseholds
        {
            get
            {
                if (Session["SelectedHouseholds"] == null)
                {
                    Session["SelectedHouseholds"] = new List<string>();
                }
                return (List<string>)Session["SelectedHouseholds"];
            }
            set
            {
                Session["SelectedHouseholds"] = value.Distinct().ToList();
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = (CheckBox)sender;
            GridDataItem item = (GridDataItem)chkSelect.NamingContainer;
            string householdNumber = item.GetDataKeyValue("household_number").ToString();

            if (chkSelect.Checked)
            {
                // Add to selected list if checked
                if (!SelectedHouseholds.Contains(householdNumber))
                {
                    SelectedHouseholds.Add(householdNumber);
                }
            }
            else
            {
                // Remove from selected list if unchecked
                if (SelectedHouseholds.Contains(householdNumber))
                {
                    SelectedHouseholds.Remove(householdNumber);
                }
            }
        }

        protected void rgResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                string householdNumber = item.GetDataKeyValue("household_number").ToString();

                chkSelect.Checked = false;
                // If the household number is in the selected list, check the checkbox
                if (SelectedHouseholds.Contains(householdNumber))
                {
                    chkSelect.Checked = true;
                }

            }
        }

        protected void rgResults_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            rgResults.Rebind(); // This forces a rebind of the data when the page index is changed
        }

        protected void rgResults_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            rgResults.Rebind(); // This forces a rebind of the data when the page size is changed
        }

        private void ClearMessages()
        {
            // Clear and hide both success and error messages
            lblSuccessMessage.Text = string.Empty;
            lblSuccessMessage.Visible = false;
            lblErrorMessage.Text = string.Empty;
            lblErrorMessage.Visible = false;
        }


    }
}