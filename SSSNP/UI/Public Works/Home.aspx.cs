using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Text;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

public partial class Home : System.Web.UI.Page
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
            //  ddltypeip.SelectedValue = (DateTime.Now.Year.ToString());
            ddlYear.SelectedValue = (DateTime.Now.Year.ToString());
            hasflash.Value = ddlYear.SelectedValue;
            FillDetails();
        }
    }

    void FillDetails()
    {
        hasflash.Value = ddlYear.SelectedValue;
        DataSet ds = Gen.GetGenericDashboardKIT(Convert.ToString(ddlYear.SelectedValue), Session["User_Code"].ToString().Trim());
        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
        {
            lblNameOfIP.Text = ds.Tables[0].Rows[0]["cnt"].ToString();
            if (ds.Tables.Count > 1)
            {
                ImplementingStateCount.Text = ds.Tables[1].Rows[0]["state_count"].ToString();
                ImplementingCountyCount.Text = ds.Tables[1].Rows[0]["county_count"].ToString();
                ImplementingPayamCount.Text = ds.Tables[1].Rows[0]["payam_count"].ToString();
                ImplementingBomaCount.Text = ds.Tables[1].Rows[0]["boma_count"].ToString();
            }
            if (ds.Tables.Count > 2)
            {
                LblEnBen.Text = ds.Tables[2].Rows[0]["enrolled_count"].ToString();
                LblEnBenM.Text = ds.Tables[2].Rows[0]["male_count"].ToString();
                LblEnBenF.Text = ds.Tables[2].Rows[0]["female_count"].ToString();
                BindchartGender(LblEnBenM.Text, LblEnBenF.Text);
            }
            if (ds.Tables.Count > 3)
            {
                LabelTotalBeneficiaryCount.Text = ds.Tables[3].Rows[0]["total_beneficiary_count"].ToString().Trim();
                LabelPendingBeneficiaryCount.Text = ds.Tables[3].Rows[0]["pending_beneficiary_count"].ToString().Trim();
                BindchartTotalVPending(LabelTotalBeneficiaryCount.Text, LabelPendingBeneficiaryCount.Text);
            }
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        Chart1.Visible = false;
        Chart2.Visible = false;
        FillDetails();
    }

    private void BindchartGender(String maleCount, String femaleCount)
    {
        if (maleCount == null || maleCount.Equals("") || femaleCount == null || femaleCount.Equals(""))
        {
            return;
        }
        string[] XPointMember = new string[2];
        int[] YPointMember = new int[2];

        int count = 0;
        XPointMember[count] = "Male";
        YPointMember[count] = Convert.ToInt32(maleCount);
        count += 1;

        XPointMember[count] = "Female";
        YPointMember[count] = Convert.ToInt32(femaleCount);
        Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        //Chart1.Series[0].BorderWidth = 10;
        //Chart1.Series[0].ChartType = SeriesChartType.Bar;
        ////Chart1.Series[0].ChartType = SeriesChartType.StackedBar;
        //Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
        //Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
        //Chart1.ChartAreas["ChartArea1"].Area3DStyle.Enable3D = false;

        Chart1.Series[0].BorderWidth = 10;
        Chart1.Series[0].ChartType = SeriesChartType.Pie;
        foreach (Series charts in Chart1.Series)
        {
            foreach (DataPoint point in charts.Points)
            {
                switch (point.AxisLabel)
                {
                    case "Male": point.Color = Color.ForestGreen; break;
                    case "Female": point.Color = Color.DeepPink; break;
                }
                point.Label = string.Format("{0:0} - {1}", point.YValues[0], point.AxisLabel);

            }
        }

        Chart1.Visible = true;
    }

    private void BindchartTotalVPending(String TotalBeneficiaryCount, String PendingBeneficiaryCount)
    {
        string[] XPointMember = new string[2];
        int[] YPointMember = new int[2];

        int count = 0;

        if (PendingBeneficiaryCount != null && !PendingBeneficiaryCount.Equals(""))
        {
            XPointMember[count] = "Pending Beneficiary";
            YPointMember[count] = Convert.ToInt32(PendingBeneficiaryCount);
        }

        count += 1;

        if (TotalBeneficiaryCount != null && !TotalBeneficiaryCount.Equals("") && !TotalBeneficiaryCount.Equals("0"))
        {
            XPointMember[count] = "Total Beneficiary";
            YPointMember[count] = Convert.ToInt32(TotalBeneficiaryCount);
        } else
        {
            return;
        }

        Chart2.Series[0].Points.DataBindXY(XPointMember, YPointMember);

        Chart2.Series[0].BorderWidth = 10;
        Chart2.Series[0].ChartType = SeriesChartType.Bar;
        //Chart2.Series[0].ChartType = SeriesChartType.StackedBar;
        Chart2.ChartAreas["ChartArea2"].AxisX.MajorGrid.Enabled = false;
        Chart2.ChartAreas["ChartArea2"].AxisY.MajorGrid.Enabled = false;
        Chart2.ChartAreas["ChartArea2"].Area3DStyle.Enable3D = false;

        Chart2.Visible = true;
    }
}
