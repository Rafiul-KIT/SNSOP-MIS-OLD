using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class IPDashboard : System.Web.UI.Page
{

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


        DataSet ds =  Gen.IPDashboardDetails (Session["uid"].ToString());
       if (ds.Tables[0].Rows.Count > 0)
       {
           lbl0.Text = ds.Tables[0].Rows[0]["InvoiceSubmitted"].ToString();
            lbl1.Text = ds.Tables[0].Rows[0]["InvoiceReviewed"].ToString();
            lbl2.Text = ds.Tables[0].Rows[0]["InvoiceVerified"].ToString();
            lbl7.Text = ds.Tables[0].Rows[0]["InvoiceApproved"].ToString();
           
           lbl9.Text = ds.Tables[0].Rows[0]["InvoiceRejected"].ToString();
           

       }
    }
}