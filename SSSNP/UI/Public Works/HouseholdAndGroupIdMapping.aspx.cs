using System;
using System.Data;
using SSSNP_SNSOP.Core.Model.Component2; // Model namespace
using SSSNP_SNSOP.Core.DbHandler; // Repository namespace
using Telerik.Web.UI;
using SSSNP_SNSOP.Core.Repository;
using System.Collections.Generic;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class HouseholdAndGroupIdMapping : System.Web.UI.Page
    {
        // Assuming LookupManagerService is your geo-location lookup service
        private readonly LookupManagerService _lookupService;
        private readonly Component2Repository _component2Repository;

        public HouseholdAndGroupIdMapping()
        {
            // Initialize services with connection string (modify with actual connection string source)
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            _lookupService = new LookupManagerService(connectionString);
            _component2Repository = new Component2Repository(connectionString); // Adjust with your actual repository initialization
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStateDropdown();
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            if (IsValidInput())
            {
                LoadHouseholdGroupData();
            }
        }

        private bool IsValidInput()
        {
            // Add validation if necessary
            return ddlState.SelectedIndex > 0 && ddlCounty.SelectedIndex > 0 && ddlPayam.SelectedIndex > 0 && ddlBoma.SelectedIndex > 0;
        }

        private void LoadHouseholdGroupData()
        {
            // Create the model for HouseholdGroupLinkage
            HouseholdGroupLinkageModel linkageModel = new HouseholdGroupLinkageModel
            {
                HouseholdNumber = "", // Modify as per your requirement
                GroupId = int.Parse(ddlBoma.SelectedValue), // Assuming GroupId is BomaId here
                HouseholdNumbers = new List<string> { /* Load household numbers from request or UI */ }
            };

            // Fetch data using repository method (adjust with actual method)
            var response = _component2Repository.SaveHouseholdGroupLinkage(linkageModel);

            if (response.IsSuccess)
            {
                // Bind data to grid
                rgResults.DataSource = response; // Set the correct data source from response
                rgResults.DataBind();
            }
            else
            {
                // Handle error or show message
                // Example: Show error message in a label
                // lblMessage.Text = response.Message;
            }
        }

        // Grid data binding on demand
        protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            LoadHouseholdGroupData();
        }

        // Grid item data bound (custom logic if needed)
        protected void rgResults_ItemDataBound(object sender, GridItemEventArgs e)
        {
            // Handle item data bound logic if needed
        }
    }
}
