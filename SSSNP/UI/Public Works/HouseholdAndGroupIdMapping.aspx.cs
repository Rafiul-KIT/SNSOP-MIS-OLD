using System;
using System.Data;
using SSSNP_SNSOP.Core.Model.Component2; // Model namespace
using SSSNP_SNSOP.Core.DbHandler; // Repository namespace
using Telerik.Web.UI;
using SSSNP_SNSOP.Core.Repository;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class HouseholdAndGroupIdMapping : System.Web.UI.Page
    {
        private readonly LookupManagerService _lookupService;
        private readonly Component2Repository _component2Repository;

        public HouseholdAndGroupIdMapping()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            _lookupService = new LookupManagerService(connectionString);
            _component2Repository = new Component2Repository(connectionString);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStateDropdown();
                DisableDependentDropdowns(); // Initially disable all dependent dropdowns
            }
        }

        private void LoadStateDropdown()
        {
            DataTable dtStates = _lookupService.GetStateList();
            ddlState.DataSource = dtStates;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "intStateid";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
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

        private void LoadCountyDropdown(int stateId)
        {
            DataTable dtCounties = _lookupService.GetCountyListByStateId(stateId);
            ddlCounty.DataSource = dtCounties;
            ddlCounty.DataTextField = "CountieName";
            ddlCounty.DataValueField = "intCountieid";
            ddlCounty.DataBind();
            ddlCounty.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
        }

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
            {
                int countyId = int.Parse(ddlCounty.SelectedValue);
                LoadPayamDropdown(countyId);
                ddlPayam.Enabled = true; // Enable Payam dropdown
                ddlBoma.Enabled = false; // Disable Boma dropdown
                ClearDropDowns(ddlBoma); // Clear dependent Boma dropdown
            }
            else
            {
                ddlPayam.Enabled = false;
                ddlBoma.Enabled = false;
                ClearDropDowns(ddlPayam, ddlBoma);
            }
        }

        private void LoadPayamDropdown(int countyId)
        {
            DataTable dtPayams = _lookupService.GetPayamListByCountyId(countyId);
            ddlPayam.DataSource = dtPayams;
            ddlPayam.DataTextField = "PayamName";
            ddlPayam.DataValueField = "intPayamid";
            ddlPayam.DataBind();
            ddlPayam.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
        }

        protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ddlPayam.SelectedValue))
            {
                int payamId = int.Parse(ddlPayam.SelectedValue);
                LoadBomaDropdown(payamId);
                ddlBoma.Enabled = true; // Enable Boma dropdown
            }
            else
            {
                ddlBoma.Enabled = false;
                ClearDropDowns(ddlBoma);
            }
        }

        private void LoadBomaDropdown(int payamId)
        {
            DataTable dtBomas = _lookupService.GetBomaListByPayamId(payamId);
            ddlBoma.DataSource = dtBomas;
            ddlBoma.DataTextField = "BomaName";
            ddlBoma.DataValueField = "intBomasid";
            ddlBoma.DataBind();
            ddlBoma.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
        }

        private void DisableDependentDropdowns()
        {
            ddlCounty.Enabled = false;
            ddlPayam.Enabled = false;
            ddlBoma.Enabled = false;
            ClearDropDowns(ddlCounty, ddlPayam, ddlBoma);
        }

        private void ClearDropDowns(params System.Web.UI.WebControls.DropDownList[] dropdowns)
        {
            foreach (var ddl in dropdowns)
            {
                ddl.Items.Clear();
                ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", ""));
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            LoadHouseholdGroupData();
        }

        private bool IsValidInput()
        {
            return ddlState.SelectedIndex > 0 && ddlCounty.SelectedIndex > 0 && ddlPayam.SelectedIndex > 0 && ddlBoma.SelectedIndex > 0;
        }

        private void LoadHouseholdGroupData()
        {
            try
            {
                // Fetch search criteria
                int stateId = string.IsNullOrEmpty(ddlState.SelectedValue) ? 0 : int.Parse(ddlState.SelectedValue);
                int countyId = string.IsNullOrEmpty(ddlCounty.SelectedValue) ? 0 : int.Parse(ddlCounty.SelectedValue);
                int payamId = string.IsNullOrEmpty(ddlPayam.SelectedValue) ? 0 : int.Parse(ddlPayam.SelectedValue);
                int bomaId = string.IsNullOrEmpty(ddlBoma.SelectedValue) ? 0 : int.Parse(ddlBoma.SelectedValue);

                // Call your repository method to get the data
                var households = _component2Repository.GetHouseholdDataByLocation(stateId, countyId, payamId, bomaId);

                if (households != null && households.Count > 0)
                {
                    rgResults.DataSource = households;
                }
                else
                {
                    rgResults.DataSource = null;
                }
                rgResults.Rebind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                // Handle the error
            }
        }

        protected void SelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelectAll = (CheckBox)sender;
            foreach (GridDataItem item in rgResults.MasterTableView.Items)
            {
                CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                if (chkSelect != null)
                {
                    chkSelect.Checked = chkSelectAll.Checked;  // Select or deselect all checkboxes
                }
            }
        }

        // Grid data binding on demand
        protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //LoadHouseholdGroupData();
        }

        // Grid item data bound (custom logic if needed)
        protected void rgResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
            // Handle item data bound logic if needed
        }
    }
}
