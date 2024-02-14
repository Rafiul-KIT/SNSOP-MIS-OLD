using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SpecialistInvoiceDashboard : System.Web.UI.Page
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


        DataSet ds = Gen.SpecialistDashboardDetails (Session["uid"].ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl0.Text = ds.Tables[0].Rows[0]["InvoiceSubmittedForReview"].ToString();
            lbl1.Text = ds.Tables[0].Rows[0]["PendingInvoiceForReviewRejection"].ToString();

            lbl7.Text = ds.Tables[0].Rows[0]["InvoiceReviewed"].ToString();

            lbl9.Text = ds.Tables[0].Rows[0]["InvoiceRejected"].ToString();


        }
    }
}