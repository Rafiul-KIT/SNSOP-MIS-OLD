using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class WageIPProcessDashboard : System.Web.UI.Page
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
          //  lblOfficer.Text = Session["user_type"].ToString().Trim();

            FillDetails();
        }
        
        
    }
   
    void FillDetails()
    {

        DataSet ds = new DataSet();

        ds = Gen.getIPWageProcessDashboard(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim());

        if (ds.Tables[0].Rows.Count > 0)
        {





            Label1.Text = ds.Tables[0].Rows[0]["TotalReq"].ToString().Trim();
            Label26.Text = ds.Tables[0].Rows[0]["TotalReqCoodinator"].ToString().Trim();

            Label41.Text = ds.Tables[0].Rows[0]["TotalReqSepcialist"].ToString().Trim();

            //TotalReqManager
            Label8.Text = ds.Tables[0].Rows[0]["TotalReqManager"].ToString().Trim();
            Label6.Text = ds.Tables[0].Rows[0]["TotalReqRejected"].ToString().Trim();


            Label2.Text = ds.Tables[0].Rows[0]["TotalReqpayingagent"].ToString().Trim();
            Label3.Text = ds.Tables[0].Rows[0]["TotalReqPayCoOrdinator"].ToString().Trim();

            Label4.Text = ds.Tables[0].Rows[0]["TotalReqPaySpecialist"].ToString().Trim();
            Label5.Text = ds.Tables[0].Rows[0]["TotalReqPayManager"].ToString().Trim();

            Label7.Text = ds.Tables[0].Rows[0]["TotalReqPayPayingAgentConfirmation"].ToString().Trim();

            Label10.Text = ds.Tables[0].Rows[0]["TotalReqPayPayingAgentDone"].ToString().Trim();
            Label9.Text = ds.Tables[0].Rows[0]["TotalReqRejectedPay"].ToString().Trim();
           // Label5.Text = ds.Tables[0].Rows[0]["Pending"].ToString();
            Label12.Text = ds.Tables[0].Rows[0]["CoOrdinatorRejectWage"].ToString().Trim();
            Label15.Text = ds.Tables[0].Rows[0]["CoOrdinatorRejectInvoice"].ToString().Trim();
            

       }


    }


   
}
