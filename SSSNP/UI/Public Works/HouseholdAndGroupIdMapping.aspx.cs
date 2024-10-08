using System;
using System.Data;
using Telerik.Web.UI;
using SSSNP_SNSOP.Core.Repository;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Diagnostics;
using System.Data.SqlClient;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class HouseholdAndGroupIdMapping : System.Web.UI.Page
    {
        private LookupManagerService _lookupService;
        private Component2Repository _component2Repository;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeServices(); // Initialize services here
                if (!IsPostBack)
                {
                    LoadStateDropdown();
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
            }
            catch (Exception ex)
            {
                LogError("Error initializing services: " + ex.Message, ex);
            }
        }

        private void LogError(string message, Exception ex)
        {
            // Implement your logging mechanism here (e.g., writing to a log file or database)
            Console.WriteLine(message);
            if (ex != null)
            {
                Console.WriteLine("Stack Trace: " + ex.StackTrace);
            }
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
            try
            {
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
                    // Store the data in session to persist between postbacks
                    Session["HouseholdData"] = householdsTable;
                    rgResults.Rebind(); // Trigger re-binding of the grid
                    
                }
                else
                {
                    // Handle empty data case and rebind
                    Session["HouseholdData"] = new DataTable();
                    rgResults.Rebind();
                    
                }
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

        protected void btnAssignGroup_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetch the selected Group ID from the dropdown in the header
                DropDownList ddlGroupID = rgResults.MasterTableView.GetItems(GridItemType.Header)[0].FindControl("ddlGroupID") as DropDownList;
                if (ddlGroupID == null || ddlGroupID.SelectedValue == "0")
                {
                    lblErrorMessage.Text = "Please select a valid Group ID.";
                    lblErrorMessage.Visible = true;
                    return;
                }

                int selectedGroupId = int.Parse(ddlGroupID.SelectedValue);

                // Iterate through the grid to find selected rows (households)
                foreach (GridDataItem item in rgResults.MasterTableView.Items)
                {
                    CheckBox chkSelect = item.FindControl("chkSelect") as CheckBox;
                    if (chkSelect != null && chkSelect.Checked)
                    {
                        // Get the household number (or any key field) for the selected row
                        string householdNumber = item.GetDataKeyValue("household_number").ToString();

                        // Assign the selected group ID to the household (you can add your database update logic here)
                        UpdateHouseholdGroup(householdNumber, selectedGroupId);
                    }
                }

                // Provide feedback after the update
                lblErrorMessage.Text = "Selected households have been assigned the group ID.";
                lblErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "An error occurred while assigning group ID.";
                lblErrorMessage.Visible = true;
                LogError("Error in btnAssignGroup_Click: " + ex.Message, ex);
            }
        }

        // Method to update household group (you can modify this to update your database)
        private void UpdateHouseholdGroup(string householdNumber, int groupId)
        {
            // Example: You could perform an update to the database here
            // using a repository or directly through SQL queries
            _component2Repository.UpdateHouseholdGroup(householdNumber, groupId);
        }



    }
}