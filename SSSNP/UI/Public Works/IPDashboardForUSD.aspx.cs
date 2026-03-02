using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class IPDashboardForUSD : System.Web.UI.Page
{
    //designed by israr as on 29-07-2017 
    //Purpose : wages rate in USD  dashboard
    //Tables used : All
    //Stored procedures Used :YearwiseDashboardforAdmin
    
    General Gen = new General();

    protected void Page_Load(object sender, EventArgs e)
    {
                
        if (!IsPostBack)
        {
            FillDetails();

            //ddltypeip.Items.Add(System.DateTime.Now.Year.ToString());
        }

    }

    void FillDetails()
    {


        DataSet ds = Gen.IPWISEDASHBORDLATEST(Session["User_Code"].ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl0.Text = ds.Tables[0].Rows[0]["IPName"].ToString();
            lbl1.Text = ds.Tables[0].Rows[0]["NoofCounties"].ToString();
            lbl2.Text = ds.Tables[0].Rows[0]["NoofPayams"].ToString();
            lbl3.Text = ds.Tables[0].Rows[0]["NoofBomas"].ToString();
            lbl4.Text = ds.Tables[0].Rows[0]["NoofWorksProposed"].ToString();
            lbl5.Text = ds.Tables[0].Rows[0]["NoofWorkstobeapproved"].ToString();
            lbl6.Text = ds.Tables[0].Rows[0]["NoofWorksinprogress"].ToString();
            lbl7.Text = ds.Tables[0].Rows[0]["NoofWorksclosed"].ToString();
            lbl8.Text = ds.Tables[0].Rows[0]["Fundsdisbursed"].ToString();
            lbl9.Text = ds.Tables[0].Rows[0]["TotalBenificiaries"].ToString();
            lbl10.Text = ds.Tables[0].Rows[0]["TotalFemaleBenificiaries"].ToString();
            lbl11.Text = ds.Tables[0].Rows[0]["TotalMaleBenificiaries"].ToString();
            lbl12.Text = ds.Tables[0].Rows[0]["NoofWorkdaysCreated"].ToString();
            lbl13.Text = ds.Tables[0].Rows[0]["TotalWageAmount"].ToString();
            Label1.Text = ds.Tables[0].Rows[0]["NoofWorksRejected"].ToString();
            Session["Applid"] = ds.Tables[0].Rows[0]["intIP"].ToString();
            

        }
    }
    
   
}
