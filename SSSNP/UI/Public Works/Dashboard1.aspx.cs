using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Dashboard1 : System.Web.UI.Page
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
            Response.Redirect("../../frmUserLogin.aspx");
        }

        if (!IsPostBack)
        {
            LoadYearDDL();
           // AddLinkAttributes();
          //  ddltypeip.SelectedValue = (DateTime.Now.Year.ToString());
            ddltypeip.SelectedValue = (DateTime.Now.Year.ToString());
            hasflash.Value = HttpUtility.UrlEncode(ddltypeip.SelectedValue.ToString()); // Fix Sec issue XSS
            FillDetails();
        }
    }

    protected void LoadYearDDL()
    {
        List<int> YearList = new List<int>();
        int currentYear = DateTime.Now.Year;
        for (int i = 2016; i < currentYear; i++)
        {
            YearList.Add(i);
        }
        YearList.Add(currentYear);
        ddltypeip.DataSource = YearList;
        ddltypeip.DataBind();

    }

    protected void AddLinkAttributes()
    {
        IPname.Attributes.Add("target", "_blank");
    }

    void FillDetails()
    {
        hasflash.Value = HttpUtility.UrlEncode(ddltypeip.SelectedValue.ToString()); // Fix Sec issue XSS        
        int checkYear;
        int.TryParse(hasflash.Value, out checkYear);
        if (checkYear == 0)
            hasflash.Value = DateTime.Now.Year.ToString();
        //DataSet ds = Gen.YearwiseDashboardforAdmin(Convert.ToString(ddltypeip.SelectedValue));
        DataSet ds = Gen.YearwiseDashboardforAdmin(Convert.ToString(hasflash.Value));
        if (ds.Tables[0].Rows.Count > 0)
        {
            lbl0.Text = ds.Tables[0].Rows[0]["cnt"].ToString();
            lbl1.Text = ds.Tables[1].Rows[0]["cnt"].ToString();
            lbl2.Text = ds.Tables[2].Rows[0]["cnt"].ToString();
            lbl3.Text = ds.Tables[3].Rows[0]["cnt"].ToString();
            lbl4.Text = ds.Tables[4].Rows[0]["cnt"].ToString();
            lbl5.Text = ds.Tables[5].Rows[0]["cnt"].ToString();
            lbl6.Text = ds.Tables[6].Rows[0]["cnt"].ToString();
            lbl7.Text = ds.Tables[7].Rows[0]["cnt"].ToString();
            lbl8.Text = ds.Tables[8].Rows[0]["cnt"].ToString();
            lbl9.Text = ds.Tables[9].Rows[0]["cnt"].ToString();
            lbl10.Text = ds.Tables[10].Rows[0]["cnt"].ToString();
            lbl11.Text = ds.Tables[11].Rows[0]["cnt"].ToString();
            lbl12.Text = ds.Tables[12].Rows[0]["cnt"].ToString();

            lbl13.Text = ds.Tables[15].Rows[0]["cnt"].ToString();
            Label9.Text = ds.Tables[13].Rows[0]["cnt"].ToString();
            lbl_funds_uti.Text = ds.Tables[22].Rows[0]["cnt"].ToString();
            LblEnBen.Text = ds.Tables[23].Rows[0]["cnt"].ToString();
            LblEnBenM.Text = ds.Tables[24].Rows[0]["cnt"].ToString();
            LblEnBenF.Text = ds.Tables[25].Rows[0]["cnt"].ToString();
            int total_groupId = 0;
            int total_cst = 0;
            int param_value = 0;
            decimal project_cost = 0;
            decimal Wage_Rate = 0;
            decimal Exchange_Rate = 0;

            if(ds.Tables[16].Rows.Count>0)
            {
                total_groupId = Convert.ToInt32(ds.Tables[16].Rows[0]["cnt"]);
            }
            if (ds.Tables[17].Rows.Count > 0)
            {
                total_cst = Convert.ToInt32(ds.Tables[17].Rows[0]["cnt"]);
            }
            if (ds.Tables[18].Rows.Count > 0)
            {
                param_value = Convert.ToInt32(ds.Tables[18].Rows[0]["param_value"]);
            }
            if (ds.Tables[19].Rows.Count > 0)
            {
                project_cost = Convert.ToDecimal(ds.Tables[19].Rows[0]["ProjectCost"]);
            }
            if (ds.Tables[20].Rows.Count > 0)
            {
                Wage_Rate = Convert.ToDecimal(ds.Tables[20].Rows[0]["Wage_Rate"]);
            }
            if (ds.Tables[21].Rows.Count > 0)
            {
                Exchange_Rate = Convert.ToDecimal(ds.Tables[21].Rows[0]["Exchange_Rate"]);
            }
            //
            int total_work_days_target = 0;
            int direct_beneficiary = Convert.ToInt32(ds.Tables[9].Rows[0]["cnt"]);

            if(ddltypeip.SelectedValue=="2016")
            {
                total_work_days_target = (total_groupId * 60) + (total_cst * 120);
            }
            else
            {
                total_work_days_target = direct_beneficiary * 90;
            }
            lbl_work_days_target.Text = Convert.ToString(total_work_days_target);
            int no_work_created = Convert.ToInt32(ds.Tables[12].Rows[0]["cnt"]);

            if(no_work_created >0 && total_work_days_target>0)
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
            if(ds.Tables[15].Rows.Count>0)
            {
                usd_paid = Convert.ToDecimal(ds.Tables[15].Rows[0]["cnt"]);
            }
            usd_target = Convert.ToDecimal(total_work_days_target * Wage_Rate);
            if (usd_paid >0 && usd_target>0)
            {
                Percentage_usd_target = (usd_paid / usd_target) * 100;
            }
            Label6.Text = (Percentage_usd_target).ToString("0.00");

            //SSP
          
            string Total_Wages_Target_Usd = (total_work_days_target * Wage_Rate).ToString("0.00");
            Label8.Text = (Convert.ToDecimal(Total_Wages_Target_Usd) * Exchange_Rate).ToString("0.00");
            
            string total_wages_target_ssp = (Convert.ToDecimal(Total_Wages_Target_Usd) * Exchange_Rate).ToString("0.00");

            if(Convert.ToInt32(ds.Tables[13].Rows[0]["cnt"]) >0 && Convert.ToDecimal(total_wages_target_ssp) >0)
            {
                string Percentage_Wages_created_ssp = Convert.ToString((Convert.ToInt32(ds.Tables[13].Rows[0]["cnt"]) / Convert.ToDecimal(total_wages_target_ssp)) * 100);
                Label10.Text = string.Format("{0:0.00}", Convert.ToDecimal(Percentage_Wages_created_ssp));
            }
            else
            {
                Label10.Text = "0.00";
            }
           
           
            Label11.Text = project_cost.ToString("0.00");
            decimal funds_disbursement = 0;
            if(ds.Tables[8].Rows.Count>0)
            {
                funds_disbursement = Convert.ToDecimal(ds.Tables[8].Rows[0]["cnt"]);
            }
            if(project_cost >0 && funds_disbursement>0)
            {
                Label13.Text = ((funds_disbursement / project_cost) * 100).ToString("0.00");
            }
            else
            {
                Label13.Text = "0.00";
            }
        }
    }



    protected void ddltypeip_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillDetails();
    }

    protected void IPname_Click(object sender, EventArgs e)
    {        
        //Response.Redirect("DrillDownIPname.aspx?Year=" + hasflash.Value + "&status=1");
        //Response.Write("<script>window.open('DrillDownIPname.aspx?Year=" + hasflash.Value + "&status=1','_blank')</script>");
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownIPname.aspx?Year=" + hasflash.Value + "&status=1','_blank')", true);
    }

    protected void CountriesDashboard_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownCountriesDashboard.aspx?Year=" + hasflash.Value + "&status=2','_blank')", true);
    }

    protected void PayamsDashboard_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownPayamsDashboard.aspx?Year=" + hasflash.Value + "&status=3','_blank')", true);
    }

    protected void BomasDashboard_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownBomasDashboard.aspx?Year=" + hasflash.Value + "&status=4','_blank')", true);
    }

    protected void WorksApprovedDashboard_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownFundDistributedReportsDashboard.aspx?Year=" + hasflash.Value + "&status=6','_blank')", true);
    }

    protected void WorksInProgressDashboard_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownFundDistributedReportsDashboard.aspx?Year=" + hasflash.Value + "&status=7','_blank')", true);
    }

    protected void WorksCompletedDashboard_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownFundDistributedReportsDashboard.aspx?Year=" + hasflash.Value + "&status=8','_blank')", true);
    }

    protected void WorksProposedDashboard_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownFundDistributedReportsDashboard.aspx?Year=" + hasflash.Value + "&status=5','_blank')", true);
    }

    protected void AllocatedBeneficiaries_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownBenificiaryWagespaymentsReportsDashboard.aspx?Year=" + hasflash.Value + "&status=9','_blank')", true);
    }

    protected void AllocatedMaleBeneficiaries_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownBenificiaryWagespaymentsReportsDashboard.aspx?Year=" + hasflash.Value + "&status=10','_blank')", true);
    }

    protected void AllocatedFemaleBeneficiaries_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Close", "window.open('DrillDownBenificiaryWagespaymentsReportsDashboard.aspx?Year=" + hasflash.Value + "&status=11','_blank')", true);
    }
}
