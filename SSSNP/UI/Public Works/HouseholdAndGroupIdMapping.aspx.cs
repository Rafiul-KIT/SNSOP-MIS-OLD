using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP.UI.Public_Works
{
    public partial class HouseholdAndGroupIdMapping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Populate dropdowns on initial load
                LoadStateDropdown();
                LoadCountyDropdown();
                LoadPayamDropdown();
                LoadBomaDropdown();
            }
        }

        // Sample method for populating State dropdown
        private void LoadStateDropdown()
        {
            // Add code to fetch data from the database
            ddlState.Items.Clear();
            ddlState.Items.Add(new ListItem("--Select--", ""));
            // Example: ddlState.Items.Add(new ListItem("State1", "1"));
            // Add actual data from your data source here
        }

        private void LoadCountyDropdown()
        {
            ddlCounty.Items.Clear();
            ddlCounty.Items.Add(new ListItem("--Select--", ""));
            // Add actual data from your data source here
        }

        private void LoadPayamDropdown()
        {
            ddlPayam.Items.Clear();
            ddlPayam.Items.Add(new ListItem("--Select--", ""));
            // Add actual data from your data source here
        }

        private void LoadBomaDropdown()
        {
            ddlBoma.Items.Clear();
            ddlBoma.Items.Add(new ListItem("--Select--", ""));
            // Add actual data from your data source here
        }

        // Event when State dropdown changes
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle state selection change
            // Optionally load dependent data like County
            LoadCountyDropdown();
        }

        // Event when County dropdown changes
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle county selection change
            LoadPayamDropdown();
        }

        // Event when Payam dropdown changes
        protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle Payam selection change
            LoadBomaDropdown();
        }

        // Search button click event
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            // Trigger grid data binding when search is clicked
            rgResults.Rebind();
        }

        // Telerik RadGrid DataSource event handler
        protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            // Fetch data from database based on selected state, county, etc.
            DataTable dt = GetHouseholdGroupData();
            rgResults.DataSource = dt;
        }

        // Method to fetch data based on selected dropdowns
        private DataTable GetHouseholdGroupData()
        {
            DataTable dt = new DataTable();

            // Create sample columns (replace with actual data)
            dt.Columns.Add("HouseholdNumber");
            dt.Columns.Add("GroupID");

            // Add sample data (replace with actual query result)
            dt.Rows.Add("Household 1", "Group 1");
            dt.Rows.Add("Household 2", "Group 2");

            return dt;
        }

        // Telerik RadGrid ItemDataBound event handler for customizing row data display
        protected void rgResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                // Example: Customize the row appearance or modify data
                GridDataItem item = e.Item as GridDataItem;
                string householdNumber = item["HouseholdNumber"].Text;
                string groupId = item["GroupID"].Text;
                // Add custom logic if needed
            }
        }
    }
}
