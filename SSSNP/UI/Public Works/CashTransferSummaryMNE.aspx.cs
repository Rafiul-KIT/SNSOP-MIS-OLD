using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class CashTransferSummaryMNE : Page
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
        LoadPaymentData();
    }

    private void LoadPaymentData()
    {
        // Fetch payment data based on selected filters
        DataTable dt = GetPaymentData();
        /*rgResults.DataSource = dt;
        rgResults.DataBind();
        rgResults.Visible = true;*/
    }

    private DataTable GetPaymentData()
    {
        // Implement your data fetching logic here
        DataTable dt = new DataTable();
        dt.Columns.Add("intPaymentId");
        dt.Columns.Add("geoLocation");
        dt.Columns.Add("currency");
        dt.Columns.Add("paymentCycle");
        dt.Columns.Add("paymentStatus");
        dt.Columns.Add("component");
        dt.Columns.Add("paymentPartner");
        dt.Columns.Add("paymentAmount");
        dt.Columns.Add("paymentDate");

        // Add dummy data for example purposes
        dt.Rows.Add("1", "Location1", "SSP", "Monthly", "Paid", "LIPW", "Partner1", "1000", DateTime.Now.ToString());
        dt.Rows.Add("2", "Location2", "USD", "Quarterly", "Not Paid", "DIS", "Partner2", "2000", DateTime.Now.ToString());

        return dt;
    }

    protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        /*rgResults.DataSource = GetPaymentData();*/
    }

    protected void rgResults_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Handle any additional item data binding logic here
    }
}
