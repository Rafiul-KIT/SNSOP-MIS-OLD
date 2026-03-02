using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AdminWageProcessDashboard : System.Web.UI.Page
{
    //designed by siva as on 29-1-2016 
    //Purpose : Report for Year wise dashboard
    //Tables used : All
    //Stored procedures Used :YearwiseDashboardforAdmin
   
    General Gen = new General();
    protected void Page_Load(object sender, EventArgs e)
    {
                
        if (Session.Count <= 0)
        {
        //    Response.Redirect("../../frmUserLogin.aspx");
           // Response.Redirect("~/frmuserlogin.aspx");
            Response.Redirect("~/frmuserlogin.aspx");
        }

        if (!IsPostBack)
        {
            lblOfficer.Text = Request.QueryString[0].ToString().Trim();
            String userType = Request.QueryString[0].ToString().Trim();
            if (userType != null)
            {
                if (userType.Equals("Cordinator")) lblOfficer.Text = "Initial Reviewer";
                else if (userType.Equals("Specialist")) lblOfficer.Text = "Financial Approver";
                else if (userType.Equals("Manager")) lblOfficer.Text = "Final Payment Authorizer";
            }


            Session["AdminWageOfficerType"] = Request.QueryString[0].ToString().Trim();

            FillDetails();


            if (Request.QueryString[0].ToString().Trim() == "PA")
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




            if (Request.QueryString[0].ToString().Trim() == "Cordinator")
            {

                Label14.Text = "Pending Wage requests for Review";
                Label15.Text = "Reviewed Wage requests";
                Label16.Text = "Total Invoices Submitted";
                Label17.Text = "Pending Invoices for Review";
                Label18.Text = "Reviewed Invoices";
                Label19.Text = "Rejected Invoices";
            }
            if (Request.QueryString[0].ToString().Trim() == "Specialist")
            {

                Label14.Text = "Pending Wage requests for Verification";
                Label15.Text = "Verified Wage requests";
                Label16.Text = "Total Invoices Submitted";
                Label17.Text = "Pending Invoices for Verification";
                Label18.Text = "Verified Invoices";
                Label19.Text = "Rejected Invoices";
            }
        }
        
        
    }
   
    void FillDetails()
    {

        DataSet ds = new DataSet();

        ds = Gen.getWageProcessDashboardAdmin(Session["uid"].ToString().Trim(), Request.QueryString[0].ToString().Trim());

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
                lnlPA4.Visible = false ;
                lnlPA5.Visible = false;
                lnkOtherD.Visible = true;
                lnkPA6.Visible = false;
            }
          
           // Label5.Text = ds.Tables[0].Rows[0]["Pending"].ToString();

       }


    }


   
}
