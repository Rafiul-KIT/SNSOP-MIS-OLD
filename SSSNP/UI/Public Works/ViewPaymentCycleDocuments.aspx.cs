using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class ViewPaymentCycleDocuments : System.Web.UI.Page
{
    comFunctions cmf = new comFunctions();
    General Gen = new General();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("~/frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            getPaymentCycle();
            rgResults.Visible = false;
        }
    }

    private void getPaymentCycle()
    {
        DataSet dsPaymentCycle = new DataSet();
        dsPaymentCycle = Gen.getPaymentCycle();
        if (dsPaymentCycle.Tables[0].Rows.Count > 0)
        {
            ddlPaymentCycle.DataSource = dsPaymentCycle.Tables[0];
            ddlPaymentCycle.DataTextField = "WagePaymentCycle";
            ddlPaymentCycle.DataValueField = "intPaymentCycleID";
            ddlPaymentCycle.DataBind();
        }
        ddlPaymentCycle.Items.Insert(0, new ListItem("--Select--", ""));
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            rgResults.Visible = true; // Show the grid when the search button is clicked

            // Initialize total record count
            string year = ddlYear.SelectedValue.ToString();
            string paymentCycle = ddlPaymentCycle.SelectedValue.ToString();

            //Session["TotalRecordCount"] = Gen.getPaymentCycleDocCount(year, paymentCycle);

            rgResults.CurrentPageIndex = 0; // Reset to the first page
            rgResults.Rebind();
        }
        catch (Exception ex)
        {
            LogError("Error in BtnSearch_Click: " + ex.Message, ex);
            throw;
        }
    }

    //Update rgResults_NeedDataSource to Use Cached Total Record Count
    protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            Log("rgResults_NeedDataSource started.");
            string year = ddlYear.SelectedValue;
            string paymentCycle = ddlPaymentCycle.SelectedValue;

            //int totalRecords = (int)Session["TotalRecordCount"]; // Use cached total record count
            DataSet ds = Gen.getPaymentCycleDocuments(year, paymentCycle);
            int totalRecords = 0;
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                rgResults.DataSource = dt;
                totalRecords = dt.Rows.Count;
                rgResults.VirtualItemCount = totalRecords; // Set the total number of records
            }            
        }
        catch (Exception ex)
        {
            LogError("Error in rgResults_NeedDataSource: " + ex.Message, ex);
            throw;
        }
    }

    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem dataItem = (GridDataItem)e.Item;
            string doc1Path = dataItem["Year"].Text + "/Payment_Cycle_" + dataItem["PaymentCycle"].Text
                + "/File_1/" + dataItem["Doc1Column"].Text;

            string fileName = dataItem["Doc1Column"].Text;
            string fileUrl = ConfigurationManager.AppSettings["SSSNPWEB"].ToString() + "snsop_docs/" + doc1Path;
            dataItem["Doc1Column"].Text = $"<a href='{fileUrl}' download>{fileName}</a>";

            string doc2Path = dataItem["Year"].Text + "/Payment_Cycle_" + dataItem["PaymentCycle"].Text
                + "/File_2/" + dataItem["Doc2Column"].Text;

            string fileName2 = dataItem["Doc2Column"].Text;
            string fileUrl2 = ConfigurationManager.AppSettings["SSSNPWEB"].ToString() + "snsop_docs/" + doc2Path;
            dataItem["Doc2Column"].Text = $"<a href='{fileUrl2}' download>{fileName2}</a>";

            string doc3Path = dataItem["Year"].Text + "/Payment_Cycle_" + dataItem["PaymentCycle"].Text
                + "/File_3/" + dataItem["Doc3Column"].Text;

            string fileName3 = dataItem["Doc3Column"].Text;
            string fileUrl3 = ConfigurationManager.AppSettings["SSSNPWEB"].ToString() + "snsop_docs/" + doc3Path;
            dataItem["Doc3Column"].Text = $"<a href='{fileUrl3}' download>{fileName3}</a>";
        }
    }

    private void Log(string message)
    {
        // Implement your logging logic here
        System.Diagnostics.Debug.WriteLine(message);
    }

    private void LogError(string message, Exception ex)
    {
        // Implement your error logging logic here
        System.Diagnostics.Debug.WriteLine(message);
        // Example: ErrorLogger.LogError(ex);
    }

}