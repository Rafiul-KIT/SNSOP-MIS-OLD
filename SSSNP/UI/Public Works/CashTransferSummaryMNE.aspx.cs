using System;
using System.Data;
using System.Diagnostics;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web;
using System.Web.SessionState;
using System.Web.UI.WebControls.WebParts;



public partial class CashTransferSummaryMNE : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                LoadInitialData();
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            LogError(ex);
            // Redirect to a custom error page
        }
    }

    private void LogError(Exception ex)
    {
        // Log exception details to the Event Viewer
        EventLog.WriteEntry("Application", ex.ToString(), EventLogEntryType.Error);
    }

    private void LoadInitialData()
    {
        // Populate state dropdown with dummy data
        ddlState.Items.Add(new ListItem("--Select--", ""));
        ddlState.Items.Add(new ListItem("State 1", "1"));
        ddlState.Items.Add(new ListItem("State 2", "2"));

        // Populate county dropdown with dummy data
        ddlCounty.Items.Add(new ListItem("--Select--", ""));
        ddlCounty.Items.Add(new ListItem("County 1", "1"));
        ddlCounty.Items.Add(new ListItem("County 2", "2"));

        // Populate payam dropdown with dummy data
        ddlPayam.Items.Add(new ListItem("--Select--", ""));
        ddlPayam.Items.Add(new ListItem("Payam 1", "1"));
        ddlPayam.Items.Add(new ListItem("Payam 2", "2"));

        // Populate boma dropdown with dummy data
        ddlBoma.Items.Add(new ListItem("--Select--", ""));
        ddlBoma.Items.Add(new ListItem("Boma 1", "1"));
        ddlBoma.Items.Add(new ListItem("Boma 2", "2"));

        // Populate currency dropdown with dummy data
        ddlCurrency.Items.Add(new ListItem("--Select--", ""));
        ddlCurrency.Items.Add(new ListItem("SSP", "SSP"));
        ddlCurrency.Items.Add(new ListItem("USD", "USD"));

        // Populate payment cycle dropdown with dummy data
        ddlPaymentCycle.Items.Add(new ListItem("--Select--", ""));
        ddlPaymentCycle.Items.Add(new ListItem("Monthly", "Monthly"));
        ddlPaymentCycle.Items.Add(new ListItem("Quarterly", "Quarterly"));
        ddlPaymentCycle.Items.Add(new ListItem("Bi-Annually", "Bi-Annually"));
        ddlPaymentCycle.Items.Add(new ListItem("Annually", "Annually"));

        // Populate payment status dropdown with dummy data
        ddlPaymentStatus.Items.Add(new ListItem("--Select--", ""));
        ddlPaymentStatus.Items.Add(new ListItem("Paid", "Paid"));
        ddlPaymentStatus.Items.Add(new ListItem("Not Paid", "NotPaid"));

        // Populate component dropdown with dummy data
        ddlComponent.Items.Add(new ListItem("--Select--", ""));
        ddlComponent.Items.Add(new ListItem("LIPW", "LIPW"));
        ddlComponent.Items.Add(new ListItem("DIS", "DIS"));

        // Populate payment partner dropdown with dummy data
        ddlPaymentPartner.Items.Add(new ListItem("--Select--", ""));
        ddlPaymentPartner.Items.Add(new ListItem("Partner 1", "Partner1"));
        ddlPaymentPartner.Items.Add(new ListItem("Partner 2", "Partner2"));
        ddlPaymentPartner.Items.Add(new ListItem("Partner 3", "Partner3"));
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
        try
        {
            LoadPaymentData();
        }
        catch (Exception ex)
        {
            // Log the exception
            LogError(ex);
            // Redirect to a custom error page
            Response.Redirect("~/ErrorPage.aspx");
        }
    }

    private void LoadPaymentData()
    {
        // Fetch payment data based on selected filters
        DataTable dt = GetPaymentData();
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

    

    protected void rgResults_ItemDataBound(object sender, GridItemEventArgs e)
    {
        // Handle any additional item data binding logic here
    }
}
