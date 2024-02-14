using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class WBWageProcessDashboard : System.Web.UI.Page
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
            lblOfficer.Text = Session["user_type"].ToString().Trim();

            FillDetails();


           
        }
        
        
    }
   
    void FillDetails()
    {

        DataSet ds = new DataSet();

        ds = Gen.getWageProcessDashboard(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim());

        if (ds.Tables[0].Rows.Count > 0)
        {





          
        
            Label2.Text = ds.Tables[0].Rows[0]["TotalReqPay"].ToString().Trim();
            Label3.Text = ds.Tables[0].Rows[0]["TotalReqPendingPay"].ToString().Trim();
            
                
            

            Label4.Text = ds.Tables[0].Rows[0]["TotalReqAprrovedPay"].ToString().Trim();
            Label5.Text = ds.Tables[0].Rows[0]["TotalReqRejectedPay"].ToString().Trim();




           
          
           // Label5.Text = ds.Tables[0].Rows[0]["Pending"].ToString();

       }


    }


   
}
