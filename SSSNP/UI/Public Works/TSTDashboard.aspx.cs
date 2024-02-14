using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class TSTDashboard : System.Web.UI.Page
{//designed by siva as on 29-1-2016 Work status wise Report
    General Gen = new General();
    protected void Page_Load(object sender, EventArgs e)
    {
                
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }

        if (!IsPostBack)
        {
            FillDetails();
        }

        
    }

    void FillDetails()
    {
        string TST = "",IP="";
        if (Session["user_type"].ToString() == "TST")
        {
            TST = Session["User_Code"].ToString();
            IP = "%";
        }
        else if (Session["user_type"].ToString() == "IP")
        {
            TST = "%";
            IP = Session["User_Code"].ToString();
        }
        else
        {
            TST = "%";
            IP = "%";
        }
        hdfID.Value = Session["User_Code"].ToString();

        DataSet ds = new DataSet();

        ds = Gen.GetWorkStatusReport(TST, IP);

        if (ds.Tables[0].Rows.Count > 0)
        {
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
        }
        else
        {
            grdDetails.DataSource = null;
            grdDetails.DataBind();
        }

        DataSet dashboard = Gen.GetWorkStatusReportByTST(Session["User_Code"].ToString());

        if (dashboard.Tables.Count > 0)
        {
            lblworks.Text = dashboard.Tables[0].Rows[0]["TotalWorkProposed"].ToString();
            lbltobeApproved.Text = dashboard.Tables[0].Rows[0]["TotaltobeApproved"].ToString();
            lbltobeFinApproved.Text = dashboard.Tables[0].Rows[0]["TotaltobeFinApproved"].ToString();
            lblWorkInProgress.Text = dashboard.Tables[0].Rows[0]["TotalInProgress"].ToString();
            lblworkCompleted.Text = dashboard.Tables[0].Rows[0]["TotalCompleted"].ToString();
            lblIps.Text = dashboard.Tables[0].Rows[0]["ToatlIPS"].ToString();
            lblFundReleased.Text = dashboard.Tables[0].Rows[0]["FundReleased"].ToString();
            //Ips.NavigateUrl = "../Lookups/LookupTSTI.aspx?id="+ hdfID.Value;

            //Ips

            lblCA.Text = dashboard.Tables[0].Rows[0]["ToatlCAs"].ToString();
            lblPDC.Text = dashboard.Tables[0].Rows[0]["ToatlPDC"].ToString();
            lblBDC.Text = dashboard.Tables[0].Rows[0]["ToatlBDC"].ToString();
        }

    }
    
   
}
