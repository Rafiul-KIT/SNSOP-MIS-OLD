using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class IPDashboard1 : System.Web.UI.Page
{
    //designed by siva as on 29-1-2016 
    //Purpose : Report for Year wise dashboard
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
        string s = Session["User_Code"].ToString();

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

            Label2.Text = ds.Tables[0].Rows[0]["TotalBenificiariesWMW"].ToString();
            //Label5.Text = ds.Tables[0].Rows[0]["TotalFemaleBenificiariesWMW"].ToString();
            //Label7.Text = ds.Tables[0].Rows[0]["TotalMaleBenificiariesWMW"].ToString();


            lbl12.Text = ds.Tables[0].Rows[0]["NoofWorkdaysCreated"].ToString();
            if(Convert.ToString(ds.Tables[0].Rows[0]["TotalWageAmount"])!="")
            {
                lbl13.Text = ds.Tables[0].Rows[0]["TotalWageAmount"].ToString();
            }
            if (Convert.ToString(ds.Tables[0].Rows[0]["TotalWageAmountUSD"]) != "")
            {
                lblpaidusd.Text = ds.Tables[0].Rows[0]["TotalWageAmountUSD"].ToString();
            }
            if (Convert.ToString(ds.Tables[0].Rows[0]["Total_Funds"]) != "")
            {
                lbl_funds_uti.Text = ds.Tables[0].Rows[0]["Total_Funds"].ToString();
            }
            Label1.Text = ds.Tables[0].Rows[0]["NoofWorksRejected"].ToString();
            Session["Applid"] = ds.Tables[0].Rows[0]["intIP"].ToString();


            //
            int total_groupId = 0;
            int total_cst = 0;
            int total_groupId_2016 = 0;
            int total_cst_2016 = 0;
            int param_value = 0;
            decimal project_cost = 0;
            decimal Wage_Rate = 0;
            decimal Exchange_Rate = 0;

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Group_cnt"]) > 0)
            {
                total_groupId = Convert.ToInt32(ds.Tables[0].Rows[0]["Group_cnt"]);
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["cst_cnt"]) > 0)
            {
                total_cst = Convert.ToInt32(ds.Tables[0].Rows[0]["cst_cnt"]);
            }

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["Group_2016"]) > 0)
            {
                total_groupId_2016 = Convert.ToInt32(ds.Tables[0].Rows[0]["Group_2016"]);
            }
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["cst_2016"]) > 0)
            {
                total_cst_2016 = Convert.ToInt32(ds.Tables[0].Rows[0]["cst_2016"]);
            }

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["param_value"]) > 0)
            {
                param_value = Convert.ToInt32(ds.Tables[0].Rows[0]["param_value"]);
            }

            if (Convert.ToString(ds.Tables[0].Rows[0]["ProjectCost"])!="")
            {
                project_cost = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectCost"]);
            }
            if (Convert.ToString(ds.Tables[0].Rows[0]["Wage_Rate"])!="")
            {
                Wage_Rate = Convert.ToDecimal(ds.Tables[0].Rows[0]["Wage_Rate"]);
            }
            if (Convert.ToString(ds.Tables[0].Rows[0]["Exchange_Rate"])!="")
            {
                Exchange_Rate = Convert.ToDecimal(ds.Tables[0].Rows[0]["Exchange_Rate"]);
            }

            //
            int total_work_days_target = 0;
            int direct_beneficiary = Convert.ToInt32(ds.Tables[0].Rows[0]["TotalBenificiaries"]);

            //total_work_days_target = (total_groupId * 60) + (total_cst * 120);
            if (total_groupId_2016>0 || total_cst_2016>0)
            {
                total_work_days_target = (total_groupId_2016 * 60) + (total_cst_2016 * 120);
            }
            total_work_days_target = total_work_days_target + direct_beneficiary * 90;
            lbl_work_days_target.Text = Convert.ToString(total_work_days_target);

            int no_work_created = Convert.ToInt32(ds.Tables[0].Rows[0]["NoofWorkdaysCreated"]);

            if (no_work_created > 0 && total_work_days_target > 0)
            {              
                string Percentage_Work_Days_Created = Convert.ToString((no_work_created / Convert.ToDecimal(total_work_days_target)) * 100);
                Label3.Text = string.Format("{0:0.00}", Convert.ToDecimal(Percentage_Work_Days_Created));
            }
            else
            {
                Label3.Text = "0.00";
            }

            //USD
            Label4.Text = (total_work_days_target * Wage_Rate).ToString("0.00");
            decimal usd_paid = 0;
            decimal usd_target = 0;
            decimal Percentage_usd_target = 0;
            if (Convert.ToString(ds.Tables[0].Rows[0]["TotalWageAmountUSD"])!="")
            {
                usd_paid = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalWageAmountUSD"]);
            }
            usd_target = Convert.ToDecimal(total_work_days_target * Wage_Rate);
            if (usd_paid > 0 && usd_target > 0)
            {
                Percentage_usd_target = (usd_paid / usd_target) * 100;
            }
            Label6.Text = (Percentage_usd_target).ToString("0.00");

            //SSP
            //Label8.Text = (total_work_days_target * Exchange_Rate).ToString("0.00");

            string Total_Wages_Target_Usd = (total_work_days_target * Wage_Rate).ToString("0.00");
            Label8.Text = (Convert.ToDecimal(Total_Wages_Target_Usd) * Exchange_Rate).ToString("0.00");

            //ds.Tables[0].Rows[0]["TotalWageAmount"]
            string total_wages_target_ssp = (Convert.ToDecimal(Total_Wages_Target_Usd) * Exchange_Rate).ToString("0.00");

            if (Convert.ToInt32(ds.Tables[0].Rows[0]["TotalWageAmount"]) > 0 && Convert.ToDecimal(total_wages_target_ssp) > 0)
            {
                string Percentage_Wages_created_ssp = Convert.ToString((Convert.ToInt32(ds.Tables[0].Rows[0]["TotalWageAmount"]) / Convert.ToDecimal(total_wages_target_ssp)) * 100);
                Label10.Text = string.Format("{0:0.00}", Convert.ToDecimal(Percentage_Wages_created_ssp));
            }
            else
            {
                Label10.Text = "0.00";
            }
            
            Label11.Text = project_cost.ToString("0.00");
            decimal funds_disbursement = 0;
            if (Convert.ToString(ds.Tables[0].Rows[0]["Fundsdisbursed"])!="")
            {
                funds_disbursement = Convert.ToDecimal(ds.Tables[0].Rows[0]["Fundsdisbursed"]);
            }
            if (project_cost > 0 && funds_disbursement > 0)
            {
                Label13.Text = ((funds_disbursement / project_cost) * 100).ToString("0.00");
            }

        }
    }
    
   
}
