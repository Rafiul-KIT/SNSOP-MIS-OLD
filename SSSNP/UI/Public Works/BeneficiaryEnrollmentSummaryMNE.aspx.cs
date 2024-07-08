using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class BeneficiaryEnrollmentSummaryMNE : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadInitialData();
        }
    }

    private void LoadInitialData()
    {
        // Load initial data for dropdown lists (states, counties, payams, etc.)
        // For example:
        // ddlState.DataSource = GetStates();
        // ddlState.DataBind();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Load counties based on selected state
    }

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Load payams based on selected county
    }

    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Load bomas based on selected payam
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        LoadBeneficiaryData();
    }

    private void LoadBeneficiaryData()
    {
        // Fetch beneficiary data based on selected filters
        DataTable dt = GetBeneficiaryData();
        rgResults.DataSource = dt;
        rgResults.DataBind();
        rgResults.Visible = true;
    }

    private DataTable GetBeneficiaryData()
    {
        // Implement your data fetching logic here
        DataTable dt = new DataTable();
        dt.Columns.Add("intBeneficiaryId");
        dt.Columns.Add("geoLocation");
        dt.Columns.Add("age");
        dt.Columns.Add("gender");
        dt.Columns.Add("component");
        dt.Columns.Add("enrollmentDate");
        dt.Columns.Add("status");

        // Add dummy data for example purposes
        dt.Rows.Add("1", "Location1", "25", "Male", "LIPW", DateTime.Now.ToString(), "Active");
        dt.Rows.Add("2", "Location2", "30", "Female", "DIS", DateTime.Now.ToString(), "Inactive");

        return dt;
    }

    protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        rgResults.DataSource = GetBeneficiaryData();
    }

    protected void rgResults_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Handle any additional item data binding logic here
    }
}
