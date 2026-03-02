#region Using/Inherits Classess
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
#endregion

#region Amendment History Begin
//Form Name : Wage Process Dashboard
//Purpose : To  Wage Process Dashboard
//Tables used : td_WorkWageRequest,td_WorkWageRequestPA,td_WorkWageRequest,td_WageReqBenPADet
//Stored procedures Used : getWageProcessDashboard
#endregion Amendment History End

public partial class WageProcessDashboard : System.Web.UI.Page
{

    #region reference class calling and Variable declaration
    General Gen = new General();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // If Session cleared, Redirect to Login page       
        if (Session.Count <= 0)
        {

            Response.Redirect("~/frmuserlogin.aspx");
        }

        #region Is not Postback Statement
        if (!IsPostBack)
        {
            string userType = Session["user_type"].ToString().Trim();
            switch (userType)
            {
                case "Cordinator":
                    userType = "Initial Reviewer";
                    break;
                case "Specialist":
                    userType = "Financial Approver";
                    break;
                case "Manager":
                    userType = "Final Payment Authorizer";
                    break;
            }
            lblOfficer.Text = userType;

            FillDetails();


            if (Session["user_type"].ToString().Trim() == "PA")
            {

                lnkOther.Visible = false;
                lnkPA.Visible = true;
                lnkPA1.Visible = true;
                lnkInvoiceCofirmationPending.Visible = false;
            }
            else
            {
                lnkOther.Visible = true;
                lnkPA.Visible = false;
                lnkPA1.Visible = false;
                lnkInvoiceCofirmationPending.Visible = true;

            }
            if (Session["user_type"].ToString().Trim() == "Cordinator")
            {

                Label14.Text = "Pending Wage requests for Review";
                Label15.Text = "Reviewed Wage requests";
                Label16.Text = "Total Invoices Submitted";
                Label17.Text = "Pending Invoices for Review";
                Label18.Text = "Reviewed Invoices";
                Label19.Text = "Rejected Invoices";
            }
            if (Session["user_type"].ToString().Trim() == "Specialist")
            {

                Label14.Text = "Pending Wage requests for Verification";
                Label15.Text = "Verified Wage requests";
                Label16.Text = "Total Invoices Submitted";
                Label17.Text = "Pending Invoices for Verification";
                Label18.Text = "Verified Invoices";
                Label19.Text = "Rejected Invoices";
            }
        }
        #endregion


    }
    #endregion

    #region Search Report and Filling Details Functions
    void FillDetails()
    {

        DataSet ds = new DataSet();
        DataSet dsLoc = new DataSet();

        //ds = Gen.getWageProcessDashboard(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim());
        ds = Gen.getWageProcessDashboardKIT(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim());
    

        if (ds.Tables[0].Rows.Count > 0)
        {
            Label1.Text = ds.Tables[0].Rows[0]["TotalReq"].ToString().Trim();
            Label26.Text = ds.Tables[0].Rows[0]["TotalReqPending"].ToString().Trim();
            Label9.Text = ds.Tables[0].Rows[0]["TotalReqPending"].ToString().Trim();

            Label41.Text = ds.Tables[0].Rows[0]["TotalReqAprroved"].ToString().Trim();
            Label8.Text = ds.Tables[0].Rows[0]["TotalReqRejected"].ToString().Trim();

            Label2.Text = ds.Tables[0].Rows[0]["TotalReqPay"].ToString().Trim();
            Label3.Text = ds.Tables[0].Rows[0]["TotalReqPendingPay"].ToString().Trim();
            if (Session["user_type"].ToString().Trim() == "PA")
            {
                Label10.Text = ds.Tables[0].Rows[0]["TotalReqPendingforAfterGeneration"].ToString().Trim();
                Label20.Text = ds.Tables[0].Rows[0]["TotalReqRejectedPayCo"].ToString().Trim();
            }


            Label4.Text = ds.Tables[0].Rows[0]["TotalReqAprrovedPay"].ToString().Trim();
            Label11.Text = ds.Tables[0].Rows[0]["TotalReqAprrovedPay"].ToString().Trim();
            Label5.Text = ds.Tables[0].Rows[0]["TotalReqRejectedPay"].ToString().Trim();
            Label12.Text = ds.Tables[0].Rows[0]["TotalReqRejectedPay"].ToString().Trim();

            Label13.Text = ds.Tables[0].Rows[0]["TotalReqAprroved"].ToString().Trim();

            //lblTotalWageLocust.Text = dsLoc.Tables[0].Rows[0]["TotalReq"].ToString().Trim();

            if (Session["user_type"].ToString().Trim() == "PA")
            {
                trPA.Visible = true;
                trPA1.Visible = true;
                Label7.Text = ds.Tables[0].Rows[0]["TotalReqPendingAfterSelection"].ToString().Trim();
                lnlOtherA.Visible = false;
                lnlOtherB.Visible = false;
                lnlPA4.Visible = true;
                lnlPA5.Visible = true;
                lnkOtherD.Visible = false;
                lnkPA6.Visible = true;

            }
            else
            {
                trPA.Visible = false;
                trPA1.Visible = false;
                lnlOtherA.Visible = true;
                lnlOtherB.Visible = true;
                lnlPA4.Visible = false;
                lnlPA5.Visible = false;
                lnkOtherD.Visible = true;
                lnkPA6.Visible = false;
            }

            // Label5.Text = ds.Tables[0].Rows[0]["Pending"].ToString();

        }


    }
    #endregion


}
